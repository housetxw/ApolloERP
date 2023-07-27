<template>
  <main class="container account-authority">
    <el-row id="createOrUpdate" class="margin-top-30">
      <el-col :span="8">
        <el-form :model="formModel" :rules="rules" ref="formModel">
          <el-form-item label="账号" :label-width="formLabelWidth" prop="name">
            <el-input
              v-model="formModel.name"
              autocomplete="off"
              autofocus="true"
              placeholder="请输入登录账号(手机号)"
              clearable
            ></el-input>
          </el-form-item>
          <el-form-item label="旧密码" :label-width="formLabelWidth" prop="password">
            <el-input
              v-model="formModel.password"
              autocomplete="off"
              placeholder="请输入旧密码"
              clearable
              show-password
            ></el-input>
          </el-form-item>
          <el-form-item label="新密码" :label-width="formLabelWidth" prop="newPassword">
            <el-input
              v-model="formModel.newPassword"
              autocomplete="off"
              placeholder="请输入新密码"
              clearable
              show-password
            ></el-input>
          </el-form-item>
          <el-form-item style="float:right">
            <el-button icon="el-icon-close" @click="resetForm('formModel')">重置</el-button>
            <el-button
              icon="el-icon-check"
              type="primary"
              :loading="btnSaveLoading"
              @click="save('formModel')"
            >保存</el-button>
          </el-form-item>
        </el-form>
      </el-col>
      <el-col :span="8"></el-col>
      <el-col :span="8"></el-col>
    </el-row>
  </main>
</template>

<script>
import { validateURL } from "@/utils/validate";
import { pcSvc } from "@/api/personalcenter/personalcenter";

export default {
  name: "chanagepassword",
  data() {
    return {
      formVisible: true,
      btnSaveLoading: false,
      formModel: {
        name: "",
        password: "",
        newPassword: ""
      },
      rules: {
        name: [
          { required: true, message: "请输入登录账号", trigger: "blur" },
          { max: 11, message: "登录账号长度不允许超过11", trigger: "blur" }
        ],
        password: [
          { required: true, message: "请输入旧密码", trigger: "blur" }
        ],
        newPassword: [
          { required: true, message: "请输入新密码", trigger: "blur" },
          { min: 8, message: "新密码长度不允许低于8位", trigger: "blur" }
        ]
      },
      formLabelWidth: "120px"
    };
  },
  computed: {},
  created() {
    this.fetchData();
  },
  methods: {
    fetchData() {},
    save(formName) {
      var vm = this;
      console.log("formModel: " + JSON.stringify(this.formModel));
      this.$refs[formName].validate(valid => {
        if (valid) {
          vm.btnSaveLoading = true;

          pcSvc
            .updatePassword(this.formModel)
            .then(res => {
              debugger;
              if (res.data) {
                this.$message({
                  message: res.message,
                  type: "success"
                });
                vm.resetForm("formModel");
              }
            })
            .catch(error => {
              console.log(error);
            })
            .finally(() => {
              vm.btnSaveLoading = false;
            });
        } else {
          return false;
        }
      });
    },
    resetForm(formName) {
      //   this.formModel = JSON.parse(JSON.stringify(this.formInit));
      this.$refs[formName].resetFields();
    }
  }
};
</script>