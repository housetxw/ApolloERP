<template>
  <div class="demo_page">
    <div class="title_tab">
      <van-tabs v-model="active" sticky swipeable color="#FF6324" line-width="40">
        <van-tab title="配送到店">
          <div class="top_title">
            <van-dropdown-menu active-color="red">
              <van-dropdown-item
                title="上海市"
                v-model="value1"
                :options="option1"
                @change="onChange"
              />
              <van-dropdown-item title="默认排序" v-model="value2" :options="option2" />
            </van-dropdown-menu>
          </div>
          <!-- 一个门店 -->
          <div class="mealbox" v-for="(item, index) in storearr" :key="index">
            <div class="box">
              <div class="top_img" @tap="radioClick(item)">
                <img :src="item.switchdis?item.src1:item.src" alt class="img1" />
              </div>
              <div class="top_one">
                <div class="top_images">
                  <img
                    src="https://m.aerp.com.cn/mini-RG-main/order_goods_img.png"
                    alt
                    class="img1"
                  />
                </div>
                <div class="top_content">
                  <p class="p1">{{item.title}}</p>
                  <div class="top_center">
                    <p class="p3">{{item.text}}</p>
                    <div class="top_all">
                      <img
                        src="https://m.aerp.com.cn/mini-RG-main/order_navigation_icon.png"
                        alt
                        class="img2"
                      />
                      <text class="txt3">{{item.km}}KM</text>
                    </div>
                  </div>
                  <div class="p2">
                    <p class="txt1">
                      总评分：
                      <span>{{item.fen}}</span>
                    </p>
                    <p class="txt1 txt2">
                      总订单：
                      <span>{{item.ordernum}}</span>
                    </p>
                  </div>
                </div>
              </div>
            </div>
            <div class="top_three">
              <text
                style="font-size:24rpx;font-family:Source Han Sans CN;font-weight:400;color:rgba(102,102,102,1);"
              >门店服务项目：</text>
              <text class="metal" v-for="(item1, index1) in item.mealArr" :key="index1">{{item1}}</text>
            </div>
          </div>
        </van-tab>
        <van-tab title="配送到家">
          <div class="address">
            <div v-for="(items,indexs) in addressArr" :key="indexs">
              <van-swipe-cell id="swipe-cell" right-width="65" @close="onClose">
                <div class="top_one">
                  <div class="top_content">
                    <div class="p1">
                      <text class="txt1">{{items.title}}</text>
                      <text class="txt2">{{items.phoneNumber}}</text>
                      <text :class="[{'distxt1':items.switchdis}]">{{items.switchdis?'默认':''}}</text>
                      <text class="txt3">{{items.meal}}</text>
                    </div>
                    <p class="p3">{{items.licensePlate}}</p>
                  </div>
                  <div class="top_images2" @tap="editClcik">
                    <img :src="items.src2" alt class="img2" />
                  </div>
                </div>
                <div slot="right" class="clear">删除</div>
              </van-swipe-cell>
            </div>
          </div>
          <div class="position" @tap="addressClick">
            <van-button round size="large" color="#FF6324" type="default">＋ 新增收货地址</van-button>
          </div>
        </van-tab>
      </van-tabs>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      active: 1,
      value1: 0,
      value2: 'a',
      windowHeight: '',
      option1: [
        { text: '全部商品', value: 0 },
        { text: '新款商品', value: 1 },
        { text: '活动商品', value: 2 }
      ],
      option2: [
        { text: '默认排序', value: 'a' },
        { text: '好评排序', value: 'b' },
        { text: '销量排序', value: 'c' }
      ],
      storearr: [
        {
          src: `${this.globalData.imgPubUrl}maintenance_radio.png`,
          src1: `${this.globalData.imgPubUrl}maintenance_radio_click.png`,
          switchdis: false,
          title: '致大养车-黄山店',
          text: '安徽省黄山市黄山区205国道',
          km: '2.8',
          fen: '5.0',
          ordernum: 1688,
          mealArr: ['保养', '洗车', '美容', '轮胎', '电池']
        },
        {
          src: `${this.globalData.imgPubUrl}maintenance_radio.png`,
          src1: `${this.globalData.imgPubUrl}maintenance_radio_click.png`,
          switchdis: false,
          title: '致大养车-黄山店【迎客松路】',
          text: '安徽省黄山市黄山区205国道',
          km: '2.8',
          fen: '5.0',
          ordernum: 1688,
          mealArr: ['保养', '洗车', '美容', '轮胎', '电池']
        }
      ],
      addressArr: [
        {
          title: '王珊',
          phoneNumber: '1875182475',
          licensePlate: '上海普陀区真北路2999弄77号401室',
          src2: `${this.globalData.imgPubUrl}order_edit_icon.png`,
          switchdis: true,
          meal: '家'
        },
        {
          title: '王珊',
          phoneNumber: '1875182475',
          licensePlate: '上海普陀区真北路2999弄77号401室',
          src2: `${this.globalData.imgPubUrl}order_edit_icon.png`,
          switchdis: false,
          meal: '学校'
        }
      ]
    }
  },
  methods: {
    radioClick(item) {
      item.switchdis = !item.switchdis
    },
    // 新增收货地址
    addressClick() {
      this.$router.push('/pages/newAddPeople/main')
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
  }
}
</script>
<style scoped lang="scss">
.demo_page {
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  background: #f3f3f3;
}
.title_tab {
  width: 100%;
  display: flex;
  flex-direction: column;
  .top_title {
    width: 100%;
  }
  .mealbox {
    width: 100%;
    height: 336rpx;
    padding: 30rpx;
    padding-bottom: 0;
    background: rgba(255, 255, 255, 1);
    box-sizing: border-box;
    margin-top: 16rpx;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    .box {
      width: 100%;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      .top_img {
        width: 50rpx;
        height: 210rpx;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: flex-start;
        .img1 {
          width: 36rpx;
          height: 36rpx;
        }
      }
      .top_one {
        width: 640rpx;
        // height: 210rpx;
        display: flex;
        flex-direction: row;
        justify-content: flex-start;
        align-items: center;
        padding-bottom: 20rpx;
        box-sizing: border-box;
        border-bottom: 1rpx solid #f3f3f3;
        .top_images {
          width: 190rpx;
          height: 210rpx;
          display: flex;
          flex-direction: column;
          justify-content: flex-start;
          align-items: flex-start;
          .img1 {
            width: 180rpx;
            height: 146rpx;
          }
        }
        .top_content {
          width: 450rpx;
          height: 210rpx;
          display: flex;
          flex-direction: column;
          justify-content: space-between;
          align-items: flex-start;
          .p1 {
            font-size: 28rpx;
            font-family: Source Han Sans CN;
            font-weight: 400;
            color: rgba(33, 33, 33, 1);
            width: 100%;
            overflow: hidden;
            white-space: pre-wrap;
            display: -webkit-box;
            text-overflow: ellipsis;
            -webkit-line-clamp: 2;
            -webkit-box-orient: vertical;
          }
          .top_center {
            width: 100%;
            display: flex;
            flex-direction: row;
            justify-content: space-between;
            align-items: center;
            .p3 {
              width: 327rpx;
              height: 60rpx;
              font-size: 24rpx;
              font-family: Source Han Sans CN;
              font-weight: 400;
              color: rgba(136, 136, 136, 1);
              overflow: hidden;
              white-space: pre-wrap;
              display: -webkit-box;
              text-overflow: ellipsis;
              -webkit-line-clamp: 2;
              -webkit-box-orient: vertical;
            }
            .top_all {
              // width: 50rpx;
              display: flex;
              flex-direction: column;
              justify-content: center;
              align-items: center;
              .img2 {
                width: 44rpx;
                height: 44rpx;
              }
              .txt3 {
                font-size: 24rpx;
                font-family: Source Han Sans CN;
                font-weight: 400;
                color: rgba(136, 136, 136, 1);
              }
            }
          }
          .p2 {
            width: 100%;
            display: flex;
            flex-direction: row;
            align-items: center;
            .txt1 {
              font-size: 24rpx;
              font-family: Source Han Sans CN;
              font-weight: 400;
              color: rgba(33, 33, 33, 1);
              span {
                font-size: 24rpx;
                font-family: Source Han Sans CN;
                font-weight: 400;
                color: rgba(255, 174, 0, 1);
              }
            }
            .txt2 {
              margin-left: 40rpx;
            }
          }
        }
      }
    }
    .top_three {
      width: 100%;
      height: 66rpx;
      padding: 0 20rpx;
      box-sizing: border-box;
      display: flex;
      flex-direction: row;
      justify-content: center;
      align-items: center;
      // border-bottom: 1rpx solid #f3f3f3;
      .metal {
        width: 65rpx;
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
    }
  }
  .address {
    width: 100%;
    margin-top: 16rpx;
    display: flex;
    flex-direction: column;
    .top_one {
      width: 100%;
      height: 180rpx;
      background: rgba(255, 255, 255, 1);
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      padding: 38rpx 30rpx;
      box-sizing: border-box;
      border-bottom: 1rpx solid #f3f3f3;
      .top_content {
        width: 640rpx;
        height: 104rpx;
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        align-items: flex-start;
        .p1 {
          display: flex;
          flex-direction: row;
          // justify-content: center;
          align-items: center;
          .txt1 {
            font-size: 30rpx;
            font-family: Source Han Sans CN;
            font-weight: bold;
            color: rgba(51, 51, 51, 1);
          }
          .txt2 {
            font-size: 30rpx;
            font-family: Source Han Sans CN;
            font-weight: bold;
            color: rgba(51, 51, 51, 1);
            margin-left: 100rpx;
          }
          .txt3 {
            width: 66rpx;
            height: 30rpx;
            background: rgba(222, 239, 255, 1);
            border-radius: 4rpx;
            text-align: center;
            line-height: 30rpx;
            font-size: 24rpx;
            font-family: Source Han Sans CN;
            font-weight: 400;
            color: rgba(68, 164, 254, 1);
            margin-left: 20rpx;
          }
        }
        .p3 {
          font-size: 26rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(51, 51, 51, 1);
        }
      }
      .top_images2 {
        width: 50rpx;
        height: 104rpx;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        .img2 {
          width: 34rpx;
          height: 34rpx;
        }
      }
    }
    .clear {
      width: 130rpx;
      height: 180rpx;
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      color: #ffffff;
      background: #f63d3d;
    }
  }
  .position {
    position: fixed;
    bottom: 0;
    left: 0;
    width: 100%;
    background: #ffffff;
    padding: 10rpx 30rpx;
    box-sizing: border-box;
  }
}
.distxt1 {
  width: 80rpx;
  height: 30rpx;
  background: rgba(255, 238, 231, 1);
  border-radius: 4rpx;
  font-size: 24rpx;
  font-family: Source Han Sans CN;
  font-weight: 400;
  color: rgba(255, 99, 36, 1);
  margin-left: 20rpx;
  text-align: center;
}
</style>