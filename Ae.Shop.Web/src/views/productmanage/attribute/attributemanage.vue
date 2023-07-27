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
    >
      <template v-slot:condition>
        <el-form-item prop="key">
          <el-input v-model="condition.key" autocomplete="off" placeholder="请选择属性名称" />
        </el-form-item>
        <el-form-item label="是否禁用" style="margin-left:10px;" prop="isDeleted">
          <el-select v-model="condition.isDeleted" placeholder="请选择是否禁用">
            <el-option
              v-for="item in options"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            />
          </el-select>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="handleAdd">新增</el-button>
      </template>

      <template v-slot:tb_cols>
        <!-- <el-table v-loading="tableLoading" :data="tableData" border style="width: 100%" size="small"> -->
        <el-table-column label="属性编码" prop="attributeName" />
        <el-table-column label="属性名称" prop="displayName" />
        <el-table-column label="备注" prop="description" />
        <el-table-column label="是否禁用" prop="isDisable" width="150">
          <template slot-scope="scope">
            <el-tag
              :type="scope.row.isDeleted === 0 ? '' : 'danger'"
            >{{scope.row.isDeleted === 1 ? '是' : '否'}}</el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="120" align="center" fixed="right">
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
        <el-form ref="formModel" :model="formModel" :rules="rules" label-width="120px">
          <el-row>
            <el-col :span="16">
              <el-form-item label="属性编码" prop="attributeName">
                <el-input v-model="formModel.attributeName" autocomplete="off" size="mini" />
              </el-form-item>
            </el-col>
            <el-col :span="16">
              <el-form-item label="属性名称" prop="displayName">
                <el-input v-model="formModel.displayName" autocomplete="off" size="mini" />
              </el-form-item>
            </el-col>
            <el-col :span="16">
              <el-form-item label="属性类型" prop="attributeType">
                <el-select
                  v-model="formModel.attributeType"
                  filterable
                  placeholder="请选择属性类型"
                  style="width:100%"
                  :disabled="IsEdit"
                  size="mini"
                >
                  <el-option
                    v-for="dict in attributeTypes"
                    :key="dict.value"
                    :label="dict.label"
                    :value="dict.value"
                    size="mini"
                  />
                </el-select>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="16">
              <el-form-item label="控件类型" prop="controlType">
                <el-select
                  v-model="formModel.controlType"
                  filterable
                  placeholder="请选择控件类型"
                  style="width:100%"
                  size="mini"
                >
                  <el-option
                    v-for="dict in controlTypes"
                    :key="dict.value"
                    :label="dict.label"
                    :value="dict.value"
                    size="mini"
                  />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col v-if="formModel.controlType!=0" :span="16">
              <el-form-item label="属性值" prop="attributeArray">
                <el-select
                  v-model="attributeArray"
                  multiple
                  filterable
                  allow-create
                  default-first-option
                  placeholder="请输入你要填写的属性值，按回车键保存"
                  style="width:100%"
                  size="mini"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="8">
              <el-form-item label="数据类型" prop="dataType">
                <el-radio-group
                  v-model="formModel.dataType"
                  :disabled="IsEdit||formModel.controlType>0"
                >
                  <el-radio :label="0">本文</el-radio>
                  <el-radio :label="1">数字</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
            <el-col v-if="formModel.dataType==1" :span="8">
              <el-form-item label="最小值" prop="minValue">
                <el-input
                  v-model="formModel.minValue"
                  type="number"
                  min="0"
                  autocomplete="off"
                  size="mini"
                  @blur="function (){ formModel.minValue = formModel.minValue ? formModel.minValue:0 }"
                />
              </el-form-item>
            </el-col>
            <el-col v-if="formModel.dataType==1" :span="8">
              <el-form-item label="最大值" prop="maxValue">
                <el-input
                  v-model="formModel.maxValue"
                  type="number"
                  min="0"
                  autocomplete="off"
                  @blur="function (){ formModel.maxValue = formModel.maxValue ? formModel.maxValue:0 }"
                />
              </el-form-item>
            </el-col>
            <el-col v-if="formModel.dataType==0" :span="8">
              <el-form-item label="最大长度" prop="maxLength">
                <el-input
                  v-model="formModel.maxLength"
                  type="number"
                  min="0"
                  autocomplete="off"
                  size="mini"
                  :disabled="formModel.controlType>0"
                  @blur="function (){ formModel.maxLength = formModel.maxLength ? formModel.maxLength:0 }"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="8">
              <el-form-item label="是否必须">
                <el-radio-group v-model="formModel.isRequired">
                  <el-radio :label="1">是</el-radio>
                  <el-radio :label="0">否</el-radio>
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
            <el-col :span="16">
              <el-form-item label="备注" prop="description">
                <el-input
                  v-model="formModel.description"
                  type="textarea"
                  :rows="3"
                  autocomplete="off"
                  size="mini"
                />
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
import { attributesearchSvc } from "@/api/product/attributesearch";
import { productUtil } from "@/api/product/common";

export default {
  name: "attributemanage",
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
        isDeleted: 0,
        pageIndex: 1,
        pageSize: 15
      },
      totalCount: 0,
      rules: {
        attributeName: [
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
        ]
      },
      controlTypes: [
        {
          value: 0,
          label: "Input 输入框"
        },
        {
          value: 1,
          label: "Select 选择器"
        },
        {
          value: 2,
          label: "Multiple Select 选择器"
        }
      ],
      attributeTypes: [
        {
          value: 0,
          label: "通用属性"
        },
        {
          value: 1,
          label: "普通属性"
        },
        {
          value: 2,
          label: "销售属性"
        }
      ],
      formModel: {
        attributeName: "",
        displayName: "",
        controlType: 0,
        attributeType: 0,
        dataType: 0,
        minValue: 0,
        maxValue: 0,
        maxLength: 0,
        isDeleted: 0,
        isRequired: 0,
        description: ""
      },
      attributeArray: [],
      attributeValues: [],
      attributeValuesOld: [],
      IsEdit: false
    };
  },
  created() {
    this.getPageData(false);
  },
  methods: {
    getPageData(flag) {
      this.tableLoading = true;
      attributesearchSvc
        .searchAttribute(this.condition)
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
    //分页
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPageData();
    },
    handleAdd() {
      this.dialogFormVisible = true;
      this.IsEdit = false;
      this.attributeArray = [];
    },
    handleEdit(index, row) {
      this.tableLoading = true;
      var attributeNameId = row.id;
      this.IsEdit = true;
      attributesearchSvc
        .getAttributeNameById({ attributeNameId: attributeNameId })
        .then(
          res => {
            var data = res.data;
            this.formModel = data.attributeName;
            this.attributeValues = data.attributeValues;
            this.attributeValuesOld = productUtil.copyDeep(
              data.attributeValues
            );
            this.attributeArray = data.attributeValues.map(function(t) {
              return t.attributeValue;
            });
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
          const loading = this.$loading({
            lock: true,
            text: "Loading",
            spinner: "el-icon-loading",
            background: "rgba(0, 0, 0, 0.7)"
          });
          var attributeData = this.attributeValues || [];
          var attributeArrayNew = this.attributeArray || [];
          attributeArrayNew.map(function(t) {
            var index = attributeData.findIndex(v => v.attributeValue == t);
            if (index < 0) {
              attributeData.push({
                attributeValue: t,
                isDeleted: 0,
                id: 0
              });
            }
          });
          (attributeData || []).map(function(t) {
            var index = attributeArrayNew.findIndex(a => a == t.attributeValue);
            if (index < 0) {
              t.IsDeleted = 1;
            }
          });
          var attributeValueList = productUtil.checkData(
            attributeData,
            this.attributeValuesOld,
            ["AttributeValue"]
          );
          var data = {
            AttributeName: this.formModel,
            IsEdit: this.formModel.id > 0,
            AttributeValues: attributeValueList
          };
          attributesearchSvc
            .saveAttribute(data)
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
    cancelForm(formModel) {
      this.dialogFormVisible = false;
      // this.$refs[formName].resetFields();
      Object.assign(this.$data.formModel, this.$options.data().formModel);
    }
  }
};
</script>
<style lang="scss" scoped>
.container {
  margin-left: 20px;
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
</style>
