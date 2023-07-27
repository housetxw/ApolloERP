<template>
  <div class="child4">
    <main class="container">
      <!-- 首页 -->
      <section id="indexPage">
        <header class="bodyTop">
          <article class="filter-container">
            <span class="input-label">评论单号:</span>
            <el-input
              v-model="condition.id"
              clearable
              @change="updateCommentId()"
              placeholder="请输入评论单号"
              style="width:200px;"
            />

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span class="input-label"
              >订单号:</span
            >
            <el-input
              v-model="condition.orderNo"
              clearable
              @change="updateOrderId()"
              placeholder="请输入订单号"
              style="width:200px;"
            />
            &nbsp;&nbsp;&nbsp; <span class="input-label">门店名称:</span>
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
              >
              </el-option>
            </el-select>

            &nbsp;&nbsp;&nbsp;<span class="input-label">评论类型:</span>
            <el-select
              v-model="condition.type"
              size="small"
              clearable
              filterable
              placeholder="请选择评论类型"
              class="margin-right-10"
            >
              <el-option
                v-for="item in commmentTypeSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              >
              </el-option>
            </el-select>

            &nbsp;<span class="input-label">审核状态:</span>
            <el-select
              v-model="condition.checkStatus"
              size="small"
              clearable
              filterable
              placeholder="请选择审核状态"
              class="margin-right-10"
            >
              <el-option
                v-for="item in checkStatusSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              >
              </el-option>
            </el-select>
            <br />
            <span class="input-label">订单渠道:</span>
            <el-select
              v-model="condition.channel"
              size="small"
              clearable
              filterable
              placeholder="请选择订单渠道"
              class="margin-right-10"
            >
              <el-option
                v-for="item in channelSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              >
              </el-option>
            </el-select>

            <span class="input-label">订单类型:</span>
            <el-select
              v-model="condition.orderType"
              size="small"
              clearable
              filterable
              placeholder="请选择订单类型"
              class="margin-right-10"
            >
              <el-option
                v-for="item in orderTypeSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              >
              </el-option>
            </el-select>

           
            <span class="input-label">评论时间:</span>
            <template>
              <el-date-picker
                v-model="condition.createTime"
                type="datetimerange"
                :picker-options="sendpickerOptions"
                range-separator="至"
                start-placeholder="开始日期"
                end-placeholder="结束日期"
                align="right"
              >
              </el-date-picker>
            </template>

         
            <el-button
              type="primary"
              size="small"
              style="margin-left:30px;"
              @click="search(true)"
              icon="el-icon-search"
              >搜索</el-button
            >
          </article>
        </header>
        <main>
          <section>
            <el-table
              border
              :data="tableData"
              style="width: 100%"
              :cell-style="cellStyle"
            >
              <el-table-column type="expand">
                <template slot-scope="props">
                  <el-form
                    label-position="left"
                    inline
                    class="demo-table-expand"
                  >
                    
                     
                      <el-form-item
                        label="审核意见"
                        v-show="props.row.checkStatus == 0 ? false : true"
                      >
                        <span>{{ props.row.checkComment }}</span>
                      </el-form-item>
                      <el-form-item label="评论内容">
                        <span>{{ props.row.content }}</span>
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
              

              <el-table-column
                label="评论类型"
                prop="replyTypeStr"
              ></el-table-column>

              <el-table-column label="订单号" prop="orderNo"></el-table-column>

              <el-table-column
                label="订单类型"
                prop="orderTypeStr"
              ></el-table-column>
              <el-table-column
                label="订单渠道"
                prop="channelStr"
              ></el-table-column>

              <el-table-column
                label="门店名称"
                prop="shopName"
              ></el-table-column>

              <el-table-column
                label="审核状态"
                prop="checkStatusStr"
              ></el-table-column>
              <el-table-column
                label="创建时间"
                prop="createTime"
              ></el-table-column>
              <el-table-column label="操作">
                <template slot-scope="scope">
                  <el-button
                    type="text"
                    size="small"
                    :disabled="scope.row.isCheck ? true : false"
                    @click="checkComment(scope.row)"
                    >审核</el-button
                  >
                </template>
              </el-table-column>
            </el-table>
          </section>
          <section class="pagination">
            <el-pagination
              background
              :page-size="10"
              :page-sizes="[10, 30, 50, 100]"
              layout="total, sizes, prev, pager, next, jumper"
              :total="totalList"
              @current-change="pageTurning"
              :current-page.sync="currentPage"
              @size-change="sizeChange"
            ></el-pagination>
          </section>
        </main>
      </section>
      <!-- 首页 -->

      <!-- 标记异常窗口 -->
      <section id="checkDialog">
        <el-dialog
          width="500px"
          :title="checkFormTitle"
          :close-on-click-modal="false"
          :visible.sync="checkFormVisible"
          :before-close="checkCancel"
        >
          <el-form :model="checkFormModel" ref="checkFormModel">
            <el-form-item label="审核意见:">
              <el-input
                type="textarea"
                :rows="5"
                placeholder="请输入审核意见"
                v-model="checkFormModel.checkComment"
              >
              </el-input>
            </el-form-item>
          </el-form>
          <div slot="footer" class="dialog-footer">
            <el-button @click="checkCancel('checkFormModel')">取消</el-button>
            <el-button
              type="primary"
              @click="checkSave('checkFormModel', 'pass')"
              >通过</el-button
            >
            <el-button
              type="warning"
              @click="checkSave('checkFormModel', 'reject')"
              >驳回</el-button
            >
          </div>
        </el-dialog>
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
  name: "child4",
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
        createTime: undefined,
        techName: undefined,
        techLevel: undefined
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
        requestType: 6
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
      techSel: [],
      techSelCondition: {
        requestType: 5
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
    formatComment(row, column, cellValue) {
      debugger;
      if (cellValue === 1) {
        return "产品";
      } else if (cellValue === 2) {
        return "门店";
      } else if (cellValue === 3) {
        return "技师";
      }
    },

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
        debugger;
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
            .checkOrderReply(this.checkFormModel)
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
     

      // if (row.row.status === "驳回" && row.column.label === "状态") {
      //   return "color:orange";
      // }
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
    //技师级别
    getTechLevel() {
      appSvc
        .getBaseInfos(this.techSelCondition)
        .then(
          res => {
            this.techSel = res.data;
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
        .getOrderCommentForReplyList(this.condition)
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
      this.getTechLevel();
    },
    sizeChange(val) {
      this.condition.pageIndex = this.currentPage = 1;
      this.condition.pageSize = val;
      this.getPaged();
    },
    pageTurning() {
      this.condition.pageIndex = this.currentPage;
      this.getPaged();
    },

    getPaged(flag) {
      this.tableLoading = true;
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

      if (this.condition.techName == "") {
        this.condition.techName = undefined;
      }

      if (this.condition.techLevel == "") {
        this.condition.techLevel = undefined;
      }

      if (this.condition.productName == "") {
        this.condition.productName = undefined;
      }

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getOrderCommentForReplyList(this.condition)
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
    height: 135px;
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
</style>
