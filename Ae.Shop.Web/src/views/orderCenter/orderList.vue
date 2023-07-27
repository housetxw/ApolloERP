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
        <el-form-item prop="ContactPhone">
          <el-input v-model="condition.ContactPhone"
                    placeholder="客户手机"
                    style="width: 120px"></el-input>
        </el-form-item>
        <el-form-item prop="customerName">
          <el-input v-model="condition.customerName"
                    size="mini"
                    style="width: 100%"
                    placeholder="客户姓名">
            <i slot="prefix" class="el-input__icon el-icon-search"></i>
          </el-input>
        </el-form-item>
        <el-form-item prop="orderNo">
          <el-input v-model="condition.orderNo"
                    size="mini"
                    style="width: 100%"
                    placeholder="查询订单编号">
            <i slot="prefix" class="el-input__icon el-icon-search"></i>
          </el-input>
        </el-form-item>
        <el-form-item prop="orderType">
          <el-select v-model="condition.orderType"
                     placeholder="订单类型"
                     size="mini"
                     clearable
                     style="width: 100px">
            <el-option v-for="item in orderType"
                       :key="item.id"
                       :label="item.name"
                       :value="item.id"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="ProductType">
          <el-select v-model="condition.ProductType"
                     size="mini"
                     placeholder="订单分类"
                     clearable
                     style="width: 100px">
            <el-option v-for="item in ProductType"
                       :key="item.id"
                       :label="item.name"
                       :value="item.id"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="orderStatus">
          <el-select v-model="condition.orderStatus"
                     size="mini"
                     placeholder="订单状态"
                     clearable
                     style="width: 100px">
            <el-option v-for="item in orderStatus"
                       :key="item.id"
                       :label="item.status"
                       :value="item.id"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="orderChannel">
          <el-select v-model="condition.orderChannel"
                     placeholder="订单渠道"
                     size="mini"
                     clearable
                     style="width: 100px">
            <el-option v-for="item in orderChannel"
                       :key="item.id"
                       :label="item.name"
                       :value="item.id"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="exceptionOrder">
          <el-select v-model="condition.exceptionOrder"
                     placeholder="是否异常订单"
                     size="mini"
                     clearable
                     style="width: 110px">
            <el-option v-for="item in exceptionOrder"
                       :key="item.id"
                       :label="item.name"
                       :value="item.id"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="installStatus" clearable style="width: 100px">
          <el-select v-model="condition.installStatus"
                     placeholder="安装状态"
                     style="width: 100%"
                     size="mini"
                     clearable>
            <el-option v-for="item in installStatus"
                       :key="item.id"
                       :label="item.name"
                       :value="item.id"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="payStatus" clearable>
          <el-select v-model="condition.payStatus"
                     placeholder="付款状态"
                     size="mini"
                     clearable
                     style="width: 100px">
            <el-option v-for="item in payStatus"
                       :key="item.id"
                       :label="item.name"
                       :value="item.id"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="createDate">
          <el-date-picker v-model="condition.createDate"
                          type="daterange"
                          range-separator="-"
                          start-placeholder="建单日期开始"
                          end-placeholder="建单日期结束"
                          size="mini"
                          style="width: 220px"></el-date-picker>
        </el-form-item>
        <el-form-item prop="installTime">
          <el-date-picker v-model="condition.installTime"
                          type="daterange"
                          range-separator="-"
                          start-placeholder="安装日期开始"
                          end-placeholder="安装日期结束"
                          size="mini"
                          style="width: 220px"></el-date-picker>
        </el-form-item>
      </template>

      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <el-table-column prop="orderNo"
                         label="订单编号"
                         min-width="40"
                         class-name="order">
          <template slot-scope="scope">
            <el-button type="text" @click="showDetail(scope.row)">
              {{
              scope.row.orderNo
              }}
            </el-button>
          </template>
        </el-table-column>
        <el-table-column prop="orderType"
                         label="订单类型"
                         align="center"
                         min-width="40">
          <template slot-scope="scope">
            {{
              [
                "",
                "轮胎安装",
                "保养服务",
                "美容洗车",
                "钣金喷漆",
                "维修改装",
                "其他",
              ][scope.row.orderType]
            }}
          </template>
        </el-table-column>
        <el-table-column prop="showProductType"
                         label="订单分类"
                         align="center"
                         min-width="40">
        </el-table-column>
        <el-table-column prop="shopName"
                         label="门店名称"
                         min-width="100"></el-table-column>
        <el-table-column prop="userName"
                         label="客户姓名"
                         min-width="40"></el-table-column>
        <el-table-column prop="userPhone"
                         label="客户电话"
                         min-width="40"></el-table-column>
        <!-- <el-table-column prop="channel"
                         label="订单渠道"
                         align="center"
                         min-width="40">
          <template slot-scope="scope">
            {{ ["", "C端", "门店","电销下单","美团","大客户",""][scope.row.channel] }}
          </template>
        </el-table-column> -->
        <el-table-column prop="orderStatus"
                         label="订单状态"
                         align="center"
                         min-width="40">
          <template slot-scope="scope">
            {{
              {
                v_10: "已提交",
                v_20: "已确认",
                v_30: "已完成",
                v_40: "已取消",
              }["v_" + scope.row.orderStatus] || ""
            }}
          </template>
        </el-table-column>
        <el-table-column prop="payStatus"
                         label="支付状态"
                         align="center"
                         min-width="40px">
          <template slot-scope="scope">
            {{
              { "v_-1": "未设置", v_0: "未支付", v_1: "已支付" }[
                "v_" + scope.row.payStatus
              ] || ""
            }}
          </template>
        </el-table-column>
        <!-- <el-table-column prop="installStatus"
                         label="安装状态"
                         align="center"
                         min-width="40">
          <template slot-scope="scope">
            {{
              { "v_-1": "未设置", v_0: "未安装", v_1: "已安装" }[
                "v_" + scope.row.installStatus
              ] || ""
            }}
          </template>
        </el-table-column> -->

        <el-table-column prop="actualAmount"
                         label="实付金额"
                         align="right"
                         min-width="40">
          <template slot-scope="scope">
            {{
            $jscom.toMoney(scope.row.actualAmount)
            }}
          </template>
        </el-table-column>

        <el-table-column prop="createTime"
                         label="创建时间"
                         min-width="60"
                         align="center">
          <template slot-scope="scope">
            {{
            $jscom.toYMDHm(scope.row.createTime)
            }}
          </template>
        </el-table-column>
        <!-- <el-table-column prop="installTime"
                         label="安装时间"
                         min-width="60"
                         align="center">
          <template slot-scope="scope">
            {{
            $jscom.toYMDHm(scope.row.installTime)
            }}
          </template>
        </el-table-column> -->

        <el-table-column prop="remark"
                         label="备注"
                         min-width="50"></el-table-column>
        <el-table-column prop="name"
                         label="操作"
                         align="center"
                         min-width="50"
                         fixed="right">
          <template slot-scope="scope">
            <el-row>
              <!-- <router-link :to="{path:'orderDetails/:orderNo',query:{orderNo:scope.row.orderNo}}"> -->
              <!-- <el-button size="small"
                         type="primary"
                         @click="showDetail(scope.row)">订单详情</el-button> -->

              <el-button type="text" size="small" @click="showPrint(scope.row)">查看安装单</el-button>
            </el-row>
          </template>
        </el-table-column>
        <!-- Table数据 -->
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

    <rg-dialog
      :title="detail.title"
      :visible.sync="detail.v_detail"
      :beforeClose="
        () => {
          detail.v_detail = !detail.v_detail;
        }
      "
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          detail.v_detail = !detail.v_detail;
        },
      }"
      :btnRemove="detail.cancelOrder"
      :footbar="true"
      width="98%"
      maxWidth="1024px"
      minWidth="900px"
    >
      <template v-slot:content>
        <pg-detail
          :visible="detail.v_detail"
          :orderNo="detail.orderNo"
          :cancelOrder="detail.cancelOrder"
        />
      </template>
      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          size="mini"
          type="warning"
          :disabled="detail.orderStatus == '30' ? false : true"
          @click="orderInstallReduceStock()"
          >检查出库</el-button
        >
      </template>
    </rg-dialog>

    <rg-dialog
      :title="print.title"
      :visible.sync="print.v_detail"
      v-if="print.v_detail"
      :beforeClose="
        () => {
          print.v_detail = !print.v_detail;
        }
      "
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          print.v_detail = !print.v_detail;
        },
      }"
      :footbar="true"
      width="98%"
      maxWidth="1024px"
      minWidth="900px"
    >
      <template v-slot:content>
        <install-bill
          :visible="print.v_detail"
          :orderNo="print.orderNo"
          id="printInstall"
        />
      </template>
      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          size="mini"
          type="primary"
          v-print="printObj"
          >打印</el-button
        >
      </template>
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
import { orderCenterSvc } from "@/api/orderCenter/orderCenter";
import pgDetail from "./orderDetails.vue";
import installBill from "./installBill.vue";
export default {
  components: { pgDetail, installBill },
  name: "demopage",
  data() {
    return {
      detail: {
        title: "订单详细",
        cancelOrder: {
          label: "取消订单",
          show: true,
          disabled: true,
          click: () => {},
        },
        orderNo: "",
        orderStatus: "",
        v_detail: false,
      },
      printObj: {
        id: "printInstall", //打印区域 Dom ID
        popTitle: "打印维修工单",
        extraCss: "",
        extraHead:'<meta http-equiv="Content-Language"content="zh-cn"/>,<style> #printInstall { height: auto !important; } <style>',
      },
      print: {
        title: "维修工单",
        orderNo: "",
        v_detail: false,
      },
      orderStockRequest: {
        queueNo: "",
        createBy: "",
        queueStatus: "",
        sourceType: "",
      },
      tableLoading: false,
      condition: {
        orderNo: "",
        ProductType: -1,
        orderStatus: "",
        orderChannel: "",
        installStatus: "",
        payStatus: "",
        orderType: "",
        exceptionOrder: "",
        customerName: "",
        ContactPhone: "",
        createDate: ["", ""],
        installTime: ["", ""],
        pageIndex: 1,
        pageSize: 20,
        createStartTime: "",
        createEndTime: "",
        installStartTime: "",
        installEndTime: "",
      },
      tableData: [],
      totalList: 0,
      //订单状态（0全部 10已提交 20已确认 30已完成 40已取消）
      orderStatus: [
        // {
        //   id: "",
        //   status: "全部"
        // },
        {
          id: "Submitted",
          status: "已提交",
        },
        {
          id: "Confirmed",
          status: "已确认",
        },
        {
          id: "Completed",
          status: "已完成",
        },
        {
          id: "Canceled",
          status: "已取消",
        },
      ],
      //订单渠道（0全部 1C端 2门店）
      orderChannel: [
        // {
        //   id: "",
        //   name: "全部"
        // },
        {
          id: "ToC",
          name: "C端",
        },
        {
          id: "ToShop",
          name: "门店",
        },
        {
          id: "DianSale",
          name: "电销下单",
        },
        {
          id: "MeiTuan",
          name: "美团",
        },
        {
          id: "Dakehu",
          name: "大客户",
        },
      ],
      //安装服务状态（-1全部 0未安装 1已安装）
      installStatus: [
        // { id: "", name: "全部" },
        {
          id: "NotInstall",
          name: "未安装",
        },
        {
          id: "HaveInstall",
          name: "已安装",
        },
      ],
      //支付状态（-1全部 0未支付 1已支付）
      payStatus: [
        // { id: "", name: "全部" },
        {
          id: "NotPay",
          name: "未支付",
        },
        {
          id: "HavePay",
          name: "已支付",
        },
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
        { id: 11, name: "订单" },
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
      //订单类型（0全部 1轮胎 2保养 3美容）
      orderType: [
        // { id: "", name: "全部" },
        {
          id: "Tire",
          name: "轮胎安装",
        },
        {
          id: "BaoYang",
          name: "保养服务",
        },
        {
          id: "Wash",
          name: "美容洗车",
        },
        {
          id: "SheetMetal",
          name: "钣金喷漆",
        },
        {
          id: "CarModification",
          name: "维修改装",
        },
        {
          id: "Other",
          name: "其他",
        },
      ],
      exceptionOrder: [
        // { id: "", name: "全部" },
        {
          id: "Yes",
          name: "是",
        },
        {
          id: "No",
          name: "否",
        },
      ],
    };
  },
  created() {
    this.fetchData();
    this.v_detail = false;
  },
  methods: {
    showDetail(row) {
      this.detail.orderNo = row.orderNo;
      this.detail.orderStatus = row.orderStatus;
      this.detail.v_detail = true;
    },
    showPrint(row) {
      this.print.orderNo = row.orderNo;
      this.print.v_detail = true;
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
      if (data.createDate != null) {
        data.createStartTime =
          data.createDate.length > 1 ? data.createDate[0] : "";
        data.createEndTime =
          data.createDate.length > 1 ? data.createDate[1] : "";
      }
      if (data.installTime != null) {
        data.installStartTime =
          data.installTime.length > 1 ? data.installTime[0] : "";
        data.installEndTime =
          data.installTime.length > 1 ? data.installTime[1] : "";
      }
      console.log(data);

      this.tableLoading = true;
      orderCenterSvc
        .GetOrderInfoListForShop(data)
        .then(
          (res) => {
            let data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
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
    
    orderInstallReduceStock() {
      let data = JSON.parse(JSON.stringify(this.orderStockRequest));
      data.queueNo = this.detail.orderNo;
      orderCenterSvc
        .OrderRepeatReduceStock(data)
        .then(
              res => {
                if (res.code=="10000") {
                  this.$message({
                    message: "操作成功",
                    type: "success"
                  });
                }
              },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {
          console.error(err);
        });
    },
  },
};
</script>
