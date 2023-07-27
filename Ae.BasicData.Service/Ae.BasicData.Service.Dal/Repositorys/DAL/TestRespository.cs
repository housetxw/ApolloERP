using Ae.BasicData.Service.Dal.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BasicData.Service.Dal.Repositorys.IDAL;
using DynamicParameters = Dapper.DynamicParameters;

namespace Ae.BasicData.Service.Dal.Repositorys.DAL
{
    public class TestRespository : AbstractRepository<PurchaseInfo>, ITestRespository
    {
        public TestRespository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.SqlServer);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ApolloErpSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ApolloErpSqlReadOnly");
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
