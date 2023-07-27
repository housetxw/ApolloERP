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
          <el-input
            class="input"
            clearable
            placeholder="请输入客户手机号"
            style="width: 160px"
            prefix-icon="el-icon-search"
            v-model="condition.userTel"
          >
          </el-input>
        </el-form-item>
        <el-select
          v-model="condition.shopId"
          filterable
          remote
          clearable
          reserve-keyword
          placeholder="请输入门店名称"
          :remote-method="getShopinfo"
          :loading="loading"
          style="width: 300px"
        >
          <el-option
            v-for="item in shopSel"
            :key="item.key"
            :label="item.value"
            :value="item.key"
          ></el-option>
        </el-select>
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="请输入优惠券规则Id"
            style="width: 170px"
            prefix-icon="el-icon-search"
            v-model="condition.couponRule"
          >
          </el-input>
        </el-form-item>
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="请输入优惠券活动Id"
            style="width: 170px"
            prefix-icon="el-icon-search"
            v-model="condition.couponActivity"
          >
          </el-input>
        </el-form-item>
        <el-form-item>
          <el-select
            style="width: 110px"
            v-model="condition.status"
            filterable
            placeholder="-请选择状态-"
          >
            <el-option
              v-for="item in userCouponStatusEnum"
              :key="item.id"
              :label="item.displayName"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            style="width: 110px"
            v-model="condition.grantMethod"
            filterable
            placeholder="-请选择发放方式-"
          >
            <el-option
              v-for="item in grantMethodEnum"
              :key="item.id"
              :label="item.displayName"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-tooltip
            class="item"
            effect="dark"
            content="领券时间范围"
            placement="bottom"
          >
            <el-form-item>
              <el-date-picker
                style="width: 128px"
                v-model="condition.startTime"
                type="date"
                placeholder="开始日期"
              ></el-date-picker
              >&nbsp;-
              <el-date-picker
                style="width: 128px"
                v-model="condition.endTime"
                type="date"
                placeholder="结束日期"
              ></el-date-picker>
            </el-form-item>
          </el-tooltip>
        </el-form-item>
      </template>

      <template v-slot:tb_cols>
        <el-table-column
          label="活动名称"
          prop="couponActivityName"
        ></el-table-column>
        <el-table-column
          label="活动Id"
          prop="couponActivityId"
          :width="80"
        ></el-table-column>
        <el-table-column label="优惠券" prop="couponRuleName"></el-table-column>
        <el-table-column
          label="优惠券Id"
          prop="couponRuleId"
          :width="80"
        ></el-table-column>
        <el-table-column
          label="客户名称"
          prop="userName"
          :width="100"
        ></el-table-column>
        <el-table-column
          label="客户手机号"
          prop="userTel"
          :width="100"
        ></el-table-column>
        <el-table-column
          label="发放方式"
          prop="grantMethodName"
        ></el-table-column>
        <el-table-column
          label="状态"
          prop="statusName"
          :width="120"
        ></el-table-column>
        <el-table-column
          label="开始有效时间"
          prop="startValidTime"
          :width="130"
        ></el-table-column>
        <el-table-column
          label="截止有效时间"
          prop="endValidTime"
          :width="130"
        ></el-table-column>
        <el-table-column
          label="发放时间"
          prop="createTime"
          :width="130"
        ></el-table-column>
        <el-table-column label="发放人" prop="createBy"></el-table-column>
        <el-table-column label="操作" :width="50">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                v-show="scope.row.status == 0"
                @click="invalidatedUserCoupon(scope.row.userCouponId)"
                type="danger"
                size="mini"
                >作废</el-button
              >
              <el-button
                v-show="scope.row.status != 1"
                @click="delayUserCoupon(scope.row.userCouponId)"
                type="primary"
                size="mini"
                >延期</el-button
              >
            </el-button-group>
          </template>
        </el-table-column>
      </template>
    </rg-page>
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { couponSvc } from "@/api/coupon/coupon";
import { appSvc } from "@/api/stock/stock";
export default {
  name: "grantrecord",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      condition: {
        pageIndex: 1,
        pageSize: 20,
        userTel: "",
        startTime: "",
        endTime: "",
        couponRuleId: 0,
        couponRule: "",
        couponActivityId: 0,
        couponActivity: "",
        status: -1,
        grantMethod: 0,
        shopId: undefined,
      },
      tableData: [],
      userCouponStatusEnum: [
        { id: -1, displayName: "全部状态" },
        { id: 0, displayName: "未使用" },
        { id: 1, displayName: "已使用" },
        { id: 2, displayName: "已过期" },
        { id: 3, displayName: "已作废" },
      ],
      grantMethodEnum: [
        { id: 0, displayName: "全部方式" },
        { id: 1, displayName: "新用户注册发放" },
        { id: 2, displayName: "系统自动触发" },
        { id: 3, displayName: "运营手动触发" },
        { id: 4, displayName: "用户自主领取" },
        { id: 5, displayName: "用户积分兑换" },
        { id: 6, displayName: "用户优惠码兑换" },
        { id: 7, displayName: "用户购买" },
        { id: 8, displayName: "门店发放" },
      ],
      loading: false,
      shopSelCondition: {
        simpleName: undefined,
      },
      shopSel: [],
    };
  },
  created() {
    this.fetchData();
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
      var r = /^\+?[1-9][0-9]*$/; //正整数
      if (!r.test(this.condition.couponRule)) {
        this.condition.couponRuleId = 0;
      } else {
        this.condition.couponRuleId = Number(this.condition.couponRule);
      }
      if (!r.test(this.condition.couponActivity)) {
        this.condition.couponActivityId = 0;
      } else {
        this.condition.couponActivityId = Number(this.condition.couponActivity);
      }
debugger;
      if (this.condition.shopId == undefined || this.condition.shopId == "") {
        this.condition.shopId = 0;
      }

      this.tableLoading = true;
      couponSvc
        .getUserCouponGrantRecord(this.condition)
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
          this.condition.shopId = undefined;
        });
    },
    invalidatedUserCoupon(userCouponId) {
      this.$confirm("确认作废吗？", "提示", {
        confirmButtonText: "确认",
        cancelButtonText: "取消",
        closeOnClickModal: false,
        type: "warning",
      })
        .then(() => {
          var deleteRequest = {
            userCouponId: userCouponId,
          };
          couponSvc
            .invalidatedUserCoupon(deleteRequest)
            .then(
              (res) => {
                var data = res.data;
                if (data) {
                  this.$message({ message: "作废成功", type: "success" });
                } else {
                  this.$message({ message: "作废失败", type: "error" });
                }
                this.getPaged();
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally(() => {});
        })
        .catch(() => {});
    },
    delayUserCoupon(userCouponId) {
      this.$confirm("确认延期吗？", "提示", {
        confirmButtonText: "确认",
        cancelButtonText: "取消",
        closeOnClickModal: false,
        type: "warning",
      })
        .then(() => {
          var delayRequest = {
            userCouponId: userCouponId,
            dayNum: 0
          };
          couponSvc
            .delayUserCoupon(delayRequest)
            .then(
              (res) => {
                var data = res.data;
                if (data) {
                  this.$message({ message: "延期成功", type: "success" });
                } else {
                  this.$message({ message: "延期失败", type: "error" });
                }
                this.getPaged();
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally(() => {});
        })
        .catch(() => {});
    },
    getShopinfo(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.simpleName = query;
          appSvc
            .getShopList(this.shopSelCondition)
            .then(
              (res) => {
                this.shopSel = res.data;
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
  },
};
</script>
