<template>
  <div class="demo_page">
    <div class="search">
      <div class="search_one">
        <img :src="searchImg" alt class="searchimg" />
        <input
          type="text"
          :focus="autofocus"
          :value="value"
          placeholder="请输入搜索关键词"
          class="searchInput"
          @input="ChangeClick"
        />
      </div>
      <text @tap="searchClick">搜索</text>
    </div>
    <div class="top_title">
      <p class="p1">热门搜索</p>
      <div class="box">
        <text
          class="one"
          v-for="(item, index) in searchArr"
          :key="index"
          @tap="HotWordClick(item)"
        >{{item}}</text>
      </div>
    </div>
    <div class="history_record">
      <div class="p1">
        <p class="txt1">历史搜索</p>
        <img
          src="https://m.aerp.com.cn/mini-app-main/search_delete_icon.png"
          alt
          class="img1"
          @tap="deleteAll"
        />
      </div>
      <div class="none_img" :style="{height:windowHeight}" v-if="historyArr.length === 0">
        <img src="https://m.aerp.com.cn/mini-app-main/search_null_pic.png" alt class="img2" />
        <text class="txt1">没有历史记录</text>
      </div>
      <div class="box" :style="{minHeight:windowHeight}" v-else>
        <p class="one" v-for="(item1, index1) in historyArr" :key="index1">
          <text class="txt1" @tap="historyClick(item1)">{{item1}}</text>
          <img
            src="https://m.aerp.com.cn/mini-app-main/search_clean_icon.png"
            class="img1"
            alt
            @tap="deleteOne(index1)"
          />
        </p>
      </div>
    </div>
  </div>
</template>
<script>
import { GetSearchHotWord } from '../../api'
export default {
  data() {
    return {
      autofocus: false,
      searchArr: [],
      historyArr: [],
      searchImg: `${this.globalData.imgPubUrl}search_search_icon.png`, // 搜索图标
      value: '',
      windowHeight: ''
    }
  },
  methods: {
    // input改变事件
    ChangeClick(e) {
      console.log('键盘输入的值', e)
      this.value = e.mp.detail.value
    },
    // 历史记录搜索事件
    historyClick(item1) {
      this.$router.push({
        path: '/pages/searchList/main',
        query: { value: item1 }
      })
    },
    // 搜索事件
    searchClick() {
      let that = this
      if (this.value == '') {
        wx.showToast({ title: '请输入搜索内容', icon: 'none' })
        return false
      }
      // this.historyArr.unshift(this.value)
      let array = this.historyArr
      console.log(array)
      console.log(1111)
      // if (array.length < 10 && array.length > 0) {
      var newArr = array.findIndex(function(item) {
        return item === that.value
      })
      console.log(newArr)
      // }
      if (newArr != -1) {
        // 删除已存在后重新插入至数组
        array.splice(newArr, 1)
        array.unshift(this.value)
        console.log(array)
        that.historyArr = array
        console.log(that.historyArr)
      } else {
        array.unshift(this.value)

        that.historyArr = array
      }

      if (this.historyArr.length > 10) {
        wx.removeStorageSync('history')
        console.log('大于10')

        this.historyArr.splice(10, this.historyArr.length - 10)

        this.historyArr = this.historyArr
      }
      wx.setStorageSync('history', that.historyArr)
      this.$router.push({
        path: '/pages/searchList/main',
        query: { value: that.value }
      })
    },
    // 删除全部历史记录
    deleteAll() {
      if (this.historyArr.length === 0) {
        wx.showToast({ title: '并无数据可删', icon: 'none' })
      } else if (this.historyArr.length > 0) {
        wx.removeStorageSync('history')
        this.historyArr = []
        if (this.historyArr.length == 0) {
          wx.showToast({ title: '已删除', icon: 'none' })
        }
      }
    },
    deleteOne(index1) {
      this.historyArr.splice(index1, 1)
      wx.setStorageSync('history', this.historyArr)
    },
    // 热词点击事件
    HotWordClick(item) {
      let array = this.historyArr
      let aritem = item
      console.log(array)
      console.log(1111)

      var newArr = array.findIndex(function(item) {
        return item === aritem
      })
      console.log(22, newArr)

      if (newArr != -1) {
        // 删除已存在后重新插入至数组
        array.splice(newArr, 1)
        array.unshift(item)
        console.log(array)
        this.historyArr = array
        console.log(22, this.historyArr)
      } else {
        array.unshift(item)
        this.historyArr = array

        // that.historyArr = array
      }
      let that = this
      if (this.historyArr.length > 10) {
        wx.removeStorageSync('history')
        // this.historyArr.pop()
        // this.historyArr.unshift(item)
        this.historyArr.splice(10, this.historyArr.length - 10)

        this.historyArr = this.historyArr
      }
      wx.setStorageSync('history', that.historyArr)
      this.$router.push({
        path: '/pages/searchList/main',
        query: { value: item }
      })
    },
    // 页面刚加载拿缓存中的数据
    cacheClick() {
      if (wx.getStorageSync('history').length == 0) {
        this.historyArr = []
      } else {
        this.historyArr = wx.getStorageSync('history')
      }
    }
  },
  onLoad() {
    let that = this
    wx.getSystemInfo({
      success: function(res) {
        let clientHeight = res.windowHeight
        let clientWidth = res.windowWidth
        let ratio = 750 / clientWidth
        that.windowHeight = clientHeight * ratio - 460 + 'rpx'
      }
    })
  },
  mounted() {
    let that = this
    // 搜索热词
    GetSearchHotWord('')
      .then(res => {
        that.searchArr = res.data
      })
      .catch(err => {})
  },
  onUnload() {
    this.value = ''
  },
  onHide() {
    this.value = ''
  },
  onShow() {
    this.cacheClick()
    this.autofocus = true
  }
}
</script>
<style scoped lang="scss">
.demo_page {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  background: #f3f3f3;
}
.search {
  width: 100%;
  padding: 0 20rpx 0 30rpx;
  box-sizing: border-box;
  // height: 50rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  background: #ffffff;
  .search_one {
    width: 620rpx;
    height: 80rpx;
    background: #f8f8f8;
    border-radius: 50rpx;
    padding-left: 20rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: row;
    // justify-content: flex-start;
    align-items: center;
    .searchimg {
      width: 32rpx;
      height: 32rpx;
    }
    .searchInput {
      width: 580rpx;
      // margin-left: 10rpx;
      padding: 0 20rpx;
      box-sizing: border-box;
      font-size: 26rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
  }
  text {
    font-size: 30rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(136, 136, 136, 1);
  }
}
.top_title {
  width: 100%;
  height: 290rpx;
  background: rgba(255, 255, 255, 1);
  padding: 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  .p1 {
    font-size: 30rpx;
    font-family: Source Han Sans CN;
    font-weight: bold;
    color: rgba(51, 51, 51, 1);
    line-height: 50rpx;
  }
  .box {
    width: 100%;
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: flex-start;
    align-items: center;
    margin-top: 30rpx;
    .one {
      height: 60rpx;
      background: rgba(255, 255, 255, 1);
      border: 1rpx solid rgba(227, 227, 227, 1);
      border-radius: 8rpx;
      line-height: 58rpx;
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
      padding: 0 30rpx;
      box-sizing: border-box;
      margin-right: 20rpx;
      margin-bottom: 20rpx;
    }
  }
}
.history_record {
  width: 100%;
  padding: 0 30rpx;
  box-sizing: border-box;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  .p1 {
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: flex-start;
    .txt1 {
      font-size: 30rpx;
      font-family: Source Han Sans CN;
      font-weight: bold;
      color: rgba(51, 51, 51, 1);
      margin-bottom: 50rpx;
    }
    .img1 {
      width: 32rpx;
      height: 32rpx;
    }
  }
  .none_img {
    width: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    .img2 {
      width: 215rpx;
      height: 215rpx;
    }
    .txt1 {
      font-size: 32rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(136, 136, 136, 1);
      margin-top: 54rpx;
    }
  }
  .box {
    width: 100%;
    .one {
      width: 100%;
      height: 90rpx;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      border-bottom: 1rpx solid #f3f3f3;
      .txt1 {
        width: 80%;
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(136, 136, 136, 1);
      }
      .img1 {
        width: 30rpx;
        height: 30rpx;
      }
    }
  }
}
</style>