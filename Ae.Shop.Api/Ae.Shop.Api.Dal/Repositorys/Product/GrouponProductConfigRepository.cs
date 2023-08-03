using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Ae.Shop.Api.Dal.Model.Product.Extend;

namespace Ae.Shop.Api.Dal.Repositorys.Product
{
    public class GrouponProductConfigRepository : AbstractRepository<GrouponProductConfigDO>,
        IGrouponProductConfigRepository
    {
        public GrouponProductConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ProductSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductSqlReadOnly");
        }

        /// <summary>
        /// 获取美容团购产品
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public async Task<List<GrouponProductDto>> SearchGrouponProduct(string productName)
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
	a.on_sale = 1 
	AND a.is_deleted = 0{0};";
            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            if (!string.IsNullOrEmpty(productName))
            {
                condition.Append(" AND (a.product_code = @productCode OR a.`name` LIKE @name)");
                para.Add("@productCode", productName);
                para.Add("@name", $"%{productName}%");
            }

            List<GrouponProductDto> result = new List<GrouponProductDto>();

            await OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<GrouponProductDto>(string.Format(sql, condition.ToString()), para))
                    ?.ToList() ?? new List<GrouponProductDto>();
            });
            return result;
        }

        /// <summary>
        /// 获取美容团购产品详情
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async Task<GrouponProductDto> GetGrouponProductDetail(string pid)
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
	a.product_code = @productCode 
	AND a.on_sale = 1 
	AND a.is_deleted = 0;";
            var para = new DynamicParameters();
            para.Add("@productCode", pid);

            GrouponProductDto result = null;

            await OpenSlaveConnectionAsync(async conn =>
            {
                result = await conn.QueryFirstOrDefaultAsync<GrouponProductDto>(sql, para);
            });
            return result;
        }
    }
}
