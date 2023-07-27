import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_WMS_API;
let appSvc = {
  getBasicInfoList(params) {
    return request2({
      url: domain + '/Asn/GetBasicInfoList',
      method: 'get',
      params
    })
  },
  getAsnList(params) {
    return request2({
      url: domain + '/Asn/GetAsnList',
      method: 'get',
      params
    })
  },
  asnSign(data) {
    return request2({
      url: domain + '/Asn/AsnSign',
      method: 'post',
      data
    })
  },
  getAsnReceiptProducts(params) {
    return request2({
      url: domain + '/Asn/GetAsnReceiptProducts',
      method: 'get',
      params
    })
  },
  // 上报差异
  createAsnEndreceiptProduct(data) {
    return request2({
      url: domain + '/Asn/CreateAsnEndreceiptProduct',
      method: 'post',
      data
    })
  },
  createAsnReceiptProduct(data) {
    return request2({
      url: domain + '/Asn/CreateAsnReceiptProduct',
      method: 'post',
      data
    })
  },
  getPurchaseOrderDeliveries(params) {
    return request2({
      url: domain + '/Asn/GetPurchaseOrderDeliveries',
      method: 'get',
      params
    })
  },
  
  updatePurchaseOrderDeliveryStatusCommon(data) {
    return request2({
      url: domain + '/Asn/UpdatePurchaseOrderDeliveryStatusCommon',
      method: 'post',
      data
    })
  },
  getWmsLogs(params) {
    return request2({
      url: domain + '/Asn/GetWmsLogs',
      method: 'get',
      params
    })
  }
};

export {
  appSvc
};