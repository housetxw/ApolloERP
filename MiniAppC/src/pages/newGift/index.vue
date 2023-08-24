<template>
  <div class="page">
    <div class="back" @click="back">
      <image src="https://m.aerp.com.cn/mini-app-main/white_back.png" style="width:16rpx;height:29rpx;"></image>
    </div>
    <image src="https://m.aerp.com.cn/mini-app-main/new_gift_autumn_01.png" mode="widthFix" style="width:100%;position:relative;margin-top: -1rpx;">  </image>
    <image src="https://m.aerp.com.cn/mini-app-main/new_gift_autumn_02.png" mode="widthFix" style="width:100%;position:relative;margin-top: -1rpx;">   
      <div class="content">
        <div class="coupon">
          <div class="couponItem" v-for="(item, index) in couponList" :key="index">
            <div class="itemName">{{item.name}}</div>
            <div class="rowPrice"><span class="priceSymbol">¥</span><span class="price"> {{item.value}}</span> </div>
            <div class="tit">{{item.displayName}}</div>
            <!-- <div class="titShort">{{item.useRuleDesc}}</div> -->
          </div>
        </div>
        <div class="button" @click="exchangeCouponByActId">
                <image src="https://m.aerp.com.cn/mini-app-main/new_gift_autumn_button.png" style="width:555rpx;height:145rpx;"></image>
        </div>
      </div>
    </image>
     <image src="https://m.aerp.com.cn/mini-app-main/new_gift_autumn_03.png" mode="widthFix" style="width:100%;position:relative;margin-top: -1rpx;"></image>
     <image src="https://m.aerp.com.cn/mini-app-main/new_gift_autumn_04.png" mode="widthFix" style="width:100%;position:relative;margin-top: -1rpx;"></image>
  </div>

</template>

<script>
import { GetCouponDetailByActId, ExchangeCouponByActId } from '../../api'
export default {
  data() {
    return {
      couponList: [],
      CouponActivityId: '',
      isFromShare: false
    }
  },
  mounted() {
    this.getCouponDetailByActId()
  },
  onLoad(op) {
    this.CouponActivityId = op.activeId
    this.isFromShare = op.isFromShare == 'true' || op.isFromShare === true
    if (op.activedId == 1) {
      this.globalData.shareUserId = op.shareUserId
    }
  },
  
  onShareAppMessage(res) {
    let that = this
    console.log(`onShareAppMessage: function`, res)
    if (res.from === 'button') {
      // 来自页面内转发按钮
      // console.log("转达",res.target)
    }
    return {
      title: that.shopTitle,
      path: `/pages/newGift/main?shopId=0&activeId=${that.CouponActivityId }&isFromShare=true&isFromSearch=''&activedId=1&shareUserId=${
        that.globalData.userInfo?that.globalData.userInfo.userId:''
      } `,
      imageUrl:'https://m.aerp.com.cn/mini-app-main/new_gift_summer_01.png',
      success(res) {
        console.log(`res分享按钮`, res)
        // 转发成功
        wx.showToast({
          title: '分享成功',
          icon: 'success',
          duration: 2000
        })
      },
      fail(res) {
        // 分享失败
        wx.showToast({
          title: '分享失败',
          icon: 'none',
          duration: 2000
        })
      }
    }
  },

  methods: {
    back() {
      if(this.isFromShare){
          wx.reLaunch({
            url: '/pages/index/main'
          })
      }else{
          wx.navigateBack()
      }
    },
    getCouponDetailByActId() {
      GetCouponDetailByActId({
        CouponActivityId: this.CouponActivityId
      }).then(res => {
        this.couponList = res.data.couponList
      })
    },
    exchangeCouponByActId() {
      ExchangeCouponByActId({
        CouponActivityId: this.CouponActivityId
      }).then(res => {
        if (res.data) {
          wx.showModal({
            title: '提示',
            content: '优惠券领取成功，去逛一逛吧',
            showCancel: true,
            cancelText: '取消',
            cancelColor: '#000000',
            confirmText: '确定',
            confirmColor: '#3CC51F',
            success: result => {
              if (result.confirm) {
                wx.navigateBack({
                  delta: 1
                })
              }
            },
            fail: () => {},
            complete: () => {}
          })
        }
      })
    }
  }
}
</script>
<style>
page {
  background:#ffffff;
  position: relative;
}
</style>
<style scoped lang="scss">
.page {
  display: flex;
  flex-direction: column;
  align-items: center;
  .back {
    position: absolute;
    width: 50rpx;
    height: 50rpx;
    display: flex;
    align-items: center;
    justify-content: center;
    left: 43rpx;
    top: 72rpx;
    z-index: 1;
  }
  .content {
    position: absolute;
    top:175rpx;
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;
    .coupon {
      width: 600rpx;
      height: 294rpx;
      display: flex;
      overflow-x: scroll;
      .couponItem {
        width: 236rpx;
        height: 294rpx;
        margin-left: 30rpx;
        margin-right:30rpx;
        flex-shrink: 0;
        background: url(https://m.aerp.com.cn/mini-app-main/new_gift_item_bg_autumn.png)
          no-repeat;
        background-size: contain;
        display: flex;
        flex-direction: column;
        align-items: center;

        .itemName{
          height: 20rpx;
          font-size: 18rpx;
          font-family: Source Han Sans CN;
          font-weight: 300;
          color: #FF4023;
          line-height: 20rpx;
          margin-top: 30rpx;
        }

        .rowPrice{
          margin-top: 30rpx;
          font-size: 0rpx;
        }
        .priceSymbol{
          font-size: 26rpx;
          font-family: Source Han Sans CN;
          font-weight: 500;
          color: #FF4023;
        }
        .price {
          height: 50rpx;
          font-size: 48rpx;
          font-family: Source Han Sans CN;
          font-weight: 800;
          color: #FF4023;
          line-height: 50rpx;
          margin-top: 2rpx;
        }
        .tit {
          width: 154rpx;
          height:68rpx;
          font-size: 22rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          text-align: center;
          color: #88370E;
          line-height: 34rpx;
          margin-top: 60rpx;
          word-break: break-all; // 允许单词内自动换行，如果一个单词很长的话
          text-overflow: ellipsis; // 溢出用省略号显示
          overflow: hidden; // 超出的文本隐藏
          display: -webkit-box; // 作为弹性伸缩盒子模型显示
          -webkit-line-clamp: 2; // 显示的行
          -webkit-box-orient: vertical; // 设置伸缩盒子的子元素排列方式--从上到下垂直排列
        }
        .titShort {
          font-size: 24rpx;
          font-family: Source Han Sans CN;
          font-weight: 500;
          color: #ff2a14;
          margin-top: 6rpx;
          line-height: 36rpx;
          height: 72rpx;
          width: 154rpx;
          text-align: center;
          border-top: 2px dashed #fa7414;
          overflow: hidden;
          text-overflow: ellipsis;
          -webkit-line-clamp: 2;
          // white-space: nowrap;
        }
      }
    }

    .button {
      width: 600rpx;
      height: 110rpx;
      line-height: 110rpx;
      text-align: center;
      margin-top: 20rpx;
    }

  }
}
</style>
