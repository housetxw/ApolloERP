<template>
  <main class="container">
          <el-divider content-position="left">
              <h3>新增服务</h3>
            </el-divider>

    <div class="content">
      <div>
        <el-form ref="form" :model="service2" :inline="true">
          <el-form-item label="选择类型：">
            <el-radio-group v-model="serviceList" @change="changeHandler">
              <el-radio
                v-for="(item,index) in service"
                :label="3*(index+1)"
                :key="index"
                @change="tables(item.id)"
              >{{item.name}}
              </el-radio>
            </el-radio-group>
          </el-form-item>
          <!-- <el-form ref="service2" :model="service2" :inline="true" :rules="rules1">
            <el-form-item label="新增大类名称：" prop="name">
              <el-input v-model="service2.name" size="small" required="required"></el-input>
            </el-form-item>
            <el-form-item label="英文标识：" prop="code">
              <el-input v-model="service2.code" size="small" required="required"></el-input>
            </el-form-item>
            <el-form-item>
              <el-button type="warning" size="mini" @click="addService(service2)">添加大类</el-button>
            </el-form-item>
          </el-form> -->
        </el-form>

        <div class="xiaolei">
          <el-form
            :inline="true"
            :model="tableData3"
            class="demo-form-inline"
            ref="tableData3"
            :rules="rules"
          >
            <el-form-item label="服务名称" prop="name" label-width="100px">
              <el-input v-model="tableData3.name" size="mini"></el-input>
            </el-form-item>
            <el-form-item label="服务产品编号" prop="productId" label-width="100px">
              <el-input v-model="tableData3.productId" size="mini"></el-input>
            </el-form-item>

            <el-form-item label="结算价格" prop="defaultCostPrice" label-width="100px">
              <el-input v-model.number="tableData3.defaultCostPrice" size="mini"></el-input>
            </el-form-item>
            <br/>
            <el-form-item label="结算上限" prop="defaultCostPriceLimit" label-width="100px"> 
              <el-input
                required="required"
                v-model.number="tableData3.defaultCostPriceLimit"
                size="mini"
              ></el-input>
            </el-form-item>

            <el-form-item label="销售价格" prop="defaultSalePrice" label-width="100px">
              <el-input v-model.number="tableData3.defaultSalePrice" size="mini"></el-input>
            </el-form-item>
            <el-form-item label="销售上限" prop="defaultSalePriceLimit" label-width="100px">
              <el-input v-model.number="tableData3.defaultSalePriceLimit" size="mini"></el-input>
            </el-form-item>

            <br/>
            <el-form-item label="服务说明" label-width="100px">         
              <el-input type="textarea" v-model="tableData3.serviceRemark" style="width:420px"></el-input>&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;
              <!-- <el-form-item label="是否可用：">
                <el-checkbox v-model="checkList.no4"></el-checkbox>
                <el-button type="warning" size="mini" @click="addTable(tableData3)">添加服务</el-button>
              </el-form-item> -->
              <el-button type="warning" size="mini" @click="addTable(tableData3)">添加服务</el-button>
              <br/>
            </el-form-item>
          </el-form>
        </div>
        <div class="mainTable">
        
  <el-divider content-position="left">
              <h3>基础服务列表</h3>
            </el-divider>
          <el-table
            :data="tableData"
            style="width: 100%; "
            :row-class-name="tableRowClassName"
            v-loading="tableLoading"
          >
            <el-table-column prop="id" label="编号" width="60" height="53"></el-table-column>
            <el-table-column prop="name" label="服务名称" width="267" height="53"></el-table-column>
            <el-table-column prop="productId" label="服务产品编号" width="165" height="53"></el-table-column>
            <el-table-column prop="defaultSalePrice" label="销售价格" width="74" class="te" height="53"></el-table-column>
            <el-table-column prop="defaultSalePriceLimit" label="销售上限" width="70" height="53"></el-table-column>
            <el-table-column prop="defaultCostPrice" label="结算价格" width="74" class="te" height="53"></el-table-column>
            <el-table-column prop="defaultCostPriceLimit" label="结算上限" width="70" height="53"></el-table-column>
            <el-table-column prop="createTime" label="创建时间" width="110" height="53"></el-table-column>
            <el-table-column label="最后更新日期" width="110" height="53">
                <template slot-scope="scope">
                  <p v-if="Date.parse(scope.row.updateTime) > Date.parse('1900-01-01')">
                      {{scope.row.updateTime}}
                  </p>
              </template>
            </el-table-column>
            <el-table-column label="操作" width="80" height="53">
              <template slot-scope="scope">
                <el-button type="text" @click="editTable(scope.row)">编辑</el-button>
              </template>
            </el-table-column>
          </el-table>
        </div>
        <!-- 编辑弹框 -->
        <div class="dialogs">
          <el-dialog title="编辑服务" :visible.sync="dialogFormVisible" :close-on-click-modal="false">
            <div style="margin-top:18px ;">
              <el-form
                :inline="true"
                :model="tableData2"
                class="demo-form-inline"
                ref="tableData"
                :rules="rules"
              >
                <el-form-item label="服务名称:" prop="name">
                  <el-input v-model="tableData2.name" size="mini"></el-input>
                </el-form-item>
                <el-form-item label="产品ID:" prop="productId">
                  <el-input v-model="tableData2.productId" size="mini"></el-input>
                </el-form-item>

                <el-form-item label="结算价格:" prop="defaultCostPrice">
                  <el-input v-model.number="tableData2.defaultCostPrice" size="mini"></el-input>
                </el-form-item>
                <el-form-item label="结算上限:" prop="defaultCostPriceLimit">
                  <el-input v-model.number="tableData2.defaultCostPriceLimit" size="mini"></el-input>
                </el-form-item>
                <el-form-item label="销售价格:" prop="defaultSalePrice">
                  <el-input v-model.number="tableData2.defaultSalePrice" size="mini"></el-input>
                </el-form-item>
                <el-form-item label="销售上限:" prop="defaultSalePriceLimit">
                  <el-input v-model.number="tableData2.defaultSalePriceLimit" size="mini"></el-input>
                </el-form-item>
                <br/>
                <el-form-item>
                  <span style="color: #606266;" prop="name">服务说明：</span>
                  <br/>
                  <el-input type="textarea" v-model="form.desc" style="width:420px"></el-input>
                </el-form-item>
              </el-form>
            </div>
            <div slot="footer" class="dialog-footer">
              <el-button @click="dialogFormVisible = false">取 消</el-button>
              <el-button type="primary" @click="save()">确 定</el-button>
            </div>
          </el-dialog>
        </div>
      </div>
    </div>
  </main>
</template>

<script>
  import { shopManageSvc } from '@/api/shopmanage/shopmanage'

  let itemId = ''
  export default {
    data() {
      var checkName = (rule, value, callback) => {
        if (!value) {
          return callback(new Error('不能为空'))
        } else {
          callback()
        }
      }
      var checkNum = (rule, value, callback) => {
        if (!value) {
          return callback(new Error('不能为空'))
        }
        setTimeout(() => {
          if (!Number.isInteger(value)) {
            callback(new Error('请输入数字值'))
          } else {
            if (value < 0) {
              callback(new Error('数值必须大于0'))
            } else {
              callback()
            }
          }
        }, 1000)
      }

      return {
        form: {
          name: '',
          region: '',
          date1: '',
          date2: '',
          delivery: false,
          type: [],
          resource: '',
          desc: ''
        },
        service2: {
          name: '',
          code: ''
        },
        // tableData2:{defaultCostPrice:'',
        //  defaultCostPriceLimit:'',
        // defaultSalePriceLimit:'',
        // defaultSalePrice:'',
        // },
        rules1: {
          name: [
            {
              required: true,
              validator: checkName,
              message: '请输入名称',
              trigger: 'blur'
            }
          ],
          code: [
            {
              required: true,
              validator: checkName,
              message: '请输入英文标识',
              trigger: 'blur'
            }
          ]
        },

        rules: {
          name: [
            {
              required: true,
              validator: checkName,
              message: '请输入名称',
              trigger: 'blur'
            }
          ],
          defaultCostPrice: [
            {
              required: true,
              validator: checkNum,

              trigger: 'blur'
            }
          ],
          productId: [
            {
              required: true,
              validator: checkName,
              message: '请输入ID',
              trigger: 'blur'
            }
          ],

          defaultCostPriceLimit: [
            {
              required: true,

              validator: checkNum,

              trigger: 'blur'
            }
          ],
          defaultSalePriceLimit: [
            {
              required: true,
              validator: checkNum,

              trigger: 'blur'
            }
          ],
          defaultSalePrice: [
            {
              required: true,

              validator: checkNum,
              trigger: 'blur'
            }
          ]
        },
        // radio:'0',
        serviceList: 3,
        // isShow: true,
        dialogFormVisible: false,
        formLabelWidth: '120px',
        checkList: {
          checked: true,
          no1: false,
          no2: false,
          no3: false,
          no4: true
        },
        service2: {},
        service: [],
        // serviceList: {},

        tableData2: [],
        tableData: [{}],
        tableData3: {},
        tableLoading: false
      }
    },
    mounted() {
      this.GetBaseServiceList(0)
      this.tables(1)
    },

    methods: {
      editTable(row) {
        // console.log(999333,this.tableData2)
        this.dialogFormVisible = true;
        shopManageSvc
          .GetBaseServiceInfo({ServiceId:row.id})
          .then(
            res => {
              this.tableData2 = res.data
              console.log('tableData2: ' + JSON.stringify(this.tableData2))
            },
            error => {
              console.log(error)
            }
          )
          .catch(() => {
          })
      },
      changeHandler(item) {
        // console.log(12345,item.value)
        // this.tables()
      },
      //添加大类
      // addService() {
      //   shopManageSvc
      //     .AddBaseServiceCategory({
      //       name: this.service2.name,
      //       code: this.service2.code
      //     })
      //     .then(
      //       res => {
      //         var data = res.data;
      //         this.GetBaseServiceList(0);
      //       },
      //       error => {
      //         console.log(error);
      //       }
      //     )
      //     .catch(() => {});
      // },
      //添加大类以及验证表单提交
      // addService(service2) {
      //   this.$refs.service2.validate((valid) => {
      //     if (valid) {
      //       shopManageSvc
      //         .AddBaseServiceCategory({
      //           name: this.service2.name,
      //           code: this.service2.code
      //         })
      //         .then(
      //           res => {
      //             var data = res.data
      //             this.GetBaseServiceList(0)
      //           },
      //           error => {
      //             console.log(error)
      //           }
      //         )
      //         .catch(() => {
      //         })

      //     } else {

      //       return false
      //     }
      //   })
      // },

      //添加小类以及验证表单提交
      addTable(tableData3) {
        this.$refs.tableData3.validate((valid) => {
          if (valid) {
            shopManageSvc
              .AddBaseService({
                id: this.tableData3.id,
                productId: this.tableData3.productId,
                name: this.tableData3.name,
                defaultSalePrice: this.tableData3.defaultSalePrice,
                defaultCostPrice: this.tableData3.defaultCostPrice,
                defaultSalePriceLimit: this.tableData3.defaultSalePriceLimit,
                defaultCostPriceLimit: this.tableData3.defaultCostPriceLimit,
                categoryId: itemId,
                categoryType: this.tableData3.categoryType,
                serviceRemark: this.tableData3.serviceRemark,
                salePriceRemark: this.tableData3.salePriceRemark,
                createBy: this.tableData3.createBy
              })
              .then(
                res => {
                  var data = res.data
                  this.GetBaseServiceList(itemId)
                },
                error => {
                  console.log(error)
                }
              )
              .catch(() => {
              })
              .finally(() => {
              })
          } else {

            return false
          }
        })
      },
      //编辑
      save(tableData2) {
        this.dialogFormVisible = true
        shopManageSvc
          .ModifyBaseServiceInfo({
            Id: this.tableData2.id,
            ProductId: this.tableData2.productId,
            Name: this.tableData2.name,
            DefaultSalePrice: this.tableData2.defaultSalePrice,
            DefaultCostPrice: this.tableData2.defaultCostPrice,
            DefaultSalePriceLimit: this.tableData2.defaultSalePriceLimit,
            DefaultCostPriceLimit: this.tableData2.defaultCostPriceLimit,
            CategoryId: this.tableData2.categoryId,
            CategoryType: this.tableData2.categoryType,
            ServiceRemark: this.tableData2.serviceRemark,
            SalePriceRemark: this.tableData2.salePriceRemark,
            IsDeleted: this.tableData2.isDeleted,
          })
          .then(
            res => {
              var data = res.data
              this.dialogFormVisible = false
              console.log(9999, this.tableData2.id)
              this.GetBaseServiceList(itemId)
            },
            error => {
              console.log(error)
            }
          )
          .catch(() => {
          })
          .finally(() => {
          })
      },

      // this.formModel = JSON.parse(JSON.stringify(row));

      tableRowClassName({ row, rowIndex }) {
        if (rowIndex === 1) {
          return 'warning-row'
        } else if (rowIndex === 3) {
          return 'success-row'
        }
        return ''
      },
      tables(id) {
        itemId = id
        this.GetBaseServiceList(id)
        console.log(1)
      },

      //获取table表格
      GetBaseServiceList(id) {
        this.tableLoading = true
        shopManageSvc
          .GetBaseServiceList({
            CategoryId: id
          })

          .then(
            res => {
              if (id == 0) {
                var data = res.data
                this.service = data
                console.log(data)
              } else {
                var data = res.data
                this.tableData = data
                console.log(111, this.tableData)
              }
            },
            error => {
              console.log(error)
            }
          )
          .then()
          .catch(() => {
          })
          .finally(() => {
            console.log('tableLoading: false')
            this.tableLoading = false
          })
      }
    }
  }
</script>
<style lang="scss" scoped>
  .container {
    padding: 10px;
    margin: 20px 0 0 20px;
    background: #ffffff;
    border: 1px solid #d9d9d9;

    .title {
      border-bottom: 1px solid #d9d9d9;
      padding: 5px;
      font-size: 14px;
      font-weight: bold;
      line-height: 25px;
      margin-top: 2px;
    }

    .content {
      padding: 10px;
    }
  }

  > > > .el-radio__label {
    font-size: 12px;
    padding: 2px;
  }

  > > > .el-radio {
    margin: 0;
  }

  .xiaolei {
    width: 100%;
    margin-bottom: 15px;
    overflow: hidden;

    overflow-x: auto;
  }

  > > > .input {
    width: 400px;
  }

  > > > .el-form-item__label {
    padding: 0;
  }

  > > > .el-checkbox {
    margin-right: 4px;
  }

  //   >>>.el-table .warning-row {
  //     background: oldlace;
  //   }

  > > > .el-table .success-row {
    background: #f0f9eb;
  }

  > > > .el-table .cell,
  .el-table th div {
    text-align: center;
  }

  > > > .te {
    background-color: #cae1ff !important;
    padding: 1px 0 !important;
  }

  > > > .el-table th,
  .el-table tr {
    text-align: center;
    background: #f5f7fa;
    color: #333;
  }

  > > > .el-table th {
    padding: 0 !important;
  }

  > > > .el-tabs__header {
    margin: 0 0 0px;
  }

  .dialogs {
    max-width: 300px;
  }

  // >>>.el-dialog__wrapper {
  //     position: fixed;
  //     top: 0;
  //     right: 0;
  //     bottom: 0;
  //     left: 0;
  //     overflow: auto;
  //     margin: 0;
  //     width:900px;
  //     padding:0
  // }

  > > > .el-dialog__header {
    padding: 10px 10px 10px;
    background: #eee;
  }
</style>
