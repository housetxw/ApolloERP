import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_Product_API;
let attributesearchSvc = {
  searchAttribute(params) {
    return request2({
      url: domain + '/ProductConfig/SearchAttribute',
      method: 'get',
      params
    })
  },
  getAttributeNameById(params) {
    return request2({
      url: domain + '/ProductConfig/GetAttributeNameById',
      method: 'get',
      params
    })
  },
  saveAttribute(data) {
    return request2({
      url: domain + '/ProductConfig/SaveAttribute',
      method: 'post',
      data
    })
  }
}

export {
  attributesearchSvc
}
