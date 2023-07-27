import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_SHOP_API;
let appSvc = {
  searchVenderProductListForApp(params) {
    return request2({
      url: domain + "/PurchaseStock/SearchVenderProductListForApp",
      method: "get",
      params
    });
  },
  submitVenderStock(data) {
    return request2({
      url: domain + "/PurchaseStock/SubmitVenderStock",
      method: "post",
      data
    });
  },
  searchVenderProductList(params) {
    return request2({
      url: domain + "/PurchaseStock/SearchVenderProductList",
      method: "get",
      params
    });
  },
  submitCompanyInStock(data) {
    return request2({
      url: domain + "/PurchaseStock/SubmitCompanyInStock",
      method: "post",
      data
    });
  },
  getCompanyInStocks(params) {
    return request2({
      url: domain + "/PurchaseStock/GetCompanyInStocks",
      method: "get",
      params
    });
  },
  
  companySendStock(data) {
    return request2({
      url: domain + "/PurchaseStock/CompanySendStock",
      method: "post",
      data
    });
  },
  getCompanyStocks(params) {
    return request2({
      url: domain + "/PurchaseStock/GetCompanyStocks",
      method: "get",
      params
    });
  },
  cancelCompanySendStock(data) {
    return request2({
      url: domain + "/PurchaseStock/CancelCompanySendStock",
      method: "post",
      data
    });
  },
    
  venderCheckInStock(data) {
    return request2({
      url: domain + "/PurchaseStock/VenderCheckInStock",
      method: "post",
      data
    });
  }
};

export { appSvc };
