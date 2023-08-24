<template>
  <div class="page">
    <div class="content">
      <div class="tab">
        <div class="car">
          <img :src="mainData.brandUrl" alt class="img" />
          <div class="carTitle">
            <p class="carName">{{mainData.carPlate}}</p>

            <p class="carNumber">{{mainData.carType}}</p>

            <p class="carUser">{{mainData.userName}}：{{mainData.userTelDisplay}}</p>
          </div>
        </div>
      </div>
      <div class="checkDetails">
        <p class="checkTitle">
          时间：
          <span class="checkResult">{{mainData.inTime}}</span>
        </p>
        <p class="checkTitle">
          VIN：
          <span class="checkResult">{{mainData.vinCode}}</span>
        </p>
        <p class="checkTitle">
          公里数：
          <span class="checkResult">{{mainData.mileage}}公里</span>
        </p>
        <p class="checkTitle">
          客户描述：
          <span class="checkResult">{{mainData.narration}}</span>
        </p>
        <p class="checkTitle">仪表盘图片</p>
        <div class="checkimgs">
          <img
            :src="mainData.dashboardImg"
            alt
            class="checkimg"
            @click="previewImage([mainData.dashboardImg],0)"
            :data-src="mainData.dashboardImg"
          />
        </div>

        <div class="checkTitle" v-for="(item,index) in otherProjectResult" :key="index">
          {{item.projectName}}:
          <span class="checkResult">{{item.resultValue}}</span>
          <div class="checkimgs" v-if="item.image.length>0">
            <img :src="item5" alt class="checkimg" v-for="(item5,index5) in item.image" :key='index5' @click="previewImage(item.image,index5)"
            :data-src="item5" :data-i="item.image"/>
          </div>
        </div>
      </div>
      <div>
        <!-- <div class="projects" :class=" isFixedTop ? 'projectsFixed' : 'projects'" id="projects"  >
          <div
            class="project"
            v-for="(item,index) in allConditions"
            :data-index="index"
            :key="index"
            @tap="actived"
            :class="index === currentIndexNav ? 'active' : 'project'"
            :id="index"
          >
            {{item.displayName}}
            <span class="number">({{item.count}})</span>
          </div>
        </div>-->

        <van-tabs :active="active" color="#FF6214" class="tabstyle">
          <!-- <van-tab v-for="index in 4" :title="'选项 ' + index">
    内容 {{ index }}
          </van-tab>-->
          <!-- <van-tabs :active="active" sticky swipeable color="#FF6324" line-width="40" class="tabs" @change="onChangeTitle"> -->
          <van-tab :title="displayName1">
            <div class="abnormal" v-for="(item,index) in abnormal" :key="index">
              <div class="abnormaltit" @click="tomore">
                <p class="titles">
                  {{ item.displayName}}
                  <span class="number">({{item.count}})</span>
                </p>
                <img
                  src="https://m.aerp.com.cn/mini-app-main/more.png"
                  alt
                  class="moreimg"
                  v-if="showmore"
                />
                <img
                  src="https://m.aerp.com.cn/mini-app-main/toright.png"
                  alt
                  class="moreimg"
                  v-else
                />
              </div>

              <div v-for="(itemm,indexx) in item.checkClassfy" :key="indexx" style="width:100%">
                <div
                  class="abnormalC"
                  v-show="showmore"
                  v-for="(item1,index1) in itemm.resultItems"
                  :key="index1"
                >
                  <div>
                    <p class="abnormalName">{{item1.displayName}}</p>
                    <p class="abnormalDetail" v-if="item1.errorDesList.length>0">
                      异常情况：
                      <span
                        v-for="(item2,index2) in item1.errorDesList"
                        :key="index2"
                      >{{item2.errorDes}}</span>
                    </p>
                    <div class="checkimgs checkimgs1" v-if="item1.images.length>0">
                      <img
                        :src="item3"
                        alt
                        class="checkimg"
                        v-for="(item3,index3) in item1.images"
                        :key="index3"
                        :class="item1.displayName=='仪表盘-故障灯' ? 'checkimg1' : 'checkimg'"
                        @click="previewImage(item1.images,index3)"
                        :data-i="item1.images"
                        :data-src="item3"
                      />
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </van-tab>
          <van-tab :title="displayName2">
            <div class="abnormal" id="abnormal1" v-for="(item,index) in noChecked" :key="index">
              <div class="abnormaltit" @click="tomore1">
                <p class="titles">
                  {{ item.displayName}}
                  <span class="number">({{item.count}})</span>
                </p>
                <img
                  src="https://m.aerp.com.cn/mini-app-main/more.png"
                  alt
                  class="moreimg"
                  v-if="showmore1"
                />
                <img
                  src="https://m.aerp.com.cn/mini-app-main/toright.png"
                  alt
                  class="moreimg"
                  v-else
                />
              </div>
              <div v-for="(itemm,indexx) in item.checkClassfy" :key="indexx" style="width:100%">
                <div class="abnormalC" v-show="showmore1">
                  <p class="nocheckedName">{{itemm.classfyName}}</p>
                  <div
                    class="nocheckedDetail"
                    v-for="(item1,index1) in itemm.resultItems"
                    :key="index1"
                  >
                    <div class="checkimgs" v-if="item1.images.length>0">
                      <img
                        :src="iteem"
                        alt
                        class="checkimg1"
                        v-for="(iteem,indeex) in item1.images"
                        :key="indeex"
                      />
                    </div>
                    <div v-else style="width:100%" class="checkdes">
                      <p class="tit">{{item1.displayName}}</p>
                      <div class="allDes">
                        <p
                          class="tit1"
                          v-for="(item2,index2) in item1.normalDes"
                          :key="index2"
                        >{{item2}}</p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </van-tab>
          <van-tab :title="displayName3">
            <div class="abnormal" id="abnormal2" v-for="(item,index) in normal" :key="index">
              <div class="abnormaltit" @click="tomore2">
                <p class="titles">
                  {{ item.displayName}}
                  <span class="number">({{item.count}})</span>
                </p>
                <img
                  src="https://m.aerp.com.cn/mini-app-main/more.png"
                  alt
                  class="moreimg"
                  v-if="showmore2"
                />
                <img
                  src="https://m.aerp.com.cn/mini-app-main/toright.png"
                  alt
                  class="moreimg"
                  v-else
                />
              </div>
              <div v-for="(itemm,indexx) in item.checkClassfy" :key="indexx" style="width:100%">
                <div class="abnormalC" v-show="showmore2">
                  <p class="nocheckedName">{{itemm.classfyName}}</p>
                  <div
                    class="nocheckedDetail"
                    v-for="(item1,index1) in itemm.resultItems"
                    :key="index1"
                  >
                    <div class="checkimgs" v-if="item1.images.length>0">
                      <img
                        :src="iteem"
                        alt
                        class="checkimg1"
                        v-for="(iteem,indeex) in item1.images"
                        :key="indeex"
                      />
                    </div>
                    <div v-else style="width:100%" class="checkdes">
                      <p class="tit">{{item1.displayName}}</p>
                      <div class="allDes">
                        <p
                          class="tit1"
                          v-for="(item2,index2) in item1.normalDes"
                          :key="index2"
                        >{{item2}}</p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </van-tab>
        </van-tabs>

        <div class="tab1">
          <p class="qianzi">技师签字</p>
          <img
            :src="mainData.technicianSignature"
            alt
            class="checkimg2"
            @click="previewImage([mainData.technicianSignature],0)"
            :data-src="mainData.technicianSignatur"
          />
        </div>
        <div class="tab1">
          <p class="qianzi">客户签字</p>
          <img
            :src="mainData.customerSignature"
            alt
            class="checkimg2"
            @click="previewImage([mainData.customerSignature],0)"
            :data-src="mainData.customerSignature"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { GetCheckReport } from "@/api/customermanage/user";
import { ImagePreview } from "vant";
export default {
  data() {
    return {
      active: 0,
      heightArray1: [],
      windowHeight: "",
      windowHeight1: "",
      normal: [], // 正常项
      noChecked: [],
      abnormal: [],
      allConditions: [],
      otherProjectResult: {},
      isSatisfy: "",
      isTop: false,
      mainData: {},
      checkReport: [],
      showmore: true,
      showmore1: true,
      showmore2: true,
      activeNames: 1,
      flag: false,
      top: 0,
      activeIndex: "",
      index: "",
      navbarInitTop: 0,
      isFixedTop: false,
      currentIndexNav: 0,
      abnormalTop0: "",
      abnormalTop1: 0,
      abnormalTop2: "",
      scrollTop: "",
      CheckId: "",
      displayName1: "",
      displayName2: "",
      displayName3: ""
    };
  },
  // onShow() {
  //   this.getCheckReport()
  // },
  mounted() {
    console.log(8899, this.$route.params.car);
    this.CheckId = this.$route.params.car.checkId;

    this.getCheckReport();
  },
  methods: {
    previewImage(imgArr, imgIndex) {
      console.log(889911, imgArr, imgIndex);
      ImagePreview({
        images: imgArr,
        startPosition: imgIndex
      });
    },
    // 查看图片
    // previewImage(e) {
    //   var current = e.target.dataset.src
    //   console.log(233, e.target.dataset.src)
    //   wx.previewImage({
    //     current: current, // 当前显示图片的http链接
    //     urls: [current] // 需要预览的图片http链接列表
    //   })
    // },
    previewImage2(e) {
      var current = e.target.dataset.src;
      console.log(233, e.target.dataset.src);
      wx.previewImage({
        current: current, // 当前显示图片的http链接
        urls: [current] // 需要预览的图片http链接列表
      });
    },
    previewImage3(e) {
      var current = e.target.dataset.src;
      console.log(233, e.target.dataset.src);
      wx.previewImage({
        current: current, // 当前显示图片的http链接
        urls: [current] // 需要预览的图片http链接列表
      });
    },
    previewImage1(e) {
      var current = e.target.dataset.src;
      console.log(1, e.target.dataset.i);
      console.log(2, e.target.dataset.src);
      wx.previewImage({
        current: current, // 当前显示图片的http链接
        urls: e.target.dataset.i // 需要预览的图片http链接列表
      });
    },
    // 获取检查报告
    getCheckReport() {
      let that = this;
      console.log(8855, that.CheckId);
      let rquest = {
        CheckId: that.CheckId
      };
      GetCheckReport(rquest)
        .then(res => {
          that.checkReport = res.data;
          that.mainData = res.data.mainData;
          that.otherProjectResult = res.data.otherProjectResult;
          that.allConditions = res.data.items;
          console.log(22, that.allConditions[0].displayName);
          that.displayName1 =
            that.allConditions[0].displayName +
            "(" +
            that.allConditions[0].count +
            ")";
          that.displayName2 =
            that.allConditions[1].displayName +
            "(" +
            that.allConditions[1].count +
            ")";
          that.displayName3 =
            that.allConditions[2].displayName +
            "(" +
            that.allConditions[2].count +
            ")";
          that.abnormal = that.allConditions.filter(function(obj) {
            return obj.displayName == "异常项";
          });
          that.noChecked = that.allConditions.filter(function(obj) {
            return obj.displayName == "未检查项";
          });
          that.normal = that.allConditions.filter(function(obj) {
            return obj.displayName == "正常项";
          });
          that.otherProjectResult.map(function(val) {
            if (val.resultValue == 0) {
              val.resultValue = "未检查";
            } else if (val.resultValue == 1) {
              val.resultValue = "有";
            } else if (val.resultValue == 2) {
              val.resultValue = "无";
            } else {
            }
          });

          // console.log('mainData', that.mainData)
          console.log("检查报告", that.checkReport);
        })
        .catch(err => {});
    },
    scroll() {
      return {
        scrollTop: this.navbarInitTop,
        isFixed: true
      };
    },
    tomore() {
      this.showmore = !this.showmore;
    },
    tomore1() {
      this.showmore1 = !this.showmore1;
    },
    tomore2() {
      this.showmore2 = !this.showmore2;
    },
    toabnormal1() {
      this.$forceUpdate();
      if (this.abnormalTop1 != 0) {
        wx.pageScrollTo({
          scrollTop: this.abnormalTop1
        });
      }
      console.log(2, this.abnormalTop1);
    },
    actived(e) {
      this.currentIndexNav = e.target.dataset.index;
      this.activeIndex = "abnormal" + this.currentIndexNav;
      console.log("activeIndex", this.activeIndex);
      let activetab = "#" + "abnormal" + this.currentIndexNav;
      console.log("activetab", activetab);
      console.log("isSatisfy", this.isSatisfy);
      let abnormalT = "abnormalTop" + this.currentIndexNav;
      console.log("abnormalT", abnormalT);
      console.log(3, this.abnormalTop1);

      // if (this.currentIndexNav == 1) {
      //   this.toabnormal1()
      // }
    }
  }
};
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
  background: rgba(243, 243, 243, 1);
}
.content {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  padding: 16px 30px 0 30px;
  box-sizing: border-box;
  height: 100%;
}
.tab {
  width: 100%;
  border-radius: 10px 10px 10px 10px;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  padding: 30px;
  padding-top: 0;
  box-sizing: border-box;
  background: #ffffff;
  margin-bottom: 16px;
  position: relative;
}
.tab1 {
  width: 672px;
  border-radius: 10px 10px 10px 10px;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  padding: 30px;
  padding-top: 0;
  box-sizing: border-box;
  background: #ffffff;
  margin-bottom: 16px;
}
.car {
  width: 100%;
  min-height: 160px;
  background: #fff;
  border-radius: 10px;
  box-sizing: border-box;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;

  .img {
    width: 86px;
    height: 86px;
  }
  .carTitle {
    width: 530px;
    padding-top: 20px;
    box-sizing: border-box;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: flex-start;
  }
}
.carName {
  color: #333;
  font: 32px/1 "SourceHanSansCN-Medium";
  padding-top: 20px;
}
.carNumber {
  color: #666666;
  font: 28px/1 "SourceHanSansCN-Medium";
  padding-top: 20px;
}
.carUser {
  color: #333;
  font: 28px/1 "SourceHanSansCN-Medium";
  padding-top: 20px;
}
.condition {
  width: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
}
.conditionTop {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  padding-top: 20px;
}
.title {
  color: #333;
  font: 28px/1 "SourceHanSansCN-Medium";
}
.toCheck {
  color: #ff6234;
  font: 24px/1 "PingFang-SC-Medium-Medium";
  display: flex;

  align-items: center;
}
.tocheckimg {
  width: 28px;
  height: 28px;
}
.conditionB {
  width: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  padding-top: 20px;
}
.resultimg {
  width: 28px;
  height: 28px;
  margin-right: 8px;
}
.result {
  display: flex;
  color: #666;
  font: 28px/1 "PingFang-SC-Medium";
  align-items: center;
}
.else {
  color: #666;
  font: 26px/1 "SourceHanSansCN-Regular";
  margin-top: 20px;
}
.checkDetails {
  width: 100%;
  border-radius: 10px 10px 10px 10px;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  padding: 30px;
  padding-top: 0;
  box-sizing: border-box;
  background: #ffffff;
  margin-bottom: 16px;
  position: relative;
}
.checkTitle {
  color: #333;
  font: 28px/1 "SourceHanSansCN-Regular";
  margin-top: 20px;
}
.checkResult {
  color: #666;
  font: 28px/1 "SourceHanSansCN-Regular";
}
.checkimgs {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  flex-wrap: wrap;
}
.checkimgs1 {
  padding: 20px 0;
}
.checkimg {
  width: 150px;
  height: 160px;
  margin: 20px 20px 0 0;
}
.projects {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  background: rgba(243, 243, 243, 1);
  border-bottom: 1px solid #e2e2e2;
  width: 100%;
  padding: 0 30px;
  box-sizing: border-box;
}
.project {
  color: #333;
  font: 32px/1 "SourceHanSansCN-Medium";
  text-align: center;
  box-sizing: border-box;
  padding: 20px;
}
.active {
  // width: 33%;
  color: #ff6214;
  font: 32px/1 "SourceHanSansCN-Medium";
  border-bottom: 4px solid #ff6214;
  box-sizing: border-box;
  padding: 20px;
  text-align: center;
  .number {
    color: #ff6214;
  }
}
.projectsFixed {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  background: rgba(243, 243, 243, 1);
  border-bottom: 1px solid #e2e2e2;
  width: 100%;
  padding: 0 40px 0 20px;
  position: fixed;
  top: 0;
  z-index: 10;
  box-sizing: border-box;
}
.scroll-view-vertical {
  width: 100%;
  // height:100%;
}
.scroll-view-vertical1 {
  width: 100%;
  // height: calc(100vh);
  height: 100%;
}
.abnormal {
  width: 100%;
  border-radius: 10px 10px 10px 10px;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  padding: 30px;
  padding-top: 0;
  box-sizing: border-box;
  background: #ffffff;
  margin-bottom: 16px;
  position: relative;
  min-height: 100px;
  margin-top: 20px;
}
.abnormaltit {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  padding: 30px 0;
  border-bottom: 1px solid #e2e2e2;
  width: 100%;
  box-sizing: border-box;
}
.moreimg {
  width: 40px;
  height: 40px;
}
.titles {
  color: #333;
  font: 30px/1 "SourceHanSansCN-Medium";
}
.number {
  color: #999;
  font: 24px/1 "SourceHanSansCN-Medium";
}
.abnormalName {
  color: #333;
  font: 28px/1 "SourceHanSansCN-Medium";
  margin-top: 20px;
}
.abnormalDetail {
  color: #666;
  font: 24px/1 "SourceHanSansCN-Medium";
  margin: 20px 0;
}
.checkimg1 {
  height: 60px;
  width: 60px;
  margin-right: 10px;
}
.abnormalC {
  width: 100%;
}
.nocheckedName {
  color: #333;
  font: 28px/1 "SourceHanSansCN-Medium";
  border-left: 4px solid #ff6214;
  padding-left: 12px;
  margin: 20px 0 10px;
}
.nocheckedDetail {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  border-bottom: 1px solid #e2e2e2;
  width: 100%;
  padding: 30px 0;
}
.checkdes {
  display: flex;
  flex-direction: row;
  justify-content: space-between;

  width: 100%;
}
.tit {
  color: #333;
  font: 26px/1 "SourceHanSansCN-Medium";
}
.tit1 {
  color: #999;
  font: 26px/1 "SourceHanSansCN-Medium";
}
.checkimg2 {
  height: 220px;
  width: 100%;
}
.qianzi {
  color: #333;
  font: 600 32px/1 "SourceHanSansCN-Medium";
  padding: 30px 0;
}
.allDes {
  max-width: 60%;
}
.tabstyle {
  // height: 100vh;
  width: 100%;
  // position: sticky;
}
</style>
