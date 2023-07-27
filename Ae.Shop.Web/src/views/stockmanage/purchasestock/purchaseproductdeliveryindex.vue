<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <main>
        <el-tabs v-model="activeName" type="card" @tab-click="handleClick">
          <el-tab-pane label="待发货" name="first">
            <el-button
              size="mini"
              type="primary"
              style="margin-bottom:10px;"
              @click="purchaseDelivery()"
              >批量发货</el-button
            >
            <el-table
              border
              :data="waitdeliveryTableData"
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

              <el-table-column label="操作">
                <template slot-scope="scope">
                  <el-button type="primary" @click="cancelDelivery(scope.row)"
                    >取消</el-button
                  >
                </template>
              </el-table-column>
            </el-table>
          </el-tab-pane>
          <el-tab-pane label="已发货" name="second">
            <el-table border :data="deliveryTableData" style="width: 100%">
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
                label="发货时间"
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

import { appSvc } from "@/api/purchasestock/purchase";

import { isNumber } from "util";

export default {
  data() {
    return {
      activeName: "first",
      waitdeliveryTableData: [],
      deliveryTableData: [],
      cancelRequest: {
        relationId: undefined,
        taskType: undefined
      },
      deliveryRequest: {
        products: []
      },
      formModel: {
        searchType: undefined
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
        this.formModel.searchType = 1;
      } else if (this.activeName == "second") {
        this.formModel.searchType = 2;
      }
      this.getCompanyInStocks();
    },
    getCompanyInStocks() {
      if (
        this.formModel.searchType == "" ||
        this.formModel.searchType == undefined
      ) {
        this.formModel.searchType = 1;
      }
      appSvc
        .getCompanyInStocks(this.formModel)
        .then(
          res => {
            if (this.formModel.searchType == 1) {
              this.waitdeliveryTableData = res.data;
            } else if (this.formModel.searchType == 2) {
              this.deliveryTableData = res.data;
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
    purchaseDelivery() {
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
              this.deliveryRequest.products.push(
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
              .companySendStock(this.deliveryRequest)
              .then(
                res => {
                  this.formModel.searchType = 1;
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
              message: "请选择需要发货的产品!",
              type: "warning"
            });
          }
        })
        .catch(error => {
          console.log(error);
        });
    },
    //取消发货 cancelDelivery
    cancelDelivery(row) {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.cancelRequest.relationId = row.relationId;
          this.cancelRequest.taskType = row.taskType;
          appSvc
            .cancelCompanySendStock(this.cancelRequest)
            .then(
              res => {
                this.formModel.searchType = 1;
                this.getCompanyInStocks();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
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
