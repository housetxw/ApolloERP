<template>
  <div class="navbar">
    <hamburger
      :is-active="sidebar.opened"
      class="hamburger-container"
      @toggleClick="toggleSideBar"
    />

    <breadcrumb class="breadcrumb-container" />
    <div>
      <!-- <i class="el-icon-chat-dot-square" style="height:16px;width:16px"></i> -->

      <div class="right-menu">
        <div>
          <span class="s2">
            <SwitchLogin />
            {{ userInfo.organizationName }}--
            {{ userInfo.employeeName }}
          </span>

          <span class="s1" @click="logout">
            <el-link
              icon="el-icon-switch-button"
              :underline="false"
              style="color:red;font-size:16px"
            >退出</el-link>
          </span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import Breadcrumb from "@/components/Breadcrumb";
import Hamburger from "@/components/Hamburger";
import SwitchLogin from "@/components/SwitchLogin";

export default {
  components: {
    Breadcrumb,
    Hamburger,
    SwitchLogin,
  },
  computed: {
    ...mapGetters(["sidebar", "avatar", "userInfo"]),
  },
  methods: {
    toggleSideBar() {
      this.$store.dispatch("app/toggleSideBar");
      this.$set(
        this.$root.$data,
        "toggleSideBar",
        !this.$root.$data.toggleSideBar
      );
    },
    async logout() {
      let _this = this;
      this.$confirm("确定退出系统吗！", "退出登录", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      }).then(() => {
        _this.$store.dispatch("user/logout");
        _this.$router.push(`/login?redirect=${_this.$route.fullPath}`);
        location.reload();
      });
    },
  },
};
</script>
<style lang="scss" >
.navbar {
  height: 60px;
  overflow: hidden;
  position: relative;
  background: #fff;
  box-shadow: 0 1px 4px rgba(0, 21, 41, 0.08);
  padding: 5px 0;
  .el-breadcrumb > span > :first-child {
    display: none;
  }
  .el-breadcrumb > span > :last-child {
    display: block;
  }
  .hamburger-container {
    line-height: 46px;
    height: 100%;
    float: left;
    cursor: pointer;
    transition: background 0.3s;
    -webkit-tap-highlight-color: transparent;

    &:hover {
      background: rgba(0, 0, 0, 0.025);
    }
  }

  .breadcrumb-container {
    float: left;
  }

  .right-menu {
    float: right;
    padding: 0 20px;
    height: 100%;
    line-height: 50px;

    &:focus {
      outline: none;
    }
    >>> .el-button.is-circle {
      padding: 16px;
      background: #67c23a;
      font-size: 12px;
      color: white;
    }
    .right-menu-item {
      display: inline-block;
      padding: 0 5px;
      height: 100%;
      font-size: 18px;
      color: #5a5e66;
      vertical-align: text-bottom;

      &.hover-effect {
        cursor: pointer;
        transition: background 0.3s;

        &:hover {
          background: rgba(0, 0, 0, 0.025);
        }
      }
    }
    .b1 {
      float: left;
      height: 46px;
      width: 46px;
      margin-top: 0;
    }
    .s2 {
      margin: 0 23px;
      float: left;
      cursor: pointer;

      color: rgba(51, 51, 51, 1);
      font: 400 18px/48px "Microsoft YaHei";
    }
    .s1 {
      display: block;

      float: right;
      cursor: pointer;
      font: 400 18px/48px "Microsoft YaHei";
      color: rgba(255, 0, 0, 1);
    }
    .avatar-container {
      margin-right: 30px;

      .avatar-wrapper {
        margin-top: 5px;
        position: relative;

        .user-avatar {
          cursor: pointer;
          width: 40px;
          height: 40px;
          border-radius: 10px;
        }

        .el-icon-caret-bottom {
          cursor: pointer;
          position: absolute;
          right: -20px;
          top: 25px;
          font-size: 12px;
        }
      }
    }
  }
}
</style>