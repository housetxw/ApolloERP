<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <rg-page
        background
        id="indexPage"
        :pageIndex="condition.pageIndex"
        :pageSize="condition.pageSize"
        :dataCount="totalList"
        :tableLoading="tableLoading"
        :tableData="tableData"
        :pageChange="pageTurning"
        :headerBtnWidth="180"
        :searching="search"
        :defaultCollapse="false"
        :data="tableData"
        ref="multipleTable"
        :handleSelectionChange="handleSelectionChange"
        :cell-style="cellStyle"
      >
        <template v-slot:condition>
          <el-form-item prop="checkStatus">
            <el-select v-model="condition.checkStatus" clearable filterable placeholder="请选择审核状态">
              <el-option
                v-for="item in checkStatueSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item prop="onlineStatus">
            <el-select v-model="condition.onlineStatus" clearable filterable placeholder="请选择上下架状态">
              <el-option
                v-for="item in onlineStatusSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item prop="title">
            <el-input v-model="condition.title" clearable placeholder="请输入标题" />
          </el-form-item>
          <el-form-item prop="times" label="创建时间">
            <el-date-picker
              v-model="condition.times"
              type="datetimerange"
              :picker-options="sendpickerOptions"
              range-separator="至"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              align="right"
              style="width:300px;"
            ></el-date-picker>
          </el-form-item>
          <br />
          <el-form-item>
            <span>
              已选中
              <label style="color:red;">{{selectNum}}</label>项
            </span>
            <el-button type="primary" size="mini" @click="batchOnline(1)">批量上架</el-button>&nbsp;&nbsp;
            <el-button type="warning" size="mini" @click="batchOnline(0)">批量下架</el-button>
          </el-form-item>
        </template>

        <template v-slot:header_btn>
          <el-button type="primary" size="mini" icon="el-icon-plus" @click="showCreate('create')">新增</el-button>
        </template>

        <template v-slot:tb_cols>
          <!-- <el-table
            border
            style="width: 100%;margin-top:10px;"
            :data="tableData"
            ref="multipleTable"
            @selection-change="handleSelectionChange"
            :cell-style="cellStyle"
          >-->
          <el-table-column type="selection"></el-table-column>
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
          <el-table-column label="上架时间">
            <template slot-scope="scope">
              <label v-show="scope.row.onlineStatus === 1?true:false">{{scope.row.publishTime}}</label>
            </template>
          </el-table-column>

          <el-table-column label="海报" width="300px">
            <template slot-scope="scope">
              <img
                :src="scope.row.content"
                style="width:200px;height:200px;padding:5px;"
                v-if="scope.row.content!=''"
                @error="imgError(scope.row)"
              />
            </template>
          </el-table-column>
          <el-table-column label="创建人" prop="createBy"></el-table-column>
          <el-table-column label="创建时间" prop="createTime"></el-table-column>
          <el-table-column label="操作" width="100" fixed="right">
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
              <el-button type="text" size="small" @click="showDetail(scope.row)">编辑</el-button>
              <el-button type="text" size="small" @click="viewhistory(scope.row)">操作历史</el-button>
            </template>
          </el-table-column>
        </template>
      </rg-page>
    </section>

    <!-- 首页 -->
    <!-- 操作日志 -->
    <section id="historySection">
 
          <rg-dialog
        :v-title="logTitle"
        :visible.sync="logVisible"
        :beforeClose="viewHistoryCancel"
        :btnCancel="{label:'关闭',click:(done)=>{viewHistoryCancel()}}"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="1000px"
        minWidth="600px"
        :close-on-click-modal="false"
      >
        <template v-slot:content>
        <el-table border :data="logTableData" style="width: 100%">
          <el-table-column label="编号" prop="id" width="100px"></el-table-column>
          <el-table-column label="操作" prop="remark"></el-table-column>
          <el-table-column label="操作人" prop="createBy" width="120px"></el-table-column>
          <el-table-column label="操作时间" prop="createTime" width="180px"></el-table-column>
          <!-- <el-table-column label="操作" width="80px">
            <template slot-scope="scope">
              <el-button type="text" size="small" @click="viewLogDetail(scope.row)">详情</el-button>
            </template>
          </el-table-column>-->
        </el-table>
        </template>
      </rg-dialog>
    </section>
    <!-- 操作日志 -->

    <!-- 操作日志 -->
    <section id="historyDetailSection">
         <rg-dialog
        :v-title="logDetailTitle"
        :visible.sync="logDetailVisible"
        :beforeClose="logDetailCancel"
        :btnCancel="{label:'关闭',click:(done)=>{logDetailCancel()}}"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="1000px"
        minWidth="600px"
        :close-on-click-modal="false"
      >
        <template v-slot:content>
     
        <el-table border :data="logDetailTableData" style="width: 100%">
          <el-table-column label="操作前" prop="beforeValue"></el-table-column>
          <el-table-column label="操作后" prop="afterValue"></el-table-column>
        </el-table>
        </template> 
      </rg-dialog>
    </section>
    <!-- 操作日志 -->
    <!-- 新增海报 -->
    <section id="createposterDialog">
        <rg-dialog
        :v-title="createposterFormTitle"
        :visible.sync="createposterFormVisible"
        :beforeClose="createposterCancel"
        :btnCancel="{label:'关闭',click:(done)=>{createposterCancel()}}"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="1000px"
        minWidth="600px"
        :close-on-click-modal="false"
      >
        <template v-slot:content>
        <el-form :model="createposterFormModel" :rules="rules" ref="createposterFormModel">
          <el-form-item label="标题:" prop="title" :label-width="formLabelWidth">
            <el-input clearable placeholder="请输入标题" v-model="createposterFormModel.title" style="width:80%"></el-input>
          </el-form-item>
          <el-form-item label="描述:" size="50" :label-width="formLabelWidth">
            <el-input
              type="textarea"
              :rows="5"
              placeholder="请输入描述"
              v-model="createposterFormModel.description"
              style="width:80%"
            ></el-input>
          </el-form-item>

          <el-form-item label="海报:" size="50" :label-width="formLabelWidth">
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
        </template>
           <template v-slot:footer>
               <el-button type="primary" @click="createposterSave('createposterFormModel')" size="mini">保存</el-button>

        </template>
      </rg-dialog>
      <el-dialog :visible.sync="dialogVisible">
        <img width="100%" :src="dialogImageUrl" alt />
      </el-dialog>
    </section>

    <!-- 查看海报详情 -->
    <section id="posterDetailDialog">
      <rg-dialog
        :v-title="posterDetailFormTitle"
        :visible.sync="posterDetailFormVisible"
        :beforeClose="posterDetailCancel"
        :btnCancel="{label:'关闭',click:(done)=>{posterDetailCancel()}}"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="1000px"
        minWidth="600px"
        :close-on-click-modal="false"
      >
        <template v-slot:content>
          <el-form :model="posterDetailFormModel" ref="posterDetailFormModel">
            <el-form-item label="标题" prop="title" :label-width="formLabelWidth">
              <el-input clearable placeholder="请输入标题" v-model="posterDetailFormModel.title" style="width:80%"></el-input>
            </el-form-item>
            <el-form-item label="描述" size="50" :label-width="formLabelWidth" >
              <el-input
                type="textarea"
                clearable
                :rows="5"
                placeholder="请输入描述"
                v-model="posterDetailFormModel.description"
                style="width:80%"
              ></el-input>
            </el-form-item>

            <el-form-item label="海报" size="50" :label-width="formLabelWidth">
              <el-upload
                class="upload-demo"
                action="http://upload.qiniup.com/"
                :on-remove="handleRemove1"
                :before-remove="beforeRemove1"
                :limit="1"
                list-type="picture-card"
                :http-request="uploadRequest1"
                :on-preview="handlePictureCardPreview"
                name="image1"
                :file-list="fileList2"
                accept="image/jpeg, image/png, image/jpg"
              >
                <i class="el-icon-plus"></i>
              </el-upload>
              <!-- <img :src="posterDetailFormModel.content" style="width:100%; height:100%;" /> -->
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button type="primary" @click="posterDetailsave()" size="mini">保存</el-button>
        </template>
      </rg-dialog>
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
      sendpickerOptions: {
        // disabledDate(time) {
        //   return time.getTime() < Date.now();
        // },
        shortcuts: [
          {
            text: "最近一周",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近一个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近三个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit("pick", [start, end]);
            }
          }
        ]
      },
      logTableData: [],
      logTitle: undefined,
      logVisible: false,
      logCondition: {
        objectId: undefined,
        objectType: undefined
      },
      logConditionInit: {
        objectId: undefined,
        objectType: undefined
      },

      logDetailTableData: [],
      logDetailTitle: undefined,
      logDetailVisible: false,
      logDetailCondition: {
        id: undefined
      },
      logDetailConditionInit: {
        id: undefined
      },
      selectNum: 0,
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
        pageSize: 10,
        title: undefined,
        times: undefined
      },
      batchOnlineCondition: {
        ids: [],
        type: undefined
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
      fileList2: [],
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
    imgError(row){
row.content=''
    },
    beforeRemove1(file, fileList) {
      return false;
    },
    handleRemove1(file, fileList) {},

    uploadRequest1: function(request) {},
    logDetailCancel() {
      this.logDetailVisible = false;
      this.logDetailCondition.id = undefined;
    },
    viewLogDetail(row) {
      this.logDetailTitle = "单号【" + row.id + "】的详细操作记录";
      this.logDetailVisible = true;
      this.logDetailCondition.id = row.id;

      appSvc
        .getActivityLogDetail(this.logDetailCondition)
        .then(
          res => {
            debugger;
            this.logDetailTableData.push(res.data);
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },

    viewhistory(row) {
      this.logTitle = "单号【" + row.id + "】的操作记录";
      this.logVisible = true;
      this.logCondition.objectId = row.id;
      this.logCondition.objectType = "Promote";
      appSvc
        .getActivityLogs(this.logCondition)
        .then(
          res => {
            this.logTableData = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    viewHistoryCancel() {
      this.logVisible = false;
      this.logCondition = JSON.parse(JSON.stringify(this.logConditionInit));
    },
    toggleSelection(rows) {
      if (rows) {
        rows.forEach(row => {
          this.$refs.multipleTable.toggleRowSelection(row);
        });
      } else {
        this.$refs.multipleTable.clearSelection();
      }
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
      this.selectNum = val.length;
    },
    batchOnline(status) {
      var msg = status == 1 ? "上架" : "下架";
      this.$refs.multipleTable.selection = this.multipleSelection;
      if (this.$refs.multipleTable.selection.length > 0) {
        //上架操作
        if (status == 1) {
          var tempArr = [];
          //如果没有需要上架的数据 系统需要提示
          for (var i = 0; i < this.$refs.multipleTable.selection.length; i++) {
            if (this.$refs.multipleTable.selection[i].onlineStatus == 0) {
              tempArr.push(this.$refs.multipleTable.selection[i].id);
            }
          }
          if (tempArr.length > 0) {
            this.batchOnlineCondition.ids = tempArr;
            this.batchOnlineCondition.type = 1;

            this.$confirm(
              "您将批量上架" +
                tempArr.length +
                "个项目，此操作不可逆，请仔细核对后批量上架。确认是否继续批量上架?",
              "警告",
              {
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                type: "warning"
              }
            )
              .then(() => {
                const loading = this.$loading({
                  lock: true,
                  text: "拼命上架中......",
                  spinner: "el-icon-loading",
                  background: "rgba(0, 0, 0, 0.7)"
                });
                appSvc
                  .updatePromoteStatus(this.batchOnlineCondition)
                  .then(
                    res => {
                      this.batchOnlineCondition.ids = [];
                      this.batchOnlineCondition.type = undefined;

                      for (var j = 0; j < tempArr.length; j++) {
                        for (var i = 0; i < this.tableData.length; i++) {
                          if (tempArr[j] == this.tableData[i].id) {
                            this.tableData[i].onlineStatus = 1;
                            this.tableData[i].publishTime = this.dateFormat();
                            break;
                          }
                        }
                      }
                      this.$refs.multipleTable.clearSelection();
                      if (res.code == 10000) {
                        this.$message({
                          message:
                            "已成功为您批量上架" + tempArr.length + "个项目",
                          type: "success"
                        });
                      }
                    },
                    error => {
                      console.log(error);
                    }
                  )
                  .catch(() => {})
                  .finally(() => {
                    loading.close();
                  });
              })
              .catch(() => {});
          } else {
            this.$message({
              message: "您选择的项目都是已上架项目，请仔细核对后重新批量上架",
              type: "warning"
            });
          }
        }
        //下架操作
        else {
          var tempArr = [];
          //如果没有需要上架的数据 系统需要提示
          for (var i = 0; i < this.$refs.multipleTable.selection.length; i++) {
            if (this.$refs.multipleTable.selection[i].onlineStatus == 1) {
              tempArr.push(this.$refs.multipleTable.selection[i].id);
            }
          }
          if (tempArr.length > 0) {
            this.batchOnlineCondition.ids = tempArr;
            this.batchOnlineCondition.type = 0;

            this.$confirm(
              "您将批量下架" +
                tempArr.length +
                "个项目，此操作不可逆，请仔细核对后批量下架。确认是否继续批量下架?",
              "警告",
              {
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                type: "warning"
              }
            )
              .then(() => {
                const loading = this.$loading({
                  lock: true,
                  text: "拼命下架中......",
                  spinner: "el-icon-loading",
                  background: "rgba(0, 0, 0, 0.7)"
                });
                appSvc
                  .updatePromoteStatus(this.batchOnlineCondition)
                  .then(
                    res => {
                      this.batchOnlineCondition.ids = [];
                      this.batchOnlineCondition.type = undefined;

                      for (var j = 0; j < tempArr.length; j++) {
                        for (var i = 0; i < this.tableData.length; i++) {
                          if (tempArr[j] == this.tableData[i].id) {
                            this.tableData[i].onlineStatus = 0;
                            break;
                          }
                        }
                      }
                      this.$refs.multipleTable.clearSelection();

                      if (res.code == 10000) {
                        this.$message({
                          message:
                            "已成功为您批量下架" + tempArr.length + "个项目",
                          type: "success"
                        });
                      }
                    },
                    error => {
                      console.log(error);
                    }
                  )
                  .catch(() => {})
                  .finally(() => {
                    loading.close();
                  });
              })
              .catch(() => {});
          } else {
            this.$message({
              message: "您选择的项目都是已下架项目，请仔细核对后重新批量下架!",
              type: "warning"
            });
          }
        }
      } else {
        this.$message({
          message: "请选择需要操作的项目!",
          type: "warning"
        });
      }
    },
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

            this.uploadFileModel = JSON.parse(
              JSON.stringify(this.uploadFileModelInit)
            );
            this.uploadFileModel.name = "";
            this.uploadFileModel.url = this.posterDetailFormModel.content;
            this.fileList2.push(this.uploadFileModel);
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
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
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
>>> .el-form--inline .el-form-item {
  padding-bottom: 10px !important ;
}
</style>
