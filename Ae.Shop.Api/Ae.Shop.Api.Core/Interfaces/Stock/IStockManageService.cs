using Ae.Shop.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.Porduct;
using Ae.Shop.Api.Core.Response.Product;
using Ae.Shop.Api.Core.Response;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IStockManageService
    {
        Task<ApiResult<string>> StockInOutTaskDelivery(StockInOutDTO request);

        Task<ApiPagedResult<InventoryDTO>> GetShopStockList(ShopStockRequest request);

        Task<ApiPagedResult<StockInOutDTO>> GetStockInOutPageData(ShopBatchFlowRequest request);

        Task<ApiResult<List<ProductAllInfoVo>>> GetShopProducts(ProductSearchRequest request);

        Task<ApiResult<string>> CreateInStockTask(StockInOutDTO request);

        Task<ApiResult<string>> CreateInStockTaskForTransfer(StockInOutDTO request);

        Task<ApiResult<string>> CreateOutStockTask(StockInOutDTO request);


        /// <summary>
        /// 创建出入库任务并更新库存（无批次）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<long>> CreateInOutStockAndUpdateInventory(StockInOutDTO request);

        /// <summary>
        /// 出库使用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<StockInoutItemDTO>>> GetStockInoutItems(GetStockInoutItemRequest request);
        Task<ApiResult<List<StockInOutTempDTO>>> GetStockInoutItemsByPid(GetShopStockRequest request);

        Task<ApiResult<StockInOutDTO>> GetStockInOutInfo(StockInOutDTO request);

        Task<ApiResult<List<StockInoutItemDTO>>> GetPurchaseOrderProducts(StockInOutDTO request);

        /// <summary>
        /// 创建入库任务公共接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<string>> CreateInStockTaskCommon(StockInOutDTO request);

        /// <summary>
        /// 盘亏出库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<string>> CreateStorageOutStockTask(StockInOutDTO request);

        Task<ShopSimpleDTO> GetShopById();

        Task<ApiResult<string>> OrderCancelReleaseStock(StockInOutDTO request);

        Task<ApiResult<List<InventoryBatchDTO>>> GetAvailableBatchs(GetAvailableBatchsRequest request);


        Task<ApiResult<List<InventoryBatchDTO>>> GetShopAvailableBatchs(GetAvailableBatchsRequest request);

        Task<ApiResult<string>> OrderInstallReduceStock(OrderStockRequest request);
        Task<ApiResult<string>> OrderRepeatReduceStock(OrderStockRequest request);

        /// <summary>
        /// 铺货同步库存记录
        /// </summary>
        /// <param name="transferProduct"></param>
        /// <param name="shopInfo"></param>
        /// <param name="productTransfer"></param>
        /// <returns></returns>
        Task<ApiResult<string>> TransferSyncInventoryData(object transferProduct, ShopSimpleDTO shopInfo, ProductTransferDTO productTransfer);
    
        /// <summary>
        /// 批量查询门店铺货产品的库存(内部使用)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<ProductStockDTO>>> GetProductStocks(List<ProductStockDTO> request);

        
        Task<ApiResult<List<ProductStockDTO>>> GetTransferProductStocks(GetShopProductStocksRequest request);

        Task<ApiResult<List<ProductStockResponse>>> GetShopProductStocks(GetShopProductStocksRequest request);
        Task<ApiResult<List<ProductStockDTO>>> GetProductStocksForOut(List<ProductStockDTO> request);

        Task<ApiResult<string>> CancelStockInoutTask(StockInOutDTO request);

        /// <summary>
        /// 返回门店铺货sku库存
        /// StockNum:库存总量  Num：可用库存
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<ProductStockDTO>>> GetAllProductStocks();

        Task<ApiResult<List<InventoryBatchDTO>>> GetInventoryBatchs(GetShopStockRequest request);

        Task<ApiResult<List<InventoryFlowItemDTO>>> GetInventoryFlowItems(GetShopStockRequest request);

        Task<ApiResult<List<BasicInfoDTO>>> GetStockInTypes();

        Task<ApiResult<List<BasicInfoDTO>>> GetStockOutTypes();

        Task<ApiResult<List<ShopWmsLogDTO>>> GetShopWmsLogs(ShopWmsLogDTO request);

        Task<ApiResult<List<ProductStockDTO>>> GetAvailableStocks(GetProductStocksRequest request);

        Task<ApiResult<List<ProductStockDTO>>> GetOccupyStocks(GetProductStocksRequest request);

        Task<ApiResult<List<SupplierProductStockDTO>>> GetSupplierProductStocks(GetSupplierStockRequest request);

        Task<ApiResult<List<SupplierProductStockDTO>>> GetSupplierProductStockDetails(GetSupplierStockRequest request);

        /// <summary>
        /// 门店产品库存查询（只查非0库存）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<ProductStockDTO>>> GetShopOutPurchaseStocks(GetBatchProductStockRequest request);

        /// <summary>
        /// 通用门店库存查询（无批次）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<ProductStockDTO>>> GetShopAllProductStocks(GetProductStockRequest request);

        Task<ApiResult<string>> BatchCreateStockOutTask(StockInOutDTO request);
    }
}
