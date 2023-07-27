import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_Purchase_API;
let appSvc = {
    createPurchaseOrder(data) {
    return request2({
      url: domain + '/PurchaseOrder/CreateOrUpdatePurchaseOrder',
      method: 'post',
      data
    })
  },
  getPurchaseOrderList(params) {
    return request2({
      url: domain + '/PurchaseOrder/GetPurchaseOrderList',
      method: 'get',
      params
    })
  },
  getBasicInfoList(params) {
    return request2({
      url: domain + '/PurchaseOrder/GetBasicInfoList',
      method: 'get',
      params
    })
  },
  getPurchaseOrderProduct(params) {
    return request2({
      url: domain + '/PurchaseOrder/GetPurchaseOrderProduct',
      method: 'get',
      params
    })
  },
  selectSopos(params) {
    return request2({
      url: domain + '/Sopo/SelectSopos',
      method: 'get',
      params
    })
  },

  sendPurchaseOrder(data) {
    return request2({
      url: domain + '/PurchaseOrder/SendPurchaseOrder',
      method: 'post',
      data
    })
  },

  deletePurchaseOrderProduct(data) {
    return request2({
      url: domain + '/PurchaseOrder/DeletePurchaseOrderProduct',
      method: 'post',
      data
    })
  },
  productSearch(params) {
    return request2({
      url: domain + '/Product/Search',
      method: 'get',
      params
    })
  },
  getPurchaseOrderTrackLogs(params) {
    return request2({
      url: domain + '/PurchaseOrder/GetPurchaseOrderTrackLogs',
      method: 'get',
      params
    })
  },
  getPurchaseOrderDeliveries(params) {
    return request2({
      url: domain + '/PurchaseOrderDelivery/GetPurchaseOrderDeliveries',
      method: 'get',
      params
    })
  },
  createPurchaseOrderDelivery(data) {
    return request2({
      url: domain + '/PurchaseOrderDelivery/CreatePurchaseOrderDelivery',
      method: 'post',
      data
    })
  },
  updatePurchaseOrderDeliveryStatus(data) {
    return request2({
      url: domain + '/PurchaseOrderDelivery/UpdatePurchaseOrderDeliveryStatus',
      method: 'post',
      data
    })
  },
  cancelPurchaseOrder(data) {
    return request2({
      url: domain + '/PurchaseOrder/CancelPurchaseOrder',
      method: 'post',
      data
    })
  },
  
  purchaseOrderReturn(data) {
    return request2({
      url: domain + '/PurchaseOrder/PurchaseOrderReturn',
      method: 'post',
      data
    })
  }
};

export {
  appSvc
};