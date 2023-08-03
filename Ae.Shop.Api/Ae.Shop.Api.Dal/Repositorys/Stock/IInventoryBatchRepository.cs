using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
using Ae.Shop.Api.Dal.Model;
using ApolloErp.Data.DapperExtensions;
using System.Threading.Tasks;
using Ae.Shop.Api.Core.Request;

namespace Ae.Shop.Api.Dal.Repositorys
{

    public interface IInventoryBatchRepository : IRepository<InventoryBatchDO>
    {
        Task<List<InventoryBatchDO>> GetInventoryBatchs(GetStockInoutItemRequest request);

        Task<List<InventoryBatchDO>> GetInventoryBatchByIds(List<long> ids);

        Task<int> UpdateInventoryBatchNum(InventoryBatchDO request);
    }
}