using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public class ShopLogRepository : AbstractRepository<ShopLogDO>, IShopLogRepository
    {
        public ShopLogRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }
        public async Task<List<ShopLogDTO>> GetShopLog(long shopId)
        {
            string sql = @"select filter1 AS ShopId,type1 AS OperateType,create_time AS CreateTime  ,create_by AS CreateBy,summary AS Summary  from shop_log where filter1=@ShopId
                            GROUP BY filter1,type1
                            ORDER BY create_time DESC";
            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();

            para.Add("@ShopId", shopId);

            IEnumerable<ShopLogDTO> list = new List<ShopLogDTO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                list = await conn.QueryAsync<ShopLogDTO>(sql, para);
            });
            return list?.AsList();
        }
    }
}
