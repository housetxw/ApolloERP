using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
using Ae.Shop.Api.Dal.Model;
using ApolloErp.Data.DapperExtensions;
using System.Threading.Tasks;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Model;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public interface IStockInoutRepository : IRepository<StockInoutDO>
    {
        Task<PagedEntity<StockInOutTempDO>> GetStockInOutPageData(ShopBatchFlowRequest request);

        Task<int> GenerateStockNo(StockInoutDO request);

        Task<int> UpdateStockInoutStatus(StockInoutDO request);

        Task<int> DeleteStockInout(StockInoutDO request);

        Task<List<SupplierProductStockDTO>> GetSupplierSaleProducts(GetSupplierStockRequest request);
    }
}