using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.OrderComment.Service.Core.Request;
using Ae.OrderComment.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ae.OrderComment.Service.Common;
using Ae.OrderComment.Service.Core.Enums;
using Ae.OrderComment.Service.Core.Request.OrderCommentForApp;
using Ae.OrderComment.Service.Core.Response.OrderCommentForApp;

namespace Ae.OrderComment.Service.Dal.Repositorys.OrderComment
{

    public class OrderCommentRepository : AbstractRepository<CommentDO>, IOrderCommentRepository
    {
        public OrderCommentRepository()
        {
            SetDbType(ApolloErp.Data.DapperExtensions.DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderCommentSqlReadOnly");

        }

        public async Task<int> CheckOrderComment(CheckOrderCommentRequest request)
        {
            var sql = @"UPDATE COMMENT 
	                    SET check_status = @check_status,
	                    check_comment = @check_comment,
                        check_time = SYSDATE( ),
	                    update_by = @update_by,
	                    update_time = SYSDATE( ) 
                    WHERE
	                    id = @Id";

            var param = new DynamicParameters();
            param.Add("@check_status", request.CheckStatus);
            param.Add("@check_comment", request.CheckComment);
            param.Add("@update_by", request.UpdateBy);
            param.Add("@Id", request.CommentId);

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<int> CheckOrderReply(CheckOrderCommentRequest request)
        {

            var sql = @"UPDATE comment_reply 
	                    SET check_status = @check_status,
	                    check_comment = @check_comment,
                        check_time = SYSDATE( ),
	                    update_by = @update_by,
	                    update_time = SYSDATE( ) 
                    WHERE
	                    id = @Id";

            var param = new DynamicParameters();
            param.Add("@check_status", request.CheckStatus);
            param.Add("@check_comment", request.CheckComment);
            param.Add("@update_by", request.UpdateBy);
            param.Add("@Id", request.CommentId);

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<List<CommentImageDO>> GetCommentImages(GetCommentImageRequest request)
        {
            IEnumerable<CommentImageDO> commentImages = null;
            var param = new DynamicParameters();
            param.Add("@relationType", request.RelationType);

            var sql = $@"SELECT
	                    id Id,
	                    comment_id CommentId,
	                    image_url ImageUrl 
                    FROM
	                    comment_image 
                    WHERE
	                    is_deleted = 0 
	                    AND comment_id in ({string.Join(',', request.CommentIds).TrimEnd(',')}) and relation_type=@relationType;";

            await OpenSlaveConnectionAsync(async conn => commentImages = await conn.QueryAsync<CommentImageDO>(sql, param));
            return commentImages != null ? commentImages.ToList() : new List<CommentImageDO>();
        }

        public async Task<PagedEntity<GetOrderCommentForProductDO>> GetOrderCommentForProductList(GetOrderCommentForProductRequest request)
        {
            var res = new PagedEntity<GetOrderCommentForProductDO>();
            var total = 0;

            var condition = new StringBuilder();
            var param = new DynamicParameters();
            var dateTime = new DateTime(2019, 10, 1);
            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);

            if (request.Id > 0)
            {
                condition.Append(" and oc.id =@Id");
                param.Add("@Id", request.Id);
            }

            if (!string.IsNullOrWhiteSpace(request.OrderNo))
            {
                condition.Append(" and oc.order_no =@OrderNo ");
                param.Add("@OrderNo", request.OrderNo);
            }

            if (!string.IsNullOrWhiteSpace(request.UserId))
            {
                var userId = request.UserId;
                var userNickNames = request.UserId.Split(new char[] { ' ', '　', ',', '，', ';', '；', '\\', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                if (userNickNames.Any())
                {
                    string productName = string.Join("%", userNickNames);
                    condition.Append(" AND (oc.user_nick_name LIKE N'%" + productName.Replace("'", "''") + "%'  OR oc.user_id=@userId)");
                    param.Add("@userId", userId);
                }
            }

            if (request.ShopId > 0)
            {
                condition.Append(" and oc.shop_id =@ShopId");
                param.Add("@ShopId", request.ShopId);
            }

            if (request.Channel > 0)
            {
                condition.Append(" and oc.channel =@Channel");
                param.Add("@Channel", request.Channel);
            }

            if (request.CheckStatus >= 0)
            {
                condition.Append(" and oc.check_status =@CheckStatus");
                param.Add("@CheckStatus", request.CheckStatus);
            }

            if (request.OrderType > 0)
            {
                condition.Append(" and oc.order_type =@OrderType");
                param.Add("@OrderType", request.OrderType);
            }


            if (request.CreateStartTime != null &&
                request.CreateStartTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and oc.create_time >=@CreateStartTime");
                param.Add("@CreateStartTime", request.CreateStartTime);
            }

            if (request.CreateEndTime != null &&
                request.CreateEndTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and oc.create_time <=@CreateEndTime");
                param.Add("@CreateEndTime", request.CreateEndTime);
            }

            if (!string.IsNullOrWhiteSpace(request.ProductName))
            {
                var productId = request.ProductName;
                var productNames = request.ProductName.Split(new char[] { ' ', '　', ',', '，', ';', '；', '\\', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                if (productNames.Any())
                {
                    string productName = string.Join("%", productNames);
                    condition.Append(" AND (cdp.product_display_name LIKE N'%" + productName.Replace("'", "''") + "%'  OR cdp.product_id=@productId)");
                    param.Add("@productId", productId);
                }
            }



            var sqlCount = @"SELECT
	                    count(oc.id) FROM
	                    COMMENT oc
	                    INNER JOIN comment_detail_product cdp ON oc.id = cdp.comment_id 
                    WHERE
	                    1 = 1 
	                    AND oc.is_deleted = 0 
	                    AND cdp.is_deleted =0 " + condition.ToString();


            var sql = @"SELECT
	                    oc.id Id,
	                    oc.order_id OrderId,
	                    oc.order_no OrderNo,
	                    oc.channel Channel,
	                    oc.order_type OrderType,
	                    oc.user_id UserId,
	                    oc.user_nick_name UserNickName,
	                    oc.car_id CarId,
	                    oc.shop_id ShopId,
	                    oc.shop_name ShopName,
	                    oc.content Content,
	                    oc.is_anonymous IsAnonymous,
	                    oc.check_status CheckStatus,
	                    oc.check_comment CheckComment,
	                    oc.is_top IsTop,
	                    oc.is_best IsBest,
	                    oc.like_num LikeNum,
	                    oc.create_by CreateBy,
	                    oc.create_time CreateTime,
	                    cdp.product_id ProductId,
	                    cdp.product_display_name ProductDisplayName,
	                    cdp.product_image_url ProductImageUrl,
	                    cdp.score  Score
                    FROM
	                    COMMENT oc
	                    INNER JOIN comment_detail_product cdp ON oc.id = cdp.comment_id 
                    WHERE
	                    1 = 1 
	                    AND oc.is_deleted = 0 
	                    AND cdp.is_deleted =0 " + condition.ToString() + " order by id desc LIMIT @index, @size";

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            IEnumerable<GetOrderCommentForProductDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<GetOrderCommentForProductDO>(sql, param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;
        }

        public async Task<PagedEntity<GetOrderCommentForReplyDO>> GetOrderCommentForReplyList(GetOrderCommentForReplyRequest request)
        {
            var res = new PagedEntity<GetOrderCommentForReplyDO>();
            var total = 0;

            var condition = new StringBuilder();
            var param = new DynamicParameters();
            var dateTime = new DateTime(2019, 10, 1);
            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);

            if (request.Id > 0)
            {
                condition.Append(" and ocr.id =@Id");
                param.Add("@Id", request.Id);
            }

            if (!string.IsNullOrWhiteSpace(request.OrderNo))
            {
                condition.Append(" and ocr.order_no =@OrderNo ");
                param.Add("@OrderNo", request.OrderNo);
            }
            if (request.Channel > 0)
            {
                condition.Append(" and ocr.channel =@Channel");
                param.Add("@Channel", request.Channel);
            }

            if (request.ShopId > 0)
            {
                condition.Append(" and ocr.shop_id =@ShopId");
                param.Add("@ShopId", request.ShopId);
            }

            if (request.Type > 0)
            {

                condition.Append(" and ocr.reply_type =@ReplyType");
                param.Add("@ReplyType", request.Type);
            }

            if (request.CheckStatus >= 0)
            {
                condition.Append(" and ocr.check_status =@CheckStatus");
                param.Add("@CheckStatus", request.CheckStatus);
            }

            if (request.OrderType > 0)
            {
                condition.Append(" and ocr.order_type =@OrderType");
                param.Add("@OrderType", request.OrderType);
            }


            if (request.CreateStartTime != null &&
                request.CreateStartTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and ocr.create_time >=@CreateStartTime");
                param.Add("@CreateStartTime", request.CreateStartTime);
            }

            if (request.CreateEndTime != null &&
                request.CreateEndTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and ocr.create_time <=@CreateEndTime");
                param.Add("@CreateEndTime", request.CreateEndTime);
            }

            var sqlCount = @" SELECT
	                    count(ocr.id) FROM
	                    comment_reply ocr 
                    WHERE
	                    is_deleted = 0 " + condition.ToString();


            var sql = @"SELECT
	                    ocr.id Id,
	                    ocr.reply_id ReplyId,
	                    ocr.reply_part_type ReplyPartType,
	                    ocr.reply_type ReplyType,
	                    ocr.shop_id ShopId,
	                    ocr.shop_name ShopName,
	                    ocr.channel Channel,
	                    ocr.order_type OrderType,
	                    ocr.order_no OrderNo,
	                    ocr.check_status CheckStatus,
	                    ocr.check_comment CheckComment,
	                    ocr.content Content,
	                    ocr.create_by CreateBy,
	                    ocr.create_time CreateTime 
                    FROM
	                    comment_reply ocr 
                    WHERE
	                    is_deleted = 0 " + condition.ToString() + " order by ocr.id desc LIMIT @index, @size";

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            IEnumerable<GetOrderCommentForReplyDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<GetOrderCommentForReplyDO>(sql, param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;

        }

        public async Task<PagedEntity<GetOrderCommentForShopDO>> GetOrderCommentForShopList(GetOrderCommentForShopRequest request)
        {
            var res = new PagedEntity<GetOrderCommentForShopDO>();
            var total = 0;

            var condition = new StringBuilder();
            var param = new DynamicParameters();
            var dateTime = new DateTime(2019, 10, 1);
            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);

            if (request.Id > 0)
            {
                condition.Append(" and oc.id =@Id");
                param.Add("@Id", request.Id);
            }

            if (!string.IsNullOrWhiteSpace(request.OrderNo))
            {
                condition.Append(" and oc.order_no =@OrderNo ");
                param.Add("@OrderNo", request.OrderNo);
            }
            if (request.Channel > 0)
            {
                condition.Append(" and oc.channel =@Channel");
                param.Add("@Channel", request.Channel);
            }

            if (!string.IsNullOrWhiteSpace(request.UserId))
            {
                var userId = request.UserId;
                var userNickNames = request.UserId.Split(new char[] { ' ', '　', ',', '，', ';', '；', '\\', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                if (userNickNames.Any())
                {
                    string productName = string.Join("%", userNickNames);
                    condition.Append(" AND (oc.user_nick_name LIKE N'%" + productName.Replace("'", "''") + "%'  OR oc.user_id=@userId)");
                    param.Add("@userId", userId);
                }
            }

            if (request.ShopId > 0)
            {
                condition.Append(" and oc.shop_id =@ShopId");
                param.Add("@ShopId", request.ShopId);
            }

            if (request.CheckStatus >= 0)
            {
                condition.Append(" and oc.check_status =@CheckStatus");
                param.Add("@CheckStatus", request.CheckStatus);
            }

            if (request.OrderType > 0)
            {
                condition.Append(" and oc.order_type =@OrderType");
                param.Add("@OrderType", request.OrderType);
            }


            if (request.CreateStartTime != null &&
                request.CreateStartTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and oc.create_time >=@CreateStartTime");
                param.Add("@CreateStartTime", request.CreateStartTime);
            }

            if (request.CreateEndTime != null &&
                request.CreateEndTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and oc.create_time <=@CreateEndTime");
                param.Add("@CreateEndTime", request.CreateEndTime);
            }


            var sqlCount = @"SELECT
	                    count(oc.id) FROM
	                    COMMENT oc
	                    INNER JOIN comment_detail_shop cds ON oc.id = cds.comment_id 
                    WHERE
	                    1 = 1 
	                    AND oc.is_deleted = 0 
	                    AND cds.is_deleted =0  " + condition.ToString();

            var sql = @"SELECT
	                    oc.id Id,
	                    oc.order_id OrderId,
	                    oc.order_no OrderNo,
	                    oc.channel Channel,
	                    oc.order_type OrderType,
	                    oc.user_id UserId,
	                    oc.user_nick_name UserNickName,
	                    oc.car_id CarId,
	                    oc.shop_id ShopId,
	                    oc.shop_name ShopName,
	                    oc.content Content,
	                    oc.is_anonymous IsAnonymous,
	                    oc.check_status CheckStatus,
	                    oc.check_comment CheckComment,
	                    oc.is_top IsTop,
	                    oc.is_best IsBest,
	                    oc.like_num LikeNum,
	                    oc.create_by CreateBy,
	                    oc.create_time CreateTime,
	                    cds.shop_id DetailShopId,
	                    cds.shop_image_url ShopImageUrl,
	                    cds.score 
                    FROM
	                    COMMENT oc
	                    INNER JOIN comment_detail_shop cds ON oc.id = cds.comment_id 
                    WHERE
	                    1 = 1 
	                    AND oc.is_deleted = 0 
	                    AND cds.is_deleted =0  " + condition.ToString() + " order by id desc LIMIT @index, @size";

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            IEnumerable<GetOrderCommentForShopDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<GetOrderCommentForShopDO>(sql, param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;

        }

        public async Task<PagedEntity<GetOrderCommentForTechDO>> GetOrderCommentForTechList(GetOrderCommentForTechRequest request)
        {
            var res = new PagedEntity<GetOrderCommentForTechDO>();
            var total = 0;

            var condition = new StringBuilder();
            var param = new DynamicParameters();
            var dateTime = new DateTime(2019, 10, 1);
            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);

            if (request.Id > 0)
            {
                condition.Append(" and oc.id =@Id");
                param.Add("@Id", request.Id);
            }

            if (request.Channel > 0)
            {
                condition.Append(" and oc.channel =@Channel");
                param.Add("@Channel", request.Channel);
            }
            if (!string.IsNullOrWhiteSpace(request.OrderNo))
            {
                condition.Append(" and oc.order_no =@OrderNo ");
                param.Add("@OrderNo", request.OrderNo);
            }

            if (!string.IsNullOrWhiteSpace(request.UserId))
            {
                var userId = request.UserId;
                var userNickNames = request.UserId.Split(new char[] { ' ', '　', ',', '，', ';', '；', '\\', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                if (userNickNames.Any())
                {
                    string productName = string.Join("%", userNickNames);
                    condition.Append(" AND (oc.user_nick_name LIKE N'%" + productName.Replace("'", "''") + "%'  OR oc.user_id=@userId)");
                    param.Add("@userId", userId);
                }
            }

            if (request.ShopId > 0)
            {
                condition.Append(" and oc.shop_id =@ShopId");
                param.Add("@ShopId", request.ShopId);
            }

            if (request.CheckStatus >= 0)
            {
                condition.Append(" and oc.check_status =@CheckStatus");
                param.Add("@CheckStatus", request.CheckStatus);
            }

            if (request.OrderType > 0)
            {
                condition.Append(" and oc.order_type =@OrderType");
                param.Add("@OrderType", request.OrderType);
            }

            if (request.TechLevel >= 0) {
                condition.Append(" and cdt.tech_level =@TechLevel");
                param.Add("@TechLevel", request.TechLevel);
            }

            if (request.CreateStartTime != null &&
                request.CreateStartTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and oc.create_time >=@CreateStartTime");
                param.Add("@CreateStartTime", request.CreateStartTime);
            }

            if (request.CreateEndTime != null &&
                request.CreateEndTime.Subtract(dateTime).TotalDays > 0)
            {
                condition.Append(" and oc.create_time <=@CreateEndTime");
                param.Add("@CreateEndTime", request.CreateEndTime);
            }



            if (!string.IsNullOrWhiteSpace(request.TechName))
            {
                var techId = request.TechName;
                var techNames = request.TechName.Split(new char[] { ' ', '　', ',', '，', ';', '；', '\\', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                if (techNames.Any())
                {
                    string techName = string.Join("%", techNames);
                    condition.Append(" AND (cdt.tech_name LIKE N'%" + techName.Replace("'", "''") + "%'  OR cdt.employee_id=@techId)");
                    param.Add("@techId", techId);
                }
            }


            var sqlCount = @"SELECT
	                    count(oc.id) FROM
	                    COMMENT oc
	                    INNER JOIN comment_detail_tech cdt ON oc.id = cdt.comment_id 
                    WHERE
	                    1 = 1 
	                    AND oc.is_deleted = 0 
	                    AND cdt.is_deleted =0  " + condition.ToString();


            var sql = @"SELECT
	                    oc.id Id,
	                    oc.order_id OrderId,
	                    oc.order_no OrderNo,
	                    oc.channel Channel,
	                    oc.order_type OrderType,
	                    oc.user_id UserId,
	                    oc.user_nick_name UserNickName,
	                    oc.car_id CarId,
	                    oc.shop_id ShopId,
	                    oc.shop_name ShopName,
	                    oc.content Content,
	                    oc.is_anonymous IsAnonymous,
	                    oc.check_status CheckStatus,
	                    oc.check_comment CheckComment,
	                    oc.is_top IsTop,
	                    oc.is_best IsBest,
	                    oc.like_num LikeNum,
	                    oc.create_by CreateBy,
	                    oc.create_time CreateTime,
	                    cdt.tech_name TechName,cdt.employee_id EmployeeId,
	                    cdt.tech_level TechLevel,
	                    cdt.tech_head_url TechHeadUrl,
	                    cdt.score  Score
                    FROM
	                    COMMENT oc
	                    INNER JOIN comment_detail_tech cdt ON oc.id = cdt.comment_id 
                    WHERE
	                    1 = 1 
	                    AND oc.is_deleted = 0 
	                    AND cdt.is_deleted =0 " + condition.ToString() + " order by id desc LIMIT @index, @size";

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            IEnumerable<GetOrderCommentForTechDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<GetOrderCommentForTechDO>(sql, param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;
        }

        /// <summary>
        /// 获取商品评论数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ProductCommentTotalDO>> GetProductCommentTotal(GetProductCommentTotal request)
        {
            string sql = @"SELECT
	product_id as ProductId,
	count( id ) AS CommentCount 
FROM
	comment_detail_product 
WHERE
	product_id IN @ProductIds
GROUP BY product_id";
            var param = new DynamicParameters();
            param.Add("@ProductIds", request.ProductIds);

            IEnumerable<ProductCommentTotalDO> productComments = null;
            await OpenConnectionAsync(async conn =>
                productComments = await conn.QueryAsync<ProductCommentTotalDO>(sql, param)
            );
            return productComments.ToList();
        }
        

        /// <summary>
        /// 得到点评信息列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<GetCommentListDO>> GetCommentList(GetCommentListRequest request)
        {
            var response = new PagedEntity<GetCommentListDO>();
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine("where 1=1");
            parameters.Add("@ShopId",request.ShopId);
            parameters.Add("@CheckStatus",CheckStatusEnum.审核通过.ToInt());
            if (!string.IsNullOrEmpty(request.OrderType))
            {
              
                if (request.OrderType.Trim() == OrderTypeEnum.BaoYang.ToString())
                {
                    builder.AppendLine(" and A.order_type=@OrderType");
                    parameters.Add("@OrderType", OrderTypeEnum.BaoYang.ToInt());
                }
                if (request.OrderType.Trim() == OrderTypeEnum.Tire.ToString())
                {
                    builder.AppendLine(" and A.order_type=@OrderType");
                    parameters.Add("@OrderType", OrderTypeEnum.Tire.ToInt());
                }
                if (request.OrderType.Trim() == OrderTypeEnum.Beauty.ToString())
                {
                    builder.AppendLine(" and A.order_type=@OrderType");
                    parameters.Add("@OrderType", OrderTypeEnum.Beauty.ToInt());
                }
            }

            if (!string.IsNullOrEmpty(request.ScoreLevel))
            {
                if (request.ScoreLevel == ShopScoreLevelEnum.ShopGood.ToString())
                {
                    builder.AppendLine(" and B.score>=@Score");
                    parameters.Add("@Score", ShopScoreLevelEnum.ShopGood.ToInt());
                }

                if (request.ScoreLevel == ShopScoreLevelEnum.ShopNegative.ToString())
                {
                    builder.AppendLine(" and B.score<=@Score");
                    parameters.Add("@Score", ShopScoreLevelEnum.ShopNegative.ToInt());
                }
            }

            if (!string.IsNullOrEmpty(request.ReplyStatus))
            {
                if (request.ReplyStatus == ReplyStatusEnum.ShopNotReply.ToString())
                {

                    builder.AppendLine(" and A.shop_reply_type=@ShopReplyType");
                    parameters.Add("@ShopReplyType", ReplyStatusEnum.ShopNotReply.ToInt());
                }
                if (request.ReplyStatus == ReplyStatusEnum.ShopReply.ToString())
                {

                    builder.AppendLine(" and A.shop_reply_type=@ShopReplyType");
                    parameters.Add("@ShopReplyType", ReplyStatusEnum.ShopReply.ToInt());
                }
            }

            var sqlCount = @"select Count(1) FROM (
                                          select A.Id from `comment` A
                                            inner join  comment_detail_shop B
                                            on A.id=B.comment_id
                                            AND A.is_deleted=0
                                            AND B.is_deleted=0 AND A.check_status=@CheckStatus
                                            AND A.shop_id=@ShopId " + builder.ToString() + @"
                                       ) t";

            var total = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parameters);
            });

            var sql = @" select A.id AS Id,A.car_id AS CarId,A.channel AS Channel,
                                    A.check_comment AS CheckComment,A.check_status AS CheckStatus,A.content AS Content,
                                    A.create_by As CreateBy,A.create_time AS CreateTime,A.head_url AS HeadUrl,A.is_anonymous AS IsAnonymous,
                                    A.is_best As IsBest,A.is_deleted As IsDeleted,A.is_top AS IsTop,A.like_num AS LikeNum,A.office_type AS OfficeType,
                                    A.order_id AS OrderId,A.order_no As OrderNo,
                                    A.order_type AS OrderType,A.shop_id AS ShopId,
                                    A.shop_name AS ShopName,A.shop_reply_type AS ShopReplyType,
                                    A.update_by AS UpdateBy,A.update_time AS UpdateTime,
                                    A.user_id AS UserId,A.user_nick_name AS NickName,A.user_reply_type As UserReplyType,
                                    B.score AS ShopScore  from `comment` A
                                    inner join  comment_detail_shop B
                                    on A.id=B.comment_id
                                    AND A.is_deleted=0
                                    AND B.is_deleted=0 AND  A.check_status=@CheckStatus
                                    AND A.shop_id=@ShopId " + builder.ToString() + @"
                                    Order by A.create_time DESC limit " + (request.PageIndex - 1) * request.PageSize + "," + request.PageSize + "";


            IEnumerable<GetCommentListDO> orderDos = null;
            await OpenConnectionAsync(async conn => orderDos = await conn.QueryAsync<GetCommentListDO>(sql, parameters));

            response.TotalItems = total;
            response.Items = orderDos.ToList();

            return response;
        }

        /// <summary>
        /// 得到回评论的信息
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<CommentReplyDO>> GetCommentReplyList(List<long> ids, long shopId)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();



            var sql = @"
            select id AS Id, reply_id AS ReplyId, reply_part_type AS ReplyPartType, reply_type AS ReplyType, shop_id AS ShopId, 
         shop_name As ShopName, channel AS Channel, order_type AS OrderType,
                order_no AS OrderNo, check_status AS CheckStatus, check_comment AS CheckComment, content AS Content, 
               is_deleted As IsDeleted, create_by AS CreateBy, create_time AS CreateTime, update_by AS UpdateBy, update_time AS UpdateTime from comment_reply
            where reply_id  IN @ReplyId and shop_id=@ShopId and is_deleted=0
            UNION
            select id AS Id, reply_id AS ReplyId, reply_part_type AS ReplyPartType, reply_type AS ReplyType, shop_id AS ShopId, 
         shop_name As ShopName, channel AS Channel, order_type AS OrderType,
                order_no AS OrderNo, check_status AS CheckStatus, check_comment AS CheckComment, content AS Content, 
               is_deleted As IsDeleted, create_by AS CreateBy, create_time AS CreateTime, update_by AS UpdateBy, update_time AS UpdateTime from comment_reply
                where reply_id IN(

                select Id from comment_reply


                    where reply_id  IN @ReplyId and shop_id=@ShopId and is_deleted=0
            
            )";
            parameters.Add("@ShopId", shopId);
            parameters.Add("@ReplyId", ids);
           // parameters.Add("@CheckStatus", CheckStatusEnum.审核通过.ToInt());

            IEnumerable<CommentReplyDO> commentReply = null;
            await OpenConnectionAsync(async conn => commentReply = await conn.QueryAsync<CommentReplyDO>(sql, parameters));
            return commentReply?.ToList();
        }

        /// <summary>
        /// 得到评价的图片信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<CommentImageDO>> GetCommentImageList(List<long> ids, int relationType)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine("where 1=1 and is_deleted=0");
            builder.AppendLine(" and comment_id IN @Ids");
            parameters.Add("@Ids", ids);
            builder.AppendLine(" and relation_type = @RelationType");
            parameters.Add("@RelationType", relationType);
            var list = await GetListAsync<CommentImageDO>(builder.ToString(),
                parameters, false);
            return list?.ToList();
        }

        /// <summary>
        /// 得到点评信息
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<CommentDO>> GetCommentListByCommentIds(List<long> Ids, long shopId, int checkStatus)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine("where 1=1");
            builder.AppendLine(" and is_deleted=0");
            builder.AppendLine(" and shop_Id=@ShopId");
            parameters.Add("@ShopId", shopId);
            builder.AppendLine(" and id IN @Ids");
            parameters.Add("@Ids", Ids);
            builder.AppendLine(" and check_status=@CheckStatus");
            parameters.Add("@CheckStatus", checkStatus);

            var data = await GetListAsync<CommentDO>(builder.ToString(), parameters, false);
            return data?.ToList();

        }

        /// <summary>
        /// 得到点评信息分数
        /// </summary>
        /// <param name="Ids"></param>
        /// <param name="shopId"></param>
        /// <param name="checkStatus"></param>
        /// <returns></returns>
        public async Task<List<GetCommentListDO>> GetCommentListByCommentIdsForScore(List<long> Ids, long shopId, int checkStatus)
        {
            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            parameters.Add("@ShopId", shopId);
            parameters.Add("@Ids", Ids);
            parameters.Add("@CheckStatus", checkStatus);


            var sql = @" select A.id AS Id,A.car_id AS CarId,A.channel AS Channel,
                                    A.check_comment AS CheckComment,A.check_status AS CheckStatus,A.content AS Content,
                                    A.create_by As CreateBy,A.create_time AS CreateTime,A.head_url AS HeadUrl,A.is_anonymous AS IsAnonymous,
                                    A.is_best As IsBest,A.is_deleted As IsDeleted,A.is_top AS IsTop,A.like_num AS LikeNum,A.office_type AS OfficeType,
                                    A.order_id AS OrderId,A.order_no As OrderNo,
                                    A.order_type AS OrderType,A.shop_id AS ShopId,
                                    A.shop_name AS ShopName,A.shop_reply_type AS ShopReplyType,
                                    A.update_by AS UpdateBy,A.update_time AS UpdateTime,
                                    A.user_id AS UserId,A.user_nick_name AS NickName,A.user_reply_type As UserReplyType,
                                    B.score AS ShopScore  from `comment` A
                                    inner join  comment_detail_shop B
                                    on A.id=B.comment_id
                                    AND A.is_deleted=0
                                    AND B.is_deleted=0 AND  A.check_status=@CheckStatus
                                    AND A.shop_id=@ShopId  and A.id in @Ids";

            IEnumerable<GetCommentListDO> data = null;
            await OpenConnectionAsync(async conn => data = await conn.QueryAsync<GetCommentListDO>(sql, parameters));

            return data?.ToList();
        }


        /// <summary>
        /// 保存评论信息
        /// </summary>
        /// <param name="commentDo"></param>
        /// <param name="commentReplyDo"></param>
        /// <param name="createBy"></param>
        /// <returns></returns>
        public async Task<long> SaveCommentReplyInfo(CommentDO commentDo, CommentReplyDO commentReplyDo,
            string createBy)
        {
            long id = 0;

            await OpenConnectionAsync(async conn =>
            {
                var tran = conn.BeginTransaction();

                try
                {
                    id = await InsertAsync(commentReplyDo);

                    string sql =
                        "Update comment set update_by=@UpdateBy,update_time=NOW(3),shop_reply_type=1 where id=@CommentId and is_deleted=0 ";
                    var parameters = new DynamicParameters();
                    parameters.Add("@UpdateBy", createBy);
                    parameters.Add("@CommentId", commentDo.Id);
                    await conn.ExecuteAsync(sql, parameters);
                }

                catch (Exception e)
                {
                    id = 0;
                    tran.Rollback();
                }
                finally
                {
                    tran.Commit();
                }
            });

            return id;
        }

        /// <summary>
        /// 得到今日好评数
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<int> GetTodayGoodCommentNum(int shopId)
        {
            var condition = new StringBuilder();
            var parameters = new DynamicParameters();

            var sql =@"
            select Count(1) AS GoodCommentNum from comment_detail_shop
            where shop_id = @ShopId
            and is_deleted = 0
            and score = 5
            and create_time between @Today and @Tomorrow";
 
            parameters.Add("@ShopId", shopId);

            parameters.Add("@Today", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));

            parameters.Add("@Tomorrow", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));

            var num = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                num = await conn.QueryFirstOrDefaultAsync<int>(sql, parameters);
            });

            return num;
        }
        /// <summary>
        /// 得到今日差评数
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<int> GetTodayNavigateCommentNum(int shopId)
        {
            var condition = new StringBuilder();
            var parameters = new DynamicParameters();

            var sql = @"
            select Count(1) AS GoodCommentNum from comment_detail_shop
            where shop_id = @ShopId
            and is_deleted = 0
            and score <=3
            and create_time between @Today and @Tomorrow";

            parameters.Add("@ShopId", shopId);

            parameters.Add("@Today", DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));

            parameters.Add("@Tomorrow", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00"));

            var num = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                num = await conn.QueryFirstOrDefaultAsync<int>(sql, parameters);
            });

            return num;
        }

        /// <summary>
        /// 得到今日点评数
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<GetTodayCommentResponse> GetTodayCommentCount(int shopId)
        {
            GetTodayCommentResponse getTodayCommentResponse = new GetTodayCommentResponse();
            int goodNum = 0;
            int navigateNum = 0;

            var tasklist = new Task[]
            {
                Task.Factory.StartNew(async () => { goodNum = await GetTodayGoodCommentNum(shopId); }),
                Task.Factory.StartNew(function: async () =>
                {
                    navigateNum = await GetTodayNavigateCommentNum(shopId);
                }),

            };
            await Task.WhenAll(tasklist).ContinueWith(_ =>
            {
                getTodayCommentResponse.GoodCommentNum = goodNum;
                getTodayCommentResponse.NavigateCommentNum = navigateNum;
            });

            return getTodayCommentResponse;
        }

        /// 查询我的评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<CommentDO>> GetMyCommentList(GetMyCommentListRequest request)
        {
            var sqlCount = @"SELECT
	                    count(id) cs FROM
	                    COMMENT
                    WHERE user_id = @UserId
	                    AND is_deleted =0  ";
            var param = new DynamicParameters();
            param.Add("@UserId", request.UserId);
            var condition = new StringBuilder();
            int count = 0;
            await OpenSlaveConnectionAsync(async conn =>
                count = await conn.ExecuteScalarAsync<int>(sqlCount, param)
            );
            PagedEntity<CommentDO> result = new PagedEntity<CommentDO>();
            if (count > 0) 
            {
                var sql = @"SELECT id, order_no OrderNo,
	order_type OrderType,
	user_id UserId,
	head_url HeadUrl,
	user_nick_name UserNickName,
	car_id CarId,
	shop_id ShopId,
	shop_name ShopName,
	content,
	is_anonymous IsAnonymous,
	like_num LikeNum,
    is_best IsBest,
    create_time CreateTime
    FROM COMMENT
                    WHERE user_id = @UserId
	                    AND is_deleted =0  ";

                var Offset = (request.PageIndex - 1) * request.PageSize;
                param.Add("@Offset", Offset);
                param.Add("@PageSize", request.PageSize);
                condition.Append(" ORDER BY id desc");
                condition.Append(" limit @Offset,@PageSize;");
                sql = sql + condition.ToString();
                IEnumerable<CommentDO> comments = null;
                await OpenSlaveConnectionAsync(async conn =>
                    comments = await conn.QueryAsync<CommentDO>(sql, param)
                );
                result.Items = comments.ToList();
            }
            result.TotalItems = count;
            return result;

        }


        /// <summary>
        /// 得到回评的信息
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <returns></returns>
        public async Task<List<CommentReplyDO>> GetCommentReplyListByCommentId(long commentId)
        {
            
            var sql = @"
            select id, reply_id ReplyId, reply_part_type ReplyPartType, reply_type ReplyType, shop_id ShopId, 
         shop_name ShopName, channel Channel, order_type OrderType,
                order_no OrderNo, check_status CheckStatus, check_comment CheckComment, content Content, 
                create_by CreateBy, create_time CreateTime from comment_reply
            where reply_id = @ReplyId AND check_status = 1
            UNION
            select id, reply_id ReplyId, reply_part_type ReplyPartType, reply_type ReplyType, shop_id ShopId, 
         shop_name ShopName, channel Channel, order_type  OrderType,
                order_no OrderNo, check_status CheckStatus, check_comment CheckComment, content Content, 
                create_by CreateBy, create_time  CreateTime from comment_reply
                where reply_id IN(
                select Id from comment_reply
                    where reply_id  = @ReplyId
            ) AND check_status = 1";
            var parameters = new DynamicParameters();
            parameters.Add("@ReplyId", commentId);
            IEnumerable<CommentReplyDO> commentReply = new List<CommentReplyDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                commentReply = await conn.QueryAsync<CommentReplyDO>(sql, parameters);
            });
            return commentReply?.ToList();
        }

        /// <summary>
        /// 得到追评的主点评信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<CommentDO>> GetAppendCommentDo(long id,int shopId)
        {
            var parameters = new DynamicParameters();
            var sql = @"select A.id AS Id,A.car_id AS CarId,A.channel AS Channel,
            A.check_comment AS CheckComment,A.check_status AS CheckStatus,A.content AS Content,
                A.create_by As CreateBy,A.create_time AS CreateTime,A.head_url AS HeadUrl,A.is_anonymous AS IsAnonymous,
                A.is_best As IsBest,A.is_deleted As IsDeleted,A.is_top AS IsTop,A.like_num AS LikeNum,A.office_type AS OfficeType,
                A.order_id AS OrderId,A.order_no As OrderNo,
                A.order_type AS OrderType,A.shop_id AS ShopId,
                A.shop_name AS ShopName,A.shop_reply_type AS ShopReplyType,
                A.update_by AS UpdateBy,A.update_time AS UpdateTime,
                A.user_id AS UserId,A.user_nick_name AS NickName,A.user_reply_type As UserReplyType from comment_reply B
            inner join `comment` A
                on A.id = B.reply_id
            and B.id = @Id
            and B.is_deleted = 0 and A.is_deleted = 0
            and A.shop_id = @ShopId
            and A.check_status = @CheckStatus";

            parameters.Add("@Id",id);
            parameters.Add("@ShopId", shopId);
            parameters.Add("@CheckStatus", CheckStatusEnum.审核通过.ToInt());

            IEnumerable<CommentDO> comments = null;
            await OpenConnectionAsync(async conn =>
            comments = await conn.QueryAsync<CommentDO>(sql, parameters)
                );
            return comments?.ToList();
        }
    }
}






