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
          <h1 class="b1">
            <img src="@/assets/shouye_images/touxiang.png" style="width: 36px; height: 36px;" />
          </h1>
          <span class="s2">{{ userInfo.employeeName }}</span>

          <span class="s1" @click="logout">退出</span>
        </div>
        <!--       
      <el-dropdown class="avatar-container" trigger="click">
        
        <div class="avatar-wrapper">
          <img :src="avatar+'?imageView2/1/w/80/h/80'" class="user-avatar">
          <i class="el-icon-caret-bottom" />
        </div>
        <el-dropdown-menu slot="dropdown" class="user-dropdown">
          <router-link to="/">
            <el-dropdown-item>
              首页
            </el-dropdown-item>
          </router-link>
          <el-dropdown-item divided>
            <span style="display:block;" @click="logout">退出</span>
          </el-dropdown-item>
        </el-dropdown-menu>
        </el-dropdown>-->
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import Breadcrumb from "@/components/Breadcrumb";
import Hamburger from "@/components/Hamburger";

export default {
  components: {
    Breadcrumb,
    Hamburger,
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
      await this.$store.dispatch("user/logout");
      this.$store.commit("permission/RESET_ROUTES");
      this.$router.push(`/login?redirect=${this.$route.fullPath}`);
      location.reload();
    },
  },
};
</script>

<style lang="scss" scoped>
.navbar {
  height: 60px;
  overflow: hidden;
  position: relative;
  background: #fff;
  box-shadow: 0 1px 4px rgba(0, 21, 41, 0.08);
  padding: 5px 0;
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
      background: #304156;
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
      margin-top: 5px;
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
