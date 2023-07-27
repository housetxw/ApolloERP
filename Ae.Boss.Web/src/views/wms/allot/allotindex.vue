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
      :defaultCollapse="false"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-select
            v-model="condition.sourceWarehouse"
            filterable
            remote
            clearable
            reserve-keyword
            placeholder="请输入源仓"
            :remote-method="getSourceWarehouse"
            :loading="loading"
          >
            <el-option-group
              v-for="group in sourceWarehouseSel"
              :key="group.label"
              :label="group.label"
            >
              <el-option
                v-for="item in group.options"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-option-group>
          </el-select>
        </el-form-item>

        <el-form-item label>
          <el-select
            v-model="condition.targetWarehouse"
            filterable
            remote
            clearable
            reserve-keyword
            placeholder="请输入目标仓"
            :remote-method="getTargetWarehouse"
            :loading="loading"
          >
            <el-option-group
              v-for="group in targetWarehouseSel"
              :key="group.label"
              :label="group.label"
            >
              <el-option
                v-for="item in group.options"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-option-group>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-input
            v-model="condition.productId"
            clearable
            style="width:200px;"
            placeholder="请输入产品名称或编码"
          />
        </el-form-item>

        <el-form-item label>
          <el-tooltip
            class="item"
            effect="dark"
            content="领取日期"
            placement="bottom"
          >
            <el-date-picker
              v-model="condition.times"
              type="daterange"
              :picker-options="$root.$data.timeRgPickerOpt"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              align="center"
              style="width:210px"
            ></el-date-picker>
          </el-tooltip>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.taskStatus"
            clearable
            filterable
            style="width:160px"
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
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
          >新增</el-button
        >
      </template>
      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <rg-table-column label="编号" prop="taskNo"></rg-table-column>
        <rg-table-column
          label="源仓"
          prop="sourceWarehouseName"
        ></rg-table-column>
        <rg-table-column
          label="目标仓"
          prop="targetWarehouseName"
        ></rg-table-column>
        <rg-table-column label="申请人" prop="operator"></rg-table-column>

        <rg-table-column
          label="申请日期"
          prop="operatorTime"
          is-datetime
          fix-min
        ></rg-table-column>
        <rg-table-column label="状态" prop="taskStatus"></rg-table-column>

        <rg-table-column label="创建人" prop="createBy"></rg-table-column>
        <rg-table-column
          label="创建时间"
          prop="createTime"
          is-datetime
          fix-min
        />

        <rg-table-column label="备注" prop="remark"></rg-table-column>

        <rg-table-column label="操作">
          <template slot-scope="scope">
            <!-- 签收和入库是两个概念 -->
            <el-button
              style="padding:10px 7px;"
              size="mini"
              type="primary"
              @click="viewDetail(scope.row)"
              >详情</el-button
            >
          </template>
        </rg-table-column>
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

    <!-- 创建调拨单页面 -->
    <section id="outstockPage">
      <rg-dialog
        :title="formTitle"
        :visible.sync="formVisible"
        :beforeClose="cancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            cancel('formModel');
          }
        }"
        :btnRemove="{
          label: '取消',
          show:
            this.formModel.taskStatus == '待审核' ||
            this.formModel.taskStatus == '审核未通过'
              ? true
              : false,
          disabled: false,
          click: done => {
            cancelTask('formModel');
          }
        }"
        :footbar="true"
        width="90%"
        v-if="formVisible"
        maxWidth="1024px"
        minWidth="900px"
      >
        <template v-slot:content>
          <el-form
            :model="formModel"
            :disabled="
              formModel.taskStatus == '审核未通过' ||
              formModel.taskStatus == '' ||
              formModel.taskStatus == undefined
                ? false
                : true
            "
            :rules="rules1"
            size="mini"
            ref="formModel"
            label-width="120px"
          >
            <el-row>
              <el-col :span="12">
                <el-form-item label="源仓" prop="sourceWarehouse">
                  <el-select
                    v-model="formModel.sourceWarehouse"
                    filterable
                    remote
                    clearable
                    :disabled="
                      formModel.taskStatus == '' ||
                      formModel.taskStatus == undefined
                        ? false
                        : true
                    "
                    reserve-keyword
                    placeholder="请输入源仓"
                    style="width:220px;"
                    :remote-method="getSourceWarehouseNew"
                    :loading="loading"
                  >
                    <el-option-group
                      v-for="group in sourceWarehouseSelNew"
                      :key="group.label"
                      :label="group.label"
                    >
                      <el-option
                        v-for="item in group.options"
                        :key="item.key"
                        :label="item.value"
                        :value="item.key"
                      ></el-option>
                    </el-option-group>
                  </el-select>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="目标仓" prop="targetWarehouse">
                  <el-select
                    v-model="formModel.targetWarehouse"
                    filterable
                    remote
                    :disabled="
                      formModel.taskStatus == '' ||
                      formModel.taskStatus == undefined
                        ? false
                        : true
                    "
                    clearable
                    reserve-keyword
                    placeholder="请输入目标仓"
                    :remote-method="getTargetWarehouseNew"
                    :loading="loading"
                    style="width:220px;"
                  >
                    <el-option-group
                      v-for="group in targetWarehouseSelNew"
                      :key="group.label"
                      :label="group.label"
                    >
                      <el-option
                        v-for="item in group.options"
                        :key="item.key"
                        :label="item.value"
                        :value="item.key"
                      ></el-option>
                    </el-option-group>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item label="申请人" prop="operator">
                  <el-input
                    v-model="formModel.operator"
                    clearable
                    style="width:220px;"
                    placeholder="请输入申请人姓名和手机号"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="申请时间">
                  <el-date-picker
                    v-model="formModel.operatorTime"
                    type="datetime"
                    placeholder="选择日期时间"
                    default-time="12:00:00"
                    style="width:220px;"
                  ></el-date-picker>
                </el-form-item>
              </el-col>
            </el-row>

            <el-form-item label="备注">
              <el-input
                v-model="formModel.remark"
                clearable
                placeholder="请输入备注说明"
                maxlength="300"
                show-word-limit
              />
            </el-form-item>
            <el-form-item label="经办人">
              <label>{{ formModel.createBy }}</label>
            </el-form-item>
            <el-row>
              <el-button
                type="primary"
                size="mini"
                @click="selectProduct()"
                style="margin:10px 30px;"
                >添加产品</el-button
              >
            </el-row>
          </el-form>
          <el-row>
            <el-table
              border
              :data="productStockTable"
              style="width: 100%"
              stripe
              :disabled="
                formModel.taskStatus == '审核未通过' ||
                formModel.taskStatus == '' ||
                formModel.taskStatus == undefined
                  ? false
                  : true
              "
              element-loading-text="加载中..."
              size="mini"
            >
              <rg-table-column label="产品编号" prop="productId" fix-min />
              <rg-table-column label="产品名称" prop="productName" />
              <rg-table-column label="批次" prop="batchId" fix-min />
              <rg-table-column
                label="成本单价"
                prop="costPrice"
                is-money
                fix-min
              />
              <!-- <rg-table-column label="单位" prop="stockUnit" fix-min /> -->
              <rg-table-column
                label="库存量"
                fix-min
                is-number
                prop="availableNum"
                v-if="
                  formModel.taskStatus == '审核未通过' ||
                  formModel.taskStatus == '' ||
                  formModel.taskStatus == undefined
                    ? true
                    : false
                "
              />
              <rg-table-column label="调拨数量" width="100px">
                <template slot-scope="scope">
                  <el-input
                    placeholder="0"
                    size="mini"
                    :disabled="
                      formModel.taskStatus == '审核未通过' ||
                      formModel.taskStatus == '' ||
                      formModel.taskStatus == undefined
                        ? false
                        : true
                    "
                    v-model="scope.row.num"
                    oninput="value=value.replace(/[^\d.]/g,'')"
                    @blur="checkNum(scope.row)"
                    clearable
                  ></el-input>
                </template>
              </rg-table-column>
              <!-- <rg-table-column label="库存类型">
                <template slot-scope="scope">
                  <label v-if="scope.row.ownerId == -1">平台库存</label>
                  <label v-else>门店库存</label>
                </template>
              </rg-table-column> -->
              <rg-table-column label="单位" prop="unit" />
              <rg-table-column label="操作" align="center" width="70">
                <template slot-scope="scope">
                  <el-button
                    type="danger"
                    size="mini"
                    :disabled="
                      formModel.taskStatus == '审核未通过' ||
                      formModel.taskStatus == '' ||
                      formModel.taskStatus == undefined
                        ? false
                        : true
                    "
                    @click="removeItem(scope.$index, productStockTable)"
                    >移除</el-button
                  >
                </template>
              </rg-table-column>
            </el-table>
          </el-row>
          <!-- <el-form
            style="padding-top:10px;"
            :disabled="formModel.taskStatus == '待审核' ? false : true"
            v-if="formModel.id>0?false:false"
          >
            <el-form-item label="审核意见">
              <el-radio-group v-model="radio">
                <el-radio :label="1">通过</el-radio>
                <el-radio :label="0">不通过</el-radio> </el-radio-group
              >&nbsp;&nbsp;&nbsp;
              <el-input
                v-model="formModel.auditRemark"
                clearable
                :rows="4"
                type="textarea"
                maxlength="300"
                show-word-limit
                placeholder="请输入审核说明"
              />
            </el-form-item>
          </el-form> -->
          <!-- <div
            v-if="
              formModel.taskStatus != '' && formModel.taskStatus != undefined
                ? false
                : false
            "
          >
            <el-divider content-position="left">操作历史</el-divider>

            <div class="block">
              <el-timeline>
                <el-timeline-item
                  v-for="(activity, index) in logData"
                  :key="index"
                  :icon="eltimelineicon"
                  :type="eltimelinetype"
                  :size="eltimelinesize"
                  :timestamp="activity.createTime"
                  >{{ activity.createBy }}
                  {{ activity.remark }}</el-timeline-item
                >
              </el-timeline>
            </div>
          </div> -->
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            :disabled="viewBtn"
            type="primary"
            @click="outsave('formModel')"
            >提交</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 新增入库单页面 -->
    <!-- 选择产品页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        :title="productformTitle"
        :visible.sync="productformVisible"
        :beforeClose="productcancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            productcancel('productformModel');
          }
        }"
        :footbar="true"
        width="100%"
        maxWidth="800px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-form
            :model="productformModel"
            :inline="true"
            ref="productformModel"
          >
            <el-form-item>
              <el-input
                size="mini"
                placeholder="请输入产品名称"
                v-model="productformModel.key"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>

            <el-form-item>
              <el-button
                icon="el-icon-search"
                size="mini"
                type="warning"
                @click="searchproduct()"
                >检索</el-button
              >
            </el-form-item>
          </el-form>
          <el-table
            border
            ref="multipleTable"
            :height="400"
            stripe
            size="mini"
            @selection-change="handleSelectionChange"
            :data="productTableData"
            style="width: 100%"
          >
            <el-table-column type="selection" align="center"></el-table-column>

            <el-table-column
              label="产品编号"
              prop="productCode"
            ></el-table-column>
            <el-table-column label="产品名称" prop="name"></el-table-column>
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="saveProduct()"
            >确定</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 选择产品页面 -->
  </main>
</template>
<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/wms/transfer";
import { Loading } from "element-ui";
import { isNumber } from "util";
export default {
  data() {
    return {
      input: undefined,
      actualInStockNum: 1,
      readonly: true,
      tableLoading: false,
      currentPage: 1,
      //flag: this.global.deletedFlag,
      //下拉框的条件
      eltimelineicon: "el-icon-more",
      eltimelinetype: "primary",
      eltimelinesize: "large",
      taskStatusSelCondition: {
        RequestType: 1
      },

      sourceWarehouseSel: [],
      targetWarehouseSel: [],
      sourceWarehouseSelNew: [],
      targetWarehouseSelNew: [],

      //table页面的条件
      condition: {
        taskstatus: undefined,
        pageIndex: 1,
        pageSize: 10,
        //产品编号
        productName: undefined,
        targetWarehouse: undefined,
        sourceWarehouse: undefined,
        times: []
      },
      radio: 1, //通过radio
      orginSourceValue: undefined,
      orginSource: undefined,
      orginTargetValue: undefined,
      orginTarget: undefined,

      isModify: 0, //初始状态为0  新增为1  修改为2
      shopSel: [],
      taskStatusSel: [],
      loading: false,
      shopSelCondition: {
        simpleName: undefined
      },
      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",
      rules1: {
        sourceWarehouse: [
          { required: true, message: "请选择物流公司", trigger: "blur" }
        ],
        targetWarehouse: [
          { required: true, message: "请输入物流单号", trigger: "blur" }
        ],
        operator: [
          { required: true, message: "请输入物流电话", trigger: "blur" }
        ]
      },
      formLabelWidth: "120px",
      formModel: {
        id: undefined,
        sourceWarehouse: undefined,
        sourceWarehouseName: undefined,
        targetWarehouse: undefined,
        targetWarehouseName: undefined,
        operator: undefined,
        operatorTime: undefined,
        isAudit: undefined,
        auditRemark: undefined,
        remark: undefined,
        products: [],
        createBy: undefined,
        taskStatus: undefined,
        sourceType: undefined,
        targetType: undefined,
        taskNo: undefined
      },

      productStockTable: [],

      productTableData: [],
      productformTitle: "",
      productformVisible: false,
      productformModel: {
        key: undefined
      },
      productStockCondition: {
        pIds: [],
        sourceWarehouse: undefined,
        isValid: undefined,
        id: undefined
      },
      logData: [],
      logCondition: {
        objectId: undefined,
        objectType: undefined
      },
      detailCondition: {
        taskId: undefined
      },
      cancelCondition: {
        taskNo: undefined
      },
      auditModel: {
        id: undefined,
        isAudit: undefined,
        auditRemark: undefined
      },

      viewBtn: false //控制提交按钮的显示
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
    checkNum(row) {
      if (parseInt(row.num) > parseInt(row.availableNum)) {
        row.num = 0;
      }
    },
    removeItem(index, rows) {
      //移除一行

      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          rows.splice(index, 1); //删掉该行
        })
        .catch(error => {
          console.log(error);
        });
    },

    outsave(formName) {
      var vm = this;
      //新增
      if (
        this.formModel.taskStatus == undefined ||
        this.formModel.taskStatus == ""
      ) {
        if (this.productStockTable.length > 0) {
          var existflag = false;
          for (var i = 0; i < this.productStockTable.length; i++) {
            if (
              this.productStockTable[i].num != undefined &&
              parseInt(this.productStockTable[i].num) > 0
            ) {
              existflag = true;
              break;
            }
          }
          if (!existflag) {
            this.$message({
              message: "请填写调拨数量!",
              type: "warning"
            });
            return false;
          } else {
            debugger;
            //从门店->仓库，退货的时候，库存选择必须是带有 批次的
            this.formModel.sourceType = this.formModel.sourceWarehouse.split(
              "-"
            )[1];
            this.formModel.targetType = this.formModel.targetWarehouse.split(
              "-"
            )[1];

            // if (
            //   this.formModel.sourceType == "Shop" &&
            //   this.formModel.targetType == "Warehouse"
            // ) {
            //   //需要check 库存是否包含了货主 == -1的
            //   var checkOwnerFlag = true;

            //   for (var i = 0; i < this.productStockTable.length; i++) {
            //     if (
            //       this.productStockTable[i].num != undefined &&
            //       parseInt(this.productStockTable[i].num) > 0 &&
            //       this.productStockTable[i].ownerId != -1
            //     ) {
            //       checkOwnerFlag = false;
            //       break;
            //     }
            //   }
            //   if (checkOwnerFlag == false) {
            //     this.$message({
            //       message: "从门店调拨到仓库的库存必须是能追踪的!",
            //       type: "warning"
            //     });
            //     return false;
            //   }
            // }

            if (
              this.formModel.sourceType == "Shop" &&
              this.formModel.targetType == "Shop"
            ) {
              //需要check 
              var checkOwnerFlag = false;

              if (checkOwnerFlag == false) {
                this.$message({
                  message: "门店间调拨请到门店系统操作!",
                  type: "warning"
                });
                return false;
              }
            }

            this.$confirm("确定操作吗?", "信息", {
              confirmButtonText: "确定",
              cancelButtonText: "取消",
              type: "warning"
            })
              .then(() => {
                this.$refs[formName].validate(valid => {
                  if (valid) {
                    var vm = this;
                    vm.btnSaveLoading = true;
                    //获取源仓名称
                    var sourceName = "";
                    for (
                      var i = 0;
                      i < this.sourceWarehouseSelNew.length;
                      i++
                    ) {
                      var findFlag = false;
                      for (
                        var j = 0;
                        j < this.sourceWarehouseSelNew[i].options.length;
                        j++
                      ) {
                        if (
                          this.sourceWarehouseSelNew[i].options[j].key ==
                          this.formModel.sourceWarehouse
                        ) {
                          sourceName = this.sourceWarehouseSelNew[i].options[j]
                            .value;
                          findFlag = true;
                          break;
                        }
                      }
                      if (findFlag) {
                        break;
                      }
                    }
                    var targetName = "";
                    //获取目标仓名称
                    for (
                      var i = 0;
                      i < this.targetWarehouseSelNew.length;
                      i++
                    ) {
                      var findFlag = false;
                      for (
                        var j = 0;
                        j < this.targetWarehouseSelNew[i].options.length;
                        j++
                      ) {
                        if (
                          this.targetWarehouseSelNew[i].options[j].key ==
                          this.formModel.targetWarehouse
                        ) {
                          targetName = this.targetWarehouseSelNew[i].options[j]
                            .value;
                          findFlag = true;
                          break;
                        }
                      }
                      if (findFlag) {
                        break;
                      }
                    }
                    var sourceId = this.formModel.sourceWarehouse.split("-")[0];
                    var targetId = this.formModel.targetWarehouse.split("-")[0];

                    this.formModel.sourceWarehouse = sourceId;
                    this.formModel.targetWarehouse = targetId;

                    this.formModel.sourceWarehouseName = sourceName;
                    this.formModel.targetWarehouseName = targetName;
                    
                    for (var i = 0; i < this.productStockTable.length; i++) {
                      if (
                        this.productStockTable[i].num != undefined &&
                        parseInt(this.productStockTable[i].num) > 0
                      ) {
                        this.formModel.products.push(this.productStockTable[i]);
                      }
                    }
                    appSvc
                      .createAllotTask(this.formModel)
                      .then(
                        res => {
                          if (res.code == 10000) {
                            this.$message({
                              message: "操作成功",
                              type: "success"
                            });
                            vm.search();

                            vm.cancel();
                          }
                        },
                        err => {}
                      )
                      .catch(err => {
                        console.error(err);
                      })
                      .finally(() => {
                        vm.btnSaveLoading = false;
                      });
                  } else {
                    return false;
                  }
                });
              })
              .catch(error => {
                console.log(error);
              });
          }
        } else {
          this.$message({
            message: "请选择产品!",
            type: "warning"
          });
          return false;
        }

        //for(this.productStockTable.length;)
      }
      //只能修改审核结果
      else if (this.formModel.taskStatus == "待审核") {
        this.formModel.isAudit = this.radio;
        if (this.formModel.isAudit != undefined) {
          if (
            this.formModel.isAudit == 0 &&
            (this.formModel.auditRemark == undefined ||
              this.formModel.auditRemark == "")
          ) {
            this.$message({
              message: "请填写审核说明!",
              type: "warning"
            });
            return false;
          }

          this.auditModel.id = this.formModel.id;
          this.auditModel.isAudit = this.formModel.isAudit;
          this.auditModel.auditRemark = this.formModel.auditRemark;

          appSvc
            .auditAllotTask(this.auditModel)
            .then(
              res => {
                if (res.code == 10000) {
                  this.$message({
                    message: "操作成功",
                    type: "success"
                  });
                  vm.search();

                  vm.cancel();
                }
              },
              err => {}
            )
            .catch(err => {
              console.error(err);
            })
            .finally(() => {
              vm.btnSaveLoading = false;
            });
        } else {
          this.$message({
            message: "请选择审核结果!",
            type: "warning"
          });
          return false;
        }
      }
      //数据可以修改
      else if (this.formModel.taskStatus == "审核未通过") {
        if (this.productStockTable.length > 0) {
          var existflag = false;
          for (var i = 0; i < this.productStockTable.length; i++) {
            if (
              this.productStockTable[i].num != undefined &&
              parseInt(this.productStockTable[i].num)
            ) {
              existflag = true;
              break;
            }
          }
          if (!existflag) {
            this.$message({
              message: "请填写铺货数量!",
              type: "warning"
            });
            return false;
          } else {
            this.formModel.products = this.productStockTable;

            this.$confirm("确定操作吗?", "信息", {
              confirmButtonText: "确定",
              cancelButtonText: "取消",
              type: "warning"
            })
              .then(() => {
                this.$refs[formName].validate(valid => {
                  if (valid) {
                    this.formModel.sourceWarehouse = undefined;
                    this.formModel.targetWarehouse = undefined;
                    appSvc
                      .createAllotTask(this.formModel)
                      .then(
                        res => {
                          if (res.code == 10000) {
                            this.$message({
                              message: "操作成功",
                              type: "success"
                            });
                            vm.search();

                            vm.cancel();
                          }
                        },
                        err => {}
                      )
                      .catch(err => {
                        console.error(err);
                      })
                      .finally(() => {
                        vm.btnSaveLoading = false;
                      });
                  } else {
                    return false;
                  }
                });
              })
              .catch(error => {
                console.log(error);
              });
          }
        } else {
          this.$message({
            message: "请选择产品!",
            type: "warning"
          });
          return false;
        }
      }
    },

    saveProduct() {
      if (
        this.formModel.sourceWarehouse == "" ||
        this.formModel.sourceWarehouse == undefined
      ) {
        this.$message({
          message: "请选择源仓!",
          type: "warning"
        });
        return false;
      }

      //获取产品查询库存
      var productCode = "";

      var productCodes = [];
      //这部分的sku需要到门店/仓库查询库存
      if (this.$refs.multipleTable.selection.length > 0) {
        for (var i = 0; i < this.$refs.multipleTable.selection.length; i++) {
          productCode = this.$refs.multipleTable.selection[i].productCode;
          productCodes.push(productCode);
        }

        this.productStockCondition.pIds = productCodes;

        if (this.isModify == 1) {
          this.productStockCondition.sourceWarehouse = this.formModel.sourceWarehouse;
          this.productStockCondition.isValid = 1;
        } else if (this.isModify == 2) {
          this.productStockCondition.isValid = 0;
          this.productStockCondition.id = this.formModel.id;
        }
        const loading = this.$loading({
          lock: true,
          text: "拼命处理中......",
          spinner: "el-icon-loading",
          background: "rgba(0, 0, 0, 0.7)"
        });
        appSvc
          .getAllotProductsStock(
            this.$jscom.deepCopy(this.productStockCondition)
          )
          .then(
            res => {
              if (res.data.length > 0) {
                res.data.forEach(r => {
                  this.productStockTable.push({
                    availableNum: r.availableNum,
                    refBatchId: r.refBatchId,
                    batchId: r.batchId,
                    costPrice: r.costPrice,
                    productId: r.productId,
                    productName: r.productName,
                    stockType: r.stockType,
                    stockId: r.id,
                    ownerId: r.ownerId
                  });
                });
              } else {
                this.$message({
                  message: "请选择产品无库存，请重选择产品。",
                  type: "warning"
                });
              }
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
        this.productcancel();
      } else {
        this.$message({
          message: "请选择产品!",
          type: "warning"
        });
        return;
      }
    },

    productcancel() {
      this.productformVisible = false;
      this.$refs.multipleTable.clearSelection();
      this.productTableData = [];
      Object.assign(
        this.$data.productformModel,
        this.$options.data().productformModel
      );
      Object.assign(
        this.$data.productStockCondition,
        this.$options.data().productStockCondition
      );
    },

    selectProduct() {
      this.productformTitle = "选择产品";
      this.productformVisible = true;
    },

    searchproduct() {
      // this.tableLoading = true;
      const loading = this.$loading({
        lock: true,
        text: "拼命处理中......",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)"
      });
      appSvc
        .searchProduct(this.productformModel || {})
        .then(
          res => {
            this.productTableData = res.data.items;
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
    //取消调拨单
    cancelTask() {
      if (this.formModel.id > 0) {
        if (
          this.formModel.taskStatus == "待审核" ||
          this.formModel.taskStatus == "审核未通过"
        ) {
          this.$confirm("确定操作吗?", "信息", {
            confirmButtonText: "确定",
            cancelButtonText: "取消",
            type: "warning"
          })
            .then(() => {
              this.cancelCondition.taskNo = this.formModel.taskNo;
              appSvc
                .cancelAllotTask(this.cancelCondition)
                .then(
                  res => {
                    this.$message({
                      message: "操作成功!",
                      type: "success"
                    });
                    this.search();
                    this.cancel();
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
        }
      }
    },

    //关闭弹窗
    cancel() {
      this.formVisible = false;

      Object.assign(this.$data.formModel, this.$options.data().formModel);
      this.sourceWarehouseSelNew = [];
      this.targetWarehouseSelNew = [];
      this.productStockTable = [];

      Object.assign(
        this.$data.detailCondition,
        this.$options.data().detailCondition
      );
      Object.assign(this.$data.auditModel, this.$options.data().auditModel);

      this.viewBtn = false;
      this.isModify = 0;

      //重置值
      this.orginSource = undefined;
      this.orginTarget = undefined;
      this.orginSourceValue = undefined;
      this.orginTargetValue = undefined;
    },
    add() {
      this.formVisible = true;
      this.formTitle = "创建调拨单（门店间调拨请到门店系统操作）";
      this.isModify = 1;
      appSvc
        .getCurrentUser()
        .then(
          res => {
            this.formModel.createBy = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    viewDetail(row) {
      this.isModify = 2;
      this.formVisible = true;
      this.formTitle = "【" + row.taskNo + "】调拨单详情";

      //查询详情信息
      this.detailCondition.taskId = row.id;
      appSvc
        .getAllotTaskDetail(this.detailCondition)
        .then(
          res => {
            var resData = res.data;
            // var source = resData.sourceWarehouse + "-" + resData.sourceType;
            // var target = resData.targetWarehouse + "-" + resData.targetType;
            this.formModel = res.data;
            this.radio = this.formModel.isAudit;
            this.formModel.targetWarehouse = this.formModel.targetWarehouseName;
            this.formModel.sourceWarehouse = this.formModel.sourceWarehouseName;
            this.productStockTable = res.data.products;
            //系统暂存值
            this.orginSource = this.formModel.sourceWarehouse;
            this.orginSourceValue = this.formModel.sourceWarehouseName;

            this.orginTarget = this.formModel.targetWarehouse;
            this.orginTargetValue = this.formModel.targetWarehouseName;

            if (
              this.formModel.taskStatus == "待审核" ||
              this.formModel.taskStatus == "审核未通过"
            ) {
              this.viewBtn = false;
            } else {
              this.viewBtn = true;
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});

      //查询日志
      // this.logCondition.objectId = row.id;
      // this.logCondition.objectType = "AllotTask";

      // appSvc
      //   .getWmsLogs(this.logCondition)
      //   .then(
      //     res => {
      //       this.logData = res.data;
      //     },
      //     error => {
      //       console.log(error);
      //     }
      //   )
      //   .catch(() => {});
    },
    getSourceWarehouse(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.shopSelCondition.simpleName = query;
          appSvc
            .getSourceWarehouses(this.shopSelCondition)
            .then(
              res => {
                this.sourceWarehouseSel = res.data;
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally(p => {
              this.loading = false;
            });
        }, 200);
      } else {
        this.options = [];
      }
    },
    getTargetWarehouse(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.simpleName = query;
          appSvc
            .getSourceWarehouses(this.shopSelCondition)
            .then(
              res => {
                this.targetWarehouseSel = res.data;
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

    getSourceWarehouseNew(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.simpleName = query;
          appSvc
            .getSourceWarehouses(this.shopSelCondition)
            .then(
              res => {
                this.sourceWarehouseSelNew = res.data;
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
    getTargetWarehouseNew(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.simpleName = query;
          appSvc
            .getSourceWarehouses(this.shopSelCondition)
            .then(
              res => {
                this.targetWarehouseSelNew = res.data;
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

    //获取采购状态
    getTaskStatus() {
      appSvc
        .getAllotTaskStatus()
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

    fetchData() {
      // this.getwarehouses();
      this.getTaskStatus();
      // this.getTransferType();
      // this.getConditionData();

      appSvc
        .getAllotPageData(this.condition)
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
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    // pageTurning() {
    //   this.condition.pageIndex = this.currentPage;
    //   this.getPaged();
    // },

    getPaged(flag) {
      this.tableLoading = true;

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getAllotPageData(this.condition)
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
>>> .el-form--inline .el-form-item {
  padding-bottom: 10px;
}
</style>
