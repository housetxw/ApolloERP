import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_AFTERSALE_API;
let wmsdomain = process.env.VUE_APP_BASE_WMS_API;
let fileUploaddomain = process.env.VUE_APP_BASE_FileUpload_API;
let appSvc = {
    getBasicInfoList(params) {
    return request2({
      url: domain + '/Tousu/GetBasicInfoList',
      method: 'get',
      params
    })
  },
  getShopList(params) {
    return request2({
      url: wmsdomain + '/ShopManager/GetShopList',
      method: 'get',
      params
    })
  },
  getTousuList(params) {
    return request2({
      url: domain + '/Tousu/GetTousuList',
      method: 'get',
      params
    })
  },
  createTousu(data) {
    return request2({
      url: domain + '/Tousu/CreateTousu',
      method: 'post',
      data
    })
  },
  getTousuLogs(params) {
    return request2({
      url: domain + '/Tousu/GetTousuLogs',
      method: 'get',
      params
    })
  },
  getTousuFiles(params) {
    return request2({
      url: domain + '/Tousu/GetTousuFiles',
      method: 'get',
      params
    })
  },
  getQiNiuToken(params) {
    return request2({
      url: fileUploaddomain + '/QiNiu/GetQiNiuToken',
      method: 'get',
      params
    })
  },
  getTousuDetail(params) {
    return request2({
      url: domain + '/Tousu/GetTousuDetail',
      method: 'get',
      params
    })
  },
  getOrderInfo(params) {
    return request2({
      url: domain + '/Tousu/GetOrderInfo',
      method: 'get',
      params
    })
  },
  updateTousuStatus(data) {
    return request2({
      url: domain + '/Tousu/UpdateTousuStatus',
      method: 'post',
      data
    })
  },
  getDictionaryDOs(params) {
    return request2({
      url: domain + '/Tousu/GetDictionaryDOs',
      method: 'get',
      params
    })
  },
  updateTousuDealResult(data) {
    return request2({
      url: domain + '/Tousu/UpdateTousuDealResult',
      method: 'post',
      data
    })
  },
  updateTousuDutyInfo(data) {
    return request2({
      url: domain + '/Tousu/UpdateTousuDutyInfo',
      method: 'post',
      data
    })
  },
  createTousuFile(data) {
    return request2({
      url: domain + '/Tousu/CreateTousuFile',
      method: 'post',
      data
    })
  }
};

export {
  appSvc
};