import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_SHOP_API;

let shopAssetSvc = {
    upsertShopAsset(data) {
        return request2({
            url: domain + '/ShopSetting/UpsertShopAsset',
            method: 'post',
            data
        })
    },
    deleteShopAsset(data) {
        return request2({
            url: domain + '/ShopSetting/DeleteShopAsset',
            method: 'post',
            data
        })
    },
    getShopAssetList(params) {
        return request2({
            url: domain + '/ShopSetting/GetShopAssetList',
            method: 'get',
            params
        })
    },
    getShopAsset(params) {
        return request2({
            url: domain + '/ShopSetting/GetShopAsset',
            method: 'get',
            params
        })
    },
    shopAssetTypeOptions(params) {
        return request2({
            url: domain + '/ShopSetting/ShopAssetTypeOptions',
            method: 'get',
            params
        })
    },
    discardShopAsset(data) {
        return request2({
            url: domain + '/ShopSetting/DiscardShopAsset',
            method: 'post',
            data
        })
    },
    maintainShopAsset(data) {
        return request2({
            url: domain + '/ShopSetting/MaintainShopAsset',
            method: 'post',
            data
        })
    }
}

export { shopAssetSvc }
