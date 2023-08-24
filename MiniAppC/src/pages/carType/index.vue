<template>
  <div>
    <view class="carmodel">
      <view class="car-top">
        <img :src="selectCar.Url" class="car-logo" />
        <view class="car-name">{{selectCar.vehicle}}</view>
      </view>
    </view>
    <view>
      <view class="selectedTitle">
        <view class="pailiang">{{selectCar.displacement}}</view>
        <img
          src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png"
          alt
          style="width:24rpx;height:24rpx;margin: 0 15rpx;"
        />
        <view class="pailiang">{{selectCar.year}}</view>
        <img
          src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png"
          alt
          style="width:24rpx;height:24rpx;margin:0 15rpx;"
        />
      </view>
      <view class="choose-title">选择车型</view>
      <form bindsubmit="formSubmit" report-submit>
        <view class="list">
          <view
            class="item"
            @click="bindSelect(item)"
            v-for="(item, index) in typeList"
            :key="index"
          >
            {{item.salesName}}
            <button class="getformbtn" form-type="submit"></button>
          </view>
        </view>
      </form>
    </view>
  </div>
</template>

<script>
import { GetVehicleSalesNameByNian } from '../../api'
import eventBus from '../../utils/eventBus.js'
export default {
  data() {
    return {
      selectCar: {},
      typeList: []
    }
  },
  mounted() {
    this.selectCar = this.$route.query

    GetVehicleSalesNameByNian({
      VehicleId: this.selectCar.vehicleId,
      PaiLiang: this.selectCar.displacement,
      Nian: this.selectCar.year
    }).then(res => {
      this.typeList = res.data
    })
  },
  methods: {
    bindSelect(item) {
      this.selectCar.salesName = item.salesName
      this.selectCar.tid = item.tid
      if (this.globalData.isEditCar) {
        eventBus.$emit('editCar', this.selectCar)
        this.$router.back()
      } else {
        this.$router.replace({
          path: '/pages/carNum/main',
          query: this.selectCar
        })
      }

      // AddUserCar({
      //   brand: this.selectCar.BrandShow,
      //   vehicle: this.selectCar.vehicle,
      //   vehicleId: this.selectCar.vehicleId,
      //   paiLiang: this.selectCar.displacement,
      //   nian: this.selectCar.year,
      //   salesName: item.salesName
      // })
    }
  }
}
</script>

<style scoped lang="scss">
view {
  box-sizing: border-box;
}
.carmodel {
  height: 84rpx;
  display: -webkit-box;
  display: -webkit-flex;
  display: flex;
  -webkit-box-align: center;
  -webkit-align-items: center;
  align-items: center;
  font-size: 24rpx;
  /*border-bottom: 1px solid #d9d9d9;*/
  background: #fff;
}

.carmodel {
  height: 100rpx;
  widows: 100%;
  padding-left: 30rpx;
}

.carmodel .car-top {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: row;
  align-items: center;
}

.carmodel .car-logo {
  height: 60rpx;
  width: 60rpx;
}

.carmodel .car-name {
  line-height: 100rpx;
  font-size: 30rpx;
  color: #333333;
  box-sizing: border-box;
  padding-left: 30rpx;
  font-weight: bolder;
}

.carmodel {
  background-color: #ff6324;
  .car-name {
    color: #fff;
  }
}

.selectedTitle {
  font-size: 28rpx;
  color: #333333;
  height: 90rpx;
  padding-left: 32rpx;
  line-height: 90rpx;
  display: -webkit-box;
  display: -webkit-flex;
  display: flex;
  background-color: #fff;
  -webkit-box-align: center;
  -webkit-align-items: center;
  align-items: center;
  .selected-icon {
    margin-left: 20rpx;
    margin-right: 20rpx;
    width: 20rpx;
    height: 100%;
    font-size: 20rpx;
    position: relative;
    &::before {
      position: absolute;
      top: 50%;
      transform: translateY(-50%);
    }
  }
}

.choose-title {
  height: 60rpx;
  line-height: 60rpx;
  padding-left: 30rpx;
  font: 700 24rpx/60rpx PingFangSC-Medium;
  background-color: #eee;
}

.list .item {
  line-height: 90rpx;
  color: #333;
  margin-left: 30rpx;
  position: relative;
  &::after {
    content: '';
    width: 100%;
    height: 1px;
    position: absolute;
    right: 0;
    bottom: 0;
    background-color: #d9d9d9;
    transform: scaleY(0.5);
  }
}

.list .item:active {
  background: #f8f8f8;
  margin: 0;
  padding-left: 30rpx;
}

.car-desc .demo {
  height: 1024rpx;
}
</style>
