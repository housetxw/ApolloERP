
<template>
  <el-card class="box-card" v-show="showCard" v-loading="loading">
    <div slot="header" class="clearfix">
      <el-button style="float: left; padding: 3px 0" type="text" @click="last">上一步</el-button>
      <span>选择服务内容</span>
      <el-button
        style="float: right; padding: 3px 0"
        type="text"
        @click="next"
        v-if="JSON.stringify(srvData)!='{}'"
      >下一步</el-button>
    </div>
    <div class="bax-card-container">
      <el-form :model="formMolel" label-width="80px" ref="formCarInfo" size="mini">
        <!-- <el-divider content-position="left">选择服务</el-divider> -->
        <el-form-item label="保养养护">
          <el-button disabled type="primary" class="s-mini" size="mini">更换服务</el-button>
          <label style="color:red">注意：基础保养服务和深度保养服务互斥，其余可多选。</label>
        </el-form-item>
        <el-row>
          <hr class="split" />
          <el-checkbox-group
            v-model="checkedCategory"
            size="mini"
            @change="handleCheckedSrvChange"
            v-if="JSON.stringify(srvData)!='{}'"
          >
            <el-checkbox
              v-for="(value,key,index) in srvData"
              :value="key"
              :label="value"
              :key="index"
            >{{value.packageName}}</el-checkbox>
          </el-checkbox-group>
          <el-form-item v-else>
            <label style="color:red">该车辆无适配的服务，请更换车辆！</label>
          </el-form-item>
          <hr class="split" />
        </el-row>

        <el-row v-for="(value,key,index) in checkedCategory" :key="value.pid">
          <el-row style="line-height:40px">
            <el-col :span="8">{{value.packageName}}</el-col>
            <el-col :span="8">
              <label style="padding-right:5px">小计</label>
              <font color="red">{{value.totalMoney.toMoney()}}</font>
            </el-col>
            <el-col :span="5">
              <el-button
                type="text"
                icon="el-icon-delete"
                @click="()=>{
                value.items.forEach(item=>{ 
                  $set(item, 'checked', false); });
                 $set(value, 'totalMoney', 0);
                }"
              >清空</el-button>
            </el-col>
            <el-col :span="3">
              <el-button type="text" icon="el-icon-d-arrow-right" @click="loadmore(value)">加载更多</el-button>
            </el-col>
          </el-row>
          <el-row v-for="(item,k) in value.items" :key="(index||0)*100+k">
            <el-row style="border:1px solid lightgray;line-height:40px">
              <el-col
                :span="15"
                :style="item.count>0?'padding-left:15px;color:#409EFF':'padding-left:15px;'"
              >
                <el-checkbox
                  v-model="item.checked"
                  @change="(v)=>{
                  if(v==false)  $set(value, 'totalMoney', 0);
                  else {
                    value.items.filter(p=>{ return p.checked==true}).forEach(p=>{if(p.pid !=item.pid) p.checked=false});//互斥
                    $set(value, 'totalMoney', item.price* item.count);
                  }
                }"
                >{{item.baoYangName}}</el-checkbox>
              </el-col>
              <el-col :span="4">
                <label style="padding-right:5px">单价</label>
                <font color="red">{{item.price.toMoney()}}</font>
              </el-col>
              <el-col :span="4">
                <label style="padding-right:5px">×</label>
                <el-input-number
                  v-model="item.count"
                  :precision="0"
                  :min="0"
                  :max="100"
                  style="width:90px"
                  size="mini"
                  @change="(cur,old)=>{
                    value.totalMoney =0;
                    value.items.forEach(item=>{ if(item.checked) value.totalMoney +=item.count*item.price;});
                  }"
                ></el-input-number>
              </el-col>
              <el-col :span="1">
                <el-button
                  type="text"
                  :icon="item.expand?'el-icon-arrow-up':'el-icon-arrow-down'"
                  @click="()=>{
                     $set(item, 'expand', !item.expand);
                  }"
                ></el-button>
              </el-col>
            </el-row>
            <el-row style="border:1px solid lightgray; border-top-width:0" v-show="item.expand">
              <el-form-item label="套餐描述" label-width="75px">{{value.suggestTip}}</el-form-item>
              <el-row style="line-height:40px" v-for="(subP,k) in item.childProducts" :key="k">
                <el-col :span="1">&nbsp;</el-col>
                <el-col :span="19" style="padding-left:15px">{{subP.displayName}}</el-col>
                <el-col :span="4">
                  <label style="padding-right:5px">数量</label>
                  × {{subP.count||0}}
                </el-col>
              </el-row>
            </el-row>
          </el-row>
        </el-row>
      </el-form>
    </div>
  </el-card>
</template>

<script>
import { appSvc as baoyang } from "@/api/baoyang/baoyang";
export default {
  name: "step2",
  props: {
    showCard: { type: Boolean, default: false },
    params: { type: Object, default: {} }
  },
  data() {
    return {
      loading: false,
      formMolel: {},
      installtype: 1,
      srvData: {},
      checkedCategory: [],
      choose_products: [],
      carId: "",
      addressId: ""
    };
  },
  watch: {
    showCard: function(v, oldv) {
      if (v == true) {
        if (this.carId != this.params.carId || this.params.reload) {
          //初始数据
          Object.assign(this.$data, this.$options.data());
          this.getBaoYangPackages(this.params || {});
        }
      }
    }
  },
  methods: {
    last() {
      this.$emit("dolast", {
        choose_products: this.choose_products || []
      });
    },
    next() {
      this.choose_products = [];
      this.checkedCategory.forEach(p => {
        if (p.totalMoney > 0) {
          p.items.forEach(item => {
            if (item.checked && item.count > 0)
              this.choose_products.push(Object.assign({}, item));
          });
        }
      });
      if (!this.choose_products.length) {
        return this.$message.error("请选择服务项目");
      }

      this.$emit("donext", {
        choose_products: this.choose_products
      });
    },
    getBaoYangPackages(params) {
      this.checkedCategory = [];
      this.loading = true;
      baoyang
        .getBaoYangPackages(params)
        .then(res => {
          res.data = res.data || [];
          let data = {};
          res.data.forEach(item => {
            if (item.packageItems.length == 0) return false; //continue
            item.packageItems.forEach(pkg => {
              if (pkg.items.length == 0) return false; //continue
              pkg.items.forEach(subP => {
                if (subP.products.length == 0) return false; //continue
                subP.products.forEach(product => {
                  data[pkg.packageType] = data[pkg.packageType] || {
                    packageType: pkg.packageType,
                    packageName: pkg.zhName,
                    packageGroup: pkg.groupName,
                    suggestTip: pkg.suggestTip,
                    baoYangType: subP.baoYangType,
                    totalMoney: 0,
                    items: []
                  };
                  data[pkg.packageType].totalMoney = 0; //金额小计
                  data[pkg.packageType].items.push(
                    Object.assign(
                      {
                        packageType: pkg.packageType,
                        packageName: pkg.zhName,
                        packageGroup: pkg.groupName,
                        baoYangType: subP.baoYangType,
                        baoYangName: product.displayName,
                        baoYangGroup: subP.groupName,
                        checked: false
                      },
                      product
                    )
                  );
                });
              });
            });
          });
          this.carId = params.carId;
          this.addressId = params.address.addressId;
          this.srvData = data;
          console.log("srvData====>", data);
          this.loading = false;
        })
        .catch(() => {
          this.loading = false;
        })
        .finally(() => {});
    },
    loadmore(pkg) {
      let p = {
        vehicle: this.params.vehicle,
        packageType: pkg.packageType,
        baoYangType: pkg.baoYangType,
        pidCount: pkg.items,
        provinceId: this.params.address.districtId,
        cityId: this.params.address.cityId,
        userId: this.params.userId
      };
      this.loading = true;
      baoyang
        .searchPackageProductsWithCondition(p)
        .then(res => {
          let data = res.data || [];
          data.products = data.products || [];
          if (data.products.length > 0) {
            data.products.forEach(item => {
              let find = pkg.items.filter(sub => {
                return sub.pid == item.pid;
              });

              if (find != null && find.length > 0) return false; //continue;
              pkg.items.push(
                Object.assign(
                  {
                    packageType: pkg.items[0].packageType,
                    packageName: pkg.items[0].packageName,
                    packageGroup: pkg.items[0].packageGroup,
                    baoYangType: pkg.items[0].baoYangType,
                    baoYangName: item.displayName,
                    baoYangGroup: pkg.items[0].baoYangGroup
                  },
                  item
                )
              );
              // pkg.totalMoney += (item.count || 0) * (item.price || 0); //金额小计
            });
          }
          this.loading = false;
        })
        .catch(() => {
          this.loading = false;
        })
        .finally(() => {});
    },
    handleCheckedSrvChange(value) {
      if (value.length == 0) return;
      let lastItem = value[value.length - 1];//最后一条记录为最新选择的
      this.checkedCategory = this.checkedCategory.filter(p => {
        //互斥处理：将与最后选择相冲突的记录剔除 
        return (
          p.packageGroup != lastItem.packageGroup ||
          p.packageType == lastItem.packageType
        );
      });
    }
  }
};
</script>