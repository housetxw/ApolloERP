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
    public class CommentRewardRuleConfigRepository : AbstractRepository<CommentRewardRuleConfigDO>, ICommentRewardRuleConfigRepository
    {
        public CommentRewardRuleConfigRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSqlReadOnly");

        }
        /// <summary>
        /// 根据会员等级，奖励类型查询评论奖励规则
        /// </summary>
        /// <param name="level"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<CommentRewardRuleConfigDO> GetCommentRewardRuleInfoAsync(int level,int type) 
        {
            string condition = @"where reward_type =@Type AND member_level=@Level ";
            var para = new DynamicParameters();
            para.Add("@Level", level);
            para.Add("@Type", type);

            var ruleList = await GetListAsync(condition, para);
            return ruleList?.FirstOrDefault();
        }
    }
}
