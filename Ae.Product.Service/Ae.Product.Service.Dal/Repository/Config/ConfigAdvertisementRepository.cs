using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Config
{
    public class ConfigAdvertisementRepository : AbstractRepository<ConfigAdvertisementDo>,
        IConfigAdvertisementRepository
    {
        public ConfigAdvertisementRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<List<ConfigAdvertisementDo>> GetValidConfigAdvertisement(string terminalType)
        {
            var para = new DynamicParameters();
            para.Add("@terminalType", terminalType);
            para.Add("@currentTime", DateTime.Now);
            string condition =
                "WHERE `terminal_type` = @terminalType AND `start_time` <= @currentTime AND end_time >= @currentTime  and is_online=1";

            var result = await GetListAsync(condition, para);

            return result?.OrderBy(_ => _.Rank)?.ToList() ?? new List<ConfigAdvertisementDo>();
        }
    }
}
