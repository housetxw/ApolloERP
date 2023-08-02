using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Model.Product;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Core.Request.Product;

namespace Ae.B.Product.Api.Controllers
{
    [Route("[controller]/[action]")]
    public class PageConfigController : ControllerBase
    {
        private readonly IPageConfigService _pageConfigService;

        public PageConfigController(IPageConfigService pageConfigService)
        {
            _pageConfigService = pageConfigService;
        }

        #region 首页促销配置
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> DeleteConfigAdvertisement([FromBody]ApiRequest<ConfigAdvertisementVo> request)
        {
            return await _pageConfigService.DeleteConfigAdvertisement(request.Data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ConfigAdvertisementVo>> GetConfigAdvertisements([FromQuery]GetConfigAdvertisementsRequest request)
        {
            return await _pageConfigService.GetConfigAdvertisements(request);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> AddConfigAdvertisement([FromBody]ApiRequest<ConfigAdvertisementVo> request)
        {
            return await _pageConfigService.AddConfigAdvertisement(request.Data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ConfigAdvertisementVo>> GetConfigAdvertisement([FromQuery]ConfigAdvertisementVo request)
        {
            return await _pageConfigService.GetConfigAdvertisement(request);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> UpdateConfigAdvertisement([FromBody]ApiRequest<ConfigAdvertisementVo> request)
        {
            return await _pageConfigService.UpdateConfigAdvertisement(request.Data);
        }
        #endregion

        #region 美容团购商品管理

        /// <summary>
        /// 美容团购商品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GrouponProductVo>> GetGrouponProductPageList(
            [FromQuery] GrouponProductPageListRequest request)
        {
            var result = await _pageConfigService.GetGrouponProductPageList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 搜索产品For美容团购
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GrouponProductVo>> SearchProductForGroupon(
            [FromQuery] SearchProductForGrouponRequest request)
        {
            var result = await _pageConfigService.SearchProductForGroupon(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 新增团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddGrouponProduct([FromBody] ApiRequest<AddGrouponProductRequest> request)
        {
            var result = await _pageConfigService.AddGrouponProduct(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑美容团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditGrouponProduct([FromBody] ApiResult<EditGrouponProductRequest> request)
        {
            var result = await _pageConfigService.EditGrouponProduct(request.Data);

            return Result.Success(result);
        }

        #endregion

    }
}