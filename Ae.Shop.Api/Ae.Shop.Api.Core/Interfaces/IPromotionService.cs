using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model.Promotion;
using Ae.Shop.Api.Core.Request.Promotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    /// <summary>
    /// 促销Service
    /// </summary>
    public interface IPromotionService
    {
        /// <summary>
        /// 活动列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<PromotionActivityListVo>> SearchPromotionActivity(SearchPromotionActivityRequest request);

        /// <summary>
        /// 促销详情（包含促销商品信息）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        Task<PromotionActivityDetailVo> GetPromotionActivityDetail(PromotionActivityDetailRequest request);

        /// <summary>
        /// 查询门店商品by编码
        /// </summary>
        /// <param name="request">门店商品编码</param>
        /// <returns></returns>
        Task<ShopProductVo> GetShopProductByCode(ShopProductByCodeRequest request);

        /// <summary>
        /// 商品搜索（根据Pid或商品名称）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopProductVo>> SearchProductByNameOrCode(SearchProductByNameOrCodeRequest request);

        /// <summary>
        /// 新增促销活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<string> AddPromotionActivity(AddPromotionActivityRequest request);

        /// <summary>
        /// 结束活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> FinishPromotion(FinishPromotionRequest request);

        /// <summary>
        /// 编辑促销
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditPromotion(EditPromotionRequest request);

        /// <summary>
        /// 查询门店美容团购产品
        /// </summary>
        /// <param name="request"></param>
        Task<ApiPagedResultData<GrouponProductPageVo>> GetGrouponProductPageList(GrouponProductPageListRequest request);

        /// <summary>
        /// 美容团购产品详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<GrouponProductPageVo> GetGrouponProductDetail(GrouponProductDetailRequest request);

        /// <summary>
        /// 保存门店美容团购产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveShopGrouponProduct(SaveShopGrouponProductRequest request);
    }
}
