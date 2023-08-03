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
    public class ShopArrivalVideoRepository : AbstractRepository<ShopArrivalVideoDO>, IShopArrivalVideoRepository
    {
        public ShopArrivalVideoRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 查询到店施工视频
        /// </summary>
        /// <param name="recId"></param>
        /// <returns></returns>
        public async Task<List<ShopArrivalVideoDO>> GetShopArrivalVideos(long recId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@recId", recId);
            var result = await GetListAsync<ShopArrivalVideoDO>("WHERE `arrival_id` = @recId", parameters);
            return result?.ToList() ?? new List<ShopArrivalVideoDO>();
        }
    }
}
