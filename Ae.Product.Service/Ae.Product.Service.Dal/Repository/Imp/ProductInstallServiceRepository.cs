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
    public class ProductInstallServiceRepository: AbstractRepository<RelProductInstallserviceDO>, IProductInstallServiceRepository
    {
        public ProductInstallServiceRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<List<RelProductInstallserviceDO>> GetRelInstallServiceByProductIds(List<string> productIds)
        {
            var para = new DynamicParameters();
            para.Add("@productIds", productIds);
            var result = await GetListAsync("WHERE product_id in @productIds", para);

            return result?.ToList() ?? new List<RelProductInstallserviceDO>();
        }

        public async Task<List<RelProductInstallserviceDO>> GetRelInstallServiceByPidList(List<string> pidList, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@pidList", pidList);
            var result = await GetListAsync("WHERE `pid` in @pidList", para, !readOnly);

            return result?.ToList() ?? new List<RelProductInstallserviceDO>();
        }

        public async Task<List<RelProductInstallserviceDO>> GetRelInstallServiceByPidList(List<string> pidList,
            string serviceId, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@pidList", pidList);
            para.Add("@serviceId", serviceId);
            var result = await GetListAsync("WHERE `pid` in @pidList AND `service_id` IN @serviceId", para, !readOnly);

            return result?.ToList() ?? new List<RelProductInstallserviceDO>();
        }

        /// <summary>
        /// 产品安装服务配置页
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<ProductInstallDTO>> GetProductInstallServicePageList(
            ProductInstallServicePageListCondition request)
        {
            string sql = @"SELECT
    a.`id` AS ProductId,
	a.product_code AS Pid,
	a.`name` AS `Name`,
	a.brand AS Brand,
	a.sales_price AS Price,
	a.on_sale AS OnSale 
FROM
	fct_product a
	LEFT JOIN rel_product_installservice b ON ( a.id = b.product_id AND b.is_deleted = 0 ) 
WHERE
	a.is_deleted = 0 
    AND a.class_type = 2
	AND a.product_attribute = 0{0}
GROUP BY
	a.id 
ORDER BY
	a.id DESC 
	LIMIT @startIndex,
	@pageSize;";

            string sqlCount = @"SELECT
	COUNT( DISTINCT a.id ) 
FROM
	fct_product a
	LEFT JOIN rel_product_installservice b ON ( a.id = b.product_id AND b.is_deleted = 0 ) 
WHERE
	a.is_deleted = 0 
	AND a.class_type = 2 
	AND a.product_attribute = 0{0};";

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

            if (request.HasInstallService.HasValue)
            {
                if (request.HasInstallService.Value == 0)
                {
                    condition.Append(" AND b.id IS NULL");
                    ;
                }
                else
                {
                    condition.Append(" AND b.id IS NOT NULL");
                }
            }

            int totalCount = 0;
            List<ProductInstallDTO> data = new List<ProductInstallDTO>();
            Task dataTask = OpenSlaveConnectionAsync(async conn =>
            {
                data = (await conn.QueryAsync<ProductInstallDTO>(string.Format(sql, condition.ToString()), para))
                    ?.AsList() ?? new List<ProductInstallDTO>();
            });

            Task countTask = OpenSlaveConnectionAsync(async conn =>
            {
                totalCount =
                    await conn.ExecuteScalarAsync<int>(string.Format(sqlCount, condition.ToString()), para);
            });
            await Task.WhenAll(dataTask, countTask);
            return new PagedEntity<ProductInstallDTO>()
            {
                TotalItems = totalCount,
                Items = data
            };
        }
    }
}
