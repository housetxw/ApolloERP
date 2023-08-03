using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog.Filters;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Interfaces;
using Ae.C.MiniApp.Api.Core.Model.ShopProduct;
using Ae.C.MiniApp.Api.Core.Request.ShopProduct;
using Ae.C.MiniApp.Api.Core.Response.Product;

namespace Ae.C.MiniApp.Api.Controllers
{
    /// <summary>
    /// 门店商品查询
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ShopProductController))]
    public class ShopProductController : Controller
    {
        private readonly IShopProductService _shopProductService;
        public ShopProductController(IShopProductService shopProductService)
        {
            this._shopProductService = shopProductService;
        }

        /// <summary>
        /// 查询门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopProductVO>>> SelectShopProduct([FromQuery]ShopProductQueryRequest request)
        {
            var result = await _shopProductService.SelectShopProduct(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取门店服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ServiceProjectVo>>> GetShopServiceProject(
            [FromQuery] ShopServiceProjectRequest request)
        {
            var result = await _shopProductService.GetShopServiceProject(request);

            return Result.Success(result);
        }



        /// <summary>
        /// 服务大类 商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<ProductListByServiceTypeResponse>> GetProductListByServiceType(
            [FromBody] ApiRequest<ProductListByServiceTypeRequest> request)
        {
            var result = await _shopProductService.GetProductListByServiceType(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 商品详情页 （自营商品、门店服务项目）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<ProductDetailPageDataResponse>> GetProductDetailPageData(
            [FromQuery] ProductDetailPageDataRequest request)
        {
            var result = await _shopProductService.GetProductDetailPageData(request);

            return Result.Success(result);
        }
    }
}