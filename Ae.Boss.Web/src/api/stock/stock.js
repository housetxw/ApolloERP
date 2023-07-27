import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_STOCK_API;
let wmsdomain = process.env.VUE_APP_BASE_WMS_API;
let purchasedomain = process.env.VUE_APP_BASE_Purchase_API;
let shopApidomain = process.env.VUE_APP_BASE_SHOP_API;
let appSvc = {
  getStockLocationTrackList(params) {
    return request2({
      url: domain + "/StockLocation/GetStockLocationTrackList",
      method: "get",
      params
    });
  },
  getStockLocationTrackDetails(params) {
    return request2({
      url: domain + "/StockLocation/GetStockLocationTrackDetails",
      method: "get",
      params
    });
  },
  //获取仓库信息
  getBasicInfoList(params) {
    return request2({
      url: wmsdomain + "/Warehouse/GetWarehouses",
      method: "get",
      params
    });
  },
  //获取供应商信息
  getVenders(params) {
    return request2({
      url: purchasedomain + "/Vender/GetVenders",
      method: "get",
      params
    });
  },

  getStockTrackDetail(params) {
    return request2({
      url: domain + "/StockLocation/GetStockTrackDetail",
      method: "get",
      params
    });
  },
  getInventoryBatchs(params) {
    return request2({
      url: shopApidomain + "/StockManage/GetInventoryBatchs",
      method: "get",
      params
    });
  },
  getInventoryFlowItems(params) {
    return request2({
      url: shopApidomain + "/StockManage/GetInventoryFlowItems",
      method: "get",
      params
    });
  },
  getShopStockList(params) {
    return request2({
      url: shopApidomain + "/StockManage/GetShopStockList",
      method: "get",
      params
    });
  },
  GetStockInoutItemsByPid(params) {
    return request2({
      url: shopApidomain + "/StockManage/GetStockInoutItemsByPid",
      method: "get",
      params
    });
  },
  getShopList(params) {
    return request2({
      url: wmsdomain + '/ShopManager/GetShopList',
      method: 'get',
      params
    })
  },

  getSourceWarehouses(params) {
    return request2({
      url: wmsdomain + "/Transfer/GetSourceWarehouses",
      method: "get",
      params
    });
  },
  getProductStocks(data) {
    return request2({
      url: wmsdomain + '/ProductStock/GetProductStocks',
      method: 'post',
      data
    })
  },
  getProductStockDetails(data) {
    return request2({
      url: wmsdomain + '/ProductStock/GetProductStockDetails',
      method: 'post',
      data
    })
  }
};

export { appSvc };
