using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Sql.Internal;
using ApolloErp.Web.WebApi;
using Ae.FMS.Service.Common.Constant;
using Ae.FMS.Service.Core.Interfaces;
using Ae.FMS.Service.Core.Model;
using Ae.FMS.Service.Core.Model.AccountCheck;
using Ae.FMS.Service.Core.Request.Settlement;
using Ae.FMS.Service.Core.Response;
using Ae.FMS.Service.Core.Response.Settlement;
using Ae.FMS.Service.Filters;

namespace Ae.FMS.Service.Controllers
{
    /// <summary>
    /// 结算
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(SettlementController))]
    public class SettlementController : Controller
    {
        public ISettlementService _settlementService;
        public SettlementController(ISettlementService settlementService)
        {
            _settlementService = settlementService;
        }
        /// <summary>
        /// 门店账户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetAccountInfoResponse>> GetAccountInfo([FromQuery] GetAccountInfoRequest request)
        {
            var accountResponse = await _settlementService.GetAccountInfo(request);
            if (accountResponse != null)
                return new ApiResult<GetAccountInfoResponse>()
                {
                    Code = ResultCode.Success,
                    Data = accountResponse
                };
            return new ApiResult<GetAccountInfoResponse>()
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.ErrorOccured
            };
        }

        /// <summary>
        /// 得到开始提现申请的信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetWithdrawalApplyResponse>> GetWithdrawalApply([FromQuery] GetWithdrawalApplyRequest request)
        {
            var getWithdrawalApplyResponse = await _settlementService.GetWithdrawalApply(request);
            if (getWithdrawalApplyResponse != null)
                return new ApiResult<GetWithdrawalApplyResponse>()
                {
                    Code = ResultCode.Success,
                    Data = getWithdrawalApplyResponse,
                };

            return new ApiResult<GetWithdrawalApplyResponse>()
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.ErrorOccured
            };
        }

        /// <summary>
        /// 得到提现记录列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetWithdrawalRecordListResponse>> GetWithdrawalRecordList(
            [FromQuery] GetWithdrawalRecordListRequest request)
        {
            var geWithdrawalRecordListResponse = await _settlementService.GetWithdrawalRecordList(request);
            if (geWithdrawalRecordListResponse != null)
                return new ApiPagedResult<GetWithdrawalRecordListResponse>
                {
                    Code = ResultCode.Success,
                    Data = geWithdrawalRecordListResponse
                };
            return new ApiPagedResult<GetWithdrawalRecordListResponse>
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.ErrorOccured
            };
        }

        /// <summary>
        /// 得到订单提现记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetWithdrawalOrderRecordListResponse>> GetWithdrawalOrderRecordList([FromQuery] GetWithdrawalOrderRecordListRequest request)
        {
            var getWithdrawalOrderRecordList = await _settlementService.GetWithdrawalOrderRecordList(request);
            if (getWithdrawalOrderRecordList != null)
                return new ApiPagedResult<GetWithdrawalOrderRecordListResponse>
                {
                    Code = ResultCode.Success,
                    Data = getWithdrawalOrderRecordList
                };
            return new ApiPagedResult<GetWithdrawalOrderRecordListResponse>
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.ErrorOccured
            };
        }

        /// <summary>
        /// 得到财务账单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<GetFianceReconciliationStatusListResponse>> GetFianceReconciliationStatusList([FromBody] GetFianceReconciliationStatusListRequest request)
        {
            var getFianceReconciliationStatusListResponse = await _settlementService.GetFianceReconciliationStatusList(request);
            if (getFianceReconciliationStatusListResponse != null)
                return new ApiPagedResult<GetFianceReconciliationStatusListResponse>
                {
                    Code = ResultCode.Success,
                    Data = getFianceReconciliationStatusListResponse
                };
            return new ApiPagedResult<GetFianceReconciliationStatusListResponse>
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.ErrorOccured
            };
        }

        /// <summary>
        /// 得到财务账单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetFianceBillDetailResponse>> GetFianceBillDetail([FromQuery] GetFianceBillDetailRequest request)
        {
            var getFianceBillDetailResponse = await _settlementService.GetFianceBillDetail(request);
            if (getFianceBillDetailResponse != null)
                return new ApiResult<GetFianceBillDetailResponse>
                {
                    Code = ResultCode.Success,
                    Data = getFianceBillDetailResponse
                };
            return new ApiResult<GetFianceBillDetailResponse>
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.ErrorOccured
            };
        }

        /// <summary>
        /// 发起提现申请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<SubmitWithdrawalApplyResponse>> SubmitWithdrawalApply(
            [FromBody] SubmitWithdrawalApplyRequest request)
        {
            return await _settlementService.SubmitWithdrawalApply(request);
        }

        /// <summary>
        ///// 得到对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<GetSettlementListResponse>> GetSettlementList([FromBody] GetSettlementListRequest request)
        {
            var getSettlementList = await _settlementService.GetSettlementList(request);
            return new ApiPagedResult<GetSettlementListResponse>()
            {
                Code = ResultCode.Success,
                Data = getSettlementList
            };
        }

        /// <summary>
        /// 结算处理
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SaveSettlementReview([FromBody] SaveSettlementReviewRequest request)
        {
            var saveSettlementReviewResponse = await _settlementService.SaveSettlementReview(request);
            if (saveSettlementReviewResponse > 0)
                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = "保存成功"
                };
            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.ErrorOccured
            };
        }

        /// <summary>
        /// 获得结算单明细
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetSettlementDetailResponse>>> GetSettlementDetail([FromQuery] GetSettlementDetailRequest request)
        {
            var result = await _settlementService.GetSettlementDetail(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 得到对账信息表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<AccountCheckDTO>> GetAccountCheckInfo([FromQuery] GetFianceBillDetailRequest request)
        {
            var getAccountCheckInfo = await _settlementService.GetAccountCheckInfo(request);
            return new ApiResult<AccountCheckDTO>()
            {
                Code = ResultCode.Success,
                Data = getAccountCheckInfo
            };
        }

        [HttpGet]
        public async Task<ApiResult<SettlementPayReviewDTO>> SettlementPayReviewDO([FromQuery] SettlementPayReviewDTO request)
        {
            var result = await _settlementService.SettlementPayReviewDO(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 得到采购结算单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<PurchaseSettlementBatchDTO>> GetPurchaseSettlementList([FromQuery] GetPurchaseSettlementListRequest request)
        {
            var result = await _settlementService.GetPurchaseSettlementList(request);
            return result;
        }

        /// <summary>
        /// 得到采购结算单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<PurchaseSettlementDetailDTO>>> GetPurchaseSettlementDetail([FromQuery] GetPurchaseSettlementDetailRequest request)
        {
            return await _settlementService.GetPurchaseSettlementDetail(request);
        }

        /// <summary>
        /// 生成结算单订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> SavePurchaseSettlementOrder([FromBody] ApiRequest<SavePurchaseSettlementOrderRequest> request)
        {
            return await _settlementService.SavePurchaseSettlementOrder(request);
        }

        /// <summary>
        /// 审核结算单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> SavePurchaseSettlementReview([FromBody] ApiRequest<SavePurchaseSettlementReviewRequest> request)
        {
            return await _settlementService.SavePurchaseSettlementReview(request);
        }


    }
}