<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <rg-page
        background
        id="indexPage"
        :pageIndex="condition.pageIndex"
        :pageSize="condition.pageSize"
        :dataCount="totalList"
        :tableLoading="tableLoading"
        :tableData="tableData"
        :pageChange="pageTurning"
        :headerBtnWidth="180"
        :searching="search"
        :data="tableData"
        :cell-style="cellStyle"
      >
        <template v-slot:condition>
          <el-form-item prop="searchLocationId">
            <el-select
              v-model="condition.searchLocationId"
              @change="updateLocation"
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
          </el-form-item>
          <el-form-item prop="provinceId">
            <el-select
              v-model="condition.provinceId"
              clearable
              filterable
              placeholder="请选择省"
              @change="getCity"
              class="margin-right-10"
            >
              <el-option
                v-for="item in provinceSel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item prop="cityId">
            <el-select
              v-model="condition.cityId"
              clearable
              filterable
              @change="getDistrict"
              placeholder="请选择市"
              class="margin-right-10"
            >
              <el-option
                v-for="item in citySel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item prop="districtId">
            <el-select
              v-model="condition.districtId"
              clearable
              filterable
              placeholder="请选择区"
              class="margin-right-10"
            >
              <el-option
                v-for="item in districtSel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>
        </template>
        <template v-slot:tb_cols>
          <el-table-column label="门店编号" width="100px" prop="shopId"></el-table-column>
          <el-table-column label="门店名称" width="240px" prop="shopName"></el-table-column>
          <!-- <el-table-column label="总部已对账应结算" width="150px" prop="accountedSettlement"></el-table-column>
          <el-table-column label="总部未对账应结算" prop="unAccountedSettlement"></el-table-column>
          <el-table-column label="小计" prop="subtotal"></el-table-column> -->

          <el-table-column label="门店未对账" prop="shopUnAccountNum"></el-table-column>
          <el-table-column label="门店已对账" prop="shopAccountNum"></el-table-column>

          <el-table-column label="总部已核对" prop="rgAccountNum"></el-table-column>
          <el-table-column label="对账异常" prop="accountErrorNum"></el-table-column>

          <el-table-column label="操作" width="100px" fixed="right">
            <template slot-scope="scope">
              <router-link
                :to="{
                    path: 'accountcheckindex',
                    query: { locationId: scope.row.shopId }
                  }"
              >
                <a href style="font-size:14px;color: #409EFF;">详情</a>
              </router-link>
            </template>
          </el-table-column>
        </template>
      </rg-page>
    </section>
  </main>
</template>


<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appSvc } from "@/api/fms/accountcheck";

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
      actualInStockNum: 1,
      readonly: true,
      tableLoading: false,
      currentPage: 1,

      provinceSel: [],

      citySel: [],
      districtSel: [],
      regionCondition: {
        regionId: undefined
      },

      //table页面的条件
      condition: {
        shopName: undefined,
        provinceId: undefined,
        cityId: undefined,
        districtId: undefined,
        pageIndex: 1,
        pageSize: 20,
        searchLocationId: undefined,
        shopId: undefined
      },
      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",

      formLabelWidth: "120px",
      shopSel: [],
      loading: false,
      shopSelCondition: {
        simpleName: undefined
      },
      logFormTitle: "",
      logFormVisible: false,
      logCondition: {
        relationNo: undefined
      },
      logtableData: []
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
    this.fetchData();
  },
  methods: {
    getShopinfo(query) {
      if (query != "") {
        debugger;
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.simpleName = query;
          appSvc
            .getShopListAsync(this.shopSelCondition)
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
    cellStyle(row, column, rowIndex, columnIndex) {
      if (row.row.priority === "紧急" && row.column.label === "优先级") {
        return "color:red";
      }

      if (row.row.status === "驳回" && row.column.label === "状态") {
        return "color:orange";
      }
      // else if(row.column.label==="告警级别"&& row.row.alarmLevel==="一般告警" ){
      //   return 'color:yellow'
      // }
    },

    updateLocation() {
      if (this.condition.searchLocationId == "") {
        this.condition.searchLocationId = undefined;
        this.condition.shopId = undefined;
      }
    },
    updateOrderId() {
      if (this.condition.orderNo == "") {
        this.condition.orderNo = undefined;
      }
    },
    logCancel() {
      this.logFormVisible = false;
    },

    markExceptionCancel() {
      this.markExceptionFormVisible = false;

      this.markExceptionFormModel = JSON.parse(
        JSON.stringify(this.markExceptionFormModelInit)
      );
    },

    getCity() {
      debugger;
      if (
        this.condition.provinceId != "" &&
        this.condition.provinceId != undefined
      ) {
        this.regionCondition.regionId = this.condition.provinceId;
        appSvc
          .getRegionChinaListByRegionId(this.regionCondition)
          .then(
            res => {
              this.citySel = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },

    getDistrict() {
      if (this.condition.cityId != "" && this.condition.cityId != undefined) {
        this.regionCondition.regionId = this.condition.cityId;

        appSvc
          .getRegionChinaListByRegionId(this.regionCondition)
          .then(
            res => {
              this.districtSel = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },
    fetchData() {
      //需要切换接口
      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getAccountCheckCollects(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;

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

      appSvc
        .getRegionChinaListByRegionId(this.regionCondition)
        .then(
          res => {
            this.provinceSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },


    getPaged(flag) {
      this.tableLoading = true;
      if (
        this.condition.searchLocationId != "" &&
        this.condition.searchLocationId != undefined
      ) {
        this.condition.shopId = this.condition.searchLocationId;
      }

      //选择了省必须选择市
      if (
        this.condition.provinceId != null &&
        this.condition.provinceId != undefined &&
        (this.condition.cityId == null || this.condition.cityId == undefined)
      ) {
        this.$message({
          message: "请选择市!",
          type: "warning"
        });
        return;
      }
this.tableLoading = true;
      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getAccountCheckCollects(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;

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
