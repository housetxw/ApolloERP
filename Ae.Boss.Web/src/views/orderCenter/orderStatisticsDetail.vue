<template>
  <main class="container">
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :tableData="tableData"
      :pageChange="pageTurning"
      :defaultCollapse="false"
      :conditionModel="condition"
      :showReset="false"
      :showSearch="false"
    >
      <template v-slot:tb_cols>
        <rg-table-column prop="orderTime" label="下单日期" align="center" width="80"></rg-table-column>
        <rg-table-column prop="orderNo" label="订单号" align="center" width="80"/>
        <rg-table-column prop="orderStatus" label="订单状态" align="center"  width="80" />
        <rg-table-column prop="contactName" label="车主姓名" align="center" width="100"/>
        <rg-table-column prop="contactPhone" label="车主电话" align="center"  width="100"></rg-table-column>
        <rg-table-column prop="carNumber" label="车牌号" align="center" width="100"></rg-table-column>
        <rg-table-column prop="dispatchTime" label="施工日期" width="100" align="center"></rg-table-column>
        <rg-table-column prop="shopName" label="施工门店" width="140" align="center"></rg-table-column>
        <rg-table-column prop="techName" label="施工技师" width="80" align="center"></rg-table-column>

        <rg-table-column prop="checkName" label="质检技师" width="80" align="center"></rg-table-column>
        <rg-table-column prop="orderAmount" label="订单金额" width="80" align="center"></rg-table-column>
        <rg-table-column prop="actualAmount" label="收款金额" align="center"  width="80"></rg-table-column>
        <rg-table-column prop="discountContent" label="优惠内容" />
        <rg-table-column prop="remark" label="备注" align="center"></rg-table-column>
      </template>
    </rg-page>
  </main>
</template>

<script>
import { orderCenterSvc } from "@/api/orderCenter/orderCenter";
export default {
  name: "orderStatisticsDetail",
  data() {
    return {
      tableData: [],
      tableLoading: false,
      totalList: 0,
      currentPage: 1,
      condition: {
        type: "Week",
        pageIndex: 1,
        pageSize: 20,
      },
    };
  },
  created() {
    // 拿到传过来的日期区间
    let cycleTime = this.$route.params.dateArea;
    if (cycleTime == "本周") {
      this.condition.type = "Week";
    }
    if (cycleTime == "本月") {
      this.condition.type = "Month";
    }
    if (cycleTime == "本年") {
      this.condition.type = "Year";
    }
    console.log(this.condition.type);
    this.getTable();
  },
  methods: {
    getTable() {
      this.tableLoading = true;
      orderCenterSvc
        .GetOrderDetailStaticReport(this.condition)
        .then(
          (res) => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
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
          this.tableLoading = false;
        });
    },
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getTable();
    },
  },
};
</script>
<style lang="scss" scoped>
</style>
