using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public interface IShopCheckResultRepository : IRepository<ShopCheckResultDo>
    {
        /// <summary>
        /// 检查结果查询
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <param name="propertyType"></param>
        /// <returns></returns>
        Task<List<ShopCheckResultDo>> GetShopCheckResult(long checkId, int categoryId, int propertyType);
    }
}
