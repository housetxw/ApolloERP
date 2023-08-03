using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public class ShopAssetMaintainRepository : AbstractRepository<ShopAssetMaintainDO>, IShopAssetMaintainRepository
    {
        public ShopAssetMaintainRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }
    }
}
