<template>
  <div class="container">
      <rg-page
      background
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalCount"
      :tableLoading="tableLoading"
      :tableData="tableData"
      :pageChange="pageTurning"
      :headerBtnWidth="180"
      :searching="search"
      :defaultCollapse="false"
      @expand-change="loadChildTableData"
      :getRowKeys="getRowKeys"
      :expands="expands"
      rel="mainTable"
      :highlight-current-row="true"
            :tbRowClick="handleSelectChange"

    >
      <template v-slot:condition>
          <el-form-item>
            <el-input
              v-model="condition.key"
              autocomplete="off"
              placeholder="请输入编码或名称"
              @keyup.enter.native="search(true)"
            />
          </el-form-item>
          <el-form-item >
            <el-select
              v-model="condition.brand"
              placeholder="产品品牌"
              clearable
              filterable
              style="width:120px"
            >
              <el-option
                v-for="dict in brand"
                :key="dict.brandName"
                :label="dict.brandName"
                :value="dict.brandName"
              />
            </el-select>
          </el-form-item>
          <el-form-item >
            <el-select
              v-model="condition.productAttribute"
              placeholder="产品性质"
              clearable
              style="width:120px"
            >
              <el-option
                v-for="dict in productattributeList"
                :key="dict.value"
                :label="dict.label"
                :value="dict.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item >
            <el-select
              v-model="condition.mainCategoryId"
              @change="loadSecondCategorys"
              placeholder="一级类目"
              filterable
              clearable
              style="width:130px"
            >
              <el-option
                v-for="dict in mainCategoryList"
                :key="dict.id"
                :label="dict.displayName"
                :value="dict.id"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item >
            <el-select
              v-model="condition.secondCategoryId"
              @change="loadChildCategorys"
              placeholder="二级类目"
              filterable
              clearable
              style="width:130px"
            >
              <el-option
                v-for="dict in secondCategoryList"
                :key="dict.id"
                :label="dict.displayName"
                :value="dict.id"
              />
            </el-select>
          </el-form-item>
          <el-form-item >
            <el-select
              v-model="condition.childCategoryId"
              filterable
              placeholder="三级类目"
              clearable
              style="width:130px"
            >
              <el-option
                v-for="dict in childCategoryList"
                :key="dict.id"
                :label="dict.displayName"
                :value="dict.id"
              />
            </el-select>
          </el-form-item> 
          <el-form-item>
            <el-input class="input" clearable placeholder="输入配件编码" style="width:120px" v-model="condition.partCode" />
          </el-form-item>
          <el-form-item>
            <el-input class="input" clearable placeholder="输入配件编号" style="width:120px" v-model="condition.partNo" />
          </el-form-item>
          <el-form-item>
            <el-select v-model="condition.isOnSale" style="width:100px" clearable placeholder="是否上架">
              <el-option
                v-for="item in onSaleEnum"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select v-model="condition.isRetail" style="width:100px" clearable placeholder="是否零售">
              <el-option 
                v-for="item in retailEnum"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select v-model="condition.isDeleted" style="width:100px" clearable placeholder="是否禁用">
              <el-option
                v-for="item in deleteEnum"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          
          <el-form-item>
            <el-upload
              style="display:inline"
              ref="uploadProduct"
              :action="importProductUrl"
              :limit="1"
              :show-file-list="false"
              :http-request="uploadProductFile"
              accept=".xls, .xlsx"
            >
              <el-button size="mini" type="primary" icon="el-icon-edit">导入商品</el-button>
            </el-upload>
            <el-upload
              style="display:inline"
              ref="uploadProductAttribute"
              :action="importProductAttributeUrl"
              :limit="1"
              :show-file-list="false"
              :http-request="uploadProductAttributeFile"
              accept=".xls, .xlsx"
            >
              <el-button size="mini" type="primary" icon="el-icon-edit">导入商品属性</el-button>
            </el-upload>
          </el-form-item>

          <el-form-item>
            <el-button type="primary" size="mini" @click="download" icon="el-icon-download">下载模板</el-button>
          </el-form-item>

          <el-form-item>
            <el-button
              type="primary"
              size="mini"
              @click="copyParentProduct()"
              icon="el-icon-document-copy"
            >复制父产品</el-button>
          </el-form-item>
   

      </template>
         <template v-slot:header_btn>
            
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="toProductedit">新增父产品</el-button>
      </template>
    <!-- <el-table
      v-loading="tableLoading"
      :data="tableData"
      border
      style="width: 100%"
      @expand-change="loadChildTableData"
      :row-key="getRowKeys"
      :expand-row-keys="expands"
      rel="mainTable"
      :highlight-current-row="true"
      @current-change="handleSelectChange"
      size="small"
    > -->
      <template v-slot:tb_cols>

      <!-- <el-table-column type="index" width="50"></el-table-column> -->
      <el-table-column type="expand">
        <template slot-scope="props">
          <el-table v-loading="tableLoading" :data="childTableData" border style="width: 100%" size="small">
            <el-table-column type="index" width="50"></el-table-column>
            <el-table-column label="子产品编码" prop="productCode" />
            <el-table-column label="子产品名称" prop="name" />
            <el-table-column label="品牌" prop="brand" width="120" />
            <el-table-column label="销售价" prop="salesPrice" width="100" />
            <el-table-column label="批发价" prop="wholesalePrice" width="100" />
            <el-table-column label="门店结算价" prop="settlementPrice" width="100" />
            <el-table-column label="上下架" prop="onSale" width="100">
              <template slot-scope="scope">
                <el-tag
                  :type="scope.row.onSale === 0 ? '' : 'danger'"
                >{{scope.row.onSale === 1 ? '是' : '否'}}</el-tag>
              </template>
            </el-table-column>
            <el-table-column label="是否禁用" prop="isDeleted" width="100" align="center">
              <template slot-scope="scope">
                <el-tag
                  :type="scope.row.isDeleted === 0 ? '' : 'danger'"
                >{{scope.row.isDeleted === 1 ? '是' : '否'}}</el-tag>
              </template>
            </el-table-column>
            <el-table-column label="操作" width="200" align="center">
              <template slot-scope="scope">
                <router-link :to="'/product/productedit/'+scope.row.productCode+'/'+2+'/'+1">
                  <el-button size="small" type="primary" icon="el-icon-edit">编辑</el-button>
                </router-link>
              </template>
            </el-table-column>
          </el-table>
          <div class="pagination">
            <el-pagination
              background
              :page-size="10"
              layout="prev, pager, next"
              :hide-on-single-page="childTotalCount<10"
              :total="childTotalCount"
              @current-change="handleChildCurrentChange"
              :current-page.sync="childCondition.pageIndex"
            />
          </div>
        </template>
      </el-table-column>
      <el-table-column label="一级类目" prop="mainCategoryName" />
      <el-table-column label="二级类目" prop="secondCategoryName" />
      <el-table-column label="三级类目" prop="childCategoryName" />
      <el-table-column label="父产品编码" prop="productCode" />
      <el-table-column label="父产品名称" prop="name" width="450" />
      <el-table-column label="品牌" prop="brand" />
      <el-table-column label="是否上架" width="100" align="center">
        <template slot-scope="scope">
          <el-tag
            :type="scope.row.onSale === 0 ? '' : 'danger'"
          >{{scope.row.onSale === 1 ? '是' : '否'}}</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="是否禁用" width="100" align="center">
        <template slot-scope="scope">
          <el-tag
            :type="scope.row.isDeleted === 0 ? '' : 'danger'"
          >{{scope.row.isDeleted === 1 ? '是' : '否'}}</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="220" align="center" fixed="right">
        <template slot-scope="scope">
          <router-link :to="'/product/productedit/'+scope.row.productCode+'/'+4+'/'+1">
            <el-button type="primary" size="small" icon="el-icon-edit">编辑</el-button>
          </router-link>
          <router-link :to="'/product/productedit/'+scope.row.productCode+'/'+2+'/'+0">
            <el-button type="primary" size="small" icon="el-icon-plus">新增子产品</el-button>
          </router-link>
        </template>
      </el-table-column>
      </template>
    <!-- <div class="pagination">
      <el-pagination
        background
        :page-sizes="[10,20, 40, 60, 80]"
        :page-size="10"
        layout="total, sizes, prev, pager, next, jumper"
        :total="totalCount"
        @size-change="handleSizeChange"
        :current-page.sync="condition.pageIndex"
        @current-change="handleCurrentChange"
      />
    </div> -->
      </rg-page>
    <el-dialog :title="'提示'" hei width="800" :visible.sync="dialogFormVisible">
      <div style="width:100%; height:500">
        <el-table :data="productErrorData" border>
          <el-table-column label="商品名称" prop="name" />
          <el-table-column label="异常原因" prop="errorMessage" />
        </el-table>
      </div>
    </el-dialog>

    <el-dialog :title="'提示'" hei width="800" :visible.sync="dialogFormVisible">
      <div style="width:100%; height:500">
        <el-table :data="productErrorData" border>
          <el-table-column label="配件编码" prop="partCode" />
          <el-table-column label="异常原因" prop="errorMessage" />
        </el-table>
      </div>
    </el-dialog>
  </div>
</template>

<script>

import { upload } from "@/utils/uploadfile";
import { appSvc } from "@/api/qiniu/qiniuservice";
import { productSearchSvc } from "@/api/product/productsearch";
import { categorySearchSvc } from "@/api/product/categorysearch";
import { brandSearchSvc } from "@/api/product/brandsearch";
import { getToken } from "@/utils/auth";
export default {
  name: "productsearch",
  data() {
    return {
      expands: [],
      getRowKeys(row) {
        return row.id;
      },
      tableLoading: false,
      mainCategoryList: [],
      secondCategoryList: [],
      childCategoryList: [],
      condition: {
        key: "",
        isDeleted: 0,
        isOnSale: undefined,
        pageIndex: 1,
        pageSize: 10,
        classType: 4,
        brand:'',
        partCode:'',
        partNo:'',
        isRetail:undefined,
        mainCategoryId: undefined,
        secondCategoryId: undefined,
        childCategoryId: undefined
      },
      childCondition: {
        parentProductId: 0,
        isDeleted: 0, //之产品只查询启用的
        isOnSale: undefined,
        pageIndex: 1,
        pageSize: 10,
        classType: 2
      },
      tableData: [],
      childTableData: [],
      totalCount: 0,
      childTotalCount: 0,
      onSaleEnum:[
        {
          value: 1,
          label: "上架"
        },
        {
          value: 0,
          label: "下架"
        }
      ],
      deleteEnum:[
        {
          value: 0,
          label: "未禁用"
        },
        {
          value: 1,
          label: "禁用"
        }
      ],
      retailEnum:[
        {
          value: 1,
          label: "零售"
        },
        {
          value: 0,
          label: "非零售"
        }
      ],
      dialogFormVisible: false,
      productattributeList: [
        // {
        //   value: "",
        //   label: "全部"
        // },
        {
          value: 0,
          label: "实物产品"
        },
        {
          value: 1,
          label: "套餐产品"
        },
        {
          value: 2,
          label: "服务产品"
        },
        {
          value: 3,
          label: "数字产品"
        }
      ],
      importProductUrl: "",
      importProductAttributeUrl: "",
      productErrorData: [],
      currentSelectRow: null,
      brand:[]
    };
  },
  created() {
    this.getPageData(false);
    this.loadMainCategorys();
    this.getBrandList();
  },
  methods: {
    toProductedit(){
      this.$router.push({path:"/product/productedit/0/4/0"})
    },
    getPageData(flag) {
      this.tableLoading = true;
      productSearchSvc
        .searchProduct(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalCount = data.totalItems;
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
    search(flag) {
      this.condition.pageIndex = 1;
      this.expands = [];
      this.currentSelectRow = null;
      this.getPageData(flag);
    },
    // 分页
    // handleSizeChange(val) {
    //   this.expands = [];
    //   this.currentSelectRow = null;
    //   this.condition.pageIndex = 1;
    //   this.condition.pageSize = val;
    //   this.getPageData(false);
    // },
      pageTurning(p) {
          this.expands = [];
             this.currentSelectRow = null;
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPageData(false);
    },
    handleCurrentChange(val) {
      this.expands = [];
      this.currentSelectRow = null;
      this.getPageData(false);
    },
    getBrandList(){
      brandSearchSvc.getBrandList().then(
          res => {
            var data = res.data;
            this.brand = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getChildPageData() {
      this.tableLoading = true;
      productSearchSvc
        .searchProduct(this.childCondition)
        .then(
          res => {
            var data = res.data;
            this.childTableData = data.items;
            this.childTotalCount = data.totalItems;
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
    loadChildTableData(row, expandedRows) {
      var that = this;
      if (expandedRows.length) {
        that.expands = [];
        if (row) {
          that.expands.push(row.id);
        }
      } else {
        that.expands = [];
      }
      if (expandedRows.length > 0) {
        this.childCondition.parentProductId = row.id;
        this.getChildPageData();
      }
    },
    handleChildCurrentChange(val) {
      this.getChildPageData();
    },
    //查询主分类
    loadMainCategorys: function() {
      categorySearchSvc
        .getCategorysById({
          categoryId: 0,
          level: 1
        })
        .then(
          res => {
            var data = res.data;
            this.mainCategoryList = data;
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
        .finally(() => {});
    },
    //查询二级分类
    loadSecondCategorys: function(categoryId) {
      if (!categoryId) {
        return;
      }
      this.condition.secondCategoryId = undefined;
      this.condition.childCategoryId = undefined;
      categorySearchSvc
        .getCategorysById({
          categoryId: categoryId,
          level: 2
        })
        .then(
          res => {
            var data = res.data;
            this.secondCategoryList = data;
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
        .finally(() => {});
    },
    //查询三级分类
    loadChildCategorys: function(categoryId) {
      if (!categoryId) {
        return;
      }
      this.condition.childCategoryId = undefined;
      categorySearchSvc
        .getCategorysById({
          categoryId: categoryId,
          level: 3
        })
        .then(
          res => {
            var data = res.data;
            this.childCategoryList = data;
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
        .finally(() => {});
    },
    // 直接下载，用户体验好
    download: function() {
      var filePath = "https://m.aerp.com.cn/productTemplate/商品导入模板.xlsx";
      window.open(filePath);
    },
    //导入商品信息
    uploadProductFile(request) {
      const loading = this.$loading({
        lock: true,
        text: "Loading",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)"
      });
      let formData = new FormData();
      formData.append("file", request.file);
      productSearchSvc.importProduct(formData).then(
        res => {
          loading.close();
          var reData = res.data;
          if (reData && reData.length > 0) {
            this.productErrorData = reData;
            this.dialogFormVisible = true;
          } else {
            this.$message.success("导入成功!");
          }
          this.$refs.uploadProduct.clearFiles();
        },
        err => {
          loading.close();
          this.$refs.uploadProduct.clearFiles();
        }
      );
    },
    //导入商品属性信息
    uploadProductAttributeFile(request) {
      const loading = this.$loading({
        lock: true,
        text: "Loading",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)"
      });
      let formData = new FormData();
      formData.append("file", request.file);
      productSearchSvc.importProductAttribute(formData).then(
        res => {
          loading.close();
          var data = res.data;
          if (data && data.length > 0) {
            this.productErrorData = data;
            this.dialogFormVisible = true;
          } else {
            this.$message.success("导入成功!");
          }
          this.$refs.uploadProductAttribute.clearFiles();
        },
        err => {
          loading.close();
          this.$refs.uploadProductAttribute.clearFiles();
        }
      );
    },
    //获取当前选中的行
    // handleSelectChange(currentRow, oldCurrentRow) {
    //   this.currentSelectRow = currentRow;
    //   console.log(this.currentSelectRow,'this.currentSelectRow')
    // },
        handleSelectChange(val) {
      this.currentSelectRow = val;
          console.log(this.currentSelectRow,'this.currentSelectRow')

    },
    //复制父产品
    copyParentProduct() {
      if (!this.currentSelectRow) {
        this.$message.warning("请选择要复制的父产品!");
        return;
      }
      var productCode = this.currentSelectRow.productCode;
      this.$router.push("/product/productedit/" + productCode + "/4/0");
    }
  }
};
</script>
<style lang="scss" scoped>
.container {
  margin: 25px 0px 0px 10px;
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
  .input-label {
    display: inline-block;
    width: 100px;
    margin-left: 40px;
  }
}
>>> .el-form--inline .el-form-item {
  padding-bottom: 10px;
}
</style>