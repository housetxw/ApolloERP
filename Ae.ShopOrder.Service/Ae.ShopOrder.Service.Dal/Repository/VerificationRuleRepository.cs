using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class VerificationRuleRepository : AbstractRepository<VerificationRuleDO>, IVerificationRuleRepository
    {
        public VerificationRuleRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderConfigSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderConfigSqlReadOnly");
        }

        public async Task<List<VerificationRuleDO>> GetRule(int shopType, long shopId, string pid)
        {
            string sql = @"SELECT
	                            `id` Id,
	                            `name` NAME,
	                            `is_valid` IsValid,
	                            `is_by_pid` IsByPid,
	                            `is_by_shop_type` IsByShopType,
	                            `shop_type` ShopType,
	                            `is_by_shop_id` IsByShopId,
	                            `use_rule_desc` UseRuleDesc,
	                            `valid_start_type` ValidStartType,
	                            `valid_end_type` ValidEndType,
	                            `valid_days` ValidDays,
	                            `earliest_use_date` EarliestUseDate,
	                            `latest_use_date` LatestUseDate,
	                            `is_deleted` IsDeleted,
	                            `create_by` CreateBy,
	                            `create_time` CreateTime,
	                            `update_by` UpdateBy,
	                            `update_time` UpdateTime 
                            FROM
	                            verification_rule AS r 
                            WHERE
	                            is_deleted = 0 
	                            AND is_valid = 1 
	                            AND (
	                            ( is_by_shop_type = 1 AND shop_type & @ShopType = @ShopType ) 
	                            OR (
	                            is_by_shop_id = 1 
	                            AND EXISTS ( SELECT id FROM verification_rule_shop_id WHERE is_deleted = 0 AND rule_id = r.id AND shop_id = @ShopId ) 
	                            ) 
	                            ) 
	                            AND (
	                            is_by_pid = 1 
	                            AND EXISTS ( SELECT id FROM verification_rule_pid WHERE is_deleted = 0 AND rule_id = r.id AND pid = @Pid ) 
	                            )";

            IEnumerable<VerificationRuleDO> list = null;
            await OpenSlaveConnectionAsync(async conn =>
            {
                list = await conn.QueryAsync<VerificationRuleDO>(sql, new
                {
                    ShopType = shopType,
                    ShopId = shopId,
                    Pid = pid
                });
            });
            return list?.ToList();
        }

        public async Task<PagedEntity<VerificationRuleDO>> GetVerificationRule(GetVerificationRuleRequest request)
        {
            PagedEntity<VerificationRuleDO> response = new PagedEntity<VerificationRuleDO>();

            var parameters = new DynamicParameters();
            var builder = new StringBuilder();
            builder.AppendLine("where is_deleted=0");

            if (!string.IsNullOrEmpty(request.Name))
            {
               
                    builder.AppendLine(" and name like CONCAT('%',@Name,'%')");
                    parameters.Add("@Name", request.Name);
                
            }

			if (!string.IsNullOrEmpty(request.Pid))
			{

				builder.AppendLine(" AND id in ( SELECT rule_id FROM verification_rule_pid WHERE is_deleted = 0  AND pid = @Pid )  ");
				parameters.Add("@Pid", request.Pid);

			}

			response = await GetListPagedAsync<VerificationRuleDO>(request.PageIndex, request.PageSize, builder.ToString(), "id desc",
                parameters, false);

            return response;
        }

		public async Task<List<VerificationRuleDO>> GetRuleForPackageCard(int shopType, long shopId, long ruleId)
		{
			string sql = @"SELECT
	                            `id` Id,
	                            `name` NAME,
	                            `is_valid` IsValid,
	                            `is_by_pid` IsByPid,
	                            `is_by_shop_type` IsByShopType,
	                            `shop_type` ShopType,
	                            `is_by_shop_id` IsByShopId,
	                            `use_rule_desc` UseRuleDesc,
	                            `valid_start_type` ValidStartType,
	                            `valid_end_type` ValidEndType,
	                            `valid_days` ValidDays,
	                            `earliest_use_date` EarliestUseDate,
	                            `latest_use_date` LatestUseDate,
	                            `is_deleted` IsDeleted,
	                            `create_by` CreateBy,
	                            `create_time` CreateTime,
	                            `update_by` UpdateBy,
	                            `update_time` UpdateTime 
                            FROM
	                            verification_rule AS r 
                            WHERE
	                            is_deleted = 0 
	                            AND is_valid = 1 
								AND id=@RuleId
	                            AND (
	                            ( is_by_shop_type = 1 AND shop_type & @ShopType = @ShopType ) 
	                            OR (
	                            is_by_shop_id = 1 
	                            AND EXISTS ( SELECT id FROM verification_rule_shop_id WHERE is_deleted = 0 AND rule_id = r.id AND shop_id = @ShopId ) 
	                            ) 
	                            )";

			IEnumerable<VerificationRuleDO> list = null;
			await OpenSlaveConnectionAsync(async conn =>
			{
				list = await conn.QueryAsync<VerificationRuleDO>(sql, new
				{
					ShopType = shopType,
					ShopId = shopId,
					RuleId = ruleId
				});
			});
			return list?.ToList();
		}

		public async Task<List<VerificationRuleDO>> GetRuleForPackage(string pid)
		{
			string sql = @"SELECT
	                            `id` Id,
	                            `name` NAME,
	                            `is_valid` IsValid,
	                            `is_by_pid` IsByPid,
	                            `is_by_shop_type` IsByShopType,
	                            `shop_type` ShopType,
	                            `is_by_shop_id` IsByShopId,
	                            `use_rule_desc` UseRuleDesc,
	                            `valid_start_type` ValidStartType,
	                            `valid_end_type` ValidEndType,
	                            `valid_days` ValidDays,
	                            `earliest_use_date` EarliestUseDate,
	                            `latest_use_date` LatestUseDate,
	                            `is_deleted` IsDeleted,
	                            `create_by` CreateBy,
	                            `create_time` CreateTime,
	                            `update_by` UpdateBy,
	                            `update_time` UpdateTime 
                            FROM
	                            verification_rule AS r 
                            WHERE
	                            is_deleted = 0 
	                            AND is_valid = 1 
	                            AND (
									 EXISTS ( SELECT id FROM verification_rule_pid WHERE is_deleted = 0 AND rule_id = r.id AND pid = @Pid ) 
	                            )";

			IEnumerable<VerificationRuleDO> list = null;
			await OpenSlaveConnectionAsync(async conn =>
			{
				list = await conn.QueryAsync<VerificationRuleDO>(sql, new
				{
					Pid = pid
				});
			});
			return list?.ToList();
		}
	}
}
