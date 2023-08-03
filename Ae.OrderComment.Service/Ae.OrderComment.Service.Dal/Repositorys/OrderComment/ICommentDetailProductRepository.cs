using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Core.Request;
using Ae.OrderComment.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Dal.Repositorys.OrderComment
{
    public interface ICommentDetailProductRepository : IRepository<CommentDetailProductDO>
    {
        Task<int> GetProductCommentTotal(string productId, int score);
        Task<PagedEntity<BaseCommentListDO>> GetProductCommentList(GetProductCommentListRequest request);
        /// <summary>
        /// 根据审核日期返回获取有新增评论的商品ID集合
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Task<List<string>> GetDistinctCommentDetailProductIds(DateTime startDate, DateTime endDate);
        /// <summary>
        /// 根据商品ID获取总评价数
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<int> GetCountByProductId(string productId);
        /// <summary>
        /// 根据商品ID获取好评数
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<int> GetGoodCountByProductId(string productId);
    }
}
