<template>
  <div class="child1">
    <main class="container">
      <!-- 首页：列表数据 -->
      <section id="indexPage">
        <header class="bodyTop">
          <article class="filter-container">
            <el-input
              v-model="condition.productId"
              size="small"
              style="width:300px"
              autocomplete="off"
              placeholder="请输入产品名称或编码"
              clearable
            ></el-input>
            &nbsp;&nbsp;
            <el-date-picker
              v-model="condition.times"
              type="datetimerange"
              :picker-options="sendpickerOptions"
              range-separator="至"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              align="right"
            >
            </el-date-picker>
            <el-button
              type="primary"
              size="small"
              style="margin-left:30px;"
              @click="search(true)"
              icon="el-icon-search"
              >搜索</el-button
            >
          </article>
        </header>
        <main>
          <el-table
            :span-method="cellMerge"
            border
            :data="tableData"
            style="width: 100%"
          >
            <rg-table-column label="仓库名称" prop="locationName" />
            <rg-table-column label="产品编号" prop="productId" />
            <rg-table-column label="产品名称" prop="productName" />
            <rg-table-column label="采购入库量" is-number>
              <template slot-scope="scope">
                <div v-if="scope.row.purchaseInStockNum > 0">
                  <el-button
                    @click="getdetailNum(scope.row,1)"
                    size="mini"
                    type="text"
                    >{{ scope.row.purchaseInStockNum }}</el-button >
                </div>
                <div v-else>
                  <label>{{ scope.row.purchaseInStockNum }}</label>
                </div>
              </template>
            </rg-table-column>
            <rg-table-column label="其他入库量">
              <template slot-scope="scope">
                <div v-if="scope.row.otherInStockNum > 0">
                  <el-button
                    @click="getdetailNum(scope.row,3)"
                    size="mini"
                    type="text"
                    >{{ scope.row.otherInStockNum }}</el-button >
                </div>
                <div v-else>
                  <label>{{ scope.row.otherInStockNum }}</label>
                </div>
              </template>
            </rg-table-column>
            <rg-table-column label="其他出库量" is-number>
              <template slot-scope="scope">
                <div v-if="scope.row.otherOutStockNum > 0">
                   <el-button
                    @click="getdetailNum(scope.row,2)"
                    size="mini"
                    type="text"
                    >{{ scope.row.otherOutStockNum }}</el-button >
                </div>
                <div v-else>
                  <label>{{ scope.row.otherOutStockNum }}</label>
                </div>
              </template>
            </rg-table-column>
            <rg-table-column label="在途量" is-number>
              <template slot-scope="scope">
                <div v-if="scope.row.onRoadStockNum > 0">
                  <el-button
                    @click="getdetailNum(scope.row,4)"
                    size="mini"
                    type="text"
                    >{{ scope.row.onRoadStockNum }}</el-button >
                </div>
                <div v-else>
                  <label>{{ scope.row.onRoadStockNum }}</label>
                </div>
              </template>
            </rg-table-column>
              <rg-table-column label="占用量" is-number>
              <template slot-scope="scope">
                <div v-if="scope.row.occupyNum > 0">
                  <el-button
                    @click="getdetailNum(scope.row,6)"
                    size="mini"
                    type="text"
                    >{{ scope.row.occupyNum }}</el-button >
                </div>
                <div v-else>
                  <label>{{ scope.row.occupyNum }}</label>
                </div>
              </template>
            </rg-table-column>
            <rg-table-column label="剩余库存量" is-number>
              <template slot-scope="scope">
                <label>{{ scope.row.availableStockNum }}</label>
                <!-- <div v-if="scope.row.availableStockNum > 0">
                <router-link
                  :to="{
                    name: 'availablestockdetail',
                    params: {
                      orderNo: scope.row.availableStockNum,
                      onlyview: 1
                    }
                  }"
                >
                  <el-button size="mini" type="text">{{
                    scope.row.availableStockNum
                  }}</el-button>
                </router-link>
              </div>
              <div v-else>
                <label>{{ scope.row.availableStockNum }}</label>
              </div> -->
              </template>
            </rg-table-column>
          </el-table>
        </main>
      </section>

      <!-- 首页：列表数据 -->
    </main>
  </div>
</template>

<script>
import { appSvc } from "@/api/stock/stock";
export default {
  name: "child1",
  data() {
    return {
      sendpickerOptions: {
        shortcuts: [
          {
            text: "最近一周",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近一个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近三个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit("pick", [start, end]);
            }
          }
        ]
      },
      w_search_right: 100,
      tableLoading: false,

      condition: {
        productId: undefined,
        times: undefined,
        locationType: 1
      },
      loading: false,
      spanArr: [],
      tableData: [],
      totalList: 0
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    getdetailNum(row, type) {
      this.$router.push({
        name: "productstockdetail",
        params: {
          locationId: row.locationId,
          productId: row.productId,
          locationType: 1,
          searchType: type,
          times: this.condition.times
        }
      });
    },

    defaultDate() {
      const end = new Date();
      const start = new Date();
      start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
      //picker.$emit("pick", [start, end]);
      this.condition.times = [start, end];
    },
    fetchData() {
      this.defaultDate();

      const loading = this.$loading({
        lock: true,
        text: "拼命加载中...",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)"
      });
      appSvc
        .getProductStocks(this.condition)
        .then(
          res => {
            //  this.tableData = res.data;
            this.tableData = res.data;
            this.getSpanArr(this.tableData);
            // this.tableData = data.items;
            // this.totalList = data.totalItems;
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
    search(flag) {
      // this.condition.pageIndex = this.currentPage = 1;
      this.getPaged();
    },
    getPaged() {
      const loading = this.$loading({
        lock: true,
        text: "Loading",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)"
      });
      appSvc
        .getProductStocks(this.condition)
        .then(
          res => {
            //  this.tableData = res.data;
            this.tableData = res.data;
            this.getSpanArr(this.tableData);
            // this.tableData = data.items;
            // this.totalList = data.totalItems;
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
    cellMerge({ row, column, rowIndex, columnIndex }) {
      if (columnIndex === 0) {
        const _row = this.spanArr[rowIndex];
        const _col = _row > 0 ? 1 : 0;
        return {
          rowspan: _row,
          colspan: _col
        };
      }
    },
    getSpanArr(data) {
      this.spanArr = [];
      this.pos = undefined;
      for (var i = 0; i < data.length; i++) {
        if (i === 0) {
          this.spanArr.push(1);
          this.pos = 0;
        } else {
          // 判断当前元素与上一个元素是否相同
          if (data[i].locationId === data[i - 1].locationId) {
            this.spanArr[this.pos] += 1;
            this.spanArr.push(0);
          } else {
            this.spanArr.push(1);
            this.pos = i;
          }
        }
      }
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
