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
        <!-- <el-pagination
            background
            :page-size="10"
            :page-sizes="[10, 30, 50, 100]"
            layout="total, sizes, prev, pager, next, jumper"
            :total="totalList"
            @current-change="pageTurning"
            :current-page.sync="currentPage"
            @size-change="sizeChange"
        ></el-pagination>-->
        <template v-slot:condition>
          <el-form-item>
            <el-input
              v-model="condition.id"
              clearable
              @change="updateAsnId()"
              placeholder="请输入申请单号"
            />
          </el-form-item>

          <el-form-item>
            <el-input
              v-model="condition.asnNo"
              clearable
              @change="updateAsnNo()"
              placeholder="请输入关联单号"
            />
          </el-form-item>
          <el-form-item>
            <el-input
              v-model="condition.refNo"
              clearable
              placeholder="请输入外联单号"
              @change="updateRefNo()"
              style="width:150px;"
            />
          </el-form-item>
          <el-form-item>
            <el-input v-model="condition.productId" clearable placeholder="请输入产品名称或编码" />
          </el-form-item>
          <el-form-item>
            <el-select v-model="condition.venderId" clearable filterable placeholder="请选择供应商">
              <el-option
                v-for="item in venderSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select v-model="condition.warehouseId" clearable filterable placeholder="请选择仓库">
              <el-option
                v-for="item in warehouseSel"
                :key="item.id"
                :label="item.warehouseName"
                :value="item.id"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select v-model="condition.status" clearable filterable placeholder="请选择状态">
              <el-option
                v-for="item in statusSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select v-model="condition.asnType" clearable filterable placeholder="请选择入库类型">
              <el-option
                v-for="item in asnTypeSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <!-- <el-input v-model="condition.name" clearable class="el-input el-input--suffix" placeholder="请输入系统名称"></el-input> -->
          <!-- <el-button
            type="primary"
            size="small"
            style="margin-left:30px;"
            @click="search(true)"
            icon="el-icon-search"
          >搜索</el-button>-->
          <!-- <el-button
            type="primary"
            size="small"
            @click="showCreate"
          >新增</el-button>-->
        </template>

        <template v-slot:tb_cols>
          <!-- <el-table
              border
              :data="tableData"
              @expand-change="expandSelect"
              @row-click="clickRow"
              style="width: 100%"
              :cell-style="cellStyle"
          >-->

          <el-table-column type="expand">
            <template slot-scope="props">
              <el-table
                border
                :data="props.row.products"
                :cell-style="cellStyle"
                style="width: 100%"
              >
                <rg-table-column label="产品单号" prop="id" />
                <rg-table-column label="产品编号" prop="productId" />
                <rg-table-column label="产品名称" prop="productName" />
                <rg-table-column label="状态" width="100px" prop="productStatus" align="center"/>
                <rg-table-column label="采购数量" prop="num" is-number/>
                <rg-table-column label="成本单价" prop="cost" is-money/>
                <rg-table-column label="外联单号" prop="refNo" />
              </el-table>
            </template>
          </el-table-column>
          <rg-table-column label="申请单号" prop="asnId" width="80px;" align="center"/>
          <rg-table-column label="关联单号" prop="asnNo" align="center"/>
          <rg-table-column label="入库类型" prop="asnType" align="center"/>
          <rg-table-column label="状态" prop="asnStatus" align="center"/>

          <rg-table-column label="仓库" prop="location" align="center"/>
          <rg-table-column label="供应商" prop="sender" align="center"/>

          <el-table-column label="应入库" align="center">
            <rg-table-column label="总数" prop="inStockTotalNum"  is-number/>
            <rg-table-column label="总额" prop="inStockTotalPrice" is-money/>
          </el-table-column>

          <el-table-column label="实际入库" align="center">
            <rg-table-column label="总数" prop="actualInStockTotalNum" is-number/>
            <rg-table-column label="总额" prop="actualInStockTotalPrice" is-money/>
          </el-table-column>

          <el-table-column label="到达时间" align="center">
            <rg-table-column label="最早" prop="earliestArrivalTime" is-datetime/>
            <rg-table-column label="最晚" prop="latestArrivalTime" is-datetime/>
          </el-table-column>

          <rg-table-column label="采购人" prop="createBy" align="center"/>
          <rg-table-column label="创建时间" prop="createTime" is-datetime/>
          <el-table-column label="操作" width="160" fixed="right" align="center">
            <template slot-scope="scope">
              <!-- 签收和入库是两个概念 -->
              <el-button-group>
              <el-button
                size="mini"
                type="primary"
                :disabled="scope.row.isSign == true"
                @click="asnSign(scope.row)"
              >签收</el-button>
              <el-button
                size="mini"
                type="success"
                :disabled="scope.row.isInStock == true"
                @click="asnInStock(scope.row)"
              >入库</el-button>

              <el-button size="mini" type="text" @click="viewAsnHistory(scope.row)">操作历史</el-button>
              </el-button-group>

            </template>
          </el-table-column>
        </template>
      </rg-page>
    </section>
    <!-- 首页 -->

    <!-- 入库收货界面 -->
    <section id="inStockDialog">
      <rg-dialog
        :title="inStockFormTitle"
        :visible.sync="inStockFormVisible"
        v-if="inStockFormVisible"
        :beforeClose="inStockCancel"
        :btnCancel="{label:'关闭',click:(done)=>{inStockCancel()}}"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="900px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-table border :data="instockTableData" style="width: 100%">
            <el-table-column label="入库单号" width="85px" prop="id" align="center"></el-table-column>
            <el-table-column label="产品编号" prop="productId" align="center"></el-table-column>
            <el-table-column label="产品名称" prop="productName" align="center"></el-table-column>
            <el-table-column label="数量" align="center">
              <el-table-column label="应入" width="85px" prop="receiveNum"></el-table-column>
              <el-table-column label="已入" width="85px" prop="actualNum"></el-table-column>
              <el-table-column label="差异" width="85px" prop="diffNum"></el-table-column>
            </el-table-column>
            <el-table-column label="外联单号" width="95px" prop="refNo" align="center"></el-table-column>
            <el-table-column width="120px" prop="inStockNum" label="操作" align="center">
              <template slot-scope="scope">
                <div
                  v-show="
                  scope.row.actualNum + scope.row.diffNum >=
                  scope.row.receiveNum
                    ? false
                    : true
                "
                >
                  <el-input
                    placeholder="请输入数量"
                    v-model="scope.row.inStockNum"
                    oninput="value=value.replace(/[^\d.]/g,'')"
                    @change="distinctReport(scope.row, 'change')"
                    clearable
                    size="mini"
                  ></el-input>
                  <el-button
                    size="mini"
                    type="primary"
                    @click="distinctReport(scope.row, 'click')"
                    style="margin-top:5px;"
                  >差异上报</el-button>
                  <span v-show="scope.row.diffNum > 0 ? true : false">
                    <el-checkbox @click="inStockFinish(this)" v-model="scope.row.isFinish">入库结束</el-checkbox>
                  </span>
                </div>
              </template>
            </el-table-column>
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button type="primary" @click="inStockSave()" size="mini">保存</el-button>
        </template>
      </rg-dialog>
    </section>
    <!-- 入库收货界面 -->
    <!-- 差异上报页面 -->

    <section id="receiptDiff">
      <rg-dialog
        :title="receiptDiffFormTitle"
        :visible.sync="receiptDiffFormVisible"
        v-if="receiptDiffFormVisible"
        :beforeClose="receiptDiffCancel"
        :btnCancel="{label:'关闭',click:(done)=>{receiptDiffCancel('receiptDiffFormModel')}}"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-form :model="receiptDiffFormModel" ref="receiptDiffFormModel">
            <el-row>
              <el-col :span="16">
                <el-form-item label="差异类型" :label-width="formLabelWidth">
                  <el-select
                    v-model="receiptDiffFormModel.diffType"
                    style="width:100%;"
                    clearable
                    filterable
                    placeholder="请选择差异类型"
                  >
                    <el-option
                      v-for="item in receiptDiffSel"
                      :key="item.key"
                      :label="item.value"
                      :value="item.key"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item label="差异数量" :label-width="formLabelWidth">
                  <el-input
                    clearable
                    oninput="value=value.replace(/[^\d.]/g,'')"
                    placeholder="请输入差异数量"
                    v-model="receiptDiffFormModel.num"
                  ></el-input>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item label="理由" size="50" :label-width="formLabelWidth">
                  <el-input
                    type="textarea"
                    :rows="5"
                    placeholder="请输入理由"
                    v-model="receiptDiffFormModel.reason"
                  ></el-input>
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button type="primary" @click="receiptDiffSave('receiptDiffFormModel')" size="mini">保存</el-button>
        </template>
      </rg-dialog>
    </section>
    <!-- 差异上报页面 -->
    <!-- 采购发货页面 -->
    <section id="deliverySection">
      <rg-dialog
        :title="deliveryFormTitle"
        :visible.sync="deliveryFormVisible"
        v-if="deliveryFormVisible"
        :beforeClose="deliveryCancel"
        :btnCancel="{label:'关闭',click:(done)=>{deliveryCancel('deliveryFormModel')}}"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-table border :data="deliveryTableData" :cell-style="cellStyle" style="width: 100%">
            <el-table-column label="物流公司" prop="deliveryCompany" align="center"></el-table-column>
            <el-table-column label="物流单号" prop="deliveryCode" align="center"></el-table-column>
            <el-table-column label="物流电话" prop="deliveryTel" align="center"></el-table-column>
            <el-table-column label="包裹重量" prop="deliveryWeight" align="center"></el-table-column>
            <el-table-column label="物流费用" prop="deliveryFee" align="center"></el-table-column>
            <el-table-column label="备注" prop="remark" align="center"></el-table-column>
            <el-table-column label="操作" width="200" align="center">
              <template slot-scope="scope" align="center">
                <el-button
                  size="mini"
                  type="primary"
                  :disabled="scope.row.status == '已签收'?true:false"
                  @click="packageSign(scope.row)"
                >签收</el-button>
              </template>
            </el-table-column>
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button type="warning" @click="deliverySign('deliveryFormModel')" size="mini">全部签收</el-button>
        </template>
      </rg-dialog>
    </section>
    <!-- 采购发货页面 -->

    <!-- Asn操作历史 -->
    <section id="viewAsnHistory">
      <rg-dialog
        :title="viewAsnHistoryFormTitle"
        :visible.sync="viewAsnHistoryFormVisible"
        v-if="viewAsnHistoryFormVisible"
        :beforeClose="viewAsnHistoryCancel"
        :btnCancel="{label:'关闭',click:(done)=>{viewAsnHistoryCancel()}}"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="700px"
      >
        <!-- <el-table border :data="purchaseorderHistoryTable" style="width: 100%">
          <el-table-column label="编号" prop="no"></el-table-column>
          <el-table-column label="备注" prop="remark"></el-table-column>
          <el-table-column label="创建人" prop="createBy"></el-table-column>
          <el-table-column label="创建时间" prop="createTime"></el-table-column>
        </el-table>-->
          <template v-slot:content>

        <div class="block">
          <el-timeline>
            <el-timeline-item
              v-for="(activity, index) in asnHistoryTable"
              :key="index"
              :icon="eltimelineicon"
              :type="eltimelinetype"
              :size="eltimelinesize"
              :timestamp="activity.createTime"
            >{{activity.createBy}} {{activity.remark}}</el-timeline-item>
          </el-timeline>
        </div>
          </template>
      </rg-dialog>
    </section>
    <!-- Asn操作历史 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/wms/asn";
import { appWarehouseSvc } from "@/api/wms/warehouse";
import { isNumber } from "util";
export default {
  data() {
    return {
       expands: [],
      getRowKeys(row) {
        return row.asnId;
      },
      pickerOptionsStart: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        },
      },
      input: undefined,
      eltimelineicon: "el-icon-more",
      eltimelinetype: "primary",
      eltimelinesize: "large",
      actualInStockNum: 1,
      readonly: true,
      tableLoading: false,
      currentPage: 1,
      //flag: this.global.deletedFlag,
      //下拉框的条件

      statusSelCondition: {
        RequestType: 1,
      },
      asnTypeSelCondition: {
        RequestType: 2,
      },
      venderSelCondition: {
        RequestType: 3,
      },
      receiptDiffSel: [],
      receiptDiffSelCondition: {
        RequestType: 4,
      },
      receiptDiffFormTitle: "入库差异上报",
      receiptDiffFormVisible: false,

      instockTableData: [],
      inStockFormTitle: "入库详情",
      inStockFormVisible: false,
      //编辑条件
      editCondition: {
        id: undefined,
      },

      receiptDiffFormModel: {
        diffType: undefined,
        reason: undefined,
        num: undefined,
        productId: undefined,
        productName: undefined,
        //asn表的主键
        asnId: undefined,
        //asnproduct的主键
        id: undefined,
      },
      receiptDiffFormModelInit: {
        diffType: undefined,
        reason: undefined,
        num: undefined,
        productId: undefined,
        productName: undefined,
        //asn表的主键
        asnId: undefined,
        //asnproduct的主键
        id: undefined,
      },

      //table页面的条件
      condition: {
        venderId: undefined,

        status: undefined,

        shipmentType: undefined,
        pageIndex: 1,
        pageSize: 10,
        //产品编号
        productId: undefined,
        asnId: undefined,
        //采购总单号
        asnNo: undefined,
        //采购子单号
        refNo: undefined,
        warehouseId: undefined,
        asnType: undefined,
      },

      deleteCondition: {
        id: undefined,
        poId: undefined,
      },

      venderSel: [],

      statusSel: [],

      warehouseSel: [],
      asnTypeSel: [],

      asnSignCondition: {
        id: undefined,
      },

      viewAsnHistoryFormTitle: undefined,
      viewAsnHistoryFormVisible: false,
      asnHistoryTable: [],
      asnHistoryCondition: {
        objectType: undefined,
        objectId: undefined,
      },

      asnHistoryConditionInit: {
        objectType: undefined,
        objectId: undefined,
      },

      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",

      formLabelWidth: "120px",
      deliveryFormTitle: undefined,
      deliveryFormVisible: false,
      deliveryTableData: [],
      deliveryCondition: {
        purchaseId: undefined,
        purchaseType: undefined,
      },
      deliveryConditionInit: {
        purchaseId: undefined,
        purchaseType: undefined,
      },
      packagesignModel: {
        purchaseId: undefined,
        purchaseType: undefined,
        asnId: undefined,
        deliveryCode: undefined,
        updateType: undefined,
      },
      packagesignModelInit: {
        purchaseId: undefined,
        purchaseType: undefined,
        asnId: undefined,
        deliveryCode: undefined,
        updateType: undefined,
      },
    };
  },
  computed: {
    routeComputed: {
      get: function () {
        return this.formModel.route;
      },
      set: function (val) {
        this.formModel.route = val.toLowerCase();
      },
    },
  },
  created() {
    //页面初始化函数
    this.fetchData();
  },
  methods: {
    inStockFinish(obj) {
      debugger;
    },
    viewAsnHistoryCancel() {
      this.viewAsnHistoryFormVisible = false;
      this.asnHistoryCondition = JSON.parse(
        JSON.stringify(this.asnHistoryConditionInit)
      );
    },

    viewAsnHistory(row) {
      this.viewAsnHistoryFormTitle = "Asn【" + row.asnId + "】操作历史";
      this.viewAsnHistoryFormVisible = true;
      this.asnHistoryCondition.objectType = "Asn";
      this.asnHistoryCondition.objectId = row.asnId;

      appSvc
        .getWmsLogs(this.asnHistoryCondition)
        .then(
          (res) => {
            this.asnHistoryTable = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    cellStyle(row, column, rowIndex, columnIndex) {
      //根据报警级别显示颜色
      // console.log(row);
      // console.log(row.column);
      if (row.row.asnStatus === "已取消" && row.column.label === "状态") {
        return "color:red";
      }

      if (row.row.productStatus === "已取消" && row.column.label === "状态") {
        return "color:red";
      }
      // else if(row.column.label==="告警级别"&& row.row.alarmLevel==="一般告警" ){
      //   return 'color:yellow'
      // }
    },
    //按照包裹维度签收
    packageSign(row) {
      this.packagesignModel.deliveryCode = row.deliveryCode;
      this.packagesignModel.updateType = 1;
      var vm = this;
      appSvc
        .updatePurchaseOrderDeliveryStatusCommon(this.packagesignModel)
        .then(
          (res) => {
            this.$message({
              message: res.message,
              type: "success",
            });

            if (res.data == "reloadPage") {
              vm.search();

              this.deliveryFormVisible = false;
              this.deliveryCondition = JSON.parse(
                JSON.stringify(this.deliveryConditionInit)
              );
              this.packagesignModel = JSON.parse(
                JSON.stringify(this.packagesignModelInit)
              );
            } else if (res.data == "reloadData") {
              row.status = "已签收";
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    //包裹整单签收
    deliverySign() {
      this.packagesignModel.updateType = 2;

      var vm = this;
      appSvc
        .updatePurchaseOrderDeliveryStatusCommon(this.packagesignModel)
        .then(
          (res) => {
            debugger;
            if ((res.code = 10000)) {
              this.$message({
                message: res.message,
                type: "success",
              });
            }
            vm.search();

            this.deliveryFormVisible = false;
            this.deliveryCondition = JSON.parse(
              JSON.stringify(this.deliveryConditionInit)
            );
            this.packagesignModel = JSON.parse(
              JSON.stringify(this.packagesignModelInit)
            );
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    //asn签收 展示物流信息
    asnSign(row) {
      //提前赋值
      this.packagesignModel.purchaseId = row.asnNo;
      this.packagesignModel.purchaseType = "采购单";
      this.packagesignModel.asnId = row.asnId;

      this.deliveryCondition.purchaseId = row.asnNo;
      this.deliveryCondition.purchaseType = "采购单";
      this.deliveryFormVisible = true;
      this.deliveryFormTitle = "签收采购单【" + row.asnNo + "】";
      appSvc
        .getPurchaseOrderDeliveries(this.deliveryCondition)
        .then(
          (res) => {
            debugger;
            this.deliveryTableData = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});

      // var vm = this;
      // this.asnSignCondition.id = row.asnId;
      // this.$confirm("是否要签收入库申请单【 " + row.asnId + " 】！", "信息", {
      //   confirmButtonText: "确定",
      //   cancelButtonText: "取消",
      //   type: "info"
      // })
      //   .then(() => {
      //     appSvc
      //       .asnSign(this.asnSignCondition)
      //       .then(
      //         res => {
      //           if (res.code == 10000) {
      //             this.$message({
      //               message: res.message,
      //               type: "success"
      //             });
      //             vm.search();
      //             // vm.getConditionData();
      //             vm.cancel();
      //           } else {
      //             this.$message({
      //               message: res.message,
      //               type: "warning"
      //             });
      //           }
      //         },
      //         error => {
      //           console.log(error);
      //         }
      //       )
      //       .catch(() => {});
      //   })
      //   .catch(() => {});
    },

    deliveryCancel(formName) {
      this.deliveryFormVisible = false;
      this.deliveryCondition = JSON.parse(
        JSON.stringify(this.deliveryConditionInit)
      );
    },

    distinctReport(row, type) {
      debugger;
      if (type == "change") {
        //本次入库数+已入库数<=应收数
        if (Number(row.inStockNum) + row.actualNum <= row.receiveNum) {
          return;
        } else {
          //自动触发警告
          this.$confirm("是否要上报差异！", "信息", {
            confirmButtonText: "确定",
            cancelButtonText: "取消",
            type: "warning",
          })
            .then(() => {
              //提前把赋值
              this.receiptDiffFormModel.productId = row.productId;
              this.receiptDiffFormModel.productName = row.productName;
              this.receiptDiffFormModel.asnId = row.asnId;
              this.receiptDiffFormModel.id = row.id;

              this.receiptDiffFormVisible = true;

              appSvc
                .getBasicInfoList(this.receiptDiffSelCondition)
                .then(
                  (res) => {
                    debugger;
                    this.receiptDiffSel = res.data;
                  },
                  (error) => {
                    console.log(error);
                  }
                )
                .catch(() => {});
            })
            .catch((error) => {
              console.log(error);
            });
        }
      } else if (type == "click") {
        //自动触发警告
        this.$confirm("是否要上报差异！", "信息", {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning",
        })
          .then(() => {
            //提前把赋值
            this.receiptDiffFormModel.productId = row.productId;
            this.receiptDiffFormModel.productName = row.productName;
            this.receiptDiffFormModel.asnId = row.asnId;
            this.receiptDiffFormModel.id = row.id;

            this.receiptDiffFormVisible = true;

            appSvc
              .getBasicInfoList(this.receiptDiffSelCondition)
              .then(
                (res) => {
                  debugger;
                  this.receiptDiffSel = res.data;
                },
                (error) => {
                  console.log(error);
                }
              )
              .catch(() => {});
          })
          .catch((error) => {
            console.log(error);
          });
      }
    },

    reportReceiptDiff(row) {},

    //获取待入库列表
    asnInStock(row) {
      this.inStockFormVisible = true;
      this.asnSignCondition.id = row.asnId;
      appSvc
        .getAsnReceiptProducts(this.asnSignCondition)
        .then(
          (res) => {
            debugger;
            this.instockTableData = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    updateAsnId() {
      if (this.condition.id == "") {
        this.condition.id = undefined;
      }
    },
    updateAsnNo() {
      if (this.condition.asnNo == "") {
        this.condition.asnNo = undefined;
      }
    },
    updateRefNo() {
      if (this.condition.refNo == "") {
        this.condition.refNo = undefined;
      }
    },

    expandSelect(row, expandedRows) {
      // expandedRows.length != 0 ? console.log(row.id) : ''
      if (expandedRows.length != 0) {
        //在tableData中循环

        let obj = {};
        obj = this.tableData.find((item) => {
          return item.asnId === row.asnId;
        });

        row.products = obj.products;
      }
    },
    clickRow(row, column, event) {
      console.log(row.id);
      // console.log(column)
      // console.log(event)
    },
    //获取供应商信息
    getVenderData() {
      appSvc
        .getBasicInfoList(this.venderSelCondition)
        .then(
          (res) => {
            this.venderSel = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    //获取目标仓
    getwarehouses() {
      appWarehouseSvc
        .getWarehouses()
        .then(
          (res) => {
            this.warehouseSel = res.data;
            
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    //获取采购状态
    getAsnStatus() {
      appSvc
        .getBasicInfoList(this.statusSelCondition)
        .then(
          (res) => {
            this.statusSel = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    //获取付款状态
    getAsnType() {
      appSvc
        .getBasicInfoList(this.asnTypeSelCondition)
        .then(
          (res) => {
            this.asnTypeSel = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    fetchData() {
      this.tableLoading=true;
      // this.listLoading = true;
      this.getVenderData();
      //this.getPurchaseModes();
      // this.getPurchaseStatus();
      // this.getShipmentType();
      // this.getPayStatus();
      this.getwarehouses();
      this.getAsnStatus();
      this.getAsnType();
      // this.getConditionData();

      appSvc
        .getAsnList(this.condition)
        .then(
          (res) => {
            //  this.tableData = res.data;
            var data = res.data;

            this.tableData = data.asnList;
            this.totalList = data.totalItems;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
          .finally(() => {
            this.tableLoading=false;
          });
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
    getPaged(flag) {
      this.tableLoading = true;

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getAsnList(this.condition)
        .then(
          (res) => {
            var data = res.data;
            this.tableData = data.asnList;
            this.totalList = data.totalItems;

            if (flag) {
              if (res.message != "") {
                this.$message({
                  message: res.message,
                  type: "success",
                });
              }
            }
          },
          (error) => {
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
    inStockSave(formName) {
      var vm = this;
      console.log("formModel: " + JSON.stringify(this.instockTableData));

      //做数据Check
      var stockTable = this.instockTableData;
      for (var i = 0; i < stockTable.length; i++) {
        if (stockTable[i].inStockNum > 0) {
          if (
            Number(stockTable[i].inStockNum) + Number(stockTable[i].actualNum) >
            Number(stockTable[i].receiveNum)
          ) {
            debugger;
            this.$message({
              message:
                "入库产品单[" +
                stockTable[i].id +
                "]的本次入库数[" +
                stockTable[i].inStockNum +
                "] +已入数[" +
                stockTable[i].actualNum +
                "]大于应入数[" +
                stockTable[i].receiveNum +
                "]",
              type: "warning",
            });
            return;
          }
        }
      }

      this.$confirm(
        "是否要保存入库申请单【 " + this.asnSignCondition.id + " 】！",
        "信息",
        {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "info",
        }
      )
        .then(() => {
          const loading = this.$loading({
            lock: true,
            text: "拼命保存中...",
            spinner: "el-icon-loading",
            background: "rgba(0, 0, 0, 0.7)",
          });
          appSvc
            .createAsnReceiptProduct(this.instockTableData)
            .then((res) => {
              loading.close();
              if (res.code == 10000) {
                this.$message({
                  message: res.message,
                  type: "success",
                });
                this.inStockFormVisible = false;
                vm.search();

                vm.cancel();
              }
            })
            .catch((error) => {
              console.log(error);
            });
        })
        .catch(() => {});
    },

    inStockCancel(formName) {
      this.inStockFormVisible = false;
    },
    //关闭差异上报窗口
    receiptDiffCancel() {
      this.receiptDiffFormVisible = false;
      // this.resetSendForm();
      this.receiptDiffFormModel = JSON.parse(
        JSON.stringify(this.receiptDiffFormModelInit)
      );
      var frmName =
        typeof formName === "string" && formName
          ? formName
          : "receiptDiffFormModel";
      this.$refs[frmName].resetFields();
    },
    
    //保存差异上报记录
    receiptDiffSave(formName) {
      var vm = this;
      console.log("formModel: " + JSON.stringify(this.receiptDiffFormModel));
      if (
        this.receiptDiffFormModel.diffType == "" ||
        this.receiptDiffFormModel.diffType == undefined
      ) {
        this.$message({
          message: "请选择差异类型!",
          type: "warning",
        });
        return;
      }

      if (this.receiptDiffFormModel.num <= 0) {
        this.$message({
          message: "差异数必须大于0",
          type: "warning",
        });
        return;
      }
      var asnId = this.receiptDiffFormModel.asnId;
      appSvc
        .createAsnEndreceiptProduct(this.receiptDiffFormModel)
        .then((res) => {
          if (res.code == 10000) {
            this.$message({
              message: res.message,
              type: "success",
            });
            //vm.search();
            this.receiptDiffFormVisible = false;
            this.receiptDiffFormModel = JSON.parse(
              JSON.stringify(this.receiptDiffFormModelInit)
            );

            //入库详情页的数据重新加载
            this.asnSignCondition.id = asnId;
            appSvc
              .getAsnReceiptProducts(this.asnSignCondition)
              .then(
                (res) => {
                  debugger;
                  this.instockTableData = res.data;
                },
                (error) => {
                  console.log(error);
                }
              )
              .catch(() => {});
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 118px;
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
