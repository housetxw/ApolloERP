using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Dal.Model;

namespace Ae.ConsumerOrder.Service.Dal.Repository
{
    public class VerificationRuleShopIdRepository : AbstractRepository<VerificationRuleShopIdDO>, IVerificationRuleShopIdRepository
    {
        public VerificationRuleShopIdRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderConfigSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderConfigSqlReadOnly");
        }

        public async Task<IEnumerable<VerificationRuleShopIdDO>> GetListByRuleId(long ruleId)
        {
            var list = await GetListAsync("where rule_id=@RuleId", new { RuleId = ruleId });
            return list;
        }
    }
}
