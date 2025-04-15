<template>
  <main class="container account-authority">
    <!-- 首页 -->
    <section id="indexPage">
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
      >
        <template v-slot:condition>
          <el-form-item prop="id">
            <el-select
              v-model="condition.id"
              clearable
              filterable
              placeholder="请选择系统名称"
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
          <el-form-item label="是否禁用" prop="isDeleted">
            <el-select v-model="condition.isDeleted" placeholder="请选择">
              <el-option
                v-for="item in flag"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
            <!-- <el-select v-model="condition.initialism" size="small" clearable placeholder="请选择系统简称">
          <el-option
            v-for="item in applications"
            :key="item.initialism"
            :label="item.initialism"
            :value="item.initialism"
          ></el-option>
            </el-select>-->
            <!-- <el-input v-model="condition.name" clearable class="el-input el-input--suffix" placeholder="请输入系统名称"clearable></el-input> -->
          </el-form-item>
        </template>
        <template v-slot:header_btn>
          <el-button type="primary" size="mini" icon="el-icon-plus" @click="showCreate">新增</el-button>
        </template>

        <template v-slot:tb_cols>
          <!-- Row展开效果 -->
          <el-table-column type="expand">
            <template slot-scope="props">
              <el-form label-position="left" inline class="account-table-expand">
                <el-form-item label="系统名称: ">
                  <span>{{ props.row.name }}</span>
                </el-form-item>
                <el-form-item label="系统简称: ">
                  <span>{{ props.row.initialism }}</span>
                </el-form-item>
                <el-form-item label="系统域名: ">
                  <span>{{ props.row.route }}</span>
                </el-form-item>
                <el-form-item label="是否禁用: ">
                  <span
                    class="el-tag"
                    :class="[props.row.isDeleted?'el-tag--danger':'el-tag--primary']"
                  >{{ props.row.isDeleted === false ? '否' : '是' }}</span>
                </el-form-item>
                <el-form-item label="创建人: ">
                  <span>{{ props.row.createBy }}</span>
                </el-form-item>
                <el-form-item label="创建时间: ">
                  <span>{{ props.row.createTime }}</span>
                </el-form-item>
                <el-form-item label="修改人: ">
                  <span>{{ props.row.updateBy }}</span>
                </el-form-item>
                <el-form-item label="修改时间: ">
                  <span>{{ props.row.updateTime }}</span>
                </el-form-item>
              </el-form>
            </template>
          </el-table-column>
          <!-- Row展开效果 -->
          <!-- Table数据 -->
          <el-table-column label="系统Id" prop="id" v-if="false"></el-table-column>
          <el-table-column label="系统名称" prop="name"></el-table-column>
          <el-table-column label="系统简称" prop="initialism"></el-table-column>
          <el-table-column label="系统域名" prop="route"></el-table-column>
          <el-table-column label="是否禁用" prop="isDeleted">
            <template slot-scope="scope">
              <el-tag
                size="medium"
                :type="scope.row.isDeleted === false ? '' : 'danger'"
              >{{scope.row.isDeleted === false ? '否' : '是'}}</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="创建人" prop="createBy"></el-table-column>
          <el-table-column label="创建人Id" prop="createId" v-if="false"></el-table-column>
          <el-table-column label="创建时间" prop="createTime"></el-table-column>
          <el-table-column label="修改人" prop="updateBy"></el-table-column>
          <el-table-column label="修改人Id" prop="updateId" v-if="false"></el-table-column>
          <el-table-column label="修改时间" prop="updateTime"></el-table-column>
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
        width="100%"
        maxWidth="800px"
        minWidth="600px"
        :close-on-click-modal="false"
      >
        <template v-slot:content>
          <el-form :model="formModel" :rules="rules" ref="formModel">
            <el-form-item label="系统名称" :label-width="formLabelWidth" prop="name">
              <!-- required prop="name"> -->
              <el-input
                v-model="formModel.name"
                autocomplete="off"
                autofocus="true"
                placeholder="订单管理系统"
                clearable
                style="width:80%"
              ></el-input>
            </el-form-item>
            <el-form-item label="系统简称" :label-width="formLabelWidth" prop="initialism">
              <el-input
                v-model="formModel.initialism"
                autocomplete="off"
                placeholder="OMS"
                clearable
                style="width:80%"
              ></el-input>
            </el-form-item>
            <!-- <template slot="prepend">Https://</template>
            <template slot="append">.cn</template>-->
            <el-form-item label="系统域名" :label-width="formLabelWidth" prop="route">
              <el-input
                v-model="routeComputed"
                autocomplete="off"
                placeholder="示例: https://order.aerp.com.cn"
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
import { comBusSvc, appSvc } from "@/api/accountauthority/application";

export default {
  name: "application",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      flag: this.global.deletedFlag,
      condition: {
        id: undefined,
        initialism: "",
        isDeleted: 0,
        pageIndex: 1,
        pageSize: 20
      },
      applicationSel: [],
      tableData: [],
      totalList: 0,

      formVisible: false,
      btnSaveLoading: false,
      formInit: {
        id: undefined,
        name: "",
        initialism: "",
        route: ""
      },
      formModel: {
        id: undefined,
        name: "",
        initialism: "",
        route: ""
      },
      rules: {
        name: [
          { required: true, message: "请输入系统名称", trigger: "blur" },
          { max: 50, message: "系统名称长度不允许超过50", trigger: "blur" }
        ],
        initialism: [
          { max: 10, message: "系统简称长度不允许超过10", trigger: "blur" }
        ],
        route: [
          { required: true, message: "请输入系统域名", trigger: "blur" },
          {
            validator: validateURL,
            message: "请输入正确格式的系统域名地址",
            trigger: "blur"
          },
          { max: 100, message: "系统域名长度不允许超过100", trigger: "blur" }
        ]
      },
      formLabelWidth: "120px"
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
    this.fetchData();
  },
  methods: {
    getConditionData() {
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
        this.$alert("系统 " + row.name + " 已禁用，不允许编辑！", "警告", {
          confirmButtonText: "关闭",
          type: "warning"
        });
        return;
      }

      this.formVisible = true;

      this.formModel = JSON.parse(JSON.stringify(row));
    },
    getPaged(flag) {
      console.log("condition: " + JSON.stringify(this.condition));

      if (!this.condition.id) {
        this.condition.id = undefined;
      }

      this.tableLoading = true;
      appSvc
        .getPagedApplicationList(this.condition)
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
      console.log("formModel: " + JSON.stringify(this.formModel));
      this.$refs[formName].validate(valid => {
        if (valid) {
          vm.btnSaveLoading = true;

          appSvc
            .createOrUpdateApplication(this.formModel)
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

      if (row.isDeleted) {
        this.$alert("系统 " + row.name + " 已禁用！", "警告", {
          confirmButtonText: "关闭",
          type: "warning"
        });
        return;
      }

      this.$confirm("是否要禁用 " + row.name + " 系统！", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "info"
      })
        .then(() => {
          appSvc
            .deleteApplicationById(row)
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