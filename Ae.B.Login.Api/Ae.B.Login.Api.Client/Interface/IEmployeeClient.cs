using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.Login.Api.Client.Request.ShopManage.Employee;
using Ae.B.Login.Api.Client.Response.ShopManage.Employee;

namespace Ae.B.Login.Api.Client.Interface
{
    public interface IEmployeeClient
    {
        Task<List<EmployeeResDTO>> GetEmpAndOrgListForLoginByAccountIdAsync(EmployeeListReqDTO req);

        Task<ApiPagedResult<EmployeePageDTO>> GetEmpAndOrgPageForLoginByAccountIdAsync(EmployeePageForLoginListReqDTO req);

        Task<ApiPagedResult<EmployeePageDTO>> GetEmpAndOrgPageForLoginByEmpIdAsync(EmployeePageForLoginListByTokenReqDTO req);

        Task<ApiPagedResult<EmployeePageDTO>> GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync(EmployeePageForLoginListByTokenReqDTO req);

        Task<ApiPagedResult<EmployeePageDTO>> GetOrgRangePageForLoginByEmpIdExcCurOrgId(EmployeePageForLoginListByTokenReqDTO req);

    }
}
