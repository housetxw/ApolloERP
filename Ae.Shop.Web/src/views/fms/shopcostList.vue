<template>
  <main class="container">
    <!-- 首页：列表数据 -->
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :headerBtnWidth="160"
      :tableData="tableData"
      :pageChange="pageChange"
      :searching="search"
      :conditionModel="condition"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item prop="costType">
          <el-select v-model="condition.costType" placeholder="费用类别" clearable style="width:160px">
            <el-option
              v-for="item in costTypes"
              :key="item.code"
              :label="item.name"
              :value="item.code"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-tooltip class="item" effect="dark" content="支付日期" placement="bottom">
            <el-date-picker             
              @change="handleCostDateChange"
              v-model="condition.costDates"
              type="daterange"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              format="yyyy-MM-dd "
              value-format="yyyy-MM-dd"
              style="width:220px"
              :picker-options="$root.$data.timeRgPickerOpt"
            ></el-date-picker>
          </el-tooltip>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.payChannel"
            style="width:200px;"
            placeholder="支付渠道"
          >
            <el-option
              v-for="item in payChannels"
              :key="item.name"
              :label="item.name"
              :value="item.name"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.isDeleted"
            style="width:90px;"
            placeholder="状态"
          >
            <el-option
              v-for="item in isDeleteds"
              :key="item.code"
              :label="item.name"
              :value="item.code"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add">新增</el-button>
      </template>
      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <el-table-column label="主键" prop="id" align="center" v-if="false"></el-table-column>
        <rg-table-column label="费用类别" prop="costTypeLabel" />
        <rg-table-column label="金额" prop="money" is-money />
        <rg-table-column label="费用备注" prop="remark" />
        <rg-table-column label="支付日期" prop="date" is-date />
        <rg-table-column label="支付渠道" prop="payChannel" />
        <rg-table-column label="创建人" prop="createBy" />
        <rg-table-column label="创建时间" prop="createTime" is-datetime />
        <rg-table-column label="状态" prop="isDeletedLabel" />
        <el-table-column fixed="right" align="center" label="操作" :width="90">
          <template slot-scope="scope">
            <el-button-group>
              <el-button @click="routerToDetail(scope.row.id)" type="primary" size="mini">编辑</el-button>
              <el-button @click="deleteCost(scope.row.id)" type="danger" size="mini">删除</el-button>
            </el-button-group>
          </template>
        </el-table-column>
        <!-- Table数据 -->
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

    <!-- 新增和编辑页面 -->
    <section id="detailOrEdit">
      <rg-dialog
        :title="formTitle"
        :loading="formLoading"
        :visible.sync="formVisible"
        :beforeClose="cancel"
        :btnCancel="{label:'关闭',click:(done)=>{cancel('formModel')}}"
        :btnRemove="false"
        :footbar="true"
        v-if="formVisible"
        width="650px"
      >
        <template v-slot:content>
          <el-form
            :model="formModel"
            :rules="rules"
            ref="formModel"
            size="small"
            :label-width="formLabelWidth"
            :inline-message="true"
          >
                <el-form-item label="费用类别" prop="costType">
                  <el-select
                    v-model="formModel.costType"
                    :disabled="formCheck"
                    placeholder="请选择"
                    style="width:420px"
                  >
                    <el-option
                      v-for="item in costTypes"
                      :key="item.code"
                      :label="item.name"
                      :value="item.code"
                    />
                  </el-select>
                </el-form-item>
                <el-form-item label="支付日期" prop="date">
                  <el-date-picker
                    v-model="formModel.date"
                    type="date"
                    placeholder="年/月/日"
                    format="yyyy-MM-dd"
                    value-format="yyyy-MM-dd"
                    clearable
                    :disabled="formCheck"
                  />
                </el-form-item>
                <el-form-item label="费用金额" prop="money">
                  <el-input
                    v-model="formModel.money"
                    type="number"
                    step="0.01"
                    style="width:420px"
                    autocomplete="off"
                    clearable
                    :disabled="formCheck"
                    placeholder="请填写费用金额"
                  />
                </el-form-item>
                <el-form-item label="支付渠道" prop="payChannel">
                  <el-select
                    v-model="formModel.payChannel"
                    :disabled="formCheck"
                    placeholder="请选择"
                    style="width:420px"
                  >
                    <el-option
                      v-for="item in payChannels"
                      :key="item.name"
                      :label="item.name"
                      :value="item.name"
                    />
                  </el-select>
                </el-form-item>
                <el-form-item label="发票" prop="invoiceType">
                  <el-select
                    v-model="formModel.invoiceType"
                    :disabled="formCheck"
                    placeholder="请选择"
                    style="width:420px"
                  >
                    <el-option
                      v-for="item in invoiceTypes"
                      :key="item.code"
                      :label="item.name"
                      :value="item.code"
                    />
                  </el-select>
                </el-form-item>
                <el-form-item label="费用说明" prop="remark">
                  <el-input
                    v-model="formModel.remark"
                    type="textarea"
                    style="width:420px;"
                    autocomplete="off"
                    clearable
                    maxlength="300"
                    rows="5"
                    show-word-limit
                    :disabled="formCheck"
                    placeholder="请填写费用说明"
                  />
                </el-form-item>
          </el-form>
        </template>

        <template v-slot:footer>
          <el-button-group>
            <el-button
              icon="el-icon-check"
              size="mini"
              type="primary"
              :loading="btnSaveLoading"
              :style="{ display: btnSaveVisible }"
              @click="save('formModel')"
            >保存</el-button>
          </el-button-group>
        </template>
      </rg-dialog>
    </section>
    <!-- 新增和编辑页面 -->
  </main>
</template>

<script>
import { shopcostSvc } from "@/api/fms/shopcost";
export default {
  name: "shopcostList",
  data() {
    return {
      costTypes: "",
      isDeleteds: [],
      tableLoading: false,
      btnSaveVisible: "",
      isAdd: true,
      formLabelWidth: "100px",
      condition: {
        costType: "",
        costDates: undefined,
        startDate: undefined,
        endDate: undefined,
        payChannel: "",
        isDeleted: 0,
        pageIndex: 1,
        pageSize: 20
      },

      tableData: [],
      totalList: 0,
      isDeleteds: [
        {
          code: 0,
          name: "正常"
        },
        {
          code: 1,
          name: "已删除"
        }
      ],
      invoiceTypes: [
        {
          code: 0,
          name: "无发票"
        },
        {
          code: 1,
          name: "普通发票"
        },
        {
          code: 2,
          name: "增值税普通发票"
        },
        {
          code: 3,
          name: "增值税专用发票"
        }
      ],
      payChannels: [
        {
          name: "金华总部"
        },
        {
          name: "上海总部"
        },
        {
          name: "黄山总部"
        },
        {
          name: "总部备用金"
        },
        {
          name: "门店备用金"
        },
        {
          name: "内部结算"
        },
        {
          name: "其他"
        }
      ],
      formVisible: false,
      formTitle: "详情",
      formCheck: false,
      formLoading: false,
      formInit: {
        id: 0,
        costType: "",
        payChannel: "",
        money: 0,
        invoiceType: 0,
        date: "",
        remark: "",
        isDeleted: 0
      },
      btnSaveLoading: false,
      formModel: {
        id: 0,
        costType: "",
        payChannel: "",
        money: 0,
        invoiceType: 0,
        date: "",
        remark: "",
        isDeleted: 0
      },
      rules: {
        costType: [
          { required: true, message: "请选择费用类别", trigger: "blur" }
        ],
        money: [{ required: true, message: "请输入费用金额", trigger: "blur" }],
        date: [{ required: true, message: "请选择支付日期", trigger: "blur" }],
        remark: [{ required: true, message: "请输入费用说明", trigger: "blur" }]
      }
    };
  },
  created() {
    this.getCondition();
    this.initdate();
    this.fetchData();
  },

  watch: {},
  methods: {
    getDate() {
      let nowDate = new Date();
      let date = {
        year: nowDate.getFullYear(),
        month: nowDate.getMonth() + 1,
        date: nowDate.getDate()
      };
      let systemDate = date.year + "/" + date.month + "/" + date.date;
      return systemDate;
    },
    handleCostDateChange() {
      if (this.condition.costDates == undefined) {
        this.condition.startDate = undefined;
        this.condition.endDate = undefined;
      } else {
        this.condition.startDate = this.condition.costDates[0];
        this.condition.endDate = this.condition.costDates[1];
      }
    },
    initdate() {
      //this.condition.costDates = [this.getDate(), this.getDate()];
      //this.condition.startDate = this.condition.costDates[0];
      //this.condition.endDate = this.condition.costDates[1];
    },
    //处理开始使用日期改变
    fetchData() {
      this.getPaged();
    },
    search(flag) {
      this.condition.pageIndex = this.currerrentPage = 1;
      this.getPaged(flag);
    },
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    getPaged(flag) {
      this.tableLoading = true;
      shopcostSvc
        .getShopCostList(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (flag) {
              this.$messageSuccess(res.message || "查询成功");
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });
    },
    getCondition() {
      shopcostSvc
        .getShopCostTypeListCondition()
        .then(
          res => {
            var data = res.data;
            this.costTypes = data.typeList;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    add() {
      this.formTitle = "新增";
      this.formVisible = true;
      this.formCheck = false;
      this.btnSaveVisible = "";
      this.isAdd = true;
    },
    deleteCost(id) {
      var vm = this;
      this.$confirm("确定删除吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var costRequest = {
            id: id
          };
          shopcostSvc
            .deleteShopCost(costRequest)
            .then(
              res => {
                if (res.code == 10000) {
                  this.$messageSuccess(res.message);
                  vm.search();
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

    routerToDetail(id) {
      this.formVisible = true;
      this.formLoading = true;
      this.isAdd = false;
      var costRequest = {
        id: id
      };
      shopcostSvc
        .getShopCostDetail(costRequest)
        .then(
          res => {
            var data = res.data;
            if (data == null) {
              this.$message({
                message: "系统异常，请关闭重新打开",
                type: "error"
              });
            } else {
              this.formModel = data;
              //按钮控制
              this.btnSaveVisible = "";
              if (this.formModel.isDeleted == 1) {
                this.btnSaveVisible = "none";
                this.formCheck = true;
              } else {
                this.btnSaveVisible = "";
                this.formCheck = false;
              }
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.formLoading = false;
        });
    },

    save(formName) {
      var vm = this;
      console.log("save formModel: " + JSON.stringify(this.formModel));
      this.$refs[formName].validate(valid => {
        if (valid) {
          vm.btnSaveLoading = true;
          if (this.isAdd) {
            shopcostSvc
              .addShopCost(this.formModel)
              .then(
                res => {
                  if (res.code == 10000) {
                    this.cancel("formModel");
                    this.$messageSuccess(res.message);
                    this.getPaged();
                  }
                },
                err => {
                  console.log(err);
                }
              )
              .catch(err => {
                console.error(err);
              })
              .finally(() => {
                vm.btnSaveLoading = false;
              });
          } else {
            shopcostSvc
              .updateShopCost(this.formModel)
              .then(
                res => {
                  if (res.code == 10000) {
                    this.cancel("formModel");
                    this.$message({
                      message: res.message,
                      type: "success"
                    });
                    this.getPaged();
                  }
                },
                err => {
                  console.log(err);
                }
              )
              .catch(err => {
                console.error(err);
              })
              .finally(() => {
                vm.btnSaveLoading = false;
              });
          }
        } else {
          return false;
        }
      });
    },
    cancel(formName) {
      this.formVisible = false;
      this.formCheck = false;

      this.resetForm();

      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },
    resetForm() {
      this.formModel = JSON.parse(JSON.stringify(this.formInit));
    }
  }
};
</script>