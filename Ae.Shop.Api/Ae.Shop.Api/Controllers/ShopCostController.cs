using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Request.ShopCost;
using Ae.Shop.Api.Core.Response.ShopCost;
using Ae.Shop.Api.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Ae.Shop.Api.Controllers
{
    /// <summary>
    /// 到店记录
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(ShopCostController))]
    public class ShopCostController : ControllerBase
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private IShopCostService shopCostService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="shopCostService"></param>
        public ShopCostController(IShopCostService shopCostService)
        {
            this.shopCostService = shopCostService;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        
        /// <summary>
        /// 门店费用列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetShopCostResponse>> GetShopCostList([FromQuery]ShopCostRequest request)
        {
            return await shopCostService.GetShopCostList(request);
        }

        /// <summary>
        /// 门店费用类别查询条件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetShopCostTypeListResponse>> GetShopCostTypeListCondition()
        {
            return await shopCostService.GetShopCostTypeListCondition();
        }

        /// <summary>
        /// 门店费用编辑详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetShopCostResponse>> GetShopCostDetail([FromQuery]GetShopCostDetailRequest request)
        {
            return await shopCostService.GetShopCostDetail(request);
        }

        /// <summary>
        /// 门店费用新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddShopCost([FromBody]ApiRequest<AddShopCostRequest> request)
        {
            return await shopCostService.AddShopCost(request.Data);
        }

        /// <summary>
        /// 门店费用编辑
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateShopCost([FromBody]ApiRequest<AddShopCostRequest> request)
        {
            return await shopCostService.UpdateShopCost(request.Data);
        }


        /// <summary>
        /// 门店费用删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteShopCost([FromBody]ApiRequest<AddShopCostRequest> request)
        {
            return await shopCostService.DeleteShopCost(request.Data);
        }
    }
}
