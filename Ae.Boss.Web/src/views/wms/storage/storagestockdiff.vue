<template>
  <main class="container">
    <!-- 首页：列表数据 -->
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :headerBtnWidth="70"
      :tableData="tableData"
      :pageChange="pageChange"
      :searching="search"
      :tbCellClassName="(p)=>{ 
        if(p.columnIndex==6)
          return p.row.diffNum<0 ?'loss':p.row.diffNum>0?'overage':'';
        else if(p.columnIndex==7)
          return {'差异处理中':'','盘点完成':'yellowgreen','无效':'red'}[p.row.status];
        }"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
      
        <el-form-item label>
          <el-input
            v-model="condition.productId"
            style="width:250px"
            autocomplete="off"
            placeholder="产品名称或编码"
            clearable
          ></el-input>
        </el-form-item>

        <el-form-item label>
          <el-input
            v-model="condition.planNo"
            style="width:150px"
            autocomplete="off"
            placeholder="盘点计划编号"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item label>
          <el-tooltip class="item" effect="dark" content="盘点提交日期" placement="bottom">
            <el-date-picker
              v-model="condition.times"
              type="daterange"
              :picker-options="$root.$data.timeRgPickerOpt"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              align="center"
              style="width:220px"
            ></el-date-picker>
          </el-tooltip>
        </el-form-item>

        <el-form-item>
          <el-select
            v-model="condition.tempType"
            clearable
            placeholder="请选择盘盈亏"
            style="width:150px"
            @change="changeType()"
          >
            <el-option
              v-for="item in typeSel"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>

        <el-form-item>
          <el-select
            @change="changeStatus()"
            v-model="condition.tempStatus"
            clearable
            placeholder="请选择状态"
            style="width:150px"
          >
            <el-option
              v-for="item in statusSel"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
         <el-form-item>
          <el-select
            v-model="condition.locationId"
            clearable
            filterable
            placeholder="请选择仓库"
          >
            <el-option
              v-for="item in warehouseSel"
              :key="item.id"
              :label="item.warehouseName"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <el-table-column label="盘库计划编号" prop="planNo" width="100"></el-table-column>
    
    <rg-table-column label="仓库名称" prop="locationName" />
        <rg-table-column label="产品编号" prop="productId" />
        <rg-table-column label="产品名称" prop="productName" />
        <el-table-column label="系统数量" align="right" prop="systemNum" width="80">
          <template slot-scope="scope">{{scope.row.systemNum.format(0)}}</template>
        </el-table-column>
        <el-table-column label="盘点数量" align="right" prop="storageNum" width="100">
          <template slot-scope="scope">{{scope.row.storageNum.format(0)}}</template>
        </el-table-column>
        <el-table-column label="盈/亏量" align="right" width="80">
          <template slot-scope="scope">{{absNum(scope.row.diffNum.format(0))}}</template>
        </el-table-column>
        <el-table-column label="处理状态" align="center" width="80">
          <template slot-scope="scope">{{{"差异处理中":"待确认","盘点完成":"已确认","无效":"重盘"}[scope.row.status]}}</template>
        </el-table-column>
        <el-table-column label="处理人" prop="dealBy" width="80"></el-table-column>
        <el-table-column label="处理时间" align="center" prop="dealTime" width="110">
          <template slot-scope="scope">{{$jscom.toYMDHm(scope.row.dealTime)}}</template>
        </el-table-column>
        <el-table-column label="盘点时间" align="center" prop="operateTime" width="110">
          <template slot-scope="scope">{{$jscom.toYMDHm(scope.row.operateTime)}}</template>
        </el-table-column>
        <el-table-column label="盘点人" width="80" prop="operateBy"></el-table-column>

        <el-table-column label="操作" fixed="right" align="center" width="80">
          <template slot-scope="scope">
            <div v-if="scope.row.status == '差异处理中'">
              <el-button type="primary" size="mini" @click="viewdiff(scope.row,1)">差异处理</el-button>
            </div>
            <div v-else>
              <el-button type="success" size="mini" @click="viewdiff(scope.row,2)">查看详情</el-button>
            </div>
          </template>
        </el-table-column>
        <!-- Table数据 -->
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

    <!-- 盘库差异处理 -->
    <section id="viewdiff">
      <rg-dialog
        :title="diffformTitle"
        :visible.sync="diffformVisible"
        :beforeClose="diffcancel"
        :btnCancel="{label:'关闭',click:(done)=>{diffcancel('stockDiffModel')}}"
        :btnRemove="false"
        :footbar="true"
        width="90%"
        v-if="diffformVisible"
        maxWidth="900px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-form
            :model="stockDiffModel"
            :rules="rules"
            ref="stockDiffModel"
            label-width="120px"
            size="mini"
            :disabled="disEditable"
          >
            <el-form-item label="盘点计划">
              <span>{{stockDiffModel.planNo}}</span>
            </el-form-item>

            <el-form-item label="盘点计划名称">
              <span>{{stockDiffModel.planName}}</span>
            </el-form-item>
            <el-form-item label="盘点类型">
              <label v-if="disEditable">{{["","指定产品","全盘"][stockDiffModel.planType1]}}</label>
              <el-radio-group v-else v-model="stockDiffModel.planType1">
                <el-radio disabled :label="1">指定产品</el-radio>
                <el-radio disabled :label="2">全盘</el-radio>
              </el-radio-group>
            </el-form-item>
            <el-form-item label="盘点提交人">
              <label>{{stockDiffModel.operateBy}}</label>
            </el-form-item>

            <el-form-item label="产品编号">
              <label>{{stockDiffModel.productId}}</label>
            </el-form-item>
            <el-form-item label="产品名称">
              <label>{{stockDiffModel.productName}}</label>
            </el-form-item>
            <el-form-item label="产品分类">
              <label>{{stockDiffModel.productType}}</label>
            </el-form-item>
            <el-form-item label="系统数量">
              <label>{{stockDiffModel.systemNum}}个</label>
            </el-form-item>

            <el-form-item label="盘点数量">
              <label>{{stockDiffModel.storageNum}}个</label>&nbsp;&nbsp;&nbsp;&nbsp;
              <span
                v-if="stockDiffModel.diffNum>0"
                style="background-color:orange;color:white;padding:5px 10px;"
              >盘盈&nbsp;&nbsp;&nbsp;&nbsp;{{stockDiffModel.diffNum.format(0)}}</span>
              <span
                v-else-if="stockDiffModel.diffNum<0"
                style="background-color:red;color:white;padding:5px 10px;"
              >盘亏&nbsp;&nbsp;&nbsp;&nbsp;{{absNum(stockDiffModel.diffNum).format(0)}}</span>
            </el-form-item>

            <el-form-item label="处理方式" prop="planTime">
              <label v-if="disEditable">
                {{["","要求重盘此产品","差异确认通过"][stockDiffModel.dealType1]}}
                <span
                  v-if="stockDiffModel.dealType1 == 1"
                  class="italic gray"
                >{{$jscom.toYMDHm(stockDiffModel.planTime)}}</span>
              </label>
              <template v-else>
                <el-radio-group v-model="stockDiffModel.dealType1">
                  <el-radio :label="2">差异确认通过</el-radio>
                  <el-radio :label="1">要求重盘此产品</el-radio>
                </el-radio-group>
                <el-date-picker
                  style="margin-left:11px"
                  v-show="stockDiffModel.dealType1 == 1"
                  v-model="stockDiffModel.planTime"
                  type="datetime"
                  :picker-options="pickerOptions"
                  placeholder="选择最晚完成盘点时间"
                  default-time="12:00:00"
                ></el-date-picker>
              </template>
            </el-form-item>
            <el-form-item label="处理备注">
              <label v-if="disEditable">{{stockDiffModel.dealRemark}}</label>
              <el-input
                v-else
                v-model="stockDiffModel.dealRemark"
                type="textarea"
                :rows="5"
                style="width:495px;"
                autocomplete="off"
                placeholder="请输入备注"
                clearable
              ></el-input>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            :disabled="disEditable"
            @click="diffsave('stockDiffModel')"
          >提交</el-button>
        </template>
      </rg-dialog>
    </section>
    <!-- 盘库差异处理 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/wms/storage";
import { Loading } from "element-ui";
import { isNumber } from "util";

export default {
  name: "demopage",
  data() {
    return {
      pickerOptions: {
        disabledDate(time) {
          const date = new Date();
          date.setTime(date.getTime() - 3600 * 1000 * 24);
          return time.getTime() < date;
        },
      },
      w_search_right: 100,
      tableLoading: false,
      condition: {
        productId: undefined,
        type: undefined,
        pageIndex: 1,
        pageSize: 10,
        times: [],
        status: undefined,
        tempStatus: undefined,
        tempType: undefined,
        planNo: undefined,
        sourceType: undefined,
        locationId:undefined
      },
      sourceTypeSel: [
        {
          value: "Company",
          label: "铺货商品库",
        },
        {
          value: "Shop",
          label: "外采商品库",
        },
      ],
      currentPage: 1,
      tableData: [],
      warehouseSel:[],
      totalList: 0,
      typeSel: [
        {
          key: "1",
          value: "盘盈亏",
        },
        {
          key: "2",
          value: "盘亏",
        },
        {
          key: "3",
          value: "盘盈",
        },
      ],
      statusSel: [
        {
          key: "待确认",
          value: "待确认",
        },
        {
          key: "已确认",
          value: "已确认",
        },
        {
          key: "重盘",
          value: "重盘",
        },
      ],
      disEditable: false,
      diffcondition: {
        id: undefined,
      },
      stockDiffModel: {
        id: undefined,
        planNo: undefined,
        planName: undefined,
        planType: undefined,
        planId: undefined,
        productId: undefined,
        productName: undefined,
        systemNum: undefined,
        storageNum: undefined,
        diffNum: undefined,
        operateBy: undefined,
        operateTime: undefined,
        dealType: undefined,
        dealRemark: undefined,
        planType1: undefined,
        dealType1: undefined,
        productType: undefined,
        planTime: undefined,
      },
      stockDiffModelInit: {
        id: undefined,
        planNo: undefined,
        planName: undefined,
        planType: undefined,
        planId: undefined,
        productId: undefined,
        productName: undefined,
        systemNum: undefined,
        storageNum: undefined,
        diffNum: undefined,
        operateBy: undefined,
        operateTime: undefined,
        dealType: undefined,
        dealRemark: undefined,
        planType1: undefined,
        dealType1: undefined,
        productType: undefined,
        planTime: undefined,
      },
      diffformTitle: "",
      diffformVisible: false,
      rules: {
        planTime: [
          {
            type: "date",
            validator: (rule, value, callback) => {
              if (
                !this.disEditable &&
                this.stockDiffModel.dealType1 == 1 &&
                !value
              ) {
                return callback(new Error("请选择时间"));
              }
            },
            trigger: "change",
          },
        ],
      },
    };
  },
  created() {
    this.fetchData();
  },
  watch: {},
  methods: {
     getwarehouses() {
      appSvc
        .getWarehouses()
        .then(
          res => {
            this.warehouseSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    changeType() {
      if (this.condition.tempType == "") {
        this.condition.type = undefined;
      }
    },
    changeStatus() {
      if (this.condition.tempStatus == "") {
        this.condition.status = undefined;
      }
    },

    absNum(num) {
      return Math.abs(num);
    },
    diffcancel(formName) {
      this.diffformVisible = false;
      this.stockDiffModel = JSON.parse(JSON.stringify(this.stockDiffModelInit));
      this.disEditable = false;
    },
    diffsave(formName) {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          let isValid = true;
          this.$refs[formName].validate((valid) => {
            if (!valid) {
              isValid = false;
              return false;
            }
          });
          if (isValid == false) return false;

          var dealType = "";
          if (this.stockDiffModel.dealType1 == 1) {
            dealType = "要求重盘此产品";
          } else if (this.stockDiffModel.dealType1 == 2) {
            dealType = "差异确认通过";
          }

          this.stockDiffModel.dealType = dealType;

          var vm = this;
          appSvc
            .dealStorageProduct(this.stockDiffModel)
            .then(
              (res) => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                  vm.search();
                  vm.diffcancel();
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
        })
        .catch((error) => {
          console.log(error);
        });
    },
    viewdiff(row, type) {
      if (type == 1) {
        this.diffformTitle = "盘库差异处理";
        this.disEditable = false;
      } else {
        this.diffformTitle = "查看盘库差异";
        this.disEditable = true;
      }

      this.diffformVisible = true;
      this.diffcondition.id = row.id;
      appSvc
        .getStockDiffDetail(this.diffcondition)
        .then(
          (res) => {
            this.stockDiffModel = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    fetchData() {
      this.getwarehouses();
      appSvc
        .getStockDiffs(this.condition)
        .then(
          (res) => {
            //  this.tableData = res.data;
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;

            if (res.message != "") {
              this.$message({
                message: res.message,
                type: "success",
              });
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    // pageTurning() {
    //   this.condition.pageIndex = this.currentPage;
    //   this.getPaged();
    // },


    getPaged(flag) {
      debugger;
      this.tableLoading = true;

      if (this.condition.productId == "") {
        this.condition.productId = undefined;
      }
      if(this.condition.locationId == ""){
        this.condition.locationId = undefined;
      }

      if (this.condition.tempType == "") {
        this.condition.tempType = undefined;
      }
      //状态做转换
      if (
        this.condition.tempStatus != "" &&
        this.condition.tempStatus != undefined
      ) {
        var status = "";
        if (this.condition.tempStatus == "待确认") {
          status = "差异处理中";
        } else if (this.condition.tempStatus == "已确认") {
          status = "盘点完成";
        } else if (this.condition.tempStatus == "重盘") {
          status = "无效";
        }
        this.condition.status = status;
      }

      //盘盈亏类型转换
      if (
        this.condition.tempType != "" &&
        this.condition.tempType != undefined
      ) {
        var type = -1;
        if (this.condition.tempType == "盘盈亏") {
          type = 1;
        } else if (this.condition.tempType == "盘亏") {
          type = 2;
        } else if (this.condition.tempType == "盘盈") {
          type = 3;
        }
        this.condition.type = type;
      }

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getStockDiffs(this.condition)
        .then(
          (res) => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;

            //查询成功后 清除选中值
            this.condition.status = undefined;
            this.condition.type = undefined;

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
    height: 48px;
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