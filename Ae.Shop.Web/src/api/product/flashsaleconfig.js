import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_Product_API;
let purchasedomain = process.env.VUE_APP_BASE_Purchase_API
let appSvc = {
    updateFlashSaleConfig(data) {
    return request2({
      url: domain + "/FlashSaleConfig/UpdateFlashSaleConfig",
      method: "post",
      data
    });
  },  
  creatFlashSaleConfig(data) {
    return request2({
      url: domain + "/FlashSaleConfig/CreatFlashSaleConfig",
      method: "post",
      data
    });
  },
  getFlashSaleConfigs(params) {
    return request2({
      url: domain + "/FlashSaleConfig/GetFlashSaleConfigs",
      method: "get",
      params
    });
  },
  productSearch(params) {
    return request2({
      url: purchasedomain + '/Product/Search',
      method: 'get',
      params
    })
  }
};

export { appSvc };
