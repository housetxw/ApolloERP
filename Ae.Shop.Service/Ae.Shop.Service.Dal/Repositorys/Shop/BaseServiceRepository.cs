using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public class BaseServiceRepository : AbstractRepository<BaseServiceDO>, IBaseServiceRepository
    {
        public BaseServiceRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }
        /// <summary>
        /// 根据名称/code查询基础服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseServiceDO> GetBaseServiceByNameAsync(string name,string code)
        {
            var sql = "select * from base_service where category_id=0 AND is_deleted = 0 AND name =@Name or code = @Code ";
            var para = new DynamicParameters();
            para.Add("@Name", name);
            para.Add("@Code", code);
            var baseServiceInfo = new BaseServiceDO();
            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    baseServiceInfo = (await conn.QueryAsync<BaseServiceDO>(sql, para)).FirstOrDefault();
                });
            }
            catch (Exception ex)
            {

                throw;
            }
            
            return baseServiceInfo;
        }

        /// <summary>
        /// 获取基本服务
        /// </summary>
        /// <returns></returns>
        public async Task<List<BaseServiceDO>> GetBaseServiceListAsync(GetBaseServiceListRequest request)
        {
            var conditon = "where category_id=@CategoryId AND is_deleted = 0";
            var paras = new
            {
                CategoryId = request.CategoryId
            };
            var result = await GetListAsync(conditon, paras);
            return result.ToList();
        }
        /// <summary>
        /// 查询门店服务详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ShopServiceInfoVO> GetShopServiceInfoAsync(GetShopServiceInfoRequest request) 
        {
            string sql = @"SELECT
	s.id,
	s.shop_id ShopId,
	s.base_service_id BaseServiceId,
    s.status,
	b.NAME,
	s.product_id ProductId,
	s.cost_price CostPrice,
	s.sale_price SalePrice,
	b.default_sale_price_limit DefaultSalePriceLimit,
	b.default_cost_price_limit DefaultCostPriceLimit
FROM
	base_service b
	LEFT JOIN shop_service s ON b.id = s.base_service_id 
WHERE
    s.id = @Id
	AND s.is_deleted = 0 
	AND b.is_deleted = 0";

            var para = new DynamicParameters();
            para.Add("@Id", request.Id);
            var shopServiceInfo = new ShopServiceInfoVO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                shopServiceInfo = await conn.QueryFirstAsync<ShopServiceInfoVO>(sql, para);
            });
            return shopServiceInfo;
        }
    }
}
