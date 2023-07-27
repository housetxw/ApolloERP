<template>
  <div class="tabZujian">
    <el-tabs v-model="activeName" type="border-card" @tab-click="handleClick">
      <el-tab-pane label="门店未对账列表" name="first" :key="'first'">
        <child1 v-if="isChildUpdate1"></child1>
      </el-tab-pane>

      <el-tab-pane label="门店已对账列表" name="second" :key="'second'">
        <child2 v-if="isChildUpdate2"></child2>
      </el-tab-pane>
      <el-tab-pane label="总部已对账列表" name="third" :key="'third'">
        <child3 v-if="isChildUpdate3"></child3>
      </el-tab-pane>

      <el-tab-pane label="对账异常监控" name="fouth" :key="'fouth'">
        <child4 v-if="isChildUpdate4"></child4>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script>

import shopaccountunchecklist from "@/views/fms/accountcheck/shopaccountunchecklist";
import shopaccountchecklist from "@/views/fms/accountcheck/shopaccountchecklist";
import rgaccountchecklist from "@/views/fms/accountcheck/rgaccountchecklist";
import accountcheckexceptionlist from "@/views/fms/accountcheck/accountcheckexceptionlist";
export default {
  name: 'tabZujian',
  components: {
    child1: shopaccountunchecklist,
    child2: shopaccountchecklist,
    child3: rgaccountchecklist,
    child4: accountcheckexceptionlist
  },
  data() {
    return {
      //默认第一个选项卡
      activeName: 'first',
      isChildUpdate1: true,
      isChildUpdate2: false,
      isChildUpdate3: false,
      isChildUpdate4: false
    }
  },
  mounted() {},
  methods: {
    handleClick(tab) {
      if (tab.name == 'first') {
        this.isChildUpdate1 = true
        this.isChildUpdate2 = false
        this.isChildUpdate3 = false
        this.isChildUpdate4 = false
      } else if (tab.name == 'second') {
        this.isChildUpdate1 = false
        this.isChildUpdate2 = true
        this.isChildUpdate3 = false
        this.isChildUpdate4 = false
      } else if (tab.name == 'third') {
        this.isChildUpdate1 = false
        this.isChildUpdate2 = false
        this.isChildUpdate3 = true
        this.isChildUpdate4 = false
      } else if (tab.name == 'fouth') {
        this.isChildUpdate1 = false
        this.isChildUpdate2 = false
        this.isChildUpdate3 = false
        this.isChildUpdate4 = true
      }
    }
  }
}
</script>

<style>
.tabZujian {
  margin: 0px 0px 0px 25px;
}
</style>
