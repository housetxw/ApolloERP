using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.AccountAuthority.Service.Common.Constant;
using Ae.AccountAuthority.Service.Common.Exceptions;
using Ae.AccountAuthority.Service.Common.Extension;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Dal.Model;
using Ae.AccountAuthority.Service.Dal.Repositorys.IDAL;
using DbType = ApolloErp.Data.DapperExtensions.DbType;

namespace Ae.AccountAuthority.Service.Dal.Repositorys.DAL
{
    public class RoleAuthorityRepository : AbstractRepository<RoleAuthorityDO>, IRoleAuthorityRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<RoleAuthorityRepository> logger;

        public RoleAuthorityRepository(ApolloErpLogger<RoleAuthorityRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("AccountAuthoritySql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("AccountAuthoritySqlReadOnly");

            this.logger = logger;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<bool> SaveRoleAuthority(RoleAuthorityReqDO req)
        {
            if (null == req)
            {
                logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                return false;
            }
            if (req.RoleId <= 0)
            { return false; }

            var updateTime = DateTime.Now;
            try
            {
                var checkRoleAuthorityList = (await GetRoleAuthorityListByRoleId(new RoleAuthorityListReqByRoleIdDTO
                {
                    RoleId = req.RoleId,
                    IsDeleted = -1
                }));

                if (checkRoleAuthorityList.Any())
                {
                    foreach (var roleAuthority in checkRoleAuthorityList)
                    {
                        roleAuthority.IsDeleted = true;
                        roleAuthority.UpdateBy = req.UpdateBy;
                        roleAuthority.UpdateTime = updateTime;
                    }
                }
                if (req.AddList.Any())
                {
                    foreach (var item in req.AddList)
                    {
                        var roleAuthority = checkRoleAuthorityList.Where(_ => _.AuthorityId == item.AuthorityId)?.FirstOrDefault();
                        if (roleAuthority == null)
                        {
                            item.IsDeleted = false;
                            item.CreateTime = updateTime;
                            await InsertAsync(item);
                        }
                        else
                        {
                            roleAuthority.IsDeleted = false;
                            roleAuthority.HalfCheck = item.HalfCheck;
                        }
                    }
                }

                if (checkRoleAuthorityList.Any())
                {
                    var param = new List<string>
                    {
                        "IsDeleted","HalfCheck","UpdateBy","UpdateTime"
                    };

                    foreach (var roleAuthority in checkRoleAuthorityList)
                    {
                        await UpdateAsync(roleAuthority, param);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBInsertException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            return true;


            //var res = false;
            //var delFlag = true;

            //OpenConnectionAsync(async conn =>
            //{
            //    var tran = conn.BeginTransaction();

            //    try
            //    {
            //        var checkRoleAuthorityList = (await GetRoleAuthorityListByRoleId(new RoleAuthorityListReqByRoleIdDTO { RoleId = req.RoleId })).Any();
            //        if (checkRoleAuthorityList && req.RoleId > 0)
            //        {
            //            delFlag = DeleteRoleAuthorityByRoleId(req, conn);
            //            if (!delFlag)
            //            {
            //                var msg = $"{CommonConstant.DBDeleteFailed}{CommonConstant.ParameterReqDetail}\n" +
            //                          $"{JsonConvert.SerializeObject(req)}";
            //                logger.Error(msg);
            //            }
            //            res = true;
            //        }

            //        if (delFlag && req.AddList.Any())
            //        {
            //            //await InsertBatchAsync(req.AddList);
            //            var insSql = @"INSERT role_authority (role_id, authority_id, half_check, create_by)
            //                           VALUES (@RoleId, @AuthorityId, @HalfCheck, @CreateBy)";
            //            res = conn.Execute(insSql, req.AddList) > 0;
            //        }

            //        tran.Commit();
            //    }
            //    catch (Exception e)
            //    {
            //        tran.Rollback();

            //        logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBInsertException), e);
            //        throw new CustomException(CommonConstant.InternalDBError);
            //    }
            //});

            //return res;
        }

        public async Task<List<RoleAuthorityDO>> GetRoleAuthorityListByRoleId(RoleAuthorityListReqByRoleIdDTO req)
        {
            if (req == null || req.RoleId <= 0)
            {
                var msg = $"{CommonConstant.ArgumentValidateFailed}\n" +
                          $"{CommonConstant.ParameterReqDetail}\n{JsonConvert.SerializeObject(req)}";
                logger.Warn(msg);
                return null;
            }

            var param = new DynamicParameters();
            param.Add("@roleId", req.RoleId);

            IEnumerable<RoleAuthorityDO> res = new List<RoleAuthorityDO>();
            var sql = @"SELECT ra.id, ra.role_id roleId, ra.authority_id authorityId, ra.half_check halfCheck,
                        ra.is_deleted isdeleted, ra.create_by createby, ra.create_time createtime, ra.update_by updateby, ra.update_time updatetime 
                        FROM role_authority ra
                        WHERE 0 = 0 AND ra.role_id = @roleId";

            if (req.IsDeleted >= 0)
            {
                param.Add("@isDeleted", req.IsDeleted);
                sql += " AND ra.is_deleted = @isDeleted ";
            }
            else
            {
                sql += " AND (ra.is_deleted = 0 or ra.is_deleted = 1) ";
            }
            await OpenSlaveConnectionAsync(async conn => { res = await conn.QueryAsync<RoleAuthorityDO>(sql, param); });

            return res.ToList();
        }

        // ---------------------------------- 私有方法 --------------------------------------
        private bool DeleteRoleAuthorityByRoleId(RoleAuthorityReqDO req, IDbConnection conn = null)
        {
            var result = false;
            try
            {
                var roleId = req?.RoleId;

                var sql = @"UPDATE role_authority SET is_deleted = 1, update_by = @updateBy, update_time = @updateTime
                            WHERE is_deleted = 0 AND role_id = @roleId";
                var param = new DynamicParameters();
                param.Add("@roleId", roleId);
                param.Add("@updateBy", req?.UpdateBy);
                param.Add("@updateTime", DateTime.Now);

                if (conn == null)
                {
                    OpenConnection(con => result = con.Execute(sql, param) > 0);
                }
                else
                {
                    result = conn.Execute(sql, param) > 0;
                }
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }

            return result;
        }

        private async Task<bool> DeleteRoleAuthorityByRoleIdAndAuthorityId(List<RoleAuthorityDO> req)
        {
            var result = false;
            try
            {
                if (null == req?.FirstOrDefault())
                {
                    logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo());
                    return false;
                }

                var roleId = req.FirstOrDefault()?.RoleId;
                var authorityId = req.Select(s => s.AuthorityId).ToList();
                var authStr = $"({string.Join(",", authorityId)})";

                var sql = @"UPDATE role_authority SET is_deleted = 1, update_by = @updateBy, update_time = @updateTime
                            WHERE is_deleted = 0 AND role_id = @roleId AND authority_id IN @authorityId";
                var param = new DynamicParameters();
                param.Add("@roleId", roleId);
                param.Add("@authorityId", authStr);
                param.Add("@updateBy", req.FirstOrDefault()?.UpdateBy);
                param.Add("@updateTime", DateTime.Now);

                await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param) > 0);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }

            return result;
        }


    }
}
