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
    public class CommentDetailProductRepository : AbstractRepository<CommentDetailProductDO>, ICommentDetailProductRepository
    {
        public CommentDetailProductRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSqlReadOnly");
        }
        /// <summary>
        /// 获取商品评论数量
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="score">评分 0：代表所有</param>
        /// <returns></returns>
        public async Task<int> GetProductCommentTotal(string productId, int score)
        {
            string sqlCount = @"select count(id) from comment_detail_product where product_id = @ProductId and is_deleted = 0";

            var condition = new StringBuilder();
            if (score > 0)
            {
                condition.Append(" AND score = @Score");
                sqlCount = sqlCount + condition.ToString();
            }

            var param = new DynamicParameters();
            param.Add("@ProductId", productId);
            param.Add("@Score", score);

            var total = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });
            return total;
        }


        /// 查询商品评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<BaseCommentListDO>> GetProductCommentList(GetProductCommentListRequest request)
        {
            var sqlCount = @"SELECT
	                    count(p.id) cs FROM
	                    comment_detail_product p
	LEFT JOIN `comment` c ON p.comment_id = c.id 
WHERE
	p.product_id = @ProductId
	AND p.is_deleted = 0 AND c.is_deleted = 0";
            var param = new DynamicParameters();
            param.Add("@ProductId", request.ProductId);
            var condition = new StringBuilder();
            int count = 0;
            await OpenSlaveConnectionAsync(async conn =>
                count = await conn.ExecuteScalarAsync<int>(sqlCount, param)
            );
            PagedEntity<BaseCommentListDO> result = new PagedEntity<BaseCommentListDO>();
            if (count > 0)
            {
                var sql = @"SELECT
	p.comment_id CommentId,
	c.user_id UserId,
	c.head_url HeadUrl,
	c.user_nick_name NickName,
	c.create_time CreateTime,
	p.score Score,
	c.content Content,
	c.shop_name ShopName,
	c.car_id CarId,
    c.vehicle AS CarName,
    c.is_anonymous AS IsAnonymous,
	c.is_best IsBest
FROM
	comment_detail_product p
	LEFT JOIN `comment` c ON p.comment_id = c.id 
WHERE
	p.product_id = @ProductId
	AND p.is_deleted = 0 AND c.is_deleted = 0";

                var Offset = (request.PageIndex - 1) * request.PageSize;
                param.Add("@Offset", Offset);
                param.Add("@PageSize", request.PageSize);
                condition.Append(" ORDER BY p.id desc");
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

        public async Task<List<string>> GetDistinctCommentDetailProductIds(DateTime startDate, DateTime endDate)
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

            IEnumerable<string> list = null;
            await OpenSlaveConnectionAsync(async conn =>
            {
                list = await conn.QueryAsync<string>(sql, new { StartDate = startDate.ToString("yyyy-MM-dd"), EndDate = endDate.ToString("yyyy-MM-dd") });
            });

            return list?.ToList();
        }

        public async Task<int> GetCountByProductId(string productId)
        {
            var sql = @"SELECT
	                        COUNT( 0 ) 
                        FROM
	                        `comment` a
	                        LEFT JOIN comment_detail_shop b ON b.comment_id = a.id 
	                        AND a.check_status = 1 
                        WHERE
	                        b.product_id = @ProductId";

            int count = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sql, new { ProductId = productId });
            });

            return count;
        }

        public async Task<int> GetGoodCountByProductId(string productId)
        {
            var sql = @"SELECT
	                        COUNT( 0 ) 
                        FROM
	                        `comment` a
	                        LEFT JOIN comment_detail_shop b ON b.comment_id = a.id 
	                        AND a.check_status = 1 
                        WHERE
	                        b.product_id = @ProductId 
	                        AND score >=4";

            int count = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                count = await conn.ExecuteScalarAsync<int>(sql, new { ProductId = productId });
            });

            return count;
        }
    }
}
