using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Dal.Repositorys.OrderComment
{
    public interface ICommentRewardRuleConfigRepository: IRepository<CommentRewardRuleConfigDO>
    {
        /// <summary>
        /// 根据会员等级，奖励类型查询评论奖励规则
        /// </summary>
        /// <param name="level"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<CommentRewardRuleConfigDO> GetCommentRewardRuleInfoAsync(int level, int type);
    }
}
