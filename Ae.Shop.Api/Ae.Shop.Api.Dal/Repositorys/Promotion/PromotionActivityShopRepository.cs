using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Repositorys.Promotion
{
    public class PromotionActivityShopRepository : AbstractRepository<PromotionActivityShopDo>, IPromotionActivityShopRepository
    {
        public PromotionActivityShopRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ProductSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductSqlReadOnly");
        }
    }
}
