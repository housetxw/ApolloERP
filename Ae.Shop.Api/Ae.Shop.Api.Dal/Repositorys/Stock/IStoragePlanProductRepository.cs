using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public interface IStoragePlanProductRepository:IRepository<StoragePlanProductDO>
    {
        Task<int> CancelStoragePlanProduct(StoragePlanProductDO request);

        Task<int> UpdateProductStorageNum(StoragePlanProductDO request);

        Task<int> UpdateStoragePlanStatus(StoragePlanProductDO request);

        Task<PagedEntity<StockDiffDO>> GetStockDiffs(StockDiffRequest request);

        Task<int> DealStorageProduct(StoragePlanProductDO request);

        Task<StockDiffDO> GetStockDiffDetail(StoragePlanProductDO request);
    }
}
