<template>
  <main class="container">
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :tableData="tableData"
      :pageChange="pageChange"
      :headerBtnWidth="140"
      :searching="search"
    >
      <template v-slot:condition>
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="输入产品编号或名称"
            style="width:180px"
            prefix-icon="el-icon-search"
            v-model="condition.searchContent"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="输入产品型号"
            style="width:150px"
            prefix-icon="el-icon-search"
            v-model="condition.specification"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-cascader
            style="width:180px;"
            v-model="condition.value"
            filterable
            clearable
            placeholder="请选择产品类目"
            :options="category"
            :props="optionProps"
          >
          </el-cascader>
        </el-form-item>
      </template>

      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
          >新增</el-button
        >
      </template>

      <template v-slot:tb_cols>
        <rg-table-column label="门店名称" prop="shopName" />
        <el-table-column
          label="产品编号"
          prop="productCode"
          :width="200"
        ></el-table-column>
        <rg-table-column label="产品名称" prop="productName" />
        <rg-table-column label="产品型号" prop="specification" />
        <el-table-column
          label="原厂编号"
          prop="oeNumber"
          :width="200"
        ></el-table-column>
        <el-table-column
          label="产品类目"
          prop="categoryName"
          :width="150"
        ></el-table-column>
        <el-table-column
          label="销售价"
          prop="salesPrice"
          :width="100"
        ></el-table-column>
        <el-table-column
          label="采购价"
          prop="purchasePrice"
          :width="100"
        ></el-table-column>

        <!-- <el-table-column label="采购单价" prop="purchasePrice" :width="100"></el-table-column> -->
        <el-table-column label="单位" prop="unit" :width="80"></el-table-column>
        <el-table-column label="操作" :width="100">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                @click="routeDetail(scope.row.productCode, scope.row.shopId)"
                type="primary"
                size="mini"
                >查看详情</el-button
              >
            </el-button-group>
          </template>
        </el-table-column>
      </template>
    </rg-page>

    <rg-dialog
      :title="formTitle"
      :visible.sync="dialogVisible"
      :beforeClose="cancel"
      :btnCancel="{
        label: '关闭',
        click: done => {
          cancel();
        }
      }"
      :btnRemove="false"
      :footbar="true"
      width="80%"
      maxWidth="600px"
      minWidth="600px"
    >
      <template v-slot:content>
        <el-form :model="addRequest" size="mini" label-width="120px">
          <el-form-item label="产品类目" required>
            <el-cascader
              style="width:200px;"
              v-model="addRequest.value"
              filterable
              :options="category"
              :props="optionProps"
            >
            </el-cascader>
          </el-form-item>
          <el-form-item label="产品名称" required>
            <el-input
              style="width:200px;"
              v-model="addRequest.productName"
            ></el-input>
          </el-form-item>
          <el-form-item label="原厂编号">
            <el-input
              style="width:200px;"
              v-model="addRequest.oeNumber"
            ></el-input>
          </el-form-item>
          <el-form-item label="型号规格">
            <el-input
              style="width:200px;"
              v-model="addRequest.specification"
            ></el-input>
          </el-form-item>
          <el-form-item label="产品单位">
            <el-select
              style="width:200px;"
              placeholder="请选择"
              filterable
              clearable
              v-model="addRequest.unit"
            >
              <el-option
                v-for="item in shopUnitEnum"
                :key="item"
                :label="item"
                :value="item"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="采购单价">
            <el-input
              style="width:200px;"
              v-model="addRequest.purchasePrice"
            ></el-input>
          </el-form-item>
          <el-form-item label="销售单价">
            <el-input
              style="width:200px;"
              v-model="addRequest.salesPrice"
            ></el-input>
          </el-form-item>
        </el-form>
      </template>

      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          size="mini"
          type="primary"
          :loading="btnSaveLoadingA"
          @click="saveProduct()"
          >提交</el-button
        >
      </template>
    </rg-dialog>

    <rg-dialog
      :title="formTitle1"
      :visible.sync="dialogVisible1"
      :loading="formLoading"
      :beforeClose="cancel1"
      :btnCancel="{
        label: '关闭',
        click: done => {
          cancel1();
        }
      }"
      :btnRemove="{
        label: '删除',
        loading: btnSaveLoadingD,
        click: deleteProduct
      }"
      :footbar="true"
      width="80%"
      maxWidth="600px"
      minWidth="600px"
    >
      <template v-slot:content>
        <el-form :model="detail" size="mini" label-width="120px">
          <el-form-item label="产品类目" required>
            <el-input
              style="width:200px;"
              disabled
              v-model="detail.categoryName"
            ></el-input>
            <!-- <el-select style="width:200px;" disabled placeholder="请选择" filterable v-model="detail.categoryId" >
                            <el-option
                                v-for="item in serviceTypeEnum"
                                :key="item.id"
                                :label="item.displayName"
                                :value="item.id">
                            </el-option>
                        </el-select> -->
          </el-form-item>
          <el-form-item label="产品名称" required>
            <el-input
              style="width:200px;"
              v-model="detail.productName"
            ></el-input>
          </el-form-item>
          <el-form-item label="原厂编号">
            <el-input style="width:200px;" v-model="detail.oeNumber"></el-input>
          </el-form-item>
          <el-form-item label="型号规格">
            <el-input
              style="width:200px;"
              v-model="detail.specification"
            ></el-input>
          </el-form-item>
          <el-form-item label="产品单位">
            <el-select
              style="width:200px;"
              placeholder="请选择"
              filterable
              clearable
              v-model="detail.unit"
            >
              <el-option
                v-for="item in shopUnitEnum"
                :key="item"
                :label="item"
                :value="item"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="采购单价">
            <el-input
              style="width:200px;"
              v-model="detail.purchasePrice"
            ></el-input>
          </el-form-item>
          <el-form-item label="销售单价">
            <el-input
              style="width:200px;"
              v-model="detail.salesPrice"
            ></el-input>
          </el-form-item>
        </el-form>
      </template>

      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          size="mini"
          type="primary"
          :loading="btnSaveLoadingE"
          @click="editProduct()"
          >提交</el-button
        >
      </template>
    </rg-dialog>
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/purchase/shopproduct";
export default {
  name: "shopproduct",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      condition: {
        pageIndex: 1,
        pageSize: 20,
        searchContent: "",
        specification: "",
        categoryId: 0,
        value: []
      },
      tableData: [],

      formTitle: "新增产品",
      dialogVisible: false,
      btnSaveLoadingA: false,
      addRequest: {
        categoryId: 0,
        oeNumber: "",
        productName: "",
        specification: "",
        salesPrice: "",
        purchasePrice: 0,
        unit: "",
        value: []
      },
      serviceTypeEnum: [],
      category: [],
      shopUnitEnum: [],
      formTitle1: "产品详情",
      dialogVisible1: false,
      detail: {
        productCode: "",
        productName: "",
        specification: "",
        categoryId: 0,
        CategoryName: "",
        salesPrice: 0,
        purchasePrice: 0,
        unit: "",
        oeNumber: ""
      },
      btnSaveLoadingE: false,
      formLoading: false,
      btnSaveLoadingD: false,
      optionProps: {
        expandTrigger: "hover",
        value: "categoryId",
        label: "displayName",
        children: "children"
      }
    };
  },
  created() {
    this.fetchData();
    this.getShopProductCategory();
    this.getServiceType();
    this.getProductUnit();
  },
  methods: {
    fetchData() {
      this.getPaged();
    },
    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    },
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    getPaged(flag) {
      this.tableLoading = true;
      var valueLength = this.condition.value.length;
      if (valueLength > 0) {
        this.condition.categoryId = this.condition.value[valueLength - 1];
      } else {
        this.condition.categoryId = 0;
      }
      appSvc
        .getShopProductList(this.condition)
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
          this.tableLoading = false;
        });
    },
    cancel() {
      this.dialogVisible = false;
    },
    cancel1() {
      this.dialogVisible1 = false;
    },
    add() {
      this.addRequest = {
        categoryId: 0,
        oeNumber: "",
        productName: "",
        specification: "",
        salesPrice: "",
        purchasePrice: "",
        unit: "",
        value: []
      };
      this.dialogVisible = true;
    },
    getShopProductCategory() {
      appSvc
        .getShopProductCategory()
        .then(
          res => {
            var data = res.data;
            this.category = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getServiceType() {
      appSvc
        .getShopServiceType()
        .then(
          res => {
            var data = res.data;
            this.serviceTypeEnum = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getProductUnit() {
      appSvc
        .getShopProductUnit()
        .then(
          res => {
            var data = res.data;
            this.shopUnitEnum = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    saveProduct() {
      debugger;
      var valueLength = this.addRequest.value.length;
      if (valueLength == 0) {
        this.$message({ message: "请选择产品类目", type: "warning" });
        return;
      }
      this.addRequest.categoryId = this.addRequest.value[valueLength - 1];
      // if(this.addRequest.oeNumber == ''){
      //     this.$message({message: "请输入产品编号",type: "warning"});
      //     return;
      // }
      if (this.addRequest.productName == "") {
        this.$message({ message: "请输入产品名称", type: "warning" });
        return;
      }

      var salesPrice = this.addRequest.salesPrice;
      var purchasePrice = this.addRequest.purchasePrice;
      //var purchasePrice = this.addRequest.purchasePrice;
      var s = /(^[1-9](\d+)?(\.\d{1,2})?$)|(^\d\.\d{1,2}$)/;
      // if(purchasePrice == ''){
      //     this.addRequest.purchasePrice = '0';
      // }
      // else{
      //     if(!s.test(purchasePrice)){
      //         this.$message({message: "采购单价格式有误，最多2位小数！",type: "warning"});
      //         return;
      //     }
      // }
      if (salesPrice == "" || salesPrice == "0") {
        this.addRequest.salesPrice = "0";
      } else {
        if (!s.test(salesPrice)) {
          this.$message({
            message: "销售单价格式有误，最多2位小数！",
            type: "warning"
          });
          return;
        }
      }

      if (purchasePrice == "" || purchasePrice == "0") {
        this.addRequest.purchasePrice = "0";
      } else {
        if (!s.test(purchasePrice)) {
          this.$message({
            message: "采购价格式有误，最多2位小数！",
            type: "warning"
          });
          return;
        }
      }

      this.btnSaveLoadingA = true;
      appSvc
        .saveShopProduct(this.addRequest)
        .then(
          res => {
            var data = res.data;
            this.$message({ message: "提交成功！", type: "success" });
            this.dialogVisible = false;
            this.getPaged(false);
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.btnSaveLoadingA = false;
        });
    },
    routeDetail(productCodePara, shopId) {
      var detailRequest = {
        productCode: productCodePara,
        shopId: shopId
      };
      this.formLoading = true;
      appSvc
        .getShopProductDetail(detailRequest)
        .then(
          res => {
            var data = res.data;
            if (data == null) {
              this.$message({
                message: "系统异常，请重试！",
                type: "error"
              });
              return;
            }
            this.detail = data;
            this.dialogVisible1 = true;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.formLoading = false;
        });
    },
    deleteProduct() {
      this.$confirm("确定要删除吗?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        closeOnClickModal: false,
        type: "warning"
      })
        .then(() => {
          var deleteRequest = {
            productCode: this.detail.productCode,
            shopId: this.detail.shopId
          };
          this.btnSaveLoadingD = true;
          appSvc
            .deleteShopProduct(deleteRequest)
            .then(
              res => {
                var data = res.data;
                if (data) {
                  this.$message({ message: "操作成功", type: "success" });
                  this.dialogVisible1 = false;
                  this.getPaged(false);
                } else {
                  this.$message({ message: "操作失败", type: "warning" });
                }
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally(() => {
              this.btnSaveLoadingD = false;
            });
        })
        .catch(() => {});
    },
    editProduct() {
      var editRequest = {
        productCode: this.detail.productCode,
        productName: this.detail.productName,
        specification: this.detail.specification,
        salesPrice: 0,
        purchasePrice: 0,
        unit: this.detail.unit,
        oeNumber: this.detail.oeNumber,
        shopId: this.detail.shopId,
        purchasePrice: 0
      };
      // if(this.detail.oeNumber == ''){
      //     this.$message({message: "请输入产品编号",type: "warning"});
      //     return;
      // }
      if (this.detail.productName == "") {
        this.$message({ message: "请输入产品名称", type: "warning" });
        return;
      }

      var salesPrice = this.detail.salesPrice;
      var purchasePrice = this.detail.purchasePrice;
      var s = /(^[1-9](\d+)?(\.\d{1,2})?$)|(^\d\.\d{1,2}$)/;
      // if(purchasePrice != ''){
      //     if(!s.test(purchasePrice)){
      //         this.$message({message: "采购单价格式有误，最多2位小数！",type: "warning"});
      //         return;
      //     }
      //     editRequest.purchasePrice = purchasePrice;
      // }
      if (salesPrice != "" && salesPrice != "0") {
        if (!s.test(salesPrice)) {
          this.$message({
            message: "销售单价格式有误，最多2位小数！",
            type: "warning"
          });
          return;
        }
        editRequest.salesPrice = salesPrice;
      }

      if (purchasePrice != "" && purchasePrice != "0") {
        if (!s.test(purchasePrice)) {
          this.$message({
            message: "采购单价格式有误，最多2位小数！",
            type: "warning"
          });
          return;
        }
        editRequest.purchasePrice = purchasePrice;
      }

      this.btnSaveLoadingE = true;
      appSvc
        .saveShopProduct(editRequest)
        .then(
          res => {
            var data = res.data;
            this.$message({ message: "提交成功！", type: "success" });
            this.dialogVisible1 = false;
            this.getPaged(false);
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.btnSaveLoadingE = false;
        });
    }
  }
};
</script>
