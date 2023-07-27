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

            <el-table-column label="标题" prop="title"></el-table-column>
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

            <el-table-column label="海报" width="300px">
              <template slot-scope="scope">
                <img :src="scope.row.content" style="width:100%; height:100%;padding:5px;" />
              </template>
            </el-table-column>
            <el-table-column label="创建人" prop="createBy"></el-table-column>
            <el-table-column label="创建时间" prop="createTime"></el-table-column>
            <el-table-column label="操作" width="100">
              <template slot-scope="scope">
                <el-button
                  type="text"
                  size="small"
                  v-show="scope.row.onlineStatus == 1?true:false"
                  :disabled="scope.row.onlineStatus == 0?true:false"
                  @click="onlinePoster(scope.row,0)"
                >下架</el-button>
                <el-button
                  type="text"
                  size="small"
                  v-show="scope.row.onlineStatus == 0?true:false"
                  @click="onlinePoster(scope.row,1)"
                >上架</el-button>
                <el-button type="text" size="small" @click="deletePoster(scope.row)">删除</el-button>
                <el-button type="text" size="small" @click="showDetail(scope.row)">详情</el-button>
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

    <!-- 新增海报 -->
    <section id="createposterDialog">
      <el-dialog
        :title="createposterFormTitle"
        :close-on-click-modal="false"
        :visible.sync="createposterFormVisible"
        :before-close="createposterCancel"
      >
        <el-form :model="createposterFormModel" :rules="rules" ref="createposterFormModel">
          <el-form-item label="标题:" prop="title">
            <el-input clearable placeholder="请输入标题" v-model="createposterFormModel.title"></el-input>
          </el-form-item>
          <el-form-item label="描述:" size="50">
            <el-input
              type="textarea"
              :rows="5"
              placeholder="请输入描述"
              v-model="createposterFormModel.description"
            ></el-input>
          </el-form-item>

          <el-form-item label="海报:" size="50">
            <!-- <el-upload
              class="upload-demo"
              action="http://upload.qiniup.com/"
              :on-preview="handlePreview"
              :on-remove="handleRemove"
              :before-remove="beforeRemove"
              :limit="1"
              :on-exceed="handleExceed"
              :file-list="fileList"
              list-type="picture"
              :http-request="uploadRequest"
            >
              <el-button size="small" type="primary">上传图片</el-button>
              <div slot="tip" class="el-upload__tip">大小小于500kb的jpg/png文件</div>
            </el-upload>-->
            <el-upload
              class="upload-demo"
              action="http://upload.qiniup.com/"
              :on-remove="handleRemove"
              :before-remove="beforeRemove"
              :limit="1"
              :on-exceed="handleExceed"
              list-type="picture-card"
              :http-request="uploadRequest"
              :on-preview="handlePictureCardPreview"
              name="image1"
              :file-list="fileList"
              accept="image/jpeg, image/png, image/jpg"
            >
              <i class="el-icon-plus"></i>
            </el-upload>
          </el-form-item>
        </el-form>

        <div slot="footer" class="dialog-footer">
          <el-button @click="createposterCancel()">取消</el-button>
          <el-button type="primary" @click="createposterSave('createposterFormModel')">保存</el-button>
        </div>
      </el-dialog>
      <el-dialog :visible.sync="dialogVisible">
        <img width="100%" :src="dialogImageUrl" alt />
      </el-dialog>
    </section>

    <!-- 查看海报详情 -->
    <section id="posterDetailDialog">
      <el-dialog
        :title="posterDetailFormTitle"
        :close-on-click-modal="false"
        :visible.sync="posterDetailFormVisible"
        :before-close="posterDetailCancel"
      >
        <el-form :model="posterDetailFormModel" ref="posterDetailFormModel">
          <el-form-item label="标题:" prop="title">
            <el-input clearable placeholder="请输入标题" v-model="posterDetailFormModel.title"></el-input>
          </el-form-item>
          <el-form-item label="描述:" size="50">
            <el-input
              type="textarea"
              clearable
              :rows="5"
              placeholder="请输入描述"
              v-model="posterDetailFormModel.description"
            ></el-input>
          </el-form-item>

          <el-form-item label="海报:" size="50">
            <img :src="posterDetailFormModel.content" style="width:100%; height:100%;" />
          </el-form-item>
        </el-form>

        <div slot="footer" class="dialog-footer">
          <el-button type="primary" @click="posterDetailsave()">保存</el-button>
          <el-button @click="posterDetailCancel()">关闭</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 查看海报详情 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/activity/promote";
import { Loading } from "element-ui";
import { isNumber } from "util";
import { upload } from "@/utils/uploadfile";
export default {
  data() {
    return {
      dialogImageUrl: "",
      dialogVisible: false,
      imagehost: "https://m.aerp.com.cn/",
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
        type: 2,
        pageIndex: 1,
        pageSize: 10
      },

      createposterFormTitle: undefined,
      createposterFormVisible: false,
      fileList: [],
      uploadFileModel: {
        name: undefined,
        url: undefined
      },

      createposterFormModel: {
        title: undefined,
        description: undefined,
        content: undefined,
        type: 2
      },
      createposterFormModelInit: {
        title: undefined,
        description: undefined,
        content: undefined,
        type: 2
      },

      uploadFileModelInit: {
        name: undefined,
        url: undefined
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
        title: [{ required: true, message: "请输入标题", trigger: "blur" }]
      },
      posterDetailFormTitle: undefined,
      posterDetailFormVisible: false,

      posterDetailFormModel: {
        title: undefined,
        description: undefined,
        content: undefined,
        id: undefined,
        type: undefined
      },
      posterDetailFormModelInit: {
        title: undefined,
        description: undefined,
        content: undefined,
        id: undefined,
        type: undefined
      },
      posterDetailCondition: {
        id: undefined
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
    handlePictureCardPreview(file) {
      this.dialogImageUrl = file.url;
      this.dialogVisible = true;
    },
    posterDetailsave() {
      this.$confirm("确定保存吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          appSvc
            .updatePromoteContent(this.posterDetailFormModel)
            .then(
              res => {
                vm.posterDetailCancel();

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

    posterDetailCancel() {
      this.posterDetailFormVisible = false;
      this.posterDetailFormModel = JSON.parse(
        JSON.stringify(this.posterDetailFormModelInit)
      );
    },

    showDetail(row) {
      this.posterDetailFormTitle = "海报详情";
      this.posterDetailFormVisible = true;
      this.posterDetailCondition.id = row.id;
      appSvc
        .getPromoteContentDetail(this.posterDetailCondition)
        .then(
          res => {
            this.posterDetailFormModel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    createposterCancel() {
      this.fileList = [];
      this.createposterFormVisible = false;
      this.createposterFormModel = JSON.parse(
        JSON.stringify(this.createposterFormModelInit)
      );
    },
    createposterSave(formName) {
      this.$confirm("确定保存吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.$refs[formName].validate(valid => {
            if (valid) {
              var vm = this;
              //验证是否上传海报
              if (this.fileList.length <= 0) {
                this.$message({
                  message: "请上传营销海报",
                  type: "warning"
                });
                return false;
              } else {
                var fileUrl = this.fileList[0].url;
                this.createposterFormModel.content = fileUrl;
                appSvc
                  .createPromoteContent(this.createposterFormModel)
                  .then(
                    res => {
                      vm.createposterCancel();

                      vm.search();
                    },
                    error => {
                      console.log(error);
                    }
                  )
                  .catch(() => {});
              }
            } else {
              return false;
            }
          });
        })
        .catch(() => {});
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
    onlinePoster(row, status) {
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

    deletePoster(row) {
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
    // 图片上传--start ----
    handleRemove(file, fileList) {
      this.fileList = [];
      // console.log(file, fileList);
    },
    handlePreview(file) {
      // console.log(file);
    },
    handleExceed(files, fileList) {
      //this.$message.warning("限制是1个!");
    },
    beforeRemove(file, fileList) {
      return this.$confirm(`确认删除?`);
    },
    getToken(directoryName, fileName) {
      return appSvc.getQiNiuToken({
        directory: directoryName,
        fileName: fileName
      });
    },
    uploadRequest: function(request) {
      const loading = this.$loading({
        lock: true,
        text: "拼命上传中......",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)"
      });
      debugger;
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        appSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        appSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

      var directoryName = "activity/poster";
      var key = directoryName + "/" + newFileName;
      this.getToken(directoryName, fileName)
        .then(res => {
          var result = res.data;
          const token = result.token;
          const host = result.host;
          upload(
            token,
            key,
            request,
            next => {
              const total = next.total;
            },
            error => {
              this.fileList = [];
            },
            complete => {
              const hash = complete.hash;
              const key = complete.key;
              var url = host + "/" + key;
              //model重新清空

              this.uploadFileModel = JSON.parse(
                JSON.stringify(this.uploadFileModelInit)
              );
              this.uploadFileModel.name = fileName;
              this.uploadFileModel.url = url;

              this.fileList.push(this.uploadFileModel);
            }
          );
        })
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
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
      this.createposterFormVisible = true;
      this.createposterFormTitle = "创建营销海报";
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
