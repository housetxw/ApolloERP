<template>
  <div class="demo_page">
    <div class="box">
      <!-- <div style="width:100%;">
        <p class="p1">服务类型</p>
        <div class="serviceType">
          <p class="serviceP">
            <img
              :src="serviceCheck==1?'https://m.aerp.com.cn/mini-app-main/roundCheck.png':'https://m.aerp.com.cn/mini-app-main/round.png'"
              alt
              class="roundSize"
            />
            <text class="user">上门服务</text>
          </p>
          <p class="serviceP">
            <img
              :src="serviceCheck==2?'https://m.aerp.com.cn/mini-app-main/roundCheck.png':'https://m.aerp.com.cn/mini-app-main/round.png'"
              alt
              class="roundSize"
            />
            <text class="user">到店服务</text>
          </p>
        </div>
      </div>-->
      <p class="p1">{{summary}}</p>
      <p class="p2">{{subSummary}}</p>
      <div class="title">{{displayOrderType}}</div>
    </div>
    <!-- 用户信息 -->
    <div class="top" v-if="productType==5">
      <div class="customer">
        <img src="https://m.aerp.com.cn/mini-app-main/ordercheck_user_icon.png" alt class="img1" />
        <text class="user">联系人:{{receiverName}}</text>
      </div>
      <div class="customer">
        <img src="https://m.aerp.com.cn/mini-app-main/ordercheck_iphone_icon.png" alt class="img1" />
        <text class="user">电话:{{receiverPhone}}</text>
      </div>
    </div>
    <!-- <div class="customer customer1"  v-if="productType==5&&reserverTime!=''"> -->
    <div class="customer customer1" v-if="productType==5">
      <img src="https://m.aerp.com.cn/mini-app-main/time.png" alt class="img1" />
      <span class="user">预约时间：{{reserverTime}}</span>
    </div>
    <div class="customer customer1" v-if="productType==5">
      <img src="https://m.aerp.com.cn/mini-app-main/address.png" alt class="img1" />
      <span class="user">上门服务地址：{{shopAddress}}</span>
    </div>
    <!-- 门店信息 -->
    <div class="shop_top_info" v-else>
      <div class="img">
        <img :src="store_information.img" alt class="img1" />
      </div>
      <div class="content">
        <p class="p1">{{store_information.title}}</p>
        <div class="bottom">
          <p class="p2">{{store_information.address}}</p>
          <p
            class="p3"
            @click.stop="navigate(store_information.latitude, store_information.longitude)"
          >
            <img
              src="https://m.aerp.com.cn/mini-app-main/order_navigation_icon.png"
              alt
              class="img2"
            />
            <span class="txt1">{{store_information.distence}}KM</span>
          </p>
        </div>
      </div>
    </div>
    <!-- 车辆信息 -->
    <div class="one">
      <p class="p1">车辆信息</p>
      <p class="p2">{{userCar}}</p>
      <p class="p3">{{cartext}}</p>
    </div>
    <div class="one1" v-if="recommendProductInfos.length>0">
      <p class="p1">推荐商品</p>
      <div
        v-for="(itemm,indexx) in recommendProductInfos"
        v-if="recommendProductInfos.length>0"
        v-show="indexx<num"
      >
        <div class="allProduct">
          <img :src="itemm.image" alt class="productImg" v-if="itemm.image!=''" />
          <img
            src="https://m.aerp.com.cn/mini-app-main/produceNo.png"
            alt
            mode="widthFix"
            v-else
            class="productImg"
          />

          <div class="productDes">
            <p class="d1">{{itemm.displayName}}</p>
            <p class="d2">{{itemm.description}}</p>
            <p class="d3">¥{{itemm.price}}</p>
          </div>
          <p class="butt" @click="buy(itemm)">抢购</p>
        </div>
      </div>
      <div @click="showMore" class="moreList" v-if="recommendProductInfos.length>1">
        <span>{{moretxt}}</span>
        <img
          :src="isShow?'https://m.aerp.com.cn/mini-app-main/viewAll.png':'https://m.aerp.com.cn/mini-app-main/packUp.png'"
          alt
          class="moreImg"
        />
      </div>
    </div>
    <!-- 商品信息 -->
    <div class="shop" v-if="products.length > 0">
      <p class="p1">商品</p>
      <div class="shop_additionalClass" v-for="(item3,index3) in products" :key="index3">
        <div class="shop_meal_top">
          <div class="left">
            <img
              :src="item3.packageOrProduct.imageUrl"
              mode="widthFix"
              alt
              v-if="item3.packageOrProduct.imageUrl!=''"
            />

            <img src="https://m.aerp.com.cn/mini-app-main/produceNo.png" alt mode="widthFix" v-else />
          </div>
          <div class="right">
            <p class="right_p1">{{item3.packageOrProduct.productName}}</p>
            <p class="right_p2">{{item3.packageOrProduct.displayName}}</p>
            <p class="right_p3">
              <!--<text class="txt1_txt">套餐编号：待定</text>-->
              <text class="txt2_txt">￥{{item3.packageOrProduct.price}}</text>
              <span
                style="color:#333;margin-left:10rpx;"
                class="txt2_txt"
              >x{{item3.packageOrProduct.number}}</span>
            </p>
          </div>
        </div>
        <div class="shop_one" v-for="(item1,index1) in item3.packageProducts" :key="index1">
          <div class="img">
            <img :src="item1.imageUrl" mode="widthFix" alt class="img1" v-if="item1.imageUrl!=''" />

            <img src="https://m.aerp.com.cn/mini-app-main/produceNo.png" alt class="img1" v-else />
          </div>
          <div class="content">
            <p class="p1">{{item1.productName}}</p>
            <div class="bottom">
              <p class="p4">
                <text class="txt1">￥{{item1.price}}</text>
                <text class="txt2">x{{item1.number}}</text>
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
    <!-- 服务 -->
    <div class="free" v-if="services.length > 0">
      <p class="p1">服务</p>
      <div class="p2" v-for="(item4,index4) in services" :key="index4">
        <p class="p3">
          <img :src="item4.imageUrl" alt class="img1" v-if="item4.imageUrl!=''" />
          <img src="https://m.aerp.com.cn/mini-app-main/serviceIcon1.png" alt class="img1" v-else />

          <text class="txt1">{{item4.productName}}</text>
        </p>
        <p class="p4">
          <text class="txt1">￥{{item4.price}}</text>
          <text class="txt2">x{{item4.number}}</text>
        </p>
      </div>
    </div>
    <!-- 订单信息 -->
    <div class="order_info">
      <p class="p1">
        <text class="txt1">订单编号：</text>
        <text class="txt2">{{orderNo}}</text>
      </p>
      <p class="p1">
        <text class="txt1">订单时间：</text>
        <text class="txt2">{{orderTime}}</text>
      </p>
      <p class="p1">
        <text class="txt1">订单渠道：</text>
        <text class="txt2">{{displayOrderChannel}}</text>
      </p>
      <p class="p1">
        <text class="txt1">服务类型：</text>
        <text class="txt2">{{displayServiceCategory}}</text>
      </p>
      <p class="p1">
        <text class="txt1">支付方式：</text>
        <text class="txt2">{{displayPayMethod}}</text>
      </p>
      <!-- <p class="p1">
        <text class="txt1">配送方式：</text>
        <text class="txt2">{{displayDeliveryMethod}}</text>
      </p>-->
      <p class="p1">
        <text class="txt1">订单状态：</text>
        <text class="txt2">{{displayOrderStatus}}</text>
      </p>
      <p class="p1">
        <text class="txt1">客户姓名：</text>
        <text class="txt2">{{orderUser}}</text>
      </p>
      <p class="p1">
        <text class="txt1">客户电话：</text>
        <text class="txt2">{{orderPhone}}</text>
      </p>
    </div>
    <!-- 商品结算信息 -->
    <div class="shop_info">
      <p class="p1">
        <text class="txt1">商品金额</text>
        <text class="txt2">￥{{productAmount}}</text>
      </p>
      <p class="p1">
        <text class="txt1">服务金额</text>
        <text class="txt2">+￥{{serviceAmount}}</text>
      </p>
      <p class="p1" v-if="productType==5">
        <text class="txt1">上门服务费</text>
        <text class="txt2">+￥{{toHomePrice}}</text>
      </p>
      <p class="p1">
        <text class="txt1">运费</text>
        <text class="txt2">+￥{{deliveryFee}}</text>
      </p>
      <p class="p1">
        <text class="txt1">优惠</text>
        <text class="txt2">-￥{{totalCouponAmount}}</text>
      </p>
      <p class="p2">
        <text class="txt1">实付款：</text>
        <text class="txt2">￥{{actualAmount}}</text>
      </p>
    </div>
    <!-- 安装码 -->
    <div class="section_code" v-if="orderInstallCodeInfos.length > 0">
      <div class="section_code_top">
        <p class="txt1">AERP安装码</p>
        <!-- <p class="txt2" @tap="seeMoreClick">查看更多 <img src="https://m.aerp.com.cn/mini-app-main/red.png" alt="" class="img1"/></p> -->
      </div>
      <!-- <p class="section_code_data">有效期至：{{date1}}</p> -->
      <div class="ermcode">
        <img :src="orderInstallCodeInfos[0].qrCodeBase64String" alt />
      </div>
      <div class="ewmid">ID:{{orderInstallCodeInfos[0].code}}</div>
      <p class="section_code_p1">
        <text class="txt1">使用时间：</text>
        <text
          class="txt2"
        >{{orderInstallCodeInfos[0].startUseBussinessTime+'-'+orderInstallCodeInfos[0].endUseBussinessTime}}</text>
      </p>
      <div class="section_code_p1" style="display: flex;align-items: center;flex-direction: row;">
        <p class="txt1">适用门店：</p>
        <p class="txt2">{{orderInstallCodeInfos[0].shopName}}</p>
      </div>
      <div class="section_code_p2">
        <text class="txt1" style="width:120rpx;minHeight:120rpx;">适用地址：</text>
        <text class="txt2" style="minHeight:120rpx;">{{orderInstallCodeInfos[0].shopAddress}}</text>
      </div>
      <div class="section_code_p2">
        <text class="txt1" style="width:320rpx;minHeight:120rpx;">适用规则：</text>
        <text class="txt2" style="minHeight:120rpx;">{{orderInstallCodeInfos[0].ruleDesc}}</text>
      </div>
    </div>
    <!-- 核销码 -->
    <div class="section_code_code" v-if="orderVerificationCodeInfos.length > 0">
      <div class="section_code_top">
        <p class="txt1">AERP核销码</p>
        <p class="txt2" @tap="seeMoreClick">
          查看更多
          <img src="https://m.aerp.com.cn/mini-app-main/red.png" alt class="img1" />
        </p>
      </div>
      <p
        class="section_code_data"
      >有效期至：{{orderVerificationCodeInfos[0].verificationRuleInfo.endValidTime}}</p>
      <div class="ermcode">
        <img :src="orderVerificationCodeInfos[0].qrCodeBase64String" alt />
      </div>
      <div
        :class="['ewmid',{'text_decoration':orderVerificationCodeInfos[0].status == 2 || orderVerificationCodeInfos[0].status == 1}]"
      >ID:{{orderVerificationCodeInfos[0].code}}</div>
      <!-- <p class="section_code_p1">
        <text class="txt1">使用时间：</text>
        <text class="txt2">{{orderVerificationCodeInfos[0].a}}</text>
      </p>-->
      <p class="section_code_p1">
        <text class="txt1">适用范围：</text>
        <text class="txt2">{{orderVerificationCodeInfos[0].verificationRuleInfo.applicableRange}}</text>
      </p>
      <div class="section_code_p1" style="display: flex;align-items: center;flex-direction: row;">
        <p class="txt1">适用门店：</p>
        <p class="txt2">
          门店列表
          <text @tap="seeClick">点击查看</text>
        </p>
      </div>
      <div class="section_code_p2">
        <text class="txt1" style="width:320rpx;minHeight:120rpx;">适用规则：</text>
        <text
          class="txt2"
          style="minHeight:120rpx;"
        >{{orderVerificationCodeInfos[0].verificationRuleInfo.ruleDesc}}</text>
      </div>
      <!-- 核销码状图图 -->
      <img
        src="https://m.aerp.com.cn/mini-app-main/Label used.png"
        alt
        class="hxmStatus"
        v-if="orderVerificationCodeInfos[0].status == 1"
      />
      <!-- 已使用 -->
      <img
        src="https://m.aerp.com.cn/mini-app-main/Expired.png"
        alt
        class="hxmStatus"
        v-if="orderVerificationCodeInfos[0].status == 2"
      />
      <!-- 已过期 -->
    </div>
    <div class="section_code" v-if="orderVideos.length > 0">
      <div class="section_code_top" style="margin-bottom:20rpx;">
        <p class="txt1">订单视频</p>
      </div>
      <div>
        <video
          :src="item.videoPath"
          v-for="(item,index) in orderVideos"
          :key="index"
          style="width:100%;margin-bottom:10rpx;"
        ></video>
      </div>
    </div>
    <!-- 联系客服按钮 -->

    <div class="service" @tap="serviceClick ">
      <img src="https://m.aerp.com.cn/mini-app-main/index_customer_icon.png" class="serviceImg" />
      <span class="serviceWord">客服</span>
    </div>
    <!-- 联系技师 -->
    <div class="service_customer" v-if="orderDispatchtrue">
      <div class="service_div">
        <p class="service_p">
          <img :src="orderDispatch.userImgUrl" alt class="img" />
          <text class="txt1">{{orderDispatch.name}}</text>
        </p>
        <p>
          <text class="txt2">服务时间：{{orderDispatch.createTime}}</text>
        </p>
      </div>
      <div style="display:flex;flex-direction:row-reverse ;">
        <div class="toService" @tap="toTechTel">联系技师</div>
      </div>
    </div>
    <!-- 取消订单 申请发票-->
    <div class="p_button" v-if="orderUserOperations.length > 0">
      <p
        v-for="(item,index) in orderUserOperations"
        :key="index"
        @tap="showPopup(item)"
      >{{item.showFunctionName}}</p>
    </div>
    <!-- 取消原因弹窗 -->
    <div class="clearC" v-if="showOrder">
      <van-popup
        :show="showOrder"
        closeable
        @close="onClose"
        position="bottom"
        lock-scroll
        custom-style="height: 75%;"
      >
        <div class="popup_header">取消原因</div>
        <van-radio-group v-model="radio">
          <van-cell-group>
            <van-cell
              :title="itemreason.reason"
              clickable
              @tap="clearClick(itemreason)"
              v-for="(itemreason,indexreason) in reasonArr"
              :key="indexreason"
            >
              <van-radio slot="right-icon" checked-color="#FF6324" :name="itemreason.id" />
            </van-cell>
          </van-cell-group>
        </van-radio-group>
        <div
          style="width:100%;height:300rpx;padding: 30rpx;box-sizing: border-box;margin-top: 28rpx;"
          v-if="radio == '4'"
        >
          <div class="popup_inp">
            <textarea
              rows="3"
              class="tareaText"
              :value="textvalue"
              @input="bindTextAreaBlur"
              placeholder-style="color:rgba(102,102,102,1);font-size:28rpx;font-weight:400;"
              maxlength="50"
              placeholder="请输入原因，50字以内"
              style="padding: 30rpx"
            ></textarea>
          </div>
        </div>
        <div v-else style="width:100%;height:300rpx;"></div>
        <div class="popup_btn">
          <button @tap="reasionClick">确定</button>
        </div>
      </van-popup>
    </div>
  </div>
</template>
<script>
import {
  PostNearbyStore,
  GetCode,
  GetOrderDetail,
  GetClearReasion,
  PostSureOrder,
  GetStoresDetails,
  ConfirmPay,
  ClosePay
} from '../../api'
let productInfos = []
export default {
  data() {
    return {
      orderUser: '',
      orderPhone: '',
      orderVideos: [],
      carId: '',
      canBuy: true,
      moretxt: '查看全部',
      isShow: true,
      num: 1,
      recommendProductInfos: [], // 推荐商品列表页
      toHomePrice: 0.0, // 上门服务费
      receiverName: '',
      receiverPhone: '',
      displayOrderType: '',
      productType: '',
      displayOrderChannel: '', // 订单渠道
      reserverTime: '', // 预约时间
      shopAddress: '', // 门店地址
      serviceCheck: 1,
      canPay: true,
      appropriateAddress: '', // 安装码门店地址
      summary: '',
      subSummary: '',
      isNeedDelivery: '',
      orderInstallCodeInfos: [], // 安装码数组
      orderVerificationCodeInfos: [], // 核销码数组
      radio: 1, // 取消原因变量
      username: '', // 用户姓名
      usernumber: '', // 用户手机号
      userCar: '', // 车辆标题
      cartext: '', // 车辆说明
      orderNo: '', // 订单编号
      orderTime: '', // 订单时间
      displayServiceCategory: '', // 服务类型：
      displayPayMethod: '', // 支付方式：
      displayDeliveryMethod: '', // 配送方式：
      displayOrderStatus: '', // 订单状态：
      productAmount: 0.0, // 商品金额
      serviceAmount: 0.0, // 服务金额
      deliveryFee: 0.0, // 运费
      totalCouponAmount: '', // 优惠
      actualAmount: 0.0, // 实付款：
      showOrder: false, // 弹窗取消订单
      reasonArr: [], // 取消原因数组
      textvalue: '', // 用户输入取消原因文字
      orderUserOperations: [], // 用户可执行操作集合按钮
      store_information: {
        title: '',
        img: '',
        address: '',
        distence: '',
        longitude: '',
        latitude: ''
      }, // 收货门店
      products: [], // 商品数组
      productsShop: {}, // 商品信息----商品套餐
      packageProducts: [], // 商品信息----商品套餐数组
      services: [], // 服务数组
      orderDispatchtrue: true,
      orderDispatch: {
        // telephone: '18838959011',
        // techName: 'AERP',
        // createTime: '2020-07-09',
        // userImgUrl: 'https://m.aerp.com.cn/mini-app-main/gotel.png'
      }, // 技师对象
      storeArr: {
        simpleName: '', // 门店名称
        address: '', // 门第地址
        img: '' // 门店图片
      }, // 门店信息
      storeid: 0
    }
  },
  methods: {
    buy(itemm) {
      let product1 = {}
      product1.pid = itemm.pid
      product1.number = 1
      productInfos.push(product1)
      this.globalData.serviceType = 'toHome'
      this.globalData.ProductType = 5
      this.globalData.OrderType = 2
      if (this.canBuy) {
        this.$router.push({
          path: '/pages/orderSure/main',
          query: {
            cardID: this.carId,
            storeid: this.storeid,
            productInfos: JSON.stringify(productInfos),
            orderType: 2,
            storeArr: JSON.stringify(this.storeArr)
          }
        })
      } else {
        wx.showToast({
          title: '该城市暂不支持上门提供该服务',
          icon: 'none'
        })
      }
    },
    getShop() {
      console.log(111, 'getshop')
      let that = this
      wx.getLocation({
        type: 'gcj02',
        altitude: true, // 高精度定位
        // 定位成功，更新定位结果
        success: res => {
          that.longitude = res.longitude
          that.latitude = res.latitude
          console.log(that.latitude)
          let rquestshop = {
            shopIds: [],
            longitude: res.longitude,
            latitude: res.latitude,
            cityId: that.$store.state.curCityInfo.cityId,
            districtId: that.$store.state.curCityInfo.districtId,
            pageIndex: 1,
            pageSize: 1,
            type: 4
          }
          // 获取附近门店列表

          PostNearbyStore(rquestshop)
            .then(res => {
              if (res.data.items.length <= 0) {
                that.canBuy = false
              }
              that.storeArr.address = res.data.items[0].address
              that.storeArr.simpleName = res.data.items[0].simpleName
              that.storeArr.img = res.data.items[0].img
              that.storeid = res.data.items[0].id
            })
            .catch(err => {
              that.canBuy = false
            })
        }
      })
    },
    showMore() {
      console.log('1', this.isShow)
      this.isShow = !this.isShow
      console.log('2', this.isShow)
      this.num = this.isShow ? 1 : this.recommendProductInfos.length
      this.moretxt = this.isShow ? '查看全部' : '收起'
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
          wx.requestPayment({
            timeStamp,
            nonceStr,
            package: package1,
            signType,
            paySign,
            success(res) {
              // that.$router.push('/pages/allOrders/main')
              that.canPay = true
              wx.navigateBack({})
            },
            fail(res) {
              ClosePay({ PayNo }).then(() => {
                that.canPay = true
              })
            }
          })
        })
        .catch(err => {
          this.canPay = true
          console.log(`试算金额错误信息${err}`)
        })
    },
    // 查询地址
    navigate(latitude, longitude) {
      wx.openLocation({
        latitude,
        longitude,
        scale: 18,
        name: this.store_information.title,
        address: this.store_information.address,
        success: res => {
          console.log(`打开位置成功`, res)
        }
      })
    },
    // 客服事件
    serviceClick() {
      let that = this
      wx.showModal({
        // content: that.shopPhone,
        content: '4008008008',

        confirmText: '呼叫',
        confirmColor: '#FF6324',
        success(res) {
          if (res.confirm) {
            console.log('用户点击确定')
            wx.makePhoneCall({
              phoneNumber: '4008008008'
            })
          } else if (res.cancel) {
            console.log('用户点击取消')
          }
        }
      })
    },
    toTechTel() {
      console.log(1111)
      let that = this
      let Telephone = that.orderDispatch.telephone
      console.log(Telephone)
      wx.showModal({
        content: Telephone,
        confirmText: '呼叫',
        confirmColor: '#FF6324',
        success(res) {
          if (res.confirm) {
            console.log('用户点击确定')
            wx.makePhoneCall({
              phoneNumber: Telephone
            })
          } else if (res.cancel) {
            console.log('用户点击取消')
          }
        }
      })
    },
    clearClick(itemreason) {
      this.radio = itemreason.id
    },
    // 取消原因备注
    bindTextAreaBlur(e) {
      console.log(`e${JSON.stringify(e)}`)
      this.textvalue = e.mp.detail.value
    },
    // 用户可操作按钮点击事件
    showPopup(item) {
      if (item.showFunctionName === '支付订单') {
        this.confirmPay(this.orderNo, this.actualAmount)
      } else if (item.showFunctionName === '取消订单') {
        console.log('取消订单')
        this.showOrder = true
      } else if (item.showFunctionName === '再次购买') {
        console.log('再次购买')
      } else if (item.showFunctionName === '申请预约') {
        console.log('申请预约')
        this.$router.push({
          path: '/pages/newAppointment/main',
          query: {
            idnum: '2',
            applynum: '3',
            orderNo: this.orderNo
          }
        })
      } else if (item.showFunctionName === '评价订单') {
        // wx.showToast({
        //   title: '程序猿正在开发中',
        //   icon: 'none'
        // })
        this.$router.push({
          path: '/pages/SelectionAndEvaluation/main',
          query: {
            OrderNo: this.orderNo
          }
        })
      }
    },
    onClose() {
      this.showOrder = false
    },
    // 获取门店详情
    getShopDetails(ShopId, Longitude, Latitude) {
      let that = this
      let shopdata = {
        ShopId: ShopId,
        Longitude: Longitude,
        Latitude: Latitude
      }
      GetStoresDetails(shopdata)
        .then(resshop => {
          console.log(`获取门店详情函数,${resshop}`)
          that.store_information.title = resshop.data.simpleName
          that.store_information.img = resshop.data.imgs[0]
          that.store_information.address = resshop.data.address
          that.store_information.distence = resshop.data.distance
          this.store_information.latitude = resshop.data.latitude
          this.store_information.longitude = resshop.data.longitude
        })
        .catch(err => {
          console.log(`获取门店详情失败函数,${err}`)
        })
    },
    // 获取订单详情
    getOrderXq() {
      let that = this
      let rquest = {
        OrderNo: that.orderNo
      }
      // 获取订单详情
      GetOrderDetail(rquest)
        .then(res => {
          // console.log(`获取订单详情,${JSON.stringify(res)}`)
          that.carId = res.data.carId
          that.summary = res.data.summary // 等待客人支付
          that.subSummary = res.data.subSummary // 时间
          that.displayOrderType = res.data.displayOrderType // 订单类型
          that.username = res.data.receiverName // 用户姓名
          that.usernumber = res.data.receiverPhone // 用户手机号
          that.userCar = res.data.displayVehicleName // 用户车辆
          that.cartext = res.data.displayModelName // 车辆标识
          that.orderTime = res.data.orderTime // 订单时间
          that.displayServiceCategory = res.data.displayServiceCategory // 服务类型：
          that.displayPayMethod = res.data.displayPayMethod // 支付方式
          that.displayDeliveryMethod = res.data.displayDeliveryMethod // 配送方式
          that.displayOrderStatus = res.data.displayOrderStatus // 订单状态
          that.orderUser = res.data.userName
          that.orderPhone = res.data.userPhone
          that.productAmount = res.data.productAmount.toFixed(2) // 商品金额
          that.serviceAmount = res.data.serviceAmount.toFixed(2) // 服务金额
          that.deliveryFee = res.data.deliveryFee.toFixed(2) // 运费
          that.totalCouponAmount = res.data.totalCouponAmount.toFixed(2) // 服务费
          that.actualAmount = res.data.actualAmount.toFixed(2) // 实付款
          that.orderUserOperations = res.data.orderUserOperations // 用户可执行操作集合按钮
          that.orderInstallCodeInfos = res.data.orderInstallCodeInfos // 安装码数组
          that.orderVerificationCodeInfos = res.data.orderVerificationCodeInfos // 核销码数组
          that.displayOrderChannel = res.data.displayOrderChannel // 订单渠道
          that.recommendProductInfos = res.data.recommendProductInfos // 推荐商品列表
          that.orderVideos = res.data.orderVideos
          if (that.recommendProductInfos == null) {
            that.recommendProductInfos = []
          }
          that.productType = res.data.productType // 订单渠道
          that.orderDispatch = res.data.orderDispatch
          that.receiverName = res.data.receiverName
          that.receiverPhone = res.data.receiverPhone
          if (that.orderDispatch == null) {
            that.orderDispatchtrue = false
            console.log(that.orderDispatchtrue)
          }
          that.shopAddress = res.data.shopAddress
          that.reserverTime = res.data.reserverTime
          that.products = res.data.products // 商品信息数组
          that.shopPhone = res.data.shopPhone
          // 服务数组
          that.services = res.data.services
          for (var i = 0; i < that.services.length; i++) {
            if (that.services[i].displayName == '上门服务费') {
              that.toHomePrice = that.services[i].price.toFixed(2)
              this.$forceUpdate()
            }
          }
          if (res.data.shopId != '') {
            wx.getLocation({
              type: 'gcj02',
              altitude: true, // 高精度定位
              // 定位成功，更新定位结果
              success: resLocation => {
                that.getShopDetails(
                  res.data.shopId,
                  resLocation.longitude,
                  resLocation.latitude
                )
              },
              // 定位失败回调
              fail: function() {
                that.getShopDetails(res.data.shopId, 0, 0)
              }
            })
          }
        })
        .catch(err => {
          console.log(`获取订单详情失败函数,${err}`)
        })
    },
    // 点击查看
    seeClick() {
      let that = this
      this.$router.push({
        path: '/pages/stores/main',
        query: {
          arr: JSON.stringify(
            that.orderVerificationCodeInfos[0].verificationRuleInfo.shopIds
          )
        }
      })
    },
    // 查看更多
    seeMoreClick() {
      this.$router.push({
        path: '/pages/CodeSet/main',
        query: {
          orderNo: this.orderNo
        }
      })
    },
    // 取消原因提交按钮
    reasionClick() {
      let that = this
      let rquest = {
        orderNo: that.orderNo,
        reasonId: that.radio,
        instruction: that.textvalue
      }
      // 取消原因提交按钮
      PostSureOrder(rquest)
        .then(res => {
          // console.log(`取消原因提交按钮成功函数,${JSON.stringify(res)}`)
          wx.showToast({ title: res.message, icon: 'none' })
          that.showOrder = false
          setTimeout(function() {
            that.$router.push('/pages/allOrders/main')
          }, 2000)
        })
        .catch(err => {
          console.log(`取消原因提交按钮失败函数,${err}`)
          wx.showToast({ title: res.message, icon: 'none' })
          that.showOrder = false
          setTimeout(function() {
            that.$router.push('/pages/allOrders/main')
          }, 2000)
        })
    },
    // 获取退货原因数组
    GetShop() {
      let that = this
      let rquest = {
        ApplyType: 'Cancel'
      }
      // 获取申请原因集合
      GetClearReasion(rquest)
        .then(res => {
          console.log(`获取申请原因集合成功函数,${JSON.stringify(res)}`)
          that.reasonArr = res.data
        })
        .catch(err => {
          console.log(`获取申请原因集合失败函数,${err}`)
        })
    }
  },
  mounted() {
    this.getOrderXq()
    this.GetShop()
    this.getShop()
  },
  onShow() {
    productInfos = []
  },
  onLoad(options) {
    this.orderNo = options.orderNo
  },
  onUnload() {
    this.orderNo = ''
    this.showOrder = false
  }
}
</script>
<style scoped lang="scss">
.demo_page {
  width: 100%;
  // height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #f3f3f3;
}
.box {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
  min-height: 234rpx;
  background: rgba(255, 99, 36, 1);
  padding: 30rpx;
  padding-bottom: 0;
  box-sizing: border-box;
  .p1 {
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(255, 255, 255, 1);
  }
  .p2 {
    font-size: 24rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(255, 187, 160, 1);
    margin-bottom: 30rpx;
  }
  .title {
    width: 100%;
    height: 88rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 10rpx 10rpx 0 0;
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: bold;
    color: rgba(51, 51, 51, 1);
    padding: 30rpx 0 0 27rpx;
    box-sizing: border-box;
  }
}
.serviceType {
  display: flex;
  justify-content: space-between;
  color: #fff;
  width: 100%;
  padding: 30rpx 30rpx 40rpx 0;
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
.top {
  width: 690rpx;
  height: 82rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx 10rpx 0 0;
  padding: 28rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.customer1 {
  background: #fff;
  box-sizing: border-box;
  padding: 10rpx 20rpx;
  width: 690rpx;
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
  .user {
    font-size: 30rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(33, 33, 33, 1);
    margin-left: 10rpx;
  }
}
.shop_top_info {
  width: 690rpx;
  height: 200rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 0 0 10rpx 10rpx;
  padding: 28rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  .img {
    width: 200rpx;
    height: 138rpx;
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: flex-start;
    .img1 {
      width: 180rpx;
      height: 146rpx;
    }
  }
  .content {
    width: 430rpx;
    height: 200rpx;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: flex-start;
    .p1 {
      width: 100%;
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(33, 33, 33, 1);
      overflow: hidden;
      white-space: pre-wrap;
      display: -webkit-box;
      text-overflow: ellipsis;
      -webkit-line-clamp: 2;
      -webkit-box-orient: vertical;
    }
    .bottom {
      width: 100%;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      .p2 {
        width: 310rpx;
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(136, 136, 136, 1);
        overflow: hidden;
        white-space: pre-wrap;
        display: -webkit-box;
        text-overflow: ellipsis;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
      }
      .p3 {
        width: 100rpx;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        .txt1 {
          font-size: 24rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(136, 136, 136, 1);
        }
        .img2 {
          width: 44rpx;
          height: 44rpx;
        }
      }
    }
  }
}

.one {
  width: 690rpx;
  min-height: 200rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  padding: 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: flex-start;
  margin-top: 16rpx;
  .p1 {
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: bold;
    color: rgba(51, 51, 51, 1);
  }
  .p2 {
    font-size: 28rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(33, 33, 33, 1);
    margin: 30rpx 0 16rpx 0;
  }
  .p3 {
    font-size: 24rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(136, 136, 136, 1);
  }
}
.one1 {
  width: 690rpx;
  min-height: 200rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  padding: 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  margin-top: 16rpx;
  .p1 {
    width: 100%;
    height: 70rpx;
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: bold;
    color: rgba(51, 51, 51, 1);
  }
}
.shop {
  width: 690rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: flex-start;
  padding: 30rpx 28rpx;
  box-sizing: border-box;
  margin: 16rpx 0;
  .p1 {
    width: 100%;
    height: 70rpx;
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: bold;
    color: rgba(51, 51, 51, 1);
  }
  .shop_one {
    width: 100%;
    height: 170rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 10rpx;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    // border-bottom: 1rpx solid #f3f3f3;
    .img {
      width: 150rpx;
      // height: 138rpx;
      display: flex;
      flex-direction: column;
      justify-content: flex-start;
      align-items: flex-start;
      .img1 {
        width: 102rpx;
        height: 102rpx;
      }
    }
    .content {
      width: 500rpx;
      // height: 138rpx;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-start;
      .p1 {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
        overflow: hidden;
        min-height: 80rpx;
        height: auto;
        white-space: pre-wrap;
        display: -webkit-box;
        text-overflow: ellipsis;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
      }
      .bottom {
        width: 100%;
        display: flex;
        flex-direction: row;
        justify-content: flex-end;
        align-items: center;
        .p4 {
          // width: 100rpx;
          display: flex;
          flex-direction: row;
          justify-content: flex-end;
          align-items: flex-end;
          .txt1 {
            font-size: 28rpx;
            font-family: Source Han Sans CN;
            font-weight: 400;
            color: rgba(255, 99, 36, 1);
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
  //  margin-top: 16rpx;

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
        // text-decoration: line-through;
        color: #ff6323;
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
.order_info {
  width: 690rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  padding: 40rpx 30rpx 0;
  box-sizing: border-box;
  margin-top: 16rpx;
  .p1 {
    width: 100%;
    // height: 90rpx;
    padding-bottom: 40rpx;
    .txt1 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(136, 136, 136, 1);
    }
    .txt2 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
  }
}
.shop_info {
  width: 690rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  padding: 10rpx 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  margin: 16rpx 0;
  .p1 {
    width: 100%;
    height: 90rpx;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .txt1 {
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(136, 136, 136, 1);
    }
    .txt2 {
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
  }
  .p2 {
    width: 100%;
    height: 90rpx;
    display: flex;
    flex-direction: row;
    justify-content: flex-end;
    align-items: center;
    .txt1 {
      font-size: 32rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
    .txt2 {
      font-size: 34rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(255, 99, 36, 1);
    }
  }
}
.service_customer {
  width: 690rpx;
  min-height: 90rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;

  margin-bottom: 20rpx;

  padding: 30rpx 20rpx;
  box-sizing: border-box;

  .img {
    width: 80rpx;
    height: 80rpx;
  }
  .txt1 {
    font-size: 28rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: #212121;
    margin-left: 10rpx;
  }
  .txt2 {
    color: #666666;
    font: 400 28rpx/1 'Source Han Sans CN';
  }
  .service_div {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
  }
}
.toService {
  height: 60rpx;
  width: 160rpx;
  border: 1rpx solid #ff6324;
  border-radius: 25rpx;
  color: #ff6324;
  font: 25rpx/1 'SourceHanSansCN-Regular';
  display: flex;
  align-items: center;
  justify-content: center;
}
.service_p {
  display: flex;
  align-items: center;
}

.service {
  position: fixed;
  right: 30rpx;
  bottom: 204rpx;
  z-index: 9999;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 90rpx;
  height: 90rpx;
  background: rgba(255, 255, 255, 1);
  box-shadow: 0rpx 6rpx 20rpx 0rpx rgba(184, 184, 184, 0.55);
  border-radius: 50%;
  .serviceImg {
    width: 42rpx;
    height: 34rpx;
  }
  .serviceWord {
    font-size: 18rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(33, 33, 33, 1);
  }
}
.technician {
  min-height: 90rpx;
}
.btn {
  width: 100%;
  padding: 12rpx 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: flex-end;
  align-items: center;
  background: #ffffff;
  margin-top: 20rpx;
  p {
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
  p:nth-child(2) {
    margin-left: 10rpx;
  }
}
.p_button {
  width: 100%;
  padding: 12rpx 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: flex-end;
  align-items: center;
  background: #ffffff;
  // margin-top: 20rpx;
  p {
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
  p:nth-child(2) {
    margin-left: 10rpx;
  }
}

// 弹窗
.clearC {
  width: 100%;
}
.popup_header {
  width: 100%;
  text-align: center;
  font-weight: bold;
  line-height: 100rpx;
  font-size: 35rpx;
}
.popup_btn {
  width: 100%;
}
.popup_btn > button {
  width: 640rpx;
  height: 90rpx;
  background: #ff6324;
  margin: 0 auto;
  border-radius: 45rpx;
  color: #fff;
}
.popup_inp {
  width: 100%;
  height: 200rpx;
  background: #f2f2f2;
  border-radius: 10rpx;
}
.tareaText {
  height: 200rpx;
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}

// 安装码
.section_code {
  position: relative;
  width: 690rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  padding: 20rpx 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  margin-bottom: 16rpx;
  .section_code_top {
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .txt1 {
      font-size: 32rpx;
      font-family: Source Han Sans CN;
      font-weight: bold;
      color: rgba(51, 51, 51, 1);
    }
    .txt2 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(255, 99, 36, 1);
      .img1 {
        width: 32rpx;
        height: 32rpx;
        vertical-align: middle;
      }
    }
  }
  .section_code_data {
    font-size: 28rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(136, 136, 136, 1);
    margin-top: 40rpx;
  }
  .ermcode {
    width: 100%;
    height: 370rpx;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    img {
      width: 332rpx;
      height: 332rpx;
    }
  }
  .ewmid {
    width: 578rpx;
    height: 80rpx;
    border: 2rpx solid rgba(204, 204, 204, 1);
    border-radius: 10rpx;
    text-align: center;
    line-height: 76rpx;
    margin: 0 auto;
    margin-bottom: 36rpx;
  }
  .text_decoration {
    text-decoration: line-through;
    color: #cccccc;
  }
  .section_code_p1 {
    width: 100%;
    height: 60rpx;
    .txt1 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(136, 136, 136, 1);
    }
    .txt2 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
      margin-left: 40rpx;
      text {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 99, 36, 1);
        margin-left: 10rpx;
      }
    }
  }
  .section_code_p2 {
    display: flex;
    flex-direction: row;
    align-items: flex-start;
    justify-content: flex-start;
    margin-top: 15rpx;
    .txt1 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(136, 136, 136, 1);
    }
    .txt2 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
      margin-left: 40rpx;
    }
  }
}
// 核销码
.section_code_code {
  position: relative;
  width: 690rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  padding: 20rpx 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  margin: 16rpx 0;
  .section_code_top {
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .txt1 {
      font-size: 32rpx;
      font-family: Source Han Sans CN;
      font-weight: bold;
      color: rgba(51, 51, 51, 1);
    }
    .txt2 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(255, 99, 36, 1);
      .img1 {
        width: 32rpx;
        height: 32rpx;
        vertical-align: middle;
      }
    }
  }
  .section_code_data {
    font-size: 28rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(136, 136, 136, 1);
    margin-top: 40rpx;
  }
  .ermcode {
    width: 100%;
    height: 370rpx;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    img {
      width: 332rpx;
      height: 332rpx;
    }
  }
  .ewmid {
    width: 578rpx;
    height: 80rpx;
    border: 2rpx solid rgba(204, 204, 204, 1);
    border-radius: 10rpx;
    text-align: center;
    line-height: 76rpx;
    margin: 0 auto;
    margin-bottom: 36rpx;
  }
  .section_code_p1 {
    width: 100%;
    height: 60rpx;
    .txt1 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(136, 136, 136, 1);
    }
    .txt2 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
      margin-left: 40rpx;
      text {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 99, 36, 1);
        margin-left: 10rpx;
      }
    }
  }
  .section_code_p2 {
    display: flex;
    flex-direction: row;
    align-items: flex-start;
    margin-top: 15rpx;
    .txt1 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(136, 136, 136, 1);
    }
    .txt2 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
      margin-left: 40rpx;
    }
  }
}
.hxmStatus {
  position: absolute;
  top: 156rpx;
  right: 178rpx;
  width: 120rpx;
  height: 120rpx;
}

// 套餐
.shop_meal_top {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: center;
  // align-items: center;
  border-bottom: 2rpx solid #e8e8e8;
  .left {
    width: 150rpx;
    height: 150rpx;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    img {
      width: 146rpx;
      height: 146rpx;
    }
  }
  .right {
    width: 540rpx;
    // height: 150rpx;
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: center;
    margin-left: 20rpx;
    .right_p1 {
      width: 100%;
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
    .right_p2 {
      width: 100%;
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
    .right_p3 {
      margin-top: 30rpx;
      width: 100%;
      display: flex;
      flex-direction: row;
      justify-content: flex-end;
      align-items: center;
      .txt1_txt {
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(136, 136, 136, 1);
      }
      .txt2_txt {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 99, 36, 1);
      }
    }
  }
}

.shop_additionalClass {
  width: 100%;
  display: flex;
  flex-direction: column;
}
.allProduct {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-bottom: 2rpx solid #ebebeb;
  padding: 30rpx 0;
}
.productImg {
  width: 140rpx;
  height: 140rpx;
}
.productDes {
  width: 360rpx;
  display: flex;
  // justify-content: flex-start;
  flex-direction: column;
  margin-left: 15rpx;
  .d1 {
    color: #333;
    font: 28rpx/1.5 'SourceHanSansCN-Medium';
  }
  .d2 {
    color: #666;
    font: 24rpx/2 'SourceHanSansCN-Medium';
  }
  .d3 {
    color: #ff6324;
    font: 28rpx/2 'SourceHanSansCN-Medium';
  }
}
.butt {
  width: 110rpx;
  height: 60rpx;
  background: rgba(255, 99, 36, 1);
  border-radius: 30rpx;
  color: #fff;
  font: 26rpx/1 'SourceHanSansCN-Regular';
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: 110rpx;
}
.moreList {
  height: 63rpx;
  display: flex;
  justify-content: center;
  align-items: center;
  font: 24rpx/1 'SourceHanSansCN-Regular';
  color: #333;
  width: 100%;
  padding-top: 20rpx;
}
.moreImg {
  height: 42rpx;
  width: 42rpx;
  margin-left: 8rpx;
}
</style>