<template>
  <div class="demo_pages">
    <!-- 车店信息 -->
    <div class="carInfo">
      <div class="car">
        <p class="p1">{{carName}}</p>
        <!-- <img src="https://m.aerp.com.cn/mini-RG-main/maintenance_jump_icon.png" alt=""> -->
      </div>
      <div class="line"></div>
      <div class="stor">
        <p class="p2">{{shopName}}</p>
        <!-- <img src="https://m.aerp.com.cn/mini-RG-main/maintenance_jump_icon.png" alt=""> -->
      </div>
    </div>
    <!-- 技师信息 -->
    <div class="page"
      v-for="(techInfo,index) in techs"
      :key="index"
      v-if="techs.length > 0">
      
      <!-- 技师 -->
      <div class="page_top">
        <img :src="techInfo.techHeadUrl" alt="" class="page_img">
        <div class="page_two">
          <p class="page_p1">{{techInfo.techName}}</p>
          <p class="page_p2">{{techInfo.techLevel}}</p>
        </div>
      </div>
      <!-- 笑脸 -->
      <div class="icon_content">
        <div class="content_Icon" v-for="(mes,index1) in techInfo.Img" :key="index1" @tap="techSmileClick(mes,index1,index)">
          <div class="content_icon">
            <img :src="mes.isTrue?mes.urls:mes.url" alt />
          </div>
          <div :class="['content_name',{'content_nameColor':mes.isTrue}]">{{mes.name}}</div>
        </div>
      </div>
      <!-- 评价 -->
      <div class="page_meal" v-if="techInfo.techShowMeal">
        <div
          :class="['one',{'oneColor':itemt.isChecked}]"
          v-for="(itemt,indext) in techInfo.techMealPeople"
          :key="indext"
          @tap="techMealClick(itemt,indext,techInfo,index)"
        >{{itemt.labelName}}</div>
      </div>
    </div>
    <!-- 门店信息 -->
    <div class="page">
      <!-- 门店 -->
      <div class="page_top">
        <img :src="shopInfo.shopImageUrl" alt class="page_img1" />
        <div class="page_two">
          <p class="page_p1">{{shopInfo.shopName}}</p>
          <!-- <p class="page_p2">高级技术</p> -->
        </div>
      </div>
      <!-- 笑脸 -->
      <div class="icon_content">
        <div
          class="content_Icon"
          v-for="(mes,index1) in Img"
          :key="index1"
          @tap="storeSmileClick(mes,index1)"
        >
          <div class="content_icon">
            <img :src="index1 == storeSmileActive?mes.urls:mes.url" alt />
          </div>
          <div :class="['content_name',{'content_nameColor':index1 == storeSmileActive}]">{{mes.name}}</div>
        </div>
      </div>
      <!-- 评价 -->
      <div class="page_meal" v-if="storeShowMeal">
        <div
          :class="['one',{'oneColor':itemb.isChecked}]"
          v-for="(itemb,indexb) in storeMealPeople"
          :key="indexb"
          @tap="storeMealClick(itemb,indexb)"
        >{{itemb.labelName}}</div>
      </div>
    </div>
    <!-- 商品信息 -->
    <div
      class="page"
      v-for="(item,index) in orderShopInfo"
      :key="index"
      v-if="orderShopInfo.length > 0"
    >
      <!-- 技师 -->
      <div class="page_top">
        <img :src="item.productImageUrl" alt class="page_img2" />
        <div class="page_two">
          <p class="page_p1">{{item.productDisplayName}}</p>
        </div>
      </div>
      <!-- 笑脸 -->
      <div class="icon_content">
        <div
          class="content_Icon"
          v-for="(mes,index1) in item.Img"
          :key="index1"
          @tap="shopSmileClick(mes,index1,index)"
        >
          <div class="content_icon">
            <img :src="mes.isTrue?mes.urls:mes.url" alt />
          </div>
          <div :class="['content_name',{'content_nameColor':mes.isTrue}]">{{mes.name}}</div>
        </div>
      </div>
      <!-- 评价 -->
      <div class="page_meal" v-if="item.shopShowMeal">
        <div
          :class="['one',{'oneColor':itema.isChecked}]"
          v-for="(itema,indexa) in item.shopMealPeople"
          :key="indexa"
          @tap="shopMealClick(itema,indexa,item,index)"
        >{{itema.labelName}}</div>
      </div>
    </div>
    <!-- 用户输入内容 -->
    <div class="shop_Writecomments">
      <div class="Ipt">
        <textarea
          name="desc"
          :value="textvalue"
          placeholder-style="color:rgba(102,102,102,1);font-size:28rpx;font-weight:400;"
          placeholder="请留下您的真实感受。20-500字以内。"
          maxlength="500"
          @input="bindTextAreaBlur"
        ></textarea>
        <div class="Obtain">评价/追评订单可获得100积分</div>
        <!-- 上传图片 -->
        <div class="nav_img" v-for="(itemss,nn) in fileList" :key="nn">
          <van-icon name="cross" @click="delImg(nn)" class="delte" />
          <img :src="itemss" alt />
        </div>
        <div class="add_img" @click="upLoad">
          <img :src="up" alt />
        </div>
      </div>
      <div class="anonymous">
        <van-checkbox :value="checked" checked-color="#07c160" @change="onChange">匿名评价</van-checkbox>
      </div>
    </div>
    <!-- 提交评价 -->
    <div class="footer">
      <p @tap="commitComment">提交评价</p>
    </div>
  </div>
</template>

<script>
import eventBus from '../../utils/eventBus.js'
import { GetSelection, GetCommentMeal, commoitSubmission } from '../../api'

let list = []
let techList = []
export default {
  data() {
    return {
      OrderNo: '', // 订单号
      textvalue: '', // 用户评价内容
      carName: '', // 车名字
      shopName: '', // 门店名字
      shopInfo: {}, // 门店信息
      orderShopInfo: [], // 订单商品信息
      storeShowMeal: false, // 门店信息点击笑脸显示标签字段
      storeMealPeople: [], // 门店标签数组
      storeIdarr: [], // 门店标签数组id集合
      storeSmileActive: -1, // 门店笑脸索引
      shopsmileActive: -1, // 商品笑脸索引

      techs:[],//技师信息

      Img: [
        {
          url: `${this.globalData.imgPubUrl}Very bad - unchecked.png`,
          name: '非常差',
          urls: `${this.globalData.imgPubUrl}Very bad - selected.png`
        },
        {
          url: `${this.globalData.imgPubUrl}Poor - not selected.png`,
          name: '较差',
          urls: `${this.globalData.imgPubUrl}Poor - selected.png`
        },
        {
          url: `${this.globalData.imgPubUrl}General - unchecked.png`,
          name: '一般',
          urls: `${this.globalData.imgPubUrl}commonly.png`
        },
        {
          url: `${this.globalData.imgPubUrl}Satisfied - not selected.png`,
          name: '满意',
          urls: `${this.globalData.imgPubUrl}Satisfied - selected.png`
        },
        {
          url: `${this.globalData.imgPubUrl}Great - unchecked.png`,
          name: '超赞',
          urls: `${this.globalData.imgPubUrl}Great - check.png`
        }
      ], // 笑脸数组
      shopimgarr: [
        {
          url: `${this.globalData.imgPubUrl}Very bad - unchecked.png`,
          name: '非常差',
          urls: `${this.globalData.imgPubUrl}Very bad - selected.png`,
          isTrue: false
        },
        {
          url: `${this.globalData.imgPubUrl}Poor - not selected.png`,
          name: '较差',
          urls: `${this.globalData.imgPubUrl}Poor - selected.png`,
          isTrue: false
        },
        {
          url: `${this.globalData.imgPubUrl}General - unchecked.png`,
          name: '一般',
          urls: `${this.globalData.imgPubUrl}commonly.png`,
          isTrue: false
        },
        {
          url: `${this.globalData.imgPubUrl}Satisfied - not selected.png`,
          name: '满意',
          urls: `${this.globalData.imgPubUrl}Satisfied - selected.png`,
          isTrue: false
        },
        {
          url: `${this.globalData.imgPubUrl}Great - unchecked.png`,
          name: '超赞',
          urls: `${this.globalData.imgPubUrl}Great - check.png`,
          isTrue: false
        }
      ],
      techimgarr: [
        {
          url: `${this.globalData.imgPubUrl}Very bad - unchecked.png`,
          name: '非常差',
          urls: `${this.globalData.imgPubUrl}Very bad - selected.png`,
          isTrue: false
        },
        {
          url: `${this.globalData.imgPubUrl}Poor - not selected.png`,
          name: '较差',
          urls: `${this.globalData.imgPubUrl}Poor - selected.png`,
          isTrue: false
        },
        {
          url: `${this.globalData.imgPubUrl}General - unchecked.png`,
          name: '一般',
          urls: `${this.globalData.imgPubUrl}commonly.png`,
          isTrue: false
        },
        {
          url: `${this.globalData.imgPubUrl}Satisfied - not selected.png`,
          name: '满意',
          urls: `${this.globalData.imgPubUrl}Satisfied - selected.png`,
          isTrue: false
        },
        {
          url: `${this.globalData.imgPubUrl}Great - unchecked.png`,
          name: '超赞',
          urls: `${this.globalData.imgPubUrl}Great - check.png`,
          isTrue: false
        }
      ],
      checked: false, // 匿名评价
      up: `${this.globalData.imgPubUrl}camera.png`, // 上传图片
      fileList: [] // 上传图片数组
    }
  },
  methods: {
    // 用户输入的内容
    bindTextAreaBlur(e) {
      this.textvalue = e.mp.detail.value
    },
     // 技师笑脸点击事件
    techSmileClick(mes, index1,index) {
      this.techs[index].techShowMeal = true // 标签的显示
      this.techs[index].Img.forEach(element => {
        element.isTrue = false
      })
      this.techs[index].Img[index1].isTrue = true
      this.delArrVal(techList, index)
      if (this.techs[index].Img[index1].isTrue) {
        let obj = {}
        obj.employeeId = this.techs[index].employeeId
        obj.techHeadUrl = this.techs[index].techHeadUrl
        obj.techName = this.techs[index].techName
        obj.techLevel = this.techs[index].techLevel
        obj.score = index1 + 1
        obj.selectedLabelIds = []
        obj.index = index
        techList.push(obj)
      }
      this.pageGetGetCommentMealtech(1, index1 + 1,index)
    },
    // 技师标签点击事件
    techMealClick(itemt, indext, item, index) {
      this.techs[index].techMealPeople[indext].isChecked = !this.techs[index].techMealPeople[indext].isChecked

      let arr = this.techs[index].techMealPeople.filter(v => {
        return v.isChecked == true
      })
      let arr1 = []
      arr.forEach(element => {
        arr1.push(element.id)
      })
      techList.forEach(element => {
        if (element.employeeId == item.employeeId) {
          element.selectedLabelIds = arr1
        }
      })
    },
    // 门店笑脸点击事件
    storeSmileClick(mes, index1) {
      this.storeShowMeal = true
      this.storeSmileActive = index1
      this.pageGetGetCommentMealone(2, index1 + 1)
    },
    // 门店标签点击事件
    storeMealClick(itemb, indexb) {
      itemb.isChecked = !itemb.isChecked
    },
    delArrVal(arr, val) {
      // 查找数组中的某个值并全部删除    第一个参数是查找的数组 第二个参数是删除的值
      for (let i = 0; i < arr.length; i++) {
        if (arr[i].index == val) {
          arr.splice(i, 1)
          i--
        }
      }
      return arr
    },
    // 商品笑脸点击事件
    shopSmileClick(mes, index1, index) {
      this.orderShopInfo[index].shopShowMeal = true // 标签的显示
      this.orderShopInfo[index].Img.forEach(element => {
        element.isTrue = false
      })
      this.orderShopInfo[index].Img[index1].isTrue = true
      this.delArrVal(list, index)
      if (this.orderShopInfo[index].Img[index1].isTrue) {
        let obj = {}
        obj.orderProductId = this.orderShopInfo[index].orderProductId
        obj.score = index1 + 1
        obj.selectedLabelIds = []
        obj.index = index
        list.push(obj)
      }
      this.pageGetGetCommentMealtwo(3, index1 + 1, index)
    },
    // 商品标签点击事件
    shopMealClick(itema, indexa, item, index) {
      this.orderShopInfo[index].shopMealPeople[indexa].isChecked = !this
        .orderShopInfo[index].shopMealPeople[indexa].isChecked

      let arr = this.orderShopInfo[index].shopMealPeople.filter(v => {
        return v.isChecked == true
      })
      let arr1 = []
      arr.forEach(element => {
        arr1.push(element.id)
      })
      list.forEach(element => {
        if (element.orderProductId == item.orderProductId) {
          element.selectedLabelIds = arr1
        }
      })
    },
    // 删除上传的图片
    delImg(index) {
      this.fileList.splice(index, 1)
    },
    // 上传图片
    upLoad() {
      let that = this
      wx.chooseImage({
        count: 6,
        sizeType: ['original', 'compressed'], // original 原图，compressed 压缩图，默认二者都有
        sourceType: ['album', 'camera'],
        success(res) {
          wx.uploadFile({
            url: 'https://miniapi.aerp.com.cn//QiNiu/UploadStream', // 仅为示例，非真实的接口地址
            filePath: res.tempFilePaths[0],
            name: 'file',
            formData: {
              File: 'file',
              Directory: res.tempFilePaths[0]
            },
            header: {
              'Content-Type': 'multipart/form-data', // 记得设置
              'Content-Type': 'application/json',
              Authorization:
                'Bearer ' + wx.getStorageSync('tokenInfo').accessToken
            },
            success(resa) {
              console.log(`wx.uploadFile`, JSON.parse(resa.data))
              let url = JSON.parse(resa.data).data
              that.fileList.push('https://m.aerp.com.cn/' + url)
            },
            fail(data) {
              console.log('上传错误信息', data)
            }
          })
        }
      })
    },
    // 匿名评价
    onChange() {
      this.checked = !this.checked
    },
    // 加载评价晒单视图
    loadEvaluateView(OrderNo) {
      let that = this
      let rquest = {
        OrderNo: OrderNo
      }
      GetSelection(rquest).then(res => {
        that.carName = res.data.carName
        that.shopName = res.data.shopName
        that.shopInfo = res.data.shop
        
        let techArr = res.data.techs
        techArr.forEach(element => {
          element.Img = JSON.parse(JSON.stringify(that.techimgarr))
          element.techShowMeal = false
          element.techMealPeople = []
        })
        that.techs = techArr
        
        let arr = res.data.products
        arr.forEach(element => {
          element.Img = JSON.parse(JSON.stringify(that.shopimgarr))
          element.shopShowMeal = false
          element.shopMealPeople = []
        })
        that.orderShopInfo = arr
      })
    },
    // 获取点评标签 技师
    pageGetGetCommentMealtech(CommentDetailType, Score,index) {
      let that = this
      let rquest = {
        CommentDetailType: CommentDetailType,
        Score: Score
      }
      GetCommentMeal(rquest)
        .then(res => {
          that.techs[index].techMealPeople = res.data
        })
        .catch(err => {})
    },
    // 获取点评标签
    pageGetGetCommentMealone(CommentDetailType, Score) {
      let that = this
      let rquest = {
        CommentDetailType: CommentDetailType,
        Score: Score
      }
      GetCommentMeal(rquest)
        .then(res => {
          that.storeMealPeople = res.data
        })
        .catch(err => {})
    },
    pageGetGetCommentMealtwo(CommentDetailType, Score, index) {
      let that = this
      let rquest = {
        CommentDetailType: CommentDetailType,
        Score: Score
      }
      GetCommentMeal(rquest)
        .then(res => {
          that.orderShopInfo[index].shopMealPeople = res.data
        })
        .catch(err => {})
    },
    // 提交评价
    commitComment() {
      // wx.navigateBack({})

      let that = this
      that.storeIdarr = []
      this.storeMealPeople.forEach(item1 => {
        if (item1.isChecked) {
          that.storeIdarr.push(item1.id)
        }
      })
      let data = {
        shop: {
          shopId: that.shopInfo.shopId,
          score: that.storeSmileActive + 1,
          selectedLabelIds: that.storeIdarr
        },
        techs:techList,
        products: list,
        connent: that.textvalue,
        isAnonymous: that.checked,
        imageUrls: that.fileList,
        orderNo: that.OrderNo // 'RGC00261'
      }
      commoitSubmission(data)
        .then(res => {
          console.log(`提交评价`, res)
          if (res.code == 10000) {
            wx.showToast({
              title: '评价完成',
              icon: 'none'
            })
            setTimeout(()=>{
              wx.navigateBack({})
            },500)
          }
        })
        .catch(err => {})
    }
  },
  mounted() {
    this.loadEvaluateView(this.OrderNo) // 页面初始加载--加载评价晒单视图
    // this.globalData.orderListStatus = 4
    console.log(this.globalData.orderListStatus, 'orderListStatus4')
  },
  onLoad(options) {
    this.OrderNo = options.OrderNo
  },
  onUnload() {
    this.OrderNo = '' // 订单号
    this.textvalue = '' // 用户评价内容
    this.carName = '' // 车名字
    this.shopName = '' // 门店名字
    this.shopInfo = {} // 门店信息
    this.orderShopInfo = [] // 订单商品信息
    this.storeShowMeal = false // 门店信息点击笑脸显示标签字段
    this.storeMealPeople = [] // 门店标签数组
    this.storeIdarr = [] // 门店标签数组id集合
    this.storeSmileActive = -1 // 门店笑脸索引
    this.shopsmileActive = -1 // 商品笑脸索引

    this.techs=[]//技师信息

    this.checked = false // 匿名评价
    this.fileList = [] // 上传图片数组
    list = []
    techList = []
  }
}
</script>

<style scoped lang="scss">
.demo_pages {
  width: 100%;
  background: #f2f2f2;
}
// 车店信息
.carInfo {
  width: 100%;
  padding: 30rpx;
  background: #fff;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  .car {
    width: 49%;
    height: 30rpx;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .p1 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
    img {
      width: 24rpx;
      height: 24rpx;
    }
  }
  .line {
    border: 3rpx solid rgba(243, 243, 243, 1);
    height: 30rpx;
    margin: 0 10rpx 0 8rpx;
  }
  .stor {
    width: 49%;
    height: 30rpx;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    .p2 {
      font-size: 24rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(51, 51, 51, 1);
    }
    img {
      width: 24rpx;
      height: 24rpx;
    }
  }
}
// 技师css
.page {
  width: 100%;
  background: #fff;
  padding: 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  margin-top: 16rpx;
  .page_top {
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    align-items: center;
    .page_img {
      width: 80rpx;
      height: 80rpx;
      border-radius: 40rpx;
    }
    .page_img1 {
      width: 80rpx;
      height: 80rpx;
    }
    .page_img2 {
      width: 80rpx;
      height: 80rpx;
    }
    .page_two {
      .page_p1 {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 500;
        color: rgba(33, 33, 33, 1);
        margin-left: 20rpx;
        margin-bottom: 6rpx;
      }
      .page_p2 {
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(102, 102, 102, 1);
        margin-left: 20rpx;
      }
    }
  }
  .icon_content {
    display: flex;
    margin-top: 30rpx;
    .content_Icon {
      padding-left: 60rpx;
      padding-bottom: 30rpx;
      .content_icon {
        img {
          width: 72rpx;
          height: 72rpx;
        }
      }
      .content_name {
        font-size: 24rpx;
        color: #999999;
        text-align: center;
      }
      .content_nameColor {
        color: rgba(255, 99, 36, 1);
      }
    }
  }
  .page_meal {
    width: 100%;
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: flex-start;
    align-items: center;
    .one {
      width: 204rpx;
      height: 68rpx;
      border: 1rpx solid rgba(153, 153, 153, 1);
      border-radius: 10rpx;
      text-align: center;
      line-height: 66rpx;
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(153, 153, 153, 1);
      margin-right: 23rpx;
      margin-bottom: 21rpx;
    }
    .oneColor {
      border: 1rpx solid rgba(255, 99, 36, 1);
      color: rgba(255, 99, 36, 1);
    }
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
  height: 150rpx;
  margin: 0 auto;
}
.Obtain {
  width: 90%;
  margin: 0 auto;
  font-size: 20rpx;
  color: #999999;
}
.bottom_btn {
  width: 100%;
  height: 100rpx;
  background: #fff;
  position: fixed;
  bottom: 0;
  left: 0;
}
.bottom_btn > button {
  width: 90%;
  height: 75rpx;
  background: #ff6324;
  color: #fff;
  margin: 16rpx auto;
  line-height: 75rpx;
  border-radius: 38rpx;
}
.add_img {
  // width: 90%;
  width: 120rpx;
  height: 120rpx;
  margin: 30rpx;
  font-size: 20rpx;
  color: #999999;
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
.anonymous {
  width: 50%;
  margin: 30rpx;
}
.footer {
  width: 100%;
  margin-top: 30rpx;
  background: #fff;
  padding: 20rpx 0;
}
.footer > p {
  width: 690rpx;
  height: 75rpx;
  background: rgba(255, 99, 36, 1);
  border-radius: 38px;
  margin: 0 auto;
  color: #ffffff;
  line-height: 75rpx;
  text-align: center;
}
.icon_content {
  display: flex;
}
.content_Icon {
  padding-left: 60rpx;
  padding-bottom: 30rpx;
}
.content_icon > img {
  width: 72rpx;
  height: 72rpx;
}
.content_name {
  font-size: 24rpx;
  color: #999999;
  text-align: center;
}
</style>
