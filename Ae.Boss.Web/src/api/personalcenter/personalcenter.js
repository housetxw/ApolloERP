import request2 from '@/utils/request2'

let domain = process.env.VUE_APP_BASE_ACCOUNTAUTHORITH_API;

let pcSvc = {

    updatePassword(data) {
        return request2({
            url: domain + '/Account/UpdatePassword',
            method: 'post',
            data
        })
    }

};

export { pcSvc };
