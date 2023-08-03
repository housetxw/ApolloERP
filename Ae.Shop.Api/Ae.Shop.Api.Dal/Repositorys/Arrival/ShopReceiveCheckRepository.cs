using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.Shop.Api.Core.Model.Arrival;
using Ae.Shop.Api.Dal.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Arrival
{
    public class ShopReceiveCheckRepository : AbstractRepository<ShopReceiveCheckDO>, IShopReceiveCheckRepository
    {
        private readonly ApolloErpLogger<ShopReceiveCheckRepository> logger;
        private readonly string className;
        public ShopReceiveCheckRepository(ApolloErpLogger<ShopReceiveCheckRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }

        /// <summary>
        /// 得到门店到店检测信息
        /// </summary>
        /// <param name="receiveId"></param>
        /// <returns></returns>
        public async Task<List<ShopReceiveCheckDO>> GetShopReceiveCheckInfo(List<long> receiveIds)
        {
            IEnumerable<ShopReceiveCheckDO> list = new List<ShopReceiveCheckDO>();

            var shopReceiveChecks = await GetListAsync<ShopReceiveCheckDO>("where receive_id in @ReceiveIds", new { ReceiveIds = receiveIds });

            return shopReceiveChecks?.ToList();

        }
    }
}
