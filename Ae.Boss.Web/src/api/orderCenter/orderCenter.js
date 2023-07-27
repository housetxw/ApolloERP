
import request2 from '@/utils/request2'
const orderCenter = process.env.VUE_APP_BASE_ORDERCENTER_API

const orderCenterSvc = {
  // 获取订单列表
  GetOrderList(params) {
    // debugger

    return request2({
      url: orderCenter + '/OrderQuery/GetOrderList',
      method: 'get',
      params
    })
  },
  // 获取合并订单列表
  GetMergeOrderList(params) {
    // debugger

    return request2({
      url: orderCenter + '/OrderQuery/GetMergeOrderList',
      method: 'get',
      params
    })
  },
  // 获取首次加载订单确认页信息
  GetOrderConfirm(data) {
    return request2({
      url: orderCenter + '/OrderQuery/GetOrderConfirm',
      method: 'post',
      data
    })
  },
  // 试算订单金额
  TrialCalcOrderAmount(data) {
    return request2({
      url: orderCenter + '/OrderQuery/TrialCalcOrderAmount',
      method: 'post',
      data
    })
  },
  // /OrderQuery/GetReverseReasons 获取申请原因集合
  GetReverseReasons(params) {
    // debugger
    return request2({
      url: orderCenter + '/OrderQuery/GetReverseReasons',
      method: 'get',
      params
    })
  },
  // /OrderCommand/CancelOrder  取消订单
  CancelOrder(data) {
    return request2({
      url: orderCenter + '/OrderCommand/CancelOrder',
      method: 'post',
      data
    })
  },
  // /OrderCommand/CheckOrder
  // 审核确认订单
  CheckOrder(data) {
    return request2({
      url: orderCenter + '/OrderCommand/CheckOrder',
      method: 'post',
      data
    })
  },
  //   /OrderCommand/RefundApply
  // 退款申请
  RefundApply(data) {
    return request2({
      url: orderCenter + '/OrderCommand/RefundApply',
      method: 'post',
      data
    })
  },
  // /OrderQuery/GetOrderDetail
  // 获取订单详情
  GetOrderDetail(params) {
    return request2({
      url: orderCenter + '/OrderQuery/GetOrderDetail',
      method: 'get',
      params
    })
  },
  // 订单日志列表
  GetOrderLogList(params) {
    return request2({
      url: orderCenter + '/OrderQuery/GetOrderLogList',
      method: 'get',
      params
    })
  },
  // 追加下单
  OrderCommand(data) {
    return request2({
      url: orderCenter + '/OrderCommand/AppendSubmitOrder',
      method: 'post',
      data
    })
  },
  //订单提交
  SubmitOrder(data) {
    return request2({
      url: orderCenter + '/OrderCommand/SubmitOrder',
      method: 'post',
      data
    })
  },

  //订单手动优惠
  UpdateCouponAmount(data) {
    return request2({
      url: orderCenter + '/OrderCommand/UpdateCouponAmount',
      method: 'post',
      data
    })
  },

  //更新订单产品的成本价格
  UpdateOrderProductCostPrice(data) {
    return request2({
      url: orderCenter + '/OrderCommand/UpdateOrderProductCostPrice',
      method: 'post',
      data
    })
  },

  //更新订单产品的优惠和实付金额
  UpdateOrderProductActualPayAmount(data) {
    return request2({
      url: orderCenter + '/OrderCommand/UpdateOrderProductActualPayAmount',
      method: 'post',
      data
    })
  },

  //批次更新订单产品的成本价格\优惠和实付金额
  BatchUpdateOrderProductCostPriceActualPayAmount(data) {
    return request2({
      url: orderCenter + '/OrderCommand/BatchUpdateOrderProductCostPriceActualPayAmount',
      method: 'post',
      data
    })
  },

  //批次更新员工绩效订单
  BatchUpdateEmployeePerformanceOrder(data) {
    return request2({
      url: orderCenter + '/OrderCommand/BatchUpdateEmployeePerformanceOrder',
      method: 'post',
      data
    })
  },

  //修改订单预约时间
  ModifyReserveTime(data) {
    return request2({
      url: orderCenter + '/Receive/ModifyReserveTime',
      method: 'post',
      data
    })
  },
  //更新订单地址
  UpdateOrderAddress(data) {
    return request2({
      url: orderCenter + '/OrderCommand/UpdateOrderAddress',
      method: 'post',
      data
    })
  },
  //强制订单签收
  UpdateOrderSignStatus(data) {
    return request2({
      url: orderCenter + '/OrderCommand/UpdateOrderSignStatus',
      method: 'post',
      data
    })
  },
  //订单统计报表
  GetOrderStaticReport() {
    return request2({
      url: orderCenter + '/OrderQuery/GetOrderStaticReport',
      method: 'get'
    })
  },
  //订单报表详情( Week，Month，Year)
  GetOrderDetailStaticReport(params) {
    return request2({
      url: orderCenter + '/OrderQuery/GetOrderDetailStaticReport',
      method: 'get',
      params
    })
  },
  getPackageCardRecords(params) {
    return request2({
      url: orderCenter + '/OrderQuery/GetPackageCardRecords',
      method: 'get',
      params
    })
  },
  //外采报表
  GetOrderOutProductsProfitReport(data) {
    return request2({
      url: orderCenter + '/OrderQuery/GetOrderOutProductsProfitReport',
      method: 'post',
      data
    })
  },
  //产品明细
  GetOrderProductsReport(data) {
    return request2({
      url: orderCenter + '/OrderQuery/GetOrderProductsReport',
      method: 'post',
      data
    })
  },



}
export {
  orderCenterSvc
}
