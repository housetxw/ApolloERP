using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Enums;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Filters;

namespace Ae.Shop.Api.Controllers
{

    [Route("[controller]/[action]")]
    //[Filter(nameof(ShopStockController))]
    public class ShopStockController : ControllerBase
    {
        public IOrderStockService _orderStockService;
        private IIdentityService identityService;
        private readonly IStockManageService _stockManageService;

        public ShopStockController(IOrderStockService orderStockService,
            IIdentityService identityService, IStockManageService stockManageService)
        {
            this._orderStockService = orderStockService;
            this.identityService = identityService;
            this._stockManageService = stockManageService;
        }

        /// <summary>
        /// 订单占库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<string>> OrderOccupyStock([FromBody]OrderStockRequest request)
        {
            return await _orderStockService.OrderOccupyStock(request);
        }

        /// <summary>
        /// 订单安装 扣减库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<string>> OrderInstallReduceStock([FromBody]OrderStockRequest request)
        {
            //return await _orderStockService.OrderInstallReduceStock(request);
            //库存简化，取消批次，直接出库
            request.OperationType = StockOperateTypeEnum.出库.ToString();
            request.SourceType = StockOutTypeEnum.订单安装.ToString();
            return await _orderStockService.OrderRepeatReduceStock(request);
        }

        /// <summary>
        /// 订单重新扣减库存（正常安装扣减失败时调用）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> OrderRepeatReduceStock([FromBody] ApiRequest<OrderStockRequest> request)
        {
            request.Data.OperationType = StockOperateTypeEnum.出库.ToString();
            request.Data.SourceType = StockOutTypeEnum.订单安装.ToString();
            return await _orderStockService.OrderRepeatReduceStock(request.Data);
        }

        /// <summary>
        /// 订单取消释放库存 无批次
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<string>> OrderCancelReleaseStockNoBatch([FromBody]OrderStockRequest request)
        {
            request.OperationType = StockOperateTypeEnum.入库.ToString();
            request.SourceType = StockInTypeEnum.订单取消.ToString();
            return await _orderStockService.OrderRepeatReduceStock(request);
        }

        /// <summary>
        /// 释放库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<string>> ReleaseStock([FromBody] OrderStockRequest request)
        {
            return await _orderStockService.ReleaseStock(request);
        }

        /// <summary>
        /// 门店可用库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<List<InventoryBatchDTO>>> GetAvailableBatchs([FromQuery]GetAvailableBatchsRequest request)
        {
            return await _stockManageService.GetAvailableBatchs(request);
        }

        [HttpGet]
        public async Task<ApiPagedResult<OccupyQueueDTO>> GetOrderQueues([FromQuery]GetOrderQueueRequest request)
        {
            return await _orderStockService.GetOrderQueues(request);
        }

        [HttpPost]
        public async Task<ApiResult<string>> ReOccupyStock([FromBody]ApiRequest<GetOrderQueueRequest> request)
        {
            return await _orderStockService.ReOccupyStock(request.Data);
        }

        [HttpPost]
        public async Task<ApiResult<string>> CancelOrderQueue([FromBody]ApiRequest<GetOrderQueueRequest> request)
        {
            return await _orderStockService.CancelOrderQueue(request.Data);
        }

        [HttpGet]
        public async Task<ApiResult<List<OccupyStockLogDTO>>> GetOccupyStockLogs([FromQuery]OccupyStockLogDTO request)
        {
            return await _orderStockService.GetOccupyStockLogs(request);
        }

        [HttpGet]
        public async Task<ApiResult<List<OccupyItemDTO>>> GetOccupyItems([FromQuery]OccupyItemDTO request)
        {
            return await _orderStockService.GetOccupyItems(request);
        }
    }
}