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
    >
      <!-- 搜索 -->
      <template v-slot:condition>
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="客户手机号"
            style="width: 150px"
            prefix-icon="el-icon-search"
            v-model="condition.userTel"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-input
            class="input"
            clearable
            placeholder="客户姓名"
            style="width: 150px"
            prefix-icon="el-icon-search"
            v-model="condition.userName"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-select
            v-model="lastArriveTime"
            @change="startIntimeSelect"
            clearable
            style="width: 120px"
            placeholder="最后到店时间"
          >
            <el-option
              v-for="item in lastReceiveTime"
              :key="item.value"
              :label="item.displayName"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
      </template>
      <!-- 数据表格 -->
      <template v-slot:tb_cols>
        <rg-table-column label="姓名" prop="userName" />
        <rg-table-column label="昵称" prop="nickName" />
        <rg-table-column label="手机号" prop="userTel" fix-min />
        <rg-table-column
          label="性别"
          prop="genderDisplay"
          align="center"
          fix-min
        />
        <rg-table-column
          label="最后到店时间"
          prop="lastInTime"
          is-datetime
          fix-min
        />
        <rg-table-column label="常用车辆">
          <template slot-scope="scope">{{
            scope.row.defaultVehicle == null
              ? ""
              : scope.row.defaultVehicle.carPlate +
                "（" +
                scope.row.defaultVehicle.carType +
                "）"
          }}</template>
        </rg-table-column>
        <rg-table-column fixed="right" label="操作" fix-min align="center">
          <template slot-scope="scope">
            <el-button-group>
              <el-button
                @click="routerToDetail(scope.row.userId)"
                type="primary"
                size="mini"
                >查看详情</el-button
              >
            </el-button-group>
          </template>
        </rg-table-column>
      </template>
    </rg-page>

    <rg-dialog
      :title="formTitle"
      :visible.sync="dialogVisible"
      :beforeClose="
        () => {
          this.dialogVisible = false;
        }
      "
      :btnCancel="{
        label: '关闭',
        click: (done) => {
          this.dialogVisible = false;
        },
      }"
      :btnRemove="false"
      :footbar="true"
      width="90%"
      maxWidth="1200px"
      minWidth="800px"
    >
      <template v-slot:content>
        <el-row>
          <el-divider content-position="left">客户基本资料</el-divider>
          <el-col :span="9">
            <el-form :model="userData" label-width="120px" size="mini">
              <el-form-item label="姓名">
                <label>{{ userData.userName }}</label>
              </el-form-item>
              <el-form-item label="手机">
                <label>{{ userData.userTel }}</label>
              </el-form-item>
              <el-form-item label="邮箱">
                <label>{{ userData.email }}</label>
              </el-form-item>
              <el-form-item label="积分">
                <label>{{ userData.point }}</label>
              </el-form-item>
              <el-form-item label="个性签名">
                <label>{{ userData.personalSign }}</label>
              </el-form-item>
              <el-form-item label="驾驶证到期日">
                <label>{{
                  userData.driverLicenseExpiry != "1900-01-01 00:00:00"
                    ? userData.driverLicenseExpiry
                    : ""
                }}</label>
              </el-form-item>
            </el-form>
          </el-col>
          <el-col :span="9">
            <el-form :model="userData" label-width="120px" size="mini">
              <el-form-item label="昵称">
                <label>{{ userData.nickName }}</label>
              </el-form-item>
              <el-form-item label="性别">
                <label>{{
                  userData.gender == 1 ? "男" : userData.gender == 2 ? "女" : ""
                }}</label>
              </el-form-item>
              <el-form-item label="生日">
                <label>{{ userData.birthDay }}</label>
              </el-form-item>
              <el-form-item label="最后到店时间">
                <label>{{ $jscom.toYMDHm(userData.lastArriveTime) }}</label>
              </el-form-item>
            </el-form>
          </el-col>
          <el-col :span="6">
            <img :src="userData.headUrl" width="150px;" height="150px;" />
          </el-col>
        </el-row>
        <el-row>
          <el-divider content-position="left">客户车辆信息</el-divider>
          <el-table
            :data="vehicleData"
            size="mini"
            height="300"
            border
            style="width: 100%"
          >
            <el-table-column
              label="车牌"
              prop="carNumber"
              width="90"
            ></el-table-column>
            <el-table-column
              label="品牌"
              prop="brand"
              min-width="100"
            ></el-table-column>
            <el-table-column
              label="车系"
              prop="vehicle"
              min-width="180"
            ></el-table-column>
            <el-table-column
              label="排量"
              prop="paiLiang"
              width="90"
            ></el-table-column>
            <el-table-column
              label="生产年份"
              prop="nian"
              align="center"
              width="80"
            ></el-table-column>
            <el-table-column
              label="款型"
              prop="salesName"
              min-width="250"
            ></el-table-column>
            <el-table-column
              label="公里数"
              prop="totalMileage"
              align="right"
              width="90"
            ></el-table-column>
            <el-table-column
              label="车架号"
              prop="vinCode"
              min-width="120"
            ></el-table-column>
            <el-table-column
              label="保险公司"
              prop="insuranceCompany"
              min-width="120"
            ></el-table-column>
            <el-table-column
              label="保险到期日期"
              prop="insureExpireDate"
              min-width="120"
            ></el-table-column>
            <el-table-column label="车辆档案">
              <template slot-scope="scope">
                <el-button
                  type="primary"
                  size="mini"
                  @click="toCarRecord(scope.row)"
                  >查看详情</el-button
                >
              </template>
            </el-table-column>
          </el-table>
        </el-row>
      </template>
    </rg-dialog>
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/customermanage/user";

export default {
  name: "userlist",
  data() {
    return {
      formTitle: "客户详情",
      lastArriveTime: "",
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      condition: {
        pageIndex: 1,
        pageSize: 20,
        startInTime: 0,
        userTel: "",
        userName: "",
      },
      tableData: [],
      lastReceiveTime: [
        {
          displayName: "一个月前",
          value: 1,
        },
        {
          displayName: "三个月前",
          value: 3,
        },
        {
          displayName: "半年前",
          value: 6,
        },
        {
          displayName: "一年前",
          value: 12,
        },
      ],
      dialogVisible: false,
      userData: {
        userName: "",
        userTel: "",
        email: "",
        nickName: "",
        gender: 0,
        birthDay: "",
        headUrl: "",
        personalSign: "",
        point: 0,
        memberLevel: "",
      },
      vehicleData: [],
    };
  },
  created() {
    this.fetchData();
  },

  watch: {},
  methods: {
    toCarRecord(data) {
      this.dialogVisible = false;
      console.log(data);
      this.$router.push({
        name: "carRecord",
        query: data,
      });
    },
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
      appSvc
        .getUserList(this.condition)
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
    routerToDetail(userIdPara) {
      var userRequest = {
        userId: userIdPara,
      };
      appSvc
        .getUserInfo(userRequest)
        .then(
          (res) => {
            var data = res.data;
            this.userData = data;
            this.vehicleData = data.vehicles;
            this.dialogVisible = true;
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
    startIntimeSelect(selVal) {
      if (selVal == undefined || selVal == "") {
        this.condition.startInTime = 0;
      } else {
        this.condition.startInTime = selVal;
      }
    },
  },
};
</script>
