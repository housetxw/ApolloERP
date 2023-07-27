using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System.Linq;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public class ShopServiceRepository : AbstractRepository<ShopServiceDO>, IShopServiceRepository
    {
        public ShopServiceRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 根据PID查询门店服务
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceDO>> GetShopServiceListWithPID(long shopId,List<string> productIds)
        {
            var para = new DynamicParameters();
            para.Add("@ShopId", shopId);
            para.Add("@ProductIds", productIds);
            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE is_deleted = 0 AND shop_id = @ShopId AND product_id IN @ProductIds ");
            IEnumerable<ShopServiceDO> ShopServiceList = new List<ShopServiceDO>();
            ShopServiceList = await GetListAsync<ShopServiceDO>(condition.ToString(), para, false);
            return ShopServiceList.AsList();
        }

        /// <summary>
        /// 获取门店所有上架的服务
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceDO>> GetAllOnLineServicesByShopId(long shopId)
        {
            var para = new DynamicParameters();
            para.Add("@ShopId", shopId);
            var result = await GetListAsync("WHERE shop_id = @ShopId AND `status` = 1", para);
            return result?.ToList() ?? new List<ShopServiceDO>();
        }

        /// <summary>
        /// 获取门店服务列表
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceInfoVO>> GetShopServiceListAsync(long shopId, long categoryId)
        {
            string sql = @"SELECT  ss.id,
                                ss.cost_price CostPrice ,
                                ss.create_time CreateTime,
                                ss.update_time UpdateTime,
                                ss.status,
                                ss.sale_price SalePrice,
                                bs.id AS BaseServiceId,
                                bs.product_id ProductId,
                                bs.name,
                                bs.default_sale_price DefaultSalePrice,
                                bs.default_cost_price DefaultCostPrice,
                                bs.service_remark ServiceRemark,
                                bs.sale_price_remark SalePriceRemark,
                                bs.default_sale_price_limit DefaultSalePriceLimit ,
                                bs.default_cost_price_limit DefaultCostPriceLimit,
                                bs.service_charge ServiceCharge
                        FROM    base_service bs 
                                LEFT JOIN shop_service ss ON ss.base_service_id = bs.id AND ss.shop_id=@ShopId AND ss.is_deleted = 0
                        WHERE   bs.category_id = @CategoryId AND bs.is_deleted = 0
                        ORDER BY ss.id DESC";
            var para = new DynamicParameters();
            para.Add("@ShopId", shopId);
            para.Add("@CategoryId", categoryId);
            
            IEnumerable<ShopServiceInfoVO> ShopServiceList = new List<ShopServiceInfoVO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                ShopServiceList = await conn.QueryAsync<ShopServiceInfoVO>(sql, para);
            });
            return ShopServiceList.AsList();
        }

        /// <summary>
        /// 门店添加服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AddShopServiceAsync(AddShopServiceRequest request) 
        {
            string sql = @"INSERT shop_service
                            ( shop_id ,
                              base_service_id ,
                              product_id ,
                              cost_price ,
                              sale_price ,
                              status ,
                              create_by ,
                              create_time ,
                              update_by ,
                              update_time
                            )

                            SELECT  @ShopId ,
                                    id,
                                    product_id,
                                    default_cost_price ,
                                    default_sale_price ,
                                    1 ,
                                    @CreateBy,
                                    now(),
                                    '',
                                    now()
                            FROM    base_service
                            WHERE   id = @BaseServiceId;";
            var para = new DynamicParameters();
            para.Add("@ShopId", request.ShopId);
            para.Add("@BaseServiceId", request.BaseServiceId);
            para.Add("@CreateBy", request.CreateBy);

            var num = 0;
            await OpenConnectionAsync(async conn =>
            {
                num = await conn.ExecuteAsync(sql, para);
            });
            return num > 0;
        }

        /// <summary>
        /// 查询门店开通的服务分类
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<BaseServiceDO>> GetShopServiceCategory(long shopId)
        {
            var sql = @"SELECT DISTINCT
    bs.id,	
    bs.name,
	bs.code 
FROM
	shop_service_category ssc
	LEFT JOIN base_service bs ON ssc.category_id = bs.id 
	AND bs.category_id = 0 
WHERE
	ssc.shop_id = @ShopId
	AND bs.is_deleted = 0";
            var param = new DynamicParameters();
            param.Add("@ShopId", shopId);

            IEnumerable<BaseServiceDO> ShopServiceList = new List<BaseServiceDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                ShopServiceList = await conn.QueryAsync<BaseServiceDO>(sql, param);
            });
            return ShopServiceList.ToList();
        }

        /// <summary>
        /// 查询门店开通的服务分类
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        public async Task<List<ShopCategoryServiceDO>> GetServiceCategoryWithShops(List<long> shopIds)
        {
            var sql = @"
SELECT DISTINCT
	ssc.shop_id ShopId ,
	bs.id CategoryId,
	bs.name Name,
	bs.code Code
FROM
	shop_service_category ssc
	LEFT JOIN base_service bs ON ssc.category_id = bs.id 
	AND bs.category_id = 0 
WHERE
	ssc.shop_id in @ShopId
	AND bs.is_deleted = 0";
            var param = new DynamicParameters();
            param.Add("@ShopId", shopIds);

            IEnumerable<ShopCategoryServiceDO> ShopServiceList = new List<ShopCategoryServiceDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                ShopServiceList = await conn.QueryAsync<ShopCategoryServiceDO>(sql, param);
            });
            return ShopServiceList.ToList();
        }
        /// <summary>
        /// 获取门店专修品牌
        /// </summary>
        /// <param name="shopIds"></param>
        /// <returns></returns>
        public async  Task<List<ShopServiceBrandDO>> GetShopServiceBrands(List<long> shopIds)
        {
            if (shopIds.Any()) {
                var shopIdStr = string.Join(',', shopIds);
                var sql = @"SELECT
	                        id Id,
	                        shop_id ShopId,
	                        brand Brand,
	                        brand_url BrandUrl,
	                        is_deleted IsDeleted,
	                        remark Remark 
                        FROM
	                        shop_service_brand 
                        WHERE
	                        shop_id IN ("+ shopIdStr + " ) and is_deleted=0";

                IEnumerable<ShopServiceBrandDO> brands = null;
                await OpenConnectionAsync(async conn => brands = await conn.QueryAsync<ShopServiceBrandDO>(sql));

                return brands?.ToList() ?? new List<ShopServiceBrandDO>();
            }
            return new List<ShopServiceBrandDO>();
        }

        public async Task<int> CreateShopServiceBrand(ShopServiceBrandDO request) {
            object obj = null;
            var sql = @"select count(1) from shop_service_brand where shop_id=@shopId and brand=@brand and is_deleted=0";
            var param = new DynamicParameters();
            param.Add("@shopId",request.ShopId);
            param.Add("@brand", request.Brand);
            await OpenConnectionAsync(async conn => obj = await conn.ExecuteScalarAsync(sql, param));
            if (obj != null && Convert.ToInt32(obj) > 0)
            {
                //有记录
                return -1;
            }
            else {
                return await InsertAsync<ShopServiceBrandDO>(request);
            }            
        }

        public async Task<int> DeleteShopServiceBrand(ShopServiceBrandDO request)
        {
            var sql = @"UPDATE shop_service_brand 
                        SET is_deleted = 1,
                        update_by = @updateBy,
                        update_time = SYSDATE( ) 
                        WHERE
	                        shop_id = @shop_id";
            var param = new DynamicParameters();
            param.Add("@shop_id", request.ShopId);
            param.Add("@updateBy", request.UpdateBy);
            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }


    }
}
