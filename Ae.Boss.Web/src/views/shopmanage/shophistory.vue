<template>
  <main class="container" >
    <!-- 首页 --> <section style="margin-top:30px;">
                  <el-table v-loading="tableLoading" :data="tableData" border    style="width: 87%;margin-left:80px;" >
                    <el-table-column label="编号" prop="shopId" width="80"></el-table-column>
                    <el-table-column label="操作人" prop="createBy" width="180"></el-table-column>
                    <el-table-column label="操作时间" prop="createTime" width="180"></el-table-column>
                    <el-table-column label="操作项目" prop="operateType" width="200"></el-table-column>
                    <el-table-column label="操作内容" prop="summary" ></el-table-column>
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
    
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { shopManageSvc } from "@/api/shopmanage/shopmanage";

export default {
    data() {
        return{
ShopId:"",
            readonly: true,
            tableLoading: false,
            currentPage: 1,
            flag: this.global.deletedFlag,
            tableData: [],
            totalList: 0,
            //table页面的条件
       
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
           this.ShopId = this.$route.query.ShopId
        shopManageSvc
          .getShopLog({ShopId:this.ShopId})
          .then(
            res => {
              this.tableData = res.data;
              this.totalList= this.tableData.length;
              console.log( 2222,this.tableData.length);
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      },
      //分页 列表
      getPaged(flag) {

  this.ShopId = this.$route.params.ShopId
 console.log(333,this.ShopId)
        //debugger;
        this.tableLoading = true;
     console.log(333,this.ShopId)
        shopManageSvc
        .getShopLog({ShopId:this.ShopId})
        .then(
          res => {
            let data = res.data;
            console.log( 222,data);

     this.tableData = data;
     this.totalList= this.tableData.length;
      console.log( 2222,this.tableData);
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
        console.log(121,this.condition.pageSize)
        this.getPaged();
      },
      // handleEdit(row) {
      //   console.log(row.id);
      //   this.$router.push({
      //     path: `/shopdetail/${row.id}`,
      //   }
      // },
      handleDelete(index, row) {
        console.log(index, row)
      },
      pageTurning() {
        this.condition.pageIndex = this.currentPage;
        this.getPaged();
      }


    }
      
     
      
    
    
   
}
</script>

<style lang="scss" scoped>
.container {
  margin-left: 30px;
    
    display:flex;
    justify-content:center;
    flex-direction:column;

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
    margin-top:160px;
  }
}
>>> .el-row[data-v-67b0bb0c] {
  padding-left: 20px;
  margin-top: -10px;
}
>>> .el-dialog__header {
  padding: 10px 10px 10px;
  background: #eee;
  text-align: center;
}

>>> .el-table th,
.el-table tr {
  text-align: center;
  background: #f5f7fa;
  color: #333;
}
>>> .el-table td, .el-table th{
text-align: center;
}
</style>