using Ae.Shop.Service.Client.Model;
using Ae.Shop.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Client.Response;

namespace Ae.Shop.Service.Client.Inteface
{
    public interface IAccountAuthorityClient
    {
        Task<ApiResult<CreateAccountResDTO>> CreateAccountWithIdDefaultPwdAsync(AccountCreateRequest request);

        Task<ApiResult<CheckAccountExistDTO>> CheckAccountIsExistByName(AccountEntityReqByNameDTO req);

        Task<bool> DeleteAccountById(AccountDeleteByIdRequest request);

        Task<List<EmployeeRoleListDTO>> GetEmployeeRoleListByEmpId(EmployeeRoleListReqDTO req);

        Task<bool> AddShopEmployeeDefaultRole(EmployeeDefaultRoleClientRequest request);

        Task<ApiResult<List<RoleDTO>>> GetRoleListByOrgIdAndType(RoleListReqDTO req);

        Task<List<OrgRangeRolesDTO>> GetRoleListByOrgIds(List<OrgEntityReqDTO> req);

        Task<bool> EditEmployeeRoleWithRoleId(EmployeeRoleSaveReqWithRoleIdDTO req);
        /// <summary>
        /// 获取员工包含角色列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<List<EmployeeRolesDTO>> GetEmployeeRoleListByEmpIds(EmployeeRoleListByEmpIdsReqDTO req);
    }
}
