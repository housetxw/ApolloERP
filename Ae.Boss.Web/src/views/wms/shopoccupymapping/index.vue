<template>
  <main class="container">
    <!-- 首页 -->
    <section>
      <rg-page
        background
        id="indexPage"
        :pageIndex="condition.pageIndex"
        :pageSize="condition.pageSize"
        :dataCount="totalList"
        :tableLoading="tableLoading"
        :tableData="tableData"
        :pageChange="pageChange"
        :headerBtnWidth="180"
        :searching="search"
      >
        <template v-slot:condition>
          <el-form-item label="门店">
            <el-select
              v-model="condition.shopId"
              filterable
              remote
              clearable
              reserve-keyword
              placeholder="请输入门店名称"
              :remote-method="getShopinfo"
              :loading="loading"
            >
              <el-option
                v-for="item in shopSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="前置仓">
            <el-select
              v-model="condition.relationShopId"
              filterable
              remote
              clearable
              reserve-keyword
              placeholder="请输入前置仓名称"
              :remote-method="getShopinfo1"
              :loading="loading"
            >
              <el-option
                v-for="item in shopSel1"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
        </template>
        <template v-slot:header_btn>
          <el-button
            type="primary"
            size="mini"
            icon="el-icon-plus"
            @click="showCreate()"
            >新增</el-button
          >
        </template>
        <template v-slot:tb_cols>
          <rg-table-column
            label="门店名称"
            prop="shopName"
            align="center"
          ></rg-table-column>
          <rg-table-column
            label="前置仓"
            prop="relationShopName"
            align="center"
          ></rg-table-column>

          <rg-table-column label="状态" align="center">
            <template slot-scope="scope">
              <label style="color:red;" v-if="scope.row.status == '已失效'">{{
                scope.row.status
              }}</label>
              <label v-else>{{ scope.row.status }}</label>
            </template>
          </rg-table-column>
          <rg-table-column label="创建人" prop="createBy" align="center" />

          <rg-table-column label="创建时间" prop="createTime" align="center" />
          <rg-table-column
            label="操作"
            width="150px"
            align="center"
            fixed="right"
          >
            <template slot-scope="scope">
              <el-button
                v-if="scope.row.status == '正常'"
                size="mini"
                type="danger"
                style="padding:10px 7px;"
                @click="deleteShopOccupyMapping(scope.row, 1)"
                >删除</el-button
              >
              <el-button
                v-else
                size="mini"
                type="warning"
                style="padding:10px 7px;"
                @click="deleteShopOccupyMapping(scope.row, 0)"
                >恢复</el-button
              >
              <el-button
                size="mini"
                type="primary"
                style="padding:10px 7px;"
                v-if="scope.row.status == '正常'"
                @click="updateShopOccupyMapping(scope.row)"
                >修改</el-button
              >
            </template>
          </rg-table-column>
        </template>
      </rg-page>
    </section>
    <!-- 首页 -->

    <section id="create">
      <rg-dialog
        :title="formTitle"
        :visible.sync="formVisible"
        v-if="formVisible"
        :beforeClose="cancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            cancel('formModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="600px"
      >
        <template v-slot:content>
          <el-form
            :model="formModel"
            :rules="rules"
            ref="formModel"
            size="small"
          >
            <el-row>
              <el-col :span="17">
                <el-form-item
                  label="门店名称"
                  prop="shopId"
                  :label-width="formLabelWidth"
                >
                  <el-select
                    v-model="formModel.shopId"
                    filterable
                    remote
                    clearable
                    reserve-keyword
                    placeholder="请输入门店名称"
                    :remote-method="getShopinfo2"
                    :loading="loading"
                  >
                    <el-option
                      v-for="item in shopSel2"
                      :key="item.key"
                      :label="item.value"
                      :value="item.key"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="17">
                <el-form-item
                  label="前置仓"
                  prop="relationShopId"
                  :label-width="formLabelWidth"
                >
                  <el-select
                    v-model="formModel.relationShopId"
                    filterable
                    remote
                    clearable
                    reserve-keyword
                    placeholder="请输入门店名称"
                    :remote-method="getShopinfo3"
                    :loading="loading"
                  >
                    <el-option
                      v-for="item in shopSel3"
                      :key="item.key"
                      :label="item.value"
                      :value="item.key"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="17">
                <el-form-item label="备注" :label-width="formLabelWidth">
                  <el-input
                    v-model="formModel.remark"
                    clearable
                    type="textarea"
                    :rows="2"
                    placeholder="请输入备注"
                  />
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
            @click="save('formModel')"
            >保存</el-button
          >
        </template>
      </rg-dialog>
    </section>

    <!-- 修改仓库-->
    <section id="update">
      <rg-dialog
        :title="updateformTitle"
        :visible.sync="upateformVisible"
        v-if="upateformVisible"
        :beforeClose="updatecancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            updatecancel('formModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="600px"
      >
        <template v-slot:content>
          <el-form
            :model="formModel"
            :rules="rules"
            ref="formModel"
            size="small"
          >
            <el-row>
              <el-col :span="17">
                <el-form-item
                  label="门店名称"
                  prop="shopId"
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="formModel.shopName"
                    clearable
                    :disabled="true"
                  />
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="17">
                <el-form-item
                  label="前置仓"
                  prop="relationShopId"
                  :label-width="formLabelWidth"
                >
                  <el-select
                    v-model="formModel.relationShopName"
                    filterable
                    remote
                    clearable
                    reserve-keyword
                    placeholder="请输入门店名称"
                    :remote-method="getShopinfo3"
                    :loading="loading"
                  >
                    <el-option
                      v-for="item in shopSel3"
                      :key="item.key"
                      :label="item.value"
                      :value="item.key"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="17">
                <el-form-item label="备注" :label-width="formLabelWidth">
                  <el-input
                    v-model="formModel.remark"
                    clearable
                    type="textarea"
                    :rows="2"
                    placeholder="请输入备注"
                  />
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
            @click="updatesave('formModel')"
            >保存</el-button
          >
        </template>
      </rg-dialog>
    </section>
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appSvc } from "@/api/wms/shopoccupymapping";
import { isNumber } from "util";
export default {
  data() {
    return {
      tableLoading: false,
      currentPage: 1,

      //table页面的条件
      condition: {
        pageIndex: 1,
        pageSize: 20,
        shopId: undefined,
        relationShopId: undefined
      },
      shopSelCondition: {
        simpleName: undefined
      },

      shopSelCondition1: {
        simpleName: undefined,
        shopType: 32
      },
      getCondition: {
        id: undefined
      },
      shopSel: [],
      shopSel1: [],
      shopSel2: [],
      shopSel3: [],

      rules: {
        relationShopId: [
          { required: true, message: "请选择门店", trigger: "blur" }
        ],
        shopId: [{ required: true, message: "请选择前置仓", trigger: "blur" }]
      },
      loading: false,
      formModel: {
        shopId: undefined,
        shopName: undefined,
        relationShopId: undefined,
        relationShopName: undefined,
        remark: undefined
      },
      formModelInit: {
        shopId: undefined,
        shopName: undefined,
        relationShopId: undefined,
        relationShopName: undefined,
        remark: undefined
      },

      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",

      formLabelWidth: "120px",
      deleteCondition: {
        id: undefined,
        status: undefined
      },
      upateformVisible: false,
      updateformTitle: undefined,
      updateCondition: {
        id: undefined,
        relationShopId: undefined,
        relationShopName: undefined
      }
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
    updateShopOccupyMapping(row) {
      this.upateformVisible = true;
      this.updateformTitle = "修改门店占库配置";

      this.getCondition.id = row.id;
      appSvc
        .getShopOccupyMappingInfo(this.getCondition)
        .then(res => {
          this.formModel = res.data;
        })
        .catch(error => {
          console.log(error);
        });
    },

    updatecancel() {
      this.upateformVisible = false;
      // this.resetSendForm();
      this.formModel = JSON.parse(JSON.stringify(this.formModelInit));
    },
    updatesave() {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var shopName = "";
          debugger;
          var shopId = this.formModel.relationShopName;
          for (var i = 0; i < this.shopSel3.length; i++) {
            if (this.shopSel3[i].key == shopId)
              shopName = this.shopSel3[i].value;
          }
          var vm = this;
          this.formModel.relationShopId = shopId;
          this.formModel.relationShopName = shopName;
          appSvc
            .updateShopOccupyMapping(this.formModel)
            .then(res => {
              if (res.code == 10000) {
                this.$message({
                  message: res.message,
                  type: "success"
                });
                vm.search();
                this.formVisible = false;
                vm.cancel();
              }
            })
            .catch(error => {
              console.log(error);
            });
        })

        .catch(error => {
          console.log(error);
        });
    },
    deleteShopOccupyMapping(row, status) {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          var statusText = status == 1 ? "已失效" : "正常";
          this.deleteCondition.id = row.id;
          this.deleteCondition.status = statusText;
          appSvc
            .deleteShopOccupyMapping(this.deleteCondition)
            .then(res => {
              if (res.code == 10000) {
                this.$message({
                  message: res.message,
                  type: "success"
                });
                vm.search();
                this.formVisible = false;
                vm.cancel();
              }
            })
            .catch(error => {
              console.log(error);
            });
        })
        .catch(error => {
          console.log(error);
        });
    },
    cellStyle(row, column, rowIndex, columnIndex) {
      debugger;
      //根据报警级别显示颜色
      console.log(row);
      // console.log(row.column);
      if (row.row.status === "正常" && row.column.label === "状态") {
        return "color:red";
      }
    },
    getShopinfo(query) {
      if (query != "") {
        debugger;
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          debugger;
          this.shopSelCondition.simpleName = query;
          appSvc
            .getShopList(this.shopSelCondition)
            .then(
              res => {
                this.shopSel = res.data;
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

    getShopinfo1(query) {
      if (query != "") {
        debugger;
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          debugger;
          this.shopSelCondition1.simpleName = query;
          appSvc
            .getShopListByType(this.shopSelCondition1)
            .then(
              res => {
                this.shopSel1 = res.data;
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
    getShopinfo2(query) {
      if (query != "") {
        debugger;
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          debugger;
          this.shopSelCondition.simpleName = query;
          appSvc
            .getShopList(this.shopSelCondition)
            .then(
              res => {
                this.shopSel2 = res.data;
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

    getShopinfo3(query) {
      if (query != "") {
        debugger;
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          debugger;
          this.shopSelCondition1.simpleName = query;
          appSvc
            .getShopListByType(this.shopSelCondition1)
            .then(
              res => {
                this.shopSel3 = res.data;
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

    clickRow(row, column, event) {
      console.log(row.id);
    },
    showCreate() {
      this.formVisible = true;
      this.formTitle = "新增门店占库配置";
    },

    save(formName) {
      var vm = this;

      this.$refs[formName].validate(valid => {
        if (valid) {
          var warehouseName = "";
          for (var i = 0; i < this.shopSel2.length; i++) {
            if (this.shopSel2[i].key == Number(this.formModel.shopId))
              warehouseName = this.shopSel2[i].value;
          }
          this.formModel.shopName = warehouseName;

          for (var i = 0; i < this.shopSel3.length; i++) {
            if (this.shopSel3[i].key == Number(this.formModel.relationShopId))
              warehouseName = this.shopSel3[i].value;
          }
          this.formModel.relationShopName = warehouseName;
          appSvc
            .addShopOccupyMapping(this.formModel)
            .then(res => {
              if (res.code == 10000) {
                this.$message({
                  message: res.message,
                  type: "success"
                });
                vm.search();
                this.formVisible = false;
                vm.cancel();
              }
            })
            .catch(error => {
              console.log(error);
            });
        } else {
          return false;
        }
      });
    },
    cancel(formName) {
      this.formVisible = false;
      // this.resetSendForm();
      this.formModel = JSON.parse(JSON.stringify(this.formModelInit));
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },

    fetchData() {
      appSvc
        .getShopOccupyMappings(this.condition)
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
    pageTurning() {
      this.condition.pageIndex = this.currentPage;
      this.getPaged();
    },

    getPaged(flag) {
      this.tableLoading = true;

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getShopOccupyMappings(this.condition)
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
<style lang="scss"></style>
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
>>> .el-dialog__wrapper .rg-dialog .dialog_body {
  padding: 20px 40px;
}
>>> .header-search,
.search-line {
  padding-bottom: 10px;
}
</style>
