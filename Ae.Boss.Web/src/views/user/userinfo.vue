<template>
  <div class="container">
    <div class="customer-list-title">
      <span>客户基本资料</span>
    </div>
    <el-row>
      <el-col class="elCol">
        <el-row class="elRow">
          <el-col class="titleSpan">姓名：</el-col>
          <el-col class="contentSpan"
            ><span>{{ userData.userName }}</span></el-col
          >
        </el-row>
        <el-row class="elRow">
          <el-col class="titleSpan">手机：</el-col>
          <el-col class="contentSpan"
            ><span>{{ userData.userTel }}</span></el-col
          >
        </el-row>
        <el-row class="elRow">
          <el-col class="titleSpan">邮箱：</el-col>
          <el-col class="contentSpan"
            ><span>{{ userData.email }}</span></el-col
          >
        </el-row>
        <el-row class="elRow">
          <el-col class="titleSpan">积分：</el-col>
          <el-col class="contentSpan"
            ><span>{{ userData.point }}</span></el-col
          >
        </el-row>
        <el-row class="elRow">
          <el-col class="titleSpan">个性签名：</el-col>
          <el-col class="contentSpan"
            ><span>{{ userData.personalSign }}</span></el-col
          >
        </el-row>
        <el-row class="elRow">
          <el-col class="titleSpan">驾驶证到期日：</el-col>
          <el-col class="contentSpan"
            ><span>{{
              userData.driverLicenseExpiry != "1900-01-01 00:00:00"
                ? userData.driverLicenseExpiry
                : ""
            }}</span></el-col
          >
        </el-row>
      </el-col>
      <el-col class="elCol">
        <el-row class="elRow">
          <el-col class="titleSpan">昵称：</el-col>
          <el-col class="contentSpan"
            ><span>{{ userData.nickName }}</span></el-col
          >
        </el-row>
        <el-row class="elRow">
          <el-col class="titleSpan">性别：</el-col>
          <el-col class="contentSpan">
            <span v-if="userData.gender == 1">男</span>
            <span v-else-if="userData.gender == 2">女</span>
          </el-col>
        </el-row>
        <el-row class="elRow">
          <el-col class="titleSpan">生日：</el-col>
          <el-col class="contentSpan"
            ><span>{{ userData.birthDay }}</span></el-col
          >
        </el-row>
        <el-row class="elRow">
          <el-col class="titleSpan">等级：</el-col>
          <el-col class="contentSpan"
            ><span>{{ userData.memberLevel }}</span></el-col
          >
        </el-row>
        <el-row class="elRow">
          <el-col class="titleSpan">联系姓名：</el-col>
          <el-col class="contentSpan"
            ><span>{{ userData.contactName }}</span></el-col
          >
        </el-row>
      </el-col>
      <el-col style="width: 20%; margin-top: 20px">
        <img :src="userData.headUrl" width="150px;" height="150px;" />
      </el-col>
    </el-row>
    <!-- <el-row class="elRow">
          <el-col class="titleSpan">个性签名：</el-col>
          <el-col class="contentSpan"><span>{{ userData.personalSign }}</span></el-col>
      </el-row> -->

    <div class="customer-list-title" style="margin-top: 50px !important">
      <span>客户车辆信息</span>
    </div>
    <el-table :data="vehicleData" border style="width: 100%; margin-top: 20px">
      <el-table-column
        label="车牌"
        prop="carNumber"
        width="150"
      ></el-table-column>
      <el-table-column label="品牌" prop="brand" width="150"></el-table-column>
      <el-table-column
        label="车系"
        prop="vehicle"
        width="240"
      ></el-table-column>
      <el-table-column
        label="排量"
        prop="paiLiang"
        width="150"
      ></el-table-column>
      <el-table-column
        label="生产年份"
        prop="nian"
        width="180"
      ></el-table-column>
      <el-table-column
        label="款型"
        prop="salesName"
        width="300"
      ></el-table-column>
      <el-table-column
        label="公里数"
        prop="totalMileage"
        width="150"
      ></el-table-column>
      <el-table-column label="保险公司" prop="insuranceCompany"></el-table-column>
      <el-table-column label="保险到期日期" prop="insureExpireDate"></el-table-column>
      <el-table-column fixed="right" label="操作" :width="100">
        <template slot-scope="scope">
          <el-button @click="deleteUserCar(scope.row)" type="text" size="small">删除</el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/user/user";
export default {
  name: "userinfo",
  data() {
    return {
      userRequest: {
        userId: this.$route.params.userId,
      },
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
  methods: {
    fetchData() {
      console.log("condition: " + JSON.stringify(this.userRequest));
      this.tableLoading = true;
      appSvc
        .getUserInfo(this.userRequest)
        .then(
          (res) => {
            var data = res.data;
            console.log("111: " + JSON.stringify(data));
            this.userData = data;
            this.vehicleData = data.vehicles;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          // console.log("tableLoading: false");
          this.tableLoading = false;
        });
    },
    
    deleteUserCar(value) {
      
      this.$confirm(value.carNumber+",确定要删除该车辆吗？", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
            
          this.loading = true;
          appSvc
            .deleteUserCar({
              userId: value.userId,
              carId: value.carId,
            })
            .then(
              (res) => {
                var data = res.data;
                this.$message({ message: "删除成功", type: "success" });
          
                this.fetchData();
              },
              (error) => {
                console.log(error);
              }
            )
            .catch(() => {})
            .finally(() => {
              this.loading = false;
            });
        }); 
    },

  },
};
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 88px;
    display: flex;
    align-items: center;
  }
  .titleSpan {
    width: 100px;
    text-align: right;
    font-weight: bold;
  }
  .contentSpan {
    width: calc(100% - 100px);
    font-weight: normal;
  }
  .elCol {
    font-size: 13px !important;
    width: 40%;
  }
  .elRow {
    margin-top: 25px;
    font-size: 13px !important;
  }
  .customer-list-title {
    border-bottom: 1px solid #cfcfcf;
    padding: 10px;
    font-size: 13px !important;
    margin-top: 20px;
  }
}
</style>
