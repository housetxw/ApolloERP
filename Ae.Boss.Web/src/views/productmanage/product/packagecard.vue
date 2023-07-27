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
          <el-input
            v-model="condition.key"
            clearable
            placeholder="请输入套餐卡名称或Pid"
            @keyup.enter.native="search(true)"
          />
        </el-form-item>
        <el-form-item>
          <el-select v-model="condition.type" multiple placeholder="套餐卡类型">
            <el-option
              v-for="dict in packageTypeEnum"
              :key="dict.id"
              :label="dict.displayName"
              :value="dict.id"
            />
          </el-select>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
          >新增</el-button
        >
      </template>
      <template v-slot:tb_cols>
        <el-table-column
          label="套餐卡Pid"
          prop="productCode"
          :width="170"
        ></el-table-column>
        <el-table-column label="套餐卡名称" prop="name"></el-table-column>
        <el-table-column label="图片" align="center" :width="80">
          <template slot-scope="scope">
            <el-image
              v-show="scope.row.image1 != ''"
              @click="routeImageDetail(scope.row.image1)"
              style="width: 25px; height: 25px"
              :src="scope.row.image1"
            ></el-image>
          </template>
        </el-table-column>
        <el-table-column
          label="销售价"
          prop="salesPrice"
          :width="80"
          align="right"
        ></el-table-column>
        <el-table-column align="center" label="是否上架" :width="80">
          <template slot-scope="scope">
            <el-tag
              size="mini"
              :type="scope.row.onSale === 1 ? '' : 'danger'"
              >{{ scope.row.onSale === 1 ? "上架" : "下架" }}</el-tag
            >
          </template>
        </el-table-column>
        <el-table-column
          align="center"
          label="排序"
          prop="rank"
          :width="60"
        ></el-table-column>
        <el-table-column
          align="center"
          label="类型"
          prop="typeDisplay"
          :width="80"
        ></el-table-column>
        <el-table-column align="center" label="操作" :width="90">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                @click="routeDetail(scope.row)"
                type="primary"
                size="mini"
                >编辑</el-button
              >
              <el-button
                @click="deletePackageCard(scope.row.packageCardId)"
                type="danger"
                size="mini"
                >删除</el-button
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
        click: (done) => {
          cancel();
        },
      }"
      :btnRemove="false"
      :footbar="true"
      width="80%"
      maxWidth="600px"
      minWidth="600px"
    >
      <template v-slot:content>
        <el-form :model="detail" size="mini" label-width="120px">
          <el-form-item label="套餐卡Pid">
            <el-input
              style="width: 300px"
              :readonly="true"
              v-model="detail.productCode"
            ></el-input>
          </el-form-item>
          <el-form-item label="套餐卡名称">
            <el-input
              style="width: 300px"
              :readonly="true"
              v-model="detail.name"
            ></el-input>
          </el-form-item>
          <el-form-item label="图片">
            <el-image
              v-show="detail.image1 != ''"
              @click="routeImageDetail(detail.image1)"
              style="width: 25px; height: 25px"
              :src="detail.image1"
            ></el-image>
          </el-form-item>
          <el-form-item label="销售价">
            <el-input
              style="width: 300px"
              :readonly="true"
              v-model="detail.salesPrice"
            ></el-input>
          </el-form-item>
          <el-form-item label="上下架">
            <el-tag size="mini" :type="detail.onSale === 1 ? '' : 'danger'">{{
              detail.onSale === 1 ? "上架" : "下架"
            }}</el-tag>
          </el-form-item>
          <el-form-item label="类型">
            <el-select
              style="width: 120px"
              v-model="detail.type"
              placeholder="套餐卡类型"
            >
              <el-option
                v-for="dict in packageTypeEnum"
                :key="dict.id"
                :label="dict.displayName"
                :value="dict.id"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="排序">
            <el-input style="width: 300px" v-model="detail.rank"></el-input>
          </el-form-item>
        </el-form>
      </template>

      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          :loading="editLoading"
          size="mini"
          type="success"
          @click="editPackageCard()"
          >提交</el-button
        >
      </template>
    </rg-dialog>

    <rg-dialog
      :title="formTitle1"
      :visible.sync="dialogVisible1"
      :beforeClose="cancel1"
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          cancel1();
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
        <el-form :model="productRequest" size="mini" label-width="120px">
          <rg-page
            id="productPage"
            :pageIndex="productRequest.pageIndex"
            :pageSize="productRequest.pageSize"
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
                <el-input
                  class="input"
                  clearable
                  size="mini"
                  placeholder="请输套餐卡名称或Pid"
                  style="width: 180px"
                  prefix-icon="el-icon-search"
                  v-model="productRequest.productName"
                ></el-input>
              </el-form-item>
              <el-form-item>
                <el-select
                  v-model="productRequest.brand"
                  size="mini"
                  filterable
                  clearable
                  style="width: 120px"
                  placeholder="请选择品牌"
                >
                  <el-option
                    v-for="dict in brand"
                    :key="dict.brandName"
                    :label="dict.brandName"
                    :value="dict.brandName"
                  />
                </el-select>
              </el-form-item>
              <el-form-item>
                <el-select
                  v-model="productRequest.mainCategoryId"
                  @change="loadSecondCategorys"
                  placeholder="一级类目"
                  filterable
                  clearable
                  style="width: 130px"
                >
                  <el-option
                    v-for="dict in mainCategoryList"
                    :key="dict.id"
                    :label="dict.displayName"
                    :value="dict.id"
                  ></el-option>
                </el-select>
              </el-form-item>
              <el-form-item>
                <el-select
                  v-model="productRequest.secondCategoryId"
                  @change="loadChildCategorys"
                  placeholder="二级类目"
                  filterable
                  clearable
                  style="width: 130px"
                >
                  <el-option
                    v-for="dict in secondCategoryList"
                    :key="dict.id"
                    :label="dict.displayName"
                    :value="dict.id"
                  />
                </el-select>
              </el-form-item>
              <el-form-item>
                <el-select
                  v-model="productRequest.childCategoryId"
                  filterable
                  placeholder="三级类目"
                  clearable
                  style="width: 130px"
                >
                  <el-option
                    v-for="dict in childCategoryList"
                    :key="dict.id"
                    :label="dict.displayName"
                    :value="dict.id"
                  />
                </el-select>
              </el-form-item>
            </template>
            <template v-slot:tb_cols>
              <el-table-column
                type="selection"
                :selectable="selectable"
                :width="60"
                align="center"
              ></el-table-column>
              <el-table-column label="排序" :width="60">
                <template slot-scope="scope">
                  <el-input
                    :readonly="scope.row.packageCardId > 0"
                    size="mini"
                    v-model="scope.row.rank"
                  ></el-input>
                </template>
              </el-table-column>
              <el-table-column label="类型" :width="90">
                <template slot-scope="scope">
                  <el-select
                    :disabled="scope.row.packageCardId > 0"
                    v-model="scope.row.type"
                    size="mini"
                    placeholder="-套餐卡类型-"
                  >
                    <el-option
                      v-for="dict in packageTypeEnum"
                      :key="dict.id"
                      :label="dict.displayName"
                      :value="dict.id"
                    />
                  </el-select>
                </template>
              </el-table-column>
              <el-table-column
                label="产品Pid"
                prop="productCode"
                :width="150"
              ></el-table-column>
              <el-table-column label="产品名称" prop="name"></el-table-column>
              <el-table-column
                label="售价"
                align="right"
                prop="salesPrice"
                :width="60"
              ></el-table-column>
              <el-table-column label="上下架" align="center" :width="80">
                <template slot-scope="scope">
                  <el-tag
                    size="mini"
                    :type="scope.row.onSale === 1 ? '' : 'danger'"
                    >{{ scope.row.onSale === 1 ? "上架" : "下架" }}</el-tag
                  >
                </template>
              </el-table-column>
            </template>
          </rg-page>
        </el-form>
      </template>
      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          :loading="addLoadingP"
          size="mini"
          type="success"
          @click="addHotProduct()"
          >添加</el-button
        >
      </template>
    </rg-dialog>
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { productSearchSvc } from "@/api/product/productsearch";
import { brandSearchSvc } from "@/api/product/brandsearch";
import { categorySearchSvc } from "@/api/product/categorysearch";
export default {
  name: "packagecard",
  data() {
    return {
      tableLoading: false,
      mainCategoryList: [],
      secondCategoryList: [],
      childCategoryList: [],
      condition: {
        key: "",
        type: [],
        pageIndex: 1,
        pageSize: 20,
      },
      tableData: [],
      currentPage: 1,
      totalList: 0,
      brand: [],
      packageTypeEnum: [],
      formTitle: "详情",
      dialogVisible: false,
      detail: {
        packageCardId: 0,
        rank: 0,
        type: 0,
        typeDisplay: "",
        productCode: "",
        name: "",
        brand: "",
        image1: "",
        salesPrice: 0,
        onSale: 0,
      },
      editLoading: false,
      formTitle1: "新增",
      dialogVisible1: false,
      tableLoading1: false,
      tableData1: [],
      currentPage1: 1,
      totalList1: 0,
      productRequest: {
        pageIndex: 1,
        pageSize: 20,
        productName: "",
        brand: "",
        mainCategoryId: undefined,
        secondCategoryId: undefined,
        childCategoryId: undefined,
      },
      multipleSelection: [],
      addLoadingP: false,
    };
  },
  created() {
    this.fetchData();
    this.getBrandList();
    this.getPackageTypeEnum();
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
      productSearchSvc
        .getPackageCardProductPageList(this.condition)
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
    getBrandList() {
      brandSearchSvc
        .getBrandList()
        .then(
          (res) => {
            var data = res.data;
            this.brand = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getPackageTypeEnum() {
      productSearchSvc
        .getPackageCardTypeEnum()
        .then(
          (res) => {
            var data = res.data;
            this.packageTypeEnum = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    //查询主分类
    loadMainCategorys: function () {
      categorySearchSvc
        .getCategorysById({
          categoryId: 0,
          level: 1,
        })
        .then(
          (res) => {
            var data = res.data;
            this.mainCategoryList = data;
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
        .finally(() => {});
    },
    //查询二级分类
    loadSecondCategorys: function (categoryId) {
      this.productRequest.secondCategoryId = undefined;
      this.productRequest.childCategoryId = undefined;
      if (!categoryId) {
        return;
      }
      categorySearchSvc
        .getCategorysById({
          categoryId: categoryId,
          level: 2,
        })
        .then(
          (res) => {
            var data = res.data;
            this.secondCategoryList = data;
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
        .finally(() => {});
    },
    //查询三级分类
    loadChildCategorys: function (categoryId) {
      this.productRequest.childCategoryId = undefined;
      if (!categoryId) {
        return;
      }
      categorySearchSvc
        .getCategorysById({
          categoryId: categoryId,
          level: 3,
        })
        .then(
          (res) => {
            var data = res.data;
            this.childCategoryList = data;
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
        .finally(() => {});
    },
    routeImageDetail(url) {
      window.open(url);
    },
    add() {
      this.productRequest = {
        pageIndex: 1,
        pageSize: 20,
        productName: "",
        brand: "",
        mainCategoryId: undefined,
        secondCategoryId: undefined,
        childCategoryId: undefined,
      };
      this.multipleSelection = [];
      this.tableData1 = [];
      this.currentPage1 = 1;
      this.totalList1 = 0;
      this.dialogVisible1 = true;
    },
    cancel() {
      this.dialogVisible = false;
    },
    routeDetail(rowP) {
      Object.assign(this.detail, rowP);
      this.dialogVisible = true;
    },
    deletePackageCard(packageCardId) {
      this.$confirm("确定要删除吗?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        closeOnClickModal: false,
        type: "warning",
      })
        .then(() => {
          var deleteRequest = {
            id: packageCardId,
            isDeleted: 1,
          };
          productSearchSvc
            .editPackageCardProduct(deleteRequest)
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
    editPackageCard() {
      var rank = this.detail.rank;
      var r = /^-?\d+$/; //正整数
      if (!r.test(rank)) {
        this.$message({ message: "排序输入有误，请输入整数", type: "warning" });
        return;
      }
      this.editLoading = true;
      var editRequest = {
        id: this.detail.packageCardId,
        type: this.detail.type,
        rank: this.detail.rank,
      };

      productSearchSvc
        .editPackageCardProduct(editRequest)
        .then(
          (res) => {
            var data = res.data;
            if (data) {
              this.$message({ message: "操作成功", type: "success" });
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
          this.editLoading = false;
        });
    },
    cancel1() {
      this.dialogVisible1 = false;
    },
    search1(flag) {
      this.productRequest.pageIndex = this.currentPage1 = 1;
      this.getPaged1(flag);
    },
    pageChange1(p) {
      this.productRequest.pageIndex = p.pageIndex;
      this.productRequest.pageSize = p.pageSize;
      this.getPaged1();
    },
    getPaged1(flag) {
      this.tableLoading1 = true;
      productSearchSvc
        .searchProductForPackageCard(this.productRequest)
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
    selectable(row, index) {
      if (row.packageCardId == 0) {
        return true;
      } else {
        return false;
      }
    },
    addHotProduct() {
      if (this.multipleSelection.length == 0) {
        this.$message({ message: "请选择产品", type: "warning" });
        return;
      }
      var addRequest = {
        products: [],
      };
      for (var i = 0; i < this.multipleSelection.length; i++) {
        var rank = this.multipleSelection[i].rank;
        var r = /^-?\d+$/; //正整数
        if (!r.test(rank)) {
          this.$message({
            message: "第" + (i + 1) + "行商品排序输入有误，请输入整数",
            type: "warning",
          });
          return;
        }
        var itemProduct = {
          pid: this.multipleSelection[i].productCode,
          rank: this.multipleSelection[i].rank,
          type: this.multipleSelection[i].type,
        };
        addRequest.products.push(itemProduct);
      }
      this.addLoadingP = true;
      productSearchSvc
        .addPackageCardProduct(addRequest)
        .then(
          (res) => {
            var data = res.data;
            if (data) {
              this.$message({ message: "保存成功", type: "success" });
              this.dialogVisible1 = false;
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
          this.addLoadingP = false;
        });
    },
  },
};
</script>

<style lang="scss" scoped>
/deep/ #productPage .el-table--fit {
  height: calc(100vh - 334px) !important; //567px
}
.el-image {
  display: inline-table;
}
</style>