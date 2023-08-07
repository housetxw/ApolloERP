<template>
  <div class="page">
    <div class="comboItem" v-for="(item, index) in packageCard" :key="index" @click="toDetail(item)">
      <image :src="item.image1" style="width:640rpx;height:385rpx;padding:25rpx"></image>
      <div class="comboItemB">
        <div class="comboItemTit">{{item.name}}</div>
        <div class="comboItemBBottom">
          <div class="comboItemDec">{{item.displayName}}</div>
          <div class="seckillItemPrice">¥{{item.salesPrice}}</div>
        </div>

      </div>
    </div>
  </div>

</template>

<script>
import { GetPackageCardProduct } from '../../api'
export default {
  data() {
    return {
      packageCard: []
    }
  },
  mounted() {
    this.getPackageCardProduct()
  },
  methods: {
    toDetail(itemm) {
      let that = this
      this.$router.push({
        path: '/pages/detailsPages/main',
        query: {
          provinceId: that.$store.state.curCityInfo.provinceId,
          cityId: that.$store.state.curCityInfo.cityId,
          productCode: itemm.productCode
        }
      })
    },
    getPackageCardProduct() {
      GetPackageCardProduct({
        PageSize: 100,
        PageIndex: 1
      })
        .then(res => {
          this.packageCard = res.data.items
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

  .comboItem {
    margin-top: 20rpx;
    width: 690rpx;
    height: 580rpx;
    background: #ffffff;
    box-shadow: 0px 0px 15rpx 0px rgba(44, 49, 140, 0.11);
    border-radius: 10rpx;

    .comboItemB {
      border-top: 2rpx #ececec solid;
      height: 143rpx;
      display: flex;
      flex-direction: column;
      justify-content: center;
      padding: 0 25rpx;
      .comboItemTit {
        height: 34rpx;
        font-size: 36rpx;
        font-family: PingFang SC;
        font-weight: bold;
        color: #333333;
        line-height: 21rpx;
      }
      .comboItemBBottom {
        margin-top: 21rpx;
        display: flex;
        align-items: center;
        justify-content: space-between;
        height: 26rpx;
        font-size: 28rpx;
        font-family: PingFang SC;
        font-weight: 500;
        color: #999999;
        line-height: 21rpx;
        .seckillItemPrice {
          height: 35rpx;
          font-size: 46rpx;
          font-family: PingFang SC;
          font-weight: bold;
          color: #ff6214;
          line-height: 21rpx;
        }
      }
    }
  }
}
</style>
