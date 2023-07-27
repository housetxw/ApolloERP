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
        <el-form-item prop="PurchaseNo">
          <el-input
            v-model="condition.PurchaseNo"
            placeholder="采购单号"
            style="width: 120px"
          ></el-input>
        </el-form-item>
        <el-form-item prop="productId">
          <el-input
            v-model="condition.productId"
            placeholder="产品编号"
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
            style="width: 200px"
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
      </template>

      <template v-slot:tb_cols>
        <rg-table-column prop="saleOrder" label="销售单号" class-name="order">
          <template scope="scope">
            <router-link
              :to="{
                name: 'orderdetails',
                params: { orderNo: scope.row.saleOrder },
              }"
            >
              <el-button size="mini" type="text">{{
                scope.row.saleOrder
              }}</el-button>
            </router-link>
          </template>
        </rg-table-column>
        <rg-table-column prop="shopId" label="门店编号" fix-min />
        <rg-table-column prop="simpleName" label="门店名称" />
        <rg-table-column prop="productId" label="产品编号" fix-min />
        <rg-table-column prop="productName" label="产品名称" fix-min />
        <rg-table-column prop="stockNum" label="库存数量" fix-min />
        <rg-table-column prop="salePrice" label="销售单价" is-money fix-min />
        <rg-table-column prop="totalNumber" label="销售数量" fix-min />
        <rg-table-column prop="saleOrderPrice" label="销售金额" is-money fix-min />
        <!-- <rg-table-column prop="oeNumber" label="原厂编号" fix-min />
        <rg-table-column prop="categoryCode" label="产品类目" fix-min /> -->
        <rg-table-column prop="venderShortName" label="供应商简称" fix-min />
        <rg-table-column prop="purchasePrice" label="采购单价" is-money fix-min />
        <rg-table-column prop="purchaseNumber" label="采购数量" fix-min />
        <rg-table-column prop="purchaseTotalPrice" label="采购金额" is-money fix-min />
        <rg-table-column prop="purchaseOrder" label="采购单号" fix-min />
        
        <rg-table-column
          prop="actualAmount"
          label="订单实收金额"
          is-money
          fix-min
        />
        <rg-table-column
          prop="createTime"
          label="销售时间"
          is-datetime
          fix-min
        />
        <rg-table-column
          prop="purchaseTime"
          label="采购时间"
          is-datetime
          fix-min
        />
        <rg-table-column prop="carNumber" label="车牌号" fix-min />
        <rg-table-column prop="carInfo" label="车型信息" fix-min />


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
        PurchaseNo: "",
        productId: "",
        StartCreateTime: "2022-04-01",
        EndCreateTime: "",
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
        .GetOrderOutProductsProfitReport(this.condition)

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
          //this.condition.shopId=undefined;
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
      //this.condition.pageIndex = this.currentPage = 1;
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
