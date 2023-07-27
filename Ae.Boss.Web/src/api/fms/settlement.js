import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_FMS_API;

let appSvc = {
 
  //更新对账结果
  getSettlementList(data) {
    return request2({
      url: domain + '/Settlement/GetSettlementList',
      method: 'post',
      data
    })
  },
  //对账标记异常
  saveSettlementReview(data) {
    return request2({
      url: domain + '/Settlement/SaveSettlementReview',
      method: 'post',
      data
    })
  },
  getSettlementDetail(params) {
    return request2({
      url: domain + '/Settlement/GetSettlementDetail',
      method: 'get',
      params
    })
  },
  getRegionChinaListByRegionId(params) {
    return request2({
      url: domain + '/AccountCheck/GetRegionChinaListByRegionId',
      method: 'get',
      params
    })
  },
  getShopListAsync(params) {
    return request2({
      url: domain + '/ShopManage/GetShopListAsync',
      method: 'get',
      params
    })
  },
  getAccountCheckLogs(params) {
    return request2({
      url: domain + '/AccountCheck/GetAccountCheckLogs',
      method: 'get',
      params
    })
  },
  settlementPayReviewDO(params) {
    return request2({
      url: domain + '/Settlement/SettlementPayReviewDO',
      method: 'get',
      params
    })
  }
  
};

export {
  appSvc
};