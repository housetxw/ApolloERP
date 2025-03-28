<template>
  <div class="demo_page">
    <!-- <div class="top_title_user row" @tap="changeImg"> -->
    <div class="top_title_user row">
      <p class="p1">头像</p>
      <p class="p2">
        <!-- <img :src="src" alt class="user_img" /> -->
     
        <button class="avatar-wrapper" open-type="chooseAvatar" @chooseavatar="onChooseAvatar">
          <image class="avatar user_img" :src="src"></image>
        </button>

      </p>
    </div>
    <div class="top_title row" @tap="nickName">
      <p class="p1">昵称</p>
      <p class="p2">
        {{nickname}}
        <img
          :src="src1"
          alt
          style="width:24rpx;height:24rpx;margin-left:15rpx;"
        />
      </p>
    </div>
    <div class="top_sex row" @tap="changeSex">
      <p class="p1">性别</p>
      <div class="p3" v-if="gender === 0">未设置</div>

      <div class="p2" v-else>
        <p class="nan">
          <img :src="gender == 1?url2:url1" alt />
          <text>男</text>
        </p>
        <p class="nv">
          <img :src="gender == 2?url4:url3" alt />
          <text>女</text>
        </p>
      </div>
    </div>
    <div class="top_title row">
      <p class="p1">生日</p>
      <p class="p2">
        {{birthday}}
        <!-- <img
          :src="src1"
          alt
          style="width:24rpx;height:24rpx;margin-left:15rpx;"
        />-->
      </p>
    </div>
    <div class="top_title row" @tap="realName">
      <p class="p1">姓名</p>
      <p class="p2">
        {{username}}
        <img
          :src="src1"
          alt
          style="width:24rpx;height:24rpx;margin-left:15rpx;"
        />
      </p>
    </div>
    <div class="top_title row" @tap="Telephone">
      <p class="p1">手机号码</p>
      <p class="p2">
        {{userTelDes}}
        <img
          :src="src1"
          alt
          style="width:24rpx;height:24rpx;margin-left:15rpx;"
        />
      </p>
    </div>
    <div class="top_title row" @tap="toDrivingDate">
      <p class="p1">驾驶证到期日</p>
      <p class="p2">
        {{drivingLicence}}
        <img
          :src="src1"
          alt
          style="width:24rpx;height:24rpx;margin-left:15rpx;"
        />
      </p>
    </div>
    <div class="top_title row" style="margin-top:16rpx" @tap="memberClick">
      <p class="p1">会员等级</p>
      <p class="p2">
        {{memberLevel}}
        <img
          :src="src1"
          alt
          style="width:24rpx;height:24rpx;margin-left:15rpx;"
        />
      </p>
    </div>
    <div class="top_title row" @tap="jifenClick">
      <p class="p1">我的积分</p>
      <p class="p2">
        {{point}}
        <img
          :src="src1"
          alt
          style="width:24rpx;height:24rpx;margin-left:15rpx;"
        />
      </p>
    </div>
    <div class="top_title row" style="margin-top:16rpx" @tap="addresClick">
      <p class="p1">收货地址</p>
      <p class="p2">
        <img
          :src="src1"
          alt
          style="width:24rpx;height:24rpx"
        />
      </p>
    </div>
    <div class="top_title row">
      <p class="p1">注销账号</p>
      <p class="p2">
        <!-- <img
          :src="src1"
          alt
          style="width:24rpx;height:24rpx"
        />-->
      </p>
    </div>
  </div>
</template>
<script>
import { GetuserInfor,
  EditUserInfo 
 } from '../../api'
export default {
  data() {
    return {
      checked: true,
      nickname: '',
      username: '',
      gender: '',
      birthday: '请填写',
      memberLevel: '',
      point: '',
      userTelDes: '',
      drivingLicence: '',
      src: ``, // 用户头像
      src1: `${this.globalData.imgPubUrl}maintenance_jump_icon.png`,
      url1: `${this.globalData.imgPubUrl}mine_man.png`,
      url2: `${this.globalData.imgPubUrl}mine_man_click.png`,
      url3: `${this.globalData.imgPubUrl}mine_woman.png`,
      url4: `${this.globalData.imgPubUrl}mine_woman_click.png`,
      imgURL: `${this.globalData.imgPubUrl}Sweep code.png`
    }
  },
  methods: {
    toDrivingDate() {
      this.$router.push('/pages/drivingDate/main')
    },
    // 会员等级
    memberClick() {
      this.$router.push('/pages/membershiplevel/main')
    },
    // 积分页面
    jifenClick() {
      this.$router.push('/pages/myPoints/main')
    },
    // 收货地址
    addresClick() {
      this.$router.push('/pages/addressManagement/main')
    },
    // 修改手机号码
    Telephone() {
      this.$router.push('/pages/TelephoneModification/main')
    },
    // 修改昵称
    nickName() {
      this.$router.push('/pages/newName/main')
    },
    // 修改姓名
    realName() {
      this.$router.push('/pages/realName/main')
    },
    // 修改性别    
    changeSex() {
      let that = this;
      wx.showActionSheet({
        itemList: ["男", "女"],
        itemColor: "#333",
        success(res) {
          if (!res.cancel) {
            let genderNumber = (res.tapIndex + 1)
            EditUserInfo({
              gender: genderNumber
            })
              .then(res => {
                if (res.data == true) {
                  that.gender = genderNumber
                  wx.showToast({ title: '修改成功', icon: 'none' })
                }
              })
              .catch(err => {
                wx.showToast({ title: '修改失败', icon: 'none' })
              });
          }
        }
      });
    },

    // 修改头像
    changeImg() {
      let that = this;
      wx.showActionSheet({
        itemList: ["从相册中选择", "拍照"],
        itemColor: "#333",
        success(res) {
          if (!res.cancel) {
            if (res.tapIndex == 0) {
              that.chooseWxImageShop("album"); //从相册中选择
            } else if (res.tapIndex == 1) {
              that.chooseWxImageShop("camera"); //手机拍照
            }
          }
        }
      });
    },
    chooseWxImageShop(type) {
      let that = this;
      wx.chooseImage({
        count: 1,
        sizeType: ["original", "compressed"],
        sourceType: [type],
        success(res) {
          // tempFilePath可以作为img标签的src属性显示图片
          const tempFilePaths = res.tempFilePaths[0];
          console.log(tempFilePaths)
          wx.uploadFile({
            url: `${this.globalData.publicUrl}QiNiu/UploadStream`, // 仅为示例，非真实的接口地址
            filePath: tempFilePaths,
            name: 'file',
            formData: {
              File: 'file',
              Directory: 'miniApp/headUrl'
            },
            header: {
              'Content-Type': 'multipart/form-data', // 记得设置
              // 'Content-Type': 'application/json',
              Authorization:
                'Bearer ' + wx.getStorageSync('tokenInfo').accessToken
            },
            success(resa) {
              let url = JSON.parse(resa.data).data
              url = this.globalData.imgRootUrl + url
              EditUserInfo({
                headUrl: url
              })
                .then(res => {
                  if (res.data == true) {
                    that.src = url
                    wx.showToast({ title: '头像修改成功', icon: 'none' })
                  }
                })
                .catch(err => {
                  wx.showToast({ title: '头像更新失败', icon: 'none' })
                });
            },
            fail(data) {
              console.log('上传错误信息', data)
            }
          })
        }
      });
    },
    onChooseAvatar(e) {
      let that = this
      const { avatarUrl } = e.target
      wx.uploadFile({
        url: `${this.globalData.publicUrl}QiNiu/UploadStream`, // 仅为示例，非真实的接口地址
        filePath: avatarUrl,
        name: 'file',
        formData: {
          File: 'file',
          Directory: 'miniApp/headUrl'
        },
        header: {
          'Content-Type': 'multipart/form-data', // 记得设置
          //'Content-Type': 'application/json',
          Authorization:
            'Bearer ' + wx.getStorageSync('tokenInfo').accessToken
        },
        success(resa) {
          let url = JSON.parse(resa.data).data
          url = this.globalData.imgRootUrl + url
          EditUserInfo({
            headUrl: url
          })
            .then(res => {
              if (res.data == true) {
                that.src = url
                wx.showToast({ title: '头像修改成功', icon: 'none' })
              }
            })
            .catch(err => {
              wx.showToast({ title: '头像更新失败', icon: 'none' })
            });
        },
        fail(data) {
          console.log('上传错误信息', data)
        }
      })
    }

  },
  onShow() {
    let that = this
    GetuserInfor('')
      .then(res => {        
        // 在这里可以确保DOM已经更新
        that.src = res.data.headUrl
        that.nickname = res.data.nickName
        that.username = res.data.userName
        that.gender = res.data.gender
        that.birthday = res.data.birthDay
        that.memberLevel = res.data.memberLevel
        that.point = res.data.point
        that.userTelDes = res.data.userTelDes
        that.drivingLicence = res.data.driverLicenseExpiry
      })
      .catch(err => {})
  }
}
</script>
<style scoped lang="scss">
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
.top_title_user {
  width: 100%;
  height: 160rpx;
  .p1 {
    font-size: 30rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(102, 102, 102, 1);
  }
  .p2 {
    width: 140rpx;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .avatar-wrapper {
      padding: 0;
      margin: 0;
      width: 106rpx;
      height: 106rpx;

      .user_img {
        width: 106rpx;
        height: 106rpx;
      }

    }
  }
}
.top_sex {
  width: 100%;
  height: 100rpx;
  .p1 {
    font-size: 30rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(102, 102, 102, 1);
  }
  .p2 {
    width: 220rpx;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .nan,
    .nv {
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      img {
        width: 34rpx;
        height: 34rpx;
      }
      text {
        margin-left: 11rpx;
        font-size: 30rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
      }
    }
  }
  .p3 {
    font-size: 30rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(51, 51, 51, 1);
  }
}
.top_title .p1 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(102, 102, 102, 1);
}
.top_title .p2 {
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
</style>