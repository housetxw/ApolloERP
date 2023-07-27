import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_LOGIN_API
let appSvc = {
  GetOrgPageListByTokenAsync(data) {
    return request2({
      url: domain + '/Authentication/GetOrgPageListByTokenAsync',
      method: 'post',
      data
    })
  },
  GetTokenInfoByToken(data) {
    return request2({
      url: domain + '/Authentication/GetTokenInfoByToken',
      method: 'post',
      data
    })
  },
}

export {
  appSvc
} 