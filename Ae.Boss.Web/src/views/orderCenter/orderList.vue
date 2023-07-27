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
      :on-reset="
        (p) => {
          condition.LikeType = 0;
        }
      "
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item prop="ContactPhone">
          <el-input
            v-model="condition.ContactPhone"
            placeholder="客户手机"
            style="width: 120px"
          ></el-input>
        </el-form-item>
        <!-- <el-form-item prop="OrderNo">
          <el-input
            v-model="condition.OrderNo"
            placeholder="订单号"
            style="width: 120px"
          ></el-input>
        </el-form-item> -->
        <el-form-item prop="LikeValue">
          <el-input
            placeholder="请输入内容"
            v-model="condition.LikeValue"
            class="input-with-select"
            :readonly="condition.LikeType == 0"
          >
            <el-select
              v-model="condition.LikeType"
              slot="prepend"
              placeholder="请选择"
              style="width: 100px"
              @change="
                (p) => {
                  if (p === 0) condition.LikeValue = '';
                }
              "
            >
              <el-option
                v-for="item in search"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              ></el-option>
            </el-select>
          </el-input>
        </el-form-item>

        <!-- <el-form-item prop="CarNumber">
          <el-input v-model="condition.CarNumber" placeholder="车牌" style="width:100px"></el-input>
        </el-form-item>-->
        <el-form-item prop="OrderType">
          <el-select
            v-model="condition.OrderType"
            placeholder="订单类型"
            style="width: 100px; color: gray"
            v-bind:rg="condition.OrderType"
            @change="searchTable"
          >
            <el-option
              v-for="item in OrderType"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="ProductType">
          <el-select
            v-model="condition.ProductType"
            placeholder="订单来源"
            style="width: 100px; color: gray"
            v-bind:rg="condition.ProductType"
            @change="searchTable"
          >
            <el-option
              v-for="item in ProductType"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="OrderStatus">
          <el-select
            v-model="condition.OrderStatus"
            placeholder="请选择订单状态"
            style="width: 100px; color: gray"
            v-bind:rg="condition.OrderStatus"
            @change="searchTable"
          >
            <el-option
              v-for="item in orderStatus"
              :key="item.id"
              :label="item.status"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="OrderChannel">
          <el-select
            v-model="condition.OrderChannel"
            placeholder="请选择订单渠道"
            style="width: 100px"
            v-bind:rg="condition.OrderChannel"
            @change="searchTable"
          >
            <el-option
              v-for="item in OrderChannel"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="DeliveryMethod">
          <el-select
            v-model="condition.DeliveryMethod"
            placeholder="请选择配送方式"
            style="width: 100px"
            v-bind:rg="condition.DeliveryMethod"
            @change="searchTable"
          >
            <el-option
              v-for="item in deliveryMethod"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="DeliveryStatus">
          <el-select
            v-model="condition.DeliveryStatus"
            placeholder="配送状态"
            style="width: 100px"
            v-bind:rg="condition.DeliveryStatus"
            @change="searchTable"
          >
            <el-option
              v-for="item in deliveryStatus"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="IsNeedInstall">
          <el-select
            v-model="condition.IsNeedInstall"
            placeholder="安装类型"
            style="width: 100px"
            v-bind:rg="condition.IsNeedInstall"
            @change="searchTable"
          >
            <el-option
              v-for="item in IsNeedInstall"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>

        <el-form-item prop="PayMethod">
          <el-select
            v-model="condition.PayMethod"
            placeholder="付款方式"
            style="width: 100px"
            v-bind:rg="condition.PayMethod"
            @change="searchTable"
          >
            <el-option
              v-for="item in PayMethod"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="PayStatus">
          <el-select
            v-model="condition.PayStatus"
            placeholder="付款状态"
            style="width: 100px"
            v-bind:rg="condition.PayStatus"
            @change="searchTable"
          >
            <el-option
              v-for="item in PayStatus"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <!-- <el-form-item prop="OrderType">
          <el-select
            v-model="condition.OrderType"
            placeholder="订单类型"
            style="width: 100px"
            v-bind:rg="condition.OrderType"
            @change="searchTable"
          >
            <el-option
              v-for="item in OrderType"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item> -->
        <el-form-item prop="InstallStatus">
          <el-select
            v-model="condition.InstallStatus"
            placeholder="安装状态"
            style="width: 100px"
            v-bind:rg="condition.InstallStatus"
            @change="searchTable"
          >
            <el-option
              v-for="item in InstallStatus"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-col :span="11" prop="DateTimeType">
            <el-date-picker
              type="date"
              style="width: 135px"
              placeholder="创建开始日期"
              @change="searchTable"
              v-model="condition.StartDateTime"
            ></el-date-picker>
          </el-col>
          <el-col class="line" :span="1">~</el-col>
          <el-col :span="11" prop="EndDateTime">
            <el-date-picker
              type="date"
              style="width: 135px"
              placeholder="创建结束日期"
              @change="searchTable"
              v-model="condition.EndDateTime"
            ></el-date-picker>
          </el-col>
        </el-form-item>
        <el-form-item>
          <el-col :span="11" prop="InstallStartTime">
            <el-date-picker
              type="date"
              style="width: 135px"
              placeholder="安装开始日期"
              @change="searchTable"
              v-model="condition.InstallStartTime"
            ></el-date-picker>
          </el-col>
          <el-col class="line" :span="1">~</el-col>
          <el-col :span="11" prop="InstallEndTime">
            <el-date-picker
              type="date"
              style="width: 135px"
              placeholder="安装结束日期"
              @change="searchTable"
              v-model="condition.InstallEndTime"
            ></el-date-picker>
          </el-col>
        </el-form-item>

        <el-form-item prop="searchLocationId">
          <el-select
            v-model="condition.searchLocationId"
            @change="updateLocation"
            filterable
            remote
            clearable
            reserve-keyword
            placeholder="请输入门店名称"
            :remote-method="getShopinfo"
            :loading="loading"
          >
            <el-option
              v-for="item in shopSel"
              :key="item.id"
              :label="item.simpleName"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" icon="el-icon-plus" @click="newOrder()"
          >新增</el-button
        >
      </template>
      <template v-slot:tb_cols>
        <rg-table-column prop="orderNo" label="订单编号" class-name="order">
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
        <rg-table-column
          prop="productType"
          label="订单来源"
          width="80"
          align="center"
        >
          <template scope="scope">{{
            {
              0: "普通订单",
              1: "购买核销码订单",
              2: "使用核销码订单",
              5: "上门服务订单",
              6: "会员卡订单",
              7: "代客下单订单",
              8: "门店押金单",
              9: "公司押金单",
              11: "技师订单",
              12: "支付货款订单",
              13: "代收款订单",
              14: "购买套餐卡订单",
              15: "使用套餐卡订单",
              16: "大客户订单",
              17: "秒杀订单",
              18: "热销产品订单",
              19: "搜索入口订单",
              20: "快速下单订单",
              21: "客户向小仓订单",
              22: "门店向小仓订单",
              23: "月结对账订单",
              24: "美团核销订单",
            }[scope.row.productType] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column prop="shopId" v-if="false" label="门店编号" fix-min />
        <rg-table-column prop="shopName" label="门店名称" fix-min />
        <rg-table-column prop="userName" label="客户姓名" fix-min />
        <rg-table-column prop="contactPhone" label="客户电话" fix-min />
        <!-- <rg-table-column prop="carNumber" label="车牌" fix-min /> -->
        <rg-table-column
          prop="actualAmount"
          label="实付金额"
          is-money
          fix-min
        />
        <rg-table-column
          prop="orderStatus"
          label="订单状态"
          fix-min
          align="center"
        >
          <template scope="scope">{{
            { 10: "已提交", 20: "已确认", 30: "已完成", 40: "已取消" }[
              scope.row.orderStatus
            ] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column prop="channel" label="订单渠道" fix-min align="center">
          <template scope="scope">{{
            { 0: "未设置", 1: "总部C端", 2: "总部门店", 3: "电销下单", 4: "美团", 5: "大客户" }[
              scope.row.channel
            ] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column
          prop="deliveryMethod"
          label="配送方式"
          width="100"
          align="center"
        >
          <template scope="scope">{{
            { 0: "未设置", 1: "自配", 2: "快递" }[scope.row.deliveryMethod] ||
            ""
          }}</template>
        </rg-table-column>
        <rg-table-column
          prop="deliveryStatus"
          label="配送状态"
          width="80"
          align="center"
        >
          <template scope="scope">{{
            { 0: "未设置", 1: "已配送" }[scope.row.deliveryStatus] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column
          prop="deliveryStatus"
          label="签收状态"
          width="80"
          align="center"
        >
          <template scope="scope">{{
            { 0: "未签收", 1: "已签收" }[scope.row.signStatus || 0] || ""
          }}</template>
        </rg-table-column>

        <rg-table-column
          prop="payMethod"
          label="付款方式"
          width="80"
          align="center"
        >
          <template scope="scope">{{
            { 0: "未设置", 1: "在线支付", 2: "到店付款", 3: "月结" }[
              scope.row.payMethod
            ] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column
          prop="payStatus"
          label="付款状态"
          width="80"
          align="center"
        >
          <template scope="scope">{{
            { 0: "未支付", 1: "已支付" }[scope.row.payStatus] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column
          prop="orderType"
          label="订单类型"
          fix-min
          align="center"
        >
          <template scope="scope">{{
            {
              0: "未设置",
              1: "轮胎安装",
              2: "保养服务",
              3: "美容洗车",
              4: "钣金喷漆",
              5: "维修改装",
              6: "其他",
            }[scope.row.orderType] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column
          prop="createTime"
          label="创建时间"
          is-datetime
          fix-min
        />
        <!-- <el-table-column prop="name" label="预约时间" width="80"></el-table-column> -->
        <rg-table-column
          prop="installType"
          label="安装类型"
          width="80"
          align="center"
        >
          <template scope="scope">{{
            { 0: "无需安装", 1: "到店安装", 2: "上门安装" }[
              scope.row.installType
            ] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column
          prop="installStatus"
          label="安装状态"
          width="80"
          align="center"
        >
          <template scope="scope">{{
            { 0: "未安装", 1: "已安装" }[scope.row.installStatus] || ""
          }}</template>
        </rg-table-column>

        <rg-table-column
          prop="installTime"
          label="安装时间"
          is-datetime
          fix-min
        />
        <rg-table-column prop="carInfo" label="车辆信息" fix-min />

        <el-table-column
          prop="name"
          label="操作"
          fixed="right"
          width="120"
          align="center"
        >
          <template slot-scope="scope">
            <el-button-group>
              <router-link
                :to="{
                  name: 'orderdetails',
                  params: { orderNo: scope.row.orderNo },
                }"
              >
                <el-button size="mini" type="primary">订单详情</el-button>
              </router-link>

              <router-link
                :to="{
                  name: 'createtousu',
                  query: { orderNo: scope.row.orderNo },
                }"
              >
                <el-button size="mini" type="warning">售后</el-button>
              </router-link>
            </el-button-group>
          </template>
        </el-table-column>
      </template>
    </rg-page>
  </main>
</template>



<script>
import { orderCenterSvc } from "@/api/orderCenter/orderCenter";
import { appSvc } from "@/api/fms/accountcheck";
export default {
  name: "orderlist",
  data() {
    return {
      flag: this.global.deletedFlag,
      dialogFormVisible: false,
      tableLoading: false,
      totalList: 0,
      //搜索下拉框数据集合
      // 模糊搜索条件类型（0未设置 1订单编号 2商品名称 3客户姓名）
      search: [
        { id: 0, name: "请选择" },
        { id: 1, name: "订单编号" },
        { id: 2, name: "商品名称" },
        { id: 3, name: "客户姓名" },
      ],

      //订单状态（0全部 10已提交 20已确认 30已完成 40已取消）
      orderStatus: [
        { id: 0, status: "订单状态" },
        { id: 10, status: "已提交" },
        { id: 20, status: "已确认" },
        { id: 30, status: "已完成" },
        { id: 40, status: "已取消" },
      ],
      //订单渠道（0全部 1:总部C端 2:总部门店 3:电销下单
      OrderChannel: [
        { id: 0, name: "订单渠道" },
        { id: 1, name: "总部C端" },
        { id: 2, name: "总部门店" },
        { id: 3, name: "电销下单" },
        { id: 4, name: "美团" },
        { id: 5, name: "大客户" },
      ],
      //配送方式（0未设置 1自配 2快递）
      deliveryMethod: [
        { id: 0, name: "配送方式" },
        { id: 1, name: "自配" },
        { id: 2, name: "快递" },
      ],
      //配送状态（-1全部 0未配送 1已配送）
      deliveryStatus: [
        { id: -1, name: "配送状态" },
        { id: 0, name: "未配送" },
        { id: 1, name: "已配送" },
      ],
      //是否需要安装服务（-1全部 0不需要 1需要）
      IsNeedInstall: [
        { id: -1, name: "安装类型" },
        { id: 0, name: "无需安装" },
        { id: 1, name: "需安装" },
      ],
      //安装服务状态（-1全部 0未安装 1已安装）
      InstallStatus: [
        { id: -1, name: "安装状态" },
        { id: 0, name: "未安装" },
        { id: 1, name: "已安装" },
      ],
      //支付方式（-1：全部 0：在线支付 1：到店付款）
      PayMethod: [
        { id: -1, name: "支付方式" },
        { id: 1, name: "在线支付" },
        { id: 2, name: "到店付款" },
        { id: 3, name: "月结" },
      ],
      //支付状态（-1全部 0未支付 1已支付）
      PayStatus: [
        { id: -1, name: "支付状态" },
        { id: 0, name: "未支付" },
        { id: 1, name: "已支付" },
      ],
      //订单类型
      OrderType: [
        { id: 0, name: "订单类型" },
        { id: 1, name:  "轮胎安装"},
        { id: 2, name:  "保养服务"},
        { id: 3, name:  "美容洗车"},
        { id: 4, name:  "钣金喷漆"},
        { id: 5, name:  "维修改装"},
        { id: 6, name:  "其他"},      
      ],
      //订单方式（0全部 1轮胎 2保养 3美容）
      ProductType: [
        { id: -1, name: "订单分类" },
        { id: 0, name: "普通订单" },
        { id: 1, name: "购买核销码订单" },
        { id: 2, name: "使用核销码订单" },
        { id: 5, name: "上门服务订单" },
        { id: 6, name: "会员卡订单" },
        { id: 7, name: "代客下单订单" },
        { id: 8, name: "门店押金单" },
        { id: 9, name: "公司押金单" },
        { id: 11, name: "技师订单" },
        { id: 12, name: "支付货款订单" },
        { id: 13, name: "代收款订单" },
        { id: 14, name: "购买套餐卡订单" },
        { id: 15, name: "使用套餐卡订单" },
        { id: 16, name: "大客户订单" },
        { id: 17, name: "秒杀订单" },
        { id: 18, name: "热销产品订单" },
        { id: 19, name: "搜索入口订单" },
        { id: 20, name: "快速下单订单" },
        { id: 21, name: "客户向小仓订单" },
        { id: 22, name: "门店向小仓订单" },
        { id: 23, name: "月结对账订单" },
        { id: 24, name: "美团核销订单" },
      ],
      //  Canc:Cancel,
      //订单状态（0全部 10已提交 20已确认 30已完成 40已取消）
      OrderStatus: [
        { id: 10, status: "已提交" },
        { id: 20, status: "已确认" },
        { id: 30, status: "已完成" },
        { id: 40, status: "已取消" },
      ],

      form: {},
      tableData: [],
      currentPage: 1,

      //表单列表的条件
      condition: {
        ContactPhone: "",
        LikeType: 0,
        LikeValue: "",
        OrderStatus: 0,
        OrderChannel: 0,
        DeliveryMethod: 0,
        DeliveryStatus: -1,
        IsNeedInstall: -1,
        InstallStatus: -1,
        PayMethod: -1,
        PayStatus: -1,
        OrderType: 0,
        DateTimeType: 1,
        StartDateTime: "",
        EndDateTime: "",
        PageIndex: 1,
        PageSize: 20,
        ProductType: -1,
        OrderType: 0,
        OrderNo: "",
        searchLocationId: undefined,
        shopId: undefined,
        InstallStartTime: "",
        InstallEndTime: "",
      },
      shopSelCondition: {
        simpleName: undefined,
      },
      shopSel: [],
      loading: false,
      // tableData2:{
      //   reasons:[],
      //   instruction:[]
      // },
      tableData2: [
        {
          reasons: [],
          instruction: [],
        },
      ],

      reasons: [],
    };
  },
  created() {
    this.getTable();
  },
  methods: {
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

      if (
        this.condition.searchLocationId != "" &&
        this.condition.searchLocationId != undefined
      ) {
        this.condition.shopId = this.condition.searchLocationId;
      }

      console.log("condition: " + JSON.stringify(this.condition));

      orderCenterSvc
        .GetOrderList(this.condition)

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
    updateLocation() {
      if (this.condition.searchLocationId == "") {
        this.condition.searchLocationId = undefined;
        this.condition.shopId = undefined;
      }
    },

    getShopinfo(query) {
      if (query != "") {
        debugger;
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.simpleName = query;
          appSvc
            .getShopListAsync(this.shopSelCondition)
            .then(
              (res) => {
                var resData = res.data;
                this.shopSel = resData.items;
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
