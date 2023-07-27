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
      :tbRowDblClick="tbRowDblClick"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-input
            v-model="condition.venderName"
            placeholder="输入供应商名称"
            style="width:200px"
            autocomplete="off"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-input
            v-model="condition.contact"
            placeholder="输入联系人名称或电话"
            style="width:200px"
            autocomplete="off"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.status"
            placeholder="状态"
            style="width:120px"
            autocomplete="off"
            clearable
          >
            <el-option label="全部" value="0"></el-option>
            <el-option label="正常" value="1"></el-option>
            <el-option label="停用" value="2"></el-option>
            <!-- <el-option label="已删除" value="3"></el-option> -->
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.settlementMethod"
            placeholder="结算方式"
            style="width:120px"
            autocomplete="off"
            clearable
          >
            <el-option label="现结" value="1"></el-option>
            <el-option label="账期" value="2"></el-option>
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

        <!-- <rg-table-column label="门店名称" prop="shopName" /> -->
        <rg-table-column label="简称" prop="venderShortName" />
        <rg-table-column label="全称" prop="venderName" fix-width="true" />
        <rg-table-column label="分类标注" prop="classifyMark" />
        <rg-table-column label="结算方式" prop="settlementMethod">
          <template slot-scope="scope">
            <span>{{ cellValue(scope.row, "settlementMethod") }}</span>
          </template>
        </rg-table-column>
        <rg-table-column label="联系人" prop="contact"></rg-table-column>
        <rg-table-column label="联系电话" prop="telNum"></rg-table-column>
        <rg-table-column label="类型" prop="isActive">
          <template slot-scope="scope">
            <span>{{ cellValue(scope.row, "isActive") }}</span>
          </template>
        </rg-table-column>
        <rg-table-column
          label="操作"
          fixed="right"
          align="center"
          width="100px"
        >
          <template slot-scope="scope">
            <el-button-group>
              <el-button type="primary" size="mini" @click="edit(scope.row)"
                >修改</el-button
              >
              <el-button
                type="danger"
                size="mini"
                @click="activeVender(scope.row, 0)"
                v-if="scope.row.isActive == 1"
                >停用</el-button
              >
              <el-button
                type="success"
                size="mini"
                @click="activeVender(scope.row, 1)"
                v-if="scope.row.isActive == 0"
                >启用</el-button
              >
            </el-button-group>
          </template>
        </rg-table-column>
        <!-- Table数据 -->
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

    <!-- 操作 -->
    <rg-dialog
      :title="formTitle"
      :visible.sync="formVisible"
      width="80%"
      max-width="800px"
      min-width="600px"
      :before-close="handleClose"
    >
      <template v-slot:content>
        <el-form
          :model="formModel"
          :rules="rules"
          size="mini"
          ref="formModel"
          label-width="120px"
          class="demo-ruleForm"
          :inline-message="true"
        >
          <el-divider content-position="left">基础信息</el-divider>
          <el-form-item label="供应商全称" prop="venderName">
            <el-col :span="18">
              <el-input
                v-model="formModel.venderName"
                :disabled="formCheck"
              ></el-input>
            </el-col>
          </el-form-item>
          <el-form-item label="供应商简称" prop="venderShortName">
            <el-col :span="18">
              <el-input
                v-model="formModel.venderShortName"
                :disabled="formCheck"
              ></el-input>
            </el-col>
          </el-form-item>
          <el-form-item label="联系地址" required>
            <el-col :span="6">
              <el-form-item prop="provinceId">
                <el-select
                  v-model="formModel.provinceId"
                  style="width:100%"
                  clearable
                  filterable
                  placeholder="请选择省"
                  @change="getCity"
                  :disabled="formCheck"
                  autocomplete="off"
                >
                  <el-option
                    v-for="item in provinceSel"
                    :key="item.regionId"
                    :label="item.name"
                    :value="item.regionId"
                  ></el-option>
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item prop="cityId">
                <el-select
                  v-model="formModel.cityId"
                  clearable
                  style="width:100%"
                  filterable
                  @change="getDistrict"
                  placeholder="请选择市"
                  :disabled="formCheck"
                  autocomplete="off"
                >
                  <el-option
                    v-for="item in citySel"
                    :key="item.regionId"
                    :label="item.name"
                    :value="item.regionId"
                  ></el-option>
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item prop="districtId">
                <el-select
                  style="width:100%"
                  v-model="formModel.districtId"
                  clearable
                  filterable
                  placeholder="请选择区"
                  :disabled="formCheck"
                  autocomplete="off"
                >
                  <el-option
                    v-for="item in districtSel"
                    :key="item.regionId"
                    :label="item.name"
                    :value="item.regionId"
                  ></el-option>
                </el-select>
              </el-form-item>
            </el-col>
          </el-form-item>
          <el-form-item prop="officeAddress">
            <el-col :span="18">
              <el-input
                v-model="formModel.officeAddress"
                :disabled="formCheck"
                placeholder="街道地址"
              ></el-input>
            </el-col>
          </el-form-item>
          <el-form-item label="官网地址" prop="offcialWebsite">
            <el-col :span="18">
              <el-input
                v-model="formModel.offcialWebsite"
                :disabled="formCheck"
                placeholder="官网地址"
                autocomplete="off"
              ></el-input>
            </el-col>
          </el-form-item>
          <el-form-item label="联系人" required>
            <el-col :span="9">
              <el-form-item prop="contact">
                <el-input
                  v-model="formModel.contact"
                  :disabled="formCheck"
                  placeholder="请输入联系人名称"
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="9">
              <el-form-item prop="telNum">
                <el-input
                  v-model="formModel.telNum"
                  :disabled="formCheck"
                  placeholder="联系电话"
                >
                  <template slot="prefix"
                    >电话</template
                  >
                </el-input>
              </el-form-item>
            </el-col>
            <el-col slot="error"> </el-col>
          </el-form-item>

          <el-form-item label="发票类型" prop="invoiceType">
            <el-radio-group
              v-model="formModel.invoiceType"
              :disabled="formCheck"
            >
              <el-radio :label="1">无需发票</el-radio>
              <el-radio :label="2">普通发票</el-radio>
              <el-radio :label="3">增值税发票</el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item label="结算方式" prop="settlementMethod">
            <el-radio-group
              v-model="formModel.settlementMethod"
              :disabled="formCheck"
            >
              <el-radio :label="1">现结</el-radio>
              <el-radio :label="3">月结</el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item label="账期" prop="paymentDay">
            <span>每月(</span>
            <el-input-number
              v-model="formModel.paymentDay"
              :min="0"
              label="账期天数"
              placeholder="天"
              :disabled="formCheck"
            ></el-input-number>
            <span>)天</span>
          </el-form-item>
          <el-form-item label="分类标注" prop="classifyMark">
            <el-col :span="18">
              <el-input
                v-model="formModel.classifyMark"
                :disabled="formCheck"
                placeholder="例如：供应机油"
              ></el-input>
            </el-col>
          </el-form-item>
          <el-divider content-position="left">财务信息</el-divider>
          <el-form-item label="收款银行" prop="bank">
            <el-col :span="18">
              <el-input
                v-model="formModel.bank"
                :disabled="formCheck"
                placeholder="XXXXXXXXXXX公司"
              ></el-input>
            </el-col>
          </el-form-item>
          <el-form-item label="收款户名" prop="receiveBankName">
            <el-col :span="18">
              <el-input
                v-model="formModel.receiveBankName"
                label="收款户名"
                placeholder="请输入收款户名"
                :disabled="formCheck"
              ></el-input>
            </el-col>
          </el-form-item>
          <el-form-item label="开户银行" required>
            <el-col :span="9">
              <el-form-item prop="bank">
                <el-input
                  v-model="formModel.openingBank"
                  :disabled="formCheck"
                  placeholder="开户银行"
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="9">
              <el-form-item prop="account">
                <el-input
                  v-model="formModel.account"
                  :disabled="formCheck"
                  placeholder="银行帐号"
                >
                  <template slot="prefix"
                    >账号</template
                  >
                </el-input>
              </el-form-item>
            </el-col>
          </el-form-item>

          <el-form-item label="财务联系人" required>
            <el-col :span="9">
              <el-form-item prop="financeName">
                <el-input
                  v-model="formModel.financeName"
                  :disabled="formCheck"
                  placeholder="请输入联系人名称"
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="9">
              <el-form-item prop="financeTel">
                <el-input
                  v-model="formModel.financeTel"
                  :disabled="formCheck"
                  placeholder="请输入联系电话"
                >
                  <template slot="prefix"
                    >电话</template
                  >
                </el-input>
              </el-form-item>
            </el-col>
          </el-form-item>

          <el-form-item label="税号" prop="tex">
            <el-col :span="18">
              <el-input
                v-model="formModel.tex"
                :disabled="formCheck"
                placeholder="请输入税号"
              ></el-input>
            </el-col>
          </el-form-item>
          <el-form-item label="票点(税点)" prop="taxPoint">
            <el-col :span="18">
              <el-input-number
                v-model="formModel.taxPoint"
                :min="0"
                label="票点(税点)"
                placeholder="请输入票点(税点)"
                :disabled="formCheck"
              ></el-input-number>
            </el-col>
          </el-form-item>
        </el-form>
      </template>
      <template v-slot:footer>
        <el-button type="primary" size="mini" @click="handleSubmit"
          >提 交</el-button
        >
        <!-- <el-button @click="handleClose">关 闭</el-button> -->
      </template>
    </rg-dialog>
    <!-- 操作 -->
  </main>
</template>

<script>
import { appVenderSvc } from "@/api/purchase/vender.js";
export default {
  name: "vender",
  data() {
    return {
      //*****公共属性*****
      tableLoading: false, //表格加载动画
      totalList: 0, //总记录数
      formVisible: false, //表单可视
      formTitle: "详情", //表单标题
      formCheck: false, //表单可编辑
      formCheckEdit: false, //修改时表单不可编辑
      btnSaveLoading: false, //保存按钮加载动画
      tableData: [], //表格数据
      condition: {
        pageIndex: 1,
        pageSize: 10,
        contact: undefined,
        status: 0,
        venderName: undefined,
        settlementMethod: undefined
      }, //查询条件
      formInit: {
        id: 0,
        venderName: "",
        venderShortName: "",
        officeAddress: "",
        provinceId: "",
        cityId: "",
        districtId: "",
        provinceName: "",
        cityName: "",
        districtName: "",
        offcialWebsite: "",
        contact: "",
        telNum: "",
        invoiceType: undefined,
        settlementMethod: undefined,
        paymentDay: undefined,
        classifyMark: "",
        bank: "",
        openingBank: "",
        account: "",
        financeName: "",
        financeTel: "",
        tex: "",
        taxPoint: undefined,
        receiveBankName: ""
      }, //表单初始化数据
      formModel: {
        id: 0,
        venderName: "",
        venderShortName: "",
        officeAddress: "",
        provinceId: "",
        cityId: "",
        districtId: "",
        provinceName: "",
        cityName: "",
        districtName: "",
        offcialWebsite: "",
        contact: "",
        telNum: "",
        invoiceType: undefined,
        settlementMethod: undefined,
        paymentDay: undefined,
        classifyMark: "",
        bank: "",
        openingBank: "",
        account: "",
        financeName: "",
        financeTel: "",
        tex: "",
        taxPoint: undefined,
        receiveBankName: ""
      }, //表单数据
      rules: {
        venderName: [
          { required: true, message: "请填写", trigger: "blur" },
          { max: 50, message: "长度不允许超过50", trigger: "blur" }
        ],
        venderShortName: [
          { required: true, message: "请填写", trigger: "blur" },
          { max: 50, message: "长度不允许超过50", trigger: "blur" }
        ],
        officeAddress: [
          { required: true, message: "请填写", trigger: "blur" },
          { max: 50, message: "长度不允许超过50", trigger: "blur" }
        ],
        provinceId: [{ required: true, message: "请选择", trigger: "change" }],
        cityId: [{ required: true, message: "请选择", trigger: "change" }],
        districtId: [{ required: true, message: "请选择", trigger: "change" }],
        contact: [
          { required: true, message: "请填写", trigger: "blur" },
          { max: 50, message: "长度不允许超过50", trigger: "blur" }
        ],
        telNum: [
          { required: true, message: "请填写", trigger: "blur" },
          { max: 50, message: "长度不允许超过50", trigger: "blur" }
        ],
        invoiceType: [{ required: true, message: "请选择", trigger: "blur" }],
        settlementMethod: [
          { required: true, message: "请选择", trigger: "blur" }
        ],
        classifyMark: [
          { required: true, message: "请填写", trigger: "blur" },
          { max: 50, message: "长度不允许超过50", trigger: "blur" }
        ],
        bank: [
          { required: true, message: "请填写", trigger: "blur" },
          { max: 50, message: "长度不允许超过50", trigger: "blur" }
        ],
        receiveBankName: [
          { required: true, message: "请填写", trigger: "blur" },
          { max: 50, message: "长度不允许超过50", trigger: "blur" }
        ],
        openingBank: [
          { required: true, message: "请填写", trigger: "blur" },
          { max: 50, message: "长度不允许超过50", trigger: "blur" }
        ],
        account: [
          { required: true, message: "请填写", trigger: "blur" },
          { max: 50, message: "长度不允许超过50", trigger: "blur" }
        ],
        financeName: [
          { required: true, message: "请填写", trigger: "blur" },
          { max: 50, message: "长度不允许超过50", trigger: "blur" }
        ],
        financeTel: [
          { required: true, message: "请填写", trigger: "blur" },
          { max: 50, message: "长度不允许超过50", trigger: "blur" }
        ]
      }, //校验规则

      //*****自定义属性*****
      provinceSel: [],
      citySel: [],
      districtSel: []
    };
  },
  created() {
    this.getConditionData(); //获取查询条件数据
    this.fetchData(); //页面首次加载时默认搜索
  },
  watch: {},
  methods: {
    //*****自定义方法*****
    getProvince() {
      appVenderSvc
        .getRegionChinaListByRegionId({ regionId: 0 })
        .then(
          res => {
            this.provinceSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    getCity() {
      if (
        this.formModel.provinceId != "" &&
        this.formModel.provinceId != undefined
      ) {
        appVenderSvc
          .getRegionChinaListByRegionId({ regionId: this.formModel.provinceId })
          .then(
            res => {
              this.citySel = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },
    getDistrict() {
      if (this.formModel.cityId != "" && this.formModel.cityId != undefined) {
        appVenderSvc
          .getRegionChinaListByRegionId({ regionId: this.formModel.cityId })
          .then(
            res => {
              this.districtSel = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },
    isEmpty(property) {
      return (
        property === null || property === "" || typeof property === "undefined"
      );
    },
    cellValue(row, columnName) {
      var text = "";
      switch (columnName) {
        case "settlementMethod":
          if (row.settlementMethod == 1) {
            text = "现结";
          } else if (row.settlementMethod == 3) {
            text = "月结";
          }
          break;
        case "isActive":
          if (row.isDeleted == 1) {
            text = "已删除";
          } else {
            if (row.isActive == 1) {
              text = "正常";
            } else {
              text = "已停用";
            }
          }
          break;
      }
      return text;
    },
    handleClose(done) {
      this.formVisible = false;
      // this.$confirm("确认关闭？")
      //   .then(_ => {
      //     this.formVisible = false;
      //     done();
      //   })
      //   .catch(_ => {});
    },
    handleSubmit() {
      this.$refs["formModel"].validate(valid => {
        if (valid) {
          this.$confirm("确定操作吗！", "警告", {
            confirmButtonText: "确定",
            cancelButtonText: "取消",
            type: "warning"
          })
            .then(() => {
              if (this.formModel.settlementMethod == 3) {
                if (!this.formModel.paymentDay > 0) {
                  this.$messageWarning("选择账期后，必须录入账期");
                  return;
                }
              }

              var provinceName = "";
              for (var i = 0; i < this.provinceSel.length; i++) {
                if (this.provinceSel[i].regionId == this.formModel.provinceId) {
                  provinceName = this.provinceSel[i].name;
                  break;
                }
              }

              var cityName = "";
              for (var i = 0; i < this.citySel.length; i++) {
                if (this.citySel[i].regionId == this.formModel.cityId) {
                  cityName = this.citySel[i].name;
                  break;
                }
              }

              var districtName = "";
              for (var i = 0; i < this.districtSel.length; i++) {
                if (this.districtSel[i].regionId == this.formModel.districtId) {
                  districtName = this.districtSel[i].name;
                  break;
                }
              }

              this.formModel.provinceName = provinceName;
              this.formModel.cityName = cityName;
              this.formModel.districtName = districtName;

              var vm = this;
              appVenderSvc
                .upsertVender(this.formModel)
                .then(
                  res => {
                    if (res.code == 10000) {
                      this.$message({
                        message: res.message,
                        type: "success"
                      });
                      vm.search();
                      this.formVisible = false;
                    } else {
                      this.$message({
                        message: res.message,
                        type: "warning"
                      });
                    }
                  },
                  error => {
                    console.log(error);
                  }
                )
                .catch(() => {});
            })
            .catch(() => {});
        }
      });
    },
    getDetail(id) {
      appVenderSvc
        .getVender({ id: id })
        .then(res => {
          let data = res.data;
          this.formModel = data;
          this.getCity();
          this.getDistrict();
        })
        .catch(err => {
          console.error(err);
        })
        .finally(() => {});
    }, //查询详情
    activeVender(row, isActive) {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          appVenderSvc
            .activeVender({ id: row.id, isActive: isActive })
            .then(
              res => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  vm.search();
                  this.formVisible = false;
                } else {
                  this.$message({
                    message: res.message,
                    type: "warning"
                  });
                }
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },
    //*****固定方法*****
    fetchData() {
      // this.condition.status = 1;
      this.getPaged();
      this.getProvince();
    }, //列表查询
    getConditionData() {}, //获取查询条件
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    }, //分页器改变事件
    getPaged(flag) {
      this.tableLoading = true;
      appVenderSvc
        .searchVender(this.condition)
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
        return this.$alertWarning(`已删除，不允许查看详情！`);
      }
      this.resetForm();
      tb.toggleRowSelection(row);
      tb.clearSelection();
      this.getDetail(row.id);
      this.formTitle = "详情";
      this.formVisible = true;
      this.formCheck = true;
      this.formCheckEdit = true;
    },
    add() {
      this.resetForm();
      this.formTitle = "新增";
      this.formVisible = true;
      this.formCheck = false;
      this.formCheckEdit = false;
    }, //新增弹窗
    edit(row) {
      this.resetForm();
      this.getDetail(row.id);
      this.formTitle = "修改";
      this.formVisible = true;
      this.formCheck = false;
      this.formCheckEdit = false;
    }, //编辑弹窗
    save(formName) {
      var vm = this;
      this.$refs[formName].validate(valid => {
        if (valid) {
          vm.btnSaveLoading = true;
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
      this.formModel = this.formInit;
    }, //重置表单
    resetSel() {} //重置查询条件
  }
};
</script>
