using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Dal.Model;

namespace Ae.AccountAuthority.Service.Dal.Repositorys.IDAL
{
    public interface IEmployeeRoleRepository
    {
        #region ！！！授权接口实现！！！

        Task<List<AuthorizationDO>> GetEmpAuthorityListByEmpIdAsync(AuthorizationReqDTO req);

        Task<List<AuthorizationDO>> GetRangeRoleAuthorityListByIds(RangeRoleAuthorityReqDTO req);


        #endregion ！！！授权接口实现！！！

        Task<List<EmployeeRoleListDO>> GetEmployeeRoleListByEmpIdAsync(EmployeeRoleListReqDTO req);

        Task<List<EmployeeRoleListDTO>> GetEmployeeRoleListByEmpIds(EmployeeRoleListByEmpIdsReqDTO req);

        Task<bool> EditEmployeeRole(EmployeeRoleSaveReqDO req);

    }
}
