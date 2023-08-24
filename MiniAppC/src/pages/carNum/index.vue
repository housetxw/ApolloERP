<template>
  <div class="page">
    <div class="bg">
    <view class="carNum">
      <!-- <view class="section">
        <view class="carNumFirst" bind:tap="showKeyboard" data-type="province">
          <input class="province" placeholder="沪" disabled placeholder-style="color:#D9D9D9;" value="{{carNumFist}}"/>
          <image src="https://img3.ApolloERP.org/PeccancyCheXingYi/FnfyqrrPtO9Y_Ax_1uDVRiez32gi_w56_h56.png@100Q.png"></image>
        </view>
        <input class="carNumlast" placeholder="车牌号" disabled placeholder-style="color:#D9D9D9;" bind:tap="showKeyboard" data-type="t41" value="{{carNumLast}}"/>
      </view>-->

      <view class="section">
        <input placeholder-class="place-holder" placeholder="请输入车牌号" v-model="carNum" auto-focus />
      </view>
    </view>
    <view class="carBrand">
      {{selectCar.vehicle}}
      <!--<image src="https://img2.ApolloERP.org/PeccancyCheXingYi/d947/8e45/69136df2ebc416e9648abe4d_w128_h128.png@100Q.png"></image>-->
    </view>
</div>
      <div style="margin-bottom:176rpx;"></div>
    <div class="bottom1">
      <van-button type="primary" size="large" round color="#FF6324" @tap="bindSelect">保存此车</van-button>
    </div>
  </div>

</template>

<script>
import eventBus from '../../utils/eventBus.js'
export default {
  data () {
    return {
      selectCar: {},
      carNum: ''
    }
  },
  mounted () {
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
    bindSelect (item) {
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
            eventBus.$emit('isonShow', true)
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
    isVehicleNumber (vehicleNumber) {
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
}
.bg{
  width:100%;
  background:#fff;

padding: 40rpx;

display: flex;
flex-direction: column;

align-items: center;

}
image {
  width: 28rpx;
  height: 28rpx;
}
.carNum {
  .section {
    width: 686rpx;
    height: 104rpx;
    background: rgba(243, 243, 243, 1);
    display: flex;
    align-items: center;
    box-sizing: border-box;
    padding-left: 56rpx;
    font-size: 28rpx;
    font-family: PingFangSC-Regular;
    font-weight: 400;

    color: rgba(39, 49, 62, 1);
    display: flex;
    align-items: center;
    border-radius: 45rpx;

    .carNumFirst {
      display: flex;
      align-items: center;
      width: 100rpx;
      height: 100%;
      justify-content: center;
      border-right: 1rpx solid rgba(229, 229, 229, 1);

      .province {
        width: 29rpx;
        margin-right: 12rpx;
      }
    }
    .carNumlast {
      padding-left: 14rpx;
      flex: 1;
      height: 100%;
    }
  }
  .place-holder {
    color: rgba(163, 170, 180, 1);
  }
}
.carBrand {
  width: 686rpx;
  height: 104rpx;
  margin-top: 30rpx;
  box-sizing: border-box;
  background: rgba(243, 243, 243, 1);
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 16rpx 0 56rpx;
  font-size: 28rpx;
  font-family: PingFangSC-Regular;
  font-weight: 400;
  color: rgba(39, 49, 62, 1);
  border-radius: 45rpx;

  image {
    width: 28rpx;
    height: 28rpx;
  }
}

.tip {
  width: 686rpx;
  font-size: 24rpx;
  font-family: PingFangSC-Regular;
  font-weight: 400;
  color: rgba(163, 170, 180, 1);
  line-height: 33rpx;
  margin-top: 475rpx;
}
.confirm {
  width: 100%;
  margin-top: 30rpx;
}
.list {
  display: flex;
  margin: 0 24rpx;
  margin-bottom: 0rpx;
  margin-top: 20rpx;
  width: 686rpx;
}

.first {
  margin-top: 40rpx;
}

.list .logo {
  width: 80rpx;
  height: 80rpx;
}

.list .center {
  display: flex;
  margin-left: 30rpx;
  max-width: 500rpx;
  flex-direction: column;
  justify-content: center;
}

.list .center .margin {
  margin-top: 10rpx;
  font-size: 24rpx;
  word-break: break-all;
  color: gray;
}

.center .status {
  width: 102rpx;
  height: 30rpx;
  margin-left: 6rpx;
}

.list .default {
  background: red;
  margin-left: 10px;
  padding: 6rpx;
  border-radius: 10rpx;
  font-size: 24rpx;
  color: #fff;
}

.list .right {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: flex-end;
}
.bottom1 {
  position: fixed;
  left: 0;
  right: 0;
  background: #fff;
  padding: 8rpx 45rpx 8rpx;

  bottom: 0;
}
</style>
