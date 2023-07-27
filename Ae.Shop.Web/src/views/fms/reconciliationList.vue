<template>
  <main class="container">
    <!-- 首页：列表数据 -->
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :headerBtnWidth="260"
      :tableData="tableData"
      :pageChange="pageChange"
      :searching="search"
      :handleSelectionChange="handleSelectionChange"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item prop="reconciliationType">
          <el-select
            v-model="condition.reconciliationType"
            placeholder="对账类型"
            clearable
            style="width: 200px"
          >
            <el-option
              v-for="item in reconciliationType"
              :key="item.id"
              :label="item.status"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="orderNo">
          <el-input
            v-model="condition.orderNo"
            style="width: 100%"
            placeholder="查询订单编号"
            clearable
          >
            <i slot="prefix" class="el-input__icon el-icon-search"></i>
          </el-input>
        </el-form-item>
        <el-form-item prop="installTime">
          <el-tooltip
            class="item"
            effect="dark"
            content="订单安装完成日期"
            placement="bottom"
          >
            <el-date-picker
              v-model="condition.installTime"
              type="daterange"
              range-separator="-"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              :picker-options="$root.$data.timeRgPickerOpt"
              style="width: 220px"
            ></el-date-picker>
          </el-tooltip>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button
          type="primary"
          @click="batchReconciliation()"
          :disabled="shopCheckShow"
          icon="el-icon-plus"
          >批量对账</el-button
        >
        <el-button
          type="danger"
          @click="batchflushBillOrder()"
          :disabled="shopCheckShow"
          icon="el-icon-plus"
          >批量刷新对账单</el-button
        >
      </template>
      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <el-table-column
          type="selection"
          width="55"
          align="center"
        ></el-table-column>
        <el-table-column
          prop="orderNo"
          label="订单编号"
          min-width="120"
          class-name="order"
        >
          <!-- <template slot-scope="scope">
            <router-link
              :to="{path:'/orderDetails/:orderNo',query:{orderNo:scope.row.orderNo}}"
            >{{scope.row.orderNo}}</router-link>
          </template>-->
        </el-table-column>
        <el-table-column
          prop="orderType"
          label="订单类型"
          align="center"
          min-width="80px"
        ></el-table-column>
        <el-table-column
          prop="payMethod"
          label="付款方式"
          align="center"
          min-width="80px"
        ></el-table-column>
        <el-table-column
          prop="moneyArriveStatus"
          label="到账状态"
          align="center"
          min-width="80px"
        ></el-table-column>
        <el-table-column
          prop="installTime"
          label="安装日期"
          width="110px"
          align="center"
        >
          <template slot-scope="scope">{{
            $jscom.toYMDHm(scope.row.installTime)
          }}</template>
        </el-table-column>

        <el-table-column
          label="施工金额"
          prop="saleTotalAmount"
          align="right"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.saleTotalAmount)
          }}</template>
        </el-table-column>
        <el-table-column
          label="实付金额"
          prop="actualAmount"
          align="right"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.actualAmount)
          }}</template>
        </el-table-column>
        <el-table-column
          label="服务安装费"
          prop="shopInstallAmount"
          align="right"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.shopInstallAmount)
          }}</template>
        </el-table-column>
        <el-table-column
          label="铺货成本"
          prop="shopDistributionCost"
          align="right"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.shopDistributionCost)
          }}</template>
        </el-table-column>
        <el-table-column
          label="铺货毛利"
          prop="shopDistributionGrossProfit"
          align="right"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.shopDistributionGrossProfit)
          }}</template>
        </el-table-column>
        <el-table-column
          label="补差价"
          prop="shopDifferencePrice"
          align="right"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.shopDifferencePrice)
          }}</template>
        </el-table-column>
        <el-table-column
          label="门店手续费"
          prop="shopCommissionFee"
          align="right"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.shopCommissionFee)
          }}</template>
        </el-table-column>
        <!-- <el-table-column
          label="门店其他费用"
          prop="shopOtherFee"
          align="right"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.shopOtherFee)
          }}</template>
        </el-table-column> -->
        <el-table-column
          label="门店其他扣款"
          prop="shopOtherFee"
          align="right"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.shopOtherFee)
          }}</template>
        </el-table-column>
        <el-table-column
          label="门店外采金额"
          prop="shopItemFee"
          align="right"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.shopItemFee)
          }}</template>
        </el-table-column>
        <el-table-column label="门店外采成本" prop="shopOutProductCost">
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.shopOutProductCost)
          }}</template>
        </el-table-column>
        <el-table-column
          label="门店结算金额"
          prop="shopSettlementAmount"
          align="right"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.shopSettlementAmount)
          }}</template>
        </el-table-column>
        <el-table-column
          prop="productDetail"
          label="订单产品明细"
          min-width="300px"
        ></el-table-column>
        <el-table-column
          prop="shopCheckResult"
          label="门店核对"
          v-if="rgCheckShow"
          min-width="150px"
        ></el-table-column>
        <el-table-column
          prop="rgCheckResult"
          label="总部核对"
          v-if="rgCheckShow"
          min-width="150px"
        ></el-table-column>
        <el-table-column
          label="上报人"
          v-if="reportByShow"
          prop="reportBy"
          min-width="90px"
        ></el-table-column>
        <el-table-column
          label="上报原因"
          v-if="reportReasonShow"
          prop="reportReason"
          min-width="90px"
        ></el-table-column>
        <el-table-column
          prop="name"
          label="操作"
          align="center"
          fixed="right"
          width="160"
        >
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                type="danger"
                :disabled="shopCheckShow"
                @click="markException(scope.row)"
                size="mini"
                >标记异常</el-button
              >
              <el-button type="primary" size="mini" @click="viewLog(scope.row)"
                >查看日志</el-button
              >
              <el-button
                type="danger"
                size="mini"
                :disabled="shopFlushOrder"
                @click="flushBillOrder(scope.row)"
                >刷新对账单</el-button
              >
            </el-button-group>
          </template>
        </el-table-column>
        <!-- Table数据 -->
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

    <rg-dialog
      :title="formTitle"
      :visible.sync="formVisible"
      :beforeClose="cancel"
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          cancel('formModel');
        },
      }"
      :btnRemove="false"
      :footbar="true"
      width="78%"
      maxWidth="800px"
      minWidth="700px"
    >
      <template v-slot:content>
        <el-table :data="tableDataLog" border style="width: 100%">
          <el-table-column prop="relationNo" label="订单编号"></el-table-column>
          <el-table-column prop="relationType" label="类型"></el-table-column>
          <el-table-column prop="remark" label="内容"></el-table-column>
          <el-table-column prop="createBy" label="操作者"></el-table-column>
          <el-table-column prop="createTime" label="操作时间" align="center">
            <template slot-scope="scope">{{
              $jscom.toYMDHm(scope.row.createTime)
            }}</template>
          </el-table-column>
        </el-table>
      </template>
    </rg-dialog>

    <rg-dialog
      :title="formTitleException"
      :visible.sync="formVisibleException"
      :beforeClose="cancelException"
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          cancelException('formModelException');
        },
      }"
      :btnRemove="false"
      :footbar="true"
      width="78%"
      maxWidth="800px"
      minWidth="700px"
    >
      <template v-slot:content>
        <el-form
          :model="formModelException"
          ref="formModelException"
          label-width="120px"
          size="mini"
        >
          <el-form-item label="异常描述：" prop="remark">
            <el-input
              v-model="formModelException.remark"
              type="textarea"
              style="width: 420px"
              autocomplete="off"
              clearable
              placeholder="请填写取消描述"
            />
          </el-form-item>
        </el-form>
      </template>
      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          size="mini"
          type="primary"
          :disabled="formCheckException"
          :loading="btnSaveLoading"
          @click="save('formModelException')"
          >保存</el-button
        >
      </template>
    </rg-dialog>
  </main>
</template>
<style>
</style>
<script>
import { appSvc } from "@/api/fms/accountcheck";
export default {
  name: "demopage",
  data() {
    return {
      formTitle: "查看日志",
      formTitleException: "标记异常",
      formVisible: false,
      formVisibleException: false,
      formCheckException: false,
      tableLoading: false,
      btnSaveLoading: false,
      tbHandle: undefined,
      condition: {
        orderNo: "",
        reconciliationType: "",
        installTime: ["", ""],
        startTime: "",
        endTime: "",
        pageIndex: 1,
        pageSize: 20,
      },
      shopCheckShow: false,
      shopFlushOrder: false,
      rgCheckShow: false,
      reportByShow: false,
      reportReasonShow: false,
      tableData: [],
      totalList: 0,
      tableDataLog: [],
      curReconciliationType: "",
      reconciliationType: [
        {
          id: "ShopNoReconciliation",
          status: "门店未对账",
        },
        {
          id: "ShopHavedReconciliation",
          status: "门店已对账",
        },
        {
          id: "RgHavedReconciliation",
          status: "总部已对账",
        },
        {
          id: "ExceptionReconciliation",
          status: "异常对账",
        },
      ],
      multipleSelection: [],
      formModelException: {
        remark: "",
      },
      shopExcptionRow: [],
    };
  },
  created() {
    this.fetchData();
  },
  watch: {
    //当列数发生改变时，重新刷新表格布局
    curReconciliationType(newVal, oldVal) {
      this.$nextTick(() => {
        this.tbHandle.doLayout();
      });
    },
  },
  methods: {
    flushBillOrder(se) {
      this.tableLoading = true;
      let obj = { orderNo: se.orderNo, createBy: "" };
      appSvc
        .CalculationReconciliationFee(obj)
        .then(
          (res) => {
            this.$messageSuccess(res.message || "操作成功");
            this.getPaged();
          },
          (err) => {
            console.error(err);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });
    },
    batchflushBillOrder() {
      let multiSelectLen = this.multipleSelection.length;

      if (multiSelectLen > 0) {
        let vm = this;
        let msg = `是否要进行批量刷新对账`;
        let multiSelection = this.multipleSelection;
        let shopAccounts = [];
        for (var len = 0; len < multiSelectLen; len++) {
          shopAccounts.push({
            orderNo: multiSelection[len].orderNo,
            createBy: "",
          });
        }
        this.$confirmWarning(msg).then(
          () => {
            this.tableLoading = true;
            appSvc
              .CalculationReconciliationFeeBatch({ accounts: shopAccounts })
              .then(
                (res) => {
                  if (res.code == 10000) {
                    this.$messageSuccess(res.message);
                    vm.search();
                  } else {
                    this.$messageWarning(res.message);
                  }
                },
                (err) => {}
              )
              .catch(() => {})
              .finally(() => {
                this.tableLoading = false;
              });
          },
          (cancel) => {
            console.warn(cancel);
          }
        );
      } else {
        this.$messageWarning("请先选择需要刷新对账的订单！");
      }
    },
    synicVar(p) {
      this.tbHandle = p.tbHandle;
    },
    tableRowClassName(row, rowIndex) {
      if (row.isExceptionOrder) {
        return "warning-row";
      }
      return "";
    },
    fetchData() {
      this.getConditionData();
      this.getPaged();
    },
    getConditionData() {
      // this.getAllOrganizationExceptShopSelectListAsync();
    },
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    getPaged(flag) {
      let data = JSON.parse(JSON.stringify(this.condition));

      this.curReconciliationType = data.reconciliationType;

      if (data.reconciliationType === "ShopNoReconciliation") {
        this.shopCheckShow = false;
        this.rgCheckShow = false;
        this.reportByShow = false;
        this.reportReasonShow = false;
        this.shopFlushOrder = false;
      }
      if (data.reconciliationType === "ShopHavedReconciliation") {
        this.shopCheckShow = true;
        this.rgCheckShow = true;
        this.reportByShow = false;
        this.reportReasonShow = false;
        this.shopFlushOrder = true;
      }
       if (data.reconciliationType === "RgHavedReconciliation") {
        this.shopCheckShow = true;
        this.rgCheckShow = true;
        this.reportByShow = false;
        this.reportReasonShow = false;
        this.shopFlushOrder = true;
      }
      if (data.reconciliationType === "ExceptionReconciliation") {
        this.shopCheckShow = true;
        this.rgCheckShow = false;
        this.reportByShow = true;
        this.reportReasonShow = true;
        this.shopFlushOrder = true;
      }
      if (data.installTime != null) {
        data.startTime = data.installTime.length > 1 ? data.installTime[0] : "";
        data.endTime = data.installTime.length > 1 ? data.installTime[1] : "";
      }

      this.tableLoading = true;
      console.log("rec", data.reconciliationType);
      if (
        data.reconciliationType === "ShopNoReconciliation" ||
        data.reconciliationType === ""
      ) {
        appSvc
          .getShopAccountUnChecks(data)
          .then(
            (res) => {
              let data = res.data;
              this.tableData = data.items;
              this.totalList = data.totalItems;
              console.log(data);
              if (flag) {
                this.$messageSuccess(res.message || "查询成功");
              }
            },
            (err) => {
              console.error(err);
            }
          )
          .catch(() => {})
          .finally(() => {
            this.tableLoading = false;
          });
      }
      if (data.reconciliationType === "ShopHavedReconciliation") {
        appSvc
          .GetShopAccountChecks(data)
          .then(
            (res) => {
              let data = res.data;
              this.tableData = data.items;
              this.totalList = data.totalItems;
              if (flag) {
                this.$messageSuccess(res.message || "查询成功");
              }
            },
            (err) => {
              console.error(err);
            }
          )
          .catch(() => {})
          .finally(() => {
            this.tableLoading = false;
          });
      }
      if (data.reconciliationType === "RgHavedReconciliation") {
        appSvc
          .GetRgAccountChecks(data)
          .then(
            (res) => {
              let data = res.data;
              this.tableData = data.items;
              this.totalList = data.totalItems;
              if (flag) {
                this.$messageSuccess(res.message || "查询成功");
              }
            },
            (err) => {
              console.error(err);
            }
          )
          .catch(() => {})
          .finally(() => {
            this.tableLoading = false;
          });
      }
      if (data.reconciliationType === "ExceptionReconciliation") {
        appSvc
          .GetAccountCheckExceptionCollectList(data)
          .then(
            (res) => {
              let data = res.data;
              this.tableData = data.items;
              this.totalList = data.totalItems;
              if (flag) {
                this.$messageSuccess(res.message || "查询成功");
              }
            },
            (err) => {
              console.error(err);
            }
          )
          .catch(() => {})
          .finally(() => {
            this.tableLoading = false;
          });
      }
    },
    search(flag) {
      this.condition.pageIndex = 1;
      this.getPaged(flag);
    },
    batchReconciliation() {
      let multiSelectLen = this.multipleSelection.length;

      if (multiSelectLen > 0) {
        let vm = this;
        let msg = `是否要进行批量对账`;
        let multiSelection = this.multipleSelection;
        let shopAccounts = [];
        for (var len = 0; len < multiSelectLen; len++) {
          shopAccounts.push({
            orderNo: multiSelection[len].orderNo,
            shopCheckResult: "已核对",
            remark: "",
          });
        }
        this.$confirmWarning(msg).then(
          () => {
            this.tableLoading = true;
            appSvc
              .CreateAccountCheck({ accounts: shopAccounts })
              .then(
                (res) => {
                  if (res.code == 10000) {
                    this.$messageSuccess(res.message);
                    vm.search();
                  } else {
                    this.$messageWarning(res.message);
                  }
                },
                (err) => {}
              )
              .catch(() => {})
              .finally(() => {
                this.tableLoading = false;
              });
          },
          (cancel) => {
            console.warn(cancel);
          }
        );
      } else {
        this.$messageWarning("请先选择需要对账的订单！");
      }
    },
    markException(se) {
      console.log("se", se);

      this.formVisibleException = true;
      let shopExceptionAccounts = [];

      console.log(this.formModelException.remark);
      shopExceptionAccounts.push({
        orderNo: se.orderNo,
        shopCheckResult: "核对异常",
        remark: "",
      });
      this.shopExcptionRow = shopExceptionAccounts;

      console.log("shopExcptionRow", this.shopExcptionRow);
    },
    cancel(formName) {
      this.formVisible = false;
    },
    cancelException(formName) {
      this.formVisibleException = false;
      this.formCheckException = false;
      this.$refs["formModelException"].resetFields();
    },
    save(formName) {
      var vm = this;

      vm.btnSaveLoading = true;

      let shopAccounts = [];

      console.log(this.shopExcptionRow.length);
      for (var i = 0; i < this.shopExcptionRow.length; i++) {
        this.shopExcptionRow[i].remark = this.formModelException.remark;

        this.shopExcptionRow[i].orderNo = this.shopExcptionRow[i].orderNo;
        this.shopExcptionRow[i].shopCheckResult =
          this.shopExcptionRow[i].shopCheckResult;
      }

      this.tableLoading = true;
      appSvc
        .CreateAccountCheck({ accounts: this.shopExcptionRow })
        .then(
          (res) => {
            if (res.code == 10000) {
              this.$messageSuccess(res.message);
              vm.search();
            } else {
              this.$messageWarning(res.message);
            }
            this.tableLoading = false;
          },
          (err) => {}
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
          this.formVisibleException = false;
        });
    },
    // 取消订单
    viewLog(se) {
      this.formVisible = true;
      appSvc
        .GetAccountCheckLogs({ relationNo: se.orderNo, relationType: "门店" })
        .then(
          (res) => {
            let data = res.data;
            this.tableDataLog = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    handleSelectionChange(val) {
      console.log(val);
      this.multipleSelection = val;
    },
  },
};
</script>
