import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_Product_API;
let domain2 = process.env.VUE_APP_BASE_LOGIN_API
let domain1 = process.env.VUE_APP_BASE_ACCOUNTAUTHORITH_API 
let appSvc={
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
    getPaiLiangByVehicleId(params){
        return request2({
            url:domain+'/Vehicle/GetPaiLiangByVehicleId',
            method:'get',
            params
        })
    },
    //获取生产年份
    getVehicleNianByPaiLiang(params){
        return request2({
            url:domain+'/Vehicle/GetVehicleNianByPaiLiang',
            method:'get',
            params
        })
    },
    //获取款型
    getVehicleSalesNameByNian(params){
        return request2({
            url:domain+'/Vehicle/GetVehicleSalesNameByNian',
            method:'get',
            params
        })
    },
    //查询车型已绑定套餐
    getBaoYangPackageRef(params){
        return request2({
            url:domain+'/BaoYangConfig/GetBaoYangPackageRef',
            method:'get',
            params
        })
    },
    //获取套餐baoYangType
    getPackageByType(params){
      return request2({
        url:domain+'/BaoYangConfig/GetPackageByType',
        method:'get',
        params
      })
    },
    //根据Tid适配套餐（剔除 已关联的 套餐）
    getBaoYangPackageByTid(params){
      return request2({
        url:domain+'/BaoYangConfig/GetBaoYangPackageByTid',
        method:'get',
        params
      })
    },
    addBaoYangPackage(data){
      return request2({
        url:domain+'/BaoYangConfig/RelateVehicleAndPackage',
        method: 'post',
        data
      })
    },
    deleteBaoYangPackageRef(data){
      return request2({
        url:domain+'/BaoYangConfig/DeleteBaoYangPackageRef',
        method: 'post',
        data
      })
    },
    getPartAccessoryConfig(params){
      return request2({
        url:domain+'/BaoYangConfig/GetPartAccessoryConfig',
        method:'get',
        params
      })
    },
    getBaoYangParts(){
      return request2({
        url:domain+'/BaoYangConfig/GetBaoYangParts',
        method:'get'
      })
    },
    getBaoYangPropertyAdaptation(params){
      return request2({
        url:domain+'/BaoYangConfig/GetBaoYangPropertyAdaptation',
        method:'get',
        params
      })
    },
    getBaoYangPartAccessory(params){
      return request2({
        url:domain+'/BaoYangConfig/GetBaoYangPartAccessory',
        method:'get',
        params
      })
    },
    getPartNameList(){
      return request2({
        url:domain+'/BaoYangConfig/GetPartNameList',
        method:'get'
      })
    },
    getBaoYangPartAdaptations(params){
      return request2({
        url:domain+'/BaoYangConfig/GetBaoYangPartAdaptations',
        method:'get',
        params
      })
    },
    updateAccessory(data){
      return request2({
        url:domain+'/BaoYangConfig/UpdateAccessory',
        method:'post',
        data
      })
    },
    batchEditAccessory(data){
      return request2({
        url:domain+'/BaoYangConfig/BatchEditAccessory',
        method:'post',
        data
      })
    },
    deleteAccessory(data){
      return request2({
        url:domain+'/BaoYangConfig/DeleteAccessory',
        method:'post',
        data
      })
    },
    updatePartCode(data){
      return request2({
        url:domain+"/BaoYangConfig/UpdatePartCode",
        method:'post',
        data
      })
    },
    deletePartCode(data){
      return request2({
        url:domain+"/BaoYangConfig/DeletePartCode",
        method:'post',
        data
      })
    },
    insertPartCode(data){
      return request2({
        url:domain+"/BaoYangConfig/AddPartCode",
        method:'post',
        data
      })
    },
    updateOePartCode(data){
      return request2({
        url:domain+'/BaoYangConfig/UpdateOePartCode',
        method:'post',
        data
      })
    },
    insertSpecialPartCode(data){
      return request2({
        url:domain+'/BaoYangConfig/AddSpecialPartCode',
        method:'post',
        data
      })
    },
    getPartCodeAndBrandByOe(params){
      return request2({
        url:domain+'/BaoYangConfig/GetPartCodeAndBrandByOe',
        method:'get',
        params
      })
    },
    addBaoYangPartCommon(data){
      return request2({
        url:domain+'/BaoYangConfig/BatchAddAdaptation',
        method:'post',
        data
      })
    },
    getVehicleInfoList(params){
      return request2({
        url:domain+'/Vehicle/GetVehicleInfoList',
        method:'get',
        params
      })
    },
    getTidList(params){
      return request2({
        url:domain+'/Vehicle/GetTidsByVehicle',
        method:'get',
        params
      })
    }
};

export {
    appSvc
  };

  export function LoginAsync(data) {
    return request2({
      url: `${domain2}Authentication/LoginAsync`,
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
      url: `${domain2}Authentication/GetOrgPageListByAuthCodeAsync`,
      method: 'post',
      data
    })
  }
  
  export function GetTokenInfoByAuthCode(data) {
    return request2({
      url: `${domain2}Authentication/GetTokenInfoByAuthCode`,
      method: 'post',
      data
    })
  }
  
  export function GetSMSCodeAsync(data) {
    return request2({
      url: `${domain2}Authentication/GetSMSCodeAsync`,
      method: 'post',
      data
    })
  }
  
  export function LoginWithSMSAsync(data) {
    return request2({
      url: `${domain2}Authentication/LoginWithSMSAsync`,
      method: 'post',
      data
    })
  }
  export function GetEmpAuthorityListForWebByEmpIdAsync(params) {
    return request2({
      url: `${domain1}Authorization/GetEmpAuthorityListForWebByEmpIdAsync`,
      method: 'get',
      params
    })
  }