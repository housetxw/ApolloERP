import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_WMS_API;
let appSvc = {
  getBasicInfoList(params) {
    return request2({
      url: domain + '/ReplenishTask/GetBasicInfoList',
      method: 'get',
      params
    })
  },
  getReplenishTaskList(params) {
    return request2({
      url: domain + '/ReplenishTask/GetReplenishTasks',
      method: 'get',
      params
    })
  },
  createReplenishTask(data) {
    return request2({
      url: domain + '/ReplenishTask/CreateReplenishTask',
      method: 'post',
      data
    })
  },
  rejectReplenishTask(data) {
    return request2({
      url: domain + '/ReplenishTask/RejectReplenishTask',
      method: 'post',
      data
    })
  },
  passReplenishTask(data) {
    return request2({
      url: domain + '/ReplenishTask/PassReplenishTask',
      method: 'post',
      data
    })
  },
  getStockLocations(params) {
    return request2({
      url: domain + '/ReplenishTask/GetStockLocations',
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
  createWarehouseTransferTask(data){
    return request2({
      url: domain + '/ReplenishTask/CreateWarehouseTransferTask',
      method: 'post',
      data
    })
  },
  searchProduct(data){
    return request2({
      url: domain + '/ReplenishTask/SearchProduct',
      method: 'post',
      data
    })
  }  
};

export {
  appSvc
};