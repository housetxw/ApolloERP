<template>
  <div class="page">
    <div class="back" @click="back">
      <image src="https://m.aerp.com.cn/mini-RG-main/white_back.png" style="width:16rpx;height:29rpx;"></image>
    </div>

    <div class="content">    

        <div  v-if="beginImageUrl.length>0" style="width:100%;position:relative;">
            <image   :src='beginImageUrl' mode="widthFix"  style="width:100%;position:relative;min-height:100%;" />   
         </div>  

        <div  v-if="btnImageUrl.length>0" style="width:100%;position:relative;">
            <image   :src='btnImageUrl' mode="widthFix"  
                   @click="onButton1Click"
                   style="width:100%;position:relative;min-height:100% ;" />   
         </div> 

        <div  v-if="btn2ImageUrl.length>0" style="width:100%;position:relative;">
            <image   :src='btn2ImageUrl' mode="widthFix"  
                   @click="onButton2Click"
                   style="width:100%;position:relative;min-height:100% ;" />   
         </div>  
         
        <div  v-if="btn3ImageUrl.length>0" style="width:100%;position:relative;">
            <image   :src='btn3ImageUrl' mode="widthFix"  
                   @click="onButton3Click"
                   style="width:100%;position:relative;min-height:100% ;" />   
         </div> 

        <div  v-if="endImageUrl.length>0" style="width:100%;position:relative;">
            <image   :src='endImageUrl' mode="widthFix"  style="width:100%;position:relative;min-height:100%;" />   
         </div>        
    </div>        
  </div>
</template>

<script>
import { GetIndexPage } from '../../api'
export default {
  data() {
    return {
        advertisingCode:'',
        isFromShare: false,
        isFromScanCode:false,
 
        beginImageUrl:"",

        btnImageUrl:"",
        buttonRouter:"",

        btn2ImageUrl: "",
        button2Router:"",

        btn3ImageUrl:"",
        button3Router:"",

        endImageUrl:"",

    }
  },
  mounted() {
    this.getIndexpage();
  },
  onLoad(op) {

    if (op.activedId == 1) {
      this.globalData.shareUserId = op.shareUserId
    }
    this.isFromShare = op.isFromShare == 'true' || op.isFromShare === true
    if (op.q) {
      console.log("-1-op.q--->",op.q)
      let res = decodeURIComponent(op.q)
      console.log("-1-op.q-res-->",res)
      this.advertisingCode = this.getRequest2('code', res) || ''
      console.log("-1-op.q-advertisingCode-->",this.advertisingCode)
      this.isFromScanCode = true;
    } else {
      this.advertisingCode = op.code || ''
    }
    // this.isFromShare = op.isFromShare
    // this.buttonRouter  = decodeURIComponent(op.gotoUrl)
    // this.button2Router = decodeURIComponent(op.goto2Url)
    // this.button3Router = decodeURIComponent(op.goto3Url)


    // this.btnImageUrl = op.btnImageUrl
    // this.btn2ImageUrl = op.btn2ImageUrl
    // this.btn3ImageUrl = op.btn3ImageUrl


    // var ihead = "https://m.aerp.com.cn/"

    // if(op.beginImageUrl!=null && op.beginImageUrl.length>0){
    //   this.beginImageUrl = ihead +op.beginImageUrl
    // }
    // if(op.btnImageUrl!=null && op.btnImageUrl.length>0){
    //   this.btnImageUrl = ihead +op.btnImageUrl
    // }
    // if(op.btn2ImageUrl!=null && op.btn2ImageUrl.length>0){
    //   this.btn2ImageUrl = ihead +op.btn2ImageUrl
    // }
    // if(op.btn3ImageUrl!=null && op.btn3ImageUrl.length>0){
    //   this.btn3ImageUrl = ihead +op.btn3ImageUrl
    // }
    // if(op.endImageUrl!=null && op.endImageUrl.length>0){
    //   this.endImageUrl = ihead +op.endImageUrl
    // }
    // console.log("onload btnImageUrl = " + this.btnImageUrl)
    // console.log("onload btn2ImageUrl = " + this.btn2ImageUrl)
    // console.log("onload btn3ImageUrl = " + this.btn3ImageUrl)
    // console.log("this.buttonRouter "+ this.buttonRouter)
    // console.log("this.button2Router "+ this.button2Router)
    // console.log("this.button3Router "+ this.button3Router)


  },
  onShareAppMessage(res) {
    let that = this
    console.log(`onShareAppMessage: function`, res)
    if (res.from === 'button') {
      // 来自页面内转发按钮
      // console.log("转达",res.target)
    }
    return {
      title: that.shopTitle,
      path: `/pages/bannerDetail/main?isFromShare=true&code=${that.advertisingCode}&activedId=1&shareUserId=${that.globalData.userInfo?that.globalData.userInfo.userId:''}`,
      success(res) {
        console.log(`res分享按钮`, res)
        // 转发成功
        wx.showToast({
          title: '分享成功',
          icon: 'success',
          duration: 2000
        })
      },
      fail(res) {
        // 分享失败
        wx.showToast({
          title: '分享失败',
          icon: 'none',
          duration: 2000
        })
      }
    }
  },
  methods: {
     getRequest2(qs, url) {
      var s = url.replace('?', '?&').split('&')
      let re = ''
      for (var i = 1; i < s.length; i++) {
        if (s[i].indexOf(qs + '=') == 0) {
          re = s[i].replace(qs + '=', '')
        }
      }
      return re
    },
     getIndexpage() {
      let that = this
      GetIndexPage({ Version: 2 })
        .then(res => {
          let imgUrls = res.data.topAdvertising // 轮播topAdvertising
          if(imgUrls && imgUrls.length > 0){
            let currentItem = imgUrls.find(item=>item.advertisingCode == that.advertisingCode)
            if(currentItem){
              that.buttonRouter  = decodeURIComponent(currentItem.gotoUrl)
              that.button2Router = decodeURIComponent(currentItem.goto2Url)
              that.button3Router = decodeURIComponent(currentItem.goto3Url)


              that.btnImageUrl = currentItem.btnImageUrl
              that.btn2ImageUrl = currentItem.btn2ImageUrl
              that.btn3ImageUrl = currentItem.btn3ImageUrl


              var ihead = "https://m.aerp.com.cn/"

              if(currentItem.beginImageUrl!=null && currentItem.beginImageUrl.length>0){
                that.beginImageUrl = ihead +currentItem.beginImageUrl
              }
              if(currentItem.btnImageUrl!=null && currentItem.btnImageUrl.length>0){
                that.btnImageUrl = ihead +currentItem.btnImageUrl
              }
              if(currentItem.btn2ImageUrl!=null && currentItem.btn2ImageUrl.length>0){
                that.btn2ImageUrl = ihead +currentItem.btn2ImageUrl
              }
              if(currentItem.btn3ImageUrl!=null && currentItem.btn3ImageUrl.length>0){
                that.btn3ImageUrl = ihead +currentItem.btn3ImageUrl
              }
              if(currentItem.endImageUrl!=null && currentItem.endImageUrl.length>0){
                that.endImageUrl = ihead +currentItem.endImageUrl
              }
              console.log("onload btnImageUrl = " + this.btnImageUrl)
              console.log("onload btn2ImageUrl = " + this.btn2ImageUrl)
              console.log("onload btn3ImageUrl = " + this.btn3ImageUrl)
              console.log("this.buttonRouter "+ this.buttonRouter)
              console.log("this.button2Router "+ this.button2Router)
              console.log("this.button3Router "+ this.button3Router)
            }
          }
        })
        .catch(err => {
          console.log(`bannerDetail首页数据信息获取失败函数,${err}`)
        })
    },  
    back() {
      if(this.isFromShare || this.isFromScanCode){
          wx.reLaunch({
            url: '/pages/index/main'
          })
      }else{
          wx.navigateBack()
      }
    }, 
    onButton1Click(){
      console.log("onButton1Click "+ this.buttonRouter)
      this.$router.push({
        path: this.buttonRouter,
        // query: {
        //   CategoryId: 1,
        //   baoYangType: ["xby"]
        // }
      })
    },
     onButton2Click(){
      console.log("onButton2Click "+ this.button2Router)
      this.$router.push({
        path: this.button2Router,        
      })
    }
    , onButton3Click(){
      console.log("onButton3Click "+ this.button3Router)
      this.$router.push({
        path: this.button3Router
      })
    }
  }
}
</script>
<style>
  page {
    position: relative;
  }
</style>

<style scoped lang="scss">
.page {
  display: flex;
  flex-direction: column;
  align-items: center;
  .back {
    position: fixed;
    width: 50rpx;
    height: 50rpx;
    display: flex;
    align-items: center;
    justify-content: center;
    left: 43rpx;
    top: 72rpx;
    z-index: 1;
  }
  .content {
    position: absolute;
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 100%;
    font-size: 0;  
  }
  
}
</style>
