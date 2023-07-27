<template>
  <main class="container">
    <!-- 首页：列表数据 -->
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"      
      :tableData="tableData"
      :pageChange="pageTurning"
      :searching="search"
      @expand-change="expandSelect"
      @row-click="clickRow"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-select
            v-model="condition.techId"
            clearable
            placeholder="请选择领用人"
            style="width:150px"
          >
            <el-option
              v-for="item in techSel"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            ></el-option>
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
          <el-tooltip
            class="item"
            effect="dark"
            content="领取日期"
            placement="bottom"
          >
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
        <!-- <el-form-item>
          <el-select
            v-model="condition.status"
            clearable
            placeholder="请选择状态"
            style="width:150px"
          >
            <el-option
              v-for="item in statusSel"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item> -->
        <el-form-item label v-if="false">
          <el-select
            v-model="condition.type"
            placeholder="请选择货物来源"
            style="width:150px"
          >
            <el-option
              v-for="item in productTypeSel"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
          >新增</el-button
        >
      </template>
      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <!-- <el-table-column label="编号" prop="id" align="center" width="80px;"></el-table-column> -->
        <!-- <el-table-column label="门店名称" align="center" prop="locationName"></el-table-column> -->
        <rg-table-column
          label="领用编号"
          prop="id"
          width="150"
        ></rg-table-column>
        <rg-table-column label="领用人" prop="techName"></rg-table-column>
        <rg-table-column
          label="领用日期"
          prop="receiveTime"
          is-datetime
          fix-min
        ></rg-table-column>

        <rg-table-column label="产品信息" prop="joinText"></rg-table-column>
        <rg-table-column label="备注" prop="remark"></rg-table-column>
        <rg-table-column label="状态" prop="status"></rg-table-column>
        <rg-table-column label="创建人" prop="createBy"></rg-table-column>
        <rg-table-column
          label="创建时间"
          align="right"
          prop="createTime"
          is-datetime
          fix-min
        ></rg-table-column>
        
            <rg-table-column label="操作" align="center" width="150">
              <template slot-scope="scope">   
                <el-button
                  type="danger"
                  size="mini"
                  @click="ClaimRepeatReduceStock(scope.row)"
                  >重新出库</el-button
                >             
                <el-button
                  type="danger"
                  size="mini"
                  v-if="scope.row.status != '已取消'"
                  @click="cancelRecord(scope.row)"
                  >取消</el-button
                >
              </template>
            </rg-table-column>
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

    <!-- 选择产品页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        :title="productformTitle"
        :visible.sync="productformVisible"
        :beforeClose="productcancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            productcancel('productformModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="800px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-form
            :model="productformModel"
            :inline="true"
            ref="productformModel"
          >
            <el-form-item>
              <el-input
                size="mini"
                placeholder="请输入产品名称"
                v-model="productformModel.key"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
            <el-form-item>
              <el-select
                v-model="productformModel.tempType"
                size="mini"
                placeholder="请选择类型"
                style="width:120px"
              >
                <el-option
                  v-for="item in productTypeSel"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item>
              <el-button
                icon="el-icon-search"
                size="mini"
                type="warning"
                @click="searchproduct()"
                >检索</el-button
              >
            </el-form-item>
          </el-form>

          <el-table
            border
            ref="multipleTable"
            :height="400"
            stripe
            size="mini"
            @selection-change="handleSelectionChange"
            :data="productTableData"
            style="width: 100%"
          >
            <el-table-column type="selection" align="center"></el-table-column>
            <el-table-column label="品类" prop="categoryName"></el-table-column>
            <el-table-column
              label="产品编号"
              prop="productCode"
            ></el-table-column>
            <el-table-column
              label="产品名称"
              prop="productName"
            ></el-table-column>
            <el-table-column
              label="采购价格"
              prop="costPrice"
            ></el-table-column>
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="saveProduct()"
            >确定</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 选择产品页面 -->

    <!-- 新增出库单页面 -->
    <section id="outstockPage">
      <rg-dialog
        :title="formTitle"
        :visible.sync="formVisible"
        :beforeClose="cancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            cancel('formModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="90%"
        v-if="formVisible"
        maxWidth="1024px"
        minWidth="900px"
      >
        <template v-slot:content>
          <el-form :model="formModel" :rules="rules1" ref="formModel">
            <!-- <el-form-item label="仓库" :label-width="formLabelWidth" prop="shopName">
              <el-input
                size="mini"
                :disabled="true"
                v-model="formModel.shopName"
                autocomplete="off"
                clearable
                style="width:220px;"
              ></el-input>
            </el-form-item> -->

            <el-form-item
              label="出库时间"
              :label-width="formLabelWidth"
              prop="receiveTime"
            >
              <el-date-picker
                v-model="formModel.receiveTime"
                size="mini"
                type="datetime"
                :picker-options="pickerOptions"
                placeholder="选择日期时间"
                default-time="12:00:00"
              ></el-date-picker>
            </el-form-item>

            <el-form-item
              label="领用人"
              :label-width="formLabelWidth"
              prop="techId"
            >
              <el-select
                v-model="formModel.techId"
                size="mini"
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
            
            <el-form-item
              label="备注"
              :label-width="formLabelWidth"
              prop="remark">
              <el-input
                size="mini"
                placeholder="请输备注"
                v-model="formModel.remark"
                autocomplete="off"
                style="width:220px"
                clearable
              ></el-input>
            </el-form-item>

            <el-form-item label="出库经办" :label-width="formLabelWidth">
              <label>{{ formModel.createBy }}</label>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" size="mini" @click="selectProduct()"
                >选择产品</el-button
              >
            </el-form-item>
          </el-form>

          <el-table
            border
            :data="productStockTable"
            style="width: 100%"
            stripe
          
            size="mini"
          >
            <rg-table-column label="品类" prop="categoryName" />
            <rg-table-column label="产品编号" prop="productId" />
            <rg-table-column
              label="产品名称"
              prop="productName"
            ></rg-table-column>

            <rg-table-column
              label="采购价格"
              prop="costPrice"
            ></rg-table-column>
            <rg-table-column label="库存数量" prop="stockNum" />
            <rg-table-column label="领取数量">
              <template slot-scope="scope">
                <el-input
                  placeholder="0"
                  size="mini"
                  v-model="scope.row.num"
                  oninput="value=value.replace(/[^\d.]/g,'')"
                  @blur="checkNum(scope.row)"
                  clearable
                ></el-input>
              </template>
            </rg-table-column>
            <rg-table-column label="操作" align="center" width="70">
              <template slot-scope="scope">
                <el-button
                  type="danger"
                  size="mini"
                  @click="removeItem(scope.$index, productStockTable)"
                  >移除</el-button
                >
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
            >提交</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 新增入库单页面 -->
  </main>
</template>

<script>
import { appSvc } from "@/api/stock/homecare";
import { Loading } from "element-ui";
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
        status: undefined,
        times: undefined
      },
      careproductCondition: {
        recordId: undefined
      },
      productStockCondition: {
        stocks: []
      },

      productStockModel: {
        categoryName: undefined,
        productId: undefined,
        productName: undefined,
        costPrice: undefined
      },

      productStockModelInit: {
        categoryName: undefined,
        productId: undefined,
        productName: undefined,
        costPrice: undefined
      },

      tableData: [],
      totalList: 0,
      techSel: [],
      statusSel: [
        {
          key: "未完结",
          value: "未完结"
        },
        {
          key: "正常完结",
          value: "正常完结"
        },
        {
          key: "异常完结",
          value: "异常完结"
        }
      ],
      formLabelWidth: "100px",
      productTableData: [],
      productformTitle: "",
      productformVisible: false,
      productformModel: {
        key: undefined,
        purchaseType: undefined,
        tempType: undefined,
        isHaoCai: 1
      },
      productformModelInit: {
        key: undefined,
        purchaseType: undefined,
        tempType: undefined,
        isHaoCai: 1
      },
      formVisible: false,
      formTitle: "详情",
      productStockTable: [],
      formInit: {
        shopName: undefined,
        techId: undefined,
        techName: undefined,
        receiveTime: undefined,
        products: [],
        createBy: undefined,
        receiveName: undefined
      },
      formModel: {
        shopName: undefined,
        techId: undefined,
        techName: undefined,
        receiveTime: undefined,
        products: [],
        createBy: undefined,
        receiveName: undefined
      },
      techTempSel: [],
      productInfoModel: {
        categoryName: undefined,
        productId: undefined,
        productName: undefined,
        costPrice: undefined,
        stockNum: undefined,
        num: undefined
      },

      productInfoModelInit: {
        categoryName: undefined,
        productId: undefined,
        productName: undefined,
        costPrice: undefined,
        stockNum: undefined,
        num: undefined
      },

      rules1: {
        shopName: [{ required: true, message: "", trigger: "blur" }],
        receiveTime: [
          { required: true, message: "请选择出库时间", trigger: "blur" }
        ],
        techId: [{ required: true, message: "请选择技师", trigger: "blur" }]
      },
      productTypeSel: [
        {
          key: "总部供应链",
          value: "总部供应链"
        },
        {
          key: "门店外采",
          value: "门店外采"
        }
      ]
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    checkNum(row) {
      if (parseInt(row.num) > parseInt(row.stockNum)) {
        row.num = 0;
      }
    },
    saveProduct() {
      // this.$confirm("确定操作吗?", "信息", {
      //   confirmButtonText: "确定",
      //   cancelButtonText: "取消",
      //   type: "warning"
      // })
      //   .then(() => {
          if (this.$refs.multipleTable.selection.length > 0) {
            // debugger;
            var productArr = [];
            for (
              var i = 0;
              i < this.$refs.multipleTable.selection.length;
              i++
            ) {
              var productCode = this.$refs.multipleTable.selection[i]
                .productCode;
              var productName = this.$refs.multipleTable.selection[i]
                .productName;

              var categoryName = this.$refs.multipleTable.selection[i]
                .categoryName;
              var costPrice = this.$refs.multipleTable.selection[i]
                .costPrice;

              this.productStockModel.productId = productCode;
              this.productStockModel.productName = productName;
              this.productStockModel.categoryName = categoryName;
              this.productStockModel.costPrice = costPrice;
              // this.stockItemModel.num = 0;
              //this.stockItemModel.tempnum = undefined;

              productArr.push(this.productStockModel);
              //格式化数据
              this.productStockModel = JSON.parse(
                JSON.stringify(this.productStockModelInit)
              );
            }

            this.productStockCondition.stocks = productArr;
            var purchaseType = 0;
            if (this.productformModel.tempType == "总部供应链") {
              purchaseType = 1;
            } else {
              purchaseType = 2;
            }

            this.productcancel();

            if (purchaseType == 1) {
              appSvc
                .getProductStocks(this.productStockCondition)
                .then(
                  res => {
                    if (res.data.length > 0) {
                      for (var i = 0; i < res.data.length; i++) {
                        var item = res.data[i];
                        var existProduct = this.productStockTable.find(r => {
                          return r.productId === item.productId;
                        });

                        if (existProduct != undefined) {
                          continue;
                        } else {
                          this.productInfoModel.categoryName =
                            item.categoryName;
                          this.productInfoModel.productId = item.productId;
                          this.productInfoModel.productName = item.productName;
                          this.productInfoModel.stockNum = item.stockNum;
                          this.productInfoModel.costPrice = item.costPrice;

                          this.productStockTable.push(this.productInfoModel);
                          this.productInfoModel = JSON.parse(
                            JSON.stringify(this.productInfoModelInit)
                          );
                        }
                      }
                    }
                    // this.productStockTable = res.data;
                  },
                  error => {
                    console.log(error);
                  }
                )
                .catch(() => {});
            } else if (purchaseType == 2) {
              appSvc
                .getProductStocksForOut(this.productStockCondition)
                .then(
                  res => {
                    if (res.data.length > 0) {
                      for (var i = 0; i < res.data.length; i++) {
                        var item = res.data[i];
                        var existProduct = this.productStockTable.find(r => {
                          return r.productId === item.productId;
                        });

                        if (existProduct != undefined) {
                          continue;
                        } else {
                          this.productInfoModel.categoryName =
                            item.categoryName;
                          this.productInfoModel.productId = item.productId;
                          this.productInfoModel.productName = item.productName;
                          this.productInfoModel.stockNum = item.stockNum;
                          this.productInfoModel.costPrice = item.costPrice;

                          this.productStockTable.push(this.productInfoModel);
                          this.productInfoModel = JSON.parse(
                            JSON.stringify(this.productInfoModelInit)
                          );
                        }
                      }
                    }
                    // this.productStockTable = res.data;
                  },
                  error => {
                    console.log(error);
                  }
                )
                .catch(() => {});
            }
            // debugger;
          } else {
            this.$message({
              message: "请选择产品!",
              type: "warning"
            });
          }
        // })
        // .catch(error => {
        //   console.log(error);
        // });
    },

    removeItem(index, rows) {
      //移除一行

      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          rows.splice(index, 1); //删掉该行
        })
        .catch(error => {
          console.log(error);
        });
    },
    cancel() {
      this.formVisible = false;
      this.productStockTable = [];
      this.formModel = JSON.parse(JSON.stringify(this.formInit));
      this.techTempSel = [];
    },
    cancelRecord(row) {
      this.$confirm("确定取消该领料记录吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          
          appSvc
            .CancelProductClaimRecord({id :row.id})
            .then(
              res => {
                //this.logTable = res.data;
                this.getPaged();
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
    ClaimRepeatReduceStock(row) {
      this.$confirm("确定重新出库吗?（不会重复扣减库存）", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          
          appSvc
            .ClaimRepeatReduceStock({id :row.id})
            .then(
              res => {
                //this.logTable = res.data;
                this.getPaged();
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
    outsave(formName) {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.$refs[formName].validate(valid => {
            if (valid) {
              var vm = this;
              //vm.btnSaveLoading = true;

              var techObj = this.techTempSel.find(item => {
                return item.id === this.formModel.techId;
              });

              this.formModel.techName = techObj.name;
              this.formModel.receiveName = techObj.name;
              if (this.productStockTable.length > 0) {
                var isValid = true;
                for (var i = 0; i < this.productStockTable.length; i++) {
                  if (
                    this.productStockTable[i].num != undefined &&
                    parseInt(this.productStockTable[i].num) > 0
                  ) {
                    continue;
                  } else {
                    isValid = false;
                    break;
                  }
                }

                if (isValid) {
                  this.formModel.products = this.productStockTable;

                  appSvc
                    .createProductClaimRecord(this.formModel)
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
                      //vm.btnSaveLoading = false;
                    });
                } else {
                  this.$message({
                    message: "请填写领取数量!",
                    type: "warning"
                  });
                }
              } else {
                this.$message({
                  message: "请选择产品!",
                  type: "warning"
                });
              }
            } else {
              return false;
            }
          });
        })
        .catch(error => {
          console.log(error);
        });
    },
    add() {
      this.formVisible = true;
      this.formTitle = "产品出库";
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

      // appSvc
      //   .getShopById()
      //   .then(
      //     res => {
      //       //  this.tableData = res.data;
      //       this.formModel.shopName = res.data.simpleName;
      //     },
      //     error => {
      //       console.log(error);
      //     }
      //   )
      //   .catch(() => {});
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
    productcancel() {
      this.productformVisible = false;

      this.productformModel = JSON.parse(
        JSON.stringify(this.productformModelInit)
      );
      this.$refs.multipleTable.clearSelection();
      this.productTableData = [];
    },

    selectProduct() {
      this.productformTitle = "选择产品";
      this.productformVisible = true;
    },

    searchproduct() {
      debugger;
      if (
        this.productformModel.tempType != "" &&
        this.productformModel.tempType != undefined
      ) {
        this.tableLoading = true;
        var purchaseType = -1;
        if (this.productformModel.tempType == "总部供应链") {
          purchaseType = 1;
        } else {
          purchaseType = 2;
        }
        this.productformModel.purchaseType = purchaseType;

        const loading = this.$loading({
          lock: true,
          text: "拼命处理中......",
          spinner: "el-icon-loading",
          background: "rgba(0, 0, 0, 0.7)"
        });
        appSvc
          .getShopProducts(this.productformModel)
          .then(
            res => {
              //  this.tableData = res.data;
              this.productTableData = res.data;
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
          message: "请选择类型！",
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

    expandSelect(row, expandedRows) {
      if (expandedRows.length != 0) {
        this.careproductCondition.recordId = row.id;
        appSvc
          .getProductClaimRecords(this.careproductCondition)
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
      }
    },
    clickRow(row, column, event) {
      console.log(row.id);
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
        .getProductClaimRecordPages(this.condition)
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
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },

    getPaged(flag) {
      this.tableLoading = true;

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getProductClaimRecordPages(this.condition)
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
