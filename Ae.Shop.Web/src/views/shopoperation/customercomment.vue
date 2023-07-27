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
          <el-select
            v-model="condition.orderType"
            size="small"
            style="width:100px"
            autocomplete="off"
            placeholder="订单类型"
            clearable
          >
            <el-option
              v-for="item in orderTypeOptions"
              :key="item.code"
              :label="item.name"
              :value="item.code"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.scoreLevel"
            size="small"
            style="width:100px"
            autocomplete="off"
            placeholder="评分级别"
            clearable
          >
            <el-option
              v-for="item in scoreLevelOptions"
              :key="item.code"
              :label="item.name"
              :value="item.code"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.replyStatus"
            size="small"
            style="width:100px"
            autocomplete="off"
            placeholder="回复状态"
            clearable
          >
            <el-option
              v-for="item in replyStatusOptions"
              :key="item.code"
              :label="item.name"
              :value="item.code"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <!-- <el-button type="primary" size="mini" icon="el-icon-plus" @click="add">新增</el-button> -->
      </template>
      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <el-table-column label="评论编号" prop="id"></el-table-column>
        <el-table-column label="订单编号" prop="orderNo"></el-table-column>
        <el-table-column label="创建时间" prop="createTime"></el-table-column>
        <el-table-column label="门店评价" prop="shopScore">
          <template slot-scope="scope">
            <el-rate v-model="scope.row.shopScore" :allow-half="true" disabled text-color="#ff9900"></el-rate>
          </template>
        </el-table-column>
        <el-table-column label="评分内容" prop="content" :show-overflow-tooltip="true"></el-table-column>
        <el-table-column type="expand" label="详情">
          <template slot-scope="props">
            <el-form label-position="left" inline class="demo-table-expand">
              <el-form-item label="头像" prop="headUrl">
                <el-avatar style="width: 100px; height: 100px" :src="props.row.headUrl"></el-avatar>
              </el-form-item>
              <el-form-item label="昵称">
                <span>{{ props.row.nickName }}</span>
              </el-form-item>
              <el-form-item label="评论图片">
                <div class="demo-image__preview">
                  <el-image
                    style="width: 100px; height: 100px"
                    :src="item.url"
                    :preview-src-list="getImgUrls(props.row.imgList)"
                    v-for="item in props.row.imgList"
                    v-bind:key="item.url"
                  ></el-image>
                </div>
              </el-form-item>
            </el-form>
            <el-table border :data="props.row.replyInfos" style="width: 90%">
              <el-table-column label="回复类型" prop="displayTitle" style="width: 20%"></el-table-column>
              <el-table-column label="回复内容" prop="replyContent" style="width: 80%"></el-table-column>
            </el-table>
          </template>
        </el-table-column>
        <el-table-column label="操作" fixed="right" width="180" align="center">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                type="primary"
                size="mini"
                @click="replyComment(scope.row,'ReplyComment')"
                v-if="canButtonShow(scope.row,'ReplyComment')"
              >回复评价</el-button>
              <el-button
                type="warning"
                size="mini"
                @click="replyComment(scope.row,'ReplyAppendComment')"
                v-if="canButtonShow(scope.row,'ReplyAppendComment')"
              >回复追评</el-button>
            </el-button-group>
          </template>
        </el-table-column>
        <!-- Table数据 -->
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

    <!-- 操作 -->
    <el-dialog
      :title="dialogTitle"
      :visible.sync="dialogVisible"
      width="30%"
      :before-close="handleClose"
    >
      <el-form
        :model="replyCommentRequest"
        :rules="rules"
        ref="replyCommentRequest"
        label-width="100px"
        class="demo-ruleForm"
      >
        <el-alert title="回复内容提交后需平台审核后可见，请提交后耐心等待！" type="warning" :closable="false"></el-alert>
        <el-form-item label="回复内容" prop="replyContent">
          <el-input
            type="textarea"
            v-model="replyCommentRequest.replyContent"
            placeholder="请输入回复内容"
          ></el-input>
        </el-form-item>
      </el-form>
      <span slot="footer" class="dialog-footer">
        <el-button @click="handleClose">取 消</el-button>
        <el-button type="primary" @click="handleSubmit">提 交</el-button>
      </span>
    </el-dialog>
    <!-- 操作 -->
  </main>
</template>

<script>
import { appSvc } from "@/api/ordercomment/ordercomment";

export default {
  name: "customercomment",
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
        pageIndex: 1,
        pageSize: 10,
        orderType: undefined,
        scoreLevel: undefined,
        replyStatus: undefined
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
        replyContent: [
          { required: true, message: "请填写回复内容", trigger: "blur" },
          { max: 500, message: "回复内容长度不允许超过500", trigger: "blur" }
        ]
      }, //校验规则

      //*****自定义属性*****
      orderTypeOptions: [],
      scoreLevelOptions: [],
      replyStatusOptions: [],
      dialogTitle: "",
      dialogVisible: false,
      replyCommentRequest: {
        replyType: undefined, //使用到的枚举值：ReplyComment ReplyAppendComment
        replyToCommentId: 0,
        replyContent: undefined
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
    canButtonShow(row, buttonName) {
      var index = (row.replyButtons || []).findIndex(
        t => t.function === buttonName
      );
      return index > -1;
    },
    replyComment(row, buttonName) {
      var index = (row.replyButtons || []).findIndex(
        t => t.function === buttonName
      );
      if (index <= -1) {
        return;
      }
      var replyButton = row.replyButtons[index];
      this.dialogTitle = replyButton.showFunctionName;
      this.replyCommentRequest.replyToCommentId = replyButton.commentId;
      this.replyCommentRequest.replyType = buttonName;
      this.dialogVisible = true;
    },
    handleClose(done) {
      this.$confirm("确认关闭？")
        .then(_ => {
          this.dialogVisible = false;
          this.replyCommentRequest.replyType = undefined;
          this.replyCommentRequest.replyToCommentId = 0;
          this.replyCommentRequest.replyContent = undefined;
          done();
        })
        .catch(_ => {});
    },
    handleSubmit() {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          appSvc
            .replyComment(this.replyCommentRequest)
            .then(
              res => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  vm.search();
                  this.dialogVisible = false;
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
    getImgUrls(imgList) {
      var urls = [];
      if (imgList != undefined) {
        for (var i = 0; i < imgList.length; i++) {
          urls.push(imgList[i].url);
        }
      }
      return urls;
    },
    isEmpty(property) {
      return (
        property === null || property === "" || typeof property === "undefined"
      );
    },
    cellStyle(row, column, rowIndex, columnIndex) {},
    //*****固定方法*****
    fetchData() {
      this.getPaged();
    }, //列表查询
    getConditionData() {}, //获取查询条件
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    }, //分页器改变事件
    getPaged(flag) {
      this.tableLoading = true;
      appSvc
        .getCommentList(this.condition)
        .then(
          res => {
            let data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            this.orderTypeOptions = data.orderTypeSelectItems;
            this.scoreLevelOptions = data.scoreLevelSelectItems;
            this.replyStatusOptions = data.replyStatusSelectItems;
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
    }, //编辑弹窗
    save(formName) {
      var vm = this;
      this.$refs[formName].validate(valid => {
        if (valid) {
          vm.btnSaveLoading = true;
        }
      });
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
