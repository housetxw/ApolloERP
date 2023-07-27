<template>
  <main class="container">
    <section id="indexPage">
      <main>
        <section>
          <el-form :model="friendjokesDetailFormModel" ref="friendjokesDetailFormModel" label-width="80px">
            <el-form-item label="内容" prop="content" >
              <el-input
                type="textarea"
                rows="5"
                clearable
                placeholder="请输入内容"
                v-model="friendjokesDetailFormModel.content"
                style="width:60%;"
              ></el-input>
            </el-form-item>
            <!-- <Tinymce ref="editor" v-model="friendjokesDetailFormModel.content" :height="350" /> -->

            <el-form-item label="图片" >
              <el-upload
                class="upload-demo"
                action="http://upload.qiniup.com/"
                :on-remove="handleRemove"
                :before-remove="beforeRemove"
                :limit="9"
                :on-preview="handlePictureCardPreview"
                :on-exceed="handleExceed"
                list-type="picture-card"
                :http-request="uploadRequest"
                :file-list="fileList"
                accept="image/jpeg, image/png, image/jpg"
              >
                <i class="el-icon-plus"></i>
              </el-upload>
              <el-dialog :visible.sync="dialogVisible">
                <img width="100%" :src="dialogImageUrl" alt />
              </el-dialog>
            </el-form-item>
          </el-form>
          <div slot="footer" class="dialog-footer" style="margin-top:20px;margin-left:200px;">
            <el-button @click="createfriendjokesCancel()">取消</el-button>
            <el-button
              type="primary"
              @click="createfriendjokesSave('friendjokesDetailFormModel')"
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

      readonly: true,
      tableLoading: false,

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

  created() {},
  methods: {
    handlePictureCardPreview(file) {
      this.dialogImageUrl = file.url;
      this.dialogVisible = true;
    },

    createfriendjokesCancel() {
      this.friendjokesDetailFormModel = JSON.parse(
        JSON.stringify(this.friendjokesDetailFormModelInit)
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
          var vm = this;

          if (this.fileList.length > 0) {
            //获取isdeleted=0的
            var fileArr = [];
            for (var i = 0; i < this.fileList.length; i++) {
              if (this.fileList[i].isDeleted == 0) {
                fileArr.push(this.fileList[i]);
              }
            }
            if (fileArr.length > 0) {
              this.friendjokesDetailFormModel.attachments = this.fileList;
            }
          }
          appSvc
            .createPromoteContent(this.friendjokesDetailFormModel)
            .then(
              res => {
                vm.createfriendjokesCancel();
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success"
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

    // 图片上传--start ----
    handleRemove(file, fileList) {
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

      //需要从数组中remove
      // console.log(file, fileList);
    },

    handlePreview(file) {
      // console.log(file);
    },
    handleExceed(files, fileList) {},
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
            this.uploadFileModel.url = url;
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

    fetchData() {}
  }
};
</script>
<style lang="scss" scoped>
.container {
  margin-top: 40px;
  margin-bottom: 20px;
  margin-left: 30px;
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