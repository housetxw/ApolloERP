<template>
  <main class="container">
    <!-- 首页：列表数据 -->
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :tableData="tableData"
      :pageChange="pageChange"
      :searching="search"
      :tableRowClassName="tableRowClassName"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item prop="key">
          <el-input
            v-model="condition.key"
            size="mini"
            style="width: 100%"
            placeholder="查询 员工姓名（模糊搜索）+手机号（精确搜索）"
          >
            <i slot="prefix" class="el-input__icon el-icon-search"></i>
          </el-input>
        </el-form-item>

        <el-form-item prop="createDate">
          <el-date-picker
            v-model="condition.createDate"
            type="daterange"
            range-separator="-"
            start-placeholder="日期开始"
            end-placeholder="日期结束"
            size="mini"
            style="width: 220px"
          ></el-date-picker>
        </el-form-item>
      </template>

      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <el-table-column
          prop="employeeName"
          label="员工姓名"
          min-width="30"
          class-name="order"
        >
          <!-- <template slot-scope="scope">
            <el-button type="text" @click="showDetail(scope.row)">{{
              scope.row.orderNo
            }}</el-button>
          </template> -->
        </el-table-column>
        <el-table-column
          prop="mobile"
          label="手机号码"
          align="center"
          min-width="30"
        >
          <!-- <template slot-scope="scope">
            {{ ["", "C端", "门店"][scope.row.channel] }}
          </template> -->
        </el-table-column>

        <el-table-column
          prop="installPoint"
          label="安装绩效"
          align="right"
          min-width="20"
        >
          <!-- <template slot-scope="scope">{{
            scope.row.installPoint
          }}</template> -->
        </el-table-column>
        <el-table-column
          prop="pullNewPoint"
          label="新客绩效"
          min-width="40"
        ></el-table-column>
        <el-table-column
          prop="cunsumePoint"
          label="新客首消费绩效"
          min-width="40"
        ></el-table-column>
        <el-table-column
          prop="totalPoint"
          label="总绩效"
          min-width="40"
        ></el-table-column>

        <!-- Table数据 -->
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->
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
import { reportFormSvc } from "@/api/reportForm/reportForm.js";
export default {

  name: "demopage",
  data() {
    return {


      tableLoading: false,
      condition: {
        key: "",
        createDate: ["", ""],
        pageIndex: 1,
        pageSize: 20,
        startDate:"",
        endDate:""
      },
      tableData: [],
      totalList: 0,
      //订单状态（0全部 10已提交 20已确认 30已完成 40已取消）
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
      if (data.createDate != null) {
        data.startDate =
          data.createDate.length > 1 ? data.createDate[0] : "";
        data.endDate =
          data.createDate.length > 1 ? data.createDate[1] : "";
      }

      console.log(data);

      this.tableLoading = true;
      reportFormSvc
        .GetEmployeePerformanceList(data)
        .then(
          (res) => {
            let data = res.data;
            this.tableData = data;
            this.totalList = data.length;
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
  },
};
</script>
