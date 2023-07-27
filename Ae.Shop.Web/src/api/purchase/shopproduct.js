import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_SHOP_API;
let appSvc={
    getShopProductList(params) {
        return request2({
          url: domain + '/ShopProduct/GetShopProductList',
          method: 'get',
          params
        })
    },
    getShopServiceType(params){
        return request2({
            url: domain + '/ShopProduct/GetShopServiceType',
            method: 'get',
            params
        })
    },
    getShopProductUnit(params){
        return request2({
            url: domain + '/ShopProduct/GetShopProductUnit',
            method: 'get',
            params
        })
    },
    getShopProductDetail(params){
        return request2({
            url: domain + '/ShopProduct/GetShopProductDetail',
            method: 'get',
            params
        })
    },
    deleteShopProduct(data){
        return request2({
            url:domain+'/ShopProduct/DeleteShopProduct',
            method: 'post',
            data
        })
    },
    saveShopProduct(data){
        return request2({
            url:domain+'/ShopProduct/SaveShopProduct',
            method: 'post',
            data
        })
    },
    //外采产品类目2
    getShopProductCategory(params){
        return request2({
            url: domain + '/ShopProduct/GetShopProductCategory',
            method: 'get',
            params
        })
    }
};

export {
    appSvc
};

