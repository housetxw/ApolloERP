<template>
  <div class="demo_page">
    <div class="demo_section">
      <div class="demo_header">
        <div class="header_summary">{{summary}}</div>
        <div class="header_subSummary">{{subSummary}}</div>
        <div class="header_isNeedDelivery">
          <p>{{isNeedDelivery}}</p>
        </div>
      </div>
    </div>
    <div class="demo_nav">
      <div class="nav_left">
        <img :src="leftimg" alt width="40rpx" height="40rpx" />
        {{receiverName}}
      </div>
      <div class="nav_right">
        <img :src="rightimg" alt width="40rpx" height="40rpx" />
        {{receiverPhone}}
      </div>
    </div>
    <div class="demo_car">
      <div class="car_user">车辆信息</div>
      <div class="car_name">{{displayVehicleName}}</div>
      <div class="car_p">{{displayModelName}}</div>
    </div>
    <div class="demo_shop">
      <div class="car_user">商品</div>
      <div class="shop" v-for="(item,index) in products" :key="index">
        <div class="shop_img">
          <img :src="item.imageUrl" alt />
        </div>
        <div class="shop_content">
          <div class="shop_name">{{item.displayName}}</div>
          <div class="shop_Price">￥{{item.price}} * {{item.number}}</div>
        </div>
      </div>
    </div>
    <div class="demo_shop">
      <div class="car_user">服务</div>
      <div class="shop">
        <div class="service_img">
          <img :src="services.imageUrl" alt />
        </div>
        <div class="service_title">{{services.displayName}}</div>
        <div class="service_Price">
          <p class="services_salesPrice">￥{{services.salesPrice}}</p>*
          <p>{{services.number}}</p>
        </div>
      </div>
    </div>
    <div class="demo_list">
      <div class="list_head">
        <div class="list_title">订单编号</div>
        <div class="list_content">{{orderNo}}</div>
      </div>
      <div class="list_head">
        <div class="list_title">订单时间</div>
        <div class="list_content">{{orderTime}}</div>
      </div>
      <div class="list_head">
        <div class="list_title">服务类型</div>
        <div class="list_content">{{displayServiceCategory}}</div>
      </div>
      <div class="list_head">
        <div class="list_title">支付方式</div>
        <div class="list_content">{{displayPayMethod}}</div>
      </div>
      <div class="list_head">
        <div class="list_title">配送方式</div>
        <div class="list_content">{{displayDeliveryMethod}}</div>
      </div>
      <div class="list_head">
        <div class="list_title">订单状态</div>
        <div class="list_content">{{displayOrderStatus}}</div>
      </div>
    </div>
    <div class="demo_list">
      <div class="list_head">
        <div class="money_title">商品金额</div>
        <div class="money_content">￥ {{productAmount}}</div>
      </div>
      <div class="list_head">
        <div class="money_title">服务金额</div>
        <div class="money_content">￥ {{serviceAmount}}</div>
      </div>
      <div class="list_head">
        <div class="money_title">运费</div>
        <div class="money_content">+ {{deliveryFee}}</div>
      </div>
      <div class="list_head">
        <div class="money_title">优惠</div>
        <div class="money_content">- {{totalCouponAmount}}</div>
      </div>
      <div class="payment">
        实付款： ￥
        <div class="actualAmount">{{actualAmount}}</div>
      </div>
    </div>
    <div class="section_code">
      <div class="section_header">{{head}}</div>
      <div class="section_time">有效期至：</div>
      <!-- 核销码可使用 -->
      <div class="section_QRcode" v-if="status == 0">
        <img :src="QRcode" alt class="codeImg" />
      </div>
      <!-- 核销码已使用 -->
      <div class="section_QRcode" v-if="status == 1">
        <img :src="QRcode" alt class="codeImgs" />
        <img :src="AlreadyUsed" alt class="LabelUsed" />
      </div>
      <!-- 核销码已过期 -->
      <div class="section_QRcode" v-if="status == 2">
        <img :src="QRcode" alt class="codeImgs" />
        <img :src="Expired" alt class="LabelUsed" />
      </div>
      <div class="section_id" v-if="status == 0">ID：</div>
      <div class="section_id1" v-if="status == 1">ID：</div>
      <div class="section_id2" v-if="status == 2">ID：</div>
      <div class="section_title">
        <div class="section_title_left">使用时间:</div>
        <div class="section_title_right"></div>
      </div>
      <div class="section_title">
        <div class="section_title_left">使用范围:</div>
        <div class="section_title_right">{{applicableRange}}</div>
      </div>
      <div class="section_title">
        <div class="section_title_left">使用门店:</div>
        <div class="section_title_right">
          门店列表
          <span>点击查看</span>
        </div>
      </div>
      <div class="section_title">
        <div class="section_title_left">使用规则:</div>
        <div class="section_title_right">{{ruleDesc}}</div>
      </div>
    </div>
    <!-- 核销码 -->
    <!-- <div class="section_code">
      <div class="section_header">{{head}}</div>
      <div class="section_time">有效期至：</div>
      <div class="section_QRcode">
        <img :src="QRcode" alt="">
      </div>
      <div class="section_id">ID：</div>
      <div class="section_title">
        <div class="section_title_left">使用时间:</div>
        <div class="section_title_right"></div>
      </div>
      <div class="section_title">
        <div class="section_title_left">使用范围:</div>
        <div class="section_title_right">{{applicableRange}}</div>
      </div>
      <div class="section_title">
        <div class="section_title_left">使用门店:</div>
        <div class="section_title_right">门店列表 <span> 点击查看</span></div>
      </div>
      <div class="section_title">
        <div class="section_title_left">使用规则:</div>
        <div class="section_title_right">{{ruleDesc}}</div>
      </div>
    </div>-->
    <div class="customer">
      <img
        src="https://m.aerp.com.cn/mini-RG-main/order_navigation_icon.png"
        alt
        class="img"
        v-if="status == 0"
      />
      <text class="txt1">联系客服</text>
    </div>
    <div class="footer">
      <button class="footer_btn1" @click="showPopup">取消订单</button>
      <button class="footer_btn2">支付订单</button>
    </div>
    <van-popup
      :show="show"
      closeable
      @close="onClose"
      position="bottom"
      custom-style="height: 70%;"
    >
      <div class="popup_header">取消原因</div>
      <van-radio-group v-model="radio">
        <van-cell-group>
          <van-cell title="7天无理由退货" clickable @click="radio = '1'">
            <van-radio slot="right-icon" name="1" />
          </van-cell>
          <van-cell title="不喜欢 / 不想要" clickable @click="radio = '2'">
            <van-radio slot="right-icon" name="2" />
          </van-cell>
          <van-cell title="待审核时间长" clickable @click="radio = '3'">
            <van-radio slot="right-icon" name="3" />
          </van-cell>
          <van-cell title="其他" clickable @click="radio = '4'">
            <van-radio slot="right-icon" name="4" />
          </van-cell>
        </van-cell-group>
      </van-radio-group>
      <div class="popup_inp">
        <textarea rows="3" maxlength="50" placeholder="请输入原因，50字以内" style="padding: 30rpx"></textarea>
      </div>
      <div class="popup_btn">
        <button>确定</button>
      </div>
    </van-popup>
  </div>
</template>
<script>
import { GetCode, GetOrderDetail } from '../../api'
export default {
  data() {
    return {
      show: false,
      radio: '1',
      showFunctionName: '',
      AlreadyUsed: `${this.globalData.imgPubUrl}Label used.png`,
      Expired: `${this.globalData.imgPubUrl}Expired.png`,
      head: '',
      QRcode: '',
      applicableRange: '',
      ruleDesc: '',
      status: '',
      summary: '等待仓库发货',
      subSummary: '到货后会及时和客人预约到店服务时间',
      isNeedDelivery: '配送到个人',
      leftimg: `${this.globalData.imgPubUrl}personal@2x.png`,
      rightimg: `${this.globalData.imgPubUrl}Mobile phone.png`,
      receiverName: '红小',
      receiverPhone: '15675274444',
      displayVehicleName: 'R- 荣威RX5 -上海汽车',
      displayModelName: '2018款 30t 两驱自动互联网智享版',
      products: [
        {
          imageUrl: 'https://img.yzcdn.cn/vant/leaf.jpg',
          displayName: '美孚/Mobil 1号全合成机油 5W- 30SNdv级4L',
          price: '489.00',
          number: '1'
        }
      ],
      services: {
        imageUrl: 'https://img.yzcdn.cn/vant/leaf.jpg',
        displayName: '',
        salesPrice: '',
        number: ''
      },
      orderNo: '',
      orderTime: '',
      displayServiceCategory: '',
      displayPayMethod: '',
      displayDeliveryMethod: '',
      displayOrderStatus: '',
      productAmount: '',
      serviceAmount: '',
      deliveryFee: '',
      totalCouponAmount: '',
      actualAmount: ''
    }
  },
  mounted() {
    let that = this
    let rquest = {
      OrderNo: 'RGC00010'
    }
    // 获取订单详情
    GetOrderDetail(rquest)
      .then(res => {
        that.summary = res.data.summary
        that.subSummary = res.data.subSummary
        that.isNeedDelivery = res.data.displayDeliveryMethod
        that.receiverName = res.data.receiverName
        that.receiverPhone = res.data.receiverPhone
        that.displayVehicleName = res.data.displayVehicleName
        that.displayModelName = res.data.displayModelName
        that.services = res.data.services[0]
        that.services.imageUrl = res.data.services[0].imageUrl
        that.services.displayName = res.data.services[0].displayName
        that.services.salesPrice = res.data.services[0].salesPrice
        that.services.number = res.data.services[0].number
        that.orderNo = res.data.orderNo
        that.orderTime = res.data.orderTime
        that.displayServiceCategory = res.data.displayServiceCategory
        that.displayPayMethod = res.data.displayPayMethod
        that.displayDeliveryMethod = res.data.displayDeliveryMethod
        that.displayOrderStatus = res.data.displayOrderStatus
        that.productAmount = res.data.productAmount
        that.serviceAmount = res.data.serviceAmount
        that.deliveryFee = res.data.deliveryFee
        that.totalCouponAmount = res.data.totalCouponAmount
        that.actualAmount = res.data.actualAmount

        that.head =
          res.data.orderVerificationCodeInfos[0].verificationRuleInfo.displayCodeName
        that.applicableRange =
          res.data.orderVerificationCodeInfos[0].verificationRuleInfo.applicableRange
        that.ruleDesc =
          res.data.orderVerificationCodeInfos[0].verificationRuleInfo.ruleDesc
        that.status = res.data.orderVerificationCodeInfos[0].status
      })
      .catch(err => {})

    // 获取订单核销码
    GetCode(rquest)
      .then(res => {
        that.code = res.data
        that.QRcode =
          res.data.orderVerificationCodeBaseInfos[0].qrCodeBase64String
        // that.head = res.data.verificationRuleInfo.displayCodeName
        // that.applicableRange = res.data.verificationRuleInfo.applicableRange
        // that.ruleDesc = res.data.verificationRuleInfo.ruleDesc
        // that.status = res.data.orderVerificationCodeBaseInfos[0].status
      })
      .catch(err => {})
  },
  methods: {
    showPopup() {
      this.show = true
    },
    onClose() {
      this.show = false
    }
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
  // align-items: center;
  background: #f3f3f3;
}
.demo_section {
  width: 100%;
  background: #ff6324;
}
.demo_header {
  width: 690rpx;
  margin: 0 auto;
}
.header_summary {
  font-size: 32rpx;
  color: #ffffff;
  padding-top: 36rpx;
}
.header_subSummary {
  font-size: 24rpx;
  color: #ffbba0;
  padding: 20rpx 0;
}
.header_isNeedDelivery {
  width: 690rpx;
  height: 80rpx;
  background: #fff;
  font-size: 32rpx;
  color: #333333;
  font-weight: bold;
  margin: 0 auto;
  line-height: 80rpx;
  border-radius: 10rpx 10rpx 0 0;
}
.header_isNeedDelivery > p {
  padding: 0 30rpx;
}
.demo_nav {
  width: 690rpx;
  height: 80rpx;
  background: #fff;
  color: #333333;
  margin: 0 auto;
  border-radius: 0 0 10rpx 10rpx;
  display: flex;
}
.nav_left,
.nav_right {
  flex: 3;
  padding: 30rpx;
}
.nav_left > img {
  width: 40rpx;
  height: 40rpx;
  vertical-align: middle;
}
.nav_right > img {
  width: 40rpx;
  height: 40rpx;
  vertical-align: middle;
}
.demo_car {
  width: 690rpx;
  background: #fff;
  margin: 16rpx auto;
}
.car_user {
  font-size: 32rpx;
  color: #333333;
  font-weight: bold;
  line-height: 42rpx;
  padding: 30rpx;
}
.car_name {
  font-size: 28rpx;
  color: #333333;
  line-height: 64rpx;
  padding-left: 30rpx;
}
.car_p {
  font-size: 24rpx;
  color: #888888;
  line-height: 64rpx;
  padding-left: 30rpx;
}
.demo_shop {
  width: 690rpx;
  background: #fff;
  margin: 16rpx auto;
}
.shop {
  display: flex;
}
.shop_img > img {
  width: 120rpx;
  height: 120rpx;
  padding: 15rpx 15rpx 15rpx 30rpx;
}
.shop_name {
  font-size: 28rpx;
  color: #333333;
}
.shop_Price {
  font-size: 28rpx;
  color: #333333;
  text-align: right;
  padding-right: 30rpx;
  padding-top: 30rpx;
}
.service_img > img {
  flex: 1;
  width: 60rpx;
  height: 60rpx;
  padding: 15rpx 15rpx 15rpx 30rpx;
}
.service_title {
  flex: 4;
  height: 60rpx;
  padding: 15rpx 15rpx 15rpx 30rpx;
  font-size: 28rpx;
  color: #333333;
  line-height: 60rpx;
}
.service_Price {
  flex: 1;
  display: flex;
  padding: 15rpx;
  font-size: 28rpx;
  color: #333333;
  line-height: 60rpx;
}
.services_salesPrice {
  text-decoration: line-through;
  color: #888888;
  font-size: 28rpx;
}
.demo_list {
  width: 690rpx;
  background: #fff;
  margin: 16rpx auto;
}
.list_head {
  display: flex;
  padding: 30rpx;
}
.list_title {
  flex: 2;
  font-size: 24rpx;
  color: #888888;
  text-align: center;
}
.list_content {
  flex: 4;
  font-size: 24rpx;
  color: #333333;
}
.money_title {
  flex: 2;
  font-size: 24rpx;
  color: #888888;
  text-align: center;
}
.money_content {
  flex: 4;
  font-size: 24rpx;
  color: #333333;
  text-align: right;
}
.payment {
  width: 100%;
  text-align: right;
  font-size: 32rpx;
  margin: 40rpx 0;
}
.actualAmount {
  display: inline-block;
  color: #ff6324;
  padding-right: 30rpx;
}
.LabelUsed {
  width: 120rpx;
  height: 120rpx;
  position: absolute;
  top: 0;
  right: 0;
}
.section_id1 {
  width: 580rpx;
  height: 80rpx;
  border: 1px solid #cccccc;
  text-align: center;
  line-height: 80rpx;
  margin: 0 auto;
  border-radius: 10rpx;
  text-decoration: line-through;
}
.section_id2 {
  width: 580rpx;
  height: 80rpx;
  border: 1px solid #cccccc;
  text-align: center;
  line-height: 80rpx;
  margin: 0 auto;
  border-radius: 10rpx;
  text-decoration: line-through;
}
.customer {
  width: 690rpx;
  height: 90rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  margin: 0 auto;
  .img {
    width: 47rpx;
    height: 47rpx;
  }
  .txt1 {
    font-size: 28rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(51, 51, 51, 1);
    margin-left: 10rpx;
  }
}
.section_code {
  width: 690rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  padding: 28rpx;
  box-sizing: border-box;
  margin: 30rpx auto;
}
.section_header {
  font-size: 32rpx;
  color: #333333;
  font-weight: bold;
}
.section_time {
  font-size: 28rpx;
  color: #888888;
  padding: 40rpx 0 0 0;
}
.section_QRcode {
  width: 400rpx;
  height: 400rpx;
  margin: 0 auto;
  position: relative;
  top: 0;
  left: 0;
}
.section_QRcode > .codeImg {
  width: 100%;
  height: 100%;
}
.section_QRcode > .codeImgs {
  width: 100%;
  height: 100%;
  opacity: 0.2;
}
.section_title {
  width: 100%;
  display: flex;
  margin: 30rpx 0;
}
.section_title_left {
  flex: 1;
  font-size: 28rpx;
  color: #888888;
}
.section_title_right {
  flex: 4;
  padding: 0 20rpx 0 20rpx;
  font-size: 28rpx;
  color: #333333;
}
.section_title_right > span {
  color: #ff6324;
}
.section_id {
  width: 580rpx;
  height: 80rpx;
  border: 1px solid #cccccc;
  text-align: center;
  line-height: 80rpx;
  margin: 0 auto;
  border-radius: 10rpx;
}
.footer {
  width: 100%;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  padding: 28rpx;
  box-sizing: border-box;
  margin: 30rpx auto;
  text-align: right;
  margin-bottom: 0;
}
.footer_btn1 {
  width: 240rpx;
  height: 75rpx;
  border-radius: 38rpx;
  display: inline-block;
  margin-left: 16rpx;
  background: #dddddd;
  color: #888888;
  font-size: 32rpx;
}
.footer_btn2 {
  width: 240rpx;
  height: 75rpx;
  border-radius: 38rpx;
  display: inline-block;
  margin-left: 16rpx;
  background: #ff6324;
  color: #ffffff;
  font-size: 32rpx;
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
</style>