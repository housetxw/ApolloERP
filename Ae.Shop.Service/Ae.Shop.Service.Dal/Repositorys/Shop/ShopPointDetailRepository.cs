using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public class ShopPointDetailRepository : AbstractRepository<ShopPointDetailDo>, IShopPointDetailRepository
    {
        public ShopPointDetailRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 获取门店积分详情
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<ShopPointDetailDo>> GetShopPointDetailByShopId(int shopId)
        {
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);

            var result = await GetListAsync<ShopPointDetailDo>("WHERE `shop_id` = @shopId", para);

            return result?.AsList() ?? new List<ShopPointDetailDo>();
        }
    }
}
