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
    public class UserExpandShopRepository : AbstractRepository<UserExpandShopDo>, IUserExpandShopRepository
    {
        public UserExpandShopRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("User");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserReadOnly");
        }

        public async Task<UserExpandShopDo> GetDefaultUserExpandShop(string userId)
        {
            var para = new DynamicParameters();
            para.Add("@userId", userId);
            var result = await GetListAsync("WHERE user_id = @userId", para);
            return result?.FirstOrDefault();
        }

        public async Task<UserExpandShopDo> GetDefaultUserExpandShop(long shopId)
        {
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);
            var result = await GetListAsync("WHERE relation_shop_id = @shopId", para);
            return result?.FirstOrDefault();
        }
    }
}
