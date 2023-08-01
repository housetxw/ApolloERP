using ApolloErp.Data.DapperExtensions;
using Ae.User.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.User.Service.Dal.Repository
{
    public interface IUserProductFocusRepository : IRepository<UserProductFocusDO>
    {
        /// <summary>
        /// 查询用户已关注商品
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<UserProductFocusDO>> GetUserAttentionProductAsync(string userId);

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pidList"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        Task<bool> DeleteUserProductAsync(string userId, List<string> pidList, string submitBy);
    }
}
