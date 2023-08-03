using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Dal.Repositorys.OrderComment
{
    public class CommentLabelConfigRepository : AbstractRepository<CommentLabelConfigDO>, ICommentLabelConfigRepository
    {
        public CommentLabelConfigRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSqlReadOnly");
        }

        /// <summary>
        /// 查询标签列表
        /// </summary>
        /// <param name="labels"></param>
        /// <returns></returns>
        public async Task<List<CommentLabelConfigDO>> GetCommentLabelListByIds(List<long> labels) 
        {
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE id in @Labels AND is_deleted = 0");

            var para = new DynamicParameters();
            para.Add("@Labels", labels);
            var result = await GetListAsync(condition.ToString(), para);
            return result.ToList();
        }

        /// <summary>
        /// 获取所哟标签
        /// </summary>
        /// <returns></returns>
        public async Task<List<CommentLabelConfigDO>> GetAllCommentLabelList()
        {
            var result = await GetListAsync<CommentLabelConfigDO>("WHERE is_valid = 1");

            return result?.ToList() ?? new List<CommentLabelConfigDO>();
        }
    }
}
