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
       <template v-slot:condition prop='unitName'>
<el-form-item>
      <el-input v-model="condition.unitName" autocomplete="off" style="width:160px"   placeholder="请输入单位名称"/>
</el-form-item>

<el-form-item label="是否禁用" style="margin-left:10px;" prop="IsForbid"> 

      <el-select v-model="condition.IsForbid" placeholder="请选择是否禁用">
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
  
      <el-table-column v-if="false" label="id" prop="id" />
      <el-table-column label="单位名称" prop="unitName" />
      <el-table-column label="是否禁用" prop="isDeleted">
        <template slot-scope="scope">
          <el-tag
            :type="scope.row.isDeleted === 0 ? '' : 'danger'"
          >{{ scope.row.isDeleted === 1 ? '是' : '否' }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="200" fixed="right">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
        </template>
      </el-table-column>
        </template>

   </rg-page>
         <rg-dialog
        :title="dialogtitle"
        :visible.sync="dialogFormVisible"
        v-if="dialogFormVisible"
        :footbar="true"
        :beforeClose="cancelForm"
        :btnCancel="{label:'关闭',click:(done)=>{cancelForm('formModel')}}"
        maxWidth="1024px"
        minWidth="900px"
      >
        <template v-slot:content>
    <!-- <el-dialog :title="dialogtitle" :visible.sync="dialogFormVisible"> -->
      <el-form ref="formModel" :model="formModel" :rules="rules">
        <el-form-item label="单位名称" :label-width="formLabelWidth" prop="unitName">
          <el-input v-model="formModel.unitName" autocomplete="off" />
        </el-form-item>
        <el-form-item v-if="!formModel.isAdd" label="是否禁用" :label-width="formLabelWidth">
          <el-radio-group v-model="formModel.IsForbid">
            <el-radio label="是" value="1" />
            <el-radio label="否" value="0" />
          </el-radio-group>
        </el-form-item>
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
import { unitSearchSvc } from '@/api/product/unitsearch'
export default {
  name: 'UnitManage',
  data() {
    return {
      tableData: [],
      tableLoading: false,
      options: [
        {
          value: '-1',
          label: '全部'
        },
        {
          value: '1',
          label: '是'
        },
        {
          value: '0',
          label: '否'
        }
      ],
      dialogFormVisible: false,
      formModel: {
        unitName: '',
        IsForbid: '否',
        id: 0,
        isAdd: true
      },
      formLabelWidth: '100px',
      condition: {
        unitName: '',
        IsForbid: '-1',
        pageIndex: 1,
        pageSize: 20
      },
      totalCount: 0,
      dialogtitle: '新增',
      rules: {
        unitName: [
          {
            required: true,
            message: '请输入单位名称！',
            trigger: 'blur'
          },
          {
            min: 1,
            max: 90,
            message: '长度应该是1到90!',
            trigger: 'blur'
          }
        ]
      }
    }
  },
  created() {
    this.getPageData()
  },
  methods: {
    getPageData() {
      this.tableLoading = true
      unitSearchSvc
        .GetProductUnitList(this.condition)
        .then(
          res => {
            var data = res.data
            this.tableData = data.items
            this.totalCount = data.totalItems
            console.log(data)
            if (res.code !== 10000) {
              this.$message({
                message: res.message || '查询失败',
                type: 'error'
              })
            }
          },
          error => {
            console.log(error)
          }
        )
        .catch(() => { })
        .finally(() => {
          this.tableLoading = false
        })
    },
    handleAdd() {
      this.dialogtitle = '新增'
      this.formModel.isAdd = true
      this.resetData()
      this.dialogFormVisible = true
    },
    search() {
      this.condition.pageIndex = 1
      this.getPageData()
    },
    handleEdit(index, row) {
      this.dialogtitle = '编辑'
      this.formModel.isAdd = false
      this.resetData()
      this.dialogFormVisible = true
      this.formModel.unitName = row.unitName
      this.formModel.IsForbid = row.isDeleted === 1 ? '是' : '否'
      this.formModel.id = row.id
      console.log(index, row)
    },
    resetData() {
      this.formModel.unitName = ''
      this.formModel.IsForbid = '否'
    },
    submitForm(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          this.dialogFormVisible = false
          var params = {
            unitName: this.formModel.unitName,
            isForbid: this.formModel.IsForbid === '是' ? 1 : 0,
            id: this.formModel.id
          }
          // eslint-disable-next-line no-empty
          if (this.formModel.isAdd) {
            unitSearchSvc
              .AddUnit(params)
              .then(
                res => {
                  console.log(res.code)
                  if (res.code !== 10000) {
                    this.$message({
                      message: res.message || '添加失败',
                      type: 'error'
                    })
                  }
                },
                error => {
                  console.log(error)
                }
              )
              .catch(() => { })
              .finally(() => {
                this.tableLoading = false
                this.getPageData()
              })
          } else {
            unitSearchSvc
              .EditUnit(params)
              .then(
                res => {
                  console.log(res.code)
                  if (res.code !== 10000) {
                    this.$message({
                      message: res.message || '编辑失败',
                      type: 'error'
                    })
                  }
                },
                error => {
                  console.log(error)
                }
              )
              .catch(() => { })
              .finally(() => {
                this.tableLoading = false
                this.getPageData()
              })
          }

          this.resetData()
          this.$refs[formName].resetFields()
        } else {
          console.log('error submit!!')
          return false
        }
      })
    },
    cancelForm(formName) {
      this.dialogFormVisible = false
      this.resetData()
      this.$refs[formName].resetFields()
    },
    //分页
        pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPageData();
    },
    // 分页
    handleSizeChange(val) {
      this.condition.pageIndex = 1
      this.condition.pageSize = val
      this.getPageData()
    },
    handleCurrentChange(val) {
      this.getPageData()
      console.log(`current page: ${val}`)
    }
  }
}
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
  .input-label {
    display: inline-block;
    width: 100px;
    margin-left: 40px;
  }
}
</style>
