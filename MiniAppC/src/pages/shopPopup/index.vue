<template>
  <div class="demo_page">
    <!-- 最外层盒子 -->
    <div class="shop_shop" v-if="imgdis">
      <!-- 第二层盒子（内容） -->
      <div class="shop_box">
        <!-- 标题 -->
        <div class="shop_top">
          <img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_banner_pic.png" alt class="img1" />
          <div class="shop_content">
            <p class="shop_content_p1">商品名称商品名称商品名称商品 名称，需要全部展示</p>
            <p class="shop_content_p2">
              <text class="txt1">¥ 689.00</text>
              <text class="txt2">¥ 589.00</text>
            </p>
          </div>
          <img
            src="https://m.aerp.com.cn/mini-app-main/goodsdetail_close_black.png"
            alt
            @tap="disimgClick"
            class="img2"
          />
        </div>
        <div v-for="(itemaa,indexaa) in specifications" :key="indexaa">
          <p class="shop_ttitle">{{itemaa.name}}</p>
          <div class="shop_attribute">
            <text
              class="shop_radios"
              v-for="(item,index) in itemaa.items"
              :key="index"
              @tap="AttributeClick(item)"
            >{{item.name}}</text>
          </div>
          <!-- <p class="shop_ttitle_two">属性2</p>
          <div class="shop_attribute_two">
            <text class="shop_radios_two" v-for="(item1,index1) in attrArrTwo" :key="index1" @tap="BttributeClick(item1)">{{item1}}</text>
          </div>-->
        </div>
        <!-- 车型 -->
        <div class="card_type" @tap="cardallClick">
          <text class="card_type_onetxt">车型</text>
          <text class="card_type_twotxt" v-if="card.cartitle == ''" @tap="CardClick">+ 添加车型</text>
          <div class="haveInfor" v-else>
            <p class="have_top">{{card.status}}</p>
            <div class="have_bottom">
              <p class="have_one">{{card.cartitle}}</p>
              <img
                src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png"
                alt
                style="width:24rpx;height:24rpx;margin-left:15rpx;"
              />
            </div>
          </div>
        </div>
        <!-- <p class="shop_service">
          <text class="shop_service_onetxt">服务</text>
          <text :class="['shop_service_twotxt',{'yuan_bg':a == 1?true:false}]" @tap="oneClick()">专业技师安装 | ¥ 0.00</text>
          <text :class="['shop_service_threetxt',{'yuan_bg':a == 2?true:false}]" @tap="twoClick()">无需服务</text>
        </p>-->
        <p class="shop_stock">
          <text class="shop_stock_onetxt">库存</text>
          <text class="shop_stock_twotxt">{{havegoods}}</text>
        </p>
        <!-- 门店 -->
        <div class="shop_store" @tap="storallClick">
          <text class="shop_store_onetxt">门店</text>
          <text class="shop_store_twotxt" v-if="stor.title == ''" @tap="selectStor">+ 选择门店</text>
          <div class="haveInfor" v-else>
            <p class="have_top">{{stor.title}}</p>
            <div class="have_bottom">
              <p class="have_one">{{stor.address}}</p>
              <img
                src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png"
                alt
                style="width:24rpx;height:24rpx;margin-left:15rpx;"
              />
            </div>
          </div>
        </div>
        <p class="shop_num">
          <text class="shop_num_onetxt">数量</text>
          <van-stepper v-model="shopvalue" />
        </p>
      </div>
      <!-- 按钮 -->
      <div class="btn">
        <p class="p1" @tap="addshopCard">加入购物车</p>
        <p class="p2" @tap="CardType">
          <text>按车型购买</text>
          <text>到店安装</text>
        </p>
      </div>
      <!-- 弹窗按钮一 -->
      <div class="position" @tap="Besure">
        <p class="sure_one">确定</p>
      </div>
      <!-- 弹窗按钮二 -->
      <div class="position_two" @tap="CardStore">
        <p class="pos_twp">
          <text>按车型购买</text>
          <text>按车型适配到店安装</text>
        </p>
      </div>
    </div>
  </div>
</template>
<script>
import { GetSearchSelectShop } from '../../api'
export default {
  data() {
    return {
      specifications: [], //属性数组
      shopvalue: 2,
      imgdis: true,
      // 车型
      card: {
        status: '此商品不匹配车型',
        cartitle: '上海汽车-eRX5 1.5T-混合力 2017 2017款 1.5T 自动尊贵旗舰版'
      },
      // 门店
      stor: {
        title: '门店名称门店名称门店名称,全部展示,支持折行',
        address:
          '上海市闵行区莲花南豆腐干豆腐路3000号普洛斯闵行物流园 B3库，地址，全部展示，支持折行'
      },
      havegoods: '有货', //库存是否有货
      a: 1
    }
  },
  methods: {
    // 弹窗消失事件
    disimgClick() {
      this.imgdis = false
    },
    // 属性1点击事件
    AttributeClick(item) {},
    // 属性2点击事件
    BttributeClick(item1) {},
    // 加入购物车
    addshopCard() {},
    // 按车型购买
    CardType() {},
    // 确定
    Besure() {},
    // 按车型适配到店安装
    CardStore() {},
    // 选择门店事件
    selectStor() {},
    // 选择车型事件
    CardClick() {},
    // 车型跳页
    cardallClick() {},
    // 门店跳页
    storallClick() {}
    // 技师安装
    // oneClick(){

    //   this.a = 1
    // },
    // twoClick(){

    //   this.a = 2
    // }
  },
  onLoad(options) {
    let that = this
    let rquest = {
      productCode: 'BYFW-JY-TSO-150'
    }
    // 商品规格接口 查询是否设置关联商品
    GetSearchSelectShop(rquest)
      .then(res => {
        that.specifications = res.data.specifications
      })
      .catch(err => {})
  }
}
</script>
<style scoped lang="scss">
.demo_page {
  height: 100%;
}
.shop_shop {
  width: 750rpx;
  padding-top: 335rpx;
  box-sizing: border-box;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  flex-direction: column;
  justify-content: flex-end;
  align-items: center;
  .shop_box {
    width: 750rpx;
    background: #ffffff;
    display: flex;
    flex-direction: column;
    padding: 30rpx;
    box-sizing: border-box;
    .shop_top {
      width: 100%;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: flex-start;
      .shop_content {
        width: 416rpx;
        .shop_content_p1 {
          width: 100%;
          font-size: 30rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(33, 33, 33, 1);
        }
        .shop_content_p2 {
          width: 100%;
          .txt1 {
            font-size: 46rpx;
            font-family: Source Han Sans CN;
            font-weight: 400;
            color: rgba(255, 99, 36, 1);
          }
          .txt2 {
            font-size: 24rpx;
            font-family: Source Han Sans CN;
            font-weight: 400;
            text-decoration: line-through;
            color: rgba(173, 173, 173, 1);
            margin-left: 10rpx;
          }
        }
      }
      .img1 {
        width: 150rpx;
        height: 150rpx;
      }
      .img2 {
        width: 26rpx;
        height: 26rpx;
      }
    }
    .shop_ttitle,
    .shop_ttitle_two {
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 500;
      color: rgba(51, 51, 51, 1);
      margin: 30rpx 0;
    }
    .shop_attribute {
      width: 100%;
      display: flex;
      flex-direction: row;
      flex-wrap: wrap;
      justify-content: flex-start;
      align-items: flex-start;
      border-bottom: 1rpx solid #f3f3f3;
      padding-bottom: 18rpx;
      box-sizing: border-box;
      .shop_radios {
        width: 140rpx;
        height: 60rpx;
        background: rgba(255, 238, 231, 1);
        border: 1rpx solid rgba(255, 193, 173, 1);
        border-radius: 30rpx;
        margin: 0 28rpx 18rpx 0;
        text-align: center;
        line-height: 58rpx;
        font-size: 26rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(136, 136, 136, 1);
      }
    }
    .shop_attribute_two {
      width: 100%;
      display: flex;
      flex-direction: row;
      flex-wrap: wrap;
      justify-content: flex-start;
      align-items: flex-start;
      border-bottom: 1rpx solid #f3f3f3;
      padding-bottom: 18rpx;
      box-sizing: border-box;
      .shop_radios_two {
        width: 140rpx;
        height: 60rpx;
        background: rgba(255, 238, 231, 1);
        border: 1rpx solid rgba(255, 193, 173, 1);
        border-radius: 30rpx;
        margin: 0 28rpx 18rpx 0;
        text-align: center;
        line-height: 58rpx;
        font-size: 26rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(136, 136, 136, 1);
      }
    }
    .card_type {
      width: 100%;
      // height: 100rpx;
      padding-top: 36rpx;
      padding-bottom: 36rpx;
      box-sizing: border-box;
      display: flex;
      flex-direction: row;
      justify-content: flex-start;
      align-items: flex-start;
      border-bottom: 1rpx solid #f3f3f3;
      .card_type_onetxt {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 500;
        color: rgba(51, 51, 51, 1);
        margin-right: 62rpx;
      }
      .card_type_twotxt {
        width: 200rpx;
        height: 60rpx;
        border: 1rpx solid rgba(173, 173, 173, 1);
        opacity: 0.99;
        border-radius: 30rpx;
        text-align: center;
        line-height: 58rpx;
        font-size: 26rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
      }
      .haveInfor {
        width: 550rpx;
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        align-items: flex-start;
        .have_top {
          width: 100%;
          font-size: 24rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(51, 51, 51, 1);
          overflow: hidden;
          white-space: pre-wrap;
          display: -webkit-box;
          text-overflow: ellipsis;
          -webkit-line-clamp: 2;
          -webkit-box-orient: vertical;
        }
        .have_bottom {
          display: flex;
          flex-direction: row;
          justify-content: flex-start;
          align-items: flex-start;
          margin-top: 20rpx;
          .have_one {
            width: 100%;
            font-size: 28rpx;
            font-family: Source Han Sans CN;
            font-weight: 400;
            color: rgba(240, 60, 60, 1);
            overflow: hidden;
            white-space: pre-wrap;
            display: -webkit-box;
            text-overflow: ellipsis;
            -webkit-line-clamp: 2;
            -webkit-box-orient: vertical;
          }
        }
      }
    }
    .shop_service {
      width: 100%;
      height: 100rpx;
      display: flex;
      flex-direction: row;
      justify-content: flex-start;
      align-items: center;
      border-bottom: 1rpx solid #f3f3f3;
      .shop_service_onetxt {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 500;
        color: rgba(51, 51, 51, 1);
        margin-right: 62rpx;
      }
    }
    .shop_stock {
      width: 100%;
      height: 100rpx;
      display: flex;
      flex-direction: row;
      justify-content: flex-start;
      align-items: center;
      border-bottom: 1rpx solid #f3f3f3;
      .shop_stock_onetxt {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 500;
        color: rgba(51, 51, 51, 1);
        margin-right: 62rpx;
      }
      .shop_stock_twotxt {
        font-size: 26rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
      }
    }
    .shop_store {
      width: 100%;
      // height: 100rpx;
      padding-top: 36rpx;
      padding-bottom: 36rpx;
      box-sizing: border-box;
      display: flex;
      flex-direction: row;
      justify-content: flex-start;
      align-items: flex-start;
      border-bottom: 1rpx solid #f3f3f3;
      .shop_store_onetxt {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 500;
        color: rgba(51, 51, 51, 1);
        margin-right: 62rpx;
      }
      .haveInfor {
        width: 550rpx;
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        align-items: flex-start;
        .have_top {
          width: 100%;
          font-size: 28rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(51, 51, 51, 1);
          overflow: hidden;
          white-space: pre-wrap;
          display: -webkit-box;
          text-overflow: ellipsis;
          -webkit-line-clamp: 2;
          -webkit-box-orient: vertical;
        }
        .have_bottom {
          display: flex;
          flex-direction: row;
          justify-content: flex-start;
          align-items: flex-start;
          margin-top: 20rpx;
          .have_one {
            width: 100%;
            font-size: 28rpx;
            font-family: Source Han Sans CN;
            font-weight: 400;
            color: rgba(240, 60, 60, 1);
            overflow: hidden;
            white-space: pre-wrap;
            display: -webkit-box;
            text-overflow: ellipsis;
            -webkit-line-clamp: 2;
            -webkit-box-orient: vertical;
          }
        }
      }
      .shop_store_twotxt {
        width: 200rpx;
        height: 60rpx;
        border: 1rpx solid rgba(173, 173, 173, 1);
        opacity: 0.99;
        border-radius: 30rpx;
        text-align: center;
        line-height: 58rpx;
        font-size: 26rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
      }
    }
    .shop_num {
      width: 100%;
      height: 100rpx;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      border-bottom: 1rpx solid #f3f3f3;
      .shop_num_onetxt {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 500;
        color: rgba(51, 51, 51, 1);
        margin-right: 62rpx;
      }
    }
  }
  .btn {
    width: 100%;
    padding: 12rpx 30rpx;
    box-sizing: border-box;
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
    background: #ffffff;
    .p1 {
      width: 345rpx;
      height: 75rpx;
      background: rgba(255, 170, 70, 1);
      border-radius: 38rpx 1rpx 0rpx 38rpx;
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(255, 255, 255, 1);
      text-align: center;
      line-height: 75rpx;
    }
    .p2 {
      width: 345rpx;
      height: 75rpx;
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      background: rgba(255, 99, 36, 1);
      border-radius: 1rpx 38rpx 38rpx 0rpx;
      text:nth-child(1) {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 255, 255, 1);
      }
      text:nth-child(2) {
        font-size: 22rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 189, 162, 1);
      }
    }
  }
  .position {
    width: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    background: #ffffff;
    .sure_one {
      width: 690rpx;
      height: 75rpx;
      background: rgba(255, 99, 36, 1);
      border-radius: 38rpx;
      text-align: center;
      line-height: 75rpx;
      font-size: 32rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(255, 254, 254, 1);
    }
  }
  .position_two {
    width: 750rpx;
    height: 98rpx;
    background: rgba(255, 255, 255, 1);
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    .pos_twp {
      width: 690rpx;
      height: 75rpx;
      background: rgba(255, 99, 36, 1);
      border-radius: 38rpx;
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      text:nth-child(1) {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 255, 255, 1);
      }
      text:nth-child(2) {
        font-size: 22rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 189, 162, 1);
      }
    }
  }
}
.shop_service_twotxt {
  width: 290rpx;
  height: 60rpx;
  border-radius: 30rpx;
  text-align: center;
  line-height: 58rpx;
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  background: rgba(237, 237, 237, 1);
  color: rgba(136, 136, 136, 1);
  margin-right: 20rpx;
}
.shop_service_threetxt {
  width: 150rpx;
  height: 60rpx;
  background: rgba(237, 237, 237, 1);
  border-radius: 30rpx;
  text-align: center;
  line-height: 60rpx;
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
}
.yuan_bg {
  background: rgba(255, 238, 231, 1);
  border: 1rpx solid rgba(255, 193, 173, 1);
  color: rgba(255, 99, 36, 1);
}
</style>