import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_SHOP_API;
let domain2 = process.env.VUE_APP_BASE_LOGIN_API
let domain1 = process.env.VUE_APP_BASE_ACCOUNTAUTHORITH_API 
let appSvc={
  getReserveList(params) {
      return request2({
        url: domain + '/Reserve/GetReserveListPage',
        method: 'get',
        params
      })
  },
  getReserveListCondition(params) {
    return request2({
      url: domain + '/Reserve/GetReserveListCondition',
      method: 'get',
      params
    })
  },
  getReserveDetailForWeb(params){
    return request2({
      url: domain + '/Reserve/GetReserveDetailForWeb',
      method: 'get',
      params
    })
  },
  getReserveDateForWeb(params){
    return request2({
      url: domain + '/Reserve/GetReserveDateForWeb',
      method: 'get',
      params
    })
  },
  getAllUserVehicles(params){
    return request2({
      url: domain + '/Reserve/GetAllUserVehicles',
      method: 'get',
      params
    })
  },
  getVehicleBrandList(params){
    return request2({
      url: domain + '/Vehicle/GetVehicleBrandListAsync',
      method: 'get',
      params
    })
  },
  getVehicleListByBrand(params){
    return request2({
      url: domain + '/Vehicle/GetVehicleListByBrandAsync',
      method: 'get',
      params
    })
  },
  getPaiLiangByVehicleId(params){
    return request2({
      url: domain + '/Vehicle/GetPaiLiangByVehicleIdAsync',
      method: 'get',
      params
    })
  },
  getVehicleNianByPaiLiang(params){
    return request2({
      url: domain + '/Vehicle/GetVehicleNianByPaiLiangAsync',
      method: 'get',
      params
    })
  },
  getVehicleSalesNameByNian(params){
    return request2({
      url: domain + '/Vehicle/GetVehicleSalesNameByNianAsync',
      method: 'get',
      params
    })
  },
  cancelReserve(data){
    return request2({
      url:domain+'/Reserve/CancelReserve',
      method: 'post',
      data
    })
  },
  editReserve(data){
    return request2({
      url:domain+'/Reserve/EditReserve',
      method: 'post',
      data
    })
  },
  getUserDetailByUserTel(params){
    return request2({
      url: domain + '/Reserve/GetUserDetailByUserTel',
      method: 'get',
      params
    })
  },
  getValidReserve(params){
    return request2({
      url: domain + '/Reserve/GetValidReserve',
      method: 'get',
      params
    })
  },
  addReserve(data){
    return request2({
      url:domain+'/Reserve/AddReserve',
      method: 'post',
      data
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
  
  export function LoginWithSMS(data) {
    return request2({
      url: `${domain2}/Authentication/LoginWithSMSAsync`,
      method: 'post',
      data
    })
  }
  
  export function GetEmpAuthorityListForWebByEmpIdAsync(params) {
    return request2({
      url: `${domain1}/Authorization/GetEmpAuthorityListForWebByEmpIdAsync`,
      method: 'get',
      params
    })
  }