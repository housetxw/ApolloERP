<template>
  <div class="tabZujian">
    <el-tabs v-model="activeName" type="border-card" @tab-click="handleClick">
      <el-tab-pane label="门店评论列表" name="first" :key="'first'">
        <child1 v-if="isChildUpdate1"></child1>
      </el-tab-pane>

      <el-tab-pane label="技师评论列表" name="second" :key="'second'">
        <child2 v-if="isChildUpdate2"></child2>
      </el-tab-pane>
      <el-tab-pane label="产品评论列表" name="third" :key="'third'">
        <child3 v-if="isChildUpdate3"></child3>
      </el-tab-pane>
      <el-tab-pane label="回评列表" name="fourth" :key="'fourth'">
        <child4 v-if="isChildUpdate4"></child4>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script>
import orderCommentForProductIndex from "@/views/ordercomment/orderCommentForProductIndex";
import orderCommentForShopIndex from "@/views/ordercomment/orderCommentForShopIndex";
import orderCommentForTechIndex from "@/views/ordercomment/orderCommentForTechIndex";
import orderCommentForReplyIndex from "@/views/ordercomment/orderCommentForReplyIndex";

export default {
  name: "tabZujian",
  components: {
    child1: orderCommentForShopIndex,
    child2: orderCommentForTechIndex,
    child3: orderCommentForProductIndex,
    child4: orderCommentForReplyIndex
  },
  data() {
    return {
      //默认第一个选项卡
      activeName: "first",
      isChildUpdate1: true,
      isChildUpdate2: false,
      isChildUpdate3: false,
      isChildUpdate4: false
    };
  },
  mounted() {},
  methods: {
    handleClick(tab) {
      if (tab.name == "first") {
        this.isChildUpdate1 = true;
        this.isChildUpdate2 = false;
        this.isChildUpdate3 = false;
        this.isChildUpdate4 = false;
      } else if (tab.name == "second") {
        this.isChildUpdate1 = false;
        this.isChildUpdate2 = true;
        this.isChildUpdate3 = false;
        this.isChildUpdate4 = false;
      } else if (tab.name == "third") {
        this.isChildUpdate1 = false;
        this.isChildUpdate2 = false;
        this.isChildUpdate3 = true;
        this.isChildUpdate4 = false;
      } else if (tab.name == "fourth") {
        this.isChildUpdate1 = false;
        this.isChildUpdate2 = false;
        this.isChildUpdate3 = false;
        this.isChildUpdate4 = true;
      }
    }
  }
};
</script>

<style>
.tabZujian {
  margin: 0px 0px 0px 25px;
}
</style>
