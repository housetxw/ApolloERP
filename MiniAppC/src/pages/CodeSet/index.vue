<template>
  <div class="demo_page" :style="{minHeight:windowHeight}">
    <div class="section_code">
      <div class="section_code_top">
        <p class="txt1">{{name}}</p>
      </div>
      <p class="section_code_data">有效期至：{{endValidTime}}</p>
      <div class="hxmList" v-for="(item,index) in orderVerificationCodeBaseInfos" :key="index">
        <div class="ermcode">
          <img :src="item.qrCodeBase64String" alt />
        </div>
        <div
          :class="['ewmid',{'text_decoration':item.status == 2 || item.status == 1}]"
        >ID:{{item.code}}</div>
        <!-- 核销码状图图 -->
        <img
          src="https://m.aerp.com.cn/mini-app-main/Label used.png"
          alt
          class="hxmStatus"
          v-if="item.status == 1"
        />
        <!-- 已使用 -->
        <img
          src="https://m.aerp.com.cn/mini-app-main/Expired.png"
          alt
          class="hxmStatus"
          v-if="item.status == 2"
        />
        <!-- 已过期 -->
      </div>

      <!-- <p class="section_code_p1">
        <text class="txt1">使用时间：</text>
        <text class="txt2">{{userdata}}</text>
      </p>-->
      <p class="section_code_p1">
        <text class="txt1">适用范围：</text>
        <text class="txt2">{{userdataA}}</text>
      </p>
      <div class="section_code_p1" style="display: flex;align-items: center;flex-direction: row;">
        <p class="txt1">适用门店：</p>
        <p class="txt2">
          门店列表
          <text @tap="seeClick">点击查看</text>
        </p>
      </div>
      <div class="section_code_p2">
        <text class="txt1" style="width:320rpx;minHeight:120rpx;">适用规则：</text>
        <text class="txt2" style="minHeight:120rpx;">{{userdataC}}</text>
      </div>
    </div>
    <div class="footer" @tap="clickBack">
      <p>返回</p>
    </div>
  </div>
</template>
<script>
import { GetCode } from '../../api'
export default {
  data() {
    return {
      windowHeight: '',
      name: '', //核销码名字
      // status:0,//核销码使用状态
      // ewmid:'',//二维码id
      userdata: '', //使用时间
      // hxmUrl:'https://m.aerp.com.cn/mini-app-main/Label used.png',//核销码图片路径
      userdataA: '', //范围
      userdataB: '', //门店
      userdataC: '', //规则
      orderNo: '', //订单号
      endValidTime: '', //有效期
      orderVerificationCodeBaseInfos: [] //核销码集合
    }
  },
  methods: {
    // 获取订单核销码集合
    getOrderList() {
      let that = this
      let rquest = {
        OrderNo: that.orderNo
      }
      GetCode(rquest)
        .then(res => {
          that.orderVerificationCodeBaseInfos =
            res.data.orderVerificationCodeBaseInfos
          that.name = res.data.verificationRuleInfo.displayCodeName
          that.userdataA = res.data.verificationRuleInfo.applicableRange
          that.userdataC = res.data.verificationRuleInfo.ruleDesc
          that.endValidTime = res.data.verificationRuleInfo.endValidTime
        })
        .catch(err => {})
    },
    // 查看更多
    seeMoreClick() {},
    // 点击查看
    seeClick() {
      this.$router.push('/pages/stores/main')
    },
    clickBack() {
      this.$router.back()
    }
  },
  mounted() {
    this.getOrderList()
  },
  onLoad(options) {
    let that = this
    that.orderNo = options.orderNo
    // 获取系统信息
    wx.getSystemInfo({
      success: function(res) {
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
  }
}
</script>
<style scoped lang="scss">
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #f3f3f3;
  padding-bottom: 100rpx;
  box-sizing: border-box;
}
.footer {
  position: fixed;
  bottom: 0;
  left: 0;
  width: 100%;
  padding: 15rpx 0;
  box-sizing: border-box;
  background: #ffffff;
  p {
    width: 662rpx;
    height: 75rpx;
    text-align: center;
    line-height: 75rpx;
    background: rgba(255, 99, 36, 1);
    border-radius: 38rpx;
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(255, 255, 255, 1);
    margin: 0 auto;
  }
}
// 核销码
.section_code {
  width: 690rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  padding: 20rpx 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  margin: 16rpx 0;
  .section_code_top {
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .txt1 {
      font-size: 32rpx;
      font-family: Source Han Sans CN;
      font-weight: bold;
      color: rgba(51, 51, 51, 1);
    }
  }
  .section_code_data {
    font-size: 28rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(136, 136, 136, 1);
    margin-top: 40rpx;
  }
  .ermcode {
    width: 100%;
    height: 370rpx;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    img {
      width: 332rpx;
      height: 332rpx;
    }
  }
  .ewmid {
    width: 578rpx;
    height: 80rpx;
    border: 2rpx solid rgba(204, 204, 204, 1);
    border-radius: 10rpx;
    text-align: center;
    line-height: 76rpx;
    margin: 0 auto;
    margin-bottom: 36rpx;
  }
  .text_decoration {
    text-decoration: line-through;
    color: #cccccc;
  }
  .section_code_p1 {
    width: 100%;
    height: 60rpx;
    .txt1 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(136, 136, 136, 1);
    }
    .txt2 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
      margin-left: 40rpx;
      text {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 99, 36, 1);
        margin-left: 10rpx;
      }
    }
  }
  .section_code_p2 {
    display: flex;
    flex-direction: row;
    align-items: flex-start;
    margin-top: 15rpx;
    .txt1 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(136, 136, 136, 1);
    }
    .txt2 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
      margin-left: 40rpx;
    }
  }
}
.hxmStatus {
  position: absolute;
  top: 0rpx;
  right: 93rpx;
  width: 120rpx;
  height: 120rpx;
}
.hxmList {
  position: relative;
  width: 100%;
  display: flex;
  flex-direction: column;
}
</style>
