<template>
  <div class="demo_page" :style="{minHeight:windowHeight}">
    <div
      style="width: 100%;display: flex;flex-direction: column;justify-content: center;align-items: center;"
    >
      <div class="topDes">
        <p class="carTitle" @click="toSelectCar">{{carInfo.vehicle || '请先选择预约车辆'}}</p>
        <p class="shopTitle">{{simpleName}}</p>
      </div>
      <!-- 订单信息  门店信息 车信息 -->
      <!-- 订单信息 -->
      <div class="appointment_have">
        <div class="tab1" v-for="(item1,index1) in productList" :key="index1">
          <!-- 订单编号 -->
          <div class="tab1_title">
            <text class="txt1">订单编号：{{item1.orderNo}}</text>
            <text class="txt2">{{item1.orderStatus}}</text>
          </div>
          <!-- 套餐 -->
          <div v-for="(item2,index2) in item1.reserveProductVOs" :key="index2">
            <!-- 商品图片 -->
            <div class="top_two">
              <div class="scroll">
                <div class="top_two_box">
                  <img :src="item2.imageUrl" alt mode="widthFix" class="img" />
                </div>
              </div>
              <div class="tab1_top">
                <p class="p1">{{item2.productName}}</p>
                <p class="p2">
                  <text class="txt1">共{{item2.number}}件</text>
                  <text class="txt2">￥{{item2.totalAmount}}</text>
                </p>
              </div>
            </div>
          </div>
        </div>
        <my-Calender
          :disabled="disabled"
          :reserveExplain="reserveExplain"
          :tileContent="tileContent"
          @childEvent="getReserveTime"
        ></my-Calender>
      </div>
      <!-- 礼拜跟时间 -->

      <div class="top_date">
        <div class="bottom_date">
          <div
            :class="['one_before',{'activeOne':item1.isHighLight?true:false}]"
            v-for="(item1,index1) in datearr"
            :key="index1"
            @tap="dateClick(item1)"
          >
            {{item1.reserveTime}}
            <img :src="posimg" alt class="posimg" v-if="item1.isChecked" />
          </div>
          <div class="call" @tap="telClick">
            <img
              src="https://m.aerp.com.cn/mini-app-main/tel2.png"
              alt
              style="width:50rpx;height:48rpx; margin-right:6rpx; "
            />
            当日急修
          </div>
        </div>
      </div>
      <!-- 预约项目 -->
      <div class="top_yuyue" v-if="orderBumber == ''">
        <p class="txt">预约项目</p>
        <div class="checkbox">
          <van-checkbox
            v-model="main.isCheck"
            checked-color="rgba(255,99,36,1)"
            @tap="checkClik(main,mark)"
            v-for="(main,mark) in maintainarr"
            :key="mark"
            style="margin-top:10rpx;"
          >{{main.name}}</van-checkbox>
        </div>
      </div>
      <!-- 预约说明 -->
      <!-- <div class="appointment_instruction">
        <p class="txt">预约说明</p>
        <p class="txt_wenzi">{{appointmentInstruction}}</p>
      </div>-->
      <!-- 按钮 -->
      <!-- <div class="btn">
        <van-button
          round
          size="large"
          :color="a == 1 && bb == true?'#FF6324':'#E8E8E8'"
          type="default"
          @tap="sureDate"
        >确认到店时间</van-button>
      </div>
      </div>-->
      <div style="margin-bottom:100rpx;width:100%"></div>
      <div class="btn">
        <van-button round size="large" :color="color" type="default" @tap="sureDate">提交</van-button>
      </div>
    </div>
  </div>
</template>
<script>
import {
  GetYuyueStart,
  PostNewAdd,
  PostNearbyStore,
  GetShopReserveSurplus,
  CanReserveTimeV3,
  GetReserveInfoV3,
  AddShopReserveV3,
  ModifyReserveTime
} from '../../api'
import myCalender from '../../components/myCalender.vue'
import eventBus from '../../utils/eventBus.js'
export default {
  components: {
    myCalender
  },
  data() {
    return {
      weekarr: [], // 礼拜数组
      datearr: [], // 时间数组
      appointmentInstruction: '', // 预约说明内容
      maintainarr: [], // 服务项目数组
      // color: '#E8E8E8',
      reserveProductVOs: [], // 订单信息的数组
      reserveOrder: {}, // 订单信息
      posimg: `${this.globalData.imgPubUrl}add_appointment_click.png`, // 时间确认小对勾
      applynum: '', // 申请预约过来的参数
      windowHeight: '',
      url: `${this.globalData.imgPubUrl}mine_carlogo_img.png`, // 奥迪车标
      url1: `${this.globalData.imgPubUrl}add_appointment_tel.png`, // 电话图片
      leftimg: `${this.globalData.imgPubUrl}maintenance_jump_icon.png`, // 箭头
      a: 0, // 确认到店的变量
      bb: false,
      identification_code: '', // 识别码   根据此识别码来判断是点击新增按钮进来的还是点击修改预约进来的页面
      // 车信息
      carInfo: {
        carNO: '', // 车牌号
        carLogo: '', // 车logo
        brand: '', // 品牌
        vehicle: '' // 车系
      },
      // 门店信息
      shopInfo: {
        simpleName: '', // 门店名称
        address: '', // 门店地址
        img: '', // 门店图片
        shopServiceItems: [] // 门店服务项目
      },
      shopId: '', // 门店id
      reserveId: '', // 修改预约传过来的预约id
      nian: '', // 点击时间的nian
      reserveDate: '', // 修改预约需要的日期
      reserveTime: '', // 修改预约需要的时间
      serviceName: '', // 服务大类名称
      serviceCode: '', // 服务code
      orderBumber: '', // 从上个页面传过来的订单编号，以此判断车和门店信息是否可以修改
      orderNo: '', // 订单编号
      reserveExplain: '',
      disabled: [],
      begin: [],
      end: [],
      tileContent: [],
      promotionId: '',
      carId: '',
      telephone: '', // 门店电话
      shopid: '',
      shopInfoId: '', // 重新预约门店id
      imageUrl: [],
      imageUrls: [],
      totalAmount1: '', // 商品总价
      amount: '', // 商品数量
      icon: '', // 商品图片
      displayName: '', // 商品名称
      salesPrice: '', // 商品价格
      description: '', // 商品说明
      id: '',
      productList: [], // 商品列表
      imgCount: 0,
      images: [], // 图片数组

      color: '#E8E8E8',

      setTime: '', // 定时跳转
      // 门店信息

      simpleName: '',
      offsetValue: '',
      activityId: '', // 促销id

      year: '', // 年
      month: '', // 月
      day: '' // 日
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
    toSelectCar() {
      let that = this
      if (this.carInfo.vehicle === null) {
        wx.navigateTo({
          url: '/pages/myCard/main',
          events: {
            // 为指定事件添加一个监听器，获取被打开页面传送到当前页面的数据
            dataFromOpenedPage(data) {
              that.carInfo.vehicle = data.data.vehicle
              that.carInfo.carId = data.data.carId
              console.log('dataFromOpenedPage', data)
              console.log('dataFromOpenedPage2', that.carInfo)
            }
          }
        })
      }
    },
    // 客服事件
    serviceClick(shopInfo) {
      wx.showModal({
        content: this.shopInfo.tel,
        confirmText: '呼叫',
        confirmColor: '#FF6324',
        success(res) {
          if (res.confirm) {
            wx.makePhoneCall({
              phoneNumber: this.shopInfo.tel
            })
          } else if (res.cancel) {
          }
        }
      })
    },
    // // 服务项目点击事件
    // checkClik (main, mark) {
    //   main.isCheck = !main.isCheck
    //   if (this.maintainarr[mark].isCheck) {
    //     let code = main.code
    //     let str = main.name
    //     this.serviceCode = this.serviceCode + code + ';'
    //     this.serviceName = this.serviceName + str + ','
    //   } else {
    //     let code = main.code
    //     let str = main.name
    //     this.serviceCode = this.serviceCode.replace(code + ';', '')
    //     this.serviceName = this.serviceName.replace(str + ',', '')
    //   }
    //   let arr = this.maintainarr.filter(v => {
    //     return v.isCheck == true
    //   })
    //   if (arr.length > 0) {
    //     this.bb = true
    //   } else if (arr.length == 0) {
    //     this.bb = false
    //   }
    // },
    getReserveTime({ Year, Month, Day }) {
      this.day = Day
      this.month = Month
      this.year = Year
      console.log('day', this.year, this.month, this.day)
      CanReserveTimeV3({ ShopId: this.shopId, Year, Month, Day })
        .then(resa => {
          if (resa.code == 10000) {
            this.datearr = resa.data
            console.log('datearr', this.datearr)
          }
        })
        .catch(erra => {})
      // .finally(err => {
      //   this.toTOp()
      // })
      console.log(11)
    },
    // toTOp (e) {
    //   console.log(123)
    //   wx.pageScrollTo({
    //     scrollTop: e.currentTarget.offsetTop,
    //     duration: 300
    //   })
    // },
    // 礼拜点击事件
    // weekClick (item, index) {
    //   let that = this
    //   that.nian = item.year // 修改预约需要的nian
    //   that.reserveDate = item.reserveDate // 修改预约需要的月份
    //   that.offsetValue = item.offsetValue
    //   console.log(889911, item.offsetValue)
    //   that.weekarr.forEach(element => {
    //     element.isHighLight = false
    //   })
    //   item.isHighLight = !item.isHighLight
    //   that.a = 0
    //   that.color = '#E8E8E8'
    //   if (that.identification_code == 1) {
    //     that.getWeekFunction(that.shopid, item.offsetValue, that.reserveId)
    //   } else {
    //     that.getWeekFunction(that.shopid, item.offsetValue, 0)
    //   }
    // },

    // 礼拜点击获取时间函数
    // getWeekFunction (storeId, OffsetValue, ReserveId) {
    //   let that = this
    //   let resultdata = {
    //     ShopId: that.shopid,
    //     // Year: that.nian,
    //     // ReserveDate: that.reserveDate,
    //     OffsetValue: OffsetValue,
    //     ReserveId: ReserveId
    //   }
    //   CanReserveTimeV3(resultdata)
    //     .then(resa => {
    //       if (resa.code == 10000) {
    //         that.datearr = resa.data
    //       }
    //     })
    //     .catch(erra => {})
    // },
    // 时间点击事件
    dateClick(item1) {
      if (item1.isHighLight) {
        this.datearr.forEach(element => {
          element.isChecked = false
        })
        item1.isChecked = !item1.isChecked
        this.color = '#FF6324'
        this.a = 1
        // this.reserveTime = item1.reserveTime
        if (item1.reserveTime == '到店等待') {
          this.reserveTime = 'IsWait'
        } else {
          this.reserveTime = item1.reserveTime
        }
      } else {
        wx.showToast({ title: '所选时间暂不可预约', icon: 'none' })
      }
    },
    // 确认到店时间
    sureDate() {
      let that = this
      console.log('a', that.a)
      if (that.a == 1) {
        // if (that.bb) {
        wx.showModal({
          title: '预约通知',
          content: '确定提交预约请求？',
          success(res) {
            if (res.confirm) {
              that.serviceCode = that.serviceCode.substr(
                0,
                that.serviceCode.length - 1
              ) // 删除最后一位
              that.serviceName = that.serviceName.substr(
                0,
                that.serviceName.length - 1
              ) // 删除最后一位
              // 等于1是用户修改预约跳转的界面
              if (that.identification_code == 1) {
                // 用户修改预约跳转后的页面
                console.log('用户修改预约跳转后的页面')

                let data = {
                  reserveId: that.reserveId,
                  year: that.year,
                  month: that.month,
                  day: that.day,
                  reserveTime: that.reserveTime
                }
                ModifyReserveTime(data).then(res => {
                  if (res.code == '10000') {
                    wx.showToast({
                      title: '预约已受理',
                      icon: 'none',
                      duration: 2000
                    })
                    that.$store.state.active = 1
                    console.log(
                      `that.$store.state.active`,
                      that.$store.state.active
                    )
                    setTimeout(function() {
                      wx.switchTab({
                        url: '/pages/appointment/main'
                      })
                    }, 2000)
                  } else {
                    wx.showToast({
                      title: res.message,
                      icon: 'none',
                      duration: 2000
                    })
                  }
                })
              } else if (that.identification_code == 2) {
                // 等于2是用户点击新增按钮跳转的页面
                console.log('用户点击新增按钮跳转的页面')
                let data = {
                  shopId: that.shopInfo.id, // that.shopId,
                  carId: that.cardID,
                  orderNo: '',
                  year: that.nian,
                  reserveDate: that.reserveDate,
                  reserveTime: that.reserveTime,
                  isAnyOrder: 0,
                  serviceCode: that.serviceCode,
                  serviceName: that.serviceName
                }
                PostNewAdd(data).then(res => {
                  if (res.code === 10000) {
                    wx.showToast({
                      title: '预约已受理',
                      icon: 'none',
                      duration: 2000
                    })
                    //  wx.showToast({
                    //     title: '预约成功',
                    //     icon: 'success'
                    //   })

                    setTimeout(function() {
                      wx.switchTab({
                        url: '/pages/appointment/main'
                      })
                    }, 2000)
                  } else {
                  }
                })
              } else if (that.applynum == 3) {
                console.log('用户点击申请预约按钮跳转的页面')
                // 等于3是用户点击申请预约按钮跳转的页面
                // let data = {
                //   // shopId: that.shopid,
                //   // carId: that.cardID,
                //   // orderNo: that.orderBumber,
                //   // year: that.nian,
                //   // reserveDate: that.reserveDate,
                //   // reserveTime: that.reserveTime,
                //   // isAnyOrder: that.orderBumber == '' ? 0 : 1,
                //   // serviceCode: that.serviceCode,
                //   // serviceName: that.serviceName

                // }
                if (that.carInfo.vehicle === null) {
                  wx.showModal({
                    title: '提示',
                    content: '请先选择预约车辆',
                    success(res) {
                      if (res.confirm) {
                        that.toSelectCar()
                      } else if (res.cancel) {
                      }
                    }
                  })
                }
                let addData = {
                  carId: that.carInfo.carId,
                  orderNo: that.orderBumber,
                  year: that.year,
                  month: that.month,
                  day: that.day,
                  reserveTime: that.reserveTime
                }
                AddShopReserveV3(addData).then(res => {
                  if (res.code === 10000) {
                    wx.showToast({
                      title: '预约已受理',
                      icon: 'none'
                    })
                    that.$store.state.active = 1
                    setTimeout(function() {
                      wx.switchTab({
                        url: '/pages/appointment/main'
                      })
                    }, 2000)
                  } else {
                  }
                })
              }
            }
          }
        })
        // } else {
        //   wx.showToast({ title: '请选择服务项目', icon: 'none' })
        // }
      } else {
        wx.showToast({ title: '请选择服务日期', icon: 'none' })
      }
    },
    // 更换车辆
    changeCard() {
      if (this.orderBumber != '') {
        wx.showToast({ title: '已有车辆', icon: 'none' })
      } else {
        this.a = 0
        this.$router.push('/pages/myCard/main')
      }
    },
    // 更换门店
    changeStore() {
      if (this.orderBumber != '') {
        wx.showToast({ title: '已有门店', icon: 'none' })
      } else {
        this.a = 0
        this.$router.push('/pages/stores/main')
      }
    },
    // 获取附近门店
    // getShopNear () {
    //   let that = this
    //   // 获取最近门店
    //   wx.getLocation({
    //     type: 'gcj02',
    //     altitude: true, // 高精度定位
    //     // 定位成功，更新定位结果
    //     success: res => {
    //       let rquestshop = {
    //         shopIds: [],
    //         longitude: res.longitude,
    //         latitude: res.latitude,
    //         cityId: that.$store.state.curCityInfo.cityId,
    //         districtId: that.$store.state.curCityInfo.districtId,
    //         orderBy: 0,
    //         pageIndex: 1,
    //         pageSize: 1
    //       }
    //       // 获取附近门店列表
    //       PostNearbyStore(rquestshop)
    //         .then(res => {
    //           that.shopInfo.address = res.data.items[0].address
    //           that.shopInfo.simpleName = res.data.items[0].simpleName
    //           that.shopInfo.shopServiceItems = res.data.items[0].shopServices
    //           that.shopInfo.id = res.data.items[0].id
    //           that.shopId = res.data.items[0].id
    //           that.shopInfo.tel = res.data.items[0].telephone
    //         })
    //         .catch(err => {})
    //     },
    // 定位失败回调
    //     fail: function () {
    //       let rquestshop = {
    //         shopIds: [0],
    //         longitude: 0, // 默认0
    //         latitude: 0, // 默认0
    //         cityId: 0,
    //         districtId: 0,
    //         orderBy: 0,
    //         pageIndex: 1,
    //         pageSize: 1
    //       }
    //       // 获取附近门店列表
    //       PostNearbyStore(rquestshop)
    //         .then(res => {
    //           that.shopInfo.address = res.data.items[0].address
    //           that.shopInfo.simpleName = res.data.items[0].simpleName
    //           that.shopInfo.shopServiceItems = res.data.items[0].shopServices
    //           that.shopInfo.id = res.data.items[0].id
    //           that.shopId = res.data.items[0].id
    //           that.shopInfo.tel = res.data.items[0].telephone
    //         })
    //         .catch(err => {})
    //     }
    //   })
    // },
    // 获取修改预约详情函数
    // modifyAppointment () {
    //   let that = this
    //   // 调用/Receive/GetReserveInfo接口  /Receive/CanReserveTime 获取可预约的时间点    获取用户要修改的预约的数据
    //   let resultnum = {
    //     reserveId: that.reserveId
    //   }
    //   GetYuyue(resultnum)
    //     .then(res => {
    //       that.weekarr = res.data.canReserveDate
    //       res.data.canReserveDate.forEach(element => {
    //         if (element.isHighLight) {
    //           that.reserveDate = element.reserveDate
    //           that.nian = element.year
    //         }
    //       })
    //       that.carInfo = res.data.carInfo
    //       that.shopInfo = res.data.shopInfo
    //       that.datearr = res.data.reserveTimeList
    //       res.data.reserveTimeList.forEach(itm => {
    //         if (itm.isChecked) {
    //           if (itm.reserveTime == '到店等待') {
    //             that.reserveTime = 'IsWait'
    //           } else {
    //             that.reserveTime = itm.reserveTime
    //           }
    //         }
    //       })
    //       that.reserveOrder = res.data.reserveOrders
    //       that.appointmentInstruction = res.data.reserveExplain // 预约说明
    //       that.maintainarr = res.data.shopServiceItems // 预约项目
    //       res.data.shopServiceItems.forEach(serve => {
    //         if (serve.isCheck) {
    //           that.serviceCode = that.serviceCode + serve.code + ';'
    //           that.serviceName = that.serviceName + serve.name + ','
    //         }
    //       })
    //       let arr = res.data.reserveOrders.reserveProductVOs
    //       arr.forEach(itemitem => {
    //         itemitem.totalAmount = itemitem.totalAmount.toFixed(2)
    //       })
    //       that.reserveProductVOs = arr
    //       that.color = '#FF6324'
    //       that.a = 1
    //     })
    //     .catch(err => {})
    // },
    modifyAppointment() {
      let that = this
      // 调用/Receive/GetReserveInfo接口  /Receive/CanReserveTime 获取可预约的时间点    获取用户要修改的预约的数据
      let resultnum = {
        // ReserveId: that.reserveId
        ReserveId: that.reserveId
      }
      console.log(885577)
      GetReserveInfoV3(resultnum)
        .then(res => {
          console.log(332255, res)
          that.carInfo = res.data.carInfo
          that.simpleName = res.data.simpleName
          that.disabled = res.data.disabled
          that.tileContent = res.data.items
          that.begin = res.data.surplusBegin
          that.end = res.data.surplusEnd
          that.shopId = res.data.shopId
          that.shopid = res.data.shopId
          that.productList = res.data.reserveOrders
          that.productList.forEach((item, index) => {
            item.reserveProductVOs.forEach((item1, index1) => {
              item1.totalAmount = item1.totalAmount.toFixed(2)
            })
          })

          that.datearr = res.data.reserveTimeList
          that.reserveExplain = res.data.reserveExplain // 预约说明
        })
        .catch(err => {})
        .finally(() => {
          that.getWeek()
        })
    },
    // // 获取申请预约详情函数
    // applyAppointment () {
    //   let that = this
    //   let getStart = {
    //     OrderNo: that.orderBumber
    //   }
    //   GetYuyueStart(getStart)
    //     .then(resb => {
    //       console.log('resb', resb)
    //       that.weekarr = resb.data.canReserveDate
    //       that.carInfo = resb.data.carInfo
    //       that.shopInfo = resb.data.shopInfo
    //       that.shopId = resb.data.shopInfo.id
    //       that.datearr = resb.data.reserveTimeList
    //       that.reserveOrder = resb.data.reserveOrder
    //       that.appointmentInstruction = resb.data.reserveExplain // 预约说明
    //       that.maintainarr = resb.data.shopServiceItems // 预约项目
    //       let arr = resb.data.reserveOrder.reserveProductVOs
    //       arr.forEach(itemitem => {
    //         itemitem.totalAmount = itemitem.totalAmount.toFixed(2)
    //       })
    //       that.reserveProductVOs = arr
    //     })
    //     .catch(errb => {})
    // },
    applyAppointment() {
      let that = this
      // 调用/Receive/GetReserveInfo接口  /Receive/CanReserveTime 获取可预约的时间点    获取用户要修改的预约的数据
      let resultnum = {
        OrderNo: that.orderBumber
      }
      console.log(885577)
      GetReserveInfoV3(resultnum)
        .then(res => {
          console.log(332255, res)
          that.carInfo = res.data.carInfo
          that.simpleName = res.data.simpleName
          that.disabled = res.data.disabled
          that.tileContent = res.data.items
          that.begin = res.data.surplusBegin
          that.end = res.data.surplusEnd
          that.shopId = res.data.shopId
          that.shopid = res.data.shopId
          that.productList = res.data.reserveOrders
          that.datearr = res.data.reserveTimeList
          that.reserveExplain = res.data.reserveExplain // 预约说明
          that.productList.forEach((item, index) => {
            item.reserveProductVOs.forEach((item1, index1) => {
              item1.totalAmount = item1.totalAmount.toFixed(2)
            })
          })
        })
        .catch(err => {})
        .finally(() => {
          that.getWeek()
        })
    },

    // 获取可预约的时间点
    getWeek() {
      GetShopReserveSurplus({ shopId: this.shopId })
        .then(res => {
          this.weekarr = res.data.reserveDate
          console.log('新增日期', this.weekarr)
          this.datearr = res.data.reserveTime
          this.telephone = res.data.phone
        })
        .catch(err => {})
    },
    // 获取新增预约详情函数
    newAddFunction() {
      let that = this
      GetYuyueStart('')
        .then(resb => {
          that.datearr = resb.data.reserveTimeList
          that.weekarr = resb.data.canReserveDate
          that.appointmentInstruction = resb.data.reserveExplain // 预约说明
          that.maintainarr = resb.data.shopServiceItems // 预约项目
        })
        .catch(errb => {})
    },
    // 封装礼拜跟时间清零事件
    weekAndtimeClick() {
      this.weekarr.forEach(element => {
        element.isHighLight = false
      })
      this.datearr.forEach(element => {
        element.isHighLight = false
        element.isChecked = false
      })
    }
  },
  mounted() {
    let that = this
    // 获取系统信息
    wx.getSystemInfo({
      success: function(res) {
        let clientHeight = res.windowHeight
        let clientWidth = res.windowWidth
        let ratio = 750 / clientWidth
        that.windowHeight = clientHeight * ratio + 'rpx'
      }
    })
  },
  onLoad(options) {
    console.log('day', this.year, this.month, this.day)

    let that = this
    if (options.idnum == '1') {
      // 等于1是用户点击修改跳转过来的页面
      wx.setNavigationBarTitle({ title: '修改预约' })
      that.reserveId = options.reserveId // 预约id
      that.identification_code = 1
      that.orderBumber = options.orderNo
      that.shopId = options.shopId
      that.modifyAppointment()
      // let arr = this.datearr.filter((v) => {
      //   return v.isChecked == true
      // })
      // if(arr.length > 0){
      //   this.a = 1
      // }else if(arr.length == 0){
      //   this.a = 0
      // }
      // this.a = 1
      this.bb = true
    } else if (options.idnum == '2') {
      if (options.applynum == '3') {
        // 等于3是申请预约按钮跳转过来的页面
        wx.setNavigationBarTitle({ title: '申请预约' })
        that.orderBumber = options.orderNo // 申请预约按钮传过来的订单编号
        that.applynum = 3
        that.applyAppointment()

        this.bb = true
      } else {
        // 等于2是点击新增按钮跳转过来的页面
        that.identification_code = 2
        that.orderBumber = ''
        that.newAddFunction()
        // that.getShopNear()
      }
    }
  },
  onShow() {
    let that = this
    // if (this.$store.state.carArr[0]) {
    //   if (this.$store.state.globalShop) {
    //     that.shopInfo.simpleName = this.$store.state.globalShop.simpleName
    //     that.shopInfo.address = this.$store.state.globalShop.address
    //     that.shopInfo.shopServiceItems = this.$store.state.globalShop.shopServices
    //     that.shopId = this.$store.state.globalShop.id
    //     this.weekAndtimeClick()
    //   } else {
    //     that.getShopNear()
    //     this.weekAndtimeClick()
    //   }
    //   this.carInfo.carLogo = this.$store.state.carArr[0].brandUrl
    //   this.carInfo.vehicle = this.$store.state.carArr[0].vehicle
    //   this.carInfo.carNO = this.$store.state.carArr[0].carNumber
    // } else {
    //   let that = this
    //   wx.showModal({
    //     title: '提示',
    //     content: '请先选择您的爱车',
    //     showCancel: true,
    //     success (res) {
    //       if (res.confirm) {
    //         that.changeCard()
    //       } else if (res.cancel) {
    //         wx.navigateBack({})
    //       }
    //     },
    //     fail () {
    //       that.changeCard()
    //     }
    //   })
    // }
  },
  onHide() {
    this.weekarr = []
    this.a = 0
    this.datearr = []
  },
  // 当点击返回按钮，页面销毁时，清除从上个页面传的参，防止不明情况的bug产生
  onUnload: function() {
    this.identification_code = ''
    this.weekarr = []
    this.carInfo.carNO = ''
    this.carInfo.carLogo = ''
    this.carInfo.brand = ''
    this.carInfo.vehicle = ''
    this.shopInfo.address = ''
    this.shopInfo.simpleName = ''
    this.shopInfo.shopServiceItems = []
    this.datearr = []
    this.applynum = ''
    this.a = 0
    this.bb = false
    this.reserveProductVOs = []
    this.reserveOrder = {}
    this.color = '#E8E8E8'
    this.shopId = ''
    this.reserveId = ''
    this.nian = ''
    this.reserveDate = ''
    this.reserveTime = ''
    this.maintainarr = []
    this.orderBumber = ''
    this.reserveExplain = ''
    this.serviceCode = ''
    this.serviceName = ''
    this.tileContent = []
    this.begin = []
    this.end = []
    this.day = ''
    this.month = ''
    this.year = ''
    clearInterval(this.setTime)
  }
}
</script>
<style scoped lang="scss">
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  background: #f9f9f9;
}
.appointment {
  width: 100%;
}

.btn {
  position: fixed;
  bottom: 0;
  left: 0;
  width: 750rpx;
  /* height:98rpx; */
  background: rgba(255, 255, 255, 1);
  padding: 10rpx 30rpx;
  box-sizing: border-box;
  z-index: 20;
}

.tab1 {
  width: 100%;
  // height: 260rpx;
  border-radius: 10rpx 10rpx 10rpx 10rpx;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  padding: 30rpx;
  padding-top: 0;
  box-sizing: border-box;
  background: #ffffff;
  margin-top: 16rpx;
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
.topDes {
  background: #fff;
  width: 100%;
  display: flex;
  align-items: center;
  margin-bottom: 20rpx;
  padding: 30rpx;
  box-sizing: border-box;
}
.carTitle {
  max-width: 300rpx;
  border-right: 1rpx solid #eee;
  font: 28rpx/1 '思源黑体 CN Regular';

  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 1;
  -webkit-box-orient: vertical;
  overflow: hidden;
  min-width: 300rpx;
}
.shopTitle {
  max-width: 320rpx;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 1;
  -webkit-box-orient: vertical;
  overflow: hidden;
  min-width: 300rpx;

  font: 28rpx/1 '思源黑体 CN Regular';
  margin-left: 30rpx;
}
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  background: #f9f9f9;
}

.appointment {
  width: 100%;
}

.appointment_have {
  width: 100%;
  padding: 0 30rpx;
  box-sizing: border-box;

  .card_tab {
    width: 100%;
    height: 160rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 10rpx;
    padding: 20rpx 28rpx 25rpx 16rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: row;
    // justify-content: space-between;
    align-items: center;
    margin-top: 16rpx;
    border-right: 2rpx solid #e2e2e2;

    .img_card {
      width: 100rpx;
      height: 100rpx;
      margin-right: 8rpx;
    }

    .card_content {
      width: 376rpx;
      padding: 10rpx 10rpx;
      height: 120rpx;
      box-sizing: border-box;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-start;
      margin-right: 60rpx;
      border-right: 1px solid #eee;

      .p1 {
        font-size: 28rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
      }

      .p2 {
        font-size: 24rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(136, 136, 136, 1);
      }
    }
  }

  .change {
    display: flex;
    flex-direction: column;
    align-items: center;
    color: #999;
    font: 25rpx/1.5 'SourceHanSansCN-Regular';
  }

  .card_add {
    width: 100%;
    height: 100rpx;
    border: 2rpx solid #f3f3f3;
    line-height: 96rpx;
    background: rgba(255, 255, 255, 1);
    font: 400 28rpx/80rpx 'Source Han Sans CN';
    color: rgba(51, 51, 51, 1);
    padding-left: 20rpx;
    margin-top: 16rpx;
    display: flex;
    align-items: center;
  }

  .card_store {
    width: 100%;
    height: 200rpx;
    padding: 20rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: flex-start;
    background: #ffffff;
    margin-top: 16rpx;

    .p_one {
      width: 100%;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;

      .p_one_content {
        font-size: 28rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
      }

      .p_one_img {
        width: 28rpx;
        height: 28rpx;
      }
    }

    .p_two {
      width: 533rpx;
      font-size: 24rpx;
      font-family: SourceHanSansCN;
      font-weight: 400;
      color: rgba(166, 165, 174, 1);
      overflow: hidden;
      white-space: pre-wrap;
      display: -webkit-box;
      text-overflow: ellipsis;
      -webkit-line-clamp: 2;
      -webkit-box-orient: vertical;
    }

    .p_three {
      width: 100%;
      display: flex;
      flex-direction: row;
      justify-content: flex-start;
      align-items: center;

      .txt1 {
        padding: 0 10rpx;
        box-sizing: border-box;
        height: 36rpx;
        // text-align: center;
        // line-height: 28rpx;
        border: 1rpx solid rgba(255, 99, 36, 1);
        border-radius: 4rpx;
        font-size: 24rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(255, 99, 36, 1);
        margin-right: 10rpx;
      }
    }
  }

  .store_add {
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
    margin-top: 16rpx;
  }
}

.top_card {
  width: 100%;
  height: 90rpx;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  background: rgba(255, 255, 255, 1);
}

.top_card text {
  flex: 1;
  text-align: center;
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}

.top_date {
  width: 690rpx;
  // height: 505rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  margin: 16rpx 0;
  padding: 30rpx 20rpx;
  box-sizing: border-box;
}

.top_week {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}

.top_date .top_week .one {
  width: 100rpx;
  height: 100rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.top_date .top_week .one .yuan {
  width: 70rpx;
  height: 85rpx;
  border-radius: 36rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  padding: 5rpx 0;
}

.span_span {
  width: 64rpx;
  height: 64rpx;
  background: rgba(232, 232, 232, 1);
  border-radius: 50%;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  box-sizing: border-box;
}

.top_date .top_week .one .txt {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
  margin-top: 5rpx;
}

.yuan_bg {
  background: rgba(255, 203, 182, 1);
}

.span_bg {
  background: rgba(255, 99, 36, 1);
  color: rgba(255, 255, 255, 1);
  border: 6rpx solid rgba(255, 203, 182, 1);
  box-sizing: border-box;
}

.bottom_date {
  width: 100%;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: flex-start;
  align-items: flex-start;
  margin-top: 37rpx;
  position: relative;
}

.one_before {
  position: relative;
  width: 145rpx;
  height: 60rpx;
  background: rgba(255, 255, 255, 1);
  border: 1rpx solid rgba(227, 227, 227, 1);
  border-radius: 8rpx;
  text-align: center;
  line-height: 58rpx;
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
  margin-right: 16rpx;
  margin-bottom: 20rpx;
}

.posimg {
  position: absolute;
  top: 0rpx;
  right: 2rpx;
  width: 24rpx;
  height: 24rpx;
}

.call {
  position: absolute;
  right: 16rpx;
  bottom: 16rpx;

  width: 180rpx;
  height: 60rpx;
  background: #ffeee7;
  border: 2rpx solid #ff6324;
  border-radius: 50rpx;
  color: #ff6324;
  font: 24rpx/60rpx 'SourceHanSansCN-Regular';
  padding: 10rpx;
  box-sizing: border-box;
  display: flex;
  align-items: center;
}

.stor {
  // width: 310rpx;
  width: 145rpx;
  height: 60rpx;
  background: rgba(255, 255, 255, 1);
  border: 1rpx solid rgba(227, 227, 227, 1);
  border-radius: 8rpx;
  text-align: center;
  line-height: 58rpx;
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
}

.top_yuyue {
  width: 690rpx;
  height: 180rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  margin: 0 0 16rpx;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
  padding: 38rpx 26rpx 45rpx 28rpx;
  box-sizing: border-box;
}

.top_yuyue .txt {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(51, 51, 51, 1);
}

.top_yuyue .checkbox {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}

.appointment_instruction {
  width: 690rpx;
  // height: 240rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
  padding: 30rpx 32rpx 35rpx 30rpx;
  box-sizing: border-box;
  margin-bottom: 33rpx;
}

.img {
  width: 155rpx;
  height: 160rpx;

  margin-right: 30rpx;
}

.box2 {
  position: relative;
}

.box1 {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: flex-start;
  align-items: center;
  margin: 20rpx 20rpx 0 0;
}

.delbtn {
  width: 40rpx;
  height: 40rpx;
  position: absolute;
  top: -4rpx;
  right: 4rpx;
}

.box {
  width: 100%;
  height: 210rpx;
  background: #fff;
  border-radius: 10rpx;
  padding-top: 15rpx;

  .top_input {
    width: 100%;
    min-height: 180rpx;
    padding: 30rpx 20rpx;
    box-sizing: border-box;
    text-align: left;
    border-radius: 20rpx;
    font-size: 28rpx;
    background: #f3f3f3;
  }
}

.appointment_instruction .txt {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(51, 51, 51, 1);
}

.appointment_instruction .txt_wenzi {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(153, 153, 153, 1);
  margin-top: 10rpx;
}

// .btn {
//   width: 750rpx;
//   /* height:98rpx; */
//   background: rgba(255, 255, 255, 1);
//   padding: 10rpx 30rpx;
//   box-sizing: border-box;
// }

.tab1 {
  width: 100%;
  // height: 260rpx;
  border-radius: 10rpx 10rpx 10rpx 10rpx;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  padding: 30rpx;
  padding-top: 0;
  box-sizing: border-box;
  background: #ffffff;
  margin-top: 16rpx;
}

.top_two {
  position: relative;
  // padding-left: 30rpx;
  // padding-right: 30rpx;
  box-sizing: border-box;
  // width: 690rpx;
  height: 170rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  margin: 16rpx 0;
  .scroll {
    width: 100%;
    display: -webkit-box;
    -webkit-overflow-scrolling: touch;
    -ms-overflow-style: none;
    overflow: -moz-scrollbars-none;
    overflow-x: scroll;
    .top_two_box {
      width: 100%;
      display: flex;
      flex-direction: row;
      justify-content: flex-start;
      align-items: center;
      .img {
        width: 102rpx;
        height: 102rpx;
        margin-right: 18rpx;
      }
    }
  }
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

.matainl {
  flex: 2;
  display: flex;
  flex-direction: row;

  align-items: center;
}

.matainl img {
  width: 49rpx;
  height: 56rpx;
}

.matainR {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  min-width: 36rpx;
}

.matainR img {
  width: 36rpx;
  height: 36rpx;
  margin: 0 0 20rpx 50rpx;
}

.matain {
  width: 100%;
  min-height: 140rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  background: #ffffff;
  padding: 10rpx 0 10rpx 16rpx;
  box-sizing: border-box;
  border-bottom: 1rpx solid #f3f3f3;
}
.topDes {
  background: #fff;
  width: 100%;
  display: flex;
  align-items: center;
  margin-bottom: 20rpx;
  padding: 30rpx;
  box-sizing: border-box;
}
.carTitle {
  max-width: 300rpx;
  border-right: 1rpx solid #eee;
  font: 28rpx/1 '思源黑体 CN Regular';

  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 1;
  -webkit-box-orient: vertical;
  overflow: hidden;
  min-width: 300rpx;
}
.shopTitle {
  max-width: 320rpx;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 1;
  -webkit-box-orient: vertical;
  overflow: hidden;
  min-width: 300rpx;

  font: 28rpx/1 '思源黑体 CN Regular';
  margin-left: 30rpx;
}
.activeOne {
  background: rgba(255, 238, 231, 1);
  border: 1rpx solid rgba(255, 193, 173, 1);
  color: rgba(255, 99, 36, 1);
}
</style>