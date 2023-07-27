using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;

namespace Ae.Account.Api.Core.Interfaces
{
    public interface IEmployeeRoleService
    {
        #region ！！！授权接口实现！！！

        Task<List<AuthorizationWebResDTO>> GetEmpAuthorityListForWebByEmpIdAsync(AuthorizationReqVO req);
        

        #endregion ！！！授权接口实现！！！

        Task<List<EmployeeRoleLisVO>> GetRoleListByOrgIdAsync(EmployeeRoleDialogListReqVO req);

        Task<List<EmployeeRoleLisVO>> GetEmployeeRoleListByEmpIdAsync(EmployeeRoleListReqVO req);

        Task<bool> EditEmployeeRole(EmployeeRoleSaveReqVO req);

    }
}
