<template>
  <div class="page">
    <div class="couponItem" v-for="(item, index) in componDetailList" :key="item.code" :class="{'gray':item.status!='未使用'}" @click="toComboCouponDetail(item)">
      <div class="itemL">
        <div class="tit">{{item.productName}}</div>
        <div class="des">不可与其他券叠加使用；</div>
        <div class="des">不可与店内其他优惠同享；</div>
        <div class="des">核销码：{{item.code}}；</div>
        <div class="date">有效期：{{item.showStartValidTime}}至{{item.showEndValidTime}}</div>
      </div>
      <div class="itemR">{{item.status}}</div>
    </div>
  </div>

</template>

<script>
import { GetOrderPackageCards } from '../../api'
let orderNo = ''
export default {
  data() {
    return {
      componDetailList: []
    }
  },
  onLoad(op) {
    orderNo = op.orderNo
  },
  mounted() {
    this.getOrderPackageCards()
  },
  methods: {
    toComboCouponDetail(itemm) {
      if (itemm.status != '未使用') {
        return false
      }
      this.$router.push({
        path: '/pages/comboCouponDetail/main',
        query: {
          code: itemm.code
        }
      })
    },
    getOrderPackageCards() {
      GetOrderPackageCards({
        PageSize: 50,
        PageIndex: 1,
        SourceOrderNo: orderNo
      })
        .then(res => {
          this.componDetailList = res.data
        })
        .catch(err => {
          console.log(`首页数据信息获取失败函数,${err}`)
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
  .gray {
    background: url(https://m.aerp.com.cn/mini-RG-main/comboDetail_gray.png)
      no-repeat !important;
    background-size: contain !important;
  }
  .couponItem {
    margin-top: 31rpx;
    width: 690rpx;
    height: 280rpx;
    padding: 0 59rpx 0 66rpx;
    box-sizing: border-box;
    display: flex;
    align-items: center;
    justify-content: space-between;
    background: url(https://m.aerp.com.cn/mini-RG-main/comboCoupon.png)
      no-repeat;
    background-size: contain;
    .itemR {
      width: 32rpx;
      font-size: 32rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: #ffffff;
      line-height: 35rpx;
    }
    .itemL {
      .tit {
        margin-top: 20rpx;
        width: 80%;
        height: auto;
        font-size: 30rpx;
        font-family: Source Han Sans CN;
        font-weight: 500;
        color: #ffffff;
        line-height: 35rpx;
        margin-bottom: 20rpx;
      }
      .des {
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: #f2f2f2;
        line-height: 38rpx;
      }
      .date {
        height: 23rpx;
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: #ffffff;
        line-height: 35rpx;
        margin-top: 10rpx;
      }
    }
  }
}
</style>
