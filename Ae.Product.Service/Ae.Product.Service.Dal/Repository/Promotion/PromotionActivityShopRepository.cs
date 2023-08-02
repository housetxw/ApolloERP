using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Repository.Promotion
{
    public class PromotionActivityShopRepository : AbstractRepository<PromotionActivityShopDo>, IPromotionActivityShopRepository
    {
        public PromotionActivityShopRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }
    }
}
