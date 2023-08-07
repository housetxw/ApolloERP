<template>
  <div class="main" :style="{minHeight:windowHeight}">
    <div class="demo_page">
      <div class="top_title">
        <p class="user">{{username}}</p>
        <p class="pass">{{password}}</p>
        <p class="title">车辆信息</p>
      </div>

      <div class="top_city">
        <div class="bgr">
          <div class="top_city_text">
            <div class="top_card">
              <!--<div class="card_tab" @tap="changeCard" v-if="vehicle != ''">-->
              <!--  <img :src="carBrandIcon" alt class="img_card" />-->
              <!--  <div class="card_content">-->
              <!--    <p class="p1">{{vehicle1}}</p>-->
              <!--    <p class="p2">{{carNo}}</p>-->
              <!--  </div>-->
              <!--  <p class="po" @tap="changeCard">换车</p>-->
              <!--</div>-->
              <div class="card_tab" v-if="carArrFirst.vehicle != ''">
                <img :src="carArrFirst.brandUrl" alt class="img_card" />
                <div class="card_content">
                  <p class="p1">{{carArrFirst.vehicle}}</p>
                  <p class="p2">{{carArrFirst.carNumber}}</p>
                </div>
                <p class="po" @tap="changeCard">换车</p>
              </div>

              <div class="card_add" @tap="changeCard" v-else>请选择车辆</div>
            </div>
          </div>
          <div style="margin:25rpx 0rpx">排队门店: {{shopName}}</div>
          <div class="service">
            <p class="top_city_title">
              服务类型&nbsp;
              <span style="color:#999;font-size:22rpx;font-weight:400;">(必填)</span>
            </p>
            <div class="group" style="display: flex;flex-direction: row;flex-wrap: wrap;">
              <van-radio
                :value="radio"
                :name="item.value"
                @change="changeRadio"
                v-for="(item,index) in maintainarr"
                :key="index"
                checked-color="#FF6324"
                style="margin:16rpx 0 0 24rpx;"
              >{{item.value}}</van-radio>
            </div>
          </div>
          <div class="top_resion">
            <p class="top_resion_title">备注</p>
            <div class="box">
              <!--           <input type="textarea"  @input="bindTextAreaBlur" :value="textvalue" placeholder-style="color:rgba(102,102,102,1);font-size:28rpx;font-weight:400;margin-top:0rpc" class="top_input"  placeholder="是否现有店面等信息"  />-->
              <textarea
                @input="bindTextAreaBlur"
                :value="textvalue"
                placeholder-style="color:rgba(102,102,102,1);font:400 24rpx/60rpx 'SourceHanSansCN-Regular';"
                class="top_input"
                auto-height
                placeholder="可输入服务名称，例如：深度保养、基础保养、四轮定位、洗车等，100字以内。"
              />
            </div>
          </div>
          <div class="fixed">
            <div class="btn" @tap="send()">快速拿号</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import {
  QuickQueue,
  QuickTakeNumberLayer,
  QuickTakeNumber,
  ReserveTakeNumber,
  GetWxacodeScene
} from '../../api'
export default {
  data() {
    return {
      carText: '请选择车辆',
      shopid: 7,
      storeArr: {
        simpleName: '', // 门店名称
        address: '', // 门第地址
        img: '' // 门店图片
      }, // 门店信息
      maintainarr: [],
      show1: true,
      windowHeight: '',
      username: '',
      password: '',
      textvalue: '',
      show: false,
      shopList: [],
      vehicle: [],
      radio: '',
      carBrandIcon: '',
      vehicle1: '',
      carNo: '',
      showNum: 0,
      shopName: ''
    }
  },
  computed: {
    carArrFirst() {
      return this.$store.state.carArr[0] || {}
    },
    // 车辆id
    cardID() {
      if (this.$store.state.carArr[0]) {
        return this.$store.state.carArr[0].carId
      } else {
        return ''
      }
    }
  },
  methods: {
    showPopup() {
      this.show = true
      this.show1 = false
    },
    cancle() {
      this.show = false
      this.show1 = true
    },
    changeRadio(e) {
      console.log(23, e.mp.detail)
      this.radio = e.mp.detail
      console.log(235, this.radio)
    },

    onChange(event) {
      this.setData({
        checked: event.detail
      })
    },
    changeCard() {
      this.$router.push('/pages/myCard/main')
    },

    // 用户昵称
    completeInputnumber(event) {
      this.username = event.mp.detail
    },
    // 手机号
    completeInputduanxin(event) {
      this.password = event.mp.detail
    },
    // 备注
    bindTextAreaBlur(e) {
      this.textvalue = e.mp.detail.value
    },
    send() {
      let that = this
      console.log(256, that.carArrFirst.vehicle)
      if (that.carArrFirst.vehicle == '') {
        wx.showToast({
          title: '请添加车型',
          icon: 'none'
        })
      } else if (that.radio == '') {
        wx.showToast({
          title: '请选择服务类型',
          icon: 'none'
        })
      } else {
        let adddata = {
          vechicle: {
            brand: that.carArrFirst.brand,
            carBrandIcon: that.carArrFirst.brandUrl,
            carNo: that.carArrFirst.carNumber,
            vehicle: that.carArrFirst.vehicle,
            carId: that.carArrFirst.carId
          },
          shopid: that.shopid,
          serviceType: that.radio,
          remark: that.textvalue
        }

        // 获取排队号码
        QuickTakeNumberLayer(adddata)
          .then(res => {
            if (res.code == 10000) {
              let reserverNo = res.data.content
              console.log('reserverNo', reserverNo)
              let quedata = {
                vechicle: {
                  brand: that.carArrFirst.brand,
                  carBrandIcon: that.carArrFirst.brandUrl,
                  carNo: that.carArrFirst.carNumber,
                  vehicle: that.carArrFirst.vehicle,
                  carId: that.carArrFirst.carId
                },
                reserverNo: reserverNo,
                shopid: that.shopid,
                serviceType: that.radio,
                remark: that.textvalue
              }
              if (res.data.quickTakeNumberType == 0) {
                wx.showModal({
                  title: '',
                  content: '根据预约记录【' + reserverNo + '】号排队？',
                  confirmText: '是',
                  cancelText: '否',
                  success(res) {
                    if (res.confirm) {
                      ReserveTakeNumber(quedata)
                        .then(res => {
                          if (res.code == 10000) {
                            wx.showModal({
                              title: '',
                              content: res.data.content,
                              showCancel: false,
                              success(res) {
                                if (res.confirm) {
                                  that.$router.push('/pages/serverRecords/main')
                                  console.log('用户点击确定跳转')
                                } else if (res.cancel) {
                                }
                              }
                            })
                          }
                        })
                        .catch(err => {})
                      console.log('用户点击确定')
                    } else if (res.cancel) {
                      console.log('用户点击取消')
                      QuickTakeNumber(adddata)
                        .then(res => {
                          if (res.code == 10000) {
                            wx.showModal({
                              title: '',
                              content: res.data.content,
                              showCancel: false,
                              success(res) {
                                if (res.confirm) {
                                  that.$router.push('/pages/serverRecords/main')
                                  console.log('用户点击确定跳转')
                                } else if (res.cancel) {
                                  console.log('用户点击取消')
                                }
                              }
                            })
                          }
                        })
                        .catch(err => {})
                    }
                  }
                })
              } else {
                wx.showModal({
                  title: '',
                  content: res.data.content,
                  showCancel: false,
                  success(res) {
                    if (res.confirm) {
                      that.$router.push('/pages/serverRecords/main')
                      console.log('用户点击确定')
                    } else if (res.cancel) {
                      console.log('用户点击取消')
                    }
                  }
                })
              }
            }
          })
          .catch(err => {})
      }
    }
  },
  onLoad(query) {
    this.SceneId = query.scene
  },
  onShow() {
    let that = this
    GetWxacodeScene({ SceneId: this.SceneId })
      .then(res => {
        if (res.code === 10002) {
          wx.showModal({
            title: '提示',
            content: '扫码解析错误将为您定位到附近门店',
            showCancel: false,
            success(res) {
              if (res.confirm) {
                that.$router.push({ path: '/pages/index/main', isTab: true })
              }
            }
          })
          return false
        }
        that.shopid = res.data.shopId
        QuickQueue({ shopid: that.shopid })
          .then(res => {
            that.shopList = res.data
            that.username = that.shopList.userName
            // if(that.shopList.userName==''){
            //   that.username=that.shopList.nickName
            // }else{
            //   that.username=that.shopList.userName
            // }
            that.shopName = res.data.shopName
            that.password = that.shopList.telephone
            that.vehicle = that.shopList.vehicle
            that.maintainarr = that.shopList.serviceTypeSelected
            // that.carBrandIcon=that.shopList.vehicle.carBrandIcon
            //   that.vehicle1=that.shopList.vehicle.vehicle
            //   that.carNo=that.shopList.vehicle.carNo
            // console.log(23,that.vehicle.carNo)
          })
          .catch(err => {})
        that.$store.dispatch('getCarArr')
      })
      .catch(() => {
        wx.showModal({
          title: '提示',
          content: '扫码解析错误将为您定位到附近门店',
          showCancel: false,
          success(res) {
            if (res.confirm) {
              that.$router.push({ path: '/pages/index/main', isTab: true })
            }
          }
        })
      })
    // if (this.showNum > 1) {
    //   QuickQueue({ shopid: this.shopid })
    //     .then(res => {
    //       this.shopList = res.data
    //       this.username = this.shopList.userName
    //       // if(that.shopList.userName==''){
    //       //   that.username=that.shopList.nickName
    //       // }else{
    //       //   that.username=that.shopList.userName
    //       // }
    //       this.password = this.shopList.telephone
    //       this.vehicle = this.shopList.vehicle
    //       this.maintainarr = this.shopList.serviceTypeSelected
    //       // that.carBrandIcon=that.shopList.vehicle.carBrandIcon
    //       //   that.vehicle1=that.shopList.vehicle.vehicle
    //       //   that.carNo=that.shopList.vehicle.carNo
    //       // console.log(23,that.vehicle.carNo)
    //     })
    //     .catch(err => {})
    //   this.$store.dispatch('getCarArr')
    // }
  },
  onUnload() {}
}
</script>

<style scoped lang="scss">
.main {
  background: #f3f3f3;
  height: 100%;
}

.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #fff;

  z-index: 1;
}

.top_title {
  width: 100%;
  height: 255rpx;
  background: rgba(250, 121, 69, 1);
  padding: 30rpx 30rpx 0 32rpx;
  box-sizing: border-box;
  /*display: flex;*/
  /*flex-direction: column;*/
  /*justify-content: space-between;*/
  /*align-items: center;*/

  color: #fff;

  .user {
    height: 52rpx;
    font: 32rpx/52rpx 'SourceHanSansCN-Medium';
    padding: 0 25rpx;
  }

  .pass {
    height: 42rpx;
    font: 28rpx/42rpx 'SourceHanSansCN-Regular';
    padding: 0 30rpx;
  }
}

.title {
  width: 685rpx;
  height: 87rpx;
  background: #fff;
  border-radius: 14rpx 14rpx 0rpx 0rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  padding: 33rpx 94rpx 28rpx 20rpx;
  box-sizing: border-box;
  color: #333;
  margin-top: 50rpx;
  font-weight: bold;
  font-family: SourceHanSansCN-Bold;
}

.top_city {
  width: 100%;
  padding: 0 94rpx;

  background: #f3f3f3;
}

.top_city_title {
  width: 100%;
  height: 91rpx;
  font: 700 30rpx/87rpx 'SourceHanSansCN-Bold';
  color: #333;
  margin-left: 20rpx;
}

.top_resion {
  width: 100%;
  padding: 0 30rpx 30rpx 20rpx;
  box-sizing: border-box;
  background: #ffffff;

  .top_resion_title {
    width: 100%;
    height: 91rpx;
    line-height: 87rpx;
    font-size: 28rpx;
    font-family: SourceHanSansCN-Medium;
    color: #333;
  }

  .box {
    width: 100%;
    height: 282rpx;
    background: rgba(243, 243, 243, 1);
    border-radius: 10rpx;

    .top_input {
      width: 100%;
      min-height: 280rpx;
      padding: 30rpx 20rpx;
      box-sizing: border-box;
      text-align: left;

      background: #f3f3f3;
    }
  }
}

.service {
  width: 100%;
  margin-top: 18rpx;
  background: #fff;
}

.bgr {
  background: #f3f3f3;
  padding: 0 34rpx;
  width: 100%;
  box-sizing: border-box;
}

.fixed {
  width: 100%;
  position: fixed;
  bottom: 0;
  left: 0;
  right: 0;
  background: #fff;
  height: 100rpx;
}

.btn {
  position: fixed;
  bottom: 5rpx;
  left: 58rpx;
  width: 635rpx;
  height: 90rpx;
  background: #ff6324;
  border-radius: 25rpx;
  color: #ffffff;
  text-align: center;
  line-height: 90rpx;
}

.top_card {
  // height: 320rpx;
  display: flex;
  flex-direction: column;
  // justify-content: center;
  align-items: center;
  background: #ffffff;
}

.card_tab {
  height: 160rpx;
  background: #fff;
  border-radius: 10rpx;
  padding: 20rpx 28rpx 25rpx 28rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  border-bottom: 1rpx solid #f3f3f3;
}

// margin-top: 16rpx;
.img_card {
  width: 100rpx;
  height: 100rpx;
  margin-right: 16rpx;
}

.card_content {
  width: 346rpx;
  height: 125rpx;
  padding: 20rpx 0;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
}

.p1 {
  width: 100%;
  height: 80rpx;
  font-size: 28rpx;
  font-family: SourceHanSansCN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
}

.p2 {
  width: 100%;
  height: 50rpx;
  font-size: 24rpx;
  font-family: SourceHanSansCN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
}

.card_add {
  width: 100%;
  height: 100rpx;
  border: 2rpx solid #f3f3f3;
  line-height: 96rpx;
  background: rgba(255, 255, 255, 1);
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
  text-align: center;
}

.po {
  width: 140rpx;
  height: 110rpx;
  padding-top: 8rpx;
  border-left: 2rpx solid #eee;
  text-align: center;
  font: 32rpx/110rpx '';
  color: #666;
}

> .van-radio__label {
  color: #666;
}
</style>
