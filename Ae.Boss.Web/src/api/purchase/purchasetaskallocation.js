import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_Purchase_API;
let appSvc = {
  getPurchaseTaskAllocations(params) {
    return request2({
      url: domain + "/PurchaseTaskAllocation/GetPurchaseTaskAllocations",
      method: "get",
      params
    });
  },

  updatePurchaseTaskAllocation(data) {
    return request2({
      url: domain + "/PurchaseTaskAllocation/UpdatePurchaseTaskAllocation",
      method: "post",
      data
    });
  },
  getBasicInfoList(params) {
    return request2({
      url: domain + '/PurchaseOrder/GetBasicInfoList',
      method: 'get',
      params
    })
  },
  getVenders(params) {
    return request2({
      url: domain+'/Vender/GetVenders',
      method: 'get',
      params
    })
  },
  getPurchaseTaskAllocationDetail(params) {
    return request2({
      url: domain+'/PurchaseTaskAllocation/GetPurchaseTaskAllocationDetail',
      method: 'get',
      params
    })
  },
  createPurchaseOrder(data) {
    return request2({
      url: domain + '/PurchaseTaskAllocation/CreatePurchaseOrder',
      method: 'post',
      data
    })
  },
  createSwtichWarehouseApply(data) {
    return request2({
      url: domain + '/PurchaseTaskAllocation/CreateSwtichWarehouseApply',
      method: 'post',
      data
    })
  },
  
  selectDefaultWarehouseConfigs(params) {
    return request2({
      url: domain + '/OrderSwitchWarehouse/SelectDefaultWarehouseConfigs',
      method: 'get',
      params
    })
  },
  getSwitchWarehouseHistorys(params) {
    return request2({
      url: domain + '/OrderSwitchWarehouse/GetSwitchWarehouseHistorys',
      method: 'get',
      params
    })
  }
};

export { appSvc };
