import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_SHOP_API;
let reportFormSvc={
GetShopSalesMonthList(params) {
        return request2({
          url: domain + '/ShopReport/GetShopSalesMonthList',
          method: 'get',
          params
        })
    },

    // 获取门店进店趋势数据
    GetArrivalTrendStatistics(params) {
      return request2({
        url: domain + '/Arrival/GetArrivalTrendStatistics',
        method: 'get',
        params
      })},
      GetEmployeePerformanceList(params) {
        return request2({
          url: domain + '/ShopReport/GetEmployeePerformanceList',
          method: 'get',
          params
        })
    },


};

export {
  reportFormSvc
  };
