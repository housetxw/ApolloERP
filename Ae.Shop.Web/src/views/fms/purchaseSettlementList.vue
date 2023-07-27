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
        <el-form-item prop="payStatus">
          <el-select
            v-model="condition.payStatus"
            placeholder="付款状态"
            clearable
            style="width: 200px"
          >
            <el-option
              v-for="item in payStatus"
              :key="item.id"
              :label="item.value"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="settlementBatchNo">
          <el-input
            v-model="condition.settlementBatchNo"
            style="width: 100%"
            placeholder="查询结算批次"
            clearable
          >
            <i slot="prefix" class="el-input__icon el-icon-search"></i>
          </el-input>
        </el-form-item>
        <el-form-item prop="settlementTime">
          <el-tooltip
            class="item"
            effect="dark"
            content="提现申请日期"
            placement="bottom"
          >
            <el-date-picker
              v-model="condition.settlementTime"
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
        <!-- <el-button
          type="primary"
          size="mini"
          icon="el-icon-plus"
          @click="withdrawal()"
          >申请提现</el-button
        > -->
      </template>

      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <el-table-column
          prop="settlementBatchNo"
          label="结算批次"
          min-width="120"
        ></el-table-column>
        <el-table-column
          prop="settlementType"
          label="结算方式"
          min-width="80"
        ></el-table-column>
        <el-table-column
          prop="settlementYear"
          label="结算年份"
          min-width="80"
        ></el-table-column>
        <el-table-column
          prop="settlementMonth"
          label="结算月份"
          min-width="80"
        ></el-table-column>
        <el-table-column
          prop="settlementStaff"
          label="结算人员"
          min-width="80"
        ></el-table-column>

        <el-table-column
          prop="billAmount"
          label="结算单金额"
          align="right"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toMoney(scope.row.billAmount)
          }}</template>
        </el-table-column>
        <el-table-column
          prop="applyUser"
          label="申请人"
          align="center"
          min-width="80px"
        ></el-table-column>
        <el-table-column
          prop="applyTime"
          label="申请日期"
          align="center"
          min-width="80px"
        >
          <template slot-scope="scope">{{
            $jscom.toYMDHm(scope.row.applyTime)
          }}</template>
        </el-table-column>
        <el-table-column
          prop="status"
          label="付款状态"
          width="110px"
          align="center"
        >
          <template slot-scope="scope">
            {{ scope.row.status === 0 ? "未付款" : "" }}
            {{ scope.row.status === 1 ? "付款失败" : "" }}
            {{ scope.row.status === 2 ? "已付款" : "" }}
          </template>
        </el-table-column>
        <el-table-column
          prop="name"
          label="操作"
          align="center"
          width="180"
          fixed="right"
        >
          <template slot-scope="scope">
            <el-button-group>
              <el-button type="primary" size="mini" @click="viewLog(scope.row)"
                >查看详情</el-button
              >

              <el-button
                type="text"
                size="small"
                :disabled="scope.row.status == 2 ? true : false"
                @click="settlementHandle(scope.row)"
                >付款处理</el-button
              >

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
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          cancel('formModel');
        },
      }"
      :btnRemove="false"
      :footbar="true"
      width="78%"
      maxWidth="900px"
      minWidth="700px"
    >
      <template v-slot:content>
        <el-table :data="tableDataLog" border style="width: 100%">
          <el-table-column prop="purchaseNo" label="采购单号"></el-table-column>
          <el-table-column
            prop="settlementStaff"
            label="结算人员"
          ></el-table-column>
          <el-table-column
            prop="settlementType"
            label="结算方式"
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
          ></el-table-column>
          <el-table-column
            prop="productCode"
            label="产品编码"
          ></el-table-column>
          <el-table-column
            prop="productName"
            label="产品名称"
          ></el-table-column>
          <el-table-column
            prop="specification"
            label="型号规格"
          ></el-table-column>
          <el-table-column prop="oeNumber" label="原厂编号"></el-table-column>
          <el-table-column prop="unit" label="单位"></el-table-column>
          <el-table-column prop="num" label="数量"></el-table-column>
          <el-table-column prop="num" label="数量"></el-table-column>
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
        </el-table>
        <!-- <el-divider content-position="left">处理记录</el-divider>
        <el-timeline>
          <el-timeline-item
            v-for="(log, index) in logs"
            :key="index"
            :timestamp="log.createTime"
            >{{ log.createBy }}-{{ log.remark }}</el-timeline-item
          >
        </el-timeline> -->
      </template>
    </rg-dialog>

    <section id="settlementhandleDialog">
      <rg-dialog
        :title="settlementFormTitle"
        :visible.sync="settlementFormVisible"
        :beforeClose="settlementCancel"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            settlementCancel();
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
            :model="settlementFormModel"
            ref="settlementFormModel"
            :label-width="formLabelWidth"
          >
            <el-form-item label="处理意见">
              <el-input
                type="textarea"
                :rows="5"
                placeholder="请输入处理意见"
                v-model="settlementFormModel.remark"
              ></el-input>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            type="primary"
            @click="settlementSave('settlementFormModel', 'pass')"
            size="mini"
            >已付款</el-button
          >
          <el-button
            type="warning"
            @click="settlementSave('settlementFormModel', 'reject')"
            size="mini"
            >付款失败</el-button
          >
        </template>
      </rg-dialog>
    </section>
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
      formTitle: "查看详情",
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
        applyAmount: 0,
      },
      condition: {
        settlementBatchNo: "",
        payStatus: "",
        settlementTime: ["", ""],
        startSettlementTime: "",
        endSettlementTime: "",
        pageIndex: 1,
        pageSize: 20,
      },
      tableData: [],
      totalList: 0,
      tableDataLog: [],
      logs: [],
      payStatus: [
        {
          id: 0,
          value: "申请中",
        },
        {
          id: 1,
          value: "打款失败",
        },
        {
          id: 2,
          value: "已打款",
        },
      ],
      settlementFormTitle: "",
      settlementFormVisible: false,
      settlementFormModelInit: {
        settlementNo: undefined,
        remark: undefined,
        status: undefined,
      },
      settlementFormModel: {
        settlementNo: undefined,
        remark: undefined,
        status: undefined,
      },
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
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
      if (data.settlementTime != null) {
        data.startSettlementTime =
          data.settlementTime.length > 1 ? data.settlementTime[0] : "";
        data.endSettlementTime =
          data.settlementTime.length > 1 ? data.settlementTime[1] : "";
      }

      console.log(data);

      this.tableLoading = true;
      appSvc
        .getPurchaseSettlementList(data)
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
      this.formTitle = "查看详情( " + se.settlementBatchNo + " )";
      appSvc
        .getPurchaseSettlementDetail({
          settlementBatchNo: se.settlementBatchNo,
        })
        .then(
          (res) => {
            let data = res.data;
            this.tableDataLog = res.data;
            console.log(data);
          },
          (error) => {}
        )
        .catch(() => {});
    },

    settlementHandle(row) {
      this.settlementFormTitle = "付款处理";
      this.settlementFormVisible = true;
      this.settlementFormModel.settlementNo = row.settlementBatchNo;
    },
    settlementCancel(formName) {
      this.settlementFormVisible = false;

      this.settlementFormModel = JSON.parse(
        JSON.stringify(this.settlementFormModelInit)
      );
    },
    settlementSave(formName, type) {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          this.settlementFormModel.status = 2;
          if (type == "reject") {
            if (
              this.settlementFormModel.remark == "" ||
              this.settlementFormModel.remark == undefined
            ) {
              this.$message({
                message: "请填写付款失败原因!",
                type: "warning",
              });
              return;
            } else {
              this.settlementFormModel.status = 1;
            }
          }

          debugger;

          var vm = this;
          console.log("formModel: " + JSON.stringify(this.settlementFormModel));

          appSvc
            .savePurchaseSettlementReview(this.settlementFormModel)
            .then(
              (res) => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                  vm.search();
                  this.settlementFormVisible = false;
                  this.settlementFormModel = JSON.parse(
                    JSON.stringify(this.settlementFormModelInit)
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
  },
};
</script>
