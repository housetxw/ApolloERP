<template>
  <main class="container" v-loading="dbLoading">
    <!-- 首页：列表数据 -->
    <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :tableData="tableData"
      :pageChange="pageChange"
      :searching="search"
      :showRowIndex="false"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-select
            v-model="condition.shopId"
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
          <el-input
            v-model="condition.productId"
            style="width:200px"
            autocomplete="off"
            placeholder="请输入产品名称或编码"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item label>
          <el-input
            v-model="condition.stockId"
            style="width:160px"
            autocomplete="off"
            placeholder="请输入出入库编号"
            clearable
          ></el-input>
        </el-form-item>
        <el-form-item label>
          <el-input
            v-model="condition.sourceInventoryNo"
            style="width:160px"
            autocomplete="off"
            placeholder="请输入来源单号"
            clearable
          ></el-input>
        </el-form-item>

        <el-form-item label>
          <el-select
            v-model="condition.operationType"
            placeholder="请选择类型"
            style="width:120px"
            clearable
            @change="selectSourceTypeSel()"
          >
            <el-option
              v-for="item in typeSel"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label>
          <el-select
            v-model="condition.sourceType"
            placeholder="请选择来源"
            style="width:120px"
            clearable
          >
            <el-option
              v-for="item in sourcetypeSel"
              :key="item.key"
              :label="item.value"
              :value="item.key"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label>
          <el-tooltip
            class="item"
            effect="dark"
            content="出\入库日期范围"
            placement="bottom"
          >
            <el-date-picker
              v-model="condition.times"
              type="daterange"
              :picker-options="$root.$data.timeRgPickerOpt"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
              align="center"
              style="width:210px"
            ></el-date-picker>
          </el-tooltip>
        </el-form-item>
      </template>
      <template v-slot:header_btn>
        <!-- <el-button
          type="primary"
          size="mini"
          @click="createOutStock()"
          icon="el-icon-plus"
          >出库</el-button
        >
        <el-button
          type="success"
          size="mini"
          @click="createInStock()"
          icon="el-icon-plus"
          >入库</el-button
        > -->
      </template>

      <!-- 搜索-->

      <!-- 列表 -->
      <template v-slot:tb_cols>
        <!-- Table数据 -->
        <el-table-column label="仓库名称"  prop="locationName"></el-table-column>
        <rg-table-column label="出入库编号" prop="stockId" />
        <el-table-column
          label="操作类型"
          prop="type"
          width="150"
        ></el-table-column>
        <rg-table-column
          label="关联单号"
          prop="sourceInventoryNo"
        ></rg-table-column>
        <el-table-column
          label="状态"
          prop="status"
          width="100"
        ></el-table-column>
        <el-table-column
          label="备注"
          prop="remark"
          width="100"
        ></el-table-column>
        <el-table-column
          label="操作人"
          prop="createBy"
          width="100"
        ></el-table-column>
        <el-table-column
          label="操作时间"
          align="center"
          prop="createTime"
          width="120"
        >
          <template slot-scope="scope">{{
            scope.row.createTime.substr(0, 16)
          }}</template>
        </el-table-column>
        <el-table-column label="操作" fixed="right" align="center" width="180">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                type="primary"
                style="margin-right:5px;"
                v-show="
                  scope.row.status == '新建' &&
                  scope.row.sourceType == '调拨出库'
                    ? true
                    : false
                "
                @click="sendout(scope.row)"
                size="mini"
                >发出</el-button
              >
              <el-button
                type="primary"
                @click="viewstockinoutDetail(scope.row)"
                size="mini"
                >查看明细</el-button
              >
            </el-button-group>
          </template>
        </el-table-column>
        <!-- Table数据 -->
      </template>
      <!-- 列表 -->
    </rg-page>
    <!-- 首页：列表数据 -->

    <!-- 新增入库单页面 -->
    <section id="instockPage">
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
        width="90%"
        v-if="formVisible"
        maxWidth="1024px"
        minWidth="900px"
      >
        <template v-slot:content>
          <el-form :model="formModel" :rules="rules" ref="formModel">
            <el-form-item
              label="入库时间"
              :label-width="formLabelWidth"
              prop="operateTime"
            >
              <label v-if="viewBtn">{{
                $jscom.toYMDHm(formModel.operateTime)
              }}</label>
              <el-date-picker
                v-else
                v-model="formModel.operateTime"
                type="datetime"
                size="mini"
                :picker-options="pickerOptions"
                placeholder="选择日期时间"
                default-time="12:00:00"
                :disabled="viewBtn"
              ></el-date-picker>
            </el-form-item>
            <el-form-item
              label="入库类型"
              :label-width="formLabelWidth"
              prop="sourceType"
            >
              <template v-if="viewBtn">
                <label>{{ formModel.sourceType }}</label>
                <label v-show="colShowFlag">{{
                  formModel.sourceInventoryNo
                }}</label>
              </template>
              <template v-else>
                <el-select
                  v-model="formModel.sourceType"
                  size="mini"
                  :disabled="viewBtn"
                  placeholder="请选择类型"
                  style="width:220px"
                  @change="selectSourceType()"
                >
                  <el-option
                    v-for="item in instocktypeSel"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  ></el-option>
                </el-select>
                <el-input
                  v-show="colShowFlag"
                  size="mini"
                  v-model="formModel.sourceInventoryNo"
                  autocomplete="off"
                  clearable
                  style="width:220px;"
                  placeholder="请输入采购单号"
                  :disabled="viewBtn"
                ></el-input>
                <el-button
                  v-show="colShowFlag"
                  type="warning"
                  :disabled="viewBtn"
                  size="mini"
                  @click="getpurchaseOrderDetail()"
                  >查询</el-button
                >
              </template>
            </el-form-item>
            <el-form-item label="入库经办" :label-width="formLabelWidth">
              <label>{{ formModel.createBy }} {{ formModel.createTime }}</label>
            </el-form-item>
            <el-form-item label="备注" :label-width="formLabelWidth">
              <label>{{ formModel.remark }} </label>
            </el-form-item>
            <el-form-item v-if="!colShowFlag && !viewBtn">
              <el-button
                :disabled="viewBtn"
                type="primary"
                size="mini"
                @click="selectProduct()"
                >选择产品</el-button
              >
            </el-form-item>
          </el-form>

          <el-table
            border
            :data="stockTempTable"
            :height="300"
            stripe
            size="mini"
            style="width: 100%"
          >
            <el-table-column
              v-if="colShowFlag"
              :key="1"
              label="明细号"
              width="60"
              prop="releationId"
            ></el-table-column>
            <rg-table-column
              label="产品编号"
              :key="2"
              prop="productId"
            ></rg-table-column>
            <rg-table-column
              label="产品名称"
              :key="3"
              prop="productName"
            ></rg-table-column>
            <!-- <el-table-column v-if="colShowFlag" label="采购数量" prop="num"></el-table-column> -->
            <el-table-column
              v-if="viewshowFlag"
              :key="4"
              label="应入"
              align="right"
              width="85px"
              prop="num"
            ></el-table-column>
            <el-table-column
              v-if="viewshowFlag"
              :key="5"
              label="已入"
              align="right"
              width="85"
              prop="actualNum"
            ></el-table-column>
            <el-table-column
              v-if="colShowFlag"
              :key="6"
              label="良品"
              width="95"
            >
              <template slot-scope="scope">
                <el-input
                  placeholder="0"
                  size="mini"
                  v-model="scope.row.goodNum"
                  oninput="value=value.replace(/[^\d.]/g,'')"
                  @blur="checkNum(scope.row, 1)"
                  :disabled="viewBtn"
                  clearable
                ></el-input>
              </template>
            </el-table-column>
            <el-table-column
              v-if="colShowFlag"
              :key="7"
              label="不良品数"
              width="95"
              prop="diffNum"
            >
              <template slot-scope="scope">
                <el-input
                  size="mini"
                  placeholder="0"
                  v-model="scope.row.badNum"
                  oninput="value=value.replace(/[^\d.]/g,'')"
                  @blur="checkNum(scope.row, 2)"
                  :disabled="viewBtn"
                  clearable
                ></el-input>
              </template>
            </el-table-column>

            <el-table-column
              v-if="instockNumFlag"
              :key="8"
              align="right"
              width="95"
              label="入库数量"
            >
              <template slot-scope="scope">
                <!-- <el-input
                      placeholder="请输入数量"
                      v-model="scope.row.tempnum"
                      oninput="value=value.replace(/[^\d.]/g,'')"
                      clearable
                ></el-input>-->
                <div v-if="scope.row.status == '已收货'">
                  <label>{{ scope.row.num }}</label>
                </div>
                <div v-else>
                  <el-input
                    placeholder="0"
                    v-model="scope.row.tempnum"
                    oninput="value=value.replace(/[^\d.]/g,'')"
                    clearable
                  ></el-input>
                </div>
              </template>
            </el-table-column>
            <el-table-column
              :key="9"
              v-if="viewshowFlag"
              label="差异数"
              align="right"
              width="95px"
              prop="diffNum"
            ></el-table-column>
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            :disabled="viewBtn"
            type="primary"
            @click="save('formModel')"
            >保存</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 新增入库单页面 -->

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
          >
            <el-form-item>
              <el-input
                size="mini"
                placeholder="请输入产品名称"
                v-model="productformModel.key"
                autocomplete="off"
                clearable
              ></el-input>
            </el-form-item>

            <el-form-item>
              <el-select
                v-model="productformModel.tempType"
                size="mini"
                placeholder="请选择类型"
                style="width:120px"
              >
                <el-option
                  v-for="item in productTypeSel"
                  :key="item.value"
                  :label="item.label"
                  :value="item.value"
                ></el-option>
              </el-select>
            </el-form-item>
            <el-form-item>
              <el-button
                icon="el-icon-search"
                size="mini"
                type="warning"
                @click="searchproduct()"
                >检索</el-button
              >
            </el-form-item>
          </el-form>

          <el-table
            border
            ref="multipleTable"
            :height="400"
            stripe
            size="mini"
            @selection-change="handleSelectionChange"
            :data="productTableData"
            style="width: 100%"
          >
            <el-table-column type="selection" align="center"></el-table-column>
            <el-table-column
              label="产品编号"
              prop="productCode"
            ></el-table-column>
            <el-table-column
              label="产品名称"
              prop="productName"
            ></el-table-column>
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

    <!-- 新增出库单页面 -->
    <section id="outstockPage">
      <rg-dialog
        :title="outformTitle"
        :visible.sync="outformVisible"
        :beforeClose="outcancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            outcancel('outformModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="90%"
        v-if="outformVisible"
        maxWidth="1024px"
        minWidth="900px"
      >
        <template v-slot:content>
          <el-form :model="outformModel" :rules="rules1" ref="outformModel">
            <el-form-item
              label="出库时间"
              :label-width="formLabelWidth"
              prop="operateTime"
            >
              <label v-if="viewBtn">{{
                $jscom.toYMDHm(outformModel.operateTime)
              }}</label>
              <el-date-picker
                v-else
                v-model="outformModel.operateTime"
                size="mini"
                type="datetime"
                :picker-options="pickerOptions"
                placeholder="选择日期时间"
                default-time="12:00:00"
                :disabled="viewBtn"
              ></el-date-picker>
            </el-form-item>
            <el-form-item
              label="出库类型"
              :label-width="formLabelWidth"
              prop="sourceType"
            >
              <template v-if="viewBtn">
                <label>{{ outformModel.sourceType }}</label>
                <label v-show="outColShopFlag">{{
                  outformModel.sourceInventoryNo
                }}</label>
              </template>
              <template v-else>
                <el-select
                  v-model="outformModel.sourceType"
                  size="mini"
                  placeholder="请选择出库类型"
                  style="width:220px"
                  :disabled="viewBtn"
                  @change="selectoutSourceType()"
                >
                  <el-option
                    v-for="item in outstocktypeSel"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                  ></el-option>
                </el-select>
                <el-input
                  v-show="outColShopFlag"
                  size="mini"
                  v-model="outformModel.sourceInventoryNo"
                  autocomplete="off"
                  clearable
                  style="width:220px;"
                  placeholder="请输入关联单号"
                  :disabled="viewBtn"
                ></el-input
                >&nbsp;&nbsp;
                <el-button
                  type="warning"
                  size="mini"
                  v-show="outColShopFlag"
                  :disabled="searchPurchaseOrder"
                  @click="getInStockDetail()"
                  >查询</el-button
                >
              </template>
            </el-form-item>
            <el-form-item label="出库说明" :label-width="formLabelWidth">
              <label v-if="viewBtn">{{ outformModel.remark }}</label>
              <el-input
                v-else
                size="mini"
                v-model="outformModel.remark"
                autocomplete="off"
                clearable
                style="width:220px;"
                placeholder="请填写出库说明"
                :disabled="viewBtn"
              ></el-input>
            </el-form-item>

            <el-form-item
              label="领用人"
              :label-width="formLabelWidth"
              prop="operator"
            >
              <label v-if="viewBtn">{{ outformModel.operator }}</label>
              <el-input
                v-else
                size="mini"
                v-model="outformModel.operator"
                autocomplete="off"
                clearable
                style="width:220px;"
                placeholder="请输入领用人"
                :disabled="viewBtn"
              ></el-input>
            </el-form-item>
            <el-form-item label="出库经办" :label-width="formLabelWidth">
              <label
                >{{ outformModel.createBy }}
                {{ outformModel.createTime }}</label
              >
            </el-form-item>
            <el-form-item v-if="disabledBtn">
              <el-button
                type="primary"
                size="mini"
                :disabled="!disabledBtn"
                @click="selectProduct()"
                >选择产品</el-button
              >
            </el-form-item>
          </el-form>

          <el-table
            border
            :data="stockoutTempTable"
            style="width: 100%"
            stripe
            element-loading-text="加载中..."
            size="mini"
            height="300"
          >
            <rg-table-column label="产品编号" prop="productId" />
            <rg-table-column
              label="产品名称"
              prop="productName"
            ></rg-table-column>
            <el-table-column
              label="批次"
              align="center"
              prop="batchNo"
              width="90"
            ></el-table-column>
            <el-table-column
              label="库存数量"
              v-if="coloutflag"
              align="right"
              prop="availableNum"
              width="90"
            ></el-table-column>
            <el-table-column label="领取数量" align="center" width="100">
              <template slot-scope="scope">
                <el-input
                  placeholder="0"
                  size="mini"
                  v-model="scope.row.num"
                  :disabled="viewBtn"
                  oninput="value=value.replace(/[^\d.]/g,'')"
                  @blur="checkNum(scope.row, 3)"
                  clearable
                ></el-input>
              </template>
            </el-table-column>
            <el-table-column label="操作" align="center" width="70">
              <template slot-scope="scope">
                <el-button
                  type="danger"
                  size="mini"
                  :disabled="viewBtn"
                  @click="removeItem(scope.$index, stockoutTempTable)"
                  >移除</el-button
                >
              </template>
            </el-table-column>
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            :disabled="viewBtn"
            @click="outsave('outformModel')"
            >保存</el-button
          >
        </template>
      </rg-dialog>
    </section>
    <!-- 新增入库单页面 -->
  </main>
</template>

<script>
import { appSvc } from "@/api/stock/stockmanage";
import { Loading } from "element-ui";
export default {
  name: "demopage",
  data() {
    return {
      dbLoading: false,
      headerBtnWidth: 300,
      pickerOptions: {
        disabledDate(time) {
          const date = new Date();
          date.setTime(date.getTime() - 3600 * 1000 * 24);
          return time.getTime() < date;
        }
      },
      w_search_right: 100,
      tableLoading: false,

      getPurchaseOrderCondition: {
        sourceInventoryNo: undefined
      },
      sourcetypeSel: [],
      condition: {
        shopId: undefined,
        productId: undefined,
        stockId: undefined,
        operationType: undefined,
        times: [],
        pageIndex: 1,
        pageSize: 50,
        sourceType: undefined,
        sourceInventoryNo:undefined,
      },
      warehouseSel: [],
      tableData: [],
      totalList: 0,
      typeSel: [
        {
          key: "全部",
          value: "全部"
        },
        {
          key: "入库",
          value: "入库"
        },
        {
          key: "出库",
          value: "出库"
        }
      ],
      formVisible: false,
      formTitle: "详情",
      formCheck: false,
      btnSaveLoading: false,
      formLabelWidth: "100px",
      formInit: {
        locationName: undefined,
        operateTime: undefined,
        sourceType: "采购入库", //设置默认值
        sourceInventoryNo: undefined,
        operator: undefined,
        stockItems: [],
        remark: undefined,
        createBy: undefined
      },
      formModel: {
        locationName: undefined,
        operateTime: undefined,
        sourceType: "采购入库", //设置默认值
        sourceInventoryNo: undefined,
        operator: undefined,
        stockItems: [],
        remark: undefined,
        createBy: undefined
      },

      stockTempTable: [],
      stockItemModel: {
        releationId: undefined,
        productId: undefined,
        productName: undefined,
        num: undefined,
        actualNum: undefined,
        goodNum: undefined,
        badNum: undefined,
        diffNum: undefined,
        cost: undefined,
        tempnum: undefined,
        status: undefined,
        batchNo: undefined,
        inoutId: undefined,
        id: undefined
      },

      stockItemModelInit: {
        releationId: undefined,
        productId: undefined,
        productName: undefined,
        num: undefined,
        actualNum: undefined,
        goodNum: undefined,
        badNum: undefined,
        diffNum: undefined,
        cost: undefined,
        tempnum: undefined,
        status: undefined,
        batchNo: undefined,
        inoutId: undefined,
        id: undefined
      },

      rules: {
        operateTime: [
          { required: true, message: "请选择入库时间", trigger: "blur" }
        ],
        sourceType: [
          { required: true, message: "请选择入库类型", trigger: "blur" }
        ],
        sourceInventoryNo: [
          { required: true, message: "请输入采购单号", trigger: "blur" }
        ]
      },
      rules1: {
        operateTime: [
          { required: true, message: "请选择入库时间", trigger: "blur" }
        ],
        sourceType: [
          { required: true, message: "请选择入库类型", trigger: "blur" }
        ],
        sourceInventoryNo: [
          { required: true, message: "请输入采购单号", trigger: "blur" }
        ],
        operator: [{ required: true, message: "请输入领用人", trigger: "blur" }]
      },

      instocktypeSel: [
        {
          key: "采购入库",
          value: "采购入库"
        },
        {
          key: "其他入库",
          value: "其他入库"
        }
      ],
      outstocktypeSel: [
        {
          key: "采购退货",
          value: "采购退货"
        },
        {
          key: "订单安装",
          value: "订单安装"
        },
        {
          key: "货损出库",
          value: "货损出库"
        },
        {
          key: "其他出库",
          value: "其他出库"
        }
      ],

      colShowFlag: true, //入库控制控件v-show属性
      outColShopFlag: true, //出库控制控件v-show属性
      disabledBtn: false, //控制选择产品按钮
      productformTitle: "",
      productformVisible: false,
      productformModel: {
        key: undefined,
        purchaseType: undefined,
        tempType: undefined
      },
      productformModelInit: {
        key: undefined,
        purchaseType: undefined,
        tempType: undefined
      },

      productTypeSel: [
        {
          key: "总部供应链",
          value: "总部供应链"
        },
        {
          key: "非总部供应链",
          value: "非总部供应链"
        }
      ],

      viewBtn: false, //控制保存按钮
      coloutflag: true, //控制库存数量是否展示
      viewshowFlag: true, //控制采购入库查看时 应入 已入是否展示
      instockNumFlag: false, //控制入库数量flag显示  其他入库使用
      productTableData: [],
      searchPurchaseOrder: false,
      outformTitle: "",

      outformVisible: false,
      outformModel: {
        locationName: undefined,
        operateTime: undefined,
        sourceType: "采购退货", //设置默认值
        sourceInventoryNo: undefined,
        operator: undefined,
        stockItems: [],
        remark: undefined,
        createBy: undefined,
        stockoutflag: undefined //标识位
      },
      outformModelInit: {
        locationName: undefined,
        operateTime: undefined,
        sourceType: "采购退货", //设置默认值
        sourceInventoryNo: undefined,
        operator: undefined,
        stockItems: [],
        remark: undefined,
        createBy: undefined,
        stockoutflag: undefined //标识位
      },
      stockoutTempTable: [],
      outStockCondition: {
        shopId: undefined,
        pIds: [],
        objectId: undefined,
        objectType: undefined
      },

      stockItemTempArr: [],
      stockinoutCondition: {
        sourceInventoryNo: undefined,
        operationType: undefined,
        stockId: undefined
      },
      stockoutcondition: {
        stockId: undefined
      }
    };
  },
  created() {
    this.fetchData();
  },
  watch: {},
  methods: {
    selectSourceTypeSel() {
      if (
        this.condition.operationType != undefined &&
        this.condition.operationType != "" &&
        this.condition.operationType != "全部"
      ) {
        if (this.condition.operationType == "入库") {
          appSvc
            .getStockInTypes()
            .then(
              res => {
                this.sourcetypeSel = res.data;
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        } else if (this.condition.operationType == "出库") {
          appSvc
            .getStockOutTypes()
            .then(
              res => {
                this.sourcetypeSel = res.data;
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        }
      } else {
        this.sourcetypeSel = [];
      }
    },

    //发出
    sendout(row) {
      debugger;
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          const loading = this.$loading({
            lock: true,
            text: "Loading",
            spinner: "el-icon-loading",
            background: "rgba(0, 0, 0, 0.7)"
          });

          var vm = this;
          this.stockoutcondition.stockId = row.stockId;
          appSvc
            .stockInOutTaskDelivery(this.stockoutcondition)
            .then(
              res => {
                //系统需要做判断
                if (res.code == "10000") {
                  //从门店发出成功了
                  if (res.message == "success") {
                    vm.search();
                  } else {
                    this.$alert(res.message, "提示", {
                      confirmButtonText: "确定",
                      callback: action => {}
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
              loading.close();
            });
        })
        .catch(error => {
          console.log(error);
        });
    },

    viewstockinoutDetail(row) {
      // this.stockinoutCondition.sourceInventoryNo = row.sourceInventoryNo;
      // this.stockinoutCondition.operationType = row.operationType;
      this.stockinoutCondition.stockId = row.stockId;

      if (row.operationType == "入库") {
        this.viewBtn = true;
        appSvc
          .getStockInOutInfo(this.stockinoutCondition)
          .then(
            res => {
              //放到不同的Table中
              this.formModel = res.data;

              // if (this.formModel.sourceType == "采购入库") {
              //   this.colShowFlag = true;
              //   this.viewshowFlag = false;
              //   this.stockTempTable = res.data.stockItems;
              //   this.instockNumFlag = false;
              // } else if (
              //   this.formModel.sourceType == "其他入库" ||
              //   this.formModel.sourceType == "盘盈入库" ||
              //   this.formModel.sourceType == "铺货入库" ||
              //   this.formModel.sourceType == "调拨入库"
              // ) {
              //   this.colShowFlag = false;
              //   this.viewshowFlag = false;
              //   this.stockTempTable = res.data.stockItems;
              //   this.instockNumFlag = true;
              // }


                this.colShowFlag = false;
                this.viewshowFlag = true;
                this.stockTempTable = res.data.stockItems;
                this.instockNumFlag = false;

              this.formVisible = true;
              this.formTitle = row.type; //"入库";
              // this.colShowFlag = true;
              // this.stockTempTable = res.data.stockItems;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      } else if (row.operationType == "出库") {
        this.viewBtn = true;
        this.searchPurchaseOrder = true;
        this.coloutflag = false;
        appSvc
          .getStockInOutInfo(this.stockinoutCondition)
          .then(
            res => {
              //放到不同的Table中
              this.outformModel = res.data;
              this.outformVisible = true;
              this.outformTitle = row.type;
              //this.outformTitle = "出库";
              this.stockoutTempTable = res.data.stockItems;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      }
    },

    //核对入库出库的数量
    checkNum(row, type) {
      if (type == 1) {
        if (
          parseInt(row.goodNum) +
            parseInt(row.actualNum) +
            parseInt(row.badNum) >
          parseInt(row.num)
        ) {
          row.goodNum = 0;
          this.$message({
            message: "入库数不能大于采购数量!",
            type: "warning"
          });
        } else {
          row.diffNum =
            parseInt(row.num) -
            (parseInt(row.goodNum) +
              parseInt(row.actualNum) +
              parseInt(row.badNum));
        }
      } else if (type == 2) {
        if (
          parseInt(row.goodNum) +
            parseInt(row.actualNum) +
            parseInt(row.badNum) >
          parseInt(row.num)
        ) {
          row.badNum = 0;
          this.$message({
            message: "不良品数不能大于采购数量!",
            type: "warning"
          });
        } else {
          row.diffNum =
            parseInt(row.num) -
            (parseInt(row.goodNum) +
              parseInt(row.actualNum) +
              parseInt(row.badNum));
        }
      } else if (type == 3) {
        if (row.num > row.availableNum) {
          row.num = 0;
          this.$message({
            message: "领取数量不能大于库存数量!",
            type: "warning"
          });
        }
      }
    },
    removeItem(index, rows) {
      //移除一行

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

    getInStockDetail() {
      if (
        this.outformModel.sourceInventoryNo != "" &&
        this.outformModel.sourceInventoryNo != undefined
      ) {
        this.outStockCondition.objectId = this.outformModel.sourceInventoryNo;
        this.outStockCondition.objectType = "采购入库";
        appSvc
          .getStockInoutItems(this.outStockCondition)
          .then(
            res => {
              this.stockoutTempTable = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      } else {
        this.$message({
          message: "请输入采购单号!",
          type: "warning"
        });
      }
    },

    createOutStock() {
      this.outformVisible = true;
      this.outformTitle = "产品出库";
      this.outformModel.stockoutflag = true;
      appSvc
        .getCurrentUser()
        .then(
          res => {
            //  this.tableData = res.data;
            this.outformModel.createBy = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    selectoutSourceType() {
      if (this.outformModel.sourceType != "") {
        if (
          this.outformModel.sourceType == "采购退货" ||
          this.outformModel.sourceType == "订单安装"
        ) {
          this.outColShopFlag = true;
        } else {
          this.outColShopFlag = false;
        }

        if (this.outformModel.sourceType == "采购退货") {
          this.searchPurchaseOrder = false;
          this.disabledBtn = false;
        } else {
          this.searchPurchaseOrder = true;
          this.disabledBtn = true;
        }
        this.stockoutTempTable = [];
      }
    },
    outcancel() {
      this.outformVisible = false;
      this.stockoutTempTable = [];
      this.outformModel = JSON.parse(JSON.stringify(this.outformModelInit));
      this.stockItemTempArr = [];
      this.viewBtn = false;
      this.searchPurchaseOrder = false;
      this.coloutflag = true;
      this.stockItemModel = JSON.parse(JSON.stringify(this.stockItemModelInit));
    },

    outsave(formName) {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          this.$refs[formName].validate(valid => {
            if (valid) {
              if (
                this.outformModel.sourceType == "其他" &&
                (this.outformModel.remark == "" ||
                  this.outformModel.remark == undefined)
              ) {
                this.$message({
                  message: "请填写出库说明!",
                  type: "warning"
                });
                return;
              }
              vm.btnSaveLoading = true;

              if (this.stockoutTempTable.length > 0) {
                for (var i = 0; i < this.stockoutTempTable.length; i++) {
                  //领取数量
                  if (parseInt(this.stockoutTempTable[i].num) > 0) {
                    this.stockItemModel.releationId = this.stockoutTempTable[
                      i
                    ].releationId;
                    this.stockItemModel.productId = this.stockoutTempTable[
                      i
                    ].productId;
                    this.stockItemModel.productName = this.stockoutTempTable[
                      i
                    ].productName;
                    this.stockItemModel.batchNo = this.stockoutTempTable[
                      i
                    ].batchNo;
                    this.stockItemModel.num = this.stockoutTempTable[i].num;
                    this.stockItemModel.id = this.stockoutTempTable[i].id;
                    this.stockItemTempArr.push(this.stockItemModel);
                    this.stockItemModel = JSON.parse(
                      JSON.stringify(this.stockItemModelInit)
                    );
                  }
                }
                if (this.stockItemTempArr.length > 0) {
                  this.outformModel.stockItems = this.stockItemTempArr;
                  appSvc
                    .createOutStockTask(this.outformModel)
                    .then(
                      res => {
                       
                        if (res.code == "10000") {
                          this.$message({
                            message: "操作成功",
                            type: "success"
                          });
                          vm.search();

                          vm.outcancel();
                        }
                      },
                      err => {}
                    )
                    .catch(err => {
                      //数据需要清空
                      console.error(err);
                    })
                    .finally(() => {
                      vm.btnSaveLoading = false;
                      this.outformModel.stockItems=[];
                    });
                } else {
                  this.$message({
                    message: "请选择需要出库的产品!",
                    type: "warning"
                  });
                }
              } else {
                this.$message({
                  message: "请选择需要出库的产品!",
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

              this.stockItemModel.productId = productCode;
              this.stockItemModel.productName = productName;
              // this.stockItemModel.num = 0;
              //this.stockItemModel.tempnum = undefined;

              this.stockTempTable.push(this.stockItemModel);
              //格式化数据
              this.stockItemModel = JSON.parse(
                JSON.stringify(this.stockItemModelInit)
              );
            }

            this.productcancel();

            //如果是出库操作
            if (
              this.outformModel.stockoutflag != "" &&
              this.outformModel.stockoutflag == true
            ) {
              this.outStockCondition.objectType = this.outformModel.sourceType;
              for (var i = 0; i < this.stockTempTable.length; i++) {
                this.outStockCondition.pIds.push(
                  this.stockTempTable[i].productId
                );
              }

              appSvc
                .getStockInoutItems(this.outStockCondition)
                .then(
                  res => {
                    this.stockoutTempTable = res.data;
                  },
                  error => {
                    console.log(error);
                  }
                )
                .catch(() => {});
            }
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

      this.productformModel = JSON.parse(
        JSON.stringify(this.productformModelInit)
      );
      this.$refs.multipleTable.clearSelection();
      this.productTableData = [];
    },

    selectProduct() {
      this.productformTitle = "选择产品";
      this.productformVisible = true;
    },

    searchproduct() {
      // debugger;
      if (
        this.productformModel.tempType != "" &&
        this.productformModel.tempType != undefined
      ) {
        var purchaseType = -1;
        if (this.productformModel.tempType == "总部供应链") {
          purchaseType = 1;
        } else if (this.productformModel.tempType == "非总部供应链") {
          purchaseType = 2;
        }
        this.productformModel.purchaseType = purchaseType;

        this.dbLoading = true;
        appSvc
          .getShopProducts(this.productformModel)
          .then(
            res => {
              //  this.tableData = res.data;
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
      } else {
        this.$message({
          message: "请选择类型！",
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
    selectSourceType() {
      if (this.formModel.sourceType != "") {
        if (this.formModel.sourceType == "采购入库") {
          this.colShowFlag = true;
          this.viewshowFlag = true;
          this.instockNumFlag = false;

          this.formModel.sourceInventoryNo = undefined;
        } else if ((this.formModel.sourceType = "其他入库")) {
          this.colShowFlag = false;
          this.viewshowFlag = false;
          this.instockNumFlag = true;
          this.formModel.sourceInventoryNo = undefined;
        }
        this.stockTempTable = [];
      }
    },
    getpurchaseOrderDetail() {
      this.getPurchaseOrderCondition.sourceInventoryNo = this.formModel.sourceInventoryNo;
      appSvc
        .getPurchaseOrderProducts(this.getPurchaseOrderCondition)
        .then(
          res => {
            this.stockTempTable = res.data;
            this.instockNumFlag = false;
            //如果无差异数了  保存按钮就需要禁用掉
            // if (this.stockTempTable.length > 0) {
            //   var saveFlag = false;
            //   for (var i = 0; i < this.stockTempTable.length; i++) {
            //     if (this.stockTempTable[i].diffNum > 0) {
            //       break;
            //     } else {
            //       saveFlag = true;
            //     }
            //   }

            //   if (saveFlag) {
            //   //  this.viewBtn = true;
            //   }
            // }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    createInStock(row) {
      this.formVisible = true;
      this.formTitle = "产品入库";
      this.colShowFlag = true;
      this.viewBtn = false;
      appSvc
        .getCurrentUser()
        .then(
          res => {
            //  this.tableData = res.data;
            this.formModel.createBy = res.data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    cellStyle(row, column, rowIndex, columnIndex) {
      //根据报警级别显示颜色
      // console.log(row);
      // console.log(row.column);
      if (row.row.priority === "紧急" && row.column.label === "优先级") {
        return "color:red";
      }

      if (row.row.status === "驳回" && row.column.label === "状态") {
        return "color:orange";
      }
      // else if(row.column.label==="告警级别"&& row.row.alarmLevel==="一般告警" ){
      //   return 'color:yellow'
      // }
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
    // pageTurning() {
    //   this.condition.pageIndex = this.currentPage;
    //   this.getPaged();
    // },

    getPaged(flag) {
      if (this.condition.productId == "") {
        this.condition.productId = undefined;
      }

      if (this.condition.type == "") {
        this.condition.type = undefined;
      }
      this.tableLoading = true;
      appSvc
        .getStockInOutPageData(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
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
          this.tableLoading = false;
        });
    },

    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    },
    cancel(formName) {
      this.formVisible = false;
      this.formCheck = false;
      this.colShowFlag = undefined;
      this.resetForm();
      this.viewBtn = false;
      this.viewshowFlag = true;
      this.instockNumFlag = false;
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
      this.stockTempTable = [];
      this.stockItemTempArr = [];
      this.stockItemModel = JSON.parse(JSON.stringify(this.stockItemModelInit));
    },
    resetForm() {
      this.formModel = JSON.parse(JSON.stringify(this.formInit));
    },
    save(formName) {
      this.$confirm("确定操作吗?", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          this.$refs[formName].validate(valid => {
            if (valid) {
              if (this.stockTempTable.length > 0) {
                if (this.formModel.sourceType == "采购入库") {
                  for (var i = 0; i < this.stockTempTable.length; i++) {
                    if (
                      parseInt(this.stockTempTable[i].goodNum) == 0 &&
                      parseInt(this.stockTempTable[i].badNum) == 0
                    ) {
                      continue;
                    }
                    this.stockItemModel.productId = this.stockTempTable[
                      i
                    ].productId;
                    this.stockItemModel.productName = this.stockTempTable[
                      i
                    ].productName;
                    this.stockItemModel.num = parseInt(
                      this.stockTempTable[i].num
                    );

                    this.stockItemModel.goodNum = parseInt(
                      this.stockTempTable[i].goodNum
                    );
                    this.stockItemModel.badNum = parseInt(
                      this.stockTempTable[i].badNum
                    );
                    //已入数+本次入库数
                    this.stockItemModel.actualNum = parseInt(
                      this.stockItemModel.goodNum
                    );
                    this.stockItemModel.releationId = this.stockTempTable[
                      i
                    ].releationId;
                    this.stockItemModel.diffNum = parseInt(
                      this.stockTempTable[i].diffNum
                    );
                    this.stockItemModel.cost = this.stockTempTable[i].cost;
                    this.stockItemModel.id = this.stockTempTable[i].id;

                    this.stockItemModel.inoutId = this.stockTempTable[
                      i
                    ].inoutId;

                    this.stockItemTempArr.push(this.stockItemModel);
                    this.stockItemModel = JSON.parse(
                      JSON.stringify(this.stockItemModelInit)
                    );
                  }
                } else if (this.formModel.sourceType == "其他入库") {
                  for (var i = 0; i < this.stockTempTable.length; i++) {
                    if (
                      parseInt(this.stockTempTable[i].tempnum) <= 0 ||
                      this.stockTempTable[i].tempnum == "" ||
                      this.stockTempTable[i].tempnum == undefined
                    ) {
                      continue;
                    }
                    this.stockItemModel.productId = this.stockTempTable[
                      i
                    ].productId;
                    this.stockItemModel.productName = this.stockTempTable[
                      i
                    ].productName;
                    this.stockItemModel.goodNum = parseInt(
                      this.stockTempTable[i].tempnum
                    );
                    this.stockItemModel.actualNum = parseInt(
                      this.stockTempTable[i].tempnum
                    );
                    this.stockItemModel.num = parseInt(
                      this.stockTempTable[i].tempnum
                    );
                    this.stockItemModel.cost = 0; //TODO 设置为0
                    this.stockItemTempArr.push(this.stockItemModel);
                    this.stockItemModel = JSON.parse(
                      JSON.stringify(this.stockItemModelInit)
                    );
                  }
                }

                if (this.stockItemTempArr.length > 0) {
                  this.formModel.stockItems = this.stockItemTempArr;
                  vm.btnSaveLoading = true;

                  appSvc
                    .createInStockTask(this.formModel)
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
                    message: "请选择需要入库的产品!",
                    type: "warning"
                  });
                }
              } else {
                this.$message({
                  message: "请选择需要入库的产品!",
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
</style>
