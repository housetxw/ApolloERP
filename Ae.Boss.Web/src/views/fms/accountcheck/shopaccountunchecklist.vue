<template>
  <div class="child1">
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
              <!-- <el-select
            v-model="condition.locationId"
            size="small"
            clearable
            filterable
            placeholder="请选择门店"
            class="margin-right-10"
          >
            <el-option
              v-for="item in shopSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            >
            </el-option>
              </el-select>-->

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
                style="width: 200px"
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
          <!-- <el-button
              type="primary"
              size="small"
              style="margin-left:30px;"
              @click="search(true)"
              icon="el-icon-search"
              >搜索</el-button
          >-->
          <template v-slot:tb_cols>
            <el-table-column
              label="订单号"
              width="120px"
              prop="orderNo"
            ></el-table-column>
            <el-table-column
              label="安装日期"
              width="120px"
              prop="installTimeStr"
            ></el-table-column>
            <el-table-column
              label="付款方式"
              prop="payMethod"
            ></el-table-column>
            <el-table-column
              label="到账状态"
              prop="moneyArriveStatus"
            ></el-table-column>

            <el-table-column
              label="订单类型"
              prop="orderType"
            ></el-table-column>
            <el-table-column
              label="施工金额"
              prop="saleTotalAmount"
            ></el-table-column>
            <el-table-column
              label="实付金额"
              prop="actualAmount"
            ></el-table-column>

            <!-- <el-table-column label="门店服务结算价" prop="totalCost"></el-table-column> -->
            <el-table-column
              label="服务安装费"
              prop="shopInstallAmount"
            ></el-table-column>
            <el-table-column
              label="铺货成本"
              prop="shopDistributionCost"
            ></el-table-column
            >。
            <el-table-column
              label="铺货毛利"
              prop="shopDistributionGrossProfit"
            ></el-table-column>
            <el-table-column
              label="补差价"
              prop="shopDifferencePrice"
            ></el-table-column>
            <el-table-column
              label="门店其他扣款"
              prop="shopOtherFee"
            ></el-table-column>
            <!-- <el-table-column label="门店其他费用" prop="shopOtherFee"></el-table-column> -->
            <el-table-column
              label="门店外采金额"
              prop="shopItemFee"
            ></el-table-column>
            <el-table-column
              label="门店外采成本"
              prop="shopOutProductCost"
            ></el-table-column>
            <el-table-column
              label="门店手续费"
              prop="shopCommissionFee"
            ></el-table-column>
            <el-table-column
              label="门店结算金额"
              prop="shopSettlementAmount"
            ></el-table-column>
           <!--  <el-table-column
              label="公司名称"
              prop="companyName"
            ></el-table-column>
            <el-table-column
              label="公司返现金额"
              prop="companyBackAmount"
            ></el-table-column>
            <el-table-column
              label="公司其他费用"
              prop="companyOtherFee"
            ></el-table-column>
            <el-table-column
              label="公司扣手续费"
              prop="companyCommissionFee"
            ></el-table-column>
            <el-table-column
              label="公司应结算金额"
              prop="companySettlementAmount"
            ></el-table-column> -->
            <el-table-column label="操作" width="195px" fixed="right">
              <template slot-scope="scope">
                <el-button
                  type="text"
                  size="small"
                  @click="markException(scope.row)"
                  >标记异常</el-button
                >
                <el-button
                  type="text"
                  size="small"
                  @click="viewoprlog(scope.row)"
                  >操作日志</el-button
                >
                <!-- <el-button
                      type="primary"
                      size="small"

                      @click="markException(scope.row)" style="padding:9px 5px;"
                      >标记异常</el-button
                    >
                    <el-button
                      type="primary"
                      size="small" style="padding:9px 5px;"

                      @click="viewoprlog(scope.row)"
                      >操作日志</el-button
                >-->
              </template>
            </el-table-column>
          </template>
        </rg-page>
      </section>
      <!-- 首页 -->

      <!-- 标记异常窗口 -->
      <section id="markExceptionDialog">
        <el-dialog
          width="500px"
          :title="markExceptionFormTitle"
          :close-on-click-modal="false"
          :visible.sync="markExceptionFormVisible"
          :before-close="markExceptionCancel"
        >
          <el-form :model="markExceptionFormModel" ref="markExceptionFormModel">
            <el-form-item label="异常类型:">
              <el-select
                v-model="markExceptionFormModel.reportType"
                size="small"
                clearable
                filterable
                placeholder="请选择异常类型"
                class="margin-right-10"
              >
                <el-option
                  v-for="item in reportTypeSel"
                  :key="item.key"
                  :label="item.value"
                  :value="item.key"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item label="描述:">
              <el-input
                type="textarea"
                :rows="5"
                placeholder="请输入描述"
                v-model="markExceptionFormModel.reportReason"
              ></el-input>
            </el-form-item>
          </el-form>
          <div slot="footer" class="dialog-footer">
            <el-button @click="markExceptionCancel('markExceptionFormModel')"
              >取消</el-button
            >
            <el-button
              type="primary"
              @click="markExceptionSave('markExceptionFormModel')"
              >确定</el-button
            >
          </div>
        </el-dialog>
      </section>
      <!-- 标记异常窗口 -->

      <!-- 查看操作日志窗口 -->
      <section id="accoutlog">
        <el-dialog
          :title="logFormTitle"
          :close-on-click-modal="false"
          :visible.sync="logFormVisible"
          :before-close="logCancel"
        >
          <el-table
            border
            :data="logtableData"
            style="width: 100%"
            :cell-style="cellStyle"
          >
            <el-table-column
              label="编号"
              prop="id"
              width="80px;"
            ></el-table-column>
            <el-table-column
              label="上报类型"
              prop="relationType"
              width="80px;"
            ></el-table-column>
            <el-table-column label="备注" prop="remark"></el-table-column>
            <el-table-column
              label="操作人"
              prop="createBy"
              width="80px;"
            ></el-table-column>
            <el-table-column
              label="操作时间"
              prop="createTime"
              width="150px;"
            ></el-table-column>
          </el-table>

          <div slot="footer" class="dialog-footer">
            <el-button @click="logCancel()">关闭</el-button>
          </div>
        </el-dialog>
      </section>
      <!-- 查看操作日志窗口 -->
    </main>
  </div>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appSvc } from "@/api/fms/accountcheck";

import { isNumber } from "util";

export default {
  name: "child1",
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
        installTime: undefined,
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

      markExceptionFormTitle: "",
      markExceptionFormVisible: false,
      markExceptionFormModel: {
        reportType: undefined,
        reportReason: undefined,
        relationType: undefined,
        relationNo: undefined,
        locationId: undefined,
        locationName: undefined,
      },
      markExceptionFormModelInit: {
        reportType: undefined,
        reportReason: undefined,
        relationType: undefined,
        relationNo: undefined,
        locationId: undefined,
        locationName: undefined,
      },
      reportTypeSel: [],
      reportTypeSelCondition: {
        requestType: 2,
      },
      shopSel: [],
      shopSelCondition: {
        simpleName: undefined,
      },
      logFormTitle: "",
      logFormVisible: false,
      logCondition: {
        relationNo: undefined,
      },
      logtableData: [],
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
    console.log("param:" + JSON.stringify(this.$route.query));
    this.condition.locationId = this.$route.query.locationId;
    //页面初始化函数
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

    markException(row) {
      this.markExceptionFormTitle = "【" + row.orderNo + "】对账异常上报";
      this.markExceptionFormVisible = true;
      this.markExceptionFormModel.locationId = row.locationId;
      this.markExceptionFormModel.locationName = row.locationName;
      appSvc
        .getBasicInfoList(this.reportTypeSelCondition)
        .then(
          (res) => {
            this.reportTypeSel = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
      this.markExceptionFormModel.relationType = "门店";
      this.markExceptionFormModel.relationNo = row.orderNo;
    },
    viewoprlog(row) {
      this.logFormTitle = "【" + row.orderNo + "】的操作日志";
      this.logFormVisible = true;

      this.logCondition.relationNo = row.orderNo;
      appSvc
        .getAccountCheckLogs(this.logCondition)
        .then(
          (res) => {
            this.logtableData = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    updateOrderId() {
      if (this.condition.orderNo == "") {
        this.condition.orderNo = undefined;
      }
    },
    logCancel() {
      this.logFormVisible = false;
    },

    markExceptionCancel() {
      this.markExceptionFormVisible = false;

      this.markExceptionFormModel = JSON.parse(
        JSON.stringify(this.markExceptionFormModelInit)
      );
    },

    markExceptionSave(formName) {
      debugger;
      var vm = this;

      console.log("formModel: " + JSON.stringify(this.markExceptionFormModel));

      this.$confirm("确定标记异常吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          appSvc
            .createAccountCheckException(this.markExceptionFormModel)
            .then(
              (res) => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                  vm.search();
                  this.markExceptionFormVisible = false;

                  this.markExceptionFormModel = JSON.parse(
                    JSON.stringify(this.markExceptionFormModelInit)
                  );
                  var frmName =
                    typeof formName === "string" && formName
                      ? formName
                      : "createtousuFormModel";
                  this.$refs[frmName].resetFields();

                  vm.cancel();
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

    fetchData() {
      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getShopAccountUnChecks(this.condition)
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
      debugger;
      this.tableLoading = true;
      if (
        this.condition.searchLocationId != "" &&
        this.condition.searchLocationId != undefined
      ) {
        this.condition.locationId = this.condition.searchLocationId;
      }
      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getShopAccountUnChecks(this.condition)
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
