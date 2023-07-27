<template>
  <main class="container">
    <rg-page
      id="indexPage"
      :tableLoading="tableLoading"
      :tableData="tableData"
      :conditionModel="condition"
      :defaultCollapse="false"
    >
      <template v-slot:tb_cols>
        <rg-table-column label="仓库名称" prop="locationName" />
        <rg-table-column label="产品编号" prop="productId" />
        <rg-table-column label="产品名称" prop="productName" />
        <rg-table-column label="数量" prop="num" />
      </template>
    </rg-page>
  </main>
</template>

<script>
import { appSvc } from "@/api/stock/stock";
export default {
  name: "child3",
  data() {
    return {
      w_search_right: 100,
      tableLoading: false,

      condition: {
        productId: undefined,
        times: undefined,
        pageIndex: 1,
        pageSize: 50
      },
      loading: false,

      tableData: [],
      totalList: 0
    };
  },
  created() {
    //要获取$router里面的参数 初始化方法

    this.fetchData();
  },
  methods: {
    fetchData() {
      appSvc
        .getAvailableProductStocks(this.condition)
        .then(
          res => {
            //  this.tableData = res.data;
            var data = res.data;

            this.tableData = data.items;
            this.totalList = data.totalItems;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    }
  }
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 48px;
    display: flex;
    align-items: center;
  }
  .pagination {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100px;
  }
}
</style>
