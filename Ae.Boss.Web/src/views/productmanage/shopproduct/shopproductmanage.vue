<template>
  <div class="container">
    <rg-page
      background
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalCount"
      :tableLoading="tableLoading"
      :tableData="tableData"
      :pageChange="pageTurning"
      :headerBtnWidth="180"
      :searching="search"
      :defaultCollapse="false"
    >
      <template v-slot:condition>
        <!-- <el-form label-width="120px" class="demo-dynamic">
      <el-row>
        <el-col :span="6">-->
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
            size="small"
          >
            <el-option
              v-for="item in shopList"
              :key="item.id"
              :label="item.simpleName"
              :value="item.id"
            />
          </el-select>
        </el-form-item>
        <!-- </el-col>
        <el-col :span="6">-->
        <el-form-item>
          <el-select
            v-model="condition.categoryId"
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
        <!-- </el-col>
        <el-col :span="6">-->
        <el-form-item>
          <el-input
            v-model="condition.key"
            autocomplete="off"
            style="width:100%"
            size="small"
            placeholder="请输入项目名称(编码)"
          />
        </el-form-item>

        <el-form-item label="是否上架">
          <el-select
            v-model="condition.isOnSale"
            filterable
            placeholder="请选择是否上架"
            size="small"
            clearable
          >
            <el-option
              v-for="item in options"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            />
          </el-select>
        </el-form-item>
     
        <el-form-item label="是否禁用">
          <el-select v-model="condition.isDeleted" size="mini" placeholder="请选择是否禁用">
            <el-option
              v-for="item in options"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-upload
            style="display:inline"
            ref="uploadProduct"
            :action="importProductUrl"
            :limit="1"
            :show-file-list="false"
            :http-request="uploadProductFile"
            accept=".xls, .xlsx"
          >
            <el-button size="mini" type="primary" icon="el-icon-edit">导入商品</el-button>
          </el-upload>
          <el-button type="primary" size="mini" @click="download" icon="el-icon-download">下载模板</el-button>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="handleAdd">新增</el-button>
      </template>
      <template v-slot:tb_cols>
        <el-table-column label="门店名称" prop="shopName" />
        <el-table-column label="项目类型" prop="categoryName" width="150" />
        <el-table-column label="项目编码" prop="productCode" />
        <el-table-column label="项目名称" prop="productName" />
        <el-table-column label="销售价格" prop="salesPrice" width="150" />
        <el-table-column label="是否上架" prop="onSale" width="150">
          <template slot-scope="scope">
            <el-tag
              :type="scope.row.onSale === 0 ? '' : 'danger'"
            >{{ scope.row.onSale === 1 ? '是' : '否' }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="是否禁用" prop="isDeleted" width="150">
          <template slot-scope="scope">
            <el-tag
              :type="scope.row.isDeleted === 0 ? '' : 'danger'"
            >{{ scope.row.isDeleted === 1 ? '是' : '否'}}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="120" align="center">
          <template slot-scope="scope">
            <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
          </template>
        </el-table-column>
      </template>
    </rg-page>

        <rg-dialog
      :title="formModel.id ? '编辑':'新增'"
      :visible.sync="dialogFormVisible"
      v-if="dialogFormVisible"
      :footbar="true"
      :beforeClose="cancelForm"
      :btnCancel="{label:'关闭',click:(done)=>{cancelForm('formModel')}}"
      maxWidth="900px"
      minWidth="700px"
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
          <el-col :span="16">
            <el-form-item label="门店名称" prop="shopId">
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
          <el-col :span="16">
            <el-form-item label="项目类型" prop="categoryId">
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
          <el-col :span="16">
            <el-form-item label="项目编码" prop="productCode">
              <el-input v-model="formModel.productCode" autocomplete="off" size="medium" disabled  placeholder="请输入项目编码"/>
            </el-form-item>
          </el-col>
          <el-col :span="16">
            <el-form-item label="项目名称" prop="productName">
              <el-input v-model="formModel.productName" autocomplete="off" size="medium"  placeholder="项目名称"/>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="16">
            <el-form-item label="销售价格" prop="salesPrice">
              <el-input
                v-model="formModel.salesPrice"
                type="number"
                autocomplete="off"
                step="0.01"
                min="0"
                size="medium"
              />
            </el-form-item>
          </el-col>
          <el-col :span="16">
            <el-form-item label="折扣率" prop="discountRate">
              <el-input
                v-model="formModel.discountRateS"
                size="medium" style="width:100px"
              />%
            </el-form-item>
          </el-col>
          <el-col :span="16">
            <el-form-item label="小程序展示名称" prop="displayName">
              <el-input v-model="formModel.displayName" autocomplete="off" size="medium" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="16">
            <el-form-item label="项目描述" prop="description">
              <el-input v-model="formModel.description" autocomplete="off" size="medium" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="8">
            <el-form-item label="是否上架">
              <el-radio-group v-model="formModel.onSale">
                <el-radio :label="1">是</el-radio>
                <el-radio :label="0">否</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          <el-col :span="10">
            <el-form-item label="排序方式">
              <el-radio-group v-model="formModel.sortType">
                <el-radio :label="1">置顶</el-radio>
                <el-radio :label="2">上架时间</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="是否禁用">
              <el-radio-group v-model="formModel.isDeleted">
                <el-radio :label="1">是</el-radio>
                <el-radio :label="0">否</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="缩率图">
              <el-upload
                class="upload-demo"
                action="http://upload.qiniup.com/"
                :on-preview="handlePreview"
                :on-remove="handleRemove"
                :before-remove="beforeRemove"
                multiple
                :limit="1"
                :on-exceed="handleExceed"
                :file-list="fileList"
                list-type="picture"
                :http-request="uploadRequest"
                accept="image/jpeg, image/png, image/jpg"
              >
                <el-button size="small" type="primary">上传图片</el-button>
                <div slot="tip" class="el-upload__tip">大小小于500kb的jpg/png文件</div>
              </el-upload>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="详情图">
              <el-upload
                class="upload-demo"
                action="http://upload.qiniup.com/"
                :on-preview="handlePreview"
                :on-remove="handleDetialRemove"
                :before-remove="beforeRemove"
                multiple
                :limit="4"
                :on-exceed="handleDetialExceed"
                :file-list="detialfileList"
                list-type="picture-card"
                :http-request="uploadDetialRequest"
                accept="image/jpeg, image/png, image/jpg"
              >
                <el-button size="small" type="primary">上传图片</el-button>
                <div slot="tip" class="el-upload__tip">大小小于500kb的jpg/png文件</div>
              </el-upload>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="16">
            <el-form-item label="详情" prop="detail">
              <el-input v-model="formModel.detail" type="textarea" :rows="5" autocomplete="off" />
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
          </template>
      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          size="mini"
          type="primary"
          @click="submitForm('formModel')"
        >保存</el-button>
      </template>

    </rg-dialog>
    <el-dialog :title="'提示'" hei width="800" :visible.sync="dialogErrorFormVisible">
      <div style="width:100%; height:500">
        <el-table :data="productErrorData" border>
          <el-table-column label="商品名称" prop="productName" />
          <el-table-column label="异常原因" prop="errorMessage" />
        </el-table>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import { upload } from "@/utils/uploadfile";
import { appSvc } from "@/api/qiniu/qiniuservice";
import { shopproductmanageSvc } from "@/api/product/shopproductmanage";
export default {
  name: "shopproductmanage",
  data() {
    return {
      imagehost: "http://m.aerp.com.cn/",
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
        isOnSale: "",
        pageIndex: 1,
        pageSize: 10,
        shopId: undefined,
        categoryId: undefined
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
        ],
        discountRate:[
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              var reg = new RegExp("^(\\d|[1-9]\\d|100)$");
              if (!reg.test(this.formModel.discountRateS))
                callback(new Error("折扣率输入有误，请输入0-100的整数"));
              else callback();
            }
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
        discountRateS:0,
        discountRate:0,
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
        discountRateS:0,
        discountRate:0,
        displayName: "",
        description: "",
        onSale: 0,
        isDeleted: 0,
        sortType: 2,
        detail: "",
        images: []
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
    //this.getPageData(false);
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
      if (!this.condition.shopId) {
        this.$message({
          showClose: true,
          message: "请选择要查询的门店！",
          type: "warning"
        });
        return;
      }
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
   pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPageData();
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
            //Object.assign(this.formModel,data)
            var discountRate = this.formModel.discountRate;
            //this.formModel.discountRateS = discountRate * 100;
            this.$set(this.formModel,"discountRateS",discountRate * 100)
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
    submitForm(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          if (this.formModel.icon == "" || !this.formModel.icon) {
            this.$message({
              message: "请上传缩率图！",
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
          this.formModel.discountRate = parseInt(this.formModel.discountRateS)/100;
          var data = {
            ShopProduct: this.formModel,
            IsEdit: this.formModel.id > 0
          };
          shopproductmanageSvc
            .saveShopProduct(data)
            .then(
              res => {
                var data = res.data;
                if (data) {
                  this.$message({
                    message: "保存成功",
                    type: "success"
                  });
                  this.formModel = this.formModelInit;
                  this.detialfileList = [];
                  this.fileList = [];
                  this.dialogFormVisible = false;
                  this.search(true);
                } else {
                  this.$message({
                    message: "保存失败:" + res.message,
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
        }
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
    handleDetialRemove(file, fileList) {
      var imgIndex = this.formModel.images.findIndex(
        t => this.imagehost + t == file.url
      );
      this.formModel.images.splice(imgIndex, 1);
      var img01Index = this.detialfileList.findIndex(t => this.url == file.url);
      this.detialfileList.splice(img01Index, 1);
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
    }
  }
};
</script>
<style lang="scss" scoped>
.container {
  margin: 25px 0px 0px 10px;
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
>>> .el-form--inline .el-form-item {
  padding-bottom: 10px;
}
</style>

