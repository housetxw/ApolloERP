using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public class BaoYangPartAccessoryLiyangRepository : AbstractRepository<BaoYangPartAccessoryLiyangDO>,
        IBaoYangPartAccessoryLiyangRepository
    {
        public BaoYangPartAccessoryLiyangRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<List<BaoYangPartAccessoryLiyangDO>> GetAllPartAccessory()
        {
            var result = await GetListAsync<BaoYangPartAccessoryLiyangDO>("");

            return result?.ToList() ?? new List<BaoYangPartAccessoryLiyangDO>();
        }
    }
}
