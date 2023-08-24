<template>
  <div class="page">
    <div class="couponItem" v-for="(item, index) in packageCard" :key="item.orderNo" @click="toComboDetailList(item.orderNo)">
      <div class="itemL">
        <div class="tit">{{item.name}}</div>

        <div class="date">购买日期：{{item.createTime}}</div>
        <div class="des">订单号：{{item.orderNo}}</div>
      </div>
      <div class="itemR">立即使用</div>
    </div>
    <!-- <div class="couponItem gray">
      <div class="itemL">
        <div class="tit">标准洗套餐卡</div>

        <div class="date">有效期：2021-01-01至2021-12-31</div>
        <div class="des">可用范围：到店使用或预约上门</div>
      </div>
      <div class="itemR">立即使用</div>
    </div> -->
  </div>

</template>

<script>
import { GetPackageCardMainInfo } from '../../api'
export default {
  data() {
    return {
      packageCard: []
    }
  },
  mounted() {
    this.getPackageCardMainInfo()
  },
  methods: {
    toComboDetailList(orderNo) {
      wx.navigateTo({
        url: '/pages/comboDetail/main?orderNo=' + orderNo
      })
    },
    getPackageCardMainInfo() {
      GetPackageCardMainInfo({}).then(res => {
        this.packageCard = res.data
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

  .couponItem {
    margin-top: 31rpx;
    width: 690rpx;
    height: 247rpx;
    padding: 0 51rpx 0 40rpx;
    box-sizing: border-box;
    display: flex;
    align-items: center;
    justify-content: space-between;
    background: url(https://m.aerp.com.cn/mini-app-main/myCombo.png) no-repeat;
    background-size: contain;
    .itemR {
      width: 31rpx;

      font-size: 32rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: #ffffff;
      line-height: 48rpx;
    }
    .itemL {
      .tit {
        width: 509rpx;
        height: 59rpx;
        font-size: 50rpx;
        font-family: Source Han Sans CN;
        font-weight: 500;
        color: #ffffff;
        line-height: 59rpx;
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
      }
      .des {
        height: 18rpx;
        font-size: 18rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: #222222;
        line-height: 49rpx;
        margin-top: 55rpx;
      }
      .date {
        height: 23rpx;
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: #f2f2f2;
        line-height: 38rpx;

        margin-top: 22rpx;
      }
    }
  }
  .gray {
    background: url(https://m.aerp.com.cn/mini-app-main/myCombo_gray.png)
      no-repeat !important;
    background-size: contain !important;
  }
}
</style>
