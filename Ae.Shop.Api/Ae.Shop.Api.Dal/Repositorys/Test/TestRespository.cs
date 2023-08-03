using Ae.Shop.Api.Dal.Model;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using DynamicParameters = Dapper.DynamicParameters;

namespace Ae.Shop.Api.Dal.Repositorys.Test
{
    public class TestRespository : AbstractRepository<PurchaseInfo>, ITestRespository
    {
        public TestRespository()
        {
            SetDbType(DbType.SqlServer);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }
        public async Task<PagedEntity<PurchaseInfo>> PurchaseInfo(int pageIndex, int pageSize, int shopId)
        {
            // 先按未读、后按创建时间
            var orderBy = "CreatedTime DESC";

            var parames = new DynamicParameters();
            var condtions = new StringBuilder();

            condtions.Append(" WHERE SHOPID=@ShopID");
            parames.Add("@ShopID", shopId);

            var result = await GetListPagedAsync(pageIndex, pageSize, condtions.ToString(), orderBy, parames, true);

            return result;
        }
    }
}
