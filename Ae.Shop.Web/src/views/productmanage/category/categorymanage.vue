<template>
  <div class="app-container">
    <el-form :inline="true">
      <el-form-item >
        <el-input
          v-model="queryParams.displayName"
          placeholder="请输入类目名称"
          clearable
          size="mini"
          @keyup.enter.native="handleQuery"
        />
      </el-form-item>
      <el-form-item>
        <el-button type="warning" icon="el-icon-search" size="mini"  @click="handleQuery">搜索</el-button>
        <el-button type="primary" icon="el-icon-plus" size="mini" @click="handleAdd">新增</el-button>
      </el-form-item>
    </el-form>
    <el-table
      v-loading="loading"
      :data="categoryList"
      row-key="id"
      :tree-props="{children: 'children', hasChildren: 'hasChildren'} "
      size="small"
    >
      <el-table-column prop="displayName" label="类目名称" :show-overflow-tooltip="true" />
      <el-table-column prop="id" label="ID" :show-overflow-tooltip="true" />
      <el-table-column prop="categoryCode" label="类目编号" :show-overflow-tooltip="true" />
      <el-table-column prop="categoryCodeShort" label="编号缩写" :show-overflow-tooltip="true" />
      <el-table-column label="操作" align="center" class-name="small-padding fixed-width">
        <template slot-scope="scope">
          <el-button
            size="mini"
            type="text"
            icon="el-icon-edit"
            @click="handleUpdate(scope.row)"
          >修改</el-button>
          <el-button
            size="mini"
            type="text"
            icon="el-icon-plus"
            @click="handleAdd(scope.row)"
          >新增</el-button>
          <el-button
            v-if="scope.row.parentId != 0"
            size="mini"
            type="text"
            icon="el-icon-delete"
            @click="handleDelete(scope.row)"
          >删除</el-button>
        </template>
      </el-table-column>
      <el-table-column prop="description" label="类目描述" :show-overflow-tooltip="true" />
    </el-table>

    <!-- 添加或修改类目对话框 -->
      <rg-dialog
      :title="title"
      :visible.sync="open"
      v-if="open"
      :footbar="true"
      :beforeClose="cancel"
      :btnCancel="{label:'关闭',click:(done)=>{cancel('formModel')}}"
      maxWidth="800px"
      minWidth="600px"
    >
      <template v-slot:content>
    <!-- <el-dialog :title="title" :visible.sync="open" width="600px"> -->
      <el-form ref="form" :model="form" :rules="rules" label-width="80px">
        <el-row>
          <el-col v-if="form.parentId !== 0" :span="20">
            <el-form-item label="上级类目" prop="parentId" :label-width="formLabelWidth">
              <treeselect
                v-model="form.parentId"
                :options="categoryOptions"
                :show-count="true"
                placeholder="选择上级类目"
                :disabled="form.id > 0"
              />
            </el-form-item>
          </el-col>
          <el-col :span="20">
            <el-form-item label="类目编号" prop="categoryCode" :label-width="formLabelWidth">
              <el-input v-model="form.categoryCode" placeholder="请输入类目编号" />
            </el-form-item>
          </el-col>
          <el-col :span="20">
            <el-form-item label="编号缩写" prop="categoryCodeShort" :label-width="formLabelWidth">
              <el-input v-model="form.categoryCodeShort" placeholder="请输入编号缩写" />
            </el-form-item>
          </el-col>
          <el-col :span="20">
            <el-form-item label="类目名称" prop="displayName" :label-width="formLabelWidth">
              <el-input v-model="form.displayName" placeholder="请输入类目名称" />
            </el-form-item>
          </el-col>
          <el-col :span="20">
            <el-form-item label="类目描述" prop="description" :label-width="formLabelWidth">
              <el-input v-model="form.description" placeholder="请选填类目描述" />
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
import { proudctConfigSvc } from '@/api/product/categorymanage'
import Treeselect from '@riophae/vue-treeselect'
import '@riophae/vue-treeselect/dist/vue-treeselect.css'

export default {
  components: { Treeselect },
  data() {
    return {
      // 遮罩层
      loading: true,
      // 类目表格树数据
      categoryList: [],
      // 类目树选项
      categoryOptions: undefined,
      // 弹出层标题
      title: '',
      // 是否显示弹出层
      open: false,
      // 类目状态数据字典
      visibleOptions: [],
      // 查询参数
      queryParams: {
        displayName: undefined
      },
      // 表单参数
      form: {},
      // 表单校验
      rules: {
        // parentId: [
        //   { required: true, message: '父级类目不能为空', trigger: 'blur' }
        // ],
        categoryCode: [
          { required: true, message: '类目编号不能为空', trigger: 'blur' }
        ],
        categoryCodeShort: [
          { required: true, message: '编号缩写不能为空', trigger: 'blur' }
        ],
        displayName: [
          { required: true, message: '类目名称不能为空', trigger: 'blur' }
        ]
      }
    }
  },
  created() {
    this.getList()
  },
  methods: {
    /** 查询类目列表 */
    getList() {
      this.loading = true
      proudctConfigSvc.listCategory(this.queryParams).then(response => {
        this.$message.success(response.message)
        this.categoryList = response.data
        this.loading = false
      })
    },
    /** 查询类目下拉树结构 */
    getTreeSelect() {
      proudctConfigSvc.treeSelect().then(response => {
        this.categoryOptions = response.data
      })
    },
    // 取消按钮
    cancel() {
      this.open = false
      this.reset()
    },
    // 表单重置
    reset() {
      this.form = {
        id: undefined,
        parentId: undefined,
        categoryCode: '',
        categoryCodeShort: '',
        displayName: ''
      }
      if (this.$refs['form'] !== undefined) {
        this.$refs['form'].resetFields()
      }
    },
    /** 搜索按钮操作 */
    handleQuery() {
      this.getList()
    },
    /** 新增按钮操作 */
    handleAdd(row) {
      this.reset()
      this.getTreeSelect()
      if (row != null) {
        this.form.parentId = row.id
      }
      this.open = true
      this.title = '添加类目'
    },
    /** 修改按钮操作 */
    handleUpdate(row) {
      this.reset()
      this.getTreeSelect()
      proudctConfigSvc.getCategory(row.id).then(response => {
        this.form = response.data
        this.open = true
        this.title = '修改类目'
      })
    },
    /** 提交按钮 */
    submitForm: function() {
      this.$refs['form'].validate(valid => {
        if (valid) {
          if (this.form.id !== undefined) {
            proudctConfigSvc.updateCategory(this.form).then(response => {
              if (response.code === 10000) {
                this.$message.success(response.message)
                this.open = false
                this.getList()
              } else {
                this.$message.error(response.message)
              }
            })
          } else {
            proudctConfigSvc.addCategory(this.form).then(response => {
              if (response.code === 10000) {
                this.$message.success(response.message)
                this.open = false
                this.getList()
              } else {
                this.$message.error(response.message)
              }
            })
          }
        }
      })
    },
    /** 删除按钮操作 */
    handleDelete(row) {
      var tempThis = this
      this.$confirm('是否确认删除名称为"' + row.displayName + '"的数据项?', '警告', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(function() {
        proudctConfigSvc.deleteCategory(row.id).then(response => {
          if (response.code === 10000) {
            tempThis.$message.success(response.message)
            tempThis.getList()
          } else {
            tempThis.$message.error(response.message)
          }
        })
      }).then(() => {
        this.getList()
      }).catch(function() {})
    }
  }
}
</script>
