import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_WMS_API;
let purchasedomain = process.env.VUE_APP_BASE_Purchase_API
let appSvc = {
  deleteProductBarcodeConfig(data) {
    return request2({
      url: domain + "/ProductBarCode/DeleteProductBarcodeConfig",
      method: "post",
      data
    });
  },
  getProductBarcodeConfigs(params) {
    return request2({
      url: domain + "/ProductBarCode/GetProductBarcodeConfigs",
      method: "get",
      params
    });
  },
  addProductBarcodeConfig(data) {
    return request2({
      url: domain + "/ProductBarCode/AddProductBarcodeConfig",
      method: "post",
      data
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
