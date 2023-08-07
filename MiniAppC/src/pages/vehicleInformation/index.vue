<template>
  <div class="demo_page">
    <div class="carPopup" v-if="showDis">
      <div class="box_Popup">
        <div class="top_title">{{imgTitle}}位置示意图</div>
        <div class="all_popup">
          <img src="https://m.aerp.com.cn/mini-RG-main/DriversLicense.png" alt class="img_card_img" />
          <!-- <p class="all_zhushi">这里可以是文字说明的地方随便多少字吧</p> -->
        </div>
      </div>
      <img
        src="https://m.aerp.com.cn/mini-RG-main/goodsdetail_close_icon.png"
        alt
        class="ximg"
        @tap="disPopupClick"
      />
    </div>
    <div class="top_title row" @click="toAddCar">
      <p class="p1">
        选择车型
        <span style="color:red">*</span>
      </p>
      <p class="p2">
        {{carInfo.vehicle}}
        <img
          src="https://m.aerp.com.cn/mini-RG-main/maintenance_jump_icon.png"
          alt
          style="width:24rpx;height:24rpx;margin-left:15rpx;"
        />
      </p>
    </div>
    <div class="top_title row">
      <p class="p1">车牌号</p>
      <div class="p2">
        <input placeholder="请输入车牌号" class="top_input" v-model="carInfo.carNumber" />
      </div>
    </div>
    <div class="top_title row">
      <p class="p1">行驶里程</p>
      <p class="p2">
        <input placeholder="请输入公里数" class="top_input" v-model="carInfo.totalMileage" />KM
      </p>
    </div>
    <div class="top_title row">
      <p class="p1">车架号</p>
      <p class="p2">
        <input placeholder="请输入车架号" class="top_input" v-model="carInfo.vinCode" />
        <img
          src="https://m.aerp.com.cn/mini-RG-main/mine_help_icon.png"
          alt
          class="img_img"
          @tap="showimg('车架号')"
        />
      </p>
    </div>
    <div class="top_title row">
      <p class="p1">发动机号</p>
      <p class="p2">
        <input placeholder="请输入发动机号" class="top_input" v-model="carInfo.engineNo" />
        <img
          src="https://m.aerp.com.cn/mini-RG-main/mine_help_icon.png"
          alt
          class="img_img"
          @tap="showimg('发动机号')"
        />
      </p>
    </div>
    <div class="top_title row" @click="toAddInsure">
      <p class="p1">商业车险公司</p>
      <p class="p2">
        {{insuranceComany}}
        <img
          src="https://m.aerp.com.cn/mini-RG-main/maintenance_jump_icon.png"
          alt
          style="width:24rpx;height:24rpx;margin-left:15rpx;"
        />
      </p>
    </div>
    <!-- <div class="top_title row">
      <p class="p1">保险开始日期</p>
      <p class="p2" @tap="starttimeSelect">
        {{currentDate1}}
        <img
          src="https://m.aerp.com.cn/mini-RG-main/maintenance_jump_icon.png"
          alt
          style="width:24rpx;height:24rpx;margin-left:15rpx;"
        />
      </p>
      <div class="bottomDate" v-if="startnum">
        <van-datetime-picker
          type="date"
          :value="currentDate"
          :max-date="maxDate"
          @confirm="sureText"
          @cancel="clearText"
        />
      </div>
    </div>-->
    <div class="top_title row" @tap="EndtimeSelect">
      <p class="p1">保险到期日期</p>
      <p class="p2 p3">
        {{endDate1}}
        <img
          src="https://m.aerp.com.cn/mini-RG-main/maintenance_jump_icon.png"
          alt
          style="width:24rpx;height:24rpx;margin-left:15rpx;"
        />
      </p>
    </div>
    <div class="bottomDate" v-if="endnum">
      <van-datetime-picker
        type="date"
        :value="endDate"
        :min-date="minDate"
        @confirm="endsureText"
        @cancel="endclearText"
      />
    </div>
    <!-- <div class="top_bottom row">
      <div class="bottom1">
        <p class="p1">设置默认车辆</p>
        <p class="p2">提醒：每次适配会默认推荐使用该车辆</p>
      </div>
      <div class="bottom2">
        <switch :checked="checked" color="#FF6324" @change="switchClcik" />
      </div>
    </div>-->
    <div class="position">
      <van-button round size="large" color="#FF6324" type="default" @click="save">保存</van-button>
    </div>
  </div>
</template>
<script>
import { EditUserCar } from '../../api'
import eventBus from '../../utils/eventBus.js'
export default {
  data() {
    return {
      checked: true, // 默认车辆变量
      showDis: false, // 发动机弹窗变量
      currentDate1: '请选择', // 开始日期
      endDate1: '请选择', // 结束日期
      maxDate: new Date().getTime(), // 可选时间的最大值
      minDate: new Date().getTime(), // 可选时间的最小值
      startnum: false, // 开始日期显示变量
      endnum: false, // 结束日期显示变量
      carInfo: {},
      insuranceComany: '请选择',
      imgTitle: ''
    }
  },
  mounted() {
    this.carInfo = JSON.parse(this.$route.query.item)
    this.endDate1 = JSON.parse(this.$route.query.item).insureExpireDate
    this.insuranceComany = JSON.parse(this.$route.query.item).insuranceCompany
    eventBus.$on('editCar', item => {
      console.log(99998, item)
      this.carInfo.BrandShow = item.BrandShow
      this.carInfo.brand = item.brand
      this.carInfo.displacement = item.displacement
      this.carInfo.tid = item.tid
      this.carInfo.vehicle = item.vehicle
      this.carInfo.vehicleId = item.vehicleId
      this.carInfo.vehicleSeries = item.vehicleSeries
      this.carInfo.nian = item.year
      this.carInfo.salesName = item.salesName
      this.endDate1 = item.insureExpireDate
      this.insuranceComany = item.insuranceCompany
    })
    eventBus.$on('selectCompany', item => {
      this.insuranceComany = item
    })
    console.log(9999, this.$route.query)
  },
  methods: {
    toAddCar() {
      this.globalData.isEditCar = true
      this.$router.push('/pages/carBrand/main')
    },
    toAddInsure() {
      this.$router.push('/pages/chooseInsure/main')
    },
    save() {
      if (this.endDate1 === '请选择') {
        wx.showModal({
          content: '请选择保险到期日期',
          confirmText: '确定'
        })
        return false
      }
      let reg = /^\d{1,}$/
      let pattern = new RegExp(reg)
      if (
        this.carInfo.totalMileage != '' &&
        !pattern.test(this.carInfo.totalMileage)
      ) {
        wx.showModal({
          content: '请输入纯数字公里数',
          confirmText: '确定'
        })
        return false
      }
      console.log(7788, this.insuranceComany)
      if (this.isVehicleNumber(this.carInfo.carNumber)) {
        let carInfo = {
          // insuranceCompany: this.insuranceCompany,
          insuranceCompany: this.insuranceComany,
          carId: this.carInfo.carId,
          carNumber: this.carInfo.carNumber,
          brand: this.carInfo.brand,
          vehicle: this.carInfo.vehicle,
          vehicleId: this.carInfo.vehicleId,
          nian: this.carInfo.nian,
          tid: this.carInfo.tid,
          salesName: this.carInfo.salesName,
          // buyYear: 'string',
          // buyMonth: 'string',
          insureExpireDate: this.endDate1,
          totalMileage: this.carInfo.totalMileage,
          // lastBaoYangKm: 0,
          // lastBaoYangTime: '2020-02-03T09:22:47.684Z',
          vinCode: this.carInfo.vinCode,
          defaultCar: false,
          engineNo: this.carInfo.engineNo
          // tireSizeForSingle: 'string',

          // registrationTime: '2020-02-03T09:22:47.684Z',
          // carNoProvince: 'string',
          // carNoCity: 'string',
          // carProperties: [
          //   {
          //     propertyKey: 'string',
          //     propertyValue: 'string'
          //   }
          // ]
        }
        this.$store.dispatch('editUserCar', carInfo).then(() => {
          eventBus.$emit('changeCar')
          wx.navigateBack({})
        })
        // EditUserCar(carInfo).then(res => {
        //   wx.navigateBack({
        //     delta: 1
        //   })
        // })
      } else {
        wx.showModal({
          title: '提示',
          content: '您输入的车牌不符合规则,请重新输入',
          showCancel: false
        })
      }
    },
    isVehicleNumber(vehicleNumber) {
      const xreg = /^[京沪浙苏粤鲁晋冀豫川渝辽吉黑皖鄂湘赣闽陕甘宁蒙津贵云桂琼青新藏港澳]{1}[A-Z]{1}[A-Z0-9]{4,5}[A-Z0-9挂学警港澳领]{1}$/
      return xreg.test(vehicleNumber)
    },
    // 默认车辆t
    switchClcik() {
      this.checked = !this.checked
      if (this.checked) {
      } else {
      }
    },
    // 显示发动机
    showimg(title) {
      this.imgTitle = title
      this.showDis = true
    },
    // 时间戳转换时间
    formatDateTime(inputTime) {
      var date = new Date(inputTime)
      var y = date.getFullYear()
      var m = date.getMonth() + 1
      m = m < 10 ? '0' + m : m
      var d = date.getDate()
      d = d < 10 ? '0' + d : d
      return y + '-' + m + '-' + d
    },
    // 开始日期确定按钮事件
    sureText(event) {
      let date = this.formatDateTime(event.mp.detail)
      this.startnum = false
      this.currentDate = date
      this.currentDate1 = this.currentDate
    },
    // 开始日期取消按钮事件
    clearText() {
      this.startnum = false
    },
    // 结束日期确定按钮事件
    endsureText(event) {
      let date = this.formatDateTime(event.mp.detail)
      this.endnum = false
      this.endDate = date
      this.endDate1 = this.endDate
    },
    // 结束日期取消按钮事件
    endclearText() {
      this.endnum = false
    },
    // 车辆发动机弹窗取消事件
    disPopupClick() {
      this.showDis = false
    },
    // 保险开始日期
    starttimeSelect() {
      this.startnum = !this.startnum
    },
    // 保险结束日期
    EndtimeSelect() {
      this.endnum = !this.endnum
    }
  },
  onUnload: function() {
    this.startnum = false
    this.endnum = false
  }
}
</script>
<style scoped lang="scss">
.p3 {
  width: 382rpx;
  display: flex;
  justify-content: flex-end !important;
}
.carPopup {
  position: fixed;
  top: 0;
  left: 0;
  z-index: 99999;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.4);
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  .box_Popup {
    width: 635rpx;
    // height:760rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 14rpx;
    display: flex;
    flex-direction: column;
    .top_title {
      width: 100%;
      height: 90rpx;
      background: url(https://m.aerp.com.cn/mini-RG-main/goodsdetail_title_bg.png)
        no-repeat;
      background-size: cover;
      border-radius: 14rpx 14rpx 0rpx 1rpx;
      text-align: center;
      line-height: 90rpx;
      font-size: 46rpx;
      font-family: Source Han Sans CN;
      font-weight: bold;
      color: rgba(255, 255, 255, 1);
    }
    .all_popup {
      width: 100%;
      height: 418rpx;
      display: flex;
      flex-direction: column;

      align-items: center;
      box-sizing: border-box;
      .img_card_img {
        width: 475rpx;
        height: 304rpx;
        margin-top: 56rpx;
      }
      .all_zhushi {
        width: 100%;
        text-align: center;
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(173, 173, 173, 1);
        margin-top: 56rpx;
      }
    }
  }
  .ximg {
    width: 63rpx;
    height: 63rpx;
    margin-top: 40rpx;
  }
}
.row {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  background: #ffffff;
  padding: 0 30rpx;
  box-sizing: border-box;
  border-bottom: 1rpx solid #f3f3f3;
}
.demo_page {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #f3f3f3;
}
.top_title {
  width: 100%;
  height: 100rpx;
}
.top_title .p1 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(102, 102, 102, 1);
}
.top_title .p2 {
  // width: 100rpx;
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}
.top_bottom {
  width: 750rpx;
  height: 140rpx;
  background: rgba(255, 255, 255, 1);
  margin: 16rpx 0 139rpx 0;
}
.bottom1 .p1 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}
.bottom1 .p2 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
  margin-top: 20rpx;
}
.img_img {
  width: 34rpx;
  height: 34rpx;
  margin-left: 47rpx;
}
.top_input {
  width: 200rpx;
  text-align: right;
}
.chepai_img {
  width: 24rpx;
  height: 24rpx;
}
.position {
  width: 100%;
  background: #ffffff;
  padding: 10rpx 30rpx;
  box-sizing: border-box;
  position: fixed;
  bottom: 0;
  left: 0;
}
.bottomDate {
  position: fixed;
  bottom: 0;
  left: 0;
  width: 750rpx;
  z-index: 999;
}
</style>
