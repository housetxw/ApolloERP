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
        <el-form-item label="姓名">
          <el-input
            v-model="condition.employeeName"
            size="small"
            style="width:100px"
            autocomplete="off"
            placeholder="员工姓名"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item label="手机">
          <el-input
            v-model="condition.employeeName"
            size="small"
            style="width:100px"
            autocomplete="off"
            placeholder="员工手机"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item label="是否禁用">
          <el-select
            v-model="condition.isDeleted"
            size="small"
            placeholder="请选择"
            style="width:100px"
          >
            <el-option
              v-for="item in flag"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add">新增</el-button>
      </template>
      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <el-table-column label="姓名" prop="name" width="90"></el-table-column>
        <el-table-column label="手机号" prop="mobile" show-overflow-tooltip width="100"></el-table-column>
        <el-table-column label="性别" prop="gender" width="50" align="center"></el-table-column>
        <el-table-column label="岗位" prop="jobName" min-width="120">
          <template
            slot-scope="scope"
          >{{scope.row.jobName}} {{scope.row.workKindName?' - '+scope.row.workKindName:""}}</template>
        </el-table-column>
        <el-table-column label="禁用" prop="isDeleted" width="60" align="center">
          <template slot-scope="scope">{{scope.row.isDeleted === false ? '否' : '是'}}</template>
        </el-table-column>
        <el-table-column label="操作" fixed="right" align="center" width="140">
          <template slot-scope="scope">
            <el-button-group>
              <el-button type="primary" size="mini" @click="edit(scope.row)">编辑</el-button>
              <el-button type="danger" size="mini" @click="deleted(scope.row)">禁用</el-button>
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
        width="78%"
        maxWidth="800px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-form :model="formModel" :rules="rules" ref="formModel">
            <el-row>
              <el-col :span="8">
                <el-form-item label="员工姓名" :label-width="formLabelWidth" prop="name">
                  <el-input
                    size="medium"
                    v-model="formModel.name"
                    autocomplete="off"
                    clearable
                    :disabled="formCheck"
                  ></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="性别" :label-width="formLabelWidth" prop="gender">
                  <el-select
                    v-model="formModel.gender"
                    size="medium"
                    placeholder="请选择性别"
                    width="100%"
                    :disabled="formCheck"
                  >
                    <el-option
                      v-for="item in genderSel"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="8">
                <el-form-item label="职位" :label-width="formLabelWidth" prop="jobId">
                  <el-cascader
                    v-model="formModel.jobId"
                    :options="jobSel"
                    :props="{ expandTrigger: 'hover' }"
                    separator="-"
                    filterable
                    :disabled="formCheck"
                    @change="handleJobChange"
                  ></el-cascader>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="8">
                <el-form-item label="手机号码" :label-width="formLabelWidth" prop="mobile">
                  <el-input
                    size="medium"
                    v-model="formModel.mobile"
                    autocomplete="off"
                    clearable
                    :disabled="formCheck"
                  ></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="邮箱" :label-width="formLabelWidth" prop="email">
                  <el-input
                    size="medium"
                    v-model="formModel.email"
                    autocomplete="off"
                    clearable
                    :disabled="formCheck"
                  ></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="工号" :label-width="formLabelWidth" prop="number">
                  <el-input
                    size="medium"
                    v-model="formModel.number"
                    autocomplete="off"
                    clearable
                    :disabled="formCheck"
                  ></el-input>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="8">
                <el-form-item label="身份证号" :label-width="formLabelWidth" prop="identity">
                  <el-input
                    size="medium"
                    v-model="formModel.identity"
                    autocomplete="off"
                    clearable
                    :disabled="formCheck"
                  ></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="微信号" :label-width="formLabelWidth" prop="weChat">
                  <el-input
                    size="medium"
                    v-model="formModel.weChat"
                    autocomplete="off"
                    clearable
                    :disabled="formCheck"
                  ></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="8">
                <el-form-item label="QQ" :label-width="formLabelWidth" prop="qq">
                  <el-input
                    size="medium"
                    v-model="formModel.qq"
                    autocomplete="off"
                    clearable
                    :disabled="formCheck"
                  ></el-input>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item label="描述" :label-width="formLabelWidth" prop="description">
                  <el-input
                    size="medium"
                    type="textarea"
                    :rows="2"
                    v-model="formModel.description"
                    :disabled="formCheck"
                  ></el-input>
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            :disabled="formCheck"
            :loading="btnSaveLoading"
            @click="save('formModel')"
          >保存</el-button>
        </template>
      </rg-dialog>
    </section>
    <!-- 新增和编辑页面 -->
  </main>
</template>

<script>
import { accountSvc } from "@/api/accountauthority/account";
import { comBusSvc, empSvc } from "@/api/accountauthority/employee";
export default {
  name: "demopage",
  data() {
    return {
      w_search_right: 100,
      tableLoading: false,
      flag: this.global.deletedFlag,
      type: this.global.employeeSrchType,
      condition: {
        LikeType: 0,
        LikeValue: "",
        OrderStatus: 0,
        OrderChannel: 0,
        DeliveryMethod: 0,
        DeliveryStatus: -1,
        IsNeedInstall: -1,
        InstallStatus: -1,
        PayMethod: -1,
        PayStatus: -1,
        OrderType: 0,
        DateTimeType: 0,
        ExceptionOrder: 0,
        UserName:"",
        CreateDate: ["", ""],
        InstallTime:["",""],
        pageIndex: 1,
        pageSize: 20
      },
      tableData: [],
      totalList: 0,

      organizationSel: [],
      departmentSel: [],
      jobSel: [],
      genderSel: this.global.genderSel,
      formVisible: false,
      formTitle: "详情",
      formCheck: false,
      btnSaveLoading: false,
      formType: this.global.employeeFormType,
      formInit: {
        employeeId: undefined,
        type: 1,
        name: "",
        gender: 0,
        email: "",
        mobile: "",
        identity: "",
        organizationId: undefined,
        departmentId: undefined,
        level: 0,
        workKindId: 0,
        jobId: [],
        number: "",

        weChat: "",
        qq: "",
        description: ""
      },
      formModel: {
        employeeId: undefined,
        type: 1,
        name: "",
        gender: 0,
        email: "",
        mobile: "",
        identity: "",
        organizationId: undefined,
        departmentId: undefined,
        level: 0,
        workKindId: 0,
        jobId: [],
        number: "",

        weChat: "",
        qq: "",
        description: ""
      },
      rules: {
        name: [
          { required: true, message: "请输入员工姓名", trigger: "blur" },
          { max: 50, message: "员工姓名长度不允许超过50", trigger: "blur" }
        ],
        email: [
          // { required: true, message: "请输入邮箱", trigger: "blur" },
          { max: 50, message: "邮箱长度不允许超过50", trigger: "blur" }
        ],
        mobile: [
          { required: true, message: "请输入手机号", trigger: "blur" },
          { max: 20, message: "手机号长度不允许超过20", trigger: "blur" }
        ],
        identity: [
          { required: true, message: "请输入身份证号", trigger: "blur" },
          { max: 30, message: "身份证号长度不允许超过30", trigger: "blur" }
        ],
        number: [
          // { required: true, message: "请输入员工工号", trigger: "blur" },
          { max: 10, message: "员工工号长度不允许超过10", trigger: "blur" }
        ],
        weChat: [
          { max: 30, message: "WeChat长度不允许超过30", trigger: "blur" }
        ],
        qq: [{ max: 30, message: "QQ号长度不允许超过30", trigger: "blur" }],
        description: [
          { max: 255, message: "描述长度不允许超过255", trigger: "blur" }
        ],

        organizationId: [
          { required: true, message: "请选择所属单位", trigger: "blur" }
        ],
        jobId: [{ required: true, message: "请选择职位", trigger: "blur" }],
        type: [{ required: true, message: "请选择员工类型", trigger: "blur" }]
      },
      formLabelWidth: "100px",

      originalDeptId: -1
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    fetchData() {
      this.getConditionData();
      this.getPaged();
    },
    getUnitsForSelByType(type) {
      comBusSvc
        .getUnitsForSelByType({ type: type })
        .then(
          res => {
            this.organizationSel = res.data;
          },
          err => {
            console.error(err);
          }
        )
        .catch(() => {});
    },
    getDepartmentSelByOrgIdType(orgId, type) {
      empSvc
        .getDepartmentSelByOrgIdType({ organizationId: orgId, type: type })
        .then(
          res => {
            this.departmentSel = res.data;
          },
          err => {
            console.error(err);
          }
        )
        .catch(() => {});
    },
    getJobListByOrgIdForWeb(orgId, type) {
      empSvc
        .getJobListByOrgIdForWeb({ organizationId: orgId, type: type })
        .then(
          res => {
            this.jobSel = res.data;
          },
          err => {
            console.error(err);
          }
        )
        .catch(() => {});
    },
    getEmployeeInfo(empId, orgId, empType) {
      empSvc
        .getEmployeeInfo({ employeeId: empId })
        .then(res => {
          let data = res.data;
          this.formModel = data;
          this.formModel.employeeId = data.id;
          if (data.type == 1 || data.departmentId == 0) {
            this.formModel.departmentId = undefined;
          } else {
            this.originalDeptId = data.departmentId;
          }

          if (data.isTechnican) {
            let jobId = this.formModel.jobId;
            this.formModel.jobId = [];

            this.formModel.jobId.push(jobId);
            this.formModel.jobId.push(data.workKindId);
            this.formModel.jobId.push(data.level);
          }

          this.getUnitsForSelByType(empType);
          this.getDepartmentSelByOrgIdType(orgId, empType);
          this.getJobListByOrgIdForWeb(orgId, empType);
        })
        .catch(err => {
          console.error(err);
        })
        .finally(() => {});
    },
    getConditionData() {
      // this.getAllOrganizationExceptShopSelectListAsync();
    },
    filterOrgType(value) {
      let newType = JSON.parse(JSON.stringify(this.formType));
      for (let index = 0; index < newType.length; index++) {
        let item = newType[index];
        if (item.value === value) {
          return item.label;
        }
      }
    },
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },

    getPaged(flag) {
      let data = JSON.parse(JSON.stringify(this.condition));
      if (data.organizationId) {
        var arr = data.organizationId.split("|");
        data.organizationId = arr[0];
        data.type = arr[1];
      }

      this.tableLoading = true;
      empSvc
        .getEmployeePage(data)
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
        return this.$alertWarning(`员工 ${row.name} 已禁用，不允许查看详情！`);
      }

      this.resetSel();
      tb.toggleRowSelection(row);
      tb.clearSelection();

      this.formTitle = "详情";
      this.formVisible = true;
      this.formCheck = true;

      this.getEmployeeInfo(row.id, row.organizationId, row.employeeType);
    },
    handleTypeChange(value) {
      this.formModel.organizationId = undefined;
      this.formModel.departmentId = undefined;
      this.formModel.jobId = [];

      this.getUnitsForSelByType(value);
    },
    handleOrgChange(value) {
      this.formModel.jobId = [];

      this.getDepartmentSelByOrgIdType(value, this.formModel.type);
      this.getJobListByOrgIdForWeb(value, this.formModel.type);
    },
    handleJobChange(value) {
      // console.log(`jobId: ${value}`);
    },
    add() {
      this.resetSel();
      this.formTitle = "新增";
      this.formVisible = true;
      this.getUnitsForSelByType(this.formModel.type);
    },
    edit(sel) {
      this.formTitle = "编辑";
      this.formVisible = true;
      this.getEmployeeInfo(sel.id, sel.organizationId, sel.employeeType);
    },
    save(formName) {
      var vm = this;
      // console.log("save formModel: " + JSON.stringify(this.formModel));
      this.$refs[formName].validate(valid => {
        if (valid) {
          vm.btnSaveLoading = true;

          var jobIds = this.formModel.jobId;
          var len = jobIds.length;
          if (this.formModel.type == 1 && len != 1) {
            this.formModel.jobId = jobIds[0];
            this.formModel.workKindId = jobIds[1];
            this.formModel.level = jobIds[2];
          } else {
            this.formModel.jobId = jobIds[0] == undefined ? jobIds : jobIds[0];
            this.formModel.workKindId = 0;
            this.formModel.level = 0;
          }

          empSvc
            .createOrUpdateEmployeeForWeb(this.formModel)
            .then(
              res => {
                if (res.data) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  vm.search();
                  if (vm.formVisible) {
                    vm.getConditionData();
                  }
                  vm.cancel();
                }
              },
              err => {
                //恢复所选的职位
                this.formModel.jobId = [];

                var len = jobIds.length;
                if (this.formModel.type == 1 && len != 1) {
                  this.formModel.jobId.push(jobIds[0]);
                  this.formModel.jobId.push(jobIds[1]);
                  this.formModel.jobId.push(jobIds[2]);
                } else {
                  this.formModel.jobId.push(jobIds[0]);
                  this.formModel.workKindId = 0;
                  this.formModel.level = 0;
                }
              }
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
      let msg = `是否要禁用员工 ${sel.name}?`;
      this.$confirmWarning(msg).then(
        () => {
          this.tableLoading = true;
          empSvc
            .batchDeleteEmployeeById({ EmployeeIds: sel.id })
            .then(
              res => {
                if (res.data) {
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
          console.error(JSON.stringify(cancel));
        }
      );
    },
    cancel(formName) {
      this.formVisible = false;
      this.formCheck = false;

      this.resetForm();

      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },
    resetForm() {
      this.formModel = JSON.parse(JSON.stringify(this.formInit));
    },
    resetSel() {
      this.organizationSel = [];
      this.jobSel = [];
    }
  }
};
</script>
