using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Repositorys.WMS
{
    public class ProductPackageRelationRepository : AbstractRepository<ProductPackageRelationDO>, IProductPackageRelationRepository
    {
        public ProductPackageRelationRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("WMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("WMSSqlReadOnly");
        }


    }


}
