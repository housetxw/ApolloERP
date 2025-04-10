<template>
  <div class="demo_page">
    <Swiper :images="images" />
    <div class="top_bao">
      <img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_safe_icon.png" alt class="img1" />
      <div class="top_one">
        <img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_hook_icon.png" alt />
        <text>全正品</text>
      </div>
      <div class="top_one">
        <img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_hook_icon.png" alt />
        <text>价格低</text>
      </div>
      <div class="top_one">
        <img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_hook_icon.png" alt />
        <text>放心购</text>
      </div>
      <div class="top_one">
        <img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_hook_icon.png" alt />
        <text>快送达</text>
      </div>
      <img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_more_icon.png" alt class="img2" />
    </div>
    <div class="top_introduce">
      <div>
        <P class="p1">
          <text class="top_txt1">￥689.00</text>
          <text class="top_txt2">￥589.00</text>
        </P>
        <p class="p2">
          <img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_share_icon.png" alt />
          <text>分享</text>
        </p>
      </div>
      <p class="p3">【正品授权】美孚/Mobil 美孚1号全合成机油 5W-30SN级（4L装）</p>
      <p class="p4">新老包装更替中，实物包装可能与图片略有区别</p>
    </div>
    <div class="top_discount">
      <p class="top_txt1">优惠:</p>
      <p class="top_txt2">
        <text class="top_txt">满减</text>
        <text class="top_txt">积分可用</text>
        <text class="top_txt">积分可用</text>
      </p>
      <img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_share_black.png" alt />
    </div>
    <div class="top_pIcc">
      <div class="txt">
        <p class="txt1">AERP正品保 PICC中国人保承担</p>
        <p class="txt2">30亿正品险担保 最高赔付200万</p>
      </div>
      <img
        src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png"
        alt
        style="width:24rpx;height:24rpx;margin-left:15rpx;"
      />
    </div>
    <div class="top_selected">
      <text>已选</text>
      <p>商品规格，区分不同的商品，商品规则内 全部显示，支持折行，的发挥地方体会让他回复的过分的话可选服务</p>
      <div>
        <img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_share_black.png" alt />
      </div>
    </div>
    <div class="top_card">
      <text class="top_card_txt1">车型</text>
      <div>
        <p>此商品适配车型</p>
        <text>上海汽车-eRX5 1.5T-混合力 2017 2017款 1.5T 自动而特色我要让他巨化股份服地黄方安慰人无尊贵旗舰版</text>
      </div>
      <img
        src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png"
        alt
        style="width:24rpx;height:24rpx;margin-left:15rpx;"
      />
    </div>
    <div class="top_service">
      <text class="txt">服务</text>
      <p>
        <text class="txt1">专业技师安装 | ¥ 0.00</text>
        <text class="txt2">无需服务</text>
      </p>
    </div>
    <div class="top_kucun">
      <text>库存</text>
      <p>有货</p>
    </div>
    <div class="top_card">
      <text class="top_card_txt1">门店</text>
      <div>
        <p>门店名称门店名称门店名称，全部展示发热加工如果和人均突破还让他电视里放快递支持折行</p>
        <text>上海市闵行区莲花南路3000号普洛斯闵行物流园 B3库，地址，全部展示，支持折行</text>
      </div>
      <img
        src="https://m.aerp.com.cn/mini-app-main/maintenance_jump_icon.png"
        alt
        style="width:24rpx;height:24rpx;margin-left:15rpx;"
      />
    </div>
    <div class="activeTitle">
      <img src="https://m.aerp.com.cn/mini-app-main/title_left_icon.png" alt class="activeImg" />
      <span class="activeWord">超值活动</span>
      <img src="https://m.aerp.com.cn/mini-app-main/title_right_icon.png" alt class="activeImg" />
    </div>
    <div class="bottom_img">
      <img src="https://m.aerp.com.cn/mini-app-main/goodsdetail_banner_pic.png" alt />
    </div>
    <div class="btn">
      <van-button round color="#FF6324" type="default" size="large" @tap="selectClick">选择</van-button>
    </div>
    <div class="addBtn_one">
      <img :src="src1" alt class="img_img" />
      <img :src="src2" alt class="img_img" />
      <img :src="src3" alt class="img_img" />
      <div>
        <p class="addBtn_p1">加入【测试车】</p>
        <p class="addBtn_p2">
          <text class="txt1">按车型【测试】</text>
          <text class="txt2">到店安装</text>
        </p>
      </div>
      <div class="posi_img">2</div>
    </div>
    <div class="addBtn_two">
      <img :src="src4" alt class="img_img" />
      <img :src="src2" alt class="img_img" />
      <img :src="src3" alt class="img_img" />
      <p class="two_wenzi">
        <text>按车型【测试】</text>
        <text>按车型适配到店安装</text>
      </p>
      <div class="posi_img">2</div>
    </div>
    <div class="addBtn_three">
      <img :src="src1" alt class="img_img" />
      <img :src="src2" alt class="img_img" />
      <img :src="src3" alt class="img_img" />
      <div class="three_btn">
        <p class="p1">加入【测试车】</p>
        <p class="p2">立即【测试】</p>
      </div>
      <div class="posi_img">2</div>
    </div>
  </div>
</template>
<script>
import Swiper from '@/components/swiper'
export default {
  components: { Swiper },
  data() {
    return {
      images: [
        {
          url: `${this.globalData.imgPubUrl}goodsdetail_banner_pic.png`,
          num: 16546345643645136,
          num1: 1
        },
        {
          url: `${this.globalData.imgPubUrl}goodsdetail_banner_pic.png`,
          num: 967895341432165,
          num1: 2
        },
        {
          url: `${this.globalData.imgPubUrl}goodsdetail_banner_pic.png`,
          num: 967895341432165,
          num1: 3
        },
        {
          url: `${this.globalData.imgPubUrl}goodsdetail_banner_pic.png`,
          num: 967895341432165,
          num1: 4
        }
      ],
      src1: `${this.globalData.imgPubUrl}goodsdetail_collect.png`,
      src2: `${this.globalData.imgPubUrl}maintenance_tel_icon.png`,
      src3: `${this.globalData.imgPubUrl}goodsdetail_cart_icon.png`,
      src4: `${this.globalData.imgPubUrl}goodsdetail_collect_click.png`
    }
  },
  onLoad() {
    let that = this
    // 获取系统信息
    wx.getSystemInfo({
      success: function(res) {
        // 获取可使用窗口宽度
        let clientHeight = res.windowHeight
        // 获取可使用窗口高度
        let clientWidth = res.windowWidth
        // 算出比例
        let ratio = 750 / clientWidth
        // 算出高度(单位rpx)
        that.windowHeight = clientHeight * ratio - 88 + 'rpx'
      }
    })
  },
  methods: {}
}
</script>

<style scoped lang="scss">
.demo_page {
  width: 100%;
  // height: 100%;
  display: flex;
  flex-direction: column;
  background: #f3f3f3;
}
.shop_shop {
  position: fixed;
  top: 0;
  left: 0;
  z-index: 9999;
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
      .shop_service_twotxt {
        width: 290rpx;
        height: 60rpx;
        background: rgba(255, 238, 231, 1);
        border: 1rpx solid rgba(255, 193, 173, 1);
        border-radius: 30rpx;
        text-align: center;
        line-height: 58rpx;
        font-size: 26rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 99, 36, 1);
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

.top_bao {
  width: 100%;
  height: 80rpx;
  background: rgba(255, 233, 224, 1);
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  padding: 0 30rpx;
  box-sizing: border-box;
}
.top_one {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}
.top_one img {
  width: 25rpx;
  height: 25rpx;
}
.top_one text {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
}
.img1 {
  width: 44rpx;
  height: 44rpx;
}
.img2 {
  width: 32rpx;
  height: 32rpx;
}
.top_introduce {
  width: 100%;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  padding: 40rpx 0 30rpx 0;
  box-sizing: border-box;
}
.top_introduce div {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  padding-left: 30rpx;
  box-sizing: border-box;
}
.p1 .top_txt1 {
  font-size: 46rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(255, 99, 36, 1);
}
.p1 .top_txt2 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  text-decoration: line-through;
  color: rgba(173, 173, 173, 1);
}
.p2 {
  width: 125rpx;
  height: 55rpx;
  text-align: center;
  background: rgba(247, 245, 244, 1);
  border-radius: 28rpx 0rpx 0rpx 28rpx;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}
.p2 img {
  width: 28rpx;
  height: 28rpx;
}
.p2 text {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(33, 33, 33, 1);
}
.p3 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 500;
  color: rgba(50, 50, 50, 1);
  width: 100%;
  padding-left: 30rpx;
  box-sizing: border-box;
  margin-top: 40rpx;
  margin-bottom: 35rpx;
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
}
.p4 {
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(173, 173, 173, 1);
  width: 100%;
  padding-left: 30rpx;
  box-sizing: border-box;
}
.top_discount {
  width: 100%;
  height: 100rpx;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  margin-top: 16rpx;
  margin-bottom: 16rpx;
  padding: 0 30rpx;
  box-sizing: border-box;
}
.top_discount .top_txt1 {
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 500;
  color: rgba(51, 51, 51, 1);
}
.top_discount .top_txt2 {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}
.top_discount img {
  width: 32rpx;
  height: 32rpx;
}
.top_discount .top_txt2 .top_txt {
  width: 120rpx;
  height: 35rpx;
  border: 1rpx solid rgba(36, 144, 255, 1);
  border-radius: 4rpx;
  text-align: center;
  line-height: 35rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(36, 144, 255, 1);
  margin-right: 10rpx;
}
.top_pIcc {
  width: 100%;
  height: 140rpx;
  background: rgba(255, 255, 255, 1);
  padding: 35rpx 30rpx 35rpx 35rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
}
.top_pIcc .txt {
  flex: 2;
}
.top_pIcc .txt .txt1 {
  font-size: 30rpx;
  font-family: Source Han Sans CN;
  font-weight: 500;
  color: rgba(51, 51, 51, 1);
}
.top_pIcc .txt .txt2 {
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(173, 173, 173, 1);
  margin-top: 18rpx;
}
.top_selected {
  width: 100%;
  height: 140rpx;
  background: rgba(255, 255, 255, 1);
  padding: 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: flex-start;
}
.top_selected text {
  width: 146rpx;
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 500;
  color: rgba(51, 51, 51, 1);
}
.top_selected p {
  width: 500rpx;
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
.top_selected div {
  width: 50rpx;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.top_selected div img {
  width: 32rpx;
  height: 32rpx;
}
.top_card {
  width: 100%;
  padding: 30rpx;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: flex-start;
  background: rgba(255, 255, 255, 1);
}
.top_card .top_card_txt1 {
  width: 146rpx;
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 500;
  color: rgba(51, 51, 51, 1);
}
.top_card div {
  width: 500rpx;
}
.top_card div p {
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(92, 198, 130, 1);
  overflow: hidden;
  white-space: pre-wrap;
  display: -webkit-box;
  text-overflow: ellipsis;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
}
.top_card div text {
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
.top_service {
  width: 100%;
  height: 100rpx;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: center;
  padding: 20rpx 30rpx;
  box-sizing: border-box;
}
.top_service .txt {
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 500;
  color: rgba(51, 51, 51, 1);
}
.top_service p {
  margin-left: 62rpx;
}
.top_service p .txt1 {
  width: 390rpx;
  height: 60rpx;
  background: rgba(255, 238, 231, 1);
  border: 1rpx solid rgba(255, 193, 173, 1);
  border-radius: 30rpx;
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
  text-align: center;
}
.top_service p .txt2 {
  width: 200rpx;
  height: 60rpx;
  background: rgba(237, 237, 237, 1);
  border-radius: 30rpx;
  font-size: 26rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(136, 136, 136, 1);
  margin-left: 20rpx;
}
.top_kucun {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: flex-start;
  padding: 30rpx;
  box-sizing: border-box;
  background: rgba(255, 255, 255, 1);
}
.top_kucun text {
  flex: 1;
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 500;
  color: rgba(51, 51, 51, 1);
}
.top_kucun p {
  flex: 2;
  font-size: 28rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(51, 51, 51, 1);
}
.activeTitle {
  display: flex;
  align-items: center;
  width: 100%;
  height: 90rpx;
  margin-top: 21rpx;
  justify-content: center;
  background: rgba(255, 255, 255, 1);
  margin-top: 16rpx;
}
.activeImg {
  width: 25rpx;
  height: 25rpx;
}
.activeWord {
  font-size: 32rpx;
  font-family: Source Han Sans CN;
  font-weight: bold;
  color: rgba(33, 33, 33, 1);
  margin: 0 15rpx;
}
.bottom_img {
  width: 100%;
  height: 750rpx;
}
.bottom_img img {
  width: 100%;
  height: 100%;
}
.btn {
  width: 100%;
  /* height:98rpx; */
  background: rgba(255, 255, 255, 1);
  padding: 10rpx 30rpx;
  box-sizing: border-box;
  margin-top: 20rpx;
}
.addBtn_one {
  position: relative;
  width: 750rpx;
  height: 98rpx;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-direction: row;
  justify-content: space-around;
  align-items: center;
  padding: 0 30rpx;
  box-sizing: border-box;
  margin-top: 16rpx;
  .img_img {
    width: 42rpx;
    height: 42rpx;
  }
  div {
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
    .addBtn_p1 {
      width: 210rpx;
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
    .addBtn_p2 {
      width: 210rpx;
      height: 75rpx;
      background: rgba(255, 99, 36, 1);
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      border-radius: 1rpx 38rpx 38rpx 0rpx;
      .txt1 {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 255, 255, 1);
      }
      .txt2 {
        font-size: 22rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(255, 189, 162, 1);
      }
    }
  }
  .posi_img {
    position: absolute;
    top: 25rpx;
    left: 227rpx;
    width: 22rpx;
    height: 22rpx;
    background: rgba(248, 80, 80, 1);
    border-radius: 50%;
    text-align: center;
    line-height: 22rpx;
    font-size: 16rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(255, 255, 255, 1);
  }
}
.addBtn_two {
  position: relative;
  width: 750rpx;
  height: 98rpx;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-direction: row;
  justify-content: space-around;
  align-items: center;
  padding: 0 30rpx;
  box-sizing: border-box;
  margin-top: 16rpx;
  .img_img {
    width: 42rpx;
    height: 42rpx;
  }
  .two_wenzi {
    width: 420rpx;
    height: 75rpx;
    background: rgba(255, 99, 36, 1);
    border-radius: 37rpx 38rpx 38rpx 37rpx;
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
  .posi_img {
    position: absolute;
    top: 25rpx;
    left: 227rpx;
    width: 22rpx;
    height: 22rpx;
    background: rgba(248, 80, 80, 1);
    border-radius: 50%;
    text-align: center;
    line-height: 22rpx;
    font-size: 16rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(255, 255, 255, 1);
  }
}
.addBtn_three {
  position: relative;
  width: 750rpx;
  height: 98rpx;
  background: rgba(255, 255, 255, 1);
  display: flex;
  flex-direction: row;
  justify-content: space-around;
  align-items: center;
  padding: 0 30rpx;
  box-sizing: border-box;
  margin-top: 16rpx;
  .img_img {
    width: 42rpx;
    height: 42rpx;
  }
  .three_btn {
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
    p {
      width: 210rpx;
      height: 75rpx;
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(255, 255, 255, 1);
      text-align: center;
      line-height: 75rpx;
    }
    .p1 {
      background: rgba(255, 170, 70, 1);
      border-radius: 38rpx 1rpx 0rpx 38rpx;
    }
    .p2 {
      background: rgba(255, 99, 36, 1);
      border-radius: 1px 38px 38px 0px;
    }
  }
  .posi_img {
    position: absolute;
    top: 25rpx;
    left: 227rpx;
    width: 22rpx;
    height: 22rpx;
    background: rgba(248, 80, 80, 1);
    border-radius: 50%;
    text-align: center;
    line-height: 22rpx;
    font-size: 16rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(255, 255, 255, 1);
  }
}
</style>