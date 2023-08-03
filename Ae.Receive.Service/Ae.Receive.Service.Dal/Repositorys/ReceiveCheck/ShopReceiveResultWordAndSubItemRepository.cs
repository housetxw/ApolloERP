using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public class ShopReceiveResultWordAndSubItemRepository : AbstractRepository<ShopReceiveResultWordAndSubItemDo>, IShopReceiveResultWordAndSubItemRepository
    {
        public ShopReceiveResultWordAndSubItemRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 结果-结果词
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopReceiveResultWordAndSubItemDo>> GetShopReceiveResultWordAndSubItem()
        {
            var result = await GetListAsync<ShopReceiveResultWordAndSubItemDo>("");

            return result?.ToList() ?? new List<ShopReceiveResultWordAndSubItemDo>();
        }
    }
}
