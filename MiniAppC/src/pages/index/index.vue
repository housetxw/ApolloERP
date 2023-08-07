<template>
  <div class="page">
    <navigation-bar :title="videoTitle" :titleColor="'green'" :back-visible="true" :home-path="'/pages/index/main'"></navigation-bar>
    <div class='body'>

      <div class="car car1" @click="addCar" v-if="carArrFirst==null">
        <img src="https://m.aerp.com.cn/mini-RG-main/home_add.png" alt class="img_card" mode="aspectFit" style="width:41rpx;height:41rpx;margin:10rpx 20rpx 0 0;" />
        <span>点击添加我要保养的车型</span>
      </div>
      <div class="car car2" v-else>
        <div class="card_tab" @tap="changeCard">
          <img :src="carArrFirst.brandUrl" alt class="img_card" mode="aspectFit" />
          <div class="card_content">
            <p class="p1">{{carArrFirst.vehicle}}</p>
            <p class="p2">{{carArrFirst.carNumber}}</p>
          </div>
          <div style="display:flex;flex-direction:column;align-items:center;">
            <img src="https://m.aerp.com.cn/mini-RG-main/changeCarIcon.png" alt style="width:60rpx;height:60rpx;margin-top:20rpx;" />
            <p class="p2">换车</p>
          </div>
        </div>
      </div>


      <swiper v-if="imgUrls.length > 0" :indicator-dots='true' :autoplay='true' style="width:690rpx;height:260rpx;">
        <block v-for="(item, index) in imgUrls" :key="index">
          <swiper-item>
            <image :src="item.imageUrl" mode="widthFix" style="width:690rpx;height:260rpx;" @click="onBannerClick(item)"></image>
          </swiper-item>
        </block>
      </swiper>


      
      <div class="topCont">
        <div class="topCont-item" v-for="(item, index) in serveList" :key="index" link-type="navigateTo" @click="toService(item,index)">
          <image :src="item.imageUrl" mode="widthFix" style="width:82rpx;height:82rpx;"></image>
          <span style="font-size: 24rpx;font-family: Source Han Sans CN;font-weight: 500;color: #333333;margin-top:15rpx">{{item.displayName}}</span>
        </div>

      </div>
      <div class="combo" @click="toCombo">
        <div class="comboTop">
          <div class="comboTopL">套餐卡</div>
          <div class="comboTopR">更多<img src="https://m.aerp.com.cn/mini-RG-main/maintenance_jump_icon.png" alt style="width:9rpx;height:16rpx;margin-left:13rpx;" /></div>
        </div>
        <div class="comboBot">
          <image class="comboBotItem" :src="item.image1" v-for="(item, index) in packageCard" :key="index">

          </image>
        </div>
      </div>
      <div class="seckill" @click="toSeckill" v-if="seckillList.length>0">
        <div class="seckillTop">
          <div class="seckillTopL"><span style="margin:0 46rpx 0 13rpx">限时秒杀</span>
            <van-count-down use-slot :time="time" @change="onChange">
              <text class="item">{{ timeData.hours }}</text><span class="colon">:</span>
              <text class="item">{{ timeData.minutes }}</text><span class="colon">:</span>
              <text class="item">{{ timeData.seconds }}</text>
            </van-count-down>
          </div>
          <div class="seckillTopR">超值好物 立即抢购<img src="https://m.aerp.com.cn/mini-RG-main/white_arrow.png" alt style="width:9rpx;height:16rpx;margin-left:13rpx;" /></div>
        </div>
        <div class="seckillBottom">
          <div class="seckillItem" v-for="(item, index) in seckillList" :key="index">
            <image :src="item.image1" style="width:200rpx;height:200rpx;" mode="aspectFit"></image>
            <div class="seckillItem-word">
              {{item.productName}}
            </div>
            <div class="seckillItem-price">
              ￥{{item.price}} <span class="seckillItem-old-price">￥{{item.salesPrice}}</span>
            </div>
          </div>

        </div>
      </div>
      <!-- 九宫格 -->
      <!-- <div class="gridWrap">
        <van-grid column-num="2">
          <van-grid-item use-slot v-for="(item, index) in serveList" :key="index" link-type="navigateTo" @click="toService(item,index)">
            <div class='grid'>
              <div class='gridLeft'>
                <img :src="item.imageUrl" alt="" class='gridLeftImg'>
              </div>
              <div class='gridRight'>
                <div class='gridRightTop'>{{item.displayName}}</div>
                <div class='gridRightBot'>{{item.subTitle}}</div>
              </div>
            </div>
          </van-grid-item>
        </van-grid>
      </div> -->
      <!-- 超值活动 -->
      <!-- <div class="activeTitle" v-if="show4">
        <img src="https://m.aerp.com.cn/mini-RG-main/title_left_icon.png" alt="" class="activeImg">
        <span class="activeWord">超值活动</span>
        <img src="https://m.aerp.com.cn/mini-RG-main/title_right_icon.png" alt="" class="activeImg">
      </div> -->
      <!-- <div class='activeList'  >
      <swiper v-if="imgUrls2.length >= 0" 
        :autoplay='true'
        next-margin='20rpx'
        style="width:690rpx;height:140rpx;" 
       
        >
       <block v-for="(item, index) in imgUrls2" :key="index"  >
       
    <navigator url="'/pages/detailsPages/main?productCode='+imgUrls2Key" >   
          <swiper-item>
            <image :src="item.imageUrl" mode="widthFix" style="width:690rpx;height:180rpx;" ></image>
          </swiper-item>
          
      </navigator>
        </block>
      </swiper> 
      
    </div>   -->
      <div class='activeList'>
        <swiper v-if="imgUrls2.length > 0" :autoplay='true' style="width:690rpx;height:150rpx;">
          <block v-for="(item1, index1) in imgUrls2" :key="index1">
            <swiper-item>
              <image :src="item1.imageUrl" mode="widthFix" style="width:690rpx;height:150rpx;" @tap="swipclick(item1)"></image>
            </swiper-item>
          </block>
        </swiper>
      </div>

      <div class="activeTitle" v-if="show4">
        <img src="https://m.aerp.com.cn/mini-RG-main/title_left_icon.png" alt="" class="activeImg">
        <span class="activeWord">热销产品</span>
        <img src="https://m.aerp.com.cn/mini-RG-main/title_right_icon.png" alt="" class="activeImg">
      </div>

      <div class="hot">
        <div class="hot-item" v-for="(item, index) in HotProduct" :key="index" @click="toDetail(item)">
          <image :src="item.image1" style="width:290rpx;height:290rpx;" mode="aspectFit"></image>
          <div class="hot-item-word">
            {{item.name}}
          </div>
          <div class="hot-item-price">
            ￥{{item.salesPrice}}
          </div>
        </div>
      </div>

      <!-- 客服 -->
      <div class="service" @tap="serviceClick">
        <img src="https://m.aerp.com.cn/mini-RG-main/index_customer_icon.png" alt="" class='serviceImg'>
        <span class="serviceWord">客服</span>
      </div>
    </div>
    <!-- <div  @tap="goPage38">
      领取专区
    </div> -->
  </div>
</template>

<script>
import navigationBar from '../../components/navigationBar.vue'
import {
  GetIndexPage,
  GetWxOpenId,
  Location,
  GetHotProductPageList,
  GetPackageCardProduct,
  GetActiveFlashSaleConfigs
} from '../../api'
import eventBus from '../../utils/eventBus.js'
import store from '../../store'
export default {
  components: {
    navigationBar
  },
  data() {
    return {
      seckillList: [],
      packageCard: [],
      HotProduct: [],
      time: 0,
      timeData: {},
      imgUrls2Key: '',
      motto: 'Hello miniprograme',
      show4: false,
      userInfo: {
        nickName: 'mpvue',
        avatarUrl: 'https://mpvue.com/assets/logo.png'
      },
      imgUrls: [
        // {advertisingCode: "activity1", imageUrl: "https://m.aerp.com.cn/Images/Logo/benchi.png", routeUrl: ""},
       //  {"advertisingCode": "string", "imageUrl": "string",   "beginImageUrl": "string",   "btnImageUrl": "string",
        // "endImageUrl": "string",        "routeUrl": "string",        "key": "string"      }
      ],
      imgUrls2: [
        //   {
        //   advertisingCode: 'ProminentActive1',
        //   imageUrl: 'https://m.aerp.com.cn/Images/Logo/bentian.png',
        //   routeUrl: ''
        // },
      ],
      val: 6,
      videoTitle: '自定义标题',
      serveList: []
      // citymove:''
    }
  },
  computed: {
    carArrFirst() {
      return this.$store.state.carArr[0] || null
    }
  },
  mounted() {
    let myDate = new Date()
    this.time =
      (23 - myDate.getHours()) * 60 * 60 * 1000 +
      (59 - myDate.getMinutes()) * 60 * 1000 +
      (60 - myDate.getSeconds()) * 1000
    this.checkUpdate()
    this.autoLogin()
    this.getHotProductPageList()
    this.getPackageCardProduct()
    this.getActiveFlashSaleConfigs()
    eventBus.$on('login', () => {
      this.$store.dispatch('getCarArr').then(carArr => {
        this.carArrFirst = carArr[0]
      })
    })
  },
      
  methods: {
    checkUpdate(){
        console.log("checkUpdate")
        if (wx.canIUse('getUpdateManager')) {
            const updateManager = wx.getUpdateManager();
            updateManager.onCheckForUpdate(function (res) {
                console.log("onCheckForUpdate")
                if (res.hasUpdate) { // 请求完新版本信息的回调
                    updateManager.onUpdateReady(function () {
                        wx.showModal({
                            title: '更新提示',
                            content: '新版本已经准备好，是否重启应用？',
                            success: function (res) {
                                if (res.confirm) {// 新的版本已经下载好，调用 applyUpdate 应用新版本并重启
                                    updateManager.applyUpdate()
                                }
                            }
                        })
                    });
                    updateManager.onUpdateFailed(function () {
                        wx.showModal({// 新的版本下载失败
                            title: '已经有新版本了哟~',
                            content: '新版本已经上线啦~，请您删除当前小程序，重新搜索进入哟~',
                        })
                    })
                }
            })
        } else {
            wx.showModal({// 如果希望用户在最新版本的客户端上体验您的小程序，可以这样子提示
                title: '提示',
                content: '当前微信版本过低，无法使用该功能，请升级到最新微信版本后重试。'
            })
        }
    },
    toCombo() {
      this.globalData.serviceType = 'toShop'

      this.globalData.OrderType = 6

      this.globalData.ProductType = 14

      this.$router.push('/pages/comboList/main')
    },
    toSeckill() {
      this.globalData.serviceType = 'toShop'
      this.globalData.OrderType = 6
      this.globalData.ProductType = 17
      this.$router.push('/pages/seckill/main')
    },
    toDetail(itemm) {
      this.globalData.serviceType = 'toShop'
      this.globalData.OrderType = 6
      this.globalData.ProductType = 18
      let that = this
      this.$router.push({
        path: '/pages/detailsPages/main',
        query: {
          provinceId: that.$store.state.curCityInfo.provinceId,
          cityId: that.$store.state.curCityInfo.cityId,
          productCode: itemm.productCode
        }
      })
    },
    getActiveFlashSaleConfigs() {
      GetActiveFlashSaleConfigs({})
        .then(res => {
          this.seckillList = res.data
        })
        .catch(err => {
          console.log(`首页数据信息获取失败函数,${err}`)
        })
    },
    getPackageCardProduct() {
      GetPackageCardProduct({
        PageSize: 2,
        PageIndex: 1
      })
        .then(res => {
          this.packageCard = res.data.items
        })
        .catch(err => {
          console.log(`首页数据信息获取失败函数,${err}`)
        })
    },
    getHotProductPageList() {
      GetHotProductPageList({
        PageSize: 100,
        PageIndex: 1
      })
        .then(res => {
          this.HotProduct = res.data.items
        })
        .catch(err => {
          console.log(`首页数据信息获取失败函数,${err}`)
        })
    },
    onChange(e) {
      this.timeData = e.mp.detail
    },
    addCar() {
      this.$router.push('/pages/carBrand/main')
    },
    // banner图片点击
    onBannerClick(item) {
      console.log('onBannerClick',item.routeUrl)
      if (item.routeUrl) {
        if(item.routeUrl.substr(0, 4)=="http" ){
          //  wx.navigateTo({
          //   url: '/pages/commonWeb/main?url='+encodeURIComponent(item.routeUrl)
          // })
        }else{
          // var itemStr = JSON.stringify(item)
          // console.log("------------------------" )
          // console.log( itemStr)
          // console.log("------------------------")
          this.$router.push({
            path: item.routeUrl,
            query: {
              code: item.advertisingCode || '',
              // key: item.key,
              // beginImageUrl:  item.beginImageUrl,
              // btnImageUrl:  item.btnImageUrl,
              // btn2ImageUrl: item.btn2ImageUrl,
              // btn3ImageUrl: item.btn3ImageUrl,
              // endImageUrl:  item.endImageUrl,
              // gotoUrl: encodeURIComponent(item.gotoUrl),
              // goto2Url: encodeURIComponent(item.goto2Url),
              // goto3Url: encodeURIComponent(item.goto3Url),
            }
          })

          // wx.navigateTo({
          //   url: item.routeUrl+'?key=' + item.key
          //                   +'&beginImageUrl='+item.beginImageUrl
          //                   +'&btnImageUrl='+item.btnImageUrl
          //                   +'&btn2ImageUrl='+item.btn2ImageUrl
          //                   +'&btn3ImageUrl='+item.btn3ImageUrl                         
          //                   +'&endImageUrl='+item.endImageUrl
          //                   +'&gotoUrl='+item.gotoUrl
          //                   +'&goto2Url='+item.goto2Url
          //                   +'&goto3Url='+item.goto3Url
                            
          // })
        }
      }else{
          var itemStr = JSON.stringify(item)
          console.log("------------------------" + itemStr)
          wx.navigateTo({
            url: '/pages/bannerDetail/main?banner=' + itemStr
          })
      }
    },
    swipclick(item1) {
      // if (item1.advertisingCode == 'ProminentActive1') {
      //   wx.navigateTo({
      //     url: `/pages/detailsPages2/main?productCode=${
      //       item1.key
      //     }&serviceType=toHome&OrderType=2&ProductType=5&activedId=1&shopId=0`
      //   })
      //   // let that = this
      //   // this.$router.push({
      //   //   path: '/pages/detailsPages2/main',
      //   //   query: {
      //   //     provinceId: that.$store.state.curCityInfo.provinceId,
      //   //     cityId: that.$store.state.curCityInfo.cityId,
      //   //     productCode: item1.key
      //   //   }
      //   // })
      // } else {
          // wx.navigateTo({
          //   url: item1.routeUrl + '?activeId=' + item1.key
          // })
          // var itemStr = JSON.stringify(item1)
          if(item1.routeUrl){
            let pathUrl = item1.routeUrl.startsWith('/')?item1.routeUrl:('/'+item1.routeUrl)
            this.$router.push({
              path: pathUrl,
              query: {
                // paramData:itemStr
                code:item1.advertisingCode || '',
              }
            })
          }else{
             wx.showModal({// 如果希望用户在最新版本的客户端上体验您的小程序，可以这样子提示
                title: '提示',
                content: '该服务路径为空！'
            })
          }
      // }
    },
    autoLogin() {
      var that = this
      wx.login({
        success(res) {
          if (res.code) {
            console.log("wxCode", res.code)
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
                  that.$store.dispatch('getCarArr').then(carArr => {
                    that.carArrFirst = carArr[0]
                  })

                  that.getCity()
                  that.getIndexpage()
                })
              },
              fail() {
                that.getCity()
                that.getIndexpage()
                that.carArrFirst = wx.getStorageSync('tokenInfo') || null
              }
            })
          }
        }
      })
    },

    toService(item, index) {
      this.$router.push({
        path: item.routeUrl,
        query: {
          CategoryId: 1,
          baoYangType: JSON.stringify(item.packageType)
        }
      })
    },

    toCity() {
      this.$router.push('/pages/city/main')
    },
    // 换车
    changeCard() {
      this.$router.push('/pages/myCard/main')
    },
    getCity() {
      console.log('定位失败1235')
      wx.showLoading({
        title: '定位中',
        mask: true
      })
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
            console.log(67895, res.data)
          })
        },
        // 定位失败回调
        fail: function() {
          console.log('定位失败123')
          wx.navigateTo({ url: '/pages/city/main' })
          wx.showToast({
            title: '定位失败',
            icon: 'none'
          })
        },

        complete: function() {
          // 隐藏定位中信息进度
          wx.hideLoading()
        }
      })
    },
    // 客服事件
    serviceClick() {
            wx.openCustomerServiceChat({
              extInfo: {url: 'https://work.weixin.qq.com/kfid/kfccd9327fbcde02b4d'},
              corpId: 'ww53f79834a2874e28',
              success(res) {}
            } )

      /* wx.showModal({
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
      }) */
    },
    // 首页数据信息获取
    getIndexpage() {
      let that = this
      GetIndexPage({ Version: 2 })
        .then(res => {
          let allBanner = res.data.prominentActive || []
          that.imgUrls = allBanner.filter(item=>item.type == 'Top')
          that.imgUrls2 = allBanner.filter(item=>item.type == 'Bottom') // 超值活动
          // that.imgUrls = res.data.topAdvertising // 轮播topAdvertising
          that.serveList = res.data.modules // 功能块
          console.log(that.serveList)
          // that.imgUrls2 = res.data.prominentActive // 超值活动
          that.imgUrls2Key = that.imgUrls2[0].key

          that.show4 = true
        })
        .catch(err => {
          console.log(`首页数据信息获取失败函数,${err}`)
        })
    },
    bindViewTap() {
      const url = '../logs/main'
      if (mpvuePlatform === 'wx') {
        mpvue.switchTab({ url })
      } else {
        mpvue.navigateTo({ url })
      }
    },
    getPhoneNumber(e) {
      console.log(e)
    },
    onClickLeft() {
      wx.showToast({ title: '点击返回', icon: 'none' })
    },
    onClickRight() {
      wx.showToast({ title: '点击按钮', icon: 'none' })
    },
    getUserInfo(res) {
      console.log(22, res)
    },
    toActive(item) {
      console.log(121, item.advertisingCode)
      console.log(item)
      this.$router.push({
        path: 'item.routeUrl',
        query: {
          productCode: item.advertisingCode,
          activeId: 1
        }
      })
    }
  },
  onShareAppMessage(res) {
    let that = this
    if (res.from === 'button') {
      // 来自页面内转发按钮
      console.log(res.target)
    }
    return {
      title: '致大养车',
      path: `/pages/index/main?activedId=1&shareUserId=${that.globalData.userInfo?that.globalData.userInfo.userId:''}`
    }
  },
  created() {
    // let app = getApp()
  },
  onLoad(options){
    if (options.activedId == 1) {
      this.globalData.shareUserId = options.shareUserId
    }
  }
}
</script>

<style scoped lang="scss">
.item {
  display: inline-block;
  width: 35rpx;
  height: 35rpx;
  line-height: 35rpx;

  text-align: center;
  background-color: #fff;
  border-radius: 5rpx;

  font-size: 26rpx;
  font-family: PingFang SC;
  font-weight: 500;
  color: #f14553;
}
.hot {
  display: flex;
  justify-content: space-between;
  flex-wrap: wrap;
  .hot-item {
    width: 330rpx;
    height: 480rpx;
    background: #ffffff;
    margin-bottom: 20rpx;
    box-sizing: border-box;
    padding: 20rpx;
    .hot-item-word {
      font-size: 28rpx;
      font-family: PingFang SC;
      font-weight: 500;
      color: #333333;
      line-height: 40rpx;
      height: 80rpx;
      margin: 16rpx 0;
      overflow: hidden;
      text-overflow: ellipsis;
      display: -webkit-box;
      -webkit-line-clamp: 2;
      -webkit-box-orient: vertical;
    }
    .hot-item-price {
      font-size: 36rpx;
      font-family: PingFang SC;
      font-weight: bold;
      color: #ff6214;
      line-height: 35rpx;
    }
  }
}
.seckill {
  width: 692rpx;
  height: 455rpx;
  margin-bottom: 37rpx;
  background-image: linear-gradient(to right, #ff6214, #ff352e);
  border-radius: 10rpx;
  margin-top: 21rpx;
  box-sizing: border-box;
  padding: 0 11rpx;
  .seckillTop {
    display: flex;
    align-items: center;
    height: 82rpx;
    justify-content: space-between;
    .seckillTopL {
      display: flex;
      align-items: center;

      font-size: 32rpx;
      font-family: PingFang SC;
      font-weight: bold;
      color: #ffffff;
      .colon {
        font-size: 26rpx;
        font-family: PingFang SC;
        font-weight: 400;
        color: #ffffff;
        margin: 0 12rpx;
      }
    }
    .seckillTopR {
      display: flex;
      align-items: center;

      font-size: 24rpx;
      font-family: PingFang SC;
      font-weight: 400;
      color: #ffffff;
    }
  }
  .seckillBottom {
    width: 671rpx;
    height: 360rpx;
    background: #ffffff;
    border-radius: 10rpx;
    display: flex;
    align-items: center;
    justify-content: space-around;
    .seckillItem {
      width: 200rpx;
      .seckillItem-word {
        height: 65rpx;
        font-size: 24rpx;
        font-family: PingFang SC;
        font-weight: 500;
        color: #333333;
        line-height: 34rpx;
        overflow: hidden;
        text-overflow: ellipsis;
        display: -webkit-box;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
      }
      .seckillItem-price {
        height: 23rpx;
        font-size: 19rpx;
        font-family: PingFang SC;
        font-weight: 800;
        color: #ff6214;
        line-height: 41rpx;
        .seckillItem-old-price {
          height: 16rpx;
          font-size: 20rpx;
          font-family: PingFang SC;
          font-weight: 400;
          text-decoration: line-through;
          color: #999999;
          line-height: 41rpx;
        }
      }
    }
  }
}
.combo {
  width: 690rpx;
  height: 317rpx;
  margin-top: 21rpx;
  background: #ffffff;
  box-shadow: 0px 0px 15rpx 0px rgba(44, 49, 140, 0.11);
  border-radius: 10rpx;
  box-sizing: border-box;
  padding: 0 15rpx;
  .comboTop {
    display: flex;
    align-items: center;
    justify-content: space-between;
    height: 95rpx;
    .comboTopL {
      font-size: 32rpx;
      font-family: PingFang SC;
      font-weight: bold;
      color: #333333;
      margin-right: 12rpx;
    }
    .comboTopR {
      display: flex;
      align-items: center;
      font-size: 24rpx;
      font-family: PingFang SC;
      font-weight: 400;
      color: #999999;
    }
  }
  .comboBot {
    display: flex;
    .comboBotItem {
      width: 320rpx;
      height: 200rpx;
      background: red;
      display: flex;
      flex-direction: column;
      border-radius: 10rpx;
      justify-content: center;
      // .comboBotItemTit {
      //   margin-bottom: 19rpx;
      //   font-size: 30rpx;
      //   font-family: PingFang SC;
      //   font-weight: bold;
      //   color: #ffffff;
      //   line-height: 21rpx;
      // }
      // .comboBotItemDec {
      //   font-size: 24rpx;
      //   font-family: PingFang SC;
      //   font-weight: 400;
      //   color: #ffffff;
      //   line-height: 32rpx;
      //   width: 165rpx;
      // }
    }
    .comboBotItem:nth-child(2n) {
      margin-left: 16rpx;
    }
  }
}

.topCont {
  width: 690rpx;
  height: 210rpx;
  background: #ffffff;
  box-shadow: 0px 0px 15rpx 0px rgba(44, 49, 140, 0.11);
  border-radius: 10rpx;
  display: flex;
  align-items: center;
  justify-content: space-around;
  margin-top: 21rpx;
  .topCont-item {
    display: flex;
    flex-direction: column;
    align-items: center;
  }
}
.card_tab {
  width: 100%;
  height: 120rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  padding: 0rpx 28rpx 5rpx 28rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  // margin-top: 16rpx;
  .img_card {
    width: 80rpx;
    height: 80rpx;
    margin-right: 20rpx;
  }
  .card_content {
    width: 486rpx;
    height: 120rpx;
    padding: 20rpx 0;
    box-sizing: border-box;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: flex-start;
    .p1 {
      width: 100%;
      height: 60rpx;
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
.p2 {
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
page {
  background: rgba(255, 255, 255, 1);
  width: 100vh;
  box-sizing: border-box;
  overflow-x: hidden;
}
.service {
  position: fixed;
  right: 30rpx;
  bottom: 204rpx;
  z-index: 9999;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 90rpx;
  height: 90rpx;
  background: rgba(255, 255, 255, 1);
  box-shadow: 0rpx 6rpx 20rpx 0rpx rgba(184, 184, 184, 0.55);
  border-radius: 50%;
  .serviceImg {
    width: 42rpx;
    height: 34rpx;
  }
  .serviceWord {
    font-size: 18rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(33, 33, 33, 1);
  }
}
.gridWrap {
  box-shadow: 0px 5rpx 16rpx 0px rgba(204, 204, 204, 0.5);
  border-radius: 10rpx;
  margin-top: 20rpx;
  .grid {
    display: flex;
    align-items: center;
    .gridLeft {
      width: 113rpx;
      display: flex;
      justify-content: center;
      .gridLeftImg {
        left: 50%;
        transform: tranlateX(-50%);
        width: 66rpx;
        height: 66rpx;
      }
    }

    .gridRight {
      width: 232rpx;
      .gridRightTop {
        font-size: 30rpx;
        font-family: Source Han Sans CN;
        font-weight: bold;
        color: rgba(51, 51, 51, 1);
        margin-bottom: 20rpx;
      }
      .gridRightBot {
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(166, 165, 174, 1);
      }
    }
  }
}
.activeTitle {
  display: flex;
  align-items: center;
  width: 100%;
  height: 90rpx;
  margin: 30rpx 0 30rpx 0;
  justify-content: center;
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
}
.activeList {
  width: 750rpx;
  min-height: 150rpx;
  display: flex;
  align-items: center;
  margin: 40rpx 0 0 0;
}
.body {
  width: 690rpx;
  margin: 20rpx auto 0 auto;
  box-sizing: border-box;
  overflow-x: hidden;
  .car {
    width: 100%;
    background: #ffffff;
    box-shadow: 0px 0px 15rpx 0rpx rgba(44, 49, 140, 0.11);
    border-radius: 10rpx;
    margin-bottom: 21rpx;
  }
  .car1 {
    display: flex;
    align-items: center;
    height: 89rpx;
    justify-content: center;

    font-size: 30rpx;
    font-family: PingFang SC;
    font-weight: bold;
    color: #333333;
  }
  .car2 {
  }
}
.searchBar {
  width: 470rpx;
  height: 65rpx;
  background: rgba(248, 248, 248, 1);
  border-radius: 33rpx;
  display: flex;
  align-items: center;
  transform: translateY(-30%);
}
>>> .van-grid {
  border-radius: 20rpx;
}
</style>
