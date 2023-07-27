
<template>
  <el-card class="box-card" v-if="showCard" v-loading="loading">
    <div slot="header" class="clearfix">
      <span>订单提交成功</span>
      <el-button style="float: right;" size="mini" type="primary" @click="next">新建订单</el-button>
    </div>
    <div class="bax-card-container">
      <el-form label-width="120px" ref="formCarInfo" size="mini">
        <el-divider content-position="left">客户信息</el-divider>
        <el-form-item label="客户">{{params.customer.userName +' 【电话：'+params.customer.userTel}}】</el-form-item>
        <el-form-item
          label="联系方式"
        >{{params.address.userName +' 【电话：'+ params.address.mobileNumber+' 地址：'+ params.address.addressLine}}】</el-form-item>
        <el-form-item
          label="车辆"
        >{{params.vehicle.carNumber +' | '+ params.vehicle.brand+' | '+ params.vehicle.vehicle+' | '+ params.vehicle.paiLiang+' | '+ params.vehicle.nian+' | '+ params.vehicle.salesName}}</el-form-item>

        <el-divider content-position="left">服务内容</el-divider>
          <el-form-item v-for="item in params.choose_products" :key="item.pid" :label="item.packageName">
            <el-col
              :span="16"
              style="padding-left:15px;color:#409EFF"
            >{{item.baoYangName}} &nbsp;&nbsp;x {{item.count}}</el-col>
            <el-col :span="8">
              <label style="padding-right:5px">单价</label>
              <font color="red">{{item.price.toMoney()}}</font>
            </el-col>
            
          </el-form-item>
        <el-divider content-position="left">安装方式&服务门店</el-divider>
        <el-form-item label="安装方式" required>
          <el-radio-group v-model="params.orderInfo.installType" disabled>
            <el-radio :label="2">上门保养</el-radio>
            <el-radio :label="1" disabled>到店安装</el-radio>
            <el-radio :label="3" disabled>无需安装</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="服务门店" required>{{params.choose_shop.simpleName}}</el-form-item>
      
        <el-row>
          <el-divider content-position="left">预约服务时间</el-divider>
        </el-row>
        <el-form-item label="预约时间" required>
          <label style="display:inline-block;width:150px">{{params.orderInfo.reserverTime}}</label>
        </el-form-item>
        <el-divider content-position="left">支付及其他</el-divider>
        <el-form-item label="支付方式">
          <el-radio-group v-model="params.orderInfo.payMethod" disabled>
            <el-radio :label="1">现场支付</el-radio>
            <el-radio :label="2">微信支付</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="优惠券">{{params.orderInfo.couponDisplay||'未使用'}}</el-form-item>

        <el-form-item label="留言">
          <el-input
            readonly
            type="textarea"
            placeholder="请输入客户留言"
            rows="5"
            v-model="params.orderInfo.remark"
            maxlength="300"
            show-word-limit
          ></el-input>
        </el-form-item>
        <el-row>
          <el-col style="width:calc(100% - 300px)">&nbsp;</el-col>
          <el-col style="width:300px">
            <el-row>&nbsp;</el-row>
            <el-form-item label="商品总金额">
              <font color="red">{{(params.orderInfo["totalProductAmount"]||0).toMoney()}}</font>
            </el-form-item>
            <el-form-item label="上门服务费">
              <font color="red">{{(params.orderInfo["totalDoorToDoorFee"]||0).toMoney()}}</font>
            </el-form-item>
            <el-form-item label="优惠">
              <font color="red">{{(params.orderInfo["totalCouponAmount"]||0).toMoney()}}</font>
            </el-form-item>
            <el-row>
              <hr />
            </el-row>
            <el-form-item label="实付金额">
              <font color="red">{{(params.orderInfo["actualAmount"]||0).toMoney()}}</font>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
    </div>
  </el-card>
</template>

<script>
export default {
  name: "step5",
  props: {
    showCard: { type: Boolean, default: false },
    params: { type: Object, default: {} }
  },
  data() {
    return {
      loading: false
    };
  },
  watch: {
    showCard: function(v, oldv) {
      if (v == true) {
        // console.log(this.params);
        this.orderInfo = this.params.orderInfo;
      }
    }
  },
  methods: {
    last() {},
    next() {
      this.$emit("donext",false);
    }
  }
};
</script>

