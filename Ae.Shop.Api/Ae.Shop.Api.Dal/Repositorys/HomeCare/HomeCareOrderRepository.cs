using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public class HomeCareOrderRepository : AbstractRepository<HomeCareOrderDO>, IHomeCareOrderRepository
    {
        public HomeCareOrderRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSqlReadOnly");
        }
    }
}
