<template>
  <main class="container" v-loading="dbLoading">
    <!-- 首页：列表数据 -->
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
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-select
            v-model="condition.warehouseId"
            clearable
            style="width:150px"
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
        <el-form-item label>
          <el-select
            v-model="condition.sourceType"
            placeholder="请选择商品类型"
            clearable
            style="width:120px"
          >
            <el-option
              v-for="item in sourceTypeSel"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
      
        <el-form-item label>
          <el-tooltip
            class="item"
            effect="dark"
            content="盘点计划最晚完成日期"
            placement="bottom"
          >
            <el-date-picker
              v-model="condition.times"
              type="daterange"
              :picker-options="$root.$data.timeRgPickerOpt"
              style="width:220px"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              align="right"
            ></el-date-picker>
          </el-tooltip>
        </el-form-item>
        <el-form-item label>
          <el-select
            v-model="condition.status"
            clearable
            placeholder="请选择状态"
            style="width:120px"
          >
            <el-option
              v-for="item in statusSel"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label>
          <el-select
            v-model="condition.type"
            clearable
            placeholder="请选择盘点类型"
            style="width:120px"
          >
            <el-option
              v-for="item in typeSel"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <el-button type="primary" icon="el-icon-plus" @click="addplan"
          >新增</el-button
        >
      </template>
      <!-- 搜索 -->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <el-table-column
          label="盘点单号"
          prop="id"
          v-if="true"
        ></el-table-column>
        <rg-table-column label="盘点名称" prop="planName" />
        <rg-table-column label="仓库" prop="warehouseName" />
        <rg-table-column label="商品类型">
          <template slot-scope="scope">
            <label>{{
              scope.row.sourceType == "Company" ? "平台商品" : "外采商品"
            }}</label>
          </template>
        </rg-table-column>
        <el-table-column
          label="盘点类型"
          prop="type"
          width="120px"
        ></el-table-column>
        <el-table-column
          label="执行状态"
          prop="status"
          width="120px"
        ></el-table-column>
        <el-table-column
          label="计划最晚完成时间"
          prop="planTime"
          align="center"
          width="120px"
        >
          <template slot-scope="scope">{{
            $jscom.toYMDHm(scope.row.planTime)
          }}</template>
        </el-table-column>
        <el-table-column label="实际完成时间" align="center" width="120px">
          <template slot-scope="scope">{{
            $jscom.toYMDHm(scope.row.actualTime)
          }}</template>
        </el-table-column>
        <el-table-column
          label="创建人"
          prop="createBy"
          width="90px"
        ></el-table-column>
        <el-table-column
          label="创建时间"
          prop="createTime"
          align="center"
          width="120px"
        >
          <template slot-scope="scope">{{
            $jscom.toYMDHm(scope.row.createTime)
          }}</template>
        </el-table-column>
        <el-table-column label="操作" fixed="right" align="center" width="180px">
          <template slot-scope="scope">
            <el-button-group>
              <el-button type="primary" style="margin-right:3px;" size="mini" @click="viewplan(scope.row)"
                >盘点详情</el-button
              >
              <el-button type="primary" size="mini" @click="viewlog(scope.row)"
                >查看日志</el-button
              >
            </el-button-group>
          </template>
        </el-table-column>

        <!-- Table数据 -->
      </template>
      <!-- 列表 -->
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
          click: done => {
            cancel('formModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        v-if="formVisible"
        maxWidth="800px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-form
            :model="formModel"
            :rules="rules"
            size="mini"
            ref="formModel"
            label-width="120px"
          >
            <el-row>
              <el-col :span="16">
                <el-form-item label="盘点名称" prop="planName">
                  <el-input
                    style="width:220px"
                    v-model="formModel.planName"
                    autocomplete="off"
                    clearable
                  ></el-input>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="16">
                <el-form-item label="仓库" prop="warehouseId">
                  <el-select
                    v-model="formModel.warehouseId"
                    clearable
                    style="width:220px"
                    filterable
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
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="16">
                <el-form-item label="商品类型" prop="sourceType">
                  <el-select v-model="formModel.sourceType" >
                    <el-option
                      v-for="item in sourceTypeSel"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="16">
                <el-form-item label="盘点类型" prop="type">
                  <el-radio-group
                    @change="changetype()"
                    v-model="formModel.type"
                  >
                    <el-radio :label="1">指定产品</el-radio>
                    <!-- <el-radio :label="2">全盘</el-radio> -->
                  </el-radio-group>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="24">
                <el-form-item label="最晚盘点时间" prop="planTime">
                  <el-date-picker
                    v-model="formModel.planTime"
                    type="datetime"
                    :picker-options="pickerOptions"
                    placeholder="选择日期时间"
                    default-time="12:00:00"
                  ></el-date-picker>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row v-if="addproductFlag">
              <el-col :span="24">
                <!-- <el-tabs v-model="productTabName" @tab-click="handleClick">
                <el-tab-pane label="总部产品" name="first">-->
                <el-button type="primary" size="mini" @click="selectProduct()"
                  >选择产品</el-button
                >
                <el-row>
                  <el-table
                    border
                    :data="selectproductTable"
                    style="width: 100%"
                    height="300px"
                    size="mini"
                  >
                    <el-table-column
                      label="产品编号"
                      prop="productId"
                      width="120px"
                    ></el-table-column>
                    <el-table-column
                      label="产品名称"
                      prop="productName"
                    ></el-table-column>
                    <el-table-column label="操作" align="center" width="80px">
                      <template slot-scope="scope">
                        <el-button
                          type="primary"
                          size="mini"
                          @click="removeItem(scope.$index, selectproductTable)"
                          >移除</el-button
                        >
                      </template>
                    </el-table-column>
                  </el-table>
                </el-row>
              </el-col>
            </el-row>
          </el-form>
        </template>

        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            type="primary"
            size="mini"
            :disabled="formCheck"
            :loading="btnSaveLoading"
            @click="save('formModel')"
            >提交</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 新增和编辑页面 -->

    <!-- 选择产品页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        :title="productformTitle"
        :visible.sync="productformVisible"
        :beforeClose="productcancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            productcancel('productformModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="800px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-form
            :model="productformModel"
            :inline="true"
            ref="productformModel"
            size="mini"
          >
            <el-form-item label="产品名称">
              <el-input
                v-model="productformModel.key"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>
            <el-form-item>
              <el-button
                icon="el-icon-search"
                type="warning"
                size="mini"
                @click="searchproduct()"
                >查询</el-button
              >
            </el-form-item>
          </el-form>

          <el-table
            border
            ref="multipleTable"
            @selection-change="handleSelectionChange"
            :data="productTableData"
            style="width: 100%"
            height="300px"
            size="mini"
          >
            <el-table-column type="selection" align="center"></el-table-column>
            <el-table-column
              label="产品编号"
              prop="productCode"
              min-width="120px"
            ></el-table-column>
            <el-table-column
              label="产品名称"
              prop="productName"
            ></el-table-column>
            <el-table-column
              v-if="false"
              label="销售价格"
              prop="salesPrice"
              width="90px"
              align="right"
            >
              <template slot-scope="scope">{{
                $jscom.toMoney(scope.row.salesPrice)
              }}</template>
            </el-table-column>
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="saveProduct()"
            >确定</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 选择产品页面 -->

    <!-- 查看明细页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        :title="detailformTitle"
        :visible.sync="detialformVisible"
        :beforeClose="viewdetailcancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            viewdetailcancel();
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="90%"
        v-if="detialformVisible"
        maxWidth="1024px"
        minWidth="900px"
      >
        <template v-slot:content>
          <el-form
            :model="detailFormModel"
            :rules="rules"
            ref="detailFormModel"
            label-width="120px"
            size="mini"
          >
            <el-form-item label="盘点名称">
              <span>{{ detailFormModel.planName }}</span>
            </el-form-item>

            <el-form-item label="仓库">
              <span>{{ detailFormModel.warehouseName }}</span>
            </el-form-item>
            <el-form-item label="商品类型">
              <span>{{ detailFormModel.sourceType }}</span>
            </el-form-item>
            <el-form-item label="最晚盘点时间">
              <span>{{ detailFormModel.planTime }}</span>
            </el-form-item>
          </el-form>
          <el-row>
            <el-col :span="24">
              <el-tabs v-model="storageTabName" @tab-click="viewStorageData">
                <el-tab-pane label="待盘点" name="first">
                  <el-row>
                    <section>
                      <el-table
                        border
                        :data="waitstorageProductTable"
                        style="width: 100%"
                        size="mini"
                        height="400px"
                      >
                        <rg-table-column
                          label="产品编号"
                          prop="productId"
                        ></rg-table-column>
                        <rg-table-column
                          label="产品名称"
                          prop="productName"
                        ></rg-table-column>
                        <el-table-column
                          label="单位"
                          prop="unit"
                          width="90"
                        ></el-table-column>
                        <el-table-column
                          label="系统数量"
                          prop="systemNum"
                          align="right"
                          width="90"
                        ></el-table-column>
                        <el-table-column
                          label="盘点数量"
                          align="right"
                          width="90"
                        >
                          <template slot-scope="scope">
                            <el-input
                              size="mini"
                              v-model="scope.row.storageNum"
                              oninput="value=value.replace(/[^\d.]/g,'')"
                              @blur="checkNum(scope.row)"
                              clearable
                            ></el-input>
                          </template>
                        </el-table-column>

                        <el-table-column
                          label="差异"
                          prop="diffNum"
                          align="right"
                          width="90"
                        ></el-table-column>
                      </el-table>
                    </section>
                    <section>
                      <el-pagination
                        style="text-align:center;margin-top:15px;"
                        background
                        :page-size="50"
                        :page-sizes="[10, 30, 50, 100]"
                        layout="total, sizes, prev, pager, next, jumper"
                        :total="waittotallist"
                        @current-change="waitpageTurning"
                        :current-page.sync="waitcurrentPage"
                        @size-change="waitsizeChange"
                      ></el-pagination>
                    </section>
                  </el-row>
                </el-tab-pane>
                <el-tab-pane label="已盘点" name="second">
                  <el-row>
                    <section>
                      <el-table
                        border
                        :data="finishstorageProductTable"
                        style="width: 100%"
                        :row-class-name="
                          p => {
                            return p.row.status == '无效' ? 'invalid' : '';
                          }
                        "
                        :cell-class-name="
                          p => {
                            return p.columnIndex == 5
                              ? p.row.diffNum < 0
                                ? 'red'
                                : p.row.diffNum > 0
                                ? 'orange'
                                : ''
                              : '';
                          }
                        "
                        size="mini"
                        height="400"
                      >
                        <rg-table-column
                          label="产品编号"
                          prop="productId"
                        ></rg-table-column>
                        <rg-table-column
                          label="产品名称"
                          prop="productName"
                        ></rg-table-column>
                        <el-table-column
                          label="单位"
                          prop="unit"
                          width="80"
                        ></el-table-column>
                        <el-table-column
                          label="系统数量"
                          prop="systemNum"
                          align="right"
                          width="80"
                        ></el-table-column>
                        <el-table-column
                          label="盘点数量"
                          prop="storageNum"
                          align="right"
                          width="80"
                        ></el-table-column>
                        <el-table-column label="差异" align="right" width="80">
                          <template slot-scope="scope">
                            <label :title="scope.row.status">{{
                              absNum(scope.row.diffNum.format(0))
                            }}</label>
                          </template>
                        </el-table-column>
                        <el-table-column
                          label="状态"
                          prop="status"
                          width="90"
                        ></el-table-column>
                        <el-table-column
                          label="盘点人"
                          prop="operateBy"
                          width="90"
                        ></el-table-column>
                        <el-table-column
                          label="盘点时间"
                          prop="operateTime"
                          align="center"
                          width="110"
                        >
                          <template slot-scope="scope">{{
                            $jscom.toYMDHm(scope.row.operateTime)
                          }}</template>
                        </el-table-column>
                        <el-table-column
                          label="操作"
                          align="center"
                          width="80"
                          fixed="right"
                        >
                          <template slot-scope="scope">
                            <el-button
                              size="mini"
                              :disabled="disabledBtn"
                              type="danger"
                              @click="
                                removeproduct(
                                  scope.row,
                                  scope.$index,
                                  finishstorageProductTable
                                )
                              "
                              >删除重盘</el-button
                            >
                          </template>
                        </el-table-column>
                      </el-table>
                    </section>
                    <section>
                      <el-pagination
                        style="text-align:center;margin-top:15px;"
                        background
                        :page-size="50"
                        :page-sizes="[10, 30, 50, 100]"
                        layout="total, sizes, prev, pager, next, jumper"
                        :total="finishtotallist"
                        @current-change="finishpageTurning"
                        :current-page.sync="finishcurrentPage"
                        @size-change="finishsizeChange"
                      ></el-pagination>
                    </section>
                  </el-row>
                </el-tab-pane>
              </el-tabs>
            </el-col>
          </el-row>
        </template>

        <template v-slot:footer>
          <el-button
            size="mini"
            style="background-color:#33ccb0"
            type="primary"
            :disabled="disabledBtn"
            :loading="btnSaveLoading"
            @click="tempSave()"
            >暂存</el-button
          >

          <el-button
            size="mini"
            type="primary"
            :disabled="disabledBtn"
            :loading="btnSaveLoading"
            @click="saveplanData()"
            >盘点完成</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 查看明细页面 -->

    <!-- 日志详情页面 -->
    <section id="createOrUpdate">
      <rg-dialog
        :title="logformTitle"
        :visible.sync="logformVisible"
        :beforeClose="closeLogForm"
        :btnCancel="{
          label: '关闭',
          click: done => {
            closeLogForm();
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="100%"
        maxWidth="800px"
        minWidth="700px"
      >
        <template v-slot:content>
          <div v-if="logTable.length>0?true:false">
          <el-timeline>
            <el-timeline-item
              v-for="(activity, index) in logTable"
              :key="index"
              :icon="eltimelineicon"
              :type="eltimelinetype"
              :size="eltimelinesize"
              :timestamp="activity.createTime"
              >{{ activity.createBy }} {{ activity.remark }}</el-timeline-item
            >
          </el-timeline>
          </div>
          <div v-else>
            <p style="text-align:center;">无相关数据!</p>
          </div>
        </template>        
      </rg-dialog>
    </section>
    <!-- 选择产品页面 -->
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { appSvc } from "@/api/stock/stockmanage";
export default {
  name: "shopasset",
  data() {
    return {
      pickerOptions: {
        disabledDate(time) {
          const date = new Date();
          date.setTime(date.getTime() - 3600 * 1000 * 24);
          return time.getTime() < date;
        }
      },
      dbLoading: false,
      eltimelineicon: "el-icon-more",
      eltimelinetype: "primary",
      eltimelinesize: "large",
      warehouseSel: [],
      statusSel: [
        {
          value: "新建",
          label: "新建"
        },
        {
          value: "盘点中",
          label: "盘点中"
        },
        {
          value: "差异处理中",
          label: "差异处理中"
        },
        {
          value: "盘点完成",
          label: "盘点完成"
        }
      ],
      typeSel: [
        // {
        //   value: "全盘",
        //   label: "全盘"
        // },
        {
          value: "指定产品",
          label: "指定产品"
        }
      ],

      sourceTypeSel: [
        {
          value: "Company",
          label: "平台商品"
        },
        {
          value: "Shop",
          label: "外采商品"
        }
      ],
      tableLoading: false,
      formLabelWidth: "120px",
      condition: {
        pageIndex: 1,
        pageSize: 50,
        times: [],
        status: undefined,
        type: undefined,
        sourceType: undefined,
        warehouseId: undefined
      },
      showplantime: true,
      tableData: [],
      totalList: 0,
      formVisible: false,
      formTitle: "详情",
      formCheck: false,
      btnSaveLoading: false,

      storagePlans: [],

      formInit: {
        warehouseId :undefined,
        planName: undefined,
        type: 1,
        planTime: undefined,
        storagePlans: [],
        sourceType: undefined
      },
      formModel: {
        warehouseId :undefined,
        planName: undefined,
        type: 1, //默认指定产品
        planTime: undefined,
        storagePlans: [],
        sourceType: undefined
      },
      planProductModel: {
        productId: undefined,
        productName: undefined,
        productType: undefined
      },

      planProductModelInit: {
        productId: undefined,
        productName: undefined,
        productType: undefined
      },
      stockItemModel: {
        productId: undefined,
        productName: undefined,
        productType: undefined
      },

      stockItemModelInit: {
        productId: undefined,
        productName: undefined,
        productType: undefined
      },
      rules: {
        planName: [
          { required: true, message: "请输入盘点名称", trigger: "blur" }
        ],
        warehouseId: [{ required: true, message: "请选择仓库", trigger: "blur" }],
        type: [{ required: true, message: "请选择盘点类型", trigger: "blur" }],
        sourceType: [{ required: true, message: "请选择商品类型", trigger: "blur" }],
        planTime: [
          { required: true, message: "请选择盘点时间", trigger: "blur" }
        ]
      },
      disabledBtn: false, //是否显示撤销按钮
      productformTitle: "",
      productformVisible: false,
      productformModel: {
        key: undefined,
        purchaseType: undefined //1总部  2:非总部
      },

      productformModelInit: {
        key: undefined,
        purchaseType: undefined //1总部  2:非总部
      },

      productTableData: [],
      selectproductTable: [],
      productTabName: "first",
      addproductFlag: true,

      detailformTitle: "",
      detialformVisible: false,
      detailCondition: {
        planId: undefined
      },
      //待盘点
      waitstorageProductTable: [],
      waittotallist: 0,
      waitstorageCondition: {
        pageIndex: 1,
        pageSize: 50,
        planId: undefined,
        warehouseId: undefined,
        status: undefined
      },
      //已盘点
      finishstorageProductTable: [],
      finishtotallist: 0,
      finishstorageCondition: {
        pageIndex: 1,
        pageSize: 50,
        planId: undefined,
        warehouseId: undefined,
        status: undefined
      },

      storageCoditionInit: {
        pageIndex: 1,
        pageSize: 50,
        planId: undefined,
        warehouseId: undefined,
        status: undefined
      },

      storageTabName: "first",

      waitcurrentPage: 1,
      finishcurrentPage: 1,
      cancelProductCondition: {
        id: undefined
      },
      storageProductModel: {
        productId: undefined,
        productName: undefined,
        unit: undefined,
        systemNum: undefined,
        storageNum: undefined,
        diffNum: undefined,
        id: undefined,
        productType: undefined,
        planId: undefined
      },
      storageProductModelInit: {
        productId: undefined,
        productName: undefined,
        unit: undefined,
        systemNum: undefined,
        storageNum: undefined,
        diffNum: undefined,
        id: undefined,
        productType: undefined,
        planId: undefined
      },
      updateStoragePlanProductModel: {
        products: [],
        planId: undefined
      },
      detailFormModel: {
        planName: undefined,
        warehouseName: undefined,
        sourceType: undefined,
        planTime: undefined
      },
      detailPlanCondition: {
        id: undefined
      },
      logCondition: {
        objectId: undefined,
        objectType: undefined
      },
      logformTitle: undefined,
      logformVisible: false,
      logTable: []
    };
  },
  created() {
    this.fetchData();
  },
  watch: {},
  methods: {
    closeLogForm() {
      this.logformVisible = false;
      this.logTable = [];
    },
    viewlog(row) {
      this.logformTitle = "【" + row.planName + "】的操作记录";
      this.logformVisible = true;

      this.logCondition.objectId = row.id;
      this.logCondition.objectType = "Storage";

      appSvc
        .getShopWmsLogs(this.logCondition)
        .then(
          res => {
            this.logTable = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    checkNum(row) {
      var diffNum =
        parseInt(row.systemNum) -
        (row.storageNum != "" && row.storageNum != undefined
          ? parseInt(row.storageNum)
          : 0);
      row.diffNum = Math.abs(diffNum);
    },
    absNum(num) {
      return Math.abs(num);
    },
    waitpageTurning() {
      this.waitstorageCondition.pageIndex = this.waitcurrentPage;
      this.getPlanProductsPage(1);
    },
    waitsizeChange(val) {
      this.waitstorageCondition.pageIndex = this.waitcurrentPage = 1;
      this.waitstorageCondition.pageSize = val;
      this.getPlanProductsPage(1);
    },
    finishpageTurning() {
      this.finishstorageCondition.pageIndex = this.finishcurrentPage;
      this.getPlanProductsPage(2);
    },

    finishsizeChange(val) {
      this.finishstorageCondition.pageIndex = this.finishcurrentPage = 1;
      this.finishstorageCondition.pageSize = val;
      this.getPlanProductsPage(2);
    },
    viewdetailcancel() {
      this.waitstorageProductTable = [];
      this.finishstorageProductTable = [];
      this.detialformVisible = false;
      this.disabledBtn = false;

      this.waitstorageCondition = JSON.parse(
        JSON.stringify(this.storageCoditionInit)
      );

      this.finishstorageCondition = JSON.parse(
        JSON.stringify(this.storageCoditionInit)
      );
    },

    viewStorageData(tab, event) {
      if (tab.name == "first") {
        this.waitstorageProductTable = [];
        this.waitstorageCondition.status = "新建";
        appSvc
          .getStoragePlanProducts(this.waitstorageCondition)
          .then(
            res => {
              let data = res.data;

              var itemData = data.items;
              for (var i = 0; i < itemData.length; i++) {
                var item = itemData[i];
                item.storageNum = undefined; //清空盘库数量
                this.waitstorageProductTable.push(item);
              }

              //this.waitstorageProductTable = data.items;
              this.waittotallist = data.totalItems;
              if (flag) {
                this.$messageSuccess(res.message || "查询成功");
              }
            },
            err => {
              console.error(err);
            }
          )
          .catch(() => {})
          .finally(() => {
            this.tableLoading = false;
          });
      } else if (tab.name == "second") {
        this.finishstorageProductTable = [];
        this.finishstorageCondition.status = "盘点中";
        appSvc
          .getStoragePlanProducts(this.finishstorageCondition)
          .then(
            res => {
              let data = res.data;
              this.finishstorageProductTable = data.items;
              this.finishtotallist = data.totalItems;
              if (flag) {
                this.$messageSuccess(res.message || "查询成功");
              }
            },
            err => {
              console.error(err);
            }
          )
          .catch(() => {})
          .finally(() => {
            this.tableLoading = false;
          });
      }
    },
    getPlanProductsPage(type) {
      if (type == 1) {
        //this.waitstorageCondition.planId
        this.waitstorageCondition.status = "新建";
        appSvc
          .getStoragePlanProducts(this.waitstorageCondition)
          .then(
            res => {
              //先清除
              this.waitstorageProductTable = [];

              let data = res.data;

              var itemData = data.items;
              for (var i = 0; i < itemData.length; i++) {
                var item = itemData[i];
                item.storageNum = undefined; //清空盘库数量
                this.waitstorageProductTable.push(item);
              }

              //this.waitstorageProductTable = data.items;
              this.waittotallist = data.totalItems;
              if (flag) {
                this.$messageSuccess(res.message || "查询成功");
              }
            },
            err => {
              console.error(err);
            }
          )
          .catch(() => {})
          .finally(() => {
            this.tableLoading = false;
          });
      } else if (type == 2) {
        this.finishstorageCondition.status = "盘点中";
        appSvc
          .getStoragePlanProducts(this.finishstorageCondition)
          .then(
            res => {
              let data = res.data;
              this.finishstorageProductTable = data.items;
              this.finishtotallist = data.totalItems;
              if (flag) {
                this.$messageSuccess(res.message || "查询成功");
              }
            },
            err => {
              console.error(err);
            }
          )
          .catch(() => {})
          .finally(() => {
            this.tableLoading = false;
          });
      }
    },

    //暂存数据
    tempSave() {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          if (this.waitstorageProductTable.length > 0) {
            var tempArr = [];

            for (var i = 0; i < this.waitstorageProductTable.length; i++) {
              if (
                this.waitstorageProductTable[i].storageNum != undefined &&
                parseInt(this.waitstorageProductTable[i].storageNum) >= 0
              ) {
                this.storageProductModel.id = this.waitstorageProductTable[
                  i
                ].id;
                this.storageProductModel.storageNum = parseInt(
                  this.waitstorageProductTable[i].storageNum
                );
                this.storageProductModel.systemNum = parseInt(
                  this.waitstorageProductTable[i].systemNum
                );
                this.storageProductModel.planId = this.waitstorageProductTable[
                  i
                ].planId;

                tempArr.push(this.storageProductModel);
                this.storageProductModel = JSON.parse(
                  JSON.stringify(this.storageProductModelInit)
                );
              }
            }

            if (tempArr.length > 0) {
              this.updateStoragePlanProductModel.products = tempArr;
              appSvc
                .updateStoragePlanProduct(this.updateStoragePlanProductModel)
                .then(
                  res => {
                    //当前这列移除掉

                    //重新加载待盘点的记录
                    appSvc
                      .getStoragePlanProducts(this.waitstorageCondition)
                      .then(
                        res => {
                          //先清空数组
                          this.waitstorageProductTable = [];
                          let data = res.data;

                          var itemData = data.items;
                          for (var i = 0; i < itemData.length; i++) {
                            var item = itemData[i];
                            item.storageNum = undefined; //清空盘库数量
                            this.waitstorageProductTable.push(item);
                          }

                          //this.waitstorageProductTable = data.items;
                          this.waittotallist = data.totalItems;
                          if (flag) {
                            this.$messageSuccess(res.message || "查询成功");
                          }
                        },
                        err => {
                          console.error(err);
                        }
                      )
                      .catch(() => {})
                      .finally(() => {
                        this.tableLoading = false;
                      });
                  },
                  error => {
                    console.log(error);
                  }
                )
                .catch(() => {});
            } else {
              this.$message({
                message: "请填写盘点数量!",
                type: "warning"
              });
            }
          } else {
            this.$message({
              message: "请填写盘点数量!",
              type: "warning"
            });
          }
        })
        .catch(error => {
          console.log(error);
        });
    },
    //盘点完成
    saveplanData() {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var tempArr = [];
          if (this.waitstorageProductTable.length > 0) {
            for (var i = 0; i < this.waitstorageProductTable.length; i++) {
              if (
                this.waitstorageProductTable[i].storageNum != undefined &&
                parseInt(this.waitstorageProductTable[i].storageNum) >= 0
              ) {
                this.storageProductModel.id = this.waitstorageProductTable[
                  i
                ].id;
                this.storageProductModel.storageNum = parseInt(
                  this.waitstorageProductTable[i].storageNum
                );
                this.storageProductModel.planId = this.waitstorageProductTable[
                  i
                ].planId;
                this.storageProductModel.systemNum = parseInt(
                  this.waitstorageProductTable[i].systemNum
                );

                tempArr.push(this.storageProductModel);
                this.storageProductModel = JSON.parse(
                  JSON.stringify(this.storageProductModelInit)
                );
              } else {
                this.$message({
                  message:
                    "请填写【" +
                    this.waitstorageProductTable[i].productName +
                    "】的盘点数量!",
                  type: "warning"
                });

                return false;
              }
            }
          }

          var vm = this;
          if (
            this.waitstorageCondition.planId != "" &&
            parseInt(this.waitstorageProductTable.planId) > 0
          ) {
            this.updateStoragePlanProductModel.planId = this.waitstorageCondition.planId;
          } else if (
            this.finishstorageCondition.planId != "" &&
            parseInt(this.finishstorageCondition.planId) > 0
          ) {
            this.updateStoragePlanProductModel.planId = this.finishstorageCondition.planId;
          }

          if (tempArr.length > 0) {
            this.updateStoragePlanProductModel.products = tempArr;
          }

          appSvc
            .updateStoragePlanProductStatus(this.updateStoragePlanProductModel)
            .then(
              res => {
                if (res.code == 10000) {
                  if (res.message != "") {
                    this.$message({
                      message: res.message,
                      type: "warning"
                    });

                    //页面继续刷新

                    //重新加载待盘点的记录
                    appSvc
                      .getStoragePlanProducts(this.waitstorageCondition)
                      .then(
                        res => {
                          //先清空数组
                          this.waitstorageProductTable = [];
                          let data = res.data;

                          var itemData = data.items;
                          for (var i = 0; i < itemData.length; i++) {
                            var item = itemData[i];
                            item.storageNum = undefined; //清空盘库数量
                            this.waitstorageProductTable.push(item);
                          }

                          //this.waitstorageProductTable = data.items;
                          this.waittotallist = data.totalItems;
                          if (flag) {
                            this.$messageSuccess(res.message || "查询成功");
                          }
                        },
                        err => {
                          console.error(err);
                        }
                      )
                      .catch(() => {})
                      .finally(() => {
                        this.tableLoading = false;
                      });
                  } else {
                    vm.search();
                    vm.viewdetailcancel();
                  }
                }
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
    changetype() {
      debugger;
      if (this.formModel.type == 1) {
        this.addproductFlag = true;
      } else if (this.formModel.type == 2) {
        this.addproductFlag = false;
      }
    },

    handleClick() {},

    saveProduct() {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          if (this.$refs.multipleTable.selection.length > 0) {
            for (
              var i = 0;
              i < this.$refs.multipleTable.selection.length;
              i++
            ) {
              var productCode = this.$refs.multipleTable.selection[i]
                .productCode;
              var productName = this.$refs.multipleTable.selection[i]
                .productName;

              //格式化数据

              this.stockItemModel.productId = productCode;
              this.stockItemModel.productName = productName;

              // if (this.formModel.sourceType == "Company") {
              //   this.stockItemModel.productType = "总部产品";
              // } else if (this.formModel.sourceType == "Shop") {
              //   this.stockItemModel.productType = "其他产品";
              // }
              this.stockItemModel.productType = this.formModel.sourceType;

              this.selectproductTable.push(this.stockItemModel);

              this.stockItemModel = JSON.parse(
                JSON.stringify(this.stockItemModelInit)
              );
            }

            this.productcancel();
          } else {
            this.$message({
              message: "请选择产品!",
              type: "warning"
            });
          }
        })
        .catch(error => {
          console.log(error);
        });
    },

    productcancel() {
      this.productformVisible = false;
      this.productformModel.productId = undefined;
      this.$refs.multipleTable.clearSelection();
      this.productTableData = [];

      this.productformModel = JSON.parse(
        JSON.stringify(this.productformModelInit)
      );
    },

    selectProduct(type) {
      if (
        this.formModel.sourceType != "" &&
        this.formModel.sourceType != undefined
      ) {
        this.productformTitle = "选择产品";
        this.productformVisible = true;
      } else {
        this.$message({
          message: "请选择类型!",
          type: "warning"
        });
        return false;
      }
    },
    removeproduct(row, index, tables) {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.cancelProductCondition.id = row.id;
          appSvc
            .cancelStoragePlanProduct(this.cancelProductCondition)
            .then(
              res => {
                //当前这列移除掉
                tables.splice(index, 1);

                this.finishtotallist -= 1; //减去总数
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
    searchproduct() {
      if (this.formModel.sourceType == "Company") {
        this.productformModel.purchaseType = 1;
      } else if (this.formModel.sourceType == "Shop") {
        this.productformModel.purchaseType = 2;
      }
      this.dbLoading = true;
      appSvc
        .getShopProducts(this.productformModel)
        .then(
          res => {
            this.productTableData = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.dbLoading = false;
        });
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

    removeItem(index, rows) {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          rows.splice(index, 1); //删掉该行
        })
        .catch(error => {
          console.log(error);
        });
    },
    viewplan(row) {
      this.detailformTitle = "盘库明细";
      this.detialformVisible = true;

      this.detailPlanCondition.id = row.id;
      //需要展示明细信息
      appSvc
        .getStoragePlan(this.detailPlanCondition)
        .then(
          res => {
            this.detailFormModel = res.data;
          },
          err => {
            console.error(err);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });

      //查看详情时两个model都需要赋值
      this.waitstorageCondition.planId = row.id;
      this.waitstorageCondition.warehouseId = row.warehouseId;
      this.finishstorageCondition.planId = row.id;
      this.finishstorageCondition.warehouseId = row.warehouseId;

      if (row.status == "新建") {
        this.storageTabName = "first";
        this.waitstorageCondition.planId = row.id;
        this.waitstorageCondition.status = "新建";

        //数量全部置空

        appSvc
          .getStoragePlanProducts(this.waitstorageCondition)
          .then(
            res => {
              let data = res.data;
              var itemData = data.items;
              for (var i = 0; i < itemData.length; i++) {
                var item = itemData[i];
                item.storageNum = undefined; //清空盘库数量
                this.waitstorageProductTable.push(item);
              }
              //  this.waitstorageProductTable = data.items;
              this.waittotallist = data.totalItems;
              if (flag) {
                this.$messageSuccess(res.message || "查询成功");
              }
            },
            err => {
              console.error(err);
            }
          )
          .catch(() => {})
          .finally(() => {
            this.tableLoading = false;
          });
      } else {
        this.storageTabName = "second";
        this.finishstorageCondition.planId = row.id;
        this.finishstorageCondition.status = "盘点中";

        if (row.status == "盘点中" || row.status == "新建") {
          this.disabledBtn = false;
        } else {
          this.disabledBtn = true;
        }

        appSvc
          .getStoragePlanProducts(this.finishstorageCondition)
          .then(
            res => {
              let data = res.data;
              this.finishstorageProductTable = data.items;
              this.finishtotallist = data.totalItems;
              if (flag) {
                this.$messageSuccess(res.message || "查询成功");
              }
            },
            err => {
              console.error(err);
            }
          )
          .catch(() => {})
          .finally(() => {
            this.tableLoading = false;
          });
      }
    },

    //添加计划
    addplan() {
      this.formTitle = "新增盘点单";
      this.formVisible = true;
    },

    fetchData() {
      this.getwarehouses();
      this.getPaged();
    },

    getwarehouses() {
      appSvc
        .GetShopWareHouseList()
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
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    getPaged(flag) {
      // let data = JSON.parse(JSON.stringify(this.condition));
      this.tableLoading = true;
      appSvc
        .getStoragePlans(this.condition)
        .then(
          res => {
            let data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (flag) {
              this.$messageSuccess(res.message || "查询成功");
            }
          },
          err => {
            console.error(err);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });
    },
    search(flag) {
      this.condition.pageIndex = 1;
      this.getPaged(flag);
    },

    save(formName) {
      if (
        this.formModel.sourceType != "" &&
        this.formModel.sourceType != undefined
      ) {
      } else {
        this.$message({
          message: "请选择商品类型!",
          type: "warning"
        });
        return false;
      }

      var msg = "";
      if (this.formModel.type == 1) {
        msg = "确定操作吗?";
      } else if (this.formModel.type == 2) {
        msg = "确定操作全盘吗？(请三思！)";
      }

      this.$confirm(msg, "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          this.$refs[formName].validate(valid => {
            if (valid) {
              debugger;
              vm.btnSaveLoading = true;

              var tempProductArr = [];

              var type = "";
              if (this.formModel.type == 1) {
                type = "指定产品";
              } else if (this.formModel.type == 2) {
                type = "全盘";
              }

              this.formModel.type = type;
              var warehouse = this.warehouseSel.find(
                r => r.id === this.formModel.warehouseId
              );
              this.formModel.warehouseName = warehouse.simpleName;

              if (this.selectproductTable.length > 0) {
                for (var i = 0; i < this.selectproductTable.length; i++) {
                  this.planProductModel.productId = this.selectproductTable[
                    i
                  ].productId;
                  this.planProductModel.productName = this.selectproductTable[
                    i
                  ].productName;
                  this.planProductModel.productType = this.selectproductTable[
                    i
                  ].productType;
                  tempProductArr.push(this.planProductModel);
                  this.planProductModel = JSON.parse(
                    JSON.stringify(this.planProductModelInit)
                  );
                }
              }

              if (
                (tempProductArr.length > 0 && type == "指定产品") ||
                type == "全盘"
              ) {
                this.formModel.storagePlans = tempProductArr;
                appSvc
                  .createStoragePlan(this.formModel)
                  .then(
                    res => {
                      if (res.code == 10000) {
                        this.$message({
                          message: "操作成功",
                          type: "success"
                        });
                        vm.search();

                        vm.cancel();
                      }
                    },
                    err => {}
                  )
                  .catch(err => {
                    console.error(err);
                  })
                  .finally(() => {
                    vm.btnSaveLoading = false;
                  });
              } else {
                this.$message({
                  message: "请选择产品!",
                  type: "warning"
                });
              }
            } else {
              return false;
            }
          });
        })
        .catch(error => {
          console.log(error);
        });
    },

    cancel(formName) {
      this.formVisible = false;
      this.formCheck = false;

      this.resetForm();
      this.selectproductTable = [];

      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },
    resetForm() {
      this.formModel = JSON.parse(JSON.stringify(this.formInit));
    },
    resetSel() {}
  }
};
</script>
