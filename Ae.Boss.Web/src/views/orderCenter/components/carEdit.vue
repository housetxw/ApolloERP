<template >
  <el-form
    :model="carData"
    ref="formCarInfo"
    size="mini"
    :rules="rules"
    v-show="show"
    v-loading="loading"
  >
    <el-form-item>
      <el-col :span="5">
        <el-form-item prop="brand">
          <el-select
            v-model="carData.brand"
            style="width:100%"
            prop="brand"
            @change="getVehicleByBrand(true)"
            filterable
            placeholder="-请选择品牌-"
            size="mini"
          >
            <el-option
              v-for="item in carData.brandList"
              :key="item.brand"
              :label="item.brand"
              :value="item.brand"
            ></el-option>
          </el-select>
        </el-form-item>
      </el-col>
      <el-col :span="5">
        <el-form-item prop="vehicle">
          <el-select
            v-model="carData.vehicle"
            style="width:100%"
            @change="getPaiLiangByVehicleId(true)"
            filterable
            placeholder="-请选择车系-"
            size="mini"
            value-key="vehicleId"
          >
            <el-option
              v-for="item in carData.vehicleList"
              :key="item.vehicleId"
              :label="item.vehicle"
              :value="item"
            ></el-option>
          </el-select>
        </el-form-item>
      </el-col>
      <el-col :span="4">
        <el-form-item prop="paiLiang">
          <el-select
            v-model="carData.paiLiang"
            style="width:100%"
            prop="paiLiang"
            @change="getVehicleNianByPaiLiang(true)"
            filterable
            placeholder="-请选择排量-"
            size="mini"
          >
            <el-option v-for="item in carData.paiLiangList" :key="item" :label="item" :value="item"></el-option>
          </el-select>
        </el-form-item>
      </el-col>
      <el-col :span="3">
        <el-form-item>
          <el-select
            v-model="carData.nian"
            style="width:100%"
            @clear="tid='';salesNameList=[];"
            @change="getVehicleSalesNameByNian(true)"
            clearable
            filterable
            placeholder="-请选择生产年份-"
            size="mini"
          >
            <el-option v-for="item in carData.nianList" :key="item" :label="item" :value="item"></el-option>
          </el-select>
        </el-form-item>
      </el-col>
      <el-col :span="6">
        <el-form-item>
          <el-select
            v-model="carData.tid"
            style="width:100%"
            clearable
            filterable
            placeholder="-请选择车款-"
            size="mini"
            value-key="tid"
          >
            <el-option
              v-for="item in carData.salesNameList"
              :key="item.tid"
              :label="item.salesName"
              :value="item"
            ></el-option>
          </el-select>
        </el-form-item>
      </el-col>
    </el-form-item>
    <el-form-item>
      <el-col :span="10">
        <el-form-item prop="carNumber">
          <el-input
            clearable
            maxlength="10"
            placeholder="车牌号"
            v-model="carData.carNumber"
            style="width:160px"
          ></el-input>
        </el-form-item>
      </el-col>

      <el-col :span="6" style="padding-left:5px">
        <el-checkbox v-model="carData.defaultCar">设为默认车辆</el-checkbox>
      </el-col>
      <el-col :span="8">
        <el-button-group size="mini">
          <el-button
            type="primary"
            icon="el-icon-check"
            @click="saveCar()"
            class="s-mini"
            v-if="carData.userId"
          >保存</el-button>
          <el-button
            type="danger"
            icon="el-icon-delete"
            @click="delCar()"
            class="s-mini"
            v-if="carData.carId"
          >删除</el-button>
        </el-button-group>
      </el-col>
    </el-form-item>
  </el-form>
</template>
<script>
import { appSvc as user } from "@/api/user/user";
import { appSvc as baoyang } from "@/api/baoyang/baoyang";
export default {
  name: "carEdit",
  props: {
    userId: { type: String, default: "" },
    show: { type: Boolean, default: false },
    params: { type: Object, default: {} }
  },
  data() {
    return {
      loading: false,
      carData: {
        carNumber: "",
        brand: "",
        brandList: [],
        vehicle: {},
        vehicleList: [],
        paiLiang: "",
        paiLiangList: [],
        nian: "",
        nianList: [],
        tid: {},
        salesNameList: [],
        defaultCar: false
      },
      rules: {
        carNumber: [
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              if (!this.carData.carNumber) callback(new Error("请输入车牌"));
              else callback();
            }
          }
        ],
        brand: [
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              if (!this.carData.brand) callback(new Error("请选择车辆品牌"));
              else callback();
            }
          }
        ],
        vehicle: [
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              if (!this.carData.vehicle || !this.carData.vehicle.vehicleId)
                callback(new Error("请选择车系"));
              else callback();
            }
          }
        ],
        paiLiang: [
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              if (!this.carData.paiLiang) callback(new Error("请选择排量"));
              else callback();
            }
          }
        ]
      }
    };
  },
  watch: {
    show: function(v, oldv) {
      //   console.log(this.params);
      if (this.params.carId == 1) {
        Object.assign(this.$data.carData, this.$options.data().carData);
      } else {
        Object.assign(this.carData, this.params, {
          carId: this.params.carId,
          vehicle: {
            vehicle: this.params.vehicle,
            vehicleId: this.params.vehicleId
          },
          tid: { tid: this.params.tid, salesName: this.params.salesName }
        });
        // console.log(this.carData)
      }
      if (v == true && this.carData.brandList.length == 0) {
        this.getBrandList();
      }
    }
  },

  methods: {
    getBrandList() {
      baoyang
        .getBrandList()
        .then(
          res => {
            this.carData.brandList = res.data;
            if (this.carData.carId) this.getVehicleByBrand();
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    getVehicleByBrand(changed) {
      if (this.carData.brand == "") {
        return;
      }
      baoyang
        .getListByBrand({ brand: this.carData.brand })
        .then(
          res => {
            var data = res.data;
            this.carData.vehicleList = data;
            if (this.carData.carId) {
              this.getPaiLiangByVehicleId();
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          if (!changed) return;
          this.carData.vehicle = {};
          this.carData.paiLiang = "";
          this.carData.nian = "";
          this.carData.tid = {};
        });
    },
    getPaiLiangByVehicleId(changed) {
      console.log(this.carData.vehicle);
      if (!this.carData.vehicle["vehicleId"]) {
        return;
      }
      baoyang
        .getPaiLiangByVehicleId({
          vehicleId: this.carData.vehicle.vehicleId
        })
        .then(
          res => {
            var data = res.data;
            this.carData.paiLiangList = data;
            if (this.carData.carId) {
              this.getVehicleNianByPaiLiang();
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          if (!changed) return;
          this.carData.paiLiang = "";
          this.carData.nian = "";
          this.carData.tid = {};
        });
    },
    getVehicleNianByPaiLiang(changed) {
      if (!this.carData.vehicle["vehicleId"] || this.carData.paiLiang == "") {
        return;
      }
      baoyang
        .getVehicleNianByPaiLiang({
          vehicleId: this.carData.vehicle.vehicleId,
          paiLiang: this.carData.paiLiang
        })
        .then(
          res => {
            var data = res.data;
            this.carData.nianList = data;
            if (this.carData.carId) this.getVehicleSalesNameByNian();
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          if (!changed) return;
          this.carData.nian = "";
          this.carData.tid = "";
        });
    },
    getVehicleSalesNameByNian(changed) {
      if (
        !this.carData.vehicle["vehicleId"] == {} ||
        this.carData.paiLiang == "" ||
        this.carData.nian == ""
      ) {
        return;
      }
      baoyang
        .getVehicleSalesNameByNian({
          vehicleId: this.carData.vehicle.vehicleId,
          paiLiang: this.carData.paiLiang,
          nian: this.carData.nian
        })
        .then(
          res => {
            var data = res.data;
            this.carData.salesNameList = data;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          if (!changed) return;
          this.carData.tid = "";
        });
    },
    validate() {
      return new Promise((resolve, reject) => {
        this.$refs["formCarInfo"].validate(valid => {
          resolve(valid);
        });
      }).catch(e => {
        console.log(e);
      });
    },
    saveCar(userId, callback) {
      this.validate().then(valid => {
        if (!valid) return;
        this.loading = true;
        let postData = {
          userId: this.userId || userId,
          carId: this.carData.carId || "",
          carNumber: this.carData.carNumber,
          brand: this.carData.brand,
          vehicle: this.carData.vehicle.vehicle,
          vehicleId: this.carData.vehicle.vehicleId,
          paiLiang: this.carData.paiLiang,
          nian: this.carData.nian,
          tid: this.carData.tid["tid"],
          salesName: this.carData.tid["salesName"],
          defaultCar: this.carData.defaultCar
        };
        (!postData.carId
          ? user.addUserCar(postData)
          : user.editUserCar(postData)
        )
          .then(
            res => {
              if (res.data) {
                let newCar =
                  postData.carId == ""
                    ? Object.assign({ carId: res.data }, postData)
                    : postData;
                this.$emit("changed", newCar);
                if (callback) callback.call(res);
              }
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => reject)
          .finally(() => {
            this.loading = false;
          });
      });
    },
    delCar() {
      this.$confirm("确定要删除此车辆？", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      }).then(() => {
        user
          .deleteUserCar({ userId: this.userId, carId: this.carData.carId })
          .then(
            res => {
              if (res.data) {
                this.$emit("changed", null);
              }
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => reject)
          .finally(() => {
            this.loading = false;
          });
      });
    }
  }
};
</script>