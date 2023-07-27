<template>
  <main class="container">
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :headerBtnWidth="160"
      :tableData="tableData"
      :pageChange="pageChange"
      :searching="search"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="车牌"
            style="width:150px"
            prefix-icon="el-icon-search"
            v-model="condition.carNumber"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="用车技师或手机号"
            style="width:150px"
            prefix-icon="el-icon-search"
            v-model="condition.mobile"
          ></el-input>
        </el-form-item>
        <el-form-item prop="installTime">
          <el-tooltip class="item" effect="dark" content="用车日期" placement="bottom">
            <el-date-picker
              v-model="condition.installTime"
              type="daterange"
              range-separator="-"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              :picker-options="$root.$data.timeRgPickerOpt"
              style="width:220px;"
            ></el-date-picker>
          </el-tooltip>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.status"
            style="width:120px"
            placeholder="请选择状态"
          >
            <el-option
              v-for="item in statusList"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>

      <template v-slot:header_btn>
        <!-- <el-button type="primary" icon="el-icon-plus" @click="add">新增记录</el-button> -->
      </template>
      <!-- 搜索 -->
      <!-- 数据表格 -->
      <template v-slot:tb_cols>
        <el-table-column label="车牌号" prop="carNumber" min-width="15"></el-table-column>
        <el-table-column label="用车技师" prop="vehicleCar" min-width="30">
          <template
            slot-scope="scope"
          >{{scope.row.name}} {{scope.row.mobile?' - '+scope.row.mobile:""}}</template>
        </el-table-column>
        <el-table-column label="用车时间" prop="startTime" min-width="30" align="center">
          <template slot-scope="scope">{{$jscom.toYMDHm(scope.row.startTime) }}</template>
        </el-table-column>
        <el-table-column label="还车时间" prop="returnTime" min-width="30" align="center">
          <template slot-scope="scope">{{$jscom.toYMDHm(scope.row.returnTime) }}</template>
        </el-table-column> 
        <el-table-column label="状态" prop="statusName" align="center" width="80"></el-table-column>
        <el-table-column label="备注" prop="remark" align="center" width="200"></el-table-column>
<!-- 
        <el-table-column label="开始里程数" prop="startMileage" min-width="10" align="center"></el-table-column>
        <el-table-column label="结束里程数" prop="endMileage" min-width="10" align="center"></el-table-column>
        <el-table-column label="油耗（升）" prop="oilWear" min-width="10" align="center"></el-table-column>
        
        <el-table-column label="创建者" prop="createBy" min-width="10" align="center"></el-table-column>
        <el-table-column label="创建时间" prop="createTime" min-width="30" align="center"></el-table-column>
        <el-table-column label="最后更新者" prop="updateBy" min-width="10" align="center"></el-table-column>
        <el-table-column label="最后更新时间" prop="updateTime" min-width="30" align="center"></el-table-column>
         -->
        <el-table-column label="操作" align="center" fixed="right" width="90">
          <template slot-scope="scope">
            <el-button-group>
              <!-- <el-button type="primary" size="mini" v-show="scope.row.isDeleted == 0?true:false" @click="edit(scope.row)">编辑</el-button> -->
              <el-button type="primary" size="mini" v-show="true" @click="edit(scope.row)">详情</el-button>
            </el-button-group>
          </template>
        </el-table-column>
      </template>
    </rg-page>
    <!-- 首页：列表数据 -->
    <!-- 新增和编辑页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        :title="formTitle"
        :visible.sync="formVisible"
        :beforeClose="cancel"
        :btnCancel="{label:'关闭',click:(done)=>{cancel('formModel')}}"
        v-if="formVisible"
        :footbar="true"
        :btnRemove="{label:'删除',disabled:true,show:true,click:(done)=>{deleteShopCarRecordStatus()}}"
        width="90%"
        maxWidth="1024px"
        minWidth="900px"
      >
        <template v-slot:content>
          <el-form
            :model="formModel"
            :rules="rules"
            ref="formModel"
            size="mini"
            :label-width="formLabelWidth"
          >
            <el-divider content-position="left">
              <h3>基本信息</h3>
            </el-divider>
            <el-row>
              <el-form-item label="车牌" prop="carNumber">
                <el-col :span="6">
                    <el-input v-model="formModel.carNumber" :disabled="formCheck&&formCheckEdit"></el-input>               
                </el-col>
                <el-col :span="12">   
                    <el-input v-model="formModel.carVehicle" :disabled="formCheck&&formCheckEdit"></el-input>              
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="用车技师" prop="brand">
                <el-col :span="6">
                    <el-input v-model="formModel.name" :disabled="formCheck&&formCheckEdit"></el-input>                      
                </el-col>
                <el-col :span="6">   
                    <el-input v-model="formModel.mobile" :disabled="formCheck&&formCheckEdit"></el-input>                   
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="订单号">
                <el-col :span="6">
                  <el-input v-model="formModel.orderNo" placeholder="订单号" clearable> </el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="状态" prop="status">
                <el-col :span="6">                  
                  <el-select
                    v-model="formModel.status"
                    clearable
                    style="width:120px"
                    placeholder=""
                  >
                    <el-option
                      v-for="item in statusList"
                      :key="item.key"
                      :label="item.value"
                      :value="item.key"
                    ></el-option>
                  </el-select>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
            <el-form-item label="用车时间" >
              <el-col :span="6">
                <el-date-picker
                  v-model="formModel.startTime"
                  type="datetime"
                  placeholder="开始时间"
                  format="yyyy-MM-dd HH:mm"
                  class="no-radius-right"
                  value-format="yyyy-MM-dd  HH:mm"
                  clearable
                />
              </el-col>
              <el-col :span="6">
                <el-date-picker
                  v-model="formModel.returnTime"
                  type="datetime"
                  class="no-radius-left"
                  placeholder="结束时间"
                  format="yyyy-MM-dd HH:mm"
                  value-format="yyyy-MM-dd HH:mm"
                  clearable
                />
              </el-col>
            </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="量程区间">
                <el-col :span="6">
                  <el-input v-model="formModel.startMileage" placeholder="开始量程读数" clearable>
                      <template slot="append">公里</template>
                  </el-input>
                </el-col>             
                <el-col :span="6">
                  <el-input v-model="formModel.endMileage" placeholder="结束量程读数" clearable>
                      <template slot="append">公里</template>
                  </el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="油格区间">
                <el-col :span="6">
                  <el-input v-model="formModel.startOil" placeholder="油格数" clearable>
                      <template slot="append">格</template>
                  </el-input>
                </el-col>
                <el-col :span="6">
                  <el-input v-model="formModel.endOil" placeholder="油格数" clearable>
                      <template slot="append">格</template>
                  </el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="当日加油">
                <el-col :span="6">
                  <el-input v-model="formModel.refuelled" placeholder="加油" clearable>
                      <template slot="append">升</template>
                  </el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="备注">
                <el-col :span="18">
                  <el-input v-model="formModel.remark" placeholder="备注" clearable> </el-input>
                </el-col>
              </el-form-item>
            </el-row>

            <el-row>
              <el-form-item label="创建人">
                <el-col :span="6">
                  <el-input v-model="formModel.createBy" placeholder="" clearable> </el-input>
                </el-col>
              </el-form-item>
            </el-row>

            <el-row>
              <el-form-item label="创建时间">
                <el-col :span="6">
                  <el-input v-model="formModel.createTime" placeholder="" clearable> </el-input>
                </el-col>
              </el-form-item>
            </el-row>

            <el-row>
              <el-form-item label="更新人">
                <el-col :span="6">
                  <el-input v-model="formModel.updateBy" placeholder="" clearable> </el-input>
                </el-col>
              </el-form-item>
            </el-row>

            <el-row>
              <el-form-item label="更新时间">
                <el-col :span="6">
                  <el-input v-model="formModel.updateTime" placeholder="" clearable> </el-input>
                </el-col>
              </el-form-item>
            </el-row>


          </el-form>
        </template>
        <template v-slot:footer>
          <!-- <el-button-group>
            <el-button
              icon="el-icon-check"
              size="mini"
              type="primary"
              :loading="btnSaveLoading"
              @click="save('formModel')"
            >提交</el-button>
          </el-button-group> -->
        </template>
      </rg-dialog>
    </section>
    <!-- 新增和编辑页面 -->
  </main>
</template>


<script>
import { mapGetters } from "vuex";
import { Loading } from "element-ui";
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { shopManageSvc } from "@/api/shopmanage/shopmanage";
import { employeeSvc } from "@/api/accountauthority/employee";
import { comBusSvc, empSvc } from "@/api/accountauthority/employee";
export default {
  name: "techtriprecord",
  data() {
    return {
      w_search_right: 100,
      tableLoading: false,
      flag: this.global.deletedFlag,
      btnSaveVisible: "",
      btnStatusVisible: "",
      currentPage: 1,
      tableData: [],
      totalList: 0,
      condition: {
        pageIndex: 1,
        pageSize: 20,
        carNumber: undefined,
        mobile: undefined,
        installTime: ["", ""],
        startTime: undefined,
        returnTime: undefined,
        status: -1
      },
      formVisible: false,
      formTitle: "详情",
      formCheck: false,
      btnSaveLoading: false,
      formInit: {
        id: 0,
        carNumber: "",
        carId: 0,
        carVehicle : "",
        orderNo : "",
        name: "",
        mobile: "",
        startTime: "",
        returnTime: "",
        oilWear: 0.0,
        startMileage: 0,
        startMileageImg : "",
        endMileage : 0,
        endMileageImg : "",
        startOil: 0.0,
        endOil: 0.0,
        startOilImg : "",
        endOilImg : "",
        refuelled: 0.0,
        remark : "",
        status : 0,
        createBy : "",
        createTime : "",
        updateBy : "",
        updateTime : ""
      },
      formModel: {
        id: 0,
        carNumber: "",
        carId: 0,
        carVehicle : "",
        orderNo : "",
        name: "",
        mobile: "",
        startTime: "",
        returnTime: "",
        oilWear: 0.0,
        startMileage: 0,
        startMileageImg : "",
        endMileage : 0,
        endMileageImg : "",
        startOil: 0.0,
        endOil: 0.0,
        startOilImg : "",
        endOilImg : "",
        refuelled: 0.0,
        remark : "",
        status : 0,
        createBy : "",
        createTime : "",
        updateBy : "",
        updateTime : ""
      },
      shopCarList: [],
      techList :[],
      rules: {
        name: [{ required: true, message: "请输入公司全称", trigger: "blur" }],
        simpleName: [
          { required: true, message: "请输入公司简称", trigger: "blur" }
        ],
        mobile: [
          { required: true, message: "请输入联系电话", trigger: "blur" },
          { max: 20, message: "联系电话长度不允许超过20", trigger: "blur" }
        ],
        province: [{ required: true, message: "请输入省", trigger: "blur" }],
        city: [{ required: true, message: "请输入市", trigger: "blur" }],
        district: [{ required: true, message: "请输入区", trigger: "blur" }],
        address: [
          { required: true, message: "请输入街道地址", trigger: "blur" }
        ]
      },
      formLabelWidth: "120px",

      statusList: [
        {
          key: -1,
          value: "选择状态"
        },
        {
          key: 0,
          value: "未还车"
        },
        {
          key: 1,
          value: "已还车"
        }
      ]
    };
  },
  created() {
    this.fetchData();
  },
  watch: {},
  computed: {
    ...mapGetters(["userInfo"])
  },
  methods: {
    getStatusName(value) {
      var name = "";
      switch (value) {
        case 1:
          name = "已还车";
          break;
        case 0:
          name = "未还车";
          break;
      }
      return name;
    },
    fetchData() {
      this.getPaged();
     /*  this.getShopCarListByShopId();
      this.getShopTechListByShopId(); */
    },
    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    },
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    getPaged(flag) {
      this.tableLoading = true;
      let data = JSON.parse(JSON.stringify(this.condition));
      if (data.installTime != null) {
        if (data.installTime[0] != "") {
          data.startTime = data.installTime[0];
        }
        if (data.installTime[1] != "") {
          data.returnTime = data.installTime[1];
        }
      }
      console.log("condition: " + JSON.stringify(data));
      shopManageSvc
        .getTripRecordPageList(data)
        .then(
          res => {
            var data = res.data;
            console.log("data1: " + JSON.stringify(data));
            this.tableData = data.items;
            this.totalList = data.totalItems;
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

    add() {
      // console.log("新增按钮");
      this.formTitle = "添加车辆";
      this.formVisible = true;
      this.getShopCarListByShopId();
      this.getShopTechListByShopId();
    },
    edit(sel) {
      this.formTitle = "技师出行详情";
      this.formVisible = true;
      this.getTechTripRecordInfo(sel.id);
      /* this.getShopCarListByShopId();
      this.getShopTechListByShopId(); */
    },

    getShopCarListByShopId() {
      this.tableLoading = true;
      var carList = {status : 0,pageIndex : 1, pageSize : 100}
      console.log("condition: " + JSON.stringify(carList));
      shopManageSvc
        .getShopCarListByShopId(carList)
        .then(
          res => {
            var data = res.data;
            console.log("ShopCarList: " + JSON.stringify(data));
            this.shopCarList = data.items;
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
    getShopTechListByShopId() {
      this.tableLoading = true;
      var employeeList = {organizationId : this.userInfo.organizationId,pageIndex : 1, pageSize : 100}
      console.log("condition: " + JSON.stringify(employeeList));
      empSvc
        .getEmployeePage(employeeList)
        .then(
          res => {
            var data = res.data;
            this.techList = data.items;
            console.log("ShopTechList: " + JSON.stringify(this.techList));
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

    //查询门店车辆详情
    getTechTripRecordInfo(id) {
      console.log("ID:" + id);
      shopManageSvc
        .getTechTripRecordInfo({ Id: id })
        .then(res => {
          let data = res.data.techTripRecordVO;
          console.log("formModel:" + JSON.stringify(data));
          this.formModel = data;
          this.formCheck = true;
        })
        .catch(err => {
          console.error(err);
        })
        .finally(() => {});
    },

    save(formName) {
      var vm = this;
      //debugger;
      let objCarId = {};
      objCarId = this.shopCarList.find(item => {
        //model就是上面的数据源
        return item.carNumber === this.formModel.carNumber; //筛选出匹配数据
      });
      this.formModel.carId = objCarId.Id;

      let objTech = {};
      objTech = this.techList.find(item => {
        //model就是上面的数据源
        return item.name === this.formModel.technician; //筛选出匹配数据
      });
      this.formModel.mobile = objTech.mobile;
      console.log("save formModel: " + JSON.stringify(this.formModel));
      this.$refs[formName].validate(valid => {
        if (valid) {
          vm.btnSaveLoading = true;
          shopManageSvc
            .addShopCarRecordInfo(this.formModel)
            .then(
              res => {
                if (res.code == 10000) {
                  this.cancel("formModel");
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  this.getPaged();
                }
              },
              err => {
                console.log(err);
              }
            )
            .catch(err => {
              console.error(err);
            })
            .finally(() => {
              vm.btnSaveLoading = false;
            });
        } else {
          return false;
        }
      });
    },
    
    cancel(formName) {
      this.formVisible = false;
      this.formCheck = false;

      this.resetForm();

      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },
    resetForm() {
      this.formModel = JSON.parse(JSON.stringify(this.formInit));
    },
    resetSel() {
      this.organizationSel = [];
      this.jobSel = [];
    },
    deleteShopCarRecordStatus() {
      shopManageSvc
        .deleteShopCarRecordStatus({
          Id: this.formModel.id,
        })
        .then(res => {
          if (res.code == 10000) {
            this.cancel("formModel");
            this.$message({
              message: res.message,
              type: "success"
            });
            this.getPaged();
          }
        })
        .catch(err => {
          console.error(err);
        })
        .finally(() => {});
    }
    
  }
};
</script>