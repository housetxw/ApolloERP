<template>
  <div class="demo_page">
    <div class="title_tab">
      <van-tabs v-model="active" sticky swipeable color="#FF6324" line-width="40" class="tabs">
        <van-tab title="可预约">
          <div class="title_tab_content" :style="{height:windowHeight}">
            <div class="box" v-if="orderarr.length > 0">
              <div class="tab1" v-for="(item1,index1) in orderarr" :key="index1">
                <div class="tab1_title">
                  <text class="txt1">订单编号：{{item1.ordernum}}</text>
                  <text class="txt2">{{item1.status}}</text>
                </div>
                <div class="tab1_contetn">
                  <div class="tab1-images">
                    <img :src="item1.src1" alt class="tab1_img" />
                  </div>
                  <div class="tab1_top">
                    <p class="p1">{{item1.title}}</p>
                    <p class="p2">
                      <text class="txt1">共{{item1.num}}件</text>
                      <text class="txt2">￥{{item1.price}}</text>
                    </p>
                  </div>
                </div>
                <div class="btn">
                  <van-button
                    round
                    size="small"
                    color="#FF6324"
                    type="default"
                    @tap="applyYuyue"
                  >申请预约</van-button>
                </div>
              </div>
            </div>
            <div class="else" v-else :style="{height:windowHeight}">
              <img :src="src2" alt class="elseimg" />
              <p
                style="font-size:32rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(136,136,136,1);"
              >无可预约订单</p>
            </div>
          </div>
        </van-tab>
        <van-tab title="已预约">
          <div
            class="already_appointment"
            :style="{minHeight:windowHeight}"
            v-if="alreadyArr.length > 0"
          >
            <div class="appointment" v-for="(item2,index2) in alreadyArr" :key="index2">
              <div class="box">
                <div class="box_img">
                  <img :src="item2.url" alt class="img1" />
                </div>
                <div class="box_content">
                  <p class="p1">{{item2.title}}</p>
                  <p class="p2">{{item2.conent}}</p>
                </div>
              </div>
              <div class="content">
                <p class="one">
                  <img :src="url1" alt class="img1" />
                  <text class="txt1">预约编号：</text>
                  <text class="txt2">{{item2.num}}</text>
                </p>
                <p class="two">
                  <img :src="url1" alt class="img1" />
                  <text class="txt1">预约编号：</text>
                  <text class="txt2">{{item2.date}}</text>
                </p>
                <p class="three">
                  <img :src="url1" alt class="img1" />
                  <text class="txt1">预约编号：</text>
                  <text class="txt2">{{item2.store}}</text>
                </p>
                <p class="four" v-if="item2.switchdisplay">
                  <img :src="url1" alt class="img1" />
                  <text class="txt1">预约编号：</text>
                  <text class="txt2">{{item2.project}}</text>
                </p>
                <p class="five" v-if="item2.switchdisplay">
                  <img :src="url1" alt class="img1" />
                  <text class="txt1">预约编号：</text>
                  <text class="txt2">{{item2.number}}</text>
                </p>
                <p class="p_three" @tap="openClick(item2,index2)">
                  {{item2.switchdisplay?'收起':'展开'}}
                  <img
                    :src="item2.switchdisplay?'https://m.aerp.com.cn/mini-app-main/maintenance_up_icon.png':'https://m.aerp.com.cn/mini-app-main/maintenance_down_icon.png'"
                    alt
                  />
                </p>
                <div class="btn">
                  <van-button
                    round
                    size="small"
                    plain
                    hairline
                    color="#ADADAD"
                    type="default"
                    @tap="cancelAppo(index2)"
                  >取消预约</van-button>
                  <van-button
                    round
                    size="small"
                    plain
                    hairline
                    color="#FF743C"
                    type="default"
                    @tap="againAppo"
                  >再次预约</van-button>
                </div>
              </div>
            </div>
            <div class="position">
              <van-button round size="large" color="#FF6324" type="default" @tap="newAppo">新增预约</van-button>
            </div>
          </div>
          <div class="else" v-else :style="{height:windowHeight}">
            <img :src="src2" alt class="elseimg" />
            <p
              style="font-size:32rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(136,136,136,1);"
            >无可预约订单</p>
          </div>
        </van-tab>
      </van-tabs>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      active: 0,
      src2: `${this.globalData.imgPubUrl}mine_appointment_null.png`,
      windowHeight: '',
      orderarr: [
        {
          ordernum: 'BY00000001',
          status: '待安装',
          src1: `${this.globalData.imgPubUrl}search_goods_img.png`,
          title: '美孚/Mobil 1号全合成机油 5W- 30SNdv 级4L',
          num: 2,
          price: '489.00'
        },
        {
          ordernum: 'BY00000001',
          status: '已安装',
          src1: `${this.globalData.imgPubUrl}search_goods_img.png`,
          title: '美孚/Mobil 1号全合成机油 5W- 30SNdv 级4L',
          num: 2,
          price: '489.00'
        }
      ],
      alreadyArr: [
        {
          url: `${this.globalData.imgPubUrl}appointment_car_logo.png`,
          title: '北京奔驰-C200L',
          conent: '2018款 2.0T 手自一体 上市特别版',
          num: 13,
          date: '2019-10-10 08:00',
          store: '致大养车-黄山店【迎客松路】',
          project: '大保养，更换轮胎及雨刮器',
          number: 'BY00000001',
          switchdisplay: false
        },
        {
          url: `${this.globalData.imgPubUrl}appointment_car_logo.png`,
          title: '北京奔驰-C200L',
          conent: '2018款 2.0T 手自一体 上市特别版',
          num: 13,
          date: '2019-10-10 08:00',
          store: '致大养车-黄山店【迎客松路】',
          project: '大保养，更换轮胎及雨刮器',
          number: 'BY00000001',
          switchdisplay: false
        },
        {
          url: `${this.globalData.imgPubUrl}appointment_car_logo.png`,
          title: '北京奔驰-C200L',
          conent: '2018款 2.0T 手自一体 上市特别版',
          num: 13,
          date: '2019-10-10 08:00',
          store: '致大养车-黄山店【迎客松路】',
          project: '大保养，更换轮胎及雨刮器',
          number: 'BY00000001',
          switchdisplay: false
        },
        {
          url: `${this.globalData.imgPubUrl}appointment_car_logo.png`,
          title: '北京奔驰-C200L',
          conent: '2018款 2.0T 手自一体 上市特别版',
          num: 13,
          date: '2019-10-10 08:00',
          store: '致大养车-黄山店【迎客松路】',
          project: '大保养，更换轮胎及雨刮器',
          number: 'BY00000001',
          switchdisplay: true
        }
      ],
      url1: `${this.globalData.imgPubUrl}appointment_number_icon.png`
    }
  },
  methods: {
    openClick(item2, index2) {
      item2.switchdisplay = !item2.switchdisplay
    },
    // 申请预约
    applyYuyue() {
      this.$router.push('/pages/newAppointment/main')
    },
    // 取消预约
    cancelAppo(index2) {
      let that = this
      wx.showModal({
        title: '取消预约',
        content: '确定取消已确认预约记录？取消后将无法恢复。',
        success(res) {
          if (res.confirm) {
            that.alreadyArr.splice(index2, 1)
            wx.showToast({
              title: '成功取消',
              icon: 'success',
              duration: 1000
            })
          } else if (res.cancel) {
          }
        }
      })
    },
    // 再次预约
    againAppo() {
      let that = this
      wx.showModal({
        title: '修改预约',
        content: '确定需要修改当前预约时间？致大将重新和您确认预约时间。',
        success(res) {
          if (res.confirm) {
            that.$router.push('/pages/newAppointment/main')
          } else if (res.cancel) {
          }
        }
      })
    },
    // 新增预约
    newAppo() {
      this.$router.push('/pages/newAppointment/main')
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
  }
}
</script>
<style scoped lang="scss">
.already_appointment {
  width: 100%;
  display: flex;
  flex-direction: column;
  padding: 0 30rpx 120rpx 30rpx;
  box-sizing: border-box;
  margin-bottom: 20rpx;
  .appointment {
    width: 100%;
    margin-top: 16rpx;
    display: flex;
    flex-direction: column;
    .box {
      width: 100%;
      height: 120rpx;
      background: linear-gradient(
        90deg,
        rgba(149, 156, 170, 1),
        rgba(100, 109, 130, 1)
      );
      border-radius: 10rpx 10rpx 0rpx 0rpx;
      display: flex;
      flex-direction: row;
      justify-content: flex-start;
      align-items: center;
      padding: 25rpx;
      box-sizing: border-box;
      .box_img {
        width: 80rpx;
        height: 100%;
        .img1 {
          width: 68rpx;
          height: 68rpx;
        }
      }
      .box_content {
        width: 400rpx;
        height: 100%;
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        align-items: flex-start;
        .p1 {
          font-size: 30rpx;
          font-family: SourceHanSansCN;
          font-weight: 400;
          color: rgba(255, 255, 255, 1);
        }
        .p2 {
          font-size: 24rpx;
          font-family: SourceHanSansCN;
          font-weight: 400;
          color: rgba(182, 190, 207, 1);
        }
      }
    }
    .content {
      width: 100%;
      display: flex;
      flex-direction: column;
      padding: 30rpx 0 0 30rpx;
      box-sizing: border-box;
      background: #ffffff;
      .one,
      .two,
      .three,
      .four,
      .five {
        width: 100%;
        height: 60rpx;
        display: flex;
        flex-direction: row;
        justify-content: flex-start;
        align-items: flex-start;
        .img1 {
          width: 28rpx;
          height: 28rpx;
        }
        .txt1 {
          font-size: 26rpx;
          font-family: SourceHanSansCN;
          font-weight: 400;
          color: rgba(136, 136, 136, 1);
          margin-left: 20rpx;
          margin-right: 30rpx;
        }
        .txt2 {
          width: 430rpx;
          font-size: 26rpx;
          font-family: SourceHanSansCN;
          font-weight: 400;
          color: rgba(33, 33, 33, 1);
          overflow: hidden;
          white-space: pre-wrap;
          display: -webkit-box;
          text-overflow: ellipsis;
          -webkit-line-clamp: 2;
          -webkit-box-orient: vertical;
        }
      }
      .three {
        height: 90rpx;
      }
      .p_three {
        width: 100%;
        text-align: center;
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
        margin-top: 20rpx;
        img {
          width: 24rpx;
          height: 24rpx;
        }
      }
      .btn {
        width: 100%;
        height: 100rpx;
        padding-left: 300rpx;
        padding-right: 50rpx;
        box-sizing: border-box;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
        margin-top: 30rpx;
      }
    }
  }
  .position {
    position: fixed;
    bottom: 0;
    left: 0;
    width: 100%;
    background: #ffffff;
    padding: 10rpx 30rpx;
    box-sizing: border-box;
  }
}
.demo_page {
  width: 100%;
  // height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #f3f3f3;
}
.title_tab {
  width: 100%;
  display: flex;
  flex-direction: column;
}
.title_tab_content {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  padding: 16rpx 30rpx 0 30rpx;
  box-sizing: border-box;
}
.else {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.elseimg {
  width: 215rpx;
  height: 215rpx;
}

.tab1 {
  width: 100%;
  height: 360rpx;
  border-radius: 10rpx 10rpx 10rpx 10rpx;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  padding: 30rpx;
  padding-top: 0;
  box-sizing: border-box;
  background: #ffffff;
  margin-bottom: 16rpx;
}
.tab1_title {
  width: 100%;
  height: 90rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.tab1_title .txt1 {
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
}
.tab1_title .txt2 {
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
}
.tab1_contetn {
  width: 100%;
  height: 170rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.tab1-images {
  width: 150rpx;
  height: 150rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: flex-start;
}
.tab1_img {
  width: 102rpx;
  height: 102rpx;
}
.tab1_top {
  width: 500rpx;
}
.tab1_top .p1 {
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}
.tab1_top .p2 {
  width: 500rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  margin-top: 27rpx;
}
.tab1_top .p2 .txt1 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
}
.tab1_top .p2 .txt2 {
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
}
.tab1 .btn {
  width: 100%;
  height: 100rpx;
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  align-items: flex-end;
}
</style>