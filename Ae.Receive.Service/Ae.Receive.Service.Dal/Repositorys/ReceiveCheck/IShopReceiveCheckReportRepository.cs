using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public interface IShopReceiveCheckReportRepository : IRepository<ShopReceiveCheckReportDo>
    {
        /// <summary>
        /// 查询检查报告
        /// </summary>
        /// <param name="checkIds"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<List<ShopReceiveCheckReportDo>> GetReceiveCheckReporList(List<long> checkIds, int categoryId);
    }
}
