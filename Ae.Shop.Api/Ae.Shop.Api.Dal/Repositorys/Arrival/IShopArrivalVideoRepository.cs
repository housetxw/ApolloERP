using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Arrival
{
    public interface IShopArrivalVideoRepository : IRepository<ShopArrivalVideoDO>
    {
        /// <summary>
        /// 查询到店施工视频
        /// </summary>
        /// <param name="recId"></param>
        /// <returns></returns>
        Task<List<ShopArrivalVideoDO>> GetShopArrivalVideos(long recId);
    }
}
