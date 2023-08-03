using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public interface IShopReceiveResultWordRepository : IRepository<ShopReceiveResultWordDo>
    {
        /// <summary>
        /// 结果词
        /// </summary>
        /// <returns></returns>
        Task<List<ShopReceiveResultWordDo>> GetShopReceiveResultWord();
    }
}
