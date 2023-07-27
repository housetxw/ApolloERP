import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_Product_API;
let categorySearchSvc = {
  getCategorysById(params) {
    return request2({
      url: domain + '/ProductConfig/GetCategorysById',
      method: 'get',
      params
    })
  }
}

export {
  categorySearchSvc
}
