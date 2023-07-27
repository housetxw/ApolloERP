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
            <el-table-column type="expand">
              <template slot-scope="props">
                <el-form label-position="left" class="demo-table-expand">
                  <el-form-item label="内容">
                    <label>{{props.row.content}}</label>
                  </el-form-item>
                  <el-form-item label="图片">
                    <img
                      :src="item.url"
                      v-for="(item, index) in props.row.attachments"
                      style="width: 30%; height: 30%;padding:5px;"
                      v-bind:key="item.url"
                    />
                  </el-form-item>
                </el-form>
              </template>
            </el-table-column>
            <el-table-column label="编号" prop="id" width="80px;"></el-table-column>

            <el-table-column label="审核状态">
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

            <el-table-column label="上下架状态">
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
            <el-table-column label="内容" prop="contentStr"></el-table-column>
            <el-table-column label="创建人" prop="createBy"></el-table-column>
            <el-table-column label="创建时间" prop="createTime"></el-table-column>
            <el-table-column label="操作" width="150px">
              <template slot-scope="scope">
                <el-button
                  type="text"
                  size="small"
                  v-show="scope.row.onlineStatus == 1?true:false"
                  :disabled="scope.row.onlineStatus == 0?true:false"
                  @click="onlinefriendjokes(scope.row,0)"
                >下架</el-button>
                <el-button
                  type="text"
                  size="small"
                  v-show="scope.row.onlineStatus == 0?true:false"
                  @click="onlinefriendjokes(scope.row,1)"
                >上架</el-button>
                <el-button type="text" size="small" @click="deletefriendjokes(scope.row)">删除</el-button>
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

    <!-- 新增朋友圈段子 -->
    <section id="createfriendjokesDialog">
      <el-dialog
        :title="createfriendjokesFormTitle"
        :close-on-click-modal="false"
        :visible.sync="createfriendjokesFormVisible"
        :before-close="createfriendjokesCancel"
      >
        <el-form
          :model="createfriendjokesFormModel"
          :rules="rules"
          ref="createfriendjokesFormModel"
        >
          <el-form-item label="发布内容:" prop="content">
            <el-input
              type="textarea"
              :rows="5"
              clearable
              placeholder="请输入发布内容"
              v-model="createfriendjokesFormModel.content"
            ></el-input>
          </el-form-item>

          <!-- 最多九张 -->
          <el-form-item label="图片:" size="50">
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
              :limit="9"
              :on-exceed="handleExceed"
              list-type="picture-card"
              :http-request="uploadRequest"
              name="image5"
              accept="image/jpeg, image/png, image/jpg"
            >
              <i class="el-icon-plus"></i>
            </el-upload>
          </el-form-item>
        </el-form>

        <div slot="footer" class="dialog-footer">
          <el-button @click="createfriendjokesCancel()">取消</el-button>
          <el-button type="primary" @click="createfriendjokesSave('createfriendjokesFormModel')">保存</el-button>
        </div>
      </el-dialog>
    </section>

    <!-- 查看海报详情 -->
    <section id="friendjokesDetailDialog">
      <el-dialog
        :title="friendjokesDetailFormTitle"
        :close-on-click-modal="false"
        :visible.sync="friendjokesDetailFormVisible"
        :before-close="friendjokesDetailCancel"
      >
        <el-form :model="friendjokesDetailFormModel" ref="friendjokesDetailFormModel">
          <el-form-item label="发布内容:" prop="content">
            <el-input
              type="textarea"
              :rows="5"
              clearable
              v-model="friendjokesDetailFormModel.content"
            ></el-input>
          </el-form-item>

          <el-form-item label="照片:" size="50">
            <el-upload
              class="upload-demo"
              action="http://upload.qiniup.com/"
              :on-remove="handleRemove1"
              :before-remove="beforeRemove"
              :limit="9"
              :file-list="uploadfileList"
              :on-exceed="handleExceed"
              list-type="picture-card"
              :http-request="uploadRequest"
              name="image5"
              accept="image/jpeg, image/png, image/jpg"
            >
              <i class="el-icon-plus"></i>
            </el-upload>
          </el-form-item>
        </el-form>

        <div slot="footer" class="dialog-footer">
          <el-button type="primary" @click="friendjokesDetailsave()">保存</el-button>
          <el-button @click="friendjokesDetailCancel()">关闭</el-button>
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

import { isNumber } from "util";
import { upload } from "@/utils/uploadfile";
export default {
  data() {
    return {
      dialogImageUrl: "",
      dialogVisible: false,
      imagehost: "https://m.aerp.com.cn/",
      uploadfileList: [],
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
        type: 3,
        pageIndex: 1,
        pageSize: 10
      },

      createfriendjokesFormTitle: undefined,
      createfriendjokesFormVisible: false,
      fileList: [],

      createfriendjokesFormModel: {
        title: undefined,
        description: undefined,
        content: undefined,
        type: 3,
        attachments: []
      },
      createfriendjokesFormModelInit: {
        title: undefined,
        description: undefined,
        content: undefined,
        type: 3,
        attachments: []
      },
      uploadFileModel: {
        name: undefined,
        url: undefined,
        isDeleted: undefined
      },
      uploadFileModelInit: {
        filename: undefined,
        fileurl: undefined,
        isDeleted: undefined
      },
      uploadformModel: {
        image1: undefined
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
      friendjokesDetailFormTitle: undefined,
      friendjokesDetailFormVisible: false,

      friendjokesDetailFormModel: {
        title: undefined,
        description: undefined,
        content: undefined,
        id: undefined,
        type: 3,
        attachments: []
      },
      friendjokesDetailFormModelInit: {
        title: undefined,
        description: undefined,
        content: undefined,
        id: undefined,
        type: 3,
        attachments: []
      },
      friendjokesDetailCondition: {
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
    handlePictureCardPreview(file) {
      this.dialogImageUrl = file.url;
      this.dialogVisible = true;
    },
    friendjokesDetailsave() {
      this.$confirm("确定保存吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;

          //将新加的图片传进去
          if (this.fileList.length > 0) {
            var newFileList = [];
            for (var i = 0; i < this.fileList.length; i++) {
              if (this.fileList[i].isDeleted == 0) {
                newFileList.push(this.fileList[i]);
              }
            }
            if (newFileList.length > 0) {
              this.friendjokesDetailFormModel.attachments = newFileList;
            }
          }
          appSvc
            .updatePromoteContent(this.friendjokesDetailFormModel)
            .then(
              res => {
                vm.friendjokesDetailCancel();

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

    friendjokesDetailCancel() {
      this.friendjokesDetailFormVisible = false;
      this.friendjokesDetailFormModel = JSON.parse(
        JSON.stringify(this.friendjokesDetailFormModelInit)
      );
      this.uploadfileList = [];
      this.fileList = [];
    },

    showDetail(row) {
      this.$router.push({
        path: "editfriendjokes",
        query: { id: row.id }
      });
    },
    createfriendjokesCancel() {
      this.createfriendjokesFormVisible = false;
      this.createfriendjokesFormModel = JSON.parse(
        JSON.stringify(this.createfriendjokesFormModelInit)
      );
      this.fileList = [];
    },
    createfriendjokesSave(formName) {
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

              if (this.fileList.length > 0) {
                this.createfriendjokesFormModel.attachments = this.fileList;
              }
              appSvc
                .createPromoteContent(this.createfriendjokesFormModel)
                .then(
                  res => {
                    vm.createfriendjokesCancel();

                    vm.search();
                  },
                  error => {
                    console.log(error);
                  }
                )
                .catch(() => {});
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
    onlinefriendjokes(row, status) {
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

    deletefriendjokes(row) {
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
      // console.log(file, fileList);
    },
    handleRemove1(file, fileList) {
      //如果该图片是新追加的 但是还没有保存到数据  移除了也要清掉
      if (file.promoteContentId == undefined) {
        if (this.fileList.length > 0) {
          for (var i = 0; i < this.fileList.length; i++) {
            if (this.fileList[i].name == file.name) {
              this.fileList[i].isDeleted = 1;
            }
          }
        }
      } else {
        //原来已经存在的

        this.deletePromoteAttachmentContion.id = file.id;
        this.deletePromoteAttachmentContion.promoteContentId =
          file.promoteContentId;
        appSvc
          .deletePromoteAttachment(this.deletePromoteAttachmentContion)
          .then(
            res => {
              //从数据库中删除,并且从Array中remove
              for (var i = 0; i < this.uploadfileList.length; i++) {
                if (
                  this.uploadfileList[i].id ==
                  this.deletePromoteAttachmentContion.id
                ) {
                  this.uploadfileList[i].isDeleted = 1;
                }
              }
              this.deletePromoteAttachmentContion.id = undefined;
              this.deletePromoteAttachmentContion.promoteContentId = undefined;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
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
      debugger;
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        appSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        appSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

      var directoryName = "activity/friendjokes";
      var key = directoryName + "/" + newFileName;
      this.getToken(directoryName, fileName).then(res => {
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
            this.uploadFileModel.url = key;
            this.uploadFileModel.isDeleted = 0;

            this.fileList.push(this.uploadFileModel);
          }
        );
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
      this.$router.push({
        path: "createfriendjokes"
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
