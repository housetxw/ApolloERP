<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <header class="bodyTop">
        <article class="filter-container">
          <span class="input-label">申请单号:</span>
          <el-input
            v-model="condition.taskId"
            clearable
            @change="updatetaskId()"
            placeholder="请输入申请单号"
            style="width:150px;"
          />

          <span class="input-label">任务类型:</span>
          <el-select
            v-model="condition.taskType"
            size="small"
            clearable
            filterable
            placeholder="请选择类型"
            class="margin-right-10"
            style="width:160px;"
          >
            <el-option
              v-for="item in taskTypeSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>

          <span class="input-label">任务状态:</span>
          <el-select
            v-model="condition.taskStatus"
            size="small"
            clearable
            filterable
            placeholder="请选择状态"
            class="margin-right-10"
          >
            <el-option
              v-for="item in taskStatusSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>

          <span class="input-label">产品名称:</span>
          <el-input
            v-model="condition.productId"
            clearable
            style="width:200px;"
            placeholder="请输入产品名称或编码"
          />

          <span class="input-label">源仓:</span>
          <el-select
            v-model="condition.sourceWarehouse"
            size="small"
            clearable
            filterable
            placeholder="请选择仓库"
            class="margin-right-10"
          >
            <el-option
              v-for="item in warehouseSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>

          <span class="input-label">目标仓:</span>

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

          <!-- <el-input v-model="condition.name" clearable class="el-input el-input--suffix" placeholder="请输入系统名称"></el-input> -->
          <el-button
            type="primary"
            size="small"
            style="margin-left:30px;"
            @click="search(true)"
            icon="el-icon-search"
          >搜索</el-button>
          <el-button type="primary" size="small" @click="showCreate('create', null)">新增</el-button>
        </article>
      </header>
      <main>
        <section>
          <el-table
            border
            :data="tableData"
            @expand-change="expandSelect"
            @row-click="clickRow"
            style="width: 100%"
            :cell-style="cellStyle"
          >
            <el-table-column type="expand">
              <template slot-scope="props">
                <el-table
                  border
                  :data="props.row.products"
                  :cell-style="cellStyle"
                  style="width: 100%"
                >
                  <el-table-column label="产品单号" prop="id"></el-table-column>
                  <el-table-column label="状态" prop="productStatus"></el-table-column>
                  <el-table-column label="产品名称" prop="productName"></el-table-column>
                  <el-table-column label="产品编号" prop="productId"></el-table-column>

                  <el-table-column label="申请数量" prop="num"></el-table-column>

                  <el-table-column label="操作" width="200">
                    <template slot-scope="scope">
                      <el-button
                        size="mini"
                        type="danger"
                        :disabled="scope.row.isReject == true"
                        @click="rejectReplenishTask(scope.row)"
                      >驳回</el-button>

                      <el-button
                        size="mini"
                        type="primary"
                        :disabled="scope.row.isSend == true"
                        @click="outStockLoad(scope.row)"
                      >出库</el-button>
                    </template>
                  </el-table-column>
                </el-table>
              </template>
            </el-table-column>
            <el-table-column label="申请单号" prop="taskId" width="80px;"></el-table-column>
            <el-table-column label="申请类型" prop="taskType"></el-table-column>
            <el-table-column label="状态" prop="taskStatus"></el-table-column>

            <el-table-column label="源仓库" prop="sourceWarehouseName"></el-table-column>
            <el-table-column label="目标仓库" prop="targetWarehouseName"></el-table-column>

            <el-table-column label="申请总数" prop="totalNum"></el-table-column>

            <el-table-column label="采购人" prop="createBy"></el-table-column>
            <el-table-column label="创建时间" prop="createTime"></el-table-column>
            <el-table-column label="操作" width="200">
              <template slot-scope="scope">
                <!-- 签收和入库是两个概念 -->
                <el-button
                  type="primary"
                  size="small"
                  :disabled="scope.row.isAdd == true"
                  @click="showCreate('add', scope.row)"
                >新增</el-button>
                <el-button
                  size="mini"
                  type="primary"
                  :disabled="scope.row.isPass == true"
                  @click="passReplenish(scope.row)"
                >通过</el-button>
              </template>
            </el-table-column>
          </el-table>
        </section>
        <section class="pagination">
          <el-pagination
            background
            :page-size="10"
            :page-sizes="[10, 30, 50, 100]"
            layout="total, sizes, prev, pager, next, jumper"
            :total="totalList"
            @current-change="pageTurning"
            :current-page.sync="currentPage"
            @size-change="sizeChange"
          ></el-pagination>
        </section>
      </main>
    </section>
    <!-- 首页 -->

    <!-- 创建出库申请单 -->
    <section id="orderSend">
      <el-dialog
        :title="formTitle"
        :close-on-click-modal="false"
        :visible.sync="formVisible"
        :before-close="outCancel"
      >
        <el-form :model="outFormModel" :inline="true" :rules="rules" ref="outFormModel">
          <el-form-item label="源发货仓" prop="sourceWarehouseName">
            <el-select
              v-model="outFormModel.sourceWarehouseName"
              clearable
              filterable
              @change="changeSourceWarehouse"
              placeholder="请选择源仓库"
            >
              <el-option
                v-for="item in sourceWarehouseSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="目标仓库" prop="targetWarehouseName">
            <!-- <el-select
              v-model="outFormModel.targetWarehouseName"
              clearable
              @change="changeTargetWarehouse"
              filterable
              placeholder="请选择目标仓库"
            >
              <el-option
                v-for="item in targetWarehouseSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>-->

            <el-select
              v-model="shopcondition.searchLocationId"
              @change="updatenewLocation"
              filterable
              remote
              clearable
              reserve-keyword
              placeholder="请输入门店名称"
              :remote-method="getnewShopinfo"
              :loading="loading"
            >
              <el-option
                v-for="item in newShopSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="产品编号:" prop="productId">
            <el-input v-model="outFormModel.productId" placeholder="请输入产品编号" />
          </el-form-item>

          <el-form-item label="产品名称:" prop="productName">
            <el-input v-model="outFormModel.productName" placeholder="请输入产品名称" />
          </el-form-item>
          <el-form-item label="出库数量:" prop="num">
            <el-input
              oninput="value=value.replace(/[^\d.]/g,'')"
              v-model.number="outFormModel.num"
              placeholder="请输入出库数量"
            />
          </el-form-item>
          <el-form-item label="备注:">
            <el-input placeholder="请输入备注" type="textarea" :rows="5" v-model="outFormModel.remark" />
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="outCancel('outFormModel')">取消</el-button>
          <el-button type="primary" @click="outSave('outFormModel')">保存</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 创建出库申请单 -->

    <!-- 驳回申请子单页面 -->
    <section id="rejectTask">
      <el-dialog
        width="500px"
        :title="rejectFormTitle"
        :close-on-click-modal="false"
        :visible.sync="rejectFormVisible"
        :before-close="rejectCancel"
      >
        <el-form :model="rejectFormModel" ref="rejectFormModel">
          <el-form-item label="理由:" size="50">
            <el-input
              type="textarea"
              :rows="5"
              placeholder="请输入理由"
              v-model="rejectFormModel.rejectReason"
            ></el-input>
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="rejectCancel('rejectFormModel')">取消</el-button>
          <el-button type="primary" @click="rejectSave('rejectFormModel')">确定</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 删除采购子单页面 -->

    <!-- 选择出库产品界面 -->
    <section id="outStockDialog">
      <el-dialog
        :title="outStockFormTitle"
        :close-on-click-modal="false"
        :visible.sync="outStockFormVisible"
      >
        <el-table border :data="outstockTableData" style="width: 100%">
          <el-table-column label="库存编号" prop="id"></el-table-column>
          <el-table-column label="产品编号" prop="productId"></el-table-column>
          <el-table-column label="产品名称" prop="productName"></el-table-column>
          <el-table-column label="可用库存" width="85px" prop="availableNum"></el-table-column>
          <el-table-column label="剩余申请量" width="85px" prop="applyNum"></el-table-column>
          <el-table-column label="批次已申请数量" width="85px" prop="alreadyOutNum"></el-table-column>

          <!-- <el-table-column
            label="良品类型"
            width="85px"
            prop="stockTypeName"
          ></el-table-column>-->
          <el-table-column label="批次号" width="85px" prop="batchId"></el-table-column>

          <el-table-column label="周期" width="95px" prop="weekYear"></el-table-column>
          <el-table-column label="创建时间" width="95px" prop="createTime"></el-table-column>

          <el-table-column width="120px" prop="outStockNum" label="操作">
            <template slot-scope="scope">
              <el-input
                placeholder="出库数"
                v-model="scope.row.outStockNum"
                oninput="value=value.replace(/[^\d.]/g,'')"
                clearable
              ></el-input>
            </template>
          </el-table-column>
        </el-table>

        <div slot="footer" class="dialog-footer">
          <el-button @click="outStockCancel()">取消</el-button>
          <el-button type="primary" @click="outStockSave()">保存</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 入库收货界面 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/wms/replenishtask";

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
      actualInStockNum: 1,
      readonly: true,
      tableLoading: false,
      currentPage: 1,
      //flag: this.global.deletedFlag,
      //下拉框的条件

      taskStatusSelCondition: {
        RequestType: 2
      },
      taskTypeSelCondition: {
        RequestType: 1
      },
      warehouseSelCondition: {
        RequestType: 3
      },
      rejectFormTitle: undefined,
      rejectFormVisible: false,
      rejectFormModel: {
        rejectReason: undefined,
        taskId: undefined,
        id: undefined
      },
      rejectFormModelInit: {
        rejectReason: undefined,
        taskId: undefined,
        id: undefined
      },

      passFormModel: {
        id: undefined,
        taskId: undefined
      },

      outFormModel: {
        id: undefined,
        taskId: undefined,
        sourceWarehouseName: undefined,
        targetWarehouseName: undefined,
        sourceWarehouse: undefined,
        targetWarehouse: undefined,
        productId: undefined,
        productName: undefined,
        num: undefined,
        remark: undefined,
        taskStatus: undefined,
        productStatus: undefined
      },

      outFormModelInit: {
        id: undefined,
        taskId: undefined,
        sourceWarehouseName: undefined,
        targetWarehouseName: undefined,
        sourceWarehouse: undefined,
        targetWarehouse: undefined,
        productId: undefined,
        productName: undefined,
        num: undefined,
        remark: undefined,
        taskStatus: undefined,
        productStatus: undefined
      },

      outStockFormTitle: "",
      outStockFormVisible: false,
      outstockTableData: [],

      //table页面的条件
      condition: {
        taskId: undefined,

        taskstatus: undefined,
        searchLocationId: undefined,
        pageIndex: 1,
        pageSize: 10,
        //产品编号
        productId: undefined,
        targetWarehouse: undefined,
        sourceWarehouse: undefined,
        taskType: undefined
      },

      outStockCondition: {
        locationId: undefined,
        productId: undefined,
        taskId: undefined,
        taskProductId: undefined,
        applyNum: undefined
      },

      outStockConditionInit: {
        locationId: undefined,
        productId: undefined,
        taskId: undefined,
        taskProductId: undefined,
        applyNum: undefined
      },

      taskStatusSel: [],
      shopSel: [],
      loading: false,
      shopSelCondition: {
        simpleName: undefined
      },
      shopcondition: {
        searchLocationId: undefined
      },

      shopnewCondition: {
        simpleName: undefined
      },
      newShopSel: [],

      warehouseSel: [],
      taskTypeSel: [],
      sourceWarehouseSel: [],
      targetWarehouseSel: [],
      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",
      rules: {
        sourceWarehouseName: [
          { required: true, message: "请选择源发货仓", trigger: "blur" }
        ],
        targetWarehouseName: [
          { required: true, message: "请选择目标仓库", trigger: "blur" }
        ],

        productId: [
          { required: true, message: "请输入产品编号", trigger: "blur" }
        ],
        productName: [
          { required: true, message: "请输入产品名称", trigger: "blur" }
        ],

        num: [{ required: true, message: "请输入申请数量", trigger: "blur" }]
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
    updateLocation() {
      if (this.condition.searchLocationId == "") {
        this.condition.searchLocationId = undefined;
        this.condition.targetWarehouse = undefined;
      }
    },

    getnewShopinfo(query) {
      if (query != "") {
        debugger;
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopnewCondition.simpleName = query;
          appSvc
            .getShopList(this.shopcondition)
            .then(
              res => {
                this.newShopSel = res.data;
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
      // else if(row.column.label==="告警级别"&& row.row.alarmLevel==="一般告警" ){
      //   return 'color:yellow'
      // }
    },

    updatetaskId() {
      if (this.condition.taskId == "") {
        this.condition.taskId = undefined;
      }
    },

    rejectReplenishTask(row) {
      this.rejectFormVisible = true;
      this.rejectFormTitle = "驳回申请单";
      this.rejectFormModel.taskId = row.taskId;
      this.rejectFormModel.id = row.id;
    },
    changeSourceWarehouse(val) {
      //locations是v-for里面的也是datas里面的值
      let obj = {};
      var warehouseName = "";
      for (var i = 0; i < this.sourceWarehouseSel.length; i++) {
        if (this.sourceWarehouseSel[i].key == Number(val))
          warehouseName = this.sourceWarehouseSel[i].value;
      }

      // obj = this.warehouseSel.find(item => {
      //   return item.key === Number(val);
      // });
      this.outFormModel.sourceWarehouseName = warehouseName;
      this.outFormModel.sourceWarehouse = Number(val);
    },

    updatenewLocation(val) {
      //locations是v-for里面的也是datas里面的值
      let obj = {};
      var warehouseName = "";
      for (var i = 0; i < this.newShopSel.length; i++) {
        if (this.newShopSel[i].key == Number(val))
          warehouseName = this.newShopSel[i].value;
      }

      // obj = this.warehouseSel.find(item => {
      //   return item.key === Number(val);
      // });
      this.outFormModel.targetWarehouseName = warehouseName;
      this.outFormModel.targetWarehouse = Number(val);
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
    outStockLoad(row) {
      debugger;
      this.outStockCondition.locationId = row.locationId;
      this.outStockCondition.productId = row.productId;
      this.outStockCondition.taskId = row.taskId;
      this.outStockCondition.taskProductId = row.id;
      this.outStockCondition.applyNum = row.num;
      appSvc
        .getStockLocations(this.outStockCondition)
        .then(
          res => {
            this.outstockTableData = res.data;
            this.outStockFormVisible = true;
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
    getTaskType() {
      appSvc
        .getBasicInfoList(this.taskTypeSelCondition)
        .then(
          res => {
            this.taskTypeSel = res.data;
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
      this.getTaskType();
      // this.getConditionData();

      appSvc
        .getReplenishTaskList(this.condition)
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
    sizeChange(val) {
      this.condition.pageIndex = this.currentPage = 1;
      this.condition.pageSize = val;
      this.getPaged();
    },
    pageTurning() {
      this.condition.pageIndex = this.currentPage;
      this.getPaged();
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
        .getReplenishTaskList(this.condition)
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
    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    },
    //保存入库详情的数据
    outSave(formName) {
      debugger;
      var vm = this;
      console.log("formModel: " + JSON.stringify(this.outFormModel));
      if (Number(this.outFormModel.num) <= 0) {
        this.$message({
          message: "申请数量必须大于0",
          type: "warning"
        });
        return;
      }

      this.$refs[formName].validate(valid => {
        if (valid) {
          appSvc
            .createReplenishTask(this.outFormModel)
            .then(res => {
              if (res.code == 10000) {
                this.$message({
                  message: res.message,
                  type: "success"
                });
                this.formVisible = false;
                this.outFormModel = JSON.parse(
                  JSON.stringify(this.outFormModelInit)
                );
                vm.search();

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

    passReplenish(row) {
      this.$confirm("确定审核通过吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          this.passFormModel.id = row.id;
          this.passFormModel.taskId = row.taskId;

          //调用后台接口
          appSvc
            .passReplenishTask(this.passFormModel)
            .then(res => {
              if (res.code == 10000) {
                this.$message({
                  message: res.message,
                  type: "success"
                });

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

    outCancel(formName) {
      this.formVisible = false;
      // this.resetSendForm();
      this.outFormModel = JSON.parse(JSON.stringify(this.outFormModelInit));
      var frmName =
        typeof formName === "string" && formName ? formName : "outFormModel";
      this.$refs[frmName].resetFields();
    },

    rejectSave(formName) {
      debugger;
      var vm = this;
      console.log("formModel: " + JSON.stringify(this.rejectFormModel));
      if (
        this.rejectFormModel.rejectReason == "" ||
        this.rejectFormModel.rejectReason == null
      ) {
        this.$message({
          message: "请填写驳回原因!",
          type: "warning"
        });
        return;
      }
      appSvc
        .rejectReplenishTask(this.rejectFormModel)
        .then(res => {
          if (res.code == 10000) {
            this.$message({
              message: res.message,
              type: "success"
            });
            this.rejectFormVisible = false;
            this.rejectFormModel = JSON.parse(
              JSON.stringify(this.rejectFormModelInit)
            );
            vm.search();

            vm.cancel();
          }
        })
        .catch(error => {
          console.log(error);
        });
    },
    rejectCancel(formName) {
      this.rejectFormVisible = false;
      // this.resetSendForm();
      this.rejectFormModel = JSON.parse(
        JSON.stringify(this.rejectFormModelInit)
      );
      var frmName =
        typeof formName === "string" && formName ? formName : "rejectFormModel";
      this.$refs[frmName].resetFields();
    },
    outStockCancel() {
      this.outStockFormVisible = false;
      this.outStockCondition = JSON.parse(
        JSON.stringify(this.outStockConditionInit)
      );
    },
    outStockSave() {
      debugger;
      if (this.outstockTableData.length <= 0) {
        return;
      }

      //获取出库数量不能大于申请数量
      var sumOutStockNum = 0;

      for (var i = 0; i < this.outstockTableData.length; i++) {
        if (
          Number(this.outstockTableData[i].outStockNum) >
          Number(this.outstockTableData[i].availableNum)
        ) {
          this.$message({
            message:
              "填写的出库数量【" +
              this.outstockTableData[i].outStockNum +
              "】" +
              "不能大于该批次的可用数量【" +
              this.outstockTableData[i].availableNum +
              "】",
            type: "warning"
          });
          return;
        }
      }

      for (var i = 0; i < this.outstockTableData.length; i++) {
        if (Number(this.outstockTableData[i].outStockNum) > 0) {
          sumOutStockNum += Number(this.outstockTableData[i].outStockNum);
        }
      }
      var applyNum = this.outstockTableData[0].applyNum;
      if (sumOutStockNum > applyNum) {
        this.$message({
          message:
            "填写的出库数量【" +
            sumOutStockNum +
            "】" +
            "不能大于申请数量【" +
            applyNum +
            "】",
          type: "warning"
        });
        return;
      }
      var vm = this;
      //提交数据 createWarehouseTransferTask

      appSvc
        .createWarehouseTransferTask(this.outstockTableData)
        .then(res => {
          if (res.code == 10000) {
            this.$message({
              message: res.message,
              type: "success"
            });
            this.outStockFormVisible = false;
            // this.rejectFormModel = JSON.parse(
            //   JSON.stringify(this.rejectFormModelInit)
            // );
            vm.search();

            vm.cancel();
          }
        })
        .catch(error => {
          console.log(error);
        });
    }
  }
};
</script>

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
</style>
