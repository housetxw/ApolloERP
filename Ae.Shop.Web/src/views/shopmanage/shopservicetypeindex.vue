<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <header class="bodyTop">
        <article class="filter-container">
          <span class="input-label">门店名称:</span>
          <el-select
            v-model="condition.shopId"
            filterable
            remote
            clearable
            reserve-keyword
            placeholder="请输入门店名称"
            :remote-method="getShopinfo"
            :loading="loading"
          >
            <el-option
              v-for="item in shopSel"
              :key="item.id"
              :label="item.simpleName"
              :value="item.id"
            ></el-option>
          </el-select>

          <el-button type="primary" size="small" @click="search()">查询</el-button>
        </article>
      </header>
      <main>
        <section>
          <el-table
            border
            :data="tableData"
            :span-method="cellMerge"
            style="width: 100%"
            :cell-style="cellStyle"
          >
            <el-table-column label="门店" prop="shopName"></el-table-column>
            <el-table-column label="服务名称" prop="serviceName"></el-table-column>

            <el-table-column label="是否有效" width="120px">
              <template slot-scope="scope">
                <div v-if="scope.row.isDeleted === 0">
                  <label>有效</label>
                </div>
                <div v-else-if="scope.row.isDeleted === 1">
                  <label style="color:red;">无效</label>
                </div>
              </template>
            </el-table-column>

            <el-table-column label="操作" width="150px">
              <template slot-scope="scope">
                <div v-if="scope.row.isDeleted === 0">
                  <el-button
                    size="mini"
                    type="warning"
                    style="padding:10px 7px;"
                    @click="deleteShopServiceType(scope.row,1)"
                  >删除</el-button>
                </div>
                <div v-else-if="scope.row.isDeleted === 1">
                  <el-button
                    size="mini"
                    type="danger"
                    style="padding:10px 7px;"
                    @click="deleteShopServiceType(scope.row,0)"
                  >恢复</el-button>
                </div>
              </template>
            </el-table-column>
          </el-table>
        </section>
        <section class="pagination">
          <el-pagination
            background
            :page-size="10"
            :page-sizes="[10, 30, 50, 100]"
            layout="total, sizes, prev, pager, next, jumper"
            :total="totalList"
            @current-change="pageTurning"
            :current-page.sync="currentPage"
            @size-change="sizeChange"
          ></el-pagination>
        </section>
      </main>
    </section>
    <!-- 首页 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { shopManageSvc } from "@/api/shopmanage/shopmanage";

import { isNumber } from "util";
export default {
  data() {
    return {
      pickerOptionsStart: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        }
      },
      input: undefined,

      readonly: true,
      tableLoading: false,
      currentPage: 1,
      loading: false,

      //table页面的条件
      condition: {
        shopId: undefined,
        pageIndex: 1,
        pageSize: 10
      },

      queryCondition: {
        shopId: undefined,
        pageIndex: 1,
        pageSize: 10
      },
      deleteCondition: {
        id: undefined,
        isDeleted: undefined
      },

      deleteConditionInit: {
        id: undefined,
        isDeleted: undefined
      },
      spanArr: [],
      pos: undefined,
      tableData: [],
      totalList: 0,

      tableData: [],

      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",
      shopSel: [],
      shopSelCondition: {
        pageIndex: undefined,
        pageSize: undefined,
        simpleName: undefined
      },

      formLabelWidth: "120px"
    };
  },
  computed: {
    routeComputed: {
      get: function() {
        return this.formModel.route;
      },
      set: function(val) {
        this.formModel.route = val.toLowerCase();
      }
    }
  },
  created() {
    //页面初始化函数
    var shopId = this.$route.query.shopId;
    this.fetchData(shopId);
  },
  methods: {
    getShopinfo(query) {
      if (query != "") {
        debugger;
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.pageIndex = 1;
          this.shopSelCondition.pageSize = 20;
          this.shopSelCondition.simpleName = query;
          debugger;
          shopManageSvc
            .getShopListNewAsync(this.shopSelCondition)
            .then(
              res => {
                var resData = res.data;
                this.shopSel = resData.items;
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        }, 200);
      } else {
        this.options = [];
      }
    },
    deleteShopServiceType(row, type) {
      var msg = type == 1 ? "删除" : "恢复";

      this.$confirm("确定" + msg + "吗?", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.deleteCondition.id = row.id;
          this.deleteCondition.isDeleted = type;
          var vm = this;
          shopManageSvc
            .deleteShopServiceType(this.deleteCondition)
            .then(
              res => {
                this.deleteCondition = JSON.parse(
                  JSON.stringify(this.deleteConditionInit)
                );

                vm.search();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },

    updateLocation() {
      if (this.condition.searchLocationId == "") {
        this.condition.searchLocationId = undefined;
        this.condition.locationId = this.$route.query.locationId;
      }
    },
    cellStyle(row, column, rowIndex, columnIndex) {
      //根据报警级别显示颜色
      // console.log(row);
      // console.log(row.column);
      if (row.row.isLimit === 1 && row.column.label === "是否限制") {
        return "color:red";
      }
    },
    fetchData(shopId) {
      var conditionModel = undefined;
      //从外部直接跳过来的
      if (shopId != "" && shopId != undefined) {
        this.queryCondition.shopId = shopId;
        conditionModel = this.queryCondition;
      } else {
        conditionModel = this.condition;
      }

      shopManageSvc
        .getShopServiceTypePagesAsync(conditionModel)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            this.getSpanArr(this.tableData);
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    sizeChange(val) {
      this.condition.pageIndex = this.currentPage = 1;
      this.condition.pageSize = val;
      this.getPaged();
    },
    pageTurning() {
      this.condition.pageIndex = this.currentPage;
      this.getPaged();
    },

    getPaged(flag) {
      this.tableLoading = true;

      console.log("condition: " + JSON.stringify(this.condition));
      shopManageSvc
        .getShopServiceTypePagesAsync(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            this.getSpanArr(this.tableData);
            if (flag) {
              if (res.message != "") {
                this.$message({
                  message: res.message,
                  type: "success"
                });
              }
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

    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    },

    cellMerge({ row, column, rowIndex, columnIndex }) {
      if (columnIndex === 0) {
        const _row = this.spanArr[rowIndex];
        const _col = _row > 0 ? 1 : 0;
        return {
          rowspan: _row,
          colspan: _col
        };
      }
    },
    getSpanArr(data) {
      this.spanArr = [];
      this.pos = undefined;
      for (var i = 0; i < data.length; i++) {
        if (i === 0) {
          this.spanArr.push(1);
          this.pos = 0;
        } else {
          // 判断当前元素与上一个元素是否相同
          if (data[i].shopId === data[i - 1].shopId) {
            this.spanArr[this.pos] += 1;
            this.spanArr.push(0);
          } else {
            this.spanArr.push(1);
            this.pos = i;
          }
        }
      }
    }
  }
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 88px;
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
