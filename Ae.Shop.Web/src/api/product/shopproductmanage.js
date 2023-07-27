import request2 from '@/utils/request2'
import upfileRequest from '@/utils/upfileRequest'

let domain = process.env.VUE_APP_BASE_Product_API;
let shopproductmanageSvc = {
  getShopList(params) {
    return request2({
      url: domain + '/ShopProduct/GetShopList',
      method: 'get',
      params
    })
  },
  getShopServiceTgype(params) {
    return request2({
      url: domain + '/ShopProduct/GetShopServiceType',
      method: 'get',
      params
    })
  },
  searchShopProduct(params) {
    return request2({
      url: domain + '/ShopProduct/SearchShopProduct',
      method: 'get',
      params
    })
  },
  saveShopProduct(data) {
    return request2({
      url: domain + '/ShopProduct/SaveShopProduct',
      method: 'post',
      data
    })
  },
  getShopProductByCode(params) {
    return request2({
      url: domain + '/ShopProduct/GetShopProductByCode',
      method: 'get',
      params
    })
  },
  importShopProduct(data) {
    return upfileRequest({
      url: domain + '/ShopProduct/ImportShopProduct',
      method: 'post',
      data
    })
  },
  auditShopProduct(data) {
    return request2({
      url: domain + '/ShopProduct/AuditShopProduct',
      method: 'post',
      data
    })
  },
}
export {
  shopproductmanageSvc
}