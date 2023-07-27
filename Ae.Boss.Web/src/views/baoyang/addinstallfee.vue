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
            style="width: 200px"
            v-model="condition.serviceId"
            filterable
            clearable
            placeholder="-安装服务-"
          >
            <el-option
              v-for="item in installService"
              :key="item.productCode"
              :label="item.name"
              :value="item.productCode"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="请输入车型指导价"
            style="width: 160px"
            prefix-icon="el-icon-search"
            v-model="condition.guidePrice"
          >
          </el-input>
        </el-form-item>
      </template>

      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
          >新增</el-button
        >
      </template>

      <template v-slot:tb_cols>
        <el-table-column
          label="服务pid"
          prop="serviceId"
          :width="150"
        ></el-table-column>
        <el-table-column label="服务名称" prop="serviceName"></el-table-column>
        <el-table-column
          label="最小指导价(万)"
          prop="carMinPrice"
          align="right"
          :width="102"
        ></el-table-column>
        <el-table-column
          label="最大指导价(万)"
          prop="carMaxPrice"
          align="right"
          :width="102"
        ></el-table-column>
        <el-table-column
          label="服务原价"
          prop="originalPrice"
          align="right"
          :width="70"
        ></el-table-column>
        <el-table-column
          label="服务加价"
          prop="additionalPrice"
          align="right"
          :width="70"
        ></el-table-column>
        <el-table-column label="备注" prop="remark"></el-table-column>
        <el-table-column
          label="创建人"
          prop="createBy"
          :width="70"
        ></el-table-column>
        <el-table-column
          label="创建时间"
          prop="createTime"
          :width="128"
        ></el-table-column>
        <el-table-column label="操作" :width="90">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                @click="routeDetail(scope.row)"
                type="primary"
                size="mini"
                >编辑</el-button
              >
              <el-button
                @click="deleteData(scope.row.id)"
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
          <el-form-item label="服务pid">
            <el-input
              style="width: 300px"
              :disabled="true"
              v-model="detail.serviceId"
            ></el-input>
          </el-form-item>
          <el-form-item label="服务名称">
            <el-input
              style="width: 300px"
              :disabled="true"
              v-model="detail.serviceName"
            ></el-input>
          </el-form-item>
          <el-form-item label="服务原价">
            <el-input
              style="width: 100px"
              :disabled="true"
              v-model="detail.originalPrice"
            ></el-input>
          </el-form-item>
          <el-form-item label="最小指导价">
            <el-input
              style="width: 100px"
              v-model="detail.carMinPrice"
            ></el-input>
            <span>万</span>
          </el-form-item>
          <el-form-item label="最大指导价">
            <el-input
              style="width: 100px"
              v-model="detail.carMaxPrice"
            ></el-input>
            <span>万（不包括{{ detail.carMaxPrice }}万）</span>
          </el-form-item>
          <el-form-item label="服务加价">
            <el-input
              style="width: 100px"
              v-model="detail.additionalPrice"
            ></el-input>
          </el-form-item>
          <el-form-item label="备注">
            <el-input style="width: 300px" v-model="detail.remark"></el-input>
          </el-form-item>
        </el-form>
      </template>
      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          :loading="saveLoading"
          size="mini"
          type="success"
          @click="editInstallFee()"
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
      width="80%"
      maxWidth="600px"
      minWidth="600px"
    >
      <template v-slot:content>
        <el-form :model="addInstall" size="mini" label-width="120px">
          <el-form-item label="安装服务">
            <el-select
              style="width: 300px"
              v-model="addInstall.serviceId"
              filterable
              placeholder="-安装服务-"
            >
              <el-option
                v-for="item in installService"
                :key="item.productCode"
                :label="item.name"
                :value="item.productCode"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="最小指导价">
            <el-input
              style="width: 100px"
              v-model="addInstall.carMinPrice"
            ></el-input>
            <span>万</span>
          </el-form-item>
          <el-form-item label="最大指导价">
            <el-input
              style="width: 100px"
              v-model="addInstall.carMaxPrice"
            ></el-input>
            <span>万（不包括{{ addInstall.carMaxPrice }}万）</span>
          </el-form-item>
          <el-form-item label="服务加价">
            <el-input
              style="width: 100px"
              v-model="addInstall.additionalPrice"
            ></el-input>
          </el-form-item>
          <el-form-item label="备注">
            <el-input
              style="width: 300px"
              v-model="addInstall.remark"
            ></el-input>
          </el-form-item>
        </el-form>
      </template>
      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          :loading="saveLoading1"
          size="mini"
          type="success"
          @click="addInstallFeeConfig()"
          >提交</el-button
        >
      </template>
    </rg-dialog>
  </main>
</template>
<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/baoyang/baoyang";
export default {
  name: "addinstallfee",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      condition: {
        pageIndex: 1,
        pageSize: 20,
        serviceId: "",
        guidePrice: undefined,
      },
      installService: [],
      tableData: [],
      detail: {
        id: 0,
        serviceId: "",
        serviceName: "",
        carMinPrice: 0,
        carMaxPrice: 0,
        originalPrice: 0,
        additionalPrice: 0,
        remark: "",
        createBy: "",
        createTime: "",
      },
      formTitle: "详情",
      dialogVisible: false,
      saveLoading: false,
      formTitle1: "新增",
      dialogVisible1: false,
      addInstall: {
        serviceId: "",
        carMinPrice: undefined,
        carMaxPrice: undefined,
        additionalPrice: undefined,
        remark: "",
      },
      saveLoading1: false,
    };
  },
  created() {
    this.fetchData();
    this.getBaoYangInstallServiceEnum();
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
      var s = /(^[1-9](\d+)?(\.\d{1,2})?$)|(^\d\.\d{1,2}$)/;
      if (
        !s.test(this.condition.guidePrice) &&
        this.condition.guidePrice !== "0"
      ) {
        this.condition.guidePrice = undefined;
      }
      appSvc
        .getInstallAddFeeConfig(this.condition)
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
    getBaoYangInstallServiceEnum() {
      appSvc
        .getBaoYangInstallService()
        .then(
          (res) => {
            var data = res.data;
            this.installService = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    add() {
      this.addInstall = {
        serviceId: "",
        carMinPrice: undefined,
        carMaxPrice: undefined,
        additionalPrice: undefined,
        remark: "",
      };
      this.dialogVisible1 = true;
    },
    routeDetail(rowI) {
      Object.assign(this.detail, rowI);
      this.dialogVisible = true;
    },
    cancel() {
      this.dialogVisible = false;
    },
    editInstallFee() {
      var s = /(^[1-9](\d+)?(\.\d{1,2})?$)|(^\d\.\d{1,2}$)/;
      if (!s.test(this.detail.carMinPrice) && this.detail.carMinPrice !== "0") {
        this.$message({
          message: "最小指导价输入有误，请输入最多2位小数的非负数",
          type: "warning",
        });
        return;
      }
      if (!s.test(this.detail.carMaxPrice) && this.detail.carMaxPrice !== "0") {
        this.$message({
          message: "最大指导价输入有误，请输入最多2位小数的非负数",
          type: "warning",
        });
        return;
      }
      if (
        !s.test(this.detail.additionalPrice) &&
        this.detail.additionalPrice !== "0"
      ) {
        this.$message({
          message: "服务加价输入有误，请输入最多2位小数的非负数",
          type: "warning",
        });
        return;
      }
      var deitRequest = {
        id: this.detail.id,
        carMinPrice: this.detail.carMinPrice,
        carMaxPrice: this.detail.carMaxPrice,
        additionalPrice: this.detail.additionalPrice,
        remark: this.detail.remark,
      };
      this.saveLoading = true;

      appSvc
        .editInstallAddFeeConfig(deitRequest)
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
          this.saveLoading = false;
        });
    },
    deleteData(installFeeId) {
      this.$confirm("确定要删除吗?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        closeOnClickModal: false,
        type: "warning",
      })
        .then(() => {
          var deleteRequest = {
            id: installFeeId,
            isDeleted: true,
          };
          appSvc
            .editInstallAddFeeConfig(deleteRequest)
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
    cancel1() {
      this.dialogVisible1 = false;
    },
    addInstallFeeConfig() {
      if (this.addInstall.serviceId == "") {
        this.$message({
          message: "请选择安装服务",
          type: "warning",
        });
        return;
      }
      var s = /(^[1-9](\d+)?(\.\d{1,2})?$)|(^\d\.\d{1,2}$)/;
      if (
        !s.test(this.addInstall.carMinPrice) &&
        this.addInstall.carMinPrice !== "0"
      ) {
        this.$message({
          message: "最小指导价输入有误，请输入最多2位小数的非负数",
          type: "warning",
        });
        return;
      }
      if (
        !s.test(this.addInstall.carMaxPrice) &&
        this.addInstall.carMaxPrice !== "0"
      ) {
        this.$message({
          message: "最大指导价输入有误，请输入最多2位小数的非负数",
          type: "warning",
        });
        return;
      }
      if (
        !s.test(this.addInstall.additionalPrice) &&
        this.addInstall.additionalPrice !== "0"
      ) {
        this.$message({
          message: "服务加价输入有误，请输入最多2位小数的非负数",
          type: "warning",
        });
        return;
      }
      var addRequest = {
        serviceId: this.addInstall.serviceId,
        carMinPrice: this.addInstall.carMinPrice,
        carMaxPrice: this.addInstall.carMaxPrice,
        additionalPrice: this.addInstall.additionalPrice,
        remark: this.addInstall.remark,
      };
      this.saveLoading1 = true;
      appSvc
        .addInstallAddFeeConfig(addRequest)
        .then(
          (res) => {
            var data = res.data;
            if (data) {
              this.$message({ message: "新增成功", type: "success" });
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
          this.saveLoading1 = false;
        });
    },
  },
};
</script>