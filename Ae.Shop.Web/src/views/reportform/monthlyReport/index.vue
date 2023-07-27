<template>
  <main class="container">
    <div class="mainForm">
      <el-form
        :inline="true"
        :model="condition"
        class="demo-form-inline"
        ref="condition"
      >
        <el-form-item prop="installDate" label="安装时间:">
          <el-date-picker
            v-model="condition.installDate"
            type="daterange"
            align="center"
            unlink-panels
            range-separator="-"
            start-placeholder="开始时间"
            end-placeholder="结束时间"
            :picker-options="pickerOptions"
            @change="handleArrivalDateChange"
            size="mini"
            value-format="yyyy-MM-dd"
            format="yyyy-MM-dd"
          ></el-date-picker>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" size="mini" @click="search(true)"
            >确认</el-button
          >
        </el-form-item>
      </el-form>
    </div>
    <!-- 首页：列表数据 -->
    <!-- <rg-page
      id="indexPage"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :headerBtnWidth="260"
      :tableData="tableData"
      :pageChange="pageChange"
      :searching="search"
      :showRowIndex="false"
      :amount="true"
    >-->
    <!-- 搜索 -->
    <!-- <template >
        <el-form-item prop="installDate" label="安装时间:">
          <el-date-picker
            v-model="condition.installDate"
            type="daterange"
            align="center"
            unlink-panels
            range-separator="-"
            start-placeholder="开始时间"
            end-placeholder="结束时间"
            :picker-options="pickerOptions"
            @change="handleArrivalDateChange"
          ></el-date-picker>
        </el-form-item>


    </template>-->

    <!-- 搜索 -->

    <!-- 列表 -->
    <!-- <template v-slot:tb_cols> -->
    <!-- Table数据 -->
    <el-table
      ref="tableData"
      v-loading="tableLoading"
      element-loading-text="加载中..."
      :data="tableData"
      stripe
      border
      size="mini"
      style="width: 100%; margin-top: 20px"
      max-height="500px"
      show-summary
      :summary-method="getSummaries"
    >
      <rg-table-column
        label="安装日期"
        align="center"
        prop="installDate"
      ></rg-table-column>
      <!-- <template scope="scope" >  -->
      <el-table-column label="总体">
        <rg-table-column
          label="客户数"
          align="center"
          prop="shopSaleMonthList[0].customerNum"
        ></rg-table-column>
        <rg-table-column
          label="订单数"
          align="center"
          prop="shopSaleMonthList[0].orderNum"
        ></rg-table-column>
        <rg-table-column
          label="订单总金额"
          align="center"
          prop="shopSaleMonthList[0].orderSumMoney"
        ></rg-table-column>
        <rg-table-column
          label="客均金额"
          align="center"
          prop="shopSaleMonthList[0].customerAvgMoney"
        ></rg-table-column>
        <rg-table-column
          label="单均金额"
          align="center"
          prop="shopSaleMonthList[0].orderAvgMoney"
        ></rg-table-column>
      </el-table-column>
      <el-table-column label="轮胎">
        <rg-table-column
          label="客户数"
          align="center"
          prop="shopSaleMonthList[1].customerNum"
        ></rg-table-column>
        <rg-table-column
          label="订单数"
          align="center"
          prop="shopSaleMonthList[1].orderNum"
        ></rg-table-column>
        <rg-table-column
          label="订单总金额"
          align="center"
          prop="shopSaleMonthList[1].orderSumMoney"
        ></rg-table-column>
        <rg-table-column
          label="客均金额"
          align="center"
          prop="shopSaleMonthList[1].customerAvgMoney"
        ></rg-table-column>
        <rg-table-column
          label="单均金额"
          align="center"
          prop="shopSaleMonthList[1].orderAvgMoney"
        ></rg-table-column>
      </el-table-column>
      <el-table-column label="保养">
        <rg-table-column
          label="客户数"
          align="center"
          prop="shopSaleMonthList[2].customerNum"
        ></rg-table-column>
        <rg-table-column
          label="订单数"
          align="center"
          prop="shopSaleMonthList[2].orderNum"
        ></rg-table-column>
        <rg-table-column
          label="订单总金额"
          align="center"
          prop="shopSaleMonthList[2].orderSumMoney"
        ></rg-table-column>
        <rg-table-column
          label="客均金额"
          align="center"
          prop="shopSaleMonthList[2].customerAvgMoney"
        ></rg-table-column>
        <rg-table-column
          label="单均金额"
          align="center"
          prop="shopSaleMonthList[2].orderAvgMoney"
        ></rg-table-column>
      </el-table-column>
      <el-table-column label="美容">
        <rg-table-column
          label="客户数"
          align="center"
          prop="shopSaleMonthList[3].customerNum"
        ></rg-table-column>
        <rg-table-column
          label="订单数"
          align="center"
          prop="shopSaleMonthList[3].orderNum"
        ></rg-table-column>
        <rg-table-column
          label="订单总金额"
          align="center"
          prop="shopSaleMonthList[3].orderSumMoney"
        ></rg-table-column>
        <rg-table-column
          label="客均金额"
          align="center"
          prop="shopSaleMonthList[3].customerAvgMoney"
        ></rg-table-column>
        <rg-table-column
          label="单均金额"
          align="center"
          prop="shopSaleMonthList[3].orderAvgMoney"
        ></rg-table-column>
      </el-table-column>
      <el-table-column label="钣金喷漆">
        <rg-table-column
          label="客户数"
          align="center"
          prop="shopSaleMonthList[4].customerNum"
        ></rg-table-column>
        <rg-table-column
          label="订单数"
          align="center"
          prop="shopSaleMonthList[4].orderNum"
        ></rg-table-column>
        <rg-table-column
          label="订单总金额"
          align="center"
          prop="shopSaleMonthList[4].orderSumMoney"
        ></rg-table-column>
        <rg-table-column
          label="客均金额"
          align="center"
          prop="shopSaleMonthList[4].customerAvgMoney"
        ></rg-table-column>
        <rg-table-column
          label="单均金额"
          align="center"
          prop="shopSaleMonthList[4].orderAvgMoney"
        ></rg-table-column>
      </el-table-column>
      <el-table-column label="维修改装">
        <rg-table-column
          label="客户数"
          align="center"
          prop="shopSaleMonthList[5].customerNum"
        ></rg-table-column>
        <rg-table-column
          label="订单数"
          align="center"
          prop="shopSaleMonthList[5].orderNum"
        ></rg-table-column>
        <rg-table-column
          label="订单总金额"
          align="center"
          prop="shopSaleMonthList[5].orderSumMoney"
        ></rg-table-column>
        <rg-table-column
          label="客均金额"
          align="center"
          prop="shopSaleMonthList[5].customerAvgMoney"
        ></rg-table-column>
        <rg-table-column
          label="单均金额"
          align="center"
          prop="shopSaleMonthList[5].orderAvgMoney"
        ></rg-table-column>
      </el-table-column>
      <el-table-column label="其他">
        <rg-table-column
          label="客户数"
          align="center"
          prop="shopSaleMonthList[6].customerNum"
        ></rg-table-column>
        <rg-table-column
          label="订单数"
          align="center"
          prop="shopSaleMonthList[6].orderNum"
        ></rg-table-column>
        <rg-table-column
          label="订单总金额"
          align="center"
          prop="shopSaleMonthList[6].orderSumMoney"
        ></rg-table-column>
        <rg-table-column
          label="客均金额"
          align="center"
          prop="shopSaleMonthList[6].customerAvgMoney"
        ></rg-table-column>
        <rg-table-column
          label="单均金额"
          align="center"
          prop="shopSaleMonthList[6].orderAvgMoney"
        ></rg-table-column>
        <!-- </template>  -->
      </el-table-column>
    </el-table>
    <!-- Table数据 -->
    <!-- </template> -->
    <!-- 列表 -->
    <!-- </rg-page> -->
    <!-- 首页：列表数据 -->
  </main>
</template>
<style>
</style>
<script>
import { mapGetters } from "vuex";
import rgPage from "@/components/TemplatePage";
import { common } from "@/utils/common.js";
import { reportFormSvc } from "@/api/reportForm/reportForm.js";
export default {
  components: { rgPage },
  name: "monthlyReport",
  data() {
    return {
      showRowIndex: false,
      timeDefaultShow: "",
      pickerOptions: {
        onPick: ({ maxDate, minDate }) => {
          this.condition.StartDate = minDate;
          this.condition.endDate = maxDate;
        },
        disabledDate: (time) => {
          let curDate = new Date().getTime();
          let two = 90 * 24 * 3600 * 1000;
          let twoyear = curDate - two;
          let three = 30 * 3 * 24 * 3600 * 1000;
          if (this.minDate) {
            return (
              time.getTime() > Date.now() ||
              time.getTime() < twoyear ||
              time > new Date(this.minDate.getTime() + three) ||
              time < new Date(this.minDate.getTime() - three)
            );
          }
          return time.getTime() > Date.now() || time.getTime() < twoyear;
        },
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
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 89);
              picker.$emit("pick", [start, end]);
            },
          },
        ],
      },
      numdata: [
        {
          customerAvgMoney: 0,
          customerNum: 0,
          orderAvgMoney: 0,
          orderNum: 0,
          orderSumMoney: 0,
          type: 0,
        },
        {
          customerAvgMoney: 0,
          customerNum: 0,
          orderAvgMoney: 0,
          orderNum: 0,
          orderSumMoney: 0,
          type: 1,
        },
        {
          customerAvgMoney: 0,
          customerNum: 0,
          orderAvgMoney: 0,
          orderNum: 0,
          orderSumMoney: 0,
          type: 2,
        },
        {
          customerAvgMoney: 0,
          customerNum: 0,
          orderAvgMoney: 0,
          orderNum: 0,
          orderSumMoney: 0,
          type: 3,
        },
        {
          customerAvgMoney: 0,
          customerNum: 0,
          orderAvgMoney: 0,
          orderNum: 0,
          orderSumMoney: 0,
          type: 4,
        },
        {
          customerAvgMoney: 0,
          customerNum: 0,
          orderAvgMoney: 0,
          orderNum: 0,
          orderSumMoney: 0,
          type: 5,
        },
        {
          customerAvgMoney: 0,
          customerNum: 0,
          orderAvgMoney: 0,
          orderNum: 0,
          orderSumMoney: 0,
          type: 6,
        },
      ],
      shopSaleMonthList: [],
      condition: {
        installDate: ["", ""],
        ShopId: "",
        StartDate: "",
        endDate: "",
      },
      tableLoading: false,
      tableData: [],
      totalList: 0,
      amount: true,
    };
  },
  created() {
    this.defaultDate();
    this.condition.ShopId = this.userInfo.organizationId;
    console.log("ShopId", this.condition.ShopId);
  },
  computed: {
    ...mapGetters(["userInfo"]),
  },
  methods: {
    handleArrivalDateChange() {
      if (this.condition.installDate == undefined) {
        this.defaultDate();
      } else {
        this.condition.startDate = this.condition.installDate[0];
        this.condition.endDate = this.condition.installDate[1];
      }
    },

    //设置默认时间
    defaultDate() {
      //获取新的时间(2019.4.12）
      let date = new Date();
      //获取当前时间的年份转为字符串
      let year = date.getFullYear().toString(); //'2019'
      //获取月份，由于月份从0开始，此处要加1，判断是否小于10，如果是在字符串前面拼接'0'
      let month =
        date.getMonth() + 1 < 10
          ? "0" + (date.getMonth() + 1).toString()
          : (date.getMonth() + 1).toString(); //'04'
      //获取天，判断是否小于10，如果是在字符串前面拼接'0'
      let da =
        date.getDate() < 10
          ? "0" + date.getDate().toString()
          : date.getDate().toString(); //'12'
      //字符串拼接，开始时间，结束时间
      let end = year + "-" + month + "-" + da; //当天'2019-04-12'
      let beg = year + "-" + month + "-01"; //当月第一天'2019-04-01'
      this.condition.installDate = [beg, end]; //将值设置给插件绑定的数据
      console.log(12, this.condition.installDate);
      this.condition.startDate = this.condition.installDate[0];
      this.condition.endDate = this.condition.installDate[1];
      console.log(121, this.condition.startDate);
      this.fetchData();
    },
    fetchData() {
      this.getPaged();
    },
    getPaged(flag) {
      let data = JSON.parse(JSON.stringify(this.condition));
      if (data.installDate != null) {
        data.StartDate = data.installDate.length > 1 ? data.installDate[0] : "";
        data.EndDate = data.installDate.length > 1 ? data.installDate[1] : "";
      }
      console.log(data);
      let adddata = {};
      let ShopId = this.condition.ShopId;
      // delete adddata.installDate//删除 installDate
      adddata = {
        StartDate: this.condition.startDate,
        EndDate: this.condition.endDate,
        //  ShopId:this.userInfo.organizationId
      };

      console.log(2, adddata);
      this.tableLoading = true;
      reportFormSvc
        .GetShopSalesMonthList(adddata)
        .then(
          (res) => {
            let data = res.data;
            this.tableData = data;
            console.log(122, this.tableData);
            var arr = [];
            this.tableData.forEach((item) => {
              if (item.shopSaleMonthList.length == 0) {
                item.shopSaleMonthList = this.numdata;
              }
              arr.push(item.shopSaleMonthList);
            });
            //     this.tableData.forEach(item => {
            // if(item.shopSaleMonthList){

            //   }else{
            //     item.shopSaleMonthList = this.numdata;

            //   }

            // });
            this.shopSaleMonthList = arr;
            console.log("tableData", this.tableData);

            // console.log(123, this.shopSaleMonthList);
            if (flag) {
              this.$messageSuccess(res.message || "查询成功");
            }
          },
          (err) => {
            console.error(err);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });
    },
    allamount(index, num) {
      const sums = [];
      sums[index] = this.shopSaleMonthList
        .map((row) => row[num].customerNum)
        .reduce((acc, cur) => Number(cur) + acc, 0);
      return;
    },
    search(flag) {
      this.getPaged(flag);
    },
    getSummaries(param) {
      const { columns, data } = param;
      const sums = [];
      columns.forEach((column, index) => {
        if (index === 0) {
          sums[index] = "总计";
          return;
        }
        let arr2 = [];
        const values = data.map((item) => Number(item[column.property]));
        if (index === 1) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[0].customerNum)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
          console.log(sums[index]);
        }

        if (index === 2) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[0].orderNum)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }
        if (index === 3) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[0].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }
        if (index === 4) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[0].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (sums[1] != undefined || sums[1] != 0) {
            sums[index] = sums[index] / sums[1];
          }
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }

          return;
        }
        if (index === 5) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[0].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (sums[2] != undefined || sums[2] != 0) {
            sums[index] = sums[index] / sums[2];
          }
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }
        if (index === 6) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[1].customerNum)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }

        if (index === 7) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[1].orderNum)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }
        if (index === 8) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[1].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }
        if (index === 9) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[1].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (sums[6] != undefined || sums[6] != 0) {
            sums[index] = sums[index] / sums[6];
          }
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }
        if (index === 10) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[1].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (sums[7] != undefined || sums[7] != 0) {
            sums[index] = sums[index] / sums[7];
          }
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }
        if (index === 11) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[2].customerNum)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }

        if (index === 12) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[2].orderNum)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }
        if (index === 13) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[2].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }
        if (index === 14) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[2].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (sums[11] != undefined || sums[11] != 0) {
            sums[index] = sums[index] / sums[11];
          }
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }
        if (index === 15) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[2].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (sums[12] != undefined || sums[12] != 0) {
            sums[index] = sums[index] / sums[12];
          }
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }
        if (index === 16) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[3].customerNum)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }

        if (index === 17) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[3].orderNum)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }
        if (index === 18) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[3].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }
        if (index === 19) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[3].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (sums[16] != undefined || sums[16] != 0) {
            sums[index] = sums[index] / sums[16];
          }
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }
        if (index === 20) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[3].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (sums[17] != undefined || sums[17] != 0) {
            sums[index] = sums[index] / sums[17];
          }
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }
        if (index === 21) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[4].customerNum)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }

        if (index === 22) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[4].orderNum)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }
        if (index === 23) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[4].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }
        if (index === 24) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[4].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (sums[21] != undefined || sums[21] != 0) {
            sums[index] = sums[index] / sums[21];
          }
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }

        if (index === 25) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[4].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (sums[22] != undefined || sums[22] != 0) {
            sums[index] = sums[index] / sums[22];
          }
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }

        if (index === 26) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[5].customerNum)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }
        if (index === 27) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[5].orderNum)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }
        if (index === 28) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[5].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }
        if (index === 29) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[5].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (sums[26] != undefined || sums[26] != 0) {
            sums[index] = sums[index] / sums[26];
          }
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }
        if (index === 30) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[5].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (sums[27] != undefined || sums[27] != 0) {
            sums[index] = sums[index] / sums[27];
          }
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }
        if (index === 31) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[6].customerNum)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }
        if (index === 32) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[6].orderNum)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          return;
        }
        if (index === 33) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[6].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }
        if (index === 34) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[6].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (sums[31] != undefined || sums[31] != 0) {
            sums[index] = sums[index] / sums[31];
          }
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }
        if (index === 35) {
          sums[index] = this.shopSaleMonthList
            .map((row) => row[6].orderSumMoney)
            .reduce((acc, cur) => Number(cur) + acc, 0);
          if (sums[32] != undefined || sums[32] != 0) {
            sums[index] = sums[index] / sums[32];
          }
          if (isNaN(sums[index])) {
            sums[index] = 0;
          } else {
            sums[index] = this.changeTwoDecimal_f(sums[index]);
          }
          return;
        }
        if (!values.every((value) => isNaN(value))) {
          // if (column.property === 'num') {//判断均为数字
          sums[index] = values.reduce((prev, curr) => {
            const value = Number(curr);
            if (!isNaN(value)) {
              return prev + curr;
            } else {
              return prev;
            }
          }, 0);
          sums[index] += " ";
        } else {
          sums[index] = "NaN";
        }
      });

      return sums;
    },
    changeTwoDecimal_f(x) {
      var f_x = parseFloat(x);
      if (isNaN(f_x)) {
        return 0;
      }
      var f_x = Math.round(x * 100) / 100;
      var s_x = f_x.toString();
      var pos_decimal = s_x.indexOf(".");
      if (pos_decimal < 0) {
        pos_decimal = s_x.length;
        s_x += ".";
      }
      while (s_x.length <= pos_decimal + 2) {
        s_x += "0";
      }
      return s_x;
    },
  },
};
</script>

<style lang="scss" scoped>
</style>
