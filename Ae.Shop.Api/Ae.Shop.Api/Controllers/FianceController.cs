using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog.Filters;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.Fiance;
using Ae.Shop.Api.Core.Request.Fiance;
using Ae.Shop.Api.Core.Response.Fiance;

namespace Ae.Shop.Api.Controllers
{
    /// <summary>
    /// 财务
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(FianceController))]
    public class FianceController : ControllerBase
    {
        private IFianceService _fianceService;

        public FianceController(IFianceService fianceService)
        {
            _fianceService = fianceService;
        }

        /// <summary>
        /// 核对账单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CreateAccountCheck([FromBody]ApiRequest<CreateAccountCheckClientRequest> request)
        {
            return await _fianceService.CreateAccountCheck(request.Data);
        }

        /// <summary>
        /// 异常订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<AccountCheckExceptionCollectDTO>> GetAccountCheckExceptionCollectList([FromBody]ApiRequest<GetAccountCheckRequest> request)
        {
            return await _fianceService.GetAccountCheckExceptionCollectList(request.Data);
        }

        /// <summary>
        /// 门店已对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountChecks([FromBody]ApiRequest<GetAccountCheckRequest> request)
        {
            return await _fianceService.GetShopAccountChecks(request.Data);
        }

        /// <summary>
        /// 已对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<AccountCheckDTO>> GetRgAccountChecks([FromBody] ApiRequest<GetAccountCheckRequest> request)
        {
            return await _fianceService.GetRgAccountChecks(request.Data);
        }

        /// <summary>
        /// 查询门店未对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountUnChecks([FromBody]ApiRequest<GetAccountCheckRequest> request)
        {
            return await _fianceService.GetShopAccountUnChecks(request.Data);
        }


        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<AccountCheckLogDTO>>> GetAccountCheckLogs([FromQuery]GetAccountCheckLogRequest request)
        {
            return await _fianceService.GetAccountCheckLogs(request);
        }


        /// <summary>
        /// 得到提现记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetWithdrawalRecordListResponse>> GetWithdrawalRecordList([FromQuery]GetWithdrawalRecordListRequest request)
        {
            return await _fianceService.GetWithdrawalRecordList(request);
        }


        /// <summary>
        /// 得到开始提现申请的信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetWithdrawalApplyResponse>> GetWithdrawalApply([FromQuery]GetWithdrawalApplyRequest request)
        {
            return await _fianceService.GetWithdrawalApply(request);
        }

        /// <summary>
        /// 结算单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetWithdrawalOrderRecordListResponse>> GetWithdrawalOrderRecordList([FromQuery]GetWithdrawalOrderRecordListRequest request)
        {
            return await _fianceService.GetWithdrawalOrderRecordList(request);
        }

        /// <summary>
        /// 发起提现申请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<SubmitWithdrawalApplyResponse>> SubmitWithdrawalApply([FromBody]ApiRequest<SubmitWithdrawalApplyRequest> request)
        {
            return await _fianceService.SubmitWithdrawalApply(request.Data);
        }

        /// <summary>
        /// 计算门店对账公司对账金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> CalculationReconciliationFee(
            [FromQuery] CalculationReconciliationFeeRequest request)
        {
            return await _fianceService.CalculationReconciliationFee(request);
        }

        /// <summary>
        /// 批量计算门店对账公司对账金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CalculationReconciliationFeeBatch(
            [FromBody] ApiRequest<CreateAccountCheckClientRequest> request)
        {
            return await _fianceService.CalculationReconciliationFeeBatch(request.Data);
        }
    }
}