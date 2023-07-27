import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_Purchase_API;
let wmsdomain = process.env.VUE_APP_BASE_WMS_API;
let appSvc = {
    getVenderProducts(params) {
    return request2({
      url: domain + "/VenderProduct/GetVenderProducts",
      method: "get",
      params
    });
  },
  getCompanys(params) {
    return request2({
      url: domain + "/VenderProduct/GetCompanys",
      method: "get",
      params
    });
  },
  getHomeRateData(params) {
    return request2({
      url: domain + "/HomeData/GetHomeRateData",
      method: "get",
      params
    });
  },
  submitVenderStock(data) {
    return request2({
      url: wmsdomain + "/VenderStockInit/SubmitVenderStock",
      method: "post",
      data
    });
  }  
};

export { appSvc };
