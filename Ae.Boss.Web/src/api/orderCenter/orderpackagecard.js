import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_ORDERCENTER_API;
let appSvc = {

  getOrderPackageCards(params) {
    return request2({
      url: domain + "/OrderQuery/GetOrderPackageCards",
      method: "get",
      params
    });
  },
  // /OrderCommand/CheckOrder
  // 审核确认订单
  UpdateOrderPackage(data) {
    return request2({
      url: domain + '/OrderCommand/UpdateOrderPackage',
      method: 'post',
      data
    })
  },
};

export { appSvc };
