using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Model.Promotion;
using Ae.B.Product.Api.Client.Model.ShopProduc;
using Ae.B.Product.Api.Client.Request.Promotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Client.Interfaces
{
    public interface IPromotionClient
    {
        /// <summary>
        /// 活动列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<PromotionActivityListDto>> SearchPromotionActivity(
            SearchPromotionActivityClientRequest request);

        /// <summary>
        /// 新增促销活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> AddPromotionActivity(AddPromotionActivityClientRequest request);

        /// <summary>
        /// 促销详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PromotionActivityDetailDto> GetPromotionActivityDetail(PromotionActivityDetailClientRequest request);

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AuditPromotionActivity(AuditPromotionActivityClientRequest request);

        /// <summary>
        /// 查询单个门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ShopProductDto> GetShopProductByCode(string shopProductCode);

        /// <summary>
        /// 结束活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> FinishPromotion(FinishPromotionClientRequest request);
    }
}
