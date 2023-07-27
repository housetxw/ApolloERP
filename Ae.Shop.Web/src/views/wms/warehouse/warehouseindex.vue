<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <header class="bodyTop">
        <article class="filter-container">
          <span class="input-label">类型:</span>
          <el-select
            v-model="condition.warehouseType"
            size="small"
            clearable
            filterable
            placeholder="请选择类型"
            class="margin-right-10"
          >
            <el-option
              v-for="item in typeSel"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>

          <span class="input-label">状态:</span>
          <el-select
            v-model="condition.status"
            size="small"
            clearable
            filterable
            placeholder="请选择状态"
            class="margin-right-10"
          >
            <el-option
              v-for="item in statusSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>

          <el-button
            type="primary"
            size="small"
            style="margin-left:30px;"
            @click="search(true)"
            icon="el-icon-search"
          >搜索</el-button>
          <el-button type="primary" size="small" @click="showCreate()">新增</el-button>
        </article>
      </header>
      <main>
        <section>
          <el-table border :data="tableData" style="width: 100%" :cell-style="cellStyle">
            <el-table-column label="仓库名称" prop="warehouseName"></el-table-column>
            <el-table-column label="仓库类型" prop="warehouseType"></el-table-column>

            <el-table-column label="区域" prop="areaName"></el-table-column>
            <el-table-column label="负责人" prop="contacter" width="90px"></el-table-column>
            <el-table-column label="状态" prop="status" width="90px"></el-table-column>
            <el-table-column label="创建人" prop="createBy" width="120px"></el-table-column>
            <el-table-column label="创建时间" prop="createTime"></el-table-column>
            <el-table-column label="操作" width="150px">
              <template slot-scope="scope">
                <el-button
                  size="mini"
                  type="primary"
                  style="padding:10px 7px;"
                  @click="editWarehouse(scope.row)"
                >编辑</el-button>
                <el-button
                  size="mini"
                  type="danger"
                  style="padding:10px 7px;"
                  @click="deleteWarehouse(scope.row)"
                >删除</el-button>
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

    <section id="create">
      <el-dialog
        :title="formTitle"
        :close-on-click-modal="false"
        :visible.sync="formVisible"
        :before-close="cancel"
      >
        <el-form :model="formModel" :inline="true" :rules="rules" ref="formModel">
          <el-form-item label="仓库名称:" prop="warehouseName">
            <el-input v-model="formModel.warehouseName" placeholder="请输入仓库名称" />
          </el-form-item>

          <el-form-item label="仓库类型" prop="warehouseType">
            <el-select
              v-model="formModel.warehouseType"
              size="small"
              clearable
              filterable
              placeholder="请选择仓库类型"
            >
              <el-option
                v-for="item in typeSel"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="仓管负责人:" prop="contacter">
            <el-input v-model="formModel.contacter" placeholder="请输入仓管负责人" />
          </el-form-item>

          <el-form-item label="电话:" prop="mobile">
            <el-input v-model="formModel.mobile" placeholder="请输入电话" />
          </el-form-item>

          <el-form-item label="手机号:" prop="telephone">
            <el-input v-model="formModel.telephone" placeholder="请输入手机号" />
          </el-form-item>

          <el-form-item label="省" prop="provinceId">
            <el-select
              v-model="formModel.provinceId"
              size="small"
              clearable
              filterable
              placeholder="请选择省"
              @change="getCity"
              class="margin-right-10"
            >
              <el-option
                v-for="item in provinceSel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="市" prop="cityId">
            <el-select
              v-model="formModel.cityId"
              size="small"
              clearable
              filterable
              @change="getDistrict"
              placeholder="请选择市"
              class="margin-right-10"
            >
              <el-option
                v-for="item in citySel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="区" prop="districtId">
            <el-select
              v-model="formModel.districtId"
              size="small"
              clearable
              filterable
              placeholder="请选择区"
              class="margin-right-10"
            >
              <el-option
                v-for="item in districtSel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="详细地址:" prop="address">
            <el-input v-model="formModel.address" style="width:400px;" placeholder="请输入详细地址" />
          </el-form-item>
        </el-form>

        <div slot="footer" class="dialog-footer">
          <el-button @click="cancel('formModel')">取消</el-button>
          <el-button type="primary" @click="save('formModel')">保存</el-button>
        </div>
      </el-dialog>
    </section>

    <section id="edit">
      <el-dialog
        :title="editformTitle"
        :close-on-click-modal="false"
        :visible.sync="editformVisible"
        :before-close="editcancel"
      >
        <el-form :model="formModel" :inline="true" :rules="rules" ref="formModel">
          <el-form-item label="仓库名称:" prop="warehouseName">
            <el-input v-model="formModel.warehouseName" :disabled="true" placeholder="请输入仓库名称" />
          </el-form-item>

          <el-form-item label="仓库类型" prop="warehouseType">
            <el-select
              v-model="formModel.warehouseType"
              size="small"
              clearable
              filterable
              placeholder="请选择仓库类型"
            >
              <el-option
                v-for="item in typeSel"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="仓管负责人:" prop="contacter">
            <el-input v-model="formModel.contacter" placeholder="请输入仓管负责人" />
          </el-form-item>

          <el-form-item label="电话:" prop="mobile">
            <el-input v-model="formModel.mobile" placeholder="请输入电话" />
          </el-form-item>

          <el-form-item label="手机号:" prop="telephone">
            <el-input v-model="formModel.telephone" placeholder="请输入手机号" />
          </el-form-item>

          <el-form-item label="省" prop="provinceId">
            <el-select
              v-model="formModel.provinceName"
              size="small"
              clearable
              filterable
              placeholder="请选择省"
              @change="getCity"
              class="margin-right-10"
            >
              <el-option
                v-for="item in provinceSel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="市" prop="cityId">
            <el-select
              v-model="formModel.cityName"
              size="small"
              clearable
              filterable
              @change="getDistrict"
              placeholder="请选择市"
              class="margin-right-10"
            >
              <el-option
                v-for="item in citySel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="区" prop="districtId">
            <el-select
              v-model="formModel.districtName"
              size="small"
              clearable
              filterable
              placeholder="请选择区"
              class="margin-right-10"
            >
              <el-option
                v-for="item in districtSel"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="详细地址:" prop="address">
            <el-input v-model="formModel.address" style="width:400px;" placeholder="请输入详细地址" />
          </el-form-item>
        </el-form>

        <div slot="footer" class="dialog-footer">
          <el-button @click="editcancel('formModel')">取消</el-button>
          <el-button type="primary" @click="editsave('formModel')">保存</el-button>
        </div>
      </el-dialog>
    </section>

    <!-- 删除仓库页面 -->
    <section id="deletePurchaseOrder">
      <el-dialog
        width="500px"
        :title="deleteFormTitle"
        :close-on-click-modal="false"
        :visible.sync="deleteFormVisible"
        :before-close="deleteCancel"
      >
        <el-form :model="deleteFormModel" ref="deleteFormModel">
          <el-form-item label="理由:" size="50">
            <el-input
              type="textarea"
              :rows="5"
              placeholder="请输入理由"
              v-model="deleteFormModel.remark"
            ></el-input>
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="deleteCancel('deleteFormModel')">取消</el-button>
          <el-button type="warning" @click="deleteSave('deleteFormModel')">确定</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 删除仓库页面 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appWarehouseSvc } from "@/api/wms/warehouse";
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

      statusSelCondition: {
        RequestType: 1
      },
      provinceSel: [],

      citySel: [],
      districtSel: [],
      regionCondition: {
        regionId: undefined
      },
      //table页面的条件
      condition: {
        pageIndex: 1,
        pageSize: 10,
        warehouseType: undefined,
        statstatusus: undefined
      },

      deleteFormModel: {
        id: undefined,
        remark: undefined
      },

      deleteFormModelInit: {
        id: undefined,
        remark: undefined
      },

      deleteFormTitle: undefined,
      deleteFormVisible: false,

      editformTitle: undefined,
      editformVisible: undefined,
      statusSel: [],
      typeSel: [
        {
          value: "轮胎",
          label: "轮胎"
        },
        {
          value: "保养",
          label: "保养"
        },
        {
          value: "车品",
          label: "车品"
        }
      ],
      formModel: {
        id: undefined,
        warehouseName: undefined,
        warehouseType: undefined,
        contacter: undefined,
        mobile: undefined,
        provinceId: undefined,
        provinceName: undefined,
        cityId: undefined,
        cityName: undefined,
        districtId: undefined,
        districtName: undefined,
        address: undefined,
        orginprovinceName: undefined,
        orgincityName: undefined,
        orgindistrictName: undefined
      },

      formModelInit: {
        id: undefined,
        warehouseName: undefined,
        warehouseType: undefined,
        contacter: undefined,
        mobile: undefined,
        provinceId: undefined,
        provinceName: undefined,
        cityId: undefined,
        cityName: undefined,
        districtId: undefined,
        districtName: undefined,
        address: undefined,
        orginprovinceName: undefined,
        orgincityName: undefined,
        orgindistrictName: undefined
      },
      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",
      rules: {
        warehouseName: [
          { required: true, message: "请输入仓库名称", trigger: "blur" }
        ],
        warehouseType: [
          { required: true, message: "请选择仓库类型", trigger: "blur" }
        ],

        contacter: [
          { required: true, message: "请输入仓管姓名", trigger: "blur" }
        ],
        telephone: [
          { required: true, message: "请输入仓管手机号", trigger: "blur" }
        ],
        provinceId: [{ required: true, message: "请选择省", trigger: "blur" }],
        cityId: [{ required: true, message: "请选择市", trigger: "blur" }],
        districtId: [{ required: true, message: "请选择区", trigger: "blur" }],
        address: [
          { required: true, message: "请输入详细地址", trigger: "blur" }
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
    getCity() {
      debugger;
      if (
        this.formModel.provinceId != "" &&
        this.formModel.provinceId != undefined
      ) {
        this.regionCondition.regionId = this.formModel.provinceId;
        appWarehouseSvc
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
      } else {
        this.formModel.cityId = undefined;
        this.formModel.districtId = undefined;
        this.citySel=[];
        this.districtSel=[];
      }
    },

    getDistrict() {
      if (this.formModel.cityId != "" && this.formModel.cityId != undefined) {
        this.regionCondition.regionId = this.formModel.cityId;

        appWarehouseSvc
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
      } else {
        this.formModel.districtId = undefined;
        this.districtSel=[];
      }
    },

    cellStyle(row, column, rowIndex, columnIndex) {
      //根据报警级别显示颜色
      // console.log(row);
      // console.log(row.column);
      if (row.row.taskStatus === "已驳回" && row.column.label === "状态") {
        return "color:red";
      }

      if (row.row.productStatus === "已驳回" && row.column.label === "状态") {
        return "color:red";
      }
      // else if(row.column.label==="告警级别"&& row.row.alarmLevel==="一般告警" ){
      //   return 'color:yellow'
      // }
    },

    //新增采购信息配置

    showCreate() {
      this.formVisible = true;
      this.formTitle = "新增仓库配置";
      this.regionCondition.regionId = 0;
      appWarehouseSvc
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
    save(formName) {
      var vm = this;

      this.$refs[formName].validate(valid => {
        if (valid) {
          var provinceName = "";
          for (var i = 0; i < this.provinceSel.length; i++) {
            if (this.provinceSel[i].regionId == this.formModel.provinceId) {
              provinceName = this.provinceSel[i].name;
              break;
            }
          }

          var cityName = "";
          for (var i = 0; i < this.citySel.length; i++) {
            if (this.citySel[i].regionId == this.formModel.cityId) {
              cityName = this.citySel[i].name;
              break;
            }
          }

          var districtName = "";
          for (var i = 0; i < this.districtSel.length; i++) {
            if (this.districtSel[i].regionId == this.formModel.districtId) {
              districtName = this.districtSel[i].name;
              break;
            }
          }

          this.formModel.provinceName = provinceName;
          this.formModel.cityName = cityName;
          this.formModel.districtName = districtName;

          appWarehouseSvc
            .createWarehouse(this.formModel)
            .then(res => {
              if (res.code == 10000) {
                this.$message({
                  message: res.message,
                  type: "success"
                });
                vm.search();
                this.formVisible = false;
                vm.cancel();
              }
            })
            .catch(error => {
              console.log(error);
            });
        } else {
          return false;
        }
      });
    },
    cancel(formName) {
      this.formVisible = false;
      // this.resetSendForm();
      this.formModel = JSON.parse(JSON.stringify(this.formModelInit));
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },
    //新增采购信息配置

    // 编辑采购信息配置
    editWarehouse(row) {
      this.editformTitle = "编辑【" + row.warehouseName + "】信息";
      this.editformVisible = true;

      this.deleteFormModel.id = row.id;

      appWarehouseSvc
        .getWarehouseConfig(this.deleteFormModel)
        .then(
          res => {
            //  this.tableData = res.data;
            this.formModel = res.data;
            this.formModel.orginprovinceName = this.formModel.provinceName;
            this.formModel.orgincityName = this.formModel.cityName;
            this.formModel.orgindistrictName = this.formModel.districtName;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    editcancel(formName) {
      this.editformVisible = false;
      // this.resetSendForm();
      this.formModel = JSON.parse(JSON.stringify(this.formModelInit));
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },
    editsave(formName) {
      this.$confirm("确定保存吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;

          if (this.formModel.provinceName != this.formModel.orginprovinceName) {
            var provinceId = this.formModel.provinceName;
            var provinceName = "";
            for (var i = 0; i < this.provinceSel.length; i++) {
              if (this.provinceSel[i].regionId == provinceId) {
                provinceName = this.provinceSel[i].name;
                break;
              }
            }
            this.formModel.provinceId = provinceId;
            this.formModel.provinceName = provinceName;
          }

          if (this.formModel.cityName != this.formModel.orgincityName) {
            var cityId = this.formModel.cityName;
            var cityName = "";
            for (var i = 0; i < this.citySel.length; i++) {
              if (this.citySel[i].regionId == cityId) {
                cityName = this.citySel[i].name;
                break;
              }
            }
            this.formModel.cityId = cityId;
            this.formModel.cityName = cityName;
          }

          if (this.formModel.districtName != this.formModel.orgindistrictName) {
            var districtId = this.formModel.districtName;
            var districtName = "";
            for (var i = 0; i < this.districtSel.length; i++) {
              if (this.districtSel[i].regionId == districtId) {
                districtName = this.districtSel[i].name;
                break;
              }
            }
            this.formModel.districtId = districtId;
            this.formModel.districtName = districtName;
          }

          console.log("condition: " + JSON.stringify(this.formModel));
          appWarehouseSvc
            .editWarehouseConfig(this.formModel)
            .then(
              res => {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                }
                vm.search();

                this.editformVisible = false;
                // this.resetSendForm();
                this.formModel = JSON.parse(JSON.stringify(this.formModelInit));

                vm.cancel();
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
        })
        .catch(() => {});
    },
    // 删除仓库
    deleteWarehouse(row) {
      this.deleteFormModel.id = row.id;
      this.deleteFormVisible = true;
      this.deleteFormTitle = "删除仓库【" + row.warehouseName + "】";
    },

    deleteCancel(formModel) {
      this.deleteFormVisible = false;
      // this.resetSendForm();
      this.deleteFormModel = JSON.parse(
        JSON.stringify(this.deleteFormModelInit)
      );
      var frmName =
        typeof formName === "string" && formName ? formName : "deleteFormModel";
      this.$refs[frmName].resetFields();
    },

    deleteSave(formModel) {
      this.$confirm("确定删除吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;

          appWarehouseSvc
            .deleteWarehouseConfig(this.deleteFormModel)
            .then(
              res => {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                }

                vm.search();

                this.deleteFormVisible = false;
                this.deleteFormModel = JSON.parse(
                  JSON.stringify(this.deleteFormModelInit)
                );

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

    clickRow(row, column, event) {
      console.log(row.id);
      // console.log(column)
      // console.log(event)
    },

    fetchData() {
      appWarehouseSvc
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

      appWarehouseSvc
        .getWarehousePages(this.condition)
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

      console.log("condition: " + JSON.stringify(this.condition));
      appWarehouseSvc
        .getWarehousePages(this.condition)
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
