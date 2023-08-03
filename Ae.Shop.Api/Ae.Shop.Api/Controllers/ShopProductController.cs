using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.ShopProduct;
using Ae.Shop.Api.Core.Request.Porduct;
using Ae.Shop.Api.Core.Request.ShopProduct;
using Ae.Shop.Api.Filters;

namespace Ae.Shop.Api.Controllers
{
    /// <summary>
    /// 门店商品
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
   // [Filter(nameof(ShopProductController))]
    public class ShopProductController : ControllerBase
    {
        private readonly IShopProductService shopProductService;

        /// <summary>
        /// 构造
        /// </summary>
        public ShopProductController(IShopProductService shopProductService)
        {
            this.shopProductService = shopProductService;
        }

        /// <summary>
        /// 外采商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ShopProductListVo>> GetShopProductList([FromQuery]ShopProductListRequest request)
        {
            var result = await shopProductService.GetShopProductList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 商品详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopProductDetailVo>> GetShopProductDetail([FromQuery]ShopProductDetailRequest request)
        {
            var result = await shopProductService.GetShopProductDetail(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 删除商品 禁用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteShopProduct([FromBody]ApiRequest<DeleteShopProductRequest> request)
        {
            var result = await shopProductService.DeleteShopProduct(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveShopProduct([FromBody]ApiRequest<AddShopProductRequest> request)
        {
            var result = await shopProductService.SaveShopProduct(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取门店服务大类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopServiceTypeVo>>> GetShopServiceType()
        {
            var result = await shopProductService.GetShopServiceType();

            return Result.Success(result);
        }

        /// <summary>
        /// 获取商品单位
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<string>>> GetShopProductUnit()
        {
            var result = await shopProductService.GetShopProductUnit();

            return Result.Success(result);
        }

        /// <summary>
        /// 获取外采产品类目
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ProductCategoryVo>>> GetShopProductCategory()
        {
            var result = await shopProductService.GetShopProductCategory();

            return Result.Success(result);
        }

        #region 商品管理

        /// <summary>
        /// 获取商品品牌
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CatalogBrandVo>>> GetBrandList()
        {
            var result = await shopProductService.GetBrandList();
            return Result.Success(result);
        }

        /// <summary>
        ///   查询类目息 by 类目 和 类目 level 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<CategoryInfoVo>>> GetCategorysById(int? categoryId, int? level)
        {
            var result = await shopProductService.GetCategorysById(categoryId, level);
            return Result.Success(result);
        }

        /// <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ProductAllInfoVo>> SearchProductPageList(
            [FromQuery] SearchProductPageListRequest request)
        {
            var result = await shopProductService.SearchProductPageList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 查询单位信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<UnitVo>>> GetUnitList()
        {
            var result = await shopProductService.GetUnitList();

            return Result.Success(result);
        }

        /// <summary>
        ///  编辑商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveProduct([FromBody] ApiRequest<ProductEditRequest> request)
        {
            var result = await shopProductService.SaveProduct(request.Data);

            return Result.Success<bool>(result);
        }

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ProductDetailVo>> GetProductByProductCode(string productCode)
        {
            var result = await shopProductService.GetProductByProductCode(productCode);

            return Result.Success(result);
        }

        #endregion
    }
}