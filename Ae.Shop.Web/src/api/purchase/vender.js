import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_Purchase_API
let shopApiDomain = process.env.VUE_APP_BASE_SHOP_API

let appVenderSvc = {
  searchVender(params) {
    return request2({
      url: shopApiDomain + '/ShopPurchase/SearchVender',
      method: 'get',
      params
    })
  },
  getVender(params) {
    return request2({
      url: shopApiDomain + '/ShopPurchase/GetVender',
      method: 'get',
      params
    })
  },
  upsertVender(data) {
    return request2({
      url: shopApiDomain + '/ShopPurchase/UpsertVender',
      method: 'post',
      data
    })
  },
  activeVender(data) {
    return request2({
      url: shopApiDomain + '/ShopPurchase/ActiveVender',
      method: 'post',
      data
    })
  },
  getVenderDetail(params) {
    return request2({
      url: domain + '/Vender/GetVenderDetail',
      method: 'get',
      params
    })
  },
  getVenderList(params) {
    return request2({
      url: domain + '/Vender/GetVenderList',
      method: 'get',
      params
    })
  },
  getRegionChinaListByRegionId(params) {
    return request2({
      url: domain + '/PurchaseOrder/GetRegionChinaListByRegionIdDTOs',
      method: 'get',
      params
    })
  },
  getBasicInfoList(params) {
    return request2({
      url: domain + '/Vender/GetBasicInfoList',
      method: 'get',
      params
    })
  },
  editVender(data) {
    return request2({
      url: domain + '/Vender/EditVender',
      method: 'post',
      data
    })
  },
  deleteVender(data) {
    return request2({
      url: domain + '/Vender/DeleteVender',
      method: 'post',
      data
    })
  },
  createVender(data) {
    return request2({
      url: domain + '/Vender/CreateVender',
      method: 'post',
      data
    })
  },
}

export {
  appVenderSvc
} 