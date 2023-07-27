import request2 from '@/utils/request2'
import comBusSvc from '@/api/accountauthority/commonbuslogical'

let domain = process.env.VUE_APP_BASE_ACCOUNTAUTHORITH_API;
let appSvc = {
  getApplicationSelectList(params) {
    return request2({
      url: domain + '/Application/GetApplicationSelectList',
      method: 'get',
      params
    })
  },
  getPagedApplicationList(params) {
    return request2({
      url: domain + '/Application/GetPagedApplicationList',
      method: 'get',
      params
    })
  },
  createOrUpdateApplication(data) {
    return request2({
      url: domain + '/Application/CreateOrUpdateApplication',
      method: 'post',
      data
    })
  },
  deleteApplicationById(data) {
    return request2({
      url: domain + '/Application/DeleteApplicationById',
      method: 'post',
      data
    })
  }
};

export { comBusSvc, appSvc };
