<template>
  <main class="container">
    <!-- 详情页面 -->
    <section id="indexPage">
      <el-form :model="formModel" :inline="true" ref="formModel">
        <!-- 基本信息开始 -->
        <el-container>
          <el-header>基本信息</el-header>
          <el-main>
            <el-form
              :inline="true"
              :model="shop"
              class="demo-form-inline"
              ref="shop"
              :rules="rules"
            >
              <el-row :gutter="10">
                <el-form-item label="门店简称:" prop="simpleName">
                  <el-input
                    v-model="shop.simpleName"
                    placeholder="请输入门店简称"
                  />
                </el-form-item>

                <el-form-item label="门店全称:" prop="fullName">
                  <el-input
                    v-model="shop.fullName"                  
                    placeholder="请输入门店全称"
                  />
                </el-form-item>
              </el-row>
              <el-row :gutter="10">
                <!-- <el-form-item label="店公司名称:" prop="shopCompanyName">
                  <el-input
                    v-model="shop.shopCompanyName"
                    placeholder="请输入公司名称"
                  />
                </el-form-item> -->
                <el-form-item label="所属公司:" prop="companyName">
                  <!-- <el-input
                    v-model="shop.companyName"
                    placeholder="请输入公司名称"
                  /> -->
                  <el-select
                    v-model="shop.companyId"
                    filterable
                    remote
                    clearable
                    reserve-keyword
                    placeholder="请输入公司名称"
                    :remote-method="getCompanyinfo"
                    :loading="loading"
                    style="width: 300px"
                  >
                    <el-option
                      v-for="item in companySel"
                      :key="item.key"
                      :label="item.value"
                      :value="item.key"
                    ></el-option>
                  </el-select>
                </el-form-item>

              </el-row>
              <el-row :gutter="20">
                <el-form-item label="门店类型:" prop="type">
                  <el-radio-group v-model="shop.type">
                    <!-- <el-radio :label="1">合作店</el-radio> -->
                    <el-radio :label="2">自营店</el-radio>
                    <el-radio :label="4">上门养护</el-radio>
                    <!-- <el-radio :label="8">认证店</el-radio>
                    <el-radio :label="16">技师</el-radio>
                    <el-radio :label="32">前置仓</el-radio> -->
                  </el-radio-group>
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="营业状态:" prop="status">
                  <el-radio-group
                    v-model="shop.status"
                    @change="changeStatus()"
                  >
                    <el-radio :label="0">正常营业</el-radio>
                    <el-radio :label="1">终止合作</el-radio>
                    <el-radio :label="2">暂停营业</el-radio>
                  </el-radio-group>
                </el-form-item>
                <el-form-item>
                   <div v-show="showTime">
                    <label>开始时间:{{ shopConfig.suspendStartDateTime }}</label
                    >&nbsp;&nbsp; &nbsp;<label
                      >结束时间:{{ shopConfig.suspendEndDateTime }}</label
                    >
                    &nbsp;&nbsp;
                    <el-button
                      type="text"
                      size="small"
                      @click="modifySuspendTime()"
                      >修改暂停营业时间</el-button
                    >
                  </div>
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="门店联系人:" prop="contact">
                  <el-input
                    v-model="shop.contact"
                    placeholder="请输入门店联系人"
                  />
                </el-form-item>
                <el-form-item label="座机:" prop="telephone">
                  <el-input v-model="shop.telephone" placeholder="请输入座机" />
                </el-form-item>
                <el-form-item label="手机:" prop="mobile">
                  <el-input v-model="shop.mobile" placeholder="请输入手机" />
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="运营负责人:" prop="head">
                  <el-input
                    v-model="shop.head"
                    placeholder="请输入运营负责人"
                  />
                </el-form-item>
                <el-form-item label="手机:" prop="headPhone">
                  <el-input
                    v-model="shop.headPhone"
                    placeholder="请输入运营负责人手机"
                  />
                </el-form-item>
                <el-form-item label="邮箱:" prop="headEmail">
                  <el-input
                    v-model="shop.headEmail"
                    placeholder="请输入运营负责人邮箱"
                  />
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="门店老板:" prop="ownerName">
                  <el-input
                    v-model="shop.ownerName"
                    placeholder="请输入门店老板"
                  />
                </el-form-item>
                <el-form-item label="老板手机:" prop="ownerPhone">
                  <el-input
                    v-model="shop.ownerPhone"
                    placeholder="请输入老板手机"
                  />
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item
                  label="是否同步为老板创建账号:"
                  prop="isCreateAccount"
                >
                  <el-radio-group v-model="shopConfig.isCreateAccount">
                    <el-radio :label="1">是</el-radio>
                    <el-radio :label="0">否</el-radio>
                  </el-radio-group>
                </el-form-item>
                <el-form-item
                  label="是否短信告知老板账号信息:"
                  prop="isSendMessage"
                >
                  <el-radio-group v-model="shopConfig.isSendMessage">
                    <el-radio :label="1">是，保存自动发送短信</el-radio>
                    <el-radio :label="0">否，个人另行通知</el-radio>
                  </el-radio-group>
                </el-form-item>
              </el-row>
               <!-- <el-row :gutter="20">
                <el-form-item
                  label="系统版本:"
                  prop="systemType"
                >
                  <el-radio-group v-model="shop.systemType">
                   <el-radio :label="0">普通</el-radio>
                    <el-radio :label="1">高级 </el-radio>
                    <el-radio :label="2">个人 </el-radio>
                  </el-radio-group>
                </el-form-item>
              </el-row> -->
              <el-row :gutter="20">
                <el-form-item label="门店标签:">
                  <!-- <el-checkbox-group v-model="tagGroup">
                    <el-checkbox-button
                      v-for="tag in tags"
                      @change="changeTag(this)"
                      v-model="selectTagValue"
                      :label="tag"
                      :key="tag"
                    >{{tag}}</el-checkbox-button>
                  </el-checkbox-group>-->
                  <el-tag
                    :key="tag"
                    v-for="tag in tags"
                    closable
                    effect="dark"
                    :disable-transitions="false"
                    @close="handleClose(tag)"
                    >{{ tag }}</el-tag
                  >
                  <el-button type="text" size="small" @click="addTag()"
                    >添加标签</el-button
                  >
                </el-form-item>
                <el-form-item label="专修品牌:">
                  <!-- <el-checkbox-group v-model="brandGroup">
                    <el-checkbox-button
                      v-for="brand in selectBrands"
                      :label="brand"
                      :key="brand"
                    >{{brand}}</el-checkbox-button>
                  </el-checkbox-group>-->
                  <el-tag
                    :key="tag"
                    v-for="tag in selectBrands"
                    closable
                    effect="dark"
                    :disable-transitions="false"
                    @close="handleClose2(tag)"
                    >{{ tag }}</el-tag
                  >
                  <el-button type="text" size="small" @click="addBrand()"
                    >添加专修品牌</el-button
                  >
                </el-form-item>
              </el-row>
            </el-form>
          </el-main>
        </el-container>
        <!-- 基本信息结束 -->
        <!-- 联系地址开始 -->
        <el-container>
          <el-header>联系地址</el-header>
          <el-main>
            <el-form
              :inline="true"
              :model="shop"
              class="demo-form-inline"
              ref="shop"
              :rules="rules"
            >
              <el-row :gutter="20">
                <el-form-item label="地址:" required>
                  <el-select
                    v-model="shop.provinceId"
                    @change="getCityByRegionId"
                    placeholder="省"
                    prop="provinceId "
                  >
                    <el-option
                      v-for="item in provinceList"
                      :key="item.regionId"
                      :label="item.name"
                      :value="item.regionId"
                    ></el-option>
                  </el-select>
                  <el-select
                    v-model="shop.cityId"
                    @change="getDistrictByRegionId"
                    placeholder="市"
                    prop="cityId"
                  >
                    <el-option
                      v-for="item in cityList"
                      :key="item.regionId"
                      :label="item.name"
                      :value="item.regionId"
                    ></el-option>
                  </el-select>
                  <el-select
                    v-model="shop.districtId"
                    placeholder="区"
                    prop="districtId"
                  >
                    <el-option
                      v-for="item in districtList"
                      :key="item.regionId"
                      :label="item.name"
                      :value="item.regionId"
                    ></el-option>
                  </el-select>
                </el-form-item>
                <el-form-item label="详细地址:" prop="address">
                  <el-input
                    v-model="shop.address"
                    style="width: 400px"
                    placeholder="请输入详细地址"
                  />
                </el-form-item>
                <el-form-item label="邮编:" prop="post">
                  <el-input v-model="shop.post" placeholder="请输入邮编" />
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="纬度:" prop="latitude">
                  <el-input v-model="shop.latitude" placeholder="请输入纬度" />
                </el-form-item>
                <el-form-item label="经度:" prop="longitude">
                  <el-input v-model="shop.longitude" placeholder="请输入经度" />
                </el-form-item>

                <el-link
                  href="https://lbs.qq.com/tool/getpoint/index.html"
                  target="_blank"
                  ><label style="color: red; font-weight: 600; font-size: 18px"
                    >地图搜索</label
                  >
                  <i class="el-icon-view el-icon--right"></i>
                </el-link>

                &nbsp;&nbsp;&nbsp;<label>(请选择腾讯高德的坐标！)</label>
              </el-row>
              <el-row>
                <label>
                  <span style="color: red">示例</span>:&nbsp;32.9072622400
                </label>

                <label style="margin-left: 120px">
                  <span style="color: red">示例</span>:&nbsp;101.7773437500
                </label>
              </el-row>
            </el-form>
          </el-main>
        </el-container>
        <!-- 联系地址结束 -->
        <!-- 配置信息开始 -->
        <el-container>
          <el-header>配置信息</el-header>
          <el-main>
            <el-form
              :inline="true"
              :model="shopConfig"
              class="demo-form-inline"
              ref="shopConfig"
              :rules="rules2"
            >
              <el-row :gutter="20">
                <el-form-item label="营业时间:" required>
                  <el-col :span="11">
                    <el-form-item prop="startWorkTime">
                      <el-time-picker
                        v-model="shopConfig.startWorkTime"
                        type="fixed-time"
                        format="HH:mm"
                        value-format="HH:mm"
                        placeholder="开始时间"
                        style="width: 100%"
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="2" class="line">-</el-col>
                  <el-col :span="11">
                    <el-form-item prop="endWorkTime">
                      <el-time-picker
                        v-model="shopConfig.endWorkTime"
                        format="HH:mm"
                        value-format="HH:mm"
                        type="fixed-time"
                        placeholder="结束时间:"
                        style="width: 100%"
                      />
                    </el-form-item>
                  </el-col>
                </el-form-item>
                <el-form-item label="正式上线时间:" required prop="onlineTime">
                  <el-col :span="11">
                    <el-date-picker
                      v-model="shopConfig.onlineTime"
                      type="date"
                      placeholder="开始日期"
                      format="yyyy-MM-dd"
                      value-format="yyyy-MM-dd"
                      clearable
                    />
                  </el-col>
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="租赁日期" required>
                  <el-col :span="11">
                    <el-form-item prop="leaseStartDate">
                      <el-date-picker
                        v-model="shopConfig.leaseStartDate"
                        type="date"
                        placeholder="开始日期"
                        format="yyyy-MM-dd"
                        value-format="yyyy-MM-dd"
                        clearable
                      />
                    </el-form-item>
                  </el-col>
                  <el-col :span="2" class="line">-</el-col>
                  <el-col :span="11">
                    <el-form-item prop="leaseEndDate">
                      <el-date-picker
                        v-model="shopConfig.leaseEndDate"
                        type="date"
                        placeholder="结束日期"
                        format="yyyy-MM-dd"
                        value-format="yyyy-MM-dd"
                        clearable
                      />
                    </el-form-item>
                  </el-col>
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="休息室:">
                  <el-radio-group v-model="shopConfig.isLoungeRoom">
                    <el-radio :label="0">无</el-radio>
                    <el-radio :label="1">有</el-radio>
                  </el-radio-group>
                </el-form-item>
                <el-form-item label="卫生间:">
                  <el-radio-group v-model="shopConfig.isRestRoom">
                    <el-radio :label="0">无</el-radio>
                    <el-radio :label="1">有</el-radio>
                  </el-radio-group>
                </el-form-item>
                <el-form-item label="免费wifi:">
                  <el-radio-group v-model="shopConfig.isFreeWifi">
                    <el-radio :label="0">无</el-radio>
                    <el-radio :label="1">有</el-radio>
                  </el-radio-group>
                </el-form-item>
                <el-form-item label="柱式举升机:">
                  <el-radio-group v-model="shopConfig.isPostLift">
                    <el-radio :label="0">无</el-radio>
                    <el-radio :label="1">有</el-radio>
                  </el-radio-group>
                </el-form-item>
                <el-form-item label="汽车故障诊断仪:">
                  <el-radio-group v-model="shopConfig.carFaultDiagnosticTool">
                    <el-radio :label="0">无</el-radio>
                    <el-radio :label="1">有</el-radio>
                  </el-radio-group>
                </el-form-item>
              </el-row>
            </el-form>
          </el-main>
        </el-container>
        <!-- 配置信息结束 -->
        <!-- 服务信息开始 -->
        <!-- <el-container>
          <el-header>服务信息</el-header>
          <el-main>
            <el-form
              :inline="true"
              :model="shopConfig"
              class="demo-form-inline"
              ref="shopConfig"
            >
              <el-row :gutter="40">
                <el-form-item label="前刹车片费用:" prop="feePerFrontBrake">
                  <el-input v-model.number="shopConfig.feePerFrontBrake" />
                </el-form-item>
                <el-form-item label="后刹车片费用:" prop="feePerRearBrake">
                  <el-input v-model.number="shopConfig.feePerRearBrake" />
                </el-form-item>
                <el-form-item label="前刹车盘费用:" prop="feePerFrontDisc">
                  <el-input v-model.number="shopConfig.feePerFrontDisc" />
                </el-form-item>
                <el-form-item label="后刹车盘费用:" prop="feePerRearDisc">
                  <el-input v-model.number="shopConfig.feePerRearDisc" />
                </el-form-item>
              </el-row>
              <el-row :gutter="60">
                <el-form-item label="换轮胎费用:" prop="feePerTire">
                  <el-input v-model.number="shopConfig.feePerTire" />
                </el-form-item>
                <el-form-item label="保养服务费用:" prop="feePerMaintain">
                  <el-input v-model.number="shopConfig.feePerMaintain" />
                </el-form-item>
                <el-form-item label="四轮定位费用:" prop="feePer4Wheel">
                  <el-input v-model.number="shopConfig.feePer4Wheel" />
                </el-form-item>
                <el-form-item
                  label="PM2.5滤芯安装费用:"
                  prop="feePmInstallation"
                >
                  <el-input v-model.number="shopConfig.feePmInstallation" />
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="每日保养约单量:" prop="dailyOrderQuantity">
                  <el-input v-model.number="shopConfig.dailyOrderQuantity" />
                </el-form-item>
                <el-form-item
                  label="保养爆单量/保养约单量:"
                  prop="optionOrderCount"
                >
                  <el-input v-model.number="shopConfig.optionOrderCount" />
                </el-form-item>
                <el-form-item
                  label="每日保养爆单量:"
                  prop="dailyOrderUpperLimit"
                >
                  <el-input v-model.number="shopConfig.dailyOrderUpperLimit" />
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item
                  label="每日轮胎约单量:"
                  prop="dailyTireOrderQuantity"
                >
                  <el-input
                    v-model.number="shopConfig.dailyTireOrderQuantity"
                  />
                </el-form-item>
                <el-form-item
                  label="轮胎爆单量/轮胎约单量:"
                  prop="optionTireOrderCount"
                >
                  <el-input v-model.number="shopConfig.optionTireOrderCount" />
                </el-form-item>
                <el-form-item
                  label="每日轮胎爆单量:"
                  prop="dailyTireOrderUpperLimit"
                >
                  <el-input
                    v-model.number="shopConfig.dailyTireOrderUpperLimit"
                  />
                </el-form-item>
              </el-row>
            </el-form>
          </el-main>
        </el-container> -->
        <!-- 服务信息结束 -->
        <!-- 图片信息开始 -->
        <el-container>
          <el-header
            >图片信息<label style="color: red; font-weight: 600"
              >*上传图片大小不能超过5MB</label
            ></el-header
          >
          <el-main>
            <el-row :gutter="20">
              <el-form-item label="门头照片:">
                <el-upload
                  class="upload-demo"
                  action="http://upload.qiniup.com/"
                  :on-preview="handlePictureCardPreview"
                  :on-remove="handleRemove1"
                  :before-remove="beforeRemove"
                  multiple
                  :limit="1"
                  :before-upload="beforeAvatarUpload"
                  :on-exceed="handleExceed"
                  :file-list="fileList1"
                  list-type="picture-card"
                  :http-request="uploadRequest1"
                  accept="image/jpeg, image/png, image/jpg"
                >
                  <i class="el-icon-plus"></i>
                </el-upload>
              </el-form-item>
            </el-row>
            <el-row :gutter="20">
              <el-form-item label="正面照片:">
                <el-upload
                  class="upload-demo"
                  action="http://upload.qiniup.com/"
                  :on-preview="handlePictureCardPreview"
                  :on-remove="handleRemove2"
                  :before-remove="beforeRemove"
                  multiple
                  :before-upload="beforeAvatarUpload"
                  accept="image/jpeg, image/png, image/jpg"
                  :on-exceed="handleExceed"
                  :file-list="fileList2"
                  list-type="picture-card"
                  :http-request="uploadRequest2"
                >
                  <i class="el-icon-plus"></i>
                </el-upload>
              </el-form-item>
            </el-row>
            <el-row :gutter="20">
              <el-form-item label="门店照片:">
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
                >
                  <i class="el-icon-plus"></i>
                </el-upload>
              </el-form-item>
            </el-row>
            <el-row :gutter="20">
              <el-form-item label="资质证明:">
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
                  accept="image/jpeg, image/png, image/jpg"
                >
                  <i class="el-icon-plus"></i>
                </el-upload>
                <el-dialog :visible.sync="dialogVisible">
                  <img width="100%" :src="dialogImageUrl" alt />
                </el-dialog>
              </el-form-item>
            </el-row>

            <el-row :gutter="20">
              <el-form-item label="营业执照:">
                <el-upload
                  class="upload-demo"
                  action="http://upload.qiniup.com/"
                  :on-preview="handlePictureCardPreview"
                  :on-remove="handleRemove5"
                  :before-remove="beforeRemove"
                  multiple
                  :before-upload="beforeAvatarUpload"
                  accept="image/jpeg, image/png, image/jpg"
                  :on-exceed="handleExceed"
                  :file-list="fileList5"
                  list-type="picture-card"
                  :http-request="uploadRequest5"
                >
                  <i class="el-icon-plus"></i>
                </el-upload>
              </el-form-item>
            </el-row>

            <el-row :gutter="20">
              <el-form-item label="经营许可证:">
                <el-upload
                  class="upload-demo"
                  action="http://upload.qiniup.com/"
                  :on-preview="handlePictureCardPreview"
                  :on-remove="handleRemove6"
                  :before-remove="beforeRemove"
                  multiple
                  :before-upload="beforeAvatarUpload"
                  accept="image/jpeg, image/png, image/jpg"
                  :on-exceed="handleExceed"
                  :file-list="fileList6"
                  list-type="picture-card"
                  :http-request="uploadRequest6"
                >
                  <i class="el-icon-plus"></i>
                </el-upload>
              </el-form-item>
            </el-row>
          </el-main>
        </el-container>
        <!-- 图片信息结束 -->
        <!-- 账户信息开始 -->
        <el-container>
          <el-header>账户信息</el-header>
          <el-main>
            <el-row :gutter="20">
              <el-form-item label="开户银行:">
                <el-input v-model="shopBankCard.openingBank" />
              </el-form-item>
              <el-form-item label="开户支行:">
                <el-input v-model="shopBankCard.openingBranch" />
              </el-form-item>
            </el-row>
            <el-row :gutter="20">
              <el-form-item label="开户银行行号:">
                <el-input v-model="shopBankCard.bankNo" />
              </el-form-item>
              <el-form-item label="开户人姓名:">
                <el-input v-model="shopBankCard.openingUserName" />
              </el-form-item>
              <el-form-item label="银行卡号:">
                <el-input v-model="shopBankCard.bankCardNo" />
              </el-form-item>
            </el-row>
          </el-main>
        </el-container>
        <!-- 账户信息结束 -->
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button icon="el-icon-check" type="primary" @click="save()"
          >保存</el-button
        >
      </div>
    </section>
    <!-- 详情 -->
    <!-- 添加标签 -->
    <section id="posterDetailDialog">
      <el-dialog
        :title="tagFormTitle"
        :close-on-click-modal="false"
        :visible.sync="tagFormVisible"
        :before-close="tagCancel"
        width="500px"
      >
        <el-form :model="tagFormModel" ref="tagFormModel">
          <el-form-item label="标题:" prop="title">
            <el-input
              clearable
              placeholder="输入标签名称"
              v-model="tagFormModel.tagName"
            ></el-input>
          </el-form-item>
        </el-form>

        <div slot="footer" class="dialog-footer">
          <el-button type="primary" @click="tagSave()">保存</el-button>
          <el-button @click="tagCancel()">关闭</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 添加标签 -->

    <!-- 添加专修品牌 -->
    <section id="brandDialog">
      <el-dialog
        :title="brandFormTitle"
        :close-on-click-modal="false"
        :visible.sync="brandFormVisible"
        :before-close="brandCancel"
        width="600px"
      >
        <el-form :model="brandFormModel" ref="brandFormModel">
          <el-form-item label="选择专修品牌:">
            <el-select
              style="width: 400px"
              v-model="brandFormModel.brandSuffix"
              size="small"
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

        <div slot="footer" class="dialog-footer">
          <el-button type="primary" @click="brandSave()">保存</el-button>
          <el-button @click="brandCancel()">关闭</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 添加专修品牌 -->

    <!-- 发货页面 -->
    <section id="status">
      <el-dialog
        :title="statusFormTitle"
        :close-on-click-modal="false"
        :visible.sync="statusFormVisible"
        :before-close="statuscancel"
        width="550px"
      >
        <el-form :model="statusFormModel" ref="statusFormModel">
          <el-form-item label="暂停营业时间:" size>
            <template>
              <div class="block">
                <el-date-picker
                  v-model="statusFormModel.stopTime"
                  type="datetimerange"
                  :picker-options="sendpickerOptions"
                  range-separator="至"
                  start-placeholder="开始日期"
                  end-placeholder="结束日期"
                  align="right"
                ></el-date-picker>
              </div>
            </template>
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="statuscancel('statusFormModel')">取消</el-button>

          <el-button type="primary" @click="statusSave('statusFormModel')"
            >确定</el-button
          >
        </div>
      </el-dialog>
    </section>
  </main>
</template>


<script>
import { Loading } from "element-ui";
import { Message, MessageBox } from "element-ui";
import { shopManageSvc } from "@/api/shopmanage/shopmanage";
import { upload } from "@/utils/uploadfile";
import { isNumber } from "util";
export default {
  data() {
    var longreg = (rule, value, callback) => {
      if (
        !/^[\-\+]?(0(\.\d{1,10})?|([1-9](\d)?)(\.\d{1,10})?|1[0-7]\d{1}(\.\d{1,10})?|180\.0{1,10})$/.test(
          value
        )
      ) {
        return callback(new Error("经度整数部分为0-180,小数部分为0到8位!"));
      }
    };
    var latreg = (rule, value, callback) => {
      if (
        !/^[\-\+]?((0|([1-8]\d?))(\.\d{1,10})?|90(\.0{1,10})?)$/.test(value)
      ) {
        return callback(new Error("纬度整数部分为0-90,小数部分为0到8位!"));
      }
    };
    return {
      formArr: ["shopConfig", "shop", "formModel"],
      resultArr: [],
      formModel: {
        shop: [],
        shopConfig: [],
        shopBankCard: [],
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
      brandFormTitle: undefined,
      brandFormVisible: false,
      brandFormModel: {
        brandSuffix: [],
      },
      //标签+专修品牌

      //暂停营业时间
      statusFormTitle: undefined,
      statusFormVisible: false,
      statusFormModel: {
        stopTime: [],
      },
      showTime: false,
      //暂停营业时间
      //照片变量定义
      imagehost: "https://m.aerp.com.cn/",
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
      uploadFileModelInit: {
        filename: undefined,
        fileurl: undefined,
        isDeleted: undefined,
      },
      dialogImageUrl: "",
      dialogVisible: false,
      //照片变量定义
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
      shop: {
        simpleName: "", // 简单名称
        fullName: "", // 店全称
        companyId: 0,
        companyName: "", // 公司名称
        businessType: 0, // 营业类型
        type: 2, // 门店类型
        brand: "", // 品牌
        description: "", // 描述
        provinceId: "请选择", // 省
        cityId: "请选择", // 市
        districtId: "请选择", // 区
        province: "", // 省名称
        city: "", // 市名称
        district: "", // 区名称
        address: "", // 详细地址
        longitude: 0.0, // 经度
        latitude: 0.0, // 维度
        post: "", // 邮编
        contact: "", // 联系人
        telephone: "", // 电话
        mobile: "", // 手机
        email: "", // 邮箱
        externalPhone: "", // 对外电话
        fax: "", //传真
        accountingContact: "", //对账联系人
        accountingContactPhone: "", // 对账联系人电话
        payableAccount: 0, // 应付账户
        recievableAccount: 0, // 回款账户
        accountingPerson: "", // 对账人
        rebateAccountOne: 0, // 财务账号1
        rebateAccountTwo: 0, // 财务账号2
        rebateAccountThree: 0, // 财务账号3
        accountingPeriod: "", // 对账周期
        reconciliationMode: 0, // 对账方式
        submitor: "", // 提交人
        submitorTel: "", // 提交人电话
        ownerName: "", // 门店老板姓名
        ownerPhone: "", // 门店老板电话
        tagNames: [],
        brandNames: [],
        frontImg: [],
        headImg: [],
        shopImgs: [],
        shopProofImgs: [],
        businessLienseImgs: [],
        managementLicenseImgs: [],
        systemType:1
      },
      shopConfig: {
        shopCertification: 0, // 门店认证
        startWorkTime: "", // 营业开始时间
        endWorkTime: "", // 营业结束时间
        taxNumber: "", // 税号
        contractType: 0, // 合同类型
        service: "", // // 轮胎/保养服务
        serivceLevel: 0, // 服务等级
        tireTech: "", // 技术水平
        paymentMethod: "", // 收款方式
        feePerTire: 0, // 换轮胎费用
        feePerFrontBrake: 0, // 前刹车片费用
        feePerRearBrake: 0, // 后刹车片费用
        feePerFrontDisc: 0, // 前刹车盘费用
        feePerRearDisc: 0, // 后刹车盘费用
        feePerMaintain: 0, // 保养服务费用
        feePer4Wheel: 0, // 四轮定位费用
        feePmInstallation: 0, // PM2.5滤芯安装费用
        dailyOrderQuantity: 0, // 每日保养约单量
        dailyOrderUpperLimit: 0, // 每日保养爆单量
        optionOrderCount: 0, // 保养爆单量/约单量比例
        dailyTireOrderQuantity: 0, // 每日轮胎约单量
        dailyTireOrderUpperLimit: 0, // 每日轮胎爆单量
        optionTireOrderCount: 0, // 轮胎爆单量/约单量比例
        onlineTime: "", // 正式上线时间
        leaseFreePeriod: 0, //  租赁期
        leaseStartDate: "", // 租赁开始日期
        leaseEndDate: "", // 租赁结束日期
        isVipShop: 0, // 是否是VIP门店
        jointAccount: "", // 对接账号
        businessStatus: 0, // 门店营业状态 0：营业中 1: 暂定营业
        isDistribute: 0, // 是否铺货 0：否 1：是
        lunbaoResponsiblePerson: "", // 轮保负责人
        lunbaoResponsiblePersonTel: "", // 轮保负责人电话
        meirongResponsiblePerson: "", // 美容负责人
        meirongResponsiblePersonTel: "", // 美容负责人电话
        isCheckInstallCode: 0, // 是否需要安装确认码
        suspendStartDateTime: "",
        suspendEndDateTime: "",
      },
      shopBankCard: {
        openingBank: "",
        openingBranch:"",
        bankNo: "",
        openingUserName: "",
        bankCardNo: "",
      },
      shopType: [
        {
          value: 2,
          name: "自营店",
        },
        {
          value: 1,
          name: "合作店",
        },
        {
          value: 4,
          name: "上门养护",
        },
      ],
      regionProvince: {
        regionId: "0",
      },
      regionCity: {
        regionId: "0",
      },
      regionDistrict: {
        regionId: "0",
      },
      provinceList: [], //省数据
      cityList: [], //市
      districtList: [], //区县
      regionId: 0,
      dialogFormVisible: true,
      formLabelWidth: "100px",
      fileList: [],
      addShopData: [],
      rules1: {
        feePerFrontBrake: [
          { required: true, message: "请输入数字值" },
          { type: "number", message: "请输入数字值" },
        ],
        feePerRearBrake: [
          { required: true, message: "请输入数字值" },
          { type: "number", message: "请输入数字值" },
        ],
        feePerFrontDisc: [
          { required: true, message: "请输入数字值" },
          { type: "number", message: "请输入数字值" },
        ],
        feePerRearDisc: [
          { required: true, message: "请输入数字值" },
          { type: "number", message: "请输入数字值" },
        ],
        feePerTire: [
          { required: true, message: "请输入数字值" },
          { type: "number", message: "请输入数字值" },
        ],
        feePerMaintain: [
          { required: true, message: "请输入数字值" },
          { type: "number", message: "请输入数字值" },
        ],
        feePer4Wheel: [
          { required: true, message: "请输入数字值" },
          { type: "number", message: "请输入数字值" },
        ],
        feePmInstallation: [
          { required: true, message: "请输入数字值" },
          { type: "number", message: "请输入数字值" },
        ],
        dailyOrderQuantity: [
          { required: true, message: "请输入数字值" },
          { type: "number", message: "请输入数字值" },
        ],
        optionOrderCount: [
          { required: true, message: "请输入数字值" },
          { type: "number", message: "请输入数字值" },
        ],
        dailyOrderUpperLimit: [
          { required: true, message: "请输入数字值" },
          { type: "number", message: "请输入数字值" },
        ],
        dailyTireOrderQuantity: [
          { required: true, message: "请输入数字值" },
          { type: "number", message: "请输入数字值" },
        ],
        optionTireOrderCount: [
          { required: true, message: "请输入数字值" },
          { type: "number", message: "请输入数字值" },
        ],
        dailyTireOrderUpperLimit: [
          { required: true, message: "请输入数字值" },
          { type: "number", message: "请输入数字值" },
        ],
      },
      rules: {
        simpleName: [
          { required: true, message: "请输入门店简称", trigger: "blur" },
        ],
        fullName: [
          { required: true, message: "请输入门店全称", trigger: "blur" },
        ],
        // shopCompanyName: [
        //   { required: true, message: "请输入店公司名称", trigger: "blur" },
        // ],
        type: [{ required: true, message: "请选门店类型", trigger: "change" }],
        status: [
          { required: true, message: "请选择营业状态", trigger: "change" },
        ],
        provinceId: [
          { required: true, message: "请选择省份", trigger: "change" },
        ],
        cityId: [{ required: true, message: "请选择城市", trigger: "change" }],
        districtId: [
          { required: true, message: "请选择区域", trigger: "change" },
        ],
        address: [
          { required: true, message: "请输入详细地址", trigger: "change" },
        ],
        post: [{ required: true, message: "请输入邮编", trigger: "change" }],
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
        //新增校验
        contact: [
          { required: true, message: "请输入门店联系人", trigger: "blur" },
        ],
        mobile: [
          { required: true, message: "请输入门店联系人手机", trigger: "blur" },
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
        //end
      },
      rules2: {
        startWorkTime: [
          { required: true, message: "请选择开始时间", trigger: "blur" },
        ],
        endWorkTime: [
          { required: true, message: "请选择结束时间", trigger: "blur" },
        ],
        onlineTime: [
          { required: true, message: "请选择开始日期", trigger: "blur" },
        ],
        leaseStartDate: [
          { required: true, message: "请选择开始日期", trigger: "blur" },
        ],
        leaseEndDate: [
          { required: true, message: "请选择结束日期", trigger: "blur" },
        ],
      },
      loading: false,
      companySel: [],
      companyCondition: {
        pageIndex: 1,
        pageSize: 10,
        parentId: 1,
        name: undefined,
        provinceId: undefined,
        cityId: undefined,
        districtId: undefined,
        status: "-1",
        parentId:-1
      },
    };
  },

  created() {
    //页面初始化函数
    this.fetchData();
  },

  methods: {
    statuscancel() {
      this.statusFormVisible = false;
      // this.statusFormModel.stopTime = [];
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

    modifySuspendTime() {
      this.statusFormVisible = true;
      this.statusFormTitle = "选择暂停营业时间";
    },
    statusSave() {
      //时间要赋到model中
      if (this.statusFormModel.stopTime && this.statusFormModel.stopTime.length > 0) {
        this.shopConfig.suspendStartDateTime = this.dateFormat(
          this.statusFormModel.stopTime[0]
        );
        this.shopConfig.suspendEndDateTime = this.dateFormat(
          this.statusFormModel.stopTime[1]
        );
        this.statusFormVisible = false;
        // this.statusFormModel.stopTime = [];
        
      } else {
        this.$message({
          message: "请选择暂停营业时间!",
          type: "warning",
        });
      }
    },

    changeStatus() {
      debugger;
      if (this.shop.status == 2) {
        //显示时间范围选择
        this.statusFormVisible = true;
        this.statusFormTitle = "选择暂停营业时间";
        this.statusFormModel.stopTime = [];
        this.showTime = true;
      } else {
        this.shopConfig.suspendStartDateTime = undefined;
        this.shopConfig.suspendEndDateTime = undefined;
        this.showTime = false;
      }
    },

    handlePictureCardPreview(file) {
      this.dialogImageUrl = file.url;
      this.dialogVisible = true;
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
          for (var i = 0; i < this.brandFormModel.brandSuffix.length; i++) {
            this.selectBrands.push(this.brandFormModel.brandSuffix[i]);
          }
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
    fetchData() {
      this.getProvinceByRegionId();
      this.getShopTags();
      this.getVehicleBrandList();
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
    getShopTags() {
      shopManageSvc
        .getShopTags()
        .then(
          (res) => {
            this.tags = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //获取省
    getProvinceByRegionId() {
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
      this.regionCity.regionId = provinceId;
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
    },
    //获取区
    getDistrictByRegionId(cityId) {
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
    },

    handleRemove1(file, fileList) {
      var tempFileList = [];
      for (var i = 0; i < this.fileList1.length; i++) {
        if (file.name == this.fileList1[i].name) {
          this.fileList1[i].isDeleted = 1;
        } else {
          tempFileList.push(this.fileList1[i]);
        }
      }
      if (tempFileList.length > 0) {
        this.fileList1 = tempFileList;
      } else {
        this.fileList1 = [];
      }
    },
    handleRemove2(file, fileList) {
      var tempFileList = [];
      for (var i = 0; i < this.fileList2.length; i++) {
        if (file.name == this.fileList2[i].name) {
          this.fileList2[i].isDeleted = 1;
        } else {
          tempFileList.push(this.fileList2[i]);
        }
      }
      if (tempFileList.length > 0) {
        this.fileList2 = tempFileList;
      } else {
        this.fileList2 = [];
      }
    },

    handleRemove3(file, fileList) {
      var tempFileList = [];
      for (var i = 0; i < this.fileList3.length; i++) {
        if (file.name == this.fileList3[i].name) {
          this.fileList3[i].isDeleted = 1;
        } else {
          tempFileList.push(this.fileList3[i]);
        }
      }
      if (tempFileList.length > 0) {
        this.fileList3 = tempFileList;
      } else {
        this.fileList3 = [];
      }
    },

    handleRemove4(file, fileList) {
      var tempFileList = [];
      for (var i = 0; i < this.fileList4.length; i++) {
        if (file.name == this.fileList4[i].name) {
          this.fileList4[i].isDeleted = 1;
        } else {
          tempFileList.push(this.fileList4[i]);
        }
      }
      if (tempFileList.length > 0) {
        this.fileList4 = tempFileList;
      } else {
        this.fileList4 = [];
      }
    },

    handleRemove5(file, fileList) {
      var tempFileList = [];
      for (var i = 0; i < this.fileList5.length; i++) {
        if (file.name == this.fileList5[i].name) {
          this.fileList5[i].isDeleted = 1;
        } else {
          tempFileList.push(this.fileList5[i]);
        }
      }
      if (tempFileList.length > 0) {
        this.fileList5 = tempFileList;
      } else {
        this.fileList5 = [];
      }
    },

    handleRemove6(file, fileList) {
      var tempFileList = [];
      for (var i = 0; i < this.fileList6.length; i++) {
        if (file.name == this.fileList6[i].name) {
          this.fileList6[i].isDeleted = 1;
        } else {
          tempFileList.push(this.fileList6[i]);
        }
      }
      if (tempFileList.length > 0) {
        this.fileList6 = tempFileList;
      } else {
        this.fileList6 = [];
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
      //this.$message.warning('限制是1个!')
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
              //model重新清空

              this.uploadFileModel = JSON.parse(
                JSON.stringify(this.uploadFileModelInit)
              );
              this.uploadFileModel.name = fileName;
              this.uploadFileModel.url = url;
              this.uploadFileModel.isDeleted = 0;

              this.fileList1.push(this.uploadFileModel);
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
              // loading.close();
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
              // loading.close();
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
              // loading.close();
            }
          );
        })
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    },
    // 图片上传--end ----
    // checkForm(formArr){
    //   let _self=this;
    //   let result = new Promise(function(resolve, reject) {
    //     _self.$refs[formArr].validate((valid) => {
    //       if (valid) {
    //         resolve();
    //       } else { reject() }
    //     })
    //   })
    //   _self.resultArr.push(result) //push 得到promise的结果
    // },

    //添加门店数据
    save(shop) {
      for (let i = 0; i < this.cityList.length; i++) {
        const elemen = this.shop.cityId;
        if (elemen == this.cityList[i].regionId) {
          this.shop.city = this.cityList[i].name;
          console.log("市", this.shop.city);
        }
      }
      for (let i = 0; i < this.provinceList.length; i++) {
        const elem = this.shop.provinceId;
        if (elem == this.provinceList[i].regionId) {
          this.shop.province = this.provinceList[i].name;
          console.log("省", this.shop.province);
        }
      }
      for (let i = 0; i < this.districtList.length; i++) {
        const ele = this.shop.districtId;
        if (ele == this.districtList[i].regionId) {
          this.shop.district = this.districtList[i].name;
          console.log("区", this.shop.district);
        }
      }
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

      let that = this;
      //校验
      if (that.shopConfig.startWorkTime == "") {
        this.$message({
          message: "请填写开始营业时间",
          type: "error",
          offset: "80",
        });
        return false;
      }

      if (that.shopConfig.endWorkTime == "") {
        this.$message({
          message: "请填写结束营业时间",
          type: "error",
          offset: "80",
        });
        return false;
      }

      if (that.shopConfig.onlineTime == "") {
        this.$message({
          message: "请填写正式上线时间",
          type: "error",
          offset: "80",
        });
        return false;
      }

      if (that.shopConfig.leaseStartDate == "") {
        this.$message({
          message: "请填写租赁开始时间",
          type: "error",
          offset: "80",
        });
        return false;
      }

      if (that.shopConfig.leaseEndDate == "") {
        this.$message({
          message: "请填写租赁结束时间",
          type: "error",
          offset: "80",
        });
        return false;
      }

      if (that.shop.simpleName == "") {
        this.$message({
          message: "请填写门店简称",
          type: "error",
          offset: "80",
        });
        return false;
      }

      if (that.shop.fullName == "") {
        this.$message({
          message: "请填写门店全称",
          type: "error",
          offset: "80"
        });
        return false;
      }

      // if (that.shop.shopCompanyName == "") {
      //   this.$message({
      //     message: "请填写店公司名称",
      //     type: "error",
      //     offset: "80",
      //   });
      //   return false;
      // }

      let objCompany = {};
      objCompany = that.companySel.find((item) => {
        return item.key === that.shop.companyId; //筛选出匹配数据
      });
      that.shop.companyName = objCompany.value;

      if (that.shop.type == "") {
        this.$message({
          message: "请选择门店类型",
          type: "error",
          offset: "80",
        });
        return false;
      }

      if (that.shop.address == "") {
        this.$message({
          message: "请填写详细地址",
          type: "error",
          offset: "80",
        });
        return false;
      }

      if (that.shop.post == "") {
        this.$message({
          message: "请填写邮编",
          type: "error",
          offset: "80",
        });
        return false;
      }
      //校验
      // that.formArr.forEach(item => { //根据表单的ref校验
      //   that.checkForm(item)
      // })
      // Promise.all(that.resultArr).then(function() { //都通过了
      //   that.$refs.shop.validate((valid) => {
      //       if (valid) {
      //       this.$refs.shopConfig.validate((valid) => {
      //         if (valid) {
      var shopData = {
        Shop: that.shop,
        ShopConfig: that.shopConfig,
        ShopBankCard: that.shopBankCard,
      };
      console.log("addShop", shopData);
      shopManageSvc
        .addShop(shopData)
        .then(
          (res) => {
            var data = res.code;
            if (data == "10000") {
              that.$message({
                message: "保存成功",
                type: "success",
              });
              // that.shop.tagName = undefined //保存成功后清空值
              this.goLastView(that.visitedViews, 1000);
            } else {
              this.$message({
                message: "保存失败:" + res.message,
                type: "error",
              });
            }
            setTimeout(function () {
              loading.close();
            }, 1500);
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});

      // }).catch(() => {
      //   //校验失败
      //   console.log("error");
      // })

      // }).catch(function (data) {
      //   that.$message.error(data);
      // });
      //       } else {
      //         console.log('error submit!!')
      //         return false
      //       }
      //     })
      //
      //   }else {
      //     console.log('error submit!!')
      //     return false
      //   }
      //  }
    },
    getCompanyinfo(query) {
      if (query != "") {
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          this.companyCondition.name = query;
          shopManageSvc
            .getPageCompanyListForShopAsync(this.companyCondition)
            .then(
              (res) => {
                debugger;
                if (res.data != undefined) {
                  if (
                    res.data.items != undefined &&
                    res.data.items.length > 0
                  ) {
                    this.companySel = [];
                    for (let i in res.data.items) {
                      this.companySel.push({
                        key: res.data.items[i].id,
                        value: res.data.items[i].simpleName,
                      });
                    }
                  }
                }

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
  },
};
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

  .el-header,
  .el-footer {
    background-color: #b3c0d1;
    color: #333;
    text-align: center;
    line-height: 60px;
  }

  .el-aside {
    background-color: #d3dce6;
    color: #333;
    text-align: center;
    line-height: 200px;
  }

  .el-main {
    background-color: #e9eef3;
    color: #333;
  }

  body > .el-container {
    margin-bottom: 40px;
  }
  .el-tag + .el-tag {
    margin-left: 10px;
  }
}
</style>


