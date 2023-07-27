<template>
  <main class="container">
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
    >
      <template v-slot:condition>
        <el-form-item>
          <el-input
            v-model="condition.name"
            clearable
            placeholder="请输入规则名称"
          />
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="condition.actived"
            placeholder="状态"
            clearable
            style="width: 100px"
          >
            <el-option
              v-for="dict in activedStatusEnum"
              :key="dict.id"
              :label="dict.displayName"
              :value="dict.id"
            />
          </el-select>
        </el-form-item>
      </template>

      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
          >新增</el-button
        >
      </template>

      <template v-slot:tb_cols>
        <el-table-column label="规则Id" prop="id" :width="60"></el-table-column>
        <el-table-column label="规则名称" prop="name"></el-table-column>
        <el-table-column
          label="适用渠道"
          prop="channelName"
          :width="80"
        ></el-table-column>
        <el-table-column
          label="阈值"
          prop="threshold"
          :width="80"
        ></el-table-column>
        <el-table-column
          label="开始时间"
          prop="startTime"
          :width="128"
        ></el-table-column>
        <el-table-column
          label="结束时间"
          prop="endTime"
          :width="128"
        ></el-table-column>
        <el-table-column align="center" label="是否生效" :width="80">
          <template slot-scope="scope">
            <el-tag
              size="mini"
              :type="scope.row.actived === 1 ? '' : 'danger'"
              >{{ scope.row.actived === 1 ? "生效中" : "未生效" }}</el-tag
            >
          </template>
        </el-table-column>
        <el-table-column
          label="创建人"
          prop="createBy"
          :width="80"
        ></el-table-column>
        <el-table-column
          label="创建时间"
          prop="createTime"
          :width="128"
        ></el-table-column>
        <el-table-column align="center" label="操作" :width="90">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                @click="routeDetail(scope.row.id)"
                type="primary"
                size="mini"
                >编辑</el-button
              >
            </el-button-group>
          </template>
        </el-table-column>
      </template>
    </rg-page>

    <rg-dialog
      :title="formTitle"
      :visible.sync="dialogVisible"
      :beforeClose="cancel"
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          cancel();
        },
      }"
      :btnRemove="false"
      :footbar="true"
      width="80%"
      maxWidth="600px"
      minWidth="600px"
      minHeight="600px"
    >
      <template v-slot:content>
        <el-form :model="addRuleModel" size="mini" label-width="130px">
          <el-form-item label="规则名称" required>
            <el-input
              style="width: 300px"
              placeholder="请输入规则名称"
              v-model="addRuleModel.name"
            ></el-input>
          </el-form-item>
          <el-form-item label="开始时间" required>
            <el-date-picker
              style="width: 300px"
              v-model="addRuleModel.startTime"
              type="datetime"
              placeholder="请选择开始时间"
            >
            </el-date-picker>
          </el-form-item>
          <el-form-item label="结束时间" required>
            <el-date-picker
              style="width: 300px"
              v-model="addRuleModel.endTime"
              type="datetime"
              placeholder="请选择结束时间"
            >
            </el-date-picker>
          </el-form-item>
          <el-form-item label="适用渠道">
            <el-select style="width: 300px" v-model="addRuleModel.channel">
              <el-option
                v-for="item in activityChannelList"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="单用户最大领取数">
            <el-input
              style="width: 300px"
              placeholder=""
              v-model="addRuleModel.maxNumPerUser"
            ></el-input>
          </el-form-item>
          <el-form-item label="阀值">
            <el-input
              style="width: 300px"
              v-model="addRuleModel.threshold"
            ></el-input>
          </el-form-item>
          <el-form-item label="是否生效">
            <el-select style="width: 300px" v-model="addRuleModel.actived">
              <el-option
                v-for="item in activedStatusEnum"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="优惠券活动id">
            <el-input
              style="width: 300px"
              v-model="addRuleModel.couponActivityIds"
              placeholder="请输入优惠券活动id，英文逗号分隔"
            ></el-input>
          </el-form-item>
          <el-form-item label="产品pid">
            <el-input
              style="width: 300px"
              v-model="addRuleModel.pidLists"
              placeholder="请输入产品pid，英文逗号分隔"
            ></el-input>
          </el-form-item>
        </el-form>
      </template>

      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          size="mini"
          type="primary"
          :loading="btnSaveLoadingA"
          @click="addRule()"
          >提交</el-button
        >
      </template>
    </rg-dialog>

    <rg-dialog
      :title="formTitle1"
      :visible.sync="dialogVisible1"
      :beforeClose="cancel1"
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          cancel1();
        },
      }"
      :btnRemove="false"
      :footbar="true"
      width="80%"
      maxWidth="600px"
      minWidth="600px"
      minHeight="600px"
    >
      <template v-slot:content>
        <el-form :model="detail" size="mini" label-width="130px">
          <el-form-item label="规则名称" required>
            <el-input
              style="width: 300px"
              placeholder="请输入规则名称"
              v-model="detail.name"
            ></el-input>
          </el-form-item>
          <el-form-item label="开始时间" required>
            <el-date-picker
              style="width: 300px"
              v-model="detail.startTime"
              type="datetime"
              placeholder="请选择开始时间"
            >
            </el-date-picker>
          </el-form-item>
          <el-form-item label="结束时间" required>
            <el-date-picker
              style="width: 300px"
              v-model="detail.endTime"
              type="datetime"
              placeholder="请选择结束时间"
            >
            </el-date-picker>
          </el-form-item>
          <el-form-item label="适用渠道">
            <el-select style="width: 300px" v-model="detail.channel">
              <el-option
                v-for="item in activityChannelList"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="单用户最大领取数">
            <el-input
              style="width: 300px"
              placeholder=""
              v-model="detail.maxNumPerUser"
            ></el-input>
          </el-form-item>
          <el-form-item label="阀值">
            <el-input
              style="width: 300px"
              v-model="detail.threshold"
            ></el-input>
          </el-form-item>
          <el-form-item label="是否生效">
            <el-select style="width: 300px" v-model="detail.actived">
              <el-option
                v-for="item in activedStatusEnum"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="优惠券活动id">
            <el-input
              style="width: 300px"
              v-model="detail.couponActListStr"
              placeholder="请输入优惠券活动id，英文逗号分隔"
            ></el-input>
          </el-form-item>
          <el-form-item label="产品pid">
            <el-input
              style="width: 300px"
              v-model="detail.productsStr"
              placeholder="请输入产品pid，英文逗号分隔"
            ></el-input>
          </el-form-item>
        </el-form>
      </template>
      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          size="mini"
          type="primary"
          :loading="btnSaveLoading1"
          @click="editRule()"
          >保存</el-button
        >
      </template>
    </rg-dialog>
  </main>
</template>
<script>
import { couponSvc } from "@/api/coupon/coupon";
export default {
  name: "giftcouponconfig",
  data() {
    return {
      condition: {
        name: "",
        pageIndex: 1,
        pageSize: 20,
        actived: undefined,
      },
      tableLoading: false,
      tableData: [],
      currentPage: 1,
      totalList: 0,
      activedStatusEnum: [
        { id: 1, displayName: "生效中" },
        { id: 0, displayName: "未生效" },
      ],
      formTitle: "新增",
      dialogVisible: false,
      addRuleModel: {
        name: "",
        channel: 0,
        maxNumPerUser: 0,
        threshold: 0,
        startTime: "",
        endTime: "",
        actived: 0,
        couponActivityId: [],
        couponActivityIds: "",
        pidList: [],
        pidLists: "",
      },
      activityChannelList: [
        { id: 0, displayName: "未设置" },
        { id: 1, displayName: "线下渠道" },
        { id: 2, displayName: "线上渠道" },
      ],
      btnSaveLoadingA: false,
      detail: {
        id: 0,
        name: "",
        channel: 0,
        maxNumPerUser: 0,
        threshold: 0,
        startTime: "",
        endTime: "",
        actived: 0,
        createBy: "",
        createTime: "",
        couponActList: [],
        products: [],
        couponActListStr: "",
        productsStr: "",
      },
      formTitle1: "详情",
      dialogVisible1: false,
      btnSaveLoading1: false,
    };
  },
  created() {
    this.fetchData();
  },
  methods: {
    fetchData() {
      this.getPaged();
    },
    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    },
    pageChange(p) {
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    getPaged(flag) {
      this.tableLoading = true;
      couponSvc
        .getGiftCouponRulePageList(this.condition)
        .then(
          (res) => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (flag) {
              this.$message({
                message: res.message || "查询成功",
                type: "success",
              });
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });
    },
    add() {
      this.addRuleModel = {
        name: "",
        channel: 0,
        maxNumPerUser: 0,
        threshold: 0,
        startTime: "",
        endTime: "",
        actived: 0,
        couponActivityId: [],
        couponActivityIds: "",
        pidList: [],
        pidLists: "",
      };
      this.dialogVisible = true;
    },
    routeDetail(giftId) {
      var detailRequest = {
        giftId: giftId,
      };
      couponSvc
        .getGiftCouponRuleDetail(detailRequest)
        .then(
          (res) => {
            var data = res.data;
            if (data != null) {
              Object.assign(this.detail, data);
              var s1 = "";
              for (var t = 0; t < data.couponActList.length; t++) {
                s1 += data.couponActList[t].actId + ",";
              }
              var s2 = "";
              for (var t = 0; t < data.products.length; t++) {
                s2 += data.products[t].pid + ",";
              }
              this.detail.couponActListStr = s1.substr(0, s1.length - 1);
              this.detail.productsStr = s2.substr(0, s2.length - 1);
              this.dialogVisible1 = true;
            } else {
              this.$message({ message: "当前数据有误", type: "error" });
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    cancel() {
      this.dialogVisible = false;
    },
    cancel1() {
      this.dialogVisible1 = false;
    },
    addRule() {
      if (this.addRuleModel.name == "") {
        this.$message({ message: "请输入规则名称", type: "warning" });
        return;
      }
      if (this.addRuleModel.startTime == "") {
        this.$message({ message: "请输入开始时间", type: "warning" });
        return;
      }
      if (this.addRuleModel.endTime == "") {
        this.$message({ message: "请输入结束时间", type: "warning" });
        return;
      }
      var r = /(^\+?[1-9][0-9]*$)|(^[0]$)/; //正整数或0
      if (!r.test(this.addRuleModel.maxNumPerUser)) {
        this.$message({
          message: "单用户最大领取数输入有误，请输入非负整数",
          type: "warning",
        });
        return;
      }
      var s = /(^[1-9](\d+)?(\.\d{1,2})?$)|(^\d\.\d{1,2}$)|(^[0]$)/;
      if (!s.test(this.addRuleModel.threshold)) {
        this.$message({
          message: "阀值输入有误，请输入最多2位小数的非负数",
          type: "warning",
        });
        return;
      }

      var q = /^\+?[1-9][0-9]*$/; //正整数或0
      if (this.addRuleModel.couponActivityIds == "") {
        this.$message({ message: "请输入优惠券活动id", type: "warning" });
        return;
      }
      var actList = this.addRuleModel.couponActivityIds.split(",");
      for (var t = 0; t < actList.length; t++) {
        if (!q.test(actList[t])) {
          this.$message({
            message: "优惠券活动id输入有误，请重新确认",
            type: "warning",
          });
          return;
        }
      }
      this.addRuleModel.couponActivityId = actList;

      if (this.addRuleModel.pidLists == "") {
        this.$message({ message: "请输入产品pid", type: "warning" });
        return;
      }
      this.addRuleModel.pidList = this.addRuleModel.pidLists.split(",");

      this.btnSaveLoadingA = true;
      couponSvc
        .addGiftCouponRule(this.addRuleModel)
        .then(
          (res) => {
            var data = res.data;
            this.$message({ message: "提交成功", type: "success" });
            this.dialogVisible = false;
            this.getPaged();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.btnSaveLoadingA = false;
        });
    },
    editRule() {
      if (this.detail.name == "") {
        this.$message({ message: "请输入规则名称", type: "warning" });
        return;
      }
      if (this.detail.startTime == "") {
        this.$message({ message: "请输入开始时间", type: "warning" });
        return;
      }
      if (this.detail.endTime == "") {
        this.$message({ message: "请输入结束时间", type: "warning" });
        return;
      }
      var r = /(^\+?[1-9][0-9]*$)|(^[0]$)/; //正整数或0
      if (!r.test(this.detail.maxNumPerUser)) {
        this.$message({
          message: "单用户最大领取数输入有误，请输入非负整数",
          type: "warning",
        });
        return;
      }
      var s = /(^[1-9](\d+)?(\.\d{1,2})?$)|(^\d\.\d{1,2}$)|(^[0]$)/;
      if (!s.test(this.detail.threshold)) {
        this.$message({
          message: "阀值输入有误，请输入最多2位小数的非负数",
          type: "warning",
        });
        return;
      }

      var q = /^\+?[1-9][0-9]*$/; //正整数或0
      if (this.detail.couponActListStr == "") {
        this.$message({ message: "请输入优惠券活动id", type: "warning" });
        return;
      }
      var actList = this.detail.couponActListStr.split(",");
      for (var t = 0; t < actList.length; t++) {
        if (!q.test(actList[t])) {
          this.$message({
            message: "优惠券活动id输入有误，请重新确认",
            type: "warning",
          });
          return;
        }
      }

      if (this.detail.productsStr == "") {
        this.$message({ message: "请输入产品pid", type: "warning" });
        return;
      }
      var editRequest = {
        id: this.detail.id,
        name: this.detail.name,
        channel: this.detail.channel,
        maxNumPerUser: this.detail.maxNumPerUser,
        threshold: this.detail.threshold,
        startTime: this.detail.startTime,
        endTime: this.detail.endTime,
        actived: this.detail.actived,
        couponActivityId: actList,
        pidList: this.detail.productsStr.split(","),
      };

      this.btnSaveLoading1 = true;
      couponSvc
        .editGiftCouponRule(editRequest)
        .then(
          (res) => {
            var data = res.data;
            this.$message({ message: "保存成功", type: "success" });
            this.dialogVisible1 = false;
            this.getPaged();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.btnSaveLoading1 = false;
        });
    },
  },
};
</script>