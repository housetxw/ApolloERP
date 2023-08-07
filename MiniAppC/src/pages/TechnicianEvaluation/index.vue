<template>
  <div class="demo_page">
    <!-- 轮播图 -->
    <swiper
      :autoplay="true"
      :previous-margin="preMargin"
      :next-margin="nextMargin"
      :circular="true"
      style="width:100%;height:480rpx;background: #fff;"
      @change="swiperChange"
    >
      <!-- <block v-for="(itema,indexa) in swiperarr" :key="indexa">
        <swiper-item>
          <div class="item_box">
            <div :class="['swiper_box',{'active':currentIndex == indexa}]">
              <div class="technician_img">
                <img :src="itema.avatar" alt="">
              </div>
              <div class="technician_content">
                <p class="technician_name">{{itema.name}}</p>
                <p class="technician_level">{{itema.level}}</p>
                <p class="technician_introduce">技师介绍</p>
                <p class="technician_description">{{itema.description}}</p>
              </div>
            </div>
          </div>
        </swiper-item>
      </block>-->
      <block>
        <swiper-item>
          <div class="item_box">
            <div class="swiper_box">
              <div class="technician_img">
                <img :src="swiper.avatar" alt />
              </div>
              <div class="technician_content">
                <p class="technician_name">{{swiper.name}}</p>
                <p class="technician_level">{{swiper.level}}</p>
                <p class="technician_introduce">技师介绍</p>
                <p class="technician_description">{{swiper.description}}</p>
              </div>
            </div>
          </div>
        </swiper-item>
      </block>
    </swiper>
    <!-- 技师评价 -->
    <div class="evaluate">
      <text class="evaluate_left">技师评价({{commentNum}})</text>
      <text class="evaluate_right">好评率:{{applauseRate}}%</text>
    </div>
    <!-- 技师评价标签 -->
    <div class="tag">
      <div
        class="taglist"
        v-for="(item,index) in list"
        :key="index"
      >{{item.labelName}}({{item.num}})</div>
    </div>
    <!-- 主体 -->
    <div class="comment" v-for="(item,index1) in comment" :key="index1">
      <!-- 用户头像 左侧部分-->
      <div class="comment_headUrl">
        <img :src="item.headUrl" alt style="width:60rpx;height: 60rpx;border-radius: 30rpx;" />
      </div>
      <!-- 右侧部分 -->
      <div class="comment_comment">
        <div class="comment_header">
          <!-- 昵称 -->
          <div class="comment_nickName">{{item.nickName}}</div>
          <!-- 会员等级 -->
          <div class="comment_vip">
            <img :src="Url0" alt v-if="item.memberGrade == 0" />
            <img :src="Url10" alt v-if="item.memberGrade == 10" />
            <img :src="Url20" alt v-if="item.memberGrade == 20" />
            <img :src="Url30" alt v-if="item.memberGrade == 30" />
            <img :src="Url40" alt v-if="item.memberGrade == 40" />
            <img :src="Url50" alt v-if="item.memberGrade == 50" />
          </div>
          <!-- 服务车型 -->
          <div class="comment_carName">服务车型：{{item.carName}}</div>
        </div>
        <!-- 星星数量 -->
        <van-rate v-model="item.score" readonly />
        <!-- 车师傅非常专业 -->
        <div class="comment_section">
          <div class="comment_title">{{item.content}}</div>
        </div>
        <!-- 门店图片 -->
        <div class="comment_section_img">
          <div class="comment_title_img" v-for="(items,index2) in item.imageUrls" :key="index2">
            <img :src="items" alt />
          </div>
        </div>

        <!-- 时间 -->
        <div class="comment_createTime_top">{{item.createTime}}</div>
        <!-- 追评 -->
        <div class="comment_Review" v-for="(item2,index3) in item.replyInfos" :key="index3">
          <div class="review_content2" v-if="item2.commentType == 2 || item2.commentType == 4">
            <p
              class="review_title1"
              v-if="item2.commentType == 2 || item2.commentType == 4"
            >{{item2.displayTitle}}</p>
            {{item2.replyContent}}
          </div>
          <div class="review_content" v-if="item2.commentType == 3 || item2.commentType == 1">
            <p
              class="review_title2"
              v-if="item2.commentType == 3 || item2.commentType == 1"
            >{{item2.displayTitle}}</p>
            {{item2.replyContent}}
          </div>
        </div>
        <!-- 店铺名称 -->
        <div class="comment_header">
          <div class="comment_shopName">店铺名称：{{item.shopName}}</div>
        </div>
        <!-- 底部 -->
        <div class="comment_footer">
          <!-- 时间 -->
          <div class="comment_createTime">{{item.createTime}}</div>
          <!-- 点赞 -->
          <div class="zan">
            <div
              class="comment_zan_img"
              v-for="(itemb,indexb) in item.commArr"
              :key="indexb"
              @tap="applauseClick(item,indexb)"
            >
              <img :src="itemb.isTrue?itemb.url:itemb.srcs" alt />
            </div>
            <div class="comment_likeNum">{{item.likeNum}}</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import {
  GetUserId1,
  GetGood,
  GetShopList1,
  GetEvaluate,
  GetLike
} from '../../api'
export default {
  data() {
    return {
      currentIndex: 0,
      EmployeeId: '', // 员工id
      preMargin: '200rpx',
      nextMargin: '200rpx',
      swiper: {
        avatar: '',
        name: '',
        level: '',
        description: ''
      },
      applauseRate: '',
      commentNum: '',
      list: [],
      comment: [],
      swiperarr: [
        {
          avatar: 'https://m.aerp.com.cn/Shops/2014113155926.jpg',
          name: '洪小',
          level: '高级技师',
          description: '高级技师，曾获得国家一等技师荣誉称号'
        },
        {
          avatar: 'https://m.aerp.com.cn/Shops/2014113155926.jpg',
          name: '洪小',
          level: '高级技师',
          description: '高级技师，曾获得国家一等技师荣誉称号'
        },
        {
          avatar: 'https://m.aerp.com.cn/Shops/2014113155926.jpg',
          name: '洪小',
          level: '高级技师',
          description: '高级技师，曾获得国家一等技师荣誉称号'
        }
      ],
      // 会员等级图片
      Url0: `${this.globalData.imgPubUrl}minevip_vip_zero.png`,
      Url10: `${this.globalData.imgPubUrl}minevip_vip_one.png`,
      Url20: `${this.globalData.imgPubUrl}minevip_vip_two.png`,
      Url30: `${this.globalData.imgPubUrl}minevip_vip_three.png`,
      Url40: `${this.globalData.imgPubUrl}minevip_vip_four.png`,
      Url50: `${this.globalData.imgPubUrl}minevip_vip_five.png`,
      srcs: `${this.globalData.imgPubUrl}Like - unchecked.png`, // 点赞图片
      commArr: [
        {
          srcs: `${this.globalData.imgPubUrl}Like - unchecked.png`,
          url: `${this.globalData.imgPubUrl}Like - check.png`,
          isTrue: false
        }
      ] // 点赞图片数组
    }
  },
  methods: {
    swiperChange(e) {
      this.currentIndex = e.mp.detail.current
    },
    // 点赞
    applauseClick(item, indexb) {
      let that = this
      if (item.fabulousIndex) {
        item.likeNum = item.likeNum > 0 ? item.likeNum - 1 : item.likeNum
        item.commArr[indexb].isTrue = false
      } else {
        let date = {
          commentId: item.commentId
        }
        // 获取点赞
        GetLike(date)
          .then(res => {
            console.log(`获取点赞`, res)
            if (res.code == 10000) {
              item.commArr[indexb].isTrue = true
              item.likeNum = item.likeNum + 1
              wx.showToast({ title: '点赞成功', icon: 'none' })
            }
          })
          .catch(err => {})
      }
      item.fabulousIndex = !item.fabulousIndex
    },
    // 获取员工基本信息
    getStaffInfo() {
      let that = this
      let rquest = {
        EmployeeId: that.EmployeeId
      }
      GetUserId1(rquest)
        .then(res => {
          console.log(`获取员工基本信息`, res)
          this.swiper = res.data
        })
        .catch(err => {})
    },
    // 获取技师评价数量和好评率
    getTechnicianNum() {
      let that = this
      let rquest = {
        EmployeeId: that.EmployeeId
      }
      GetGood(rquest)
        .then(res => {
          console.log(`获取技师评价数量和好评率`, res)
          that.commentNum = res.data.commentNum
          that.applauseRate = res.data.applauseRate
        })
        .catch(err => {})
    },
    // 获取技师评价标签列表
    getTechnicianMeal() {
      let that = this
      let rquest = {
        EmployeeId: that.EmployeeId
      }
      GetShopList1(rquest)
        .then(res => {
          console.log(`获取技师评价标签列表`, res)
          that.list = res.data.items
        })
        .catch(err => {})
    },
    // 获取技师评价列表
    getTechnicianList() {
      let that = this
      let rquest = {
        EmployeeId: that.EmployeeId
      }
      GetEvaluate(rquest)
        .then(res => {
          console.log(`获取技师评价列表`, res)
          let arr = res.data.items
          arr.forEach(element => {
            element.commArr = JSON.parse(JSON.stringify(that.commArr))
            element.commDis = false // 点赞所需变量
          })
          that.comment = arr
        })
        .catch(err => {})
    }
  },
  mounted() {
    this.getStaffInfo() // 获取员工基本信息
    this.getTechnicianNum() // 获取技师评价数量和好评率
    this.getTechnicianMeal() // 获取技师评价标签列表
    this.getTechnicianList() // 获取技师评价列表
  },
  onLoad(options) {
    this.EmployeeId = options.EmployeeId
  },
  onUnload() {
    this.avatar = ''
    this.name = ''
    this.level = ''
    this.description = ''
    this.applauseRate = ''
    this.commentNum = ''
    this.list = []
    this.comment = []
  }
}
</script>
<style scoped lang="scss">
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #f3f3f3;
}
.technician_img {
  width: 150rpx;
  height: 150rpx;
  // margin: 20rpx auto;
}
.technician_img > img {
  width: 100%;
  height: 100%;
}
.technician_content > .technician_name {
  text-align: center;
  font-size: 28rpx;
  color: #333333;
}
.technician_level {
  text-align: center;
  font-size: 32rpx;
  color: #333333;
  padding: 18rpx 0 34rpx 0;
}
.technician_introduce {
  text-align: center;
  font-size: 24rpx;
  color: #666666;
}
.technician_description {
  width: 100%;
  text-align: center;
  padding: 0 20rpx;
  box-sizing: border-box;
  font-size: 24rpx;
  color: #666666;
}
.evaluate {
  width: 100%;
  padding: 30rpx;
  padding-bottom: 0;
  margin-top: 21rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: flex-start;
  background: rgba(255, 255, 255, 1);
}
.evaluate > .evaluate_left {
  font-size: 30rpx;
  font-weight: bold;
}
.evaluate > .evaluate_right {
  font-size: 24rpx;
  color: #999999;
}
.evaluate > .evaluate_right > span {
  font-size: 24rpx;
  color: #ff5914;
}
.tag {
  width: 100%;
  padding: 30rpx;
  padding-bottom: 0;
  box-sizing: border-box;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-wrap: wrap;
}
.tag > .taglist {
  flex-shrink: 0;
  height: 57rpx;
  margin: 0 7rpx 29rpx 0;
  font-size: 24rpx;
  border: 1px solid #cccccc;
  border-radius: 28.5rpx;
  text-align: center;
  line-height: 57rpx;
  color: #666666;
  padding: 0 41rpx;
}
.comment {
  width: 100%;
  padding: 30rpx;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-wrap: wrap;
  box-sizing: border-box;
}
.comment_comment {
  flex: 5;
  padding: 0 18rpx;
  padding-right: 0;
  box-sizing: border-box;
}
.comment_header {
  display: flex;
  margin-top: 15rpx;
}
.comment_nickName {
  font-size: 28rpx;
  color: #666666;
}
.comment_carName {
  font-size: 24rpx;
  color: #999999;
}
.comment_section {
  padding-top: 24rpx;
}
.comment_title_img {
  width: 200rpx;
  height: 200rpx;
  display: inline-block;
  padding-top: 24rpx;
  margin-left: 6rpx;
}
.comment_title_img > img {
  width: 100%;
  height: 100%;
}
.comment_shopName {
  font-size: 24rpx;
  color: #999999;
}
.comment_footer {
  padding: 24rpx 0;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: flex-start;
  background: rgba(255, 255, 255, 1);
}
.comment_createTime {
  font-size: 24rpx;
  color: #999999;
}
.comment_likeNum {
  font-size: 24rpx;
  color: #999999;
}
.zan {
  display: flex;
}
.comment_zan_img {
  width: 32rpx;
  height: 32rpx;
  margin-right: 15rpx;
}
.comment_zan_img > img {
  width: 100%;
  height: 100%;
}
.comment_vip {
  width: 40rpx;
  height: 40rpx;
  margin: 0 25rpx;
}
.comment_vip > img {
  width: 100%;
  height: 100%;
}
.review_title1 {
  font-size: 28rpx;
  padding-top: 30rpx;
}
.review_content2 {
  background: rgba(242, 242, 242, 1);
  margin-top: 30rpx;
  padding: 0 30rpx 30rpx 30rpx;
  color: #333333;
  font-size: 28rpx;
}
.review_content2 > p {
  padding: 16rpx 0;
}
.review_title2 {
  font-size: 28rpx;
  padding: 30rpx 0;
  color: #ff6324;
}
.review_title3 {
  font-size: 28rpx;
  padding-top: 30rpx;
}
.comment_createTime_top {
  font-size: 24rpx;
  color: #999999;
  padding: 30rpx 0 0 0;
}

.item_box {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 460rpx;
}
.swiper_box {
  width: 300rpx;
  background: #abacac;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 20rpx 0;
  box-sizing: border-box;
  height: 400rpx;
}
.active {
  height: 450rpx;
  width: 400rpx;
  background: #f3f3f3;
  padding: 20rpx;
  box-sizing: border-box;
  transition: all 0.2s ease-in 0s;
}
</style>