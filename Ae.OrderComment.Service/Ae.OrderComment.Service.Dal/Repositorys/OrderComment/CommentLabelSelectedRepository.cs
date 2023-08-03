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
    public class CommentLabelSelectedRepository : AbstractRepository<CommentLabelSelectedDO>, ICommentLabelSelectedRepository
    {
        public CommentLabelSelectedRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSqlReadOnly");
        }

        /// 查询商品标签列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<CommentLabelTotalDO>> GetProductCommentLabelList(GetProductCommentListRequest request)
        {
            var sqlCount = @"
SELECT
	COUNT(LabelId) count from (
SELECT
	l.label_name LabelName,
	l.label_id LabelId  
FROM
	comment_label_selected l
	LEFT JOIN comment_detail_product p ON p.comment_id = l.comment_id 
	AND p.id = l.comment_detail_id 
	AND l.comment_detail_type = 3 
WHERE
	p.product_id = @ProductId
	AND p.is_deleted = 0 
GROUP BY l.label_id ) t";
            var param = new DynamicParameters();
            param.Add("@ProductId", request.ProductId);
            var condition = new StringBuilder();

            PagedEntity<CommentLabelTotalDO> result = new PagedEntity<CommentLabelTotalDO>();
            int count = 0;
            await OpenSlaveConnectionAsync(async conn =>
                count = await conn.ExecuteScalarAsync<int>(sqlCount, param)
            );
            if (count > 0)
            {
                var sql = @"SELECT
	* 
FROM
	(
SELECT
	l.label_name LabelName,
	l.label_id Id,
	count( l.label_id ) Num 
FROM
	comment_label_selected l
	LEFT JOIN comment_detail_product p ON p.comment_id = l.comment_id 
	AND p.id = l.comment_detail_id 
	AND l.comment_detail_type = 3 
WHERE
	p.product_id = @ProductId 
	AND p.is_deleted = 0 
GROUP BY
	l.label_id 
	) t 
ORDER BY
	Num DESC ";

                var Offset = (request.PageIndex - 1) * request.PageSize;
                param.Add("@Offset", Offset);
                param.Add("@PageSize", request.PageSize);
                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();
                IEnumerable<CommentLabelTotalDO> comments = new List<CommentLabelTotalDO>();
                await OpenSlaveConnectionAsync(async conn =>
                    comments = await conn.QueryAsync<CommentLabelTotalDO>(sql, param)
                );
                result.Items = comments.ToList();
            }
            result.TotalItems = count;
            return result;

        }
        /// <summary>
        /// 获取门店评价标签列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<CommentLabelTotalDO>> GetShopCommentLabelList(GetShopCommentListRequest request)
        {
            var sqlCount = @"
SELECT
	COUNT(LabelId) count from (
SELECT
	l.label_name LabelName,
	l.label_id LabelId  
FROM
	comment_label_selected l
	LEFT JOIN comment_detail_shop s ON s.comment_id = l.comment_id 
	AND s.id = l.comment_detail_id 
	AND l.comment_detail_type = 2 
WHERE
	s.shop_id = @ShopId
	AND s.is_deleted = 0 AND l.is_deleted = 0
GROUP BY l.label_id ) t";
            var param = new DynamicParameters();
            param.Add("@ShopId", request.ShopId);
            var condition = new StringBuilder();

            PagedEntity<CommentLabelTotalDO> result = new PagedEntity<CommentLabelTotalDO>();
            int count = 0;
            await OpenSlaveConnectionAsync(async conn =>
                count = await conn.ExecuteScalarAsync<int>(sqlCount, param)
            );
            if (count > 0)
            {
                var sql = @"SELECT
	* 
FROM
	(
SELECT
	l.label_name LabelName,
	l.label_id Id,
	count( l.label_id ) Num 
FROM
	comment_label_selected l
	LEFT JOIN comment_detail_shop s ON s.comment_id = l.comment_id 
	AND s.id = l.comment_detail_id 
	AND l.comment_detail_type = 2 
WHERE
	s.shop_id = @ShopId
	AND s.is_deleted = 0 AND l.is_deleted = 0
GROUP BY l.label_id ) t
ORDER BY
	Num DESC ";

                var Offset = (request.PageIndex - 1) * request.PageSize;
                param.Add("@Offset", Offset);
                param.Add("@PageSize", request.PageSize);
                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();
                IEnumerable<CommentLabelTotalDO> comments = new List<CommentLabelTotalDO>();
                await OpenSlaveConnectionAsync(async conn =>
                    comments = await conn.QueryAsync<CommentLabelTotalDO>(sql, param)
                );
                result.Items = comments.ToList();
            }
            result.TotalItems = count;
            return result;

        }

        public async Task<List<ShopLabelStatisticsDo>> GetShopLabel(long shopId)
        {
            List<ShopLabelStatisticsDo> result = new List<ShopLabelStatisticsDo>();

            string sql = @"SELECT
	c.label_id AS LabelId,
	COUNT( * ) AS Count 
FROM
	comment_detail_shop a
	INNER JOIN `comment` b ON ( a.comment_id = b.id AND b.check_status = 1 AND b.is_deleted = 0 )
	INNER JOIN comment_label_selected c ON ( c.comment_id = b.id AND c.is_deleted = 0 ) 
WHERE
	b.shop_id = @shopId 
	AND b.is_deleted = 0 
GROUP BY
	c.label_id";

            var param = new DynamicParameters();
            param.Add("@shopId", shopId);

            await OpenSlaveConnectionAsync(async conn =>
                result = (await conn.QueryAsync<ShopLabelStatisticsDo>(sql, param))?.ToList() ??
                         new List<ShopLabelStatisticsDo>()
            );

            return result;
        }
    }
}
