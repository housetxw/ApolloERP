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
        <el-form-item label>
          <el-input
            v-model="condition.queueNo"
            size="small"
            style="width:300px"
            autocomplete="off"
            placeholder="订单号"
            clearable
          ></el-input>
        </el-form-item>

        <el-form-item label>
          <el-select
            v-model="condition.queueStatus"
            size="small" clearable
            placeholder="请选择状态"
            style="width:150px"
          >
            <el-option
              v-for="item in statusSel"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>
      <!-- <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add">新增</el-button>
      </template>-->
      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <el-table-column type="expand">
          <template slot-scope="props">
            <el-form label-position="left" inline class="demo-table-expand">
              <el-form-item label="备注" v-show="props.row.status == 'UnOccupy'">
                <label>{{props.row.remark}}</label>
              </el-form-item>
            </el-form>
          </template>
        </el-table-column>
        <!-- Table数据 -->
        <!-- <el-table-column label="编号" prop="id" align="center" width="80px;"></el-table-column> -->
        <!-- <el-table-column label="门店名称" align="center" prop="locationName"></el-table-column> -->
        <el-table-column label="订单号" prop="sourceNo" width="150"></el-table-column>
        <el-table-column label="占库状态">
          <template slot-scope="scope">
            <label v-if="scope.row.status == 'New'" style="color:orange;">未占用</label>
            <label v-else-if="scope.row.status == 'UnOccupy'" style="color:blue;">占用失败</label>
            <label v-else-if="scope.row.status == 'Cancel'" style="color:red;">已取消</label>
            <label v-else>已占用</label>
          </template>
        </el-table-column>
        <el-table-column label="处理状态">
          <template slot-scope="scope">
            <label v-if="scope.row.isProcessing == 1">处理中</label>
            <label v-else>处理完成</label>
          </template>
        </el-table-column>

        <el-table-column label="创建时间" prop="createTime" is-datetime fix-min></el-table-column>
        <!-- <el-table-column label="库存金额" align="center" prop="totalCost"></el-table-column> -->
        <el-table-column label="操作" fixed="right" align="center">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                type="primary"
                @click="occupyStock(scope.row)"
                v-if="scope.row.status == 'New' || scope.row.status == 'UnOccupy'"
                size="mini"
              >手动占库</el-button>
              <el-button
                style="margin-left:5px;"
                type="primary"
                @click="canceloccupy(scope.row)"
                v-if="scope.row.status == 'New' || scope.row.status == 'UnOccupy'"
                size="mini"
              >取消</el-button>
              <el-button
                style="margin-left:5px;"
                type="primary"
                @click="viewLogDetail(scope.row)"
                v-if="scope.row.status == 'Occupy'"
                size="mini"
              >占用明细</el-button>
            </el-button-group>
          </template>
        </el-table-column>
        <!-- Table数据 -->
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

    <!-- 占用详情 -->
    <section id="createOrUpdate2">
      <rg-dialog
        :title="logformTitle"
        :visible.sync="logformVisible"
        :beforeClose="logcancel"
        :btnCancel="{label:'关闭',click:(done)=>{logcancel()}}"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="900px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-table border :data="logTableData" style="width: 100%" height="300px" size="mini">
            <el-table-column label="订单号" width="100px" prop="sourceInventoryNo"></el-table-column>            
            <el-table-column label="产品编号" prop="productId"></el-table-column>
            <el-table-column label="产品名称" prop="productName"></el-table-column>
            <el-table-column label="数量" width="60px" prop="num"></el-table-column>
            <el-table-column label="批次号" prop="batchNo" width="80px"></el-table-column>
            <el-table-column label="备注" prop="remark" width="220px"></el-table-column>
            <!-- <el-table-column label="创建时间" is-datetime prop="createTime"></el-table-column> -->
          </el-table>
        </template>
      </rg-dialog>
    </section>
    <!-- 占用详情 -->
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
        queueStatus: undefined,
        queueNo: undefined,
        pageIndex: 1,
        pageSize: 10,
      },

      tableData: [],
      totalList: 0,
      logCondition: {
        sourceInventoryNo: undefined,
      },
      logformTitle: undefined,
      logformVisible: false,
      logTableData: [],

      occupyCondition: {
        id: undefined,
      },

      statusSel: [
        {
          value: "New",
          label: "未占用",
        },
        {
          value: "UnOccupy",
          label: "占用失败",
        },
        {
          value: "Occupy",
          label: "已占用",
        },
        {
          value: "Cancel",
          label: "已取消",
        },
      ],
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    logcancel() {
      this.logformTitle = undefined;
      this.logformVisible = false;
      this.logTableData = [];
      this.logCondition.sourceInventoryNo = undefined;
    },
    // stockFlowcancel() {
    //   this.stockFlowformTitle = undefined;
    //   this.stockFlowformVisible = false;
    //   this.stockFlowTableData = [];
    //   this.stockFlowCondition.productId = undefined;
    //   this.stockFlowCondition.batchNo = undefined;
    // },
    // stockcancel() {
    //   this.stockformVisible = false;
    //   this.stockformTitle = undefined;
    //   this.stockTableData = [];
    //   this.stockCondition.productId = undefined;
    // },

    // viewStockFlowDetail(row) {
    //   this.stockFlowformTitle = "【" + row.productName + "】的库存流水明细";
    //   this.stockFlowformVisible = true;

    //   this.stockFlowCondition.productId = row.productId;
    //   this.stockFlowCondition.batchNo = row.id;
    //   appSvc
    //     .getInventoryFlowItems(this.stockFlowCondition)
    //     .then(
    //       (res) => {
    //         debugger;
    //         //  this.tableData = res.data;
    //         this.stockFlowTableData = res.data;
    //       },
    //       (error) => {
    //         console.log(error);
    //       }
    //     )
    //     .catch(() => {});
    // },

    viewLogDetail(row) {
      this.logformTitle = "【" + row.sourceNo + "】的占用明细";
      this.logformVisible = true;
      this.logCondition.sourceInventoryNo = row.sourceNo;
      appSvc
        .getOccupyItems(this.logCondition)
        .then(
          (res) => {
            //  this.tableData = res.data;
            this.logTableData = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    occupyStock(row) {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          this.occupyCondition.id = row.id;
          appSvc
            .reOccupyStock(this.occupyCondition)
            .then(
              (res) => {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                }
                this.occupyCondition.id = undefined;
                this.search();
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch((error) => {
          console.log(error);
        });
    },
    canceloccupy(row) {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          debugger;
          this.occupyCondition.id = row.id;
          appSvc
            .cancelOrderQueue(this.occupyCondition)
            .then(
              (res) => {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                }
                this.occupyCondition.id = undefined;
                this.search();
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch((error) => {
          console.log(error);
        });
    },
    fetchData() {
      debugger;
      appSvc
        .getOrderQueues(this.condition)
        .then(
          (res) => {
            debugger;
            //  this.tableData = res.data;
            var data = res.data;

            this.tableData = data.items;
            this.totalList = data.totalItems;
          },
          (error) => {
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
      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getOrderQueues(this.condition)
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