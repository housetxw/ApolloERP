<template>
  <main class="container">
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalCount"
      :tableLoading="tableLoading"
      :headerBtnWidth="175"
      :tableData="tableData"
      :pageChange="handleSizeChange"
      :searching="search"
    >
      <template v-slot:condition>
        <el-form-item>
          <el-select
            v-model="condition.shopId"
            filterable
            clearable
            reserve-keyword
            placeholder="请输入门店名称"
            :loading="loading"
            v-if="orgType == '0'"
          >
            <el-option
              v-for="item in shopSel"
              :key="item.id"
              :label="item.simpleName"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>
        <!-- <el-form-item>
          <el-select
            v-model="condition.warehouseId"
            filterable
            clearable
            reserve-keyword
            placeholder="请输入仓库名称"
            :loading="loading"
          >
            <el-option
              v-for="item in warehouseSel"
              :key="item.id"
              :label="item.simpleName"
              :value="item.id"
            ></el-option>
          </el-select>
        </el-form-item>  -->
        <el-form-item>
          <el-input
            v-model="condition.purchaseOrderId"
            placeholder="采销单号"
            autocomplete="off"
            style="width:120px"
          />
        </el-form-item>
        <el-form-item>
          <el-input
            v-model="condition.key"
            autocomplete="off"
            style="width:160px"
            placeholder="产品名字(编码)"
          />
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.venderId"
            filterable
            clearable
            :loading="loading"
            placeholder="供应商/客户"
            style="width:160px"
          >
            <el-option
              v-for="item in supplierList"
              :key="item.id"
              :label="item.venderShortName"
              :value="item.id"
            />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.saleCompanyId"
            filterable
            clearable
            :loading="loading"
            placeholder="内销公司"
            style="width:160px"
            v-if="orgType == '0'"
          >
            <el-option
              v-for="item in companyList"
              :key="item.id"
              :label="item.simpleName"
              :value="item.id"
            />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.purchaseType"
            filterable
            clearable
            :loading="loading"
            placeholder="单据类型"
            style="width:160px"
            v-if="orgType == '0'"
          >
            <el-option
              v-for="item in purchaseTypeList"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.status"
            placeholder="单据状态"
            filterable
            clearable
            style="width:125px"
          >
            <el-option
              v-for="(name, value) in purchaseOrderStatusList"
              :key="value"
              :value="value"
              :label="name"
            />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.payStatus"
            placeholder="付款状态"
            filterable
            clearable
            style="width:100px"
          >
            <el-option
              v-for="dict in payStatusList"
              :key="dict.value"
              :label="dict.label.replace('全部', '付款状态')"
              :value="dict.value"
            />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.payType"
            placeholder="结算方式"
            filterable
            clearable
            style="width:125px"
          >
            <el-option
              v-for="item in payTypeList"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            />
          </el-select>
        </el-form-item>

        <el-form-item>
          <el-tooltip
            class="item"
            effect="dark"
            content="采购日期"
            placement="bottom"
          >
            <el-date-picker
              v-model="condition.purchaseDates"
              type="daterange"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              format="yyyy-MM-dd "
              value-format="yyyy-MM-dd"
              style="width:220px"
              :picker-options="$root.$data.timeRgPickerOpt"
            ></el-date-picker>
          </el-tooltip>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button
          type="primary"
          size="mini"
          icon="el-icon-plus"
          @click="handleAdd"
          >新增</el-button
        >

        <!-- <el-button
          type="primary"
          size="mini"
          icon="el-icon-plus"
          @click="exportData"
          >导出记录</el-button
        > -->
      </template>
      <template v-slot:tb_cols>
        <rg-table-column label="单据类型" prop="status" align="center" fix-min>
          <template slot-scope="scope">{{
            purchaseTypeSel[scope.row.purchaseType]
          }}</template>
        </rg-table-column>

        <rg-table-column label="采销单号" prop="id" fix-min>
          <template slot-scope="scope">
            <label
              style="background-color:red;color:white;"
              v-if="scope.row.hcType == '红冲订单'"
              >{{ scope.row.id }}</label
            >
            <label v-else>{{ scope.row.id }}</label>
          </template>
        </rg-table-column>
        <rg-table-column label="供应商/客户" prop="venderShortName" />
        <rg-table-column label="订单状态" prop="status" align="center" fix-min>
          <template slot-scope="scope">{{
            purchaseOrderStatusList[scope.row.status]
          }}</template>
        </rg-table-column>

        <rg-table-column label="结算方式" prop="payType" align="center" fix-min>
          <template slot-scope="scope">{{
            payTypeSel[scope.row.payType]
          }}</template>
        </rg-table-column>

        <rg-table-column
          label="支付状态"
          prop="payStatus"
          align="center"
          fix-min
        >
          <template slot-scope="scope">{{
            payStatusSel[scope.row.payStatus]
          }}</template>
        </rg-table-column>

        <rg-table-column label="商品总额" prop="totalAmount" is-money fix-min />
        
        <rg-table-column label="优惠金额" prop="couponAmount" is-money fix-min />
        
        <rg-table-column label="运费" prop="ownFreight" is-money fix-min />
        
        <rg-table-column label="实付款" prop="actualAmount" is-money fix-min />
        
        <rg-table-column label="运单号" prop="waybillNumber" fix-min  />
        <rg-table-column label="门店名称" prop="shopName" fix-min />
        <rg-table-column label="采销人员" prop="buyer" fix-min  />
        <rg-table-column label="支付时间" prop="payTime" fix-min >
          <template slot-scope="scope">
            <label
              style="font-weight:50;"
              v-show="scope.row.payTime != '1900-01-01 00:00:00' ? true : false"
              >{{ scope.row.payTime }}</label
            >
          </template>
        </rg-table-column>

        <rg-table-column label="支付人" prop="payer" fix-min  />
        <rg-table-column label="创建人" prop="createBy" fix-min  />
        <rg-table-column
          label="创建时间"
          prop="createTime"
          align="center"
          is-datetime
          fix-min
        />
        <rg-table-column label="产品信息" prop="joinText" width="200px" />
        <rg-table-column label="红冲单类型"  align="center">
           <template slot-scope="scope">
             <label style="background-color:red;color:white;" v-if="scope.row.hcType == '红冲订单'">红冲订单,原单:{{scope.row.relationPurchaseId}}</label>
             <label v-else></label>
           </template>
        </rg-table-column>

        <rg-table-column label="操作" fix-min align="center" fixed="right">
          <template slot-scope="scope">
            <el-button
              size="mini"
              type="primary"
              @click="handleEdit(scope.$index, scope.row)"
              >查看详情</el-button
            >
          </template>
        </rg-table-column>
      </template>
    </rg-page>

    <rg-dialog
      :title="(formModel.id ? '编辑采销单：'+formModel.id+'  '+formModel.hcType : '新增采销单') + '   提示：门店采购需要在门店系统中操作'"
      :visible.sync="dialogFormVisible"
      :before-close="cancelForm"
      width="80%"
      max-width="1366px"
      min-width="600px"
      :loading="tableLoading"
      :btnRemove="{
        label: '取消订单',
        show:
          (formModel.status == 2 || formModel.status == 1 || formModel.status == 4) &&
          formModel.hcType != '红冲订单',
        disabled: false,
        click: done => {
          cancelPurchaseOrder('formModel', 3);
        }
      }"
    >
      <template v-slot:content>
        <el-form
          v-if="dialogFormVisible"
          :model="formModel"
          :rules="rules"
          size="mini"
          label-width="150px"
          ref="formModel"
          :inline="true"
          :disabled="formModel.status >= 2"
        >
          <el-form-item label="单据类型" prop="purchaseType">
            <el-select
              v-model="formModel.purchaseType"
              filterable
              :loading="loading"
              style="width:200px"
              placeholder="请选择类型"    
              :disabled="orgType == '1'"        
            >
              <el-option
                v-for="item in purchaseTypeList"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>

          <el-form-item label="供应商/客户" prop="venderId">
            <el-select
              v-model="formModel.venderId"
              filterable
              :loading="loading"
              @change="changeVender()"
              style="width:200px"
              placeholder="请选择供应商/客户" 
              v-if="formModel.purchaseType != '3'"
            >
              <el-option
                v-for="item in supplierList"
                :key="item.id"
                :label="item.venderShortName"
                :value="item.id"
              />
            </el-select>
            <el-select
              v-model="formModel.venderId"
              filterable
              :loading="loading"
              style="width:200px"
              placeholder="请选择公司" 
              v-if="formModel.purchaseType == '3'"
            >
              <el-option
                v-for="item in companyList"
                :key="item.id"
                :label="item.simpleName"
                :value="item.id"
              />
            </el-select>
            <!-- <el-button
              type="primary"
              @click="addVender()"
              size="mini"
              v-if="(formModel.status || 0) < 2"
              >新建供应商</el-button
            > -->
          </el-form-item>

          <el-form-item label="仓库" prop="warehouseId">
            <el-select
              v-model="formModel.warehouseId"
              filterable
              :loading="loading"
              style="width:200px"
              placeholder="请选择仓库" 
            >
              <el-option
                v-for="item in warehouseSel"
                :key="item.id"
                :label="item.simpleName"
                :value="item.id"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="发票" prop="pinvoiceType">
            <el-select
              v-model="formModel.pinvoiceType"
              filterable
              :loading="loading"
              :disabled="disabledText"
              style="width:200px"
            >
              <el-option
                v-for="item in pinvoiceTypeList"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="结算方式" prop="payType">
            <el-select
              v-model="formModel.payType"
              :disabled="disabledText"
              filterable
              :loading="loading"
              style="width:200px"
            >
              <el-option
                v-for="item in payTypeList"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="采销员" prop="buyer">
            <el-select
              v-model="formModel.buyer"
              filterable
              :loading="loading"
              style="width:200px"
            >
              <el-option
                v-for="item in buyerList"
                :key="item.name"
                :label="item.name"
                :value="item.name"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="运单号">
            <el-col>
              <el-input
                type="textarea"
                :rows="1"
                v-model="formModel.waybillNumber"
                autocomplete="off"
                style="width:200px"
                :disabled="true"
              />
            </el-col>
          </el-form-item>
          <el-form-item label="运费">
            <el-input
              type="number"
              step="0.01"
              min="0"
              style="width:200px"
              v-model="formModel.ownFreight"
              autocomplete="off"
              :disabled="true"
              @change="validationsTable()"
            />
            <!-- :disabled="formModel.purchaseType==2" -->
          </el-form-item>
          <el-form-item label="优惠">
            <el-input
              type="number"
              step="0.01"
              min="0"
              style="width:200px"
              v-model="formModel.couponAmount"
              autocomplete="off"
              :disabled="false"
              @change="validationsTable()"
            />
          </el-form-item>
          <el-form-item label="订单状态">
            <el-select
              v-model="formModel.status"
              :disabled="true"
              filterable
              :loading="loading"
              style="width:200px"
            >
              <el-option
              v-for="(name, value) in purchaseOrderStatusList"
              :key="value"
              :value="value"
              :label="name"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="备注说明">
            <el-input
              type="textarea"
              v-model="formModel.remark"
              :rows="1"
              autocomplete="off"
              style="width:200px"
              maxlength="200"
              show-word-limit
            ></el-input>
          </el-form-item>
          <el-form-item label="实付款">
            <el-input
              type="number"
              step="0.01"
              min="0"
              style="width:200px"
              v-model="formModel.actualAmount"
              autocomplete="off"
              :disabled="true"
            />
          </el-form-item>
          <el-form-item label="添加产品" v-if="formModel.status < 2">
            <el-select
              v-model="searchProductCode"
              filterable
              remote
              clearable
              reserve-keyword
              placeholder="请输入商品名称或编码"
              :remote-method="loadProducts"
              :loading="loading"
              style="width:400px"
            >
              <el-option
                v-for="item in productList"
                :key="item.productCode"
                :label="item.productDisplayName"
                :value="item.productCode"
              />
            </el-select>
            <el-button
              type="primary"
              @click="addProduct()"
              size="mini"
              :disabled="formModel.status > 2"
              >添加产品</el-button
            >
            <!-- <el-button
              type="primary"
              @click="addnewProduct()"
              size="mini"
              :disabled="formModel.status == 3"
              >新增产品</el-button
            > -->
          </el-form-item>
          <el-table
            v-loading="tableLoading"
            :data="purchaseOrderProductData"
            border
            style="width: 100%;"
            height="300px"
            size="mini"
            show-summary
            :summary-method="getSummaries"
          >
            <el-table-column
              prop="productCode"
              label="产品编号"
              fixed="left"
              fix-min
            />
            <el-table-column
              prop="productName"
              label="产品名称"
              fixed="left"
            />
            <el-table-column prop="num" label="数量"  width="90" align="right">
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.num"
                  type="number"
                  size="mini"
                  min="1"
                  @blur="calcPrice(scope.row, purchaseOrderProductData)"
                ></el-input>
              </template>
            </el-table-column>
            <el-table-column prop="price" label="单价"  width="90">
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.price"
                  type="number"
                  size="mini"
                  min="1"
                  step="0.01"
                  @blur="calcPrice(scope.row, purchaseOrderProductData)"
                ></el-input>
              </template>
            </el-table-column>
            <el-table-column
              prop="amount"
              label="金额"
              width="90" align="right"
              is-money
            />
<!-- 
            <el-table-column prop="taxPoint" width="90" label="税率">
              <template slot-scope="scope">
                <el-select
                  v-model="scope.row.taxPoint"
                  size="mini"
                  placeholder
                  @change="calcPrice(scope.row, purchaseOrderProductData)"
                >
                  <el-option
                    v-for="value in [
                      0,
                      0.03,
                      0.05,
                      0.06,
                      0.07,
                      0.11,
                      0.13,
                      0.16
                    ]"
                    :key="value"
                    :label="(value * 100.0).toFixed() + '%'"
                    :value="value"
                  />
                </el-select>
              </template>
            </el-table-column> -->

            <el-table-column prop="unit" label="单位" width="90" fix-min />
            <el-table-column label="操作" align="center" width="100">
              <template slot-scope="scope">
                <el-button
                  type="danger"
                  size="mini"
                  @click="deleteRow(scope.$index, purchaseOrderProductData)"
                  :disabled="formModel.status == 3"
                  v-if="formModel.status < 2"
                  >删除</el-button
                >
              </template>
            </el-table-column>
          </el-table>
        </el-form>

        <el-divider content-position="left" v-if="purchaseLogTable.length > 0"
          >操作记录</el-divider
        >
        <el-timeline
          style="margin-left:30px"
          height="200px"
          v-if="purchaseLogTable.length > 0"
        >
          <el-timeline-item
            v-for="(log, index) in purchaseLogTable"
            :key="index"
            :timestamp="log.createTime"
            >{{ log.createBy }}-{{ log.remark }}</el-timeline-item
          >
        </el-timeline>
      </template>
      <template v-slot:footer>
        <template>
          <el-button type="primary"
                     icon="el-icon-check"
                     size="mini"
                     @click="showpurchaseOrderDelivery()"
                     v-if="
              (formModel.status || 0) == 2 && formModel.hcType != '红冲订单'
            ">发货</el-button>
          <!-- <el-button type="primary"
                     icon="el-icon-check"
                     size="mini"
                     v-if="(formModel.status || 0) < 2 && formModel.hcType != '红冲订单'"
                     @click="submitForm('formModel', 1)">保存</el-button> -->
          <el-button type="success"
                     icon="el-icon-check"
                     size="mini"
                     v-if="(formModel.status || 0) < 2 && formModel.hcType != '红冲订单'"
                     @click="submitForm('formModel', 2)">提交</el-button>
          <el-button type="success"
                     icon="el-icon-check"
                     size="mini"
                     v-if="formModel.status == 4 && formModel.hcType != '红冲订单'"
                     @click="instock()">入库
          </el-button>

          <el-button type="primary"
                     icon="el-icon-check"
                     size="mini"
                     v-if="
                (formModel.payStatus < 3) &&
                formModel.hcType != '红冲订单' &&
                formModel.status > 4 && formModel.payType!=3 "
                     @click="payPurchaseOrder()">支付
          </el-button>

          <el-button type="success"
                     icon="el-icon-check"
                     size="mini"
                     v-if="formModel.payStatus == 3 && formModel.hcType != '红冲订单'"
                     @click="viewPayInfo()">查看支付信息</el-button>

          <!--formModel.payType == 3 &&-->
          <el-button type="success"
                     icon="el-icon-check"
                     size="mini"
                     v-if="
                formModel.hcType != '红冲订单' &&
                formModel.status == 6 "
                     @click="showPurchaseBack()">申请退货
          </el-button>

          <el-button type="success"
                     icon="el-icon-check"
                     size="mini"
                     v-if="
              formModel.hcType == '红冲订单' &&
                formModel.status != 3 &&
                formModel.status != 6
            "
                     @click="releaseOccupyStock()">红冲退货</el-button>

          <el-button type="primary"
                     @click="showpurchaseOrderDelivery()"
                     size="mini"
                     v-if="formModel.status >= 6">补填运单号</el-button>
          
          <el-button
                  type="warning"
                  size="mini"
                  @click="showEditPrice()"
                  v-if="formModel.status > 1 && orgType == '0'"
                  >修改价格</el-button
                >
        </template>
      </template>
    </rg-dialog>

    <!-- 填写发货信息页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        :title="deliveryformTitle"
        :visible.sync="deliveryformVisible"
        :beforeClose="deliverycancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            deliverycancel();
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="600px"
        minWidth="500px"
      >
        <template v-slot:content>
          <el-form :model="deliveryformModel" ref="deliveryformModel">
            <el-form-item label="运单号">
              <el-input
                size="mini"
                type="textarea"
                :rows="5"
                placeholder="请输入运单号"
                v-model="deliveryformModel.waybillNumber"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
            <el-form-item label="运费">
              <el-input
                size="mini"
                placeholder="请输入运费"
                v-model="deliveryformModel.ownFreight"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="savePurchaseDelivery()"
            >确定</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 填写发货信息页面 -->

    <!-- 修改价格页面 -->
    <section id="editPrice">
      <rg-dialog
        :title="editPriceFormTitle"
        :visible.sync="editPriceFormVisible"
        :beforeClose="editPricecancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            editPricecancel();
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="600px"
        minWidth="500px"
      >
        <template v-slot:content>
          <el-form :model="editPriceFormModel" ref="editPriceFormModel">
           <el-table
            v-loading="tableLoading"
            :data="purchaseOrderProductData"
            border
            style="width: 100%;"
            height="200px"
            size="mini"
          >
            <el-table-column
              prop="productCode"
              label="产品编号"
              fixed="left"
              fix-min
            />
            <el-table-column
              prop="productName"
              label="产品名称"
              fixed="left"
            />
            <el-table-column prop="purchaseCost" label="单价"
              width="90">
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.purchaseCost"
                  type="number"
                  size="mini"
                  min="1"
                  step="0.01"
                ></el-input>
              </template>
            </el-table-column>
          </el-table>
          </el-form>

        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="modifyPrice('editPriceFormModel')"
            >确定</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 修改价格页面 -->

    <!-- 申请退货页面 -->
    <section id="purchaseBack">
      <rg-dialog
        :title="申请退货"
        :visible.sync="purchaseBackFormVisible"
        :beforeClose="purchaseBackCancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            purchaseBackCancel();
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="600px"
        minWidth="500px"
      >
        <template v-slot:content>
          <el-form :model="purchaseBackFormModel" ref="purchaseBackFormModel">
           <el-table
            v-loading="tableLoading"
            :data="purchaseOrderProductData"
            border
            style="width: 100%;"
            height="300px"
            size="mini"
          >
            <el-table-column
              prop="productCode"
              label="产品编号"
              fixed="left"
              fix-min
            />
            <el-table-column
              prop="productName"
              label="产品名称"
              fixed="left"
            />
            <el-table-column
              prop="num"
              label="采购数量"
              fixed="left"
            />
            <el-table-column
              prop="purchaseCost"
              label="采购价格"
              fixed="left"
            />
            <el-table-column
              prop="backNum"
              label="已退数量"
              fixed="left"
            />
            <el-table-column prop="editBackNum" label="退货数量"
              width="90">
              <template slot-scope="scope">
                <el-input
                  v-model="scope.row.editBackNum"
                  type="number"
                  size="mini"
                  min="1"
                  step="1"
                ></el-input>
              </template>
            </el-table-column>
          </el-table>
          </el-form>

        </template>
        <template v-slot:footer>
          <el-button id="backButton"
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="purchaseBack('purchaseBackFormModel')"
            >确定退货</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 申请退货页面 -->

    <!-- 支付页面 -->
    <section id="addPayInfo">
      <rg-dialog
        :title="payformTitle"
        :visible.sync="payformVisible"
        :beforeClose="paycancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            paycancel();
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="600px"
        minWidth="500px"
      >
        <template v-slot:content>
          <el-form :model="payformModel" ref="payformModel" :inline="true">
            <el-form-item label="金额">
              <el-input
                size="mini"
                v-model="payformModel.amount"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
            <el-form-item label="供应商名称">
              <el-input
                size="mini"
                v-model="payformModel.venderName"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>

            <el-form-item label="开户行">
              <el-input
                size="mini"
                v-model="payformModel.openingBank"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>

            <el-form-item label="银行名称">
              <el-input
                size="mini"
                v-model="payformModel.bankName"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>

            <el-form-item label="银行账户">
              <el-input
                size="mini"
                v-model="payformModel.accountNo"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>

             <el-form-item label="收款户名">
              <el-input
                size="mini"
                v-model="payformModel.receiveBankName"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>

            <el-form-item label="支付方式">
              <el-select
                v-model="payformModel.payMethod"
                filterable
                style="width:200px"
              >
                <el-option
                  v-for="item in payMethodList"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                />
              </el-select>
            </el-form-item>

            <el-form-item label="交易参考号">
              <el-input
                size="mini"
                placeholder="请输入交易号"
                v-model="payformModel.serialNumber"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="savePayInfo()"
            >确定</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 支付页面 -->

    <!-- 查看支付信息 -->
    <section id="viewPayInfo">
      <rg-dialog
        :title="viewpayformTitle"
        :visible.sync="viewpayformVisible"
        :beforeClose="viewpaycancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            viewpaycancel();
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="600px"
        minWidth="500px"
      >
        <template v-slot:content>
          <el-form :model="viewPayModel" :inline="true" ref="viewPayModel">
            <el-form-item label="支付人">
              <el-input
                size="mini"
                v-model="viewPayModel.payer"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
            <el-form-item label="支付时间">
              <el-input
                size="mini"
                v-model="viewPayModel.payTime"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>

            <el-form-item label="支付方式">
              <el-input
                size="mini"
                v-model="viewPayModel.payMethod"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
            <el-form-item label="交易参考号">
              <el-input
                size="mini"
                v-model="viewPayModel.serialNumber"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
          </el-form>
        </template>
      </rg-dialog>
    </section>
    <!-- 查看支付信息 -->
  </main>
</template>
<script>
import { shoppurchaseManageSvc } from "@/api/shoppurchase/shoppurchasemanage";
export default {
  name: "shopproductmanage",
  data() {
    return {
      orgType: undefined,
      tableData: [],
      tableLoading: false,

      //采购单状态
      purchaseOrderStatusList: {
        1: "新建",
        2: "待发货",
        3: "已取消",
        4: "已发货",
        5: "部分收货",
        6: "已收货"
      },

      payTypeSel: {
        1: "现金",
        2: "账期",
        3: "月结"
      },

      payStatusSel: {
        1: "未付款",
        2: "部分付款",
        3: "已付款"
      },

      purchaseTypeSel: {
        1: "公司采购",
        2: "门店采购",
        3: "内销单",
        4: "外销单"
      },
     
      //付款状态
      payStatusList: [
        // {
        //   value: "",
        //   label: "全部"
        // },       
        {
          value: 1,
          label: "未付款"
        },
        {
          value: 2,
          label: "部分付款"
        },
        {
          value: 3,
          label: "已付款"
        }
      ],
      //采购类型
      purchaseTypeList: [
        // {
        //   value: 1,
        //   label: "产品内采"
        // }
        // ,
        {
          value: 2,
          label: "产品外采"
        }
      ],

      payformModel: {
        id: undefined,
        amount: undefined,
        venderName: undefined,
        bankName: undefined,
        accountNo: undefined,
        openingBank: undefined,
        payMethod: undefined,
        serialNumber: undefined,
        receiveBankName:undefined
      },
      payformModelInit: {
        id: undefined,
        amount: undefined,
        venderName: undefined,
        bankName: undefined,
        accountNo: undefined,
        payMethod: undefined,
        serialNumber: undefined,
        receiveBankName:undefined
      },
      purchaseOrderRequest: {
        poId: undefined
      },

      disabledFlag: false,
      payformVisible: false,
      payformTitle: undefined,
      payMethodList: [
        {
          value: "支付宝",
          label: "支付宝"
        },
        {
          value: "微信",
          label: "微信"
        },
        {
          value: "银联",
          label: "银联"
        }
      ],
      //发票类型
      pinvoiceTypeList: [
        {
          value: 0,
          label: "无需发票"
        },
        {
          value: 1,
          label: "普通发票"
        },
        {
          value: 2,
          label: "增值税发票"
        }
      ],
      disabledText: false,
      //结算方式
      payTypeList: [
        {
          value: 1,
          label: "现金"
        },
        {
          value: 3,
          label: "月结"
        }
      ],
      //单据类型1 公司采购 2 门店采购，3内销单，4外销单
      purchaseTypeList: [
        {
          value: 1,
          label: "公司采购"
        },
        {
          value: 2,
          label: "门店采购"
        },
        {
          value: 3,
          label: "内销单"
        },
        {
          value: 4,
          label: "外销单"
        }
      ],
      dialogFormVisible: false,
      condition: {
        key: undefined,
        pageIndex: 1,
        pageSize: 50,
        venderId: undefined,
        status: undefined,
        payStatus: undefined,
        purchaseOrderId: undefined,
        purchaseOrderStartTime: undefined,
        PurchaseOrderEenTime: undefined,
        purchaseDates: [],
        payType: undefined, //这个有问题
        shopId: undefined,
        purchaseType: undefined,
        warehouseId: 0,
        saleCompanyId: undefined,
        
      },
      totalCount: 0,

      //修改价格弹框
      editPriceFormTitle: undefined,
      editPriceFormVisible: false,
      editPriceFormModel: {
        productCode: undefined,
        purchasePrice: undefined,
        id: undefined,
        poId: undefined
      },
      editPriceFormModelInit: {
        productCode: undefined,
        purchasePrice: undefined,
        id: undefined,
        poId: undefined
      },

      //申请退货弹框
      purchaseBackFormTitle: undefined,
      purchaseBackFormVisible: false,
      purchaseBackFormModel: {
        productCode: undefined,
        backNum: undefined,
        id: undefined,
        poId: undefined
      },
      purchaseBackFormModelInit: {
        productCode: undefined,
        backNum: undefined,
        id: undefined,
        poId: undefined
      },

      //发货弹框
      deliveryformTitle: undefined,
      deliveryformVisible: false,
      deliveryformModel: {
        waybillNumber: undefined,
        ownFreight: undefined,
        id: undefined
      },
      deliveryformModelInit: {
        waybillNumber: undefined,
        ownFreight: undefined,
        id: undefined
      },
      isRefresh: false,
      //发货弹框
      buyerList: [],

      purchaseLogTable: [],
      deleteCondition: {
        id: undefined
      },

      rules: {
        warehouseId: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ],
        shopId: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ],
        purchaseType: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ],
        venderId: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ],
        pinvoiceType: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ],
        payType: [
          {
            required: true,
            message: "必填",
            trigger: "blur"
          }
        ],
        buyer: [
          {
            required: true,
            message: "请输入采购员",
            trigger: "blur"
          }
        ]
      },

      viewpayformTitle: undefined,
      viewpayformVisible: false,
      viewPayModel: {
        payMethod: undefined,
        serialNumber: undefined,

        payer: undefined,
        payTime: undefined
      },

      formModelInit: {
        purchaseType: undefined, // 产品内采
        venderId: undefined, //供应商
        venderShortName: "",
        pinvoiceType: 0, //发票类型
        payType: 1, //结算方式
        buyer: "",
        waybillNumber: "",
        ownFreight: 0,
        remark: "",
        totalAmount: 0,
        status: 1,
        payStatus: undefined,
        shopName: undefined,
        couponAmount: 0,
        actualAmount: 0,
        warehouseId: undefined
      },
      purchaseForm: {
        id: undefined
      },

      formModel: {
        purchaseType: undefined, // 产品内采
        venderId: undefined, //供应商
        venderShortName: "",
        pinvoiceType: 0, //发票类型
        payType: 1, //结算方式
        buyer: "",
        waybillNumber: "",
        ownFreight: 0,
        remark: "",
        totalAmount: 0,
        status: 1,
        payStatus: undefined,
        shopName: undefined,
        couponAmount: 0,
        actualAmount: 0,
        warehouseId: undefined
      },
      IsEdit: false,
      loading: false,
      supplierList: [], //供应商列表
      purchaseOrderProductData: [], //采购商品列表,
      productList: [],
      shopSel: [],
      warehouseSel: [],
      companyList: [],
      searchProductCode: undefined,
      purchaseProductModel: {
        productCode: undefined,
        productName: undefined,
        purchaseCost: undefined,
        num: undefined,
        price: undefined,
        amount: undefined,
        salePrice: undefined,
        totalCost: undefined,
        taxPoint: undefined,
        purchasePrice: undefined,
        totalPrice: undefined,
        unit: undefined
      },
      purchaseProductModelInit: {
        productCode: undefined,
        productName: undefined,
        purchaseCost: undefined,
        num: undefined,
        price: undefined,
        amount: undefined,
        salePrice: undefined,
        totalCost: undefined,
        taxPoint: undefined,
        purchasePrice: undefined,
        totalPrice: undefined,
        unit: undefined
      },
      getVenderRequest: {
        id: undefined
      },
      shopSelCondition: {
        simpleName: undefined
      },
      loading: false,
      updateDeliveryCodeModel: {
        waybillNumber: undefined,
        id: undefined
      }
    };
  },
  created() {
    this.getOrgType();
    //获取供应商信息
    this.loadVenderList();
    this.getShopinfo();
    this.getCompanyList();
    this.getwarehouses();
    this.search(true); //页面首次加载时默认搜索
  },
  methods: {
    getOrgType() {
      shoppurchaseManageSvc
            .getOrgType()
            .then(
              res => {
                this.orgType = res.data;
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
    },
    getShopinfo(query) {
     
        this.loading = true;
        setTimeout(() => {
          this.loading = false;
          debugger;
          this.shopSelCondition.simpleName = query;
          shoppurchaseManageSvc
            .GetCompanyShopList({pageSize : 100})
            .then(
              res => {
                this.shopSel = res.data.items;
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        }, 200);
      
    },
    getwarehouses() {
      shoppurchaseManageSvc
        .GetShopWareHouseList({queryType : -1})
        .then(
          res => {
            this.warehouseSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    getCompanyList() {
     
        setTimeout(() => {
          this.loading = false;
          debugger;
          shoppurchaseManageSvc
            .GetPartnershipCompanyListAsync({pageSize : 100})
            .then(
              res => {
                this.companyList = res.data.items;
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        }, 200);
      
    },
    purchaseBack2() {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          debugger;
          this.purchaseOrderRequest.poId = this.purchaseForm.id;
          //保存退货信息
          shoppurchaseManageSvc
            .purchaseReturn(this.purchaseOrderRequest)
            .then(
              res => {
                this.$message({
                  message: res.message || "操作成功",
                  type: "success"
                });
                //this.formModel.payStatus = 3; //更新付款状态
                //刷新页面
                this.isRefresh = true;
                // this.paycancel();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(error => {
          console.log(error);
        });
    },

    viewPayInfo() {
      this.viewpayformTitle = "";
      this.viewpayformVisible = true;

      this.purchaseOrderRequest.poId = this.purchaseForm.id;

      shoppurchaseManageSvc
        .getPurchasePayInfo(this.purchaseOrderRequest)
        .then(
          res => {
            this.viewPayModel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    viewpaycancel() {
      this.viewpayformVisible = false;
    },

    savePayInfo() {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.payformModel.id = this.purchaseForm.id;
          //保存发货信息
          shoppurchaseManageSvc
            .savePayInfo(this.payformModel)
            .then(
              res => {
                this.$message({
                  message: res.message || "操作成功",
                  type: "success"
                });
                this.formModel.payStatus = 3; //更新付款状态
                //刷新页面
                this.isRefresh = true;
                this.paycancel();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(error => {
          console.log(error);
        });
    },
    releaseOccupyStock() {
      this.$confirm("确定操作退货出库吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.purchaseOrderRequest.poId = this.purchaseForm.id;
          //保存发货信息
          shoppurchaseManageSvc
            .releasePurchaseOccupyStock(this.purchaseOrderRequest)
            .then(
              res => {
                this.$message({
                  message: res.message || "操作成功",
                  type: "success"
                });
                //this.formModel.payStatus = 3; //更新付款状态
                //刷新页面
                this.isRefresh = true;
                this.cancelForm();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(error => {
          console.log(error);
        });
    },
    payPurchaseOrder() {
      this.payformTitle = "付款信息";
      this.payformVisible = true;

      if (this.purchaseForm.id == undefined || this.purchaseForm.id == null) {
        this.$message.error("采购单为空!");
        return false;
      }

      this.purchaseOrderRequest.poId = this.purchaseForm.id;
      shoppurchaseManageSvc
        .getPurchaseOrderPayInfo(this.purchaseOrderRequest)
        .then(
          res => {
            debugger;
            this.payformModel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    paycancel() {
      this.payformVisible = false;
      this.payformModel = JSON.parse(JSON.stringify(this.payformModelInit));
    },
    changeVender() {
      this.getVenderRequest.id = this.formModel.venderId;
      shoppurchaseManageSvc
        .getVender(this.getVenderRequest)
        .then(
          res => {
            debugger;
            var data = res.data;
            //月结  发票和结算方式为默认值
            if (data.settlementMethod == 3) {
              debugger;
              //需禁用
              this.disabledText = true;
              //结算方式
              this.formModel.pinvoiceType = data.invoiceType;
              //付款类型
              this.formModel.payType = data.settlementMethod;
            } else {
              this.disabledText = false;
              //结算方式
              this.formModel.pinvoiceType = undefined;
              //付款类型
              this.formModel.payType = undefined;
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    addnewProduct() {
      let routeUrl = this.$router.resolve({
        name: "shopproduct"
      });
      window.open(routeUrl.href, "_blank");
    },
    addVender() {
      let routeUrl = this.$router.resolve({
        name: "vender"
      });
      window.open(routeUrl.href, "_blank");
    },
    //获取门店的员工
    getShopEmployee() {
      shoppurchaseManageSvc
        .getEmployes()
        .then(
          res => {
            this.buyerList = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    instock() {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.tableLoading = true;
          //保存发货信息
          shoppurchaseManageSvc
            .purchaseInStock(this.purchaseForm)
            .then(
              res => {
                this.$message({
                  message: res.message || "操作成功",
                  type: "success"
                });
                //刷新页面
                this.isRefresh = true;
                this.cancelForm();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally(() => {
              this.tableLoading = false;
            });
        })
        .catch(error => {
          console.log(error);
        });
    },
    deliverycancel() {
      this.deliveryformVisible = false;
      this.deliveryformModel = JSON.parse(
        JSON.stringify(this.deliveryformModelInit)
      );
    },

    showpurchaseOrderDelivery() {
      this.deliveryformVisible = true;
      this.deliveryformModel.id = this.formModel.id;
    },
    savePurchaseDelivery() {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          if (this.formModel.status == 6) {
            if (
              this.deliveryformModel.waybillNumber != undefined &&
              this.deliveryformModel.waybillNumber != ""
            ) {
            } else {
              this.$message.warning("运单号不能为空!");
              return false;
            }
            //保存发货信息
            shoppurchaseManageSvc
              .updatePurchaseDeliveryCode(this.deliveryformModel)
              .then(
                res => {
                  this.$message({
                    message: res.message || "保存成功",
                    type: "success"
                  });
                  //更新model的记录
                  this.formModel.ownFreight = this.deliveryformModel.ownFreight;
                  this.formModel.waybillNumber = this.deliveryformModel.waybillNumber;
                  //this.formModel.status = 4; //设置采购单为 已发货
                  this.validationsTable();
                  this.deliverycancel();

                  this.isRefresh = true;
                },
                error => {
                  console.log(error);
                }
              )
              .catch(() => {});
          } else {
            //保存发货信息
            shoppurchaseManageSvc
              .updatePurchaseOrderDeliveryInfo(this.deliveryformModel)
              .then(
                res => {
                  this.$message({
                    message: res.message || "保存成功",
                    type: "success"
                  });
                  //更新model的记录
                  this.formModel.ownFreight = this.deliveryformModel.ownFreight;
                  this.formModel.waybillNumber = this.deliveryformModel.waybillNumber;
                  this.formModel.status = 4; //设置采购单为 已发货
                  this.validationsTable();
                  this.deliverycancel();

                  this.isRefresh = true;
                },
                error => {
                  console.log(error);
                }
              )
              .catch(() => {});
          }
        })
        .catch(error => {
          console.log(error);
        });
    },

    //修改价格
    editPricecancel() {
      this.editPriceFormVisible = false;
      this.editPriceFormModel = JSON.parse(
        JSON.stringify(this.editPriceFormModelInit)
      );
    },

    showEditPrice() {
      this.editPriceFormTitle = "修改价格"
      this.editPriceFormVisible = true;
      (this.purchaseOrderProductData || []).map(function(t) {
        t.oldPurchaseCost = t.purchaseCost;
      });
    },
    modifyPrice(formName) {
      this.$refs[formName].validate(valid => {
        if (this.purchaseOrderProductData.length <= 0) {
          this.$message({
            message: "请添加产品！",
            type: "warning"
          });
          return;
        }
      });

      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {         
            //修改价格
            shoppurchaseManageSvc
              .updatePurchasePrice(this.purchaseOrderProductData)
              .then(
                res => {
                  this.$message({
                    message: res.message || "保存成功",
                    type: "success"
                  });
                  this.editPricecancel();

                  this.isRefresh = true;
                },
                error => {
                  console.log(error);
                }
              )
              .catch(() => {});
         
        })
        .catch(error => {
          console.log(error);
        });
    },

    //申请退货
    purchaseBackCancel() {
      this.purchaseBackFormVisible = false;
      this.purchaseBackFormModel = JSON.parse(
        JSON.stringify(this.purchaseBackFormModelInit)
      );
    },

    showPurchaseBack() {
      this.purchaseBackFormTitle = "申请退货"
      this.purchaseBackFormVisible = true;
      (this.purchaseOrderProductData || []).map(function(t) {
        t.editBackNum = 0;
      });
    },
    purchaseBack(formName) {
      this.$refs[formName].validate(valid => {
        if (this.purchaseOrderProductData.length <= 0) {
          this.$message({
            message: "请添加产品！",
            type: "warning"
          });
          return;
        }
      });
      
        var isExist = false;
        (this.purchaseOrderProductData || []).map(function(t) {         
          if ((t.editBackNum > 0) && (t.editBackNum <= (t.num - t.backNum))) {
            isExist = true;
          }
        });
        if (!isExist) {   
              this.$message({
                message: "请输入正确的退货数量！",
                type: "warning"
              });
              return;
        }

      this.$confirm("确定操作退货吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {    
          document.getElementById("backButton").disabledFlag = false;    
        
          var data = {
            PurchaseOrder: this.formModel,
            PurchaseOrderProducts: this.purchaseOrderProductData,
            IsEdit: this.formModel.id > 0
          };
          //保存退货信息
          shoppurchaseManageSvc
            .purchaseReturnPart(data)
            .then(
              res => {
                this.$message({
                  message: res.message || "操作成功",
                  type: "success"
                });
                //this.formModel.payStatus = 3; //更新付款状态
                //刷新页面
                this.isRefresh = true;
                this.purchaseBackCancel();
                this.cancelForm();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
         
        })
        .catch(error => {
          console.log(error);
        });
    },

    //获取所有的供应商信息
    loadVenderList() {
      shoppurchaseManageSvc
        .getSupplierList()
        .then(
          res => {
            var resData = res.data;
            this.supplierList = resData;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //查询采购单数据
    getPageData(flag) {
      debugger;
      this.tableLoading = true;
      let p = Object.assign({}, this.condition);
      if (
        this.condition.purchaseDates &&
        this.condition.purchaseDates.length > 0
      ) {
        p.purchaseOrderStartTime = this.condition.purchaseDates[0];
        p.PurchaseOrderEenTime = this.condition.purchaseDates[1];
      }
      p.status = p.status || 0;
      p.payStatus = p.payStatus || 0;

      p.payType = p.payType || 0;
      p.purchaseType = p.purchaseType || 0;

      shoppurchaseManageSvc
        .searchPurchaseOrder(p)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalCount = data.totalItems;
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
    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPageData(flag);
    },
    // 分页
    handleSizeChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.currentPage = p.currentPage;

      this.getPageData(false);
    },
    handleAdd() {
      this.IsEdit = false;
      this.attributeArray = [];
      if (this.orgType == '0')
      {
        this.formModel.purchaseType = 1;
      }
      else
      {
        this.formModel.purchaseType = 2;
      }

      this.getShopEmployee();
      this.dialogFormVisible = true;
    },
    handleEdit(index, row) {
      this.getShopEmployee();
      //this.getOrgType();
      this.tableLoading = true;
      var purchaseOrderId = row.id;
      this.purchaseForm.id = row.id;
      shoppurchaseManageSvc
        .getPurchaseOrderDetail({ purchaseOrderId: purchaseOrderId })
        .then(
          res => {
            debugger;
            var data = res.data;
            this.formModel = data.purchaseOrder;
            this.purchaseOrderProductData = data.purchaseOrderProducts || [];
            this.dialogFormVisible = true;
            this.purchaseLogTable = data.logs || [];

            if (this.formModel.status == 6) {
              this.disabledFlag = false;
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
    submitForm(formName, status) {
      this.$refs[formName].validate(valid => {
        if (this.purchaseOrderProductData.length <= 0) {
          this.$message({
            message: "请添加产品！",
            type: "warning"
          });
          return;
        }
        if (!this.validationsTable()) {
          this.$message({
            message: "请核对价格是否填写！",
            type: "warning"
          });
          return;
        }

        if (valid) {
          const loading = this.$loading({
            lock: true,
            text: "Loading",
            spinner: "el-icon-loading",
            background: "rgba(0, 0, 0, 0.7)"
          });
          this.formModel.status = status;
          var data = {
            PurchaseOrder: this.formModel,
            PurchaseOrderProducts: this.purchaseOrderProductData,
            IsEdit: this.formModel.id > 0
          };
          shoppurchaseManageSvc
            .savePurchaseOrder(data)
            .then(
              res => {
                var data = res.data;
                if (data) {
                  // this.$message({
                  //   message: "保存成功",
                  //   type: "success"
                  // });
                  this.disabledText = false;
                  this.formModel = this.formModelInit;
                  this.productList = [];
                  this.purchaseOrderProductData = [];
                  this.dialogFormVisible = false;
                  this.purchaseLogTable = [];
                  this.purchaseProductModel = JSON.parse(
                    JSON.stringify(this.purchaseProductModelInit)
                  );
                  this.search(true);
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
        }
      });
    },

    cancelPurchaseOrder(formName, status) {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.$refs[formName].validate(valid => {
            const loading = this.$loading({
              lock: true,
              text: "Loading",
              spinner: "el-icon-loading",
              background: "rgba(0, 0, 0, 0.7)"
            });

            this.deleteCondition.id = this.formModel.id;
            // this.formModel.status = status;
            // var data = {
            //   PurchaseOrder: this.formModel,
            //   PurchaseOrderProducts: this.purchaseOrderProductData,
            //   IsEdit: this.formModel.id > 0
            // };
            shoppurchaseManageSvc
              .deletePurchaseOrder(this.deleteCondition)
              .then(
                res => {
                  this.deleteCondition.id = undefined;

                  this.formModel = this.formModelInit;
                  this.productList = [];
                  this.purchaseOrderProductData = [];
                  this.dialogFormVisible = false;

                  this.purchaseProductModel = JSON.parse(
                    JSON.stringify(this.purchaseProductModelInit)
                  );
                  this.search(true);

                  // setTimeout(function() {
                  //   loading.close();
                  // }, 1500);
                },
                error => {
                  console.log(error);
                }
              )
              .catch(() => {})
              .finally(() => {
                loading.close();
              });
          });
        })
        .catch(error => {
          console.log(error);
        });
    },

    cancelForm(formName) {
      this.dialogFormVisible = false;
      this.formModel = this.formModelInit;
      this.purchaseOrderProductData = [];
      this.productList = [];
      this.purchaseProductModel = JSON.parse(
        JSON.stringify(this.purchaseProductModelInit)
      );

      this.purchaseLogTable = [];

      //填过发货信息的采购单
      if (this.isRefresh) {
        this.isRefresh = false;
        //重新加载页面的记录
        this.search(false);
      }
      this.purchaseForm.id = undefined;
      this.disabledText = false;
    },
    //查询商品
    loadProducts(query) {
      if (
        this.formModel.purchaseType == "" ||
        this.formModel.purchaseType == undefined
      ) {
        this.$message({
          message: "请选择外采类型!",
          type: "warning"
        });
        return;
      }
      if (
        this.formModel.purchaseType == "2" &&
        this.orgType == "0"
      ) {
        this.$message({
          message: "门店外采请到门店系统中操作！",
          type: "warning"
        });
        return;
      }
      //默认查询外采商品
      this.productList = [];
      var productCondition = {
        purchaseType: this.formModel.purchaseType,
        key: query,
        pageIndex: 1,
        pageSize: 40
      };
      shoppurchaseManageSvc
        .searchProduct(productCondition)
        .then(
          res => {
            this.productList = (res.data.items || []).map(function(t) {
              return {
                productCode: t.productCode,
                productDisplayName: t.productCode + "|" + t.productName,
                productName: t.productName,
                salesPrice: t.salesPrice,
                unit: t.unit
              };
            });
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    formatDate(date, fmt) {
      if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(
          RegExp.$1,
          (date.getFullYear() + "").substr(4 - RegExp.$1.length)
        );
      }
      let o = {
        "M+": date.getMonth() + 1,
        "d+": date.getDate(),
        "h+": date.getHours(),
        "m+": date.getMinutes(),
        "s+": date.getSeconds()
      };
      for (let k in o) {
        if (new RegExp(`(${k})`).test(fmt)) {
          let str = o[k] + "";
          fmt = fmt.replace(
            RegExp.$1,
            RegExp.$1.length === 1 ? str : ("00" + str).substr(str.length)
          );
        }
      }
      return fmt;
    },
    getRandomInt(min, max) {
      return Math.floor(Math.random() * (max - min + 1)) + min;
    },

    exportData() {
      var productCondition = {
        purchaseType: 2,
        key: "",
        pageIndex: 1,
        pageSize: 40
      };
      shoppurchaseManageSvc
        .exportData(productCondition)
        .then(
          res => {
            debugger;

            let blob = new Blob([res], { type: res.type });
            if (window.navigator && window.navigator.msSaveOrOpenBlob) {
              window.navigator.msSaveOrOpenBlob(
                res,
                `用户列表_${this.formatDate(new Date(), "yyyyMMddhhmmss") +
                  this.getRandomInt(1000, 9999)}.xlsx`
              );
            } else {
              let downloadElement = document.createElement("a");
              let href = window.URL.createObjectURL(blob); //创建下载的链接
              downloadElement.href = href;
              downloadElement.download =
                "用户列表_" +
                this.formatDate(new Date(), "yyyyMMddhhmmss") +
                this.getRandomInt(1000, 9999) +
                ".xlsx"; //下载后文件名
              document.body.appendChild(downloadElement);
              downloadElement.click(); //点击下载
              document.body.removeChild(downloadElement); //下载完成移除元素
              window.URL.revokeObjectURL(href); //释放blob对象
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //添加商品
    addProduct() {
      var productCode = this.searchProductCode;
      var isExist = this.purchaseOrderProductData.findIndex(
        t => t.productCode == productCode
      );
      if (isExist >= 0) {
        return;
      }
      // var product = this.purchaseOrderProductData.find(
      //   t => t.purchaseCost == 0
      // );
      // if (product) {
      //   this.$message({
      //     message: "请核对采购价格是否填写！",
      //     type: "warning"
      //   });
      //   return;
      // }
      var product = this.productList.find(t => t.productCode == productCode);
      if (product) {
        // this.purchaseProductModel.productCode = product.productCode;
        // this.purchaseProductModel.productName = product.productName;

        // this.purchaseOrderProductData.push(this.purchaseProductModel);

        // this.purchaseProductModel = JSON.parse(
        //   JSON.stringify(this.purchaseProductModelInit)
        // );

        this.purchaseOrderProductData.push({
          productCode: product.productCode,
          productName: product.productName,
          purchaseCost: 0,
          num: 1,
          price: 0,
          amount: 0,
          salePrice: product.salesPrice,
          totalCost: 0,
          taxPoint: 0,
          purchasePrice: 0,
          totalPrice: 0,
          unit: product.unit
        });
      }
      this.searchProductCode = "";
      this.productList = [];
    },
    //计算采购价
    calcPrice(currentRow, allRows) {
      if (!currentRow.num) {
        currentRow.num = 1;
      }
      if (!currentRow.price) {
        currentRow.price = 0;
      }
      //总金额(含税)
      currentRow.amount = currentRow.num * currentRow.price;
      //计算税率价格
      // var taxRate =
      //   1 - (currentRow.taxPoint != undefined ? currentRow.taxPoint : 0);
      //单价(不含税)
      // currentRow.purchasePrice = Number(
      //   currentRow.purchaseCost * taxRate
      // ).toFixed(2);
      //总额(不含税)
      // currentRow.totalPrice = Number(
      //   currentRow.purchasePrice * currentRow.num
      // ).toFixed(2);
      this.validationsTable();
    },
    //删除行
    deleteRow(index, rows) {
      rows.splice(index, 1);
    },
    //指定列求和
    getSummaries(param) {
      const { columns, data } = param;
      const sums = [];
      columns.forEach((column, index) => {
        if (index === 0) {
          sums[index] = "合计";
          return;
        }
        const values = data.map(item => Number(item[column.property]));
        if (column.property == "totalCost" || column.property == "totalPrice" 
            || column.property == "num" || column.property == "amount") {
          sums[index] = values.reduce((prev, curr) => {
            const value = Number(curr);
            if (!isNaN(value)) {
              return prev + curr;
            } else {
              return prev;
            }
          }, 0);
          sums[index];
        }
      });
      return sums;
    },
    //验证表格数据
    validationsTable() {
      var amount = 0;
      var isExist = false;
      (this.purchaseOrderProductData || []).map(function(t) {
        if (t.price == 0) {
          isExist = true;
        }
        amount += t.amount;
      });
      if (isExist) {
        return false;
      }
      this.formModel.totalAmount = amount;
      this.formModel.actualAmount = amount - Number(this.formModel.couponAmount) + Number(this.formModel.ownFreight);
      return true;
    }
  }
};
</script>
