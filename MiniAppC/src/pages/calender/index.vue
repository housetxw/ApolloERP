<template>
  <div class="demo_page">
    <my-Calender
      :disabled="disabled"
      :reserveExplain="reserveExplain"
      :tileContent="tileContent"
      @childEvent="getReserveTime"
    ></my-Calender>

    <!-- 礼拜跟时间 -->
    <div class="top_date">
      <div class="bottom_date">
        <div
          :class="['one_before',{'activeOne':item1.isHighLight}]"
          v-for="(item1,index1) in datearr"
          :key="index1"
          @tap="dateClick(item1)"
        >
          {{item1.reserveTime}}
          <img :src="posimg" alt class="posimg" v-if="item1.isChecked" />
        </div>
        <div class="call" @tap="telClick">
          <img
            src="https://m.aerp.com.cn/mini-RG-main/tel2.png"
            alt
            style="width:50rpx;height:48rpx; margin-right:6rpx; "
          />
          当日急修
        </div>
      </div>
    </div>
    <div class="btn">
      <van-button round size="large" :color="color" type="default" @tap="sureDate">确定预约</van-button>
    </div>
  </div>
</template>
<script>
import { GetShopReserveSurplus, CanReserveTimeV3 } from '../../api'
import myCalender from '../../components/myCalender.vue'
import eventBus from '../../utils/eventBus.js'
export default {
  components: {
    myCalender
  },
  data() {
    return {
      week: '',
      color: '#E8E8E8',
      a: 0, // 确认到店的变量
      reserveExplain: '',
      disabled: [],
      begin: [],
      end: [],
      tileContent: [],
      year: '', // 年
      month: '', // 月
      day: '', // 日
      shopId: 7,
      datearr: [],
      reserveTime: '', // 修改预约需要的时间
      posimg: `${this.globalData.imgPubUrl}add_appointment_click.png` // 时间确认小对勾
    }
  },
  methods: {
    sureDate() {
      let that = this

      if (that.a == 1) {
        wx.showModal({
          title: '预约通知',
          content: '确定提交预约请求？',
          success(res) {
            if (res.confirm) {
              that.globalData.reservedTime = {
                year: that.year,
                reserveWeekDay: that.week.replace(/'星期'/i, '周'),
                reserveDate: `${that.month}/${that.day}`,
                month: that.month,
                day: that.day,
                reserveTime: that.reserveTime,
                week: that.week,
                isHighLight: true
              }
              // eventBus.$emit('selectTime', that.globalData.reservedTime)
              wx.navigateBack({
                delta: 1
              })
            } else if (res.cancel) {
              console.log('用户点击取消')
            }
          }
        })
      } else {
        wx.showToast({ title: '请选择预约时间', icon: 'none' })
      }
    },
    // 时间点击事件
    dateClick(item1) {
      if (item1.isHighLight) {
        this.datearr.forEach(element => {
          element.isChecked = false
        })
        item1.isChecked = !item1.isChecked
        this.color = '#FF6324'
        this.a = 1
        // this.reserveTime = item1.reserveTime
        if (item1.reserveTime == '到店等待') {
          this.reserveTime = 'IsWait'
        } else {
          this.reserveTime = item1.reserveTime
        }
      } else {
        wx.showToast({ title: '所选时间暂不可预约', icon: 'none' })
      }
    },
    // 客服事件
    telClick() {
      console.log('电话', this.telephone)
      wx.showModal({
        content: this.telephone,
        confirmText: '呼叫',
        confirmColor: '#FF6324',
        success(res) {
          if (res.confirm) {
            wx.makePhoneCall({
              phoneNumber: this.telephone
            })
          } else if (res.cancel) {
          }
        }
      })
    },
    modifyAppointment() {
      let that = this
      // 调用/Receive/GetReserveInfo接口  /Receive/CanReserveTime 获取可预约的时间点    获取用户要修改的预约的数据
      let resultnum = {
        // ReserveId: that.reserveId
        // ShopId: that.shopId
        ShopId: 7
      }
      GetShopReserveSurplus(resultnum)
        .then(res => {
          console.log(332255, res)
          that.disabled = res.data.disabled
          that.tileContent = res.data.reserveSurplus
          that.begin = res.data.surplusBegin
          that.end = res.data.surplusEnd
          that.datearr = res.data.reserveTimeList
          that.reserveExplain = res.data.reserveExplain // 预约说明
        })
        .catch(err => {})
    },
    getReserveTime({ Year, Month, Day, week }) {
      this.day = Day
      this.month = Month
      this.year = Year
      this.week = week
      console.log('day', this.year, this.month, this.day)
      CanReserveTimeV3({ ShopId: this.shopId, Year, Month, Day })
        .then(resa => {
          if (resa.code == 10000) {
            this.datearr = resa.data
            console.log('datearr', this.datearr)
          }
        })
        .catch(erra => {})
      console.log(11)
    }
  },
  mounted() {
    let that = this
    // 获取系统信息
    wx.getSystemInfo({
      success(res) {
        let clientHeight = res.windowHeight
        let clientWidth = res.windowWidth
        let ratio = 750 / clientWidth
        that.windowHeight = clientHeight * ratio + 'rpx'
      }
    })
  },
  onLoad(options) {
    this.modifyAppointment()
  },
  onShow() {},
  onHide() {},
  // 当点击返回按钮，页面销毁时，清除从上个页面传的参，防止不明情况的bug产生
  onUnload() {}
}
</script>
<style scoped lang="scss">
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  background: #f9f9f9;
}

.top_date {
  width: 690rpx;
  // height: 505rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  margin: 16rpx 0;
  padding: 30rpx 20rpx;
  box-sizing: border-box;
}
.top_week {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}

.top_date .top_week .one {
  width: 100rpx;
  height: 100rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.top_date .top_week .one .yuan {
  width: 70rpx;
  height: 85rpx;
  border-radius: 36rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  padding: 5rpx 0;
}
.span_span {
  width: 64rpx;
  height: 64rpx;
  background: rgba(232, 232, 232, 1);
  border-radius: 50%;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  box-sizing: border-box;
}

.top_date .top_week .one .txt {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
  margin-top: 5rpx;
}
.bottom_date {
  width: 100%;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: flex-start;
  align-items: flex-start;
  margin-top: 37rpx;
  position: relative;
}

.one_before {
  position: relative;
  width: 145rpx;
  height: 60rpx;
  background: rgba(255, 255, 255, 1);
  border: 1rpx solid rgba(227, 227, 227, 1);
  border-radius: 8rpx;
  text-align: center;
  line-height: 58rpx;
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
  margin-right: 16rpx;
  margin-bottom: 20rpx;
}

.posimg {
  position: absolute;
  top: 0rpx;
  right: 2rpx;
  width: 24rpx;
  height: 24rpx;
}

.call {
  position: absolute;
  right: 16rpx;
  bottom: 16rpx;

  width: 180rpx;
  height: 60rpx;
  background: #ffeee7;
  border: 2rpx solid #ff6324;
  border-radius: 50rpx;
  color: #ff6324;
  font: 24rpx/60rpx 'SourceHanSansCN-Regular';
  padding: 10rpx;
  box-sizing: border-box;
  display: flex;
  align-items: center;
}
.btn {
  width: 750rpx;
  /* height:98rpx; */
  background: rgba(255, 255, 255, 1);
  padding: 10rpx 30rpx;
  box-sizing: border-box;
}
.activeOne {
  background: rgba(255, 238, 231, 1);
  border: 1rpx solid rgba(255, 193, 173, 1);
  color: rgba(255, 99, 36, 1);
}
</style>


