using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Model.Product;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Core.Request.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Core.Interfaces
{
    public interface IPageConfigService
    {
        Task<ApiResult<string>> DeleteConfigAdvertisement(ConfigAdvertisementVo request);

        Task<ApiPagedResult<ConfigAdvertisementVo>> GetConfigAdvertisements(GetConfigAdvertisementsRequest request);

        Task<ApiResult<string>> AddConfigAdvertisement(ConfigAdvertisementVo request);

        Task<ApiResult<ConfigAdvertisementVo>> GetConfigAdvertisement(ConfigAdvertisementVo request);

        /// <summary>
        /// 美容团购商品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GrouponProductVo>> GetGrouponProductPageList(
            GrouponProductPageListRequest request);

        /// <summary>
        /// 搜索产品For美容团购
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<GrouponProductVo>> SearchProductForGroupon(
            SearchProductForGrouponRequest request);

        /// <summary>
        /// 新增团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddGrouponProduct(AddGrouponProductRequest request);

        /// <summary>
        /// 编辑美容团购商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditGrouponProduct(EditGrouponProductRequest request);

        Task<ApiResult<string>> UpdateConfigAdvertisement(ConfigAdvertisementVo request);
    }
}
