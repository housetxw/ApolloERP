<template>
  <div class="dashboard-container">
    <div class="dashboard-text">
      <!-- name: {{ userInfo.employeeName }}  -->
      <h3>  
      AERP多门店新零售互联网养车连锁系统
      </h3> 
      <div>
        <div class="content" style="width:800px;">
        <h4>系统简介</h4>

本系统基于SAAS构想，结合连锁ERP和互联网O2O平台经验而设计的多商户、多门店、多用户的一整套线上商城、门店管理、后台管理系统。

<h4>应用场景</h4>

主要应用于汽修、保养、洗车、汽车改装、汽车美容等连锁行业，提供线上预约线下服务、上门服务、多渠道支付、客户营销、商品进销存管理、会员卡、套餐卡、优惠券、积分管理、积分兑换、客户评论、财务对账、数据报表统计等功能，后续会持续加入更多新功能。

<h4>核心技术</h4>

  前端技术：vue、vue-cli、uniapp  <br>
  APP：uniapp、安卓原生、IOS原生（暂无）  <br>
  后端技术：.net（C#）微服务<br>

  <h4>功能模块</h4>

  小程序商城：下单，预约，活动，多门店切换，评论，积分，套餐卡，优惠券，在线支付  <br>
  小程序技师端（开发中，敬请期待）：下单、派工、采购、库存、回评、绩效、报表  <br>
  门店管理web端：订单、采购、库存、财务对账、客户管理、员工管理、门店报表  <br>
  公司管理web端：订单管理、公司采购、库存调拨、子公司管理、门店管理、员工管理、公司报表  <br>
  总部管理web端：公司管理、门店管理、平台产品、总部采购、总部库存、总部对账、优惠券管理、套餐卡管理、积分兑换管理、账号权限设置  <br>
  APP安卓原生：下单、收货、采购、库存、收款、对账、客户、员工管理、回评、绩效、门店报表  <br>

  <h4>在线体验</h4>

  门店\公司管理地址：shop.aerp.com.cn  <br>
  总部管理地址：boss.aerp.com.cn  <br>
  账户/密码：13811112222/12345678  <br>
  技术交流QQ群：221525146  <br>

<h4>商务合作联系方式</h4>
QQ：19652594<br>
Email：zhidash2020@163.com<br>

<br><br><br>
        </div>
      </div>
    </div>

    <div>
      <el-aside width="300px">
        <!-- <div class="aside"> -->
        <div class="t2">公告</div>
        <div class="co">
          <router-link to="/dash1"
            ><span class="co2">公告</span>
            <span class="t3">财务报销管理办法(试行)</span></router-link
          >
        </div>
        <div class="co">
          <router-link to="/dash2"
            ><span class="co2">公告</span>
            <span class="t3">行政管理制度通知</span>
          </router-link>
        </div>
        <div class="co">
          <router-link to="/dash4"
            ><span class="co2">公告</span>
            <span class="t3">关于钉钉考勤的通知</span>
          </router-link>
        </div>
      </el-aside>
    </div>
  </div>
  <!-- </div> -->
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "Dashboard",
  computed: {
    ...mapGetters(["userInfo"])
  },
  mounted() {},
  data() {
    this.chartSettingsshop = {
      stack: { 门店: ["今日门店", "门店总数"] },
      area: true,      
      labelMap:{        
        totalCount:'门店总数',
        everyDayCount:'今日门店'
      }      
    };
    this.chartSettingsuser = {
      stack: { 用户: ["今日用户", "累计用户"] },
      area: true,
       labelMap:{        
        totalCount:'累计用户',
        everyDayCount:'今日用户'
      }    
    };
    this.chartSettingsorder = {
      stack: { 订单: ["今日订单", "累计订单"] },
      area: true,
       labelMap:{        
        totalCount:'累计订单',
        everyDayCount:'今日订单'
      }  
    };
    this.chartSettingsMoney = {
      stack: { 订单: ["今日营收", "累计营收"] },
      area: true,
       labelMap:{        
        totalCount:'累计营收',
        everyDayCount:'今日营收'
      }
    };
    return {
      loading:true,
      shopchartData: {
        columns: ["day", "totalCount", "everyDayCount"],
        rows:[]
      },
        orderchartData: {
        columns: ["day", "totalCount", "everyDayCount"],
        rows:[]
      }, 
       userchartData: {
        columns: ["day", "totalCount", "everyDayCount"],
        rows:[]
      },
      moneyChartData:{
        columns: ["day", "totalCount", "everyDayCount"],
        rows:[]
      }
    };
  },
   created() {
    this.fetchData();
  },
  methods: {
fetchData(){
 
}

  }
};
</script>

<style lang="scss" scoped>
// .dashboard {
//   &-container {
//     margin: 30px;
//   }
//   &-text {
//     font-size: 30px;
//     line-height: 46px;
//   }
// }
.dashboard-container {
  flex-direction: row;
  display: flex;
  justify-content: center;
  // align-items: center;
  position: relative;
}
.el-aside {
  top: 0;
  right: 0;
  color: #fff;
  text-align: center;
  height: 100%;
  margin: 20px;
  margin-left: 40px;
}
.content {
  flex-direction: column;
 
  height: 100%;
  margin-left: 5px;
  width: 100%;
}
.i1 {
  max-width: 100%;
  max-height: 100%;
  display: block;
  margin: auto;
}

.box2 {
  margin-top: 20px;
  width: 100%;
}
.t2 {
  padding: 10px;
  text-align: left;
  color: #ff6324;
}
.co {
  padding: 10px;
  text-align: left;
  color: #333;
}
.co2 {
  border: 1px solid rgba(153, 153, 153, 1);
  border-radius: 4px;
}
.t3 {
  padding: 4px;
}
</style>
