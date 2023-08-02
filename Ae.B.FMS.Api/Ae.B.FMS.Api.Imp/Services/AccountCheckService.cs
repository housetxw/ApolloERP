using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Client.Interface;
using Ae.B.FMS.Api.Client.Request;
using Ae.B.FMS.Api.Core.Interfaces;
using Ae.B.FMS.Api.Core.Request;
using Ae.B.FMS.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Login.Auth;
using Ae.B.FMS.Api.Core.Model.AccountCheck;
using Ae.B.FMS.Api.Core.Request.AccountCheck;

namespace Ae.B.FMS.Api.Imp.Services
{
    public class AccountCheckService : IAccountCheckService
    {
        private readonly IMapper _mapper;
        private readonly IAccountCheckClient accountCheckClient;
        private readonly IIdentityService identityService;

        public AccountCheckService(IMapper mapper,
          IAccountCheckClient accountCheckClient, IIdentityService identityService)
        {
            _mapper = mapper;
            this.accountCheckClient = accountCheckClient;
            this.identityService = identityService;
        }

        public async Task<string> AccountCheckExceptionHandle(AccountCheckExceptionHandleRequest request)
        {
            request.UpdateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();
            AccountCheckExceptionHandleClientRequest clientRequest = _mapper.Map<AccountCheckExceptionHandleClientRequest>(request);

            var clientResponse = await accountCheckClient.AccountCheckExceptionHandle(clientRequest);

            var response = new ApiResult<string>();
            return clientResponse;
        }

        /// <summary>
        /// 创建门店对账记录 已失效!
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> CreateAccountCheck(CreateAccountCheckRequest request)
        {
            CreateAccountCheckClientRequest clientRequest = _mapper.Map<CreateAccountCheckClientRequest>(request);

            var clientResponse = await accountCheckClient.CreateAccountChecks(clientRequest);

            var response = new ApiResult<string>();
            return clientResponse;
        }

        /// <summary>
        /// 标记异常上报
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> CreateAccountCheckException(AccountCheckExceptionRequest request)
        {
            request.CreateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();
            request.ReportBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();
            AccountCheckExceptionClientRequest clientRequest = _mapper.Map<AccountCheckExceptionClientRequest>(request);

            var clientResponse = await accountCheckClient.CreateAccountCheckException(clientRequest);

            var response = new ApiResult<string>();
            return clientResponse;
        }

        /// <summary>
        /// 门店对账汇总列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckCollectResponse>> GetAccountCheckCollects(GetAccountCheckCollectRequest request)
        {
            GetAccountCheckCollectClientRequest clientRequest = _mapper.Map<GetAccountCheckCollectClientRequest>(request);

            var clientResponse = await accountCheckClient.GetAccountCheckCollects(clientRequest);

            var res = new ApiPagedResult<AccountCheckCollectResponse>();

            var response = _mapper.Map<ApiPagedResultData<AccountCheckCollectResponse>>(clientResponse.Data);
            if (response.Items.Any())
            {
                res.Data = response;
            }
            else
            {
                res.Data = new ApiPagedResultData<AccountCheckCollectResponse>();
            }
            res.Code = ResultCode.Success;
            res.Message = "查询成功!";
            return res;
        }

        /// <summary>
        /// 查询对账汇总异常列表记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckExceptionCollectResponse>> GetAccountCheckExceptionCollectList(GetAccountCheckRequest request)
        {

            if (request.InstallTime != null && request.InstallTime.Any())
            {
                request.StartTime = Convert.ToDateTime(request.InstallTime[0]);
                request.EndTime = Convert.ToDateTime(request.InstallTime[1]);
            }

            GetAccountCheckClientRequest clientRequest = _mapper.Map<GetAccountCheckClientRequest>(request);

            var clientResponse = await accountCheckClient.GetAccountCheckExceptionCollectList(clientRequest);

            var res = new ApiPagedResult<AccountCheckExceptionCollectResponse>();

            var response = _mapper.Map<ApiPagedResultData<AccountCheckExceptionCollectResponse>>(clientResponse.Data);
            if (response.Items.Any())
            {
                res.Data = response;
            }
            else
            {
                res.Data = new ApiPagedResultData<AccountCheckExceptionCollectResponse>();
            }
            res.Code = ResultCode.Success;
            res.Message = "查询成功!";
            return res;
        }

        public async Task<ApiPagedResult<AccountCheckExceptionCollectResponse>> GetAccountCheckExceptionMonitorList(GetAccountCheckRequest request)
        {
            if (request.InstallTime != null && request.InstallTime.Any())
            {
                request.StartTime = Convert.ToDateTime(request.InstallTime[0]);
                request.EndTime = Convert.ToDateTime(request.InstallTime[1]);
            }

            if (request.LocationIdList != null && request.LocationIdList.Any())
            {
                request.LocationIds = string.Join(',', request.LocationIdList).TrimEnd(',');
            }

            GetAccountCheckClientRequest clientRequest = _mapper.Map<GetAccountCheckClientRequest>(request);

            var clientResponse = await accountCheckClient.GetAccountCheckExceptionMonitorList(clientRequest);

            var res = new ApiPagedResult<AccountCheckExceptionCollectResponse>();

            var response = _mapper.Map<ApiPagedResultData<AccountCheckExceptionCollectResponse>>(clientResponse.Data);
            if (response.Items.Any())
            {
                res.Data = response;
            }
            else
            {
                res.Data = new ApiPagedResultData<AccountCheckExceptionCollectResponse>();
            }
            res.Code = ResultCode.Success;
            res.Message = "查询成功!";
            return res;

        }

        /// <summary>
        /// 操作历史
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<AccountCheckLogResponse>> GetAccountCheckLogs(AccountCheckLogRequest request)
        {
            AccountCheckLogClientRequest clientRequest = _mapper.Map<AccountCheckLogClientRequest>(request);

            var clientResponse = await accountCheckClient.GetAccountCheckLogs(clientRequest);

            var response = _mapper.Map<List<AccountCheckLogResponse>>(clientResponse);
            return response;
        }



        /// <summary>
        /// 总部已对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckResponse>> GetRgAccountChecks(GetAccountCheckRequest request)
        {
            if (request.InstallTime != null && request.InstallTime.Any())
            {
                request.StartTime = Convert.ToDateTime(request.InstallTime[0]);
                request.EndTime = Convert.ToDateTime(request.InstallTime[1]);
            }
            GetAccountCheckClientRequest clientRequest = _mapper.Map<GetAccountCheckClientRequest>(request);

            var clientResponse = await accountCheckClient.GetRgAccountChecks(clientRequest);

            ApiPagedResult<AccountCheckResponse> res = new ApiPagedResult<AccountCheckResponse>();

            var response = _mapper.Map<ApiPagedResultData<AccountCheckResponse>>(clientResponse.Data);
            if (response.Items.Any())
            {
                res.Data = response;
            }
            else
            {
                res.Data = new ApiPagedResultData<AccountCheckResponse>();
            }
            res.Code = ResultCode.Success;
            res.Message = "查询成功!";
            return res;
        }

        /// <summary>
        /// 门店已对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckResponse>> GetShopAccountChecks(GetAccountCheckRequest request)
        {
            if (request.InstallTime != null && request.InstallTime.Any())
            {
                request.StartTime = Convert.ToDateTime(request.InstallTime[0]);
                request.EndTime = Convert.ToDateTime(request.InstallTime[1]);
            }
            GetAccountCheckClientRequest clientRequest = _mapper.Map<GetAccountCheckClientRequest>(request);

            var clientResponse = await accountCheckClient.GetShopAccountChecks(clientRequest);

            ApiPagedResult<AccountCheckResponse> res = new ApiPagedResult<AccountCheckResponse>();

            var response = _mapper.Map<ApiPagedResultData<AccountCheckResponse>>(clientResponse.Data);

            if (response.Items.Any())
            {
                res.Data = response;
            }
            else
            {
                res.Data = new ApiPagedResultData<AccountCheckResponse>();
            }
            res.Code = ResultCode.Success;
            res.Message = "查询成功!";
            return res;
        }

        /// <summary>
        /// 门店未对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckResponse>> GetShopAccountUnChecks(GetAccountCheckRequest request)
        {
            if (request.InstallTime != null && request.InstallTime.Any())
            {
                request.StartTime = Convert.ToDateTime(request.InstallTime[0]);
                request.EndTime = Convert.ToDateTime(request.InstallTime[1]);
            }
            GetAccountCheckClientRequest clientRequest = _mapper.Map<GetAccountCheckClientRequest>(request);

            var clientResponse = await accountCheckClient.GetShopAccountUnChecks(clientRequest);

            ApiPagedResult<AccountCheckResponse> res = new ApiPagedResult<AccountCheckResponse>();

            var response = _mapper.Map<ApiPagedResultData<AccountCheckResponse>>(clientResponse.Data);

            if (response.Items.Any())
            {
                res.Data = response;
            }
            else
            {
                res.Data = new ApiPagedResultData<AccountCheckResponse>();
            }

            res.Code = ResultCode.Success;
            res.Message = "查询成功!";
            return res;
        }

        /// <summary>
        /// 申请提现
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> RgAccountCheckWithdraw(RgAccountCheckWithdrawRequeset request)
        {
            request.UpdateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();

            RgAccountCheckWithdrawClientRequeset clientRequest = _mapper.Map<RgAccountCheckWithdrawClientRequeset>(request);
            var clientResponse = await accountCheckClient.RgAccountCheckWithdraw(clientRequest);

            var response = new ApiResult<string>();
            return clientResponse;
        }

        /// <summary>
        /// 更新总部对账结果
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> UpdateRgAccountCheckResult(RgAccountCheckConfirmRequest request)
        {
            request.UpdateBy = this.identityService.GetUserId() + "@" + this.identityService.GetUserName();
            RgAccountCheckConfirmClientRequest clientRequest = _mapper.Map<RgAccountCheckConfirmClientRequest>(request);

            var clientResponse = await accountCheckClient.UpdateRgAccountCheckResult(clientRequest);

            var response = new ApiResult<string>();
            return clientResponse;
        }

        public async Task<ApiResult<string>> SavePurchaseAccountRecord(ApiRequest<SavePurchaseAccountRecordRequest> request)
        {
            request.Data.CreateUser = this.identityService.GetUserName();
            return await accountCheckClient.SavePurchaseAccountRecord(request);
        }

        public async Task<ApiResult<string>> SavePurchaseExceptionAccount(ApiRequest<SavePurchaseExceptionAccountRequest> request)
        {
            request.Data.ReportBy = this.identityService.GetUserName();
            return await accountCheckClient.SavePurchaseExceptionAccount(request);
            
        }
        public async Task<ApiPagedResult<PurchaseAccountCheckDTO>> GetPurchaseAccountList(GetPurchaseAccountListRequest request)
        {
            int.TryParse(identityService.GetOrganizationId(), out var shopId);
            request.ShopId = shopId;
            return await accountCheckClient.GetPurchaseAccountList(request);
        }

    }
}
