<template>
  <div class="child2">
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
          :cell-style="cellStyle"
          :defaultCollapse="false"
        >
          <template v-slot:condition>
            <el-form-item prop="id">
              <el-input
                v-model="condition.id"
                clearable
                @change="updateCommentId()"
                placeholder="请输入评论单号"
              />
            </el-form-item>
            <el-form-item prop="orderNo">
              <el-input
                v-model="condition.orderNo"
                clearable
                @change="updateOrderId()"
                placeholder="请输入订单号"
              />
            </el-form-item>
            <el-form-item prop="shopId">
              <el-select
                v-model="condition.shopId"
                @change="updateLocation"
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
            <el-form-item prop="checkStatus">
              <el-select v-model="condition.checkStatus" clearable filterable placeholder="请选择审核状态">
                <el-option
                  v-for="item in checkStatusSel"
                  :key="item.key"
                  :label="item.value"
                  :value="item.key"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item prop="channel">
              <el-select v-model="condition.channel" clearable filterable placeholder="请选择订单渠道">
                <el-option
                  v-for="item in channelSel"
                  :key="item.key"
                  :label="item.value"
                  :value="item.key"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item prop="orderType">
              <el-select v-model="condition.orderType" clearable filterable placeholder="请选择订单类型">
                <el-option
                  v-for="item in orderTypeSel"
                  :key="item.key"
                  :label="item.value"
                  :value="item.key"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item prop="createTime">
              <template>
                <el-date-picker
                  v-model="condition.createTime"
                  type="datetimerange"
                  :picker-options="sendpickerOptions"
                  range-separator="至"
                  start-placeholder="开始日期"
                  end-placeholder="结束日期"
                  align="right"
                ></el-date-picker>
              </template>
            </el-form-item>
          </template>
          <!-- <el-input v-model="condition.name" clearable class="el-input el-input--suffix" placeholder="请输入系统名称"></el-input> -->

          <template v-slot:tb_cols>
            <el-table-column type="expand">
              <template slot-scope="props">
                <el-form label-position="left" inline class="demo-table-expand">
                  <el-form-item label="审核意见" v-show="props.row.checkStatus == 0 ? false : true">
                    <span>{{ props.row.checkComment }}</span>
                  </el-form-item>
                  <el-form-item label="评论内容">
                    <span>{{ props.row.content }}</span>
                  </el-form-item>

                  <el-form-item label="门店图片">
                    <img :src="props.row.shopImageUrl" min-width="150" height="150" />
                  </el-form-item>

                  <el-form-item label="评论图片">
                    <img
                      :src="item"
                      v-for="(item, index) in props.row.imageList"
                      style="width: 150px; height: 150px;padding:5px;"
                      v-bind:key="item"
                    />
                  </el-form-item>
                </el-form>
              </template>
            </el-table-column>
            <el-table-column label="评论单号" prop="id"></el-table-column>

            <el-table-column label="订单号" prop="orderNo"></el-table-column>

            <el-table-column label="订单类型" prop="orderTypeStr"></el-table-column>
            <el-table-column label="订单渠道" prop="channelStr"></el-table-column>

            <el-table-column label="门店名称" prop="shopName"></el-table-column>

            <el-table-column label="审核状态" prop="checkStatusStr"></el-table-column>

            <el-table-column label="评论分值" prop="score"></el-table-column>
            <el-table-column label="创建时间" prop="createTime"></el-table-column>
            <el-table-column label="操作">
              <template slot-scope="scope">
                <el-button
                  type="text"
                  size="small"
                  :disabled="scope.row.isCheck ? true : false"
                  @click="checkComment(scope.row)"
                >审核</el-button>
              </template>
            </el-table-column>
          </template>
        </rg-page>
      </section>
      <!-- 首页 -->

      <!-- 标记异常窗口 -->
      <section id="checkDialog">
        <rg-dialog
          :title="checkFormTitle"
          :visible.sync="checkFormVisible"
          :beforeClose="checkCancel"
          :btnCancel="{label:'关闭',click:(done)=>{checkCancel()}}"
          :btnRemove="false"
          :footbar="true"
          width="100%"
          maxWidth="800px"
          minWidth="600px"
        >
          <template v-slot:content>
            <el-form :model="checkFormModel" ref="checkFormModel">
              <el-form-item label="审核意见" :label-width="formLabelWidth" >
                <el-input
                  type="textarea"
                  :rows="5"
                  placeholder="请输入审核意见"
                  v-model="checkFormModel.checkComment"
                ></el-input>
              </el-form-item>
            </el-form>
          </template>
          <template v-slot:footer>
            <el-button type="primary" @click="checkSave('checkFormModel', 'pass')" size="mini">通过</el-button>
            <el-button type="warning" @click="checkSave('checkFormModel', 'reject')" size="mini">驳回</el-button>
          </template>
        </rg-dialog>
      </section>
      <!-- 标记异常窗口 -->
    </main>
  </div>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appSvc } from "@/api/ordercomment/ordercomment";

import { isNumber } from "util";

export default {
  name: "child2",
  mounted() {
    console.log("tab1组件");
  },
  data() {
    return {
      pickerOptionsStart: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        }
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
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit("pick", [start, end]);
            }
          }
        ]
      },
      input: undefined,
      actualInStockNum: 1,
      readonly: true,
      tableLoading: false,
      currentPage: 1,
      loading: false,
      //table页面的条件
      condition: {
        id: undefined, //评论编号
        type: undefined, //评论类型
        orderNo: undefined,
        pageIndex: 1,
        pageSize: 10,
        userId: undefined,
        shopId: undefined,
        checkStatus: undefined, //审核状态
        channel: undefined,
        orderType: undefined,
        createTime: undefined,
        productName: undefined,
        createTime: undefined
      },
      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",

      formLabelWidth: "120px",
      //下拉框的条件
      commmentTypeSel: [],
      commmentTypeSelCondition: {
        requestType: 1
      },

      shopSel: [],
      shopSelCondition: {
        shopName: undefined
      },
      checkStatusSel: [],
      checkStatusSelCondition: {
        requestType: 2
      },
      channelSel: [],
      channelSelCondition: {
        requestType: 3
      },

      orderTypeSel: [],
      orderTypeSelCondition: {
        requestType: 4
      },

      //下拉框的条件
      checkFormTitle: "",
      checkFormVisible: false,
      checkFormModel: {
        checkComment: undefined,
        commentId: undefined,
        checkStatus: undefined
      },
      checkFormModelInit: {
        checkComment: undefined,
        commentId: undefined,
        checkStatus: undefined
      }
    };
  },
  computed: {
    routeComputed: {
      get: function() {
        return this.formModel.route;
      },
      set: function(val) {
        this.formModel.route = val.toLowerCase();
      }
    }
  },
  created() {
    this.fetchData();
  },
  methods: {
    checkComment(row) {
      this.checkFormTitle = "评论审核";
      this.checkFormVisible = true;
      this.checkFormModel.commentId = row.id;
    },

    updateLocation() {
      if (this.condition.shopId == "") {
        this.condition.shopId = undefined;
      }
    },
    updateCommentId() {
      if (this.condition.id == "") {
        this.condition.id = undefined;
      }
    },
    getShopinfo(query) {
      if (query != "" && query != undefined) {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.shopName = query;
          appSvc
            .getShopListAsync(this.shopSelCondition)
            .then(
              res => {
                var resData = res.data;
                this.shopSel = resData.items;
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        }, 200);
      } else {
        this.options = [];
      }
    },

    checkSave(formName, type) {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          debugger;

          this.checkFormModel.checkStatus = 1;
          if (type == "reject") {
            this.checkFormModel.checkStatus = 2;
            if (
              this.checkFormModel.checkComment == "" ||
              this.checkFormModel.checkComment == undefined
            ) {
              this.$message({
                message: "请填写审核意见!",
                type: "warning"
              });
              return;
            }
          }

          var vm = this;

          appSvc
            .checkOrderComment(this.checkFormModel)
            .then(
              res => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  vm.search();
                  this.checkFormVisible = false;

                  this.checkFormModel = JSON.parse(
                    JSON.stringify(this.checkFormModelInit)
                  );
                } else {
                  this.$message({
                    message: res.message,
                    type: "warning"
                  });
                }
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },
    checkCancel(formName) {
      this.checkFormVisible = false;

      this.checkFormModel = JSON.parse(JSON.stringify(this.checkFormModelInit));
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
    //评论类型
    getCommentType() {
      appSvc
        .getBaseInfos(this.commmentTypeSelCondition)
        .then(
          res => {
            this.commmentTypeSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //审核状态
    getCheckStatus() {
      appSvc
        .getBaseInfos(this.checkStatusSelCondition)
        .then(
          res => {
            this.checkStatusSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //订单渠道
    getChannel() {
      appSvc
        .getBaseInfos(this.channelSelCondition)
        .then(
          res => {
            this.channelSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //订单类型
    getOrderType() {
      appSvc
        .getBaseInfos(this.orderTypeSelCondition)
        .then(
          res => {
            this.orderTypeSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    fetchData() {
      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getOrderCommentForShopList(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;

            if (flag) {
              if (res.message != "") {
                this.$message({
                  message: res.message,
                  type: "success"
                });
              }
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          console.log("tableLoading: false");
          this.tableLoading = false;
        });
      this.getCommentType();
      this.getCheckStatus();
      this.getChannel();
      this.getOrderType();
    },
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    getPaged(flag) {
      this.tableLoading = true;

      debugger;

      if (this.condition.id == "") {
        this.condition.id = undefined;
      }

      if (this.condition.orderNo == "") {
        this.condition.orderNo = undefined;
      }
      if (this.condition.shopId == "") {
        this.condition.shopId = undefined;
      }
      if (this.condition.type == "") {
        this.condition.type = undefined;
      }
      if (this.condition.checkStatus == "") {
        this.condition.checkStatus = undefined;
      }
      if (this.condition.channel == "") {
        this.condition.channel = undefined;
      }
      if (this.condition.orderType == "") {
        this.condition.orderType = undefined;
      }

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getOrderCommentForShopList(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;

            if (flag) {
              if (res.message != "") {
                this.$message({
                  message: res.message,
                  type: "success"
                });
              }
            }
          },
          error => {
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
    }
  }
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 88px;
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

.demo-table-expand {
  font-size: 0;
}
.demo-table-expand label {
  width: 90px;
  color: #99a9bf;
}
.demo-table-expand .el-form-item {
  margin-right: 0;
  margin-bottom: 0;
  width: 50%;
}
>>> .el-form--inline .el-form-item {
  padding-bottom: 10px !important ;
}
>>> .el-table {
  height: calc(100vh - 385px) !important;
}
</style>
