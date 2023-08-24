<template>
  <div class="demo_page">
    <!-- <img src="https://m.aerp.com.cn/mini-app-main/active.png" alt=""   class="image" v-if="show1">  -->
    <div class="img">
      <img
        src="https://m.aerp.com.cn/mini-app-main/product2.png"
        alt
        style="width:680rpx;height:400rpx; display:block;"
        @tap="center('MP-RMP-SMP-10')"
      />
      <img
        src="https://m.aerp.com.cn/mini-app-main/product1.png"
        alt
        style="width:680rpx;height:400rpx; padding-top:15rpx; display:block;"
        @tap="center('MP-RMP-SMP-12')"
      />
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {}
  },
  methods: {
    center(pid) {
      this.$router.push({ path: '/pages/maintain/main', query: { pid } })
    }
  },
  onShow() {},
  mounted() {}
}
</script>
<style lang="scss" scoped>
.demo_page {
  position: relative;
  width: 100%;
  display: flex;
  // height:100%;
  // flex-direction: column;
  // justify-content: flex-start;
  // align-items: center;
  background-image: url('https://m.aerp.com.cn/mini-app-main/active.png');
  background-size: cover;
  background-position: top;
  background-repeat: no-repeat;

  height: 2000rpx;
}

.image {
  display: block;
  width: 100%;
  height: 100%;
}
.img {
  position: absolute;
  bottom: 70rpx;
  left: 33rpx;
  padding-top: 30rpx;
}
</style>