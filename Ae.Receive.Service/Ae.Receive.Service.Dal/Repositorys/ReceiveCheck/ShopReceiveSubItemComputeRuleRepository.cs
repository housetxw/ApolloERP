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
    public class ShopReceiveSubItemComputeRuleRepository : AbstractRepository<ShopReceiveSubItemComputeRuleDo>, IShopReceiveSubItemComputeRuleRepository
    {
        public ShopReceiveSubItemComputeRuleRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 查询计算规则
        /// </summary>
        /// <param name="subItemId"></param>
        /// <returns></returns>
        public async Task<List<ShopReceiveSubItemComputeRuleDo>> GetSubItemComputeRule(long subItemId)
        {
            var para = new DynamicParameters();

            para.Add("@subItemId", subItemId);

            var result = await GetListAsync<ShopReceiveSubItemComputeRuleDo>("WHERE sub_item_id = @subItemId", para);

            return result?.ToList() ?? new List<ShopReceiveSubItemComputeRuleDo>();
        }
    }
}
