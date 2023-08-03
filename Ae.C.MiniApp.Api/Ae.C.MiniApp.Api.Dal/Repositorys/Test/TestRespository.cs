using Rg.C.MiniApp.Api.Dal.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedGoose.Data.DapperExtensions;
using DynamicParameters = Dapper.DynamicParameters;
using Rg.C.MiniApp.Api.Dal.Repositorys.Test;

namespace Rg.C.MiniApp.Api.Dal.Repositorys.Test
{
    public class TestRespository : AbstractRepository<PurchaseInfo>, ITestRespository
    {
        public TestRespository()
        {
            SetDbType(RedGoose.Data.DapperExtensions.DbType.SqlServer);
            ConnectionString = ConnectionStringConfig.GetConnectionString("RedGooseSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("RedGooseSqlReadOnly");
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
