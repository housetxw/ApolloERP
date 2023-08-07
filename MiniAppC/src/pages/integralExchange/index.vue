<template>
  <div class="demo_page" :style="{minHeight:windowHeight}">
    <!-- 致大优惠券 -->
    <div class="top">
      <p class="p1">{{couponTitle}}</p>
      <div class="box" v-for="(items,indexs) in couponArr" :key="indexs">
        <div class="box_one">
          <div class="bg">
            <p class="p_txt1">{{items.needIntegralNum}}积分</p>
            <p class="p_txt2">{{items.useRuleDesc}}</p>
            <p class="p_txt3">有效期：{{items.startTime}}-{{items.endTime}}</p>
          </div>
          <div class="money">
            <p class="pp">
              <text class="txt1">￥</text><text class="txt2">{{items.value}}</text><text class="txt3">券</text>
            </p>
          </div>
          <div class="exchange" @tap="redeemRedE(indexs,items)">兑换</div>
        </div>
      </div>
    </div>
    <!-- 其他优惠券 -->
    <!-- <div class="bottom" v-for="(son,idx) in ortherCouponArr" :key="idx">
      <p class="p1">{{son.title}}</p>
      <div class="ortherCupon" v-for="(son1,idx1) in son.sonArr" :key="idx1">
        <div class="orther_one">
          <div class="onebox">
            <p class="other_txt"><text class="txt1">￥</text><text class="txt2">{{son1.money}}</text><text class="txt3">券</text></p>
            <div class="other_content">
              <p class="orther_p1">{{son1.integral}}</p>
              <p class="orther_p2">有效期：{{son1.date}}</p>
            </div>
          </div>
          <div class="exchangeelse" @tap="redeemOrther(idx1)">兑换</div>
        </div>
        <p class="explain">{{son1.text}}</p>
      </div>
    </div> -->
  </div>
</template>
<script>
import {GetActive, PostCouponActivityId} from '../../api'
export default {
  data () {
    return {
      couponArr: [], // 致大优惠券数组
      couponTitle: '', // 优惠券标题
      windowHeight: '',
      // ortherCouponArr:[
      //   {
      //     title:'辛纳瑞咖啡通用代金券',
      //     sonArr:[
      //       {
      //         integral:'2000积分',
      //         text:'全场通用，优惠券说明，全部 展示支持折行',
      //         date:'2019/09/01-10/30',
      //         money:'20'
      //       },
      //       {
      //         integral:'2000积分',
      //         text:'全场通用，优惠券说明，全部 展示支持折行',
      //         date:'2019/09/01-10/30',
      //         money:'20'
      //       },
      //       {
      //         integral:'2000积分',
      //         text:'全场通用，优惠券说明，全部 展示支持折行',
      //         date:'2019/09/01-10/30',
      //         money:'20'
      //       }
      //     ]
      //   }
      // ],
      userpoint: ''// 上个页面传过来的用户积分
    }
  },
  methods: {
    // 致大兑换
    redeemRedE (indexs, items) {
      let that = this
      // 根据userId和couponActivityId，积分兑换优惠券
      let cupondata = {
        'userId': that.globalData.userInfo.userId,
        'couponActivityId': items.couponActivityId
      }
      PostCouponActivityId(cupondata).then(res => {
        if (res.data) {
          wx.showToast({
            title: res.message,
            icon: 'none',
            duration: 2000
          })
        } else {
          wx.showToast({
            title: res.message,
            icon: 'none',
            duration: 2000
          })
        }
      }).catch(err => {

      })
    }
    // 其他兑换
    // redeemOrther(idx1){

    // }
  },
  mounted () {
    let that = this
    // 根据优惠券活动渠道，获取积分可兑换优惠券列表
    let cupondata = {
      PageIndex: 1,
      PageSize: 1
    }
    GetActive(cupondata).then(res => {
      that.couponArr = res.data.items
      that.couponTitle = res.data.items[0].category
    }).catch(err => {

    })
  },
  onLoad (options) {
    this.userpoint = options.userpoint
    let that = this
    // 获取系统信息
    wx.getSystemInfo({
      success: function (res) {
        // 获取可使用窗口宽度
        let clientHeight = res.windowHeight
        // 获取可使用窗口高度
        let clientWidth = res.windowWidth
        // 算出比例
        let ratio = 750 / clientWidth
        // 算出高度(单位rpx)
        that.windowHeight = clientHeight * ratio + 'rpx'
      }
    })
  }
}
</script>
<style scoped lang="scss">
.demo_page{
  width: 100%;
  // height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #F3F3F3;
}
.top{
  width:690rpx;
  // height:540rpx;
  background:rgba(255,255,255,1);
  border-radius:10rpx;
  margin-top: 16rpx;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  padding: 30rpx;
  padding-top: 0;
  box-sizing: border-box;
  .p1{
    width: 100%;
    height:90rpx;
    line-height: 90rpx;
    background:rgba(255,255,255,1);
    font-size:32rpx;
    font-family:Source Han Sans CN;
    font-weight:bold;
    color:rgba(51,51,51,1);
    border-bottom: 1rpx solid #f3f3f3;
    margin-bottom: 25rpx;
  }
  .box{
    width:690rpx;
    // height:430rpx;
    background:rgba(255,255,255,1);
    // padding: 25rpx 18rpx;
    padding: 0 18rpx;
    margin-bottom: 25rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: column;
    .box_one{
      position: relative;
      width:100%;
      // height:183rpx;
      padding-left:30rpx;
      box-sizing:border-box;
      .bg{
        width:100%;
        height:183rpx;
        background:rgba(255,99,36,1);
        padding:16rpx 0 16rpx 150rpx;
        box-sizing: border-box;
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        align-items: flex-start;
        .p_txt1{
          width: 310rpx;
          font-size:30rpx;
          font-family:Source Han Sans CN;
          font-weight:bold;
          color:rgba(255,255,255,1);
        }
        .p_txt2{
          width: 320rpx;
          font-size:24rpx;
          font-family:Source Han Sans CN;
          font-weight:400;
          color:rgba(255,203,182,1);
          overflow:hidden;
          white-space:pre-wrap;
          display: -webkit-box;
          text-overflow:ellipsis;
          -webkit-line-clamp: 2;
          -webkit-box-orient: vertical;
        }
        .p_txt3{
          width: 400rpx;
          font-size:24rpx;
          font-family:Source Han Sans CN;
          font-weight:400;
          color:rgba(255,167,132,1);
        }
      }
      .money{
        position:absolute;
        top: 8rpx;
        left: 0;
        // border: 1rpx solid yellow;
        width: 158rpx;
        height: 167rpx;
        background:rgba(255,255,255,1);
        border-radius:10rpx 0px 0px 10rpx;
        text-align: center;
        line-height: 167rpx;
        box-shadow:0rpx 10rpx 24rpx 0rpx rgba(255,99,36,0.5);
        .pp{
          width: 100%;
          .txt1{
            font-size:26rpx;
            font-family:Source Han Sans CN;
            font-weight:400;
            color:rgba(255,99,36,1);
          }
          .txt2{
            font-size:48rpx;
            font-family:Bebas Neue;
            font-weight:bold;
            color:rgba(255,99,36,1);
          }
          .txt3{
            font-size:26rpx;
            font-family:Source Han Sans CN;
            font-weight:400;
            color:rgba(255,99,36,1);
          }
        }
      }
      .exchange{
        position:absolute;
        right: 0;
        top: 21rpx;
        width:115rpx;
        height:50rpx;
        background:linear-gradient(90deg,rgba(255,206,116,1),rgba(255,162,53,1));
        border-radius:25rpx 0rpx 0rpx 25rpx;
        text-align: center;
        line-height: 50rpx;
        font-size:26rpx;
        font-family:Source Han Sans CN;
        font-weight:400;
        color:rgba(255,255,255,1);
      }
    }
  }
}
.bottom{
  width:690rpx;
  background:rgba(255,255,255,1);
  border-radius:10rpx;
  margin-top: 16rpx;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  padding: 30rpx;
  padding-top: 0;
  box-sizing: border-box;
  margin-bottom: 16rpx;
  .p1{
    width: 100%;
    height:90rpx;
    line-height: 90rpx;
    background:rgba(255,255,255,1);
    font-size:32rpx;
    font-family:Source Han Sans CN;
    font-weight:bold;
    color:rgba(51,51,51,1);
    border-bottom: 1rpx solid #f3f3f3;
    margin-bottom: 25rpx;
  }
  .ortherCupon{
    width: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    margin-bottom: 16rpx;
    .orther_one{
      position: relative;
      width: 100%;
      height:124rpx;
      background:rgba(255,99,36,1);
      border-radius:10rpx 10rpx 0 0;
      padding: 12rpx 11rpx 0 11rpx;
      box-sizing: border-box;
      .onebox{
        width: 100%;
        height:113rpx;
        background:rgba(255,255,255,1);
        border-radius:10rpx 10rpx 0rpx 0rpx;
        display: flex;
        flex-direction: row;
        justify-content: flex-start;
        align-items: center;
        .other_txt{
          width: 150rpx;
          height: 100%;
          text-align: center;
          line-height: 113rpx;
          .txt1{
            font-size:26rpx;
            font-family:Source Han Sans CN;
            font-weight:400;
            color:rgba(255,99,36,1);
          }
          .txt2{
            font-size:72rpx;
            font-family:Bebas Neue;
            font-weight:bold;
            color:rgba(255,99,36,1);
          }
          .txt3{
            font-size:26rpx;
            font-family:Source Han Sans CN;
            font-weight:400;
            color:rgba(255,99,36,1);
          }
        }
        .other_content{
          // width: 300rpx;
          height: 100%;
          margin-left: 40rpx;
          display: flex;
          flex-direction: column;
          justify-content: space-between;
          align-items: flex-start;
          padding: 15rpx 0;
          box-sizing: border-box;
          .orther_p1{
            font-size:30rpx;
            font-family:Source Han Sans CN;
            font-weight:bold;
            color:rgba(51,51,51,1);
          }
          .orther_p2{
            font-size:24rpx;
            font-family:Source Han Sans CN;
            font-weight:400;
            color:rgba(173,173,173,1);
          }
        }
      }
      .exchangeelse{
        position:absolute;
        right: 0;
        top: 30rpx;
        width:115rpx;
        height:50rpx;
        background:linear-gradient(90deg,rgba(255,206,116,1),rgba(255,162,53,1));
        border-radius:25rpx 0rpx 0rpx 25rpx;
        text-align: center;
        line-height: 50rpx;
        font-size:26rpx;
        font-family:Source Han Sans CN;
        font-weight:400;
        color:rgba(255,255,255,1);
      }
    }
    .explain{
      width: 100%;
      height: 71rpx;
      font-size:24rpx;
      font-family:Source Han Sans CN;
      font-weight:400;
      color:rgba(255,203,182,1);
      background: #FF8453;
      border-radius: 0 0 10rpx 10rpx;
      padding-left: 10rpx;
      box-sizing: border-box;
      line-height: 71rpx;
      
    }
  }
}
</style>