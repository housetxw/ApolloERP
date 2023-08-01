using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class ServiceTypeEnumRepository : AbstractRepository<ServiceTypeEnumDO>,
        IServiceTypeEnumRepository
    {
        public ServiceTypeEnumRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<IEnumerable<ServiceTypeEnumDO>> GetServiceTypeEnum()
        {
            return await GetListAsync<ServiceTypeEnumDO>("");
        }
    }
}
