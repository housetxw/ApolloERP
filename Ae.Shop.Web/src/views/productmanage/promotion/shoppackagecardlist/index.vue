<template>
  <div class="container">
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
      :searching="searchPromotionActivity"
      :defaultCollapse="false"
    >
      <template v-slot:condition>
        <el-form-item prop="key">
          <el-input
            class="input"
            clearable
            placeholder="请输入套餐卡名称或pid"
            prefix-icon="el-icon-search"
            v-model="condition.key"
          ></el-input>
        </el-form-item>
        <!-- <el-form-item prop="优惠卷名称">
          <el-input
            class="input"
            clearable
            placeholder="请输入y"
            prefix-icon="el-icon-search"
            v-model="condition.couponName"
          ></el-input>
        </el-form-item> -->
      </template>

      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="toAdd"
          >新增</el-button
        >
      </template>

      <template v-slot:tb_cols>
        <el-table-column label="编号" prop="id" ></el-table-column>
        <el-table-column
          label="门店"
          prop="shopIds"
          :width="200"
        ></el-table-column>
        <el-table-column
          label="套餐卡编号"
          prop="packageCardId"
        ></el-table-column>
        <el-table-column
          label="套餐卡Pid"
          prop="productCode"
          :width="120"
        ></el-table-column>
        <el-table-column label="套餐卡名称" prop="name" width="200"></el-table-column>
        <el-table-column label="图片" align="center" :width="80">
          <template slot-scope="scope">
            <el-image
              v-show="scope.row.image1 != ''"
              @click="routeImageDetail(scope.row.image1)"
              style="width: 25px; height: 25px"
              :src="scope.row.image1"
            ></el-image>
          </template>
        </el-table-column>
        <el-table-column
          label="销售价"
          prop="salesPrice"
          :width="80"
          align="right"
        ></el-table-column>
        <el-table-column align="center" label="是否上架" :width="80">
          <template slot-scope="scope">
            <el-tag
              size="mini"
              :type="scope.row.onSale === 1 ? '' : 'danger'"
              >{{ scope.row.onSale === 1 ? "上架" : "下架" }}</el-tag
            >
          </template>
        </el-table-column>
        <el-table-column
          align="center"
          label="排序"
          prop="rank"
          :width="60"
        ></el-table-column>
        <el-table-column
          align="center"
          label="类型"
          prop="typeDisplay"
          :width="80"
        ></el-table-column>

        <!-- <el-table-column
          label="创建人"
          prop="createdBy"
          width="100"
        ></el-table-column> -->
        <el-table-column fixed="right" label="操作">
          <template slot-scope="scope">
            <el-button size="mini" type="primary" @click="detail(scope.row)"
              >详情</el-button
            >
          </template>
        </el-table-column>
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
          <el-form-item label="促销门店">
            <el-input
              style="width: 300px"
              v-model="currentDetail.shopIds"
              placeholder="门店Id以,分割示例：7,8"
            ></el-input>
          </el-form-item>
          <el-form-item label="套餐卡名称">
            <el-select
              v-model="currentDetail.name"
              filterable
              remote
              clearable
              reserve-keyword
              placeholder="产品名称或Pid"
              :remote-method="getProductList"
              :loading="loading"
              @change="getProductInfo"
              style="width: 300px"
            >
              <el-option
                v-for="item in productSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="套餐卡ID">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.packageCardId"
            ></el-input>
          </el-form-item>
          <el-form-item label="套餐卡Pid">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.productCode"
            ></el-input>
          </el-form-item>

          <el-form-item label="售价">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.salesPrice"
            ></el-input>
          </el-form-item>
          <el-form-item label="上下架">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.onSaleName"
            ></el-input>
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
        <el-button
          size="mini"
          type="danger"
          :loading="loading"
          v-show="currentDetail.id > 0"
          @click="deleteData()"
          >作废</el-button
        >
      </template>
    </rg-dialog>
  </div>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/product/promotion";
import { productSearchSvc } from "@/api/product/productsearch";
export default {
  name: "promotionactivitylist",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      condition: {
        key: "",
        // activityId: "",
        // productCode: "",
        // startDate: "",
        // endDate: "",
        // status: 0,
        pageIndex: 1,
        pageSize: 10,
      },
      tableData: [],
      promotionStatus: [
        {
          displayName: "全部",
          value: 0,
        },
        {
          displayName: "进行中",
          value: 4,
        },
        {
          displayName: "未开始",
          value: 3,
        },
        {
          displayName: "已结束",
          value: 5,
        },
        {
          displayName: "待审核",
          value: 1,
        },
        {
          displayName: "已拒绝",
          value: 2,
        },
      ],
      formTitle: "门店促销配置",
      dialogVisible: false,
      currentDetail: {
        id: 0,
        packageCardId: 0,
        shopIds: "",
        isDeleted: 0,
        salesPrice: "",
        onSaleName: "",
        name: "",
        productCode: "",
      },
      loading: false,
      activitySelCondition: {
        productName: undefined,
        pageIndex: 1,
        pageSize: 20,
      },
      productSel: [],
      couponSelCondition: {
        displayName: undefined,
      },
      couponSel: [],
    };
  },
  created() {
    this.getPaged();
  },
  methods: {
    routeImageDetail(url) {
      window.open(url);
    },
    toAdd() {
      this.dialogVisible = true;
      this.currentDetail.shopIds = "";
      this.currentDetail.packageCardId = 0;
      this.currentDetail.id = 0;
      this.currentDetail.isDeleted = "";
      this.currentDetail.productCode = "";
      this.currentDetail.name = "";
    },
    searchPromotionActivity() {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(true);
    },
    getPaged(flag) {
      console.log("condition: " + JSON.stringify(this.condition));
      this.tableLoading = true;
      appSvc
        .GetShopPackageCardProductPageList(this.condition)
        .then(
          (res) => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (flag) {
              this.$message({
                message: res.message || "查询成功",
                type: "success",
              });
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });
    },
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },

    routeDetail(activityParaId) {},
    auditPromotion(activityParaId) {
      var auditRequest = {
        activityId: activityParaId,
      };
      appSvc
        .auditPromotionActivity(auditRequest)
        .then(
          (res) => {
            var data = res.data;
            if (data) {
              this.$message({ message: "操作成功", type: "success" });
            } else {
              this.$message({ message: "操作失败", type: "warning" });
            }
            this.getPaged(false);
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });
    },
    cancel() {
      this.dialogVisible = false;
    },
    getProductList(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.activitySelCondition.productName = query;
          productSearchSvc
            .searchProductForPackageCard(this.activitySelCondition)
            .then(
              (res) => {
                let items = res.data;
                if (res.data != null) {
                  if (res.data.items != null) {
                    for (let i = 0; i < res.data.items.length; i++) {
                      debugger;
                      this.productSel.push({
                        key: res.data.items[i].packageCardId,
                        value: res.data.items[i].name,
                        productCode: res.data.items[i].productCode,
                        salesPrice: res.data.items[i].salesPrice,
                        onSale: res.data.items[i].onSale,
                      });
                    }
                  }
                }
                // this.activitySel = res.data;
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        }, 200);
      } else {
        // this.options = [];
      }
    },
    getProductInfo(value) {
      // alert(1);
      // this.loading = true;
      // setTimeout(() => {
      // this.loading = false;

      if (value != undefined && value > 0) {
        if (this.productSel != undefined && this.productSel.length > 0) {
          for (var i = 0; i < this.productSel.length; i++) {
            if (this.productSel[i].key == value) {
              this.currentDetail.packageCardId = value;
              this.currentDetail.productCode = this.productSel[i].productCode;
              this.currentDetail.salesPrice = this.productSel[i].salesPrice;
              if (this.productSel[i].onSale == 1) {
                this.currentDetail.onSaleName = "上架";
              } else if (this.productSel[i].onSale == 0) {
                this.currentDetail.onSaleName = "下架";
              }
            }
          }
        }
      }

      // }, 200);
    },
    submitData() {
      if (this.currentDetail.shopIds == "") {
        this.$message({ message: "请输入门店", type: "warning" });
        return;
      }
      // if(this.currentDetail.shopIds!=""){
      //  let splitData= this.currentDetail.shopIds.split("，");
      //  if(splitData.length>0)
      // }

      if (this.currentDetail.packageCardId == 0) {
        this.$message({ message: "请选择套餐卡", type: "warning" });
        return;
      }

      this.loading = true;
      appSvc
        .SaveShopPackageCard({
          id: this.currentDetail.id,
          cardId: this.currentDetail.packageCardId,
          shopIds: this.currentDetail.shopIds,
          isDeleted: 0,
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
    deleteData() {
      this.loading = true;
      appSvc
        .SaveShopPackageCard({
          id: this.currentDetail.id,
          isDeleted: 1,
          cardId: this.currentDetail.packageCardId,
          shopIds: "",
        })
        .then(
          (res) => {
            var data = res.data;
            this.$message({ message: "作废成功", type: "success" });
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
    detail(value) {
      this.dialogVisible = true;
      this.currentDetail.shopIds = value.shopIds;
      this.currentDetail.packageCardId = value.packageCardId;
      this.currentDetail.productCode = value.productCode;
      this.currentDetail.name = value.name;
      this.currentDetail.id = value.id;
      this.currentDetail.salesPrice=value.salesPrice;
      debugger;
      if (value.onSale == 1) {
        this.currentDetail.onSaleName = "上架";
      } else if (value.status == 0) {
        this.currentDetail.onSaleName = "下架";

        //  this.currentDetail.statusName = value.status;
        this.productSel.push({
          key: value.productCode,
          value: value.name,
          productCode:value.productCode,
          salesPrice: value.salesPrice,
          onSale: value.onSale,
        });
      }
    },
  },
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 45px;
    display: flex;
    align-items: center;
    .input {
      width: 180px;
      margin-right: 20px;
    }
  }
  .input-label {
    margin-left: 4px;
    font-size: 14px;
  }
  .pagination {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100px;
  }
}

/deep/ .el-date-editor {
  /deep/ .el-input__inner {
    height: 32px;
  }
}

/deep/ .el-input__icon {
  line-height: 32px;
}
>>> .el-form--inline .el-form-item {
  padding-bottom: 10px;
}
</style>
