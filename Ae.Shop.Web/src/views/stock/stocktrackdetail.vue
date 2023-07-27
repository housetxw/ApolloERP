//查询批次的链路追踪
<template>
  <main class="container">
    <section>
      <el-table border :data="tableData" style="width: 100%">
        <el-table-column type="expand">
          <template slot-scope="props">
            <div v-if="props.row.trackType === '入库'">
              <el-table
                border
                :data="props.row.stockTracks"
                style="width: 100%"
                :cell-style="cellStyle"
              >
                <el-table-column label="关联单号" prop="relationNo"></el-table-column>

                <el-table-column label="关联类型" prop="relationType"></el-table-column>
                <el-table-column label="源供应商" prop="sourceWarehouseName"></el-table-column>
                <el-table-column label="目标仓" prop="targetWarehouseName"></el-table-column>
                <el-table-column label="产品编号" prop="prdocutId"></el-table-column>
                <el-table-column label="产品名称" prop="productName"></el-table-column>

                <el-table-column label="总量" prop="sumNum"></el-table-column>

                <el-table-column label="入库时间" prop="createTimeStr"></el-table-column>
              </el-table>
            </div>
            <div v-else-if="props.row.trackType === '占用'">
              <el-table
                border
                :data="props.row.stockTracks"
                style="width: 100%"
                :cell-style="cellStyle"
              >
                <el-table-column label="关联单号" prop="relationNo"></el-table-column>
                <el-table-column label="关联产品单号" prop="relationProductId"></el-table-column>
                <el-table-column label="关联类型" prop="relationType"></el-table-column>
                <el-table-column label="源仓库" prop="sourceWarehouseName"></el-table-column>
                <el-table-column label="目标仓/门店" prop="targetWarehouseName"></el-table-column>
                <el-table-column label="产品编号" prop="prdocutId"></el-table-column>
                <el-table-column label="产品名称" prop="productName"></el-table-column>

                <el-table-column label="占用量" prop="occupyNum"></el-table-column>
                <el-table-column label="占用类型" prop="occupyType"></el-table-column>
                <el-table-column label="占用时间" prop="createTimeStr"></el-table-column>
              </el-table>
            </div>
            <div v-else-if="props.row.trackType === '出库'">
              <el-table
                border
                :data="props.row.stockTracks"
                style="width: 100%"
                :cell-style="cellStyle"
              >
                <el-table-column label="关联单号" prop="relationNo"></el-table-column>

                <el-table-column label="关联类型" prop="relationType"></el-table-column>
                <el-table-column label="源仓库" prop="sourceWarehouseName"></el-table-column>
                <el-table-column label="目标仓" prop="targetWarehouseName"></el-table-column>
                <el-table-column label="产品编号" prop="prdocutId"></el-table-column>
                <el-table-column label="产品名称" prop="productName"></el-table-column>

                <!-- <el-table-column label="总量" prop="sumNum"></el-table-column> -->
                <el-table-column label="占用量" prop="occupyNum"></el-table-column>

                <el-table-column label="出库时间" prop="createTimeStr"></el-table-column>
              </el-table>
            </div>
          </template>
        </el-table-column>

        <el-table-column label="扭转类型">
          <template slot-scope="scope">
            <h3>{{scope.row.trackType}}</h3>
          </template>
        </el-table-column>

        <el-table-column label="总数量" prop="sumNum"></el-table-column>
      </el-table>
    </section>
  </main>
</template>

            
<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appSvc } from "@/api/stock/stock";

import { isNumber } from "util";

export default {
  data() {
    return {
      tabloading: false,
      tableData: [],
      condition: {
        targetWarehouse: undefined,
        productName: undefined,
        batchId: undefined,
        trackType: 2,
        toStockId: undefined
      }
    };
  },
  mounted() {},
  created() {
    debugger;
    this.condition.targetWarehouse = this.$route.params.targetWarehouse;
    this.condition.productName = this.$route.params.prdocutId;
    this.condition.batchId = this.$route.params.batchId;
    this.condition.toStockId = this.$route.params.toStockId;

    //页面初始化函数
    this.fetchData();
  },
  methods: {
    cellStyle(row, column, rowIndex, columnIndex) {
      //根据报警级别显示颜色
      // console.log(row);
      // console.log(row.column);
      if (row.row.relationType === "订单" && row.column.label === "关联类型") {
        return "color:orange";
      }
      if (row.row.relationType === "移库" && row.column.label === "关联类型") {
        return "color:#409eff";
      }

      // if (row.row.relationType === "订单" && row.column.label === "关联类型") {
      //   return "color:#409eff";
      // }

      if (
        row.row.targetWarehouseName === "客户" &&
        row.column.label === "目标仓"
      ) {
        return "color:red";
      }
      if (
        row.row.targetWarehouseName === "在途" &&
        row.column.label === "目标仓"
      ) {
        return "color:#409eff";
      }

      if (
        row.row.sourceWarehouseName === "在途" &&
        row.column.label === "源仓库"
      ) {
        return "color:orange";
      }

      // else if(row.column.label==="告警级别"&& row.row.alarmLevel==="一般告警" ){
      //   return 'color:yellow'
      // }
    },
    fetchData() {
      appSvc
        .getStockTrackDetail(this.condition)
        .then(
          res => {
            this.tableData = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    }
  }
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  margin-top: 30px;
  .bodyTop {
    height: 128px;
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