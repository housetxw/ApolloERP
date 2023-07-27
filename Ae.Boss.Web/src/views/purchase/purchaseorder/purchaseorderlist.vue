<template>
  <main class="container">
    <!-- 首页 -->
    <section>
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
        :defaultCollapse="false"
        :getRowKeys="getRowKeys"
        :expands="expands"
        @expand-change="expandSelect"
      >
        <template v-slot:condition>
          <el-form-item>
            <el-input
              v-model="condition.poId"
              @change="updatePoId()"
              clearable
              placeholder="请输入采购总单号"
              size="mini"
            />
          </el-form-item>
          <el-form-item>
            <el-input
              v-model="condition.id"
              clearable
              placeholder="请输入采购单号"
              @change="updateId()"
              size="mini"
            />
          </el-form-item>
          <el-form-item>
            <el-input
              v-model="condition.productId"
              size="mini"
              clearable
              placeholder="请输入产品名称或编码"
            />
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.venderId"
              size="mini"
              clearable
              filterable
              placeholder="请选择供应商"
            >
              <el-option
                v-for="item in venderSel"
                :key="item.id"
                :label="item.venderShortName"
                :value="item.id"
                size="mini"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.warehouseId"
              size="mini"
              clearable
              filterable
              placeholder="请选择目标仓库"
            >
              <el-option
                v-for="item in warehouseSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
                size="mini"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.purchaseMode"
              size="mini"
              clearable
              filterable
              placeholder="请选择采购方式"
            >
              <el-option
                v-for="item in purchaseModeSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.status"
              size="mini"
              clearable
              filterable
              placeholder="请选择采购状态"
            >
              <el-option
                v-for="item in statusSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>

          <el-form-item>
            <el-select
              v-model="condition.payStatus"
              size="mini"
              clearable
              filterable
              placeholder="请选择付款状态"
            >
              <el-option
                v-for="item in payStatusSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-select
              v-model="condition.shipmentType"
              size="mini"
              clearable
              filterable
              placeholder="请选择提货方式"
            >
              <el-option
                v-for="item in shipmentTypeSel"
                :key="item.key"
                :label="item.value"
                :value="item.key"
                size="mini"
              ></el-option>
            </el-select>
          </el-form-item>
        </template>
        <template v-slot:header_btn>
          <el-button
            type="primary"
            size="mini"
            icon="el-icon-plus"
            @click="showCreate(false, null, 'unBase')"
            >新增</el-button
          >
        </template>
        <!-- <el-input v-model="condition.name" clearable class="el-input el-input--suffix" placeholder="请输入系统名称"></el-input> -->
        <!-- <el-button
            type="primary"
            size="small"
            style="margin-left:30px;"
            @click="search(true)"
            icon="el-icon-search"
          >搜索</el-button>
        <el-button type="primary" size="small" @click="showCreate(false, null, 'unBase')">新增</el-button>-->
        <template v-slot:tb_cols>
          <el-table-column type="expand">
            <template slot-scope="scope">
              <el-table
                border
                :data="scope.row.products"
                :cell-style="cellStyle"
                style="width: 100%"
              >
                <el-table-column
                  label="采购单号"
                  prop="id"
                  align="center"
                ></el-table-column>
                <el-table-column
                  label="状态"
                  prop="status"
                  align="center"
                ></el-table-column>
                <el-table-column
                  label="产品名称"
                  prop="productName"
                  align="center"
                  width="100px"
                ></el-table-column>
                <el-table-column
                  label="产品编号"
                  prop="productId"
                  align="center"
                ></el-table-column>

                <el-table-column
                  label="采购数量"
                  width="90px"
                  prop="num"
                  align="center"
                ></el-table-column>
                <el-table-column
                  label="入库数量"
                  width="85px"
                  prop="instockNum"
                  align="center"
                >
                </el-table-column>
                <el-table-column
                  label="采购单价"
                  width="85px"
                  prop="purchasePrice"
                  align="center"
                ></el-table-column>

                <el-table-column
                  label="采购总价"
                  width="85px"
                  prop="totalPrice"
                  align="center"
                ></el-table-column>
                <el-table-column
                  label="入库总价"
                  width="85px"
                  prop="instockTotalPrice"
                  align="center"
                ></el-table-column>
                <el-table-column
                  label="返利"
                  width="50px"
                  prop="rebate1"
                  align="center"
                >
                </el-table-column>

                <el-table-column
                  label="成本单价"
                  width="80px"
                  prop="costPrice"
                  align="center"
                ></el-table-column>
                <el-table-column
                  label="支付状态"
                  width="80px"
                  prop="payStatus"
                  align="center"
                ></el-table-column>
                <el-table-column
                  label="备注"
                  prop="remark"
                  align="center"
                ></el-table-column>

                <el-table-column
                  label="操作"
                  width="180"
                  fixed="right"
                  align="center"
                >
                  <!-- <el-table-column label="是否禁用" prop="isDeleted">
              <template slot-scope="scope">
                <el-tag
                  :type="scope.row.isDeleted === false ? '' : 'danger'"
                >{{scope.row.isDeleted === false ? '否' : '是'}}</el-tag>
              </template>
                  </el-table-column>-->

                  <template slot-scope="scope">
                    <el-button
                      type="primary"
                      size="mini"
                      icon="el-icon-edit"
                      @click="editPurchaseOrder(scope.row)"
                      >编辑</el-button
                    >

                    <!-- 暂时不做 需要对总单下的多个子单做状态判断 比较复杂 -->
                    <el-button
                      size="mini"
                      type="danger"
                      icon="el-icon-delete"
                      :disabled="scope.row.isDeleted == true"
                      @click="deletePurchaseOrder(scope.row)"
                      >删除</el-button
                    >

                    <el-button
                      size="mini"
                      type="text"
                      @click="viewpurchasehistory(scope.row)"
                      >操作历史</el-button
                    >
                  </template>
                </el-table-column>
              </el-table>
            </template>
          </el-table-column>
          <el-table-column
            label="订单号"
            prop="poId"
            width="80px;"
            align="center"
          ></el-table-column>
          <el-table-column
            label="仓库名称"
            prop="warehouseName"
            align="center"
          ></el-table-column>
          <el-table-column
            label="供应商"
            prop="venderShortName"
            align="center"
          ></el-table-column>
          <el-table-column
            label="采购方式"
            width="100px"
            prop="purchaseMode"
            align="center"
          ></el-table-column>

          <el-table-column
            label="采购总额"
            prop="purchaseTotalPrice"
            align="center"
          ></el-table-column>
          <el-table-column
            label="采购总数"
            width="100px"
            prop="purchaseTotalNum"
            align="center"
          ></el-table-column>
          <el-table-column
            label="入库总额"
            width="100px"
            prop="inStockTotalPrice"
            align="center"
          ></el-table-column>
          <el-table-column
            width="100px"
            label="入库数"
            prop="inStockTotalNum"
            align="center"
          ></el-table-column>
          <el-table-column width="100px" label="采购单类型" align="center">
            <template slot-scope="scope">
              <label
                style="background-color:red;color:white;"
                v-if="scope.row.hcType == '退货单'"
                >退货单</label
              >
              <label v-else>采购单</label>
            </template>
          </el-table-column>
          <el-table-column
            label="采购人"
            prop="createBy"
            align="center"
          ></el-table-column>
          <el-table-column
            label="创建时间"
            prop="createTime"
            align="center"
          ></el-table-column>
          <el-table-column
            label="操作"
            width="200"
            fixed="right"
            align="center"
          >
            <template slot-scope="scope" align="center">
              <!-- 暂时不做 需要对总单下的多个子单做状态判断 比较复杂  -->
              <el-button
                size="mini"
                type="primary"
                :disabled="scope.row.isDelivery == true"
                @click="sendOrder(scope.row)"
                v-if="scope.row.hcType != '退货单'"
                >发货</el-button
              >
              <el-button
                size="mini"
                type="primary"
                v-if="
                  scope.row.hcType == '退货单' &&
                    scope.row.returnFlag == '未完成'
                "
                @click="returnStock(scope.row)"
                >退货出库</el-button
              >
              <el-button
                size="mini"
                type="primary"
                v-if="scope.row.isCancelSend"
                @click="cancelPurchaseOrder(scope.row)"
                >取消发货</el-button
              >
              <el-button
                type="primary"
                size="mini"
                :disabled="scope.row.isAdd == true"
                @click="showCreate(true, scope.row, 'base')"
               
                >新增</el-button
              >
            </template>
          </el-table-column>
          <!-- </el-table> -->
        </template>
        <!-- <el-table v-loading="tableLoading" :data="tableData" border style="width: 100%">

            <el-table-column label="订单号" prop="poId"></el-table-column>
            <el-table-column label="仓库名称" prop="warehouseName"></el-table-column>
            <el-table-column label="供应商" prop="venderShortName"></el-table-column>
            <el-table-column label="采购方式" prop="purchaseMode"></el-table-column>

            <el-table-column label="采购总额" prop="purchaseTotalPrice"></el-table-column>
            <el-table-column label="采购总数" prop="purchaseTotalNum"></el-table-column>
            <el-table-column label="入库总额" prop="inStockTotalPrice"></el-table-column>
            <el-table-column label="入库数" prop="inStockTotalNum"></el-table-column>
            <el-table-column label="采购人" prop="createBy"></el-table-column>
            <el-table-column label="创建时间" prop="createTime"></el-table-column>
            <el-table-column label="操作" width="200">
              <template slot-scope="scope">
                <el-button size="mini" @click="showEdit(scope.row)">编辑</el-button>
                <el-button size="mini" type="danger" @click="deleteApplication(scope.row)">禁用</el-button>
              </template>
            </el-table-column>

        </el-table>-->

        <!-- </section>
    
        </main>-->
      </rg-page>
    </section>
    <!-- 首页 -->

    <!-- 新增和编辑页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        :title="formTitle"
        :visible.sync="formVisible"
        v-if="formVisible"
        :beforeClose="cancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            cancel('formModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="600px"
        v-loading="loading"
      >
        <template v-slot:content>
          <el-form
            :model="formModel"
            size="small"
            :rules="rules"
            ref="formModel"
            :disabled="isEditDisable"
          >
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="目标仓库"
                  prop="warehouseName"
                  :label-width="formLabelWidth"
                >
                  <el-select
                    v-model="formModel.warehouseName"
                    @change="changeWarehouse"
                    clearable
                    filterable
                    placeholder="请选择目标仓"
                    style="width:100%"
                  >
                    <el-option
                      v-for="item in warehouseSel"
                      :key="item.key"
                      :label="item.value"
                      :value="item.key"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="采购单类型"
                  prop="hcType"
                  :label-width="formLabelWidth"
                >
                  <el-select
                    v-model="formModel.hcType"
                    size="mini"
                    clearable
                    filterable
                    placeholder="请选择采购单类型"
                    style="width:100%"
                  >
                    <el-option
                      v-for="item in purchaseTypeSel"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                      size="mini"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="供应商"
                  prop="venderShortName"
                  :label-width="formLabelWidth"
                >
                  <el-select
                    v-model="formModel.venderShortName"
                    @change="changeVender"
                    style="width:100%"
                    clearable
                    filterable
                    placeholder="请选择供应商"
                  >
                    <el-option
                      v-for="item in venderSel"
                      :key="item.id"
                      :label="item.venderShortName"
                      :value="item.id"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="产品编号"
                  prop="productId"
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="formModel.productId"
                    :disabled="isproductIddisabled"
                    placeholder="请输入产品编号"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="产品名称"
                  prop="productName"
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="formModel.productName"
                    :disabled="isProductNamedisabled"
                    placeholder="请输入产品名称"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="预计到货时间"
                  prop="planInstockDate"
                  :label-width="formLabelWidth"
                >
                  <el-col>
                    <el-date-picker
                      :picker-options="pickerOptionsStart"
                      value-format="yyyy-MM-dd"
                      v-model="formModel.planInstockDate"
                      type="date"
                      placeholder="请选择预计到货时间"
                      style="width:100%"
                    />
                  </el-col>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="采购数量"
                  prop="num"
                  :label-width="formLabelWidth"
                >
                  <el-input
                    oninput="value=value.replace(/[^\d.]/g,'')"
                    v-model.number="formModel.num"
                    placeholder="请输入采购数量"
                    @change="priceChange()"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="采购单价"
                  prop="purchasePrice"
                  :label-width="formLabelWidth"
                >
                  <el-input
                    oninput="value=value.replace(/[^\d.]/g,'')"
                    v-model="formModel.purchasePrice"
                    placeholder="请输入采购单价"
                    @change="priceChange()"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item label="采购总价" :label-width="formLabelWidth">
                  <el-input readonly v-model="formModel.totalPrice" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item label="返利" :label-width="formLabelWidth">
                  <el-input
                    oninput="value=value.replace(/[^\d.]/g,'')"
                    placeholder="请输入返利"
                    v-model="formModel.rebate1"
                    @change="priceChange()"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item label="成本单价" :label-width="formLabelWidth">
                  <el-input readonly v-model="formModel.costPrice" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item label="总成本" :label-width="formLabelWidth">
                  <el-input readonly v-model="formModel.totalCost" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="提货方式"
                  prop="shipmentType"
                  :label-width="formLabelWidth"
                >
                  <el-select
                    v-model="formModel.shipmentType"
                    style="width:100%"
                    clearable
                    filterable
                    placeholder="请选择提货方式"
                    size="mini"
                  >
                    <el-option
                      v-for="item in shipmentTypeSel"
                      :key="item.key"
                      :label="item.value"
                      :value="item.key"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>
            <!-- 账期 -->
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="账期"
                  prop="accountPeriod"
                  :label-width="formLabelWidth"
                >
                  <el-select
                    v-model="formModel.accountPeriod"
                    style="width:100%"
                    clearable
                    filterable
                    placeholder="请选择账期"
                  >
                    <el-option
                      v-for="item in accountPeriodSel"
                      :key="item.key"
                      :label="item.value"
                      :value="item.key"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item label="税点" :label-width="formLabelWidth">
                  <el-input v-model="formModel.taxPoint" readonly />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item label="有发票" :label-width="formLabelWidth">
                  <el-switch v-model="formModel.isInvoice" />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item label="备注" :label-width="formLabelWidth">
                  <el-input
                    placeholder="请输入备注"
                    type="textarea"
                    v-model="formModel.remark"
                  />
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>

          <!-- 展示订单和采购单 -->

          <div v-if="sopoVisible">
            <div>
              <h3>销售单和采购单关联</h3>
              <el-table border :data="sopoArr" style="width: 100%">
                <el-table-column
                  label="订单号"
                  prop="orderNo"
                ></el-table-column>
                <el-table-column
                  label="订单产品单号"
                  prop="orderProductId"
                ></el-table-column>
                <el-table-column
                  label="采购总单号"
                  prop="purchaseOrderId"
                ></el-table-column>
                <el-table-column
                  label="采购子单号"
                  prop="purchaseProductId"
                ></el-table-column>
                <el-table-column
                  label="创建时间"
                  prop="createTime"
                ></el-table-column>
              </el-table>
            </div>
          </div>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            :disabled="isEditDisable"
            size="mini"
            type="primary"
            @click="save('formModel')"
            :loading="btnSaveLoading"
            >保存</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 新增和编辑页面 -->

    <!-- 发货页面 -->
    <section id="orderSend">
      <rg-dialog
        :title="sendFormTitle"
        :visible.sync="sendFormVisible"
        v-if="sendFormVisible"
        :beforeClose="sendCancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            sendCancel('sendFormModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="600px"
        v-loading="loading"
      >
        <template v-slot:content>
          <el-form :model="sendFormModel" ref="sendFormModel">
            <el-form-item label="预计发货时间">
              <template>
                <div class="block">
                  <el-date-picker
                    v-model="sendFormModel.earliestArrivalTime"
                    type="datetimerange"
                    :picker-options="sendpickerOptions"
                    range-separator="至"
                    start-placeholder="开始日期"
                    end-placeholder="结束日期"
                  ></el-date-picker>
                </div>
              </template>
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button type="primary" size="mini" @click="showDeliveryInfo()"
            >录入物流信息</el-button
          >
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="sendSave('sendFormModel')"
            >保存</el-button
          >
        </template>
        <!-- <div slot="footer" class="dialog-footer">
          <el-button @click="sendCancel('sendFormModel')">取消</el-button>
          <el-button type="primary" @click="showDeliveryInfo()">录入物流信息</el-button>
          <el-button type="primary" @click="sendSave('sendFormModel')">保存</el-button>
        </div>-->
      </rg-dialog>
    </section>
    <!-- 发货页面 -->
    <!-- 删除采购子单页面 -->
    <section id="deletePurchaseOrder">
      <rg-dialog
        :title="deleteFormTitle"
        :visible.sync="deleteFormVisible"
        v-if="deleteFormVisible"
        :beforeClose="deleteCancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            deleteCancel('deleteFormModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="700px"
        minWidth="600px"
      >
        <template v-slot:content>
          <el-form :model="deleteFormModel" ref="deleteFormModel">
            <el-form-item
              label="理由"
              size="50"
              style="width:600px;"
              :label-width="formLabelWidth"
            >
              <el-input
                type="textarea"
                :rows="5"
                placeholder="请输入理由"
                v-model="deleteFormModel.reason"
              ></el-input>
            </el-form-item>
          </el-form>
        </template>

        <template v-slot:footer>
          <el-button
            type="warning"
            @click="deleteSave('deleteFormModel')"
            size="mini"
            >确定</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 删除采购子单页面 -->

    <!-- 产品检索页面 -->
    <section id="selectProduct">
      <el-dialog
        :title="selectProductFormTitle"
        :close-on-click-modal="false"
        :visible.sync="selectProductFormVisible"
        :before-close="selectProductCancel"
      >
        <el-table
          border
          ref="multipleTable"
          @selection-change="handleSelectionChange"
          :data="productTableData"
          style="width: 100%"
        >
          <el-table-column type="selection"></el-table-column>

          <el-table-column
            label="产品编号"
            prop="productCode"
          ></el-table-column>
          <el-table-column label="产品名称" prop="name"></el-table-column>
          <el-table-column
            label="品牌"
            prop="brand"
            width="120px"
          ></el-table-column>

          <el-table-column
            label="单位"
            prop="unit"
            width="55px"
          ></el-table-column>
        </el-table>

        <div slot="footer" class="dialog-footer">
          <el-button @click="selectProductCancel()">取消</el-button>
          <el-button type="primary" @click="selectProductSave()"
            >确定</el-button
          >
        </div>
      </el-dialog>
    </section>
    <!-- 产品检索页面 -->

    <!-- 转仓操作历史 -->
    <section id="viewpurchaseorder">
      <el-dialog
        :title="viewpurchaseorderFormTitle"
        :close-on-click-modal="false"
        :visible.sync="viewpurchaseorderFormVisible"
        :before-close="viewpurchaseorderCancel"
      >
        <!-- <el-table border :data="purchaseorderHistoryTable" style="width: 100%">
          <el-table-column label="编号" prop="no"></el-table-column>
          <el-table-column label="备注" prop="remark"></el-table-column>
          <el-table-column label="创建人" prop="createBy"></el-table-column>
          <el-table-column label="创建时间" prop="createTime"></el-table-column>
        </el-table>-->
        <div class="block">
          <el-timeline>
            <el-timeline-item
              v-for="(activity, index) in purchaseorderHistoryTable"
              :key="index"
              :icon="eltimelineicon"
              :type="eltimelinetype"
              :size="eltimelinesize"
              :timestamp="activity.createTime"
              >{{ activity.createBy }} {{ activity.remark }}</el-timeline-item
            >
          </el-timeline>
        </div>
        <div slot="footer" class="dialog-footer">
          <el-button @click="viewpurchaseorderCancel()">关闭</el-button>
        </div>
      </el-dialog>
    </section>
    <!-- 转仓操作历史 -->

    <!-- 采购发货页面 -->
    <section id="deliverySection">
      <rg-dialog
        :title="deliveryFormTitle"
        :visible.sync="deliveryFormVisible"
        v-if="deliveryFormVisible"
        :beforeClose="deliveryCancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            deliveryCancel('deliveryFormModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="600px"
        v-loading="loading"
      >
        <template v-slot:content>
          <el-form
            :model="deliveryFormModel"
            ref="deliveryFormModel"
            :rules="deliveryrules"
            size="small"
          >
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="物流公司"
                  prop="deliveryCompany"
                  :label-width="formLabelWidth"
                >
                  <el-select
                    v-model="deliveryFormModel.deliveryCompany"
                    clearable
                    filterable
                    placeholder="请选择物流公司"
                  >
                    <el-option
                      v-for="item in deliveryCompanySel"
                      :key="item.key"
                      :label="item.value"
                      :value="item.key"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="物流单号"
                  prop="deliveryCode"
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="deliveryFormModel.deliveryCode"
                    placeholder="请输入物流单号"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="物流电话"
                  prop="deliveryTel"
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="deliveryFormModel.deliveryTel"
                    placeholder="请输入物流电话"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item
                  label="物流费用"
                  prop="deliveryFee"
                  :label-width="formLabelWidth"
                >
                  <el-input
                    v-model="deliveryFormModel.deliveryFee"
                    placeholder="请输入物流费用"
                    oninput="value=value.replace(/[^\d.]/g,'')"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item label="包裹重量" :label-width="formLabelWidth">
                  <el-input
                    width="200px"
                    v-model="deliveryFormModel.deliveryWeight"
                    placeholder="请输入重量(单位:kg)"
                    oninput="value=value.replace(/[^\d.]/g,'')"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item label="备注" :label-width="formLabelWidth">
                  <el-input
                    v-model="deliveryFormModel.remark"
                    placeholder="请输入备注"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16" style="text-align:center">
                <el-form-item>
                  <el-button
                    type="primary"
                    @click="deliverySave('deliveryFormModel')"
                    >确定</el-button
                  >
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>

          <el-table
            border
            :data="deliveryTableData"
            :cell-style="cellStyle"
            style="width: 100%"
          >
            <el-table-column
              label="物流公司"
              prop="deliveryCompany"
            ></el-table-column>

            <el-table-column
              label="物流单号"
              prop="deliveryCode"
            ></el-table-column>
            <el-table-column
              label="物流电话"
              prop="deliveryTel"
            ></el-table-column>

            <el-table-column
              label="包裹重量"
              prop="deliveryWeight"
            ></el-table-column>

            <el-table-column
              label="物流费用"
              prop="deliveryFee"
            ></el-table-column>

            <el-table-column label="备注" prop="remark"></el-table-column>
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button
            type="warning"
            @click="deliveryAccept('deliveryFormModel')"
            size="mini"
            >录入结束</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 采购发货页面 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/purchase/purchase";
import { appVenderSvc } from "@/api/purchase/vender";
import { Loading } from "element-ui";
export default {
  data() {
    return {
      expands: [],
      getRowKeys(row) {
        return row.poId;
      },

      pickerOptionsStart: {
        disabledDate(time) {
          return time.getTime() < Date.now();
        }
      },

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
      btnSaveLoading: false,
      loading: false,
      eltimelineicon: "el-icon-more",
      eltimelinetype: "primary",
      eltimelinesize: "large",
      isEditDisable: false,
      readonly: true,
      tableLoading: false,
      currentPage: 1,
      isproductIddisabled: false,
      isProductNamedisabled: false,
      flag: this.global.deletedFlag,
      //下拉框的条件
      sendFormTitle: "采购单发货",
      statusSelCondition: {
        RequestType: 1
      },
      purchaseModeSelCondition: {
        RequestType: 2
      },

      shipmentTypeSelCondition: {
        RequestType: 3
      },
      payStatusSelCondition: {
        RequestType: 4
      },

      warehouseSelCondition: {
        RequestType: 5
      },
      accountPeriodCondition: {
        RequestType: 6
      },

      deliveryCompanyCondition: {
        RequestType: 7
      },
      purchaseTypeSel: [
        {
          value: "采购单",
          label: "采购单"
        },
        {
          value: "退货单",
          label: "退货单"
        }
      ],

      //编辑条件
      editCondition: {
        id: undefined
      },
      viewpurchaseorderFormTitle: undefined,
      viewpurchaseorderFormVisible: false,
      purchaseorderHistoryTable: [],
      viewpurchaseorderCondition: {
        objectId: undefined,
        objectType: undefined
      },
      sopoArr: [],
      sopoVisible: false,

      //table页面的条件
      condition: {
        venderId: undefined,
        purchaseMode: undefined,
        status: undefined,
        payStatus: undefined,
        shipmentType: undefined,
        pageIndex: 1,
        pageSize: 10,
        //产品编号
        productId: undefined,
        //采购总单号
        poId: undefined,
        //采购子单号
        id: undefined,
        warehouseId: undefined
      },

      deleteCondition: {
        id: undefined,
        poId: undefined
      },

      deliveryFormVisible: false,
      deliveryCompanySel: [],
      deliveryFormTitle: undefined,

      deliveryTableData: [],

      deliveryFormModel: {
        deliveryCompany: undefined,
        deliveryCode: undefined,
        deliveryTel: undefined,
        deliveryFee: undefined,
        deliveryWeight: undefined,
        remark: undefined,
        purchaseId: undefined,
        purchaseType: undefined
      },

      deliveryFormModelInit: {
        deliveryCompany: undefined,
        deliveryCode: undefined,
        deliveryTel: undefined,
        deliveryFee: undefined,
        deliveryWeight: undefined,
        remark: undefined,
        purchaseId: undefined,
        purchaseType: undefined
      },
      venderSel: [],
      purchaseModeSel: [],
      statusSel: [],
      payStatusSel: [],
      shipmentTypeSel: [],
      warehouseSel: [],
      accountPeriodSel: [],
      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",

      sendFormVisible: false,

      //发货使用 Begin
      sendFormModel: {
        earliestArrivalTime: undefined,
        latestArrivalTime: undefined,
        poId: undefined
      },
      sendFormModelInit: {
        earliestArrivalTime: undefined,
        latestArrivalTime: undefined,
        poId: undefined
      },
      //发货使用 End
      deleteFormVisible: false,
      deleteFormTitle: "删除采购单",

      //删除使用 Begin
      deleteFormModel: {
        reason: undefined,
        id: undefined
      },
      deleteFormModelInit: {
        reason: undefined,
        id: undefined
      },
      //删除使用 End

      // 编辑、新增使用 Begin

      formInit: {
        operateType: undefined,
        id: undefined,
        poId: undefined,
        warehouseId: "",
        warehouseName: "",
        venderId: "",
        venderShortName: "",
        productId: "",
        productName: "",
        planInstockDate: "",
        isInvoice: 0,
        purchasePrice: "",
        num: "",
        costPrice: "",
        totalCost: "",
        rebate1: 0.0,
        shipmentType: "",
        accountPeriod: "",
        taxPoint: 1,
        remark: "",
        totalPrice: "",
        hcType: ""
      },

      //产品搜索的model
      productTableData: [],
      productCondition: {
        productName: undefined
      },
      selectProductFormTitle: undefined,
      selectProductFormVisible: false,
      //产品搜索的model

      selectSopoCondition: {
        purchaseProductId: undefined
      },

      deliveryCondition: {
        purchaseId: undefined,
        purchaseType: undefined
      },
      deliveryConditionInit: {
        purchaseId: undefined,
        purchaseType: undefined
      },

      haveDeliveryTable: [],

      deliverySaveCondition: {
        purchaseId: undefined,
        purchaseType: undefined,
        status: undefined
      },

      deliverySaveConditionInit: {
        purchaseId: undefined,
        purchaseType: undefined,
        status: undefined
      },

      returnStockCondition: {
        poId: undefined
      },
      //新增采购单model
      formModel: {
        operateType: undefined,
        id: undefined,
        poId: undefined,
        warehouseId: "",
        warehouseName: "",
        venderId: "",
        venderShortName: "",
        productId: "",
        productName: "",
        planInstockDate: "",
        isInvoice: 0,
        purchasePrice: "",
        num: "",
        costPrice: "",
        totalCost: "",
        rebate1: 0.0,
        shipmentType: "",
        accountPeriod: "",
        taxPoint: 1,
        remark: "",
        totalPrice: "",
        hcType: ""
      },
      rules: {
        venderShortName: [
          {
            required: true,
            message: "请选择供应商",
            trigger: "blur"
          }
        ],
        warehouseName: [
          {
            required: true,
            message: "请选择目标仓",
            trigger: "blur"
          }
        ],

        productId: [
          {
            required: true,
            message: "请输入产品编号",
            trigger: "blur"
          }
        ],
        planInstockDate: [
          {
            required: true,
            message: "请选择预计到货时间",
            trigger: "blur"
          }
        ],
        num: [
          {
            required: true,
            message: "请输入采购数量",
            trigger: "blur"
          }
        ],
        purchasePrice: [
          {
            required: true,
            message: "请输入采购价",
            trigger: "blur"
          }
        ],
        shipmentType: [
          {
            required: true,
            message: "请选择提货方式",
            trigger: "blur"
          }
        ],
        accountPeriod: [
          {
            required: true,
            message: "请选择账期",
            trigger: "blur"
          }
        ]
      },
      deliveryrules: {
        deliveryCompany: [
          { required: true, message: "请选择物流公司", trigger: "blur" }
        ],
        deliveryCode: [
          { required: true, message: "请输入物流单号", trigger: "blur" }
        ],
        deliveryTel: [
          { required: true, message: "请输入物流电话", trigger: "blur" }
        ],
        deliveryFee: [
          { required: true, message: "请输入物流费用", trigger: "blur" }
        ]
      },
      // 编辑、新增使用 End
      formLabelWidth: "120px",
      cancelPurchaseOrderCondition: { id: undefined }
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
    returnStock(row) {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          this.returnStockCondition.poId = row.poId;

          //调用后台接口
          appSvc
            .purchaseOrderReturn(this.returnStockCondition)
            .then(res => {
              if (res.code == 10000) {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  this.search();
                }
              }
            })
            .catch(error => {
              console.log(error);
            });
        })
        .catch(() => {});
    },
    loadChildTableData(row, expandedRows) {
      this.subCompanyCondition.parentId = row.poId;
      // shopManageSvc
      //       .getCompanySubs(this.subCompanyCondition)
      //       .then(
      //         res => {
      //          this.childTableData = res.data;

      //         },
      //         error => {
      //           console.log(error);
      //         }
      //       )
      //       .catch(() => {})
      //       .finally(() => {
      //        // this.tableLoading = false;
      //       });
    },
    cancelPurchaseOrder(row) {
      this.cancelPurchaseOrderCondition.id = row.poId;
      appSvc
        .cancelPurchaseOrder(this.cancelPurchaseOrderCondition)
        .then(
          res => {
            if (res.message != "") {
              this.$message({
                message: res.message,
                type: "success"
              });
            }
            this.search();
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    viewpurchaseorderCancel() {
      this.viewpurchaseorderFormVisible = false;
    },
    deliveryCancel() {
      this.deliveryFormVisible = false;
      this.deliveryFormModel = JSON.parse(
        JSON.stringify(this.deliveryFormModelInit)
      );
    },
    showDeliveryInfo() {
      this.deliveryFormVisible = true;
      this.deliveryFormTitle = "录入物流信息";
      this.deliveryFormModel.purchaseId = this.sendFormModel.poId;
      this.deliveryFormModel.purchaseType = "采购单";
      appSvc
        .getBasicInfoList(this.deliveryCompanyCondition)
        .then(
          res => {
            this.deliveryCompanySel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    deliveryAccept() {
      this.$confirm("确定操作吗！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          this.deliverySaveCondition.purchaseId = this.deliveryFormModel.purchaseId;
          this.deliverySaveCondition.purchaseType = this.deliveryFormModel.purchaseType;
          this.deliverySaveCondition.status = "已发出";

          //调用后台接口
          appSvc
            .updatePurchaseOrderDeliveryStatus(this.deliverySaveCondition)
            .then(res => {
              if (res.code == 10000) {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                }

                this.deliveryFormVisible = false;
                this.deliverySaveCondition = JSON.parse(
                  JSON.stringify(this.deliverySaveConditionInit)
                );

                //重新load一遍数据
                this.deliveryCondition.purchaseId = this.deliveryFormModel.purchaseId;
                this.deliveryCondition.purchaseType = "采购单";

                appSvc
                  .getPurchaseOrderDeliveries(this.deliveryCondition)
                  .then(res => {
                    if (res.code == 10000) {
                      deliveryArr = res.data;
                    }
                  })
                  .catch(error => {
                    console.log(error);
                  });
              }
            })
            .catch(error => {
              console.log(error);
            });
        })
        .catch(() => {});
    },
    deliverySave(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          appSvc
            .createPurchaseOrderDelivery(this.deliveryFormModel)
            .then(
              res => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                }

                var purchaseId = this.deliveryFormModel.purchaseId;
                var purchaseType = this.deliveryFormModel.purchaseType;

                this.deliveryFormModel = JSON.parse(
                  JSON.stringify(this.deliveryFormModelInit)
                );
                //重新load数据

                this.deliveryFormModel.purchaseId = purchaseId;
                this.deliveryFormModel.purchaseType = purchaseType;
                appSvc
                  .getPurchaseOrderDeliveries(this.deliveryFormModel)
                  .then(
                    res => {
                      this.deliveryTableData = res.data;
                    },
                    error => {
                      console.log(error);
                    }
                  )
                  .catch(() => {});
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        } else {
          return false;
        }
      });
    },

    viewpurchasehistory(row) {
      this.viewpurchaseorderFormTitle = "采购单【" + row.id + "】操作历史";
      this.viewpurchaseorderFormVisible = true;
      this.viewpurchaseorderCondition.objectId = row.id;
      this.viewpurchaseorderCondition.objectType = "PoItem";
      appSvc
        .getPurchaseOrderTrackLogs(this.viewpurchaseorderCondition)
        .then(
          res => {
            this.purchaseorderHistoryTable = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    searchProduct() {
      this.selectProductFormTitle = "查询产品";
      this.selectProductFormVisible = true;
      this.productCondition.productName = "金冷 冷冻油 R-134a 汽车空调压";
      appSvc
        .productSearch(this.productCondition)
        .then(
          res => {
            debugger;
            var resData = res.data;

            this.productTableData = resData.items;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    selectProductCancel() {
      this.selectProductFormVisible = false;
    },

    //输入产品名称 检索产品编号
    selectProductSave() {
      //只能选择一个产品
      var productCode = "";
      if (this.$refs.multipleTable.selection.length > 0) {
        if (this.$refs.multipleTable.selection.length < 2) {
          for (var i = 0; i < this.$refs.multipleTable.selection.length; i++) {
            productCode = this.$refs.multipleTable.selection[i].productCode;
          }
          console.log(productCode);
        } else {
          this.$message({
            message: "最多只能选择一个产品!",
            type: "warning"
          });
        }
      } else {
        this.$message({
          message: "请选择产品!",
          type: "warning"
        });
      }
    },

    toggleSelection(rows) {
      if (rows) {
        rows.forEach(row => {
          this.$refs.multipleTable.toggleRowSelection(row);
        });
      } else {
        this.$refs.multipleTable.clearSelection();
      }
    },
    handleSelectionChange(val) {
      this.multipleSelection = val;
    },
    cellStyle(row, column, rowIndex, columnIndex) {
      //根据报警级别显示颜色
      // console.log(row);
      // console.log(row.column);
      if (row.row.status === "已取消" && row.column.label === "状态") {
        return "color:red";
      }
      // else if(row.column.label==="告警级别"&& row.row.alarmLevel==="一般告警" ){
      //   return 'color:yellow'
      // }
    },

    updatePoId() {
      if (this.condition.poId == "") {
        this.condition.poId = undefined;
      }
    },
    updateId() {
      if (this.condition.id == "") {
        this.condition.id = undefined;
      }
    },

    expandSelect(row, expandedRows) {
      // expandedRows.length != 0 ? console.log(row.id) : ''
      if (expandedRows.length != 0) {
        //在tableData中循环

        let obj = {};
        obj = this.tableData.find(item => {
          return item.poId === row.poId;
        });

        row.products = obj.products;
      }
    },
    clickRow(row, column, event) {
      console.log(row.id);
      // console.log(column)
      // console.log(event)
    },
    changeWarehouse(val) {
      //locations是v-for里面的也是datas里面的值
      let obj = {};
      var warehouseName = "";
      for (var i = 0; i < this.warehouseSel.length; i++) {
        if (this.warehouseSel[i].key == Number(val))
          warehouseName = this.warehouseSel[i].value;
      }

      // obj = this.warehouseSel.find(item => {
      //   return item.key === Number(val);
      // });
      this.formModel.warehouseName = warehouseName;
      this.formModel.warehouseId = Number(val);
    },
    changeVender(val) {
      //locations是v-for里面的也是datas里面的值
      // let obj = {};
      // obj = this.venderSel.find(item => {
      //   return item.value === val;
      // });

      var venderName = "";
      for (var i = 0; i < this.venderSel.length; i++) {
        if (this.venderSel[i].id == Number(val))
          venderName = this.venderSel[i].venderShortName;
      }
      this.formModel.venderShortName = venderName;
      this.formModel.venderId = Number(val);
    },

    //获取供应商信息
    getVenderData() {
      appVenderSvc
        .getVenders()
        .then(
          res => {
            this.venderSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //获取采购方式
    getPurchaseModes() {
      appSvc
        .getBasicInfoList(this.purchaseModeSelCondition)
        .then(
          res => {
            this.purchaseModeSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //获取目标仓
    getwarehouses() {
      appSvc
        .getBasicInfoList(this.warehouseSelCondition)
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

    //获取采购状态
    getPurchaseStatus() {
      appSvc
        .getBasicInfoList(this.statusSelCondition)
        .then(
          res => {
            this.statusSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    //获取付款状态
    getPayStatus() {
      appSvc
        .getBasicInfoList(this.payStatusSelCondition)
        .then(
          res => {
            this.payStatusSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    //获取提货方式
    getShipmentType() {
      appSvc
        .getBasicInfoList(this.shipmentTypeSelCondition)
        .then(
          res => {
            this.shipmentTypeSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //获取账期
    getAccountPeriod() {
      appSvc
        .getBasicInfoList(this.accountPeriodCondition)
        .then(
          res => {
            this.accountPeriodSel = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    priceChange() {
      if (this.formModel.purchasePrice < 0) {
        this.formModel.purchasePrice = 0;
      }

      if (this.formModel.num < 0) {
        this.formModel.num = 0;
      }
      if (this.formModel.rebate1 < 0) {
        this.formModel.rebate1 = 0;
      }
      //计算成本价
      if (this.formModel.taxPoint > 0) {
        this.formModel.costPrice = (
          this.formModel.purchasePrice * this.formModel.taxPoint -
          this.formModel.rebate1
        ).toFixed(2);
      } else {
        this.formModel.costPrice = (
          this.formModel.purchasePrice - this.formModel.rebate1
        ).toFixed(2);
      }

      this.formModel.totalPrice = (
        this.formModel.purchasePrice *
        this.formModel.num *
        this.formModel.taxPoint
      ).toFixed(2);
      this.formModel.totalCost = (
        this.formModel.costPrice * this.formModel.num
      ).toFixed(2);
    },

    fetchData() {
      this.tableLoading = true;
      // this.listLoading = true;
      this.getVenderData();
      this.getPurchaseModes();
      this.getPurchaseStatus();
      this.getShipmentType();
      this.getPayStatus();
      this.getwarehouses();
      // this.getConditionData();

      appSvc
        .getPurchaseOrderList(this.condition)
        .then(
          res => {
            //  this.tableData = res.data;
            var data = res.data;

            this.tableData = data.purchaseOrderList;
            this.totalList = data.totalItems;
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
    // sizeChange(val) {
    //   this.condition.pageIndex = this.currentPage = 1;
    //   this.condition.pageSize = val;
    //   this.getPaged();
    // },

    pageChange(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    showCreate(isBase, row, opType) {
      if (isBase) {
        this.formTitle = "【" + row.poId + "】新增采购单";
        this.formModel.warehouseName = row.warehouseName;
        this.formModel.warehouseId = row.warehouseId;
        this.formModel.poId = row.poId;
        this.formModel.venderId = row.venderId;
        this.formModel.venderShortName = row.venderShortName;
        this.formModel.hcType = row.hcType;
      } else {
        this.formTitle = "新增采购单";
      }
      this.sopoVisible = false;
      this.formVisible = true;
      this.formModel.operateType = opType;
      this.formIsCreated = true;
      this.isProductNamedisabled = true;
      this.getAccountPeriod();
    },

    sendOrder(row) {
      // this.$confirm("确定发货吗！", "警告", {
      //   confirmButtonText: "确定",
      //   cancelButtonText: "取消",
      //   type: "warning"
      // })
      //   .then(() => {
      //     this.sendFormVisible = true;
      //     this.sendFormModel.poId = row.poId;
      //   })
      //   .catch(() => {});

      this.sendFormVisible = true;
      this.sendFormModel.poId = row.poId;

      //判断是否填写了快递信息

      this.deliveryCondition.purchaseId = this.sendFormModel.poId;
      this.deliveryCondition.purchaseType = "采购单";

      appSvc
        .getPurchaseOrderDeliveries(this.deliveryCondition)
        .then(res => {
          if (res.code == 10000) {
            this.haveDeliveryTable = res.data;
          }
        })
        .catch(error => {
          console.log(error);
        });
    },

    editPurchaseOrder(row) {
      debugger;
      this.formVisible = true;
      this.formTitle = "编辑采购子单【" + row.id + "】";
      this.formIsCreated = false;
      this.isproductIddisabled = true;
      this.isProductNamedisabled = true;
      this.editCondition.id = row.id;
      //this.formModel = JSON.parse(JSON.stringify(row));

      this.selectSopoCondition.purchaseProductId = row.id;
      appSvc
        .selectSopos(this.selectSopoCondition)
        .then(res => {
          if (res.code == 10000) {
            this.sopoArr = res.data;
            if (this.sopoArr.length > 0) {
              this.sopoVisible = true;
            } else {
              this.sopoVisible = false;
            }
            //this.formModel.warehouseId = res.data.warehouseId;
          }
        })
        .catch(error => {
          console.log(error);
        });

      appSvc
        .getPurchaseOrderProduct(this.editCondition)
        .then(res => {
          if (res.code == 10000) {
            this.formModel = res.data;
            this.formModel.operateType = "edit";

            if (
              this.formModel.status == "已发货" ||
              this.formModel.status == "审核通过" ||
              this.formModel.status == "已收货" ||
              this.formModel.status == "部分收货"
            ) {
              this.isEditDisable = true;
            }

            //this.formModel.warehouseId = res.data.warehouseId;
          }
        })
        .catch(error => {
          console.log(error);
        });

      // this.formModel = JSON.parse(JSON.stringify(row));
    },

    // showEdit(row) {
    //   if (row.isDeleted) {
    //     this.$alert("系统 " + row.name + " 已禁用，不允许编辑！", "警告", {
    //       confirmButtonText: "关闭",
    //       type: "warning"
    //     });
    //     return;
    //   }

    //   this.formVisible = true;
    //   this.formTitle = "编辑";
    //   this.formIsCreated = false;

    //   this.formModel = JSON.parse(JSON.stringify(row));
    // },
    getPaged(flag) {
      debugger;

      this.tableLoading = true;

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getPurchaseOrderList(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.purchaseOrderList;
            this.totalList = data.totalItems;

            if (flag) {
              if (res.message != "") {
                this.$message({
                  message: res.message,
                  type: "success"
                });
              }
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          console.log("tableLoading: false");
          this.tableLoading = false;
        });
    },
    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    },
    resetForm() {
      this.formModel = JSON.parse(JSON.stringify(this.formInit));
    },
    resetSendForm() {
      this.sendFormModel = JSON.parse(JSON.stringify(this.sendFormModelInit));
    },

    sendSave(formName) {
      debugger;
      var vm = this;
      //不填写也没关系 但是系统要提示用户
      if (this.haveDeliveryTable.length <= 0) {
        vm.$confirm("不填写物流信息直接发货?", "警告", {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning"
        })
          .then(() => {
            if (
              this.sendFormModel.earliestArrivalTime != null &&
              this.sendFormModel.earliestArrivalTime != undefined
            ) {
              appSvc
                .sendPurchaseOrder(this.sendFormModel)
                .then(
                  res => {
                    if (res.code == 10000) {
                      this.$message({
                        message: res.message,
                        type: "success"
                      });
                      vm.search();
                      this.sendFormVisible = false;
                      this.sendFormModel = JSON.parse(
                        JSON.stringify(this.sendFormModelInit)
                      );
                      var frmName =
                        typeof formName === "string" && formName
                          ? formName
                          : "sendFormModel";
                      this.$refs[frmName].resetFields();
                      this.haveDeliveryTable = [];
                      // vm.getConditionData();
                      // vm.cancel();
                    } else {
                      this.$message({
                        message: res.message,
                        type: "warning"
                      });
                    }
                    //重置标签状态
                    this.isproductIddisabled = false;
                    this.isProductNamedisabled = false;
                  },
                  error => {
                    console.log(error);
                  }
                )
                .catch(() => {});
            } else {
              this.$message({
                message: "请填写发货时间!",
                type: "warning"
              });
            }
          })
          .catch(() => {
            this.deliveryFormVisible = true;
            this.deliveryFormTitle = "录入物流信息";
            this.deliveryFormModel.purchaseId = this.sendFormModel.poId;
            this.deliveryFormModel.purchaseType = "采购单";
            appSvc
              .getBasicInfoList(this.deliveryCompanyCondition)
              .then(
                res => {
                  this.deliveryCompanySel = res.data;
                },
                error => {
                  console.log(error);
                }
              )
              .catch(() => {});
          });
      } else {
        if (
          this.sendFormModel.earliestArrivalTime != null &&
          this.sendFormModel.earliestArrivalTime != undefined
        ) {
          appSvc
            .sendPurchaseOrder(this.sendFormModel)
            .then(
              res => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  vm.search();
                  this.sendFormVisible = false;
                  this.sendFormModel = JSON.parse(
                    JSON.stringify(this.sendFormModelInit)
                  );
                  var frmName =
                    typeof formName === "string" && formName
                      ? formName
                      : "sendFormModel";
                  this.$refs[frmName].resetFields();
                  this.haveDeliveryTable = [];
                  // vm.getConditionData();
                  // vm.cancel();
                } else {
                  this.$message({
                    message: res.message,
                    type: "warning"
                  });
                }
                //重置标签状态
                this.isproductIddisabled = false;
                this.isProductNamedisabled = false;
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        } else {
          this.$message({
            message: "请填写发货时间!",
            type: "warning"
          });
        }
      }
    },

    sendSaveFunc(formName, deliveryArr) {},

    deleteSave() {
      var vm = this;
      console.log("formModel: " + JSON.stringify(this.deleteFormModel));
      appSvc
        .deletePurchaseOrderProduct(this.deleteFormModel)
        .then(
          res => {
            if (res.code == 10000) {
              this.$message({
                message: res.message,
                type: "success"
              });
              vm.search();
              this.deleteFormVisible = false;
              this.deleteFormModel = JSON.parse(
                JSON.stringify(this.deleteFormModelInit)
              );
              // vm.getConditionData();
              vm.cancel();
            } else {
              this.$message({
                message: res.message,
                type: "warning"
              });
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    save(formName) {
      var vm = this;
      console.log("formModel: " + JSON.stringify(this.formModel));
      if (this.formModel.num <= 0) {
        this.$message({
          message: "采购数量必须大于0",
          type: "warning"
        });
        return;
      }
      if (this.formModel.purchasePrice <= 0) {
        this.$message({
          message: "采购价必须大于0",
          type: "warning"
        });
        return;
      }
      debugger;
      this.$refs[formName].validate(valid => {
        if (valid) {
          const loading = this.$loading({
            lock: true,
            text: "拼命处理中......",
            spinner: "el-icon-loading",
            background: "rgba(0, 0, 0, 0.7)"
          });
          appSvc
            .createPurchaseOrder(this.formModel)
            .then(res => {
              if (res.code == 10000) {
                this.$message({
                  message: res.message,
                  type: "success"
                });
                vm.search();
                this.formVisible = false;
                vm.cancel();
              }
            })
            .catch(error => {
              console.log(error);
            })
            .finally(() => {
              loading.close();
            });
        } else {
          return false;
        }
      });
    },
    deleteCancel() {
      this.deleteFormVisible = false;
      // this.resetSendForm();
      this.deleteFormModel = JSON.parse(
        JSON.stringify(this.deleteFormModelInit)
      );
      var frmName =
        typeof formName === "string" && formName ? formName : "deleteFormModel";
      this.$refs[frmName].resetFields();
    },

    deletePurchaseOrder(row) {
      this.$confirm("是否要删除采购单【 " + row.id + " 】！", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.deleteFormVisible = true;
          this.deleteFormModel.id = row.id;
        })
        .catch(() => {});
    },
    sendCancel(formName) {
      this.sendFormVisible = false;
      // this.resetSendForm();
      this.sendFormModel = JSON.parse(JSON.stringify(this.sendFormModelInit));
      var frmName =
        typeof formName === "string" && formName ? formName : "sendFormModel";
      this.$refs[frmName].resetFields();
      this.haveDeliveryTable = [];
    },
    cancel(formName) {
      this.isEditDisable = false;
      this.formVisible = false;
      // this.resetSendForm();
      this.formModel = JSON.parse(JSON.stringify(this.formInit));
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    }
  }
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
>>> .el-form--inline .el-form-item {
  padding-bottom: 10px;
}
</style>
