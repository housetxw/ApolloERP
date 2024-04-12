<template>
  <div class="container">
    <!-- <div class="group_title">关于</div> -->
    <!-- <van-cell title="AERP介绍" is-link  @tap="introduce"/>
    <van-cell title="企业文化" is-link   @tap="culture"/>-->
    <van-cell title="用户注册协议" is-link @tap="agreement" />
    <van-cell title="隐私政策" is-link @tap="policy" />
    <van-cell title="订单共享与安全" is-link @tap="orderSharing" />
    <van-cell title="访问信息的使用" is-link @tap="accessInformation" />
    <van-cell title="上传内容的使用" is-link @tap="UploadUse" />
    <van-cell title="退货退款说明" is-link @tap="returnRefund" />
    <van-cell title="法律声明" is-link @tap="legalExplanation" />
    <!-- <div class="jianGe"></div>
    <div class="group_title">购物指南</div>
      <van-cell title="经营证照" is-link  @tap="introduce"/>
      <van-cell title="价格说明" is-link  @tap="introduce"/>
    
     <div class="jianGe"></div>
     <div class="group_title">配送方式</div>
    <van-cell title="配送服务收费标准" is-link  @tap="introduce"/>-->

    <div class="jianGe"></div>
    <div class="group_title">支付方式</div>
    <van-cell title="在线支付" is-link @tap="onlinePayment" />
    <van-cell title="安装后付款" is-link @tap="installation" />
    <van-cell title="发票说明" is-link @tap="InvoiceDescription" />

    <!-- <div class="jianGe"></div> -->
    <!-- <div class="group_title">常见问题</div>
      <van-cell title="订单已提交成功，什么时候可以送到" is-link  @tap="introduce"/>
      <van-cell title="我的商品是否可以退换货" is-link  @tap="introduce"/>
      <van-cell title="取消订单后，款项如何退回？" is-link  @tap="introduce"/> 
      <van-cell title="订单已提交成功后，如何付款？" is-link  @tap="introduce"/>
    <van-cell title="预约提交后多久可以给出审核结果？" is-link  @tap="introduce"/>-->
  </div>
</template>

<script>
export default {
  data() {
    return {}
  },
  methods: {
    orderSharing() {
      this.$router.push('/pages/helpCenter/pages/orderSharing/main')
    },
    accessInformation() {
      this.$router.push('/pages/helpCenter/pages/accessInformation/main')
    },
    UploadUse() {
      this.$router.push('/pages/helpCenter/pages/UploadUse/main')
    },
    onlinePayment() {
      this.$router.push('/pages/helpCenter/pages/onlinePayment/main')
    },
    installation() {
      this.$router.push('/pages/helpCenter/pages/installation/main')
    },
    InvoiceDescription() {
      this.$router.push('/pages/helpCenter/pages/InvoiceDescription/main')
    },

    returnRefund() {
      this.$router.push('/pages/helpCenter/pages/returnRefund/main')
    },
    legalExplanation() {
      this.$router.push('/pages/helpCenter/pages/legalExplanation/main')
    },
    agreement() {
      this.$router.push('/pages/helpCenter/pages/agreement/main')
    },
    policy() {
      this.$router.push('/pages/helpCenter/pages/policy/main')
    }
  },
  created() {}
}
</script>

<style>
.container {
  width: 100%;
  height: 100%;
  background: #ffffff;
}
.group_title {
  width: 100%;
  padding-left: 36rpx;
  padding-top: 30rpx;
  padding-bottom: 30rpx;
  background: #ffffff;
  color: #333333;
  font-weight: bold;
  size: 12rpx;
  display: inlie-block;
  clear: both;
}
.jianGe {
  width: 100%;
  height: 8px;
  background: #f2f2f2;
  clear: both;
}
</style>
