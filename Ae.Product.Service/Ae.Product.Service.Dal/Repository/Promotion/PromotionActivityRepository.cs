using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Promotion
{
    public class PromotionActivityRepository : AbstractRepository<PromotionActivityDo>, IPromotionActivityRepository
    {
        public PromotionActivityRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 获取进行中的活动
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<Tuple<int, List<PromotionDetailDo>>> GetValidPromotionActivity(long shopId, int pageIndex, int pageSize)
        {
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);
            para.Add("@now", DateTime.Now);
            para.Add("@pageIndex", (pageIndex - 1) * pageSize);
            para.Add("@pageSize", pageSize);

            string sql = @"SELECT
	A.id AS ActivityId,
	A.`name` AS `Name`,
	A.subtitle AS Subtitle,
	A.start_time AS StartTime,
	A.end_time AS EndTime,
	A.thumb_url AS ThumbUrl 
FROM
	promotion_activity A
	INNER JOIN promotion_activity_shop B ON ( A.id = B.activity_id AND B.is_deleted = 0 ) 
WHERE
	B.shop_id = @shopId 
	AND @now >= A.start_time 
	AND @now < A.end_time 
	AND A.activity_type = 0 -- 活动类型
	
	AND A.promotion_type = 0 -- 促销类型
	
	AND A.audit_status = 1 -- 状态 已审核
	
ORDER BY
	A.create_time DESC 
	LIMIT @pageIndex,
@pageSize;";

            string sqlCount = @"SELECT
	COUNT( * ) 
FROM
	promotion_activity A
	INNER JOIN promotion_activity_shop B ON ( A.id = B.activity_id AND B.is_deleted = 0 ) 
WHERE
	B.shop_id = @shopId 
	AND @now >= A.start_time 
	AND @now < A.end_time 
	AND A.activity_type = 0 -- 活动类型
	
	AND A.promotion_type = 0 -- 促销类型
	
	AND A.audit_status = 1 -- 状态 已审核";

            int totalCount = 0;
            List<PromotionDetailDo> result = new List<PromotionDetailDo>();

            List<Task> tasks = new List<Task>();

            tasks.Add(OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<PromotionDetailDo>(sql, para))?.AsList() ?? new List<PromotionDetailDo>();

            }));

            tasks.Add(OpenSlaveConnectionAsync(async conn =>
            {
                totalCount = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            }));

            await Task.WhenAll(tasks.ToArray());

            return new Tuple<int, List<PromotionDetailDo>>(totalCount, result);
        }

        /// <summary>
        /// 获取已结束的活动
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<Tuple<int, List<PromotionDetailDo>>> GetFinishPromotionActivity(long shopId, int pageIndex, int pageSize)
        {
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);
            para.Add("@now", DateTime.Now);
            para.Add("@pageIndex", (pageIndex - 1) * pageSize);
            para.Add("@pageSize", pageSize);

            string sql = @"SELECT
	A.id AS ActivityId,
	A.`name` AS `Name`,
	A.subtitle AS Subtitle,
	A.start_time AS StartTime,
	A.end_time AS EndTime,
	A.thumb_url AS ThumbUrl 
FROM
	promotion_activity A
	INNER JOIN promotion_activity_shop B ON ( A.id = B.activity_id AND B.is_deleted = 0 ) 
WHERE
	B.shop_id = @shopId 
	AND ( @now >= A.end_time AND A.audit_status = 1 OR A.audit_status IN ( 3, 4 ) ) 
	AND A.activity_type = 0 -- 活动类型
	
	AND A.promotion_type = 0 -- 促销类型
	
ORDER BY
	A.create_time DESC 
	LIMIT @pageIndex,
@pageSize;";

            string sqlCount = @"SELECT
	COUNT( * ) 
FROM
	promotion_activity A
	INNER JOIN promotion_activity_shop B ON ( A.id = B.activity_id AND B.is_deleted = 0 ) 
WHERE
	B.shop_id = @shopId 
	AND ( @now >= A.end_time OR A.audit_status IN ( 3, 4 ) ) 
	AND A.activity_type = 0 -- 活动类型
	
	AND A.promotion_type = 0 -- 促销类型";

            int totalCount = 0;
            List<PromotionDetailDo> result = new List<PromotionDetailDo>();

            List<Task> tasks = new List<Task>();

            tasks.Add(OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<PromotionDetailDo>(sql, para))?.AsList() ?? new List<PromotionDetailDo>();

            }));

            tasks.Add(OpenSlaveConnectionAsync(async conn =>
            {
                totalCount = await conn.ExecuteScalarAsync<int>(sqlCount, para);
            }));

            await Task.WhenAll(tasks.ToArray());

            return new Tuple<int, List<PromotionDetailDo>>(totalCount, result);
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
        /// 活动列表搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<PromotionActivityDo>> SearchPromotionActivity(SearchPromotionCondition request)
        {
            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE 0 = 0");
            para.Add("@now", request.DateTimeNow);
            if (!string.IsNullOrEmpty(request.ActivityId))
            {
                condition.Append(" AND `id` = @activityId");
                para.Add("@activityId", request.ActivityId);
            }
            if (request.ActivityIds != null && request.ActivityIds.Any())
            {
                condition.Append(" AND `id` IN @activityIds");
                para.Add("@activityIds", request.ActivityIds);
            }
            var status = request.Status;
            if (status == 1)//待审核
            {
                condition.Append(" AND audit_status = 0");
            }
            else if (status == 2)//已拒绝
            {
                condition.Append(" AND audit_status = 2");
            }
            else if (status == 3)//未开始
            {
                condition.Append(" AND audit_status = 1 AND start_time > @now");
            }
            else if (status == 4)//进行中
            {
                condition.Append(" AND audit_status = 1 AND start_time <= @now AND end_time > @now");
            }
            else if (status == 5)//已结束
            {
                condition.Append(" AND ( @now >= end_time AND audit_status = 1 OR audit_status IN ( 3, 4 ) ) ");
            }

            if (!string.IsNullOrEmpty(request.ActivityName))
            {
                condition.Append(" AND `name` LIKE @activityName");
                para.Add("@activityName", $"%{request.ActivityName}%");
            }

            if (request.StartTime != null)
            {
                condition.Append(" AND (@startTime <= start_time OR @startTime <= end_time)");
                para.Add("@startTime", request.StartTime.Value);
            }
            if (request.EndTime != null)
            {
                condition.Append(" AND (@endTime >= start_time OR @endTime >= end_time)");
                para.Add("@endTime", request.EndTime.Value);
            }

            var result = await GetListPagedAsync<PromotionActivityDo>(request.PageIndex, request.PageSize, condition.ToString(), "create_time DESC", para);
            return result;
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
    A.name AS `Name`,
    A.subtitle AS `Subtitle`,
    A.description AS `Description`,
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
