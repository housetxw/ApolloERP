<template>
  <div class="container">
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
      :searching="searchPromotionActivity"
      :defaultCollapse="false"
    >
      <template v-slot:condition>
        <el-form-item prop="name">
          <el-input
            class="input"
            clearable
            placeholder="请输入活动名称"
            prefix-icon="el-icon-search"
            v-model="condition.name"
          ></el-input>
        </el-form-item>
        <!-- <el-form-item prop="优惠卷名称">
          <el-input
            class="input"
            clearable
            placeholder="请输入y"
            prefix-icon="el-icon-search"
            v-model="condition.couponName"
          ></el-input>
        </el-form-item> -->
      </template>

      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="toAdd"
          >新增</el-button
        >
      </template>

      <template v-slot:tb_cols>
        <el-table-column label="编号" prop="id" ></el-table-column>
        <el-table-column
          label="门店编号"
          prop="shopId"
          width="200"
        ></el-table-column>
        <el-table-column
          label="活动Id"
          prop="couponActivityId"
        ></el-table-column>

        <el-table-column label="活动名称" prop="name" width="200"></el-table-column>
        <el-table-column label="优惠卷名称" prop="couponName" width="200"></el-table-column>
        <el-table-column label="发放总数" prop="totalNum"></el-table-column>
        <el-table-column label="领取总数" prop="receiveNum"></el-table-column>
        <el-table-column label="兑换码" prop="code"></el-table-column>
        <el-table-column
          label="开始时间"
          prop="startTime"
          width="140"
        ></el-table-column>
        <el-table-column
          label="结束时间"
          prop="endTime"
          width="140"
        ></el-table-column>
        <el-table-column label="活动状态" prop="statusDisplay" width="80">
          <template slot-scope="scope">
            <span>{{
              { 0: "未发布", 1: "可领取", 2: "暂停领取", 3: "已作废" }[
                scope.row.status
              ] || ""
            }}</span>
            <span
              style="color: red"
              v-if="
                Date.parse(scope.row.startTime) > new Date() &&
                scope.row.status == 1
              "
            >
              （未开始）
            </span>
            <span
              style="color: red"
              v-else-if="
                Date.parse(scope.row.endTime) < new Date() &&
                scope.row.status == 1
              "
            >
              （已结束）
            </span>
          </template>
        </el-table-column>
        <!-- <el-table-column
          label="创建人"
          prop="createdBy"
          width="100"
        ></el-table-column> -->
        <el-table-column fixed="right" label="操作">
          <template slot-scope="scope">
            <el-button size="mini" type="primary" @click="detail(scope.row)"
              >详情</el-button
            >
          </template>
        </el-table-column>
      </template>
    </rg-page>

    <rg-dialog
      :title="formTitle"
      :visible.sync="dialogVisible"
      :beforeClose="cancel"
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          cancel();
        },
      }"
      :btnRemove="false"
      :footbar="true"
      width="80%"
      maxWidth="600px"
      minWidth="600px"
    >
      <template v-slot:content>
        <el-form :model="currentDetail" size="mini" label-width="120px">
          <el-form-item label="促销门店">
            <el-input
              style="width: 300px"
              v-model="currentDetail.shopIds"
              placeholder="门店Id以,分割示例：7,8"
            ></el-input>
          </el-form-item>
          <el-form-item label="活动名称">
            <el-select
              v-model="currentDetail.couponActivityId"
              filterable
              remote
              clearable
              reserve-keyword
              placeholder="活动名称"
              :remote-method="getActivityList"
              :loading="loading"
              @change="getCouponInfo"
              style="width: 300px"
            >
              <el-option
                v-for="item in activitySel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="优惠卷名称">
            <!-- <el-select
              v-model="currentDetail.couponId"
              filterable
              remote
              clearable
              reserve-keyword
              placeholder="优惠卷名称"
              :remote-method="getCouponList"
              :loading="loading"
              :change="getCouponInfo"
              style="width: 300px"
            >
              <el-option
                v-for="item in couponSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select> -->
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.couponName"
            ></el-input>
          </el-form-item>
          <el-form-item label="活动开始时间">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.startTime"
            ></el-input>
          </el-form-item>
          <el-form-item label="活动结束时间">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.endTime"
            ></el-input>
          </el-form-item>
          <el-form-item label="状态">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.statusName"
            ></el-input>
          </el-form-item>
          <el-form-item label="兑换码">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.code"
            ></el-input>
          </el-form-item>
        </el-form>
      </template>
      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          size="mini"
          type="primary"
          :loading="loading"
          @click="submitData()"
          >提交</el-button
        >
        <el-button
          size="mini"
          type="danger"
          :loading="loading"
          v-show="currentDetail.id > 0"
          @click="deleteData()"
          >作废</el-button
        >
      </template>
    </rg-dialog>
  </div>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/product/promotion";
import { couponSvc } from "@/api/coupon/coupon";
export default {
  name: "promotionactivitylist",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      condition: {
        name: "",
        // activityId: "",
        // productCode: "",
        // startDate: "",
        // endDate: "",
        // status: 0,
        pageIndex: 1,
        pageSize: 10,
      },
      tableData: [],
      promotionStatus: [
        {
          displayName: "全部",
          value: 0,
        },
        {
          displayName: "进行中",
          value: 4,
        },
        {
          displayName: "未开始",
          value: 3,
        },
        {
          displayName: "已结束",
          value: 5,
        },
        {
          displayName: "待审核",
          value: 1,
        },
        {
          displayName: "已拒绝",
          value: 2,
        },
      ],
      formTitle: "门店促销配置",
      dialogVisible: false,
      currentDetail: {
        id: 0,
        couponActivityId: 0,
        name: "",
        couponId: 0,
        couponName: "",
        startTime: "",
        endTime: "",
        statusName: "",
        code: "",
        status: 0,
        shopIds: "",
        isDeleted: 0,
      },
      loading: false,
      activitySelCondition: {
        name: undefined,
        status: 1,
      },
      activitySel: [],
      couponSelCondition: {
        displayName: undefined,
      },
      couponSel: [],
    };
  },
  created() {
    this.getPaged();
  },
  methods: {
    toAdd() {
      this.dialogVisible = true;
      this.currentDetail.shopIds = "";
      this.currentDetail.couponActivityId = undefined;
      this.currentDetail.couponName = "";
      this.currentDetail.startTime = "";
      this.currentDetail.endTime = "";
      this.currentDetail.code = "";
      this.currentDetail.id = 0;
      this.currentDetail.statusName = "";
    },
    searchPromotionActivity() {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(true);
    },
    getPaged(flag) {
      console.log("condition: " + JSON.stringify(this.condition));
      this.tableLoading = true;
      appSvc
        .GetCouponActivityListForShop(this.condition)
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
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },

    routeDetail(activityParaId) {},
    auditPromotion(activityParaId) {
      var auditRequest = {
        activityId: activityParaId,
      };
      appSvc
        .auditPromotionActivity(auditRequest)
        .then(
          (res) => {
            var data = res.data;
            if (data) {
              this.$message({ message: "操作成功", type: "success" });
            } else {
              this.$message({ message: "操作失败", type: "warning" });
            }
            this.getPaged(false);
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
    cancel() {
      this.dialogVisible = false;
    },
    getActivityList(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.activitySelCondition.name = query;
          couponSvc
            .getCouponActivityList(this.activitySelCondition)
            .then(
              (res) => {
                let items = res.data;
                if (res.data != null) {
                  if (res.data.items != null) {
                    for (let i = 0; i < res.data.items.length; i++) {
                      this.activitySel.push({
                        key: res.data.items[i].couponActivityId,
                        value: res.data.items[i].name,
                      });
                    }
                  }
                }
                // this.activitySel = res.data;
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        }, 200);
      } else {
        // this.options = [];
      }
    },
    getCouponList(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.couponSelCondition.displayName = query;
          couponSvc
            .getCouponRuleList(this.couponSelCondition)
            .then(
              (res) => {
                let items = res.data;
                if (res.data != null) {
                  if (res.data.items != null) {
                    for (let i = 0; i < res.data.items.length; i++) {
                      this.couponSel.push({
                        key: res.data.items[i].couponId,
                        value: res.data.items[i].displayName,
                      });
                    }
                  }
                }
                // this.activitySel = res.data;
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        }, 200);
      } else {
        // this.options = [];
      }
    },
    getCouponInfo(value) {
      // alert(1);
      // this.loading = true;
      // setTimeout(() => {
      debugger;
      // this.loading = false;

      if (value != undefined && value > 0) {
        couponSvc
          .getCouponActivityList({
            couponActivityId: value,
            status: -1,
          })
          .then(
            (res) => {
              if (res.data != null) {
                if (res.data.items != null) {
                  debugger;
                  for (let i = 0; i < res.data.items.length; i++) {
                    this.currentDetail.startTime = res.data.items[i].startTime;
                    this.currentDetail.endTime = res.data.items[i].endTime;
                    this.currentDetail.statusName =
                      res.data.items[i].statusName;
                    this.currentDetail.code = res.data.items[i].code;
                    this.currentDetail.couponName =
                      res.data.items[i].couponName;

                    this.activitySel.push({
                      key: res.data.items[i].couponActivityId,
                      value: res.data.items[i].name,
                    });
                  }
                }
              }
              // this.activitySel = res.data;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      }

      // }, 200);
    },
    submitData() {
      if (this.currentDetail.shopIds == "") {
        this.$message({ message: "请输入门店", type: "warning" });
        return;
      }
      // if(this.currentDetail.shopIds!=""){
      //  let splitData= this.currentDetail.shopIds.split("，");
      //  if(splitData.length>0)
      // }

      this.loading = true;
      appSvc
        .SaveShopGrantCoupon({
          id: this.currentDetail.id,
          activityId: this.currentDetail.couponActivityId,
          shopId: this.currentDetail.shopIds,
          isDeleted: this.currentDetail.isDeleted,
        })
        .then(
          (res) => {
            var data = res.data;
            this.$message({ message: "提交成功", type: "success" });
            this.dialogVisible = false;
            this.getPaged();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.loading = false;
        });
    },
    deleteData() {
      this.loading = true;
      appSvc
        .SaveShopGrantCoupon({
          id: this.currentDetail.id,
          isDeleted: 1,
          activityId: this.currentDetail.couponActivityId,
          shopId: 0,
        })
        .then(
          (res) => {
            var data = res.data;
            this.$message({ message: "作废成功", type: "success" });
            this.dialogVisible = false;
            this.getPaged();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.loading = false;
        });
    },
    detail(value) {
      this.dialogVisible = true;
      this.currentDetail.shopIds = value.shopId;
      this.currentDetail.couponActivityId = value.couponActivityId;
      this.currentDetail.cou;
      this.currentDetail.couponName = value.couponName;
      this.currentDetail.startTime = value.startTime;
      this.currentDetail.endTime = value.endTime;
      this.currentDetail.code = value.code;
      this.currentDetail.id = value.id;
      debugger;
      if (value.status == 0) {
        this.currentDetail.statusName = "未发布";
      } else if (value.status == 1) {
        this.currentDetail.statusName = "可领取";
      } else if (value.status == 1) {
        this.currentDetail.statusName = "暂停领取";
      } else if (value.status == 1) {
        this.currentDetail.statusName = "已作废";
      }
      //  this.currentDetail.statusName = value.status;
      this.activitySel.push({
        key: value.couponActivityId,
        value: value.name,
      });
    },
  },
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 45px;
    display: flex;
    align-items: center;
    .input {
      width: 180px;
      margin-right: 20px;
    }
  }
  .input-label {
    margin-left: 4px;
    font-size: 14px;
  }
  .pagination {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100px;
  }
}

/deep/ .el-date-editor {
  /deep/ .el-input__inner {
    height: 32px;
  }
}

/deep/ .el-input__icon {
  line-height: 32px;
}
>>> .el-form--inline .el-form-item {
  padding-bottom: 10px;
}
</style>
