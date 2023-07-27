<template>
  <div class="tabZujian">
    <el-tabs v-model="activeName" type="border-card" @tab-click="handleClick">
      <el-tab-pane label="仓库库存汇总列表" name="first" :key="'first'">
        <child1 v-if="isChildUpdate1"></child1>
      </el-tab-pane>

      <el-tab-pane label="供应商库存汇总查询" name="second" :key="'second'">
        <child2 v-if="isChildUpdate2"></child2>
      </el-tab-pane>
     
    </el-tabs>
  </div>
</template>

<script>
import warehousestocktrackindex from "@/views/stock/warehousestocktrackindex";
import venderstocktrackindex from "@/views/stock/venderstocktrackindex";


export default {
  name: "tabZujian",
  components: {
    child1: warehousestocktrackindex,
    child2: venderstocktrackindex,

  },
  data() {
    return {
      //默认第一个选项卡
      activeName: "first",
      isChildUpdate1: true,
      isChildUpdate2: false
    };
  },
  mounted() {},
  methods: {
    handleClick(tab) {
      if (tab.name == "first") {
        this.isChildUpdate1 = true;
        this.isChildUpdate2 = false;
     
      } else if (tab.name == "second") {
        this.isChildUpdate1 = false;
        this.isChildUpdate2 = true;
      
      } 
    }
  }
};
</script>

<style>
.tabZujian {
  margin: 20px 0px 0px 25px;
}
</style>
