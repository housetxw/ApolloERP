<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <main>
        <el-tabs v-model="activeName" type="card" @tab-click="handleClick">
          <el-tab-pane label="待收货" name="first">
            <el-button
              size="mini"
              type="primary"
              style="margin-bottom:10px;"
              @click="purchaseReceipt()"
              >批量收货</el-button
            >
            <el-table
              border
              :data="receiptTableData"
              ref="multipleTable"
              @selection-change="handleSelectionChange"
              style="width: 100%"
            >
              <el-table-column type="selection"> </el-table-column>
              <el-table-column
                label="目标单位"
                prop="locationName"
              ></el-table-column>
              <el-table-column
                label="产品名称"
                prop="productName"
              ></el-table-column>
              <el-table-column
                label="产品编号"
                prop="productId"
              ></el-table-column>
              <el-table-column label="数量" prop="num"></el-table-column>
              <el-table-column
                label="要货时间"
                prop="handelTime"
              ></el-table-column>
            </el-table>
          </el-tab-pane>
          <el-tab-pane label="已收货" name="second">
            <el-table border :data="receiptedTableData" style="width: 100%">
              <el-table-column
                label="目标单位"
                prop="locationName"
              ></el-table-column>
              <el-table-column
                label="产品名称"
                prop="productName"
              ></el-table-column>
              <el-table-column
                label="产品编号"
                prop="productId"
              ></el-table-column>
              <el-table-column label="数量" prop="num"></el-table-column>
              <el-table-column
                label="收货时间"
                prop="handelTime"
              ></el-table-column>
            </el-table>
          </el-tab-pane>
        </el-tabs>
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
       activeName: "first",
      tableData: [],
      receiptTableData: [], //待收
      receiptedTableData: [], //已收
      formModel: {
        searchType: undefined
      },
      checkInStockRequest: {
        products: []
      }
    };
  },

  created() {
    this.fetchData();
  },
  methods: {
    toggleSelection(rows) {
      if (rows) {
        rows.forEach(row => {
          this.$refs.multipleTable.toggleRowSelection(row);
        });
      } else {
        this.$refs.multipleTable.clearSelection();
      }
    },
    
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    handleClick(tab, event) {
      if (this.activeName == "first") {
        this.formModel.searchType = 3;
      } else if (this.activeName == "second") {
        this.formModel.searchType = 4;
      }
      this.getCompanyInStocks();
    },
    getCompanyInStocks() {
      if (
        this.formModel.searchType == "" ||
        this.formModel.searchType == undefined
      ) {
        this.formModel.searchType = 3;
      }
      appSvc
        .getCompanyInStocks(this.formModel)
        .then(
          res => {
            if (this.formModel.searchType == 3) {
              this.receiptTableData = res.data;
            } else if (this.formModel.searchType == 4) {
              this.receiptedTableData = res.data;
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    fetchData() {
      this.getCompanyInStocks();
    },
    //发货 purchaseDelivery
    purchaseReceipt() {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          debugger;
          if (this.$refs.multipleTable.selection.length > 0) {
            for (
              var i = 0;
              i < this.$refs.multipleTable.selection.length;
              i++
            ) {
              this.checkInStockRequest.products.push(
                this.$refs.multipleTable.selection[i]
              );
            }

            const loading = this.$loading({
              lock: true,
              text: "拼命提交中......",
              spinner: "el-icon-loading",
              background: "rgba(0, 0, 0, 0.7)"
            });
            appSvc
              .venderCheckInStock(this.checkInStockRequest)
              .then(
                res => {
                  this.formModel.searchType = 3;
                  this.getCompanyInStocks();
                },
                error => {
                  console.log(error);
                }
              )
              .catch(() => {})
              .finally(() => {
                loading.close();
              });
          } else {
            this.$message({
              message: "请选择需要收货的产品!",
              type: "warning"
            });
          }
        })
        .catch(error => {
          console.log(error);
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
