<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <main>
        <el-select
          v-model="formmodel.id"
          clearable
          filterable
          placeholder="请选择公司"
          @change="selectCompany()"
          style="width:200px;"
        >
          <el-option
            v-for="item in companySel"
            :key="item.id"
            :label="item.simpleName"
            :value="item.id"
          ></el-option>
        </el-select>

        <el-button
          type="primary"
          size="mini"
          icon="el-icon-plus"
          @click="submitStock()"
          style="margin-bottom:10px;"
          >提交</el-button
        >
        <section>
          <el-table border :data="tableData" style="width: 100%">
            <el-table-column
              label="产品编号"
              prop="productId"
            ></el-table-column>
            <el-table-column
              label="产品名称"
              prop="productName"
            ></el-table-column>

            <el-table-column label="预设值" width="120px">
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.num"
                  oninput="value=value.replace(/[^\d.]/g,'')"
                  clearable
                ></el-input>
              </template>
            </el-table-column>
          </el-table>
        </section>
      </main>
    </section>
    <!-- 首页 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appSvc } from "@/api/purchase/vendersetstock";

import { isNumber } from "util";

export default {
  data() {
    return {
      tableData: [],
      initRequest: {
        companyId:undefined,
        stocks: []
      },
      companySel: [],
      formmodel: {
        id: undefined
      }
    };
  },

  created() {
    this.fetchData();
  },
  methods: {
    getCompanys() {
      appSvc
        .getCompanys()
        .then(
          res => {
            this.companySel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    selectCompany() {
      appSvc
        .getVenderProducts(this.formmodel)
        .then(
          res => {
            this.tableData = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    fetchData() {
      this.getCompanys();
      //this.getVenders();
    },
    submitStock() {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.initRequest.companyId = this.formmodel.id;
          this.initRequest.stocks = this.tableData;
          appSvc
            .submitVenderStock(this.initRequest)
            .then(
              res => {
                this.$message({
                  message: "操作成功!",
                  type: "success"
                });
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    }
  }
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 90px;
    display: flex;
    align-items: center;
  }
  .pagination {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100px;
  }
}
</style>
