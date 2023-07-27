import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_ACCOUNTAUTHORITH_API;
let shopManageDomain = process.env.VUE_APP_BASE_SHOPMANAGE_API;

let comBusSvc = {

    getUnitsForSelByType(params) {
        return request2({
            url: shopManageDomain + '/CommonBizLogical/GetUnitsForSelByType',
            method: 'get',
            params
        })
    },

    getAllOrganizationSelectList(params) {
        return request2({
            url: domain + '/CommonBusLogical/GetAllOrganizationSelectListAsync',
            method: 'get',
            params
        })
    },

    getAllOrganizationExceptShopSelectListAsync(params) {
        return request2({
            url: domain + '/CommonBusLogical/GetAllOrganizationExceptShopSelectListAsync',
            method: 'get',
            params
        })
    }

};

export default comBusSvc
