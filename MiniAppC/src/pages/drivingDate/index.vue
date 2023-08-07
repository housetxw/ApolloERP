<template>
  <div class="demo_page">
    <div class="main">
      <van-cell-group>
        <van-cell>
          <picker mode="date" :value="drivingDate" @change="bindDateChange">
            <view class="picker">{{drivingDate}}</view>
          </picker>
        </van-cell>
      </van-cell-group>

      <p class="p">在此修改您的驾驶证到期日</p>
      <div class="bt2">
        <van-button round size="large" color="#FF6324" type="default" @tap="newAppo">保存</van-button>
      </div>
    </div>
  </div>
</template>
<script>
import { GetuserInfor, EditUserInfo } from '../../api'

export default {
  data() {
    return {
      drivingDate: ''
    }
  },
  methods: {
    nameValue(event) {
      this.drivingDate = event.mp.detail
    },
    newAppo() {
      if (this.userName == '') {
        mpvue.navigateBack({ url: 'pages/personalData/main' })
      } else if (this.userName != '') {
        EditUserInfo({
          driverLicenseExpiry: this.drivingDate
        })
          .then(res => {
            if (res.data == true) {
              wx.showToast({ title: '修改成功', icon: 'none' })

              setTimeout(() => {
                mpvue.navigateBack({ url: 'pages/personalData/main' })
              }, 1000)
            }
          })
          .catch(err => {})
      }
    },
    bindDateChange(e) {
      console.log('picker发送选择改变，携带值为', e.mp.detail.value)
      this.drivingDate = e.mp.detail.value
    }
  },
  mounted() {
    let that = this
    GetuserInfor('')
      .then(res => {
        that.drivingDate = res.data.driverLicenseExpiry
      })
      .catch(err => {})
  }
}
</script>
<style lang="scss" scoped>
.demo_page {
  width: 100%;
  height: 100%;
  background: #f3f3f3;
}

.p {
  font-size: 28rpx;
  font-family: SourceHanSansCN-Regular;
  color: #adadad;
  font-weight: 400;
  padding: 25rpx 30rpx;
}
.s {
  color: #ff6324;
}
.bt2 {
  margin: 10rpx 60rpx;
  color: #fff;
}
</style>