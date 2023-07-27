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
            v-model="condition.OrderNo"
            placeholder="订单号"
            style="width: 120px"
          ></el-input>
        </el-form-item>
        <el-form-item prop="shopId">
          <el-select
            v-model="condition.shopId"
            filterable
            remote
            clearable
            reserve-keyword
            placeholder="请输入门店名称"
            :remote-method="getShopinfo"
            :loading="loading"
            style="width: 300px"
          >
            <el-option
              v-for="item in shopSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-col :span="11" prop="StartCreateTime">
            <el-date-picker
              type="date"
              style="width: 135px"
              placeholder="创建开始日期"
        
              v-model="condition.StartCreateTime"
            ></el-date-picker>
          </el-col>
          <el-col class="line" :span="1">~</el-col>
          <el-col :span="11" prop="EndCreateTime">
            <el-date-picker
              type="date"
              style="width: 135px"
              placeholder="创建结束日期"
        
              v-model="condition.EndCreateTime"
            ></el-date-picker>
          </el-col>
        </el-form-item>
        <el-form-item prop="ProductCode">
          <el-input
            v-model="condition.ProductCode"
            placeholder="产品编号"
            style="width: 120px"
          ></el-input>
        </el-form-item>
      </template>

      <template v-slot:tb_cols>
        <rg-table-column prop="orderNo" label="订单号" class-name="order">
          <template scope="scope">
            <router-link
              :to="{
                name: 'orderdetails',
                params: { orderNo: scope.row.orderNo },
              }"
            >
              <el-button size="mini" type="text">{{
                scope.row.orderNo
              }}</el-button>
            </router-link>
          </template>
        </rg-table-column>
        <rg-table-column prop="shopId" label="门店编号" fix-min />
        <rg-table-column prop="simpleName" label="门店名称" />
        <rg-table-column prop="productId" label="产品编号" fix-min />
        <rg-table-column prop="productName" label="产品名称" fix-min />
        <rg-table-column prop="productAttribute" label="产品性质" fix-min>
          <template scope="scope">{{
            {
              0: "实物产品",
              1: "套餐产品",
              2: "服务产品",
              3: "数字产品",
              4: "门店项目",
              5: "门店外采",
            }[scope.row.productAttribute] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column
          prop="description"
          label="产品描述"
          v-if="false"
          fix-min
        />

        <rg-table-column prop="categoryCode" label="产品类目" fix-min />

        <rg-table-column prop="totalNumber" label="销售数量"  fix-min />
        <rg-table-column prop="amount" label="金额" is-money fix-min />
        <rg-table-column prop="totalAmount" label="总金额" is-money fix-min />

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
import { appSvc } from "@/api/stock/stock";
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
        OrderNo: "",
        StartCreateTime: "",
        EndCreateTime: "",
        ProductCode: "",
        PageIndex: 1,
        PageSize: 20,
        shopId: undefined,
      },

      loading: false,
      shopSelCondition: {
        simpleName: undefined,
      },
      shopSel: [],
    };
  },
  created() {
    this.getTable();
  },
  methods: {
    getShopinfo(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.simpleName = query;
          appSvc
            .getShopList(this.shopSelCondition)
            .then(
              (res) => {
                this.shopSel = res.data;
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
          (res) => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    //获取分页
    GetPaged(flag) {
      this.tableLoading = true;

      console.log("condition: " + JSON.stringify(this.condition));

      if(this.condition.shopId==""||this.condition.shopId==undefined){
        this.condition.shopId=0;
      }

      orderCenterSvc
        .GetOrderProductsReport(this.condition)

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
            // console.log(111, this.tableData);
          },
          (error) => {
            console.log(error);
          }
        )
        .then()
        .catch(() => {})
        .finally(() => {
          // this.condition.shopId=undefined;
          this.tableLoading = false;
        });
    },
    pageTurning(p) {
      debugger;
     this.currentPage = p.currentPage;
     this.condition.PageIndex = p.pageIndex;
      this.condition.PageSize = p.pageSize;
      this.GetPaged();
    },

    searchTable(flag) {
   //   this.condition.pageIndex = this.currentPage = 1;
      this.GetPaged(flag);
    },
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
  },
};
</script>
<style lang="scss" scoped>
>>> td.order {
  color: blue;
}
</style>
