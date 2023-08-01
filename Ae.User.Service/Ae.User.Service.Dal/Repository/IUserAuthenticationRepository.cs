using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.User.Service.Dal.Model;

namespace Ae.User.Service.Dal.Repository
{
    public interface IUserAuthenticationRepository : IRepository<UserAuthenticationDO>
    {
        /// <summary>
        /// 根据userId获取实名认证信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<UserAuthenticationDO> GetUserAuthentication(string userId, bool readOnly = true);
    }
}
