<template>
  <main class="container">
    <!-- 首页 -->
    <section id="indexPage">
      <el-form :model="faqInfo" :inline="true" ref="faqInfo">
          <el-container>
            <el-main>
              <el-row :gutter="10">
              <el-form-item label="渠道">
                <el-select
                    v-model="faqInfo.channelId"
                    size="small"
                    clearable
                    filterable
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
              </el-row>
              <el-row :gutter="10">
              <el-form-item label="分类">
                <span class="input-label">一级分类</span>
              <el-select
                  v-model="faqInfo.categoryOneId"
                  @change="GetFaqCategoryTwoList"
                  size="small"
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
              <span class="input-label">二级分类</span>
              <el-select
                  v-model="faqInfo.categoryTwoId"
                  @change="GetFaqCategoryThreeList"
                  size="small"
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
                  <span class="input-label">三级分类</span>
                  <el-select
                      v-model="faqInfo.categoryThreeId"
                      size="small"
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
                </el-row>
                <el-row :gutter="30">
                <el-form-item label="问题">
                  <el-input v-model="faqInfo.question" placeholder="请输入问题" type="textarea" autosize style="width:800px"/>
                </el-form-item>
                <el-form-item label="回答">
                  <el-input v-model="faqInfo.answer" placeholder="请输入回答" type="textarea" style="width:800px;"/>
                </el-form-item>
                </el-row>
                <el-form-item>
                    <el-button icon="el-icon-check" type="primary" @click="ModifyFAQ()">保存</el-button>
                </el-form-item>
              </el-main>
            </el-container>
        </el-form>
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
            faqChannelList:[],
            faqCategoryOne:[],
            faqCategoryTwo:[],
            faqCategoryThree:[],
            faqInfo:[],
            id:0,
            faqInfoCondition: {
              FAQId: 0
            },
        };
      },

    created() {
      //页面初始化函数
      this.id = this.$route.params.id
      console.log("FAQId: " + this.id);
      this.fetchData();
    },
    methods: {
      fetchData() {
        if(this.id > 0){
          this.GetFAQInfo();
        }else{
          this.GetFaqChannelList();
          this.GetFaqCategoryOneList();
        }
      },


      GetFAQInfo (){
        this.faqInfoCondition = {
            FAQId:this.$route.params.id
        }
        shopManageSvc
          .GetFAQInfo(this.faqInfoCondition)
          .then(
            res => {
              console.log("FAQInof: " + res.data);
              this.faqInfo = res.data;
              this.GetFaqChannelList();
              this.GetFaqCategoryOneList();
              this.GetFaqCategoryTwoList(this.faqInfo.categoryOneId);
              this.GetFaqCategoryThreeList(this.faqInfo.categoryTwoId);
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      },
      //渠道列表
      GetFaqChannelList() {
        shopManageSvc
          .GetFaqChannelList()
          .then(
            res => {
              this.faqChannelList = res.data;
              console.log("渠道：",this.faqChannelList);
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      },
      //分类列表
      GetFaqCategoryOneList: function() {
        shopManageSvc
          .GetFaqCategoryList({
            categoryId : 0
          })
          .then(
            res => {
              this.faqCategoryOne = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      },
      GetFaqCategoryTwoList: function(categoryId) {
        if (categoryId == "") {
          this.faqInfo.categoryOneId = undefined;
          this.faqInfo.categoryTwoId = undefined;
          this.faqInfo.categoryThreeId = undefined;
          this.faqCategoryTwo =  undefined;
          this.faqCategoryThree =  undefined;
          return;
        }
        shopManageSvc
          .GetFaqCategoryList({
            categoryId : categoryId
          })
          .then(
            res => {
              this.faqCategoryTwo = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      },
      GetFaqCategoryThreeList: function(categoryId) {
        if (categoryId == "") {
          this.faqInfo.categoryTwoId = undefined;
          this.faqInfo.categoryThreeId = undefined;
          this.faqCategoryThree =  undefined;
          return;
        }
        shopManageSvc
          .GetFaqCategoryList({
            categoryId : categoryId
          })
          .then(
            res => {
              this.faqCategoryThree = res.data;
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {});
      },
      //修改FAQ
      ModifyFAQ() {
        var data = {
            Id:this.id,
            ChannelId: this.faqInfo.channelId, 
            CategoryOneId: this.faqInfo.categoryOneId, 
            CategoryTwoId: this.faqInfo.categoryTwoId, 
            CategoryThreeId: this.faqInfo.categoryThreeId,
            Question: this.faqInfo.question,
            Answer: this.faqInfo.answer
        }
        console.log("ModifyFAQ:",data);

        shopManageSvc
          .ModifyFAQ(data)
          .then(
            res => {
              var data = res.code;
                  if (data == "10000") {
                    this.$message({
                      message: "保存成功",
                      type: "success"
                    });
                    this.goLastView(this.visitedViews, 1000);
                  } else {
                    this.$message({
                      message: "保存失败:" + res.message,
                      type: "error"
                    });
                  }
                  setTimeout(function() {
                    loading.close();
                  }, 1500);
            },
            error => {
              console.log(error);
            }
          )
          .catch(() => {})
          .finally(() => {});
      },
      

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