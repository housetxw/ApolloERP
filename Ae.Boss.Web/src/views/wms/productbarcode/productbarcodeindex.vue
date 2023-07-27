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
          <el-form-item>
            <el-input
              class="input"
              clearable
              placeholder="请输入产品名称或编号"
              prefix-icon="el-icon-search"
              v-model="condition.productName"
            ></el-input>
          </el-form-item>

          <el-form-item>
            <el-select
              v-model="condition.status"
              clearable
              filterable
              placeholder="请选择状态"
              class="margin-right-10"
            >
              <el-option
                v-for="item in statusSel"
                :key="item.label"
                :label="item.value"
                :value="item.label"
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
          <el-table-column label="条形码">
            <template slot-scope="scope">
              <img
                :src="scope.row.barCodeUrl"
                style="width:250px;height:75px;"
              />
            </template>
          </el-table-column>
          <el-table-column
            label="产品编号"
            prop="productId"
            align="center"
          ></el-table-column>

          <el-table-column
            label="产品名称"
            prop="productName"
            align="center"
          ></el-table-column>

          <el-table-column label="状态" align="center">
            <template slot-scope="scope">
              <label v-if="scope.row.status == '正常'">{{
                scope.row.status
              }}</label>
              <label style="color:red" v-else>{{ scope.row.status }}</label>
            </template>
          </el-table-column>
          <el-table-column
            label="创建人"
            prop="createBy"
            align="center"
          ></el-table-column>
          <el-table-column
            label="创建时间"
            prop="createTime"
            align="center"
          ></el-table-column>

          <el-table-column
            label="操作"
            width="150px"
            align="center"
            fixed="right"
          >
            <template slot-scope="scope">
              <el-button
                size="mini"
                type="danger"
                style="padding:10px 7px;"
                v-if="scope.row.status == '已取消' ? false : true"
                @click="deleteProductBarCode(scope.row)"
                >删除</el-button
              >
            </template>
          </el-table-column>
        </template>
      </rg-page>
    </section>
    <!-- 首页 -->

    <!-- 产品检索页面 -->
    <section id="selectProduct">
      <rg-dialog
        :title="selectProductFormTitle"
        :visible.sync="selectProductFormVisible"
        v-if="selectProductFormVisible"
        :beforeClose="selectProductCancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            selectProductCancel('formModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-form :inline="true">
            <el-form-item label="产品名称" :label-width="formLabelWidth">
              <el-input
                placeholder="请输入产品名称"
                style="width:300px;"
                v-model="productCondition.productName"
                size="mini"
              />
            </el-form-item>

            <el-form-item>
              <el-button
                type="primary"
                icon="el-icon-search"
                @click="searchProduct()"
                size="mini"
                >搜索</el-button
              >
            </el-form-item>
          </el-form>
          <el-table
            border
            ref="multipleTable"
            @selection-change="handleSelectionChange"
            :data="productTableData"
            stripe
            size="mini"
            style="width: 100%"
            max-height="200px"
          >
            <el-table-column type="selection"></el-table-column>
            <el-table-column
              label="产品编号"
              prop="productCode"
            ></el-table-column>
            <el-table-column label="产品名称" prop="name"></el-table-column>
            <el-table-column
              label="品牌"
              prop="brand"
              width="120px"
            ></el-table-column>

            <el-table-column
              label="单位"
              prop="unit"
              width="55px"
            ></el-table-column>
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="selectProductSave()"
            >保存</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 产品检索页面 -->

    <!-- 录入条形码页面 -->
    <section id="barcode">
      <rg-dialog
        :title="barCodeFormTitle"
        :visible.sync="barCodeFormVisible"
        v-if="barCodeFormVisible"
        :beforeClose="barCodeCancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            barCodeCancel('formModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-form>
            <el-form-item>
              <el-button
                type="primary"
                icon="el-icon-search"
                @click="addProduct()"
                size="mini"
                >添加产品</el-button
              >
            </el-form-item>
          </el-form>

          <el-table
            border
            :data="productBarCodeTableData"
            stripe
            size="mini"
            style="width: 100%"
            max-height="200px"
          >            
            <el-table-column
              label="产品编号"
              prop="productId"
            ></el-table-column>
            <el-table-column
              label="产品名称"
              prop="productName"
            ></el-table-column>

            <el-table-column label="条形码" prop="unit">
              <template slot-scope="scope">
                <el-input
                  placeholder="请输入条码"
                  v-model="scope.row.barCode"
                  clearable
                  size="mini"
                ></el-input>
              </template>
            </el-table-column>
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="barCodeSave()"
            >保存</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 产品检索页面 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { Loading } from "element-ui";
import { appSvc } from "@/api/wms/productbarcode";
import { isNumber } from "util";
export default {
  data() {
    return {
      pickerOptionsStart: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        }
      },
      input: undefined,

      readonly: true,
      tableLoading: false,
      currentPage: 1,

      //table页面的条件
      condition: {
        pageIndex: 1,
        pageSize: 10,
        productName: undefined,
        status: undefined
      },

      deleteFormModel: {
        id: undefined
      },
      statusSel: [
        {
          value: "待审核",
          label: "待审核"
        },
        {
          value: "正常",
          label: "正常"
        },
        {
          value: "已取消",
          label: "已取消"
        }
      ],
      formModel: {
        products: []
      },

      productModel: {
        productId: undefined,
        productName: undefined
      },
      productModelInit: {
        productId: undefined,
        productName: undefined
      },

      productBarCodeModel: {
        productId: undefined,
        productName: undefined,
        barCode: undefined
      },
      productBarCodeModelInit: {
        productId: undefined,
        productName: undefined,
        barCode: undefined
      },

      productBarCodeTableData: [],
      barCodeFormTitle: undefined,
      barCodeFormVisible: false,

      //产品搜索的model
      productTableData: [],
      productCondition: {
        productName: undefined
      },
      selectProductFormTitle: undefined,
      selectProductFormVisible: false,
      //产品搜索的model

      formModelInit: {},
      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",

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
    //页面初始化函数
    this.fetchData();
  },
  methods: {
    addProduct() {
      this.selectProductFormTitle = "添加产品";
      this.selectProductFormVisible = true;
    },
    barCodeCancel() {
      this.formModel.products = [];
      this.barCodeFormVisible = false;
      this.productBarCodeTableData = [];
    },
    barCodeSave() {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var existflag = false;
          for (var i = 0; i < this.productBarCodeTableData.length; i++) {
            if (
              this.productBarCodeTableData[i].barCode != "" &&
              this.productBarCodeTableData[i].barCode != undefined
            ) {
              existflag = true;
            }
          }

          if (!existflag) {
            this.$message({
              message: "请输入条形码!",
              type: "warning"
            });
          }

          var vm = this;
          const loading = this.$loading({
            lock: true,
            text: "拼命处理中......",
            spinner: "el-icon-loading",
            background: "rgba(0, 0, 0, 0.7)"
          });
          this.formModel.products = this.productBarCodeTableData;
          appSvc
            .addProductBarcodeConfig(this.formModel)
            .then(
              res => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  this.barCodeFormVisible = false;
                  vm.search();

                  vm.barCodeCancel();
                }
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally(() => {
              loading.close();
            });
        })
        .catch(error => {
          console.log(error);
        });
    },

    searchProduct() {
      if (
        this.productCondition.productName != null &&
        this.productCondition.productName != ""
      ) {
        const loading = this.$loading({
          lock: true,
          text: "拼命加载中......",
          spinner: "el-icon-loading",
          background: "rgba(0, 0, 0, 0.7)"
        });
        appSvc
          .productSearch(this.productCondition)
          .then(
            res => {
              debugger;
              var resData = res.data;

              this.productTableData = resData.items;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {})
          .finally(() => {
            loading.close();
          });
      } else {
        this.$message({
          message: "请输入产品名称!",
          type: "warning"
        });
      }
    },
    toggleSelection(rows) {
      if (rows) {
        rows.forEach(row => {
          this.$refs.multipleTable.toggleRowSelection(row);
        });
      } else {
        this.$refs.multipleTable.clearSelection();
      }
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    selectProductCancel() {
      this.productCondition.productName = undefined;
      this.selectProductFormVisible = false;
      this.productTableData = [];
    },

    //输入产品名称 检索产品编号
    selectProductSave() {
      var vm = this;
      //只能选择一个产品
      var productCode = "";
      var productName = "";
      if (this.$refs.multipleTable.selection.length > 0) {
        for (var i = 0; i < this.$refs.multipleTable.selection.length; i++) {
          this.productBarCodeModel.productId = this.$refs.multipleTable.selection[
            i
          ].productCode;
          this.productBarCodeModel.productName = this.$refs.multipleTable.selection[
            i
          ].name;

          this.productBarCodeTableData.push(this.productBarCodeModel);

          this.productBarCodeModel = JSON.parse(
            JSON.stringify(this.productBarCodeModelInit)
          );
        }
        this.selectProductCancel();
      } else {
        this.$message({
          message: "请选择产品!",
          type: "warning"
        });
      }
    },
    deleteProductBarCode(row) {
      var vm = this;
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.deleteFormModel.id = row.id;
          appSvc
            .deleteProductBarcodeConfig(this.deleteFormModel)
            .then(
              res => {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                }

                vm.search();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(error => {
          console.log(error);
        });
    },
    cellStyle(row, column, rowIndex, columnIndex) {
      if (row.row.taskStatus === "已驳回" && row.column.label === "状态") {
        return "color:red";
      }

      if (row.row.productStatus === "已驳回" && row.column.label === "状态") {
        return "color:red";
      }
    },
    showCreate() {
      this.barCodeFormVisible = true;
      this.barCodeFormTitle = "新增产品条形码配置";
    },
    //新增采购信息配置

    fetchData() {
      appSvc
        .getProductBarcodeConfigs(this.condition)
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
        .getProductBarcodeConfigs(this.condition)
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
