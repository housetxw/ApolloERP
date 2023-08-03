using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public class ShopAssetDiscardRepository : AbstractRepository<ShopAssetDiscardDO>, IShopAssetDiscardRepository
    {
        public ShopAssetDiscardRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }
    }
}
