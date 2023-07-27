<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <header class="bodyTop">
        <article class="filter-container">
          <span class="input-label">投诉单号:</span>
          <el-input
            v-model="condition.id"
            clearable
            @change="updateId()"
            placeholder="请输入投诉单号"
            style="width:150px;"
          />

          <span class="input-label">订单号:</span>
          <el-input
            v-model="condition.orderNo"
            clearable
            @change="updateOrderNo()"
            placeholder="请输入订单号"
            style="width:150px;"
          />

          <span class="input-label">投诉状态:</span>
          <el-select
            v-model="condition.status"
            size="small"
            clearable
            filterable
            placeholder="请选择投诉状态"
            class="margin-right-10"
          >
            <el-option
              v-for="item in statusSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>

          <span class="input-label">优先级:</span>
          <el-select
            v-model="condition.priority"
            size="small"
            clearable
            filterable
            placeholder="请选择优先级"
            class="margin-right-10"
          >
            <el-option
              v-for="item in prioritySel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>

          <span class="input-label">一级投诉类型:</span>

          <el-select
            style="margin-top:7px;"
            v-model="condition.tousuType"
            size="small"
            clearable
            filterable
            placeholder="请选择投诉类型"
            @change="gettousuSubType1"
            class="margin-right-10"
          >
            <el-option
              v-for="item in tousuTypeSel"
              :key="item.typeText"
              :label="item.typeText"
              :value="item.typeText"
            ></el-option>
          </el-select>

          <span class="input-label">二级投诉类型:</span>

          <el-select
            v-model="condition.tousuSubType1"
            size="small"
            clearable
            filterable
            @change="gettousuSubType2"
            placeholder="请选择二级投诉类型"
            class="margin-right-10"
          >
            <el-option
              v-for="item in tousuSubType1Sel"
              :key="item.typeText"
              :label="item.typeText"
              :value="item.typeText"
            ></el-option>
          </el-select>
          <br />
          <span class="input-label">三级投诉类型:</span>

          <el-select
            style="margin-top:7px;"
            v-model="condition.tousuSubType2"
            size="small"
            clearable
            filterable
            placeholder="请选择三级投诉类型"
            class="margin-right-10"
          >
            <el-option
              v-for="item in tousuSubType2Sel"
              :key="item.typeText"
              :label="item.typeText"
              :value="item.typeText"
            ></el-option>
          </el-select>

          <!-- <el-input v-model="condition.name" clearable class="el-input el-input--suffix" placeholder="请输入系统名称"></el-input> -->
          <el-button
            type="primary"
            size="small"
            style="margin-left:30px;"
            @click="search(true)"
            icon="el-icon-search"
          >搜索</el-button>
          <el-button type="primary" size="small" @click="showCreate('create', null)">新增</el-button>
        </article>
      </header>
      <main>
        <section>
          <el-table border :data="tableData" style="width: 100%" :cell-style="cellStyle">
            <el-table-column label="投诉单号" prop="id" width="80px;"></el-table-column>

            <!-- orderDetails -->

            <el-table-column label="订单号" width="120px">
              <template slot-scope="scope">
                <!-- <router-link :to="{ name: 'orderDetails', query: { orderNo: scope.row.orderNo } }">
                    <a href style="font-size:14px;">{{scope.row.orderNo}}</a>
                </router-link>-->
                <router-link :to="{name:'orderdetails', query: { orderNo: 'RGC00024' }}">
                  <el-button type="text">{{scope.row.orderNo}}</el-button>
                </router-link>
              </template>
            </el-table-column>
            <el-table-column label="状态" prop="status"></el-table-column>
            <el-table-column label="任务主人" prop="owner"></el-table-column>

            <el-table-column label="优先级" prop="priority"></el-table-column>
            <el-table-column label="投诉主题" prop="subject"></el-table-column>

            <el-table-column label="一级投诉类型" prop="tousuType"></el-table-column>
            <el-table-column label="二级投诉类型" prop="tousuSubType1"></el-table-column>
            <el-table-column label="三级投诉类型" prop="tousuSubType2"></el-table-column>
            <el-table-column label="创建人" prop="createBy"></el-table-column>
            <el-table-column label="创建时间" prop="createTime"></el-table-column>
            <el-table-column label="操作" width="100">
              <template slot-scope="scope">
                <!-- 签收和入库是两个概念 -->
                <!-- <el-button
                  size="mini"
                  type="primary"
                 
                  @click="tousuDetail(scope.row)"
                  >详情</el-button
                >-->
                <router-link :to="{ path: 'tousudetail', query: { id: scope.row.id } }">
                  <el-button type="primary" size="small">详情</el-button>

                  <!-- <el-button
                    type="primary"
                    size="small"
                    icon="el-icon-edit"
                    style="margin-left:30px;"
                    >投诉详情</el-button
                  >-->
                </router-link>
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

    <!-- 入库收货界面 -->
    <section id="createtousuDialog">
      <el-dialog
        :title="createtousuFormTitle"
        :close-on-click-modal="false"
        :visible.sync="createtousuFormVisible"
        :before-close="createtousuCancel"
      >
        <el-form
          :model="createtousuFormModel"
          :inline="true"
          :rules="rules"
          ref="createtousuFormModel"
        >
          <el-form-item label="订单号" prop="orderNo">
            <el-input clearable placeholder="请输入订单号" v-model="createtousuFormModel.orderNo"></el-input>
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

          <el-form-item label="投诉主题:" prop="subject">
            <el-input clearable placeholder="请输入主题" v-model="createtousuFormModel.subject"></el-input>
          </el-form-item>
          <el-form-item label="描述:" size="50">
            <el-input
              type="textarea"
              :rows="5"
              placeholder="请输入描述"
              v-model="createtousuFormModel.description"
            ></el-input>
          </el-form-item>

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
      </el-dialog>
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
        orderNo: [{ required: true, message: "请输入订单号", trigger: "blur" }],
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
    //页面初始化函数
    this.fetchData();
  },
  methods: {
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
                  vm.search();
                  this.createtousuFormVisible = false;

                  this.createtousuFormModel = JSON.parse(
                    JSON.stringify(this.createtousuFormModelInit)
                  );
                  var frmName =
                    typeof formName === "string" && formName
                      ? formName
                      : "createtousuFormModel";
                  this.$refs[frmName].resetFields();
                  // vm.getConditionData();
                  vm.cancel();
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

    updateId() {
      if (this.condition.id == "") {
        this.condition.id = undefined;
      }
    },
    updateOrderNo() {
      if (this.condition.orderNo == "") {
        this.condition.orderNo = undefined;
      }
    },
    gettousuSubType1(val) {
      var typeValue = "";
      for (var i = 0; i < this.tousuTypeSel.length; i++) {
        if (this.tousuTypeSel[i].typeText == val) {
          typeValue = this.tousuTypeSel[i].typeValue;
          break;
        }
      }

      this.tousuTypeCondition.parentName = typeValue;
      this.condition.tousuSubType1 = undefined;
      appTypeSvc
        .getTousuTypes(this.tousuTypeCondition)
        .then(
          res => {
            this.tousuSubType1Sel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    gettousuSubType2(val) {
      var typeValue = "";
      for (var i = 0; i < this.tousuSubType1Sel.length; i++) {
        if (this.tousuSubType1Sel[i].typeText == val) {
          typeValue = this.tousuSubType1Sel[i].typeValue;
          break;
        }
      }

      this.tousuTypeCondition.parentName = typeValue;
      this.condition.tousuSubType2 = undefined;
      appTypeSvc
        .getTousuTypes(this.tousuTypeCondition)
        .then(
          res => {
            this.tousuSubType2Sel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
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

    //获取目标仓
    getStatus() {
      appSvc
        .getBasicInfoList(this.statusSelCondition)
        .then(
          res => {
            this.statusSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
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

    //获取一级投诉类型
    getTousuType() {
      this.tousuTypeCondition.parentName = "tousuType";
      appTypeSvc
        .getTousuTypes(this.tousuTypeCondition)
        .then(
          res => {
            this.tousuTypeSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    fetchData() {
      this.getStatus();
      this.getPriority();
      this.getTousuType();

      appSvc
        .getTousuList(this.condition)
        .then(
          res => {
            //  this.tableData = res.data;
            var data = res.data;

            this.tableData = data.tousuList;
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
      this.createtousuFormVisible = true;
      this.createtousuFormTitle = "创建投诉";

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
    },
    getPaged(flag) {
      this.tableLoading = true;

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getTousuList(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.tousuList;
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
