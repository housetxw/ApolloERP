<template>
  <div class="demo_page" :style="{minHeight:windowHeight}">
    <div class="top_card">
      <!-- 車信息-->
      <div class="card_tab" @tap="changeCard" v-if="carArrFirst.vehicle != ''">
        <img :src="carArrFirst.brandUrl" alt class="img_card" />
        <div class="card_content">
          <p class="p1">{{carArrFirst.vehicle}}</p>
          <p class="p2">{{carArrFirst.carNumber}}</p>
        </div>
        <div style="display:flex;flex-direction:column;align-items:center;">
          <img
            src="https://m.aerp.com.cn/mini-RG-main/changeCarIcon.png"
            alt
            style="width:60rpx;height:60rpx;margin-top:20rpx;"
          />
          <p class="p2">换车</p>
        </div>
      </div>
      <div class="card_add" @tap="changeCard" v-else>请选择车辆</div>
      <!-- 门店信息 -->
      <div class="card_store" @tap="changeStore" v-if="storeArr.simpleName != ''">
        <img :src="storeArr.img" alt class="img_card" />
        <div class="card_content">
          <p class="p1_p1">{{storeArr.simpleName}}</p>
          <p class="p2_p2">{{storeArr.address}}</p>
        </div>
        <img
          src="https://m.aerp.com.cn/mini-RG-main/maintenance_jump_icon.png"
          alt
          style="width:24rpx;height:24rpx"
        />
      </div>
      <div class="store_add" @tap="changeStore" v-else>请选择服务门店</div>
    </div>
    <!-- 套餐卡服务 -->
    <div class="box" v-for="(item1,index1) in boxarr" :key="index1">
      <div class="bottom_box">
        <!-- 洗车 -->
        <div class="byservice" @tap="serviceClick(item1,index1)">
          <div class="byservice_left">
            <img :src="item1.imageUrl" alt />
            <div style="margin-left:10rpx;">
              <p
                class="overHide"
                style="font-size:30rpx;font-weight:bold;color:rgba(51,51,51,1);"
              >{{item1.categoryName}}</p>
              <p
                class="overHide"
                style="font-size:24rpx;font-weight:400;color:rgba(166,165,174,1);"
              >{{item1.description}}</p>
            </div>
          </div>
          <div class="byservice_right">
            <img
              :src="activeIndex == index1?'https://m.aerp.com.cn/mini-RG-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-RG-main/maintenance_radio.png'"
              alt
            />
          </div>
        </div>
        <!-- 爱车推荐  -->
        <div class="loveCard" v-if="activeIndex == index1">
          <text class="recommend">根据您的爱车推荐</text>
        </div>
        <!-- 套餐内容 -->
        <div v-if="activeIndex == index1">
          <div
            :class="[{'border_style': item2.isSelected?true:false} , 'top_title']"
            v-for="(item2,index2) in mealarr"
            :key="index2"
          >
            <div class="top_img" @tap="toDetail(item2)">
              <img :src="item2.image1" alt class="img1" />
            </div>
            <div style="position:relative;display:flex;flex-direction:row;width:520rpx;">
              <div class="top_content" @tap="toDetail(item2)">
                <p class="p1">{{item2.name}}</p>
                <div class="p2">
                  <p class="p3">
                    <text class="txt1">￥{{item2.salesPrice}}</text>
                  </p>
                </div>
              </div>
              <div class="add">
                <!-- <div class="left_foot">-</div>
                  <div class="center_foot">{{item2.quantity}}</div>
                <div class="right_foot">+</div>-->
                <van-stepper v-model="item2.quantity" disabled />
              </div>
              <div class="selectedImg" @tap="item2Click(item1,index1,item2,index2)">
                <img
                  :src="item2.isSelected?'https://m.aerp.com.cn/mini-RG-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-RG-main/maintenance_radio.png'"
                  alt
                  @tap="item2Click(item1,index1,item2,index2)"
                  class="img"
                />
              </div>
            </div>
          </div>
        </div>
        <div v-if="mealarr.length == 0 && activeIndex == index1" class="else_img_one">
          <img src="https://m.aerp.com.cn/mini-RG-main/maintenance_null_img.png" alt />
          <text>暂无合适商品推荐</text>
        </div>
      </div>
    </div>
    <!-- 底部确定按钮 -->
    <div class="bottom" style="z-index:30">
      <img
        src="https://m.aerp.com.cn/mini-RG-main/maintenance_tel_icon.png"
        alt
        class="tel"
        @tap="telClick"
      />
      <!-- <button open-type="contact" session-from="weapp" show-message-card send-message-path="/pages/detailsPages/main" send-message-title="【正品授权】美孚/Mobil 美孚1号全合成机油 5W-30SN级（4L装）" send-message-img="https://m.aerp.com.cn/mini-RG-main/goodsdetail_banner_pic.png">客服</button> -->
      <p style="min-width:220rpx;">
        合计：
        <span class="orange1">￥{{money}}</span>
      </p>

      <div class="orange" @tap="downUp">
        明细
        <img
          src="https://m.aerp.com.cn/mini-RG-main/downRed.png"
          alt
          class="down"
          v-if="downShow"
        />
        <img src="https://m.aerp.com.cn/mini-RG-main/upRed.png" alt class="down" v-else />
      </div>

      <text class="btn" @tap="sureJiesuan">确定</text>
    </div>
    <van-popup
      :show="moneyShow "
      closeable
      position="bottom"
      custom-style="height: 50%"
      @close="onClose"
      z-index="20"
    >
      <div class="van-top">
        <h3 class="van-title">金额优惠明细</h3>
        <h3 class="van-title1">*实际优惠金额请以下单页为准</h3>
      </div>
      <div class="top-content">
        <h3 class="content-name">服务总价</h3>
        <h3 class="content-value">￥{{allMoney}}</h3>
      </div>
      <div class="top-content" v-if="serviceFee!=''">
        <h3 class="content-name">服务费</h3>
        <h3 class="content-value">+¥{{serviceFee}}</h3>
      </div>
      <div class="top-content" v-if="diamondsDiscountAmount!=''">
        <h3 class="content-name">促销</h3>
        <h3 class="content-value">-¥{{diamondsDiscountAmount}}</h3>
      </div>
      <div class="top-content">
        <h3 class="content-name">
          优惠券优惠
          <!-- <span style="color:#FF6424;font-size:24rpx;" @tap="couponClick">点击查看优惠券</span> -->
        </h3>
        <h3 class="content-value1">-¥{{concession}}</h3>
      </div>
    </van-popup>
  </div>
</template>
<script>
import {
  PostNearbyStore,
  GetIndexPageInfor,
  GetIndexPageSelectInfor,
  PostOrderMoney,
  PostFirstOrder
} from '../../api'

import eventBus from '../../utils/eventBus.js'

let pidCount = []
export default {
  data() {
    return {
      isShow: false,
      downShow: true,
      moneyShow: false,
      windowHeight: '',
      boxarr: [], // 标题数组
      mealarr: [], // 套餐数组
      money: '0.00',
      storeArr: {
        simpleName: '', // 门店名称
        address: '', // 门第地址
        img: '' // 门店图片
      }, // 门店信息
      storeid: '', // 门店id
      activeIndex: -1, // 选中的变量
      allApplicableCoupons: [], // 优惠券数组
      services: [], // 服务数组
      servicePrice: '0.00', // 服务费
      concession: '0.00', // 优惠
      allMoney: '0.00', // 服务总价
      serviceFee: '0.00', // 服务费
      diamondsDiscountAmount: '0.00' // 促销
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
    // 客服事件
    telClick() {
      wx.showModal({
        content: '021-55558888',
        confirmText: '呼叫',
        confirmColor: '#FF6324',
        success(res) {
          if (res.confirm) {
            console.log('用户点击确定')
            wx.makePhoneCall({
              phoneNumber: '021-55558888'
            })
          } else if (res.cancel) {
            console.log('用户点击取消')
          }
        }
      })
    },
    // 跳转门店详情
    toDetail(item2) {
      console.log('item2', item2.productCode)
      console.log('cardID', this.cardID)
      let that = this
      // 增加判断条件，看商品是否可以进入商品详情页
      // if(){

      // }else{

      // }
      this.$router.push({
        path: '/pages/detailsPages/main',
        query: {
          provinceId: that.$store.state.curCityInfo.provinceId,
          // shopPid: item.pid,
          cityId: that.$store.state.curCityInfo.cityId,
          productCode: item2.productCode,
          storeArr: JSON.stringify(that.storeArr),
          isFromSearch: 'istrue',
          carId: that.cardID,
          shopId: that.storeid
        }
      })
    },
    // 套餐点击事件
    item2Click(item1, index1, item2, index2) {
      this.mealarr.forEach(element => {
        element.isSelected = false
      })
      item2.isSelected = !item2.isSelected
      pidCount = this.delArrVal(pidCount, index1)
      let product1 = {}
      product1.pid = item2.productCode
      product1.number = item2.quantity
      product1.index = index1
      pidCount.push(product1)
      this.getMoney()
    },
    // 减
    reduceClick(index1, item2, item1) {
      if (item2.quantity > 1) {
        pidCount = this.delArrVal(pidCount, index1)
        item2.quantity = item2.quantity - 1
        let product1 = {}
        product1.pid = item2.productCode
        product1.number = item2.quantity
        product1.index = index1
        pidCount.push(product1)
        if (item2.isSelected) {
          this.getMoney()
        }
      } else {
        wx.showToast({ title: '不能在减少了', icon: 'none' })
      }
    },
    // 加
    addValueClick(index1, item2, item1) {
      pidCount = this.delArrVal(pidCount, index1)
      item2.quantity = item2.quantity + 1
      let product1 = {}
      product1.pid = item2.productCode
      product1.number = item2.quantity
      product1.index = index1
      pidCount.push(product1)
      if (item2.isSelected) {
        this.getMoney()
      }
    },
    delArrVal(arr, val) {
      // 查找数组中的某个值并全部删除    第一个参数是查找的数组 第二个参数是删除的值
      for (let i = 0; i < arr.length; i++) {
        if (arr[i].index == val) {
          arr.splice(i, 1)
          i--
        }
      }
      return arr
    },
    // 试算金额接口
    getMoney() {
      let that = this
      let footValue = {
        carId: that.cardID,
        shopId: that.storeid,
        productInfos: pidCount,
        produceType: 0
      }
      PostFirstOrder(footValue)
        .then(res => {
          console.log(`试算金额`, res)
          that.money = res.data.actualAmount.toFixed(2)
          that.totalDoorToDoorFee = res.data.totalDoorToDoorFee.toFixed(2)
          that.serviceFee = res.data.serviceFee.toFixed(2)
          that.diamondsDiscountAmount = res.data.diamondsDiscountAmount.toFixed(
            2
          )
          that.allMoney = res.data.totalProductAmount.toFixed(2)
          that.allApplicableCoupons = res.data.allApplicableCoupons
          that.services = res.data.services // 服务数组
          that.concession = res.data.totalCouponAmount.toFixed(2)
          that.servicePrice = res.data.serviceFee.toFixed(2)
        })
        .catch(err => {
          console.log(`试算金额错误信息${err}`)
        })
    },
    // 金额明细显示事件
    downUp() {
      this.downShow = !this.downShow
      this.moneyShow = !this.moneyShow
    },
    onClose() {
      this.moneyShow = false
    },
    // 优惠券点击事件
    couponClick() {
      console.log(`优惠券`)
      // this.$router.push('/pages/coupon/main')
      this.$router.push({
        path: '/pages/coupon/main',
        query: {
          isOrder: 'yes',
          userCouponId: JSON.stringify(this.allApplicableCoupons)
        }
      })
    },
    // 获取页面详情
    getPageDetail() {
      let that = this
      if (this.$store.state.curCityInfo.city) {
        if (this.$store.state.carArr[0]) {
          if (this.$store.state.globalShop) {
            that.storeArr = this.$store.state.globalShop
            that.storeid = this.$store.state.globalShop.id
            that.getIndexPage()
          } else {
            // 获取门店
            wx.getLocation({
              type: 'gcj02',
              altitude: true, // 高精度定位
              // 定位成功，更新定位结果
              success: res => {
                that.longitude = res.longitude
                that.latitude = res.latitude
                let rquestshop = {
                  shopIds: [],
                  longitude: res.longitude,
                  latitude: res.latitude,
                  cityId: that.$store.state.curCityInfo.cityId,
                  districtId: that.$store.state.curCityInfo.districtId,
                  orderBy: 0,
                  pageIndex: 1,
                  pageSize: 1
                }
                // 获取附近门店列表

                PostNearbyStore(rquestshop)
                  .then(res => {
                    console.log(
                      `获取附近门店列表成功函数,${JSON.stringify(res)}`
                    )
                    that.storeArr.address = res.data.items[0].address
                    that.storeArr.simpleName = res.data.items[0].simpleName
                    that.storeArr.img = res.data.items[0].img
                    that.storeid = res.data.items[0].id
                    that.getIndexPage()
                  })
                  .catch(err => {
                    console.log(`获取附近门店列表失败函数,${err}`)
                  })
              },
              // 定位失败回调
              fail: function() {
                let rquestshop = {
                  shopIds: [],
                  longitude: 0,
                  latitude: 0,
                  cityId: that.$store.state.curCityInfo.cityId,
                  districtId: that.$store.state.curCityInfo.districtId,
                  orderBy: 0,
                  pageIndex: 1,
                  pageSize: 1
                }
                // 获取附近门店列表
                PostNearbyStore(rquestshop)
                  .then(res => {
                    // console.log(`获取附近门店列表成功函数,${JSON.stringify(res)}`)
                    that.storeArr.address = res.data.items[0].address
                    that.storeArr.simpleName = res.data.items[0].simpleName
                    that.storeArr.img = res.data.items[0].img
                    that.storeid = res.data.items[0].id
                    that.getIndexPage()
                  })
                  .catch(err => {
                    console.log(`获取附近门店列表失败函数,${err}`)
                  })
              }
            })
          }
        } else {
          let that = this
          wx.showModal({
            title: '提示',
            content: '请先选择您的爱车',
            showCancel: true,
            success(res) {
              if (res.confirm) {
                that.$router.push('/pages/carBrand/main')
              } else if (res.cancel) {
                wx.navigateBack({})
              }
            },
            fail() {
              that.$router.push('/pages/carBrand/main')
            }
          })
        }
      } else {
        wx.showModal({
          title: '提示',
          content: '请先选择城市',
          showCancel: true,

          success(res) {
            if (res.confirm) {
              that.toCity()
            } else if (res.cancel) {
              that.$router.push({ path: '/pages/index/main', isTab: true })
            }
          },
          fail() {
            that.$router.push({ path: '/pages/index/main', isTab: true })
          }
        })
      }
    },
    // 洗车服务
    serviceClick(item1, index1) {
      console.log(`小保养服务`, item1, index1)
      pidCount = []
      this.money = '0.00'
      this.activeIndex = index1
      this.getIndexPageSelect(item1.id, this.storeid, index1) // this.storeid
      if (this.mealarr.length === 0) {
        return false
      }

      // if (this.activeIndex === index1) {
      //   let product =this.mealarr.find(ele => {
      //     return ele.isSelected === true
      //   })
      //   let product1 = {}
      //   product1.pid = product.productCode
      //   product1.number = product.quantity
      //   product1.index = index1
      //   pidCount.push(product1)
      // }else{
      //   pidCount = this.delArrVal(pidCount, index1)
      // }
      // if (pidCount.length === 0) {
      //   this.money = '0.00'
      //   return false
      // }
      // this.getMoney()
    },
    // 换车
    changeCard() {
      this.$router.push('/pages/myCard/main')
    },
    // 换店
    changeStore() {
      this.$router.push('/pages/stores/main')
    },
    // 获取门店函数
    getShopStores() {
      let that = this
      // 获取门店
      wx.getLocation({
        type: 'gcj02',
        altitude: true, // 高精度定位
        // 定位成功，更新定位结果
        success: res => {
          let rquestshop = {
            shopIds: [],
            longitude: res.longitude,
            latitude: res.latitude,
            cityId: that.$store.state.curCityInfo.cityId,
            districtId: that.$store.state.curCityInfo.districtId,
            orderBy: 0,
            pageIndex: 1,
            pageSize: 1
          }
          // 获取附近门店列表
          PostNearbyStore(rquestshop)
            .then(res => {
              // console.log(`获取附近门店列表成功函数,${JSON.stringify(res)}`)
              that.storeArr.address = res.data.items[0].address
              that.storeArr.simpleName = res.data.items[0].simpleName
              that.storeArr.img = res.data.items[0].img
              that.storeid = res.data.items[0].id
            })
            .catch(err => {
              console.log(`获取附近门店列表失败函数,${err}`)
            })
        },
        // 定位失败回调
        fail: function() {
          let rquestshop = {
            shopIds: [],
            longitude: 0,
            latitude: 0,
            cityId: that.$store.state.curCityInfo.cityId,
            districtId: that.$store.state.curCityInfo.districtId,
            orderBy: 0,
            pageIndex: 1,
            pageSize: 1
          }
          // 获取附近门店列表
          PostNearbyStore(rquestshop)
            .then(res => {
              // console.log(`获取附近门店列表成功函数,${JSON.stringify(res)}`)
              that.storeArr.address = res.data.items[0].address
              that.storeArr.simpleName = res.data.items[0].simpleName
              that.storeArr.img = res.data.items[0].img
              that.storeid = res.data.items[0].id
            })
            .catch(err => {
              console.log(`获取附近门店列表失败函数,${err}`)
            })
        }
      })
    },
    // 结算跳转至订单确认页面
    sureJiesuan() {
      if (this.carArrFirst.vehicle == '') {
        wx.showToast({ title: '请选择车辆', icon: 'none' })
      } else if (this.storeArr.simpleName == '') {
        wx.showToast({ title: '请选择服务门店', icon: 'none' })
      } else if (this.money == '0.00') {
        wx.showToast({ title: '请选择商品', icon: 'none' })
      } else {
        this.$router.push({
          path: '/pages/orderSure/main',
          query: {
            cardID: this.cardID,
            storeid: this.storeid,
            productInfos: JSON.stringify(pidCount),
            orderType: 3,
            storeArr: JSON.stringify(this.storeArr)
          }
        })
      }
    },
    // 页面首次加载获取信息(美容洗车)
    getIndexPage() {
      let that = this
      let carData = { categoryId: '2' }
      GetIndexPageInfor(carData)
        .then(res => {
          console.log(`获取首页类目信息,${JSON.stringify(res)}`)
          that.boxarr = res.data
        })
        .catch(err => {
          console.log(`获取首页类目信息失败函数,${err}`)
        })
    },
    // 根据类目查询商品信息
    getIndexPageSelect(a, b, index1) {
      let that = this
      let rquest = {
        CategoryId: a,
        ShopId: b
      }
      GetIndexPageSelectInfor(rquest)
        .then(res => {
          console.log(`根据类目查询商品信息,${JSON.stringify(res)}`)
          that.mealarr = res.data
          // let addData = [{
          //   commentNumber: 0,
          //   displayName: '普洗',
          //   favorableRate: 0,
          //   image1: 'https://m.aerp.com.cn/productImage/201912231706157144.jpg',
          //   isSelected: false,
          //   name: '普洗',
          //   originalFactory: false,
          //   productCode: 'MRXC-BZ-JC-4-FU',
          //   quantity: 1,
          //   salesPrice: 0.01
          // }, {
          //   commentNumber: 0,
          //   displayName: '普洗',
          //   favorableRate: 0,
          //   image1: 'https://m.aerp.com.cn/productImage/201912231706157144.jpg',
          //   isSelected: false,
          //   name: '普洗',
          //   originalFactory: false,
          //   productCode: 'MRXC-BZ-JC-4-FU',
          //   quantity: 1,
          //   salesPrice: 0.01
          // }]
          // that.mealarr = addData
          that.mealarr.forEach(element => {
            element.isSelected = false
          })
          that.mealarr[0].isSelected = !that.mealarr[0].isSelected
          pidCount = that.delArrVal(pidCount, index1)
          let product1 = {}
          product1.pid = that.mealarr[0].productCode
          product1.number = that.mealarr[0].quantity
          product1.index = index1
          pidCount.push(product1)
          that.getMoney()
        })
        .catch(err => {
          console.log(`根据类目查询商品信息失败函数,${err}`)
        })
    }
  },
  mounted() {
    this.getPageDetail()
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
        that.windowHeight = clientHeight * ratio + 'rpx'
      }
    })
    that.globalData.serviceType = 'toShop'
    that.globalData.OrderType = 3
    // console.log(that.globalData.serviceType)
    this.globalData.ProductType = 0
    console.log('globalData.ProductType', that.globalData.ProductType)
    console.log('globalData.OrderType', that.globalData.OrderType)

    console.log('globalData.serviceType', that.globalData.serviceType)
  },
  onShow() {
    eventBus.$on('isonShow', res => {
      console.log(res)
      this.isShow = res
    })
    if (this.isShow == true) {
      this.getPageDetail()
      this.money = '0.00'
      this.serviceFee = ' 0.00'
      this.allMoney = '0.00'
      this.concession = '0.00'
      this.diamondsDiscountAmount = '0.00'
      this.activeIndex = -1
      pidCount = []
    }
  },
  onHide: function() {
    // this.activeIndex = -1
    this.isShow = false
    this.downShow = true
    this.moneyShow = false
  },
  onUnload() {
    this.money = '0.00'
    this.diamondsDiscountAmount = '0.00'
    this.serviceFee = ' 0.00'
    this.allMoney = '0.00'
    this.concession = '0.00'
    this.activeIndex = -1
    this.isShow = false
    this.downShow = true
    this.moneyShow = false
    pidCount = []
    this.globalData.isCarShow = false
  }
}
</script>
<style scoped lang="scss">
.overHide {
  width: 450rpx;
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 1;
  -webkit-box-orient: vertical;
  font-family: Source Han Sans CN;
  margin-top: 15rpx;
}
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  background: #f9f9f9;
  padding-bottom: 100rpx;
  box-sizing: border-box;
}
/* 顶部 */
.box {
  margin-top: 16rpx;
}

.top_card {
  width: 100%;
  // height: 320rpx;
  display: flex;
  flex-direction: column;
  // justify-content: center;
  align-items: center;
  background: rgba(255, 255, 255, 1);
  .card_tab {
    width: 100%;
    min-height: 160rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 10rpx;
    padding: 20rpx 28rpx 25rpx 28rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    border-bottom: 1rpx solid #f3f3f3;
    // margin-top: 16rpx;
    .img_card {
      width: 100rpx;
      height: 100rpx;
    }
    .card_content {
      width: 486rpx;
      min-height: 125rpx;
      padding: 20rpx 0;
      box-sizing: border-box;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-start;
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
    }
  }
  .p2 {
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
  .card_store {
    width: 100%;
    min-height: 150rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 10rpx;
    padding: 10rpx 28rpx 10rpx 28rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .img_card {
      width: 100rpx;
      height: 100rpx;
    }
    .card_content {
      width: 486rpx;
      // height: 125rpx;
      // padding: 20rpx 0;
      box-sizing: border-box;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-start;
      .p1_p1 {
        width: 454rpx;
        min-height: 41rpx;

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
      .p2_p2 {
        width: 454rpx;
        height: 69rpx;
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
  }
}

.add {
  width: 230rpx;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  position: absolute;

  div {
    width: 50rpx;
    height: 50rpx;
    // border: 2rpx solid #e8e8e8;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    background: #f2f3f5;
    border: 0;
    margin: 2rpx;
    padding: 1rpx;
    white-space: nowrap;
    font-family: 'UICTFontTextStyleBody';
    color: #323233;
  }
}

.bottom_box {
  width: 100%;
  padding: 30rpx;
  padding-top: 0;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  background: rgba(255, 255, 255, 1);
}
.byservice {
  width: 100%;
  height: 140rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.byservice_left {
  width: 550rpx;
  display: flex;
  flex-direction: row;
  align-items: center;
}
.byservice_left img {
  width: 66rpx;
  height: 66rpx;
}
.byservice_right {
  height: 66rpx;
  width: 66rpx;

  position: absolute;
  right: 22rpx;
  display: flex;
  justify-content: center;
  align-items: center;

  margin-right: 25rpx;
}
.byservice_right img {
  width: 36rpx;
  height: 36rpx;
}

.loveCard {
  width: 100%;
  height: 42rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 18rpx;
}
.recommend {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}
.rlues {
  display: flex;
  flex-direction: row;
  align-items: center;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
}

/* 一个套餐样式 */
.top_title {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  height: 170rpx;
  background: rgba(255, 255, 255, 1);
  border: 2rpx solid rgba(233, 233, 233, 1);
  border-radius: 10rpx;
  margin-bottom: 16rpx;
  padding: 20rpx;
  box-sizing: border-box;
}
.border_style {
  border: 2rpx solid rgba(255, 99, 36, 1);
  background: #fffbfa;
}
.top_img {
  width: 130rpx;
  height: 130rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.top_img .img1 {
  width: 102rpx;
  height: 102rpx;
}
.top_content {
  width: 300rpx;

  height: 120rpx;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
}
.top_content .p1 {
  width: 100%;
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
}
.top_content .p2 {
  width: 100%;
  height: 50rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.p3 .txt1 {
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
}
.txt2 {
  width: 120rpx;
  height: 30rpx;
  border: 1rpx solid rgba(92, 198, 130, 1);
  border-radius: 4rpx;
  margin-left: 15rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(92, 198, 130, 1);
  text-align: center;
}

/* 底部按钮样式 */
.bottom {
  // 底部按钮定位
  position: fixed;
  bottom: 0;
  left: 0;
  z-index: 9999;
  width: 100%;
  height: 98rpx;
  background: rgba(255, 255, 255, 1);
  // margin-top: 48rpx;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  padding-left: 30rpx;
  padding-right: 20rpx;
  box-sizing: border-box;
}
.tel {
  width: 42rpx;
  height: 42rpx;
  margin-right: 20rpx;
}
.orange1 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
  margin-right: 30rpx;
}
.orange {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
  width: 70px;

  display: flex;
  align-items: center;
}
.bottom .btn {
  width: 240rpx;
  height: 75rpx;
  background: rgba(255, 99, 36, 1);
  border-radius: 38rpx;
  font-size: 32rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 255, 255, 1);
  text-align: center;
  line-height: 75rpx;
}
.down {
  width: 32rpx;
  height: 32rpx;
  margin-right: 10rpx;
}
.van-top {
  text-align: center;
  margin-top: 20rpx;
}
.van-title {
  color: #000000;
  font: 32rpx/2 'SourceHanSansCN-Medium';
}
.van-title1 {
  color: #9a9a9a;
  font: 25rpx/1 'SourceHanSansCN-Regular';
}
.top-content {
  display: flex;
  justify-content: space-between;
  padding: 30rpx;
}
.content-name {
  color: #343434;
  font: 28rpx/1 'SourceHanSansCN-Regular';
}
.content-value {
  color: #343434;
  font: 600 28rpx/1 'SourceHanSansCN-Medium';
}
.content-value1 {
  color: #ff6424;
  font: 600 28rpx/1 'SourceHanSansCN-Medium';
}
.else_img_one {
  width: 100%;
  height: 260rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  border: 1rpx solid #f3f3f3;
  img {
    width: 164rpx;
    height: 129rpx;
  }
  text {
    font-size: 28rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(136, 136, 136, 1);
  }
}
.selectedImg {
  position: absolute;
  right: -10rpx;
  top: -5rpx;
  width: 66rpx;
  height: 66rpx;
  z-index: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}
.selectedImg .img {
  width: 36rpx;
  height: 36rpx;
}
>>> .van-stepper__minus {
  border-radius: 50%;
  background: #fff;
  color: #adadad;
  border: 2rpx solid rgba(237, 237, 237, 1);
  box-shadow: 0 6rpx 16rpx 0 rgba(211, 211, 211, 0.6);
}
>>> .van-stepper__input {
  background: none;
  font-size: 32rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(63, 63, 63, 1);
}
>>> .van-stepper__plus {
  border-radius: 50%;
  background: rgba(255, 99, 36, 0.9);
  // box-shadow:0px 6rpx 16rpx 0rpx rgba(255,99,36,0.68);
  color: #fff;
}
</style>