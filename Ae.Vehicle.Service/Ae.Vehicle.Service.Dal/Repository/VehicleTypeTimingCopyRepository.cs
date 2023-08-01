using ApolloErp.Data.DapperExtensions;
using Ae.Vehicle.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Vehicle.Service.Dal.Repository
{
    public class VehicleTypeTimingCopyRepository : AbstractRepository<VehicleTypeTimingCopyDO>,
        IVehicleTypeTimingCopyRepository
    {
        public VehicleTypeTimingCopyRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<List<VehicleTypeTimingCopyDO>> GetAAllVehicleTypeTiming()
        {
            var result = await GetListAsync<VehicleTypeTimingCopyDO>("");

            return result?.ToList() ?? new List<VehicleTypeTimingCopyDO>();
        }
    }
}
