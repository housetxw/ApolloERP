import request2 from '@/utils/request2'
import comBusSvc from '@/api/accountauthority/commonbuslogical'

let domain = process.env.VUE_APP_BASE_ACCOUNTAUTHORITH_API;
let roleSvc = {
    getPagedRoleList(params) {
        return request2({
            url: domain + '/Role/GetPagedRoleList',
            method: 'get',
            params
        })
    },
    getAllRoleSelectList(params) {
        return request2({
            url: domain + '/Role/GetAllRoleSelectList',
            method: 'get',
            params
        })
    },
    getAllOrganizationSelectList(params) {
        return request2({
            url: domain + '/Role/GetAllOrganizationSelectListAsync',
            method: 'get',
            params
        })
    },
    getAuthorityTreeWithApplicationInfo(params) {
        return request2({
            url: domain + '/Authority/GetAuthorityTreeWithApplicationInfo',
            method: 'get',
            params
        })
    },
    createOrUpdateRole(data) {
        return request2({
            url: domain + '/Role/CreateOrUpdateRole',
            method: 'post',
            data
        })
    },
    deleteRoleById(data) {
        return request2({
            url: domain + '/Role/DeleteRoleById',
            method: 'post',
            data
        })
    },
    saveRoleAuthority(data) {
        return request2({
            url: domain + '/Role/SaveRoleAuthority',
            method: 'post',
            data
        })
    },
    getRoleAuthorityListByRoleId(params) {
        return request2({
            url: domain + '/Role/GetRoleAuthorityListByRoleId',
            method: 'get',
            params
        })
    }
};

export { comBusSvc, roleSvc };
