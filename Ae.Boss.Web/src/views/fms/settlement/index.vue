<template>
  <div class="child4">
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
          :defaultCollapse="false"
          :cell-style="cellStyle"
        >
          <template v-slot:condition>
            <el-form-item prop="shopId">
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
            </el-form-item>
            <el-form-item prop="provinceId">
              <el-select
                v-model="condition.provinceId"
                clearable
                filterable
                placeholder="请选择省"
                @change="getCity"
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
              >
                <el-option
                  v-for="item in districtSel"
                  :key="item.regionId"
                  :label="item.name"
                  :value="item.regionId"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item prop="settlementTimes" label="结算日期">
              <template>
                <el-date-picker
                  v-model="condition.settlementTimes"
                  type="datetimerange"
                  :picker-options="sendpickerOptions"
                  range-separator="至"
                  start-placeholder="开始日期"
                  end-placeholder="结束日期"
                  align="right"
                  style="width: 260px"
                ></el-date-picker>
              </template>
            </el-form-item>
            <el-form-item>
              <el-checkbox-group v-model="checkboxGroup1">
                <el-input
                  v-model="condition.checkUser"
                  clearable
                  placeholder="请输入核对人"
                  style="width: 200px; padding-right: 10px"
                />
                <el-checkbox-button
                  v-for="status in payStatuss"
                  :label="status"
                  :key="status"
                  >{{ status }}</el-checkbox-button
                >
              </el-checkbox-group>
            </el-form-item>
          </template>

          <!-- <el-input v-model="condition.name" clearable class="el-input el-input--suffix" placeholder="请输入系统名称"></el-input> -->

          <template v-slot:tb_cols>
            <el-table-column
              label="结算批次"
              width="150px"
              prop="settlementBatchNo"
            ></el-table-column>
            <el-table-column
              label="门店名称"
              width="180px"
              prop="locationName"
            ></el-table-column>
             <el-table-column
              label="本期对账金额"
              width="125px"
              prop="billAmount"
            ></el-table-column>
            <el-table-column
              label="付款状态"
              width="120px"
              prop="statusStr"
            ></el-table-column>
            <el-table-column
              label="银行名称"
              width="180px"
              prop="bankName"
            ></el-table-column>
            <el-table-column
              label="开户支行"
              width="180px"
              prop="bankBranch"
            ></el-table-column>
            <el-table-column
              label="开户人姓名"
              width="180px"
              prop="bankUser"
            ></el-table-column>
            <el-table-column
              label="银行账号"
              width="180px"
              prop="bankNo"
            ></el-table-column>
            <el-table-column
              label="对账日期"
              width="120px"
              prop="createTimeStr"
            ></el-table-column>
            <el-table-column
              label="对账人"
              width="120px"
              prop="checkUser"
            ></el-table-column>
            <el-table-column
              label="申请日期"
              width="120px"
              prop="applyTimeStr"
            ></el-table-column>
            <el-table-column
              label="申请人"
              width="150px"
              prop="applyUser"
            ></el-table-column>
            <el-table-column
              label="付款备注"
              prop="topRemark"
            ></el-table-column>

            <el-table-column label="操作" width="180px" fixed="right">
              <template slot-scope="scope">
                <el-button
                  type="text"
                  size="small"
                  :disabled="scope.row.status == 2 ? true : false"
                  @click="settlementHandle(scope.row)"
                  >付款处理</el-button
                >

                <el-button
                  type="text"
                  size="small"
                  @click="handleLog(scope.row)"
                  >操作详情</el-button
                >

                <router-link
                  :to="{
                    path: 'detail',
                    query: { settlementBatchNo: scope.row.settlementBatchNo },
                  }"
                >
                  <el-button type="text" size="small">详情</el-button>
                  <!-- <a href="" style="font-size:14px;color:#409EFF;">详情</a> -->
                </router-link>
              </template>
            </el-table-column>
          </template>
        </rg-page>
      </section>
      <!-- 首页 -->

      <!-- 标记异常窗口 -->
      <section id="settlementhandleDialog">
        <rg-dialog
          :title="settlementFormTitle"
          :visible.sync="settlementFormVisible"
          :beforeClose="settlementCancel"
          :btnCancel="{
            label: '关闭',
            click: (done) => {
              settlementCancel();
            },
          }"
          :btnRemove="false"
          :footbar="true"
          width="80%"
          maxWidth="800px"
          minWidth="600px"
          :close-on-click-modal="false"
        >
          <template v-slot:content>
            <el-form
              :model="settlementFormModel"
              ref="settlementFormModel"
              :label-width="formLabelWidth"
            >
              <el-form-item label="门店名称：">
                 <span>{{settlementInfo.locationName || ''}}</span>
              </el-form-item>
              <el-form-item label="银行名称：">
                 <span>{{settlementInfo.bankName || ''}}</span>
              </el-form-item>
              <el-form-item label="开户支行：">
                 <span>{{settlementInfo.bankBranch || ''}}</span>
              </el-form-item>
              <el-form-item label="开户人姓名：">
                 <span>{{settlementInfo.bankUser || ''}}</span>
              </el-form-item>
              <el-form-item label="银行卡号：">
                 <span>{{settlementInfo.bankNo || ''}}</span>
              </el-form-item>
              <el-form-item label="本期对账金额：">
                 <span>{{settlementInfo.billAmount || ''}}</span>
              </el-form-item>
              <el-form-item label="处理意见：">
                <el-input
                  type="textarea"
                  :rows="5"
                  placeholder="请输入处理意见"
                  v-model="settlementFormModel.remark"
                ></el-input>
              </el-form-item>
            </el-form>
          </template>
          <template v-slot:footer>
            <el-button
              type="primary"
              @click="settlementSave('settlementFormModel', 'pass')"
              size="mini"
              >已付款</el-button
            >
            <el-button
              type="warning"
              @click="settlementSave('settlementFormModel', 'reject')"
              size="mini"
              >付款失败</el-button
            >
          </template>
        </rg-dialog>
      </section>
      <!-- 标记异常窗口 -->

      <!-- 查看操作日志窗口 -->
      <section id="accoutlog">
        <rg-dialog
          :title="logFormTitle"
          :visible.sync="logFormVisible"
          :beforeClose="logCancel"
          :btnCancel="{
            label: '关闭',
            click: (done) => {
              logCancel();
            },
          }"
          :btnRemove="false"
          :footbar="true"
          width="80%"
          maxWidth="800px"
          minWidth="600px"
          :close-on-click-modal="false"
        >
          <template v-slot:content>
            <el-table
              border
              :data="logtableData"
              style="width: 100%"
              :cell-style="cellStyle"
            >
              <el-table-column
                label="编号"
                prop="id"
                width="80px;"
              ></el-table-column>
              <el-table-column label="备注" prop="remark"></el-table-column>
              <el-table-column
                label="操作人"
                prop="createBy"
                width="80px;"
              ></el-table-column>
              <el-table-column
                label="操作时间"
                prop="createTime"
                width="150px;"
              ></el-table-column>
            </el-table>
          </template>
        </rg-dialog>
      </section>
      <!-- 查看操作日志窗口 -->
    </main>
  </div>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appSvc } from "@/api/fms/settlement";

import { isNumber } from "util";
const statusOptions = ["未付款", "已付款", "付款失败"];

export default {
  name: "child4",
  mounted() {
    console.log("tab1组件");
  },
  data() {
    return {
      payStatuss: statusOptions,
      checkboxGroup1: ["未付款"],
      pickerOptionsStart: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        },
      },

      sendpickerOptions: {
        // disabledDate(time) {
        //   return time.getTime() < Date.now();
        // },
        shortcuts: [
          {
            text: "最近一周",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit("pick", [start, end]);
            },
          },
          {
            text: "最近一个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit("pick", [start, end]);
            },
          },
          {
            text: "最近三个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit("pick", [start, end]);
            },
          },
        ],
      },
      input: undefined,
      actualInStockNum: 1,
      readonly: true,
      tableLoading: false,
      currentPage: 1,
      loading: false,
      //table页面的条件
      condition: {
        pageIndex: 1,
        pageSize: 10,
        provinceId: undefined,
        cityId: undefined,
        districtId: undefined,
        pageIndex: 1,
        pageSize: 10,
        checkUser: undefined,
        settlementTimes: undefined,
        shopId: undefined,
        payStatuss: undefined,
      },
      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",

      formLabelWidth: "180px",
      provinceSel: [],

      citySel: [],
      districtSel: [],
      regionCondition: {
        regionId: undefined,
      },
      shopSel: [],
      shopSelCondition: {
        simpleName: undefined,
      },

      settlementInfo:{},
      settlementFormModel: {
        settlementBatchNo: undefined,
        remark: undefined,
        status: undefined,
      },

      settlementFormModelInit: {
        settlementBatchNo: undefined,
        remark: undefined,
        status: undefined,
      },

      settlementFormTitle: "",
      settlementFormVisible: false,

      logFormTitle: "",
      logFormVisible: false,
      logCondition: {
        relationNo: undefined,
      },
      logtableData: [],
    };
  },
  computed: {
    routeComputed: {
      get: function () {
        return this.formModel.route;
      },
      set: function (val) {
        this.formModel.route = val.toLowerCase();
      },
    },
  },
  created() {
    //页面初始化函数
    // console.log("param:" + JSON.stringify(this.$route.query));
    // this.condition.locationId = this.$route.query.locationId;
    this.fetchData();
  },
  methods: {
    logCancel() {
      this.logFormVisible = false;
    },
    handleLog(row) {
      this.logFormTitle = "【" + row.settlementBatchNo + "】的操作日志";
      this.logFormVisible = true;

      this.logCondition.relationNo = row.settlementBatchNo;
      appSvc
        .getAccountCheckLogs(this.logCondition)
        .then(
          (res) => {
            this.logtableData = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
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
            (res) => {
              this.citySel = res.data;
            },
            (error) => {
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
            (res) => {
              this.districtSel = res.data;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },

    updateLocation() {
      if (this.condition.searchLocationId == "") {
        this.condition.searchLocationId = undefined;
        this.condition.locationId = this.$route.query.locationId;
      }
    },
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
              (res) => {
                var resData = res.data;
                this.shopSel = resData.items;
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        }, 200);
      } else {
        this.options = [];
      }
    },
    settlementHandle(row) {
      this.settlementFormTitle = "付款处理";
      this.settlementFormVisible = true;
      this.settlementInfo = row
      this.settlementFormModel.settlementBatchNo = row.settlementBatchNo;
    },
    settlementSave(formName, type) {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          this.settlementFormModel.status = 2;
          if (type == "reject") {
            if (
              this.settlementFormModel.remark == "" ||
              this.settlementFormModel.remark == undefined
            ) {
              this.$message({
                message: "请填写付款失败原因!",
                type: "warning",
              });
              return;
            } else {
              this.settlementFormModel.status = 1;
            }
          }

          debugger;

          var vm = this;
          console.log("formModel: " + JSON.stringify(this.settlementFormModel));

          appSvc
            .saveSettlementReview(this.settlementFormModel)
            .then(
              (res) => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                  vm.search();
                  this.settlementFormVisible = false;
                  this.settlementInfo = {};
                  this.settlementFormModel = JSON.parse(
                    JSON.stringify(this.settlementFormModelInit)
                  );
                } else {
                  this.$message({
                    message: res.message,
                    type: "warning",
                  });
                }
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },
    settlementCancel(formName) {
      this.settlementFormVisible = false;
      this.settlementInfo = {};
      this.settlementFormModel = JSON.parse(
        JSON.stringify(this.settlementFormModelInit)
      );
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
    fetchData() {
      this.condition.payStatuss = this.checkboxGroup1;
      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getSettlementList(this.condition)
        .then(
          (res) => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;

            if (flag) {
              if (res.message != "") {
                this.$message({
                  message: res.message,
                  type: "success",
                });
              }
            }
          },
          (error) => {
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
          (res) => {
            this.provinceSel = res.data;
          },
          (error) => {
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
      this.condition.payStatuss = this.checkboxGroup1;
      debugger;

      if (this.condition.shopId == "") {
        this.condition.shopId = undefined;
      }

      if (this.condition.provinceId == "") {
        this.condition.provinceId = undefined;
      }

      if (this.condition.cityId == "") {
        this.condition.cityId = undefined;
      }

      if (this.condition.districtId == "") {
        this.condition.districtId = undefined;
      }

      if (this.condition.checkUser == "") {
        this.condition.checkUser = undefined;
      }

      //选择了省必须选择市
      if (
        this.condition.provinceId != null &&
        this.condition.provinceId != undefined &&
        (this.condition.cityId == null || this.condition.cityId == undefined)
      ) {
        this.$message({
          message: "请选择市!",
          type: "warning",
        });
        return;
      }

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getSettlementList(this.condition)
        .then(
          (res) => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;

            if (flag) {
              if (res.message != "") {
                this.$message({
                  message: res.message,
                  type: "success",
                });
              }
            }
          },
          (error) => {
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
  },
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 150px;
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
>>> .el-form--inline .el-form-item {
  padding-bottom: 10px !important ;
}
</style>
