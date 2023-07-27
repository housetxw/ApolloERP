import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_Purchase_API;
let appSvc = {
  getBasicInfoList(params) {
    return request2({
      url: domain + "/PurchaseOrder/GetBasicInfoList",
      method: "get",
      params
    });
  },
  createPurchaseWarehouseConfig(data) {
    return request2({
      url: domain + "/PurchaseWarehouseConfig/CreatePurchaseWarehouseConfig",
      method: "post",
      data
    });
  },
  getPurchaseWarehouseConfigPages(params) {
    return request2({
      url: domain + "/PurchaseWarehouseConfig/GetPurchaseWarehouseConfigPages",
      method: "get",
      params
    });
  },

  deletePurchaseWarehouseConfig(data) {
    return request2({
      url: domain + "/PurchaseWarehouseConfig/DeletePurchaseWarehouseConfig",
      method: "post",
      data
    });
  },
  editPurchaseWarehouseConfig(data) {
    return request2({
      url: domain + "/PurchaseWarehouseConfig/EditPurchaseWarehouseConfig",
      method: "post",
      data
    });
  },
  getRegionChinaListByRegionId(params) {
    return request2({
      url: domain+'/PurchaseOrder/GetRegionChinaListByRegionIdDTOs',
      method: 'get',
      params
    });
  }
};

export { appSvc };
