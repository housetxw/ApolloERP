using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;

namespace Ae.Shop.Api.Controllers
{

    [Route("[controller]/[action]")]
    //[Filter(nameof(ShopManageController))]

    public class ShopManageController : ControllerBase
    {
        private readonly IShopManageService shopManageService;

        public ShopManageController(IShopManageService shopManageService)
        {
            this.shopManageService = shopManageService;
        }

        /// <summary>
        /// 查询门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ShopSimpleInfoResponse>> GetShopList([FromQuery]GetShopListRequest request)
        {
            var result = await shopManageService.GetShopListAsync(request);
            return await Task.FromResult(result);
        }

        /// <summary>
        /// 查询门店仓库列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopSimpleInfoResponse>>> GetShopWareHouseList([FromQuery] GetShopListRequest request)
        {
            var result = await shopManageService.GetShopWareHouseList(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 查询同公司下的门店列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ShopSimpleInfoResponse>> GetCompanyShopList([FromQuery] GetShopListRequest request)
        {
            var result = await shopManageService.GetCompanyShopList(request);
            return await Task.FromResult(result);
        }
    }
}