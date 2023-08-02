using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Model.ShopProduct;
using Ae.B.Product.Api.Core.Request.ShopProduct;
using Ae.B.Product.Api.Core.Response.ShopProduct;
using Ae.B.Product.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Controllers
{
    /// <summary>
    /// 门店商品查询
    /// </summary>
    [Route("[controller]/[action]")]
    public class ShopProductController : ControllerBase
    {
        private readonly IShopProductService _shopProductService;
        public ShopProductController(IShopProductService shopProductService)
        {
            this._shopProductService = shopProductService;
        }

        /// <summary>
        /// 查询门店商品by编码
        /// </summary>
        /// <param name="shopProductCode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopProductVo>> GetShopProductByCode(string shopProductCode)
        {
            var result = await _shopProductService.GetShopProductByCode(shopProductCode);
            return Result.Success(result);
        }

        /// <summary>
        /// 保存门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SaveShopProduct([FromBody]ApiRequest<ShopProductEditRequest> request)
        {
            var result = await _shopProductService.SaveShopProduct(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 分页查询门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopProductSearchResponse>> SearchShopProduct([FromQuery]ShopProductSearchRequest request)
        {
            var result = await _shopProductService.SearchShopProduct(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取门店信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ShopSimpleInfoVo>> GetShopList([FromQuery]ShopListRequest request)
        {
            var result = await _shopProductService.GetShopList(request);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 获取门店开通的服务项目
        /// </summary>
        /// <param name="shopId">门店Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopServiceTypeVo>>> GetShopServiceType(long shopId)
        {
            var result = await _shopProductService.GetShopServiceType(shopId);
            return Result.Success(result);
        }
        /// <summary>
        ///  导入商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<ShopProductImportVo>>> ImportShopProduct(IFormFile file)
        {
            var result = await _shopProductService.ImportShopProduct(file);
            return Result.Success(result);
        }
        /// <summary>
        /// 审核门店商品上架
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AuditShopProduct([FromBody]ApiRequest<ShopProductAuditRequest> request)
        {
            var result = await _shopProductService.AuditShopProduct(request.Data);
            return Result.Success(result);
        }
    }
}