<template>
  <main class="container">
    <!-- 首页：列表数据 -->
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :headerBtnWidth="140"
      :tableData="tableData"
      :pageChange="pageChange"
      :searching="search"
      :tbRowDblClick="tbRowDblClick"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-cascader
            expand-trigger="hover"
            :options="options"
            placeholder="请选择资产类别"
            v-model="conditionTypes"
            @change="handleConditionTypeChange"
          ></el-cascader>
        </el-form-item>
        <el-form-item>
          <el-input
            v-model="condition.name"
            style="width:140px"
            autocomplete="off"
            placeholder="请输入资产名称"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-input
            v-model="condition.code"
            style="width:140px"
            autocomplete="off"
            placeholder="请输入资产编码"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.addMethod"
            style="width:100px"
            autocomplete="off"
            placeholder="增加方式"
            clearable
          >
            <el-option
              v-for="item in addMethods"
              :key="item.value"
              :label="item.name"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.useStatus"
            style="width:100px"
            autocomplete="off"
            placeholder="使用状态"
            clearable
          >
            <el-option
              v-for="item in useStatuses"
              :key="item.value"
              :label="item.name"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-tooltip class="item" effect="dark" content="开始使用日期范围" placement="bottom">
            <el-date-picker
              v-model="startUseDates"
              style="width:210px"
              type="daterange"
              :picker-options="$root.$data.timeRgPickerOpt"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              :default-time="['00:00:00', '00:00:00']"
              @change="handleStartUseDateChange"
            ></el-date-picker>
          </el-tooltip>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" icon="el-icon-plus" @click="add">新增</el-button>
      </template>
      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <rg-table-column label="资产编码" prop="code" />
        <el-table-column label="资产类别" prop="firstTypeName" min-width="150">
          <template
            slot-scope="scope"
          >{{getTypeName(scope.row.firstTypeName,scope.row.secondTypeName)}}</template>
        </el-table-column>
        <el-table-column label="资产名称" prop="name"></el-table-column>
        <rg-table-column label="规格型号" prop="specification" />
        <el-table-column label="增加方式" prop="addMethod" width="70">
          <template slot-scope="scope">{{getAddMethodName(scope.row.addMethod)}}</template>
        </el-table-column>
        <el-table-column label="使用状态" prop="useStatus" align="center" width="70">
          <template slot-scope="scope">{{getUseStatusName(scope.row.useStatus)}}</template>
        </el-table-column>
        <el-table-column label="开始使用日期" prop="startUseDate" align="center" width="120">
          <template slot-scope="scope">{{scope.row.startUseDate.substr(0,16)}}</template>
        </el-table-column>
        <el-table-column label="已使用月份" prop="usedMonths" align="right" width="100">
          <template slot-scope="scope">{{monDiff(scope.row.startUseDate,new Date())}}</template>
        </el-table-column>
        <el-table-column label="原单价" prop="price" align="right" width="100">
          <template slot-scope="scope">￥{{scope.row.price.format(2)}}</template>
        </el-table-column>
        <el-table-column label="估计累计折旧" prop="estimateDepreciation" align="right" width="100">
          <template slot-scope="scope">￥{{scope.row.estimateDepreciation.format(2)}}</template>
        </el-table-column>
        <el-table-column label="存放地点" prop="storageLocation" min-width="100px"></el-table-column>
        <el-table-column label="创建日期" prop="createTime" align="center" width="120">
          <template slot-scope="scope">{{scope.row.createTime.substr(0,16)}}</template>
        </el-table-column>
        <el-table-column label="操作" fixed="right" align="center" width="180">
          <template slot-scope="scope">
            <el-button-group>
              <el-button type="primary" size="mini" @click="edit(scope.row)">修改</el-button>
              <el-button type="danger" size="mini" @click="deleted(scope.row)">删除</el-button>
              <el-button
                type="warning"
                size="mini"
                @click="maintain(scope.row)"
                :disabled="scope.row.useStatus==2"
              >维修</el-button>
              <el-button
                type="danger"
                size="mini"
                @click="discard(scope.row)"
                :disabled="scope.row.useStatus==2"
              >作废</el-button>
            </el-button-group>
          </template>
        </el-table-column>
        <!-- Table数据 -->
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

    <!-- 新增和编辑页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        :title="formTitle"
        :visible.sync="formVisible"
        :beforeClose="cancel"
        :btnCancel="{label:'关闭',click:(done)=>{cancel('formModel')}}"
        :btnRemove="false"
        :footbar="true"
        width="60%"
        v-if="formVisible"
        maxWidth="800px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-form
            :model="formModel"
            :rules="rules"
            ref="formModel"
            autocomplete="off"
            clearable
            :label-width="formLabelWidth"
            size="mini"
          >
            <el-form-item label="资产类别" prop="formTypes">
              <el-cascader
                expand-trigger="hover"
                style="width:200px"
                :options="options"
                v-model="formModel.formTypes"
                @change="handleFormTypeChange"
                :disabled="formCheck"
              ></el-cascader>
            </el-form-item>
            <el-form-item label="资产名称" prop="name">
              <el-col :span="12">
                <el-input v-model="formModel.name" :disabled="formCheck"></el-input>
              </el-col>
            </el-form-item>
            <el-form-item label="资产型号" prop="specification">
              <el-col :span="12">
                <el-input v-model="formModel.specification" :disabled="formCheck"></el-input>
              </el-col>
            </el-form-item>
            <el-form-item label="数量" prop="number">
              <el-input-number
                v-model="formModel.number"
                :disabled="formCheck"
                :min="0"
                style="width:200px"
              ></el-input-number>
            </el-form-item>
            <el-form-item label="开始使用日期" prop="startUseDate">
              <el-date-picker
                style="width:200px"
                v-model="formModel.startUseDate"
                type="date"
                placeholder="请选择"
                value-format="yyyy-MM-dd HH:mm:ss"
                :disabled="formCheck"
              ></el-date-picker>
            </el-form-item>
            <el-form-item label="存放地点" prop="storageLocation">
              <el-col :span="12">
                <el-input v-model="formModel.storageLocation" :disabled="formCheck&&formCheckEdit"></el-input>
              </el-col>
            </el-form-item>
            <el-form-item label="增加方式" prop="addMethod">
              <el-select v-model="formModel.addMethod" :disabled="formCheck" style="width:200px">
                <el-option
                  v-for="item in addMethods"
                  :key="item.value"
                  :label="item.name"
                  :value="item.value"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item label="计划使用月份" prop="planUseMonths">
              <el-input-number
                style="width:200px"
                v-model="formModel.planUseMonths"
                controls-position="right"
                :min="0"
                :disabled="formCheck"
              ></el-input-number>
            </el-form-item>
            <el-form-item label="原单价" prop="price">
              <el-input-number
                style="width:200px"
                v-model="formModel.price"
                controls-position="right"
                :min="0"
                :precision="2"
                :step="0.1"
                @change="(v)=>{formModel.estimateDepreciation=Math.min(formModel.estimateDepreciation,formModel.price)}"
                :disabled="formCheck"
              ></el-input-number>
            </el-form-item>
            <el-form-item label="估算折旧" prop="estimateDepreciation">
              <el-input-number
                style="width:200px"
                v-model="formModel.estimateDepreciation"
                :disabled="formCheck"
                controls-position="right"
                :precision="2"
                :step="0.1"
                :max="formModel.price"
              ></el-input-number>
              <el-popover
                style="margin-left:-5px; border-left:none"
                placement="top-start"
                title="直线折旧法："
                trigger="hover"
                content="估算折旧= 固定资产原价/计划使用月份*当前使用月份（满一个月开始计算）"
              >
                <el-button slot="reference">元 （直线折旧法）</el-button>
              </el-popover>
            </el-form-item>
            <el-form-item label="备注" prop="remark">
              <el-input
                type="textarea"
                v-model="formModel.remark"
                :disabled="formCheck&&formCheckEdit"
              ></el-input>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            :disabled="formCheck&&formCheckEdit"
            :loading="btnSaveLoading"
            @click="save('formModel')"
          >保存</el-button>
        </template>
      </rg-dialog>
    </section>
    <!-- 新增和编辑页面 -->

    <!--操作-->
    <el-dialog
      title="新增作废"
      :visible.sync="dialogVisibleDiscard"
      width="30%"
      :before-close="handleCloseDiscard"
    >
      <el-form ref="formDiscard" :model="formDiscard" label-width="80px" :rules="rulesDiscard">
        <el-form-item label="资产ID" v-if="false">
          <el-input v-model="formDiscard.assetid"></el-input>
        </el-form-item>
        <el-form-item label="报废类型" prop="discardType">
          <el-select v-model="formDiscard.discardType" placeholder="请报废类型">
            <el-option label="自然报废" value="1"></el-option>
            <el-option label="灾害事故" value="2"></el-option>
            <el-option label="人为损坏" value="3"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="报废原因" prop="reason">
          <el-input v-model="formDiscard.reason"></el-input>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="handleCloseDiscard">取 消</el-button>
        <el-button type="primary" @click="saveDiscard('formDiscard')">确 定</el-button>
      </span>
    </el-dialog>
    <el-dialog
      title="新增资产维修记录"
      :visible.sync="dialogVisibleMaintain"
      width="30%"
      :before-close="handleCloseMaintain"
    >
      <el-form
        ref="formMaintain"
        :model="formMaintain"
        label-width="80px"
        :rules="rulesMaintain"
        size="mini"
      >
        <el-form-item label="资产ID" v-if="false">
          <el-input v-model="formMaintain.assetid"></el-input>
        </el-form-item>
        <el-form-item label="维修日期" prop="date">
          <el-date-picker v-model="formMaintain.date" type="date" placeholder="选择日期"></el-date-picker>
        </el-form-item>
        <el-form-item label="维修费用" prop="cost">
          <el-input-number v-model="formMaintain.cost" controls-position="right" :min="0"></el-input-number>
        </el-form-item>
        <el-form-item label="供应商" prop="supplier">
          <el-input v-model="formMaintain.supplier"></el-input>
        </el-form-item>
        <el-form-item label="维修内容" prop="content">
          <el-input type="textarea" v-model="formMaintain.content"></el-input>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="handleCloseMaintain">取 消</el-button>
        <el-button type="primary" @click="saveMaintain('formMaintain')">保 存</el-button>
      </span>
    </el-dialog>
    <!--操作-->
  </main>
</template>

<script>
import { shopAssetSvc } from "@/api/shopsetting/shopasset";
export default {
  name: "shopasset",
  data() {
    return {
      tableLoading: false, //表格加载动画
      formLabelWidth: "150px", //表单标签宽度
      conditionTypes: [],
      startUseDates: [],
      condition: {
        pageIndex: 1,
        pageSize: 20,
        firstTypeCode: undefined,
        secondTypeCode: undefined,
        name: undefined,
        code: undefined,
        addMethod: undefined,
        useStatus: undefined,
        startUseDateOrigin: undefined,
        startUseDateOff: undefined
      }, //查询条件
      tableData: [], //表格数据
      totalList: 0, //总记录数
      formVisible: false, //表单可视
      formTitle: "详情", //表单标题
      formCheck: false, //表单不可编辑
      formCheckEdit: false, //修改时表单不可编辑
      btnSaveLoading: false, //保存按钮加载动画
      formInit: {
        formTypes: [],
        id: 0,
        firstTypeCode: undefined,
        secondTypeCode: undefined,
        code: undefined,
        name: undefined,
        specification: undefined,
        number: 1,
        startUseDate: undefined,
        storageLocation: undefined,
        addMethod: undefined,
        planUseMonths: undefined,
        price: undefined,
        estimateDepreciation: undefined,
        remark: undefined,
        useStatus: 1
      }, //表单初始化数据
      formModel: {
        formTypes: [],
        id: 0,
        firstTypeCode: undefined,
        secondTypeCode: undefined,
        code: undefined,
        name: undefined,
        specification: undefined,
        number: undefined,
        useStatus: undefined,
        startUseDate: undefined,
        storageLocation: undefined,
        addMethod: undefined,
        planUseMonths: undefined,
        price: undefined,
        estimateDepreciation: undefined,
        remark: undefined
      }, //表单数据
      formDiscard: {
        assetid: undefined,
        discardType: undefined,
        reason: undefined
      }, //作废表单
      dialogVisibleDiscard: false,
      formMaintain: {
        assetId: undefined,
        date: undefined,
        cost: undefined,
        supplier: undefined,
        content: undefined
      }, //维修表单
      dialogVisibleMaintain: false,
      rulesDiscard: {
        discardType: [
          { required: true, message: "请选择报废类型", trigger: "blur" }
        ],
        reason: [
          { required: true, message: "请填写报废原因", trigger: "blur" },
          { max: 50, message: "报废原因长度不允许超过50", trigger: "blur" }
        ]
      },
      rulesMaintain: {
        date: [
          {
            type: "date",
            required: true,
            message: "请选择维修日期",
            trigger: "change"
          }
        ]
      },
      rules: {
        formTypes: [
          {
            type: "array",
            required: true,
            message: "请选择资产类别",
            trigger: "change"
          }
        ],
        name: [
          { required: true, message: "请输入资产名称", trigger: "blur" },
          { max: 50, message: "资产名称长度不允许超过50", trigger: "blur" }
        ],
        specification: [
          { required: true, message: "请输入资产型号", trigger: "blur" },
          { max: 50, message: "资产型号长度不允许超过50", trigger: "blur" }
        ],
        number: [{ required: true, message: "请输入数量", trigger: "blur" }],
        startUseDate: [
          { required: true, message: "请输入开始使用日期", trigger: "blur" }
        ],
        storageLocation: [
          { required: true, message: "请输入存放地点", trigger: "blur" },
          { max: 50, message: "存放地点长度不允许超过50", trigger: "blur" }
        ],
        addMethod: [
          { required: true, message: "请选择增加方式", trigger: "change" }
        ],
        planUseMonths: [
          { required: true, message: "请输入计划使用月份", trigger: "blur" }
        ],
        price: [{ required: true, message: "请输入原单价", trigger: "blur" }],
        estimateDepreciation: [
          { required: true, message: "请输入估计折旧", trigger: "blur" }
        ],
        remark: [
          { max: 200, message: "备注长度不允许超过200", trigger: "blur" }
        ]
      }, //校验规则
      addMethods: [
        { value: 1, name: "购入" },
        { value: 2, name: "转入" }
      ],
      useStatuses: [
        { value: 1, name: "使用中" },
        { value: 2, name: "已报废" },
        { value: 3, name: "已调拨" }
      ],
      options: [] //选项
    };
  },
  created() {
    this.fetchData(); //页面首次加载时默认搜索
  },
  watch: {
   
  },
  methods: {
    monDiff(startTime, endTime) {
      startTime = new Date(startTime);
      endTime = new Date(endTime);
      var date2Mon;
      var startDate =
        startTime.getDate() +
        startTime.getHours() / 24 +
        startTime.getMinutes() / 24 / 60;
      var endDate =
        endTime.getDate() +
        endTime.getHours() / 24 +
        endTime.getMinutes() / 24 / 60;
      if (endDate >= startDate) {
        date2Mon = 0;
      } else {
        date2Mon = -1;
      }
      var mon =
        (endTime.getYear() - startTime.getYear()) * 12 +
        endTime.getMonth() -
        startTime.getMonth() +
        date2Mon;
      return mon;
    },
    getTypeName(type1, type2) {
      var fullType = "";
      if (!this.isEmpty(type1)) {
        fullType += type1;
      }
      if (!this.isEmpty(type2)) {
        fullType += "/" + type2;
      }
      return fullType;
    },
    getAddMethodName(value) {
      var name = "";
      switch (value) {
        case 1:
          name = "购入";
          break;
        case 2:
          name = "转入";
          break;
      }
      return name;
    },
    getUseStatusName(value) {
      var name = "";
      switch (value) {
        case 1:
          name = "使用中";
          break;
        case 2:
          name = "已报废";
          break;
        case 3:
          name = "已调拨";
          break;
      }
      return name;
    },
    isEmpty(property) {
      return (
        property === null || property === "" || typeof property === "undefined"
      );
    },
    handleConditionTypeChange() {
      this.condition.firstTypeCode = this.conditionTypes[0];
      this.condition.secondTypeCode = this.conditionTypes[1];
    }, //处理查询类型改变
    handleFormTypeChange() {
      this.formModel.firstTypeCode = this.formModel.formTypes[0];
      this.formModel.secondTypeCode = this.formModel.formTypes[1];
    }, //处理表单类型改变
    handleStartUseDateChange() {
      if (this.startUseDates == undefined) {
        this.condition.startUseDateOrigin = undefined;
        this.condition.startUseDateOff = undefined;
      } else {
        this.condition.startUseDateOrigin = this.startUseDates[0].toLocaleDateString();
        this.condition.startUseDateOff = this.startUseDates[1].toLocaleDateString();
      }
    }, //处理开始使用日期改变
    handleCloseDiscard() {
      this.$refs["formDiscard"].resetFields();
      this.dialogVisibleDiscard = false;
    },
    handleCloseMaintain() {
      this.$refs["formMaintain"].resetFields();
      this.dialogVisibleMaintain = false;
    },
    setFormType() {
      this.formModel.formTypes = [];
      if (!this.isEmpty(this.formModel.firstTypeCode)) {
        this.formModel.formTypes.push(this.formModel.firstTypeCode);
      }
      if (!this.isEmpty(this.formModel.secondTypeCode)) {
        this.formModel.formTypes.push(this.formModel.secondTypeCode);
      }
    }, //设置表单选项值
    fetchData() {
      this.getConditionData();
      this.getPaged();
    }, //列表查询
    getShopAsset(id) {
      shopAssetSvc
        .getShopAsset({ id: id })
        .then(res => {
          let data = res.data;
          this.formModel = data;
          this.setFormType();
        })
        .catch(err => {
          console.error(err);
        })
        .finally(() => {});
    }, //查询详情
    getConditionData() {
      shopAssetSvc.shopAssetTypeOptions({}).then(res => {
        this.options = res.data;
      });
    }, //获取查询条件
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    }, //分页器改变事件
    getPaged(flag) {
      this.tableLoading = true;
      shopAssetSvc
        .getShopAssetList(this.condition)
        .then(
          res => {
            let data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (flag) {
              this.$messageSuccess(res.message || "查询成功");
            }
          },
          err => {
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
    tbRowDblClick(row, column, event, tb) {
      if (row.isDeleted) {
        return this.$alertWarning(`资产 ${row.name} 已删除，不允许查看详情！`);
      }

      this.resetForm();
      tb.toggleRowSelection(row);
      tb.clearSelection();

      this.formTitle = "详情";
      this.formVisible = true;
      this.formCheck = true;
      this.formCheckEdit = true;

      this.getShopAsset(row.id);
    },
    add() {
      this.resetForm();
      this.formTitle = "新增资产";
      this.formVisible = true;
      this.formCheck = false;
      this.formCheckEdit = false;
    }, //新增弹窗
    edit(sel) {
      this.resetForm();
      this.formTitle = "修改资产信息";
      this.formVisible = true;
      this.formCheck = true;
      this.formCheckEdit = false;
      this.getShopAsset(sel.id);
    }, //编辑弹窗
    save(formName) {
      var vm = this;
      this.$refs[formName].validate(valid => {
        if (valid) {
          vm.btnSaveLoading = true;

          shopAssetSvc
            .upsertShopAsset(this.formModel)
            .then(
              res => {
                if (res.code == "10000") {
                  this.$message({
                    message: "操作成功",
                    type: "success"
                  });
                  vm.search();
                  if (vm.formVisible) {
                    vm.getConditionData();
                  }
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
    },
    deleted(sel) {
      let vm = this;
      let msg = `是否要删除资产：${sel.name}?`;
      this.$confirmWarning(msg).then(
        () => {
          this.tableLoading = true;
          shopAssetSvc
            .deleteShopAsset({ id: parseInt(sel.id) })
            .then(
              res => {
                if (res.code == 10000) {
                  this.$messageSuccess(res.message);
                  vm.search();
                } else {
                  this.$messageWarning(res.message);
                }
              },
              err => {
                console.error(err);
              }
            )
            .catch(() => {})
            .finally(() => {
              this.tableLoading = false;
            });
        },
        cancel => {
          console.warn(cancel);
        }
      );
    },
    maintain(sel) {
      this.formMaintain.assetid = sel.id;
      this.dialogVisibleMaintain = true;
    },
    saveMaintain(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          let vm = this;
          let msg = `是否要提交维修记录?`;
          this.$confirmWarning(msg).then(
            () => {
              this.tableLoading = true;
              shopAssetSvc
                .maintainShopAsset(this.formMaintain)
                .then(
                  res => {
                    if (res.code == 10000) {
                      this.$messageSuccess(res.message);
                      vm.search();
                    } else {
                      this.$messageWarning(res.message);
                    }
                  },
                  err => {
                    console.error(err);
                  }
                )
                .catch(() => {})
                .finally(() => {
                  this.tableLoading = false;
                  this.dialogVisibleMaintain = false;
                });
            },
            cancel => {
              console.warn(cancel);
            }
          );
        }
      });
    },
    discard(sel) {
      this.formDiscard.assetid = sel.id;
      this.dialogVisibleDiscard = true;
    },
    saveDiscard(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          let vm = this;
          let msg = `是否要作废该资产?`;
          this.$confirmWarning(msg).then(
            () => {
              this.tableLoading = true;
              shopAssetSvc
                .discardShopAsset(this.formDiscard)
                .then(
                  res => {
                    if (res.code == 10000) {
                      this.$messageSuccess(res.message);
                      vm.search();
                    } else {
                      this.$messageWarning(res.message);
                    }
                  },
                  err => {
                    console.error(err);
                  }
                )
                .catch(() => {})
                .finally(() => {
                  this.tableLoading = false;
                  this.dialogVisibleDiscard = false;
                });
            },
            cancel => {
              console.warn(cancel);
            }
          );
        }
      });
    },
    cancel(formName) {
      this.formVisible = false;
      this.formCheck = false;
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
      this.resetForm();
    },
    resetForm() {
      this.formModel.formTypes = [];
      this.formModel = this.formInit;
    }, //重置表单
    resetSel() {
      this.conditionTypes = [];
      this.startUseDates = [];
    } //重置查询条件
  }
};
</script>
