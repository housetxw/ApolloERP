using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public class UserCarComponentsRepository : AbstractRepository<UserCarComponentsDo>, IUserCarComponentsRepository
    {
        public UserCarComponentsRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("User");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserReadOnly");
        }

        /// <summary>
        /// 获取车辆部件
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserCarComponentsDo>> GetCarComponents()
        {
            var result = await GetListAsync<UserCarComponentsDo>("");

            return result?.ToList() ?? new List<UserCarComponentsDo>();
        }
    }
}
