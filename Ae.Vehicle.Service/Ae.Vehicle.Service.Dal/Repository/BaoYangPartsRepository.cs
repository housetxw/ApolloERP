using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public class BaoYangPartsRepository : AbstractRepository<BaoYangPartsDO>, IBaoYangPartsRepository
    {
        public BaoYangPartsRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<List<BaoYangPartsDO>> GetAllBaoYangParts()
        {
            var result = await GetListAsync<BaoYangPartsDO>("");

            return result?.ToList() ?? new List<BaoYangPartsDO>();
        }
    }
}
