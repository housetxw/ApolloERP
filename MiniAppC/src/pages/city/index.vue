<template>
  <div class="container ub-box ub-col">
    <city @cityService="cityService" :citys="citys" :hotCitys="hotCitys"></city>
  </div>
</template>
<script>
import city from '../../components/city.vue'
export default {
  components: { city },
  data () {
    return {
      citys: [],
      hotCitys: []
    }
  },
  mounted () {
  },
  methods: {
    cityService (city) {
      // 更新Vuex state中的curCity
      this.$store.commit('updateCity', city)
    }
  },
  onShow () {
    wx.setNavigationBarTitle({ title: '选择城市' })
  }
}
</script>
<style scoped>
.container {
  width: 100%;
  height: 100vh;
  background: #fff;
}
</style>
