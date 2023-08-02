using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Interfaces;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Core.Request.Config;
using Ae.Product.Service.Core.Request.Product;
using Ae.Product.Service.Core.Response;

namespace Ae.Product.Service.Controllers
{
    /// <summary>
    /// 首页配置相关
    /// </summary>
    [Route("[controller]/[action]")]
    public class PageConfigController : ControllerBase
    {
        private readonly IPageConfigService _pageConfigService;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="pageConfigService"></param>
        public PageConfigController(IPageConfigService pageConfigService)
        {
            _pageConfigService = pageConfigService;
        }

        /// <summary>
        /// 首页活动配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ConfigAdvertisementVo>>> QueryConfigAdvertisement(
            [FromQuery] QueryConfigAdvertisementRequest request)
        {
            var result = await _pageConfigService.QueryConfigAdvertisement(request);

            return Result.Success(result);
        }

        /// <summary>
        ///获取前台一级类目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ConfigFrontCategoryVo>>> GetMainFrontCategory(
            [FromQuery] MainFrontCategoryRequest request)
        {
            var result = await _pageConfigService.GetMainFrontCategory(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ProductBaseInfoVo>>> GetHotProduct([FromQuery] HotProductRequest request)
        {
            var result = await _pageConfigService.GetHotProduct(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 分页获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ConfigHotProductVo>> GetHotProductPageList(
            [FromQuery] HotProductPageListRequest request)
        {
            var result = await _pageConfigService.GetHotProductPageList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 搜索商品For添加热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ConfigHotProductVo>> SearchProductForHot(
            [FromQuery] SearchProductForHotRequest request)
        {
            var result = await _pageConfigService.SearchProductForHot(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 新增热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddHotProduct([FromBody] AddHotProductRequest request)
        {
            var result = await _pageConfigService.AddHotProduct(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑热销产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditHotProduct([FromBody] EditHotProductRequest request)
        {
            var result = await _pageConfigService.EditHotProduct(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ProductListByFrontCategoryIdResponse>> GetProductListByFrontCategoryId(
            [FromQuery] ProductListByFrontCategoryIdRequest request)
        {
            var result = await _pageConfigService.GetProductListByFrontCategoryId(request);

            return Result.Success(result);
        }

        #region 套餐卡

        /// <summary>
        /// 套餐卡查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<PackageCardProductVo>> GetPackageCardProductPageList(
            [FromBody] PackageCardProductPageListRequest request)
        {
            var result = await _pageConfigService.GetPackageCardProductPageList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 搜索商品For添加套餐卡商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<PackageCardProductVo>> SearchProductForPackageCard(
            [FromQuery] SearchProductForPackageCardRequest request)
        {
            var result = await _pageConfigService.SearchProductForPackageCard(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 添加套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddPackageCardProduct([FromBody] AddPackageCardProductRequest request)
        {
            var result = await _pageConfigService.AddPackageCardProduct(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditPackageCardProduct([FromBody] EditPackageCardProductRequest request)
        {
            var result = await _pageConfigService.EditPackageCardProduct(request);

            return Result.Success(result);
        }


        /// <summary>
        /// 门店套餐卡查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<GetShopPackageCardProductPageListVo>> GetShopPackageCardProductPageList(
            [FromBody] ApiRequest< GetShopPackageCardProductPageListRequest> request)
        {
            return await _pageConfigService.GetShopPackageCardProductPageList(request.Data);
        }

        /// <summary>
        /// 保存门店套餐卡
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> SaveShopPackageCard([FromBody]  ApiRequest<ConfigShopPackageCardDTO> request)
        {
            return await _pageConfigService.SaveShopPackageCard(request);
        }

        /// <summary>
        /// 门店套餐卡查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<GetShopPackageCardProductPageListVo>> GetShopCardDetail(
            [FromBody] ApiRequest<GetShopCardDetailRequest> request)
        {
           var data= await _pageConfigService.GetShopCardDetail(request);

            return Result.Success(data);
        }


        #endregion

        #region 首页促销配置

        [HttpPost]
        public async Task<ApiResult<string>> DeleteConfigAdvertisement([FromBody]ConfigAdvertisementVo request)
        {
            return await _pageConfigService.DeleteConfigAdvertisement(request);
        }

        [HttpGet]
        public async Task<ApiPagedResult<ConfigAdvertisementVo>> GetConfigAdvertisements([FromQuery]GetConfigAdvertisementsRequest request)
        {
            return await _pageConfigService.GetConfigAdvertisements(request);
        }

        [HttpPost]
        public async Task<ApiResult<string>> AddConfigAdvertisement([FromBody]ConfigAdvertisementVo request)
        {
            return await _pageConfigService.AddConfigAdvertisement(request);
        }

        [HttpGet]
        public async Task<ApiResult<ConfigAdvertisementVo>> GetConfigAdvertisement([FromQuery]ConfigAdvertisementVo request)
        {
            return await _pageConfigService.GetConfigAdvertisement(request);
        }

        [HttpPost]
        public async Task<ApiResult<string>> UpdateConfigAdvertisement([FromBody]ConfigAdvertisementVo request)
        {
            return await _pageConfigService.UpdateConfigAdvertisement(request);
        }
        #endregion

        #region 美容团购产品管理

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
        public async Task<ApiResult<bool>> AddGrouponProduct([FromBody] AddGrouponProductRequest request)
        {
            var result = await _pageConfigService.AddGrouponProduct(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑美容团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditGrouponProduct([FromBody] EditGrouponProductRequest request)
        {
            var result = await _pageConfigService.EditGrouponProduct(request);

            return Result.Success(result);
        }

        #endregion


    }
}