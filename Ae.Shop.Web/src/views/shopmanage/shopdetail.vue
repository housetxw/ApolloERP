<template>
  <main class="container">
    <!-- 详情页面 -->
    <section id="indexPage" style="margin-top:20px;">                     
      <el-form :model="formModel" :inline="true" ref="formModel">
        <router-link :to="{name:'shopservicetypeindex', query: { shopId: id }}">
                  <el-button type="primary" style="margin-bottom:5px;">门店服务类型</el-button>
                </router-link>
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
                  <el-input v-model="shop.simpleName" placeholder="请输入门店简称"/>
                </el-form-item>

                <el-form-item label="门店全称:" prop="fullName">
                  <el-input v-model="shop.fullName" placeholder="请输入门店全称"/>
                </el-form-item>
              </el-row>
              <el-row :gutter="10">
                <el-form-item label="店公司名称:" prop="shopCompanyName">
                  <el-input v-model="shop.shopCompanyName" placeholder="请输入公司名称"/>
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="门店类型:" prop="type">
                  <el-radio-group v-model="shop.type">
                    <el-radio :label="1">合作店</el-radio>
                    <el-radio :label="2">自营店</el-radio>
                  </el-radio-group>
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="营业状态:" prop="status">
                  <el-radio-group v-model="shop.status" @change="changeStatus()">
                    <el-radio :label="0">正常营业</el-radio>
                    <el-radio :label="1">终止合作</el-radio>
                    <el-radio :label="2">暂停营业</el-radio>
                  </el-radio-group>
                  <div v-show="showTime">
                     
                 <label>开始时间:{{shopConfig.suspendStartDateTime}}</label>&nbsp;&nbsp;
                  &nbsp;<label>结束时间:{{shopConfig.suspendEndDateTime}}</label>
                  &nbsp;&nbsp;
                    <el-button type="text" size="small" @click="modifySuspendTime()">修改暂停营业时间</el-button>
                
                  </div>
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="门店联系人:" prop="contact">
                  <el-input v-model="shop.contact" placeholder="请输入联系人"/>
                </el-form-item>
                <el-form-item label="电话:" prop="telephone">
                  <el-input v-model="shop.telephone" placeholder="请输入电话"/>
                </el-form-item>
                <el-form-item label="手机:" prop="mobile">
                  <el-input v-model="shop.mobile" placeholder="请输入手机"/>
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="运营负责人:" prop="head">
                  <el-input v-model="shop.head" placeholder="请输入运营负责人"/>
                </el-form-item>
                <el-form-item label="电话:" prop="headPhone">
                  <el-input v-model="shop.headPhone" placeholder="请输入运营负责人电话"/>
                </el-form-item>
                <el-form-item label="邮箱:" prop="headEmail">
                  <el-input v-model="shop.headEmail" placeholder="请输入运营负责人邮箱"/>
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="门店老板:" prop="ownerName">
                  <el-input v-model="shop.ownerName" placeholder="请输入门店老板"/>
                </el-form-item>
                <el-form-item label="老板电话:" prop="ownerPhone">
                  <el-input v-model="shop.ownerPhone" placeholder="请输入电话"/>
                </el-form-item>

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
                  >{{tag}}</el-tag>
                  <el-button type="text" size="small" @click="addTag()">添加标签</el-button>
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
                  >{{tag}}</el-tag>
                  <el-button type="text" size="small" @click="addBrand()">添加专修品牌</el-button>
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item>
                  <el-button icon="el-icon-check" type="primary" @click="ModifyShopBaseInfo()">保存</el-button>
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
            <el-row :gutter="20">
              <el-form-item label="地址:">
                <el-select v-model="shop.provinceId" @change="changeProvince" placeholder="请选择">
                  <el-option
                    v-for="item in provinceList"
                    :key="item.regionId"
                    :label="item.name"
                    :value="item.regionId"
                  ></el-option>
                </el-select>
                <el-select v-model="shop.cityId" placeholder="市">
                  <el-option
                    v-for="item in cityList"
                    :key="item.regionId"
                    :label="item.name"
                    :value="item.regionId"
                  ></el-option>
                </el-select>
                <el-select v-model="shop.districtId" placeholder="区">
                  <el-option
                    v-for="item in districtList"
                    :key="item.regionId"
                    :label="item.name"
                    :value="item.regionId"
                  ></el-option>
                </el-select>
              </el-form-item>
            </el-row>
            <el-row :gutter="20">
              <el-form-item label="详细地址:">
                <el-input v-model="shop.address" style="width:400px;" placeholder="请输入详细地址"/>
              </el-form-item>
              <el-form-item label="邮编:">
                <el-input v-model="shop.post" placeholder="请输入邮编"/>
              </el-form-item>
            </el-row>
            <el-row :gutter="20">
              <el-form-item label="纬度:" prop="latitude">
                <el-input v-model="shop.latitude" placeholder="请输入纬度"/>
              </el-form-item>
              <el-form-item label="经度:" prop="longitude">
                <el-input v-model="shop.longitude" placeholder="请输入经度"/>
              </el-form-item>
            <el-link href="http://www.gpsspg.com/maps.htm" target="_blank"><label style="color:red;font-weight:600;font-size:18px;">地图搜索</label>
  <i class="el-icon-view el-icon--right"></i> </el-link>
                  
                &nbsp;&nbsp;&nbsp;<label>(请选择腾讯高德的坐标！)</label>
            </el-row>
            <el-row :gutter="20">
              <el-form-item>
                <el-button icon="el-icon-check" type="primary" @click="ModifyShopAddress()">保存</el-button>
              </el-form-item>
            </el-row>
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
                        style="width: 100%;"
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
                        style="width: 100%;"
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
              <el-row :gutter="20">
                <el-form-item>
                  <el-button icon="el-icon-check" type="primary" @click="ModifyShopConfigInfo()">保存</el-button>
                </el-form-item>
              </el-row>
            </el-form>
          </el-main>
        </el-container>
        <!-- 配置信息结束 -->
        <!-- 服务信息开始 -->
        <el-container>
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
                  <el-input v-model.number="shopConfig.feePerFrontBrake"/>
                </el-form-item>
                <el-form-item label="后刹车片费用:" prop="feePerRearBrake">
                  <el-input v-model.number="shopConfig.feePerRearBrake"/>
                </el-form-item>
                <el-form-item label="前刹车盘费用:" prop="feePerFrontDisc">
                  <el-input v-model.number="shopConfig.feePerFrontDisc"/>
                </el-form-item>
                <el-form-item label="后刹车盘费用:" prop="feePerRearDisc">
                  <el-input v-model.number="shopConfig.feePerRearDisc"/>
                </el-form-item>
              </el-row>
              <el-row :gutter="60">
                <el-form-item label="换轮胎费用:" prop="feePerTire">
                  <el-input v-model.number="shopConfig.feePerTire"/>
                </el-form-item>
                <el-form-item label="保养服务费用:" prop="feePerMaintain">
                  <el-input v-model.number="shopConfig.feePerMaintain"/>
                </el-form-item>
                <el-form-item label="四轮定位费用:" prop="feePer4Wheel">
                  <el-input v-model.number="shopConfig.feePer4Wheel"/>
                </el-form-item>
                <el-form-item label="PM2.5滤芯安装费用:" prop="feePmInstallation">
                  <el-input v-model.number="shopConfig.feePmInstallation"/>
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="每日保养约单量:" prop="dailyOrderQuantity">
                  <el-input v-model.number="shopConfig.dailyOrderQuantity"/>
                </el-form-item>
                <el-form-item label="保养爆单量/保养约单量:" prop="optionOrderCount">
                  <el-input v-model.number="shopConfig.optionOrderCount"/>
                </el-form-item>
                <el-form-item label="每日保养爆单量:" prop="dailyOrderUpperLimit">
                  <el-input v-model.number="shopConfig.dailyOrderUpperLimit"/>
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item label="每日轮胎约单量:" prop="dailyTireOrderQuantity">
                  <el-input v-model.number="shopConfig.dailyTireOrderQuantity"/>
                </el-form-item>
                <el-form-item label="轮胎爆单量/轮胎约单量:" prop="optionTireOrderCount">
                  <el-input v-model.number="shopConfig.optionTireOrderCount"/>
                </el-form-item>
                <el-form-item label="每日轮胎爆单量:" prop="dailyTireOrderUpperLimit">
                  <el-input v-model.number="shopConfig.dailyTireOrderUpperLimit"/>
                </el-form-item>
              </el-row>
              <el-row :gutter="20">
                <el-form-item>
                  <el-button icon="el-icon-check" type="primary" @click="ModifyShopConfigExpense()">保存</el-button>
                </el-form-item>
              </el-row>
            </el-form>
          </el-main>
        </el-container>
        <!-- 服务信息结束 -->
        <!-- 图片信息开始 -->
        <el-container>
          <el-header>图片信息 <label style="color:red;font-weight:600">*上传图片大小不能超过5MB</label></el-header>
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
                  :on-exceed="handleExceed"
                  :file-list="tempfileList1"
                  list-type="picture-card"
                  :http-request="uploadRequest1"
                  name="headImg"
                   :before-upload="beforeAvatarUpload"
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
                  :on-exceed="handleExceed"
                 :file-list="tempfileList2"
                 list-type="picture-card"
                  :http-request="uploadRequest2"
                    accept="image/jpeg, image/png, image/jpg"
                  name="frontImg"
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
                    :file-list="tempfileList3"
                  list-type="picture-card"
                  :http-request="uploadRequest3"
                  name="shopImgs"
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
                    :file-list="tempfileList4"
                   list-type="picture-card"
                  :http-request="uploadRequest4"
                  name="shopProofImgs"
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
                  :on-exceed="handleExceed"
                    :file-list="tempfileList5"
                   list-type="picture-card"
                  :http-request="uploadRequest5"
                  name="shopProofImgs"
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
                  :on-exceed="handleExceed"
                    :file-list="tempfileList6"
                   list-type="picture-card"
                  :http-request="uploadRequest6"
                  name="shopProofImgs"
                >
                   <i class="el-icon-plus"></i>
                </el-upload>                 
              </el-form-item>
            </el-row>
            <el-row :gutter="20">
              <el-form-item>
                <el-button icon="el-icon-check" type="primary" @click="AddShopImgs()">保存</el-button>
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
                <el-input v-model="shopBankCard.openingBank"/>
              </el-form-item>
            </el-row>
            <el-row :gutter="20">
              <el-form-item label="开户银行行号:">
                <el-input v-model="shopBankCard.bankNo"/>
              </el-form-item>
              <el-form-item label="开户人姓名:">
                <el-input v-model="shopBankCard.openingUserName"/>
              </el-form-item>
              <el-form-item label="银行卡号:">
                <el-input v-model="shopBankCard.bankCardNo"/>
              </el-form-item>
            </el-row>
            <el-row :gutter="20">
              <el-form-item>
                <el-button icon="el-icon-check" type="primary" @click="ModifyShopBankAccount()">保存</el-button>
              </el-form-item>
            </el-row>
          </el-main>
        </el-container>
      </el-form>
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
            <el-input clearable placeholder="输入标签名称" v-model="tagFormModel.tagName"></el-input>
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
              style="width:400px;"
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
          <el-button @click="statusCancel('statusFormModel')">取消</el-button>
          
          <el-button type="primary" @click="statusSave('statusFormModel')">确定</el-button>
        </div>
      </el-dialog>
    </section>
  </main>
</template>


<script>
import { Loading } from 'element-ui';
  import { Message, MessageBox } from 'element-ui'
  import { shopManageSvc } from '@/api/shopmanage/shopmanage'
  import { upload } from '@/utils/uploadfile'
  import { appSvc } from '@/api/qiniu/qiniuservice'

  export default {
    data() {
      // var checkSale = (rule, value, callback) => {
      //   if (!value) {
      //     return callback(new Error('不能为空'));
      //   }
      //   setTimeout(() => {
      //     if (!Number.isInteger(value)) {
      //       callback(new Error('请输入数字值'));
      //     }
      //   }, 1000);
      // };
      return {

      
        //照片变量定义
         imagehost: "https://m.aerp.com.cn/",
        fileList1:[],
        fileList2:[],
        fileList3:[],
        fileList4:[],
        fileList5:[],
        fileList6:[],

deleteCondition:{
  imgId:undefined,
  shopId:undefined,
},

tempfileList1:[],
        tempfileList2:[],
        tempfileList3:[],
        tempfileList4:[],

         tempfileList5:[],
        tempfileList6:[],

        uploadFileModel: {
        name: undefined,
        url: undefined,
        isDeleted: undefined
      },
      uploadFileModelInit: {
        filename: undefined,
        fileurl: undefined,
        isDeleted: undefined
      },
      dialogImageUrl: "",
      dialogVisible: false,
      //暂停营业时间
statusFormTitle:undefined,
statusFormVisible:false,
statusFormModel:{
  stopTime:[]
},
showTime:false,
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
            }
          },
          {
            text: "最近一个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近三个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit("pick", [start, end]);
            }
          }
        ]
      },
        //照片变量定义
        formModel: {
          shop: [],
          shopConfig: [],
          shopBankCard: []
        },


      shopImageModel:{
        shopId:undefined,
        headImg:[],
        frontImg:[],
        shopImgs:[],
        shopProofImgs:[],
        businessLicenseImgs:[],
        managementLicenseImgs:[]
      },

 shopImageModelInit:{
        shopId:undefined,
        headImg:[],
        frontImg:[],
        shopImgs:[],
        shopProofImgs:[],
         businessLicenseImgs:[],
        managementLicenseImgs:[]
      },

        //标签+专修品牌
      tags: [],
      brands: [],
      selectBrands: [],
   
      tagFormTitle: undefined,
      tagFormVisible: false,
      tagFormModel: {
        tagName: undefined
      },
      brandFormTitle: undefined,
      brandFormVisible: false,
      brandFormModel: {
        brandSuffix: []
      },
      brandCondition:{
        shopId:undefined
      },
      
      tempBrandArr:[],
      //标签+专修品牌
        shop: {
          simpleName: '', // 简单名称
          fullName: '', // 店全称
          shopCompanyName: '', // 店公司名称
          businessType: 0, // 营业类型
          type: 0, // 门店类型
          brand: '', // 品牌
          description: '', // 描述
          provinceId: '', // 省
          cityId: '', // 市
          districtId: '', // 区
          address: '', // 详细地址
          longitude: 0.0, // 经度
          latitude: 0.0, // 维度
          post: '', // 邮编
          contact: '', // 联系人
          telephone: '', // 电话
          mobile: '', // 手机
          email: '', // 邮箱
          externalPhone: '', // 对外电话
          fax: '', //传真
          accountingContact: '', //对账联系人
          accountingContactPhone: '', // 对账联系人电话
          payableAccount: 0, // 应付账户
          recievableAccount: 0, // 回款账户
          accountingPerson: '', // 对账人
          rebateAccountOne: 0, // 财务账号1
          rebateAccountTwo: 0, // 财务账号2
          rebateAccountThree: 0, // 财务账号3
          accountingPeriod: '', // 对账周期
          reconciliationMode: 0, // 对账方式
          submitor: '', // 提交人
          submitorTel: '', // 提交人电话
          ownerName: '', // 门店老板姓名
          ownerPhone: '', // 门店老板电话
          headImg: [], //门头图片
          frontImg: [], //正面照
          shopImgs: [], //门店照
          shopProofImgs: [], //资质证明照
          businessLienseImgs:[],
          managementLicenseImgs:[],
          tagNames: [],
          brandNames: []
        },
        shopConfig: {
          shopCertification: 0, // 门店认证
          startWorkTime: '', // 营业开始时间
          endWorkTime: '', // 营业结束时间
          taxNumber: '', // 税号
          contractType: 0, // 合同类型
          service: '', // // 轮胎/保养服务
          serivceLevel: 0, // 服务等级
          tireTech: '', // 技术水平
          paymentMethod: '', // 收款方式
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
          onlineTime: '', // 正式上线时间
          leaseFreePeriod: 0, //  租赁期
          leaseStartDate: '', // 租赁开始日期
          leaseEndDate: '', // 租赁结束日期
          isVipShop: 0, // 是否是VIP门店
          jointAccount: '', // 对接账号
          isDistribute: 0, // 是否铺货 0：否 1：是
          lunbaoResponsiblePerson: '', // 轮保负责人
          lunbaoResponsiblePersonTel: '', // 轮保负责人电话
          meirongResponsiblePerson: '', // 美容负责人
          meirongResponsiblePersonTel: '', // 美容负责人电话
          suspendStartDateTime: '', // 暂停营业 开始时间
          suspendEndDateTime: '', // 暂停营业 结束时间
          isLoungeRoom: 0, //是否有休息室
          isRestRoom: 0, //卫生间
          isFreeWifi: 0, //免费wifi
          isPostLift: 0, //柱式举升机
          carFaultDiagnosticTool: 0 //汽车故障诊断仪
        },
        shopBankCard: {
          openingBank: '',
          bankNo: '',
          openingUserName: '',
          bankCardNo: ''
        },

        provinceList: [], //省数据
        cityList: [], //市
        districtList: [], //区县
        regionCondition: {
          regionId: '0'
        },
        regionCity: {
          regionId: '0'
        },
        regionDistrict: {
          regionId: '0'
        },
        shopInfoCondition: {
          shopId: 0
        },
        shopImgs: {
          url: ''
        },
        shopProofImgs: {
          url: ''
        },
        dialogFormVisible: true,
        formLabelWidth: '100px',
        fileList: [],
        condition: {
          barandName: '',
          isDisable: '',
          currentPage: 3
        },
        id: 0,
        rules1: {
          feePerFrontBrake: [
            { required: true, message: '请输入数字值' },
            { type: 'number', message: '请输入数字值' }
          ],
          feePerRearBrake: [
            { required: true, message: '请输入数字值' },
            { type: 'number', message: '请输入数字值' }
          ],
          feePerFrontDisc: [
            { required: true, message: '请输入数字值' },
            { type: 'number', message: '请输入数字值' }
          ],
          feePerRearDisc: [
            { required: true, message: '请输入数字值' },
            { type: 'number', message: '请输入数字值' }
          ],
          feePerTire: [
            { required: true, message: '请输入数字值' },
            { type: 'number', message: '请输入数字值' }
          ],
          feePerMaintain: [
            { required: true, message: '请输入数字值' },
            { type: 'number', message: '请输入数字值' }
          ],
          feePer4Wheel: [
            { required: true, message: '请输入数字值' },
            { type: 'number', message: '请输入数字值' }
          ],
          feePmInstallation: [
            { required: true, message: '请输入数字值' },
            { type: 'number', message: '请输入数字值' }
          ],
          dailyOrderQuantity: [
            { required: true, message: '请输入数字值' },
            { type: 'number', message: '请输入数字值' }
          ],
          optionOrderCount: [
            { required: true, message: '请输入数字值' },
            { type: 'number', message: '请输入数字值' }
          ],
          dailyOrderUpperLimit: [
            { required: true, message: '请输入数字值' },
            { type: 'number', message: '请输入数字值' }
          ],
          dailyTireOrderQuantity: [
            { required: true, message: '请输入数字值' },
            { type: 'number', message: '请输入数字值' }
          ],
          optionTireOrderCount: [
            { required: true, message: '请输入数字值' },
            { type: 'number', message: '请输入数字值' }
          ],
          dailyTireOrderUpperLimit: [
            { required: true, message: '请输入数字值' },
            { type: 'number', message: '请输入数字值' }
          ]
        },
        rules: {
          simpleName: [
            { required: true, message: '请输入门店简称', trigger: 'blur' }
          ],
          fullName: [
            { required: true, message: '请输入门店全称', trigger: 'blur' }
          ],
          shopCompanyName: [
            { required: true, message: '请输入公司名称', trigger: 'blur' }
          ],
           //新增校验
          contact:[
            { required: true, message: '请输入门店联系人', trigger: 'blur' }
          ],         
           mobile:[
            { required: true, message: '请输入门店联系人手机', trigger: 'blur' }
          ],
           head:[
            { required: true, message: '请输入运营负责人', trigger: 'blur' }
          ],
           headPhone:[
            { required: true, message: '请输入运营负责人手机', trigger: 'blur' }
          ],
           ownerName:[
            { required: true, message: '请输入门店老板', trigger: 'blur' }
          ],
           ownerPhone:[
            { required: true, message: '请输入老板手机', trigger: 'blur' }
          ]
          //end
        },
        rules2: {
          startWorkTime: [
            { required: true, message: '请选择开始时间', trigger: 'blur' }
          ],
          endWorkTime: [
            { required: true, message: '请选择结束时间', trigger: 'blur' }
          ],
          onlineTime: [
            { required: true, message: '请选择开始日期', trigger: 'blur' }
          ],
          leaseStartDate: [
            { required: true, message: '请选择开始日期', trigger: 'blur' }
          ],
          leaseEndDate: [
            { required: true, message: '请选择结束日期', trigger: 'blur' }
          ]
        }

      }
    },

    created() {
      //页面初始化函数
      this.fetchData()
    },
    mounted() {
      this.id = this.$route.params.id
      console.log('shopId: ' + this.id)
    },
    methods: {
modifySuspendTime(){
    this.statusFormVisible=true;
      this.statusFormTitle ='选择暂停营业时间';
},
statuscancel(){
  this.statusFormVisible=false;
  this.statusFormModel.stopTime=[];
},
dateFormat(time) {
		var date=new Date(time);
		var year=date.getFullYear();
		/* 在日期格式中，月份是从0开始的，因此要加0
		 * 使用三元表达式在小于10的前面加0，以达到格式统一  如 09:11:05
		 * */
		var month= date.getMonth()+1<10 ? "0"+(date.getMonth()+1) : date.getMonth()+1;
		var day=date.getDate()<10 ? "0"+date.getDate() : date.getDate();
		var hours=date.getHours()<10 ? "0"+date.getHours() : date.getHours();
		var minutes=date.getMinutes()<10 ? "0"+date.getMinutes() : date.getMinutes();
		var seconds=date.getSeconds()<10 ? "0"+date.getSeconds() : date.getSeconds();
		// 拼接
		return year+"-"+month+"-"+day+" "+hours+":"+minutes+":"+seconds;
},

statusSave(){
this.statusFormVisible =false;
//时间要赋到model中
if(this.statusFormModel.stopTime.length>0){
  this.shopConfig.suspendStartDateTime =this.dateFormat(this.statusFormModel.stopTime[0]);
  this.shopConfig.suspendEndDateTime =this.dateFormat(this.statusFormModel.stopTime[1]);

  this.statusFormModel.stopTime=[];
  this.showTime=true;
}

},

changeStatus(){
  debugger;
  if(this.shop.status ==2){
      //显示时间范围选择
       this.statusFormVisible=true;
      this.statusFormTitle ='选择暂停营业时间';
    }else{
      this.shopConfig.suspendStartDateTime='';
      this.shopConfig.suspendEndDateTime='';
      this.showTime=false;
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
            type: "warning"
          });
          return;
        }
        if (this.tagFormModel.tagName.length > 6) {
          this.$message({
            message: "标签长度最多为6个字符!",
            type: "warning"
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
                  type: "warning"
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
          }else{
              this.tags.push(this.tagFormModel.tagName);

              this.tagFormModel.tagName = undefined;
              this.tagFormVisible = false;
          }
        }
      } else {
        this.$message({
          message: "标签不能为空!",
          type: "warning"
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
            type: "warning"
          });
          return;
        }
        if (
          this.selectBrands.length + this.brandFormModel.brandSuffix.length >
          5
        ) {
          this.$message({
            message: "最多添加5个专修品牌!",
            type: "warning"
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
              type: "warning"
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
          type: "warning"
        });
      }
    },
      fetchData() {
        this.getShopById()
      //  this.getShopTags()  //可供选择的标签
        this.getVehicleBrandList();  //可供选择的品牌
        this.getShopServiceBrands(); //已选择的品牌
      },

 getShopServiceBrands() {
   this.brandCondition.shopId =  this.$route.params.id;
      shopManageSvc
        .getShopServiceBrands(this.brandCondition)
        .then(
          res => {
            if(res.data!=null && res.data.length>0){
              for(var i =0;i<res.data.length;i++){
                this.selectBrands.push(res.data[i].brand);
              }
            }
            //this.brands = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

      getVehicleBrandList() {
      shopManageSvc
        .getVehicleBrandList()
        .then(
          res => {
            this.brands = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

      //获取门店信息
      getShopById() {
        this.shopInfoCondition.shopId = this.$route.params.id
        console.log('shopId: ' + JSON.stringify(this.shopInfoCondition))

        shopManageSvc
          .getShopById(this.shopInfoCondition)
          .then(
            res => {
              debugger;
              this.shop = res.data.shop
              console.log('门店信息: ' + JSON.stringify(this.shop))
              this.tempfileList1= this.shop.headImg;
              this.tempfileList2 = this.shop.frontImg;
              this.tempfileList3 = this.shop.shopImgs;
              this.tempfileList4 = this.shop.shopProofImgs;
              this.tempfileList5 = this.shop.businessLienseImgs;
              this.tempfileList6 = this.shop.managementLicenseImgs;


              this.shopImgs = this.shop.shopImgs
              this.shopProofImgs = this.shop.shopProofImgs
              this.shopConfig = res.data.shopConfig
              if(this.shop.status ==2 ){
                this.showTime =true;
              }
              this.shopBankCard = res.data.shopBankCard
              this.getProvinceByRegionId()
              this.getCityByRegionId(this.shop.provinceId)
              this.getDistrictByRegionId(this.shop.cityId)
              if (res.data.shop.tagNames.length > 0) {
                this.tags = res.data.shop.tagNames
              }
            },
            error => {
              console.log(error)
            }
          )
          .catch(() => {
          })
      },
      //获取省
      getProvinceByRegionId() {
        shopManageSvc
          .getRegionChinaListByRegionId(this.regionCondition)
          .then(
            res => {
              this.provinceList = res.data
            },
            error => {
              console.log(error)
            }
          )
          .catch(() => {
          })
      },
      //获取市
      getCityByRegionId(provinceId) {
        if(provinceId!='' && provinceId!=undefined){
        this.regionCity.regionId = provinceId
        shopManageSvc
          .getRegionChinaListByRegionId(this.regionCity)
          .then(
            res => {
              this.cityList = res.data
            },
            error => {
              console.log(error)
            }
          )
          .catch(() => {
          })
        }
      },
      //获取区
      getDistrictByRegionId(cityId) {
        if(cityId!='' && cityId!=undefined){
        
        this.regionDistrict.regionId = cityId
        shopManageSvc
          .getRegionChinaListByRegionId(this.regionDistrict)
          .then(
            res => {
              this.districtList = res.data
            },
            error => {
              console.log(error)
            }
          )
          .catch(() => {
          })
        }
      },

      changeProvince(provinceId) {
        console.log('provinceId: ' + JSON.stringify(provinceId))
        this.regionCondition.regionId = provinceId
        shopManageSvc
          .getRegionChinaListByRegionId(this.regionCondition)
          .then(
            res => {
              this.cityList = res.data
            },
            error => {
              console.log(error)
            }
          )
          .catch(() => {
          })
      },
      handleRemove1(file, fileList) {
 //如果该图片是新追加的 但是还没有保存到数据  移除了也要清掉
      if (file.imgId == undefined) {
        if (this.fileList1.length > 0) {
          for (var i = 0; i < this.fileList1.length; i++) {
            if (this.fileList1[i].name == file.name) {
              this.fileList1[i].isDeleted = 1;
            }
          }
        }
      } else {
        //原来已经存在的
        debugger;
        this.deleteCondition.imgId = file.imgId;
       this.deleteCondition.shopId = this.id;
        shopManageSvc
          .DeleteShopImg(this.deleteCondition)
          .then(
            res => {
              //从数据库中删除,并且从Array中remove
              for (var i = 0; i < this.tempfileList1.length; i++) {
                if (
                  this.tempfileList1[i].imgId ==
                  this.deleteCondition.imgId
                ) {
                  this.tempfileList1[i].isDeleted = 1;
                }
              }
              this.deleteCondition.imgId = undefined;
              this.deleteCondition.shopId =undefined;
            },
            error => {
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
              this.fileList2[i].isDeleted = 1;
            }
          }
        }
      } else {
        //原来已经存在的
        debugger;
        this.deleteCondition.imgId = file.imgId;
       this.deleteCondition.shopId = this.id;
        shopManageSvc
          .DeleteShopImg(this.deleteCondition)
          .then(
            res => {
              //从数据库中删除,并且从Array中remove
              for (var i = 0; i < this.tempfileList2.length; i++) {
                if (
                  this.tempfileList2[i].imgId ==
                  this.deleteCondition.imgId
                ) {
                  this.tempfileList2[i].isDeleted = 1;
                }
              }
              this.deleteCondition.imgId = undefined;
              this.deleteCondition.shopId =undefined;
            },
            error => {
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
              this.fileList3[i].isDeleted = 1;
            }
          }
        }
      } else {
        //原来已经存在的
        debugger;
        this.deleteCondition.imgId = file.imgId;
       this.deleteCondition.shopId = this.id;
        shopManageSvc
          .DeleteShopImg(this.deleteCondition)
          .then(
            res => {
              //从数据库中删除,并且从Array中remove
              for (var i = 0; i < this.tempfileList3.length; i++) {
                if (
                  this.tempfileList3[i].imgId ==
                  this.deleteCondition.imgId
                ) {
                  this.tempfileList3[i].isDeleted = 1;
                }
              }
              this.deleteCondition.imgId = undefined;
              this.deleteCondition.shopId =undefined;
            },
            error => {
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
              this.fileList4[i].isDeleted = 1;
            }
          }
        }
      } else {
        //原来已经存在的
        debugger;
        this.deleteCondition.imgId = file.imgId;
       this.deleteCondition.shopId = this.id;
        shopManageSvc
          .DeleteShopImg(this.deleteCondition)
          .then(
            res => {
              //从数据库中删除,并且从Array中remove
              for (var i = 0; i < this.tempfileList4.length; i++) {
                if (
                  this.tempfileList4[i].imgId ==
                  this.deleteCondition.imgId
                ) {
                  this.tempfileList4[i].isDeleted = 1;
                }
              }
              this.deleteCondition.imgId = undefined;
              this.deleteCondition.shopId =undefined;
            },
            error => {
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
              this.fileList5[i].isDeleted = 1;
            }
          }
        }
      } else {
        //原来已经存在的
        debugger;
        this.deleteCondition.imgId = file.imgId;
       this.deleteCondition.shopId = this.id;
        shopManageSvc
          .DeleteShopImg(this.deleteCondition)
          .then(
            res => {
              //从数据库中删除,并且从Array中remove
              for (var i = 0; i < this.tempfileList5.length; i++) {
                if (
                  this.tempfileList5[i].imgId ==
                  this.deleteCondition.imgId
                ) {
                  this.tempfileList5[i].isDeleted = 1;
                }
              }
              this.deleteCondition.imgId = undefined;
              this.deleteCondition.shopId =undefined;
            },
            error => {
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
              this.fileList6[i].isDeleted = 1;
            }
          }
        }
      } else {
        //原来已经存在的
        debugger;
        this.deleteCondition.imgId = file.imgId;
       this.deleteCondition.shopId = this.id;
        shopManageSvc
          .DeleteShopImg(this.deleteCondition)
          .then(
            res => {
              //从数据库中删除,并且从Array中remove
              for (var i = 0; i < this.tempfileList6.length; i++) {
                if (
                  this.tempfileList6[i].imgId ==
                  this.deleteCondition.imgId
                ) {
                  this.tempfileList6[i].isDeleted = 1;
                }
              }
              this.deleteCondition.imgId = undefined;
              this.deleteCondition.shopId =undefined;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
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
        this.$message.warning('限制是1个!')
      },
      beforeRemove(file, fileList) {
        return this.$confirm(`确认删除?`)
      },
      //获取token
      getToken(directoryName, fileName) {
        return shopManageSvc.getQiNiuToken({
          directory: directoryName,
          fileName: fileName
        })
      },
     
      uploadRequest1: function(request) {
      debugger;

      const loading = this.$loading({
          lock: true,
          text: '拼命上传中......',
          spinner: 'el-icon-loading',
          background: 'rgba(0, 0, 0, 0.7)'
        });

      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        shopManageSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        shopManageSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

       var directoryName = 'Shops/Images'
      var key = directoryName + "/" + newFileName;
      this.getToken(directoryName, fileName).then(res => {
        var result = res.data;
        const token = result.token;
        const host = result.host;
        upload(
          token,
          key,
          request,
          next => {
            const total = next.total;
          },
          error => {
            this.fileList = [];
          },
          complete => {
            const hash = complete.hash;
            const key = complete.key;
            var url = host + "/" + key;
            //model重新清空

            this.uploadFileModel = JSON.parse(
              JSON.stringify(this.uploadFileModelInit)
            );
            this.uploadFileModel.name = fileName;
            this.uploadFileModel.url = key;
            this.uploadFileModel.isDeleted = 0;

            this.fileList1.push(this.uploadFileModel);
           // loading.close();
          }
        );
      }).catch(() => {
            })
            .finally(() => {
              loading.close();
            })
    },

     uploadRequest2: function(request) {
        const loading = this.$loading({
          lock: true,
          text: '拼命上传中......',
          spinner: 'el-icon-loading',
          background: 'rgba(0, 0, 0, 0.7)'
        });
      debugger;
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        shopManageSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        shopManageSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

       var directoryName = 'Shops/Images'
      var key = directoryName + "/" + newFileName;
      this.getToken(directoryName, fileName).then(res => {
        var result = res.data;
        const token = result.token;
        const host = result.host;
        upload(
          token,
          key,
          request,
          next => {
            const total = next.total;
          },
          error => {
            this.fileList = [];
          },
          complete => {
            const hash = complete.hash;
            const key = complete.key;
            var url = host + "/" + key;
            //model重新清空

            this.uploadFileModel = JSON.parse(
              JSON.stringify(this.uploadFileModelInit)
            );
            this.uploadFileModel.name = fileName;
            this.uploadFileModel.url = key;
            this.uploadFileModel.isDeleted = 0;

            this.fileList2.push(this.uploadFileModel);
           // loading.close();
          }
        );
      }).catch(() => {
            })
            .finally(() => {
              loading.close();
            })
    },

     uploadRequest3: function(request) {
        const loading = this.$loading({
          lock: true,
          text: '拼命上传中......',
          spinner: 'el-icon-loading',
          background: 'rgba(0, 0, 0, 0.7)'
        });
      debugger;
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        shopManageSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        shopManageSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

       var directoryName = 'Shops/Images'
      var key = directoryName + "/" + newFileName;
      this.getToken(directoryName, fileName).then(res => {
        var result = res.data;
        const token = result.token;
        const host = result.host;
        upload(
          token,
          key,
          request,
          next => {
            const total = next.total;
          },
          error => {
            this.fileList = [];
          },
          complete => {
            const hash = complete.hash;
            const key = complete.key;
            var url = host + "/" + key;
            //model重新清空

            this.uploadFileModel = JSON.parse(
              JSON.stringify(this.uploadFileModelInit)
            );
            this.uploadFileModel.name = fileName;
            this.uploadFileModel.url = key;
            this.uploadFileModel.isDeleted = 0;

            this.fileList3.push(this.uploadFileModel);
          //  loading.close();
          }
        );
      }).catch(() => {
            })
            .finally(() => {
              loading.close();
            })
    },


     uploadRequest4: function(request) {
        const loading = this.$loading({
          lock: true,
          text: '拼命上传中......',
          spinner: 'el-icon-loading',
          background: 'rgba(0, 0, 0, 0.7)'
        });
      debugger;
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        shopManageSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        shopManageSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

       var directoryName = 'Shops/Images'
      var key = directoryName + "/" + newFileName;
      this.getToken(directoryName, fileName).then(res => {
        var result = res.data;
        const token = result.token;
        const host = result.host;
        upload(
          token,
          key,
          request,
          next => {
            const total = next.total;
          },
          error => {
            this.fileList = [];
          },
          complete => {
            const hash = complete.hash;
            const key = complete.key;
            var url = host + "/" + key;
            //model重新清空

            this.uploadFileModel = JSON.parse(
              JSON.stringify(this.uploadFileModelInit)
            );
            this.uploadFileModel.name = fileName;
            this.uploadFileModel.url = key;
            this.uploadFileModel.isDeleted = 0;

            this.fileList4.push(this.uploadFileModel);
            //loading.close();
          }
        );
      }).catch(() => {
            })
            .finally(() => {
              loading.close();
            })
    },

    
     uploadRequest5: function(request) {
        const loading = this.$loading({
          lock: true,
          text: '拼命上传中......',
          spinner: 'el-icon-loading',
          background: 'rgba(0, 0, 0, 0.7)'
        });
      debugger;
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        shopManageSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        shopManageSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

       var directoryName = 'Shops/Images'
      var key = directoryName + "/" + newFileName;
      this.getToken(directoryName, fileName).then(res => {
        var result = res.data;
        const token = result.token;
        const host = result.host;
        upload(
          token,
          key,
          request,
          next => {
            const total = next.total;
          },
          error => {
            this.fileList = [];
          },
          complete => {
            const hash = complete.hash;
            const key = complete.key;
            var url = host + "/" + key;
            //model重新清空

            this.uploadFileModel = JSON.parse(
              JSON.stringify(this.uploadFileModelInit)
            );
            this.uploadFileModel.name = fileName;
            this.uploadFileModel.url = key;
            this.uploadFileModel.isDeleted = 0;

            this.fileList5.push(this.uploadFileModel);
            //loading.close();
          }
        );
      }).catch(() => {
            })
            .finally(() => {
              loading.close();
            })
    },



    
     uploadRequest6: function(request) {
        const loading = this.$loading({
          lock: true,
          text: '拼命上传中......',
          spinner: 'el-icon-loading',
          background: 'rgba(0, 0, 0, 0.7)'
        });
      debugger;
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        shopManageSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        shopManageSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;

       var directoryName = 'Shops/Images'
      var key = directoryName + "/" + newFileName;
      this.getToken(directoryName, fileName).then(res => {
        var result = res.data;
        const token = result.token;
        const host = result.host;
        upload(
          token,
          key,
          request,
          next => {
            const total = next.total;
          },
          error => {
            this.fileList = [];
          },
          complete => {
            const hash = complete.hash;
            const key = complete.key;
            var url = host + "/" + key;
            //model重新清空

            this.uploadFileModel = JSON.parse(
              JSON.stringify(this.uploadFileModelInit)
            );
            this.uploadFileModel.name = fileName;
            this.uploadFileModel.url = key;
            this.uploadFileModel.isDeleted = 0;

            this.fileList6.push(this.uploadFileModel);
            loading.close();
          }
        );
      }).catch(() => {
            })
            .finally(() => {
              loading.close();
            })
    },

      // 图片上传--end ----

      //修改门店基本数据
      ModifyShopBaseInfo(shop) {
        debugger;
       if (this.tags.length > 0) {
       this.shop.tagNames =this.tags;       
      }

      if (this.selectBrands.length > 0) {
        this.shop.brandNames = this.selectBrands;
      }

        this.$refs.shop.validate((valid) => {
          if (valid) {
            shopManageSvc
              .ModifyShopBaseInfo({
                ShopId: this.id,
                simpleName: this.shop.simpleName, // 简单名称
                fullName: this.shop.fullName, // 店全称
                shopCompanyName: this.shop.shopCompanyName, // 店公司名称
                businessType: this.shop.businessType, // 营业类型
                type: this.shop.type, // 门店类型
                Status: this.shop.status,
                Contact: this.shop.contact,
                Telephone: this.shop.telephone,
                Mobile: this.shop.mobile,
                OwnerName: this.shop.ownerName,
                OwnerPhone: this.shop.ownerPhone,
                Head: this.shop.head,
                HeadPhone: this.shop.headPhone,
                HeadEmail: this.shop.headEmail,
                UpdateBy: '',
                tagNames: this.shop.tagNames,
                brandNames:this.shop.brandNames
              })
              .then(
                res => {
                  var data = res.code
                  if (data == '10000') {
                    this.$message({
                      message: '保存成功',
                      type: 'success'
                    })
                    this.goLastView(this.visitedViews, 1000)
                  } else {
                    this.$message({
                      message: '保存失败:' + res.message,
                      type: 'error'
                    })
                  }
                  setTimeout(function() {
                    loading.close()
                  }, 1500)
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
            console.log('error submit!!')
            return false
          }
        })
        // }
      },
      //修改门店地址
      ModifyShopAddress() {
        var longreg = /^[\-\+]?(0(\.\d{1,10})?|([1-9](\d)?)(\.\d{1,10})?|1[0-7]\d{1}(\.\d{1,10})?|180\.0{1,10})$/
        var latreg = /^[\-\+]?((0|([1-8]\d?))(\.\d{1,10})?|90(\.0{1,10})?)$/
        if (!longreg.test(this.shop.longitude)) {
          this.$message({
            message: '经度整数部分为0-180,小数部分为0到10位!',
            type: 'error'
          })
          return false
        }
        if (!latreg.test(this.shop.latitude)) {
          this.$message({
            message: '纬度整数部分为0-90,小数部分为0到10位!',
            type: 'error'
          })
          return false
        } else {

          let objProvince = {}
          objProvince = this.provinceList.find(item => {
            //model就是上面的数据源
            return item.regionId === this.shop.provinceId //筛选出匹配数据
          })
          debugger;
          var provinceName = objProvince.name
          let objCity = {}
          objCity = this.cityList.find(item => {
            //model就是上面的数据源
            return item.regionId === this.shop.cityId //筛选出匹配数据
          })
          var cityName = objCity.name
          let objDistrict = {}
          objDistrict = this.districtList.find(item => {
            //model就是上面的数据源
            return item.regionId === this.shop.districtId //筛选出匹配数据
          })
          var districtName = objDistrict.name

          shopManageSvc
            .ModifyShopAddress({
              ShopId: this.id,
              ProvinceId: this.shop.provinceId,
              CityId: this.shop.cityId,
              DistrictId: this.shop.districtId,
              Province: provinceName,
              City: cityName,
              District: districtName,
              Address: this.shop.address,
              Longitude: this.shop.longitude,
              Latitude: this.shop.latitude,
              Post: this.shop.post,
              UpdateBy: ''
            })

            .then(
              res => {
                var data = res.code
                if (data == '10000') {
                  this.$message({
                    message: '保存成功',
                    type: 'success'
                  })
                  this.goLastView(this.visitedViews, 1000)
                } else {
                  this.$message({
                    message: '保存失败:' + res.message,
                    type: 'error'
                  })
                }
                setTimeout(function() {
                  loading.close()
                }, 1500)
              },
              error => {
                console.log(error)
              }
            )
            .catch(() => {
            })
            .finally(() => {
            })
        }
      },
      //修改门店配置信息
      ModifyShopConfigInfo(shopConfig) {

        this.$refs.shopConfig.validate((valid) => {
          if (valid) {
            shopManageSvc
              .ModifyShopConfigInfo({
                ShopId: this.id,
                StartWorkTime: this.shopConfig.startWorkTime,
                EndWorkTime: this.shopConfig.endWorkTime,
                OnlineTime: this.shopConfig.onlineTime,
                LeaseStartDate: this.shopConfig.leaseStartDate,
                LeaseEndDate: this.shopConfig.leaseEndDate,
                CarFaultDiagnosticTool: this.shopConfig.carFaultDiagnosticTool,
                IsLoungeRoom: this.shopConfig.isLoungeRoom,
                IsRestRoom: this.shopConfig.isRestRoom,
                IsFreeWifi: this.shopConfig.isFreeWifi,
                IsPostLift: this.shopConfig.isPostLift,
                UpdateBy: ''
              })
              .then(
                res => {
                  var data = res.code
                  if (data == '10000') {
                    this.$message({
                      message: '保存成功',
                      type: 'success'
                    })
                    this.goLastView(this.visitedViews, 1000)
                  } else {
                    this.$message({
                      message: '保存失败:' + res.message,
                      type: 'error'
                    })
                  }
                  setTimeout(function() {
                    loading.close()
                  }, 1500)
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
      //修改门店服务信息

      ModifyShopConfigExpense(shopConfig) {

        this.$refs.shopConfig.validate((valid) => {
          if (valid) {
            shopManageSvc
              .ModifyShopConfigExpense({
                ShopId: this.id,
                FeePerTire: this.shopConfig.feePerTire,
                FeePerFrontBrake: this.shopConfig.feePerFrontBrake,
                FeePerRearBrake: this.shopConfig.feePerRearBrake,
                FeePerFrontDisc: this.shopConfig.feePerFrontDisc,
                FeePerRearDisc: this.shopConfig.feePerRearDisc,
                FeePerMaintain: this.shopConfig.feePerMaintain,
                FeePer4Wheel: this.shopConfig.feePer4Wheel,
                FeePmInstallation: this.shopConfig.feePmInstallation,
                DailyOrderQuantity: this.shopConfig.dailyOrderQuantity,
                DailyOrderUpperLimit: this.shopConfig.dailyOrderUpperLimit,
                DailyTireOrderQuantity: this.shopConfig.dailyTireOrderQuantity,
                DailyTireOrderUpperLimit: this.shopConfig.dailyTireOrderUpperLimit,
                UpdateBy: ''
              })
              .then(
                res => {
                  var data = res.code
                  if (data == '10000') {
                    this.$message({
                      message: '保存成功',
                      type: 'success'
                    })
                    this.goLastView(this.visitedViews, 1000)
                  } else {
                    this.$message({
                      message: '保存失败:' + res.message,
                      type: 'error'
                    })
                  }
                  setTimeout(function() {
                    loading.close()
                  }, 1500)
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
            console.log('error submit!!')
            return false
          }
        })
      },
      //修改门店银行账户信息
      ModifyShopBankAccount() {
        shopManageSvc
          .ModifyShopBankAccount({
            ShopId: this.id,
            OpeningBank: this.shopBankCard.openingBank,
            BankNo: this.shopBankCard.bankNo,
            OpeningUserName: this.shopBankCard.openingUserName,
            BankCardNo: this.shopBankCard.bankCardNo,
            UpdateBy: ''
          })
          .then(
            res => {
              var data = res.code
              if (data == '10000') {
                this.$message({
                  message: '保存成功',
                  type: 'success'
                })
                this.goLastView(this.visitedViews, 1000)
              } else {
                this.$message({
                  message: '保存失败:' + res.message,
                  type: 'error'
                })
              }
              setTimeout(function() {
                loading.close()
              }, 1500)
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
      //添加门店图片信息
      AddShopImgs() {
        //图片list
        debugger;
//将新加的图片传进去
          if (this.fileList1.length > 0) {
            var newFileList = [];
            for (var i = 0; i < this.fileList1.length; i++) {
              if (this.fileList1[i].isDeleted == 0) {
                newFileList.push(this.fileList1[i]);
              }
            }
            if (newFileList.length > 0) {
              this.shopImageModel.headImg =newFileList;
             // this.friendjokesDetailFormModel.attachments = newFileList;
            }
          } else {
            //如果未编辑图片 将数据清空
           // this.friendjokesDetailFormModel.attachments = [];
           this.shopImageModel.headImg =[];
          }

          if (this.fileList2.length > 0) {
            var newFileList = [];
            for (var i = 0; i < this.fileList2.length; i++) {
              if (this.fileList2[i].isDeleted == 0) {
                newFileList.push(this.fileList2[i]);
              }
            }
            if (newFileList.length > 0) {
              this.shopImageModel.frontImg =newFileList;
             // this.friendjokesDetailFormModel.attachments = newFileList;
            }
          } else {
            //如果未编辑图片 将数据清空
           // this.friendjokesDetailFormModel.attachments = [];
           this.shopImageModel.frontImg =[];
          }


          if (this.fileList3.length > 0) {
            var newFileList = [];
            for (var i = 0; i < this.fileList3.length; i++) {
              if (this.fileList3[i].isDeleted == 0) {
                newFileList.push(this.fileList3[i]);
              }
            }
            if (newFileList.length > 0) {
              this.shopImageModel.shopImgs =newFileList;
             // this.friendjokesDetailFormModel.attachments = newFileList;
            }
          } else {
            //如果未编辑图片 将数据清空
           // this.friendjokesDetailFormModel.attachments = [];
           this.shopImageModel.shopImgs =[];
          }

          if (this.fileList4.length > 0) {
            var newFileList = [];
            for (var i = 0; i < this.fileList4.length; i++) {
              if (this.fileList4[i].isDeleted == 0) {
                newFileList.push(this.fileList4[i]);
              }
            }
            if (newFileList.length > 0) {
              this.shopImageModel.shopProofImgs =newFileList;
             // this.friendjokesDetailFormModel.attachments = newFileList;
            }
          } else {
            //如果未编辑图片 将数据清空
           // this.friendjokesDetailFormModel.attachments = [];
           this.shopImageModel.shopProofImgs =[];
          }


 if (this.fileList5.length > 0) {
            var newFileList = [];
            for (var i = 0; i < this.fileList5.length; i++) {
              if (this.fileList5[i].isDeleted == 0) {
                newFileList.push(this.fileList5[i]);
              }
            }
            if (newFileList.length > 0) {
              this.shopImageModel.businessLicenseImgs =newFileList;
             // this.friendjokesDetailFormModel.attachments = newFileList;
            }
          } else {
            //如果未编辑图片 将数据清空
           // this.friendjokesDetailFormModel.attachments = [];
           this.shopImageModel.businessLicenseImgs =[];
          }



           if (this.fileList6.length > 0) {
            var newFileList = [];
            for (var i = 0; i < this.fileList6.length; i++) {
              if (this.fileList6[i].isDeleted == 0) {
                newFileList.push(this.fileList6[i]);
              }
            }
            if (newFileList.length > 0) {
              this.shopImageModel.managementLicenseImgs =newFileList;
             // this.friendjokesDetailFormModel.attachments = newFileList;
            }
          } else {
            //如果未编辑图片 将数据清空
           // this.friendjokesDetailFormModel.attachments = [];
           this.shopImageModel.managementLicenseImgs =[];
          }

        this.shopImageModel.shopId = this.id;
        shopManageSvc
          .AddShopImgs(this.shopImageModel)
          .then(
            res => {
              var data = res.code
              this.shopImageModel = JSON.parse(
        JSON.stringify(this.shopImageModelInit)
      );
              //一定要将四个上传数组的数据清除!!!!
              this.fileList1=[];
              this.fileList2=[];
              this.fileList3=[];
              this.fileList4=[];

               this.fileList5=[];
              this.fileList6=[];

              if (data == '10000') {
                this.$message({
                  message: '保存成功',
                  type: 'success'
                })
                this.goLastView(this.visitedViews, 1000)
              } else {
                this.$message({
                  message: '保存失败:' + res.message,
                  type: 'error'
                })
              }
              setTimeout(function() {
                loading.close()
              }, 1500)
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
      //删除门店图片信息
      DeleteShopImg() {
        shopManageSvc
          .DeleteShopImg({
            ShopId: this.id,
            ImgId: 0,
            UpdateBy: ''
          })
          .then(
            res => {
              var data = res.code
              if (data == '10000') {
                this.$message({
                  message: '保存成功',
                  type: 'success'
                })
                this.goLastView(this.visitedViews, 1000)
              } else {
                this.$message({
                  message: '保存失败:' + res.message,
                  type: 'error'
                })
              }
              setTimeout(function() {
                loading.close()
              }, 1500)
            },
            error => {
              console.log(error)
            }
          )
          .catch(() => {
          })
          .finally(() => {
          })
      }
    }
  }
</script>

<style lang="scss" scoped>
  .container {
    margin-left: 30px;

    .indexPage {
      height: 88;
      display: flex;
    }

    .pagination {
      display: flex;
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

    .dot_img {
      width: 300px;
      height: 300px;
      float: left;
    }
  }

  > > > .el-row {
    margin-left: -20px;
  }

  > > > .el-form-item__label {
    padding: 0 5px 0 0;
  }
   .el-tag + .el-tag {
    margin-left: 10px;
  }
</style>
