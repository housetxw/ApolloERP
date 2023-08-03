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
    public class ShopReceiveCheckResultWordRepository : AbstractRepository<ShopReceiveCheckResultWordDo>, IShopReceiveCheckResultWordRepository
    {
        public ShopReceiveCheckResultWordRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 检查结果 结果词
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<List<ShopReceiveCheckResultWordDo>> GetShopReceiveCheckResultWord(long checkId, int categoryId)
        {
            var para = new DynamicParameters();

            para.Add("@checkId", checkId);
            para.Add("@categoryId", categoryId);

            var result = await GetListAsync<ShopReceiveCheckResultWordDo>("WHERE `check_id` = @checkId AND category_id = @categoryId", para);

            return result?.ToList() ?? new List<ShopReceiveCheckResultWordDo>();
        }
    }
}
