import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_Product_API;
let domain2 = process.env.VUE_APP_BASE_LOGIN_API
let domain1 = process.env.VUE_APP_BASE_AUTH_API
let appSvc={
    //促销列表搜索
    searchPromotionActivity(params) {
        return request2({
          url: domain + '/Promotion/SearchPromotionActivity',
          method: 'get',
          params
        })
    },
    //根据Pid搜索门店商品
    getShopProductByCode(params){
        return request2({
            url:domain+'/Promotion/GetShopProductByCode',
            method:'get',
            params
        })
    },
    //新增促销
    addPromotionActivity(data){
        return request2({
            url:domain+'/Promotion/AddPromotionActivity',
            method:'post',
            data
        })
    },
    getPromotionDetail(params){
      return request2({
        url:domain+'/Promotion/GetPromotionDetail',
        method:'get',
        params
      })
    },
    finishPromotion(data){
      return request2({
        url:domain+'/Promotion/FinishPromotion',
        method:'post',
        data
    })
    },
    auditPromotionActivity(data){
      return request2({
        url:domain+'/Promotion/AuditPromotionActivity',
        method:'post',
        data
      })
    },
    GetCouponActivityListForShop(data){
      return request2({
        url:domain+'/Promotion/GetCouponActivityListForShop',
        method:'post',
        data
      })
    },
    SaveShopGrantCoupon(data){
      return request2({
        url:domain+'/Promotion/SaveShopGrantCoupon',
        method:'post',
        data
      })
    },
    GetShopPackageCardProductPageList(data){
      return request2({
        url:domain+'/ProductConfig/GetShopPackageCardProductPageList',
        method:'post',
        data
      })
    },
    SaveShopPackageCard(data){
      return request2({
        url:domain+'/ProductConfig/SaveShopPackageCard',
        method:'post',
        data
      })
    },
    GetShopCardDetail(data){
      return request2({
        url:domain+'/ProductConfig/GetShopCardDetail',
        method:'post',
        data
      })
    },
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
