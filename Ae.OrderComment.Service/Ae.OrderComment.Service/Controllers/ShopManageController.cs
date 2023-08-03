using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Core.Interfaces;
using Ae.OrderComment.Service.Core.Model;
using Ae.OrderComment.Service.Core.Request;
using Ae.OrderComment.Service.Filters;

namespace Ae.OrderComment.Service.Controllers
{

    /// <summary>
    /// 门店管理Demo
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(OrderCommentController))]
    public class ShopManageController : ControllerBase
    {
        public IShopManageService shopManageService;
        private readonly string _constMessage = "无数据";
        public ShopManageController(IShopManageService shopManageService)
        {
            this.shopManageService = shopManageService;
        }

        [HttpGet]
        public async Task<ApiPagedResult<ShopSimpleInfoDTO>> GetShopListAsync([FromQuery]GetShopListRequest request)
        {
            var result = await this.shopManageService.GetShopListAsync(request);

            return await Task.FromResult(result);
            
        }

    }
}