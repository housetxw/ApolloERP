<template>
  <div class="demo_page">
    <!-- 轮播 -->
    <swiper class="swiper" :autoplay="autoplay" :interval="interval" :duration="duration">
      <block v-for="(item, index) in images" :key="index">
        <swiper-item>
          <image :src="item" class="slide-image" mode="aspectFill" />
          <div class="top_tags1">
            <p
              class="metal1"
              v-for="(item1, index1) in brandNames"
              :key="index1"
              v-if="brandNames.length >=1"
              style="background:#FF6214;color:#fff;border:1rpx solid #FF6214;margin-top:6rpx;"
            >
              <img :src="item1.brandUrl" alt class="logo" />
              <text>{{item1.brand}}</text>
            </p>
          </div>
        </swiper-item>
      </block>
    </swiper>
    <!-- 门店信息 -->
    <div class="top_title">
      <div style="display:flex;">
        <p class="p1">{{storeTitle}}</p>
        <p class="top_tags">
          <text class="metal1" v-for="(item1, index1) in tagNames" :key="index1">{{item1}}</text>
        </p>
      </div>
      <div style="display:flex;color:#FF6324;font:24rpx/2 'SourceHanSansCN-Regular';width:100%;">
        <p style="margin-right:30rpx;">
          总评分:
          <span>{{storeBranch}}</span>
        </p>
        <!-- <p class="tx1">
          总订单:
          <span>{{storeOrder}}</span>
        </p>-->
      </div>
      <div class="butt1">{{type}}</div>
      <div class="top_content">
        <div class="top_left">
          <p class="p2">
            <img src="https://m.aerp.com.cn/mini-app-main/address1.png" alt class="img3" />
            <span class="s1">{{storeAddress}}</span>
          </p>
          <!-- <p class="p4">联系电话：{{storeNumber}}</p> -->

          <p class="p4">
            <img src="https://m.aerp.com.cn/mini-app-main/time1.png" alt class="img3" />
            <span>营业时间：{{storeTime}}</span>
          </p>
          <p class="p3">
            <text
              class="metal"
              v-for="(item2, index2) in servicearr"
              :key="index2"
            >{{item2.serviceName}}</text>
          </p>
        </div>
        <div class="top_right">
          <div @click.stop="navigate(latitude, longitude)" class="right_content">
            <img src="https://m.aerp.com.cn/mini-app-main/toAddress.png" alt class="img2" />
            <text class="txt3">路线</text>
          </div>
          <div @tap="telClick" class="right_content">
            <img src="https://m.aerp.com.cn/mini-app-main/toTell.png" alt class="img2" />
            <text class="txt3">电话</text>
          </div>
        </div>
      </div>
      <!-- <div class="top_p3">
        <p class="txt1">
          总评分：
          <span>{{storeBranch}}</span>
        </p>
        <p class="txt1 txt2">
          总订单：
          <span>{{storeOrder}}</span>
        </p>
        <p class="txt3">
          营业时间：
          <span>{{storeTime}}</span>
        </p>
      </div> -->
    </div>
    <!-- 九宫格 -->
    <div class="gridWrap">
      <van-grid column-num="2">
        <van-grid-item
          use-slot
          v-for="(item, index) in serveList"
          :key="index"
          link-type="navigateTo"
          @click="toService(item,index)"
        >
          <!--          <van-grid-item use-slot v-for="(item, index) in serveList" :key="index" link-type="navigateTo" >-->
          <div class="grid">
            <div class="gridLeft">
              <img :src="item.imageUrl" alt class="gridLeftImg" />
            </div>
            <div class="gridRight">
              <div class="gridRightTop">{{item.displayName}}</div>
              <div class="gridRightBot">{{item.description}}</div>
            </div>
          </div>
        </van-grid-item>
      </van-grid>
    </div>
    <!-- 门店技师 -->
    <div class="top_store">
      <div class="top_box_store">
        <p class="store_title">门店技师</p>
        <!-- <p class="technician_comment" @tap="technicianCommentClick">技师点评<img src="https://m.aerp.com.cn/mini-app-main/rightArrow.png" alt="" class="technician_img"></p> -->
      </div>
      <div class="store_box">
        <div
          class="store_one"
          v-for="(item1, index1) in storearr"
          :key="index1"
          @tap="technicianClick(item1)"
        >
          <div class="store_img">
            <img :src="item1.headImg" alt class="img1" />
          </div>
          <div class="store_text">
            <p class="store_p1">{{item1.name}}</p>
            <p class="store_p2">{{item1.techLevel}}</p>
          </div>
        </div>
      </div>
    </div>
    <!-- 门店服务项目 -->
    <!-- <div class="top_bottom" v-if="shopServiceType.length>0">
      <p class="p1">服务项目</p>
      <div class="tabTitle">
        <van-tabs
          :active="active"
          sticky
          swipeable
          color="#FF6324"
          class="tabs"
          title-active-color="#FF6324"
          @change="shopService"
        >
          <van-tab
            :title="itemorder.displayName"
            v-for="(itemorder,indexorder) in shopServiceType"
            :key="indexorder"
          >
            <div
              style="width:100%;"
              v-for="(itemm,indexx) in serviceList"
              v-if="serviceList.length>0"
            >
              <div class="tabContent">
                <img :src="itemm.imageUrl" alt class="tabImg" />
                <div class="tadDes">
                  <div @tap="toDetail(itemm,indexx)" style="width:100%;height:100%;">
                    <p class="desP1">{{itemm.displayName}}</p>
                    <div style="display:flex;justify-content:center;">
                      <p
                        v-for="(itemtag,indextag) in itemm.tags"
                        :key="indextag"
                        :style="{color:itemtag.tagColor,borderColor:itemtag.tagColor,}"
                        style="width:87rpx;height:30rpx;line-height:30rpx;text-align:center;font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;border-width:1rpx;border-style:solid;margin-right: 10rpx;border-radius: 6rpx;"
                      >{{itemtag.tag}}</p>
                    </div>
                    <p class="desP2">{{itemm.description}}</p>
                    <p class="desP3">¥{{itemm.price}}</p>
                  </div>
                  <p class="butt" @click="buy(itemm)">【测试】</p>
                </div>
              </div>
            </div>
            <div class="else1" v-if="serviceList.length==0">
              <img :src="src2" alt class="elseimg" />
              <p
                style="font-size:28rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(136,136,136,1);"
              >暂无{{itemorder.displayName}}项目</p>
            </div>
          </van-tab>
        </van-tabs>
      </div>
    </div>
    <div class="top_bottom" v-if="shopServiceType.length==0">
      <p class="p1">服务项目</p>

      <div class="else1">
        <img :src="src2" alt class="elseimg" />
        <p
          style="font-size:28rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(136,136,136,1);"
        >暂无推荐项目</p>
      </div>
    </div>-->
    <!-- <div class="top_bottom">

      <p class="p1">门店服务项目</p>
      <div class="bottom_one" v-for="(item2, index2) in servicearr" :key="index2">
        <div class="bottom_img">
          <img :src="item2.serviceIcon" alt class="img1" />
    <text class="txt1">{{item2.serviceName}}</text>-->
    <!-- 门店服务标签 -->
    <!-- <text :class="[{'biaoqian':item2.switchdis}]">{{item2.switchdis?item2.bq:''}}</text>
        </div>
        <text class="txt2" @tap="serviceClick(item2)">{{item2.fitName}}</text>
      </div>
    </div>-->
    <!-- 门店评价 -->
    <div class="evaluate">
      <text class="evaluate_left">门店评价 ({{commentNum}})</text>
      <text class="evaluate_right">好评率 : {{applauseRate}}%</text>
    </div>
    <!-- 门店标签 -->
    <div class="tag">
      <div
        class="taglist"
        v-for="(item,index) in list"
        :key="index"
      >{{item.labelName}}({{item.num}})</div>
    </div>
    <!-- 评论内容 -->
    <div class="comment" v-for="(item,index1) in comment" :key="index1">
      <div class="comment_headUrl">
        <img :src="item.headUrl" alt style="width:44rpx;height: 44rpx;" />
      </div>
      <div class="comment_comment">
        <div class="comment_header">
          <div class="comment_nickName">{{item.nickName}}</div>
          <div class="comment_vip">
            <img :src="Url0" alt v-if="item.memberGrade == 0" />
            <img :src="Url10" alt v-if="item.memberGrade == 10" />
            <img :src="Url20" alt v-if="item.memberGrade == 20" />
            <img :src="Url30" alt v-if="item.memberGrade == 30" />
            <img :src="Url40" alt v-if="item.memberGrade == 40" />
            <img :src="Url50" alt v-if="item.memberGrade == 50" />
          </div>
          <div class="comment_carName">服务车型：{{item.carName}}</div>
        </div>
        <van-rate v-model="item.score" readonly />
        <div class="comment_section">
          <div class="comment_title">{{item.content}}</div>
        </div>
        <div class="comment_section_img">
          <div class="comment_title_img" v-for="(items,index2) in item.imageUrls" :key="index2">
            <img :src="items" alt />
          </div>
        </div>
        <div class="comment_header">
          <div class="comment_shopName">店铺名称：{{item.shopName}}</div>
        </div>
        <!-- <div class="comment_createTime_top">{{item.createTime}}</div> -->
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
        <div class="comment_footer">
          <div class="comment_createTime">{{item.createTime}}</div>
          <!-- <div class="zan">
            <div class="comment_zan_img" v-for="(itemb,indexb) in item.commArr" :key="indexb">
              <img :src="itemb.isTrue?itemb.url:itemb.srcs" alt @click="back(item,indexb)" />
            </div>
            <div class="comment_likeNum">{{item.likeNum}}</div>
          </div>-->
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import {
  GetEvaluation,
  GetShopList,
  GetShopComment,
  GetLike,
  GetStoresDetails,
  GetShopServiceProject,
  GetWxacodeScene,
  GetWxOpenId
} from '../../api'
import eventBus from '../../utils/eventBus.js'

export default {
  data() {
    return {
      brandNames: [],
      serveList: [],
      storeArr1: {},
      src2: `${this.globalData.imgPubUrl}mine_appointment_null.png`,
      topIndex: 1,
      serviceList: [],
      type: '', //门店类型
      productArr: ['保养养护', '美容洗车', '轮胎服务', '其他服务'], // vant标题数组
      active: 0,
      latitude: '',
      longitude: '',
      images: [], // 轮播图数组
      storeTitle: '', // 门店标题
      storeAddress: '', // 门店地址
      storeNumber: '', // 门店联系电话
      storeDistance: '', // 距离门店距离
      storeBranch: '', // 总评分
      storeOrder: '', // 总订单
      shopServiceType: [], //服务项目
      tagNames: '', //标签
      storeTime: '', // 营业时间
      autoplay: true,
      interval: 3000,
      duration: 500,
      storearr: [], // 技师数组
      servicearr: [], // 门店服务
      list: [], // 门店评价
      comment: [],
      Url0: `${this.globalData.imgPubUrl}minevip_vip_zero.png`,
      Url10: `${this.globalData.imgPubUrl}minevip_vip_one.png`,
      Url20: `${this.globalData.imgPubUrl}minevip_vip_two.png`,
      Url30: `${this.globalData.imgPubUrl}minevip_vip_three.png`,
      Url40: `${this.globalData.imgPubUrl}minevip_vip_four.png`,
      Url50: `${this.globalData.imgPubUrl}minevip_vip_five.png`,
      commentNum: '', // 商品评价
      applauseRate: '', // 好评率
      storeShop: '', // 上个页面传过来的门店id
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
    buy(item) {
      let that = this
      that.globalData.serviceType = 'toShop'
      that.globalData.ProductType = 0
      if (that.topIndex == 1) {
        that.globalData.OrderType = 2
      } else if (that.topIndex == 2) {
        that.globalData.OrderType = 1
      } else if (that.topIndex == 3) {
        that.globalData.OrderType = 3
      } else if (that.topIndex == 4) {
        that.globalData.OrderType = 4
      } else if (that.topIndex == 5) {
        that.globalData.OrderType = 5
      } else if (that.topIndex == 6) {
        that.globalData.OrderType = 6
      }
      console.log(2233, that.storeAddress)
      that.storeArr1['address'] = that.storeAddress
      that.storeArr1.simpleName = that.storeTitle
      that.storeArr1.img = that.images[0]
      that.productInfos = [
        { pid: item.productCode, number: 1, productOwnAttri: 1 }
      ]
      let carId = ''
      if (that.$store.state.carArr[0]) {
        carId = that.$store.state.carArr[0].carId
      }
      that.$router.push({
        path: '/pages/orderSure/main',
        query: {
          cardID: carId,
          storeid: that.storeShop,
          productInfos: JSON.stringify(that.productInfos),
          orderType: 2,
          storeArr: JSON.stringify(that.storeArr1),
          isFromStore: true
        }
      })
    },
    //跳转商品详情页
    toDetail(itemm, indexx) {
      console.log(1111, '跳转详情页')
      let that = this
      this.$router.push({
        path: '/pages/goodsDetails/main',
        query: {
          productCode: itemm.productCode,
          shopId: that.storeShop,
          telephone: this.storeNumber,
          simpleName: this.storeTitle,
          shopaddress: this.storeAddress
        }
      })
    },
    //获取门店服务项目
    shopService(event) {
      let that = this
      that.globalData.serviceType = 'toShop'
      that.globalData.ProductType = 0
      that.topIndex = event.mp.detail.index + 1
      if (that.topIndex == 1) {
        that.globalData.OrderType = 2
      } else if (that.topIndex == 2) {
        that.globalData.OrderType = 1
      } else if (that.topIndex == 3) {
        that.globalData.OrderType = 3
      } else if (that.topIndex == 4) {
        that.globalData.OrderType = 4
      } else if (that.topIndex == 5) {
        that.globalData.OrderType = 5
      } else if (that.topIndex == 6) {
        that.globalData.OrderType = 6
      }
      console.log('topIndex', that.topIndex)
      this.getShopServiceProject(that.topIndex, this.storeShop)
    },
    // 技师点击事件
    technicianClick(item1) {
      // return false
      console.log(`技师数组`, item1)
      this.$router.push({
        path: '/pages/TechnicianEvaluation/main',
        query: {
          EmployeeId: item1.techId
        }
      })
    },

    technicianCommentClick() {
      this.$router.push({
        path: '/pages/TechnicianEvaluation/main',
        query: {
          EmployeeId: this.storearr[0].techId
        }
      })
    },
    // 导航图标
    navigate(latitude, longitude) {
      wx.openLocation({
        latitude,
        longitude,
        scale: 18,
        name: this.storeTitle,
        address: this.storeAddress,
        success: res => {
          console.log(`打开位置成功`, res)
        }
      })
    },
    // 点赞
    back(item, indexb) {
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
    // 选择按钮
    selectshop() {
      console.log(`选择按钮`)
    },
    telClick() {
      wx.showModal({
        content: this.storeNumber,
        confirmText: '呼叫',
        confirmColor: '#FF6324',
        success(res) {
          if (res.confirm) {
            wx.makePhoneCall({
              phoneNumber: this.storeNumber
            })
          } else if (res.cancel) {
          }
        }
      })
    },
    // 跳转服务页面
    toService(item, index) {
      let that = this
      that.globalData.serviceType = 'toShop'
      that.globalData.ProductType = 0
      that.globalData.OrderType = item.orderType
      this.$router.push({
        path: '/pages/BgoodsList/main',
        query: {
          // telephone: this.telephone,
          // CategoryId: item.serviceTypeId,
          // serviceName: item.displayName,
          // serviceCode: item.serviceType,
          // shopid: this.shopid
          telephone: this.telephone,
          simpleName: this.storeTitle,
          shopaddress: this.storeAddress,
          imgs: this.images,
          CategoryId: item.serviceTypeId,
          // serviceName: item.displayName,
          // serviceCode: item.serviceType,
          shopid: this.storeShop
        }
      })
    },
    // 门店服务项目按钮
    serviceClick(item2) {
      this.$router.push(item2.fitUrl)
    },
    // 门店服务项目列表
    getShopServiceProject(serviceTypeId, ShopId) {
      let that = this
      let rquest = {
        ShopId: ShopId,
        CategoryId: serviceTypeId
      }
      GetShopServiceProject(rquest)
        .then(res => {
          that.serviceList = res.data
        })
        .catch(err => {
          console.log(`获取门店服务项目列表失败函数,${err}`)
        })
    },
    // 获取门店评价数量和好评率
    getStoreEvaluate(ShopId) {
      let that = this
      let rquest = {
        ShopId: ShopId
      }
      GetEvaluation(rquest)
        .then(res => {
          that.commentNum = res.data.commentNum
          that.applauseRate = res.data.applauseRate
        })
        .catch(err => {
          console.log(`获取门店评价数量和好评率失败函数,${err}`)
        })
    },
    // 获取门店评价标签列表
    getStoreList(PageIndex, PageSize, ShopId) {
      let that = this
      let rquest = {
        PageIndex: PageIndex,
        PageSize: PageSize,
        ShopId: ShopId
      }
      GetShopList(rquest)
        .then(res => {
          that.list = res.data.items
        })
        .catch(err => {
          console.log(`/获取门店评价标签列表失败函数,${err}`)
        })
    },
    // 获取门店评价列表
    getStoreEvalList(PageIndex, PageSize, ShopId) {
      let that = this
      let rquest = {
        PageIndex: PageIndex,
        PageSize: PageSize,
        ShopId: ShopId
      }
      GetShopComment(rquest)
        .then(res => {
          let arr = res.data.items
          arr.forEach(element => {
            element.commArr = JSON.parse(JSON.stringify(that.commArr))
            element.commDis = false // 点赞所需变量
            // element.moreIndex = false //更多所需变量
          })
          that.comment = arr
          // that.comment = res.data.items
        })
        .catch(err => {
          console.log(`获取门店评价列表失败函数,${err}`)
        })
    },
    // 获取门店详情
    getStoreDetails(ShopId, Longitude, Latitude) {
      let that = this
      let shopdata = {
        ShopId: ShopId,
        Longitude: Longitude,
        Latitude: Latitude
      }
      GetStoresDetails(shopdata)
        .then(res => {
          that.brandNames = res.data.brandNames
          that.storeTitle = res.data.simpleName // 门店标题
          that.storeAddress = res.data.address // 门店地址
          that.storeNumber = res.data.telephone // 门店联系电话
          that.storeDistance = res.data.distance // 距离门店距离
          that.storeBranch = res.data.score // 总评分
          that.storeOrder = res.data.totalOrder // 总订单
          that.shopServiceType = res.data.shopServiceType //服务项目
          that.tagNames = res.data.tagNames //标签
          that.type = res.data.type
          that.storeTime = res.data.workTime // 营业时间
          that.images = res.data.imgs // 轮播图
          that.storearr = res.data.techs // 技师数组
          that.servicearr = res.data.shopServices // 门店服务
          that.longitude = res.data.longitude // 门店服务
          that.latitude = res.data.latitude // 门店服务
          that.serveList = res.data.shopServiceType
        })
        .catch(err => {
          console.log(`获取门店详情失败函数,${err}`)
        })
    }
  },
  mounted() {},
  onLoad(options) {
    if (options.scene) {
      this.scene = options.scene
    } else {
      this.storeShop = options.id
      this.latitude = options.latitude
      this.longitude = options.longitude
    }
  },
  onUnload() {
    this.latitude = ''
    this.longitude = ''
    this.images = [] // 轮播图数组
    this.storeTitle = '' // 门店标题
    this.storeAddress = '' // 门店地址
    this.storeNumber = '' // 门店联系电话
    this.storeDistance = '' // 距离门店距离
    this.storeBranch = '' // 总评分
    this.storeOrder = '' // 总订单
    this.storeTime = '' // 营业时间
    this.storearr = [] // 技师数组
    this.servicearr = [] // 门店服务
    this.list = [] // 门店评价
    this.comment = []
    this.commentNum = '' // 商品评价
    this.applauseRate = '' // 好评率
    this.storeShop = '' // 上个页面传过来的门店id
  },
  onShow() {
    let that = this
    if (this.scene) {
      const scene = this.scene
      GetWxacodeScene({ SceneId: scene })
        .then(res => {
          if (res.code === 10002) {
            wx.showModal({
              title: '提示',
              content: '扫码解析错误将为您定位到附近门店',
              showCancel: false,
              success(res) {
                if (res.confirm) {
                  that.$router.push({ path: '/pages/index/main', isTab: true })
                }
              }
            })
            return false
          }

          that.storeShop = res.data.shopId
          wx.getLocation({
            type: 'gcj02',
            altitude: true, // 高精度定位
            // 定位成功，更新定位结果
            success: res3 => {
              that.latitude = res3.latitude
              that.longitude = res3.longitude
              this.getStoreEvaluate(this.storeShop)
              this.getShopServiceProject(1, this.storeShop)

              this.getStoreList(1, 10, this.storeShop)
              this.getStoreEvalList(1, 30, this.storeShop)
              this.getStoreDetails(
                this.storeShop,
                this.longitude,
                this.latitude
              )
            },
            // 定位失败回调
            fail() {
              that.latitude = 0
              that.longitude = 0
              wx.showToast({
                title: '定位失败',
                icon: 'none'
              })
              this.getStoreEvaluate(this.storeShop)
              this.getShopServiceProject(1, this.storeShop)

              this.getStoreList(1, 10, this.storeShop)
              this.getStoreEvalList(1, 30, this.storeShop)
              this.getStoreDetails(
                this.storeShop,
                this.longitude,
                this.latitude
              )
            },

            complete() {
              // 隐藏定位中信息进度
              wx.hideLoading()
            }
          })
        })
        .catch(() => {
          wx.showModal({
            title: '提示',
            content: '扫码解析错误将为您定位到附近门店',
            showCancel: false,
            success(res) {
              if (res.confirm) {
                that.$router.push({ path: '/pages/index/main', isTab: true })
              }
            }
          })
        })
    } else {
      eventBus.$emit('isShow1', false)
      this.getStoreEvaluate(this.storeShop)
      this.getShopServiceProject(1, this.storeShop)

      this.getStoreList(1, 10, this.storeShop)
      this.getStoreEvalList(1, 30, this.storeShop)
      this.getStoreDetails(this.storeShop, this.longitude, this.latitude)
    }
  }
}
</script>
<style scoped lang="scss">
.gridWrap {
  box-shadow: 0px 5rpx 16rpx 0px rgba(204, 204, 204, 0.5);
  border-radius: 10rpx;
  margin-top: 20rpx;

  .grid {
    display: flex;
    align-items: center;

    .gridLeft {
      width: 113rpx;
      display: flex;
      justify-content: center;

      .gridLeftImg {
        left: 50%;
        transform: tranlateX(-50%);
        width: 66rpx;
        height: 66rpx;
      }
    }

    .gridRight {
      width: 232rpx;

      .gridRightTop {
        font-size: 30rpx;
        font-family: Source Han Sans CN;
        font-weight: bold;
        color: rgba(51, 51, 51, 1);
        margin-bottom: 20rpx;
      }

      .gridRightBot {
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(166, 165, 174, 1);
      }
    }
  }
}
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #f3f3f3;
}

/* 轮播类名 */
.swiper {
  width: 100%;
  height: 400rpx !important;
}
.slide-image {
  height: 100%;
  width: 100%;
}
.swiper_text {
  position: absolute;
  top: 290rpx;
  left: 0;
  width: 100%;
  padding-bottom: 30rpx;
  padding-right: 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: flex-end;
  align-items: flex-end;
}
.swiper_text2 {
  width: 75rpx;
  height: 75rpx;
  background: #212121;
  opacity: 0.5;
  border-radius: 50%;
  font-size: 36rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 255, 255, 1);
  text-align: center;
  line-height: 75rpx;
}

.top_title {
  width: 100%;
  min-height: 360rpx;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  // align-items: center;
  padding: 30rpx;
  box-sizing: border-box;
}
.top_title .p1 {
  // width: 100%;
  max-width: 360rpx;
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 600;
  color: rgba(50, 50, 50, 1);
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  margin-right: 20rpx;
}
.top_content {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.top_left {
  min-height: 90rpx;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
  .img3 {
    width: 32rpx;
    height: 32rpx;
  }
}
.top_left .p2 {
  width: 350rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);

  display: flex;
  align-items: center;
  .s1 {
    overflow: hidden;
    white-space: pre-wrap;
    display: -webkit-box;
    text-overflow: ellipsis;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
    width: 300rpx;
  }
}
.top_left .p4 {
  font-size: 22rpx;
  font-family: Adobe Heiti Std;
  font-weight: normal;
  color: rgba(136, 136, 136, 1);
  display: flex;
  align-items: center;
}
.top_right {
  width: 274rpx;
  min-height: 90rpx;
  display: flex;
  // flex-direction: column;
  justify-content: space-between;
  align-items: center;
  border-left: 2rpx solid #e2e2e2;
  padding: 0 20rpx 0 60rpx;
  margin-top: -10rpx;

  box-sizing: border-box;
}
.right_content {
  display: flex;
  flex-direction: column;
  // justify-content: space-between;
  align-items: center;
}
.metal {
  padding: 0 10rpx;
  box-sizing: border-box;
  height: 30rpx;
  border: 1rpx solid rgba(255, 99, 36, 1);
  border-radius: 4rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
  text-align: center;
  line-height: 28rpx;
  margin-right: 10rpx;
}
.img2 {
  width: 48rpx;
  height: 48rpx;
}
.txt3 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
}
.top_p3 {
  width: 100%;
  height: 80rpx;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  border-top: 1rpx solid #f3f3f3;
}
.top_p3 .txt1 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(33, 33, 33, 1);
}
.top_p3 .txt1 span {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 174, 0, 1);
}
.top_p3 .txt3 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
}
.top_p3 .txt3 span {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(33, 33, 33, 1);
}
.top_store {
  width: 100%;
  min-height: 260rpx;
  background: #ffffff;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
  padding: 30rpx 0 30rpx 30rpx;
  box-sizing: border-box;
  margin: 16rpx 0;
}
.top_box_store {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  .store_title {
    width: 200rpx;
    font-size: 30rpx;
    font-family: Source Han Sans CN;
    font-weight: bold;
    color: rgba(51, 51, 51, 1);
  }
  .technician_comment {
    font-size: 24rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(255, 89, 20, 1);
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
    .technician_img {
      width: 32rpx;
      height: 32rpx;
    }
  }
}
.store_box {
  width: 100%;
  display: -webkit-box;
  -webkit-overflow-scrolling: touch;
  -ms-overflow-style: none;
  overflow: -moz-scrollbars-none;
  overflow-x: scroll;
}
.store_one {
  width: 280rpx;
  min-height: 120rpx;
  box-shadow: 0rpx 5rpx 16rpx 0rpx rgba(204, 204, 204, 0.5);
  border-radius: 10rpx;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  padding: 20rpx;
  box-sizing: border-box;
  margin: 5rpx 20rpx 5rpx 0;
}
.store_img {
  width: 100rpx;
  height: 80rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.store_img .img1 {
  width: 80rpx;
  height: 80rpx;
  border-radius: 40rpx;
}
.store_text {
  width: 160rpx;
  height: 80rpx;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: flex-start;
  margin-left: 20rpx;
}
.store_text .store_p1 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(33, 33, 33, 1);
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 1;
  -webkit-box-orient: vertical;
  width: 160rpx;
}
.store_text .store_p2 {
  width: 170rpx;
  height: 32rpx;
  background: rgba(255, 234, 234, 1);
  border-radius: 4rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(254, 71, 71, 1);
  text-align: left;
}
.top_bottom {
  width: 100%;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  padding: 30rpx;
  box-sizing: border-box;
}
.top_bottom .p1 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(51, 51, 51, 1);
  line-height: 100rpx;
  border-bottom: 2rpx solid #eee;
  width: 100%;
}
.bottom_one {
  width: 100%;
  height: 100rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.bottom_img {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}
.bottom_img .img1 {
  width: 58rpx;
  height: 58rpx;
}
.bottom_img .txt1 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 500;
  color: rgba(52, 52, 52, 1);
  margin-left: 25rpx;
}
.bottom_img .biaoqian {
  width: 87rpx;
  height: 30rpx;
  background: rgba(255, 238, 231, 1);
  border-radius: 4rpx;
  text-align: center;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
  margin-left: 15rpx;
}
.bottom_one .txt2 {
  width: 170rpx;
  height: 60rpx;
  background: rgba(255, 99, 36, 1);
  border-radius: 30rpx;
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 255, 255, 1);
  text-align: center;
  line-height: 60rpx;
}
/* 门店评价 */
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
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-wrap: wrap;
  box-sizing: border-box;
}
.tag > .taglist {
  flex-shrink: 0;
  height: 57rpx;
  margin: 0 7rpx 35rpx 7rpx;
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
  padding-top: 0;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-wrap: wrap;
  box-sizing: border-box;
}
.comment_comment {
  flex: 5;
  padding: 0 18rpx;
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
  padding-right: 40rpx;
}
.comment_title {
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
  width: 100%;
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 4;
  -webkit-box-orient: vertical;
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
  flex-direction: row;
  justify-content: flex-end;
  align-items: center;
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
.review_content {
  padding-right: 60rpx;
}
.review_content2 {
  background: rgba(242, 242, 242, 1);
  margin: 30rpx 60rpx 0 0;
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
.btn {
  width: 100%;
  background: #fff;
  margin-top: 30rpx;
}
.btn > button {
  width: 700rpx;
  margin: 15rpx 30rpx;
  border-radius: 38rpx;
  background: #ff6324;
  color: #fff;
}
.tabTitle {
  width: 100%;
  display: flex;
  flex-direction: column;
}
.tabContent {
  width: 100%;
  display: flex;
  align-items: center;
  padding: 30rpx 10rpx;
  border-bottom: 2rpx solid #ebebeb;
}
.tabImg {
  height: 184rpx;
  width: 184rpx;
  margin-right: 25rpx;
}
.tadDes {
  display: flex;
  flex-direction: column;
  // align-items: center;
  width: 450rpx;
  height: 184rpx;
  position: relative;

  .desP1 {
    color: #323232;
    font: 30rpx/2 'SourceHanSansCN-Medium';
  }
  .desP2 {
    color: #999999;
    font: 22rpx/2 'SourceHanSansCN-Medium';

    overflow: hidden;
    white-space: pre-wrap;
    display: -webkit-box;
    text-overflow: ellipsis;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
    width: 350rpx;
  }
  .desP3 {
    color: #d80519;
    font: 30rpx/2 'SourceHanSansCN-Medium';
    margin-top: 10rpx;
    position: absolute;
    bottom: -10rpx;
  }
}
.butt {
  width: 110rpx;
  height: 60rpx;
  background: rgba(255, 99, 36, 1);
  border-radius: 30rpx;
  color: #fff;
  font: 26rpx/1 'SourceHanSansCN-Regular';
  display: flex;
  justify-content: center;
  align-items: center;
  position: absolute;
  bottom: 5rpx;
  right: 10rpx;
}
.butt1 {
  width: 110rpx;
  min-height: 42rpx;
  background: #ff6324;
  border-radius: 23rpx;
  color: #fff;
  font: 26rpx/1 'SourceHanSansCN-Regular';
  display: flex;
  justify-content: center;
  align-items: center;
  margin: 20rpx 0;
  padding: 2rpx;
}
.top_tags {
  // margin: 20rpx 0;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  width: 360rpx;
}
.metal1 {
  padding: 2rpx 4rpx;
  box-sizing: border-box;
  border: 1rpx solid #ff6214;
  border-radius: 15rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: #ff6214;
  text-align: center;
  margin-top: 10rpx;
  margin-right: 10rpx;
  height: 45rpx;
}
>>> .van-tabs__wrap--scrollable .van-tab {
  flex: none !important;
  padding: 0 30rpx;
}
.else1 {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.elseimg {
  width: 150rpx;
  height: 150rpx;
  margin-top: 30rpx;
}
>>> .van-tabs__line {
  z-index: -1 !important;
  opacity: 0 !important;
}
.top_tags1 {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  position: absolute;
  bottom: 50rpx;
  left: 40rpx;
  right: 0;
  margin-top: 5rpx;
}

.logo {
  width: 30rpx;
  height: 30rpx;
  margin: 0 5rpx;
}
.metal1 {
  padding: 2rpx 5rpx 2rpx 5rpx;
  box-sizing: border-box;
  border: 1rpx solid #1ab394;
  border-radius: 10rpx;
  font-size: 25rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: #1ab394;
  text-align: center;
  margin-right: 10rpx;
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>