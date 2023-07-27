import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_SHOP_API;
let shopManage = process.env.VUE_APP_BASE_SHOPMANAGE_API;
let appSvc = {
  //查询公司列表
  getPageCompanyListForShopAsync(params) {
    return request2({
      url: domain + "/Company/GetPageCompanyListForShopAsync",
      method: "get",
      params
    });
  },
  //新增公司
  addOrUpdateCompany(data) {
    return request2({
      url: domain + "/Company/AddOrUpdateCompany",
      method: "post",
      data
    });
  },
  //查询公司信息
  getCompanyInfo(params) {
    return request2({
      url: domain + "/Company/GetCompanyInfo",
      method: "get",
      params
    });
  },
  //修改公司状态
  updateCompanyStatus(data) {
    return request2({
      url: shopManage + "/Company/UpdateCompanyStatus",
      method: "post",
      data
    });
  },
};
export { appSvc };
