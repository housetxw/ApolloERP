using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
using ApolloErp.Data.DapperExtensions;
using System.Threading.Tasks;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Core.Model;

namespace Ae.Shop.Api.Dal.Repositorys
{

    public interface IStockInoutItemRepository : IRepository<StockInoutItemDO>
    {
        Task<List<StockInoutItemDO>> GetStockInoutItemsByStockId(StockInoutItemDO request);

        Task<List<StockInoutItemDO>> GetStockInoutItemsByRelationIdAndType(GetStockInoutItemRequest request);
        Task<List<StockInOutTempDTO>> GetStockInoutItemsByPID(GetShopStockRequest request);

        Task<int> UpdateStockInoutItem(StockInoutItemDO request);

        Task<int> UpdateStockInoutRelationId(StockInoutItemDO request);

        Task<int> UpdateStockInoutBatch(StockInoutItemDO request);

        Task<int> DeleteStockInoutItem(StockInoutItemDO request);
    }
}