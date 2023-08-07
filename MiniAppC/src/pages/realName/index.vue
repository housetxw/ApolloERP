<template>
  <div class="demo_page">
   

    <div class="main">
      <van-cell-group>
        <van-field
          :value="userName"
          center
          clearable="true"
          border="false"
          use-button-slot
          @change="nameValue"
        ></van-field>
      </van-cell-group>
      <p class="p">请填写真实姓名，用于下单或者客服联系您核实订单信息时使用</p>
      <div class="bt2">
        <van-button round size="large" color="#FF6324" type="default" @tap="newAppo">保存</van-button>
      </div>
    </div>
  </div>
</template>
<script>

import { GetuserInfor, EditUserInfo } from "../../api";

export default {

  data() {
    return {
      userName: ''
    }
  },
  methods: {
    nameValue(event) {
  
      this.userName = event.mp.detail;
    },
    newAppo() {
      if (this.userName == "") {
 
        mpvue.navigateBack({ url: "pages/personalData/main" });
      } else if (this.userName != "") {
        EditUserInfo({
          userName: this.userName
        })
          .then(res => {
        
       
            if (res.data == true) {
              wx.showToast({ title: '修改成功', icon: 'none' })

              setTimeout(() => {
                mpvue.navigateBack({ url: 'pages/personalData/main' })
              }, 1000)
            }
          })
          .catch(err => {
       
          });
      }
    }
  },
  mounted () {
    let that = this
    GetuserInfor('')
      .then(res => {

        that.userName = res.data.userName
      })
      .catch(err => {
    
      });
  }
}
</script>
<style lang="scss" scoped>
.demo_page {
  width: 100%;
  height: 100%;
  background: #f3f3f3;
}

.p {
  font-size: 28rpx;
  font-family: SourceHanSansCN-Regular;
  color: #adadad;
  font-weight: 400;
  padding: 25rpx 30rpx;
}
.s {
  color: #ff6324;
}
.bt2 {
  margin: 10rpx 60rpx;
  color: #fff;
}
</style>