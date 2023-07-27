import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_SHOP_API;
let appSvc = {
    searchPromotionActivity(params) {
        return request2({
            url: domain + '/Promotion/SearchPromotionActivity',
            method: 'get',
            params
        })
    },
    getPromotionActivityDetail(params) {
        return request2({
            url: domain + '/Promotion/GetPromotionActivityDetail',
            method: 'get',
            params
        })
    },
    getShopProductByCode(params) {
        return request2({
            url: domain + '/Promotion/GetShopProductByCode',
            method: 'get',
            params
        })
    },
    addPromotionActivity(data) {
        return request2({
            url: domain + '/Promotion/AddPromotionActivity',
            method: 'post',
            data
        })
    },
    finishPromotion(data) {
        return request2({
            url: domain + '/Promotion/FinishPromotion',
            method: 'post',
            data
        })
    },
    editPromotion(data) {
        return request2({
            url: domain + '/Promotion/EditPromotion',
            method: 'post',
            data
        })
    },
    searchProductByNameOrCode(params) {
        return request2({
            url: domain + '/Promotion/SearchProductByNameOrCode',
            method: 'get',
            params
        })
    },
    getGrouponProductPageList(params) {
        return request2({
            url: domain + '/Groupon/GetGrouponProductPageList',
            method: 'get',
            params
        })
    },
    getGrouponProductDetail(params) {
        return request2({
            url: domain + '/Groupon/GetGrouponProductDetail',
            method: 'get',
            params
        })
    },
    saveShopGrouponProduct(data) {
        return request2({
            url: domain + '/Groupon/SaveShopGrouponProduct',
            method: 'post',
            data
        })
    }
};

export {
    appSvc
};

