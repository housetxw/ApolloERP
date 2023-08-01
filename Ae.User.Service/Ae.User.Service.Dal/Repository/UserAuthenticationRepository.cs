using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.User.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.User.Service.Dal.Repository
{
    public class UserAuthenticationRepository : AbstractRepository<UserAuthenticationDO>, IUserAuthenticationRepository
    {
        public UserAuthenticationRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("User");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserReadOnly");
        }

        /// <summary>
        /// 根据userId获取实名认证信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<UserAuthenticationDO> GetUserAuthentication(string userId, bool readOnly = true)
        {
            var param = new DynamicParameters();
            param.Add("@userId", userId);
            var result = await GetListAsync("WHERE `user_id` = @userId", param, !readOnly);
            return result?.FirstOrDefault();
        }
    }
}
