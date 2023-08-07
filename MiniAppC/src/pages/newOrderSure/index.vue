<template>
  <div class="page">
    <div class="seckillItem">
      <image src="https://m.aerp.com.cn/mini-RG-main/changeCarIcon.png" mode="widthFix" style="width:192rpx;height:252rpx;"></image>
      <div class="seckillItemR">
        <div class="seckillItemTit">致大H1000 5W-30 全合成机...</div>
        <div class="seckillItemDec">SN PLUS级 减少油耗 抑制油泥</div>
        <div class="seckillItemPrice">¥99.99</div>
        <div class="seckillItemRBottom">立即抢购</div>
      </div>
    </div>
  </div>

</template>

<script>
export default {
  data() {
    return {
      selectCar: {},
      carNum: ''
    }
  },
  mounted() {
    this.selectCar = this.$route.query

    // GetVehicleSalesNameByNian({
    //   VehicleId: this.selectCar.vehicleId,
    //   PaiLiang: this.selectCar.displacement,
    //   Nian: this.selectCar.year
    // }).then(res => {
    //   this.typeList = res.data
    // })
  },
  methods: {
    bindSelect(item) {
      if (this.isVehicleNumber(this.carNum)) {
        this.$store
          .dispatch('addCar', {
            brand: this.selectCar.brand,
            vehicle: this.selectCar.vehicle,
            vehicleId: this.selectCar.vehicleId,
            paiLiang: this.selectCar.displacement,
            nian: this.selectCar.year,
            salesName: this.selectCar.salesName,
            tid: this.selectCar.tid,
            carNumber: this.carNum,
            defaultCar: true
          })
          .then(() => {
            eventBus.$emit('changeCar')
            wx.navigateBack({})
          })
      } else {
        wx.showModal({
          title: '提示',
          content: '您输入的车牌不符合规则,请重新输入',
          showCancel: false
        })
      }
    },
    isVehicleNumber(vehicleNumber) {
      const xreg = /^[京沪浙苏粤鲁晋冀豫川渝辽吉黑皖鄂湘赣闽陕甘宁蒙津贵云桂琼青新藏港澳]{1}[A-Z]{1}[A-Z0-9]{4,5}[A-Z0-9挂学警港澳领]{1}$/
      return xreg.test(vehicleNumber)
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
  }
}
</style>
