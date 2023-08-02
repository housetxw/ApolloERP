using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Interfaces;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Model.ShopProduct;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Core.Request.ShopProduct;
using Ae.Product.Service.Filters;

namespace Ae.Product.Service.Controllers
{
    /// <summary>
    /// 门店商品相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    // [Filter(nameof(ShopProductController))]
    public class ShopProductController : ControllerBase
    {
        private readonly IShopProductManageService _shopProductManageService;
        public ShopProductController(IShopProductManageService shopProductManageService)
        {
            this._shopProductManageService = shopProductManageService;
        }

        /// <summary>
        /// 分页查询门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiPagedResult<ShopProductVo> SearchShopProduct([FromBody] ShopProductSearchRequest request)
        {
            var result = _shopProductManageService.SearchShopProduct(request);
            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 查询门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<ShopProductVo>> SelectShopProduct([FromBody] ShopProductQueryRequest request)
        {
            var result = _shopProductManageService.SelectShopProduct(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 查询门店商品by编码
        /// </summary>
        /// <param name="shopProductCode">门店商品编码</param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<ShopProductVo> GetShopProductByCode(string shopProductCode)
        {
            var result = _shopProductManageService.GetShopProductByCode(shopProductCode);
            return Result.Success(result);
        }

        /// <summary>
        /// 保存门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<bool> SaveShopProduct([FromBody] ShopProductEditRequest request)
        {
            var result = _shopProductManageService.SaveShopProduct(request);
            return Result.Success(true);
        }

        /// <summary>
        /// 新增外采产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveShopPurchaseProduct([FromBody] AddShopProductRequest request)
        {
            var result = await _shopProductManageService.SaveShopPurchaseProduct(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 查询门店商品byCodes
        /// </summary>
        /// <param name="request">门店商品</param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<List<ShopProductVo>> GetShopProductByCodes([FromBody] ShopProductDetialRequest request)
        {
            var result = _shopProductManageService.GetShopProductByCodes(request.ShopProductCodes);
            return Result.Success(result);
        }

        /// <summary>
        ///  批量导入门店商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<bool> ImportShopProduct([FromBody] ImportShopProductRequest request)
        {
            var result = _shopProductManageService.ImportShopProduct(request);
            return Result.Success<bool>(result);
        }

        /// <summary>
        /// 初始化门店基础服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<bool> InitShopBasicService([FromBody] InitShopServiceRequest request)
        {
            var result = _shopProductManageService.InitShopBasicService(request);
            return Result.Success<bool>(result);
        }

        /// <summary>
        /// 审核门店商品上架
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<bool> AuditShopProduct([FromBody] ShopProductAuditRequest request)
        {
            var result = _shopProductManageService.AuditShopProduct(request);
            return Result.Success<bool>(result);
        }
        /// <summary>
        /// 自动审核门店商品上架
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<bool> AutoAuditShopProduct()
        {
            var result = _shopProductManageService.AutoAuditShopProduct();
            return Result.Success<bool>(result);
        }

        /// <summary>
        /// 获取门店上架的服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopProductDetailVo>>> GetOnSaleShopProduct(
            [FromQuery] OnSaleShopProductRequest request)
        {
            var result = await _shopProductManageService.GetOnSaleShopProduct(request);

            return Result.Success(result);
        }

        
        [HttpPost]
        public async Task<ApiResult<string>> UpdateProductPriceInfo([FromBody] UpdateProductPriceRequest request)
        {
            return await _shopProductManageService.UpdateProductPriceInfo(request);
        }
    }
}