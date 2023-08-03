using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ae.Shop.Api.Filters;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Microsoft.AspNetCore.Authorization;
using ApolloErp.Login.Auth;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Response.Product;
using Ae.Shop.Api.Core.Request.Porduct;
using Ae.Shop.Api.Core.Response;
using AutoMapper;
using ApolloErp.Log;
using Newtonsoft.Json;

namespace Ae.Shop.Api.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(StockManageController))]
    public class StockManageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<StockManageController> _logger;
        public IStockManageService stockManageService;
        private IIdentityService identityService;
        private IStoragePlanService storagePlanService;
        public StockManageController(IMapper mapper, IStockManageService stockManageService, ApolloErpLogger<StockManageController> logger,
            IIdentityService identityService, IStoragePlanService storagePlanService)
        {
            this._mapper = mapper;
            this._logger = logger;
            this.stockManageService = stockManageService;
            this.identityService = identityService;
            this.storagePlanService = storagePlanService;
        }

        /// <summary>
        /// 门店库存列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<InventoryDTO>> GetShopStockList([FromQuery]ShopStockRequest request)
        {
            return await stockManageService.GetShopStockList(request);
        }

        [HttpGet]
        public async Task<ApiResult<List<InventoryBatchDTO>>> GetInventoryBatchs([FromQuery]GetShopStockRequest request)
        {
            return await stockManageService.GetInventoryBatchs(request);
        }

        [HttpGet]
        public async Task<ApiResult<List<InventoryFlowItemDTO>>> GetInventoryFlowItems([FromQuery]GetShopStockRequest request)
        {
            return await stockManageService.GetInventoryFlowItems(request);
        }

        #region 出入库记录

        [HttpPost]
        public async Task<ApiPagedResult<StockInOutDTO>> GetStockInOutPageData([FromBody]ApiRequest<ShopBatchFlowRequest> request)
        {
            return await stockManageService.GetStockInOutPageData(request.Data);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<List<ProductAllInfoVo>>> GetShopProducts([FromQuery]ProductSearchRequest request)
        {
            return await stockManageService.GetShopProducts(request);
        }

        [HttpGet]
        public async Task<ApiResult> GetCurrentUser()
        {
            var userName = this.identityService.GetUserName() + " " + DateTime.Now.ToString("yyyy-MM-dd hh:mm");
            return Result.Success<string>(userName);
        }

        [HttpPost]
        public async Task<ApiResult<List<StockInoutItemDTO>>> GetStockInoutItems([FromBody]ApiResult<GetStockInoutItemRequest> request)
        {
            return await stockManageService.GetStockInoutItems(request.Data);
        }

        /// <summary>
        /// 查询产品出入库记录  --库存明细使用!!!
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<StockInOutTempDTO>>> GetStockInoutItemsByPid([FromQuery] GetShopStockRequest request)
        {
            return await stockManageService.GetStockInoutItemsByPid(request);
        }

        [HttpPost]
        public async Task<ApiResult<string>> CreateInStockTask([FromBody]ApiResult<StockInOutDTO> request)
        {
            return await stockManageService.CreateInStockTask(request.Data);
        }

        [HttpPost]
        public async Task<ApiResult<string>> CreateOutStockTask([FromBody]ApiResult<StockInOutDTO> request)
        {
            return await stockManageService.CreateOutStockTask(request.Data);
        }


        [HttpGet]
        public async Task<ApiResult<List<StockInoutItemDTO>>> GetPurchaseOrderProducts([FromQuery]StockInOutDTO request)
        {
            return await stockManageService.GetPurchaseOrderProducts(request);
        }


        [HttpGet]
        public async Task<ApiResult<StockInOutDTO>> GetStockInOutInfo([FromQuery]StockInOutDTO request)
        {
            return await stockManageService.GetStockInOutInfo(request);
        }


        /// <summary>
        /// 出库任务发出
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> StockInOutTaskDelivery([FromBody]ApiRequest<StockInOutDTO> request)
        {
            return await stockManageService.StockInOutTaskDelivery(request.Data);
        }


        [HttpPost]
        public async Task<ApiResult<string>> CancelStockInoutTask([FromBody]ApiRequest<StockInOutDTO> request)
        {
            return await stockManageService.CancelStockInoutTask(request.Data);
        }
        #endregion

        #region 盘库计划


        [HttpPost]
        public async Task<ApiResult<string>> CreateStoragePlan([FromBody]ApiRequest<StoragePlanDTO> request)
        {
            return await storagePlanService.CreateStoragePlan(request.Data);
        }

        [HttpPost]
        public async Task<ApiPagedResult<StoragePlanDTO>> GetStoragePlans([FromBody]ApiRequest<GetStoragePlanRequest> request)
        {
            return await storagePlanService.GetStoragePlans(request.Data);
        }

        [HttpGet]
        public async Task<ApiPagedResult<StoragePlanProductDTO>> GetStoragePlanProducts([FromQuery]GetStoragePlanProductsRequest request)
        {
            return await storagePlanService.GetStoragePlanProducts(request);
        }

        [HttpPost]
        public async Task<ApiResult<string>> CancelStoragePlanProduct([FromBody]ApiRequest<StoragePlanProductDTO> request)
        {
            return await storagePlanService.CancelStoragePlanProduct(request.Data);
        }

        /// <summary>
        /// 盘库暂存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> UpdateStoragePlanProduct([FromBody]ApiRequest<UpdateStoragePlanRequest> request)
        {
            return await storagePlanService.UpdateStoragePlanProduct(request.Data);
        }

        /// <summary>
        /// 盘库提交
        /// </summary>
        /// <param name="request">修改的产品</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> UpdateStoragePlanProductStatus([FromBody]ApiRequest<UpdateStoragePlanRequest> request)
        {
            return await storagePlanService.UpdateStoragePlanProductStatus(request.Data);
        }

        [HttpGet]
        public async Task<ApiResult<StoragePlanDTO>> GetStoragePlan([FromQuery]StoragePlanDTO request)
        {
            return await storagePlanService.GetStoragePlan(request);
        }
        #endregion

        #region  库存差异
        [HttpPost]
        public async Task<ApiPagedResult<StockDiffDTO>> GetStockDiffs([FromBody]ApiResult<StockDiffRequest> request)
        {
            return await storagePlanService.GetStockDiffs(request.Data);
        }

        [HttpGet]
        public async Task<ApiResult<StockDiffDTO>> GetStockDiffDetail([FromQuery]StoragePlanProductDTO request)
        {
            return await storagePlanService.GetStockDiffDetail(request);
        }

        [HttpPost]
        public async Task<ApiResult<string>> DealStorageProduct([FromBody]ApiRequest<StockDiffDTO> request)
        {
            return await storagePlanService.DealStorageProduct(request.Data);
        }


        #endregion

        [HttpGet]
        public async Task<ApiResult<ShopSimpleDTO>> GetShopById()
        {
            var result = await stockManageService.GetShopById();
            return Result.Success<ShopSimpleDTO>(result);
        }

        /// <summary>
        /// 批量查询门店铺货产品的库存量(内部使用)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ProductStockDTO>>> GetProductStocks([FromBody]ApiRequest<ProductStockRequest> request)
        {
            //return await stockManageService.GetProductStocks(request.Data.Stocks);
            var shopId = Convert.ToInt64(identityService.GetOrganizationId());

            var result = await stockManageService.GetShopAllProductStocks(new GetProductStockRequest
            {
                LocationId = shopId,
                ProductIds = request.Data.Stocks.Select(r => r.ProductId).ToList(),
                NumType = 0
            });
            foreach (var item in result.Data)
            {
                var stock = request.Data.Stocks.Find(r => r.ProductId == item.ProductId);
                if (stock != null)
                {
                    item.CategoryName = stock.CategoryName;
                    item.CostPrice = stock.CostPrice;
                }
            }

            return result;
        }

        /// <summary>
        /// 批量查询门店外采产品的库存量(内部使用)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ProductStockDTO>>> GetProductStocksForOut([FromBody]ApiResult<ProductStockRequest> request)
        {
            //return await stockManageService.GetProductStocksForOut(request.Data.Stocks);
            var shopId = Convert.ToInt64(identityService.GetOrganizationId());

            var result = await stockManageService.GetShopAllProductStocks(new GetProductStockRequest
            {
                LocationId = shopId,
                ProductIds = request.Data.Stocks.Select(r => r.ProductId).ToList(),
                NumType = 0
            });
            foreach (var item in result.Data)
            {
                var stock = request.Data.Stocks.Find(r => r.ProductId == item.ProductId);
                if (stock != null)
                {
                    item.CategoryName = stock.CategoryName;
                    item.CostPrice = stock.CostPrice;
                }
            }

            return result;
        }

        /// <summary>
        /// 门店调拨 查询门店库存接口,boss里也会使用到
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult<List<ProductStockDTO>>> GetShopProductStocks([FromBody] ApiResult<GetShopProductStocksRequest> request)
        {
            _logger.Info(JsonConvert.SerializeObject(request));

            //return await stockManageService.GetShopProductStocks(request);
            if (request.Data.ShopId <= 0)
            {
                var shopId = Convert.ToInt64(identityService.GetOrganizationId());
                request.Data.ShopId = shopId;
            }

            long sourceWarehouse = 0;
            if (!string.IsNullOrWhiteSpace(request.Data.SourceWarehouse))
            {
                sourceWarehouse = Convert.ToInt64(request.Data.SourceWarehouse.Split('-')[0]);
            }
            if (sourceWarehouse <= 0)
            {
                sourceWarehouse = request.Data.ShopId;
            }

            var result = await stockManageService.GetShopAllProductStocks(new GetProductStockRequest
            {
                LocationId = sourceWarehouse,
                SearchKey = request.Data.SearchKey,
                ProductIds = request.Data.Pids,
                NumType = 1
            });
            return result;

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult<string>> CreateInStockTaskCommon([FromBody]StockInOutDTO request)
        {
            return await stockManageService.CreateInStockTaskCommon(request);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult<long>> CreateInOutStockAndUpdateInventory([FromBody] StockInOutDTO request)
        {
            return await stockManageService.CreateInOutStockAndUpdateInventory(request);
        }

        /// <summary>
        /// 订单详情查询库存接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult<List<ProductStockDTO>>> GetTransferProductStocks([FromBody]GetShopProductStocksRequest request)
        {
            //return await stockManageService.GetTransferProductStocks(request);
            var tempRequest = new GetProductStockRequest{
                LocationId = request.ShopId,
                ProductIds = request.Pids,
                SearchKey = request.SearchKey
            };
            return await stockManageService.GetShopAllProductStocks(tempRequest);
        }

        [HttpGet]
        public async Task<ApiResult<List<BasicInfoDTO>>> GetStockInTypes()
        {
            return await stockManageService.GetStockInTypes();
        }

        [HttpGet]
        public async Task<ApiResult<List<BasicInfoDTO>>> GetStockOutTypes()
        {
            return await stockManageService.GetStockOutTypes();
        }


        [HttpGet]
        public async Task<ApiResult<List<ShopWmsLogDTO>>> GetShopWmsLogs([FromQuery]ShopWmsLogDTO request)
        {
            return await stockManageService.GetShopWmsLogs(request);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult<List<ProductStockDTO>>> GetAvailableStocks([FromBody]GetProductStocksRequest request)
        {
            return await stockManageService.GetAvailableStocks(request);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult<List<ProductStockDTO>>> GetOccupyStocks([FromBody]GetProductStocksRequest request)
        {
            return await stockManageService.GetOccupyStocks(request);
        }

        [HttpGet]
        public async Task<ApiResult<List<SupplierProductStockDTO>>> GetSupplierProductStocks([FromQuery]GetSupplierStockRequest request)
        {
            return await stockManageService.GetSupplierProductStocks(request);
        }

        [HttpGet]
        public async Task<ApiResult<List<SupplierProductStockDTO>>> GetSupplierProductStockDetails([FromQuery]GetSupplierStockRequest request)
        {
            return await stockManageService.GetSupplierProductStockDetails(request);
        }

        /// <summary>
        /// 获取一个门店指定产品/所有产品的库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]        
        public async Task<ApiResult<List<ProductStockDTO>>> GetShopOutPurchaseStocks([FromBody]GetBatchProductStockRequest request)
        {
            //return await stockManageService.GetShopOutPurchaseStocks(request);
            return await stockManageService.GetShopAllProductStocks(new GetProductStockRequest {
                LocationId = request.LocationId,
                ProductIds = request.ProductIds,
                NumType = 2
            });
        }

        /// <summary>
        /// 通用门店库存查询（无批次）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult<List<ProductStockDTO>>> GetShopAllProductStocks([FromBody] GetProductStockRequest request)
        {
            return await stockManageService.GetShopAllProductStocks(request);
        }

    }
}