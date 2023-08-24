<template>
  <div class="demo_page">
    <!-- <navigation-bar
      title="修改手机号码"
      :back-visible="true"
      :home-path="'/pages/index/main'"
      @test="test"
    ></navigation-bar>-->

    <div class="main">
      <van-cell-group>
        <van-field
          :value="userTelDes"
          border="false "
          :left-icon="leftIcon"
          input-class="txt1"
          type="number"
          @input="inputCode"
          @change="completeInputnumber"
        >
          <van-button
            slot="button"
            round
            size="small"
            :color="color1"
            type="default"
            @tap="getmsm"
            @click="sendClick"
          >{{ yzmtext}}</van-button>

          <!-- <van-button slot="button" size="small" >发送验证码</van-button> -->
        </van-field>
        <van-field
          :value="value2"
          :left-icon="leftIcon1"
          center
          clearable
          border="false "
          use-button-slot
          @change="completeInputduanxin"
          @input="inputdaunxin"
          type="number"
        />
      </van-cell-group>
      <p class="p">
        如无法自主修改，请拨打
        <span class="s" @tap="tel">{{phone}}</span>咨询客服服务
      </p>
      <div class="bt2">
        <van-button round size="large" :color="color2" type="default" @tap="newAppo">{{change}}</van-button>
      </div>
    </div>
  </div>
</template>
<script>
import {
  GetuserInfor,
  SendVerifyCodeSmsForChangeMobile,
  VerifyCurrentMobile
} from '../../api'
import { postTyre, postMoreshop } from '../../api'
export default {
  data() {
    return {
      phone: '021-5555-8888',
      value1: '',
      value2: '',
      isLock: false,
      change: '下一步',
      color2: '#E8E8E8',
      color1: '#FF6324',
      yzmtext: '获取验证码',
      userTelDes: '',
      leftIcon: 'https://m.aerp.com.cn/mini-app-main/Mobile_phone.png',
      leftIcon1: 'https://m.aerp.com.cn/mini-app-main/Password.png',
      userTel: ''
    }
  },
  methods: {
    tel() {
      wx.makePhoneCall({
        phoneNumber: this.phone
      })
    },

    getmsm() {},
    newAppo() {
      if (this.userTelDes == '') {
        wx.showToast({ title: '请输入手机号码', icon: 'none' })
      } else if (this.value2 == '') {
        this.color2 = '#E8E8E8'
        wx.showToast({ title: '请输入验证码', icon: 'none' })
      } else if (this.userTelDes !== '' && this.value2 !== '') {
        // 验证手机号码

        VerifyCurrentMobile({
          mobilePhone: this.userTel,
          securityCode: this.value2
        })
          .then(res => {
            if (res.data == true) {
              // this.$router.push({ path: "/pages/newNumber/main" });
              mpvue.navigateTo({ url: '/pages/newNumber/main' })
            }
          })
          .catch(err => {})

        //  this.change='确认绑定'
        // this.color2 = "#FF6324";
      }
    },
    // input改变事件(手机号)
    completeInputnumber(event) {
      this.value1 = event.mp.detail
    },
    // input改变事件(验证码)
    completeInputduanxin(event) {
      this.value2 = event.mp.detail
      if (this.value2.length == 4) {
        this.color2 = '#FF6324'
      } else if (this.value2 == '') {
        this.color2 = '#E8E8E8'
      }
    },

    sendClick() {
      if (!this.isLock) {
        if (!this.userTelDes) {
          wx.showToast({ title: '手机号输入有误', icon: 'none' })
          return false
        } else {
          this.isLock = true

          SendVerifyCodeSmsForChangeMobile({ mobilePhone: this.userTel }) //获取验证码
          this.color1 = '#E8E8E8'
          let that = this
          let i = 60
          let timer = setInterval(function() {
            i--
            that.yzmtext = '已发送(' + i + 's)'
            if (i === 0) {
              clearInterval(timer)
              that.yzmtext = '发送验证码'
              that.color1 = '#FF6324'
              that.isLock = false
            }
          }, 1000)
        }
      }
    }
  },
  mounted() {
    let that = this
    GetuserInfor('')
      .then(res => {
        that.userTelDes = res.data.userTelDes
        that.userTel = res.data.userTel
      })
      .catch(err => {})
  },

  onUnload: function() {
    this.value2 = ''
    this.color2 = '#E8E8E8'
  }
}
</script>
<style lang="scss" scoped>
.demo_page {
  width: 100%;
  height: 100%;
  background: #f3f3f3;
}
.main {
  background: #fff;
  padding-bottom: 25rpx;
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
.txt1 {
  padding: 0 6rpx;
  font-size: 27rpx;
  color: #333;
}
</style>