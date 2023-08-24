<template>
  <div class="demo_page">
    <div class="top_title">
      <div class="top_self" @longpress="longTap" @tap="toLogin">
        <div class="top_img_txt">
          <img :src="usersrc" alt class="userimg" />
          <div class="top_txt">
            <p class="p_name">{{username}}</p>
            <p class="p_member">
              <img :src="numbersrc" alt class="img_member" />
              {{member}}
            </p>
          </div>
        </div>
        <img src="https://m.aerp.com.cn/mini-app-main/mine_whtie_jump.png" alt style="width:24rpx;height:24rpx;" />
      </div>
      <div class="top_infor">
        <div class="coupon" @tap="couponClick">

          <span class="bold">{{coupon}}张</span>
          优惠券
        </div>
        <div class="coupon" @tap="comboClick">

          <span class="bold">{{packageCardNum}}张</span>
          套餐卡
        </div>
        <div class="coupon" @tap="integralClick">

          <span class="bold">{{integral}}</span>
          积分
        </div>
      </div>
    </div>
    <div class="bottom_box">
      <div class="my_order" @tap="orderClick">
        <text class="my_order1">我的订单</text>
        <p class="my_order2">
          全部订单
          <img src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png" alt style="width:24rpx;height:24rpx;margin-left:15rpx;" />
        </p>
      </div>
      <!-- <div class="order_self">
        <div
          v-for="(value,index) in orderarr"
          :key="index"
          @tap="selfOrderClick(value,index)"
          class="order_box"
         
        >
          <img :src="value.src" alt class="orderimg"/>
          <div :v-model="order">
          <span class="info" :v-model="order.count">{{order.count}}</span>
          <text class="order_txt" :v-model="order.Status">{{value.txt}}</text>
          </div>
        </div>
      </div>-->
      <div class="order_self">
        <div @tap="selfOrderClick(0)" class="order_box">
          <img :src="orderarr[0].src1" alt class="orderimg" />

          <span class="info" v-show="showcount">{{orderarr[0].count1}}</span>
          <text class="order_txt">{{orderarr[0].txt1}}</text>
        </div>
        <div @tap="selfOrderClick(1)" class="order_box">
          <img :src="orderarr[1].src2" alt class="orderimg" />

          <span class="info" v-show="showcount1">{{orderarr[1].count2}}</span>
          <text class="order_txt">{{orderarr[1].txt2}}</text>
        </div>
        <div class="order_box" @tap="selfOrderClick(2)">
          <img :src="orderarr[2].src3" alt class="orderimg" />

          <span class="info" v-show="showcount2">{{orderarr[2].count3}}</span>
          <text class="order_txt">{{orderarr[2].txt3}}</text>
        </div>
        <div class="order_box" @tap="selfOrderClick(3)">
          <img :src="orderarr[3].src4" alt class="orderimg" />

          <span class="info" v-show="showcount3">{{orderarr[3].count4}}</span>
          <text class="order_txt">{{orderarr[3].txt4}}</text>
        </div>
        <div class="order_box" @tap="selfOrderClick(4)">
          <img :src="orderarr[4].src5" alt class="orderimg" />

          <span class="info" v-show="showcount4">{{orderarr[4].count5}}</span>
          <text class="order_txt">{{orderarr[4].txt5}}</text>
        </div>
      </div>
      <p class="usedFuction">常用功能</p>
      <div class="usedFuction_self">
        <div v-for="(usedvalue,usedindex) in usedarr" :key="usedindex" @tap="functionClick(usedvalue,usedindex)" class="usedFuction_box">
          <img :src="usedvalue.src" alt class="usedFuctionimg" />
          <text class="usedFuction_txt">{{usedvalue.txt}}</text>
        </div>
      </div>
      <div class="share_img">
        <img src="https://m.aerp.com.cn/mini-app-main/mine_ad_picture.png" alt />
      </div>
    </div>
  </div>
</template>

<script>
import {
  GetYuyueType,
  GetEachStatusOrderCount,
  GetuserInfor
} from '../../../api'

export default {
  data() {
    return {
      order: [{ status: ' ', count: ' ' }],
      showcount: false,
      showcount1: false,
      showcount2: false,
      showcount3: false,
      showcount4: false,
      showcount5: false,
      coupon: '',
      packageCardNum: 0,
      integral: '',
      username: '点击登录账户',
      member: '',
      usersrc: '',
      numbersrc: '',
      orderarr: [
        {
          src1: `${this.globalData.imgPubUrl}mine_wait_pay.png`,
          txt1: '待付款',
          count1: '',
          status1: ''
        },
        {
          src2: `${this.globalData.imgPubUrl}mine_wati_goods.png`,
          txt2: '待收货',
          count2: '',
          status2: ''
        },
        {
          src3: `${this.globalData.imgPubUrl}mine_wait_installation.png`,
          txt3: '待安装',
          count3: '',
          status3: ''
        },
        {
          src4: `${this.globalData.imgPubUrl}mine_wait_evaluation.png`,
          txt4: '待评价',
          count4: '',
          status4: ''
        },
        {
          src5: `${this.globalData.imgPubUrl}mine_wait_refund.png`,
          txt5: '售后/退款',
          count5: '',
          status5: ''
        }
      ],
      usedarr: [
        {
          src: `http://m.aerp.com.cn/mini-app-main/myService.png`,
          txt: '服务记录'
        },
        // {
        //   src: `${this.globalData.imgPubUrl}mine_sign_in.png`,
        //   txt: '签到有礼'
        // },
        {
          src: `${this.globalData.imgPubUrl}mine_car_archives.png`,
          txt: '爱车档案'
        },
        // {
        //   src: 'https://m.aerp.com.cn/mini-app-main/mine_appointment1.png',
        //   txt: '我的预约'
        // },
        {
          src: `${this.globalData.imgPubUrl}Inspection record.png`,
          txt: '检查记录'
        },
        // {
        //   src: `${this.globalData.imgPubUrl}mine_car_card.png`,
        //   txt: '养车卡'
        // },
        // {
        //   src: `${this.globalData.imgPubUrl}mine_my_integral.png`,
        //   txt: '我的积分'
        // },
        {
          src: `${this.globalData.imgPubUrl}maintenance_tel_icon.png`,
          txt: '官方电话'
        },
        // {
        //   src: `${this.globalData.imgPubUrl}mine_my_focus.png`,
        //   txt: '我的关注'
        // },
        // {
        //   src: `${this.globalData.imgPubUrl}mine_my_cart.png`,
        //   txt: '购物车'
        // },
        {
          src: `${this.globalData.imgPubUrl}mine_address.png`,
          txt: '服务地址'
        },
        // {
        //   src: `${this.globalData.imgPubUrl}mine_my_invoice.png`,
        //   txt: '我的发票'
        // },
        // {
        //   src: `${this.globalData.imgPubUrl}mine_join_us.png`,
        //   txt: '加盟致大'
        // },
        {
          src: `${this.globalData.imgPubUrl}Help center.png`,
          txt: '帮助中心'
        },
        {
          src: `${this.globalData.imgPubUrl}sharers.png`,
          txt: '分享有礼'
        },
        {
          src: `${this.globalData.imgPubUrl}index_customer_icon.png`,
          txt: '在线客服'
        },
        {
          src: `${this.globalData.imgPubUrl}mine_wait_evaluation.png`,
          txt: '我的评价'
        }
      ]
    }
  },
  onShow() {
    console.log('onShow  ')
    this.changeStatus()

    // 接口调试完毕，再次加载，现在暂时封存
    this.username = this.globalData.userInfo.nickName
    this.usersrc = this.globalData.userInfo.headUrl

    let that = this
    // 获取我的页面各状态订单数量
    GetEachStatusOrderCount()
      .then(res => {
        // let arr =res.data;

        that.orderarr[0].count1 = res.data[1].count
        that.orderarr[1].count2 = res.data[2].count
        that.orderarr[2].count3 = res.data[3].count
        that.orderarr[3].count4 = res.data[4].count
        that.orderarr[4].count5 = res.data[5].count

        that.changeStatus()
      })
      .catch(err => {})
    // 获取页面个人信息

    GetuserInfor('')
      .then(res => {
        // that.src = res.data.headUrl
        that.coupon = res.data.availCouponCount
        that.packageCardNum = res.data.packageCardNum
        that.numbersrc = res.data.memberUrl
        that.member = res.data.memberLevel
        that.integral = res.data.point
      })
      .catch(err => {})
  },
  methods: {
    // 个人信息页面
    // userInfor () {

    //   this.$router.push('/pages/personalData/main')
    // },
    // 优惠券
    // 修改info状态
    changeStatus() {
      if (this.orderarr[0].count1 != 0) {
        this.showcount = true
      }
      if (this.orderarr[1].count2 != 0) {
        this.showcount1 = true
      }
      if (this.orderarr[2].count3 != 0) {
        this.showcount2 = true
      }
      if (this.orderarr[3].count4 != 0) {
        this.showcount3 = true
      }
      if (this.orderarr[4].count5 != 0) {
        this.showcount4 = true
      }
    },
    couponClick() {
      this.$router.push('/pages/coupon/main')
    },
    comboClick() {
      this.$router.push('/pages/myCombo/main')
    },

    // 积分
    integralClick() {
      this.$router.push('/pages/myPoints/main')
    },
    // 全部订单
    orderClick() {
      this.$router.push('/pages/allOrders/main')
    },
    // 跳转各个订单
    selfOrderClick(index) {
      // this.$router.push('/pages/allOrders/main')
      if (index != 4) {
        this.$router.push({
          path: '/pages/allOrders/main',
          query: { index: index + 1, num: '123' }
        })
      } else {
        wx.showModal({
          content: '请拨打4008008008为您处理',
          confirmText: '呼叫',
          confirmColor: '#FF6324',
          success(res) {
            if (res.confirm) {
              wx.makePhoneCall({
                phoneNumber: '4008008008'
              })
            } else if (res.cancel) {
            }
          }
        })
      }
    },
    longTap(e) {
      if (this.globalData.tokenInfo !== '') {
        wx.showModal({
          title: '确定要退出登录吗？',
          content: '',
          showCancel: true,
          cancelText: '取消',
          cancelColor: '#000000',
          confirmText: '确定',
          confirmColor: '#3CC51F',
          success: result => {
            if (result.confirm) {
              this.username = '未登录'
              this.usersrc = `${this.globalData.imgPubUrl}mine_head_default.png`
              this.globalData.userInfo = ''
              this.globalData.tokenInfo = ''
              wx.setStorageSync('tokenInfo', '')
            }
          }
        })
      }
    },
    toLogin() {
      if (!this.globalData.tokenInfo) {
        this.$router.push('/pages/authUserInfo/main')
      } else {
        this.$router.push('/pages/personalData/main')
      }
    },
    // 各个功能的点击事件
    functionClick(usedvalue, usedindex) {
      let that = this
      switch (usedindex) {
        case 0: // 客服服务(已完成)
          this.$router.push('/pages/serverRecords/main')
          break
        case 1: // 爱车档案(已完成)
          this.$router.push({
            path: '/pages/myCard/main',
            query: { isFromPerson: true }
          })
          break
        case 2: // 我的预约(已完成)
          this.$router.push('/pages/my/inspection/main') // 检查记录
          break
        // case 4: // 养车卡
        //   wx.showToast({ title: '敬请期待', icon: 'none' })
        //   break
        // case 3: // 我的积分(已完成)
        //   this.$router.push('/pages/myPoints/main')
        //   break
        case 3: // 官方电话
          wx.showModal({
            content: '4008008008',
            confirmText: '呼叫',
            confirmColor: '#FF6324',
            success(res) {
              if (res.confirm) {
                console.log('用户点击确定')
                wx.makePhoneCall({
                  phoneNumber: '4008008008'
                })
              } else if (res.cancel) {
                console.log('用户点击取消')
              }
            }
          })
          break
        // case 4: // 我的关注(已完成)
        //   this.$router.push('/pages/myConcern/main')
        //   break
        // case 7: // 购物车
        //   wx.showToast({ title: '敬请期待', icon: 'none' })
        //   break
        case 4: // 收货地址(已完成)
          this.$router.push({
            path: '/pages/addressManagement/main',
            query: { isFromPerson: true }
          })
          break
        // case 9: // 我的发票
        //   wx.showToast({ title: '敬请期待', icon: 'none' })
        //   break
        // case 5: // 加盟致大
        //   this.$router.push('/pages/joinRedE/main')
        //   break
        case 5: // 帮助中心
          this.$router.push('/pages/helpCenter/pages/help/main')
          break
        case 6: // 分享有礼
          this.$router.push('/pages/sharers/main')
          break
        case 7: // 在线客服
            wx.openCustomerServiceChat({
              extInfo: {url: 'https://work.weixin.qq.com/kfid/'},
              corpId: '',
              success(res) {}
            } )
          break
        case 8: // 我的评价
          this.$router.push('/pages/MyEvaluation/main')
          break
        default:
          wx.showToast({ title: '点击有误', icon: 'none' })
      }
    }
  },
  mounted() {}
}
</script>

<style scoped>
.share_img {
  width: 690rpx;
  height: 160rpx;
  margin-top: 16rpx;
}
.share_img img {
  width: 100%;
  height: 100%;
}

.usedFuction {
  width: 100%;
  font-size: 32rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(51, 51, 51, 1);
  margin-bottom: 21rpx;
}
.usedFuction_self {
  width: 690rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: flex-start;
  align-items: center;
  padding: 40rpx;
  box-sizing: border-box;
}
.usedFuction_box {
  width: 25%;
  height: 100rpx;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
  margin-bottom: 40rpx;
}
.usedFuctionimg {
  width: 46rpx;
  height: 46rpx;
}
.usedFuction_txt {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}

.order_self {
  width: 690rpx;
  height: 180rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  background: #ffffff;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  margin-top: 21rpx;
  margin-bottom: 52rpx;
  padding: 40rpx;
  box-sizing: border-box;
}
.orderimg {
  width: 46rpx;
  height: 46rpx;
  position: relative;
}
.info {
  width: 28rpx;
  height: 28rpx;
  border: 1px solid #ff5f5f;
  border-radius: 100%;
  /* background:#EA5921; */
  background: #ff5f5f;
  position: absolute;
  top: -14rpx;
  right: 13px;
  font-size: 20rpx;
  text-align: center;
  vertical-align: middle;
  color: #fff;
}
.order_box {
  flex: 1;
  height: 100rpx;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
  position: relative;
}
.order_txt {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}

.bottom_box {
  width: 100%;
  display: flex;
  flex-direction: column;
  padding: 30rpx;
  box-sizing: border-box;
}
.my_order {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.my_order1 {
  font-size: 32rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(51, 51, 51, 1);
}
.my_order2 {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(173, 173, 173, 1);
}

.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  background: #f3f3f3;
}
.top_title {
  width: 100%;
  height: 310rpx;
  background: rgba(250, 121, 69, 1);
  padding: 30rpx 30rpx 0 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
}
.top_self {
  width: 100%;
  height: 106rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.top_img_txt {
  flex: 2;
  height: 106rpx;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}
.userimg {
  width: 106rpx;
  height: 106rpx;
  border-radius: 53rpx;
}
.back {
  flex: 1;
  text-align: right;
  color: #ffffff;
}
.top_txt {
  flex: 1;
  margin-left: 10rpx;
}
.p_name {
  font-size: 34rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 255, 255, 1);
}
.p_member {
  width: 177rpx;
  height: 45rpx;
  background: rgba(255, 147, 104, 1);
  border-radius: 23rpx;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  line-height: 45rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 255, 255, 1);
  padding: 2rpx 7rpx;
  box-sizing: border-box;
  margin-top: 10rpx;
}
.img_member {
  width: 32rpx;
  height: 36rpx;
  border-radius: 10rpx;
  vertical-align: middle;
  margin-right: 10rpx;
}

.top_infor {
  width: 690rpx;
  height: 123rpx;
  background: rgba(255, 239, 232, 1);
  border-radius: 14rpx 14rpx 0rpx 0rpx;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 33rpx 94rpx 28rpx 94rpx;
  box-sizing: border-box;
}
.coupon {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}
.img {
  width: 22rpx;
  height: 22rpx;
  margin-left: 10rpx;
}
.bold {
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(51, 51, 51, 1);
  margin-bottom: 15rpx;
}
</style>
