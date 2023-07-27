import request2 from '@/utils/request2'
import comBusSvc from '@/api/accountauthority/commonbuslogical'

let domain = process.env.VUE_APP_BASE_ACCOUNTAUTHORITH_API;
let shopManageDomain = process.env.VUE_APP_BASE_SHOPMANAGE_API;

let empSvc = {
    getEmployeePage(params) {
        return request2({
            url: shopManageDomain + '/Employee/GetEmployeePageForWeb',
            method: 'get',
            params
        })
    },

    getEmployeeInfo(params) {
        return request2({
            url: shopManageDomain + '/Employee/GetEmployeeInfo',
            method: 'get',
            params
        })
    },

    getDepartmentSelByOrgIdType(params) {
        return request2({
            url: shopManageDomain + '/Department/GetDepartmentSelByOrgIdType',
            method: 'get',
            params
        })
    },

    getDepartmentTreeByOrgIdType(params) {
        return request2({
            url: shopManageDomain + '/Department/GetDepartmentTreeByOrgIdType',
            method: 'get',
            params
        })
    },

    getJobListByOrgIdForWeb(params) {
        return request2({
            url: shopManageDomain + '/Job/GetJobListByOrgIdForWeb',
            method: 'get',
            params
        })
    },

    createOrUpdateEmployeeForWeb(data) {
        return request2({
            url: shopManageDomain + '/Employee/CreateOrUpdateEmployeeForWeb',
            method: 'post',
            data
        })
    },

    batchDeleteEmployeeById(data) {
        return request2({
            url: shopManageDomain + '/Employee/BatchDeleteEmployeeById',
            method: 'post',
            data
        })
    },
    CheckAccountIsExistByName(params) {
        return request2({
            url: shopManageDomain + '/Employee/CheckAccountIsExistByName',
            method: 'get',
            params
        })
    },
    GetRangeRoleListByOrgId(params) {
        return request2({
            url: shopManageDomain + '/Employee/GetRangeRoleListByOrgId',
            method: 'get',
            params
        })
    },
    GetRoleList(params) {
        return request2({
            url: shopManageDomain + '/Employee/GetRoleList',
            method: 'get',
            params
        })
    },
    GetEmployeeListByOrgIdAndType(params) {
        return request2({
            url: shopManageDomain + '/Employee/GetEmployeeListByOrgIdAndTypeAsync',
            method: 'get',
            params
        })
    }


};

export { comBusSvc, empSvc };
