<template>
  <main class="container">
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
        ref="multipleEmpTable"
        :defaultCollapse="false"
        :handleSelectionChange="tableSelectionChange"
      >
        <!-- @change="changeOrganization" -->
        <template v-slot:condition>
          <el-form-item prop="organizationId">
            <el-select
              v-model="condition.organizationId"
              size="small"
              clearable
              filterable
              placeholder="请选择所属单位"
                 @change="changeOrganization"
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
          </el-form-item>
          <el-form-item prop="employeeName">
            <el-input
              v-model="condition.employeeName"
              size="small"
              autocomplete="off"
              placeholder="请输入员工名称"
              clearable
            ></el-input>
            <!-- <el-select
              v-model="condition.id"
              size="small"
              clearable
              filterable
              placeholder="请选择员工名称"
              @change="changeEmployee"
            >
              <el-option
                v-for="item in employeeSel"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              >
                <span style="float: left">{{ item.name }}</span>
                <el-tag size="medium" type="info" style="float: right;">{{ item.typeName }}</el-tag>
                <span
                  style="float: right;"
                  :class="[item.isDeleted?'el-tag el-tag--danger':'']"
                >{{ item.isDeleted ? "已禁用" : "" }}</span>
              </el-option>
            </el-select>-->
          </el-form-item>
          <el-form-item label="员工是否禁用" prop="isDeleted">
            <el-select v-model="condition.isDeleted" size="small" placeholder="请选择员工是否禁用">
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
          <el-button
            type="primary"
            size="small"
            icon="el-icon-search"
            @click="showEmpRole()"
            :disabled="disabledCheckEmpRole"
          >查看员工角色</el-button>
          <el-button
            type="primary"
            size="small"
            icon="el-icon-edit"
            @click="showEditEmpRole()"
            :disabled="disabledEditEmpRole"
          >编辑员工角色</el-button>
        </template>

        <!-- 搜索 -->
        <!-- 列表 -->
        <template v-slot:tb_cols>
          <!-- <el-table
            ref="multipleEmpTable"
            v-loading="tableLoading"
            element-loading-text="加载中..."
            :data="tableData"
            border
            style="width: 100%"
            @selection-change="tableSelectionChange"
            @row-click="tableRowClick"
          >-->
          <el-table-column type="selection"></el-table-column>
          <!-- Table数据 -->
          <el-table-column label="员工Id" prop="employeeId" v-if="false"></el-table-column>
          <el-table-column label="账号Id" prop="accountId" v-if="false"></el-table-column>
          <el-table-column label="登录账号" prop="accountName"></el-table-column>
          <el-table-column label="员工名称" prop="employeeName" show-overflow-tooltip></el-table-column>
          <el-table-column label="员工类型" prop="type" width="120">
            <template slot-scope="scope">
              <el-tag size="medium" type="info">{{filterOrgType(scope.row.type)}}</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="所属单位Id" prop="organizationId" v-if="false"></el-table-column>
          <el-table-column label="所属单位" prop="organizationName" show-overflow-tooltip width="240"></el-table-column>
          <el-table-column label="部门Id" prop="departmentId" v-if="false"></el-table-column>
          <el-table-column label="部门" prop="departmentName" show-overflow-tooltip></el-table-column>
          <el-table-column label="岗位Id" prop="jobId" v-if="false"></el-table-column>
          <el-table-column label="岗位" prop="jobName" show-overflow-tooltip></el-table-column>
          <el-table-column label="员工是否禁用" prop="isDeleted">
            <template slot-scope="scope">
              <el-tag
                size="medium"
                :type="scope.row.isDeleted === false ? '' : 'danger'"
              >{{scope.row.isDeleted === false ? '否' : '是'}}</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="创建人" prop="createBy" v-if="false"></el-table-column>
          <el-table-column label="创建人Id" prop="createId" v-if="false"></el-table-column>
          <el-table-column label="创建时间" prop="createTime" v-if="false"></el-table-column>
          <el-table-column label="修改人" prop="updateBy" v-if="false"></el-table-column>
          <el-table-column label="修改人Id" prop="updateId" v-if="false"></el-table-column>
          <el-table-column label="修改时间" prop="updateTime" v-if="false"></el-table-column>
          <!-- Table数据 -->
        </template>
      </rg-page>
      <!-- 列表 -->
    </section>
    <!-- 首页 -->
    <!-- 新增和编辑页面 -->
    <section id="rolePage">
      <rg-dialog
        :title="formCheckRole?'查看员工的角色':'编辑员工的角色'"
        :visible.sync="formVisible"
        :beforeClose="cancelForm"
        :btnCancel="{label:'关闭',click:(done)=>{cancelForm()}}"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="1000px"
        minWidth="800px"
      >
        <template v-slot:content>
          <!-- 搜索 -->
          <header v-if="!formCheckRole">
            <el-form :inline="true">
              <el-form-item prop="organizationId">
                <el-select
                  v-model="formCondition.organizationId"
                  size="small"
                  clearable
                  filterable
                  placeholder="请选择所属单位"
                  :disabled="true"
                >
                  <el-option
                    v-for="item in organizationSel"
                    :key="item.id"
                    :label="item.name"
                    :value="item.id"
                  >
                    <span style="float: left">{{ item.name }}</span>
                    <el-tag
                      size="medium"
                      type="info"
                      style="float: right;"
                    >{{filterType(item.type)}}</el-tag>
                  </el-option>
                </el-select>
              </el-form-item>
              <el-form-item prop="roleId">
                <el-select
                  v-model="formCondition.roleId"
                  size="small"
                  clearable
                  filterable
                  placeholder="请选择角色名称"
                  @change="changeRole"
                >
                  <el-option
                    v-for="item in roleSel"
                    :key="item.roleId"
                    :label="item.roleName"
                    :value="item.roleId"
                  >
                    <span style="float: left">{{ item.roleName }}</span>
                    <span
                      style="float: right;"
                      :class="[item.isDeleted?'el-tag el-tag--danger':'']"
                    >{{ item.isDeleted ? "已禁用" : "" }}</span>
                  </el-option>
                </el-select>
              </el-form-item>
              <el-form-item prop="type">
                <el-select
                  v-model="formCondition.type"
                  size="small"
                  clearable
                  filterable
                  placeholder="请选择角色类型"
                  :disabled="true"
                >
                  <el-option
                    v-for="item in type"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  ></el-option>
                </el-select>
              </el-form-item>
              <el-form-item>
                <el-button
                  type="primary"
                  size="small"
                  icon="el-icon-search"
                  @click="formSearch(true)"
                >搜索</el-button>
              </el-form-item>
            </el-form>
          </header>
          <!-- 搜索 -->
          <!-- 所有角色列表 -->
          <el-table
            ref="multipleRoleTable"
            v-loading="roleTableLoading"
            element-loading-text="加载中..."
            :data="roleTableData"
            border
            @selection-change="roleTableSelectionChange"
            @row-click="roleTableRowClick"
            style="width: 100%; overflow-y: auto;"
            max-height="550"
            min-height='200'
          >
            <el-table-column type="selection" v-if="!formCheckRole"></el-table-column>
            <!-- Table数据 -->
            <el-table-column label="角色Id" prop="roleId" v-if="false"></el-table-column>
            <el-table-column label="角色名称" prop="roleName" show-overflow-tooltip></el-table-column>
            <el-table-column label="角色类型" prop="roleType">
              <template slot-scope="scope">
                <el-tag size="medium" type="info">{{filterType(scope.row.roleType)}}</el-tag>
              </template>
            </el-table-column>
            <el-table-column label="所属单位Id" prop="organizationId" v-if="false"></el-table-column>
            <el-table-column label="所属单位" prop="organizationName" width="200" show-overflow-tooltip></el-table-column>
            <el-table-column label="角色描述" prop="description" show-overflow-tooltip></el-table-column>
            <el-table-column label="角色是否禁用" width="108" prop="isDeleted">
              <template slot-scope="scope">
                <el-tag
                  size="medium"
                  :type="scope.row.isDeleted ? 'danger' : ''"
                >{{scope.row.isDeleted ? '是' : '否'}}</el-tag>
              </template>
            </el-table-column>
            <el-table-column label="创建人" prop="createBy" show-overflow-tooltip width="110"></el-table-column>
            <el-table-column label="创建人Id" prop="createId" v-if="false"></el-table-column>
            <el-table-column label="创建时间" prop="createTime" show-overflow-tooltip width="110"></el-table-column>
            <el-table-column label="修改人" prop="updateBy" show-overflow-tooltip width="110"></el-table-column>
            <el-table-column label="修改人Id" prop="updateId" v-if="false"></el-table-column>
            <el-table-column label="修改时间" prop="updateTime" show-overflow-tooltip width="110"></el-table-column>
            <!-- Table数据 -->
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            class="pull-right"
            type="primary"
            v-if="!formCheckRole"
            :loading="btnSaveLoading"
            @click="saveForm"
            size="mini"
          >保存</el-button>
        </template>
        <!-- 所有角色列表 -->
      </rg-dialog>
    </section>
    <!-- 新增和编辑页面 -->
  </main>
</template>

<script>
import { roleSvc } from "@/api/accountauthority/role";
import { comBusSvc, empRoleSvc } from "@/api/accountauthority/employeerole";

export default {
  name: "employeerole",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      flag: this.global.deletedFlag,
      type: [
        {
          value: undefined,
          label: "全部"
        },
        {
          value: 0,
          label: "公司"
        },
        {
          value: 1,
          label: "门店"
        },
        {
          value: 2,
          label: "仓库"
        },
         { value: 3, label: "拓展" },
        { value: 4, label: "供应商" }
      ],
      condition: {
        organizationId: undefined,
        type: undefined,
        id: undefined,
        employeeName: "",
        isDeleted: undefined,
        pageIndex: 1,
        pageSize: 20
      },
      employeeSel: [],
      organizationSel: [],
      tableData: [],
      totalList: 0,
      //   disabledQueryEmpRole: true,
      disabledCheckEmpRole: true,
      disabledEditEmpRole: true,
      multipleEmpSelection: [],

      roleTableLoading: false,
      roleTableData: [],
      roleSel: [],
      formVisible: false,
      btnSaveLoading: false,
      formCheckRole: true,
      fixedRoleTable: 100,
      formType: [
        {
          value: 0,
          label: "公司"
        },
        {
          value: 1,
          label: "门店"
        },
        {
          value: 2,
          label: "仓库"
        },
         { value: 3, label: "拓展" },
        { value: 4, label: "供应商" }
      ],
      formCondition: {
        organizationId: undefined,
        roleId: undefined,
        type: undefined,
        isDeleted: 0
      },
      multipleRoleSelection: [],
      formLabelWidth: "120px"
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
    getAllOrganizationExceptShopSelectListAsync() {
      comBusSvc
        .getAllOrganizationExceptShopSelectListAsync()
        .then(
          res => {
            this.organizationSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    getAllEmployeeListAsync() {
      empRoleSvc
        .getAllEmployeeListAsync()
        .then(
          res => {
            this.employeeSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    getEmployeeListByOrgIdAndTypeAsync(data, name) {
      empRoleSvc
        .getEmployeeListByOrgIdAndTypeAsync(data)
        .then(
          res => {
            this.employeeSel = res.data;

            // if (!this.employeeSel.length) {
            //   this.$alert("所属单位 " + name + " 里没有员工！", "警告", {
            //     confirmButtonText: "关闭",
            //     type: "warning"
            //   });
            //   return;
            // }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    getConditionData() {
      this.getAllOrganizationExceptShopSelectListAsync();
      // this.getAllEmployeeListAsync();
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
    changeOrganization(orgId) {
      let curOrg = this.organizationSel.find(ele => {
        return ele.id == orgId;
      });

      console.log("organizationId: " + orgId);
      console.log("curOrg: " + JSON.stringify(curOrg));
      let type = curOrg ? curOrg.type : undefined;
      this.condition.type = type;

      // if (orgId) {
      //   this.getEmployeeListByOrgIdAndTypeAsync(
      //     {
      //       organizationId: orgId.split("|")[0],
      //       type: type
      //     },
      //     curOrg.name
      //   );
      // } else {
      //   this.getAllEmployeeListAsync();
      // }
    },
    changeEmployee(empId) {
      //   this.disabledQueryEmpRole = !empId;
    },
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    showEmpRole() {
      console.log(567, this.$refs.multipleEmpTable.selection);
      let rows = this.$refs.multipleEmpTable.selection;
      if (rows && rows.length != 1) {
        this.$alert("同时只能查看一个员工角色", "警告", {
          confirmButtonText: "关闭",
          type: "warning"
        });
        return;
      }

      this.roleTableData = [];
      this.formVisible = true;
      this.formCheckRole = true;

      let empId = "";
      if (rows && rows.length > 0 && rows[0].type >= 0) {
        empId = rows[0].employeeId;
      } else {
        return;
      }

      //获取员工角色列表成功后，重置Table Height
      this.getEmployeeRoleListByEmpIdAsync({ employeeId: empId });
      this.fixedRoleTable = 580;
    },
    showEditEmpRole() {
      let sels = this.$refs.multipleEmpTable.selection;
      if (sels && sels.length != 1) {
        this.$alert("同时只能编辑一个员工角色", "警告", {
          confirmButtonText: "关闭",
          type: "warning"
        });
        return;
      }

      this.formVisible = true;
      this.formCheckRole = false;

      console.log("edit row: " + JSON.stringify(sels));
      console.log("formCondition: " + JSON.stringify(this.formCondition));

      let reqData = JSON.parse(JSON.stringify(this.formCondition));
      reqData.organizationId = reqData.organizationId.split("|")[0];

      empRoleSvc
        .getRoleListByOrgIdAsync(reqData)
        .then(
          res => {
            //获取角色下拉框的数据
            this.roleSel = res.data;

            this.roleTableData = res.data;
            this.fixedRoleTable = 580;
          },
          error => {
            console.log(error);
          }
        )
        .then(() => {
          empRoleSvc
            .getEmployeeRoleListByEmpIdAsync({ employeeId: sels[0].employeeId })
            .then(
              res => {
                let roleRows = this.roleTableData;
                let rows = res.data;
                if (rows.length > 0) {
                  rows.forEach(row => {
                    let idx = roleRows.findIndex(
                      ele => ele.roleId === row.roleId
                    );

                    this.$refs.multipleRoleTable.toggleRowSelection(
                      roleRows[idx]
                    );
                  });
                }
              },
              error => {
                console.log(error);
              }
            );
        })
        .catch(() => {});
    },
    tableSelectionChange(rows) {
      this.multipleEmpSelection = rows;
      this.$refs.multipleEmpTable.selection = rows;
      this.disabledCheckEmpRole = rows.length == 0 || rows.length > 1;
      this.disabledEditEmpRole = rows.length == 0 || rows.length > 1;

      console.log("tableSelectionChange rows: " + JSON.stringify(rows));

      if (rows && rows.length > 0 && rows[0].type >= 0) {
        this.formCondition.organizationId = rows[0].organizationId;
        this.formCondition.type = rows[0].type;
      } else {
        return;
      }

      console.log(
        "multipleEmpSelection: " + JSON.stringify(this.multipleEmpSelection)
      );

      let sels = this.$refs.multipleEmpTable.selection;
      if (sels && sels.length == 1) {
        this.getRoleListByOrgIdAsync(this.formCondition);
      }
    },
    tableRowClick(row, column, event, multipleEmpTable) {
      console.log("tableRowClick: " + JSON.stringify(row));
      multipleEmpTable.toggleRowSelection(row);
      this.$refs.multipleEmpTable.selection = multipleEmpTable.selection;
    },
    getPaged(flag) {
      console.log("condition: " + JSON.stringify(this.condition));

      let data = JSON.parse(JSON.stringify(this.condition));

      //   if (!data.organizationId) {
      //     this.$alert("请选择所属单位！", "警告", {
      //       confirmButtonText: "关闭",
      //       type: "warning"
      //     });
      //     return;
      //   }

      if (data.organizationId) {
        data.organizationId = data.organizationId.split("|")[0];
      } else {
        data.organizationId = undefined;
      }

      console.log("data: " + JSON.stringify(data));

      this.tableLoading = true;
      empRoleSvc
        .getEmployeePage(data)
        .then(
          res => {
            let data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (flag) {
              this.$message({
                message: res.message || "查询成功",
                type: "success"
              });
            }
          },
          error => {
            console.log(error);
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
    filterType(value) {
      let newType = JSON.parse(JSON.stringify(this.formType));
      for (let index = 0; index < newType.length; index++) {
        let item = newType[index];
        if (item.value === value) {
          return item.label;
        }
      }
    },
    getRoleListByOrgIdAsync(data) {
      console.log("getRoleListByOrgIdAsync raw data: " + JSON.stringify(data));

      let sels = this.$refs.multipleEmpTable.selection;

      let reqData = JSON.parse(JSON.stringify(data));
      reqData.organizationId = reqData.organizationId.split("|")[0];

      console.log(
        "getRoleListByOrgIdAsync reqData: " + JSON.stringify(reqData)
      );

      empRoleSvc
        .getRoleListByOrgIdAsync(reqData)
        .then(
          res => {
            this.roleTableData = res.data;
            this.fixedRoleTable = 580;
          },
          error => {
            console.log(error);
          }
        )
        .then(() => {
          empRoleSvc
            .getEmployeeRoleListByEmpIdAsync({ employeeId: sels[0].employeeId })
            .then(
              res => {
                let roleRows = this.roleTableData;
                let rows = res.data;
                if (rows.length > 0) {
                  rows.forEach(row => {
                    let idx = roleRows.findIndex(
                      ele => ele.roleId === row.roleId
                    );
                    if (this.$refs.multipleRoleTable) {
                      this.$refs.multipleRoleTable.toggleRowSelection(
                        roleRows[idx]
                      );
                    }
                  });
                }
              },
              error => {
                console.log(error);
              }
            );
        })
        .catch(() => {});
    },
    getEmployeeRoleListByEmpIdAsync(data) {
      empRoleSvc
        .getEmployeeRoleListByEmpIdAsync(data)
        .then(
          res => {
            this.roleTableData = res.data;

            this.fixedRoleTable = 580;

            this.$message({
              message: res.message,
              type: "success"
            });
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    formSearch() {
      this.getRoleListByOrgIdAsync(this.formCondition);
    },
    changeRole(roleId) {
      if (roleId) {
        this.formCondition.roleId = roleId;
        this.getRoleListByOrgIdAsync(this.formCondition);
      } else {
        this.formCondition.roleId = undefined;
        this.formSearch();
      }
    },
    roleTableSelectionChange(rows) {
      this.multipleRoleSelection = rows;
      // this.$refs.multipleRoleTable.selection = rows;
      let sels = this.$refs.multipleRoleTable.selection;

      console.log(
        "multipleEmpSelection: " + JSON.stringify(this.multipleEmpSelection)
      );
      console.log(
        "multipleRoleSelection: " + JSON.stringify(this.multipleRoleSelection)
      );
    },
    roleTableRowClick(row, column, event) {
      if (!this.formCheckRole) {
        this.$refs.multipleRoleTable.toggleRowSelection(row);
      }
    },
    saveEmpRole(data) {
      let vm = this;

      vm.btnSaveLoading = true;
      empRoleSvc
        .editEmployeeRole(data)
        .then(res => {
          if (res.data) {
            this.$message({
              message: res.message,
              type: "success"
            });
            vm.search();
            vm.cancelForm();
          } else {
            this.$message({
              message: res.message,
              type: "warning"
            });
          }
        })
        .catch(() => {})
        .finally(() => {
          vm.btnSaveLoading = false;
        });
    },
    saveForm() {
      let empId = this.multipleEmpSelection[0].employeeId;
      let empName = this.multipleEmpSelection[0].employeeName;
      let sels = this.$refs.multipleRoleTable.selection;

      let roleIdArr = sels.map(a => a.roleId);
      let data = { employeeId: empId, roleIdList: roleIdArr };
      console.log("saveForm data: " + JSON.stringify(data));

      if (sels.length <= 0) {
        this.$confirm("是否要删除员工 " + empName + " 所有的权限吗?", "警告", {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning"
        })
          .then(
            () => {
              this.saveEmpRole(data);
            },
            cancel => {
              console.log(JSON.stringify(cancel));
            }
          )
          .catch(() => {});
      } else {
        this.saveEmpRole(data);
      }
    },
    cancelForm() {
      this.formVisible = false;

      this.fixedRoleTable = 100;
      this.roleTableData = [];
      this.formCondition.roleId = undefined;
    }
  }
};
</script>
