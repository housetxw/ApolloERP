using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model.ShopReport;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.ShopReport;
using Ae.Shop.Api.Core.Response.ShopReport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IShopReportService
    {
        Task<ApiResult<List<ShopSalesMonthResponse>>> GetShopSalesMonthList(GetShopSalesMonthRequest request);

        Task<ApiResult<List<EmployeePerformanceDto>>> GetEmployeePerformanceList(EmployeePerformanceRequest request);
    }
}
