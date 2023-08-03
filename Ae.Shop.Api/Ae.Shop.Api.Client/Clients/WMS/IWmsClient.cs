using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Model;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Client.Response;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.ShopWms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients
{
    public interface IWmsClient
    {
        Task<ApiResult<string>> UpdateProductPackageStatus(PackageProductClientRequest request);

        Task<ApiResult<string>> UpdateAllotTaskStatus(AllotTaskRequest request);
        Task<ApiResult<string>> CreateAllotTask(ApiRequest<AllotTaskDTO> request);

        Task<ApiResult> CreateAsn(CreateAsnRequest request);

        Task<ApiResult<BatchCreateStockLocationClientResponse>> BatchCreateStockLocation(BatchCreateStockLocationClientRequest request);

        Task<ApiResult> BatchCreateStockTranction(BatchCreateStockTranctionClientRequest request);

        Task<ApiResult> UpdateWarehouseTransferProductReceiveNum(UpdateWarehouseTransferProductRequest request);

        Task<ApiResult<List<SupplierProductStockDTO>>> GetSupplierProductStocks(GetSupplierStockRequest request);
        Task<ApiResult<WarehouseConfigDTO>> GetWarehouseConfig(WarehouseConfigRequest request);


        #region 供应商收发货功能

        Task<ApiResult<string>> SubmitVenderStock(VenderStockInitRequest request);

        Task<ApiResult<string>> SubmitCompanyInStock(AcceptCompanyStockRequest request);

        Task<ApiResult<List<VenderStockDTO>>> GetCompanyInStocks(GetCompanyStocksRequest request);

        Task<ApiResult<string>> CompanySendStock(AcceptCompanyStockRequest request);

        Task<ApiResult<VenderStockResponse>> GetCompanyStocks(GetCompanyStocksRequest request);

        Task<ApiResult<string>> CancelCompanySendStock(CancelTaskRequest request);
        #endregion
        Task<ApiResult<ShopOccupyMappingDTO>> GetShopOccupyMappingInfo(GetShopOccupyMappingRequest request);

        
    }
}
