<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <header class="bodyTop">
        <article class="filter-container">
          <span class="input-label">审核状态:</span>
          <el-select
            v-model="condition.checkStatus"
            size="small"
            clearable
            filterable
            placeholder="请选择审核状态"
          >
            <el-option
              v-for="item in checkStatueSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>

          <span class="input-label">上下架状态:</span>
          <el-select
            v-model="condition.onlineStatus"
            size="small"
            clearable
            filterable
            placeholder="请选择上下架状态"
          >
            <el-option
              v-for="item in onlineStatusSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>

          <el-button
            type="primary"
            size="small"
            style="margin-left:30px;"
            @click="search(true)"
            icon="el-icon-search"
          >搜索</el-button>
          <el-button type="primary" size="small" @click="showCreate('create')">新增</el-button>
        </article>
      </header>
      <main>
        <section>
          <el-table border :data="tableData" style="width: 100%" :cell-style="cellStyle">
            <el-table-column label="编号" prop="id" width="80px;"></el-table-column>

            <el-table-column label="审核状态" width="100px">
              <template slot-scope="scope">
                <div v-if="scope.row.checkStatus === 0">
                  <label>待审核</label>
                </div>
                <div v-else-if="scope.row.checkStatus === 1">
                  <label>通过</label>
                </div>
                <div v-else-if="scope.row.checkStatus === 2">
                  <label style="color:red;">驳回</label>
                </div>
              </template>
            </el-table-column>

            <el-table-column label="上下架状态" width="110px">
              <template slot-scope="scope">
                <div v-if="scope.row.onlineStatus === 0">
                  <label style="color:red">下架</label>
                </div>
                <div v-else-if="scope.row.onlineStatus === 1">
                  <label>上架</label>
                </div>
              </template>
            </el-table-column>
            <el-table-column label="发布时间">
              <template slot-scope="scope">
                <label v-show="scope.row.onlineStatus === 1?true:false">{{scope.row.publishTime}}</label>
              </template>
            </el-table-column>

            <el-table-column label="标题" prop="title"></el-table-column>
            <el-table-column label="创建人" prop="createBy"></el-table-column>
            <el-table-column label="创建时间" prop="createTime"></el-table-column>
            <el-table-column label="操作" width="150px">
              <template slot-scope="scope">
                <el-button
                  type="text"
                  size="small"
                  v-show="scope.row.onlineStatus == 1?true:false"
                  :disabled="scope.row.onlineStatus == 0?true:false"
                  @click="onlinearticle(scope.row,0)"
                >下架</el-button>
                <el-button
                  type="text"
                  size="small"
                  v-show="scope.row.onlineStatus == 0?true:false"
                  @click="onlinearticle(scope.row,1)"
                >上架</el-button>
                <el-button type="text" size="small" @click="deletearticle(scope.row)">删除</el-button>
                <el-button type="text" size="small" @click="showDetail(scope.row)">详情</el-button>
                <el-button type="text" size="small" @click="reviewarticle(scope.row)">预览</el-button>
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
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/activity/promote";
import Tinymce from "@/components/Tinymce";
import { isNumber } from "util";
import { upload } from "@/utils/uploadfile";
export default {
  data() {
    return {
      input: undefined,
      actualInStockNum: 1,
      readonly: true,
      tableLoading: false,
      currentPage: 1,

      //table页面的条件
      condition: {
        id: undefined,
        checkStatus: undefined,
        onlineStatus: undefined,
        type: 1,
        pageIndex: 1,
        pageSize: 10
      },

      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",
      deleteCondition: {
        id: undefined
      },
      onlineCondition: {
        id: undefined,
        onlineStatus: undefined
      },
      onlineConditionInit: {
        id: undefined,
        onlineStatus: undefined
      },
      rules: {
        content: [
          { required: true, message: "请输入发布内容", trigger: "blur" }
        ]
      },

      formLabelWidth: "120px",
      checkStatueSel: [
        {
          key: "3",
          value: "所有"
        },
        {
          key: "0",
          value: "待审核"
        },
        {
          key: "1",
          value: "审核通过"
        },
        {
          key: "2",
          value: "审核不通过"
        }
      ],
      deletePromoteAttachmentContion: {
        id: undefined,
        promoteContentId: undefined
      },
      onlineStatusSel: [
        {
          key: "2",
          value: "所有"
        },
        {
          key: "0",
          value: "下架"
        },
        {
          key: "1",
          value: "上架"
        }
      ]
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
    //页面初始化函数
    this.fetchData();
  },
  methods: {
    showDetail(row) {
      this.$router.push({
        path: "editarticle",
        query: { id: row.id }
      });
    },
    reviewarticle(row) {
      this.$router.push({
        path: "reviewarticle",
        query: { id: row.id }
      });
    },
    dateFormat() {
      var date = new Date();
      var year = date.getFullYear();
      /* 在日期格式中，月份是从0开始的，因此要加0
       * 使用三元表达式在小于10的前面加0，以达到格式统一  如 09:11:05
       * */
      var month =
        date.getMonth() + 1 < 10
          ? "0" + (date.getMonth() + 1)
          : date.getMonth() + 1;
      var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
      var hours =
        date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
      var minutes =
        date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
      var seconds =
        date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
      // 拼接
      return (
        year +
        "-" +
        month +
        "-" +
        day +
        " " +
        hours +
        ":" +
        minutes +
        ":" +
        seconds
      );
    },
    onlinearticle(row, status) {
      var msg = status == 1 ? "上架" : "下架";

      this.$confirm("确定" + msg + "吗?", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.onlineCondition.id = row.id;
          this.onlineCondition.onlineStatus = status;
          var vm = this;
          appSvc
            .updatePromoteContentOnline(this.onlineCondition)
            .then(
              res => {
                this.onlineCondition = JSON.parse(
                  JSON.stringify(this.onlineConditionInit)
                );
                if (status == 1) {
                  row.onlineStatus = 1;
                  row.publishTime = this.dateFormat();
                } else {
                  row.onlineStatus = 0;
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

    deletearticle(row) {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.deleteCondition.id = row.id;
          var vm = this;
          appSvc
            .deletePromoteContent(this.deleteCondition)
            .then(
              res => {
                this.deleteCondition.id = undefined;
                vm.search();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },

    cellStyle(row, column, rowIndex, columnIndex) {
      //根据报警级别显示颜色
      // console.log(row);
      // console.log(row.column);
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

    fetchData() {
      appSvc
        .getPromoteContents(this.condition)
        .then(
          res => {
            //  this.tableData = res.data;
            var data = res.data;

            this.tableData = data.items;
            this.totalList = data.totalItems;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
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

    showCreate(type, row) {
      this.$router.push({
        path: "createarticle"
      });
    },
    getPaged(flag) {
      this.tableLoading = true;

      if (this.condition.checkStatus == "") {
        this.condition.checkStatus = undefined;
      }

      if (this.condition.onlineStatus == "") {
        this.condition.onlineStatus = undefined;
      }
      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getPromoteContents(this.condition)
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
    height: 123px;
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
