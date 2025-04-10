<template>
  <div class="demo_page">
    <!-- 轮播 -->
    <swiper class="swiper" :autoplay="autoplay" :interval="interval" :duration="duration">
      <block v-for="(item, index) in images" :key="index">
        <swiper-item>
          <image :src="item" class="slide-image" mode="aspectFill" />
          <div class="swiper_text">
            <text class="swiper_text2">{{index + 1}}/{{images.length}}</text>
          </div>
        </swiper-item>
      </block>
    </swiper>
    <!-- 价格部分 -->
    <div class="top_introduce">
      <div>
        <div class="p1">
          <text class="top_txt1" v-if="promotionInfo.activityId">￥{{promotionInfo.promotionPrice}}</text>
          <text class="top_txt1" v-else>￥{{salesPrice}}</text>

          <text class="top_txt2" v-if="promotionInfo.activityId">￥{{salesPrice}}</text>
          <div class="top_tags">
            <text
              class="metal1"
              v-for="(item1, index1) in tags"
              :key="index1"
              :style="{color:item1.tagColor,borderColor:item1.tagColor,}"
            >{{item1.tag}}</text>
          </div>
        </div>

        <!--        <button class="p2_button" size="default" open-type="share"><img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_share_icon.png" alt />分享</button>-->
      </div>
      <p class="p3">{{name}}</p>

      <p class="p4">{{subtitle}}</p>
    </div>
    <div class="top_introduce" style="flex-direction:row;" v-if="promotionInfo.activityId">
      <p class="p3 p9" style="width: 400rpx;padding:0rpx!important">
        可销售数量：
        <span class="p6 p7">{{promotionInfo.availQuantity}}</span>
      </p>
      <div class="step p9">
        数量：
        <van-stepper
          :value="amount"
          input-width="60rpx"
          button-size="50rpx"
          @change="onChange"
          :max="promotionInfo.availQuantity"
          v-if="name!='' "
        />
      </div>
    </div>
    <div class="top_introduce" v-if="promotionInfo.activityId">
      <div
        class="p3"
        style="color: #666;font-size: 28rpx;font-weight:400;font-family: SourceHanSansCN-Regular;"
      >
        活动起止时间：
        <span class="p4 p6">{{promotionInfo.startTime}}—{{promotionInfo.endTime}}</span>
      </div>
    </div>
    <div class="top_introduce">
      <div
        class="p3"
        style="color: #333;font-size: 28rpx;font-weight:400;font-family: SourceHanSansCN-Regular;display:flex;flex-direction:row;"
      >
        门店：
        <div style="display:flex;flex-direction:column;max-width: 600rpx; ">
          <p class="p4 p6" style="color:#666;">{{shopInfo.simpleName}}</p>
          <p class="p4 p6">{{shopInfo.address}}</p>
        </div>
      </div>
    </div>
    <!-- 商品描述 -->

    <div class="activeTitle">
      <img src="https://m.aerp.com.cn/mini-app-main/title_left_icon.png" alt class="activeImg" />
      <span class="activeWord">商品描述</span>
      <img src="https://m.aerp.com.cn/mini-app-main/title_right_icon.png" alt class="activeImg" />
    </div>

    <!--     超值活动的图片-->

    <!--    <div class="bottom_img">-->
    <!--      <img-->
    <!--        src="https://m.aerp.com.cn/mini-app-main/maintenance_null_img.png"-->
    <!--        alt-->

    <!--        mode="widthFix"-->

    <!--      />-->
    <!--    </div>-->
    <div style="background:#fff;">
      <rich-text :nodes="description"></rich-text>
    </div>
    <div style="height:100rpx; background:#fff;margin-bottom:20rpx;"></div>
    <div class="bottom">
      <div class="bbutton" @tap="back()">返回</div>
      <!-- <div  class="bbutton1" @tap="sure()">单独【测试】</div> -->
    </div>
    <div class="goTop">
      <img src="https://m.aerp.com.cn/mini-app-main/toTop.png" alt @tap="goTopAction" />
    </div>
  </div>
</template>
<script>
import { GetProductDetailPageData, GetWxOpenId } from '../../api'
let code = 1
let Promotions = []
export default {
  data() {
    return {
      images: [],
      productData: {},
      salesPrice: '',
      shopInfo: {}, // 门店详情
      promotionInfo: {
        activityId: ''
      }, // 促销详情
      serviceCode: '',
      serviceName: '', // 服务大类名称
      icon: '', // 商品图片
      amount: '', // 商品默认数量
      tags: [], // 活动标签
      telephone: '', // 门店电话
      CategoryId: '',
      shopid: '',
      activityId: '', // 促销活动Id
      name: '', // 主标题
      subtitle: '', // 副标题
      startTime: '', // 活动开始时
      endTime: '', // 活动结束时间
      promotionPrice: 0.0, // 促销价
      originalPrice: 0.0, // 原价
      availQuantity: 1, // 可售数量
      description: '', // 描述
      Pid: '', // 促销商品pid
      productInfos: [], // 促销商品详情
      imageUrl: [],
      code: ''
    }
  },

  onShareAppMessage: function(res) {
    let that = this
    console.log(`onShareAppMessage: function`, res)
    if (res.from === 'button') {
      // 来自页面内转发按钮
      // console.log("转达",res.target)
    }
    return {
      title: that.name,
      path: `/pages/goodsDetails/main?shopId=${that.shopid}&pid=${that.Pid}&activedId=1&shareUserId=${that.globalData.userInfo?that.globalData.userInfo.userId:''}`,
      imageUrl: that.Url0,
      success: function(res) {
        console.log(`res分享按钮`, res)
        // 转发成功
        wx.showToast({
          title: '分享成功',
          icon: 'success',
          duration: 2000
        })
      },
      fail: function(res) {
        // 分享失败
        wx.showToast({
          title: '分享失败',
          icon: 'none',
          duration: 2000
        })
      }
    }
  },
  mounted() {
    let that = this
    // 获取系统信息
    wx.getSystemInfo({
      success: function(res) {
        let clientHeight = res.windowHeight
        let clientWidth = res.windowWidth
        let ratio = 750 / clientWidth
        that.windowHeight = clientHeight * ratio - 88 + 'rpx'
      }
    })
  },
  methods: {
    // 数量
    onChange(event) {
      this.amount = event.mp.detail
      this.globalData.amount = this.amount
      // console.log(22,this.amount)
    },
    // 向上滚动
    onPageScroll(e) {
      console.log(e)
      if (e.scrollTop > 100) {
        this.setData({
          floorstatus: true
        })
      } else {
        this.setData({
          floorstatus: false
        })
      }
    },

    goTopAction(e) {
      if (wx.pageScrollTo) {
        wx.pageScrollTo({
          scrollTop: 0
        })
      } else {
        wx.showModal({
          title: '提示',
          content:
            '当前微信版本过低，无法使用该功能，请升级到最新微信版本后重试。'
        })
      }
    },
    // 获取商品详情页面刚加载函数
    getShopPageInfo() {
      let that = this
      console.log(that.Pid)
      console.log(that.shopid)
      let shop = {
        ShopId: that.shopid,
        ProductCode: that.Pid
      }
      // 获取商品详情（已完成）
      GetProductDetailPageData(shop)
        .then(res => {
          console.log(222, res)
          that.productData = res.data.productData
          that.name = that.productData.name
          that.salesPrice = that.productData.salesPrice
          that.description = that.productData.description
          that.tags = that.productData.tags
          that.images = that.productData.images
          that.shopInfo = res.data.shopInfo
          that.promotionInfo = res.data.promotionInfo
          if (!that.promotionInfo) {
            that.promotionInfo = {}
            console.log('promotionInfo', that.promotionInfo)
          }
          // that.description = '<div>测试代码</div><img src="https://ss0.bdstatic.com/5aV1bjqh_Q23odCf/static/superman/img/logo_top_ca79a146.png" alt="">'
          // console.log(12345, that.description)
          that.description = that.description
            .replace(
              /<img/gi,
              '<img style="max-width:100%;height:auto;display:block; margin:0 auto;" '
            )
            .replace(/<section/g, '<div')
            .replace(/\/section>/g, 'div>')
        })
        .catch(err => {
          console.log(333, err)
        })
    },

    back() {
      this.$router.go(-1)
    },
    changeAmount() {
      this.amount = this.globalData.amount
      console.log('amount', this.amount)
      this.$forceUpdate()
    }
  },
  // onShow () {
  //    this.changeAmount()

  //  },
  onLoad(options) {
    console.log(options)
    if (options.activedId == 1) {
      this.globalData.shareUserId = options.shareUserId
    }

    let that = this

    that.Pid = options.productCode
    // that.ActivityId = options.activityId
    that.amount = 1
    that.shopid = options.shopId
    this.globalData.referrerShopId = that.shopid
    this.$forceUpdate()
    that.getShopPageInfo()
    console.log(that.Pid)
    console.log(that.shopid)

    // wx.login({
    //   success (res) {
    //     if (res.code) {
    //       console.log(33, res.code)
    //       wx.getUserInfo({
    //         success (userRes) {
    //           GetWxOpenId({
    //             wxCode: res.code,
    //             encryptedData: userRes.encryptedData,
    //             iv: userRes.iv,
    //             PlatForm: 2
    //           }).then(res1 => {
    //             // console.log(`获取用户的oppenId,${res1}`)
    //             that.globalData.userInfo = res1.data.userInfo
    //             that.globalData.tokenInfo = res1.data.tokenInfo
    //             wx.setStorageSync('tokenInfo', res1.data.tokenInfo)
    //             that.globalData.openId = res1.data.openId
    //             that.globalData.unionId = res1.data.unionId
    //             if (!res1.data.isExistUser) {
    //               that.$router.push('/pages/authUserInfo/main')
    //             } else {
    //               that.getShopPageInfo()
    //             }
    //           })
    //         },
    //         fail () {
    //           that.$router.push('/pages/authUserInfo/main')
    //         }
    //       })
    //     }
    //   }
    // })

    console.log(12345, this.globalData.openId)
  },
  onHide() {
    Promotions = []
  },

  onUnload() {
    this.images = []
    this.descriptionImages = []
    this.productCode = ''
    Promotions = []
    this.name = ''
    this.subtitle = ''
    this.startTime = ''
    this.endTime = ''
    this.promotionPrice = ''
    this.originalPrice = ''
    this.description = ''
    this.tags = ''
    this.imageUrl = ''
    this.icon = ''
    this.serviceCode = ''
    this.serviceName = ''
    this.CategoryId = ''
  }
}
</script>

<style scoped lang="scss">
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  background: #f3f3f3;
}

// 轮播数组
.swiper {
  width: 100%;
  height: 750rpx !important;
}

.slide-image {
  height: 100%;
  width: 100%;
}

.swiper_text {
  position: absolute;
  bottom: 0rpx;
  left: 0;
  width: 100%;
  padding-bottom: 30rpx;
  padding-right: 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: flex-end;
  align-items: flex-end;
}

.swiper_text2 {
  width: 75rpx;
  height: 75rpx;
  background: #6b665d;
  opacity: 0.5;
  border-radius: 50%;
  font-size: 36rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 255, 255, 1);
  text-align: center;
  line-height: 75rpx;
}

// 商品编号
.shop_number {
  width: 100%;
  padding-left: 30rpx;
  box-sizing: border-box;
  height: 80rpx;
  line-height: 80rpx;
  background: #ffffff;
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}

// 轮播加质保
.top_bao {
  width: 100%;
  height: 80rpx;
  background: rgba(255, 233, 224, 1);
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  padding: 0 30rpx;
  box-sizing: border-box;
}

.top_one {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}

.top_one img {
  width: 25rpx;
  height: 25rpx;
}

.top_one text {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
}

.img1 {
  width: 44rpx;
  height: 44rpx;
}

.img2 {
  width: 32rpx;
  height: 32rpx;
}

// 价格部分
.top_introduce {
  width: 100%;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  // align-items: center;
  padding: 40rpx 0 30rpx 0;
  box-sizing: border-box;
  margin-top: 20rpx;
}

.top_introduce .p1 {
  width: 100%;
  display: flex;
  flex-direction: row;

  // align-items: center;
  padding-left: 20rpx;
  box-sizing: border-box;
}

.p1 {
  width: 586rpx;
  display: flex;
  flex-direction: row;
  align-items: center;
}

.p1 .top_txt1 {
  font-size: 46rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(255, 99, 36, 1);
}

.p1 .top_txt2 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  text-decoration: line-through;
  color: rgba(173, 173, 173, 1);
  margin-left: 4rpx;
}

.p2_button {
  height: 55rpx;
  background: rgba(247, 245, 244, 1);
  border-radius: 28rpx 0rpx 0rpx 28rpx;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(33, 33, 33, 1);

  img {
    width: 28rpx;
    height: 28rpx;
  }
}

button::after {
  border: none;
}

.p2 {
  width: 125rpx;
  height: 55rpx;
  text-align: center;
  background: rgba(247, 245, 244, 1);
  border-radius: 28rpx 0rpx 0rpx 28rpx;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}

.p2 img {
  width: 28rpx;
  height: 28rpx;
}

.p2 text {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(33, 33, 33, 1);
}

.p3 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 500;
  color: #323232;
  width: 100%;
  padding: 8rpx 30rpx 0;
  box-sizing: border-box;
  margin-bottom: 15rpx;
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
}
.p9 {
  color: #666;
  font-size: 28rpx;
  font-weight: 400;
  font-family: SourceHanSansCN-Regular;
  display: flex;
  align-items: center;
}
.p5 {
  font-size: 25rpx;
  font-family: Source Han Sans CN;
  color: #333;
  width: 100%;
  padding: 6rpx 30rpx;
  box-sizing: border-box;
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  -webkit-box-orient: vertical;
  background: #fff;
}

.p7 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(173, 173, 173, 1);
  min-width: 10%;
  padding-left: 30rpx;
  box-sizing: border-box;
  padding-right: 10rpx;
}
.p4 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(173, 173, 173, 1);
  width: 100%;
  padding-left: 30rpx;
  box-sizing: border-box;
}
.p6 {
  padding-left: 6rpx;
  font-size: 28rpx;
}

// 超值活动
.activeTitle {
  display: flex;
  align-items: center;
  width: 100%;
  height: 90rpx;
  margin-top: 21rpx;
  justify-content: center;
  background: rgba(255, 255, 255, 1);
  margin-top: 16rpx;
}

.activeImg {
  width: 25rpx;
  height: 25rpx;
}

.activeWord {
  font-size: 32rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(33, 33, 33, 1);
  margin: 0 15rpx;
}

// 超值活动的图片
.bottom_img {
  width: 100%;
}

.bottom_img img {
  width: 750rpx;
}

// 选择按钮
.btn {
  width: 100%;
  background: rgba(255, 255, 255, 1);
  padding: 10rpx 30rpx;
  box-sizing: border-box;
  margin-top: 20rpx;
}

.bottom {
  position: fixed;
  left: 0;
  right: 0;
  background: #fff;
  padding: 10rpx 20rpx 10rpx 25rpx;
  bottom: 0;
  display: flex;
  justify-content: center;
}

.goTop {
  position: fixed;
  bottom: 100rpx;
  right: 20rpx;
  font: 700 24rpx/70rpx 'Arial-BoldMT', 'Arial Bold', 'Arial';
  font-style: normal;

  color: #333333;
  text-align: center;

  img {
    width: 96rpx;
    height: 96rpx;
  }
}
.metal1 {
  padding: 0 10rpx;
  box-sizing: border-box;
  // border: 1rpx solid red;
  border-radius: 8rpx;
  font-size: 23rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  // color: red;
  text-align: center;
  margin: 0 15rpx;
  display: flex;
  justify-content: center;
  align-items: center;
  border-width: 1rpx;
  border-style: solid;
}
.top_tags {
  margin-top: 30rpx;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  margin-top: 5rpx;
}
.bbutton {
  background: #ff6324;
  height: 80rpx;
  // width:350rpx;

  // border-radius: 30rpx 0 0 30rpx;
  display: flex;
  justify-content: center;
  align-items: center;
  color: #fff;
  width: 100%;
  border-radius: 40rpx;
}
.bbutton1 {
  background: #ff6324;

  height: 80rpx;
  width: 350rpx;

  border-radius: 0 30rpx 30rpx 0;
  display: flex;
  justify-content: center;
  align-items: center;
  color: #fff;
}
</style>
