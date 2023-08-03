using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Core.Request;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response.Order;
using Ae.ShopOrder.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public interface IEmployeePerformanceOrderRepository : IRepository<EmployeePerformanceOrderDO>
    {
        /// <summary>
        /// 获取员工绩效列表
        /// </summary>
        /// <returns></returns>
        Task<List<EmployeePerformanceOrderDO>> GetEmployeePerformanceOrderList(EmployeePerformanceRequest request);
        Task<int> DeleteShopPerformanceConfig(BatchUpdateCompleteStatusRequest request);
    }
}
