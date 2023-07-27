
<template>
  <main class="container">
    <el-table :data="tableData" style="width: 100%" >
      <el-table-column prop="cycleTime" label="统计期间"></el-table-column>
      <el-table-column label="已完工订单">
        <el-table-column label="数量" prop="haveFinishOrderNum"></el-table-column>
        <el-table-column label="金额" prop="haveFinishOrderAmount"></el-table-column>
      </el-table-column>
      <el-table-column label="未完工订单">
        <el-table-column label="数量" prop="notFinishOrderNum"></el-table-column>
        <el-table-column label="金额" prop="notFinishOrderAmount"></el-table-column>
      </el-table-column>
      <el-table-column label="操作">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)" type="primary">查看详情</el-button>
        </template>
      </el-table-column>
    </el-table>
  </main>
</template>

<script>
import { orderCenterSvc } from "@/api/orderCenter/orderCenter";
export default {
  name: "orderStatistics",
  data() {
    return {
      tableData: [],
    };
  },
  created() {
    // 页面初始化函数
    this.getTable();
  },
  mounted() {},
  methods: {
    handleEdit(index, row) {
      this.$router.push({
        name: "orderdetailreport",
        params: {
          dateArea: row.cycleTime,
        },
      });
      console.log(index, row.cycleTime);
    },
    getTable() {
      orderCenterSvc
        .GetOrderStaticReport()
        .then(
          (res) => {
            var data = res.data;
            this.tableData = data;
            // console.log(this.tableData);
            // this.totalList = data.totalItems;
            if (flag) {
              if (res.message != "") {
                this.$message({
                  message: res.message,
                  type: "success",
                });
              }
            }
            // console.log(111, this.tableData);
          },
          (error) => {
            console.log(error);
          }
        )
        .then()
        .catch(() => {})
        .finally(() => {
          //this.tableLoading = false;
        });
    },
  },
};
</script>
<style lang="scss" scoped>
</style>
