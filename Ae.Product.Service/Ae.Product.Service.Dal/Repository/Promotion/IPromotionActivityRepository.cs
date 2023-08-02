using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Promotion
{
    public interface IPromotionActivityRepository : IRepository<PromotionActivityDo>
    {
        /// <summary>
        /// 获取进行中的活动
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<Tuple<int, List<PromotionDetailDo>>> GetValidPromotionActivity(long shopId, int pageIndex, int pageSize);

        /// <summary>
        /// 获取已结束的活动
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<Tuple<int, List<PromotionDetailDo>>> GetFinishPromotionActivity(long shopId, int pageIndex, int pageSize);

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<PromotionActivityDo> GetPromotionDetail(string activityId, bool readOnly = true);

        /// <summary>
        /// 活动列表搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<PromotionActivityDo>> SearchPromotionActivity(SearchPromotionCondition request);

        /// <summary>
        /// 根据Pid查询活动
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<List<ProductPromotionDo>> GetPromotionByPidList(List<string> pidList, bool readOnly = true);
    }
}
