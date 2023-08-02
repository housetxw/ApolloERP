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
    public class ConfigHotProductRepository : AbstractRepository<ConfigHotProductDo>, IConfigHotProductRepository
    {
        public ConfigHotProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<List<ConfigHotProductDo>> GetConfigHotProduct(string terminalType, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@terminalType", terminalType);
            var result = await GetListAsync("WHERE terminal_type = @terminalType", para, !readOnly);

            return result?.OrderBy(_ => _.Rank)?.ToList() ?? new List<ConfigHotProductDo>();
        }

        /// <summary>
        /// 分页获取热门商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<ConfigHotProductVo>> GetHotProductPageList(HotProductPageListCondition request)
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
    b.`id` AS HotProductId,
	b.rank AS `Rank`,
	b.terminal_type AS TerminalType 
FROM
	fct_product a
	INNER JOIN config_hot_product b ON ( a.product_code = b.pid AND b.is_deleted = 0 ) 
WHERE
	a.is_deleted = 0{0}
ORDER BY
	b.rank 
	LIMIT @startIndex,
	@pageSize;";

            string sqlCount = @"SELECT
	COUNT( * ) 
FROM
	fct_product a
	INNER JOIN config_hot_product b ON ( a.product_code = b.pid AND b.is_deleted = 0 ) 
WHERE
	a.is_deleted = 0{0};";

            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            para.Add("@startIndex", (request.PageIndex - 1) * request.PageSize);
            para.Add("@pageSize", request.PageSize);
            if (request.TerminalType != null)
            {
                condition.Append(" AND b.terminal_type = @terminalType");
                para.Add("@terminalType", request.TerminalType.ToString());
            }

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

            if (request.OnSale.HasValue)
            {
                condition.Append(" AND a.on_sale = @onSale");
                para.Add("@onSale", request.OnSale.Value);
            }

            int totalCount = 0;
            List<ConfigHotProductVo> data = new List<ConfigHotProductVo>();
            Task dataTask = OpenSlaveConnectionAsync(async conn =>
            {
                data = (await conn.QueryAsync<ConfigHotProductVo>(string.Format(sql, condition.ToString()), para))
                    ?.AsList() ?? new List<ConfigHotProductVo>();
            });

            Task countTask = OpenSlaveConnectionAsync(async conn =>
            {
                totalCount =
                    await conn.ExecuteScalarAsync<int>(string.Format(sqlCount, condition.ToString()), para);
            });
            await Task.WhenAll(dataTask, countTask);
            return new PagedEntity<ConfigHotProductVo>()
            {
                TotalItems = totalCount,
                Items = data
            };
        }
    }
}
