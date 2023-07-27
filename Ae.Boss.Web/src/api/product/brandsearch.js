import request2 from '@/utils/request2'

const domain = process.env.VUE_APP_BASE_Product_API
const brandSearchSvc = {
  getBrandList(params) {
    return request2({
      url: domain + '/ProductConfig/GetBrandList',
      method: 'get',
      params
    })
  },
  getProductBrandList(params) {
    return request2({
      url: domain + '/ProductConfig/GetProductBrandList',
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
  AddBrand(data) {
    return request2({
      url: domain + '/ProductConfig/AddBrand',
      method: 'post',
      data
    })
  },
  EditBrand(data) {
    return request2({
      url: domain + '/ProductConfig/EditBrand',
      method: 'post',
      data
    })
  }
}
export {
  brandSearchSvc
}
