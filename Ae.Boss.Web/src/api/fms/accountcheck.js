import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_FMS_API;

let appSvc = {
  // 门店未对账列表
    getShopAccountUnChecks(data) {
    return request2({
      url: domain + '/AccountCheck/GetShopAccountUnChecks',
      method: 'post',
      data
    })
  },
  //门店对账列表
  getShopAccountChecks(data) {
    return request2({
      url: domain + '/AccountCheck/GetShopAccountChecks',
      method: 'post',
      data
    })
  },
//总部对账列表
  getRgAccountChecks(data) {
    return request2({
      url: domain + '/AccountCheck/GetRgAccountChecks',
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
  }
  
};

export {
  appSvc
};