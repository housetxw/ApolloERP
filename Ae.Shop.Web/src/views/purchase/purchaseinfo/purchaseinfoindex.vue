<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <header class="bodyTop">
        <article class="filter-container">
          <span class="input-label">供应商:</span>
          <el-select
            v-model="condition.venderId"
            size="small"
            clearable
            filterable
            placeholder="请选择供应商"
            class="margin-right-10"
          >
            <el-option
              v-for="item in venderSel"
              :key="item.id"
              :label="item.venderShortName"
              :value="item.id"
            ></el-option>
          </el-select>

          <span class="input-label">状态:</span>
          <el-select
            v-model="condition.status"
            size="small"
            clearable
            filterable
            placeholder="请选择状态"
            class="margin-right-10"
          >
            <el-option
              v-for="item in statusSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>

          <span class="input-label">产品名称:</span>
          <el-input
            v-model="condition.productName"
            clearable
            style="width:200px;"
            placeholder="请输入产品名称或编码"
          />

          <el-button
            type="primary"
            size="small"
            style="margin-left:30px;"
            @click="search(true)"
            icon="el-icon-search"
          >搜索</el-button>
          <el-button type="primary" size="small" @click="showCreate()">新增</el-button>
        </article>
      </header>
      <main>
        <section>
          <el-table border :data="tableData" style="width: 100%" :cell-style="cellStyle">
            <el-table-column label="供应商" prop="venderShortName"></el-table-column>
            <el-table-column label="产品编号" prop="productId"></el-table-column>

            <el-table-column label="产品名称" prop="productName"></el-table-column>
            <el-table-column label="采购价" prop="purchasePrice" width="90px"></el-table-column>

            <el-table-column label="成本价" prop="costPrice" width="90px"></el-table-column>
            <el-table-column label="状态" prop="status" width="100px"></el-table-column>
            <el-table-column label="创建人" prop="createBy" width="120px"></el-table-column>
            <el-table-column label="创建时间" prop="createTime"></el-table-column>
            <el-table-column label="操作" width="150px">
              <template slot-scope="scope">
                <el-button
                  size="mini"
                  type="primary"
                  style="padding:10px 7px;"
                  @click="editPurchaseInfo(scope.row)"
                >编辑</el-button>
                <el-button
                  size="mini"
                  type="danger"
                  style="padding:10px 7px;"
                  @click="deletePurchaseInfo(scope.row)"
                >删除</el-button>
              </template>
            </el-table-column>
          </el-table>
        </section>
        <section class="pagination">
          <el-pagination
            background
            :page-size="10"
            :page-sizes="[10, 30, 50, 100]"
            layout="total, sizes, prev, pager, next, jumper"
            :total="totalList"
            @current-change="pageTurning"
            :current-page.sync="currentPage"
            @size-change="sizeChange"
          ></el-pagination>
        </section>
      </main>
    </section>
    <!-- 首页 -->

    <section id="create">
      <el-dialog
        :title="formTitle"
        :close-on-click-modal="false"
        :visible.sync="formVisible"
        :before-close="cancel"
      >
        <el-form :model="formModel" :inline="true" :rules="rules" ref="formModel">
          <el-form-item label="供应商" prop="venderShortName">
            <el-select
              v-model="formModel.venderShortName"
              size="small"
              @change="updatevender"
              clearable
              filterable
              placeholder="请选择供应商"
            >
              <el-option
                v-for="item in venderSel"
                :key="item.id"
                :label="item.venderShortName"
                :value="item.id"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="产品编号:" prop="productId">
            <el-input v-model="formModel.productId" :disabled="true" placeholder="请输入产品编号" />
          </el-form-item>&nbsp;&nbsp;&nbsp;&nbsp;
          <el-form-item>
            <el-button
              size="mini"
              type="warning"
              style="padding:10px 7px;"
              @click="showsearchProduct()"
            >检索商品</el-button>
          </el-form-item>

          <el-form-item label="产品名称:">
            <el-input :disabled="true" style="width:400px;" v-model="formModel.productName" />
          </el-form-item>
          <el-form-item label="采购价:" prop="purchasePrice">
            <el-input
              oninput="value=value.replace(/[^\d.]/g,'')"
              v-model="formModel.purchasePrice"
              placeholder="请输入采购价"
              @change="priceChange()"
            />
          </el-form-item>
          <el-form-item label="成本价:" prop="costPrice">
            <el-input
              oninput="value=value.replace(/[^\d.]/g,'')"
              v-model="formModel.costPrice"
              placeholder="请输入成本价"
              @change="priceChange()"
            />
          </el-form-item>&nbsp;&nbsp;&nbsp;&nbsp;
          <el-form-item label="备注:">
            <el-input placeholder="请输入备注" type="textarea" :rows="6" v-model="formModel.remark" />
          </el-form-item>
        </el-form>

        <div slot="footer" class="dialog-footer">
          <el-button @click="cancel('formModel')">取消</el-button>
          <el-button type="primary" @click="save('formModel')">保存</el-button>
        </div>
      </el-dialog>
    </section>

    <section id="edit">
      <el-dialog
        :title="editformTitle"
        :close-on-click-modal="false"
        :visible.sync="editformVisible"
        :before-close="editcancel"
      >
        <el-form :model="formModel" :inline="true" :rules="rules" ref="formModel">
          <el-form-item label="产品编号:">
            <el-input :disabled="true" v-model="formModel.productId" />
          </el-form-item>

          <el-form-item label="产品名称:">
            <el-input :disabled="true" style="width:400px;" v-model="formModel.productName" />
          </el-form-item>

          <el-form-item label="采购价:" prop="purchasePrice">
            <el-input
              oninput="value=value.replace(/[^\d.]/g,'')"
              v-model="formModel.purchasePrice"
              placeholder="请输入采购单价"
            />
          </el-form-item>

          <el-form-item label="成本价:">
            <el-input v-model="formModel.costPrice" />
          </el-form-item>
          <br />
          <el-form-item label="备注:">
            <el-input
              placeholder="请输入备注"
              type="textarea"
              style="width:300px;height:120px;"
              v-model="formModel.remark"
            />
          </el-form-item>
        </el-form>
        <!-- 展示订单和采购单 -->

        <div slot="footer" class="dialog-footer">
          <el-button @click="editcancel('formModel')">取消</el-button>
          <el-button type="primary" @click="editsave('formModel')">保存</el-button>
        </div>
      </el-dialog>
    </section>

    <!-- 产品检索页面 -->
    <section id="selectProduct">
      <el-dialog
        :title="selectProductFormTitle"
        :close-on-click-modal="false"
        :visible.sync="selectProductFormVisible"
        :before-close="selectProductCancel"
      >
        <el-form :inline="true">
          <el-form-item label="产品名称:">
            <el-input
              placeholder="请输入产品名称"
              style="width:300px;"
              v-model="productCondition.productName"
            />
          </el-form-item>

          <el-form-item>
            <el-button type="primary" icon="el-icon-search" @click="searchProduct()">搜索</el-button>
          </el-form-item>
        </el-form>

        <el-table
          border
          ref="multipleTable"
          @selection-change="handleSelectionChange"
          :data="productTableData"
          style="width: 100%"
        >
          <el-table-column type="selection"></el-table-column>

          <el-table-column label="产品编号" prop="productCode"></el-table-column>
          <el-table-column label="产品名称" prop="name"></el-table-column>
          <el-table-column label="品牌" prop="brand" width="120px"></el-table-column>

          <el-table-column label="单位" prop="unit" width="55px"></el-table-column>
        </el-table>

        <div slot="footer" class="dialog-footer">
          <el-button @click="selectProductCancel()">取消</el-button>
          <el-button type="primary" @click="selectProductSave()">确定</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 产品检索页面 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/purchase/purchaseinfo";
import { appVenderSvc } from "@/api/purchase/vender";
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
      //flag: this.global.deletedFlag,
      //下拉框的条件

      statusSelCondition: {
        RequestType: 1
      },

      //table页面的条件
      condition: {
        pageIndex: 1,
        pageSize: 10,
        venderId: undefined,
        status: undefined,

        productId: undefined
      },

      deleteCondition: {
        id: undefined
      },

      editformTitle: undefined,
      editformVisible: undefined,
      statusSel: [],
      venderSel: [],
      formModel: {
        id: undefined,
        venderShortName: undefined,
        productId: undefined,
        productName: undefined,
        purchasePrice: undefined,
        costPrice: undefined,
        remark: undefined,
        venderId: undefined
      },

      formModelInit: {
        id: undefined,
        venderShortName: undefined,
        productId: undefined,
        purchasePrice: undefined,
        costPrice: undefined,
        remark: undefined
      },
      //产品搜索的model
      productTableData: [],
      productCondition: {
        productName: undefined
      },
      selectProductFormTitle: undefined,
      selectProductFormVisible: false,
      //产品搜索的model

      selectSopoCondition: {
        purchaseProductId: undefined
      },
      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",
      rules: {
        costPrice: [
          { required: true, message: "请输入成本价", trigger: "blur" }
        ],
        purchasePrice: [
          { required: true, message: "请输入采购价", trigger: "blur" }
        ],

        productId: [
          { required: true, message: "请输入产品编号", trigger: "blur" }
        ],
        venderShortName: [
          { required: true, message: "请选择供应商", trigger: "blur" }
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
    //页面初始化函数
    this.fetchData();
  },
  methods: {
    cellStyle(row, column, rowIndex, columnIndex) {
      //根据报警级别显示颜色
      // console.log(row);
      // console.log(row.column);
      if (row.row.taskStatus === "已驳回" && row.column.label === "状态") {
        return "color:red";
      }

      if (row.row.productStatus === "已驳回" && row.column.label === "状态") {
        return "color:red";
      }
      // else if(row.column.label==="告警级别"&& row.row.alarmLevel==="一般告警" ){
      //   return 'color:yellow'
      // }
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
    showsearchProduct() {
      this.selectProductFormTitle = "查询产品";
      this.selectProductFormVisible = true;
      //this.productCondition.productName = "金冷 冷冻油 R-134a 汽车空调压";
      // appSvc
      //   .productSearch(this.productCondition)
      //   .then(
      //     res => {
      //       debugger;
      //       var resData = res.data;

      //       this.productTableData = resData.items;
      //     },
      //     error => {
      //       console.log(error);
      //     }
      //   )
      //   .catch(() => {});
    },
    searchProduct() {
      if (
        this.productCondition.productName != null &&
        this.productCondition.productName != ""
      ) {
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
          .catch(() => {});
      } else {
        this.$message({
          message: "请输入产品名称!",
          type: "warning"
        });
      }
    },

    selectProductCancel() {
      this.productCondition.productName = undefined;
      this.selectProductFormVisible = false;
      this.productTableData = [];
    },

    //输入产品名称 检索产品编号
    selectProductSave() {
      //只能选择一个产品
      var productCode = "";
      var productName = "";
      if (this.$refs.multipleTable.selection.length > 0) {
        if (this.$refs.multipleTable.selection.length < 2) {
          for (var i = 0; i < this.$refs.multipleTable.selection.length; i++) {
            productCode = this.$refs.multipleTable.selection[i].productCode;
            productName = this.$refs.multipleTable.selection[i].name;
          }
          this.formModel.productId = productCode;
          this.formModel.productName = productName;

          this.selectProductFormVisible = false;
          this.productCondition.productName = undefined;
          this.productTableData = [];
        } else {
          this.$message({
            message: "最多只能选择一个产品!",
            type: "warning"
          });
        }
      } else {
        this.$message({
          message: "请选择产品!",
          type: "warning"
        });
      }
    },

    selectProduct() {},

    //新增采购信息配置

    showCreate() {
      this.formVisible = true;
      this.formTitle = "新增采购信息配置";
    },
    save(formName) {
      var vm = this;
      console.log("formModel: " + JSON.stringify(this.formModel));
      if (this.formModel.costPrice <= 0) {
        this.$message({
          message: "成本价必须大于0",
          type: "warning"
        });
        return;
      }
      if (this.formModel.purchasePrice <= 0) {
        this.$message({
          message: "采购价必须大于0",
          type: "warning"
        });
        return;
      }
      this.$refs[formName].validate(valid => {
        if (valid) {
          appSvc
            .createPurchaseInfoConfig(this.formModel)
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
    //新增采购信息配置

    // 编辑采购信息配置
    editPurchaseInfo(row) {
      this.editformTitle = "编辑采购信息";
      this.editformVisible = true;

      this.formModel.id = row.id;
      this.formModel.productId = row.productId;
      this.formModel.productName = row.productName;
      this.formModel.costPrice = row.costPrice;
      this.formModel.purchasePrice = row.purchasePrice;
    },

    editcancel(formName) {
      this.editformVisible = false;
      // this.resetSendForm();
      this.formModel = JSON.parse(JSON.stringify(this.formModelInit));
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },
    editsave(formName) {
      this.$confirm("确定保存吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          console.log("condition: " + JSON.stringify(this.formModel));
          appSvc
            .eEditPurchaseInfoConfig(this.formModel)
            .then(
              res => {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                }
                vm.search();

                this.editformVisible = false;
                // this.resetSendForm();
                this.formModel = JSON.parse(JSON.stringify(this.formModelInit));

                vm.cancel();
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
        })
        .catch(() => {});
    },
    // 编辑采购信息配置
    deletePurchaseInfo(row) {
      this.$confirm("确定删除吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          this.deleteCondition.id = row.id;
          appSvc
            .deletePurchaseInfoConfig(this.deleteCondition)
            .then(
              res => {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                }

                vm.search();

                vm.cancel();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },

    updatevender(val) {
      //locations是v-for里面的也是datas里面的值
      let obj = {};
      var venderName = "";
      for (var i = 0; i < this.venderSel.length; i++) {
        if (this.venderSel[i].id == Number(val))
          venderName = this.venderSel[i].venderShortName;
      }

      // obj = this.warehouseSel.find(item => {
      //   return item.key === Number(val);
      // });
      this.formModel.venderShortName = venderName;
      this.formModel.venderId = Number(val);
    },

    clickRow(row, column, event) {
      console.log(row.id);
      // console.log(column)
      // console.log(event)
    },

    getpurchaseinfostatus() {
      appSvc
        .getBasicInfoList(this.statusSelCondition)
        .then(
          res => {
            //  this.tableData = res.data;
            this.statusSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    getVenders() {
      appVenderSvc
        .getVenders()
        .then(
          res => {
            this.venderSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    fetchData() {
      this.getpurchaseinfostatus();
      this.getVenders();
      appSvc
        .getPurchaseInfoConfigPages(this.condition)
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
    sizeChange(val) {
      this.condition.pageIndex = this.currentPage = 1;
      this.condition.pageSize = val;
      this.getPaged();
    },
    pageTurning() {
      this.condition.pageIndex = this.currentPage;
      this.getPaged();
    },

    getPaged(flag) {
      this.tableLoading = true;
      if (this.condition.venderId == "") {
        this.condition.venderId = undefined;
      }

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getPurchaseInfoConfigPages(this.condition)
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
    },

    outCancel(formName) {
      this.formVisible = false;
      // this.resetSendForm();
      this.outFormModel = JSON.parse(JSON.stringify(this.outFormModelInit));
      var frmName =
        typeof formName === "string" && formName ? formName : "outFormModel";
      this.$refs[frmName].resetFields();
    }
  }
};
</script>

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
</style>
