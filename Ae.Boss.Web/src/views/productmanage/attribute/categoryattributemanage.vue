<template>
  <div class="container">
    <el-form label-width="120px" class="demo-dynamic" inline="true" >
          <el-form-item>
            <el-select
              v-model="condition.mainCategoryId"
              @change="loadSecondCategorys"
              placeholder="请选择一级类目"
              filterable
              size="mini"
              clearable
            >
              <el-option
                v-for="dict in mainCategoryList"
                :key="dict.id"
                :label="dict.displayName"
                :value="dict.id"
              />
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.secondCategoryId"
              @change="loadChildCategorys"
              placeholder="请选择二级类目"
              filterable
              size="mini"
              clearable
            >
              <el-option
                v-for="dict in secondCategoryList"
                :key="dict.id"
                :label="dict.displayName"
                :value="dict.id"
              />
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.childCategoryId"
              filterable
              placeholder="请选择三级类目"
              size="mini"
              clearable
            >
              <el-option
                v-for="dict in childCategoryList"
                :key="dict.id"
                :label="dict.displayName"
                :value="dict.id"
              />
            </el-select>
          </el-form-item>

          <el-form-item>
            <el-input
              v-model="condition.key"
              autocomplete="off"
              style="width:200px"
              size="mini"
              placeholder="请输入属性名称"
            />
          </el-form-item>

          <el-button type="primary" size="mini" style="margin-left:50px; margin-top:5px;" @click="handleAdd">新增</el-button>
          <el-button type="primary" size="mini" @click="handleEdit(0)" style=" margin-top:5px;">启用</el-button>
          <el-button type="danger" size="mini" @click="handleEdit(1)" style=" margin-top:5px;">禁用</el-button>
          <el-button type="primary" size="mini" @click="search(true)" style=" margin-top:5px;">搜索</el-button>
      <el-row>
        <el-col :span="7" style="margin-left:-40px;">
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
        </el-col>
      </el-row>
    </el-form>
    <el-table
      v-loading="tableLoading"
      :data="tableData"
      border
      style="width: 100%"
      size="small"
      @selection-change="handleSelectionChange"
    >
      <el-table-column type="selection" width="40" align="center" />
      <el-table-column label="属性编码" prop="attributeName" />
      <el-table-column label="属性名称" prop="attributeDisplayName" />
      <el-table-column label="是否禁用" prop="isDisable" width="150">
        <template slot-scope="scope">
          <el-tag
            :type="scope.row.isDeleted === 0 ? '' : 'danger'"
          >{{scope.row.isDeleted === 1 ? '是' : '否'}}</el-tag>
        </template>
      </el-table-column>
      <!-- <el-table-column label="操作" width="120" align="center">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)" :type="scope.row.isDeleted === 0 ? 'danger' : ''">
            {{scope.row.isDeleted === 1 ? '启用' : '禁用'}}</el-button>
        </template>
      </el-table-column>-->
    </el-table>
    <rg-dialog
      :title="'新增'"
      :visible.sync="dialogFormVisible"
      v-if="dialogFormVisible"
      :footbar="true"
      :beforeClose="cancelForm"
      :btnCancel="{label:'关闭',click:(done)=>{cancelForm('formModel')}}"
      maxWidth="900px"
      minWidth="700px"
    >
      <template v-slot:content>
        <el-form ref="formModel" :model="formModel" :rules="rules" >
          <el-row>
            <el-col :span="16">
              <el-form-item label="一级类目" prop="mainCategoryId" :label-width="formLabelWidth">
                <el-select
                  v-model="formModel.mainCategoryId"
                  placeholder="请选择一级类目"
                  filterable
                  size="mini"
                  clearable
                     style="width:80%"

                  @change="loadSecondCategorys"
                >
                  <el-option
                    v-for="dict in mainCategoryList"
                    :key="dict.id"
                    :label="dict.displayName"
                    :value="dict.id"
                  />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="16">
              <el-form-item label="二级类目" prop="secondCategoryId" :label-width="formLabelWidth">
                <el-select
                  v-model="formModel.secondCategoryId"
                  placeholder="请选择二级类目"
                  filterable
                  size="mini"
                  clearable
                     style="width:80%"
                  @change="loadChildCategorys"
                >
                  <el-option
                    v-for="dict in secondCategoryList"
                    :key="dict.id"
                    :label="dict.displayName"
                    :value="dict.id"
                  />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="16">
              <el-form-item label="三级类目" prop="childCategoryId" :label-width="formLabelWidth">
                <el-select
                  v-model="formModel.childCategoryId"
                  filterable
                  placeholder="请选择三级类目"
                  size="small"
                  clearable
                   style="width:80%"
                >
                  <el-option
                    v-for="dict in childCategoryList"
                    :key="dict.id"
                    :label="dict.displayName"
                    :value="dict.id"
                  />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="16">
              <el-form-item label="属性信息" prop="attributeNames" :label-width="formLabelWidth">
                <el-select
                  v-model="formModel.attributeNames"
                  filterable
                  placeholder="请选择属性信息"
                  size="mini"
                  clearable
                  multiple
                  style="width:80%"
                >
                  <el-option
                    v-for="dict in attributeList"
                    :key="dict.id"
                    :label="dict.displayName"
                    :value="dict.id"
                  />
                </el-select>
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
  </div>
</template>

<script>
import { categorySearchSvc } from "@/api/product/categorysearch";
import { productUtil } from "@/api/product/common";
import { categoryAttributeSearchSvc } from "@/api/product/categoryAttributeSearch";
export default {
  name: "categoryattributemanage",
  data() {
    return {
      tableData: [],
      tableLoading: false,
      options: [
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
        mainCategoryId: undefined,
        secondCategoryId: undefined,
        childCategoryId: undefined,
        isDeleted: 0
      },
      totalCount: 0,
      rules: {
        mainCategoryId: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ],
        secondCategoryId: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ],
        childCategoryId: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ],
        attributeNames: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ]
      },
      formModel: {
        mainCategoryId: undefined,
        secondCategoryId: undefined,
        childCategoryId: undefined,
        attributeNames: []
      },
      mainCategoryList: [],
      secondCategoryList: [],
      childCategoryList: [],
      attributeList: [],
      multipleSelection: []
    };
  },
  created() {
    //this.getPageData(false);
    this.loadMainCategorys();
    this.loadAttributes();
  },
  methods: {
    getPageData(flag) {
      this.tableLoading = true;
      categoryAttributeSearchSvc
        .selectCategoryAttribute(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data;
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
      if (!this.condition.childCategoryId) {
        this.$message({
          message: "请选择三级类目！",
          type: "warning"
        });
        return;
      }
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
    },
    handleEdit(type) {
      if (this.multipleSelection.length == 0) {
        this.$message({
          message: "请选择要编辑的数据",
          type: "warning"
        });
        return;
      }
      debugger;
      var categoryAttributeList = [];
      this.multipleSelection.map(function(t) {
        if (t.isDeleted != type) {
          categoryAttributeList.push({
            AttributeNameId: t.attributeNameId,
            IsDeleted: type,
            Id: t.id
          });
        }
      });
      if (categoryAttributeList.length == 0) {
        return;
      }
      const loading = this.$loading({
        lock: true,
        text: "Loading",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)"
      });
      var data = {
        CategoryId: 0,
        CategoryAttributes: categoryAttributeList
      };
      categoryAttributeSearchSvc
        .saveCategoryAttribute(data)
        .then(
          res => {
            var data = res.data;
            if (data) {
              this.$message({
                message: "编辑成功",
                type: "success"
              });
              this.dialogFormVisible = false;
              this.search(true);
            } else {
              this.$message({
                message: "编辑失败:" + res.message,
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
    submitForm(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          const loading = this.$loading({
            lock: true,
            text: "Loading",
            spinner: "el-icon-loading",
            background: "rgba(0, 0, 0, 0.7)"
          });
          var categoryAttributeList = [];
          var categoryAttributeData = this.tableData || [];
          (this.formModel.attributeNames || []).map(function(t) {
            if (
              categoryAttributeData.findIndex(a => a.attributeNameId == t) < 0
            ) {
              categoryAttributeList.push({
                AttributeNameId: t,
                IsDeleted: 0
              });
            }
          });
          var data = {
            CategoryId: this.formModel.childCategoryId,
            CategoryAttributes: categoryAttributeList
          };
          categoryAttributeSearchSvc
            .saveCategoryAttribute(data)
            .then(
              res => {
                var data = res.data;
                if (data) {
                  this.$message({
                    message: "保存成功",
                    type: "success"
                  });
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
      // this.$refs[formName].resetFields();
      Object.assign(this.$data.formModel, this.$options.data().formModel);
      this.dialogFormVisible = false;
    },
    //查询主分类
    loadMainCategorys: function() {
      categorySearchSvc
        .getCategorysById({
          categoryId: 0,
          level: 1
        })
        .then(
          res => {
            var data = res.data;
            this.mainCategoryList = data;
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
        .finally(() => {});
    },
    //查询二级分类
    loadSecondCategorys: function(categoryId) {
      if (!categoryId) {
        return;
      }
      this.condition.secondCategoryId = undefined;
      this.condition.childCategoryId = undefined;
      categorySearchSvc
        .getCategorysById({
          categoryId: categoryId,
          level: 2
        })
        .then(
          res => {
            var data = res.data;
            this.secondCategoryList = data;
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
        .finally(() => {});
    },
    //查询三级分类
    loadChildCategorys: function(categoryId) {
      if (!categoryId) {
        return;
      }
      this.condition.childCategoryId = undefined;
      categorySearchSvc
        .getCategorysById({
          categoryId: categoryId,
          level: 3
        })
        .then(
          res => {
            var data = res.data;
            this.childCategoryList = data;
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
        .finally(() => {});
    },
    //加载所有的属性信息
    loadAttributes: function() {
      categoryAttributeSearchSvc
        .getAttributeNames({})
        .then(
          res => {
            var data = res.data;
            this.attributeList = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
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
>>> .el-form-item__content {
  margin-left: 10px !important;
}
>>> .el-col-6 {
  width: 15%;
}
</style>
