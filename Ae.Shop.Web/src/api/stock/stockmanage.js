import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_SHOP_API;

let appSvc = {
  getOrgType(params) {
    return request2({
      url: domain + "/Company/GetOrgType",
      method: "get",
      params
    });
  },
  getShopStockList(params) {
    return request2({
      url: domain + "/StockManage/GetShopStockList",
      method: "get",
      params
    });
  },
  getStockInOutPageData(data) {
    return request2({
      url: domain + "/StockManage/GetStockInOutPageData",
      method: "post",
      data
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
  //获取当前门店详情
  getShopById(params) {
    return request2({
      url: domain + "/StockManage/GetShopById",
      method: "get",
      params
    });
  },
  getStockInoutItems(data) {
    return request2({
      url: domain + "/StockManage/GetStockInoutItems",
      method: "post",
      data
    });
  },
  GetStockInoutItemsByPid(params) {
    return request2({
      url: domain + "/StockManage/GetStockInoutItemsByPid",
      method: "get",
      params
    });
  },
  createInStockTask(data) {
    return request2({
      url: domain + "/StockManage/CreateInStockTask",
      method: "post",
      data
    });
  },
  createOutStockTask(data) {
    return request2({
      url: domain + "/StockManage/CreateOutStockTask",
      method: "post",
      data
    });
  },
  getPurchaseOrderProducts(params) {
    return request2({
      url: domain + "/StockManage/GetPurchaseOrderProducts",
      method: "get",
      params
    });
  },
  getStockInOutInfo(params) {
    return request2({
      url: domain + "/StockManage/GetStockInOutInfo",
      method: "get",
      params
    });
  },
  getStoragePlans(data) {
    return request2({
      url: domain + "/StockManage/GetStoragePlans",
      method: "post",
      data
    });
  },
  getStoragePlanProducts(params) {
    return request2({
      url: domain + "/StockManage/GetStoragePlanProducts",
      method: "get",
      params
    });
  },
  createStoragePlan(data) {
    return request2({
      url: domain + "/StockManage/CreateStoragePlan",
      method: "post",
      data
    });
  },
  cancelStoragePlanProduct(data) {
    return request2({
      url: domain + "/StockManage/CancelStoragePlanProduct",
      method: "post",
      data
    });
  },

  updateStoragePlanProduct(data) {
    return request2({
      url: domain + "/StockManage/UpdateStoragePlanProduct",
      method: "post",
      data
    });
  },
  updateStoragePlanProductStatus(data) {
    return request2({
      url: domain + "/StockManage/UpdateStoragePlanProductStatus",
      method: "post",
      data
    });
  },
  getStockDiffs(data) {
    return request2({
      url: domain + "/StockManage/GetStockDiffs",
      method: "post",
      data
    });
  },
  getStockDiffDetail(params) {
    return request2({
      url: domain + "/StockManage/GetStockDiffDetail",
      method: "get",
      params
    });
  },
  dealStorageProduct(data) {
    return request2({
      url: domain + "/StockManage/DealStorageProduct",
      method: "post",
      data
    });
  },
  stockInOutTaskDelivery(data) {
    return request2({
      url: domain + "/StockManage/StockInOutTaskDelivery",
      method: "post",
      data
    });
  },
  cancelStockInoutTask(data) {
    return request2({
      url: domain + "/StockManage/CancelStockInoutTask",
      method: "post",
      data
    });
  },
  getStoragePlan(params) {
    return request2({
      url: domain + "/StockManage/GetStoragePlan",
      method: "get",
      params
    });
  },
  getInventoryBatchs(params) {
    return request2({
      url: domain + "/StockManage/GetInventoryBatchs",
      method: "get",
      params
    });
  },
  getInventoryFlowItems(params) {
    return request2({
      url: domain + "/StockManage/GetInventoryFlowItems",
      method: "get",
      params
    });
  },
  getOrderQueues(params) {
    return request2({
      url: domain + "/ShopStock/GetOrderQueues",
      method: "get",
      params
    });
  },

  reOccupyStock(data) {
    return request2({
      url: domain + "/ShopStock/ReOccupyStock",
      method: "post",
      data
    });
  },
  cancelOrderQueue(data) {
    return request2({
      url: domain + "/ShopStock/CancelOrderQueue",
      method: "post",
      data
    });
  },
  getOccupyStockLogs(params) {
    return request2({
      url: domain + "/ShopStock/GetOccupyStockLogs",
      method: "get",
      params
    });
  },
  getOccupyItems(params) {
    return request2({
      url: domain + "/ShopStock/GetOccupyItems",
      method: "get",
      params
    });
  },
  getStockInTypes(params) {
    return request2({
      url: domain + "/StockManage/GetStockInTypes",
      method: "get",
      params
    });
  },
  getStockOutTypes(params) {
    return request2({
      url: domain + "/StockManage/GetStockOutTypes",
      method: "get",
      params
    });
  },
  getShopWmsLogs(params) {
    return request2({
      url: domain + "/StockManage/GetShopWmsLogs",
      method: "get",
      params
    });
  },   
  getSupplierProductStocks(params) {
    return request2({
      url: domain + "/StockManage/GetSupplierProductStocks",
      method: "get",
      params
    });
  },
  getSupplierProductStockDetails(params) {
    return request2({
      url: domain + "/StockManage/GetSupplierProductStockDetails",
      method: "get",
      params
    });
  },
  GetShopProductStocks(data) {
    return request2({
      url: domain + "/StockManage/GetShopProductStocks",
      method: "post",
      data
    });
  },
  GetCompanyShopList(params) {
    return request2({
      url: domain + '/ShopManage/GetCompanyShopList',
      method: 'get',
      params
    })
  },
  GetShopWareHouseList(params) {
    return request2({
      url: domain + '/ShopManage/GetShopWareHouseList',
      method: 'get',
      params
    })
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
  getAllotTaskDetail(params) {
    return request2({
      url: domain + "/Transfer/GetAllotTaskDetail",
      method: "get",
      params
    });
  },
  getAllotTaskStatus(params) {
    return request2({
      url: domain + "/Transfer/GetAllotTaskStatus",
      method: "get",
      params
    });
  },
  getSourceWarehouses(params) {
    return request2({
      url: domain + "/Transfer/GetSourceWarehouses",
      method: "get",
      params
    });
  },
  getWmsLogs(params) {
    return request2({
      url: domain + "/Transfer/GetWmsLogs",
      method: "get",
      params
    });
  }
  
  
};

export { appSvc };
