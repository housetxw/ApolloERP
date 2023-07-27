import request2 from "@/utils/request2";
import request1 from "@/utils/request";
const orderCenter = process.env.VUE_APP_BASE_SHOP_API;
let wmsdomain = process.env.VUE_APP_BASE_WMS_API;
let orderApidomain = process.env.VUE_APP_BASE_ORDERCENTER_API;

const orderCenterSvc = {

  
  exportData(params) {
    return request1({
      url: orderCenter + '/Order/DownloadOrders',
      method: 'get',
      params,
      responseType: 'blob'

    })
  },
  
  // 获取订单列表
  GetOrderInfoListForShop(params) {
    // debugger
    return request2({
      url: orderCenter + "/Order/GetOrderInfoListForShop",
      method: "get",
      params
    });
  },
  // /OrderQuery/GetReverseReasons 获取申请原因集合
  GetReverseReasons(params) {
    // debugger
    return request2({
      url: orderCenter + "/OrderQuery/GetReverseReasons",
      method: "get",
      params
    });
  },
  // /OrderCommand/CancelOrder  取消订单
  CancelOrder(data) {
    return request2({
      url: orderCenter + "/OrderCommand/CancelOrder",
      method: "post",
      data
    });
  },
  // /OrderCommand/CheckOrder
  // 审核确认订单
  CheckOrder(data) {
    return request2({
      url: orderCenter + "/OrderCommand/CheckOrder",
      method: "post",
      data
    });
  },
  //   /OrderCommand/RefundApply
  // 退款申请
  RefundApply(data) {
    return request2({
      url: orderCenter + "/OrderCommand/RefundApply",
      method: "post",
      data
    });
  },
  // /OrderQuery/GetOrderDetail
  // 获取订单详情
  GetOrderDetail(data) {
    return request2({
      url: orderCenter + "/Order/GetOrderBaseInfo",
      method: "post",
      data
    });
  },
  ///获取订单产品
  GetOrderProducts(data) {
    return request2({
      url: orderCenter + "/Order/GetOrderProduct",
      method: "post",
      data
    });
  },
  ///获取订单预约
  GetReserverInfo(params) {
    return request2({
      url: orderCenter + "/Order/GetReserverInfo",
      method: "get",
      params
    });
  },
  ///获取订单派工
  GetDispatchInfo(params) {
    return request2({
      url: orderCenter + "/Order/GetDispatchInfo",
      method: "get",
      params
    });
  },
  // 订单日志列表
  GetOrderLogList(params) {
    return request2({
      url: orderCenter + "/OrderQuery/GetOrderLogList",
      method: "get",
      params
    });
  },
  // 追加下单
  OrderCommand(data) {
    return request2({
      url: orderCenter + "/OrderCommand/AppendSubmitOrder",
      method: "post",
      data
    });
  },
  // /OrderQuery/GetOrderDetail
  // 获取订单保险信息
  GetOrderInsuranceCompany(params) {
    return request2({
      url: orderCenter + "/Order/GetOrderInsuranceCompany",
      method: "get",
      params
    });
  },
  GetOrderCarsInfo(data) {
    return request2({
      url: orderCenter + "/Order/GetOrderCarsInfo",
      method: "post",
      data
    });
  },

  //订单报表详情( Week，Month，Year)
  GetOrderDetailStaticReport(params) {
    return request2({
      url: orderCenter + "/Order/GetOrderDetailStaticReportForShop",
      method: "get",
      params
    });
  },

  //订单统计报表
  GetOrderStaticReport() {
    return request2({
      url: orderCenter + "/Order/GetOrderStaticReportForShop",
      method: "get"
    });
  }, //外采报表
  GetOrderOutProductsProfitReport(data) {
    return request2({
      url: orderCenter + "/Order/GetOrderOutProductsProfitReportForShop",
      method: "post",
      data
    });
  },
  //产品明细
  GetOrderProductsReport(data) {
    return request2({
      url: orderCenter + "/Order/GetOrderProductsReportForShop",
      method: "post",
      data
    });
  },
  getShopList(params) {
    return request2({
      url: wmsdomain + '/ShopManager/GetShopList',
      method: 'get',
      params
    })
  },
  //订单重新安装
  OrderRepeatReduceStock(data) {
    return request2({
      url: orderCenter + "/ShopStock/OrderRepeatReduceStock",
      method: "post",
      data
    });
  },
};
export { orderCenterSvc };
