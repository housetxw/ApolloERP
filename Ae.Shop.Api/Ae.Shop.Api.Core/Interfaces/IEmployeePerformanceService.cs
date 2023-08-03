using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IEmployeePerformanceService
    {
        Task<ApiResult<List<EmployeePerformanceOrderDTO>>> GetEmployeePerformanceOrderList(EmployeePerformanceRequest request);
        Task<ApiResult> UpdateEmployeePerformanceOrder(UpdateEmployeePerformanceRequest request);
    }
}
