<template>
  <div class="demo_page">
    <div class="dom_view">
      <!-- 我的评价头部 -->
      <div class="demo_user" v-for="(mesgs,index) in username" :key="index">
        <div class="demo_header">
          <div class="header_img">
            <img :src="mesgs.headUrl" alt="">
          </div>
          <div class="header_right">
            <div class="header_user">{{mesgs.nickName}}</div>
            <div class="vip_img">
              <img :src="Url0" alt="" v-if="mesgs.memberGrade == 0">
              <img :src="Url10" alt="" v-if="mesgs.memberGrade == 10">
              <img :src="Url20" alt="" v-if="mesgs.memberGrade == 20">
              <img :src="Url30" alt="" v-if="mesgs.memberGrade == 30">
              <img :src="Url40" alt="" v-if="mesgs.memberGrade == 40">
              <img :src="Url50" alt="" v-if="mesgs.memberGrade == 50">
            </div>
            <div class="vip_name">{{mesgs.memberLevelDisplayName}}</div>
          </div>
        </div>
      </div>
      <!-- 我的订单详情 -->
      <div class="demo_order" v-for="(item,index0) in view" :key="index0">
        <div class="demo_section">
          <!-- 订单编号  服务车型 -->
          <div class="section_header">
            <div class="section_header_orderNo">订单编号：{{item.orderNo}}</div>
            <div class="section_header_carName">服务车型：{{item.carName}}</div>  
          </div>
          <!-- 单条订单信息 -->
          <div v-if="item.productNum == 1">
            <div class="section_shop" v-for="(mes,index1) in item.productOrPackages" :key="index1">
              <div class="section_shop_img">
                <img :src="mes.imageUrl" alt="">
              </div>
              <div class="section_shop_right">
                <div class="section_shop_title">{{mes.displayName}}</div>
                <div class="section_shop_bottom">
                  <div class="section_shop_price">￥ {{mes.price}}</div>
                  <div class="section_shop_num">x{{mes.number}}</div>
                </div>
              </div>
            </div>
          </div>
          <!-- 多条订单信息 -->
          <div class="section_shop_shop" v-else>
            <img :src="item2.imageUrl" alt="" v-for="(item2,index2) in item.productOrPackages" :key="index2">
          </div>
          <img :src="srcImg" alt="" v-if="item.isBest" class="EssenceClass">
          <!-- 我的订单评论 -->
          <div class="section_comment">
            <div class="section_comment_comment">
              <p class="p1">{{item.content}}<text v-if="a == 0" @tap="moreClick">更多</text></p>
              <p class="p2" v-if="a == 1">{{item.content1}} <text v-if="a == 1" @tap="retractClick">收起</text></p>
            </div>
            <div class="section_comment_img" v-for="(mesg,index2) in item.imageUrls" :key="index2">
              <img :src="mesg" alt="">
            </div>
            <!-- 回复 追评 -->
            <div v-if="item.replyInfos.length > 0">
              <div class="box_all" v-for="(item3,index3) in item.replyInfos" :key="index3">
                <!-- 官方追评 -->
                <div class="date_Official" v-if="item3.commentType == 3 || item3.commentType == 1">{{item3.createTime}}</div>
                <div class="Official_class" v-if="item3.commentType == 3 || item3.commentType == 1">
                  {{item3.displayTitle}}:
                  <text>{{item3.replyContent}}</text>
                </div>
                <div class="line_Official"></div>
                <!-- 用户追评 -->
                <div class="user_review" v-if="item3.commentType == 0 || item3.commentType == 2">{{item3.displayTitle}}</div>
                <div class="user_content" v-if="item3.commentType == 0 || item3.commentType == 2">{{item3.replyContent}}</div>
                <div class="user_imgList" v-if="item3.commentType == 0 || item3.commentType == 2"><img :src="itemimg" alt="" v-for="(itemimg,imgindex) in item3.imgUrl" :key="imgindex"></div>
              </div>
            </div>
            <div class="section_comment_shopName">店铺名称：{{item.shopName}}</div>
            <!-- 最后一行评论 -->
            <div class="section_comment_bottom">
              <div class="section_comment_createTime">{{item.createTime}}</div>
              <!-- 点赞 -->
              <div class="section_comment_like">
                <div class="zan">
                  <!-- <img :src="srcs" alt="" @click="back(item)"> -->
                  <img :src="srcs" alt="" >
                </div>
                <div class="likenum">{{item.likeNum}}</div>
              </div>
              <!-- 评论 -->
              <div class="section_comment_review" v-if="item.replyInfos.length == 0" @click="ReplyCommentsback(item)">
                <div class="zan">
                  <img :src="srcComment" alt="">
                </div>
                <div class="likenum" style="min-width:40rpx;">{{item.replyButtons.name}}</div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import {GetMyEvaluation,GetUserId,GetLike} from '../../api'
export default {
  data(){
    return{
      a:0,
      fabulousIndex:false,//点赞索引
      replyInfos:[],//用户回复 官方回复 数组
      srcs:`${this.globalData.imgPubUrl}Like - unchecked.png`,//点赞图标
      srcComment:`${this.globalData.imgPubUrl}Reply.png`,//评论图标
      username: [{headUrl: '', nickName: '',memberGrade: '',memberLevelDisplayName: ''}],//用户信息
      view: [],//订单信息
      srcImg:`${this.globalData.imgPubUrl}Essences.png`,//精华点评图片
      // 会员一系类图片
      Url0: `${this.globalData.imgPubUrl}minevip_vip_zero.png`,
      Url10: `${this.globalData.imgPubUrl}minevip_vip_one.png`,
      Url20: `${this.globalData.imgPubUrl}minevip_vip_two.png`,
      Url30: `${this.globalData.imgPubUrl}minevip_vip_three.png`,
      Url40: `${this.globalData.imgPubUrl}minevip_vip_four.png`,
      Url50: `${this.globalData.imgPubUrl}minevip_vip_five.png`,
    }
  },
  methods: {
    back(item){
      let that = item
      if(this.fabulousIndex){
        return false
      }
      let date = {
        "commentId":that.CommentId
      }
      //获取点赞
      GetLike(date).then(res => {
        console.log(`点赞问题`,res)
        if(res.data){
          that.srcs = `${this.globalData.imgPubUrl}Like - check.png`
          item.likeNum = item.likeNum + 1
          that.fabulousIndex = true
        }
      }).catch(err => {
   
      })

    },
    // 回复评论
    ReplyCommentsback(item){
      this.$router.push({
        path: '/pages/PublicationReview/main',
        query: {
          orderNo:item.orderNo,
          CommentId:item.replyButtons.replyToCommentId
        }
      })
    },
    // 更多点击事件
    moreClick(){
      this.a = 1
    },
    // 收起点击事件
    retractClick(){
      this.a = 0
    },
    // 我的评价列表
    myEvaluationList(){
      let that = this
      let rquest = {
        "PageIndex":1,
        "PageSize":1
      }
      GetMyEvaluation(rquest).then(res=>{
        console.log(`我的评价列表`,res)
        that.view = res.data.items
      }).catch(err => {
  
      })
    },
    // 获取当前用户列表
    getUserList(){
      let that = this
      GetUserId("").then(res => {
        console.log(`获取当前用户列表`,res)
        that.username[0].headUrl = res.data.headUrl
        that.username[0].nickName = res.data.nickName
        that.username[0].memberGrade = res.data.memberGrade
        that.username[0].memberLevelDisplayName = res.data.memberLevel
      }).catch(err => {
  
      })
    }
  },
  mounted(){
    this.myEvaluationList()
    this.getUserList()
  },
  onUnload(){
    this.replyInfos = []//用户回复 官方回复 数组
    this.username[0].headUrl = '' 
    this.username[0].nickName = ''
    this.username[0].memberGrade = ''
    this.username[0].memberLevelDisplayName = ''
    this.view = []
  }
}
</script>
<style scoped lang="scss">
  .dom_view{
    width: 100%;
    background: #F3F3F3;
    // display: flex;
    // flex-direction: column;
  }
  .demo_user{
    width: 100%;
  }
  .demo_order{
    position: relative;
    width: 100%;
  }
  .EssenceClass{
    position: absolute;
    top: 200rpx;
    right: 83rpx;
    width: 120rpx;
    height: 120rpx;
  }
  .demo_header{
    width: 100%;
    background: #fff;
    display: flex;
    padding: 50rpx 30rpx;
    box-sizing: border-box;
  }
  .header_img{
    width: 80rpx;
    height: 80rpx;
  }
  .header_img>img{
    width: 80rpx;
    height: 80rpx;
    border-radius: 50%;
  }
  .header_right{
    padding: 0 30rpx;
  }
  .header_user{
    font-size: 30rpx;
    color: #212121;
  }
  .vip_img{
    width: 45rpx;
    height: 45rpx;
    display: inline-block;
    padding-top: 5rpx;
  }
  .vip_img>img{
    width: 40rpx;
    height: 40rpx;
    vertical-align: middle;
  }
  .vip_name{
    font-size: 24rpx;
    color: #666666;
    display: inline-block;
    padding-left: 20rpx;
    padding-top: 5rpx;
  }
  .demo_section{
    margin-top: 16rpx;
    background: #fff;
    padding: 30rpx;
  }
  .section_header{
    display: flex;
  }
  .section_header_orderNo{
    font-size: 30rpx;
    color: #212121;
    font-weight: bold;
  }
  .section_header_carName{
    font-size: 24rpx;
    color: #999999;
    line-height: 40rpx;
    padding-left: 30rpx;
  }
  .section_shop{
    display: flex;
    padding-top: 30rpx;
    padding-bottom: 30rpx;
    border-bottom: 1px solid rgba(231,231,231,1);
  }
  .section_shop_shop{
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: center;
    padding: 30rpx 0;
    box-sizing: border-box;
    border-bottom: 1px solid rgba(231,231,231,1);
    img{
      width: 120rpx;
      height: 120rpx;
    }
  }
  .section_shop_img{
    width: 120rpx;
    height: 120rpx;
  }
  .section_shop_img>img{
    width: 100%;
    height: 100%;
  }
  .section_shop_right{
    width: 100%;
    padding-left: 30rpx;
  }
  .section_shop_title{
    width: 100%;
    font-size: 28rpx;
    color: #666666;
  }
  .section_shop_bottom{
    display: flex;
    padding-top: 16rpx;
  }
  .section_shop_price{
    font-size: 24rpx;
    color: #FF0000;
    flex: 5;
  }
  .section_shop_num{
    font-size: 24rpx;
    color: #cccccc;
  }
  .section_comment_comment{
    padding: 30rpx 0;
    p{
      font-size:28rpx;
      font-family:Source Han Sans CN;
      font-weight:400;
      color:rgba(51,51,51,1);
      text{
        font-family:Source Han Sans CN;
        font-weight:400;
        color: #FF6324;
        font-size: 24rpx;
      }
    }
  }
  .section_comment_img{
    width: 200rpx;
    height: 200rpx;
    display: inline-block;
    margin: 8rpx;
  }
  .section_comment_img>img{
    width: 100%;
    height: 100%;
  }
  // 店铺名称
  .section_comment_shopName{
    font-size: 24rpx;
    color: #999999;
    padding: 20rpx 0;
  }
  .section_comment_bottom{
    display: flex;
  }
  .section_comment_createTime{
    flex: 5;
    font-size: 24rpx;
    color: #999999;
    padding: 0 0 30rpx 0;
  }
  .section_comment_like{
    flex: 1;
    display: flex;
    flex-direction: row;
    justify-content: center;
  }
  // 评论
  .section_comment_review{
    flex:2;
    display: flex;
    flex-direction: row;
    justify-content: flex-end;
    .zan{
      img{
        width: 30rpx;
        height: 30rpx;
      }
    }
    .likenum{
      font-size: 24rpx;
      color: #999999;
      margin-left: 5rpx;
    }
  }
  // 赞
  .zan{
    width: 30rpx;
    height: 30rpx;
  }
  .zan>img{
    width: 100%;
    height: 100%;
  }
  .likenum{
    font-size: 24rpx;
    color: #999999;
    text-align: right;
  }

  .date_Official{
    width: 100%;
    font-size:24rpx;
    font-family:Source Han Sans CN;
    font-weight:400;
    color:rgba(153,153,153,1);
    margin: 15rpx 0;
  }
  // 官方回复
  .Official_class{
    width: 100%;
    padding: 30rpx;
    box-sizing: border-box;
    background:rgba(242,242,242,1);
    border-radius:10rpx;
    font-size:28rpx;
    font-family:Source Han Sans CN;
    font-weight:400;
    color:rgba(51,51,51,1);
    // border: 2rpx solid red;
    text{
      color: #888888;
    }
  }
  .line_Official{
    border: 2rpx solid #e8e8ee;
    margin: 20rpx 0;
    margin-bottom: 0;
  }
  .user_review{
    width: 100%;
    font-size:28rpx;
    font-family:Source Han Sans CN;
    font-weight:400;
    color:rgba(255,99,36,1);
    line-height: 42rpx;
    margin: 20rpx 0;
  }
  .user_content{
    width: 100%;
    font-size:28rpx;
    font-family:Source Han Sans CN;
    font-weight:400;
    color:rgba(51,51,51,1);
    overflow: hidden;
    white-space: pre-wrap;
    display: -webkit-box;
    text-overflow: ellipsis;
    -webkit-line-clamp: 4;
    -webkit-box-orient: vertical;
  }
  .user_imgList{
    width: 100%;
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: flex-start;
    align-items: center;
    margin-top: 30rpx;
    img{
      width: 200rpx;
      height: 200rpx;
      margin-right: 12rpx;
    }
  }

  .box_all{
    width: 100%;
    display: flex;
    flex-direction: column;
  }
</style>