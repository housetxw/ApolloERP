using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Common.Constant;
using Ae.ShopOrder.Service.Core.Interfaces;
using Ae.ShopOrder.Service.Core.Request.Sign;
using Ae.ShopOrder.Service.Core.Response.Sign;
using Ae.ShopOrder.Service.WebApi.Filters;

namespace Ae.ShopOrder.Service.Controllers
{
    /// <summary>
    /// 签收
    /// </summary>
    /// <summary>
    /// 订单查询服务
    /// </summary>
    [Route("[controller]/[action]")]
   // [Filter(nameof(SignController))]
    public class SignController
    {
        private readonly ISignService _signService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="signService"></param>
        public SignController(ISignService signService)
        {
            _signService = signService;
        }
        /// <summary>
        /// 验证待签收的订单/包裹号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<ValidateWaitingSignOrPackageResponse>> ValidateWaitingSignOrderOrPackage(
            [FromBody] ValidateWaitingSignOrderOrPackageRequest request)
        {
            return await _signService.GetValidateWaitingSign(request);
        }
        
        /// <summary>
        ///  得到今日签收列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<GetTodaySignPackageResponse>> GetTodaySignPackage(
            [FromBody]GetTodaySignPackageRequest request)
        {
            var geTodaySignPackage = await _signService.GetTodaySignPackage(request);
            if (geTodaySignPackage?.Items != null && geTodaySignPackage.Items.Any())
            {
                return new ApiPagedResult<GetTodaySignPackageResponse>()
                {
                    Code = ResultCode.Success,
                    Data = geTodaySignPackage
                };
            }
            else
            {
                return new ApiPagedResult<GetTodaySignPackageResponse>()
                {
                    Code = ResultCode.Success,
                    Message = CommonConstant.NullData,
                    Data = new ApiPagedResultData<GetTodaySignPackageResponse>()
                    {
                        TotalItems = 0,
                        Items = new List<GetTodaySignPackageResponse>()
                    }
                };
            }
        }

        /// <summary>
        /// 今日收货进度
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<GetTodayReceiveResponse>> GetTodayReceive([FromBody]TodayReceiveRequest request)
        {
            var getTodayReceiveResponse = await _signService.GetTodayReceiveTask(request);
            if (getTodayReceiveResponse?.Items?.Any() ?? false)
            {
                return new ApiResult<GetTodayReceiveResponse>()
                {
                    Code = ResultCode.Success,
                    Data = getTodayReceiveResponse
                };
            }
            else
            {
                return new ApiResult<GetTodayReceiveResponse>()
                {
                    Code = ResultCode.Success,
                    Data = default(GetTodayReceiveResponse),
                    Message = CommonConstant.NullData
                };
            }
        }

        /// <summary>
        /// 签收包裹
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<SignResponse>> UserSign([FromBody] SignRequest request)
        {
            if (request.SignType == 1)//门店向小仓下单签收
            {
                return await _signService.ShopToSamllwarehouseOrderUserSign(request);
            }
            else
            {
                return await _signService.UserSign(request);
            }
        }

      
    }
}
