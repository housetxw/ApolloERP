<template>
  <div class="tabZujian">
    <el-tabs v-model="activeName" type="border-card" @tab-click="handleClick">
      <el-tab-pane label="仓库库存列表" name="first" :key="'first'">
        <child1 v-if="isChildUpdate1"></child1>
      </el-tab-pane>

      <el-tab-pane label="公司库存列表" name="second" :key="'second'">
        <child2 v-if="isChildUpdate2"></child2>
      </el-tab-pane>
      <el-tab-pane label="门店库存列表" name="third" :key="'third'">
        <child3 v-if="isChildUpdate3"></child3>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>
<script>
import companystockindex from "@/views/stock/productstock/companystockindex";
import shopstockindex from "@/views/stock/productstock/shopstockindex";
import warehousestockindex from "@/views/stock/productstock/warehousestockindex";
export default {
  name: "tabZujian",
  components: {
    child1:  warehousestockindex,
    child2:  companystockindex,
    child3: shopstockindex
  },
  data() {
    return {
      //默认第一个选项卡
      activeName: "first",
      isChildUpdate1: true,
      isChildUpdate2: false,
      isChildUpdate3: false
    };
  },
  mounted() {},
  methods: {
    handleClick(tab) {
      if (tab.name == "first") {
        this.isChildUpdate1 = true;
        this.isChildUpdate2 = false;
        this.isChildUpdate3 = false;
      } else if (tab.name == "second") {
        this.isChildUpdate1 = false;
        this.isChildUpdate2 = true;
        this.isChildUpdate3 = false;
      } else if (tab.name == "third") {
        this.isChildUpdate1 = false;
        this.isChildUpdate2 = false;
        this.isChildUpdate3 = true;
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
