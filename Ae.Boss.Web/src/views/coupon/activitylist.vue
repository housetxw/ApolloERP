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
            class="input"
            clearable
            placeholder="请输入活动名称"
            style="width: 180px"
            prefix-icon="el-icon-search"
            v-model="condition.name"
          >
          </el-input>
        </el-form-item>

        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="活动ID"
            style="width: 110px"
            prefix-icon="el-icon-search"
            v-model="condition.couponActivityId"
          >
          </el-input>
        </el-form-item>

        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="优惠券ID"
            style="width: 110px"
            prefix-icon="el-icon-search"
            v-model="condition.couponRuleId"
          >
          </el-input>
        </el-form-item>

        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="兑换码"
            style="width: 150px"
            prefix-icon="el-icon-search"
            v-model="condition.code"
          >
          </el-input>
        </el-form-item>

        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="活动编码"
            style="width: 120px"
            prefix-icon="el-icon-search"
            v-model="condition.codeId"
          >
          </el-input>
        </el-form-item>

        <el-form-item>
          <el-select
            style="width: 120px"
            v-model="condition.status"
            filterable
            placeholder="-请选择活动状态-"
          >
            <el-option
              v-for="item in activityStatusEnum"
              :key="item.id"
              :label="item.displayName"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
      </template>

      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add"
          >添加</el-button
        >
      </template>

      <template v-slot:tb_cols>
        <el-table-column
          label="活动Id"
          prop="couponActivityId"
          :width="80"
        ></el-table-column>
        <el-table-column label="活动名称" prop="name"></el-table-column>
        <el-table-column label="优惠券名称">
          <template slot-scope="scope">
            <span>{{ scope.row.couponName + " - " + scope.row.couponId }}</span>
          </template>
        </el-table-column>
        <el-table-column
          label="活动开始时间"
          prop="startTime"
        ></el-table-column>
        <el-table-column label="活动结束时间" prop="endTime"></el-table-column>
        <el-table-column label="状态">
          <template slot-scope="scope">
            <span>{{ scope.row.statusName }}</span>
            <span
              style="color: red"
              v-if="
                Date.parse(scope.row.startTime) > new Date() &&
                scope.row.status == 1
              "
            >
              （未开始）
            </span>
            <span
              style="color: red"
              v-else-if="
                Date.parse(scope.row.endTime) < new Date() &&
                scope.row.status == 1
              "
            >
              （已结束）
            </span>
          </template>
        </el-table-column>
        <el-table-column  label="发放总数"  prop="totalNum"  :width="80" ></el-table-column>
        <el-table-column  label="领取总数"  prop="receiveNum"  :width="80" ></el-table-column>
        <el-table-column
          label="兑换码"
          prop="code"
          :width="100"
        ></el-table-column>
        <el-table-column label="创建时间" prop="createTime"></el-table-column>
        <el-table-column label="操作" :width="120">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                @click="getCouponActivityDetail(scope.row.couponActivityId)"
                type="primary"
                size="mini"
                >查看详情</el-button
              >
              <el-button
                v-show="scope.row.status == 0"
                :loading="btnSaveLoadingP"
                @click="publishActivity(scope.row.couponActivityId, 0)"
                type="success"
                size="mini"
                >发布</el-button
              >
              <el-button
                v-show="
                  scope.row.status == 1 &&
                  Date.parse(scope.row.startTime) <= new Date() &&
                  Date.parse(scope.row.endTime) >= new Date()
                "
                @click="sendCoupon(scope.row.couponActivityId)"
                size="mini"
                type="warning"
                >塞券</el-button
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
    >
      <template v-slot:content>
        <el-form :model="addActivityModel" size="mini" label-width="130px">
          <el-form-item label="活动名称" required>
            <el-input
              style="width: 300px"
              placeholder="请输入活动名称"
              v-model="addActivityModel.name"
            ></el-input>
          </el-form-item>
          <el-form-item label="活动开始时间" required>
            <el-date-picker
              style="width: 300px"
              v-model="addActivityModel.startTime"
              type="datetime"
              placeholder="请选择开始时间"
            >
            </el-date-picker>
          </el-form-item>
          <el-form-item label="活动结束时间" required>
            <el-date-picker
              style="width: 300px"
              v-model="addActivityModel.endTime"
              type="datetime"
              placeholder="请选择结束时间"
            >
            </el-date-picker>
          </el-form-item>
          <el-form-item label="选择优惠券" required="">
            <el-button @click="chooseCoupon(0)" type="primary" size="mini"
              >选择优惠券</el-button
            >
            <span>{{ addActivityModel.couponRuleName }}</span>
          </el-form-item>
          <el-form-item label="兑换码">
            <el-input
              style="width: 300px"
              placeholder="请输入兑换码"
              v-model="addActivityModel.code"
            ></el-input>
          </el-form-item>
          <el-form-item label="活动链接">
            <el-input
              style="width: 300px"
              placeholder="请输入活动链接"
              v-model="addActivityModel.url"
            ></el-input>
          </el-form-item>
          <el-form-item label="渠道">
            <el-select style="width: 300px" v-model="addActivityModel.channel">
              <el-option
                v-for="item in activityChannelList"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="是否仅新用户">
            <el-checkbox v-model="addActivityModel.isNewUserOnly"
              >仅新用户使用</el-checkbox
            >
          </el-form-item>
          <el-form-item label="单用户最大领取数">
            <el-input
              style="width: 300px"
              placeholder=""
              v-model="addActivityModel.maxNumPerUser"
            ></el-input>
          </el-form-item>
          <el-form-item label="是否积分兑换活动">
            <el-checkbox v-model="addActivityModel.isIntegralExchange"
              >积分兑换</el-checkbox
            >
          </el-form-item>
          <el-form-item
            label="积分类型"
            v-show="addActivityModel.isIntegralExchange"
          >
            <el-select
              style="width: 300px"
              v-model="addActivityModel.needIntegralType"
            >
              <el-option
                v-for="item in integralTypeList"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item
            label="需要积分数量"
            v-show="addActivityModel.isIntegralExchange"
          >
            <el-input
              style="width: 300px"
              placeholder=""
              v-model="addActivityModel.needIntegralNum"
            ></el-input>
          </el-form-item>
          <el-form-item label="发放总数量">
            <el-input
              style="width: 300px"
              placeholder=""
              v-model="addActivityModel.totalNum"
            ></el-input>
          </el-form-item>
          <el-form-item label="单次发放数量">
            <el-input
              style="width: 300px"
              placeholder=""
              v-model="addActivityModel.num"
            ></el-input>
          </el-form-item>
          <el-form-item label="发放方式">
            <el-select
              style="width: 300px"
              v-model="addActivityModel.grantPattern"
            >
              <el-option
                v-for="item in grantPatternList"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="父级活动id">
            <el-input
              style="width: 300px"
              placeholder=""
              v-model="addActivityModel.parentId"
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
          @click="submitActicityData()"
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
      height="542px"
      minWidth="600px"
    >
      <template v-slot:content>
        <el-input
          class="input"
          size="mini"
          clearable
          placeholder="请输入优惠券名称"
          style="width: 180px"
          prefix-icon="el-icon-search"
          v-model="coupon.condition.displayName"
        >
        </el-input>
        <el-button
          type="warning"
          size="mini"
          icon="el-icon-search"
          @click="searchCoupon()"
          >搜索</el-button
        >
        <el-table
          :data="coupon.tableData"
          border
          stripe
          style="margin-top: 10px"
          v-loading="coupon.tableLoading"
          height="350px"
          size="mini"
        >
          <el-table-column label="操作" :width="60">
            <template slot-scope="scope">
              <el-button-group>
                <el-button
                  @click="chooseRule(scope.row)"
                  type="primary"
                  size="mini"
                  >选择</el-button
                >
              </el-button-group>
            </template>
          </el-table-column>
          <el-table-column
            label="优惠券Id"
            prop="couponId"
            :width="70"
          ></el-table-column>
          <el-table-column
            label="优惠券名称"
            prop="displayName"
          ></el-table-column>
          <el-table-column label="类型" prop="typeName"></el-table-column>
          <el-table-column
            label="面值"
            prop="value"
            :width="60"
            align="right"
          ></el-table-column>
          <el-table-column
            label="阀值"
            prop="threshold"
            :width="60"
            align="right"
          ></el-table-column>
        </el-table>
      </template>
    </rg-dialog>

    <rg-dialog
      :title="formTitle2"
      :visible.sync="dialogVisible2"
      :beforeClose="cancel2"
      :loading="formLoadingD"
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          cancel2();
        },
      }"
      :btnRemove="false"
      :footbar="true"
      width="80%"
      maxWidth="600px"
      minWidth="600px"
    >
      <template v-slot:content>
        <el-form :model="acticityDetail" size="mini" label-width="130px">
          <el-form-item label="活动名称" required>
            <el-input
              style="width: 300px"
              :readonly="acticityDetail.status != 0"
              placeholder="请输入活动名称"
              v-model="acticityDetail.name"
            ></el-input>
          </el-form-item>
          <el-form-item label="活动开始时间" required>
            <el-date-picker
              style="width: 300px"
              :readonly="acticityDetail.status != 0"
              v-model="acticityDetail.startTime"
              type="datetime"
              placeholder="请选择开始时间"
            >
            </el-date-picker>
          </el-form-item>
          <el-form-item label="活动结束时间" required>
            <el-date-picker
              style="width: 300px"
              :readonly="acticityDetail.status != 0"
              v-model="acticityDetail.endTime"
              type="datetime"
              placeholder="请选择结束时间"
            >
            </el-date-picker>
          </el-form-item>
          <el-form-item label="选择优惠券" required="">
            <span>{{
              acticityDetail.couponId +
              " - " +
              acticityDetail.couponDisplayName +
              " - " +
              acticityDetail.couponName
            }}</span>
            <el-button
              @click="chooseCoupon(1)"
              :disabled="acticityDetail.status != 0"
              type="primary"
              size="mini"
              >更换</el-button
            >
          </el-form-item>
          <el-form-item label="兑换码">
            <el-input
              style="width: 300px"
              :readonly="acticityDetail.status != 0"
              placeholder="请输入兑换码"
              v-model="acticityDetail.code"
            ></el-input>
          </el-form-item>
          <el-form-item label="活动链接">
            <el-input
              style="width: 300px"
              :readonly="acticityDetail.status != 0"
              placeholder="请输入活动链接"
              v-model="acticityDetail.url"
            ></el-input>
          </el-form-item>
          <el-form-item label="渠道">
            <el-select
              style="width: 300px"
              v-model="acticityDetail.channel"
              :disabled="acticityDetail.status != 0"
            >
              <el-option
                v-for="item in activityChannelList"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="是否仅新用户">
            <el-checkbox
              v-model="acticityDetail.isNewUserOnly"
              :disabled="acticityDetail.status != 0"
              >仅新用户使用</el-checkbox
            >
          </el-form-item>
          <el-form-item label="单用户最大领取数">
            <el-input
              style="width: 300px"
              :readonly="acticityDetail.status != 0"
              placeholder=""
              v-model="acticityDetail.maxNumPerUser"
            ></el-input>
          </el-form-item>
          <el-form-item label="是否积分兑换活动">
            <el-checkbox
              v-model="acticityDetail.isIntegralExchange"
              :disabled="acticityDetail.status != 0"
              >积分兑换</el-checkbox
            >
          </el-form-item>
          <el-form-item
            label="积分类型"
            v-show="acticityDetail.isIntegralExchange"
          >
            <el-select
              style="width: 300px"
              v-model="acticityDetail.needIntegralType"
              :disabled="acticityDetail.status != 0"
            >
              <el-option
                v-for="item in integralTypeList"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item
            label="需要积分数量"
            v-show="acticityDetail.isIntegralExchange"
          >
            <el-input
              style="width: 300px"
              :readonly="acticityDetail.status != 0"
              placeholder=""
              v-model="acticityDetail.needIntegralNum"
            ></el-input>
          </el-form-item>
          <el-form-item label="发放总数量">
            <el-input
              style="width: 300px"
              :disabled="btn_modify == '修改'"
              placeholder=""
              v-model="acticityDetail.totalNum"
            ></el-input>
            <el-button
              @click="changeTotalNum()"
              v-show="acticityDetail.status != 0"
              type="primary"
              :loading="changeTotalNumLoading"
              size="mini"
              >{{ btn_modify }}</el-button
            >
          </el-form-item>
          <el-form-item label="单次发放数量">
            <el-input
              style="width: 300px"
              :readonly="acticityDetail.status != 0"
              placeholder=""
              v-model="acticityDetail.num"
            ></el-input>
          </el-form-item>
          <el-form-item label="已领取数量">
            <el-input
              style="width: 300px"
              :disabled="true"
              placeholder=""
              v-model="acticityDetail.receiveNum"
            ></el-input>
          </el-form-item>
          <el-form-item label="发放方式">
            <el-select
              style="width: 300px"
              v-model="acticityDetail.grantPattern"
              :disabled="acticityDetail.status != 0"
            >
              <el-option
                v-for="item in grantPatternList"
                :key="item.id"
                :label="item.displayName"
                :value="item.id"
              >
              </el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="父级活动id">
            <el-input
              style="width: 300px"
              :readonly="acticityDetail.status != 0"
              placeholder=""
              v-model="acticityDetail.parentId"
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
          v-show="acticityDetail.status == 0"
          @click="editActicityData()"
          >保存</el-button
        >

        <el-button
          size="mini"
          type="success"
          :loading="btnSaveLoading2"
          v-show="acticityDetail.status == 0"
          @click="publishActivity(acticityDetail.couponActivityId, 1)"
          >发布</el-button
        >

        <el-button
          size="mini"
          type="warning"
          :loading="btnSaveLoading3"
          v-show="acticityDetail.status == 1"
          @click="pauseActivity(acticityDetail.couponActivityId)"
          >暂停领取</el-button
        >

        <el-button
          size="mini"
          type="warning"
          :loading="btnSaveLoading4"
          v-show="acticityDetail.status == 2"
          @click="regainActivity(acticityDetail.couponActivityId)"
          >恢复领取</el-button
        >

        <el-button
          size="mini"
          type="danger"
          :loading="btnSaveLoading5"
          v-show="acticityDetail.status != 3"
          @click="deleteActivity(acticityDetail.couponActivityId)"
          >作废</el-button
        >
      </template>
    </rg-dialog>

    <rg-dialog
      :title="formTitle3"
      :visible.sync="dialogVisible3"
      :beforeClose="cancel3"
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          cancel3();
        },
      }"
      :btnRemove="false"
      :footbar="true"
      width="80%"
      maxWidth="600px"
      minWidth="600px"
    >
      <template v-slot:content>
        <el-input
          clearable
          size="mini"
          placeholder="请输入客户手机号"
          style="width: 180px"
          prefix-icon="el-icon-search"
          v-model="searchUser.condition.userTel"
        >
        </el-input>
        <el-input
          clearable
          size="mini"
          placeholder="请输入客户姓名"
          style="width: 180px"
          prefix-icon="el-icon-search"
          v-model="searchUser.condition.userName"
        >
        </el-input>
        <el-button
          type="warning"
          size="mini"
          icon="el-icon-search"
          @click="searchUserDialog()"
          >搜索</el-button
        >
        <el-table
          :data="searchUser.tableData"
          v-loading="searchUser.tableLoading"
          border
          stripe
          style="margin-top: 10px"
          height="350px"
          size="mini"
        >
          <el-table-column label="操作" :width="60" align="center">
            <template slot-scope="scope">
              <el-checkbox v-model="scope.row.defaultCheck"></el-checkbox>
            </template>
          </el-table-column>
          <el-table-column label="客户姓名" prop="userName"></el-table-column>
          <el-table-column label="客户昵称" prop="nickName"></el-table-column>
          <el-table-column
            label="手机号"
            prop="userTel"
            :width="120"
          ></el-table-column>
          <el-table-column
            label="性别"
            prop="gender"
            :width="70"
          ></el-table-column>
        </el-table>
        <section class="pagination">
          <el-pagination
            background
            :page-size="10"
            :page-sizes="[10, 30, 50, 100]"
            layout="total, sizes, prev, pager, next, jumper"
            :total="searchUser.totalList"
            @current-change="pageTurning1"
            :current-page.sync="searchUser.currentPage"
            @size-change="sizeChange1"
          ></el-pagination>
        </section>
      </template>

      <template v-slot:footer>
        <el-button
          icon="el-icon-check"
          size="mini"
          type="primary"
          :loading="btnSendLoading"
          @click="submitUserCoupon()"
          >确认塞券</el-button
        >
      </template>
    </rg-dialog>
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { couponSvc } from "@/api/coupon/coupon";
export default {
  name: "activitylist",
  data() {
    return {
      btn_modify: "修改",
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      condition: {
        pageIndex: 1,
        pageSize: 20,
        name: "",
        couponActivityId: "",
        couponRuleId: "",
        code: "",
        codeId: "",
        status: -1,
      },
      tableData: [],
      coupon: {
        tableLoading: false,
        currentPage: 1,
        totalList: 0,
        condition: {
          pageIndex: 1,
          pageSize: 100,
          displayName: "",
        },
        tableData: [],
      },
      activityStatusEnum: [
        { id: -1, displayName: "全部" },
        { id: 0, displayName: "未发布" },
        { id: 1, displayName: "可领取" },
        { id: 2, displayName: "暂停领取" },
        { id: 3, displayName: "已作废" },
      ],
      formTitle: "添加活动配置",
      dialogVisible: false,
      addActivityModel: {
        name: "",
        code: "",
        couponRuleId: 0,
        couponRuleName: "",
        channel: 0,
        isNewUserOnly: false,
        maxNumPerUser: 0,
        isIntegralExchange: false,
        needIntegralType: 0,
        needIntegralNum: 0,
        totalNum: 0,
        num: 1,
        url: "",
        startTime: "",
        endTime: "",
        grantPattern: 0,
        parentId: 0,
      },
      activityChannelList: [],
      integralTypeList: [],
      grantPatternList: [
        { id: 0, displayName: "未设置" },
        { id: 1, displayName: "系统发放-主动促发" },
        { id: 2, displayName: "系统发放-被动响应" },
        { id: 3, displayName: "人工发放" },
      ],
      formTitle1: "选择优惠券",
      dialogVisible1: false,
      btnSaveLoadingA: false,
      formTitle2: "活动配置详情",
      dialogVisible2: false,
      btnSaveLoadingP: false,
      acticityDetail: {
        couponActivityId: 0,
        name: "",
        couponId: 0,
        couponDisplayName: "",
        couponName: "",
        startTime: "",
        endTime: "",
        createTime: "",
        status: 0,
        statusName: "",
        code: "",
        channel: 0,
        isNewUserOnly: false,
        maxNumPerUser: 0,
        isIntegralExchange: false,
        needIntegralType: 0,
        needIntegralNum: 0,
        totalNum: 0,
        num: 0,
        receiveNum: 0,
        url: "",
        grantPattern: 0,
        parentId: 0,
      },
      formLoadingD: false,
      btnSaveLoading1: false,
      changeTotalNumLoading: false,
      btnSaveLoading2: false,
      btnSaveLoading3: false,
      btnSaveLoading4: false,
      btnSaveLoading5: false,
      detailOrEdit: 0,
      formTitle3: "搜索用户",
      dialogVisible3: false,
      currentActivityId: 0,
      searchUser: {
        tableLoading: false,
        currentPage: 1,
        totalList: 0,
        condition: {
          userName: "",
          userTel: "",
          pageIndex: 1,
          pageSize: 10,
        },
        tableData: [],
      },
      btnSendLoading: false,
    };
  },
  created() {
    this.fetchData();
    this.getCouponActivityChannel();
    this.getCouponActivityIntegralType();
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
        .getCouponActivityList(this.condition)
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
      this.addActivityModel = {
        name: "",
        code: "",
        couponRuleId: 0,
        couponRuleName: "",
        channel: 0,
        isNewUserOnly: false,
        maxNumPerUser: 0,
        isIntegralExchange: false,
        needIntegralType: 0,
        needIntegralNum: 0,
        totalNum: 0,
        num: 1,
        url: "",
        startTime: "",
        endTime: "",
        grantPattern: 0,
        parentId: 0,
      };
      this.dialogVisible = true;
    },
    cancel() {
      this.dialogVisible = false;
    },
    cancel1() {
      this.dialogVisible1 = false;
    },
    cancel2() {
      this.dialogVisible2 = false;
    },
    getCouponActivityChannel() {
      couponSvc
        .getCouponActivityChannel()
        .then(
          (res) => {
            var data = res.data;
            this.activityChannelList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getCouponActivityIntegralType() {
      couponSvc
        .getCouponActivityIntegralType()
        .then(
          (res) => {
            var data = res.data;
            this.integralTypeList = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    chooseCoupon(isDetail) {
      this.detailOrEdit = isDetail;
      this.dialogVisible1 = true;
    },
    searchCoupon() {
      if (this.coupon.condition.displayName == "") {
        this.$message({
          message: "请输入优惠券名称搜索",
          type: "warning",
        });
        return;
      }
      this.coupon.tableLoading = true;
      couponSvc
        .getCouponRuleList(this.coupon.condition)
        .then(
          (res) => {
            var data = res.data;
            this.coupon.tableData = data.items;
            this.coupon.totalList = data.totalItems;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.coupon.tableLoading = false;
        });
    },
    chooseRule(couponRow) {
      if (this.detailOrEdit == 0) {
        this.addActivityModel.couponRuleId = couponRow.couponId;
        this.addActivityModel.couponRuleName = couponRow.displayName;
      } else {
        this.acticityDetail.couponId = couponRow.couponId;
        this.acticityDetail.couponDisplayName = couponRow.displayName;
        this.acticityDetail.couponName = couponRow.name;
      }
      this.dialogVisible1 = false;
    },
    testNum(numData) {
      var r = /^\+?[1-9][0-9]*$/; //正整数
      if (numData === "") {
        return false;
      }
      if (numData == 0) {
        return true;
      }
      return r.test(numData);
    },
    testNumZ(numData) {
      var r = /^\+?[1-9][0-9]*$/; //正整数
      if (numData === "") {
        return false;
      }
      return r.test(numData);
    },
    submitActicityData() {
      if (this.addActivityModel.name == "") {
        this.$message({ message: "请输入活动", type: "warning" });
        return;
      }
      if (this.addActivityModel.startTime == "") {
        this.$message({ message: "请输入活动开始时间", type: "warning" });
        return;
      }
      if (this.addActivityModel.endTime == "") {
        this.$message({ message: "请输入活动结束时间", type: "warning" });
        return;
      }
      if (this.addActivityModel.couponRuleId <= 0) {
        this.$message({ message: "请选择优惠券", type: "warning" });
        return;
      }
      if (!this.testNum(this.addActivityModel.maxNumPerUser)) {
        this.$message({
          message: "单用户最大领取数 输入有误",
          type: "warning",
        });
        return;
      }
      if (this.addActivityModel.isIntegralExchange) {
        // if (this.addActivityModel.needIntegralType == 0) {
        //   this.$message({ message: "请选择积分类型", type: "warning" });
        //   return;
        // }
        if (!this.testNumZ(this.addActivityModel.needIntegralNum)) {
          this.$message({ message: "请输入需要积分数量", type: "warning" });
          return;
        }
      } else {
        this.addActivityModel.needIntegralType = 0;
        this.addActivityModel.needIntegralNum = 0;
      }

      if (!this.testNum(this.addActivityModel.totalNum)) {
        this.$message({ message: "发放总数量 输入有误", type: "warning" });
        return;
      }

      if (!this.testNumZ(this.addActivityModel.num)) {
        this.$message({ message: "单次发放数量 输入有误", type: "warning" });
        return;
      }

      if (!this.testNum(this.addActivityModel.parentId)) {
        this.$message({ message: "父级活动id 输入有误", type: "warning" });
        return;
      }

      this.btnSaveLoadingA = true;
      couponSvc
        .addCouponActivity(this.addActivityModel)
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
    publishActivity(activityId, type) {
      var pubRequest = {
        activityId: activityId,
        status: 1,
      };
      if (type == 1) {
        this.btnSaveLoading2 = true;
      } else {
        this.btnSaveLoadingP = true;
      }

      couponSvc
        .updateCouponActivityStatus(pubRequest)
        .then(
          (res) => {
            var data = res.data;
            this.$message({ message: "发布成功", type: "success" });
            if (type == 1) {
              this.dialogVisible2 = false;
            }
            this.getPaged();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          if (type == 1) {
            this.btnSaveLoading2 = false;
          } else {
            this.btnSaveLoadingP = false;
          }
        });
    },
    iniDetailData() {
      this.acticityDetail = {
        couponActivityId: 0,
        name: "",
        couponId: 0,
        couponDisplayName: "",
        couponName: "",
        startTime: "",
        endTime: "",
        createTime: "",
        status: 3,
        statusName: "",
        code: "",
        channel: 0,
        isNewUserOnly: false,
        maxNumPerUser: 0,
        isIntegralExchange: false,
        needIntegralType: 0,
        needIntegralNum: 0,
        totalNum: 0,
        num: 0,
        url: "",
        grantPattern: 0,
        parentId: 0,
      };
    },
    getCouponActivityDetail(activityId) {
      this.iniDetailData();
      this.dialogVisible2 = true;
      this.formLoadingD = true;
      var detailRequest = {
        activityId: activityId,
      };
      couponSvc
        .getCouponActivityDetail(detailRequest)
        .then(
          (res) => {
            var data = res.data;
            if (data != null) {
              this.acticityDetail = data;
              if (this.acticityDetail.status == 0) {
                this.btn_modify = "保存";
              } else {
                this.btn_modify = "修改";
              }
            } else {
              this.$message({ message: "数据不存在", type: "error" });
              this.dialogVisible2 = false;
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.formLoadingD = false;
        });
    },
    editActicityData() {
      var r = /^\+?[1-9][0-9]*$/; //正整数
      if (this.acticityDetail.name == "") {
        this.$message({ message: "请输入活动", type: "warning" });
        return;
      }
      if (this.acticityDetail.startTime == "") {
        this.$message({ message: "请输入活动开始时间", type: "warning" });
        return;
      }
      if (this.acticityDetail.endTime == "") {
        this.$message({ message: "请输入活动结束时间", type: "warning" });
        return;
      }
      if (this.acticityDetail.couponId <= 0) {
        this.$message({ message: "请选择优惠券", type: "warning" });
        return;
      }
      if (!this.testNum(this.acticityDetail.maxNumPerUser)) {
        this.$message({
          message: "单用户最大领取数 输入有误",
          type: "warning",
        });
        return;
      }
      if (this.acticityDetail.isIntegralExchange) {
        // if (this.acticityDetail.needIntegralType == 0) {
        //   this.$message({ message: "请选择积分类型", type: "warning" });
        //   return;
        // }
        if (!this.testNumZ(this.acticityDetail.needIntegralNum)) {
          this.$message({ message: "请输入需要积分数量", type: "warning" });
          return;
        }
      } else {
        this.acticityDetail.needIntegralType = 0;
        this.acticityDetail.needIntegralNum = 0;
      }

      if (!this.testNum(this.acticityDetail.totalNum)) {
        this.$message({ message: "发放总数量 输入有误", type: "warning" });
        return;
      }

      if (!this.testNumZ(this.acticityDetail.num)) {
        this.$message({ message: "单次发放数量 输入有误", type: "warning" });
        return;
      }

      if (!this.testNum(this.acticityDetail.parentId)) {
        this.$message({ message: "父级活动id 输入有误", type: "warning" });
        return;
      }

      this.btnSaveLoading1 = true;
      couponSvc
        .editCouponActivity(this.acticityDetail)
        .then(
          (res) => {
            var data = res.data;
            this.$message({ message: "保存成功", type: "success" });
            this.dialogVisible2 = false;
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
    pauseActivity(activityId) {
      var pubRequest = {
        activityId: activityId,
        status: 2,
      };
      this.btnSaveLoading3 = true;
      couponSvc
        .updateCouponActivityStatus(pubRequest)
        .then(
          (res) => {
            var data = res.data;
            this.$message({ message: "暂停活动成功", type: "success" });
            this.dialogVisible2 = false;
            this.getPaged();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.btnSaveLoading3 = false;
        });
    },
    regainActivity(activityId) {
      var pubRequest = {
        activityId: activityId,
        status: 1,
      };
      this.btnSaveLoading4 = true;
      couponSvc
        .updateCouponActivityStatus(pubRequest)
        .then(
          (res) => {
            var data = res.data;
            this.$message({ message: "活动恢复成功", type: "success" });
            this.dialogVisible2 = false;
            this.getPaged();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.btnSaveLoading4 = false;
        });
    },
    deleteActivity(activityId) {
      var pubRequest = {
        activityId: activityId,
        status: 3,
      };
      this.btnSaveLoading5 = true;
      couponSvc
        .updateCouponActivityStatus(pubRequest)
        .then(
          (res) => {
            var data = res.data;
            this.$message({ message: "作废成功", type: "success" });
            this.dialogVisible2 = false;
            this.getPaged();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.btnSaveLoading5 = false;
        });
    },
    sendCoupon(activityId) {
      this.searchUser = {
        tableLoading: false,
        currentPage: 1,
        totalList: 0,
        condition: {
          userName: "",
          userTel: "",
          pageIndex: 1,
          pageSize: 10,
        },
        tableData: [],
      };
      this.currentActivityId = activityId;
      this.dialogVisible3 = true;
    },
    cancel3() {
      this.dialogVisible3 = false;
    },
    searchUserDialog() {
      this.getPagedU(true);
    },
    pageTurning1() {
      this.searchUser.condition.pageIndex = this.searchUser.currentPage;
      this.getPagedU(false);
    },
    sizeChange1(val) {
      this.searchUser.condition.pageIndex = this.searchUser.currentPage = 1;
      this.searchUser.condition.pageSize = val;
      this.getPagedU(false);
    },
    getPagedU(flag) {
      if (
        this.searchUser.condition.userName == "" &&
        this.searchUser.condition.userTel == ""
      ) {
        this.$message({
          message: "请输入客户姓名或手机号搜索",
          type: "warning",
        });
        return;
      }
      this.searchUser.tableLoading = true;
      couponSvc
        .searchUserInfo(this.searchUser.condition)
        .then(
          (res) => {
            var data = res.data;
            this.searchUser.tableData = data.items;
            this.searchUser.totalList = data.totalItems;
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
          this.searchUser.tableLoading = false;
        });
    },
    submitUserCoupon() {
      var userIds = new Array();
      for (var f = 0; f < this.searchUser.tableData.length; f++) {
        if (this.searchUser.tableData[f].defaultCheck) {
          userIds.push(this.searchUser.tableData[f].userId);
        }
      }
      if (userIds.length == 0) {
        this.$message({ message: "请选择用户", type: "warning" });
        return;
      }
      this.btnSendLoading = true;
      var sendRequest = {
        activityId: this.currentActivityId,
        userIdList: userIds,
      };
      couponSvc
        .grantUserCoupon(sendRequest)
        .then(
          (res) => {
            var data = res.data;
            this.$message({ message: "塞券成功", type: "success" });
            this.dialogVisible3 = false;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.btnSendLoading = false;
        });
    },
    changeTotalNum() {
      if (this.btn_modify == "修改") {
        this.btn_modify = "保存";
      } else {
        var r = /^\+?[1-9][0-9]*$/; //正整数
        if (!this.testNum(this.acticityDetail.totalNum)) {
          this.$message({ message: "发放总数量 输入有误", type: "warning" });
          return;
        }
        var updateRequest = {
          couponActivityId: this.acticityDetail.couponActivityId,
          totalNum: this.acticityDetail.totalNum,
        };
        this.changeTotalNumLoading = true;
        couponSvc
          .updateActivityTotalNum(updateRequest)
          .then(
            (res) => {
              var data = res.data;
              if (data) {
                this.$message({ message: "保存成功", type: "success" });
              } else {
                this.$message({ message: "保存失败", type: "error" });
              }
            },
            (error) => {
              console.log(error);
            }
          )
          .catch(() => {})
          .finally(() => {
            this.changeTotalNumLoading = false;
            this.btn_modify = "修改";
          });
      }
    },
  },
};
</script>