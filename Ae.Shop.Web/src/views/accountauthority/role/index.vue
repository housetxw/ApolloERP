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
        :defaultCollapse="false"
      >
        <template v-slot:condition>
          <el-form-item prop="organizationId" v-if="false">
            <el-select
              v-model="condition.organizationId"
              clearable
              filterable
              placeholder="请选择所属单位"
              @change="changeConditionOrganization"
            >
              <el-option
                v-for="item in organizationSel"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              >
                <span style="float: left">{{ item.name }}</span>
                <el-tag size="medium" type="info" style="float: right">{{
                  filterType(item.type)
                }}</el-tag>
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item prop="id" v-if="false">
            <el-select
              v-model="condition.id"
              clearable
              filterable
              placeholder="请选择角色名称"
            >
              <el-option
                v-for="item in roleSel"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              >
                <span style="float: left">{{ item.name }}</span>
                <span
                  style="float: right"
                  :class="[item.isDeleted ? 'el-tag el-tag--danger' : '']"
                  >{{ item.isDeleted ? "已禁用" : "" }}</span
                >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item prop="type" v-if="false">
            <el-select
              v-model="condition.type"
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
          <el-form-item label="是否禁用">
            <el-select
              v-model="condition.isDeleted"
              placeholder="请选择是否禁用"
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
          <el-button
            type="primary"
            size="mini"
            icon="el-icon-plus"
            @click="showCreate"
            >新增</el-button
          >
        </template>
        <!-- 搜索 -->
        <!-- 列表 -->
        <template v-slot:tb_cols>
         
          <!-- Table数据 -->
          <el-table-column
            label="角色Id"
            prop="id"
            v-if="false"
          ></el-table-column>
          <el-table-column
            label="角色名称"
            prop="name"
            show-overflow-tooltip
          ></el-table-column>
          <!-- <el-table-column label="角色类型" prop="type" width="120">
            <template slot-scope="scope">
              <el-tag size="medium" type="info">{{
                filterType(scope.row.type)
              }}</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="系统角色" prop="type" width="120">
            <template slot-scope="scope">
              <el-tag size="medium" type="info">{{
                featureType(scope.row)
              }}</el-tag>
            </template>
          </el-table-column>
          <el-table-column
            label="所属单位Id"
            prop="organizationId"
            v-if="false"
          ></el-table-column>
          <el-table-column
            label="所属单位"
            prop="organizationName"
            show-overflow-tooltip
          ></el-table-column> -->

          <el-table-column
            label="描述"
            prop="description"
            show-overflow-tooltip
          ></el-table-column>
          <el-table-column label="是否禁用" prop="isDeleted">
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
          <el-table-column label="操作" width="292" fixed="right">
            <template slot-scope="scope">
              <el-tooltip
                class="item"
                effect="dark"
                :content="genEditRoleAuthorityTooltip(scope.row.name)"
                placement="top"
              >
                <el-button
                  size="mini"
                  icon="el-icon-edit"
                  type="primary"
                  @click="showAuthorityTreeEdit(scope.row)"
                  >编辑权限</el-button
                >
              </el-tooltip>
              <el-button
                size="mini"
                icon="el-icon-edit"
                @click="showEdit(scope.row)"
                >编辑</el-button
              >
              <el-button
                size="mini"
                icon="el-icon-delete"
                type="danger"
                @click="deleteApplication(scope.row)"
                >禁用</el-button
              >
            </template>
          </el-table-column>
          <!-- Table数据 -->
        </template>
      </rg-page>
      <!-- 列表 -->
    </section>
    <!-- 首页 -->
    <!-- 新增和编辑页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        :title="formModel.id ? '编辑' : '新增'"
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
        width="80%"
        maxWidth="800px"
        minWidth="600px"
      >
        <template v-slot:content>
          <el-form :model="formModel" :rules="rules" ref="formModel">
            <!-- <el-form-item
              label="所属单位"
              :label-width="formLabelWidth"
              prop="organizationId"
            >
              <el-select
                v-model="formModel.organizationId"
                clearable
                filterable
                placeholder="请选择所属单位"
                style="width: 80%"
                :disabled="!!formModel.id"
                @change="changeFormOrganization"
              >
                <el-option
                  v-for="item in organizationSel"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                >
                  <span style="float: left">{{ item.name }}</span>
                  <el-tag size="medium" type="info" style="float: right">{{
                    filterType(item.type)
                  }}</el-tag>
                </el-option>
              </el-select>
            </el-form-item> -->
            <el-form-item
              label="角色名称"
              :label-width="formLabelWidth"
              prop="name"
            >
              <el-input
                v-model="formModel.name"
                autocomplete="off"
                placeholder="请输入角色名称"
                clearable
                style="width: 80%"
              ></el-input>
            </el-form-item>
            <!-- <el-form-item
              label="角色类型"
              :label-width="formLabelWidth"
              prop="type"
            >
              <el-select
                v-model="formModel.type"
                clearable
                filterable
                placeholder="请选择角色类型"
                style="width: 80%"
                :disabled="isDisableOrgType"
              >
                <el-option
                  v-for="item in formType"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                ></el-option>
              </el-select>
            </el-form-item> -->
            <!-- <el-form-item
              label="系统角色"
              :label-width="formLabelWidth"
              prop="shopFeature"
              v-show="isShopFeature"
            >
              <el-radio-group v-model="formModel.shopFeature">
                <el-radio :label="0">普通</el-radio>
                <el-radio :label="1">高级</el-radio>
                <el-radio :label="2">个人</el-radio>
              </el-radio-group>
            </el-form-item> -->
            <!-- <el-form-item
              label="系统角色"
              :label-width="formLabelWidth"
              prop="companyFeature"
              v-show="isCompanyFeature"
            >
              <el-radio-group v-model="formModel.companyFeature">
                <el-radio :label="1">平台公司</el-radio>
                <el-radio :label="2">普通公司</el-radio>
                <el-radio :label="3">区域合伙公司</el-radio>
              </el-radio-group>
            </el-form-item> -->
            <el-form-item
              label="描述"
              :label-width="formLabelWidth"
              prop="description"
            >
              <el-input
                type="textarea"
                :rows="2"
                v-model="formModel.description"
                autocomplete="off"
                placeholder
                style="width: 80%"
                clearable
              ></el-input>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            type="primary"
            @click="save('formModel')"
            size="mini"
            >保存</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 新增和编辑页面 -->
    <!-- 权限树页面 -->
    <section id="authorityTree">
      <rg-dialog
        :title="formAuthorityTreeTitle"
        :visible.sync="formAuthorityTreeVisible"
        :beforeClose="cancelAuthorityTree"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            cancelAuthorityTree();
          },
        }"
        :btnRemove="false"
        :footbar="true"
        width="50%"
        maxWidth="800px"
        minWidth="600px"
      >
        <template v-slot:content>
          <el-tree
            :data="authorityTreeList"
            show-checkbox
            node-key="id"
            ref="tree"
            :default-expanded-keys="defaultExpandedKeys"
            :default-checked-keys="defaultCheckedKeys"
            :props="defaultProps"
            :check-on-click-node="true"
            :highlight-current="true"
            class="custom-el-dialog-vh"
          ></el-tree>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            type="primary"
            :loading="btnSaveRoleTreeLoading"
            @click="saveAuthorityTree"
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
import { authoritySvc } from "@/api/accountauthority/authority";
import { comBusSvc, roleSvc } from "@/api/accountauthority/role";

export default {
  name: "role",
  data() {
    return {
      tableLoading: false,
      btnSaveRoleLoading: false,
      currentPage: 1,
      flag: this.global.deletedFlag,
      type: [
        {
          value: undefined,
          label: "全部",
        },
        {
          value: 0,
          label: "公司",
        },
        {
          value: 1,
          label: "门店",
        },
        {
          value: 2,
          label: "仓库",
        },
      ],
      condition: {
        organizationId: undefined,
        id: undefined,
        type: undefined,
        isDeleted: undefined,
        pageIndex: 1,
        pageSize: 20,
      },
      roleSel: [],
      organizationSel: [],
      tableData: [],
      totalList: 0,

      formVisible: false,
      btnSaveRoleTreeLoading: false,
      isDisableOrgType: false,
      isShopFeature: false,
      isCompanyFeature: false,
      formType: [
        {
          value: 0,
          label: "公司",
        },
        {
          value: 1,
          label: "门店",
        },
        {
          value: 2,
          label: "仓库",
        },
        {
          value: 3,
          label: "拓展",
        },
        {
          value: 4,
          label: "供应商",
        },
      ],
      formInit: {
        id: undefined,
        organizationId: undefined,
        name: "",
        type: undefined,
        description: "",
        shopFeature: 0,
        companyFeature: 0,
        features: 0,
      },
      formModel: {
        id: undefined,
        organizationId: undefined,
        name: "",
        type: undefined,
        description: "",
        shopFeature: 0,
        companyFeature: 0,
        features: 0,
      },
      rules: {
        // organizationId: [
        //   { required: true, message: "请选择所属单位", trigger: "blur" },
        // ],
        name: [
          { required: true, message: "请输入角色名称", trigger: "blur" },
          { max: 50, message: "角色名称长度不允许超过50", trigger: "blur" },
        ],
        // type: [{ required: true, message: "请选择角色类型", trigger: "blur" }],
        description: [
          { max: 255, message: "角色描述长度不允许超过255", trigger: "blur" },
        ],
      },
      formLabelWidth: "120px",

      formAuthorityTreeVisible: false,
      formAuthorityTreeTitle: "",
      defaultProps: {
        children: "children",
        label: "label",
      },
      defaultExpandedKeys: [],
      defaultCheckedKeys: [],
      authorityTreeList: [],
      currentEditRoleAuthority: {},
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    getConditionData() {
      // console.log(JSON.stringify(authoritySvc));
      // comBusSvc
      //   .getAllOrganizationExceptShopSelectListAsync()
      //   .then(
      //     (res) => {
      //       this.organizationSel = res.data;
      //       console.log(this.organizationSel);
      //     },
      //     (error) => {
      //       console.log(error);
      //     }
      //   )
      //   .catch(() => {});

      // roleSvc
      //   .getAllRoleSelectList()
      //   .then(
      //     (res) => {
      //       this.roleSel = res.data;
      //     },
      //     (error) => {
      //       console.log(error);
      //     }
      //   )
      //   .catch(() => {});
    },
    filterType(value) {
      var newType = JSON.parse(JSON.stringify(this.formType));
      for (let index = 0; index < newType.length; index++) {
        let item = newType[index];
        if (item.value === value) {
          return item.label;
        }
      }
    },
    featureType(value) {
      var newType = JSON.parse(JSON.stringify(value));
      if (value.type === 0 || value.type === 3 || value.type === 4) {
        if (value.features === 1) {
          return "平台公司";
        } else if (value.features === 2) {
          return "普通公司";
        } else if (value.features === 3) {
          return "区域合伙公司";
        }
      } else if (value.type === 1) {
        if (value.features === 0) {
          return "普通";
        } else if (value.features === 1) {
          return "高级";
        } else if (value.features === 2) {
          return "个人";
        }
      }
    },
    changeConditionOrganization(orgId) {
      let curOrg = this.organizationSel.find((ele) => ele.id === orgId);

      console.log("organizationId: " + orgId);
      console.log("curOrg: " + JSON.stringify(curOrg));

      let type = curOrg ? curOrg.type : undefined;
      this.condition.type = type;
    },
    getRoleAuthorityTree() {
      roleSvc
        .getAuthorityTreeWithApplicationInfo()
        .then(
          (res) => {
            this.authorityTreeList = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .then(() => {
          this.getRoleAuthorityList();
        })
        .catch(() => {});
    },
    getRoleAuthorityList() {
      roleSvc
        .getRoleAuthorityListByRoleId({
          roleId: this.currentEditRoleAuthority.id,
        })
        .then(
          (res) => {
            this.defaultExpandedKeys = res.data.checked;
            this.defaultCheckedKeys = res.data.checked;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    fetchData() {
      this.getConditionData();
      this.getPaged();
    },
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    showCreate() {
      this.formVisible = true;
    },
    showEdit(row) {
      if (row.isDeleted) {
        this.$alert("角色 " + row.name + " 已禁用，不允许编辑！", "警告", {
          confirmButtonText: "关闭",
          type: "warning",
        });
        return;
      }

      this.formVisible = true;
      let obj = JSON.parse(JSON.stringify(row));

      this.isCompanyFeature = false;
      this.isShopFeature = false;
      if (obj.type === 0 || obj.type === 3 || obj.type === 4) {
        obj.companyFeature = obj.features;

        this.isCompanyFeature = true;
      }
      if (obj.type === 1) {
        obj.shopFeature = obj.features;
        this.isShopFeature = true;
      }

      this.formModel = obj;
      console.log(JSON.parse(JSON.stringify(this.formModel)));
    },
    genEditRoleAuthorityTooltip(value) {
      return '查看或编辑角色 "' + value + '" 的权限';
    },
    changeFormOrganization(orgId) {
      var curOrg = this.organizationSel.find((ele) => ele.id === orgId);

      //根据所选单位的类型，来决定角色的类型
      console.log("organizationId: " + orgId);
      console.log("curOrg: " + JSON.stringify(curOrg));
      debugger;
      if (curOrg.id === "0") {
        this.isDisableOrgType = false;
        this.isCompanyFeature = true;
      } else {
        this.isDisableOrgType = true;
        this.isCompanyFeature = false;
        this.formModel.companyFeature = 0;
      }
      if (curOrg.id === "0|Shop") {
        this.isShopFeature = true;
      } else {
        this.isShopFeature = false;
        this.formModel.shopFeature = "0";
      }
      this.formModel.type = curOrg ? curOrg.type : undefined;
    },
    getPaged(flag) {
      console.log("condition: " + JSON.stringify(this.condition));

      this.tableLoading = true;

      var data = JSON.parse(JSON.stringify(this.condition));
      if (data.organizationId) {
        data.organizationId = data.organizationId.split("|")[0];
      } else {
        data.organizationId = undefined;
      }
      if (!data.id) {
        data.id = undefined;
      }

      if (data.id > 0) {
        let org = {};
        org = this.roleSel.find((item) => {
          //model就是上面的数据源
          return item.id === data.id; //筛选出匹配数据
        });
        data.type = org.type;
      }

      roleSvc
        .getPagedRoleList(data)
        .then(
          (res) => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (flag) {
              this.$message({
                message: res.message || "查询成功",
                type: "success",
              });
            }
          },
          (error) => {
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
    resetForm() {
      this.formModel = JSON.parse(JSON.stringify(this.formInit));
    },
    save(formName) {
      var vm = this;

      this.$refs[formName].validate((valid) => {
        if (valid) {
          console.log("formModel: " + JSON.stringify(this.formModel));

          // for (let index = 0; index < this.organizationSel.length; index++) {
          //   let ele = this.organizationSel[index];
          //   if (ele.id === this.formModel.organizationId && ele.status != 2) {
          //     this.$alert(
          //       "所属单位 " + ele.name + " 状态异常，请重新选择所属单位！",
          //       "警告",
          //       {
          //         confirmButtonText: "关闭",
          //         type: "warning",
          //       }
          //     );

          //     return;
          //   }
          // }

          if (this.formModel.shopFeature >= 0) {
            this.formModel.features = this.formModel.shopFeature;
          }
          if (this.formModel.companyFeature > 0) {
            this.formModel.features = this.formModel.companyFeature;
          }

          console.log("features: " + this.formModel.features);

          var data = JSON.parse(JSON.stringify(this.formModel));
          if (data.organizationId) {
            data.organizationId = data.organizationId.split("|")[0];
          }
          console.log("data: " + JSON.stringify(data));

          vm.btnSaveRoleLoading = true;

          roleSvc
            .createOrUpdateRole(data)
            .then((res) => {
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
            })
            .catch((error) => {
              console.log(error);
            })
            .finally(() => {
              vm.btnSaveRoleLoading = false;
            });
        } else {
          return false;
        }
      });
    },
    deleteApplication(row) {
      var vm = this;

      if (row.isDeleted) {
        this.$alert("角色 " + row.name + " 已禁用！", "警告", {
          confirmButtonText: "关闭",
          type: "warning",
        });
        return;
      }

      if (row.features == 9) {
        this.$alert("默认角色 " + row.name + " 不可禁用！", "警告", {
          confirmButtonText: "关闭",
          type: "warning",
        });
        return;
      }

      this.$confirm("是否要禁用 " + row.name + " 角色！", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "info",
      })
        .then(() => {
          var data = JSON.parse(JSON.stringify(row));
          if (data.organizationId) {
            data.organizationId = data.organizationId.split("|")[0];
          }
          console.log("data: " + JSON.stringify(data));

          roleSvc
            .deleteRoleById(data)
            .then(
              (res) => {
                if (res.data) {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                  vm.search();
                  vm.getConditionData();
                  vm.cancel();
                } else {
                  this.$message({
                    message: res.message,
                    type: "warning",
                  });
                }
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },
    cancel(formName) {
      this.formVisible = false;
      this.resetForm();

      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },
    showAuthorityTreeEdit(row) {
      if (row.isDeleted) {
        this.$alert(
          "角色 " + row.name + " 已禁用, 不允许编辑角色权限！",
          "警告",
          {
            confirmButtonText: "关闭",
            type: "warning",
          }
        );
        return;
      }

      this.formAuthorityTreeVisible = true;
      this.currentEditRoleAuthority = JSON.parse(JSON.stringify(row));
      this.formAuthorityTreeTitle =
        "查看或编辑 " + this.currentEditRoleAuthority.name + " 的权限";
      this.getRoleAuthorityTree();
    },
    saveAuthorityTree() {
      var role = this.currentEditRoleAuthority;
      console.log("role: " + JSON.stringify(role));
      console.log("HalfChecked: " + this.$refs.tree.getHalfCheckedKeys());
      console.log("Checked: " + this.$refs.tree.getCheckedKeys());

      var vm = this;
      var data = {
        roleId: role.id,
        halfCheckedList: this.$refs.tree.getHalfCheckedKeys(),
        checkedList: this.$refs.tree.getCheckedKeys(),
      };

      vm.btnSaveRoleTreeLoading = true;
      roleSvc
        .saveRoleAuthority(data)
        .then((res) => {
          if (res.data) {
            this.$message({
              message: res.message,
              type: "success",
            });
            vm.cancelAuthorityTree();
          }
        })
        .catch(() => {})
        .finally(() => {
          vm.btnSaveRoleTreeLoading = false;
        });
    },
    cancelAuthorityTree() {
      this.formAuthorityTreeVisible = false;
      this.currentEditRoleAuthority = {};
      this.authorityTreeList = [];
      this.defaultExpandedKeys = [];
      this.defaultCheckedKeys = [];
    },
  },
};
</script>
