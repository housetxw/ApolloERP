<template>
  <div class="upload-container">
    <el-button
      :style="{background:color,borderColor:color}"
      icon="el-icon-upload"
      size="mini"
      type="primary"
      @click="dialogVisible=true"
    >上传图片</el-button>
    <el-dialog :visible.sync="dialogVisible">
      <el-upload
        class="editor-slide-upload"
        action="http://upload.qiniup.com/"
        :before-remove="beforeRemove"
        :on-remove="handleRemove"
        multiple
        :file-list="fileList"
        list-type="picture-card"
        :http-request="uploadRequest"
        :before-upload="beforeAvatarUpload"
      >
      <el-button size="small" type="primary">上传图片</el-button>
      </el-upload>
      <el-button @click="handleCancel">取消</el-button>
      <el-button type="primary" @click="handleSubmit">确定</el-button>
    </el-dialog>
  </div>
</template>

<script>
import { upload } from "@/utils/uploadfile";
import { appSvc } from "@/api/qiniu/qiniuservice";
import { Loading } from "element-ui";
export default {
  name: "EditorSlideUpload",
  props: {
    color: {
      type: String,
      default: "#1890ff"
    }
  },
  data() {
    return {
      dialogVisible: false,
      fileList: []
    };
  },
  methods: {
    handleCancel() {
      this.fileList = [];
      this.dialogVisible = false;
    },
    handleSubmit() {
      this.$emit("successCBK", this.fileList);
      this.fileList = [];
      this.dialogVisible = false;
    },
    // 图片上传--start ----
    beforeRemove(file, fileList) {
      return this.$confirm(`确认删除?`);
    },
    handleRemove(file, fileList) {
      this.fileList=fileList
    },
    beforeAvatarUpload(file) {
      const isLt2M = file.size / 1024 / 1024 < 2;
      if (!isLt2M) {
        this.$message.error("上传头像图片大小不能超过 2MB!");
      }
      return isLt2M;
    },
    //获取token
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

      var fileName = request.file.name;
      var uid = request.file.uid
      var directoryName = "productImage";
      var fileExtension = fileName.split('.').pop();
      const newFileName = appSvc.formatDate(new Date(),"yyyyMMddhhmmss") + appSvc.getRandomInt(1000, 9999)+"."+fileExtension;
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
            //this.fileList = [];
          },
          complete => {
            const hash = complete.hash;
            const key = complete.key;
            var url = host + "/" + key;
            var img = { name: fileName, url: url,uid:uid };
            this.fileList.push(img);
          }
        );
      }) .catch(() => {})
        .finally(() => {
          loading.close();
        });
    }
    // 图片上传--end ----
  }
};
</script>

<style lang="scss" scoped>
.editor-slide-upload {
  margin-bottom: 20px;
 
}
</style>
