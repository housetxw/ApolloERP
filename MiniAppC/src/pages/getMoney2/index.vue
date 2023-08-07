<template>
  <div class="demo_page">
    <img
      src="https://m.aerp.com.cn/mini-RG-main/getMoneyTop.png"
      alt
      style="width:360rpx;height:100rpx;margin-bottom:80rpx;margin-top:120rpx"
    />
    <div style="font-size:40rpx;margin-bottom:6rpx">请输入您要打款的支付宝账号</div>
    <div style="font-size:40rpx;margin-bottom:6rpx">向其付款</div>
    <div style="font-size:15px;margin-top:15px;width:500rpx">
      支付宝账号：
      <input
        v-model="account"
        placeholder="收款方手机号/邮箱/支付宝id"
        style="height:50rpx;border-bottom:2rpx solid #ccc;margin-top:6rpx;"
      />
    </div>
    <div style="font-size:30rpx;margin-top:30rpx;width:500rpx">
      支付宝实名：
      <input
        v-model="name"
        placeholder="请输入收款方真实姓名"
        style="height:50rpx;border-bottom:2rpx solid #ccc;margin-top:6rpx;"
      />
    </div>
    <div style="font-size:30rpx;margin-top:30rpx;width:500rpx">
      需打款金额：
      <input
        v-model="amount"
        placeholder="请输入您要打款的金额"
        style="height:50rpx;border-bottom:2rpx solid #ccc;margin-top:6rpx;"
      />
    </div>
    <div style="font-size:30rpx;margin-top:30rpx; color:red">提示：错误的账号或姓名会导致打款失败</div>

    <div class="btn" style="margin-top:100rpx" @click="weChatTransferAliPay">付款</div>
    <div style="font-size:40rpx;margin-top:30rpx; color:red" v-if="showTip">{{tip}}</div>
  </div>
</template>
<script>
import { GetWxOpenId, WeChatTransferAliPay } from '../../api'

export default {
  data() {
    return {
      canTap: true,
      name: '',
      account: '',
      showTip: false,
      tip: '',
      amount: ''
    }
  },
  onShow() {
    let that = this
    if (that.globalData.userInfo) {
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
    weChatTransferAliPay() {
      console.log(111111)
      this.canTap = false
      WeChatTransferAliPay({
        openId: this.globalData.openId,
        identity: this.account,
        userName: this.name,
        amount: this.amount
      }).then(res => {
        let {
          timeStamp,
          nonceStr,
          signType,
          paySign
        } = res.data.wxMiniAppPayCallData
        let package1 = res.data.wxMiniAppPayCallData.package
        wx.requestPayment({
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
  onLoad(opitions) {}
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

