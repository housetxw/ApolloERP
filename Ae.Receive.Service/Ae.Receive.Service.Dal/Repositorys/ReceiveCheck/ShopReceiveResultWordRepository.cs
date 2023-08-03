using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public class ShopReceiveResultWordRepository : AbstractRepository<ShopReceiveResultWordDo>, IShopReceiveResultWordRepository
    {
        public ShopReceiveResultWordRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 结果词
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopReceiveResultWordDo>> GetShopReceiveResultWord()
        {
            var result = await GetListAsync<ShopReceiveResultWordDo>("");

            return result?.ToList() ?? new List<ShopReceiveResultWordDo>();
        }
    }
}
