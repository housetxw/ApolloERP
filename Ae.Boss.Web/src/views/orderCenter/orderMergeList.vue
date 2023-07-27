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
        <el-form-item>
          <el-col :span="11" prop="StartCreateTime">
            <el-date-picker
              type="date"
              style="width: 135px"
              placeholder="创建开始日期"
              @change="searchTable"
              v-model="condition.StartCreateTime"
            ></el-date-picker>
          </el-col>
          <el-col class="line" :span="1">~</el-col>
          <el-col :span="11" prop="EndCreateTime">
            <el-date-picker
              type="date"
              style="width: 135px"
              placeholder="创建结束日期"
              @change="searchTable"
              v-model="condition.EndCreateTime"
            ></el-date-picker>
          </el-col>
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
        <rg-table-column prop="mergeNo" label="合并支付单号" fix-min />

        <rg-table-column prop="payNo" label="支付单号" fix-min />
        <rg-table-column prop="tradeNo" label="交易单号" fix-min />
        <rg-table-column prop="buyerAccountfrom" label="买家账号" fix-min />
        <rg-table-column prop="status" label="合并支付状态" fix-min >
         <template scope="scope">{{
            { 0: "新建", 1: "已调用等待回调中", 2: "成功", 3: "失败",4:"取消",5:"关闭" }[
              scope.row.status
            ] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column prop="innerNoType" label="合并支付类型"  fix-min >
        <template scope="scope">{{
            { 0: "未设置", 1: "订单号", 2: "预约", 3: "到店",4:"兑换码",5:"微信转支付宝（付）",6:"微信转支付宝（收）",7:"合并支付" }[
              scope.row.innerNoType
            ] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column prop="channel" label="支付渠道"  fix-min >
        <template scope="scope">{{
            { 0: "未设置", 1: "微信支付", 2: "支付宝", 3: "银联"}[
              scope.row.channel
            ] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column prop="method" label="支付方式"  fix-min >
        <template scope="scope">{{
            { 0: "未设置", 1: "在线支付", 2: "到店付款"}[
              scope.row.method
            ] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column prop="payTime" label="支付日期" fix-min />
        <rg-table-column prop="amount" label="支付金额" fix-min />

        <rg-table-column
          prop="createTime"
          label="创建时间"
          is-datetime
          fix-min
        />
        <rg-table-column prop="createBy" label="创建人" fix-min />
      </template>
    </rg-page>
  </main>
</template>



<script>
import { orderCenterSvc } from "@/api/orderCenter/orderCenter";
export default {
  name: "orderMergeList",
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
        PageIndex: 1,
        PageSize: 20
      },

      loading: false,
    };
  },
  created() {
    this.getTable();
  },
  methods: {
    getTable() {
      this.GetPaged();
    },

    //获取分页
    GetPaged(flag) {
      this.tableLoading = true;

      console.log("condition: " + JSON.stringify(this.condition));



      orderCenterSvc
        .GetMergeOrderList(this.condition)

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
           this.condition.shopId=undefined;
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
