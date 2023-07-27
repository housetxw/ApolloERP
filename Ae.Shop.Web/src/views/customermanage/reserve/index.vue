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
      :searching="search"
      :headerBtnWidth="180"
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="客户手机号"
            style="width:150px"
            prefix-icon="el-icon-search"
            v-model="condition.userTel"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="客户车牌"
            style="width:150px"
            prefix-icon="el-icon-search"
            v-model="condition.carPlate"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-select v-model="condition.reserveType" style="width:120px;">
            <el-option
              title="预约类型"
              v-for="item in reserveTypeEnum"
              :key="item.value"
              :label="item.displayName.replace('全部','全部预约类型')"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select v-model="condition.reserveChannel" style="width:100px;">
            <el-option
              v-for="item in reserveChannelEnum"
              :key="item.value"
              :label="item.displayName.replace('全部','全部渠道')"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-select v-model="condition.status" style="width:100px;">
            <el-option
              v-for="item in statusEnum"
              :key="item.value"
              :label="item.displayName.replace('全部','全部状态')"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-tooltip class="item" effect="dark" content="预约时间范围" placement="bottom">
            <el-form-item>
              <el-date-picker
                style="width: 110px;"
                v-model="condition.startDate"
                type="date"
                placeholder="开始日期"
              ></el-date-picker>&nbsp;-&nbsp;
              <el-date-picker
                style="width: 110px;"
                v-model="condition.endDate"
                type="date"
                placeholder="结束日期"
              ></el-date-picker>
            </el-form-item>
          </el-tooltip>
        </el-form-item>
        <el-form-item>
          <el-checkbox v-model="condition.reserveTech">已预约技师</el-checkbox>
        </el-form-item>
      </template>

      <template v-slot:header_btn>
        <el-button type="primary" size="mini" icon="el-icon-plus" @click="add">预约登记</el-button>
      </template>

      <!-- 数据表格 -->
      <template v-slot:tb_cols>
        <rg-table-column label="客户名称" prop="userName" />
        <el-table-column label="手机号" prop="userTel" :width="100"></el-table-column>
        <el-table-column label="状态" prop="statusDisplay" align="center" :width="100"></el-table-column>
        <el-table-column label="渠道" prop="channelDisplay" :width="80"></el-table-column>
        <el-table-column label="预约类型" prop="reserveTypeDisplay" :width="90"></el-table-column>
        <el-table-column label="预约时间" prop="reserveTime" :width="120">
          <template slot-scope="scope">{{scope.row.reserveTime.substr(0,16)}}</template>
        </el-table-column>
        <el-table-column label="预约技师" :width="80">
          <template slot-scope="scope">{{scope.row.techName==""?"不约技师":scope.row.techName}}</template>
        </el-table-column>
        <el-table-column label="车辆" min-width="300">
          <template slot-scope="scope">{{scope.row.carPlate+" "+scope.row.carType}}</template>
        </el-table-column>
        <el-table-column fixed="right" label="操作" :width="90">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                @click="routerToDetail(scope.row.reserveId)"
                type="primary"
                size="mini"
              >查看详情</el-button>
            </el-button-group>
          </template>
        </el-table-column>
      </template>
    </rg-page>

    <section id="detailOrEdit">
      <rg-dialog
        :title="formTitle"
        :loading="formLoading"
        :visible.sync="formVisible"
        :beforeClose="cancel"
        :btnCancel="{label:'关闭',click:(done)=>{cancel('formModel')}}"
        :btnRemove="{label:'取消预约',disabled:formCheck1,loading:btnSaveLoading, click:cancelShopReserveDia}"
        :footbar="true"
        width="78%"
        maxWidth="1024px"
        minWidth="800px"
      >
        <template v-slot:content>
          <el-form
            :model="formModel"
            :rules="rules"
            ref="formModel"
            size="mini"
            label-width="120px"
          >
            <el-form-item label="客户" required>
              <el-input style="width:200px;" v-model="formModel.userName"></el-input>
            </el-form-item>
            <el-form-item label="电话">{{formModel.userTel}}</el-form-item>
            <el-form-item label="渠道">{{formModel.channelDisplay}}</el-form-item>
            <el-form-item label="预约车辆">
              {{formModel.vehicle.carPlate+' '+formModel.vehicle.carType}}
              <el-button style="margin-left:5px;" @click="getUserVehicles();" type="primary">更换车辆</el-button>
            </el-form-item>
            <el-form-item label="提交时间">{{$jscom.toYMDHm(formModel.createdTime)}}</el-form-item>
            <el-form-item label="预约类型">
              <el-radio-group v-model="formModel.reserveType">
                <el-radio
                  v-for="reType in reserveTypeEnumEdit"
                  :key="reType.value"
                  :label="reType.value"
                >{{reType.displayName}}</el-radio>
              </el-radio-group>
            </el-form-item>
            <el-form-item label="预约时间" required>
              {{formModel.reserveTime}}
              <el-button
                style="margin-left:5px;"
                @click="queryReserveDashboard();"
                type="primary"
              >约定时间</el-button>
              <span style="color:red">【注意】预约技师时，须先与该技师确认预约时间。</span>
            </el-form-item>
            <el-form-item label="预约技师">
              <el-select
                v-model="techModel"
                value-key="value"
                clearable
                placeholder="不约技师"
                style="width:200px;"
              >
                <el-option
                  v-for="item in technicianList"
                  :key="item.value"
                  :label="item.displayName"
                  :value="item"
                ></el-option>
              </el-select>
            </el-form-item>

            <el-form-item label="预约备注">
              <el-input type="textarea" :rows="3" v-model="formModel.remark"></el-input>
            </el-form-item>
            <el-form-item>
              <el-image
                style="width: 100px; height: 100px"
                v-for="(itemImage) in formModel.imageList"
                :src="itemImage"
                :key="itemImage"
                :preview-src-list="[itemImage]"
              ></el-image>
            </el-form-item>

            <el-divider content-position="left">预约项目</el-divider>
            <el-table :data="formModel.projects" border stripe size="mini">
              <el-table-column label="项目内容">
                <template slot-scope="scope">{{scope.row.displayName+"*"+scope.row.count}}</template>
              </el-table-column>
              <el-table-column label="总金额" align="right" prop="totalPrice" width="100">
                <template slot-scope="scope">{{$jscom.toYMDHm(scope.row.totalPrice)}}</template>
              </el-table-column>
            </el-table>
            <el-divider content-position="left">历史到店记录</el-divider>
            <el-table :data="formModel.historyReceive" border stripe size="mini" max-height="200">
              <el-table-column label="编号" prop="recId" width="80"></el-table-column>
              <el-table-column label="到店时间" align="center" prop="receiveTime" width="150">
                <template slot-scope="scope">{{$jscom.toYMDHm(scope.row.receiveTime)}}</template>
              </el-table-column>
              <el-table-column label="到店车辆" prop="carPlate"></el-table-column>
              <el-table-column label="服务类型" prop="serviceType" width="100"></el-table-column>
              <el-table-column label="施工技师" prop="receiveBy" width="100"></el-table-column>
            </el-table>
            <el-divider content-position="left">历史处理记录</el-divider>
            <el-timeline>
              <el-timeline-item
                v-for="(activity, index) in formModel.historyProcessList"
                :key="index"
                type="primary"
                :timestamp="titleShow(activity)"
                placement="top"
              >
                <p v-for="(item, index1) in activity.detail" :key="index1">{{item}}</p>
                <!-- {{activity.optTime+' '+activity.optTitle}} -->
              </el-timeline-item>
            </el-timeline>
          </el-form>
        </template>

        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            :disabled="formCheck2"
            :loading="btnSaveLoadingE"
            @click="editReserve()"
          >提交修改</el-button>
        </template>
      </rg-dialog>
    </section>

    <section id="choosetime">
      <rg-dialog
        dlgId="dlgBooking"
        :title="formTitle1"
        :loading="formLoading1"
        :visible.sync="formVisible1"
        :beforeClose="(done)=>{formVisible1 = false}"
        :btnCancel="{label:'关闭',click:(done)=>{formVisible1 = false}}"
        :btnRemove="false"
        :footbar="true"
        width="900px"
      >
        <template v-slot:content>
          <!-- bookingRangData -->
          <el-table
            :data="bookingRangData.data"
            ref="bookingTb"
            border
            size="mini"
            class="booking-table"
            style="width: 100%"
            :max-height="$root.$data.screen.height - 275"
            :cell-class-name="cellClass"
            @cell-click="cellClick"
          >
            <el-table-column label="时间" prop="time" align="center" width="60" fixed="left"></el-table-column>
            <el-table-column
              align="center"
              prop="col_1"
              :formatter="colFormatter"
              :render-header="renderHeader"
            ></el-table-column>
            <el-table-column
              align="center"
              prop="col_2"
              :formatter="colFormatter"
              :render-header="renderHeader"
            ></el-table-column>
            <el-table-column
              align="center"
              prop="col_3"
              :formatter="colFormatter"
              :render-header="renderHeader"
            ></el-table-column>
            <el-table-column
              align="center"
              prop="col_4"
              :formatter="colFormatter"
              :render-header="renderHeader"
            ></el-table-column>
            <el-table-column
              align="center"
              prop="col_5"
              :formatter="colFormatter"
              :render-header="renderHeader"
            ></el-table-column>
            <el-table-column
              align="center"
              prop="col_6"
              :formatter="colFormatter"
              :render-header="renderHeader"
            ></el-table-column>
            <el-table-column
              align="center"
              prop="col_7"
              :formatter="colFormatter"
              :render-header="renderHeader"
            ></el-table-column>
            <el-table-column
              align="center"
              prop="col_8"
              :formatter="colFormatter"
              :render-header="renderHeader"
            ></el-table-column>
            <el-table-column
              align="center"
              prop="col_9"
              :formatter="colFormatter"
              :render-header="renderHeader"
            ></el-table-column>
            <el-table-column
              align="center"
              prop="col_10"
              :formatter="colFormatter"
              :render-header="renderHeader"
            ></el-table-column>
          </el-table>
        </template>
        <template v-slot:footer>
          <el-button icon="el-icon-check" size="mini" type="primary" @click="changeReserveTime()">确定</el-button>
        </template>
      </rg-dialog>
    </section>
    <section id="chooseCar">
      <rg-dialog
        :title="formTitle2"
        :loading="formLoading2"
        :visible.sync="formVisible2"
        :beforeClose="cancel2"
        :btnCancel="{label:'关闭',click:(done)=>{cancel2()}}"
        :btnRemove="false"
        :footbar="true"
        width="900px"
      >
        <template v-slot:content>
          <el-radio-group class="vehicle-radio" v-model="currentCar" @change="changeVehicleSelect">
            <el-radio
              v-for="choseCar in allUserVehicle"
              :key="choseCar.carId"
              :label="choseCar"
            >{{choseCar.carPlate +' '+ choseCar.carType}}</el-radio>
            <el-radio :label="noNeedCar">{{noNeedCar.carPlate}}</el-radio>
            <div
              style="border:1px solid #dadce0;margin-top:8px;margin-left:20px;width:calc(100% - 50px)"
            >
              <el-radio
                style="margin-left:0;margin-top:0;width:calc(100% - 30px);border:0;"
                :label="addCar"
              >{{addCar.carPlate}}</el-radio>
              <el-form size="mini" label-width="80px" v-show="showPannel">
                <el-form-item label="车牌" required>
                  <el-input
                    clearable
                    maxlength="10"
                    placeholder="车牌号"
                    style="width:120px"
                    v-model="userNewCar.carPlate"
                  ></el-input>
                </el-form-item>
                <el-form-item label="车型" required>
                  <el-input
                    :disabled="vehicleInputDisable"
                    clearable
                    maxlength="50"
                    placeholder="请输入车型"
                    style="width:320px"
                    v-model="userNewCar.vehicle"
                  ></el-input>
                  <el-checkbox v-model="standVehicle" @change="changeVehicleType">标准车型</el-checkbox>
                </el-form-item>
                <el-form-item v-show="vehicleInputDisable" label="标准车型">
                  <el-select
                    class="carSelect"
                    v-model="brand"
                    @change="getVehicleByBrand();"
                    filterable
                    placeholder="-请选择品牌-"
                  >
                    <el-option
                      v-for="item in brandList"
                      :key="item.brand"
                      :label="item.brand"
                      :value="item.brand"
                    ></el-option>
                  </el-select>
                  <el-select
                    class="carSelect"
                    v-model="vehicleModel"
                    value-key="vehicleId"
                    @change="getPaiLiangByVehicleId"
                    filterable
                    placeholder="-请选择车系-"
                  >
                    <el-option
                      v-for="item in vehicleList"
                      :key="item.vehicleId"
                      :label="item.vehicle"
                      :value="item"
                    ></el-option>
                  </el-select>
                  <el-select
                    class="carSelect"
                    v-model="paiLiang"
                    @change="getVehicleNianByPaiLiang()"
                    filterable
                    placeholder="-请选择排量-"
                  >
                    <el-option v-for="item in paiLiangList" :key="item" :label="item" :value="item"></el-option>
                  </el-select>
                  <el-select
                    class="carSelect"
                    v-model="nian"
                    @clear="salesModel={tid:'',salesName:''};salesNameList=[];"
                    @change="getVehicleSalesNameByNian()"
                    clearable
                    filterable
                    placeholder="-请选择年份-"
                  >
                    <el-option v-for="item in nianList" :key="item" :label="item" :value="item"></el-option>
                  </el-select>
                  <el-select
                    class="carSelect"
                    v-model="salesModel"
                    value-key="tid"
                    clearable
                    filterable
                    placeholder="-请选择车款-"
                  >
                    <el-option
                      v-for="item in salesNameList"
                      :key="item.tid"
                      :label="item.salesName"
                      :value="item"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-form>
            </div>
          </el-radio-group>
        </template>

        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            :disabled="formCheck2"
            :loading="btnSaveLoading"
            @click="chooseNewCar()"
          >确定</el-button>
        </template>
      </rg-dialog>
    </section>

    <section id="cancelReserve">
      <rg-dialog
        :title="formTitle3"
        :visible.sync="formVisible3"
        :beforeClose="cancel3"
        :btnCancel="{label:'关闭',click:(done)=>{cancel3()}}"
        :btnRemove="false"
        :footbar="true"
        width="400px"
        maxWidth="800px"
        minWidth="400px"
      >
        <template v-slot:content>
          <el-input
            style="margin-top:-8px;"
            type="textarea"
            :rows="3"
            maxlength="200"
            placeholder="请输入取消原因，限200字以内"
            v-model="cancelReason"
          ></el-input>
        </template>

        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            :loading="btnSaveLoadingC"
            @click="cancleReserve()"
          >确定</el-button>
        </template>
      </rg-dialog>
    </section>

    <section id="addReserve">
      <rg-dialog
        :title="formTitleA"
        :loading="formLoadingA"
        :visible.sync="formVisibleA"
        :beforeClose="cancelA"
        :btnCancel="{label:'关闭',click:(done)=>{cancelA()}}"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        height="calc(100vh - 150px)"
        maxWidth="1024px"
        minWidth="800px"
      >
        <template v-slot:content>
          <el-form :model="addReserve" ref="frmAddReserve" label-width="120px" size="mini">
            <el-form-item label="客户手机" required>
              <el-input
                style="width:150px"
                clearable
                placeholder="请输入客户手机"
                v-model="inputMobile"
                maxlength="11"
                @keyup.enter.native="getUserInfo"
              ></el-input>
              <el-button
                type="warning"
                style="margin-left:5px;"
                icon="el-icon-search"
                @click="getUserInfo"
              >搜索车主</el-button>
            </el-form-item>
            <el-row v-show="reserveRecord">
              <el-divider content-position="left">预约记录</el-divider>
              <ul class="booking-his">
                <li v-for="itemReserve in validReserveList" :key="itemReserve.reserveId">
                  <span>{{itemReserve.reserveTime+' | '+itemReserve.reserveTypeName+' | '+$jscom.toYMDHm(itemReserve.carPlate)}}</span>
                  <el-link type="primary" @click="routerToDetail(itemReserve.reserveId)">查看详情</el-link>
                </li>
              </ul>
            </el-row>
            <el-row v-show="createReserve">
              <el-divider content-position="left">
                创建新预约
                <font class="warning" v-show="newUser">找不到该客户，请手动录入以下信息</font>
              </el-divider>
              <el-form-item label="客户名称">
                <el-input style="width:150px;" v-model="addReserve.userName"></el-input>
              </el-form-item>
              <el-form-item label="选择车辆">
                <el-radio-group v-model="chooseAddCar" @change="changeVehicleSelectAdd">
                  <el-radio
                    class="rediaCar"
                    v-for="choseCar in addAllUserVehicle"
                    :label="choseCar"
                  >{{choseCar.carNumber +' '+ choseCar.carType}}</el-radio>
                  <el-radio class="rediaCar" :label="noNeedCar">{{noNeedCar.carPlate}}</el-radio>
                  <el-radio class="rediaCar" :label="addCar">{{addCar.carPlate}}</el-radio>
                  <el-form size="mini" label-width="80px" v-show="showPannelAdd">
                    <el-form-item label="车牌" required>
                      <el-input
                        clearable
                        maxlength="10"
                        placeholder="车牌号"
                        style="width:120px"
                        v-model="userNewCarAdd.carPlate"
                      ></el-input>
                    </el-form-item>
                    <el-form-item label="车型" required>
                      <el-input
                        :disabled="vehicleInputDisableAdd"
                        clearable
                        maxlength="50"
                        placeholder="请输入车型"
                        style="width:320px"
                        v-model="userNewCarAdd.vehicle"
                      ></el-input>
                      <el-checkbox v-model="standVehicleAdd" @change="changeVehicleTypeAdd">标准车型</el-checkbox>
                    </el-form-item>
                    <el-form-item v-show="vehicleInputDisableAdd" label="标准车型">
                      <el-select
                        class="carSelect"
                        v-model="brand"
                        @change="getVehicleByBrand();"
                        filterable
                        placeholder="-请选择品牌-"
                      >
                        <el-option
                          v-for="item in brandList"
                          :key="item.brand"
                          :label="item.brand"
                          :value="item.brand"
                        ></el-option>
                      </el-select>
                      <el-select
                        class="carSelect"
                        v-model="vehicleModel"
                        value-key="vehicleId"
                        @change="getPaiLiangByVehicleId"
                        filterable
                        placeholder="-请选择车系-"
                      >
                        <el-option
                          v-for="item in vehicleList"
                          :key="item.vehicleId"
                          :label="item.vehicle"
                          :value="item"
                        ></el-option>
                      </el-select>
                      <el-select
                        class="carSelect"
                        v-model="paiLiang"
                        @change="getVehicleNianByPaiLiang()"
                        filterable
                        placeholder="-请选择排量-"
                      >
                        <el-option
                          v-for="item in paiLiangList"
                          :key="item"
                          :label="item"
                          :value="item"
                        ></el-option>
                      </el-select>
                      <el-select
                        class="carSelect"
                        v-model="nian"
                        @clear="salesModel={tid:'',salesName:''};salesNameList=[];"
                        @change="getVehicleSalesNameByNian()"
                        clearable
                        filterable
                        placeholder="-请选择年份-"
                      >
                        <el-option v-for="item in nianList" :key="item" :label="item" :value="item"></el-option>
                      </el-select>
                      <el-select
                        class="carSelect"
                        v-model="salesModel"
                        value-key="tid"
                        clearable
                        filterable
                        placeholder="-请选择车款-"
                      >
                        <el-option
                          v-for="item in salesNameList"
                          :key="item.tid"
                          :label="item.salesName"
                          :value="item"
                        ></el-option>
                      </el-select>
                    </el-form-item>
                  </el-form>
                </el-radio-group>
              </el-form-item>
              <el-form-item label="预约类型">
                <el-radio-group v-model="addReserve.reserveType">
                  <el-radio
                    v-for="reType in reserveTypeEnumEdit"
                    :label="reType.value"
                  >{{reType.displayName}}</el-radio>
                </el-radio-group>
              </el-form-item>
              <el-form-item label="预约时间">
                <span>{{addReserve.reserveTime}}</span>
                <el-button
                  style="margin-left:5px;"
                  @click="queryReserveDashboardForAdd();"
                  type="primary"
                  size="mini"
                >约定时间</el-button>
                <span style="color:red">【注意】预约技师时，须先与该技师确认预约时间。</span>
              </el-form-item>
              <el-form-item label="预约技师">
                <el-select
                  v-model="techModelForAdd"
                  value-key="value"
                  clearable
                  size="small"
                  placeholder="不约技师"
                  style="width:150px;"
                >
                  <el-option
                    v-for="item in technicianList"
                    :key="item.value"
                    :label="item.displayName"
                    :value="item"
                  ></el-option>
                </el-select>
              </el-form-item>
              <el-form-item label="预约备注">
                <el-input type="textarea" :rows="2" v-model="addReserve.remark"></el-input>
              </el-form-item>
            </el-row>
          </el-form>
        </template>

        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            :disabled="submitDisable"
            :loading="btnSaveLoadingA"
            @click="addReserveSubmit()"
          >提交</el-button>
        </template>
      </rg-dialog>
    </section>
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/customermanage/reserve";
export default {
  name: "reservelist",
  data() {
    return {
      showPannel: false,
      reserveTypeEnum: [{ value: "", displayName: "全部" }],
      reserveTypeEnumEdit: [],
      reserveChannelEnum: [{ value: 0, displayName: "全部" }],
      technicianList: [],
      statusEnum: [{ value: 0, displayName: "全部" }],
      condition: {
        userTel: "",
        carPlate: "",
        reserveType: "",
        reserveChannel: 0,
        status: 0,
        reserveTech: false,
        startDate: "",
        endDate: "",
        pageIndex: 1,
        pageSize: 20
      },
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      tableData: [],
      formTitle: "预约处理",
      formVisible: false,
      formInit: {
        reserveId: 0,
        userId: "",
        userName: "",
        userTel: "",
        channelDisplay: "",
        vehicle: {
          carId: "",
          carPlate: "",
          carType: "",
          brand: "",
          vehicle: "",
          vehicleId: "",
          paiLiang: "",
          nian: "",
          tid: "",
          salesName: ""
        },
        createdTime: "",
        reserveType: "",
        reserveTypeName: "",
        reserveTime: "",
        technician: "",
        techId: "",
        historyProcessList: "",
        imageList: [],
        remark: ""
      },
      formModel: {
        reserveId: 0,
        userId: "",
        userName: "",
        userTel: "",
        channelDisplay: "",
        vehicle: {
          carId: "",
          carPlate: "",
          carType: "",
          brand: "",
          vehicle: "",
          vehicleId: "",
          paiLiang: "",
          nian: "",
          tid: "",
          salesName: ""
        },
        createdTime: "",
        reserveType: "",
        reserveTypeName: "",
        reserveTime: "",
        technician: "",
        techId: "",
        historyProcessList: "",
        imageList: [],
        remark: ""
      },
      techModel: {
        value: "",
        displayName: ""
      },
      rules: {},
      formLoading: false,
      btnSaveLoading: false,
      formCheck1: true,
      formCheck2: true,
      formTitle1: "选择预约日期",
      formLoading1: false,
      formVisible1: false,
      dialogDateWidth: "822px",
      innerHtmlDate: "",
      formTitle2: "选择预约车辆",
      formLoading2: false,
      formVisible2: false,
      allUserVehicle: [],
      currentCar: {
        carId: "",
        carPlate: "",
        carPlate: "",
        brand: "",
        vehicle: "",
        vehicleId: "",
        paiLiang: "",
        nian: "",
        tid: "",
        salesName: ""
      },
      noNeedCar: {
        carId: "0",
        carPlate: "不指定车辆",
        carType: ""
      },
      addCar: {
        carId: "1",
        carPlate: "新增车辆",
        carType: ""
      },
      userNewCar: {
        carPlate: "",
        brand: "",
        vehicle: "",
        vehicleId: "",
        paiLiang: "",
        nian: "",
        tid: "",
        salesName: ""
      },
      standVehicle: false,
      vehicleInputDisable: false,
      brandList: [],
      vehicleList: [],
      paiLiangList: [],
      nianList: [],
      salesNameList: [],
      brand: "",
      vehicleModel: {
        vehicleId: "",
        vehicle: ""
      },
      paiLiang: "",
      nian: "",
      salesModel: {
        tid: "",
        salesName: ""
      },
      formTitle3: "取消预约",
      formVisible3: false,
      cancelReason: "",
      btnSaveLoadingC: false,
      btnSaveLoadingE: false,
      formTitleA: "预约登记",
      formLoadingA: false,
      formVisibleA: false,
      btnSaveLoadingA: false,
      addReserve: {
        userId: "",
        userName: "",
        userTel: "",
        reserveType: "",
        reserveTime: "",
        techId: "",
        techName: "",
        remark: "",
        vehicle: {
          carId: "",
          carPlate: "",
          carType: "",
          brand: "",
          vehicle: "",
          vehicleId: "",
          paiLiang: "",
          nian: "",
          tid: "",
          salesName: ""
        }
      },
      techModelForAdd: {
        value: "",
        displayName: ""
      },
      chooseAddCar: {
        carId: "",
        carPlate: "",
        carType: "",
        brand: "",
        vehicle: "",
        vehicleId: "",
        paiLiang: "",
        nian: "",
        tid: "",
        salesName: ""
      },
      inputMobile: "",
      addAllUserVehicle: [],
      validReserveList: [],
      createReserve: false,
      reserveRecord: false,
      newUser: false,
      chooseType: 0, //编辑or新增
      showPannelAdd: false,
      userNewCarAdd: {
        carPlate: "",
        vehicle: ""
      },
      vehicleInputDisableAdd: false,
      standVehicleAdd: false,
      submitDisable: true,
      bookingRangData: [], //预约时间数据
      bookingTimeFrmDH: ""
    };
  },
  created() {
    this.getCondition();
    this.fetchData();
    this.getBrandList();
  },
  watch: {
  },
  methods: {
    fetchData() {
      this.getPaged();
    },
    titleShow(activity) {
      return activity.optTime + " " + activity.optTitle;
    },
    contentShow(detail) {
      //for(var )

      return '<span style="margin-left:20px;">sdsd</span>';
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
      appSvc
        .getReserveList(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (flag) {
              this.$message({
                message: res.message || "查询成功",
                type: "success"
              });
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });
    },
    getCondition() {
      appSvc
        .getReserveListCondition()
        .then(
          res => {
            var data = res.data;
            this.reserveTypeEnum = data.type;
            this.reserveChannelEnum = data.channel;
            this.statusEnum = data.status;
            this.reserveTypeEnumEdit = data.type.filter(
              item => item.value !== ""
            );
            this.technicianList = data.technician;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    routerToDetail(reserveId) {
      this.formVisible = true;
      this.formLoading = true;
      var reserveRequest = {
        reserveId: reserveId
      };
      this.formCheck1 = true;
      this.formCheck2 = true;
      appSvc
        .getReserveDetailForWeb(reserveRequest)
        .then(
          res => {
            var data = res.data;
            if (data == null) {
              this.formModel = this.formInit;
              this.$message({
                message: "系统异常，请关闭重新打开",
                type: "error"
              });
            } else {
              this.formModel = data;
              this.techModel = {
                value: this.formModel.techId,
                displayName: this.formModel.technician
              };
              if (data.operation.indexOf("CanCancel") >= 0) {
                this.formCheck1 = false;
              }
              if (data.operation.indexOf("CanModify") >= 0) {
                this.formCheck2 = false;
              }
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.formLoading = false;
        });
    },
    iniDiaVehicle() {
      this.currentCar = {
        carId: "",
        carPlate: "",
        carPlate: "",
        brand: "",
        vehicle: "",
        vehicleId: "",
        paiLiang: "",
        nian: "",
        tid: "",
        salesName: ""
      };

      this.userNewCar = {
        carPlate: "",
        brand: "",
        vehicle: "",
        vehicleId: "",
        paiLiang: "",
        nian: "",
        tid: "",
        salesName: ""
      };

      this.showPannel = false;

      this.standVehicle = false;
      this.vehicleInputDisable = false;

      this.vehicleList = [];
      this.paiLiangList = [];
      this.nianList = [];
      this.salesNameList = [];

      this.brand = "";
      this.vehicleModel = {
        vehicleId: "",
        vehicle: ""
      };
      this.paiLiang = "";
      this.nian = "";
      this.salesModel = {
        tid: "",
        salesName: ""
      };
    },
    getUserVehicles() {
      this.iniDiaVehicle();
      this.formVisible2 = true;
      this.formLoading2 = true;
      var vehicleRequest = {
        userId: this.formModel.userId
      };
      appSvc
        .getAllUserVehicles(vehicleRequest)
        .then(
          res => {
            var data = res.data;
            this.allUserVehicle = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.formLoading2 = false;
        });
    },
    queryReserveDashboard() {
      this.formVisible1 = true;
      this.formLoading1 = true;
      appSvc
        .getReserveDateForWeb()
        .then(
          res => {
            var data = res.data;
            this.bindReserveDateData(data, this.formModel.reserveTime);
            this.chooseType = 0;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.formLoading1 = false;
        });
    },
    queryReserveDashboardForAdd() {
      this.formVisible1 = true;
      this.formLoading1 = true;
      appSvc
        .getReserveDateForWeb()
        .then(
          res => {
            var data = res.data;
            this.bindReserveDateData(data, this.addReserve.reserveTime);
            this.chooseType = 1;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.formLoading1 = false;
        });
    },
    cancel(formName) {
      this.formVisible = false;

      this.resetForm();

      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },
    cancel2() {
      this.formVisible2 = false;
    },
    cancel3() {
      this.formVisible3 = false;
    },
    cancelA() {
      this.formVisibleA = false;
    },
    resetForm() {
      this.formModel = JSON.parse(JSON.stringify(this.formInit));
    },
    renderHeader(h, { column, $index }) {
      try {
        return this.bookingRangData.header[$index].label;
      } catch {}
    },
    cellClass({ row, column, rowIndex, columnIndex }) {
      if (columnIndex === 0) return "time";
      let cellValue = row["col_" + columnIndex];
      if (Math.max(cellValue.enableNum, cellValue.reservedNum || 0) == 0)
        return "disabled";
      if (cellValue.overdue) return "overdue";
      if ((cellValue.enableNum || 0) == (cellValue.reservedNum || 0))
        return "full";
      return "normal";
    },
    cellClick(row, column, cell, event) {
      if (this.$jscom.hasClass(cell, "normal")) {
        this.$jscom.removeClass(
          document.querySelector(".cell-selected"),
          "cell-selected"
        );
        this.$nextTick(() => {
          this.$jscom.addClass(cell, "cell-selected");
        });
        //选中预约时间
        this.bookingRangData.selected = row[column.property].dateTimePart;
      }
    },
    colFormatter(row, column, cellValue, index) {
      if (Math.max(cellValue.enableNum, cellValue.reservedNum || 0) == 0)
        return "";
      return (cellValue.reservedNum || 0) + "/" + (cellValue.enableNum || 0);
    },
    bindReserveDateData(data) {
      let d = {
        data: [],
        header: [{ label: "时间", field: "time", width: 90 }]
      };
      data.timePartList.forEach(t => {
        d.data.push({ time: t });
      });
      let iCol = 0,
        iRow = 0;
      data.reserveDateInfoList.forEach(e => {
        iCol++;
        d.header.push({
          date: e.date,
          week: e.week,
          fullReserve: e.fullReserve,
          label: e.date + "\n" + e.week
        });
        iRow = 0;
        e.items.forEach(item => {
          d.data[iRow]["col_" + iCol] = item;
          iRow++;
        });
      });
      this.bookingRangData = d;
    },
    changeReserveTime() {
      if (!this.bookingRangData.selected) {
        return this.$message({
          message: "请选择预约时间",
          type: "error"
        });
      } else {
        this.formVisible1 = false;
        this.formModel.reserveTime = this.bookingRangData.selected;
        this.addReserve.reserveTime= this.bookingRangData.selected;
      }
    },
    changeVehicleSelect(selVal) {
      if (selVal.carId == "1") {
        this.showPannel = true;
      } else {
        this.showPannel = false;
      }
    },
    changeVehicleSelectAdd(selVal) {
      if (selVal.carId == "1") {
        this.showPannelAdd = true;
      } else {
        this.showPannelAdd = false;
      }
    },
    getBrandList() {
      appSvc
        .getVehicleBrandList()
        .then(
          res => {
            var data = res.data;
            this.brandList = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getVehicleByBrand() {
      if (this.brand == "") {
        return;
      }
      this.vehicleModel = {
        vehicleId: "",
        vehicle: ""
      };
      this.paiLiang = "";
      this.nian = "";
      this.salesModel = {
        tid: "",
        salesName: ""
      };
      this.vehicleList = [];
      this.paiLiangList = [];
      this.nianList = [];
      this.salesNameList = [];
      var vehicleCondition = { brand: this.brand };
      appSvc
        .getVehicleListByBrand(vehicleCondition)
        .then(
          res => {
            var data = res.data;
            this.vehicleList = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getPaiLiangByVehicleId(val) {
      var vehicleId = val.vehicleId;
      if (vehicleId == "") {
        return;
      }
      this.paiLiang = "";
      this.nian = "";
      this.salesModel = {
        tid: "",
        salesName: ""
      };
      this.paiLiangList = [];
      this.nianList = [];
      this.salesNameList = [];
      var paiLiangRequest = { vehicleId: vehicleId };
      console.log(JSON.stringify(paiLiangRequest));
      appSvc
        .getPaiLiangByVehicleId(paiLiangRequest)
        .then(
          res => {
            var data = res.data;
            this.paiLiangList = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getVehicleNianByPaiLiang() {
      var vehicleId = this.vehicleModel.vehicleId;
      if (vehicleId == "" || this.paiLiang == "") {
        return;
      }
      this.nian = "";
      this.salesModel = {
        tid: "",
        salesName: ""
      };
      this.nianList = [];
      this.salesNameList = [];
      var nianRequest = { vehicleId: vehicleId, paiLiang: this.paiLiang };
      appSvc
        .getVehicleNianByPaiLiang(nianRequest)
        .then(
          res => {
            var data = res.data;
            this.nianList = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getVehicleSalesNameByNian() {
      var vehicleId = this.vehicleModel.vehicleId;
      if (vehicleId == "" || this.paiLiang == "" || this.nian == "") {
        return;
      }
      this.salesModel = {
        tid: "",
        salesName: ""
      };
      this.salesNameList = [];
      var tidRequest = {
        vehicleId: vehicleId,
        paiLiang: this.paiLiang,
        nian: this.nian
      };
      appSvc
        .getVehicleSalesNameByNian(tidRequest)
        .then(
          res => {
            var data = res.data;
            this.salesNameList = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    changeVehicleType() {
      this.userNewCar.vehicle = "";
      this.vehicleInputDisable = this.standVehicle;
    },
    changeVehicleTypeAdd() {
      this.userNewCarAdd.vehicle = "";
      this.vehicleInputDisableAdd = this.standVehicleAdd;
    },
    //选择车辆
    chooseNewCar() {
      var chooseCar = this.currentCar;
      if (chooseCar.carId == "") {
        this.$message({ message: "请选择车辆", type: "error" });
        return;
      }
      if (chooseCar.carId == "1") {
        if (this.userNewCar.carPlate == "") {
          this.$message({ message: "请输入车牌号", type: "error" });
          return;
        }
        if (this.standVehicle) {
          if (this.brand == "") {
            this.$message({ message: "请选择品牌", type: "error" });
            return;
          }
          if (this.vehicleModel.vehicleId == "") {
            this.$message({ message: "请选择车系", type: "error" });
            return;
          }
          if (this.paiLiang == "") {
            this.$message({ message: "请选择排量", type: "error" });
            return;
          }
          if (this.nian == "") {
            this.$message({ message: "请选择年份", type: "error" });
            return;
          }
          if (this.salesModel.tid == "") {
            this.$message({ message: "请选择款型", type: "error" });
            return;
          }
          this.iniAddCar();
          this.formModel.vehicle.carPlate = this.userNewCar.carPlate;
          this.formModel.vehicle.brand = this.brand;
          this.formModel.vehicle.vehicleId = this.vehicleModel.vehicleId;
          this.formModel.vehicle.vehicle = this.vehicleModel.vehicle;
          this.formModel.vehicle.paiLiang = this.paiLiang;
          this.formModel.vehicle.nian = this.nian;
          this.formModel.vehicle.tid = this.salesModel.tid;
          this.formModel.vehicle.salesName = this.salesModel.salesName;
          this.formModel.vehicle.carType =
            this.vehicleModel.vehicle +
            "|" +
            this.paiLiang +
            "|" +
            this.nian +
            "|" +
            this.salesModel.salesName;
        } else {
          if (this.userNewCar.vehicle == "") {
            this.$message({ message: "请输入车型", type: "error" });
            return;
          }
          this.iniAddCar();
          this.formModel.vehicle.carPlate = this.userNewCar.carPlate;
          this.formModel.vehicle.vehicle = this.userNewCar.vehicle;
          this.formModel.vehicle.carType = this.userNewCar.vehicle;
        }
      } else if (chooseCar.carId == "0") {
        this.iniAddCar();
        this.formModel.vehicle.carType = chooseCar.carPlate;
      } else {
        this.formModel.vehicle = chooseCar;
      }
      this.formVisible2 = false;
    },
    iniAddCar() {
      this.formModel.vehicle.carId = "";
      this.formModel.vehicle.carPlate = "";
      this.formModel.vehicle.carType = "";
      this.formModel.vehicle.brand = "";
      this.formModel.vehicle.vehicle = "";
      this.formModel.vehicle.vehicleId = "";
      this.formModel.vehicle.paiLiang = "";
      this.formModel.vehicle.nian = "";
      this.formModel.vehicle.tid = "";
      this.formModel.vehicle.salesName = "";
    },
    cancelShopReserveDia() {
      this.formVisible3 = true;
      this.cancelReason = "";
    },
    cancleReserve() {
      if (this.cancelReason == "") {
        this.$message({ message: "请输入取消原因", type: "error" });
        return;
      }
      this.btnSaveLoadingC = true;
      var cancelRequest = {
        reserveId: this.formModel.reserveId,
        cancelReason: this.cancelReason
      };
      appSvc
        .cancelReserve(cancelRequest)
        .then(
          res => {
            this.formVisible3 = false;
            var data = res.data;
            if (data) {
              this.$message({ message: "取消成功！", type: "success" });
              this.formVisible = false;
              this.getPaged();
            } else {
              this.$message({ message: "取消失败，请重试！", type: "error" });
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.btnSaveLoadingC = false;
        });
    },
    editReserve() {
      var editRequest = {
        userName: this.formModel.userName,
        reserveId: this.formModel.reserveId,
        reserveType: this.formModel.reserveType,
        reserveTime: this.formModel.reserveTime,
        techId: this.techModel.value,
        techName: this.techModel.displayName,
        remark: this.formModel.remark,
        vehicle: this.formModel.vehicle
      };
      this.btnSaveLoadingE = true;
      appSvc
        .editReserve(editRequest)
        .then(
          res => {
            var data = res.data;
            if (data) {
              this.$message({ message: "修改成功！", type: "success" });
              this.formVisible = false;
              this.getPaged();
            } else {
              this.$message({ message: "修改失败，请重试！", type: "error" });
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.btnSaveLoadingE = false;
        });
    },
    add() {
      this.formVisibleA = true;
      this.createReserve = false;
      this.reserveRecord = false;
      this.inputMobile = "";
      this.submitDisable = true;
    },
    getUserInfo() {
      if (this.inputMobile == "") {
        this.$message({ message: "请输入手机号查询", type: "error" });
        return;
      }

      var myreg = /^[1][0-9]{10}$/;

      if (!myreg.test(this.inputMobile)) {
        this.$message({ message: "手机号格式不正确", type: "error" });
        return;
      }
      var userRequest = {
        userTel: this.inputMobile
      };
      appSvc
        .getUserDetailByUserTel(userRequest)
        .then(
          res => {
            this.iniAddReserve();
            this.addReserve.userTel = this.inputMobile;
            var data = res.data;
            if (data != null) {
              this.addReserve.userId = data.userId;
              this.addReserve.userName = data.userName;
              this.addAllUserVehicle = data.cars;
              this.newUser = false;
            } else {
              this.newUser = true;
            }
            this.createReserve = true;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});

      appSvc
        .getValidReserve(userRequest)
        .then(
          res => {
            var data = res.data;
            if (data != null && data.length > 0) {
              this.validReserveList = data;
              this.reserveRecord = true;
            } else {
              this.reserveRecord = false;
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    addReserveSubmit() {
      if (this.addReserve.userName == "") {
        this.$message({ message: "客户名称不能为空", type: "error" });
        return;
      }

      var chooseCar = this.chooseAddCar;
      if (chooseCar.carId == "") {
        this.$message({ message: "请选择车辆", type: "error" });
        return;
      }
      if (chooseCar.carId == "1") {
        if (this.userNewCarAdd.carPlate == "") {
          this.$message({ message: "请输入车牌号", type: "error" });
          return;
        }
        if (this.standVehicleAdd) {
          if (this.brand == "") {
            this.$message({ message: "请选择品牌", type: "error" });
            return;
          }
          if (this.vehicleModel.vehicleId == "") {
            this.$message({ message: "请选择车系", type: "error" });
            return;
          }
          if (this.paiLiang == "") {
            this.$message({ message: "请选择排量", type: "error" });
            return;
          }
          if (this.nian == "") {
            this.$message({ message: "请选择年份", type: "error" });
            return;
          }
          if (this.salesModel.tid == "") {
            this.$message({ message: "请选择款型", type: "error" });
            return;
          }
          this.addReserve.vehicle.carPlate = this.userNewCarAdd.carPlate;
          this.addReserve.vehicle.brand = this.brand;
          this.addReserve.vehicle.vehicleId = this.vehicleModel.vehicleId;
          this.addReserve.vehicle.vehicle = this.vehicleModel.vehicle;
          this.addReserve.vehicle.paiLiang = this.paiLiang;
          this.addReserve.vehicle.nian = this.nian;
          this.addReserve.vehicle.tid = this.salesModel.tid;
          this.addReserve.vehicle.salesName = this.salesModel.salesName;
          this.addReserve.vehicle.carType =
            this.vehicleModel.vehicle +
            "|" +
            this.paiLiang +
            "|" +
            this.nian +
            "|" +
            this.salesModel.salesName;
        } else {
          if (this.userNewCarAdd.vehicle == "") {
            this.$message({ message: "请输入车型", type: "error" });
            return;
          }
          this.addReserve.vehicle.carPlate = this.userNewCarAdd.carPlate;
          this.addReserve.vehicle.vehicle = this.userNewCarAdd.vehicle;
          this.addReserve.vehicle.carType = this.userNewCarAdd.vehicle;
        }
      } else if (chooseCar.carId == "0") {
        this.addReserve.vehicle.carType = chooseCar.carPlate;
      } else {
        this.addReserve.vehicle = chooseCar;
      }

      if (this.addReserve.reserveType == "") {
        this.$message({ message: "请选择预约类型", type: "error" });
        return;
      }

      if (this.addReserve.reserveTime == "") {
        this.$message({ message: "请选择预约时间", type: "error" });
        return;
      }

      this.addReserve.techId = this.techModelForAdd.value;
      this.addReserve.techName = this.techModelForAdd.displayName;
      this.btnSaveLoadingA = true;
      appSvc
        .addReserve(this.addReserve)
        .then(
          res => {
            var data = res.data;
            if (data) {
              this.$message({ message: "预约成功！", type: "success" });
              this.formVisibleA = false;
              this.getPaged();
            } else {
              this.$message({ message: "预约失败，请重试！", type: "error" });
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.btnSaveLoadingA = false;
        });
    },
    iniAddReserve() {
      this.addReserve = {
        userId: "",
        userName: "",
        userTel: "",
        reserveType: "",
        reserveTime: "",
        techId: "",
        techName: "",
        remark: "",
        vehicle: {
          carId: "",
          carPlate: "",
          carType: "",
          brand: "",
          vehicle: "",
          vehicleId: "",
          paiLiang: "",
          nian: "",
          tid: "",
          salesName: ""
        }
      };
      this.addAllUserVehicle = [];
      this.techModelForAdd = {
        value: "",
        displayName: ""
      };
      this.chooseAddCar = {
        carId: "",
        carPlate: "",
        carType: "",
        brand: "",
        vehicle: "",
        vehicleId: "",
        paiLiang: "",
        nian: "",
        tid: "",
        salesName: ""
      };
      this.showPannelAdd = false;
      this.userNewCarAdd = {
        carPlate: "",
        vehicle: ""
      };
      this.vehicleInputDisableAdd = false;
      this.standVehicleAdd = false;
      this.vehicleList = [];
      this.paiLiangList = [];
      this.nianList = [];
      this.salesNameList = [];
      this.brand = "";
      this.vehicleModel = {
        vehicleId: "",
        vehicle: ""
      };
      this.paiLiang = "";
      this.nian = "";
      this.salesModel = {
        tid: "",
        salesName: ""
      };
      this.submitDisable = false;
    }
  }
};
</script>

<style lang="scss" scoped>
.fs16 {
  font-size: 16px;
  font-weight: 600;
}
.ml13 {
  margin-left: 13px;
}

.ml30 {
  margin-left: 30px;
}

.ml50 {
  margin-left: 50px;
}

.ml70 {
  margin-left: 70px;
}

.mt10 {
  margin-top: 10px;
}

.elRow {
  margin-top: 25px;
  margin-left: 50px;
  font-size: 13px !important;
}

.elRowAddCar {
  padding: 3px;
  margin-left: 52px;
  font-size: 14px;
}

.vehicle-radio {
  .el-radio {
    margin-left: 20px;
    margin-top: 8px;
    width: calc(100% - 50px);
    border: 1px solid #dadce0;
    padding: 10px;
  }
}

.rediaCar {
  padding: 5px;
  width: calc(100% - 30px);
}

.warning {
  background-color: #fbf8e5;
  padding: 5px;
  margin: -10px 0 10px 20px;
  width: 350px;
  color: red;
  font-size: 12px;
}

/deep/ .tableContainer {
  border-right: 1px solid #dadce0;
  border-bottom: 1px solid #dadce0;
  border-collapse: collapse;
  height: 500px;
  color: black;
  font-size: 12px;
  table-layout: fixed;
  td {
    border-left: 1px solid #dadce0;
    border-top: 1px solid #dadce0;
    text-align: center;
    padding: 0;
    width: 71px;
    height: 46px;
  }
  th {
    border-left: 1px solid #dadce0;
    border-top: 1px solid #dadce0;
    text-align: center;
    padding: 0;
    width: 71px;
    height: 46px;
  }
  tbody {
    display: block;
    width: 100%;
    overflow: auto;
    height: 453px;
  }
  thead tr {
    display: block;
  }
}

/deep/ .el-dialog__body {
  padding: 10px 20px;
}

.table-time-wrapper {
  height: 500px;
  overflow: auto;
}

.carSelect {
  width: 120px;
}

.table-time-wrapper ::-webkit-scrollbar {
  width: 0;
}

/deep/ .td-hover {
  height: 45px;
  line-height: 45px;
}

/deep/ .none-sty {
  background-color: #f8ac59;
}

/deep/ .over-time-sty {
  background-color: #b5bbc3;
}

/deep/ .thHead {
  background-color: #e7f2fc;
}

/deep/ .click-sty {
  img {
    width: 14px;
    height: 14px;
    position: relative;
    top: 2px;
  }
}

/deep/ .chooseStyle {
  padding: 14px 13px;
  cursor: pointer;
  font-weight: normal;
}

/deep/ .blue {
  color: #1b88ee;
}

.customer-list-title {
  border-bottom: 1px solid #cfcfcf;
  padding: 10px;
  font-size: 13px !important;
  margin-top: 20px;
}

/deep/ .el-timeline-item__timestamp {
  font-size: 14px;
  color: #303133;
}

/deep/ .el-timeline-item {
  padding-bottom: 1px;
}
/deep/ .booking-his {
  li {
    line-height: 28px;
    list-style-type:none;
  }
}
/deep/ .booking-table {
  th .cell {
    white-space: pre-line;
  }
  td.time {
    background: content-box;
    &:hover {
      background: content-box;
    }
  }
  td.full {
    background: #f8ac59;
    cursor: not-allowed;
    .cell {
      display: inline;
    }
    &::before {
      text-shadow: 1px 1px 1px white;
      content: "约满";
    }
  }
  td.overdue {
    background: #ccc;
    cursor: not-allowed;
    .cell {
      display: inline;
    }
    &::before {
      text-shadow: 1px 1px 1px white;
      content: "过时";
    }
  }

  td.disabled {
    text-shadow: 1px 1px 1px white;
    background: #ccc;
    cursor: not-allowed;
    &::after {
      content: "未开放";
    }
  }
  td.normal {
    background: white;
    cursor: default;
    &:hover {
      background: darkcyan;
      color: white;
    }
  }
  td.cell-selected {
    background: darkcyan;
    color: white;
    cursor: default;
    &:hover {
      background: darkcyan;
      color: white;
    }
  }
}
</style>