import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_SHOP_API;
let arrivalSvc={
    getArrivalList(params) {
        return request2({
          url: domain + '/Arrival/GetArrivalList',
          method: 'get',
          params
        })
    },
    getArrivalListCondition(params) {
      return request2({
        url: domain + '/Arrival/GetArrivalListCondition',
        method: 'get',
        params
      })
   },
   getArrivalDetail(params){
    return request2({
      url: domain + '/Arrival/GetArrivalDetail',
      method: 'get',
      params
    })
  }
};

export {
    arrivalSvc
  };

