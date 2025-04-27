using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using DbType = ApolloErp.Data.DapperExtensions.DbType;
using Ae.Product.Service.Core.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Imp
{
    public class CategoryRepository : AbstractRepository<DimCategoryDO>, ICategoryRepository
    {
        public CategoryRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<int> AddCategory(DimCategoryDO categoryDo)
        {
            string sql = @"INSERT INTO dim_category (`shop_id`, `category_code`, `category_code_short`, `display_name`, `description`, `parent_id`, `category_Type`, `category_level`, `is_deleted`, `create_by`, `create_time`, `update_by`, `update_time` ) SELECT
                            * 
                            FROM
	                            (
                            SELECT
	                            @ShopId ShopId,
	                            @CategoryCode CategoryCode,
	                            @CategoryCodeShort CategoryCodeShort,
	                            @DisplayName DisplayName,
	                            @Description Description,
	                            @ParentId ParentId,
	                            @CategoryType CategoryType,
	                            @CategoryLevel CategoryLevel,
	                            @IsDeleted IsDeleted,
	                            @CreateBy CreateBy,
	                            @CreateTime CreateTime,
	                            @UpdateBy UpdateBy,
	                            @UpdateTime UpdateTime
	                            ) AS temp 
                            WHERE
	                            NOT EXISTS ( SELECT id FROM dim_category WHERE is_deleted = 0 and shop_id = @ShopId  AND (category_code = @CategoryCode OR category_code_short = @CategoryCodeShort AND parent_id = @ParentId) ) 
	                            LIMIT 1;";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, categoryDo);
            });
            return count;
        }

        /// <summary>
        /// 根据三级类目获取类目信息
        /// </summary>
        /// <param name="childCategoryId"></param>
        /// <returns></returns>
        public List<AllCategoryVo> GetAllCategoryById(long childCategoryId)
        {
            var paras = new
            {
                childCategoryId = childCategoryId
            };
            var sql = @"select distinct dc1.id MainCategoryId,dc1.category_code MainCategoryCode,dc1.display_name MainCategoryName,
			     dc2.id SecondCategoryId,dc2.category_code SecondCategoryCode,dc2.display_name SecondCategoryName,
	            dc3.id ChildCategoryId,dc3.category_code ChildCategoryCode,dc3.display_name ChildCategoryName,
                dc1.category_code_short MainCategoryShortCode, dc2.category_code_short SecondCategoryShortCode,
                dc3.category_code_short ChildCategoryShortCode
			 from dim_category  dc3 
             inner join dim_category dc2 on dc2.id=dc3.parent_id
             inner join dim_category dc1 on dc1.id=dc2.parent_id
             where  dc1.is_deleted=0 and dc2.is_deleted=0 and dc3.is_deleted=0 and dc3.id=@childCategoryId";
            IEnumerable<AllCategoryVo> categorys = new List<AllCategoryVo>();
            OpenConnection(conn =>
            {
                categorys = conn.Query<AllCategoryVo>(sql, paras);
            });
            return categorys.ToList();
        }

        public async Task<int> UpdateCategory(DimCategoryDO categoryDo)
        {
            string sql = @"UPDATE dim_category a
                            LEFT JOIN ( SELECT * FROM dim_category ) b ON a.id <> b.id 
                            AND b.is_deleted = 0 
                            AND ( b.category_code = @CategoryCode OR b.category_code_short = @CategoryCodeShort AND b.parent_id = @ParentId ) 
                            SET a.category_code = @CategoryCode,
                            a.category_code_short = @CategoryCodeShort,
                            a.display_name = @DisplayName,
                            a.description = @Description,
                            a.update_by = @UpdateBy,
                            a.update_time = NOW( 3 ) 
                            WHERE
	                            a.id = @Id 
	                            AND b.id IS NULL";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, categoryDo);
            });
            return count;
        }

        /// <summary>
        /// 获取所有产品类目
        /// </summary>
        /// <returns></returns>
        public async Task<List<DimCategoryDO>> GetAllCategory(long shopId)
        {
            string sqlWhere = "";
            if (shopId > 1)
            { sqlWhere = " and shop_id = " + shopId.ToString(); }

            var result = await GetListAsync("WHERE is_deleted = 0 " + sqlWhere);

            return result?.ToList() ?? new List<DimCategoryDO>();
        }
    }
}
