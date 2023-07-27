<template>
  <main class="container">
    <!-- 首页：列表数据 -->
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :headerBtnWidth="160"
      :tableData="tableData"
      :pageChange="pageChange"
      :searching="search"
    >
      <!-- 搜索getShopinfo -->
      <template v-slot:condition>
        <!-- <el-form-item prop="costType">
          <el-select
            v-model="condition.locationId"
            filterable
            remote
            clearable
            reserve-keyword
            placeholder="请输入仓库或门店名称"
            :remote-method="getShopinfo"
            :loading="loading"
            style="width:300px"
          >
            <el-option
              v-for="item in shopSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>
        </el-form-item> -->
        <el-form-item>
          <el-select
            v-model="condition.locationId"
            filterable
            remote
            clearable
            reserve-keyword
            placeholder="请输入仓库或门店名称"
            :remote-method="getSourceWarehouse"
            :loading="loading"
            style="width:300px"
          >
            <el-option-group
              v-for="group in shopSel"
              :key="group.label"
              :label="group.label"
            >
              <el-option
                v-for="item in group.options"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-option-group>
          </el-select>
        </el-form-item>
        <el-form-item label>
          <el-input
            v-model="condition.productId"
            size="small"
            style="width:300px"
            autocomplete="off"
            placeholder="请输入产品名称或编码"
            clearable
          ></el-input>
        </el-form-item>

        <el-form-item label>
          <el-select
            v-model="condition.sourceType"
            size="small"
            placeholder="请选择货物来源"
            style="width:150px"
            clearable
          >
            <el-option
              v-for="item in typeSel"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-switch
            style="display: block"
            v-model="condition.isStock"
            active-color="#13ce66"
            inactive-color="#ff4949"
            active-text="有货"
            inactive-text="无货"
          >
          </el-switch>
        </el-form-item>
      </template>
      <!-- <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add">新增</el-button>
      </template>-->
      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <!-- <el-table-column label="编号" prop="id" align="center" width="80px;"></el-table-column> -->
        <rg-table-column label="仓库名称" align="left" prop="locationName" />
        <rg-table-column label="产品编号" prop="productId" />
        <rg-table-column label="产品名称" prop="productName" />
        <rg-table-column label="库存量" prop="totalNum" is-number />
        <rg-table-column label="单位" prop="uomText" width="90" />
        <!-- <el-table-column label="库存金额" align="center" prop="totalCost"></el-table-column> -->
        <el-table-column label="操作" fixed="right" align="center" width="90">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                type="primary"
                @click="viewStockDetail2(scope.row)"
                size="mini"
                >查看明细</el-button
              >
            </el-button-group>
          </template>
        </el-table-column>
        <!-- Table数据 -->
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

    <!-- 库存明细页面 出入库 -->
    <section id="createOrUpdateNew">
      <rg-dialog
        :title="stockformTitle"
        :visible.sync="stockformVisible2"
        :beforeClose="stockcancel2"
        :btnCancel="{label:'关闭',click:(done)=>{stockcancel2()}}"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="1024px"
        minWidth="750px"
      >
        <template v-slot:content>
          <el-table border :data="stockTableData2" style="width: 100%" height="400px" size="mini">
            <el-table-column label="产品编号" prop="productId" min-width="120px"></el-table-column>
            <!-- <el-table-column label="产品名称" prop="productName" min-width="170px"></el-table-column> -->
            <el-table-column label="出入库单号" prop="inoutId"></el-table-column>
            <el-table-column label="批次" prop="batchNo"></el-table-column>
            <el-table-column label="来源单号" prop="sourceInventoryNo"></el-table-column>
            <el-table-column label="来源明细" prop="releationId"></el-table-column>
            <el-table-column label="类型" prop="type" width="150"></el-table-column>
            <!-- <el-table-column label="数量" prop="actualNum" is-number fix-min></el-table-column> -->
            <el-table-column label="变更量">
              <template slot-scope="scope">
                <label style="color:red;" v-if="scope.row.operationType == '出库'">-{{scope.row.actualNum}}</label>
                <label style="color:blue;" v-else-if="scope.row.operationType == '入库'">+{{scope.row.actualNum}}</label>
                <label style="color:green;" v-else>({{scope.row.actualNum}})</label>
              </template>
            </el-table-column>

            <el-table-column label="单位" prop="uomText"></el-table-column>
            <el-table-column label="操作时间" prop="createTime" is-datetime fix-min width="150"></el-table-column>
            
          </el-table>
        </template>
      </rg-dialog>
    </section>
    <!-- 库存明细页面 -->

    <!-- 库存明细页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        :title="stockformTitle"
        :visible.sync="stockformVisible"
        :beforeClose="stockcancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            stockcancel();
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="1024px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-table
            border
            :data="stockTableData"
            style="width: 100%"
            height="300px"
            size="mini"
          >
            <rg-table-column label="产品编号" prop="productId" />
            <rg-table-column label="产品名称" prop="productName" />
            <rg-table-column label="批次" prop="id" fix-min />
            <rg-table-column
              label="库存量"
              prop="canUseNum"
              is-number
              fix-min
            />
            <rg-table-column
              label="可用量"
              prop="availableNum"
              is-number
              fix-min
            />
            <rg-table-column
              label="占用量"
              prop="occupyNum"
              is-number
              fix-min
            />
            <rg-table-column label="成本" prop="cost" is-money fix-min />
            <el-table-column
              label="操作"
              fixed="right"
              align="center"
              width="90"
            >
              <template slot-scope="scope">
                <el-button-group>
                  <el-button
                    type="primary"
                    @click="viewStockFlowDetail(scope.row)"
                    size="mini"
                    >查看流水</el-button
                  >
                </el-button-group>
              </template>
            </el-table-column>
          </el-table>
        </template>
      </rg-dialog>
    </section>
    <!-- 库存明细页面 -->

    <!-- 库存流水明细 -->
    <section id="createOrUpdate2">
      <rg-dialog
        :title="stockFlowformTitle"
        :visible.sync="stockFlowformVisible"
        :beforeClose="stockFlowcancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            stockFlowcancel();
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="1024px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-table
            border
            :data="stockFlowTableData"
            style="width: 100%"
            height="300px"
            size="mini"
          >
            <rg-table-column label="产品编号" prop="productId" fix-min />
            <rg-table-column label="产品名称" prop="productName" />
            <rg-table-column label="批次" prop="batchNo" fix-min />
            <rg-table-column label="变更量" align="right" fix-min>
              <template slot-scope="scope">
                <label style="color:red;" v-if="scope.row.type == '增加'"
                  >+{{ scope.row.changeTotalNum }}</label
                >
                <label style="color:green;" v-else
                  >-{{ scope.row.changeTotalNum }}</label
                >
              </template>
            </rg-table-column>

            <rg-table-column
              label="操作时间"
              prop="createTime"
              is-datetime
              fix-min
            />
          </el-table>
        </template>
      </rg-dialog>
    </section>
    <!-- 库存流水明细 -->
  </main>
</template>

<script>
import { appSvc } from "@/api/stock/stock";
export default {
  name: "demopage",
  data() {
    return {
      w_search_right: 100,
      tableLoading: false,

      condition: {
        productId: undefined,
        type: undefined,
        pageIndex: 1,
        pageSize: 50,
        locationId: undefined,
        isStock:true //默认是true
      },
      loading: false,
      detailArr: [],
      tableData: [],
      totalList: 0,
      shopSel: [],
      stockformTitle: undefined,
      stockformVisible: false,
      stockformVisible2: false,
      stockTableData: [],
      stockTableData2: [],
      stockCondition: {
        productId: undefined,
        shopId: undefined
      },
      stockFlowformTitle: undefined,
      stockFlowformVisible: false,
      stockFlowTableData: [],
      stockFlowCondition: {
        productId: undefined,
        batchNo: undefined,
        shopId: undefined
      },
      shopSelCondition: {
        simpleName: undefined
      },
      typeSel: [
        {
          value: "Company",
          label: "总部产品"
        },
        {
          value: "Shop",
          label: "外采产品"
        }
      ]
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    getShopinfo(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.simpleName = query;
          appSvc
            .getShopList(this.shopSelCondition)
            .then(
              res => {
                this.shopSel = res.data;
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        }, 200);
      } else {
        this.options = [];
      }
    },
    getSourceWarehouse(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.shopSelCondition.simpleName = query;
          appSvc
            .getSourceWarehouses(this.shopSelCondition)
            .then(
              res => {
                this.shopSel = res.data;
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally(p => {
              this.loading = false;
            });
        }, 200);
      } else {
        this.options = [];
      }
    },
    stockFlowcancel() {
      this.stockFlowformTitle = undefined;
      this.stockFlowformVisible = false;
      this.stockFlowTableData = [];
      this.stockFlowCondition.productId = undefined;
      this.stockFlowCondition.batchNo = undefined;
      this.stockFlowCondition.shopId = undefined;
    },
    stockcancel() {
      this.stockformVisible = false;
      this.stockformTitle = undefined;
      this.stockTableData = [];
      this.stockCondition.productId = undefined;
      this.stockCondition.shopId = undefined;
    },

    stockcancel2() {
      this.stockformVisible2 = false;
      this.stockformTitle = undefined;
      this.stockTableData2 = [];
      this.stockCondition.productId = undefined;
    },

    viewStockFlowDetail(row) {
      this.stockFlowformTitle =
        "【" + row.productName + "】的批次【" + row.id + "】流水明细";
      this.stockFlowformVisible = true;

      this.stockFlowCondition.productId = row.productId;
      this.stockFlowCondition.batchNo = row.id;
      this.stockFlowCondition.shopId = row.shopId;
      appSvc
        .getInventoryFlowItems(this.stockFlowCondition)
        .then(
          res => {
            //  this.tableData = res.data;
            this.stockFlowTableData = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    viewStockDetail(row) {
      this.stockformTitle = "【" + row.productName + "】的库存明细";
      this.stockformVisible = true;
      this.stockCondition.productId = row.productId;
      this.stockCondition.shopId = row.locationId;
      appSvc
        .getInventoryBatchs(this.stockCondition)
        .then(
          res => {
            //  this.tableData = res.data;
            this.stockTableData = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    
    viewStockDetail2(row) {
      this.stockformTitle = "【" + row.productName + "】的库存明细";
      this.stockformVisible2 = true;
      this.stockCondition.productId = row.productId;
      this.stockCondition.shopId = row.locationId;
      appSvc
        .GetStockInoutItemsByPid(this.stockCondition)
        .then(
          (res) => {
            //  this.tableData = res.data;
            this.stockTableData2 = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    fetchData() {
      appSvc
        .getShopStockList(this.condition)
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
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    // pageTurning() {
    //   this.condition.pageIndex = this.currentPage;
    //   this.getPaged();
    // },

    getPaged(flag) {
      var conditionData = this.condition;
      if (
        this.condition.locationId == "" ||
        this.condition.locationId == undefined
      ) {
        // this.$message({
        //   message: "请选择门店!",
        //   type: "warning"
        // });
        // return false;
        conditionData.locationId = undefined;
      }
      else
      {
        var sourceId = this.condition.locationId.split("-")[0];
        conditionData.locationId = sourceId;
      }

      if (this.condition.productId == "") {
        conditionData.productId = undefined;
      }

      if (this.condition.type == "") {
        conditionData.type = undefined;
      }
      this.tableLoading = true;
      console.log("condition: " + JSON.stringify(conditionData));
      appSvc
        .getShopStockList(conditionData)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;

            if (flag) {
              if (res.message != "") {
                this.$message({
                  message: res.message,
                  type: "success"
                });
              }
            }
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
