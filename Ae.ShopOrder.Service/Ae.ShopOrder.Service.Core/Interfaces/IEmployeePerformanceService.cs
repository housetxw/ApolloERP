using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Core.Interfaces
{
    public interface IEmployeePerformanceService
    {
        /// <summary>
        /// 获取员工绩效列表
        /// </summary>
        /// <returns></returns>
        Task<List<EmployeePerformanceOrderDTO>> GetEmployeePerformanceOrderList(EmployeePerformanceRequest request);
        /// <summary>
        /// 更新订单绩效
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateOrderPerformance(BatchUpdateOrderRequest request);


    }
}
