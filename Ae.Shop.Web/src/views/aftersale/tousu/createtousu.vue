<template>
  <main class="container">
    <section id="indexPage">
      <main>
        <!-- 入库收货界面 -->
        <section>
          <el-form
            :model="createtousuFormModel"
            :inline="true"
            :rules="rules"
            ref="createtousuFormModel"
          >
            <el-form-item label="订单号" prop="orderNo">
              <label>{{orderBaseInfo.orderNo}}</label>
            </el-form-item>

            <el-form-item label="用户手机" prop="orderNo">
              <label>{{orderBaseInfo.userTel}}</label>
            </el-form-item>

            <el-form-item label="用户名" prop="orderNo">
              <label>{{orderBaseInfo.userName}}</label>
            </el-form-item>

            <el-form-item label="优先级:" prop="priority">
              <el-select
                v-model="createtousuFormModel.priority"
                size="small"
                clearable
                filterable
                placeholder="请选择优先级"
              >
                <el-option
                  v-for="item in prioritySel"
                  :key="item.key"
                  :label="item.value"
                  :value="item.key"
                ></el-option>
              </el-select>
            </el-form-item>
            <br />
            <el-form-item label="一级投诉类型:" prop="tousuType">
              <el-select
                v-model="createtousuFormModel.tousuType"
                size="small"
                clearable
                filterable
                placeholder="请选择投诉类型"
                @change="creategettousuSubType1"
                class="margin-right-10"
              >
                <el-option
                  v-for="item in createtousuTypeSel"
                  :key="item.typeText"
                  :label="item.typeText"
                  :value="item.typeText"
                ></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="二级投诉类型:" prop="tousuSubType1">
              <el-select
                v-model="createtousuFormModel.tousuSubType1"
                size="small"
                clearable
                filterable
                @change="creategettousuSubType2"
                placeholder="请选择二级投诉类型"
                class="margin-right-10"
              >
                <el-option
                  v-for="item in createtousuSubType1Sel"
                  :key="item.typeText"
                  :label="item.typeText"
                  :value="item.typeText"
                ></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="三级投诉类型:">
              <el-select
                v-model="createtousuFormModel.tousuSubType2"
                size="small"
                clearable
                filterable
                placeholder="请选择三级投诉类型"
                class="margin-right-10"
              >
                <el-option
                  v-for="item in createtousuSubType2Sel"
                  :key="item.typeText"
                  :label="item.typeText"
                  :value="item.typeText"
                ></el-option>
              </el-select>
            </el-form-item>
            <br />
            <el-form-item label="投诉主题:" prop="subject">
              <el-input clearable placeholder="请输入主题" v-model="createtousuFormModel.subject"></el-input>
            </el-form-item>
            <br />
            <el-form-item label="描述:" size="100">
              <el-input
                type="textarea"
                :rows="7"
                :cols="30"
                placeholder="请输入描述"
                v-model="createtousuFormModel.description"
              ></el-input>
            </el-form-item>
            <br />
            <el-form-item label="投诉图片:" size="50">
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
                <el-button size="small" type="primary">上传图片</el-button>
                <div slot="tip" class="el-upload__tip">大小小于500kb的jpg/png文件</div>
              </el-upload>
            </el-form-item>
          </el-form>

          <div slot="footer" class="dialog-footer">
            <el-button @click="createtousuCancel()">取消</el-button>
            <el-button type="primary" @click="createtousuSave('createtousuFormModel')">保存</el-button>
          </div>
        </section>
      </main>
    </section>
    <!-- 入库收货界面 -->
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
      pickerOptionsStart: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        }
      },
      input: undefined,
      actualInStockNum: 1,
      readonly: true,
      tableLoading: false,
      currentPage: 1,
      //flag: this.global.deletedFlag,
      //下拉框的条件

      statusSelCondition: {
        RequestType: 1
      },
      prioritySelCondition: {
        RequestType: 2
      },

      tousuTypeCondition: {
        parentName: undefined
      },

      createtousuTypeCondition: {
        parentName: undefined
      },

      createtousuTypeSel: [],
      createtousuSubType1Sel: [],

      createtousuSubType2Sel: [],
      statusSel: [],

      prioritySel: [],

      tousuTypeSel: [],
      tousuSubType1Sel: [],

      tousuSubType2Sel: [],

      //table页面的条件
      condition: {
        id: undefined,
        orderNo: undefined,
        status: undefined,
        pageIndex: 1,
        pageSize: 10,
        //产品编号
        priority: undefined,
        tousuType: undefined,
        tousuSubType1: undefined,
        tousuSubType2: undefined
      },

      orderBaseInfo: {
        orderNo: undefined,
        userTel: undefined,
        userName: undefined,
        userId: undefined
      },

      createtousuFormTitle: undefined,
      createtousuFormVisible: false,
      fileList: [],
      uploadFileModel: {
        filename: undefined,
        fileurl: undefined
      },

      uploadFileModelInit: {
        filename: undefined,
        fileurl: undefined
      },

      createtousuFormModel: {
        orderNo: undefined,
        priority: undefined,
        tousuType: undefined,
        tousuSubType1: undefined,
        tousuSubType2: undefined,
        subject: undefined,
        description: undefined,
        fileList: []
      },

      orderCondition: {
        orderNo: undefined
      },

      createtousuFormModelInit: {
        orderNo: undefined,
        priority: undefined,
        tousuType: undefined,
        tousuSubType1: undefined,
        tousuSubType2: undefined,
        subject: undefined,
        description: undefined,
        fileList: []
      },
      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",
      rules: {
        priority: [
          { required: true, message: "请选择优先级", trigger: "blur" }
        ],

        tousuType: [
          { required: true, message: "请选择一级投诉类型", trigger: "blur" }
        ],
        tousuSubType1: [
          { required: true, message: "请选择一级投诉类型", trigger: "blur" }
        ],
        subject: [
          { required: true, message: "请输入投诉主题", trigger: "blur" }
        ]
      },
      formLabelWidth: "120px"
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
    this.orderCondition.orderNo = this.$route.query.orderNo;
    //页面初始化函数
    this.fetchData();
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
      debugger;
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
            //model重新清空

            this.uploadFileModel = JSON.parse(
              JSON.stringify(this.uploadFileModelInit)
            );
            this.uploadFileModel.filename = fileName;
            this.uploadFileModel.fileurl = key;

            this.fileList.push(this.uploadFileModel);
          }
        );
      });
    },

    createtousuCancel() {
      this.createtousuFormVisible = false;

      this.createtousuFormModel = JSON.parse(
        JSON.stringify(this.createtousuFormModelInit)
      );
    },

    createtousuSave(formName) {
      debugger;
      var vm = this;
      this.createtousuFormModel.fileList = this.fileList;
      this.createtousuFormModel.orderNo = this.orderBaseInfo.orderNo;
      console.log("formModel: " + JSON.stringify(this.createtousuFormModel));
      this.$refs[formName].validate(valid => {
        if (valid) {
          appSvc
            .createTousu(this.createtousuFormModel)
            .then(
              res => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  var tousuId = res.data;
                  this.createtousuFormModel = JSON.parse(
                    JSON.stringify(this.createtousuFormModelInit)
                  );
                  var frmName =
                    typeof formName === "string" && formName
                      ? formName
                      : "createtousuFormModel";
                  this.$refs[frmName].resetFields();

                  this.$router.push({
                    path: "tousudetail",
                    query: { id: tousuId }
                  });
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
        } else {
          return false;
        }
      });
    },
    creategettousuSubType1(val) {
      debugger;
      //获取英文
      let obj = {};
      var typeValue = "";
      for (var i = 0; i < this.createtousuTypeSel.length; i++) {
        if (this.createtousuTypeSel[i].typeText == val) {
          typeValue = this.createtousuTypeSel[i].typeValue;
          break;
        }
      }

      this.createtousuTypeCondition.parentName = typeValue;
      this.createtousuFormModel.tousuSubType1 = undefined;
      appTypeSvc
        .getTousuTypes(this.createtousuTypeCondition)
        .then(
          res => {
            this.createtousuSubType1Sel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    creategettousuSubType2(val) {
      var typeValue = "";
      for (var i = 0; i < this.createtousuSubType1Sel.length; i++) {
        if (this.createtousuSubType1Sel[i].typeText == val) {
          typeValue = this.createtousuSubType1Sel[i].typeValue;
          break;
        }
      }

      this.createtousuTypeCondition.parentName = typeValue;
      this.createtousuFormModel.tousuSubType2 = undefined;
      appTypeSvc
        .getTousuTypes(this.createtousuTypeCondition)
        .then(
          res => {
            this.createtousuSubType2Sel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    expandSelect(row, expandedRows) {
      // expandedRows.length != 0 ? console.log(row.id) : ''
      if (expandedRows.length != 0) {
        //在tableData中循环

        let obj = {};
        obj = this.tableData.find(item => {
          return item.taskId === row.taskId;
        });

        row.products = obj.products;
      }
    },
    clickRow(row, column, event) {
      console.log(row.id);
      // console.log(column)
      // console.log(event)
    },
    //获取采购状态
    getPriority() {
      appSvc
        .getBasicInfoList(this.prioritySelCondition)
        .then(
          res => {
            this.prioritySel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    fetchData() {
      debugger;
      //加载投诉优先级
      this.getPriority();

      //加载投诉类型
      this.createtousuTypeCondition.parentName = "tousuType";
      this.createtousuFormModel.tousuType = undefined;
      appTypeSvc
        .getTousuTypes(this.createtousuTypeCondition)
        .then(
          res => {
            this.createtousuTypeSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});

      //加载订单数据
      appSvc
        .getOrderInfo(this.orderCondition)
        .then(
          res => {
            this.orderBaseInfo = res.data;
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
  margin-top: 30px;
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
