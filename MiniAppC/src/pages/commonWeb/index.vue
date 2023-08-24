<template>
  <div class="page">
    <div class="back" @click="back">
      <image src="https://m.aerp.com.cn/mini-app-main/white_back.png" style="width:16rpx;height:29rpx;"></image>
    </div>

    <div class="content">
            <web-view :webview-styles="webviewStyles"  :src="webUrl"></web-view>
    </div>       
  </div>
</template>

<script>
export default {
  data() {
    return {
      "webUrl":""
    }
  },
  mounted() {},
  onLoad(op) {
    let that = this
    that.webUrl = decodeURIComponent(op.url);
    console.log("onload", that.webUrl)
  },
  methods: {   
    back() {
      wx.navigateBack()
    }
 
  }
}
</script>
<style>
  page {
    position: relative;
  }
</style>

<style scoped lang="scss">
.page {
  display: flex;
  flex-direction: column;
  align-items: center;
  .back {
    position: fixed;
    width: 50rpx;
    height: 50rpx;
    display: flex;
    align-items: center;
    justify-content: center;
    left: 43rpx;
    top: 72rpx;
    z-index: 1;
  }
  .content {
    position: absolute;
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;    
  }  
}
</style>
