using Dapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Core.Request;
using Ae.OrderComment.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ae.OrderComment.Service.Core.Enums;
using Ae.OrderComment.Service.Core.Request.OrderCommentForApp;
using Ae.OrderComment.Service.Core.Response.OrderCommentForApp;


namespace Ae.OrderComment.Service.Dal.Repositorys.OrderComment
{
    public interface IOrderCommentRepository : IRepository<CommentDO>
    {
        Task<PagedEntity<GetOrderCommentForProductDO>> GetOrderCommentForProductList(GetOrderCommentForProductRequest request);

        Task<PagedEntity<GetOrderCommentForShopDO>> GetOrderCommentForShopList(GetOrderCommentForShopRequest request);

        Task<PagedEntity<GetOrderCommentForTechDO>> GetOrderCommentForTechList(GetOrderCommentForTechRequest request);

        Task<List<CommentImageDO>> GetCommentImages(GetCommentImageRequest request);

        Task<int> CheckOrderComment(CheckOrderCommentRequest request);

        Task<int> CheckOrderReply(CheckOrderCommentRequest request);

        Task<PagedEntity<GetOrderCommentForReplyDO>> GetOrderCommentForReplyList(GetOrderCommentForReplyRequest request);

        Task<List<ProductCommentTotalDO>> GetProductCommentTotal(GetProductCommentTotal request);

        /// <summary>
        /// 得到点评信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<GetCommentListDO>> GetCommentList(GetCommentListRequest request);

        /// <summary>
        /// 得到回评列表
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<CommentReplyDO>> GetCommentReplyList(List<long> ids, long shopId);

        /// <summary>
        /// 得到评价的图片信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<CommentImageDO>> GetCommentImageList(List<long> ids, int relationType);

        /// <summary>
        /// 得到点评信息
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<CommentDO>> GetCommentListByCommentIds(List<long> Ids, long shopId, int checkStatus);

        /// <summary>
        /// 得到点评信息
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<List<GetCommentListDO>> GetCommentListByCommentIdsForScore(List<long> Ids, long shopId, int checkStatus);

        /// <summary>
        /// 提交评论
        /// </summary>
        /// <param name="commentDo"></param>
        /// <param name="commentReplyDo"></param>
        /// <returns></returns>
        Task<long> SaveCommentReplyInfo(CommentDO commentDo, CommentReplyDO commentReplyDo, string createBy);


        /// <summary>
        /// 得到今日好评
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<int> GetTodayGoodCommentNum(int shopId);


        /// <summary>
        /// 今日差评数量
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<int> GetTodayNavigateCommentNum(int shopId);

        /// <summary>
        /// 得到今日评论数量
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        Task<GetTodayCommentResponse> GetTodayCommentCount(int shopId);

        Task<PagedEntity<CommentDO>> GetMyCommentList(GetMyCommentListRequest request);

        Task<List<CommentReplyDO>> GetCommentReplyListByCommentId(long commentId);

        /// <summary>
        /// 获得追评的commentDO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<CommentDO>> GetAppendCommentDo(long id, int shopId);

    }
}
