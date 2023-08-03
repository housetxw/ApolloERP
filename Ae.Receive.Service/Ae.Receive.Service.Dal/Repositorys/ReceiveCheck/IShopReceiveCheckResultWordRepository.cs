using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public interface IShopReceiveCheckResultWordRepository : IRepository<ShopReceiveCheckResultWordDo>
    {
        /// <summary>
        /// 检查结果 结果词
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<List<ShopReceiveCheckResultWordDo>> GetShopReceiveCheckResultWord(long checkId, int categoryId);
    }
}
