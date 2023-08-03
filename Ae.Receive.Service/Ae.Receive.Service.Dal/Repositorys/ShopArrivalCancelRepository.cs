using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public class ShopArrivalCancelRepository : AbstractRepository<ShopArrivalCancelDO>, IShopArrivalCancelRepository
    {
        public ShopArrivalCancelRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        public async Task<ShopArrivalCancelDO> GetArrivalCancel(long arrivalId)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine(" where is_deleted=0");
            builder.AppendLine(" and  arrival_id=@ArrivalId");

            parameters.Add("@ArrivalId", arrivalId);

            IEnumerable<ShopArrivalCancelDO> shopArrivalCancelDOs = new List<ShopArrivalCancelDO>();
            shopArrivalCancelDOs=await GetListAsync(builder.ToString(), parameters,false);
           

            return shopArrivalCancelDOs?.ToList()?.FirstOrDefault();
           
        }
    }
}
