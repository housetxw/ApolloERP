using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Reserve;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Core.Response.Reserve;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 预约、到店 相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ReceiveController))]
    public class ReceiveController : ControllerBase
    {
        private IReceiveService receiveService;
        private readonly ApolloErpLogger<ReceiveController> _logger;

        public ReceiveController(IReceiveService receiveService,
            ApolloErpLogger<ReceiveController> logger
            )
        {
            this.receiveService = receiveService;
            _logger = logger;
        }


        /// <summary>
        /// 判断我的预约跳转类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<JudgeMyReserveResponse>> JudgeMyReserve([FromQuery] BaseGetRequest request) 
        {
            var result = await receiveService.JudgeMyReserve();
            return Result.Success(result);
        }


        /// <summary>
        /// 获取可预约订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<CanReserveOrderVO>> CanReserveOrderList([FromQuery] BasePageRequest request)
        {
            var result = await receiveService.CanReserveOrderList(request);
            ApiPagedResult<CanReserveOrderVO> response = new ApiPagedResult<CanReserveOrderVO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }

        /// <summary>
        /// 初始化预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ReserveInitialResponse>> ReserveInitial([FromQuery] ReserveInitialRequest request)
        {
            var result = await receiveService.ReserveInitial(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 新增预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddReserve([FromBody] ApiRequest<AddReserveRequest> request)
        {
            _logger.Info($"AddReserve 请求参数：{JsonConvert.SerializeObject(request)}");
            return await receiveService.AddShopReserveAsync(request.Data);
        }


        /// <summary>
        /// 新增预约V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddShopReserveV3([FromBody] ApiRequest<AddReserveV3Request> request) 
        {
            _logger.Info($"AddShopReserveV3 请求参数：{JsonConvert.SerializeObject(request)}");
            return await receiveService.AddShopReserveV3(request.Data);
        }
        /// <summary>
        /// 修改预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> ModifyReserve([FromBody] ApiRequest<ModifyReserveRequest> request)
        {
            _logger.Info($"ModifyReserve 请求参数：{JsonConvert.SerializeObject(request)}");
            var result = await receiveService.ModifyReserve(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 修改预约时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> ModifyReserveTime([FromBody] ApiRequest<ModifyReserveTimeRequest> request) 
        {
            var result = await receiveService.ModifyReserveTime(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> CancelReserve([FromBody] ApiRequest<CancelReserveRequest> request)
        {
            var result = await receiveService.CancelReserve(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取预约详情信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetReserveInfoResponse>> GetReserveInfo([FromQuery] GetReserveInfoRequest request)
        {
            _logger.Info($"GetReserveInfo 请求参数：{JsonConvert.SerializeObject(request)}");
            var result = await receiveService.GetReserveInfo(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取可预约日期
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CanReserveDateResponse>> CanReserveDate([FromQuery]BaseShopRequest request)
        {
            var result = await receiveService.CanReserveDate(request);
            return Result.Success(result);
        }


        /// <summary>
        /// 获取可预约的时间点
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReserveTimeVO>>> CanReserveTime([FromQuery] CanReserveTimeRequest request)
        {
            var result = await receiveService.CanReserveTime(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 获取可预约的时间点V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReserveTimeVO>>> CanReserveTimeV2([FromQuery] CanReserveTimeRequest request)
        {
            var result = await receiveService.CanReserveTimeV2(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取可预约的时间点V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReserveTimeVO>>> CanReserveTimeV3([FromQuery] CanReserveTimeV3Request request)
        {
            var result = await receiveService.CanReserveTimeV3(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 已预约列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<ReservedListResponse>> ReservedList([FromBody] ApiRequest<BasePageRequest> request)
        {
            var result = await receiveService.ReservedList(request.Data);
            ApiPagedResult<ReservedListResponse> response = new ApiPagedResult<ReservedListResponse>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }


        #region 鼠年贺岁版  Celebrate The New Year


        /// <summary>
        /// 新增预约V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<long>> AddReserveV2Async([FromBody] ApiRequest<AddReserveNewYearRequest> request)
        {
            _logger.Info($"AddReserveV2Async 请求参数：{JsonConvert.SerializeObject(request)}");
            return await receiveService.AddReserveV2Async(request.Data);
        }
        

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> CancelReserveV2([FromBody] ApiRequest<CancelReserveRequest> request)
        {
            var result = await receiveService.CancelReserveV2(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 预约列表V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<ReservedListV2Response>> ReserveListV2([FromBody] ApiRequest<ReservedListRequest> request)
        {
            var result = await receiveService.ReserveListV2(request.Data);
            ApiPagedResult<ReservedListV2Response> response = new ApiPagedResult<ReservedListV2Response>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }

        /// <summary>
        /// 获取预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetRebookReserveResponse>> GetReserveInfoV2([FromQuery] GetReserveInfoRequest request)
        {
            return await receiveService.GetReserveInfoV2(request);
        }

        #endregion

        /// <summary>
        /// 收款列表 ForMiniApp
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<PayListResponse>> PayList([FromQuery]PayListForMiniRequest request)
        {
            return await receiveService.PayListForMini(request);
        }


        /// <summary>
        /// 判断是否需要唤醒收银台
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<bool>> CheckIsNeedPayControl([FromQuery]PayListForMiniRequest request)
        {
            return await receiveService.CheckIsNeedPayControlForMini(request);
        }

        /// <summary>
        /// 获取上门养车门店预约日期+时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReserveDateVO>>> GetShopReserveDateTime([FromQuery] BaseShopRequest request)
        {
            var result = await receiveService.GetShopReserveDateTime(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 预约日历
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetReserveSurplusResponse>> GetShopReserveSurplus([FromQuery] BaseShopRequest request)
        {
            var result = await receiveService.GetShopReserveSurplus(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取预约详情V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetReserveInfoV3Response>> GetReserveInfoV3([FromQuery] GetReserveInfoV3Request request) 
        {
            var result = await receiveService.GetReserveInfoV3(request);
            return Result.Success(result);
        }
    }
}
