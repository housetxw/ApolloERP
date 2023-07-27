<template>
  <div class="container">
       <rg-page
      id="indexPage"
      :pageIndex="condition.pageIndex"
      :pageSize="condition.pageSize"
      :dataCount="totalList"
      :tableLoading="tableLoading"
      :tableData="tableData"
      :pageChange="pageTurning"
      :searching="search"
      :conditionModel="condition"
    >
     <template v-slot:condition>
        <el-form-item prop="userTel"> 
        <el-input class="input" clearable  placeholder="请输入用户手机号" prefix-icon="el-icon-search" v-model="condition.userTel">
        </el-input>
         </el-form-item>
            <el-form-item prop="userName">
        <el-input class="input" clearable  placeholder="请输入用户姓名" prefix-icon="el-icon-search" v-model="condition.userName">
        </el-input>
         </el-form-item>
            <el-form-item prop="userId">
        <el-input class="input" clearable  placeholder="请输入用户Id" prefix-icon="el-icon-search" v-model="condition.userId">
        </el-input>
         </el-form-item>
           
        <!-- <el-button type="primary" size="small" style="margin-left:30px;" @click="search(true)" >搜索</el-button> -->
    </template>
 <template v-slot:tb_cols>
    <!-- <el-table :data="tableData" border style="width: 100%" v-loading="tableLoading"> -->
      <el-table-column label="用户Id" prop="userId"  width="250"></el-table-column>
      <el-table-column label="姓名" prop="userName" ></el-table-column>
      <el-table-column label="昵称" prop="nickName" ></el-table-column>
      <el-table-column label="联系姓名" prop="contactName"></el-table-column>
      <el-table-column label="手机号" prop="userTelDes" :width="120"></el-table-column>
      <el-table-column label="邮箱" prop="email" :width="120"></el-table-column>
      <el-table-column label="出生年月" prop="birthDay" :width="100"></el-table-column>
      <el-table-column label="性别" align="center" prop="gender" :width="60">
        <template slot-scope="scope">					
            <span v-if="scope.row.gender==1">
                男
            </span>
            <span v-else-if="scope.row.gender==2">
                女
            </span>					
        </template>
      </el-table-column>
      <el-table-column label="注册时间" prop="createTime" :width="140"></el-table-column>
      <el-table-column fixed="right" label="操作" :width="100">
        <template slot-scope="scope">
           <router-link :to="'userinfo/'+scope.row.userId">
              <el-link type="primary">查看详情</el-link>
           </router-link>
          <!-- <el-button @click="routerToDetail(scope.row.userId)" type="text" size="small">查看详情</el-button> -->
        </template>
      </el-table-column>
    </template>

    <!-- </el-table> -->
    <!-- <section class="pagination">
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
        </section> -->
       </rg-page>
  </div>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/user/user";

export default {
    name:'userlist',
  data() {
    return {
      userTel: '',
      userName: '',
      userId: '',
      tableLoading: false,
      currentPage: 1,
      totalList: 0,
      condition: {
        pageIndex: 1,
        pageSize: 20,
        userId:"",
        userTel:"",
        userName:""
      },
      tableData:[]
    }
  },
  created() {
    this.fetchData();
  },
  methods:{
      search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    },
    fetchData(){
        this.getPaged();
    },
    getPaged(flag) {
      console.log("condition: " + JSON.stringify(this.condition));
      this.tableLoading = true;
      appSvc
        .getUserList(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (flag) {
              this.$message({
                message: res.message || "查询成功",
                type: "success"
              });
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          // console.log("tableLoading: false");
          this.tableLoading = false;
        });
    },
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    //     pageChange(p) {
    //   this.condition.pageIndex = p.pageIndex;
    //   this.condition.pageSize = p.pageSize;
    //   this.getPaged();
    // },
    sizeChange(val) {
      this.condition.pageIndex = this.currentPage = 1;
      this.condition.pageSize = val;
      this.getPaged();
    },
    routerToDetail(userIdPara){
        this.$router.push({ path: `userinfo/${userIdPara}`, query: { userId: userIdPara }});
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
    .input{
        width: 180px;
        margin-right: 20px;
    }
  }
  .pagination {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100px;
  }
}
</style>