<template>
  <main class="container">
    <div class="install">
      <el-row>
        <el-col :span="6">
          <!-- <h1 class="sidebar-title"></h1> -->
          <img v-if="logo" :src="logo" class="sidebar-logo" />
        </el-col>
        <el-col :span="12">
          <h1 class="title">
            维修工单 <label class="subTitle">(客户联)</label>
          </h1>
        </el-col>
        <el-col :span="6">
          <h1 class="title">
            总计:￥{{
              (
                orderInfo.actualAmount|| 0
              ).format(2)
            }}
          </h1>
        </el-col>
      </el-row>
      <el-row>
        <HR color="black" SIZE="1" />
      </el-row>
      <el-row>
        <el-form
          ref="form"
          :model="orderInfo"
          label-width="70px"
          size="mini"
          label-position="left"
          style="font-weight: 0"
        >
          <el-row class="rowContent">
            <el-col :span="10">
              <el-form-item label="车主姓名" v-if="orderInfo">
                {{ orderInfo.userName }}
              </el-form-item>
            </el-col>
            <el-col :span="14">
              <el-form-item label="公里数" v-if="carInfo">
                {{ carInfo.totalMileage > 0 ? carInfo.totalMileage : "" }}
              </el-form-item>
              <el-form-item label="公里数" v-else></el-form-item>
            </el-col>
          </el-row>
          <el-row class="rowContent">
            <el-col :span="10">
              <el-form-item label="车牌号码" v-if="carInfo">
                {{ carInfo.carNumber }}
              </el-form-item>
              <el-form-item label="车牌号码" v-else></el-form-item>
            </el-col>
            <el-col :span="14">
              <el-form-item label="车型" v-if="carInfo">
                {{
                  carInfo.brand != ""
                    ? carInfo.brand +
                      "|" +
                      carInfo.vehicle +
                      "|" +
                      carInfo.nian +
                      "|" +
                      carInfo.paiLiang +
                      "|" +
                      carInfo.salesName
                    : ""
                }}
              </el-form-item>
              <el-form-item label="车型" v-else></el-form-item>
            </el-col>
          </el-row>

          <el-row class="rowContent">
            <el-col :span="10">
              <el-form-item label="安装门店">
                {{ shop.simpleName }}
              </el-form-item>
            </el-col>
            <el-col :span="14">
              <el-form-item label="门店电话">
                {{ shop.mobile }}
              </el-form-item>
            </el-col>
          </el-row>

          <el-row class="rowContent">
            <el-col :span="24">
              <el-form-item label="门店地址">
                {{ shop.address }}
              </el-form-item>
            </el-col>
          </el-row>

          <el-row class="rowContent">
            <el-col :span="24">
              <el-form-item label="下单时间">
                {{ orderInfo.createTime }}
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>
      </el-row>

      <el-row class="formContainer">
        <el-form
          ref="form"
          :model="orderInfo"
          label-width="70px"
          size="mini"
          label-position="left"
          style="padding: 10px"
        >
          <el-row class="rowContent">
            <el-col :span="10">
              <el-form-item label="订单号">
                {{ orderInfo.orderNo }}
              </el-form-item>
            </el-col>
            <el-col :span="14">
              <el-form-item label="支付方式">
                {{
                  orderInfo.payChannel == 0
                    ? ""
                    : orderInfo.payChannel == 1
                    ? "微信支付"
                    : "支付宝"
                }}
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>
        <div>
          <el-table
            v-loading="tableLoading"
            :data="tableData"
            border
            size="mini"
            :cell-style="cellStyle"
            :header-cell-style="{ 'text-align': 'left' }"
          >
            <rg-table-column prop="productId" align="left" label="产品编号" />
            <el-table-column prop="productName" align="left" label="产品名称">
            </el-table-column>
            <rg-table-column prop="number" label="数量" align="right">
              <template slot-scope="scope">{{
                scope.row.number.toLocaleString()
              }}</template>
            </rg-table-column>
            <rg-table-column prop="unit" label="单位" align="center" />

            <rg-table-column prop="price" label="单价" align="right">
              <template slot-scope="scope">{{
                $jscom.toMoney(scope.row.price)
              }}</template>
            </rg-table-column>
            <rg-table-column prop="totalAmount" label="总价" align="right">
              <template slot-scope="scope">
                {{ $jscom.toMoney(scope.row.totalAmount) }}</template
              >
            </rg-table-column>
          </el-table>
        </div>
      </el-row>
      <el-row>
        <div>
          <h1 class="sidebar-title">
            提示:如安装时订单发生变动，请以系统金额为准！
          </h1>
        </div>
      </el-row>
      <el-row>
        <HR color="black" SIZE="1" />
      </el-row>
      <el-row>
        <div>安装完毕后请确认下列项目：</div>
        <div>1、安装完毕后测得的实际胎压：前轮______BAR、后轮_______BAR。</div>
        <div>2、旧配件处理选择:</div>
        <div style="margin-left: 10px">
          <el-checkbox
            >a.旧配件已确认，用户声明放弃并交承接方处理；</el-checkbox
          >
          <el-checkbox>b.旧配件已确认,用户自行处理；</el-checkbox>
          <el-checkbox>c.无配件；</el-checkbox>
        </div>
        <div>3、轮胎产品安装完毕请确认以下信息:</div>
        <div style="margin-left: 10px">
          a.更换新的气门嘴； b.给__个轮胎免费做动平衡（包括平衡块）；
          c.四轮定位（额外收费）提供数据单。
        </div>
        <el-form
          ref="form"
          label-width="100px"
          :model="orderInfo"
          size="mini"
          label-position="left"
          style="margin-top: 10px"
        >
          <el-row>
            <el-col :span="12">
              <el-form-item label="安装时间" v-if="orderInfo">
                {{
                  orderInfo.installTime == "1900-01-01 00:00:00"
                    ? " "
                    : orderInfo.installTime
                }}
              </el-form-item>
              <!-- <el-form-item label="安装时间" v-else></el-form-item> -->
            </el-col>
            <el-col :span="6">
              <el-form-item label="维修员签名" v-if="orderInfo">
                <!-- {{
                orderInfo.refNo
              }} -->
              </el-form-item>
              <!-- <el-form-item label="维修员签名" v-else></el-form-item> -->
            </el-col>
            <el-col :span="6"> </el-col>
          </el-row>
        </el-form>
      </el-row>
      <el-row>
        <div>(沿此虚线剪开)以上为客户联</div>
        <HR style="border: 1px dashed black; height: 1px" SIZE="1" />
        <div style="margin-left: 95px">以下为门店留存联</div>
      </el-row>

      <el-row>
        <el-col :span="24">
          <h1 class="title">
            安装确认回执联 <label class="subTitle">(门店存根联)</label>
          </h1>
        </el-col>
      </el-row>
      <el-row>
        <el-form
          ref="form"
          label-width="70px"
          :model="orderInfo"
          size="mini"
          label-position="left"
          style="margin-top: 10px"
        >
          <el-row>
            <el-col :span="12">
              <el-form-item label="订单号" v-if="orderInfo">
                {{ orderInfo.orderNo }}
              </el-form-item>
              <!-- <el-form-item label="安装时间" v-else></el-form-item> -->
            </el-col>
            <el-col :span="6">
              <el-form-item label="总计" v-if="orderInfo">
                ￥{{
                  (
                    orderInfo.actualAmount || 0
                  ).format(2)
                }}
              </el-form-item>
            </el-col>
            <el-col :span="6"> </el-col>
          </el-row>
          <el-row>
            <el-col :span="12">
              <el-form-item label="门店名称" v-if="shop">
                {{ shop.simpleName }}
              </el-form-item>
            </el-col>
          </el-row>
        </el-form>
      </el-row>
      <el-row>
        <div>经确认产品与装箱清单吻合，已完成安装并能正常使用</div>
      </el-row>
      <el-row>
        <div>安装完毕后请确认下列项目：</div>
        <div>1、安装完毕后测得的实际胎压：前轮______BAR、后轮_______BAR。</div>
        <div>2、旧配件处理选择:</div>
        <div style="margin-left: 10px">
          <el-checkbox
            >a.旧配件已确认，用户声明放弃并交承接方处理；</el-checkbox
          >
          <el-checkbox>b.旧配件已确认,用户自行处理；</el-checkbox>
          <el-checkbox>c.无配件；</el-checkbox>
        </div>
        <div>3、轮胎产品安装完毕请确认以下信息:</div>
        <div style="margin-left: 10px">
          a.更换新的气门嘴； b.给__个轮胎免费做动平衡（包括平衡块）；
          c.四轮定位（额外收费）提供数据单。
        </div>
        <el-form
          ref="form"
          label-width="110px"
          :model="orderInfo"
          size="mini"
          label-position="left"
          style="margin-top: 10px"
        >
          <el-row>
            <el-col :span="12">
              <el-form-item label="安装时间：" v-if="orderInfo">
                {{
                  orderInfo.installTime == "1900-01-01 00:00:00"
                    ? " "
                    : orderInfo.installTime
                }}
              </el-form-item>
              <!-- <el-form-item label="安装时间" v-else></el-form-item> -->
            </el-col>
            <el-col :span="6">
              <el-form-item label="备注：" v-if="orderInfo">
                {{ orderInfo.remark }}
              </el-form-item>
              <!-- <el-form-item label="维修员签名" v-else></el-form-item> -->
            </el-col>
            <el-col :span="6"> </el-col>
          </el-row>
          <el-row>
            <el-col :span="12">
              <el-form-item label="维修人员签名：" v-if="orderInfo">
                <!-- {{
                insuranceInfo.name
              }} -->
              </el-form-item>
              <!-- <el-form-item label="安装时间" v-else></el-form-item> -->
            </el-col>
            <el-col :span="6">
              <el-form-item label="客户签名：" v-if="orderInfo">
                <!-- {{
                orderInfo.refNo
              }} -->
              </el-form-item>
              <!-- <el-form-item label="维修员签名" v-else></el-form-item> -->
            </el-col>
            <el-col :span="6"> </el-col>
          </el-row>
        </el-form>
      </el-row>
      <el-row>
        <div style="margin-left: 500px">____年___月____日</div>
      </el-row>
    </div>
  </main>
</template>

<script>
import { orderCenterSvc } from "@/api/orderCenter/orderCenter";
import { shopManageSvc } from "@/api/shopmanage/shopmanage";
export default {
  props: {
    visible: { type: Boolean, default: false },
    orderNo: { type: String, default: "" },
  },
  data() {
    return {
      logo: require("@/assets/login_images/newLog.png"),
      tableData: [],
      tableLoading: false,
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
        shopId: 0,
      },
      reserverInfo: {
        id: 0,
        status: "",
        reserveTime: "",
        remark: "",
      },
      shop: {
        simpleName: "", // 简单名称
        fullName: "", // 店全称
        address: "", // 详细地址
        post: "", // 邮编
        telephone: "", // 电话
      },
      carInfo: {
        carNumber: "",
        brand: "",
        vehicle: "",
        paiLiang: "",
        nian: "",
        tid: "",
        salesName: "",
        totalMileage: "",
        lastBaoYangKm: "",
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
  },
  methods: {
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

              shopManageSvc
                .getShopById({ shopId: this.orderInfo.shopId })
                .then(
                  (res) => {
                    this.shop = res.data.shop;
                    // console.log("门店信息: " + JSON.stringify(this.shop));
                    this.$forceUpdate();
                  },
                  (error) => {
                    console.log(error);
                  }
                )
                .catch(() => {});
            }
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
      // orderCenterSvc
      //   .GetReserverInfo({ OrderNo: this.orderInfo.orderNo })
      //   .then(
      //     (res) => {
      //       this.reserverInfo = res.data;
      //     },
      //     (error) => {
      //       console.log(error);
      //     }
      //   )
      //   .catch(() => {});

      orderCenterSvc
        .GetOrderCarsInfo({ orderNos: [this.orderInfo.orderNo] })
        .then(
          (res) => {
            let data = res.data;
            if (data != null || data != undefined) {
              if (data.length > 0) {
                this.carInfo.carNumber = data[0].carNumber;
                this.carInfo.brand = data[0].brand;
                this.carInfo.vehicle = data[0].vehicle;
                this.carInfo.paiLiang = data[0].paiLiang;
                this.carInfo.nian = data[0].nian;
                this.carInfo.tid = data[0].tid;
                this.carInfo.salesName = data[0].salesName;
                this.carInfo.totalMileage = data[0].totalMileage;
                this.carInfo.lastBaoYangKm = data[0].lastBaoYangKm;
              }
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    cellStyle({ row, column, colunmIndex }) {
      let cellStyle =
        "border:1px solid black; border-left:none;border-bottom:none;text-align:left;";
      return cellStyle;
    },
    initial(){
      // this.orderInfo.orderNo="";
      //   this.orderInfo.orderNo="";
    }
  },
};
</script>
<style lang="scss" scoped>
.container {
  height: 100%;
  overflow: auto;
  border: solid 3px #ebeef5;
}
.install {
  margin: 30px;
}
.formContainer {
  height: 100%;
  overflow: auto;
  border: solid 1px black;
}
.el-table th > .cell {
  text-align: left !important;
}
.el-row {
  margin-bottom: 0px;
}

.s2 {
  color: red;
  font: 16px/20px "";
}
.sidebar-title {
  display: inline-block;
  margin: 0;
  color: #ea5921;
  font-weight: 600;
  line-height: 50px;
  font-size: 18px;
  font-family: Avenir, Helvetica Neue, Arial, Helvetica, sans-serif;
  vertical-align: middle;
}
.sidebar-logo {
  width: 250px;
  height: 80px;
  vertical-align: middle;
  margin-right: 12px;
}
.title {
  font-size: 18px;
  vertical-align: middle;
  text-align: center;
}
.subTitle {
  font-size: 14px;
  vertical-align: middle;
  text-align: center;
}
.rowContent {
  margin-bottom: 0px;
}
.el-table--border,
.el-table--group {
  border-top: 1px solid black;
}
.el-table th {
  border-right: 1px solid black !important;
}
</style>
