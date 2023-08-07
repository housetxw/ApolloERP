<template>
  <div class="demo_page" :style="{minHeight:windowHeight}">
    <div class="top_title">
      <div class="top_points">
        <p class="p1">{{userpoint}}</p>
        <p class="p2" @tap="duihuanClick">兑换</p>
      </div>
    </div>
    <p class="title">我的积分详情</p>
    <div class="top_details" v-for="(item,index) in detailsarr" :key="index">
      <text class="txt1">{{item.showCreateTime?(item.showCreateTime + ' '):''}}{{item.description}}{{item.referrerNoDes}}</text>
      <text class="txt2">{{item.pointValue}}</text>
    </div>
    <p class="title">积分用途</p>
    <div class="bottom">{{purpose}}</div>
  </div>
</template>
<script>
import { GetuserPoint } from '../../api'
export default {
  data () {
    return {
      detailsarr: [], // 积分获取数组
      windowHeight: '',
      userpoint: 0, // 用户积分
      purpose: '' // 积分用途
    }
  },
  methods: {
    // 积分对换
    duihuanClick () {
      // wx.showToast({ title: '开发中，敬请期待', icon: 'none' })
        this.$router.push({
          path: '/pages/integralExchange/main',
          query: {userpoint: this.userpoint}
        })
    },
    // 时间戳转换时间
    formatDateTime(inputTime) {
      let result = inputTime.slice(0,10);
     /*  if(inputTime){
        try{
          var date = new Date(inputTime)
          var y = date.getFullYear()
          var m = date.getMonth() + 1
          m = m < 10 ? '0' + m : m
          var d = date.getDate()
          d = d < 10 ? '0' + d : d
          result =  y + '-' + m + '-' + d
        }catch(e){}
      } */
      return result;
    },
  },
  onLoad () {
    let that = this
    // 获取系统信息
    wx.getSystemInfo({
      success: function (res) {
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
  },
  mounted () {
    let that = this
    GetuserPoint('')
      .then(res => {
        that.userpoint = res.data.currentPoint
        that.purpose = res.data.pointPurpose
        let detailsarrTemp = res.data.pointList
        if(detailsarrTemp){
          detailsarrTemp = detailsarrTemp.map(item=>{
            item['showCreateTime'] = that.formatDateTime(item.createTime)
            return item;
          })
        }
        that.detailsarr = detailsarrTemp
      })
      .catch(err => {})
  }
}
</script>
<style scoped>
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #f3f3f3;
}
.top_title {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  background: #fffcfb;
  padding: 30rpx;
  box-sizing: border-box;
}
.top_points {
  width: 100%;
  height: 240rpx;
  background: rgba(255, 99, 36, 1);
  box-shadow: 0rpx 10rpx 24rpx 0rpx rgba(255, 99, 36, 0.5);
  border-radius: 10rpx;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  padding: 50rpx 0 40rpx;
}
.top_points .p1 {
  font-size: 72rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(255, 255, 255, 1);
}
.top_points .p2 {
  width: 140rpx;
  height: 50rpx;
  border: 2rpx solid rgba(255, 255, 255, 1);
  border-radius: 24rpx;
  text-align: center;
  line-height: 48rpx;
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 255, 255, 1);
}
.title {
  width: 690rpx;
  height: 90rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx 10rpx 0rpx 0rpx;
  font-size: 32rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(51, 51, 51, 1);
  margin-top: 16rpx;
  text-align: left;
  line-height: 90rpx;
  padding-left: 28rpx;
  box-sizing: border-box;
  border-bottom: 1rpx solid #f3f3f3;
}
.top_details {
  width: 690rpx;
  height: 100rpx;
  background: rgba(255, 255, 255, 1);
  padding: 0 28rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  border-bottom: 1rpx solid #f3f3f3;
}
.top_details .txt1 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}
.top_details .txt2 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
}
.bottom {
  width: 690rpx;
  height: 120rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 0rpx 0rpx 10rpx 10rpx;
  padding: 34rpx 0 0 28rpx;
  box-sizing: border-box;
  margin-bottom: 63rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
}
</style>