using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public class ShopServiceCategoryRepository : AbstractRepository<ShopServiceCategoryDO>, IShopServiceCategoryRepository
    {
        public ShopServiceCategoryRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 根据分类ID查询门店开通的服务分类
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceCategoryDO>> ServiceCategoryByShopId(long shopId, long categoryId)
        {
            var conditon = "where shop_id=@ShopId AND category_id = @CategoryId";
            var paras = new
            {
                ShopId = shopId,
                CategoryId = categoryId
            };
            var serviceCategoryList = await GetListAsync(conditon.ToString(), paras);
            return serviceCategoryList.ToList();
        }
    }
}
