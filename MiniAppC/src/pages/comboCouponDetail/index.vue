<template>
  <div class="page">
    <div class="top"></div>
    <div class="content">
      <img src="https://m.aerp.com.cn/mini-RG-main/comboCouponDetailTop.png" alt="" style="width:717rpx;height:96rpx;">
      <div class="content-main">
        <div class="tit">{{couponDetail.productName}}</div>
        <div class="date">有效期：{{couponDetail.startValidTime }}至{{couponDetail.endValidTime }}</div>
        <img :src="couponDetail.qrCodeBase64String" alt="" style="width:243rpx;height:243rpx;">
        <div class="code">{{couponDetail.code}}</div>
        <div class="button" @click="toDetail()">立即预约</div>
        <div class="dash"></div>
        <div class="tipTit">
          使用须知
        </div>
        <div class="tip">
          1、如您已经到店，出示二维码即可。
        </div>
        <div class="tip">
          2、如您需要预约，可点击二维码下方的立即预约按钮进行预约。
        </div>
      </div>

    </div>
  </div>

</template>

<script>
import { GetPackageVerificationCodeDetail } from '../../api'
let code = ''
export default {
  data() {
    return {
      couponDetail: {}
    }
  },
  onLoad(op) {
    code = op.code
  },
  mounted() {
    function flatArr(arr) {
      let arr1 = []

      for (var i = 0; i < arr.length; i++) {
        if (Array.isArray(arr[i])) {
          arr1 = arr1.concat(flatArr(arr[i]))
        } else {
          arr1.push(arr[i])
        }
      }
      return arr1
    }
    console.log(88996, flatArr([1, [2, [3]]]))
    this.getPackageVerificationCodeDetail()
  },
  methods: {
    getPackageVerificationCodeDetail() {
      GetPackageVerificationCodeDetail({
        Code: code
      })
        .then(res => {
          this.couponDetail = res.data
        })
        .catch(err => {
          console.log(`首页数据信息获取失败函数,${err}`)
        })
    },
    toDetail() {
      let that = this
      this.globalData.serviceType = 'toShop'
      this.globalData.OrderType = 6
      this.globalData.ProductType = 15
      this.$router.push({
        path: '/pages/detailsPages/main',
        query: {
          provinceId: that.$store.state.curCityInfo.provinceId,
          cityId: that.$store.state.curCityInfo.cityId,
          productCode: that.couponDetail.productId,
          isShowStep: false,
          code: this.couponDetail.code
        }
      })
    }
  }
}
</script>
<style>
page {
  background: rgba(243, 243, 243, 1);
}
</style>
<style scoped lang="scss">
.page {
  display: flex;
  flex-direction: column;
  align-items: center;

  .top {
    width: 100%;
    height: 260rpx;
    background: #ff4b22;
  }
  .content {
    width: 689rpx;

    border-radius: 10rpx;
    display: flex;
    flex-direction: column;

    align-items: center;
    margin-top: -200rpx;
    justify-content: center;
    .content-main {
      background: #fff;

      display: flex;
      flex-direction: column;
      align-items: center;
      width: 100%;
      margin-top: -2rpx;
      padding-bottom: 40rpx;
      .tit {
        width: 500rpx;
        height: 62rpx;
        line-height: 53rpx;
        font-size: 56rpx;
        font-family: Source Han Sans CN;
        font-weight: bold;
        color: #ff6214;
        margin-bottom: 19rpx;
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
      }
      .code {
        height: 22rpx;
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: #666666;
        margin-top: 30rpx;
      }
      .date {
        height: 24rpx;
        font-size: 26rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: #999999;
        margin-bottom: 46rpx;
      }
      .dash {
        width: 650rpx;
        height: 1px;
        border-top: 1px dashed #999999;
        margin-top: 46rpx;
      }
      .button {
        width: 170rpx;
        height: 52rpx;
        border: 1rpx solid #ff6214;
        border-radius: 26rpx;

        font-size: 30rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: #ff6214;
        line-height: 52rpx;
        text-align: center;
        margin: 46rpx 0 0 0;
      }
      .tipTit {
        width: 650rpx;
        box-sizing: border-box;
        height: 32rpx;
        line-height: 32rpx;
        border-left: 6rpx solid #ff6214;
        font-size: 30rpx;
        font-family: Source Han Sans CN;
        font-weight: bold;
        color: #333333;
        padding-left: 17rpx;
        margin: 36rpx 0 41rpx;
      }
      .tip {
        width: 650rpx;
        padding-left: 23rpx;
        font-size: 26rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: #999999;
        margin-bottom: 15rpx;
      }
    }
  }
}
</style>
