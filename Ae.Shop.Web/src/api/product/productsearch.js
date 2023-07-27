import request2 from '@/utils/request2'
import upfileRequest from '@/utils/upfileRequest'

let domain = process.env.VUE_APP_BASE_Product_API;
let productSearchSvc = {
  searchProduct(params) {
    return request2({
      url: domain + '/ProductConfig/Search',
      method: 'get',
      params
    })
  },
  getProductByProductCode(params) {
    return request2({
      url: domain + '/ProductConfig/GetProductByProductCode',
      method: 'get',
      params
    })
  },
  getBaoYangParts(params) {
    return request2({
      url: domain + '/BaoYangConfig/GetBaoYangParts',
      method: 'get',
      params
    })
  },
  saveProduct(data) {
    return request2({
      url: domain + '/ProductConfig/SaveProduct',
      method: 'post',
      data
    })
  },
  importProductAttribute(data) {
    return upfileRequest({
      url: domain + '/ProductConfig/ImportProductAttribute',
      method: 'post',
      data
    })
  },
  importProduct(data) {
    return upfileRequest({
      url: domain + '/ProductConfig/ImportProduct',
      method: 'post',
      data
    })
  },
  //上门产品列表
  getDoorProductPageList(params){
    return request2({
      url: domain + '/ProductConfig/GetDoorProductPageList',
      method: 'get',
      params
    })
  },
  editDoorProduct(data){
    return request2({
      url: domain + '/ProductConfig/EditDoorProduct',
      method: 'post',
      data
    })
  },
  addDoorProducts(data){
    return request2({
      url: domain + '/ProductConfig/AddDoorProducts',
      method: 'post',
      data
    })
  },
  getProductInstallService(params){
    return request2({
      url: domain + '/ProductConfig/GetProductInstallService',
      method: 'get',
      params
    })
  },
  getProductInstallServiceDetail(params){
    return request2({
      url: domain + '/ProductConfig/GetProductInstallServiceDetail',
      method: 'get',
      params
    })
  },
  searchInstallService(params){
    return request2({
      url: domain + '/ProductConfig/SearchInstallService',
      method: 'get',
      params
    })
  },
  editInstallService(data){
    return request2({
      url: domain + '/ProductConfig/EditInstallService',
      method: 'post',
      data
    })
  },
  getHotProductPageList(params){
    return request2({
      url: domain + '/ProductConfig/GetHotProductPageList',
      method: 'get',
      params
    })
  },
  getTerminalTypeEnum(params){
    return request2({
      url: domain + '/ProductConfig/GetTerminalTypeEnum',
      method: 'get',
      params
    })
  },
  addHotProduct(data){
    return request2({
      url: domain + '/ProductConfig/AddHotProduct',
      method: 'post',
      data
    })
  },
  editHotProduct(data){
    return request2({
      url: domain + '/ProductConfig/EditHotProduct',
      method: 'post',
      data
    })
  },
  searchProductForHot(params){
    return request2({
      url: domain + '/ProductConfig/SearchProductForHot',
      method: 'get',
      params
    })
  },
  getPackageCardProductPageList(data){
    return request2({
      url: domain + '/ProductConfig/GetPackageCardProductPageList',
      method: 'post',
      data
    })
  },
  searchProductForPackageCard(params){
    return request2({
      url: domain + '/ProductConfig/SearchProductForPackageCard',
      method: 'get',
      params
    })
  },
  addPackageCardProduct(data){
    return request2({
      url: domain + '/ProductConfig/AddPackageCardProduct',
      method: 'post',
      data
    })
  },
  editPackageCardProduct(data){
    return request2({
      url: domain + '/ProductConfig/EditPackageCardProduct',
      method: 'post',
      data
    })
  },
  getPackageCardTypeEnum(params){
    return request2({
      url: domain + '/ProductConfig/GetPackageCardTypeEnum',
      method: 'get',
      params
    })
  },
  getGrouponProductPageList(params){
    return request2({
      url: domain + '/PageConfig/GetGrouponProductPageList',
      method: 'get',
      params
    })
  },
  searchProductForGroupon(params){
    return request2({
      url: domain + '/PageConfig/SearchProductForGroupon',
      method: 'get',
      params
    })
  },
  addGrouponProduct(data){
    return request2({
      url: domain + '/PageConfig/AddGrouponProduct',
      method: 'post',
      data
    })
  },
  editGrouponProduct(data){
    return request2({
      url: domain + '/PageConfig/EditGrouponProduct',
      method: 'post',
      data
    })
  }
}
export {
  productSearchSvc
}
