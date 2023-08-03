using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog.Filters;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.ShopPurchase;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.Porduct;
using Ae.Shop.Api.Core.Request.ShopPurchase;
using Ae.Shop.Api.Core.Response.Product;
using Ae.Shop.Api.Core.Response.ShopPurchase;
using ApolloErp.Login.Auth;
using Ae.Shop.Api.Core.Model;

namespace Ae.Shop.Api.Controllers
{
    /// <summary>
    /// 到店记录
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ArrivalController))]
    public class ShopPurchaseController : ControllerBase
    {
        private readonly IShopPurchaseService _shopPurchaseService;
        private readonly IIdentityService identityService;


        public ShopPurchaseController(IShopPurchaseService shopPurchaseService, IIdentityService identityService)
        {
            this._shopPurchaseService = shopPurchaseService;
            this.identityService = identityService;
        }

        /// <summary>
        ///  获取所有的供应商信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<VenderDTO>>> GetSupplierList()
        {
            return await _shopPurchaseService.GetSupplierList();
        }

        /// <summary>
        /// 分页查询门店采购信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<PurchaseOrderDTO>> SearchPurchaseOrder([FromQuery]PurchaseOrderSearchRequest request)
        {
            return await _shopPurchaseService.SearchPurchaseOrder(request);

        }

        /// <summary>
        /// 编辑采购单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SavePurchaseOrder([FromBody] ApiRequest<PurchaseOrderEditRequest> request)
        {
            return await _shopPurchaseService.SavePurchaseOrder(request.Data);
        }

        [HttpPost]
        public async Task<ApiResult<string>> DeletePurchaseOrder([FromBody] ApiRequest<PurchaseOrderDTO> request)
        {
            return await _shopPurchaseService.DeletePurchaseOrder(request.Data);
        }

        /// <summary>
        /// 获取采购单信息
        /// </summary>
        /// <param name="purchaseOrderId">采购单Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<PurchaseOrderDetailResponse>> GetPurchaseOrderDetail(string purchaseOrderId)
        {
            return await _shopPurchaseService.GetPurchaseOrderDetail(purchaseOrderId);
        }

        /// <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="request">搜索关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<ProductSearchResponse>> SearchProduct([FromQuery] ProductSearchRequest request)
        {
            var result = await _shopPurchaseService.SearchProduct(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 搜索供应商
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<VenderVO>> SearchVender([FromQuery] SearchVenderRequest request)
        {
            return await _shopPurchaseService.SearchVender(request);
        }

        [HttpGet]
        public async Task<ApiResult<string>> GetCurrentUser()
        {
            return Result.Success<string>(this.identityService.GetUserName());
        }

        /// <summary>
        /// 增改供应商
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> UpsertVender([FromBody] ApiRequest<VenderVO> request)
        {
            return await _shopPurchaseService.UpsertVender(request.Data);
        }

        /// <summary>
        /// 获取供应商详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<VenderVO>> GetVender([FromQuery] BaseGetByIdRequest request)
        {
            return await _shopPurchaseService.GetVender(request);
        }

        /// <summary>
        /// 启用或禁用供应商
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> ActiveVender([FromBody] ApiRequest<VenderVO> request)
        {
            return await _shopPurchaseService.ActiveVender(request.Data);
        }

        /// <summary>
        /// 修改采购产品价格
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> UpdatePurchasePrice([FromBody] ApiRequest<List<PurchaseOrderProductDTO>> request)
        {
            return await _shopPurchaseService.UpdatePurchasePrice(request.Data);
        }

        /// <summary>
        /// 采购单发货
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> UpdatePurchaseOrderDeliveryInfo([FromBody] ApiRequest<PurchaseOrderDTO> request)
        {
            return await _shopPurchaseService.UpdatePurchaseOrderDeliveryInfo(request.Data);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<string>> QuickCreateVender([FromBody] VenderDTO request)
        {
            return await _shopPurchaseService.QuickCreateVender(request);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<List<VenderDTO>>> GetVenders([FromQuery] VenderDTO request)
        {
            return await _shopPurchaseService.GetVenders(request);
        }

        [HttpGet]
        public async Task<ApiResult<List<VenderDTO>>> GetTargetVenders([FromQuery] VenderDTO request)
        {
            return await _shopPurchaseService.GetVenders(request);
        }

        /// <summary>
        /// App的外采入口(创建采购单+入库)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ApiResult<string>> CreatePurchaseOrder([FromBody] PurchaseOrderEditRequest request)
        {
            return await _shopPurchaseService.CreatePurchaseOrder(request);
        }

        /// <summary>
        /// 采购单入库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> PurchaseInStock([FromBody] ApiResult<PurchaseOrderDTO> request)
        {
            return await _shopPurchaseService.PurchaseInStock(request.Data);
        }

        /// <summary>
        /// App的外采入口(创建采购单,不入库)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult<string>> CreatePurchaseOrderFouOut([FromBody] PurchaseOrderEditRequest request)
        {
            return await _shopPurchaseService.CreatePurchaseOrderFouOut(request);
        }

        /// <summary>
        /// App的外采入口(采购单发货入库)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult<string>> SendPurchaseOrder([FromBody] PurchaseOrderBatchSendDTO request)
        {
            return await _shopPurchaseService.SendPurchaseOrder(request);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<List<PurchaseOrderViewDTO>>> SelectOutPurchaseOrders([FromQuery] SelectOutPurchaseOrdersRequest request)
        {
            var result = await _shopPurchaseService.SelectOutPurchaseOrders(request);
            return Result.Success(result);

        }

        [HttpGet]
        public async Task<ApiResult<PurchaseOrderPayDTO>> GetPurchaseOrderPayInfo([FromQuery] PurchaseOrderViewDTO request)
        {
            var result = await _shopPurchaseService.GetPurchaseOrderPayInfo(request);
            return Result.Success(result);

        }

        [HttpPost]
        public async Task<ApiResult<string>> SavePayInfo([FromBody] ApiRequest<PurchaseOrderDTO> request)
        {
            return await _shopPurchaseService.SavePayInfo(request.Data);

        }

        [HttpPost]
        public async Task<ApiResult<string>> UpsertPurchaseMonthPayInfo([FromBody] ApiRequest<PurchaseMonthPayDTO> request)
        {
            return await _shopPurchaseService.UpsertPurchaseMonthPayInfo(request.Data);

        }

        [HttpGet]
        public async Task<ApiPagedResult<PurchaseMonthPayDTO>> SelectPurchaseMonthPayInfos([FromQuery] PurchaseMonthPayRequest request)
        {
            return await _shopPurchaseService.SelectPurchaseMonthPayInfos(request);

        }

        [HttpPost]
        public async Task<ApiResult<string>> AuditPurchaseMonthPay([FromBody] ApiRequest<PurchaseMonthPayDTO> request)
        {
            return await _shopPurchaseService.AuditPurchaseMonthPay(request.Data);

        }

        [HttpPost]
        public async Task<ApiResult<string>> SavePurchaseMonthPay([FromBody] ApiRequest<PurchaseMonthPayDTO> request)
        {
            return await _shopPurchaseService.SavePurchaseMonthPay(request.Data);

        }

        [HttpPost]
        public async Task<ApiResult<GetTargetVenderPurchaseOrderResponse>> GetVenderPurchaseOrders([FromBody] ApiRequest<GetVenderPurchaseOrdersRequest> request)
        {
            return await _shopPurchaseService.GetVenderPurchaseOrders(request.Data);

        }

        [HttpGet]
        public async Task<ApiResult<List<AddPurchasePayInfo>>> GetTargetPurchaseOrders([FromQuery]PurchaseMonthPayDTO request)
        {
            return await _shopPurchaseService.GetTargetPurchaseOrders(request);
        }

        [HttpGet]
        public async Task<ApiResult<PurchaseOrderDTO>> GetPurchasePayInfo([FromQuery]PurchaseOrderViewDTO request)
        {
            return await _shopPurchaseService.GetPurchasePayInfo(request);
        }

        /// <summary>
        /// 采购退货--整单退
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> PurchaseReturn([FromBody]ApiRequest<PurchaseOrderViewDTO> request)
        {
            return await _shopPurchaseService.PurchaseReturn(request.Data);
        }

        /// <summary>
        /// 采购退货--部分退
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> PurchaseReturnPart([FromBody] ApiRequest<PurchaseOrderEditRequest> request)
        {
            return await _shopPurchaseService.PurchaseReturnPart(request.Data);
        }

        /// <summary>
        /// 采购退货--退货出库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]        
        public async Task<ApiResult<string>> ReleasePurchaseOccupyStock([FromBody]ApiRequest<PurchaseOrderViewDTO> request)
        {
            //return await _shopPurchaseService.ReleasePurchaseOccupyStock(request.Data);

            //txw20221206 简化库存，去掉批次和占库
            return await _shopPurchaseService.ReleasePurchaseOccupyStock_s(request.Data);

        }

        [HttpPost]
        public async Task<ApiResult<string>> UpdatePurchaseDeliveryCode([FromBody]ApiRequest<PurchaseOrderDTO> request)
        {
            return await _shopPurchaseService.UpdatePurchaseDeliveryCode(request.Data);
        }

    }

}
