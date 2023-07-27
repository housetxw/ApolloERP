<template>
  <main class="container">
    <section>
      <rg-page
        background
        id="indexPage"
        :pageIndex="condition.pageIndex"
        :pageSize="condition.pageSize"
        :dataCount="totalList"
        :tableLoading="tableLoading"
        :tableData="tableData"
        :pageChange="pageChange"
        :headerBtnWidth="180"
        @expand-change="loadChildTableData"
        :defaultCollapse="false"
        :searching="search"
        :getRowKeys="getRowKeys"
        :expands="expands"
      >
        <template v-slot:condition>
          <el-form-item>
            <el-input
              v-model="condition.name"
              style="width: 180px"
              clearable
              placeholder="公司名称"
            />
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.provinceId"
              clearable
              filterable
              placeholder="请选择省"
              @change="getCityByRegionId"
              class="margin-left-20"
            >
              <el-option
                v-for="item in provinceList"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.cityId"
              clearable
              filterable
              @change="getDistrictByRegionId"
              placeholder="请选择市"
            >
              <el-option
                v-for="item in cityList"
                :key="item.regionId"
                :label="item.name"
                :value="item.regionId"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.districtId"
              clearable
              filterable
              placeholder="请选择区"
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
              filterable
              placeholder="请选择审核状态"
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
          <el-button
            type="primary"
            size="mini"
            icon="el-icon-plus"
            @click="add()"
            >新增</el-button
          >
        </template>

        <!-- 搜索 -->
        <!-- 数据表格 -->
        <template v-slot:tb_cols>
          <el-table-column type="expand">
            <template>
              <el-table
                :data="childTableData"
                border
                style="width: 100%"
                size="small"
              >
                <el-table-column
                  label="编号"
                  prop="id"
                  align="center"
                ></el-table-column>
                <el-table-column
                  label="父级编号"
                  prop="parentId"
                  align="center"
                ></el-table-column>
                <el-table-column
                  label="公司全称"
                  prop="name"
                  align="center"
                ></el-table-column>
                <el-table-column
                  label="简称"
                  prop="simpleName"
                  align="center"
                ></el-table-column>
              </el-table>
            </template>
          </el-table-column>
          <el-table-column
            label="编号"
            prop="id"
            align="center"
          ></el-table-column>
          <el-table-column
            label="公司全称"
            prop="name"
            align="center"
          ></el-table-column>
          <el-table-column
            label="简称"
            prop="simpleName"
            align="center"
          ></el-table-column>
          <el-table-column
            label="门店数"
            align="right"
            prop="shopTotalCount"
            min-width="50"
          ></el-table-column>
          <el-table-column
            label="总负责人"
            prop="head"
            align="center"
            min-width="150px"
          >
            <template slot-scope="scope"
              >{{ scope.row.head }}
              {{ scope.row.headPhone ? +scope.row.headPhone : "" }}</template
            >
          </el-table-column>
          <el-table-column label="公司法人" prop="legalPerson" align="center" />
          <el-table-column label="省" prop="province" align="center" />
          <el-table-column label="市" prop="city" align="center" />
          <el-table-column label="区" prop="district" align="center" />
          <el-table-column
            label="注册资金(万元)"
            prop="registerMoney"
            width="120"
            align="right"
          />
          <el-table-column
            label="工商注册日期"
            prop="registerTime"
            :formatter="dateFormat"
            width="110"
            align="center"
          />
          <el-table-column label="状态" prop="status" align="center" width="80">
            <template slot-scope="scope">{{
              scope.row.status == 0
                ? "待提交"
                : scope.row.status == 1
                ? "审核中"
                : scope.row.status == 2
                ? "正常"
                : scope.row.status == 3
                ? "已注销"
                : "未通过"
            }}</template>
          </el-table-column>
          <el-table-column
            label="创建人"
            prop="createBy"
            align="center"
          ></el-table-column>
          <el-table-column
            label="创建时间"
            prop="createTime"
            align="center"
          ></el-table-column>
          <el-table-column
            label="修改人"
            prop="updateBy"
            align="center"
          ></el-table-column>
          <el-table-column label="修改时间" align="center">
            <template slot-scope="scope">
              <label
                v-if="scope.row.updateTime == '1900-01-01 00:00:00'"
              ></label>
              <label v-else>{{ scope.row.updateTime }}</label>
            </template>
          </el-table-column>
          <el-table-column label="操作" align="center" fixed="right" width="90">
            <template slot-scope="scope">
              <el-button-group>
                <el-button type="primary" size="mini" @click="edit(scope.row)"
                  >编辑</el-button
                >
                <el-button
                  type="primary"
                  size="mini"
                  v-show="scope.row.status == 1 ? true : false"
                  @click="audit(scope.row)"
                  >审核</el-button
                >
              </el-button-group>
            </template>
          </el-table-column>
        </template>
      </rg-page>
    </section>
    <!-- 首页：列表数据 -->

    <!-- 新增和编辑页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        :title="formTitle"
        :visible.sync="formVisible"
        v-if="formVisible"
        :footbar="true"
        :beforeClose="cancel"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            cancel('formModel');
          },
        }"
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
            <el-row v-show="formModel.id > 0">
              <el-form-item label="已支付押金">
                <el-col :span="6">
                  <el-input
                    v-model="formModel.depositAmount"
                    placeholder="押金"
                    :readonly="true"
                  >
                    <template slot="append">元</template>
                  </el-input>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-form-item label="供应商">
                <el-col :span="6">
                  <el-select
                    v-model="formModel.venderId"
                    clearable
                    filterable
                    placeholder="请选择供应商"
                    class="margin-left-20"
                  >
                    <el-option
                      v-for="item in venderList"
                      :key="item.id"
                      :label="item.venderShortName"
                      :value="item.id"
                    ></el-option>
                  </el-select>
                </el-col>
              </el-form-item>
            </el-row>
            <el-row>
              <el-col :span="14">
                <el-form-item label="类型:" prop="type">
                  <el-radio-group v-model="formModel.type">
                    <el-radio :label="0">公司</el-radio>
                    <el-radio :label="2">仓库</el-radio>
                    <el-radio :label="3">拓展 </el-radio>
                    <el-radio :label="4">供应商</el-radio>
                  </el-radio-group>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
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
            </el-row>
            <el-divider content-position="left">
              <h3>工商注册信息</h3>
            </el-divider>
            <el-row>
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
            </el-row>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            @click="updateCompanyStatus(3)"
            :style="{ display: btnDeleteVisible }"
            size="mini"
            type="danger"
            class="positinbtn_left"
            >注销</el-button
          >
          <el-button
            @click="updateCompanyStatus(2)"
            :style="{ display: btnRestoreVisble }"
            size="mini"
            type="danger"
            class="positinbtn_left"
            >恢复营业</el-button
          >
          <el-button
            type="primary"
            @click="save('formModel')"
            size="mini"
            icon="el-icon-check"
            >确 定</el-button
          >
          <el-button
            size="mini"
            icon="el-icon-check"
            type="primary"
            :style="{ display: btnStatusVisible }"
            :loading="btnSaveLoading"
            @click="updateCompany(1)"
            >提交</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 新增和编辑页面 -->
    <!-- 审核页面 -->
    <section>
      <rg-dialog
        :title="auditFormTitle"
        :visible.sync="auditFormVisible"
        v-if="auditFormVisible"
        :footbar="true"
        :beforeClose="auditCancel"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            auditCancel();
          },
        }"
        :btnRemove="{
          label: '驳回',
          click: (done) => {
            auditCancel();
          },
        }"
        maxWidth="900px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-form :model="companyStatusModel" ref="companyStatusModel">
            <el-form-item label="原因" size="50" :label-width="formLabelWidth">
              <el-input
                type="textarea"
                :rows="5"
                placeholder="请输入驳回原因"
                v-model="companyStatusModel.FailedExaminedReason"
              ></el-input>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            @click="updateCompanyStatus(4)"
            size="mini"
            type="danger"
            class="positinbtn_left"
            >审核驳回</el-button
          >
          <el-button
            type="primary"
            @click="updateCompanyStatus(2)"
            size="mini"
            icon="el-icon-check"
            >审核通过</el-button
          >
        </template>
      </rg-dialog>
    </section>

    <!-- 审核页面 -->
  </main>
</template>

<script>
import { Loading } from "element-ui";
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { shopManageSvc } from "@/api/shopmanage/shopmanage";
import { upload } from "@/utils/uploadfile";

export default {
  name: "companylist",
  data() {
    return {
      expands: [],
      getRowKeys(row) {
        return row.id;
      },
      w_search_right: 100,
      tableLoading: false,
      flag: this.global.deletedFlag,
      btnSaveVisible: "",
      btnStatusVisible: "",
      btnDeleteVisible: "",
      btnRestoreVisble: "none",
      name: "",
      provinceId: "",
      cityId: "",
      districtId: "",
      status: -1,
      currentPage: 1,
      tableData: [],
      totalList: 0,
      companyId: 0,
      condition: {
        pageIndex: 1,
        pageSize: 10,
        parentId: 1,
        name: undefined,
        provinceId: undefined,
        cityId: undefined,
        districtId: undefined,
        status: "-1",
      },
      formVisible: false,
      formTitle: "详情",
      auditFormVisible: false,
      auditFormTitle: "公司审核",
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
        depositAmount: 0,
      },
      formModel: {
        id: 0,
        simpleName: "",
        name: "",
        parentId: 0,
        status: 0,
        type: 0,
        level: 0,
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
        depositAmount: 0,
        venderId: undefined,
        systemType: 0,
      },
      venderList: [],
      companyStatusModel: {
        CompanyId: undefined,
        Status: undefined,
        FailedExaminedReason: undefined,
      },
      companyStatusModelInit: {
        CompanyId: undefined,
        Status: undefined,
        FailedExaminedReason: undefined,
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
      childTableData: [],
      expands: [],
      subCompanyCondition: {
        parentId: undefined,
      },
    };
  },
  created() {
    this.fetchData();
  },
  watch: {},
  methods: {
    loadChildTableData(row, expandedRows) {
      this.subCompanyCondition.parentId = row.id;
      shopManageSvc
        .getCompanySubs(this.subCompanyCondition)
        .then(
          (res) => {
            this.childTableData = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          // this.tableLoading = false;
        });
    },
    fetchData() {
      debugger;
      this.getPaged();
      this.getProvinceByRegionId();
    },
    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    },
    // pageChange(p) {
    //   this.condition.pageIndex = p.pageIndex;
    //   this.condition.pageSize = p.pageSize;
    //   this.getPaged();
    // },
    pageChange(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    sizeChange(val) {
      this.condition.pageIndex = this.currentPage = 1;
      this.condition.pageSize = val;
      this.getPaged();
    },
    getPaged(flag) {
      this.tableLoading = true;
      if (this.condition.status == "") {
        this.condition.status = "-1";
      }
      console.log("condition: " + JSON.stringify(this.condition));
      shopManageSvc
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
          this.tableLoading = false;
        });
    },

    dateFormat(row, column, cellValue, index) {
      const daterc = row[column.property];
      if (daterc != null) {
        const dateMat = new Date(daterc);
        const year = dateMat.getFullYear();
        const month = dateMat.getMonth() + 1;
        const day = dateMat.getDate();
        const timeFormat = year + "-" + month + "-" + day;
        return timeFormat;
      }
    },

    add() {
      this.formTitle = "新增公司";
      this.formVisible = true;
      this.btnDeleteVisible = "none";
      this.btnRestoreVisble = "none";
      this.getVenders();
    },

    getVenders() {
      shopManageSvc
        .getVenders()
        .then(
          (res) => {
            this.venderList = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    edit(sel) {
      this.formTitle = "公司详情";
      this.formVisible = true;
      this.getCompanyInfo(sel.id);
      this.companyStatusModel.CompanyId = sel.id;
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
          shopManageSvc
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
    audit(row) {
      this.auditFormTitle = "审核【" + row.simpleName + "】";
      this.auditFormVisible = true;
      this.companyStatusModel.CompanyId = row.id;
    },
    auditCancel() {
      this.auditFormVisible = false;
      this.companyId = 0;
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
      this.venderList = [];
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
      shopManageSvc
        .getCompanyInfo({ id: id })
        .then((res) => {
          let data = res.data;
          console.log("公司信息:" + JSON.stringify(data));
          this.formModel = data;
          this.getCityListByRegionId(data.provinceId);
          this.getDistrictListByRegionId(data.cityId);
          this.formCheck = true;
          this.getVenders();
          if (this.formModel.status == 0) {
            this.btnSaveVisible = "";
            this.btnStatusVisible = "";
            this.btnRestoreVisble = "none";
          }
          if (this.formModel.status == 1) {
            this.btnStatusVisible = "none";
            this.btnRestoreVisble = "none";
          }
          if (this.formModel.status == 2) {
            this.btnStatusVisible = "none";
            this.btnRestoreVisble = "none";
          }
          if (this.formModel.status == 3) {
            this.btnSaveVisible = "none";
            this.btnStatusVisible = "none";
            this.btnDeleteVisible = "none";
            this.btnRestoreVisble = "";
          }
          if (this.formModel.status != 3) {
            this.btnDeleteVisible = "";
            this.btnRestoreVisble = "none";
          }
        })
        .catch((err) => {
          console.error(err);
        })
        .finally(() => {});
    },
    updateCompany(type) {
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
          shopManageSvc
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
    updateCompanyStatus(type) {
      var msg = type == 2 ? "审核通过" : type == 3 ? "注销" : "驳回";
      this.$confirm("确定" + msg + "吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          var vm = this;
          this.companyStatusModel.Status = type;
          if (type == 4) {
            if (
              this.companyStatusModel.FailedExaminedReason == "" ||
              this.companyStatusModel.FailedExaminedReason == undefined
            ) {
              this.$message({
                message: "请填写驳回理由!",
                type: "warning",
              });
              return false;
            }
          }
          shopManageSvc
            .updateCompanyStatus(this.companyStatusModel)
            .then(
              (res) => {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success",
                  });
                }
                this.formVisible = false;
                this.formCheck = false;
                this.auditFormVisible = false;
                this.companyStatusModel = JSON.parse(
                  JSON.stringify(this.companyStatusModelInit)
                );
                vm.search();
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
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
.container {
  margin-left: 30px;
  .bodyTop {
    height: 128px;
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
.positinbtn_left {
  position: absolute;
  left: 5%;
}
>>> .el-form--inline .el-form-item {
  padding-bottom: 10px !important ;
}
</style>
