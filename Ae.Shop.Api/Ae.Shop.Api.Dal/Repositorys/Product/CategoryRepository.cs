using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Ae.Shop.Api.Dal.Repositorys.Product
{
    public class CategoryRepository : AbstractRepository<DimCategoryDO>, ICategoryRepository
    {
        public CategoryRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ProductSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductSqlReadOnly");
        }

        /// <summary>
        /// 获取三级类目
        /// </summary>
        /// <param name="childCategoryId"></param>
        /// <returns></returns>
        public async Task<AllCategoryDo> GetAllCategoryById(int childCategoryId)
        {
            var paras = new
            {
                childCategoryId = childCategoryId
            };
            var sql =
                @"select distinct dc1.id MainCategoryId,dc1.category_code MainCategoryCode,dc1.display_name MainCategoryName,
			     dc2.id SecondCategoryId,dc2.category_code SecondCategoryCode,dc2.display_name SecondCategoryName,
	            dc3.id ChildCategoryId,dc3.category_code ChildCategoryCode,dc3.display_name ChildCategoryName,
                dc1.category_code_short MainCategoryShortCode, dc2.category_code_short SecondCategoryShortCode,
                dc3.category_code_short ChildCategoryShortCode
			 from dim_category  dc3 
             inner join dim_category dc2 on dc2.id=dc3.parent_id
             inner join dim_category dc1 on dc1.id=dc2.parent_id
             where  dc1.is_deleted=0 and dc2.is_deleted=0 and dc3.is_deleted=0 and dc3.id=@childCategoryId";
            IEnumerable<AllCategoryDo> categorys = new List<AllCategoryDo>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                categorys = await conn.QueryAsync<AllCategoryDo>(sql, paras);
            });
            return categorys?.FirstOrDefault();
        }
    }
}
