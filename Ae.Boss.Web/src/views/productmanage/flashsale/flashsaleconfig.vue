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
        :cell-style="cellStyle"
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
          <el-form-item label="活动时间">
            <el-date-picker
              style="width: 160px;"
              v-model="condition.startTime"
              type="date"
              placeholder="开始时间"
              size="mini"
            ></el-date-picker>
          </el-form-item>
          <el-form-item>
            <el-date-picker
              style="width: 160px;"
              v-model="condition.endTime"
              type="date"
              placeholder="结束时间"
              size="mini"
            ></el-date-picker>
          </el-form-item>
          <el-form-item prop="status" label="状态">
            <el-select
              v-model="condition.status"
              size="mini"
              style="width:100px;"
            >
              <el-option
                v-for="item in statusSel"
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
            @click="showCreate()"
            >新增</el-button
          >
        </template>
        <template v-slot:tb_cols>
          <rg-table-column
            label="开始时间"
            prop="activeStartTime"
            align="center"
          ></rg-table-column>
          <rg-table-column
            label="结束时间"
            prop="activeEndTime"
            align="center"
          ></rg-table-column>

          <rg-table-column
            label="产品名称"
            prop="productName"
            align="center"
          ></rg-table-column>
          <rg-table-column label="产品编号" prop="productId" />
          <rg-table-column label="数量" prop="num" />
          <rg-table-column label="售价" prop="price" align="center" />

          <rg-table-column label="状态" prop="status" align="center" />
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
                size="mini"
                type="danger"
                style="padding:10px 7px;"
                @click="updateflashsale(scope.row)"
                >删除</el-button
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
                  label="产品编号"
                  prop="productId"
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="formModel.productId"
                    :disabled="true"
                    placeholder="请输入产品编号"
                  />
                </el-form-item>
              </el-col>

              <el-col :span="6">
                <el-form-item>
                  <el-button
                    size="mini"
                    type="warning"
                    style="padding:10px 7px;margin-left:10px;"
                    @click="showsearchProduct()"
                    >检索商品</el-button
                  >
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="17">
                <el-form-item
                  label="产品名称"
                  prop="productName"
                  :label-width="formLabelWidth"
                >
                  <el-input :disabled="true" v-model="formModel.productName" />
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="10">
                <el-form-item
                  prop="num"
                  :label-width="formLabelWidth"
                  label="数量"
                >
                  <el-input v-model="formModel.num" placeholder="请输入数量" />
                </el-form-item>
              </el-col>
              <el-col :span="10">
                <el-form-item
                  prop="price"
                  :label-width="formLabelWidth"
                  label="售价"
                >
                  <el-input
                    v-model="formModel.price"
                    placeholder="请输入售价"
                  />
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="20">
                <el-form-item
                  prop="times"
                  :label-width="formLabelWidth"
                  label="活动时间"
                >
                  <el-date-picker
                    v-model="formModel.times"
                    type="datetimerange"
                    :picker-options="pickerOptions"
                    range-separator="至"
                    start-placeholder="开始日期"
                    end-placeholder="结束日期"
                    align="right"
                  >
                  </el-date-picker>
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
            v-loading="tableLoading"
            element-loading-text="加载中..."
            :data="productTableData"
            stripe
            size="mini"
            style="width: 100%"
            max-height="200px"
          >
            <!-- <el-table
          border
          ref="multipleTable"
          @selection-change="handleSelectionChange"
          :data="productTableData"
          style="width: 100%"
          >-->
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
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appSvc } from "@/api/product/flashsaleconfig";
import { isNumber } from "util";
export default {
  data() {
    return {
      pickerOptionsStart: {
        disabledDate(time) {
          return time.getTime() < Date.now();
          
        }
      },
      pickerOptions: {
        disabledDate(time) {
           var curDate = new Date();
          var preDate = new Date(curDate.getTime() - 24 * 60 * 60 * 1000);
          return time.getTime() <= preDate.getTime();
        },
        shortcuts: [
          {
            text: "最近一周",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近一个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近三个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit("pick", [start, end]);
            }
          }
        ]
      },
      input: undefined,

      readonly: true,
      tableLoading: false,
      currentPage: 1,

      //table页面的条件
      condition: {
        pageIndex: 1,
        pageSize: 20,
        status: undefined,
        startTime: undefined,
        productName: undefined,
        endTime: undefined
      },
      statusSel: [
        {
          value: "待审核",
          label: "待审核"
        },
        {
          value: "已生效",
          label: "已生效"
        },
        {
          value: "已结束",
          label: "已结束"
        },
        {
          value: "已取消",
          label: "已取消"
        }
      ],
      rules: {
        productId: [
          { required: true, message: "请输入产品编号", trigger: "blur" }
        ],
        productName: [
          { required: true, message: "请输入产品名称", trigger: "blur" }
        ],

        num: [{ required: true, message: "请输入数量", trigger: "blur" }],
        price: [{ required: true, message: "请输入价格", trigger: "blur" }],
        times: [{ required: true, message: "请选择活动时间", trigger: "blur" }]
      },
      updateflashsaleModel: {
        id: undefined
      },
      formModel: {
        activeStartTime: undefined,
        activeEndTime: undefined,
        productName: undefined,
        productId: undefined,
        num: undefined,
        price: undefined,
        times:[]
      },
      formModelInit: {
        activeStartTime: undefined,
        activeEndTime: undefined,
        productName: undefined,
        productId: undefined,
        num: undefined,
        price: undefined,
        times:[]
      },
      //产品搜索的model
      productTableData: [],
      productCondition: {
        productName: undefined
      },
      selectProductFormTitle: undefined,
      selectProductFormVisible: false,
      //产品搜索的model
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

    cellStyle(row, column, rowIndex, columnIndex) {
      debugger;
      //根据报警级别显示颜色
      // console.log(row);
      // console.log(row.column);
      if (row.row.status === "已取消" && row.column.label === "状态") {
        return "color:red";
      }
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

    clickRow(row, column, event) {
      console.log(row.id);
    },
    showCreate() {
      this.formVisible = true;
      this.formTitle = "新增产品活动配置";
      16;
    },
    save(formName) {
      var vm = this;

      this.$refs[formName].validate(valid => {
        if (valid) {
          appSvc
            .creatFlashSaleConfig(this.formModel)
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

    updateflashsale(row) {
      var vm = this;
      this.updateflashsaleModel.id = row.id;
      appSvc
        .updateFlashSaleConfig(this.updateflashsaleModel)
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
    },
    fetchData() {
      appSvc
        .getFlashSaleConfigs(this.condition)
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
        .getFlashSaleConfigs(this.condition)
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
