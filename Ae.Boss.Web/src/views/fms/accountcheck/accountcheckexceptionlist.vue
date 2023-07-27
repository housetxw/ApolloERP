<template>
  <div class="child4">
    <main class="container">
      <!-- 首页 -->
      <section id="indexPage">
        <rg-page
          background
          id="indexPage"
          :pageIndex="condition.pageIndex"
          :pageSize="condition.pageSize"
          :dataCount="totalList"
          :tableLoading="tableLoading"
          :tableData="tableData"
          :pageChange="pageTurning"
          :headerBtnWidth="180"
          :searching="search"
          :defaultCollapse="false"
          :cell-style="cellStyle"
        >
          <template v-slot:condition>
            <el-form-item prop="searchLocationId">
              <el-select
                v-model="condition.searchLocationId"
                @change="updateLocation"
                filterable
                remote
                clearable
                reserve-keyword
                placeholder="请输入门店名称"
                :remote-method="getShopinfo"
                :loading="loading"
              >
                <el-option
                  v-for="item in shopSel"
                  :key="item.id"
                  :label="item.simpleName"
                  :value="item.id"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item prop="orderNo">
              <el-input
                v-model="condition.orderNo"
                clearable
                @change="updateOrderId()"
                placeholder="请输入订单号"
                style="width:200px;"
              />
            </el-form-item>
            <el-form-item prop="installTime" label="安装时间">
              <template>
                <el-date-picker
                  v-model="condition.installTime"
                  type="datetimerange"
                  :picker-options="sendpickerOptions"
                  range-separator="至"
                  start-placeholder="开始日期"
                  end-placeholder="结束日期"
                  align="right"
                ></el-date-picker>
              </template>
            </el-form-item>
          </template>
          <!-- <el-input v-model="condition.name" clearable class="el-input el-input--suffix" placeholder="请输入系统名称"></el-input> -->

          <template v-slot:tb_cols>
            <el-table-column label="订单号" width="120px" prop="orderNo"></el-table-column>
            <el-table-column label="安装日期" width="100px" prop="installTimeStr"></el-table-column>
            <el-table-column label="付款方式" width="100px" prop="payMethod"></el-table-column>
            <el-table-column label="到账状态" width="100px" prop="moneyArriveStatus"></el-table-column>

            <el-table-column label="订单类型" width="100px" prop="orderType"></el-table-column>
            <el-table-column
              label="施工金额"
              prop="saleTotalAmount"
            ></el-table-column>
            <el-table-column
              label="实付金额"
              prop="actualAmount"
            ></el-table-column>
            <el-table-column label="服务安装费" prop="shopInstallAmount"></el-table-column>
            <el-table-column label="铺货成本" prop="shopDistributionCost"></el-table-column>。
            <el-table-column label="铺货毛利" prop="shopDistributionGrossProfit"></el-table-column>
            <el-table-column label="补差价" prop="shopDifferencePrice"></el-table-column>
            <el-table-column label="门店其他费用" prop="shopOtherFee"></el-table-column>
            <!-- <el-table-column label="门店其他费用" prop="shopOtherFee"></el-table-column> -->
            <el-table-column label="门店外采金额" prop="shopItemFee"></el-table-column>
            <el-table-column label="门店外采成本" prop="shopOutProductCost"></el-table-column>
            <el-table-column label="门店手续费" prop="shopCommissionFee"></el-table-column>
            <el-table-column label="门店结算金额" prop="shopSettlementAmount"></el-table-column>
            <el-table-column
              label="订单产品明细"
              width="400px"
              prop="productDetail"
            ></el-table-column>
          <!--   <el-table-column label="公司名称" prop="companyName"></el-table-column>
            <el-table-column label="公司返现金额" prop="companyBackAmount"></el-table-column>
            <el-table-column label="公司其他费用" prop="companyOtherFee"></el-table-column>
            <el-table-column label="公司扣手续费" prop="companyCommissionFee"></el-table-column>
            <el-table-column label="公司应结算金额" prop="companySettlementAmount"></el-table-column> -->
            <el-table-column label="上报人" prop="reportBy"></el-table-column>
            <el-table-column label="上报原因" prop="reportReason"></el-table-column>
            <el-table-column label="操作" width="80px" fixed="right">
              <template slot-scope="scope">
                <el-button type="text" size="small" @click="handelException(scope.row)">处理</el-button>
              </template>
            </el-table-column>
          </template>
        </rg-page>
      </section>
      <!-- 首页 -->

      <!-- 标记异常窗口 -->
      <section id="handleDialog">
        <el-dialog
          width="500px"
          :title="handleFormTitle"
          :close-on-click-modal="false"
          :visible.sync="handleFormVisible"
          :before-close="handleCancel"
        >
          <el-form :model="handleFormModel" ref="handleFormModel">
            <el-form-item label="处理意见:">
              <el-input
                type="textarea"
                :rows="5"
                placeholder="请输入处理意见"
                v-model="handleFormModel.suggestion"
              ></el-input>
            </el-form-item>
          </el-form>
          <div slot="footer" class="dialog-footer">
            <el-button @click="handleCancel('handleFormModel')">取消</el-button>
            <el-button type="primary" @click="handleSave('handleFormModel')">确定</el-button>
          </div>
        </el-dialog>
      </section>
      <!-- 标记异常窗口 -->
    </main>
  </div>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appSvc } from "@/api/fms/accountcheck";

import { isNumber } from "util";

export default {
  name: "child4",
  mounted() {
    console.log("tab1组件");
  },
  data() {
    return {
      pickerOptionsStart: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        },
      },

      sendpickerOptions: {
        // disabledDate(time) {
        //   return time.getTime() < Date.now();
        // },
        shortcuts: [
          {
            text: "最近一周",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit("pick", [start, end]);
            },
          },
          {
            text: "最近一个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit("pick", [start, end]);
            },
          },
          {
            text: "最近三个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit("pick", [start, end]);
            },
          },
        ],
      },
      input: undefined,
      actualInStockNum: 1,
      readonly: true,
      tableLoading: false,
      currentPage: 1,
      loading: false,
      //table页面的条件
      condition: {
        locationId: undefined,
        orderNo: undefined,
        installTime: [],
        pageIndex: 1,
        pageSize: 10,
        searchLocationId: undefined,
      },
      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",

      formLabelWidth: "120px",

      shopSel: [],
      shopSelCondition: {
        simpleName: undefined,
      },
      handleFormTitle: "",
      handleFormVisible: false,
      handleFormModel: {
        suggestion: undefined,
        orderNo: undefined,
        locationId: undefined,
        id: undefined,
      },
      handleFormModelInit: {
        suggestion: undefined,
        orderNo: undefined,
        locationId: undefined,
        id: undefined,
      },
    };
  },
  computed: {
    routeComputed: {
      get: function () {
        return this.formModel.route;
      },
      set: function (val) {
        this.formModel.route = val.toLowerCase();
      },
    },
  },
  created() {
    //页面初始化函数
    console.log("param:" + JSON.stringify(this.$route.query));
    this.condition.locationId = this.$route.query.locationId;
    this.fetchData();
  },
  methods: {
    updateLocation() {
      if (this.condition.searchLocationId == "") {
        this.condition.searchLocationId = undefined;
        this.condition.locationId = this.$route.query.locationId;
      }
    },
    getShopinfo(query) {
      if (query != "") {
        debugger;
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.simpleName = query;
          appSvc
            .getShopListAsync(this.shopSelCondition)
            .then(
              (res) => {
                var resData = res.data;
                this.shopSel = resData.items;
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        }, 200);
      } else {
        this.options = [];
      }
    },
    handelException(row) {
      this.handleFormTitle = "对账异常处理";
      this.handleFormVisible = true;
      this.handleFormModel.orderNo = row.orderNo;
      this.handleFormModel.id = row.id;
    },
    handleSave(formName) {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          debugger;
          this.handleFormModel.locationId = this.condition.locationId;
          var vm = this;
          console.log("formModel: " + JSON.stringify(this.handleFormModel));

          appSvc
            .accountCheckExceptionHandle(this.handleFormModel)
            .then(
              (res) => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                  vm.search();
                  this.handleFormVisible = false;
                  this.handleFormModel = JSON.parse(
                    JSON.stringify(this.handleFormModelInit)
                  );
                } else {
                  this.$message({
                    message: res.message,
                    type: "warning",
                  });
                }
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },
    handleCancel(formName) {
      this.handleFormVisible = false;

      this.handleFormModel = JSON.parse(
        JSON.stringify(this.handleFormModelInit)
      );
    },
    cellStyle(row, column, rowIndex, columnIndex) {
      if (row.row.priority === "紧急" && row.column.label === "优先级") {
        return "color:red";
      }

      if (row.row.status === "驳回" && row.column.label === "状态") {
        return "color:orange";
      }
      // else if(row.column.label==="告警级别"&& row.row.alarmLevel==="一般告警" ){
      //   return 'color:yellow'
      // }
    },
    fetchData() {
      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getAccountCheckExceptionCollectList(this.condition)
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

      appSvc
        .getBasicInfoList(this.shopSelCondition)
        .then(
          (res) => {
            this.shopSel = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },

    getPaged(flag) {
      this.tableLoading = true;
      if (
        this.condition.searchLocationId != "" &&
        this.condition.searchLocationId != undefined
      ) {
        this.condition.locationId = this.condition.searchLocationId;
      }
      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getAccountCheckExceptionCollectList(this.condition)
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
>>> .el-table {
  height: calc(100vh - 300px) !important;
}
</style>
