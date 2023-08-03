using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public class ShopAssetRepository : AbstractRepository<ShopAssetDO>, IShopAssetRepository
    {
        public ShopAssetRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }
    }
}
