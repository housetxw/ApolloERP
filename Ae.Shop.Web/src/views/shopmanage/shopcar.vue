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
      :tbCellClassName="(p)=>{ 
        if(p.columnIndex==11)
          return p.row.status==0 ?'red':'yellowgreen';
        }"
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
          <el-select
            v-model="condition.status"
            clearable
            style="width:120px"
            filterable
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
        <el-button type="primary" icon="el-icon-plus" @click="add">新增</el-button>
      </template>
      <!-- 搜索 -->
      <!-- 数据表格 -->
      <template v-slot:tb_cols>
        <el-table-column label="车牌号" prop="carNumber" min-width="30"></el-table-column>
        <el-table-column label="车型" prop="vehicleCar" min-width="60"></el-table-column>
        <el-table-column label="状态" prop="status" align="center" width="80">
          <template slot-scope="scope">{{scope.row.status==0?"正常":"禁用"}}</template>
        </el-table-column>
        <el-table-column label="创建者" prop="createBy" min-width="10" align="center"></el-table-column>
        <el-table-column label="创建时间" prop="createTime" min-width="30" align="center"></el-table-column>
        <el-table-column label="最后更新者" prop="updateBy" min-width="10" align="center"></el-table-column>
        <el-table-column label="最后更新时间" prop="updateTime" min-width="30" align="center"></el-table-column>
        <el-table-column label="操作" align="center" fixed="right" width="90">
          <template slot-scope="scope">
            <el-button-group>
              <el-button type="primary" size="mini" @click="edit(scope.row)">编辑</el-button>
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
        :btnRemove="{label:'禁用',disabled:formModel.status==1,show:true,click:(done)=>{updateShopCarStatus(1)}}"
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
                <el-col :span="12">
                  <el-input
                    v-model="formModel.carNumber"
                    placeholder="请输入车牌"
                    clearable
                  ></el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="车型" prop="brand">
                <el-col :span="6">
                  <el-select
                    style="width:100%"
                    v-model="formModel.brand"
                    @change="getVehicleByBrand();"
                    prop="province"
                    filterable placeholder="-请选择品牌-"
                  >
                    <el-option
                      v-for="item in brandList"
                      :key="item.brand"
                      :label="item.brand"
                      :value="item.brand"
                    ></el-option>
                  </el-select>
                </el-col>
                <el-col :span="6">
                  <el-select
                    style="width:100%"
                    v-model="formModel.vehicleId"
                    @change="getPaiLiangByVehicleId()"
                    clearable filterable placeholder="-请选择车系-"
                    prop="vehicle"
                  >
                    <el-option
                      v-for="item in vehicleList"
                      :key="item.vehicleId"
                      :label="item.vehicle"
                      :value="item.vehicleId"
                    ></el-option>
                  </el-select>
                </el-col>
                <el-col :span="6">
                  <el-select v-model="formModel.paiLiang" @change="getVehicleNianByPaiLiang()"  clearable filterable style="width:100%">
                    <el-option
                      v-for="item in paiLiangList"
                      :key="item"
                      :label="item"
                      :value="item"
                    ></el-option>
                  </el-select>
                </el-col>
                <el-col :span="6">
                  <el-select v-model="formModel.nian" @change="getVehicleSalesNameByNian()" clearable filterable placeholder="-请选择生产年份-"  style="width:100%">
                    <el-option
                      v-for="item in nianList"
                      :key="item"
                      :label="item"
                      :value="item"
                    ></el-option>
                  </el-select>
                </el-col>
                <el-col :span="12">
                  <el-select v-model="formModel.salesName" clearable filterable placeholder="-请选择车款-"  style="width:100%">
                    <el-option
                      v-for="item in salesNameList"
                      :key="item.tid"
                      :label="item.salesName"
                      :value="item.salesName"
                    ></el-option>
                  </el-select>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="车架号" prop="vinCode">
                <el-col :span="18">
                  <el-input v-model="formModel.vinCode" placeholder="车架号"></el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="里程数">
                <el-col :span="6">
                  <el-input v-model="formModel.totalMileage" placeholder="里程数" clearable></el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="车辆期初价格">
                <el-col :span="6">
                  <el-input
                    v-model="formModel.price"
                    placeholder="车辆期初价格"
                    clearable
                  >
                    <template slot="append">元</template>
                  </el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-form-item label="保险日期" required>
              <el-col :span="6">
                <el-date-picker
                  v-model="formModel.insureStart"
                  type="date"
                  placeholder="开始日期"
                  format="yyyy-MM-dd"
                  class="no-radius-right"
                  value-format="yyyy-MM-dd"
                  clearable
                />
              </el-col>
              <el-col :span="6">
                <el-date-picker
                  v-model="formModel.insureEnd"
                  type="date"
                  class="no-radius-left"
                  placeholder="结束日期"
                  format="yyyy-MM-dd"
                  value-format="yyyy-MM-dd"
                  clearable
                />
              </el-col>
            </el-form-item>
            <el-row>
              <el-form-item label="投保公司">
                <el-col :span="6">
                  <el-input v-model="formModel.insuranceCompany" placeholder="投保公司" clearable></el-input>
                </el-col>
              </el-form-item>
            </el-row>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button-group>
            <el-button
              icon="el-icon-check"
              size="mini"
              type="primary"
              :loading="btnSaveLoading"
              @click="save('formModel')"
            >提交</el-button>
          </el-button-group>
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
import { appSvc } from "@/api/baoyang/baoyang";

export default {
  name: "shopcar",
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
        shopId: 0,
        carNumber: undefined,
        status: "-1"
      },
      formVisible: false,
      formTitle: "详情",
      formCheck: false,
      btnSaveLoading: false,
      formInit: {
        id: 0,
        carNumber: "",
        brand: "",
        status: 0,
        vehicle: "",
        vehicleId: "",
        paiLiang: "",
        nian: "",
        tid: "",
        salesName: "",
        vinCode: "",
        totalMileage: 0,
        price: 0.00,
        insureStart: "",
        insureEnd: "",
        insuranceCompany: ""
      },
      formModel: {
        id: 0,
        carNumber: "",
        brand: "",
        status: 0,
        vehicle: "",
        vehicleId: "",
        paiLiang: "",
        nian: "",
        tid: "",
        salesName: "",
        vinCode: "",
        totalMileage: 0,
        price: 0.00,
        insureStart: "",
        insureEnd: "",
        insuranceCompany: ""
      },
      brandList:[],
      vehicleList:[],
      paiLiangList:[],
      nianList:[],
      salesNameList:[],
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
          key: "-1",
          value: "选择状态"
        },
        {
          key: "0",
          value: "正常"
        },
        {
          key: "1",
          value: "禁用"
        }
      ],
      //照片变量定义
      fileList: []
    };
  },
  created() {
    this.fetchData();
    this.getBrandList();
  },
  watch: {},
  computed: {
    ...mapGetters(["userInfo"])
  },
  methods: {
    fetchData() {
      this.getPaged();
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
      if (this.condition.status == "" || this.condition.status == undefined) {
        this.condition.status = "-1";
      }
      console.log("condition: " + JSON.stringify(this.condition));
      shopManageSvc
        .getShopCarListByShopId(this.condition)
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
    },
    edit(sel) {
      this.formTitle = "编辑车辆";
      this.formVisible = true;
      this.getShopCarInfoById(sel.id);
    },

    getBrandList() {
      appSvc
        .getBrandList()
        .then(
          res => {
            var data = res.data;
            this.brandList = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getVehicleByBrand() {
      if (this.formModel.brand == "") {
        return;
      }
      var vehicleCondition = { brand: this.formModel.brand };
      appSvc
        .getListByBrand(vehicleCondition)
        .then(
          res => {
            var data = res.data;
            this.vehicleList = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getPaiLiangByVehicleId() {
      if (this.formModel.vehicleId == "") {
        return;
      }
      var paiLiangRequest = { vehicleId: this.formModel.vehicleId };
      console.log(JSON.stringify(paiLiangRequest));
      appSvc
        .getPaiLiangByVehicleId(paiLiangRequest)
        .then(
          res => {
            var data = res.data;
            this.paiLiangList = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getVehicleNianByPaiLiang() {
      if (this.formModel.vehicleId == "" || this.formModel.paiLiang == "") {
        return;
      }
      var nianRequest = { vehicleId: this.formModel.vehicleId, paiLiang: this.formModel.paiLiang };
      appSvc
        .getVehicleNianByPaiLiang(nianRequest)
        .then(
          res => {
            var data = res.data;
            this.nianList = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getVehicleSalesNameByNian() {
      if (this.formModel.vehicleId == "" || this.formModel.paiLiang == "" || this.formModel.nian == "") {
        return;
      }
      var tidRequest = {
        vehicleId: this.formModel.vehicleId,
        paiLiang: this.formModel.paiLiang,
        nian: this.formModel.nian
      };
      appSvc
        .getVehicleSalesNameByNian(tidRequest)
        .then(
          res => {
            var data = res.data;
            this.salesNameList = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },

    save(formName) {
      var vm = this;
      console.log("save formModel: " + JSON.stringify(this.formModel));
      //debugger;
      
      let objVehicle = {};
      objVehicle = this.vehicleList.find(item => {
        //model就是上面的数据源
        return item.vehicleId === this.formModel.vehicleId; //筛选出匹配数据
      });
      this.formModel.vehicle = objVehicle.vehicle;
      this.$refs[formName].validate(valid => {
        if (valid) {
          vm.btnSaveLoading = true;
          shopManageSvc
            .AddOrModifyShopCar(this.formModel)
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

    //查询门店车辆详情
    getShopCarInfoById(id) {
      console.log("ID:" + id);
      shopManageSvc
        .getShopCarInfoById({ id: id })
        .then(res => {
          let data = res.data;
          console.log("formModel:" + JSON.stringify(data));
          this.formModel = data;
          this.getVehicleByBrand();
          this.getPaiLiangByVehicleId();
          this.getVehicleNianByPaiLiang();
          this.getVehicleSalesNameByNian();
          this.formCheck = true;
          
        })
        .catch(err => {
          console.error(err);
        })
        .finally(() => {});
    },
    updateShopCarStatus(type) {
      shopManageSvc
        .updateShopCarStatus({
          Id: this.formModel.id,
          Status: type
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