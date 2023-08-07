<template>
  <div class="demo_page" :style="{minHeight:windowHeight}">
    <div style="height:160rpx;background:#fff;"></div>
    <div class="search">
      <div class="search_one">
        <img :src="searchImg" alt class="searchimg" />

        <input
          type="text"
          :value="value"
          placeholder="搜索保险公司名称"
          class="searchInput"
          @input="ChangeClick"
        />
      </div>
    </div>

    <div>
      <div
        class="top_title row"
        v-if="companyList.length>0"
        v-for="(item, index) in companyList"
        :key="index"
        @tap="choose( item.displayName)"
      >
        <img :src="item.imageUrl" alt style="width:80rpx;height:80rpx;margin-right: 16rpx;" />
        <p class="p2">{{ item.displayName}}</p>
      </div>
    </div>
  </div>
</template>
<script>
import { GetInsuranceCompanyList } from '../../api'
import eventBus from '../../utils/eventBus.js'
export default {
  data () {
    return {
      searchImg: `${this.globalData.imgPubUrl}search_search_icon.png`, // 搜索图标
      companyList: [],
      value: ''
    }
  },
  methods: {
    getList (param) {
      let that = this
      // 获取保险公司列表

      GetInsuranceCompanyList(param)
        .then(res => {
          that.companyList = res.data
        })
        .catch(err => {})
    },
    choose (displayName) {
      eventBus.$emit('selectCompany', displayName)
      wx.navigateBack({
        delta: 1
      })
      console.log(333, displayName) // 获取保险公司名称
    },

    ChangeClick (e) {
      const value = e.target.value.trim()
      console.log(344, value)
      let that = this

      if (value) {
        let data1 = { SearchContent: value }
        that.getList(data1)
      } else {
        that.getList()
      }
    }
  },
  onShow () {},
  mounted () {
    const param = {
      SearchContent: null
    }
    this.getList()
  }
}
</script>
<style lang="scss" scoped>
.demo_page {
  width: 100%;
  height: 140%;
  display: flex;
  flex-direction: column;
  background: #f3f3f3;
}

.search {
  width: 100%;
  padding: 0 20rpx 20rpx 30rpx;
  box-sizing: border-box;
  // height: 50rpx;
  min-height: 120rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  background: #ffffff;
  position: fixed;
  top: 0rpx;
  z-index:99
}

.search_one {
  width: 620rpx;
  min-height: 75rpx;
  background: #f8f8f8;
  border-radius: 50rpx;
  padding-left: 20rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  // justify-content: flex-start;
  align-items: center;
}

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

.top_title {
  width: 100%;
  height: 90rpx;

  text-align: center;
  line-height: 90rpx;
  font-size: 46rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(255, 255, 255, 1);
}

.row {
  display: flex;
  flex-direction: row;
  align-items: center;
  background: #ffffff;
  padding: 0 30rpx;
  box-sizing: border-box;
  border-bottom: 1rpx solid #f3f3f3;
}

.top_title {
  width: 100%;
  min-height: 120rpx;
}

.top_title .p2 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}
</style>
