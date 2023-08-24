<template>
  <div class="demo_page">
    <div class="top_title">
      <p class="p1">小保养服务</p>
      <div class="shop_one">
        <div class="img">
          <img src="https://m.aerp.com.cn/mini-RG-main/maintenance_good_pic.png" alt class="img1" />
        </div>
        <div class="content">
          <p class="p_p1">美孚/Mobil-1号全合成机油5W-30SNdv级4L</p>
          <div class="bottom">
            <p class="p2">共1件</p>
            <p class="p4">
              <text class="txt1">￥489.00</text>
              <text class="txt2">x1</text>
            </p>
          </div>
        </div>
      </div>
      <div class="shop_one">
        <div class="img">
          <img src="https://m.aerp.com.cn/mini-RG-main/maintenance_good_pic.png" alt class="img1" />
        </div>
        <div class="content">
          <p class="p_p1">美孚/Mobil-1号全合成机油5W-30SNdv级4L</p>
          <div class="bottom">
            <p class="p2">共1件</p>
            <p class="p4">
              <text class="txt1">￥489.00</text>
              <text class="txt2">x1</text>
            </p>
          </div>
        </div>
      </div>
      <div class="shuoming">
        <p class="write1">安装服务说明</p>
        <p class="write2">更换机油及机油滤清器</p>
        <p class="write3">注：相关操作:保养完成需对保养提示灯进行归零复位，特殊车型保养灯复位请与门店协商</p>
      </div>
    </div>
    <div class="free">
      <p class="free_p1">免费常规检测</p>
      <div class="free_p2">
        <p class="free_p3">
          <img src="https://m.aerp.com.cn/mini-RG-main/ordercheck_free_icon.png" alt class="img1" />
          <text class="txt1">免费常规检测服务</text>
        </p>
        <p class="free_p4">
          <text class="txt1">￥0.00</text>
          <text class="txt2">x1</text>
        </p>
      </div>
      <p class="write1">安装服务说明</p>
      <p class="write2">更换机油及机油滤清器</p>
      <p class="write3">注：相关操作:保养完成需对保养提示灯进行归零复位，特殊车型保养灯复位请与门店协商</p>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {}
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
  padding: 30rpx;
  padding-top: 0;
  box-sizing: border-box;
}
.top_title {
  width: 100%;
  display: flex;
  flex-direction: column;
  margin-top: 16rpx;
  padding: 30rpx;
  padding-top: 0;
  box-sizing: border-box;
  background: #ffffff;
  .p1 {
    width: 100%;
    height: 90rpx;
    background: rgba(255, 255, 255, 1);
    border-radius: 10rpx 10rpx 0rpx 0rpx;
    line-height: 90rpx;
  }
  .shop_one {
    width: 100%;
    height: 170rpx;
    background: rgba(255, 255, 255, 1);
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    border-bottom: 1rpx solid #f3f3f3;
    .img {
      width: 150rpx;
      height: 138rpx;
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      .img1 {
        width: 102rpx;
        height: 102rpx;
      }
    }
    .content {
      width: 500rpx;
      height: 138rpx;
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      align-items: flex-start;
      .p_p1 {
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
      .bottom {
        width: 100%;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: center;
        .p2 {
          font-size: 24rpx;
          font-family: Source Han Sans CN;
          font-weight: 400;
          color: rgba(136, 136, 136, 1);
        }
        .p4 {
          display: flex;
          flex-direction: row;
          justify-content: flex-end;
          align-items: flex-end;
          .txt1 {
            font-size: 28rpx;
            font-family: Source Han Sans CN;
            font-weight: 400;
            color: rgba(255, 99, 36, 1);
          }
          .txt2 {
            font-size: 24rpx;
            font-family: Source Han Sans CN;
            font-weight: 400;
            color: rgba(51, 51, 51, 1);
          }
        }
      }
    }
  }
  .shuoming {
    width: 100%;
    background: #ffffff;
    .write1 {
      font-size: 28rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(136, 136, 136, 1);
      margin-top: 15rpx;
    }
    .write2 {
      font-size: 26rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(102, 102, 102, 1);
      margin: 25rpx 0 10rpx 0;
    }
    .write3 {
      width: 100%;
      font-size: 26rpx;
      font-family: Source Han Sans CN;
      font-weight: 400;
      color: rgba(102, 102, 102, 1);
      overflow: hidden;
      white-space: pre-wrap;
      display: -webkit-box;
      text-overflow: ellipsis;
      -webkit-line-clamp: 2;
      -webkit-box-orient: vertical;
    }
  }
}
.free {
  width: 100%;
  // height:530rpx;
  background: rgba(255, 255, 255, 1);
  border-radius: 10rpx;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  padding: 30rpx;
  box-sizing: border-box;
  margin: 16rpx 0;
  .free_p1 {
    font-size: 32rpx;
    font-family: Source Han Sans CN;
    font-weight: bold;
    color: rgba(51, 51, 51, 1);
    margin-bottom: 30rpx;
  }
  .free_p2 {
    width: 100%;
    height: 120rpx;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    border-bottom: 1rpx solid #f3f3f3;
    .free_p3 {
      display: flex;
      flex-direction: row;
      .img1 {
        width: 55rpx;
        height: 55rpx;
      }
      .txt1 {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
        margin-left: 20rpx;
      }
    }
    .free_p4 {
      .txt1 {
        font-size: 28rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        text-decoration: line-through;
        color: rgba(136, 136, 136, 1);
      }
      .txt2 {
        font-size: 24rpx;
        font-family: Source Han Sans CN;
        font-weight: 400;
        color: rgba(51, 51, 51, 1);
      }
    }
  }
  .write1 {
    font-size: 28rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(136, 136, 136, 1);
    margin-top: 15rpx;
  }
  .write2 {
    font-size: 26rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(102, 102, 102, 1);
    margin: 25rpx 0 10rpx 0;
  }
  .write3 {
    width: 100%;
    font-size: 26rpx;
    font-family: Source Han Sans CN;
    font-weight: 400;
    color: rgba(102, 102, 102, 1);
    overflow: hidden;
    white-space: pre-wrap;
    display: -webkit-box;
    text-overflow: ellipsis;
    -webkit-line-clamp: 2;
    -webkit-box-orient: vertical;
  }
}
</style>