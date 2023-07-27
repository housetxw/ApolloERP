using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;

namespace Ae.AccountAuthority.Service.Core.Interfaces
{
    public interface IEmployeeRoleService
    {
        #region ！！！授权接口实现！！！

        Task<AuthorizationAPPResDTO> GetEmpAuthorityListForAPPByEmpIdAsync(AuthorizationReqDTO req);

        Task<List<AuthorizationResDTO>> GetEmpAuthorityListByEmpIdAsync(AuthorizationReqDTO req);

        Task<List<AuthorizationResDTO>> GetRangeRoleAuthorityListByIds(RangeRoleAuthorityReqDTO req);


        #endregion ！！！授权接口实现！！！

        Task<bool> EditEmployeeRoleWithRoleId(EmployeeRoleSaveReqWithRoleIdDTO req);

        Task<(bool flag, string message)> AddShopEmployeeDefaultRole(EmployeeDefaultRoleReqDTO req);

        Task<List<EmployeeRoleListDTO>> GetEmployeeRoleListByEmpIdAsync(EmployeeRoleListReqDTO req);

        Task<List<EmployeeRolesDTO>> GetEmployeeRoleListByEmpIds(EmployeeRoleListByEmpIdsReqDTO req);

        Task<bool> EditEmployeeRole(EmployeeRoleSaveReqDTO req);

    }
}
