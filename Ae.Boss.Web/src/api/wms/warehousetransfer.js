import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_WMS_API;
let appSvc = {
    getBasicInfoList(params) {
    return request2({
      url: domain + '/WarehouseTransfer/GetBasicInfoList',
      method: 'get',
      params
    })
  },
  getWarehouseTransferList(params) {
    return request2({
      url: domain + '/WarehouseTransfer/GetWarehouseTransferList',
      method: 'get',
      params
    })
  },
  createWarehouseTransferPackage(data) {
    return request2({
      url: domain + '/WarehouseTransfer/CreateWarehouseTransferPackage',
      method: 'post',
      data
    })
  },
  getWarehouseTransferPackages(params) {
    return request2({
      url: domain + '/WarehouseTransfer/GetWarehouseTransferPackageList',
      method: 'get',
      params
    })
  },
  getShopList(params) {
    return request2({
      url: domain + '/ShopManager/GetShopList',
      method: 'get',
      params
    })
  },
  updateWarehouseTransferStatus(data) {
    return request2({
      url: domain + '/WarehouseTransfer/UpdateWarehouseTransferStatus',
      method: 'post',
      data
    })
  }
};

export {
  appSvc
};