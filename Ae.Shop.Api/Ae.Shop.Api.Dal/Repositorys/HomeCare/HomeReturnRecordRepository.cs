using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.HomeCare
{

    public class HomeReturnRecordRepository : AbstractRepository<HomeReturnRecordDO>, IHomeReturnRecordRepository
    {
        public HomeReturnRecordRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSqlReadOnly");
        }

        public async Task<PagedEntity<HomeReturnRecordDO>> GetHomeReturnRecordPages(HomeCareRecordRequest request)
        {
            PagedEntity<HomeReturnRecordDO> res = new PagedEntity<HomeReturnRecordDO>();
            var total = 0;

            var sqlWhere = new StringBuilder(" where hrr.shop_id=@shopId ");
            var param = new DynamicParameters();
            var dateTime = new DateTime(2019, 10, 1);

            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);
            param.Add("@shopId", request.ShopId);


            if (!string.IsNullOrWhiteSpace(request.TechId))
            {
                param.Add("@TechId", request.TechId);
                sqlWhere.Append(" and hrr.tech_id =@TechId");
            }

            if (!string.IsNullOrWhiteSpace(request.ProductName))
            {
                var productId = request.ProductName;
                var productNames = request.ProductName.Split(new char[] { ' ', '　', ',', '，', ';', '；', '\\', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                if (productNames.Any())
                {
                    string productName = string.Join("%", productNames);
                    sqlWhere.Append(" AND (hrp.product_name LIKE N'%" + productName.Replace("'", "''") + "%'  OR hrp.product_id=@productId)");
                    param.Add("@productId", productId);
                }
            }

            if (request.StartTime.HasValue &&
               request.StartTime.Value.Subtract(dateTime).TotalDays > 0)
            {
                sqlWhere.Append(" and hrr.receive_time>=@CreateStartTime");
                param.Add("@CreateStartTime", request.StartTime);
            }

            if (request.EndTime.HasValue &&
                request.EndTime.Value.Subtract(dateTime).TotalDays > 0)
            {
                sqlWhere.Append(" and hrr.receive_time<=@CreateEndTime");
                param.Add("@CreateEndTime", request.EndTime);

            }

            var sqlCount = @"SELECT
	                        count(distinct hrr.id) 
                        FROM
	                        home_return_record hrr INNER JOIN home_return_product hrp
                            ON hrr.id = hrp.record_id AND hrp.is_deleted = 0
	                       " + sqlWhere.ToString();

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            var sql = $@"select
                        hrr.id Id
                        , hrr.shop_id ShopId
                        , hrr.receive_name ReceiveName
                        , hrr.receive_time ReceiveTime
                        , hrr.category_num CategoryNum
                        , hrr.sum_product_num SumProductNum
                        , hrr.status Status
                        , hrr.remark Remark
                        , hrr.create_by CreateBy
                        , hrr.create_time CreateTime
                        FROM home_return_record hrr
                        WHERE hrr.id IN (
	                        SELECT t.id
	                        FROM (
		                        SELECT id
		                        FROM (
			                        SELECT DISTINCT  hrr.id AS id
			                        FROM home_return_record hrr
				                        INNER JOIN home_return_product hrp ON hrr.id = hrp.record_id and hrp.is_deleted = 0
			                          {sqlWhere.ToString()}  
			                        ORDER BY hrr.id
		                        ) T
		                        ORDER BY t.id DESC
		                        LIMIT @index, @size
	                        ) t
                        )";

            IEnumerable<HomeReturnRecordDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<HomeReturnRecordDO>(sql, param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;
        }
    }
}
