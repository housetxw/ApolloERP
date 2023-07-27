<template>
  <div class="child2">
    <main class="container">
      <!-- 首页 -->
      <section id="indexPage">
        <header class="bodyTop">
          <article class="filter-container">
            <span class="input-label">产品名称:</span>
            <el-input
              v-model="condition.productId"
              style="width:200px;"
              clearable
              placeholder="请输入产品名称或编码"
            />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <span class="input-label">供应商:</span>
            <el-select
              v-model="condition.venderId"
              size="small"
              clearable
              filterable
              placeholder="请选择供应商"
              class="margin-right-10"
            >
              <el-option
                v-for="item in venderSel"
                :key="item.id"
                :label="item.venderShortName"
                :value="item.id"
              ></el-option>
            </el-select>

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
            <el-table border :data="tableData" style="width: 100%"    :cell-style="cellStyle">
              <el-table-column label="仓库名称" prop="location" width="200px"></el-table-column>
              <el-table-column label="产品编号" prop="productId"></el-table-column>
              <el-table-column label="产品名称" prop="productName"></el-table-column>

              <el-table-column label="库存总量" prop="totalNum" width="120px"></el-table-column>
              <el-table-column label="总成本" prop="totalCost" width="150px"></el-table-column>
              <el-table-column label="总占用量" width="120px" prop="totalOccupyNum"></el-table-column>
              <el-table-column label="总可用量" width="120px" prop="totalAvailableNum"></el-table-column>

              <el-table-column label="操作" width="100px">
                <template slot-scope="scope">
                  <router-link
                    :to="{ path: 'stocktracklist', query: { sourceWarehouse: scope.row.locationId,productName:scope.row.productId } }"
                  >
                    <el-button size="mini" type="primary">详情</el-button>
                  </router-link>
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

      venderSel: [],
      condition: {
        //产品编号
        productId: undefined,
        trackType: 1,
        venderId: undefined
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
    this.fetchData();
  },
  methods: {

 cellStyle(row, column, rowIndex, columnIndex) {
      //根据报警级别显示颜色
      // console.log(row);
      // console.log(row.column);
      if (row.row.totalAvailableNum === 0 && row.column.label === "总可用量") {
        return "color:red";
      }
      else if (row.row.totalOccupyNum === 0 && row.column.label === "总占用量") {
        return "color:orange";
      }
      
      // else if(row.column.label==="告警级别"&& row.row.alarmLevel==="一般告警" ){
      //   return 'color:yellow'
      // }
    },

    fetchData() {
      this.getVenders();
    },
    getVenders() {
      appSvc
        .getVenders()
        .then(
          res => {
            this.venderSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    search() {
      debugger;

      this.tableLoading = true;

      if (this.condition.venderId != undefined && this.condition.venderId > 0) {
        console.log("condition: " + JSON.stringify(this.condition));
        appSvc
          .getStockLocationTrackList(this.condition)
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
      } else {
        this.$message({
          message: "请选择供应商!",
          type: "warning"
        });
        return;
      }
    }
  }
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 88px;
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


