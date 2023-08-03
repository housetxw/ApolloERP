using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public class TransferTaskRepository : AbstractRepository<WarehouseTransferPackageDO>, ITransferTaskRepository
    {
        public TransferTaskRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("WMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("WMSSqlReadOnly");
        }

        public async Task<List<WarehouseTransferPackageDO>> GetWarehouseTransferPackages(WarehouseTransferPackageDO request)
        {
            var conditions = new StringBuilder();
            var param = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(request.TransferId))
            {
                conditions.Append(" and transfer_id =@transfer_id");
                param.Add("@transfer_id", request.TransferId);
            }

            if (!string.IsNullOrWhiteSpace(request.TransferType))
            {
                conditions.Append(" and transfer_type =@transfer_type");
                param.Add("@transfer_type", request.TransferType);
            }

            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                conditions.Append(" and status =@Status");
                param.Add("@Status", request.Status);
            }

            if (request.PackageNos.Any())
            {
                conditions.Append(" and delivery_code in @packageNos");
                param.Add("@packageNos", request.PackageNos);
            }
            var sql = @"SELECT
                        id Id,
                        transfer_id TransferId,
	                    transfer_type TransferType,
                        delivery_type DeliveryType,
	                    delivery_code DeliveryCode,
                        delivery_company DeliveryCompany,
	                    delivery_fee DeliveryFee,
                        delivery_weight DeliveryWeight,delivery_tel DeliveryTel,
	                    status Status,
                        remark Remark,
	                    create_by CreateBy,
                        create_time CreateTime
                    FROM
                        warehouse_transfer_package wtp
                    WHERE
                        1 = 1 " + conditions.ToString();

            IEnumerable<WarehouseTransferPackageDO> transferPackages = null;

            await OpenConnectionAsync(async conn => transferPackages = await conn.QueryAsync<WarehouseTransferPackageDO>(sql, param));

            return transferPackages.Any() ? transferPackages.ToList() : new List<WarehouseTransferPackageDO>();
        }

        public async Task<List<WarehouseTransferProductDO>> GetWarehouseTransferProducts(WarehouseTransferProductDO request)
        {
            var conditions = new StringBuilder();
            var param = new DynamicParameters();
            if (request.Id > 0)
            {
                conditions.Append(" and id =@Id");
                param.Add("@Id", request.Id);
            }

            if (request.TransferId > 0)
            {
                conditions.Append(" and transfer_id =@TransferId");
                param.Add("@TransferId", request.TransferId);
            }

            var sql = @"select id Id
                        ,transfer_id TransferId
                        ,transfer_product_id
                        ,product_id ProductId
                        ,product_name ProductName
                        ,num Num 
                        ,receive_num ReceiveNum
                        ,cost Cost
                        ,price Price
                        ,vender_short_name VenderShortName
                        ,vender_id VenderId
                        ,batch_id BatchId
                        ,owner_id OwnerId
                        ,owner_name OwnerName from warehouse_transfer_product WHERE 1=1 " + conditions.ToString();

            IEnumerable<WarehouseTransferProductDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<WarehouseTransferProductDO>(sql, param));
            return productDOs?.ToList() ?? new List<WarehouseTransferProductDO>();
        }

        public async Task<WarehouseTransferTaskDO> GetWarehouseTransferTask(WarehouseTransferTaskDO request)
        {
            var conditions = new StringBuilder();
            var param = new DynamicParameters();
            IEnumerable<WarehouseTransferTaskDO> transferTasks = null;

            if (!string.IsNullOrWhiteSpace(request.TransferId))
            {
                conditions.Append(" and transfer_id=@transfer_id ");
                param.Add("@transfer_id", request.TransferId);
            }

            if (!string.IsNullOrWhiteSpace(request.TransferType))
            {
                conditions.Append(" and transfer_type=@TransferType ");
                param.Add("@TransferType", request.TransferType);
            }

            if (request.SourceWarehouse > 0) {
                conditions.Append(" and source_warehouse=@SourceWarehouse ");
                param.Add("@SourceWarehouse", request.SourceWarehouse);
            }

            if (request.TargetWarehouse > 0)
            {
                conditions.Append(" and target_warehouse=@TargetWarehouse ");
                param.Add("@TargetWarehouse", request.TargetWarehouse);
            }

            if (!string.IsNullOrWhiteSpace(request.TaskStatus)) {
                conditions.Append(" and task_status=@TaskStatus ");
                param.Add("@TaskStatus", request.TaskStatus);
            }

            if (request.Id > 0)
            {
                conditions.Append(" and id=@id ");
                param.Add("@id", request.Id);
            }

            var sql = @"select id Id
                        ,source_warehouse SourceWarehouse
                        ,source_warehouse_name SourceWarehouseName
                        ,target_warehouse TargetWarehouse
                        ,target_warehouse_name TargetWarehouseName
                        ,transfer_id TransferId
                        ,transfer_type TransferType
                        ,task_status TaskStatus from  warehouse_transfer_task where 1=1 " + conditions.ToString();

            await OpenConnectionAsync(async conn => transferTasks = await conn.QueryAsync<WarehouseTransferTaskDO>(sql, param));

            return transferTasks.FirstOrDefault();
        }

        public async Task<int> UpdatePackageStatus(WarehouseTransferPackageDO request)
        {
            if (request.PackageNos.Any())
            {
                var param = new DynamicParameters();
                param.Add("@update_by", request.UpdateBy);
                param.Add("@status", request.Status);

                param.Add("@packageNos", request.PackageNos);

                var sql = $@"update warehouse_transfer_package set update_by=@update_by,update_time =SYSDATE(),status=@status
	                            where delivery_code in @packageNos";

                int result = -1;
                await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
                return result;
            }
            return -1;
        }

        public async Task<int> UpdateProductPackageRelation(ProductPackageRelationDO request)
        {
            var sql = @"UPDATE product_package_relation 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        receive_num = @receive_num 
                        WHERE
	                        transfer_product_id = @transfer_product_id and package_no=@package_no";

            var param = new DynamicParameters();
            param.Add("@update_by", request.UpdateBy);
            param.Add("@receive_num", request.ReceiveNum);
            param.Add("@transfer_product_id", request.TransferProductId);
            param.Add("@package_no", request.PackageNo);
            var result = -1;

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<int> UpdateProductPackageRelationStatus(ProductPackageRelationDO request)
        {
            if (request.PackageNos.Any())
            {
                var param = new DynamicParameters();
                param.Add("@update_by", request.UpdateBy);
                param.Add("@status", request.Status);
                param.Add("@packageNos", request.PackageNos);

                var sql = $@"UPDATE product_package_relation 
                            SET update_by = @update_by,
                            update_time = SYSDATE( ),
                            status = @status 
                            WHERE
	                            package_no IN @packageNos";

                int result = -1;
                await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
                return result;
            }
            return -1;
        }

        public async Task<int> UpdateWarehouseTransferProduct(WarehouseTransferProductDO request)
        {
            var sql = @"UPDATE warehouse_transfer_product 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        receive_num =receive_num+ @receive_num 
                        WHERE
	                        id = @id";

            var param = new DynamicParameters();
            param.Add("@update_by", request.UpdateBy);
            param.Add("@receive_num", request.ReceiveNum);
            param.Add("@id", request.Id);

            var result = -1;

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<int> UpdateWarehouseTransferStatus(WarehouseTransferTaskDO request)
        {
            var sql = @"UPDATE warehouse_transfer_task 
                        SET update_by = @update_by,
                        update_time = SYSDATE( ),
                        task_status = @task_status 
                        WHERE
	                        transfer_id = @transfer_id 
	                        AND transfer_type = @transfer_type";

            var param = new DynamicParameters();
            param.Add("@update_by", request.UpdateBy);
            param.Add("@task_status", request.TaskStatus);
            param.Add("@transfer_id", request.TransferId);
            param.Add("@transfer_type", request.TransferType);

            var result = -1;

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }
    }
}
