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
using System.Transactions;

namespace Ae.C.Login.Api.Dal.Repositorys.UserThird
{
    public class UserThirdRespository : AbstractRepository<UserThirdDO>, IUserThirdRespository
    {
        public UserThirdRespository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("UserSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserSqlReadOnly");
        }
        public async Task<bool> UpdateOpenIdById(UserThirdDO req)
        {
            var param = new List<string>
            {
                "OpenId",
                "UpdateBy"
            };
            return await UpdateAsync(req, param) >= 0;
        }

        public async Task<bool> InsertUserThird(UserThirdDO req)
        {
            return await InsertAsync(req) > 0;
        }
    }
}
