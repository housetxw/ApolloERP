<template>
  <div class="demo_page">
    <!-- <navigation-bar title="发票说明" :back-visible="true" :home-path="'/pages/index/main'" @test="test"></navigation-bar> -->

    <div class="main">
      <div>
        <h3 class="h3">安装后付款</h3>

        <h4 class="h6">您可通过以下两种方式进行安装后付款：</h4>
<div>
        <p class="p">1.您可通过AERP平台在线下单并支付后可在对应合作店或中心店进行安装后进行验收</p>


        <p class="p">2.您可通过AERP平台在线下单后，可在对应中心店到店后进行安装并验收付款。</p>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.demo_page {
  width: 100%;
  height: 100%;
  background: #ffffff


}
.main{
  
padding:0 60rpx 60rpx 60rpx;

}
.p {
  text-indent: 69rpx;
  text-align: left;
}
p {
  text-align: left;
}
.h3{
  font: 700 46rpx/2.2 '';
  text-align: center;

}
.h4{
  font: 500 32rpx/1.5 '';
text-align: center;
}
.h6{
  font: 600 36rpx/2 '';
text-align: center;
}
.P1{
    font: 600 32rpx/1.2 '';

     text-align: left;
   
}

</style>