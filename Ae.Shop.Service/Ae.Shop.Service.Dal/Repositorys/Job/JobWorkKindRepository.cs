using Microsoft.Extensions.Configuration;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Repositorys.Job
{
    public class JobWorkKindRepository : AbstractRepository<JobWorkKindRelationDO>, IJobWorkKindRepository
    {
        private IConfiguration configuration;

        private readonly ApolloErpLogger<JobWorkKindRepository> logger;
        private readonly string className;

        public JobWorkKindRepository(ApolloErpLogger<JobWorkKindRepository> logger, IConfiguration configuration)
        {
            this.configuration = configuration;
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }
    }
}
