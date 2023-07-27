<template>
  <main class="container">
    <!-- 首页 -->
    <section>
      <rg-page
        id="indexPage"
        :pageIndex="condition.pageIndex"
        :pageSize="condition.pageSize"
        :dataCount="totalList"
        :tableLoading="tableLoading"
        :tableData="tableData"
        :pageChange="pageChange"
        :headerBtnWidth="180"
        :searching="search"
        :defaultCollapse="false"
      >
        <template v-slot:condition>
          <el-form-item>
            <el-input
              v-model="condition.id"
              clearable
              style="width:175px;"
              placeholder="请输入编号"
              size="mini"
            />
            <!-- 后期会修改为名称检索 -->
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.venderId"
              size="mini"
              clearable
              filterable
              placeholder="请选择供应商"
            >
              <el-option
                v-for="item in venderSel"
                :key="item.id"
                :label="item.venderShortName"
                :value="item.id"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.type"
              size="mini"
              clearable
              filterable
              placeholder="请选择状态"
            >
              <el-option
                v-for="item in statusSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.supplyType"
              size="mini"
              clearable
              filterable
              placeholder="请选择类型"
            >
              <el-option
                v-for="item in supplyTypeSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.provinceId"
              size="mini"
              clearable
              filterable
              placeholder="请选择省"
              @change="getCity"
            >
              <el-option
                v-for="item in provinceSel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.cityId"
              size="mini"
              clearable
              filterable
              @change="getDistrict"
              placeholder="请选择市"
            >
              <el-option
                v-for="item in citySel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.districtId"
              size="mini"
              clearable
              filterable
              placeholder="请选择区"
            >
              <el-option
                v-for="item in districtSel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>
        </template>
        <template v-slot:header_btn>
          <el-button type="primary" size="mini" icon="el-icon-plus" @click="showCreate()">新增</el-button>
        </template>
        <template v-slot:tb_cols>
          <el-table-column label="编号" prop="id" align="center"></el-table-column>
          <el-table-column label="简称" prop="venderShortName" align="center"></el-table-column>
          <el-table-column label="全称" prop="venderName" align="center"></el-table-column>
          <el-table-column label="类型" prop="supplyType" align="center"></el-table-column>

          <el-table-column label="区域" prop="regionName" align="center"></el-table-column>
          <el-table-column label="状态" width="100px" align="center">
            <template slot-scope="scope">
              <div v-if="scope.row.isActive === 1 && scope.row.isDel === 0 ">
                <label>正常</label>
              </div>
              <div v-else-if="scope.row.isActive === 0 && scope.row.isDel === 0 ">
                <label>停用</label>
              </div>
              <div v-else-if="scope.row.isDel === 1 ">
                <label>删除</label>
              </div>
            </template>
          </el-table-column>

          <el-table-column label="创建人" prop="createBy" align="center"></el-table-column>
          <el-table-column label="创建时间" prop="createTime" align="center"></el-table-column>
          <el-table-column label="操作" fixed="right" width="180" align="center">
            <template slot-scope="scope">
              <el-button
                size="mini"
                type="primary"
                v-show="(scope.row.isDel ==0 && scope.row.isActive == 1)?true:false"
                style="padding:10px 7px;"
                @click="unActiveVender(scope.row)"
              >停用</el-button>
              <el-button
                size="mini"
                type="primary"
                style="padding:10px 7px;"
                v-show="(scope.row.isDel ==0 && scope.row.isActive == 0)?true:false"
                @click="activeVender(scope.row)"
              >启用</el-button>
              <el-button
                size="mini"
                type="danger"
                style="padding:10px 7px;"
                v-show="scope.row.isDel ==0?true:false"
                @click="deleteVender(scope.row)"
              >删除</el-button>
              <el-button
                size="mini"
                type="primary"
                style="padding:10px 7px;"
                v-show="scope.row.isDel !=1?true:false"
                @click="editvender(scope.row)"
              >编辑</el-button>
            </template>
          </el-table-column>
        </template>
      </rg-page>
    </section>
    <!-- 首页 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appVenderSvc } from "@/api/purchase/vender";
import { isNumber } from "util";
export default {
  data() {
    return {
      pickerOptionsStart: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        }
      },
      input: undefined,

      readonly: true,
      tableLoading: false,
      currentPage: 1,
      //flag: this.global.deletedFlag,
      //下拉框的条件

      statusSelCondition: {
        RequestType: 2
      },

      supplyTypeSelCondition: {
        RequestType: 1
      },
      provinceSel: [],
      supplyTypeSel: [],
      citySel: [],
      districtSel: [],
      regionCondition: {
        regionId: undefined
      },

      //table页面的条件
      condition: {
        pageIndex: 1,
        pageSize: 10,
        id: undefined,
        venderId: undefined,
        type: undefined,
        supplyType: undefined,
        provinceId: undefined,
        cityId: undefined,
        districtId: undefined,
        backType: undefined
      },
      deleteCondition: {
        id: undefined,
        isActive: undefined,
        isDeleted: undefined,
        updateType: undefined
      },

      statusSel: [],
      venderSel: [],

      tableData: [],
      totalList: 0,
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
    showCreate() {
      this.$router.push({
        path: "createvender"
      });
    },
    editvender(row) {
      this.$router.push({
        path: "editvender",
        query: { id: row.id }
      });
    },
    deleteVender(row) {
      this.$confirm(
        "确定删除供应商【" + row.venderShortName + "】吗！",
        "警告",
        {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning"
        }
      )
        .then(() => {
          var vm = this;
          this.deleteCondition.id = row.id;
          this.deleteCondition.isDeleted = 1;
          this.deleteCondition.updateType = 2;
          appVenderSvc
            .deleteVender(this.deleteCondition)
            .then(
              res => {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                }

                vm.search();

                vm.cancel();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },
    unActiveVender(row) {
      this.$confirm(
        "确定停用供应商【" + row.venderShortName + "】吗！",
        "警告",
        {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning"
        }
      )
        .then(() => {
          var vm = this;
          this.deleteCondition.id = row.id;
          this.deleteCondition.isActive = 0;
          this.deleteCondition.updateType = 1;
          appVenderSvc
            .deleteVender(this.deleteCondition)
            .then(
              res => {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                }

                vm.search();

                vm.cancel();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },
    activeVender(row) {
      this.$confirm(
        "确定启用供应商【" + row.venderShortName + "】吗！",
        "警告",
        {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning"
        }
      )
        .then(() => {
          var vm = this;
          this.deleteCondition.id = row.id;
          this.deleteCondition.isActive = 1;
          this.deleteCondition.updateType = 1;
          appVenderSvc
            .deleteVender(this.deleteCondition)
            .then(
              res => {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                }

                vm.search();

                vm.cancel();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },
    changeType(val) {
      if (val != "" && val != undefined) {
        if (val == "已停用") {
          this.condition.backType = 2;
        } else if (val == "正常") {
          this.condition.backType = 1;
        } else if (val == "已删除") {
          this.condition.backType = 3;
        }
      } else {
        this.condition.backType = undefined;
      }
    },
    getCity() {
      debugger;
      if (
        this.condition.provinceId != "" &&
        this.condition.provinceId != undefined
      ) {
        this.regionCondition.regionId = this.condition.provinceId;
        appVenderSvc
          .getRegionChinaListByRegionId(this.regionCondition)
          .then(
            res => {
              this.citySel = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },

    getDistrict() {
      if (this.condition.cityId != "" && this.condition.cityId != undefined) {
        this.regionCondition.regionId = this.condition.cityId;

        appVenderSvc
          .getRegionChinaListByRegionId(this.regionCondition)
          .then(
            res => {
              this.districtSel = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },

    cellStyle(row, column, rowIndex, columnIndex) {
      if (row.row.isActive === 0 && row.column.label === "状态") {
        return "color:orange";
      }
      if (row.row.isDel === 1 && row.column.label === "状态") {
        return "color:red";
      }
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
    },

    clickRow(row, column, event) {
      console.log(row.id);
      // console.log(column)
      // console.log(event)
    },

    getSupplyType() {
      appVenderSvc
        .getBasicInfoList(this.supplyTypeSelCondition)
        .then(
          res => {
            //  this.tableData = res.data;
            this.supplyTypeSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    getVenderStatus() {
      appVenderSvc
        .getBasicInfoList(this.statusSelCondition)
        .then(
          res => {
            //  this.tableData = res.data;
            this.statusSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    getVenders() {
      appVenderSvc
        .getVenders()
        .then(
          res => {
            this.venderSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    fetchData() {
      this.getSupplyType();
      this.getVenderStatus();
      this.getVenders();
      appVenderSvc
        .getVenderList(this.condition)
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
      this.regionCondition.regionId = 0;
      appVenderSvc
        .getRegionChinaListByRegionId(this.regionCondition)
        .then(
          res => {
            this.provinceSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    pageChange(p) {
   this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    // pageTurning() {
    //   this.condition.pageIndex = this.currentPage;
    //   this.getPaged();
    // },

    getPaged(flag) {
      this.tableLoading = true;
      if (this.condition.id == "") {
        this.condition.id = undefined;
      }
      if (this.condition.type == "") {
        this.condition.type = undefined;
      }

      //选择了省必须选择市
      if (
        this.condition.provinceId != null &&
        this.condition.provinceId != undefined &&
        (this.condition.cityId == null || this.condition.cityId == undefined)
      ) {
        this.$message({
          message: "请选择市!",
          type: "warning"
        });
        return;
      }

      console.log("condition: " + JSON.stringify(this.condition));
      appVenderSvc
        .getVenderList(this.condition)
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
    height: 120px;
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
>>>.el-form--inline .el-form-item{
  padding-bottom:10px !important ;
}
</style>
