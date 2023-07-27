<template>
  <main class="container full-height">
    <section class="margin-top-30">
      <el-table border :data="tableData" style="width: 100%">
        <rg-table-column label="产品编号" prop="productId" />
        <rg-table-column label="产品名称" prop="productName" />
        <rg-table-column label="采购量" prop="purchaseNum" is-number />
        <rg-table-column label="库存量" prop="stockNum" is-number />
        <rg-table-column label="销售量" prop="saleNum" is-number>
          <template slot-scope="scope">
            <div>
              <el-button
                @click="getdetailNum(scope.row)"
                size="mini"
                type="text"
                >{{ scope.row.saleNum }}</el-button
              >
            </div>
          </template>
        </rg-table-column>
        <rg-table-column label="其他" prop="otherNum" is-number />
      </el-table>
    </section>
    <section id="createOrUpdate">
      <rg-dialog
        :title="detailformTitle"
        :visible.sync="detailformVisible"
        :beforeClose="viewdetailcancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            viewdetailcancel();
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="90%"
        maxWidth="1024px"
        minWidth="900px"
      >
        <template v-slot:content>
          <section>
            <el-table
              border show-summary :summary-method="getSummaries"
              :data="productStockTable"
              style="width: 100%"
              size="mini"
            >
              <rg-table-column
                label="门店名称"
                prop="locationName" 
              ></rg-table-column>
              <rg-table-column
                label="产品编号"
                prop="productId"  maxWidth="200px"
              ></rg-table-column>
              <rg-table-column
                label="产品名称"
                prop="productName"
              ></rg-table-column>

              <rg-table-column
                label="库存量"
                prop="stockNum"
                align="right"
              ></rg-table-column>

              <rg-table-column
                label="销售量"
                prop="saleNum"
                align="right"
              ></rg-table-column>
            </el-table>
          </section>
        </template>
      </rg-dialog>
    </section>
  </main>
</template>

<script>
import { appSvc } from "@/api/stock/stockmanage";
export default {
  name: "child1",
  data() {
    return {
      tableData: [],
      productStockTable: [],
      productStockConditon: {
        productId: undefined
      },
      detailformVisible: false,
      detailformTitle: undefined
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
     getSummaries(param) {
      const { columns, data } = param;
      const sums = [];
      columns.forEach((column, index) => {
        if (index === 0) {
          sums[index] = "合计";
          return;
        }
        const values = data.map(item => Number(item[column.property])); 
        if (column.property == "stockNum" || column.property == "saleNum") {
          sums[index] = values.reduce((prev, curr) => {
            const value = Number(curr);
            if (!isNaN(value)) {
              return prev + curr;
            } else {
              return prev;
            }
          }, 0);
          sums[index];
        }
      });
      return sums;
    },
    viewdetailcancel() {
      this.productStockTable = [];
      this.detailformVisible = false;
    },
    getdetailNum(row) {
      this.detailformTitle = "【" + row.productName + "】的库存明细";
      this.detailformVisible = true;
      const loading = this.$loading({
        lock: true,
        text: "拼命加载中...",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)"
      });
      this.productStockConditon.productId = row.productId;
      appSvc
        .getSupplierProductStockDetails(this.productStockConditon)
        .then(
          res => {
            this.productStockTable = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    },
    fetchData() {
      const loading = this.$loading({
        lock: true,
        text: "拼命加载中...",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)"
      });
      appSvc
        .getSupplierProductStocks()
        .then(
          res => {
            this.tableData = res.data;
          },
          error => {
            console.log(error);
          }
        )
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
    height: 48px;
    display: flex;
    align-items: center;
  }
  .pagination {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100px;
  }
  .header-search {
    display: none;
  }
}
</style>
