// // 供应商/仓库查询 查询产品的具体占用
<template>
  <div class="child2">
    <main class="container">
      <!-- 首页 -->
      <section id="indexPage">
        <header class="bodyTop">
          <article class="filter-container">
            <span class="input-label">产品名称:</span>
            <el-input
              v-model="condition.orginProductName"
              style="width:200px;"
              clearable
              @change="updateorginProductName"
              placeholder="请输入产品名称或编码"
            />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <span class="input-label">仓库:</span>
            <el-select
              v-model="condition.orginSourceWarehouse"
              size="small"
              @change="updateOrginSourceWarehouse"
              clearable
              filterable
              placeholder="请选择仓库"
              class="margin-right-10"
            >
              <el-option
                v-for="item in warehouseSel"
                :key="item.id"
                :label="item.warehouseName"
                :value="item.id"
              ></el-option>
            </el-select>
            <el-switch
             
              v-model="searchFlag"
              active-color="#13ce66"
              inactive-color="#ff4949"
              active-text="有库存"
              inactive-text="无库存"
            ></el-switch>
            <el-button
              type="primary"
              size="small"
              style="margin-left:30px;"
              @click="search(true)"
              icon="el-icon-search"
            >搜索</el-button>
          </article>
        </header>
        <main>
          <section>
            <el-table border :data="tableData" style="width: 100%" :cell-style="cellStyle">
              <el-table-column label="仓库名称" prop="location" width="200px"></el-table-column>
              <el-table-column label="产品编号" prop="productId"></el-table-column>
              <el-table-column label="产品名称" prop="productName"></el-table-column>
              <el-table-column label="入库批次" prop="batchId" width="120px"></el-table-column>
              <el-table-column label="总量" prop="num" width="100px"></el-table-column>
              <el-table-column label="总成本" prop="totalCost" width="120px"></el-table-column>
              <el-table-column label="占用量">
                <el-table-column label="订单" width="100px" prop="orderOccupyNum"></el-table-column>
                <el-table-column label="退货" width="100px" prop="returnOccupyNum"></el-table-column>
                <el-table-column label="调拨" width="100px" prop="allotOccupyNum"></el-table-column>                
                <el-table-column label="出库" width="100px" prop="transferOccupyNum"></el-table-column>
              </el-table-column>
              <el-table-column label="可用量" width="100px" prop="availableNum"></el-table-column>

              <el-table-column label="操作" width="120px">
                <template slot-scope="scope">
                  <div v-if="scope.row.availableNum>0">
                    <el-button size="mini" type="primary" @click="stockDetail(scope.row)">链路详情</el-button>
                  </div>
                  <div v-else-if="scope.row.availableNum<=0">
                    <el-button
                      size="mini"
                      type
                      style="background-color:gray;color:white;border:1px solid gray"
                      @click="stockDetail(scope.row)"
                    >链路详情</el-button>
                  </div>
                </template>
              </el-table-column>
            </el-table>
          </section>
        </main>
      </section>
      <!-- 首页 -->
    </main>
  </div>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appSvc } from "@/api/stock/stock";

import { isNumber } from "util";

export default {
  name: "child2",
  mounted() {
    console.log("tab1组件");
  },
  data() {
    return {
      readonly: true,
      tableLoading: false,
      currentPage: 1,
      flag: this.global.deletedFlag,

      warehouseSelCondition: {
        RequestType: 5
      },
      warehouseSel: [],
      searchFlag: true,
      condition: {
        //产品编号
        productName: undefined,
        sourceWarehouse: undefined,
        orginSourceWarehouse: undefined,
        orginProductName: undefined,
        searchFlag: undefined
      },
      tableData: [],
      totalList: 0
    };
  },
  computed: {
    routeComputed: {
      get: function() {
        return this.formModel.route;
      },
      set: function(val) {
        this.formModel.route = val.toLowerCase();
      }
    }
  },
  created() {
    console.log("param:" + JSON.stringify(this.$route.query));
    var sourceWarehouse = this.$route.query.sourceWarehouse;
    var productName = this.$route.query.productName;

    // this.condition.orginSourceWarehouse=sourceWarehouse;
    // this.condition.productName = productName;

    this.condition.sourceWarehouse = sourceWarehouse;
    this.condition.productName = productName;
    this.condition.searchFlag = 1; //默认查询有库存的记录
    this.fetchData();
    this.getwarehouses();
  },
  methods: {
    getwarehouses() {
      appSvc
        .getBasicInfoList(this.warehouseSelCondition)
        .then(
          res => {
            this.warehouseSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    cellStyle(row, column, rowIndex, columnIndex) {
      //根据报警级别显示颜色
      // console.log(row);
      // console.log(row.column);
      if (row.row.availableNum === 0 && row.column.label === "可用量") {
        return "color:red";
      }

      // else if(row.column.label==="告警级别"&& row.row.alarmLevel==="一般告警" ){
      //   return 'color:yellow'
      // }
    },

    fetchData() {
      appSvc
        .getStockLocationTrackDetails(this.condition)
        .then(
          res => {
            this.tableData = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});

      //this.getVenders();
    },
    updateOrginSourceWarehouse() {
      if (this.condition.orginSourceWarehouse === "") {
        this.condition.sourceWarehouse = this.$route.query.sourceWarehouse;
      }
    },
    updateorginProductName() {
      if (this.condition.orginProductName == "") {
        this.condition.productName = this.$route.query.productName;
      }
    },
    stockDetail(row) {
      this.$router.push({
        name: "stocktrackdetail",
        params: {
          targetWarehouse: row.locationId,
          productName: row.productName,
          batchId: row.batchId,
          trackType: 2,
          toStockId: row.stockId
        }
      });
    },

    search() {
      debugger;

      this.tableLoading = true;

      if (
        this.condition.orginSourceWarehouse == undefined ||
        this.condition.orginSourceWarehouse <= 0
      ) {
        this.$message({
          message: "仓库不能为空!",
          type: "warning"
        });
        return;
      }

      if (
        this.condition.orginProductName == undefined ||
        this.condition.orginProductName == ""
      ) {
        this.$message({
          message: res.message,
          type: "warning"
        });
        return;
      }

      this.condition.productName = this.condition.orginProductName;
      this.condition.sourceWarehouse = this.condition.orginSourceWarehouse;

      if (this.searchFlag) {
        this.condition.searchFlag = 1;
      } else {
        this.condition.searchFlag = 2;
      }

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getStockLocationTrackDetails(this.condition)
        .then(
          res => {
            this.tableData = res.data;
            this.tableLoading = false;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          console.log("tableLoading: false");
          this.tableLoading = false;
        });
    }
  }
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 90px;
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


