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
        <el-form-item prop="status">
          <el-select
            v-model="condition.status"
            placeholder="对账类型"
            clearable
            style="width: 200px"
          >
            <el-option
              v-for="item in status"
              :key="item.id"
              :label="item.status"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="settlementType">
          <el-select
            v-model="condition.settlementType"
            placeholder="结算方式"
            clearable
            style="width: 200px"
          >
            <el-option
              v-for="item in settlementType"
              :key="item.id"
              :label="item.status"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="purchaseNo">
          <el-input
            v-model="condition.purchaseNo"
            style="width: 100%"
            placeholder="查询采购编号"
            clearable
          >
            <i slot="prefix" class="el-input__icon el-icon-search"></i>
          </el-input>
        </el-form-item>
        <el-form-item prop="reconciliationTime">
          <el-tooltip
            class="item"
            effect="dark"
            content="订单安装完成日期"
            placement="bottom"
          >
            <el-date-picker
              v-model="condition.reconciliationTime"
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
          type="primary"
          @click="generageSettlement()"
          :disabled="shopCheckShow"
          icon="el-icon-plus"
          >生成结算单</el-button
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
        <el-table-column prop="id" label="id" v-if="false" min-width="50">
          <!-- <template slot-scope="scope">
            <router-link
              :to="{path:'/orderDetails/:orderNo',query:{orderNo:scope.row.orderNo}}"
            >{{scope.row.orderNo}}</router-link>
          </template>-->
        </el-table-column>
        <el-table-column prop="purchaseNo" label="采购单号" min-width="120">
          <!-- <template slot-scope="scope">
            <router-link
              :to="{path:'/orderDetails/:orderNo',query:{orderNo:scope.row.orderNo}}"
            >{{scope.row.orderNo}}</router-link>
          </template>-->
        </el-table-column>
        <el-table-column
          prop="settlementType"
          label="结算方式"
          align="center"
          min-width="80px"
        ></el-table-column>
        <el-table-column
          prop="settlementStaff"
          label="结算人员"
          align="center"
          min-width="80px"
        ></el-table-column>

        <el-table-column
          prop="purchaseDate"
          label="采购日期"
          width="110px"
          align="center"
        >
          <template slot-scope="scope">{{
            $jscom.toYMDHm(scope.row.purchaseDate)
          }}</template>
        </el-table-column>
        <el-table-column
          prop="productCategory"
          label="产品类目"
          align="center"
          min-width="80px"
        ></el-table-column>
        <el-table-column
          prop="productCode"
          label="产品编码"
          align="center"
          min-width="80px"
        ></el-table-column>
        <el-table-column
          prop="productName"
          label="产品名称"
          align="center"
          min-width="80px"
        ></el-table-column>
        <el-table-column
          prop="oeNumber"
          label="型号规格"
          align="center"
          min-width="80px"
        ></el-table-column>
        <el-table-column
          prop="specification"
          label="原厂编号"
          align="center"
          min-width="80px"
        ></el-table-column>
        <el-table-column
          prop="unit"
          label="单位"
          align="center"
          min-width="80px"
        ></el-table-column>
        <el-table-column
          prop="num"
          label="数量 "
          align="center"
          min-width="80px"
        ></el-table-column>

        <el-table-column
          label="单价"
          prop="purchaseCost"
          align="right"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.purchaseCost)
          }}</template>
        </el-table-column>
        <el-table-column
          label="金额"
          prop="settlementAmount"
          align="right"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.settlementAmount)
          }}</template>
        </el-table-column>
        <el-table-column
          prop="status"
          label="状态 "
          align="center"
          min-width="80px"
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
                :disabled="scope.row.status == '未核对' ? false : true"
                @click="markException(scope.row)"
                size="mini"
                >标记异常</el-button
              >
              <el-button
                type="text"
                size="small"
                :disabled="scope.row.status == '核对异常' ? false : true"
                @click="exceptionHandle(scope.row)"
                >异常处理</el-button
              >
              <!-- <el-button type="primary" size="mini" @click="viewLog(scope.row)"
                >查看日志</el-button
              > -->
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

    <section id="exceptionDialog">
      <rg-dialog
        :title="exceptionFormTitle"
        :visible.sync="exceptionFormVisible"
        :beforeClose="exceptionCancel"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            exceptionCancel();
          },
        }"
        :btnRemove="false"
        :footbar="true"
        width="80%"
        maxWidth="800px"
        minWidth="600px"
        :close-on-click-modal="false"
      >
        <template v-slot:content>
          <el-form
            :model="exceptionFormModel"
            ref="settlementFormModel"

          >
            <el-form-item label="结算金额">
              <el-input
                type="number"
                :rows="5"
                placeholder="请输入金额"
                v-model="exceptionFormModel.amount"
                @input="checkNumber(exceptionFormModel.amount)"
              ></el-input>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button type="primary" @click="exceptionHandleSave()" size="mini"
            >保存</el-button
          >
        </template>
      </rg-dialog>
    </section>
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
        purchaseNo: "",
        status: "",
        settlementType: "",
        reconciliationTime: ["", ""],
        startDate: "",
        endDate: "",
        pageIndex: 1,
        pageSize: 20,
      },
      shopCheckShow: false,
      rgCheckShow: false,
      reportByShow: false,
      reportReasonShow: false,
      tableData: [],
      totalList: 0,
      tableDataLog: [],
      curReconciliationType: "",
      status: [
        {
          id: "未核对",
          status: "未核对",
        },
        {
          id: "已核对",
          status: "已核对",
        },
        {
          id: "核对异常",
          status: "核对异常",
        },
        {
          id: "已结算",
          status: "已结算",
        },
      ],
      settlementType: [
        {
          id: "现金",
          status: "现金",
        },
        {
          id: "月结",
          status: "月结",
        },
      ],
      multipleSelection: [],
      formModelException: {
        remark: "",
      },
      shopExcptionRow: {},
      exceptionFormTitle: "",
      exceptionFormVisible: false,
      exceptionFormModelInit: {
        amount: 0.0,
        id: 0,
      },
      exceptionFormModel: {
        amount: 0.0,
        id: 0,
      },
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

      this.curReconciliationType = data.status;

      if (data.reconciliationTime != null) {
        data.startDate =
          data.reconciliationTime.length > 1 ? data.reconciliationTime[0] : "";
        data.endDate =
          data.reconciliationTime.length > 1 ? data.reconciliationTime[1] : "";
      }

      this.tableLoading = true;
      appSvc
        .getPurchaseAccountList(data)
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
        let purchaseAccounts = [];
        for (var len = 0; len < multiSelectLen; len++) {
          if (multiSelection[len].status != "未核对") {
            this.$messageWarning("请选择状态为 未核对 状态 进行 对账");
            return;
          }
          purchaseAccounts.push({
            id: multiSelection[len].id,
          });
        }
        this.$confirmWarning(msg).then(
          () => {
            this.tableLoading = true;
            appSvc
              .savePurchaseAccountRecord({ purchaseAccounts: purchaseAccounts })
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
    generageSettlement() {
      let data = JSON.parse(JSON.stringify(this.condition));
      if (data.reconciliationTime != null) {
        data.startDate =
          data.reconciliationTime.length > 1 ? data.reconciliationTime[0] : "";
        data.endDate =
          data.reconciliationTime.length > 1 ? data.reconciliationTime[1] : "";
      }
      if (data.startDate == "" && data.endDate == "") {
        this.$messageWarning("请选择生成结算单日期");
        return;
      }
      appSvc
        .savePurchaseSettlementOrder({
          startSettelementDate: data.startDate,
          endSettlementDate: data.endDate,
        })
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
          // this.formVisibleException = false;
        });
      this.search(true);
    },
    markException(se) {
      console.log("se", se);
      if (se.status != "未核对") {
        this.$messageWarning("请选择状态为 未核对 状态 进行 标记异常");
        return;
      }
      this.formVisibleException = true;
      let shopExceptionAccounts = [];

      console.log(this.formModelException.remark);

      this.shopExcptionRow = {
        id: se.id,
        reportReason: this.formModelException.remark,
      };
      console.log(this.shopExcptionRow);
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

      this.tableLoading = true;
      appSvc
        .savePurchaseExceptionAccount({
          id: this.shopExcptionRow.id,
          reportReason: this.shopExcptionRow.reportReason,
        })
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
            vm.btnSaveLoading = false;
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

    exceptionHandle(row) {
      this.exceptionFormTitle = "异常对账处理";
      this.exceptionFormVisible = true;
      this.exceptionFormModel.amount = row.amount;
      this.exceptionFormModel.id = row.id;
    },
    exceptionCancel(formName) {
      this.exceptionFormVisible = false;

      this.exceptionFormModel = JSON.parse(
        JSON.stringify(this.exceptionFormModelInit)
      );
    },
    exceptionHandleSave(formName, type) {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          var vm = this;
          console.log("formModel: " + JSON.stringify(this.exceptionFormModel));
          let purchaseAccounts = [];
          purchaseAccounts.push({
            id: this.exceptionFormModel.id,
          });
          appSvc
            .savePurchaseAccountRecord({
              purchaseAccounts: purchaseAccounts,
              status: "核对异常",
              settlementAmount: this.exceptionFormModel.amount,
            })
            .then(
              (res) => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                  vm.search();
                  this.exceptionFormVisible = false;
                  this.exceptionFormModel = JSON.parse(
                    JSON.stringify(this.exceptionFormModelInit)
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
    checkNumber: function (num) {
      this.exceptionFormModel.amount = num.match(/^\d*(\.?\d{0,2})/g)[0] || null;
    },
  },
};
</script>
