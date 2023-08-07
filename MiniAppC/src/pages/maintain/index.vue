<template>
  <div class="demo_page" :style="{minHeight:windowHeight1}">
    <div class="top_card">
      <div class="card_tab" @tap="changeCard" v-if="carArrFirst != null">
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
      <div class="card_add" @tap="changeCard" v-else>请选择车辆</div>
      <div class="card_store" @tap="changeStore" v-if="storeArr.simpleName != ''">
        <img :src="storeArr.img" alt class="img_card" mode="aspectFill" />
        <div class="card_content">
          <p class="p1_p1">{{storeArr.simpleName}}</p>
          <p class="p2_p2">{{storeArr.address}}</p>
        </div>
        <img src="https://m.aerp.com.cn/mini-RG-main/maintenance_jump_icon.png" alt style="width:24rpx;height:24rpx" />
      </div>
      <div class="store_add" @tap="changeStore" v-else>请选择服务门店</div>
    </div>

    <div class="box" v-for="(item1,index1) in boxarr" :key="index1">
      <!-- 常规保养 -->
      <p class="p_cgby">{{item1.categoryName}}</p>
      <div class="bottom_box" v-for="(itema,indexa) in item1.packageItems" :key="indexa">
        <!-- 小保养服务 -->
        <div class="byservice" @tap="serviceClick(itema,indexa,item1)">
          <div class="byservice_left">
            <img :src="itema.imageUrl" alt mode="aspectFit" />
            <div style="margin-left:10rpx;">
              <p style="font-size:30rpx;font-family:Source Han Sans CN;font-weight:bold;color:rgba(51,51,51,1);">{{itema.zhName}}</p>
              <p style="font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(166,165,174,1);margin-top:14rpx;">{{itema.briefDescription}}</p>
            </div>
          </div>
          <div class="byservice_right">
            <img :src="itema.isDefaultExpand?'https://m.aerp.com.cn/mini-RG-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-RG-main/maintenance_radio.png'" alt mode="aspectFit" />
          </div>
        </div>
        <!-- 爱车推荐  -->
        <div class="loveCard" style="font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(51,51,51,1);" v-if="itema.isDefaultExpand">
          <text class="recommend">根据您的爱车推荐</text>
        </div>
        <!-- 一个模块 -->
        <div v-if="itema.items.length > 0 && itema.isDefaultExpand">
          <div v-for="(itemaa1,indexaa1) in itema.items">
            <div :class="['set_meal',{'set_meal_border':itemb.isDefaultSelect}]" v-for="(itemb,indexb) in itemaa1.products" :key="indexb" v-if="itemaa1.products.length>0">
              <!-- <div @tap="selectCkick(itemb, index1, indexa)"> -->
              <div :class="['set_background',{'set_background1':itemb.isDefaultSelect}]">
                <img :src="itemb.image" alt class="set_meal_shopimg" @tap="commodityClcik(itemb,indexb)" mode="aspectFit" />
                <div>
                  <div @tap="commodityClcik(itemb,indexb)" style="width:480rpx;display:flex;flex-direction:column;">
                    <div>
                      <p class="p_one">
                        <text style="font-size:28rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(51,51,51,1);">{{itemb.displayName}}</text>
                        <!-- <text
                  style="font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(255,99,36,1);"
                        >{{itemb.isPackageProduct?itema.suggestTip:''}}</text>-->

                        <!-- itemb.price+'/份' 源数据 -->
                      </p>
                      <p class="p_two">
                        <text>{{itemb.description}}</text>
                      </p>
                    </div>
                  </div>
                  <p class="p_four">
                    <text style="font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(255,99,36,1);">￥{{itemb.price}}/份</text>
                    <!-- 步进器 -->
                    <van-stepper v-model="itemb.count" @change="countChange" :data-index="indexb" :data-item="itemb" :data-indexa="indexa" :data-indexaa1="indexaa1" :data-index1="index1" />
                  </p>
                </div>
                <div class="byservice_right" @tap="selectCkick(itemb, indexa,boxarr ,indexb)" style="position:absolute;right:-10rpx;top: 15rpx">
                  <img :src="itemb.isDefaultSelect?'https://m.aerp.com.cn/mini-RG-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-RG-main/maintenance_radio.png'" alt mode="aspectFit" />
                </div>
              </div>
              <!-- 套餐 -->
              <div v-if="itemb.isPackageProduct">
                <div class="set_meal_shop" v-for="(item,index) in itemb.childProducts" :key="index">
                  <img :src="item.image" alt class="set_meal_shopimg" mode="aspectFit" />
                  <div class="set_meal_shopbox">
                    <p class="set_meal_shoptop">{{item.displayName}}</p>
                    <div :class="['set_meal_shopbottom',{'biaoqian':item.tags === null?true:false}]">
                      <p v-if="item.tags.length == 0" style="width:87rpx;height:30rpx;line-height:30rpx;text-align:center;font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;"></p>
                      <div style="display:flex;justify-content:center;">
                        <p v-for="(itemtag,indextag) in item.tags" :key="indextag" :style="{color:itemtag.tagColor,borderColor:itemtag.tagColor,}" style="width:87rpx;height:30rpx;line-height:30rpx;text-align:center;font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;border-width:1rpx;border-style:solid;margin-right: 10rpx;border-radius: 6rpx;">{{itemtag.tag}}</p>
                      </div>
                      <!-- <p
                      v-for="(itemtag,indextag) in item.tags"
                      :key="indextag"
                      style="width:87rpx;height:30rpx;line-height:30rpx;text-align:center;border:1rpx solid rgba(92,198,130,1);font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(92,198,130,1);"
                      >{{itemtag.tag}}</p>-->
                      <p style="font-size:28rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(255,99,36,1);">
                        <!-- 价格部分 -->
                        <!-- ￥{{item.price}} -->
                        <text class="span_num">x{{item.count}}</text>
                      </p>
                    </div>
                  </div>
                </div>
              </div>
              <!-- 收起展开部分 -->
              <div class="p_three" v-if="itemb.childProducts.length>0">
                <p @tap="openClick(itemb,indexb)">
                  <text>{{itemb.isPackageProduct?'收起全部商品':'展开全部商品'}}</text>
                  <img :src="itemb.isPackageProduct?'https://m.aerp.com.cn/mini-RG-main/maintenance_up_icon.png':'https://m.aerp.com.cn/mini-RG-main/maintenance_down_icon.png'" alt mode="aspectFit" />
                </p>
                <!-- 加入购物车图片  与  关注商品图片  -->
                <!-- <div v-if="itemb.isPackageProduct">
                <img
                  style="margin-left:80rpx;width:44rpx;height:44rpx;"
                  src="https://m.aerp.com.cn/mini-RG-main/maintenance_collection_click.png"
                  alt
                />
              </div>
              <div v-if="itemb.isPackageProduct">
                <img
                  style="margin-left:30rpx;width:44rpx;height:44rpx;"
                  src="https://m.aerp.com.cn/mini-RG-main/maintenance_cart_icon.png"
                  alt
                />
                </div>-->
              </div>
            </div>
            <!-- <div
            class="showallshop"
            v-if="itema.items.length > 0"
            @tap="showallshop(itema,index1,indexa)"
            >{{itema.items[0].products[0].isShowAllPackage?'收起全部套餐':'展开全部套餐'}}</div>-->
            <div class="showallshop" v-if="itemaa1.products.length " @tap="showallshop(itema,index1,indexa,indexaa1)">{{itema.isShowAllPackage?'收起全部套餐':'展开全部套餐'}}</div>
          </div>
        </div>
        <!-- 暂无合适商品推荐 -->
        <div v-if="itema.isDefaultExpand && itema.items.length == 0" class="else_img_one">
          <img src="https://m.aerp.com.cn/mini-RG-main/maintenance_null_img.png" alt />
          <text>暂无合适商品推荐</text>
        </div>
      </div>
    </div>
    <!-- 底部按钮 -->
    <div class="bottom" style="z-index:30">
      <img src="https://m.aerp.com.cn/mini-RG-main/maintenance_tel_icon.png" alt class="tel" @tap="telClick" />
      <!-- <button open-type="contact" session-from="weapp" show-message-card send-message-path="/pages/detailsPages/main" send-message-title="【正品授权】美孚/Mobil 美孚1号全合成机油 5W-30SN级（4L装）" send-message-img="https://m.aerp.com.cn/mini-RG-main/goodsdetail_banner_pic.png">客服</button> -->
      <p style="min-width:220rpx;">
        合计：
        <span class="orange1">￥{{money}}</span>
      </p>

      <div class="orange" @tap="downUp">
        明细
        <img src="https://m.aerp.com.cn/mini-RG-main/downRed.png" alt class="down" v-if="downShow" />
        <img src="https://m.aerp.com.cn/mini-RG-main/upRed.png" alt class="down" v-else />
      </div>

      <text class="btn" @tap="sureJiesuan">确定</text>
    </div>
    <van-popup :show="moneyShow " closeable position="bottom" custom-style="height: 50%" @close="onClose" z-index="20">
      <div class="van-top">
        <h3 class="van-title">金额优惠明细</h3>
        <h3 class="van-title1">*实际优惠金额请以下单页为准</h3>
      </div>
      <div class="top-content">
        <h3 class="content-name">服务总价</h3>
        <h3 class="content-value">￥{{allMoney}}</h3>
      </div>
      <div class="top-content" v-if="serviceFee!=''">
        <h3 class="content-name">服务费</h3>
        <h3 class="content-value">+¥{{serviceFee}}</h3>
      </div>
      <div class="top-content" v-if="diamondsDiscountAmount!=''">
        <h3 class="content-name">促销</h3>
        <h3 class="content-value">-¥{{diamondsDiscountAmount}}</h3>
      </div>
      <div class="top-content">
        <h3 class="content-name">
          优惠券优惠
          <!-- <span style="color:#FF6424;font-size:24rpx;" @tap="couponClick">点击查看优惠券</span> -->
        </h3>
        <h3 class="content-value1">-¥{{concession}}</h3>
      </div>
    </van-popup>
  </div>
</template>
<script>
import {
  ByyhHome,
  postallshop,
  PostNearbyStore,
  PostOrderMoney,
  PostFirstOrder
} from '../../api'

import eventBus from '../../utils/eventBus.js'
let productInfos = []
let productAll = []
let pids = []
export default {
  data() {
    return {
      Arrproducts: [],
      Arrbox: [],
      isshowAllMeal: true,
      isproductAll: false,
      orderType: '',
      count: 1,
      isShow: false,
      downShow: true,
      moneyShow: false,
      boxarr: [], // 全页数组
      pid: [],
      windowHeight1: '',
      storeArr: {
        simpleName: '', // 门店名称
        address: '', // 门第地址
        img: '' // 门店图片
      }, // 门店信息
      storeid: '', // 门店id
      money: '0.00',
      longitude: '', // 经度
      latitude: '', // 纬度
      products: [], // 展开全部商品数组
      shopPid: '', // 商品的pid 订单页面刚加载需要的参数
      shopNum: '', // 商品数量  订单页面刚加载需要的参数
      a: 1,
      allApplicableCoupons: [], // 优惠券数组
      services: [], // 服务数组
      servicePrice: '0.00', // 服务费
      concession: '0.00', // 优惠
      allMoney: '0.00', // 服务总价
      serviceFee: '0.00', // 服务费
      diamondsDiscountAmount: '0.00' // 促销
    }
  },
  computed: {
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
        that.windowHeight1 = clientHeight * ratio + 'rpx'
        // console.log(`that.windowHeight:${that.windowHeight}`)
      }
    })
    that.globalData.serviceType = 'toShop'
    console.log('globalData.serviceType', that.globalData.serviceType)
    that.globalData.OrderType = 2
    that.orderType = 2
    console.log('globalData.OrderType', that.globalData.OrderType)
    this.globalData.ProductType = 0
    console.log('globalData.ProductType', that.globalData.ProductType)

    // 活动商品pid
    console.log(222, this.$route.query.pid)
    if (this.$route.query.pid) {
      this.pid = [this.$route.query.pid]
    }
  },
  mounted() {
    this.getPageDetail()
  },
  methods: {
    // 客服事件
    telClick() {
      wx.showModal({
        content: '021-55558888',
        confirmText: '呼叫',
        confirmColor: '#FF6324',
        success(res) {
          if (res.confirm) {
            wx.makePhoneCall({
              phoneNumber: '021-55558888'
            })
          } else if (res.cancel) {
          }
        }
      })
    },
    // 金额明细显示事件
    downUp() {
      this.downShow = !this.downShow
      this.moneyShow = !this.moneyShow
    },
    onClose() {
      this.moneyShow = false
    },
    // 判断商品是否选中
    selectCkick(itemb, indexa, itemall, indexb) {
      // this.boxarr[index1].packageItems[indexa].items[0].products.forEach(
      //   element => {
      //     element.isDefaultSelect = false
      //   }
      // )

      // if (itemb.isDefaultSelect == false) {
      //   itemb.count = 1
      // }
      // this.$forceUpdate()
      console.log('itema2', itemall)
      productInfos = []
      pids = []
      itemb.isDefaultSelect = !itemb.isDefaultSelect
      console.log(itemall)
      itemall.forEach((itema1, indexa1) => {
        itema1.packageItems.forEach((itembb, indexbb) => {
          if (itembb.isDefaultExpand == true) {
            itembb.items.forEach((itembb1, indexbb1) => {
              itembb1.products.forEach((itemb, indexb) => {
                console.log(itemb, 'itemb')
                if (itemb.isDefaultSelect) {
                  let product1 = {}
                  product1.vest = itembb.zhName
                  product1.pid = itemb.pid
                  product1.number = itemb.count
                  product1.index = indexbb
                  productInfos.push(product1)
                  pids.push(itemb.pid)
                }
              })
            })
          }
        })
        // else {
        //   productInfos = this.delArrVal(productInfos, indexa)
        // }
      })
      // productInfos = this.delArrVal(productInfos, indexa)
      // let product1 = {}
      // product1.pid = itemb.pid
      // product1.number = itemb.count
      // product1.index = indexb

      // productInfos.push(product1)
      if (productInfos.length > 0) {
        this.getMoney()
      } else {
        this.money = '0.00'
        this.serviceFee = '0.00'
        this.diamondsDiscountAmount = '0.00'

        this.allMoney = '0.00'
        this.concession = '0.00'
      }
    },
    getMaintainPackage(shopId, productId) {
      productInfos = []
      pids = []
      let that = this
      let request = {
        vehicle: {
          vehicleId: that.carArrFirst.vehicleId,
          paiLiang: that.carArrFirst.paiLiang,
          nian: that.carArrFirst.nian,
          tid: that.carArrFirst.tid,
          properties: that.carArrFirst.carProperties[0],
          distance: that.carArrFirst.totalMileage,
          onRoadTime:
            that.carArrFirst.buyYear + '-' + that.carArrFirst.buyMonth,
          tireSize: that.carArrFirst.tireSizeForSingle
        },
        baoYangType: [],
        productId: productId,
        provinceId: that.$store.state.curCityInfo.provinceId, // 省id
        cityId: that.$store.state.curCityInfo.cityId, // 市id
        shopId
      }
      // 保养养护
      ByyhHome(request)
        .then(res => {
          if (res.message) {
            // let that = this
            // wx.showModal({
            //   title: '提示',
            //   content: res.message,
            //   cancelText: '返回',
            //   confirmText: '查看',
            //   showCancel: true,
            //   success (res) {
            //     if (res.confirm) {
            //       that.pid = []
            //       that.getMaintainPackage(that.storeid, that.pid)
            //     } else if (res.cancel) {
            //       wx.navigateBack({})
            //     }
            //   },
            //   fail () {}
            // })
          } else {
            let listarr = res.data
            this.$forceUpdate()
            listarr.forEach(item => {
              item.packageItems.forEach(resa => {
                // resa.isShowAllPackage = false //增加字段

                if (resa.items.length > 0) {
                  resa.items.forEach(resb => {
                    if (resb.products.length > 0) {
                      if (resb.products[0].childProducts.length > 0) {
                        resb.products[0].childProducts.forEach(resc => {
                          resc.price = resc.price.toFixed(2)
                        })
                      }
                    }
                  })
                }
              })
            })
            that.boxarr = listarr
            console.log('boxarr', that.boxarr)
            // that.boxarr.forEach(item => {

            // })

            that.boxarr.forEach(item => {
              item.packageItems.forEach((resa, index) => {
                resa.items.forEach((ress, indexs) => {
                  ress.products.forEach((resy, indexy) => {
                    // resy.isDefaultSelect = false

                    resy.isPackageProduct = false
                    // ress[0].isDefaultSelect = true
                  })

                  // ress[0].isPackageProduct = false
                })
                if (resa.isDefaultExpand) {
                  if (resa.items.length > 0) {
                    resa.items.forEach(resb => {
                      resb.products.forEach(resc => {
                        if (resc.isDefaultSelect) {
                          let product1 = {}
                          product1.pid = resc.pid
                          product1.vest = resa.zhName
                          product1.number = resc.count
                          product1.index = index
                          productInfos.push(product1)
                          pids.push(itemb.pid)
                        }
                      })
                    })
                  }
                }
              })
            })
            // that.boxarr = that.boxarr

            console.log('boxarr', that.boxarr)
            if (productInfos.length === 0) {
              this.money = '0.00'
              return false
            }
            that.getMoney()
          }
        })
        .catch(err => {
          console.log(`保养养护失败函数,${err}`)
        })
    },
    // 获取页面详情
    getPageDetail() {
      let that = this
      if (this.$store.state.curCityInfo.city) {
        if (this.$store.state.carArr[0]) {
          if (this.$store.state.globalShop) {
            that.storeArr = this.$store.state.globalShop
            that.storeid = this.$store.state.globalShop.id
            that.getMaintainPackage(that.storeid, that.pid)
          } else {
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

                PostNearbyStore(rquestshop)
                  .then(res => {
                    console.log(
                      `获取附近门店列表成功函数,${JSON.stringify(res)}`
                    )
                    that.storeArr.address = res.data.items[0].address
                    that.storeArr.simpleName = res.data.items[0].simpleName
                    that.storeArr.img = res.data.items[0].img
                    that.storeid = res.data.items[0].id
                    that.getMaintainPackage(that.storeid, that.pid)
                  })
                  .catch(err => {
                    console.log(`获取附近门店列表失败函数,${err}`)
                  })
              },
              // 定位失败回调
              fail: function() {
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
                    that.storeArr.address = res.data.items[0].address
                    that.storeArr.simpleName = res.data.items[0].simpleName
                    that.storeArr.img = res.data.items[0].img
                    that.storeid = res.data.items[0].id
                    that.getMaintainPackage(that.storeid, that.pid)
                  })
                  .catch(err => {
                    console.log(`获取附近门店列表失败函数,${err}`)
                  })
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
                wx.navigateBack({})
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
    },
    openClick(itemb, indexb) {
      itemb.isPackageProduct = !itemb.isPackageProduct
      console.log(itemb.isPackageProduct)
    },
    // 优惠券点击事件
    couponClick() {
      console.log(`优惠券`)
      // this.$router.push('/pages/coupon/main')
      this.$router.push({
        path: '/pages/coupon/main',
        query: {
          isOrder: 'yes',
          userCouponId: JSON.stringify(this.allApplicableCoupons)
        }
      })
    },
    delArrVal(arr, val) {
      // 查找数组中的某个值并全部删除    第一个参数是查找的数组 第二个参数是删除的值
      for (let i = 0; i < arr.length; i++) {
        if (arr[i].index == val) {
          arr.splice(i, 1)
          i--
        }
      }
      return arr
    },
    // 商品数量
    countChange(e) {
      console.log(`商品的值`, e)
      var index = e.currentTarget.dataset.index // 获取当前点击事件的下标索引
      console.log(index, 'index')

      // var item1 = e.currentTarget.dataset.item1
      var item = e.currentTarget.dataset.item
      var indexa = e.currentTarget.dataset.indexa
      var index1 = e.currentTarget.dataset.index1

      var indexaa1 = e.currentTarget.dataset.indexaa1
      console.log('index1', index1)
      console.log('this.boxarr[index1]', this.boxarr[index1])
      console.log(
        this.boxarr[index1].packageItems[indexa].items[indexaa1].products[index]
          .count
      )
      // itema.items[0].products,indexb
      var itema = e.currentTarget.dataset.itema

      console.log(this.boxarr, 'All')
      this.boxarr[index1].packageItems[indexa].items[indexaa1].products[
        index
      ].count =
        e.mp.detail
      console.log('item', item)
      // console.log('productAll', productAll)
      // console.log('count', this.boxarr[index1].items[indexaa1].products)
      // item1[index].count = e.mp.detail
      this.selectCkick(item, indexa, this.boxarr, index)
      this.$forceUpdate()
    },
    // 商品详情页
    commodityClcik(item, index) {
      let that = this
      // 增加判断条件，看商品是否可以进入商品详情页
      // if(){

      // }else{

      // }
      this.$router.push({
        path: '/pages/detailsPages/main',
        query: {
          provinceId: that.$store.state.curCityInfo.provinceId,
          // shopPid: item.pid,
          cityId: that.$store.state.curCityInfo.cityId,
          productCode: item.pid,
          storeArr: JSON.stringify(that.storeArr),
          isFromSearch: 'istrue',
          carId: that.cardID,
          shopId: that.storeid
        }
      })
    },

    getMoney() {
      console.log(1111)
      let that = this
      let footValue = {
        carId: that.cardID,
        shopId: that.storeid,
        productInfos: productInfos,
        produceType: 0
      }
      PostFirstOrder(footValue)
        .then(res => {
          console.log(`试算金额`, res)
          that.money = res.data.actualAmount.toFixed(2)
          that.allMoney = res.data.totalProductAmount.toFixed(2)
          that.totalDoorToDoorFee = res.data.totalDoorToDoorFee.toFixed(2)
          that.serviceFee = res.data.serviceFee.toFixed(2)
          that.diamondsDiscountAmount = res.data.diamondsDiscountAmount.toFixed(
            2
          )

          that.allApplicableCoupons = res.data.allApplicableCoupons
          that.services = res.data.services // 服务数组
          that.concession = res.data.totalCouponAmount.toFixed(2)
          that.servicePrice = res.data.serviceFee.toFixed(2)
          // var sum = 0
          // that.services.forEach(ele => {
          //   sum += ele.number * ele.price
          // })
          // that.servicePrice = sum.toFixed(2)
          // that.money = res.data.totalProductAmount.toFixed(2)
        })
        .catch(err => {
          console.log(`试算金额错误信息${err}`)
        })
    },
    // 小保养服务
    serviceClick(itema, indexa, item1) {
      itema.isDefaultExpand = !itema.isDefaultExpand
      console.log(221, item1.packageItems)
      console.log(223, itema.groupName)
      console.log('indexa', indexa)
      if (itema.packageType == 'xby') {
        console.log(1)
        for (var i = 0; i < item1.packageItems.length; i++) {
          if (item1.packageItems[i].packageType == 'dby') {
            console.log(2)
            item1.packageItems[i].isDefaultExpand = false

            this.$forceUpdate()
          }
        }
      } else if (itema.packageType == 'dby') {
        console.log(3)
        for (var i = 0; i < item1.packageItems.length; i++) {
          // console.log(288, item1.packageItems[i].groupName)
          if (item1.packageItems[i].packageType == 'xby') {
            console.log(4)
            item1.packageItems[i].isDefaultExpand = false
            //  productInfos = this.delArrVal(productInfos, indexa)
            this.$forceUpdate()
          }
        }
      }
      if (itema.isDefaultExpand) {
        // itema.items[0].products.forEach((itemaa, indexaa) => {
        //   itemaa.isDefaultSelect = false
        // })
        if (itema.items.length > 0 && itema.items[0].products.length > 0) {
          let product = itema.items[0].products.find(ele => {
            return (ele.isDefaultSelect = true)
          })
        }
        // this.boxarr[showIndex1].packageItems
      }
      productInfos = []
      pids = []
      this.boxarr.forEach((itema1, indexa1) => {
        itema1.packageItems.forEach((itembb, indexbb) => {
          if (itembb.isDefaultExpand == true) {
            itembb.items.forEach((itembb1, indexbb1) => {
              itembb1.products.forEach((itemb, indexb) => {
                console.log(itemb, 'itemb')
                if (itemb.isDefaultSelect) {
                  let product1 = {}
                  product1.vest = itembb.zhName
                  product1.pid = itemb.pid
                  product1.number = itemb.count
                  product1.index = indexbb
                  productInfos.push(product1)
                  pids.push(itemb.pid)  
                }
              })
            })
          }
        })
        // else {
        //   productInfos = this.delArrVal(productInfos, indexa)
        // }
      })
      if (productInfos.length === 0) {
        this.money = '0.00'
        return false
      }
      this.getMoney()
    },
    // 测试加函数
    add(a) {
      let b = a + Number(this.money)
      this.money = b.toFixed(2)
      return this.money
    },
    // 测试减函数
    reduce(a) {
      let b = Number(this.money) - a
      this.money = b.toFixed(2)
      return this.money
    },
    toCity() {
      this.$router.push('/pages/city/main')
    },
    // 换车
    changeCard() {
      this.$router.push('/pages/myCard/main')
    },
    // 换店
    changeStore() {
      this.$router.push('/pages/stores/main')
    },
    // 结算跳转至订单确认页面
    sureJiesuan() {
      console.log(productInfos)
      if (this.cardtitle == '') {
        wx.showToast({ title: '请选择车辆', icon: 'none' })
      } else if (this.storeArr.simpleName == '') {
        wx.showToast({ title: '请选择服务门店', icon: 'none' })
      } else if (productInfos.length == 0) {
        wx.showToast({ title: '请选择商品', icon: 'none' })
      } else {
        if (
          productInfos.find(item => {
            return item.vest == '添加机油'
          }) &&
          !productInfos.find(item => {
            return item.vest == '基础保养服务'
          })
        ) {
          wx.showModal({
            title: '提示',
            content: '购买机油需和基础保养服务一起购买'
          })
        } else {
          this.$router.push({
            path: '/pages/orderSure/main',
            query: {
              cardID: this.cardID,
              storeid: this.storeid,
              productInfos: JSON.stringify(productInfos),
              pids: JSON.stringify(pids),
              orderType: 2,
              userCouponId: this.userCouponId,
              storeArr: JSON.stringify(this.storeArr)
            }
          })
        }
      }
    },
    couponMoney(userCouponId) {
      // PostOrderMoney
      let data = {
        orderType: 2,
        carId: this.cardID,
        shopId: this.storeid,
        productInfos: productInfos,
        payment: 1,
        deliveryType: 1,
        userCouponId
      }
      PostOrderMoney(data)
        .then(res => {
          this.money = res.data.actualAmount.toFixed(2)
        })
        .catch(err => {})
    },
    // 展开全部套餐封装方法 1323
    showAllMeal(
      showPid,
      showCount,
      showPackageType,
      showBaoYangType,
      showIndex1,
      showIndexa,
      indexaa1,
      itema
    ) {
      // if (itema.isDefaultExpand == false) {
      let pidArr = [
        {
          pid: showPid,
          count: showCount
        }
      ]
      let that = this
      let request = {
        vehicle: {
          vehicleId: that.carArrFirst.vehicleId,
          paiLiang: that.carArrFirst.paiLiang,
          nian: that.carArrFirst.nian,
          tid: that.carArrFirst.tid,
          properties: that.carArrFirst.carProperties[0],
          distance: that.carArrFirst.totalMileage,
          onRoadTime:
            that.carArrFirst.buyYear + '-' + that.carArrFirst.buyMonth,
          tireSize: that.carArrFirst.tireSizeForSingle
        },
        pidCount: pidArr,
        packageType: showPackageType,
        baoYangType: showBaoYangType,
        provinceId: that.$store.state.curCityInfo.provinceId,
        cityId: that.$store.state.curCityInfo.cityId,
        shopId: that.storeid
      }
      postallshop(request)
        .then(res => {
          let arr = res.data.products
          this.Arrproducts = arr
          // console.log(this.boxarr[showIndex1].packageItems[showIndexa].items[indexaa1], 'boxarr')
          arr.forEach(resc => {
            resc.price = resc.price.toFixed(2)
            resc.isPackageProduct = false
            // arr[0].count = this.boxarr[showIndex1].packageItems[showIndexa].items[indexaa1].products[0].count
          })
          this.Arrbox =
            that.boxarr[showIndex1].packageItems[showIndexa].items[
              indexaa1
            ].products
          this.getData(
            this.Arrbox,
            this.Arrproducts,
            showIndex1,
            showIndexa,
            indexaa1
          )
        })
        .catch(err => {
          console.log(`展开全部商品错误信息${err}`)
        })
      // }
      //  else {
      //   let that = this
      //   that.boxarr[showIndex1].packageItems[
      //     showIndexa
      //   ].items[indexaa1].products = that.boxarr[showIndex1].packageItems[
      //     showIndexa
      //   ].items[indexaa1].products
      // }
    },
    getData(Arrbox, Arrproducts, showIndex1, showIndexa, indexaa1) {
      // 两个数组去重合并
      let json = Arrbox.concat(Arrproducts)
      console.log(1, json)
      let newJson = []
      for (var i = 0; i < json.length; i++) {
        let flag = true
        for (var j = 0; j < newJson.length; j++) {
          if (newJson[j].pid == json[i].pid) {
            flag = false
            break
          }
        }
        if (flag) {
          // 判断是否重复
          newJson.push(json[i])
        }
      }
      console.log(12, newJson)
      Arrbox = newJson
      console.log(Arrbox, 'Arrbox')
      this.boxarr[showIndex1].packageItems[showIndexa].items[
        indexaa1
      ].products = Arrbox
      return Arrbox
    },

    // 展开全部套餐
    showallshop(itema, index1, indexa, indexaa1) {
      if (this.boxarr[index1].packageItems[indexa].isShowAllPackage) {
        // itema.isDefaultExpand = false
        this.boxarr[index1].packageItems[indexa].items[indexaa1].products.sort(
          (a, b) => b.isDefaultSelect - a.isDefaultSelect
        )
        let productArr = this.boxarr[index1].packageItems[indexa].items[
          indexaa1
        ].products
        this.boxarr[index1].packageItems[indexa].items[
          indexaa1
        ].products = this.boxarr[index1].packageItems[indexa].items[
          indexaa1
        ].products.filter(item => item.isDefaultSelect == true)
        this.boxarr[index1].packageItems[indexa].items[
          indexaa1
        ].products.forEach(element => {
          element.isPackageProduct = false
        })

        this.boxarr[index1].packageItems[indexa].isShowAllPackage = false
        var defaultSelectShow = this.boxarr[index1].packageItems[indexa].items[
          indexaa1
        ].products.some((item, index) => {
          console.log(item, 'item')
          return item.isDefaultSelect === true
        })
        console.log(12345)
        console.log(
          121,
          this.boxarr[index1].packageItems[indexa].items[
            indexaa1
          ].products.some((item, index) => {
            console.log(item, 'item')
            return item.isDefaultSelect === true
          })
        )
        console.log(defaultSelectShow, 'defaultSelectShow')
        if (defaultSelectShow == true) {
          itema.isDefaultExpand = true
        } else {
          itema.isDefaultExpand = false
          // this.boxarr[index1].packageItems[indexa].isShowAllPackage = true
          console.log('productArr[0]', productArr[0])
          this.boxarr[index1].packageItems[indexa].items[
            indexaa1
          ].products.push(productArr[0])
        }
        // this.isshowAllMeal = false
      } else {
        this.showAllMeal(
          itema.items[indexaa1].products[0].pid,
          itema.items[indexaa1].products[0].count,
          itema.packageType,
          itema.items[indexaa1].baoYangType,
          index1,
          indexa,
          indexaa1,
          itema
        )
        this.boxarr[index1].packageItems[indexa].isShowAllPackage = true
        // this.boxarr.forEach(item => {
        //   item.packageItems.forEach((resa, index) => {
        //     resa.items.forEach((ress, indexs) => {
        //       ress.products.forEach((resy, indexy) => {
        //         resy.isPackageProduct = false
        //       })
        //     })
        //   })
        // })
      }

      // this.showAllMeal(
      //   itema.items[0].products[0].pid,
      //   itema.items[0].products[0].count,
      //   itema.packageType,
      //   itema.items[0].baoYangType,
      //   index1,
      //   indexa
      // )
    }
  },

  onShow() {
    let that = this
    eventBus.$on('selectCoupon', item => {
      // that.userCouponDisplayName = item.displayName\
      console.log('selectCoupon', item)
      that.concession = item.value
      that.userCouponId = item.userCouponId
      that.couponMoney(that.userCouponId)
    })
    eventBus.$on('isonShow', res => {
      console.log(res)
      this.isShow = res
      if (this.isShow == true) {
        this.getPageDetail()
        productInfos = []
        this.money = '0.00'
        this.serviceFee = ' 0.00'
        this.allMoney = '0.00'
        this.concession = '0.00'
        this.diamondsDiscountAmount = ' 0.00'
      }
    })
  },

  onUnload: function() {
    eventBus.$off('selectCoupon')
    this.money = '0.00'
    this.serviceFee = ' 0.00'
    this.diamondsDiscountAmount = ' 0.00'
    this.allMoney = '0.00'
    this.concession = '0.00'
    productInfos = []
    this.boxarr = []
    this.pid = []
    this.isShow = false
    this.count = 1
    productAll = []
    this.isproductAll = false
    this.moneyShow = false
    this.globalData.isCarShow = false
  },
  onHide() {
    // this.isShow = true
    // this.money = '0.00'
    // productInfos = []
    // this.boxarr = []
    this.isShow = false
    this.moneyShow = false
    // this.isproductAll = false
  }
}
</script>
<style scoped lang="scss">
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  background: #f9f9f9;
  padding-bottom: 100rpx;
  box-sizing: border-box;
}
.top_card {
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
.bottom_box {
  width: 100%;
  padding: 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  background: rgba(255, 255, 255, 1);
  border-bottom: 2rpx solid #e8e8e8;
}
.p_cgby {
  width: 100%;
  padding-left: 30rpx;
  padding-right: 30rpx;
  line-height: 70rpx;
  box-sizing: border-box;
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
}
.byservice {
  width: 100%;
  height: 140rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  padding-right: 20rpx;
  box-sizing: border-box;
}
.byservice_left {
  flex: 2;
  display: flex;
  flex-direction: row;
  align-items: center;
}
.byservice_left img {
  width: 49rpx;
  height: 56rpx;
}
.byservice_right {
  width: 66rpx;
  height: 66rpx;
  position: absolute;
  right: 22rpx;
  display: flex;
  justify-content: center;
  align-items: center;
}

.byservice_right img {
  width: 36rpx;
  height: 36rpx;
}
.else_img_one {
  width: 100%;
  height: 260rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  border: 1rpx solid #f3f3f3;
  img {
    width: 164rpx;
    height: 129rpx;
  }
  text {
    font-size: 28rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(136, 136, 136, 1);
  }
}
.loveCard {
  width: 100%;
  height: 42rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 18rpx;
}
.recommend {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}
.rlues {
  display: flex;
  flex-direction: row;
  align-items: center;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
}

.set_meal {
  width: 100%;
  /* height: 210rpx; */
  border: 2rpx solid rgba(233, 233, 233, 1);
  border-radius: 10rpx;
  display: flex;
  flex-direction: column;
  padding: 4rpx 4rpx 0rpx;
  box-sizing: border-box;
  margin-top: 16rpx;
  background: #fff;
}
.set_background {
  position: relative;
  padding: 30rpx;
  display: flex;
}
.set_background1 {
  background: #ffeee7;
  position: relative;
  padding: 30rpx;
  display: flex;
}
.p_one {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.p_two {
  width: 100%;
}
.p_two text {
  width: 100%;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(166, 165, 174, 1);
  margin-top: 20rpx;
}
.p_three {
  width: 100%;
  display: flex;
  flex-direction: row;
  align-items: center;
  // text-align: center;
  padding: 0 0 20rpx 40rpx;
  // padding-left: 40rpx;
  box-sizing: border-box;
  margin-top: 20rpx;
  p {
    width: 400rpx;
    height: 100%;
    padding-right: 10rpx;
    box-sizing: border-box;
    text-align: right;
    text {
      // width: 100rpx;
      // text-align: center;
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
    img {
      width: 24rpx;
      height: 24rpx;
    }
  }
}
.p_three img {
  width: 24rpx;
  height: 24rpx;
}
.p_four {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  margin-top: 10rpx;
  align-items: center;
}
.set_meal_border {
  border: 1px solid rgba(255, 99, 36, 1);
  // background: rgba(255, 251, 250, 1);
}
.set_meal_border1 {
  border: 1px solid rgba(255, 99, 36, 1);
  background: rgba(255, 251, 250, 1);
}
.set_meal_shop {
  width: 100%;
  height: 170rpx;
  // background: rgba(252, 243, 241, 1);
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  // 样式
  // padding: 0 30rpx;
  padding: 30rpx;
  box-sizing: border-box;
  margin-top: 27rpx;
}
.set_meal_shopimg {
  width: 100rpx;
  height: 100rpx;
  margin-right: 15rpx;
}
.set_meal_shopbox {
  width: 490rpx;
  height: 120rpx;
  margin-left: 20rpx;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
}
.set_meal_shoptop {
  height: 76rpx;
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
}
.set_meal_shopbottom {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.biaoqian {
  justify-content: flex-end;
  // align-items: flex-end;
}
.span_num {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
  margin-left: 5rpx;
}
.bottom {
  // 底部按钮定位
  position: fixed;
  bottom: 0;
  left: 0;
  width: 100%;
  height: 98rpx;
  background: rgba(255, 255, 255, 1);
  // margin-top: 48rpx;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  padding-left: 30rpx;
  padding-right: 20rpx;
  box-sizing: border-box;
}
.tel {
  width: 42rpx;
  height: 42rpx;
  margin-right: 20rpx;
}
.orange1 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
  margin-right: 30rpx;
}
.orange {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
  width: 70px;

  display: flex;
  align-items: center;
}
.bottom .btn {
  width: 240rpx;
  height: 75rpx;
  background: rgba(255, 99, 36, 1);
  border-radius: 38rpx;
  font-size: 32rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 255, 255, 1);
  text-align: center;
  line-height: 75rpx;
}
.showallshop {
  width: 100%;
  text-align: center;
  height: 90rpx;
  line-height: 90rpx;
  font-size: 24rpx;
  font-family: SourceHanSansCN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}
.down {
  width: 32rpx;
  height: 32rpx;
  margin-right: 10rpx;
}
.van-top {
  text-align: center;
  margin-top: 20rpx;
}
.van-title {
  color: #000000;
  font: 32rpx/2 'SourceHanSansCN-Medium';
}
.van-title1 {
  color: #9a9a9a;
  font: 25rpx/1 'SourceHanSansCN-Regular';
}
.top-content {
  display: flex;
  justify-content: space-between;
  padding: 30rpx;
}
.content-name {
  color: #343434;
  font: 28rpx/1 'SourceHanSansCN-Regular';
}
.content-value {
  color: #343434;
  font: 600 28rpx/1 'SourceHanSansCN-Medium';
}
.content-value1 {
  color: #ff6424;
  font: 600 28rpx/1 'SourceHanSansCN-Medium';
}
>>> .van-stepper__minus {
  border-radius: 50%;
  background: #fff;
  color: #adadad;
  border: 2rpx solid rgba(237, 237, 237, 1);
  box-shadow: 0 6rpx 16rpx 0 rgba(211, 211, 211, 0.6);
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
  background: rgba(255, 99, 36, 0.9);
  box-shadow: 0px 6rpx 16rpx 0rpx rgba(255, 99, 36, 0.6);
  color: #fff;
}
</style>