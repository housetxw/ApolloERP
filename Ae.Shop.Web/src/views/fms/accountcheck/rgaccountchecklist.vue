<template>
  <div class="child3">
    <main class="container">
      <!-- 首页 -->
      <section id="indexPage">
        <header class="bodyTop">
          <article class="filter-container">
            <span class="input-label">门店名称:</span>
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
              >
              </el-option>
            </el-select>

            <span class="input-label">订单号:</span>
            <el-input
              v-model="condition.orderNo"
              clearable
              @change="updateOrderId()"
              placeholder="请输入订单号"
              style="width:200px;"
            />

            &nbsp;&nbsp;&nbsp;<span class="input-label">安装时间:</span>
            <template>
              <el-date-picker
                v-model="condition.installTime"
                type="datetimerange"
                :picker-options="sendpickerOptions"
                range-separator="至"
                start-placeholder="开始日期"
                end-placeholder="结束日期"
                align="right"
              >
              </el-date-picker>
            </template>

            <!-- <el-input v-model="condition.name" clearable class="el-input el-input--suffix" placeholder="请输入系统名称"></el-input> -->
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
          <section>
            
            <el-table
              border
              ref="multipleTable"
              @selection-change="handleSelectionChange"
              :data="tableData"
              style="width: 100%"
              :cell-style="cellStyle"
            >
              <el-table-column type="selection"> </el-table-column>
              <el-table-column
                label="订单号"
                width="120px"
                prop="orderNo"
              ></el-table-column>
              <el-table-column
                label="安装日期"
                width="100px"
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
                label="销售总价"
                prop="saleTotalAmount"
              ></el-table-column>

              <el-table-column
                label="门店服务结算价"
                prop="totalCost"
              ></el-table-column>
              <el-table-column
                label="订单服务费"
                prop="serviceFee"
              ></el-table-column>
              <el-table-column
                label="扣手续费"
                prop="commissionFee"
              ></el-table-column>
              <el-table-column label="扣其他" prop="otherFee"></el-table-column>
              <el-table-column
                label="应结算"
                prop="settlementFee"
              ></el-table-column>
              <el-table-column
                label="门店核对结果"
                prop="shopCheckResult"
                width="150px"
              ></el-table-column>
              <el-table-column
                label="总部核对结果"
                prop="rgCheckResult"
                width="150px"
              ></el-table-column>
              <el-table-column width="150px">
                <template slot="header">
                  <label>操作</label>
                  <el-button type="text" size="small" :disabled="true" @click="allwithdraw()"
                    >全部申请提现</el-button
                  >
                </template>
                <template slot-scope="scope">
                  <el-button
                    type="text"
                    size="small" :disabled="true"
                    @click="withdraw(scope.row)"
                    >申请提现</el-button
                  >
                  <el-button
                    type="text"
                    size="small"
                    @click="viewoprlog(scope.row)"
                    >操作日志</el-button
                  >
                </template>
              </el-table-column>
            </el-table>
          </section>
          <section class="pagination">
            <el-pagination
              background
              :page-size="10"
              :page-sizes="[10, 30, 50, 100]"
              layout="total, sizes, prev, pager, next, jumper"
              :total="totalList"
              @current-change="pageTurning"
              :current-page.sync="currentPage"
              @size-change="sizeChange"
            ></el-pagination>
          </section>
        </main>
      </section>
      <!-- 首页 -->

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
  name: "child3",
  mounted() {
    console.log("tab1组件");
  },
  data() {
    return {
      pickerOptionsStart: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        }
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
        searchLocationId: undefined
      },
      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",

      formLabelWidth: "120px",

      withdrawCondition: {
        orderNos: [],
        locationId: undefined,
        locationName: undefined
      },

      withdrawConditionInit: {
        orderNos: [],
        locationId: undefined,
        locationName: undefined
      },

      reportTypeSel: [],
      reportTypeSelCondition: {
        requestType: 2
      },
      shopSel: [],
      shopSelCondition: {
        simpleName: undefined
      },
      logFormTitle: "",
      logFormVisible: false,
      logCondition: {
        relationNo: undefined
      },
      logtableData: []
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
    this.condition.locationId = this.$route.query.locationId;
    //页面初始化函数
    this.fetchData();
  },
  methods: {
    updateLocation() {
      if (this.condition.searchLocationId == "") {
        this.condition.searchLocationId = undefined;
        this.condition.locationId =this.$route.query.locationId;
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
              res => {
                var resData = res.data;
                this.shopSel = resData.items;
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
    //批量核对
    allwithdraw() {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          if (this.$refs.multipleTable.selection.length > 0) {
            for (
              var i = 0;
              i < this.$refs.multipleTable.selection.length;
              i++
            ) {
              this.withdrawCondition.orderNos.push(
                this.$refs.multipleTable.selection[i].orderNo
              );
            }
            debugger;
            this.withdrawCondition.locationName = this.tableData[0].locationName;
            this.withdrawCondition.locationId = this.condition.locationId;
            var vm = this;
            console.log("formModel: " + JSON.stringify(this.withdrawCondition));

            appSvc
              .rgAccountCheckWithdraw(this.withdrawCondition)
              .then(
                res => {
                  if (res.code == 10000) {
                    this.$message({
                      message: res.message,
                      type: "success"
                    });
                    vm.search();

                    this.withdrawCondition = JSON.parse(
                      JSON.stringify(this.withdrawConditionInit)
                    );
                  } else {
                    this.$message({
                      message: res.message,
                      type: "warning"
                    });
                  }
                },
                error => {
                  console.log(error);
                }
              )
              .catch(() => {});
          } else {
            this.$message({
              message: "暂无可提现的订单!",
              type: "warning"
            });
          }
        })
        .catch(() => {});
    },

    //单个申请提现
    withdraw(row) {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          debugger;
          this.withdrawCondition.orderNos.push(row.orderNo);
          this.withdrawCondition.locationId = this.condition.locationId;
          this.withdrawCondition.locationName = row.locationName;
          var vm = this;
          console.log("formModel: " + JSON.stringify(this.withdrawCondition));

          appSvc
            .rgAccountCheckWithdraw(this.withdrawCondition)
            .then(
              res => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  vm.search();

                  this.withdrawCondition = JSON.parse(
                    JSON.stringify(this.withdrawConditionInit)
                  );
                } else {
                  this.$message({
                    message: res.message,
                    type: "warning"
                  });
                }
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },
    toggleSelection(rows) {
      if (rows) {
        rows.forEach(row => {
          this.$refs.multipleTable.toggleRowSelection(row);
        });
      } else {
        this.$refs.multipleTable.clearSelection();
      }
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
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
      appSvc
        .getBasicInfoList(this.reportTypeSelCondition)
        .then(
          res => {
            this.reportTypeSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
      this.markExceptionFormModel.relationType = "总部";
      this.markExceptionFormModel.relationNo = row.orderNo;
    },
    viewoprlog(row) {
      this.logFormTitle = "【" + row.orderNo + "】的操作日志";
      this.logFormVisible = true;

      this.logCondition.relationNo = row.orderNo;
      appSvc
        .getAccountCheckLogs(this.logCondition)
        .then(
          res => {
            this.logtableData = res.data;
          },
          error => {
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

      appSvc
        .createAccountCheckException(this.markExceptionFormModel)
        .then(
          res => {
            if (res.code == 10000) {
              this.$message({
                message: res.message,
                type: "success"
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
                type: "warning"
              });
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    fetchData() {
      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getRgAccountChecks(this.condition)
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

      appSvc
        .getBasicInfoList(this.shopSelCondition)
        .then(
          res => {
            this.shopSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    sizeChange(val) {
      this.condition.pageIndex = this.currentPage = 1;
      this.condition.pageSize = val;
      this.getPaged();
    },
    pageTurning() {
      this.condition.pageIndex = this.currentPage;
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
        .getRgAccountChecks(this.condition)
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
