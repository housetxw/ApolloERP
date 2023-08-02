using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Model.Coupon;
using Ae.B.Product.Api.Core.Model.Promotion;
using Ae.B.Product.Api.Core.Model.ShopProduct;
using Ae.B.Product.Api.Core.Request.Coupon;
using Ae.B.Product.Api.Core.Request.Promotion;
using Ae.B.Product.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Core.Interfaces
{
    /// <summary>
    /// 促销Service
    /// </summary>
    public interface IPromotionService
    {
        /// <summary>
        /// 促销活动列表搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<PromotionActivityListVo>> SearchPromotionActivity(SearchPromotionActivityRequest request);

        /// <summary>
        /// 新增促销活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> AddPromotionActivity(AddPromotionActivityRequest request);

        /// <summary>
        /// 促销详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PromotionDetailVo> GetPromotionDetail(PromotionDetailRequest request);

        /// <summary>
        /// 查询门店商品by编码
        /// </summary>
        /// <param name="shopProductCode"></param>
        /// <returns></returns>
        Task<ShopProductVo> GetShopProductByCode(string shopProductCode);

        /// <summary>
        /// 结束活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> FinishPromotion(FinishPromotionRequest request);

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AuditPromotionActivity(AuditPromotionActivityRequest request);

        Task<ApiPagedResult<GetCouponActivityListForShopResponse>> GetCouponActivityListForShop(
           ApiRequest<GetCouponActivityListForShopRequest> request);

        Task<ApiResult<bool>> SaveShopGrantCoupon(ApiRequest<ShopGrantCouponDTO> request);
    }
}
