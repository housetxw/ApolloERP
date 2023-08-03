using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Promotion
{
    public interface IPromotionActivityProductRepository : IRepository<PromotionActivityProductDo>
    {
        /// <summary>
        /// 根据Pid查询活动
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<List<PromotionActivityProductDo>> GetPromotionProductByPid(string pid);

        /// <summary>
        /// 活动商品明细
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        Task<List<PromotionActivityProductDo>> GetPromotionProductByActivityId(string activityId);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        Task<bool> DeletePromotionProduct(string activityId, string submitBy);
    }
}
