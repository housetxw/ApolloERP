<template>
  <main class="container">
    <!-- 首页 -->
    <section>
      <rg-page
        background
        id="indexPage"
        :pageIndex="condition.pageIndex"
        :pageSize="condition.pageSize"
        :dataCount="totalList"
        :tableLoading="tableLoading"
        :tableData="tableData"
        :pageChange="pageChange"
        :headerBtnWidth="180"
        :searching="search"
      >
        <template v-slot:condition>
          <el-form-item>
            <el-input
              class="input"
              clearable
              placeholder="请输入手机号或用户名"
              prefix-icon="el-icon-search"
              v-model="condition.userName"
            ></el-input>
          </el-form-item>
        </template>
        <template v-slot:tb_cols>
          <el-table-column
            label="订单号"
            prop="orderNo"
            align="center"
          ></el-table-column>

          <el-table-column
            label="用户名"
            prop="userName"
            align="center"
          ></el-table-column>
          <rg-table-column label="手机号" prop="userPhone" />
          <rg-table-column label="实付金额" prop="actualAmount" />
          <el-table-column
            label="下单时间"
            prop="orderTime"
            align="center"
          ></el-table-column>
        </template>
      </rg-page>
    </section>
    <!-- 首页 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { orderCenterSvc } from "@/api/orderCenter/orderCenter";
import { isNumber } from "util";
export default {
  data() {
    return {
      pickerOptionsStart: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        }
      },
      input: undefined,

      readonly: true,
      tableLoading: false,
      currentPage: 1,

      //table页面的条件
      condition: {
        pageIndex: 1,
        pageSize: 20,
        userName: undefined
      },

      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",

      formLabelWidth: "120px"
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
    //页面初始化函数
    this.fetchData();
  },
  methods: {
    clickRow(row, column, event) {
      console.log(row.id);
    },

    fetchData() {
      orderCenterSvc
        .getPackageCardRecords(this.condition)
        .then(
          res => {
            //  this.tableData = res.data;
            var data = res.data;

            this.tableData = data.items;
            this.totalList = data.totalItems;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    pageChange(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    pageTurning() {
      this.condition.pageIndex = this.currentPage;
      this.getPaged();
    },

    getPaged(flag) {
      this.tableLoading = true;

      console.log("condition: " + JSON.stringify(this.condition));
      orderCenterSvc
        .getPackageCardRecords(this.condition)
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
<style lang="scss"></style>
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
>>> .el-dialog__wrapper .rg-dialog .dialog_body {
  padding: 20px 40px;
}
>>> .header-search,
.search-line {
  padding-bottom: 10px;
}
</style>
