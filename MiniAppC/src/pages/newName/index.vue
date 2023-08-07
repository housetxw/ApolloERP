<template>
  <div class="demo_page">
    <!-- <navigation-bar title="修改昵称" :back-visible="true" :home-path="'/pages/index/main'" @test="test"></navigation-bar> -->

    <div class="main">
      <van-cell-group>
        <van-field
          :value="nickname"
          center
          clearable="true"
          border="false"
          use-button-slot
          @change="nameValue"
        ></van-field>
      </van-cell-group>
      <p class="p">昵称非真实姓名，仅用于发现评论等场景里使用</p>
      <div class="bt2">
        <van-button round size="large" color="#FF6324" type="default" @tap="newAppo">保存</van-button>
      </div>
    </div>
  </div>
</template>
<script>

import { GetuserInfor, EditUserInfo } from "../../api";
import { postTyre, postMoreshop } from "../../api";
export default {

  data() {
    return {
      nickname: ''
    }
  },
  methods: {
    nameValue(event) {
 
      this.nickName = event.mp.detail;
    },
    newAppo() {
      if (this.nickName == "") {
 
        mpvue.navigateBack({ url: "pages/personalData/main" });
      } else if (this.nickName != "") {
        EditUserInfo({
          nickName: this.nickName
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

        that.nickname = res.data.nickName
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