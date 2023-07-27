<template>
  <main class="container">
    <section id="indexPage">
      <main>
        <section>
          <el-form :model="friendjokesDetailFormModel" ref="friendjokesDetailFormModel">
            <!-- <el-form-item label="内容" prop="venderShortName">
              <Tinymce ref="editor" v-model="friendjokesDetailFormModel.content" :height="350" />
            </el-form-item> -->
               <el-form-item label="内容" prop="content">
              <el-input
                type="textarea"
                rows="5"
                clearable
                placeholder="请输入内容"
                v-model="friendjokesDetailFormModel.content"
              ></el-input>
            </el-form-item>
            <el-form-item label="图片">
              <el-upload
                class="upload-demo"
                action="http://upload.qiniup.com/"
                :on-remove="handleRemove1"
                :before-remove="beforeRemove"
                :on-preview="handlePictureCardPreview"
                :limit="9"
                :on-exceed="handleExceed"
                list-type="picture-card"
                :http-request="uploadRequest"
                :file-list="uploadfileList"
                accept="image/jpeg, image/png, image/jpg"
              >
                <i class="el-icon-plus"></i>
              </el-upload>
              <el-dialog :visible.sync="dialogVisible">
                <img width="100%" :src="dialogImageUrl" alt />
              </el-dialog>
            </el-form-item>
          </el-form>
          <div slot="footer" class="dialog-footer" style="margin-top:10px;">
            <el-button @click="back()">返回</el-button>
            <el-button
              type="primary"
              @click="friendjokesDetailsave('friendjokesDetailFormModel')"
            >保存</el-button>
          </div>
        </section>
      </main>
    </section>
  </main>
</template>

<script>
import { Loading } from "element-ui";
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/activity/promote";
import Tinymce from "@/components/Tinymce";
import { isNumber } from "util";
import { upload } from "@/utils/uploadfile";
export default {
  components: { Tinymce },
  data() {
    return {
      dialogImageUrl: "",
      dialogVisible: false,
      imagehost: "https://m.aerp.com.cn/",
      uploadfileList: [],

      readonly: true,
      tableLoading: false,
      currentPage: 1,

      fileList: [],

      uploadFileModel: {
        name: undefined,
        url: undefined,
        isDeleted: undefined
      },
      uploadFileModelInit: {
        name: undefined,
        url: undefined,
        isDeleted: undefined
      },

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
      deletePromoteAttachmentContion: {
        id: undefined,
        promoteContentId: undefined
      },
      deletePromoteAttachmentContionInit: {
        id: undefined,
        promoteContentId: undefined
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
    console.log("param:" + JSON.stringify(this.$route.query));
    var promoteId = this.$route.query.id;
    //页面初始化函数
    this.fetchData(promoteId);
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
          } else {
            //如果未编辑图片 将数据清空
            this.friendjokesDetailFormModel.attachments = [];
          }
          appSvc
            .updatePromoteContent(this.friendjokesDetailFormModel)
            .then(
              res => {
                this.$router.push({
                  path: "friendjokes"
                });
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },
    back() {
      this.$router.push({
        path: "friendjokes"
      });
    },
    friendjokesDetailCancel() {
      this.friendjokesDetailFormModel = JSON.parse(
        JSON.stringify(this.friendjokesDetailFormModelInit)
      );
      this.uploadfileList = [];
      this.fileList = [];
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
        debugger;
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
        const loading = this.$loading({
          lock: true,
          text: '拼命上传中......',
          spinner: 'el-icon-loading',
          background: 'rgba(0, 0, 0, 0.7)'
        });
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
      }).catch(() => {
            })
            .finally(() => {
              loading.close();
            })
    },

    fetchData(promoteId) {
      this.friendjokesDetailCondition.id = promoteId;
      appSvc
        .getPromoteContentDetail(this.friendjokesDetailCondition)
        .then(
          res => {
            this.friendjokesDetailFormModel = res.data;
            //  this.friendjokesDetailFormModel.attachments =[];  //清除原来已经存在的
            this.uploadfileList = res.data.attachments;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    }
  }
};
</script>
<style lang="scss" scoped>
.container {
  margin-top: 40px;
  margin-left: 30px;
  margin-bottom: 20px;
  .indexPage {
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