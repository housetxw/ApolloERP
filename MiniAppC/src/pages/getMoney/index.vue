<template>
  <div class="demo_page">
    <img
      src="https://m.aerp.com.cn/mini-app-main/getMoneyTop.png"
      alt
      style="width:360rpx;height:100rpx;margin-bottom:80rpx;margin-top:120rpx"
    />
    <div style="font-size:40rpx;margin-bottom:6rpx">恭喜您获得{{money}}元红包</div>
    <!-- <div style="font-size:40rpx;margin-bottom:6rpx">领取({{money}})元红包</div> -->
    <div style="font-size:40rpx;margin-bottom:6rpx">{{redeemCode}}</div>

    <div class="btn" style="margin-top:100rpx" @click="drawDecapAward">点此领红包</div>
    <div style="font-size:40rpx;margin-top:30rpx; color:red" v-if="showTip">{{tip}}</div>
  </div>
</template>
<script>
import {
  GetDecapAwardDetailByCode,
  GetWxOpenId,
  DrawDecapAwardV2
} from '../../api'

export default {
  data() {
    return {
      canTap: true,
      money: 0,
      showTip: false,
      tip: '',
      redeemCode: ''
    }
  },
  onShow() {
    let that = this
    if (that.globalData.userInfo) {
      this.getDecapAwardDetailByCode()
      return false
    }
    wx.login({
      success(res) {
        if (res.code) {
          console.log(33, res.code)
          wx.getUserInfo({
            success(userRes) {
              GetWxOpenId({
                wxCode: res.code,
                encryptedData: userRes.encryptedData,
                iv: userRes.iv,
                PlatForm: 2
              })
                .then(res1 => {
                  console.log(889911, !res1.data.isExistUser)
                  console.log(`获取用户的oppenId,${res1}`)
                  that.globalData.userInfo = res1.data.userInfo
                  that.globalData.tokenInfo = res1.data.tokenInfo
                  that.globalData.openId = res1.data.openId
                  that.globalData.unionId = res1.data.unionId
                  console.log(8899, !res1.data.isExistUser)
                  if (!res1.data.isExistUser) {
                    console.log(7788)
                    // if (true) {
                    wx.showModal({
                      title: '提示',
                      content: '您尚未注册，点此注册登录',
                      showCancel: false,
                      success(res) {
                        if (res.confirm) {
                          that.$router.push('/pages/authUserInfo/main')
                        }
                      }
                    })
                  } else {
                    that.getDecapAwardDetailByCode()
                  }
                })
                .catch(() => {
                  that.$router.push('/pages/authUserInfo/main')
                })
            },
            fail() {
              that.$router.push('/pages/authUserInfo/main')
            }
          })
        }
      }
    })
  },
  methods: {
    getRequest2(qs, url) {
      var s = url.replace('?', '?&').split('&')
      let re = ''
      for (var i = 1; i < s.length; i++) {
        if (s[i].indexOf(qs + '=') == 0) {
          re = s[i].replace(qs + '=', '')
        }
      }
      return re
    },
    getDecapAwardDetailByCode() {
      GetDecapAwardDetailByCode({
        Code: this.redeemCode,
        OpenId: this.globalData.openId
      }).then(res => {
        this.money = res.data.amount
        this.showTip = !res.data.canDraw
        this.tip = res.data.message
        this.awardId = res.data.awardId
      })
    },
    drawDecapAward() {
      console.log(111111)
      this.canTap = false
      DrawDecapAwardV2({
        payChannel: 'WeChat',
        code: this.redeemCode,
        awardId: this.awardId,
        identity: this.globalData.openId
      }).then(res => {
        let {
          timeStamp,
          nonceStr,
          signType,
          paySign
        } = res.data.wxMiniAppHbCallData
        let package1 = res.data.wxMiniAppHbCallData.package
        wx.sendBizRedPacket({
          timeStamp, // 支付签名时间戳，
          nonceStr, // 支付签名随机串，不长于 32 位
          package: package1, //扩展字段，由商户传入
          signType, // 签名方式，
          paySign, // 支付签名
          success: function(res) {
            console.log(55, res)
             this.canTap = true
          },
          fail: function(res) {
            console.log(66, res)
             this.canTap = true
          },
          complete: function(res) {
             this.canTap = true
          }
        })
       
      })
    }
  },
  mounted() {},
  onHide() {},
  onUnload() {},
  onLoad(opitions) {
    console.log(opitions.q)
    let res = decodeURIComponent(opitions.q)
    console.log(123456, res)
    this.redeemCode = this.getRequest2('code', res)
  }
}
</script>
<style>
page {
  background: white;
}
</style>
<style scoped lang="scss">
.demo_page {
  display: flex;
  flex-direction: column;
  align-items: center;
}
.btn {
  position: relative;
  margin-top: 60rpx;
  width: 600rpx;
  height: 100rpx;
  line-height: 100rpx;
  background-color: #1aad19;
  color: #ffffff;
  font-size: 36rpx;
  text-align: center;
  border-radius: 8rpx;
}
</style>

