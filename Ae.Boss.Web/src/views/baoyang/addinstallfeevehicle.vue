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
          <span style="color: red; font-size: 12px">*</span>
          <el-select
            v-model="condition.brand"
            @change="getVehicleByBrand()"
            filterable
            style="width: 120px"
            placeholder="品牌"
          >
            <el-option
              v-for="item in brandList"
              :key="item.brand"
              :label="item.brand"
              :value="item.brand"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <span style="color: red; font-size: 12px">*</span>
          <el-select
            v-model="condition.vehicleId"
            @change="getPaiLiangByVehicleId()"
            filterable
            style="width: 150px"
            placeholder="车系"
          >
            <el-option
              v-for="item in vehicleList"
              :key="item.vehicleId"
              :label="item.vehicle"
              :value="item.vehicleId"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <span style="color: red; font-size: 12px">*</span>
          <el-select
            v-model="condition.paiLiang"
            @change="getVehicleNianByPaiLiang()"
            filterable
            style="width: 90px"
            placeholder="排量"
          >
            <el-option
              v-for="item in paiLiangList"
              :key="item"
              :label="item"
              :value="item"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.nian"
            @clear="
              tid = '';
              salesNameList = [];
            "
            @change="getVehicleSalesNameByNian()"
            clearable
            filterable
            style="width: 100px"
            placeholder="生产年份"
          >
            <el-option
              v-for="item in nianList"
              :key="item"
              :label="item"
              :value="item"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.tid"
            clearable
            filterable
            style="width: 180px"
            placeholder="车款"
          >
            <el-option
              v-for="item in salesNameList"
              :key="item.tid"
              :label="item.salesName"
              :value="item.tid"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            style="width: 200px"
            v-model="condition.serviceId"
            filterable
            clearable
            placeholder="安装服务"
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
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
          >配置</el-button
        >
      </template>
      <template v-slot:tb_cols>
        <el-table-column label="TID" prop="tid" width="60"></el-table-column>
        <el-table-column
          label="VehicleID"
          prop="vehicleId"
          width="120"
        ></el-table-column>
        <el-table-column label="品牌" prop="brand" width="60"></el-table-column>
        <el-table-column
          label="车系"
          prop="vehicle"
          width="60"
        ></el-table-column>
        <el-table-column
          label="排量"
          prop="paiLiang"
          width="60"
        ></el-table-column>
        <el-table-column
          label="发动机型号"
          prop="engineNo"
          width="90"
        ></el-table-column>
        <el-table-column label="生产年份" prop="packagePrice" width="80">
          <template slot-scope="scope">
            {{ scope.row.listedYear }}~{{ scope.row.stopProductionYear }}
          </template>
        </el-table-column>
        <el-table-column label="年款" prop="nian" :width="60"></el-table-column>
        <el-table-column
          label="版型"
          prop="salesName"
          width="220"
        ></el-table-column>
        <el-table-column
          align="right"
          label="指导价(万)"
          prop="guidePrice"
          width="80"
        ></el-table-column>
        <el-table-column label="安装服务">
          <template slot-scope="scope">
            {{ scope.row.serviceId }} 【{{ scope.row.serviceName }}】 【￥{{
              scope.row.originalPrice
            }}】
          </template>
        </el-table-column>
        <el-table-column label="加价金额" :width="80" align="right">
          <template slot-scope="scope">
            <span style="color: red" v-show="scope.row.installAddFeeId > 0">
              ￥{{ scope.row.additionalPrice }}
            </span>
          </template>
        </el-table-column>
        <el-table-column
          label="加价备注"
          prop="remark"
          width="100"
        ></el-table-column>
      </template>
    </rg-page>
  </main>
</template>
<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/baoyang/baoyang";
export default {
  name: "addinstallfeevehicle",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      condition: {
        pageIndex: 1,
        pageSize: 50,
        serviceId: "",
        brand: "",
        vehicleId: "",
        paiLiang: "",
        nian: "",
        tid: "",
      },
      installService: [],
      tableData: [],
      brandList: [],
      vehicleList: [],
      paiLiangList: [],
      nianList: [],
      salesNameList: [],
    };
  },
  created() {
    //this.fetchData();
    this.getBrandList();
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
        .getVehicleInstallAddFee(this.condition)
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
      this.$router.push({ path: `addinstallfee` });
    },
    getBrandList() {
      appSvc
        .getBrandList()
        .then(
          (res) => {
            var data = res.data;
            this.brandList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getVehicleByBrand() {
      if (this.condition.brand == "") {
        return;
      }
      this.condition.vehicleId = "";
      this.condition.paiLiang = "";
      this.condition.nian = "";
      this.condition.tid = "";
      var vehicleCondition = { brand: this.condition.brand };
      appSvc
        .getListByBrand(vehicleCondition)
        .then(
          (res) => {
            var data = res.data;
            this.vehicleList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getPaiLiangByVehicleId() {
      if (this.condition.vehicleId == "") {
        return;
      }
      this.condition.paiLiang = "";
      this.condition.nian = "";
      this.condition.tid = "";
      var paiLiangRequest = { vehicleId: this.condition.vehicleId };
      appSvc
        .getPaiLiangByVehicleId(paiLiangRequest)
        .then(
          (res) => {
            var data = res.data;
            this.paiLiangList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getVehicleNianByPaiLiang() {
      if (this.condition.vehicleId == "" || this.condition.paiLiang == "") {
        return;
      }
      this.condition.nian = "";
      this.condition.tid = "";
      var nianRequest = {
        vehicleId: this.condition.vehicleId,
        paiLiang: this.condition.paiLiang,
      };
      appSvc
        .getVehicleNianByPaiLiang(nianRequest)
        .then(
          (res) => {
            var data = res.data;
            this.nianList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getVehicleSalesNameByNian() {
      if (
        this.condition.vehicleId == "" ||
        this.condition.paiLiang == "" ||
        this.condition.nian == ""
      ) {
        return;
      }
      this.condition.tid = "";
      var tidRequest = {
        vehicleId: this.condition.vehicleId,
        paiLiang: this.condition.paiLiang,
        nian: this.condition.nian,
      };
      appSvc
        .getVehicleSalesNameByNian(tidRequest)
        .then(
          (res) => {
            var data = res.data;
            this.salesNameList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
  },
};
</script>