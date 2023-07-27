<template>
  <div class="container">
    <header>
      <div class="title">
        <h4>门店服务设置</h4>
      </div>
    </header>
    <div class="radios">
      <span class="input-label">服务大类:</span>
      <el-radio-group v-model="serviceList">
        <el-radio
          v-for="(item,index) in shopService"
          :label="3*(index+1)"
          :key="index"
          @change="table2(item.id)"
        >{{item.name}}</el-radio>
      </el-radio-group>
    </div>
    <section class="main">
      <div>
        <el-tabs v-model="activeName" type="card" @tab-click="handleClick">
          <el-tab-pane label="上架过的服务" name="first">
            <div class="tables">
              <el-table :data="tableData2" style="width: 100%; " v-loading="tableLoading">
                <el-table-column prop="id" label="编号" width="160"></el-table-column>
                <el-table-column prop="productId" label="服务PID" width="180"></el-table-column>
                <el-table-column prop="name" label="服务名称" width="180"></el-table-column>
                <el-table-column prop="costPrice" label="结算价格" width="180"></el-table-column>
                <el-table-column prop="salePrice" label="销售价格" width="180"></el-table-column>
                <el-table-column prop="address" label="服务说明" width="180"></el-table-column>
                <el-table-column prop="createTime" label="创建时间" width="180"></el-table-column>
                <el-table-column prop="updateTime" label="更新时间" width="180"></el-table-column>
                <el-table-column prop="address" label="操作">
                  <template slot-scope="scope">
                    <el-button size="mini" type="warning" @click="handleEdit(scope.row)">编辑</el-button>
                  </template>
                </el-table-column>
              </el-table>
            </div>
          </el-tab-pane>
          <el-tab-pane label="未上架的服务" name="second">
            <div class="tables">
              <el-table
                :data="tableData4"
                style="width: 100%; "
                v-loading="tableLoading"
                ref="tableData4"
              >
                <el-table-column prop="baseServiceId" label="编号" width="160"></el-table-column>
                <el-table-column prop="productId" label="服务PID" width="180"></el-table-column>
                <el-table-column prop="name" label="服务名称" width="180"></el-table-column>
                <el-table-column prop="defaultCostPrice" label="结算价格" width="180"></el-table-column>
                <el-table-column prop="defaultSalePrice" label="销售价格" width="180"></el-table-column>
                <el-table-column prop="address" label="服务说明" width="180"></el-table-column>
                <el-table-column prop="createTime" label="创建时间" width="180"></el-table-column>
                <el-table-column prop="updateTime" label="更新时间" width="180"></el-table-column>
                <el-table-column prop="address" label="操作">
                  <template slot-scope="scope">
                    <el-button size="mini" type="warning" @click="addShopService(scope.row)">添加</el-button>
                  </template>
                </el-table-column>
              </el-table>
            </div>
          </el-tab-pane>
        </el-tabs>
      </div>
      <!-- 编辑上架 -->
      <div>
        <el-dialog title="编辑服务：" :visible.sync="dialogFormVisible" :close-on-click-modal="false">
          <el-form :model="tableData3" :inline="true" class="demo-form-inline">
            <el-form-item label="服务项目：">
              <el-input v-model="tableData3.name" autocomplete="off"></el-input>
            </el-form-item>
            <el-form-item>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <el-checkbox
                :value="tableData3.status === 1"
                @change="val => $set(tableData3,'status',val ? 1 : 0)"
              >是否上架</el-checkbox>
            </el-form-item>
            <br />
            <el-row :gutter="20">
            <el-form-item label="结算价格：">
              <el-input v-model="tableData3.costPrice" autocomplete="off"></el-input>
            </el-form-item>
            <el-form-item label="结算上限：">
              <el-input v-model="tableData3.defaultCostPriceLimit" autocomplete="off" disabled="true"></el-input>
            </el-form-item>
            </el-row>
            <el-row :gutter="20">
            <el-form-item label="销售价格：">
              <el-input v-model="tableData3.salePrice" autocomplete="off"></el-input>
            </el-form-item>
            <el-form-item label="销售上限：">
              <el-input v-model="tableData3.defaultSalePriceLimit" autocomplete="off" disabled="true"></el-input>
            </el-form-item>
            </el-row>
          </el-form>
          <div slot="footer" class="dialog-footer">
            <el-button @click="dialogFormVisible = false">取 消</el-button>
            <el-button type="primary" @click="ModifyShopService()">确 定</el-button>
          </div>
        </el-dialog>
      </div>
    </section>
  </div>
</template>
<script>
import { shopManageSvc } from "@/api/shopmanage/shopmanage";
let itemId = "";
// let type = "";

export default {
  data() {
    return {
      activeName: "first",
      dialogFormVisible: false,
      dialogFormVisible2: false,
      tableData3: [{ status: 1 }],
      tableData2: [],
      serviceList: {},
      shopService: [],
      tableData4: [],
      tableData5: [],
      shopId: 0,
      serviceList: 3,
      id2: "",
      tableLoading: false
    };
  },

  created() {
    this.GetBaseServiceList(0);
    this.table2(1, 1);
  },
  mounted() {
    this.shopId = this.$route.params.id;
  },
  methods: {
    //改变type的值
    handleClick(tab, event) {
      if (tab.name == "first") {
        this.GetShopServiceList(itemId, 1);
      } else if (tab.name == "second") {
        this.GetShopServiceList(itemId, 0);
      }
    },

    table2(id, type) {
      itemId = id;
      if (this.activeName == "first") {
        this.GetShopServiceList(id, 1);
      } else if (this.activeName == "second") {
        // handleClick(tab)

        this.GetShopServiceList(id, 0);
      }
      //   this.GetShopServiceList(id, type);
    },
    //获取table表格
    GetShopServiceList(id2, type) {
      console.log("门店ID: " + JSON.stringify(this.$route.params.id));
      this.tableLoading = true;
      shopManageSvc
        .GetShopServiceList({
          CategoryId: id2,
          ShopId: this.$route.params.id,
          Type: type
        })
        .then(
          res => {
            if (id2 == 0) {
              var data = res.data;
              this.shopService = data;
              console.log(222, data);
              console.log(id2);
            } else if (type == 1) {
              var data = res.data;
              this.tableData2 = data;
              console.log(11111, this.tableData2);
              // console.log(1111, id);
            } else if (type == 0) {
              var data = res.data;
              this.tableData4 = data;
              console.log(1345, this.tableData4);
              // GetShopServiceList(itemId, type);
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          console.log("tableLoading: false");
          this.tableLoading = false;
        });
    },
    //获取table表格
    GetBaseServiceList(id) {
      this.tableLoading = true;
      shopManageSvc
        .GetBaseServiceList({
          CategoryId: id
        })

        .then(
          res => {
            if (id == 0) {
              var data = res.data;
              this.shopService = data;
              console.log(222, data);
              console.log(id2);
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          console.log("tableLoading: false");
          this.tableLoading = false;
        });
    },
    // 编辑上架商品
    handleEdit(row) {
      this.tableData3 = row;
      // console.log(999333,this.tableData2)
      this.tableData3 = Object.assign({}, row);
      this.dialogFormVisible = true;
    },
    ModifyShopService() {
      console.log(9999, this.tableData3);
      shopManageSvc
        .ModifyShopService({
          id: this.tableData3.id,
          status: this.tableData3.status,
          costPrice: this.tableData3.costPrice,
          salePrice: this.tableData3.salePrice,
          updateBy:'system'
        })
        .then(
          res => {
            var data = res.code;
                if (data == "10000") {
                  this.$message({
                    message: "编辑成功",
                    type: "success"
                  });
                  this.goLastView(this.visitedViews, 1000);
                } else {
                  this.$message({
                    message: "编辑失败:" + res.message,
                    type: "error"
                  });
                }
                setTimeout(function() {
                  loading.close();
                }, 1500);
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },

    //添加未上架商品
    addShopService(row) {
      this.tableData5 = row;
      this.tableData5 = Object.assign({}, row);
      console.log(2342, row);
      console.log(2342, this.tableData5.baseServiceId);
      shopManageSvc
        .AddShopService({
          shopId: this.shopId,

          baseServiceId: this.tableData5.baseServiceId,

          createBy: "system"
        })
        .then(
          res => {
            var data = res.data;

            //   this.row.splice(index, 1);
            //  console.log(66666,index)

            console.log(2222, row);

            if (res.code == 10000) {
              this.$message({
                message: "添加成功",
                type: "success"
              });
            }
            this.GetShopServiceList(itemId, 0);
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    }
  }
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 20px;
  border: 1px solid #d9d9d9;
  padding: 20px;
}
.title {
  padding: 20px;
  height: 50px;
  background: #eee;
}
h4 {
  margin: 0;
  font-size: 17px;
}
.radios {
  margin-top: 15px;
}
.main {
  margin-top: 10px;
}

.el-table th,
.el-table tr {
  text-align: center;
  background: #f5f7fa;
  color: #333;
}
 .el-table td,
.el-table th {
  text-align: center;
}

.el-radio-group {
  padding: 15px;
}
</style>