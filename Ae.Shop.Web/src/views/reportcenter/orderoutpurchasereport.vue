<template>
  <main class="container">
    <rg-page
      id="indexPage"
      :pageIndex="condition.PageIndex"
      :pageSize="condition.PageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :tableData="tableData"
      :pageChange="pageTurning"
      :searching="searchTable"
      :conditionModel="condition"
      :defaultCollapse="false"
      :showReset="true"
      :tableRowClassName="tableRowClassName"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item prop="OrderNo">
          <el-input
            v-model="condition.orderNo"
            placeholder="订单号"
            style="width: 120px"
          ></el-input>
        </el-form-item>

        <el-form-item>
          <el-col :span="11" prop="StartCreateTime">
            <el-date-picker
              type="date"
              style="width: 135px"
              placeholder="创建开始日期"
              @change="searchTable"
              v-model="condition.startCreateTime"
            ></el-date-picker>
          </el-col>
          <el-col class="line" :span="1">~</el-col>
          <el-col :span="11" prop="EndCreateTime">
            <el-date-picker
              type="date"
              style="width: 135px"
              placeholder="创建结束日期"
              @change="searchTable"
              v-model="condition.endCreateTime"
            ></el-date-picker>
          </el-col>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button
          type="primary"
          size="mini"
          icon="el-icon-plus"
          @click="exportData()"
          >导出记录</el-button
        >
      </template>
      <template v-slot:tb_cols>
        <rg-table-column prop="saleOrder" label="销售单号" class-name="order">
          <template scope="scope">
            <router-link
              :to="{
                name: 'orderdetails',
                params: { orderNo: scope.row.saleOrder }
              }"
            >
              <el-button size="mini" type="text">{{
                scope.row.saleOrder
              }}</el-button>
            </router-link>
          </template>
        </rg-table-column>
        <!-- <rg-table-column prop="shopId" label="门店编号" fix-min />
        <rg-table-column prop="simpleName" label="门店名称" /> -->
        <rg-table-column prop="productId" label="产品编号" fix-min />
        <rg-table-column prop="productName" label="产品名称" fix-min />
        <rg-table-column prop="oeNumber" label="原厂编号" fix-min />
        <rg-table-column prop="categoryCode" label="产品类目" fix-min />
        <rg-table-column prop="venderShortName" label="供应商简称" fix-min />
        <rg-table-column prop="purchaseOrder" label="采购单号" fix-min />
        <rg-table-column prop="carNumber" label="车牌号" fix-min />
        <rg-table-column prop="carInfo" label="车型信息" fix-min />
        <rg-table-column
          prop="purchasePrice"
          label="采购单价"
          is-money
          fix-min
        />
        <rg-table-column prop="salePrice" label="销售定价" is-money fix-min />

        <rg-table-column
          prop="saleOrderPrice"
          label="销售单价"
          is-money
          fix-min
        />
        <rg-table-column prop="totalNumber" label="销售数量" fix-min />
        <rg-table-column
          prop="purchaseTotalPrice"
          label="采购金额"
          is-money
          fix-min
        />

        <rg-table-column
          prop="actualAmount"
          label="实收金额"
          is-money
          fix-min
        />
        <!-- <rg-table-column
          prop="saleOrderPrice"
          label="实收金额"
          is-money
          fix-min
        /> -->

        <rg-table-column
          prop="createTime"
          label="创建时间"
          is-datetime
          fix-min
        />
      </template>
    </rg-page>
  </main>
</template>

<script>
import { orderCenterSvc } from "@/api/orderCenter/orderCenter";

export default {
  name: "orderOutProduct",
  data() {
    return {
      flag: this.global.deletedFlag,
      dialogFormVisible: false,
      tableLoading: false,
      totalList: 0,
      tableData: [],
      currentPage: 1,
      //表单列表的条件
      condition: {
        orderNo: undefined,
        startCreateTime: undefined,
        endCreateTime: undefined,
        pageIndex: 1,
        pageSize: 20
      },

      loading: false,
      shopSelCondition: {
        simpleName: undefined
      },
      shopSel: []
    };
  },
  created() {
    this.getTable();
  },
  methods: {
    exportData() {
      orderCenterSvc
        .exportData(this.condition)
        .then(
          res => {
            debugger;

            let blob = new Blob([res], { type: res.type });
            if (window.navigator && window.navigator.msSaveOrOpenBlob) {
              window.navigator.msSaveOrOpenBlob(
                res,
                `用户列表_${this.formatDate(new Date(), "yyyyMMddhhmmss") +
                  this.getRandomInt(1000, 9999)}.xlsx`
              );
            } else {
              let downloadElement = document.createElement("a");
              let href = window.URL.createObjectURL(blob); //创建下载的链接
              downloadElement.href = href;
              downloadElement.download =
                "订单外采统计报表_" +
                this.formatDate(new Date(), "yyyyMMddhhmmss") +
                this.getRandomInt(1000, 9999) +
                ".xlsx"; //下载后文件名
              document.body.appendChild(downloadElement);
              downloadElement.click(); //点击下载
              document.body.removeChild(downloadElement); //下载完成移除元素
              window.URL.revokeObjectURL(href); //释放blob对象
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    formatDate(date, fmt) {
      if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(
          RegExp.$1,
          (date.getFullYear() + "").substr(4 - RegExp.$1.length)
        );
      }
      let o = {
        "M+": date.getMonth() + 1,
        "d+": date.getDate(),
        "h+": date.getHours(),
        "m+": date.getMinutes(),
        "s+": date.getSeconds()
      };
      for (let k in o) {
        if (new RegExp(`(${k})`).test(fmt)) {
          let str = o[k] + "";
          fmt = fmt.replace(
            RegExp.$1,
            RegExp.$1.length === 1 ? str : ("00" + str).substr(str.length)
          );
        }
      }
      return fmt;
    },
    getRandomInt(min, max) {
      return Math.floor(Math.random() * (max - min + 1)) + min;
    },
    getShopinfo(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.simpleName = query;
          orderCenterSvc
            .getShopList(this.shopSelCondition)
            .then(
              res => {
                this.shopSel = res.data;
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
    newOrder() {
      this.$router.push({ path: "/orderCenter/newOrder" });
    },
    getTable() {
      this.GetPaged();
    },
    tableRowClassName(row, rowIndex) {
      return row.orderStatus == 40 ? "invalid" : "";
    },
    //获取订单列表
    GetOrderList() {
      orderCenterSvc
        .GetOrderList()
        .then(
          res => {
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

    //获取分页
    GetPaged(flag) {
      this.tableLoading = true;

      debugger;
      console.log("condition: " + JSON.stringify(this.condition));
      if (this.condition.shopId == "" || this.condition.shopId == undefined) {
        this.condition.shopId = 0;
      }

      orderCenterSvc
        .GetOrderOutProductsProfitReport(this.condition)

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
            // console.log(111, this.tableData);
          },
          error => {
            console.log(error);
          }
        )
        .then()
        .catch(() => {})
        .finally(() => {
          this.condition.shopId = undefined;
          this.tableLoading = false;
        });
    },
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.PageIndex = p.pageIndex;
      this.condition.PageSize = p.pageSize;
      this.GetPaged();
    },

    searchTable(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.GetPaged(flag);
    }
  },
  activated() {
    //刷新数据
    if (this.$route.meta.isFresh) {
      this.GetPaged();
      this.$route.meta.isFresh = false; //记得这里一定要设置一下false。
    }
  },
  beforeRouteEnter(to, from, next) {
    // console.log(2, to, from);
    //  if(to.query["reshed"])
    // if (from.name !== "Index") {
    //   to.meta.isFresh = true;
    // }
    next();
  }
};
</script>
<style lang="scss" scoped>
>>> td.order {
  color: blue;
}
</style>
