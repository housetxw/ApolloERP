<template>
  <div>
    <div class="calContent">
      <div class="tip" @click="showTip">
        <img
          src="https://m.aerp.com.cn/mini-RG-main/reverseTipImg@2x.png"
          style="width:28rpx;height:28rpx;margin:0 10rpx"
        />预约说明
      </div>
      <Calendar
        :months="months"
        :value="value"
        :begin="begin"
        :end="end"
        :disabled="disabled"
        @next="next"
        @prev="prev"
        :lunar="false"
        clean
        @select="select"
        ref="calendar"
        @selectMonth="selectMonth"
        @selectYear="selectYear"
        :tileContent="tileContent"
        :almanacs="almanacs"
      />
    </div>

    <!-- <button @click="setToday">返回今日</button>
    <button @click="dateInfo">日期信息</button>
    <button @click="renderer">重新渲染年月日期</button>-->
  </div>
</template>

<script>
import Calendar from 'mpvue-calendar'
import 'mpvue-calendar/src/style.css'

export default {
  props: {
    disabled: {
      default: []
    },
    begin: {
      default: []
    },
    end: {
      default: []
    },
    tileContent: {
      default: []
    },
    reserveExplain: {
      default: ''
    }
  },
  data() {
    return {
      months: [
        '一月',
        '二月',
        '三月',
        '四月',
        '五月',
        '六月',
        '七月',
        '八月',
        '九月',
        '十月',
        '十一月',
        '十二月'
      ],
      value: []
    }
  },
  components: {
    Calendar
  },
  methods: {
    showTip() {
      wx.showModal({
        title: '提示',
        showCancel: false,
        content: this.reserveExplain
      })
    },
    prev(year, month, weekIndex) {
      console.log(year, month, weekIndex)
    },
    next(year, month, weekIndex) {
      console.log(year, month, weekIndex)
    },
    selectYear(year) {
      console.log(year)
    },
    selectMonth(month, year) {
      console.log(year, month)
    },
    setToday() {
      this.$refs.calendar.setToday()
    },
    dateInfo() {
      const info = this.$refs.calendar.dateInfo(2018, 8, 23)
      console.log(info)
    },
    renderer() {
      this.$refs.calendar.renderer() // 渲染2018年8月份
    },
    select(val, val2) {
      const info = this.$refs.calendar.dateInfo(val[0], val[1], val[2])
      console.log(info)
      this.$emit('childEvent', {
        Year: val[0],
        Month: val[1],
        Day: val[2],
        week: info.ncWeek
      })
      console.log(val)
      console.log(val2)
    }
  }
}
</script>
<style>
page {
  background: rgba(243, 243, 243, 1);
}
.mpvue-calendar {
  padding-bottom: 20rpx;
}
.slot-element {
  position: absolute;
  top: 72rpx;
  left: 21rpx;
  font-size: 20rpx;
}
.mpvue-calendar ._td.selected ._span {
  background: #fe6424 !important;
}
</style>
<style scoped lang="scss">
.tip {
  display: flex;
  align-items: center;
  font-size: 24rpx;
  font-family: SourceHanSansCN-Regular, SourceHanSansCN;
  font-weight: 400;
  color: rgba(255, 98, 36, 1);
  height: 70rpx;
}
.holiday {
  position: relative;
  top: -10;
}
.calContent {
  width: 700rpx;
  margin: 0 auto;
  // background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  overflow: hidden;
}
</style>