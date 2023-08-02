using Dapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Web.WebApi;
using Ae.Product.Service.Core.Model.ShopProduct;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Dal.Model;
using Ae.Product.Service.Dal.Model.Condition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Imp
{
    public class FctShopProductRepository : AbstractRepository<FctShopProductDO>, IFctShopProductRepository
    {
        public FctShopProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 搜索门店商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<ShopProductVo> SearchShopProduct(ShopProductSearchRequest request, string condition, object paras, out int totalCount)
        {
            string sqlQuery = @"select fc.id Id,fc.shop_id ShopId,fc.category_id CategoryId,fc.product_code ProductCode ,fc.product_name ProductName,fc.display_name DisplayName,
                                fc.description Description,fc.standard_price StandardPrice,fc.sales_price SalesPrice,fc.sort_type SortType,
                                fc.is_top IsTop,fc.on_sale OnSale,fc.on_sale_time OnSaleTime,fc.icon Icon,fc.is_deleted IsDeleted, fc.remark Remark,
                                fc.apply_time ApplyTime,fc.audit_time AuditTime,fc.audit_status AuditStatus,fc.audit_user AuditUser,fc.audit_opinion AuditOpinion,fc.purchase_price PurchasePrice,
                dc1.id MainCategoryId,dc1.category_code MainCategoryCode,dc1.display_name MainCategoryName,
	            dc2.id SecondCategoryId,dc2.category_code SecondCategoryCode,dc2.display_name SecondCategoryName,
	            dc3.id ChildCategoryId,dc3.category_code ChildCategoryCode,dc3.display_name ChildCategoryName,
                dc1.category_code_short MainCategoryShortCode, dc2.category_code_short SecondCategoryShortCode,
                dc3.category_code_short ChildCategoryShortCode
                                from fct_shop_product  fc
                inner join dim_category  dc3 on dc3.id=fc.category_id and dc3.is_deleted=0 
                inner join dim_category dc2 on dc2.id=dc3.parent_id and dc2.is_deleted=0
                inner join dim_category dc1 on dc1.id=dc2.parent_id and  dc1.is_deleted=0 ";
            string sqlCount = @"select count(fc.id) 'Rows'
                                from fct_shop_product  fc
                inner join dim_category  dc3 on dc3.id=fc.category_id and dc3.is_deleted=0 
                inner join dim_category dc2 on dc2.id=dc3.parent_id and dc2.is_deleted=0
                inner join dim_category dc1 on dc1.id=dc2.parent_id and  dc1.is_deleted=0";

            var sortCon = " order  by fc.id desc ";
            var sqlPager = $" limit {request.GetSkip()},{request.PageSize} ";
            var count = 0;
            IEnumerable<ShopProductVo> result = new List<ShopProductVo>();
            OpenConnection(conn =>
            {
                count = conn.ExecuteScalar<int>(sqlCount + condition, paras);
                if (count > 0)
                {
                    result = conn.Query<ShopProductVo>(sqlQuery + condition + sortCon + sqlPager, paras);
                }
            }
            );
            totalCount = count;
            return result?.ToList();
        }

        /// <summary>
        /// 搜索外采产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<FctShopProductDO>> SearchShopProduct(SearchShopProductCondition request)
        {
            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            condition.Append("WHERE shop_id = @shopId");
            para.Add("@shopId", request.ShopId);
            if (request.CategoryId != null && request.CategoryId.Any())
            {
                condition.Append(" AND category_id IN @categoryId");
                para.Add("@categoryId", request.CategoryId);
            }

            if (request.IsOnSale.HasValue)
            {
                condition.Append(" AND on_sale = @onSale");
                para.Add("@onSale", request.IsOnSale.Value);
            }

            if (request.IsStoreoutside.HasValue)
            {
                condition.Append(" AND is_storeoutside = @isStoreoutside");
                para.Add("@isStoreoutside", request.IsStoreoutside.Value);
            }

            if (request.IsDisabled.HasValue)
            {
                condition.Append(" AND is_disabled = @isDisabled");
                para.Add("@isDisabled", request.IsDisabled.Value);
            }

            if (!string.IsNullOrEmpty(request.Key))
            {
                condition.Append(
                    " AND ( product_name like @key or product_code = @productCode or display_name like @key ) ");
                para.Add("@key", $"%{request.Key}%");
                para.Add("@productCode", request.Key);
            }

            var result = await GetListPagedAsync(request.PageIndex, request.PageSize, condition.ToString(), "`id` DESC",
                para);

            return result;
        }

        /// <summary>
        /// 搜索外采产品V2
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<FctShopProductDO>> SearchShopProductV2(SearchShopProductConditionV2 request)
        {
            string sql = @"SELECT
	`id` AS `Id`,
	`shop_id` AS `ShopId`,
	`category_id` AS `CategoryId`,
	`product_code` AS `ProductCode`,
	`product_name` AS `ProductName`,
	`display_name` AS `DisplayName`,
	`description` AS `Description`,
	`standard_price` AS `StandardPrice`,
	`sales_price` AS `SalesPrice`,
	`discount_rate` AS `DiscountRate`,
	`sort_type` AS `SortType`,
	`is_top` AS `IsTop`,
	`on_sale` AS `OnSale`,
	`on_sale_time` AS `OnSaleTime`,
	`icon` AS `Icon`,
	`remark` AS `Remark`,
	`is_deleted` AS `IsDeleted`,
	`create_by` AS `CreateBy`,
	`create_time` AS `CreateTime`,
	`update_by` AS `UpdateBy`,
	`update_time` AS `UpdateTime`,
	`ref_pid` AS `RefPid`,
	`detail` AS `Detail`,
	`image_url` AS `ImageUrl`,
	`apply_time` AS `ApplyTime`,
	`audit_time` AS `AuditTime`,
	`audit_status` AS `AuditStatus`,
	`audit_user` AS `AuditUser`,
	`audit_opinion` AS `AuditOpinion`,
	`is_storeoutside` AS `IsStoreoutside`,
	`specification` AS `Specification`,
	`unit` AS `Unit`,
	`oe_number` AS `OeNumber`,
	`purchase_price` AS `PurchasePrice`,
	`is_disabled` AS `IsDisabled` 
FROM
	`fct_shop_product` 
WHERE
	shop_id = @shopId{0}
ORDER BY
	`id` DESC 
	LIMIT @startIndex,
	@pageSize;";

            string sqlCount = @"SELECT
	COUNT( 1 ) 
FROM
	`fct_shop_product` 
WHERE
	shop_id = @shopId{0};";

            var para = new DynamicParameters();
            para.Add("@shopId", request.ShopId);
            para.Add("@startIndex", request.StartIndex);
            para.Add("@pageSize", request.PageSize);
            StringBuilder condition = new StringBuilder();
            if (request.CategoryId != null && request.CategoryId.Any())
            {
                condition.Append(" AND category_id IN @categoryId");
                para.Add("@categoryId", request.CategoryId);
            }

            if (request.IsOnSale.HasValue)
            {
                condition.Append(" AND on_sale = @onSale");
                para.Add("@onSale", request.IsOnSale.Value);
            }

            if (request.IsStoreoutside.HasValue)
            {
                condition.Append(" AND is_storeoutside = @isStoreoutside");
                para.Add("@isStoreoutside", request.IsStoreoutside.Value);
            }

            if (request.IsDisabled.HasValue)
            {
                condition.Append(" AND is_disabled = @isDisabled");
                para.Add("@isDisabled", request.IsDisabled.Value);
            }

            if (!string.IsNullOrEmpty(request.Key))
            {
                condition.Append(
                    " AND ( product_name like @key or product_code = @productCode or display_name like @key ) ");
                para.Add("@key", $"%{request.Key}%");
                para.Add("@productCode", request.Key);
            }

            List<FctShopProductDO> result = new List<FctShopProductDO>();
            int totalCount = 0;
            List<Task> tasks = new List<Task>();
            tasks.Add(OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<FctShopProductDO>(string.Format(sql, condition.ToString()), para))
                    ?.AsList() ?? new List<FctShopProductDO>();
            }));
            tasks.Add(OpenSlaveConnectionAsync(async conn =>
            {
                totalCount =
                    await conn.ExecuteScalarAsync<int>(string.Format(sqlCount, condition.ToString()), para);
            }));

            await Task.WhenAll(tasks.ToArray());

            return new PagedEntity<FctShopProductDO>()
            {
                TotalItems = totalCount,
                Items = result
            };
        }

        /// <summary>
        /// 根据ProductCodes查商品信息
        /// </summary>
        /// <param name="productCodes"></param>
        /// <returns></returns>
        public async Task<List<FctShopProductDO>> GetShopProductByProductCodes(List<string> productCodes)
        {
            var para = new DynamicParameters();
            para.Add("@productCodes", productCodes);

            var result = await GetListAsync<FctShopProductDO>("WHERE product_code IN @productCodes", para);

            return result?.AsList() ?? new List<FctShopProductDO>();
        }

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<FctShopProductDO> GetShopProductByPid(string pid, int shopId)
        {
            var para = new DynamicParameters();
            para.Add("@productCode", pid);
            para.Add("@shopId", shopId);
            var result =
                await GetListAsync<FctShopProductDO>("WHERE product_code = @productCode AND shop_id = @shopId", para);

            return result?.FirstOrDefault();
        }

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async Task<FctShopProductDO> GetShopProductByPid(string pid)
        {
            var para = new DynamicParameters();
            para.Add("@productCode", pid);
            var result =
                await GetListAsync<FctShopProductDO>("WHERE product_code = @productCode", para);

            return result?.FirstOrDefault();
        }

        /// <summary>
        /// 服务大类 商品列表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<FctShopProductDO>> GetShopProductByCategoryId(int categoryId, int shopId)
        {
            var para = new DynamicParameters();
            para.Add("@categoryId", categoryId);
            para.Add("@shopId", shopId);

            var result =
                await GetListAsync<FctShopProductDO>(
                    "WHERE shop_id = @shopId AND category_id = @categoryId AND on_sale = 1 AND is_storeoutside = 0",
                    para);

            return result?.ToList() ?? new List<FctShopProductDO>();
        }

        /// <summary>
        /// 已存在的商品
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="productName"></param>
        /// <param name="oeNumber"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<FctShopProductDO>> GetExistProducts(string productCode, string productName,
            string oeNumber, int shopId)
        {
            StringBuilder condition = new StringBuilder();
            condition.Append(
                "WHERE shop_id = @shopId AND is_disabled = 0 AND (product_name = @productName OR oe_number = @oeNumber)");
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);
            para.Add("@productName", productName);
            para.Add("@oeNumber", oeNumber);
            if (!string.IsNullOrEmpty(productCode))
            {
                condition.Append(" AND product_code != @productCode");
                para.Add("@productCode", productCode);
            }

            var result = await GetListAsync<FctShopProductDO>(condition.ToString(), para);

            return result?.ToList() ?? new List<FctShopProductDO>();
        }

        /// <summary>
        /// 更新商品
        /// </summary>
        /// <param name="fctShopProductDo"></param>
        /// <returns></returns>
        public async Task<int> UpdateShopProduct(FctShopProductDO fctShopProductDo)
        {
            string sql = @"UPDATE fct_shop_product 
SET oe_number = @oeNumber,
product_name = @productName,
display_name = @productName,
specification = @specification,
unit = @unit,
purchase_price = @purchasePrice,
sales_price = @salesPrice,
update_by = @updateBy,
update_time = NOW( ) 
WHERE
	product_code = @productCode 
	AND shop_id = @shopId 
	AND is_disabled = 0 
	AND is_deleted = 0;";

            var para = new DynamicParameters();
            para.Add("@oeNumber", fctShopProductDo.OeNumber);
            para.Add("@productName", fctShopProductDo.ProductName);
            para.Add("@specification", fctShopProductDo.Specification);
            para.Add("@unit", fctShopProductDo.Unit);
            para.Add("@purchasePrice", fctShopProductDo.PurchasePrice);
            para.Add("@salesPrice", fctShopProductDo.SalesPrice);
            para.Add("@updateBy", fctShopProductDo.UpdateBy);
            para.Add("@productCode", fctShopProductDo.ProductCode);
            para.Add("@shopId", fctShopProductDo.ShopId);

            int result = 0;

            await OpenConnectionAsync(async conn =>
            {
                result = (await conn.ExecuteAsync(sql, para));

            });

            return result;
        }

        public async Task<int> UpdateProductPriceInfo(FctShopProductDO fctShopProductDo)
        {
            string sql = @"UPDATE fct_shop_product 
                            SET sales_price = @salesPrice,
                            update_by = @updateBy,
                            update_time = NOW( ) 
                            WHERE
	                            product_code = @productCode 
	                            AND shop_id = @shopId 
	                            AND is_disabled = 0 
	                            AND is_deleted = 0;";

            var para = new DynamicParameters();
            para.Add("@salesPrice", fctShopProductDo.SalesPrice);
            para.Add("@updateBy", fctShopProductDo.UpdateBy);
            para.Add("@productCode", fctShopProductDo.ProductCode);
            para.Add("@shopId", fctShopProductDo.ShopId);

            int result = 0;

            await OpenConnectionAsync(async conn =>
            {
                result = (await conn.ExecuteAsync(sql, para));

            });

            return result;
        }
    }
}
