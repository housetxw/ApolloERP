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
    public class PromotionActivityRepository : AbstractRepository<PromotionActivityDo>, IPromotionActivityRepository
    {
        public PromotionActivityRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ProductSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductSqlReadOnly");
        }

        /// <summary>
        /// 活动列表搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<PromotionActivityDo>> SearchPromotionActivity(SearchPromotionCondition request)
        {
            string sql = @"SELECT
	a.audit_status AS AuditStatus,
	a.start_time AS StartTime,
	end_time AS EndTime,
	a.`id` AS Id,
	a.`name` AS `Name`,
	a.subtitle AS Subtitle,
	a.create_by AS CreateBy 
FROM
	promotion_activity a
	INNER JOIN promotion_activity_shop b ON ( a.id = b.activity_id AND b.is_deleted = 0 ) 
WHERE
	b.shop_id = @shopId 
	AND a.is_deleted = 0 {0}
ORDER BY
	a.create_time DESC 
	LIMIT @startIndex,@pageSize;";

            string sqlCount = @"SELECT
	COUNT( * ) 
FROM
	promotion_activity a
	INNER JOIN promotion_activity_shop b ON ( a.id = b.activity_id AND b.is_deleted = 0 ) 
WHERE
	b.shop_id = @shopId 
	AND a.is_deleted = 0 {0};";

            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            para.Add("@now", request.DateTimeNow);
            para.Add("@shopId", request.ShopId);
            para.Add("@startIndex", (request.PageIndex - 1) * request.PageSize);
            para.Add("@pageSize", request.PageSize);
            if (!string.IsNullOrEmpty(request.ActivityId))
            {
                condition.Append(" AND a.`id` = @activityId");
                para.Add("@activityId", request.ActivityId);
            }
            if (request.ActivityIds != null && request.ActivityIds.Any())
            {
                condition.Append(" AND a.`id` IN @activityIds");
                para.Add("@activityIds", request.ActivityIds);
            }
            var status = request.Status;
            if (status == 1)//待审核
            {
                condition.Append(" AND a.audit_status = 0");
            }
            else if (status == 2)//已拒绝
            {
                condition.Append(" AND a.audit_status = 2");
            }
            else if (status == 3)//未开始
            {
                condition.Append(" AND a.audit_status = 1 AND a.start_time > @now");
            }
            else if (status == 4)//进行中
            {
                condition.Append(" AND a.audit_status = 1 AND a.start_time <= @now AND a.end_time > @now");
            }
            else if (status == 5)//已结束
            {
                condition.Append(" AND ( @now >= a.end_time AND a.audit_status = 1 OR a.audit_status IN ( 3, 4 ) ) ");
            }

            if (!string.IsNullOrEmpty(request.ActivityName))
            {
                condition.Append(" AND a.`name` LIKE @activityName");
                para.Add("@activityName", $"%{request.ActivityName}%");
            }

            if (request.StartTime != null)
            {
                condition.Append(" AND (@startTime <= a.start_time OR @startTime <= a.end_time)");
                para.Add("@startTime", request.StartTime.Value);
            }
            if (request.EndTime != null)
            {
                condition.Append(" AND (@endTime >= a.start_time OR @endTime >= a.end_time)");
                para.Add("@endTime", request.EndTime.Value);
            }

            sql = string.Format(sql, condition.ToString());
            sqlCount = string.Format(sqlCount, condition.ToString());

            int totalCount = 0;
            List<PromotionActivityDo> result = new List<PromotionActivityDo>();
            List<Task> tasks = new List<Task>();
            tasks.Add(OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<PromotionActivityDo>(sql, para))?.AsList() ?? new List<PromotionActivityDo>();

            }));
            tasks.Add(OpenSlaveConnectionAsync(async conn =>
            {
                totalCount = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            }));

            await Task.WhenAll(tasks.ToArray());

            return new PagedEntity<PromotionActivityDo>
            {
                Items = result,
                TotalItems = totalCount
            };
        }

        /// <summary>
        /// 活动详情
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<PromotionActivityDo> GetPromotionDetail(string activityId, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@activityId", activityId);

            var result = await GetListAsync<PromotionActivityDo>("WHERE `id` = @activityId", para, !readOnly);

            return result?.FirstOrDefault();
        }

        /// <summary>
        /// 根据Pid查询活动
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<List<ProductPromotionDo>> GetPromotionByPidList(List<string> pidList, bool readOnly = true)
        {
            string sql = @"SELECT
	A.id AS `Id`,
	A.start_time AS `StartTime`,
	A.end_time AS `EndTime`,
	A.audit_status AS `AuditStatus`,
    A.label AS `Label`,
    A.apply_channel AS `ApplyChannel`,
	B.product_code AS `ProductCode`,
	B.promotion_price AS `PromotionPrice`,
	B.limit_quantity AS `LimitQuantity`,
	B.sold_quantity AS `SoldQuantity` 
FROM
	promotion_activity A
	INNER JOIN promotion_activity_product B ON ( A.id = B.activity_id AND B.is_deleted = 0 ) 
WHERE
	B.product_code IN @productCode 
	AND B.is_deleted = 0;";

            var para = new DynamicParameters();

            para.Add("@productCode", pidList);

            List<ProductPromotionDo> result = new List<ProductPromotionDo>();

            await OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<ProductPromotionDo>(sql, para))?.AsList() ?? new List<ProductPromotionDo>();

            });

            return result;
        }
    }
}
