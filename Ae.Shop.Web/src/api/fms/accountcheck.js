import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_SHOP_API;
let fmsDomain=process.env.VUE_APP_BASE_FMS_API;

let appSvc = {
  // 门店未对账列表
  getShopAccountUnChecks(data) {
    return request2({
      url: domain + '/Fiance/GetShopAccountUnChecks',
      method: 'post',
      data
    })
  },
  //门店对账列表
  GetShopAccountChecks(data) {
    return request2({
      url: domain + '/Fiance/GetShopAccountChecks',
      method: 'post',
      data
    })
  },
  //总部对账列表
  GetRgAccountChecks(data) {
    return request2({
      url: domain + '/Fiance/GetRgAccountChecks',
      method: 'post',
      data
    })
  },
  ///日志
  GetAccountCheckLogs(params) {
    return request2({
      url: domain + '/Fiance/GetAccountCheckLogs',
      method: 'get',
      params
    })
  },
  //创建对账
  CreateAccountCheck(data) {
    return request2({
      url: domain + '/Fiance/CreateAccountCheck',
      method: 'post',
      data
    })
  },
  //对账异常列表
  GetAccountCheckExceptionCollectList(data) {
    return request2({
      url: domain + '/Fiance/GetAccountCheckExceptionCollectList',
      method: 'post',
      data
    })
  },
  //对账日志
  getAccountCheckLogs(params) {
    return request2({
      url: domain + '/AccountCheck/GetAccountCheckLogs',
      method: 'get',
      params
    })
  },
  //更新对账结果
  updateRgAccountCheckResult(data) {
    return request2({
      url: domain + '/AccountCheck/UpdateRgAccountCheckResult',
      method: 'post',
      data
    })
  },
  //对账标记异常
  createAccountCheckException(data) {
    return request2({
      url: domain + '/AccountCheck/CreateAccountCheckException',
      method: 'post',
      data
    })
  },
  getBasicInfoList(params) {
    return request2({
      url: domain + '/AccountCheck/GetBasicInfoList',
      method: 'get',
      params
    })
  },
  getAccountCheckExceptionCollectList(data) {
    return request2({
      url: domain + '/AccountCheck/GetAccountCheckExceptionCollectList',
      method: 'post',
      data
    })
  },
  rgAccountCheckWithdraw(data) {
    return request2({
      url: domain + '/AccountCheck/RgAccountCheckWithdraw',
      method: 'post',
      data
    })
  },
  getAccountCheckCollects(params) {
    return request2({
      url: domain + '/AccountCheck/GetAccountCheckCollects',
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
  accountCheckExceptionHandle(data) {
    debugger
    return request2({
      url: domain + '/AccountCheck/AccountCheckExceptionHandle',
      method: 'post',
      data
    })
  },
  getShopListAsync(params) {
    return request2({
      url: domain + '/ShopManage/GetShopListAsync',
      method: 'get',
      params
    })
  },
  getAccountCheckExceptionMonitorList(data) {
    debugger
    return request2({
      url: domain + '/AccountCheck/GetAccountCheckExceptionMonitorList',
      method: 'post',
      data
    })
  },
   //对账日志
   getPurchaseAccountList(params) {
    return request2({
      url: fmsDomain + '/AccountCheck/GetPurchaseAccountList',
      method: 'get',
      params
    })
  },
  savePurchaseAccountRecord(data) {
    return request2({
      url: fmsDomain + '/AccountCheck/SavePurchaseAccountRecord',
      method: 'post',
      data
    })
  },
  savePurchaseExceptionAccount(data) {
    return request2({
      url: fmsDomain + '/AccountCheck/SavePurchaseExceptionAccount',
      method: 'post',
      data
    })
  },
  savePurchaseSettlementOrder(data) {
    return request2({
      url: fmsDomain + '/Settlement/SavePurchaseSettlementOrder',
      method: 'post',
      data
    })
  },
  //计算对账金额
  CalculationReconciliationFee(params) {
    return request2({
      url: domain + '/Fiance/CalculationReconciliationFee',
      method: 'get',
      params
    })
  },
  //批量计算对账金额
  CalculationReconciliationFeeBatch(data) {
    return request2({
      url: domain + '/Fiance/CalculationReconciliationFeeBatch',
      method: 'post',
      data
    })
  },


};

export {
  appSvc
};
