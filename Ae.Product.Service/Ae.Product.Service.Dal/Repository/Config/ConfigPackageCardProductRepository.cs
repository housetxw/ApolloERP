using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Dal.Model.Condition;
using Ae.Product.Service.Dal.Model.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Config
{
    public class ConfigPackageCardProductRepository : AbstractRepository<ConfigPackageCardProductDO>,
        IConfigPackageCardProductRepository
    {
        public ConfigPackageCardProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<PagedEntity<PackageCardProductVo>> GetPackageCardProductPageList(
            PackageCardProductPageListCondition request)
        {
            string sql = @"SELECT
	a.product_code AS ProductCode,
	a.`name` AS `Name`,
	a.display_name AS DisplayName,
	a.brand AS Brand,
	a.standard_price AS StandardPrice,
	a.sales_price AS SalesPrice,
	a.unit AS Unit,
	a.image1 AS Image1,
	a.advertisement AS Advertisement,
	a.on_sale AS OnSale,
	b.`id` AS PackageCardId,
	b.rank AS `Rank`,
	b.type AS `Type` 
FROM
	fct_product a
	INNER JOIN config_package_card_product b ON ( a.product_code = b.pid AND b.is_deleted = 0 ) 
WHERE a.on_sale = 1 and
	a.is_deleted = 0 {0}
ORDER BY
	b.rank 
	LIMIT @startIndex,
	@pageSize;";

            string sqlCount = @"SELECT
	COUNT( * ) 
FROM
	fct_product a
	INNER JOIN config_package_card_product b ON ( a.product_code = b.pid AND b.is_deleted = 0 ) 
WHERE a.on_sale = 1 and
	a.is_deleted = 0 {0};";

            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            para.Add("@startIndex", (request.PageIndex - 1) * request.PageSize);
            para.Add("@pageSize", request.PageSize);
            if (request.Type != null && request.Type.Any())
            {
                condition.Append(" AND b.type IN @type");
                para.Add("@type", request.Type);
            }

            if (request.OnSale.HasValue)
            {
                condition.Append(" AND a.on_sale = @onSale");
                para.Add("@onSale", request.OnSale.Value);
            }

            if (request.IsRetail.HasValue)
            {
                condition.Append(" AND a.is_retail = @isRetail");
                para.Add("@isRetail", request.IsRetail.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Key))
            {
                condition.Append(" AND (a.product_code = @productCode OR a.`name` LIKE @name)");
                para.Add("@productCode", request.Key);
                para.Add("@name", $"%{request.Key}%");
            }

            int totalCount = 0;
            List<PackageCardProductVo> data = new List<PackageCardProductVo>();
            Task dataTask = OpenSlaveConnectionAsync(async conn =>
            {
                data = (await conn.QueryAsync<PackageCardProductVo>(string.Format(sql, condition.ToString()), para))
                    ?.AsList() ?? new List<PackageCardProductVo>();
            });

            Task countTask = OpenSlaveConnectionAsync(async conn =>
            {
                totalCount =
                    await conn.ExecuteScalarAsync<int>(string.Format(sqlCount, condition.ToString()), para);
            });
            await Task.WhenAll(dataTask, countTask);
            return new PagedEntity<PackageCardProductVo>()
            {
                TotalItems = totalCount,
                Items = data
            };
        }

        public async Task<List<ConfigPackageCardProductDO>> GetPackageCardProductByPidList(List<string> pidList,
            bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@pidList", pidList);
            var result = await GetListAsync<ConfigPackageCardProductDO>("WHERE `pid` IN @pidList", para, !readOnly);
            return result?.AsList() ?? new List<ConfigPackageCardProductDO>();
        }
    }
}
