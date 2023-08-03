using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Dal.Repositorys.OrderComment
{
    public interface ICommentImageRepository : IRepository<CommentImageDO>
    {
        /// <summary>
        /// 根据commentId获取评论图片列表
        /// </summary>
        /// <param name="commentIds"></param>
        /// <param name="relationType">1 客户初评 2回评</param>
        /// <returns></returns>
        Task<List<CommentImageDO>> GetCommentImgListByCommentId(long commentId, int relationType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentIds"></param>
        /// <returns></returns>
        Task<List<CommentImageDO>> GetCommentImgList(List<long> commentIds);
    }
}
