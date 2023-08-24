<template>
  <div class="demo_page" :style="{minHeight:windowHeight1}">
    <div class="top_input">
      <input
        type="text"
        placeholder="请输入优惠码"
        placeholder-style="color:#ccc;fontSize:28rpx;"
        :value="value"
        @change="onChange"
      />
      <van-button round type="default" size="small" color="#FF6324" @tap="sureDuihuan">确认兑换</van-button>
    </div>
    <div class="box">
      <div v-if="couponarr.length > 0">
        <div
          class="coupon_class"
          v-for="(item,index) in couponarr"
          :key="index"
          @tap="couponClick(item,index)"
        >
          <div class="coupon_class_left">
            <!-- item.status == 1 || item.status == 2 -->
            <p :class="['orange',{'orange_color':item.orderEnabled?false:true}]">
              ￥
              <span>{{item.value}}</span>券
            </p>
            <p :class="['orange',{'orange_color':item.orderEnabled?false:true}]">{{item.name}}</p>
          </div>
          <div class="coupon_class_right">
            <p :class="['text1',{'text_color':item.orderEnabled?false:true}]">{{item.displayName}}</p>
            <p :class="['text2',{'text_color':item.orderEnabled?false:true}]">
              <span>有效期：{{item.startValidTime}}-{{item.endValidTime}}</span>
              <img
                v-if="item.orderEnabled&&isOrder"
                class="byservice_right"
                :src="item.isSelect?'https://m.aerp.com.cn/mini-app-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-app-main/maintenance_radio.png'"
                alt
              />
            </p>
            <p :class="['text3',{'text_color':item.orderEnabled?false:true}]">{{item.useRuleDesc}}</p>
          </div>
          <!-- 满减灰 -->
          <div class="coupon_img1" v-if="item.status == 1 || item.status == 2">
            <img src="https://m.aerp.com.cn/mini-app-main/mineyhq_labeltwo.png" alt />
          </div>
          <!-- 满减高亮 -->
          <div class="coupon_img1" v-if="item.type == 1 && item.status != 2 && item.status != 1 ">
            <img src="https://m.aerp.com.cn/mini-app-main/mineyhq_label_pic.png" alt />
          </div>
          <!-- 已使用灰 -->
          <div class="coupon_img2" v-if="item.status == 1">
            <img src="https://m.aerp.com.cn/mini-app-main/mineyhq_uesd_icon.png" alt />
          </div>
          <!-- 已过期灰 -->
          <div class="coupon_img2" v-if="item.status == 2">
            <img src="https://m.aerp.com.cn/mini-app-main/square_over.png" alt />
          </div>
          <!-- 已作废 -->
          <div class="coupon_img2" v-if="item.status == 3">
            <img src="https://m.aerp.com.cn/mini-app-main/Cancelled.png" alt />
          </div>
        </div>
      </div>
      <div v-else class="null_img" :style="{height:windowHeight}">
        <img src="https://m.aerp.com.cn/mini-app-main/mine_appointment_null.png" alt />
        <text>暂无优惠券</text>
      </div>
      <!-- <img
        :src="itema.isDefaultExpand?'https://m.aerp.com.cn/mini-app-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-app-main/maintenance_radio.png'"
        alt
      />-->
    </div>
    <div class="bottom" @click="noCoupon1" v-if="isOrder">
      <span>不使用优惠券</span>
      <img
        class="byservice_right"
        :src="noCoupon?'https://m.aerp.com.cn/mini-app-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-app-main/maintenance_radio.png'"
        alt
      />
    </div>
  </div>
</template>
<script>
import { GetUserCoupon, PostRedeemCoupons } from '../../api'
import eventBus from '../../utils/eventBus.js'
export default {
  data() {
    return {
      noCoupon: false,
      couponarr: [], // 用户优惠券数组
      windowHeight: '',
      windowHeight1: '',
      value: '', // 用户输入的值
      isOrder: true
    }
  },
  methods: {
    noCoupon1() {
      this.noCoupon = true
      this.couponarr.forEach(element => {
        element.isSelect = false
      })
      eventBus.$emit('selectCoupon', {
        displayName: '不使用优惠券',
        userCouponId: 0
      })

      wx.navigateBack({})
    },
    // 确认兑换
    sureDuihuan() {
      let that = this
      if (that.value != '') {
        wx.showModal({
          title: '兑换确认',
          content: '确认兑换该优惠券？兑换后优惠券不可退改。',
          success(res) {
            if (res.confirm) {
              // 根据userId和优惠码(couponCode)兑换优惠券
              let cuopondata = {
                userId: that.globalData.userInfo.userId,
                code: that.value
              }
              // 获取可预约订单
              PostRedeemCoupons(cuopondata)
                .then(res => {
                  if (res.data) {
                    wx.showToast({
                      title: '兑换成功',
                      icon: 'success',
                      duration: 2000
                    })
                  } else {
                    wx.showToast({
                      title: res.message,
                      icon: 'none',
                      duration: 2000
                    })
                  }
                })
                .catch(err => {})
            } else if (res.cancel) {
            }
          }
        })
      } else {
        wx.showToast({
          title: '请输入优惠码',
          icon: 'none',
          duration: 2000
        })
      }
    },
    // input值改变事件
    onChange(e) {
      this.value = e.mp.detail.value
    },
    // 优惠券点击事件
    couponClick(item, index) {
      if (!this.isOrder) {
        return false
      }

      if (item.status == 0) {
        // eventBus.$emit('selectCoupon',item)
        // wx.navigateBack({})
        if (item.orderEnabled) {
          this.couponarr.forEach(element => {
            element.isSelect = false
          })
          this.couponarr[index].isSelect = true
          this.noCoupon = false
          eventBus.$emit('selectCoupon', item)

          wx.navigateBack({})
        } else {
          wx.showToast({ title: '此优惠券不可用哦', icon: 'none' })
          return false
        }
      }
    },
    // 页面刚加载事件
    PageOnload(userCouponId, entranceType) {
      let that = this
      // 根据userId获取用户优惠券列表
      let data = {
        entranceType,
        UserId: that.globalData.userInfo.userId,
        UserCouponId: userCouponId,
        PageIndex: 1,
        PageSize: 100
      }
      GetUserCoupon(data)
        .then(res => {
          that.couponarr = res.data.items
          that.couponarr.forEach(item => {
            that.$set(item, 'isSelect', false)
          })
        })
        .catch(err => {})
    }
  },
  onLoad(options) {
    if (options.isOrder == 'yes') {
      this.isOrder = true
      let arr = JSON.parse(options.userCouponId)

      this.PageOnload(arr, 'Order')
    } else {
      this.isOrder = false
      this.PageOnload([], 'Mine')
    }
    this.globalData.serviceType = 'toShop'
    console.log(this.globalData.serviceType)
  },
  mounted() {
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
        that.windowHeight = clientHeight * ratio - 160 + 'rpx'
        that.windowHeight1 = clientHeight * ratio + 'rpx'
      }
    })
    eventBus.$emit('isonShow', false)
    eventBus.$emit('isonShow1', false)
  },
  onUnload: function() {
    this.value = ''
  }
}
</script>
<style scoped>
.byservice_right {
  width: 36rpx;
  height: 36rpx;
}
.bottom {
  position: fixed;
  bottom: 0;
  display: flex;
  align-items: center;
  height: 98rpx;
  width: 100%;
  box-sizing: border-box;
  background: rgba(255, 255, 255, 1);
  justify-content: space-between;
  padding: 0 58rpx 0 58rpx;
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 500;
  color: rgba(51, 51, 51, 1);
}
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  background: #f3f3f3;
}
.top_input {
  width: 100%;
  height: 100rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-around;
  align-items: center;
  background: #ffffff;
}
.top_input input {
  width: 505rpx;
  height: 65rpx;
  background: rgba(248, 248, 248, 1);
  border-radius: 33rpx;
  padding-left: 30rpx;
}
.top_input input::-webkit-input-placeholder {
  color: #cccccc;
}
.box {
  width: 100%;
  padding: 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  justify-content: center;
}
.coupon_class {
  position: relative;
  width: 690rpx;
  /* height:200rpx; */
  background: rgba(255, 255, 255, 1);
  box-shadow: 0rpx 8rpx 18rpx 0rpx rgba(226, 226, 226, 0.5);
  border-radius: 10rpx;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  padding: 20rpx;
  box-sizing: border-box;
  margin-bottom: 20rpx;
}
.coupon_class_left {
  width: 25%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.coupon_class_right {
  width: 70%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: flex-start;
  margin-left: 20rpx;
}
.orange {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
  text-align: center;
}
.orange span {
  font-size: 58rpx;
  font-family: Bebas Neue;
  font-weight: bold;
}
.text1 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}
.text2 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
  margin-top: 6rpx;
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.text3 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(173, 173, 173, 1);
  margin-top: 25rpx;
}
.orange_color {
  color: rgba(173, 173, 173, 1);
}
.text_color {
  color: rgba(208, 207, 207, 1);
}
.coupon_img1 {
  position: absolute;
  top: 0;
  right: 0;
}
.coupon_img2 {
  position: absolute;
  top: 40rpx;
  right: 70rpx;
}
.coupon_img1 img {
  width: 71rpx;
  height: 66rpx;
}
.coupon_img2 img {
  width: 84rpx;
  height: 82rpx;
}
.null_img {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.null_img img {
  width: 215rpx;
  height: 215rpx;
}
</style>