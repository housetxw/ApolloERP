<template>
  <main class="container">
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :tableData="tableData"
      :pageChange="pageTurning"
      :searching="search"
      :conditionModel="condition"
      :defaultCollapse="false"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-input
            v-model="condition.id"
            clearable
            @change="updateId()"
            placeholder="请输入投诉单号"
            style="width:120px;"
          />
        </el-form-item>
        <el-form-item>
          <el-input
            v-model="condition.orderNo"
            clearable
            @change="updateOrderNo()"
            placeholder="请输入订单号"
            style="width:120px;"
          />
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.status"
            clearable
            filterable
            style="width:120px;"
            placeholder="请选择投诉状态"
          >
            <el-option
              v-for="item in statusSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>
          <el-select
            v-model="condition.priority"
            clearable
            filterable
            style="width:120px;"
            placeholder="请选择优先级"
          >
            <el-option
              v-for="item in prioritySel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>
          <el-select
            v-model="condition.tousuType"
            clearable
            filterable
            style="width:150px;"
            placeholder="请选择投诉类型"
            @change="gettousuSubType1"
          >
            <el-option
              v-for="item in tousuTypeSel"
              :key="item.typeText"
              :label="item.typeText"
              :value="item.typeText"
            ></el-option>
          </el-select>
          <el-select
            v-model="condition.tousuSubType1"
            clearable
            filterable
            style="width:150px;"
            @change="gettousuSubType2"
            placeholder="请选择二级投诉类型"
          >
            <el-option
              v-for="item in tousuSubType1Sel"
              :key="item.typeText"
              :label="item.typeText"
              :value="item.typeText"
            ></el-option>
          </el-select>
          <el-select
            v-model="condition.tousuSubType2"
            clearable
            style="width:150px;"
            filterable
            placeholder="请选择三级投诉类型"
          >
            <el-option
              v-for="item in tousuSubType2Sel"
              :key="item.typeText"
              :label="item.typeText"
              :value="item.typeText"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="shopId">
          <el-select
            v-model="condition.shopId"
            filterable
            remote
            clearable
            reserve-keyword
            placeholder="请输入门店名称"
            :remote-method="getShopinfo"
            :loading="loading"
            style="width: 300px"
          >
            <el-option
              v-for="item in shopSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button size="mini" type="primary" icon="el-icon-plus" @click="()=>{ $router.push({ name: 'createtousu', query: { orderNo: '' } })}">新增</el-button>
      </template>
      <template v-slot:tb_cols>
        <el-table-column label="投诉单号" prop="id" width="80px;"></el-table-column>

        <!-- orderDetails -->
        <rg-table-column label="订单号">
          <template slot-scope="scope">
            <router-link
              :to="{name:'orderdetails', params: {orderNo: scope.row.orderNo,onlyview:1  }}"
            >
              <el-button size="mini" type="text">{{scope.row.orderNo}}</el-button>
            </router-link>
          </template>
        </rg-table-column>

        <rg-table-column label="门店" prop="shopName" />
        <rg-table-column label="状态" prop="status" />
        <rg-table-column label="优先级" prop="priority" />
        <rg-table-column label="投诉主题" prop="subject" />
        <rg-table-column label="一级投诉类型" prop="tousuType" />
        <rg-table-column label="二级投诉类型" prop="tousuSubType1" />
        <rg-table-column label="三级投诉类型" prop="tousuSubType2" />
        <rg-table-column label="创建人" prop="createBy" />
        <rg-table-column label="创建时间" prop="createTime" is-datetime fix-min />
        <rg-table-column label="操作" width="70px" fix="right">
          <template slot-scope="scope">
            <router-link :to="{ path: 'tousudetail', query: { id: scope.row.id } }">
              <el-button type="primary" size="mini">详情</el-button>
            </router-link>
          </template>
        </rg-table-column>
      </template>
    </rg-page>
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
  name: "tousuList",
  data() {
    return {
      detail: {
        orderNo: "",
        v_detail: false
      },
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

      shopSel: [],
      shopSelCondition: {
        simpleName: undefined,
      },

      //table页面的条件
      condition: {
        id: undefined,
        shopId: undefined,
        orderNo: undefined,
        status: undefined,
        pageIndex: 1,
        pageSize: 20,
        //产品编号
        priority: undefined,
        tousuType: undefined,
        tousuSubType1: undefined,
        tousuSubType2: undefined
      },

      fileList: [],
      uploadFileModel: {
        filename: undefined,
        fileurl: undefined
      },

      uploadFileModelInit: {
        filename: undefined,
        fileurl: undefined
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

    //获取优先级
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
    
    getShopinfo(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.simpleName = query;
          appSvc
            .getShopList(this.shopSelCondition)
            .then(
              (res) => {
                this.shopSel = res.data;
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        }, 200);
      } else {
        this.options = [];
      }
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
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },

    showCreate(type, row) {
      this.createtousuTypeCondition.parentName = "tousuType";
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
