import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_Product_API;
let wmsdomain = process.env.VUE_APP_BASE_WMS_API;
let couponSvc = {
  //优惠券配置查询
  getCouponRuleList(params) {
    return request2({
      url: domain + '/Coupon/GetCouponRuleList',
      method: 'get',
      params
    })
  },
  getCouponTypeEnum(params){
    return request2({
        url: domain + '/Coupon/GetCouponTypeEnum',
        method: 'get',
        params
      })
  },
  addCouponRule(data){
    return request2({
        url: domain + '/Coupon/AddCouponRule',
        method: 'post',
        data
      })
  },
  getShopList(params) {
    return request2({
      url: wmsdomain + '/ShopManager/GetShopList',
      method: 'get',
      params
    })
  },
  getCouponCategory(params){
    return request2({
      url: domain + '/Coupon/GetCouponCategory',
      method: 'get',
      params
    })
  },
  getCouponType(params){
    return request2({
      url: domain + '/Coupon/GetCouponType',
      method: 'get',
      params
    })
  },
  getCouponRangeType(params){
    return request2({
      url: domain + '/Coupon/GetCouponRangeType',
      method: 'get',
      params
    })
  },
  getPayMethod(params){
    return request2({
      url: domain + '/Coupon/GetPayMethod',
      method: 'get',
      params
    })
  },
  getValidStartType(params){
    return request2({
      url: domain + '/Coupon/GetValidStartType',
      method: 'get',
      params
    })
  },
  getValidEndType(params){
    return request2({
      url: domain + '/Coupon/GetValidEndType',
      method: 'get',
      params
    })
  },
  getShopTypeEnum(params){
    return request2({
      url: domain + '/Coupon/GetShopTypeEnum',
      method: 'get',
      params
    })
  },
  getCategoryByParentId(params){
    return request2({
      url: domain + '/ProductConfig/GetCategoryByParentId',
      method: 'get',
      params
    })
  },
  getAllProductBrandList(params){
    return request2({
      url: domain + '/ProductConfig/GetAllProductBrandList',
      method: 'get',
      params
    })
  },
  getCouponActivityList(params){
    return request2({
      url: domain + '/Coupon/GetCouponActivityList',
      method: 'get',
      params
    })
  },
  addCouponActivity(data){
    return request2({
      url: domain + '/Coupon/AddCouponActivity',
      method: 'post',
      data
    })
  },
  getCouponActivityChannel(params){
    return request2({
      url: domain + '/Coupon/GetCouponActivityChannel',
      method: 'get',
      params
    })
  },
  getCouponActivityIntegralType(params){
    return request2({
      url: domain + '/Coupon/GetCouponActivityIntegralType',
      method: 'get',
      params
    })
  },
  updateCouponActivityStatus(data){
    return request2({
      url: domain + '/Coupon/UpdateCouponActivityStatus',
      method: 'post',
      data
    })
  },
  getCouponActivityDetail(params){
    return request2({
      url: domain + '/Coupon/GetCouponActivityDetail',
      method: 'get',
      params
    })
  },
  editCouponActivity(data){
    return request2({
      url: domain + '/Coupon/EditCouponActivity',
      method: 'post',
      data
    })
  },
  getUserCouponGrantRecord(params){
    return request2({
      url: domain + '/Coupon/GetUserCouponGrantRecord',
      method: 'get',
      params
    })
  },
  searchUserInfo(params){
    return request2({
      url: domain + '/Coupon/SearchUserInfo',
      method: 'get',
      params
    })
  },
  grantUserCoupon(data){
    return request2({
      url: domain + '/Coupon/GrantUserCoupon',
      method: 'post',
      data
    })
  },
  invalidatedUserCoupon(data){
    return request2({
      url: domain + '/Coupon/InvalidatedUserCoupon',
      method: 'post',
      data
    })
  },
  delayUserCoupon(data){
    return request2({
      url: domain + '/Coupon/DelayUserCoupon',
      method: 'post',
      data
    })
  },
  updateActivityTotalNum(data){
    return request2({
      url: domain + '/Coupon/UpdateActivityTotalNum',
      method: 'post',
      data
    })
  },
  searchDecapAward(params){
    return request2({
      url: domain + '/Coupon/SearchDecapAward',
      method: 'get',
      params
    })
  },
  getGiftCouponRulePageList(params){
    return request2({
      url: domain + '/Coupon/GetGiftCouponRulePageList',
      method: 'get',
      params
    })
  },
  getGiftCouponRuleDetail(params){
    return request2({
      url: domain + '/Coupon/GetGiftCouponRuleDetail',
      method: 'get',
      params
    })
  },
  addGiftCouponRule(data){
    return request2({
      url: domain + '/Coupon/AddGiftCouponRule',
      method: 'post',
      data
    })
  },
  editGiftCouponRule(data){
    return request2({
      url: domain + '/Coupon/EditGiftCouponRule',
      method: 'post',
      data
    })
  }
};

export {
  couponSvc
  };
