using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model.BaoYang;
using Ae.B.Product.Api.Core.Request.BaoYang;
using Ae.B.Product.Api.Filters;

namespace Ae.B.Product.Api.Controllers
{
    /// <summary>
    /// 保养适配配置相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    public class BaoYangController : ControllerBase
    {
        private readonly IBaoYangService _baoYangService;

        public BaoYangController(IBaoYangService baoYangService)
        {
            _baoYangService = baoYangService;
        }

        /// <summary>
        /// 保养适配首页接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<BaoYangCategoryVO>>> GetBaoYangPackages(
            [FromBody] ApiRequest<GetBaoYangPackagesRequest> request)
        {
            var result = await _baoYangService.GetBaoYangPackages(request.Data);

            return result;
        }

        /// <summary>
        /// 保养更多商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<SearchProductModel<BaoYangPackageProductModel>>> SearchPackageProductsWithCondition(
            [FromBody] ApiRequest<SearchProductRequest> request)
        {
            var result = await _baoYangService.SearchPackageProductsWithCondition(request.Data);

            return Result.Success(result);
        }
    }
}