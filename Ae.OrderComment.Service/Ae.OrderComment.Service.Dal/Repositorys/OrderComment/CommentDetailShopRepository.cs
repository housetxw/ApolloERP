using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Core.Request;
using Ae.OrderComment.Service.Dal.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Dal.Repositorys.OrderComment
{
    public class CommentDetailShopRepository : AbstractRepository<CommentDetailShopDO>, ICommentDetailShopRepository
    {
        public CommentDetailShopRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSqlReadOnly");
        }

        /// <summary>
        /// 获取门店评论数量
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="score">评分 0：代表所有</param>
        /// <returns></returns>
        public async Task<int> GetShopCommentTotal(long shopId, int score)
        {
            string sqlCount = @"SELECT
	count( * ) 
FROM
	comment_detail_shop a
	INNER JOIN `comment` b ON ( a.comment_id = b.id AND b.is_deleted = 0 ) 
WHERE
	a.shop_id = @ShopId 
	AND b.check_status = 1 
	AND a.is_deleted = 0";

            var condition = new StringBuilder();
            if (score > 0)
            {
                condition.Append(" AND score >= @Score");
                sqlCount = sqlCount + condition.ToString();
            }

            var param = new DynamicParameters();
            param.Add("@ShopId", shopId);
            param.Add("@Score", score);

            var total = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });
            return total;
        }


        /// 查询门店评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<BaseCommentListDO>> GetShopCommentList(GetShopCommentListRequest request)
        {
            var sqlCount = @"SELECT
	                    count(s.id) cs FROM
	                    comment_detail_shop s
	LEFT JOIN `comment` c ON s.comment_id = c.id 
WHERE
	s.shop_id = @ShopId
    AND c.check_status = 1
	AND s.is_deleted = 0 AND c.is_deleted = 0";
            var param = new DynamicParameters();
            param.Add("@ShopId", request.ShopId);
            var condition = new StringBuilder();
            int count = 0;
            await OpenSlaveConnectionAsync(async conn =>
                count = await conn.ExecuteScalarAsync<int>(sqlCount, param)
            );
            PagedEntity<BaseCommentListDO> result = new PagedEntity<BaseCommentListDO>();
            if (count > 0)
            {
                var sql = @"SELECT
	s.comment_id CommentId,
	c.user_id UserId,
	c.head_url HeadUrl,
	c.user_nick_name NickName,
	c.create_time CreateTime,
	s.score Score,
	c.content Content,
	c.shop_name ShopName,
	c.car_id CarId,
    c.vehicle AS CarName,
    c.is_anonymous AS IsAnonymous,
	c.is_best IsBest
FROM
	comment_detail_shop s
	LEFT JOIN `comment` c ON s.comment_id = c.id 
WHERE
	s.shop_id = @ShopId
    AND c.check_status = 1
	AND s.is_deleted = 0 AND c.is_deleted = 0";

                var Offset = (request.PageIndex - 1) * request.PageSize;
                param.Add("@Offset", Offset);
                param.Add("@PageSize", request.PageSize);
                condition.Append(" ORDER BY s.id desc");
                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();
                IEnumerable<BaseCommentListDO> comments = null;
                await OpenSlaveConnectionAsync(async conn =>
                    comments = await conn.QueryAsync<BaseCommentListDO>(sql, param)
                );
                result.Items = comments.ToList();
            }

            result.TotalItems = count;
            return result;

        }

        public async Task<List<long>> GetDistinctShopIds(DateTime startDate, DateTime endDate)
        {
            var sql = @"SELECT DISTINCT
	                        b.shop_id ShopId 
                        FROM
	                        `comment` a
	                        LEFT JOIN comment_detail_shop b ON b.comment_id = a.id 
	                        AND a.check_status = 1 
                        WHERE
	                        a.is_deleted = 0 
	                        AND b.is_deleted = 0 
	                        AND a.check_time >= @StartDate 
	                        AND a.check_time < @EndDate";

            IEnumerable<long> list = null;
            await OpenSlaveConnectionAsync(async conn =>
            {
                list = await conn.QueryAsync<long>(sql, new { StartDate = startDate.ToString("yyyy-MM-dd"), EndDate = endDate.ToString("yyyy-MM-dd") });
            });

            return list?.ToList();
        }

        public async Task<int> GetCountByShopId(long shopId)
        {
            var sql = @"SELECT
	                        COUNT( 0 ) 
                        FROM
	                        `comment` a
	                        LEFT JOIN comment_detail_shop b ON b.comment_id = a.id 
	                        AND a.check_status = 1 
                        WHERE
	                        b.shop_id = @ShopId";

            int count = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sql, new { ShopId = shopId });
            });

            return count;
        }

        public async Task<int> GetGoodCountByShopId(long shopId)
        {
            var sql = @"SELECT
	                        COUNT( 0 ) 
                        FROM
	                        `comment` a
	                        LEFT JOIN comment_detail_shop b ON b.comment_id = a.id 
	                        AND a.check_status = 1 
                        WHERE
	                        b.shop_id = @ShopId 
	                        AND score >=4";

            int count = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sql, new { ShopId = shopId });
            });

            return count;
        }

        /// <summary>
        /// 获取门店评分
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        public async Task<List<ShopScoreDO>> GetShopScore(List<int> shopIds)
        {
            string sql = @"SELECT
	a.shop_id AS ShopId,
	AVG( a.score ) AS Score,
    COUNT( a.id ) AS TotalNum 
FROM
	comment_detail_shop a
	INNER JOIN `comment` b ON ( a.comment_id = b.id ) 
WHERE
	a.shop_id IN @shopIds 
	AND b.check_status = 1 
	AND a.is_deleted = 0 
	AND b.is_deleted = 0
GROUP BY
	a.shop_id;";

            List<ShopScoreDO> result = new List<ShopScoreDO>();
            var param = new DynamicParameters();
            param.Add("@shopIds", shopIds);

            await OpenSlaveConnectionAsync(async conn =>
                result = (await conn.QueryAsync<ShopScoreDO>(sql, param))?.ToList() ??
                         new List<ShopScoreDO>()
            );

            return result;
        }
    }
}
