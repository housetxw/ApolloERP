<template>
  <div class="page">
    <div class="content" v-if="allCheckList.length>0">
      <div class="tab" v-for="(item,index) in allCheckList" :key="index">
        <div class="car">
          <img :src="item.brandUrl" alt class="img" />
          <div class="carTitle">
            <p class="carName">{{item.brand}}</p>
            <div class="carTitle2">
              <p class="carNumber">{{item.carPlate}}</p>
              <p class="carName">{{item.receiveDate}}到店</p>
            </div>
          </div>
        </div>
        <div class="condition" v-for="(itemm,indexx) in item.receiveCheck" :key="indexx">
          <div class="conditionTop" @click="tocheck(itemm.checkId)" v-if="itemm.statisticsData">
            <p class="title">车况检查</p>
            <p class="toCheck" v-if="itemm.statisticsData">
              去查看
              <img src="https://m.aerp.com.cn/mini-app-main/toCheck.png" alt class="tocheckimg" />
            </p>
          </div>
          <div class="conditionB" v-if="itemm.statisticsData">
            <p class="result">
              <img src="https://m.aerp.com.cn/mini-app-main/normal.png" alt class="resultimg" />
              正常({{itemm.statisticsData.normalCount}})
            </p>
            <p class="result">
              <img src="https://m.aerp.com.cn/mini-app-main/abnormal.png" alt class="resultimg" />
              异常({{itemm.statisticsData.abNormalCount}})
            </p>
            <p class="result">
              <img src="https://m.aerp.com.cn/mini-app-main/nochecked.png" alt class="resultimg" />
              未查({{itemm.statisticsData.uncheckCount}})
            </p>
          </div>
          <div class="else" v-else>
            <p class="title1">车况检查</p>
            <p>您的爱车并未做相关检查</p>
          </div>

          <div
            class="conditionTop"
            @click="showDiagnosticReport(item.carReportUrl)"
            v-if="item.carReportUrl"
          >
            <p class="title">车辆诊断报告</p>
            <p class="toCheck" v-if="item.carReportUrl">
              去查看
              <img src="https://m.aerp.com.cn/mini-app-main/toCheck.png" alt class="tocheckimg" />
            </p>
          </div>
          <div class="else" v-else>
            <p class="title1">车辆诊断报告</p>
            <p>您的爱车并未做相关诊断</p>
          </div>
        </div>
      </div>
    </div>
    <div class="else1" v-else :style="{height:windowHeight}">
      <img :src="src2" alt class="elseimg" />
      <p
        style="font-size:32rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(136,136,136,1);"
      >您的检查记录为空</p>
    </div>
    <div style="margin-bottom:60rpx;height:10rpx;width:100%;"></div>
    <div class="positionTOP" v-show="searchLoading">{{bmessage}}</div>
    <div class="positionTOP" v-show="searchLoading1">{{bmessage1}}</div>
  </div>
</template>

<script>
import { GetUserReceiveCheckList } from '../../../api'
export default {
  data() {
    return {
      searchLoading1: false,
      bmessage1: '',
      windowHeight: '',
      allCheckList: [],
      CheckId: '',
      PageIndex: 1,
      PageSize: 10,
      bmessage: '',
      searchLoading: false,
      src2: `${this.globalData.imgPubUrl}mine_appointment_null.png`
    }
  },
  computed: {
    carArrFirst() {
      return this.$store.state.carArr[0] || {}
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
  onReachBottom() {
    if (this.searchLoading == true) {
      return false
    } else {
      this.GetUserReceiveCheckList()
      this.searchLoading1 = true
      this.bmessage1 = '正在加载中...'
    }

    // let that = this
    // that.PageIndex++
    // that.searchLoading = true
    // that.bmessage = '正在加载中...'
    // let rquest = {
    //   UserId: that.carArrFirst.userId,
    //   PageIndex: that.PageIndex,
    //   PageSize: that.PageSize
    // }

    // GetUserReceiveCheckList(rquest)
    //   .then(res => {
    //     let arr = res.data.items
    //     let totalItems = res.data.totalItems
    //     if (totalItems > that.allCheckList.length) {
    //       that.allCheckList = that.allCheckList.concat(arr)
    //       if (that.bmessage == '正在加载中...') {
    //         that.searchLoading = false
    //       }
    //       console.log(222, that.allCheckList)
    //       console.log(221, that.allCheckList.length)
    //     } else {
    //       that.searchLoading = true
    //       that.bmessage = '我也是有底线的哦'
    //       console.log('没有更多了')
    //     }

    //     if (that.PageIndex > totalItems / that.PageSize) {
    //       return false
    //     }
    //   })
    //   .catch(err => {})
    // // }
  },
  mounted() {
    this.GetUserReceiveCheckList()
  },
  methods: {
    GetUserReceiveCheckList() {
      let that = this
      let rquest = {
        UserId: that.carArrFirst.userId,
        PageIndex: that.PageIndex,
        PageSize: that.PageSize
      }
      GetUserReceiveCheckList(rquest)
        .then(ress => {
          that.totalItems = ress.data.totalItems
          if (that.PageIndex * that.PageSize >= ress.data.totalItems) {
            that.searchLoading1 = false
            that.searchLoading = true
            that.bmessage = '我也是有底线的哦'
          }

          if (that.PageIndex === 1) {
            that.allCheckList = ress.data.items
          } else {
            that.allCheckList = that.allCheckList.concat(ress.data.items)
            console.log(that.allCheckList.length)
          }
          that.PageIndex++
        })
        .catch(err => {})
    },
    tocheck(checkId) {
      console.log('checkId', checkId)
      this.CheckId = checkId
      this.$router.push({
        path: '/pages/my/inspectReport/main',
        query: {
          CheckId: this.CheckId
        }
      })
    },
    showDiagnosticReport(carReport) {
      console.log('carReport', carReport)
      wx.downloadFile({
        url: carReport,
        success(res) {
          console.log(res)
          let data = res.tempFilePath
          wx.openDocument({
            filePath: data,
            fileType: 'pdf'
          })
        }
      })
    }
  },
  onLoad() {
    let that = this

    wx.getSystemInfo({
      success: function(res) {
        let clientHeight = res.windowHeight
        let clientWidth = res.windowWidth
        let ratio = 750 / clientWidth
        that.windowHeight = clientHeight * ratio - 88 + 'rpx'
      }
    })
  },
  onUnload: function() {
    this.PageIndex = 1
    this.bmessage = ''
    this.bmessage = ''
    this.searchLoading = false
  },
  onHide() {
    this.PageIndex = 1
    this.bmessage = ''
    this.bmessage = ''
    this.searchLoading = false
  }
}
</script>
<style>
page {
  background: rgba(243, 243, 243, 1);
}
</style>
<style lang
<style scoped lang="scss">
.content {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  padding: 16rpx 30rpx 0 30rpx;
  box-sizing: border-box;
  z-index: 20;
  background: #f3f3f3;
}
.tab {
  width: 100%;
  border-radius: 10rpx 10rpx 10rpx 10rpx;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  padding: 30rpx;
  padding-top: 0;
  box-sizing: border-box;
  background: #ffffff;
  margin-bottom: 16rpx;
  position: relative;
  min-height: 100rpx;
}
.car {
  width: 100%;
  height: 160rpx;
  background: #fff;
  border-radius: 10rpx;
  padding: 20rpx 0 25rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  border-bottom: 1rpx solid #f3f3f3;
  .img {
    width: 86rpx;
    height: 86rpx;
  }
  .carTitle {
    width: 530rpx;
    height: 125rpx;
    padding: 20rpx 0;
    box-sizing: border-box;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: flex-start;
  }
}
.carTitle2 {
  width: 530rpx;
  height: 125rpx;
  padding: 20rpx 0;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: flex-start;
}
.carName {
  color: #212121;
  font: 30rpx/1 'SourceHanSansCN-Medium';
}
.carNumber {
  color: #666666;
  font: 28rpx/1 'SourceHanSansCN-Medium';
}
.condition {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
}
.conditionTop {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  padding: 20rpx 0;
}
.title {
  color: #333;
  font: 28rpx/1 'SourceHanSansCN-Medium';
}
.title1 {
  color: #333;
  font: 28rpx/1 'SourceHanSansCN-Medium';
  margin-bottom: 20rpx;
}

.toCheck {
  color: #ff6234;
  font: 24rpx/1 'PingFang-SC-Medium-Medium';
  display: flex;

  align-items: center;
}
.tocheckimg {
  width: 28rpx;
  height: 28rpx;
}
.conditionB {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  padding-top: 20rpx;
}
.resultimg {
  width: 28rpx;
  height: 28rpx;
  margin-right: 8rpx;
}
.result {
  display: flex;
  color: #666;
  font: 28rpx/1 'PingFang-SC-Medium';
  align-items: center;
}
.else {
  color: #666;
  font: 26rpx/1 'SourceHanSansCN-Regular';
  margin-top: 20rpx;
}
.positionTOP {
  position: fixed;
  left: 0;
  bottom: 40rpx;
  width: 100%;
  text-align: center;
  font-size: 30rpx;
  color: #ccc;

  z-index: -1;
  height: 25rpx;
}
.else1 {
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
</style>
