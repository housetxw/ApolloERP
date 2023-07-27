using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IShopPointRepository : IRepository<ShopPointDo>
    {
        /// <summary>
        /// 根据门店获取积分
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<ShopPointDo> GetShopPointByShopId(int shopId, bool readOnly = true);

        /// <summary>
        /// 操作门店积分
        /// </summary>
        /// <param name="shopPointDo"></param>
        /// <returns></returns>
        Task<bool> OperatePointPoint(ShopPointDo shopPointDo);
    }
}
