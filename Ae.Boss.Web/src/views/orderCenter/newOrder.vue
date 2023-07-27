
<template>
  <main class="container">
    <el-steps :active="active" class="steps" align-center>
      <el-step title="选择客户" description></el-step>
      <el-step title="选择服务内容"></el-step>
      <el-step title="选择服务门店"></el-step>
      <el-step title="提交订单"></el-step><!-- <el-step title="预约&支付"></el-step> -->
      <!-- <el-step title="完成"></el-step> -->
    </el-steps>
    <step1 @donext="next" :showCard="this.active==0" :params="postData" />
    <step2 @donext="next" @dolast="last" :showCard="this.active==1" :params="postData" />
    <step3 @donext="next" @dolast="last" :showCard="this.active==2" :params="postData" />
    <step4 @donext="next" @dolast="last" :showCard="this.active==3" :params="postData" />
    <!-- <step5 @donext="next" @dolast="last" :showCard="this.active==4" :params="postData" /> -->
  </main>
</template>

<script>
import step1 from "./components/step_1.vue";
import step2 from "./components/step_2.vue";
import step3 from "./components/step_3.vue";
import step4 from "./components/step_4.vue";
import step5 from "./components/step_5.vue";
export default {
  name: "newOrder",
  components: { step1, step2, step3, step4, step5 },
  data() {
    return {
      active: 0,
      postData: {
        choose_shop: {},
        choose_products: [],
      },
    };
  },

  created() {
    // 页面初始化函数
    // this.fetchData();
  },
  mounted() {},
  methods: {
    last(p) {
      Object.assign(this.postData, p || {});
      if (this.active-- <= 1) this.active = 0;
    },
    next(p) {
      console.log("next===", p);
      if (this.active == 0) {
        Object.assign(this.postData, p);
        if (this.postData.userId) this.active++;
      } else if (this.active == 1) {
        Object.assign(this.postData, p);
        if (this.postData.choose_products.length) this.active++;
      } else if (this.active == 2) {
        Object.assign(this.postData, p);
        if (this.postData.choose_shop.id) this.active++;
      } else if (this.active == 3) {
        Object.assign(this.postData, p);
        if (this.postData.orderInfo.orderId) this.active++;
      } 
      // else if (this.active == 4) {
      //   Object.assign(this.postData, {});
      //   this.active++;
      // }
      if (this.active > 3) this.active = 0;
    },
  },
};
</script>
<style lang="scss" scoped>
>>> .steps {
  max-width: 1000px;
  margin: 20px auto;
}
>>> .col-radio .el-radio {
  margin-left: 8px;
}
>>> .clearfix {
  text-align: center;
}
>>> .box-card {
  max-width: 1000px;
  margin: 15px auto;

  .bax-card-container {
    height: calc(100vh - 350px);
    overflow-y: auto;
  }
}
>>> .split {
  height: 1px;
  border: none;
  border-top: 1px solid lightgray;
}
</style>
