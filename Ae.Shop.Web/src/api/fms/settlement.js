import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_SHOP_API;
let fmsDomain = process.env.VUE_APP_BASE_FMS_API;

let appSvc = {

  //更新对账结果
  GetWithdrawalRecordList(params) {
    return request2({
      url: domain + '/Fiance/GetWithdrawalRecordList',
      method: 'get',
      params
    })
  },
  GetAccountCheckLogs(params) {
    return request2({
      url: domain + '/Fiance/GetAccountCheckLogs',
      method: 'get',
      params
    })
  },
  GetWithdrawalOrderRecordList(params) {
    return request2({
      url: domain + '/Fiance/GetWithdrawalOrderRecordList',
      method: 'get',
      params
    })
  },
  ///提现申请信息
  GetWithdrawalApply(params) {
    return request2({
      url: domain + '/Fiance/GetWithdrawalApply',
      method: 'get',
      params
    })
  },
  //提现
  SubmitWithdrawalApply(data) {
    return request2({
      url: domain + '/Fiance/SubmitWithdrawalApply',
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
  },
  getPurchaseSettlementList(params) {
    return request2({
      url: fmsDomain + '/Settlement/GetPurchaseSettlementList',
      method: 'get',
      params
    })
  },
  getPurchaseSettlementDetail(params) {
    return request2({
      url: fmsDomain + '/Settlement/GetPurchaseSettlementDetail',
      method: 'get',
      params
    })
  },
  savePurchaseSettlementReview(data) {
    return request2({
      url: fmsDomain + '/Settlement/SavePurchaseSettlementReview',
      method: 'post',
      data
    })
  },


};

export {
  appSvc
};
