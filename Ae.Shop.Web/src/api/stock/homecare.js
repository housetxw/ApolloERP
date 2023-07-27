import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_SHOP_API;

let appSvc = {
    getHomeCareRecordPages(data) {
    return request2({
      url: domain + "/HomeCare/GetHomeCareRecordPages",
      method: "post",
      data
    });
  },
  createHomeCareRecord(data) {
    return request2({
      url: domain + "/HomeCare/CreateHomeCareRecord",
      method: "post",
      data
    });
  },
  
  getHomeCareProducts(params) {
    return request2({
      url: domain + "/HomeCare/GetHomeCareProducts",
      method: "get",
      params
    });
  },
  getEmployes(params) {
    return request2({
      url: domain + "/HomeCare/GetEmployes",
      method: "get",
      params
    });
  },
  getShopProducts(params) {
    return request2({
      url: domain + "/StockManage/GetShopProducts",
      method: "get",
      params
    });
  },
  getCurrentUser(params) {
    return request2({
      url: domain + "/StockManage/GetCurrentUser",
      method: "get",
      params
    });
  },
  getProductStocks(data) {
    return request2({
      url: domain + "/StockManage/GetProductStocks",
      method: "post",
      data
    });
  },
  getProductStocksForOut(data) {
    return request2({
      url: domain + "/StockManage/GetProductStocksForOut",
      method: "post",
      data
    });
  },
  
  getShopById(params) {
    return request2({
      url: domain + "/StockManage/GetShopById",
      method: "get",
      params
    });
  },
  getHomeReturnRecordPages(data) {
    return request2({
      url: domain + "/HomeCare/GetHomeReturnRecordPages",
      method: "post",
      data
    });
  },
  createHomeReturnRecord(data) {
    return request2({
      url: domain + "/HomeCare/CreateHomeReturnRecord",
      method: "post",
      data
    });
  },
  getHomeReturnProducts(params) {
    return request2({
      url: domain + "/HomeCare/GetHomeReturnProducts",
      method: "get",
      params
    });
  },
  getHomeReturnProductsByTech(params) {
    return request2({
      url: domain + "/HomeCare/GetHomeReturnProductsByTech",
      method: "get",
      params
    });
  },


  getProductClaimRecordPages(data) {
    return request2({
      url: domain + "/HomeCare/GetProductClaimRecordPages",
      method: "post",
      data
    });
  },
  createProductClaimRecord(data) {
    return request2({
      url: domain + "/HomeCare/CreateProductClaimRecord",
      method: "post",
      data
    });
  },
  CancelProductClaimRecord(data) {
    return request2({
      url: domain + "/HomeCare/CancelProductClaimRecord",
      method: "post",
      data
    });
  },
  ClaimRepeatReduceStock(data) {
    return request2({
      url: domain + "/HomeCare/ClaimRepeatReduceStock",
      method: "post",
      data
    });
  },
  getProductClaimRecords(params) {
    return request2({
      url: domain + "/HomeCare/GetProductClaimRecords",
      method: "get",
      params
    });
  } 


};

export { appSvc };
