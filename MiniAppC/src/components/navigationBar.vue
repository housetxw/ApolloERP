<template>
  <div class="comp-navbar">
    <!-- 占位栏 -->
    <cover-view class="placeholder-bar" :style="{height: navBarHeight + 'px'}"></cover-view>
    <!-- 导航栏主体 -->
    <cover-view
      class="navbar"
      :style="{height: navBarHeight + 'px',backgroundColor:navBackgroundColor}"
    >
      <!-- 状态栏 -->
      <cover-view class="nav-statusbar" :style="{height: statusBarHeight + 'px'}"></cover-view>
      <!-- 标题栏 -->
      <cover-view class="nav-titlebar" :style="{height: titleBarHeight + 'px' }">
        <cover-view class="searchBar">
          <cover-view class="searchBarL" @tap="addressClick">
            <cover-view class="searchBarLWord">{{city}}</cover-view>
            <cover-image
              class="back-image1"
              src="https://m.aerp.com.cn/mini-RG-main/triangle_icon.png"
            ></cover-image>
          </cover-view>
          <!-- <cover-view class="searchBarLine"></cover-view> -->
          <cover-view class="searchBarR" @tap="searchClick">
            <cover-image
              class="back-image2"
              src="https://m.aerp.com.cn/mini-RG-main/search_icon.png"
            ></cover-image>

            <cover-view class="searchBarRWord">搜索关键词</cover-view>
          </cover-view>
        </cover-view>
        <!-- home及后退键 -->
        <!-- <cover-view class="bar-options">
          <cover-view v-if="backVisible" class="opt opt-back" @click="backClick()">
            <cover-image class="back-image" src="/static/images/back.png"></cover-image>
          </cover-view>
          <cover-view class="line" v-if="backVisible && homePath"></cover-view>
          <cover-view v-if="homePath" class="opt opt-home" @click="homeClick()">
            <cover-image class="home-image" src="/static/images/home.png"></cover-image>
          </cover-view>
        </cover-view>-->
        <!-- 标题 -->
        <!-- <cover-view class="bar-title" :style="[{color:titleColor}]">{{title}}</cover-view> -->
      </cover-view>
    </cover-view>
  </div>
</template>

<script>
import store from '../store.js'
export default {
  props: {
    // 导航栏背景色
    navBackgroundColor: {
      default: '#ffffff'
    },
    // 标题颜色
    titleColor: {
      default: '#000000'
    },
    // 标题文字
    title: {
      required: false,
      default: '来画大世界'
    },
    // 是否显示后退按钮
    backVisible: {
      required: false,
      default: false
    },
    // home按钮的路径
    homePath: {
      required: false,
      default: ''
    }
  },
  computed: {
    city() {
      return this.$store.state.curCityInfo.city || '请选择'
    }
  },
  data() {
    return {
      statusBarHeight: '', // 状态栏高度
      titleBarHeight: '', // 标题栏高度
      navBarHeight: '', // 导航栏总高度
      platform: '',
      model: '',
      brand: '',
      system: ''
    }
  },
  beforeMount() {
    const self = this
    wx.getSystemInfo({
      success(system) {
        self.statusBarHeight = system.statusBarHeight
        self.platform = system.platform
        self.model = system.model
        self.brand = system.brand
        self.system = system.system
        let platformReg = /ios/i
        if (platformReg.test(system.platform)) {
          self.titleBarHeight = 42
        } else {
          self.titleBarHeight = 48
        }
        self.navBarHeight = self.statusBarHeight + self.titleBarHeight
      }
    })
  },
  mounted() {},
  methods: {
    // 搜索事件
    searchClick() {
      this.$router.push('/pages/searchpage/main')
    },
    addressClick() {
      this.$router.push('/pages/city/main')
    },
    backClick() {
      wx.navigateBack({
        delta: 1
      })
    },
    homeClick() {
      wx.redirectTo({
        url: this.homePath
      })
    }
  }
}
</script>

<style lang="scss" scoped>
.comp-navbar {
  width: 100vw;
  .navbar {
    position: fixed;
    left: 0;
    top: 0;
    width: 100%;
    border: 1px solid rgba(237, 237, 237, 1);
    .nav-titlebar {
      // border: 1px solid green;
      width: 100%;
      display: flex;
      align-items: center;
      // justify-content: center;
      padding-left: 30rpx;
      box-sizing: border-box;
      position: relative;
      .searchBar {
        width: 470rpx;
        height: 65rpx;
        background: rgba(248, 248, 248, 1);
        border-radius: 33rpx;
        display: flex;
        align-items: center;
        transform: translateY(-5%);
        .searchBarL {
          display: flex;
          align-items: center;
          border-right: solid rgba(232, 232, 232, 1) 1px;
          .searchBarLWord {
            font-size: 28rpx;
            font-family: Source Han Sans CN;
            font-weight: 400;
            color: rgba(33, 33, 33, 1);
            margin: 0 7rpx 0 28rpx;
          }
          .back-image1 {
            width: 20rpx;
            height: 20rpx;
            margin-right: 20rpx;
          }
        }
        .searchBarLine {
          height: 35rpx;
          border-right: solid rgba(232, 232, 232, 1) 1px;
        }
        .searchBarR {
          display: flex;
          align-items: center;
          height: 100%;
          flex: 1;
          .back-image2 {
            width: 29rpx;
            height: 28rpx;
            margin: 0 20rpx 0 20rpx;
          }
          .searchBarRWord {
            font-size: 26rpx;
            font-family: Source Han Sans CN;
            font-weight: 400;
            color: rgba(190, 190, 190, 1);
          }
        }
      }
      .bar-options {
        // width: 87px;
        // height: 30px;
        width: 170rpx;
        height: 60rpx;
        display: flex;
        // border: 1px solid hsla(0, 0%, 100%, .25);
        // border: 1px solid #ededed;
        box-sizing: border-box;
        align-items: center;
        justify-content: space-around;
        position: absolute;
        left: 7px;
        display: flex;
        align-items: center;
        background: hsla(0, 0%, 100%, 0.6);
        border-radius: 27px;
        padding-right: 5rpx;
        .opt {
          width: 50rpx;
          height: 50rpx;
          display: flex;
          justify-content: center;
          align-items: center;
        }
        .opt-back {
          .back-image {
            width: 16rpx;
            height: 28rpx;
          }
        }
        .line {
          display: block;
          height: 30rpx;
          width: 1px;
          background-color: gray;
        }
        .opt-home {
          .home-image {
            width: 36rpx;
            height: 34rpx;
          }
        }
      }
      .bar-title {
        width: 45%;
        font-size: 14px;
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
        text-align: center;
      }
    }
  }
  .placeholder-bar {
    background-color: transparent;
    width: 100%;
  }
}
</style>