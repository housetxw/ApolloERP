using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public class ShopAssetTypeRepository : AbstractRepository<ShopAssetTypeDO>, IShopAssetTypeRepository
    {
        public ShopAssetTypeRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        public async Task<List<ShopAssetTypeDO>> GetShopAssetTypesByValues(List<string> values)
        {
            var condition = new StringBuilder("where value in @Values");
            var list = await GetListAsync(condition.ToString(), new { Values = values });
            return list?.ToList();
        }
    }
}
