<template>
  <div class="demo_page">
    <van-cell-group>
      <van-field
        label="联系人"
        :value="namevalue"
        placeholder="请填写姓名"
        @blur="ChangeNamevalue"
        style="position:relative;"
      />
      <img
        src="https://m.aerp.com.cn/mini-app-main/ordercheck_address_icon.png"
        alt
        class="img_card"
        @click="toPeople"
      />
      <van-field :value="numbervalue" placeholder="请填写手机号" @blur="ChangeNumbervalue" label="手机号" />
      <van-field
        :value="addressvalue"
        :right-icon="rightIcon1"
        placeholder="请选择城市区域"
        @change="ChangeAddressvalue"
        @click="showPopup"
        readonly="readonly"
        label="省市区"
      ></van-field>

      <van-field
        :value="detailsvalue"
        placeholder="请输入详细的上门服务地址"
        @blur="ChangeDetailsvalue"
        label="上门地址"
      />
    </van-cell-group>

    <div class="box">
      <p class="p1">标签</p>
      <div class="metal">
        <text
          :class="[{'bgcolor': index == mealActiveIndex?true:false},'txt1']"
          v-for="(item,index) in mealArr"
          :key="index"
          @tap="mealSelect(index,item)"
        >{{item}}</text>
        <!-- <text class="txt2" @tap="addmeal">+添加</text> -->
      </div>
    </div>
    <div class="box1">
      <p class="p1">设置默认地址</p>
      <img
        :src="switchdis?'https://m.aerp.com.cn/mini-app-main/maintenance_radio_click.png':'https://m.aerp.com.cn/mini-app-main/maintenance_radio.png'"
        @tap="imgClick"
        class="img1"
      />
    </div>
    <p class="tips">提醒：每次下单时会使用该地址，实际下单地址会根据您购物切换的地区进行智能判断，请在下单时确认哦！</p>
    <div class="btn1" v-if="addId==1">
      <van-button round color="#FF6324" type="default" size="large" @tap="saveUserAddress">保存</van-button>
    </div>
    <div class="btn" v-else>
      <div class="bbutton" @tap="delAddress()">删除</div>
      <div class="bbutton1" @tap="saveUserAddress">保存</div>
    </div>

    <div class="fixed">
      <van-area :area-list="areaList" v-if="show" @cancel="cancle" @confirm="confirm" />
    </div>
  </div>
</template>
<script>
import {
  PostNewUserAddres,
  GetProvincesAddress,
  SetDefaultAddress,
  EditUserAddress,
  DeleteUserAddress
} from '../../api'

export default {
  data() {
    return {
      addId: '',
      namevalue: '', // 用户姓名
      numbervalue: '', // 手机号
      addressvalue: '', // 详细地址
      detailsvalue: '', // 详细的街道信息/门牌
      rightIcon:
        'https://m.aerp.com.cn/mini-app-main/ordercheck_address_icon.png',
      rightIcon1:
        'https://m.aerp.com.cn/mini-app-main/ordercheck_location_icon.png',
      mealArr: ['公司', '家', '学校'],
      switchdis: true,
      mealActiveIndex: -1,
      addressId: '', // 编辑用户id
      areaList: {
        province_list: {
          // 110000: '北京市',
        },
        city_list: {
          // 110100: '北京市',
        },
        county_list: {
          // 110101: '东城区',
        }
      },
      show: false,
      province_list1: '',
      city_list1: '',
      county_list1: '',
      province_list2: '',
      city_list2: '',
      county_list2: ''
    }
  },
  methods: {
    ChangeNamevalue(event) {
      this.namevalue = event.mp.detail.value
    },
    ChangeNumbervalue(event) {
      console.log(event)

      this.numbervalue = event.mp.detail.value
      console.log(this.numbervalue)
    },
    ChangeAddressvalue(event) {
      this.addressvalue =
        this.province_list1 + this.city_list1 + this.county_list1
      this.show = true
    },
    ChangeDetailsvalue(event) {
      this.detailsvalue = event.mp.detail.value
    },

    imgClick(options) {
      this.switchdis = !this.switchdis
      console.log('switchdis', this.switchdis)
    },
    showPopup() {
      this.show = true
    },
    cancle() {
      this.show = false
    },
    // 获取地址并赋值
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
    },
    // 保存用户地址
    saveUserAddress() {
      if (!/^1[3456789]\d{9}$/.test(this.numbervalue)) {
        wx.showToast({ title: '手机号输入有误', icon: 'none' })
        return false
      }
      if (this.$route.query.id == 1) {
        let that = this
        console.log(that.switchdis)
        let data = {
          userName: that.namevalue,
          phoneNumber: that.numbervalue,
          mobileNumber: that.numbervalue,
          addressTag: that.mealArr[that.mealActiveIndex],
          province: that.province_list1,
          city: that.city_list1,
          district: that.county_list1,
          addressLine: that.detailsvalue,
          provinceId: that.province_list2,
          cityId: that.city_list2,
          districtId: that.county_list2,
          defaultAddress: that.switchdis
        }

        PostNewUserAddres(data)
          .then(res => {
            // wx.showToast({ title: "添加成功", icon: "none" });

            if (res.code === 10000) {
              wx.showToast({ title: '添加成功', icon: 'success' })
              setTimeout(() => {
                mpvue.navigateBack({ url: '/pages/addressManagement/main' })
              }, 1500)
            }
          })

          .catch(err => {})
      } else {
        let that = this
        let data = {
          userName: that.namevalue,
          mobileNumber: that.numbervalue,
          addressTag: that.mealArr[that.mealActiveIndex],
          province: that.province_list1,
          city: that.city_list1,
          district: that.county_list1,
          addressLine: that.detailsvalue,
          provinceId: that.province_list2,
          cityId: that.city_list2,
          districtId: that.county_list2,
          defaultAddress: that.switchdis,
          addressId: that.addressId
        }
        EditUserAddress(data)
          .then(res => {
            // that.addressvalue=res.data.province_list1 + res.data.city_list1 + res.data.county_list1;

            // wx.showToast({ title: "修改成功", icon: "none" });
            if (res.code === 10000) {
              wx.showToast({ title: '修改成功', icon: 'success' })
              setTimeout(() => {
                mpvue.navigateBack({ url: '/pages/addressManagement/main' })
              }, 1500)
            }
          })
          .catch(err => {})
      }
    },
    toPeople() {
      console.log(this.globalData.addressSure)
      let that = this
      if (that.globalData.addressSure == false) {
        wx.showModal({
          title: '“致大”想访问您的通讯录',
          content:
            '致大需要访问您的通讯录，以便您在购物时正常查阅、取用您的联系人信息',
          confirmText: '好',
          cancelText: '不允许',
          success(res) {
            if (res.confirm) {
              console.log('好')
              that.globalData.addressSure = true
              that.chooseAddress()
            } else if (res.cancel) {
              console.log('用户点击不允许')
            }
          }
        })
      } else {
        that.chooseAddress()
      }
    },
    chooseAddress() {
      let that = this
      wx.chooseAddress({
        success: function(res) {
          console.log(res)
          that.namevalue = res.userName
          that.numbervalue = res.telNumber
          that.county_list1 = res.countyName

          that.province_list1 = res.provinceName

          that.city_list1 = res.cityName
          that.detailsvalue = res.detailInfo
          that.addressvalue =
            that.province_list1 + that.city_list1 + that.county_list1
          // let provinceArr = Object.keys(that.areaList.province_list).map(function (k) {
          //   return {code: k, name: that.areaList.province_list[k]}
          // })

          // for (let i = 0; i < provinceArr.length; i++) {
          //   const ele = that.province_list1
          //   if (ele == provinceArr[i].name) {
          //     that.province_list2 = provinceArr[i].code
          //     console.log('省', that.province_list2)
          //   }
          // }
          for (let i in that.areaList.province_list) {
            const ele = that.province_list1

            if (ele == that.areaList.province_list[i]) {
              that.province_list2 = i
            }
          }
          for (let i in that.areaList.city_list) {
            const ele = that.city_list1

            if (ele == that.areaList.city_list[i]) {
              that.city_list2 = i
            }
          }
          for (let i in that.areaList.county_list) {
            const ele = that.county_list1

            if (ele == that.areaList.county_list[i]) {
              that.county_list2 = i
            }
          }
          console.log('市', that.city_list2)
          console.log('区', that.county_list2)
          console.log('省', that.province_list2)
        }
      })
    },
    // 删除地址
    delAddress(items) {
      let that = this
      wx.showModal({
        title: '提示',
        content: '确认删除当前收货人信息？',
        confirmText: '确定',
        cancelText: '取消',
        success(res) {
          if (res.confirm) {
            DeleteUserAddress({ addressId: that.addressId })
              .then(res => {
                wx.showToast({ title: '收货人信息删除成功', icon: 'none' })
                setTimeout(() => {
                  mpvue.navigateBack({ url: '/pages/addressManagement/main' })
                }, 1500)
              })
              .catch(err => {})
          } else if (res.cancel) {
            console.log('用户点击不允许')
          }
        }
      })
    },

    // 标签选择
    mealSelect(index) {
      this.mealActiveIndex = index
    },
    // 标签添加
    addmeal() {}
  },
  // 获取省市区地址
  mounted() {
    let that = this
    GetProvincesAddress()
      .then(res => {
        that.areaList = res.data

        that.areaList.province_list = res.data.provinces
        console.log(that.areaList.province_list)
        that.areaList.city_list = res.data.citys
        that.areaList.county_list = res.data.districts
      })
      .catch(err => {})
  },
  // 接收用户收货地址
  onLoad(options) {
    let that = this

    if (options.id === '1') {
      this.addId = options.id
      console.log('addId', this.addId)
    } else {
      wx.setNavigationBarTitle({
        title: '编辑地址'
      })
      that.addressId = options.addressId

      that.namevalue = options.userName
      that.numbervalue = options.mobileNumber
      that.addressvalue = options.province + options.city + options.district
      that.province_list1 = options.province
      that.city_list1 = options.city
      that.county_list1 = options.district
      that.detailsvalue = options.addressLine
      for (let i = 0; i < that.mealArr.length; i++) {
        if (that.mealArr[i] == options.addressTag) {
          that.mealActiveIndex = i
        }
      }
      that.switchdis = options.defaultAddress
    }
  },

  onUnload: function() {
    this.namevalue = ''
    this.numbervalue = ''
    this.addressvalue = ''
    this.detailsvalue = ''
    this.switchdis = true
    this.mealActiveIndex = -1
    this.addressId = ''
    this.show = false
  }
}
</script>
<style scoped lang="scss">
.demo_page {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  background: #f3f3f3;
}
.box {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  height: 100rpx;
  background: rgba(255, 255, 255, 1);
  padding: 30rpx;
  box-sizing: border-box;
  .p1 {
    width: 150rpx;
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: flex-start;
    font-size: 28rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(51, 51, 51, 1);
  }
  .metal {
    width: 580rpx;
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    align-items: center;
  }
}
.txt1 {
  width: 100rpx;
  height: 50rpx;
  border: 1rpx solid rgba(230, 230, 230, 1);
  border-radius: 4rpx;
  text-align: center;
  line-height: 48rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
  margin-right: 15rpx;
}
.txt2 {
  width: 100rpx;
  height: 50rpx;
  border: 1rpx solid rgba(230, 230, 230, 1);
  border-radius: 4rpx;
  text-align: center;
  line-height: 48rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
  margin-left: 100rpx;
}
.box1 {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  height: 100rpx;
  background: rgba(255, 255, 255, 1);
  padding: 30rpx;
  box-sizing: border-box;
  .p1 {
    font-size: 28rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(51, 51, 51, 1);
  }
  .img1 {
    width: 36rpx;
    height: 36rpx;
  }
}
.tips {
  width: 100%;
  height: 100rpx;
  padding: 0 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
}
.btn {
  position: fixed;
  bottom: 0;
  left: 0;
  width: 100%;
  background: rgba(255, 255, 255, 1);
  padding: 10rpx 30rpx;
  box-sizing: border-box;
  display: flex;
  justify-content: center;
}
.btn1 {
  position: fixed;
  bottom: 0;
  left: 0;
  width: 100%;
  background: rgba(255, 255, 255, 1);
  padding: 10rpx 30rpx;
  box-sizing: border-box;
}
.bgcolor {
  background: #ff6324;
  color: #ffffff;
}
.fixed {
  position: fixed;
  left: 0;
  bottom: 0;
  right: 0;
}
.img_card {
  width: 40rpx;
  height: 40rpx;
  position: absolute;
  right: 40rpx;
  top: 30rpx;
  z-index: 5;
}
.bbutton {
  background: #f63e3e;
  height: 80rpx;
  width: 350rpx;

  border-radius: 30rpx 0 0 30rpx;
  display: flex;
  justify-content: center;
  align-items: center;
  color: #fff;

  // border-radius: 40rpx;
}
.bbutton1 {
  background: #ff6324;

  height: 80rpx;
  width: 350rpx;

  border-radius: 0 30rpx 30rpx 0;
  display: flex;
  justify-content: center;
  align-items: center;
  color: #fff;
}
</style>