import request2 from '@/utils/request2'
import comBusSvc from '@/api/accountauthority/commonbuslogical'

let domain = process.env.VUE_APP_BASE_ACCOUNTAUTHORITH_API;

let accountSvc = {
    getAccountPage(params) {
        return request2({
            url: domain + '/Account/GetAccountPage',
            method: 'get',
            params
        })
    },
    unlockAccountById(data) {
        return request2({
            url: domain + '/Account/UnlockAccountById',
            method: 'post',
            data
        })
    },
    lockAccountById(data) {
        return request2({
            url: domain + '/Account/LockAccountById',
            method: 'post',
            data
        })
    },
    resetAccountPasswordById(data) {
        return request2({
            url: domain + '/Account/ResetAccountPasswordById',
            method: 'post',
            data
        })
    },
    deleteBatchAccountById(data) {
        return request2({
            url: domain + '/Account/DeleteBatchAccountById',
            method: 'post',
            data
        })
    }

};

export { comBusSvc, accountSvc };
