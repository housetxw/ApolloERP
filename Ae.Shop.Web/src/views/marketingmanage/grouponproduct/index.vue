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
            class="input"
            clearable
            placeholder="请输入产品名称或pid"
            style="width: 200px"
            prefix-icon="el-icon-search"
            v-model="condition.productName"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.status"
            clearable
            style="width: 100px"
            placeholder="团购状态"
          >
            <el-option
              v-for="item in activityStatus"
              :key="item.value"
              :label="item.displayName"
              :value="item.value"
            >
            </el-option>
          </el-select>
        </el-form-item>
      </template>
      <template v-slot:tb_cols>
        <el-table-column
          label="产品pid"
          prop="pid"
          :width="180"
        ></el-table-column>
        <el-table-column label="产品名称" prop="name"></el-table-column>
        <el-table-column
          label="平台售价"
          prop="originalPrice"
          :width="80"
          align="right"
        ></el-table-column>
        <el-table-column
          label="最小售价"
          prop="minPrice"
          :width="80"
          align="right"
        ></el-table-column>
        <el-table-column
          label="最大售价"
          prop="maxPrice"
          :width="80"
          align="right"
        ></el-table-column>
        <el-table-column label="团购状态" align="center" :width="80">
          <template slot-scope="scope">
            <el-tag
              size="mini"
              :type="scope.row.status === 1 ? '' : 'danger'"
              >{{ scope.row.status === 1 ? "已上架" : "未上架" }}</el-tag
            >
          </template>
        </el-table-column>
        <el-table-column
          label="团购价"
          prop="price"
          :width="80"
          align="right"
        ></el-table-column>
        <el-table-column align="center" label="操作" :width="60">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                @click="routeDetail(scope.row.pid)"
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
          <el-form-item label="平台售价">
            <el-input
              style="width: 300px"
              :readonly="true"
              v-model="detail.originalPrice"
            ></el-input>
          </el-form-item>
          <el-form-item label="最小售价">
            <el-input
              style="width: 300px"
              :readonly="true"
              v-model="detail.minPrice"
            ></el-input>
          </el-form-item>
          <el-form-item label="最大售价">
            <el-input
              style="width: 300px"
              :readonly="true"
              v-model="detail.maxPrice"
            ></el-input>
          </el-form-item>
          <el-form-item label="团购状态">
            <el-select
              v-model="detail.status"
              style="width: 100px"
              placeholder="选择状态"
            >
              <el-option
                v-for="item in activityStatus"
                :key="item.value"
                :label="item.displayName"
                :value="item.value"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="团购价">
            <el-input style="width: 300px" v-model="detail.price"></el-input>
          </el-form-item>
        </el-form>
      </template>
      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          :loading="editLoading"
          size="mini"
          type="success"
          @click="editGrouponProduct()"
          >提交</el-button
        >
      </template>
    </rg-dialog>
  </main>
</template>
<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/marketingmanage/promotion";
export default {
  name: "grouponproduct",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      condition: {
        pageIndex: 1,
        pageSize: 20,
        productName: "",
        status: undefined,
      },
      tableData: [],
      activityStatus: [
        {
          displayName: "未上架",
          value: 0,
        },
        {
          displayName: "已上架",
          value: 1,
        },
      ],
      editLoading: false,
      dialogVisible: false,
      formTitle: "详情",
      detail: {
        pid: "",
        name: "",
        originalPrice: 0,
        price: 0,
        minPrice: 0,
        maxPrice: 0,
        status: 0,
        statusDisplay: "",
      },
    };
  },
  created() {
    this.fetchData();
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
      appSvc
        .getGrouponProductPageList(this.condition)
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
    routeDetail(pid) {
      var detailRequest = {
        pid: pid,
      };
      appSvc
        .getGrouponProductDetail(detailRequest)
        .then(
          (res) => {
            var data = res.data;
            if (data != null) {
              Object.assign(this.detail, data);
              this.dialogVisible = true;
            } else {
              this.$message({
                message: "当前产品已下架/已禁用",
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
    editGrouponProduct() {
      var s = /(^[1-9](\d+)?(\.\d{1,2})?$)|(^\d\.\d{1,2}$)|(^[0]$)/;
      if (!s.test(this.detail.price)) {
        this.$message({
          message: "团购价输入有误，请输入最多2位小数的非负数",
          type: "warning",
        });
        return;
      }
      if (this.detail.status == 1) {
        if (
          parseFloat(this.detail.minPrice) > parseFloat(this.detail.price) ||
          parseFloat(this.detail.maxPrice) < parseFloat(this.detail.price)
        ) {
          this.$message({
            message:
              "团购价超出限制范围：" +
              this.detail.minPrice +
              " ~ " +
              this.detail.maxPrice,
            type: "warning",
          });
          return;
        }
      }

      this.editLoading = true;
      var editRequest = {
        pid: this.detail.pid,
        price: this.detail.price,
        status: this.detail.status,
      };
      appSvc
        .saveShopGrouponProduct(editRequest)
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
  },
};
</script>
