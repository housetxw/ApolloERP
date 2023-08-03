using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Dal.Repositorys.OrderComment
{
    public class CommentReplyRepository : AbstractRepository<CommentReplyDO>, ICommentReplyRepository
    {
        public CommentReplyRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSqlReadOnly");
        }
    }
}
