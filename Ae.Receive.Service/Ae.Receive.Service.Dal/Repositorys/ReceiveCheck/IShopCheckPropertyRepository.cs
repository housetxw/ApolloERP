using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public interface IShopCheckPropertyRepository:IRepository<ShopCheckPropertyDo>
    {
        /// <summary>
        /// 配置查询
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="parentId">-1 查所有</param>
        /// <returns></returns>
        Task<List<ShopCheckPropertyDo>> GetShopCheckProperty(int categoryId, long parentId);

        /// <summary>
        /// 根据KeyName查询data
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="categoryId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<ShopCheckPropertyDo> GetPropertyByKeyName(string keyName, int categoryId, int parentId = 0);
    }
}
