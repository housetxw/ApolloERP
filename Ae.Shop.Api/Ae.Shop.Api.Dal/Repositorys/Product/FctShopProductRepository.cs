using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Product
{
    public class FctShopProductRepository : AbstractRepository<FctShopProductDO>, IFctShopProductRepository
    {
        public FctShopProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ProductSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductSqlReadOnly");
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
        /// 根据ProductCode查商品信息
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<FctShopProductDO> GetShopProductByProductCode(string productCode, long shopId)
        {
            var para = new DynamicParameters();

            para.Add("@productCode", productCode);
            para.Add("@shopId", shopId);

            var result = await GetListAsync<FctShopProductDO>("WHERE product_code = @productCode AND shop_id = @shopId", para);

            return result?.FirstOrDefault();
        }

        /// <summary>
        /// 根据商品名称或Code搜索商品
        /// </summary>
        /// <param name="searchContent"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<FctShopProductDO>> GetShopProductsByNameOrCode(string searchContent, long shopId)
        {
            var para = new DynamicParameters();

            para.Add("@shopId", shopId);
            para.Add("@productCode", searchContent);
            para.Add("@productName", "%" + searchContent + "%");

            var result = await GetListAsync<FctShopProductDO>("WHERE shop_id = @shopId AND (product_code = @productCode OR product_name LIKE @productName)", para);

            return result?.ToList() ?? new List<FctShopProductDO>();
        }

        /// <summary>
        /// 外采商品搜索
        /// </summary>
        /// <param name="searchContent"></param>
        /// <param name="specification"></param>
        /// <param name="shopId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<PagedEntity<FctShopProductDO>> SearchShopProductByCondition(string searchContent,
            string specification, List<long> shopIds, int pageIndex, int pageSize, int categoryId)
        {
            var para = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE shop_id in @shopIds");
            condition.Append(" AND is_storeoutside = 1 AND is_disabled = 0");
            para.Add("@shopIds", shopIds);

            if (!string.IsNullOrEmpty(searchContent))
            {
                condition.Append(
                    " AND (oe_number = @oeNumber OR product_code = @productCode OR product_name LIKE @productName)");
                para.Add("@oeNumber", searchContent);
                para.Add("@productCode", searchContent);
                para.Add("@productName", "%" + searchContent + "%");
            }

            if (!string.IsNullOrEmpty(specification))
            {
                condition.Append(" AND specification = @specification");
                para.Add("@specification", specification);
            }

            if (categoryId > 0)
            {
                condition.Append(" AND category_id = @categoryId");
                para.Add("@categoryId", categoryId);
            }

            var result =
                await GetListPagedAsync<FctShopProductDO>(pageIndex, pageSize, condition.ToString(), "`id` DESC", para);

            return result;
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="shopId"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        public async Task<int> DeleteShopProduct(string productCode, long shopId, string submitBy)
        {
            string sql = @"UPDATE fct_shop_product 
SET is_disabled = 1,
update_by = @updateBy,
update_time = NOW( ) 
WHERE
	product_code = @productCode 
	AND shop_id = @shopId
    AND is_disabled = 0 
	AND is_deleted = 0;";

            var para = new DynamicParameters();
            para.Add("@updateBy", submitBy);
            para.Add("@productCode", productCode);
            para.Add("@shopId", shopId);

            int result = 0;

            await OpenConnectionAsync(async conn =>
            {
                result = (await conn.ExecuteAsync(sql, para));

            });

            return result;
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

        /// <summary>
        /// 已存在的商品
        /// </summary>
        /// <param name="productCode"></param>
        /// <param name="productName"></param>
        /// <param name="oeNumber"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<FctShopProductDO>> GetExistProducts(string productCode, string productName, string oeNumber, long shopId)
        {
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE shop_id = @shopId AND is_disabled = 0 AND (product_name = @productName OR oe_number = @oeNumber)");
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
        /// pid查产品
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async Task<FctShopProductDO> GetShopProductByPid(string pid)
        {
            var para = new DynamicParameters();
            para.Add("@pid", pid);
            var result = await GetListAsync("WHERE product_code = @pid", para, true);
            return result?.FirstOrDefault();
        }
    }
}
