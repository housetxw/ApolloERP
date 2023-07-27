using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.AccountAuthority.Service.Common.Constant;
using Ae.AccountAuthority.Service.Common.Exceptions;
using Ae.AccountAuthority.Service.Common.Extension;
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Dal.Model;
using Ae.AccountAuthority.Service.Dal.Repositorys.IDAL;
using DbType = ApolloErp.Data.DapperExtensions.DbType;

namespace Ae.AccountAuthority.Service.Dal.Repositorys.DAL
{
    public class AccountRepository : AbstractRepository<AccountDO>, IAccountRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<AccountRepository> logger;
        private readonly string className;

        public AccountRepository(ApolloErpLogger<AccountRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("AccountAuthoritySql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("AccountAuthoritySqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }

        // ---------------------------------- 创建账号，更新密码和登录操作相关方法 --------------------------------------
        public async Task<string> CreateAccountAsync(AccountDO req)
        {
            string res;
            try
            {
                //ShopManage无对应的Api，系统默认操作人有可能过长
                if (req.UpdateBy.Length > CommonConstant.Fifty) req.UpdateBy = req.UpdateBy.Substring(0, 50);
                if (req.CreateBy.Length > CommonConstant.Fifty) req.CreateBy = req.CreateBy.Substring(0, 50);

                res = await InsertAsync<string>(req);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.DBInsertException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            return res;
        }

        public async Task<AccountKeyInfoWithPwdEntityResDTO> GetAccountByNameAsync(AccountEntityReqByNameDTO req)
        {
            var res = new AccountKeyInfoWithPwdEntityResDTO();

            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var param = new DynamicParameters();
                    var whereClause = "WHERE is_deleted = 0 ";
                    if (req.Name.IsNotNullOrWhiteSpace())
                    {
                        param.Add("@Name", req.Name);
                        whereClause += whereClause.Contains("WHERE") ? " AND Name = @Name " : " WHERE Name = @Name ";
                    }

                    var sql = @"SELECT id, name, password, password_iteration passwordIteration, password_salt passwordSalt, state, error_count errorCount, is_deleted isdeleted 
                                    FROM account "
                                  + whereClause +
                                  " LIMIT 1 ";
                    res = await conn.QueryFirstOrDefaultAsync<AccountKeyInfoWithPwdEntityResDTO>(sql, param);
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
            }
            finally
            {
                logger.Info($"DB: {className}.GetAccountByNameAsync 请求参数：{JsonConvert.SerializeObject(req)}");
                logger.Info($"DB: {className}.GetAccountByNameAsync 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        public async Task<bool> UpdateAccountPasswordByNameAsync(AccountDO req)
        {
            long ud = 0;
            await OpenConnectionAsync(async conn =>
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Name", req.Name);
                    param.Add("@Password", req.Password);
                    param.Add("@PasswordIteration", req.PasswordIteration);
                    param.Add("@PasswordSalt", req.PasswordSalt);
                    param.Add("@UpdateBy", req.UpdateBy);

                    var sql = @"UPDATE account SET password = @Password, password_iteration = @PasswordIteration, password_salt = @PasswordSalt,
                                state = 0, error_count = 0, password_update_time = NOW(3), update_by = @UpdateBy, update_time = NOW(3) 
                                WHERE is_deleted = 0 AND Name = @Name ";
                    ud = await conn.ExecuteAsync(sql, param);
                }
                catch (Exception e)
                {
                    var msg = $"{CommonConstant.ExceptionOccured}\n" +
                              $"{CommonConstant.ParameterReqDetail}\n{JsonConvert.SerializeObject(req)}";
                    logger.Error(msg, e);
                    throw new CustomException(CommonConstant.InternalDBError);
                }
            });
            return ud >= 0;
        }

        // ---------------------------------- API相关的方法 --------------------------------------
        public async Task<PagedEntity<AccountPageDTO>> GetAuthorityPage(AccountPageReqDTO req)
        {
            var res = new PagedEntity<AccountPageDTO>();
            IEnumerable<AccountPageDTO> enumerable = null;
            int total;

            try
            {
                var param = new DynamicParameters();
                param.Add("@index", (req.PageIndex - 1) * req.PageSize);
                param.Add("@size", req.PageSize);

                var whereClause = "";
                if (req.IsDeleted >= 0)
                {
                    param.Add("@isDeleted", req.IsDeleted);
                    whereClause += "WHERE acct.is_deleted = @isDeleted ";
                }
                else
                {
                    whereClause += "WHERE (acct.is_deleted = 0 OR acct.is_deleted = 1) ";
                }

                if (req.Name.IsNotNullOrWhiteSpace())
                {
                    param.Add("@Name", req.Name);
                    whereClause += "AND acct.Name = @Name ";
                }

                var sqlCount = @"SELECT COUNT(id) FROM account acct " + whereClause;
                await OpenSlaveConnectionAsync(async conn =>
                {
                    total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);

                    if (total > 0)
                    {
                        var sql = @"SELECT id, name, password, password_iteration passwordIteration, password_salt passwordSalt, state, error_count errorCount, 
                                    is_deleted isdeleted, create_by createby, create_time createtime, update_by updateby, update_time updatetime
                                    FROM account acct "
                                  + whereClause +
                                    @"ORDER BY acct.update_time DESC, acct.create_time DESC 
                                    LIMIT @index, @size";

                        enumerable = await conn.QueryAsync<AccountPageDTO>(sql, param);
                        res.Items = enumerable.ToList();
                        res.TotalItems = total;
                    }
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }

            return res;
        }

        public async Task<List<AccountDO>> GetAccountKeyInfoListById(AccountListReqDTO req)
        {
            return await GetAccountListWithAnyCondition(req);
        }

        public async Task<List<AccountDO>> GetAllAccountListAsync()
        {
            return await GetAccountListWithAnyCondition(null);
        }

        public async Task<AccountDO> GetAccountEntityById(AccountEntityReqDTO req)
        {
            return await GetAccountEntityWithAnyCondition(req);
        }

        public async Task<bool> UnlockAccountById(AccountUnlockReqByIdDTO req)
        {
            long del = 0;
            await OpenConnectionAsync(async conn =>
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", req.Id);
                    param.Add("@UpdateBy", req.UpdateBy ?? "【UnlockAccountById】");

                    var sql = @"UPDATE account SET state = 0, error_count = 0, update_by = @UpdateBy, update_time = NOW(3) 
                                WHERE is_deleted = 0 AND id = @Id ";
                    del = await conn.ExecuteAsync(sql, param);
                }
                catch (Exception e)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateException), e);
                    throw new CustomException(CommonConstant.InternalDBError);
                }
            });
            return del >= 0;
        }

        public async Task<bool> LockAccountById(AccountLockReqByIdDTO req)
        {
            long del = 0;
            await OpenConnectionAsync(async conn =>
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", req.Id.Distinct().ToList());
                    param.Add("@UpdateBy", req.UpdateBy ?? "【LockAccountById】");

                    var sql = @"UPDATE account SET state = 2, update_by = @UpdateBy, update_time = NOW(3) 
                                WHERE is_deleted = 0 AND id IN @Id ";
                    del = await conn.ExecuteAsync(sql, param);
                }
                catch (Exception e)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateException), e);
                    throw new CustomException(CommonConstant.InternalDBError);
                }
            });
            return del >= 0;
        }

        public async Task<bool> DeleteAccountById(AccountDeleteReqByIdDTO req)
        {
            long del = 0;
            await OpenConnectionAsync(async conn =>
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", req.Id);
                    param.Add("@UpdateBy", req.UpdateBy ?? "【DeleteAccountById】");

                    var sql = @"UPDATE account SET is_deleted = 1, update_by = @UpdateBy, update_time = NOW(3) 
                                WHERE is_deleted = 0 AND id = @Id ";
                    del = await conn.ExecuteAsync(sql, param);
                }
                catch (Exception e)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateException), e);
                    throw new CustomException(CommonConstant.InternalDBError);
                }
            });
            return del >= 0;
        }

        public async Task<bool> DeleteBatchAccountById(AccountBatchDeleteReqByIdDTO req)
        {
            long del = 0;
            await OpenConnectionAsync(async conn =>
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", req.Id.Distinct().ToList());
                    param.Add("@UpdateBy", req.UpdateBy ?? "【DeleteAccountById】");

                    var sql = @"UPDATE account SET is_deleted = 1, update_by = @UpdateBy, update_time = NOW(3) 
                                WHERE is_deleted = 0 AND id IN @Id ";
                    del = await conn.ExecuteAsync(sql, param);
                }
                catch (Exception e)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBDeleteException), e);
                    throw new CustomException(CommonConstant.InternalDBError);
                }
            });
            return del >= 0;
        }

        public async Task<bool> UpdateAccountPasswordById(AccountResetPasswordReqByIdIntlDTO req)
        {
            long ud = 0;
            await OpenConnectionAsync(async conn =>
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@Id", req.Id);
                    param.Add("@Password", req.Password);
                    param.Add("@PasswordIteration", req.PasswordIteration);
                    param.Add("@PasswordSalt", req.PasswordSalt);
                    param.Add("@UpdateBy", req.UpdateBy);

                    var sql = @"UPDATE account SET password = @Password, password_iteration = @PasswordIteration, password_salt = @PasswordSalt,
                                state = 0, error_count = 0, password_update_time = NOW(3), update_by = @UpdateBy, update_time = NOW(3) 
                                WHERE is_deleted = 0 AND id = @Id ";
                    ud = await conn.ExecuteAsync(sql, param);
                }
                catch (Exception e)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateException), e);
                    throw new CustomException(CommonConstant.InternalDBError);
                }
            });
            return ud >= 0;
        }

        // ---------------------------------- 私有方法 --------------------------------------
        private async Task<List<AccountDO>> GetAccountListWithAnyCondition(AccountListReqDTO req)
        {
            var param = new DynamicParameters();
            var whereClause = "";

            #region Where Clause
            if (null != req)
            {
                if (req.IsDeleted >= 0)
                {
                    param.Add("@isDeleted", req.IsDeleted);
                    whereClause += " WHERE is_deleted = @isDeleted ";
                }
                if (req.Id.Any())
                {
                    param.Add("@Id", req.Id);
                    whereClause += whereClause.Contains("WHERE")
                        ? " AND id IN @Id " : " WHERE id IN @Id ";
                }
                if (req.Name.IsNotNullOrWhiteSpace())
                {
                    param.Add("@Name", req.Name);
                    whereClause += whereClause.Contains("WHERE")
                        ? " AND name = @Name " : " WHERE name = @Name ";
                }
            }
            #endregion Where Clause

            IEnumerable<AccountDO> res = new List<AccountDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                try
                {
                    var sql = @"SELECT id, name, state, error_count ErrorCount,
                                is_deleted isdeleted, create_by createby, create_time createtime, update_by updateby, update_time updatetime
                                FROM account "
                              + whereClause +
                              " ORDER BY is_deleted ASC, update_time DESC, create_by DESC ";
                    res = await conn.QueryAsync<AccountDO>(sql, param);
                }
                catch (Exception e)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                    throw new CustomException(CommonConstant.InternalDBError);
                }
            });
            return res.ToList();
        }

        private async Task<AccountDO> GetAccountEntityWithAnyCondition(AccountEntityReqDTO req)
        {
            var param = new DynamicParameters();
            var whereClause = "";

            #region Where Clause
            if (null != req)
            {
                if (req.IsDeleted >= 0)
                {
                    param.Add("@isDeleted", req.IsDeleted);
                    whereClause += "WHERE is_deleted = @isDeleted ";
                }
                else
                {
                    whereClause += "WHERE (is_deleted = 0 OR is_deleted = 1) ";
                }
                if (req.Id.Any())
                {
                    param.Add("@Id", req.Id);
                    whereClause += " AND id = @Id ";
                }
                if (req.Name.IsNotNullOrWhiteSpace())
                {
                    param.Add("@Name", req.Name);
                    whereClause += " AND name = @Name ";
                }
            }
            #endregion Where Clause

            AccountDO res = new AccountDO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                try
                {
                    var sql = @"SELECT id, name, state, error_count ErrorCount,
                                is_deleted isdeleted, create_by createby, create_time createtime, update_by updateby, update_time updatetime
                                FROM account "
                              + whereClause +
                              " ORDER BY is_deleted ASC, update_time DESC, create_by DESC " +
                              " LIMIT 1 ";
                    res = await conn.QueryFirstOrDefaultAsync<AccountDO>(sql, param);
                }
                catch (Exception e)
                {
                    logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                    throw new CustomException(CommonConstant.InternalDBError);
                }
            });
            return res;
        }

    }
}
