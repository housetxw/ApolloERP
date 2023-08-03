using AutoMapper;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Clients.Fiance;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.Fiance;
using Ae.Shop.Api.Core.Request.Fiance;
using Ae.Shop.Api.Core.Response.Fiance;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Imp.Services
{
    public class FianceService:IFianceService
    {
        private readonly ApolloErpLogger<FianceService> _logger;
        private readonly IFianceClient _fianceClient;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public FianceService(ApolloErpLogger<FianceService> logger,
          IFianceClient fianceClient, IMapper mapper,
          IConfiguration configuration, IIdentityService identityService)
        {
            _configuration = configuration;
            _logger = logger;
            _fianceClient = fianceClient;
            _mapper = mapper;
            _identityService = identityService;
        }

        /// <summary>
        /// 核对账单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> CreateAccountCheck(CreateAccountCheckClientRequest request)
        {
            var organizationId = _identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);

            request.Accounts?.ForEach(_ =>
            {
                _.LocationId = shopId;
                _.LocationName = _identityService.GetOrganizationId();
            });
            return await _fianceClient.CreateAccountCheck(request);
        }

        /// <summary>
        /// 异常订单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckExceptionCollectDTO>> GetAccountCheckExceptionCollectList(GetAccountCheckRequest request)
        {
            var organizationId = _identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            request.LocationId = (int)shopId;
            if (string.IsNullOrEmpty(request.StartTime))
            {
                request.StartTime = (new DateTime(1900, 1, 1)).ToString();
            }
            if (string.IsNullOrEmpty(request.EndTime))
            {
                request.EndTime = (new DateTime(1900, 1, 1)).ToString();
            }
            return await _fianceClient.GetAccountCheckExceptionCollectList(request);
        }

        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<AccountCheckLogDTO>>> GetAccountCheckLogs(GetAccountCheckLogRequest request)
        {
            return await _fianceClient.GetAccountCheckLogs(request);
        }

        /// <summary>
        /// 门店已对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountChecks(GetAccountCheckRequest request)
        {
            var organizationId = _identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            request.LocationId = (int)shopId;
            if (string.IsNullOrEmpty(request.StartTime))
            {
                request.StartTime = (new DateTime(1900, 1, 1)).ToString();
            }
            if (string.IsNullOrEmpty(request.EndTime))
            {
                request.EndTime = (new DateTime(1900, 1, 1)).ToString();
            }
            return await _fianceClient.GetShopAccountChecks(request);
        }

        /// <summary>
        /// 已对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckDTO>> GetRgAccountChecks(GetAccountCheckRequest request)
        {
            var organizationId = _identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            request.LocationId = (int)shopId;
            if (string.IsNullOrEmpty(request.StartTime))
            {
                request.StartTime = (new DateTime(1900, 1, 1)).ToString();
            }
            if (string.IsNullOrEmpty(request.EndTime))
            {
                request.EndTime = (new DateTime(1900, 1, 1)).ToString();
            }
            return await _fianceClient.GetRgAccountChecks(request);
        }

        /// <summary>
        /// 查询门店未对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountUnChecks(GetAccountCheckRequest request)
        {
            var organizationId = _identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            request.LocationId = (int)shopId;
            if (string.IsNullOrEmpty(request.StartTime))
            {
                request.StartTime = (new DateTime(1900, 1, 1)).ToString();
            }
            if (string.IsNullOrEmpty(request.EndTime))
            {
                request.EndTime = (new DateTime(1900, 1, 1)).ToString();
            }
            return await _fianceClient.GetShopAccountUnChecks(request);
        }

        /// <summary>
        /// 得到开始提现申请的信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetWithdrawalApplyResponse>> GetWithdrawalApply(GetWithdrawalApplyRequest request)
        {
            var organizationId = _identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            request.ShopId = (int)shopId;
            return await _fianceClient.GetWithdrawalApply(request);
        }

        /// <summary>
        /// 得到提现记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<GetWithdrawalRecordListResponse>> GetWithdrawalRecordList(GetWithdrawalRecordListRequest request)
        {
            var organizationId = _identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            request.ShopId = (int)shopId;
            return await _fianceClient.GetWithdrawalRecordList(request);
        }

        /// <summary>
        /// 结算单详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public  async Task<ApiPagedResult<GetWithdrawalOrderRecordListResponse>> GetWithdrawalOrderRecordList(GetWithdrawalOrderRecordListRequest request)
        {
            var organizationId = _identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            request.ShopId = (int)shopId;
            return await _fianceClient.GetWithdrawalOrderRecordList(request);
        }

        /// <summary>
        /// 发起提现申请
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<SubmitWithdrawalApplyResponse>> SubmitWithdrawalApply(SubmitWithdrawalApplyRequest request)
        {
            var organizationId = _identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            request.ShopId = (int)shopId;
            request.ApplyUser = _identityService?.GetUserName() ?? "System";
            return await _fianceClient.SubmitWithdrawalApply(request);
        }

        public async Task<ApiResult> CalculationReconciliationFee(CalculationReconciliationFeeRequest request)
        {
            request.CreateBy = _identityService?.GetUserName() ?? "System";
            return await _fianceClient.CalculationReconciliationFee(request);
        }

        public async Task<ApiResult> CalculationReconciliationFeeBatch(CreateAccountCheckClientRequest request)
        {
            request.Accounts?.ForEach(_ =>
            {
                _.CreateBy = _identityService?.GetUserName() ?? "System";
            });
            return await _fianceClient.CalculationReconciliationFeeBatch(request);
        }
    }
}
