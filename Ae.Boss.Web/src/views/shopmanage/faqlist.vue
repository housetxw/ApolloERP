<template>
  <main class="container">
    <!-- 首页 -->
    <!-- <section id="indexPage"> -->
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
        <el-form-item prop="channelId">
          <el-select
            v-model="condition.channelId"
            clearable
            filterable
            @change="unchannel"
            placeholder="请选择渠道"
            class="margin-right-10"
          >
            <el-option
              v-for="item in faqChannelList"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="oneCategoryId">
          <el-select
            v-model="condition.oneCategoryId"
            @change="GetFaqCategoryTwoList"
            clearable
            filterable
            placeholder="请选择一级分类"
            class="margin-right-10"
          >
            <el-option
              v-for="item in faqCategoryOne"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="twoCategoryId">
          <el-select
            v-model="condition.twoCategoryId"
            @change="GetFaqCategoryThreeList"
            clearable
            filterable
            placeholder="请选择二级分类"
            class="margin-right-10"
          >
            <el-option
              v-for="item in faqCategoryTwo"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="threeCategoryId">
          <el-select
            v-model="condition.threeCategoryId"
            @change="uncategoryThree"
            clearable
            filterable
            placeholder="请选择三级分类"
            class="margin-right-10"
          >
            <el-option
              v-for="item in faqCategoryThree"
              :key="item.id"
              :label="item.name"
              :value="item.id"
            >
            </el-option>
          </el-select>
        </el-form-item>
        <el-form-item prop="question">
          <el-input
            v-model="condition.question"
            style="width: 180px"
            clearable
            placeholder="请输入问题"
          />
        </el-form-item>
        <router-link :to="{ name: 'faqinfo', params: { id: 0 } }">
          <el-button size="mini" type="primary">添加</el-button>
        </router-link>
      </template>

      <template v-slot:tb_cols>
        <el-table-column label="编号" prop="id"></el-table-column>
        <el-table-column label="渠道" prop="channelName"></el-table-column>
        <el-table-column
          label="一级分类"
          prop="categoryOneName"
        ></el-table-column>
        <el-table-column
          label="二级分类"
          prop="categoryTwoName"
          width="100"
        ></el-table-column>
        <el-table-column
          label="三级分类"
          prop="categoryThreeName"
          width="100"
        ></el-table-column>
        <el-table-column
          label="问题"
          prop="question"
          width="180"
        ></el-table-column>
        <el-table-column
          label="回答"
          prop="answer"
          width="350"
        ></el-table-column>
        <el-table-column
          label="创建时间"
          prop="createTime"
          width="130"
        ></el-table-column>
        <el-table-column
          label="修改时间"
          prop="createTime"
          width="130"
        ></el-table-column>
        <el-table-column
          label="创建人"
          prop="createBy"
          width="150"
        ></el-table-column>
        <el-table-column
          label="修改人"
          prop="updateBy"
          width="150"
        ></el-table-column>
        <el-table-column label="操作" fixed="right" width="100">
          <template slot-scope="scope">
            <router-link
              :to="{ name: 'faqinfo', params: { id: scope.row.id } }"
            >
              <el-button size="mini" type="primary">详情</el-button>
            </router-link>
            <el-button
              type="danger"
              size="small"
              @click="handleDelete(scope.row)"
              >删除</el-button
            >
          </template>
        </el-table-column>
      </template>
    </rg-page>
    <!-- </section> -->
  </main>
</template>


<script>
import { Message, MessageBox } from "element-ui";
import { shopManageSvc } from "@/api/shopmanage/shopmanage";

export default {
  data() {
    return {
      readonly: true,
      tableLoading: false,
      currentPage: 1,
      flag: this.global.deletedFlag,
      tableData: [],
      totalList: 0,
      faqChannelList: [],
      faqCategoryOne: [],
      faqCategoryTwo: [],
      faqCategoryThree: [],
      //table页面的条件
      condition: {
        channelId: undefined,
        oneCategoryId: undefined,
        twoCategoryId: undefined,
        threeCategoryId: undefined,
        question: "",
        pageIndex: 1,
        pageSize: 10,
      },
    };
  },

  created() {
    //页面初始化函数
    this.fetchData();
  },

  methods: {
    fetchData() {
      this.GetFaqChannelList();
      this.GetFaqCategoryOneList();
      this.getPaged();
    },
    unchannel() {
      if (this.condition.channelId == "") {
        this.condition.channelId = undefined;
      }
    },
    uncategoryThree() {
      if (this.condition.threeCategoryId == "") {
        this.condition.threeCategoryId = undefined;
      }
    },
    //渠道列表
    GetFaqChannelList() {
      shopManageSvc
        .GetFaqChannelList()
        .then(
          (res) => {
            this.faqChannelList = res.data;
            console.log("渠道：", this.faqChannelList);
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    //分类列表
    GetFaqCategoryOneList: function () {
      shopManageSvc
        .GetFaqCategoryList({
          categoryId: 0,
        })
        .then(
          (res) => {
            this.faqCategoryOne = res.data;
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    GetFaqCategoryTwoList: function (categoryId) {
      if (categoryId == "") {
        this.condition.oneCategoryId = undefined;
        this.condition.twoCategoryId = undefined;
        this.faqCategoryTwo = undefined;
        return;
      }
      shopManageSvc
        .GetFaqCategoryList({
          categoryId: categoryId,
        })
        .then(
          (res) => {
            this.faqCategoryTwo = res.data;
            if (type == 1) {
              this.faqCategoryOne = res.data;
            } else if (type == 2) {
              this.faqCategoryTwo = res.data;
            } else if (type == 3) {
              this.faqCategoryThree = res.data;
            }
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {});
    },
    GetFaqCategoryThreeList: function (categoryId) {
      if (categoryId == "") {
        this.condition.twoCategoryId = undefined;
        this.condition.threeCategoryId = undefined;
        this.faqCategoryThree = undefined;
        return;
      }
      shopManageSvc
        .GetFaqCategoryList({
          categoryId: categoryId,
        })
        .then(
          (res) => {
            this.faqCategoryThree = res.data;
          },
          (error) => {
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
        .GetFAQList(this.condition)
        .then(
          (res) => {
            let data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;
            if (res.code == "10000") {
              this.$message({
                message: "查询成功",
                type: "success",
              });
              this.goLastView(this.visitedViews, 1000);
            } else {
              this.$message({
                message: "查询失败:" + res.message,
                type: "error",
              });
            }
            setTimeout(function () {
              loading.close();
            }, 1500);
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          this.tableLoading = false;
        });
    },

    sizeChange(val) {
      this.condition.pageIndex = this.currentPage = 1;
      this.condition.pageSize = val;
      this.getPaged();
    },

    handleAdd() {
      this.$router.push({
        path: `/faqinfo/0`,
      });
    },
    handleEdit(row) {
      console.log(row.id);
      this.$router.push({
        path: `/faqinfo/${row.id}`,
      });
    },
    //删除FAQ
    handleDelete(row) {
      console.log("删除：", row);
      var data = {
        FAQId: row.id,
      };
      shopManageSvc
        .DeleteFAQById(data)
        .then(
          (res) => {
            var data = res.code;
            if (data == "10000") {
              this.$message({
                message: "删除成功",
                type: "success",
              });
              this.getPaged();
            } else {
              this.$message({
                message: "删除失败:" + res.message,
                type: "error",
              });
            }
            setTimeout(function () {
              loading.close();
            }, 1500);
          },
          (error) => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {});
    },
    pageTurning(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    //搜索
    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    },
  },
};
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
