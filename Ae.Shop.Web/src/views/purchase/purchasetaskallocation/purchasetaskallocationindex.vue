<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <header class="bodyTop">
        <article class="filter-container"></article>
      </header>
      <main>
        <section>
          <el-tabs type="border-card" v-model="activeName" @tab-click="handleClick">
            <el-tab-pane label="保养" name="baoyang">
              <el-table
                border
                :data="tableData"
                @row-click="clickRow"
                :default-expand-all="isExpand"
                style="width: 100%"
              >
                <el-table-column type="expand">
                  <template slot-scope="props">
                    <el-table
                      border
                      :data="props.row.products"
                      :cell-style="cellStyle"
                      style="width: 100%"
                    >
                      <el-table-column label="订单子单号" prop="orderProductId"></el-table-column>
                      <el-table-column label="产品名称" prop="productName"></el-table-column>
                      <el-table-column label="产品编号" prop="productId"></el-table-column>
                      <el-table-column label="品牌" prop="brand"></el-table-column>
                      <el-table-column label="数量" prop="num"></el-table-column>
                      <el-table-column label="任务状态" prop="taskStatus"></el-table-column>
                      <el-table-column label="主人" prop="taskOwner"></el-table-column>
                      <el-table-column label="分配时间" prop="allocationTime">
                        <template slot-scope="scope">
                          <label
                            style="font-weight:50;"
                            v-show="scope.row.allocationTime!='1900-01-01 00:00:00'?true:false"
                          >{{scope.row.allocationTime}}</label>
                        </template>
                      </el-table-column>
                      <el-table-column label="创建人" prop="createBy"></el-table-column>
                      <el-table-column label="创建时间" prop="createTime"></el-table-column>
                      <el-table-column label="备注" prop="remark"></el-table-column>

                      <el-table-column label="操作">
                        <template slot-scope="scope">
                          <div v-if="scope.row.taskStatus == '新建'">
                            <el-button type="text" size="mini" @click="beginPurchase(scope.row)">待采购</el-button>
                          </div>
                          <div v-else-if="scope.row.taskStatus == '已分配'">
                            <el-button
                              type="text"
                              size="mini"
                              @click="createPurchaseOrder(scope.row)"
                            >采购中</el-button>
                          </div>
                          <div v-else-if="scope.row.taskStatus == '已完成'">
                            <el-button type="text" disabled>已采购</el-button>
                          </div>
                        </template>
                      </el-table-column>
                    </el-table>
                  </template>
                </el-table-column>
                <el-table-column label="订单号" prop="orderNo"></el-table-column>
                <el-table-column label="订单类型" prop="orderType"></el-table-column>
                <el-table-column label="订单渠道" prop="orderChannel"></el-table-column>
                <el-table-column label="仓库" prop="warehouseName"></el-table-column>

                <el-table-column label="用户名" prop="userName"></el-table-column>
                <el-table-column label="区域" prop="regionName"></el-table-column>
                <el-table-column label="下单时间" prop="orderDatetime"></el-table-column>
                <el-table-column label="操作">
                  <template slot-scope="scope">
                    <!-- 暂时不做 需要对总单下的多个子单做状态判断 比较复杂  -->
                    <el-button
                      size="mini"
                      :disabled="scope.row.isSwtich ==0?true:false"
                      type="text"
                      @click="switchWarehouse(scope.row)"
                    >转仓</el-button>
                    <el-button
                      size="mini"
                      :disabled="scope.row.isSwtich ==0?true:false"
                      type="text"
                      @click="viewSwitchHistorys(scope.row)"
                    >转仓历史</el-button>
                  </template>
                </el-table-column>
              </el-table>
            </el-tab-pane>
            <el-tab-pane label="轮胎/轮毂" name="tire">保养</el-tab-pane>
            <el-tab-pane label="车品" name="chepin">车品</el-tab-pane>
          </el-tabs>
        </section>
      </main>
    </section>
    <!-- 首页 -->

    <!-- 审核驳回采购任务 -->
    <section id="beginPurchaseOrder">
      <el-dialog
        width="500px"
        :title="beginPurchaseOrderFormTitle"
        :close-on-click-modal="false"
        :visible.sync="beginPurchaseOrderFormVisible"
        :before-close="beginPurchaseOrderCancel"
      >
        <el-form :model="deleteFormModel" ref="deleteFormModel">
          <el-form-item label="理由:" size="50">
            <el-input
              type="textarea"
              :rows="5"
              placeholder="请输入理由"
              v-model="deleteFormModel.remark"
            ></el-input>
          </el-form-item>
        </el-form>

        <div slot="footer" class="dialog-footer">
          <el-button @click="beginPurchaseOrderCancel()">关闭</el-button>
          <el-button type="warning" @click="beginPurchaseOrderSave('reject')">驳回</el-button>
          <el-button type="primary" @click="beginPurchaseOrderSave('pass')">通过</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 审核驳回采购任务 -->

    <!-- 新增和编辑页面 -->
    <section id="createOrUpdate">
      <el-dialog
        :title="formTitle"
        :close-on-click-modal="false"
        :visible.sync="formVisible"
        :before-close="cancel"
      >
        <el-form :model="formModel" :inline="true" :rules="rules" ref="formModel">
          <el-form-item label="目标仓库" prop="warehouseName">
            <el-select
              v-model="formModel.warehouseName"
              @change="changeWarehouse"
              size="small"
              clearable
              filterable
              placeholder="请选择目标仓"
            >
              <el-option
                v-for="item in warehouseSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="供应商" prop="venderShortName">
            <el-select
              v-model="formModel.venderShortName"
              @change="changeVender"
              size="small"
              clearable
              filterable
              placeholder="请选择供应商"
            >
              <el-option
                v-for="item in venderSel"
                :key="item.id"
                :label="item.venderShortName"
                :value="item.id"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="产品编号:" prop="productId">
            <el-input v-model="formModel.productId" placeholder="请输入产品编号" />
          </el-form-item>
          <el-form-item label="产品名称:">
            <el-input
              v-model="formModel.productName"
              style="width:500px;"
              :disabled="true"
              placeholder="请输入产品名称"
            />
          </el-form-item>
          <el-form-item label="预计到货时间:" prop="planInstockDate">
            <el-col>
              <el-date-picker
                :picker-options="pickerOptionsStart"
                value-format="yyyy-MM-dd"
                v-model="formModel.planInstockDate"
                type="date"
                placeholder="请选择预计到货时间"
                style="width: 100%;"
              />
            </el-col>
          </el-form-item>

          <el-form-item label="采购数量:" prop="num">
            <el-input
              oninput="value=value.replace(/[^\d.]/g,'')"
              v-model.number="formModel.num"
              placeholder="请输入采购数量"
              @change="priceChange()"
            />
          </el-form-item>

          <el-form-item label="采购单价:" prop="purchasePrice">
            <el-input
              oninput="value=value.replace(/[^\d.]/g,'')"
              v-model="formModel.purchasePrice"
              placeholder="请输入采购单价"
              @change="priceChange()"
            />
          </el-form-item>

          <el-form-item label="采购总价:">
            <el-input readonly v-model="formModel.totalPrice" />
          </el-form-item>
          <el-form-item label="返利:">
            <el-input
              oninput="value=value.replace(/[^\d.]/g,'')"
              placeholder="请输入返利"
              v-model="formModel.rebate1"
              @change="priceChange()"
            />
          </el-form-item>

          <el-form-item label="成本单价:">
            <el-input readonly v-model="formModel.costPrice" />
          </el-form-item>

          <el-form-item label="总成本:">
            <el-input readonly v-model="formModel.totalCost" />
          </el-form-item>

          <el-form-item label="提货方式" prop="shipmentType">
            <el-select
              v-model="formModel.shipmentType"
              size="small"
              clearable
              filterable
              placeholder="请选择提货方式"
              class="margin-right-10"
            >
              <el-option
                v-for="item in shipmentTypeSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <!-- 账期 -->
          <el-form-item label="账期" prop="accountPeriod">
            <el-select
              v-model="formModel.accountPeriod"
              size="small"
              clearable
              filterable
              placeholder="请选择账期"
              class="margin-right-10"
            >
              <el-option
                v-for="item in accountPeriodSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="税点:">
            <el-input v-model="formModel.taxPoint" readonly />
          </el-form-item>
          <br />
          <el-form-item label="有发票">
            <el-switch v-model="formModel.isInvoice" />
          </el-form-item>&nbsp;&nbsp;&nbsp;&nbsp;
          <el-form-item label="备注:" size>
            <el-input
              placeholder="请输入备注"
              type="textarea"
              style="width:300px;height:120px;"
              v-model="formModel.remark"
            />
          </el-form-item>
        </el-form>
        <!-- 展示订单和采购单 -->

        <div slot="footer" class="dialog-footer">
          <el-button @click="cancel('formModel')">取消</el-button>
          <el-button type="primary" @click="save('formModel')">保存</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 新增和编辑页面 -->

    <!-- 转仓页面 -->
    <section id="switchWarehouse">
      <el-dialog
        width="500px"
        :title="switchWarehouseFormTitle"
        :close-on-click-modal="false"
        :visible.sync="switchWarehouseFormVisible"
        :before-close="switchWarehouseCancel"
      >
        <el-form :model="switchWarehouseFormModel" ref="switchWarehouseFormModel">
          <el-form-item label="目标仓库" prop="warehouseName">
            <el-select
              v-model="switchWarehouseFormModel.newWarehouseId"
              size="small"
              clearable
              filterable
              @change="switchWarehouseOptions"
              placeholder="请选择目标仓"
            >
              <el-option-group
                v-for="group in warehouseoptions"
                :key="group.key"
                :label="group.key"
              >
                <el-option
                  v-for="item in group.basicInfoList"
                  :key="item.key"
                  :label="item.value"
                  :value="item.key"
                ></el-option>
              </el-option-group>
            </el-select>
          </el-form-item>

          <el-form-item label="备注:" size="50">
            <el-input
              type="textarea"
              :rows="5"
              placeholder="请输入备注"
              v-model="switchWarehouseFormModel.remark"
            ></el-input>
          </el-form-item>
        </el-form>

        <div slot="footer" class="dialog-footer">
          <el-button @click="switchWarehouseCancel()">关闭</el-button>
          <el-button type="warning" @click="switchWarehouseSave()">转仓</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 转仓页面 -->

    <!-- 转仓操作历史 -->
    <section id="viewswitchWarehouse">
      <el-dialog
        :title="viewswitchWarehouseFormTitle"
        :close-on-click-modal="false"
        :visible.sync="viewswitchWarehouseFormVisible"
        :before-close="viewswitchWarehouseCancel"
      >
        <el-table border :data="switchHistoryTable" style="width: 100%">
          <el-table-column label="源仓" prop="orginWarehouseName"></el-table-column>
          <el-table-column label="目标仓" prop="newWarehouseName"></el-table-column>
          <el-table-column label="备注" prop="remark"></el-table-column>
          <el-table-column label="转仓时间" prop="createTime"></el-table-column>
        </el-table>

        <div slot="footer" class="dialog-footer">
          <el-button @click="viewswitchWarehouseCancel()">关闭</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 转仓操作历史 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appSvc } from "@/api/purchase/purchasetaskallocation";
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
      activeName: "baoyang",
      shipmentTypeSel: [],
      isExpand: false, //是否展开所有的子元素
      accountPeriodSel: [],
      warehouseSel: [],
      venderSel: [],

      objectModel: {},

      shipmentTypeSelCondition: {
        RequestType: 3
      },

      accountPeriodCondition: {
        RequestType: 6
      },
      warehouseSelCondition: {
        RequestType: 5
      },
      warehouseoptions: [],
      //table页面的条件
      condition: {
        id: undefined,
        orderNo: undefined,
        taskType: undefined,
        taskStatus: undefined
      },

      tableData: [],
      totalList: 0,
      formLabelWidth: "120px",
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",

      beginPurchaseOrderFormTitle: "",
      beginPurchaseOrderFormVisible: false,

      //转仓配置
      switchWarehouseFormTitle: undefined,
      switchWarehouseFormVisible: false,
      switchWarehouseFormModel: {
        orginWarehouseId: undefined,
        orginWarehouseName: undefined,
        newWarehouseId: undefined,
        newWarehouseName: undefined,
        switchType: undefined,
        orderNo: undefined,
        remark: undefined,
        targetRemark: undefined,
        isCount: undefined
      },
      switchWarehouseFormModelInit: {
        orginWarehouseId: undefined,
        orginWarehouseName: undefined,
        newWarehouseId: undefined,
        newWarehouseName: undefined,
        switchType: undefined,
        orderNo: undefined,
        remark: undefined,
        targetRemark: undefined,
        isCount: undefined
      },

      deleteFormModel: {
        id: undefined,
        remark: undefined,
        updateType: undefined,
        taskStatus: undefined
      },
      createPurchaseOrderCondition: { id: undefined },
      switchHistoryTable: [],

      viewswitchWarehouseFormTitle: undefined,
      viewswitchWarehouseFormVisible: false,
      viewSwitchHistoryCondition: {
        orderNo: undefined
      },
      deleteFormModelInit: {
        id: undefined,
        remark: undefined,
        updateType: undefined,
        taskStatus: undefined
      },

      switchWarehouseCondition: { addressId: undefined },
      formInit: {
        operateType: "unBase",
        id: undefined,
        poId: undefined,
        warehouseId: "",
        warehouseName: "",
        venderId: "",
        venderShortName: "",
        productId: "",
        productName: "",
        planInstockDate: "",
        isInvoice: 0,
        purchasePrice: "",
        num: "",
        costPrice: "",
        totalCost: "",
        rebate1: 0.0,
        shipmentType: "",
        accountPeriod: "",
        taxPoint: 1,
        remark: "",
        totalPrice: "",
        orderNo: undefined,
        orderProductId: undefined
      },
      //新增采购单model
      formModel: {
        operateType: "unBase",
        id: undefined, //赋值采购任务单号

        poId: undefined,
        warehouseId: "",
        warehouseName: "",
        venderId: "",
        venderShortName: "",
        productId: "",
        productName: "",
        planInstockDate: "",
        isInvoice: 0,
        purchasePrice: "",
        num: "",
        costPrice: "",
        totalCost: "",
        rebate1: 0.0,
        shipmentType: "",
        accountPeriod: "",
        taxPoint: 1,
        remark: "",
        totalPrice: "",
        orderNo: undefined,
        orderProductId: undefined
      },
      rules: {
        venderShortName: [
          {
            required: true,
            message: "请选择供应商",
            trigger: "blur"
          }
        ],
        warehouseName: [
          {
            required: true,
            message: "请选择目标仓",
            trigger: "blur"
          }
        ],

        productId: [
          {
            required: true,
            message: "请输入产品编号",
            trigger: "blur"
          }
        ],

        planInstockDate: [
          {
            required: true,
            message: "请选择预计到货时间",
            trigger: "blur"
          }
        ],
        num: [
          {
            required: true,
            message: "请输入采购数量",
            trigger: "blur"
          }
        ],
        purchasePrice: [
          {
            required: true,
            message: "请输入采购价",
            trigger: "blur"
          }
        ],
        shipmentType: [
          {
            required: true,
            message: "请选择提货方式",
            trigger: "blur"
          }
        ],
        accountPeriod: [
          {
            required: true,
            message: "请选择账期",
            trigger: "blur"
          }
        ]
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
    //页面初始化函数
    this.fetchData();
  },
  methods: {
    getRowClassName({ row, rowIndex }) {
      return "expanded";
    },

    viewswitchWarehouseCancel() {
      this.viewswitchWarehouseFormVisible = false;
      this.viewSwitchHistoryCondition.orderNo = undefined;
      this.switchHistoryTable = [];
    },
    viewSwitchHistorys(row) {
      this.viewswitchWarehouseFormVisible = true;
      this.viewswitchWarehouseFormTitle = "订单【" + row.orderNo + "】转仓历史";
      this.viewSwitchHistoryCondition.orderNo = row.orderNo;
      appSvc
        .getSwitchWarehouseHistorys(this.viewSwitchHistoryCondition)
        .then(
          res => {
            this.switchHistoryTable = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    switchWarehouseCancel() {
      this.switchWarehouseFormVisible = false;
      // this.resetSendForm();
      this.switchWarehouseFormModel = JSON.parse(
        JSON.stringify(this.switchWarehouseFormModelInit)
      );
      this.warehouseoptions = [];
    },
    switchWarehouseSave() {
      if (
        this.switchWarehouseFormModel.newWarehouseId == "" ||
        this.switchWarehouseFormModel.newWarehouseId == undefined
      ) {
        this.$message({
          message: "请选择目标仓库!",
          type: "warning"
        });
        return;
      }

      if (
        this.switchWarehouseFormModel.orginWarehouseId ==
        this.switchWarehouseFormModel.newWarehouseId
      ) {
        this.$message({
          message: "目标仓和源仓一致!",
          type: "warning"
        });
        return;
      }

      if (
        this.switchWarehouseFormModel.remark != "" &&
        this.switchWarehouseFormModel.remark != undefined
      ) {
        this.switchWarehouseFormModel.remark =
          this.switchWarehouseFormModel.remark +
          " " +
          this.switchWarehouseFormModel.targetRemark;
      } else {
        this.switchWarehouseFormModel.remark =
          " " + this.switchWarehouseFormModel.targetRemark;
      }

      appSvc
        .createSwtichWarehouseApply(this.switchWarehouseFormModel)
        .then(
          res => {
            if (res.code == 10000) {
              this.$message({
                message: res.message,
                type: "success"
              });
              this.switchWarehouseCancel();
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    switchWarehouse(row) {
      this.switchWarehouseFormTitle = "订单转仓";
      this.switchWarehouseFormVisible = true;
      this.switchWarehouseFormModel.orginWarehouseId = row.warehouseId;
      this.switchWarehouseFormModel.orginWarehouseName = row.warehouseName;
      this.switchWarehouseFormModel.orderNo = row.orderNo;
      this.switchWarehouseFormModel.switchType = "手动";

      this.switchWarehouseCondition.addressId = row.regionName.split("-")[0];
      appSvc
        .selectDefaultWarehouseConfigs(this.switchWarehouseCondition)
        .then(
          res => {
            this.warehouseoptions = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    switchWarehouseOptions(val) {
      if (val == this.switchWarehouseFormModel.orginWarehouseId) {
        this.$message({
          message: "目标仓和源仓一致!",
          type: "warning"
        });
        this.switchWarehouseFormModel.newWarehouseId = undefined;
        return;
      } else {
        //判断是转的备选仓/其他仓
        var remark = "";
        for (var i = 0; i < this.warehouseoptions.length; i++) {
          if (this.warehouseoptions[i].key == "默认仓/备选仓") {
            if (this.warehouseoptions[i].basicInfoList.length > 0) {
              var basicList = this.warehouseoptions[i].basicInfoList;
              for (var j = 0; j < basicList.length; j++) {
                if (basicList[j].key == val) {
                  remark =
                    "在默认/备选仓中转仓,源仓:【" +
                    this.switchWarehouseFormModel.orginWarehouseName +
                    "】,目标仓:【" +
                    basicList[j].value +
                    "】";
                  this.switchWarehouseFormModel.newWarehouseId = val;
                  this.switchWarehouseFormModel.newWarehouseName =
                    basicList[j].value;
                  this.switchWarehouseFormModel.isCount = 0;
                  break;
                }
              }
            }
          } else if (this.warehouseoptions[i].key == "其他仓") {
            if (this.warehouseoptions[i].basicInfoList.length > 0) {
              var basicList = this.warehouseoptions[i].basicInfoList;
              for (var j = 0; j < basicList.length; j++) {
                if (basicList[j].key == val) {
                  remark =
                    "在其他仓中转仓,源仓:【" +
                    this.switchWarehouseFormModel.orginWarehouseName +
                    "】,目标仓:【" +
                    basicList[j].value +
                    "】";
                  this.switchWarehouseFormModel.newWarehouseId = val;
                  this.switchWarehouseFormModel.newWarehouseName =
                    basicList[j].value;
                  this.switchWarehouseFormModel.isCount = 1;
                  break;
                }
              }
            }
          }
        }
        this.switchWarehouseFormModel.targetRemark = remark;
      }
    },
    cancel(formName) {
      this.formVisible = false;
      // this.resetSendForm();
      this.formModel = JSON.parse(JSON.stringify(this.formInit));
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();

      this.search();
    },
    save(formName) {
      var vm = this;
      console.log("formModel: " + JSON.stringify(this.formModel));
      if (this.formModel.num <= 0) {
        this.$message({
          message: "采购数量必须大于0",
          type: "warning"
        });
        return;
      }
      if (this.formModel.purchasePrice <= 0) {
        this.$message({
          message: "采购价必须大于0",
          type: "warning"
        });
        return;
      }
      this.$refs[formName].validate(valid => {
        if (valid) {
          appSvc
            .createPurchaseOrder(this.formModel)
            .then(res => {
              if (res.code == 10000) {
                this.$message({
                  message: res.message,
                  type: "success"
                });
                vm.search();
                this.formVisible = false;
                this.formModel = JSON.parse(JSON.stringify(this.formInit));

                vm.cancel();
              }
            })
            .catch(error => {
              console.log(error);
            });
        } else {
          return false;
        }
      });
    },
    changeVender(val) {
      //locations是v-for里面的也是datas里面的值
      // let obj = {};
      // obj = this.venderSel.find(item => {
      //   return item.value === val;
      // });

      var venderName = "";
      for (var i = 0; i < this.venderSel.length; i++) {
        if (this.venderSel[i].id == Number(val))
          venderName = this.venderSel[i].venderShortName;
      }
      this.formModel.venderShortName = venderName;
      this.formModel.venderId = Number(val);
    },

    beginPurchaseOrderSave(type) {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          if (type == "reject") {
            if (
              this.deleteFormModel.remark == "" ||
              this.deleteFormModel.remark == undefined
            ) {
              this.$message({
                message: "请填写驳回理由!",
                type: "warning"
              });
              return;
            }

            this.deleteFormModel.taskStatus = "已取消";
            this.deleteFormModel.updateType = 3;

            appSvc
              .updatePurchaseTaskAllocation(this.deleteFormModel)
              .then(
                res => {
                  if (res.code == 10000) {
                    this.$message({
                      message: res.message,
                      type: "success"
                    });
                  }

                  this.beginPurchaseOrderFormVisible = false;
                  this.deleteFormModel = JSON.parse(
                    JSON.stringify(this.deleteFormModelInit)
                  );
                  this.objectModel = undefined;
                  vm.search();
                },
                error => {
                  console.log(error);
                }
              )
              .catch(() => {});
          } else if (type == "pass") {
            this.deleteFormModel.taskStatus = "已分配";
            this.deleteFormModel.updateType = 1;
            appSvc
              .updatePurchaseTaskAllocation(this.deleteFormModel)
              .then(
                res => {
                  if (res.code == 10000) {
                    this.$message({
                      message: res.message,
                      type: "success"
                    });
                  }
                  this.formModel.id = this.deleteFormModel.id;
                  this.beginPurchaseOrderFormVisible = false;
                  this.deleteFormModel = JSON.parse(
                    JSON.stringify(this.deleteFormModelInit)
                  );

                  this.createPurchaseOrder();
                },
                error => {
                  console.log(error);
                }
              )
              .catch(() => {});
          }
        })
        .catch(() => {});
    },
    beginPurchaseOrderCancel() {
      this.beginPurchaseOrderFormVisible = false;
      this.deleteFormModel = JSON.parse(
        JSON.stringify(this.deleteFormModelInit)
      );
      this.objectModel = undefined;
    },

    //获取供应商信息
    getVenderData() {
      appSvc
        .getVenders()
        .then(
          res => {
            this.venderSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //获取提货方式
    getShipmentType() {
      appSvc
        .getBasicInfoList(this.shipmentTypeSelCondition)
        .then(
          res => {
            this.shipmentTypeSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //获取账期
    getAccountPeriod() {
      appSvc
        .getBasicInfoList(this.accountPeriodCondition)
        .then(
          res => {
            this.accountPeriodSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    priceChange() {
      if (this.formModel.purchasePrice < 0) {
        this.formModel.purchasePrice = 0;
      }

      if (this.formModel.num < 0) {
        this.formModel.num = 0;
      }
      if (this.formModel.rebate1 < 0) {
        this.formModel.rebate1 = 0;
      }
      //计算成本价
      if (this.formModel.taxPoint > 0) {
        this.formModel.costPrice = (
          this.formModel.purchasePrice * this.formModel.taxPoint -
          this.formModel.rebate1
        ).toFixed(2);
      } else {
        this.formModel.costPrice = (
          this.formModel.purchasePrice - this.formModel.rebate1
        ).toFixed(2);
      }

      this.formModel.totalPrice = (
        this.formModel.purchasePrice *
        this.formModel.num *
        this.formModel.taxPoint
      ).toFixed(2);
      this.formModel.totalCost = (
        this.formModel.costPrice * this.formModel.num
      ).toFixed(2);
    },
    changeWarehouse(val) {
      //locations是v-for里面的也是datas里面的值
      let obj = {};
      var warehouseName = "";
      for (var i = 0; i < this.warehouseSel.length; i++) {
        if (this.warehouseSel[i].key == Number(val))
          warehouseName = this.warehouseSel[i].value;
      }

      // obj = this.warehouseSel.find(item => {
      //   return item.key === Number(val);
      // });
      this.formModel.warehouseName = warehouseName;
      this.formModel.warehouseId = Number(val);
    },

    getwarehouses() {
      appSvc
        .getBasicInfoList(this.warehouseSelCondition)
        .then(
          res => {
            this.warehouseSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //后期是有专门的Job分配的
    beginPurchase(row) {
      this.beginPurchaseOrderFormTitle = "审核采购任务";
      this.beginPurchaseOrderFormVisible = true;

      this.deleteFormModel.id = row.id;

      this.objectModel = row;

      //   this.$confirm("确定采购吗！", "警告", {
      //     confirmButtonText: "确定",
      //     cancelButtonText: "取消",
      //     type: "warning"
      //   })
      //     .then(() => {
      //       //更新采购任务状态

      //       appSvc
      //         .getVenders()
      //         .then(
      //           res => {
      //             this.venderSel = res.data;
      //           },
      //           error => {
      //             console.log(error);
      //           }
      //         )
      //         .catch(() => {});

      //       this.formVisible = true;
      //       this.formTitle = "创建采购单";
      //       this.getVenderData();
      //       this.getwarehouses();
      //       this.getShipmentType();
      //       this.getAccountPeriod();
      //     })
      //     .catch(() => {});
    },
    createPurchaseOrder(row) {
      this.formVisible = true;
      this.formTitle = "创建采购单";
      this.getVenderData();
      this.getwarehouses();
      this.getShipmentType();
      this.getAccountPeriod();
      if (row != null && row != undefined) {
        this.formModel.id = row.id;
      }
      this.createPurchaseOrderCondition.id = this.formModel.id;
      appSvc
        .getPurchaseTaskAllocationDetail(this.createPurchaseOrderCondition)
        .then(
          res => {
            var result = res.data;
            this.formModel.warehouseId = result.warehouseId;
            this.formModel.warehouseName = result.warehouseName;
            this.formModel.productId = result.productId;
            this.formModel.productName = result.productName;
            this.formModel.num = result.num;
            this.formModel.remark = result.remark;
            this.formModel.taxPoint = 1;
            this.formModel.orderNo = row.orderNo;
            this.formModel.orderProductId = row.orderProductId;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    expandSelect(row, expandedRows) {
      // expandedRows.length != 0 ? console.log(row.id) : ''
      if (expandedRows.length != 0) {
        //在tableData中循环

        let obj = {};
        obj = this.tableData.find(item => {
          return item.orderNo === row.orderNo;
        });

        row.products = obj.products;
      }
    },

    cellStyle(row, column, rowIndex, columnIndex) {
      if (row.row.taskStatus === "已分配" && row.column.label === "任务状态") {
        return "color:orange";
      }
      if (row.row.taskStatus === "新建" && row.column.label === "任务状态") {
        return "color:red";
      }
    },
    handleClick(tab, event) {
      console.log(tab, event);
    },

    clickRow(row, column, event) {
      console.log(row.id);
    },

    fetchData() {
      appSvc
        .getPurchaseTaskAllocations(this.condition)
        .then(
          res => {
            this.tableData = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    search(flag) {
      this.fetchData();
    }
  }
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 30px;
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
