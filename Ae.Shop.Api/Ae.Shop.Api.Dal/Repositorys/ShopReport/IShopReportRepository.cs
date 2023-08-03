using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request.ShopReport;
using Ae.Shop.Api.Dal.Model.ShopReport;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.ShopReport
{
    public interface IShopReportRepository
    {
        /// <summary>
        /// 得到订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopOrderDO>> GetShopOrderList(GetShopSalesMonthRequest request);
    }
}
