<template>
  <div class="demo_page" :style="{minHeight:windowHeight}">
    <div class="demo_shop">
      <!-- 商品信息 -->
      <div class="shop_header" v-for="(item,index) in orderShop" :key="index">
        <div class="shop_img">
          <img :src="item.productImageUrl" alt />
        </div>
        <div class="shop_content">
          <div class="shop_content_title">{{item.productDisplayName}}</div>
          <div class="shop_content_name">服务车型：{{carName}}</div>
        </div>
      </div>
      <!-- 商品评论 -->
      <div class="shop_comment">
        <div class="shop_comment_content">{{commentContent}}</div>
        <div class="shop_comment_IDname">店铺名称：{{shopName}}</div>
        <div class="shop_comment_bottom">
          <div class="shop_comment_time">{{createTime}}</div>
          <div class="shop_comment_like">
            <img :src="srcs" alt />
            <text>{{likeNum}}</text>
          </div>
        </div>
      </div>
      <!-- 书写商品评论 -->
      <div class="shop_Writecomments">
        <div class="Ipt">
          <textarea
            name="desc"
            :value="textvalue"
            placeholder-style="color:rgba(102,102,102,1);font-size:28rpx;"
            placeholder="追加点评：写一下你的感受吧~  20-500字以内。图片1M以内，jpg或png格式"
            maxlength="500"
            @input="bindTextAreaBlur"
          ></textarea>
          <div class="Obtain">
            追评订单可获得
            <text>100积分</text>
          </div>
          <!-- 上传图片 -->
          <div class="nav_img" v-for="(itemss,nn) in fileList" :key="nn">
            <van-icon name="cross" @click="delImg(nn)" class="delte" />
            <img :src="itemss" alt @click="preview(nn)" :data-src="itemss" />
          </div>
          <div class="add_img" @click="upLoad">
            <img :src="up" alt />
          </div>
        </div>
      </div>
    </div>
    <div class="bottom_btn">
      <p @click="Submission">提交评价</p>
    </div>
  </div>
</template>
<script>
import { GetShop, GetLike, GetSubmission } from '../../api'
export default {
  data() {
    return {
      windowHeight: '',
      textvalue: '', //用户评价内容
      orderShop: [], //订单信息
      commentContent: '', //点评内容
      shopName: '', //门店名称
      createTime: '', //点评时间
      likeNum: '', //点赞数
      carName: '', //服务车型
      shop: [],
      srcs: `${this.globalData.imgPubUrl}Like - check.png`, //已点赞图片
      up: `${this.globalData.imgPubUrl}camera.png`,
      fileList: [],
      orderNo: '', //上个页面传入的订单号
      CommentId: '' //评论id
    }
  },
  methods: {
    // 用户输入的内容
    bindTextAreaBlur(e) {
      this.textvalue = e.mp.detail.value
    },
    // 全屏预览图片
    preview(nn) {
      let currentUrl = this.fileList[nn]
      wx.previewImage({
        current: currentUrl, // 当前显示图片的http链接
        urls: this.fileList // 需要预览的图片http链接列表
      })
    },
    // 删除单张图
    delImg(index) {
      // 删除指定下标的图片对象
      // if (isNaN(index) || index >= this.fileList.length) {
      //   return false
      // }
      // let tmp = []
      // for (let i = 0, len = this.fileList.length; i < len; i++) {
      //   if (this.fileList[i] !== this.fileList[index]) {
      //     tmp.push(this.fileList[i])
      //   }
      // }
      // this.fileList = tmp
      this.fileList.splice(index, 1)
    },
    upLoad() {
      let that = this
      wx.chooseImage({
        sizeType: ['original', 'compressed'], // original 原图，compressed 压缩图，默认二者都有
        sourceType: ['album', 'camera'],
        success(res) {
          wx.uploadFile({
            url: 'https://cminiapi.aerp.com.cn/QiNiu/UploadStream',
            filePath: res.tempFilePaths[0],
            name: 'file',
            formData: {
              File: 'file',
              Directory: res.tempFilePaths[0]
            },
            header: {
              'Content-Type': 'multipart/form-data', // 记得设置
              // 'Content-Type': 'application/json',
              Authorization:
                'Bearer ' + wx.getStorageSync('tokenInfo').accessToken
            },
            success(resa) {
              console.log(`wx.uploadFile`, JSON.parse(resa.data))
              let url = JSON.parse(resa.data).data
              that.fileList.push('https://m.aerp.com.cn/' + url)
            },
            fail: function(data) {
              console.log('上传错误信息', data)
            }
          })
        }
      })
    },
    // 提交评价
    Submission() {
      let that = this
      let rquest = {
        orderNo: that.orderNo,
        commentId: that.CommentId,
        connent: that.textvalue,
        isAnonymous: true,
        imageUrls: that.fileList
      }
      //提交评价
      GetSubmission(rquest).then(res => {
        console.log(`提交评价`, res)
        if (res.code === 10000) {
          wx.showToast({ title: '提交成功', icon: 'none' })
        } else {
          wx.showToast({ title: res.message, icon: 'none' })
        }
      })
    },
    //加载追加点评视图
    loadCommentView() {
      let that = this
      let rquest = {
        orderNo: that.orderNo
      }
      GetShop(rquest).then(res => {
        console.log(`加载追加点评视图`, res)
        that.orderShop = res.data.products
        that.commentContent = res.data.commentContent
        that.shopName = res.data.shopName
        that.createTime = res.data.createTime
        that.likeNum = res.data.likeNum
        that.carName = res.data.carName
        that.fileList = res.data.imgs
        that.CommentId = res.data.commentId
      })
    }
  },
  onLoad(options) {
    let that = this
    wx.getSystemInfo({
      success: function(res) {
        let clientHeight = res.windowHeight
        let clientWidth = res.windowWidth
        let ratio = 750 / clientWidth
        that.windowHeight = clientHeight * ratio + 'rpx'
      }
    })
    that.orderNo = options.orderNo
  },
  mounted() {
    this.loadCommentView()
  },
  onUnload() {
    this.textvalue = '' //用户评价内容
    this.orderShop = [] //订单信息
    this.commentContent = '' //点评内容
    this.shopName = '' //门店名称
    this.createTime = '' //点评时间
    this.likeNum = '' //点赞数
    this.carName = '' //服务车型
    this.shop = []
    ;(this.fileList = []), (this.orderNo = '') //上个页面传入的订单号
    this.CommentId = '' //上个页面传入的评论id
    this.srcs = `${this.globalData.imgPubUrl}Like - unchecked.png`
    // this.fabulousIndex = false
  }
}
</script>
<style scoped lang="scss">
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  background: #f3f3f3;
  padding-bottom: 126rpx;
  box-sizing: border-box;
}
.demo_shop {
  width: 100%;
  background: #f3f3f3;
  margin-bottom: 30rpx;
}
.shop_header {
  width: 100%;
  display: flex;
  padding: 30rpx;
  background: #fff;
}
.shop_img > img {
  width: 120rpx;
  height: 120rpx;
}
.shop_content_title {
  padding: 0 30rpx;
  font-size: 24rpx;
  color: #666666;
}
.shop_content_name {
  padding: 30rpx;
  font-size: 24rpx;
  color: #999999;
}
.shop_comment {
  width: 100%;
  background: #fff;
}
.shop_comment_content {
  padding: 0 30rpx 30rpx 30rpx;
  font-size: 28rpx;
  color: #333333;
}
.shop_comment_IDname {
  padding: 0 30rpx 30rpx 30rpx;
  font-size: 24rpx;
  color: #999999;
}
.shop_comment_bottom {
  display: flex;
}
.shop_comment_time {
  padding: 0 30rpx 30rpx 30rpx;
  font-size: 24rpx;
  color: #999999;
  flex: 5;
}
.shop_comment_like {
  flex: 1;
  font-size: 24rpx;
  color: #999999;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  img {
    width: 36rpx;
    height: 36rpx;
  }
  text {
    font-size: 24rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(153, 153, 153, 1);
  }
}
.shop_Writecomments {
  width: 100%;
  background: #fff;
  margin-top: 16rpx;
  padding: 30rpx;
  box-sizing: border-box;
}
.Ipt {
  width: 690rpx;
  background: #f3f3f3;
  padding: 30rpx 0;
}
.Ipt > textarea {
  width: 90%;
  height: 100rpx;
  margin: 0 auto;
}
.Obtain {
  width: 90%;
  margin: 0 auto;
  font-size: 20rpx;
  color: #999999;
  text {
    font-size: 20rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: #ff6234;
  }
}
.bottom_btn {
  width: 100%;
  background: #fff;
  position: fixed;
  bottom: 0;
  left: 0;
  padding: 10rpx 0;
}
.bottom_btn > p {
  text-align: center;
  width: 90%;
  height: 75rpx;
  background: #ff6324;
  color: #fff;
  margin: 16rpx auto;
  line-height: 75rpx;
  border-radius: 38rpx;
}
.add_img {
  width: 120rpx;
  height: 120rpx;
  margin: 30rpx;
  font-size: 20rpx;
  color: #999999;
}
.add_img::after {
  width: 200rpx;
  height: 200rpx;
  border: 1rpx solid red;
}
.add_img > img {
  width: 100%;
  height: 100%;
}
.nav_img {
  width: 200rpx;
  height: 200rpx;
  display: inline-block;
  position: relative;
  top: 0;
  left: 0;
  margin: 10rpx 0 10rpx 30rpx;
}
.nav_img > img {
  width: 100%;
  height: 100%;
}
.delte {
  position: absolute;
  right: 0;
  top: 0;
}
</style>