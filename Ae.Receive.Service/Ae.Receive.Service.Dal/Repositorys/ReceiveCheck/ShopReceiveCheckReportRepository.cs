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
    public class ShopReceiveCheckReportRepository : AbstractRepository<ShopReceiveCheckReportDo>, IShopReceiveCheckReportRepository
    {
        public ShopReceiveCheckReportRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 查询检查报告
        /// </summary>
        /// <param name="checkIds"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<List<ShopReceiveCheckReportDo>> GetReceiveCheckReporList(List<long> checkIds, int categoryId)
        {
            var para = new DynamicParameters();
            para.Add("@checkIds", checkIds);
            para.Add("@categoryId", categoryId);

            var result = await GetListAsync<ShopReceiveCheckReportDo>("WHERE check_id IN @checkIds AND category_id = @categoryId", para);

            return result?.ToList() ?? new List<ShopReceiveCheckReportDo>();
        }
    }
}
