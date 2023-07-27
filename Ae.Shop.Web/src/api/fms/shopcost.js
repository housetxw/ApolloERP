import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_SHOP_API;
let shopcostSvc={
   getShopCostList(params) {
        return request2({
          url: domain + '/ShopCost/GetShopCostList',
          method: 'get',
          params
        })
    },
    getShopCostTypeListCondition(params) {
      return request2({
        url: domain + '/ShopCost/GetShopCostTypeListCondition',
        method: 'get',
        params
      })
   },
   getShopCostDetail(params){
    return request2({
      url: domain + '/ShopCost/GetShopCostDetail',
      method: 'get',
      params
    })
  },
  addShopCost(data) {
  
    return request2({
      url: domain + '/ShopCost/AddShopCost',
      method: 'post',
      data
    })
  },
  updateShopCost(data) {
    return request2({
      url: domain + '/ShopCost/UpdateShopCost',
      method: 'post',
      data
    })
  },
  deleteShopCost(data) {
    debugger;
    return request2({
      url: domain + '/ShopCost/DeleteShopCost',
      method: 'post',
      data
    })
  }
};

export {
  shopcostSvc
  };

