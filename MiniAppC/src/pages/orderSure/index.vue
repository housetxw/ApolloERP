<template>
  <div class="demo_page">
    <div class="box" v-if="showHomeAndShop&&!isFromStore">
      <div style="width:100%;">
        <p class="p1" style="display:flex;align-items:center;">
          <img src="https://m.aerp.com.cn/mini-app-main/serviceTypeIcon.png" alt style="width:32rpx;height:32rpx;margin-right:12rpx;" mode="aspectFit" />
          服务方式
        </p>
        <div class="serviceType">
          <p class="serviceP" @click="serviceCheck1">
            <img :src="serviceCheck==1?'https://m.aerp.com.cn/mini-app-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-app-main/maintenance_radio.png'" alt class="roundSize" mode="aspectFit" />
            <text class="user">上门服务</text>
          </p>
          <p class="serviceP" @click="serviceCheck2">
            <img :src="serviceCheck==2?'https://m.aerp.com.cn/mini-app-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-app-main/maintenance_radio.png'" alt class="roundSize" mode="aspectFit" />
            <text class="user">到店服务</text>
          </p>
        </div>
      </div>
    </div>
    <!-- 收货人信息 -->

    <div v-if="serviceCheck==2 && showHomeAndShop" style="margin-top:16rpx">
      <div class="top">
        <div class="customer">
          <img src="https://m.aerp.com.cn/mini-app-main/ordercheck_user_icon.png" alt class="img1" mode="aspectFit" />
          <text class="user">{{receiverName}}</text>
        </div>
        <div class="customer">
          <img src="https://m.aerp.com.cn/mini-app-main/ordercheck_iphone_icon.png" alt class="img1" mode="aspectFit" />
          <text class="user">{{receiverPhone}}</text>
          <!-- <img src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png" alt class="img" /> -->
        </div>
      </div>

      <!-- 配送到门店信息 -->
      <div class="top_first">
        <div class="top_first_left">
          <p class="top_first_p1">{{storeArr.simpleName}}</p>
          <p class="top_first_p2">{{storeArr.address}}</p>
        </div>
        <!-- <div class="top_first_right">
        <img src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png" alt class="img" />
        </div>-->
      </div>
    </div>
    <!-- 上门服务 -->
    <div class="homeDetail" v-if="serviceCheck==1 && showHomeAndShop" @click="toSelectAddress" style="margin-top:16rpx">
      <div class="goHome" v-if="homeAddress">
        <div class="top1">
          <div class="customer">
            <img src="https://m.aerp.com.cn/mini-app-main/ordercheck_user_icon.png" alt class="img1" mode="aspectFit" />
            <text class="user">{{homeAddress.userName}}</text>
          </div>
          <div class="customer">
            <img src="https://m.aerp.com.cn/mini-app-main/ordercheck_iphone_icon.png" alt class="img1" mode="aspectFit" />
            <text class="user">{{homeAddress.mobileNumber}}</text>
          </div>
        </div>
        <div class="customer">
          <img src="https://m.aerp.com.cn/mini-app-main/city_location_icon1.png" alt class="img1" mode="aspectFit" />
          <text class="user">上门地址：{{homeAddress.province}}{{homeAddress.district}}{{homeAddress.city}}{{homeAddress.addressLine}}</text>
        </div>
      </div>
      <div v-else @click="toSelectAddress">请选择您的上门服务地址</div>
      <img src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png" alt style="width:40rpx;height:40rpx;" mode="aspectFit" />
    </div>
    <div class="box" v-if="serviceCheck==1 && showHomeAndShop">
      <div style="width:100%;">
        <p class="p1" style="display:flex;align-items:center;">
          <img src="https://m.aerp.com.cn/mini-app-main/serviceTypeIcon.png" alt style="width:32rpx;height:32rpx;margin-right:12rpx;" mode="aspectFit" />
          预约上门服务日期:
          <span style="color:#666;">（选填）</span>
        </p>
        <div>
          <div style=" min-height: 80rpx;white-space:nowrap;padding-top:15rpx;position:relative;">
            <scroll-view class="scrollContainer" scroll-x="true">
              <div class="scrollDate" v-for="(itemx,indexx) in productsTime" :key="indexx" @click="checktime(itemx,indexx)" :class="['scrollDate',{'scrollDate1':itemx.isHighLight?true:false}]">
                <p>{{itemx.reserveDate}} {{itemx.reserveWeekDay}}</p>
                <p>{{itemx.reserveTime}}</p>
                <img v-if="itemx.isHighLight" src="https://m.aerp.com.cn/mini-app-main/maintenance_radio_click.png" class="roundSize1" mode="aspectFit" />
              </div>
            </scroll-view>
            <div class="moreDate" @click="toCalender">
              <p>更多</p>
              <p class="p1">日期</p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- 一个套餐的商品 -->
    <div class="top_onecontent" v-if="products.length > 0">
      <div class="topContent" v-for="(item3,index3) in products" :key="index3">
        <!-- <p class="p1">{{item3.packageOrProduct.displayName}}</p> -->
        <p class="p1">{{item3.packageOrProduct.productName}}</p>
        <div class="p2">
          <text class="txt1">￥{{item3.packageOrProduct.price}}</text>
          <div class="add">
            <van-stepper v-model="item3.packageOrProduct.number" @change="countChange" :data-index="index3" v-if="isShowStep" />
          </div>
        </div>

        <div class="set_meal_shop" v-for="(itema,indexa) in item3.packageProducts" :key="indexa" v-if="item3.packageProducts.length > 0">
          <img :src="itema.imageUrl" alt class="set_meal_shopimg" mode="aspectFit" />
          <div class="set_meal_shopbox">
            <div class="set_meal_shoptop">{{itema.productName}}</div>
            <div class="set_meal_shopbottom">
              <text v-if="itema.label != ''" style="width:87rpx;height:30rpx;line-height:30rpx;text-align:center;border:1rpx solid rgba(92,198,130,1);font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(92,198,130,1);">{{itema.label}}</text>
              <text v-else style="width:87rpx;height:30rpx;line-height:30rpx;text-align:center;font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;"></text>
              <p style="font-size:28rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(255,99,36,1);">
                <!-- ¥{{itema.price}} -->
                <text class="span_num">x{{itema.number}}</text>
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="top_onecontent" v-if="serviceCheck==1 && showHomeAndShop ">
      <div class="topContent">
        <p class="p1">{{doorToDoorService.productName}}</p>
        <div class="p2">
          <text class="txt1">￥{{doorToDoorService.totalAmount}}</text>
          <div class="add">x {{doorToDoorService.number}}</div>
        </div>
      </div>
    </div>
    <!-- 服务 -->
    <div class="free" v-if="services.length > 0">
      <p class="p1">服务</p>
      <div class="p2" v-for="(item1,index1) in services" :key="index1">
        <p class="p3">
          <img :src="item1.imageUrl" alt class="img1" mode="aspectFit" />
          <text class="txt1">{{item1.displayName}}</text>
        </p>
        <p class="p4">
          <text class="txt1">￥{{item1.price}}</text>
          <text class="txt2">x{{item1.number}}</text>
        </p>
      </div>
    </div>
    <!-- 支付方式，优惠券 -->
    <div class="pay_box">
      <div class="pay_box_one" @tap="payFunction">
        <p class="pay_box_p1">支付方式</p>
        <p class="pay_box_p2">
          <text class="txt1" v-if="payment == 1">微信支付</text>
          <text class="txt1" v-if="payment == 2">现场支付</text>
          <img src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png" alt class="img" mode="aspectFit" />
        </p>
      </div>
      <div class="pay_box_one" @tap="cuoponClick">
        <p class="pay_box_p1">优惠券</p>
        <p class="pay_box_p2">
          <text class="txt1">{{userCouponDisplayName || '无适用优惠券'}}</text>
          <img src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png" alt class="img" mode="aspectFit" />
        </p>
      </div>
      <!-- <div class="pay_box_one" @tap="invoiceClick">
        <p class="pay_box_p1">发票</p>
        <p class="pay_box_p2">
          <text class="txt1" v-if="isNeedInvoice">需要开票</text>
          <text class="txt1" v-else>无需开票</text>
          
          <img src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png" alt class="img" />
        </p>
      </div>-->
    </div>
    <!-- 各种服务费 -->
    <div class="pay_box_money">
      <div class="pay_box_moneyone">
        <p class="pay_box_money_p1">服务总价</p>
        <p class="pay_box_money_p2">¥{{shopAllMOney}}</p>
      </div>
      <div class="pay_box_moneyone">
        <p class="pay_box_money_p1">服务费</p>
        <p class="pay_box_money_p2">+¥{{serviceCharge}}</p>
      </div>
      <div class="pay_box_moneyone" v-if="serviceCheck==1 && showHomeAndShop">
        <p class="pay_box_money_p1">上门服务费</p>
        <p class="pay_box_money_p2">+¥{{doorToDoorService.totalAmount}}</p>
      </div>
      <div class="pay_box_moneyone">
        <p class="pay_box_money_p1">运费</p>
        <p class="pay_box_money_p2">+¥{{freight}}</p>
      </div>
      <div class="pay_box_moneyone">
        <p class="pay_box_money_p1">促销</p>
        <p class="pay_box_money_count">-¥{{diamondsDiscountAmount}}</p>
      </div>
      <div class="pay_box_moneyone">
        <p class="pay_box_money_p1">优惠</p>
        <p class="pay_box_money_count">-¥{{concession}}</p>
      </div>
    </div>
    <!-- 按钮 -->
    <div class="button_btn">
      <div class="one">
        <p class="one_one">
          实付款：
          <text>¥{{actualAmount}}</text>
        </p>
        <!-- <p class="one_two">
          {{btnText}}
        </p>-->
      </div>
      <div class="two" @tap="submissionOrder">提交订单</div>
    </div>
    <!-- 选择支付方式弹框 -->
    <div style="width:100%" v-if="showpay">
      <van-popup :show="showpay" closeable @close="onClose" position="bottom" lock-scroll custom-style="height: 40%;">
        <div class="popup_header">支付方式</div>
        <van-cell-group>
          <van-cell :title="item.title" clickable @tap="clearClickProup(item.id)" v-for="(item,index) in paymentname" :key="index"></van-cell>
        </van-cell-group>
        <div class="popup_btn">
          <button @tap="onClose">取消</button>
        </div>
      </van-popup>
    </div>
  </div>
</template>
<script>
import {
  PostFirstOrder,
  PostOrder,
  PostOrderMoney,
  ConfirmPay,
  ClosePay,
  GetOrderServiceType,
  GetUserAddres,
  GetShopReserveDateTime
} from '../../api'

import eventBus from '../../utils/eventBus.js'

let OrtehrproductInfos = []
export default {
  data() {
    return {
      code: '',
      isFromStore: false,
      diamondsDiscountAmount: 0.0,
      hasEventBus: false,
      selectTime: '',
      installType: 0,
      showHomeAndShop: true,
      homeAddress: null,
      doorToDoorService: {},
      showpay: false,
      productsTime: [],
      serviceCheck: 1,
      canPay: true,
      // invoice:'',//发票
      userCouponId: 0, // 选择的用户券id  默认为0
      storeArr: {}, // 上个页面传过来的门店信息
      cardid: '', // 上个页面传过来的车id
      storeid: 0, // 上个页面传过来的门店id
      productInfos: [], // 上个页面传过来的商品数组
      pids:[],// 上个页面传过来的PID数组
      shopAllMOney: '', // 商品总价
      serviceCharge: '', // 服务费
      freight: '', // 运费
      concession: '', // 优惠
      payment: '', // 支付方式
      isNeedInvoice: '', // 是否需要发票
      receiverName: '', // 收货人姓名
      receiverPhone: '', // 收货人电话
      userCouponDisplayName: '', // 优惠券名称
      actualAmount: '', // 实付款
      orderType: '', // 判断是轮胎还是保养服务的订单
      services: [], // 服务数组
      products: [], // 整体数组
      allApplicableCoupons: [], // 用户可用优惠券数组
      canToShop: false,
      canToHome: false,
      paymentname: [
        {
          id: 1,
          title: '微信支付'
        },
        {
          id: 2,
          title: '现场支付'
        }
      ]
    }
  },
  computed: {
    isShowStep() {
      return this.code === ''
    }
  },
  methods: {
    getShopReserveDateTime() {
      GetShopReserveDateTime({ ShopId: this.storeid }).then(res => {
        this.productsTime = res.data
      })
    },
    toCalender() {
      this.$router.push('/pages/calender/main')
    },
    toSelectAddress() {
      this.$router.push('/pages/addressManagement/main')
    },
    onClose() {
      this.showpay = false
    },
    getOrderServiceType(ProductType, OrderType, pids) {
      GetOrderServiceType({ ProductType, OrderType, pids }).then(res => {
        this.canToShop = res.data.arrivalToShop
        this.canToHome = res.data.doorToDoor
        // if (this.serviceCheck == 2 && this.canToHome) {
        //   wx.showModal({
        //     title: '温馨提示',
        //     content: '此类商品可提供上门服务，是否需要技师上门保养？',
        //     success(res) {
        //       if (res.confirm) {
        //         that.serviceCheck1()
        //       } else if (res.cancel) {
        //       }
        //     }
        //   })
        // }
      })
    },
    confirmPay(orderNo, amount) {
      if (!this.canPay) {
        return false
      }
      this.canPay = false
      let footValue = {
        innerNoType: 'OrderNo',
        orderNo,
        buyerAccount: this.globalData.openId,
        amount
      }
      ConfirmPay(footValue)
        .then(res => {
          let that = this
          let {
            timeStamp,
            nonceStr,
            signType,
            paySign
          } = res.data.wxMiniAppPayCallData
          let package1 = res.data.wxMiniAppPayCallData.package
          let PayNo = res.data.payNo
          if (res.data.arousePayment) {
            wx.requestPayment({
              timeStamp,
              nonceStr,
              package: package1,
              signType,
              paySign,
              success(res1) {
                that.canPay = true
                wx.reLaunch({
                  url: '/pages/orderDetailsTwo/main?orderNo=' + res.data.orderNo
                })
              },
              fail(res) {
                ClosePay({ PayNo }).then(() => {
                  that.canPay = true
                  wx.reLaunch({
                    url: '/pages/allOrders/main'
                  })
                })
              }
            })
          } else {
            wx.reLaunch({
              url: '/pages/orderDetailsTwo/main?orderNo=' + res.data.orderNo
            })
          }
        })
        .catch(err => {
          this.canPay = true
          wx.showModal({
            title: '提示',
            content: '支付出现异常,请尝试重新下单,该操作不会影响您的资金安全',
            success(res) {
              if (res.confirm) {
                wx.switchTab({
                  url: '/pages/index/main'
                })
              } else if (res.cancel) {
              }
            }
          })
        })
    },
    clearClickProup(id) {
      this.payment = id
      this.showpay = false
      console.log('payment', this.payment)
    },
    serviceCheck1() {
      if (this.canToHome) {
        this.serviceCheck = 1
        this.installType = 2
        OrtehrproductInfos = []
        this.getPageIndexInfor(5, this.globalData.OrderType)
      } else {
        wx.showToast({ title: '该商品不支持上门服务', icon: 'none' })
      }
    },
    serviceCheck2() {
      if (this.canToShop) {
        this.serviceCheck = 2
        this.installType = 1
        OrtehrproductInfos = []
        this.getPageIndexInfor(0, this.globalData.OrderType)
      } else {
        wx.showToast({ title: '该商品不支持上门服务', icon: 'none' })
      }
    },
    // 提交订单
    submissionOrder() {
      // if (this.installType == 2 && this.homeAddress == null) {
      //   wx.showToast({ title: '请添加您的上门地址', icon: 'none' })
      //   return false
      // }
      let that = this

      let receiverName = that.receiverName
      let receiverPhone = that.receiverPhone
      let receiverAddressId = 0
      console.log(5566, this.installType)
      if (this.installType == 2) {
        if (this.homeAddress == null) {
          receiverName = ''
          receiverPhone = ''
          receiverAddressId = 0
        } else {
          receiverName = this.homeAddress.userName || ''
          receiverPhone = this.homeAddress.mobileNumber || ''
          receiverAddressId = this.homeAddress.addressId || 0
        }
      }

      let orderSubmit = {
        code: that.code,
        orderType: that.globalData.OrderType,
        produceType: that.globalData.ProductType,
        carId: that.cardid, // "a380dba1-8046-4c8b-89ba-7d79a3f2b62c"
        shopId: that.storeid, // 3
        deliveryType: 1, // 只有1
        receiverName,
        receiverPhone,
        receiverAddressId,
        productInfos: OrtehrproductInfos,
        payment: that.payment,
        userCouponId: that.userCouponId,
        isNeedInvoice: true,
        installType: that.installType,
        reserverTime: that.selectTime
      }
      PostOrder(orderSubmit)
        .then(res => {
          if (res.code == 10000) {
            if (that.payment == 1) {
              debugger
              that.confirmPay(res.data.orderNo, this.actualAmount)
              wx.showToast({ title: '提交成功', icon: 'none' })
            }
            if (that.payment == 2) {
              wx.reLaunch({
                url: '/pages/orderDetailsTwo/main?orderNo=' + res.data.orderNo
              })
            }
            wx.showToast({ title: '提交成功', icon: 'none' })
          } else {
            wx.showToast({ title: res.code, icon: 'none' })
          }
        })
        .catch(err => {})
    },
    // 支付方式
    payFunction() {
      this.showpay = true
    },
    // 优惠券
    cuoponClick() {
      this.$router.push({
        path: '/pages/coupon/main',
        query: {
          isOrder: 'yes',
          userCouponId: JSON.stringify(this.allApplicableCoupons)
        }
      })
    },
    // 发票
    invoiceClick() {
      let that = this
      // wx.showModal({
      //   title: '是否需要发票',
      //   // content: '确定提交预约请求？',
      //   success (res) {
      //     if (res.confirm) {
      //       that.invoice = '需要发票'
      //     } else if (res.cancel) {
      //       that.invoice = '无需发票'
      //     }
      //   }
      // })
    },
    // 选择预约时间
    checktime(itemx, indexx) {
      console.log(itemx)
      let itemIsHighLight = itemx.isHighLight
      this.productsTime.forEach(element => {
        element.isHighLight = false
      })
      itemx.isHighLight = !itemIsHighLight
      this.selectTime = `${itemx.year}/${itemx.reserveDate} ${
        itemx.reserveTime
      }`
    },
    countChange(e) {
      console.log(`商品的值`, e)
      var index = e.currentTarget.dataset.index // 获取当前点击事件的下标索引
      console.log(index)
      // var itemb = e.currentTarget.dataset.item
      // itemb.count = e.mp.detail
      // this.count = e.mp.detail
      OrtehrproductInfos[index].number = e.mp.detail
      console.log('count', OrtehrproductInfos[index].number)
      this.getMoney()
    },
    // 试算金额
    getMoney(userCouponId) {
      let that = this
      let footValue = {
        orderType: that.globalData.OrderType,
        produceType: that.globalData.ProductType,
        carId: that.cardid,
        shopId: that.storeid,
        deliveryType: 1,
        receiverName: 'string',
        receiverPhone: 'string',
        receiverAddressId: 0,
        productInfos: OrtehrproductInfos,
        payment: 1,
        userCouponId: userCouponId,
        isNeedInvoice: true
      }
      PostOrderMoney(footValue)
        .then(res => {
          that.actualAmount = res.data.actualAmount.toFixed(2)
          that.shopAllMOney = res.data.totalProductAmount.toFixed(2)
          that.serviceCharge = res.data.serviceFee.toFixed(2)
          that.freight = res.data.deliveryFee.toFixed(2)
          that.concession = res.data.totalCouponAmount.toFixed(2)
          that.diamondsDiscountAmount = res.data.diamondsDiscountAmount.toFixed(
            2
          )
        })
        .catch(err => {})
    },
    // 获取首次加载订单确认页信息
    getPageIndexInfor(produceType, orderType) {
      let that = this
      let orderdata = {
        carId: that.cardid,
        shopId: that.storeid,
        productInfos: that.productInfos,
        produceType,
        orderType
      }
      PostFirstOrder(orderdata)
        .then(res => {
          that.userCouponId = res.data.userCouponId
          that.shopAllMOney = res.data.totalProductAmount.toFixed(2)
          that.serviceCharge = res.data.serviceFee.toFixed(2)
          that.freight = res.data.deliveryFee.toFixed(2)
          that.concession = res.data.totalCouponAmount.toFixed(2)
          that.diamondsDiscountAmount = res.data.diamondsDiscountAmount.toFixed(
            2
          )
          that.payment = res.data.payment
          that.isNeedInvoice = res.data.isNeedInvoice
          if (res.data.doorToDoorService) {
            that.doorToDoorService = res.data.doorToDoorService
            that.doorToDoorService.totalAmount = that.doorToDoorService.totalAmount.toFixed(
              2
            )
          }

          that.receiverName = res.data.receiverName
          that.receiverPhone = res.data.receiverPhone
          that.userCouponDisplayName = res.data.userCouponDisplayName
          that.actualAmount = res.data.actualAmount.toFixed(2)
          that.allApplicableCoupons = res.data.allApplicableCoupons // 用户优惠券数组
          that.services = res.data.services // 服务数组
          let arr = res.data.products
          arr.forEach(element => {
            element.packageOrProduct.price = element.packageOrProduct.price.toFixed(
              2
            )
            element.packageProducts.forEach(itemab => {
              itemab.price = itemab.price.toFixed(2)
            })
          })
          that.products = arr // 接收整体数组

          res.data.products.forEach(item => {
            item.pid = item.packageOrProduct.productId
            item.number = item.packageOrProduct.number
            item.productOwnAttri = item.packageOrProduct.productOwnAttri
            OrtehrproductInfos.push(item)
          })
        })
        .catch(err => {})
    }
  },
  mounted() {
    console.log(112233334, this.globalData.OrderType)
    let that = this
    eventBus.$on('selectCoupon', item => {
      that.userCouponDisplayName = item.displayName
      that.userCouponId = item.userCouponId
      that.getMoney(that.userCouponId)
    })
    // eventBus.$on('selectTime', item => {
    //   console.log(3311, item)
    //   this.productsTime.forEach(element => {
    //     element.isHighLight = false
    //   })
    //   this.productsTime.unshift(item)
    //   this.selectTime = `${item.year}/${item.reserveDate} ${item.reserveTime}`
    // })
    that.getPageIndexInfor(
      this.globalData.ProductType,
      this.globalData.OrderType
    )
  },
  onShow() {
    this.homeAddress = this.globalData.homeAddress

    if (this.globalData.reservedTime) {
      this.productsTime.forEach(element => {
        element.isHighLight = false
      })
      this.productsTime.unshift(this.globalData.reservedTime)
      this.selectTime = `${this.globalData.reservedTime.year}/${
        this.globalData.reservedTime.reserveDate
      } ${this.globalData.reservedTime.reserveTime}`
    }
  },
  onLoad(options) {
    this.cardid = options.cardID
    this.storeid = options.storeid
    this.code = options.code
    this.userCouponId = options.userCouponId
    this.pids = JSON.parse(options.pids)
    // this.shopPid = options.shopPid
    this.productInfos = JSON.parse(options.productInfos)
    this.storeArr = JSON.parse(options.storeArr) || ''
    // this.shopNum = options.shopNum
    this.orderType = options.orderType
    this.isFromStore = options.isFromStore
    if (this.globalData.serviceType == 'toShop') {
      this.serviceCheck = 2
      this.installType = 1
    } else {
      this.serviceCheck = 1
      this.installType = 2
    }
    if (this.globalData.ProductType == 6 || this.globalData.ProductType == 14) {
      this.showHomeAndShop = false
      this.installType = 0
      this.paymentname = [
        {
          id: 1,
          title: '微信支付'
        }
      ]
    }
    this.getOrderServiceType(
      this.globalData.ProductType,
      this.globalData.OrderType,
      this.pids
    )
    if (this.globalData.ProductType !== 14) {
      GetUserAddres('')
        .then(res => {
          if (res.data.length > 0) {
            this.globalData.homeAddress = res.data[0]
            this.homeAddress = res.data[0]
          } else {
            this.globalData.homeAddress = null
            this.homeAddress = null
          }
        })
        .catch(err => {})
      this.getShopReserveDateTime()
    }
  },
  onUnload() {
    this.canPay = true
    eventBus.$off('selectCoupon')
    this.userCouponId = 0
    this.services = []
    OrtehrproductInfos = []
    this.selectTime = ''
  },
  onHide() {
    this.globalData.reservedTime = null
  }
}
</script>
<style scoped lang="scss">
.popup_header {
  width: 100%;
  text-align: center;
  font-weight: bold;
  line-height: 100rpx;
  font-size: 35rpx;
}
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #f3f3f3;
  padding-bottom: 100rpx;
  box-sizing: border-box;
}
.top {
  width: 690rpx;
  min-height: 82rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx 10rpx 0 0;

  padding: 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  // margin-top: 16rpx;
}
.goHome {
  width: 580rpx;
  display: flex;
  flex-direction: column;
}
.homeDetail {
  width: 690rpx;
  display: flex;
  border-radius: 10rpx;
  padding: 28rpx;
  box-sizing: border-box;
  align-items: center;
  background: #fff;
  // margin-top: 16rpx;
  min-height: 82rpx;
  border-bottom: 1rpx solid #ebebeb;
  justify-content: space-between;
}
.top1 {
  background: rgba(255, 255, 255, 1);
  padding: 10rpx 120rpx 20rpx 10rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.customer {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: center;
  .img1 {
    width: 32rpx;
    height: 32rpx;
  }
  .img {
    width: 24rpx;
    height: 24rpx;
    margin-left: 10rpx;
  }
  .user {
    font-size: 30rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(33, 33, 33, 1);
    margin-left: 10rpx;
  }
}
.top_first {
  width: 690rpx;
  min-height: 140rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 0rpx 0rpx 10rpx 10rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  padding: 0 28rpx 28rpx;

  box-sizing: border-box;
  // margin-bottom: 16rpx;

  .top_first_left {
    width: 510rpx;
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: flex-start;
    .top_first_p1 {
      font-size: 30rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(33, 33, 33, 1);
    }
    .top_first_p2 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(166, 165, 174, 1);
    }
  }
  .top_first_right {
    width: 100rpx;
    display: flex;
    flex-direction: row;
    justify-content: flex-end;
    .img {
      width: 24rpx;
      height: 24rpx;
    }
  }
}
// 图片集合
.top_two {
  position: relative;
  padding-left: 30rpx;
  padding-right: 30rpx;
  box-sizing: border-box;
  width: 690rpx;
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
  .img_img {
    width: 24rpx;
    height: 24rpx;
  }
  .position {
    position: absolute;
    top: 26rpx;
    right: 76rpx;
    width: 110rpx;
    height: 110rpx;
    background: rgba(0, 0, 0, 1);
    text-align: center;
    line-height: 110rpx;
    opacity: 0.5;
    font-size: 26rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(255, 255, 255, 1);
  }
}
/* 一个套餐样式 */
.top_title {
  width: 690rpx;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  height: 180rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  margin-bottom: 16rpx;
  margin-top: 16rpx;
  padding: 20rpx;
  box-sizing: border-box;
  .top_img {
    width: 130rpx;
    height: 130rpx;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    .img1 {
      width: 102rpx;
      height: 102rpx;
    }
  }
  .top_content {
    width: 500rpx;
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: flex-start;
    .p1 {
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
    .p2 {
      width: 100%;
      height: 50rpx;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      .p3 {
        .txt1 {
          font-size: 28rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(255, 99, 36, 1);
        }
      }
      .add {
        min-width: 200rpx;
      }
    }
  }
}
.top_onecontent {
  margin-bottom: 16rpx;
  width: 690rpx;
  // height:180rpx;
  // background: rgba(255, 255, 255, 1);
  display: flex;
  flex-direction: column;
  // justify-content: space-between;
  align-items: flex-start;
  // padding: 20rpx 28rpx 20rpx 28rpx;
  // box-sizing: border-box;
  .p1 {
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
  .p2 {
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    margin: 25rpx 0;
    .txt1 {
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(255, 99, 36, 1);
    }

    .add {
      // width: 200rpx;
      color: #999;
      font-size: 30rpx;
    }
  }
  .topContent {
    width: 100%;
    padding: 20rpx 28rpx 20rpx 28rpx;
    background: rgba(255, 255, 255, 1);
    box-sizing: border-box;
    margin-top: 20rpx;
    border-radius: 10rpx;
  }
  .set_meal_shop {
    width: 100%;
    height: 170rpx;
    background: rgba(246, 246, 246, 1);
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    padding: 0 30rpx;
    box-sizing: border-box;
    // margin-top: 27rpx;
    .set_meal_shopimg {
      width: 110rpx;
      height: 110rpx;
    }
    .set_meal_shopbox {
      width: 490rpx;
      /* height: 102rpx; */
      margin-left: 20rpx;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-start;
      .set_meal_shoptop {
        height: 76rpx;
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
        overflow: hidden;
      }
      .set_meal_shopbottom {
        width: 100%;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
        .span_num {
          font-size: 24rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(136, 136, 136, 1);
          margin-left: 5rpx;
        }
      }
    }
  }
}
.pay_box {
  width: 690rpx;
  display: flex;
  flex-direction: column;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  padding: 0 28rpx;
  box-sizing: border-box;
  // margin-top: 16rpx;
  margin-bottom: 16rpx;
  .pay_box_one {
    width: 100%;
    height: 100rpx;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .pay_box_p1 {
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 500;
      color: rgba(51, 51, 51, 1);
    }
    .pay_box_p2 {
      .txt1 {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
      }
      .img {
        width: 24rpx;
        height: 24rpx;
        margin-left: 15rpx;
      }
    }
  }
}
.pay_box_money {
  width: 690rpx;
  display: flex;
  flex-direction: column;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  padding: 0 28rpx;
  box-sizing: border-box;
  margin-bottom: 16rpx;
  .pay_box_moneyone {
    width: 100%;
    height: 100rpx;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .pay_box_money_p1 {
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
    .pay_box_money_p2 {
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(255, 99, 36, 1);
    }
    .pay_box_money_count {
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
  }
}
// 底部按钮
.button_btn {
  position: fixed;
  bottom: 0;
  left: 0;
  width: 750rpx;
  height: 98rpx;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  padding: 30rpx;
  box-sizing: border-box;
  .one {
    .one_one {
      font-size: 28rpx;
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
    .one_two {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(173, 173, 173, 1);
      text {
        font-size: 30rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 99, 36, 1);
      }
    }
  }
  .two {
    width: 240rpx;
    height: 75rpx;
    background: rgba(255, 99, 36, 1);
    border-radius: 38rpx;
    text-align: center;
    line-height: 75rpx;
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(255, 255, 255, 1);
  }
}
.free {
  width: 690rpx;
  // height:530rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  padding: 30rpx;
  box-sizing: border-box;
  margin: 16rpx 0;
  .p1 {
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: bold;
    color: rgba(51, 51, 51, 1);
  }
  .p2 {
    width: 100%;
    height: 120rpx;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    // border-bottom: 1rpx solid #f3f3f3;
    .p3 {
      display: flex;
      flex-direction: row;
      justify-content: center;
      align-items: center;
      .img1 {
        width: 55rpx;
        height: 55rpx;
      }
      .txt1 {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
        margin-left: 20rpx;
      }
    }
    .p4 {
      .txt1 {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        // text-decoration:line-through;
        color: rgba(136, 136, 136, 1);
      }
      .txt2 {
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
        margin-left: 10rpx;
      }
    }
  }
}
.oneMealShop {
  width: 100%;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  margin-bottom: 20rpx;
  padding: 20rpx 28rpx 20rpx 28rpx;
  box-sizing: border-box;
}

.serviceType {
  display: flex;
  justify-content: space-between;
  color: #333;
  width: 100%;
  padding: 40rpx 30rpx 10rpx 0;

  margin-bottom: 10rpx;
}
.serviceP {
  display: flex;
  align-items: center;
}
.roundSize {
  width: 40rpx;
  height: 40rpx;
  margin: 0 20rpx 0 0;
}
.box {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
  min-height: 180rpx;
  background: #fff;
  padding: 20rpx 80rpx 30rpx 30rpx;

  margin: 16rpx 0 0 0;
  padding-bottom: 0;
  box-sizing: border-box;
  width: 690rpx;

  border-radius: 10rpx;

  .p1 {
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: #333;
  }
}
.scrollContainer {
  width: 100%;
}
.scrollDate {
  width: 175rpx;

  border: 1rpx solid #ff6224;
  color: #ff6224;
  text-align: center;
  font: 28rpx/1 '思源黑体 CN Regular';
  padding: 20rpx;
  display: inline-block;
  padding: 15rpx 10rpx;
  display: inline-block;
  border-radius: 20rpx;
  margin-right: 20rpx;
  box-sizing: border-box;
}
.moreDate {
  color: #ff6224;
  font: 28rpx/1 '\601D\6E90\9ED1\4F53 CN Regular';
  position: absolute;
  top: 3rpx;
  right: -70rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  border-left: 1rpx solid #eee;
  height: 100rpx;
  width: 60rpx;
  padding-left: 12rpx;
  align-items: center;
  box-shadow: 1rpx 0 0 0 0 rgba(153, 153, 153, 0.3);
  // box-shadow:0 1rpx 2rpx 0 rgba(153,153,153,0.3);
  .p1 {
    margin-top: 6rpx;
    color: #ff6224;
    font: 28rpx/1 '思源黑体 CN Regular';
  }
}
.scrollDate1 {
  background: #ffeee7;
  position: relative;
}
.roundSize1 {
  position: absolute;
  bottom: 6rpx;
  right: 6rpx;
  width: 30rpx;
  height: 30rpx;
}
.moreDate {
  color: #ff6224;
  font: 28rpx/1 '\601D\6E90\9ED1\4F53 CN Regular';
  position: absolute;
  top: 3rpx;
  right: -70rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  border-left: 1rpx solid #eee;
  height: 100rpx;
  width: 60rpx;
  padding-left: 12rpx;
  align-items: center;
  box-shadow: 1rpx 0 0 0 0 rgba(153, 153, 153, 0.3);
  // box-shadow:0 1rpx 2rpx 0 rgba(153,153,153,0.3);
  .p1 {
    margin-top: 6rpx;
    color: #ff6224;
    font: 28rpx/1 '思源黑体 CN Regular';
  }
}
.popup_btn {
  width: 100%;
  padding: 40rpx 0;
}
.popup_btn > button {
  width: 640rpx;
  height: 90rpx;
  background: #ff6324;
  margin: 0 auto;
  border-radius: 45rpx;
  color: #fff;
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
  box-shadow: 0px 6rpx 16rpx 0rpx rgba(255, 99, 36, 0.6);
  color: #fff;
}
</style>