<template>
  <div class="page">
    <div class="main">
      <div class="border"  v-for="(item,index) in abnormalDes" :key="index">
        <div class="condition">
          <img src="https://m.aerp.com.cn/mini-app-main/juxing.png" alt class="borderimg" />
          <p class="title">
            <span class="date">{{item.checkDate}}</span>
            <span class="shopname">{{item.shopSimpleName}}</span>
          </p>
          <div class="content" v-for="(itemm,indexx) in item.resultItems" :key="indexx">
            <p
              class="contentT"
              v-for="(item1,index1) in itemm.errorDesList"
              :key="index1"
            >异常描述:{{item1.errorDes}}</p>
            <div class="allimg">
              <img
                :src="item2"
                alt
                class="images"
                v-for="(item2,index2) in itemm.images"
                :key="index2"
                @click="previewImage(itemm.images,index2)"
                :data-i="itemm.images"
                :data-src="item2"
              />
            </div>
          </div>
          <!-- <div class="content">
            <p class="contentT">异常描述:漆面划痕</p>
            <div class="contentB">
              <img src="https://m.aerp.com.cn/mini-app-main/more.png" alt class="images" />
            </div>
          </div>-->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { GetCheckErrorDetail } from '@/api/customermanage/user'
import { ImagePreview } from 'vant'

export default {
  data() {
    return {
      CarId: '',
      KeyName: '',
      abnormalDes: [],
      displayName: ''
    }
  },
  mounted() {
    this.CarId = this.$route.query.CarId
    this.KeyName = this.$route.query.keyName
    this.displayName = this.$route.query.displayName
    this.GetCheckErrorDetail()
  },
  methods: {
    previewImage(imgArr, imgIndex) {
      console.log(889911, imgArr, imgIndex)
      ImagePreview({
        images: imgArr,
        startPosition: imgIndex
      })
    },
    GetCheckErrorDetail() {
      let that = this
      let rquest = {
        CarId: that.CarId,
        KeyName: that.KeyName
      }
      console.log(rquest)
      GetCheckErrorDetail(rquest)
        .then(res => {
          let arr = res.data
          that.abnormalDes = arr
          console.log(arr)
        })
        .catch(err => {})
    }
  }

  // onReady() {
  //   wx.setNavigationBarTitle({
  //     title: this.displayName
  //   })
  // }
}
</script>
<style>
page {
  background: rgba(243, 243, 243, 1);
}
</style>
<style scoped lang="scss">
.page {
  width: 750px;
  height: 100%;
  overflow: auto;
  margin: 0 auto;
  // background: rgba(243, 243, 243, 1);
}
.main {
  background: #fff;
  min-height: 200px;
  padding: 30px;
  width: 100%;
  box-sizing: border-box;
}
.condition {
  width: 100%;
  min-height: 90px;
  box-sizing: border-box;
}
.borderimg {
  height: 32px;
  width: 32px;
  position: absolute;
  left: -16px;
  top: -16px;
}
.title {
  position: relative;
  display: flex;
  align-items: center;
  width: 100%;
  margin-bottom: 20px;
}
.date {
  color: #999;
  font: 22px/1 'SourceHanSansCN-Medium';
}
.shopname {
  color: #333;
  font: 700 28px/1 'SourceHanSansCN-Medium';
  position: absolute;
  left: 210px;
}
.content {
  min-height: 100px;
  background: #f2f2f2;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  padding: 30px;
  width: 100%;
  border-radius: 10px;
  margin-bottom: 20px;
}
.contentT {
  color: #666;
  font: 26px/1 'SourceHanSansCN-Medium';
}
.contentB {
  display: flex;
  justify-content: flex-start;
  align-items: center;
  flex-wrap: wrap;
}
.allimg {
  padding: 10px 0;
}
.images {
  height: 180px;
  width: 180px;
  margin: 10px 20px 0 0;
}
.border {
  border-left: 1px solid #ccc;
  width: 100%;
  padding: 20px 0 20px 20px;
  box-sizing: border-box;
  position: relative;
}
</style>
