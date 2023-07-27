import request2 from "@/utils/request2";
import request1 from "@/utils/request";
let shopApi = process.env.VUE_APP_BASE_SHOP_API;
let domain = process.env.VUE_APP_BASE_FMS_API;
let shoppurchaseManageSvc = {
  
  getOrgType(params) {
    return request2({
      url: shopApi + "/Company/GetOrgType",
      method: "get",
      params
    });
  },
  //查询区域合伙列表 - 分页
  GetPartnershipCompanyListAsync(params) {
    return request2({
      url: shopApi + "/Company/GetPartnershipCompanyListAsync",
      method: "get",
      params
    });
  },
  //获取所有的供应商列表
  getSupplierList(params) {
    return request2({
      url: shopApi + "/ShopPurchase/GetSupplierList",
      method: "get",
      params
    });
  },
  //搜索采购单信息
  searchPurchaseOrder(params) {
    return request2({
      url: shopApi + '/ShopPurchase/SearchPurchaseOrder',
      method: 'get',
      params
    })
  },
  //搜索采购商品
  searchProduct(params) {
    return request2({
      url: shopApi + '/ShopPurchase/SearchProduct',
      method: 'get',
      params
    })
  },
  exportData(params) {
    return request1({
      url: shopApi + '/ShopPurchase/DownloadOrders',
      method: 'get',
      params,
      responseType: 'blob'

    })
  },
  
  //保存采购单
  savePurchaseOrder(data) {
    return request2({
      url: shopApi + '/ShopPurchase/SavePurchaseOrder',
      method: 'post',
      data
    })
  },
  //查询采购单
  getPurchaseOrderDetail(params) {
    return request2({
      url: shopApi + '/ShopPurchase/GetPurchaseOrderDetail',
      method: 'get',
      params
    })
  },
  getCurrentUser(params) {
    return request2({
      url: shopApi + '/ShopPurchase/GetCurrentUser',
      method: 'get',
      params
    })
  },
  updatePurchaseOrderDeliveryInfo(data) {
    return request2({
      url: shopApi + '/ShopPurchase/UpdatePurchaseOrderDeliveryInfo',
      method: 'post',
      data
    })
  },
  updatePurchasePrice(data) {
    return request2({
      url: shopApi + '/ShopPurchase/UpdatePurchasePrice',
      method: 'post',
      data
    })
  },
  deletePurchaseOrder(data) {
    return request2({
      url: shopApi + '/ShopPurchase/DeletePurchaseOrder',
      method: 'post',
      data
    })
  },
  
  purchaseInStock(data) {
    return request2({
      url: shopApi + '/ShopPurchase/PurchaseInStock',
      method: 'post',
      data
    })
  },
  getEmployes(params) {
    return request2({
      url: shopApi + '/HomeCare/GetEmployes',
      method: 'get',
      params
    })
  },
  getVender(params) {
    return request2({
      url: shopApi + '/ShopPurchase/GetVender',
      method: 'get',
      params
    })
  },
  
  savePayInfo(data) {
    return request2({
      url: shopApi + '/ShopPurchase/SavePayInfo',
      method: 'post',
      data
    })
  },
  //获取采购单付款信息
  getPurchasePayInfo(params) {
    return request2({
      url: shopApi + '/ShopPurchase/GetPurchasePayInfo',
      method: 'get',
      params
    })
  },
  
  getPurchaseOrderPayInfo(params) {
    return request2({
      url: shopApi + '/ShopPurchase/GetPurchaseOrderPayInfo',
      method: 'get',
      params
    })
  },


  upsertPurchaseMonthPayInfo(data) {
    return request2({
      url: shopApi + '/ShopPurchase/UpsertPurchaseMonthPayInfo',
      method: 'post',
      data
    })
  },
  selectPurchaseMonthPayInfos(params) {
    return request2({
      url: shopApi + '/ShopPurchase/SelectPurchaseMonthPayInfos',
      method: 'get',
      params
    })
  },

  auditPurchaseMonthPay(data) {
    return request2({
      url: shopApi + '/ShopPurchase/AuditPurchaseMonthPay',
      method: 'post',
      data
    })
  },

  savePurchaseMonthPay(data) {
    return request2({
      url: shopApi + '/ShopPurchase/SavePurchaseMonthPay',
      method: 'post',
      data
    })
  },

  getVenderPurchaseOrders(data) {
    return request2({
      url: shopApi + '/ShopPurchase/GetVenderPurchaseOrders',
      method: 'post',
      data
    })
  },
  getTargetVenders(params) {
    return request2({
      url: shopApi + '/ShopPurchase/GetTargetVenders',
      method: 'get',
      params
    })
  },

  getTargetPurchaseOrders(params) {
    return request2({
      url: shopApi + '/ShopPurchase/GetTargetPurchaseOrders',
      method: 'get',
      params
    })
  },
  purchaseReturn(data) {
    return request2({
      url: shopApi + '/ShopPurchase/PurchaseReturn',
      method: 'post',
      data
    })
  },
  purchaseReturnPart(data) {
    return request2({
      url: shopApi + '/ShopPurchase/PurchaseReturnPart',
      method: 'post',
      data
    })
  },
  releasePurchaseOccupyStock(data) {
    return request2({
      url: shopApi + '/ShopPurchase/ReleasePurchaseOccupyStock',
      method: 'post',
      data
    })
  },
  getShopListAsync(params) {
    return request2({
      url: domain + '/ShopManage/GetShopListAsync',
      method: 'get',
      params
    })
  },
  GetCompanyShopList(params) {
    return request2({
      url: shopApi + '/ShopManage/GetCompanyShopList',
      method: 'get',
      params
    })
  },
  GetShopWareHouseList(params) {
    return request2({
      url: shopApi + '/ShopManage/GetShopWareHouseList',
      method: 'get',
      params
    })
  },
  updatePurchaseDeliveryCode(data) {
    return request2({
      url: shopApi + '/ShopPurchase/UpdatePurchaseDeliveryCode',
      method: 'post',
      data
    })
  }
  

  
  
}

export {
  shoppurchaseManageSvc
}
