<template>
  <div class="demo_page" :style="{minHeight:windowHeight}">
    <div class="topContent">
      <p class="topTitle">
        <span>亲爱的{{userName}}：</span>
        <span class="title" @tap="toHelp">
          <img src="https://m.aerp.com.cn/mini-RG-main/help2.png" alt class="img3" />规则说明
        </span>
      </p>
      <p class="topTitle2">
        通过您的分享来到致大养车注册的新用户已有
        <span class="title1">{{referrerUserNum}}</span>人，     
        其中完成养车服务的用户已有
        <span class="title1">{{numberOfSharedUsers}}</span>人，     
        已为您自动推送
        <span class="title1">{{numberOfDiscountNumPushed}}</span>张优惠券。
      </p>
    </div>
    <div class="conterntC">
      <div class="conternt">
        <p class="p1">优惠券</p>
        <p class="p2" @tap="toCoupon">查看更多</p>
      </div>
    </div>

    <div class="box">
      <div v-if="couponarr!=null">
        <div class="coupon_class" v-if="couponarr!=null">
          <div class="coupon_class_left">
            <!-- item.status == 1 || item.status == 2 -->
            <p :class="['orange',{'orange_color':couponarr.orderEnabled?false:true}]">
              ￥
              <span>{{couponarr.value}}</span>券
            </p>
            <p
              :class="['orange',{'orange_color':couponarr.orderEnabled?false:true}]"
            >{{couponarr.name}}</p>
          </div>
          <div class="coupon_class_right">
            <p
              :class="['text1',{'text_color':couponarr.orderEnabled?false:true}]"
            >{{couponarr.displayName}}</p>
            <p :class="['text2',{'text_color':couponarr.orderEnabled?false:true}]">
              <span>有效期：{{couponarr.startValidTime}}-{{couponarr.endValidTime}}</span>
              <img
                v-if="couponarr.orderEnabled"
                class="byservice_right"
                :src="couponarr.isSelect?'https://m.aerp.com.cn/mini-RG-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-RG-main/maintenance_radio.png'"
                alt
              />
            </p>
            <p
              :class="['text3',{'text_color':couponarr.orderEnabled?false:true}]"
            >{{couponarr.useRuleDesc}}</p>
          </div>
          <!-- 满减灰 -->
          <div class="coupon_img1" v-if="couponarr.status == 1 || couponarr.status == 2">
            <img src="https://m.aerp.com.cn/mini-RG-main/mineyhq_labeltwo.png" alt />
          </div>
          <!-- 满减高亮 -->
          <div
            class="coupon_img1"
            v-if="couponarr.type == 1 && couponarr.status != 2 && couponarr.status != 1 "
          >
            <img src="https://m.aerp.com.cn/mini-RG-main/mineyhq_label_pic.png" alt />
          </div>
          <!-- 已使用灰 -->
          <div class="coupon_img2" v-if="couponarr.status == 1">
            <img src="https://m.aerp.com.cn/mini-RG-main/mineyhq_uesd_icon.png" alt />
          </div>
          <!-- 已过期灰 -->
          <div class="coupon_img2" v-if="couponarr.status == 2">
            <img src="https://m.aerp.com.cn/mini-RG-main/square_over.png" alt />
          </div>
          <!-- 已注销 -->
          <div class="coupon_img2" v-if="couponarr.status == 3">
            <img src="https://m.aerp.com.cn/mini-RG-main/Cancelled.png" alt />
          </div>
        </div>
      </div>
      <div
        style="width:100%;background:#fff;margin-bottom:30rpx;margin-top:-10rpx;border-top:2rpx solid #ddd;padding-bottom:20rpx; "
        v-else
      >
        <div class="null_img" :style="{height:windowHeight}">
          <img src="https://m.aerp.com.cn/mini-RG-main/mine_appointment_null.png" alt />
          <text>暂无分享优惠券</text>
        </div>
      </div>
      <!-- <img
        :src="itema.isDefaultExpand?'https://m.aerp.com.cn/mini-RG-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-RG-main/maintenance_radio.png'"
        alt
      />-->
    </div>
    <div class="body">
      <div class="main">
        <div class="pTitle">订单信息</div>
        <div v-if="allOrders.length>0">
          <div class="order" v-for="(item1,index1) in allOrders" :key="index1">
            <div class="content1">
              <p class="p3">会员：{{item1.userName}}</p>
              <p class="p3">电话:{{item1.telephone}}</p>
            </div>
            <div class="content1">
              <p class="p4">
                订单编号：
                <span class="p3">{{item1.orderNo}}</span>
              </p>
              <p class="p5">{{item1.orderStatus}}</p>
            </div>
            <div class="content1">
              <p class="p4">下单时间：{{item1.orderTime}}</p>
            </div>
            <div v-for="(item2,index2) in item1.products" :key="index2">
              <div class="orderContent" v-if="item1.products.length==1">
                <img :src="item2.imageUrl" alt class="img1" />
                <div>
                  <p class="name">{{item2.productName}}</p>
                  <p class="num">共{{item2.totalNumber}}件</p>
                </div>
              </div>
              <div class="orderContent1" v-if="item1.products.length>1&&item1.products.length<5">
                <img
                  src="https://m.aerp.com.cn/mini-RG-main/mine_appointment_null.png"
                  alt
                  class="orderimg"
                />
                <div>
                  <!-- <p class="name">{{item2.productName}}</p> -->
                  <p class="num">共{{item2.totalNumber}}件</p>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="More" @tap="toMore" v-if="showMore&&allOrders.length>1">加载更多</div>
        <div class="null_img" v-if="allOrders.length<1">
          <img src="https://m.aerp.com.cn/mini-RG-main/mine_appointment_null.png" alt />
          <text>暂无订单</text>
        </div>
      </div>
    </div>
    <div class="body">
      <div class="main">
        <div class="pTitle">您分享的新用户</div>
        <div v-if="shareUserList.length>0">
          <div class="comment" v-for="(item1,index1) in shareUserList" :key="index1">
            <div class="comment_headUrl">
              <img :src="item1.headUrl" alt style="width:60rpx;height: 60rpx;border-radius: 30rpx;" />
            </div>
             <div class="comment_header">
              <!-- 昵称 -->
              <div class="comment_nickName">{{item1.nickName}}</div>
              <!-- 手机 -->
              <div class="comment_mobile">{{item1.userTelDes}}</div>
            </div>
          </div>
        </div>

        <div class="More" @tap="toShareUserMore" v-if="shareUserShowMore && shareUserList.length>1">加载更多</div>
        <div class="null_img" v-if="shareUserList.length<1">
          <img src="https://m.aerp.com.cn/mini-RG-main/mine_appointment_null.png" alt />
          <text>暂无分享用户</text>
        </div>
      </div>
    </div>
    <div style="width:100%;" v-if="showPopup">
      <van-popup
        :show="showPopup"
        closeable
        @close="onClose"
        position="center"
        lock-scroll
        custom-style="height:70%;"
        round
        :close-on-click-overlay="false"
        :overlay="true"
      >
        <div class="popup_header">规则说明</div>
        <div class="popup_content">
          <div v-for="(itemm,indexx) in illustration" :key="indexx">
            <p class="popup_p">
              <span style="color:#FF6324;font-size:28rpx;">{{itemm.id}}.</span>
              {{itemm.item}}
            </p>
          </div>
        </div>
      </van-popup>
    </div>
  </div>
</template>
<script>
import {
  GetSharingSummary,
  GetSharingRuleDescription,
  GetSharingOrders,
  GetSharingCoupon,
  GetReferrerCustomerList,//分享用户列表
} from '../../api'

export default {
  data() {
    return {
      showMore: true,
      // couponarr: [

      //   {
      //     couponActivityId: 2,
      //     couponRuleId: 1,
      //     displayName: "致大养车到店保养券",
      //     endValidTime: "2030.12.31",
      //     imageUrl: "",
      //     name: "满300元减120",
      //     orderEnabled: true,
      //     startValidTime: "2020.01.01",
      //     status: 0,
      //     type: 1,
      //     useRuleDesc: "新用户首单，满300减120",
      //     userCouponId: 97,
      //     userId: "1fe23753-9d73-425d-9d6a-a574c571de6d",
      //     value: 120
      //   }
      // ],
      couponarr: {},
      showPopup: false,
      userName: '', // 用户名称
      referrerUserNum: '', // 推广用户数量
      numberOfSharedUsers: '', // 分享的已做订单用户数量
      numberOfDiscountNumPushed: '', // 已推送优惠卷得数量
      illustration: [], // 分享说明
      allOrders: [], // 订单列表
      couponarr: [], // 优惠券列表
      referrerUsers: [], // 推广用户列表
      PageSize: 3,
      PageIndex: 1,

      //分享的用户信息
      shareUserList:[],
      shareUserPageSize: 10,
      shareUserPageIndex: 1,
      shareUserShowMore: true,
    }
  },

  onShow() {},
  methods: {
    onClose() {
      this.showPopup = false
    },
    toHelp() {
      this.showPopup = true
      console.log('规则说明')
    },
    toCoupon() {
      this.$router.push('/pages/coupon/main')
    },
    getSharingSummary() {
      let that = this
      let rquest = {
        UserId: that.globalData.userInfo.userId
      }
      GetSharingSummary(rquest)
        .then(res => {
          that.userName = res.data.userName
          that.referrerUserNum = res.data.referrerUserNum
          that.numberOfSharedUsers = res.data.numberOfSharedUsers
          that.numberOfDiscountNumPushed = res.data.numberOfDiscountNumPushed
        })
        .catch(err => {})
    },
    getSharingRuleDescription() {
      let that = this
      let rquest = {
        UserId: that.globalData.userInfo.userId
      }
      GetSharingRuleDescription(rquest)
        .then(res => {
          let illustration = res.data

          let illustrationObj = illustration.map(function(item, index) {
            let obj = {}
            obj.item = item
            obj.id = index + 1
            return obj
          })
          that.illustration = illustrationObj
          //  let illustrationObj = { ...illustration };

          console.log('illustration', that.illustration)
        })
        .catch(err => {})
    },
    getSharingOrders(UserId, PageIndex, PageSize) {
      let that = this
      if (this.showMore == false) {
        return false
      }
      let rquest = {
        UserId: that.globalData.userInfo.userId,
        PageIndex: that.PageIndex,
        PageSize: that.PageSize
      }
      GetSharingOrders(rquest)
        .then(ress => {
          let arr = ress.data.items
          if (that.PageIndex * that.PageSize >= ress.data.totalItems) {
            this.showMore = false
          }
          if (that.PageIndex === 1) {
            that.allOrders = ress.data.items
          } else {
            that.allOrders = that.allOrders.concat(ress.data.items)
            console.log(that.allOrders.length)
          }

          that.PageIndex++
          console.log('订单列表', that.allOrders)
          console.log(res.data.items)
          // that.allOrders = res.data.items;
        })
        .catch(err => {})
    },
    getShareUserList(UserId, PageIndex, PageSize) {
      let that = this
      if (this.shareUserShowMore == false) {
        return false
      }
      let rquest = {
        UserId: that.globalData.userInfo.userId,
        PageIndex: that.shareUserPageIndex,
        PageSize: that.shareUserPageSize
      }
      GetReferrerCustomerList(rquest)
        .then(ress => {
          let arr = ress.data.items
          if (that.shareUserPageIndex * that.shareUserPageSize >= ress.data.totalItems) {
            this.shareUserShowMore = false
          }
          if (that.shareUserPageIndex === 1) {
            that.shareUserList = ress.data.items
          } else {
            that.shareUserList = that.shareUserList.concat(ress.data.items)
            console.log(that.shareUserList.length)
          }

          that.shareUserPageIndex++
          console.log('分享用户列表', that.shareUserList)
          console.log(res.data.items)
          // that.shareUserList = res.data.items;
        })
        .catch(err => {})
    },
    getSharingCoupon() {
      let that = this
      let rquest = {
        UserId: that.globalData.userInfo.userId
      }
      GetSharingCoupon(rquest)
        .then(res => {
          that.couponarr = res.data
          console.log(11, that.couponarr)
        })
        .catch(err => {})
    },
    toMore() {
      if (this.showMore == false) {
        return false
      } else {
        let that = this
        let PageIndex = that.PageIndex
        let PageSize = that.PageSize
        let UserId = that.globalData.userInfo.userId
        that.getSharingOrders(UserId, PageIndex, PageSize)
      }
    },
    toShareUserMore() {
      if (this.shareUserShowMore == false) {
        return false
      } else {
        let that = this
        let PageIndex = that.shareUserPageIndex
        let PageSize = that.shareUserPageSize
        let UserId = that.globalData.userInfo.userId
        that.getShareUserList(UserId, PageIndex, PageSize)
      }
    }
  },
  mounted() {
    this.getSharingOrders()
    this.getShareUserList();
    this.getSharingSummary()
    this.getSharingRuleDescription()
    this.getSharingCoupon()
  },
  onHide() {},
  onUnload() {
    this.PageIndex = 1
    this.PageSize = 3
    this.showPopup = false
  },
  onLoad() {
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

<style scoped>
.topContent {
  width: 100%;
  height: 260rpx;
  background: rgba(255, 99, 36, 1);
  padding: 30rpx 30rpx 0 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
}
.topTitle {
  display: flex;
  justify-content: space-between;
  align-items: center;
  color: #fff;
  font: 30rpx/1 'SourceHanSansCN-Regular';
}
.title {
  font: 20rpx/1 'SourceHanSansCN-Normal';
  display: flex;
  align-items: center;
}
.topTitle2 {
  color: #ffbba0;
  font: 24rpx/1.5 'SourceHanSansCN-Regular';
  margin-top: 40rpx;
}
.title1 {
  color: #fff;
  margin: 0 6rpx;
}
.conterntC {
  width: 100%;
  padding: 30rpx 30rpx 0 30rpx;
  box-sizing: border-box;
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-direction: column;
}
.conternt {
  width: 100%;
  height: 120rpx;
  background: #fff;
  display: flex;
  margin-top: -110rpx;
  justify-content: space-between;
  align-items: center;
  border-radius: 10rpx;
  padding: 0 20rpx;
  box-sizing: border-box;
}
.p1 {
  color: rgba(51, 51, 51, 1);
  font: 600 32rpx/1 'SourceHanSansCN-Bold';
}
.p2 {
  color: rgba(255, 99, 36, 1);
  font: 24rpx/1 'SourceHanSansCN-Regular';
}
.box {
  width: 100%;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  justify-content: center;
  min-height: 200rpx;
  padding: 10rpx 30rpx 0 30rpx;
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
  font: 26rpx/2 '';
  color: #333;
}
.null_img img {
  width: 115rpx;
  height: 115rpx;
  margin-top: 20rpx;
}
.pTitle {
  height: 113rpx;
  padding: 30rpx;
  box-sizing: border-box;
  color: #333333;
  font-size: 32rpx;
  font-family: Source Han Sans CN;
  width: 100%;
  font-weight: 700;

  border-bottom: 2rpx solid rgba(221, 221, 221, 1);
}
.body {
  padding: 0 30rpx;
  box-sizing: border-box;
  margin-bottom: 12px;
}
.main {
  background: #fff;
  width: 100%;
  border-radius: 10rpx;
  padding-bottom: 30rpx;
}
.p3 {
  color: #333;
  font: 28rpx/1 'SourceHanSansCN-Medium';
}
.p4 {
  color: #666;
  font: 28rpx/1 'SourceHanSansCN-Regular';
}
.p5 {
  color: #5cc782;
  font: 28rpx/1 'SourceHanSansCN-Regular';
}
.order {
  width: 100%;
  min-height: 100rpx;
  border-bottom: 2rpx solid rgba(221, 221, 221, 1);
}
.content1 {
  display: flex;
  justify-content: space-between;
  width: 100%;
  padding: 30rpx 30rpx 0;
  box-sizing: border-box;
}
.orderContent {
  padding: 30rpx;
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.orderContent1 {
  padding: 30rpx;
  display: flex;
  align-items: center;
  justify-content: flex-start;
}
.img1 {
  width: 110rpx;
  height: 110rpx;
}
.orderimg {
  width: 110rpx;
  height: 110rpx;
  padding-right: 20rpx;
}

.name {
  color: #333;
  font:  28rpx/1 'SourceHanSansCN-Medium';
  width: 500rpx;
}
.num {
  color: #999;
  font: 22rpx/1 'SourceHanSansCN-Normal';
  margin-top: 30rpx;
}
.popup_header {
  width: 660rpx;
  text-align: center;
  font-weight: bold;
  line-height: 100rpx;
  font-size: 35rpx;
  background-image: url('https://m.aerp.com.cn/mini-RG-main/titleBg.png');
  background-repeat: no-repeat;
  background-size: 100% 100%;
  color: #fff;
}
.popup_content {
  padding: 40rpx 20rpx;
  color: #666666;
  font: 26rpx/1 'SourceHanSansCN-Regular';
}
.popup_p {
  color: #666;
  font: 26rpx/1.5 'SourceHanSansCN-Regular';
  margin: 20rpx;
}
.img3 {
  height: 28rpx;
  width: 28rpx;
  margin-right: 6rpx;
}
.More {
  background: #fff;
  text-align: center;
  height: 80rpx;
  color: #ff6324;
  padding: 30rpx;
  box-sizing: border-box;
  font: 24rpx/1 'SourceHanSansCN-Regular';
}

.comment {
  width: 100%;
  padding: 20rpx 30rpx;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-wrap: wrap;
  box-sizing: border-box;
  border-bottom: 1rpx solid #eee;
}
.comment_comment {
  flex: 5;
  padding: 0 18rpx;
  padding-right: 0;
  box-sizing: border-box;
}
.comment_header {
  display: flex;
  margin-top: 15rpx;
}
.comment_nickName {
  margin-left: 15rpx;
  font-size: 28rpx;
  color: #666666;
}
.comment_mobile {
  margin-left: 15rpx;
  font-size: 28rpx;
  color: #666666;
}
.comment_carName {
  font-size: 24rpx;
  color: #999999;
}
.comment_section {
  padding-top: 24rpx;
}
</style>