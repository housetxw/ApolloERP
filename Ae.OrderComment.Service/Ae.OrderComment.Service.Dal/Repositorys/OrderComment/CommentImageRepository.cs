using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Ae.OrderComment.Service.Dal.Repositorys.OrderComment
{
    public class CommentImageRepository : AbstractRepository<CommentImageDO>, ICommentImageRepository
    {
        public CommentImageRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSqlReadOnly");
        }


        /// <summary>
        /// 根据commentId获取评论图片列表
        /// </summary>
        /// <param name="commentIds"></param>
        /// <param name="relationType">1 客户初评 2回评</param>
        /// <returns></returns>
        public async Task<List<CommentImageDO>> GetCommentImgListByCommentId(long commentId, int relationType)
        {
            var condition = "where is_deleted = 0 AND relation_type = @RelationType AND comment_id = @CommentId";
            var paras = new
            {
                CommentId = commentId,
                RelationType = relationType
            };
            var list = await GetListAsync(condition, paras);
            return list.ToList();
        }

        public async Task<List<CommentImageDO>> GetCommentImgList(List<long> commentIds)
        {
            var condition = "where comment_id IN @CommentId";
            var param = new DynamicParameters();
            param.Add("@CommentId", commentIds);
            var list = await GetListAsync(condition, param);
            return list.ToList();
        }
    }
}
