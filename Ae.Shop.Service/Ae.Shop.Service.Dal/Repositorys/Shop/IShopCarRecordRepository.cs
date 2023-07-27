using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IShopCarRecordRepository : IRepository<ShopCarRecordDO>
    {
        /// <summary>
        /// 查询门店车辆记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<ShopCarRecordDO>> GetShopCarRecordListByShopId(GetShopCarRecordPageListRequest request);

        /// <summary>
        /// 修改门店车辆使用记录
        /// </summary>
        /// <param name="shopCarRecordDO"></param>
        /// <returns></returns>
        Task<int> ModifyShopCarRecordInfo(ShopCarRecordDO shopCarRecordDO);
    }
}
