using System.Collections.Generic;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Model;
using Ae.C.MiniApp.Api.Client.Model.Shop;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Request.Shop;
using Ae.C.MiniApp.Api.Client.Response;

namespace Ae.C.MiniApp.Api.Client.Interface
{
    public interface IShopClient
    {
        Task<bool> JoinInAsync(JoinInClientRequest request);
        Task<ApiPagedResultData<NearShopInfoDTO>> GetNearShopListAsync(GetNearShopListClientRequest request);
        Task<GetShopDetailClientResponse> GetShopDetailAsync(GetShopDetailClientRequest request);
        Task<List<ShopLocationDTO>> GetRegionByCityId(GetRegionByCityIdClientRequest request);
        Task<CompanySimpleInfoClientResponse> MiniGetCompanyInfo(GetCompanyInfoClientRequest request);
        Task<GetCityListClientResponse> GetChinaCitys();
        /// <summary>
        /// 查询门店服务上下架
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopOnSaleServiceDTO>> GetOnSaleShopServiceList(ShopServiceListRequest request);
        /// <summary>
        /// 获取门店开通的服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopServiceProjectDTO>> GetShopServiceProject(BaseShopClientRequest request);
        /// <summary>
        /// 定位门店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GetOptimalShopClientResponse> GetOptimalShop(GetOptimalShopClientRequest request);
    }
}