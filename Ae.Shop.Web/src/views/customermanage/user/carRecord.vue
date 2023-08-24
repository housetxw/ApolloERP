<template>
  <div class="page">
    <div class="top_title"></div>
    <div class="activeList">
      <div class="cars">
        <img :src="car.brandUrl" class="carimg" />
        <div class="carDes">
          <p class="carnumber">{{car.carNumber}}</p>
          <p class="carname">{{car.vehicle}}</p>
        </div>
      </div>

      <!-- </navigator> -->

      <!-- <img src="http://pyguln7uf.bkt.clouddn.com/mini-app-main/activity_img.png" alt="" class="activeItem"> -->
    </div>
    <div class="topTitle" @click="addmore">
      <p class="titles">基本状况</p>
      <img src="https://m.aerp.com.cn/mini-app-main/more.png" alt class="moreimg" v-if="showmore" />
      <img src="https://m.aerp.com.cn/mini-app-main/toright.png" alt class="moreimg" v-else />
    </div>
    <div class="content" v-if="showmore">
      <div class="main">
        <div class="condition" v-if="mainData.length>0">
          <p class="name">最后到店时间</p>
          <p class="time">{{mainData.lastInTime}}</p>
        </div>
        <div class="condition">
          <p class="name">总里程数</p>
          <p class="time">{{mainData.mileage}}公里</p>
        </div>
        <div class="condition">
          <p class="name">到店总计</p>
          <p class="time">{{mainData.receiveCount}}次</p>
        </div>
        <div class="condition">
          <p class="name">消费总计</p>
          <p class="time">￥{{mainData.totalMoney}}</p>
        </div>
        <div class="condition">
          <p class="name">保险公司</p>
          <p class="time">{{mainData.insuranceCompany}}</p>
        </div>
        <div class="condition">
          <p class="name">保险到期日</p>
          <p class="time">{{mainData.insureExpireDate}}</p>
        </div>
      </div>
    </div>
    <div class="topTitle" style="margin-top:0" @click="addmore1">
      <p class="titles">车辆部件状况</p>
      <img src="https://m.aerp.com.cn/mini-app-main/green.png" alt class="moreimg" />
      <span class="font24">正常</span>
      <img src="https://m.aerp.com.cn/mini-app-main/danger.png" alt class="moreimg" />
      <span class="font24">有过异常、维修</span>
      <img src="https://m.aerp.com.cn/mini-app-main/more.png" alt class="moreimg" v-if="showmore1" />
      <img src="https://m.aerp.com.cn/mini-app-main/toright.png" alt class="moreimg" v-else />
    </div>
    <div v-show="showmore1">
      <div class="content" v-if="carPartsSituation.length>0">
        <div class="main" v-for="(item,index) in carPartsSituation" :key="index">
          <p class="title1">{{item.title}}</p>
          <div class="buttons">
            <button
              class="button"
              @click="toDetail(itemm.keyName,itemm.displayName)"
              v-for="(itemm,indexx) in item.items"
              :key="indexx"
              :disabled="itemm.status==0"
            >
              <img
                src="https://m.aerp.com.cn/mini-app-main/green.png"
                alt
                class="moreimg"
                v-if="itemm.status==0"
              />
              <img
                src="https://m.aerp.com.cn/mini-app-main/danger.png"
                alt
                class="moreimg"
                v-if="itemm.status==1"
              />
              <span class="part">{{itemm.displayName}}</span>
            </button>
            <!-- <div class="button">
              <img src="https://m.aerp.com.cn/mini-app-main/danger.png" alt class="moreimg" />
              <span class="part">制动系统</span>
            </div>
            <div class="button">
              <img src="https://m.aerp.com.cn/mini-app-main/danger.png" alt class="moreimg" />
              <span class="part">全车喷漆</span>
            </div>-->
          </div>
        </div>
      </div>
    </div>
    <div class="topTitle" style="margin-top:0" @click="addmore2">
      <p class="titles">维修保养记录</p>
      <img src="https://m.aerp.com.cn/mini-app-main/more.png" alt class="moreimg" v-if="showmore2" />
      <img src="https://m.aerp.com.cn/mini-app-main/toright.png" alt class="moreimg" v-else />
    </div>
    <div>
      <div class="content1" v-show="showmore2">
        <van-tabs
          :active="active"
          color="#FF6214"
          class="tabstyle"
          v-if="serviceCategory.length>0"
          @click="changeTab"
        >
          <!-- <van-tabs :active="active" sticky swipeable color="#FF6324" line-width="40" class="tabs" @change="onChangeTitle"> -->
          <van-tab
            :title="itemm.displayName+'('+itemm.count+')'"
            v-for="(itemm,indexx) in serviceCategory"
            :key="indexx"
            
          >
            <div style="padding:30px;width:100%;background:#fff;box-sizing:border-box;">
              <div class="tabContent" v-if="serviceRecord.length>0">
                <div class="tabBorder" v-for="(item1,index1) in serviceRecord" :key="index1">
                  <img src="https://m.aerp.com.cn/mini-app-main/juxing.png" alt class="borderimg" />
                  <p class="tabTitle">
                    <span class="s1">{{item1.receiveTime}}</span>
                    <span
                      class="s2"
                      v-for="(item3,index3) in item1.tags"
                      :key="index3"
                    >{{item3.tag}}</span>
                    <span class="s3">{{item1.shopName}}</span>

                    <span class="s4" v-if="item1.mileage>0">{{item1.mileage}}公里</span>
                  </p>
                  <div class="tabparts main">
                    <p class="projects" v-for="(item2,index2) in item1.projects" :key="index2">
                      <span class="p1">{{item2.displayName}}</span>

                      <span class="p2">x{{item2.count}}</span>
                      <span class="p3">{{item2.techName}}</span>
                      <span class="p4">¥{{item2.totalMoney}}</span>
                    </p>
                  </div>
                </div>
              </div>
              <div class="else1" v-else>
                <img :src="src2" alt class="elseimg" />
                <p
                  style="font-size:28px;font-family:Source Han Sans CN;font-weight:400;color:rgba(136,136,136,1);"
                >您的保养记录为空</p>
              </div>
            </div>
          </van-tab>
        </van-tabs>
      </div>
    </div>
  </div>
</template>

<script>
import {
  GetUserVehicleFile,
  GetMaintenancerecord
} from '@/api/customermanage/user'
import { mapGetters } from 'vuex'
;(function(doc, win) {
  var docEl = doc.documentElement,
    resizeEvt = 'orientationchange' in window ? 'orientationchange' : 'resize',
    recalc = function() {
      var clientWidth = docEl.clientWidth
      if (!clientWidth) return
      if (clientWidth >= 750) clientWidth = 750
      if (clientWidth <= 320) clientWidth = 320
      docEl.style.fontSize = 625 * (clientWidth / 750) + '%'
    }
  if (!doc.addEventListener) return
  win.addEventListener(resizeEvt, recalc, false)
  doc.addEventListener('DOMContentLoaded', recalc, false)
})(document, window)
export default {
  data() {
    return {
      serviceCode: '',
      serviceRecord: [],
      currentCar: 0,
      carArr: [],
      car: {},
      carId: 0,
      serviceCategory: [],
      maintenancerecor: [],
      displayName: '',
      keyName: '',
      disabled: true,
      carPartsSituation: [],
      mainData: {},
      active: 0,
      topIndex: 0,
      showmore: true,
      showmore1: true,
      showmore2: true,
      src2: 'https://m.aerp.com.cn/mini-app-main/mine_appointment_null.png',

      tabList: [' 全部', '美容', '保养', '轮胎', '钣喷', '改装', '其他'],
      imgUrls2: [
        {
          advertisingCode: 'ProminentActive1',
          imageUrl: 'https://m.aerp.com.cn/mini-app-main/newbanner.png',
          routeUrl: ''
        },
        {
          advertisingCode: 'ProminentActive1',
          imageUrl: 'https://m.aerp.com.cn/mini-app-main/newbanner.png',
          routeUrl: ''
        }
      ]
    }
  },
  mounted() {
    // this.carArr = this.$store.state.carArr
    console.log(8888, this.$router)
    this.carId = this.$route.query.carId
    this.car = this.$route.query

    this.GetUserVehicleFile()
    this.serviceCode = 'None'
    this.GetMaintenancerecord()
  },
  computed: {
    ...mapGetters(['userInfo'])
  },
  methods: {
    switchItem(res) {
      console.log(111, res)
      let oIndex = res.mp.detail.current
      this.currentCar = oIndex
      this.GetUserVehicleFile()
      this.GetMaintenancerecord()
    },
    GetUserVehicleFile() {
      // console.log(121, that.carArr[that.currentCar].carId)
      let that = this
      let rquest = {
        CarId: that.carId,
        userId: that.car.userId
      }
      GetUserVehicleFile(rquest)
        .then(res => {
          let arr = res.data
          that.allCondition = arr
          that.mainData = arr.mainData
          that.carPartsSituation = arr.carPartsSituation
          that.maintenancerecord = arr.maintenancerecord
          that.serviceCategory = that.maintenancerecord.serviceCategory
          console.log(121, that.serviceCategory)
          console.log(arr)
        })
        .catch(err => {})
    },
    GetMaintenancerecord() {
      // console.log(121, that.carArr[that.currentCar].carId)
      let that = this
      let rquest = {
        CarId: that.carId,
        serviceCode: that.serviceCode
      }
      GetMaintenancerecord(rquest)
        .then(res => {
          let arr = res.data
          that.serviceRecord = arr.serviceRecord
          console.log('serviceRecord ', that.serviceRecord)
          this.$forceUpdate()
        })
        .catch(err => {})
    },
    addmore() {
      this.showmore = !this.showmore
      console.log(111, this.showmore)
    },
    addmore1() {
      this.showmore1 = !this.showmore1
      console.log(111, this.showmore1)
    },
    addmore2() {
      this.showmore2 = !this.showmore2
      console.log(111, this.showmore1)
    },
    changeTab(name, title) {
      this.topIndex = name
      // this.active = event.target.dataset.code
      this.serviceCode = this.serviceCategory[this.topIndex].serviceCode
      console.log(999, event)
      console.log('topIndex', this.topIndex)
      // console.log('切换到标签', event.mp.detail.name)
      // this.serviceCode = event.mp.detail.name
      this.GetMaintenancerecord()
    },
    toDetail(keyName, displayName) {
      this.keyName = keyName
      this.displayName = displayName
      this.$router.push({
        name: 'abnormal',
        query: {
          keyName: this.keyName,
          CarId: this.carId,
          displayName: this.displayName
        }
      })
    }
  }
}
</script>
<style>
/* page {
  background: rgba(243, 243, 243, 1);
} */
</style>

<style scoped lang="scss">
.font24 {
  font-size: 24px !important;
}
.page {
  padding-bottom: 20px;
  width: 750px;
  height: 100%;
  overflow: scroll;
  position: relative;
  margin: 0 auto;
  background: rgba(243, 243, 243, 1);
}
.top_title {
  width: 100%;
  height: 180px;
  background: rgba(250, 121, 69, 1);
  padding: 30px 30px 0 32px;
  box-sizing: border-box;
  /*display: flex;*/
  /*flex-direction: column;*/
  /*justify-content: space-between;*/
  /*align-items: center;*/

  color: #fff;
  position: relative;
}
.activeList {
  position: absolute;
  top: 50px;
  border-radius: 10px 10px 10px 10px;
  // display: flex;
  // flex-direction: column;
  // justify-content: flex-start;
  // align-items: flex-start;

  left: 30px;
  right: 30px;
  padding: 20px 0;

  box-sizing: border-box;
  background: #ffffff;
  margin-bottom: 16px;
  padding: 20px 20px 20px;
  min-height: 18px;
}
.topTitle {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  padding: 30px;
  width: 100%;
  box-sizing: border-box;
  align-items: center;
  margin-top: 120px;
}
.titles {
  color: #333;
  font: 700 32px/1 'SourceHanSansCN-Medium';
}
.moreimg {
  height: 38px;
  width: 38px;
}
.main {
  width: 100%;
  border-radius: 10px 10px 10px 10px;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  padding: 30px;
  padding-top: 0;
  box-sizing: border-box;
  background: #ffffff;
  margin-bottom: 16px;

  min-height: 10px;
}
.content {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  padding: 16px 30px 0 30px;
  box-sizing: border-box;
}
.condition {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  min-height: 90px;
  border-bottom: 1px solid #e2e2e2;
  width: 100%;
  padding: 30px 0;

  box-sizing: border-box;
  position: relative;
}
.name {
  color: #333;
  font: 28px/1 'SourceHanSansCN-Medium';
}
.time {
  color: #666;
  font: 28px/1 'SourceHanSansCN-Medium';
  position: absolute;
  left: 280px;
}
.title1 {
  color: #333;
  font: 600 30px/1 'SourceHanSansCN-Medium';
  padding: 30px 0;
}
.buttons {
  display: flex;
  justify-content: flex-start;
  width: 100%;
  flex-wrap: wrap;
}
.button {
  min-width: 30%;
  border: 1px solid #999;
  border-radius: 15px;
  min-height: 60px;
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 20px 16px 0 0;
  padding: 0;
  background: #fff !important;
}
.part {
  text-align: center;
  min-width: 120px;
  color: #333;
  font: 26px/1 'SourceHanSansCN-Medium';
  max-width: 160px;
}
.content1 {
  box-sizing: border-box;
  padding: 0 30px;
}

.tabContent {
  width: 100%;
  box-sizing: border-box;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  background: #fff;
}
.tabBorder {
  border-left: 1px solid #ccc;
  width: 100%;
  padding: 20px 30px;
  box-sizing: border-box;
  position: relative;
}
.borderimg {
  height: 32px;
  width: 32px;
  position: absolute;
  left: -16px;
  top: -16px;
}
.tabTitle {
  position: relative;
  width: 100%;
  display: flex;
  align-items: center;
}
.s1 {
  color: #999;
  font: 26px/1 'PingFang-SC-Medium';
}
.s2 {
  color: #333;
  font: 28px/1 'SourceHanSansCN-Medium';
  margin: 0 10px 0 20px;
max-width: 120px;
}
.s3 {
  color: #333;
  font: 28px/1 'SourceHanSansCN-Medium';
  width: 190px;
}
.s4 {
  color: #666;
  font: 26px/1 'SourceHanSansCN-Medium';
  position: absolute;
  right: 0px;
  max-width: 90px;
}
.tabparts {
  background: #f2f2f2;
  margin-top: 20px;
}
.projects {
  //   display: flex;
  // align-items: center;
  // justify-content: space-between;
  position: relative;
  width: 100%;
  padding-top: 20px;
  display: flex;
  align-items: center;
}
.p1 {
  color: #666;
  font: 26px/1 'SourceHanSansCN-Medium';

  max-width: 260px;
}
.p2 {
  color: #999;
  font: 22px/1 'SourceHanSansCN-Medium';

  margin-left: 10px;
}
.p3 {
  color: #666;
  font: 26px/1 'SourceHanSansCN-Medium';

  position: absolute;
  left: 288px;
  max-width: 150px;
}
.p4 {
  color: #ff0000;
  font: 24px/1 'SourceHanSansCN-Medium';
  position: absolute;
  right: 0;
}
.cars {
  display: flex;
  justify-content: flex-start;
  width: 100%;
  align-items: center;
  height: 100%;
}
.carimg {
  width: 86px;
  height: 86px !important;
}
.carDes {
  margin-left: 20px;
  font: 28px/1 'SourceHanSansCN-Medium';
  width: 560px;
}
.carnumber {
  color: #333;
  font: 600 32px/1 'SourceHanSansCN-Medium';

  padding: 20px 0;
}
.carname {
  color: #666;
  font: 28px/1 'SourceHanSansCN-Medium';
}
.else1 {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.elseimg {
  width: 150px;
  height: 150px;
  margin-top: 10px;
}
</style>
