using Microsoft.AspNetCore.Mvc;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Controllers
{
    [Route("[controller]/[action]")]
    public class AdController : ControllerBase
    {
        private readonly IAdService _adService;
        private readonly ApolloErpLogger<AdController> _logger;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="shopService"></param>
        public AdController(
            ApolloErpLogger<AdController> logger,
            IAdService adService
            )
        {
            _logger = logger;
            _adService = adService;
            
        }

        /// <summary>
        /// 查询广告列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<AdvertisementDTO>>> GetAdListAsync()
        {
            var result = await _adService.GetAdListAsync();
            return Result.Success(result);
        }

        /// <summary>
        /// 获取最新公告
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<NoticeDTO>> GetTopNewNoticeAsync([FromQuery] GetTopNewNoticeRequest request)
        {
            var result = await _adService.GetTopNewNoticeAsync(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 获取公告详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<NoticeDTO>> GetNoticeInfoAsync([FromQuery] GetNoticeInfoRequest request)
        {
            var result = await _adService.GetNoticeInfoAsync(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetNoticeListResponse>> GetNoticeListAsync([FromQuery] GetNoticeListRequest request) 
        {
            var result = await _adService.GetNoticeListAsync(request);
            return Result.Success(result);
        }
    }
}
