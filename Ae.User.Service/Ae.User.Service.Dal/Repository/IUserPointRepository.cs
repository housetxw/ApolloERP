using ApolloErp.Data.DapperExtensions;
using Ae.User.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.User.Service.Dal.Repository
{
    public interface IUserPointRepository : IRepository<UserPointDO>
    {
        /// <summary>
        /// 获取用户当前积分 成长值
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<UserPointDO> GetUserPointAsync(string userId, bool readOnly = true);

        /// <summary>
        /// 批量获取用户积分
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        Task<List<UserPointDO>> GetUserPointByUserIds(List<string> userIds);

        /// <summary>
        /// 操作积分
        /// </summary>
        /// <param name="userPointDo"></param>
        /// <returns></returns>
        Task<bool> OperatePointPoint(UserPointDO userPointDo);

        /// <summary>
        /// 操作成长值
        /// </summary>
        /// <param name="userPointDo"></param>
        /// <returns></returns>
        Task<bool> OperateUserGrowth(UserPointDO userPointDo);
    }
}
