<template>
  <div class="mainlogin">
    <div class="container">
      <div class="content">
        <header>
          <img class="left" src="@/assets/login_images/newLog.png" />
          <div class="box">
            <div class="title"></div>
            <div class="title2"> </div>
          </div>
        </header>
        <!-- <div class="box2" >
       <img  class=" bg" src="@/assets/login_images/car.png">
        </div>-->
        <div
          class="box2"
          :style="{backgroundImage:'url('+require('@/assets/login_images/login.png')+')',backgroundSize: 'cover'}"
        >
          <div class="login-container">
            <el-form
              ref="loginForm"
              :model="loginForm"
              :rules="loginRules"
              class="login-form"
              auto-complete="on"
              label-position="left"
            >
              <div class="title-container">
                <h3 class="title">总部后台管理系统</h3>
              </div>

              <el-form-item prop="username">
                <span class="svg-container">
                  <svg-icon icon-class="user" />
                </span>
                <el-input
                  ref="username"
                  v-model="loginForm.username"
                  placeholder="手机号"
                  name="username"
                  type="text"
                  tabindex="1"
                  auto-complete="on"
                  maxlength="11"
                />
              </el-form-item>

              <el-form-item prop="password" v-if="isShowPassword">
                <span class="svg-container">
                  <svg-icon icon-class="password" />
                </span>
                <el-input
                  :key="passwordType"
                  ref="password"
                  v-model="loginForm.password"
                  :type="passwordType"
                  placeholder="密码"
                  name="password"
                  tabindex="2"
                  auto-complete="on"
                  @keyup.enter.native="handleLogin"
                />
                <span class="show-pwd" @click="showPwd">
                  <svg-icon :icon-class="passwordType === 'password' ? 'eye' : 'eye-open'" />
                </span>
              </el-form-item>
              <el-form-item prop="authCode" v-if="!isShowPassword">
                <span class="svg-container">
                  <svg-icon icon-class="password" />
                </span>
                <el-input
                  ref="authCode"
                  v-model="loginForm.password"
                  placeholder="验证码"
                  name="password"
                  tabindex="2"
                  auto-complete="on"
                  @keyup.enter.native="handleLogin"
                />
                <span class="show-pwd">
                  <el-button
                    type="primary"
                    size="small"
                    :disabled="isLock"
                    @click="getAuthCode"
                    style="border-radius:10px;"
                  >{{yzmtext}}</el-button>
                </span>
              </el-form-item>
              <div style="text-align:right;color:white;margin-bottom:15px;">
                <span
                  style="cursor:pointer; color:#999; font-size:12px;"
                  @click="changeLoginWay"
                >{{LoginWayText}}</span>
              </div>

              <el-button
                :loading="loading"
                type="primary"
                style="width:100%;margin-bottom:30px;"
                @click.native.prevent="handleLogin"
              >登录</el-button>

              <!-- <div class="tips">
        <span style="margin-right:20px;">username: admin</span>
        <span>password: any</span>
              </div>-->
            </el-form>
            <el-dialog title="请选择门店" :visible.sync="dialogFormVisible">
              <div
                v-for="(item,index) in shopList"
                :key="index"
                class="shopItem"
                @click="selectShop(item)"
              >{{item.organizationName}}</div>
            </el-dialog>
          </div>
        </div>
      </div>
    </div>
    <!-- <footer>
      <div class="title4"> </div>
    </footer> -->
  </div>
</template>

<script>
import { MessageBox } from 'element-ui'
import { validUsername } from '@/utils/validate'
import { GetSMSCodeAsync } from '@/api/user/user'
export default {
  name: 'Login',
  data() {
    const validateUsername = (rule, value, callback) => {
      if (!validUsername(value)) {
        callback(new Error('请输入正确的手机号'))
      } else {
        callback()
      }
    }
    const validatePassword = (rule, value, callback) => {
      if (value.length < 4) {
        callback(new Error('密码不正确'))
      } else {
        callback()
      }
    }
    return {
      isShowPassword: true,
      LoginWayText: '短信验证码登录',
      yzmtext: '发送验证码',
      isLock: false,
      dialogFormVisible: false,
      loginForm: {
        username: '',
        password: ''
      },
      loginRules: {
        username: [
          { required: true, trigger: 'blur', validator: validateUsername }
        ],
        password: [
          { required: true, trigger: 'blur', validator: validatePassword }
        ]
      },
      shopList: [],
      loading: false,
      passwordType: 'password',
      redirect: undefined,
      organizationInfo : {
        employeeId:'',
        organizationId:1,
        employeeName:''
      }
    }
  },
  created() {
    // console.log(cripto.encrypt('string'))
    // console.log(cripto.encrypt1('string'))
  },
  watch: {
    $route: {
      handler: function(route) {
        this.redirect = route.query && route.query.redirect
      },
      immediate: true
    }
  },
  methods: {
    changeLoginWay() {
      if (this.LoginWayText === '账号密码登录') {
        this.LoginWayText = '短信验证码登录'
        this.isShowPassword = true
      } else {
        this.LoginWayText = '账号密码登录'
        this.isShowPassword = false
      }
    },
    getAuthCode() {
      // console.log(6666, )
      if (!this.isLock) {
        if (!/^1[3456789]\d{9}$/.test(this.loginForm.username)) {
          MessageBox.alert('手机号有误' || 'Error', '警告', {
            confirmButtonText: '关闭',
            type: 'error'
          })
          return false
        } else {
          this.isLock = true
          GetSMSCodeAsync({
            name: this.loginForm.username,
            smsType: 'Login'
          })
          let that = this
          let i = 60
          let timer = setInterval(function() {
            i--
            that.yzmtext = '已发送(' + i + 's)'
            if (i === 0) {
              clearInterval(timer)
              that.yzmtext = '发送验证码'
              that.isLock = false
            }
          }, 1000)
        }
      }
    },
    selectShop(item) {
      this.loading = true
      this.$store
        .dispatch('user/setTokenInfoByAuthCode', item)
        .then(() => {
          console.log(3355)
          this.$router.push({ path: this.redirect || '/' })
          this.loading = false
        })
        .catch(() => {
          this.loading = false
        })
    },
    showPwd() {
      if (this.passwordType === 'password') {
        this.passwordType = ''
      } else {
        this.passwordType = 'password'
      }
      this.$nextTick(() => {
        this.$refs.password.focus()
      })
    },

    handleLogin() {
      if (this.LoginWayText === '短信验证码登录') {
        this.$refs.loginForm.validate(valid => {
          if (valid) {
            this.loading = true
            this.$store
              .dispatch('user/login', this.loginForm)
              .then(data => {
                if (data) {
                  console.log(1122, data)
                  for(var i=0;i<data.items.length;i++){ 
                      if(data.items[i].organizationId === 1){
                        this.organizationInfo = data.items[i];
                      }
                  }
                  this.selectShop(this.organizationInfo);
                  // this.shopList = data.items
                  // this.dialogFormVisible = true
                  // this.loading = false
                } else {
                  this.$router.push({ path: this.redirect || '/' })
                  this.loading = false
                }
              })
              .catch(() => {
                this.loading = false
              })
          } else {
            console.log('error submit!!')
            return false
          }
        })
      } else {
        this.$refs.loginForm.validate(valid => {
          if (valid) {
            console.log(555)
            this.loading = true
            this.$store
              .dispatch('user/loginWithSMS', this.loginForm)
              .then(data => {
                if (data) {
                  console.log(1122, data)
                  for(var i=0;i<data.items.length;i++){ 
                      if(data.items[i].organizationId === 1){
                        this.organizationInfo = data.items[i];
                      }
                  }
                  this.selectShop(this.organizationInfo);
                  // this.shopList = data.items
                  // this.dialogFormVisible = true
                  // this.loading = false
                } else {
                  this.$router.push({ path: this.redirect || '/' })
                  this.loading = false
                }
              })
              .catch(() => {
                this.loading = false
              })
          } else {
            console.log('error submit!!')
            console.log(666)
            return false
          }
        })
      }
    }
  }
}
</script>

<style lang="scss">
/* 修复input 背景不协调 和光标变色 */
/* Detail see https://github.com/PanJiaChen/vue-element-admin/pull/927 */

$bg: #283443;
$light_gray: #fff;
$cursor: #fff;

@supports (-webkit-mask: none) and (not (cater-color: $cursor)) {
  .login-container .el-input input {
    color: $cursor;
  }
}

/* reset element-ui css */
.login-container {
  .el-input {
    display: inline-block;
    height: 47px;
    width: 85%;

    input {
      background: transparent;
      border: 0px;
      -webkit-appearance: none;
      border-radius: 0 5px 5px 0px;
      // padding: 8px 5px 10px 15px;
      color: $light_gray;
      height: 47px;
      right: -3px;
      caret-color: $bg;

      &:-webkit-autofill {
        box-shadow: 0 0 0px 1000px $bg inset !important;
        -webkit-text-fill-color: $cursor !important;
      }
    }
  }

  .el-form-item {
    // border: 1px solid rgba(255, 255, 255, 0.1);
    // background: rgba(0, 0, 0, 0.1);
    border-radius: 5px;
    color: #454545;
  }
  .shopItem {
    height: 56px;
    line-height: 56px;
    border-bottom: 1px solid #ccc;
    font-size: 20px;
    cursor: pointer;
  }
}
</style>

<style lang="scss" scoped>
$cl: #307ddb;
$bg: #2d3a4b;
$dark_gray: #889aa4;
$light_gray: #eee;
// #app {
//     height: 100%;
//     background: #304156;
// }
.mainlogin {
  background: #304156;
  height: 100%;
  width: 100%;
}

.container {
  background: #fff;
  // margin: 0;
  // padding: 65px 0 0 81px;
  height: 100%;
  width: 100%;

  display: flex;
  justify-content: center;
  align-items: center;
}
.content {
  width: 1600px;
  height: 800px;
  // width: 1200px;
  // height: 100%;
  // width: 100%;
}
.left {
  float: left;
}
.box {
  float: left;
  margin-left: 28px;
  .title {
    width: 282px;

    color: #ea5921;

    margin: 0;
    font: 400 55px/57px 'STXingkai';
  }
  .title2 {
    height: 10px;

    color: #ea5921;
    margin: 0;

    font: 400 16px/23px 'Eras Bold ITC';
  }
}
.box2 {
  // padding-top: 50px;
  // width: 1200px;
  display: flex;
  justify-content: right;
  align-items: top;
  width: 100%;
  height: 100%;
  // background: no-repeat center cover;
}
.login-container {
  width: 316px;
  height: 361px;
  background-color: #fff;
  overflow: hidden;
  position: relative;
  top: 200px;
  right: 200px;
  .login-form {
    position: relative;
    width: 320px;
    max-width: 100%;
    padding: 40px 35px 0;
    margin: 0 auto;
    overflow: hidden;
  }

  .tips {
    font-size: 14px;
    color: #fff;
    margin-bottom: 16px;

    span {
      &:first-of-type {
        margin-right: 16px;
      }
    }
  }

  .svg-container {
    padding: 6px 5px 6px 15px;
    color: $dark_gray;
    vertical-align: middle;
    width: 30px;
    display: inline-block;
  }

  .title-container {
    position: relative;

    .title {
      font-size: 26px;
      color: $cl;
      margin: 0px auto 30px auto;
      text-align: center;
      font-weight: bold;
    }
  }

  .show-pwd {
    position: absolute;
    right: 10px;
    top: 16px;
    font-size: 16px;
    color: $dark_gray;
    cursor: pointer;
    user-select: none;
  }
}
footer {
  width: 100%;
  height: 310px;
  background: $cl;

  z-index: 0;
  padding-top: 140px;
  // padding-left:560px;
  text-align: center;
  box-sizing: border-box;
}
.login-container .el-form-item {
  // border: 1px solid rgba(255,255,255,0.1);
  // background: rgba(0,0,0,0.1);
  // border-radius: 5px;
  // color: #454545;
  // width:368px;
  height: 46px;
  background: rgba(244, 244, 244, 1);
  border: 1px solid rgba(244, 244, 244, 1);
  border-radius: 5px;
}
>>>.login-container .el-input input {
  color: #333;
}
.el-button--primary {
  color: #fff;
  background-color: $cl;
  border-color: $cl;
}
.el-button--small {
  padding: 6px 15px;
  font-size: 12px;
  border-radius: 3px;
}
.title4 {
  // position: absolute;

  height: 23px;
  font-size: 20px;
  font-family: ZhenyanGB;
  font-weight: 400;
  color: #ea5921;
  line-height: 270px;
  letter-spacing: 14px;

}
</style>
