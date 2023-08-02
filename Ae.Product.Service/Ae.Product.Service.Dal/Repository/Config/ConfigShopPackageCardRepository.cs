using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Core.Request.Config;
using Ae.Product.Service.Dal.Model.Config;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Config
{
    public class ConfigShopPackageCardRepository : AbstractRepository<ConfigShopPackageCardDO>,
        IConfigShopPackageCardRepository
    {
        public ConfigShopPackageCardRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<GetShopPackageCardProductPageListVo> GetShopCardDetail(GetShopCardDetailRequest request)
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
	b.type AS `Type` ,
	c.id AS Id,c.shop_ids ShopIds,
    a.description Description
FROM
	fct_product a
	INNER JOIN config_package_card_product b ON ( a.product_code = b.pid ) 
	inner join config_shop_package_card c 	on c.card_id=b.id	and c.is_deleted=0
WHERE
	a.is_deleted = 0 and c.id=@Id";


            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            para.Add("@Id", request.Id);
            GetShopPackageCardProductPageListVo data = new GetShopPackageCardProductPageListVo();
            Task dataTask = OpenSlaveConnectionAsync(async conn =>
            {
                data = await conn.QueryFirstAsync<GetShopPackageCardProductPageListVo>(sql, para);
            });
            await Task.WhenAll(dataTask);
            return data;
        }

        public async Task<PagedEntity<GetShopPackageCardProductPageListVo>> GetShopPackageCardProductPageList(GetShopPackageCardProductPageListRequest request)
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
	b.type AS `Type` ,
	c.id AS Id,c.shop_ids ShopIds

FROM
	fct_product a
	INNER JOIN config_package_card_product b ON ( a.product_code = b.pid ) 
	inner join config_shop_package_card c on c.card_id=b.id	and c.is_deleted=0
WHERE a.on_sale = 1 and
	a.is_deleted = 0 {0}
ORDER BY
	b.rank 
	LIMIT @startIndex,
	@pageSize;";

            string sqlCount = @"SELECT
	count(*)
FROM
	fct_product a
	INNER JOIN config_package_card_product b ON ( a.product_code = b.pid ) 
	inner join config_shop_package_card c 	on c.card_id=b.id	and c.is_deleted=0
WHERE a.on_sale = 1 and
	a.is_deleted = 0 {0};";

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

            if (request.ShopId > 0)
            {
                condition.Append(" AND (c.shop_ids  LIKE @ShopIds)");
                para.Add("@ShopIds", $"%{request.ShopId}%");
            }

            int totalCount = 0;
            List<GetShopPackageCardProductPageListVo> data = new List<GetShopPackageCardProductPageListVo>();
            Task dataTask = OpenSlaveConnectionAsync(async conn =>
            {
                data = (await conn.QueryAsync<GetShopPackageCardProductPageListVo>(string.Format(sql, condition.ToString()), para))
                    ?.AsList() ?? new List<GetShopPackageCardProductPageListVo>();
            });

            Task countTask = OpenSlaveConnectionAsync(async conn =>
            {
                totalCount =
                    await conn.ExecuteScalarAsync<int>(string.Format(sqlCount, condition.ToString()), para);
            });
            await Task.WhenAll(dataTask, countTask);
            return new PagedEntity<GetShopPackageCardProductPageListVo>()
            {
                TotalItems = totalCount,
                Items = data
            };
        }
    }
}
