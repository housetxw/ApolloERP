<template>
  <div class="page">
    <div class="subTitle">车辆信息</div>
    <div class="top_card">
      <div class="card_tab" @tap="changeCard">
        <img :src="carArrFirst.brandUrl" alt class="img_card" v-if="carArrFirst.vehicle" />
        <div class="card_content" v-if="carArrFirst.vehicle">
          <p class="p1">{{carArrFirst.vehicle}}</p>
          <p class="p2">{{carArrFirst.carNumber}}</p>
        </div>
        <div class="card_add" @tap="changeCard" v-if="!carArrFirst.vehicle">请先选择车辆</div>
        <img
          src="https://m.aerp.com.cn/mini-RG-main/maintenance_jump_icon.png"
          alt
          style="width:24rpx;height:24rpx"
        />
      </div>
    </div>
    <div class="subTitle">消费历史</div>
    <div class="history" v-if="consumptionHistory.arriveShopSumCount">
      <div class="historyItem">到店总计：{{consumptionHistory.arriveShopSumCount}}次</div>
      <div class="historyItem">消费总计：{{consumptionHistory.totalConsumption}}</div>
      <div class="historyItem">最后到店时间：{{consumptionHistory.lastArriveShopTime}}</div>
      <div class="historyItem">最后公里数：{{consumptionHistory.lastKilometres}}</div>
    </div>
    <div class="NoStatus NoStatus1" v-else>
      <img src="http://m.aerp.com.cn/mini-RG-main/servermessage.png" alt class="NoStatusImg1" />
      <div class="NoStatusWord">暂无历史信息</div>
    </div>
    <div class="subTitle">维修保养记录</div>
    <div class="recordItem" v-for="(item,index) in maintenanceRecordHistory" :key="index">
      <img :src="item.icon" alt class="ItemIcon" />
      <div class="ItemR">
        <div class="ItemRTop">
          <div class="ItemRTopLeft">
            <span class="itemTitle">{{item.titleName}}</span>
            <span class="itemTime">{{item.arriveDate}}</span>
          </div>
          <span class="itemDistance"></span>
        </div>
        <div class="itemBodyItem1">
          <div class="itemBodyItemLeft">
            <img
              src="http://m.aerp.com.cn/mini-RG-main/appointment_project_icon.png"
              alt
              class="itemBodyIcon"
            />
            <span class="itemBodyItemKey">服务名称：</span>
            <span class="itemBodyItemValue">{{item.serviceName}}</span>
          </div>
          <!-- <span class="itemBodyItemPrice">￥650.00</span> -->
        </div>
        <div class="itemBodyItem">
          <img
            src="http://m.aerp.com.cn/mini-RG-main/appointment_store_icon.png"
            alt
            class="itemBodyIcon"
          />
          <span class="itemBodyItemKey">预约门店：</span>
          <span class="itemBodyItemValue">{{item.serviceShop}}</span>
        </div>
        <div class="itemBodyItem">
          <img
            src="http://m.aerp.com.cn/mini-RG-main/appointment_time_icon.png"
            alt
            class="itemBodyIcon"
          />
          <span class="itemBodyItemKey">预约编号：</span>
          <span class="itemBodyItemValue">{{item.reserveNumber}}</span>
        </div>
        <div class="itemBodyItem">
          <img
            src="http://m.aerp.com.cn/mini-RG-main/appointment_number_icon.png"
            alt
            class="itemBodyIcon"
          />
          <span class="itemBodyItemKey">排队编号：</span>
          <span class="itemBodyItemValue">{{item.queueNumber}}</span>
        </div>
        <div class="itemBodyItem">
          <img
            src="http://m.aerp.com.cn/mini-RG-main/appointment_car_icon.png"
            alt
            class="itemBodyIcon"
          />
          <span class="itemBodyItemKey">服务技师：</span>
          <span class="itemBodyItemValue">{{item.serviceTechnician}}</span>
        </div>
        <div class="mark">备注：</div>
        <div class="markContent">{{item.remark}}</div>
      </div>
    </div>
    <div class="NoStatus" v-if="maintenanceRecordHistory.length == 0">
      <img src="http://m.aerp.com.cn/mini-RG-main/servercar.png" alt class="NoStatusImg" />
      <div class="NoStatusWord">暂无维修保养记录</div>
    </div>
  </div>
</template>

<script>
import eventBus from '../../utils/eventBus.js'

import { GetServiceRecord } from '../../api'
export default {
  data() {
    return {
      consumptionHistory: {},
      maintenanceRecordHistory: []
    }
  },
  mounted() {
    this.selectCar = this.$route.query
  },
  computed: {
    carArrFirst() {
      return this.$store.state.carArr[0] || {}
    },
    cardID() {
      if (this.$store.state.carArr[0]) {
        return this.$store.state.carArr[0].carId
      } else {
        return ''
      }
    }
  },
  methods: {
    // 换车
    changeCard() {
      this.$router.push('/pages/myCard/main')
    },
    getServiceRecord() {
      if (this.cardID) {
        GetServiceRecord({ CarId: this.cardID }).then(res => {
          this.consumptionHistory = res.data.consumptionHistory
          this.maintenanceRecordHistory = res.data.maintenanceRecordHistory
        })
      }
    }
  },
  onShow() {
    this.getServiceRecord()
  }
}
</script>

<style scoped lang="scss">
.NoStatus {
  width: 691rpx;
  height: 294rpx;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  background: rgba(255, 255, 255, 1);
  .NoStatusImg {
    width: 236rpx;
    height: 110rpx;
  }
  .NoStatusImg1 {
    width: 167rpx;
    height: 149rpx;
  }
  .NoStatusWord {
    height: 25rpx;
    font-size: 26rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(153, 153, 153, 1);
    line-height: 42px;
  }
}
.NoStatus1 {
  height: 391rpx;
}
.itemBodyItemValue {
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(33, 33, 33, 1);
}
.itemBodyItemPrice {
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 0, 0, 1);
}
img {
  width: 28rpx;
  height: 28rpx;
}
.recordItem {
  display: flex;
  box-sizing: border-box;
  padding: 30rpx 30rpx 30rpx 30rpx;
  background: #fff;
  width: 100%;
  margin-bottom: 20rpx;
  .ItemIcon {
    display: flex;
    width: 40rpx;
    height: 40rpx;
    border-radius: 4rpx;
    overflow: hidden;
    margin-right: 30rpx;
    margin-top: 10rpx;
  }
  .ItemR {
    flex: 1;
    .ItemRTop {
      display: flex;
      justify-content: space-between;
      align-items: flex-end;
      margin-bottom: 41rpx;
      .itemTitle {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 500;
        color: rgba(51, 51, 51, 1);
      }
      .itemTime {
        font-size: 26rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(153, 153, 153, 1);
        margin-left: 37rpx;
      }
      .itemDistance {
        font-size: 22rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(153, 153, 153, 1);
      }
    }
    .itemBodyItem {
      display: flex;
      align-items: center;
      margin-bottom: 28rpx;
      .itemBodyItemKey {
        font-size: 26rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(136, 136, 136, 1);
        margin-left: 16rpx;
        margin-right: 27rpx;
      }
    }
    .itemBodyItem1 {
      display: flex;
      align-items: center;
      justify-content: space-between;
      margin-bottom: 28rpx;
      .itemBodyItemLeft {
        display: flex;
        align-items: center;
        .itemBodyItemKey {
          font-size: 26rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(136, 136, 136, 1);
          margin-left: 16rpx;
          margin-right: 27rpx;
        }
      }
    }
    .mark {
      font-size: 26rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
      margin-bottom: 27rpx;
    }
    .markContent {
      font-size: 26rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(153, 153, 153, 1);
    }
  }
}
.history {
  background: #fff;
  .historyItem {
    width: 720rpx;
    height: 100rpx;
    line-height: 100rpx;
    padding-left: 30rpx;
    font-size: 28rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(51, 51, 51, 1);
  }
}
.subTitle {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(51, 51, 51, 1);
  height: 79rpx;
  line-height: 79rpx;
  width: 720rpx;
  padding-left: 30rpx;
}
.page {
  display: flex;
  flex-direction: column;
  align-items: center;
  // height: 100%;
  background: #f3f3f3;
}
.top_card {
  width: 100%;
  // height: 320rpx;
  display: flex;
  flex-direction: column;
  // justify-content: center;
  align-items: center;
  background: rgba(255, 255, 255, 1);
  .card_tab {
    width: 100%;
    height: 160rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 10rpx;
    padding: 20rpx 28rpx 25rpx 28rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    border-bottom: 1rpx solid #f3f3f3;
    // margin-top: 16rpx;
    .img_card {
      width: 100rpx;
      height: 100rpx;
    }
    .card_content {
      width: 486rpx;
      height: 125rpx;
      padding: 20rpx 0;
      box-sizing: border-box;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-start;
      .p1 {
        width: 100%;
        height: 80rpx;
        font-size: 28rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
        overflow: hidden;
        white-space: pre-wrap;
        display: -webkit-box;
        text-overflow: ellipsis;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
      }
      .p2 {
        width: 100%;
        height: 50rpx;
        font-size: 24rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(136, 136, 136, 1);
        overflow: hidden;
        white-space: pre-wrap;
        display: -webkit-box;
        text-overflow: ellipsis;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
      }
    }
  }
  .card_add {
    width: 100%;
    height: 100rpx;
    // border: 2rpx solid #f3f3f3;
    line-height: 96rpx;
    background: rgba(255, 255, 255, 1);
    font-size: 30rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(51, 51, 51, 1);
    text-align: center;
  }
  .card_store {
    width: 100%;
    height: 180rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 10rpx;
    padding: 10rpx 28rpx 10rpx 28rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .img_card {
      width: 100rpx;
      height: 100rpx;
    }
    .card_content {
      width: 486rpx;
      // height: 125rpx;
      // padding: 20rpx 0;
      box-sizing: border-box;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-start;
      .p1_p1 {
        width: 454rpx;
        height: 83rpx;
        font-size: 28rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
        overflow: hidden;
        white-space: pre-wrap;
        display: -webkit-box;
        text-overflow: ellipsis;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
      }
      .p2_p2 {
        width: 454rpx;
        height: 69rpx;
        font-size: 24rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(136, 136, 136, 1);
        overflow: hidden;
        white-space: pre-wrap;
        display: -webkit-box;
        text-overflow: ellipsis;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
      }
    }
  }
  .store_add {
    width: 100%;
    height: 100rpx;
    border: 2rpx solid #f3f3f3;
    line-height: 96rpx;
    background: rgba(255, 255, 255, 1);
    font-size: 30rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(51, 51, 51, 1);
    text-align: center;
  }
}
</style>

