<template>
  <div>
  <view class="wx_quick_login">
    <image class="wx_img" src="https://m.aerp.com.cn/mini-app-main/select_phone.png"></image>
    <view class="wx_text">手机号快捷登录</view>
    <button class="wx_login_btn" form-type="submit" open-type="getPhoneNumber" @getphonenumber="getPhoneNumber"></button>
  </view>
  <view class="phone_login" @click="tapPhoneLogin">
    <image class="phone_img" src="https://m.aerp.com.cn/mini-app-main/Password.png"></image>
    <view class="phone_text">手机验证码登录</view>
    <button class="getformbtn" form-type="submit"></button>
  </view>
  </div>
</template>

<script>

import eventBus from '../../utils/eventBus.js'
import {
  ThirtyLogin
} from '../../api'
export default {


  data () {
    return {

    }
  },

  created () {

  },

  methods: {
    getPhoneNumber (e) {
      console.log(853,e)
      this.globalData.encryptedData = e.mp.detail.encryptedData
      this.globalData.iv = e.mp.detail.iv
      let data = {
        loginType: 'Test',//正式发布改为：WeChat
        openId: this.globalData.openId,
        unionId: this.globalData.unionId,
        nickName: this.globalData.nickName,
        headUrl: this.globalData.avatarUrl,
        encryptedData: 'Test',//正式发布改为：this.globalData.encryptedData,
        iv: 'Test',//正式发布改为：this.globalData.iv,
        gender: this.globalData.gender,
        sceneId: this.globalData.sceneId,
        shareUserId: this.globalData.shareUserId
      }
      ThirtyLogin(data).then((res) => {
        this.globalData.userInfo = res.data.userInfo
        this.globalData.tokenInfo = res.data.tokenInfo
        wx.setStorageSync('tokenInfo', res.data.tokenInfo)
        eventBus.$emit('login')
        wx.navigateBack({
          delta: 2
        })
        // this.$router.push('/pages/index/main')
      })
    },
    tapPhoneLogin () {
      this.$router.push('/pages/login/main')
    }
  }
}
</script>

<style scoped lang="scss">
button::after {
  display: none;
}
.wx_quick_login {
  position: relative;
  display: flex;
  flex-direction: row;
  align-items: center;
  width: 690rpx;
  height: 96rpx;
  margin: 268rpx 30rpx 40rpx 30rpx;
  background-color: #1aad19;
  border-radius: 10rpx;
  box-sizing: border-box;
  padding-left: 180rpx;
  .wx_img {
    width: 40rpx;
    height: 36rpx;
  }
  .wx_text {
    font-family: PingFang SC;
    font-size: 32rpx;
    color: #ffffff;
    line-height: 32rpx;
    margin-left: 40rpx;
  }
  .wx_login_btn {
    position: absolute;
    left: 0;
    top: 0;
    width: 690rpx;
    height: 96rpx;
    opacity: 0;
  }
}

.phone_login {
  position: relative;
  display: flex;
  flex-direction: row;
  align-items: center;
  width: 690rpx;
  height: 96rpx;
  margin: 0rpx 30rpx 20rpx 30rpx;
  border-radius: 10rpx;
  border: 1px solid #666666;
  box-sizing: border-box;
  padding-left: 180rpx;
  .phone_img {
    height: 45rpx;
    width: 40rpx;
  }
  .phone_text {
    font-family: PingFang SC;
    font-size: 32rpx;
    color: #666666;
    line-height: 32rpx;
    margin-left: 40rpx;
  }
}
</style>
