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
      :tableRowClassName="tableRowClassName"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item prop="status">
          <el-select v-model="condition.status" placeholder="付款状态" clearable style="width:200px">
            <el-option v-for="item in status" :key="item.id" :label="item.value" :value="item.id"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="settlementBatchNo">
          <el-input
            v-model="condition.settlementBatchNo"
            style="width:100%"
            placeholder="查询结算批次"
            clearable
          >
            <i slot="prefix" class="el-input__icon el-icon-search"></i>
          </el-input>
        </el-form-item>
        <el-form-item prop="applyTime">
          <el-tooltip class="item" effect="dark" content="提现申请日期" placement="bottom">
            <el-date-picker
              v-model="condition.applyTime"
              type="daterange"
              range-separator="-"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              :picker-options="$root.$data.timeRgPickerOpt"
              style="width:220px;"
            ></el-date-picker>
          </el-tooltip>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="withdrawal();">申请提现</el-button>
      </template>

      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <el-table-column prop="settlementBatchNo" label="结算批次" min-width="60" class-name="order"></el-table-column>
        <el-table-column prop="billAmount" label="提现金额" align="right" min-width="60px">
          <template slot-scope="scope">{{ $jscom.toMoney(scope.row.billAmount) }}</template>
        </el-table-column>
        <el-table-column prop="status" label="付款状态" width="80px" align="center">
          <template slot-scope="scope">
            {{ scope.row.status === 0 ? '未付款': '' }}
            {{ scope.row.status === 1? '付款失败': '' }}
            {{ scope.row.status === 2? '已付款': '' }}
          </template>
        </el-table-column>
        
        <el-table-column prop="bankName" label="银行名称" align="center" min-width="60px"></el-table-column>
        <el-table-column prop="bankBranch" label="银行支行" align="center" min-width="80px"></el-table-column>
        <el-table-column prop="bankUser" label="银行账户名" align="center" min-width="80px"></el-table-column>
        <el-table-column prop="bankNo" label="银行卡号" align="center" min-width="80px"></el-table-column>
        <el-table-column prop="applyUser" label="申请人" align="center" min-width="100px"></el-table-column>
        <el-table-column prop="applyTime" label="申请日期" align="center" min-width="60px">
          <template slot-scope="scope">{{$jscom.toYMDHm(scope.row.applyTime) }}</template>
        </el-table-column>
        <el-table-column prop="name" label="操作" align="center" width="100" fixed="right">
          <template slot-scope="scope">
            <el-button-group>
              <el-button type="primary" size="mini" @click="viewLog(scope.row)">查看详情</el-button>
              <!-- <el-button
                type="warning"
                size="mini"
                @click="maintain(scope.row)"
                :disabled="scope.row.useStatus==2"
              >维修</el-button>
              <el-button
                type="danger"
                size="mini"
                @click="discard(scope.row)"
                :disabled="scope.row.useStatus==2"
              >作废</el-button>-->
            </el-button-group>
          </template>
        </el-table-column>
        <!-- Table数据 -->

        <!-- Table数据 -->
      </template>

      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->
    <rg-dialog
      :title="formTitle"
      :visible.sync="formVisible"
      :beforeClose="cancel"
      :btnCancel="{label:'关闭',click:(done)=>{cancel('formModel')}}"
      :btnRemove="false"
      :footbar="true"
      width="90%"
      top="5%"
      height="90%"
    >
      <template v-slot:content>
        <el-table ref="diag_table" id="diag_table" 
        :data="tableDataLog" border style="width: 100%" 
        height="calc(100% - 110px)" 
        show-summary 
        :span-method="arraySpanMethod"
        :summary-method="getSummaries">
           <el-table-column prop="installTime" label="安装日期" width="150px" align="center">
            <template slot-scope="scope">{{$jscom.toYMDHm(scope.row.installTime) }}</template>
          </el-table-column>
          <el-table-column prop="orderNo" label="订单编号"></el-table-column>
          <!-- <el-table-column prop="payMethod" label="付款方式"></el-table-column> -->
          <el-table-column prop="orderType" label="订单类型"></el-table-column>
          <el-table-column label="施工金额" prop="saleTotalAmount" align="right" min-width="80px">
            <template slot-scope="scope">{{ $jscom.toMoney(scope.row.saleTotalAmount) }}</template>
          </el-table-column>
          <el-table-column label="实付金额" prop="actualAmount" align="right" min-width="80px">
            <template slot-scope="scope">{{ $jscom.toMoney(scope.row.actualAmount) }}</template>
          </el-table-column>
          <el-table-column label="服务安装费" prop="shopInstallAmount" align="right" min-width="80px">
            <template slot-scope="scope">{{ $jscom.toMoney(scope.row.shopInstallAmount) }}</template>
          </el-table-column>
          <el-table-column label="铺货成本" prop="shopDistributionCost" align="right" min-width="80px">
            <template slot-scope="scope">{{ $jscom.toMoney(scope.row.shopDistributionCost) }}</template>
          </el-table-column>
          <el-table-column
            label="铺货毛利"
            prop="shopDistributionGrossProfit"
            align="right"
            min-width="80px"
          >
            <template slot-scope="scope">{{ $jscom.toMoney(scope.row.shopDistributionGrossProfit) }}</template>
          </el-table-column>
          <el-table-column label="补差价" prop="shopDifferencePrice" align="right" min-width="80px">
            <template slot-scope="scope">{{ $jscom.toMoney(scope.row.shopDifferencePrice) }}</template>
          </el-table-column>
          <el-table-column label="门店手续费" prop="shopCommissionFee" align="right" min-width="80px">
            <template slot-scope="scope">{{ $jscom.toMoney(scope.row.shopCommissionFee) }}</template>
          </el-table-column>
          <el-table-column label="门店其他扣款" prop="shopOtherFee" align="right" min-width="80px">
            <template slot-scope="scope">{{ $jscom.toMoney(scope.row.shopOtherFee) }}</template>
          </el-table-column>
          <el-table-column label="门店外采金额" prop="shopItemFee" align="right" min-width="80px">
            <template slot-scope="scope">{{ $jscom.toMoney(scope.row.shopItemFee) }}</template>
          </el-table-column>
          <el-table-column label="门店外采成本" prop="shopOutProductCost" align="right" min-width="80px">
            <template slot-scope="scope">{{ $jscom.toMoney(scope.row.shopOutProductCost) }}</template>
          </el-table-column>
          <el-table-column
            label="门店结算金额"
            prop="shopSettlementAmount"
            align="right"
            min-width="80px"
          >
            <template slot-scope="scope">{{ $jscom.toMoney(scope.row.shopSettlementAmount) }}</template>
          </el-table-column>
         
        </el-table>
        <el-divider content-position="left" v-if="logs.length > 0">处理记录</el-divider>
        <el-timeline v-if="logs.length > 0">
          <el-timeline-item
            v-for="(log, index) in logs"
            :key="index"
            :timestamp="log.createTime"
          >{{log.createBy}}-{{log.remark}}</el-timeline-item>
        </el-timeline>
      </template>
    </rg-dialog>

    <rg-dialog
      :title="formTitle2"
      :visible.sync="formVisible2"
      :beforeClose="cancelWithdrawal"
      :btnCancel="{label:'关闭',click:(done)=>{cancelWithdrawal('formModel')}}"
      :btnRemove="false"
      :footbar="true"
      width="78%"
      maxWidth="800px"
      minWidth="700px"
    >
      <template v-slot:content>
        <el-form :model="formModel2" ref="formModel2">
          <el-row>
            <el-col :span="8">
              <el-form-item :label-width="formLabelWidth" label="开户行名称" prop="bankName">
                <el-input
                  v-model="formModel2.bankName"
                  style="width:420px"
                  autocomplete="off"
                  readonly
                />
              </el-form-item> 
             
              <el-form-item :label-width="formLabelWidth" label="开户支行" prop="bankBranch">
                <el-input
                  v-model="formModel2.bankBranch"
                  style="width:420px"
                  autocomplete="off"
                  readonly
                />
              </el-form-item>
              <el-form-item :label-width="formLabelWidth" label="开户人姓名" prop="bankUser">
                <el-input
                  v-model="formModel2.bankUser"
                  style="width:420px"
                  autocomplete="off"
                  readonly
                />
              </el-form-item>
              <el-form-item :label-width="formLabelWidth" label="银行卡号" prop="bankNo">
                <el-input
                  v-model="formModel2.bankNo"
                  style="width:420px"
                  autocomplete="off"
                  readonly
                />
              </el-form-item>
              <el-form-item :label-width="formLabelWidth" label="可提现金额" prop="applyAmount">
                <el-input
                  v-model="formModel2.applyAmount"
                  style="width:420px"
                  autocomplete="off"
                  readonly
                >
                  <template slot="prepend">￥</template>
                </el-input>
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>
      </template>
      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          size="mini"
          type="primary"
          :disabled="formCheck2"
          :loading="btnSaveLoading2"
          @click="withdrawalSave('formModel2')"
        >保存</el-button>
      </template> -->
    </rg-dialog>
  </main>
</template>
<style>
.el-table .warning-row,
.el-table .warning-row td {
  background: rgb(253, 230, 230) !important;
}
a {
  color: rgb(20, 22, 155);
}
</style>
<script>
import { appSvc } from "@/api/fms/settlement";
export default {
  name: "demopage",
  data() {
    return {
      formTitle: "查看日志",
      formLabelWidth: "100px",
      formVisible: false,
      tableLoading: false,
      btnSaveLoading: false,
      formModel: {
        reasons: [],
        instruction: "",
      },
      reasons: [
        {
          id: "1",
          reason: "7天无理由退货",
        },
        {
          id: "2",
          reason: "不喜欢/不想要",
        },
        {
          id: "3",
          reason: "待审核时间长",
        },
        {
          id: "4",
          reason: "其他",
        },
      ],
      formTitle2: "申请提现",
      formVisible2: false,
      formCheck2: false,
      btnSaveLoading2: false,
      formModel2: {
        bankName: "",
        bankNo: "",

        bankBranch:"",
        bankUser:"",
        applyAmount: 0,
      },
      condition: {
        settlementBatchNo: "",
        status: "",
        applyTime: ["", ""],
        applyStartTime: "",
        applyEndTime: "",
        pageIndex: 1,
        pageSize: 20,
      },
      tableData: [],
      totalList: 0,
      tableDataLog: [],
      logs: [],
      status: [
        {
          id: "Apply",
          value: "申请中",
        },
        {
          id: "PayFailure",
          value: "打款失败",
        },
        {
          id: "HavePay",
          value: "已打款",
        },
      ],
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    getSummaries(param) {
        const { columns, data } = param;
        const sums = [];
        columns.forEach((column, index) => {
          if (index === 0) {
            sums[index] = '合计';
            return;
          }
          if(index>3){
            const values = data.map(item => Number(item[column.property]));
            if (!values.every(value => isNaN(value))) {
              sums[index] = values.reduce((prev, curr) => {
                const value = Number(curr);
                if (!isNaN(value)) {
                  return prev + curr;
                } else {
                  return prev;
                }
              }, 0);
              sums[index] = this.$jscom.toMoney(sums[index]||0);
            } else {
              sums[index] = this.$jscom.toMoney(0);
            }
          }
        });
        return sums;
    },
    arraySpanMethod({ row, column, rowIndex, columnIndex }) {
        // if (this.tableDataLog && rowIndex === this.tableDataLog.length) {
        //   if (columnIndex === 0) {
        //     return [1, 4];
        //   } else if (columnIndex >= 1 && columnIndex < 4) {
        //     return [0, 0];
        //   }
        // }
      if (this.tableDataLog && rowIndex === this.tableDataLog.length - 1) {//计算合计行的合并
        let that = this;
        setTimeout(function () {
          if (that.$el.querySelector("#diag_table")) {
            var current = that.$el
              .querySelector("#diag_table")
              .querySelector(".el-table__footer-wrapper")
              .querySelector(".el-table__footer");
            var cell = current.rows[0].cells;
            cell[0].colSpan = "4";
            cell[1].style.display = "none";
            cell[2].style.display = "none";
            cell[3].style.display = "none";
          }
        }, 50);
      }
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
      if (data.applyTime != null) {
        data.applyStartTime =
          data.applyTime.length > 1 ? data.applyTime[0] : "";
        data.applyEndTime =
          data.applyEndTime.length > 1 ? data.applyTime[1] : "";
      }

      console.log(data);
      if (data.status === "") {
        data.status = "None";
      }
      this.tableLoading = true;
      appSvc
        .GetWithdrawalRecordList(data)
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
    cancel(formName) {
      this.formVisible = false;
    },
    // 取消订单
    viewLog(se) {
      this.formVisible = true;
      appSvc
        .GetWithdrawalOrderRecordList({
          batchNo: se.settlementBatchNo,
          pageIndex: 1,
          pageSize: 10000,
        })
        .then(
          (res) => {
            let data = res.data;
            this.tableDataLog = data.items;
            this.$nextTick(() => {//刷新table渲染 以正常展示合并行
              this.$refs['diag_table'].doLayout();
            });
            console.log(data);
          },
          (error) => {}
        )
        .catch(() => {});
      appSvc
        .GetAccountCheckLogs({
          relationNo: se.settlementBatchNo,
          relationType: "结算单",
        })
        .then(
          (res) => {
            let data = res.data;
            this.logs = data;
            console.log("结算单日志", data);
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    cancelWithdrawal(formName) {
      this.formVisible2 = false;
      this.formCheck2 = false;

      // this.resetForm();

      // var frmName =
      //   typeof formName === "string" && formName ? formName : "formModel2";
      // this.$refs[frmName].resetFields();
    },
    withdrawalSave(formName) {
      var vm = this;

      this.formVisible2 = false;

      vm.btnSaveLoading2 = true;

      this.tableLoading = true;
      let data = JSON.parse(JSON.stringify(this.formModel2));
      appSvc
        .SubmitWithdrawalApply(data)
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
          this.formVisible2 = false;
          this.btnSaveLoading2 = false;
        });
    },
    // 提现列表
    withdrawal() {
      this.formVisible2 = true;
      this.formCheck2 = false;
      appSvc
        .GetWithdrawalApply(this.formVisible2)
        .then(
          (res) => {
            var data = res.data;
            this.formModel2.bankName = data.bankName;
            this.formModel2.bankNo = data.bankNo;
            this.formModel2.bankBranch = data.bankBranch;
            this.formModel2.bankUser = data.bankUser;
            this.formModel2.applyAmount = data.withdrawalAmount;
            if (data.withdrawalAmount > 0) {
              this.formCheck2 = false;
            } else {
              this.formCheck2 = true;
            }
          },
          (error) => {}
        )
        .catch(() => {});
    },
  },
};
</script>
