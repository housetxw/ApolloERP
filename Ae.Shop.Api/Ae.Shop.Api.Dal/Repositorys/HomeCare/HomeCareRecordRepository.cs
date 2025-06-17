using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public class HomeCareRecordRepository : AbstractRepository<HomeCareRecordDO>, IHomeCareRecordRepository
    {
        public HomeCareRecordRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSqlReadOnly");
        }

        public async Task<PagedEntity<HomeCareRecordDO>> GetHomeCareRecordPages(HomeCareRecordRequest request)
        {
            PagedEntity<HomeCareRecordDO> res = new PagedEntity<HomeCareRecordDO>();
            var total = 0;

            var sqlWhere = new StringBuilder(" where hcr.shop_id=@shopId ");
            var param = new DynamicParameters();
            var dateTime = new DateTime(2019, 10, 1);

            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);
            param.Add("@shopId", request.ShopId);


            if (!string.IsNullOrWhiteSpace(request.TechId))
            {
                param.Add("@TechId", request.TechId);
                sqlWhere.Append(" and hcr.tech_id =@TechId");
            }

            if (!string.IsNullOrWhiteSpace(request.ProductName))
            {
                var productId = request.ProductName;
                var productNames = request.ProductName.Split(new char[] { ' ', '　', ',', '，', ';', '；', '\\', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                if (productNames.Any())
                {
                    string productName = string.Join("%", productNames);
                    sqlWhere.Append(" AND (hcp.product_name LIKE N'%" + productName.Replace("'", "''") + "%'  OR hcp.product_id=@productId)");
                    param.Add("@productId", productId);
                }
            }

            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                param.Add("@status", request.Status);
                sqlWhere.Append(" and hcr.status =@status");
            }

            if (request.StartTime.HasValue &&
               request.StartTime.Value.Subtract(dateTime).TotalDays > 0)
            {
                sqlWhere.Append(" and hcr.receive_time>=@CreateStartTime");
                param.Add("@CreateStartTime", request.StartTime);
            }

            if (request.EndTime.HasValue &&
                request.EndTime.Value.Subtract(dateTime).TotalDays > 0)
            {
                sqlWhere.Append(" and hcr.receive_time<=@CreateEndTime");
                param.Add("@CreateEndTime", request.EndTime);

            }

            var sqlCount = @"SELECT
	                        count(distinct hcr.id) 
                        FROM
	                        home_care_record hcr INNER JOIN home_care_product hcp ON hcr.id = hcp.record_id AND hcp.is_deleted = 0
	                       " + sqlWhere.ToString();

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            var sql = $@"SELECT
	                    hcr.id Id,
	                    hcr.shop_id ShopId,
	                    hcr.shop_name ShopName,
	                    hcr.tech_id TechId,
	                    hcr.tech_name TechName,
	                    hcr.receive_name ReceiveName,
	                    hcr.receive_time ReceiveTime,
	                    hcr.status Status,
	                    hcr.category_num CategoryNum,
	                    hcr.sum_product_num SumProductNum,
	                    hcr.remark Remark,
	                    hcr.create_by CreateBy,
	                    hcr.create_time CreateTime
                        FROM home_care_record hcr
                        WHERE hcr.id IN (
	                        SELECT t.id
	                        FROM (
		                        SELECT id
		                        FROM (
			                        SELECT DISTINCT  hcr.id AS id
			                        FROM home_care_record hcr
				                        INNER JOIN home_care_product hcp ON hcr.id = hcp.record_id
			                          {sqlWhere.ToString()}  
			                        ORDER BY hcr.id
		                        ) T
		                        ORDER BY T.id DESC
		                        LIMIT @index, @size
	                        ) t
                        )";

            IEnumerable<HomeCareRecordDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<HomeCareRecordDO>(sql, param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;
        }

        public async Task<int> UpdateHomeCareRecordStatus(HomeCareRecordDO request)
        {
            var sql = @" UPDATE home_care_record 
                    SET status = @status,
                    update_by = @update_by,
                    update_time = SYSDATE( ) 
                    WHERE
	                    id = @id";
            var param = new DynamicParameters();
            param.Add("@id", request.Id);
            param.Add("@update_by", request.UpdateBy);
            param.Add("@status", request.Status);

            var result = -1;

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }
    }
}
