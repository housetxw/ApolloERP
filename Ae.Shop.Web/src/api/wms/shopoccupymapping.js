import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_WMS_API;
let appSvc = {
  deleteShopOccupyMapping(data) {
    return request2({
      url: domain + "/ShopOccupyMapping/DeleteShopOccupyMapping",
      method: "post",
      data
    });
  },
  updateShopOccupyMapping(data) {
    return request2({
      url: domain + "/ShopOccupyMapping/UpdateShopOccupyMapping",
      method: "post",
      data
    });
  },
  addShopOccupyMapping(data) {
    return request2({
      url: domain + "/ShopOccupyMapping/AddShopOccupyMapping",
      method: "post",
      data
    });
  },  
  getShopList(params) {
    return request2({
      url: domain + '/ShopManager/GetShopList',
      method: 'get',
      params
    })
  },

  getShopListByType(params) {
    return request2({
      url: domain + '/ShopManager/GetShopListByType',
      method: 'get',
      params
    })
  },
  getShopOccupyMappingInfo(params) {
    return request2({
      url: domain + '/ShopOccupyMapping/GetShopOccupyMappingInfo',
      method: 'get',
      params
    })
  },
  getShopOccupyMappings(params) {
    return request2({
      url: domain + "/ShopOccupyMapping/GetShopOccupyMappings",
      method: "get",
      params
    });
  }
};

export { appSvc };
