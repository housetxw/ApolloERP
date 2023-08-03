using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Promotion
{
    public class PromotionActivityProductRepository: AbstractRepository<PromotionActivityProductDo>, IPromotionActivityProductRepository
    {
        public PromotionActivityProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ProductSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductSqlReadOnly");
        }

        /// <summary>
        /// 根据Pid查询活动
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async Task<List<PromotionActivityProductDo>> GetPromotionProductByPid(string pid)
        {
            var para = new DynamicParameters();
            para.Add("@pid", pid);

            var result = await GetListAsync<PromotionActivityProductDo>("WHERE product_code = @pid", para);

            return result?.ToList() ?? new List<PromotionActivityProductDo>();
        }

        /// <summary>
        /// 活动商品明细
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public async Task<List<PromotionActivityProductDo>> GetPromotionProductByActivityId(string activityId)
        {
            var para = new DynamicParameters();
            para.Add("@activityId", activityId);

            var result = await GetListAsync<PromotionActivityProductDo>("WHERE activity_id = @activityId", para);

            return result?.AsList() ?? new List<PromotionActivityProductDo>();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        public async Task<bool> DeletePromotionProduct(string activityId, string submitBy)
        {
            string sql = @"UPDATE promotion_activity_product 
SET is_deleted = 1,
update_by = @updateBy,
update_time = NOW( ) 
WHERE
	activity_id = @activityId
    AND is_deleted = 0;";

            var para = new DynamicParameters();
            para.Add("@updateBy", submitBy);
            para.Add("@activityId", activityId);

            int result = 0;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, para));
            return result > 0;
        }
    }
}
