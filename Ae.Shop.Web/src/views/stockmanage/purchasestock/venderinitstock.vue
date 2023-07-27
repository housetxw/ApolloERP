<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <main>
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
import { Loading } from "element-ui";
import { appSvc } from "@/api/purchasestock/purchase";

import { isNumber } from "util";

export default {
  data() {
    return {
      tableData: [],
      initRequest: {
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
    searchVenderProductListForApp() {
      appSvc
        .searchVenderProductListForApp()
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
      this.searchVenderProductListForApp();
      //this.getVenders();
    },
    submitStock() {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          const loading = this.$loading({
            lock: true,
            text: "拼命提交中......",
            spinner: "el-icon-loading",
            background: "rgba(0, 0, 0, 0.7)"
          });

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
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
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
