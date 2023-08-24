<template>
  <div class="demo_page">
    <!-- 轮播 -->
    <swiper class="swiper" :autoplay="autoplay" :interval="interval" :duration="duration">
      <block v-for="(item, index) in images" :key="index">
        <swiper-item>
          <image :src="item" class="slide-image" mode="aspectFit" />
          <div class="swiper_text">
            <text class="swiper_text2">{{index + 1}}/{{images.length}}</text>
          </div>
        </swiper-item>
      </block>
    </swiper>
    <!-- 价格部分 -->
    <div class="top_introduce" style="margin-top:0;">
      <div>
        <div class="p1">
          <text class="top_txt1">￥{{nowPrice}}</text>
          <!-- <text class="top_txt1" >￥{{salesPrice}}</text> -->

          <text class="top_txt2">￥{{originalPrice}}</text>
          <button open-type="share" class="p2">
            <img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_share_icon.png" alt />
            <text>分享</text>
          </button>

          <div class="top_tags">
            <text class="metal1" v-for="(item1, index1) in tags" :key="index1" :style="{color:item1.tagColor,borderColor:item1.tagColor,}">{{item1.tag}}</text>
          </div>
        </div>

        <!--        <button class="p2_button" size="default" open-type="share"><img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_share_icon.png" alt />分享</button>-->
      </div>
      <p class="p3" style="margin-bottom:0;">{{shopTitle}}</p>
      <p class="p3" style="color:red;margin-bottom:0;" v-if="advertisement!=''">{{advertisement}}</p>
      <!-- <p class="p4">{{subtitle}}</p> -->
    </div>
    <div class="top_introduce" style="flex-direction:row;" v-if="isShowStep">
      <!-- <p class="p3 p9" style="width: 400rpx;">
        <span class="p6 p7"></span>
      </p>-->
      <div class="step p9">
        数量：
        <van-stepper :value="amount" input-width="90rpx" button-size="60rpx" @change="onChange" v-if="name!='' " :disabled="stopAdd" />
      </div>
    </div>
    <div class="top_card" v-if="isShoeShop">
      <div class="card_tab" @tap="changeCard" v-if="carArrFirst != null">
        <img :src="carArrFirst.brandUrl" alt class="img_card" mode="aspectFit" />
        <div class="card_content">
          <p class="p1">{{carArrFirst.vehicle}}</p>
          <p class="p12">{{carArrFirst.carNumber}}</p>
        </div>
        <div style="display:flex;flex-direction:column;align-items:center;">
          <img src="https://m.aerp.com.cn/mini-app-main/changeCarIcon.png" alt style="width:60rpx;height:60rpx;margin-top:20rpx;" />
          <p class="p12">换车</p>
        </div>
      </div>
      <div class="card_add" @tap="changeCard" v-else>请选择车辆</div>
      <div class="card_store" @tap="changeStore" v-if="storeArr != null">
        <img :src="storeArr.img" alt class="img_card" mode="aspectFill" />
        <div class="card_content">
          <p class="p1_p1">{{storeArr.simpleName}}</p>
          <p class="p2_p2">{{storeArr.address}}</p>
        </div>
        <img src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png" alt style="width:24rpx;height:24rpx" />
      </div>
      <div class="store_add" @tap="changeStore" v-else>请选择服务门店</div>
    </div>
    <!-- <div class="top_introduce" v-if="promotionInfo.activityId">
      <div
        class="p3"
        style="color: #666;font-size: 28rpx;font-weight:400;font-family: SourceHanSansCN-Regular;"
      >
        活动起止时间：
        <span class="p4 p6">{{promotionInfo.startTime}}—{{promotionInfo.endTime}}</span>
      </div>
    </div>-->
    <!-- <div class="top_introduce">
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
    </div>-->
    <!-- 商品描述 -->
    <div class="activeTitle">
      <img src="https://m.aerp.com.cn/mini-app-main/title_left_icon.png" alt class="activeImg" />
      <span class="activeWord">超值活动</span>
      <img src="https://m.aerp.com.cn/mini-app-main/title_right_icon.png" alt class="activeImg" />
    </div>
    <!-- 超值活动的图片 -->
    <div class="bottom_img">
      <img :src="itemx" alt v-for="(itemx,indexx) in descriptionImages" mode="widthFix" :key="indexx" />
    </div>

    <div class="goTop">
      <img src="https://m.aerp.com.cn/mini-app-main/toTop.png" alt @tap="goTopAction" />
    </div>
    <!-- <van-button type="primary" size="mini" round color="#FF6324" @tap="toPopup()">去选择</van-button> -->

    <div style="height:100rpx; background:#fff;margin-bottom:20rpx;"></div>

    <div class="bottom" v-if="isFromShare">
      <!-- <div class="bbutton" @tap="back()">返回</div> -->
      <div class="bbutton2" @tap="sure()">购买</div>
    </div>
    <div class="bottom" v-else>
      <div class="bbutton" @tap="back()">返回</div>
      <div class="bbutton1" @tap="sure()" v-if="isShowStep">购买</div>
      <div class="bbutton1" @tap="sure()" v-else>核销</div>
    </div>
    <!-- 发动机弹框 -->
    <div style="width:100%;" v-if="showPopup">
      <van-popup :show="showPopup" closeable @close="toHelp" position="center" :close-on-click-overlay="false" close-icon="https://m.aerp.com.cn/mini-app-main/help.png" lock-scroll custom-style="height:70%;" round>
        <div class="popup_header">{{property.title}}</div>
        <div class="popup_title">{{property.content}}</div>
        <div class="popup_content">
          <div class="popup_check" v-for="(item,index) in property.values" :key="index">
            <div @tap="checkEngine(item)">
              <!-- <div
              class="popup_img"
             
              :class="['popup_img',{'popup_img1':item.checked}]"
              >-->
              <div class="popup_img" @tap="checkEngine(item,index)" :class="{'popup_img1':changeindex==index}">
                <img :src="item.imageUrl" alt style="width:160rpx;height:160rpx;" />
                <img src="https://m.aerp.com.cn/mini-app-main/toLarge.png" alt style="width:60rpx;height:60rpx;position:absolute;right:5rpx; bottom:5rpx;" @tap="previewImage([item.imageUrl],item.imageUrl)" :data-src="item.imageUrl" />
              </div>
            </div>
            <p class="popup_tit">{{item.displayValue}}</p>
          </div>
        </div>
        <div class="popup_btn">
          <button @tap="onClose" style="background:#ddd;color:#8F8F8F;">取消</button>
          <button @tap="onSure">确认</button>
        </div>
      </van-popup>
    </div>
    <!-- <div style="width:100%;" v-if="showPopup1">
      <van-popup :show="showPopup1" closeable @close="onClose1" position="center" lock-scroll custom-style="height:70%;" round :close-on-click-overlay="false" :overlay="false">
        <div class="popup_header">查看发动机基本信息</div>
        <div class="popup_title" style="font-weight:600;">车辆发动机相关信息可以在车辆铭牌上查看：</div>
        <div style="width:100%;padding:0 30rpx 30rpx;box-sizing:border-box;">
          <div class="popup_content1">
            <div class="popup_center">
              <p class="content_title1">品牌：华晨宝马</p>
              <p class="content_title2">制造国：德国</p>
            </div>
            <div class="popup_center">
              <p class="content_title1">生产厂：某某股份有限公司</p>
            </div>
            <div class="popup_center">
              <p class="content_title1">车辆识别代号：LFV3A28K7E3100848</p>
              <p class="content_title2">制造国：德国</p>
            </div>
            <div class="popup_center">
              <p class="content_title1">整车型号：A5 2.0T F5SCVK</p>
              <p class="content_title2">发动机排量：1984ml</p>
            </div>
            <div class="popup_center">
              <p class="content_title1">发动机型号：123456</p>
              <p class="content_title2">发动机最大净功率：140kw</p>
            </div>
            <div class="popup_center">
              <p class="content_title1">制造年月：2017.3</p>
              <p class="content_title2">最大允许总重量：1kg</p>
            </div>
          </div>
          <div class="brandTit">铭牌位置：</div>
        </div>
        <div style="width:100%;display:flex;justify-content:space-bentween;">
          <img src="https://m.aerp.com.cn/mini-app-main/fadongji1.png" alt class="brandImg" />
          <img src="https://m.aerp.com.cn/mini-app-main/fadongji2.png" alt class="brandImg" />
        </div>
      </van-popup>
    </div> -->
  </div>
</template>
<script>
import {
  GetShopDetails,
  GetWxOpenId,
  Location,
  PostNearbyStore,
  SharePromotionContent,
  VerifyAdaptiveProductForBuy,
  GetWxacodeScene
} from '../../api'
import eventBus from '../../utils/eventBus.js'
import store from '../../store.js'
export default {
  data() {
    return {
      code: '',
      isShowStep: true,
      ProductEntrance: 'NoSet',
      advertisement: '',
      stopAdd: false,
      changeindex: -1,
      property: {
        type: 'Property',
        title: '请选择发动机型号',
        content:
          '您所选的车型存在多款发动机型号，不同发动机型号的车型，配件存在差异。',
        name: '发动机',
        values: [
          {
            displayValue: 'CDS(2005/04/30之前)',
            imageUrl:
              'https://m.aerp.com.cn/BaoYang/Images/202006181140445722.png',
            key: 'CDS(2005/04/30之前)'
          }
        ]
      },
      engine: {}, // 选择的发动机类型
      propertyKey: '',
      propertyValue: '',
      showPopup: false,
      showPopup1: false,
      isFromShare: false,
      amount: 1,
      productInfos: [],
      carId: '',
      shopId: 0,
      user:"",
      attributevalues: [], // 已选数组
      autoplay: true,
      interval: 3000,
      duration: 500,
      images: [], // 轮播图数组
      nowPrice: '0.00', // 现在价格
      originalPrice: '0.00', // 原来价格
      shopTitle: '', // 商品标题
      productCode: '', // 商品编码
      whether: '', // 库存
      aa: 1, // 安装方式
      partCode: '', // 已选
      testShp: '', // 用此属性代替，商品是否匹配车型
      storeArr: null, // 门店信息
      descriptionImages: [], // 超值活动下的内容
      list: [], // 商品评价标签列表
      comment: [], // 页面整体数组
      srcs: `${this.globalData.imgPubUrl}Like - unchecked.png`, // 赞
      Url0: `${this.globalData.imgPubUrl}minevip_vip_zero.png`,
      Url10: `${this.globalData.imgPubUrl}minevip_vip_one.png`,
      Url20: `${this.globalData.imgPubUrl}minevip_vip_two.png`,
      Url30: `${this.globalData.imgPubUrl}minevip_vip_three.png`,
      Url40: `${this.globalData.imgPubUrl}minevip_vip_four.png`,
      Url50: `${this.globalData.imgPubUrl}minevip_vip_five.png`,
      commentNum: '', // 商品评价
      applauseRate: '', // 好评率
      isShoeShop: false, // 详情页面门店是否展示字段
      shop_productCode: '', // 上个页面传过来的商品id
      shop_provinceId: '', // 上个页面传过来的省id
      shop_cityId: '', // 上个页面传过来的城市id
      commArr: [
        {
          srcs: `${this.globalData.imgPubUrl}Like - unchecked.png`,
          url: `${this.globalData.imgPubUrl}Like - check.png`,
          isTrue: false
        }
      ] // 点赞图片数组
    }
  },
  computed: {
    carArr() {
      return this.$store.state.carArr
    },
    carArrFirst() {
      return this.$store.state.carArr[0] || null
    },
    // 车辆id
    cardID() {
      if (this.$store.state.carArr[0]) {
        return this.$store.state.carArr[0].carId
      } else {
        return ''
      }
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
      path: `/pages/detailsPages/main?cityId=''&provinceId=''&carId=''&shopId=0&isFromShare=true&isFromSearch=''&productCode=${
        that.shop_productCode
      }&serviceType=${that.globalData.serviceType}&OrderType=${
        that.globalData.OrderType
      }&ProductType=${that.globalData.ProductType}&activedId=1&shareUserId=${
        that.globalData.userInfo?that.globalData.userInfo.userId:''
      } `,
      imageUrl: that.images &&that.images.length > 0?that.images[0]:'',
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
  mounted() {
    let that = this
    // 获取系统信息
    wx.getSystemInfo({
      success(res) {
        let clientHeight = res.windowHeight
        let clientWidth = res.windowWidth
        let ratio = 750 / clientWidth
        that.windowHeight = clientHeight * ratio - 88 + 'rpx'
      }
    })
  },
  methods: {
    changeStore() {
      this.$router.push('/pages/stores/main')
    },
    changeCard() {
      this.$router.push('/pages/myCard/main')
    },
    autoLogin() {
      var that = this
      wx.login({
        success(res) {
          if (res.code) {
            wx.getUserInfo({
              success(userRes) {
                console.log(852, userRes)
                GetWxOpenId({
                  wxCode: res.code,
                  encryptedData: userRes.encryptedData,
                  iv: userRes.iv,
                  PlatForm: 2
                }).then(res1 => {
                  // console.log(`获取用户的oppenId,${res1}`)
                  that.globalData.userInfo = res1.data.userInfo
                  that.globalData.tokenInfo = res1.data.tokenInfo
                  wx.setStorageSync('tokenInfo', res1.data.tokenInfo)
                  that.globalData.openId = res1.data.openId
                  that.globalData.unionId = res1.data.unionId
                  if (!res1.data.isExistUser) {
                    // that.$router.push('/pages/authUserInfo/main')
                    that.getCity()
                    // that.getIndexpage()
                  } else {
                    that.getCity()
                    that.$store.dispatch('getCarArr')
                    // that.getIndexpage()
                  }
                })
              },
              fail() {
                that.getCity()
                // that.getIndexpage()
              }
            })
          }
        }
      })
    },
    toPopup() {
      this.showPopup = true
      // this.verifyAdaptiveProductForBuy()
    },
    toHelp() {
      this.showPopup1 = true
      console.log('查看发动机基本信息')
    },
    onClose1() {
      this.showPopup1 = false
    },
    // 弹框事件
    onClose() {
      this.showPopup = false
      // this.property.values.forEach(element => {
      //   element.checked = false
      // })
      this.changeindex = -1

      this.engine = {}
    },
    checkEngine(item, index) {
      // this.property.values.forEach(element => {
      //   element.checked = false
      // })
      this.changeindex = index
      // item.checked = !item.checked
      // console.log(item.checked)
      this.engine = item
      console.log('engine', this.engine)
      this.propertyValue = this.engine.key
      this.propertyKey = this.property.name

      // this.$forceUpdate()
    },
    // onSure() {
    //   console.log('onSure engine', this.engine)

    //   if (Object.keys(this.engine).length == 0) {
    //     console.log('未选择发动机')
    //     let that = this
    //     wx.showModal({
    //       title: '',
    //       content: '您有未选择的机油滤清器',
    //       confirmText: '去选择',
    //       cancelText: '取消',
    //       success: function(res) {
    //         if (res.confirm) {
    //         } else if (res.cancel) {
    //           that.showPopup = false
    //         }
    //       }
    //     })
    //   } else {
    //     // this.popup_content.forEach(element => {
    //     //   element.checked = false
    //     // })
    //     let that = this
    //     let properties = [
    //       {
    //         propertyKey: that.propertyKey,
    //         propertyValue: that.propertyValue
    //       }
    //     ]
    //     this.$store.dispatch('editUserCar', {
    //       carId: that.carId,
    //       carProperties: properties
    //     })
    //     console.log('carArr', this.$store.state.carArr[0])

    //     this.showPopup = false
    //   }
    // },
    // 点击查看大图
    previewImage(imgArr, imgIndex) {
      console.log(889911, imgArr, imgIndex)
      wx.previewImage({
        current: imgIndex,
        urls: imgArr
      })
    },
    getCity() {
      var that = this
      wx.getLocation({
        type: 'gcj02',
        altitude: true, // 高精度定位
        // 定位成功，更新定位结果
        success: res => {
          var latitude = res.latitude
          var longitude = res.longitude
          Location({ Longitude: longitude, Latitude: latitude }).then(res => {
            // that.globalData.currCityInfo = res.data
            store.commit('updateCity', res.data)
            store.commit('setPointCity', res.data)
            console.log(67895, that.globalData.currCityInfo)
            that.shop_cityId = res.data.cityId
            that.shop_provinceId = res.data.provinceId
            that.getShopPageInfo()
          })
        },
        // 定位失败回调
        fail() {
          that.getShopPageInfo()
          // console.log('定位失败123')
          // wx.navigateTo({ url: '/pages/city/main' })
          // wx.showToast({
          //   title: '定位失败',
          //   icon: 'none'
          // })
        },

        complete() {
          // 隐藏定位中信息进度
          wx.hideLoading()
        }
      })
    },
    onChange(e) {
      this.amount = e.mp.detail
    },
    sure() {
      let that = this
      if (this.isShoeShop) {
        if (this.$store.state.curCityInfo.city) {
          if (this.$store.state.carArr[0]) {
            if (this.storeArr != null) {
              that.productInfos = [
                { pid: that.productCode, number: that.amount }
              ]
              let pids = [that.productCode]
              that.$router.replace({
                path: '/pages/orderSure/main',
                query: {
                  code: that.code,
                  cardID: that.$store.state.carArr[0].carId,
                  storeid: that.shopId,
                  productInfos: JSON.stringify(that.productInfos),
                  pids:JSON.stringify(pids),
                  orderType: 6,
                  storeArr: JSON.stringify(that.storeArr)
                }
              })
            } else {
              let that = this
              wx.showModal({
                title: '提示',
                content: '请先选择为您服务的门店',
                showCancel: true,
                success(res) {
                  if (res.confirm) {
                    that.$router.push('/pages/stores/main')
                  } else if (res.cancel) {
                  }
                },
                fail() {
                  that.$router.push('/pages/stores/main')
                }
              })
            }
          } else {
            let that = this
            wx.showModal({
              title: '提示',
              content: '请先选择您的爱车',
              showCancel: true,
              success(res) {
                if (res.confirm) {
                  that.$router.push('/pages/carBrand/main')
                } else if (res.cancel) {
                }
              },
              fail() {
                that.$router.push('/pages/carBrand/main')
              }
            })
          }
        } else {
          wx.showModal({
            title: '提示',
            content: '请先选择城市',
            showCancel: true,

            success(res) {
              if (res.confirm) {
                that.toCity()
              } else if (res.cancel) {
                that.$router.push({ path: '/pages/index/main', isTab: true })
              }
            },
            fail() {
              that.$router.push({ path: '/pages/index/main', isTab: true })
            }
          })
        }
      } else {
        that.productInfos = [{ pid: that.productCode, number: that.amount }]
        that.storeArr = {}
        that.shopId = 0
        that.productInfos = [{ pid: that.productCode, number: that.amount }]
        let carId = ''
        if (that.$store.state.carArr[0]) {
          carId = that.$store.state.carArr[0].carId
        }
        
        let pids = [that.productCode]
        that.$router.replace({
          path: '/pages/orderSure/main',
          query: {
            code: that.code,
            cardID: carId,
            storeid: that.shopId,
            productInfos: JSON.stringify(that.productInfos),
            orderType: 2,
            pids:JSON.stringify(pids),
            storeArr: JSON.stringify(that.storeArr)
          }
        })            
      }
    },
    verifyAdaptiveProductForBuy() {
      let that = this
      console.log(that.productCode)

      let data = {
        shopId: that.shopId,
        pid: that.shop_productCode,
        vehicle: {
          vehicleId: that.$store.state.carArr[0].vehicleId,
          paiLiang: that.$store.state.carArr[0].paiLiang,
          nian: that.$store.state.carArr[0].nian,
          tid: that.$store.state.carArr[0].tid,
          properties: that.$store.state.carArr[0].carProperties[0],
          distance: that.$store.state.carArr[0].totalMileage,
          onRoadTime:
            that.$store.state.carArr[0].buyYear +
            '-' +
            that.$store.state.carArr[0].buyMonth,
          tireSize: that.$store.state.carArr[0].tireSizeForSingle
        }
      }
      VerifyAdaptiveProductForBuy(data).then(res => {
        // this.property = res.data.property
      })
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
    },
    // 更多点击事件
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

    back() {
      this.$router.go(-1)
    },
    // 已选规格
    selectedSpecifications() {},

    // 获取商品详情页面刚加载函数
    getShopPageInfo() {
      let that = this
      let shop = {
        provinceId: that.shop_provinceId, // '310000'
        cityId: that.shop_cityId, // '310100'
        productCode: that.shop_productCode, // 'BYFW-JY-TSO-150'
        ProductEntrance: that.ProductEntrance
      }
      // 获取商品详情（已完成）
      GetShopDetails(shop).then(res => {
        that.originalPrice = res.data.product.standardPrice.toFixed(2)
        that.nowPrice = res.data.product.salesPrice.toFixed(2)
        that.shopTitle = res.data.product.name
        that.advertisement = res.data.product.advertisement
        that.images = res.data.images
        that.productCode = res.data.product.productCode
        that.whether = res.data.product.hasStock
        that.partCode = res.data.product.partCode
        that.descriptionImages = res.data.descriptionImages
        that.attributevalues = res.data.attributevalues
        if (this.globalData.ProductType != 14) {
          that.getShop()
          this.isShoeShop = true
        }else{
           this.isShoeShop = false
        }
      })
    },
    toCity() {
      this.$router.push('/pages/city/main')
    },
    getShop() {
      let that = this
      if (this.$store.state.curCityInfo.city) {
        // 获取门店
        wx.getLocation({
          type: 'gcj02',
          altitude: true, // 高精度定位
          // 定位成功，更新定位结果
          success: res => {
            that.longitude = res.longitude
            that.latitude = res.latitude
            let rquestshop = {
              shopIds: [],
              longitude: res.longitude,
              latitude: res.latitude,
              cityId: that.$store.state.curCityInfo.cityId,
              districtId: that.$store.state.curCityInfo.districtId,
              orderBy: 0,
              pageIndex: 1,
              pageSize: 1
            }
            // 获取附近门店列表

            PostNearbyStore(rquestshop).then(res => {
              that.storeArr = res.data.items[0]
              that.shopId = res.data.items[0].id
            })
          },
          // 定位失败回调
          fail() {
            let rquestshop = {
              shopIds: [],
              longitude: 0,
              latitude: 0,
              cityId: that.$store.state.curCityInfo.cityId,
              districtId: that.$store.state.curCityInfo.districtId,
              orderBy: 0,
              pageIndex: 1,
              pageSize: 1
            }
            // 获取附近门店列表
            PostNearbyStore(rquestshop)
              .then(res => {
                // console.log(`获取附近门店列表成功函数,${JSON.stringify(res)}`)

                that.storeArr = res.data.items[0]
                that.shopId = res.data.items[0].id
              })
              .catch(err => {
                console.log(`获取附近门店列表失败函数,${err}`)
              })
          }
        })
      } else {
        wx.showModal({
          title: '提示',
          content: '请先选择城市',
          showCancel: true,

          success(res) {
            if (res.confirm) {
              that.toCity()
            } else if (res.cancel) {
              that.$router.push({ path: '/pages/index/main', isTab: true })
            }
          },
          fail() {
            that.$router.push({ path: '/pages/index/main', isTab: true })
          }
        })
      }
    }
  },
  async onLoad(options, query) {
    if (options.isShowStep === 'false') {
      this.isShowStep = false
      this.code = options.code
    }
    let that = this
    eventBus.$on('selectShop', item => {
      // that.userCouponDisplayName = item.displayName\
      console.log('selectCoupon', item)
      that.storeArr = item
      that.storeid = item.id
    })
    if (options.scene) {
      const scene = options.scene
      let res = await GetWxacodeScene({ SceneId: scene }).catch(() => {
        wx.showModal({
          title: '提示',
          content: '扫码解析错误将为您定位到首页',
          showCancel: false,
          success(res) {
            if (res.confirm) {
              that.$router.push({ path: '/pages/index/main', isTab: true })
            }
          }
        })
      })
      if (res.code === 10002) {
        wx.showModal({
          title: '提示',
          content: '扫码解析错误将为您定位到首页',
          showCancel: false,
          success(res) {
            if (res.confirm) {
              that.$router.push({ path: '/pages/index/main', isTab: true })
            }
          }
        })
        return false
      }
      options = res.data
    }

    if (options.activedId == 1) {
      that.globalData.serviceType = options.serviceType
      console.log('globalData.serviceType', that.globalData.serviceType)
      that.globalData.OrderType = options.OrderType
      console.log('globalData.OrderType', that.globalData.OrderType)
      this.globalData.ProductType = options.ProductType
      console.log('globalData.ProductType', that.globalData.ProductType)
      this.globalData.shareUserId = options.shareUserId
      that.stopAdd = true
    }
    that.shop_productCode = options.productCode
    if (options.q) {
      console.log(options.q)
      let res = decodeURIComponent(options.q)
      console.log(123456, res)
      that.globalData.serviceType = this.getRequest2('serviceType', res)
      that.globalData.serviceType = options.serviceType
      console.log('globalData.serviceType', that.globalData.serviceType)
      that.globalData.OrderType = this.getRequest2('OrderType', res)
      console.log('globalData.OrderType', that.globalData.OrderType)
      that.globalData.ProductType = this.getRequest2('ProductType', res)
      console.log('globalData.ProductType', that.globalData.ProductType)
      that.shop_productCode = this.getRequest2('productCode', res)
      console.log('123457', that.shop_productCode)
      that.autoLogin()
    }
    console.log('获取参数')
    if (options.shopId) {
      that.shopId = options.shopId
    }
    if (options.user) {
      that.user = options.user
      wx.showModal({
          title: '提示',
          content:  '获取到了扫码传来的参数 '+options.shopId + options.user,
          showCancel: false,
          success(res) {
            
          }
        })
    }
    if (options.ProductEntrance) {
      that.ProductEntrance = options.ProductEntrance
    }
    that.shop_cityId = options.cityId
    that.shop_provinceId = options.provinceId
    that.carId = options.carId
    that.shopId = options.shopId
    // 判断此字段的布尔值，来控制门店显示
    if (options.isFromSearch == 'istrue') {
      this.storeArr = JSON.parse(options.storeArr)
    }
    if (options.isFromShare == 'true' || options.isFromShare === true) {
      this.isFromShare = true
      this.autoLogin()
      SharePromotionContent({
        forwardType: 'WxFriend',
        promoteContentType: 'ProductDetail',
        id: 0,
        productPromotionId: '',
        pid: that.shop_productCode
      })
    }
    this.getShopPageInfo()

    // 页面刚加载函数
  },
  onUnload() {
    eventBus.$off('selectShop')
    this.sFromShare = false
    this.images = []
    this.storeArr = null
    this.descriptionImages = []
    this.testShp = ''
    this.whether = ''
    this.nowPrice = ''
    this.originalPrice = ''
    this.shopTitle = ''
    this.productCode = ''
    this.stopAdd = false
    this.ProductEntrance = 'NoSet'
    this.isShoeShop = false
    this.isShowStep = true
    this.code = ''
  }
}
</script>

<style scoped lang="scss">
.top_card {
  margin-top: 20rpx;
  width: 100%;
  // height: 320rpx;
  display: flex;
  flex-direction: column;
  // justify-content: center;
  align-items: center;
  background: rgba(255, 255, 255, 1);
  .card_tab {
    width: 100%;
    height: 160rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 10rpx;
    padding: 20rpx 28rpx 25rpx 28rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    border-bottom: 1rpx solid #f3f3f3;
    // margin-top: 16rpx;
    .img_card {
      width: 100rpx;
      height: 100rpx;
    }
    .card_content {
      width: 486rpx;
      height: 125rpx;
      padding: 20rpx 0;
      box-sizing: border-box;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-start;
      .p1 {
        width: 100%;
        height: 80rpx;
        font-size: 28rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
        overflow: hidden;
        white-space: pre-wrap;
        display: -webkit-box;
        text-overflow: ellipsis;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
      }
    }
  }
  .p12 {
    height: 50rpx;
    font-size: 24rpx;
    font-family: SourceHanSansCN;
    font-weight: 400;
    color: rgba(136, 136, 136, 1);
    overflow: hidden;
    white-space: pre-wrap;
    display: -webkit-box;
    text-overflow: ellipsis;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
  }
  .card_add {
    width: 100%;
    height: 100rpx;
    border: 2rpx solid #f3f3f3;
    line-height: 96rpx;
    background: rgba(255, 255, 255, 1);
    font-size: 30rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(51, 51, 51, 1);
    text-align: center;
  }
  .card_store {
    width: 100%;
    min-height: 150rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 10rpx;
    padding: 10rpx 28rpx 10rpx 28rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .img_card {
      width: 100rpx;
      height: 100rpx;
    }
    .card_content {
      width: 486rpx;
      // height: 125rpx;
      // padding: 20rpx 0;
      box-sizing: border-box;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-start;
      .p1_p1 {
        width: 454rpx;
        min-height: 41rpx;

        font-size: 28rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
        overflow: hidden;
        white-space: pre-wrap;
        display: -webkit-box;
        text-overflow: ellipsis;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
      }
      .p2_p2 {
        width: 454rpx;
        height: 69rpx;
        font-size: 24rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(136, 136, 136, 1);
        overflow: hidden;
        white-space: pre-wrap;
        display: -webkit-box;
        text-overflow: ellipsis;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
      }
    }
  }
  .store_add {
    width: 100%;
    height: 100rpx;
    border: 2rpx solid #f3f3f3;
    line-height: 96rpx;
    background: rgba(255, 255, 255, 1);
    font-size: 30rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(51, 51, 51, 1);
    text-align: center;
  }
}
button {
  padding: 0;
}
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
  padding: 30rpx;
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
  position: relative;
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
  position: absolute;
  right: -30rpx;
  top: 0;
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
  background: #e8e8e8;
  height: 80rpx;
  width: 350rpx;

  border-radius: 40rpx 0 0 40rpx;
  display: flex;
  justify-content: center;
  align-items: center;
  color: #fff;

  // border-radius: 40rpx;
}
.bbutton1 {
  background: #ff6324;

  height: 80rpx;
  width: 350rpx;

  border-radius: 0 40rpx 40rpx 0;
  display: flex;
  justify-content: center;
  align-items: center;
  color: #fff;
}
.bbutton2 {
  background: #ff6324;
  height: 80rpx;
  width: 650rpx;
  display: flex;
  justify-content: center;
  align-items: center;
  color: #fff;
  border-radius: 30rpx;
}
.popup_btn {
  width: 100%;
  padding: 40rpx 0;
  display: flex;
  flex-wrap: nowrap;
}
.popup_btn > button {
  width: 240rpx;
  height: 90rpx;
  background: #ff6324;
  margin: 0 auto;
  border-radius: 45rpx;
  color: #fff;
}
.popup_header {
  width: 660rpx;
  text-align: center;
  font-weight: bold;
  line-height: 100rpx;
  font-size: 35rpx;
  background-image: url('https://m.aerp.com.cn/mini-app-main/titleBg.png');
  background-repeat: no-repeat;
  background-size: 100% 100%;
}
.popup_title {
  padding: 30rpx;
  color: #888;
  font: 26rpx/1.5 'SourceHanSansCN-Regular';
}
.popup_content {
  width: 100%;
  display: flex;
  align-items: center;
  justify-content: flex-start;
  flex-wrap: wrap;
}
.popup_content1 {
  width: 100%;
  //   display:flex;
  //   align-items: center;
  // flex-direction: column;
  background: #f2f2f2;
  min-height: 100rpx;
}
.popup_center {
  overflow: hidden;
  color: #666;
  font: 24rpx/1 'SourceHanSansCN-Regular';
  padding: 20rpx;
  border-radius: 10rpx;
}
.content_title1 {
  float: left;
}
.content_title2 {
  float: right;
}
.brandTit {
  color: #333333;
  font: 600 26rpx/2 'SourceHanSansCN-Regular';
  margin: 20rpx 0;
}
.brandImg {
  width: 45%;
  height: 200rpx;
  margin: -20rpx 20rpx 20rpx;
}
.popup_check {
  width: 50%;
  display: flex;
  align-items: center;
  flex-direction: column;
  justify-content: center;
}
.popup_img {
  width: 260rpx;
  height: 260rpx;
  display: flex;
  align-items: center;
  justify-content: center;
  border: 1rpx solid #ddd;
  position: relative;
}
.popup_img1 {
  width: 260rpx;
  height: 260rpx;
  display: flex;
  align-items: center;
  justify-content: center;
  border: 1rpx solid #000;
  position: relative;
}
.popup_tit {
  color: #030000;
  font: 28rpx/1.5 'SourceHanSansCN-Regular';
  padding: 20rpx 0;
}
.closeImg {
  width: 68rpx;
  height: 68rpx;
  position: fixed;

  left: 0;
  right: 0;
  bottom: 30rpx;
  margin: 0 auto;
  z-index: 999;
}
>>> .van-stepper__minus {
  border-radius: 50%;
  background: #fff;
  color: #adadad;
  border: 2rpx solid rgba(237, 237, 237, 1);
  box-shadow: 0 6rpx 16rpx 0 rgba(211, 211, 211, 0.68);
}
>>> .van-stepper__input {
  background: none;
  font-size: 32rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(63, 63, 63, 1);
}
>>> .van-stepper__plus {
  border-radius: 50%;
  background: rgba(255, 99, 36, 1);
  // box-shadow:0px 6rpx 16rpx 0rpx rgba(255,99,36,0.68);
  color: #fff;
}
</style>
