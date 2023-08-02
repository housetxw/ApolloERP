using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Request;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Model.Product;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Core.Request.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Client.Interfaces
{
    public interface IPageConfigClient
    {
        Task<ApiResult<string>> DeleteConfigAdvertisement(ConfigAdvertisementVo request);

        Task<ApiPagedResult<ConfigAdvertisementVo>> GetConfigAdvertisements(GetConfigAdvertisementsRequest request);

        Task<ApiResult<string>> AddConfigAdvertisement(ConfigAdvertisementVo request);

        Task<ApiResult<ConfigAdvertisementVo>> GetConfigAdvertisement(ConfigAdvertisementVo request);
        Task<ApiResult<string>> UpdateConfigAdvertisement(ConfigAdvertisementVo request);

        Task<ApiPagedResult<GetShopPackageCardProductPageListVo>> GetShopPackageCardProductPageList(
          ApiRequest<GetShopPackageCardProductPageListRequest> request);

        Task<ApiResult> SaveShopPackageCard(ApiRequest<ConfigShopPackageCardDTO> request);

        Task<ApiResult<GetShopPackageCardProductPageListVo>> GetShopCardDetail(
           ApiRequest<GetShopCardDetailRequest> request);

    }
}
