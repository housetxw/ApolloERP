import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_Product_API
let unitSearchSvc = {
  getUnits(params) {
    return request2({
      url: domain + '/ProductConfig/GetUnitList',
      method: 'get',
      params
    })
  },
  GetProductUnitList(params) {
    return request2({
      url: domain + '/ProductConfig/GetProductUnitList',
      method: 'get',
      params
    })
  },
  AddUnit(data) {
    return request2({
      url: domain + '/ProductConfig/AddUnit',
      method: 'post',
      data
    })
  },
  EditUnit(data) {
    return request2({
      url: domain + '/ProductConfig/EditUnit',
      method: 'post',
      data
    })
  },
}
export {
  unitSearchSvc
}
