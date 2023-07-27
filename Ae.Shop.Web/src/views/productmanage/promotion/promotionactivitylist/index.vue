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
        <el-form-item prop="activityName">
          <el-input
            class="input"
            clearable
            placeholder="请输入活动名称"
            prefix-icon="el-icon-search"
            v-model="condition.activityName"
          ></el-input>
        </el-form-item>
        <el-form-item prop="activityId">
          <el-input
            class="input"
            clearable
            placeholder="请输入活动Id"
            prefix-icon="el-icon-search"
            v-model="condition.activityId"
          ></el-input>
        </el-form-item>

        <el-form-item prop="productCode">
          <el-input
            class="input"
            clearable
            placeholder="请输入商品Pid"
            prefix-icon="el-icon-search"
            v-model="condition.productCode"
          ></el-input>
        </el-form-item>

        <el-form-item prop="startDate" label="活动时间">
          <el-date-picker
            style="width: 160px;"
            v-model="condition.startDate"
            type="date"
            placeholder="开始日期"
            size="mini"
          ></el-date-picker>
        </el-form-item>
        <el-form-item prop="endDate">
          <el-date-picker
            style="width: 160px;"
            v-model="condition.endDate"
            type="date"
            placeholder="终止日期"
            size="mini"
          ></el-date-picker>
        </el-form-item>
        <el-form-item prop="status" label="活动状态">
          <el-select v-model="condition.status" size="mini" style="width:100px;">
            <el-option
              v-for="item in promotionStatus"
              :key="item.value"
              :label="item.displayName"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>

      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="toAdd">新增</el-button>
      </template>

      <template v-slot:tb_cols>
        <el-table-column label="促销活动Id" prop="activityId" width="290"></el-table-column>
        <el-table-column label="活动名称" prop="name"></el-table-column>
        <el-table-column label="促销语" prop="subtitle"></el-table-column>
        <el-table-column label="开始时间" prop="startTime" width="160"></el-table-column>
        <el-table-column label="结束时间" prop="endTime" width="160"></el-table-column>
        <el-table-column label="活动状态" prop="statusDisplay" width="80"></el-table-column>
        <el-table-column label="创建人" prop="createdBy" width="100"></el-table-column>
        <el-table-column fixed="right" label="操作">
          <template slot-scope="scope">
            <router-link :to="'/marketingconfig/shop/promotiondetail/'+scope.row.activityId">
              <el-button size="mini" type="primary">详情</el-button>
            </router-link>
            <el-button
              size="mini"
              v-show="scope.row.status == 1"
              type="success"
              @click="auditPromotion(scope.row.activityId)"
            >审核通过</el-button>
          </template>
        </el-table-column>
      </template>
    </rg-page>
  </div>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/product/promotion";
export default {
  name: "promotionactivitylist",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      condition: {
        activityName: "",
        activityId: "",
        productCode: "",
        startDate: "",
        endDate: "",
        status: 0,
        pageIndex: 1,
        pageSize: 10
      },
      tableData: [],
      promotionStatus: [
        {
          displayName: "全部",
          value: 0
        },
        {
          displayName: "进行中",
          value: 4
        },
        {
          displayName: "未开始",
          value: 3
        },
        {
          displayName: "已结束",
          value: 5
        },
        {
          displayName: "待审核",
          value: 1
        },
        {
          displayName: "已拒绝",
          value: 2
        }
      ]
    };
  },
  created() {
    this.getPaged();
  },
  methods: {
    toAdd() {
      this.$router.push("/marketingconfig/shop/addpromotion");
    },
    searchPromotionActivity() {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(true);
    },
    getPaged(flag) {
      console.log("condition: " + JSON.stringify(this.condition));
      this.tableLoading = true;
      appSvc
        .searchPromotionActivity(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (flag) {
              this.$message({
                message: res.message || "查询成功",
                type: "success"
              });
            }
          },
          error => {
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
        activityId: activityParaId
      };
      appSvc
        .auditPromotionActivity(auditRequest)
        .then(
          res => {
            var data = res.data;
            if (data) {
              this.$message({ message: "操作成功", type: "success" });
            } else {
              this.$message({ message: "操作失败", type: "warning" });
            }
            this.getPaged(false);
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });
    }
  }
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