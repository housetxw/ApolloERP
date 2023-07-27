
<template>
  <el-card class="box-card" v-if="showCard" v-loading="loading">
    <div slot="header" class="clearfix">
      <el-button style="float: left; padding: 3px 0" type="text" @click="last">上一步</el-button>
      <span>预约&支付</span>
      <el-button style="float: right;" size="mini" type="primary" @click="next">提交订单</el-button>
    </div>
    <div class="bax-card-container">
      <el-form label-width="120px" ref="formCarInfo" size="mini">
        <el-row>
          <el-divider content-position="left">预约服务时间</el-divider>
        </el-row>
        <el-form-item label="预约时间" required>
          <label style="display:inline-block;width:150px">{{reserverTime}}</label>
          <el-button
            @click="()=>{this.dlg_booking =true}"
            type="primary"
            class="s-mini"
            size="mini"
          >约定时间</el-button>
        </el-form-item>
        <el-divider content-position="left">支付及其他</el-divider>
        <el-form-item label="支付方式">
          <el-radio-group v-model="payMethod">
            <el-radio :label="1">现场支付</el-radio>
            <el-radio :label="2">微信支付</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="优惠券">
          <label v-if="orderInfo.userCouponId==0">无优惠券</label>
          <label v-else-if="allApplicableCoupons.length==1">
            {{orderInfo.userCouponDisplayName}}
            &nbsp;&nbsp;
            <el-button
              type="text"
              icon="el-icon-d-arrow-right"
              v-if="orderInfo.allApplicableCoupons.length>1"
            >更多优惠券</el-button>
          </label>
          <el-radio-group
            v-model="orderInfo.userCouponId"
            @change="change_coupon"
            v-else
            style="margin-top:8px"
          >
            <el-radio
              class="rediaCar"
              v-for="coupon in allApplicableCoupons"
              :key="coupon.userCouponId"
              :value="coupon.userCouponId"
              :label="coupon.userCouponId"
              v-bind:title="'使用规则：'+coupon.useRuleDesc"
            >{{coupon.displayName}}【价值：{{coupon.value.toMoney()}}，有效期:{{coupon.startValidTime}}~{{coupon.endValidTime}}】</el-radio>
          </el-radio-group>
        </el-form-item>

        <el-form-item label="留言">
          <el-input
            type="textarea"
            placeholder="请输入客户留言"
            rows="5"
            v-model="remark"
            maxlength="300"
            show-word-limit
          ></el-input>
        </el-form-item>
        <el-row>
          <el-col style="width:calc(100% - 300px)">&nbsp;</el-col>
          <el-col style="width:300px">
            <el-row>&nbsp;</el-row>
            <el-form-item label="商品总金额">
              <font color="red">{{(orderInfo["totalProductAmount"]||0).toMoney()}}</font>
            </el-form-item>
            <el-form-item label="上门服务费">
              <font color="red">{{(orderInfo["totalDoorToDoorFee"]||0).toMoney()}}</font>
            </el-form-item>
            <el-form-item label="优惠券优惠">
              <font color="red">- {{(orderInfo["totalCouponAmount"]||0).toMoney()}}</font>
            </el-form-item>
            <el-form-item label="会员优惠" v-if="orderInfo['diamondsDiscountAmount']">
              <font color="red">- {{(orderInfo["diamondsDiscountAmount"]||0).toMoney()}}</font>
            </el-form-item>
            <el-row>
              <hr />
            </el-row>
            <el-form-item label="实付金额">
              <font color="red">{{(orderInfo["actualAmount"]||0).toMoney()}}</font>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
    </div>

    <section id="choosetime">
      <dlgBookingTime :dlg_booking="dlg_booking" @onclose="closeDlgBbooking" :shopId="shopId" />
    </section>
  </el-card>
</template>

<script>
import dlgBookingTime from "./dlgBookingTime.vue";
import { orderCenterSvc as orderAPI } from "@/api/orderCenter/orderCenter";
import { appSvc as baoyang } from "@/api/baoyang/baoyang";

export default {
  name: "step4",
  props: {
    showCard: { type: Boolean, default: false },
    params: { type: Object, default: {} },
  },
  components: { dlgBookingTime },
  data() {
    return {
      loading: false,
      dlg_booking: false,
      reserverTime: "",
      payMethod: 1,
      remark: "",
      orderInfo: {
        allApplicableCoupons: [],
        userCouponId: 0,
      },
      allApplicableCoupons: [],
    };
  },
  computed: {
    shopId() {
      return this.params.choose_shop["id"] || 0;
    },
  },
  watch: {
    showCard: function (v, oldv) {
      if (v == true) {
        if (!this.orderInfo.orderId) {
          Object.assign(this.$data, this.$options.data());
          this.getOrderConfirm();
        } else {
          Object.assign(this.orderInfo, this.params.orderInfo || {});
        }
      }
    },
  },
  methods: {
    last() {
      this.$emit("dolast", {
        reserverTime: this.reserverTime,
        payMethod: this.payMethod,
        remark: this.remark,
      });
    },
    next() {
      if (this.reserverTime == "") {
        return this.$message.error("请选择预约时间！");
      }
      this.submitOrder().then((p) => {
        //打开订单列表
        this.$root.close_curpage("/orderCenter/orderList", true);

        // if (this.orderInfo.userCouponId != 0) {
        //   let tmp = this.allApplicableCoupons.filter(c => {
        //     return c.userCouponId == orderInfo.userCouponId;
        //   });
        //   this.orderInfo.couponDisplay =
        //     tmp.displayName +
        //     "【价值：" +
        //     tmp.value.toMoney() +
        //     "，有效期:" +
        //     tmp.startValidTime +
        //     "~" +
        //     tmp.endValidTime +
        //     "】";
        // }
        // this.$emit("donext", {
        //   orderInfo: this.orderInfo
        // });
      });
    },
    getOrderConfirm() {
      let productInfos = [];
      this.params.choose_products.forEach((p) => {
        productInfos.push({ pid: p.pid, number: p.count });
      });
      let p = {
        carId: this.params.carId,
        shopId: this.shopId,
        productInfos: productInfos,
        orderType: 2,
        userId: this.params.userId,
        productType: 5, //上门保养
        userCouponId: this.orderInfo.userCouponId || 0,
      };
      // console.log(this.params);
      this.loading = true;
      orderAPI
        .TrialCalcOrderAmount(p)
        .then((res) => {
          Object.assign(this.orderInfo, res.data || {});
          if (this.orderInfo.allApplicableCoupons.length > 0) {
            baoyang
              .getUserCoupons({
                userId: p.userId,
                userCouponId: this.orderInfo.allApplicableCoupons,
                entranceType: "Mine",
                pageIndex: 1,
                pageSize: 100,
              })
              .then((res) => {
                this.allApplicableCoupons = res.data["items"] || [];
              })
              .finally(() => {
                this.loading = false;
              });
          } else {
            this.loading = false;
          }
          if (p.userCouponId != 0) this.orderInfo.userCouponId = p.userCouponId;
        })
        .catch(() => {
          this.loading = false;
        });
    },

    submitOrder() {
      let productInfos = [];
      this.params.choose_products.forEach((p) => {
        productInfos.push({ pid: p.pid, number: p.count });
      });
      let p = {
        carId: this.params.carId,
        shopId: this.shopId,
        productInfos: productInfos,
        orderType: 2,
        channel: 3, //渠道 电销下单
        userId: this.params.userId,
        remark: this.remark,
        installType: this.params.installType,
        payMethod: this.payMethod, //1在线支付 2到店付款
        userCouponId: this.orderInfo.userCouponId || 0, //选择使用的用户优惠券Id（0未选择）
        terminalType: 6, //终端类型（0未设置 1小程序 2Android-App 3iOS-App 4S网站 5官网 6 boss）
        orderType: 2, //入口类型（0未设置 1轮胎 2保养 3美容 4 钣金维修 5 汽车改装 6 其他）
        produceType: 5, //产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生，5  上门服务）
        reserverTime: this.reserverTime, //预约时间
        receiverName: this.params.address.userName,
        receiverPhone: this.params.address.mobileNumber,
        receiverAddressId: this.params.address.addressId,
      };
      // console.log(JSON.stringify(p));
      return new Promise((resolve, reject) => {
        this.loading = true;
        orderAPI
          .SubmitOrder(p)
          .then((res) => {
            p.orderId = res.data;
            this.orderInfo = Object.assign(this.orderInfo, p);
            this.$message({
              type: "success",
              message: "订单提交成功",
            });

            return resolve(p);
          })
          .catch(() => {})
          .finally(() => {
            this.loading = false;
          });
      });
    },
    closeDlgBbooking(p) {
      this.dlg_booking = false;
      if (p != false) this.reserverTime = p;
    },
    change_coupon(v) {
      this.getOrderConfirm();
      // console.log(v);
    },
  },
};
</script>
<style lang="scss" scoped>
.rediaCar {
  margin-bottom: 10px;
  width: calc(100% - 30px);
}
</style>
