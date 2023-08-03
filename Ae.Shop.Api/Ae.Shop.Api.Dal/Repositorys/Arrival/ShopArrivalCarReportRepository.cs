using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Arrival
{
    public class ShopArrivalCarReportRepository : AbstractRepository<ShopArrivalCarReportDO>,
        IShopArrivalCarReportRepository
    {
        public ShopArrivalCarReportRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        public async Task<ShopArrivalCarReportDO> GetCarReportByRecId(long recId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@recId", recId);
            var result = await GetListAsync<ShopArrivalCarReportDO>("WHERE arrival_id = @recId", parameters);

            return result?.OrderByDescending(_ => _.Id)?.FirstOrDefault();
        }
    }
}
