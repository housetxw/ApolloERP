using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class InsuranceCompanyRepository : AbstractRepository<InsuranceCompanyDO>, IInsuranceCompanyRepository
    {
        public InsuranceCompanyRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderConfigSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderConfigSqlReadOnly");
        }
    }
}
