<template>
  <div class="container1">
    <el-form label-width="120px" class="demo-dynamic" style="margin-top:20px;" :inline="true">
      <el-form-item>
        <el-select
          v-model="condition.shopId"
          @change="loadShopProject"
          filterable
          remote
          clearable
          reserve-keyword
          placeholder="请输入门店名称"
          :remote-method="getShoplist"
          :loading="loading"
          size="mini"
        >
          <el-option
            v-for="item in shopList"
            :key="item.id"
            :label="item.simpleName"
            :value="item.id"
          />
        </el-select>
      </el-form-item>

      <el-form-item>
        <el-select
          v-model="condition.categoryId"
          placeholder="请选择项目类型"
          filterable
          size="mini"
          clearable
        >
          <el-option
            v-for="dict in shopProjectList"
            :key="dict.serviceTypeId"
            :label="dict.displayName"
            :value="dict.serviceTypeId"
          />
        </el-select>
      </el-form-item>

      <el-form-item>
        <el-input
          v-model="condition.key"
          autocomplete="off"
          style="width:100%"
          size="mini"
          placeholder="请输入项目名称(编码)"
        />
      </el-form-item>

      <el-button
        type="warning"
        icon="el-icon-search"
        size="mini"
        style="margin-left:40px; margin-top:7px;"
        @click="search(true)"
      >搜索</el-button>
    </el-form>
    <el-tabs v-model="condition.auditStatus" type="card" @tab-click="handleClick">
      <el-tab-pane label="未审核" name="2">
        <el-table
          v-loading="tableLoading"
          :data="tableData"
          border
          style="width: 100%;"
          size="small"
          height="250"
 
        >
          <el-table-column label="门店名称" prop="shopName" width="180" align="center" />
          <el-table-column label="项目类型" prop="categoryName" width="150" align="center" />
          <el-table-column label="项目编码" prop="productCode" width="120" align="center" />
          <el-table-column label="项目名称" prop="productName" align="center" />
          <el-table-column label="小程序展示名称" prop="displayName" align="center" />
          <el-table-column label="销售价格" prop="salesPrice" width="120" align="center" />
          <el-table-column label="申请时间" prop="applyTime" width="150" align="center" />
          <el-table-column label="状态" prop="onSale" width="100" align="center">
            <template slot-scope="scope">
              <el-tag>未审核</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="操作" width="120" align="center" fixed="right">
            <template slot-scope="scope">
              <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">审核</el-button>
            </template>
          </el-table-column>
        </el-table>
        <div class="pagination">
          <el-pagination
            background
            :page-sizes="[10,20, 40, 60, 80]"
            :page-size="10"
            layout="total, sizes, prev, pager, next, jumper"
            :total="totalCount"
            :current-page.sync="condition.pageIndex"
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
          />
        </div>
      </el-tab-pane>
      <el-tab-pane label="已通过" name="1">
        <el-table
          v-loading="tableLoading"
          :data="tableData"
          border
          style="width: 100%;"
          size="mini"
          height="250"
 
        >
          <el-table-column label="门店名称" prop="shopName" width="180" align="center" />
          <el-table-column label="项目类型" prop="categoryName" width="150" align="center" />
          <el-table-column label="项目编码" prop="productCode" width="120" align="center" />
          <el-table-column label="项目名称" prop="productName" align="center" />
          <el-table-column label="小程序展示名称" prop="displayName" align="center" />
          <el-table-column label="销售价格" prop="salesPrice" width="120" align="center" />
          <el-table-column label="申请时间" prop="applyTime" width="150" align="center" />
          <el-table-column label="审核人/审核时间" prop="auditTime" width="150" align="center">
            <template slot-scope="scope">
              {{ scope.row.auditUser}}
              <br />
              {{ scope.row.auditTime}}
            </template>
          </el-table-column>
          <el-table-column label="状态" prop="onSale" width="100" align="center">
            <template slot-scope="scope">
              <el-tag>已通过</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="审核意见" prop="auditOpinion" align="center" />
          <el-table-column label="操作" width="120" align="center" fixed="right">
            <template slot-scope="scope">
              <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">审核详情</el-button>
            </template>
          </el-table-column>
        </el-table>
        <div class="pagination">
          <el-pagination
            background
            :page-sizes="[10,20, 40, 60, 80]"
            :page-size="10"
            layout="total, sizes, prev, pager, next, jumper"
            :total="totalCount"
            :current-page.sync="condition.pageIndex"
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
          />
        </div>
      </el-tab-pane>
      <el-tab-pane label="未通过" name="3">
        <el-table
          v-loading="tableLoading"
          :data="tableData"
          border
          style="width: 100%;"
          size="small"
          height="250"
 
        >
          <el-table-column label="门店名称" prop="shopName" width="180" align="center" />
          <el-table-column label="项目类型" prop="categoryName" width="150" align="center" />
          <el-table-column label="项目编码" prop="productCode" width="120" align="center" />
          <el-table-column label="项目名称" prop="productName" align="center" />
          <el-table-column label="小程序展示名称" prop="displayName" align="center" />
          <el-table-column label="销售价格" prop="salesPrice" width="120" align="center" />
          <el-table-column label="申请时间" prop="applyTime" width="150" align="center" />
          <el-table-column label="审核人/审核时间" prop="auditTime" width="150" align="center">
            <template slot-scope="scope">
              {{ scope.row.auditUser}}
              <br />
              {{ scope.row.auditTime}}
            </template>
          </el-table-column>
          <el-table-column label="状态" prop="onSale" width="100" align="center">
            <template slot-scope="scope">
              <el-tag>未通过</el-tag>
            </template>
          </el-table-column>
          <el-table-column label="审核意见" prop="auditOpinion" align="center" />
          <el-table-column label="操作" width="120" align="center" fixed="right">
            <template slot-scope="scope">
              <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">审核详情</el-button>
            </template>
          </el-table-column>
        </el-table>
        <div class="pagination">
          <el-pagination
            background
            :page-sizes="[10,20, 40, 60, 80]"
            :page-size="10"
            layout="total, sizes, prev, pager, next, jumper"
            :total="totalCount"
            :current-page.sync="condition.pageIndex"
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
          />
        </div>
      </el-tab-pane>
    </el-tabs>
          <rg-dialog
        :title="'申请信息'"
        :visible.sync="dialogFormVisible"
        :beforeClose="cancelForm"
        :btnCancel="{label:'关闭',click:(done)=>{cancelForm()}}"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="800px"
        minWidth="600px"
      >
        <template v-slot:content>

      <el-form
        v-if="dialogFormVisible"
        :model="formModel"
        :rules="rules"
        label-width="120px"
        ref="formModel"
      >
        <el-row>
          <el-col :span="14">
            <el-form-item label="门店名称">
              <el-select
                v-if="formModel.id == 0"
                v-model="formModel.shopId"
                @change="loadShopProjectEdit"
                filterable
                remote
                clearable
                reserve-keyword
                placeholder="请输入门店名称"
                :remote-method="getShoplist"
                :loading="loading"
                style="width:100%"
                size="medium"
                
              >
                <el-option
                  v-for="item in shopList"
                  :key="item.id"
                  :label="item.simpleName"
                  :value="item.id"
                />
              </el-select>
              <el-input
                v-else-if="formModel.id > 0"
                v-model="formModel.shopName"
                autocomplete="off"
                size="medium"
                disabled
              />
            </el-form-item>
          </el-col>
          <el-col :span="10">
            <el-form-item label="项目类型">
              <el-select
                :disabled="formModel.id > 0"
                v-model="formModel.categoryId"
                placeholder="请选择项目类型"
                filterable
                size="small"
                clearable
                style="width:100%"
              >
                <el-option
                  v-for="dict in shopProjectList"
                  :key="dict.serviceTypeId"
                  :label="dict.displayName"
                  :value="dict.serviceTypeId"
                />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="10">
            <el-form-item label="项目编码">
              <el-input v-model="formModel.productCode" autocomplete="off" size="medium" disabled />
            </el-form-item>
          </el-col>
          <el-col :span="14">
            <el-form-item label="项目名称">
              <el-input v-model="formModel.productName" autocomplete="off" size="medium" disabled />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="10">
            <el-form-item label="销售价格">
              <el-input
                v-model="formModel.salesPrice"
                type="number"
                autocomplete="off"
                step="0.01"
                min="0"
                size="medium"
                disabled
              />
            </el-form-item>
          </el-col>
          <el-col :span="14">
            <el-form-item label="小程序展示名称">
              <el-input v-model="formModel.displayName" autocomplete="off" size="medium" disabled />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="项目描述">
              <el-input v-model="formModel.description" autocomplete="off" size="medium" disabled />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="8">
            <el-form-item label="是否上架">
              <el-radio-group v-model="formModel.onSale" disabled>
                <el-radio :label="1">是</el-radio>
                <el-radio :label="0">否</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="申请时间">{{formModel.applyTime}}</el-form-item>
          </el-col>
          <el-col :span="7">
            <el-form-item
              label="状态"
            >{{ formModel.auditStatus==2 ? "待审核" : formModel.auditStatus==1 ? "已通过" : formModel.auditStatus==3 ? "未通过":"" }}</el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="8">
            <el-form-item label="缩率图">
              <div style="float:left" v-for="img in fileList">
                <el-image style="width: 100px; height: 100px;margin-left:15px" :src="img.url" />
              </div>
            </el-form-item>
          </el-col>
          <el-col :span="10">
            <el-form-item label="排序方式">
              <el-radio-group v-model="formModel.sortType" disabled>
                <el-radio :label="1">置顶</el-radio>
                <el-radio :label="2">上架时间</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="详情图">
              <div style="float:left" v-for="img in detialfileList">
                <el-image style="width: 100px; height: 100px;margin-left:15px" :src="img.url" />
              </div>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="详情" prop="detail">
              <el-input v-model="formModel.detail" type="textarea" :rows="3" disabled />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="审核意见" prop="detail">
              <el-input v-model="formModel.auditOpinion" type="textarea" :rows="2" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
        </template>
        <template v-slot:footer v-if="formModel.auditStatus==2">

      <!-- <div slot="footer" class="dialog-footer" v-if="formModel.auditStatus==2"> -->
        <el-button @click="auditShopProduct(3)" size="mini">拒 绝</el-button>
        <el-button type="primary" @click="auditShopProduct(1)" size="mini">通 过</el-button>
      <!-- </div> -->
        </template>
    </rg-dialog>
  </div>
</template>
<script>
import { upload } from "@/utils/uploadfile";
import { appSvc } from "@/api/qiniu/qiniuservice";
import { shopproductmanageSvc } from "@/api/product/shopproductmanage";
export default {
  name: "shopproductstatemanage",
  data() {
    return {
      imagehost: "https://m.aerp.com.cn/",
      tableData: [],
      tableLoading: false,
      options: [
        {
          value: "",
          label: "全部"
        },
        {
          value: 1,
          label: "是"
        },
        {
          value: 0,
          label: "否"
        }
      ],
      dialogFormVisible: false,
      condition: {
        key: "",
        isDeleted: 0,
        isOnSale: 0,
        pageIndex: 1,
        pageSize: 10,
        shopId: undefined,
        categoryId: undefined,
        auditStatus: "2"
      },
      totalCount: 0,
      rules: {
        shopId: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ],
        categoryId: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ],
        productName: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ],
        displayName: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ],
        salesPrice: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ],
        icon: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ]
      },
      formModelInit: {
        id: 0,
        shopId: undefined,
        categoryId: undefined,
        productCode: "",
        productName: "",
        salesPrice: 0,
        displayName: "",
        description: "",
        onSale: 0,
        isDeleted: 0,
        sortType: 2,
        detail: "",
        images: []
      },
      formModel: {
        id: 0,
        shopId: undefined,
        categoryId: undefined,
        productCode: "",
        productName: "",
        salesPrice: 0,
        displayName: "",
        description: "",
        onSale: 0,
        isDeleted: 0,
        sortType: 2,
        detail: "",
        images: [],
        auditStatus: 0
      },
      IsEdit: false,
      shopSelCondition: {
        simpleName: undefined,
        pageIndex: 1,
        pageSize: 50
      },
      shopList: [],
      loading: false,
      shopProjectList: [],
      formLabelWidth: "100px",
      fileList: [],
      detialfileList: [],
      productErrorData: [],
      dialogErrorFormVisible: false,
      importProductUrl: ""
    };
  },
  created() {
    this.getPageData();
  },
  methods: {
    //获取门店信息
    getShoplist(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.simpleName = query;
          shopproductmanageSvc
            .getShopList(this.shopSelCondition)
            .then(
              res => {
                var resData = res.data;
                this.shopList = resData.items;
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        }, 200);
      } else {
        this.options = [];
      }
    },
    //获取项目类型
    loadShopProject() {
      if (!this.condition.shopId) {
        return;
      }
      this.shopProjectList = [];
      this.condition.categoryId = undefined;
      shopproductmanageSvc
        .getShopServiceTgype({ shopId: this.condition.shopId })
        .then(
          res => {
            var resData = res.data;
            this.shopProjectList = resData;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //获取项目类型
    loadShopProjectEdit() {
      if (!this.formModel.shopId) {
        return;
      }
      this.shopProjectList = [];
      this.formModel.categoryId = undefined;
      shopproductmanageSvc
        .getShopServiceTgype({ shopId: this.formModel.shopId })
        .then(
          res => {
            var resData = res.data;
            this.shopProjectList = resData;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    getPageData(flag) {
      this.tableLoading = true;
      shopproductmanageSvc
        .searchShopProduct(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalCount = data.totalItems;
            if (flag) {
              this.$message({
                message: res.message || "查询成功",
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
          this.tableLoading = false;
        });
    },
    search(flag) {
      this.condition.pageIndex = 1;
      this.getPageData(flag);
    },
    // 分页
    handleSizeChange(val) {
      this.condition.pageIndex = 1;
      this.condition.pageSize = val;
      this.getPageData(false);
    },
    handleCurrentChange(val) {
      this.getPageData(false);
    },
    handleAdd() {
      this.dialogFormVisible = true;
      this.IsEdit = false;
      this.attributeArray = [];
    },
    handleEdit(index, row) {
      this.tableLoading = true;
      var shopProductCode = row.productCode;
      this.IsEdit = true;
      shopproductmanageSvc
        .getShopProductByCode({ shopProductCode: shopProductCode })
        .then(
          res => {
            var data = res.data;
            this.formModel = data;
            this.fileList = [
              { name: "", url: this.imagehost + this.formModel.icon }
            ];
            var imagehost = this.imagehost;
            var detialfileList = this.detialfileList;
            (this.formModel.images || []).map(function(t) {
              var oldimage = { name: "", url: imagehost + t };
              detialfileList.push(oldimage);
            });
            this.detialfileList = detialfileList;
            this.dialogFormVisible = true;
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
    auditShopProduct(status) {
      if (status == 3 && this.formModel.auditOpinion == "") {
        this.$message({
          message: "请填写审核意见！",
          type: "warning"
        });
        return;
      }
      const loading = this.$loading({
        lock: true,
        text: "Loading",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)"
      });
      var data = {
        ShopProductId: this.formModel.id,
        ShopProductCode: this.formModel.productCode,
        AuditStatus: status,
        AuditOpinion: this.formModel.auditOpinion
      };
      shopproductmanageSvc
        .auditShopProduct(data)
        .then(
          res => {
            var data = res.data;
            if (data) {
              this.$message({
                message: "操作成功！",
                type: "success"
              });
              this.formModel = this.formModelInit;
              this.detialfileList = [];
              this.fileList = [];
              this.dialogFormVisible = false;
              this.search(true);
            } else {
              this.$message({
                message: "操作失败:" + res.message,
                type: "error"
              });
            }
            setTimeout(function() {
              loading.close();
            }, 1500);
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    },
    cancelForm(formName) {
      this.fileList = [];
      this.detialfileList = [];
      this.dialogFormVisible = false;
      this.formModel = this.formModelInit;
    },
    // 图片上传--start ----
    handleRemove(file, fileList) {
      this.formModel.icon = "";
    },
    handlePreview(file) {
      // console.log(file);
    },
    handleExceed(files, fileList) {
      this.$message.warning("限制是1个!");
    },
    beforeRemove(file, fileList) {
      return this.$confirm(`确认删除?`);
    },
    // 获取token
    getToken(directoryName, fileName) {
      return appSvc.getQiNiuToken({
        directory: directoryName,
        fileName: fileName
      });
    },
    uploadRequest: function(request) {
      var fileName = request.file.name;
      var directoryName = "shopProductImage";
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        appSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        appSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;
      var key = directoryName + "/" + newFileName;
      this.getToken(directoryName, fileName).then(res => {
        var result = res.data;
        const token = result.token;
        const host = result.host;
        upload(
          token,
          key,
          request,
          next => {
            // eslint-disable-next-line no-unused-vars
            const total = next.total;
          },
          // eslint-disable-next-line handle-callback-err
          error => {
            this.fileList = [];
          },
          complete => {
            // eslint-disable-next-line no-unused-vars
            const hash = complete.hash;
            const key = complete.key;
            var url = host + "/" + key;
            this.fileList = [{ name: "", url: url }];
            this.formModel.icon = key;
          }
        );
      });
    },
    handleDetialExceed(files, fileList) {
      this.$message.warning("限制是4个!");
    },
    //上传详情
    uploadDetialRequest: function(request) {
      var fileName = request.file.name;
      var directoryName = "shopProductImage";
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        appSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        appSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;
      var key = directoryName + "/" + newFileName;
      this.getToken(directoryName, fileName).then(res => {
        var result = res.data;
        const token = result.token;
        const host = result.host;
        upload(
          token,
          key,
          request,
          next => {
            // eslint-disable-next-line no-unused-vars
            const total = next.total;
          },
          // eslint-disable-next-line handle-callback-err
          error => {
            this.fileList = [];
          },
          complete => {
            // eslint-disable-next-line no-unused-vars
            const hash = complete.hash;
            const key = complete.key;
            var url = host + "/" + key;
            var newImage = { name: "", url: url };
            this.detialfileList.push(newImage);
            this.formModel.images.push(key);
          }
        );
      });
    },
    // 图片上传--end ----
    //导入商品信息
    uploadProductFile(request) {
      const loading = this.$loading({
        lock: true,
        text: "Loading",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)"
      });
      let formData = new FormData();
      formData.append("file", request.file);
      shopproductmanageSvc.importShopProduct(formData).then(
        res => {
          loading.close();
          var reData = res.data;
          if (reData && reData.length > 0) {
            this.productErrorData = reData;
            this.dialogErrorFormVisible = true;
          } else {
            this.$message.success("导入成功!");
          }
          this.$refs.uploadProduct.clearFiles();
        },
        err => {
          loading.close();
          this.$refs.uploadProduct.clearFiles();
        }
      );
    },
    // 直接下载，用户体验好
    download: function() {
      var filePath =
        "https://m.aerp.com.cn/productTemplate/门店商品导入模板.xlsx";
      window.open(filePath);
    },
    handleClick(tab, event) {
      this.condition.pageIndex = 1;
      if (tab.name == "1") {
        this.condition.isOnSale = 1;
      } else {
        this.condition.isOnSale = 0;
      }
      this.getPageData(true);
    }
  }
};
</script>
<style lang="scss" scoped>
.container1 {
  margin: -10px 0px 0px 30px;
  height:100%;
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
  .input-label {
    display: inline-block;
    width: 100px;
    margin-left: 40px;
  }
}
>>> .el-table {
  height: calc(100vh - 300px) !important;
}
</style>

