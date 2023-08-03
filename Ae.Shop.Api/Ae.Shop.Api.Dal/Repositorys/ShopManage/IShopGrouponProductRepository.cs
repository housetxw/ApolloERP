using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.ShopManage;

namespace Ae.Shop.Api.Dal.Repositorys.ShopManage
{
    public interface IShopGrouponProductRepository : IRepository<ShopGrouponProductDO>
    {
        /// <summary>
        /// 获取门店美容团购产品
        /// </summary>
        /// <param name="shopId"></param>]
        /// <param name="status"></param>
        /// <returns></returns>
        Task<List<ShopGrouponProductDO>> GetShopGrouponProduct(int shopId, sbyte? status);

        /// <summary>
        /// 获取门店美容团购产品详情
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pid"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<ShopGrouponProductDO> GetShopGrouponProductDetail(int shopId, string pid,
            bool readOnly = true);
    }
}
