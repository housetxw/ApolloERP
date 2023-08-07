<template>
  <div class="demo_page" :style="{minHeight:windowHeight}">
    <div class="top_title">
      <div class="city" @click="toCity">
        {{city || '选择城市'}}
        <div class="san"></div>
      </div>
      <div class="dropDownFa">
        <van-dropdown-menu class="fa" active-color="#FA7943">
          <!-- <span class="toCity" style="position:releative">112233</span> -->
          <!-- <van-dropdown-item :value="value1" :options="option1" /> -->
          <van-dropdown-item :value="value2" :options="option2" @change="onChange2" id="item1" />
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
          <img :src="item.img" alt class="img1" mode="aspectFill" lazy-load="true" />
          <div class="butt1">{{item.type}}</div>
        </div>
        <div class="top_content">
          <p class="p1">{{item.simpleName}}</p>
          <div class="top_center">
            <p class="p3">{{item.address}}</p>
            <div class="top_all" @click.stop="navigate(item.latitude, item.longitude,item.simpleName,item.address)">
              <img src="https://m.aerp.com.cn/mini-RG-main/order_navigation_icon.png" alt class="img2" />
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

      isShow: true,
      lock: false,
      city: '',
      value2: 3,
      value4: 0,
      option2: [{ text: '请选择', value: 3 }],
      option4: [
        { text: '默认排序', value: 0 }
        // { text: '上升排序', value: 1 },
        // { text: '下降排序', value: 2 }
      ],
      storearr: [],
      windowHeight: '',
      longitude: 0,
      latitude: 0
    }
  },
  // mounted () {

  // },
  onShow() {
    eventBus.$on('isShow1', res => {
      console.log("onShow res = ",res)
      this.isShow = res
    })
    if (this.isShow) {
      if (!this.lock) {
        if (this.$store.state.curCityInfo.city) {
          this.city = this.$store.state.curCityInfo.city
          console.log("city  =   ",this.$store.state.curCityInfo.city)
          GetRegionByCityId({
            CityId: this.$store.state.curCityInfo.cityId
          })
            .then(res => {
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
                console.log(5588)
                this.value2 = Number(this.$store.state.curCityInfo.districtId)
              } else {
                console.log(5599)
                this.value2 = this.option2[0].value
              }
              this.getNearShop(this.value2, this.value4)
              // console.log(2255, this.value2)
            })
            .finally(rea => {})
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
        this.$forceUpdate()
      }
    }
  },
  methods: {
    navigate(latitude, longitude, name, address) {
      wx.openLocation({
        latitude,
        longitude,
        scale: 18,
        name,
        address,
        success: res => {
          console.log(`打开位置成功`, res)
        }
      })
    },
    toCity() {
      this.$router.push('/pages/city/main')
    },
    // 下拉菜单事件
    onChange2(option2) {
      this.lock = true
      console.log('下拉菜单事件2',JSON.stringify(option2))
      this.value2 = option2.mp.detail
      this.getNearShop(this.value2, this.value4)
    },
    onChange4(option4) {
      this.value4 = option4.mp.detail
      this.getNearShop(this.value2, this.value4)
      console.log('下拉菜单事件4',JSON.stringify(option4))
    },
    // 门店详情页面
    storeClick(item) {
      this.$router.push({
        path: '/pages/storeDetails/main',
        query: {
          id: item.id,
          longitude: this.longitude,
          latitude: this.latitude
        }
      })
    },
    // 获取附近门店方法
    getNearShop(districtId, orderBy) {
      let that = this
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
            districtId,
            orderBy: orderBy,
            pageIndex: 1,
            pageSize: 100
          }
          // 获取附近门店列表
          PostNearbyStore(rquestshop)
            .then(res => {
              // console.log(`获取附近门店列表成功函数,${JSON.stringify(res)}`)
              that.storearr = res.data.items
            })
            .catch(err => {
              console.log(`获取附近门店列表失败函数,${err}`)
            })
        },
        // 定位失败回调
        fail: function() {
          that.longitude = 0
          that.latitude = 0
          let rquestshop = {
            shopIds: [1],
            longitude: 0,
            latitude: 0,
            cityId: that.$store.state.curCityInfo.cityId,
            districtId: districtId,
            orderBy: orderBy,
            pageIndex: 1,
            pageSize: 100
          }
          // 获取附近门店列表
          PostNearbyStore(rquestshop)
            .then(res => {
              // console.log(`获取附近门店列表成功函数,${JSON.stringify(res)}`)
              that.storearr = res.data.items
            })
            .catch(err => {
              console.log(`获取附近门店列表失败函数,${err}`)
            })
        }
      })
    }
  },
  onHide() {
    this.lock = false
  },
  onUnload() {
    this.lock = false
    this.isShow = true
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
  // flex: 1;
  background: #ffffff;
  display: flex;
  align-items: center;
  justify-content: center;
  height: 50px;
  width: 200rpx;
  margin-right: 50rpx;
  /* color: #333; */
  font-size: 17px;
  padding: var(--dropdown-menu-title-padding, 0 16rpx);
  /* color: #323233; */
  color: var(--dropdown-menu-title-text-color, #323233);
  /* font-size: 15px; */
  font-size: var(--dropdown-menu-title-font-size, 32rpx);
  /* line-height: 18px; */
  line-height: var(--dropdown-menu-title-line-height, 36rpx);
  /* overflow: hidden;
white-space: nowrap;
text-overflow: ellipsis; */
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
  margin-top: 16rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  padding: 0 30rpx;
  box-sizing: border-box;
  background: #ffffff;
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
  height: 218rpx;
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
.metal_service {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgb(248, 186, 161);
}
.san {
  width: 0;
  height: 0;
  border: 4.5px solid transparent;
  border-top-color: #555;
  margin-top: 8rpx;
  margin-left: 10rpx;
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