<template>
  <main class="container">
    <section id="indexPage">
      <main>
        <section>
          <el-form :model="installguideModel" ref="installguideModel" :rules="guiderules"  label-width="80px"  >
            <el-form-item label="标题" prop="title" style="width:400px;">
              <el-input clearable placeholder="请输入标题" v-model="installguideModel.title"></el-input>
            </el-form-item>
            <el-form-item label="分类" prop="categoryId">
              <el-select
                v-model="installguideModel.categoryId"
                size="small"
                clearable
                filterable
                placeholder="请选择分类"
              >
                <el-option
                  v-for="item in categorySel"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item label="车型:">
              <el-select
                style="padding-top:5px;"
                v-model="installguideModel.brand"
                size="small"
                clearable
                filterable
                @change="changeBrand(1)"
                placeholder="请选择品牌"
              >
                <el-option
                  v-for="item in brandSel"
                  :key="item.brand"
                  :label="item.brand"
                  :value="item.brand"
                ></el-option>
              </el-select>&nbsp;&nbsp;&nbsp;
              <el-select
                v-model="installguideModel.vehicleSeries"
                size="small"
                clearable
                filterable
                @change="changevehicleSeries(1)"
                placeholder="请选择车系"
              >
                <el-option
                  v-for="item in vehicleSeriesSel"
                  :key="item.vehicleId"
                  :label="item.vehicle"
                  :value="item.vehicleId"
                ></el-option>
              </el-select>&nbsp;&nbsp;&nbsp;
              <el-select
                v-model="installguideModel.paiLiang"
                size="small"
                clearable
                filterable
                @change="changepaiLiang(1)"
                placeholder="请选择发动机排量"
              >
                <el-option
                  v-for="item in paiLiangSel"
                  :key="item.key"
                  :label="item.value"
                  :value="item.key"
                ></el-option>
              </el-select>&nbsp;&nbsp;&nbsp;
              <el-select
                v-model="installguideModel.nian"
                size="small"
                clearable
                filterable
                @change="changenian(1)"
                placeholder="请选择生产年份"
              >
                <el-option
                  v-for="item in nianSel"
                  :key="item.key"
                  :label="item.value"
                  :value="item.key"
                ></el-option>
              </el-select>&nbsp;&nbsp;&nbsp;
              <el-select
                style="width:350px;"
                v-model="installguideModel.salesName"
                size="small"
                clearable
                filterable
                placeholder="请选择车型"
              >
                <el-option
                  v-for="item in salesNameSel"
                  :key="item.salesName"
                  :label="item.salesName"
                  :value="item.salesName"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item label="描述" style="width:600px;" prop="content" >
              <el-input
                type="textarea"
                rows="7"
                clearable
                placeholder="请输入描述"
                v-model="installguideModel.content"
              ></el-input>
            </el-form-item>
            <el-form-item label="附件" style="width:600px;">
              <el-upload
                class="upload-demo"
                action="http://upload.qiniup.com/"
                :on-remove="handleRemove"
                :before-remove="beforeRemove"
                :before-upload="beforeAvatarUpload"
                :limit="5"
                :on-preview="handlePreview"
                :on-exceed="handleExceed"
                :file-list="tempfileList"
                :http-request="uploadRequest"
              >
                <i class="el-icon-plus"></i>
              </el-upload>
            </el-form-item>
          </el-form>
          <div slot="footer" class="dialog-footer" style="margin-top:10px;margin-left:200px;">
            <el-button @click="guidecancle()">返回</el-button>
            <el-button @click="guidesave()" type="primary">保存</el-button>
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
import { appSvc } from "@/api/activity/installguide";
import { upload } from "@/utils/uploadfilecommon";

import { isNumber } from "util";
export default {
  data() {
    return {
      host: "https://m.aerp.com.cn/",
      installguideModel: {
        title: undefined,
        categoryId: undefined,
        content: undefined,
        brand: undefined,
        vehicleSeries: undefined,
        paiLiang: undefined,
        nian: undefined,
        salesName: undefined,
        fileList: [],
        vehicle: undefined
      },
      installguideModelInit: {
        title: undefined,
        categoryId: undefined,
        content: undefined,
        brand: undefined,
        vehicleSeries: undefined,
        paiLiang: undefined,
        nian: undefined,
        salesName: undefined,
        fileList: [],
        vehicle: undefined
      },

      condition: { id: undefined },
      fileList: [],
      tempfileList: [],
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
      categorySel: [],
      brandSel: [],
      brandCondition: {
        brand: undefined
      },
      vehicleSeriesSel: [],
      vehicleSeriesCondition: {
        vehicleId: undefined
      },
      paiLiangSel: [],
      paiLiangCondition: {
        vehicleId: undefined,
        paiLiang: undefined
      },
      nianSel: [],
      nianCondition: {
        vehicleId: undefined,
        paiLiang: undefined,
        nian: undefined
      },

      salesNameSel: [],
      deleteCondition: {
        id: undefined
      },
      guiderules: {
        title: [{ required: true, message: "请填写标题", trigger: "blur" }],
        content: [{ required: true, message: "请填写内容", trigger: "blur" }],
        categoryId: [{ required: true, message: "请选择分类", trigger: "blur" }]
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
    var id = this.$route.query.id;
    this.fetchData(id);
  },
  methods: {
    handlePreview(file) {
      window.open(file.url);
    },
    handleChange(file, fileList) {
      this.tempfileList = fileList.slice(-3);
    },

    guidecancle() {
      // this.installguideModel = JSON.parse(
      //   JSON.stringify(this.installguideModelInit)
      // );
      this.$router.push({
        path: "installguideindex"
      });
    },
    guidesave() {
      this.$confirm("确定保存吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          if (
            this.installguideModel.title == "" ||
            this.installguideModel.title == undefined
          ) {
            this.$message({
              message: "标题不能为空!",
              type: "warning"
            });
            return;
          }

          if (
            this.installguideModel.categoryId == "" ||
            this.installguideModel.categoryId == undefined
          ) {
            this.$message({
              message: "请选择分类!",
              type: "warning"
            });
            return;
          }

          if (
            this.installguideModel.content == "" ||
            this.installguideModel.content == undefined
          ) {
            this.$message({
              message: "内容不能为空!",
              type: "warning"
            });
            return;
          }

          //组装车型的数据
          var vehicleName = "";

          if (
            this.installguideModel.brand != "" &&
            this.installguideModel.brand != undefined
          ) {
            vehicleName = this.installguideModel.brand;
          }

          if (
            this.installguideModel.vehicleSeries != "" &&
            this.installguideModel.vehicleSeries != undefined
          ) {
            var vehicleSeries = "";
            for (var i = 0; i < this.vehicleSeriesSel.length; i++) {
              if (
                this.vehicleSeriesSel[i].vehicleId ==
                this.installguideModel.vehicleSeries
              ) {
                vehicleSeries = this.vehicleSeriesSel[i].vehicle;
              }
            }
            if (vehicleSeries != "") {
              vehicleName = vehicleName + "|" + vehicleSeries;
            }
          }

          if (
            this.installguideModel.paiLiang != "" &&
            this.installguideModel.paiLiang != undefined
          ) {
            vehicleName = vehicleName + "|" + this.installguideModel.paiLiang;
          }

          if (
            this.installguideModel.nian != "" &&
            this.installguideModel.nian != undefined
          ) {
            vehicleName = vehicleName + "|" + this.installguideModel.nian;
          }

          if (
            this.installguideModel.salesName != "" &&
            this.installguideModel.salesName != undefined
          ) {
            vehicleName = vehicleName + "|" + this.installguideModel.salesName;
          }

          if (vehicleName != "") {
            this.installguideModel.vehicle = vehicleName;
          } else {
            this.installguideModel.vehicle = "";
          }

          //将新加的图片传进去
          if (this.fileList.length > 0) {
            var newFileList = [];
            for (var i = 0; i < this.fileList.length; i++) {
              if (this.fileList[i].isDeleted == 0) {
                newFileList.push(this.fileList[i]);
              }
            }
            if (newFileList.length > 0) {
              this.installguideModel.fileList = newFileList;
            } else {
              this.installguideModel.fileList = [];
            }
          } else {
            //如果未编辑图片 将数据清空
            this.installguideModel.fileList = [];
          }

          appSvc
            .updateInstallGuideInfo(this.installguideModel)
            .then(
              res => {
                this.$router.push({
                  path: "installguideindex"
                });
                // this.installguideModel = JSON.parse(
                //   JSON.stringify(this.installguideModelInit)
                // );
                // if (res.message != "") {
                //   this.$message({
                //     message: res.message,
                //     type: "success"
                //   });
                // }
                // this.fileList = [];
                // this.tempfileList =
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },

    //获取品牌
    getvehiclebrand() {
      appSvc
        .getVehicleBrandList()
        .then(
          res => {
            this.brandSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    //获取分类
    getInstallGuideCategory() {
      appSvc
        .getInstallGuideCategory()
        .then(
          res => {
            this.categorySel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    //获取车系
    changeBrand(clearFlag) {
      if (clearFlag == 1) {
        //全部清空
        this.vehicleSeriesSel = [];
        this.paiLiangSel = [];
        this.nianSel = [];
        this.salesNameSel = [];
        this.installguideModel.vehicleSeries = "";
        this.installguideModel.paiLiang = "";
        this.installguideModel.nian = "";
        this.installguideModel.salesName = "";
      }
      if (this.installguideModel.brand != "") {
        this.brandCondition.brand = this.installguideModel.brand;
        appSvc
          .getVehicleListByBrand(this.brandCondition)
          .then(
            res => {
              this.vehicleSeriesSel = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },

    //获取排量
    changevehicleSeries(clearFlag) {
      if (clearFlag == 1) {
        this.paiLiangSel = [];
        this.nianSel = [];
        this.salesNameSel = [];
        this.installguideModel.paiLiang = "";
        this.installguideModel.nian = "";
        this.installguideModel.salesName = "";
      }
      if (this.installguideModel.vehicleSeries != "") {
        this.vehicleSeriesCondition.vehicleId = this.installguideModel.vehicleSeries;
        appSvc
          .getPaiLiangByVehicleId(this.vehicleSeriesCondition)
          .then(
            res => {
              this.paiLiangSel = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },

    //获取生产年份
    changepaiLiang(clearFlag) {
      if (clearFlag == 1) {
        this.nianSel = [];
        this.salesNameSel = [];

        this.installguideModel.nian = "";
        this.installguideModel.salesName = "";
      }
      if (
        this.installguideModel.vehicleSeries != "" &&
        this.installguideModel.paiLiang != ""
      ) {
        this.paiLiangCondition.vehicleId = this.installguideModel.vehicleSeries;
        this.paiLiangCondition.paiLiang = this.installguideModel.paiLiang;
        appSvc
          .getVehicleNianByPaiLiang(this.paiLiangCondition)
          .then(
            res => {
              this.nianSel = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },
    //获取车型
    changenian(clearFlag) {
      if (clearFlag == 1) {
        this.salesNameSel = [];
        this.installguideModel.salesName = "";
      }
      if (
        this.installguideModel.vehicleSeries != "" &&
        this.installguideModel.paiLiang != "" &&
        this.installguideModel.nian != ""
      ) {
        this.nianCondition.vehicleId = this.installguideModel.vehicleSeries;
        this.nianCondition.paiLiang = this.installguideModel.paiLiang;
        this.nianCondition.nian = this.installguideModel.nian;
        appSvc
          .getVehicleSalesNameByNian(this.nianCondition)
          .then(
            res => {
              this.salesNameSel = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },
    fetchData(id) {
      debugger;
      this.getvehiclebrand();
      this.getInstallGuideCategory();
      this.condition.id = id;
      appSvc
        .getInstallGuideDetail(this.condition)
        .then(
          res => {
            this.installguideModel = res.data;
            //加载车型
            this.changeBrand(0);
              //加载车系
            this.changevehicleSeries(0);
            //加载排量
            this.changepaiLiang(0);
            //加载年份
            this.changenian(0);
          
            this.tempfileList = this.installguideModel.fileList;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    beforeAvatarUpload(file) {
      const isMP4 = file.type === "video/mp4";
      const isPDF = file.type === "application/pdf";
      const isLt50 = file.size / 1024 / 1024 < 50;
      const isLt100 = file.size / 1024 / 1024 < 100;

      if (isMP4) {
        if (!isLt100) {
          this.$message.error("mp4不能大于100M!");
          return false;
        }
      } else {
        if (isPDF) {
          if (!isLt50) {
            this.$message.error("pdf不能大于50M!");
            return false;
          }
        } else {
          this.$message.error("附件只能是pdf或者mp4格式!");
          return false;
        }
      }
    },
    // 图片上传--start ----
    handleRemove(file, fileList) {
      //如果该图片是新追加的 但是还没有保存到数据  移除了也要清掉
      if (file.id == undefined) {
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
        this.deleteCondition.id = file.id;

        appSvc
          .deleteInstallGuideFile(this.deleteCondition)
          .then(
            res => {
              //从数据库中删除,并且从Array中remove
              for (var i = 0; i < this.tempfileList.length; i++) {
                if (this.tempfileList[i].id == this.deleteCondition.id) {
                  this.tempfileList[i].isDeleted = 1;
                }
              }
              this.deleteCondition.id = undefined;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },

    handleExceed(files, fileList) {
      //this.$message.warning("限制是1个!");
    },
    beforeRemove(file, fileList) {
      //return this.$confirm(`确认删除?`);
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

      var directoryName = "activity/installguide/installfile";
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
              this.uploadFileModel.isDeleted = 0;

              this.fileList.push(this.uploadFileModel);
            }
          );
        })
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    }
  }
};
</script>

<style lang="scss" scoped>
.container {
  margin-top: 40px;
  margin-left: 30px;
  margin-bottom: 20px;
  .bodyTop {
    height: 88px;
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
