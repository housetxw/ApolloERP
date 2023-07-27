<template>
  <main class="container">
    <el-table border     show-summary :summary-method="getSummaries"
 :data="tableData" style="width: 100%">
      <rg-table-column label="出入库编号" prop="inOutId" />
      <rg-table-column label="关联单号" prop="relationId" />
      <rg-table-column label="操作类型" prop="operateType" />
      <rg-table-column label="仓库名称" prop="locationName" />
      <rg-table-column label="产品编号" prop="productId" />
      <rg-table-column label="产品名称" prop="productName" />
      <rg-table-column label="数量" prop="num" />
      <rg-table-column label="批次" prop="batchNo" />
      <rg-table-column label="备注" prop="remark" />
      <rg-table-column label="操作人" prop="operator" />
      <rg-table-column label="操作时间" prop="operateTime" />
    </el-table>
  </main>
</template>

<script>
import { appSvc } from "@/api/stock/stock";
export default {
  data() {
    return {
      w_search_right: 100,
      tableLoading: false,

      condition: {
        productId: undefined,
        times: undefined,
        locationId: undefined,
        locationType: 1,
        searchType: undefined
      },
      loading: false,

      tableData: []
    };
  },
  created() {
    //要获取$router里面的参数 初始化方法
    debugger;
    var param = this.$route.params;
    this.condition.productId = param.productId;
    this.condition.locationId = param.locationId;
    this.condition.locationType = param.locationType;
    this.condition.searchType = param.searchType;
    this.condition.times = param.times;
    this.fetchData();
  },
  methods: {
    fetchData() {
      const loading = this.$loading({
        lock: true,
        text: "拼命加载中...",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)"
      });
      appSvc
        .getProductStockDetails(this.condition)
        .then(
          res => {
            //  this.tableData = res.data;
            this.tableData = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    },
      getSummaries(param) {
      const { columns, data } = param;
      const sums = [];
      columns.forEach((column, index) => {
        if (index === 0) {
          sums[index] = "合计";
          return;
        }
        const values = data.map(item => Number(item[column.property]));
        if (column.property == "num") {
          sums[index] = values.reduce((prev, curr) => {
            const value = Number(curr);
            if (!isNaN(value)) {
              return prev + curr;
            } else {
              return prev;
            }
          }, 0);
          sums[index];
        }
      });
      return sums;
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
