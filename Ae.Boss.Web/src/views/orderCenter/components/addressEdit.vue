<template >
  <el-form
    :model="addresses"
    ref="formAddressInfo"
    size="mini"
    :rules="rules"
    v-show="show"
    v-loading="loading"
  >
    <el-form-item>
      <el-col style="width:160px">
        <el-form-item prop="userName">
          <el-input v-model="addresses.userName" maxlength="20" placeholder="请输入联系人名" clearable></el-input>
        </el-form-item>
      </el-col>
      <el-col style="width:160px">
        <el-form-item prop="mobileNumber">
          <el-input
            v-model="addresses.mobileNumber"
            maxlength="20"
            placeholder="请输入联系人电话"
            clearable
          ></el-input>
        </el-form-item>
      </el-col>
      <el-col style="width:150px; padding-left:10px" v-if="!addresses.orderNo">
        <el-checkbox v-model="addresses.defaultAddress">设为默认地址</el-checkbox>
      </el-col>
    </el-form-item>

    <el-form-item>
      <el-col style="width:160px">
        <el-form-item prop="province">
          <el-select
            v-model="addresses.province"
            value-key="regionId"
            @change="getCity(true)"
            class="no-radius-right"
          >
            <el-option
              v-for="item in addresses.provinceSel"
              :key="item.regionId"
              :label="item.name"
              :value="item"
            ></el-option>
          </el-select>
        </el-form-item>
      </el-col>
      <el-col style="width:160px">
        <el-form-item prop="city">
          <el-select
            v-model="addresses.city"
            value-key="regionId"
            @change="getDistrict(true)"
            class="no-radius-right no-radius-left"
          >
            <el-option
              v-for="item in addresses.citySel"
              :key="item.regionId"
              :label="item.name"
              :value="item"
            ></el-option>
          </el-select>
        </el-form-item>
      </el-col>
      <el-col style="width:160px">
        <el-form-item prop="district">
          <el-select
            v-model="addresses.district"
            value-key="regionId"
            class="no-radius-left"
            prop="districtId"
          >
            <el-option
              v-for="item in addresses.districtSel"
              :key="item.regionId"
              :label="item.name"
              :value="item"
            ></el-option>
          </el-select>
        </el-form-item>
      </el-col>
    </el-form-item>
    <el-form-item prop>
      <el-col :span="16">
        <el-form-item prop="addressLine">
          <el-input
            v-model="addresses.addressLine"
            placeholder="街道地址"
            clearable
            style="width:480px"
          ></el-input>
        </el-form-item>
      </el-col>
      <el-col :span="8">
        <el-button-group size="mini">
          <el-button
            type="primary"
            icon="el-icon-check"
            @click="saveAddress()"
            class="s-mini"
            v-if="addresses.addressId || addresses.orderNo"
          >保存</el-button>
          <el-button
            type="danger"
            icon="el-icon-delete"
            @click="delAddress()"
            class="s-mini"
            v-if="addresses.addressId && !addresses.orderNo"
          >删除</el-button>
        </el-button-group>
      </el-col>
    </el-form-item>
  </el-form>
</template>
<script>
import { appSvc as user } from "@/api/user/user";
import { appSvc as baoyang } from "@/api/baoyang/baoyang";
import { shopManageSvc as shop } from "@/api/shopmanage/shopmanage";
import { orderCenterSvc as orderAPI } from "@/api/orderCenter/orderCenter";

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
      addresses: {
        userName: "",
        mobileNumber: "",
        addressLine: "",
        province: {},
        city: {},
        district: {},
        provinceId: {},
        provinceSel: [],
        cityId: {},
        citySel: [],
        districtId: {},
        districtSel: [],
        defaultAddress: false
      },
      rules: {
        province: [
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              if (
                !this.addresses.province ||
                !this.addresses.province["regionId"]
              )
                callback(new Error("请选择省"));
              else callback();
            }
          }
        ],
        city: [
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              if (!this.addresses.city || !this.addresses.city["regionId"])
                callback(new Error("请选择市"));
              else callback();
            }
          }
        ],
        district: [
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              if (
                !this.addresses.district ||
                !this.addresses.district["regionId"]
              )
                callback(new Error("请选择区"));
              else callback();
            }
          }
        ],
        addressLine: [
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              if (!this.addresses.addressLine)
                callback(new Error("请输入街道地址"));
              else callback();
            }
          }
        ],
        userName: [
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              if (!this.addresses.userName)
                callback(new Error("请输入联系人名"));
              else callback();
            }
          }
        ],
        mobileNumber: [
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              if (!this.addresses.mobileNumber)
                callback(new Error("请输入联系人电话"));
              else callback();
            }
          }
        ]
      }
    };
  },
  watch: {
    show: function(v, oldv) {
      console.log(this.params);
      if (this.params.addressId == 1) {
        Object.assign(this.$data.addresses, this.$options.data().addresses);
        this.addresses.addressId = "";
        this.addresses.userName = this.params.userName;
        this.addresses.mobileNumber = this.params.mobileNumber;
      } else {
        Object.assign(this.addresses, this.params, {
          addressId: this.params.addressId,
          defaultAddress: this.params.defaultAddress == 1,
          province: {
            regionId: this.params.provinceId,
            name: this.params.province
          },
          city: {
            regionId: this.params.cityId,
            name: this.params.city
          },
          district: {
            regionId: this.params.districtId,
            name: this.params.district
          },
          addressLine: this.params.addressLine.replace(
            this.params.province +
              " " +
              this.params.city +
              " " +
              this.params.district,
            ""
          )
        });
        if (this.addresses.addressLine) {
          var reg = /.+?(省|市|自治区|自治州|县|区)/g;
          // let ps = this.addresses.addressLine.match(reg);
          // if (ps.length == 3) {
          //   this.addresses.province = { regionId: "", name: ps[0].trim() };
          //   this.addresses.city = { regionId: "", name: ps[1].trim() };
          //   this.addresses.district = { regionId: "", name: ps[2].trim() };
          // } else if (ps.length == 2) {
          //   this.addresses.province = { regionId: "", name: ps[0].trim() };
          //   this.addresses.city = { regionId: "", name: ps[0].trim() };
          //   this.addresses.district = { regionId: "", name: ps[1].trim() };
          // }
          this.addresses.addressLine = this.addresses.addressLine
            .replace(reg, "")
            .trim();
        }
      }
      if (v == true && this.addresses.provinceSel.length == 0) {
        this.getProvince();
      } else {
        this.setDefaultRegion();
      }
    }
  },
  methods: {
    setDefaultRegion() {
      if (
        this.addresses.provinceSel.length > 0 &&
        this.addresses.province.regionId &&
        !this.addresses.province.name
      ) {
        this.addresses.provinceSel.forEach(p => {
          if (p.regionId == this.addresses.province.regionId) {
            this.addresses.province.name = p.name;
            return false;
          }
        });
      }

      if (
        this.addresses.citySel.length > 0 &&
        this.addresses.city.regionId &&
        !this.addresses.city.name
      ) {
        this.addresses.citySel.forEach(p => {
          if (p.regionId == this.addresses.city.regionId) {
            this.addresses.city.name = p.name;
            return false;
          }
        });
      }

      if (
        this.addresses.districtSel.length > 0 &&
        this.addresses.district.regionId &&
        !this.addresses.district.name
      ) {
        this.addresses.districtSel.forEach(p => {
          if (p.regionId == this.addresses.district.regionId) {
            this.addresses.district.name = p.name;
            return false;
          }
        });
      }
    },
    //获取省
    getProvince() {
      shop.getRegionChinaListByRegionId({ regionId: 0 }).then(
        res => {
          this.addresses.provinceSel = res.data;
          if (this.addresses.addressId || this.addresses.province.regionId)
            this.getCity();
          this.setDefaultRegion();
        },
        error => {
          console.log(error);
        }
      );
    },
    getCity(changed) {
      if (this.addresses.province["regionId"])
        shop
          .getRegionChinaListByRegionId({
            regionId: this.addresses.province.regionId
          })
          .then(
            res => {
              this.addresses.citySel = res.data;
              if (this.addresses.addressId || this.addresses.city.regionId)
                this.getDistrict();
              this.setDefaultRegion();
            },
            error => {
              console.log(error);
            }
          )
          .finally(() => {
            if (!changed) return;
            this.addresses.city = {};
            this.addresses.district = {};
            this.addresses.districtSel = [];
          });
    },
    getDistrict(changed) {
      if (this.addresses.city["regionId"])
        shop
          .getRegionChinaListByRegionId({
            regionId: this.addresses.city.regionId
          })
          .then(
            res => {
              this.addresses.districtSel = res.data;
              this.setDefaultRegion();
            },
            error => {
              console.log(error);
            }
          )
          .finally(() => {
            if (!changed) return;
            this.addresses.district = {};
          });
    },
    validate() {
      return new Promise((resolve, reject) => {
        this.$refs["formAddressInfo"].validate(valid => {
          resolve(valid);
        });
      }).catch(e => {});
    },
    saveAddress(userId, callback) {
      this.validate().then(valid => {
        if (!valid) return;
        let postData = {
          addressId: this.addresses.addressId,
          defaultAddress: this.addresses.defaultAddress,
          userId: this.userId || userId,
          userName: this.addresses.userName,
          mobileNumber: this.addresses.mobileNumber,
          province: this.addresses.province.name,
          city: this.addresses.city.name,
          district: this.addresses.district.name,
          provinceId: this.addresses.province.regionId,
          cityId: this.addresses.city.regionId,
          districtId: this.addresses.district.regionId,
          addressLine:
            this.addresses.province.name +
            (this.addresses.city.name == this.addresses.province.name
              ? ""
              : this.addresses.city.name) +
            this.addresses.district.name +
            this.addresses.addressLine
        };
        if (this.addresses.orderNo) {
          postData.orderNo = this.addresses.orderNo;
          postData.receiverName = postData.userName;
          postData.receiverPhone = postData.mobileNumber;
          postData.detailAddress = this.addresses.addressLine;
        }
        (this.addresses.orderNo
          ? orderAPI.UpdateOrderAddress(postData)
          : !postData.addressId
          ? user.addUserAddress(postData)
          : user.editUserAddress(postData)
        ).then(
          res => {
            if (this.addresses.orderNo) {
              if (res.code == 10000) {
                this.$emit("changed", postData);
              } else {
                this.$message.error("地址保存失败！");
              }
            } else if (res.data == false) {
              this.$message.error("地址保存失败！");
            } else {
              let newAddress = postData;
              if (postData.addressId == "")
                newAddress.addressId = res.data || "";
              this.$emit("changed", newAddress);
              if (callback != undefined) callback.call(res);
            }
          },
          error => {
            console.log(error);
          }
        );
      });
    },
    delAddress() {
      this.$confirm("确定要删除此联系人信息？", "信息", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      }).then(() => {
        user
          .deleteUserAddress({
            userId: this.userId,
            addressId: this.addresses.addressId
          })
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