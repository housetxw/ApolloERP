<template>
  <div class="demo_page">
    <div class="carPopup" v-if="showDis">
      <div class="box_Popup">
        <div class="top_title">
          <p>优惠信息</p>
          <img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_close_black.png" alt />
        </div>
        <div class="top_bottom">
          <p class="top_bottom_p1">优惠</p>
          <p class="top_bottom_p2">【超级会员专享】每满800立减80，最高减400</p>
          <p class="top_bottom_p3">
            <text class="top_bottom_text">活动时间：</text>
            <text class="top_bottom_text2">2022.09.09-2022.09.30</text>
          </p>
          <p class="top_bottom_p3">
            <text class="top_bottom_text">活动说明：</text>
            <text
              class="top_bottom_text2"
            >专业综合网上购物商城，销售超数万 4020万种商品，囊括家电手机、电脑、服装、居家 婴、美妆、个护、食务！</text>
          </p>
          <p class="top_bottom_p4">积分可用</p>
          <p class="top_bottom_p5">全部商品分类,为您展示所有品类详细分布。选商品看分类,让您的购物更便捷、轻松!网购上**,省钱又放心!</p>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      showDis: true
    }
  },
  methods: {
    disPopupClick() {
      wx.showToast({ title: '关闭弹窗', icon: 'none' })
      this.showDis = false
    }
  }
}
</script>
<style scoped lang="scss">
.demo_page {
  height: 100%;
}
.carPopup {
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.4);
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  align-items: center;
  .box_Popup {
    width: 750rpx;
    height: 700rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 14rpx 14rpx 0rpx 0rpx;
    display: flex;
    flex-direction: column;
    .top_title {
      width: 100%;
      height: 100rpx;
      background: rgba(247, 247, 247, 1);
      border-radius: 14rpx 14rpx 0rpx 0rpx;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      padding-right: 30rpx;
      box-sizing: border-box;
      p {
        width: 60%;
        height: 100%;
        line-height: 100rpx;
        text-align: right;
        font-size: 32rpx;
        font-family: Source Han Sans CN;
        font-weight: bold;
        color: rgba(51, 51, 51, 1);
      }
      img {
        width: 26rpx;
        height: 26rpx;
      }
    }
    .top_bottom {
      width: 100%;
      padding: 30rpx;
      padding-right: 10rpx;
      box-sizing: border-box;
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: flex-start;
      .top_bottom_p1 {
        width: 70rpx;
        height: 35rpx;
        text-align: center;
        background: rgba(255, 238, 231, 1);
        border-radius: 4rpx;
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 99, 36, 1);
      }
      .top_bottom_p2 {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 500;
        color: rgba(51, 51, 51, 1);
        margin-top: 30rpx;
        margin-bottom: 11rpx;
      }
      .top_bottom_p3 {
        display: flex;
        flex-direction: row;
        justify-content: flex-start;
        align-items: flex-start;
        margin-top: 26rpx;
        .top_bottom_text {
          width: 140rpx;
          font-size: 26rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(136, 136, 136, 1);
        }
        .top_bottom_text2 {
          width: 550rpx;
          font-size: 26rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(51, 51, 51, 1);
        }
      }
      .top_bottom_p4 {
        width: 120rpx;
        height: 35rpx;
        text-align: center;
        background: rgba(255, 238, 231, 1);
        border-radius: 4rpx;
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 99, 36, 1);
        margin-top: 70rpx;
      }
      .top_bottom_p5 {
        margin-top: 32rpx;
        font-size: 26rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
      }
    }
  }
}
</style>