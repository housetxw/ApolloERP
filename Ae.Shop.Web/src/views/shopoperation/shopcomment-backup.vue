<template>
  <main class="container">
    <!-- 首页：列表数据 -->
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :headerBtnWidth="160"
      :tableData="tableData"
      :pageChange="pageChange"
      :searching="search"
      :tbRowDblClick="tbRowDblClick"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-input
            v-model="condition.id"
            size="small"
            style="width:140px"
            autocomplete="off"
            placeholder="请输入评论单号"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-input
            v-model="condition.orderNo"
            size="small"
            style="width:140px"
            autocomplete="off"
            placeholder="请输入订单号"
            clearable
          ></el-input>
        </el-form-item>
        <!-- <el-form-item>
          <el-select
            v-model="condition.shopId"
            filterable
            remote
            clearable
            reserve-keyword
            placeholder="请输入门店名称"
            :remote-method="getShopinfo"
            :loading="tableLoading"
          >
            <el-option
              v-for="item in shopSel"
              :key="item.id"
              :label="item.simpleName"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>-->
        <el-form-item>
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
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.channel"
            size="small"
            clearable
            filterable
            placeholder="请选择订单渠道"
          >
            <el-option
              v-for="item in channelSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.orderType"
            size="small"
            clearable
            filterable
            placeholder="请选择订单类型"
          >
            <el-option
              v-for="item in orderTypeSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-date-picker
            v-model="condition.createTime"
            type="datetimerange"
            :picker-options="sendpickerOptions"
            range-separator="至"
            start-placeholder="开始日期"
            end-placeholder="结束日期"
            align="right"
          ></el-date-picker>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <!-- <el-button type="primary" size="mini" icon="el-icon-plus" @click="add">新增</el-button> -->
      </template>
      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <el-table-column type="expand" label="详情">
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
                  v-for="item in props.row.imageList"
                  style="width: 150px; height: 150px;padding:5px;"
                  v-bind:key="item"
                />
              </el-form-item>
            </el-form>
          </template>
        </el-table-column>
        <el-table-column label="评论编号" prop="id"></el-table-column>
        <el-table-column label="订单号" prop="orderNo"></el-table-column>
        <el-table-column label="订单类型" prop="orderTypeStr"></el-table-column>
        <el-table-column label="订单渠道" prop="channelStr"></el-table-column>
        <el-table-column label="门店名称" prop="shopName"></el-table-column>
        <el-table-column label="审核状态" prop="checkStatusStr"></el-table-column>
        <el-table-column label="评论分值" prop="score"></el-table-column>
        <el-table-column label="创建时间" prop="createTime"></el-table-column>
        <el-table-column label="操作" fixed="right" width="180" align="center">
          <template slot-scope="scope">
            <el-button-group>
              <el-button type="primary" size="mini" @click="checkComment(scope.row)">审核</el-button>
            </el-button-group>
          </template>
        </el-table-column>
        <!-- Table数据 -->
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

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
            ></el-input>
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="checkCancel('checkFormModel')">取消</el-button>
          <el-button type="primary" @click="checkSave('checkFormModel', 'pass')">通过</el-button>
          <el-button type="warning" @click="checkSave('checkFormModel', 'reject')">驳回</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 标记异常窗口 -->
  </main>
</template>

<script>
import { appSvc } from "@/api/ordercomment/ordercomment";

export default {
  name: "shopasset",
  data() {
    return {
      //*****公共属性*****
      tableLoading: false, //表格加载动画
      formLabelWidth: "150px", //表单标签宽度
      totalList: 0, //总记录数
      formVisible: false, //表单可视
      formTitle: "详情", //表单标题
      formCheck: false, //表单可编辑
      formCheckEdit: false, //修改时表单不可编辑
      btnSaveLoading: false, //保存按钮加载动画
      tableData: [], //表格数据
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
      }, //查询条件
      formInit: {
        formTypes: [],
        id: 0
      }, //表单初始化数据
      formModel: {
        formTypes: [],
        id: 0
      }, //表单数据
      rules: {
        remark: [
          { max: 200, message: "备注长度不允许超过200", trigger: "blur" }
        ]
      }, //校验规则

      //*****自定义属性*****
      shopSel: [],
      shopSelCondition: {
        shopName: undefined
      },
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
  created() {
    this.getConditionData(); //获取查询条件数据
    this.fetchData(); //页面首次加载时默认搜索
  },
  watch: {
   
  },
  methods: {
    //*****自定义方法*****
    //门店查询
    getShopinfo(query) {
      if (query != "" && query != undefined) {
        this.tableLoading = true;
        setTimeout(() => {
          this.tableLoading = false;
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
    isEmpty(property) {
      return (
        property === null || property === "" || typeof property === "undefined"
      );
    },
    checkComment(row) {
      this.checkFormTitle = "评论审核";
      this.checkFormVisible = true;
      this.checkFormModel.commentId = row.id;
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
    //*****固定方法*****
    fetchData() {
      this.getPaged();
    }, //列表查询
    getConditionData() {
      this.getCommentType();
      this.getCheckStatus();
      this.getChannel();
      this.getOrderType();
    }, //获取查询条件
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    }, //分页器改变事件
    getPaged(flag) {
      this.tableLoading = true;
      appSvc
        .getOrderCommentForShopListForShopApi(this.condition)
        .then(
          res => {
            let data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
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
      this.condition.pageIndex = 1;
      this.getPaged(flag);
    },
    tbRowDblClick(row, column, event, tb) {
      if (row.isDeleted) {
        return this.$alertWarning(`已删除，不允许查看详情！`);
      }
      this.resetForm();
      tb.toggleRowSelection(row);
      tb.clearSelection();
      this.formTitle = "详情";
      this.formVisible = true;
      this.formCheck = true;
      //this.getShopAsset(row.id);
    },
    add() {
      this.resetForm();
      this.formTitle = "新增";
      this.formVisible = true;
    }, //新增弹窗
    edit(sel) {
      this.resetForm();
      this.formTitle = "修改";
      this.formVisible = true;
      this.formCheckEdit = true;
      //this.getShopAsset(sel.id);
    }, //编辑弹窗
    save(formName) {
      var vm = this;
      this.$refs[formName].validate(valid => {
        if (valid) {
          vm.btnSaveLoading = true;
        }
      });
    },
    deleted(sel) {
      let vm = this;
      let msg = `是否要删除?`;
      this.$confirmWarning(msg).then(
        () => {
          this.tableLoading = true;
        },
        cancel => {
          console.warn(cancel);
        }
      );
    },
    cancel(formName) {
      this.formVisible = false;
      this.formCheck = false;
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
      this.resetForm();
    },
    resetForm() {
      // this.formModel.formTypes = [];
      // this.formModel = this.formInit;
    }, //重置表单
    resetSel() {
      // this.conditionTypes = [];
      // this.startUseDates = [];
    } //重置查询条件
  }
};
</script>
