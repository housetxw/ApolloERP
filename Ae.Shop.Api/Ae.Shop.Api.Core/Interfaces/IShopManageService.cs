using ApolloErp.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IShopManageService
    {
        Task<ApiPagedResult<ShopSimpleInfoResponse>> GetShopListAsync(GetShopListRequest request);
        Task<ApiPagedResult<ShopSimpleInfoResponse>> GetCompanyShopList(GetShopListRequest request);
        Task<List<ShopSimpleInfoResponse>> GetShopWareHouseList(GetShopListRequest request);
    }
}
