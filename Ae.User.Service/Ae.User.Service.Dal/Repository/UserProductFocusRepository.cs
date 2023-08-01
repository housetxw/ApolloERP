using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.User.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.User.Service.Dal.Repository
{
    public class UserProductFocusRepository : AbstractRepository<UserProductFocusDO>, IUserProductFocusRepository
    {
        public UserProductFocusRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("User");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserReadOnly");
        }

        /// <summary>
        /// 查询用户已关注商品
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<UserProductFocusDO>> GetUserAttentionProductAsync(string userId)
        {
            var para = new DynamicParameters();
            para.Add("@userId", userId);
            var result = await GetListAsync<UserProductFocusDO>("WHERE user_id = @userId", para);

            return result;
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pidList"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserProductAsync(string userId, List<string> pidList, string submitBy)
        {
            string sql = @"UPDATE user_product_focus 
SET is_deleted = 1,
update_by = @update_by,
update_time = NOW( ) 
WHERE
	user_id = @userId 
	AND pid IN @pidList 
	AND is_deleted = 0;";

            var para = new DynamicParameters();
            para.Add("@update_by", submitBy);
            para.Add("@userId", userId);
            para.Add("@pidList", pidList);

            int id = 0;
            await OpenConnectionAsync(async conn => { id = await conn.ExecuteAsync(sql, para); });
            return id > 0;
        }
    }
}
