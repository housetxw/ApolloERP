using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.ShopManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.ShopManage
{
    public class ShopGrouponProductRepository : AbstractRepository<ShopGrouponProductDO>, IShopGrouponProductRepository
    {
        public ShopGrouponProductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
        }

        /// <summary>
        /// 获取门店美容团购产品
        /// </summary>
        /// <param name="shopId"></param>]
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<List<ShopGrouponProductDO>> GetShopGrouponProduct(int shopId, sbyte? status)
        {
            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            condition.Append("WHERE shop_id = @shopId");
            para.Add("@shopId", shopId);
            if (status.HasValue)
            {
                condition.Append(" AND `status` = @status");
                para.Add("@status", status.Value);
            }

            var result = await GetListAsync<ShopGrouponProductDO>(condition.ToString(), para);

            return result?.AsList() ?? new List<ShopGrouponProductDO>();
        }

        /// <summary>
        /// 获取门店美容团购产品详情
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="pid"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<ShopGrouponProductDO> GetShopGrouponProductDetail(int shopId, string pid,
            bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);
            para.Add("@pid", pid);
            var result =
                await GetListAsync<ShopGrouponProductDO>("WHERE shop_id = @shopId AND service_id = @pid", para,
                    !readOnly);
            return result?.FirstOrDefault();
        }
    }
}
