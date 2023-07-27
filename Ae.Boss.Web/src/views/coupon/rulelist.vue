<template>
  <main class="container">
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :tableData="tableData"
      :pageChange="pageChange"
      :headerBtnWidth="180"
      :searching="search"
    >
      <template v-slot:condition>
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="请输入优惠券名称"
            style="width: 180px"
            prefix-icon="el-icon-search"
            v-model="condition.displayName"
          >
          </el-input>
        </el-form-item>
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="优惠券ID"
            style="width: 120px"
            prefix-icon="el-icon-search"
            v-model="condition.id"
          >
          </el-input>
        </el-form-item>
        <el-form-item>
          <el-select
            style="width: 120px"
            v-model="condition.type"
            filterable
            placeholder="-请选择券类型-"
          >
            <el-option
              v-for="item in couponTypeEnum"
              :key="item.id"
              :label="item.displayName"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            style="width: 120px"
            v-model="condition.category"
            filterable
            placeholder="-请选择发行分类-"
          >
            <el-option
              v-for="item in categoryTypeEnum"
              :key="item.id"
              :label="item.displayName"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            style="width: 120px"
            v-model="condition.rangeType"
            filterable
            placeholder="-请选择使用范围-"
          >
            <el-option
              v-for="item in rangeTypeEnum"
              :key="item.id"
              :label="item.displayName"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="shopId">
          <el-select
            v-model="condition.shopId"
            filterable
            remote
            clearable
            reserve-keyword
            placeholder="请输入门店名称"
            :remote-method="getShopinfo"
            :loading="loading"
            style="width: 300px"
          >
            <el-option
              v-for="item in shopSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>

      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
          >添加</el-button
        >
      </template>

      <template v-slot:tb_cols>
        <el-table-column
          label="优惠券Id"
          prop="couponId"
          :width="80"
        ></el-table-column>
        <el-table-column
          label="优惠券名称"
          prop="displayName"
        ></el-table-column>
        <el-table-column label="优惠券规则名" prop="name"></el-table-column>
        <el-table-column label="发行分类" prop="categoryName"></el-table-column>
        <el-table-column label="券类型" prop="typeName"></el-table-column>
        <el-table-column
          label="券使用限制"
          prop="rangeTypeName"
        ></el-table-column>
        <el-table-column
          label="面值"
          prop="value"
          :width="100"
          align="right"
        ></el-table-column>
        <el-table-column
          label="阀值"
          prop="threshold"
          :width="100"
          align="right"
        ></el-table-column>
        <el-table-column
          label="范围门店编号"
          prop="shopIdList"
        ></el-table-column>
        <el-table-column label="创建时间" prop="createTime"></el-table-column>
        <el-table-column label="创建人" prop="createBy"></el-table-column>
        <el-table-column label="操作" :width="100">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                @click="routeDetail(scope.row)"
                type="primary"
                size="mini"
                >查看详情</el-button
              >
            </el-button-group>
          </template>
        </el-table-column>
      </template>
    </rg-page>

    <rg-dialog
      :title="formTitle"
      :visible.sync="dialogVisible"
      :beforeClose="cancel"
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          cancel();
        },
      }"
      :btnRemove="false"
      :footbar="true"
      width="80%"
      maxWidth="600px"
      minWidth="600px"
    >
      <template v-slot:content>
        <el-form :model="currentDetail" size="mini" label-width="120px">
          <el-form-item label="优惠券名称">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.displayName"
            ></el-input>
          </el-form-item>
          <el-form-item label="优惠券规则名">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.name"
            ></el-input>
          </el-form-item>
          <el-form-item label="发行分类">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.categoryName"
            ></el-input>
          </el-form-item>
          <el-form-item label="券类型">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.typeName"
            ></el-input>
          </el-form-item>
          <el-form-item label="券使用限制">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.rangeTypeName"
            ></el-input>
          </el-form-item>
          <el-form-item label="面值">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.value"
            ></el-input>
          </el-form-item>
          <el-form-item label="阀值">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.threshold"
            ></el-input>
          </el-form-item>
          <el-form-item label="图片Url">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.imageUrl"
            ></el-input>
          </el-form-item>
          <el-form-item label="支付方式">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.payMethodName"
            ></el-input>
          </el-form-item>
          <el-form-item label="适用规则描述">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.useRuleDesc"
            ></el-input>
          </el-form-item>
          <el-form-item label="门店区域配置">
            <el-select
              style="width: 300px"
              v-model="currentDetail.regionStrList"
              multiple
              disabled
              placeholder=""
            >
              <el-option
                v-for="item in currentDetail.regionStrList"
                :key="item"
                :label="item"
                :value="item"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="门店类型配置">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.shopTypeName"
            ></el-input>
          </el-form-item>
          <el-form-item label="门店配置">
            <el-select
              style="width: 300px"
              v-model="currentDetail.shopIdList"
              multiple
              disabled
              placeholder=""
            >
              <el-option
                v-for="item in currentDetail.shopIdList"
                :key="item"
                :label="item"
                :value="item"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="产品大类配置">
            <el-select
              style="width: 300px"
              v-model="currentDetail.categoryList"
              multiple
              disabled
              placeholder=""
            >
              <!-- <el-option
                                v-for="item in currentDetail.categoryList"
                                :key="item"
                                :label="item"
                                :value="item">
                            </el-option> -->
              <el-option
                v-for="item in productCategoryList"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="产品品牌配置">
            <el-select
              style="width: 300px"
              v-model="currentDetail.brandList"
              multiple
              disabled
              placeholder=""
            >
              <el-option
                v-for="item in currentDetail.brandList"
                :key="item"
                :label="item"
                :value="item"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="产品Pid配置">
            <el-select
              style="width: 300px"
              v-model="currentDetail.pidList"
              multiple
              disabled
              placeholder=""
            >
              <el-option
                v-for="item in currentDetail.pidList"
                :key="item"
                :label="item"
                :value="item"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="有效期开始类型">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.validStartTypeName"
            ></el-input>
          </el-form-item>
          <el-form-item
            label="最早开始日期"
            v-show="currentDetail.validStartType == 2"
          >
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.earliestUseDate"
            ></el-input>
          </el-form-item>
          <el-form-item label="有效期结束类型">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.validEndTypeName"
            ></el-input>
          </el-form-item>
          <el-form-item
            label="有效时长天数"
            v-show="currentDetail.validEndType == 1"
          >
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.validDays"
            ></el-input>
          </el-form-item>
          <el-form-item
            label="最晚使用日期"
            v-show="currentDetail.validEndType == 2"
          >
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.latestUseDate"
            ></el-input>
          </el-form-item>
          <el-form-item label="创建人">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.createBy"
            ></el-input>
          </el-form-item>
          <el-form-item label="创建时间">
            <el-input
              style="width: 300px"
              readonly
              v-model="currentDetail.createTime"
            ></el-input>
          </el-form-item>
        </el-form>
      </template>
    </rg-dialog>

    <rg-dialog
      :title="formTitle1"
      :visible.sync="dialogVisible1"
      :beforeClose="cancel1"
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          cancel1();
        },
      }"
      :btnRemove="false"
      :footbar="true"
      width="80%"
      maxWidth="600px"
      minWidth="600px"
    >
      <template v-slot:content>
        <el-form :model="addCouponVo" size="mini" label-width="120px">
          <el-form-item label="优惠券名称" required>
            <el-input
              style="width: 300px"
              placeholder="请输入优惠券名称"
              v-model="addCouponVo.displayName"
            ></el-input>
          </el-form-item>
          <el-form-item label="优惠券规则名" required>
            <el-input
              style="width: 300px"
              placeholder="请输入优惠券规则名"
              v-model="addCouponVo.name"
            ></el-input>
          </el-form-item>
          <el-form-item label="发行分类" required>
            <el-select style="width: 300px" v-model="addCouponVo.category">
              <el-option
                v-for="item in couponCategoryList"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="券类型" required>
            <el-select style="width: 300px" v-model="addCouponVo.type">
              <el-option
                v-for="item in couponTypeList"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="券使用限制" required>
            <el-select style="width: 300px" v-model="addCouponVo.rangeType">
              <el-option
                v-for="item in couponRangeType"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="面值" required>
            <el-input
              style="width: 300px"
              placeholder="请输入面值"
              v-model="addCouponVo.value"
            ></el-input>
          </el-form-item>
          <el-form-item label="阀值" required>
            <el-input
              style="width: 300px"
              placeholder="请输入阀值"
              v-model="addCouponVo.threshold"
            ></el-input>
          </el-form-item>
          <el-form-item label="图片">
            <el-image
              v-show="addCouponVo.imageUrl != ''"
              @click="routeImageDetail(addCouponVo.imageUrl)"
              style="width: 25px; height: 25px; margin-left: 10px"
              :src="addCouponVo.imageUrl"
            ></el-image>
            <el-upload
              style="float: left"
              action="http://upload.qiniup.com/"
              :before-upload="beforeAvatarUpload"
              :show-file-list="false"
              accept="image/jpeg, image/png, image/jpg"
              :http-request="uploadRequest"
            >
              <el-button size="mini" type="primary">点击上传</el-button>
            </el-upload>
          </el-form-item>
          <el-form-item label="支付方式">
            <el-select style="width: 300px" v-model="addCouponVo.payMethod">
              <el-option
                v-for="item in payMethod"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="适用规则描述" required>
            <el-input
              style="width: 300px"
              placeholder="请输入适用规则描述"
              v-model="addCouponVo.useRuleDesc"
            ></el-input>
          </el-form-item>
          <el-form-item label="门店区域配置">
            <el-select
              style="width: 100px"
              v-model="provinceModel"
              value-key="regionId"
              clearable
              filterable
              @change="getCityByRegionId"
            >
              <el-option
                v-for="item in provinceList"
                :key="item.regionId"
                :label="item.name"
                :value="item"
              >
              </el-option>
            </el-select>
            <el-select
              style="width: 100px"
              v-model="cityModel"
              value-key="regionId"
              clearable
              filterable
              @change="getDistrictByRegionId"
            >
              <el-option
                v-for="item in cityList"
                :key="item.regionId"
                :label="item.name"
                :value="item"
              >
              </el-option>
            </el-select>
            <el-select
              style="width: 100px"
              v-model="districtModel"
              value-key="regionId"
              clearable
              filterable
            >
              <el-option
                v-for="item in districtList"
                :key="item.regionId"
                :label="item.name"
                :value="item"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="门店类型配置">
            <el-select style="width: 300px" v-model="addCouponVo.shopType">
              <el-option
                v-for="item in shopTypeList"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="门店配置">
            <el-input
              style="width: 300px"
              placeholder="请输入门店编号，英文逗号隔开(例如：1,2,3)"
              v-model="addCouponVo.shopIdList"
            ></el-input>
          </el-form-item>
          <el-form-item label="产品大类配置">
            <el-select
              style="width: 300px"
              v-model="addCouponVo.categoryList"
              multiple
            >
              <el-option
                v-for="item in productCategoryList"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="产品品牌配置">
            <el-select
              style="width: 300px"
              v-model="addCouponVo.brandList"
              multiple
            >
              <el-option
                v-for="item in productBrandList"
                :key="item.id"
                :label="item.brandName"
                :value="item.brandName"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="产品Pid配置">
            <el-input
              style="width: 300px"
              placeholder="请输入产品pid，英文逗号隔开"
              v-model="addCouponVo.pidList"
            ></el-input>
          </el-form-item>
          <el-form-item label="有效期开始类型" required>
            <el-radio v-model="addCouponVo.validStartType" label="1"
              >领取当天生效</el-radio
            >
            <el-radio v-model="addCouponVo.validStartType" label="2"
              >指定开始日期</el-radio
            >
          </el-form-item>
          <el-form-item
            label="最早开始日期"
            required
            v-show="addCouponVo.validStartType == 2"
          >
            <el-date-picker
              style="width: 300px"
              v-model="addCouponVo.earliestUseDate"
              type="datetime"
              placeholder="请选择最早开始日期"
            >
            </el-date-picker>
          </el-form-item>
          <el-form-item label="有效期结束类型" required>
            <el-radio v-model="addCouponVo.validEndType" label="1"
              >持续指定天数</el-radio
            >
            <el-radio v-model="addCouponVo.validEndType" label="2"
              >指定截止日期</el-radio
            >
          </el-form-item>
          <el-form-item
            label="有效时长天数"
            required
            v-show="addCouponVo.validEndType == 1"
          >
            <el-input
              style="width: 300px"
              placeholder="请输入有效时长天数"
              v-model="addCouponVo.validDays"
            ></el-input>
          </el-form-item>
          <el-form-item
            label="最晚使用日期"
            required
            v-show="addCouponVo.validEndType == 2"
          >
            <el-date-picker
              style="width: 300px"
              v-model="addCouponVo.latestUseDate"
              type="datetime"
              placeholder="请选择最晚使用日期"
            >
            </el-date-picker>
          </el-form-item>
        </el-form>
      </template>

      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          size="mini"
          type="primary"
          :loading="btnSaveLoadingA"
          @click="submitCouponData()"
          >提交</el-button
        >
      </template>
    </rg-dialog>
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { couponSvc } from "@/api/coupon/coupon";
import { appSvc } from "@/api/baoyang/baoyang";
import { upload } from "@/utils/uploadfile";
import { shopManageSvc } from "@/api/shopmanage/shopmanage";
export default {
  name: "rulelist",
  data() {
    return {
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      loading: false,
      condition: {
        pageIndex: 1,
        pageSize: 20,
        id : "",
        displayName: "",
        type: 0,
        category: 0,
        rangeType: 0,
        shopId: undefined,
      },
      shopSel: [],
      shopSelCondition: {
        simpleName: undefined,
      },
      tableData: [],
      couponTypeEnum: [
        { id: 0, displayName: "券类型" },
        { id: 1, displayName: "满减券" },
        { id: 2, displayName: "实付券" },
      ],
      categoryTypeEnum: [
        { id: 0, displayName: "发行分类" },
        { id: 1, displayName: "总部发行" },
        { id: 2, displayName: "地方发行" },
      ],
      rangeTypeEnum: [
        { id: 0, displayName: "使用限制" },
        { id: 1, displayName: "线上线下通用" },
        { id: 2, displayName: "只限线上使用" },
        { id: 3, displayName: "只限线下使用" },
      ],
      formTitle: "优惠券详情",
      dialogVisible: false,
      currentDetail: {},
      formTitle1: "添加优惠券",
      dialogVisible1: false,
      addCouponVo: {
        displayName: "",
        name: "",
        category: 1,
        type: 1,
        rangeType: 1,
        value: "",
        threshold: "",
        imageUrl: "",
        payMethod: 0,
        shopType: 0,
        shopIdList: "",
        regionList: [],
        categoryList: [],
        brandList: [],
        pidList: "",
        useRuleDesc: "",
        validStartType: "1",
        validEndType: "1",
        validDays: "",
        earliestUseDate: "",
        latestUseDate: "",
      },
      couponCategoryList: [
        { id: 1, displayName: "总部发行" },
        { id: 2, displayName: "地方发行" },
        ],
      couponTypeList: [
        { id: 1, displayName: "满减券" },
        { id: 2, displayName: "实付券" },
        ],
      couponRangeType: [
        { id: 1, displayName: "线上线下通用" },
        { id: 2, displayName: "只限线上使用" },
        { id: 3, displayName: "只限线下使用" },
        ],
      payMethod: [],
      shopTypeList: [],
      productBrandList: [],
      productCategoryList: [],
      provinceList: [],
      cityList: [],
      districtList: [],
      provinceModel: {
        regionId: "",
        name: "",
      },
      cityModel: {
        regionId: "",
        name: "",
      },
      districtModel: {
        regionId: "",
        name: "",
      },
      btnSaveLoadingA: false,
    };
  },
  created() {
    // this.getCouponType();
    this.fetchData();
    // this.getCouponCategory();
    // this.getCouponTypeList();
    // this.getCouponRangeType();
    this.getPayMethod();
    this.getShopTypeEnum();
    this.getCategoryByParentId();
    this.getAllProductBrandList();
    this.getProvinceByRegionId();
  },
  methods: {
    getShopinfo(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.shopSelCondition.simpleName = query;
          couponSvc
            .getShopList(this.shopSelCondition)
            .then(
              (res) => {
                this.shopSel = res.data;
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {});
        }, 200);
      } else {
        this.options = [];
      }
    },
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
/*     getCouponType() {
      var couponTypeRequest = {
        showAll: true,
      };
      couponSvc
        .getCouponTypeEnum(couponTypeRequest)
        .then(
          (res) => {
            var data = res.data;
            this.couponTypeEnum = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    }, */
/*     getCouponCategory() {
      couponSvc
        .getCouponCategory()
        .then(
          (res) => {
            var data = res.data;
            this.couponCategoryList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    }, */
  /*   getCouponTypeList() {
      couponSvc
        .getCouponType()
        .then(
          (res) => {
            var data = res.data;
            this.couponTypeList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    }, */
/*     getCouponRangeType() {
      couponSvc
        .getCouponRangeType()
        .then(
          (res) => {
            var data = res.data;
            this.couponRangeType = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    }, */
    getPayMethod() {
      couponSvc
        .getPayMethod()
        .then(
          (res) => {
            var data = res.data;
            this.payMethod = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getShopTypeEnum() {
      couponSvc
        .getShopTypeEnum()
        .then(
          (res) => {
            var data = res.data;
            this.shopTypeList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getCategoryByParentId() {
      couponSvc
        .getCategoryByParentId()
        .then(
          (res) => {
            var data = res.data;
            this.productCategoryList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getAllProductBrandList() {
      couponSvc
        .getAllProductBrandList()
        .then(
          (res) => {
            var data = res.data;
            this.productBrandList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getPaged(flag) {
      this.tableLoading = true;
      if (this.condition.shopId == undefined || this.condition.shopId == "") {
        this.condition.shopId = 0;
      }
      if (this.condition.id == undefined || this.condition.id == "") {
        this.condition.id = 0;
      }
      couponSvc
        .getCouponRuleList(this.condition)
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
          this.condition.shopId = undefined;
        });
    },
    add() {
      this.addCouponVo = {
        displayName: "",
        name: "",
        category: 1,
        type: 1,
        rangeType: 1,
        value: "",
        threshold: "",
        imageUrl: "",
        payMethod: 0,
        shopType: 0,
        shopIdList: "",
        regionList: [],
        categoryList: [],
        brandList: [],
        pidList: "",
        useRuleDesc: "",
        validStartType: "1",
        validEndType: "1",
        validDays: "",
        earliestUseDate: "",
        latestUseDate: "",
      };
      this.cityList = [];
      this.districtList = [];
      this.provinceModel = {
        regionId: "",
        name: "",
      };
      this.cityModel = {
        regionId: "",
        name: "",
      };
      this.districtModel = {
        regionId: "",
        name: "",
      };
      this.dialogVisible1 = true;
    },
    routeDetail(cuDetail) {
      this.currentDetail = cuDetail;
      this.dialogVisible = true;
    },
    cancel() {
      this.dialogVisible = false;
    },
    cancel1() {
      this.dialogVisible1 = false;
    },
    beforeAvatarUpload(file) {
      const isLt2M = file.size / 1024 / 1024 < 5;
      if (!isLt2M) {
        this.$message.error("上传图片大小不能超过 5MB!");
      }
      return isLt2M;
    },
    uploadRequest: function (request) {
      const loading = this.$loading({
        lock: true,
        text: "拼命上传中......",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });
      debugger;
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        appSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        appSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;
      var directoryName = "BaoYang/Images";
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
              const hash = complete.hash;
              const key = complete.key;
              var url = host + "/" + key;
              this.addCouponVo.imageUrl = url;
            }
          );
        })
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    },
    //获取token
    getToken(directoryName, fileName) {
      return appSvc.getQiNiuToken({
        directory: directoryName,
        fileName: fileName,
      });
    },
    routeImageDetail(url) {
      window.open(url);
    },
    //获取省
    getProvinceByRegionId() {
      var regionCondition = {
        regionId: "0",
      };
      shopManageSvc
        .getRegionChinaListByRegionId(regionCondition)
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
    getCityByRegionId(val) {
      var regionId = val.regionId;
      this.cityList = [];
      this.cityModel = {
        regionId: "",
        name: "",
      };
      this.districtList = [];
      this.districtModel = {
        regionId: "",
        name: "",
      };
      if (regionId == "" || regionId == undefined) {
        return;
      }
      var regionCity = {
        regionId: regionId,
      };
      shopManageSvc
        .getRegionChinaListByRegionId(regionCity)
        .then(
          (res) => {
            this.cityList = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //获取区
    getDistrictByRegionId(val) {
      var regionId = val.regionId;
      this.districtList = [];
      this.districtModel = {
        regionId: "",
        name: "",
      };
      if (regionId == "" || regionId == undefined) {
        return;
      }
      var regionDistrict = {
        regionId: regionId,
      };
      shopManageSvc
        .getRegionChinaListByRegionId(regionDistrict)
        .then(
          (res) => {
            this.districtList = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    submitCouponData() {
      var s = /(^[1-9](\d+)?(\.\d{1,2})?$)|(^\d\.\d{1,2}$)/;
      var r = /^\+?[1-9][0-9]*$/; //正整数
      if (this.addCouponVo.displayName == "") {
        this.$message({ message: "请输入优惠券名称", type: "warning" });
        return;
      }
      if (this.addCouponVo.name == "") {
        this.$message({ message: "请输入优惠券规则名", type: "warning" });
        return;
      }
      if (this.addCouponVo.category == 0) {
        this.$message({ message: "请选择发行分类", type: "warning" });
        return;
      }
      if (this.addCouponVo.type == 0) {
        this.$message({ message: "请选择券类型", type: "warning" });
        return;
      }
      if (this.addCouponVo.rangeType == 0) {
        this.$message({ message: "请选择券使用范围", type: "warning" });
        return;
      }
      if (this.addCouponVo.value == "") {
        this.$message({ message: "请输入面值", type: "warning" });
        return;
      }
      if (!s.test(this.addCouponVo.value)) {
        this.$message({
          message: "面值输入有误，大于0且最多2位小数！",
          type: "warning",
        });
        return;
      }
      if (this.addCouponVo.threshold == "") {
        this.$message({ message: "请输入阀值", type: "warning" });
        return;
      }
      if (!s.test(this.addCouponVo.threshold)) {
        this.$message({
          message: "阀值输入有误，大于0且最多2位小数！",
          type: "warning",
        });
        return;
      }
      if (this.addCouponVo.useRuleDesc == "") {
        this.$message({ message: "请输入适用规则描述", type: "warning" });
        return;
      }
      var regionItem = {
        provinceId: "",
        cityId: "",
        districtId: "",
        province: "",
        city: "",
        district: "",
      };
      if (this.provinceModel != "") {
        regionItem.provinceId = this.provinceModel.regionId;
        regionItem.province = this.provinceModel.name;
      }
      if (this.cityModel != "") {
        regionItem.cityId = this.cityModel.regionId;
        regionItem.city = this.cityModel.name;
      }
      if (this.districtModel != "") {
        regionItem.districtId = this.districtModel.regionId;
        regionItem.district = this.districtModel.name;
      }
      this.addCouponVo.regionList = [];
      if (regionItem.provinceId != "" && regionItem.provinceId != undefined) {
        this.addCouponVo.regionList.push(regionItem);
      }
      if (this.addCouponVo.validStartType == 2) {
        if (this.addCouponVo.earliestUseDate == "") {
          this.$message({ message: "请选择最早开始日期", type: "warning" });
          return;
        }
      } else {
        this.addCouponVo.earliestUseDate = "";
      }
      if (this.addCouponVo.validEndType == 1) {
        this.addCouponVo.latestUseDate = "";
        if (this.addCouponVo.validDays == "") {
          this.$message({ message: "请输入有效时长天数", type: "warning" });
          return;
        }
        if (!r.test(this.addCouponVo.validDays)) {
          this.$message({
            message: "有效时长天数输入有误，请输入正整数！",
            type: "warning",
          });
          return;
        }
      } else {
        this.addCouponVo.validDays = "";
        if (this.addCouponVo.latestUseDate == "") {
          this.$message({ message: "请选择最晚使用日期", type: "warning" });
          return;
        }
      }
      this.btnSaveLoadingA = true;
      couponSvc
        .addCouponRule(this.addCouponVo)
        .then(
          (res) => {
            var data = res.data;
            this.$message({ message: "提交成功", type: "success" });
            this.dialogVisible1 = false;
            this.getPaged();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.btnSaveLoadingA = false;
        });
    },
  },
};
</script>
