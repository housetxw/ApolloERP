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

    public interface IInventoryFlowItemRepository : IRepository<InventoryFlowItemDO>
    {
        Task<int> UpdateInventoryFlowItemOccupy(InventoryFlowItemDO request);

        Task<int> ReleaseInventoryFlowItem(InventoryFlowItemDO request);

        Task<List<InventoryFlowItemDO>> SelectTargetValues(List<long> shopIds,List<long> batchIds);
    }
}