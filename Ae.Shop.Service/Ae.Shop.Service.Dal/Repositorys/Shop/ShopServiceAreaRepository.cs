using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions.Data;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public class ShopServiceAreaRepository : AbstractRepository<ShopServiceAreaDO>, IShopServiceAreaRepository
    {
        public ShopServiceAreaRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 门店服务区域配置
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceAreaDO>> GetShopServiceAreaByShopId(long shopId, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);
            var result = await GetListAsync<ShopServiceAreaDO>("WHERE shop_id = @shopId", para, !readOnly);
            return result?.ToList() ?? new List<ShopServiceAreaDO>();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<bool> DeleteShopServiceArea(List<long> idList, string updateBy)
        {
            string sql = @"UPDATE shop_service_area 
SET is_deleted = 1,
update_by = @updateBy,
update_time = NOW( ) 
WHERE
	id IN @ids;";

            var para = new DynamicParameters();
            para.Add("@ids", idList);
            para.Add("@updateBy", updateBy);

            bool result = false;

            await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para) > 0; });

            return result;
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="type"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<bool> DeleteShopServiceAreaByShopId(long shopId, int type, string updateBy)
        {
            string sql = @"UPDATE shop_service_area 
SET is_deleted = 1,
update_by = @updateBy,
update_time = NOW( ) 
WHERE
	shop_id = @shopId AND `type` = @type;";

            var para = new DynamicParameters();
            para.Add("@shopId", shopId);
            para.Add("@type", type);
            para.Add("@updateBy", updateBy);

            bool result = false;

            await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para) > 0; });

            return result;
        }

        /// <summary>
        /// shopId批量查询
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceAreaDO>> GetShopServiceAreaByShopIds(List<long> shopIds)
        {
            var para = new DynamicParameters();
            para.Add("@shopIds", shopIds);
            var result = await GetListAsync<ShopServiceAreaDO>("WHERE shop_id IN @shopIds", para);
            return result?.ToList() ?? new List<ShopServiceAreaDO>();
        }
    }
}
