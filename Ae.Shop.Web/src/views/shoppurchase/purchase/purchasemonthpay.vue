<template>
  <main class="container">
    <!-- 首页：列表数据 -->
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :headerBtnWidth="160"
      :tableData="tableData"
      :pageChange="pageChange"
      :searching="search"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item label>
          <el-select
            v-model="condition.status"
            size="small"
            placeholder="请选择状态"
            style="width:150px"
            clearable
          >
            <el-option
              v-for="item in statusSel"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
          >新增</el-button
        >
      </template>
      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <!-- <el-table-column label="编号" prop="id" align="center" width="80px;"></el-table-column> -->
        <!-- <el-table-column label="门店名称" align="center" prop="locationName"></el-table-column> -->
        <el-table-column
          label="付款单号"
          prop="id"
          width="150"
        ></el-table-column>
        <el-table-column label="供应商" prop="venderName"></el-table-column>
        <el-table-column label="开户行" prop="openingBank"></el-table-column>
        <el-table-column label="银行名称" prop="bankName"></el-table-column>
        <el-table-column
          label="收款户名"
          prop="receiveBankName"
        ></el-table-column>
        <el-table-column label="账户" prop="accountNo"></el-table-column>
        <el-table-column label="付款金额" prop="amount"></el-table-column>
        <el-table-column label="状态" prop="status"></el-table-column>
        <el-table-column label="支付方式" prop="payMethod"></el-table-column>
        <el-table-column label="交易号" prop="serialNumber"></el-table-column>
        <el-table-column label="审核时间" prop="auditTime">
          <template slot-scope="scope">
            <label
              style="font-weight:50;"
              v-show="
                scope.row.auditTime != '1900-01-01 00:00:00' ? true : false
              "
              >{{ scope.row.auditTime }}</label
            >
          </template>
        </el-table-column>
        <el-table-column label="审核人" prop="auditUser"></el-table-column>

        <el-table-column label="付款时间" prop="payTime">
          <template slot-scope="scope">
            <label
              style="font-weight:50;"
              v-show="scope.row.payTime != '1900-01-01 00:00:00' ? true : false"
              >{{ scope.row.payTime }}</label
            >
          </template>
        </el-table-column>
        <el-table-column label="付款人" prop="payer"></el-table-column>
        <el-table-column label="取消时间" prop="cancleTime">
          <template slot-scope="scope">
            <label
              style="font-weight:50;"
              v-show="
                scope.row.cancleTime != '1900-01-01 00:00:00' ? true : false
              "
              >{{ scope.row.cancleTime }}</label
            >
          </template>
        </el-table-column>
        <el-table-column label="取消人" prop="cancleUser"></el-table-column>

        <!-- <el-table-column label="库存金额" align="center" prop="totalCost"></el-table-column> -->
        <el-table-column label="操作" fixed="right" align="center" width="180">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                type="primary"
                @click="auditPurchaseOrder(scope.row, 1)"
                size="mini"
                v-if="scope.row.status == '新建'"
                >审核</el-button
              >

              <el-button
                type="primary"
                @click="auditPurchaseOrder(scope.row, 2)"
                size="mini"
                v-if="
                  scope.row.status == '新建' || scope.row.status == '已审核'
                "
                >取消</el-button
              >

              <el-button
                type="primary"
                @click="payPurchaseOrder(scope.row)"
                size="mini"
                v-if="scope.row.status == '已审核'"
                >付款</el-button
              >

              <el-button
                type="primary"
                @click="viewTargetPurchaseOrders(scope.row)"
                size="mini"
                >采购清单</el-button
              >
            </el-button-group>
          </template>
        </el-table-column>
        <!-- Table数据 -->
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

    <!-- 填写发货信息页面 -->
    <section id="addPayInfo">
      <rg-dialog
        :title="payformTitle"
        :visible.sync="payformVisible"
        :beforeClose="paycancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            paycancel();
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="600px"
        minWidth="500px"
      >
        <template v-slot:content>
          <el-form :model="payformModel" ref="payformModel" :inline="true">
            <el-form-item label="金额">
              <el-input
                size="mini"
                v-model="payformModel.amount"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
            <el-form-item label="供应商名称">
              <el-input
                size="mini"
                v-model="payformModel.venderName"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
            <el-form-item label="开户行">
              <el-input
                size="mini"
                v-model="payformModel.openingBank"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
            <el-form-item label="银行名称">
              <el-input
                size="mini"
                v-model="payformModel.bankName"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>

            <el-form-item label="银行账户">
              <el-input
                size="mini"
                v-model="payformModel.accountNo"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
            <el-form-item label="收款户名">
              <el-input
                size="mini"
                v-model="payformModel.receiveBankName"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
            <el-form-item label="支付方式">
              <el-select
                v-model="payformModel.payMethod"
                filterable
                style="width:200px"
              >
                <el-option
                  v-for="item in payMethodList"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>

            <el-form-item label="交易参考号">
              <el-input
                size="mini"
                placeholder="请输入交易号"
                v-model="payformModel.serialNumber"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="savePayInfo()"
            >确定</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 填写发货信息页面 -->

    <!-- 创建月结单页面 -->
    <section id="createPayInfo">
      <rg-dialog
        :title="createpayformTitle"
        :visible.sync="createpayformVisible"
        :beforeClose="createpaycancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            createpaycancel();
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="1024px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-form
            :model="createpayformModel"
            ref="createpayformModel"
            :inline="true"
          >
            <el-form-item>
              <el-select
                v-model="selectShopId"
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
            <el-form-item label="供应商">
              <el-select
                v-model="selectVenderId"
                size="small"
                placeholder="请选择供应商"
                style="width:150px"
              
                clearable
              >
                <el-option
                  v-for="item in venderSel"
                  :key="item.id"
                  :label="item.venderShortName"
                  :value="item.id"
                ></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="开户行">
              <el-input
                size="mini"
                v-model="createpayformModel.openingBank"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
            <el-form-item label="收款户名">
              <el-input
                size="mini"
                v-model="createpayformModel.receiveBankName"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>

            <el-form-item label="银行名称">
              <el-input
                size="mini"
                v-model="createpayformModel.bank"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>

            <el-form-item label="银行账户">
              <el-input
                size="mini"
                v-model="createpayformModel.account"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
 <el-form-item label>
          <el-tooltip
            class="item"
            effect="dark"
            content="采购单创建时间"
            placement="bottom"
          >
            <el-date-picker
              v-model="times"
              type="daterange"
              :picker-options="$root.$data.timeRgPickerOpt"
              style="width:220px"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              align="right"
            ></el-date-picker>
          </el-tooltip>
        </el-form-item>

            <el-form-item>
               <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="getVenderOrders()"
            >查询</el-button
          >
            </el-form-item>
          </el-form>

          <el-table
            v-loading="tableLoading"
            :data="createpayformModel.purchaseOrders"
            ref="multipleTable"
            border
            style="width: 100%"
            @selection-change="handleSelectionChange"
          >
            <!-- <el-table-column type="expand">
              <template slot-scope="scope">
                <el-table border :data="scope.row.products" style="width: 100%">
                  <el-table-column
                    label="产品名称"
                    prop="productName"
                    align="center"
                  ></el-table-column>
                  <el-table-column
                    label="产品编号"
                    prop="productId"
                    align="center"
                    width="150px"
                  ></el-table-column>

                  <el-table-column
                    label="采购价"
                    prop="price"
                    align="center"
                    width="80px"
                  ></el-table-column>
                  <el-table-column
                    label="数量"
                    prop="num"
                    align="center"
                    width="80px"
                  ></el-table-column>
                </el-table>
              </template>
            </el-table-column> -->
            <el-table-column type="selection" width="55"> </el-table-column>
            <el-table-column label="门店名称" prop="shopName"></el-table-column>
            <el-table-column
              label="采购单号"
              prop="purchaseId"
            ></el-table-column>
            <el-table-column label="红冲单类型">
              <template slot-scope="scope">
                <label
                  style="background-color:red;color:white;"
                  v-if="scope.row.hcType == '红冲订单'"
                  >红冲订单,原单:{{ scope.row.relationPurchaseId }}</label
                >
                <label v-else></label>
              </template>
            </el-table-column>
            <el-table-column
              label="运单号"
              prop="deliveryCode"
            ></el-table-column>
            <el-table-column label="总金额" prop="amount"></el-table-column>
            <el-table-column
              label="创建时间"
              prop="createTime"
            ></el-table-column>
            <el-table-column label="总金额" prop="amount"></el-table-column>
            <el-table-column label="产品信息" prop="joinText"></el-table-column>
          </el-table>
          <label>合计:{{ sumAmount }}</label>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="savecreatePayInfo()"
            >确定</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 创建月结单页面 -->

    <!-- 创建月结单页面 -->
    <section id="vierOrderInfo">
      <rg-dialog
        :title="vierOrderformTitle"
        :visible.sync="viewOrderformVisible"
        :beforeClose="viewTargetOrderCancle"
        :btnCancel="{
          label: '关闭',
          click: done => {
            viewTargetOrderCancle();
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="800px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-table
            v-loading="tableLoading"
            :data="targetpurchaseOrders"
            border
            style="width: 100%"
          >
            <!-- <el-table-column type="expand">
              <template slot-scope="scope">
                <el-table border :data="scope.row.products" style="width: 100%">
                  <el-table-column
                    label="产品名称"
                    prop="productName"
                    align="center"
                  ></el-table-column>
                  <el-table-column
                    label="产品编号"
                    prop="productId"
                    align="center"
                    width="150px"
                  ></el-table-column>

                  <el-table-column
                    label="采购价"
                    prop="price"
                    align="center"
                    width="80px"
                  ></el-table-column>
                  <el-table-column
                    label="数量"
                    prop="num"
                    align="center"
                    width="80px"
                  ></el-table-column>
                </el-table>
              </template>
            </el-table-column> -->
            <el-table-column type="selection" width="55"> </el-table-column>
            <el-table-column label="门店名称" prop="shopName"></el-table-column>
            <el-table-column
              label="采购单号"
              prop="purchaseId"
            ></el-table-column>
              <el-table-column label="红冲单类型">
              <template slot-scope="scope">
                <label
                  style="background-color:red;color:white;"
                  v-if="scope.row.hcType == '红冲订单'"
                  >红冲订单,原单:{{ scope.row.relationPurchaseId }}</label
                >
                <label v-else></label>
              </template>
            </el-table-column>
            <el-table-column
              label="运单号"
              prop="deliveryCode"
            ></el-table-column>
            <el-table-column label="总金额" prop="amount"></el-table-column>
            <el-table-column
              label="创建时间"
              prop="createTime"
            ></el-table-column>
            <el-table-column label="产品信息" prop="joinText"></el-table-column>
          </el-table>
          <label>合计:{{ sumAmount }}</label>
        </template>
        <!-- <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="savecreatePayInfo()"
            >确定</el-button
          >
        </template> -->
      </rg-dialog>
    </section>
    <!-- 创建月结单页面 -->
  </main>
</template>

<script>
import { shoppurchaseManageSvc } from "@/api/shoppurchase/shoppurchasemanage";

export default {
  name: "demopage",
  data() {
    return {
      w_search_right: 100,
      tableLoading: false,

      condition: {
        pageIndex: 1,
        pageSize: 10,
        status: undefined //默认是true
      },
      detailArr: [],
      tableData: [],
      totalList: 0,
      sumAmount: 0,
times:[],
      stockformTitle: undefined,
      stockformVisible: false,
      stockTableData: [],
      stockCondition: {
        productId: undefined
      },
      stockFlowformTitle: undefined,
      stockFlowformVisible: false,
      stockFlowTableData: [],
      stockFlowCondition: {
        productId: undefined,
        batchNo: undefined
      },
      auditPurchaseOrderModel: {
        id: undefined,
        type: undefined
      },
      venderSel: [],
      multipleSelection: [],

      payformModel: {
        id: undefined,
        amount: undefined,
        venderName: undefined,
        bankName: undefined,
        accountNo: undefined,
        payMethod: undefined,
        serialNumber: undefined,
        openingBank: undefined,
        receiveBankName: undefined
      },
      payformModelInit: {
        id: undefined,
        amount: undefined,
        venderName: undefined,
        bankName: undefined,
        accountNo: undefined,
        payMethod: undefined,
        serialNumber: undefined,
        openingBank: undefined,
        receiveBankName: undefined
      },

      createpayformTitle: undefined,
      createpayformVisible: false,
      selectVenderId: undefined,
      selectShopId: undefined,
      createpayformModel: {
        bank: undefined,
        account: undefined,
        openingBank: undefined,
        receiveBankName: undefined,
        purchaseOrders: []
      },
      createpayformModelInit: {
        bank: undefined,
        account: undefined,
        openingBank: undefined,
        receiveBankName: undefined,
        purchaseOrders: []
      },

      getPayOrderModel: {
        venderId: undefined,
        shopId: undefined,
        times:undefined
      },
      createPayTemp: {
        venderName: undefined,
        bankName: undefined,
        openingBank: undefined,
        accountNo: undefined,
        amount: undefined,
        venderId: undefined,
        receiveBankName: undefined,
        purchaseIds: []
      },
      viewOrderformVisible: false,
      vierOrderformTitle: undefined,
      targetpurchaseOrders: [],
      payformVisible: false,
      payformTitle: undefined,
      payMethodList: [
        {
          value: "支付宝",
          label: "支付宝"
        },
        {
          value: "微信",
          label: "微信"
        },
        {
          value: "银联",
          label: "银联"
        }
      ],

      statusSel: [
        {
          value: "新建",
          label: "新建"
        },
        {
          value: "已取消",
          label: "已取消"
        },
        {
          value: "已审核",
          label: "已审核"
        },
        {
          value: "已付款",
          label: "已付款"
        }
      ],
      
      shopSelCondition: {
        simpleName: undefined
      },
      loading: false,
      shopSel: [],
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    viewTargetPurchaseOrders(row) {
      this.vierOrderformTitle = "采购单列表";
      this.viewOrderformVisible = true;
      this.auditPurchaseOrderModel.id = row.id;
      shoppurchaseManageSvc
        .getTargetPurchaseOrders(this.auditPurchaseOrderModel)
        .then(
          res => {
            //获取门店指定的供应商
            this.targetpurchaseOrders = res.data;
            debugger;
            //计算金额
            if (
              this.targetpurchaseOrders != null &&
              this.targetpurchaseOrders.length > 0
            ) {
              let sum = 0;

              this.targetpurchaseOrders.forEach(r => {
                sum += r.amount;
              });
              this.sumAmount = sum;
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    viewTargetOrderCancle() {
      this.viewOrderformVisible = false;
      this.targetpurchaseOrders = [];
    },

    handleSelectionChange(val) {
      this.multipleSelection = val;
      let sum = 0;

      this.multipleSelection.forEach(r => {
        sum += r.amount;
      });
      this.sumAmount = sum;
    },
    createpaycancel() {
      this.createpayformVisible = false;

      this.createpayformModel = JSON.parse(
        JSON.stringify(this.createpayformModelInit)
      );
      this.venderSel = [];
      this.selectVenderId = undefined;
      this.selectShopId = undefined;
    },

    add() {
      this.createpayformVisible = true;
      this.createpayformTitle = "创建采购月结单";
      //获取这个门店的供应商

      shoppurchaseManageSvc
        .getTargetVenders(this.payformModel)
        .then(
          res => {
            //获取门店指定的供应商
            this.venderSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    
    getShopinfo(query) {
      if (query != "") {
        debugger;
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          debugger;
          this.shopSelCondition.simpleName = query;
          shoppurchaseManageSvc
            .getShopListAsync(this.shopSelCondition)
            .then(
              res => {
                this.shopSel = res.data.items;
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

    getVenderOrders() {
      debugger;
      this.getPayOrderModel.times = this.times;
      this.getPayOrderModel.venderId = this.selectVenderId;
      this.getPayOrderModel.shopId = this.selectShopId;
      shoppurchaseManageSvc
        .getVenderPurchaseOrders(this.getPayOrderModel)
        .then(
          res => {
            if (res.data != null) {
              this.createpayformModel = res.data;
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    savecreatePayInfo() {
      if (this.multipleSelection.length > 0) {
        this.$confirm("确定操作吗?", "信息", {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning"
        })
          .then(() => {
            //创建月结单

            this.createPayTemp.venderId = this.selectVenderId;
            this.createPayTemp.bankName = this.createpayformModel.bank;
            this.createPayTemp.accountNo = this.createpayformModel.account;
            this.createPayTemp.receiveBankName = this.createpayformModel.receiveBankName;
            this.createPayTemp.openingBank = this.createpayformModel.openingBank;

            this.createPayTemp.amount = this.sumAmount;
            var venderName = "";
            for (var i = 0; i < this.venderSel.length; i++) {
              if (this.venderSel[i].id == this.selectVenderId) {
                venderName = this.venderSel[i].venderShortName;
                break;
              }
            }
            this.createPayTemp.venderName = venderName;

            this.multipleSelection.forEach(r => {
              this.createPayTemp.purchaseIds.push(r.purchaseId);
            });

            shoppurchaseManageSvc
              .upsertPurchaseMonthPayInfo(this.createPayTemp)
              .then(
                res => {
                  this.$message({
                    message: res.message || "操作成功",
                    type: "success"
                  });
                  this.search();
                  this.createpaycancel();
                },
                error => {
                  console.log(error);
                }
              )
              .catch(() => {});
          })
          .catch(error => {
            console.log(error);
          });
      } else {
        this.$message.warning("请选择采购单!");
        return false;
      }
    },

    savePayInfo() {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          // this.payformModel.id = this.purchaseForm.id;
          //保存发货信息
          shoppurchaseManageSvc
            .savePurchaseMonthPay(this.payformModel)
            .then(
              res => {
                this.$message({
                  message: res.message || "操作成功",
                  type: "success"
                });
                //刷新页面
                this.isRefresh = true;
                this.paycancel();
                this.search();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(error => {
          console.log(error);
        });
    },

    payPurchaseOrder(row) {
      this.payformTitle = "付款信息";
      this.payformVisible = true;
      this.payformModel.id = row.id;
      this.payformModel.amount = row.amount;
      this.payformModel.venderName = row.venderName;
      this.payformModel.bankName = row.bankName;
      this.payformModel.accountNo = row.accountNo;
      this.payformModel.openingBank = row.openingBank;
    },
    paycancel() {
      this.payformVisible = false;
      this.payformModel = JSON.parse(JSON.stringify(this.payformModelInit));
    },

    auditPurchaseOrder(row, type) {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.auditPurchaseOrderModel.id = row.id;
          this.auditPurchaseOrderModel.type = type;
          //保存发货信息
          shoppurchaseManageSvc
            .auditPurchaseMonthPay(this.auditPurchaseOrderModel)
            .then(
              res => {
                this.$message({
                  message: res.message || "操作成功",
                  type: "success"
                });
                //刷新页面
                this.search();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(error => {
          console.log(error);
        });
    },

    fetchData() {
      shoppurchaseManageSvc
        .selectPurchaseMonthPayInfos(this.condition)
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
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    // pageTurning() {
    //   this.condition.pageIndex = this.currentPage;
    //   this.getPaged();
    // },

    getPaged(flag) {
      debugger;
      this.tableLoading = true;

      console.log("condition: " + JSON.stringify(this.condition));
      shoppurchaseManageSvc
        .selectPurchaseMonthPayInfos(this.condition)
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

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 48px;
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
