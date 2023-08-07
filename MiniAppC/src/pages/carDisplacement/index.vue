<template>
  <div>
    <view class="carmodel">
      <view class="car-top">
        <img :src="selectCar.Url" class="car-logo" />
        <view class="car-name">{{selectCar.vehicle}}</view>
      </view>
    </view>
    <view>
      <view class="selectedTitle">选择发动机排量</view>
      <form bindsubmit="formSubmit" report-submit>
        <view class="list">
          <view
            class="item"
            @click="bindYear(item)"
            v-for="(item, index) in displaceList"
            :key="index"
          >
            {{item}}
            <button class="getformbtn" form-type="submit"></button>
          </view>
        </view>
      </form>
    </view>
  </div>
</template>

<script>
import { GetPaiLiangByVehicleId } from '../../api'
export default {
  data () {
    return {
      selectCar: {},
      displaceList: []
    }
  },
  mounted () {
    this.selectCar = this.$route.query
    GetPaiLiangByVehicleId({ VehicleId: this.selectCar.vehicleId }).then(
      res => {
        this.displaceList = res.data
      }
    )

  },
  methods: {
    bindYear (item) {
      this.selectCar.displacement = item
    
      this.$router.replace({ path: '/pages/carYear/main', query: this.selectCar })
    
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
  background-color: #FF6324;
  .car-name {
    color: #fff;
  }
}

.selectedTitle {
  padding-left: 30rpx;
  height: 60rpx;
  line-height: 60rpx;
  font-size: 24rpx;
  font-family: PingFangSC-Medium;
  font-weight: 700;
  color: #333333;
  background-color: #eeeeee;
}
.list .item {
  position: relative;
  line-height: 90rpx;
  color: #222;
  margin-left: 32rpx;
  margin: 0 0 0 30rpx;
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
  height: 1034rpx;
}
</style>
