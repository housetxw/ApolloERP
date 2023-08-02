using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Promotion
{
    public interface IPromotionActivityProductRepository : IRepository<PromotionActivityProductDo>
    {
        /// <summary>
        /// 活动商品查询批量
        /// </summary>
        /// <param name="activityIds"></param>
        /// <returns></returns>
        Task<List<PromotionActivityProductDo>> GetPromotionProductByActivityIds(List<Guid> activityIds);

        /// <summary>
        /// 活动商品明细
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        Task<List<PromotionActivityProductDo>> GetPromotionProductByActivityId(string activityId);

        /// <summary>
        /// 活动商品详情
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="pid"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<PromotionActivityProductDo> GetPromotionProductDetail(string activityId, string pid, bool readOnly = true);

        /// <summary>
        /// 根据Pid查询活动
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<List<PromotionActivityProductDo>> GetPromotionProductByPid(string pid);
    }
}
