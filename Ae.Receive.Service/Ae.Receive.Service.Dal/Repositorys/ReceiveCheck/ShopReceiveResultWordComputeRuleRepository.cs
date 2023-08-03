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
    public class ShopReceiveResultWordComputeRuleRepository : AbstractRepository<ShopReceiveResultWordComputeRuleDo>, IShopReceiveResultWordComputeRuleRepository
    {
        public ShopReceiveResultWordComputeRuleRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 规则关联结果词
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopReceiveResultWordComputeRuleDo>> GetResultWordComputeRule()
        {
            var result = await GetListAsync<ShopReceiveResultWordComputeRuleDo>("");

            return result?.ToList() ?? new List<ShopReceiveResultWordComputeRuleDo>();
        }

        /// <summary>
        /// rule关联word
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShopCheckRuleWordDo>> GetResultWordAndRule()
        {
            string sql = @"SELECT
	A.sub_item_id AS SubItemId,
	A.suggestion AS Suggestion,
	A.id AS RuleId,
	B.result_word_id AS ResultWordId 
FROM
	shop_receive_sub_item_compute_rule A
	INNER JOIN shop_receive_result_word_compute_rule B ON ( A.id = B.rule_id AND B.is_deleted = 0 ) 
WHERE
	A.is_deleted = 0;";

            IEnumerable<ShopCheckRuleWordDo> result = new List<ShopCheckRuleWordDo>();

            await OpenConnectionAsync(async conn =>
            {
                result = await conn.QueryAsync<ShopCheckRuleWordDo>(sql);
            });

            return result?.ToList() ?? new List<ShopCheckRuleWordDo>();
        }
    }
}
