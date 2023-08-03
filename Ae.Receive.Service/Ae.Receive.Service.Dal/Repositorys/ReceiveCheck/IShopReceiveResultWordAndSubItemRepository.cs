using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public interface IShopReceiveResultWordAndSubItemRepository : IRepository<ShopReceiveResultWordAndSubItemDo>
    {
        /// <summary>
        /// 结果-结果词
        /// </summary>
        /// <returns></returns>
        Task<List<ShopReceiveResultWordAndSubItemDo>> GetShopReceiveResultWordAndSubItem();
    }
}
