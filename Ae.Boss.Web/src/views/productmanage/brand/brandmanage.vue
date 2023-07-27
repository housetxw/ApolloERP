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
      <!-- <span class="input-label">品牌名称：</span> -->
           <el-form-item >
      <el-input v-model="condition.brandName" autocomplete="off" style="width:160px"  placeholder="请输入品牌名称"/>
           </el-form-item>
                  <el-form-item label="是否禁用" style="margin-left:10px;">
     
      <el-select v-model="condition.IsForbid"  placeholder="请选择是否禁用">
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
      <el-table-column label="品牌名称" prop="brandName" />
      <el-table-column label="是否禁用" prop="isDeleted">
        <template slot-scope="scope">
          <el-tag
            :type="scope.row.isDeleted === 0 ? '' : 'danger'"
          >{{ scope.row.isDeleted === 1 ? '是' : '否' }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="200">
        <template slot-scope="scope">
          <el-button size="mini" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
        </template>
      </el-table-column>
  </template>

    <!-- <div class="pagination">
      <el-pagination
        background
        :current-page.sync="condition.pageIndex"
        :page-sizes="[20, 40, 60, 80]"
        :page-size="20"
        layout="sizes, prev, pager, next"
        :total="totalCount"
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
      />
    </div> -->
      </rg-page>
    <!-- <el-dialog :title="dialogtitle" :visible.sync="dialogFormVisible"> -->
        <rg-dialog
        :title="dialogtitle"
        :visible.sync="dialogFormVisible"
        v-if="dialogFormVisible"
        :footbar="true"
        :beforeClose="cancelForm"
        :btnCancel="{label:'关闭',click:(done)=>{cancelForm('formModel')}}"
        maxWidth="900px"
        minWidth="600px"
      >
        <template v-slot:content>
      <el-form ref="formModel" :model="formModel" :rules="rules">
        <el-form-item label="品牌名称" :label-width="formLabelWidth" prop="brandName">
          <el-input v-model="formModel.brandName" autocomplete="off"  />

        </el-form-item>
        <el-form-item v-if="!formModel.isAdd" label="是否禁用" :label-width="formLabelWidth">
          <el-radio-group v-model="formModel.IsForbid">
            <el-radio label="是" value="1" />
            <el-radio label="否" value="0" />
          </el-radio-group>
        </el-form-item>
        <el-form-item label="品牌LOGO" :label-width="formLabelWidth">
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
          >
            <el-button size="small" type="primary">上传图片</el-button>
            <div slot="tip" class="el-upload__tip">大小小于500kb的jpg/png文件</div>
          </el-upload>
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
      <!-- <div slot="footer" class="dialog-footer">
        <el-button @click="cancelForm('formModel')">取 消</el-button>
        <el-button type="primary" @click="submitForm('formModel')">确 定</el-button>
      </div> -->
    </rg-dialog>
  </div>
</template>

<script>
import { upload } from '@/utils/uploadfile'
import { appSvc } from '@/api/qiniu/qiniuservice'
import { brandSearchSvc } from '@/api/product/brandsearch'
export default {
  name: 'BrandManage',
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
        brandName: '',
        barandImage: '',
        IsForbid: '否',
        id: 0,
        isAdd: true
      },
      formLabelWidth: '100px',
      fileList: [],
      condition: {
        brandName: '',
        IsForbid: '-1',
        pageIndex: 1,
        pageSize: 20
      },
      totalCount: 0,
      dialogtitle: '新增',
      rules: {
        brandName: [
          {
            required: true,
            message: '请输入品牌名称！',
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
      brandSearchSvc
        .getProductBrandList(this.condition)
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
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    handleEdit(index, row) {
      this.dialogtitle = '编辑'
      this.formModel.isAdd = false
      console.log(row.brandName)
      this.resetData()
      this.dialogFormVisible = true
      this.formModel.brandName = row.brandName
      this.formModel.IsForbid = row.isDeleted === 1 ? '是' : '否'
      this.formModel.id = row.id
      if (row.brandImg.trim().length > 0) {
        this.fileList.push({ name: row.brandName, url: row.brandImg })
      }

      console.log(index, row)
    },
    resetData() {
      this.formModel.brandName = ''
      this.formModel.barandImage = ''
      this.formModel.IsForbid = '否'
      this.fileList = []
    },
    submitForm(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          this.dialogFormVisible = false
          var url = ''
          if (this.fileList.length > 0) {
            this.formModel.brandImg = this.fileList[0].url
            url = this.fileList[0].url
          }
          var params = {
            brandName: this.formModel.brandName,
            brandImg: url,
            isForbid: this.formModel.IsForbid === '是' ? 1 : 0,
            id: this.formModel.id
          }
          // eslint-disable-next-line no-empty
          if (this.formModel.isAdd) {
            console.log(1111)
            brandSearchSvc
              .AddBrand(params)
              .then(
                res => {
                  var data = res.data
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
            brandSearchSvc
              .EditBrand(params)
              .then(
                res => {
                  var data = res.data
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
    // 图片上传--start ----
    handleRemove(file, fileList) {
      // console.log(file, fileList);
    },
    handlePreview(file) {
      // console.log(file);
    },
    handleExceed(files, fileList) {
      this.$message.warning('限制是1个!')
    },
    beforeRemove(file, fileList) {
      return this.$confirm(`确认删除?`)
    },
    // 获取token
    getToken(directoryName, fileName) {
      return appSvc.getQiNiuToken({
        directory: directoryName,
        fileName: fileName
      })
    },
    uploadRequest: function(request) {
      var fileName = request.file.name
      var directoryName = 'productImage'
      var key = directoryName + '/' + fileName
      this.getToken(directoryName, fileName).then(res => {
        var result = res.data
        const token = result.token
        const host = result.host
        upload(
          token,
          key,
          request,
          next => {
            // eslint-disable-next-line no-unused-vars
            const total = next.total
          },
          // eslint-disable-next-line handle-callback-err
          error => {
            this.fileList = []
          },
          complete => {
            // eslint-disable-next-line no-unused-vars
            const hash = complete.hash
            const key = complete.key
            var url = host + '/' + key
            this.fileList = [{ name: fileName, url: url }]
            console.log('url' + url)
          }
        )
      })
    },
    // 图片上传--end ----
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
