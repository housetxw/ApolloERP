using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.Receive.Service.Core.Request.Arrival;
using Ae.Receive.Service.Dal.Model;
using Ae.Receive.Service.Dal.Model.Arrival;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public class ShopArrivalVideoRepository : AbstractRepository<ShopArrivalVideoDO>, IShopArrivalVideoRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<ShopArrivalVideoRepository> logger;
        private readonly string className;

        public ShopArrivalVideoRepository(ApolloErpLogger<ShopArrivalVideoRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }
        public async Task<List<ShopArrivalVideoDO>> GetShopArrivalVideos(ShopArrivalVideoRequest request)
        {
            var para = new DynamicParameters();
            para.Add("@ShopArrivalId", request.ArrivalId);

            var result = await GetListAsync<ShopArrivalVideoDO>(" WHERE arrival_id = @ShopArrivalId", para);

            return result?.ToList() ?? new List<ShopArrivalVideoDO>();
        }

        public async Task<bool> UpdateShopArrivalVideo(DeleteShopArrivalVideoRequest request)
        {
            string sql = @"UPDATE shop_arrival_video 
                            SET is_deleted = 1,update_by=@UpdateBy,update_time=Now(3)
                            WHERE
                                arrival_id = @ShopArrivalId AND id=@Id";

            int count = 0;
            await OpenConnectionAsync(async conn =>
            {
                count = await conn.ExecuteAsync(sql, new
                {
                    ShopArrivalId = request.ArrivalId,
                    Id = request.Id,
                    UpdateBy = request.CreateBy
                });
            });
            return count > 0;
        }
    }
}
