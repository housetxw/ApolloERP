<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <header class="bodyTop">
        <article class="filter-container">
          <el-button type="primary" size="small" @click="showCreate()">新增</el-button>
        </article>
      </header>
      <main>
        <section>
          <el-table
            border
            :data="tableData"
            :span-method="cellMerge"
            style="width: 100%"
            :cell-style="cellStyle"
          >
            <el-table-column label="地区" prop="proviceName"></el-table-column>
            <el-table-column label="仓库" prop="warehouseName"></el-table-column>

            <el-table-column label="优先级" prop="priority"></el-table-column>
            <el-table-column label="是否限制" width="90px">
              <template slot-scope="scope">
                <div v-if="scope.row.isLimit === 1">
                  <label>是</label>
                </div>
                <div v-else-if="scope.row.isLimit === 0">
                  <label>否</label>
                </div>
              </template>
            </el-table-column>

            <el-table-column label="操作" width="150px">
              <template slot-scope="scope">
                <el-button
                  size="mini"
                  type="warning"
                  style="padding:10px 7px;"
                  @click="limitPurchaseWarehouseConfig(scope.row)"
                >限制</el-button>
                <el-button
                  size="mini"
                  type="danger"
                  style="padding:10px 7px;"
                  @click="deletePurchaseWarehouseConfig(scope.row)"
                >删除</el-button>
              </template>
            </el-table-column>
          </el-table>
        </section>
      </main>
    </section>
    <!-- 首页 -->

    <!-- 新加配置 -->
    <section id="create">
      <el-dialog
        :title="formTitle"
        :close-on-click-modal="false"
        :visible.sync="formVisible"
        :before-close="cancel"
      >
        <el-form :model="formModel" :inline="true" :rules="rules" ref="formModel">
          <el-form-item label="区域" prop="proviceId">
            <el-select
              v-model="formModel.proviceId"
              size="small"
              clearable
              filterable
              placeholder="请选择省"
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

          <el-form-item label="发货仓" prop="warehouseId">
            <el-select
              v-model="formModel.warehouseId"
              size="small"
              clearable
              filterable
              placeholder="请选择发货仓"
            >
              <el-option
                v-for="item in warehouseSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item label="优先级:" prop="mobile">
            <el-input
              v-model="formModel.priority"
              oninput="value=value.replace(/[^\d.]/g,'')"
              placeholder="请输入优先级"
            />
          </el-form-item>

          <el-form-item label="备注:">
            <el-input type="textarea" :rows="5" v-model="formModel.remark" placeholder="请输入备注" />
          </el-form-item>
        </el-form>

        <div slot="footer" class="dialog-footer">
          <el-button @click="cancel('formModel')">取消</el-button>
          <el-button type="primary" @click="save('formModel')">保存</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 新加配置 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/purchase/purchasewarehouseconfig";

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

      //table页面的条件
      condition: {
        proviceId: ""
      },
      warehouseSelCondition: {
        RequestType: 5
      },
      deleteCondition: {
        id: undefined,
        remark: undefined,
        isDeleted: undefined,
        isLimit: undefined,
        updateType: undefined
      },

      spanArr: [],
      pos: undefined,
      provinceSel: undefined,
      warehouseSel: undefined,
      regionCondition: {
        regionId: undefined
      },
      formModel: {
        id: undefined,
        proviceId: undefined,
        proviceName: undefined,
        warehouseId: undefined,
        warehouseName: undefined,
        priority: undefined,
        remark: undefined
      },

      formModelInit: {
        id: undefined,
        proviceId: undefined,
        proviceName: undefined,
        warehouseId: undefined,
        warehouseName: undefined,
        priority: undefined,
        remark: undefined
      },

      tableData: [],

      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",
      rules: {
        costPrice: [
          { required: true, message: "请输入成本价", trigger: "blur" }
        ],
        purchasePrice: [
          { required: true, message: "请输入采购价", trigger: "blur" }
        ],

        productId: [
          { required: true, message: "请输入产品编号", trigger: "blur" }
        ],
        venderShortName: [
          { required: true, message: "请选择供应商", trigger: "blur" }
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
      if (row.row.isLimit === 1 && row.column.label === "是否限制") {
        return "color:red";
      }
    },

    limitPurchaseWarehouseConfig(row) {
      this.$confirm(
        "确定限制区域【" +
          row.proviceName +
          "】下的仓库【" +
          row.warehouseName +
          "】吗！",
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
          this.deleteCondition.isLimit = 1;
          this.deleteCondition.updateType = 2;
          appSvc
            .deletePurchaseWarehouseConfig(this.deleteCondition)
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
    deletePurchaseWarehouseConfig(row) {
      this.$confirm(
        "确定删除区域【" +
          row.proviceName +
          "】下的仓库【" +
          row.warehouseName +
          "】吗！",
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
          this.deleteCondition.updateType = 1;
          appSvc
            .deletePurchaseWarehouseConfig(this.deleteCondition)
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

    //新增采购信息配置

    showCreate() {
      this.formVisible = true;
      this.formTitle = "新增订单占库配置";
      this.regionCondition.regionId = 0;
      appSvc
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

      appSvc
        .getBasicInfoList(this.warehouseSelCondition)
        .then(
          res => {
            this.warehouseSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    save(formName) {
      if (Number(this.formModel.priority <= 0)) {
        this.$message({
          message: "优先级不合法!",
          type: "warning"
        });
        return;
      }

      this.$confirm("确定操作吗?", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;

          var provinceName = "";
          for (var i = 0; i < this.provinceSel.length; i++) {
            if (this.provinceSel[i].regionId == this.formModel.proviceId) {
              provinceName = this.provinceSel[i].name;
              break;
            }
          }

          this.formModel.proviceName = provinceName;
          var warehouseName = "";
          for (var i = 0; i < this.warehouseSel.length; i++) {
            if (this.warehouseSel[i].key == this.formModel.warehouseId) {
              warehouseName = this.warehouseSel[i].value;
              break;
            }
          }
          this.formModel.warehouseName = warehouseName;
          console.log("formModel: " + JSON.stringify(this.formModel));

          this.$refs[formName].validate(valid => {
            if (valid) {
              appSvc
                .createPurchaseWarehouseConfig(this.formModel)
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
        })
        .catch(() => {});
    },
    cancel(formName) {
      this.formVisible = false;
      // this.resetSendForm();
      this.formModel = JSON.parse(JSON.stringify(this.formModelInit));
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },
    clickRow(row, column, event) {
      console.log(row.id);
    },
    fetchData() {
      appSvc
        .getPurchaseWarehouseConfigPages(this.condition)
        .then(
          res => {
            this.tableData = res.data;
            this.getSpanArr(this.tableData);
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    search() {
      appSvc
        .getPurchaseWarehouseConfigPages(this.condition)
        .then(
          res => {
            this.tableData = res.data;
            this.getSpanArr(this.tableData);
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    cellMerge({ row, column, rowIndex, columnIndex }) {
      if (columnIndex === 0) {
        const _row = this.spanArr[rowIndex];
        const _col = _row > 0 ? 1 : 0;
        return {
          rowspan: _row,
          colspan: _col
        };
      }
    },
    getSpanArr(data) {
      this.spanArr = [];
      this.pos = undefined;
      for (var i = 0; i < data.length; i++) {
        if (i === 0) {
          this.spanArr.push(1);
          this.pos = 0;
        } else {
          // 判断当前元素与上一个元素是否相同
          if (data[i].proviceId === data[i - 1].proviceId) {
            this.spanArr[this.pos] += 1;
            this.spanArr.push(0);
          } else {
            this.spanArr.push(1);
            this.pos = i;
          }
        }
      }
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
