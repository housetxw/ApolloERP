using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Dal.Model.Condition;
using Dapper;

namespace Ae.Product.Service.Dal.Repository.Config
{
    public class GrouponProductConfigRepository: AbstractRepository<GrouponProductConfigDO>,
        IGrouponProductConfigRepository
    {
        public GrouponProductConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<PagedEntity<GrouponProductVo>> GetGrouponProductPageList(
            GrouponProductPageListCondition request)
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
	b.`id` AS GrouponId,
	b.min_price AS MinPrice,
	b.max_price AS MaxPrice 
FROM
	fct_product a
	INNER JOIN groupon_product_config b ON ( a.product_code = b.pid AND b.is_deleted = 0 ) 
WHERE
	a.is_deleted = 0{0}
ORDER BY
	b.`id` DESC 
	LIMIT @startIndex,
	@pageSize;";

            string sqlCount = @"SELECT
	COUNT( * ) 
FROM
	fct_product a
	INNER JOIN groupon_product_config b ON ( a.product_code = b.pid AND b.is_deleted = 0 ) 
WHERE
	a.is_deleted = 0{0};";

            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            para.Add("@startIndex", (request.PageIndex - 1) * request.PageSize);
            para.Add("@pageSize", request.PageSize);
            if (request.OnSale.HasValue)
            {
                condition.Append(" AND a.on_sale = @onSale");
                para.Add("@onSale", request.OnSale.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Key))
            {
                condition.Append(" AND (a.product_code = @productCode OR a.`name` LIKE @name)");
                para.Add("@productCode", request.Key);
                para.Add("@name", $"%{request.Key}%");
            }

            int totalCount = 0;
            List<GrouponProductVo> data = new List<GrouponProductVo>();
            Task dataTask = OpenSlaveConnectionAsync(async conn =>
            {
                data = (await conn.QueryAsync<GrouponProductVo>(string.Format(sql, condition.ToString()), para))
                    ?.AsList() ?? new List<GrouponProductVo>();
            });

            Task countTask = OpenSlaveConnectionAsync(async conn =>
            {
                totalCount =
                    await conn.ExecuteScalarAsync<int>(string.Format(sqlCount, condition.ToString()), para);
            });
            await Task.WhenAll(dataTask, countTask);
            return new PagedEntity<GrouponProductVo>()
            {
                TotalItems = totalCount,
                Items = data
            };
        }

        /// <summary>
        /// 根据pid查询团购商品
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<GrouponProductConfigDO> GetGrouponProductByPid(string pid, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@pid", pid);
            var result = await GetListAsync("WHERE pid = @pid", para, !readOnly);
            return result?.FirstOrDefault();
        }

        /// <summary>
        /// 根据pidList查询团购商品
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<List<GrouponProductConfigDO>> GetGrouponProductByPidList(List<string> pidList,
            bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@pidList", pidList);
            var result = await GetListAsync("WHERE pid IN @pidList", para, !readOnly);
            return result?.AsList() ?? new List<GrouponProductConfigDO>();
        }
    }
}
