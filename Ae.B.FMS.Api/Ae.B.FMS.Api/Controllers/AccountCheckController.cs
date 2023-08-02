using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Core.Interfaces;
using Ae.B.FMS.Api.Core.Model;
using Ae.B.FMS.Api.Core.Model.AccountCheck;
using Ae.B.FMS.Api.Core.Request;
using Ae.B.FMS.Api.Core.Request.AccountCheck;
using Ae.B.FMS.Api.Core.Response;
using Ae.B.FMS.Api.Filters;
namespace Ae.B.FMS.Api.Controllers
{

    /// <summary>
    ///财务相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(AccountCheckController))]
 
    public class AccountCheckController : ControllerBase
    {
        private readonly IAccountCheckService accountCheckService;
        private readonly IBasicDataService basicDataService;

        public AccountCheckController(IAccountCheckService accountCheckService, IBasicDataService basicDataService) {
            this.accountCheckService = accountCheckService;
            this.basicDataService = basicDataService;
        }
        [HttpPost]
        public async Task<ApiPagedResult<AccountCheckResponse>> GetShopAccountUnChecks([FromBody]ApiRequest<GetAccountCheckRequest> request)
        {
            var result = await accountCheckService.GetShopAccountUnChecks(request.Data);
            return await Task.FromResult(result);
        }


        [HttpPost]
        public async Task<ApiPagedResult<AccountCheckResponse>> GetRgAccountChecks([FromBody]ApiRequest<GetAccountCheckRequest> request)
        {
            var result = await accountCheckService.GetRgAccountChecks(request.Data);
            return await Task.FromResult(result);
        }

        [HttpPost]
        public async Task<ApiPagedResult<AccountCheckResponse>> GetShopAccountChecks([FromBody]ApiRequest<GetAccountCheckRequest> request)
        {
            var result = await accountCheckService.GetShopAccountChecks(request.Data);
            return await Task.FromResult(result);
        }

        [HttpGet]
        public async Task<ApiResult<List<AccountCheckLogResponse>>> GetAccountCheckLogs([FromQuery]AccountCheckLogRequest request)
        {
            var result = await accountCheckService.GetAccountCheckLogs(request);
            return Result.Success(result);
        }

        [HttpPost]
        public async Task<ApiResult<string>> CreateAccountCheckException([FromBody]ApiRequest<AccountCheckExceptionRequest> request)
        {
            var result = await accountCheckService.CreateAccountCheckException(request.Data);

            ApiResult<string> apiResult = new ApiResult<string>();
            apiResult.Code = ResultCode.Success;
            apiResult.Data = result;
            apiResult.Message = "标记异常成功!";
            return apiResult;
        }

        /// <summary>
        /// TODO:已失效
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<ApiResult<string>> CreateAccountCheck([FromBody]ApiRequest<CreateAccountCheckRequest> request)
        //{
        //    var result = await accountCheckService.CreateAccountCheck(request.Data);

        //    ApiResult<string> apiResult = new ApiResult<string>();
        //    apiResult.Code = ResultCode.Success;
        //    apiResult.Data = result;
        //    return apiResult;
        //}

        [HttpGet]
        public async Task<ApiResult<List<BasicInfoDTO>>> GetBasicInfoList([FromQuery]BasicInfoRequest request) {
            var basicInfoList = new List<BasicInfoDTO>();
            switch (request.RequestType)
            {
                case 1:
                  
                    break;
                case 2:
                    basicInfoList.Add(new BasicInfoDTO { 
                        Key ="其他",
                        Value ="其他"
                    });

                    basicInfoList.Add(new BasicInfoDTO
                    {
                        Key = "需要给门店补服务费",
                        Value = "需要给门店补服务费"
                    });

                    basicInfoList.Add(new BasicInfoDTO
                    {
                        Key = "订单服务费计算错误",
                        Value = "订单服务费计算错误"
                    });
                    break;
                default:
                    break;
            }
            return Result.Success(basicInfoList);
        }



        [HttpPost]
        public async Task<ApiResult<string>> UpdateRgAccountCheckResult([FromBody]ApiRequest<RgAccountCheckConfirmRequest> request)
        {
            var result = await accountCheckService.UpdateRgAccountCheckResult(request.Data);

            ApiResult<string> apiResult = new ApiResult<string>();
            apiResult.Code = ResultCode.Success;
            apiResult.Data = result;
            apiResult.Message = "核对成功!";
            return apiResult;
        }

        [HttpPost]
        public async Task<ApiPagedResult<AccountCheckExceptionCollectResponse>> GetAccountCheckExceptionCollectList([FromBody]ApiRequest<GetAccountCheckRequest> request)
        {
            var result = await accountCheckService.GetAccountCheckExceptionCollectList(request.Data);
            return await Task.FromResult(result);
        }
        
        [HttpGet]
        public async Task<ApiPagedResult<AccountCheckCollectResponse>> GetAccountCheckCollects([FromQuery]GetAccountCheckCollectRequest request)
        {
            var result = await accountCheckService.GetAccountCheckCollects(request);
            return await Task.FromResult(result);
        }

        [HttpPost]
        public async Task<ApiResult<string>> RgAccountCheckWithdraw([FromBody]ApiRequest<RgAccountCheckWithdrawRequeset> request)
        {
            var result = await accountCheckService.RgAccountCheckWithdraw(request.Data);

            ApiResult<string> apiResult = new ApiResult<string>();
            apiResult.Code = ResultCode.Success;
            apiResult.Data = result;
            apiResult.Message = "申请提现成功!";
            return apiResult;
        }

        /// <summary>
        /// 查询省市区三级信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetRegionChinaListByRegionIdResponse>>> GetRegionChinaListByRegionId([FromQuery]GetRegionChinaListByRegionIdRequest request)
        {
            var result = await basicDataService.GetRegionChinaListByRegionId(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 对账异常处理
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> AccountCheckExceptionHandle([FromBody]ApiRequest<AccountCheckExceptionHandleRequest> request)
        {
            var result = await accountCheckService.AccountCheckExceptionHandle(request.Data);
            ApiResult<string> apiResult = new ApiResult<string>();
            apiResult.Code = ResultCode.Success;
            apiResult.Data = result;
            apiResult.Message = "对账异常处理成功!";
            return apiResult;
        }

        [HttpPost]
        public async Task<ApiPagedResult<AccountCheckExceptionCollectResponse>> GetAccountCheckExceptionMonitorList([FromBody]ApiRequest<GetAccountCheckRequest> request)
        {
            var result = await accountCheckService.GetAccountCheckExceptionMonitorList(request.Data);
            return await Task.FromResult(result);
        }


        /// <summary>
        /// 得到采购对账单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<PurchaseAccountCheckDTO>> GetPurchaseAccountList([FromQuery] GetPurchaseAccountListRequest request)
        {
            return await accountCheckService.GetPurchaseAccountList(request);
        }

        /// <summary>
        /// 生成采购已对账的记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> SavePurchaseAccountRecord([FromBody] ApiRequest<SavePurchaseAccountRecordRequest> request)
        {
            return await accountCheckService.SavePurchaseAccountRecord(request);
        }

        /// <summary>
        /// 生成采购异常对账的记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> SavePurchaseExceptionAccount([FromBody] ApiRequest<SavePurchaseExceptionAccountRequest> request)
        {
            return await accountCheckService.SavePurchaseExceptionAccount(request);
        }
    }
}