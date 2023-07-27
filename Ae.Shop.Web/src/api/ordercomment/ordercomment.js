import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_ORDERCOMMENT_API;
let shopApiDomain = process.env.VUE_APP_BASE_SHOP_API;

let appSvc = {
  // 产品评论列表
  getOrderCommentForProductList(data) {
    return request2({
      url: domain + '/OrderComment/GetOrderCommentForProductList',
      method: 'post',
      data
    })
  },
  //技师评论列表
  getOrderCommentForTechList(data) {
    return request2({
      url: domain + '/OrderComment/GetOrderCommentForTechList',
      method: 'post',
      data
    })
  },
  //门店评论列表
  getOrderCommentForShopList(data) {
    return request2({
      url: domain + '/OrderComment/GetOrderCommentForShopList',
      method: 'post',
      data
    })
  },
  //门店评论列表（ShopApi）
  getOrderCommentForShopListForShopApi(data) {
    return request2({
      url: shopApiDomain + '/ShopOperation/GetOrderCommentForShopList',
      method: 'post',
      data
    })
  },
  //获取回评
  getOrderCommentForReplyList(data) {
    return request2({
      url: domain + '/OrderComment/GetOrderCommentForReplyList',
      method: 'post',
      data
    })
  },
  //获取评论图片
  GetCommentImages(params) {
    return request2({
      url: domain + '/OrderComment/GetCommentImages',
      method: 'get',
      params
    })
  },
  //客户初评审核
  checkOrderComment(data) {
    return request2({
      url: domain + '/OrderComment/CheckOrderComment',
      method: 'post',
      data
    })
  },
  //门店回评，客户追评，门店追评审核
  checkOrderReply(data) {
    return request2({
      url: domain + '/OrderComment/CheckOrderReply',
      method: 'post',
      data
    })
  },

  //获取基础信息
  getBaseInfos(params) {
    return request2({
      url: domain + '/OrderComment/GetBaseInfos',
      method: 'get',
      params
    })
  },
  //获取门店列表
  getShopListAsync(params) {
    return request2({
      url: domain + '/ShopManage/GetShopListAsync',
      method: 'get',
      params
    })
  },
  //获取评论列表
  getCommentList(params) {
    return request2({
      url: shopApiDomain + '/ShopOperation/GetCommentList',
      method: 'get',
      params
    })
  },
  //回复详情
  replyDetail(params) {
    return request2({
      url: shopApiDomain + '/ShopOperation/ReplyDetail',
      method: 'get',
      params
    })
  },
  //回复评价
  replyComment(data) {
    return request2({
      url: shopApiDomain + '/ShopOperation/ReplyComment',
      method: 'post',
      data
    })
  }
};

export {
  appSvc
};