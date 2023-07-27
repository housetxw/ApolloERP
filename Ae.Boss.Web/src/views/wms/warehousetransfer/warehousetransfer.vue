<template>
  <main class="container">
    <!-- 首页 -->
    <section>
      <rg-page
        id="indexPage"
        :pageIndex="condition.pageIndex"
        :pageSize="condition.pageSize"
        :dataCount="totalList"
        :tableLoading="tableLoading"
        :tableData="tableData"
        :pageChange="pageChange"
        :headerBtnWidth="180"
        :searching="search"
        :defaultCollapse="false"
        :getRowKeys="getRowKeys"
        :expands="expands"
        @expand-change="expandSelect"
      >
        <template v-slot:condition>
          <el-form-item>
            <el-input
              v-model="condition.taskId"
              clearable
              @change="updatetaskId()"
              placeholder="请输入申请单号"
            />
          </el-form-item>
          <el-form-item>
            <el-input
              v-model="condition.transferId"
              clearable
              @change="updatetransferId()"
              placeholder="请输入关联单号"
            />
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.transferType"
              clearable
              filterable
              placeholder="请选择类型"
            >
              <el-option
                v-for="item in transferTypeSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.taskStatus"
              clearable
              filterable
              placeholder="请选择状态"
            >
              <el-option
                v-for="item in taskStatusSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-input
              v-model="condition.productId"
              clearable
              placeholder="请输入产品名称或编码"
            />
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.sourceWarehouse"
              clearable
              filterable
              placeholder="请选择仓库"
            >
              <el-option
                v-for="item in warehouseSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
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
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
        </template>

        <template v-slot:tb_cols>
          <el-table-column type="expand">
            <template slot-scope="props">
              <el-table
                border
                :data="props.row.products"
                :cell-style="cellStyle"
                style="width: 100%"
              >
                <el-table-column
                  label="产品单号"
                  prop="id"
                  align="center"
                ></el-table-column>

                <el-table-column
                  label="产品名称"
                  prop="productName"
                  align="center"
                ></el-table-column>
                <el-table-column
                  label="产品编号"
                  prop="productId"
                  align="center"
                ></el-table-column>

                <el-table-column
                  label="出库数量"
                  prop="num"
                  align="center"
                ></el-table-column>

                <el-table-column
                  label="入库数量"
                  prop="receiveNum"
                  align="center"
                ></el-table-column>

                <el-table-column
                  label="供应商"
                  prop="venderShortName"
                  align="center"
                ></el-table-column>

                <el-table-column
                  label="批次号"
                  prop="batchId"
                  align="center"
                ></el-table-column>
                <el-table-column
                  label="成本"
                  prop="cost"
                  align="center"
                ></el-table-column>
              </el-table>
            </template>
          </el-table-column>
          <el-table-column
            label="申请单号"
            prop="taskId"
            width="100px;"
            align="center"
          ></el-table-column>
          <el-table-column
            label="关联单号"
            prop="transferId"
            width="105px;"
            align="center"
          ></el-table-column>
          <el-table-column
            label="出库类型"
            prop="transferType"
            width="100px;"
            align="center"
          ></el-table-column>
          <el-table-column
            label="状态"
            prop="taskStatus"
            width="100px;"
            align="center"
          ></el-table-column>

          <el-table-column
            label="源仓库"
            prop="sourceWarehouseName"
            align="center"
          ></el-table-column>
          <el-table-column
            label="目标仓库"
            prop="targetWarehouseName"
            align="center"
          ></el-table-column>

          <el-table-column
            label="总数"
            prop="outTotalNum"
            width="100px;"
            align="center"
          ></el-table-column>
          <el-table-column
            label="总金额"
            prop="outTotalPrice"
            width="100px;"
            align="center"
          ></el-table-column>
          <el-table-column
            label="采购人"
            prop="createBy"
            align="center"
          ></el-table-column>
          <el-table-column
            label="创建时间"
            prop="createTime"
            align="center"
          ></el-table-column>
          <el-table-column
            label="操作"
            fixed="right"
            align="center"
            width="220px"
          >
            <template slot-scope="scope">
              <!-- 签收和入库是两个概念 -->
              <el-button
                style="padding:10px 7px;"
                size="mini"
                type="primary"
                v-show="scope.row.taskStatus == '等待发出' ? true : false"
                @click="deliveryInput(scope.row)"
                >包裹录入</el-button
              >

              <el-button
                size="mini"
                style="padding:10px 7px;"
                type="primary"
                v-show="scope.row.taskStatus == '任务接收' ? true : false"
                @click="deliverySend(scope.row)"
                >发出</el-button
              >
              <el-button
                size="mini"
                style="padding:10px 7px;"
                type="primary"
                v-show="
                  scope.row.taskStatus == '任务接收' ||
                  scope.row.taskStatus == '等待发出'
                    ? true
                    : false
                "
                @click="cancelTransferTask(scope.row)"
                >取消</el-button
              >
              <el-button
                size="mini"
                style="padding:10px 7px;"
                type="primary"
                v-show="
                  scope.row.taskStatus == '等待发出' ||
                  scope.row.taskStatus == '已取消'
                    ? false
                    : true
                "
                @click="viewPackage(scope.row)"
                >查看包裹</el-button
              >
            </template>
          </el-table-column>
        </template>
      </rg-page>
    </section>
    <!-- 首页 -->

    <!-- 驳回申请子单页面 -->
    <section id="deliverySection">
      <!-- <el-dialog
        :title="deliveryFormTitle"
        :close-on-click-modal="false"
        :visible.sync="deliveryFormVisible"
        :before-close="deliveryCancel"
      >-->
      <rg-dialog
        :title="deliveryFormTitle"
        :visible.sync="deliveryFormVisible"
        v-if="deliveryFormVisible"
        :beforeClose="deliveryCancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            deliveryCancel('deliveryFormModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="600px"
      >
        <template v-slot:content>
          <el-form
            :model="deliveryFormModel"
            ref="deliveryFormModel"
            :rules="rules"
          >
            <el-form-item
              label="物流公司:"
              size="50"
              prop="deliveryCompany"
              :label-width="formLabelWidth"
            >
              <el-select
                v-model="deliveryFormModel.deliveryCompany"
                clearable
                filterable
                placeholder="请选择物流公司"
                @change="selectDeliveryCompany"
                style="width:400px"
              >
                <el-option
                  v-for="item in deliveryCompanySel"
                  :key="item.key"
                  :label="item.value"
                  :value="item.key"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item
              label="物流单号"
              prop="deliveryCode"
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="deliveryFormModel.deliveryCode"
                placeholder="请输入物流单号"
                :disabled="isInputDeliveryCode"
                style="width:400px"
              />
            </el-form-item>
            <el-form-item
              label="物流电话"
              prop="deliveryTel"
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="deliveryFormModel.deliveryTel"
                placeholder="请输入物流电话"
                style="width:400px"
              />
            </el-form-item>

            <el-form-item
              label="物流费用"
              prop="deliveryFee"
              :label-width="formLabelWidth"
            >
              <el-input
                v-model="deliveryFormModel.deliveryFee"
                placeholder="请输入物流费用"
                style="width:400px"
                oninput="value=value.replace(/[^\d.]/g,'')"
              />
            </el-form-item>
            <el-form-item label="包裹重量" :label-width="formLabelWidth">
              <el-input
                width="200px"
                v-model="deliveryFormModel.deliveryWeight"
                placeholder="请输入重量(单位:kg)"
                oninput="value=value.replace(/[^\d.]/g,'')"
                style="width:400px"
              />
            </el-form-item>
            <el-form-item label="备注" :label-width="formLabelWidth">
              <el-input
                v-model="deliveryFormModel.remark"
                placeholder="请输入备注"
                style="width:400px"
              />
            </el-form-item>
            <el-form-item>
              <el-button
                type="primary"
                @click="deliverySave('deliveryFormModel')"
                style="margin-left:120px;"
                >确定</el-button
              >
            </el-form-item>
          </el-form>

          <el-table
            border
            :data="deliveryTableData"
            :cell-style="cellStyle"
            style="width: 100%"
          >
            <el-table-column
              label="物流公司"
              prop="deliveryCompany"
              align="center"
            ></el-table-column>

            <el-table-column
              label="物流单号"
              prop="deliveryCode"
              align="center"
            ></el-table-column>
            <el-table-column
              label="物流电话"
              prop="deliveryTel"
              align="center"
            ></el-table-column>

            <el-table-column
              label="包裹重量"
              prop="deliveryWeight"
              align="center"
            ></el-table-column>

            <el-table-column
              label="物流费用"
              prop="deliveryFee"
              align="center"
            ></el-table-column>

            <el-table-column
              label="备注"
              prop="remark"
              align="center"
            ></el-table-column>
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button
            type="warning"
            @click="deliveryAccept('deliveryFormModel')"
            size="mini"
            >录入结束</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 删除采购子单页面 -->

    <!-- 查看包裹信息 -->
    <section id="packageSection">
      <!-- <el-dialog
        :title="packageFormTitle"
        :close-on-click-modal="false"
        :visible.sync="packageFormVisible"
        :before-close="packageCancel"
      >-->
      <rg-dialog
        :title="packageFormTitle"
        :visible.sync="packageFormVisible"
        v-if="packageFormVisible"
        :beforeClose="packageCancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            packageCancel('deliveryFormModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="600px"
      >
        <template v-slot:content>
          <el-table
            border
            :data="packageTableData"
            :cell-style="cellStyle"
            style="width: 100%"
          >
            <el-table-column
              label="物流公司"
              prop="deliveryCompany"
              align="center"
            ></el-table-column>

            <el-table-column
              label="物流单号"
              prop="deliveryCode"
              align="center"
            ></el-table-column>
            <el-table-column
              label="物流电话"
              prop="deliveryTel"
              align="center"
            ></el-table-column>

            <el-table-column
              label="包裹重量"
              prop="deliveryWeight"
              align="center"
            ></el-table-column>

            <el-table-column
              label="物流费用"
              prop="deliveryFee"
              align="center"
            ></el-table-column>

            <el-table-column
              label="备注"
              prop="remark"
              align="center"
            ></el-table-column>
          </el-table>
        </template>
      </rg-dialog>
    </section>
    <!-- 查看包裹信息 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/wms/warehousetransfer";
import { Loading } from "element-ui";
import { isNumber } from "util";
export default {
  data() {
    return {
      expands: [],
      getRowKeys(row) {
        return row.taskId;
      },
      pickerOptionsStart: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        }
      },
      isInputDeliveryCode: false,
      input: undefined,
      actualInStockNum: 1,
      readonly: true,
      tableLoading: false,
      currentPage: 1,
      //flag: this.global.deletedFlag,
      //下拉框的条件

      taskStatusSelCondition: {
        RequestType: 1
      },
      transferTypeSelCondition: {
        RequestType: 2
      },
      warehouseSelCondition: {
        RequestType: 3
      },
      deliverySelCondition: {
        RequestType: 4
      },

      //table页面的条件
      condition: {
        taskId: undefined,

        taskstatus: undefined,

        pageIndex: 1,
        pageSize: 10,
        //产品编号
        productId: undefined,
        transferId: undefined,
        searchLocationId: undefined,
        transferType: undefined,
        targetWarehouse: undefined,
        sourceWarehouse: undefined
      },

      deliveryFormVisible: false,
      deliveryCompanySel: [],
      deliveryFormTitle: undefined,
      taskStatusSel: [],
      deliveryTableData: [],
      warehouseSel: [],
      transferTypeSel: [],
      deliveryFormModel: {
        deliveryCompany: undefined,
        deliveryCode: undefined,
        deliveryTel: undefined,
        deliveryFee: undefined,
        deliveryWeight: undefined,
        remark: undefined,
        transferId: undefined,
        transferType: undefined
      },

      deliveryFormModelInit: {
        deliveryCompany: undefined,
        deliveryCode: undefined,
        deliveryTel: undefined,
        deliveryFee: undefined,
        deliveryWeight: undefined,
        remark: undefined,
        transferId: undefined,
        transferType: undefined
      },

      deliverySaveCondition: {
        transferId: undefined,
        transferType: undefined,
        status: undefined,
        updateType: undefined
      },

      viewPackageCondition: {
        transferId: undefined,
        transferType: undefined
      },
      viewPackageConditionInit: {
        transferId: undefined,
        transferType: undefined
      },

      packageTableData: [],
      packageFormTitle: undefined,
      packageFormVisible: false,
      deliverySaveConditionInit: {
        transferId: undefined,
        transferType: undefined,
        status: undefined,
        updateType: undefined
      },
      shopSel: [],
      loading: false,
      shopSelCondition: {
        simpleName: undefined
      },
      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",
      rules: {
        deliveryCompany: [
          { required: true, message: "请选择物流公司", trigger: "blur" }
        ],
        deliveryTel: [
          { required: true, message: "请输入物流电话", trigger: "blur" }
        ],
        deliveryFee: [
          { required: true, message: "请输入物流费用", trigger: "blur" }
        ]
      },
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
    selectDeliveryCompany() {
      if (
        this.deliveryFormModel.deliveryCompany == "自提" ||
        this.deliveryFormModel.deliveryCompany == "总部快运"
      ) {
        this.isInputDeliveryCode = true;
      } else {
        this.isInputDeliveryCode = false;
      }
    },
    cancelTransferTask(row) {
      this.$confirm("确定操作吗？", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          this.deliverySaveCondition.transferId = row.transferId;
          this.deliverySaveCondition.transferType = row.transferType;
          this.deliverySaveCondition.status = "已取消";
          this.deliverySaveCondition.updateType = 1;

          //调用后台接口
          appSvc
            .updateWarehouseTransferStatus(this.deliverySaveCondition)
            .then(res => {
              if (res.code == 10000) {
                if (res.message != "") {
                  this.$message({
                    message: "操作成功!",
                    type: "success"
                  });
                }

                this.deliveryFormVisible = false;
                this.deliverySaveCondition = JSON.parse(
                  JSON.stringify(this.deliverySaveConditionInit)
                );

                vm.search();

                vm.cancel();
              }
            })
            .catch(error => {
              console.log(error);
            });
        })
        .catch(() => {});
    },
    updateLocation() {
      if (this.condition.searchLocationId == "") {
        this.condition.searchLocationId = undefined;
        this.condition.targetWarehouse = undefined;
      }
    },
    getShopinfo(query) {
      if (query != "") {
        debugger;
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          debugger;
          this.shopSelCondition.simpleName = query;
          appSvc
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

    viewPackage(row) {
      this.packageFormTitle = "包裹信息";
      this.packageFormVisible = true;
      this.viewPackageCondition.transferId = row.transferId;
      this.viewPackageCondition.transferType = row.transferType;

      appSvc
        .getWarehouseTransferPackages(this.viewPackageCondition)
        .then(
          res => {
            this.packageTableData = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    packageCancel(form) {
      this.packageFormVisible = false;

      this.viewPackageCondition = JSON.parse(
        JSON.stringify(this.viewPackageConditionInit)
      );
    },

    cellStyle(row, column, rowIndex, columnIndex) {
      //根据报警级别显示颜色
      // console.log(row);
      // console.log(row.column);
      if (row.row.taskStatus === "已驳回" && row.column.label === "状态") {
        return "color:red";
      }

      if (row.row.productStatus === "已驳回" && row.column.label === "状态") {
        return "color:red";
      }
    },

    deliverySend(row) {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          this.deliverySaveCondition.transferId = row.transferId;
          this.deliverySaveCondition.transferType = row.transferType;
          this.deliverySaveCondition.status = "已发出";
          this.deliverySaveCondition.updateType = 2;

          const loading = this.$loading({
            lock: true,
            text: "拼命处理中......",
            spinner: "el-icon-loading",
            background: "rgba(0, 0, 0, 0.7)"
          });

          //调用后台接口
          appSvc
            .updateWarehouseTransferStatus(this.deliverySaveCondition)
            .then(res => {
              if (res.code == 10000) {
                //如果是调拨发出 系统要做提示
                if (res.message == "success") {
                  this.$message({
                    message: "操作成功!",
                    type: "success"
                  });

                  vm.search();
                  vm.cancel();
                } else {
                  //提示库存不足的消息
                  this.$alert(res.message, "提示", {
                    confirmButtonText: "确定",
                    callback: action => {}
                  });
                }

                this.deliverySaveCondition = JSON.parse(
                  JSON.stringify(this.deliverySaveConditionInit)
                );
              }
            })
            .catch(error => {
              console.log(error);
            })
            .finally(() => {
              loading.close();
            });
        })
        .catch(() => {});
    },

    deliveryAccept() {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          this.deliverySaveCondition.transferId = this.deliveryFormModel.transferId;
          this.deliverySaveCondition.transferType = this.deliveryFormModel.transferType;
          this.deliverySaveCondition.status = "任务接收";
          this.deliverySaveCondition.updateType = 1;

          //调用后台接口
          appSvc
            .updateWarehouseTransferStatus(this.deliverySaveCondition)
            .then(res => {
              if (res.code == 10000) {
                if (res.message != "") {
                  this.$message({
                    message: "操作成功!",
                    type: "success"
                  });
                }

                this.deliveryFormVisible = false;
                this.deliverySaveCondition = JSON.parse(
                  JSON.stringify(this.deliverySaveConditionInit)
                );

                vm.search();

                vm.cancel();
              }
            })
            .catch(error => {
              console.log(error);
            });
        })
        .catch(() => {});
    },

    updatetaskId() {
      if (this.condition.taskId == "") {
        this.condition.taskId = undefined;
      }
    },
    updatetransferId() {
      if (this.condition.transferId == "") {
        this.condition.transferId = undefined;
      }
    },
    deliveryInput(row) {
      this.deliveryFormVisible = true;
      this.deliveryFormTitle = "录入物流信息";
      this.deliveryFormModel.transferId = row.transferId;
      this.deliveryFormModel.transferType = row.transferType;
      appSvc
        .getBasicInfoList(this.deliverySelCondition)
        .then(
          res => {
            this.deliveryCompanySel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});

      appSvc
        .getWarehouseTransferPackages(this.deliveryFormModel)
        .then(
          res => {
            this.deliveryTableData = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});

      //deliveryTableData
    },
    expandSelect(row, expandedRows) {
      // expandedRows.length != 0 ? console.log(row.id) : ''
      if (expandedRows.length != 0) {
        //在tableData中循环

        let obj = {};
        obj = this.tableData.find(item => {
          return item.taskId === row.taskId;
        });

        row.products = obj.products;
      }
    },
    clickRow(row, column, event) {
      console.log(row.id);
      // console.log(column)
      // console.log(event)
    },

    //获取目标仓
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

    //获取采购状态
    getTaskStatus() {
      appSvc
        .getBasicInfoList(this.taskStatusSelCondition)
        .then(
          res => {
            this.taskStatusSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    //获取付款状态
    getTransferType() {
      appSvc
        .getBasicInfoList(this.transferTypeSelCondition)
        .then(
          res => {
            this.transferTypeSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    fetchData() {
      this.getwarehouses();
      this.getTaskStatus();
      this.getTransferType();
      // this.getConditionData();

      appSvc
        .getWarehouseTransferList(this.condition)
        .then(
          res => {
            //  this.tableData = res.data;
            var data = res.data;

            this.tableData = data.taskList;
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
    deliveryCancel() {
      this.deliveryFormVisible = false;
      this.deliveryFormModel = JSON.parse(
        JSON.stringify(this.deliveryFormModelInit)
      );
      this.isInputDeliveryCode = false;
    },

    showCreate(type, row) {
      this.formVisible = true;
      //
      if (type == "create") {
        this.formTitle = "创建出库申请单";
      } else {
        this.formTitle = "【" + row.taskId + "】新增出库申请单";
        this.outFormModel.sourceWarehouseName = row.sourceWarehouseName;
        this.outFormModel.sourceWarehouse = row.sourceWarehouse;
        this.outFormModel.targetWarehouseName = row.targetWarehouseName;
        this.outFormModel.targetWarehouse = row.targetWarehouse;
        this.outFormModel.taskId = row.taskId;
      }

      this.formIsCreated = true;

      //加载仓库的数据
      this.warehouseSelCondition.RequestType = 3;
      appSvc
        .getBasicInfoList(this.warehouseSelCondition)
        .then(
          res => {
            this.sourceWarehouseSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});

      appSvc
        .getBasicInfoList(this.warehouseSelCondition)
        .then(
          res => {
            this.targetWarehouseSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    getPaged(flag) {
      this.tableLoading = true;

      if (
        this.condition.searchLocationId != undefined &&
        this.condition.searchLocationId != ""
      ) {
        this.condition.targetWarehouse = this.condition.searchLocationId;
      }

      if (this.condition.sourceWarehouse == "") {
        this.condition.sourceWarehouse = undefined;
      }

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getWarehouseTransferList(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.taskList;
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

    deliverySave(formName) {
      if (
        this.deliveryFormModel.deliveryCompany != "自提" &&
        this.deliveryFormModel.deliveryCompany != "总部快运" &&
        (this.deliveryFormModel.deliveryCode == "" ||
          this.deliveryFormModel.deliveryCode == undefined)
      ) {
        this.$message({
          message: "请填写快递单号!",
          type: "warning"
        });
        return false;
      }

      this.$refs[formName].validate(valid => {
        if (valid) {
          appSvc
            .createWarehouseTransferPackage(this.deliveryFormModel)
            .then(
              res => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                }

                var transferId = this.deliveryFormModel.transferId;
                var transferType = this.deliveryFormModel.transferType;

                this.deliveryFormModel = JSON.parse(
                  JSON.stringify(this.deliveryFormModelInit)
                );
                //重新load数据

                this.deliveryFormModel.transferId = transferId;
                this.deliveryFormModel.transferType = transferType;
                appSvc
                  .getWarehouseTransferPackages(this.deliveryFormModel)
                  .then(
                    res => {
                      this.deliveryTableData = res.data;
                    },
                    error => {
                      console.log(error);
                    }
                  )
                  .catch(() => {});
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        } else {
          return false;
        }
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
    height: 128px;
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
>>> .el-form--inline .el-form-item {
  padding-bottom: 10px;
}
</style>
