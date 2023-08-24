import Vue from 'vue'
import App from './App'
import MpvueRouterPatch from 'mpvue-router-patch'
import store from './store'
import './css/common.css'
// @import 'https://at.alicdn.com/t/font_8d5l8fzk5b87iudi.css'
Vue.config.productionTip = false
App.mpType = 'app'
Vue.use(MpvueRouterPatch)
Vue.prototype.$store = store
const app = new Vue(App)
app.$mount()
let globalData = {}
globalData.imgPubUrl = 'https://m.aerp.com.cn/mini-app-main/'
globalData.currCityInfo = {}
globalData.tokenInfo = ''

globalData.addressSure = false
globalData.isCarShow = false
globalData.reservedTime = null
globalData.serviceType = ''
// 产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生 5 上门服务 6 会员卡 7 代客下单 8:代理门店押金单 9：代理门店押金单 10:推广门店下单 11：技师先生（ 工作室下单） 
globalData.ProductType = ''
globalData.code = ''
globalData.homeAddress = null
globalData.orderListStatus = -1
// 订单类型（0未设置 1轮胎 2保养 3美容 4 钣金维修 5 汽车改装 6 其他）
globalData.OrderType = -1
globalData.shareUserId = ''
Vue.prototype.globalData = globalData
