<template>
  <div class="demo_page" :style="{minHeight:windowHeight}">
    <div class="top_title">
      <div class="city" @click="toCity">
        {{city}}
        <div class="san"></div>
      </div>
      <div class="dropDownFa">
        <van-dropdown-menu class="fa">
          <!-- <span class="toCity" style="position:releative">112233</span> -->
          <!-- <van-dropdown-item :value="value1" :options="option1" /> -->
          <van-dropdown-item :value="value2" :options="option2" @change="onChange2" />
          <!-- <van-dropdown-item :value="value3" :options="option3" @change="onChange3(option3)" /> -->
          <!-- <van-dropdown-item :value="value4" :options="option4" @change="onChange4" /> -->
        </van-dropdown-menu>
      </div>
    </div>
    <div style="margin-top:100rpx;"></div>
    <!-- 门店 -->
    <div class="top_box" v-for="(item, index) in storearr" :key="index" @tap="storeClick(item)">
      <div class="top_one">
        <div class="top_images">
          <img :src="item.img" alt class="img1" mode="aspectFill" />
          <div class="butt1">{{item.type}}</div>
        </div>
        <div class="top_content">
          <p class="p1">{{item.simpleName}}</p>
          <div class="top_center">
            <p class="p3">{{item.address}}</p>
            <div class="top_all" @click.stop="navigate(item.latitude, item.longitude)">
              <img src="https://m.aerp.com.cn/mini-app-main/order_navigation_icon.png" alt class="img2" />
              <text class="txt3">{{item.distance}}KM</text>
            </div>
          </div>
          <div class="p2">
            <p class="txt1">
              总评分：
              <span>{{item.score}}</span>
            </p>
            <!-- <p class="txt1 txt2">
              总订单：
              <span>{{item.totalOrder}}</span>
            </p>-->
          </div>
        </div>
      </div>
      <!-- <div class="top_two">
        <text
          style="font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(102,102,102,1);"
        >当前工位状态：</text>
        <div class="first">
          <p :class="[{'span4':item.carWashStatus == 1},{'span2':item.carWashStatus == 2},{'span1':item.carWashStatus == 3}]"></p>
          <text class="span2">洗车适中</text>
        </div>
        <div class="first">
          <p :class="[{'span4':item.meiRongStatus == 1},{'span2':item.meiRongStatus == 2},{'span1':item.meiRongStatus == 3}]"></p>
          <text class="span2">美容适中</text>
        </div>
        <div class="first">
          <p :class="[{'span4':item.baoYangStatus == 1},{'span2':item.baoYangStatus == 2},{'span1':item.baoYangStatus == 3}]"></p>
          <text class="span2">保养适中</text>
        </div>
      </div>-->
      <!-- <div class="top_three">
       <text
          style="font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(102,102,102,1);min-width:200rpx"
        >当前服务项目：</text>
          <div class="top_four">
        <text class="metal" v-for="(item1, index1) in item.shopServices" :key="index1">{{item1}}</text>
        <text class="metal_service" v-if="item.shopServices.length == 0" >暂无服务项目</text>

          </div>
      </div>-->
    </div>
    <div class="else" :style="{height:windowHeight}" v-if="storearr.length<=0">
      <img :src="src2" alt mode="widthFix" class="elseimg" />
      <p style="font-size:32rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(136,136,136,1);">本地区暂无门店</p>
    </div>
  </div>
</template>
<script>
import { PostNearbyStore, GetRegionByCityId } from '../../api'

import eventBus from '../../utils/eventBus.js'
export default {
  data() {
    return {
      src2: `${this.globalData.imgPubUrl}mine_appointment_null.png`, // 暂无门店图片
      city: '',
      value2: 3,
      value4: 0,
      option2: [{ text: '请选择', value: 3 }],
      option4: [
        { text: '默认排序', value: 0 },
        { text: '上升排序', value: 1 },
        { text: '下降排序', value: 2 }
      ],
      storearr: [],
      windowHeight: '',
      storeArrList: [] // 上个页传过来的门店id
    }
  },
  onShow() {
    if (this.$store.state.curCityInfo.city) {
      this.city = this.$store.state.curCityInfo.city
      GetRegionByCityId({ CityId: this.$store.state.curCityInfo.cityId }).then(
        res => {
          let itemArr = []

          res.data.map(element => {
            itemArr.push({
              text: element.name,
              value: element.districtId,
              totalCount: element.totalCount
            })
          })
          this.option2 = itemArr
          if (this.$store.state.curCityInfo.districtId) {
            this.value2 = Number(this.$store.state.curCityInfo.districtId)
          } else {
            this.value2 = this.option2[0].value
          }
          this.getNearShop(this.storeArrList, this.value2, this.value4)
        }
      )
    } else {
      let that = this
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
  methods: {
    navigate(latitude, longitude) {
      wx.openLocation({
        latitude,
        longitude
      })
    },
    toCity() {
      this.$router.push('/pages/city/main')
    },
    // 下拉菜单事件
    onChange2(option2) {
      this.value2 = option2.mp.detail
      this.getNearShop(this.storeArrList, this.value2, this.value4)
    },
    onChange4(option4) {
      this.value4 = option4.mp.detail
      this.getNearShop(this.storeArrList, this.value2, this.value4)
    },
    // 门店详情页面
    storeClick(item) {
      // eventBus.$emit('changeShop', item)
      this.$store.commit('setGlobalShop', item)
      eventBus.$emit('isonShow', true)
      eventBus.$emit('selectShop', item)

      wx.navigateBack({})
    },

    // 获取附近门店方法
    getNearShop(shopIds, districtId, orderBy) {
      let that = this
      wx.getLocation({
        type: 'gcj02',
        altitude: true, // 高精度定位
        // 定位成功，更新定位结果
        success: res => {
          let rquestshop = {
            shopIds,
            longitude: res.longitude,
            latitude: res.latitude,
            cityId: that.$store.state.curCityInfo.cityId,
            districtId,
            orderBy,
            pageIndex: 1,
            pageSize: 100
          }
          // 获取附近门店列表
          PostNearbyStore(rquestshop)
            .then(res => {
              that.storearr = res.data.items
            })
            .catch(err => {})
        },
        // 定位失败回调
        fail: function() {
          let rquestshop = {
            // shopIds: [1],
            shopIds,
            longitude: 0,
            latitude: 0,
            cityId: that.$store.state.curCityInfo.cityId,
            districtId: districtId,
            orderBy,
            pageIndex: 1,
            pageSize: 10
          }
          // 获取附近门店列表
          PostNearbyStore(rquestshop)
            .then(res => {
              that.storearr = res.data.items
            })
            .catch(err => {})
        }
      })
    }
  },
  onLoad(options) {
    if (options.arr) {
      this.storeArrList = JSON.parse(options.arr)
    }
  },
  mounted() {
    let that = this
    wx.getSystemInfo({
      success: function(res) {
        let clientHeight = res.windowHeight
        let clientWidth = res.windowWidth
        let ratio = 750 / clientWidth
        that.windowHeight = clientHeight * ratio + 'rpx'
      }
    })
  }
}
</script>

<style scoped lang="scss">
.butt1 {
  width: 110rpx;
  min-height: 42rpx;
  background: #ff6324;
  border-radius: 23rpx;
  color: #fff;
  font: 26rpx/1 'SourceHanSansCN-Regular';
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 13rpx 0rpx 0rpx 34rpx;
  padding: 2rpx;
}
.city {
  /* flex: 1; */
  background: #ffffff;
  display: flex;
  align-items: center;
  justify-content: center;
  height: 50px;
  /* color: #333;
  font-size: 30rpx; */
  margin-right: 50rpx;
  width: 200rpx;
  font-size: 17px;
  padding: var(--dropdown-menu-title-padding, 0 16rpx);
  /* color: #323233; */
  color: var(--dropdown-menu-title-text-color, #323233);
  /* font-size: 15px; */
  font-size: var(--dropdown-menu-title-font-size, 32rpx);
  /* line-height: 18px; */
  line-height: var(--dropdown-menu-title-line-height, 36rpx);
}
.dropDownFa {
  width: 230rpx;
}
.fa {
  position: relative;
}

.toCity {
  position: absolute;
}
.demo_page {
  width: 100%;
  /* height: 100%; */
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #f3f3f3;
  padding-bottom: 20rpx;
  box-sizing: border-box;
}
.top_title {
  position: fixed;
  top: 0;
  left: 0;
  width: 750rpx;
  background: #ffffff;
  display: flex;
  align-items: center;
  padding: 0 150rpx 0 130rpx;
  box-sizing: border-box;
}
.top_title >>> .van-dropdown-menu {
  padding-right: 50rpx;
  box-sizing: border-box;
}
.top_box {
  width: 100%;
  /* height: 415rpx; */
  margin-top: 16rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  padding: 0 30rpx;
  box-sizing: border-box;
  background: #ffffff;
  /* padding-bottom: 20rpx; */
}
.top_one {
  width: 100%;
  height: 254rpx;
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: flex-start;
  padding: 30rpx 0;
  box-sizing: border-box;
  border-bottom: 1rpx solid #f3f3f3;
}
.top_images {
  width: 190rpx;
  height: 219rpx;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
}
.top_images .img1 {
  width: 180rpx;
  height: 146rpx;
}
.top_content {
  width: 487rpx;
  height: 194rpx;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
}
.top_content .p1 {
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(33, 33, 33, 1);
  width: 100%;
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
}
.top_center {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.top_center .p3 {
  width: 376rpx;
  /* height: 66rpx; */
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
}
.top_all {
  width: 80rpx;
  /* height: 50rpx; */
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.top_all .img2 {
  width: 44rpx;
  height: 44rpx;
}
.top_all .txt3 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
}
.p2 {
  width: 100%;
  display: flex;
  flex-direction: row;
  align-items: center;
}
.p2 .txt1 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(33, 33, 33, 1);
}
.p2 .txt2 {
  margin-left: 40rpx;
}
.p2 .txt1 span {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 174, 0, 1);
}

.top_two,
.top_three {
  width: 100%;
  min-height: 79rpx;
  padding: 0 20rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: center;
  border-bottom: 1rpx solid #f3f3f3;
}
.top_four {
  min-height: 79rpx;
  padding: 0 20rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: center;
  border-bottom: 1rpx solid #f3f3f3;
  flex-wrap: wrap;
}

.top_three {
  border-bottom: none;
  margin-bottom: 15rpx;
  min-width: 88rpx;
}
.first {
  /* width: 40rpx; */
  display: flex;
  flex-direction: row;
  align-items: center;
  margin-right: 30rpx;
}
.first .span1 {
  width: 15rpx;
  height: 15rpx;
  border-radius: 50%;
  background: green;
}
.first .span3 {
  width: 15rpx;
  height: 15rpx;
  border-radius: 50%;
  background: red;
}
.first .span4 {
  width: 15rpx;
  height: 15rpx;
  border-radius: 50%;
  background: #cccccc;
}
.first .span2 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(33, 33, 33, 1);
  margin-left: 15rpx;
}

.metal {
  padding: 4rpx 10rpx 8rpx 10rpx;
  box-sizing: border-box;
  border: 1rpx solid rgba(255, 99, 36, 1);
  border-radius: 4rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
  text-align: center;
  margin-right: 10rpx;
  margin-top: 10rpx;
}
.san {
  width: 0;
  height: 0;
  border: 9.9rpx solid transparent;
  border-top-color: #555;
  margin-top: 8rpx;
  margin-left: 10rpx;

  /* position: absolute;
   top: 47rpx;
right: 10rpx; */
}
.else {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.elseimg {
  width: 215rpx;
  height: 215rpx;
}
>>> .van-dropdown-menu__title {
  padding: var(--dropdown-menu-title-padding, 0 16rpx);
  /* color: #323233; */
  color: var(--dropdown-menu-title-text-color, #323233);
  /* font-size: 15px; */
  font-size: var(--dropdown-menu-title-font-size, 32rpx);
  /* line-height: 18px; */
  line-height: var(--dropdown-menu-title-line-height, 36rpx);
}
</style>