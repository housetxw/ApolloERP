import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_WMS_API;
let appSvc = {
  getBasicInfoList(params) {
    return request2({
      url: domain + "/WarehouseTransfer/GetBasicInfoList",
      method: "get",
      params
    });
  },
  getAllotPageData(data) {
    return request2({
      url: domain + "/Transfer/GetAllotPageData",
      method: "post",
      data
    });
  },
  createAllotTask(data) {
    return request2({
      url: domain + "/Transfer/CreateAllotTask",
      method: "post",
      data
    });
  },
  cancelAllotTask(data) {
    return request2({
      url: domain + "/Transfer/CancelAllotTask",
      method: "post",
      data
    });
  },
  
  getAllotProductsStock(data) {
    debugger;
    return request2({
      url: domain + "/Transfer/GetAllotProductsStock",
      method: "post",
      data
    });
  },
  getSourceWarehouses(params) {
    return request2({
      url: domain + "/Transfer/GetSourceWarehouses",
      method: "get",
      params
    });
  },
  getCurrentUser(params) {
    return request2({
      url: domain + "/Transfer/GetCurrentUser",
      method: "get",
      params
    });
  },
  searchProduct(data) {
    return request2({
      url: domain + "/ReplenishTask/SearchProduct",
      method: "post",
      data
    });
  },
  auditAllotTask(data) {
    return request2({
      url: domain + "/Transfer/AuditAllotTask",
      method: "post",
      data
    });
  },
  getAllotTaskStatus(params) {
    return request2({
      url: domain + "/Transfer/GetAllotTaskStatus",
      method: "get",
      params
    });
  },
  getAllotTaskDetail(params) {
    return request2({
      url: domain + "/Transfer/GetAllotTaskDetail",
      method: "get",
      params
    });
  },
  getWmsLogs(params) {
    return request2({
      url: domain + "/Asn/GetWmsLogs",
      method: "get",
      params
    });
  }

  
};

export { appSvc };
