<template>
  <div class="demo_page">
    <div v-for="(item,index) in carArr" :key="index" v-if="!isShowTip">
      <van-swipe-cell id="swipe-cell" right-width="65" @close="onClose">
        <div class="top_one">
          <div class="top_images1">
            <img :src="item.brandUrl" alt class="img1" />
          </div>
          <div class="top_content" @click="setDefaultCar(item)">
            <div class="p1">
              {{item.vehicle}}
              <p :class="[{'distxt1':item.defaultCar}]">{{item.defaultCar?'默认':''}}</p>
            </div>
            <p class="p2">{{item.salesName}}</p>
            <p class="p3">{{item.carNumber}}</p>
          </div>
          <div class="top_images2" @tap="editClcik(item)">
            <!-- <div class="top_images2"> -->
            <img src="https://m.aerp.com.cn/mini-app-main/order_edit_icon.png" alt class="img2" />
          </div>
        </div>
        <div slot="right" class="clear" @click="delCar(item.carId)">删除</div>
      </van-swipe-cell>
    </div>
    <div v-if="isShowTip" class="tips2" :style="{height:windowHeight}">
      <img :src="src2" alt mode="widthFix" class="elseimg" />
      <p>暂无车型，请点击下方新增车辆按钮添加车型</p>
    </div>
    <!-- <p class="tips">请谨慎填写正确的车牌，每个账户最多可添加5次</p> -->
    <div class="position">
      <van-button round size="large" color="#FF6324" type="default" @click="toAddCar">＋ 新增车辆</van-button>
    </div>
  </div>
</template>
<script>
import eventBus from '../../utils/eventBus.js'
export default {
  data() {
    return {
      msg: '',
      windowHeight: '',
      src2: `${this.globalData.imgPubUrl}mine_appointment_null.png` // 暂无车辆图片
    }
  },
  computed: {
    carArr() {
      return this.$store.state.carArr
    },
    isShowTip() {
      if (this.$store.state.carArr.length === 0) {
        return true
      } else {
        return false
      }
    }
  },
  mounted() {
    this.$store.dispatch('getCarArr')
    let that = this
    wx.getSystemInfo({
      success(res) {
        let clientHeight = res.windowHeight
        let clientWidth = res.windowWidth
        let ratio = 750 / clientWidth
        that.windowHeight = clientHeight * ratio - 88 + 'rpx'
        // that.windowHeight1 = clientHeight * ratio + 'rpx'
      }
    })
  },
  //     beforeDestroy() {
  //   eventBus.$off('isonShow1', this.isShow)
  // },
  methods: {
    selectCar(item) {},
    change(msg) {
      this.msg = msg
    },
    toAddCar() {
      this.$router.push('/pages/carBrand/main')
    },
    editClcik(item) {
      console.log(111, item)
      // JSON.stringify(item)
      this.$router.push({
        path: '/pages/vehicleInformation/main',
        query: { item: JSON.stringify(item) }
      })
    },
    delCar(carId) {
      this.$store.dispatch('deleteUserCar', carId)
    },
    setDefaultCar(item) {
      if (this.$route.query.isFromPerson) {
        return false
      }

      this.$store.dispatch('setUserDefaultVehicle', item.carId).then(() => {
        eventBus.$emit('changeCar')
        eventBus.$emit('isonShow', true)
        const eventChannel = this.$mp.page.getOpenerEventChannel()
        // 监听acceptDataFromOpenerPage事件，获取上一页面通过eventChannel传送到当前页面的数据
        eventChannel.emit('dataFromOpenedPage', { data: item })
        this.globalData.isCarShow = true
        wx.navigateBack({})
      })
    },
    onClose(event) {
      const { position, instance } = event.detail
      switch (position) {
        case 'right':
          Dialog.confirm({
            message: '确定删除吗？'
          }).then(() => {
            instance.close()
          })
          break
      }
    }
  }
}
</script>
<style scoped>
.demo_page {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #f3f3f3;
}
.top_one {
  width: 100%;
  height: 180rpx;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  padding: 15rpx 30rpx;
  box-sizing: border-box;
  border-bottom: 1rpx solid #f3f3f3;
}
.top_images1 {
  width: 150rpx;
  height: 150rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.top_images1 .img1 {
  width: 100rpx;
  height: 100rpx;
}
.top_images2 {
  /* visibility: hidden; */
  width: 50rpx;
  height: 150rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.top_images2 .img2 {
  width: 34rpx;
  height: 34rpx;
}
.top_content {
  width: 490rpx;
  height: 150rpx;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
}
.top_content .p1 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(51, 51, 51, 1);
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}
.distxt1 {
  width: 80rpx;
  height: 30rpx;
  background: rgba(255, 238, 231, 1);
  border-radius: 4rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
  margin-left: 20rpx;
  text-align: center;
}
.top_content .p2 {
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
}
.top_content .p3 {
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}
.tips2 {
  width: 100%;

  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  /* margin-bottom: 200rpx; */
}
.tips {
  width: 100%;
  text-align: center;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
  margin-top: 45rpx;
  margin-bottom: 200rpx;
}
.position {
  position: fixed;
  bottom: 0;
  left: 0;
  width: 100%;
  background: #ffffff;
  padding: 10rpx 30rpx;
  box-sizing: border-box;
}
.clear {
  width: 130rpx;
  height: 180rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  color: #ffffff;
  background: #f63d3d;
}
.elseimg {
  width: 215rpx;
  height: 215rpx;
}
</style>