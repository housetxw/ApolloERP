<template>
  <main class="container">
    <section id="indexPage">
      <main>
        <section>
          <el-tabs type="border-card" :tab-position="tabPosition">
            <el-tab-pane label="基础信息">
              <el-form :model="formModel" :inline="true" :rules="rules" ref="formModel">
                <el-form-item label="供应商简称" prop="venderShortName">
                  <el-input v-model="formModel.venderShortName"></el-input>
                </el-form-item>
                <el-form-item label="供应商全称" prop="venderName">
                  <el-input v-model="formModel.venderName"></el-input>
                </el-form-item>
                <el-form-item label="级别" prop="classType">
                  <el-select v-model="formModel.classType" placeholder="请选择级别">
                    <el-option
                      v-for="item in classTypeoptions"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>

                <el-form-item label="供应商类型" prop="supplyType">
                  <el-select v-model="formModel.supplyType" placeholder="请选择类型">
                    <el-option
                      v-for="item in supplyTypeSel"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>
                <el-form-item label="电话号码" prop="telNum">
                  <el-input v-model="formModel.telNum"></el-input>
                </el-form-item>
                <el-form-item label="一般纳税资格" prop="qualification">
                  <el-input v-model="formModel.qualification"></el-input>
                </el-form-item>

                <el-form-item label="营业执照" prop="businessLicense">
                  <el-input v-model="formModel.businessLicense"></el-input>
                </el-form-item>

                <el-form-item label="组织机构代码" prop="organizationCode">
                  <el-input v-model="formModel.organizationCode"></el-input>
                </el-form-item>
                <br />
                <el-form-item label="省" prop="provinceId">
                  <el-select
                    v-model="formModel.provinceId"
                    size="small"
                    clearable
                    filterable
                    placeholder="请选择省"
                    @change="getCity"
                    class="margin-right-10"
                  >
                    <el-option
                      v-for="item in provinceSel"
                      :key="item.regionId"
                      :label="item.name"
                      :value="item.regionId"
                    ></el-option>
                  </el-select>
                </el-form-item>
                <el-form-item label="市" prop="cityId">
                  <el-select
                    v-model="formModel.cityId"
                    size="small"
                    clearable
                    filterable
                    @change="getDistrict"
                    placeholder="请选择市"
                    class="margin-right-10"
                  >
                    <el-option
                      v-for="item in citySel"
                      :key="item.regionId"
                      :label="item.name"
                      :value="item.regionId"
                    ></el-option>
                  </el-select>
                </el-form-item>

                <el-form-item label="区" prop="districtId">
                  <el-select
                    v-model="formModel.districtId"
                    size="small"
                    clearable
                    filterable
                    placeholder="请选择区"
                    class="margin-right-10"
                  >
                    <el-option
                      v-for="item in districtSel"
                      :key="item.regionId"
                      :label="item.name"
                      :value="item.regionId"
                    ></el-option>
                  </el-select>
                </el-form-item>
                <br />
                <el-form-item label="办公地址" prop="officeAddress">
                  <el-input style="width:400px;" v-model="formModel.officeAddress"></el-input>
                </el-form-item>
              </el-form>
            </el-tab-pane>
            <el-tab-pane label="财务信息">
              <el-form :model="formModel" :inline="true" :rules="rules" ref="formModel">
                <el-form-item label="税号" prop="tex">
                  <el-input v-model="formModel.tex"></el-input>
                </el-form-item>
                <el-form-item label="票点(税点)" prop="taxPoint">
                  <el-input
                    v-model="formModel.taxPoint"
                    oninput="value=value.replace(/[^\d.]/g,'')"
                  ></el-input>
                </el-form-item>
                <el-form-item label="财务联系人" prop="financeName">
                  <el-input v-model="formModel.financeName"></el-input>
                </el-form-item>

                <el-form-item label="财务联系人方式" prop="financeTel">
                  <el-input v-model="formModel.financeTel"></el-input>
                </el-form-item>

                <el-form-item label="注册地址" prop="registerAddress">
                  <el-input style="width:400px;" v-model="formModel.registerAddress"></el-input>
                </el-form-item>
                <br />
                <el-form-item label="开户行" prop="bank">
                  <el-input v-model="formModel.bank"></el-input>
                </el-form-item>

                <el-form-item label="银行账号" prop="account">
                  <el-input v-model="formModel.account"></el-input>
                </el-form-item>

                <el-form-item label="收款单位" prop="payee">
                  <el-input v-model="formModel.payee"></el-input>
                </el-form-item>
              </el-form>
            </el-tab-pane>
            <el-tab-pane label="销售信息">
              <el-form :model="formModel" :inline="true" :rules="rules" ref="formModel">
                <el-form-item label="发货方式" prop="shipmentType">
                  <el-select
                    v-model="formModel.shipmentType"
                    size="small"
                    clearable
                    filterable
                    placeholder="请选择发货方式"
                    class="margin-right-10"
                  >
                    <el-option
                      v-for="item in shipmentTypeoptions"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>

                <el-form-item label="合作模式" prop="cooperationType">
                  <el-select
                    v-model="formModel.cooperationType"
                    size="small"
                    clearable
                    filterable
                    placeholder="请选择合作模式"
                    class="margin-right-10"
                  >
                    <el-option
                      v-for="item in cooperationTypeoptions"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>
                <el-form-item label="返利模式">
                  <el-select
                    v-model="formModel.rebateType"
                    size="small"
                    clearable
                    filterable
                    placeholder="请选择合作模式"
                    class="margin-right-10"
                  >
                    <el-option
                      v-for="item in rebateTypeoptions"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>
                <br />
                <el-form-item label="零采联系人">
                  <el-input v-model="formModel.bizName"></el-input>
                </el-form-item>

                <el-form-item label="零采联系方式">
                  <el-input v-model="formModel.bizTel"></el-input>
                </el-form-item>

                <el-form-item label="零采账期">
                  <el-select
                    v-model="formModel.paymentDay"
                    size="small"
                    clearable
                    filterable
                    placeholder="请选择零采账期"
                    class="margin-right-10"
                  >
                    <el-option
                      v-for="item in paymentDayoptions"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>
                <br />
                <el-form-item label="批采联系人">
                  <el-input v-model="formModel.bulkBizName"></el-input>
                </el-form-item>

                <el-form-item label="批采联系方式">
                  <el-input v-model="formModel.bulkBizTel"></el-input>
                </el-form-item>
                <br />
                <el-form-item label="企业负责人" prop="enterpriseName">
                  <el-input v-model="formModel.enterpriseName"></el-input>
                </el-form-item>

                <el-form-item label="联系方式" prop="enterpriseTel">
                  <el-input v-model="formModel.enterpriseTel"></el-input>
                </el-form-item>
              </el-form>
            </el-tab-pane>
            <el-tab-pane label="售后信息">
              <el-form :model="formModel" :inline="true" :rules="rules" ref="formModel">
                <el-form-item label="售后联系人">
                  <el-input v-model="formModel.overSaleName"></el-input>
                </el-form-item>

                <el-form-item label="联系方式">
                  <el-input v-model="formModel.overSaleTel"></el-input>
                </el-form-item>

                <el-form-item label="邮箱地址" prop="emailAddress">
                  <el-input v-model="formModel.emailAddress"></el-input>
                </el-form-item>

                <el-form-item label="传真">
                  <el-input v-model="formModel.fax"></el-input>
                </el-form-item>
                <br />
                <el-form-item label="售后地址">
                  <el-input v-model="formModel.overSaleAddress"></el-input>
                </el-form-item>
              </el-form>
            </el-tab-pane>
          </el-tabs>

          <div slot="footer" class="dialog-footer" style="margin-top:10px;">
            <el-button @click="reset()">重置</el-button>
            <el-button type="primary" @click="save('formModel')">保存</el-button>
          </div>
        </section>
      </main>
    </section>
  </main>
</template>


<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appVenderSvc } from "@/api/purchase/vender";
import { isNumber } from "util";

export default {
  data() {
    return {
      tabPosition: "left",
      supplyTypeSelCondition: {
        RequestType: 1
      },
      payDaysCondition: {
        value: undefined,
        label: undefined
      },
      classTypeoptions: [
        { value: "普通", label: "普通" },
        { value: "高级", label: "高级" },
        { value: "战略", label: "战略" }
      ],

      cooperationTypeoptions: [
        { value: "三方", label: "三方" },
        { value: "加盟", label: "加盟" }
      ],

      rebateTypeoptions: [
        { value: "月返", label: "月返" },
        { value: "季返", label: "季返" },
        { value: "年返", label: "年返" }
      ],
      shipmentTypeoptions: [
        { value: "供应商送货", label: "供应商送货" },
        { value: "总部自提", label: "总部自提" },
        { value: "快递", label: "快递" },
        { value: "物流", label: "物流" }
      ],
      paymentDayoptions: [],
      provinceSel: [],
      supplyTypeSel: [],
      citySel: [],
      districtSel: [],
      regionCondition: {
        regionId: undefined
      },
      formModel: {
        venderShortName: undefined,
        venderName: undefined,
        classType: undefined,
        supplyType: undefined,
        telNum: undefined,
        qualification: undefined,
        businessLicense: undefined,
        organizationCode: undefined,
        provinceId: undefined,
        cityId: undefined,
        districtId: undefined,
        officeAddress: undefined,
        tex: undefined,
        taxPoint: 1.0,
        financeName: undefined,
        financeTel: undefined,
        registerAddress: undefined,
        bank: undefined,
        account: undefined,
        payee: undefined,
        cooperationType: undefined,
        rebateType: undefined,
        bizName: undefined,
        bizTel: undefined,
        paymentDay: undefined,
        bulkBizName: undefined,
        bulkBizTel: undefined,
        enterpriseName: undefined,
        enterpriseTel: undefined,
        overSaleName: undefined,
        overSaleTel: undefined,
        emailAddress: undefined,
        provinceName: undefined,
        cityName: undefined,
        districtName: undefined,
        fax: undefined,
        overSaleAddress: undefined,
        shipmentType: undefined
      },
      formModelInit: {
        venderShortName: undefined,
        venderName: undefined,
        classType: undefined,
        supplyType: undefined,
        telNum: undefined,
        qualification: undefined,
        businessLicense: undefined,
        organizationCode: undefined,
        provinceId: undefined,
        cityId: undefined,
        districtId: undefined,
        officeAddress: undefined,
        tex: undefined,
        taxPoint: 1.0,
        financeName: undefined,
        financeTel: undefined,
        registerAddress: undefined,
        bank: undefined,
        account: undefined,
        payee: undefined,
        cooperationType: undefined,
        rebateType: undefined,
        bizName: undefined,
        bizTel: undefined,
        paymentDay: undefined,
        bulkBizName: undefined,
        bulkBizTel: undefined,
        enterpriseName: undefined,
        enterpriseTel: undefined,
        overSaleName: undefined,
        overSaleTel: undefined,
        emailAddress: undefined,
        provinceName: undefined,
        cityName: undefined,
        districtName: undefined,
        fax: undefined,
        overSaleAddress: undefined,
        shipmentType: undefined
      },
      rules: {
        venderShortName: [
          {
            required: true,
            message: "请填写简称",
            trigger: "blur"
          }
        ],
        venderName: [
          {
            required: true,
            message: "请填写全称",
            trigger: "blur"
          }
        ],
        classType: [
          {
            required: true,
            message: "请选择级别",
            trigger: "blur"
          }
        ],
        shipmentType: [
          {
            required: true,
            message: "请选择发货方式",
            trigger: "blur"
          }
        ],

        telNum: [
          {
            required: true,
            message: "请填写手机号",
            trigger: "blur"
          }
        ],
        supplyType: [
          {
            required: true,
            message: "请选择供应商类型",
            trigger: "blur"
          }
        ],
        qualification: [
          {
            required: true,
            message: "请填写纳税资格号",
            trigger: "blur"
          }
        ],
        businessLicense: [
          {
            required: true,
            message: "请填写营业执照",
            trigger: "blur"
          }
        ],
        organizationCode: [
          {
            required: true,
            message: "请填写组织机构代码",
            trigger: "blur"
          }
        ],
        provinceId: [
          {
            required: true,
            message: "请选择省",
            trigger: "blur"
          }
        ],
        cityId: [
          {
            required: true,
            message: "请选择市",
            trigger: "blur"
          }
        ],
        districtId: [
          {
            required: true,
            message: "请选择区",
            trigger: "blur"
          }
        ],
        officeAddress: [
          {
            required: true,
            message: "请选择办公地址",
            trigger: "blur"
          }
        ],
        taxPoint: [
          {
            required: true,
            message: "请填写税点",
            trigger: "blur"
          }
        ],

        financeName: [
          {
            required: true,
            message: "请填写财务联系人",
            trigger: "blur"
          }
        ],
        financeTel: [
          {
            required: true,
            message: "请填写财务联系方式",
            trigger: "blur"
          }
        ],
        registerAddress: [
          {
            required: true,
            message: "请填写注册地址",
            trigger: "blur"
          }
        ],
        bank: [
          {
            required: true,
            message: "请填写开户行",
            trigger: "blur"
          }
        ],

        account: [
          {
            required: true,
            message: "请填写银行账户",
            trigger: "blur"
          }
        ],
        payee: [
          {
            required: true,
            message: "请填写收款单位",
            trigger: "blur"
          }
        ],
        emailAddress: [
          {
            required: true,
            message: "请填写邮箱地址",
            trigger: "blur"
          }
        ],
        enterpriseName: [
          {
            required: true,
            message: "请填写企业负责人",
            trigger: "blur"
          }
        ],
        enterpriseTel: [
          {
            required: true,
            message: "请填写企业联系电话",
            trigger: "blur"
          }
        ]
      }
    };
  },
  computed: {
    routeComputed: {
      get: function() {
        return this.formModel.route;
      },
      set: function(val) {
        this.formModel.route = val.toLowerCase();
      }
    }
  },
  created() {
    //页面初始化函数
    this.fetchData();
  },
  methods: {
    reset() {
      this.formModel = JSON.parse(JSON.stringify(this.formModelInit));
    },
    save(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          this.$confirm("确定保存吗！", "警告", {
            confirmButtonText: "确定",
            cancelButtonText: "取消",
            type: "warning"
          })
            .then(() => {
              var provinceName = "";
              for (var i = 0; i < this.provinceSel.length; i++) {
                if (this.provinceSel[i].regionId == this.formModel.provinceId) {
                  provinceName = this.provinceSel[i].name;
                  break;
                }
              }

              var cityName = "";
              for (var i = 0; i < this.citySel.length; i++) {
                if (this.citySel[i].regionId == this.formModel.cityId) {
                  cityName = this.citySel[i].name;
                  break;
                }
              }

              var districtName = "";
              for (var i = 0; i < this.districtSel.length; i++) {
                if (this.districtSel[i].regionId == this.formModel.districtId) {
                  districtName = this.districtSel[i].name;
                  break;
                }
              }

              this.formModel.provinceName = provinceName;
              this.formModel.cityName = cityName;
              this.formModel.districtName = districtName;

              appVenderSvc
                .createVender(this.formModel)
                .then(res => {
                  if (res.code == 10000) {
                    this.$message({
                      message: res.message,
                      type: "success"
                    });
                    this.formModel = JSON.parse(
                      JSON.stringify(this.formModelInit)
                    );
                  }
                })
                .catch(error => {
                  console.log(error);
                });
            })
            .catch(() => {});
        } else {
          return false;
        }
      });
    },
    fetchData() {
      debugger;
      this.getSupplyType();
      this.regionCondition.regionId = 0;
      appVenderSvc
        .getRegionChinaListByRegionId(this.regionCondition)
        .then(
          res => {
            this.provinceSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});

      //paymentDayoptions
      for (var i = 1; i <= 31; i++) {
        let obj = {
          value: i,
          label: i
        };

        this.paymentDayoptions.push(obj);
      }
    },
    getCity() {
      debugger;
      if (
        this.formModel.provinceId != "" &&
        this.formModel.provinceId != undefined
      ) {
        this.regionCondition.regionId = this.formModel.provinceId;
        appVenderSvc
          .getRegionChinaListByRegionId(this.regionCondition)
          .then(
            res => {
              this.citySel = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },

    getDistrict() {
      if (this.formModel.cityId != "" && this.formModel.cityId != undefined) {
        this.regionCondition.regionId = this.formModel.cityId;

        appVenderSvc
          .getRegionChinaListByRegionId(this.regionCondition)
          .then(
            res => {
              this.districtSel = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },
    getSupplyType() {
      appVenderSvc
        .getBasicInfoList(this.supplyTypeSelCondition)
        .then(
          res => {
            //  this.tableData = res.data;
            this.supplyTypeSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    }
  }
};
</script>

<style lang="scss" scoped>
.container {
  margin-top: 30px;
  margin-left: 30px;
  .indexPage {
    display: flex;
    align-items: center;
  }
  .pagination {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100px;
  }
}
</style>