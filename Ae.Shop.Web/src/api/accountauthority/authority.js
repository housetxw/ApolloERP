import request2 from '@/utils/request2'
import comBusSvc from '@/api/accountauthority/commonbuslogical'

let domain = process.env.VUE_APP_BASE_ACCOUNTAUTHORITH_API;
let authoritySvc = {
  getPagedAuthorityList(params) {
    return request2({
      url: domain + '/Authority/GetPagedAuthorityList',
      method: 'get',
      params
    })
  },
  getAllAuthoritySelectList(params) {
    return request2({
      url: domain + '/Authority/GetAllAuthoritySelectList',
      method: 'get',
      params
    })
  },
  getAuthorityListByApplicationIdAsync(params) {
    return request2({
      url: domain + '/Authority/GetAuthorityListByApplicationIdAsync',
      method: 'get',
      params
    })
  },
  getAuthorityListByApplicationIdExceptIdAsync(params) {
    return request2({
      url: domain + '/Authority/GetAuthorityListByApplicationIdExceptIdAsync',
      method: 'get',
      params
    })
  },
  createOrUpdateAuthority(data) {
    return request2({
      url: domain + '/Authority/CreateOrUpdateAuthority',
      method: 'post',
      data
    })
  },
  deleteAuthorityById(data) {
    return request2({
      url: domain + '/Authority/DeleteAuthorityById',
      method: 'post',
      data
    })
  }
};

export { comBusSvc, authoritySvc };
