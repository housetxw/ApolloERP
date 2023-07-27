
<template>
  <main class="container" v-loading="loading">
    <el-form label-width="100px">
      <el-row>
        <el-divider content-position="left">基本信息</el-divider>
        <el-col :span="6">
          <el-form-item label="订单编号">{{ orderInfo.orderNo }}</el-form-item>
          <el-form-item label="创建人">{{ orderInfo.createBy }}</el-form-item>
          <el-form-item label="创建时间">{{
            orderInfo.createTime
          }}</el-form-item>
          
        </el-col>
      
        <el-col :span="6">
          <el-form-item label="订单分类">{{
            orderInfo.showProductType
          }}</el-form-item>
          <el-form-item label="订单渠道">{{
            orderInfo.channelName
          }}</el-form-item>
          <el-form-item label="订单类型">{{
            orderInfo.orderTypeName
          }}</el-form-item>
          
        </el-col>
        <el-col :span="12">
          <el-form-item label="订单状态">
            <el-col :span="8">{{ orderInfo.orderStatusName }}</el-col>
            <el-col :span="16" v-if="!onlyview">
              <el-button-group size="mini">
                <el-button
                  type="success"
                  size="mini"
                  :disabled="orderInfo.orderStatus != 10"
                  @click="examine"
                  >审核</el-button
                >
                    <!-- orderInfo.reverseStatus != 40 || -->
                <el-button
                  type="warning"
                  :disabled="
                    orderInfo.payStatus == 0 ||
                    orderInfo.isOccurReverse == 1 
                  "
                  @click="handleEdit1"
                  v-if="true"
                  size="mini"
                  >退款</el-button
                >
                <!-- 
                    orderInfo.payStatus === 1 ||
                    orderInfo.orderStatus === 30 ||
                 -->
                <el-button
                  type="danger"
                  :disabled="
                    orderInfo.isOccurReverse == 1 ||
                    orderInfo.orderStatus == 40
                  "
                  @click="handleEdit()"
                  v-if="true"
                  size="mini"
                  >取消</el-button
                >
                <el-button
                  type="warning"
                  size="mini"
                  :disabled="orderInfo.orderStatus != 20"
                  @click="youhuiValidate"
                  >手动优惠</el-button
                >
                <el-button
                  type="success"
                  size="mini"
                  :disabled="orderInfo.orderStatus != 20"
                  @click="daizhifuValidate"
                  >代支付</el-button
                >
              </el-button-group>
            </el-col>
          </el-form-item>
          <el-form-item label="签收状态">
            <el-col :span="8">{{ orderInfo.signStatus == 0 ? "未签收" : "已签收" }}
              <el-button
                type="warning"
                size="mini"
                v-if="
                  orderInfo.signStatus == 0 && this.orderInfo.orderStatus !== 40
                "
                @click="forceSign"
                >强制签收</el-button
              ></el-col>
            <el-col :span="16" v-if="!onlyview">
              <el-button-group size="mini">
                <el-button
                  type="primary"
                  size="mini"
                  :disabled="orderInfo.orderStatus != 20"
                  @click="meituanValidate"
                  >第三方核销（现结）</el-button
                >
                <el-button
                  type="primary"
                  size="mini"
                  :disabled="orderInfo.orderStatus != 20"
                  @click="dkhValidate"
                  >月结核销</el-button
                >
              </el-button-group>
            </el-col>
          </el-form-item>
          <el-form-item label="预约时间">
            <el-col :span="12">{{ (reserverInfo.reserverTime || "").toYMDHm() }}
                <el-button
                  v-if="orderInfo.orderStatus == 20 && reserverInfo.reserveId"
                  @click="
                    () => {
                      this.dlg_booking = true;
                    }
                  "
                  type="primary"
                  class="s-mini"
                  size="mini"
                  >修改预约时间</el-button
                >
            </el-col>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="6">
          <!--  <el-form-item label="门店编号">{{ orderInfo.shopId }}</el-form-item> -->
          <el-form-item label="终端类型">{{
            orderInfo.terminalTypeName
          }}</el-form-item> 
        </el-col>
        <el-col :span="10">
          <el-form-item label="门店名称">{{ orderInfo.shopName }}</el-form-item>
        </el-col>
        <el-col :span="8">
          <el-form-item label="技师名称" v-if="orderDispatchInfo">{{
            orderDispatchInfo.techName
          }}</el-form-item>
          <el-form-item label="技师名称" v-else></el-form-item>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="6">
          <el-form-item label="关联数据">{{
            orderInfo.refOrderNo
          }}</el-form-item>
        </el-col>
        <el-col :span="10">
          <el-form-item label="订单备注">{{ orderInfo.remark }}</el-form-item>
        </el-col>
        <el-col :span="8" v-if="!onlyview">
          <el-button-group size="mini">
            <el-button
              type="danger"
              size="mini"
              :disabled="false"
              @click="updateCostValidate"
              >更新成本</el-button
            >
            <el-button
              type="danger"
              size="mini"
              :disabled="false"
              @click="updatePerformanceValidate"
              >更新绩效</el-button
            >
          </el-button-group>
        </el-col>
      </el-row>
    </el-form>

    <el-row>
      <el-divider content-position="left">用户信息</el-divider>
      <el-form label-width="100px">
        <el-row>
          <el-col :span="3">
            <el-form-item label="客户">{{ userInfo.userName }}</el-form-item>
          </el-col>
          <el-col :span="3">
            <el-form-item label="客户电话">{{ userInfo.userTel }}</el-form-item>
          </el-col>
          <el-col :span="3">
            <el-form-item label="联系人">{{
              orderInfo.contactName
            }}</el-form-item>
          </el-col>
          <el-col :span="3">
            <el-form-item label="联系电话">{{
              orderInfo.contactPhone
            }}</el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="会员等级">{{
              userInfo.memberLevel
            }}</el-form-item>
          </el-col>
          <el-col :span="6">&nbsp;</el-col>
        </el-row>
        <el-form-item label="车型信息"
          >{{ userInfo.carNumber }}&nbsp;&nbsp; {{ userInfo.displayCarName }}
          {{ userInfo.vinCode }}</el-form-item
        >
        <el-form-item label="联系地址">
          <el-row>
            {{ userInfo.displayContactAddress }}
            <el-button
              v-if="orderInfo.channel == 3 && orderInfo.orderStatus == 20"
              type="primary"
              class="s-mini"
              size="mini"
              @click="addressEdit = !addressEdit"
              >修改</el-button
            >
          </el-row>
          <el-row
            style="margin-top: 5px"
            v-if="orderInfo.channel == 3 && orderInfo.orderStatus == 20"
          >
            <address-edit
              :ref="addressEdit"
              :show="addressEdit"
              :params="{
                provinceId: userInfo.provinceId,
                cityId: userInfo.cityId,
                districtId: userInfo.districtId,
                orderNo: orderInfo.orderNo,
                userId: userInfo.userId,
                userName: userInfo.userName,
                mobileNumber: userInfo.userTel,
                addressLine: userInfo.displayContactAddress,
              }"
              :userId="userInfo.userId"
              @changed="
                (p) => {
                  userInfo.userName = p.userName;
                  userInfo.userTel = p.mobileNumber;
                  userInfo.displayContactAddress = p.addressLine;

                  userInfo.provinceId = p.provinceId;
                  userInfo.cityId = p.cityId;
                  userInfo.districtId = p.districtId;

                  addressEdit = false;
                }
              "
            />
          </el-row>
        </el-form-item>
      </el-form>
    </el-row>
    <el-row>
      <el-divider content-position="left">支付&结算</el-divider>
      <el-form ref="form" label-width="100px" size="mini">
        <el-row>
          <el-col :span="6">
            <el-form-item label="付款方式">{{
              financeInfo.payMethodName
            }}</el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="支付渠道">{{
              financeInfo.payChannelName
            }}</el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="支付状态">{{
              financeInfo.payStatus ? "已支付" : "未支付"
            }}</el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="到账状态">{{
              financeInfo.moneyArriveStatus ? "已到帐" : "未到帐"
            }}</el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="6">
            <el-form-item label="付款账号">{{
              financeInfo.buyerAccount
            }}</el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="交易参考号">{{
              financeInfo.tradeNo
            }}</el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="支付时间">{{
              financeInfo.payTime
            }}</el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="退款状态">{{
              financeInfo.refundStatus ? "已退款" : "未退款"
            }}</el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="6">
            <el-form-item label="逆向申请">{{
              financeInfo.isOccurReverse ? "是" : "否"
            }}</el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="逆向申请状态">{{
              financeInfo.reverseStatusName
            }}</el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="退款时间">{{
              financeInfo.refundTime
            }}</el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="退款金额">{{
            (financeInfo.refundAmount || 0).toMoney()              
            }}</el-form-item>
          </el-col>
        </el-row>
      </el-form>
    </el-row>
    <el-row>
      <el-divider content-position="left">优惠卷</el-divider>
      <el-form ref="form" label-width="100px" size="mini">
        <el-row>
          <el-col :span="6">
            <el-form-item label="规则名称" v-if="orderCouponInfo">{{
              orderCouponInfo.name
            }}</el-form-item>
            <el-form-item label="规则名称" v-else></el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="显示名称" v-if="orderCouponInfo">{{
              orderCouponInfo.displayName
            }}</el-form-item>
            <el-form-item label="显示名称" v-else></el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="面值" v-if="orderCouponInfo">{{
              orderCouponInfo.value
            }}</el-form-item>
            <el-form-item label="面值" v-else></el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="类型" v-if="orderCouponInfo">{{
              orderCouponInfo.couponTypeText
            }}</el-form-item>
            <el-form-item label="类型" v-else></el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="6">
            <el-form-item label="发放方式" v-if="orderCouponInfo">{{
              orderCouponInfo.grantMethodText
            }}</el-form-item>
            <el-form-item label="发放方式" v-else></el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="活动名称" v-if="orderCouponInfo">{{
              orderCouponInfo.activityName
            }}</el-form-item>
            <el-form-item label="活动名称" v-else></el-form-item>
          </el-col>
          <el-col :span="6"> </el-col>
          <el-col :span="6"> </el-col>
        </el-row>
      </el-form>
    </el-row>
    <el-row>
      <el-divider content-position="left">机构公司</el-divider>
      <el-form ref="form" label-width="100px" size="mini">
        <el-row>
          <el-col :span="6">
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
          <el-col :span="6"> </el-col>
        </el-row>
      </el-form>
    </el-row>
    <el-row>
      <el-divider content-position="left">商品&服务</el-divider>
      <el-form
        ref="form"
        :model="amountInfo"
        label-width="100px"
        size="mini"
        class="col-4"
      >
        <el-form-item label="商品金额">
          <label class="s2">{{
            (amountInfo.totalProductAmount || 0).toMoney()
          }}</label>
        </el-form-item>
        <el-form-item label="运费">
          <label class="s2">{{
            (amountInfo.deliveryFee || 0).toMoney()
          }}</label>
        </el-form-item>
        <el-form-item label="优惠金额">
          <label class="s2">{{
            (amountInfo.totalCouponAmount || 0).toMoney()
          }}</label>
        </el-form-item>
        <el-form-item label="实收金额">
          <label class="s2">{{
            (amountInfo.actualAmount || 0).toMoney()
          }}</label>
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
          :cell-class-name="
            (p) => {
              if (this.orderInfo.orderStatus == 20 && p.columnIndex == 8)
                return p.row.enoughStock ? '' : 'red';
            }
          "
        >
          <rg-table-column prop="productId" label="产品编号" min-width="150px" />
          <el-table-column prop="productName" label="产品名称" min-width="200">
            <template slot-scope="scope">
              <img
                v-if="scope.row.imageUrl != ''"
                :src="scope.row.imageUrl"
                height="30"
                class="pic"
                style="float: left"
              />
              <!-- <span
                v-else-if="scope.row.imageUrl===''"
                style="float:left;padding-left:10px;line-height:30px"
              >暂无图片</span>-->
              <span style="padding-left: 10px; line-height: 30px">{{
                scope.row.productName
              }}</span>
            </template>
          </el-table-column>
          <rg-table-column prop="description" label="产品描述" fix-min />
          <rg-table-column prop="productAttribute" label="属性" fix-min align="center">
            <template scope="scope">{{
              { 0: "实物", 1: "套餐", 2: "服务", 3: "数字", 4: "项目", 5: "外采" }[
                scope.row.productAttribute
              ] || ""
            }}</template>
          </rg-table-column>
          <rg-table-column
            prop="totalNumber"
            label="数量"
            align="right"
            is-number
            fix-min
          />
          <rg-table-column prop="unit" label="单位" align="center" fix-min />
          <rg-table-column
            prop="price"
            label="单价"
            align="right"
            is-money
            fix-min
          />
          <rg-table-column
            prop="totalAmount"
            label="总价"
            align="right"
            is-money
            fix-min
          />

          <rg-table-column
            prop="enoughStock"
            label="库存"
            align="center"
            fix-min
            v-if="this.orderInfo.orderStatus == 20"
          >
            <template slot-scope="scope">{{
              scope.row.enoughStock ? "" : "缺货"
            }}</template>
          </rg-table-column>
          <rg-table-column
            prop="totalCostPrice"
            label="实物成本"
            align="right"
            is-money
            fix-min
          />
          <rg-table-column
            prop="settlementAmount"
            label="服务结算"
            align="right"
            is-money
            fix-min
          />
          <rg-table-column
            prop="shareDiscountAmount"
            label="优惠金额"
            align="right"
            is-money
            fix-min
          />
          <rg-table-column
            prop="actualPayAmount"
            label="实付金额"
            align="right"
            is-money
            fix-min
          />
          <rg-table-column prop="remark" label="备注"  />
        </el-table>
      </div>
    </el-row>

    <el-collapse accordion @change="accordionChange">
      <el-collapse-item>
        <template slot="title">
          <el-divider content-position="left">订单日志</el-divider>
        </template>
        <el-table
          v-loading="tableLoading"
          :data="orderLog.items"
          border
          style="width: 100%"
          size="mini"
        >
          <rg-table-column label="操作摘要" prop="summary" fix-min />
          <rg-table-column label="分类一" prop="type1" fix-min />
          <rg-table-column label="分类二" prop="type2" fix-min />
          <!-- <el-table-column label="操作前值">
              <template slot-scope="scope">
                <json-viewer
                  :value="JSON.parse(scope.row.beforeValue)"
                  :expand-depth="5"
                  copyable
                  boxed
                  sort
                />
              </template>
            </el-table-column>
            <el-table-column label="操作后值">
              <template slot-scope="scope">
                <json-viewer
                  :value="JSON.parse(scope.row.afterValue)"
                  :expand-depth="5"
                  copyable
                  boxed
                  sort
                />
              </template>
          </el-table-column>-->
          <el-table-column label="差异">
            <template slot-scope="scope">
              <div>
                <code-diff
                  :old-string="scope.row.beforeValue"
                  :new-string="scope.row.afterValue"
                  :context="10"
                  copyable
                  output-format="line-by-line"
                />
              </div>
            </template>
          </el-table-column>
          <rg-table-column label="操作人" prop="createBy" fix-min />
          <rg-table-column
            label="操作时间"
            prop="createTime"
            is-datetime
            fix-min
          />
        </el-table>
        <section class="pagination">
          <el-pagination
            background
            :page-size="10"
            :page-sizes="[10, 20, 50, 100]"
            layout="total, sizes, prev, pager, next, jumper"
            :total="orderLog.totalItems"
            :current-page.sync="orderLog.pageIndex"
            @current-change="pageChange"
            @size-change="sizeChange"
          />
        </section>
      </el-collapse-item>
    </el-collapse>

    <!-- 补机油弹框 -->
    <el-dialog
      title="选择机油容量"
      :visible.sync="dialogVisible"
      width="40%"
      :before-close="handleClose"
    >
      <div class="supply">
        <img
          v-if="supplyData.imageUrl != ''"
          :src="supplyData.imageUrl"
          width="40"
          height="40"
          class="pic"
        />
        <!-- <span prop=""></span> -->
        <span v-else-if="supplyData.imageUrl === ''">暂无图片</span>
        &nbsp;&nbsp;&nbsp;
        <span>{{ supplyData.productName }}</span>
      </div>

      <el-radio-group v-model="radio">
        <el-row type="flex" class="row-bg" justify="space-between">
          <el-col :span="3">
            <el-radio v-model="radio" :label="1" @change="getRadioVal()"
              >1L</el-radio
            >
          </el-col>
          <el-col :span="8">
            <el-input-number
              v-model="num"
              :min="1"
              :max="10"
              label="描述文字"
              size="small"
              @change="handleChange"
            />
          </el-col>
        </el-row>
        <el-row type="flex" class="row-bg" justify="space-between">
          <el-col :span="3">
            <el-radio v-model="radio" :label="2" @change="getRadioVal()"
              >2L</el-radio
            >
          </el-col>
          <el-col :span="8">
            <el-input-number
              v-model="num1"
              :min="1"
              :max="10"
              label="描述文字"
              size="small"
              @change="handleChange1"
            />
          </el-col>
        </el-row>
      </el-radio-group>

      <span slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="handleClose()">创建订单</el-button>
      </span>
    </el-dialog>
    <div />
    <!-- 取消订单弹框 -->
    <div>
      <rg-dialog
        title="取消订单"
        :visible.sync="dialogFormVisible"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            dialogFormVisible = false;
          },
        }"
        :btnRemove="false"
        width="600px"
      >
        <template v-slot:content>
          <el-form
            :model="tableData2"
            size="mini"
            label-width="120px"
            :rules="cancelOrderRules"
            ref="formCancelOrder"
          >
            <el-form-item label="取消原因" prop="reasons">
              <el-select
                v-model="tableData2.reasons"
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
            <el-form-item label="说明描述">
              <el-input
                v-model="tableData2.instruction"
                type="textarea"
                style="width: 420px"
                placeholder="请填写说明描述"
                rows="5"
                maxlength="300"
                show-word-limit
              />
            </el-form-item>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button type="primary" size="mini" @click="cancelOrder"
            >确 定</el-button
          >
        </template>
      </rg-dialog>
    </div>
    <!-- 申请退款弹框 -->

    <div>
      <el-dialog
        title="申请退款"
        :visible.sync="dialogFormVisible1"
        :close-on-click-modal="false"
      >
        <el-form :model="tableData3"
            :rules="cancelOrderRules">
          <el-form-item label="退款原因：" prop="reasons">
            <el-select
              v-model="tableData3.reasons1"
              placeholder="请选择"
              size="small"
              style="width: 420px"
            >
              <el-option
                v-for="item in reasons1"
                :key="item.id"
                :label="item.reason"
                :value="item.id"
              />
            </el-select>
          </el-form-item>
          <el-form-item label="退款金额：" prop="refundAmount">
            <!-- <span style="color: #606266; text-align:center;" prop="name" >取消描述：</span> -->
            <el-input
              v-model="tableData3.refundAmount"            
              style="width: 100px"
              size="small"
              placeholder="请填写退款金额"
            />
          </el-form-item>
          <br />

          <el-form-item label="退款描述：">
            <!-- <span style="color: #606266; text-align:center;" prop="name" >取消描述：</span> -->
            <el-input
              v-model="tableData3.instruction"
              type="textarea"
              style="width: 420px"
              placeholder="请填写退款描述"
            />
          </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="dialogFormVisible1 = false">取 消</el-button>
          <el-button type="primary" @click="sure1">确 定</el-button>
        </div>
      </el-dialog>
    </div>
    <!-- 审核弹框 -->
    <div>
      <rg-dialog
        title="订单审核"
        :visible.sync="centerDialogVisible"
        width="350px"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            centerDialogVisible = false;
          },
        }"
      >
        <template v-slot:content>
          <span>请确认订单信息无误后，点击【审核通过】。</span>
        </template>
        <el-form :model="tableData3">
          <el-form-item label="退款原因：">
            <el-select
              v-model="tableData3.reasons1"
              placeholder="请选择"
              size="small"
              style="width: 420px"
            >
              <el-option
                v-for="item in reasons1"
                :key="item.id"
                :label="item.reason"
                :value="item.id"
              />
            </el-select>
          </el-form-item>
          <br />

          <el-form-item label="退款描述：">
            <!-- <span style="color: #606266; text-align:center;" prop="name" >取消描述：</span> -->
            <el-input
              v-model="tableData3.instruction"
              type="textarea"
              style="width: 420px"
              placeholder="请填写退款描述"
            />
          </el-form-item>
        </el-form>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="success"
            @click="sure3"
            >审核通过</el-button
          >
        </template>
      </rg-dialog>
    </div>
    <!-- 美团核销 -->
    <div>
      <rg-dialog
        title="第三方核销"
        :visible.sync="meituanDialogVisible"
        width="500px"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            meituanDialogVisible = false;
            this.$refs['meituanModel'].resetFields();
          },
        }"
      >
        <template v-slot:content>
          <span style="color: red">请确认核销订单信息无误后，点击【核销】</span>
          <br />
          <br />
          <el-form
            :model="meituanModel"
            label-width="110px"
            label-position="left"
            :rules="rules"
            ref="meituanModel"
          >
            <el-form-item label="关联单号：" prop="meiTuanOrder">
              <!-- <span style="color: #606266; text-align:center;" prop="name" >取消描述：</span> -->
              <el-input
                v-model="meituanModel.meiTuanOrder"
                style="width: 200px"
                placeholder="请填对方单号"
              />
            </el-form-item>
            <el-form-item label="核销码：" prop="meiTuanTransferOrder">
              <!-- <span style="color: #606266; text-align:center;" prop="name" >取消描述：</span> -->
              <el-input
                v-model="meituanModel.meiTuanTransferOrder"
                style="width: 200px"
                placeholder="对方核销码："
              />
            </el-form-item>
          </el-form>
        </template>

        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="success"
            @click="hexiao('meituanModel')"
            >核销</el-button
          >
        </template>
      </rg-dialog>
    </div>
    
    <!-- 大客户核销 -->
    <div>
      <rg-dialog
        title="大客户核销"
        :visible.sync="dakehuDialogVisible"
        width="500px"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            dakehuDialogVisible = false;
            this.$refs['dakehuModel'].resetFields();
          },
        }"
      >
        <template v-slot:content>
          <span style="color: red">请确认核销订单信息无误后，点击【核销】</span>
          <br />
          <br />
          <el-form
            :model="dakehuModel"
            label-width="110px"
            label-position="left"
            :rules="rules"
            ref="dakehuModel"
          >
            <el-form-item label="关联单号：" prop="meiTuanOrder">
              <!-- <span style="color: #606266; text-align:center;" prop="name" >取消描述：</span> -->
              <el-input
                v-model="dakehuModel.meiTuanOrder"
                style="width: 200px"
                placeholder="请填对方单号"
              />
            </el-form-item>
            <el-form-item label="核销码：" prop="meiTuanTransferOrder">
              <!-- <span style="color: #606266; text-align:center;" prop="name" >取消描述：</span> -->
              <el-input
                v-model="dakehuModel.meiTuanTransferOrder"
                style="width: 200px"
                placeholder="对方核销码："
              />
            </el-form-item>
          </el-form>
        </template>

        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="success"
            @click="dkhHexiao('dakehuModel')"
            >核销</el-button
          >
        </template>
      </rg-dialog>
    </div>

    <!-- 代支付 -->
    <div>
      <rg-dialog
        title="代支付"
        :visible.sync="daizhifuDialogVisible"
        width="500px"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            daizhifuDialogVisible = false;
            this.$refs['daizhifuModel'].resetFields();
          },
        }"
      >
        <template v-slot:content>
          <span style="color: red">点击【提交支付】后直接修改支付状态，请谨慎操作。</span>
          <br />
          <br />
          <el-form
            :model="daizhifuModel"
            label-width="120px"
            label-position="left"
            :rules="rules"
            ref="daizhifuModel"
          >
            <el-form-item label="代支付订单：" prop="meiTuanTransferOrder">
              <!-- <span style="color: #606266; text-align:center;" prop="name" >取消描述：</span> -->
              <el-input
                v-model="daizhifuModel.meiTuanTransferOrder"
                style="width: 300px"
                placeholder="请填已经收款的代支付订单单号"
              />
            </el-form-item>
            <el-form-item label="备注：" prop="remark">
              <!-- <span style="color: #606266; text-align:center;" prop="name" >取消描述：</span> -->
              <el-input
                v-model="daizhifuModel.remark"
                style="width: 300px"
                placeholder="订单备注信息"
              />
            </el-form-item>
          </el-form>
        </template>

        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="success"
            @click="tijiaozhifu('daizhifuModel')"
            >提交支付</el-button
          >
        </template>
      </rg-dialog>
    </div>
    <!-- 手动优惠 -->
    <div>
      <rg-dialog
        title="手动优惠"
        :visible.sync="youhuiDialogVisible"
        width="500px"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            youhuiDialogVisible = false;
            this.$refs['youhuiModel'].resetFields();
          },
        }"
      >
        <template v-slot:content>
          <span style="color: red">点击【提交优惠】后直接修改订单金额，请谨慎操作。</span>
          <br />
          <br />
          <el-form
            :model="youhuiModel"
            label-width="120px"
            label-position="left"
            :rules="rules"
            ref="youhuiModel"
          >
            <el-form-item label="商品金额：" prop="totalProductAmount">              
              <label>{{youhuiModel.totalProductAmount}}</label>   
            </el-form-item>
            <el-form-item label="优惠金额：" prop="totalCouponAmount">
              <!-- <span style="color: #606266; text-align:center;" prop="name" >取消描述：</span> -->
              <el-input
                v-model="youhuiModel.totalCouponAmount"
                style="width: 300px"
                placeholder="请输入优惠金额"
                @input="changeYouhui('youhuiModel')"
              />
            </el-form-item>
            <el-form-item label="实收金额：" prop="actualAmount">              
              <label>{{youhuiModel.actualAmount}}</label>   
            </el-form-item>
            <el-form-item label="备注：" prop="remark">
              <!-- <span style="color: #606266; text-align:center;" prop="name" >取消描述：</span> -->
              <el-input
                v-model="youhuiModel.remark"
                style="width: 300px"
                placeholder="订单备注信息"
              />
            </el-form-item>
          </el-form>
        </template>

        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="success"
            @click="tijiaoYouhui('youhuiModel')"
            >提交优惠</el-button
          >
        </template>
      </rg-dialog>
    </div>

    <!-- 更新成本 -->
    <div>
      <rg-dialog
        title="更新成本和明细优惠分摊"
        :visible.sync="updateCostDialogVisible"
        width="500px"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            updateCostDialogVisible = false;
            this.$refs['updateCostModel'].resetFields();
          },
        }"
      >
        <template v-slot:content>
          <span style="color: red">点击【提交】后直接修改订单明细成本和金额，请谨慎操作。</span>
          <br />
          <br />
          <el-form
            :model="updateCostModel"
            label-width="120px"
            label-position="left"
            :rules="rules"
            ref="updateCostModel"
          >
            <el-form-item label="门店ID：" prop="shopId">
              <el-input
                v-model="updateCostModel.shopId"
                style="width: 200px"
                placeholder="门店编号"
              />
            </el-form-item>
            <el-form-item label="订单NO：" prop="orderNo">
              <el-input
                v-model="updateCostModel.orderNo"
                style="width: 200px"
                placeholder="订单编号"
              />
            </el-form-item>
            <el-form-item label="订单日期：" prop="orderDate">
              <el-input
                v-model="updateCostModel.orderDate"
                style="width: 200px"
                placeholder="订单日期"
              />
            </el-form-item>
          </el-form>
        </template>

        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="success"
            :disabled="loading"
            @click="tijiaoUpdateCost('updateCostModel')"
            >提交更新订单成本</el-button
          >
        </template>
      </rg-dialog>
    </div>

    <!-- 更新员工绩效 -->
    <div>
      <rg-dialog
        title="更新员工绩效"
        :visible.sync="updatePerformanceDialogVisible"
        width="500px"
        :btnCancel="{
          label: '关闭',
          click: (done) => {
            updatePerformanceDialogVisible = false;
            this.$refs['updatePerformanceModel'].resetFields();
          },
        }"
      >
        <template v-slot:content>
          <span style="color: red">点击【提交】后直接修改员工绩效订单，请谨慎操作。</span>
          <br />
          <br />
          <el-form
            :model="updatePerformanceModel"
            label-width="120px"
            label-position="left"
            :rules="rules"
            ref="updatePerformanceModel"
          >
            <el-form-item label="门店ID：" prop="shopId">
              <el-input
                v-model="updatePerformanceModel.shopId"
                style="width: 200px"
                placeholder="门店编号"
              />
            </el-form-item>
            <el-form-item label="订单NO：" prop="orderNo">
              <el-input
                v-model="updatePerformanceModel.orderNo"
                style="width: 200px"
                placeholder="订单编号"
              />
            </el-form-item>
            <el-form-item label="订单日期：" prop="orderDate">
              <el-input
                v-model="updatePerformanceModel.orderDate"
                style="width: 200px"
                placeholder="订单日期"
              />
            </el-form-item>
          </el-form>
        </template>

        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="success"
            :disabled="loading"
            @click="tijiaoUpdatePerformance('updatePerformanceModel')"
            >提交更新员工绩效</el-button
          >
        </template>
      </rg-dialog>
    </div>

    <section id="choosetime">
      <dlgBookingTime
        :dlg_booking="dlg_booking"
        @onclose="closeDlgBbooking"
        :shopId="orderInfo.shopId"
      />
    </section>
  </main>
</template>

<script>
import { orderCenterSvc, OrderCommand } from "@/api/orderCenter/orderCenter";
import CodeDiff from "vue-code-diff";
import dlgBookingTime from "./components/dlgBookingTime.vue";
import addressEdit from "./components/addressEdit.vue";

export default {
  name: "orderdetails",
  components: { CodeDiff, dlgBookingTime, addressEdit },
  data() {
    return {
      orderNo: "",
      dlg_booking: false, //选择预约时间
      addressEdit: false,
      loading: false,
      totalnumber: "",
      productId: "",
      radioValue: "",
      radio: 1,
      num: 1,
      num1: 1,
      dialogVisible: false,
      centerDialogVisible: false,
      meituanDialogVisible: false,
      daizhifuDialogVisible: false,
      youhuiDialogVisible: false,
      updateCostDialogVisible: false,
      updatePerformanceDialogVisible: false,
      dakehuDialogVisible: false,
      dialogFormVisible1: false,
      tableData3: {
        reasons1: "",
        instruction: "",
        refundAmount: 0,
      },
      meituanModel: {
        channel: 4,
        orderType: 0,
        orderNos: [],
        meiTuanOrder: "",
        meiTuanTransferOrder: "",
        produceType: 24,
        payChannel: 4,
        remark: "",
      },
      dakehuModel: {
        channel: 5,
        orderType: 0,
        orderNos: [],
        meiTuanOrder: "",
        meiTuanTransferOrder: "",
        produceType: 16,
        payChannel: 4,
        payMethod: 3,
        remark: "",
      },
      daizhifuModel: {
        payChannel: 9,
        orderType: 1,
        orderNos: [],
        remark: "",
        meiTuanTransferOrder: "",
      },
      youhuiModel: {
        totalProductAmount: 0,
        totalCouponAmount: 0,
        actualAmount: 0,
        orderNo: "",
        remark: "",
      },
      updateCostModel: {
        shopId: 0,
        orderNo: "",
        orderNos: [],
        orderDate: "",
      },
      updatePerformanceModel: {
        shopId: 0,
        orderNo: "",
        orderNos: [],
        orderDate: "",
      },
      unclick: false,
      unclick1: false,
      reasons1: "",

      tableData2: {
        reasons: "",
        instruction: "",
      },
      imgshow: true,
      reasons: "",
      dialogFormVisible: false,
      tableData: [
        {
          children: [],
        },
      ],
      tableLoading: false,
      cancelOrderRules: {
        reasons: [
          {
            required: true,
            trigger: "blur",
            message: "请指定取消原因",
          },
        ],
        refundAmount: [
          {
            required: true,
            trigger: "blur",
            message: "退款金额需大于0",
          },
        ],
      },
      imageUrl: "",
      // orderNo: "",
      orderInfo: {
        orderNo: "",
        orderStatus: "", // 订单状态（10已提交 20已确认 30已完成 40已取消）
        createBy: "", // 创建人
        orderType: 0, // 订单类型（0未设置 1轮胎 2保养 3美容）
        channel: 0, // 渠道（0未设置 1总部C端 2总部门店）
        createTime: "", // 创建时间
        orderTime: "", // 下单时间
        updateTime: "", // 修改时间
        terminalType: 0, // 终端类型（0未设置 1小程序 2Android-App 3iOS-App 4S网站 5官网）
        appVersion: "", // 应用版本号
        remark: "", // 订单备注
        payStatus: "", // 支付状态（0未支付 1已支付）
        isOccurReverse: "", // 是否发生过逆向申请（0否 1是）
        reverseStatus: "", // 逆向申请单状态（10已提交 20已确认 30已完成 40已取消）
        refundStatus: 0, // 退款状态（0未退款 1已退款）
        shopId: 0, //门店编号
        shopName: "", //门店名称
        contactName: "",
        contactPhone: "",
        showProductType: "",
        refOrderNo: "",
      },

      financeInfo: {
        payStatus: 0, // 支付状态（0未支付 1已支付）
        payChannel: 0, // 支付渠道（0未设置 1微信支付 2支付宝）
        payMethod: 0, // 支付方式（0未设置 1在线支付 2到店付款）
        payTime: "", // 支付时间
        payNo: "", // 支付单号
        tradeNo: "", // 外部交易流水号
        buyerAccount: "", // 买家付款账号（openid或支付宝账号）
        refundStatus: 0, // 退款状态（0未退款 1已退款）
        moneyArriveStatus: 0, // 到账状态（0未到账 1已到账）
        isOccurReverse: 0,
        reverseStatus: 0,
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
      amountInfo: {
        totalProductAmount: 0, // 商品总价（单品或套餐，不含单服务）
        serviceFee: 0, // 服务费（单服务）
        deliveryFee: 0, // 运费
        totalCouponAmount: 0, // 总优惠金额
        actualAmount: 0, // 实付款
      },
      reserverInfo: {}, //预约信息
      supplyData: [],
      orderLog: {
        isLoaded: false,
        pageIndex: 1,
        pageSize: 20,
        items: [],
        totalItems: 0,
      },
      orderDispatchInfo: {
        techName: "",
      },
      orderCouponInfo: {
        name: "",
        displayName: "",
        value: 0,
        couponTypeText: "",
        grantMethodText: "",
        activityName: "",
      },
      insuranceInfo: {
        name: "",
        refNo: "",
      },

      rules: {
        meiTuanOrder: [
          { required: true, message: "请输入关联单号", trigger: "blur" },
          { min: 1, max: 50, message: "长度在 1 到50 个字符", trigger: "blur" },
        ],
        meiTuanTransferOrder: [
          { required: true, message: "请输入核销码", trigger: "blur" },
          {
            min: 1,
            max: 100,
            message: "长度在 1 到100 个字符",
            trigger: "blur",
          },
        ],
      },
    };
  },

  created() {
    // 页面初始化函数
  },
  computed: {
    onlyview() {
      return this.$route.params.onlyview == 1;
    },
  },

  mounted() {
    this.orderNo =
      this.$route.params.orderNo || this.$route.query.orderNo || "";
    this.GetOrderDetail();
  },
  methods: {
    closeDlgBbooking(p) {
      console.log(p);
      if (!p) {
        this.dlg_booking = false;
        return;
      }
      //修改预约时间
      this.loading = true;
      let date = new Date(p);
      orderCenterSvc
        .ModifyReserveTime({
          reserveId: this.reserverInfo.reserveId,
          year: date.getFullYear(),
          month: date.getMonth() + 1,
          day: date.getDate(),
          reserveTime: date.JDateFormat("hh:mm"),
        })
        .then(
          (res) => {
            if (res.data) {
              this.$message();
            }
            if (res.data) {
              this.$messageSuccess(res.message || "修改成功！");
              this.dlg_booking = false;
              this.reserverInfo.reserverTime = p;
            } else {
              this.$messageWarning(res.message || "保存失败！");
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally((p) => {
          this.loading = false;
        });
    },

    // 处理数据
    formateName() {
      this.orderInfo.orderStatusName =
        { 10: "已提交", 20: "已确认", 30: "已完成", 40: "已取消" }[
          this.orderInfo.orderStatus
        ] || "";
      this.orderInfo.channelName =
        { 0: "未设置", 1: "总部C端", 2: "总部门店", 3: "电销下单", 4: "第三方订单", 5: "大客户" }[
          this.orderInfo.channel
        ] || "";
      this.orderInfo.orderTypeName =
        {
          0: "未设置",
          1: "轮胎安装",
          2: "保养服务",
          3: "美容洗车",
          4: "钣金喷漆",
          5: "维修改装",
          6: "其他",
        }[this.orderInfo.orderType] || "";
      this.orderInfo.terminalTypeName =
        {
          0: "未设置",
          1: "小程序",
          2: "Android-App",
          3: "iOS-App",
          4: "网站",
          5: "官网",
        }[this.orderInfo.terminalType] || "";
      this.financeInfo.payChannelName =
        {
          0: "未设置",
          1: "微信支付",
          2: "支付宝",
          3: "美团",
          4: "线下支付",
          9: "代付款",
        }[this.financeInfo.payChannel] || "";

      this.financeInfo.payMethodName =
        {
          0: "未设置",
          1: "在线支付",
          2: "到店付款",
          3: "月结",
        }[this.financeInfo.payMethod] || "";

      this.financeInfo.reverseStatusName =
        {
          10: "已提交",
          20: "已确认",
          30: "已完成",
          40: "已取消",
        }[this.financeInfo.reverseStatus] || "";
    },
    // 获取订单详情
    GetOrderDetail() {
      this.orderInfo.orderNo = this.orderNo;
      this.loading = true;
      orderCenterSvc
        .GetOrderDetail({ orderNo: this.orderInfo.orderNo })
        .then(
          (res) => {
            var data = res.data;
            Object.assign(this.userInfo, data.userInfo);
            this.financeInfo = data.financeInfo;
            this.orderInfo = data.orderInfo;
            this.amountInfo = data.amountInfo;
            this.reserverInfo = data.reserverInfo;
            //this.reserverInfo=null;
            if (this.reserverInfo == null) {
              this.reserverInfo = {
                reserverTime: "",
                reserveId: 0,
              };
            }

            this.tableData = data.productInfo.products;
            this.orderDispatchInfo = data.orderDispatchInfo;
            this.orderCouponInfo = data.orderCouponInfo;
            this.insuranceInfo = data.orderInsuranceCompanyInfo;
            // if (data.orderDispatchInfo != null)
            this.formateName();
            console.log(this.reserverInfo);
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally((p) => {
          this.loading = false;
        });
    },
    accordionChange(p) {
      if (p && !this.orderLog.isLoaded) {
        this.GetOrderLogList();
      }
    },
    // 订单日志列表
    GetOrderLogList() {
      this.loading = true;
      orderCenterSvc
        .GetOrderLogList({
          orderNo: this.orderInfo.orderNo,
          pageIndex: this.orderLog.pageIndex,
          pageSize: this.orderLog.pageSize,
        })
        .then((res) => {
          this.orderLog.items = res.data.items;
          this.orderLog.totalItems = res.data.totalItems;
          this.orderLog.isLoaded = true;
        })
        .finally((p) => {
          this.loading = false;
        });
    },
    sizeChange(data) {
      this.orderLog.pageSize = data;
      this.GetOrderLogList();
    },
    pageChange() {
      this.GetOrderLogList();
    },
    changeNumber() {
      if (this.radio === 1) {
        this.totalnumber = this.radio * this.num;
      }
      if (this.radio === 2) {
        this.totalnumber = this.radio * this.num1;
      }
    },
    // 补机油
    supply(row) {
      this.dialogVisible = true;
      this.supplyData.imageUrl = row.imageUrl;
      this.supplyData.productName = row.productName;
      this.productId = row.productId;
      console.log(345678, row.productId);
      this.changeNumber();
    },
    handleChange(value) {
      console.log(value);
      this.num = value;
    },
    handleChange1(value) {
      this.num1 = value;
    },
    handleClose() {
      this.loading = true;
      this.changeNumber();
      console.log(88, this.totalnumber);
      orderCenterSvc
        .OrderCommand({
          orderNo: this.orderInfo.orderNo,
          appendForProductId: this.productId,
          number: this.totalnumber,
        })
        .then(
          (res) => {
            if ((res.code = 10000)) {
              this.$message({
                duration: 3000,
                content: res.message || "操作失败",
              });
            }
            var data = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .finally((p) => {
          this.loading = false;
        })

        .catch(() => {});
      // this.$confirm("确认为该用户新增机油订单", {
      //   confirmButtonText: "确定",
      //   cancelButtonText: "取消",
      //   type: "warning"
      // })
      //   .then(() => {

      //     this.dialogVisible = false;
      //             this.$message({
      //       type: "success",
      //       message: "创单成功!"
      //     });
      //   })
      //   .catch(() => {
      //     this.$message({
      //       type: "info",
      //       message: "已取消创单"
      //     });
      //     this.dialogVisible = false;
      //   });
      this.dialogVisible = false;
    },
    // 强制签收
    forceSign() {
      const h = this.$createElement;
      this.$confirm("", {
        message: h("p", null, [
          h(
            "span",
            { style: "color: red" },
            "强制签收为非标准操作，操作前请告知研发 !"
          ),
          h("p", null, " 确认强制签收该订单？ "),
        ]),
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then((p) => {
          this.loading = true;
          orderCenterSvc
            .UpdateOrderSignStatus({ orderNos: [this.orderInfo.orderNo] })
            .then(
              (res) => {
                if (res.data || res.code === "10000") {
                  this.$message({
                    message: "操作成功！",
                    type: "success",
                  });
                  this.orderInfo.signStatus = 1;
                } else {
                  this.$message.error(res.message || "处理失败，请重试！");
                }
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally((p) => {
              this.loading = false;
            });
        })
        .catch((action) => {});
    },
    handleEdit() {
      this.dialogFormVisible = true;
      this.loading = true;
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
        .catch(() => {})
        .finally((p) => {
          this.loading = false;
        });
    },
    // 取消订单
    cancelOrder() {
      this.$refs["formCancelOrder"].validate((valid) => {
        if (valid) {
          this.loading = true;
          orderCenterSvc
            .CancelOrder({
              orderNo: this.orderInfo.orderNo,
              reasonId: this.tableData2.reasons,
              instruction: this.tableData2.instruction,
            })
            .then(
              (res) => {
                this.dialogFormVisible = false;
                this.$message({
                  type: "success",
                  message: "操作成功！",
                });
                //关闭当前页面并跳转到订单列表页面
                this.$root.close_curpage("/orderCenter/orderList", true);
                // this.GetOrderDetail();
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally((p) => {
              this.loading = false;
            });
        }
      });
    },
    // 申请退款
    handleEdit1() {
      this.dialogFormVisible1 = true;
      this.loading = true;
      orderCenterSvc
        .GetReverseReasons({ ApplyType: 2 })
        .then(
          (res) => {
            var data = res.data;
            this.reasons1 = data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally((p) => {
          this.loading = false;
        });
    },
    sure1() {
      if (this.tableData3.refundAmount <= 0) {
        this.$message({
          message: "退款金额必须大于0",
          type: "warning"
        });
        return;
      }
      this.loading = true;
      orderCenterSvc
        .RefundApply({
          orderNo: this.orderNo,
          reasonId: this.tableData3.reasons1,
          instruction: this.tableData3.instruction,
          refundAmount: this.tableData3.refundAmount,
        })
        .then(
          (res) => {
            this.$message({
              type: "success",
              message: "申请成功!",
            });
            this.dialogFormVisible1 = false;
            this.GetOrderDetail();
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally((p) => {
          this.loading = false;
        });
    },
    // 信息预审
    examine() {
      this.centerDialogVisible = true;
    },
    sure3() {
      this.loading = true;
      orderCenterSvc
        .CheckOrder({
          orderNo: this.orderNo,
        })
        .then(
          (res) => {
            this.$message({
              type: "success",
              message: "审核通过!",
            });
            this.centerDialogVisible = false;
          },
          (error) => {
            console.log(error);
            this.centerDialogVisible = false;
          }
        )
        .finally((p) => {
          this.loading = false;
        });
    },
    // 信息预审
    meituanValidate() {
      this.meituanDialogVisible = true;
      this.meituanModel.orderNos = [];
      this.meituanModel.meiTuanOrder = "";
      this.meituanModel.meiTuanTransferOrder = "";
      this.meituanModel.remark = "";
    },

    hexiao(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.loading = true;
          let dataObj = this.meituanModel;
          dataObj.orderNos = [];
          dataObj.orderNos.push(this.orderNo);
          orderCenterSvc
            .SubmitOrder(dataObj)
            .then((res) => {
              if (res.data || res.code === "10000") {
                this.$message({
                  message: "操作成功！",
                  type: "success",
                });
                window.location.reload();
              } else {
                this.$message.error(res.message || "处理失败，请重试！");
              }
            })
            .catch(() => {})
            .finally(() => {
              this.loading = false;
              this.meituanDialogVisible = false;
            });
        } else {
          //console.log("error submit!!");
          return false;
        }
      });
    },

    
    // 大客户信息预审
    dkhValidate() {
      this.dakehuDialogVisible = true;
      this.dakehuModel.orderNos = [];
      this.dakehuModel.meiTuanOrder = "";
      this.dakehuModel.meiTuanTransferOrder = "";
      this.dakehuModel.remark = "";
    },

    dkhHexiao(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.loading = true;
          let dataObj = this.dakehuModel;
          dataObj.orderNos = [];
          dataObj.orderNos.push(this.orderNo);
          orderCenterSvc
            .SubmitOrder(dataObj)
            .then((res) => {
              if (res.data || res.code === "10000") {
                this.$message({
                  message: "操作成功！",
                  type: "success",
                });
                window.location.reload();
              } else {
                this.$message.error(res.message || "处理失败，请重试！");
              }
            })
            .catch(() => {})
            .finally(() => {
              this.loading = false;
              this.dakehuDialogVisible = false;
            });
        } else {
          //console.log("error submit!!");
          return false;
        }
      });
    },

    //手动优惠 信息预审
    youhuiValidate() {
      this.youhuiDialogVisible = true;
      this.youhuiModel.orderNo = this.orderNo;
      this.youhuiModel.totalProductAmount = this.amountInfo.totalProductAmount;
      this.youhuiModel.totalCouponAmount = this.amountInfo.totalCouponAmount;
      this.youhuiModel.actualAmount = this.amountInfo.actualAmount;
      this.youhuiModel.remark = "";
    },

    tijiaoYouhui(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.loading = true;
          let dataObj = this.youhuiModel;
          dataObj.orderNo = this.orderNo;
          orderCenterSvc
            .UpdateCouponAmount(dataObj)
            .then((res) => {
              if (res.data || res.code === "10000") {
                this.$message({
                  message: "操作成功！",
                  type: "success",
                });
                window.location.reload();
              } else {
                this.$message.error(res.message || "处理失败，请重试！");
              }
            })
            .catch(() => {})
            .finally(() => {
              this.loading = false;
              this.youhuiDialogVisible = false;
            });
        } else {
          //console.log("error submit!!");
          return false;
        }
      });
    },

    changeYouhui(formName) {
      this.youhuiModel.actualAmount = this.youhuiModel.totalProductAmount
        - this.youhuiModel.totalCouponAmount;
    },


    //更新成本 信息预审
    updateCostValidate() {
      this.updateCostDialogVisible = true;
      this.updateCostModel.orderNo = this.orderNo;
      this.updateCostModel.shopId = this.orderInfo.shopId;
      this.updateCostModel.orderDate = this.orderInfo.orderTime.JDateFormat("yyyy-MM-dd");
    },

    tijiaoUpdateCost(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.loading = true;
          let dataObj = this.updateCostModel;
          dataObj.orderNos = [];
          if (this.updateCostModel.orderNo != ""){
            dataObj.orderNos.push(this.updateCostModel.orderNo);}
          orderCenterSvc
            .BatchUpdateOrderProductCostPriceActualPayAmount(dataObj)
            .then((res) => {
              if (res.data || res.code === "10000") {
                this.$message({
                  message: "操作成功！",
                  type: "success",
                });
                window.location.reload();
              } else {
                this.$message.error(res.message || "处理失败，请重试！");
              }
            })
            .catch(() => {})
            .finally(() => {
              this.loading = false;
              this.updateCostDialogVisible  = false;
            });
        } else {
          //console.log("error submit!!");
          return false;
        }
      });
    },

    
    //更新绩效 信息预审
    updatePerformanceValidate() {
      this.updatePerformanceDialogVisible = true;
      this.updatePerformanceModel.orderNo = this.orderNo;
      this.updatePerformanceModel.shopId = this.orderInfo.shopId;
      this.updatePerformanceModel.orderDate = this.orderInfo.orderTime.JDateFormat("yyyy-MM-dd");
    },

    tijiaoUpdatePerformance(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.loading = true;
          let dataObj = this.updatePerformanceModel;
          dataObj.orderNos = [];
          if (this.updatePerformanceModel.orderNo != ""){
            dataObj.orderNos.push(this.updatePerformanceModel.orderNo);}
          orderCenterSvc
            .BatchUpdateEmployeePerformanceOrder(dataObj)
            .then((res) => {
              if (res.data || res.code === "10000") {
                this.$message({
                  message: "操作成功！",
                  type: "success",
                });
                window.location.reload();
              } else {
                this.$message.error(res.message || "处理失败，请重试！");
              }
            })
            .catch(() => {})
            .finally(() => {
              this.loading = false;
              this.updatePerformanceDialogVisible = false;
            });
        } else {
          //console.log("error submit!!");
          return false;
        }
      });
    },

    // 代支付信息预审
    daizhifuValidate() {
      this.daizhifuDialogVisible = true;
      this.daizhifuModel.orderNos = [];
      this.daizhifuModel.remark = "";
      this.daizhifuModel.meiTuanTransferOrder = "";
    },

    tijiaozhifu(formName) {
      this.$refs[formName].validate((valid) => {
        if (valid) {
          this.loading = true;
          let dataObj = this.daizhifuModel;
          dataObj.orderNos = [];
          dataObj.orderNos.push(this.orderNo);
          orderCenterSvc
            .SubmitOrder(dataObj)
            .then((res) => {
              if (res.data || res.code === "10000") {
                this.$message({
                  message: "操作成功！",
                  type: "success",
                });
                window.location.reload();
              } else {
                this.$message.error(res.message || "处理失败，请重试！");
              }
            })
            .catch(() => {})
            .finally(() => {
              this.loading = false;
              this.daizhifuDialogVisible = false;
            });
        } else {
          //console.log("error submit!!");
          return false;
        }
      });
    },

  },
};
</script>
<style lang="scss" scoped>
.s2 {
  color: red;
  font: 16px/20px "";
}
</style>
