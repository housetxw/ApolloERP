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
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item prop="name">
          <el-input
            v-model="condition.name"
            placeholder="规则名称"
            style="width: 200px"
          ></el-input>
        </el-form-item>
        <el-form-item prop="pid">
          <el-input
            v-model="condition.pid"
            placeholder="商品编号"
            style="width: 200px"
          ></el-input>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" icon="el-icon-plus" @click="add()"
          >新增</el-button
        >
      </template>
      <template v-slot:tb_cols>
        <rg-table-column prop="name" min-width="150" label="规则名称">
        </rg-table-column>
        <rg-table-column
          prop="isValid"
          label="是否有效"
          width="50"
          align="center"
        >
          <template scope="scope">{{
            {
              0: "无效",
              1: "有效",
            }[scope.row.isValid] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column width="50" prop="isByShopType" label="按店类型">
          <template scope="scope">{{
            {
              0: "否",
              1: "是",
            }[scope.row.isByShopType] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column prop="shopType" width="50" label="门店类型">
          <template scope="scope">{{
            {
              1: "合作店",
              2: "工场店",
              4: "上门养车",
              8: "认证店",
              16: "工作室",
            }[scope.row.shopType] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column width="50" prop="isByShopId" label="按 门店">
          <template scope="scope">{{
            {
              0: "否",
              1: "是",
            }[scope.row.isByShopId] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column prop="shopIds" max-width="100" label="门店"></rg-table-column>
        <rg-table-column prop="useRuleDesc" min-width="50" label="规则描述" />
        <rg-table-column width="60"
          prop="validStartType"
          label="开始类型"
          align="center"
        >
          <template scope="scope">{{
            {
              0: "未设置",
              1: "当天生效",
              2: "指定日期",
            }[scope.row.validStartType] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column width="60"
          prop="validEndType"
          label="结束类型"
          align="center"
        >
          <template scope="scope">{{
            {
              0: "未设置",
              1: "生效天数",
              2: "指定日期",
            }[scope.row.validStartType] || ""
          }}</template>
        </rg-table-column>
        <rg-table-column
          width="50"
          prop="validDays"
          label="有效天数"
          align="center"
        >
        </rg-table-column>
        <rg-table-column width="80"
          prop="earliestUseDate"
          label="最早日期"
          is-date
          align="center"
        >
        </rg-table-column>
        <rg-table-column width="80"
          prop="latestUseDate"
          label="最晚日期"
          is-date
          align="center"
        >
        </rg-table-column>
        <rg-table-column prop="createBy" label="创建人" align="center">
        </rg-table-column>
        <rg-table-column
          prop="createTime"
          label="创建时间"
          is-datetime
          fix-min
        />
        <rg-table-column max-width="200" prop="pids" label="产品" />
        <el-table-column
          prop="name"
          label="操作"
          fixed="right"
          width="120"
          align="center"
        >
          <template slot-scope="scope">
            <el-button-group>
              <el-button type="primary" @click="edit(scope.row)"
                >编辑
              </el-button>
              <el-button type="primary" @click="editProduct(scope.row)"
                >编辑产品
              </el-button>
            </el-button-group>
          </template>
        </el-table-column>
      </template>
    </rg-page>

    <!-- 新增和编辑页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        title="核销规则配置"
        :visible.sync="formVisible"
        v-if="formVisible"
        :footbar="true"
        :beforeClose="cancel"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            cancel('formModel');
          },
        }"
        maxWidth="1024px"
        minWidth="900px"
      >
        <template v-slot:content>
          <el-form
            :model="formModel"
            :rules="rules"
            ref="formModel"
            size="mini"
            :label-width="formLabelWidth"
          >
            <el-row>
              <el-form-item label="名称" prop="name">
                <el-col :span="18">
                  <el-input
                    v-model="formModel.name"
                    placeholder="名称"
                  ></el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="是否有效" prop="isValid">
                <el-col :span="18">
                  <el-radio-group v-model="formModel.isValid">
                    <el-radio :label="0">无效</el-radio>
                    <el-radio :label="1">有效</el-radio>
                  </el-radio-group>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="是否按门店类型" prop="isByShopType">
                <el-col :span="18">
                  <el-radio-group
                    v-model="formModel.isByShopType"
                    @change="shopTypeCheck()"
                  >
                    <el-radio :label="0">否</el-radio>
                    <el-radio :label="1">是</el-radio>
                  </el-radio-group>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item
                label="门店类型"
                prop="shopType"
                v-show="showShopType"
              >
                <el-col :span="18">
                  <el-radio-group v-model="formModel.shopType">
                    <el-radio :label="1">合作店</el-radio>
                    <el-radio :label="2">工场店</el-radio>
                    <el-radio :label="4">上门养车</el-radio>
                    <el-radio :label="8">认证店</el-radio>
                    <el-radio :label="16">工作室</el-radio>
                  </el-radio-group>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item
                label="是否按门店"
                prop="isByShopId"
                v-show="showShopGroupRadio"
              >
                <el-col :span="18">
                  <el-radio-group
                    v-model="formModel.isByShopId"
                    :change="showShopNoCheck()"
                  >
                    <el-radio :label="0">否</el-radio>
                    <el-radio :label="1">是</el-radio>
                  </el-radio-group>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item
                label="门店编号"
                prop="shopNos"
                v-show="showShopNos"
              >
                <el-col :span="18">
                  <el-input
                    v-model="formModel.shopNos"
                    placeholder="门店编号（以,分隔 示例：7,98）"
                  ></el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="有效期开始类型" prop="validStartType">
                <el-col :span="8">
                  <el-radio-group
                    v-model="formModel.validStartType"
                    :change="showEarliestDateCheck()"
                  >
                    <el-radio :label="1">领取当天生效</el-radio>
                    <el-radio :label="2">指定开始日期</el-radio>
                  </el-radio-group>
                </el-col>
              </el-form-item>
              <el-form-item
                label="最早使用日期"
                prop="earliestUseDate"
                v-show="showEarliestDate"
              >
                <el-col :span="10">
                  <el-date-picker
                    type="date"
                    style="width: 135px"
                    placeholder="创建开始日期"
                    v-model="formModel.earliestUseDate"
                  ></el-date-picker>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="有效期结束类型" prop="validEndType">
                <el-col :span="8">
                  <el-radio-group
                    v-model="formModel.validEndType"
                    :change="showlastestDateCheck()"
                  >
                    <el-radio :label="1">生效天数</el-radio>
                    <el-radio :label="2">指定结束日期</el-radio>
                  </el-radio-group>
                </el-col>
              </el-form-item>
              <el-form-item
                label="最晚使用日期"
                prop="latestUseDate"
                v-show="showLastestDate"
              >
                <el-col :span="10">
                  <el-date-picker
                    type="date"
                    style="width: 135px"
                    placeholder="最晚使用日期"
                    v-model="formModel.latestUseDate"
                  ></el-date-picker>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item
                label="有效时长天数"
                prop="validDays"
                v-show="showValidDays"
              >
                <el-col :span="18">
                  <el-input
                    type="number"
                    v-model="formModel.validDays"
                    placeholder="有效时长天数"
                  ></el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="描述" prop="useRuleDesc">
                <el-col :span="18">
                  <el-input
                    v-model="formModel.useRuleDesc"
                    placeholder="描述"
                  ></el-input>
                </el-col>
              </el-form-item>
            </el-row>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            size="mini"
            icon="el-icon-check"
            type="primary"
            :loading="btnSaveLoading"
            @click="save('formModel')"
            >提交</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 新增和编辑页面 -->
    <section id="createOrUpdateProduct">
      <rg-dialog
        title="规则产品配置"
        :visible.sync="formVisibleProduct"
        v-if="formVisibleProduct"
        :footbar="true"
        :beforeClose="cancelProduct"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            cancelProduct('formModelProduct');
          },
        }"
        maxWidth="1024px"
        minWidth="900px"
      >
        <template v-slot:content>
          <el-form
            :model="formModelProduct"
            ref="formModelProduct"
            size="mini"
            :label-width="formLabelWidth"
          >
            <el-row>
              <el-form-item label="产品名称" prop="pidNos">
                <el-col :span="18">
                  <el-input
                    v-model="formModelProduct.pidNos"
                    placeholder="产品名称(以,分割  示例：MP-RMP-BMP-75,BYFW-LQQ-AF-4)"
                  ></el-input>
                </el-col>
              </el-form-item>
            </el-row>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            size="mini"
            icon="el-icon-check"
            type="primary"
            :loading="btnSaveLoading"
            @click="saveProduct('formModelProduct')"
            >提交</el-button
          >
        </template>
      </rg-dialog>
    </section>
  </main>
</template>

<script>
import { verficationRuleSvc } from "@/api/verficationRule/rule";
export default {
  name: "verficationRuleList",
  data() {
    return {
      tableLoading: false,
      btnSaveRoleLoading: false,
      totalList: 0,
      //表单列表的条件
      condition: {
        name: "",
        pid: "",
        PageIndex: 1,
        PageSize: 20,
      },
      tableData: [],
      currentPage: 1,
      formVisible: false,
      btnSaveLoading: false,
      formInit: {
        name: "",
        isValid: 1,
        isByShopType: 0,
        shopType: 0,
        isByShopId: 0,
        shopNos: "",
        validStartType: 1,
        validEndType: 1,
        earliestUseDate: "",
        latestUseDate: "",
        useRuleDesc: "",
      },
      formModel: {
        id: 0,
        name: "",
        isValid: 1,
        isByShopType: 0,
        shopType: 0,
        isByShopId: 0,
        shopNos: "",
        validStartType: 0,
        validEndType: 0,
        earliestUseDate: "",
        latestUseDate: "",
        useRuleDesc: "",
        shopIds: [],
      },
      formLabelWidth: "120px",
      rules: {
        name: [
          { required: true, message: "请输入规则名称", trigger: "blur" },
          { max: 50, message: "描述长度不允许超过50", trigger: "blur" },
        ],
        useRuleDesc: [
          { max: 100, message: "描述长度不允许超过100", trigger: "blur" },
        ],
      },
      showShopType: false,
      showShopGroupRadio: true,
      showShopNos: false,
      showEarliestDate: false,
      showLastestDate: false,
      showValidDays: false,

      formVisibleProduct: false,
      formModelProduct: {
        pids: [],
        pidNos: "",
        ruleId: 0,
      },
      formInitProduct: {
        pids: "",
      },
    };
  },
  created() {
    this.getTable();
  },
  methods: {
    //获取分页
    GetPaged(flag) {
      this.tableLoading = true;
      console.log("condition: " + JSON.stringify(this.condition));

      verficationRuleSvc
        .GetVerificationRule(this.condition)
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
    getTable() {
      this.GetPaged();
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
    cancel(formName) {
      this.formVisible = false;
      this.resetForm();
      this.venderList = [];
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },
    cancelProduct(formName) {
      this.formVisibleProduct = false;
      this.formModel = JSON.parse(JSON.stringify(this.formInitProduct));
      this.venderList = [];
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },
    resetForm() {
      this.formModel = JSON.parse(JSON.stringify(this.formInit));
    },
    add() {
      this.formVisible = true;
      this.formModel.validStartType = 1;
      this.formModel.validEndType = 1;
    },
    shopTypeCheck() {
      console.log(this.formModel.isByShopType);
      if (this.formModel.isByShopType === 1) {
        this.showShopType = true;
        this.showShopGroupRadio = false;
        this.formModel.shopNos = "";
      } else {
        this.showShopType = false;
        this.showShopGroupRadio = true;
      }

      this.showShopNos = false;
    },
    showShopNoCheck() {
      if (
        this.formModel.isByShopId === 1 &&
        this.formModel.isByShopType === 0
      ) {
        this.showShopNos = true;
      } else {
        this.showShopNos = false;
        this.formModel.shopNos = "";
      }
    },
    showEarliestDateCheck() {
      if (this.formModel.validStartType === 1) {
        this.showValidDays = true;
        this.showEarliestDate = false;
      } else if (this.formModel.validStartType === 2) {
        this.showEarliestDate = true;
      } else {
        this.showEarliestDate = false;
        this.formModel.earliestUseDate = "";
      }

      if (
        this.formModel.validEndType === 2 &&
        this.formModel.validStartType === 2
      ) {
        this.showValidDays = false;
      }
    },
    showlastestDateCheck() {
      if (this.formModel.validEndType === 1) {
        this.showValidDays = true;
        this.showLastestDate = false;
      } else if (this.formModel.validEndType === 2) {
        this.showLastestDate = true;
      } else {
        this.formModel.latestUseDate = "";
        this.showLastestDate = false;
      }

      if (
        this.formModel.validEndType === 2 &&
        this.formModel.validStartType === 2
      ) {
        this.showValidDays = false;
      }
    },
    save(formName) {
      var vm = this;

      this.$refs[formName].validate((valid) => {
        if (valid) {
          if (this.formModel.shopNos.length > 0) {
            let shopIds = this.formModel.shopNos.split(",");
            this.formModel.shopIds = new Array();
            for (var i = 0; i < shopIds.length; i++) {
              console.log(shopIds[i]);
              this.formModel.shopIds.push(shopIds[i]);
            }
          }
          if (this.formModel.isByShopType === 1) {
            this.formModel.shopIds = [];
            this.formModel.isByShopId=0;
          } else {
            this.formModel.shopType = 0;
          }
          if(this.formModel.isByShopId===0){
            this.formModel.shopIds=[];
          }
          console.log("formModel: " + JSON.stringify(this.formModel));
          var data = JSON.parse(JSON.stringify(this.formModel));
          vm.btnSaveRoleLoading = true;
          verficationRuleSvc
            .SaveVerificationRule(data)
            .then((res) => {
              if (res.data) {
                this.$message({
                  message: res.message,
                  type: "success",
                });
              }
            })
            .catch((error) => {
              console.log(error);
            })
            .finally(() => {
              vm.btnSaveRoleLoading = false;
              this.getTable();
              this.cancel("formModel");
            });
        } else {
          return false;
        }
      });
    },
    edit(row) {
      this.formVisible = true;
      let obj = JSON.parse(JSON.stringify(row));

      if (obj.isByShopType === 1) {
        this.showShopType = true;
        this.showShopGroupRadio = false;
        this.showShopNos = false;
      } else {
        this.showShopType = false;
      }
      if (obj.isByShopId === 1) {
        this.showShopGroupRadio = true;
        this.showShopNos = true;
      }
      if (obj.validStartType === 1) {
        this.showValidDays = true;
      }
      if (obj.validEndType === 1) {
        this.showValidDays = true;
      }
      if (obj.validEndType === 2) {
        this.formModel.latestUseDate = obj.latestUseDate;
        this.showLastestDate = true;
      }
      if (obj.validStartType === 2) {
        this.formModel.earliestUseDate = obj.earliestUseDate;
        this.showEarliestDate = true;
      }
      obj.shopNos = obj.shopIds;

      this.formModel = obj;

      console.log(JSON.parse(JSON.stringify(this.formModel)));
    },
    editProduct(row) {
      this.formVisibleProduct = true;
      this.formModelProduct.ruleId = row.id;
      this.formModelProduct.pidNos = row.pids;
      let obj = JSON.parse(JSON.stringify(row));
    },
    saveProduct(formName) {
      var vm = this;

      this.$refs[formName].validate((valid) => {
        if (valid) {
          //debugger;

          console.log("formModel: " + JSON.stringify(this.formModelProduct));

          if (this.formModelProduct.pidNos.length > 0) {
            let pids = this.formModelProduct.pidNos.split(",");
            this.formModel.pids = new Array();
            for (var i = 0; i < pids.length; i++) {
              console.log(pids[i]);
              this.formModelProduct.pids.push(pids[i]);
            }
          }

          var data = JSON.parse(JSON.stringify(this.formModelProduct));
          vm.btnSaveRoleLoading = true;
          verficationRuleSvc
            .SaveBeautiyOrPackageCardVerificationProduct(data)
            .then((res) => {
              console.log(res.data);
              if (res.data) {
                this.$message({
                  message: res.message,
                  type: "success",
                });
              }
            })
            .catch((error) => {
              console.log(error);
            })
            .finally(() => {
              vm.btnSaveRoleLoading = false;
              this.getTable();
              this.cancelProduct("formModelProduct");
            });
        } else {
          return false;
        }
      });
    },
  },
};
</script>
