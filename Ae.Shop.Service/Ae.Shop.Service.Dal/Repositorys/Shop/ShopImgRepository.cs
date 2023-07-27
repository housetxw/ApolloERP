using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Dal.Model;
using Dapper;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using DynamicParameters = Dapper.DynamicParameters;
using System.Linq;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Model.OpeningGuide;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public class ShopImgRepository : AbstractRepository<ShopImgDO>, IShopImgRepository
    {
        public ShopImgRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="shopImg"></param>
        /// <returns></returns>
        public async Task<int> AddShopImgAsync(ShopImgDO shopImg)
        {
            int id = await InsertAsync(shopImg);
            return id;
        }

        /// <summary>
        /// 查询门店图片
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<ShopImgDO>> GetImgsByShopIdAsync(long shopId)
        {
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);

            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE shop_id = @shopId  and is_deleted=0 ");

            IEnumerable<ShopImgDO> result = new List<ShopImgDO>();
            result = await GetListAsync<ShopImgDO>(condition.ToString(), para);
            return result.ToList();
        }

        /// <summary>
        /// 删除门店照片
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<int> DeleteShopImg(long shopId, string updateBy)
        {
            var sql = @"UPDATE shop_img 
                        set is_deleted=1,
                        update_by = @update_by,
                        update_time = SYSDATE()
                        WHERE shop_id=@ShopId";

            var param = new DynamicParameters();
            param.Add("@ShopId", shopId);
            param.Add("@update_by", updateBy);
            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 更新门店图片信息For 开店指导
        /// </summary>
        /// <param name="shopBaseInfo"></param>
        /// <returns></returns>
        public async Task<int> UpdateShopImgInfoForOpeningGuide(List<int> shopImgsType, long shopId, string updateBy)
        {
            var sql = @"UPDATE shop_img 
                        set is_deleted=1,
                        update_by = @update_by,
                        update_time = SYSDATE()
                        WHERE shop_id=@ShopId and
                            type in @Types";

            var param = new DynamicParameters();
            param.Add("@ShopId", shopId);
            param.Add("@update_by", updateBy);
            param.Add("@Types", shopImgsType);
            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        public async Task<List<ShopImgDO>> GetShopImagesByType(List<long> shopId, int type)
        {
            var para = new DynamicParameters();
            para.Add("@shopId", shopId);
            para.Add("@type", type);
            var result = await GetListAsync<ShopImgDO>("WHERE shop_id IN @shopId AND `type` = 1", para);
            return result?.ToList() ?? new List<ShopImgDO>();
        }
    }
}
