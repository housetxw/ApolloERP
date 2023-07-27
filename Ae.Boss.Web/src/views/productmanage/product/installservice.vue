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
      :handleSelectionChange="handleSelectionChange"
    >
      <template v-slot:condition>
        <el-form-item>
          <el-input
            v-model="condition.key"
            clearable
            placeholder="请输入产品名称或Pid"
            @keyup.enter.native="search(true)"
          />
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.brand"
            placeholder="产品品牌"
            clearable
            filterable
            style="width: 120px"
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
            v-model="condition.mainCategoryId"
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
            v-model="condition.secondCategoryId"
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
            v-model="condition.childCategoryId"
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
        <el-form-item>
          <el-select
            v-model="condition.hasInstallService"
            clearable
            placeholder="是否已配置"
            style="width: 120px"
          >
            <el-option
              v-for="dict in hasInstallServiceEnum"
              :key="dict.id"
              :label="dict.displayName"
              :value="dict.id"
            />
          </el-select>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
          >批量编辑</el-button
        >
        <el-button
          type="danger"
          size="mini"
          icon="el-icon-delete"
          @click="deleteData"
          >批量删除</el-button
        >
      </template>
      <template v-slot:tb_cols>
        <el-table-column
          :selectable="selectable"
          align="center"
          type="selection"
          width="55"
        ></el-table-column>
        <el-table-column
          label="产品Pid"
          prop="pid"
          :width="150"
        ></el-table-column>
        <el-table-column
          label="产品名称"
          prop="name"
          :width="300"
        ></el-table-column>
        <el-table-column
          label="品牌"
          prop="brand"
          :width="120"
        ></el-table-column>
        <el-table-column
          label="销售价"
          prop="price"
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
        <el-table-column label="安装服务">
          <template slot-scope="scope">
            <p
              style="margin-top: 0; margin-bottom: 0"
              v-for="item in scope.row.installService"
              :key="item.id"
            >
              {{ item.serviceId + " 【" + item.serviceName + "】" }}
              <el-tag
                size="mini"
                :type="item.freeInstall === 1 ? 'danger' : ''"
                >{{ item.freeInstall === 1 ? "包" : "不包" }}</el-tag
              >
            </p>
          </template>
        </el-table-column>
        <el-table-column align="center" label="操作" :width="90">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                @click="routeDetail(scope.row)"
                type="primary"
                size="mini"
                >编辑</el-button
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
      maxWidth="800px"
      minWidth="800px"
      height="600px"
    >
      <template v-slot:content>
        <el-form :model="detail" size="mini" label-width="100px">
          <el-form-item label="产品Pid">
            <el-input
              style="width: 300px"
              :readonly="true"
              v-model="detail.pid"
            ></el-input>
          </el-form-item>
          <el-form-item label="产品名称">
            <el-input
              style="width: 300px"
              :readonly="true"
              v-model="detail.name"
            ></el-input>
          </el-form-item>
          <el-form-item label="销售价">
            <el-input
              style="width: 300px"
              :readonly="true"
              v-model="detail.price"
            ></el-input>
          </el-form-item>
          <el-divider content-position="left">
            安装服务
            <el-button size="mini" type="primary" @click="searchService"
              >添加服务</el-button
            >
          </el-divider>
          <el-table
            :data="installServiceData"
            border
            size="mini"
            style="width: 100%"
          >
            <el-table-column
              label="服务pid"
              prop="serviceId"
              :width="170"
            ></el-table-column>
            <el-table-column
              label="服务名称"
              prop="serviceName"
            ></el-table-column>
            <el-table-column
              label="服务价格"
              prop="price"
              :width="80"
            ></el-table-column>
            <el-table-column label="包安装" :width="80">
              <template slot-scope="scope">
                <el-select
                  v-model="scope.row.freeInstall"
                  size="mini"
                  placeholder="是否包安装"
                >
                  <el-option
                    v-for="dict in freeInstallEnum"
                    :key="dict.id"
                    :label="dict.displayName"
                    :value="dict.id"
                  />
                </el-select>
              </template>
            </el-table-column>
            <el-table-column label="服务数量" :width="80">
              <template slot-scope="scope">
                <el-select
                  v-model="scope.row.changeNum"
                  size="mini"
                  placeholder="服务数量"
                >
                  <el-option
                    v-for="dict in changeNumEnum"
                    :key="dict.id"
                    :label="dict.displayName"
                    :value="dict.id"
                  />
                </el-select>
              </template>
            </el-table-column>
            <el-table-column label="操作" align="center" :width="60">
              <template slot-scope="scope">
                <el-button
                  size="mini"
                  type="danger"
                  @click="deleteInstallService(scope.$index, scope.row)"
                  >删除</el-button
                >
              </template>
            </el-table-column>
          </el-table>
        </el-form>
      </template>

      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          :loading="editLoading"
          size="mini"
          type="success"
          @click="editInstallService()"
          >提交</el-button
        >
      </template>
    </rg-dialog>

    <rg-dialog
      :title="formTitle2"
      :visible.sync="dialogVisible2"
      :beforeClose="
        () => {
          this.dialogVisible2 = false;
        }
      "
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          this.dialogVisible2 = false;
        },
      }"
      :btnRemove="false"
      :footbar="true"
      width="80%"
      maxWidth="800px"
      minWidth="800px"
      minHeight="500px"
      maxHeight="500px"
    >
      <template v-slot:content>
        <el-input
          style="width: 200px"
          prefix-icon="el-icon-search"
          clearable
          size="mini"
          placeholder="输入商品名称或Pid"
          v-model="searchPid1"
          @keydown.enter.native="queryProduct(this)"
        ></el-input>
        <el-button size="mini" type="primary" @click="queryProduct()"
          >搜索</el-button
        >
        <el-table
          size="mini"
          height="320"
          v-loading="tableLoading1"
          :data="productResult"
          border
          style="width: 100%; margin-top: 10px; overflow-y: auto"
        >
          <el-table-column label="操作" width="60px">
            <template slot-scope="scope">
              <el-link
                @click="chooseProduct(scope.row)"
                size="mini"
                type="primary"
                >选择</el-link
              >
            </template>
          </el-table-column>
          <el-table-column
            label="服务Pid"
            prop="productCode"
            width="170px"
          ></el-table-column>
          <el-table-column label="服务名称" prop="name"></el-table-column>
          <el-table-column
            label="售价"
            prop="salesPrice"
            align="right"
            width="120px"
          ></el-table-column>
          <el-table-column label="上下架状态" width="120px">
            <template slot-scope="scope">
              <el-tag size="mini" v-if="scope.row.onSale == 1"> 上架中 </el-tag>
              <el-tag
                size="mini"
                type="danger"
                v-else-if="scope.row.onSale == 0"
              >
                下架
              </el-tag>
            </template>
          </el-table-column>
        </el-table>
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
      width="80%"
      maxWidth="800px"
      minWidth="800px"
      height="600px"
    >
      <template v-slot:content>
        <el-form :model="detail1" size="mini" label-width="100px">
          <el-form-item label="产品Pid">
            <el-input
              style="width: 100%"
              rows="5"
              :readonly="true"
              v-model="detail1.pid"
              type="textarea"
            ></el-input>
          </el-form-item>

          <el-divider content-position="left">
            安装服务
            <el-button size="mini" type="primary" @click="searchService"
              >添加服务</el-button
            >
          </el-divider>
          <el-table
            :data="installServiceData"
            border
            size="mini"
            style="width: 100%"
          >
            <el-table-column
              label="服务pid"
              prop="serviceId"
              :width="170"
            ></el-table-column>
            <el-table-column
              label="服务名称"
              prop="serviceName"
            ></el-table-column>
            <el-table-column
              label="服务价格"
              prop="price"
              :width="80"
            ></el-table-column>
            <el-table-column label="包安装" :width="80">
              <template slot-scope="scope">
                <el-select
                  v-model="scope.row.freeInstall"
                  size="mini"
                  placeholder="是否包安装"
                >
                  <el-option
                    v-for="dict in freeInstallEnum"
                    :key="dict.id"
                    :label="dict.displayName"
                    :value="dict.id"
                  />
                </el-select>
              </template>
            </el-table-column>
            <el-table-column label="服务数量" :width="80">
              <template slot-scope="scope">
                <el-select
                  v-model="scope.row.changeNum"
                  size="mini"
                  placeholder="服务数量"
                >
                  <el-option
                    v-for="dict in changeNumEnum"
                    :key="dict.id"
                    :label="dict.displayName"
                    :value="dict.id"
                  />
                </el-select>
              </template>
            </el-table-column>
            <el-table-column label="操作" align="center" :width="60">
              <template slot-scope="scope">
                <el-button
                  size="mini"
                  type="danger"
                  @click="deleteInstallService(scope.$index, scope.row)"
                  >删除</el-button
                >
              </template>
            </el-table-column>
          </el-table>
        </el-form>
      </template>

      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          :loading="editLoading1"
          :disabled="installServiceData.length == 0"
          size="mini"
          type="success"
          @click="editInstallService1()"
          >提交</el-button
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
  name: "installservice",
  data() {
    return {
      tableLoading: false,
      mainCategoryList: [],
      secondCategoryList: [],
      childCategoryList: [],
      condition: {
        key: "",
        brand: "",
        pageIndex: 1,
        pageSize: 20,
        mainCategoryId: undefined,
        secondCategoryId: undefined,
        childCategoryId: undefined,
        hasInstallService: 1,
      },
      tableData: [],
      currentPage: 1,
      totalList: 0,
      brand: [],
      hasInstallServiceEnum: [
        { id: 0, displayName: "未配置" },
        { id: 1, displayName: "已配置" },
      ],
      formTitle: "编辑",
      dialogVisible: false,
      detail: {
        productId: 0,
        pid: "",
        name: "",
        brand: "",
        price: 0,
        onSale: 0,
      },
      detail1: {
        pid: "",
      },
      multipleSelection: [],
      freeInstallEnum: [
        { id: 0, displayName: "不包" },
        { id: 1, displayName: "包" },
      ],
      changeNumEnum: [
        { id: 0, displayName: "✖ 1" },
        { id: 1, displayName: "✖ n" },
      ],
      formTitle2: "安装服务",
      dialogVisible2: false,
      searchPid1: "",
      tableLoading1: false,
      productResult: [],
      editLoading: false,
      formTitle1: "批量操作",
      dialogVisible1: false,
      installServiceData: [],
      editLoading1: false,
    };
  },
  created() {
    this.fetchData();
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
      productSearchSvc
        .getProductInstallService(this.condition)
        .then(
          (res) => {
            var data = res.data;
            for (var i = 0; i < data.items.length; i++) {
              if (data.items[i].id == 0) {
                data.items[i].freeDoorFee = this.checkDoor ? 1 : 0;
              }
            }
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
      this.condition.secondCategoryId = undefined;
      this.condition.childCategoryId = undefined;
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
      this.condition.childCategoryId = undefined;
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
    selectable(row, index) {
      return true;
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    routeDetail(rowDetail) {
      var detailRequest = {
        pid: rowDetail.pid,
      };
      productSearchSvc
        .getProductInstallServiceDetail(detailRequest)
        .then(
          (res) => {
            var data = res.data;
            if (data != null) {
              Object.assign(this.detail, data);
              this.installServiceData = data.installService;
              this.searchPid1 = "";
              this.productResult = [];
              this.dialogVisible = true;
            } else {
              this.$message({
                message: "产品无效",
                type: "error",
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
    cancel() {
      this.dialogVisible = false;
    },
    cancel1() {
      this.dialogVisible1 = false;
    },
    queryProduct() {
      if (this.searchPid1 == "") {
        this.$message({
          message: "请输入服务名称",
          type: "warning",
        });
        return;
      }
      this.tableLoading1 = true;
      var searchProductRequest = {
        content: this.searchPid1,
      };
      productSearchSvc
        .searchInstallService(searchProductRequest)
        .then(
          (res) => {
            var data = res.data;
            this.productResult = data;
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
    chooseProduct(rowSe) {
      if (rowSe.onSale != 1) {
        this.$message({
          message: "服务未上架",
          type: "warning",
        });
        return;
      }
      var isExists = false;
      this.installServiceData.map(function (a) {
        if (a.serviceId == rowSe.productCode) {
          isExists = true;
        }
      });
      if (isExists) {
        this.$message({
          message: "当前服务已存在",
          type: "warning",
        });
        return;
      }

      this.installServiceData.push({
        id: 0,
        serviceId: rowSe.productCode,
        serviceName: rowSe.name,
        price: rowSe.salesPrice,
        freeInstall: 0,
        changeNum: 0,
      });
      this.dialogVisible2 = false;
    },
    searchService() {
      // this.searchPid1 = '';
      // this.productResult = [];
      this.dialogVisible2 = true;
    },
    //删除安装服务
    deleteInstallService: function (index, row) {
      this.installServiceData.splice(index, 1);
    },
    editInstallService() {
      var editRequest = {
        products: [],
        installService: [],
      };

      var product = {
        productId: this.detail.productId,
        pid: this.detail.pid,
      };

      editRequest.products.push(product);
      for (var i = 0; i < this.installServiceData.length; i++) {
        editRequest.installService.push(this.installServiceData[i]);
      }

      this.editLoading = true;

      productSearchSvc
        .editInstallService(editRequest)
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
    add() {
      if (this.multipleSelection.length == 0) {
        this.$message({ message: "请选择产品", type: "warning" });
        return;
      }
      var idsStr = this.multipleSelection
        .map(function (obj, index) {
          return obj.pid;
        })
        .join("；");
      this.detail1.pid = idsStr;
      this.installServiceData = [];
      this.searchPid1 = "";
      this.productResult = [];
      this.dialogVisible1 = true;
    },
    editInstallService1() {
      if (this.installServiceData.length == 0) {
        this.$message({
          message: "请添加安装服务",
          type: "warning",
        });
        return;
      }
      var editRequest = {
        products: [],
        installService: [],
      };
      for (var i = 0; i < this.multipleSelection.length; i++) {
        var product = {
          productId: this.multipleSelection[i].productId,
          pid: this.multipleSelection[i].pid,
        };

        editRequest.products.push(product);
      }

      for (var i = 0; i < this.installServiceData.length; i++) {
        editRequest.installService.push(this.installServiceData[i]);
      }

      this.editLoading1 = true;

      productSearchSvc
        .editInstallService(editRequest)
        .then(
          (res) => {
            var data = res.data;
            if (data) {
              this.$message({ message: "操作成功", type: "success" });
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
          this.editLoading1 = false;
        });
    },
    deleteData() {
      if (this.multipleSelection.length == 0) {
        this.$message({ message: "请选择产品", type: "warning" });
        return;
      }

      this.$confirm("确定删除已选产品的安装服务吗?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        closeOnClickModal: false,
        type: "warning",
      })
        .then(() => {
          var deleteRequest = {
            products: [],
            installService: [],
          };
          for (var i = 0; i < this.multipleSelection.length; i++) {
            var product = {
              productId: this.multipleSelection[i].productId,
              pid: this.multipleSelection[i].pid,
            };

            deleteRequest.products.push(product);
          }
          productSearchSvc
            .editInstallService(deleteRequest)
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