<template>
  <div class="z-width-100-percent">
    <scroll-view scroll-y style="height:calc(100vh);" scroll-top="0" :scroll-into-view="currView">
      <dl class="ub-box ub-col">
        <!-- <dd class="z-width-100-percent z-margin-bottom-5-px z-bg-color-fff ub-box ub-ver">
          <div class="search ub-box ub-ver-v">
            <i class="iconfont icon-sousuo z-color-666 z-font-size-16"></i>
            <input
              class="ub-flex-1 z-font-size-14 z-color-666 z-padding-v-5-px z-margin-left-8-px"
              placeholder="城市/拼音"
            />
          </div>
        </dd>-->
        <!-- <dd class="z-width-100-percent z-bg-color-fff ub-box z-border-bottom-1-eee">
          <span
            id="cc"
            class="z-font-size-14 z-font-weight-bold z-color-333 z-padding-all-8-px"
          >当前：昆明</span>
        </dd>-->
        <dd class="z-width-100-percent z-bg-color-fff ub-box">
          <span class="z-font-size-14 z-color-333 z-font-weight-bold z-padding-all-8-px">当前定位</span>
        </dd>
        <dd class="z-width-100-percent z-bg-color-fff ub-box">
          <ul class="ub-box ub-wrap z-padding-all-8-px">
            <li
              @click.stop="clickPonitCity(pointCity)"
              class="hotcity z-font-size-14 z-color-333"
              v-if="pointCity.city"
            >{{pointCity.city || '定位失败'}}</li>
            <li
              class="hotcity z-font-size-14 z-color-333 forPosition"
              v-else
            >{{pointCity.city || '定位失败'}}
            <button open-type="openSetting" @opensetting="openLocation" class="wx_login_btn">定位失败</button>
            </li>
          </ul>
          
        </dd>
        <dd class="z-width-100-percent z-bg-color-fff ub-box">
          <span class="z-font-size-14 z-color-333 z-font-weight-bold z-padding-all-8-px">热门城市</span>
        </dd>
        <dd class="z-width-100-percent z-bg-color-fff ub-box">
          <ul class="ub-box ub-wrap z-padding-all-8-px">
            <li
              @click.stop="clickCity(city)"
              v-for="(city, idx) in hotCitys"
              :key="idx"
              class="hotcity z-font-size-14 z-color-333"
            >{{city.city}}</li>
          </ul>
        </dd>
        <dd class="z-width-100-percent z-bg-color-fff ub-box">
          <span class="z-font-size-14 z-color-333 z-font-weight-bold z-padding-all-8-px">所有城市</span>
        </dd>
        <dd class="ub-box ub-col">
          <div
            v-for="(val, idx) in citys"
            :key="idx"
            class="z-width-100-percent z-bg-color-fff ub-box ub-col"
          >
            <span
              :id="val.code"
              class="ub-flex-1 z-padding-all-8-px z-font-size-14 z-color-888 codeBK"
            >{{val.code}}</span>
            <ul class="ub-box ub-col">
              <li
                @click.stop="clickCity(city)"
                v-for="(city, i) in val.citys"
                :key="i"
                class="city ub-flex-1 z-font-size-14 z-color-666"
              >{{city.city}}</li>
            </ul>
          </div>
        </dd>
      </dl>
    </scroll-view>
    <!--fixed部分-->
    <dl class="fixList ub-box ub-col ub-ver-v">
      <dt class="z-font-size-12 z-margin-bottom-3-px" style="color:#06c1ae">定位热门</dt>
      <dd
        @click.stop="clickCode(val)"
        v-for="(val, idx) in citys"
        :key="idx"
        class="z-font-size-12"
        style="margin-bottom:2px;color:#06c1ae;padding:0 50px;"
      >{{val.code}}</dd>
    </dl>
  </div>
</template>
<script>
import { GetAllCitys, Location } from '../api'
export default {
  data () {
    return {
      currView: '',
      visitCityList: [
        { zip: '010', name: '北京' },
        { zip: '021', name: '上海' }
      ],
      citys: [],
      hotCitys: [],
      cityList: [],
      selectCity: {}
    }
  },
  computed: {
    pointCity () {
    
      return this.$store.state.pointCityInfo
    }
  },
  mounted () {
    this.currView = ''
    GetAllCitys().then(res => {
      this.citys = res.data.citys
      this.hotCitys = res.data.hotCitys
    
      //  this.citys = res.data.
    })
  },
  methods: {
    getCity () {
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
          Location({Longitude: longitude, Latitude: latitude}).then((res) => {
            that.$store.commit('setPointCity', res.data)
          })
        },
        // 定位失败回调
        fail: function () {
          wx.showToast({
            title: '定位失败',
            icon: 'none'
          })
        },

        complete: function () {
        // 隐藏定位中信息进度
          wx.hideLoading()
        }
      })
    },
    openLocation(res) {
      if (res.mp.detail.authSetting['scope.userLocation']) {
        this.getCity ()
      }
    },
    clickCode (obj) {
     
      if (obj.citys.length < 1) return
      this.currView = obj.code
    },
    clickCity (city) {
      this.$store.commit('updateCity', city)
      // this.$emit('cityService', city.regionName)
      this.$router.back()
    },
    clickPonitCity (pointCity) {
      this.$store.commit('updateCity', pointCity)
      this.$router.back()
    }
  }
}
</script>
<style scoped lang="scss">
.forPosition{
  position:relative;
}
.wx_login_btn {
  position: absolute;
  left: 0;
  top: 0;
  width: 164rpx;
  height: 68rpx;
  opacity: 0;
}
.search {
  background: #f5f5f5;
  width: 90%;
  border-radius: 15px;
  padding: 0 10px;
}
.codeBK {
  background: #f5f5f5;
}
.hotcity {
  border: 1px solid #f5f5f5;
  padding: 6px 12px;
  margin: 0 8px 8px 0;
}
.city {
  padding: 10px 8px;
  border-bottom: 1px solid #f5f5f5;
}
.fixList {
  position: fixed;
  right: 5px;
  top: 12%;
  z-index: 10;
  width: 30px;
  background: transparent;
}
.refresh {
  display: flex;
  align-items: center;
  line-height:72rpx;
  box-sizing: border-box;
  margin:0 10rpx;
  padding:0 0 0 20rpx;
  justify-content:center;
  background: transparent;
  font-size: 28rpx;
  color: #df3448;
  border-radius: 0;

&::before {
   content: '\e704';
   font-size: 32rpx;
   box-shadow: none;
 }

&::after {
   border: none;
 }
}
</style>