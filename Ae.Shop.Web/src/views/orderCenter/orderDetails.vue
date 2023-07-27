
<template>
  <main class="container">
    <div class="order">
      <el-row>
        <el-divider content-position="left">基本信息</el-divider>
        <!-- <el-col :span="6"> -->
        <el-form ref="form"
                 :model="orderInfo"
                 label-width="100px"
                 size="mini"
                 class="col-3">
          <el-form-item label="订单编号">
            <label>{{ orderInfo.orderNo }}</label>
          </el-form-item>
          <el-form-item label="订单渠道">
            <label>{{ orderInfo.channel }}</label>
          </el-form-item>
          <el-form-item label="订单分类">
            <label>{{ orderInfo.showProductType }}</label>
          </el-form-item>

          <el-form-item label="订单状态">
            <label>{{ orderInfo.orderStatus }}</label>
          </el-form-item>
          <el-form-item label="下单时间">
            <label>{{ orderInfo.orderTime }}</label>
          </el-form-item>
          <el-form-item label="订单类型">
            <label>{{ orderInfo.orderType }}</label>
          </el-form-item>

          <!-- <el-form-item label="签收状态">
            <label>{{ orderInfo.signStatus }}</label>
          </el-form-item> -->
          <el-form-item label="关联数据">
            <label>{{ orderInfo.refOrderNo }}</label>
          </el-form-item>
          <el-form-item label="安装时间">
            <label>{{ orderInfo.installTime }}</label>
          </el-form-item>
          <el-form-item label="备注">
            <label>{{ orderInfo.remark }}</label>
          </el-form-item>

          <el-form-item label="创建人">
            <label>{{ orderInfo.createBy }}</label>
          </el-form-item>
          <el-form-item label="预约时间">
            <label>{{ reserverInfo.reserveTime }}</label>
          </el-form-item>
          <el-form-item label="派工技师">
            <label>{{ orderDispatchInfo.techName }}</label>
          </el-form-item>
        
        </el-form>
      </el-row>

      <el-row>
        <el-divider content-position="left">用户信息</el-divider>
        <el-form
          ref="form"
          :model="orderInfo"
          label-width="100px"
          size="mini"
          class="col-3"
        >
          <el-form-item label="车牌号码">
            <label>{{ userInfo.carNumber }}</label>
          </el-form-item>
          <el-form-item label="客户名称">
            <label>{{ orderInfo.userName }}</label>
          </el-form-item>
          <el-form-item label="客户电话">
            <label>{{ orderInfo.userPhone }}</label>
          </el-form-item>
          <!-- <el-form-item label="联系电话">
            <label>{{ orderInfo.contactPhone }}</label>
          </el-form-item> -->
          <!-- <el-form-item label="车型信息" width="66.6%">
            <label>{{ userInfo.displayCarName }}</label>
          </el-form-item> -->
        </el-form>
        <el-form
          ref="form"
          :model="orderInfo"
          label-width="100px"
          size="mini"
        >
          <el-form-item label="车型信息" >
            <label>{{ userInfo.displayCarName }}</label>
          </el-form-item>
        </el-form>
      </el-row>
<!-- 
      <el-row>
        <el-divider content-position="left">预约信息</el-divider>
        <el-form
          ref="form"
          :model="reserverInfo"
          label-width="100px"
          size="mini"
          class="col-3"
        >
          <el-form-item label="预约单号">
            <label>{{ reserverInfo.id }}</label>
          </el-form-item>
          <el-form-item label="预约状态">
            <label>{{ reserverInfo.status }}</label>
          </el-form-item>
          <el-form-item label="预约时间">
            <label>{{ reserverInfo.reserveTime }}</label>
          </el-form-item>
          <el-form-item label="预约备注">
            <label>{{ reserverInfo.remark }}</label>
          </el-form-item>
        </el-form>
      </el-row> -->

      <el-row>
        <el-divider content-position="left">机构公司</el-divider>
        <el-form ref="form" label-width="100px" size="mini">
          <el-row>
            <el-col :span="12">
              <el-form-item label="名称" v-if="insuranceInfo">{{
                insuranceInfo.name
              }}</el-form-item>
              <el-form-item label="名称" v-else></el-form-item>
            </el-col>
            <el-col :span="6">
              <el-form-item label="关联单号" v-if="insuranceInfo">{{
                insuranceInfo.refNo
              }}</el-form-item>
              <el-form-item label="关联单号" v-else></el-form-item>
            </el-col>

            <el-col :span="6"> </el-col>
          </el-row>
        </el-form>
      </el-row>
      <el-row>
        <el-divider content-position="left">支付&结算</el-divider>
        <el-form
          ref="form"
          :model="orderInfo"
          label-width="100px"
          size="mini"
          class="col-3"
        >
          <el-form-item label="付款方式">
            <label>{{ orderInfo.payMethod }}</label>
          </el-form-item>
          <el-form-item label="支付渠道">
            <label>{{ orderInfo.payChannel }}</label>
          </el-form-item>
          <el-form-item label="支付状态">
            <label>{{ orderInfo.payStatus }}</label>
            <!-- 付款时间 -->
            <label>{{ orderInfo.payTime }}</label>
          </el-form-item>
          <!-- <el-form-item label="到账状态">
            <label>{{ orderInfo.moneyArriveStatus }}</label>
          </el-form-item> -->
          <el-form-item label="对账状态">
            <label>{{ orderInfo.reconciliationStatus }}</label>
          </el-form-item>
          <el-form-item label="结算状态">
            <label>{{ orderInfo.settleStatus }}</label>
            <!-- 结算时间 -->
            <label>{{ orderInfo.settleTime }}</label>
          </el-form-item>
          <el-form-item label="退款状态">
            <label>{{ orderInfo.refundStatus ? "已退款" : "未退款" }}</label>
            <!-- 结算时间 -->
            <label>{{ orderInfo.refundAmount }}</label>
          </el-form-item>
        </el-form>
      </el-row>

      <el-row>
        <el-divider content-position="left">商品&服务</el-divider>
        <el-form
          ref="form"
          :model="orderInfo"
          label-width="100px"
          size="mini"
          class="col-4"
        >
          <el-form-item label="商品金额">
            <label class="s2"
              >￥{{ (orderInfo.totalProductAmount || 0).format(2) }}</label
            >
          </el-form-item>
          <el-form-item label="运费">
            <label class="s2"
              >￥{{ (orderInfo.deliveryFee || 0).format(2) }}</label
            >
          </el-form-item>
          <el-form-item label="优惠金额">
            <label class="s2"
              >￥{{ (orderInfo.totalCouponAmount || 0).format(2) }}</label
            >
          </el-form-item>
          <el-form-item label="实收金额">
            <label class="s2"
              >￥{{ (orderInfo.actualAmount || 0).format(2) }}</label
            >
          </el-form-item>
        </el-form>
        <div style="padding-left: 30px; width: calc(100% - 20px)">
          <el-table
            v-loading="tableLoading"
            :data="tableData"
            row-key="id"
            border
            size="mini"
            default-expand-all
            :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
          >
            <rg-table-column prop="productId" label="产品编号" fix-min />
            <el-table-column prop="productName" label="产品名称" fix-min >
              <template slot-scope="scope">
                <img
                  v-if="scope.row.imageUrl != ''"
                  :src="scope.row.imageUrl"
                  height="30"
                  class="pic"
                  style="float: left"
                />
                <span
                  v-else-if="scope.row.imageUrl === ''"
                  style="float: left; padding-left: 10px; line-height: 30px"
                  >暂无图片</span
                >
                <span style="padding-left: 10px; line-height: 30px">{{
                  scope.row.productName
                }}</span>
              </template>
            </el-table-column>
            <rg-table-column prop="number" label="数量" align="right" fix-min>
              <template slot-scope="scope">{{
                scope.row.number.toLocaleString()
              }}</template>
            </rg-table-column>
            <rg-table-column prop="unit" label="单位" align="center" fix-min />

            <rg-table-column prop="price" label="单价" align="right" fix-min >
              <template slot-scope="scope">{{
                $jscom.toMoney(scope.row.price)
              }}</template>
            </rg-table-column>
            <rg-table-column prop="totalAmount" label="总价" align="right" fix-min>
              <template slot-scope="scope">
                {{ $jscom.toMoney(scope.row.totalAmount) }}</template
              >
            </rg-table-column>
            <rg-table-column
              prop="remark"
              label="备注"
              align="left"
              fix-min
            ></rg-table-column>
          </el-table>
        </div>
      </el-row>
      <rg-dialog
        :title="formTitle"
        :visible.sync="formVisible"
        :beforeClose="cancel"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            cancel('formModel');
          },
        }"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="700px"
      >
        <template v-slot:content>
          <el-form
            :model="formModel"
            :rules="rules"
            ref="formModel"
            label-width="120px"
            size="mini"
          >
            <el-form-item label="取消原因" prop="reasons">
              <el-select
                v-model="formModel.reasons"
                placeholder="请选择"
                style="width: 420px"
              >
                <el-option
                  v-for="item in reasons"
                  :key="item.id"
                  :label="item.reason"
                  :value="item.id"
                />
              </el-select>
            </el-form-item>
            <el-form-item label="取消描述：" prop="instruction">
              <el-input
                v-model="formModel.instruction"
                type="textarea"
                style="width: 420px"
                autocomplete="off"
                clearable
                placeholder="请填写取消描述"
              />
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            :disabled="formCheck"
            :loading="btnSaveLoading"
            @click="save('formModel')"
            >保存</el-button
          >
        </template>
      </rg-dialog>
    </div>
  </main>
</template>

<script>
import { orderCenterSvc, OrderCommand } from "@/api/orderCenter/orderCenter";
export default {
  props: {
    visible: { type: Boolean, default: false },
    orderNo: { type: String, default: "" },
    cancelOrder: {
      default: {
        label: "取消订单",
        show: false,
        disabled: true,
        click: () => {},
      },
    },
  },
  data() {
    return {
      formCheck: false,
      formTitle: "取消订单",
      btnSaveLoading: false,
      rules: {
        reasons: [
          { required: true, message: "请选择取消原因", trigger: "blur" },
        ],
        instruction: [
          { required: true, message: "请填写取消描述", trigger: "blur" },
          { max: 50, message: "取消描述长度不允许超过50", trigger: "blur" },
        ],
      },
      reasons: [
        {
          id: "1",
          reason: "7天无理由退货",
        },
        {
          id: "2",
          reason: "不喜欢/不想要",
        },
        {
          id: "3",
          reason: "待审核时间长",
        },
        {
          id: "4",
          reason: "其他",
        },
      ],
      totalnumber: "",
      productId: "",
      unclick: false,

      formModel: {
        reasons: [],
        instruction: "",
      },
      //reasons: "",
      formVisible: false,
      tableData: [],
      tableLoading: false,
      imageUrl: "",
      // orderNo: "",
      orderInfo: {
        orderNo: "",
        Channel: 0,
        OrderType: 0,
        orderStatus: 0, // 订单状态（10已提交 20已确认 30已完成 40已取消）
        OrderTime: "",
        userName: "",
        userPhone: "",
        deliveryFee: 0,
        totalProductAmount: 0,
        totalCouponAmount: 0,
        actualAmount: 0,
        payMethod: 0,
        payChannel: 0,
        payStatus: 0,
        moneyArriveStatus: 0,
        reconciliationStatus: 0,
        deliveryType: 0,
        deliveryMethod: 0,
        deliveryStatus: 0,
        signStatus: 0,
        installStatus: 0,
        settleStatus: 0,
        payTime: "",
        installTime: "",
        reconciliationTime: "",
        settleTime: "",
        createBy: "", // 创建人
        createTime: "", // 创建时间
        remark: "", // 订单备注
        showProductType:"",
        refundStatus: 0,
        refundTime: "",
        refundAmount: 0,
      },
      userInfo: {
        userId: "", // 用户Id
        userName: "", // 用户姓名
        userTel: "", // 用户手机号
        memberLevel: "", // 会员等级显示
        displayContactAddress: "", // 显示联系地址
        carId: "", // 车辆Id
        carNumber: "", // 车牌号
        displayCarName: "", // 显示车型名称信息（格式：品牌Brand 车系Vehicle 年份Nian 排量PaiLiang 款型SalesName）
        totalMileage: 0, // 总公里数
        address: {},
      },

      reserverInfo: {
        id: 0,
        status: "",
        reserveTime: "",
        remark: "",
      },
      orderDispatchInfo: {
        techName: "",
      },
      insuranceInfo: {
        name: "",
        refNo: "",
      },
    };
  },
  watch: {
    visible(val) {
      this.GetOrderDetail(this.orderNo);
    },
  },
  created() {
    this.GetOrderDetail(this.orderNo);
    //取消订单的操作
    this.cancelOrder.click = this.handleEdit;
  },
  methods: {
    // 修改按钮状态
    changeButton() {
      if (this.orderInfo.payStatus === 1) {
        {
          this.unclick = true; // 不可取消订单
        }
      }
    },

    // 处理数据
    change() {
      if (this.orderInfo.installTime === "1900-01-01 00:00:00") {
        this.orderInfo.installTime = "";
      }
      if (this.orderInfo.payTime === "1900-01-01 00:00:00") {
        this.orderInfo.payTime = "";
      }
      if (this.orderInfo.settleTime === "1900-01-01 00:00:00") {
        this.orderInfo.settleTime = "";
      }
      var os = this.orderInfo.orderStatus; // 订单状态
      var oc = this.orderInfo.channel; // 订单渠道
      const oo = this.orderInfo.orderType; // 订单类型
      if (os === 10) {
        this.orderInfo.orderStatus = "已提交";
      }
      if (os === 20) {
        this.orderInfo.orderStatus = "已确认";
      }
      if (os === 30) {
        this.orderInfo.orderStatus = "已完成";
      }
      if (os === 40) {
        this.orderInfo.orderStatus = "已取消";
      }

      if (oc == 0) {
        this.orderInfo.channel = "未设置";
      }
      if (oc == 1) {
        this.orderInfo.channel = "C端";
      }
      if (oc == 2) {
        this.orderInfo.channel = "门店";
      }
      if (oc == 3) {
        this.orderInfo.channel = "电销下单";
      }
      if (oc == 4) {
        this.orderInfo.channel = "美团";
      }
      if (oc == 5) {
        this.orderInfo.channel = "大客户";
      }

      if (oo === 0) {
        this.orderInfo.orderType = "未设置";
      }
      if (oo === 1) {
        this.orderInfo.orderType = "轮胎安装";
      }
      if (oo === 2) {
        this.orderInfo.orderType = "保养服务";
      }
      if (oo === 3) {
        this.orderInfo.orderType = "美容洗车";
      }
      if (oo === 4) {
        this.orderInfo.orderType = "钣金喷漆";
      }
      if (oo === 5) {
        this.orderInfo.orderType = "维修改装";
      }
      if (oo === 6) {
        this.orderInfo.orderType = "其他";
      }

      const signS = this.orderInfo.signStatus; // 签收状态
      switch (signS) {
        case 0:
          this.orderInfo.signStatus = "未签收";
          break;
        case 1:
          this.orderInfo.signStatus = "已签收";
          break;
      }

      const fp = this.orderInfo.payStatus; // 支付状态
      switch (fp) {
        case 0:
          this.orderInfo.payStatus = "未支付";
          break;
        case 1:
          this.orderInfo.payStatus = "已支付";
          break;
      }
      const fc = this.orderInfo.payChannel; // 支付渠道
      switch (fc) {
        case 0:
          this.orderInfo.payChannel = "未设置";
          break;
        case 1:
          this.orderInfo.payChannel = "微信支付";
          break;
        case 2:
          this.orderInfo.payChannel = "支付宝";
          break;
        case 3:
          this.orderInfo.payChannel = "美团";
          break;
        case 4:
          this.orderInfo.payChannel = "线下支付";
          break;
        case 9:
          this.orderInfo.payChannel = "代付款";
          break;
      }
      const fm = this.orderInfo.payMethod; // 支付方式
      switch (fm) {
        case 0:
          this.orderInfo.payMethod = "未设置";
          break;
        case 1:
          this.orderInfo.payMethod = "在线支付";
          break;
        case 2:
          this.orderInfo.payMethod = "到店付款";
          break;
        case 3:
          this.orderInfo.payMethod = "月结";
          break;
      }
      const ma = this.orderInfo.moneyArriveStatus; // 到账状态
      switch (ma) {
        case 0:
          this.orderInfo.moneyArriveStatus = "未到账";
          break;
        case 1:
          this.orderInfo.moneyArriveStatus = "已到账";
          break;
      }
      const rs = this.orderInfo.reconciliationStatus; // 退款状态
      switch (rs) {
        case 0:
          this.orderInfo.reconciliationStatus = "未对账";
          break;
        case 1:
          this.orderInfo.reconciliationStatus = "异常对账";
          break;
        case 2:
          this.orderInfo.reconciliationStatus = "已对账";
          break;
      }
      const is = this.orderInfo.installStatus;
      switch (is) {
        case 0:
          this.orderInfo.installStatus = "未安装";
          break;
        case 1:
          this.orderInfo.installStatus = "已安装";
          break;
      }
      const ss = this.orderInfo.settleStatus;
      switch (ss) {
        case 0:
          this.orderInfo.settleStatus = "未结算";
          break;
        case 1:
          this.orderInfo.settleStatus = "已结算";
          break;
      }

      if (this.reserverInfo.id > 0) {
        const rs = this.reserverInfo.status;
        switch (rs) {
          case 0:
            this.reserverInfo.status = "待确认";
            break;
          case 1:
            this.reserverInfo.status = "已确认";
            break;
          case 2:
            this.reserverInfo.status = "已完成";
            break;
          case 3:
            this.reserverInfo.status = "取消";
            break;
        }
      } else {
        this.reserverInfo.id = "-";
        this.reserverInfo.status = "-";
        this.reserverInfo.reserveTime = "-";
        this.reserverInfo.remark = "-";
      }
    },
    // 获取订单详情
    GetOrderDetail(orderNo) {
      this.orderInfo.orderNo = orderNo || this.$route.query.orderNo;
      if (orderNo === undefined) return;
      orderCenterSvc
        .GetOrderDetail({ orderNos: [this.orderInfo.orderNo] })
        .then(
          (res) => {
            if (res.data != undefined && res.data.length > 0) {
              this.orderInfo = res.data[0];
            }
            // 取消订单判别条件
            this.cancelOrder.disabled = true;

            this.changeButton();
            this.change();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});

      orderCenterSvc
        .GetOrderProducts({ orderNos: [this.orderInfo.orderNo] })
        .then(
          (res) => {
            this.tableData = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});

      orderCenterSvc
        .GetOrderCarsInfo({ orderNos: [this.orderInfo.orderNo] })
        .then(
          (res) => {
            if (res.data != undefined && res.data.length > 0) {
              this.userInfo = res.data[0];
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});

      orderCenterSvc
        .GetReserverInfo({ OrderNo: this.orderInfo.orderNo })
        .then(
          (res) => {
            this.reserverInfo = res.data;
            this.change();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => { });

      orderCenterSvc
        .GetDispatchInfo({ OrderNo: this.orderInfo.orderNo })
        .then(
          (res) => {
            this.orderDispatchInfo = res.data;
            this.change();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => { });
      orderCenterSvc
        .GetOrderInsuranceCompany({ OrderNo: this.orderInfo.orderNo })
        .then(
          (res) => {
            this.insuranceInfo = res.data;
            this.change();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    // 取消订单
    handleEdit() {
      this.formVisible = true;
      orderCenterSvc
        .GetReverseReasons({ ApplyType: 1 })
        .then(
          (res) => {
            var data = res.data;
            this.reasons = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    sure() {
      orderCenterSvc
        .CancelOrder({
          orderNo: this.$route.query.orderNo,
          reasonId: this.tableData2.reasons,
          instruction: this.tableData2.instruction,
        })
        .then(
          (res) => {},
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    cancel(formName) {
      this.formVisible = false;
      this.formCheck = false;

      // this.resetForm();

      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },
    save(formName) {
      // var vm = this;
      this.$refs[formName].validate((valid) => {
        if (valid) {
          vm.btnSaveLoading = true;
          // shopAssetSvc
          //   .upsertShopAsset(this.formModel)
          //   .then(
          //     res => {
          //       if (res.code=="10000") {
          //         this.$message({
          //           message: "操作成功",
          //           type: "success"
          //         });
          //         vm.search();
          //         if (vm.formVisible) {
          //           vm.getConditionData();
          //         }
          //         vm.cancel();
          //       }
          //     },
          //     err => {
          //     }
          //   )
          //   .catch(err => {
          //     console.error(err);
          //   })
          //   .finally(() => {
          //     vm.btnSaveLoading = false;
          //   });
        } else {
          return false;
        }
      });
    },
  },
};
</script>
<style lang="scss" scoped>
.container {
  // margin: 10px;
  // margin-left: 45px;
  height: 100%;
  overflow: auto;
}

.el-row {
  margin-bottom: 2px;
}
.s2 {
  color: red;
  font: 16px/20px "";
}
</style>
