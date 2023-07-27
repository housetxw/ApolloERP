import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_AFTERSALE_API;
let appTypeSvc = {
    getTousuTypes(params) {
    return request2({
      url: domain + '/TousuType/GetTousuTypes',
      method: 'get',
      params
    })
  }
};

export {
    appTypeSvc
};