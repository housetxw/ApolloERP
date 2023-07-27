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
      :tbCellClassName="
        (p) => {
          if (p.columnIndex == 11)
            return p.row.status == 3
              ? 'red'
              : p.row.status == 2
              ? 'yellowgreen'
              : '';
        }
      "
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="公司名称"
            style="width: 150px"
            prefix-icon="el-icon-search"
            v-model="condition.name"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.provinceId"
            clearable
            filterable
            placeholder="选择省"
            @change="getCityByRegionId"
          >
            <el-option
              v-for="item in provinceList"
              :key="item.regionId"
              :label="item.name"
              :value="item.regionId"
            ></el-option>
          </el-select>
          <el-select
            v-model="condition.cityId"
            clearable
            filterable
            @change="getDistrictByRegionId"
            placeholder="选择市"
          >
            <el-option
              v-for="item in cityList"
              :key="item.regionId"
              :label="item.name"
              :value="item.regionId"
            ></el-option>
          </el-select>
          <el-select
            v-model="condition.districtId"
            clearable
            filterable
            placeholder="选择区"
          >
            <el-option
              v-for="item in districtList"
              :key="item.regionId"
              :label="item.name"
              :value="item.regionId"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.status"
            clearable
            style="width: 120px"
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
        <!-- <el-button type="primary" icon="el-icon-plus" @click="add"
          >新增</el-button
        > -->
      </template>
      <!-- 搜索 -->
      <!-- 数据表格 -->
      <template v-slot:tb_cols>
        <el-table-column
          label="公司全称"
          prop="name"
          min-width="150"
        ></el-table-column>
        <el-table-column
          label="简称"
          prop="simpleName"
          min-width="150"
        ></el-table-column>
        <rg-table-column label="省" prop="province" />
        <rg-table-column label="市" prop="city" />
        <rg-table-column label="区" prop="district" />
        <rg-table-column
          label="门店数"
          align="right"
          prop="shopTotalCount"
        ></rg-table-column>
        <el-table-column label="总负责人" prop="head" min-width="140">
          <template slot-scope="scope"
            >{{ scope.row.head }}
            {{
              scope.row.headPhone ? " - " + scope.row.headPhone : ""
            }}</template
          >
        </el-table-column>
        <rg-table-column label="公司法人" prop="legalPerson" />
        <el-table-column
          label="注册资金(万元)"
          prop="registerMoney"
          width="110"
          align="right"
        >
          <template slot-scope="scope"
            >{{ scope.row.registerMoney.toLocaleString() }} 万</template
          >
        </el-table-column>
        <el-table-column
          label="工商注册日期"
          prop="registerTime"
          width="100"
          align="center"
        ></el-table-column>
        <el-table-column label="状态" prop="status" align="center" width="80">
          <template slot-scope="scope">{{
            scope.row.status == 0
              ? "待提交"
              : scope.row.status == 1
              ? "审核中"
              : scope.row.status == 2
              ? "正常"
              : "已注销"
          }}</template>
        </el-table-column>
        <el-table-column label="操作" align="center" fixed="right" width="90">
          <template slot-scope="scope">
            <el-button-group>
              <el-button type="primary" size="mini" @click="edit(scope.row)"
                >查看及编辑</el-button
              >
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
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            cancel('formModel');
          },
        }"
        v-if="formVisible"
        :footbar="true"
        :btnRemove="
          formModel.status == 3
            ? false
            : {
                label: '注销',
                click: (done) => {
                  updateCompanyStatus(3);
                },
              }
        "
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
            <el-row>
              <el-col :span="12">
                <el-form-item label="隶属公司">
                  <span class="s2">{{ userInfo.organizationName }}</span>
                </el-form-item>
              </el-col>
            </el-row>
            <el-divider content-position="left">
              <h3>基本信息</h3>
            </el-divider>
            <el-row>
              <el-form-item label="公司全称" prop="name">
                <el-col :span="12">
                  <el-input
                    v-model="formModel.name"
                    placeholder="公司全称"
                    clearable
                    :disabled="formCheck"
                  ></el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="公司简称" prop="simpleName">
                <el-col :span="12">
                  <el-input
                    v-model="formModel.simpleName"
                    placeholder="公司简称"
                    clearable
                  ></el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="经营地址:" prop="address">
                <el-col :span="6">
                  <el-select
                    style="width: 100%"
                    v-model="formModel.provinceId"
                    @change="getCityListByRegionId"
                    prop="province"
                  >
                    <el-option
                      v-for="item in provinceList"
                      :key="item.regionId"
                      :label="item.name"
                      :value="item.regionId"
                    ></el-option>
                  </el-select>
                </el-col>
                <el-col :span="6">
                  <el-select
                    style="width: 100%"
                    v-model="formModel.cityId"
                    @change="getDistrictListByRegionId"
                    prop="city"
                  >
                    <el-option
                      v-for="item in cityList"
                      :key="item.regionId"
                      :label="item.name"
                      :value="item.regionId"
                    ></el-option>
                  </el-select>
                </el-col>
                <el-col :span="6">
                  <el-select
                    v-model="formModel.districtId"
                    prop="district"
                    style="width: 100%"
                  >
                    <el-option
                      v-for="item in districtList"
                      :key="item.regionId"
                      :label="item.name"
                      :value="item.regionId"
                    ></el-option>
                  </el-select>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label prop="address">
                <el-col :span="18">
                  <el-input
                    v-model="formModel.address"
                    placeholder="街道地址"
                  ></el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="联系电话">
                <el-col :span="6">
                  <el-input
                    v-model="formModel.mobile"
                    placeholder="固定电话或手机号码"
                    clearable
                  ></el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="公司负责人">
                <el-col :span="6">
                  <el-input
                    v-model="formModel.head"
                    placeholder="公司业务总负责人"
                    clearable
                  ></el-input>
                </el-col>
                <el-col :span="6">
                  <el-input
                    v-model="formModel.headPhone"
                    placeholder="联系电话"
                    clearable
                  ></el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <!-- <el-row>
              <el-col :span="14">
                <el-form-item label="类型:" prop="type">
                  <el-radio-group v-model="formModel.type">
                    <el-radio :label="0">公司</el-radio>
                    <el-radio :label="2">仓库</el-radio>
                    <el-radio :label="3">拓展</el-radio>
                    <el-radio :label="4">供应商</el-radio>
                  </el-radio-group>
                </el-form-item>
              </el-col>
            </el-row> -->
            <!-- <el-row>
              <el-col :span="14">
                <el-form-item label="系统版本:" prop="systemType">
                  <el-radio-group v-model="formModel.systemType">
                    <el-radio :label="0">无</el-radio>
                    <el-radio :label="1">平台公司</el-radio>
                    <el-radio :label="2">普通公司 </el-radio>
                    <el-radio :label="3">区域合伙公司 </el-radio>
                  </el-radio-group>
                </el-form-item>
              </el-col>
            </el-row> -->

            <!-- <el-divider content-position="left">
              <h3>工商注册信息</h3>
            </el-divider> -->
            <!-- <el-row>
              <el-form-item label="注册资金">
                <el-col :span="6">
                  <el-input
                    v-model="formModel.registerMoney"
                    placeholder="0.00"
                    oninput="value=value.replace(/[^\d.]/g,'')"
                    clearable
                  >
                    <template slot="append">万元</template>
                  </el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="工商注册日期">
                <el-col :span="6">
                  <el-date-picker
                    v-model="formModel.registerTime"
                    type="date"
                    placeholder="年/月/日"
                    format="yyyy-MM-dd"
                    value-format="yyyy-MM-dd"
                    clearable
                  />
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="公司法人">
                <el-col :span="6">
                  <el-input
                    v-model="formModel.legalPerson"
                    placeholder="法人名称"
                    clearable
                  ></el-input>
                </el-col>
                <el-col :span="6">
                  <el-input
                    v-model="formModel.legalPersonPhone"
                    placeholder="法人联系电话"
                    clearable
                  ></el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item>
                <label>营业执照（三证合一）扫描件或照片，须清晰可见</label>
              </el-form-item>
              <el-form-item>
                <el-col :span="12">
                  <el-upload
                    class="upload-company"
                    action="http://upload.qiniup.com/"
                    :on-remove="handleRemove1"
                    :before-remove="beforeRemove"
                    :limit="1"
                    :on-exceed="handleExceed"
                    :file-list="
                      formModel.businessLicense
                        ? [
                            {
                              name: '',
                              url: imagehost + formModel.businessLicense,
                            },
                          ]
                        : []
                    "
                    list-type="picture-card"
                    :http-request="uploadRequest"
                    name="businessLicense"
                    accept="image/jpeg, image/png, image/jpg"
                  >
                    <el-button size="mini" type="primary">上传图片</el-button>
                  </el-upload>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="开户许可证">
                <el-col :span="6">
                  <el-input
                    v-model="formModel.name"
                    placeholder
                    clearable
                    :disabled="true"
                  ></el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label>
                <el-col :span="6">
                  <el-input
                    v-model="formModel.openingBank"
                    placeholder="开户银行"
                    clearable
                  ></el-input>
                </el-col>
                <el-col :span="6">
                  <el-input
                    v-model="formModel.bankAccount"
                    placeholder="账号"
                    clearable
                  ></el-input>
                </el-col>
              </el-form-item>
              <el-form-item>
                <label>开户许可证扫描件或照片，须清晰可见</label>
              </el-form-item>
              <el-form-item>
                <el-upload
                  class="upload-company"
                  action="http://upload.qiniup.com/"
                  :on-remove="handleRemove2"
                  :before-remove="beforeRemove"
                  :limit="1"
                  :on-exceed="handleExceed"
                  :file-list="
                    formModel.accountOpeningLicense
                      ? [
                          {
                            name: '',
                            url: imagehost + formModel.accountOpeningLicense,
                          },
                        ]
                      : []
                  "
                  list-type="picture-card"
                  :http-request="uploadRequest"
                  name="accountOpeningLicense"
                  accept="image/jpeg, image/png, image/jpg"
                >
                  <el-button size="mini" type="primary">上传图片</el-button>
                </el-upload>
              </el-form-item>
            </el-row> -->
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button-group>
            <el-button
              icon="el-icon-check"
              size="mini"
              type="primary"
              :loading="btnSaveLoading"
              :style="{ display: btnSaveVisible }"
              @click="save('formModel')"
              >保存</el-button
            >
            <el-button
              icon="el-icon-check"
              size="mini"
              type="primary"
              :style="{ display: btnStatusVisible }"
              :loading="btnSaveLoading"
              @click="updateCompanyStatus(1)"
              >提交</el-button
            >
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
import { appSvc } from "@/api/company/company";
import { shopManageSvc } from "@/api/shopmanage/shopmanage";
import { upload } from "@/utils/uploadfile";

export default {
  name: "CompanyList",
  data() {
    return {
      w_search_right: 100,
      tableLoading: false,
      flag: this.global.deletedFlag,
      btnSaveVisible: "",
      btnStatusVisible: "",
      name: "",
      provinceId: "",
      cityId: "",
      districtId: "",
      status: -1,
      currentPage: 1,
      tableData: [],
      totalList: 0,
      condition: {
        pageIndex: 1,
        pageSize: 20,
        parentId: 1,
        name: undefined,
        provinceId: undefined,
        cityId: undefined,
        districtId: undefined,
        status: "-1",
      },
      formVisible: false,
      formTitle: "详情",
      formCheck: false,
      btnSaveLoading: false,
      formInit: {
        id: 0,
        simpleName: "",
        name: "",
        parentId: 0,
        status: 0,
        type: 0,
        level: 1,
        provinceId: "请选择",
        cityId: "请选择",
        districtId: "请选择",
        province: "",
        city: "",
        district: "",
        address: "",
        mobile: "",
        head: "",
        headPhone: "",
        registerMoney: 0,
        registerTime: "",
        legalPerson: "",
        legalPersonPhone: "",
        businessLicense: "",
        openingBank: "",
        bankAccount: "",
        accountOpeningLicense: "",
        introduction: "",
        systemType: 0,
      },
      formModel: {
        id: 0,
        simpleName: "",
        name: "",
        parentId: 0,
        status: 0,
        type: 0,
        level: 1,
        provinceId: "请选择",
        cityId: "请选择",
        districtId: "请选择",
        province: "",
        city: "",
        district: "",
        address: "",
        mobile: "",
        head: "",
        headPhone: "",
        registerMoney: 0,
        registerTime: undefined,
        legalPerson: "",
        legalPersonPhone: "",
        businessLicense: "",
        openingBank: "",
        bankAccount: "",
        accountOpeningLicense: "",
        introduction: "",
        systemType: 0,
      },
      companyStatusModel: {
        companyId: 0,
        status: 1,
      },
      rules: {
        name: [{ required: true, message: "请输入公司全称", trigger: "blur" }],
        simpleName: [
          { required: true, message: "请输入公司简称", trigger: "blur" },
        ],
        mobile: [
          { required: true, message: "请输入联系电话", trigger: "blur" },
          { max: 20, message: "联系电话长度不允许超过20", trigger: "blur" },
        ],
        province: [{ required: true, message: "请输入省", trigger: "blur" }],
        city: [{ required: true, message: "请输入市", trigger: "blur" }],
        district: [{ required: true, message: "请输入区", trigger: "blur" }],
        address: [
          { required: true, message: "请输入街道地址", trigger: "blur" },
        ],
      },
      formLabelWidth: "120px",
      provinceList: [], //省数据
      cityList: [], //市
      districtList: [], //区县
      regionProvince: {
        regionId: undefined,
      },
      regionCity: {
        regionId: undefined,
      },
      regionDistrict: {
        regionId: undefined,
      },
      statusList: [
        {
          key: "-1",
          value: "请选择状态",
        },
        {
          key: "2",
          value: "正常",
        },
        {
          key: "0",
          value: "待提交",
        },
        {
          key: "1",
          value: "审核中",
        },
        {
          key: "3",
          value: "已注销",
        },
      ],
      //照片变量定义
      imagehost: "https://m.aerp.com.cn/",
      fileList: [],
    };
  },
  created() {
    this.fetchData();
  },
  watch: {},
  computed: {
    ...mapGetters(["userInfo"]),
  },
  methods: {
    fetchData() {
      this.getPaged();
      this.getProvinceByRegionId();
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
      this.condition.parentId = this.userInfo.organizationId;
      this.tableLoading = true;
      if (this.condition.status == "") {
        this.condition.status = "-1";
      }
      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getPageCompanyListForShopAsync(this.condition)
        .then(
          (res) => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (flag) {
              this.$message({
                message: res.message || "查询成功",
                type: "success",
              });
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          // console.log("tableLoading: false");
          this.tableLoading = false;
        });
    },

    add() {
      // console.log("新增按钮");
      this.formTitle = "新增子公司";
      this.formVisible = true;
      this.formModel.parentId = this.userInfo.organizationId;
    },
    edit(sel) {
      this.formTitle = "子公司详情";
      this.formVisible = true;
      this.getCompanyInfo(sel.id);
    },
    save(formName) {
      var vm = this;
      let objProvince = {};
      objProvince = this.provinceList.find((item) => {
        //model就是上面的数据源
        return item.regionId === this.formModel.provinceId; //筛选出匹配数据
      });
      //debugger;
      this.formModel.province = objProvince.name;
      let objCity = {};
      objCity = this.cityList.find((item) => {
        //model就是上面的数据源
        return item.regionId === this.formModel.cityId; //筛选出匹配数据
      });
      this.formModel.city = objCity.name;
      let objDistrict = {};
      objDistrict = this.districtList.find((item) => {
        //model就是上面的数据源
        return item.regionId === this.formModel.districtId; //筛选出匹配数据
      });
      this.formModel.district = objDistrict.name;
      console.log("save formModel: " + JSON.stringify(this.formModel));
      this.$refs[formName].validate((valid) => {
        if (valid) {
          vm.btnSaveLoading = true;
          appSvc
            .addOrUpdateCompany(this.formModel)
            .then(
              (res) => {
                if (res.code == 10000) {
                  this.cancel("formModel");
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                  this.getPaged();
                }
              },
              (err) => {
                console.log(err);
              }
            )
            .catch((err) => {
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
    //获取省
    getProvinceByRegionId() {
      this.regionProvince.regionId = 0;
      shopManageSvc
        .getRegionChinaListByRegionId(this.regionProvince)
        .then(
          (res) => {
            this.provinceList = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //获取市
    getCityByRegionId(provinceId) {
      console.log("provinceId :" + provinceId);
      if (
        this.condition.provinceId != "" &&
        this.condition.provinceId != undefined
      ) {
        this.regionCity.regionId = provinceId;
        this.cityList = [];
        shopManageSvc
          .getRegionChinaListByRegionId(this.regionCity)
          .then(
            (res) => {
              this.cityList = res.data;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      } else {
        this.condition.cityId = undefined;
        this.condition.districtId = undefined;
        this.cityList = [];
        this.districtList = [];
      }
    },
    //获取区
    getDistrictByRegionId(cityId) {
      if (this.condition.cityId != "" && this.condition.cityId != undefined) {
        this.regionDistrict.regionId = cityId;
        shopManageSvc
          .getRegionChinaListByRegionId(this.regionDistrict)
          .then(
            (res) => {
              this.districtList = res.data;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      } else {
        this.condition.districtId = undefined;
        this.districtList = [];
      }
    },
    //获取市
    getCityListByRegionId(provinceId) {
      console.log("provinceId :" + provinceId);
      if (
        this.formModel.provinceId != "" &&
        this.formModel.provinceId != undefined
      ) {
        this.regionCity.regionId = provinceId;
        this.cityList = [];
        shopManageSvc
          .getRegionChinaListByRegionId(this.regionCity)
          .then(
            (res) => {
              this.cityList = res.data;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      } else {
        this.formModel.cityId = undefined;
        this.formModel.districtId = undefined;
        this.cityList = [];
        this.districtList = [];
      }
    },
    //获取区
    getDistrictListByRegionId(cityId) {
      if (this.formModel.cityId != "" && this.formModel.cityId != undefined) {
        this.regionDistrict.regionId = cityId;
        shopManageSvc
          .getRegionChinaListByRegionId(this.regionDistrict)
          .then(
            (res) => {
              this.districtList = res.data;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      } else {
        this.formModel.districtId = undefined;
        this.districtList = [];
      }
    },
    beforeAvatarUpload(file) {
      const isLt2M = file.size / 1024 / 1024 < 5;
      if (!isLt2M) {
        this.$message.error("上传头像图片大小不能超过 5MB!");
      }
      return isLt2M;
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
    //获取token
    getToken(directoryName, fileName) {
      return shopManageSvc.getQiNiuToken({
        directory: directoryName,
        fileName: fileName,
      });
    },
    uploadRequest: function (request) {
      const loading = this.$loading({
        lock: true,
        text: "拼命上传中......",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });
      var name = request.filename;
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        shopManageSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        shopManageSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

      var directoryName = "Companys/Images";
      var key = directoryName + "/" + newFileName;
      this.getToken(directoryName, fileName)
        .then((res) => {
          var result = res.data;
          const token = result.token;
          const host = result.host;
          upload(
            token,
            key,
            request,
            (next) => {
              const total = next.total;
            },
            (error) => {
              this.fileList = [];
            },
            (complete) => {
              debugger;
              const hash = complete.hash;
              const key = complete.key;
              var url = host + "/" + key;
              this.formModel[name] = key;
            }
          );
        })
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    },
    handlePictureCardPreview(file) {
      this.dialogImageUrl = file.url;
      this.dialogVisible = true;
    },
    handleRemove1(file, fileList) {
      this.formModel.businessLicense = "";
    },
    handleRemove2(file, fileList) {
      this.formModel.accountOpeningLicense = "";
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

    //查询公司详情
    getCompanyInfo(id) {
      console.log("公司ID:" + id);
      appSvc
        .getCompanyInfo({ id: id })
        .then((res) => {
          let data = res.data;
          this.formModel = data;
          this.getCityListByRegionId(data.provinceId);
          this.getDistrictListByRegionId(data.cityId);
          this.formCheck = true;

          if (this.formModel.status == 0) {
            this.btnSaveVisible = "";
            this.btnStatusVisible = "";
          }
          if (this.formModel.status == 1) {
            this.btnStatusVisible = "none";
          }
          if (this.formModel.status == 2) {
            this.btnStatusVisible = "none";
          }
          if (this.formModel.status == 3) {
            this.btnSaveVisible = "none";
            this.btnStatusVisible = "none";
          }
        })
        .catch((err) => {
          console.error(err);
        })
        .finally(() => {});
    },
    updateCompanyStatus(type) {
      var vm = this;
      let objProvince = {};
      objProvince = this.provinceList.find((item) => {
        //model就是上面的数据源
        return item.regionId === this.formModel.provinceId; //筛选出匹配数据
      });
      this.formModel.province = objProvince.name;
      let objCity = {};
      objCity = this.cityList.find((item) => {
        //model就是上面的数据源
        return item.regionId === this.formModel.cityId; //筛选出匹配数据
      });
      this.formModel.city = objCity.name;
      let objDistrict = {};
      objDistrict = this.districtList.find((item) => {
        //model就是上面的数据源
        return item.regionId === this.formModel.districtId; //筛选出匹配数据
      });
      this.formModel.district = objDistrict.name;
      this.formModel.status = type;
      this.$refs["formModel"].validate((valid) => {
        if (valid) {
          vm.btnSaveLoading = true;
          appSvc
            .addOrUpdateCompany(this.formModel)
            .then(
              (res) => {
                if (res.code == 10000) {
                  this.cancel("formModel");
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                  this.getPaged();
                }
              },
              (err) => {
                console.log(err);
              }
            )
            .catch((err) => {
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
    updateCompany(type) {
      appSvc
        .updateCompanyStatus({
          CompanyId: this.formModel.id,
          Status: type,
        })
        .then((res) => {
          if (res.code == 10000) {
            this.cancel("formModel");
            this.$message({
              message: res.message,
              type: "success",
            });
            this.getPaged();
          }
        })
        .catch((err) => {
          console.error(err);
        })
        .finally(() => {});
    },
  },
};
</script>

<style lang="scss"  >
.upload-company {
  display: inline-block;
  max-height: 150px;
  overflow: hidden;
  > ul {
    max-width: 150px;
    overflow: hidden;
    display: inline-block;
  }
}
</style>
