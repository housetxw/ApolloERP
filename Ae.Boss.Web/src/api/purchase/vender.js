import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_Purchase_API
let appVenderSvc = {
    getVenders(params) {
    return request2({
      url: domain+'/Vender/GetVenders',
      method: 'get',
      params
    })
  },
  getVenderDetail(params) {
    return request2({
      url: domain+'/Vender/GetVenderDetail',
      method: 'get',
      params
    })
  },

  getVenderList(params) {
    return request2({
      url: domain+'/Vender/GetVenderList',
      method: 'get',
      params
    })
  },
  getRegionChinaListByRegionId(params) {
    return request2({
      url: domain+'/PurchaseOrder/GetRegionChinaListByRegionIdDTOs',
      method: 'get',
      params
    })
  },
  getBasicInfoList(params) {
    return request2({
      url: domain+'/Vender/GetBasicInfoList',
      method: 'get',
      params
    })
  },
  editVender(data) {
    return request2({
      url: domain+'/Vender/EditVender',
      method: 'post',
      data
    })
  },
  deleteVender(data) {
    return request2({
      url: domain+'/Vender/DeleteVender',
      method: 'post',
      data
    })
  },
  createVender(data) {
    return request2({
      url: domain+'/Vender/CreateVender',
      method: 'post',
      data
    })
  },
  getVenderProduct(params){
    return request2({
      url: domain+'/Vender/GetVenderProduct',
      method: 'get',
      params
    })
  },
  searchProductForVender(params){
    return request2({
      url: domain+'/Vender/SearchProductForVender',
      method: 'get',
      params
    })
  },
  addVenderProducts(data){
    return request2({
      url: domain+'/Vender/AddVenderProducts',
      method: 'post',
      data
    })
  },
  deleteVenderProduct(data){
    return request2({
      url: domain+'/Vender/DeleteVenderProduct',
      method: 'post',
      data
    })
  }
}

export {
  appVenderSvc
} 