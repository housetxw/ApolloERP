using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Request.ShopCost;
using Ae.Shop.Api.Core.Response.ShopCost;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IShopCostService
    {
        Task<ApiPagedResult<GetShopCostResponse>> GetShopCostList(ShopCostRequest request);
        Task<ApiResult<GetShopCostTypeListResponse>> GetShopCostTypeListCondition();
        Task<ApiResult<GetShopCostResponse>> GetShopCostDetail(GetShopCostDetailRequest request);
        Task<ApiResult<bool>> AddShopCost(AddShopCostRequest request);
        Task<ApiResult<bool>> UpdateShopCost(AddShopCostRequest request);
        Task<ApiResult<bool>> DeleteShopCost(AddShopCostRequest request);
    }
}
