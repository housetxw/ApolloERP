import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_SHOP_API;
let domain2 = process.env.VUE_APP_BASE_LOGIN_API
let domain1 = process.env.VUE_APP_BASE_ACCOUNTAUTHORITH_API 
let domain3 = process.env.VUE_APP_BASE_CARREPORT_API
let appSvc = {
  getUserList(params) {
    return request2({
      url: domain + '/ShopCustomer/GetShopCustomerList',
      method: 'get',
      params
    })
  },
  getUserInfo(params) {
    return request2({
      url: domain + '/ShopCustomer/GetShopCustomerDetail',
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

export function LoginWithSMS(data) {
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
export function GetEmpAuthorityListForShopWebByEmpId(params) {
  return request2({
    url: `${domain1}Authorization/GetEmpAuthorityListForShopWebByEmpId`,
    method: 'get',
    params
  })
}
export function GetUserVehicleFile(params) {
  return request2({
    url: `${domain3}/ReceiveCheck/GetUserVehicleFile`,
    method: 'get',
    params
  })
}
export function GetMaintenancerecord(params) {
  return request2({
    url: `${domain3}/ReceiveCheck/GetMaintenancerecord`,
    method: 'get',
    params
  })
}
export function GetCheckErrorDetail(params) {
  return request2({
    url: `${domain3}/ReceiveCheck/GetCheckErrorDetail`,
    method: 'get',
    params
  })
}
export function GetCheckReport(params) {
  return request2({
    url: `${domain3}/ReceiveCheck/GetCheckReport`,
    method: 'get',
    params
  })
}
