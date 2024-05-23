import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_Product_API;
let domain2 = process.env.VUE_APP_BASE_LOGIN_API
let domain1 = process.env.VUE_APP_BASE_AUTH_API
let fileUploaddomain = process.env.VUE_APP_BASE_FileUpload_API;
let appSvc = {
  //获取车型品牌
  getBrandList(params) {
    return request2({
      url: domain + '/Vehicle/GetVehicleBrandList',
      method: 'get',
      params
    })
  },
  //获取品牌车系
  getListByBrand(params) {
    return request2({
      url: domain + '/Vehicle/GetVehicleListByBrand',
      method: 'get',
      params
    })
  },
  //获取排量
  getPaiLiangByVehicleId(params) {
    return request2({
      url: domain + '/Vehicle/GetPaiLiangByVehicleId',
      method: 'get',
      params
    })
  },
  //获取生产年份
  getVehicleNianByPaiLiang(params) {
    return request2({
      url: domain + '/Vehicle/GetVehicleNianByPaiLiang',
      method: 'get',
      params
    })
  },
  //获取款型
  getVehicleSalesNameByNian(params) {
    return request2({
      url: domain + '/Vehicle/GetVehicleSalesNameByNian',
      method: 'get',
      params
    })
  },
  //查询车型已绑定套餐
  getBaoYangPackageRef(params) {
    return request2({
      url: domain + '/BaoYangConfig/GetBaoYangPackageRef',
      method: 'get',
      params
    })
  },
  //获取套餐baoYangType
  getPackageByType(params) {
    return request2({
      url: domain + '/BaoYangConfig/GetPackageByType',
      method: 'get',
      params
    })
  },
  //根据Tid适配套餐（剔除 已关联的 套餐）
  getBaoYangPackageByTid(params) {
    return request2({
      url: domain + '/BaoYangConfig/GetBaoYangPackageByTid',
      method: 'get',
      params
    })
  },
  addBaoYangPackage(data) {
    return request2({
      url: domain + '/BaoYangConfig/RelateVehicleAndPackage',
      method: 'post',
      data
    })
  },
  deleteBaoYangPackageRef(data) {
    return request2({
      url: domain + '/BaoYangConfig/DeleteBaoYangPackageRef',
      method: 'post',
      data
    })
  },
  getPartAccessoryConfig(params) {
    return request2({
      url: domain + '/BaoYangConfig/GetPartAccessoryConfig',
      method: 'get',
      params
    })
  },
  getBaoYangParts() {
    return request2({
      url: domain + '/BaoYangConfig/GetBaoYangParts',
      method: 'get'
    })
  },
  getBaoYangPropertyAdaptation(params) {
    return request2({
      url: domain + '/BaoYangConfig/GetBaoYangPropertyAdaptation',
      method: 'get',
      params
    })
  },
  getBaoYangPartAccessory(params) {
    return request2({
      url: domain + '/BaoYangConfig/GetBaoYangPartAccessory',
      method: 'get',
      params
    })
  },
  getPartNameList() {
    return request2({
      url: domain + '/BaoYangConfig/GetPartNameList',
      method: 'get'
    })
  },
  getBaoYangPartAdaptations(params) {
    return request2({
      url: domain + '/BaoYangConfig/GetBaoYangPartAdaptations',
      method: 'get',
      params
    })
  },
  updateAccessory(data) {
    return request2({
      url: domain + '/BaoYangConfig/UpdateAccessory',
      method: 'post',
      data
    })
  },
  batchEditAccessory(data) {
    return request2({
      url: domain + '/BaoYangConfig/BatchEditAccessory',
      method: 'post',
      data
    })
  },
  deleteAccessory(data) {
    return request2({
      url: domain + '/BaoYangConfig/DeleteAccessory',
      method: 'post',
      data
    })
  },
  updatePartCode(data) {
    return request2({
      url: domain + "/BaoYangConfig/UpdatePartCode",
      method: 'post',
      data
    })
  },
  deletePartCode(data) {
    return request2({
      url: domain + "/BaoYangConfig/DeletePartCode",
      method: 'post',
      data
    })
  },
  insertPartCode(data) {
    return request2({
      url: domain + "/BaoYangConfig/AddPartCode",
      method: 'post',
      data
    })
  },
  updateOePartCode(data) {
    return request2({
      url: domain + '/BaoYangConfig/UpdateOePartCode',
      method: 'post',
      data
    })
  },
  insertSpecialPartCode(data) {
    return request2({
      url: domain + '/BaoYangConfig/AddSpecialPartCode',
      method: 'post',
      data
    })
  },
  getPartCodeAndBrandByOe(params) {
    return request2({
      url: domain + '/BaoYangConfig/GetPartCodeAndBrandByOe',
      method: 'get',
      params
    })
  },
  addBaoYangPartCommon(data) {
    return request2({
      url: domain + '/BaoYangConfig/BatchAddAdaptation',
      method: 'post',
      data
    })
  },
  getVehicleInfoList(params) {
    return request2({
      url: domain + '/Vehicle/GetVehicleInfoList',
      method: 'get',
      params
    })
  },
  getTidList(params) {
    return request2({
      url: domain + '/Vehicle/GetTidsByVehicle',
      method: 'get',
      params
    })
  },
  getOeCodeMapList(params) {
    return request2({
      url: domain + '/BaoYangConfig/GetOeCodeMapList',
      method: 'get',
      params
    })
  },
  getOeCodeMapDetailByOeCode(params) {
    return request2({
      url: domain + '/BaoYangConfig/GetOeCodeMapDetailByOeCode',
      method: 'get',
      params
    })
  },
  deleteOePartCode(data) {
    return request2({
      url: domain + '/BaoYangConfig/DeleteOePartCode',
      method: 'post',
      data
    })
  },
  editOePartCode(data) {
    return request2({
      url: domain + '/BaoYangConfig/EditOePartCode',
      method: 'post',
      data
    })
  },
  addOePartCode(data) {
    return request2({
      url: domain + '/BaoYangConfig/AddOePartCode',
      method: 'post',
      data
    })
  },
  getBaoYangPropertyDescription(params) {
    return request2({
      url: domain + '/BaoYangConfig/GetBaoYangPropertyDescription',
      method: 'get',
      params
    })
  },
  deletePropertyAdaptation(data) {
    return request2({
      url: domain + '/BaoYangConfig/DeletePropertyAdaptation',
      method: 'post',
      data
    })
  },
  savePropertyAdaptation(data) {
    return request2({
      url: domain + '/BaoYangConfig/SavePropertyAdaptation',
      method: 'post',
      data
    })
  },
  formatDate(date, fmt) {
    if (/(y+)/.test(fmt)) {
      fmt = fmt.replace(RegExp.$1, (date.getFullYear() + '').substr(4 - RegExp.$1.length));
    }
    let o = {
      'M+': date.getMonth() + 1,
      'd+': date.getDate(),
      'h+': date.getHours(),
      'm+': date.getMinutes(),
      's+': date.getSeconds()
    };
    for (let k in o) {
      if (new RegExp(`(${k})`).test(fmt)) {
        let str = o[k] + '';
        fmt = fmt.replace(RegExp.$1, (RegExp.$1.length === 1) ? str : ('00' + str).substr(str.length));
      }
    }
    return fmt;
  },
  getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  },
  getQiNiuToken(params) {
    return request2({
      url: fileUploaddomain + "/QiNiu/GetQiNiuToken",
      method: "get",
      params
    });
  },
  getBaoYangProductRef(params) {
    return request2({
      url: domain + '/BaoYangConfig/GetBaoYangProductRef',
      method: 'get',
      params
    })
  },
  deleteBaoYangProductRef(data) {
    return request2({
      url: domain + '/BaoYangConfig/DeleteBaoYangProductRef',
      method: 'post',
      data
    })
  },
  checkPartCode(params) {
    return request2({
      url: domain + '/BaoYangConfig/CheckPartCode',
      method: 'get',
      params
    })
  },
  checkProduct(params) {
    return request2({
      url: domain + '/BaoYangConfig/CheckProduct',
      method: 'get',
      params
    })
  },
  checkPartCodeAndProduct(params) {
    return request2({
      url: domain + '/BaoYangConfig/CheckPartCodeAndProduct',
      method: 'get',
      params
    })
  },
  insertPartsAssociation(data) {
    return request2({
      url: domain + '/BaoYangConfig/InsertPartsAssociation',
      method: 'post',
      data
    })
  },
  searchPackageProductsWithCondition(data) {
    return request2({
      url: domain + '/baoyang/SearchPackageProductsWithCondition',
      method: 'post',
      data
    })
  },
  getBaoYangPackages(data) {
    return request2({
      url: domain + '/baoyang/GetBaoYangPackages',
      method: 'post',
      data
    })
  },
  getUserCoupons(data) {
    return request2({
      url: domain + '/Coupon/GetUserCouponPageByUserId',
      method: 'post',
      data
    })
  },
  getPackageTypeConfig(params){
    return request2({
      url: domain + '/BaoYangConfig/GetPackageTypeConfig',
      method: 'get',
      params
    })
  },
  editPackageTypeConfig(data){
    return request2({
      url: domain + '/BaoYangConfig/EditPackageTypeConfig',
      method: 'post',
      data
    })
  },
  addPackageTypeConfig(data){
    return request2({
      url: domain + '/BaoYangConfig/AddPackageTypeConfig',
      method: 'post',
      data
    })
  },
  getBaoYangTypeConfig(params){
    return request2({
      url: domain + '/BaoYangConfig/GetBaoYangTypeConfig',
      method: 'get',
      params
    })
  },
  editBaoYangTypeConfig(data){
    return request2({
      url: domain + '/BaoYangConfig/EditBaoYangTypeConfig',
      method: 'post',
      data
    })
  },
  addBaoYangTypeConfig(data){
    return request2({
      url: domain + '/BaoYangConfig/AddBaoYangTypeConfig',
      method: 'post',
      data
    })
  },
  getValidBaoYangTypeConfig(params){
    return request2({
      url: domain + '/BaoYangConfig/GetValidBaoYangTypeConfig',
      method: 'get',
      params
    })
  },
  getBaoYangCategoryConfig(params){
    return request2({
      url: domain + '/BaoYangConfig/GetBaoYangCategoryConfig',
      method: 'get',
      params
    })
  },
  getInstallAddFeeConfig(params){
    return request2({
      url: domain + '/BaoYangConfig/GetInstallAddFeeConfig',
      method: 'get',
      params
    })
  },
  addInstallAddFeeConfig(data){
    return request2({
      url: domain + '/BaoYangConfig/AddInstallAddFeeConfig',
      method: 'post',
      data
    })
  },
  editInstallAddFeeConfig(data){
    return request2({
      url: domain + '/BaoYangConfig/EditInstallAddFeeConfig',
      method: 'post',
      data
    })
  },
  getBaoYangInstallService(params){
    return request2({
      url: domain + '/BaoYangConfig/GetBaoYangInstallService',
      method: 'get',
      params
    })
  },
  getVehicleInstallAddFee(params){
    return request2({
      url: domain + '/BaoYangConfig/GetVehicleInstallAddFee',
      method: 'get',
      params
    })
  }
};

export {
  appSvc
};

export function LoginAsync(data) {
  return request2({
    url: `${domain2}/Authentication/LoginAsync`,
    method: 'post',
    data
  })
}

export function getInfo(token) {
  return request2({
    url: '/user/info',
    method: 'get',
    params: {
      token
    }
  })
}

export function logout() {
  return request2({
    url: '/user/logout',
    method: 'post'
  })
}

export function GetOrgPageListByAuthCodeAsync(data) {
  return request2({
    url: `${domain2}/Authentication/GetOrgPageListByAuthCodeAsync`,
    method: 'post',
    data
  })
}

export function GetTokenInfoByAuthCode(data) {
  return request2({
    url: `${domain2}/Authentication/GetTokenInfoByAuthCode`,
    method: 'post',
    data
  })
}

export function GetSMSCodeAsync(data) {
  return request2({
    url: `${domain2}/Authentication/GetSMSCodeAsync`,
    method: 'post',
    data
  })
}

export function LoginWithSMSAsync(data) {
  return request2({
    url: `${domain2}/Authentication/LoginWithSMSAsync`,
    method: 'post',
    data
  })
}
