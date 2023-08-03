using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Repositorys.HomeCare
{
    public class EmployeeRepository : AbstractRepository<EmployeeDO>, IEmployeeRepository
    {
        public EmployeeRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }
    }
}
