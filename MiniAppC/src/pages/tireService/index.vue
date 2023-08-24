<template>
  <div class="demo_page" :style="{minHeight:windowHeight1}">
    <div class="top_card">
      <!-- 车信息 -->
      <div class="card_tab" @tap="changeCard" v-if="carArrFirst != null">
        <img :src="carArrFirst.brandUrl" alt class="img_card" />
        <div class="card_content">
          <p class="p1">{{carArrFirst.vehicle}}</p>
          <p class="p2">{{carArrFirst.carNumber}}</p>
        </div>
        <div style="display:flex;flex-direction:column;align-items:center;">
          <img src="https://m.aerp.com.cn/mini-app-main/changeCarIcon.png" alt style="width:60rpx;height:60rpx;margin-top:20rpx;" />
          <p class="p2">换车</p>
        </div>
      </div>
      <div class="card_add" @tap="changeCard" v-else>请先选择车辆</div>
      <!-- 门店信息 -->
      <div class="card_store" @tap="changeStore" v-if="storeArr.simpleName != ''">
        <img :src="storeArr.img" alt class="img_card" />
        <div class="card_content">
          <p class="p1_p1">{{storeArr.simpleName}}</p>
          <p class="p2_p2">{{storeArr.address}}</p>
        </div>
        <img src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png" alt style="width:24rpx;height:24rpx" />
      </div>
      <div class="store_add" @tap="changeStore" v-else>请选择服务门店</div>
    </div>
    <!-- 轮胎服务 -->
    <div class="box" v-for="(item1,index1) in boxarr" :key="index1">
      <div class="bottom_box">
        <!-- 四轮换位 -->
        <div class="byservice" @tap="serviceClick(item1,index1)">
          <div class="byservice_left">
            <img :src="item1.imageUrl" alt />
            <div style="margin-left:10rpx;">
              <p class="overHide" style="font-size:30rpx;font-weight:bold;color:rgba(51,51,51,1);">{{item1.zhName}}</p>
              <p class="overHide" style="font-size:24rpx;font-weight:400;color:rgba(166,165,174,1);">{{item1.briefDescription}}</p>
            </div>
          </div>
          <div class="byservice_right">
            <img :src="item1.isDefaultExpand?'https://m.aerp.com.cn/mini-app-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-app-main/maintenance_radio.png'" alt />
          </div>
        </div>
        <!-- 根据爱车推荐  更换规格 -->
        <div class="loveCard" style="font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(51,51,51,1);" v-if="item1.isDefaultExpand">
          <text class="recommend">根据您的爱车推荐</text>
          <p class="rlues" v-if="item1.categoryType === 'ReTire'" @tap="guigeClick(index1,item1)">
            更换规格
            <img src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png" alt style="width:24rpx;height:24rpx;margin-left:15rpx;" />
          </p>
          <div v-if="pickerVariable && item1.categoryType === 'ReTire'" class="pickerclass">
            <van-picker :columns="tireSizes" show-toolbar @cancel="onCancel" @confirm="onConfirm" />
          </div>
        </div>
        <div class="ltgg" v-if="item1.categoryType === 'ReTire' && item1.isDefaultExpand">{{defaultTireSize}}</div>
        <div v-if="item1.products.length > 0 && item1.isDefaultExpand">
          <!-- 一个套餐 -->
          <div :class="[{'border_style': item2.isDefaultSelect?true:false} , 'top_title']" v-for="(item2,index2) in item1.products" :key="index2">
            <div class="top_img" @tap="toDetail(item2)">
              <img :src="item2.image" alt class="img1" />
            </div>
            <div style="position:relative;display:flex;flex-direction:row;width:520rpx;">
              <div class="top_content" @tap="toDetail(item2)">
                <p class="p1">{{item2.displayName}}</p>
                <div class="p2">
                  <p class="p3">
                    <text class="txt1">￥{{item2.price}}</text>
                    <text class="txt2" :style="{'color':item3.tagColor,'border-color':item3.tagColor}" v-for="(item3,index3) in item2.tags" :key="index3">{{item3.tag}}</text>
                  </p>
                </div>
              </div>

              <div class="add">
                <!-- <van-stepper :value="item2.count" name="index1" @change="countClick" @tap="getValueClick(index1)"/> -->
                <!-- <div class="left_foot" @tap.stop="reduceClick(index1,item2,item1,index2)">-</div>
                  <div class="center_foot">{{item2.count}}</div>
                <div class="right_foot" @tap.stop="addValueClick(index1,item2,item1,index2)">+</div>-->
                <van-stepper v-model="item2.count" @change="countChange" :data-index2="index2" :data-item2="item2" :data-index1="index1" :data-item1="item1" />
              </div>
              <div class="selectedImg" @tap="item2Click(item1,index1,item2,index2)">
                <img :src="item2.isDefaultSelect?'https://m.aerp.com.cn/mini-app-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-app-main/maintenance_radio.png'" alt class="img" />
              </div>
            </div>
          </div>
          <p class="showShop" v-if="item1.categoryType === 'ReTire' && showMore == true" @tap="showshopall(item1,index1,index2)">显示全部商品</p>
        </div>
        <!-- 无商品 -->
        <div v-if="item1.isDefaultExpand && item1.products.length === 0" class="else_img_one">
          <img src="https://m.aerp.com.cn/mini-app-main/maintenance_null_img.png" alt />
          <text>暂无合适商品推荐</text>
        </div>
      </div>
    </div>
    <!-- 底部确定按钮 -->
    <div class="bottom" style="z-index:30">
      <img src="https://m.aerp.com.cn/mini-app-main/maintenance_tel_icon.png" alt class="tel" @tap="telClick" />
      <!-- <button open-type="contact" session-from="weapp" show-message-card send-message-path="/pages/detailsPages/main" send-message-title="【正品授权】美孚/Mobil 美孚1号全合成机油 5W-30SN级（4L装）" send-message-img="https://m.aerp.com.cn/mini-app-main/goodsdetail_banner_pic.png">客服</button> -->
      <p style="min-width:220rpx;">
        合计：
        <span class="orange1">￥{{money}}</span>
      </p>

      <div class="orange" @tap="downUp">
        明细
        <img src="https://m.aerp.com.cn/mini-app-main/downRed.png" alt class="down" v-if="downShow" />
        <img src="https://m.aerp.com.cn/mini-app-main/upRed.png" alt class="down" v-else />
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
  postTyre,
  postMoreshop,
  PostNearbyStore,
  PostOrderMoney,
  PostFirstOrder
} from '../../api'

import eventBus from '../../utils/eventBus.js'
let pidCount = []
let pids = []
export default {
  data() {
    return {
      isShow: false,
      downShow: true,
      moneyShow: false,
      firstProductNum: 1,
      showMore: true,
      windowHeight1: '',
      boxarr: [], // 整页数组
      pickerVariable: false, // 更换规格变量
      storeArr: {
        simpleName: '', // 门店名称
        address: '', // 门第地址
        img: '' // 门店图片
      }, // 门店信息
      money: '0.00',
      activeIndex: -1, // 选择轮胎的索引
      defaultTireSize: '', // 默认轮胎尺寸
      tireSizes: [], // 更换规格数组
      // products: [],// 显示全部商品数组
      storeid: '', // 门店id
      shopPid: '', // 商品的pid 订单页面刚加载需要的参数
      shopNum: '', // 商品数量  订单页面刚加载需要的参数
      a: 0, // 点击按钮所需变量
      index1Active: '', // 点击更换规格拿到的索引
      item1Active: '', // 点击更换规格拿到的索引
      categoryType: '', // 点击更换规格拿到的categoryType
      getMealIndex: '', // 点击套餐时获取到的索引
      pageIndexActive: '', // 新增 点击套餐时的索引
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
    // 跳转门店详情
    toDetail(item2) {
      console.log('item2', item2.pid)
      console.log('cardID', this.cardID)
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
          productCode: item2.pid,
          storeArr: JSON.stringify(that.storeArr),
          isFromSearch: 'istrue',
          carId: that.cardID,
          shopId: that.storeid
        }
      })
    },
    // 套餐点击事件
    item2Click(item1, index1, item2, index2) {
      // this.boxarr[index1].products.forEach(element => {
      //   element.isDefaultSelect = false
      // })
      item2.isDefaultSelect = !item2.isDefaultSelect
      console.log('this.boxarr', this.boxarr)
      pidCount = []
      pids = []
      // pidCount = this.delArrVal(pidCount, index1)
      this.boxarr.forEach((itema, indexa) => {
        if (itema.isDefaultExpand) {
          itema.products.forEach((itemaa, indexaa) => {
            if (itemaa.isDefaultSelect) {
              let product1 = {}
              product1.pid = itemaa.pid
              product1.number = itemaa.count
              product1.index = indexa
              pidCount.push(product1)
              pids.push(itemaa.pid)
            }
          })
        }
      })
      if (pidCount.length > 0) {
        this.getMoney()
      } else {
        this.money = '0.00'
        this.serviceFee = ' 0.00'
        this.allMoney = '0.00'
        this.concession = '0.00'
        this.diamondsDiscountAmount = '0.00'
      }
    },
    // 减
    reduceClick(index1, item2, item1, index2) {
      // this.boxarr[index1].products.forEach(element => {
      //   element.isDefaultSelect = false
      // })
      // item2.isDefaultSelect = !item2.isDefaultSelect
      if (this.boxarr[index1].products[index2].count > 1) {
        // pidCount = this.delArrVal(pidCount, index1)
        this.boxarr[index1].products[index2].count =
          this.boxarr[index1].products[index2].count - 1
        pidCount = []
        pids = []
        // pidCount = this.delArrVal(pidCount, index1)
        this.boxarr.forEach((itema, indexa) => {
          if (itema.isDefaultExpand) {
            itema.products.forEach((itemaa, indexaa) => {
              if (itemaa.isDefaultSelect) {
                let product1 = {}
                product1.pid = itemaa.pid
                product1.number = itemaa.count
                product1.index = indexa
                pidCount.push(product1)
                pids.push(itemaa.pid)
              }
            })
          }
        })
        this.getMoney()
      } else {
        wx.showToast({ title: '不能在减少了', icon: 'none' })
      }
    },
    countChange(e) {
      console.log(`商品的值`, e)
      var index = e.currentTarget.dataset.index // 获取当前点击事件的下标索引
      console.log(index, 'index')

      // var item1 = e.currentTarget.dataset.item1
      var item1 = e.currentTarget.dataset.item1
      var index2 = e.currentTarget.dataset.index2
      var index1 = e.currentTarget.dataset.index1
      var item2 = e.currentTarget.dataset.item2
      console.log()
      this.boxarr[index1].products[index2].count = e.mp.detail
      console.log(this.boxarr[index1].products[index2].count)
      pidCount = []
      pids = []
      // pidCount = this.delArrVal(pidCount, index1)
      this.boxarr.forEach((itema, indexa) => {
        if (itema.isDefaultExpand) {
          itema.products.forEach((itemaa, indexaa) => {
            if (itemaa.isDefaultSelect) {
              let product1 = {}
              product1.pid = itemaa.pid
              product1.number = itemaa.count
              product1.index = indexa
              pidCount.push(product1)
              pids.push(itemaa.pid)
            }
          })
        }
      })
      this.getMoney()
    },
    // 加
    addValueClick(index1, item2, item1, index2) {
      // this.boxarr[index1].products.forEach(element => {
      //   element.isDefaultSelect = false
      // })
      // item2.isDefaultSelect = !item2.isDefaultSelect
      // pidCount = this.delArrVal(pidCount, index1)
      this.boxarr[index1].products[index2].count =
        this.boxarr[index1].products[index2].count + 1
      // if (index2 == 0) {
      //   this.firstProductNum = item2.count
      // }

      pidCount = []
      pids = []
      // pidCount = this.delArrVal(pidCount, index1)
      this.boxarr.forEach((itema, indexa) => {
        if (itema.isDefaultExpand) {
          itema.products.forEach((itemaa, indexaa) => {
            if (itemaa.isDefaultSelect) {
              let product1 = {}
              product1.pid = itemaa.pid
              product1.number = itemaa.count
              product1.index = indexa
              pidCount.push(product1)
              pids.push(itemaa.pid)
            }
          })
        }
      })
      this.getMoney()
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
    // 轮胎规格选中事件
    async onConfirm(e) {
      let that = this

      this.defaultTireSize = e.mp.detail.value
      this.pickerVariable = false
      this.$store
        .dispatch('editUserCar', {
          carId: that.cardID,
          tireSizeForSingle: e.mp.detail.value
        })
        .then(() => {})
      this.getAllShop1(
        e.mp.detail.value,
        pidCount,
        this.index1Active,
        this.categoryType
      )
    },
    onCancel() {
      this.pickerVariable = false
    },
    // 试算金额接口
    getMoney() {
      let that = this
      let footValue = {
        carId: that.cardID,
        shopId: that.storeid,
        productInfos: pidCount,
        produceType: 0
      }
      PostFirstOrder(footValue)
        .then(res => {
          that.money = res.data.actualAmount.toFixed(2)
          that.allMoney = res.data.totalProductAmount.toFixed(2)
          that.totalDoorToDoorFee = res.data.totalDoorToDoorFee.toFixed(2)
          that.serviceFee = res.data.serviceFee.toFixed(2)
          that.diamondsDiscountAmount = res.data.diamondsDiscountAmount.toFixed(
            2
          )
          that.allApplicableCoupons = res.data.allApplicableCoupons
          that.services = res.data.services // 服务数组
          that.concession = res.data.totalCouponAmount.toFixed(2) // 优惠
          that.servicePrice = res.data.serviceFee.toFixed(2) // 服务费
        })
        .catch(err => {})
    },
    // 更换轮胎服务
    serviceClick(item1, index1) {
      item1.isDefaultExpand = !item1.isDefaultExpand
      if (this.boxarr[index1].products.length === 0) {
        return false
      }
      pidCount = []
      pids = []
      if (item1.isDefaultExpand) {
        let product = item1.products.find(ele => {
          return ele.isDefaultSelect === true
        })
        pidCount = []
      } else {
        // pidCount = this.delArrVal(pidCount, index1)
      }
      this.boxarr.forEach((itema, indexa) => {
        if (itema.isDefaultExpand) {
          itema.products.forEach((itemaa, indexaa) => {
            if (itemaa.isDefaultSelect) {
              let product1 = {}
              product1.pid = itemaa.pid
              product1.number = itemaa.count
              product1.index = indexa
              pidCount.push(product1)
              pids.push(itemaa.pid)
            }
          })
        }
      })
      if (pidCount.length === 0) {
        this.money = '0.00'
        return false
      }
      this.getMoney()
    },
    serviceClick1(index1) {
      let that = this
      that.$nextTick(function() {
        if (this.boxarr[index1].isDefaultExpand) {
          // pidCount = this.delArrVal(pidCount, index1) // new add
          this.money = '0.00' // new add

          // if (item1.isDefaultExpand) {
          //   let product = item1.products.find(ele => {
          //     return ele.isDefaultSelect === true
          //   })
          pidCount = []
          pids = []
          this.boxarr.forEach((itema, indexa) => {
            if (itema.isDefaultExpand) {
              itema.products.forEach((itemaa, indexaa) => {
                if (itemaa.isDefaultSelect) {
                  let product1 = {}
                  product1.pid = itemaa.pid
                  product1.number = itemaa.count
                  product1.index = indexa
                  pidCount.push(product1)
                  pids.push(itemaa.pid)
                }
              })
            }
          })
        }
        if (pidCount.length === 0) {
          this.money = '0.00'
          return false
        }
        this.getMoney()
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
    // 更换规格事件
    guigeClick(index1, item1) {
      this.pickerVariable = true
      this.index1Active = index1
      this.item1Active = item1
      this.categoryType = item1.categoryType
    },
    // 封装显示全部商品方法
    getAllShop(tiresize, pidCount, index1, categoryType, index2) {
      let that = this
      let request = {
        categoryType: categoryType,
        provinceId: that.$store.state.curCityInfo.provinceId,
        cityId: that.$store.state.curCityInfo.cityId,
        vehicleId: that.carArrFirst.vehicleId,
        pidCount: pidCount,
        tid: that.carArrFirst.tid,
        tireSize: tiresize
      }
      postMoreshop(request)
        .then(res => {
          res.data.items[0].count = this.boxarr[index1].products[0].count

          that.boxarr[index1].products = res.data.items
        })
        .catch(err => {})
    },
    getAllShop1(tiresize, pidCount, index1, categoryType) {
      let that = this
      let request = {
        categoryType: categoryType,
        provinceId: that.$store.state.curCityInfo.provinceId,
        cityId: that.$store.state.curCityInfo.cityId,
        vehicleId: that.carArrFirst.vehicleId,
        pidCount: pidCount,
        tid: that.carArrFirst.tid,
        tireSize: tiresize
      }
      postMoreshop(request)
        .then(res => {
          that.boxarr[index1].products = res.data.items
          that.boxarr[index1].products.forEach((itema, indexa) => {
            itema.isDefaultSelect = false
          })

          that.serviceClick1(index1)
        })
        .catch(err => {})
    },
    // 金额明细显示事件
    downUp() {
      this.downShow = !this.downShow
      this.moneyShow = !this.moneyShow
    },
    onClose() {
      this.moneyShow = false
    },
    // 换车
    changeCard() {
      this.$router.push('/pages/myCard/main')
    },
    // 换店
    changeStore() {
      this.$router.push('/pages/stores/main')
    },
    // 显示全部商品
    async showshopall(item1, index1, index2) {
      this.categoryType = item1.categoryType
      this.index1Active = index1
      this.showMore = false
      pidCount = this.delArrVal(pidCount, index1) // new add
      this.money = '0.00' // new add
      let product1 = {}
      product1.pid = item1.products[0].pid
      product1.number = item1.products[0].count
      product1.index = index1
      pidCount.push(product1)
      await this.getAllShop(
        this.defaultTireSize,
        pidCount,
        this.index1Active,
        this.categoryType,
        index2
      )
      this.getMoney() // new add
    },
    // 获取页面详情
    getPageDetail() {
      let that = this
      console.log(222)
      if (this.$store.state.curCityInfo.city) {
        if (this.$store.state.carArr[0]) {
          if (this.$store.state.globalShop) {
            that.storeArr = this.$store.state.globalShop
            that.storeid = this.$store.state.globalShop.id
            that.getInforPage(that.storeid)
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
                    that.getInforPage(that.storeid)
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
                    that.getInforPage(that.storeid)
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
    // 结算跳转至订单确认页面
    sureJiesuan() {
      if (this.carArrFirst.vehicle == '') {
        wx.showToast({ title: '请选择车辆', icon: 'none' })
      } else if (this.storeArr.simpleName == '') {
        wx.showToast({ title: '请选择服务门店', icon: 'none' })
      } else if (this.money == '0.00') {
        wx.showToast({ title: '请选择商品', icon: 'none' })
      } else {
        this.$router.push({
          path: '/pages/orderSure/main',
          query: {
            cardID: this.cardID,
            storeid: this.storeid,
            shopPid: this.shopPid,
            shopNum: this.shopNum,
            orderType: 1,
            productInfos: JSON.stringify(pidCount),
            storeArr: JSON.stringify(this.storeArr),
            pids:JSON.stringify(pids)
          }
        })
      }
    },
    // 轮胎页面刚加载的方法
    getInforPage(storeid) {
      console.log(111)
      let that = this
      let request = {
        provinceId: that.$store.state.curCityInfo.provinceId,
        cityId: that.$store.state.curCityInfo.cityId,
        vehicleId: that.carArrFirst.vehicleId,
        tid: that.carArrFirst.tid,
        tireSize: that.carArrFirst.tireSizeForSingle,
        storeid
      }
      postTyre(request)
        .then(res => {
          let listarr = res.data.tireCategory
          listarr.forEach(item => {
            item.products.forEach(res1 => {
              res1.price = res1.price.toFixed(2)
            })
          })
          that.boxarr = listarr
          that.defaultTireSize = res.data.defaultTireSize
          that.tireSizes = res.data.tireSizes
        })
        .catch(err => {})
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
      }
    })
    that.globalData.serviceType = 'toShop'
    that.globalData.OrderType = 1
    console.log('globalData.serviceType', that.globalData.serviceType)
    console.log('globalData.OrderType', that.globalData.OrderType)
    this.globalData.ProductType = 0
    console.log('globalData.ProductType', that.globalData.ProductType)
  },

  onShow() {
    eventBus.$on('isonShow', res => {
      console.log(res)
      this.isShow = res
    })
    if (this.isShow == true) {
      pidCount = []
      pids = []
      // this.mealarr = []
      this.$forceUpdate()
      this.activeIndex = -1

      // this.boxarr = []
      this.getPageDetail()

      this.money = '0.00'
      this.serviceFee = '0.00'
      this.allMoney = '0.00'
      this.concession = '0.00'
      this.diamondsDiscountAmount = '0.00'
    }
  },
  mounted() {
    this.getPageDetail()
  },
  // 现在的bug为返回页面还要有内容，onUnload和onHide暂隐藏，需测试
  onUnload() {
    this.globalData.isCarShow = false
    this.showMore = true
    this.boxarr = []
    this.money = '0.00'
    this.serviceFee = ' 0.00'
    this.allMoney = '0.00'
    this.concession = '0.00'
    this.diamondsDiscountAmount = '0.00'
    this.shopPid = ''
    this.shopNum = ''
    this.a = 0
    this.activeIndex = -1
    this.tireSizes = []
    pidCount = []
    pids = []
    this.isShow = false
    this.downShow = true
    this.moneyShow = false
  },
  onHide() {
    this.showMore = true
    // this.boxarr = []
    // this.money = '0.00'
    // this.shopPid = ''
    // this.shopNum = ''
    // this.a = 0
    // this.activeIndex = -1
    // this.tireSizes = []
    // pidCount = []
    this.isShow = false
    this.downShow = true
    this.moneyShow = false
  }
}
</script>
<style scoped lang="scss">
.overHide {
  width: 450rpx;
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 1;
  -webkit-box-orient: vertical;
  font-family: Source Han Sans CN;
}
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  background: #f9f9f9;
  padding-bottom: 100rpx;
  box-sizing: border-box;
}
/* 顶部 */
.box {
  margin-top: 16rpx;
}
.top_card {
  width: 100%;
  display: flex;
  flex-direction: column;
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

.add {
  width: 230rpx;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  position: absolute;
  right: -8rpx;
  bottom: -6rpx;

  div {
    width: 50rpx;
    height: 50rpx;
    // border: 2rpx solid #e8e8e8;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    background: #f2f3f5;
    border: 0;
    margin: 2rpx;
    padding: 1rpx;
    white-space: nowrap;
    font-family: 'UICTFontTextStyleBody';
    color: #323233;
  }
}

.bottom_box {
  width: 100%;
  padding: 30rpx;
  padding-top: 0;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  background: rgba(255, 255, 255, 1);
}
.byservice {
  width: 100%;
  height: 140rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.byservice_left {
  width: 550rpx;
  display: flex;
  flex-direction: row;
  align-items: center;
}
.byservice_left img {
  width: 66rpx;
  height: 66rpx;
}
.byservice_right {
  margin-right: 25rpx;
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

.loveCard {
  // position: relative;
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

/* 一个套餐样式 */
.top_title {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  min-height: 170rpx;
  background: #fff;
  border: 1rpx solid #e8e8ee;
  border-radius: 10rpx;
  margin-bottom: 16rpx;
  padding: 20rpx;
  box-sizing: border-box;
}
.border_style {
  border: 1rpx solid rgba(255, 99, 36, 1);
  background: #fffbfa;
}
.top_img {
  width: 130rpx;
  height: 130rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.top_img .img1 {
  width: 102rpx;
  height: 102rpx;
}
.top_content {
  width: 300rpx;

  min-height: 120rpx;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
}
.top_content .p1 {
  width: 100%;
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
.top_content .p2 {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  margin-top: 20rpx;
}
.p3 .txt1 {
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
}
.txt2 {
  /* width:120rpx; */
  padding: 0 10rpx;
  box-sizing: border-box;
  height: 30rpx;
  /* border:1rpx solid rgba(92,198,130,1); */
  border-width: 1rpx;
  border-style: solid;
  border-radius: 4rpx;
  margin-left: 15rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  /* color:rgba(92,198,130,1); */
  text-align: center;
}
.showShop {
  width: 100%;
  height: 90rpx;
  text-align: center;
  line-height: 90rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}

/* 底部按钮样式 */
.bottom {
  // 底部按钮定位
  position: fixed;
  bottom: 0;
  left: 0;
  z-index: 9999;
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

.ltgg {
  width: 690rpx;
  height: 170rpx;
  background: rgba(255, 255, 255, 1);
  border: 1rpx solid rgba(233, 233, 233, 1);
  border-radius: 10rpx;
  text-align: center;
  line-height: 168rpx;
  margin-bottom: 16rpx;
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
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

.pickerclass {
  width: 100%;
  position: fixed;
  bottom: 0;
  left: 0;
  z-index: 99999999;
  // border: 2rpx solid red;
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
.selectedImg {
  position: absolute;
  right: -10rpx;
  top: -5rpx;
  width: 66rpx;
  height: 66rpx;
  z-index: 1;
  display: flex;
  justify-content: center;
  align-items: center;
}
.selectedImg .img {
  width: 36rpx;
  height: 36rpx;
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
  // box-shadow:0px 6rpx 16rpx 0rpx rgba(255,99,36,0.68);
  color: #fff;
}
</style>