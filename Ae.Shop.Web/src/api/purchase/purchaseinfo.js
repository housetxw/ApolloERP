import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_Purchase_API
let appSvc = {
    createPurchaseInfoConfig(data) {
    return request2({
      url: domain+'/PurchaseInfo/CreatePurchaseInfoConfig',
      method: 'post',
      data
    })
  },
  deletePurchaseInfoConfig(data) {
    return request2({
      url: domain+'/PurchaseInfo/DeletePurchaseInfoConfig',
      method: 'post',
      data
    })
  },

  eEditPurchaseInfoConfig(data) {
    return request2({
      url: domain+'/PurchaseInfo/EditPurchaseInfoConfig',
      method: 'post',
      data
    })
  },

  getPurchaseInfoConfigPages(params) {
    return request2({
      url: domain+'/PurchaseInfo/GetPurchaseInfoConfigPages',
      method: 'get',
      params
    })
  },
  getBasicInfoList(params) {
    return request2({
      url: domain+'/PurchaseInfo/GetBasicInfoList',
      method: 'get',
      params
    })
  },
  productSearch(params) {
    return request2({
      url: domain + '/Product/Search',
      method: 'get',
      params
    })
  }
  
}

export {
    appSvc
}