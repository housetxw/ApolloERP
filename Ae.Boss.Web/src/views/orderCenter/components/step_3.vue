
<template>
  <el-card class="box-card" v-show="showCard" v-loading="loading">
    <div slot="header" class="clearfix">
      <el-button style="float: left; padding: 3px 0" type="text" @click="last">上一步</el-button>
      <span>选择服务门店</span>
      <el-button
        style="float: right; padding: 3px 0"
        type="text"
        @click="next"
        v-if="JSON.stringify(choose_shop)!='{}'"
      >下一步</el-button>
    </div>
    <div class="bax-card-container">
      <el-form :model="formMolel" label-width="80px" ref="formCarInfo" size="mini">
        <!-- <el-divider content-position="left">安装方式&服务门店</el-divider> -->
        <el-form-item label="安装方式" required>
          <el-radio-group v-model="installtype">
            <el-radio :label="2">上门保养</el-radio>
            <el-radio :label="1" disabled>到店安装</el-radio>
            <el-radio :label="3" disabled>无需安装</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="服务门店" required>
          <el-table
            :data="shopData"
            size="mini"
            border
            style="width: 100%;"
            highlight-current-row
            @current-change="(curRow,oldRow)=>{ this.choose_shop = curRow;}"
          >
            <rg-table-column label="选择" align="center" width="60px" class-name="col-radio">
              <template slot-scope="scope">
                <el-radio v-model="choose_shop" :label="scope.row">&nbsp;</el-radio>
              </template>
            </rg-table-column>
            <rg-table-column label="门店名称" prop="simpleName" />
            <rg-table-column label="联系电话" prop="mobile" />
            <rg-table-column label="门店地址" prop="address" />
          </el-table>
        </el-form-item>
      </el-form>
    </div>
  </el-card>
</template>

<script>
import { shopManageSvc as shop } from "@/api/shopmanage/shopmanage";
export default {
  name: "step3",
  props: {
    showCard: { type: Boolean, default: false },
    params: { type: Object, default: {} }
  },
  data() {
    return {
      loading: false,
      formMolel: {},
      installtype: 2,
      shopData: [],
      srvPids: [],
      choose_shop: this.params.choose_shop || {}
    };
  },
  watch: {
    showCard: function(v, oldv) {
      if (v == true) {
        let pids = [];
        this.params.choose_products.forEach(p => {
          p.childProducts.forEach(s => {
            if (s.pid && s.pid.endWith("-FU")) {
              pids.push(s.pid);
            }
          });
        });
        // if (this.srvPids.join(",") != pids.join(",")) {
        //初始数据
        this.getShopList(pids);
        // }
      }
    }
  },
  methods: {
    last() {
      this.$emit("dolast", {
        choose_shop: this.choose_shop || {}
      });
    },
    next() {
      if (!this.choose_shop["id"]) {
        return this.$message.error("请选择服务门店");
      }
      this.$emit("donext", {
        choose_shop: this.choose_shop
      });
    },
    getShopList(pids) {
      let p = {
        shopType: 4, //门店类型：上门保养
        status: 0, //门店状态：正常
        // OnLineStatus: 1, //上架
        // CheckStatus: 2 ,//已通过审核
        address: this.params.address.addressLine,
        servicePId: pids,
        pageIndex: 1,
        pageSize: 20
      };
      this.loading = true;
      shop
        .GetShopListForServiceAsync(p)
        .then(res => {
          this.shopData = res.data.items || [];
          this.srvPids = pids;
        })
        .catch(() => {})
        .finally(() => {
          this.loading = false;
        });
    }
  }
};
</script>