using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Data.DapperExtensions.Data;
using Ae.Receive.Service.Dal.Model.Arrival;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public class ShopArrivalCarReportRepository : AbstractRepository<ShopArrivalCarReportDO>,IShopArrivalCarReportRepository
    {

        public ShopArrivalCarReportRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        public async Task<ShopArrivalCarReportDO> GetShopArrivalCarReport(long arrivalId)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine(" where is_deleted=0");
            builder.AppendLine(" and  arrival_id=@ArrivalId");

            parameters.Add("@ArrivalId", arrivalId);

            IEnumerable<ShopArrivalCarReportDO> shopArrivalCancelDOs = new List<ShopArrivalCarReportDO>();
            shopArrivalCancelDOs = await GetListAsync(builder.ToString(), parameters, false);


            return shopArrivalCancelDOs?.ToList()?.FirstOrDefault();
        }

        public async Task<List<ShopArrivalCarReportDO>> GetShopArrivalCarReportList(List<long> recIds)
        {
            var para = new DynamicParameters();
            para.Add("@arrivalIds", recIds);
            var result = await GetListAsync<ShopArrivalCarReportDO>("WHERE arrival_id IN @arrivalIds", para);
            return result?.ToList() ?? new List<ShopArrivalCarReportDO>();
        }
    }
}
