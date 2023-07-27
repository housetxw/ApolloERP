<template>
  <el-popover
    placement="bottom-end"
    trigger="click"
    popper-class="switch-popover"
    v-model="visible"
  >
    <div style="height:40px">
      <span>切换登录</span>
      <el-divider direction="vertical"></el-divider>
      <i class="el-icon-user-solid"></i>
      <span>{{userInfo.employeeName}}</span>
    </div>
    <div class="seach-right">
      <el-input placeholder="请输入门店或公司名称" v-model="input3">
        <template slot="prepend">搜索</template>
        <el-button slot="append" icon="el-icon-search" @click="getFirstPage">查询</el-button>
        <el-button slot="append" @click="cancel" icon="el-icon-close" style="margin-left:10px">取消</el-button>
      </el-input>
    </div>
    <el-tabs v-model="activeName" @tab-click="handleClick">
      <el-tab-pane label="可切换的门店" name="Shop">
        <div
          v-for="(item) in shops.data"
          :key="item.id"
          class="cell-block"
          @click="changeShop(item)"
        >
          <p>{{item.organizationSimpleName}}</p>
          <p>{{item.organizationName}}</p>
        </div>
        <section class="pagination">
          <el-pagination
            background
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            :page-size="shops.pageSize"
            :page-sizes="[10, 20, 50, 100, 500]"
            layout="total, sizes, prev, pager, next"
            :total="shops.total"
            :current-page.sync="shops.currentPage"
          ></el-pagination>
        </section>
      </el-tab-pane>
      <el-tab-pane label="可切换的公司" name="Company">
        <div
          v-for="(item) in shops.data"
          :key="item.id"
          class="cell-block"
          @click="changeShop(item)"
        >
          <p>{{item.organizationSimpleName}}</p>
          <p>{{item.organizationName}}</p>
        </div>
        <section class="pagination">
          <el-pagination
            background
            @size-change="handleSizeChange"
            @current-change="handleCurrentChange"
            :page-size="shops.pageSize"
            :page-sizes="[10, 20, 50, 100, 500]"
            layout="total, sizes, prev, pager, next"
            :total="shops.total"
            :current-page.sync="shops.currentPage"
          ></el-pagination>
        </section>
      </el-tab-pane>
    </el-tabs>
    <el-link slot="reference" type="primary" :underline="false" @click="getFirstPage">切换登录</el-link>
  </el-popover>
</template>
<script>
import { mapGetters } from 'vuex'
import { getToken, setToken, setUserInfo, removeToken } from '@/utils/auth'
import { appSvc } from '@/api/user'
export default {
  props: {
    // userInfo: { type: Object, required: true }
  },
  computed: {
    ...mapGetters(['userInfo'])
  },
  methods: {
    cancel() {
      this.visible = false
      this.input3 = ''
    },
    handleSizeChange(val) {
      this.shops.pageSize = val
    },
    handleClick(tab, event) {
      this.shops.currentPage = 1
      this.getCompany(1)
    },
    getFirstPage() {
      this.shops.currentPage = 1
      this.getCompany(1)
    },
    handleCurrentChange(val) {
      this.getCompany(val)
    },
    changeShop(item) {
      let data = {
        token: JSON.parse(getToken()).accessToken,
        employeeId: item.employeeId,
        employeeName: item.employeeName,
        organizationId: item.organizationId,
        employeeType: item.employeeType
      }
      appSvc
        .GetTokenInfoByToken(data)
        .then(
          res => {
            this.$store.commit('user/SET_TOKEN', res.data)

            this.$store.commit('user/SET_USER', item)

            this.$store.commit(
              'user/SET_AVATAR',
              'https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif'
            )
            setToken(res.data)
            setUserInfo(item)
            this.$router.push('/')
            location.reload()
            this.visible = false
          },
          error => {
            console.log(error)
          }
        )
        .catch(() => {})
        .finally(() => {
          // console.log("tableLoading: false")
        })
    },
    getCompany(pageIndex) {
      let data = {
        token: JSON.parse(getToken()).accessToken,
        employeeId: this.userInfo.employeeId,
        // employeeType: this.userInfo.employeeType,
        employeeType: this.activeName,
        platform: 'Shop',
        organizationName: this.input3,
        pageIndex,
        pageSize: this.shops.pageSize
      }
      appSvc
        .GetOrgPageListByTokenAsync(data)
        .then(
          res => {
            this.shops.data = res.data.items
            this.shops.total = res.data.totalItems
          },
          error => {
            console.log(error)
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false
        })
    }
  },
  data() {
    return {
      visible: false,
      activeName: 'Shop',
      input3: '',
      shops: {
        data: [],
        total: 0,
        pageSize: 20,
        currentPage: 1
      }
    }
  }
}
</script>
<style lang="scss">
.switch-popover {
  width: calc(100% - 10px);
  top: 35px !important;
  height: calc(100% - 50px);
  .seach-right {
    display: block;
    position: absolute;
    top: 30px;
    right: 10px;
    width: 500px;
    z-index: 2;
  }
  @media screen and (max-width: 700px) {
    .seach-right {
      visibility: hidden;
    }
  }
  .el-tabs {
    height: calc(100% - 50px) !important;
    overflow: hidden !important;
    .el-tabs__content {
      height: calc(100% - 80px) !important;
      overflow-y: auto;
    }
    .pagination {
      position: fixed;
      bottom: 0;
      right: 30px;
      max-height: 40px;
    }
    .cell-block {
      width: calc(25% - 20px);
      padding: 5px 10px;
      margin: 0 10px 10px;
      border: 1px solid #e9e9e9;
      font-size: 14px;
      color: #999;
      cursor: pointer;
      float: left;
      p:last-child {
        text-align: right;
      }
    }
    .cell-block:hover {
      background-color: lightseagreen;
      color: white;
    }
    @media screen and (max-width: 1100px) {
      .cell-block {
        width: calc(33% - 20px);
      }
    }

    @media screen and (max-width: 800px) {
      .cell-block {
        width: calc(50% - 20px);
      }
    }

    @media screen and (max-width: 500px) {
      .cell-block {
        width: 280px;
      }
      .el-tabs__content {
        overflow-x: auto;
      }
    }
  }
}
</style>