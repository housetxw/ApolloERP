<template>
  <div class="demo_page">
    <div class="title_tab">
      <van-tabs
        :active="active"
        sticky
        swipeable
        color="#FF6324"
        line-width="40"
        class="tabs"
        @change="onChangeTitle"
      >
        <van-tab title="可预约">
          <div class="already_appointment">
            <div class="appointment" v-if="orderarr.length > 0" style="margin-bottom:10rpx;">
              <div class="tab1" v-for="(item1,index1) in orderarr" :key="index1">
                <div class="tab1_title">
                  <text class="txt1">订单编号：{{item1.orderNo}}</text>
                  <text class="txt2">{{item1.orderStatus}}</text>
                </div>
                <div
                  class="tab1_contetn"
                  v-for="(item3,index3) in item1.reserveProductVOs"
                  :key="index3"
                >
                  <div class="tab1-images">
                    <img :src="item3.imageUrl" alt class="tab1_img" mode="widthFix" />
                  </div>
                  <div class="tab1_top">
                    <p class="p1">{{item3.productName}}</p>
                    <p class="p2">
                      <text class="txt1">共{{item3.number}}件</text>
                      <text class="txt2">￥{{item3.totalAmount}}</text>
                    </p>
                  </div>
                </div>
                <div class="btn">
                  <van-button
                    round
                    size="small"
                    color="#FF6324"
                    type="default"
                    @tap="applyYuyue(item1)"
                  >申请预约</van-button>
                </div>
              </div>
            </div>
            <!-- 无预约订单 -->
            <div class="else" v-else :style="{height:windowHeight}">
              <img :src="src2" alt class="elseimg" />
              <p
                style="font-size:32rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(136,136,136,1);"
              >无可预约订单</p>
            </div>
            <!-- <div class="position">
              <van-button round size="large" color="#FF6324" type="default" @tap="newAppo">新增预约</van-button>
            </div>-->
          </div>
        </van-tab>
        <van-tab title="已预约">
          <div
            class="already_appointment"
            :style="{minHeight:windowHeight1}"
            v-if="alreadyArr.length > 0"
          >
            <div class="appointment" v-for="(item2,index2) in alreadyArr" :key="index2">
              <div class="box">
                <div class="box_img">
                  <img :src="item2.url" alt class="img1" />
                </div>
                <div class="box_content">
                  <p class="p1">{{item2.brand}}</p>
                  <p class="p2">{{item2.salesName}}</p>
                </div>
              </div>
              <div class="content">
                <p class="one">
                  <img :src="url1" alt class="img1" />
                  <text class="txt1">预约编号：</text>
                  <text class="txt2">{{item2.reserveNO}}</text>
                </p>
                <p class="three">
                  <img :src="url3" alt class="img1" />
                  <text class="txt1">预约类型：</text>
                  <text v-if="item2.type == '0'" class="txt2">到店服务</text>
                  <text v-if="item2.type == '1'" class="txt2">上门服务</text>
                </p>
                <p class="two">
                  <img :src="url2" alt class="img1" />
                  <text class="txt1">预约时间：</text>
                  <text class="txt2">{{item2.reserveTime}}</text>
                </p>

                <p class="three" v-if="item2.type==0">
                  <img :src="url3" alt class="img1" />
                  <text class="txt1">预约门店：</text>
                  <text class="txt2">{{item2.shopSimpleName}}</text>
                </p>
                <p class="four" v-if="item2.isDisplay">
                  <img :src="url4" alt class="img1" />
                  <text class="txt1">预约项目：</text>
                  <text class="txt2">{{item2.serviceName}}</text>
                </p>
                <p class="five" v-if="item2.isDisplay">
                  <img :src="url5" alt class="img1" />
                  <text class="txt1">订单编号：</text>
                  <text class="txt2">{{item2.orderNO || '此预约为无订单预约'}}</text>
                </p>
                <p class="p_three" @tap="openClick(item2)">
                  {{item2.isDisplay?'收起':'展开'}}
                  <img
                    :src="item2.isDisplay?'https://m.aerp.com.cn/mini-app-main/maintenance_up_icon.png':'https://m.aerp.com.cn/mini-app-main/maintenance_down_icon.png'"
                    alt
                  />
                </p>
                <div class="btn" v-if="item2.reserveStatus != 'Canceled'">
                  <van-button
                    round
                    size="small"
                    plain
                    hairline
                    color="#ADADAD"
                    type="default"
                    @tap="cancelAppo(item2,index2)"
                  >取消预约</van-button>
                  <van-button
                    round
                    size="small"
                    plain
                    hairline
                    color="#FF743C"
                    type="default"
                    @tap="againAppo(item2,index2)"
                  >修改预约</van-button>
                </div>
              </div>
              <div class="img_text">
                <img
                  :src="yuyueimg1"
                  alt
                  class="yuyueimg"
                  v-if="item2.reserveStatus == 'Unconfirmed'"
                />
                <img
                  :src="yuyueimg2"
                  alt
                  class="yuyueimg"
                  v-if="item2.reserveStatus == 'Confirmed'"
                />
                <img
                  :src="yuyueimg3"
                  alt
                  class="yuyueimg"
                  v-if="item2.reserveStatus == 'Completed'"
                />
                <img :src="yuyueimg4" alt class="yuyueimg" v-if="item2.reserveStatus == 'Canceled'" />
                <text v-if="item2.reserveStatus == 'Unconfirmed'">待确认</text>
                <text v-if="item2.reserveStatus == 'Confirmed'">已确认</text>
                <text v-if="item2.reserveStatus == 'Completed'">已完成</text>
                <text v-if="item2.reserveStatus == 'Canceled'">已取消</text>
              </div>
            </div>
          </div>
          <div class="else" v-else :style="{height:windowHeight}">
            <img :src="src2" alt class="elseimg" />
            <p
              style="font-size:32rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(136,136,136,1);"
            >无已预约订单</p>
          </div>
        </van-tab>
      </van-tabs>
    </div>
    <div class="positionTOP" v-show="showBottom1">{{bmessage1}}</div>
    <div class="positionTOP" v-show="showBottom">{{bmessage}}</div>
    <div style="margin-bottom:10rpx;height:10rpx;width:100%;"></div>
  </div>
</template>
<script>
import { Getkeyuyue, Getyiyuyue, PostDispear } from '../../api'
export default {
  data() {
    return {
      totalItems: '',
      totalItems1: '',
      showBottom1: false,
      bmessage: '',
      bmessage1: '',
      showBottom: false,
      PageIndex: 1,
      PageSize: 10,
      active: 0,
      src2: `${this.globalData.imgPubUrl}mine_appointment_null.png`,
      windowHeight: '',
      windowHeight1: '',
      orderarr: [],
      alreadyArr: [],
      // 已预约图片
      url1: `${this.globalData.imgPubUrl}appointment_number_icon.png`,
      url2: `${this.globalData.imgPubUrl}appointment_time_icon.png`,
      url3: `${this.globalData.imgPubUrl}appointment_store_icon.png`,
      url4: `${this.globalData.imgPubUrl}appointment_project_icon.png`,
      url5: `${this.globalData.imgPubUrl}appointment_order_icon.png`,
      // 预约状态确认图片
      yuyueimg1: `${this.globalData.imgPubUrl}To be confirmed .png`, // 待确认
      yuyueimg2: `${this.globalData.imgPubUrl}Confirmed.png`, // 已确认
      yuyueimg3: `${this.globalData.imgPubUrl}Completed.png`, // 已完成
      yuyueimg4: `${this.globalData.imgPubUrl}Cancelled.png` // 已取消
    }
  },
  onReachBottom() {
    if (this.showBottom == true) {
      return false
    } else {
      let that = this
      that.showBottom1 = true
      that.bmessage1 = '正在加载中...'
      if (that.active == 0) {
        that.reservationsClick()
      } else {
        that.alreadyYuyueClick()
      }
    }
  },
  methods: {
    // 展开跟叠起
    openClick(item2) {
      item2.isDisplay = !item2.isDisplay
    },
    // 申请预约
    applyYuyue(item1) {
      this.$router.push({
        path: '/pages/newAppointment/main',
        query: {
          idnum: '2',
          applynum: '3',
          // reserveId: item1.reserveId,
          // shopId: item1.shopId,
          // nian: item1.nian,
          orderNo: item1.orderNo
        }
      })
    },
    // 取消预约
    cancelAppo(item2, index2) {
      let that = this
      wx.showModal({
        title: '取消预约',
        content: '确定取消已确认预约记录？取消后将无法恢复。',
        success(res) {
          if (res.confirm) {
            let abcdata = {
              reserveId: item2.reserveId,
              cancelReason: 'string'
            }
            PostDispear(abcdata)
              .then(res => {
                if (res.code === 10000) {
                  wx.showToast({
                    title: '成功取消',
                    icon: 'success',
                    duration: 1000
                  })
                  // 重新调用接口，刷新视图，可是没反应，待研究
                  that.alreadyYuyueClick()
                } else {
                  wx.showToast({
                    title: '取消失败',
                    icon: 'none',
                    duration: 1000
                  })
                }
              })
              .catch(err => {})
          } else if (res.cancel) {
          }
        }
      })
    },
    // 修改预约
    againAppo(item2, index2) {
      console.log('item2', item2)
      let that = this
      wx.showModal({
        title: '修改预约',
        content: '确定需要修改当前预约时间？AERP将重新和您确认预约时间。',
        success(res) {
          if (res.confirm) {
            that.$router.push({
              path: '/pages/newAppointment/main',
              query: {
                idnum: '1',
                reserveId: item2.reserveId,
                shopId: item2.shopId,
                nian: item2.nian,
                orderNo: item2.orderNO
              }
            })
          } else if (res.cancel) {
          }
        }
      })
    },
    // 新增预约
    newAppo() {
      this.$router.push({
        path: '/pages/newAppointment/main',
        query: { idnum: '2' }
      })
    },

    // 已预约订单方法
    alreadyYuyueClick() {
      console.log('alreadyYuyueClick')
      console.log(this.PageIndex)
      let that = this
      if (this.showBottom == true) {
        return false
      }
      let rquest = {
        userId: that.globalData.userInfo.userId,
        pageIndex: that.PageIndex,
        pageSize: that.PageSize
      }
      // 获取已预约订单
      Getyiyuyue(rquest)
        .then(ress => {
          // that.totalItems = ress.data.totalItems
          if (that.PageIndex * that.PageSize >= ress.data.totalItems) {
            that.showBottom1 = false
            that.showBottom = true
            that.bmessage = '我也是有底线的哦'
          }

          if (that.PageIndex === 1) {
            that.alreadyArr = ress.data.items
          } else {
            that.alreadyArr = that.alreadyArr.concat(ress.data.items)
            console.log(that.alreadyArr.length)
            console.log('已预约', that.alreadyArr)
          }
          that.PageIndex++
          // if (ress.data.items != null) {

          //   that.alreadyArr = ress.data.items
          // }
        })
        .catch(err => {})
    },

    // 可预约订单方法
    reservationsClick() {
      let that = this
      if (this.showBottom == true) {
        return false
      }
      let data = {
        UserId: that.globalData.userInfo.userId,
        PageIndex: that.PageIndex,
        PageSize: that.PageSize
      }
      // 获取可预约订单
      Getkeyuyue(data)
        .then(res => {
          // if (res.data.items != null) {
          //   let arr = res.data.items
          //   arr.forEach(item => {
          //     item.reserveProductVOs.forEach(res1 => {
          //       res1.totalAmount = res1.totalAmount.toFixed(2)
          //     })
          //   })
          if (that.PageIndex * that.PageSize >= res.data.totalItems) {
            that.showBottom1 = false
            that.showBottom = true
            that.bmessage = '我也是有底线的哦'
          }
          if (that.PageIndex === 1) {
            that.orderarr = res.data.items
          } else {
            that.orderarr = that.orderarr.concat(res.data.items)
            console.log(that.orderarr.length)
          }
          that.PageIndex++
          console.log('可预约', that.orderarr)
          // }
        })
        .catch(err => {})
    },
    // 返回顶部
    backToTop() {
      wx.pageScrollTo({
        scrollTop: 0,
        duration: 400
      })
    },
    // 顶部tabbar切换
    onChangeTitle(event) {
      if (event.mp.detail.index == 0) {
        this.active = 0
        this.PageIndex = 1
        this.showBottom = false
        this.bmessage = ''
        this.bmessage1 = ''
        this.showBottom1 = false
        this.backToTop()
        this.reservationsClick()
      } else if (event.mp.detail.index == 1) {
        console.log(123, event.mp.detail.index)
        this.PageIndex = 1
        this.active = 1
        this.showBottom = false
        this.bmessage = ''
        this.bmessage1 = ''
        this.showBottom1 = false
        this.backToTop()
        this.alreadyYuyueClick()
      }
    }
  },
  onLoad() {
    let that = this
    // that.active = that.$store.state.active
    wx.getSystemInfo({
      success: function(res) {
        let clientHeight = res.windowHeight
        let clientWidth = res.windowWidth
        let ratio = 750 / clientWidth
        that.windowHeight = clientHeight * ratio - 88 + 'rpx'
        that.windowHeight1 = clientHeight * ratio - 134 + 'rpx'
      }
    })
  },
  onShow() {
    let that = this
    // that.alreadyYuyueClick()
    that.active = that.$store.state.active
    if (that.active == 0) {
      that.reservationsClick()
    } else {
      that.alreadyYuyueClick()
    }
    // this.PageIndex = 1
  },
  onUnload() {
    this.active = 0
    this.PageIndex = 1
  },
  onHide() {
    this.PageIndex = 1
  }
}
</script>
<style scoped lang="scss">
.already_appointment {
  width: 100%;
  display: flex;
  flex-direction: column;
  padding: 0 30rpx;
  box-sizing: border-box;
  // margin-bottom: 20rpx;
  .appointment {
    position: relative;
    width: 100%;
    margin-top: 16rpx;
    display: flex;
    flex-direction: column;
    .box {
      width: 100%;
      min-height: 120rpx;
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
      // .three {
      //   height: 90rpx;
      // }
      .p_three {
        width: 100%;
        text-align: center;
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
        margin-top: 20rpx;
        margin-bottom: 30rpx;
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
      }
    }
    .img_text {
      position: absolute;
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      top: 150rpx;
      right: 29rpx;
      .yuyueimg {
        width: 60rpx;
        height: 60rpx;
      }
      text {
        font-size: 16rpx;
        font-family: Adobe Heiti Std;
        font-weight: normal;
        color: rgba(255, 99, 36, 1);
      }
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

  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  padding: 16rpx 30rpx 130rpx 30rpx;
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
  // height: 360rpx;
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
  margin-bottom: 20rpx;
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
  height: 80rpx;
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  align-items: flex-end;
}
.positionTOP {
  width: 100%;
  text-align: center;
  font-size: 30rpx;
  color: #ccc;
  margin-top: 20rpx;
}
</style>