using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class VehicleFuelTypeConfigRepository : AbstractRepository<VehicleFuelTypeConfigDO>,
        IVehicleFuelTypeConfigRepository
    {
        public VehicleFuelTypeConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 获取机油类别保养项目黑名单
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<VehicleFuelTypeConfigDO>> GetVehicleFuelTypeConfigAsync()
        {
            var result = await GetListAsync<VehicleFuelTypeConfigDO>("");

            return result;
        }
    }
}
