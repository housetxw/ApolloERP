<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <header class="bodyTop">
        <article class="filter-container">
          <span class="input-label">标题:</span>
          <el-input v-model="condition.title" style="width:200px;" clearable placeholder="请输入标题" />

          <span class="input-label">分类:</span>
          <el-select
            v-model="condition.categoryId"
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
          </el-select>&nbsp;&nbsp;&nbsp;
          <span class="input-label">上下架状态:</span>
          <el-select
            v-model="condition.status"
            size="small"
            clearable
            filterable
            placeholder="请选择上下架状态"
          >
            <el-option
              v-for="item in statusSel"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
          <br />
          <span class="input-label">五级车型:</span>
          <el-select
            style="padding-top:5px;"
            v-model="condition.brand"
            size="small"
            clearable
            filterable
            @change="changeBrand()"
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
            v-model="condition.vehicleSeries"
            size="small"
            clearable
            filterable
            @change="changevehicleSeries()"
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
            v-model="condition.paiLiang"
            size="small"
            clearable
            filterable
            @change="changepaiLiang()"
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
            v-model="condition.nian"
            size="small"
            clearable
            filterable
            @change="changenian()"
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
            style="width:350px"
            v-model="condition.salesName"
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
          </el-select>&nbsp;&nbsp;&nbsp;
          <el-button type="primary" size="small" @click="search()">查询</el-button>
          <el-button type="primary" size="small" @click="showCreate()">新增</el-button>
        </article>
      </header>
      <main>
        <section>
          <el-button type="primary" size="small" @click="batchpublish()">批量发布</el-button>&nbsp;&nbsp;
          <el-button type="danger" size="small" @click="batchdelete()">批量删除</el-button>
          <el-table
            border
            :data="tableData"
            ref="multipleTable"
            @click="batchdelete()"
            @selection-change="handleSelectionChange"
            style="width: 100%;margin-top:10px;"
          >
            <el-table-column type="expand">
              <template slot-scope="props">
                <el-form label-position="left" class="demo-table-expand">
                  <el-form-item label="内容:">
                    <label>{{props.row.content}}</label>
                  </el-form-item>
                </el-form>
              </template>
            </el-table-column>
            <el-table-column type="selection"></el-table-column>
            <el-table-column label="编号" prop="id" width="100px"></el-table-column>
            <el-table-column label="分类" prop="categoryName" width="120px"></el-table-column>
            <el-table-column label="车型" prop="vehicle"></el-table-column>
            <el-table-column label="标题" prop="title"></el-table-column>
            <el-table-column label="内容" prop="contentStr" width="400px"></el-table-column>
            <el-table-column label="状态" width="80px">
              <template slot-scope="scope">
                <div v-if="scope.row.status === 0">
                  <label style="color:red;">下架</label>
                </div>
                <div v-else-if="scope.row.status === 1">
                  <label>上架</label>
                </div>
              </template>
            </el-table-column>
            <el-table-column label="创建人" prop="createBy" width="100px"></el-table-column>
            <el-table-column label="创建时间" prop="createTime" width="170px"></el-table-column>
            <el-table-column label="操作" width="150px">
              <template slot-scope="scope">
                <el-button
                  size="mini"
                  type="primary"
                  style="padding:10px 7px;"
                  v-show="scope.row.status === 0?true:false"
                  @click="onlineguide(scope.row,1)"
                >上架</el-button>

                <el-button
                  size="mini"
                  type="warning"
                  style="padding:10px 7px;"
                  v-show="scope.row.status === 0?false:true"
                  @click="onlineguide(scope.row,0)"
                >下架</el-button>

                <el-button
                  size="mini"
                  type="danger"
                  style="padding:10px 7px;"
                  v-show="scope.row.isDeleted === 0?true:false"
                  @click="deleteGuide(scope.row)"
                >删除</el-button>

                <el-button
                  size="mini"
                  type="primary"
                  style="padding:10px 7px;"
                  @click="showDetail(scope.row)"
                >详情</el-button>
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
  </main>
</template>

<script>
import { Loading } from "element-ui";
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/activity/installguide";
import { upload } from "@/utils/uploadfile";

import { isNumber } from "util";
export default {
  data() {
    return {
      readonly: true,
      tableLoading: false,
      currentPage: 1,
      loading: false,

      //table页面的条件
      condition: {
        id: undefined,
        categoryId: undefined,
        status: undefined,
        brand: undefined,
        vehicleSeries: undefined,
        paiLiang: undefined,
        nian: undefined,
        salesName: undefined,
        pageIndex: 1,
        pageSize: 10
      },
      categorySel: [],

      statusSel: [
        {
          value: "2",
          label: "-请选择-"
        },
        {
          value: "0",
          label: "下架"
        },
        {
          value: "1",
          label: "上架"
        }
      ],
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

      udpateGuideCondition: {
        type: undefined,
        installIds: []
      },

      udpateGuideConditionInit: {
        type: undefined,
        installIds: []
      },

      tableData: [],
      totalList: 0,

      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",
      shopSel: [],

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
    this.fetchData();
  },
  methods: {
    showDetail(row) {
      this.$router.push({
        path: "updateinstallguide",
        query: { id: row.id }
      });
    },
    onlineguide(row, type) {
      var msg = type == 1 ? "上架" : "下架";
      this.$confirm("确定" + msg + "吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          console.log(
            "formModel: " + JSON.stringify(this.udpateGuideCondition)
          );
          this.udpateGuideCondition.installIds.push(row.id);
          this.udpateGuideCondition.type = type == 1 ? 2 : 3;
          appSvc
            .updateInstallGuideStatus(this.udpateGuideCondition)
            .then(
              res => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  vm.search();

                  this.udpateGuideCondition = JSON.parse(
                    JSON.stringify(this.udpateGuideConditionInit)
                  );
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
        })
        .catch(() => {});
    },
    deleteGuide(row) {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          console.log(
            "formModel: " + JSON.stringify(this.udpateGuideCondition)
          );
          this.udpateGuideCondition.installIds.push(row.id);
          this.udpateGuideCondition.type = 1;
          appSvc
            .updateInstallGuideStatus(this.udpateGuideCondition)
            .then(
              res => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  vm.search();

                  this.udpateGuideCondition = JSON.parse(
                    JSON.stringify(this.udpateGuideConditionInit)
                  );
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
        })
        .catch(() => {});
    },
    //批量发布
    batchpublish() {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          if (this.$refs.multipleTable.selection.length > 0) {
            for (
              var i = 0;
              i < this.$refs.multipleTable.selection.length;
              i++
            ) {
              this.udpateGuideCondition.installIds.push(
                this.$refs.multipleTable.selection[i].id
              );
            }

            var vm = this;
            console.log(
              "formModel: " + JSON.stringify(this.udpateGuideCondition)
            );
            this.udpateGuideCondition.type = 2;
            appSvc
              .updateInstallGuideStatus(this.udpateGuideCondition)
              .then(
                res => {
                  if (res.code == 10000) {
                    this.$message({
                      message: res.message,
                      type: "success"
                    });
                    vm.search();

                    this.udpateGuideCondition = JSON.parse(
                      JSON.stringify(this.udpateGuideConditionInit)
                    );
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
            this.$message({
              message: "请选择需要操作的数据!",
              type: "warning"
            });
          }
        })
        .catch(() => {});
    },
    //批量删除
    batchdelete() {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          if (this.$refs.multipleTable.selection.length > 0) {
            for (
              var i = 0;
              i < this.$refs.multipleTable.selection.length;
              i++
            ) {
              this.udpateGuideCondition.installIds.push(
                this.$refs.multipleTable.selection[i].id
              );
            }

            var vm = this;
            console.log(
              "formModel: " + JSON.stringify(this.udpateGuideCondition)
            );
            this.udpateGuideCondition.type = 1;
            appSvc
              .updateInstallGuideStatus(this.udpateGuideCondition)
              .then(
                res => {
                  if (res.code == 10000) {
                    this.$message({
                      message: res.message,
                      type: "success"
                    });
                    vm.search();

                    this.udpateGuideCondition = JSON.parse(
                      JSON.stringify(this.udpateGuideConditionInit)
                    );
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
            this.$message({
              message: "请选择需要操作的数据!",
              type: "warning"
            });
          }
        })
        .catch(() => {});
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
    showCreate(type, row) {
      this.$router.push({
        path: "addinstallguide"
      });
    },
    // cellStyle(row) {
    //    if (row.row.status === "0" && row.column.label === "状态") {
    //     return "color:red";
    //   }
    // },
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
    changeBrand() {
      //全部清空
      this.vehicleSeriesSel=[];
      this.paiLiangSel=[];
      this.nianSel=[];
      this.salesNameSel=[];
      this.condition.vehicleSeries=undefined;
      this.condition.paiLiang=undefined;
      this.condition.nian = undefined;
      this.condition.salesName=undefined;
      if (this.condition.brand != "") {
        this.brandCondition.brand = this.condition.brand;
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
    changevehicleSeries() {
      
      this.paiLiangSel=[];
      this.nianSel=[];
      this.salesNameSel=[];
      this.condition.paiLiang=undefined;
      this.condition.nian = undefined;
      this.condition.salesName=undefined;
      if (this.condition.vehicleSeries != "") {
        this.vehicleSeriesCondition.vehicleId = this.condition.vehicleSeries;
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
    changepaiLiang() {
      
      this.nianSel=[];
      this.salesNameSel=[];
      
      this.condition.nian = undefined;
      this.condition.salesName=undefined;
      if (this.condition.vehicleSeries != "" && this.condition.paiLiang != "") {
        this.paiLiangCondition.vehicleId = this.condition.vehicleSeries;
        this.paiLiangCondition.paiLiang = this.condition.paiLiang;
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
    changenian() {
     
      this.salesNameSel=[];
      this.condition.salesName=undefined;
      if (
        this.condition.vehicleSeries != "" &&
        this.condition.paiLiang != "" &&
        this.condition.nian != ""
      ) {
        this.nianCondition.vehicleId = this.condition.vehicleSeries;
        this.nianCondition.paiLiang = this.condition.paiLiang;
        this.nianCondition.nian = this.condition.nian;
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

    deleteShopServiceType(row, type) {
      var msg = type == 1 ? "删除" : "恢复";

      this.$confirm("确定" + msg + "吗?", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.deleteCondition.id = row.id;
          this.deleteCondition.isDeleted = type;
          var vm = this;
          appSvc
            .deleteShopServiceType(this.deleteCondition)
            .then(
              res => {
                this.deleteCondition = JSON.parse(
                  JSON.stringify(this.deleteConditionInit)
                );

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

    fetchData() {
      appSvc
        .getInstallGuidePages(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});

      this.getvehiclebrand();
      this.getInstallGuideCategory();
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

    getPaged(flag) {
      this.tableLoading = true;
      if (this.condition.categoryId == "") {
        this.condition.categoryId = undefined;
      }

      if (this.condition.status == "") {
        this.condition.status = undefined;
      }
      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getInstallGuidePages(this.condition)
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
    height: 128px;
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
