<template>
  <div class="demo_page">
    <div class="title_tab">
      <van-tabs v-model="active" sticky swipeable color="#FF6324" line-width="40">
        <van-tab title="等级规则">
          <div class="box">
            <div class="box_one">
              <div class="top">
                <p class="p1">当前等级：{{member}}</p>
                <p class="p2">距离{{nextMemberGrade}}还需6563成长值哦~</p>
              </div>
              <div class="bottom">
                <p class="num">
                  <text class="num_one">0</text>
                  <text class="num_one">1</text>
                  <text class="num_one">400</text>
                  <text class="num_one">3000</text>
                  <text class="num_one">8000</text>
                  <text class="num_one">20000</text>
                </p>
                <van-progress
                  :percentage="progress"
                  color="linear-gradient(90deg,rgba(255,226,136,1),rgba(255,99,36,1));"
                />
                <div class="img_text">
                  <div class="member_one" v-for="(item,index) in memberarr" :key="index">
                    <img :src="item.src" alt class="img" />
                    <text class="zhuce">{{item.text}}</text>
                  </div>
                </div>
              </div>
              <img :src="src" alt class="img1" />
            </div>
          </div>
          <div>
            <p class="hemember">AERP会员成长</p>
            <p class="tab">
              <text class="txt1">会员等级</text>
              <text class="txt2">成长值</text>
              <text class="txt3">等级有效期及升降级标准</text>
            </p>
            <p class="one_content" v-for="(items,indexs) in memberGrades" :key="indexs">
              <img :src="src1" class="txt1" v-if="items.memberGrade == 0" />
              <img :src="src3" class="txt1" v-if="items.memberGrade == 10" />
              <img :src="src4" class="txt1" v-if="items.memberGrade == 20" />
              <img :src="src5" class="txt1" v-if="items.memberGrade == 30" />
              <img :src="src6" class="txt1" v-if="items.memberGrade == 40" />
              <img :src="src7" class="txt1" v-if="items.memberGrade == 50" />
              <text class="txt2" v-if="items.startValue == 20000">20000+</text>
              <text class="txt2" v-else>{{items.startValue}}-{{items.endValue}}</text>
              <text class="txt3">{{items.remark}}</text>
            </p>
          </div>
          <div class="shuoming">
            <p class="p1">1、银牌及以上会员的星级有效期为1年，自完成升星之日起;注册会员和铜牌会员的星级有效期长期有效</p>
            <p class="p1">2、当星级到期时，将扣除银牌及以上会员不同数量的成长值,并根据剩余成长值重新计算会员星级</p>
            <p class="p1">3、若达到.上一级别的级别标准，即可升级。若未达到，将顺次下调一个会员级别</p>
          </div>
          <p class="get">如何获得成长值</p>
          <div class="footer">
            <p class="footer_one">1) 购买产品</p>
            <p
              class="footer_two"
            >成长值数据根据订单实付金额计算。1元=1点成长值，所有成长值取订单实付金额整数，小数点后全部舍弃(例:实付金额9.98元订单成长值计9)。退款部分成长值减少，以退款成功时间起取整数，次日凌晨统一处理。</p>
            <p class="footer_one margin">2) 订单点评审核通过</p>
            <p class="footer_two">加10成长值，点评审核通过后发放成长值。</p>
            <p class="footer_one margin">3) 订单点评加精华</p>
            <p class="footer_two">加10成长值，点评被评为精华点评后发放成长值。每个自然月通过点评(包括订单点评审核通过和订单点评加精华)最高加100成长值。</p>
          </div>
        </van-tab>
        <van-tab title="我的成长值">
          <div :style="{minHeight:windowHeight}" class="paddi">
            <div class="showbox">
              <div class="box_one">
                <div class="top">
                  <p class="p1">我的成长值</p>
                  <p class="p2">{{currentGrowthValue}}</p>
                </div>
                <img :src="src2" alt class="img1" />
              </div>
            </div>
            <div class="show_text">成长明细</div>
            <div class="show_onebox" v-for="(itema,indexa) in growthDetail" :key="indexa">
              <div class="show_onebox_one">
                <p class="txt1">{{itema.description}}</p>
                <p class="txt2">
                  <text>+{{itema.pointValue}}</text>成长值
                </p>
              </div>
              <div class="show_onebox_two">
                <p class="txt1">{{itema.remark}}</p>
                <p class="txt2">{{itema.createTime}}</p>
              </div>
            </div>
            <!-- <div class="show_onebox">
              <div class="show_onebox_one">
                <p class="txt1">星级到期扣除成长值</p>
                <p class="txt2"><text>-960</text>成长值</p>
              </div>
              <div class="show_onebox_two">
                <p class="txt1"></p>
                <p class="txt2">2019-10-23 01:57:30</p>
              </div>
            </div>-->
          </div>
        </van-tab>
      </van-tabs>
    </div>
  </div>
</template>
<script>
import { GetMemberGrade } from '../../api'
export default {
  data() {
    return {
      active: 0,
      progress: 20,
      windowHeight: '',
      member: '铂金会员', //当前等级
      nextMemberGrade: '', //下一等级
      currentGrowthValue: '', //当前成长值
      memberarr: [
        {
          src: `${this.globalData.imgPubUrl}minevip_vip_zero.png`,
          text: '注册会员'
        },
        {
          src: `${this.globalData.imgPubUrl}minevip_vip_one.png`,
          text: '铜牌'
        },
        {
          src: `${this.globalData.imgPubUrl}minevip_vip_two.png`,
          text: '银牌'
        },
        {
          src: `${this.globalData.imgPubUrl}minevip_vip_three.png`,
          text: '金牌'
        },
        {
          src: `${this.globalData.imgPubUrl}minevip_vip_four.png`,
          text: '铂金'
        },
        {
          src: `${this.globalData.imgPubUrl}minevip_vip_five.png`,
          text: '钻石'
        }
      ],
      memberGrades: [], //成长等级
      growthDetail: [], //成长明细
      src: `${this.globalData.imgPubUrl}minevip_bigvip_five.png`,
      src1: `${this.globalData.imgPubUrl}minevip_vip_zero.png`,
      src3: `${this.globalData.imgPubUrl}minevip_vip_one.png`,
      src4: `${this.globalData.imgPubUrl}minevip_vip_two.png`,
      src5: `${this.globalData.imgPubUrl}minevip_vip_three.png`,
      src6: `${this.globalData.imgPubUrl}minevip_vip_four.png`,
      src7: `${this.globalData.imgPubUrl}minevip_vip_five.png`,
      src2: `${this.globalData.imgPubUrl}minevip_value_icon.png`
    }
  },
  onLoad() {
    let that = this
    // 获取系统信息
    wx.getSystemInfo({
      success: function(res) {
        // 获取可使用窗口宽度
        let clientHeight = res.windowHeight
        // 获取可使用窗口高度
        let clientWidth = res.windowWidth
        // 算出比例
        let ratio = 750 / clientWidth
        // 算出高度(单位rpx)
        that.windowHeight = clientHeight * ratio - 88 + 'rpx'
      }
    })
  },
  mounted() {
    let that = this
    GetMemberGrade('')
      .then(res => {
        that.member = res.data.displayName
        that.nextMemberGrade = res.data.nextMemberGrade
        that.currentGrowthValue = res.data.currentGrowthValue
        that.memberGrades = res.data.memberGrades
        that.growthDetail = res.data.growthDetail
      })
      .catch(err => {})
  }
}
</script>
<style scoped lang="scss">
.paddi {
  padding-bottom: 30rpx;
  box-sizing: border-box;
}
.demo_page {
  width: 100%;
  // height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  background: #f3f3f3;
}
.title_tab {
  width: 100%;
  display: flex;
  flex-direction: column;
  .box {
    position: relative;
    width: 100%;
    height: 519rpx;
    padding: 109rpx 30rpx 30rpx 30rpx;
    box-sizing: border-box;
    background: url(https://m.aerp.com.cn/mini-app-main/bg1.png) no-repeat;
    background-size: cover;
    .box_one {
      width: 100%;
      height: 410rpx;
      background: rgba(255, 255, 255, 1);
      border-radius: 10rpx;
      .top {
        width: 100%;
        padding-top: 84rpx;
        box-sizing: border-box;
        .p1 {
          width: 100%;
          text-align: center;
          font-size: 28rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(51, 51, 51, 1);
        }
        .p2 {
          width: 100%;
          text-align: center;
          font-size: 24rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(173, 173, 173, 1);
          margin-top: 22rpx;
        }
      }
      .bottom {
        width: 100%;
        padding: 0 28rpx;
        box-sizing: border-box;
        .num {
          width: 100%;
          display: flex;
          flex-direction: row;
          justify-content: space-between;
          align-items: center;
          margin-bottom: 15rpx;
          .num_one {
            width: 104rpx;
            text-align: center;
            // box-sizing: border-box;
          }
        }
        .img_text {
          width: 100%;
          display: flex;
          flex-direction: row;
          justify-content: center;
          align-items: center;
          margin-top: 20rpx;
          .member_one {
            flex: 1;
            display: flex;
            flex-direction: column;
            justify-items: center;
            align-items: center;
            .img {
              width: 54rpx;
              height: 54rpx;
            }
            .zhuce {
              font-size: 24rpx;
              font-family: Source Han Sans CN;
              font-weight: 400;
              color: rgba(51, 51, 51, 1);
            }
          }
        }
      }
      .img1 {
        position: absolute;
        top: 53rpx;
        left: 316rpx;
        width: 115rpx;
        height: 115rpx;
      }
    }
  }
  .showbox {
    position: relative;
    width: 100%;
    height: 339rpx;
    padding: 109rpx 30rpx 30rpx 30rpx;
    box-sizing: border-box;
    background: url(https://m.aerp.com.cn/mini-app-main/bg3.png) no-repeat;
    background-size: cover;
    .box_one {
      width: 100%;
      height: 230rpx;
      background: rgba(255, 255, 255, 1);
      border-radius: 10rpx;
      .top {
        width: 100%;
        padding-top: 84rpx;
        box-sizing: border-box;
        .p1 {
          width: 100%;
          text-align: center;
          font-size: 28rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(51, 51, 51, 1);
        }
        .p2 {
          width: 100%;
          text-align: center;
          font-size: 62rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(255, 99, 36, 1);
        }
      }
      .img1 {
        position: absolute;
        top: 53rpx;
        left: 316rpx;
        width: 115rpx;
        height: 115rpx;
      }
    }
  }
  .show_text {
    width: 690rpx;
    height: 90rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 10rpx 10rpx 0rpx 0rpx;
    margin: 0 auto;
    margin-top: 16rpx;
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: bold;
    color: rgba(51, 51, 51, 1);
    line-height: 90rpx;
    padding-left: 28rpx;
    box-sizing: border-box;
    border-bottom: 1rpx solid #f3f3f3;
  }
  .show_onebox {
    width: 690rpx;
    height: 140rpx;
    background: rgba(255, 255, 255, 1);
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: center;
    padding: 28rpx;
    box-sizing: border-box;
    border-bottom: 1rpx solid #f3f3f3;
    margin: 0 auto;
    .show_onebox_one {
      width: 100%;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      .txt1 {
        font-size: 30rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
      }
      .txt2 {
        font-size: 30rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
        text {
          font-size: 30rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(255, 99, 36, 1);
        }
      }
    }
    .show_onebox_two {
      width: 100%;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      p {
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(173, 173, 173, 1);
      }
    }
  }
  .hemember {
    width: 690rpx;
    height: 90rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 10rpx 10rpx 0rpx 0rpx;
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: bold;
    color: rgba(51, 51, 51, 1);
    line-height: 90rpx;
    margin: 16rpx auto 0 auto;
    padding-left: 30rpx;
    box-sizing: border-box;
  }
  .tab {
    width: 690rpx;
    height: 80rpx;
    background: rgba(255, 248, 245, 1);
    margin: 0 auto;
    padding-left: 30rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    align-items: center;
    text {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(255, 99, 36, 1);
    }
    .txt2 {
      margin: 0 67rpx 0 48rpx;
    }
  }
  .one_content {
    width: 690rpx;
    // height:80rpx;
    padding: 10rpx 0;
    background: rgba(255, 255, 255, 1);
    margin: 0 auto;
    padding-left: 50rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    align-items: center;
    border-bottom: 1rpx solid #f3f3f3;
    .txt1 {
      width: 54rpx;
      height: 54rpx;
    }
    .txt2 {
      margin: 0 0 0 70rpx;
      width: 140rpx;
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
    .txt3 {
      width: 360rpx;
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
  }
  .shuoming {
    width: 690rpx;
    background: rgba(255, 255, 255, 1);
    margin: 0 auto;
    padding: 30rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: flex-start;
    margin-bottom: 40rpx;
    p {
      width: 100%;
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(136, 136, 136, 1);
    }
  }
  .get {
    width: 690rpx;
    height: 90rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 10rpx 10rpx 0rpx 0rpx;
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: bold;
    color: rgba(51, 51, 51, 1);
    line-height: 90rpx;
    margin: 16rpx auto 0 auto;
    padding-left: 30rpx;
    box-sizing: border-box;
    border-bottom: 1rpx solid #f3f3f3;
  }
  .footer {
    width: 690rpx;
    background: rgba(255, 255, 255, 1);
    margin: 0 auto;
    padding: 30rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: flex-start;
    margin-bottom: 40rpx;
    .footer_one {
      font-size: 26rpx;
      font-family: Source Han Sans CN;
      font-weight: 500;
      color: rgba(51, 51, 51, 1);
    }
    .footer_two {
      width: 100%;
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: #888888;
    }
    .margin {
      margin-top: 30rpx;
    }
  }
}
</style>