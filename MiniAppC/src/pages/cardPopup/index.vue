<template>
  <div class="demo_page">
    <div class="carPopup" v-if="showDis">
      <div class="box_Popup">
        <div class="top_title">按车型购买</div>
        <div class="all_popup">
          <p class="all_title">精准适配，专车专用</p>
          <p class="all_text">依靠系统强大的数据分析能力，为您的爱车选择 最适用的商品。</p>
          <p class="all_title marginTop">严选套餐，服务至上</p>
          <p class="all_text">甄选优质商品组合成多个套餐，价格更低，省去 挑选烦恼，体验更加。</p>
          <p class="all_btn">继续</p>
          <p class="all_zhushi">注：适配商品以套餐方式组合售卖</p>
        </div>
      </div>
      <img
        src="https://m.aerp.com.cn/mini-RG-main/goodsdetail_close_icon.png"
        alt
        class="ximg"
        @tap="disPopupClick"
      />
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
  justify-content: center;
  align-items: center;
  .box_Popup {
    width: 635rpx;
    // height:760rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 14rpx;
    display: flex;
    flex-direction: column;
    .top_title {
      width: 100%;
      height: 90rpx;
      background: url(https://m.aerp.com.cn/mini-RG-main/goodsdetail_title_bg.png)
        no-repeat;
      background-size: cover;
      border-radius: 14rpx 14rpx 0rpx 1rpx;
      text-align: center;
      line-height: 90rpx;
      font-size: 46rpx;
      font-family: Source Han Sans CN;
      font-weight: bold;
      color: rgba(255, 255, 255, 1);
    }
    .all_popup {
      width: 100%;
      height: 670rpx;
      display: flex;
      flex-direction: column;
      padding: 50rpx 45rpx;
      box-sizing: border-box;
      .all_title {
        width: 100%;
        text-align: center;
        font-size: 32rpx;
        font-family: Source Han Sans CN;
        font-weight: bold;
        color: rgba(33, 33, 33, 1);
        margin-top: 30rpx;
      }
      .all_text {
        width: 100%;
        font-size: 26rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(136, 136, 136, 1);
        margin-top: 23rpx;
      }
      .marginTop {
        margin-top: 60rpx;
      }
      .all_btn {
        width: 421rpx;
        height: 90rpx;
        background: rgba(255, 99, 36, 1);
        border-radius: 45rpx;
        font-size: 32rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 255, 255, 1);
        text-align: center;
        line-height: 90rpx;
        margin: 49rpx auto 50rpx auto;
      }
      .all_zhushi {
        width: 100%;
        text-align: center;
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(173, 173, 173, 1);
      }
    }
  }
  .ximg {
    width: 63rpx;
    height: 63rpx;
    margin-top: 40rpx;
  }
}
</style>