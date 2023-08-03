using System;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Request.Arrival;
using Ae.Shop.Api.Core.Response.Arrival;
using Ae.Shop.Api.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ae.Shop.Api.Common.Constant;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;

namespace Ae.Shop.Api.Controllers
{
    /// <summary>
    /// 到店记录
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ArrivalController))]
    public class ArrivalController : ControllerBase
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private IArrivalService _arrivalService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="arrivalService"></param>
        public ArrivalController(IArrivalService arrivalService)
        {
            _arrivalService = arrivalService;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 到店记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetArrivalListResponse>> GetArrivalList([FromQuery]GetArrivalListRequest request)
        {
            return await _arrivalService.GetArrivalList(request);
        }

        /// <summary>
        /// 到店列表查询条件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ArrivalListConditionResponse>> GetArrivalListCondition()
        {
            ArrivalListConditionResponse result = new ArrivalListConditionResponse();
            result.Status = new List<StatusEnum> {
                new StatusEnum { Value = -1, DisplayName = "全部" },
                new StatusEnum { Value = 0, DisplayName = "等待中" },
                new StatusEnum { Value = 1, DisplayName = "派工中" },
                new StatusEnum { Value = 2, DisplayName = "已完结" },
                new StatusEnum { Value = 3, DisplayName = "未消费离店" }
            };


            return Result.Success(result);
        }

        /// <summary>
        /// 到店记录详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetArrivalDetailResponse>> GetArrivalDetail([FromQuery]GetArrivalDetailRequest request)
        {
            return await _arrivalService.GetArrivalDetail(request);
        }

        /// <summary>
        ///获取门店进店趋势数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<ArrivalTrendStatisticsResDTO> GetArrivalTrendStatistics([FromQuery] ArrivalTrendStatisticsReqDTO req)
        {
            if (req.EndTime.Subtract(req.StartTime).Days + 1 > CommonConstant.Ninety)
            {
                return Result.Failed<ArrivalTrendStatisticsResDTO>("最长只能查看三个月内的数据");
            }
            return Result.Success(_arrivalService.GetArrivalTrendStatistics(req), CommonConstant.QuerySuccess);
        }


    }
}
