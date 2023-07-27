import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_WMS_API;
let appSvc = {
  getStoragePlans(data) {
    return request2({
      url: domain + "/StoragePlan/GetStoragePlans",
      method: "post",
      data
    });
  },
  getStoragePlanProducts(params) {
    return request2({
      url: domain + "/StoragePlan/GetStoragePlanProducts",
      method: "get",
      params
    });
  },
  createStoragePlan(data) {
    return request2({
      url: domain + "/StoragePlan/CreateStoragePlan",
      method: "post",
      data
    });
  },
  cancelStoragePlanProduct(data) {
    return request2({
      url: domain + "/StoragePlan/CancelStoragePlanProduct",
      method: "post",
      data
    });
  },

  updateStoragePlanProduct(data) {
    return request2({
      url: domain + "/StoragePlan/UpdateStoragePlanProduct",
      method: "post",
      data
    });
  },
  updateStoragePlanProductStatus(data) {
    return request2({
      url: domain + "/StoragePlan/UpdateStoragePlanProductStatus",
      method: "post",
      data
    });
  },
  getStockDiffs(data) {
    return request2({
      url: domain + "/StoragePlan/GetStockDiffs",
      method: "post",
      data
    });
  },
  getStockDiffDetail(params) {
    return request2({
      url: domain + "/StoragePlan/GetStockDiffDetail",
      method: "get",
      params
    });
  },
  dealStorageProduct(data) {
    return request2({
      url: domain + "/StoragePlan/DealStorageProduct",
      method: "post",
      data
    });
  },
  getStoragePlan(params) {
    return request2({
      url: domain + "/StoragePlan/GetStoragePlan",
      method: "get",
      params
    });
  },
  getWarehouses(params) {
    return request2({
      url: domain + "/Warehouse/GetWarehouses",
      method: "get",
      params
    });
  },
  searchProduct(data){
    return request2({
      url: domain + '/ReplenishTask/SearchProduct',
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

export { appSvc };
