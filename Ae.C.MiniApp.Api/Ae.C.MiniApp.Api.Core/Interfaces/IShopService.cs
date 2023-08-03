using System;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Request.Shop;
using Ae.C.MiniApp.Api.Core.Response;

namespace Ae.C.MiniApp.Api.Core.Interfaces
{
    public interface IShopService
    {
        Task<bool> JoinInAsync(JoinInRequest request);
        Task<ApiPagedResultData<NearShopInfoVO>> GetNearShopListAsync(GetNearShopListRequest request);
        Task<GetShopDetailResponse> GetShopDetailAsync(GetShopDetailRequest request);
        Task<List<ShopLocationVO>> GetRegionByCityId(GetRegionByCityIdRequest request);
        Task<GetCompanyInfoResponse> GetCompanyInfo(GetCompanyInfoRequest request);
        Task<GetAllCitysResponse> GetAllCitys();
        Task<List<ShopServiceProjectVO>> GetShopServiceProject(BaseShopRequest request);
        Task<GetOptimalShopResponse> GetOptimalShop(GetOptimalShopRequest request);
    }
}
