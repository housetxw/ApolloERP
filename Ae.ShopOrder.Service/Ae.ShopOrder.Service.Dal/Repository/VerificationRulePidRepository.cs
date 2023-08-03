using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class VerificationRulePidRepository : AbstractRepository<VerificationRulePidDO>, IVerificationRulePidRepository
    {
        public VerificationRulePidRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderConfigSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderConfigSqlReadOnly");
        }

        public async Task<bool> DeleteRulePIds(int ruleId, string updateBy)
        {
            int id = 0;
            StringBuilder condition = new StringBuilder();
            condition.Append("Update verification_rule_pid set is_deleted=1,update_by=@UpdateBy,update_time=Now(3) where rule_id=@RuleId");


            var parameters = new DynamicParameters();
            parameters.Add("@RuleId", ruleId);
            parameters.Add("@UpdateBy", updateBy);

            await OpenSlaveConnectionAsync(async conn => { id = await conn.ExecuteAsync(condition.ToString(), parameters); });
            return id > 0;
        }

        public async Task<IEnumerable<VerificationRulePidDO>> GetListByRuleId(long ruleId)
        {
            var list = await GetListAsync("where rule_id=@RuleId", new { RuleId = ruleId });
            return list;
        }
    }
}
