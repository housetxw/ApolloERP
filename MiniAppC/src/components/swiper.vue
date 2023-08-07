<template>
    <div>
        <swiper class="swiper" :autoplay="autoplay" :interval="interval" :duration="duration">
            <block v-for="(item, index) in images" :key="index">
                <swiper-item>
                    <image :src="item.url" class="slide-image" mode="aspectFill"/>
                    <div class="swiper_text">
                        <text class="swiper_text1">商品编号：{{item.num}}</text>
                        <text class="swiper_text2">{{item.num1}}/{{images.length}}</text>
                    </div>
                </swiper-item>
            </block>
        </swiper>
    </div>
</template>

<script>
export default {
  props: {
    images: {
      type: Array
    }
  },
  data() {
    return {
      autoplay: true,
      interval: 3000,
      duration: 500
    };
  }
};
</script>

<style scoped>
.swiper {
    width: 100%;
    height: 750rpx !important;
}
image {
  height: 100%;
  width: 100%;
}
.swiper_text{
    position: absolute;
    bottom: 0;
    left: 0;
    width: 100%;
    padding-left:30rpx;
    padding-right: 30rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center; 
    background: #FFFFFF;
}
.swiper_text1{
    line-height:75rpx;
    font-size:26rpx;
    font-family:Source Han Sans CN;
    font-weight:400;
}
.swiper_text2{
    width:75rpx;
    height:75rpx;
    background:rgba(33,33,33,1);
    opacity:0.5;
    border-radius:50%;
    font-size:36rpx;
    font-family:Source Han Sans CN;
    font-weight:400;
    color:rgba(255,255,255,1);
    text-align: center;
    line-height: 75rpx;
}
</style>