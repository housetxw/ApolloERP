using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public class ShopReceiveCheckSubItemRepository : AbstractRepository<ShopReceiveCheckSubItemDo>, IShopReceiveCheckSubItemRepository
    {
        public ShopReceiveCheckSubItemRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 配置项关联结果
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopReceiveCheckSubItemDo>> GetShopReceiveCheckSubItem()
        {
            var result = await GetListAsync<ShopReceiveCheckSubItemDo>("");

            return result?.ToList() ?? new List<ShopReceiveCheckSubItemDo>();
        }
    }
}
