<template>
  <div class="page">
    <div class="secTop">
      <div class="time">距结束 <van-count-down use-slot :time="time" @change="onChange" style="margin-left:20rpx">
          <text class="item">{{ timeData.hours }}</text><span class="colon">:</span>
          <text class="item">{{ timeData.minutes }}</text><span class="colon">:</span>
          <text class="item">{{ timeData.seconds }}</text>
        </van-count-down>
      </div>
    </div>
    <div class="seckillItem" v-for="(item, index) in seckillList" :key="index" @click="toDetail(item)">
      <image :src="item.image1" mode="aspectFit" style="width:192rpx;height:252rpx;margin:0 55rpx 0 55rpx"></image>
      <div class="seckillItemR">
        <div class="seckillItemTit">{{item.productName}}</div>
        <div class="seckillItemDec">{{item.description}}</div>

        <div class="seckillItemPrice">¥{{item.price}} <span class="subPrice">¥{{item.salesPrice}}</span></div>
        <div class="seckillItemRBottom"> <span>剩余数量:{{item.num}}</span><span class="button">立即【测试】</span> </div>

      </div>
    </div>
  </div>

</template>

<script>
import { GetActiveFlashSaleConfigs } from '../../api'
export default {
  data() {
    return {
      seckillList: [],
      time: 0,
      timeData: {}
    }
  },
  mounted() {
    let myDate = new Date()
    this.time =
      (23 - myDate.getHours()) * 60 * 60 * 1000 +
      (59 - myDate.getMinutes()) * 60 * 1000 +
      (60 - myDate.getSeconds()) * 1000
    this.getActiveFlashSaleConfigs()
  },
  methods: {
    onChange(e) {
      this.timeData = e.mp.detail
    },
    toDetail(itemm) {
      let that = this
      this.$router.push({
        path: '/pages/detailsPages/main',
        query: {
          provinceId: that.$store.state.curCityInfo.provinceId,
          cityId: that.$store.state.curCityInfo.cityId,
          productCode: itemm.productId,
          ProductEntrance: 'SecKill'
        }
      })
    },
    getActiveFlashSaleConfigs() {
      GetActiveFlashSaleConfigs({})
        .then(res => {
          this.seckillList = res.data
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

  .secTop {
    height: 170rpx;
    background: url(https://m.aerp.com.cn/mini-app-main/seckTop.png) no-repeat;
    background-size: contain;
    width: 100%;
    .time {
      display: flex;
      align-items: center;
      justify-content: center;
      margin-top: 115rpx;

      font-size: 29rpx;
      font-family: PingFangSC;
      font-weight: 400;
      color: #212121;
      .item {
        display: inline-block;
        width: 35rpx;
        height: 35rpx;
        line-height: 35rpx;

        text-align: center;
        background-color: #313131;
        border-radius: 5rpx;

        font-size: 29rpx;

        font-family: zihun59hao;
        font-weight: normal;
        color: #fff;
        .colon {
          width: 4rpx;
          font-size: 29rpx;
          font-family: DINAlternate;
          font-weight: bold;
          color: #313131;
          text-align: center;
        }
      }
    }
  }
  .seckillItem {
    width: 689rpx;
    height: 305rpx;
    background: #ffffff;
    box-shadow: 0px 0px 15rpx 0px rgba(44, 49, 140, 0.11);
    border-radius: 10rpx;
    display: flex;
    align-items: center;
    margin-top: 31rpx;
    justify-content: center;
    .seckillItemR {
      width: 389rpx;
      padding-right: 23rpx;
      .seckillItemTit {
        height: 23rpx;
        line-height: 23rpx;
        font-size: 24rpx;
        font-family: PingFang SC;
        font-weight: 500;
        color: #333333;
        margin-bottom: 13rpx;
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
      }
      .seckillItemDec {
        height: 20rpx;
        line-height: 20rpx;
        font-size: 20rpx;
        font-family: PingFang SC;
        font-weight: 500;
        color: #ff6214;
        margin-bottom: 43rpx;
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
      }
      .seckillItemPrice {
        height: 26rpx;
        font-size: 34rpx;
        font-family: PingFang SC;
        font-weight: bold;
        color: #ff6214;
        margin-bottom: 61rpx;
        .subPrice {
          height: 19rpx;
          font-size: 24rpx;
          font-family: PingFang SC;
          font-weight: 500;
          text-decoration: line-through;
          color: #999999;
        }
      }
      .seckillItemRBottom {
        display: flex;
        justify-content: space-between;
        align-items: center;
        font-size: 24rpx;
        font-family: PingFang SC;
        font-weight: 500;
        color: #ff6214;
        .button {
          width: 142rpx;
          height: 65rpx;
          background: linear-gradient(90deg, #ff3130, #ff6214);
          border-radius: 33rpx;
          line-height: 65rpx;
          text-align: center;
          font-size: 26rpx;
          font-family: PingFang SC;
          font-weight: bold;
          color: #ffffff;
        }
      }
    }
  }
}
</style>
