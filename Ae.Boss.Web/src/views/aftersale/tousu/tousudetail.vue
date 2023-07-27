<template>
  <main class="container">
    <el-card class="rg-card">
      <el-row>
        <el-col :span="12">
          <el-form label-width="120px" size="mini">
            <el-row style="margin-bottom:10px">
              <el-button-group>
                <el-button
                  type="warning"
                  size="mini"
                  :disabled="this.tousuModel.status == '新建'"
                  @click="updatetoususstatus('驳回')"
                >驳回</el-button>
                <el-button
                  type="primary"
                  size="mini"
                  :disabled="this.tousuModel.status == '新建' "
                  @click="updatetoususstatus('通过')"
                >通过</el-button>
                <el-button
                  type="primary"
                  size="mini"
                  :disabled="this.tousuModel.status == '已审核'                       "
                  @click="updatetoususstatus('处理中')"
                >开始处理</el-button>
                <el-button
                  type="primary"
                  size="mini"
                  :disabled="this.tousuModel.status == '处理中'"
                  @click="updatetoususstatus('关闭')"
                >关闭</el-button>
                <el-button
                  type="warning"
                  size="mini"
                  :disabled="this.tousuModel.status == '关闭'"
                  @click="updatetoususstatus('恢复')"
                >恢复</el-button>
                <el-button
                  type="warning"
                  size="mini"
                  :disabled="this.tousuModel.status == '处理中'"
                  @click="tousuDuty()"
                >投诉定责</el-button>
              </el-button-group>
            </el-row>
            <el-form-item label="订单号">{{ tousuModel.orderNo}}</el-form-item>
            <el-form-item label="用户名">{{ tousuModel.userName}}</el-form-item>
            <el-form-item label="联系方式">{{ tousuModel.userTel}}</el-form-item>
            <el-form-item label="门店">{{ tousuModel.shopName}}</el-form-item>
            <el-form-item label="状态">{{ tousuModel.status}}</el-form-item>
            <el-form-item label="优先级">{{ tousuModel.priority}}</el-form-item>
            <el-form-item label="投诉主题">{{ tousuModel.subject}}</el-form-item>
            <el-form-item label="投诉描述">{{ tousuModel.description}}</el-form-item>
            <el-form-item label="一级投诉类型">{{ tousuModel.tousuType}}</el-form-item>
            <el-form-item label="二级投诉类型">{{ tousuModel.tousuSubType1}}</el-form-item>
            <el-form-item
              label="三级投诉类型"
              v-show="tousuModel.tousuSubType2"
            >{{ tousuModel.tousuSubType2}}</el-form-item>

            <el-form-item label="开始处理时间">{{ (tousuModel.dealTime||"").toYMDHm()}}</el-form-item>
            <el-form-item
              label="创建人"
            >{{ tousuModel.createBy }} &nbsp; &nbsp;{{ (tousuModel.createTime||"").toYMDHm()}}</el-form-item>

            <el-form-item label="责任部门" v-if="tousuModel.dutyDepart">{{ tousuModel.dutyDepart }}</el-form-item>
            <el-form-item label="责任人" v-if="tousuModel.dutyMan">{{ tousuModel.dutyMan }}</el-form-item>
            <el-form-item label="处理结果" v-if="tousuModel.result">{{ tousuModel.result }}</el-form-item>
            <el-form-item label="附件">
              <el-row v-for="item in tousuFileData" v-bind:key="item.fileName">
                <img :src="item.fileUrl" alt style="width: 50%;height: 50%;" />
              </el-row>
            </el-form-item>
            <el-form-item label="投诉图片">
              <el-form>
                <el-form-item label="" size="50">
                  <el-upload
                    class="upload-demo"
                    action="http://upload.qiniup.com/"
                    :on-preview="handlePreview"
                    :on-remove="handleRemove"
                    :before-remove="beforeRemove"
                    multiple
                    :limit="5"
                    :on-exceed="handleExceed"
                    :file-list="fileList"
                    list-type="picture"
                    :http-request="uploadRequest"
                  >
                    <el-button size="mini" type="primary">上传图片</el-button>
                    <div slot="tip" class="el-upload__tip">大小小于500kb的jpg/png文件</div>
                  </el-upload>
                </el-form-item>
              </el-form>
            </el-form-item>
          </el-form>
        </el-col>
        <el-col :span="12">
          <el-divider content-position="left">投诉单号:【{{ tousuModel.id }}】的操作历史</el-divider>
          <el-table border :data="tousulogData" size="mini">
            <rg-table-column label="编号" prop="id" fix-min />
            <rg-table-column label="操作记录" prop="remark" />
            <rg-table-column label="操作人" prop="createBy" fix-min />
            <rg-table-column label="操作时间" prop="createTime" is-datetime fix-min />
          </el-table>
        </el-col>
      </el-row>
    </el-card>

    <!-- 驳回/关闭填写处理结果 -->
    <section id="rejectTousu">
      <el-dialog
        width="500px"
        :title="rejectFormTitle"
        :close-on-click-modal="false"
        :visible.sync="rejectFormVisible"
        :before-close="rejectCancel"
      >
        <el-form :model="rejectFormModel" ref="rejectFormModel">
          <el-form-item label="处理结果:">
            <el-select
              v-model="rejectFormModel.result"
              size="small"
              clearable
              filterable
              placeholder="请选择处理结果"
              class="margin-right-10"
            >
              <el-option
                v-for="item in dealResultSel"
                :key="item.dicKey"
                :label="item.dicValue"
                :value="item.dicKey"
              ></el-option>
            </el-select>
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="rejectCancel('rejectFormModel')">取消</el-button>
          <el-button type="primary" @click="rejectSave('rejectFormModel')">确定</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 驳回/关闭填写处理结果 -->

    <!-- 定责处理 -->
    <section id="dutyHandelTousu">
      <el-dialog
        width="500px"
        :title="dutyFormTitle"
        :close-on-click-modal="false"
        :visible.sync="dutyFormVisible"
        :before-close="dutyCancel"
      >
        <el-form :model="dutyFormModel" ref="dutyFormModel">
          <el-form-item label="责任部门:">
            <el-select
              v-model="dutyFormModel.dutyDepart"
              size="small"
              clearable
              filterable
              placeholder="请选择责任部门"
              class="margin-right-10"
            >
              <el-option
                v-for="item in dutyDepSel"
                :key="item.dicKey"
                :label="item.dicValue"
                :value="item.dicKey"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="责任人（多人用逗号隔开）:">
            <el-input v-model="dutyFormModel.dutyMan" />
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="dutyCancel('dutyFormModel')">取消</el-button>
          <el-button type="primary" @click="dutySave('dutyFormModel')">确定</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 定责处理 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/aftersale/tousu";
import { appTypeSvc } from "@/api/aftersale/tousutype";

import { isNumber } from "util";
import { upload } from "@/utils/uploadfile";
export default {
  data() {
    return {
      tousuDetailCondition: {
        id: undefined
      },
      tousulogData: [],
      tousuModel: {
        id: undefined,
        shopId: undefined,
        shopName: undefined,
        status: undefined,
        userId: undefined,
        userTel: undefined,
        userName: undefined,
        caseId: undefined,
        orderNo: undefined,
        refNo: undefined,
        subject: undefined,
        description: undefined,
        owner: undefined,
        result: undefined,
        priority: undefined,
        allocateType: undefined,
        allocateTime: undefined,
        dealTime: undefined,
        dealNum: undefined,
        dutyDepart: undefined,
        dutyMan: undefined,
        isRepeat: undefined,
        tousuGroup: undefined,
        tousuType: undefined,
        tousuSubType1: undefined,
        tousuSubType2: undefined,
        tousuSubType3: undefined,
        createBy: undefined,
        createTime: undefined
      },
      updatetousustatusmodel: {
        id: undefined,
        status: undefined
      },
      dealResultSel: [],
      dealResultSelCondition: {
        type: undefined
      },
      rejectFormTitle: "",
      rejectFormVisible: false,
      rejectFormModel: {
        id: undefined,
        result: undefined,
        status: undefined
      },

      rejectFormModelInit: {
        id: undefined,
        result: undefined,
        status: undefined
      },
      fileList: [],
      uploadFileModel: {
        relationId: undefined,
        filename: undefined,
        fileurl: undefined
      },
      uploadFileModelInit: {
        relationId: undefined,
        filename: undefined,
        fileurl: undefined
      },
      dutyDepSel: [],
      dutySelCondition: {
        type: undefined
      },
      dutyFormTitle: "",
      dutyFormVisible: false,

      dutyFormModel: {
        id: undefined,
        dutyDepart: undefined,
        dutyMan: undefined
      },
      dutyFormModelInit: {
        id: undefined,
        dutyDepart: undefined,
        dutyMan: undefined
      },
      tousuFileData: [],
      getfileCondition: {
        relationId: undefined,
        relationType: undefined
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
    var tousuId = this.$route.query.id;
    this.reloadPage(tousuId);
  },
  methods: {
    // 图片上传--start ----
    handleRemove(file, fileList) {
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
      var date = new Date();
      var fullDate =
        date.getFullYear().toString() +
        (date.getMonth() + 1).toString() +
        date.getDate().toString();
      var fileName = request.file.name;
      var directoryName = "aftersaleImage";
      var key = directoryName + "/" + fullDate + "/" + fileName;
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
            //上传一次 就清一次model
            this.uploadFileModel = JSON.parse(
              JSON.stringify(this.uploadFileModelInit)
            );
            this.uploadFileModel.filename = fileName;
            this.uploadFileModel.fileurl = key;
            this.uploadFileModel.relationId = this.tousuModel.id;

            //调用后台服务
            appSvc
              .createTousuFile(this.uploadFileModel)
              .then(
                res => {
                  if (res.code == 10000) {
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
          }
        );
      });
    },

    tousuDuty() {
      this.dutySelCondition.type = "DutyDepartment";

      this.dutyFormTitle = "投诉定责";
      this.dutyFormVisible = true;

      appSvc
        .getDictionaryDOs(this.dutySelCondition)
        .then(
          res => {
            this.dutyDepSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    dutyCancel(formName) {
      this.dutyFormVisible = false;
      // this.resetSendForm();
      this.dutyFormModel = JSON.parse(JSON.stringify(this.dutyFormModelInit));
      var frmName =
        typeof formName === "string" && formName ? formName : "dutyFormModel";
      this.$refs[frmName].resetFields();
    },
    dutySave() {
      this.dutyFormModel.id = this.tousuModel.id;
      appSvc
        .updateTousuDutyInfo(this.dutyFormModel)
        .then(res => {
          if (res.code == 10000) {
            this.$message({
              message: res.message,
              type: "success"
            });
            this.dutyFormVisible = false;
            // this.resetSendForm();
            var frmName =
              typeof formName === "string" && formName
                ? formName
                : "dutyFormModel";

            this.reloadPage(this.tousuModel.id);
          }
        })
        .catch(error => {
          console.log(error);
        });
    },

    reloadPage(tousuId) {
      this.tousuDetailCondition.id = tousuId;

      appSvc
        .getTousuDetail(this.tousuDetailCondition)
        .then(
          res => {
            this.tousuModel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});

      appSvc
        .getTousuLogs(this.tousuDetailCondition)
        .then(
          res => {
            this.tousulogData = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});

      this.gettousuFile(tousuId);
    },

    gettousuFile(tousuId) {
      this.getfileCondition.relationId = tousuId;
      this.getfileCondition.relationType = "Tousu";
      appSvc
        .getTousuFiles(this.getfileCondition)
        .then(
          res => {
            this.tousuFileData = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    rejectCancel(formName) {
      this.rejectFormVisible = false;
      // this.resetSendForm();
      this.rejectFormModel = JSON.parse(
        JSON.stringify(this.rejectFormModelInit)
      );
      var frmName =
        typeof formName === "string" && formName ? formName : "rejectFormModel";
      this.$refs[frmName].resetFields();
    },
    rejectSave(formName) {
      //驳回和关闭的保存  都需要填写结果

      if (
        this.rejectFormModel.result != "" ||
        this.rejectFormModel.result != undefined
      ) {
        this.rejectFormModel.id = this.tousuModel.id;

        appSvc
          .updateTousuDealResult(this.rejectFormModel)
          .then(res => {
            if (res.code == 10000) {
              this.$message({
                message: res.message,
                type: "success"
              });
              this.rejectFormVisible = false;
              // this.resetSendForm();
              this.rejectFormModel = JSON.parse(
                JSON.stringify(this.rejectFormModelInit)
              );
              this.reloadPage(this.tousuModel.id);
            }
          })
          .catch(error => {
            console.log(error);
          });
      }
    },
    updatetoususstatus(type) {
      var confirmMsg = "确定" + type + "吗！";
      this.$confirm(confirmMsg, "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          if (type == "驳回" || type == "关闭") {
            //弹框选择原因
            this.dealResultSelCondition.type = "DealResult";
            this.rejectFormTitle = "处理结果";
            this.rejectFormVisible = true;
            this.rejectFormModel.status = type;
            appSvc
              .getDictionaryDOs(this.dealResultSelCondition)
              .then(
                res => {
                  this.dealResultSel = res.data;
                },
                error => {
                  console.log(error);
                }
              )
              .catch(() => {});
          } else {
            this.updatetousustatusmodel.status = type;
            if (type == "通过") {
              this.updatetousustatusmodel.status = "已审核";
            }
            this.updatetousustatusmodel.id = this.tousuModel.id;

            appSvc
              .updateTousuStatus(this.updatetousustatusmodel)
              .then(
                res => {
                  if (res.code == 10000) {
                    this.$message({
                      message: res.message,
                      type: "success"
                    });
                  } else {
                    this.$message({
                      message: res.message,
                      type: "warning"
                    });
                  }
                  this.reloadPage(this.tousuModel.id);
                },
                error => {
                  console.log(error);
                }
              )
              .catch(() => {});
          }
          //
        })
        .catch(() => {});
    }
  }
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
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
>>> .rg-card {
  min-width: 1000px;
  width: calc(100% - 50px);
  height: calc(100vh - 150px);
  margin: 15px auto;
  overflow-y: auto;
}
</style>
