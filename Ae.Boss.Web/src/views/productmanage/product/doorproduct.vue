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
            v-model="condition.isDoorProduct"
            @change="changeProductType"
            placeholder="是否上门产品"
            style="width: 130px"
          >
            <el-option
              v-for="dict in isDoorProductEnum"
              :key="dict.id"
              :label="dict.displayName"
              :value="dict.id"
            />
          </el-select>

          <el-select
            v-show="condition.isDoorProduct"
            v-model="condition.freeDoorFee"
            clearable
            placeholder="是否包上门"
            style="width: 110px"
          >
            <el-option
              v-for="dict in freeDooeFeeEnum1"
              :key="dict.id"
              :label="dict.displayName"
              :value="dict.id"
            />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.onSale"
            clearable
            placeholder="是否上架"
            style="width: 100px"
          >
            <el-option
              v-for="dict in isOnSaleEnum"
              :key="dict.id"
              :label="dict.displayName"
              :value="dict.id"
            />
          </el-select>
        </el-form-item>
      </template>

      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
          >批量新增</el-button
        >
        <el-checkbox
          style="margin-left: 5px"
          v-model="checkDoor"
          @change="changeDoor"
          border
          size="mini"
          >包上门费</el-checkbox
        >
      </template>

      <template v-slot:tb_cols>
        <el-table-column
          :selectable="selectable"
          align="center"
          type="selection"
          width="55"
        >
        </el-table-column>
        <el-table-column label="是否上门" width="70" align="center">
          <template slot-scope="scope">
            <el-tag size="mini" :type="scope.row.id === 0 ? 'danger' : ''">{{
              scope.row.id === 0 ? "非上门" : "上门"
            }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column
          label="产品Pid"
          prop="pid"
          :width="150"
        ></el-table-column>
        <el-table-column label="产品名称" prop="name"></el-table-column>
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
        <el-table-column align="center" label="包上门费" :width="80">
          <template slot-scope="scope">
            <el-tag
              v-show="scope.row.id > 0"
              size="mini"
              :type="scope.row.freeDoorFee === 0 ? '' : 'danger'"
              >{{ scope.row.freeDoorFee === 0 ? "不包" : "包" }}</el-tag
            >
            <el-select
              size="mini"
              v-show="scope.row.id == 0"
              v-model="scope.row.freeDoorFee"
              placeholder="是否包上门"
            >
              <el-option
                v-for="dict in freeDooeFeeEnum"
                :key="dict.id"
                :label="dict.displayName"
                :value="dict.id"
              />
            </el-select>
          </template>
        </el-table-column>
        <el-table-column align="center" label="操作" :width="90">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                v-show="scope.row.id > 0"
                @click="routeDetail(scope.row)"
                type="primary"
                size="mini"
                >编辑</el-button
              >
              <el-button
                v-show="scope.row.id > 0"
                @click="deleteDoorProduct(scope.row.id)"
                type="danger"
                size="mini"
                >删除</el-button
              >
              <el-button
                v-show="scope.row.id == 0"
                @click="addOne(scope.row)"
                type="success"
                size="mini"
                >新增</el-button
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
          <el-form-item label="品牌">
            <el-input
              style="width: 300px"
              :readonly="true"
              v-model="detail.brand"
            ></el-input>
          </el-form-item>
          <el-form-item label="销售价">
            <el-input
              style="width: 300px"
              :readonly="true"
              v-model="detail.price"
            ></el-input>
          </el-form-item>
          <el-form-item label="上下架">
            <el-tag size="mini" :type="detail.onSale === 1 ? '' : 'danger'">{{
              detail.onSale === 1 ? "上架" : "下架"
            }}</el-tag>
          </el-form-item>
          <el-form-item label="包上门费">
            <el-radio v-model="detail.freeDoorFee" :label="0">不包</el-radio>
            <el-radio v-model="detail.freeDoorFee" :label="1">包</el-radio>
          </el-form-item>
        </el-form>
      </template>

      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          :loading="editLoading"
          size="mini"
          type="success"
          @click="editDoorFee()"
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
  name: "doorproduct",
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
        isDoorProduct: true,
        freeDoorFee: undefined,
        onSale: undefined,
      },
      tableData: [],
      currentPage: 1,
      totalList: 0,
      brand: [],
      deleteLoading: false,
      formTitle: "详情",
      dialogVisible: false,
      detail: {
        id: 0,
        pid: "",
        name: "",
        brand: "",
        price: 0,
        onSale: 0,
        freeDoorFee: 0,
      },
      freeDooeFeeEnum: [
        { id: 0, displayName: "不包" },
        { id: 1, displayName: "包" },
      ],
      freeDooeFeeEnum1: [
        { id: 0, displayName: "不包上门费" },
        { id: 1, displayName: "包上门费" },
      ],
      editLoading: false,
      isDoorProductEnum: [
        { id: true, displayName: "上门产品" },
        { id: false, displayName: "非上门产品" },
      ],
      isOnSaleEnum: [
        { id: 1, displayName: "上架" },
        { id: 0, displayName: "下架" },
      ],
      multipleSelection: [],
      checkDoor: false,
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
        .getDoorProductPageList(this.condition)
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
    add() {
      if (this.multipleSelection.length == 0) {
        this.$message({ message: "请选择产品", type: "warning" });
        return;
      }
      var addRequest = {
        doorProducts: [],
      };

      for (var i = 0; i < this.multipleSelection.length; i++) {
        var itemProduct = {
          pid: this.multipleSelection[i].pid,
          categoryId: this.multipleSelection[i].categoryId,
          freeDoorFee: this.multipleSelection[i].freeDoorFee,
        };
        addRequest.doorProducts.push(itemProduct);
      }
      productSearchSvc
        .addDoorProducts(addRequest)
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
    },
    addOne(rowP) {
      var addRequest = {
        doorProducts: [],
      };
      var itemProduct = {
        pid: rowP.pid,
        categoryId: rowP.categoryId,
        freeDoorFee: rowP.freeDoorFee,
      };
      addRequest.doorProducts.push(itemProduct);
      productSearchSvc
        .addDoorProducts(addRequest)
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
    },
    deleteDoorProduct(doorProductId) {
      this.$confirm("确定要删除吗?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        closeOnClickModal: false,
        type: "warning",
      })
        .then(() => {
          var deleteRequest = {
            id: doorProductId,
            isDeleted: 1,
          };
          productSearchSvc
            .editDoorProduct(deleteRequest)
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
    cancel() {
      this.dialogVisible = false;
    },
    routeDetail(rowProduct) {
      Object.assign(this.detail, rowProduct);
      this.dialogVisible = true;
    },
    editDoorFee() {
      this.editLoading = true;
      var editRequest = {
        id: this.detail.id,
        freeDoorFee: this.detail.freeDoorFee,
      };
      productSearchSvc
        .editDoorProduct(editRequest)
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
    selectable(row, index) {
      if (row.id == 0) {
        return true;
      } else {
        return false;
      }
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    changeDoor() {
      for (var i = 0; i < this.tableData.length; i++) {
        if (this.tableData[i].id == 0) {
          this.tableData[i].freeDoorFee = this.checkDoor ? 1 : 0;
        }
      }
    },
    changeProductType() {
      if (this.condition.isDoorProduct == false) {
        this.condition.freeDoorFee = undefined;
      }
    },
  },
};
</script>