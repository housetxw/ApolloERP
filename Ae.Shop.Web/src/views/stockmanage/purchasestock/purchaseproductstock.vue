<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <el-table border :data="tableData" style="width: 100%">
        <el-table-column type="expand">
          <template slot-scope="props">
            <el-table
              border
              :data="props.row.productStocks"
              style="width: 100%"
            >
             
              <el-table-column
                label="产品编号"
                prop="productId"
              ></el-table-column>
              <el-table-column
                label="产品编号"
                prop="productId"
              ></el-table-column>
              <el-table-column label="库存量" prop="num"></el-table-column>
            </el-table>
          </template>
        </el-table-column>
         <el-table-column
                label="目标单位"
                prop="locationName"
              ></el-table-column>
      </el-table>
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
      tableData: []     
    };
  },

  created() {
    this.fetchData();
  },
  methods: {
    getCompanyStocks() {
      appSvc
        .getCompanyStocks()
        .then(
          res => {
            debugger;
            this.tableData = res.data.stocks;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    fetchData() {
      this.getCompanyStocks();
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
