using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Core.Interfaces;
using Ae.Receive.Service.Core.Model;
using Ae.Receive.Service.Core.Model.Reserve;
using Ae.Receive.Service.Core.Request;
using Ae.Receive.Service.Core.Request.Reserve;
using Ae.Receive.Service.Core.Response;
using Ae.Receive.Service.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Controllers
{
    /// <summary>
    /// 预约服务
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ReserveController))]
    public class ReserveController : ControllerBase
    {

        private readonly IShopReserveService _shopReserveService;
        private readonly ApolloErpLogger<ReserveController> logger;

        /// <summary>
        ///  构造方法
        /// </summary>
        /// <param name="shopReserveService"></param>
        public ReserveController(IShopReserveService shopReserveService,
            ApolloErpLogger<ReserveController> logger
            )
        {
            _shopReserveService = shopReserveService;
            this.logger = logger;
        }

        #region 微信小程序预约接口


        /// <summary>
        /// 判断我的预约跳转类型
        /// 1：有预约记录 2：无订单预约 3：一条订单预约 4：多条订单预约 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<JudgeMyReserveResponse>> JudgeMyReserve([FromQuery] JudgeMyReserveRequest request)
        {

            var result = await _shopReserveService.JudgeMyReserve(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 获取可预约订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<CanReserveOrderDTO>> CanReserveOrderList([FromQuery] CanReserveOrderListRequest request)
        {

            var result = await _shopReserveService.CanReserveOrderList(request);
            ApiPagedResult<CanReserveOrderDTO> response = new ApiPagedResult<CanReserveOrderDTO>()
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
            var result = await _shopReserveService.ReserveInitial(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 新增预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddShopReserveAsync([FromBody] AddReserveRequest request)
        {
            //logger.Info($"AddShopReserveAsync data：{JsonConvert.SerializeObject(request)}");
            return await _shopReserveService.AddShopReserveAsync(request);
        }

        /// <summary>
        /// 新增预约V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<long>> AddReserveV2Async([FromBody] AddReserveV2Request request)
        {
            //logger.Info($"AddReserveV2Async data：{JsonConvert.SerializeObject(request)}");
            return await _shopReserveService.AddReserveV2Async(request);
        }

        /// <summary>
        /// 新增预约v3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddShopReserveV3([FromBody] AddReserveV3Request request)
        {
            //logger.Info($"AddShopReserveV3 data：{JsonConvert.SerializeObject(request)}");
            return await _shopReserveService.AddShopReserveV3Async(request);
        }


        /// <summary>
        /// 获取预约详情信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetReserveInfoResponse>> GetReserveInfo([FromQuery]GetReserveInfoRequest request)
        {
            var result = await _shopReserveService.GetReserveInfo(request);
            return Result.Success(result);
        }


        /// <summary>
        /// 根据预约ID查询预约详情V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetReserveInfoV3Response>> GetReserveInfoV3([FromQuery] GetReserveInfoV3Request request) 
        {
            return await _shopReserveService.GetReserveInfoV3(request);
        }


        /// <summary>
        /// 修改预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> ModifyReserve([FromBody] ModifyReserveRequest request)
        {
            var result = await _shopReserveService.ModifyReserve(request);
            return Result.Success(result);
        }


        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> CancelReserve([FromBody] CancelReserveRequest request)
        {
            var result = await _shopReserveService.CancelReserve(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 修改预约状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateReserveStatus([FromBody] UpdateReserveStatusRequest request)
        {
            var result = await _shopReserveService.UpdateReserveStatus(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 修改预约时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> ModifyReserveTime([FromBody] ModifyReserveTimeRequest request)
        {
            return await _shopReserveService.ModifyReserveTime(request);
        }


        /// <summary>
        /// 获取可预约的时间点
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReserveTimeDTO>>> CanReserveTime([FromQuery]CanReserveTimeRequest request)
        {
            var result = await _shopReserveService.CanReserveTime(request);
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
            var result = await _shopReserveService.CanReserveDate(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 获取可预约的时间点V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReserveTimeDTO>>> CanReserveTimeV2([FromQuery]CanReserveTimeRequest request)
        {
            var result = await _shopReserveService.CanReserveTimeV2(request);
            return Result.Success(result);
        }


        /// <summary>
        /// 获取可预约时间节点V3
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReserveTimeDTO>>> CanReserveTimeV3(CanReserveTimeV3Request request) 
        {
            var result = await _shopReserveService.CanReserveTimeV3(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 已预约列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<ReservedInfoDTO>> ReservedList([FromBody]ReservedListRequest request)
        {
            var result = await _shopReserveService.ReservedList(request);
            ApiPagedResult<ReservedInfoDTO> response = new ApiPagedResult<ReservedInfoDTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }

        

        /// <summary>
        /// 预约列表V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<ReservedListV2DTO>> ReserveListV2([FromBody] ReserveListV2Request request)
        {
            var result = await _shopReserveService.ReserveListV2(request);
            ApiPagedResult<ReservedListV2DTO> response = new ApiPagedResult<ReservedListV2DTO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }

        /// <summary>
        /// 添加门店预约时间工位配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddReserveTimeConfig([FromBody] AddReserveTimeConfigRequest request)
        {
            var result = await _shopReserveService.AddReserveTimeConfig(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取预约时间配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReserveTimeConfigDTO>>> GetReserveTimeConfig([FromQuery] GetReserveTimeConfigRequest request)
        {
            var result = await _shopReserveService.GetReserveTimeConfig(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取预约信息V2-重新预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetRebookReserveInfoResponse>>GetReserveInfoV2([FromQuery] RebookReserveRequest request)
        {
            var result = await _shopReserveService.GetReserveInfoV2(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取上门养车门店预约日期+时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReserveDateDTO>>> GetShopReserveDateTime([FromQuery] BaseShopRequest request) 
        {
            var result = await _shopReserveService.GetShopReserveDateTime(request);
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
            var result = await _shopReserveService.GetShopReserveSurplus(request);
            return Result.Success(result);
        }
        #endregion

        #region APP预约接口

        /// <summary>
        /// 预约列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ReserveListResponse>> GetReserveList([FromQuery] ReserveListRequest request)
        {
            var result = await _shopReserveService.GetReserveList(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 预约搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ReserveSearchResponse>> ReserveSearch([FromQuery] ReserveSearchRequest request)
        {
            var result = await _shopReserveService.ReserveSearch(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 预约详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ReserveDetailVo>> ReserveDetail([FromQuery] ReserveDetailRequest request)
        {
            var result = await _shopReserveService.ReserveDetail(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 预约时间看板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReserveDateVo>>> GetReserveDate([FromQuery] ReserveDateRequest request)
        {
            var result = await _shopReserveService.GetReserveDate(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> CancelReserveV2([FromBody]CancelReserveRequest request)
        {
            return  await _shopReserveService.CancelReserveV2(request);
        }

        /// <summary>
        /// 编辑预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditReserve([FromBody]EditReserveRequest request)
        {
            var result = await _shopReserveService.EditReserve(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据手机号查询用户有效预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReserveListVo>>> GetValidReserve([FromQuery] ValidReserveRequest request)
        {
            var result = await _shopReserveService.GetValidReserve(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 添加预约（B端）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<long>> AddReserveForShop([FromBody] AddReserveForShopRequest request)
        {
            var result = await _shopReserveService.AddReserveForShop(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 预约列表查询-分页的
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ShopReserveListVo>> GetReserveListPage([FromQuery]ReserveListPageRequest request)
        {
            var result = await _shopReserveService.GetReserveListPage(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        #endregion



        #region 内部服务

        /// <summary>
        /// 获取门店预约总数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetShopTotalReserveResponse>> GetShopTotalReserve([FromQuery] BaseShopRequest request) 
        {
            var result = await _shopReserveService.GetShopTotalReserve(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 获取用户当天预约信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetReserveSimpleResponse>> GetSameDayReserveSimpleInfo([FromQuery] BaseUserRequest request) 
        {
            var result = await _shopReserveService.GetSameDayReserveSimpleInfo(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 校验一人一次是否存在预约
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<bool>> CheckTheDayReserveWithUserCarId(CheckReserveTimeRequest request) 
        {
            return await _shopReserveService.CheckTheDayReserveWithUserCarId(request);
        }

        /// <summary>
        /// 根据预约id或订单号查询预约关联订单信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ShopReserveOrderDTO>>> GetReserveInfoByReserveIdOrOrderNum([FromBody] GetReserveInfoByReserveIdOrOrderNum request) 
        {
            var result = await _shopReserveService.GetReserveInfoByReserveIdOrOrderNum(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 预约详情 - shop站点的
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ReserveDetailForWebVo>> GetReserveDetailForWeb([FromQuery]ReserveDetailRequest request)
        {
            var result = await _shopReserveService.GetReserveDetailForWeb(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 预约时间看板 Web端
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ReserveDateVo>>> GetReserveDateForWeb([FromQuery]ReserveDateRequest request)
        {
            var result = await _shopReserveService.GetReserveDateForWeb(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 预约主表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopReserveDTO>> GetShopReserveDO([FromQuery] ReserveDetailRequest request) 
        {
            var result = await _shopReserveService.GetShopReserveDO(request);
            return Result.Success(result);
        }
        #endregion
    }
}
