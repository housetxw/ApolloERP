using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.User.Service.Dal.Model;

namespace Ae.User.Service.Dal.Repository
{
    public class UserPointRecordRepository : AbstractRepository<UserPointRecordDO>, IUserPointRecordRepository
    {
        public UserPointRecordRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("User");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserReadOnly");
        }

        /// <summary>
        /// 获取用户积分记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserPointRecordDO>> GetUserPointRecordAsync(string userId)
        {
            var para = new DynamicParameters();
            para.Add("@userId", userId);
            var result = await GetListAsync<UserPointRecordDO>("WHERE user_id = @userId and point_value <> 0  and is_deleted =0 ", para);

            return result;
        }
    }
}
