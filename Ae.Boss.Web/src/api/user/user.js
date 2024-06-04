import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_USER_API;
let domain2 = process.env.VUE_APP_BASE_LOGIN_API
let domain1 = process.env.VUE_APP_BASE_AUTH_API
let appSvc = {
  getUserList(params) {
    return request2({
      url: domain + '/User/GetUserList',
      method: 'get',
      params
    })
  },
  getUserInfo(params) {
    return request2({
      url: domain + '/User/GetUserInfo',
      method: 'get',
      params
    })
  },
  searchUserInfo(params) {
    return request2({
      url: domain + '/User/SearchUserInfo',
      method: 'get',
      params
    })
  },
  getUserInfoByUserTel(params) {
    return request2({
      url: domain + '/User/GetUserInfoByUserTel',
      method: 'get',
      params
    })
  },
  createUser(data) {
    return request2({
      url: domain + '/User/CreateUser',
      method: 'post',
      data
    })
  },
  saveUser(data) {
    return request2({
      url: domain + '/User/saveUser',
      method: 'post',
      data
    })
  },
  addUserCar(data) {
    return request2({
      url: domain + '/User/AddUserCar',
      method: 'post',
      data
    })
  },
  editUserCar(data) {
    return request2({
      url: domain + '/User/EditUserCar',
      method: 'post',
      data
    })
  },
  deleteUserCar(data) {
    return request2({
      url: domain + '/User/DeleteUserCar',
      method: 'post',
      data
    })
  },
  addUserAddress(data) {
    return request2({
      url: domain + '/User/AddUserAddress',
      method: 'post',
      data
    })
  },
  editUserAddress(data) {
    return request2({
      url: domain + '/User/EditUserAddress',
      method: 'post',
      data
    })
  },
  deleteUserAddress(data) {
    return request2({
      url: domain + '/User/DeleteUserAddress',
      method: 'post',
      data
    })
  },
  getReserveDateForWeb(params){
    return request2({
      url: domain + '/Reserve/GetReserveDateForWeb',
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