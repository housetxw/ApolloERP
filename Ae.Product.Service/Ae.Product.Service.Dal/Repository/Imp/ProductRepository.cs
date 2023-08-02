using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Enums;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Imp
{
    public class ProductRepository : AbstractRepository<FctProductDO>, IProductRepository
    {
        public ProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 查询商品信息 By Ids
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public List<ProductAllInfoVo> GetProductsByProductCode(List<string> productCodes)
        {
            string sql =
                @" SELECT fc.id Id,fc.class_type ClassType,  fc.parent_id ParentId, fc.product_code ProductCode, fc.name Name,fc.advertisement AS Advertisement,fc.wholesale_price AS WholesalePrice,fc.return_cash AS ReturnCash,fc.settlement_price AS SettlementPrice,
                fc.display_name DisplayName,fc.brand Brand,fc.description Description,fc.tax_rate TaxRate,fc.create_by AS CreateBy,fc.create_time AS CreateTime,fc.update_by AS UpdateBy,fc.update_time AS UpdateTime,
                fc.category_id CategoryId,fc.standard_price StandardPrice, fc.sales_price SalesPrice,  
                fc.unit Unit,fc.length Length, fc.width Width, fc.height Height, fc.weight Weight,
                fc.color Color,fc.image1 Image1,fc.image2 Image2,fc.image3 Image3,fc.image4 Image4,fc.image5 Image5,
                fc.part_code PartCode,fc.part_no PartNo, fc.product_refer ProductRefer,fc.product_attribute ProductAttribute,
                fc.on_sale OnSale,fc.stockout Stockout,fc.is_show IsShow,fc.is_deleted IsDeleted,fc.is_storeoutside IsStoreoutside,
                fc.shop_number ShopNumber,fc.is_retail IsRetail,fc.is_insurance IsInsurance, fc.remark Remark,
                fc.score Score,fc.sales Sales,fc.favorable_rate FavorableRate,
                dc1.id MainCategoryId,dc1.category_code MainCategoryCode,dc1.display_name MainCategoryName,
	            dc2.id SecondCategoryId,dc2.category_code SecondCategoryCode,dc2.display_name SecondCategoryName,
	            dc3.id ChildCategoryId,dc3.category_code ChildCategoryCode,dc3.display_name ChildCategoryName,
                dc1.category_code_short MainCategoryShortCode, dc2.category_code_short SecondCategoryShortCode,
                dc3.category_code_short ChildCategoryShortCode
             from fct_product fc
             inner join dim_category  dc3 on dc3.id=fc.category_id
             inner join dim_category dc2 on dc2.id=dc3.parent_id
             inner join dim_category dc1 on dc1.id=dc2.parent_id
             where  dc1.is_deleted=0 and dc2.is_deleted=0 and dc3.is_deleted=0 and fc.product_code in @productCodes";
            var paras = new
            {
                productCodes = productCodes.ToArray()
            };
            IEnumerable<ProductAllInfoVo> result = new List<ProductAllInfoVo>();
            OpenConnection(conn =>
           {
               result = conn.Query<ProductAllInfoVo>(sql, paras);
           }
            );
            return result.ToList();
        }

        /// <summary>
        ///搜索子产品
        /// </summary>
        /// <param name="content"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedEntity<FctProductDO>> GetProductsByContent(string content, int pageIndex, int pageSize)
        {
            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            condition.Append(
                "WHERE class_type = 2 AND on_sale = 1 AND is_deleted = 0 AND (product_code = @productCode OR `name` LIKE @name OR display_name LIKE @name)");
            condition.Append(" and category_id <> 311 and category_id <> 312 "); //搜索子产品除去套餐卡
            para.Add("@productCode", content);
            para.Add("@name", $"%{content}%");

            var result =
                await GetListPagedAsync<FctProductDO>(pageIndex, pageSize, condition.ToString(), "`id` DESC", para);

            return result;
        }

        /// <summary>
        /// 查询商品信息 by 类目 和 类目 level  查询商品信息
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<ProductSearchInfoVo> GetProductsByCategory(CategoryProductRequest request, int level, out int totalCount)
        {
            string sqlQuery = @"select fc.id Id,fc.class_type ClassType,  fc.parent_id ParentId, fc.product_code ProductCode, fc.name Name,
                          fc.display_name DisplayName,fc.brand Brand,fc.description Description,fc.tax_rate TaxRate,
                          fc.category_id CategoryId,fc.standard_price StandardPrice, fc.sales_price SalesPrice,  
                          fc.unit Unit,fc.length Length, fc.width Width, fc.height Height, fc.weight Weight,
                          fc.color Color,fc.image1 Image1,fc.image2 Image2,fc.image3 Image3,fc.image4 Image4,fc.image5 Image5,
                          fc.part_code PartCode,fc.part_no PartNo, fc.product_refer ProductRefer,fc.product_attribute ProductAttribute,
                          fc.on_sale OnSale,fc.stockout Stockout,fc.is_show IsShow,fc.is_deleted IsDeleted,fc.is_storeoutside IsStoreoutside,
                          fc.shop_number ShopNumber,fc.is_retail IsRetail,fc.is_insurance IsInsurance, fc.remark Remark,
                          fc.score Score,fc.sales Sales
                           from fct_product fc ";
            var sqlCount = @"select count(fc.id) 'Rows' from fct_product fc ";

            var sqlWhere = "";
            //第三级类目
            if (level == 3)
            {
                sqlWhere += @" where fc.is_deleted=0 and  fc.class_type=2 and fc.category_id=@category_id";
            }
            else if (level == 2)
            {
                sqlWhere += @" inner join dim_category  dc3 on dc3.id=fc.category_id
			              where fc.is_deleted=0 and  fc.class_type=2   and dc3.parent_id=@category_id";
            }
            else if (level == 1)
            {
                sqlWhere += @" inner join dim_category  dc3 on dc3.id=fc.category_id
                          inner join dim_category dc2 on dc2.id=dc3.parent_id
			              where fc.is_deleted=0 and  fc.class_type=2 and  dc2.is_deleted=0 and dc3.is_deleted=0 and dc2.parent_id=@category_id";
            }
            if (request.IsOnSale.HasValue)
            {
                sqlWhere += " and fc.on_sale=@on_sale";
            }
            var sqlPager = $" limit {request.GetSkip()},{request.PageSize} ";
            sqlQuery = sqlQuery + sqlWhere + sqlPager;
            sqlCount = sqlCount + sqlWhere;
            var paras = new
            {
                category_id = request.CategoryId,
                on_sale = request.IsOnSale
            };
            IEnumerable<ProductSearchInfoVo> result = new List<ProductSearchInfoVo>();
            var count = 0;
            OpenConnection(conn =>
                  {
                      count = conn.ExecuteScalar<int>(sqlCount, paras);
                      if (count > 0)
                      {
                          result = conn.Query<ProductSearchInfoVo>(sqlQuery, paras);
                      }
                  }
             );
            totalCount = count;
            return result.ToList();
        }

        /// <summary>
        /// 查询所有的去重商品品牌信息
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public List<string> GetDistinctProductBrands(string conditon, object paras)
        {
            string sql = @"
                select   distinct fc.brand
                from fct_product fc
                inner join dim_category  dc3 on dc3.id=fc.category_id
                inner join dim_category dc2 on dc2.id=dc3.parent_id
                inner join dim_category dc1 on dc1.id=dc2.parent_id
                where dc1.is_deleted=0 and dc2.is_deleted=0 and dc3.is_deleted=0 ";
            IEnumerable<string> result = new List<string>();
            OpenConnection(conn =>
            {
                result = conn.Query<string>(sql + conditon, paras);
            }
            );
            return result.ToList();
        }


        /// <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public List<ProductAllInfoVo> SearchProduct(ProductSearchRequest request, string condition, object paras, out int totalCount)
        {
            string sqlQuery = @"
                SELECT fc.id Id,fc.class_type ClassType,  fc.parent_id ParentId, fc.product_code ProductCode, fc.name Name,
                fc.display_name DisplayName,fc.brand Brand,fc.description Description,fc.tax_rate TaxRate,
                fc.category_id CategoryId,fc.standard_price StandardPrice, fc.sales_price SalesPrice,  
				fc.`settlement_price` SettlementPrice,fc.`wholesale_price` WholesalePrice, fc.`return_cash`  ReturnCash,
                fc.unit Unit,fc.length Length, fc.width Width, fc.height Height, fc.weight Weight,
                fc.color Color,fc.image1 Image1,fc.image2 Image2,fc.image3 Image3,fc.image4 Image4,fc.image5 Image5,
                fc.part_code PartCode,fc.part_no PartNo, fc.product_refer ProductRefer,fc.product_attribute ProductAttribute,
                fc.on_sale OnSale,fc.stockout Stockout,fc.is_show IsShow,fc.is_deleted IsDeleted,fc.is_storeoutside IsStoreoutside,
                fc.shop_number ShopNumber,fc.is_retail IsRetail,fc.is_insurance IsInsurance, fc.remark Remark,
                fc.score Score,fc.sales Sales,fc.favorable_rate FavorableRate,fc.wholesale_price WholeSalePrice,
                dc1.id MainCategoryId,dc1.category_code MainCategoryCode,dc1.display_name MainCategoryName,
	            dc2.id SecondCategoryId,dc2.category_code SecondCategoryCode,dc2.display_name SecondCategoryName,
	            dc3.id ChildCategoryId,dc3.category_code ChildCategoryCode,dc3.display_name ChildCategoryName,
                dc1.category_code_short MainCategoryShortCode, dc2.category_code_short SecondCategoryShortCode,
                dc3.category_code_short ChildCategoryShortCode
                from fct_product fc
                inner join dim_category  dc3 on dc3.id=fc.category_id
                inner join dim_category dc2 on dc2.id=dc3.parent_id
                inner join dim_category dc1 on dc1.id=dc2.parent_id
                where  dc1.is_deleted=0 and dc2.is_deleted=0 and dc3.is_deleted=0 ";

            string sqlCount = @"select  count(fc.id) 'Rows'
                from fct_product fc
                inner join dim_category  dc3 on dc3.id=fc.category_id
                inner join dim_category dc2 on dc2.id=dc3.parent_id
                inner join dim_category dc1 on dc1.id=dc2.parent_id
                where  dc1.is_deleted=0 and dc2.is_deleted=0 and dc3.is_deleted=0 ";

            var sortCon = "";
            switch (request.Sort)
            {
                case ProductSearchSortType.PriceAsc:
                    sortCon = " order  by  fc.sales_price ";
                    break;
                case ProductSearchSortType.PriceDesc:
                    sortCon = " order  by  fc.sales_price desc ";
                    break;
                case ProductSearchSortType.ScoreAsc:
                    sortCon = " order  by  fc.score ";
                    break;
                case ProductSearchSortType.ScoreDesc:
                    sortCon = " order  by  fc.score desc ";
                    break;
                case ProductSearchSortType.SalesAsc:
                    sortCon = " order  by  fc.sales ";
                    break;
                case ProductSearchSortType.SalesDesc:
                    sortCon = " order  by fc.sales desc ";
                    break;
                default:
                    sortCon = " order  by fc.id";
                    break;
            }
            var sqlPager = $" limit {request.GetSkip()},{request.PageSize} ";
            var count = 0;
            IEnumerable<ProductAllInfoVo> result = new List<ProductAllInfoVo>();
            OpenConnection(conn =>
            {
                count = conn.ExecuteScalar<int>(sqlCount + condition, paras);
                if (count > 0)
                {
                    result = conn.Query<ProductAllInfoVo>(sqlQuery + condition + sortCon + sqlPager, paras);
                }
            }
            );
            totalCount = count;
            return result?.ToList();
        }

        /// <summary>
        /// 产品搜索-子产品搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedEntity<FctProductDO>> SearchProductCommon(SearchProductCommonCondition request)
        {
            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            condition.Append("WHERE class_type = 2 AND is_deleted = 0");

            if (!string.IsNullOrEmpty(request.Brand))
            {
                condition.Append(" AND `brand` = @brand");
                para.Add("@brand", request.Brand);
            }

            if (request.OnSale >= 0)
            {
                condition.Append(" AND on_sale = @onSale");
                para.Add("@onSale", request.OnSale);
            }

            if (request.IsRetail.HasValue)
            {
                condition.Append(" AND is_retail = @IsRetail");
                para.Add("@IsRetail", request.IsRetail);
            }

            if (request.IsShow.HasValue)
            {
                condition.Append(" AND is_show = @IsShow");
                para.Add("@IsShow", request.IsShow);
            }

            if (request.ProductAttribute != null && request.ProductAttribute.Any())
            {
                condition.Append(" AND product_attribute IN @productAttribute");
                para.Add("@productAttribute", request.ProductAttribute);
            }

            if (request.CategoryId != null && request.CategoryId.Any())
            {
                condition.Append(" AND category_id IN @categoryId");
                para.Add("@categoryId", request.CategoryId);
            }

            if (!string.IsNullOrEmpty(request.ProductName))
            {
                condition.Append(" AND (product_code = @productCode OR `name` LIKE @name OR display_name LIKE @name)");
                para.Add("@productCode", request.ProductName);
                para.Add("@name", $"%{request.ProductName}%");
            }

            var result = await GetListPagedAsync<FctProductDO>(request.PageIndex, request.PageSize,
                condition.ToString(), "`id` DESC", para);

            return result;
        }

        /// <summary>
        /// 前台类目搜索产品
        /// 子产品-上架-实物产品
        /// </summary>
        /// <param name="childCategoryId"></param>
        /// <param name="pid"></param>
        /// <param name="brand"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedEntity<FctProductDO>> GetProductByFrontCategoryConfig(List<int> childCategoryId,
            List<string> pid, List<string> brand, int pageIndex, int pageSize)
        {
            string orderBy = "`id` DESC";
            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            if (childCategoryId != null && childCategoryId.Any())
            {
                condition.Append("OR category_id IN @categoryIds ");
                para.Add("@categoryIds", childCategoryId);
            }

            if (pid != null && pid.Any())
            {
                condition.Append("OR product_code IN @productIds ");
                para.Add("@productIds", pid);
            }

            if (brand != null && brand.Any())
            {

                condition.Append("OR `brand` IN @brands ");
                para.Add("@brands", brand);
            }

            string conditionStr = string.Empty;
            if (condition.Length > 0)
            {
                conditionStr = $" AND ({condition.ToString().Substring(2)})";
            }

            var result = await GetListPagedAsync<FctProductDO>(pageIndex, pageSize,
                $"WHERE class_type = 2 AND on_sale = 1 AND product_attribute = 0{conditionStr}", orderBy, para);

            return result;
        }

        /// <summary>
        /// 根据Pid查询商品
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public async Task<FctProductDO> GetProductByPid(string pid)
        {
            var para = new DynamicParameters();
            para.Add("@productCode", pid);
            var result = await GetListAsync<FctProductDO>("WHERE product_code = @productCode", para);

            return result?.FirstOrDefault();
        }

        public async Task<List<FctProductDO>> GetProductByPidList(List<string> pidList)
        {
            var para = new DynamicParameters();
            para.Add("@productCodes", pidList);
            var result = await GetListAsync<FctProductDO>("WHERE product_code IN @productCodes", para);

            return result?.ToList() ?? new List<FctProductDO>();
        }

        /// <summary>
        /// 获取批发商品列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedEntity<FctProductDO>> GetWholesaleProducts(int pageIndex, int pageSize)
        {
            var result = await GetListPagedAsync<FctProductDO>(pageIndex, pageSize,
                "WHERE class_type = 2 AND product_attribute = 0 AND on_sale = 1 AND wholesale_price > 0",
                "category_id ASC");

            return result;
        }
    }
}
