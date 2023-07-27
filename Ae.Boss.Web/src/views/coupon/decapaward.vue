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
            placeholder="请输入兑奖码"
            style="width: 200px"
            prefix-icon="el-icon-search"
            v-model="condition.code"
          >
          </el-input>
        </el-form-item>
        <el-form-item>
          <el-select
            style="width: 120px"
            v-model="condition.status"
            filterable
            clearable
            placeholder="-领取状态-"
          >
            <el-option
              v-for="item in statusEnum"
              :key="item.id"
              :label="item.displayName"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            style="width: 120px"
            v-model="condition.payStatus"
            filterable
            clearable
            placeholder="-打款状态-"
          >
            <el-option
              v-for="item in payStatusEnum"
              :key="item.id"
              :label="item.displayName"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            style="width: 120px"
            v-model="condition.payChannel"
            filterable
            clearable
            placeholder="-领取渠道-"
          >
            <el-option
              v-for="item in channelEnum"
              :key="item.id"
              :label="item.displayName"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
      </template>

      <template v-slot:tb_cols>
          <el-table-column label="兑奖码" prop="code" :width="140"></el-table-column>
          <el-table-column label="金额" align="right" prop="amount" :width="70"></el-table-column>
          <el-table-column label="领取状态" align="center" prop="statusDisplay" :width="70"></el-table-column>
          <el-table-column label="领取时间" align="center" prop="drawTime" :width="130"></el-table-column>
        <el-table-column label="领取人OpenId" align="center" prop="drawOpenId"></el-table-column>
        <el-table-column label="领取人UserId" align="center" prop="drawUserId"></el-table-column>
        <el-table-column label="姓名" align="center" prop="userName" :width="80"></el-table-column>
        <el-table-column label="手机/邮箱" align="center" prop="userTel" :width="90"></el-table-column>
          <el-table-column label="打款状态" align="center" prop="payStatusDisplay" :width="70"></el-table-column>
          <el-table-column label="打款时间" align="center" prop="payTime" :width="130"></el-table-column>
          <el-table-column label="领取渠道" align="center" prop="clientChannelDisplay" :width="70"></el-table-column>
      </template>
    </rg-page>
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { couponSvc } from "@/api/coupon/coupon";
export default {
  name: "decapaward",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      condition: {
        pageIndex: 1,
        pageSize: 20,
        code: "",
        status: undefined,
        payStatus: undefined,
        payChannel: "",
      },
      tableData: [],
      statusEnum: [
        { id: 0, displayName: "待领取" },
        { id: 1, displayName: "已领取" },
      ],
      payStatusEnum: [
        { id: 0, displayName: "待打款" },
        { id: 1, displayName: "已打款" },
      ],
      channelEnum: [
        { id: "WeChat", displayName: "微信" },
        { id: "AliPay", displayName: "支付宝" },
      ],
    };
  },
  created(){
      this.fetchData();
  },
  methods:{
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
            couponSvc
            .searchDecapAward(this.condition)
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
        }
  }
};
</script>