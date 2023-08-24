<template>
  <div class="demo_page">
    <div class="logo_img">
      <img src="https://m.aerp.com.cn/mini-app-main/login_logo_img.png" alt />
    </div>
    <div class="button">
      <van-cell-group>
        <van-field
          :left-icon="leftIcon"
          :value="value1"
          type="number"
          placeholder="请输入手机号"
          @input="inputCode"
          @change="completeInputnumber"
        />
        <van-field
          :left-icon="rightIcon"
          :value="value2"
          type="number"
          placeholder="请输入短信验证码"
          @change="completeInputduanxin"
          @input="inputdaunxin"
          style="margin-top:7rpx"
        >
          <van-button round slot="button" size="small" :color="color" @click="sendClick">{{yzmtext}}</van-button>
        </van-field>
      </van-cell-group>
    </div>
    <div class="bottom_btn">
      <van-button
        round
        type="primary"
        size="large"
        @click="onClickButtonSubmit"
        :color="signInColor"
      >立即登录</van-button>
    </div>
    <p class="position">
      登录即代表您已阅读并同意
      <text @click="privacyClick">《致大用户协议》</text>
    </p>
  </div>
</template>
<script>
import { RgLogin, SendVerifyCodeSms } from '../../api'
import eventBus from '../../utils/eventBus.js'
export default {
  data() {
    return {
      leftIcon: `${this.globalData.imgPubUrl}login_iphone.png`,
      rightIcon: `${this.globalData.imgPubUrl}login_yzm.png`,
      value1: '',
      value2: '',
      color: '#FF6324',
      signInColor: '#E8E8E8',
      yzmtext: '发送验证码',
      isLock: false
    }
  },
  methods: {
    // input改变事件(手机号)
    completeInputnumber(event) {
      this.value1 = event.mp.detail
      if (this.value1) {
        this.leftIcon =
          'https://m.aerp.com.cn/mini-app-main/login_iphone_click.png'
      }else{
        this.leftIcon = `${this.globalData.imgPubUrl}login_iphone.png`
      }
      if (this.value1 && this.value2) {
        this.signInColor = '#FF6324'
      }else{
        this.signInColor = '#E8E8E8'
      }
    },
    // input改变事件(验证码)
    completeInputduanxin(event) {
      this.value2 = event.mp.detail
      if (this.value2) {
        this.rightIcon =
          'https://m.aerp.com.cn/mini-app-main/login_yzm_click.png'
      }else{
        this.rightIcon = `${this.globalData.imgPubUrl}login_yzm.png`
      }
      if (this.value1 && this.value2) {
        this.signInColor = '#FF6324'
      }else{
        this.signInColor = '#E8E8E8'
      }
    },
    // input事件(手机号)
    inputCode() {
      //  if (this.value1 !== '') {
      //   this.leftIcon =
      //     'https://m.aerp.com.cn/mini-app-main/login_iphone_click.png'
      // }
      //  if (this.value1 !== '' && this.value2 !== '') {
      //   this.signInColor = '#FF6324'
      // }
    },
    // input事件(验证码)
    inputdaunxin() {
      // if (this.value2 !== '') {
      //   this.rightIcon =
      //     'https://m.aerp.com.cn/mini-app-main/login_yzm_click.png'
      // }
      // if (this.value1 !== '' && this.value2 !== '') {
      //   this.signInColor = '#FF6324'
      // }
    },
    // 用户协议点击事件
    privacyClick() {
      this.$router.push('/pages/privacy/main')
    },
    // 立即登录事件
    onClickButtonSubmit() {
      if (this.value1 === '' || this.value2 === '') {
        wx.showModal({
          content: '请填入手机号或验证码',
          confirmText: '确定',
          confirmColor: '#FF6324',
          success(res) {
            if (res.confirm) {
            } else if (res.cancel) {
            }
          }
        })
        return false
      }
      let data = {
        mobileNumber: this.value1,
        verificationCode: this.value2,
        shareUserId: this.globalData.shareUserId
      }
      RgLogin(data).then(res => {
        this.globalData.userInfo = res.data.userInfo
        this.globalData.tokenInfo = res.data.tokenInfo
        wx.setStorageSync('tokenInfo', res.data.tokenInfo)
        eventBus.$emit('login')
        wx.switchTab({
          url: '/pages/index/main'
        })
      })
    },
    // 验证码登录
    sendClick() {
      if (!this.isLock) {
        if (!/^1[3456789]\d{9}$/.test(this.value1)) {
          wx.showToast({ title: '手机号输入有误', icon: 'none' })
          return false
        } else {
          this.isLock = true
          SendVerifyCodeSms({ mobilePhone: this.value1 })
          this.color = '#E8E8E8'
          let that = this
          let i = 60
          let timer = setInterval(function() {
            i--
            that.yzmtext = '已发送(' + i + 's)'
            if (i === 0) {
              clearInterval(timer)
              that.yzmtext = '发送验证码'
              that.color = '#FF6324'
              that.isLock = false
            }
          }, 1000)
        }
      }
    }
  }
}
</script>
<style>
.van-cell {
  align-items: center;
}
</style>
<style scoped>
.demo_page {
  width: 100%;
}
.logo_img {
  width: 100%;
  height: 300rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.logo_img img {
  width: 165rpx;
  height: 165rpx;
}
.button {
  width: 100%;
  padding: 60rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.bottom_btn {
  width: 100%;
  padding: 0 58rpx;
  box-sizing: border-box;
}
.position {
  position: fixed;
  bottom: 45rpx;
  left: 147rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(173, 173, 173, 1);
}
.position text {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
}
.van-field__body,
.van-field__body--number,
.van-field__body--ios {
  margin-top: -2rpx;
}
</style>
async function async1() {
  
    await async2();
    await async2();

}
async function async2() {

}



setTimeout(function() {

}, 0)

async1();

new Promise(function(resolve) {

    resolve();
}).then(function() {

});
