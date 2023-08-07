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
          <cover-view class="searchBarL">
            <cover-image
              class="back-image1"
              @tap="backC"
              src="https://m.aerp.com.cn/mini-RG-main/maintenance_back_icon.png"
            ></cover-image>
            <cover-view class="border"></cover-view>
            <cover-image
              class="back-image2"
              @tap="indexClick"
              src="https://m.aerp.com.cn/mini-RG-main/maintenance_home_icon.png"
            ></cover-image>
          </cover-view>
          <cover-view class="searchBarR" @tap="titleClcik">
            <cover-view class="searchBarRWord">{{title}}</cover-view>
            <cover-image
              class="back-image3"
              src="https://m.aerp.com.cn/mini-RG-main/order_down_icon.png"
            ></cover-image>
          </cover-view>
        </cover-view>
      </cover-view>
    </cover-view>
  </div>
</template>

<script>
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
      default: '订单页'
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
    backClick() {
      wx.navigateBack({
        delta: 1
      })
    },
    homeClick() {
      wx.redirectTo({
        url: this.homePath
      })
    },
    titleClcik() {
      this.$emit('test', true)
    },
    backC() {
      this.$router.go(-1)
    },
    indexClick() {
      this.$router.push({ path: '/pages/index/main', reLaunch: true })
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
    z-index: 999999;
    width: 100%;
    // border: 1px solid rgba(237, 237, 237, 1);
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
        width: 514rpx;
        height: 65rpx;
        // background: rgba(248, 248, 248, 1);
        // border-radius: 33rpx;
        display: flex;
        align-items: center;
        justify-content: space-between;
        transform: translateY(-5%);
        .searchBarL {
          width: 175rpx;
          height: 60rpx;
          background: rgba(255, 255, 255, 1);
          border: 1rpx solid rgba(236, 236, 236, 1);
          border-radius: 30rpx;
          padding-left: 28rpx;
          box-sizing: border-box;
          display: flex;
          align-items: center;
          // border-right: solid rgba(232, 232, 232, 1) 1px;
          .border {
            width: 1rpx;
            height: 36rpx;
            border-right: 1rpx solid rgba(232, 232, 232, 1);
            margin: 0 30rpx 0 20rpx;
          }
          .back-image1,
          .back-image2 {
            width: 36rpx;
            height: 36rpx;
          }
        }
        .searchBarR {
          width: 330rpx;
          display: flex;
          align-items: center;
          justify-content: center;
          .searchBarRWord {
            font-size: 34rpx;
            font-family: Source Han Sans CN;
            font-weight: 400;
            color: rgba(33, 33, 33, 1);
          }
          .back-image3 {
            width: 24rpx;
            height: 24rpx;
            margin-left: 10rpx;
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