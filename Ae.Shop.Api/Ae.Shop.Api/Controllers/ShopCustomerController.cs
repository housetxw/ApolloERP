using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.ShopCustomer;
using Ae.Shop.Api.Core.Request.ShopCustomer;
using Ae.Shop.Api.Filters;

namespace Ae.Shop.Api.Controllers
{
    /// <summary>
    /// 门店客户
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    //[Filter(nameof(ShopCustomerController))]
    public class ShopCustomerController : ControllerBase
    {
        private readonly IShopCustomerService shopCustomerService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="shopCustomerService"></param>
        public ShopCustomerController(IShopCustomerService shopCustomerService)
        {
            this.shopCustomerService = shopCustomerService;
        }

        /// <summary>
        /// 门店客户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<ShopCustomerListVo>> GetShopCustomerList([FromQuery]ShopCustomerListRequest request)
        {
            var result = await shopCustomerService.GetShopCustomerList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 客户详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopCustomerDetailVo>> GetShopCustomerDetail([FromQuery]ShopCustomerDetailRequest request)
        {
            var result = await shopCustomerService.GetShopCustomerDetail(request);

            return Result.Success(result);
        }
    }
}