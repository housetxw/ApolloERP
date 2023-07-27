<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <header class="bodyTop">
        <article class="filter-container">
          <span class="input-label">门店简称:</span>
          <el-input
            v-model="condition.simpleName"
            style="width:180px;"
            clearable
            placeholder="请输入门店简称"
          />
          <span class="input-label">联系电话:</span>
          <el-input
            v-model="condition.ownerPhone"
            style="width:180px;"
            clearable
            placeholder="请输入联系电话"
          />

          <el-button
            type="primary"
            size="small"
            style="margin-left:30px;"
            icon="el-icon-search"
            @click="search(true)"
            >搜索</el-button>
        </article>
      </header>
      <main>
            <section>
                  <el-table v-loading="tableLoading" :data="tableData" border style="width: 100%">
                    <el-table-column label="编号" prop="id" width="80"></el-table-column>
                    <el-table-column label="门店简称" prop="simpleName" width="180"></el-table-column>
                    <el-table-column label="门店名称" prop="fullName" width="180"></el-table-column>
                    <el-table-column label="公司名称" prop="shopCompanyName" width="180"></el-table-column>
                    <el-table-column label="地址" prop="address" width="180"></el-table-column>
                    <el-table-column label="门店联系人" prop="contact" width="180"></el-table-column>
                    <el-table-column label="门店电话" prop="telephone" width="180"></el-table-column>
                    <el-table-column label="门店类型" prop="type" width="180"></el-table-column>
                    <el-table-column label="操作">
                        <template slot-scope="scope">
                          <router-link :to="{name:'shopservice',params:{id:scope.row.id}}">
                            <el-button size="mini" type="primary">操作</el-button>
                          </router-link>
                        </template>
                    </el-table-column>
                  </el-table>
            </section>
        <section class="pagination">
          <el-pagination
            background
            :page-size="10"
            :page-sizes="[10, 30, 50, 100]"
            layout="total, sizes, prev, pager, next, jumper"
            :total="totalList"
            @current-change="pageTurning"
            :current-page.sync="currentPage"
            @size-change="sizeChange"
          ></el-pagination>
        </section>
      </main>
    </section>
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { shopManageSvc } from "@/api/shopmanage/shopmanage";

export default {
    data() {
        return{

            readonly: true,
            tableLoading: false,
            currentPage: 1,
            flag: this.global.deletedFlag,
            tableData: [],
            totalList: 0,
      
            //table页面的条件
            condition: {
            simpleName: undefined,
            status: 0,
            ownerPhone: undefined,
            ShopType: 1,
            pageIndex: 1,
            pageSize: 10,
            ServiceType: 0
            },
        };
      },

    created() {
      //页面初始化函数
      this.fetchData();
    },

    methods: {
      fetchData() {
        
        this.getPaged();
      },
      //门店列表
      GetShopList() {
        shopManageSvc
          .GetShopList()
          .then(
            res => {
              this.tableData = data.items;
              this.totalList = data.totalItems;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      },
      //分页 列表
      getPaged(flag) {

        console.log("condition: " + JSON.stringify(this.condition));

        //debugger;
        this.tableLoading = true;
        shopManageSvc
        .GetShopList(this.condition)
        .then(
          res => {
            let data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;

            if (flag) {
              if (res.message != "") {
                this.$message({
                  message: res.message,
                  type: "success"
                });
              }
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          console.log("tableLoading: false");
          this.tableLoading = false;
        });
      },

      sizeChange(val) {
        this.condition.pageIndex = this.currentPage = 1;
        this.condition.pageSize = val;
        this.getPaged();
      },
      handleEdit(row) {
        console.log(row.id);
        this.$router.push({
          path: `/shopdetail/${row.id}`,
        })


      },
      handleDelete(index, row) {
        console.log(index, row)
      },
      pageTurning() {
        this.condition.pageIndex = this.currentPage;
        this.getPaged();
      },
      //搜索
      search(flag) {
        this.condition.pageIndex = this.currentPage = 1;
        this.getPaged(flag);
      }

    }
      
     
      
    
    
   
}
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 88px;
    display: flex;
    align-items: center;
  }
  .pagination {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100px;
  }
}
</style>