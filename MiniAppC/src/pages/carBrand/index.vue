<template>
  <div>
    <!-- <view class="searchInput">
      <view class="serView" style="padding-right: 30rpx">
        <input
          type="text"
          placeholder-style="color:rgba(191, 191, 191, 1);"
          confirm-type="search"
          placeholder="汽车品牌关键字"
          v-model="message"
          @focus="showMask"
        />
      </view>
      <view class="cancel" bindtap="hideMask">取消</view>
    </view> -->
    <form bindsubmit="formSubmit" report-submit>
      <!-- <scroll-view  class="scroll" :scroll-into-view="toView" :scroll-y="scrollY"> -->
      <scroll-view style="height:calc(100vh);" :scroll-into-view="toView" scroll-y scroll-top="0">
        <div>
          <view class="hotbrand fixed" id="top">{{scroll_name}}</view>
          <view class="hotlist fixed-wrapper">
            <view v-for="(item,index) in hots" :key="index" class="item" @click="bindCars(item)">
              <view class="car">
                <image class="car-image" :src="item.brandUrl" mode="aspectFill" lazy-load="true"></image>
                <view class="car-name">{{item.brandSuffix}}</view>
                <button class="getformbtn" form-type="submit"></button>
              </view>
            </view>
          </view>
        </div>

        <view class="eachchar-margin"></view>
        <view class="slider"> 
          <view class="eachchar" v-for="(item,index) in brands1" :key="index">
            <view class="title" :id="item.brandPrefix">{{item.brandPrefix}}</view>
            <view class="eachcar" v-for="(item1,idx) in item.brandList" :key="idx" style="background:#fff;">
              <view class="car-wrapper" @click="bindCars(item1)" style="border-bottom-width:1px">
                <image class="car-image" :src="item1.brandUrl" mode="aspectFill" lazy-load="true"></image>
                <view class="car-name">{{item1.brandSuffix}}</view>
                <button class="getformbtn" form-type="submit"></button>
              </view>
            </view>
            <view class="eachchar-margin"></view>
          </view>
        </view>
      </scroll-view>
      <view class="brandPos" :animation="transform">
        <view class="brand-name">
          <image class="brand-img" :src="selectCar.Url" mode="aspectFill" />
          <view class="brand-text">{{selectCar.BrandShow}}</view>
        </view>
        <scroll-view class="brand-box" scroll-y="true">
          <view class="brand-list" v-for="(item,index) in brandTypes" :key="index">
            <view class="brand-title">{{item.factory}}</view>
            <view class="brand-item-box">
              <view class="brand-item" @click="bindTire(item1)" v-for="(item1,idx) in item.vehicleList" :key="idx">{{item1.vehicleSeries}}
                <button class="getformbtn" form-type="submit"></button>
              </view>
            </view>
          </view>
        </scroll-view>
        <!--<view class="selectedbrand">
              <view wx:for="{{brandTypes}}" wx:key="{{index}}" data-index="{{index}}" bindtap="bindDetailType" class="title {{index === selectedType ? 'selected' : ''}}">{{item}}</view>
          </view>
          <view class="brandlist">
              <view class="type-title">{{brandTypes[selectedType]}}</view>
              <view class="scrollcar" data-item="{{item}}" bindtap="bindTire" wx:for="{{brandDatas[brandTypes[selectedType]]}}" wx:key="{{index}}">{{item.Vehicle}}</view>
          </view>-->
      </view>
      <view class="slider-nav">
        <view class="eachindex" @click="bindTop">#</view>
        <view class="eachindex" @click="bindChar(item)" v-for="(item,index) in brandKeys" :key='index'>
          {{item}}
          <button class="getformbtn" form-type="submit"></button>
        </view>
      </view>
      <!-- <view class="bigger-char" >00</view> -->
    </form>
  </div>
</template>

<script>
import { GetVehicleBrandList, GetVehicleListByBrand } from '../../api'
export default {
  data() {
    return {
      message: '1234',
      hots: [],
      brands: {},
      brands1: [],
      brandKeys: [],
      chars: [],
      toView: '',
      brandTypes: [],
      brandDatas: [],
      selectedType: 0,
      searchData: [],
      animationData: {},
      transform: {},
      scrollY: true,
      showNoResule: true,
      scroll_name: '热门品牌',
      maskPopup: true,
      maskName: 'black-bg',
      currentChar: '',
      selectCar: {},
      /**
       * 搜索框是否获取焦点，
       * 点击clear-icon时会使搜索框失去焦点，用来重新获取焦点
       */
      toFocus: false
    }
  },

  onShow() {
    // 页面初始化 options为页面跳转所带来的参数
    this.startX = 0
    this.startY = 0
    this.searchPost = {}
    this.getBrands()
    this.lock = false
  },
  methods: {
    getBrands() {
      GetVehicleBrandList().then(res => {
        this.hots = res.data[0].brandList
        const brandKeys = []
        res.data.forEach(item => {
          brandKeys.push(item.brandPrefix)
        })
        brandKeys.shift()

        this.brandKeys = brandKeys
        res.data.shift()
        this.brands1 = res.data
      })
    },
    getBrandShow(item) {
      return item.Brand.split(' ')[2]
    },
    getBrandUrl(item) {
      return `https://img${item.Brand.substr(0, 1).charCodeAt() % 4 +
        1}.tuhu.org${item.Url}`
    },
    // getBrands () {
    //   return wx.fetch({
    //     url: wx.apis.brand
    //   }).then((res) => {
    //     const brands = {}

    //     for (const name in res.data.Brand) {
    //       const item = res.data.Brand[name]
    //       const key = item.Brand.substr(0, 1)
    //       if (!brands[key]) {
    //         brands[key] = []
    //       }

    //       item.Url = item.ImageUrl
    //       item.BrandShow = this.getBrandShow(item)

    //       brands[key].push(item)
    //     }

    //     const hots = []
    //     let parentIndex = -1
    //     res.data.HotBrand.every((item, index) => {
    //       if (index % 5 === 0) {
    //         parentIndex++
    //         if (parentIndex > 1) {
    //           return false
    //         }
    //         hots[parentIndex] = []
    //       }

    //       item.Url = item.ImageUrl
    //       item.BrandShow = this.getBrandShow(item)
    //       hots[parentIndex].push(item)

    //       return true
    //     })

    //     const brandKeys = Object.keys(brands).sort()

    //     this.setData({
    //       hots,
    //       brands,
    //       brandKeys
    //     })
    //     // 计算高度，设置top值
    //     this.setCharsTop()
    //   })
    // },
    /**
     * 计算高度，设置top值
     */
    setCharsTop() {
      const { brands, brandKeys } = this.data
      // 可使用窗口宽度
      const { windowWidth } = wx.getSystemInfoSync()
      // rpx2px 比率
      const rate = windowWidth / 750

      // 以下部分高度根据实际情况自行调整，如样式改动，需要对应调整
      // 另为了便于理解下面的数字含义，请勿删除注释
      // -- 2018年8月28日 by wangyouwei

      // 间隙的高度
      // .eachchar-margin高10rpx
      const gap = 10 * rate

      // 热门品牌部分占据的高度
      // .fixed-wrapper上边距 60rpx
      // 两行.item
      // 第一个item padding-top: 25rpx padding-bottom: 30rpx
      // 第二个item padding-bottom: 26rpx
      // item内
      // .car-image 高60rpx
      // .car-name margin-top: 18rpx，没有设置高度,font-size: 24rpx
      const hotHeight = (60 + 25 + 30 + 26 + (60 + 18 + 24) * 2) * rate

      // ABCD...标题高度 50rpx
      const titleHeight = 50 * rate

      // 车型品牌高度 98rpx
      const brandHeight = 98 * rate

      let itemTop = 0
      const scrollArr = [
        {
          name: '热门品牌',
          top: itemTop
        }
      ]
      brandKeys.forEach((key, index) => {
        if (index === 0) {
          // 第一个字母的top值为热门品牌的高度加间隙
          itemTop += hotHeight + gap
        } else {
          // 之后的top值为上一个字母的top值加上一个字母列表的高度（标题加内容）加间隙
          const preKey = brandKeys[index - 1]
          itemTop += titleHeight + brands[preKey].length * brandHeight + gap
        }
        scrollArr.push({
          name: key,
          top: itemTop
        })
      })
      this.scroll_arr = scrollArr
    },
    bindChar(item) {
      // const animation = wx.createAnimation({
      //   duration: 300,
      //   timingFunction: 'linear'
      // })

      // animation.scale(3, 3).step()
      // animation.scale(1, 1).step()
      this.toView = item

      // this.animationData = {
      //   [item]: animation.export()
      // }
      // this.setData({
      //   toView: item,
      //   animationData: {
      //     [item]: animation.export()
      //   }
      // })
    },
    bindCharStart(ev) {
      if (!wx.tuhu.util.isIOS()) {
        return
      }
      const { item } = ev.currentTarget.dataset
      this.charStartY = ev.touches[0].pageY
      this.charIndex = ev.currentTarget.dataset.index
      this.changeIndex = 0 // 重置为0，避免未触发touchmove时，touchend设置错误

      this.setData({
        scrollY: false,
        currentChar: item
      })
    },
    bindCharMove(ev) {
      if (!wx.tuhu.util.isIOS()) {
        return
      }

      const thisY = ev.touches[0].pageY
      const height = wx.systemInfo.windowWidth / 375 * 24
      this.changeIndex = Math.round((thisY - this.charStartY) / height)

      this.setData({
        currentChar: this.data.brandKeys[this.charIndex + this.changeIndex]
      })
    },
    bindCharEnd(ev) {
      if (!wx.tuhu.util.isIOS()) {
        return
      }
      this.setData({
        scrollY: true
      })

      this.setData({
        toView: this.data.brandKeys[this.charIndex + this.changeIndex]
      })

      setTimeout(() => {
        this.setData({
          currentChar: ''
        })
      }, 100)
    },
    getCars(brand) {
      this.selectCar = {
        brand: brand.brand,
        BrandShow: brand.brandSuffix,
        Url: brand.brandUrl
      }
      GetVehicleListByBrand({ brand: brand.brand }).then(res => {
        this.brandTypes = res.data
      })
      // /* this.setData({
      //   carsName: brand.Brand
      // }) */
      //   return wx.fetch({
      //     url: `${wx.apis.cars}?Brand=${encodeURIComponent(brand.Brand)}`
      //   }).then((res) => {
      //     const brandTypes = []
      //     const brandDatas = {}

      //     res.data.OneBrand.forEach((item) => {
      //       if (brandTypes.indexOf(item.BrandType) < 0) {
      //         brandTypes.push(item.BrandType)
      //         brandDatas[item.BrandType] = []
      //       }

      //       [item.VehicleShow] = item.Vehicle.split('-')
      //       brandDatas[item.BrandType].push(item)
      //     })
      //     return this.setData({
      //       brandTypes,
      //       brandDatas,
      //       selectCar: {
      //         BrandShow: brand.BrandShow,
      //         Url: brand.Url
      //       },
      //       selectedType: 0
      //     })
      //   })
    },
    bindCars(item) {
      let scrollY = false
      const animation = wx.createAnimation({
        duration: 300,
        timingFunction: 'linear'
      })
      if (!this.scrollY) {
        animation.translateX('100%').step()
        scrollY = true
        wx.setNavigationBarTitle({
          title: '请选择品牌'
        })
      } else {
        animation.translateX(0).step()
        scrollY = false
        wx.setNavigationBarTitle({
          title: '请选择车型'
        })
        this.getCars(item)
      }

      this.transform = animation.export()
      this.scrollY = scrollY
    },
    bindDetailType(ev) {
      const { index } = ev.currentTarget.dataset

      this.setData({
        selectedType: index
      })
    },
    bindTire(item) {
      this.$router.replace({
        path: '/pages/carDisplacement/main',
        query: { ...this.selectCar, ...item }
      })
      // if (this.isAddingCar) return
      // this.isAddingCar = true
      // const { item } = ev.currentTarget.dataset
      // // const routerData = app.globalData.CAR_MODULE_DATA

      // if (routerData.jumpType === 2) {
      //   wx.getCarPresenter().addCar(item).then(() => {
      //     this.isAddingCar = false
      //     wx.getCarRouter(routerData).dispatchJump(item)
      //   })
      //   return
      // }
      // this.isAddingCar = false
      // wx.getCarRouter(routerData).dispatchJump(item)

      const animation = wx.createAnimation({
        duration: 300,
        timingFunction: 'linear'
      })

      animation.translateX('100%').step()

      wx.setNavigationBarTitle({
        title: '请选择品牌'
      })
      this.transform = animation.export()
      this.scrollY = true
    },
    touchstart(ev) {
      this.startX = ev.touches[0].pageX
      this.startY = ev.touches[0].pageY
    },
    touchmove(ev) {
      const changeX = ev.touches[0].pageX - this.startX
      const changeY = ev.touches[0].pageY - this.startY

      const arch = Math.atan(changeY / changeX) / Math.PI * 180
      if (changeX > 0 && Math.abs(arch) < 10) {
        const animation = wx.createAnimation({
          duration: 0,
          timingFunction: 'linear'
        })
        animation.translateX(changeX).step()
        this.setData({
          transform: animation.export()
        })
      }
    },
    touchend(ev) {
      const changeX = ev.changedTouches[0].pageX - this.startX
      const animation = wx.createAnimation({
        duration: 300,
        timingFunction: 'linear'
      })

      if (changeX > 100) {
        animation.translateX('100%').step()
        this.setData({
          transform: animation.export(),
          scrollY: true
        })
      } else {
        animation.translateX(0).step()
        this.setData({
          transform: animation.export()
        })
      }
    },
    bindTop() {
      this.scrollTop = 0
    },
    // onScroll (ev) {
    //   const { scrollTop } = ev.detail
    //   const arr = this.scroll_arr

    //   let this_index = 0

    //   arr.forEach((el, index) => {
    //     if (scrollTop >= el.top) {
    //       this_index = index
    //     }
    //   })

    //   if (arr[this_index] && arr[this_index].name !== this.data.scroll_name) {
    //     this.setData({
    //       scroll_name: arr[this_index].name
    //     })
    //   }
    // },
    showMask() {
      const showPopup = () => {
        this.maskPopup = false
        this.searchData = []
        this.maskName = 'black-bg'

        wx.setNavigationBarTitle({
          title: '请选择车型'
        })
      }
      if (!this.scrollY) {
        const animation = wx.createAnimation({
          duration: 300,
          timingFunction: 'linear'
        })
        animation.translateX('100%').step()

        this.transform = animation.export()
        this.scrollY = true

        setTimeout(() => {
          showPopup()
        }, 300)
        return
      }
      if (this.data.maskPopup || this.message === '') {
        showPopup()
      }
    },
    onInput(e) {
      const { value } = e.detail

      let maskName

      this.setData({
        searchValue: value,
        showNoResule: true
      })
      this.searchPost = {}
      if (value) {
        maskName = 'white-bg'
        this.getCarList(e)
      } else {
        maskName = 'black-bg'
        this.setData({
          searchData: []
        })
      }
      this.setData({
        maskPopup: false,
        maskName
      })
    },
    clearSearchValue() {
      this.setData({
        searchValue: '',
        inputValue: '',
        searchData: [],
        maskName: 'black-bg',
        toFocus: true // 点击清除图标会导致input失去焦点，这里设置重新获取焦点
      })
    },
    hideMask() {
      this.setData({
        maskPopup: true,
        inputValue: '',
        searchData: []
      })
      wx.setNavigationBarTitle({
        title: '请选择品牌'
      })
      this.searchValue = ''
    },
    onBlur(e) {
      const { value } = e.detail

      if (this.data.maskName === 'black-bg') {
        this.hideMask()
      } else if (value) {
        this.getCarList(e)
      }
      this.setData({
        searchValue: value
      })
    },
    getCarList(e = this.e) {
      const name = e.detail.value
      let post = {
        name,
        pageIndex: 1,
        pageSize: 20
      }
      if (this.lock) {
        return
      }

      this.lock = true

      if (this.searchPost.name === name) {
        if (!this.searchPost.end) {
          this.searchPost.pageIndex++
          post = this.searchPost
        } else {
          return
        }
      }
      post.name = post.name.replace(/(\s)+/g, '')
      wx
        .fetch({
          url: wx.apis.SelectLikeCat,
          data: post,
          noStatus: true
        })
        .then(res => {
          this.lock = false

          if (this.data.searchValue.length === 0) {
            this.setData({
              searchData: []
            })
            return
          }
          this.searchPost.end = res.data.Result.length < 20

          if (this.data.searchValue && this.searchPost.name !== name) {
            this.setData({
              searchData: res.data.Result,
              showNoResule: res.data.Result.length
            })
          } else {
            this.setData({
              searchData: this.data.searchData.concat(res.data.Result)
            })
          }

          this.e = e
          this.searchPost = post
        })
    },
    onLower() {
      if (!this.searchPost.end) {
        this.getCarList()
      }
    }
    // formSubmit (e) {
    //   app.formSubmit(e)
    // }
  }
}
</script>

<style  scoped lang="scss">
/* pages/car/car.wxss */
page {
  box-sizing: border-box;
  font-size: 28rpx;
}

page {
  height: 100%;
  background: #eee;
  font-size: 28rpx;
  position: relative;
}
.scroll {
  height: 100%;
  background: #fff;
}
.hotbrand {
  color: #666666;
  padding: 23rpx 0 13rpx 30rpx;
  font-size: 24rpx;
  border-bottom: 2rpx solid #d9d9d9;
  background: #f2f2f2;
  box-sizing: border-box;
  height: 40px;
}

.hotlist {
  padding-right: 36rpx;
  background: #fff;
  display: flex;
  flex-wrap: wrap;
}

.hotlist .item {
  display: table;
  width: 20%;
  text-align: center;
}

.hotlist .item .car {
  position: relative;
  display: table-cell;
  color: #333;
  width: 20%;
  padding: 26rpx;
}
.hotlist .item .car:active {
  background: #f8f8f8;
}

.car-image {
  width: 60rpx;
  height: 60rpx;
}

.hotlist .item .car .car-name {
  margin-top: 10rpx;
  color: #333;
  font-size: 30rpx;
}

.eachchar {
}
.car-wrapper .car-image {
  margin-top: -10rpx;
}

.slider .title {
  color: #666;
  font-size: 24rpx;
  height: 50rpx;
  border-bottom: 2rpx solid #d9d9d9;
  width: 100%;
  box-sizing: border-box;
  padding-left: 30rpx;
}

.scroll .slider .eachchar .eachcar:last-child .car-name {
  border: 0;
}
.scroll .slider .eachchar .eachcar:active {
  background: #f8f8f8;
}
.slider .car-wrapper {
  position: relative;
  height: 98rpx;
  padding: 30rpx 0 0rpx 30rpx;
  display: -webkit-box;
  display: -webkit-flex;
  display: flex;
  box-sizing: border-box;
  border-bottom: 1rpx solid #d9d9d9;
}

.slider .car-name {
  display: -webkit-box;
  display: -webkit-flex;
  display: flex;
  -webkit-box-align: center;
  -webkit-align-items: center;
  align-items: center;
  margin-left: 40rpx;
  flex: 1;
  padding-bottom: 30rpx;
}
.slider-nav {
  font-size: 24rpx;
  position: fixed;
  top: 96rpx;
  right: 0;
  bottom: 0;
  z-index: 999999;
  color: #999999;
  display: -webkit-box;
  display: -webkit-flex;
  display: flex;
  padding-left: 20rpx;
  -webkit-box-orient: vertical;
  -webkit-box-direction: normal;
  -webkit-flex-direction: column;
  flex-direction: column;
  -webkit-justify-content: space-around;
  justify-content: space-around;
}

.slider-nav .eachindex {
  position: relative;
  width: 65rpx;
  text-align: center;
  height: 50rpx;
}

.brandPos {
  -webkit-transform: translateX(100%);
  transform: translateX(100%);
  width: 620rpx;
  position: fixed;
  right: 0;
  bottom: 0;
  top: 0;
  border-left: 4rpx solid #d9d9d9;
  background: #fff;
  z-index: 1000000;
  max-width: 1260rpx;
}

.brandPos .selectedbrand {
  box-sizing: border-box;
  background: #fff;
  width: 100%;
  margin-bottom: 20rpx;
  overflow: hidden;
  height: 82rpx;
  display: -webkit-box;
  display: -webkit-flex;
  display: flex;
  -webkit-justify-content: space-around;
  justify-content: space-around;
}

.brandPos .selected {
  color: #df3448;
  border-color: #df3448;
}

.brandPos .title {
  border: 2rpx solid #d9d9d9;
  border-radius: 10rpx;
  padding: 16rpx 10rpx;
  font-size: 28rpx;
  background: #fff;
  text-align: center;
  border-width: 4rpx;
  display: -webkit-box;
  display: -webkit-flex;
  display: flex;
  -webkit-box-align: center;
  -webkit-align-items: center;
  align-items: center;
}
.brandlist {
  height: calc(100% - 134rpx);
  margin-bottom: 20rpx;
  overflow: auto;
}

.brandlist .type-title {
  background: #eee;
  height: 88rpx;
  display: -webkit-box;
  display: -webkit-flex;
  display: flex;
  -webkit-box-align: center;
  -webkit-align-items: center;
  align-items: center;
  padding-left: 32rpx;
}

.brandlist .scrollcar {
  height: 90rpx;
  display: -webkit-box;
  display: -webkit-flex;
  display: flex;
  -webkit-box-align: center;
  -webkit-align-items: center;
  align-items: center;
  border-bottom: 2rpx solid #d9d9d9;
  padding-left: 32rpx;
}
.brandPos .brand-name {
  height: 100rpx;
  width: 100%;
  box-sizing: border-box;
  padding: 20rpx 30rpx;
  display: flex;
  flex-direction: row;
  border-bottom: 10rpx solid #eeeeee;
}
.brand-box {
  height: 1100rpx;
}
.brandPos .brand-name .brand-img {
  width: 60rpx;
  height: 60rpx;
}
.brandPos .brand-name .brand-text {
  font-size: 30rpx;
  color: #333333;
  flex: 1;
  line-height: 60rpx;
  padding-left: 30rpx;
  box-sizing: border-box;
  font-weight: bolder;
}
.brand-list {
  border-bottom: 10rpx solid #eeeeee;
}
.brand-list:last-child {
  border: 0;
}
.brand-list .brand-title {
  height: 50rpx;
  color: #666666;
  font-size: 24rpx;
  line-height: 50rpx;
  box-sizing: border-box;
  padding-left: 30rpx;
  box-shadow: inset 0px -1px 0px 0px #d9d9d9;
}

.brand-list .brand-item {
  position: relative;
  margin-left: 30rpx;
  height: 88rpx;
  width: 100%;
  font-size: 28rpx;
  color: #333333;
  line-height: 88rpx;
  border-bottom: 2rpx solid #eeeeee;
}
.brand-list .brand-item:active {
  background: #f8f8f8;
  margin-left: 0;
  padding-left: 30rpx;
}

.eachchar-margin {
  height: 26rpx;
  background: #f2f2f2;
}

.bigger-char {
  font-weight: bold;
  font-size: 60rpx;
  color: #333333;
  background: #fafafa;
  height: 120rpx;
  width: 120rpx;
  position: fixed;
  left: 50%;
  top: 50%;
  margin-left: -60rpx;
  margin-top: -60rpx;
  text-align: center;
  line-height: 120rpx;
  border-radius: 10rpx;
  box-shadow: 0 0 2rpx #888;
}
.fixed {
  // position: fixed;
  // top: 88rpx;
  width: 100%;
  z-index: 100;
}
.fixed-wrapper {
  // padding-top: 148rpx;
}
.searchInput {
  width: 100%;
  height: 88rpx;
  border-bottom: 1px solid rgba(217, 217, 217, 1);
  padding-left: 30rpx;
  // position: absolute;
  // z-index: 3;
  background: #fff;
  display: flex;
  flex-direction: row;
  box-sizing: border-box;
}
.searchInput .serView {
  flex: 1;
  margin: 14rpx 0rpx 0 0;
  position: relative;
}

.searchInput .serView::before {
  content: '';
  position: absolute;
  width: 26rpx;
  height: 26rpx;
  background: url('https://res.tuhu.org/images/xcx/search.png') no-repeat;
  background-size: 100%;
  top: 18rpx;
  left: 20rpx;
}

.searchInput .cancel {
  color: rgba(153, 153, 153, 1);
  font-size: 26rpx;
  width: 112rpx;
  height: 88rpx;
  line-height: 88rpx;
  text-align: center;
}

.searchInput::after {
  content: '';
  display: table;
  clear: both;
}

.searchInput .serView input {
  width: 100%;
  height: 60rpx;
  line-height: 60rpx;
  background: rgba(238, 238, 238, 1);
  font-size: 26rpx;
  border-radius: 8rpx;
  padding: 0 0 0 60rpx;
  box-sizing: border-box;
}
.mask {
  position: absolute;
  left: 0;
  right: 0;
  bottom: 0;
  top: 88rpx;
  width: 100%;
  z-index: 10000000;
  box-sizing: border-box;
  padding-left: 30rpx;
}
.black-bg {
  background: rgba(0, 0, 0, 0.64);
}
.white-bg {
  background: #fff;
}
.mask-item {
  background: #fff;
  font-size: 28rpx;
  height: 88rpx;
  line-height: 88rpx;
  width: 100%;
  color: rgba(51, 51, 51, 1);
  border-bottom: 2rpx solid #d9d9d9;
  display: flex;
  flex-direction: row;
  align-items: center;
  image {
    width: 50rpx;
    height: 50rpx;
    margin-right: 30rpx;
  }
}
.no-data {
  background: none;
  margin: 300rpx auto;
  text-align: center;
  font-size: 26rpx;
  color: #666666;
  > image {
    width: 140rpx;
    height: 140rpx;
    margin-bottom: 61rpx;
  }
}
</style>
