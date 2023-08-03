using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Dal.Model.WMS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Ae.Shop.Api.Core.Request;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public class AllotTaskRepository : AbstractRepository<AllotTaskDO>, IAllotTaskRepository
    {
        public AllotTaskRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSqlReadOnly");
        }

        public async Task<int> CreateWmsLog(WmsLogDO request)
        {
          return  await InsertAsync<WmsLogDO>(request);
        }

        public async Task<int> AuditAllotTask(AllotTaskDO request)
        {
            var sql = @"UPDATE allot_task 
                        SET is_audit = @is_audit,
                        audit_user = @audit_user,
                        audit_time = SYSDATE( ),
                        audit_remark = @audit_remark,
                        update_by = @update_by,task_status =@task_status,
                        update_time = SYSDATE( ) 
                        WHERE
	                        id = @id";

            var para = new DynamicParameters();
            para.Add("id", request.Id);
            para.Add("is_audit", request.IsAudit);
            para.Add("audit_user", request.AuditUser);
            para.Add("audit_remark", request.AuditRemark);
            para.Add("update_by", request.UpdateBy);
            para.Add("@task_status", request.TaskStatus);

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, para));
            return result;
        }

        public async Task<PagedEntity<AllotTaskDO>> GetAllotPageData(AllotPageRequest request)
        {
            PagedEntity<AllotTaskDO> res = new PagedEntity<AllotTaskDO>();
            var total = 0;

            var sqlWhere = new StringBuilder();
            var param = new DynamicParameters();
            var dateTime = new DateTime(2019, 10, 1);

            param.Add("@index", (request.PageIndex - 1) * request.PageSize);
            param.Add("@size", request.PageSize);

            if(request.ShopId > 0)
            {
                sqlWhere.Append(" and alt.location_id =@ShopId ");
                param.Add("@ShopId", request.ShopId);
            }


            if (!string.IsNullOrWhiteSpace(request.SourceWarehouse))
            {
                sqlWhere.Append(" and alt.source_warehouse =@source_warehouse");
                param.Add("@source_warehouse", request.SourceWarehouse);
            }

            if (!string.IsNullOrWhiteSpace(request.TargetWarehouse))
            {
                sqlWhere.Append(" and alt.target_warehouse =@target_warehouse");
                param.Add("@target_warehouse", request.TargetWarehouse);
            }

            if (!string.IsNullOrWhiteSpace(request.ProductName))
            {
                var productId = request.ProductName;
                var productNames = request.ProductName.Split(new char[] { ' ', '　', ',', '，', ';', '；', '\\', '-', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
                if (productNames.Any())
                {
                    string productName = string.Join("%", productNames);
                    sqlWhere.Append(" AND (ap.product_name LIKE N'%" + productName.Replace("'", "''") + "%'  OR ap.product_id=@productId)");
                    param.Add("@productId", productId);
                }
            }

            if (!string.IsNullOrWhiteSpace(request.TaskStatus))
            {
                param.Add("@status", request.TaskStatus);
                sqlWhere.Append(" and alt.task_status =@status");
            }

            if (request.StartTime.HasValue &&
               request.StartTime.Value.Subtract(dateTime).TotalDays > 0)
            {
                sqlWhere.Append(" and alt.operator_time>=@CreateStartTime");
                param.Add("@CreateStartTime", request.StartTime);
            }

            if (request.EndTime.HasValue &&
                request.EndTime.Value.Subtract(dateTime).TotalDays > 0)
            {
                sqlWhere.Append(" and alt.operator_time<=@CreateEndTime");
                param.Add("@CreateEndTime", request.EndTime);

            }

            var sqlCount = @"SELECT
	                        count( DISTINCT alt.id )
                        FROM
	                        allot_task alt
	                        INNER JOIN allot_product ap ON alt.id = ap.task_id 
	                        AND ap.is_deleted = 0 
                        WHERE
	                        1 =1" + sqlWhere.ToString();

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
            });

            var sql = $@"SELECT
						alt.id Id,
						alt.task_no TaskNo,
						alt.source_warehouse SourceWarehouse,
						alt.source_warehouse_name SourceWarehouseName,
						alt.target_warehouse TargetWarehouse,
						alt.target_warehouse_name TargetWarehouseName,
						alt.task_status TaskStatus,
						alt.task_type TaskType,
						alt.operator Operator,
						alt.operator_time OperatorTime,
						alt.is_audit IsAudit,alt.audit_time AuditTime,alt.audit_user  AuditUser,
						alt.audit_remark AuditRemark,
						alt.remark Remark,
						alt.create_by CreateBy,
						alt.create_time CreateTime 
					FROM
						allot_task alt
                        WHERE alt.id IN (
	                        SELECT t.id
	                        FROM (
		                        SELECT id
		                        FROM (
			                        SELECT DISTINCT  alt.id AS id
			                        FROM allot_task alt
				                        INNER JOIN allot_product ap ON alt.id = ap.task_id AND ap.is_deleted = 0 where 1=1
			                          {sqlWhere.ToString()}  
			                        ORDER BY alt.id 
		                        ) T
		                        ORDER BY t.id DESC
		                        LIMIT @index, @size
	                        ) t
                        ) order by alt.id desc";

            IEnumerable<AllotTaskDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<AllotTaskDO>(sql, param));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;
        }

        public async Task<int> UpdateAllotTask(AllotTaskDO request)
        {
            var result = -1;

            var sql = @"UPDATE allot_task set
                        task_status = @task_status,
                        operator = @operator,
                        operator_time = @operator_time,
                        remark = @remark,
                        update_by = @update_by,
                        update_time = SYSDATE( ) 
                        WHERE
	                        id = @id";

            var param = new DynamicParameters();
            param.Add("@id", request.Id);
            param.Add("@task_status", request.TaskStatus);
            param.Add("@operator", request.Operator);
            param.Add("@operator_time", request.OperatorTime);
            param.Add("@remark", request.Remark);
            param.Add("@update_by", request.UpdateBy);
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<int> UpdateAllotTaskNo(AllotTaskDO request)
        {
            var sql = @"update allot_task set task_no=@task_no where id =@id";

            var para = new DynamicParameters();
            para.Add("id", request.Id);
            para.Add("task_no", request.TaskNo);

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, para));
            return result;
        }

        public async Task<int> UpdateAllotTaskStatus(AllotTaskDO request)
        {
            var result = -1;

            var sql = @"UPDATE allot_task set
                        task_status = @task_status,
                        update_by = @update_by,
                        update_time = SYSDATE( ) 
                        WHERE
	                        task_no = @task_no";

            var param = new DynamicParameters();
            param.Add("@task_no", request.TaskNo);
            param.Add("@task_status", request.TaskStatus);
            param.Add("@update_by", request.UpdateBy);
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }
    }
}
