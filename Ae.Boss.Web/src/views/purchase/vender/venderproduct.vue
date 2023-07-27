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
      :headerBtnWidth="180"
      :searching="search"
    >
      <template v-slot:condition>
        <el-form-item>
          <el-select
            v-model="condition.venderIdS"
            size="mini"
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
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="请输入产品名称或Pid"
            style="width: 180px"
            prefix-icon="el-icon-search"
            v-model="condition.productName"
          >
          </el-input>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.stockStatus"
            size="mini"
            clearable
            filterable
            placeholder="供货状态"
            style="width: 120px"
          >
            <el-option
              v-for="item in stockStatusEnum"
              :key="item.id"
              :label="item.displayName"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
          >新增</el-button
        >
      </template>
      <template v-slot:tb_cols>
        <el-table-column label="供应商" :width="150">
          <template slot-scope="scope">
            <span>{{
              scope.row.venderShortName + " - " + scope.row.venderId
            }}</span>
          </template>
        </el-table-column>
        <el-table-column
          label="产品Pid"
          prop="productId"
          :width="126"
        ></el-table-column>
        <el-table-column label="产品名称" prop="productName"></el-table-column>
        <el-table-column
          label="条形码"
          prop="barCode"
          :width="100"
        ></el-table-column>
        <el-table-column
          label="品牌"
          prop="brand"
          :width="100"
        ></el-table-column>
        <el-table-column
          label="规格"
          prop="standard"
          :width="100"
        ></el-table-column>
        <el-table-column
          label="花纹"
          prop="figure"
          :width="70"
        ></el-table-column>
        <el-table-column
          label="价格"
          prop="price"
          :width="70"
        ></el-table-column>
        <el-table-column
          label="供货状态"
          prop="stockStatusDisplay"
          :width="70"
        ></el-table-column>
        <el-table-column
          label="创建人"
          prop="createBy"
          :width="100"
        ></el-table-column>
        <el-table-column
          label="创建时间"
          prop="createTime"
          :width="128"
        ></el-table-column>
        <el-table-column label="操作" align="center" fixed="right" :width="90">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                @click="updateStatus(scope.row.id)"
                type="danger"
                size="mini"
                >删除</el-button
              >
              <!-- <el-button
                @click="routeDetail(scope.row)"
                type="primary"
                size="mini"
                >详情</el-button
              > -->
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
        click: (done) => {
          cancel();
        },
      }"
      :btnRemove="false"
      :footbar="true"
      height="calc(100vh - 100px)"
      width="80%"
      maxWidth="1024px"
      minWidth="800px"
    >
      <template v-slot:content>
        <el-form :model="addVenderProduct" size="mini" label-width="120px">
          <el-form-item label="供应商" required>
            <el-select
              v-model="addVenderProduct.venderId"
              size="mini"
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
          <el-divider content-position="left">选择商品</el-divider>
          <rg-page
            id="productPage"
            :pageIndex="searchProduct.pageIndex"
            :pageSize="searchProduct.pageSize"
            :dataCount="totalList1"
            :tableLoading="tableLoading1"
            :tableData="tableData1"
            :pageChange="pageChange1"
            :headerBtnWidth="180"
            :searching="search1"
            :handleSelectionChange="handleSelectionChange"
          >
            <template v-slot:condition>
              <el-form-item>
                <el-select
                  v-model="searchProduct.brand"
                  size="mini"
                  filterable
                  clearable
                  style="width: 120px"
                  placeholder="请选择品牌"
                >
                  <el-option
                    v-for="item in brandList"
                    :key="item.brandName"
                    :label="item.brandName"
                    :value="item.brandName"
                  ></el-option>
                </el-select>
              </el-form-item>
              <el-form-item>
                <el-input
                  class="input"
                  clearable
                  size="mini"
                  placeholder="请输入产品名称或Pid"
                  style="width: 180px"
                  prefix-icon="el-icon-search"
                  v-model="searchProduct.productName"
                ></el-input>
              </el-form-item>
              <el-form-item>
                <el-select
                  v-model="searchProduct.mainCategoryId"
                  @change="loadSecondCategorys"
                  size="mini"
                  filterable
                  clearable
                  style="width: 130px"
                  placeholder="请选择一级类目"
                >
                  <el-option
                    v-for="item in mainCategoryList"
                    :key="item.id"
                    :label="item.displayName"
                    :value="item.id"
                  ></el-option>
                </el-select>
              </el-form-item>
              <el-form-item>
                <el-select
                  v-model="searchProduct.secondCategoryId"
                  @change="loadChildCategorys"
                  size="mini"
                  filterable
                  clearable
                  style="width: 130px"
                  placeholder="请选择二级类目"
                >
                  <el-option
                    v-for="item in secondCategoryList"
                    :key="item.id"
                    :label="item.displayName"
                    :value="item.id"
                  ></el-option>
                </el-select>
              </el-form-item>
              <el-form-item>
                <el-select
                  v-model="searchProduct.childCategoryId"
                  size="mini"
                  filterable
                  clearable
                  style="width: 130px"
                  placeholder="请选择三级类目"
                >
                  <el-option
                    v-for="item in childCategoryList"
                    :key="item.id"
                    :label="item.displayName"
                    :value="item.id"
                  ></el-option>
                </el-select>
              </el-form-item>
            </template>
            <template v-slot:tb_cols>
              <el-table-column type="selection" :width="80" align="center">
              </el-table-column>
              <el-table-column
                label="产品Pid"
                prop="productCode"
                :width="130"
              ></el-table-column>
              <el-table-column label="产品名称" prop="name"></el-table-column>
              <el-table-column
                label="品牌"
                prop="brand"
                :width="100"
              ></el-table-column>
              <el-table-column
                label="单位"
                prop="unit"
                :width="100"
              ></el-table-column>
              <el-table-column label="上下架" :width="100">
                <template slot-scope="scope">
                  <el-tag size="mini" :type="scope.row.onSale === 1 ? '' : 'danger'">{{
                    scope.row.onSale === 1 ? "上架" : "下架"
                  }}</el-tag>
                </template>
              </el-table-column>
            </template>
          </rg-page>
        </el-form>
      </template>
      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          :loading="saveLoadingP"
          size="mini"
          type="success"
          @click="submitProduct()"
          >保存</el-button
        >
      </template>
    </rg-dialog>
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appVenderSvc } from "@/api/purchase/vender";
import { brandSearchSvc } from "@/api/product/brandsearch";
import { categorySearchSvc } from "@/api/product/categorysearch";
export default {
  name: "venderproduct",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      condition: {
        pageIndex: 1,
        pageSize: 20,
        venderIdS: "",
        venderId: 0,
        productName: "",
        stockStatus: "",
      },
      tableData: [],
      venderSel: [],
      stockStatusEnum: [
        { id: "1", displayName: "正常" },
        { id: "2", displayName: "缺货" },
      ],
      formTitle: "新增产品",
      dialogVisible: false,
      addVenderProduct: {
        venderId: undefined,
        productCode: [],
      },
      brandList: [],
      tableLoading1: false,
      currentPage1: 1,
      totalList1: 0,
      tableData1: [],
      searchProduct: {
        brand: "",
        productName: "",
        venderId: 0,
        mainCategoryId: undefined,
        secondCategoryId: undefined,
        childCategoryId: undefined,
        pageIndex: 1,
        pageSize: 20,
      },
      mainCategoryList: [],
      secondCategoryList: [],
      childCategoryList: [],
      multipleSelection: [],
      saveLoadingP: false,
    };
  },
  created() {
    this.fetchData();
    this.getVenders();
    this.getBrandList();
    this.loadMainCategorys();
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
      if (this.condition.venderIdS == "") {
        this.condition.venderId = 0;
      } else {
        this.condition.venderId = Number(this.condition.venderIdS);
      }
      appVenderSvc
        .getVenderProduct(this.condition)
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
    getVenders() {
      appVenderSvc
        .getVenders()
        .then(
          (res) => {
            this.venderSel = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    getBrandList() {
      brandSearchSvc
        .getBrandList()
        .then(
          (res) => {
            var data = res.data;
            this.brandList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    add() {
      this.addVenderProduct = {
        venderId: undefined,
        productCode: [],
      };
      this.searchProduct = {
        brand: "",
        productName: "",
        venderId: 0,
        mainCategoryId: undefined,
        secondCategoryId: undefined,
        childCategoryId: undefined,
        pageIndex: 1,
        pageSize: 20,
      };
      this.secondCategoryList = [];
      this.childCategoryList = [];
      this.multipleSelection = [];
      this.tableData1 = [];
      this.currentPage1 = 1;
      this.totalList1 = 0;
      this.dialogVisible = true;
    },
    cancel() {
      this.dialogVisible = false;
    },
    loadMainCategorys() {
      categorySearchSvc
        .getCategorysById({
          categoryId: 0,
          level: 1,
        })
        .then(
          (res) => {
            var data = res.data;
            this.mainCategoryList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    loadSecondCategorys() {
      this.searchProduct.secondCategoryId = undefined;
      this.searchProduct.childCategoryId = undefined;
      if (!this.searchProduct.mainCategoryId) {
        this.secondCategoryList = [];
        this.childCategoryList = [];
        return;
      }

      this.childCategoryList = [];
      categorySearchSvc
        .getCategorysById({
          categoryId: this.searchProduct.mainCategoryId,
          level: 2,
        })
        .then(
          (res) => {
            var data = res.data;
            this.secondCategoryList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    loadChildCategorys() {
      this.searchProduct.childCategoryId = undefined;
      if (!this.searchProduct.secondCategoryId) {
        this.childCategoryList = [];
        return;
      }
      categorySearchSvc
        .getCategorysById({
          categoryId: this.searchProduct.secondCategoryId,
          level: 3,
        })
        .then(
          (res) => {
            var data = res.data;
            this.childCategoryList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    search1(flag) {
      debugger;
      this.searchProduct.pageIndex = this.currentPage1 = 1;
      this.getPaged1(flag);
    },
    pageChange1(p) {
      this.searchProduct.pageIndex = p.pageIndex;
      this.searchProduct.pageSize = p.pageSize;
      this.getPaged1();
    },
    getPaged1(flag) {
      if (!this.addVenderProduct.venderId) {
        this.$message({ message: "请选择供应商", type: "warning" });
        return;
      }
      if (
        this.searchProduct.productName == "" &&
        this.searchProduct.brand == "" &&
        !this.searchProduct.mainCategoryId &&
        !this.searchProduct.secondCategoryId &&
        !this.searchProduct.childCategoryId
      ) {
        this.$message({ message: "请输入查询条件", type: "warning" });
        return;
      }
      this.searchProduct.venderId = this.addVenderProduct.venderId;
      this.tableLoading1 = true;
      appVenderSvc
        .searchProductForVender(this.searchProduct)
        .then(
          (res) => {
            var data = res.data;
            this.tableData1 = data.items;
            this.totalList1 = data.totalItems;
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
          this.tableLoading1 = false;
        });
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    submitProduct() {
      if (!this.addVenderProduct.venderId) {
        this.$message({ message: "请选择供应商", type: "warning" });
        return;
      }
      if (this.multipleSelection.length == 0) {
        this.$message({ message: "请选择产品", type: "warning" });
        return;
      }
      var products = new Array();
      for (var i = 0; i < this.multipleSelection.length; i++) {
        var itemProduct = {
          pid: this.multipleSelection[i].productCode,
          name: this.multipleSelection[i].name,
          brand: this.multipleSelection[i].brand,
        };
        products.push(itemProduct);
      }
      this.addVenderProduct.productCode = products;
      this.saveLoadingP = true;
      appVenderSvc
        .addVenderProducts(this.addVenderProduct)
        .then(
          (res) => {
            var data = res.data;
            if (data) {
              this.$message({ message: "保存成功", type: "success" });
              this.dialogVisible = false;
              this.getPaged();
            } else {
              this.$message({ message: res.message, type: "error" });
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.saveLoadingP = false;
        });
    },
    updateStatus(venderProductId) {
      this.$confirm("确定要删除吗?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        closeOnClickModal: false,
        type: "warning",
      })
        .then(() => {
          var deleteRequest = {
            venderProductId: venderProductId,
          };
          appVenderSvc
            .deleteVenderProduct(deleteRequest)
            .then(
              (res) => {
                var data = res.data;
                if (data) {
                  this.$message({ message: "操作成功", type: "success" });
                } else {
                  this.$message({ message: res.message, type: "error" });
                }
                this.getPaged();
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally(() => {});
        })
        .catch(() => {});
    },
  },
};
</script>

<style lang="scss" scoped>
/deep/ #productPage .el-table--fit {
  height: calc(100vh - 412px) !important; //567px
}
</style>