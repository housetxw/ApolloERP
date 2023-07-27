<template>
  <main class="container full-height">
    <!-- 编辑页面 -->
    <section id="createOrUpdate" class="margin-top-30">
      <template>
        <el-form
          :model="shop"
          ref="shop"
          :rules="rules"
          :label-width="formLabelWidth"
          size="mini"
          class="input-width-fill"
        >
          <!-- <el-form-item label="隶属公司">
            <label>{{ userInfo.organizationName }}</label>
          </el-form-item>-->
          <el-divider content-position="left">基本信息</el-divider>
          <el-form-item label="门店全称" prop="fullName">
            <el-col :span="12">
              <label v-if="formCheck">{{ shop.fullName }}</label>
              <el-input
                v-else
                v-model="shop.fullName"
                placeholder="门店全称"
                :disabled="formCheck"
                clearable
              ></el-input>
            </el-col>
          </el-form-item>
          <el-form-item label="门店简称" prop="simpleName">
            <el-col :span="12">
              <label v-if="formCheck">{{ shop.simpleName }}</label>
              <el-input
                v-else
                v-model="shop.simpleName"
                placeholder="门店简称"
                :disabled="formCheck"
                clearable
              ></el-input>
            </el-col>
          </el-form-item>
          <el-form-item label="门店联系人" required>
            <el-col :span="6">
              <el-form-item prop="contact">
                <el-input
                  v-model="shop.contact"
                  class="no-radius-right"
                  placeholder="联系人名称"
                  clearable
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item>
                <el-input
                  v-model="shop.telephone"
                  placeholder="门店固定电话"
                  clearable
                  class="no-radius-left no-radius-right"
                >
                  <template slot="prefix">固话</template>
                </el-input>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item prop="mobile">
                <el-input
                  v-model="shop.mobile"
                  placeholder="门店手机号码"
                  clearable
                  class="no-radius-left"
                >
                  <template slot="prefix">手机</template>
                </el-input>
              </el-form-item>
            </el-col>
          </el-form-item>
          <el-form-item label="门店老板" required>
            <el-col :span="6">
              <el-form-item prop="ownerName">
                <el-input
                  v-model="shop.ownerName"
                  placeholder="门店老板名称"
                  clearable
                  class="no-radius-right"
                ></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item prop="ownerPhone">
                <el-input
                  v-model="shop.ownerPhone"
                  placeholder="门店老板手机号码"
                  clearable
                  class="no-radius-left"
                >
                  <template slot="prefix">手机</template>
                </el-input>
              </el-form-item>
            </el-col>
          </el-form-item>
          <el-form-item label="主管" prop="charge">
            <el-col :span="6">
              <label v-if="formCheck"
                >{{ shop.charge }}{{ shop.chargePhone }}</label
              >
            </el-col>
          </el-form-item>
          <el-form-item label="门店标签:">
            <el-tag
              :key="tag"
              v-for="tag in tags"
              closable
              effect="dark"
              :disable-transitions="false"
              @close="handleClose(tag)"
              >{{ tag }}</el-tag
            >
            <el-button type="text" @click="addTag()">添加标签</el-button>
          </el-form-item>
          <el-form-item label="专修品牌">
            <el-tag
              :key="tag"
              v-for="tag in selectBrands"
              closable
              effect="dark"
              :disable-transitions="false"
              @close="handleClose2(tag)"
              >{{ tag }}</el-tag
            >
            <el-button type="text" @click="addBrand()">添加专修品牌</el-button>
          </el-form-item>
          <!-- <el-form-item label="门店类型" prop="type">
            <el-radio-group v-model="shop.type">
              <el-radio :label="1">合作店</el-radio>
              <el-radio :label="2">自营店</el-radio>
            </el-radio-group>
          </el-form-item> -->
          <el-form-item label="营业状态" prop="status">
            <el-radio-group v-model="shop.status" @change="changeStatus()">
              <el-radio :label="0">正常营业</el-radio>
              <el-radio :label="1">终止合作</el-radio>
              <el-radio :label="2">暂停营业</el-radio>
            </el-radio-group>
            <div v-show="showTime">
              <label
                >开始时间:{{
                  shopConfig.suspendStartDateTime == "1900-01-01 00:00:00"
                    ? "无"
                    : shopConfig.suspendStartDateTime
                }}</label
              >&nbsp;&nbsp; &nbsp;
              <label
                >结束时间:{{
                  shopConfig.suspendEndDateTime == "1900-01-01 00:00:00"
                    ? "无"
                    : shopConfig.suspendEndDateTime
                }}</label
              >
              &nbsp;&nbsp;
              <el-button type="text" @click="modifySuspendTime()"
                >修改暂停营业时间</el-button
              >
            </div>
          </el-form-item>
          <el-form-item label="上下架" prop="online">
            <el-radio-group v-model="shop.online">
              <el-radio :label="0">下架</el-radio>
              <el-radio :label="1">上架</el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item label="经营地址" prop="shopaddress">
            <el-col :span="6">
              <el-select
                v-model="shop.provinceId"
                @change="getCityListByRegionId"
                style="width: 100%"
                class="no-radius-right"
                prop="province"
              >
                <el-option
                  v-for="item in provinceSel"
                  :key="item.regionId"
                  :label="item.name"
                  :value="item.regionId"
                ></el-option>
              </el-select>
            </el-col>
            <el-col :span="6">
              <el-select
                v-model="shop.cityId"
                @change="getDistrictListByRegionId"
                class="no-radius-right no-radius-left"
                prop="city"
                style="width: 100%"
              >
                <el-option
                  v-for="item in citySel"
                  :key="item.regionId"
                  :label="item.name"
                  :value="item.regionId"
                ></el-option>
              </el-select>
            </el-col>
            <el-col :span="6">
              <el-select
                v-model="shop.districtId"
                prop="district"
                style="width: 100%"
                class="no-radius-left"
              >
                <el-option
                  v-for="item in districtSel"
                  :key="item.regionId"
                  :label="item.name"
                  :value="item.regionId"
                ></el-option>
              </el-select>
            </el-col>
          </el-form-item>
          <el-form-item label="街道地址" prop="address">
            <el-col :span="12">
              <el-input
                v-model="shop.address"
                placeholder="街道地址"
                class="no-radius-right"
                clearable
              ></el-input>
            </el-col>
            <el-col :span="6">
              <el-input
                v-model="shop.post"
                placeholder="邮政编码"
                class="no-radius-left"
                clearable
              >
                <template slot="prefix">邮编</template>
              </el-input>
            </el-col>
          </el-form-item>
          <el-form-item label="纬/经度" required>
            <el-col :span="6">
              <el-form-item prop="latitude">
                <el-input
                  v-model="shop.latitude"
                  placeholder="请输入纬度"
                  class="no-radius-right"
                  clearable
                >
                  <template slot="prefix">纬度</template>
                </el-input>
              </el-form-item>
            </el-col>
            <el-col style="width: 355px">
              <el-form-item prop="longitude">
                <el-input
                  v-model="shop.longitude"
                  placeholder="请输入经度"
                  class="no-radius-left"
                  clearable
                >
                  <template slot="prefix">经度</template>
                  <el-button slot="append">
                    <a href="http://www.gpsspg.com/maps.htm" target="_blank"
                      >打开地图定位</a
                    >
                  </el-button>
                  <el-button
                    slot="append"
                    icon="el-icon-question"
                    @click="mapQuestion()"
                  ></el-button>
                </el-input>
              </el-form-item>
            </el-col>
          </el-form-item>
          <el-row>
            <section :style="{ display: mapShow }">
              <img style="width: 100%" src="@/assets/shop/shop_map.jpg" alt />
            </section>
          </el-row>
          <el-row>
            <el-divider content-position="left">配置信息</el-divider>
          </el-row>

          <el-form-item label="正式上线日期">
            <el-col :span="6">
              <el-date-picker
                v-model="shopConfig.onlineTime"
                type="date"
                placeholder="正式上线日期"
                format="yyyy-MM-dd"
                value-format="yyyy-MM-dd"
                clearable
              />
            </el-col>
          </el-form-item>

          <el-form-item label="营业时间">
            <el-col :span="3">
              <el-form-item>
                <el-time-picker
                  v-model="shopConfig.startWorkTime"
                  type="fixed-time"
                  format="HH:mm"
                  value-format="HH:mm"
                  class="no-radius-right"
                  placeholder="开始时间"
                />
              </el-form-item>
            </el-col>
            <el-col :span="3">
              <el-form-item>
                <el-time-picker
                  v-model="shopConfig.endWorkTime"
                  format="HH:mm"
                  class="no-radius-left"
                  value-format="HH:mm"
                  type="fixed-time"
                  placeholder="结束时间:"
                />
              </el-form-item>
            </el-col>
          </el-form-item>
          <el-form-item label="租赁日期">
            <el-col :span="6">
              <el-date-picker
                v-model="shopConfig.leaseStartDate"
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
                v-model="shopConfig.leaseEndDate"
                type="date"
                class="no-radius-left"
                placeholder="结束日期"
                format="yyyy-MM-dd"
                value-format="yyyy-MM-dd"
                clearable
              />
            </el-col>
          </el-form-item>
          <el-form-item label="休息室">
            <el-radio-group v-model="shopConfig.isLoungeRoom">
              <el-radio :label="0">无</el-radio>
              <el-radio :label="1">有</el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item label="卫生间">
            <el-radio-group v-model="shopConfig.isRestRoom">
              <el-radio :label="0">无</el-radio>
              <el-radio :label="1">有</el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item label="免费wifi">
            <el-radio-group v-model="shopConfig.isFreeWifi">
              <el-radio :label="0">无</el-radio>
              <el-radio :label="1">有</el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item label="柱式举升机">
            <el-radio-group v-model="shopConfig.isPostLift">
              <el-radio :label="0">无</el-radio>
              <el-radio :label="1">有</el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item label="汽车故障诊断仪">
            <el-radio-group v-model="shopConfig.carFaultDiagnosticTool">
              <el-radio :label="0">无</el-radio>
              <el-radio :label="1">有</el-radio>
            </el-radio-group>
          </el-form-item>

          <el-divider content-position="left">
            图片信息
            <span style="color: red; margin-left: 10px"
              >上传图片大小不能超过5MB</span
            >
          </el-divider>

          <!-- 图片信息开始 -->
          <section>
            <el-form-item label="门头照片">
              <el-upload
                class="upload-demo"
                action="http://upload.qiniup.com/"
                :on-preview="handlePictureCardPreview"
                :on-remove="handleRemove1"
                :before-remove="beforeRemove"
                multiple
                :limit="1"
                :on-exceed="handleExceed"
                :file-list="fileList1"
                list-type="picture-card"
                :http-request="uploadRequest1"
                name="headImg"
                :before-upload="beforeAvatarUpload"
                accept="image/jpeg, image/png, image/jpg"
              >
                <i class="el-icon-plus"></i>
              </el-upload>
            </el-form-item>
            <el-form-item label="正面照片">
              <el-upload
                class="upload-demo"
                action="http://upload.qiniup.com/"
                :on-preview="handlePictureCardPreview"
                :on-remove="handleRemove2"
                :before-remove="beforeRemove"
                multiple
                :before-upload="beforeAvatarUpload"
                :on-exceed="handleExceed"
                :file-list="fileList2"
                list-type="picture-card"
                :http-request="uploadRequest2"
                accept="image/jpeg, image/png, image/jpg"
                name="frontImg"
              >
                <i class="el-icon-plus"></i>
              </el-upload>
            </el-form-item>
            <el-form-item label="门店照片">
              <el-upload
                class="upload-demo"
                action="http://upload.qiniup.com/"
                :on-preview="handlePictureCardPreview"
                :on-remove="handleRemove3"
                :before-remove="beforeRemove"
                multiple
                :before-upload="beforeAvatarUpload"
                accept="image/jpeg, image/png, image/jpg"
                :on-exceed="handleExceed"
                :file-list="fileList3"
                list-type="picture-card"
                :http-request="uploadRequest3"
                name="shopImgs"
              >
                <i class="el-icon-plus"></i>
              </el-upload>
            </el-form-item>
            <el-form-item label="资质证明">
              <el-upload
                class="upload-demo"
                action="http://upload.qiniup.com/"
                :on-preview="handlePictureCardPreview"
                :on-remove="handleRemove4"
                :before-remove="beforeRemove"
                multiple
                :before-upload="beforeAvatarUpload"
                :on-exceed="handleExceed"
                :file-list="fileList4"
                list-type="picture-card"
                :http-request="uploadRequest4"
                name="shopProofImgs"
              >
                <i class="el-icon-plus"></i>
              </el-upload>
            </el-form-item>
            <el-form-item label="营业执照">
              <el-upload
                class="upload-demo"
                action="http://upload.qiniup.com/"
                :on-preview="handlePictureCardPreview"
                :on-remove="handleRemove5"
                :before-remove="beforeRemove"
                multiple
                :before-upload="beforeAvatarUpload"
                :on-exceed="handleExceed"
                :file-list="fileList5"
                list-type="picture-card"
                :http-request="uploadRequest5"
                name="shopProofImgs"
              >
                <i class="el-icon-plus"></i>
              </el-upload>
            </el-form-item>
            <el-form-item label="经营许可证:">
              <el-upload
                class="upload-demo"
                action="http://upload.qiniup.com/"
                :on-preview="handlePictureCardPreview"
                :on-remove="handleRemove6"
                :before-remove="beforeRemove"
                multiple
                :before-upload="beforeAvatarUpload"
                :on-exceed="handleExceed"
                :file-list="fileList6"
                list-type="picture-card"
                :http-request="uploadRequest6"
                name="shopProofImgs"
              >
                <i class="el-icon-plus"></i>
              </el-upload>
            </el-form-item>
          </section>
          <!-- 图片信息结束 -->
          <!-- 账户信息开始 -->
          <!-- <el-divider content-position="left">银行开户账户</el-divider>
          <el-form-item label="账号类型">
            <el-radio-group
              v-model="shopBankCard.type"
              @change="changeAccountType()"
            >
              <el-radio :label="0">个人账号</el-radio>
              <el-radio :label="1">企业账号</el-radio>
            </el-radio-group>
          </el-form-item>
          <section :style="{ display: accountType1 }">
            <el-form-item label="开户银行">
              <el-col :span="6">
                <el-autocomplete
                  class="no-radius-right"
                  v-model="shopBankCard.openingBank"
                  :fetch-suggestions="querySearch"
                  placeholder="开户银行"
                  style="width: 100%"
                  @select="handleSelect"
                ></el-autocomplete>
              </el-col>
              <el-col :span="6">
                <el-input
                  v-model="shopBankCard.openingBranch"
                  class="no-radius-left"
                >
                  <template slot="prefix">支行</template>
                </el-input>
              </el-col>
            </el-form-item>
            <el-form-item label="开户人">
              <el-col :span="6">
                <el-input
                  v-model="shopBankCard.openingUserName"
                  class="no-radius-right"
                >
                  <template slot="prefix">姓名</template>
                </el-input>
              </el-col>
              <el-col :span="6">
                <el-input
                  v-model="shopBankCard.bankCardNo"
                  class="no-radius-left"
                >
                  <template slot="prefix">卡号</template>
                </el-input>
              </el-col>
            </el-form-item>
          </section>
          <section :style="{ display: accountType2 }">
            <el-form-item label="开户许可证">
              <el-col :span="12">
                <el-input
                  v-model="shopBankCard.companyName"
                  placeholder="企业名称"
                />
              </el-col>
            </el-form-item>
            <el-form-item label="开户银行">
              <el-col :span="6">
                <el-autocomplete
                  class="inline-input no-radius-right"
                  v-model="shopBankCard.openingBank"
                  :fetch-suggestions="querySearch"
                  placeholder="开户银行"
                  @select="handleSelect"
                  style="width: 100%"
                ></el-autocomplete>
              </el-col>
              <el-col :span="6">
                <el-input
                  v-model="shopBankCard.bankCardNo"
                  placeholder="请输入开户人账号"
                  class="no-radius-left"
                >
                  <template slot="prefix">账号</template>
                </el-input>
              </el-col>
            </el-form-item> 
            <label style="color: red; font-weight: 600; margin: 20px 120px"
              >开户许可证扫描件或照片，必须清晰</label
            >
            <el-form-item label>
              <el-upload
                class="upload-demo"
                action="http://upload.qiniup.com/"
                :on-preview="handlePictureCardPreview"
                :on-remove="handleRemove7"
                :before-remove="beforeRemove"
                multiple
                :before-upload="beforeAvatarUpload"
                :on-exceed="handleExceed"
                :file-list="
                  shopBankCard.openingLicence
                    ? [
                        {
                          name: '',
                          url: imagehost + shopBankCard.openingLicence,
                        },
                      ]
                    : []
                "
                list-type="picture-card"
                :http-request="uploadRequest7"
                name="openingLicence"
              >
                <i class="el-icon-plus"></i>
              </el-upload>
            </el-form-item>
          </section>-->
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
    <!-- 编辑页面 -->

    <!-- 暂停营业时间页面开始 -->
    <section id="shopStatus">
      <rg-dialog
        :title="statusFormTitle"
        :close-on-click-modal="false"
        :visible.sync="statusFormVisible"
        :before-close="statuscancel"
        :beforeClose="cancel"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            statuscancel('statusFormModel');
          },
        }"
        v-if="statusFormVisible"
        :footbar="true"
        width="90%"
        maxWidth="1024px"
        minWidth="900px"
      >
        <template v-slot:content>
          <el-form :model="statusFormModel" ref="statusFormModel">
            <el-form-item label="暂停营业时间" :label-width="formLabelWidth">
              <el-date-picker
                v-model="statusFormModel.stopTime"
                type="datetimerange"
                :picker-options="sendpickerOptions"
                range-separator="至"
                start-placeholder="开始日期"
                end-placeholder="结束日期"
                align="right"
              ></el-date-picker>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button-group>
            <el-button
              icon="el-icon-check"
              size="mini"
              type="primary"
              :loading="btnSaveLoading"
              @click="statusSave('statusFormModel')"
              >确定</el-button
            >
          </el-button-group>
        </template>
      </rg-dialog>
    </section>
    <!-- 暂停营业时间页面结束 -->

    <!-- 添加标签 -->
    <section id="posterDetailDialog">
      <rg-dialog
        :title="tagFormTitle"
        :close-on-click-modal="false"
        :visible.sync="tagFormVisible"
        :before-close="tagCancel"
        :beforeClose="tagCancel"
        :btnRemove="false"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            tagCancel();
          },
        }"
        v-if="tagFormVisible"
        :footbar="true"
        width="500px"
      >
        <template v-slot:content>
          <el-form :model="tagFormModel" ref="tagFormModel">
            <el-form-item label="标签" :label-width="formLabelWidth">
              <el-input
                clearable
                placeholder="输入标签名称"
                v-model="tagFormModel.tagName"
              ></el-input>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button-group>
            <el-button
              icon="el-icon-check"
              size="mini"
              type="primary"
              :loading="btnSaveLoading"
              @click="tagSave()"
              >确定</el-button
            >
          </el-button-group>
        </template>
      </rg-dialog>
    </section>
    <!-- 添加标签 -->
    <!-- 添加专修品牌 -->
    <section id="brandDialog">
      <rg-dialog
        :title="brandFormTitle"
        :close-on-click-modal="false"
        :visible.sync="brandFormVisible"
        :before-close="brandCancel"
        :beforeClose="brandCancel"
        :btnRemove="false"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            brandCancel();
          },
        }"
        v-if="brandFormVisible"
        :footbar="true"
        width="600px"
      >
        <template v-slot:content>
          <el-form :model="brandFormModel" ref="brandFormModel">
            <el-form-item label="选择专修品牌" :label-width="formLabelWidth">
              <el-select
                v-model="brandFormModel.brandSuffix"
                clearable
                filterable
                multiple
                placeholder="选择专修品牌"
              >
                <el-option
                  v-for="item in brands"
                  :key="item.brandSuffix"
                  :label="item.brandSuffix"
                  :value="item.brandSuffix"
                ></el-option>
              </el-select>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button-group>
            <el-button
              icon="el-icon-check"
              size="mini"
              type="primary"
              :loading="btnSaveLoading"
              @click="brandSave()"
              >确定</el-button
            >
          </el-button-group>
        </template>
      </rg-dialog>
    </section>
    <!-- 添加专修品牌 -->
  </main>
</template>

<script>
import { mapGetters } from "vuex";
import { Loading } from "element-ui";
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { shopManageSvc } from "@/api/shopmanage/shopmanage";
import { empSvc } from "@/api/accountauthority/employee";
import { upload } from "@/utils/uploadfile";

export default {
  name: "shopinfo",
  data() {
    var longreg = (rule, value, callback) => {
      console.log("rule", rule);
      if (
        !/^[\-\+]?(0(\.\d{1,10})?|([1-9](\d)?)(\.\d{1,10})?|1[0-7]\d{1}(\.\d{1,10})?|180\.0{1,10})$/.test(
          value
        )
      ) {
        return callback(new Error("经度整数部分为0-180,小数部分为0到8位!"));
      }
    };
    var latreg = (rule, value, callback) => {
      console.log("rule", rule);
      if (
        !/^[\-\+]?((0|([1-8]\d?))(\.\d{1,10})?|90(\.0{1,10})?)$/.test(value)
      ) {
        return callback(new Error("纬度整数部分为0-90,小数部分为0到8位!"));
      }
    };

    return {
      w_search_right: 100,
      tableLoading: false,
      flag: this.global.deletedFlag,
      formLabelWidth: "120px",
      currentPage: 1,
      tableData: [],
      totalList: 0,
      formVisible: false,
      formTitle: "详情",
      formCheck: false,
      btnSaveLoading: false,
      mapShow: "none",
      restaurants: [],

      //table页面的条件
      condition: {
        pageIndex: 1,
        pageSize: 10,
        parentId: 0,
        simpleName: undefined,
        fullName: undefined,
        provinceId: undefined,
        cityId: undefined,
        districtId: undefined,
        onLineStatus: "2",
      },

      provinceSel: [],
      citySel: [],
      districtSel: [],
      regionCondition: {
        regionId: undefined,
      },
      regionProvince: {
        regionId: undefined,
      },
      regionCity: {
        regionId: undefined,
      },
      regionDistrict: {
        regionId: undefined,
      },
      checkStatueSel: [
        {
          key: "4",
          value: "所有",
        },
        {
          key: "1",
          value: "待审核",
        },
        {
          key: "2",
          value: "审核通过",
        },
        {
          key: "3",
          value: "审核不通过",
        },
      ],
      onlineStatusSel: [
        {
          key: "2",
          value: "门店上架状态",
        },
        {
          key: "0",
          value: "下架",
        },
        {
          key: "1",
          value: "上架",
        },
      ],

      auditModel: {
        failedExaminedReason: undefined,
        ShopId: undefined,
        checkStatus: undefined,
      },

      auditModelInit: {
        failedExaminedReason: undefined,
        ShopId: undefined,
        checkStatus: undefined,
      },
      formModel: {},
      shop: {
        simpleName: "", // 简单名称
        fullName: "", // 店全称
        type: 1, // 门店类型
        status: 0, //门店经营状态
        online: 0, //门店上下架
        provinceId: "", // 省
        cityId: "", // 市
        districtId: "", // 区
        address: "", // 详细地址
        longitude: 0.0, // 经度
        latitude: 0.0, // 维度
        contact: "", // 联系人
        telephone: "", // 电话
        mobile: "", // 手机
        ownerName: "", // 门店老板姓名
        ownerPhone: "", // 门店老板电话
        headImg: [], //门头图片
        frontImg: [], //正面照
        shopImgs: [], //门店照
        shopProofImgs: [], //资质证明照
        businessLienseImgs: [],
        managementLicenseImgs: [],
        tagNames: [],
        brandNames: [],
      },
      shopConfig: {
        startWorkTime: "", // 营业开始时间
        endWorkTime: "", // 营业结束时间
        onlineTime: "", // 正式上线时间
        leaseFreePeriod: 0, //  租赁期
        leaseStartDate: "", // 租赁开始日期
        leaseEndDate: "", // 租赁结束日期
        isLoungeRoom: 0, //是否有休息室
        isRestRoom: 0, //卫生间
        isFreeWifi: 0, //免费wifi
        isPostLift: 0, //柱式举升机
        carFaultDiagnosticTool: 0, //汽车故障诊断仪
      },
      shopBankCard: {
        type: 0, //默认个人账户
        openingBank: "",
        bankId: 0,
        openingLicence: "",
        openingUserName: "",
        bankCardNo: "",
      },
      //标签+专修品牌
      tags: [],
      brands: [],
      selectBrands: [],
      tagFormTitle: undefined,
      tagFormVisible: false,
      tagFormModel: {
        tagName: undefined,
      },
      brandCondition: {
        shopId: undefined,
      },
      brandFormTitle: undefined,
      brandFormVisible: false,
      brandFormModel: {
        brandSuffix: [],
      },
      //标签+专修品牌
      //照片变量定义
      imagehost: "https://m.aerp.com.cn/",
      fileList: [],
      fileList1: [],
      fileList2: [],
      fileList3: [],
      fileList4: [],
      fileList5: [],
      fileList6: [],
      uploadFileModel: {
        name: undefined,
        url: undefined,
        isDeleted: undefined,
      },
      dialogImageUrl: "",
      dialogVisible: false,
      deleteCondition: {
        imgId: undefined,
        shopId: undefined,
      },
      uploadFileModelInit: {
        filename: undefined,
        fileurl: undefined,
        isDeleted: undefined,
      },

      shopType: [
        {
          value: 1,
          name: "合作店",
        },
        {
          value: 2,
          name: "自营店",
        },
      ],

      //暂停营业时间
      statusFormTitle: undefined,
      statusFormVisible: false,
      statusFormModel: {
        stopTime: [],
      },
      modifySuspendTimeCondition: {
        times: [],
        shopId: undefined,
      },
      showTime: false,
      //暂停营业时间
      sendpickerOptions: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        },
        shortcuts: [
          {
            text: "最近一周",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit("pick", [start, end]);
            },
          },
          {
            text: "最近一个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit("pick", [start, end]);
            },
          },
          {
            text: "最近三个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit("pick", [start, end]);
            },
          },
        ],
      },

      bankList: [],
      rules: {
        simpleName: [
          { required: true, message: "请输入门店简称", trigger: "blur" },
        ],
        fullName: [
          { required: true, message: "请输入门店全称", trigger: "blur" },
        ],
        shopCompanyName: [
          { required: true, message: "请输入公司名称", trigger: "blur" },
        ],
        provinceId: [
          { required: true, message: "请选择省份", trigger: "blur" },
        ],
        cityId: [{ required: true, message: "请选择城市", trigger: "blur" }],
        districtId: [
          { required: true, message: "请选择区域", trigger: "blur" },
        ],
        address: [
          { required: true, message: "请输入街道地址", trigger: "blur" },
        ],
        shopaddress: [
          { required: true, message: "请输入经营地址", trigger: "blur" },
        ],
        longitude: [
          {
            validator: longreg,
            required: true,
            message: "请输入经度",
            trigger: "blur",
          },
        ],
        latitude: [
          {
            validator: latreg,
            required: true,
            message: "请输入纬度",
            trigger: "blur",
          },
        ],
        contact: [
          { required: true, message: "请输入门店联系人", trigger: "blur" },
        ],
        mobile: [
          { required: true, message: "请输入门店固定电话", trigger: "blur" },
        ],
        head: [
          { required: true, message: "请输入运营负责人", trigger: "blur" },
        ],
        headPhone: [
          { required: true, message: "请输入运营负责人手机", trigger: "blur" },
        ],
        ownerName: [
          { required: true, message: "请输入门店老板", trigger: "blur" },
        ],
        ownerPhone: [
          { required: true, message: "请输入老板手机", trigger: "blur" },
        ],
      },

      accountType1: "",
      accountType2: "none",
    };
  },

  created() {
    //页面初始化函数
    this.fetchData();
  },
  watch: {},
  computed: {
    ...mapGetters(["userInfo"]),
  },

  methods: {
    fetchData() {
      this.resetForm();
      this.getShopById();
      this.getProvince();
      this.getVehicleBrandList();
      this.getShopServiceBrands(); //已选择的品牌
    },

    //获取省
    getProvince() {
      this.regionProvince.regionId = 0;
      shopManageSvc
        .getRegionChinaListByRegionId(this.regionProvince)
        .then(
          (res) => {
            this.provinceSel = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    getCity() {
      if (
        this.condition.provinceId != "" &&
        this.condition.provinceId != undefined
      ) {
        this.regionCondition.regionId = this.condition.provinceId;
        shopManageSvc
          .getRegionChinaListByRegionId(this.regionCondition)
          .then(
            (res) => {
              this.citySel = res.data;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      } else {
        this.condition.cityId = undefined;
        this.condition.districtId = undefined;
        this.citySel = [];
        this.districtSel = [];
      }
    },

    getDistrict() {
      if (this.condition.cityId != "" && this.condition.cityId != undefined) {
        this.regionCondition.regionId = this.condition.cityId;
        shopManageSvc
          .getRegionChinaListByRegionId(this.regionCondition)
          .then(
            (res) => {
              this.districtSel = res.data;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      } else {
        this.condition.districtId = undefined;
        this.districtSel = [];
      }
    },
    //获取市
    getCityListByRegionId(provinceId) {
      console.log("provinceId :" + provinceId);
      if (this.shop.provinceId != "" && this.shop.provinceId != undefined) {
        this.regionCity.regionId = provinceId;
        this.cityList = [];
        shopManageSvc
          .getRegionChinaListByRegionId(this.regionCity)
          .then(
            (res) => {
              this.citySel = res.data;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      } else {
        this.shop.cityId = undefined;
        this.shop.districtId = undefined;
        this.citySel = [];
        this.districtSel = [];
      }
    },
    //获取区
    getDistrictListByRegionId(cityId) {
      if (this.shop.cityId != "" && this.shop.cityId != undefined) {
        this.regionDistrict.regionId = cityId;
        shopManageSvc
          .getRegionChinaListByRegionId(this.regionDistrict)
          .then(
            (res) => {
              this.districtSel = res.data;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      } else {
        this.shop.districtId = undefined;
        this.districtSel = [];
      }
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
      (this.formModel = {}),
        (this.shop = {}),
        (this.shopConfig = {}),
        (this.shopBankCard = {}),
        (this.tags = []),
        (this.brands = []),
        (this.selectBrands = []),
        (this.fileList = []),
        (this.fileList1 = []),
        (this.fileList2 = []),
        (this.fileList3 = []),
        (this.fileList4 = []),
        (this.fileList5 = []),
        (this.fileList6 = []);
    },

    //获取门店信息
    getShopById() {
      console.log("userInfo:" + JSON.stringify(this.userInfo));
      console.log("ShopId:" + this.userInfo.organizationId);
      shopManageSvc
        .getShopById({ ShopId: this.userInfo.organizationId })
        .then(
          (res) => {
            this.shop = res.data.shop;
            console.log("门店信息: " + JSON.stringify(this.shop));
            this.fileList1 = this.shop.headImg;
            this.fileList2 = this.shop.frontImg;
            this.fileList3 = this.shop.shopImgs;
            this.fileList4 = this.shop.shopProofImgs;
            this.fileList5 = this.shop.businessLienseImgs;
            this.fileList6 = this.shop.managementLicenseImgs;
            this.shopConfig = res.data.shopConfig;

            this.formCheck = true;
            //console.log("门店配置信息: " + JSON.stringify(this.shopConfig));
            if (this.shop.status == 2) {
              this.showTime = true;
            }
            this.shopBankCard = res.data.shopBankCard;
            if (this.shopBankCard.type == 1) {
              this.accountType1 = "none";
              this.accountType2 = "";
            } else {
              this.accountType1 = "";
              this.accountType2 = "none";
            }
            console.log("门店账户信息: " + JSON.stringify(this.shopBankCard));
            this.getBankList();
            this.getProvince();
            this.getCityListByRegionId(this.shop.provinceId);
            this.getDistrictListByRegionId(this.shop.cityId);
            if (res.data.shop.tagNames.length > 0) {
              this.tags = res.data.shop.tagNames;
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    modifySuspendTime() {
      this.statusFormVisible = true;
      this.statusFormTitle = "选择暂停营业时间";
    },
    statuscancel() {
      if (this.shopConfig.suspendStartDateTime != "1900-01-01 00:00:00") {
        this.statusFormVisible = false;
        this.statusFormModel.stopTime = [];
      } else {
        this.$message({
          message: "请选择暂停营业时间!",
          type: "warning",
        });
      }
    },

    dateFormat(time) {
      var date = new Date(time);
      var year = date.getFullYear();
      /* 在日期格式中，月份是从0开始的，因此要加0
       * 使用三元表达式在小于10的前面加0，以达到格式统一  如 09:11:05
       * */
      var month =
        date.getMonth() + 1 < 10
          ? "0" + (date.getMonth() + 1)
          : date.getMonth() + 1;
      var day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
      var hours =
        date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
      var minutes =
        date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
      var seconds =
        date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
      // 拼接
      return (
        year +
        "-" +
        month +
        "-" +
        day +
        " " +
        hours +
        ":" +
        minutes +
        ":" +
        seconds
      );
    },

    statusSave() {
      this.statusFormVisible = false;
      //时间要赋到model中
      if (this.statusFormModel.stopTime.length > 0) {
        this.shopConfig.suspendStartDateTime = this.dateFormat(
          this.statusFormModel.stopTime[0]
        );
        this.shopConfig.suspendEndDateTime = this.dateFormat(
          this.statusFormModel.stopTime[1]
        );

        this.statusFormModel.stopTime = [];
        this.showTime = true;
      }
    },

    mapQuestion() {
      if (this.mapShow == "") {
        this.mapShow = "none";
      } else {
        this.mapShow = "";
      }
    },

    //图片信息
    handlePictureCardPreview(file) {
      this.dialogImageUrl = file.url;
      this.dialogVisible = true;
    },
    handleRemove1(file, fileList) {
      //如果该图片是新追加的 但是还没有保存到数据  移除了也要清掉
      if (file.imgId == undefined) {
        if (this.fileList1.length > 0) {
          for (var i = 0; i < this.fileList1.length; i++) {
            if (this.fileList1[i].name == file.name) {
              this.fileList1.splice(i, 1);
            }
          }
        }
      } else {
        //原来已经存在的
        //debugger;
        this.deleteCondition.imgId = file.imgId;
        this.deleteCondition.shopId = this.shop.id;
        shopManageSvc
          .DeleteShopImg(this.deleteCondition)
          .then(
            (res) => {
              //从数据库中删除,并且从Array中remove
              for (var i = 0; i < this.fileList1.length; i++) {
                if (this.fileList1[i].imgId == this.deleteCondition.imgId) {
                  this.fileList1.splice(i, 1);
                }
              }
              this.deleteCondition.imgId = undefined;
              this.deleteCondition.shopId = undefined;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },
    handleRemove2(file, fileList) {
      //如果该图片是新追加的 但是还没有保存到数据  移除了也要清掉
      if (file.imgId == undefined) {
        if (this.fileList2.length > 0) {
          for (var i = 0; i < this.fileList2.length; i++) {
            if (this.fileList2[i].name == file.name) {
              this.fileList2.splice(i, 1);
            }
          }
        }
      } else {
        //原来已经存在的
        //debugger;
        this.deleteCondition.imgId = file.imgId;
        this.deleteCondition.shopId = this.shop.id;
        shopManageSvc
          .DeleteShopImg(this.deleteCondition)
          .then(
            (res) => {
              //从数据库中删除,并且从Array中remove
              for (var i = 0; i < this.fileList2.length; i++) {
                if (this.fileList2[i].imgId == this.deleteCondition.imgId) {
                  this.fileList2.splice(i, 1);
                }
              }
              this.deleteCondition.imgId = undefined;
              this.deleteCondition.shopId = undefined;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },

    handleRemove3(file, fileList) {
      //如果该图片是新追加的 但是还没有保存到数据  移除了也要清掉
      if (file.imgId == undefined) {
        if (this.fileList3.length > 0) {
          for (var i = 0; i < this.fileList3.length; i++) {
            if (this.fileList3[i].name == file.name) {
              this.fileList3.splice(i, 1);
            }
          }
        }
      } else {
        //原来已经存在的
        //debugger;
        this.deleteCondition.imgId = file.imgId;
        this.deleteCondition.shopId = this.shop.id;
        shopManageSvc
          .DeleteShopImg(this.deleteCondition)
          .then(
            (res) => {
              //从数据库中删除,并且从Array中remove
              for (var i = 0; i < this.fileList3.length; i++) {
                if (this.fileList3[i].imgId == this.deleteCondition.imgId) {
                  this.fileList3.splice(i, 1);
                }
              }
              this.deleteCondition.imgId = undefined;
              this.deleteCondition.shopId = undefined;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },

    handleRemove4(file, fileList) {
      //如果该图片是新追加的 但是还没有保存到数据  移除了也要清掉
      if (file.imgId == undefined) {
        if (this.fileList4.length > 0) {
          for (var i = 0; i < this.fileList4.length; i++) {
            if (this.fileList4[i].name == file.name) {
              this.fileList4.splice(i, 1);
            }
          }
        }
      } else {
        //原来已经存在的
        //debugger;
        this.deleteCondition.imgId = file.imgId;
        this.deleteCondition.shopId = this.shop.id;
        shopManageSvc
          .DeleteShopImg(this.deleteCondition)
          .then(
            (res) => {
              //从数据库中删除,并且从Array中remove
              for (var i = 0; i < this.fileList4.length; i++) {
                if (this.fileList4[i].imgId == this.deleteCondition.imgId) {
                  this.fileList4.splice(i, 1);
                }
              }
              this.deleteCondition.imgId = undefined;
              this.deleteCondition.shopId = undefined;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },

    handleRemove5(file, fileList) {
      //如果该图片是新追加的 但是还没有保存到数据  移除了也要清掉
      if (file.imgId == undefined) {
        if (this.fileList5.length > 0) {
          for (var i = 0; i < this.fileList5.length; i++) {
            if (this.fileList5[i].name == file.name) {
              this.fileList5.splice(i, 1);
            }
          }
        }
      } else {
        //原来已经存在的
        //debugger;
        this.deleteCondition.imgId = file.imgId;
        this.deleteCondition.shopId = this.shop.id;
        shopManageSvc
          .DeleteShopImg(this.deleteCondition)
          .then(
            (res) => {
              //从数据库中删除,并且从Array中remove
              for (var i = 0; i < this.fileList5.length; i++) {
                if (this.fileList5[i].imgId == this.deleteCondition.imgId) {
                  this.fileList5.splice(i, 1);
                }
              }
              this.deleteCondition.imgId = undefined;
              this.deleteCondition.shopId = undefined;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },

    handleRemove6(file, fileList) {
      //如果该图片是新追加的 但是还没有保存到数据  移除了也要清掉
      if (file.imgId == undefined) {
        if (this.fileList6.length > 0) {
          for (var i = 0; i < this.fileList6.length; i++) {
            if (this.fileList6[i].name == file.name) {
              this.fileList6.splice(i, 1);
            }
          }
        }
      } else {
        //原来已经存在的
        //debugger;
        this.deleteCondition.imgId = file.imgId;
        this.deleteCondition.shopId = this.shop.id;
        shopManageSvc
          .DeleteShopImg(this.deleteCondition)
          .then(
            (res) => {
              //从数据库中删除,并且从Array中remove
              for (var i = 0; i < this.fileList6.length; i++) {
                if (this.fileList6[i].imgId == this.deleteCondition.imgId) {
                  this.fileList6.splice(i, 1);
                }
              }
              this.deleteCondition.imgId = undefined;
              this.deleteCondition.shopId = undefined;
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },
    handleRemove7(file, fileList) {
      this.shopBankCard.openingLicence = "";
    },
    beforeAvatarUpload(file) {
      const isLt2M = file.size / 1024 / 1024 < 5;
      if (!isLt2M) {
        this.$message.error("上传图片大小不能超过 5MB!");
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

    uploadRequest1: function (request) {
      const loading = this.$loading({
        lock: true,
        text: "拼命上传中......",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });

      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        shopManageSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        shopManageSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

      var directoryName = "Shops/Images";
      var key = directoryName + "/" + newFileName;
      this.getToken(directoryName, fileName)
        .then((res) => {
          debugger;
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
              //model重新清空
              this.uploadFileModel = JSON.parse(
                JSON.stringify(this.uploadFileModelInit)
              );
              debugger;
              this.uploadFileModel.name = fileName;
              this.uploadFileModel.url = url;
              this.uploadFileModel.isDeleted = 0;

              this.fileList1.push(this.uploadFileModel);
              console.log(
                "uploadFileModel" + JSON.stringify(this.uploadFileModel)
              );
              console.log("fileList1" + JSON.stringify(this.fileList1));
              // loading.close();
            }
          );
        })
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    },

    uploadRequest2: function (request) {
      const loading = this.$loading({
        lock: true,
        text: "拼命上传中......",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        shopManageSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        shopManageSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

      var directoryName = "Shops/Images";
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
              //model重新清空

              this.uploadFileModel = JSON.parse(
                JSON.stringify(this.uploadFileModelInit)
              );
              this.uploadFileModel.name = fileName;
              this.uploadFileModel.url = url;
              this.uploadFileModel.isDeleted = 0;

              this.fileList2.push(this.uploadFileModel);
              // loading.close();
            }
          );
        })
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    },

    uploadRequest3: function (request) {
      const loading = this.$loading({
        lock: true,
        text: "拼命上传中......",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        shopManageSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        shopManageSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

      var directoryName = "Shops/Images";
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
              //model重新清空

              this.uploadFileModel = JSON.parse(
                JSON.stringify(this.uploadFileModelInit)
              );
              this.uploadFileModel.name = fileName;
              this.uploadFileModel.url = url;
              this.uploadFileModel.isDeleted = 0;

              this.fileList3.push(this.uploadFileModel);
              //  loading.close();
            }
          );
        })
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    },

    uploadRequest4: function (request) {
      const loading = this.$loading({
        lock: true,
        text: "拼命上传中......",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        shopManageSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        shopManageSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

      var directoryName = "Shops/Images";
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
              //model重新清空

              this.uploadFileModel = JSON.parse(
                JSON.stringify(this.uploadFileModelInit)
              );
              this.uploadFileModel.name = fileName;
              this.uploadFileModel.url = url;
              this.uploadFileModel.isDeleted = 0;

              this.fileList4.push(this.uploadFileModel);
              //loading.close();
            }
          );
        })
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    },

    uploadRequest5: function (request) {
      const loading = this.$loading({
        lock: true,
        text: "拼命上传中......",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        shopManageSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        shopManageSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

      var directoryName = "Shops/Images";
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
              //model重新清空

              this.uploadFileModel = JSON.parse(
                JSON.stringify(this.uploadFileModelInit)
              );
              this.uploadFileModel.name = fileName;
              this.uploadFileModel.url = url;
              this.uploadFileModel.isDeleted = 0;

              this.fileList5.push(this.uploadFileModel);
              //loading.close();
            }
          );
        })
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    },

    uploadRequest6: function (request) {
      const loading = this.$loading({
        lock: true,
        text: "拼命上传中......",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        shopManageSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        shopManageSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

      var directoryName = "Shops/Images";
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
              //model重新清空

              this.uploadFileModel = JSON.parse(
                JSON.stringify(this.uploadFileModelInit)
              );
              this.uploadFileModel.name = fileName;
              this.uploadFileModel.url = url;
              this.uploadFileModel.isDeleted = 0;

              this.fileList6.push(this.uploadFileModel);
              loading.close();
            }
          );
        })
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    },

    uploadRequest7: function (request) {
      const loading = this.$loading({
        lock: true,
        text: "拼命上传中......",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        shopManageSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        shopManageSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

      var directoryName = "Shops/Images";
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
              this.shopBankCard.openingLicence = url;
            }
          );
        })
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    },
    // 图片上传--end ----

    querySearch(queryString, cb) {
      var restaurants = this.bankList;
      var results = queryString
        ? restaurants.filter(this.createFilter(queryString))
        : restaurants;
      // 调用 callback 返回建议列表的数据
      cb(results);
    },
    createFilter(queryString) {
      return (restaurant) => {
        return (
          restaurant.bankName
            .toLowerCase()
            .indexOf(queryString.toLowerCase()) === 0
        );
      };
    },
    //获取银行列表
    getBankList() {
      shopManageSvc
        .getBankListAsync()
        .then(
          (res) => {
            if (res.code == 10000) {
              this.bankList = res.data;
              this.bankList.forEach((item) => {
                item.value = item.bankName;
              });
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    handleSelect(item) {
      this.shopBankCard.bankId = item.id;
    },

    changeAccountType() {
      if (this.shopBankCard.type == 0) {
        (this.accountType1 = ""), (this.accountType2 = "none");
      } else {
        (this.accountType1 = "none"), (this.accountType2 = "");
      }
    },
    changeStatus() {
      //debugger;
      if (this.shop.status == 2) {
        //显示时间范围选择
        this.statusFormVisible = true;
        this.statusFormTitle = "选择暂停营业时间";
      } else {
        this.shopConfig.suspendStartDateTime = undefined;
        this.shopConfig.suspendEndDateTime = undefined;
        this.showTime = false;
      }
    },

    //添加门店数据
    save(shop) {
      var vm = this;
      //校验
      if (this.shopConfig.startWorkTime == "") {
        this.$message({
          message: "请填写开始营业时间",
          type: "error",
          offset: "80",
        });
        return false;
      }

      if (this.shopConfig.endWorkTime == "") {
        this.$message({
          message: "请填写结束营业时间",
          type: "error",
          offset: "80",
        });
        return false;
      }

      if (this.shopConfig.onlineTime == "") {
        this.$message({
          message: "请填写正式上线时间",
          type: "error",
          offset: "80",
        });
        return false;
      }

      if (this.shopConfig.leaseStartDate == "") {
        this.$message({
          message: "请填写租赁开始时间",
          type: "error",
          offset: "80",
        });
        return false;
      }

      if (this.shopConfig.leaseEndDate == "") {
        this.$message({
          message: "请填写租赁结束时间",
          type: "error",
          offset: "80",
        });
        return false;
      }

      if (this.shop.status == 2) {
        if (
          this.shopConfig.suspendStartDateTime == "" ||
          this.shopConfig.suspendEndDateTime == ""
        ) {
          this.$message({
            message: "请填写暂停营业时间",
            type: "error",
            offset: "80",
          });
          return false;
        }
      }

      if (this.shop.simpleName == "") {
        this.$message({
          message: "请填写门店简称",
          type: "error",
          offset: "80",
        });
        return false;
      }

      if (this.shop.fullName == "") {
        this.$message({
          message: "请填写门店全称",
          type: "error",
          offset: "80",
        });
        return false;
      }
      if (this.shop.type == "") {
        this.$message({
          message: "请选择门店类型",
          type: "error",
          offset: "80",
        });
        return false;
      }
      if (
        this.shop.provinceId == "" ||
        this.shop.cityId == "" ||
        this.shop.districtId == ""
      ) {
        this.$message({
          message: "请填写省市区地址",
          type: "error",
          offset: "80",
        });
        return false;
      }
      console.log("1");
      console.log("shop:" + JSON.stringify(this.shop));
      if (
        this.shop.provinceId == "请选择" ||
        this.shop.cityId == "请选择" ||
        this.shop.districtId == "请选择"
      ) {
        this.$message({
          message: "请填写省市区地址",
          type: "error",
          offset: "80",
        });
        return false;
      }
      console.log("1");
      if (this.shop.address == "") {
        this.$message({
          message: "请填写详细地址",
          type: "error",
          offset: "80",
        });
        return false;
      }

      let objProvince = {};
      objProvince = this.provinceSel.find((item) => {
        //model就是上面的数据源
        return item.regionId === this.shop.provinceId; //筛选出匹配数据
      });
      //debugger;
      this.shop.province = objProvince.name;
      let objCity = {};
      objCity = this.citySel.find((item) => {
        //model就是上面的数据源
        return item.regionId === this.shop.cityId; //筛选出匹配数据
      });
      this.shop.city = objCity.name;
      let objDistrict = {};
      objDistrict = this.districtSel.find((item) => {
        //model就是上面的数据源
        return item.regionId === this.shop.districtId; //筛选出匹配数据
      });
      this.shop.district = objDistrict.name;

      if (this.tags.length > 0) {
        this.shop.tagNames = this.tags;
      }

      if (this.selectBrands.length > 0) {
        this.shop.brandNames = this.selectBrands;
      }

      //保存图片
      if (this.fileList1.length > 0) {
        this.shop.headImg = this.fileList1;
      }

      if (this.fileList2.length > 0) {
        this.shop.frontImg = this.fileList2;
      }

      if (this.fileList3.length > 0) {
        this.shop.shopImgs = this.fileList3;
      }

      if (this.fileList4.length > 0) {
        this.shop.shopProofImgs = this.fileList4;
      }

      if (this.fileList5.length > 0) {
        this.shop.businessLienseImgs = this.fileList5;
      }

      if (this.fileList6.length > 0) {
        this.shop.managementLicenseImgs = this.fileList6;
      }
      //this.shop.id = this.userInfo.organizationId;
      var shopData = {
        Shop: this.shop,
        ShopConfig: this.shopConfig,
        ShopBankCard: this.shopBankCard,
      };
      console.log("shopData" + JSON.stringify(shopData));
      shopManageSvc
        .addOrModifyShopInfoForShop(shopData)
        .then(
          (res) => {
            var data = res.code;
            if (data == "10000") {
              this.$message({
                message: "保存成功",
                type: "success",
              });
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch((err) => {
          console.error(err);
        });
    },

    handleClose(tag) {
      this.tags.splice(this.tags.indexOf(tag), 1);
    },

    handleClose2(tag) {
      this.selectBrands.splice(this.selectBrands.indexOf(tag), 1);
    },
    addBrand() {
      this.brandFormVisible = true;
      this.brandFormTitle = "添加专修品牌";
    },
    addTag() {
      this.tagFormTitle = "添加标签";
      this.tagFormVisible = true;
    },
    tagSave() {
      if (
        this.tagFormModel.tagName != "" &&
        this.tagFormModel.tagName != undefined
      ) {
        if (this.tags.length >= 3) {
          this.$message({
            message: "最多只能添加3个标签!",
            type: "warning",
          });
          return;
        }
        if (this.tagFormModel.tagName.length > 6) {
          this.$message({
            message: "标签长度最多为6个字符!",
            type: "warning",
          });
        } else {
          if (this.tags.length > 0) {
            var isRepeat = 0;
            //判断是否重复
            for (var i = 0; i < this.tags.length; i++) {
              if (this.tags[i] == this.tagFormModel.tagName) {
                //重复添加
                this.$message({
                  message: "标签已添加，请核对!",
                  type: "warning",
                });
                isRepeat = 1;
                return;
              }
            }
            //未重复添加后
            if (isRepeat == 0) {
              this.tags.push(this.tagFormModel.tagName);

              this.tagFormModel.tagName = undefined;
              this.tagFormVisible = false;
            }
          } else {
            this.tags.push(this.tagFormModel.tagName);

            this.tagFormModel.tagName = undefined;
            this.tagFormVisible = false;
          }
        }
      } else {
        this.$message({
          message: "标签不能为空!",
          type: "warning",
        });
      }
    },

    tagCancel() {
      this.tagFormVisible = false;
      this.tagFormModel.tagName = undefined;
    },

    getVehicleBrandList() {
      shopManageSvc
        .getVehicleBrandList()
        .then(
          (res) => {
            this.brands = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    brandCancel() {
      this.brandFormVisible = false;
      this.brandFormModel.brandSuffix = [];
    },

    brandSave() {
      debugger;
      if (this.brandFormModel.brandSuffix.length > 0) {
        if (this.selectBrands.length >= 5) {
          this.$message({
            message: "最多添加5个专修品牌!",
            type: "warning",
          });
          return;
        }
        if (
          this.selectBrands.length + this.brandFormModel.brandSuffix.length >
          5
        ) {
          this.$message({
            message: "最多添加5个专修品牌!",
            type: "warning",
          });
          return;
        }

        var repeatArr = [];
        var unRepeatArr = [];

        if (this.selectBrands.length > 0) {
          for (var j = 0; j < this.brandFormModel.brandSuffix.length; j++) {
            var isContain = 0;
            for (var i = 0; i < this.selectBrands.length; i++) {
              if (this.brandFormModel.brandSuffix[j] == this.selectBrands[i]) {
                isContain = 1;
                break;
              } else {
                continue;
              }
            }
            if (isContain == 0) {
              unRepeatArr.push(this.brandFormModel.brandSuffix[j]);
            } else {
              repeatArr.push(this.brandFormModel.brandSuffix[j]);
            }
          }
          if (unRepeatArr.length > 0) {
            for (var i = 0; i < unRepeatArr.length; i++) {
              this.selectBrands.push(unRepeatArr[i]);
            }

            this.brandFormVisible = false;
            this.brandFormModel.brandSuffix = [];
          } else {
            var repeatStr = "";
            for (var i = 0; i < repeatArr.length; i++) {
              repeatStr += repeatArr[i] + ",";
            }
            this.$message({
              message: repeatStr.trimRight(",") + "已选择！",
              type: "warning",
            });
            return;
          }
        } else {
          this.selectBrands = this.brandFormModel.brandSuffix;
          this.brandFormVisible = false;
          this.brandFormModel.brandSuffix = [];
        }
      } else {
        this.$message({
          message: "专修品牌不能为空!",
          type: "warning",
        });
      }
    },
    getShopServiceBrands() {
      this.brandCondition.shopId = this.userInfo.organizationId;
      shopManageSvc
        .getShopServiceBrands(this.brandCondition)
        .then(
          (res) => {
            if (res.data != null && res.data.length > 0) {
              for (var i = 0; i < res.data.length; i++) {
                this.selectBrands.push(res.data[i].brand);
              }
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
  },

  mounted() {
    console.log("mounted:");
    this.bankList = this.getBankList();
  },
};
</script>

<style lang="scss" scoped>
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
</style>
