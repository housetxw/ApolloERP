using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Promotion
{
    public interface IPromotionActivityRepository : IRepository<PromotionActivityDo>
    {
        /// <summary>
        /// 活动列表搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<PromotionActivityDo>> SearchPromotionActivity(SearchPromotionCondition request);

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<PromotionActivityDo> GetPromotionDetail(string activityId, bool readOnly = true);

        /// <summary>
        /// 根据Pid查询活动
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<List<ProductPromotionDo>> GetPromotionByPidList(List<string> pidList, bool readOnly = true);
    }
}
