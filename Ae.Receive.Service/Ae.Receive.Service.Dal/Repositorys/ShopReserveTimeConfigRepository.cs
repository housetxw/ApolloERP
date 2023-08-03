using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public class ShopReserveTimeConfigRepository : AbstractRepository<ShopReserveTimeConfigDO>, IShopReserveTimeConfigRepository
    {
        public ShopReserveTimeConfigRepository() 
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }


        /// <summary>
        /// 根据门店ID,日期查询配置信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<ShopReserveTimeConfigDO>> GetReserveTimeConfigByDateAsync(long shopId,int weekDay, string shortDate)
        {
            var para = new DynamicParameters();
            para.Add("@ShopId", shopId);

            para.Add("@ShortDate", shortDate);
            string sql = @"WHERE shop_id = @ShopId AND year_day = @ShortDate AND is_deleted = 0 AND config_type = 1";
            var result = await GetListAsync<ShopReserveTimeConfigDO>(sql, para);
            if (result == null || result.Count() == 0) 
            {
                if (!string.IsNullOrEmpty(shortDate))
                {
                    StringBuilder condition = new StringBuilder();
                    para.Add("@WeekDay", weekDay);
                    condition.Append("WHERE shop_id = @ShopId AND week_day = @WeekDay AND is_deleted = 0 AND config_type = 0");
                    result = await GetListAsync<ShopReserveTimeConfigDO>(condition.ToString(), para);
                }
            }
            return result?.ToList() ?? new List<ShopReserveTimeConfigDO>();
        }


        /// <summary>
        /// 根据门店ID日期查询配置信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<ShopReserveTimeConfigDO>> GetReserveTimeConfigByDateTypeAsync(long shopId, int weekDay, string shortDate,int type)
        {
            var para = new DynamicParameters();
            para.Add("@ShopId", shopId);
            para.Add("@ShortDate", shortDate);
            para.Add("@WeekDay", weekDay);
            string sql = @"WHERE shop_id = @ShopId AND week_day = @WeekDay AND is_deleted = 0 AND config_type = 0";
            if (!string.IsNullOrEmpty(shortDate) && type == 1)
            {
                sql = @"WHERE shop_id = @ShopId AND year_day = @ShortDate AND is_deleted = 0 AND config_type = 1";
            }
            var result = await GetListAsync<ShopReserveTimeConfigDO>(sql, para);
            return result?.ToList() ?? new List<ShopReserveTimeConfigDO>();
        }

        /// <summary>
        /// 删除门店预约配置
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="weekDay"></param>
        /// <param name="shortDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<int> DeleteReserveTimeConfigAsync(long shopId, int weekDay, string shortDate, int type)
        {
            var para = new DynamicParameters();
            para.Add("@ShopId", shopId);
            para.Add("@ShortDate", shortDate);
            para.Add("@WeekDay", weekDay);
            string sql = @"update shop_reserve_time_config set is_deleted = 1 
                        WHERE shop_id = @ShopId AND week_day = @WeekDay AND is_deleted = 0 AND config_type = 0";

            if (!string.IsNullOrEmpty(shortDate) && type == 1)
            {
                sql = @"update shop_reserve_time_config set is_deleted = 1
                        WHERE shop_id = @ShopId AND year_day = @ShortDate AND is_deleted = 0 AND config_type = 1";
            }
            int count = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, para);
            });
            return count;
        }



        /// <summary>
        /// 根据门店查 通用配置
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<ShopReserveTimeConfigDO>> GetDefaultReserveTimeConfigByShopId(long shopId)
        {
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);
            var result =
                await GetListAsync<ShopReserveTimeConfigDO>("WHERE shop_id = @shopId AND config_type = 0", para);
            return result?.ToList() ?? new List<ShopReserveTimeConfigDO>();
        }

        /// <summary>
        /// 根据门店和时间查自定义配置
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<ShopReserveTimeConfigDO>> GetDefinedReserveTimeConfigByShopId(long shopId,
            string startDate,
            string endDate)
        {
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);
            para.Add("@startDate", startDate);
            para.Add("@endDate", endDate);
            var result =
                await GetListAsync<ShopReserveTimeConfigDO>(
                    "WHERE shop_id = @shopId AND config_type = 1 AND year_day >= @startDate AND year_day < @endDate",
                    para);
            return result?.ToList() ?? new List<ShopReserveTimeConfigDO>();
        }

        /// <summary>
        /// 根据门店查一段时间配置
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<ShopReserveTimeConfigDO>> GetReserveTimeConfigByShopId(long shopId,string startDate,string endDate)
        {
            var para = new DynamicParameters();
            para.Add("@ShopId", shopId);
            para.Add("@StartDate", startDate);
            para.Add("@EndDate", endDate);
            string sql = @"
SELECT * FROM(
SELECT
	        week_day WeekDay,
	        year_day YearDay,
            config_type ConfigType,
	        SUM( reserve_count ) ReserveCount 
        FROM
	        shop_reserve_time_config 
        WHERE
	        shop_id = @ShopId
	        AND is_deleted = 0 
	        AND config_type = 1 
	        AND (year_day >= @StartDate
	        AND year_day < @EndDate )
        GROUP BY  YearDay 
UNION ALL
        SELECT
	        week_day WeekDay,
	        year_day YearDay,
            config_type ConfigType,
	        SUM( reserve_count ) ReserveCount 
        FROM
	        shop_reserve_time_config 
        WHERE
	        shop_id = @ShopId
	        AND is_deleted = 0 
	        AND config_type = 0 
        GROUP BY WeekDay
) t ";

            IEnumerable<ShopReserveTimeConfigDO> result = new List<ShopReserveTimeConfigDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = await conn.QueryAsync<ShopReserveTimeConfigDO>(sql, para);
            });
            return result?.ToList() ?? new List<ShopReserveTimeConfigDO>();
        }
    }
}
