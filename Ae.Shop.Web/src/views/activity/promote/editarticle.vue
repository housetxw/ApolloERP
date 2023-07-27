<template>
  <main class="container">
    <section id="indexPage">
      <main>
        <section>
          <el-form
            :model="articleDetailFormModel"
            ref="articleDetailFormModel"
            :rules="deliveryrules"
          >
            <el-form-item label="标题" prop="title">
              <el-input clearable placeholder="请输入标题" v-model="articleDetailFormModel.title"></el-input>
            </el-form-item>
            <el-form-item label="内容" prop="content">
              <Tinymce ref="content" v-model="articleDetailFormModel.content" :height="350" />
            </el-form-item>
            <el-form-item label="描述" prop="description">
              <el-input
                type="textarea"
                rows="5"
                clearable
                placeholder="请输入描述"
                v-model="articleDetailFormModel.description"
              ></el-input>
            </el-form-item>
            <el-form-item label="缩略图">
              <el-upload
                class="upload-demo"
                action="http://upload.qiniup.com/"
                :on-remove="handleRemove"
                :before-remove="beforeRemove"
                :on-preview="handlePictureCardPreview"
                :limit="1"
                :on-exceed="handleExceed"
                list-type="picture-card"
                :http-request="uploadRequest"
                :file-list="formModel.image ? [{ name: '', url: imagehost+formModel.image }]:[]"
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
            <el-button v-show="editFlag" @click="back()">返回</el-button>
            <el-button v-show="!editFlag" @click="articleDetailCancel()">取消</el-button>
            <el-button type="primary" @click="articleDetailsave('articleDetailFormModel')">保存</el-button>
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
      readonly: true,
      tableLoading: false,
      currentPage: 1,
      editFlag: true,
      formModel: {
        image: undefined
      },

      articleDetailFormModel: {
        title: undefined,
        description: undefined,
        content: undefined,
        id: undefined,
        type: 1,
        thumbnailUrl: undefined
      },
      articleDetailFormModelInit: {
        title: undefined,
        description: undefined,
        content: undefined,
        id: undefined,
        type: 1,
        thumbnailUrl: undefined
      },
      articleDetailCondition: {
        id: undefined
      },
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
      deliveryrules: {
        description: [
          { required: true, message: "请填写描述", trigger: "blur" }
        ]
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
    debugger;
    console.log("param:" + JSON.stringify(this.$route.query));
    var promoteId = this.$route.query.id;

    //新建文章
    if (promoteId != undefined) {
      //页面初始化函数
      this.fetchData(promoteId);
    } else {
      this.editFlag = false;
    }
  },
  methods: {
    handlePictureCardPreview(file) {
      this.dialogImageUrl = file.url;
      this.dialogVisible = true;
    },
    articleDetailsave() {
      this.$confirm("确定保存吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;

          if (
            this.articleDetailFormModel.description == "" ||
            this.articleDetailFormModel.description == undefined
          ) {
            this.$message({
              message: "请填写描述!",
              type: "warning"
            });
            return;
          }

          if (this.articleDetailFormModel.thumbnailUrl == "") {
            if (this.fileList.length <= 0) {
              this.$message({
                message: "请上传缩略图!",
                type: "warning"
              });
              return;
            } else {
              this.articleDetailFormModel.thumbnailUrl = this.fileList[0].url;
            }
          }
          appSvc
            .updatePromoteContent(this.articleDetailFormModel)
            .then(
              res => {
                this.$router.push({
                  path: "article"
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

    articleDetailCancel() {
      this.$refs.content.setContent("");

      this.articleDetailFormModel = JSON.parse(
        JSON.stringify(this.articleDetailFormModelInit)
      );
    },
    back() {
      this.$router.push({
        path: "article"
      });
    },
    fetchData(promoteId) {
      this.articleDetailCondition.id = promoteId;
      appSvc
        .getPromoteContentDetail(this.articleDetailCondition)
        .then(
          res => {
            this.articleDetailFormModel = res.data;
            this.formModel.image = this.articleDetailFormModel.thumbnailUrl;
            this.$refs.content.setContent(this.articleDetailFormModel.content);
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    // 图片上传--start ----
    handleRemove(file, fileList) {
      this.articleDetailFormModel.thumbnailUrl = "";
      //fileList中的数据也需要清除
      var tempFileList = [];
      for (var i = 0; i < this.fileList.length; i++) {
        if (file.name == this.fileList[i].name) {
          this.fileList[i].isDeleted = 1;
        } else {
          tempFileList.push(this.fileList[i]);
        }
      }
      if (tempFileList.length > 0) {
        this.fileList = tempFileList;
      } else {
        this.fileList = [];
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

      var directoryName = "activity/article";
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