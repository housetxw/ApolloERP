import request2 from '@/utils/request2'
import comBusSvc from '@/api/accountauthority/commonbuslogical'

let domain = process.env.VUE_APP_BASE_ACCOUNTAUTHORITH_API;
let empRoleSvc = {
    getAllEmployeeListAsync(params) {
        return request2({
            url: domain + '/EmployeeRole/GetAllEmployeeListAsync',
            method: 'get',
            params
        })
    },
    getEmployeeListByOrgIdAndTypeAsync(params) {
        return request2({
            url: domain + '/EmployeeRole/GetEmployeeListByOrgIdAndTypeAsync',
            method: 'get',
            params
        })
    },
    getAllOrganizationSelectList(params) {
        return request2({
            url: domain + '/EmployeeRole/GetAllOrganizationSelectListAsync',
            method: 'get',
            params
        })
    },
    getEmployeePage(params) {
        return request2({
            url: domain + '/EmployeeRole/GetEmployeePage',
            method: 'get',
            params
        })
    },
    getRoleListByOrgIdAsync(params) {
        return request2({
            url: domain + '/EmployeeRole/GetRoleListByOrgIdAsync',
            method: 'get',
            params
        })
    },
    getEmployeeRoleListByEmpIdAsync(params) {
        return request2({
            url: domain + '/EmployeeRole/GetEmployeeRoleListByEmpIdAsync',
            method: 'get',
            params
        })
    },
    editEmployeeRole(data) {
        return request2({
            url: domain + '/EmployeeRole/EditEmployeeRole',
            method: 'post',
            data
        })
    }
};

export { comBusSvc, empRoleSvc };
