using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Core.Request;
using Ae.OrderComment.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Dal.Repositorys.OrderComment
{
    public interface ICommentDetailShopRepository : IRepository<CommentDetailShopDO>
    {
        Task<int> GetShopCommentTotal(long shopId, int score);
        Task<PagedEntity<BaseCommentListDO>> GetShopCommentList(GetShopCommentListRequest request);
        /// <summary>
        /// 根据审核日期返回获取有新增评论的门店ID集合
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<long>> GetDistinctShopIds(DateTime startDate, DateTime endDate);
        /// <summary>
        /// 根据门店ID获取总评价数
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<int> GetCountByShopId(long shopId);
        /// <summary>
        /// 根据门店ID获取好评数
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<int> GetGoodCountByShopId(long shopId);

        /// <summary>
        /// 获取门店评分
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        Task<List<ShopScoreDO>> GetShopScore(List<int> shopIds);
    }
}
