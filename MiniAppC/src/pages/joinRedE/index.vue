<template>
  <div class="main" :style="{minHeight:windowHeight}">
    <div class="demo_page">
      <div class="top_title">
        <p class="top_title_title">联系人信息</p>
        <van-cell-group>
          <van-field
            :value="username"
            required
            clearable
            label="姓名"
            placeholder="用户昵称"
            @change="completeInputnumber"
          />
          <van-field
            :value="password"
            type="number"
            label="手机号"
            placeholder="请输入手机号"
            required
            @change="completeInputduanxin"
          />
        </van-cell-group>
      </div>
      <div class="top_city">
        <p class="top_city_title">所在城市</p>
        <div class="top_city_text">
          <van-field
            :value="addressvalue"
            placeholder="请选择所在城市"
            @change="ChangeAddressvalue"
            @click="showPopup"
            readonly="readonly"
          ></van-field>
          <!-- <p class="txt" @change="ChangeAddressvalue" @click="showPopup">{{city}}</p> -->
          <img
            src="https://m.aerp.com.cn/mini-RG-main/maintenance_jump_icon.png"
            alt
            style="width:24rpx;height:24rpx;"
          />
        </div>
      </div>
      <div class="top_resion">
        <p class="top_resion_title">备注</p>
        <div class="box">
          <!-- <input type="textarea"  @input="bindTextAreaBlur" :value="textvalue" placeholder-style="color:rgba(102,102,102,1);font-size:28rpx;font-weight:400;margin-top:0rpc" class="top_input" auto-height placeholder="是否现有店面等信息"  /> -->
          <textarea
            @input="bindTextAreaBlur"
            :value="textvalue"
            placeholder-style="color:rgba(102,102,102,1);font-size:28rpx;font-weight:400;"
            class="top_input"
            auto-height
            placeholder="是否现有店面等信息"
            v-if="show1==true"
          />
        </div>
      </div>
      <div class="btn" @tap="joinEmao">提交</div>
      <div class="fixed">
        <van-area :area-list="areaList" v-if="show" @cancel="cancle" @confirm="confirm" />
      </div>
    </div>
  </div>
</template>
<script>
import { PostAddRedE, GetProvincesAddress } from '../../api'
import { GetuserInfor } from '../../api'
export default {
  data() {
    return {
      show1: true,
      windowHeight: '',
      addressvalue: '', //城市地址
      city: '请选择所在城市',
      username: '',
      password: '',
      textvalue: '',
      areaList: {
        province_list: {
          //  110000: '北京市',
        },
        city_list: {
          // 110100: '北京市',
        },
        county_list: {
          // 110101: '东城区',
        }
      },
      show: false
    }
  },
  methods: {
    ChangeAddressvalue(event) {
      this.addressvalue = this.province_list + this.city_list + this.county_list

      this.show = true
      this.show1 = false
    },
    showPopup() {
      this.show = true
      this.show1 = false
    },
    cancle() {
      this.show = false
      this.show1 = true
    },
    //获取地址并赋值
    confirm(e) {
      this.province_list1 = e.mp.detail.values[0].name

      this.city_list1 = e.mp.detail.values[1].name
      this.county_list1 = e.mp.detail.values[2].name
      this.province_list2 = e.mp.detail.values[0].code

      this.city_list2 = e.mp.detail.values[1].code
      this.county_list2 = e.mp.detail.values[2].code
      this.addressvalue =
        this.province_list1 + this.city_list1 + this.county_list1

      this.show = false
      this.show1 = true
    },
    // 加盟致大
    joinEmao() {
      let that = this
      if (that.username != '' && that.password != '' && that.city != '') {
        let adddata = {
          name: that.username,
          phone: that.password,
          provinceId: that.province_list2,
          cityId: that.city_list2,
          districtId: that.county_list2,
          shortAddress: that.city,
          remark: that.textvalue,
          createBy: ''
        }
        PostAddRedE(adddata)
          .then(res => {
            if (res.data) {
              wx.showToast({
                title: '报名表已收到，致大会尽快和您联系，谢谢',
                icon: 'none'
              })
              setTimeout(() => {
                mpvue.navigateBack({ url: '/pages/my/main' })
              }, 900)
            }
          })
          .catch(err => {})
      } else {
        wx.showToast({ title: '请先输入信息', icon: 'none' })
      }
    },
    // 用户昵称
    completeInputnumber(event) {
      this.username = event.mp.detail
    },
    // 手机号
    completeInputduanxin(event) {
      this.password = event.mp.detail
    },
    // 备注
    bindTextAreaBlur(e) {
      this.textvalue = e.mp.detail.value
    }
  },
  //获取省市区地址
  mounted() {
    let that = this
    GetProvincesAddress()
      .then(res => {
        that.areaList = res.data

        that.areaList.province_list = res.data.provinces

        that.areaList.city_list = res.data.citys
        that.areaList.county_list = res.data.districts
      })
      .catch(err => {})
    //获取用户手机号和昵称

    GetuserInfor('')
      .then(res => {
        that.password = res.data.userTel
        if (res.data.userName == '') {
          that.username = res.data.nickName
        } else {
          that.username = res.data.userName
        }
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
    // 定位的城市
  },
  onUnload() {
    ;(this.city = '请选择所在城市'),
      (this.username = ''),
      (this.password = ''),
      (this.textvalue = '')
  }
}
</script>

<style scoped lang="scss">
.main {
  background: #fff;
  height: 100%;
}
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #f3f3f3;
  z-index: 1;
}
.top_title {
  width: 100%;
  padding: 30rpx;
  padding-top: 0;
  box-sizing: border-box;
  background: #ffffff;
  .top_title_title {
    width: 100%;
    height: 91rpx;
    line-height: 87rpx;
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: bold;
    color: rgba(19, 18, 13, 1);
  }
}
.top_city {
  width: 100%;
  padding: 0 30rpx;
  box-sizing: border-box;
  background: #ffffff;
  margin-top: 18rpx;
  .top_city_title {
    width: 100%;
    height: 91rpx;
    line-height: 87rpx;
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: bold;
    color: rgba(19, 18, 13, 1);
  }
  .top_city_text {
    width: 100%;
    height: 98rpx;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    border-top: 2rpx solid #e8e8e8;
    .txt {
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
  }
}
.top_resion {
  width: 100%;
  padding: 0 30rpx 30rpx 30rpx;
  box-sizing: border-box;
  background: #ffffff;
  margin-top: 18rpx;
  .top_resion_title {
    width: 100%;
    height: 91rpx;
    line-height: 87rpx;
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: bold;
    color: rgba(19, 18, 13, 1);
  }
  .box {
    width: 100%;
    height: 282rpx;
    background: rgba(243, 243, 243, 1);
    border-radius: 10rpx;

    .top_input {
      width: 80%;
      min-height: 280rpx;
      padding: 30rpx 20rpx;
      box-sizing: border-box;
      margin-left: 60rpx;
      text-align: left;
      z-index: 1;
    }
  }
}
.btn {
  position: fixed;
  bottom: 30rpx;
  left: 58rpx;
  width: 635rpx;
  height: 90rpx;
  background: rgba(255, 99, 36, 1);
  border-radius: 45rpx;
  color: #ffffff;
  text-align: center;
  line-height: 90rpx;
}
.fixed {
  position: fixed;
  left: 0;
  bottom: 0;
  right: 0;
  z-index: 2;
}
</style>