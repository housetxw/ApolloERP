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
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-select
            v-model="condition.locationId"
            clearable
            style="width:150px"
            placeholder="请选择仓库"
          >
            <el-option
              v-for="item in warehouseSel"
              :key="item.id"
              :label="item.simpleName"
              :value="item.id"
            ></el-option>
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
            style="width:150px" clearable
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
  inactive-text="无货">
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
        <el-table-column label="仓库名称"  prop="locationName"></el-table-column>
        <el-table-column label="产品编号" prop="productId" width="150"></el-table-column>
        <el-table-column label="产品名称" prop="productName"></el-table-column>
        <el-table-column label="库存量" align="right" prop="totalNum" width="90">
          <template slot-scope="scope">{{scope.row.totalNum.format(0)}}</template>
        </el-table-column>
        <el-table-column label="单位" align="center" prop="uomText" width="90"></el-table-column>
        <!-- <el-table-column label="库存金额" align="center" prop="totalCost"></el-table-column> -->
        <el-table-column label="操作" fixed="right" align="center" width="180">
          <template slot-scope="scope">
            <el-button-group>
              <el-button type="primary" @click="viewStockDetail2(scope.row)" size="mini">查看明细</el-button>
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
            <el-table-column label="仓库" prop="locationName" min-width="120px"></el-table-column>
            <el-table-column label="产品编号" prop="productId" min-width="120px"></el-table-column>
            <!-- <el-table-column label="产品名称" prop="productName" min-width="170px"></el-table-column> -->
            <el-table-column label="出入库单号" prop="inoutId"></el-table-column>
            <!-- <el-table-column label="批次" prop="batchNo"></el-table-column> -->
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
        :btnCancel="{label:'关闭',click:(done)=>{stockcancel()}}"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="900px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-table border :data="stockTableData" style="width: 100%" height="300px" size="mini">
            <el-table-column label="产品编号" prop="productId" min-width="120px"></el-table-column>
            <el-table-column label="产品名称" prop="productName" min-width="170px"></el-table-column>
            <el-table-column label="批次" prop="id"></el-table-column>
            <el-table-column label="库存量" prop="canUseNum" is-number fix-min></el-table-column>
            <el-table-column label="可用量" prop="availableNum" is-number fix-min></el-table-column>
            <el-table-column label="占用量" prop="occupyNum" is-number fix-min></el-table-column>
            <el-table-column label="成本" prop="cost" is-money fix-min></el-table-column>
            <el-table-column label="操作" fixed="right" align="center" width="180">
              <template slot-scope="scope">
                <el-button-group>
                  <el-button type="primary" @click="viewStockFlowDetail(scope.row)" size="mini">查看明细</el-button>
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
        :btnCancel="{label:'关闭',click:(done)=>{stockFlowcancel()}}"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="900px"
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
            <el-table-column label="产品编号" prop="productId" min-width="120px"></el-table-column>
            <!-- <el-table-column label="产品名称" prop="productName" min-width="170px"></el-table-column> -->
            <el-table-column label="批次" prop="batchNo"></el-table-column>
            <el-table-column label="来源单号" prop="sourceNo"></el-table-column>
            <el-table-column label="类型" prop="type"></el-table-column>
            <el-table-column label="变更量">
              <template slot-scope="scope">
                <label style="color:red;" v-if="scope.row.type == '增加'">+{{scope.row.changeTotalNum}}</label>
                <label style="color:blue;" v-else-if="scope.row.type == '占用'">{{scope.row.afterOccupyNum + scope.row.afterLockNum}}</label>
                <label style="color:green;" v-else>-{{scope.row.changeTotalNum}}</label>
              </template>
            </el-table-column>

            <el-table-column label="操作时间" prop="createTime" is-datetime fix-min></el-table-column>
          </el-table>
        </template>
      </rg-dialog>
    </section>
    <!-- 库存流水明细 -->
  </main>
</template>

<script>
import { appSvc } from "@/api/stock/stockmanage";

export default {
  name: "demopage",
  data() {
    return {
      w_search_right: 100,
      tableLoading: false,

      condition: {
        productId: undefined,
        locationId: undefined,
        type: undefined,
        pageIndex: 1,
        pageSize: 50,
        isStock:true //默认是true
      },
      detailArr: [],
      tableData: [],
      totalList: 0,
      warehouseSel: [],

      stockformTitle: undefined,
      stockformVisible: false,
      stockformVisible2: false,
      stockTableData: [],
      stockTableData2: [],
      stockCondition: {
        productId: undefined,
        shopId: undefined,
      },
      stockFlowformTitle: undefined,
      stockFlowformVisible: false,
      stockFlowTableData: [],
      stockFlowCondition: {
        productId: undefined,
        batchNo: undefined,
      },
      typeSel: [
        {
          value: "Company",
          label: "总部产品",
        },
        {
          value: "Shop",
          label: "外采产品",
        },
      ],
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    stockFlowcancel() {
      this.stockFlowformTitle = undefined;
      this.stockFlowformVisible = false;
      this.stockFlowTableData = [];
      this.stockFlowCondition.productId = undefined;
      this.stockFlowCondition.batchNo = undefined;
    },
    stockcancel() {
      this.stockformVisible = false;
      this.stockformTitle = undefined;
      this.stockTableData = [];
      this.stockCondition.productId = undefined;
    },

    stockcancel2() {
      this.stockformVisible2 = false;
      this.stockformTitle = undefined;
      this.stockTableData2 = [];
      this.stockCondition.productId = undefined;
    },

    viewStockFlowDetail(row) {
      this.stockFlowformTitle = "【" + row.productName + "】的批次【"+row.id+"】流水明细";
      this.stockFlowformVisible = true;

      this.stockFlowCondition.productId = row.productId;
      this.stockFlowCondition.batchNo = row.id;
      appSvc
        .getInventoryFlowItems(this.stockFlowCondition)
        .then(
          (res) => {
            debugger;
            //  this.tableData = res.data;
            this.stockFlowTableData = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    viewStockDetail(row) {
      this.stockformTitle = "【" + row.productName + "】的库存明细";
      this.stockformVisible = true;
      this.stockCondition.productId = row.productId;
      appSvc
        .getInventoryBatchs(this.stockCondition)
        .then(
          (res) => {
            //  this.tableData = res.data;
            this.stockTableData = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    
    viewStockDetail2(row) {
      this.stockformTitle = row.locationName +"【" + row.productName + "】的库存明细";
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
      this.getwarehouses();
      this.getPaged();
    },
    getwarehouses() {
      appSvc
        .GetShopWareHouseList()
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
      debugger;
      this.tableLoading = true;

      if (this.condition.productId == "") {
        this.condition.productId = undefined;
      }

      if (this.condition.type == "") {
        this.condition.type = undefined;
      }
      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getShopStockList(this.condition)
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
          },
          (error) => {
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
    },
  },
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