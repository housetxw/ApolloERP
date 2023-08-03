using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Dal.Repositorys.OrderComment
{
    public interface ICommentLabelConfigRepository: IRepository<CommentLabelConfigDO>
    {
        /// <summary>
        /// 查询标签列表
        /// </summary>
        /// <param name="labels"></param>
        /// <returns></returns>
        Task<List<CommentLabelConfigDO>> GetCommentLabelListByIds(List<long> labels);

        /// <summary>
        /// 获取所哟标签
        /// </summary>
        /// <returns></returns>
        Task<List<CommentLabelConfigDO>> GetAllCommentLabelList();
    }
}
