<template>
  <main class="container account-authority">
    <!-- 首页 -->
    <section id="indexPage">
      <!-- 搜索 -->
      <rg-page
        background
        id="indexPage"
        :pageIndex="condition.pageIndex"
        :pageSize="condition.pageSize"
        :dataCount="totalList"
        :tableLoading="tableLoading"
        :tableData="tableData"
        :pageChange="pageTurning"
        :headerBtnWidth="180"
        :searching="search"
        :tbRowClick="tableRowClick"
        element-loading-text="加载中..."
        ref="refEmpTable"
        :tbRowDblClick="tableRowDblClick"
        :defaultCollapse="false"
        :handleSelectionChange="tableSelectionChange"
      >
        <!-- <el-form-item label="所属单位:">
            <el-select
              v-model="condition.organizationId"
              size="small"
              clearable
              filterable
              placeholder="请选择所属单位"
            >
              <el-option
                v-for="item in organizationSel"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              >
                <span style="float: left">{{ item.name }}</span>
                <el-tag size="medium" type="info" style="float: right;">{{filterOrgType(item.type)}}</el-tag>
              </el-option>
            </el-select>
        </el-form-item>-->
        <template v-slot:condition>
          <el-form-item prop="employeeName">
            <el-input
              v-model="condition.employeeName"
              size="mini"
              autocomplete="off"
              placeholder="请输入员工名称"
              clearable
            ></el-input>
          </el-form-item>
          <el-form-item>
            <el-input
              v-model="condition.organizationName"
              size="mini"
              autocomplete="off"
              placeholder="请输入所属单位名称"
              clearable
            ></el-input>
          </el-form-item>
          <el-form-item prop="mobile">
            <el-input
              v-model="condition.mobile"
              size="mini"
              autocomplete="off"
              placeholder="请输入员工手机号"
              clearable
            ></el-input>
          </el-form-item>
          <el-form-item label="员工类型">
            <el-select
              v-model="condition.type"
              size="mini"
              placeholder="请选择员工类型"
            >
              <el-option
                v-for="item in type"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="是否禁用">
            <el-select
              v-model="condition.isDeleted"
              size="mini"
              placeholder="请选择"
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
          <!-- <el-button type="primary" size="small" icon="el-icon-view">查看</el-button> -->
          <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
            >新增</el-button
          >
          <el-button size="mini" icon="el-icon-edit" @click="edit"
            >编辑</el-button
          >
          <el-button
            type="danger"
            size="mini"
            icon="el-icon-delete"
            @click="deleted()"
            >禁用</el-button
          >
        </template>

        <!-- 搜索 -->
        <!-- 列表 -->
        <template v-slot:tb_cols>
          <el-table-column type="selection"></el-table-column>
          <!-- Table数据 -->
          <el-table-column
            label="员工Id"
            prop="employeeId"
            v-if="false"
          ></el-table-column>
          <el-table-column
            label="账号Id"
            prop="accountId"
            v-if="false"
          ></el-table-column>
          <el-table-column
            label="员工姓名"
            prop="name"
            width="90"
          ></el-table-column>
          <el-table-column label="员工类型" prop="employeeType" width="90">
            <template slot-scope="scope">
              <el-tag size="medium" type="info">{{
                filterOrgType(scope.row.employeeType)
              }}</el-tag>
            </template>
          </el-table-column>
          <el-table-column
            label="手机号"
            prop="mobile"
            show-overflow-tooltip
            width="110"
          ></el-table-column>
          <el-table-column
            label="所属单位Id"
            prop="organizationId"
            v-if="false"
          ></el-table-column>
          <el-table-column
            label="所属单位"
            prop="organizationName"
            show-overflow-tooltip
          ></el-table-column>
          <el-table-column
            label="部门Id"
            prop="departmentId"
            v-if="false"
          ></el-table-column>
          <el-table-column
            label="部门"
            prop="departmentName"
            show-overflow-tooltip
          ></el-table-column>
          <el-table-column
            label="岗位Id"
            prop="jobId"
            v-if="false"
          ></el-table-column>
          <el-table-column
            label="岗位"
            prop="jobName"
            show-overflow-tooltip
            width="120"
          >
            <template slot-scope="scope"
              >{{ scope.row.jobName }}
              {{ scope.row.workKindName ? " - " + scope.row.workKindName : ""
              }}{{ scope.row.level > 0 ? " - " + scope.row.level + "级" : "" }}
            </template>
          </el-table-column>
          <el-table-column label="是否禁用" prop="isDeleted" width="80">
            <template slot-scope="scope">
              <el-tag
                size="medium"
                :type="scope.row.isDeleted === false ? '' : 'danger'"
                >{{ scope.row.isDeleted === false ? "否" : "是" }}</el-tag
              >
            </template>
          </el-table-column>
          <el-table-column
            label="创建人"
            prop="createBy"
            show-overflow-tooltip
            width="110"
          ></el-table-column>
          <el-table-column
            label="创建人Id"
            prop="createId"
            v-if="false"
          ></el-table-column>
          <el-table-column
            label="创建时间"
            prop="createTime"
            show-overflow-tooltip
            width="110"
          ></el-table-column>
          <el-table-column
            label="修改人"
            prop="updateBy"
            show-overflow-tooltip
            width="110"
          ></el-table-column>
          <el-table-column
            label="修改人Id"
            prop="updateId"
            v-if="false"
          ></el-table-column>
          <el-table-column
            label="修改时间"
            prop="updateTime"
            show-overflow-tooltip
            width="110"
          ></el-table-column>
        </template>
      </rg-page>
      <!-- 列表 -->
    </section>
    <!-- 首页 -->
    <!-- 新增和编辑页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        :title="formTitle"
        :visible.sync="formVisible"
        :beforeClose="cancel"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            cancel();
          },
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="1000px"
        minWidth="800px"
      >
        <template v-slot:content>
          <el-form :model="formModel" :rules="rules" ref="formModel">
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="员工姓名"
                  :label-width="formLabelWidth"
                  prop="name"
                >
                  <el-input
                    size="medium"
                    v-model="formModel.name"
                    autocomplete="off"
                    clearable
                    :disabled="formCheck"
                  ></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="16">
                <el-form-item
                  label="性别"
                  :label-width="formLabelWidth"
                  prop="gender"
                >
                  <el-select
                    v-model="formModel.gender"
                    size="medium"
                    placeholder="请选择性别"
                    :disabled="formCheck"
                    style="width: 100%"
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
              <el-col :span="16">
                <el-form-item
                  label="员工类型"
                  :label-width="formLabelWidth"
                  prop="type"
                >
                  <el-select
                    v-model="formModel.type"
                    size="medium"
                    placeholder="请选择员工类型"
                    :disabled="formCheck"
                    @change="handleTypeChange"
                    style="width: 100%"
                  >
                    <el-option
                      v-for="item in formType"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="所属单位"
                  :label-width="formLabelWidth"
                  prop="organizationId"
                >
                  <el-select
                    v-model="formModel.organizationId"
                    size="medium"
                    filterable
                    placeholder="请选择所属单位"
                    :disabled="formCheck"
                    @change="handleOrgChange"
                    style="width: 100%"
                  >
                    <el-option
                      v-for="item in organizationSel"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
              <el-col :span="24">
                <el-row>
                  <el-col :span="10">
                    <el-form-item label="部门" :label-width="formLabelWidth">
                      <el-select
                        v-model="formModel.departmentId"
                        size="medium"
                        placeholder="请选择部门"
                        width="100%"
                        :disabled="true"
                      >
                        <el-option
                          v-for="item in departmentSel"
                          :key="item.value"
                          :label="item.label"
                          :value="item.value"
                        ></el-option>
                      </el-select>
                    </el-form-item>
                  </el-col>
                  <el-col :span="6">
                    <el-button
                      size="medium"
                      icon="el-icon-zoom-in"
                      @click="selDept"
                      :disabled="formModel.type == 1 || formCheck"
                      >选择</el-button
                    >
                  </el-col>
                </el-row>
              </el-col>
              <el-col :span="16">
                <el-form-item label="职位" :label-width="formLabelWidth">
                  <!-- <el-select
                  v-model="formModel.jobId"
                  size="medium"
                  placeholder="请选择所属单位"
                  width="100%"
                  :disabled="formCheck"
                >
                  <el-option
                    v-for="item in jobSel"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  ></el-option>
                </el-select>-->
                  <el-cascader
                    v-model="formModel.jobId"
                    :options="jobSel"
                    :props="{ expandTrigger: 'hover' }"
                    separator="-"
                    filterable
                    :disabled="formCheck"
                    @change="handleJobChange"
                    style="width: 100%"
                  ></el-cascader>
                </el-form-item>
              </el-col>
            </el-row>
            <!-- <el-row>
            <el-col :span="24">
              <el-form-item label="角色" :label-width="formLabelWidth" prop="roleIds">
                <el-select
                  v-model="formModel.roleIds"
                  size="medium"
                  multiple
                  clearable
                  placeholder="请选择角色"
                  :disabled="formCheck"
                  style="width:64%"
                >
                  <el-option
                    v-for="item in formType"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  ></el-option>
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>-->
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="手机号码"
                  :label-width="formLabelWidth"
                  prop="mobile"
                >
                  <el-input
                    size="medium"
                    v-model="formModel.mobile"
                    autocomplete="off"
                    clearable
                    :disabled="formCheck"
                  ></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="16">
                <el-form-item
                  label="邮箱"
                  :label-width="formLabelWidth"
                  prop="email"
                >
                  <el-input
                    size="medium"
                    v-model="formModel.email"
                    autocomplete="off"
                    clearable
                    :disabled="formCheck"
                  ></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="16">
                <el-form-item
                  label="工号"
                  :label-width="formLabelWidth"
                  prop="number"
                >
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
              <el-col :span="16">
                <el-form-item
                  label="身份证号"
                  :label-width="formLabelWidth"
                  prop="identity"
                >
                  <el-input
                    size="medium"
                    v-model="formModel.identity"
                    autocomplete="off"
                    clearable
                    :disabled="formCheck"
                  ></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="16">
                <el-form-item
                  label="微信号"
                  :label-width="formLabelWidth"
                  prop="weChat"
                >
                  <el-input
                    size="medium"
                    v-model="formModel.weChat"
                    autocomplete="off"
                    clearable
                    :disabled="formCheck"
                  ></el-input>
                </el-form-item>
              </el-col>
              <el-col :span="16">
                <el-form-item
                  label="QQ"
                  :label-width="formLabelWidth"
                  prop="qq"
                >
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
              <el-col :span="16">
                <el-form-item
                  label="描述"
                  :label-width="formLabelWidth"
                  prop="description"
                >
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
            type="primary"
            :loading="btnSaveLoading"
            @click="save('formModel')"
            size="mini"
            >保存</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 新增和编辑页面 -->
    <!-- 权限树页面 -->
    <section id="deptTree">
      <rg-dialog
        title="部门列表"
        :visible.sync="formDeptTreeVisible"
        :beforeClose="cancelDeptTree"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            cancelDeptTree();
          },
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="1000px"
        minWidth="800px"
        :close-on-click-modal="false"
      >
        <template v-slot:content>
          <el-input
            placeholder="请输入部门名称"
            size="small"
            v-model="deptFilterText"
            clearable
            style="margin-bottom: 10px"
          ></el-input>
          <el-tree
            :data="deptTreeList"
            show-checkbox
            node-key="id"
            ref="deptTree"
            @node-click="deptNodeChange"
            @check="deptNodeChange"
            :check-strictly="true"
            :default-expanded-keys="defaultExpandedKeys"
            :default-checked-keys="defaultCheckedKeys"
            :props="defaultProps"
            :filter-node-method="deptFilter"
            :check-on-click-node="true"
            :highlight-current="true"
            class="custom-el-dialog-vh"
          ></el-tree>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            type="primary"
            :loading="btnSaveDeptTreeLoading"
            @click="saveDeptTree"
            size="mini"
            >保存</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 权限树页面 -->
  </main>
</template>

<script>
import { accountSvc } from "@/api/accountauthority/account";
import { comBusSvc, empSvc } from "@/api/accountauthority/employee";

export default {
  name: "employee",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      flag: this.global.deletedFlag,
      type: this.global.employeeSrchType,
      condition: {
        organizationId: undefined,
        organizationName: "",
        mobile: "",
        type: undefined,
        employeeName: "",
        isDeleted: undefined,
        pageIndex: 1,
        pageSize: 10,
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
      // formType: this.global.employeeFormType,
      formType: [
        { value: 0, label: "公司" },
        { value: 1, label: "门店" },
        { value: 2, label: "仓库" },
        { value: 3, label: "拓展" },
        { value: 4, label: "供应商" },
      ],
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
        roleIds: [],
        number: "",

        weChat: "",
        qq: "",
        description: "",
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
        roleIds: [],
        number: "",

        weChat: "",
        qq: "",
        description: "",
      },
      rules: {
        name: [
          { required: true, message: "请输入员工姓名", trigger: "blur" },
          { max: 50, message: "员工姓名长度不允许超过50", trigger: "blur" },
        ],
        email: [
          // { required: true, message: "请输入邮箱", trigger: "blur" },
          { max: 50, message: "邮箱长度不允许超过50", trigger: "blur" },
        ],
        mobile: [
          { required: true, message: "请输入手机号", trigger: "blur" },
          { max: 20, message: "手机号长度不允许超过20", trigger: "blur" },
        ],
        identity: [
          { required: true, message: "请输入身份证号", trigger: "blur" },
          { max: 30, message: "身份证号长度不允许超过30", trigger: "blur" },
        ],
        number: [
          // { required: true, message: "请输入员工工号", trigger: "blur" },
          { max: 10, message: "员工工号长度不允许超过10", trigger: "blur" },
        ],
        weChat: [
          { max: 30, message: "WeChat长度不允许超过30", trigger: "blur" },
        ],
        qq: [{ max: 30, message: "QQ号长度不允许超过30", trigger: "blur" }],
        description: [
          { max: 255, message: "描述长度不允许超过255", trigger: "blur" },
        ],

        type: [{ required: true, message: "请选择员工类型", trigger: "blur" }],
        organizationId: [
          { required: true, message: "请选择所属单位", trigger: "blur" },
        ],
        // jobId: [{ required: true, message: "请选择职位", trigger: "blur" }]
        // roleIds: [{ required: true, message: "请选择角色", trigger: "blur" }]
      },
      formLabelWidth: "100px",

      originalDeptId: -1,
      formDeptTreeVisible: false,
      btnSaveDeptTreeLoading: false,
      defaultProps: {
        children: "children",
        label: "label",
      },
      defaultExpandedKeys: [],
      defaultCheckedKeys: [],
      deptTreeList: [],
      deptFilterText: "",
    };
  },
  created() {
    this.fetchData();
  },
  watch: {
    deptFilterText(val) {
      this.$refs.deptTree.filter(val);
    },
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
          (res) => {
            this.organizationSel = res.data;
          },
          (err) => {
            console.log(err);
          }
        )
        .catch(() => {});
    },
    getDepartmentSelByOrgIdType(orgId, type) {
      empSvc
        .getDepartmentSelByOrgIdType({ organizationId: orgId, type: type })
        .then(
          (res) => {
            this.departmentSel = res.data;
          },
          (err) => {
            console.log(err);
          }
        )
        .catch(() => {});
    },
    getDepartmentTreeByOrgIdType(orgId, type) {
      empSvc
        .getDepartmentTreeByOrgIdType({ organizationId: orgId, type: type })
        .then(
          (res) => {
            this.deptTreeList = res.data;
          },
          (err) => {
            console.log(err);
          }
        )
        .catch(() => {});
    },
    getJobListByOrgIdForWeb(orgId, type) {
      empSvc
        .getJobListByOrgIdForWeb({ organizationId: orgId, type: type })
        .then(
          (res) => {
            this.jobSel = res.data;
          },
          (err) => {
            console.log(err);
          }
        )
        .catch(() => {});
    },
    getEmployeeInfo(empId, orgId, empType) {
      empSvc
        .getEmployeeInfo({ employeeId: empId })
        .then((res) => {
          let data = res.data;
          this.formModel = data;
          this.formModel.employeeId = data.id;
          if (data.type == 1 || data.departmentId == 0) {
            this.formModel.departmentId = undefined;
          } else {
            this.originalDeptId = data.departmentId;
          }
          let len = this.formModel.jobId.length;

          debugger;
          let jobId = this.formModel.jobId;
          this.formModel.jobId = [];
          if (data.isTechnican) {
            debugger;
            this.formModel.jobId.push(jobId);
            this.formModel.jobId.push(data.workKindId);
            this.formModel.jobId.push(data.level);
          } else {
            this.formModel.jobId.push(jobId);
          }

          this.getUnitsForSelByType(empType);
          this.getDepartmentSelByOrgIdType(orgId, empType);
          this.getJobListByOrgIdForWeb(orgId, empType);
        })
        .catch((err) => {
          console.log(err);
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
    pageTurning(p) {
      this.currentPage = p.currentPage;
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

      console.log("data: " + JSON.stringify(data));

      this.tableLoading = true;
      empSvc
        .getEmployeePage(data)
        .then(
          (res) => {
            let data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (flag) {
              this.$messageSuccess(res.message || "查询成功");
            }
          },
          (err) => {
            console.log(err);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });
    },
    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    },
    tableSelectionChange(rows) {
      this.$refs.refEmpTable.selection = rows;
    },

    tableRowClick(row, column, event, refEmpTable) {
      console.log("tableRowClick: " + JSON.stringify(row));
      refEmpTable.toggleRowSelection(row);
      this.$refs.refEmpTable.selection = refEmpTable.selection;
    },
    tableRowDblClick(row, column, event, refEmpTable) {
      if (row.isDeleted) {
        this.$alert("员工 " + row.name + " 已禁用，不允许查看详情！", "警告", {
          confirmButtonText: "关闭",
          type: "warning",
        });
        return;
      }
      this.resetSel();
      console.log("tableRowDblClick: " + JSON.stringify(row));
      refEmpTable.toggleRowSelection(row);
      refEmpTable.clearSelection();

      this.formTitle = "详情";
      this.formVisible = true;
      this.formCheck = true;

      this.getEmployeeInfo(row.id, row.organizationId, row.employeeType);
    },
    handleTypeChange(value) {
      console.log(`EmpType: ${value}`);
      this.formModel.organizationId = undefined;
      this.formModel.departmentId = undefined;
      this.formModel.jobId = [];

      this.getUnitsForSelByType(value);
    },
    handleOrgChange(value) {
      console.log(`OrgType: ${value}`);
      this.formModel.jobId = [];

      this.getDepartmentSelByOrgIdType(value, this.formModel.type);
      this.getJobListByOrgIdForWeb(value, this.formModel.type);
    },
    handleJobChange(value) {
      console.log(`jobId: ${value}`);
    },
    add() {
      this.resetSel();
      this.formTitle = "新增";
      this.formVisible = true;
      this.getUnitsForSelByType(this.formModel.type);
    },
    edit() {
      this.resetSel();
      let sels = this.$refs.refEmpTable.selection;
      if (sels == undefined) {
        sels = [];
      }
      console.log(121, this.$refs.refEmpTable.selection);
      if (sels) {
        var msg = "";
        if (sels.length <= 0) {
          msg = "至少要选择一个要编辑的员工";
        } else if (sels.length != 1) {
          msg = "同时只能编辑一个员工";
        }
        if (msg.length > 0) {
          return this.$alertWarning(msg);
        }
      }

      let sel = sels[0];

      if (sel.isDeleted) {
        this.$alert("员工 " + sel.name + " 已禁用，不允许编辑！", "警告", {
          confirmButtonText: "关闭",
          type: "warning",
        });
        return;
      }

      this.formTitle = "编辑";
      this.formVisible = true;

      this.getEmployeeInfo(sel.id, sel.organizationId, sel.employeeType);
    },
    save(formName) {
      var vm = this;
      console.log("save formModel: " + JSON.stringify(this.formModel));
      this.$refs[formName].validate((valid) => {
        if (valid) {
          vm.btnSaveLoading = true;
          var jobIds = this.formModel.jobId;
          var len = jobIds.length;
          if (len > 0) {
            if (this.formModel.type == 1 && len != 1) {
              this.formModel.jobId = jobIds[0];
              this.formModel.workKindId = jobIds[1];
              this.formModel.level = jobIds[2];
            } else {
              this.formModel.jobId =
                jobIds[0] == undefined ? jobIds : jobIds[0];
              this.formModel.workKindId = 0;
              this.formModel.level = 0;
            }
          } else {
            this.formModel.jobId = 0;
          }

          // let unitType = {};
          // if (this.formModel.type == 0) {
          //   unitType = this.organizationSel.find((item) => {
          //     //model就是上面的数据源
          //     return item.value === this.formModel.organizationId; //筛选出匹配数据
          //   });
          //   this.formModel.type = unitType.type;
          //   console.log(unitType);
          //   console.log("type" + this.formModel.type);
          // }

          empSvc
            .createOrUpdateEmployeeForWeb(this.formModel)
            .then(
              (res) => {
                if (res.data) {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                  vm.search();
                  if (vm.formVisible) {
                    vm.getConditionData();
                  }
                  vm.cancel();
                }
              },
              (err) => {
                //恢复所选的职位
                this.formModel.jobId = [];

                var len = jobIds.length;
                if (len > 0) {
                  if (this.formModel.type == 1 && len != 1) {
                    this.formModel.jobId.push(jobIds[0]);
                    this.formModel.jobId.push(jobIds[1]);
                    this.formModel.jobId.push(jobIds[2]);
                  } else {
                    this.formModel.jobId.push(jobIds[0]);
                    this.formModel.workKindId = 0;
                    this.formModel.level = 0;
                  }
                } else {
                  this.formModel.jobId = 0;
                }
              }
            )
            .catch((err) => {
              console.log(err);
            })
            .finally(() => {
              vm.btnSaveLoading = false;
            });
        } else {
          return false;
        }
      });
    },
    deleted() {
      var vm = this;

      let sels = this.$refs.refEmpTable.selection;
      if (sels) {
        var msg = "";
        if (sels.length <= 0) {
          msg = "至少要选择一个要禁用的员工";
        }
        if (msg.length > 0) {
          return this.$alertWarning(msg);
        }
      }

      var msg = "";
      if (sels.length == 1) {
        msg = `是否要禁用员工 ${sels[0].name}?`;

        if (sels[0].isDeleted) {
          return this.$alertWarning("已禁用的员工不允许再次禁用");
        }
      } else {
        msg = "是否要禁用所选员工的员工?";
      }

      let ids = sels
        .filter((s) => {
          if (!s.isDeleted) {
            return s;
          }
        })
        .map((s) => s.id);

      if (ids.length <= 0) {
        return this.$alertWarning("至少要选择一个没有禁用的员工禁用");
      }

      this.$confirmWarning(msg).then(
        () => {
          this.tableLoading = true;

          empSvc
            .batchDeleteEmployeeById({ EmployeeIds: ids })
            .then(
              (res) => {
                if (res.data) {
                  this.$messageSuccess(res.message);
                  vm.search();
                } else {
                  this.$messageWarning(res.message);
                }
              },
              (err) => {
                console.log(err);
              }
            )
            .catch(() => {})
            .finally(() => {
              this.tableLoading = false;
            });
        },
        (cancel) => {
          console.log(JSON.stringify(cancel));
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
    },
    selDept() {
      console.log(
        "this.formModel.departmentId: " + this.formModel.departmentId
      );

      if (
        !this.formModel.organizationId ||
        this.formModel.organizationId <= 0
      ) {
        return this.$alertWarning("请选择公司！");
      }

      this.formDeptTreeVisible = true;

      this.getDepartmentTreeByOrgIdType(
        this.formModel.organizationId,
        this.formModel.type
      );

      if (this.originalDeptId == this.formModel.departmentId) {
        this.defaultExpandedKeys = this.defaultCheckedKeys = [];

        this.defaultExpandedKeys.push(this.formModel.departmentId);
        this.defaultCheckedKeys.push(this.formModel.departmentId);
      }
    },
    deptFilter(value, data) {
      if (!value) return true;
      return data.label.indexOf(value) !== -1;
    },
    deptNodeChange(data, node) {
      console.log("data: ", data);
      console.log("node: ", node);

      // if (data.children.length > 0) {
      //   return this.$messageWarning("请选择最后一级部门");
      // }

      let deptIds = this.$refs.deptTree.getCheckedKeys();
      let len = deptIds.length;

      if (len == 0) {
        this.formModel.departmentId = undefined;
        this.defaultExpandedKeys = this.defaultCheckedKeys = [];
        return;
      }

      for (let index = 0; index < len; index++) {
        this.$refs.deptTree.setChecked(
          deptIds[index],
          deptIds[index] == data.id ? true : false
        );
      }

      this.formModel.departmentId = data.id;
      this.defaultExpandedKeys = this.defaultCheckedKeys = [];
      this.defaultExpandedKeys.push(data.id);
      this.defaultCheckedKeys.push(data.id);
    },
    saveDeptTree() {
      console.log("HalfChecked: " + this.$refs.deptTree.getHalfCheckedKeys());
      console.log("Checked: " + this.$refs.deptTree.getCheckedKeys());

      var deptIds = this.$refs.deptTree.getCheckedKeys();
      var len = deptIds.length;
      if (len > 1) {
        return this.$alertWarning("只能选择一个部门");
      }
      if (len == 1) {
        this.formModel.departmentId = deptIds[0];

        this.defaultExpandedKeys = this.defaultCheckedKeys = [];
        this.defaultExpandedKeys.push(deptIds[0]);
        this.defaultCheckedKeys.push(deptIds[0]);
      }
      this.cancelDeptTree();
    },
    cancelDeptTree() {
      this.formDeptTreeVisible = false;
      this.deptTreeList = [];
    },
  },
};
</script>

<style lang="scss" scoped>
.el-input.is-disabled .el-input__inner,
.el-textarea.is-disabled .el-textarea__inner {
  background-color: #ffffff !important;
  color: #606266 !important;
}
</style>
