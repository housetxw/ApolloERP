using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NLog.Filters;
using ApolloErp.Web.WebApi;
using Ae.FMS.Service.Core.Interfaces;
using Ae.FMS.Service.Core.Model;
using Ae.FMS.Service.Core.Model.AccountCheck;
using Ae.FMS.Service.Core.Request;
using Ae.FMS.Service.Core.Request.AccountCheck;

namespace Ae.FMS.Service.Controllers
{

    /// <summary>
    /// 采购单服务
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(AccountCheckController))]
    public class AccountCheckController : ControllerBase
    {
        public IAccountCheckService accountCheckService;
        private readonly string _constMessage = "无数据";
        public IConfiguration Configuration { get; }
        public AccountCheckController(IAccountCheckService accountCheckService, IConfiguration configuration)
        {
            this.accountCheckService = accountCheckService;
            this.Configuration = configuration;
        }


        /// <summary>
        /// 查询门店未对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountUnChecks([FromBody] GetAccountCheckRequest request)
        {
            var result = await accountCheckService.GetShopAccountUnChecks(request);

            ApiPagedResult<AccountCheckDTO> response = new ApiPagedResult<AccountCheckDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            if (response.Data == null && response.Code == ResultCode.Success)
            {
                response.Message = _constMessage;
                response.Code = ResultCode.SUCCESS_NOT_EXIST;
            }
            return response;
        }




        [HttpPost]
        /// <summary>
        /// 门店已对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountChecks([FromBody] GetAccountCheckRequest request)
        {
            var result = await accountCheckService.GeShoptAccountChecks(request);

            ApiPagedResult<AccountCheckDTO> response = new ApiPagedResult<AccountCheckDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            if (response.Data == null && response.Code == ResultCode.Success)
            {
                response.Message = _constMessage;
                response.Code = ResultCode.SUCCESS_NOT_EXIST;
            }
            return response;
        }

        [HttpPost]
        /// <summary>
        /// 门店已对账列表（包含总部已对账）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckDTO>> GetShopAccountChecksList([FromBody] GetAccountCheckRequest request)
        {
            var result = await accountCheckService.GeShoptAccountChecksList(request);

            ApiPagedResult<AccountCheckDTO> response = new ApiPagedResult<AccountCheckDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            if (response.Data == null && response.Code == ResultCode.Success)
            {
                response.Message = _constMessage;
                response.Code = ResultCode.SUCCESS_NOT_EXIST;
            }
            return response;
        }

        [HttpPost]
        /// <summary>
        /// 总部已对账列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<AccountCheckDTO>> GetRgAccountChecks([FromBody] GetAccountCheckRequest request)
        {
            var result = await accountCheckService.GeRgAccountChecks(request);

            ApiPagedResult<AccountCheckDTO> response = new ApiPagedResult<AccountCheckDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            if (response.Data == null && response.Code == ResultCode.Success)
            {
                response.Message = _constMessage;
                response.Code = ResultCode.SUCCESS_NOT_EXIST;
            }
            return response;
        }

        [HttpGet]
        public async Task<ApiResult<List<AccountCheckLogDTO>>> GetAccountCheckLogs([FromQuery] GetAccountCheckLogRequest request)
        {
            var result = await accountCheckService.GetAccountCheckLogs(request);
            return Result.Success(result, "查询成功!");
        }

        [HttpPost]
        public async Task<ApiResult> CreateAccountCheckLog([FromBody] CreateAccountCheckLogRequest request)
        {
            var result = await accountCheckService.CreateAccountCheckLog(request);
            return Result.Success(result);
        }


        /// <summary>
        /// 对账标记异常
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CreateAccountCheckException([FromBody] AccountCheckExceptionDTO request)
        {
            var result = await accountCheckService.CreateAccountCheckException(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 创建对账
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CreateAccountCheck([FromBody] CreateAccountCheckRequest request)
        {
            var result = await accountCheckService.CreateAccountCheck(request);

            return Result.Success(result);
        }

        [HttpPost]
        public async Task<ApiResult> UpdateRgAccountCheckResult([FromBody] RgAccountCheckConfirmRequest request)
        {
            var result = await accountCheckService.UpdateRgAccountCheckResult(request);
            return Result.Success(result);
        }

        [HttpPost]
        public async Task<ApiPagedResult<AccountCheckExceptionCollectDTO>> GetAccountCheckExceptionCollectList([FromBody] GetAccountCheckRequest request)
        {
            var result = await accountCheckService.GetAccountCheckExceptionCollectList(request);

            ApiPagedResult<AccountCheckExceptionCollectDTO> response = new ApiPagedResult<AccountCheckExceptionCollectDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            if (response.Data == null && response.Code == ResultCode.Success)
            {
                response.Message = _constMessage;
                response.Code = ResultCode.SUCCESS_NOT_EXIST;
            }
            return response;
        }

        /// <summary>
        /// 对账汇总记录(工场店）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<AccountCheckCollectDTO>> GetAccountCheckCollects([FromQuery] AccountCheckCollectRequest request)
        {
            var result = await accountCheckService.GetAccountCheckCollects(request);
            return result;
        }

        /// <summary>
        /// 对账详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<AccountCheckDTO>> GetAccountCheckDetail([FromQuery] GetAccountCheckDetailRequest request)
        {
            var result = await accountCheckService.GetAccountCheckDetail(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 申请提现
        /// </summary>
        /// <param name="requeset"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> RgAccountCheckWithdraw([FromBody] RgAccountCheckWithdrawRequeset requeset)
        {
            var result = await accountCheckService.RgAccountCheckWithdraw(requeset);
            return Result.Success(result);
        }

        /// <summary>
        /// 对账异常处理
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AccountCheckExceptionHandle([FromBody] AccountCheckExceptionHandleRequest request)
        {
            var result = await accountCheckService.AccountCheckExceptionHandle(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 对账结算
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> RgAccountCheckSettlement([FromBody] RgAccountCheckWithdrawRequeset request)
        {
            var result = await accountCheckService.RgAccountCheckSettlement(request);
            return Result.Success(result);
        }

        [HttpPost]
        public async Task<ApiResult> BatchHandelAccountCheckData([FromBody] RgAccountCheckWithdrawRequeset requeset)
        {
            var configHour = int.Parse(this.Configuration["AccountCheckSettlementHour"]);
            var result = await accountCheckService.BatchHandelAccountCheckData(configHour);
            return Result.Success();
        }

        /// <summary>
        /// 获取对账异常列表数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<AccountCheckExceptionCollectDTO>> GetAccountCheckExceptionMonitorList([FromBody] GetAccountCheckRequest request)
        {
            var result = await accountCheckService.GetAccountCheckExceptionMonitorList(request);

            ApiPagedResult<AccountCheckExceptionCollectDTO> response = new ApiPagedResult<AccountCheckExceptionCollectDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            if (response.Data == null && response.Code == ResultCode.Success)
            {
                response.Message = _constMessage;
                response.Code = ResultCode.SUCCESS_NOT_EXIST;
            }
            return response;
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
           
            return await accountCheckService.CalculationReconciliationFee(request);

        }

        /// <summary>
        /// 批量计算门店对账公司对账金额
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CalculationReconciliationFeeBatch(
            [FromBody] CreateAccountCheckRequest request)
        {
            var result = 0;
            if (request.Accounts.Any())
            {
                foreach (var item in request.Accounts)
                {
                    var apiResult = await accountCheckService.CalculationReconciliationFee(new CalculationReconciliationFeeRequest
                    {
                        OrderNo = item.OrderNo,
                        CreateBy = item.CreateBy
                    });
                    if (apiResult.Code == ResultCode.Success)
                    { result++; }
                }
            }

            return Result.Success(result);
        }

        /// <summary>
        /// 对账订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ReconciliationFeeDTO>>> GetReconciliationFees(
            [FromBody] GetReconciliationFeesRequest request)
        {
            return await accountCheckService.GetReconciliationFees(request);
        }

        /// <summary>
        /// 得到未对账订单的金额
        /// </summary>
        [HttpPost]
        public async Task<ApiResult<List<GetNoReconciliationAmountDO>>> GetNoReconciliationAmount([FromBody] GetNoReconciliationAmountRequest request)
        {
            return await accountCheckService.GetNoReconciliationAmount(request);
        }

        /// <summary>
        /// 采购单生成对账单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CalculationPurchaseReconciliationFee([FromBody] CalculationPurchaseReconciliationFeeRequest request)
        {
            ApiRequest<CalculationPurchaseReconciliationFeeRequest> newRequest = new ApiRequest<CalculationPurchaseReconciliationFeeRequest>()
            {
                Data = request
            };
            return await accountCheckService.CalculationPurchaseReconciliationFee(newRequest);
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


        /// <summary>
        /// 删除订单对账的记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> DeleteAccountCheck([FromBody] CalculationReconciliationFeeRequest request)
        {
            return await accountCheckService.DeleteAccountCheck(request);
        }


    }
}