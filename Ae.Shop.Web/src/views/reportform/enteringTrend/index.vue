<template>
  <main class="container">
    <div class="mainForm">
      <el-form :inline="true" :model="condition" class="demo-form-inline" ref="condition">
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
          <el-button type="primary" size="mini" @click="search(true)">确认</el-button>
        </el-form-item>
      </el-form>
    </div>
    <div id="mainchart" style="max-width:100%;height:300px;"></div>
    <el-table
      ref="tableData"
      v-loading="tableLoading"
      element-loading-text="加载中..."
      :data="tableData"
      stripe
      border
      size="mini"
      style="width: 100%;margin-top:20px;"
      max-height="300px"
    >
      <rg-table-column label="日期" align="center" prop="arrivalTime"></rg-table-column>
      <rg-table-column label="进店人数总计" prop="amount" align="center"></rg-table-column>
      <rg-table-column label="已完结" align="center" prop="completed"></rg-table-column>
      <rg-table-column label="未消费" align="center" prop="leave"></rg-table-column>
      <rg-table-column label="未完成" align="center" prop="unCompleted"></rg-table-column>
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
var echarts = require("echarts");
// 引入柱状图
require("echarts/lib/chart/bar");
// 引入提示框和标题组件
require("echarts/lib/component/tooltip");
require("echarts/lib/component/title");
import { mapGetters } from "vuex";
import rgPage from "@/components/TemplatePage";
import { common } from "@/utils/common.js";
import { reportFormSvc } from "@/api/reportForm/reportForm.js";
export default {
  components: { rgPage },
  name: "monthlyReport",
  data() {
    return {
      dates: [],
      amount: [],
      completed: [],
      leave: [],
      unCompleted: [],
      timeDefaultShow: "",
      pickerOptions: {
        onPick: ({ maxDate, minDate }) => {
          this.condition.StartDate = minDate;
          this.condition.endDate = maxDate;
        },
        disabledDate: time => {
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
            }
          },
          {
            text: "最近一个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近三个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 89);
              picker.$emit("pick", [start, end]);
            }
          }
        ]
      },
      condition: {
        installDate: ["", ""],
        ShopId: "",
        StartDate: "",
        endDate: ""
        // pageIndex: 1,
        //   pageSize: 20,
      },
      tableLoading: false,
      tableData: [],
      totalList: 0,

      charts: ""
    };
  },
  created() {
    // this.fetchData();
    this.defaultDate();
    this.condition.ShopId = this.userInfo.organizationId;
    console.log("ShopId", this.condition.ShopId);
  },
  computed: {
    ...mapGetters(["userInfo"])
  },

  mounted() {
    this.$nextTick(function() {
      this.drawLine();
    });
  },
  methods: {
    drawLine() {
      var dates = this.dates;
      var amount = this.amount;
      var completed = this.completed;
      var leave = this.leave;
      var unCompleted = this.unCompleted;

      var myChart = echarts.init(document.getElementById("mainchart"));
      myChart.setOption({
        title: {
          text: "进店趋势图"
        },
        tooltip: {
          trigger: "axis"
        },
        legend: {
          // selectedMode: 'single',
          data: ["进店人数", "已完结", "未消费", "未完成"],
          left: "center",
          top: "10px",
          // itemWidth: 13,
          // itemHeight: 13,
          icon: "line"
        },
        grid: {
          left: "3%",
          right: "4%",
          bottom: "3%",
          containLabel: true
        },
        // toolbox: {
        //   feature: {
        //     saveAsImage: {}
        //   }
        // },
        xAxis: {
          type: "category",
          boundaryGap: true,
          data: dates,
          nameLocation: "end", //坐标轴名称显示位置。
          // showAllSymbol: true,
          axisLabel: {
            //坐标轴刻度标签的相关设置。
             interval: 2,
      
             rotate: "30"
          }
        },
        yAxis: {
          type: "value"
        },
        series: [
          {
            name: "进店人数",
            type: "line",
            // stack: "总量",
            // data: [120, 132, 101, 134, 90, 230, 210]
            data: amount
          },
          {
            name: "已完结",
            type: "line",
            // stack: "总量",
            // data: [220, 182, 191, 234, 290, 330, 310]
            data: completed
          },
          {
            name: "未消费",
            type: "line",
            // stack: "总量",
            // data: [150, 232, 201, 154, 190, 330, 410]
            data: leave
          },
          {
            name: "未完成",
            type: "line",
            // stack: "总量",
            data: unCompleted
            // data: [320, 332, 301, 334, 390, 330, 320]
          }
        ]
      });
    },
    handleArrivalDateChange() {
      if (this.condition.installDate == undefined) {
        this.defaultDate();
      } else {
        this.condition.startDate = this.condition.installDate[0];
        this.condition.endDate = this.condition.installDate[1];

        console.log(121, this.condition.endDate);
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
      // console.log(12, this.condition.installDate);
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
      console.log("startDate", this.condition.startDate);
      // delete adddata.installDate; //删除 installDate
      adddata = {
        StartTime: this.condition.startDate,
        EndTime: this.condition.endDate,
        ShopId: this.userInfo.organizationId
      };

      console.log("adddata", adddata);
      this.tableLoading = true;
      reportFormSvc
        .GetArrivalTrendStatistics(adddata)
        .then(
          res => {
            let data = res.data;
            this.tableData = data.tableStatistics.items;
            this.dates = data.chartStatistics.dates;
            this.amount = data.chartStatistics.amount;

            this.completed = data.chartStatistics.completed;
            this.leave = data.chartStatistics.leave;
            this.unCompleted = data.chartStatistics.unCompleted;

            this.drawLine();
            console.log("dates", this.dates);
            console.log(122, this.tableData);

            if (flag) {
              this.$messageSuccess(res.message || "查询成功");
            }
          },
          err => {
            console.error(err);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });
    },
    search(flag) {
      this.getPaged(flag);
    }
  }
};
</script>

<style lang="scss" scoped>
</style>