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
    public class UserPointRepository : AbstractRepository<UserPointDO>, IUserPointRepository
    {
        public UserPointRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("User");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserReadOnly");
        }

        /// <summary>
        /// 获取用户当前积分 成长值
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<UserPointDO> GetUserPointAsync(string userId, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@userId", userId);

            var result = await GetListAsync<UserPointDO>("WHERE user_id = @userId", para, !readOnly);

            return result.FirstOrDefault();
        }

        /// <summary>
        /// 批量获取用户积分
        /// </summary>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public async Task<List<UserPointDO>> GetUserPointByUserIds(List<string> userIds)
        {
            var para = new DynamicParameters();
            para.Add("@userIds", userIds);
            var result = await GetListAsync<UserPointDO>("WHERE user_id IN @userIds", para);
            return result?.ToList() ?? new List<UserPointDO>();
        }

        /// <summary>
        /// 操作积分
        /// </summary>
        /// <param name="userPointDo"></param>
        /// <returns></returns>
        public async Task<bool> OperatePointPoint(UserPointDO userPointDo)
        {
            if (userPointDo.Id > 0)
            {
                string sql = @"UPDATE user_point 
SET current_point = current_point + @pointValue,
update_by = @updatedBy,
update_time = NOW( ) 
WHERE
	id = @id;";
                var para = new DynamicParameters();
                para.Add("@id", userPointDo.Id);
                para.Add("@pointValue", userPointDo.CurrentPoint);
                para.Add("@updatedBy", userPointDo.UpdateBy);
                int result = 0;
                await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para); });
                return result > 0;
            }
            else
            {
                return await InsertAsync(userPointDo) > 0;
            }
        }

        /// <summary>
        /// 操作成长值
        /// </summary>
        /// <param name="userPointDo"></param>
        /// <returns></returns>
        public async Task<bool> OperateUserGrowth(UserPointDO userPointDo)
        {
            if (userPointDo.Id > 0)
            {
                string sql = @"UPDATE user_point 
SET current_growth_value =
CASE
	
	WHEN current_growth_value + @growthValue < 0 THEN
	0 ELSE current_growth_value + @growthValue 
	END,
	update_by = @updatedBy,
	update_time = NOW( ) 
WHERE
id = @id";
                var para = new DynamicParameters();
                para.Add("@id", userPointDo.Id);
                para.Add("@growthValue", userPointDo.CurrentGrowthValue);
                para.Add("@updatedBy", userPointDo.UpdateBy);
                int result = 0;
                await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para); });
                return result > 0;
            }
            else
            {
                return await InsertAsync(userPointDo) > 0;
            }
        }
    }
}
