import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_WMS_API;
let appWarehouseSvc = {
  getWarehouses(params) {
    return request2({
      url: domain + "/Warehouse/GetWarehouses",
      method: "get",
      params
    });
  },
  getWarehousePages(params) {
    return request2({
      url: domain + "/Warehouse/GetWarehousePages",
      method: "get",
      params
    });
  },
  createWarehouse(data) {
    return request2({
      url: domain + "/Warehouse/CreateWarehouse",
      method: "post",
      data
    });
  },
  editWarehouseConfig(data) {
    return request2({
      url: domain + "/Warehouse/EditWarehouseConfig",
      method: "post",
      data
    });
  },
  deleteWarehouseConfig(data) {
    return request2({
      url: domain + "/Warehouse/DeleteWarehouseConfig",
      method: "post",
      data
    });
  },
  getWarehouseConfig(params) {
    return request2({
      url: domain + "/Warehouse/GetWarehouseConfig",
      method: "get",
      params
    });
  },

  getBasicInfoList(params) {
    return request2({
      url: domain + "/Warehouse/GetBasicInfoList",
      method: "get",
      params
    });
  },
  getRegionChinaListByRegionId(params) {
    return request2({
      url: domain + "/Warehouse/GetRegionChinaListByRegionId",
      method: "get",
      params
    });
  }
};

export { appWarehouseSvc };
