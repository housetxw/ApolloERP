
<template>
  <el-card class="box-card" v-show="showCard" v-loading="loading">
    <div slot="header" class="clearfix">
      <span>选择客户</span>
      <el-button
        style="float: right; padding: 3px 0"
        type="text"
        @click="next"
        v-if="showDetailInfo"
      >下一步</el-button>
    </div>
    <div class="bax-card-container">
      <el-form :model="customer" label-width="120px" ref="formCarInfo" size="mini" :rules="rules">
        <el-form-item label="客户手机" prop="userTel">
          <el-input
            class="input"
            maxlength="11"
            clearable
            placeholder="请输入客户手机号码"
            style="width:160px"
            v-model="customer.userTel"
            @keypress="e=>{if (e.keyCode == 13) {e.cancleBubble = true; e.returnValue = false;return false;}}"
            @change="(v)=>{ 
              if(v.trim().length >=11) search_user(true);
              else if(v.trim()==''){
                resetData();
                showDetailInfo=false;
              }
              }"
          ></el-input>
          <el-button
            type="warning"
            icon="el-icon-search"
            @click="search_user(true)"
            class="s-mini"
          >搜索</el-button>
          <font class="warning" v-show="showDetailInfo && createCustomer">找不到该客户，请手动录入以下信息</font>
        </el-form-item>
        <el-row v-if="showDetailInfo">
          <el-row>
            <el-col :span="12">
              <el-form-item label="客户名称" prop="userName">
                <el-input v-model="customer.userName" clearable maxlength="20" style="width:250px"></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="性别">
                <el-radio-group v-model="customer.gender">
                  <el-radio :label="1">男</el-radio>
                  <el-radio :label="2">女</el-radio>
                  <el-radio :label="0">-</el-radio>
                </el-radio-group>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row v-if="customer.userId">
            <el-row>
              <el-col :span="12">
                <el-form-item label="昵称">
                  <label>{{customer.nickName}}</label>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="生日">
                  <label>{{customer.birthDay}}</label>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item label="积分">
                  <label>{{customer.point}}</label>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="邮箱">
                  <label>{{customer.email}}</label>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item label="等级">
                  <label>{{customer.memberLevel}}</label>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="最后到店时间">
                  <label>{{ $jscom.toYMDHm(customer.lastArriveTime) }}</label>
                </el-form-item>
              </el-col>
            </el-row>
          </el-row>
          <el-divider content-position="left"></el-divider>

          <el-form-item label="选择车辆" required>
            <el-radio-group
              v-model="chooseCar"
              @change="change_car"
              class="car-group"
              v-for="(car,index) in customer.vehicles"
              :key="index"
            >
              <el-row>
                <el-col :span="22">
                  <el-radio border :value="car.carId" :label="car">
                    {{car.carId==1?"新增车辆": car.carNumber +' | '+car.brand +' | '+ car.vehicle+' | '+ car.paiLiang+' | '+ car.nian+' | '+ car.salesName}}
                    {{car.defaultCar===true?'【默认】':''}}
                  </el-radio>
                </el-col>
                <el-col :span="2" style="text-align:center" v-if="car.carId!=1">
                  <el-button
                    type="text"
                    icon="el-icon-edit"
                    @click="$set(car,'edit',!car.edit);chooseCar=car;change_car(car)"
                  >编辑</el-button>
                </el-col>
              </el-row>
              <el-row v-show="car.edit" style="margin-top:5px">
                <car-edit
                  :ref="car.carId==1?'carEdit':''"
                  :show="car.edit"
                  :params="car"
                  :userId="customer.userId"
                  @changed="newCar=>{
                    if(newCar==null){
                      //删除车辆
                      customer.vehicles.splice(index, 1)
                      chooseCar={};
                    }
                    else{
                      if(newCar.carId==car.carId) Object.assign(car, newCar)
                      else customer.vehicles.splice(0, 0, newCar);
                      Object.assign(chooseCar, newCar);
                      $set(car,'edit',!car.edit);
                    }
                  }"
                />
              </el-row>
            </el-radio-group>
          </el-form-item>

          <el-form-item label="联系人信息" required>
            <el-radio-group
              v-model="chooseAddress"
              @change="change_address"
              class="address-group"
              v-for="(address,index) in customer.addresses"
              :key="index"
            >
              <el-row>
                <el-col :span="22">
                  <el-radio border :value="address.addressId" :label="address">
                    {{address.addressId==1?"新增联系人": address.userName +' 电话：'+ address.mobileNumber+' 地址：'+ address.addressLine}}
                    {{address.defaultAddress?'【默认】':''}}
                  </el-radio>
                </el-col>
                <el-col :span="2" style="text-align:center" v-if="address.addressId!=1">
                  <el-button
                    type="text"
                    icon="el-icon-edit"
                    @click="$set(address,'edit',!address.edit);chooseAddress=address;change_address(address)"
                  >编辑</el-button>
                </el-col>
              </el-row>
              <el-row v-show="address.edit" style="margin-top:5px">
                <address-edit
                  :ref="address.addressId==1?'addressEdit':''"
                  :show="address.edit"
                  :params="address"
                  :userId="customer.userId"
                  @changed="newAdress=>{
                    if(newAdress==null){
                      customer.addresses.splice(index, 1);
                       chooseAddress = {};
                    }
                    else {
                      if(newAdress.addressId==address.addressId) Object.assign(address, newAdress);
                      else customer.addresses.splice(0, 0, newAdress);
                      Object.assign(chooseAddress, newAdress);
                      $set(address,'edit',!address.edit);
                    }
                  }"
                />
              </el-row>
            </el-radio-group>
          </el-form-item>
        </el-row>
      </el-form>
    </div>
  </el-card>
</template>

<script>
import { appSvc } from "@/api/user/user";
import carEdit from "./carEdit.vue";
import addressEdit from "./addressEdit.vue";
export default {
  name: "step1",
  components: { carEdit, addressEdit },
  props: {
    showCard: { type: Boolean, default: false },
    params: { type: Object, default: {} }
  },
  data() {
    return {
      formMolel: {
        userTel: "",
        carNumber: "",
        brand: "",
        vehicle: "",
        paiLiang: "",
        nian: "",
        tid: "",
        userName: "",
        mobileNumber: "",
        province: "",
        city: ""
      },
      selected_userID: 0,
      selected_carId: 0,
      selected_addressId: 0,
      loading: false,
      showDetailInfo: false,
      createCustomer: false,
      customer: {
        userTel: "",
        userId: "",
        userName: ""
      },
      rules: {
        userTel: [
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              if (!this.customer.userTel)
                callback(new Error("请输入客户手机号码"));
              else callback();
            }
          }
        ],
        userName: [
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              if (!this.customer.userName)
                callback(new Error("请输入客户姓名"));
              else callback();
            }
          }
        ]
      },
      chooseCar: {},
      chooseAddress: {}
    };
  },
  watch: {
    showCard: function(v, oldv) {
      if (v == true && !this.params.userId) {
        Object.assign(this.$data, this.$options.data());
      }
    }
  },
  mounted() {},
  methods: {
    resetData() {
      this.$refs["formCarInfo"].resetFields();
      this.chooseCar = {};
      this.chooseAddress = {};
    },
    next() {
      this.saveUpdate(() => {
        this.loading = false;
        this.$emit("donext", {
          userId: this.customer.userId,
          customer: this.customer,
          carId: this.chooseCar.carId,
          address: this.chooseAddress,
          vehicle: this.chooseCar,
          reload: true
        });
      });
    },
    validate() {
      return new Promise((resolve, reject) => {
        this.$refs["formCarInfo"].validate(valid => {
          if (
            this.chooseCar.carId == undefined ||
            (this.chooseCar.carId == 1 && !this.chooseCar.edit)
          ) {
            this.$message.error("请选择车辆！");
            return resolve(false);
          }

          if (
            this.chooseAddress.addressId == undefined ||
            (this.chooseAddress.addressId == 1 && !this.chooseAddress.edit)
          ) {
            this.$message.error("请选择联系人信息！");
            return resolve(false);
          }
          if (this.chooseCar.carId == 1) {
            this.$refs.carEdit[0].validate().then(valid => {
              if (this.chooseAddress.addressId == 1)
                this.$refs.addressEdit[0].validate().then(valid => {
                  resolve(valid);
                });
              else resolve(valid);
            });
          } else if (this.chooseAddress.addressId == 1) {
            this.$refs.addressEdit[0].validate().then(valid => {
              resolve(valid);
            });
          } else resolve(valid);
        });
      }).catch(e => {});
    },
    saveUpdate(callback) {
      this.validate().then(valid => {
        if (valid != true) return;
        this.saveUser().then(p => {
          if (this.chooseCar.carId == 1) {
            this.$refs.carEdit[0].saveCar(this.customer.userId, res => {
              if (this.chooseAddress.addressId == 1) {
                this.$refs.addressEdit[0].saveAddress(
                  this.customer.userId,
                  callback
                );
              } else if (callback) callback.call();
            });
          } else if (this.chooseAddress.addressId == 1) {
            this.$refs.addressEdit[0].saveAddress(
              this.customer.userId,
              callback
            );
          } else if (callback) callback.call();
        });
      });
    },
    saveUser() {
      return new Promise((resolve, reject) => {
        this.loading = true;
        let isAddUser = !this.customer.userId;
        let isUpdateUser =
          this.curstomer_back.userName != this.customer.userName ||
          this.curstomer_back.gender != this.customer.gender;

        if (!isAddUser && !isUpdateUser) return resolve(false); //未更信息用户主体信息
        let postData = {
          userId: this.customer.userId,
          userName: this.customer.userName,
          userTel: this.customer.userTel,
          gender: this.customer.gender
        };
        (isAddUser ? appSvc.createUser(postData) : appSvc.saveUser(postData))
          .then(
            res => {
              this.customer.userId = res.data;
              this.loading = false;
              resolve(res.data);
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {})
          .finally(() => {
            this.loading = false;
          });
      });
    },

    search_user(callback) {
      if (!this.customer.userTel || this.customer.userTel.trim().length < 11) {
        return this.$message.error("请正确输入手机号码！");
      }
      if (callback == true) {
        //清深选择
        this.chooseCar = {};
        this.chooseAddress = {};
      }
      this.loading = true;
      appSvc
        .getUserInfoByUserTel({ userTel: this.customer.userTel.trim() })
        .then(
          res => {
            var data = res.data || {};
            data.vehicles = data.vehicles || [];
            data.addresses = data.addresses || [];
            data.userTel = this.customer.userTel;
            data.gender = data.gender || 0;
            data.vehicles.push({ carId: 1 });
            data.addresses.push({ addressId: 1 });
            this.customer = data;
            this.curstomer_back = Object.assign({}, data);
            this.createCustomer = !data["userId"];
            if (this.chooseCar.carId) {
              data.vehicles.some(element => {
                if (element.carId == this.chooseCar.carId) {
                  this.chooseCar = element;
                  return true; //break
                }
              });
            } else {
              // 选择默认车辆
              data.vehicles.some(element => {
                if (element.defaultCar) {
                  this.chooseCar = element;
                  return true; //break
                }
              });
            }
            if (this.chooseAddress.addressId) {
              data.addresses.some(element => {
                if (element.addressId == this.chooseAddress.addressId) {
                  this.chooseAddress = element;
                  return true; //break
                }
              });
            } else {
              //选择默认地址
              data.addresses.some(element => {
                if (element.defaultAddress) {
                  this.chooseAddress = element;
                  return true; //break
                }
              });
            }
            if (callback) callback.call();
          },
          error => {}
        )
        .catch(() => {})
        .finally(() => {
          this.loading = false;
          this.selected_userID = 0;
          this.showDetailInfo = true;
        });
    },
    change_car(selVal) {
      this.customer.vehicles
        .filter(p => {
          return p.carId != selVal.carId && p.edit == true;
        })
        .forEach(p => {
          p.edit = false;
        });
      if (selVal.carId == 1) this.$set(selVal, "edit", true);
    },
    change_address(selVal) {
      this.customer.addresses
        .filter(p => {
          return p.addressId != selVal.addressId && p.edit == true;
        })
        .forEach(p => {
          p.edit = false;
        });
      if (selVal.addressId == 1) {
        this.$set(selVal, "edit", true);
        selVal.userName = this.customer.userName;
        selVal.mobileNumber = this.customer.userTel;
      }
    }
  }
};
</script>
<style lang="scss" scoped>
.car-group,
.address-group {
  width: 100%;
  .el-radio {
    width: 100%;
  }
}
</style>
