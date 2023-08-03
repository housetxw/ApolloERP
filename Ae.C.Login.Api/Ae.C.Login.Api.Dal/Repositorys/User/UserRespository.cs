using Ae.C.Login.Api.Dal.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using DynamicParameters = Dapper.DynamicParameters;
using Ae.C.Login.Api.Dal.Repositorys.User;
using System.Transactions;

namespace Ae.C.Login.Api.Dal.Repositorys.User
{
    public class UserRespository : AbstractRepository<UserDO>, IUserRespository
    {

        public UserRespository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("UserSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserSqlReadOnly");
        }
        public async Task<UserInfoDO> GetUserInfoByOpenId(string openId,int loginType)
        {
            var parames = new DynamicParameters();
            parames.Add("@open_id", openId);
            parames.Add("@login_type", loginType);

            var sql = @"select u.id as UserId, u.user_name as UserName, u.nick_name as NickName,u.email as Email, 
                        u.mobile_number as MobileNumber,u.head_url as HeadUrl,u.state as State,t.open_id as OpenId,t.id as ThirdId
                        from user_third as t  inner join user as u on t.user_id = u.id and t.is_deleted = 0 and u.is_deleted = 0 
                        where t.login_type = @login_type and t.open_id = @open_id";

            UserInfoDO result = new UserInfoDO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = await conn.QueryFirstOrDefaultAsync<UserInfoDO>(sql, parames);
            });
            return result;
        }

        public async Task<UserInfoDO> GetUserInfoByMobile(string mobile, int loginType)
        {
            var parames = new DynamicParameters();
            parames.Add("@mobile_number", mobile);
            parames.Add("@login_type", loginType);

            var sql = @"select u.id as UserId, u.user_name as UserName, u.nick_name as NickName,u.email as Email, 
                        u.mobile_number as MobileNumber,u.head_url as HeadUrl,u.state as State,t.open_id as OpenId,t.id as ThirdId
                        from user_third as t  right join user as u on t.user_id = u.id and  t.login_type = @login_type and t.is_deleted = 0  
                        where  u.is_deleted = 0 and u.mobile_number = @mobile_number";

            UserInfoDO result = new UserInfoDO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = await conn.QueryFirstOrDefaultAsync<UserInfoDO>(sql, parames);
            });
            return result;
        }

        public async Task<UserInfoDO> GetUserInfoByMobile(string mobile)
        {
            var parames = new DynamicParameters();
            parames.Add("@mobile_number", mobile);

            var sql = @"select u.id as UserId, u.user_name as UserName, u.nick_name as NickName,u.email as Email, 
                        u.mobile_number as MobileNumber,u.head_url as HeadUrl,u.state as State 
                        from user as u where  u.is_deleted = 0 
                        and u.mobile_number = @mobile_number";

            UserInfoDO result = new UserInfoDO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = await conn.QueryFirstOrDefaultAsync<UserInfoDO>(sql, parames);
            });
            return result;
        }

        public async Task<bool> CreateUser(UserDO user)
        {
            //注册新用户
            return await InsertAsync<Guid>(user)!=null;
        }

        public async Task<bool> UpdateUserMobileByUserId(UserDO req)
        {
            var param = new List<string>
            {
                "MobileNumber",
                "UpdateBy"
            };
            return await UpdateAsync(req, param) >= 0;
        }
       
    }
}
