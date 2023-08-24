<template>
  <div class="demo_page" :style="{minHeight:windowHeight1}">
    <div class="top_card">
      <!-- 车信息 -->
      <div class="card_tab" v-if="carArrFirst.vehicle != ''">
        <img :src="carArrFirst.brandUrl" alt class="img_card" />
        <div class="card_content">
          <p class="p1">{{carArrFirst.vehicle}}</p>
          <p class="p2">{{carArrFirst.carNumber}}</p>
        </div>
        <p class="change" @tap="changeCard">
          <img
            src="https://m.aerp.com.cn/mini-app-main/change.png"
            alt
            style="width:46rpx;height:46rpx;margin-bottom:16rpx;"
          />
          <span>换车</span>
        </p>
      </div>
      <div class="card_add" @tap="changeCard" v-else>请选择车辆</div>
      <!-- 预约时间 -->
    </div>
    <!-- 轮胎服务 -->
    <div class="content" v-if="defaultTireSize">
      <!-- <div class="content"> -->
      <div class="box1" @tap="guigeClick()">
        {{defaultTireSize}}
        <p class="rlues1">
          更换规格
          <img
            src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png"
            alt
            style="width:24rpx;height:24rpx;margin-left:15rpx;"
          />
        </p>
      </div>
    </div>
    <!-- <div class="box">

      <div class="bottom_box">
    
      

        <div
          class="loveCard"
          style="font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(51,51,51,1);"
          v-if="isDefaultExpand"
    >-->
    <!-- <p class="rlues" v-if="item1.categoryType === 'ReTire'" @tap="guigeClick(index1,item1)">
            更换规格
            <img
              src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png"
              alt
              style="width:24rpx;height:24rpx;margin-left:15rpx;"
            />
    </p>-->
    <div v-if="pickerVariable" class="pickerclass">
      <van-picker :columns="tireSizes" show-toolbar @cancel="onCancel" @confirm="onConfirm" />
    </div>
    <!-- </div> -->
    <!-- <div
          class="ltgg"
          v-if="item1.categoryType === 'ReTire' && item1.isDefaultExpand"
        >{{defaultTireSize}}</div>
        <div v-if="item1.products.length > 0 && item1.isDefaultExpand">
     
          <div
            :class="[{'border_style': item2.isDefaultSelect?true:false} , 'top_title']"
            v-for="(item2,index2) in item1.products"
            :key="index2"
            @tap="item2Click(item1,index1,item2,index2)"
          >
            <div class="top_img">
              <img :src="item2.image" alt class="img1" />
            </div>
            <div class="top_content">
              <p class="p1">{{item2.displayName}}</p>
              <div class="p2">
                <p class="p3">
                  <text class="txt1">￥{{item2.price}}</text>
                  <text
                    class="txt2"
                    :style="{'color':item3.tagColor,'border-color':item3.tagColor}"
                    v-for="(item3,index3) in item2.tags"
                    :key="index3"
                  >{{item3.tag}}</text>
                </p>
                <div class="add">
                  <div class="left_foot" @tap.stop="reduceClick(index1,item2,item1)">-</div>
                  <div class="center_foot">{{item2.count}}</div>
                  <div class="right_foot" @tap.stop="addValueClick(index1,item2,item1)">+</div>
                </div>
              </div>
            </div>
          </div>
          <p
            class="showShop"
            v-if="item1.categoryType === 'ReTire'"
            @tap="showshopall(item1,index1)"
          >显示全部商品</p>
    </div>-->
    <!-- 无商品 -->
    <!-- <div v-if="item1.isDefaultExpand && item1.products.length === 0" class="else_img_one">
          <img src="https://m.aerp.com.cn/mini-app-main/maintenance_null_img.png" alt />
          <text>暂无合适商品推荐</text>
    </div>-->
    <!-- </div>
    </div>-->
    <div class="box">
      <!-- 常规保养 -->
      <!-- <p class="p_cgby">{{item1.categoryName}}</p> -->
      <div class="bottom_box">
        <!-- 爱车推荐  -->
        <div
          class="loveCard"
          style="font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(51,51,51,1);"
        >
          <!-- <text class="recommend">根据您的爱车推荐</text> -->
        </div>
        <!-- 一个模块 -->
        <div v-if="products.length > 0 ">
          <div v-for="(itemb,indexb) in products" :key="indexb">
            <div :class="['set_meal',{'set_meal_border':itemb.isDefaultSelect}]">
              <div>
                <div class="p_one">
                  <div @tap="commodityClcik(itemb,indexb)" class="p_one1">
                    <text
                      style="font-size:28rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(51,51,51,1);max-width: 430rpx;"
                    >{{itemb.displayName}}</text>
                  </div>
                  <div class="p_right">
                    <text
                      style="font-size:32rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(255,99,36,1);margin-right:10rpx;"
                    >￥{{itemb.price}}/份</text>
                    <img
                      class="byservice_right"
                      @tap="serviceClick(itemb,indexb)"
                      :src="itemb.isDefaultSelect?'https://m.aerp.com.cn/mini-app-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-app-main/maintenance_radio.png'"
                      alt
                    />
                  </div>
                  <!-- <text
                  style="font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(255,99,36,1);"
                  >{{itemb.isPackageProduct?itema.suggestTip:''}}</text>-->
                  <!-- itemb.price+'/份' 源数据 -->
                </div>
                <p class="p_two" @tap="commodityClcik(itemb,indexb)">
                  <text>{{itemb.description}}</text>
                </p>
                <div
                  class="p_two"
                  @tap="commodityClcik(itemb,indexb)"
                  style=" margin-bottom: 30rpx;"
                >
                  <p
                    v-for="(itemtag,indextag) in itemb.tags"
                    :key="indextag"
                    :style="{color:itemtag.tagColor,borderColor:itemtag.tagColor,}"
                    style="padding: 0 10rpx;margin-right:10rpx;height:30rpx;line-height:30rpx;text-align:center;font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;border-width:1rpx;border-style:solid;border-radius: 6rpx;"
                  >{{itemtag.tag}}</p>
                </div>
                <p class="p_four">
                  <!-- 步进器 -->
                  <van-stepper v-model="itemb.count" @change="countChange" :data-index="indexb" />
                </p>
              </div>

              <!-- 套餐 -->
              <div v-if="itemb.isPackageProduct" style="background:#f6f6f6;margin-top:10rpx;">
                <div class="set_meal_shop" v-for="(item,index) in itemb.childProducts" :key="index">
                  <img :src="item.image" alt class="set_meal_shopimg" />
                  <div class="set_meal_shopbox">
                    <p class="set_meal_shoptop">{{item.displayName}}</p>
                    <div
                      :class="['set_meal_shopbottom',{'biaoqian':item.tags === null?true:false}]"
                    >
                      <p
                        v-if="item.tags.length == 0"
                        style="width:87rpx;height:30rpx;line-height:30rpx;text-align:center;font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;"
                      ></p>
                      <div style="display:flex;justify-content:center;">
                        <p
                          v-for="(itemtag,indextag) in item.tags"
                          :key="indextag"
                          :style="{color:itemtag.tagColor,borderColor:itemtag.tagColor,}"
                          style="width:87rpx;height:30rpx;line-height:30rpx;text-align:center;font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;border-width:1rpx;border-style:solid;margin-right: 10rpx;border-radius: 6rpx;"
                        >{{itemtag.tag}}</p>
                      </div>
                      <p
                        style="font-size:28rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(255,99,36,1);"
                      >
                        <!-- 价格部分 -->
                        ￥{{item.price}}
                        <text class="span_num">x{{item.count}}</text>
                      </p>
                    </div>
                  </div>
                </div>
              </div>
              <!-- 收起展开部分 -->
              <div class="p_three" v-if="itemb.childProducts.length>0">
                <p @tap="openClick(itemb,indexb)">
                  <text>{{itemb.isPackageProduct?'收起':"服务明细"}}</text>
                  <img
                    :src="itemb.isPackageProduct?'https://m.aerp.com.cn/mini-app-main/maintenance_up_icon.png':'https://m.aerp.com.cn/mini-app-main/maintenance_down_icon.png'"
                    alt
                  />
                </p>
              </div>
            </div>
          </div>
        </div>
        <!-- 暂无合适商品推荐 -->
        <div v-if="products.length ==0" class="else_img_one">
          <img src="https://m.aerp.com.cn/mini-app-main/maintenance_null_img.png" alt />
          <text>暂无合适商品推荐</text>
        </div>
      </div>
    </div>
    <div style="margin-bottom:176rpx;"></div>
    <div class="bottom1">
      <van-button type="primary" size="large" round color="#FF6324" @tap="sure">下一步</van-button>
    </div>
  </div>
</template>
<script>
import { GetProductListByServiceType } from '../../api'
let pidCount = []
let pids = []
export default {
  data() {
    return {
      isShow: false,
      CategoryId: '', // 列表id
      shopid: '', // 门店ID
      shopAllMOney: '', // 商品总价
      simpleName: '', // 门店名称
      shopaddress: '', // 门店地址
      imgs: [], // 门店图片
      isDefaultExpand: true,
      orderType: '',
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
      products: [], // 显示全部商品数组
      storeid: '', // 门店id
      shopPid: '', // 商品的pid 订单页面刚加载需要的参数
      shopNum: '', // 商品数量  订单页面刚加载需要的参数
      a: 0, // 点击按钮所需变量
      index1Active: '', // 点击更换规格拿到的索引
      item1Active: '', // 点击更换规格拿到的索引
      categoryType: '', // 点击更换规格拿到的categoryType
      getMealIndex: '', // 点击套餐时获取到的索引
      pageIndexActive: '' // 新增 点击套餐时的索引
    }
  },
  computed: {
    carArrFirst() {
      return this.$store.state.carArr[0]
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
    countChange(e) {
      console.log(`商品的值`, e)
      var index = e.currentTarget.dataset.index // 获取当前点击事件的下标索引
      console.log(index)
      // var itemb = e.currentTarget.dataset.item
      // itemb.count = e.mp.detail
      this.products[index].count = e.mp.detail

      console.log('count', this.products[index].count)
    },
    // 下一步
    sure() {
      pidCount = []
      pids = []
      // let totalPrice = 0
      this.products.forEach((itemb, indexb) => {
        if (itemb.isDefaultSelect) {
          // totalPrice += parseFloat((itemb.price * itemb.count).toFixed(2))
          // this.shopAllMOney = totalPrice
          let product1 = {}
          product1.pid = itemb.pid
          product1.number = itemb.count
          product1.price = itemb.price
          product1.index = indexb
          product1.displayName = itemb.displayName
          product1.image = itemb.image
          product1.childProducts = itemb.childProducts
          product1.ProductOwnAttri = itemb.productType
          product1.activityId = itemb.activityId
          pidCount.push(product1)
          pids.push(itemb.pid)
          console.log('productInfos', pidCount)
        }
      })
      if (this.cardtitle == '') {
        wx.showToast({ title: '请选择车辆', icon: 'none' })
      } else if (pidCount.length == 0) {
        wx.showToast({ title: '请选择商品', icon: 'none' })
      } else {
        this.$router.push({
          path: '/pages/orderSure/main',
          query: {
            cardID: this.cardID,

            // storeid: this.storeid,
            storeid: this.shopid,
            // shopNum: this.shopNum,
            simpleName: this.simpleName,
            shopaddress: this.shopaddress,
            // shopAllMOney: this.shopAllMOney,
            imgs: this.imgs,
            orderType: this.orderType,
            pids:JSON.stringify(pids),
            productInfos: JSON.stringify(pidCount),
            storeArr: JSON.stringify(this.storeArr)
          }
        })
      }
    },
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
    // 服务明细
    openClick(itemb, indexb) {
      itemb.isPackageProduct = !itemb.isPackageProduct
    },
    // 套餐点击事件
    item2Click(item1, index1, item2, index2) {
      this.boxarr[index1].products.forEach(element => {
        element.isDefaultSelect = false
      })
      item2.isDefaultSelect = !item2.isDefaultSelect

      pidCount = this.delArrVal(pidCount, index1)
      let product1 = {}
      product1.pid = item2.pid
      product1.number = item2.count
      product1.index = index1
      pidCount.push(product1)
      pids.push(item2.pid)
      this.getMoney()
    },
    // 减
    reduceClick(index1, item2, item1) {
      this.boxarr[index1].products.forEach(element => {
        element.isDefaultSelect = false
      })
      item2.isDefaultSelect = !item2.isDefaultSelect
      if (item2.count > 2) {
        pidCount = this.delArrVal(pidCount, index1)
        item2.count = item2.count - 1
        let product1 = {}
        product1.pid = item2.pid
        product1.number = item2.count
        product1.index = index1
        pidCount.push(product1)
        pids.push(item2.pid)
        this.getMoney()
      } else {
        wx.showToast({ title: '不能在减少了', icon: 'none' })
      }
    },

    // // 优惠券点击事件
    // couponClick () {
    //   this.$router.push('/pages/coupon/main')
    // },
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
        .then(() => {
          that.getInforPage(false)
        })
    },
    onCancel() {
      this.pickerVariable = false
    },

    // 更换轮胎服务
    serviceClick(itemb, indexb) {
      itemb.isDefaultSelect = !itemb.isDefaultSelect
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
    guigeClick() {
      this.pickerVariable = true
      // this.index1Active = index1
      // this.item1Active = item1
      // this.categoryType = item1.categoryType
    },
    // 选择城市
    toCity() {
      this.$router.push('/pages/city/main')
    },
    // 换车
    changeCard() {
      this.$router.push('/pages/myCard/main')
      // let that = this
      // this.getInforPage()
    },
    // 换店
    changeStore() {
      this.$router.push('/pages/stores/main')
    },
    // 显示全部商品
    async showshopall(item1, index1) {
      this.categoryType = item1.categoryType
      this.index1Active = index1

      pidCount = this.delArrVal(pidCount, index1) // new add
      this.money = '0.00' // new add
      let product1 = {}
      product1.pid = item1.products[0].pid
      product1.number = item1.products[0].count
      product1.index = index1
      pidCount.push(product1)
      pids.push(product1.pid)
      await this.getAllShop(
        this.defaultTireSize,
        pidCount,
        this.index1Active,
        this.categoryType
      )
      this.getMoney() // new add
    },
    // 商品详情页
    commodityClcik(item, index) {
      // 增加判断条件，看商品是否可以进入商品详情页
      // if(){
      // }else{
      // }
      // this.$router.push({
      //   path: '/pages/goodsDetails/main',
      //   query: {
      //     id: 2,
      //     ProductCode: item.pid,
      //     // storeArr: JSON.stringify(that.storeArr),
      //     // isFromSearch: 'istrue'
      //     ShopId: this.shopid,
      //     telephone: this.telephone,
      //     simpleName: this.simpleName,
      //     shopaddress: this.shopaddress
      //   }
      // })
    },

    changeData() {
      this.getInforPage() // 最好是只写需要刷新的区域的代码，onload也可，效率低，有点low
    },
    getInforPage(isSetSize = true) {
      pidCount = []
      pids = []
      let that = this
      let request = {
        vehicle: {
          vehicleId: that.carArrFirst.vehicleId,
          paiLiang: that.carArrFirst.paiLiang,
          nian: that.carArrFirst.nian,
          tid: that.carArrFirst.tid,
          // properties: that.carArrFirst.carProperties[0],
          // distance: that.carArrFirst.totalMileage
          // onRoadTime:
          //   that.carArrFirst.buyYear + '-' + that.carArrFirst.buyMonth,
          tireSize: that.carArrFirst.tireSizeForSingle
        },
        shopId: that.shopid,
        categoryId: that.CategoryId
      }

      // 轮胎列表
      GetProductListByServiceType(request)
        .then(res => {
          let that = this
          let listarr = res.data
          // listarr.forEach(item => {
          //   item.products.forEach(element => { element.isPackageProduct = false })
          // })
          that.boxarr = listarr
          that.products = that.boxarr.products
          that.products.forEach(item => {
            item.isPackageProduct = false
          })
          if (isSetSize) {
            that.defaultTireSize = res.data.defaultTireSize
          }

          console.log('boxarr', that.boxarr)
          that.tireSizes = that.boxarr.tireSizes
          console.log('tireSizes', that.tireSizes)
        })
        .catch(err => {
          console.log(`保养养护失败函数,${err}`)
        })
    }
  },

  mounted() {
    let that = this
    if (this.$store.state.curCityInfo.city) {
      if (this.$store.state.carArr[0]) {
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
    that.getInforPage()
  },

  // 现在的bug为返回页面还要有内容，onUnload和onHide暂隐藏，需测试
  onUnload: function() {
    // this.boxarr = []
    // this.money = '0.00'
    // this.shopPid = ''
    // this.shopNum = ''
    // this.a = 0
    // this.activeIndex = -1
    // this.tireSizes = []
    // pidCount = []
    this.pickerVariable = false
    this.isShow = false
  },
  onHide() {
    //   this.boxarr = []
    //   this.money = '0.00'
    //   this.shopPid = ''
    //   this.shopNum = ''
    //   this.a = 0
    //   this.activeIndex = -1
    //   this.tireSizes = []
    //   pidCount = []
    this.isShow = false
  },
  onLoad(options) {
    console.log(options)
    this.imgs = options.imgs
    this.shopaddress = options.shopaddress
    this.shopid = options.shopid
    this.simpleName = options.simpleName
    this.CategoryId = options.CategoryId
    this.storeArr['address'] = this.shopaddress
    this.storeArr.simpleName = this.simpleName
    this.storeArr.img = this.imgs[0]
    if (this.CategoryId == 1) {
      wx.setNavigationBarTitle({
        title: '保养服务'
      })
    } else if (this.CategoryId == 2) {
      wx.setNavigationBarTitle({
        title: '轮胎安装'
      })
    } else if (this.CategoryId == 3) {
      wx.setNavigationBarTitle({
        title: '美容洗车'
      })
    } else if (this.CategoryId == 4) {
      wx.setNavigationBarTitle({
        title: '钣金维修'
      })
    } else if (this.CategoryId == 5) {
      wx.setNavigationBarTitle({
        title: '汽车改装'
      })
    } else if (this.CategoryId == 6) {
      wx.setNavigationBarTitle({
        title: '其他'
      })
    }
  },
  onShow() {
    this.getInforPage()
  }
}
</script>
<style>
page {
  background: rgba(243, 243, 243, 1);
}
</style>
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
  background: #f2f2f2 !important;
  padding-bottom: 100rpx;
  box-sizing: border-box;
}
/* 顶部 */

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

    .img_card {
      width: 100rpx;
      height: 100rpx;
    }
    .card_content {
      width: 466rpx;
      height: 125rpx;
      padding: 20rpx 0;
      box-sizing: border-box;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-start;
      border-right: 2rpx solid #eee;

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
      .p2 {
        width: 100%;
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
    }
  }
  .change {
    display: flex;
    flex-direction: column;
    align-items: center;
    color: #999;
    font: 25rpx/1.5 'SourceHanSansCN-Regular';
    justify-content: center;
    width: 80rpx;
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
    height: 180rpx;
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
        height: 83rpx;
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
  width: 158rpx;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  div {
    width: 50rpx;
    height: 50rpx;
    border: 2rpx solid #e8e8e8;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }
}

.bottom_box {
  width: 100%;
  padding: 30rpx;
  padding-top: 0;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
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
  width: 36rpx;
  height: 36rpx;
}
.byservice_right img {
  width: 100%;
  height: 100%;
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
.rlues1 {
  display: flex;
  flex-direction: row;
  align-items: center;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
  position: absolute;
  right: 10rpx;
  top: 0rpx;
}
.box1 {
  width: 100%;
  text-align: center;
  position: relative;
  color: #333;
  font: 28rpx/1 'SourceHanSansCN-Regular';
  padding: 0rpx 50rpx;
  height: 100rpx;
  line-height: 100rpx;
  background: #fff;
  box-sizing: border-box;
  margin-top: 15rpx;

  border-radius: 10rpx;
}
/* 一个套餐样式 */
.top_title {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  height: 170rpx;
  background: rgba(255, 251, 250, 1);
  border: 1rpx solid #e8e8ee;
  border-radius: 10rpx;
  margin-bottom: 16rpx;
  padding: 20rpx;
  box-sizing: border-box;
}
.border_style {
  border: 1rpx solid rgba(255, 99, 36, 1);
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
  width: 500rpx;
  height: 130rpx;
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
  height: 50rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
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
.orange {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
  margin-right: 35rpx;
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
.bottom1 {
  position: fixed;
  left: 0;
  right: 0;
  background: #fff;
  padding: 6rpx 45rpx 6rpx;

  bottom: 0;
  z-index: 20;
}
.content {
  padding: 30rpx;
  box-sizing: border-box;
}
.bottom_box {
  width: 100%;
  padding: 10rpx 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
}
.loveCard {
  width: 100%;

  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.set_meal {
  width: 100%;
  /* height: 210rpx; */
  border: 1rpx solid rgba(233, 233, 233, 1);
  border-radius: 10rpx;
  display: flex;
  flex-direction: column;
  padding: 30rpx;
  box-sizing: border-box;
  background: #fff;
  margin-top: 30rpx;
}
.p_one {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.p_two {
  margin-top: 10rpx;
  width: 100%;
  display: flex;
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
  padding-left: 50rpx;
  box-sizing: border-box;
  margin-top: 20rpx;
  p {
    width: 350rpx;
    height: 100%;
    padding-right: 40rpx;
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
.p_five {
  width: 100%;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: center;
}
.five_font {
  color: #999;
  font: 24rpx/3 'SourceHanSansCN-Regular';
  margin-right: 30rpx;
}
.p_four {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: flex-end;
}
.set_meal_border {
  border: 1px solid rgba(255, 99, 36, 1);
  background: rgba(255, 251, 250, 1);
}
.set_meal_border1 {
  border: 1px solid rgba(255, 99, 36, 1);
  background: rgba(255, 251, 250, 1);
}
.set_meal_shop {
  width: 100%;
  height: 170rpx;

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
  width: 110rpx;
  height: 110rpx;
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

.byservice_right {
  width: 36rpx;
  height: 36rpx;
}
.byservice_right img {
  width: 100%;
  height: 100%;
}
.span_num {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
  margin-left: 5rpx;
}
.p_one1 {
  width: 350rpx;
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: center;
}
.p_right {
  display: flex;

  justify-content: flex-start;
  align-items: center;
}
</style>
