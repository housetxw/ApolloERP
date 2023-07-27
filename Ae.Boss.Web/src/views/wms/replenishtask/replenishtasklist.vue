<template>
  <main class="container">
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :headerBtnWidth="160"
      :tableData="tableData"
      :pageChange="pageTurning"
      :searching="search"
      :conditionModel="condition"
      :cell-style="cellStyle"
       :defaultCollapse="false"
  :getRowKeys="getRowKeys"
      :expands="expands"
         @expand-change="expandSelect"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item prop="costType">
          <el-input
            v-model="condition.taskId"
            clearable
            @change="updatetaskId()"
            placeholder="请输入申请单号"
            style="width:150px;"
          />
        </el-form-item>
        <el-form-item prop="costType">
          <el-select
            v-model="condition.taskType"
            clearable
            filterable
            placeholder="请选择类型"
            style="width:160px;"
          >
            <el-option
              v-for="item in taskTypeSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="costType">
          <el-select v-model="condition.taskStatus" clearable filterable placeholder="请选择状态">
            <el-option
              v-for="item in taskStatusSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="costType">
          <el-select v-model="condition.sourceWarehouse" clearable filterable placeholder="请选择仓库">
            <el-option
              v-for="item in warehouseSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="costType">
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
        <el-form-item prop="costType"></el-form-item>
        <el-form-item prop="costType"></el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button
          type="primary"
          size="mini"
          icon="el-icon-plus"
          @click="showCreate('create', null)"
        >新增</el-button>
      </template>
      <!-- 列表 -->
      <template v-slot:tb_cols>
        <el-table-column type="expand">
          <template slot-scope="props">
            <el-table
              border
              :data="props.row.products"
              :cell-style="cellStyle"
              style="width: 100%"
              size="mini"
            >
              <rg-table-column label="产品单号" prop="id" fix-min />
              <rg-table-column label="状态" prop="productStatus" align="center" fix-min />
              <rg-table-column label="产品编号" prop="productId" />
              <rg-table-column label="产品名称" prop="productName" />
              <rg-table-column label="申请数量" prop="num" is-number fix-min />
              <rg-table-column label="操作" width="100">
                <template slot-scope="scope">
                  <el-button-group>
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
                  </el-button-group>
                </template>
              </rg-table-column>
            </el-table>
          </template>
        </el-table-column>
        <rg-table-column label="申请单号" prop="taskId" min-width="90" fix-min />
        <rg-table-column label="申请类型" prop="taskType" align="center" fix-min />
        <rg-table-column label="状态" prop="taskStatus" align="center" fix-min />

        <rg-table-column label="源仓库" prop="sourceWarehouseName" />
        <rg-table-column label="目标仓库" prop="targetWarehouseName" />

        <rg-table-column label="申请总数" prop="totalNum" is-number fix-min />

        <rg-table-column label="采购人" prop="createBy" fix-min />
        <rg-table-column label="创建时间" prop="createTime" is-datetime="1" width="120px" />
        <rg-table-column label="操作" width="120" align="center">
          <template slot-scope="scope">
            <el-button-group>
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
            </el-button-group>
          </template>
        </rg-table-column>
      </template>
    </rg-page>

    <!-- 创建出库申请单 -->
    <section id="orderSend">
      <rg-dialog
        :title="formTitle"
        :visible.sync="formVisible"
        :beforeClose="outCancel"
        :btnCancel="{label:'关闭',click:(done)=>{outCancel()}}"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="800px"
        minWidth="600px"
      >
        <template v-slot:content>
          <el-form
            :model="outFormModel"
            :rules="rules"
            ref="outFormModel"
            :label-width="formLabelWidth"
          >
            <el-form-item label="任务类型" prop="taskType">
              <el-select
                v-model="outFormModel.taskType"
                clearable
                filterable
                placeholder="请选择类型"
                class="margin-right-10"
                style="width:420px;"
                size="mini"
              >
                <el-option
                  v-for="item in taskTypeSel"
                  :key="item.key"
                  :label="item.value"
                  :value="item.key"
                ></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="源发货仓" prop="sourceWarehouseName" :label-width="formLabelWidth">
              <el-select
                v-model="outFormModel.sourceWarehouseName"
                clearable
                filterable
                @change="changeSourceWarehouse"
                placeholder="请选择源仓库"
                style="width:420px;"
                size="mini"
              >
                <el-option
                  v-for="item in sourceWarehouseSel"
                  :key="item.key"
                  :label="item.value"
                  :value="item.key"
                ></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="目标仓库" prop="targetWarehouseName" :label-width="formLabelWidth">
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
                style="width:420px;"
                size="mini"
              >
                <el-option
                  v-for="item in newShopSel"
                  :key="item.key"
                  :label="item.value"
                  :value="item.key"
                ></el-option>
              </el-select>
            </el-form-item>

            <!-- <el-form-item label="产品编号:" prop="productId">
            <el-input v-model="outFormModel.productId" placeholder="请输入产品编号" />
            </el-form-item>-->

            <!-- <el-form-item label="产品名称:" prop="productName">
            <el-input v-model="outFormModel.productName" placeholder="请输入产品名称" />
            </el-form-item>-->
            <!-- <el-form-item label="出库数量:" prop="num">
            <el-input
              oninput="value=value.replace(/[^\d.]/g,'')"
              v-model.number="outFormModel.num"
              placeholder="请输入出库数量"
            />
            </el-form-item>-->
            <el-form-item label="备注" :label-width="formLabelWidth">
              <el-input
                placeholder="请输入备注"
                type="textarea"
                :rows="5"
                v-model="outFormModel.remark"
              />
            </el-form-item>
            <el-button
              type="primary"
              @click="addProduct()"
              size="mini"
              style="margin:10px 30px;"
            >添加产品</el-button>
            <el-table border :data="replenishProductTable" style="width: 100%">
              <el-table-column label="产品编号" width="200px" prop="productId"></el-table-column>
              <el-table-column label="产品名称" prop="productName"></el-table-column>

              <el-table-column label="数量" width="120px">
                <template slot-scope="scope">
                  <el-input
                    placeholder="0"
                    size="mini"
                    v-model="scope.row.num" style="width:100px;"
                    oninput="value=value.replace(/[^\d.]/g,'')"
                    clearable
                  ></el-input>
                </template>
              </el-table-column>
            </el-table>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button type="primary" @click="outSave('outFormModel')" size="mini">保存</el-button>
        </template>
      </rg-dialog>
    </section>
    <!-- 创建出库申请单 -->
    <!-- 产品检索页面 -->
    <section id="selectProduct">
      <rg-dialog
        :title="selectProductFormTitle"
        :visible.sync="selectProductFormVisible"
        :beforeClose="selectProductCancel"
        :btnCancel="{label:'关闭',click:(done)=>{selectProductCancel()}}"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="800px"
        minWidth="600px"
      >
        <template v-slot:content>
          <el-form :inline="true">
            <el-form-item label="产品名称">
              <el-input
                placeholder="请输入产品名称"
                style="width:300px;"
                v-model="productCondition.key"
                size="mini"
              />
            </el-form-item>

            <el-form-item>
              <el-button
                type="primary"
                icon="el-icon-search"
                @click="searchProduct()"
                size="mini"
              >搜索</el-button>
            </el-form-item>
          </el-form>

          <el-table
            border
            ref="multipleTable"
            @selection-change="handleSelectionChange"
            :data="productTableData"
            style="width: 100%"
          >
            <el-table-column type="selection"></el-table-column>

            <el-table-column label="产品编号" prop="productCode"></el-table-column>
            <el-table-column label="产品名称" prop="name"></el-table-column>
            <el-table-column label="品牌" prop="brand" width="120px"></el-table-column>

            <el-table-column label="单位" prop="unit" width="55px"></el-table-column>
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button type="primary" @click="selectProductSave()" size="mini">保存</el-button>
        </template>
      </rg-dialog>
    </section>
    <!-- 产品检索页面 -->
    <!-- 驳回申请子单页面 -->
    <section id="rejectTask">
      <rg-dialog
        :title="rejectFormTitle"
        :visible.sync="rejectFormVisible"
        :beforeClose="rejectCancel"
        :btnCancel="{label:'关闭',click:(done)=>{rejectCancel()}}"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="800px"
        minWidth="600px"
      >
        <template v-slot:content>
          <el-form :model="rejectFormModel" ref="rejectFormModel">
            <el-form-item label="理由" :label-width="formLabelWidth">
              <el-input
                type="textarea"
                :rows="5"
                placeholder="请输入理由"
                v-model="rejectFormModel.rejectReason"
              ></el-input>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button type="primary" @click="rejectSave('rejectFormModel')" size="mini">保存</el-button>
        </template>
      </rg-dialog>
    </section>
    <!-- 删除采购子单页面 -->

    <!-- 选择出库产品界面 -->
    <section id="outStockDialog">
      <rg-dialog
        :title="outStockFormTitle"
        :visible.sync="outStockFormVisible"
        :beforeClose="outStockCancel"
        :btnCancel="{label:'关闭',click:(done)=>{outStockCancel()}}"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="900px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-table border :data="outstockTableData" style="width: 100%">
            <el-table-column label="库存编号" width="85px" prop="id"></el-table-column>
            <el-table-column label="产品编号" prop="productId"></el-table-column>
            <el-table-column label="产品名称" width="180px" prop="productName"></el-table-column>
            <el-table-column label="可用库存" width="85px" prop="availableNum"></el-table-column>
            <el-table-column label="剩余申请量" width="85px" prop="applyNum"></el-table-column>
            <el-table-column label="批次已申请数量" width="85px" prop="alreadyOutNum"></el-table-column>

            <!-- <el-table-column
            label="良品类型"
            width="85px"
            prop="stockTypeName"
            ></el-table-column>-->
            <el-table-column label="批次号" width="85px" prop="batchId"></el-table-column>

            <!-- <el-table-column label="周期" width="95px" prop="weekYear"></el-table-column> -->
            <!-- <el-table-column label="创建时间" width="95px" prop="createTime"></el-table-column> -->

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
        </template>
        <template v-slot:footer>
          <el-button type="primary" @click="outStockSave()" size="mini">保存</el-button>
        </template>
      </rg-dialog>
    </section>
    <!-- 入库收货界面 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/wms/replenishtask";
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
        // productId: undefined,
        // productName: undefined,
        // num: undefined,
        remark: undefined,
        taskStatus: undefined,
        productStatus: undefined,
        taskType: undefined,
        products: []
      },

      outFormModelInit: {
        id: undefined,
        taskId: undefined,
        sourceWarehouseName: undefined,
        targetWarehouseName: undefined,
        sourceWarehouse: undefined,
        targetWarehouse: undefined,
        // productId: undefined,
        // productName: undefined,
        // num: undefined,
        remark: undefined,
        taskStatus: undefined,
        productStatus: undefined,
        taskType: undefined,
        products: []
      },
      replenishProductTable: [],
      replenishProductModel: {
        productId: undefined,
        productName: undefined,
        num: undefined
      },
      replenishProductModelInit: {
        productId: undefined,
        productName: undefined,
        num: undefined
      },

      submitProductModel: {
        productId: undefined,
        productName: undefined,
        num: undefined
      },
      submitProductModelInit: {
        productId: undefined,
        productName: undefined,
        num: undefined
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
      //产品搜索的model
      productTableData: [],
      productCondition: {
        key: undefined
      },
      selectProductFormTitle: undefined,
      selectProductFormVisible: false,
      //产品搜索的model

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

        num: [{ required: true, message: "请输入申请数量", trigger: "blur" }],
        taskType: [
          { required: true, message: "请选择申请类型", trigger: "blur" }
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
    toggleSelection(rows) {
      if (rows) {
        rows.forEach(row => {
          this.$refs.multipleTable.toggleRowSelection(row);
        });
      } else {
        this.$refs.multipleTable.clearSelection();
      }
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    addProduct() {
      this.selectProductFormTitle = "查询产品";
      this.selectProductFormVisible = true;
      //this.productCondition.productName = "金冷 冷冻油 R-134a 汽车空调压";
      // appSvc
      //   .productSearch(this.productCondition)
      //   .then(
      //     res => {
      //       debugger;
      //       var resData = res.data;

      //       this.productTableData = resData.items;
      //     },
      //     error => {
      //       console.log(error);
      //     }
      //   )
      //   .catch(() => {});
    },
    searchProduct() {
      if (
        this.productCondition.key != null &&
        this.productCondition.key != ""
      ) {
        appSvc
          .searchProduct(this.productCondition)
          .then(
            res => {
              debugger;
              var resData = res.data;

              this.productTableData = resData.items;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      } else {
        this.$message({
          message: "请输入产品名称!",
          type: "warning"
        });
      }
    },

    selectProductCancel() {
      this.productCondition.key = undefined;
      this.selectProductFormVisible = false;
      this.productTableData = [];
    },

    //输入产品名称 检索产品编号
    selectProductSave() {
      //只能选择一个产品
      var productCode = "";
      var productName = "";
      if (this.$refs.multipleTable.selection.length > 0) {
        for (var i = 0; i < this.$refs.multipleTable.selection.length; i++) {
          productCode = this.$refs.multipleTable.selection[i].productCode;

          var existProduct = this.replenishProductTable.find(r => {
            return r.productId === productCode;
          });
          if (existProduct != undefined) {
            continue;
          }

          productName = this.$refs.multipleTable.selection[i].name;

          this.replenishProductModel.productId = productCode;
          this.replenishProductModel.productName = productName;

          this.replenishProductTable.push(this.replenishProductModel);

          this.replenishProductModel = JSON.parse(
            JSON.stringify(this.replenishProductModelInit)
          );
        }

        this.selectProductFormVisible = false;
        this.productCondition.key = undefined;
        this.productTableData = [];
      } else {
        this.$message({
          message: "请选择产品!",
          type: "warning"
        });
      }
    },

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
            .getShopList(this.shopnewCondition)
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
      this.outStockFormVisible = true;
      this.outStockFormTitle="【"+row.productName+"】申请出库";
      const loading = this.$loading({
        lock: true,
        text: "拼命处理中......",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)"
      });

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
            
          },
          error => {
            console.log(error);
          }
        )
        .catch(error => {
          console.log(error);
        })
        .finally(() => {
          loading.close();
        });
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
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
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
        this.outFormModel.taskType = row.taskType;
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
      // if (Number(this.outFormModel.num) <= 0) {
      //   this.$message({
      //     message: "申请数量必须大于0",
      //     type: "warning"
      //   });
      //   return;
      // }

      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var isAll = false;
          for (var i = 0; i < this.replenishProductTable.length; i++) {
            if (
              this.replenishProductTable[i].num != undefined &&
              parseInt(this.replenishProductTable[i].num) > 0
            ) {
              isAll = true;
              break;
            } else {
              continue;
            }
          }

          if (!isAll) {
            this.$message({
              message: "请填写申请数量!",
              type: "warning"
            });
          }

          var tempArr = [];
          for (var i = 0; i < this.replenishProductTable.length; i++) {
            var item = this.replenishProductTable[i];

            this.submitProductModel.productId = item.productId;
            this.submitProductModel.productName = item.productName;
            this.submitProductModel.num =
              item.num != undefined && parseInt(item.num) > 0
                ? parseInt(item.num)
                : 0;

            tempArr.push(this.submitProductModel);
            this.submitProductModel = JSON.parse(
              JSON.stringify(this.submitProductModelInit)
            );
          }

          this.outFormModel.products = tempArr;
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
                    this.shopcondition.searchLocationId = undefined;
                    this.newShopSel = [];
                    this.replenishProductTable = [];
                    vm.search();

                    vm.outCancel();
                  }
                })
                .catch(error => {
                  console.log(error);
                });
            } else {
              return false;
            }
          });
        })
        .catch(() => {});
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
      this.shopcondition.searchLocationId = undefined;
      this.newShopSel = [];
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

      const loading = this.$loading({
        lock: true,
        text: "拼命处理中......",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)"
      });
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
        })
        .finally(() => {
          loading.close();
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
