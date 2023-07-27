<template>
  <div class="container">
    <el-form
      ref="formModel"
      :model="formModel"
      :rules="rules"
      label-width="120px"
      class="demo-dynamic"
    >
      <el-tabs :tab-position="'top'" type="card">
        <el-tab-pane label="基本信息">
          <el-row>
            <el-col :span="6">
              <el-form-item label="一级类目:" prop="mainCategoryId">
                <el-select
                  v-model="formModel.mainCategoryId"
                  @change="loadSecondCategorys"
                  placeholder="请选择"
                  filterable
                >
                  <el-option
                    v-for="dict in mainCategoryList"
                    :key="dict.id"
                    :label="dict.displayName"
                    :value="dict.id"
                  ></el-option>
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="二级类目:" prop="secondCategoryId">
                <el-select
                  v-model="formModel.secondCategoryId"
                  @change="loadChildCategorys"
                  placeholder="请选择"
                  filterable
                >
                  <el-option
                    v-for="dict in secondCategoryList"
                    :key="dict.id"
                    :label="dict.displayName"
                    :value="dict.id"
                  ></el-option>
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="三级类目:" prop="childCategoryId">
                <el-select
                  v-model="formModel.childCategoryId"
                  @change="loadAttributeByCategoryId"
                  filterable
                  placeholder="请选择"
                >
                  <el-option
                    v-for="dict in childCategoryList"
                    :key="dict.id"
                    :label="dict.displayName"
                    :value="dict.id"
                  ></el-option>
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="产品类型：">
                <el-radio-group v-model="formModel.classType" disabled>
                  <el-radio label="4">父产品</el-radio>
                  <el-radio label="2">子产品</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="6">
              <el-form-item label="产品编码：" prop="productCode">
                <el-input v-model="formModel.productCode" disabled />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="产品名称：" prop="name">
                <el-input type="text" v-model="formModel.name" autocomplete="off" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="产品标题：" prop="displayName">
                <el-input type="text" v-model="formModel.displayName" autocomplete="off" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="是否上架：">
                <el-radio-group v-model="formModel.onSale">
                  <el-radio :label="1">是</el-radio>
                  <el-radio :label="0">否</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="6">
              <el-form-item label="产品性质：" prop="productAttribute">
                <el-select
                  v-model="formModel.productAttribute"
                  placeholder="请选择"
                  :disabled="isEdit==1&&classType==2||classType==2&&isEdit==0||classType==4&&isEdit==1"
                >
                  <el-option
                    v-for="dict in productattributeList"
                    :key="dict.value"
                    :label="dict.label"
                    :value="dict.value"
                  />
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="单位：" prop="unit">
                <el-select v-model="formModel.unit" filterable placeholder="请选择">
                  <el-option
                    v-for="dict in unitList"
                    :key="dict.unitName"
                    :label="dict.unitName"
                    :value="dict.unitName"
                  />
                </el-select>
              </el-form-item>
            </el-col>

            <el-col :span="6">
              <el-form-item label="品牌:" prop="brand">
                <el-select v-model="formModel.brand" filterable placeholder="请选择">
                  <el-option
                    v-for="dict in brandList"
                    :key="dict.brandName"
                    :label="dict.brandName"
                    :value="dict.brandName"
                  />
                </el-select>
              </el-form-item>
            </el-col>

            <el-col :span="6">
              <el-form-item label="是否禁用：">
                <el-radio-group v-model="formModel.isDeleted">
                  <el-radio :label="1">是</el-radio>
                  <el-radio :label="0">否</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="6">
              <el-form-item label="税率：" prop="taxRate">
                <el-input type="number" v-model="formModel.taxRate" step="0.01" min="0" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="市场价" prop="standardPrice">
                <el-input
                  type="number"
                  v-model="formModel.standardPrice"
                  autocomplete="off"
                  step="0.01"
                  min="0"
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="销售价：" prop="salesPrice">
                <el-input
                  type="number"
                  v-model="formModel.salesPrice"
                  autocomplete="off"
                  step="0.01"
                  min="0"
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="是否缺货">
                <el-radio-group v-model="formModel.stockout">
                  <el-radio :label="1">是</el-radio>
                  <el-radio :label="0">否</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="6">
              <el-form-item label="长：" prop="length">
                <el-input
                  type="number"
                  step="0.01"
                  min="0"
                  v-model="formModel.length"
                  autocomplete="off"
                  @blur="function (){ formModel.length = formModel.length ? formModel.length:0  }"
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="宽：" prop="width">
                <el-input
                  type="number"
                  step="0.01"
                  min="0"
                  v-model="formModel.width"
                  autocomplete="off"
                  @blur="function (){ formModel.width = formModel.width ? formModel.width:0  }"
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="高：" prop="height">
                <el-input
                  type="number"
                  step="0.01"
                  min="0"
                  v-model="formModel.height"
                  autocomplete="off"
                  @blur="function (){ formModel.height = formModel.height ? formModel.height:0  }"
                />
              </el-form-item>
            </el-col>

            <el-col :span="6">
              <el-form-item label="是否显示">
                <el-radio-group v-model="formModel.isShow" disabled>
                  <el-radio :label="1">是</el-radio>
                  <el-radio :label="0">否</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="6">
              <el-form-item label="重量：" prop="weight">
                <el-input
                  type="number"
                  step="0.01"
                  min="0"
                  v-model="formModel.weight"
                  autocomplete="off"
                  @blur="function (){ formModel.weight = formModel.weight ? formModel.weight:0  }"
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="颜色：" prop="color">
                <el-input type="text" v-model="formModel.color" autocomplete="off" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="门店编号：" prop="shopNumber">
                <el-input
                  v-model="formModel.shopNumber"
                  autocomplete="off"
                  :disabled="formModel.isStoreoutside==0"
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="是否外采">
                <el-radio-group v-model="formModel.isStoreoutside" disabled>
                  <el-radio :label="1">是</el-radio>
                  <el-radio :label="0">否</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="6">
              <el-form-item label="配件编号：" prop="partNo">
                <el-input v-model="formModel.partNo" autocomplete="off" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="配件编码：" prop="partCode">
                <el-input v-model="formModel.partCode" autocomplete="off" />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="是否零售">
                <el-radio-group v-model="formModel.isRetail">
                  <el-radio :label="1">是</el-radio>
                  <el-radio :label="0">否</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="是否有保险">
                <el-radio-group v-model="formModel.isInsurance">
                  <el-radio :label="1">是</el-radio>
                  <el-radio :label="0">否</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="6">
              <el-form-item label="批发价：" prop="wholesalePrice">
                <el-input
                  type="number"
                  v-model="formModel.wholesalePrice"
                  autocomplete="off"
                  step="0.01"
                  min="0"
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="返现价：" prop="returnCash">
                <el-input
                  type="number"
                  v-model="formModel.returnCash"
                  autocomplete="off"
                  step="0.01"
                  min="0"
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="门店结算价：" prop="settlementPrice">
                <el-input
                  type="number"
                  v-model="formModel.settlementPrice"
                  autocomplete="off"
                  step="0.01"
                  min="0"
                />
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="父产品编号：" prop="parentId">
                <el-input
                  v-model="formModel.parentId"
                  autocomplete="off"
                  :disabled="true"
                />
              </el-form-item>
            </el-col>
          </el-row>
          <el-row v-show="isEdit==1">
            <el-col :span="6">
              <el-form-item label="创建人">
                <el-input :readonly="true" v-model="formModel.createBy"></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="创建时间">
                <el-input :readonly="true" v-model="formModel.createTime"></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="最后更新人">
                <el-input :readonly="true" v-model="formModel.updateBy"></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="最后更新时间">
                <el-input :readonly="true" v-model="formModel.updateTime"></el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="24">
              <el-form-item label="备注：" prop="remark">
                <el-input v-model="formModel.remark" type="textarea" :rows="10" autocomplete="off" />
              </el-form-item>
            </el-col>
          </el-row>
        </el-tab-pane>
        <el-tab-pane label="详情图片">
          <el-row>
            <el-col :span="4">
              <el-form-item label="图片1：">
                <el-upload
                  class="upload-demo"
                  action="http://upload.qiniup.com/"
                  :on-remove="handleRemove1"
                  :before-remove="beforeRemove"
                  :limit="1"
                  :on-exceed="handleExceed"
                  list-type="picture-card"
                  :file-list="formModel.image1 ? [{ name: '', url:imagehost+ formModel.image1 }]:[]"
                  :http-request="uploadRequest"
                  name="image1"
                  accept="image/jpeg, image/png, image/jpg"
                >
                  <el-button size="small" type="primary">上传图片</el-button>
                </el-upload>
              </el-form-item>
            </el-col>
            <el-col :span="4">
              <el-form-item label="图片2：">
                <el-upload
                  class="upload-demo"
                  action="http://upload.qiniup.com/"
                  :on-remove="handleRemove2"
                  :before-remove="beforeRemove"
                  :limit="1"
                  :on-exceed="handleExceed"
                  list-type="picture-card"
                  :file-list="formModel.image2 ? [{ name: '', url: imagehost+formModel.image2 }]:[]"
                  :http-request="uploadRequest"
                  name="image2"
                  accept="image/jpeg, image/png, image/jpg"
                >
                  <el-button size="small" type="primary">上传图片</el-button>
                </el-upload>
              </el-form-item>
            </el-col>
            <el-col :span="4">
              <el-form-item label="图片3：">
                <el-upload
                  class="upload-demo"
                  action="http://upload.qiniup.com/"
                  :on-remove="handleRemove3"
                  :before-remove="beforeRemove"
                  :limit="1"
                  :on-exceed="handleExceed"
                  list-type="picture-card"
                  :file-list="formModel.image3 ? [{ name: '', url: imagehost+formModel.image3 }]:[]"
                  :http-request="uploadRequest"
                  name="image3"
                  accept="image/jpeg, image/png, image/jpg"
                >
                  <el-button size="small" type="primary">上传图片</el-button>
                </el-upload>
              </el-form-item>
            </el-col>
            <el-col :span="4">
              <el-form-item label="图片4：">
                <el-upload
                  class="upload-demo"
                  action="http://upload.qiniup.com/"
                  :on-remove="handleRemove4"
                  :before-remove="beforeRemove"
                  :limit="1"
                  :on-exceed="handleExceed"
                  list-type="picture-card"
                  :file-list="formModel.image4 ? [{ name: '', url: imagehost+formModel.image4 }]:[]"
                  :http-request="uploadRequest"
                  name="image4"
                  accept="image/jpeg, image/png, image/jpg"
                >
                  <el-button size="small" type="primary">上传图片</el-button>
                </el-upload>
              </el-form-item>
            </el-col>
            <el-col :span="4">
              <el-form-item label="图片5：">
                <el-upload
                  class="upload-demo"
                  action="http://upload.qiniup.com/"
                  :on-remove="handleRemove5"
                  :before-remove="beforeRemove"
                  :limit="1"
                  :on-exceed="handleExceed"
                  list-type="picture-card"
                  :file-list="formModel.image5 ? [{ name: '', url: imagehost+formModel.image5 }]:[]"
                  :http-request="uploadRequest"
                  name="image5"
                  accept="image/jpeg, image/png, image/jpg"
                >
                  <el-button size="small" type="primary">上传图片</el-button>
                </el-upload>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-col :span="24">
              <el-form-item label="产品详情：" prop="description">
                <Tinymce ref="editor" v-model="formModel.description" :height="400" />
              </el-form-item>
            </el-col>
          </el-row>
        </el-tab-pane>
        <el-tab-pane label="属性信息" v-if="formModel.productAttribute!=1">
          <el-row>
            <el-col>
              <div style="margin-left:20px; width:100%">
                <vxe-table
                  border
                  resizable
                  ref="xAttributeTable"
                  highlight-hover-row
                  :data="attributeData"
                  size="medium"
                  :edit-config="{trigger: 'click', mode: 'cell', showStatus: true}"
                  @edit-actived="editActivedEvent"
                  @edit-closed="editClosedEvent"
                >
                  <vxe-table-column type="index" width="60"></vxe-table-column>
                  <vxe-table-column field="attributeDisplayName" title="属性名" width="500"></vxe-table-column>
                  <vxe-table-column
                    field="attributeValue"
                    title="属性值"
                    width="700"
                    :edit-render="{type: 'default'}"
                  >
                    <template v-slot:edit="scope">
                      <el-input
                        v-model="scope.row.attributeValue"
                        @input="$refs.xAttributeTable.updateStatus(scope)"
                      ></el-input>
                    </template>
                  </vxe-table-column>
                  <vxe-table-column field="isRequired" title="是否必填" width="200">
                    <template v-slot="scope">
                      <el-tag
                        :type="scope.row.isRequired === 0 ? '' : 'danger'"
                      >{{scope.row.isRequired === 1 ? '是' : '否'}}</el-tag>
                    </template>
                  </vxe-table-column>
                </vxe-table>
              </div>
            </el-col>
          </el-row>
        </el-tab-pane>
        <el-tab-pane label="安装服务" v-if="formModel.productAttribute!=1">
          <el-row>
            <el-col :span="6">
              <el-form-item label="服务编码">
                <el-input type="text" v-model="serviceCode" autocomplete="off" />
              </el-form-item>
            </el-col>
            <el-col :span="3">
              <el-button
                type="primary"
                icon="el-icon-plus"
                size="small"
                @click="addInstallService"
                style="margin-left:50px;"
              >添加</el-button>
            </el-col>
            <el-col :span="24">
              <el-table :data="serviceData | filterAvailableData" border style="margin-left:30px">
                <el-table-column label="服务编码" prop="serviceId" />
                <el-table-column label="服务名称" prop="serviceName" />
                <el-table-column label="价格" prop="servicePrice" />
                <el-table-column label="包安装">
                  <template slot-scope="scope">
                    <el-select v-model="scope.row.freeInstall" size="mini"
                      placeholder="是否包安装">
                      <el-option
                        v-for="dict in freeInstallEnum"
                        :key="dict.id"
                        :label="dict.displayName"
                        :value="dict.id"
                      />
                    </el-select>
                  </template>
                </el-table-column>
                <el-table-column label="服务数量">
                  <template slot-scope="scope">
                    <el-select v-model="scope.row.changeNum" size="mini"
                      placeholder="服务数量">
                      <el-option
                        v-for="dict in changeNumEnum"
                        :key="dict.id"
                        :label="dict.displayName"
                        :value="dict.id"
                      />
                    </el-select>
                  </template>
                </el-table-column>
                <el-table-column label="操作" width="200">
                  <template slot-scope="scope">
                    <el-button
                      size="mini"
                      type="danger"
                      @click="deleteInstallService(scope.$index,scope.row)"
                    >删除</el-button>
                  </template>
                </el-table-column>
              </el-table>
            </el-col>
          </el-row>
        </el-tab-pane>
        <el-tab-pane label="套餐明细" v-if="formModel.productAttribute==1" style="width:100%">
          <el-row>
            <el-col :span="5">
              <el-form-item label="产品类型：">
                <el-radio-group v-model="project.projectType">
                  <el-radio label="1">产品</el-radio>
                  <el-radio label="2">配件</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
            <el-col :span="5">
              <el-form-item label="产品编码:" v-if="project.projectType==1">
                <el-input type="text" v-model="project.productCode" autocomplete="off" />
              </el-form-item>
              <el-form-item label="配件类型:" v-else-if="project.projectType==2">
                <el-select v-model="project.baoYangType" filterable clearable placeholder="请选择">
                  <el-option
                    v-for="dict in baoYangParts"
                    :key="dict.baoYangType"
                    :label="dict.displayName"
                    :value="dict.baoYangType"
                  ></el-option>
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="5" v-if="project.projectType==2">
              <el-form-item label="项目品牌：">
                <el-select
                  v-model="project.brands"
                  multiple
                  clearable
                  filterable
                  placeholder="请选择"
                  style="width:100%"
                >
                  <el-option
                    v-for="dict in brandList"
                    :key="dict.brandName"
                    :label="dict.brandName"
                    :value="dict.brandName"
                  ></el-option>
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="3">
              <el-button
                type="primary"
                icon="el-icon-plus"
                size="small"
                @click="addPackage"
                style="margin-left:50px;"
              >添加</el-button>
            </el-col>
            <el-col :span="24">
              <div style="margin-left:20px;">
                <vxe-table
                  border
                  resizable
                  ref="xPackageTable"
                  highlight-hover-row
                  :data="packageDetailData | filterAvailableData"
                  size="mini"
                  :edit-config="{trigger: 'click', mode: 'cell', showStatus: true}"
                  @edit-actived="editActivedEvent"
                  @edit-closed="editClosedEvent"
                >
                  <vxe-table-column type="index" width="50"></vxe-table-column>
                  <vxe-table-column field="projectId" title="项目ID" width="180"></vxe-table-column>
                  <vxe-table-column field="projectName" title="项目名称" width="400"></vxe-table-column>
                  <vxe-table-column field="projectType" title="项目类型" width="80">
                    <template v-slot="scope">
                      <el-tag size="mini"
                        :type="scope.row.projectType ==1 ? '' : 'danger'"
                      >{{scope.row.projectType == 1 ? '产品' : '配件'}}</el-tag>
                    </template>
                  </vxe-table-column>
                  <vxe-table-column
                    field="projectBrands"
                    title="项目品牌"
                    width="180"
                    :edit-render="{type: 'default'}"
                  >
                    <template v-slot="{ row }">
                      <el-select
                        v-model="row.projectBrands"
                        multiple
                        filterable
                        size="mini"
                        clearable
                        @change="$refs.xPackageTable.updateStatus(row)"
                        style="width:100%"
                      >
                        <el-option
                          v-for="dict in brandList"
                          :key="dict.brandName"
                          :label="dict.brandName"
                          :value="dict.brandName"
                        ></el-option>
                      </el-select>
                    </template>
                  </vxe-table-column>
                  <vxe-table-column field="standardPrice" title="市场价" width="80"></vxe-table-column>
                  <vxe-table-column field="salesPrice" title="销售价" width="80"></vxe-table-column>
                  <vxe-table-column
                    field="quantity"
                    title="数量"
                    width="80"
                    :edit-render="{type: 'default'}"
                  >
                    <template v-slot:edit="scope">
                      <el-input size="mini"
                        v-model="scope.row.quantity"
                        @input="$refs.xPackageTable.updateStatus(scope)"
                      ></el-input>
                    </template>
                  </vxe-table-column>
                  <vxe-table-column
                    field="sort"
                    title="排序"
                    width="80"
                    :edit-render="{type: 'default'}"
                  >
                    <template v-slot:edit="scope">
                      <el-input size="mini"
                        v-model="scope.row.sort"
                        @input="$refs.xPackageTable.updateStatus(scope)"
                      ></el-input>
                    </template>
                  </vxe-table-column>
                  <vxe-table-column
                    field="replaceProduct"
                    title="替换产品"
                    width="110"
                    :edit-render="{type: 'default'}"
                  >
                    <template v-slot="{ row }">
                      <el-select
                        v-model="row.replaceProduct"
                        size="mini"
                        @change="$refs.xPackageTable.updateStatus(row)"
                        style="width:100%"
                      >
                        <el-option
                          v-for="dict in replaceProductEnum"
                          :key="dict.id"
                          :label="dict.displayName"
                          :value="dict.id"
                        ></el-option>
                      </el-select>
                    </template>
                  </vxe-table-column>
                  <vxe-table-column
                    field="categoryId"
                    title="替换产品类目"
                    width="120"
                    :edit-render="{type: 'default'}"
                  >
                    <template v-slot:edit="scope">
                      <el-input size="mini"
                        v-model="scope.row.categoryId" placeholder="最小类目，逗号分隔"
                        @input="$refs.xPackageTable.updateStatus(scope)"
                      ></el-input>
                    </template>
                  </vxe-table-column>
                  <vxe-table-column
                    field="shopCategoryId"
                    title="替换外采类目"
                    width="120"
                    :edit-render="{type: 'default'}"
                  >
                    <template v-slot:edit="scope">
                      <el-input size="mini"
                        v-model="scope.row.shopCategoryId" placeholder="最小类目，逗号分隔"
                        @input="$refs.xPackageTable.updateStatus(scope)"
                      ></el-input>
                    </template>
                  </vxe-table-column>
                  <vxe-table-column title="操作" width="80">
                    <template v-slot="scope">
                      <el-button
                        size="mini"
                        type="danger"
                        @click="deletePackage(scope.$index,scope.row)"
                      >删除</el-button>
                    </template>
                  </vxe-table-column>
                </vxe-table>
              </div>
            </el-col>
          </el-row>
        </el-tab-pane>
      </el-tabs>
      <el-row>
        <el-col :span="24" :offset="9">
          <el-form-item size="large">
            <el-button @click="cancelForm('formModel')">清 空</el-button>
            <el-button type="primary" @click="submitForm('formModel')">保 存</el-button>
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
  </div>
</template>
<script>
import { upload } from "@/utils/uploadfile";
import { appSvc } from "@/api/qiniu/qiniuservice";
import Tinymce from "@/components/Tinymce";
import { categorySearchSvc } from "@/api/product/categorysearch";
import { unitSearchSvc } from "@/api/product/unitsearch";
import { brandSearchSvc } from "@/api/product/brandsearch";
import { categoryAttributeSearchSvc } from "@/api/product/categoryAttributeSearch";
import { productSearchSvc } from "@/api/product/productsearch";
import productsearchVue from "./productsearch.vue";
export default {
  name: "productedit",
  components: { Tinymce },
  data() {
    return {
      imagehost: "https://m.aerp.com.cn/",
      options: [
        {
          value: "",
          label: "全部"
        },
        {
          value: "1",
          label: "是"
        },
        {
          value: "0",
          label: "否"
        }
      ],
      productattributeList: [
        {
          value: 0,
          label: "实物产品"
        },
        {
          value: 1,
          label: "套餐产品"
        },
        {
          value: 2,
          label: "服务产品"
        },
        {
          value: 3,
          label: "数字产品"
        }
      ],
      formModel: {
        barandName: "",
        barandImage: "",
        mainCategoryId: undefined,
        secondCategoryId: undefined,
        childCategoryId: undefined,
        onSale: 0,
        isDeleted: 0,
        isInsurance: 0,
        isRetail: 0,
        isStoreoutside: 0,
        stockout: 0,
        isShow: 1,
        image1: "",
        image2: "",
        image3: "",
        image4: "",
        image5: "",
        productattribute: 0,
        id: 0,
        parentId: 0,
        productCode: "",
        wholesalePrice:0,
        returnCash:0,
        settlementPrice:0,
        createBy:'',
        createTime:'',
        updateBy:'',
        updateTime:''
      },
      formLabelWidth: "100px",
      fileList: [],
      totalCount: 0,
      rules: {
        mainCategoryId: [
          {
            required: true,
            message: "必填！",
            trigger: "blur"
          }
        ],
        secondCategoryId: [
          {
            required: true,
            message: "必填！",
            trigger: "blur"
          }
        ],
        childCategoryId: [
          {
            required: true,
            message: "必填！",
            trigger: "blur"
          }
        ],
        unit: [
          {
            required: true,
            message: "必填！",
            trigger: "blur"
          }
        ],
        brand: [
          {
            required: true,
            message: "必填！",
            trigger: "blur"
          }
        ],
        productAttribute: [
          {
            required: true,
            message: "必填！",
            trigger: "blur"
          }
        ],
        name: [
          {
            required: true,
            message: "必填！",
            trigger: "blur"
          },
          {
            min: 1,
            max: 100,
            message: "长度应该是1到100!",
            trigger: "blur"
          }
        ],
        displayName: [
          {
            required: true,
            message: "必填！",
            trigger: "blur"
          },
          {
            max: 200,
            min: 1,
            message: "长度应该是1到200!",
            trigger: "blur"
          }
        ],
        taxRate: [
          {
            required: true,
            message: "必填！",
            trigger: "blur"
          }
        ],
        standardPrice: [
          {
            required: true,
            message: "必填！",
            trigger: "blur"
          }
        ],
        salesPrice: [
          {
            required: true,
            message: "必填！",
            trigger: "blur"
          }
        ],
        wholesalePrice:[
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              var reg = /^(0|[1-9]\d*)(\s|$|\.\d{1,2}\b)/;
              //var reg = new RegExp("([1-9]\\d*(\\.\\d{1,2})?|0\\.[1-9]\\d?|0\\.0[1-9]|0|0.0)$");
              if (!reg.test(this.formModel.wholesalePrice))
                callback(new Error("批发价输入有误，大于等于0且最多2位小数"));
              else callback();
            }
          }
        ],
        returnCash:[
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              var reg = /^(0|[1-9]\d*)(\s|$|\.\d{1,2}\b)/;
              //var reg = new RegExp("([1-9]\\d*(\\.\\d{1,2})?|0\\.[1-9]\\d?|0\\.0[1-9]|0|0.0)$");
              if (!reg.test(this.formModel.returnCash))
                callback(new Error("返现价输入有误，大于等于0且最多2位小数"));
              else callback();
            }
          }
        ],
        settlementPrice:[
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              var reg = /^(0|[1-9]\d*)(\s|$|\.\d{1,2}\b)/;
              //var reg = new RegExp("([1-9]\\d*(\\.\\d{1,2})?|0\\.[1-9]\\d?|0\\.0[1-9]|0|0.0)$");
              if (!reg.test(this.formModel.settlementPrice))
                callback(new Error("门店结算价输入有误，大于等于0且最多2位小数"));
              else callback();
            }
          }
        ]
      },
      mainCategoryList: [],
      secondCategoryList: [],
      childCategoryList: [],
      unitList: [],
      brandList: [],
      loading: false,
      classType: 2,
      isEdit: 0,
      serviceCode: "",
      serviceData: [],
      serviceDataOld: [],
      attributeData: [],
      attributeDataOld: [],
      packageDetailData: [],
      packageDetailDataOld: [],
      baoYangParts: [],
      project: {
        productCode: "",
        projectType: "1",
        baoYangType: "",
        brands: []
      },
      freeInstallEnum:[
        {id:0,displayName:'不包'},
        {id:1,displayName:'包'}
      ],
      changeNumEnum:[
        {id:0,displayName:'✖ 1'},
        {id:1,displayName:'✖ n'}
      ],
      replaceProductEnum:[
        {id:0,displayName:'不可替换'},
        {id:1,displayName:'可替换'}
      ]
    };
  },
  filters: {
    filterAvailableData: function(data) {
      var arrData = [];
      (data || []).map(function(t) {
        if (t.isDeleted == 0) {
          arrData.push(t);
        }
      });
      return arrData;
    }
  },
  computed: {
    visitedViews() {
      return this.$store.state.tagsView.visitedViews;
    }
  },
  created() {
    this.classType = this.$route.params.classType;
    this.isEdit = this.$route.params.isEdit;
    var productCode = this.$route.params.productCode;
    this.loadMainCategorys();
    this.loadBrandList();
    this.loadUnitList();
    if (productCode != "0") {
      this.loadProductDetial(productCode);
    }
    this.getBaoYangParts();
  },
  methods: {
    copyDeep(templateData) {
      // templateData 是要复制的数组或对象，这样的数组或者对象就是指向新的地址的
      return JSON.parse(JSON.stringify(templateData));
    },
    goLastView(visitedViews, time) {
      var path = this.$route.fullPath;
      var index = visitedViews.findIndex(t => t.fullPath == path);
      visitedViews.splice(index, 1);
      // if (visitedViews.length > 0) {
      //   this.$router.push("/productmanage/productsearch");
      // } else {
      //   this.$router.push("/dashboard");
      // }
      this.$router.push("/product/productsearch");
    },
    submitForm(formName) {
      this.$confirm("确认要保存数据, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.$refs[formName].validate(valid => {
            if (valid) {
              const loading = this.$loading({
                lock: true,
                text: "Loading",
                spinner: "el-icon-loading",
                background: "rgba(0, 0, 0, 0.7)"
              });
              var serviceList = this.checkData(
                this.serviceData,
                this.serviceDataOld || [],
                ["serviceId", "isDeleted","freeInstall","changeNum"],
                "serviceId"
              );
              var attributeList = this.checkData(
                this.attributeData,
                this.attributeDataOld || [],
                ["attributeNameId", "attributeValue"],
                "attributeNameId"
              );
              (this.packageDetailData || []).map(function(t) {
                t.projectBrand = t.projectBrands.join();
              });
              (this.packageDetailDataOld || []).map(function(t) {
                t.projectBrand = t.projectBrands.join();
              });
              var packageList = this.checkData(
                this.packageDetailData,
                this.packageDetailDataOld || [],
                ["projectId", "isDeleted", "quantity", "projectBrand","sort","replaceProduct","categoryId","shopCategoryId"],
                "projectId"
              );
              (packageList || []).map(function(t) {
                t.projectBrands = t.projectBrand.split(",");
              });
              if(this.formModel.createTime==''){
                this.formModel.createTime='1900-01-01'
              }
              if(this.formModel.updateTime==''){
                this.formModel.updateTime='1900-01-01'
              }
              this.formModel.classType = this.classType;
              var data = {
                Product: this.formModel,
                InstallationServices: serviceList,
                Attributevalues: attributeList.filter(function(t) {
                  if (t.attributeValue != "" && t.attributeValue != null) {
                    return t;
                  }
                }),
                packageDetails: packageList,
                IsEdit: this.isEdit == 1 ? true : false
              };
              productSearchSvc
                .saveProduct(data)
                .then(
                  res => {
                    var data = res.data;
                    if (data) {
                      this.$message({
                        message: "保存成功",
                        type: "success"
                      });
                      this.goLastView(this.visitedViews, 1000);
                    } else {
                      this.$message({
                        message: "保存失败:" + res.message,
                        type: "error"
                      });
                    }
                    setTimeout(function() {
                      loading.close();
                    }, 1500);
                  },
                  error => {
                    console.log(error);
                  }
                )
                .catch(() => {})
                .finally(() => {
                  loading.close();
                });
            } else {
              this.$message({
                message: "请核对必填信息！",
                type: "warning"
              });
              return false;
            }
          });
        })
        .catch(() => {});
    },
    cancelForm(formName) {
      this.$confirm("确认要清空数据, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.$refs[formName].resetFields();
        })
        .catch(() => {});
    },
    // 图片上传--start ----
    beforeRemove(file, fileList) {
      return this.$confirm(`确认删除?`);
    },
    //临时写法 需优化
    handleRemove1(file, fileList) {
      this.formModel.image1 = "";
    },
    handleRemove2(file, fileList) {
      this.formModel.image2 = "";
    },
    handleRemove3(file, fileList) {
      this.formModel.image3 = "";
    },
    handleRemove4(file, fileList) {
      this.formModel.image4 = "";
    },
    handleRemove5(file, fileList) {
      this.formModel.image5 = "";
    },
    handleExceed(files, fileList) {
      this.$message.warning("限制是1个!");
    },
    //获取token
    getToken: function(directoryName, fileName) {
      return appSvc.getQiNiuToken({
        directory: directoryName,
        fileName: fileName
      });
    },
    uploadRequest: function(request) {
      var name = request.filename;
      var fileName = request.file.name;
      var directoryName = "productImage";
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        appSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        appSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;
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
            // var url = host + "/" + key;
            // var img = { name: fileName, url: url };
            // curren.filelist = [img];
            this.formModel[name] = key;
          }
        );
      });
    },
    // 图片上传--end ----
    // 分页
    editActivedEvent({ row, column }, event) {
      console.log(`打开 ${column.title} 列编辑`);
    },
    editClosedEvent({ row, column }, event) {
      console.log(`关闭 ${column.title} 列编辑`);
    },
    //查询主分类
    loadMainCategorys: function() {
      categorySearchSvc
        .getCategorysById({
          categoryId: 0,
          level: 1
        })
        .then(
          res => {
            var data = res.data;
            this.mainCategoryList = data;
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
        .finally(() => {});
    },
    //查询二级分类
    loadSecondCategorys: function(categoryId) {
      if (!categoryId) {
        return;
      }
      categorySearchSvc
        .getCategorysById({
          categoryId: categoryId,
          level: 2
        })
        .then(
          res => {
            var data = res.data;
            this.secondCategoryList = data;
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
        .finally(() => {});
    },
    //查询三级分类
    loadChildCategorys: function(categoryId) {
      if (!categoryId) {
        return;
      }
      categorySearchSvc
        .getCategorysById({
          categoryId: categoryId,
          level: 3
        })
        .then(
          res => {
            var data = res.data;
            this.childCategoryList = data;
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
        .finally(() => {});
    },
    //根据分类查询属性信息
    loadAttributeByCategoryId: function(categoryId, callBack) {
      if (!categoryId) {
        return;
      }
      categoryAttributeSearchSvc
        .getAttributesByCategoryId({
          categoryId: categoryId
        })
        .then(
          res => {
            var data = res.data;
            this.attributeData = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    //查询单位
    loadUnitList: function() {
      unitSearchSvc
        .getUnits()
        .then(
          res => {
            var data = res.data;
            this.unitList = data;
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
        .finally(() => {});
    },
    //查询品牌
    loadBrandList: function() {
      brandSearchSvc
        .getBrandList()
        .then(
          res => {
            var data = res.data;
            this.brandList = data;
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
        .finally(() => {});
    },
    //查询商品详情
    loadProductDetial: function(productCode) {
      if (!productCode) {
        return;
      }
      productSearchSvc
        .getProductByProductCode({
          productCode: productCode
        })
        .then(
          res => {
            var data = res.data;
            if (data) {
              this.formModel = data.product;
              this.formModel.classType = this.classType;
              this.attributeData = data.attributevalues;
              this.serviceData = data.installationServices;
              this.packageDetailData = data.packageDetails;
              if (this.isEdit == "0") {
                this.formModel.productCode = "";
                if(this.classType==2){
                  this.formModel.parentId = this.formModel.id;
                }
                else{
                  this.formModel.parentId = 0;
                }
                this.formModel.id = 0;
                this.attributeDataOld = [];
                this.serviceDataOld = [];
                this.packageDetailDataOld = [];
              } else {
                this.attributeDataOld = this.copyDeep(data.attributevalues);
                this.serviceDataOld = this.copyDeep(data.installationServices);
                this.packageDetailDataOld = this.copyDeep(data.packageDetails);
              }
              if (this.formModel.mainCategoryId) {
                this.loadSecondCategorys(this.formModel.mainCategoryId);
              }
              if (this.formModel.secondCategoryId) {
                this.loadChildCategorys(this.formModel.secondCategoryId);
              }
              this.$refs.editor.setContent(data.product.description);
            console.log("1024");
            console.log(this.isEdit);
            console.log(this.classType);
            console.log(this.formModel);
            }
           
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    //添加安装服务
    addInstallService: function() {
      if (!this.serviceCode) {
        return;
      }
      productSearchSvc
        .getProductByProductCode({
          productCode: this.serviceCode
        })
        .then(
          res => {
            var data = res.data;
            if (data) {
              var product = data.product;
              if (product == null) {
                this.$message({
                  showClose: true,
                  message: "商品不存在！",
                  type: "warning"
                });
                return;
              }
              var isExists = false;
              this.serviceData.map(function(a) {
                if (a.serviceId == product.productCode && a.isDeleted == 0) {
                  isExists = true;
                }
              });
              //需要验证是否是服务
              //&& product.productAttribute==2
              if(product.productAttribute != 2){
                this.$message({
                  showClose: true,
                  message: "只能添加服务产品！",
                  type: "warning"
                });
                return;
              }

              if (product.classType == 4) {
                this.$message({
                  showClose: true,
                  message: "只能添加子产品！",
                  type: "warning"
                });
                return;
              }
              if (!isExists) {
                this.serviceData.push({
                  serviceName: product.name,
                  servicePrice: product.salesPrice,
                  serviceId: product.productCode,
                  freeInstall:0,
                  changeNum:0,
                  isDeleted: 0
                });
              }else{
                this.$message({
                  showClose: true,
                  message: "当前服务已存在！",
                  type: "warning"
                });
              }
            } else {
              this.$message({
                showClose: true,
                message: "服务编码不存在！",
                type: "error"
              });
            }
            this.serviceCode = "";
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    //删除安装服务
    deleteInstallService: function(index, row) {
      var serviceList = this.serviceData;
      serviceList.map(function(a) {
        if (a.productCode == row.productCode) {
          //serviceList.splice(index, 1);
          row.isDeleted = 1;
        }
      });
      this.serviceData = serviceList || [];
    },
    //添加套餐明细
    addPackage: function() {
      if (this.project.projectType == 1) {
        if (this.project.productCode == "") {
          this.$message({
            showClose: true,
            message: "产品编码必填！",
            type: "error"
          });
          return;
        }
        productSearchSvc
          .getProductByProductCode({
            productCode: this.project.productCode
          })
          .then(
            res => {
              var data = res.data;
              if (data) {
                var product = data.product;
                if (product == null) {
                  this.$message({
                    showClose: true,
                    message: "商品不存在！",
                    type: "warning"
                  });
                  return;
                }
                var isExists = false;
                this.packageDetailData.map(function(a) {
                  if (a.projectId == product.productCode && a.isDeleted == 0) {
                    isExists = true;
                  }
                });
                //需要验证是否是服务
                if (product.classType == 4) {
                  this.$message({
                    showClose: true,
                    message: "只能添加子产品！",
                    type: "warning"
                  });
                  return;
                }
                // //不能添加套餐
                // if (product.productAttribute == 1) {
                //   this.$message({
                //     showClose: true,
                //     message: "不能添加套餐产品！",
                //     type: "warning"
                //   });
                //   return;
                // }
                if (!isExists) {
                  this.packageDetailData.unshift({
                    projectName: product.name,
                    standardPrice: product.standardPrice,
                    salesPrice: product.salesPrice,
                    projectId: product.productCode,
                    isDeleted: 0,
                    quantity: 1,
                    projectType: this.project.projectType,
                    projectBrands: [product.brand],
                    sort: 1,
                    replaceProduct:0,
                    categoryId:'',
                    shopCategoryId:''
                  });
                }
              } else {
                this.$message({
                  showClose: true,
                  message: "产品编码不存在！",
                  type: "error"
                });
              }
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {})
          .finally(() => {});
      } else if (this.project.projectType == 2) {
        var isExists = false;
        if (this.project.baoYangType == "") {
          this.$message({
            showClose: true,
            message: "请选择配件类型！",
            type: "error"
          });
          return;
        }
        if (this.project.brands.length == 0) {
          this.$message({
            showClose: true,
            message: "请选择项目品牌！",
            type: "error"
          });
          return;
        }
        var baoYangType = this.project.baoYangType;
        this.packageDetailData.map(function(a) {
          if (a.projectId == baoYangType && a.isDeleted == 0) {
            isExists = true;
          }
        });
        if (!isExists) {
          var baoYangPart = this.baoYangParts.find(
            t => t.baoYangType == baoYangType
          );
          if (baoYangPart) {
            this.packageDetailData.unshift({
              projectName: baoYangPart.displayName,
              standardPrice: 0,
              salesPrice: 0,
              projectId: baoYangPart.baoYangType,
              isDeleted: 0,
              quantity: 1,
              projectType: this.project.projectType,
              projectBrands: this.project.brands,
              sort: 1,
              replaceProduct:0,
              categoryId:'',
              shopCategoryId:''
            });
          }
        }
      }
      this.project = {
        productCode: "",
        projectType: "1",
        baoYangType: "",
        brands: []
      };
    },
    //删除套餐明细
    deletePackage: function(index, row) {
      var packageDetailList = this.packageDetailData;
      packageDetailList.map(function(a) {
        if (a.projectId == row.projectId) {
          //serviceList.splice(index, 1);
          row.isDeleted = 1;
        }
      });
      this.packageDetailData = packageDetailList || [];
    },
    /**
     * newList 新数据
     * oldList 旧数据
     * compareArr array 需要检查的字段
     * primaryKey 主键，原数组与拼装数组的主键相同时才会产生
     * return 返回检查后的结果集  newList 和  oldList 结构必须一样
     */
    checkData: function(newList, oldList, compareArr, primaryKey) {
      if (!compareArr || !newList) {
        return [];
      }
      var fixedArr = this.copyDeep(oldList || []);
      return (newList || []).reduce(function(inAssembleArr, item) {
        // 查找拼装数组中需要更新的数据
        if (fixedArr.length > 0) {
          for (var i = 0; i < inAssembleArr.length; i++) {
            var b = inAssembleArr[i];
            if (item[primaryKey] == 0 || b[primaryKey] == 0) {
              continue;
            }
            // 主键值相同时才会有更新的数据产生
            if (item[primaryKey] == b[primaryKey]) {
              var isChange = false;
              for (var j = 0; j < compareArr.length; j++) {
                var compare = compareArr[j];
                if (item[compare] != b[compare]) {
                  isChange = true;
                  b[compare] = item[compare];
                }
              }
              if (isChange) {
                return inAssembleArr;
              }
              // 更新队列中删除没有做过变更的项
              inAssembleArr.splice(i, 1);
              return inAssembleArr;
            }
          }
        }
        inAssembleArr.unshift(item);
        return inAssembleArr;
      }, oldList || []);
    },
    //查询配件类型
    getBaoYangParts: function() {
      productSearchSvc
        .getBaoYangParts()
        .then(
          res => {
            var data = res.data;
            this.baoYangParts = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    }
  }
};
</script>
<style lang="scss" scoped>
.container {
  margin: 10px 0px 0px 0px;
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
  .input-label {
    display: inline-block;
    width: 100px;
    margin-left: 40px;
  }
}
</style>
