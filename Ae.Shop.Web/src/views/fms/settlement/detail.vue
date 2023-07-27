<template>
  <el-row>
    <el-col :span="6">
      <div class="grid-content bg-purple">
        <span style="color:transparent;">d</span>
      </div>
    </el-col>
    <el-col :span="12">
      <main class="container">
        <!-- 首页 -->
        <section id="indexPage">
          <header class="bodyTop">
            <h3>
              门店结算单-{{ returnModel.shopName }}&nbsp;&nbsp;&nbsp;{{
                returnModel.createTimeStr
              }}
            </h3>
          </header>
          <main style="width:811px;">
            <el-table
              border
              :data="tableData"
           
              show-summary
              :cell-style="cellStyle"
            >
              <el-table-column
                label="订单号"
                width="130px"
                prop="settlementBatchNo"
              ></el-table-column>
              <el-table-column
                label="安装时间"
                width="120px"
                prop="installTimeStr"
              ></el-table-column>
              <el-table-column
                label="付款方式"
                width="110px"
                prop="payMethod"
              ></el-table-column>
              <el-table-column
                label="销售价"
                width="90px"
                prop="saleTotalAmount"
              ></el-table-column>

              <el-table-column
                label="服务费"
                width="90px"
                prop="serviceFee"
              ></el-table-column>
              <el-table-column
                label="扣手续费"
                width="90px"
                prop="commissionFee"
              ></el-table-column>

              <el-table-column
                label="扣其他"
                width="90px"
                prop="otherFee"
              ></el-table-column>
              <el-table-column
                label="应结算"
                width="90px"
                prop="settlementFee"
              ></el-table-column>
            </el-table>
          </main>
        </section>
        <!-- 首页 -->
        <section  style="width:811px;margin-top:80px;">
          <el-row>
            <el-col :span="24"
              ><h3>结算人:{{ returnModel.createBy }}</h3></el-col
            >
            <el-col :span="24"
              ><h3>结算备注:{{ returnModel.remark }}</h3></el-col
            >
            <el-col :span="24">
              <el-col :span="12"><h3>总部人员签名:</h3></el-col>
              <el-col :span="12"><h3>合作门店人员签名:</h3></el-col>
            </el-col>

            <el-col :span="24" style="margin-top:30px;">
              <el-col :span="12"
                ><span>________年_______月______日</span></el-col
              >
              <el-col :span="12"
                ><span>________年_______月______日</span></el-col
              >
            </el-col>
          </el-row>
          <el-row :span="24" style="margin-top:50px;">
            <div>
              <p>签名及确认：</p>
              <p>1：知悉上述款项详情</p>
              <p>2：确认上述款项正确无误</p>
              <p>3：对结算款项金额无异议</p>
            </div>
          </el-row>
        </section>
      </main>
    </el-col>
    <el-col :span="4">
      <div class="grid-content bg-purple">
          <span style="color:transparent;">d</span>
      </div>
    </el-col>
  </el-row>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appSvc } from "@/api/fms/settlement";

import { isNumber } from "util";
const statusOptions = ["未付款", "已付款", "付款失败"];

export default {
  name: "child4",
  mounted() {
    console.log("tab1组件");
  },
  data() {
    return {
      input: undefined,

      tableLoading: false,
      currentPage: 1,
      loading: false,
      //table页面的条件
      condition: {
        settlementBatchNo: undefined
      },
      tableData: [],
      returnModel: {
        shopName: undefined,
        createBy: undefined,
        createTimeStr: undefined,
        remark: undefined
      }
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
    debugger;
    //页面初始化函数
    console.log("param:" + JSON.stringify(this.$route.query));
    this.condition.settlementBatchNo = this.$route.query.settlementBatchNo;
    this.fetchData();
  },
  methods: {
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
        .getSettlementDetail(this.condition)
        .then(
          res => {
            debugger;
            this.tableData = res.data;

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
        .settlementPayReviewDO(this.condition)
        .then(
          res => {
            this.returnModel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    }
  }
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: auto;
  margin-right: auto;
  left: 40%;
  .bodyTop {
    height: 100px;
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
