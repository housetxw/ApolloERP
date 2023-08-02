using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model;
using Ae.Product.Service.Dal.Model.Condition;
using Ae.Product.Service.Dal.Model.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Imp
{
    public class DoorProductRepository : AbstractRepository<DoorProductDO>, IDoorProductRepository
    {
        public DoorProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 查询上门产品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<DoorProductDTO>> GetDoorProductPageList(DoorProductPageListCondition request)
        {
            string sql = @"SELECT
    b.id AS Id,
	a.product_code AS Pid,
	a.`name` AS `Name`,
	a.brand AS Brand,
	a.sales_price AS Price,
	a.on_sale AS OnSale,
	b.free_door_fee AS FreeDoorFee 
FROM
	fct_product a
	LEFT JOIN door_product b ON ( a.product_code = b.pid AND b.is_deleted = 0 ) 
WHERE
	a.is_deleted = 0 AND a.class_type = 2{0}
ORDER BY
	b.id DESC 
	LIMIT @startIndex,
	@pageSize;";

            string sqlCount = @"SELECT
	COUNT( * ) 
FROM
	fct_product a
	LEFT JOIN door_product b ON ( a.product_code = b.pid AND b.is_deleted = 0 ) 
WHERE
	a.is_deleted = 0 AND a.class_type = 2{0};";

            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            para.Add("@startIndex", (request.PageIndex - 1) * request.PageSize);
            para.Add("@pageSize", request.PageSize);
            if (!string.IsNullOrWhiteSpace(request.Key))
            {
                condition.Append(" AND (a.product_code = @productCode OR a.`name` LIKE @name)");
                para.Add("@productCode", request.Key);
                para.Add("@name", $"%{request.Key}%");
            }

            if (!string.IsNullOrWhiteSpace(request.Brand))
            {
                condition.Append(" AND a.brand = @brand");
                para.Add("@brand", request.Brand);
            }

            if (request.CategoryId != null && request.CategoryId.Any())
            {
                condition.Append(" AND a.category_id IN @categoryId");
                para.Add("@categoryId", request.CategoryId);
            }

            if (request.IsDoorProduct.HasValue)
            {
                if (request.IsDoorProduct.Value)
                {
                    condition.Append(" AND b.id IS NOT NULL");
                }
                else
                {
                    condition.Append(" AND b.id IS NULL");
                }
            }

            if (request.FreeDoorFee.HasValue)
            {
                condition.Append(" AND b.free_door_fee = @freeDoorFee");
                para.Add("@freeDoorFee", request.FreeDoorFee.Value);
            }

            if (request.OnSale.HasValue)
            {
                condition.Append(" AND a.on_sale = @onSale");
                para.Add("@onSale", request.OnSale.Value);
            }

            int totalCount = 0;
            List<DoorProductDTO> data = new List<DoorProductDTO>();

            Task dataTask = OpenSlaveConnectionAsync(async conn =>
            {
                data = (await conn.QueryAsync<DoorProductDTO>(string.Format(sql, condition.ToString()), para))
                    ?.AsList() ?? new List<DoorProductDTO>();
            });

            Task countTask = OpenSlaveConnectionAsync(async conn =>
            {
                totalCount =
                    await conn.ExecuteScalarAsync<int>(string.Format(sqlCount, condition.ToString()), para);
            });
            await Task.WhenAll(dataTask, countTask);
            return new PagedEntity<DoorProductDTO>()
            {
                TotalItems = totalCount,
                Items = data
            };
        }

        /// <summary>
        /// Pid查询上门产品
        /// </summary>
        /// <param name="pidList"></param>
        /// <returns></returns>
        public async Task<List<DoorProductDO>> GetDoorProductByPidList(List<string> pidList)
        {
            var para = new DynamicParameters();
            para.Add("@pidList", pidList);
            var result = await GetListAsync<DoorProductDO>("WHERE `pid` IN @pidList", para);
            return result?.AsList() ?? new List<DoorProductDO>();
        }
    }
}
