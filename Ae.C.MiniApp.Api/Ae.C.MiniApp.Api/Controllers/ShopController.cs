using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.C.MiniApp.Api.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(ShopController))]
    public class ShopController : ControllerBase
    {
        private readonly IShopService shopService;
        private readonly ApolloErpLogger<ShopController> _logger;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopService"></param>
        public ShopController(IShopService shopService,
            ApolloErpLogger<ShopController> logger
            )
        {

            this.shopService = shopService;
            _logger = logger;
        }

        /// <summary>
        /// 获取附近门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ApiPagedResult<NearShopInfoVO>> GetNearShopList([FromBody] ApiRequest<GetNearShopListRequest> request)
        {
            _logger.Info($"获取附近门店列表 data:{JsonConvert.SerializeObject(request.Data)}");
            var result = await shopService.GetNearShopListAsync(request.Data);
            ApiPagedResult<NearShopInfoVO> response = new ApiPagedResult<NearShopInfoVO>()
            {
                Code = ResultCode.Success,
                Data = result
            };
            return response;
        }
        /// <summary>
        /// 获取门店详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        public async Task<ApiResult<GetShopDetailResponse>> GetShopDetail([FromQuery] GetShopDetailRequest request)
        {
            var result = await shopService.GetShopDetailAsync(request);
            var getShopService = await shopService.GetShopServiceProject(new BaseShopRequest()
            {
                ShopId = request.ShopId
            });
            if (result != null)
                result.ShopServiceType = getShopService?.ToList();
            return Result.Success(result);
        }
        /// <summary>
        /// 加盟
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> JoinIn([FromBody] ApiRequest<JoinInRequest> request)
        {
            var result = await shopService.JoinInAsync(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 官网加盟
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult<bool>> JoinInByWeb([FromBody] ApiRequest<JoinInRequest> request)
        {
            var result = await shopService.JoinInAsync(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取公司信息详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetCompanyInfoResponse>> GetCompanyInfo([FromQuery] GetCompanyInfoRequest request)
        {
            var result = await shopService.GetCompanyInfo(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 根据市查询市下的区县（包含门店数量）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopLocationVO>>> GetRegionByCityId([FromQuery] GetRegionByCityIdRequest request)
        {
            var result = await shopService.GetRegionByCityId(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取所有城市
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<GetAllCitysResponse>> GetAllCitys()
        {
            var result = await shopService.GetAllCitys();
            return Result.Success(result);
        }

        /// <summary>
        /// 获取网站备案号
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<GetWebSiteInfoResponse>> GetWebSiteInfo()
        {
            var host = HttpContext.Request.Host.Value;
            _logger.Info("域名 GetWebSiteInfo：" + host);
            string beiAnHao = "皖ICP备19025011号-1";
            //if (host.Contains("ApolloErp.cn"))
            //{
            //    beiAnHao = "皖ICP备19025011号";
            //}
            //else if (host.Contains("ApolloErp.work"))
            //{
            //    beiAnHao = "皖ICP备19025011号-3";
            //}
            //else if (host.Contains("ApolloErp.com.cn"))
            //{
            //    beiAnHao = "皖ICP备19025011号-4";
            //}
            //else if (host.Contains("ApolloErp.net.cn"))
            //{
            //    beiAnHao = "皖ICP备19025011号-5";
            //}
            //else if (host.Contains("ApolloErp.net"))
            //{
            //    beiAnHao = "皖ICP备19025011号-2";
            //}
            var result = new GetWebSiteInfoResponse()
            {
                BeiAnHao = beiAnHao
            };
            return Result.Success(result);
        }



        #region 鼠年贺岁版  Celebrate The New Year

        /// <summary>
        /// 获取门店开通的服务项目
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<List<ShopServiceProjectVO>>> GetShopServiceProject([FromQuery] BaseShopRequest request)
        {
            var result = await shopService.GetShopServiceProject(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 定位门店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<GetOptimalShopResponse>> GetOptimalShop([FromQuery] GetOptimalShopRequest request)
        {
            var result = await shopService.GetOptimalShop(request);
            return Result.Success(result);
        }

        #endregion
    }
}
