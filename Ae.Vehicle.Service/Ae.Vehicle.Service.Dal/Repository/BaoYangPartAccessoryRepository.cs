using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public class BaoYangPartAccessoryRepository : AbstractRepository<BaoYangPartAccessoryDO>,
        IBaoYangPartAccessoryRepository
    {
        public BaoYangPartAccessoryRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<List<BaoYangPartAccessoryDO>> GetAllBaoYangAccessory()
        {
            var result = await GetListAsync<BaoYangPartAccessoryDO>("");

            return result.ToList();
        }
    }
}
