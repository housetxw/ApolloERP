<template>
  <main class="container full-height">
    <!-- 新增和编辑页面 -->
    <section id="createOrUpdate" class="margin-top-30">
      <template>
        <el-form
          :model="formModel"
          :rules="rules"
          ref="formModel"
          :label-width="formLabelWidth"
        >
          <el-row>
            <el-form-item label="公司全称" prop="name">
              <el-col :span="8">
                <el-input
                  size="medium"
                  v-model="formModel.name"
                  placeholder="公司全称"
                  clearable
                  :disabled="formCheck"
                ></el-input>
              </el-col>
            </el-form-item>
          </el-row>
          <el-row>
            <el-form-item label="公司简称" prop="name">
              <el-col :span="8">
                <el-input
                  size="medium"
                  v-model="formModel.simpleName"
                  placeholder="公司简称"
                  clearable
                ></el-input>
              </el-col>
            </el-form-item>
          </el-row>
          <el-row>
            <el-form-item label="经营地址:" prop="province">
              <el-col :span="3">
                <el-select
                  v-model="formModel.provinceId"
                  @change="getCityListByRegionId"
                >
                  <el-option
                    v-for="item in provinceList"
                    :key="item.regionId"
                    :label="item.name"
                    :value="item.regionId"
                  ></el-option>
                </el-select>
              </el-col>
              <el-col :span="3">
                <el-select
                  v-model="formModel.cityId"
                  @change="getDistrictListByRegionId"
                >
                  <el-option
                    v-for="item in cityList"
                    :key="item.regionId"
                    :label="item.name"
                    :value="item.regionId"
                  ></el-option>
                </el-select>
              </el-col>
              <el-col :span="3">
                <el-select v-model="formModel.districtId">
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
            <el-form-item label="" prop="address">
              <el-col :span="9">
                <el-input
                  v-model="formModel.address"
                  placeholder="街道地址"
                ></el-input>
              </el-col>
            </el-form-item>
          </el-row>
          <el-row>
            <el-form-item label="联系电话">
              <el-col :span="4">
                <el-input
                  size="medium"
                  v-model="formModel.mobile"
                  placeholder="固定电话或手机号码"
                  clearable
                ></el-input>
              </el-col>
            </el-form-item>
          </el-row>
          <el-row>
            <el-form-item label="公司负责人">
              <el-col :span="4">
                <el-input
                  size="medium"
                  v-model="formModel.head"
                  placeholder="公司业务总负责人"
                  clearable
                ></el-input>
              </el-col>
              <el-col :span="4">
                <el-input
                  size="medium"
                  v-model="formModel.headPhone"
                  placeholder="联系电话"
                  clearable
                ></el-input>
              </el-col>
            </el-form-item>
          </el-row>
          <el-row>
            <el-form-item label="注册资金">
              <el-col :span="4">
                <el-input
                  size="medium"
                  v-model="formModel.registerMoney"
                  placeholder="0.00"
                  clearable
                ></el-input>
              </el-col>
              <el-col :span="4">
                <span class="s2">万元</span>
              </el-col>
            </el-form-item>
          </el-row>
          <el-row>
            <el-form-item label="工商注册日期" prop="registerTime">
              <el-col :span="4">
                <el-date-picker
                  v-model="formModel.registerTime"
                  type="date"
                  placeholder="年/月/日"
                  value-format="yyyy-MM-dd"
                  clearable
                />
              </el-col>
            </el-form-item>
          </el-row>
          <el-row>
            <el-form-item label="公司法人">
              <el-col :span="4">
                <el-input
                  size="medium"
                  v-model="formModel.legalPerson"
                  placeholder="法人名称"
                  clearable
                ></el-input>
              </el-col>
              <el-col :span="4">
                <el-input
                  size="medium"
                  v-model="formModel.legalPersonPhone"
                  placeholder="法人联系电话"
                  clearable
                ></el-input>
              </el-col>
            </el-form-item>
          </el-row>
          <!-- <el-row>
            <el-form-item label="类型:" prop="type">
              <el-radio-group v-model="formModel.type">
                <el-radio :label="0">公司</el-radio>
                <el-radio :label="2">仓库</el-radio>
                <el-radio :label="3">拓展</el-radio>
                <el-radio :label="4">供应商</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-row> -->
          <!-- <el-row>
            <el-form-item label="系统版本:" prop="systemType">
              <el-radio-group v-model="formModel.systemType">
                <el-radio :label="0">无</el-radio>
                <el-radio :label="1">平台公司</el-radio>
                <el-radio :label="2">普通公司 </el-radio>
                <el-radio :label="3">区域合伙公司 </el-radio>
              </el-radio-group>
            </el-form-item>
          </el-row> -->
          <!-- <el-row>
            <el-col :span="20">
              <el-form-item label>
                <label>营业执照（三证合一）扫描件或照片，须清晰可见</label>
                <el-upload
                  class="upload-demo"
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
                  <el-button size="small" type="primary">上传图片</el-button>
                </el-upload>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-form-item label="开户许可证">
              <el-col :span="4">
                <el-input
                  size="medium"
                  v-model="formModel.name"
                  placeholder
                  clearable
                  :disabled="true"
                ></el-input>
              </el-col>
            </el-form-item>
          </el-row>
          <el-row>
            <el-form-item label="">
              <el-col :span="4">
                <el-input
                  size="medium"
                  v-model="formModel.openingBank"
                  placeholder="开户银行"
                  clearable
                ></el-input>
              </el-col>
              <el-col :span="4">
                <el-input
                  size="medium"
                  v-model="formModel.bankAccount"
                  placeholder="账号"
                  clearable
                ></el-input>
              </el-col>
            </el-form-item>
          </el-row>
          <el-row>
            <el-col :span="20">
              <el-form-item>
                <label>开户许可证扫描件或照片，须清晰可见</label>
                <el-upload
                  class="upload-demo"
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
                  <el-button size="small" type="primary">上传图片</el-button>
                </el-upload>
              </el-form-item>
            </el-col>
          </el-row> -->
        </el-form>
      </template>
      <template>
        <el-button
          icon="el-icon-check"
          size="mini"
          type="primary"
          class="margin-left-100"
          @click="save('formModel')"
          >保存</el-button
        >
      </template>
    </section>
    <!-- 新增和编辑页面 -->
  </main>
</template>

<script>
import { mapGetters } from "vuex";
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/company/company";
import { shopManageSvc } from "@/api/shopmanage/shopmanage";
import { upload } from "@/utils/uploadfile";

export default {
  name: "CompanyInfo",
  data() {
    return {
      w_search_right: 100,
      tableLoading: false,
      flag: this.global.deletedFlag,
      name: "",
      provinceId: "",
      cityId: "",
      districtId: "",
      status: -1,
      tableLoading: false,
      formVisible: false,
      formCheck: false,
      btnSaveLoading: true,
      formInit: {
        id: 0,
        simpleName: "",
        name: "",
        parentId: 0,
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
      rules: {
        name: [{ required: true, message: "请输入公司全称", trigger: "blur" }],
        simpleName: [
          { required: true, message: "请输入公司简称", trigger: "blur" },
        ],
        province: [
          { required: true, message: "请选择省市区", trigger: "blur" },
        ],
        address: [
          { required: true, message: "请输入街道地址", trigger: "blur" },
        ],
        mobile: [
          { required: true, message: "请输入联系电话", trigger: "blur" },
          { max: 20, message: "联系电话长度不允许超过20", trigger: "blur" },
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

      //照片变量定义
      imagehost: "https://m.aerp.com.cn/",
      fileList: [],
    };
  },
  created() {
    this.fetchData();
  },
  computed: {
    ...mapGetters(["userInfo"]),
  },
  methods: {
    fetchData() {
      this.getProvinceByRegionId();
      this.getCompanyInfo();
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
                if (res.data) {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
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
    getCompanyInfo() {
      console.log("userInfo:" + JSON.stringify(this.userInfo));
      console.log("公司ID:" + this.userInfo.organizationId);
      appSvc
        .getCompanyInfo({ id: this.userInfo.organizationId })
        .then((res) => {
          let data = res.data;
          this.formModel = data;
          this.formModel.id = data.id;
          this.getCityListByRegionId(data.provinceId);
          this.getDistrictListByRegionId(data.cityId);
          this.formCheck = true;
        })
        .catch((err) => {
          console.error(err);
        })
        .finally(() => {});
    },
  },
};
</script>
