<template>
  <main class="container">
    <!-- 首页：列表数据 -->
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :headerBtnWidth="90"
      :tableData="tableData"
      :pageChange="pageChange"
      :searching="search"
      :conditionModel="condition"
      :conditionRules="rules"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-input
            v-model="condition.telephone"
            style="width:200px"
            autocomplete="off"
            placeholder="手机号"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-input
            v-model="condition.carNo"
            style="width:150px"
            autocomplete="off"
            placeholder="车牌号"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item prop="arrivalDates">
          <el-tooltip class="item" effect="dark" content="到店日期" placement="bottom">
            <el-date-picker
              v-model="condition.arrivalDates"
              style="width:220px"
              type="daterange"
              align="center"
              :picker-options="$root.$data.timeRgPickerOpt"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              :default-time="['00:00:00', '00:00:00']"
              @change="handleArrivalDateChange"
            ></el-date-picker>
          </el-tooltip>
        </el-form-item>
        <el-form-item>
          <el-select v-model="condition.status" style="width:118px;" placeholder="到店状态">
            <el-option
              v-for="item in statusEnum"
              :key="item.value"
              :label="item.displayName"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <el-table-column label="主键" prop="id" align="center" v-if="false"></el-table-column>
        <el-table-column label="车型" prop="vehicle" min-width="200"></el-table-column>
        <el-table-column label="车牌" prop="carNo" width="90"></el-table-column>
        <rg-table-column label="客户姓名" prop="userName" />
        <el-table-column label="接待人" prop="techName" width="90"></el-table-column>
        <el-table-column label="到店时间" prop="showArrivalDate" align="center" width="110"></el-table-column>
        <el-table-column label="排队类型" prop="showQueueType" align="center" width="90"></el-table-column>
        <el-table-column label="服务类型" prop="showServiceType" align="center" width="90"></el-table-column>
        <el-table-column label="排队号" prop="queueNumber" width="90"></el-table-column>
        <el-table-column label="状态" prop="statusname" align="center" width="100"></el-table-column>
        <el-table-column label="车况检查" prop="statusname" align="center" width="100" fixed="right">
          <template slot-scope="scope">
            <el-button
              type="primary"
              size="mini"
              @click="toReport(scope.row)"
              v-if="scope.row.checkId!==0"
            >车况检查报告</el-button>
          </template>
        </el-table-column>
        <el-table-column fixed="right" label="操作" align="center" :width="90">
          <template slot-scope="scope">
            <el-button-group>
              <el-button @click="routerToDetail(scope.row)" type="primary" size="mini">查看详情</el-button>
            </el-button-group>
          </template>
        </el-table-column>
        <!-- Table数据 -->
      </template>
    </rg-page>
    <!-- 首页：列表数据 -->

    <!-- 新增和编辑页面 -->
    <section id="detailOrEdit">
      <rg-dialog
        :title="formTitle"
        :loading="formLoading"
        :visible.sync="formVisible"
        :beforeClose="cancel"
        :btnCancel="{label:'关闭',click:(done)=>{cancel('formModel')}}"
        :btnRemove="false"
        :footbar="true"
        v-if="formVisible"
        width="78%"
        maxWidth="800px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-form :model="formModel" ref="formModel" size="mini" label-width="120px">
            <el-form-item label="到店车辆">
              <el-avatar
                shape="square"
                style="float:left;background:none"
                :size="28"
                :src="formModel.carInfo.carLogo"
              ></el-avatar>
              <div style="float:left; margin-left:5px">
                {{formModel.carInfo.vehicle}}
                <i class="el-icon-minus"></i>
                {{formModel.carInfo.carNo}}
              </div>
            </el-form-item>
            <!-- <el-form-item label="车牌">
              <label>{{formModel.carInfo.carNo}}</label>
            </el-form-item>-->
            <el-form-item label="客户姓名">
              <label>{{formModel.userName}}</label>
            </el-form-item>
            <el-form-item label="客户电话">
              <label>{{formModel.telephone}}</label>
            </el-form-item>
            <el-form-item label="服务类型">
              <label>{{formModel.showServiceType}}</label>
            </el-form-item>
            <el-form-item label="到店时间">
              <label>{{formModel.showArrivalDate}}</label>
            </el-form-item>
            <el-form-item label="排队号">
              <label>{{formModel.queueNumber}}&nbsp;&nbsp;{{formModel.statusname}}</label>
            </el-form-item>
            <el-form-item label="接待人">
              <label>{{formModel.techName}}</label>
            </el-form-item>
            <el-form-item label="到店备注">
              <label>{{formModel.remark}}</label>
            </el-form-item>
            <el-form-item label="车辆诊断报告">
              <el-link v-show="formModel.carReportUrl!=''" :href="formModel.carReportUrl" target="_blank" type="primary">查看报告</el-link>
              <label v-show="formModel.carReportUrl==''">未检测</label>
            </el-form-item>
            <el-form-item label="施工视频">
              <el-row v-for="itemVideo in formModel.arrivalVideos" :key="itemVideo.id">
                <el-link :href="itemVideo.url" target="_blank" type="primary">{{itemVideo.name}}</el-link>
              </el-row>
            </el-form-item>
            <el-form-item label="服务项目">
              <el-table
                :data="formModel.projectItems"
                border
                size="mini"
                height="300"
                style="width: 100%;"
              >
                <el-table-column label="订单号" prop="orderId">
                  <template slot-scope="scope">{{scope.row.orderId}}</template>
                </el-table-column>
                <el-table-column label="项目内容">
                  <template slot-scope="scope">{{scope.row.name+"*"+scope.row.num}}</template>
                </el-table-column>
                <el-table-column label="总金额" prop="price">
                  <template slot-scope="scope">{{scope.row.price}}</template>
                </el-table-column>
              </el-table>
            </el-form-item>
          </el-form>
        </template>

        <template v-slot:footer></template>
      </rg-dialog>
    </section>
    <!-- 新增和编辑页面 -->
  </main>
</template>

<script>
import { arrivalSvc } from "@/api/customermanage/arrival";

export default {
  name: "arrivalList",
  data() {
    return {
      statusEnum: [{ value: -1, displayName: "全部" }],
      tableLoading: false,
      formLabelWidth: "100px",
      condition: {
        telephone: "",
        carNo: "",
        arrivalDates: [],
        startDate: "",
        endDate: "",
        status: 0,
        pageIndex: 1,
        pageSize: 20
      },
      tableData: [],
      totalList: 0,
      formVisible: false,
      formTitle: "到店记录详情",
      formCheck: false,
      formLoading: false,

      btnSaveLoading: false,
      formModel: {
        userName: "",
        telephone: "",
        carInfo: {
          carLogo: "",
          vehicle: "",
          carNo: ""
        },
        showArrivalDate: "",
        reserveTypeName: "",
        reserveTime: "",
        pickOne: "",
        remark: "",
        ProjectItems: [],
        carReportUrl:'',
        arrivalVideos:[]
      },
      rules: {
        arrivalDates: [
          {
            type: "array",
            required: true,
            message: "请指定到店时间范围",
            trigger: "change"
          }
        ]
      }
    };
  },
  created() {
    this.getCondition();
    this.fetchData();
  },
  watch: {},
  methods: {
    toReport(data) {
      this.$router.push({ name: "checkReport", params: { car: data } });
    },
    getDate() {
      let nowDate = new Date();
      let date = {
        year: nowDate.getFullYear(),
        month: nowDate.getMonth() + 1,
        date: nowDate.getDate()
      };
      let systemDate = date.year + "/" + date.month + "/" + date.date;
      return systemDate;
    },
    handleArrivalDateChange() {
      if (this.condition.arrivalDates == undefined) {
        this.condition.startDate = this.getDate();
        this.condition.endDate = this.getDate();
      } else {
        this.condition.startDate = this.condition.arrivalDates[0].toLocaleDateString();
        this.condition.endDate = this.condition.arrivalDates[1].toLocaleDateString();
      }
    }, //处理开始使用日期改变
    fetchData() {
      this.condition.arrivalDates = [this.getDate(), this.getDate()];
      this.getPaged();
    },
    search(flag) {
      this.condition.pageIndex = this.currerrentPage = 1;
      this.getPaged(flag);
    },
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    getPaged(flag) {
      let _this = this;
      this.tableLoading = true;
      arrivalSvc
        .getArrivalList(this.condition)
        .then(
          res => {
            var data = res.data;
            data.items.forEach(element => {
              element.showArrivalDate = element.showArrivalDate.JDateFormat();
              element.statusname =
                (_this.statusEnum.filter(item => {
                  return item.value == element.status;
                })[0] || {})["displayName"] || "";
            });
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
    getCondition() {
      arrivalSvc
        .getArrivalListCondition()
        .then(
          res => {
            var data = res.data;
            this.statusEnum = data.status;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    routerToDetail(row) {
      let arrivalId = row.id;
      this.formVisible = true;
      this.formLoading = true;
      if (this.arrivalDates == null || this.arrivalDates == "") {
      }
      var arrivalRequest = {
        arrivalId: arrivalId
      };
      arrivalSvc
        .getArrivalDetail(arrivalRequest)
        .then(
          res => {
            var data = res.data;
            if (data == null) {
              //this.formModel = this.formInit;
              this.$message({
                message: "系统异常，请关闭重新打开",
                type: "error"
              });
            } else {
              this.formModel = this.$jscom.extend(data, row);
              console.log(this.formModel, row);
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.formLoading = false;
        });
    },
    cancel(formName) {
      this.formVisible = false;

      //this.resetForm();

      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },
    resetForm() {
      //this.formModel = JSON.parse(JSON.stringify(this.formInit));
    }
  }
};
</script>
