<template>
  <div class="demo_page" :style="{minHeight:windowHeight}">
    <div class="topContent">
      <div v-if='nickName!=null'>
        <p class="topTitle">
          <span>优惠券领取</span>
          <span>推荐人: {{nickName}}{{userTelDes}}</span>
        </p>
      </div>
      <div v-else>
        <p class="topTitle">
          <span>优惠券领取</span>
        </p>
      </div>
      <div class="bgImg">
        <div class="redeemName">
          <span>{{redeemName}}</span>
        </div>
      </div>
      <div class="bottom">
        <button class="butt" :class="['butt',{'butt1':canDraw ==false}]" @tap="toCoupon">确认领取</button>
        <!-- <button class="butt" :disabled='!canDraw' :class="['butt',{'butt1':canDraw ==false}]" @tap='toCoupon'>确认领取</button> -->
      </div>
      <div class='redeemCode'>
        <span>{{redeemCode}}</span>
      </div>
    </div>
    <div class="bContent">
      <div class="contentDes">
        <p class="desTitle">
          <span class="s1">领取使用说明</span>
        </p>
        <div class="mainDes">
          <div class="main">
            <p class="yuan"></p>
            <p class="p1">优惠券在下单时可直接抵扣现金。</p>
          </div>
          <div class="main">
            <p class="yuan"></p>
            <p class="p1">优惠券每次使用仅限一张。</p>
          </div>
          <div class="main">
            <p class="yuan"></p>
            <p class="p1">具体解释权归致大养车所有。</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import {
  GetUserInfoById,
  GetCouponActivityByCode,
  ExchangeCouponByPromotionCode
} from '../../api'
export default {
  data() {
    return {
      canDraw: false,
      message: '',
      redeemCode: 'EQGafjewiu93ehfu',
      message1: '',
      redeemName:'',
      shareUserId:'',
      nickName: null,
      userTelDes: null,
    }
  },
  onShow() {
    this.getCoupon()
    this.getShareUserInfo()
  },
  methods: {
    getShareUserInfo() {
      let that = this
      if(that.shareUserId == null || that.shareUserId == '' ){
        return
      }
      let data = {
        UserId:that.shareUserId
      }
      GetUserInfoById(data)
        .then(res => {
          that.nickName = res.data.nickName
          that.userTelDes = res.data.userTelDes
          console.log(`nick name ${that.nickName} ${that.userTelDes}`);
        })
        .catch(err => {
          console.log(`获取推荐人信息失败,${err}`)
        })
    },
    getCoupon() {
      let that = this
      let data = {
        RedeemCode: that.redeemCode
      }
      GetCouponActivityByCode(data)
        .then(res => {
          that.canDraw = res.data.canDraw
          that.message = res.data.message
          that.redeemName = res.data.name
          console.log(987654, that.canDraw)
          if (res.data.canDraw == false) {
            wx.showToast({ title: res.data.message, icon: 'none' })
          }
        })
        .catch(err => {
          console.log(`获取优惠券失败函数,${err}`)
        })
    },
    toCoupon() {
      if (this.canDraw == true) {
        let that = this
        let data = {
          userId: that.globalData.userInfo.userId,
          code: that.redeemCode
        }
        ExchangeCouponByPromotionCode(data)
          .then(res => {
            console.log(res.data, 'data')
            that.message1 = res.message
            wx.showModal({
              title: '',
              content: that.message1,
              showCancel: false,
              success(res) {
                if (res.confirm) {
                  console.log('用户点击确定')
                  that.$router.replace('/pages/index/main')
                } else if (res.cancel) {
                  console.log('用户点击取消')
                }
              }
            })
          })
          .catch(err => {
            console.log(`获取优惠券失败函数,${err}`)
          })
      } else {
        let that = this
        wx.showModal({
          title: '',
          content: that.message,
          showCancel: false,
          success(res) {
            if (res.confirm) {
              console.log('用户点击确定')
              that.$router.replace('/pages/index/main')
            } else if (res.cancel) {
              console.log('用户点击取消')
            }
          }
        })
      }
    },
    getRequest2(qs, url) {
      var s = url.replace('?', '?&').split('&')
      let re = ''
      for (var i = 1; i < s.length; i++) {
        if (s[i].indexOf(qs + '=') == 0) {
          re = s[i].replace(qs + '=', '')
        }
      }
      return re
    }
  },
  mounted() {},
  onHide() {},
  onUnload() {},
  onLoad(opitions) {
    if (opitions.q) {
      console.log(opitions.q)
      let res = decodeURIComponent(opitions.q)
      console.log(123456, res)
      this.redeemCode = this.getRequest2('code', res)
      console.log(1234567, this.redeemCode)
      this.shareUserId = this.getRequest2('shareUserId',res)
      console.log(12345678, this.shareUserId)
      this.globalData.shareUserId = this.shareUserId
    } else {
      this.redeemCode = opitions.code
      this.shareUserId = opitions.shareUserId
      this.globalData.shareUserId = opitions.shareUserId
    }

    let that = this
    // 获取系统信息
    wx.getSystemInfo({
      success: function(res) {
        // 获取可使用窗口宽度
        let clientHeight = res.windowHeight
        // 获取可使用窗口高度
        let clientWidth = res.windowWidth
        // 算出比例
        let ratio = 750 / clientWidth
        // 算出高度(单位rpx)
        that.windowHeight = clientHeight * ratio + 'rpx'
      }
    })
  }
}
</script>

<style scoped lang="scss">
.topContent {
  width: 100%;
  height: 706rpx;
  background: rgba(255, 99, 36, 1);
  padding: 58rpx 30rpx 0 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
}
.topTitle {
  display: flex;
  justify-content: space-between;
  align-items: center;
  color: #fff;
  font: 500 32rpx/1 'SourceHanSansCN-Medium';
  letter-spacing: 3rpx;
}
.bgImg {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 692rpx;
  height: 230rpx;
  text-align: center;
  font-weight: bold;
  font-size: 52rpx;
  background-image: url('https://m.aerp.com.cn/mini-RG-main/couponBg.png');
  background-repeat: no-repeat;
  background-size: 100% 100%;
  color: #ff6324;
  margin: 40rpx 0 40rpx;
  .redeemName{
    width: 600rpx;
  }
}
.bottom {
  width: 100%;
  text-align: center;
  display: flex;
  justify-content: center;
}
.butt {
  background: #fff;
  height: 90rpx;
  width: 406rpx;
  box-shadow: 0rpx 12rpx 18rpx 0rpx rgba(232, 70, 5, 0.73);
  border-radius: 45rpx;
  color: #ff6324;
  font: 32rpx/90rpx 'SourceHanSansCN-Medium';
}
.butt1 {
  background: #dddddd;
  color: #adadad;
}

.bContent {
  width: 100%;
  padding: 30rpx;
  box-sizing: border-box;
}
.contentDes {
  height: 472rpx;
  background: #fff;
  border-radius: 10rpx;
  margin-top: -120rpx;
  padding: 40rpx;
  box-sizing: border-box;
}
.desTitle {
  width: 210rpx;
  border-bottom: 11rpx solid rgba(255, 99, 36, 0.5);
  position: relative;
  margin: 30rpx;
}
.s1 {
  position: absolute;
  left: 5rpx;
  bottom: -5rpx;
  color: #333333;
  font: 500 32rpx/1 'SourceHanSansCN-Medium';
  letter-spacing: 3rpx;
}
.mainDes {
  width: 100%;
  min-height: 300rpx;
  padding: 20rpx 30rpx;
  box-sizing: border-box;
  .p1 {
    color: #666;
    font: 28rpx/34rpx 'SourceHanSansCN-Regular';
    letter-spacing: 2rpx;
  }
}
.yuan {
  width: 8rpx;
  height: 8rpx;
  background: rgba(255, 99, 36, 1);
  border-radius: 50%;
  margin-right: 20rpx;
}
.main {
  display: flex;
  align-items: center;
  margin-bottom: 30rpx;
}
.redeemCode {
  color:white;
  margin: 20rpx 0 20rpx;
  text-align: center;
  font-size: 40rpx;
}
</style>