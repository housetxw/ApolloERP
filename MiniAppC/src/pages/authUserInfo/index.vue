<template>
  <div>
    <view class="page-wp">
      <view class="title">欢迎加入致大养车！</view>
      <view class="info">
        <view class="info-tit">请确认授权以下信息</view>
        <view class="info-desc">获取你的公开信息（昵称、头像等）</view>
      </view>
      <view class="btn">
        微信授权
        <!-- <button class="getformbtn" open-type="getUserInfo" @getuserinfo="getUserInfo"></button> -->        
          <!-- open-type="openSetting"   -->
        <button
          class="getformbtn"
          @click="getUserProfile"
        ></button>
      </view>
      <view class="subInfo" @click="notAuth">暂不授权</view>
    </view>
  </div>
</template>

<script>
import { GetWxOpenId } from '../../api'

export default {
  data() {
    return {}
  },

  created() {},
  methods: {
    notAuth() {
      this.$router.push({ path: '/pages/index/main', isTab: true })
    },
    getUserProfile(e) {
      // 推荐使用wx.getUserProfile获取用户信息，开发者每次通过该接口获取用户个人信息均需用户确认
      // 开发者妥善保管用户快速填写的头像昵称，避免重复弹窗
      console.log(113344, e)

      wx.getUserProfile({
        desc: '用于完善会员资料', // 声明获取用户个人信息后的用途，后续会展示在弹窗中，请谨慎填写
        success: (res) => {
          console.log(113355, res)
          this.globalData.nickName = res.userInfo.nickName
          this.globalData.gender = res.userInfo.gender
          this.globalData.avatarUrl = res.userInfo.avatarUrl
          let that = this
          wx.login({
            success(res1) {
              console.log(113366, res1)
              if (res1.code) {
                GetWxOpenId({
                  wxCode: res1.code,
                  encryptedData: res.encryptedData,
                  iv: res.iv,
                  PlatForm: 2
                }).then(res2 => {
                  console.log(113377, res2)
                  that.globalData.openId = res2.data.openId
                  that.globalData.unionId = res2.data.unionId
                  that.$router.push('/pages/selectLoginWay/main')
                })
              }
            }
          })
         
        }
      })
    },
    getUserInfo(res) {
      console.log(113322, res)
      this.globalData.nickName = res.mp.detail.userInfo.nickName
      this.globalData.gender = res.mp.detail.userInfo.gender
      this.globalData.avatarUrl = res.mp.detail.userInfo.avatarUrl
      let that = this
      wx.login({
        success(res1) {
          if (res1.code) {
            GetWxOpenId({
              wxCode: res1.code,
              encryptedData: res.mp.detail.encryptedData,
              iv: res.mp.detail.iv,
              PlatForm: 2
            }).then(res2 => {
              // console.log(`获取用户的oppenId,${res1}`)
              that.globalData.openId = res2.data.openId
              that.globalData.unionId = res2.data.unionId
              that.$router.push('/pages/selectLoginWay/main')
            })
          }
        }
      })
    }
  }
}
</script>

<style  scoped lang="scss">
.subInfo {
  text-align: center;
  margin-top: 50rpx;
}
.page-wp {
  font-family: 'PingFang SC';
  padding: 0 80rpx;
  .title {
    padding-top: 120rpx;
    color: #000000;
    font-size: 36rpx;
    line-height: 50rpx;
    font-weight: 500;
    &.bold {
      font-weight: bold;
    }
  }
  .info {
    margin-top: 84rpx;
    font-size: 26rpx;
    line-height: 36rpx;
    .info-tit {
      color: #000000;
      padding-bottom: 8rpx;
    }
    .info-desc {
      color: #999999;
    }
  }
  .btn {
    position: relative;
    margin-top: 60rpx;
    width: 100%;
    height: 100rpx;
    line-height: 100rpx;
    background-color: #1aad19;
    color: #ffffff;
    font-size: 36rpx;
    text-align: center;
    border-radius: 8rpx;
  }
}

.getformbtn {
  position: absolute;
  top: 0;
  width: 100%;
  height: 100%;
  background: transparent;
  padding: 0;
}
.getformbtn::after {
  border: none;
}
.getformbtn:hover {
  background: none;
}
</style>
