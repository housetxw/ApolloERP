using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Core.Interfaces;
using Ae.B.FMS.Api.Core.Model;
using Ae.B.FMS.Api.Core.Model.Settlement;
using Ae.B.FMS.Api.Core.Request;
using Ae.B.FMS.Api.Core.Request.Settlement;
using Ae.B.FMS.Api.Core.Response;
using Ae.B.FMS.Api.Filters;

namespace Ae.B.FMS.Api.Controllers
{

    [Route("[controller]/[action]")]
    //[Filter(nameof(SettlementController))]
   
    public class SettlementController : ControllerBase
    {
        private readonly ISettlementService settlementService;

        public SettlementController(ISettlementService settlementService)
        {
            this.settlementService = settlementService;
        }
        /// <summary>
        /// 结算列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<GetSettlementListResponse>> GetSettlementList([FromBody]ApiRequest<GetSettlementListRequest> request)
        {
            var result = await settlementService.GetSettlementList(request.Data);
            result.Message = "查询成功!";
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 付款处理
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SaveSettlementReview([FromBody]ApiRequest<SaveSettlementReviewRequest> request)
        {
            var result = await settlementService.SaveSettlementReview(request.Data);
            result.Message = "操作成功!";
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 结算处理
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetSettlementDetailResponse>>> GetSettlementDetail([FromQuery]GetSettlementDetailRequest request)
        {
            var result = await settlementService.GetSettlementDetail(request);
            return Result.Success(result);
        }

        [HttpGet]
        public async Task<ApiResult<SettlementPayReviewResponse>> SettlementPayReviewDO([FromQuery]GetSettlementDetailRequest request)
        {
            var result = await settlementService.SettlementPayReviewDO(request);
            return await Task.FromResult(result);
        }


        /// <summary>
        /// 得到采购结算单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<PurchaseSettlementBatchDTO>> GetPurchaseSettlementList([FromQuery] GetPurchaseSettlementListRequest request)
        {
            return await settlementService.GetPurchaseSettlementList(request);
        }

        /// <summary>
        /// 得到采购结算单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<PurchaseSettlementDetailDTO>>> GetPurchaseSettlementDetail([FromQuery] GetPurchaseSettlementDetailRequest request)
        {
            return await settlementService.GetPurchaseSettlementDetail(request);
        }

        /// <summary>
        /// 生成结算单订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> SavePurchaseSettlementOrder([FromBody] ApiRequest<SavePurchaseSettlementOrderRequest> request)
        {
            return await settlementService.SavePurchaseSettlementOrder(request);
        }

        /// <summary>
        /// 审核结算单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> SavePurchaseSettlementReview([FromBody] ApiRequest<SavePurchaseSettlementReviewRequest> request)
        {
            return await settlementService.SavePurchaseSettlementReview(request);
        }


    }
}