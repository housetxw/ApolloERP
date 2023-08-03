using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public class ShopCheckResultImageRepository : AbstractRepository<ShopCheckResultImageDo>, IShopCheckResultImageRepository
    {
        public ShopCheckResultImageRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 根据checkId查询图片
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<List<ShopCheckResultImageDo>> GetCheckResultImageByCheckId(long checkId, int categoryId)
        {
            var para = new DynamicParameters();
            para.Add("@categoryId", categoryId);
            para.Add("@checkId", checkId);

            var result = await GetListAsync<ShopCheckResultImageDo>("WHERE check_id = @checkId AND category_id = @categoryId", para);

            return result?.ToList() ?? new List<ShopCheckResultImageDo>();
        }
    }
}
