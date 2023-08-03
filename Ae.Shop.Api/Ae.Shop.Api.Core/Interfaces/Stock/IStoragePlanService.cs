using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
  public  interface IStoragePlanService 
    {
        Task<ApiResult<string>> CreateStoragePlan(StoragePlanDTO request);

        Task<ApiPagedResult<StoragePlanDTO>> GetStoragePlans(GetStoragePlanRequest request);

        Task<ApiPagedResult<StoragePlanProductDTO>> GetStoragePlanProducts(GetStoragePlanProductsRequest request);

        Task<ApiResult<StoragePlanDTO>> GetStoragePlan(StoragePlanDTO request);

        Task<ApiResult<string>> UpdateStoragePlanProduct(UpdateStoragePlanRequest request);

        Task<ApiResult<string>> UpdateStoragePlanProductStatus(UpdateStoragePlanRequest request);

        Task<ApiResult<string>> CancelStoragePlanProduct(StoragePlanProductDTO request);

        Task<ApiPagedResult<StockDiffDTO>> GetStockDiffs(StockDiffRequest request);

        Task<ApiResult<string>> DealStorageProduct(StockDiffDTO request);

        Task<ApiResult<StockDiffDTO>> GetStockDiffDetail(StoragePlanProductDTO request);
    }
}
