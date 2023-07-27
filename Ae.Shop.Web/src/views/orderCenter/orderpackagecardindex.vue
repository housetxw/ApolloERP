<template>
  <main class="container">
    <!-- 首页 -->
    <section>
      <rg-page
        background
        id="indexPage"
        :pageIndex="condition.pageIndex"
        :pageSize="condition.pageSize"
        :dataCount="totalList"
        :tableLoading="tableLoading"
        :tableData="tableData"
        :pageChange="pageChange"
        :headerBtnWidth="180"
        :searching="search"
      >
        <template v-slot:condition>
          <el-form-item>
            <el-input
              class="input"
              clearable
              placeholder="请输入手机号"
              prefix-icon="el-icon-search"
              v-model="condition.userPhone"
            ></el-input>
          </el-form-item>

          <el-form-item>
            <el-input
              class="input"
              clearable
              placeholder="请输入订单号"
              prefix-icon="el-icon-search"
              v-model="condition.sourceOrderNo"
            ></el-input>
          </el-form-item>

          <el-form-item>
            <el-input
              class="input"
              clearable
              placeholder="请输入核销码"
              prefix-icon="el-icon-search"
              v-model="condition.code"
            ></el-input>
          </el-form-item>
          <!-- <el-form-item>
            <el-select
              v-model="condition.status"
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
          </el-form-item> -->
        </template>
        <template v-slot:tb_cols>
          <el-table-column
            label="手机号"
            prop="userPhone"
            align="center"
          ></el-table-column>
          <el-table-column
            label="订单号"
            prop="sourceOrderNo"
            align="center"
          ></el-table-column>
<!-- 
          <el-table-column
            label="渠道"
            prop="channel"
            align="center"
          ></el-table-column> -->
          <rg-table-column label="核销码" prop="code" />
          <rg-table-column label="产品PID" prop="productId" />
          <rg-table-column label="产品名称" prop="productName" />
          <rg-table-column label="状态" prop="status" />
          <rg-table-column label="起始时间" prop="startValidTime" />
          <rg-table-column
            label="截止时间"
            prop="endValidTime"
            align="center"
          />
          <rg-table-column label="核销订单号" prop="verifyOrderNo" />
          <rg-table-column label="核销门店" prop="verifyShopName" />
          <rg-table-column label="核销时间">
            <template slot-scope="scope">
              <p
                v-if="
                  Date.parse(scope.row.verifyTime) > Date.parse('1900-01-01')
                "
              >
                {{ scope.row.verifyTime }}
              </p>
            </template>
          </rg-table-column>
          <rg-table-column label="备注" prop="remark" />
          <rg-table-column
            prop="name"
            label="操作"
            fixed="right"
            width="120"
            align="center"
          >
            <template slot-scope="scope">
              <el-button-group>
                <el-button
                  size="mini"
                  type="warning"
                  @click="detail(scope.row)"
                  :disabled="scope.row.status != '已使用' ? false : true"
                  >修改</el-button
                >
                <!-- <el-button
                  size="mini"
                  type="warning"
                  @click="recoverData(scope.row)"
                  :disabled="scope.row.status == '已使用' ? false : true"
                  >恢复</el-button
                > -->
              </el-button-group>
            </template>
          </rg-table-column>
        </template>
      </rg-page>

      <rg-dialog
        :title="formTitle"
        :visible.sync="dialogVisible"
        :beforeClose="cancel"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            cancel();
          },
        }"
        :btnRemove="false"
        :footbar="true"
        width="80%"
        maxWidth="600px"
        minWidth="600px"
      >
        <template v-slot:content>
          <el-form :model="currentDetail" size="mini" label-width="120px">
            <el-form-item label="订单号">
              <label>{{currentDetail.sourceOrderNo}}</label>             
            </el-form-item>
            <el-form-item label="核销码">
              <label>{{currentDetail.code}}</label>               
            </el-form-item>
            <el-form-item label="起始时间">
              <el-input
                style="width: 300px"
                disabled
                v-model="currentDetail.startValidTime"
              ></el-input>
            </el-form-item>
            <el-form-item label="截止时间">
              <el-date-picker
                type="date"
                style="width: 135px"
                placeholder="截止时间"
                v-model="currentDetail.endValidTime"
              ></el-date-picker>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            :loading="loading"
            @click="submitData()"
            >提交</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 首页 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";

import { appSvc } from "@/api/orderCenter/orderpackagecard";
import { isNumber } from "util";
export default {
  data() {
    return {
      pickerOptionsStart: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        },
      },
      input: undefined,

      readonly: true,
      tableLoading: false,
      currentPage: 1,

      //table页面的条件
      condition: {
        pageIndex: 1,
        pageSize: 20,
        userPhone: undefined,
        sourceOrderNo: undefined,
        productName: undefined,
        status: undefined,
        code: undefined,
        shopId: 1,
      },

      statusSel: [
        {
          key: "0",
          value: "未使用",
        },
        {
          key: "1",
          value: "已使用",
        },
        {
          key: "2",
          value: "已过期",
        },
      ],

      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      // formTitle: "新增",

      formLabelWidth: "120px",

      formTitle: "修改核销码信息",
      dialogVisible: false,
      currentDetail: {
        sourceOrderNo: "",
        code: "",
        startValidTime: "",
        endValidTime: "",
      },
      loading: false,
    };
  },
  computed: {
    routeComputed: {
      get: function () {
        return this.formModel.route;
      },
      set: function (val) {
        this.formModel.route = val.toLowerCase();
      },
    },
  },
  created() {
    //页面初始化函数
    //this.fetchData();
  },
  methods: {
    clickRow(row, column, event) {
      console.log(row.id);
    },

    fetchData() {
      appSvc
        .getOrderPackageCards(this.condition)
        .then(
          (res) => {
            //  this.tableData = res.data;
            var data = res.data;

            this.tableData = data.items;
            this.totalList = data.totalItems;
          },
          (error) => {
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
    pageTurning() {
      this.condition.pageIndex = this.currentPage;
      this.getPaged();
    },

    getPaged(flag) {
      console.log("condition: " + JSON.stringify(this.condition));
    
        this.tableLoading = true;

        appSvc
          .getOrderPackageCards(this.condition)
          .then(
            (res) => {
              var data = res.data;
              this.tableData = data.items;
              this.totalList = data.totalItems;

              if (flag) {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                }
              }
            },
            (error) => {
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
    },
    cancel() {
      this.dialogVisible = false;
    },
    detail(value) {
      this.dialogVisible = true;
      this.currentDetail.sourceOrderNo = value.sourceOrderNo;
      this.currentDetail.code = value.code;
      this.currentDetail.startValidTime = value.startValidTime;
      this.currentDetail.endValidTime = value.endValidTime;
      // this.currentDetail.shopIds = value.shopIds;
      // this.currentDetail.packageCardId = value.packageCardId;
      // this.currentDetail.productCode = value.productCode;
      // this.currentDetail.name = value.name;
      // this.currentDetail.id = value.id;
      // this.currentDetail.salesPrice = value.salesPrice;
      // debugger;
      // if (value.onSale == 1) {
      //   this.currentDetail.onSaleName = "上架";
      // } else if (value.status == 0) {
      //   this.currentDetail.onSaleName = "下架";

      //   //  this.currentDetail.statusName = value.status;
      //   this.productSel.push({
      //     key: value.productCode,
      //     value: value.name,
      //     productCode: value.productCode,
      //     salesPrice: value.salesPrice,
      //     onSale: value.onSale,
      //   });
      // }
    },
    submitData() {
      this.loading = true;
      appSvc
        .UpdateOrderPackage({
          code: this.currentDetail.code,
          sourceOrderNo: this.currentDetail.sourceOrderNo,
          validateEnd: this.currentDetail.endValidTime,
        })
        .then(
          (res) => {
            var data = res.data;
            this.$message({ message: "提交成功", type: "success" });
            this.dialogVisible = false;
            this.getPaged();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.loading = false;
        });
    },
    recoverData(value) {
      
      this.$confirm(value.code+",确定要恢复该核销码？", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
            
          this.loading = true;
          appSvc
            .UpdateOrderPackage({
              code: value.code,
              sourceOrderNo: value.sourceOrderNo,
              validateEnd: value.endValidTime,
            })
            .then(
              (res) => {
                var data = res.data;
                this.$message({ message: "恢复成功", type: "success" });
          
                this.getPaged();
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally(() => {
              this.loading = false;
            });
        }); 
    },
  },
};
</script>
<style lang="scss"></style>
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
>>> .el-dialog__wrapper .rg-dialog .dialog_body {
  padding: 20px 40px;
}
>>> .header-search,
.search-line {
  padding-bottom: 10px;
}
</style>
