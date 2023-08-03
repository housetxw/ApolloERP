using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Model.Arrival;
using Ae.Shop.Api.Dal.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Arrival
{
    public interface IShopReceiveCheckRepository : IRepository<ShopReceiveCheckDO>
    {
        /// <summary>
        /// 得到门店到店检测信息
        /// </summary>
        /// <param name="receiveId"></param>
        /// <returns></returns>
        Task<List<ShopReceiveCheckDO>> GetShopReceiveCheckInfo(List<long> receiveIds);
    }
}
