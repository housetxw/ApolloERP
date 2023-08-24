<template>
  <div class="demo_page">
    <swiper v-if="imgUrls.length > 0" 
        :indicator-dots='true'
        :autoplay='true'
        style="width:690rpx;height:280rpx;margin:30rpx 0;">
      <block v-for="(item, index) in imgUrls" :key="index" >
        <swiper-item>
          <image :src="item" mode="widthFix" style="width:690rpx;height:280rpx;"></image>
        </swiper-item>
      </block>
    </swiper>
    <div class="gridWrap">
      <van-grid :column-num="4" :border="false">
        <van-grid-item use-slot v-for="(item, index) in serveList" :key="index" @click="gridWrapClcik(item,index)">
          <div class='grid'>
            <div class='gridLeft'>
              <img :src="item.serveImg" alt="" class='gridLeftImg'>
              <div class='gridRightTop'>{{item.serveTitle}}</div>
            </div> 
          </div>
        </van-grid-item>
      </van-grid>
    </div>
    <div class="activeTitle">
      <img src="https://m.aerp.com.cn/mini-app-main/title_left_icon.png" alt="" class="activeImg">
      <span class="activeWord">超值活动</span>
      <img src="https://m.aerp.com.cn/mini-app-main/title_right_icon.png" alt="" class="activeImg">
    </div>
    <swiper v-if="imgUrls.length > 0" 
        :indicator-dots='true'
        :autoplay='true'
        style="width:690rpx;height:280rpx;margin:30rpx 0;">
      <block v-for="(item, index) in imgUrls" :key="index" >
        <swiper-item>
          <image :src="item" mode="widthFix" style="width:690rpx;height:280rpx;"></image>
        </swiper-item>
      </block>
    </swiper>
  </div>
</template>
<script>
export default {
  data () {
    return {
      imgUrls: [
        `${this.globalData.imgPubUrl}banner_img.png`,
        `${this.globalData.imgPubUrl}banner_img.png`,
        `${this.globalData.imgPubUrl}banner_img.png`
      ],
      serveList: [
        {
          serveImg: 'https://m.aerp.com.cn/mini-app-main/car_icon_one.png',
          serveTitle: '轮胎服务',
          serveSubTitle: '买轮胎送保险'
        },
        {
          serveImg: 'https://m.aerp.com.cn/mini-app-main/car_icon_two.png',
          serveTitle: '套餐卡',
          serveSubTitle: '次卡 月卡 季卡'
        },
        {
          serveImg: 'https://m.aerp.com.cn/mini-app-main/car_icon_three.png',
          serveTitle: '汽车维修',
          serveSubTitle: '二级标题说明'
        },
        {
          serveImg: 'https://m.aerp.com.cn/mini-app-main/car_icon_four.png',
          serveTitle: '汽车用品',
          serveSubTitle: '免费安装'
        },
        {
          serveImg: 'https://m.aerp.com.cn/mini-app-main/car_icon_five.png',
          serveTitle: '汽车保险',
          serveSubTitle: '智能核保 在线理赔'
        },
        {
          serveImg: 'https://m.aerp.com.cn/mini-app-main/newcar_icon.png',
          serveTitle: '新能源车',
          serveSubTitle: '充电桩查询'
        },
        {
          serveImg: 'https://m.aerp.com.cn/mini-app-main/batteryservice_icon.png',
          serveTitle: '电池服务',
          serveSubTitle: '电池查询'
        },
        {
          serveImg: 'https://m.aerp.com.cn/mini-app-main/detection_icon.png',
          serveTitle: '检测',
          serveSubTitle: '二级标题说明'
        }
      ]
    }
  },
  methods: {
    gridWrapClcik (item, index) {

      // this.$router.push({path:'/pages/gridson/main',query: { title:item.serveTitle}})
      // this.$router.push('/pages/searchpage/main')
      switch (index) {
        case 0:
          wx.showToast({ title: '敬请期待', icon: 'none' })
          break
        case 1:
          wx.showToast({ title: '敬请期待', icon: 'none' })
          break
        case 2:
          wx.showToast({ title: '敬请期待', icon: 'none' })
          break
        case 3:
          wx.showToast({ title: '敬请期待', icon: 'none' })
          break
        case 4:
          wx.showToast({ title: '敬请期待', icon: 'none' })
          break
        case 5:
          wx.showToast({ title: '敬请期待', icon: 'none' })
          break
        case 6:
          wx.showToast({ title: '敬请期待', icon: 'none' })
          break
        case 7:
          wx.showToast({ title: '敬请期待', icon: 'none' })
          break
        case 8:
          wx.showToast({ title: '敬请期待', icon: 'none' })
          break
        case 9:
          wx.showToast({ title: '敬请期待', icon: 'none' })
          break
        case 10:
          wx.showToast({ title: '敬请期待', icon: 'none' })
          break
        default:
          wx.showToast({ title: '点击有误', icon: 'none' })
      }
    }
  }

}
</script>
<style scoped lang="scss">
.demo_page{
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #FFFFFF;
}
.gridWrap {
  // box-shadow:0px 5rpx 16rpx 0px rgba(204,204,204,0.5);
  border-radius:10rpx;
  margin-top: 20rpx;
  .grid {
    display: flex;
    align-items: center;
    border: none;
    .gridLeft {
      width: 100%;
      height: 120rpx;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: center;
      .gridLeftImg {
        left: 50%;
        transform:tranlateX(-50%);
        width: 66rpx;
        height: 66rpx;
      }
      .gridRightTop{
        font-size:24rpx;
        font-family:Source Han Sans CN;
        font-weight:400;
        color:rgba(33,33,33,1);
      }
    }
  }
}
.activeTitle {
  display: flex;
  align-items: center;
  width:100%;
  height:90rpx;
  margin-top: 21rpx;
  justify-content: center;
  .activeImg{
    width:25rpx;
    height:25rpx;
  }
  .activeWord{
    font-size:32rpx;
    font-family:Source Han Sans CN;
    font-weight:bold;
    color:rgba(33,33,33,1);
    margin: 0 15rpx;
  } 
}
</style>