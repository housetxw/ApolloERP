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

          <el-form-item prop="applicationId">
            <el-select
              v-model="condition.applicationId"
              clearable
              filterable
              placeholder="请选择系统"
              class="margin-right-10"
            >
              <el-option
                v-for="item in applicationSel"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              >
                <span style="float: left">{{ item.name }}</span>
                <span
                  style="float: right;"
                  :class="[item.isDeleted?'el-tag el-tag--danger':'']"
                >{{ item.isDeleted ? "已禁用" : "" }}</span>
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item prop="id">
            <el-select
              v-model="condition.id"
              clearable
              filterable
              placeholder="请选择权限名称"
              class="margin-right-10"
            >
              <el-option
                v-for="item in authoritySel"
                :key="item.id"
                :label="item.name"
                :value="item.id"
              >
                <span style="float: left">{{ item.name }}</span>
                <span
                  style="float: right;"
                  :class="[item.isDeleted?'el-tag el-tag--danger':'']"
                >{{ item.isDeleted ? "已禁用" : "" }}</span>
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="权限类型" prop="type">
            <el-select
              v-model="condition.type"
              clearable
              filterable
              placeholder="请选择权限类型"
              class="margin-right-10"
            >
              <el-option
                v-for="item in type"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="是否禁用" prop="isDeleted">
            <el-select v-model="condition.isDeleted" placeholder="请选择">
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
            <el-button type="primary" size="mini" icon="el-icon-plus" @click="showCreate">新增</el-button>

        </template>
      <!-- 搜索 -->
      <!-- 列表 -->
        <template v-slot:tb_cols>

            <!-- Row展开效果 -->
            <el-table-column type="expand">
              <template slot-scope="props">
                <el-form label-position="left" inline class="account-table-expand">
                  <el-form-item label="权限名称 ">
                    <span>{{ props.row.name }}</span>
                  </el-form-item>
                  <el-form-item label="权限类型">
                    <el-tag size="medium" type="info">{{filterType(props.row.type)}}</el-tag>
                  </el-form-item>
                  <el-form-item label="父权限名称">
                    <span>{{ props.row.parentName }}</span>
                  </el-form-item>
                  <el-form-item label="所属系统 ">
                    <span>{{ props.row.applicationName }}</span>
                  </el-form-item>
                  <el-form-item label="权限路由">
                    <span>{{ props.row.route }}</span>
                  </el-form-item>
                  <el-form-item label="路由参数 ">
                    <span>{{ props.row.routeParameter }}</span>
                  </el-form-item>
                  <el-form-item label="描述 ">
                    <span>{{ props.row.description }}</span>
                  </el-form-item>
                  <el-form-item label="图标 ">
                    <span>{{ props.row.menuIcon }}</span>
                  </el-form-item>
                  <el-form-item label="排序序号 ">
                    <span>{{ props.row.sort }}</span>
                  </el-form-item>
                  <el-form-item label="是否禁用 ">
                    <span
                      class="el-tag"
                      :class="[props.row.isDeleted?'el-tag--danger':'el-tag--primary']"
                    >{{ props.row.isDeleted === false ? '否' : '是' }}</span>
                  </el-form-item>
                  <el-form-item label="创建人 ">
                    <span>{{ props.row.createBy }}</span>
                  </el-form-item>
                  <el-form-item label="创建时间 ">
                    <span>{{ props.row.createTime }}</span>
                  </el-form-item>
                  <el-form-item label="修改人 ">
                    <span>{{ props.row.updateBy }}</span>
                  </el-form-item>
                  <el-form-item label="修改时间 ">
                    <span>{{ props.row.updateTime }}</span>
                  </el-form-item>
                </el-form>
              </template>
            </el-table-column>
            <!-- Row展开效果 -->
            <!-- Table数据 -->
            <el-table-column label="权限Id" prop="id" v-if="false"></el-table-column>
            <el-table-column label="权限名称" prop="name" width="140" show-overflow-tooltip></el-table-column>
            <el-table-column label="权限类型" prop="type">
              <template slot-scope="scope">
                <el-tag size="medium" type="info">{{filterType(scope.row.type)}}</el-tag>
              </template>
            </el-table-column>
            <el-table-column label="父权限Id" prop="parentId" v-if="false"></el-table-column>
            <el-table-column label="父权限名称" prop="parentName" width="130" show-overflow-tooltip></el-table-column>
            <el-table-column label="所属系统Id" prop="applicationId" v-if="false"></el-table-column>
            <el-table-column label="所属系统" prop="applicationName" show-overflow-tooltip width="150"></el-table-column>
            <el-table-column label="权限路由" prop="route" show-overflow-tooltip></el-table-column>
            <el-table-column label="路由参数" prop="routeParameter" show-overflow-tooltip></el-table-column>
            <el-table-column label="描述" prop="description" show-overflow-tooltip></el-table-column>
            <el-table-column label="图标" prop="menuIcon" align="center">
               <template slot-scope="scope" >
　　　　      <img :src="scope.row.menuIcon" width="30" class="head_pic"  v-if='scope.row.menuIcon!=""'/>
　　          </template>
            </el-table-column>
            <el-table-column label="排序序号" prop="sort"></el-table-column>
            <el-table-column label="是否禁用" prop="isDeleted">
              <template slot-scope="scope">
                <el-tag
                  size="medium"
                  :type="scope.row.isDeleted === false ? '' : 'danger'"
                >{{scope.row.isDeleted === false ? '否' : '是'}}</el-tag>
              </template>
            </el-table-column>
            <el-table-column label="创建人" prop="createBy" show-overflow-tooltip></el-table-column>
            <el-table-column label="创建人Id" prop="createId" v-if="false"></el-table-column>
            <el-table-column label="创建时间" prop="createTime" show-overflow-tooltip width="110"></el-table-column>
            <el-table-column label="修改人" prop="updateBy" show-overflow-tooltip></el-table-column>
            <el-table-column label="修改人Id" prop="updateId" v-if="false"></el-table-column>
            <el-table-column label="修改时间" prop="updateTime" show-overflow-tooltip width="110"></el-table-column>
            <el-table-column label="操作" width="200" fixed="right">
              <template slot-scope="scope">
                <el-button size="mini" icon="el-icon-edit" @click="showEdit(scope.row)">编辑</el-button>
                <el-button
                  size="mini"
                  icon="el-icon-delete"
                  type="danger"
                  @click="deleteApplication(scope.row)"
                >禁用</el-button>
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
         :title="formModel.id?'编辑':'新增'"
            :visible.sync="formVisible"
            :beforeClose="cancel"
            :btnCancel="{label:'关闭',click:(done)=>{cancel()}}"
            :btnRemove="false"
            :footbar="true"
            width="80%"
            maxWidth="800px"
            minWidth="600px">
            <template v-slot:content>
   
        <el-form :model="formModel" :rules="rules" ref="formModel">
          <el-form-item label="系统名称" :label-width="formLabelWidth" prop="applicationId">
            <el-select
              v-model="formModel.applicationId"
              clearable
              filterable
              placeholder="请选择系统"
              class="margin-right-10"
              style="width:80%"
              :disabled="!!formModel.id"
              @change="changeApplication"
            >
              <el-option
                v-for="item in applicationSel"
                :key="item.id"
                :label="item.name"
                :value="item.id"
                :disabled="item.isDeleted"
              >
                <!-- v-if="!item.isDeleted" -->
                <span style="float: left">{{ item.name }}</span>
                <span
                  style="float: right;"
                  :class="[item.isDeleted?'el-tag el-tag--danger':'']"
                >{{ item.isDeleted ? "已禁用" : "" }}</span>
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="父级权限名称" :label-width="formLabelWidth" prop='parentId'>
            <el-select
              v-model="formModel.parentId"
              clearable
              filterable
              placeholder="请选择父级权限"
              class="margin-right-10"
              style="width:80%"
               @change="parentIdChange"
            >
              <el-option
                v-for="item in authorityFormSel"
                :key="item.id"
                :label="item.name"
                :value="item.id"
                :disabled="item.isDeleted"
                v-if="!item.isDeleted"
              >
                <span style="float: left">{{item.name }}</span>
                <el-tag size="medium" type="info" style="float: right;">{{item.typeName}}权限</el-tag>
                <span
                  style="float: right;"
                  :class="[item.isDeleted?'el-tag el-tag--danger':'']"
                >{{ item.isDeleted ? "已禁用" : "" }}</span>
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="权限类型" :label-width="formLabelWidth" prop="type">
            <el-select
              v-model="formModel.type"
              clearable
              filterable
              placeholder="请选择权限类型"
              class="margin-right-10"
              style="width:80%"
            >
              <el-option
                v-for="item in formType"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="权限名称" :label-width="formLabelWidth" prop="name">
            <el-input
              v-model="formModel.name"
              autocomplete="off"
              placeholder="请输入权限名称；注意：当权限类型是模块和页面时，权限名称就是菜单的名称！"
              clearable
              style="width:80%"

            ></el-input>
          </el-form-item>
          <el-form-item label="权限路由" :label-width="formLabelWidth" prop="route">
            <el-input
              v-model="formModel.route"
              autocomplete="off"
              placeholder="请输入权限路由；注意：目前仅权限类型是页面时，才要求输入！"
              clearable
              style="width:80%"

            ></el-input>
          </el-form-item>
          <el-form-item label="路由参数" :label-width="formLabelWidth" prop="routeParameter">
             
            <el-input v-model="formModel.routeParameter" autocomplete="off" placeholderclearable  style="width:80%" ></el-input>
          </el-form-item>
          <el-form-item label="描述" :label-width="formLabelWidth" prop="description">
            <el-input
              type="textarea"
              :rows="2"
              v-model="formModel.description"
              autocomplete="off"
              placeholder
              clearable
               style="width:80%"
            ></el-input>
          </el-form-item>
          <el-form-item label="图标" :label-width="formLabelWidth" prop="menuIcon">
            <el-input v-model="formModel.menuIcon" autocomplete="off" placeholderclearable  style="width:80%"> </el-input>
          </el-form-item>
          <el-form-item label="排列序号" :label-width="formLabelWidth" prop="sort">
            <el-input
              v-model.number="formModel.sort"
              type="number"
              min="0"
              max="100"
              autocomplete="off"
              placeholder
              clearable
               style="width:80%"
            ></el-input>
          </el-form-item>
        </el-form>
            </template>
            <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            type="primary"
            :loading="btnSaveLoading"
            @click="save('formModel')"
            size="mini"
          >保存</el-button>
            </template>

      </rg-dialog>
    </section>
    <!-- 新增和编辑页面 -->
  </main>
</template>

<script>
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/accountauthority/application";
import { authoritySvc } from "@/api/accountauthority/authority";

export default {
  name: "authority",
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
          label: "模块"
        },
        {
          value: 1,
          label: "页面"
        },
        {
          value: 2,
          label: "按钮"
        }
      ],
      condition: {
        id: undefined,
        applicationId: undefined,
        type: undefined,
        isDeleted: 0,
        pageIndex: 1,
        pageSize: 20
      },
      applicationSel: [],
      authoritySel: [],
      authorityFormSel: [],
      tableData: [],
      totalList: 0,

      formVisible: false,
      btnSaveLoading: false,
      formType: [
        {
          value: 0,
          label: "模块"
        },
        {
          value: 1,
          label: "页面"
        },
        {
          value: 2,
          label: "按钮"
        }
      ],
      formInit: {
        id: undefined,
        applicationId: undefined,
        parentId: undefined,
        name: "",
        type: undefined,
        route: "",
        routeParameter: "",
        description: "",
        menuIcon: "",
        sort: 0
      },
      formModel: {
        id: undefined,
        applicationId: undefined,
        parentId: undefined,
        name: "",
        type: undefined,
        route: "",
        routeParameter: "",
        description: "",
        menuIcon: "",
        sort: 0
      },
      rules: {
        applicationId: [
          { required: true, message: "请选择权限所属系统", trigger: "blur" }
        ],
        name: [
          { required: true, message: "请输入权限名称", trigger: "blur" },
          { max: 50, message: "权限名称长度不允许超过50", trigger: "blur" }
        ],
        type: [{ required: true, message: "请选择权限类型", trigger: "blur" }],
        route: [
          { max: 100, message: "权限路由长度不允许超过100", trigger: "blur" }
        ],
        routeParameter: [
          { max: 255, message: "路由参数长度不允许超过255", trigger: "blur" }
        ],
        menuIcon: [
          { max: 100, message: "图标长度不允许超过100", trigger: "blur" }
        ],
        description: [
          { max: 255, message: "权限描述长度不允许超过255", trigger: "blur" }
        ]
      },
      formLabelWidth: "120px"
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    getConditionData() {
      // console.log(JSON.stringify(authoritySvc));

      appSvc
        .getApplicationSelectList()
        .then(
          res => {
            this.applicationSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});

      authoritySvc
        .getAllAuthoritySelectList()
        .then(
          res => {
            this.authoritySel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
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
      this.formModel.parentId = undefined;
      this.authorityFormSel = [];
    },
    showEdit(row) {
      if (row.isDeleted) {
        this.$alert("权限 " + row.name + " 已禁用，不允许编辑！", "警告", {
          confirmButtonText: "关闭",
          type: "warning"
        });
        return;
      }

      this.formVisible = true;

      this.formModel = JSON.parse(JSON.stringify(row));
      if (this.formModel.parentId === 0) {
        this.formModel.parentId = undefined;
      }

      this.authorityFormSel = [];
      authoritySvc
        .getAuthorityListByApplicationIdExceptIdAsync({
          id: this.formModel.id,
          applicationId: this.formModel.applicationId
        })
        .then(
          res => {
            this.authorityFormSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    parentIdChange(){
  this.$forceUpdate();
  console.log(1111);
    },
    changeApplication(appId) {
      this.authorityFormSel = [];
      this.formModel.parentId = undefined;

      console.log(
        "applicationId: " + appId + "; authority id: " + this.formModel.id
      );

      authoritySvc
        .getAuthorityListByApplicationIdAsync({ applicationId: appId })
        .then(
          res => {
            this.authorityFormSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    getPaged(flag) {
      console.log("condition: " + JSON.stringify(this.condition));

      if (!this.condition.id) {
        this.condition.id = undefined;
      }
      if (!this.condition.applicationId) {
        this.condition.applicationId = undefined;
      }

      this.tableLoading = true;
      authoritySvc
        .getPagedAuthorityList(this.condition)
        .then(
          res => {
            var data = res.data;
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
          // console.log("tableLoading: false");
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

      this.$refs[formName].validate(valid => {
        console.log("formModel: " + JSON.stringify(this.formModel));
        if (valid) {
          for (let index = 0; index < this.applicationSel.length; index++) {
            let ele = this.applicationSel[index];
            if (ele.id === this.formModel.applicationId && ele.isDeleted) {
              this.$alert(
                "系统 " + ele.name + " 已被禁用，请重新选择系统！",
                "警告",
                {
                  confirmButtonText: "关闭",
                  type: "warning"
                }
              );

              return;
            }
          }

          vm.btnSaveLoading = true;
          authoritySvc
            .createOrUpdateAuthority(this.formModel)
            .then(res => {
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
            })
            .catch(error => {
              console.log(error);
            })
            .finally(() => {
              vm.btnSaveLoading = false;
            });
        } else {
          return false;
        }
      });
    },
    deleteApplication(row) {
      var vm = this;

      // if (row.type !== 2) {
      //   this.$alert("目前仅支持禁用'按钮'级别的权限！", "警告", {
      //     confirmButtonText: "关闭",
      //     type: "warning"
      //   });
      //   return;
      // }

      if (row.isDeleted) {
        this.$alert("权限 " + row.name + " 已禁用！", "警告", {
          confirmButtonText: "关闭",
          type: "warning"
        });
        return;
      }

      this.$confirm("是否要禁用 " + row.name + " 权限！", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "info"
      })
        .then(() => {
          authoritySvc
            .deleteAuthorityById(row)
            .then(
              res => {
                if (res.data) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  vm.search();
                  vm.getConditionData();
                  vm.cancel();
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
    cancel(formName) {
      this.formVisible = false;

      this.resetForm();
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    }
  }
};
</script>