using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Model.Promotion;
using Ae.Product.Service.Core.Request.Promotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Core.Interfaces
{
    /// <summary>
    /// 促销
    /// </summary>
    public interface IPromotionService
    {
        /// <summary>
        /// 促销列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<PromotionBriefVo>> GetPromotionList(PromotionListRequest request);

        /// <summary>
        /// 促销详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PromotionDetailVo> GetPromotionDetail(PromotionDetailRequest request);

        /// <summary>
        /// 结束促销
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> FinishPromotion(FinishPromotionRequest request);

        /// <summary>
        /// 商品促销详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductPromotionDetailVo> GetProductPromotionDetail(ProductPromotionDetailRequest request);

        /// <summary>
        /// 活动列表查询
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
        /// 审核通过
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AuditPromotionActivity(AuditPromotionActivityRequest request);

        /// <summary>
        /// 根据商品Pid查询促销信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ProductPromotionInfoVo>> GetPromotionActivitByProductCodeList(PromotionActivitByProductCodeListRequest request);

        /// <summary>
        /// 单个商品查询促销信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="promotionChannel"></param>
        /// <returns></returns>
        Task<ProductPromotionVo> GetPromotionActivityByPid(string productCode,
            string promotionChannel);

        /// <summary>
        /// 更新促销商品数量
        /// </summary>
        /// <returns></returns>
        Task<bool> InsertPromotionActivityOrder(InsertPromotionActivityOrderRequest request);

        /// <summary>
        /// 促销详情（包含促销商品信息）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PromotionActivityDetailVo> GetPromotionActivityDetail(PromotionActivityDetailRequest request);

        /// <summary>
        /// 根据订单号查询商品参加的活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ProductActivityVo>> GetProductActivityByOrderNos(ProductActivityByOrderNosRequest request);
    }
}
