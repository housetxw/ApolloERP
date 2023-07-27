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
      :pageChange="pageTurning"
      :searching="search"
      @expand-change="expandSelect"
      @row-click="clickRow"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-select v-model="condition.techId" clearable placeholder="请选择领用技师" style="width:150px">
            <el-option v-for="item in techSel" :key="item.id" :label="item.name" :value="item.id"></el-option>
          </el-select>
        </el-form-item>

        <el-form-item label>
          <el-input
            v-model="condition.productName"
            style="width:300px"
            autocomplete="off"
            placeholder="请输入产品名称或编码"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item label>
          <el-tooltip class="item" effect="dark" content="领取日期" placement="bottom">
            <el-date-picker
              v-model="condition.times"
              type="daterange"
              :picker-options="$root.$data.timeRgPickerOpt"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              align="center"
              style="width:210px"
            ></el-date-picker>
          </el-tooltip>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add">新增</el-button>
      </template>
      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <el-table-column type="expand">
          <template slot-scope="props">
            <el-table border :data="props.row.products" style="width: 100%" size="mini">
              <rg-table-column label="品类" prop="categoryName" fix-min/>
              <rg-table-column label="产品编号" prop="productId" fix-min/>
              <rg-table-column label="产品名称" prop="productName" />

              <rg-table-column label="退还" prop="actualNum" is-number fix-min/>
              <rg-table-column label="货损" prop="exceptionNum" is-number fix-min>
                <template slot-scope="scope">
                  <font v-if="scope.row.exceptionNum>0" style="color:red;">{{scope.row.exceptionNum}}</font>
                  <font v-else>{{scope.row.exceptionNum}}</font>
                </template>
              </rg-table-column>
              <rg-table-column label="丢失" prop="lessNum" is-number fix-min>
                <template slot-scope="scope">
                  <font v-if="scope.row.lessNum>0" style="color:red;">{{scope.row.lessNum}}</font>
                  <font v-else>{{scope.row.lessNum}}</font>
                </template>
              </rg-table-column>
            </el-table>
          </template>
        </el-table-column>
        <!-- Table数据 -->
        <!-- <el-table-column label="编号" prop="id" align="center" width="80px;"></el-table-column> -->
        <!-- <el-table-column label="门店名称" align="center" prop="locationName"></el-table-column> -->
        <rg-table-column label="退料编号" prop="id" width="150"></rg-table-column>
        <rg-table-column label="领料技师" prop="receiveName"></rg-table-column>
        <rg-table-column label="退料日期" prop="receiveTime" is-datetime fix-min></rg-table-column>
        <rg-table-column label="品类数" prop="categoryNum" is-number></rg-table-column>
        <rg-table-column label="单品数" prop="sumProductNum" is-number></rg-table-column>

        <rg-table-column label="经办人" prop="createBy"></rg-table-column>
        <rg-table-column label="操作时间" align="right" prop="createTime" is-datetime fix-min></rg-table-column>
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

    <!-- 新增出库单页面 -->
    <section id="outstockPage">
      <rg-dialog
        :title="formTitle"
        :visible.sync="formVisible"
        :beforeClose="cancel"
        :btnCancel="{label:'关闭',click:(done)=>{cancel('formModel')}}"
        :btnRemove="false"
        :footbar="true"
        width="90%"
        v-if="formVisible"
        maxWidth="1024px"
        minWidth="900px"
      >
        <template v-slot:content>
          <el-form :model="formModel" :rules="rules1" ref="formModel">
            <el-form-item label="退料时间" :label-width="formLabelWidth" prop="receiveTime">
              <el-date-picker
                v-model="formModel.receiveTime"
                size="mini"
                type="datetime"
                :picker-options="pickerOptions"
                placeholder="选择日期时间"
                default-time="12:00:00"
              ></el-date-picker>
            </el-form-item>

            <el-form-item label="领用人" :label-width="formLabelWidth" prop="techId">
              <el-select
                v-model="formModel.techId"
                size="mini"
                @change="changetech()"
                placeholder="请选择领用技师"
                style="width:220px"
              >
                <el-option
                  v-for="item in techTempSel"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                ></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="入库经办" :label-width="formLabelWidth">
              <label>{{formModel.createBy}}</label>
            </el-form-item>
            <el-form-item>
              <el-divider content-position="left">待退物料</el-divider>
            </el-form-item>
          </el-form>

          <el-table
            border
            :data="productStockTable"
            style="width: 100%"
            stripe
            element-loading-text="加载中..."
            size="mini"
          >
            <rg-table-column label="品类" prop="categoryName" />
            <rg-table-column label="产品编号" prop="productId" />
            <rg-table-column label="产品名称" prop="productName"></rg-table-column>

            <rg-table-column label="待还数量" prop="receiveNum" />
            <rg-table-column label="归还数量">
              <template slot-scope="scope">
                <el-input
                  placeholder="0"
                  size="mini"
                  style="width:100px;"
                  v-model="scope.row.actualNum"
                  oninput="value=value.replace(/[^\d.]/g,'')"
                  @blur="checkNum(scope.row,1)"
                  clearable
                ></el-input>
              </template>
            </rg-table-column>

            <rg-table-column label="货损数量">
              <template slot-scope="scope">
                <el-input
                  placeholder="0"
                  size="mini"
                  style="width:100px;"
                  v-model="scope.row.exceptionNum"
                  oninput="value=value.replace(/[^\d.]/g,'')"
                  @blur="checkNum(scope.row,2)"
                  clearable
                ></el-input>
              </template>
            </rg-table-column>

            <rg-table-column label="丢失数量">
              <template slot-scope="scope">
                <el-input
                  placeholder="0"
                  style="width:100px;"
                  size="mini"
                  v-model="scope.row.lessNum"
                  oninput="value=value.replace(/[^\d.]/g,'')"
                  @blur="checkNum(scope.row,3)"
                  clearable
                ></el-input>
              </template>
            </rg-table-column>
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="outsave('formModel')"
          >提交</el-button>
        </template>
      </rg-dialog>
    </section>
    <!-- 新增入库单页面 -->
  </main>
</template>

<script>
import { appSvc } from "@/api/stock/homecare";

export default {
  name: "demopage",
  data() {
    return {
      w_search_right: 100,
      tableLoading: false,
      pickerOptions: {
        disabledDate(time) {
          const date = new Date();
          date.setTime(date.getTime() - 3600 * 1000 * 24);
          return time.getTime() < date;
        }
      },
      condition: {
        productName: undefined,
        techId: undefined,
        pageIndex: 1,
        pageSize: 20,

        times: undefined
      },

      tableData: [],
      totalList: 0,
      techSel: [],

      formLabelWidth: "100px",

      formVisible: false,
      formTitle: "详情",
      productStockTable: [],
      formInit: {
        techId: undefined,
        receiveName: undefined,
        receiveTime: undefined,
        products: [],
        createBy: undefined
      },
      formModel: {
        techId: undefined,
        receiveName: undefined,
        receiveTime: undefined,
        products: [],
        createBy: undefined
      },
      techTempSel: [],
      productInfoModel: {
        categoryName: undefined,
        productId: undefined,
        productName: undefined,
        receiveNum: undefined,
        actualNum: undefined,
        exceptionNum: undefined,
        lessNum: undefined
      },

      productInfoModelInit: {
        categoryName: undefined,
        productId: undefined,
        productName: undefined,
        receiveNum: undefined,
        actualNum: undefined,
        exceptionNum: undefined,
        lessNum: undefined
      },

      submitproductInfoModel: {
        categoryName: undefined,
        productId: undefined,
        productName: undefined,
        receiveNum: undefined,
        actualNum: undefined,
        exceptionNum: undefined,
        lessNum: undefined
      },

      submitproductInfoModelInit: {
        categoryName: undefined,
        productId: undefined,
        productName: undefined,
        receiveNum: undefined,
        actualNum: undefined,
        exceptionNum: undefined,
        lessNum: undefined
      },

      techCondition: {
        techId: undefined
      },
      careproductCondition: {
        recordId: undefined
      },

      rules1: {
        receiveTime: [
          { required: true, message: "请选择出库时间", trigger: "blur" }
        ],
        techId: [{ required: true, message: "请选择技师", trigger: "blur" }]
      }
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    changetech() {
      if (this.formModel.techId != "" && this.formModel.techId != undefined) {
        this.techCondition.techId = this.formModel.techId;

        this.productStockTable = [];

        appSvc
          .getHomeReturnProductsByTech(this.techCondition)
          .then(
            res => {
              if (res.data != null && res.data.length > 0) {
                res.data.forEach(item => {
                  this.productInfoModel.productId = item.productId;
                  this.productInfoModel.categoryName = item.categoryName;
                  this.productInfoModel.productName = item.productName;
                  this.productInfoModel.receiveNum = item.receiveNum;
                  this.productStockTable.push(this.productInfoModel);
                  this.productInfoModel = JSON.parse(
                    JSON.stringify(this.productInfoModelInit)
                  );
                });
              }
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },
    checkNum(row, type) {
      debugger;

      var receiveNum =
        row.receiveNum != "" && row.receiveNum != undefined
          ? parseInt(row.receiveNum)
          : 0;
      var actualNum =
        row.actualNum != "" && row.actualNum != undefined
          ? parseInt(row.actualNum)
          : 0;
      var exceptionNum =
        row.exceptionNum != "" && row.exceptionNum != undefined
          ? parseInt(row.exceptionNum)
          : 0;
      var lessNum =
        row.lessNum != "" && row.lessNum != undefined
          ? parseInt(row.lessNum)
          : 0;

      if (type == 1) {
        if (receiveNum < actualNum + exceptionNum + lessNum) {
          row.actualNum = 0;
        }
      } else if (type == 2) {
        if (receiveNum < actualNum + exceptionNum + lessNum) {
          row.exceptionNum = 0;
        }
      } else if (type == 3) {
        if (receiveNum < actualNum + exceptionNum + lessNum) {
          row.lessNum = 0;
        }
      }
    },

    cancel() {
      this.formVisible = false;
      this.productStockTable = [];
      this.formModel = JSON.parse(JSON.stringify(this.formInit));
      this.techTempSel = [];
      this.submitproductInfoModel = JSON.parse(
        JSON.stringify(this.submitproductInfoModelInit)
      );
    },
    outsave(formName) {
      var vm = this;
      this.$refs[formName].validate(valid => {
        if (valid) {
          debugger;
          var existExceptionFlag = false;
          var existLessFlag = false;

          var isValid = false;
          for (var i = 0; i < this.productStockTable.length; i++) {
            if (
              (this.productStockTable[i].actualNum != undefined &&
                parseInt(this.productStockTable[i].actualNum) > 0) ||
              (this.productStockTable[i].exceptionNum != undefined &&
                parseInt(this.productStockTable[i].exceptionNum) > 0) ||
              (this.productStockTable[i].lessNum != undefined &&
                parseInt(this.productStockTable[i].lessNum) > 0)
            ) {
              isValid = true;
              break;
            } else {
              continue;
            }
          }

          if (!isValid) {
            this.$message({
              message: "请填写退料数量!",
              type: "warning"
            });
            return;
          }

          //confirm确认
          if (this.productStockTable.length > 0) {
            var exceptionItem = this.productStockTable.filter(item => {
              if (
                item.exceptionNum != undefined &&
                parseInt(item.exceptionNum) > 0
              ) {
                return item;
              }
            });
            if (exceptionItem != null && exceptionItem.length > 0) {
              existExceptionFlag = true;
            }

            var lessItem = this.productStockTable.filter(item => {
              if (item.lessNum != undefined && parseInt(item.lessNum) > 0) {
                return item;
              }
            });
            if (lessItem != null && lessItem.length > 0) {
              existLessFlag = true;
            }
          } else {
            this.$message({
              message: "请选择退料产品!",
              type: "warning"
            });
            return;
          }

          var confirmMsg = "";
          if (existExceptionFlag && existLessFlag) {
            confirmMsg =
              "本次退料包含货损及丢失件，系统将自动为该技师生成对应的物料实际出库记录。数据一旦提交，不可修改，确认继续?";
          } else if (existExceptionFlag && !existLessFlag) {
            confirmMsg =
              "本次退料包含货损件，系统将自动为该技师生成对应的物料实际出库记录。数据一旦提交，不可修改，确认继续?";
          } else if (!existExceptionFlag && existLessFlag) {
            confirmMsg =
              "  本次退料包含丢失件，系统将自动为该技师生成对应的物料实际出库记录。数据一旦提交，不可修改，确认继续?";
          } else {
            confirmMsg = "数据一旦提交，不可修改，确认继续?";
          }

          this.$confirm(confirmMsg, "信息", {
            confirmButtonText: "确定",
            cancelButtonText: "取消",
            type: "warning"
          })
            .then(() => {
              vm.btnSaveLoading = true;

              var techObj = this.techTempSel.find(item => {
                return item.id === this.formModel.techId;
              });

              this.formModel.receiveName = techObj.name;

              var newProductArr = [];
              //重新计算获取值
              this.productStockTable.forEach(item => {
                this.submitproductInfoModel.productId = item.productId;
                this.submitproductInfoModel.categoryName = item.categoryName;
                this.submitproductInfoModel.productName = item.productName;
                this.submitproductInfoModel.receiveNum = parseInt(
                  item.receiveNum
                );

                this.submitproductInfoModel.actualNum =
                  item.actualNum != undefined && parseInt(item.actualNum) > 0
                    ? parseInt(item.actualNum)
                    : 0;
                this.submitproductInfoModel.exceptionNum =
                  item.exceptionNum != undefined &&
                  parseInt(item.exceptionNum) > 0
                    ? parseInt(item.exceptionNum)
                    : 0;
                this.submitproductInfoModel.lessNum =
                  item.lessNum != undefined && parseInt(item.lessNum) > 0
                    ? parseInt(item.lessNum)
                    : 0;
                newProductArr.push(this.submitproductInfoModel);

                this.submitproductInfoModel = JSON.parse(
                  JSON.stringify(this.submitproductInfoModelInit)
                );
              });

              if (newProductArr.length > 0) {
                this.formModel.products = newProductArr;

                appSvc
                  .createHomeReturnRecord(this.formModel)
                  .then(
                    res => {
                      if (res.code == 10000) {
                        this.$message({
                          message: "操作成功",
                          type: "success"
                        });
                        vm.search();

                        vm.cancel();
                      }
                    },
                    err => {}
                  )
                  .catch(err => {
                    console.error(err);
                  })
                  .finally(() => {
                    vm.btnSaveLoading = false;
                  });
              } else {
                this.$message({
                  message: "请填写领取数量!",
                  type: "warning"
                });
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
    add() {
      this.formVisible = true;
      this.formTitle = "技师退料";
      appSvc
        .getCurrentUser()
        .then(
          res => {
            //  this.tableData = res.data;
            this.formModel.createBy = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});

      appSvc
        .getEmployes()
        .then(
          res => {
            //  this.tableData = res.data;
            this.techTempSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    expandSelect(row, expandedRows) {
      // expandedRows.length != 0 ? console.log(row.id) : ''
      if (expandedRows.length != 0) {
        this.careproductCondition.recordId = row.id;
        appSvc
          .getHomeReturnProducts(this.careproductCondition)
          .then(
            res => {
              //  this.tableData = res.data;
              var data = res.data;
              row.products = data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});

        //在tableData中循环

        // let obj = {};
        // obj = this.tableData.find(item => {
        //   return item.poId === row.poId;
        // });

        // row.products = obj.products;
      }
    },
    clickRow(row, column, event) {
      console.log(row.id);
      // console.log(column)
      // console.log(event)
    },

    getShopTechs() {
      appSvc
        .getEmployes()
        .then(
          res => {
            //  this.tableData = res.data;
            this.techSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    fetchData() {
      this.getShopTechs();
      appSvc
        .getHomeReturnRecordPages(this.condition)
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
    pageTurning() {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },

    getPaged(flag) {
      this.tableLoading = true;

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getHomeReturnRecordPages(this.condition)
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