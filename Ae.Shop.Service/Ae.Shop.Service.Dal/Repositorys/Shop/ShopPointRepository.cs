using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public class ShopPointRepository : AbstractRepository<ShopPointDo>, IShopPointRepository
    {
        public ShopPointRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 根据门店获取积分
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<ShopPointDo> GetShopPointByShopId(int shopId, bool readOnly = true)
        {

            var para = new DynamicParameters();
            para.Add("@shopId", shopId);

            var result = await GetListAsync<ShopPointDo>("WHERE `shop_id` = @shopId", para, !readOnly);

            return result?.FirstOrDefault();
        }

        /// <summary>
        /// 操作门店积分
        /// </summary>
        /// <param name="shopPointDo"></param>
        /// <returns></returns>
        public async Task<bool> OperatePointPoint(ShopPointDo shopPointDo)
        {
            if (shopPointDo.Id > 0)
            {
                string sql = @"UPDATE shop_point 
SET current_point = current_point + @pointValue,
update_by = @updatedBy,
update_time = NOW( ) 
WHERE
	id = @id;";
                var para = new DynamicParameters();
                para.Add("@id", shopPointDo.Id);
                para.Add("@pointValue", shopPointDo.CurrentPoint);
                para.Add("@updatedBy", shopPointDo.UpdateBy);
                int result = 0;
                await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para); });
                return result > 0;
            }
            else
            {
                return await InsertAsync(shopPointDo) > 0;
            }
        }
    }
}
