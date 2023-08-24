<template>
  <div class="page">
    <div class="back" @click="back">
      <image src="https://m.aerp.com.cn/mini-app-main/white_back.png" style="width:16rpx;height:29rpx;"></image>
    </div>
    <image src="https://m.aerp.com.cn/mini-app-main/kaiYeDetail.png" mode="widthFix" style="width:100%;position:relative;min-height:100%">

      <div class="content">

        <div class="button" @click="toGetCoupon">点击领取</div>
        <div class="button" @click="toGetCard" style="margin-top:780rpx">立即购买</div>
      </div>
    </image>
  </div>

</template>

<script>
import { GetCouponDetailByActId, ExchangeCouponByActId } from '../../api'
export default {
  data() {
    return {
      advertisingCode:'',
      isFromScanCode:false,
    }
  },
  mounted() {},
  onLoad(op) {
     if (op.q) {
      console.log("--op.q--->",op.q)
      let res = decodeURIComponent(op.q)
      console.log("--op.q-res-->",res)
      this.advertisingCode = this.getRequest2('code', res) || ''
      console.log("--op.q-advertisingCode-->",this.advertisingCode)
      this.isFromScanCode = true;
    } else {
      this.advertisingCode = op.code || ''
    }
  },
  methods: {
     getRequest2(qs, url) {
      var s = url.replace('?', '?&').split('&')
      let re = ''
      for (var i = 1; i < s.length; i++) {
        if (s[i].indexOf(qs + '=') == 0) {
          re = s[i].replace(qs + '=', '')
        }
      }
      return re
    },
    toGetCoupon() {
      this.$router.push({
        path: '/pages/receivePage/main',
        query: {
          code: 'BTFJsPoMstz9'
        }
      })
    },
    toGetCard() {
      this.globalData.serviceType = 'toShop'

      this.globalData.OrderType = 6

      this.globalData.ProductType = 14
      let that = this
      this.$router.push({
        path: '/pages/detailsPages/main',
        query: {
          provinceId: that.$store.state.curCityInfo.provinceId,
          cityId: that.$store.state.curCityInfo.cityId,
          productCode: 'VTP-C-MPC-5'
        }
      })
    },
    back() {
      if(this.isFromScanCode){
          wx.reLaunch({
            url: '/pages/index/main'
          })
      }else{
          wx.navigateBack()
      }
    }
  }
}
</script>
<style>
page {
  background: #c60000;
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
    top: 2694rpx;
    margin-left: -106rpx;
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;

    .button {
      position: absolute;
      width: 220rpx;
      height: 50rpx;
      line-height: 50rpx;
      background: #ffc700;
      box-shadow: 0px 14rpx 38rpx 0px rgba(189, 0, 0, 0.47);
      border-radius: 25rpx;
      text-align: center;
      font-size: 32rpx;
      font-family: Alibaba PuHuiTi;
      font-weight: 500;
      color: #ff301f;
    }
  }
}
</style>
