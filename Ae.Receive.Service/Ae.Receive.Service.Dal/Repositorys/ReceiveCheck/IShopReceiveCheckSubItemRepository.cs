using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public interface IShopReceiveCheckSubItemRepository:IRepository<ShopReceiveCheckSubItemDo>
    {
        /// <summary>
        /// 配置项关联结果
        /// </summary>
        /// <returns></returns>
        Task<List<ShopReceiveCheckSubItemDo>> GetShopReceiveCheckSubItem();
    }
}
