import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_Product_API;
let categoryAttributeSearchSvc = {
  getAttributesByCategoryId(params) {
    return request2({
      url: domain + '/ProductConfig/GetAttributesByCategoryId',
      method: 'get',
      params
    })
  },
  getAttributeNames(params) {
    return request2({
      url: domain + '/ProductConfig/GetAttributeNames',
      method: 'get',
      params
    })
  },
  selectCategoryAttribute(params) {
    return request2({
      url: domain + '/ProductConfig/SelectCategoryAttribute',
      method: 'get',
      params
    })
  },
  saveCategoryAttribute(data) {
    return request2({
      url: domain + '/ProductConfig/SaveCategoryAttribute',
      method: 'post',
      data
    })
  }
}
export {
  categoryAttributeSearchSvc
}
