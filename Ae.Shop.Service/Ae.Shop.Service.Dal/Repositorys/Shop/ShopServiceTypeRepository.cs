using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public class ShopServiceTypeRepository : AbstractRepository<ShopServiceTypeDO>, IShopServiceTypeRepository
    {
        public ShopServiceTypeRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        public async Task<int> CreateShopServiceType(ShopServiceTypeDO request)
        {
           return await InsertAsync(request);
        }

        public async Task<int> DeleteShopServiceType(ShopServiceTypeDO request)
        {
            var sql = @"UPDATE shop_service_type 
                        SET is_deleted=@isDeleted,
                        update_by = @update_by,
                        update_time = SYSDATE( ) 
                        WHERE
	                        id = @id";
          
            var param = new DynamicParameters();
            param.Add("@id", request.Id);
            param.Add("@update_by", request.UpdateBy);
            param.Add("@isDeleted", request.IsDeleted);
            
            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 查询门店开通的服务项目
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceTypeDO>> GetShopServiceTypeListAsync(long shopId)
        {
            var para = new DynamicParameters();
            para.Add("@ShopId", shopId);

            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE shop_id = @ShopId");

            IEnumerable<ShopServiceTypeDO> result = new List<ShopServiceTypeDO>();
            result = await GetListAsync<ShopServiceTypeDO>(condition.ToString(), para);
            return result.ToList();
        }


        /// <summary>
        /// 查询门店开通的服务项目
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<ShopServiceTypeDO>> GetShopServiceTypeListByIdsAsync(List<long> shopIds)
        {
            var para = new DynamicParameters();
            para.Add("@ShopId", shopIds);

            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE shop_id in @ShopId");

            IEnumerable<ShopServiceTypeDO> result = new List<ShopServiceTypeDO>();
            result = await GetListAsync<ShopServiceTypeDO>(condition.ToString(), para);
            return result.ToList();
        }

        public async  Task<PagedEntity<ShopServiceTypeDO>> GetShopServiceTypePagesAsync(GetShopServiceTypePageRequest request)
        {
            PagedEntity<ShopServiceTypeDO> res = new PagedEntity<ShopServiceTypeDO>();
            var total = 0;

            var parames = new DynamicParameters();
            var condtions = new StringBuilder();

            parames.Add("@index", (request.PageIndex - 1) * request.PageSize);
            parames.Add("@size", request.PageSize);

            if (request.ShopId > 0) {
                condtions.Append(" and shop_id =@ShopId");
                parames.Add("@ShopId",request.ShopId);
            }

            var sqlCount = @"SELECT
	                        count(id) 
                        FROM
	                        shop_service_type 
                        WHERE
	                        1 =1" + condtions.ToString();

            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, parames);
            });

            var sql = @" SELECT
	                        id,
	                        shop_id ShopId,
	                        service_type ServiceType,
	                        is_deleted IsDeleted,
	                        create_by CreateBy,
	                        create_time CreateTime 
                        FROM
	                        shop_service_type 
                        WHERE
	                        1 =1 " + condtions + " order by id desc LIMIT @index, @size";

            IEnumerable<ShopServiceTypeDO> productDOs = null;
            await OpenConnectionAsync(async conn => productDOs = await conn.QueryAsync<ShopServiceTypeDO>(sql, parames));
            res.TotalItems = total;
            res.Items = productDOs.ToList();
            return res;

        }

        public async Task<List<ShopServiceTypeDO>> GetServiceType(List<long> shopId, string serviceType)
        {
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);
            para.Add("@serviceType", serviceType);

            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE shop_id IN @shopId AND service_type = @serviceType");

            IEnumerable<ShopServiceTypeDO> result = await GetListAsync<ShopServiceTypeDO>(condition.ToString(), para);
            return result?.ToList() ?? new List<ShopServiceTypeDO>();
        }
    }
}
