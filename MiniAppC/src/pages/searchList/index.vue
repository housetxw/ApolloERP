<template>
  <div class="demo_page" :style="{minHeight:windowHeight1}">
    <!-- 搜索 -->
    <div class="top_search">
      <van-search v-model="KeyValuePage" shape="round" readonly @tap="onSearch"></van-search>
    </div>
    <!-- 汽车品牌 -->
    <p class="top_title" @tap="changeCard">
      <text class="txt1" v-if="!carArrFirst.brandUrl" style="color:red">需点此选车后再为您推荐搜索产品</text>
      <img :src="carArrFirst.brandUrl" alt class="img" />
      <text class="txt1">{{carArrFirst.vehicle}}{{carArrFirst.salesName}}</text>
      <img src="https://m.aerp.com.cn/mini-RG-main/maintenance_jump_icon.png" alt style="width:24rpx;height:24rpx" />
    </p>
    <!-- 下拉菜单 -->
    <div class="top_mask">
      <div class="dropdownMenu">
        <div class="dropdownMenu_one" @tap="sortClick">
          <p class="txt1">{{title}}</p>
          <img :src="src3" alt class="img" />
        </div>
        <div class="dropdownMenu_one" @tap="screenClick">
          <p class="txt1">筛选</p>
          <img :src="src2" alt class="img" />
        </div>
      </div>
      <div class="dropdownMenu_item" @tap="dispearClcik" v-if="dis" :style="{height:windowHeight}">
        <div class="dropdownMenu_item_one" v-for="(item, index) in sortArr" :key="index" @tap="selectClick(item)">
          <p :class="item.switchdis?'pq_active':'p1'">{{item.text}}</p>
          <img :src="item.switchdis?item.src:''" alt class="img" />
        </div>
      </div>
    </div>
    <!-- 第一个套餐 -->
    <!-- <div class="box" v-for="(items, indexs) in mealArr" :key="indexs">
      <div class="box_top">
        <div class="box_img_img"><img :src="items.src1" alt="" class="img1" /></div>
        <div class="box_content">
          <p class="p1">{{items.title}}</p>
          <p class="p2">{{items.text}}</p>
        </div>
      </div>
      <div class="box_bottom">
        <p class="p1">
          <text class="txt1" v-for="(item1, index1) in items.bqArr" :key="index1">{{item1}}</text>
        </p>
        <p class="p2">
          <text class="txt1">{{items.evaluate}}</text>
          <text class="txt2 txt1">￥{{items.money}}</text>
        </p>
      </div>
    </div>-->
    <!-- 第二个套餐 -->
    <div class="top_bottom" v-for="(itemm, indexx) in bottomArr" :key="indexx" @tap="shopClick(itemm)">
      <img :src="itemm.image1" alt class="img" mode="aspectFit" />
      <div class="top_bottom_right">
        <p class="p1">{{itemm.name}}</p>
        <p class="p2">
          <text class="txt1" v-for="(son,son1) in itemm.tags" :key="son1">{{son}}</text>
        </p>
        <div class="left">
          <p class="p3">
            <!-- <text class="txt1">{{itemm.commentNumber}}条评价</text>
            <text class="txt1 txt2">{{itemm.favorableRate}}%好评</text>-->
          </p>
          <text class="txt">￥{{itemm.salesPrice}}</text>
        </div>
      </div>
    </div>
    <!-- 侧边栏 -->
    <div class="side_edge" v-if="showDis">
      <div class="left" @tap="sideClick"></div>
      <div class="right">
        <p class="p1">品牌</p>
        <div class="boxx">
          <p :class="['wenzi',{'wenziColor':colorIndex == index8}]" v-for="(item8, index8) in paizaArr" :key="index8" @tap="brandClick(item8,index8)">
            <text :class="['txt1',{'text_color':colorIndex == index8}]">{{item8}}</text>
          </p>
        </div>
        <p class="p2">价格</p>
        <div class="price_box">
          <input type="number" placeholder-style="color:rgba(102,102,102,1);font-size:28rpx;" placeholder="最低价" :value="startMoney" class="input1" @change="onChange1" />
          <text class="fasd">-</text>
          <input type="number" placeholder-style="color:rgba(102,102,102,1);font-size:28rpx;" placeholder="最高价" :value="endmoney" class="input1" @change="onChange2" />
        </div>
        <div class="btn">
          <p class="btn1" @tap="resetClick">重置</p>
          <p class="btn2" @tap="sureClick">确认</p>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { GetSearch } from '../../api'
import eventBus from '../../utils/eventBus.js'
export default {
  data() {
    return {
      showDis: false,
      windowHeight: '',
      windowHeight1: '',
      dis: false,
      title: '评分从低到高',
      sortNum: 1, //排序变量
      brandNum: '', //品牌变量
      startMoney: '', //开始价格
      endmoney: '', //结束价格
      KeyValuePage: '', //上个页面传过来的值
      src2: `${this.globalData.imgPubUrl}search_screening_icon.png`, //筛选箭头
      src3: `${this.globalData.imgPubUrl}search_down_icon.png`, //下箭头
      // 筛选数组
      sortArr: [
        {
          text: '评分从低到高',
          src: `${this.globalData.imgPubUrl}search_check_icon.png`,
          switchdis: true,
          num: 1
        },
        {
          text: '评分从高到低',
          src: `${this.globalData.imgPubUrl}search_check_icon.png`,
          switchdis: false,
          num: 2
        },
        {
          text: '销量从低到高',
          src: `${this.globalData.imgPubUrl}search_check_icon.png`,
          switchdis: false,
          num: 3
        },
        {
          text: '销量从高到低',
          src: `${this.globalData.imgPubUrl}search_check_icon.png`,
          switchdis: false,
          num: 4
        },
        {
          text: '价格从低到高',
          src: `${this.globalData.imgPubUrl}search_check_icon.png`,
          switchdis: false,
          num: 5
        },
        {
          text: '价格从高到低',
          src: `${this.globalData.imgPubUrl}search_check_icon.png`,
          switchdis: false,
          num: 6
        }
      ],
      // mealArr:[
      //   {
      //     title:'套餐名称腐餐名称套的发豆腐SD敢达',
      //     src1:`${this.globalData.imgPubUrl}search_goods_img.png`,
      //     text:'套餐描述套餐豆腐98%好评',
      //     evaluate:'98%好评',
      //     money:'622.00',
      //     bqArr:['标签一','标签二']
      //   },
      //   {
      //     title:'套餐名称腐餐名称套的发豆腐SD敢达',
      //     src1:`${this.globalData.imgPubUrl}search_goods_img.png`,
      //     text:'套餐描述套餐豆腐98%好评',
      //     evaluate:'98%好评',
      //     money:'622.00',
      //     bqArr:['标签一','标签二']
      //   }
      // ],
      bottomArr: [], //全局数组
      paizaArr: [], //品牌数组
      colorIndex: -1 //品牌索引变量
    }
  },
  computed: {
    carArrFirst() {
      return this.$store.state.carArr[0] || {}
    }
  },
  methods: {
    // 跳转商品详情页
    shopClick(itemm) {
      let that = this
      this.globalData.serviceType = 'toShop'
      this.globalData.OrderType = 6
      this.globalData.ProductType = 19
      this.$router.push({
        path: '/pages/detailsPages/main',
        query: {
          provinceId: that.$store.state.curCityInfo.provinceId,
          cityId: that.$store.state.curCityInfo.cityId,
          productCode: itemm.productCode,
          shopPid: itemm.productCode
        }
      })
    },
    // 品牌点击事件
    brandClick(item8, index8) {
      this.brandNum = item8
      this.colorIndex = index8
      // this.SearchList(this.KeyValuePage,this.sortNum,this.startMoney,this.endmoney,this.brandNum,this.carArrFirst.vehicleId,this.carArrFirst.tid,this.carArrFirst.paiLiang,this.carArrFirst.nian)
      // this.showDis = false
    },
    // 重置事件
    resetClick() {
      this.startMoney = ''
      this.endmoney = ''
      this.brandNum = ''
      this.colorIndex = -1
      this.SearchList(
        this.KeyValuePage,
        this.sortNum,
        this.startMoney,
        this.endmoney,
        this.brandNum,
        this.carArrFirst.vehicleId,
        this.carArrFirst.tid,
        this.carArrFirst.paiLiang,
        this.carArrFirst.nian
      )
      this.showDis = false
    },
    // 确认事件
    sureClick() {
      this.SearchList(
        this.KeyValuePage,
        this.sortNum,
        this.startMoney,
        this.endmoney,
        this.brandNum,
        this.carArrFirst.vehicleId,
        this.carArrFirst.tid,
        this.carArrFirst.paiLiang,
        this.carArrFirst.nian
      )
      this.showDis = false
    },
    // 开始价格
    onChange1(e) {
      this.startMoney = e.mp.detail.value
    },
    // 结束价格
    onChange2(e) {
      this.endmoney = e.mp.detail.value
    },
    // 搜索事件
    onSearch() {
      wx.navigateBack()
    },
    // 更换车辆
    changeCard() {
      this.$router.push('/pages/myCard/main')
    },
    // 排序数组点击隐藏与显示
    sortClick() {
      this.dis = !this.dis
      if (this.dis) {
        this.src3 = `${this.globalData.imgPubUrl}search_up_icon.png` //上箭头
      } else {
        this.src3 = `${this.globalData.imgPubUrl}search_down_icon.png` //下箭头
      }
    },
    // 排序数组点击事件
    selectClick(item) {
      this.src3 = `${this.globalData.imgPubUrl}search_down_icon.png`
      this.sortArr.forEach(item => {
        item.switchdis = false
      })
      item.switchdis = true
      this.title = item.text
      this.dis = false
      this.sortNum = item.num
      this.SearchList(
        this.KeyValuePage,
        this.sortNum,
        this.startMoney,
        this.endmoney,
        this.brandNum,
        this.carArrFirst.vehicleId,
        this.carArrFirst.tid,
        this.carArrFirst.paiLiang,
        this.carArrFirst.nian
      )
    },
    // 排序隐藏与显示
    dispearClcik() {
      this.dis = false
    },
    // 筛选点击事件
    screenClick() {
      this.dis = false
      this.showDis = true
    },
    // 筛选消失
    sideClick() {
      this.showDis = false
    },
    // 搜索结果函数
    SearchList(
      key,
      Sort,
      StartPrice,
      EndPrice,
      Brand,
      VehicleId,
      TId,
      PaiLiang,
      Nian
    ) {
      let that = this
      let rquest = {
        Key: key,
        Sort: Sort,
        StartPrice: StartPrice,
        EndPrice: EndPrice,
        Brand: Brand,
        // "MainCategoryId":MainCategoryId,
        // "SecondCategoryId":SecondCategoryId,
        // "ChildCategoryId":ChildCategoryId,
        VehicleId: VehicleId,
        TId: TId,
        PaiLiang: PaiLiang,
        Nian: Nian,
        PageIndex: '1',
        PageSize: 50
      }
      // 搜索
      GetSearch(rquest)
        .then(res => {
          let arr = res.data.products
          arr.forEach(element => {
            element.salesPrice = element.salesPrice.toFixed(2)
            element.favorableRate = Number(element.favorableRate) * 100
          })
          that.bottomArr = arr
          that.paizaArr = res.data.brands
        })
        .catch(err => {})
    }
  },
  onLoad(options) {
    let that = this
    this.SearchList(
      options.value,
      this.sortNum,
      this.startMoney,
      this.endmoney,
      this.brandNum,
      this.carArrFirst.vehicleId,
      this.carArrFirst.tid,
      this.carArrFirst.paiLiang,
      this.carArrFirst.nian
    )
    this.KeyValuePage = options.value
  },
  mounted() {
    let that = this
    // 获取系统信息
    wx.getSystemInfo({
      success: function(res) {
        let clientHeight = res.windowHeight
        let clientWidth = res.windowWidth
        let ratio = 750 / clientWidth
        that.windowHeight = clientHeight * ratio - 326 + 'rpx'
        that.windowHeight1 = clientHeight * ratio + 'rpx'
      }
    })
    eventBus.$on('changeCar', () => {
      that.SearchList(
        that.KeyValuePage,
        that.sortNum,
        that.startMoney,
        that.endmoney,
        that.brandNum,
        that.carArrFirst.vehicleId,
        that.carArrFirst.tid,
        that.carArrFirst.paiLiang,
        that.carArrFirst.nian
      )
    })
  },
  onUnload() {
    this.sortNum = 1 //排序变量
    this.brandNum = '' //品牌变量
    this.startMoney = '' //开始价格
    this.endmoney = '' //结束价格
    this.KeyValuePage = '' //上个页面传过来的值
    this.bottomArr = [] //全局数组
    this.paizaArr = [] //品牌数组
    eventBus.$off('changeCar')
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
  padding-bottom: 20rpx;
  box-sizing: border-box;
}
.top_search {
  width: 100%;
}
.top_title {
  width: 100%;
  padding: 0 30rpx;
  box-sizing: border-box;
  height: 120rpx;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16rpx;
  .img {
    width: 80rpx;
    height: 80rpx;
    margin-right: 10rpx;
  }
  .txt1 {
    font-size: 28rpx;
    font-family: SourceHanSansCN;
    font-weight: 400;
    color: rgba(51, 51, 51, 1);
  }
}
.top_mask {
  position: relative;
  width: 100%;
  display: flex;
  flex-direction: column;
  .dropdownMenu {
    width: 100%;
    height: 90rpx;
    background: rgba(255, 255, 255, 1);
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
    .dropdownMenu_one {
      flex: 1;
      display: flex;
      flex-direction: row;
      justify-content: center;
      align-items: center;
      .txt1 {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
      }
      .img {
        width: 28rpx;
        height: 28rpx;
        margin-left: 10rpx;
      }
    }
  }
  .dropdownMenu_item {
    position: absolute;
    top: 90rpx;
    left: 0;
    width: 100%;
    background: rgba(0, 0, 0, 0.7);
    display: flex;
    flex-direction: column;
    .dropdownMenu_item_one {
      width: 100%;
      height: 90rpx;
      background: rgba(255, 255, 255, 1);
      padding: 0 30rpx;
      box-sizing: border-box;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      border-bottom: 1rpx solid #f3f3f3;
      .p1 {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
      }
      img {
        width: 30rpx;
        height: 30rpx;
      }
    }
  }
}
.pq_active {
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(255, 99, 36, 1);
}
.box {
  width: 750rpx;
  height: 270rpx;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  padding: 30rpx;
  box-sizing: border-box;
  background: #ffffff;
  border-top: 1rpx solid #f3f3f3;
  .box_top {
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .box_img_img {
      width: 130rpx;
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      .img1 {
        width: 110rpx;
        height: 110rpx;
      }
    }

    .box_content {
      width: 550rpx;
      height: 100%;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-start;
      .p1 {
        overflow: hidden;
        white-space: pre-wrap;
        display: -webkit-box;
        text-overflow: ellipsis;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
        font-size: 28rpx;
        font-family: Adobe Heiti Std;
        font-weight: normal;
        color: rgba(51, 51, 51, 1);
      }
      .p2 {
        overflow: hidden;
        white-space: pre-wrap;
        display: -webkit-box;
        text-overflow: ellipsis;
        -webkit-line-clamp: 2;
        -webkit-box-orient: vertical;
        font-size: 24rpx;
        font-family: Adobe Heiti Std;
        font-weight: normal;
        color: rgba(166, 165, 174, 1);
      }
    }
  }
  .box_bottom {
    width: 100%;
    height: 60rpx;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .p1 {
      width: 430rpx;
      overflow: hidden;
      white-space: pre-wrap;
      display: -webkit-box;
      text-overflow: ellipsis;
      -webkit-line-clamp: 1;
      -webkit-box-orient: vertical;
      .txt1 {
        // width:87rpx;
        padding: 0 10rpx;
        box-sizing: border-box;
        height: 30rpx;
        border: 1rpx solid rgba(92, 198, 130, 1);
        border-radius: 4rpx;
        text-align: center;
        line-height: 28rpx;
        font-size: 24rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(92, 198, 130, 1);
        margin-right: 10rpx;
      }
    }
    .p2 {
      .txt1 {
        font-size: 24rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(173, 173, 173, 1);
      }
      .txt2 {
        font-size: 32rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(255, 99, 36, 1);
        margin-left: 10rpx;
      }
    }
  }
}
.top_bottom {
  width: 100%;
  height: 250rpx;
  padding: 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  background: #ffffff;
  border-top: 1rpx solid #f3f3f3;
  .img {
    width: 113rpx;
    height: 113rpx;
  }
  .top_bottom_right {
    width: 544rpx;
    // height: 150rpx;
    display: flex;
    flex-direction: column;
    // justify-content: space-between;
    // align-items: flex-start;
    .p1 {
      overflow: hidden;
      white-space: pre-wrap;
      display: -webkit-box;
      text-overflow: ellipsis;
      -webkit-line-clamp: 2;
      -webkit-box-orient: vertical;
      font-size: 28rpx;
      font-family: SourceHanSansCN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
    .p2 {
      display: flex;
      flex-direction: row;
      margin: 20rpx 0;
      .txt1 {
        padding: 0 10rpx;
        box-sizing: border-box;
        height: 30rpx;
        border: 1rpx solid rgba(92, 198, 130, 1);
        border-radius: 4rpx;
        line-height: 28rpx;
        margin-right: 10rpx;
        font-size: 24rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        text-align: center;
        color: rgba(92, 198, 130, 1);
      }
    }
    .left {
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      .p3 {
        .txt1 {
          font-size: 24rpx;
          font-family: SourceHanSansCN;
          font-weight: 400;
          color: rgba(173, 173, 173, 1);
        }
        .txt2 {
          margin-left: 10rpx;
        }
      }
      .txt {
        font-size: 32rpx;
        font-family: SourceHanSansCN;
        font-weight: 400;
        color: rgba(255, 99, 36, 1);
      }
    }
  }
}
.side_edge {
  position: absolute;
  top: 0;
  left: 0;
  z-index: 999;
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: row;
  .left {
    width: 130rpx;
    // height: 100%;
    background: rgba(0, 0, 0, 0.7);
  }
  .right {
    width: 620rpx;
    background: #ffffff;
    padding: 30rpx;
    padding-right: 20rpx;
    box-sizing: border-box;
    .p1 {
      font-size: 30rpx;
      font-family: Source Han Sans CN;
      font-weight: bold;
      color: rgba(51, 51, 51, 1);
      margin-top: 66rpx;
    }
    .boxx {
      width: 100%;
      display: flex;
      flex-direction: row;
      flex-wrap: wrap;
      justify-content: flex-start;
      align-items: center;
      margin-top: 30rpx;
      .wenzi {
        width: 178rpx;
        height: 60rpx;
        background: #ffffff;
        border: 1rpx solid #ccc;
        border-radius: 8rpx;
        text-align: center;
        line-height: 58rpx;
        margin: 0 10rpx 15rpx 0;
        .txt1 {
          width: 100%;
          overflow: hidden;
          white-space: pre-wrap;
          display: -webkit-box;
          text-overflow: ellipsis;
          -webkit-line-clamp: 2;
          -webkit-box-orient: vertical;
          font-size: 24rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: #888888;
        }
        .text_color {
          color: rgba(255, 99, 36, 1);
        }
      }
      .wenziColor {
        background: rgba(255, 238, 231, 1);
        border: 1rpx solid rgba(255, 193, 173, 1);
      }
    }
    .p2 {
      font-size: 30rpx;
      font-family: Source Han Sans CN;
      font-weight: bold;
      color: rgba(51, 51, 51, 1);
      margin-top: 80rpx;
      margin-bottom: 30rpx;
    }
    .price_box {
      width: 100%;
      display: flex;
      flex-direction: row;
      justify-content: center;
      align-items: center;
      .input1 {
        width: 250rpx;
        height: 60rpx;
        background: rgba(248, 248, 248, 1);
        border-radius: 8rpx;
        text-align: center;
      }
      .fasd {
        width: 60rpx;
        height: 60rpx;
        text-align: center;
        line-height: 60rpx;
        padding: 0 15rpx;
        box-sizing: border-box;
      }
    }
    .btn {
      position: absolute;
      bottom: 72rpx;
      left: 160rpx;
      width: 100%;
      display: flex;
      flex-direction: row;
      .btn1 {
        width: 280rpx;
        height: 75rpx;
        background: rgba(255, 255, 255, 1);
        border: 1rpx solid rgba(230, 230, 230, 1);
        border-radius: 38rpx 0px 0px 38rpx;
        text-align: center;
        line-height: 73rpx;
        font-size: 32rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(136, 136, 136, 1);
      }
      .btn2 {
        width: 280rpx;
        height: 75rpx;
        background: rgba(255, 99, 36, 1);
        border-radius: 0rpx 38rpx 38rpx 0rpx;
        text-align: center;
        line-height: 73rpx;
        font-size: 32rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 255, 255, 1);
      }
    }
  }
}
</style>