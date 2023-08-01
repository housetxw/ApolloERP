using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public class BaoYangPartsLiyangRepository : AbstractRepository<BaoYangPartsLiyangDO>,
        IBaoYangPartsLiyangRepository
    {
        public BaoYangPartsLiyangRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<List<BaoYangPartsLiyangDO>> GetAllBaoYangParts()
        {
            var result = await GetListAsync<BaoYangPartsLiyangDO>("");

            return result?.ToList() ?? new List<BaoYangPartsLiyangDO>();
        }
    }
}
