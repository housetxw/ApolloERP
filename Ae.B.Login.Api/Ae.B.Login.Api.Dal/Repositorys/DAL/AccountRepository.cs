using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.B.Login.Api.Common.Constant;
using Ae.B.Login.Api.Common.Extension;
using Ae.B.Login.Api.Core.Model;
using Ae.B.Login.Api.Core.Request;
using Ae.B.Login.Api.Core.Response;
using Ae.B.Login.Api.Dal.Model;
using Ae.B.Login.Api.Dal.Repositorys.IDAL;

namespace Ae.B.Login.Api.Dal.Repositorys.DAL
{
    public class AccountRepository : AbstractRepository<AccountDO>, IAccountRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<AccountRepository> logger;

        public AccountRepository(ApolloErpLogger<AccountRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("LoginAuthenticationSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("LoginAuthenticationSqlReadOnly");

            this.logger = logger;
        }

        // ---------------------------------- 登录操作相关方法 --------------------------------------
        public async Task<AccountKeyInfoWithPwdEntityResVO> GetAccountByNameAsync(AccountEntityReqByNameVO req)
        {
            var param = new DynamicParameters();
            var whereClause = "WHERE is_deleted = 0 ";
            if (req.Name.IsNotNullOrWhiteSpace())
            {
                param.Add("@Name", req.Name);
                whereClause += " AND Name = @Name ";
            }

            var res = new AccountKeyInfoWithPwdEntityResVO();
            var sql = @"SELECT id, name, password, password_iteration passwordIteration, password_salt passwordSalt, state, error_count errorCount, is_deleted isdeleted
                        FROM account " +
                      whereClause +
                      " LIMIT 1 ";
            await OpenSlaveConnectionAsync(async conn =>
            {
                res = await conn.QueryFirstOrDefaultAsync<AccountKeyInfoWithPwdEntityResVO>(sql, param);
            });

            return res;
        }

        public async Task<bool> UpdateAccountStateOrErrorCountByIdAsync(AccountStateOrErrorCountReqVO req)
        {
            long ud = 0;
            await OpenConnectionAsync(async conn =>
            {
                try
                {
                    var sql = @"UPDATE account SET error_count = @ErrorCount, update_by = @UpdateBy, update_time = NOW(3) 
                                WHERE id = @AccountId";

                    var mobile = req.Name.HidePhoneSensitiveInfo();
                    var param = new DynamicParameters();
                    param.Add("@AccountId", req.Id);
                    param.Add("@UpdateBy", string.Join(CommonConstant.SeparatorVertical, mobile, req.Id));
                    param.Add("@ErrorCount", req.ErrorCount);
                    if (req.ErrorCount >= 10)
                    {
                        sql = @"UPDATE account SET state = @State, error_count = @ErrorCount, update_by = @UpdateBy, update_time = NOW(3) 
                                WHERE id = @AccountId";
                        param.Add("@State", AccountState.Lock);
                    }
                    ud = await conn.ExecuteAsync(sql, param);
                }
                catch (Exception e)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateException), e);
                }
            });
            return ud >= 0;
        }

        public async Task<bool> ResetAccountStateAndErrorCountByIdOrNameAsync(AccountStateOrErrorCountReqVO req)
        {
            long ud = 0;
            await OpenConnectionAsync(async conn =>
            {
                try
                {
                    var param = new DynamicParameters();
                    var whereClause = "WHERE is_deleted = 0 ";
                    if (req.Id.IsNotNullOrWhiteSpace())
                    {
                        param.Add("@Id", req.Id);
                        whereClause += " AND id = @Id ";
                    }
                    if (req.Name.IsNotNullOrWhiteSpace())
                    {
                        param.Add("@Name", req.Name);
                        whereClause += " AND Name = @Name ";
                    }

                    var mobile = req.Name.HidePhoneSensitiveInfo();
                    param.Add("@UpdateBy", string.Join(CommonConstant.SeparatorVertical, mobile, req.Id) ?? "【ResetAccountStateAndErrorCountByIdOrNameAsync】");
                    param.Add("@ErrorCount", 0);
                    param.Add("@State", 0);

                    var sql = @"UPDATE account SET state = @State, error_count = @ErrorCount, update_by = @UpdateBy, update_time = NOW(3) "
                            + whereClause;
                    ud = await conn.ExecuteAsync(sql, param);
                }
                catch (Exception e)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateException), e);
                }
            });
            return ud >= 0;
        }

        public async Task<bool> UpdateAccountPasswordByNameAsync(UpdateAccountPwdVO req)
        {
            long ud = 0;
            await OpenConnectionAsync(async conn =>
            {
                try
                {
                    var mobile = req.Name.HidePhoneSensitiveInfo();

                    var param = new DynamicParameters();
                    param.Add("@Name", req.Name);
                    param.Add("@Password", req.Password);
                    param.Add("@PasswordIteration", req.PasswordIteration);
                    param.Add("@PasswordSalt", req.PasswordSalt);
                    param.Add("@UpdateBy", string.Join(CommonConstant.SeparatorVertical, mobile, req.Id) ?? "【UpdateAccountPasswordByNameAsync】");

                    var sql = @"UPDATE account SET password = @Password, password_iteration = @PasswordIteration, password_salt = @PasswordSalt,
                                state = 0, error_count = 0, password_update_time = NOW(3), update_by = @UpdateBy, update_time = NOW(3) 
                                WHERE is_deleted = 0 AND Name = @Name ";
                    ud = await conn.ExecuteAsync(sql, param);
                }
                catch (Exception e)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateException), e);
                }
            });
            return ud >= 0;
        }


    }
}
