using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
namespace Ae.Shop.Api.Dal.Repositorys
{
    public class HomeCareProductRepository : AbstractRepository<HomeCareProductDO>, IHomeCareProductRepository
    {
        public HomeCareProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopWMSSqlReadOnly");
        }

        public async Task<List<HomeCareProductDO>> GetHomeCareProductsByTechId(HomeCareRecordDO request)
        {
            var sql = @"SELECT
						hcp.id Id,
						hcp.record_id RecordId,
						hcp.product_id ProductId,
						hcp.product_name ProductName,hcp.category_name CategoryName,
						hcp.num Num,
						hcp.install_num InstallNum,
						hcp.receive_num ReceiveNum,
						hcp.actual_num ActualNum,
						hcp.exception_num ExceptionNum,
						hcp.less_num LessNum,
						hcp.status status,
						hcp.remark Remark,
						hcp.create_by CreateBy,
						hcp.create_time CreateTime
					FROM
						home_care_product hcp
						INNER JOIN home_care_record hcr ON hcr.id = hcp.record_id
						AND hcp.is_deleted = 0 
					WHERE
						hcr.shop_Id = @shop_Id
						AND hcr.tech_id = @tech_id
						AND hcr.is_deleted = 0 and hcp.status in @statuslist";

            var param = new DynamicParameters();
            param.Add("@shop_Id", request.ShopId);
            param.Add("@tech_id", request.TechId);
            param.Add("@statuslist", request.StatusList);

            IEnumerable<HomeCareProductDO> result = null;

            await OpenConnectionAsync(async conn => result = await conn.QueryAsync<HomeCareProductDO>(sql, param));

            return result?.ToList() ?? new List<HomeCareProductDO>();
        }

        /// <summary>
        /// 订单安装更新数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateProductInstallNum(HomeCareProductDO request)
        {
            var sql = @"UPDATE home_care_product 
                        SET install_num =install_num+ @install_num,
                        receive_num=receive_num-@receive_num,
                        status = @status,
                        update_by = @updateby,
                        update_time = SYSDATE( ) ,
                        remark=CONCAT(remark,@remark)
                        WHERE
	                        id = @id ";

            var result = -1;
            var param = new DynamicParameters();
            param.Add("@id", request.Id);
            param.Add("@updateby", request.UpdateBy);
            param.Add("@install_num", request.InstallNum);
            param.Add("@receive_num", request.ReceiveNum);
            param.Add("@status", request.Status);
            param.Add("@remark", request.Remark);

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 回写领料表记录
        /// </summary>
        /// <param name="request"></param>
        /// <param name="type">1->实收 2->货损 3->缺少</param>
        /// <returns></returns>
        public async Task<int> UpdateProductReturnNum(HomeCareProductDO request, int type)
        {
            var sql = "";

            var result = -1;
            var param = new DynamicParameters();
            param.Add("@id", request.Id);
            param.Add("@updateby", request.UpdateBy);
            param.Add("@status", request.Status);
            param.Add("@receive_num", request.ReceiveNum);
            param.Add("@remark", request.Remark);

            if (type == 1)
            {
                sql = @"UPDATE home_care_product 
                        SET  receive_num = receive_num - @receive_num,   
                        actual_num = actual_num + @actual_num,
                        status = @status,
                        update_by = @updateby,
                        update_time = SYSDATE( ),
                        remark=CONCAT(remark,@remark)
                        WHERE
	                        id = @id";

                param.Add("@actual_num", request.ActualNum);
            }
            else if (type == 2)
            {
                sql = @"UPDATE home_care_product 
                        SET  receive_num = receive_num - @receive_num,
                        exception_num = exception_num + @exception_num,                       
                        status = @status,
                        update_by = @updateby,
                        update_time = SYSDATE( ) ,
                        remark=CONCAT(remark,@remark)
                        WHERE
	                        id = @id";

                param.Add("@exception_num", request.ExceptionNum);
            }
            else if (type == 3)
            {
                sql = @"UPDATE home_care_product 
                        SET receive_num = receive_num - @receive_num,
                        less_num = less_num + @less_num,
                        status = @status,
                        update_by = @updateby,
                        update_time = SYSDATE( ) ,
                        remark=CONCAT(remark,@remark)
                        WHERE
	                        id = @id";

                param.Add("@less_num", request.LessNum);
            }

            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }
    }
}




