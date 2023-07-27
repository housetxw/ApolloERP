using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;

namespace Ae.Account.Api.Client.Interface.AccountAuthorityServer
{
    public interface IEmployeeRoleClient
    {
        #region ！！！授权接口实现！！！

        Task<List<AuthorizationResDTO>> GetEmpAuthorityListByEmpIdAsync(AuthorizationReqDTO req);

        Task<List<AuthorizationResDTO>> GetRangeRoleAuthorityListByIds(RangeRoleAuthorityReqDTO req);


        #endregion ！！！授权接口实现！！！

        Task<List<EmployeeRoleListDTO>> GetEmployeeRoleListByEmpIdAsync(EmployeeRoleListReqDTO req);

        Task<bool> EditEmployeeRole(EmployeeRoleSaveReqDTO req);


    }
}
