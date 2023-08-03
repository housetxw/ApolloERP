using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Model.Product;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Product;
using Ae.C.MiniApp.Api.Core.Response.Product;
using Ae.C.MiniApp.Api.Filters;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 商品相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ProductController))]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        ///  搜索
        /// </summary>
        /// <param name="request">请求参数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ProductSearchResponse>> Search([FromQuery]ProductSearchRequest request)
        {
            var result = await _productService.ProductSearch(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 搜索热词
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<string>> HotWords()
        {
            var data = new List<string>() { "机油滤清器", "空气滤清器", "标准洗", "机油" };
            return Result.Success(data);
        }


        /// <summary>
        /// 商品详情
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<ProductDetailResponse>> Detail([FromQuery]ProductDetailRequest request)
        {
            var result = await _productService.ProductDetail(request);
            return Result.Success(result);
        }

        /// <summary>
        ///  商品规格接口
        ///  查询是否设置关联商品
        /// </summary>
        /// <param name="productCode">商品编码 测试数据 BYFW-JY-TSO-150</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ProductSpecificationResponse>> Specifications(string productCode)
        {
            var result = await _productService.Specifications(productCode);
            return Result.Success(result);
        }


        /// <summary>
        ///  获取首页美容洗车类目信息
        /// </summary>
        /// <param >类目Id</param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<CategoryInfoVo>> GetBeautyWashCategorys(int categoryId)
        {
            var result = _productService.GetBeautyWashCategorys(categoryId);
            return Result.Success(result);
        }


        /// <summary>
        ///  根据类目查询商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CategoryProductVo>>> SelectProductsByCategory([FromQuery]CategoryProductRequest request)
        {
            var result = await _productService.SelectProductsByCategory(request);
            return Result.Success(result);
        }

        [HttpGet]
        public async Task<ApiResult<GetProductDetailConfigResponse>> GetProductDetailConfig([FromQuery] GetProductDetailConfigRequest request)
        {
            return Result.Success(new GetProductDetailConfigResponse()
            {
                IsShowCar = true,
                IsShowShop = true
            });
        }

        /// <summary>
        /// 获取有效的秒杀活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]        
        [AllowAnonymous]
        public async Task<ApiResult<List<FlashSaleConfigVo>>> GetActiveFlashSaleConfigs()
        {
            return await _productService.GetActiveFlashSaleConfigs(new GetActiveFlashSaleConfigsRequest { });
        }
    }
}