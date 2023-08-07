<template>
  <div class="demo_page" :style="{minHeight:windowHeight }">
    <div class="main">
      <div class="address" v-if="addressArr.length>0">
        <div v-for="(items,indexs) in addressArr" :key="indexs">
          <van-swipe-cell id="swipe-cell" right-width="140" @close="onClose">
            <div class="top_one">
              <div class="top_content" @click="selectAddress(items)">
                <div class="p1">
                  <text class="txt1">{{items.userName}}</text>
                  <text class="txt2">{{items.mobileNumber}}</text>
                  <text :class="[{'distxt1':items.defaultAddress}]">{{items.defaultAddress?'默认':''}}</text>
                  <text class="txt3">{{items.addressTag}}</text>
                </div>
                <p
                  class="p3"
                >{{items.province}}{{items.district}}{{items.city}}{{items.addressLine}}</p>
              </div>
              <div class="top_images2" @tap="editClcik(items)">
                <img :src="useraddress" alt class="img2" />
              </div>
            </div>
            <div slot="right" class="clear">
              <text @tap="moAddress(items)">设为默认</text>
              <text @tap="delAddress(items)">删除</text>
            </div>
          </van-swipe-cell>
        </div>
      </div>
      <!-- 无地址内容显示东西 -->
      <div class="else" v-else :style="{height:windowHeight}">
        <img :src="src2" alt mode="widthFix" class="elseimg" />
        <p
          style="font-size:32rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(136,136,136,1);"
        >暂无服务地址</p>
      </div>
    </div>
    <div class="position" @tap="addressClick">
      <van-button round size="large" color="#FF6324" type="default">＋ 新增联系人</van-button>
    </div>
  </div>
</template>
<script>
import { GetUserAddres, DeleteUserAddress, SetDefaultAddress } from '../../api'

export default {
  data() {
    return {
      addressArr: [], // 用户地址
      useraddress: `${this.globalData.imgPubUrl}order_edit_icon.png`,
      windowHeight: '',
      src2: `${this.globalData.imgPubUrl}mine_appointment_null.png` // 暂无地址图片
    }
  },
  methods: {
    selectAddress(item) {
      if (this.$route.query.isFromPerson) {
        return false
      }
      this.globalData.homeAddress = item
      wx.navigateBack({
        delta: 1
      })
    },
    // 新增收货地址
    addressClick() {
      this.$router.push({ path: '/pages/newAddPeople/main', query: { id: 1 } })
    },
    // 设为默认收货地址
    moAddress(items) {
      let that = this
      SetDefaultAddress({ addressId: items.addressId })
        .then(res => {
          GetUserAddres('')
            .then(res => {
              //  (`获取用户地址:${JSON.stringify(res)}`)
              that.addressArr = res.data
            })
            .catch(err => {})
        })
        .catch(err => {})
    },
    // 删除收货地址
    delAddress(items) {
      let that = this
      DeleteUserAddress({ addressId: items.addressId })
        .then(res => {
          GetUserAddres('')
            .then(res => {
              that.addressArr = res.data
            })
            .catch(err => {})
        })
        .catch(err => {})
    },

    // 编辑用户地址
    editClcik(items) {
      this.$router.push({
        path: '/pages/newAddPeople/main',

        query: {
          addressId: items.addressId,
          userName: items.userName,
          mobileNumber: items.mobileNumber,
          province: items.province,
          city: items.city,
          district: items.district,
          addressLine: items.addressLine,
          addressTag: items.addressTag,
          defaultAddress: items.defaultAddress
        }
      })
    }
  },
  onShow() {
    let that = this
    GetUserAddres('')
      .then(res => {
        that.addressArr = res.data
      })
      .catch(err => {})
  },
  onLoad() {
    let that = this
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
  },
  mounted() {
    let that = this
    // 获取系统信息
    wx.getSystemInfo({
      success: function(res) {
        let clientHeight = res.windowHeight
        let clientWidth = res.windowWidth
        let ratio = 750 / clientWidth
        that.windowHeight = clientHeight * ratio - 88 + 'rpx'
      }
    })
  },
  onUnload() {
    // mpvue.reLaunch({url:'/pages/my/main'})
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
  margin-bottom: 16rpx;
  height: 100%;
}

.address {
  width: 100%;
  margin-bottom: 16rpx;
  height: 100%;
  display: flex;
  flex-direction: column;

  .top_one {
    width: 100%;
    height: 180rpx;
    background: rgba(255, 255, 255, 1);
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    padding: 38rpx 30rpx;
    box-sizing: border-box;
    border-bottom: 1rpx solid #f3f3f3;
    .top_content {
      width: 640rpx;
      height: 104rpx;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-start;
      .p1 {
        display: flex;
        flex-direction: row;
        // justify-content: center;
        align-items: center;
        .txt1 {
          font-size: 30rpx;
          font-family: Source Han Sans CN;
          font-weight: bold;
          color: rgba(51, 51, 51, 1);
        }
        .txt2 {
          font-size: 30rpx;
          font-family: Source Han Sans CN;
          font-weight: bold;
          color: rgba(51, 51, 51, 1);
          margin-left: 100rpx;
        }
        .txt3 {
          width: 66rpx;
          height: 30rpx;
          background: rgba(222, 239, 255, 1);
          border-radius: 4rpx;
          text-align: center;
          line-height: 30rpx;
          font-size: 24rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(68, 164, 254, 1);
          margin-left: 20rpx;
        }
      }
      .p3 {
        font-size: 26rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
      }
    }
    .top_images2 {
      width: 50rpx;
      height: 104rpx;
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      .img2 {
        width: 34rpx;
        height: 34rpx;
      }
    }
  }
  .clear {
    width: 280rpx;
    height: 180rpx;
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
    // color: #ffffff;
    // background:#F63D3D;
    text:nth-child(1) {
      flex: 1;
      height: 180rpx;
      line-height: 180rpx;
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(136, 136, 136, 1);
      background: rgba(235, 235, 235, 1);
      text-align: center;
    }
    text:nth-child(2) {
      flex: 1;
      height: 180rpx;
      line-height: 180rpx;
      background: rgba(246, 61, 61, 1);
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(255, 255, 255, 1);
      text-align: center;
    }
  }
}
.position {
  position: fixed;
  bottom: 0;
  left: 0;
  width: 100%;
  background: #ffffff;
  padding: 10rpx 30rpx;
  box-sizing: border-box;
}

.distxt1 {
  width: 80rpx;
  height: 30rpx;
  background: rgba(255, 238, 231, 1);
  border-radius: 4rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
  margin-left: 20rpx;
  text-align: center;
}
.else {
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