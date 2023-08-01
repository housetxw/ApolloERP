using ApolloErp.Data.DapperExtensions;
using Ae.User.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.User.Service.Dal.Repository
{
    public interface IUserPointRecordRepository : IRepository<UserPointRecordDO>
    {
        /// <summary>
        /// 获取用户积分记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<UserPointRecordDO>> GetUserPointRecordAsync(string userId);
    }
}
