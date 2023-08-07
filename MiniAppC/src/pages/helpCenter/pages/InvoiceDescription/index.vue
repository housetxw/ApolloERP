<template>
  <div class="demo_page">
    <!-- <navigation-bar title="发票说明" :back-visible="true" :home-path="'/pages/index/main'" @test="test"></navigation-bar> -->

    <div class="main">
      <h3 class="h3">发票说明</h3>
      <h4 class="h4">
        <p
          class="p"
        >当根据国家税法及发票管理相关规定，任何单位和个人不得要求他人为自己开具与实际经营业务情况不符的增值专用发票【包括并不限于(1) 在没有货物采购或者没有接受应税劳务的情况下要求他人为自己开具增值税专用发票；(2) 虽有货物采购或者接受应税劳务但要求他人为自己开具数量或金额与实际情况不符的增值税专用发票】，否则属于“虚开增值税专用发票”。</p>
        <p
          class="p"
        >我已充分了解上述各项相关国家税法和发票管理规定，并确认仅就我司实际购买商品及/或服务索取发票。如我司未按国家相关规定申请开具或使用增值税专用发票，由我司自行承担相应法律后果。</p>
      </h4>
      <br />
      <h6 class="h6">发票详情：</h6>

      <br />
      <h6 class="h6">一、发票开具规则：</h6>
      <p
        class="p"
      >(1) 订单完成后，需开具发票可进行拨打致大养车平台官方电话进行开具发票；增值税专用发票(纸质)如资质审核通过，将在订单完成后3-4个工作日内为您寄出，邮寄信息详见收票人信息；(2) 订单需先确认商品收货，待订单完成后为您开具发票；(3) 发票开具金额为您实际支付金额。</p>
      <br />
      <h6 class="h6">二、发票补开换开及修改规则：</h6>
      <p
        class="p"
      >(1) 发票开具成功后，1年内可支持一次发票换开；(2) 更换增值税专用发票，需将原票邮寄回我司，邮寄地址请见收票人信息；(3) 其他订单均不支持发票的补开与换开。</p>
    </div>
  </div>
</template>
<style lang="scss" scoped>
.demo_page {
  width: 100%;
  height: 100%;
  background: #ffffff;
}
.main {
  text-align: center;
  padding: 0 60rpx 60rpx 60rpx;
}
.p {
  text-indent: 69rpx;
  text-align: left;
}
p {
  text-align: left;
}
.h3 {
  font: 700 46rpx/2.2 '';
}
.h4 {
  font: 500 32rpx/1.5 '';
}
.h6 {
  font: 600 36rpx/2 '';
}
</style>