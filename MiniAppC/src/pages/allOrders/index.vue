<template>
  <div class="demo_page">
    <!-- <navigation-bar
      :title="videoTitle"
      :titleColor="'green'"
      :back-visible="true"
      :home-path="'/pages/index/main'"
      @test="test"
    ></navigation-bar>-->
    <!-- 顶部折叠 -->
    <div class="order_boxs" v-if="switcDisplay" @tap="clearClick">
      <div class="order_allboxs">
        <div
          :class="['order_boxs_one',{'bgorder':orderIndex == activeIndex?true:false}]"
          @tap="orderTitleClick(order,orderIndex)"
          v-for="(order,orderIndex) in boxOrder"
          :key="orderIndex"
        >{{order}}</div>
      </div>
    </div>
    <!-- 内容 -->
    <div class="title_tab">
      <van-tabs
        :active="active"
        sticky
        swipeable
        color="#FF6324"
        line-width="40"
        class="tabs"
        @change="onChangeTitle"
        v-if="tabsShow"
      >
        <van-tab
          :title="itemorder"
          v-for="(itemorder,indexorder) in orderListArr"
          :key="indexorder"
        >
          <div class="title_tab_content" :style="{minHeight:windowHeight1}">
            <!-- 有订单显示内容 -->
            <div class="box" v-if="orderAllarr.length > 0">
              <div class="tab1" v-for="(item1,index1) in orderAllarr" :key="index1">
                <!-- 订单编号 -->
                <div class="tab1_title">
                  <text class="txt1">订单编号：{{item1.orderNo}}</text>
                  <text class="txt2">{{item1.displayOrderStatus}}</text>
                </div>
                <!-- 套餐 -->
                <div
                  v-if="item1.isCollapseShowProducts&&item1.products.length<=5"
                  @tap="orderClickall(item1)"
                  style="width:100%"
                >
                  <div class="top_two">
                    <div class="scroll">
                      <div class="top_two_box">
                        <img
                          :src="item.imageUrl"
                          alt
                          mode="aspectFit"
                          class="img"
                          v-for="(item, index) in item1.products"
                          :key="index"
                        />
                        <div class="position1">共{{item1.products.length}}件</div>
                      </div>
                    </div>
                  </div>
                  <!-- <div class="imgListMoney">
                    <text class="txt1">共{{item1.listTotalProductNum}}件</text>
                    <text class="txt2">￥{{item1.actualAmount}}</text>
                  </div>-->
                </div>
                <div
                  v-if="item1.isCollapseShowProducts&&item1.products.length>5"
                  @tap="orderClickall(item1)"
                >
                  <!-- 商品图片 -->
                  <div class="top_two">
                    <div class="scroll">
                      <div class="top_two_box">
                        <img
                          :src="item.imageUrl"
                          alt
                          mode="widthFix"
                          class="img"
                          v-for="(item, index) in item1.products"
                          :key="index"
                        />
                      </div>
                    </div>
                    <div class="position">共{{item1.products.length}}件</div>
                  </div>
                  <!-- <div class="imgListMoney">
                    <text class="txt1">共{{item1.listTotalProductNum}}件</text>
                    <text class="txt2">￥{{item1.actualAmount}}</text>
                  </div>-->
                </div>
                <div v-if="item1.products.length==1" @tap="orderClickall(item1)">
                  <!-- 商品未折叠 -->
                  <div
                    class="tab1_contetn"
                    v-for="(itemaa,indexaa) in item1.products"
                    :key="indexaa"
                  >
                    <div class="tab1-images">
                      <img :src="itemaa.imageUrl" alt mode="aspectFit" class="tab1_img" />
                    </div>
                    <div class="tab1_top">
                      <p class="p1">{{itemaa.productName}}</p>
                      <p class="p2">
                        <text class="txt1">共{{item1.listTotalProductNum}}件</text>
                        <text class="txt2">￥{{item1.actualAmount}}</text>
                      </p>
                    </div>
                  </div>
                </div>
                <!-- 按钮 -->
                <div class="btn" v-if="item1.orderUserOperations.length>0">
                  <van-button
                    class="button_op"
                    round
                    size="small"
                    plain
                    hairline
                    :color="itema.isImportance?'#FF6324':'#ADADAD'"
                    type="default"
                    v-for="(itema,indexa) in item1.orderUserOperations"
                    :key="indexa"
                    @tap="orderBtnClick(itema,item1)"
                  >{{itema.showFunctionName}}</van-button>
                  <!--v-if="itema.showFunctionName != '评价订单'"-->
                </div>
              </div>
            </div>
            <!-- 无订单内容显示东西 -->
            <div class="else" v-else :style="{height:windowHeight}">
              <img :src="src2" alt mode="widthFix" class="elseimg" />
              <p
                style="font-size:32rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(136,136,136,1);"
              >无订单</p>
            </div>
          </div>
        </van-tab>
      </van-tabs>
    </div>
    <!-- 取消原因弹窗 -->
    <div class="clearC" v-if="showOrder">
      <van-popup
        :show="showOrder"
        closeable
        @close="onClose"
        position="bottom"
        lock-scroll
        custom-style="height: 75%;"
      >
        <div class="popup_header">取消原因</div>
        <van-radio-group v-model="radio">
          <van-cell-group>
            <van-cell
              :title="itemreason.reason"
              clickable
              @tap="clearClickProup(itemreason)"
              v-for="(itemreason,indexreason) in reasonArr"
              :key="indexreason"
            >
              <van-radio slot="right-icon" checked-color="#FF6324" :name="itemreason.id" />
            </van-cell>
          </van-cell-group>
        </van-radio-group>
        <div
          style="width:100%;height:300rpx;padding: 30rpx;box-sizing: border-box;margin-top: 28rpx;"
          v-if="radio == '4'"
        >
          <div class="popup_inp">
            <textarea
              rows="3"
              class="tareaText"
              :value="textvalue"
              @input="bindTextAreaBlur"
              placeholder-style="color:rgba(102,102,102,1);font-size:28rpx;font-weight:400;"
              maxlength="50"
              placeholder="请输入原因，50字以内"
              style="padding: 30rpx"
            ></textarea>
          </div>
        </div>
        <div v-else style="width:100%;height:300rpx;"></div>
        <div class="popup_btn">
          <button @tap="reasionClick">确定</button>
        </div>
      </van-popup>
    </div>
    <!-- <div class="positionTOP" v-show="searchLoading">正在加载中...</div> -->

    <div class="positionTOP" v-show="searchLoading">{{bmessage}}</div>
    <div class="positionTOP" v-show="searchLoading1">{{bmessage1}}</div>
    <div style="margin-bottom:10rpx;height:10rpx;width:100%;"></div>
  </div>
</template>
<script>
import navigationBar from '../../components/navigationBarone.vue'
import {
  GetOrderList,
  BuyAgain,
  ConfirmPay,
  ClosePay,
  GetClearReasion,
  PostSureOrder,
  GetShop
} from '../../api'

var pageActiveIndex = 1
export default {
  components: {
    navigationBar
  },
  data() {
    return {
      showglobalData: false,
      PageIndex: 1,
      PageSize: 10,
      ProductType: -1,
      tabsShow: false,
      canPay: true,
      radio: 1, // 取消原因变量
      showOrder: false, // 弹窗取消订单
      reasonArr: [], // 取消原因数组
      textvalue: '', // 用户输入取消原因文字
      shopnumorderNo: '', // 点击取消订单按钮的时候存的订单编号
      disswitch: true,
      switcDisplay: false,
      videoTitle: '全部订单',
      orderListArr: ['全部', '待付款', '待收货', '待安装', '待评价'], // vant标题数组
      active: 0,
      activeIndex: 0,
      src2: `${this.globalData.imgPubUrl}mine_appointment_null.png`, // 暂无订单图片
      windowHeight: '',
      windowHeight1: '',
      orderAllarr: [], // 全部订单
      items: [], // 后台返回数组
      boxOrder: ['全部订单', '轮胎服务', '保养养护', '美容洗车', '上门服务'], // 顶部折叠数组
      topIndex: 0, // 顶部tabbar切换的索引值
      PullDownRefresh: false, // 下拉刷新的变量
      searchLoading: false, // 上拉加载变量
      searchLoading1: false, // 上拉加载变量
      bmessage: '',
      bmessage1: ''
    }
  },
  onReachBottom() {
    if (this.searchLoading == true) {
      return false
    } else {
      let that = this
      that.searchLoading1 = true
      that.bmessage1 = '正在加载中...'
      let OrderType = -1
      if (that.activeIndex == 4) {
        OrderType = 0
      } else {
        OrderType = that.activeIndex
      }
      // let OrderType = that.activeIndex

      let OrderListStatus = that.topIndex
      // PageIndex: that.PageIndex * 1 + 1,
      let PageIndex = that.PageIndex
      let PageSize = that.PageSize
      that.interfaceFunction(OrderType, OrderListStatus, PageIndex, PageSize)
    }
  },
  methods: {
    confirmPay(orderNo, amount) {
      if (!this.canPay) {
        return false
      }
      this.canPay = false
      let footValue = {
        innerNoType: 'OrderNo',
        orderNo,
        buyerAccount: this.globalData.openId,
        amount
      }
      ConfirmPay(footValue)
        .then(res => {
          let that = this
          let {
            timeStamp,
            nonceStr,
            signType,
            paySign
          } = res.data.wxMiniAppPayCallData
          let package1 = res.data.wxMiniAppPayCallData.package
          let PayNo = res.data.payNo
          if (res.data.arousePayment) {
            wx.requestPayment({
              timeStamp,
              nonceStr,
              package: package1,
              signType,
              paySign,
              success(res) {
                // that.$router.push('/pages/allOrders/main')

                that.interfaceFunction(
                  that.globalData.OrderType,
                  that.globalData.orderListStatus,
                  1,
                  100
                )
              },
              fail(res) {
                ClosePay({ PayNo }).then(() => {
                  that.canPay = true
                })
                // that.PageIndex = 1
                // that.PageSize = 10
                // that.searchLoading = false
                // that.bmessage = ''
                // that.searchLoading1 = false
                // that.bmessage1 = ''
                // that.interfaceFunction(that.globalData.OrderType, that.globalData.orderListStatus, 1, 100)
              }
            })
          } else {
            // that.canPay = true
            // that.PageIndex = 1
            // that.PageSize = 10
            // that.searchLoading = false
            // that.bmessage = ''
            // that.searchLoading1 = false
            // that.bmessage1 = ''
            // that.interfaceFunction(that.globalData.OrderType, that.globalData.orderListStatus, 1, 100)
          }
        })
        .catch(err => {
          this.canPay = true
          console.log(`试算金额错误信息${err}`)
        })
    },
    buyAgain(orderNo, actualAmount) {
      BuyAgain({
        orderNo
      }).then(res => {
        if (res.code == 10000) {
          this.confirmPay(res.data.orderNo, actualAmount)
          wx.showToast({ title: '提交成功', icon: 'none' })
          // that.$router.push({ path: '/pages/allOrders/main', reLaunch: true })
        } else {
          wx.showToast({ title: res.code, icon: 'none' })
        }
      })
    },
    clearClickProup(itemreason) {
      this.radio = itemreason.id
      console.log(this.radio)
    },
    // 取消原因备注
    bindTextAreaBlur(e) {
      this.textvalue = e.mp.detail.value
    },
    onClose() {
      this.showOrder = false
    },
    // 取消原因提交按钮
    reasionClick() {
      let that = this
      let rquest = {
        orderNo: that.shopnumorderNo,
        reasonId: that.radio,
        instruction: that.textvalue
      }
      // 取消原因提交按钮
      PostSureOrder(rquest)
        .then(res => {
          wx.showToast({ title: '取消成功', icon: 'none' })
          that.showOrder = false
          that.searchLoading = false
          that.PageIndex = 1
          that.interfaceFunction(0, that.topIndex, 1, 300)
        })
        .catch(err => {
          wx.showToast({ title: '取消失败', icon: 'none' })
          that.showOrder = false
        })
    },
    // 获取退货数组
    GetShop() {
      let that = this
      let rquest = {
        ApplyType: 'Cancel'
      }
      // 获取申请原因集合
      GetClearReasion(rquest)
        .then(res => {
          that.reasonArr = res.data
        })
        .catch(err => {})
    },
    // 顶部折叠数组点击事件
    orderTitleClick(order, orderIndex) {
      let that = this
      that.videoTitle = order
      that.switcDisplay = false
      that.activeIndex = orderIndex
      that.PageIndex = 1
      that.PageSize = 10
      that.searchLoading = false
      that.bmessage = ''
      that.searchLoading1 = false
      that.bmessage1 = ''
      if (that.activeIndex == 4) {
        // that.activeIndex = 0
        this.ProductType = 5
        that.interfaceFunction(0, that.topIndex, that.PageIndex, that.PageSize)
        that.backToTop()
        console.log('上门养车')
      } else {
        this.ProductType = -1
        that.interfaceFunction(
          orderIndex,
          that.topIndex,
          that.PageIndex,
          that.PageSize
        )
      }
      that.backToTop()
    },
    // 顶部显示与隐藏
    clearClick() {
      this.switcDisplay = false
    },
    // 顶部标题的文字
    test(msg) {
      console.log('msg', msg)
      this.switcDisplay = !this.switcDisplay
      console.log(this.switcDisplay)
    },
    // 接口传参方法
    interfaceFunction(OrderType, OrderListStatus, PageIndex, PageSize) {
      console.log(23456)
      console.log(this.searchLoading)
      this.globalData.orderListStatus = OrderListStatus
      this.globalData.OrderType = OrderType
      let that = this
      if (this.searchLoading == true) {
        return false
      }
      let rquest = {
        OrderType: OrderType,
        OrderListStatus: OrderListStatus,
        PageIndex: that.PageIndex,
        PageSize: that.PageSize,
        ProductType: that.ProductType
      }
      // 获取订单列表
      GetOrderList(rquest)
        .then(ress => {
          let arr = ress.data.items
          if (that.PageIndex * that.PageSize >= ress.data.totalItems) {
            that.searchLoading1 = false
            that.searchLoading = true
            that.bmessage = '我也是有底线的哦'
          }
          if (that.PageIndex === 1) {
            that.orderAllarr = ress.data.items
          } else {
            that.orderAllarr = that.orderAllarr.concat(ress.data.items)
            console.log(that.orderAllarr.length)
          }
          that.orderAllarr.forEach((item, index) => {
            item.products.forEach((item1, index1) => {
              if (item1.imageUrl == '') {
                item1.imageUrl =
                  'https://m.aerp.com.cn/mini-app-main/produceNo.png'
              }
            })
          })
          that.orderAllarr.forEach(element => {
            element.products.forEach(aitm => {
              aitm.price = aitm.price.toFixed(2)
            })
          })
          that.PageIndex++
          console.log('订单列表', that.orderAllarr)

          // if(arr.length != 0){
          //   that.PullDownRefresh = true
          // }else{
          //   that.PullDownRefresh = false
          // }
          // that.orderAllarr = arr
        })
        .catch(err => {})
    },

    // 用户可操作按钮点击事件
    orderBtnClick(itema, item1) {
      if (itema.showFunctionName === '支付订单') {
        this.confirmPay(item1.orderNo, item1.actualAmount)
      } else if (itema.showFunctionName === '取消订单') {
        this.showOrder = true
        this.shopnumorderNo = item1.orderNo
      } else if (itema.showFunctionName === '再次【测试】') {
        this.buyAgain(item1.orderNo, item1.actualAmount)
      } else if (itema.showFunctionName === '申请预约') {
        this.$router.push({
          path: '/pages/newAppointment/main',
          query: {
            idnum: '2',
            applynum: '3',
            orderNo: item1.orderNo
          }
        })
        this.showglobalData = true
      } else if (itema.showFunctionName === '评价订单') {
        this.$router.push({
          path: '/pages/SelectionAndEvaluation/main',
          query: {
            OrderNo: item1.orderNo
          }
        })
        this.showglobalData = true
        // wx.showToast({
        //   title: '程序猿正在开发中',
        //   icon: 'none'
        // })
      } else if (itema.showFunctionName === '追加点评') {
        let rquest = {
          orderNo: item1.orderNo
        }
        GetShop(rquest).then(res => {
          console.log(`加载追加点评视图`, res)
          if (res.code == 10000) {
            this.$router.push({
              path: '/pages/PublicationReview/main',
              query: {
                orderNo: item1.orderNo
              }
            })
          } else {
            wx.showToast({ title: res.message, icon: 'none' })
          }
        })
      }
    },
    backToTop() {
      wx.pageScrollTo({
        scrollTop: 0,
        duration: 400
      })
    },
    // 顶部tabbar切换事件
    onChangeTitle(event) {
      let that = this
      that.topIndex = event.mp.detail.index      
      if (that.active === that.topIndex) {
        return;
      }
      that.active = event.mp.detail.index
      that.PageIndex = 1
      that.PageSize = 10
      that.searchLoading = false
      that.bmessage = ''
      that.searchLoading1 = false
      that.bmessage1 = ''
      console.log(that.active)
      if (that.activeIndex == 4) {
        // that.activeIndex = 0
        this.ProductType = 5
        that.interfaceFunction(0, that.topIndex, that.PageIndex, that.PageSize)
        that.backToTop()
        console.log('上门养车')
      } else {
        this.ProductType = -1
        that.interfaceFunction(
          that.activeIndex,
          that.topIndex,
          that.PageIndex,
          that.PageSize
        )
      }
      that.backToTop()
      // that.interfaceFunction(that.activeIndex, that.topIndex, that.PageIndex, that.PageSize)
    },
    // 全部订单页面的点击事件
    orderClickall(item1) {
      this.$router.push({
        path: '/pages/orderDetailsTwo/main',
        query: {
          orderNo: item1.orderNo
        }
      })
      this.showglobalData = true
    }
  },
  onLoad(options) {
    let that = this
    that.PageIndex = 1
    that.PageSize = 10
    that.searchLoading = false
    that.bmessage = ''
    that.searchLoading1 = false
    that.bmessage1 = ''
    if (options.num === '123') {
      console.log(`index`, options.index)
      that.active = Number(options.index)
      that.topIndex = Number(options.index)

      console.log(`active`, that.active)

      that.interfaceFunction(0, options.index, that.PageIndex, that.PageSize)
    } else if (options.orderPage === '1212') {
      that.active = options.orderListStatus
      that.interfaceFunction(
        options.orderType,
        options.orderListStatus,
        that.PageIndex,
        that.PageSize
      )
    } else {
      that.interfaceFunction(0, 0, that.PageIndex, that.PageSize)
      that.active = 0
    }
  },
  mounted() {
    let that = this
    that.tabsShow = true
    // 获取系统信息
    wx.getSystemInfo({
      success: function(res) {
        let clientHeight = res.windowHeight
        let clientWidth = res.windowWidth
        let ratio = 750 / clientWidth
        that.windowHeight = clientHeight * ratio - 88 + 'rpx'
        that.windowHeight1 = clientHeight * ratio + 'rpx'
      }
    })
  },
  onUnload: function() {
    this.active = this.topIndex
    this.activeIndex = 0
    this.reasonArr = [] // 取消原因数组
    this.textvalue = '' // 用户输入取消原因文字
    this.shopnumorderNo = '' // 点击取消订单按钮的时候存的订单编号
    this.videoTitle = '全部订单'
    this.orderAllarr = [] // 全部订单
    this.items = [] // 后台返回数组\\
    this.showOrder = false
    this.ProductType = -1
    this.searchLoading = false
    this.globalData.orderListStatus = -1
    this.globalData.OrderType = -1
    this.showglobalData = false
    this.switcDisplay = false
    this.PageIndex = 1
  },
  onHide() {
    // this.active = this.topIndex
    // this.activeIndex = 0
    // this.reasonArr = [] // 取消原因数组
    // this.textvalue = '' // 用户输入取消原因文字
    // this.shopnumorderNo = '' // 点击取消订单按钮的时候存的订单编号
    // this.videoTitle = '全部订单'
    // this.orderAllarr = [] // 全部订单
    // this.items = [] // 后台返回数组
    this.showOrder = false
    this.searchLoading = false
    this.PageIndex = 1
  },
  // 下拉刷新
  //   onPullDownRefresh(){
  //     wx.showLoading({title: '努力加载中'})
  //     this.interfaceFunction(this.activeIndex,this.topIndex, 1, 300)
  //     if(this.PullDownRefresh){
  //       wx.stopPullDownRefresh() //停止下拉刷新
  //       wx.hideLoading()
  //       this.PullDownRefresh = false
  //     }else{
  //       wx.showToast({title:'已是最新数据',icon:'none'})
  //       wx.stopPullDownRefresh() //停止下拉刷新
  //       wx.hideLoading()
  //       this.PullDownRefresh = false
  //     }
  //   },
  // 上拉加载
  //   onReachBottom: function() {
  //     wx.showLoading({
  //       title: '正在加载'
  //     })
  //     pageActiveIndex++
  //     this.searchLoading = true
  //     this.interfaceFunction(this.activeIndex,this.topIndex, pageActiveIndex, 300)
  //     if(this.PullDownRefresh){
  //       wx.hideLoading()
  //       this.searchLoading = false
  //     }else{
  //       wx.hideLoading()
  //       this.searchLoading = false
  //       wx.showToast({title:'已全部加载',icon:'none'})
  //     }
  //   },
  // 暂定为onshow中重新调用此函数，待测试
  onShow() {
    // this.interfaceFunction(this.activeIndex, this.active, 1, 300)
    this.GetShop()
    // console.log(this.globalData.orderListStatus, 'orderListStatus')
    // console.log(this.globalData.OrderType, 'OrderType')

    // if (this.globalData.orderListStatus != -1 && this.showglobalData == true) {
    //   this.interfaceFunction(this.globalData.OrderType, this.globalData.orderListStatus, this.PageIndex, this.PageSize)
    //   console.log(121)
    // }
  }
}
</script>
<style scoped lang="scss">
.demo_page {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  background: #f3f3f3;
  // padding-top:141rpx;
  // box-sizing:border-box;
}
.order_boxs {
  position: fixed;
  top: 143rpx;
  left: 0;
  z-index: 99999;
  width: 750rpx;
  height: 100%;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  .order_allboxs {
    width: 100%;
    height: 230rpx;
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: flex-start;
    align-items: flex-start;
    background: rgba(255, 255, 255, 1);
    padding: 30rpx 0 0 30rpx;
    box-sizing: border-box;
    .order_boxs_one {
      width: 220rpx;
      height: 70rpx;
      background: rgba(255, 255, 255, 1);
      border: 1rpx solid rgba(230, 230, 230, 1);
      border-radius: 8rpx;
      text-align: center;
      line-height: 68rpx;
      margin: 0 13rpx 13rpx 0;
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
    .bgorder {
      background: rgba(255, 238, 231, 1);
      border: 1rpx solid rgba(255, 193, 173, 1);
      color: rgba(255, 99, 36, 1);
    }
  }
}
.tabs {
  // position: relative;
}
.title_tab {
  width: 100%;
  display: flex;
  flex-direction: column;
  background: #f3f3f3;
}
.title_tab_content {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  padding: 16rpx 30rpx 0 30rpx;
  box-sizing: border-box;
  // background:#f3f3f3;
}
.else {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.elseimg {
  width: 215rpx;
  height: 215rpx;
}
.box {
  width: 100%;
  box-sizing: border-box;
}
.tab1 {
  width: 100%;
  // height: 360rpx;
  border-radius: 10rpx 10rpx 10rpx 10rpx;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  padding-top: 0;
  box-sizing: border-box;
  margin-bottom: 16rpx;
  padding: 0 30rpx;
  background: #fff;
}
.tab1_title {
  width: 100%;
  height: 90rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  border-bottom: 1px solid #f3f3f3;
}
.tab1_title .txt1 {
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
}
.tab1_title .txt2 {
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
}
.tab1_contetn {
  width: 100%;
  height: 170rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.tab1-images {
  min-width: 110rpx;
  min-height: 100rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: flex-start;
}
.tab1_img {
  width: 102rpx;
  height: 102rpx;
}
.tab1_top {
  width: 500rpx;
}
.tab1_top .p1 {
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}
.tab1_top .p2 {
  width: 500rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  margin-top: 27rpx;
}
.tab1_top .p2 .txt1 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
}
.tab1_top .p2 .txt2 {
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
}
.tab1 .btn {
  width: 100%;
  height: 100rpx;
  padding-left: 100rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  // justify-content: space-between;
  justify-content: flex-end;
  align-items: center;
}
.button_btn {
  width: 170rpx;
  height: 60rpx;
  background: rgba(255, 255, 255, 1);
  border: 1rpx solid rgba(255, 99, 36, 1);
  border-radius: 30rpx;
  line-height: 58rpx;
}
.top_two {
  position: relative;
  // padding-left: 30rpx;
  padding-right: 30rpx;
  box-sizing: border-box;
  height: 170rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  margin: 16rpx 0;
  max-width: 660rpx;
  .scroll {
    width: 100%;
    display: -webkit-box;
    -webkit-overflow-scrolling: touch;
    -ms-overflow-style: none;
    overflow: -moz-scrollbars-none;
    overflow-x: scroll;
    .top_two_box {
      width: 100%;
      display: flex;
      flex-direction: row;
      justify-content: flex-start;
      align-items: center;
      .img {
        width: 102rpx;
        height: 102rpx;
        margin-right: 18rpx;
      }
    }
  }
  .img_img {
    width: 24rpx;
    height: 24rpx;
  }
  .position {
    position: absolute;
    top: 26rpx;
    right: 76rpx;
    width: 110rpx;
    height: 110rpx;
    background: rgba(0, 0, 0, 1);
    text-align: center;
    line-height: 110rpx;
    opacity: 0.7;
    font-size: 26rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(255, 255, 255, 1);
  }
}
.position1 {
  width: 110rpx;
  height: 110rpx;
  background: rgba(0, 0, 0, 1);
  text-align: center;
  line-height: 110rpx;
  opacity: 0.7;
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 255, 255, 1);
}
// 弹窗
.clearC {
  width: 100%;
}
.popup_header {
  width: 100%;
  text-align: center;
  font-weight: bold;
  line-height: 100rpx;
  font-size: 35rpx;
}
.popup_btn {
  width: 100%;
}
.popup_btn > button {
  width: 640rpx;
  height: 90rpx;
  background: #ff6324;
  margin: 0 auto;
  border-radius: 45rpx;
  color: #fff;
}
.popup_inp {
  width: 100%;
  height: 200rpx;
  background: #f2f2f2;
  border-radius: 10rpx;
}
.tareaText {
  height: 200rpx;
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}
.button_op {
  margin: 2rpx 5rpx;
}
.imgListMoney {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: flex-end;
  align-items: center;
  .txt1 {
    font-size: 24rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(136, 136, 136, 1);
  }
  .txt2 {
    font-size: 28rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(255, 99, 36, 1);
  }
}
.positionTOP {
  width: 100%;
  text-align: center;
  font-size: 30rpx;
  color: #ccc;
}
</style>