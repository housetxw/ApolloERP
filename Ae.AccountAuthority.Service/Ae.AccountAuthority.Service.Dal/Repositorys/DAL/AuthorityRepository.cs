using System;
using System.Collections.Generic;
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

namespace Ae.AccountAuthority.Service.Dal.Repositorys.DAL
{
    public class AuthorityRepository : AbstractRepository<AuthorityDO>, IAuthorityRepository
    {
        #region 变量和构造器

        private readonly ApolloErpLogger<AuthorityRepository> logger;

        public AuthorityRepository(ApolloErpLogger<AuthorityRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("AccountAuthoritySql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("AccountAuthoritySqlReadOnly");

            this.logger = logger;
        }

        #endregion 变量和构造器

        #region 接口实现

        public async Task<bool> CreateAuthority(AuthorityDO req, List<long> parenIdList = null)
        {
            long id = 0;

            await OpenConnectionAsync(async conn =>
            {
                var tran = conn.BeginTransaction();

                try
                {
                    id = await InsertAsync<long>(req);

                    if (id > 0 && null != parenIdList && parenIdList.Any())
                    {
                        var param = new DynamicParameters();
                        param.Add("@updateBy", req.CreateBy);
                        param.Add("@authorityId", parenIdList);
                        var sql = @"UPDATE role_authority SET half_check = 1, update_by = @updateBy, update_time = NOW(3) 
                                    WHERE is_deleted = 0 AND half_check = 0 AND authority_id IN @authorityId; ";

                        var ud = await conn.ExecuteAsync(sql, param);
                        if (ud > 0) id = ud;
                    }

                    if (req.ParentId <= CommonConstant.Zero)
                    {
                        var param = new DynamicParameters();
                        param.Add("@AuthorityId", req.ApplicationId);

                        var roleAuthIds = await conn.QueryAsync<long>(
                            @"SELECT DISTINCT id FROM role_authority 
                                WHERE is_deleted = 0 AND half_check = 0 AND authority_id = @AuthorityId; ", param);

                        if (roleAuthIds.Any())
                        {
                            param.Add("@UpdateBy", req.CreateBy);
                            param.Add("@RoleAuthIds", roleAuthIds);

                            var udRoleAuthHalfChecked = await conn.ExecuteAsync(
                                @"UPDATE role_authority SET half_check = 1, update_by = @UpdateBy, update_time = NOW(3) 
                                WHERE is_deleted = 0 AND half_check = 0 AND id IN @RoleAuthIds; ", param);

                            if (udRoleAuthHalfChecked > 0) id = udRoleAuthHalfChecked;
                        }
                    }

                    if (id >= 0)
                    {
                        tran.Commit();
                    }
                }
                catch (Exception e)
                {
                    tran.Rollback();

                    logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBInsertException), e);
                    throw new CustomException(CommonConstant.InternalDBError);
                }
            });

            return id > 0;
        }

        public async Task<bool> UpdateAuthorityById(AuthorityDO req)
        {
            var res = false;
            try
            {
                var param = new List<string>
                {
                    "ParentId", "Name", "Type", "ApplicationId", "Route", "RouteParameter", "MenuIcon", "Sort",
                    "Description", "UpdateBy", "UpdateTime"
                };

                res = await UpdateAsync(req, param) > 0;
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateException), e);
            }

            return res;
        }

        public async Task<bool> DeleteAuthorityById(AuthorityDO req, List<long> parenIdList = null)
        {
            int del = 0;

            await OpenConnectionAsync(async conn =>
            {
                var tran = conn.BeginTransaction();
                try
                {
                    var authSql = @"UPDATE authority SET is_deleted = 1, update_by = @updateBy, update_time = @updateTime
                                    WHERE is_deleted = 0 AND id = @id";
                    var param = new DynamicParameters();
                    param.Add("@id", req.Id);
                    param.Add("@updateBy", req.UpdateBy);
                    param.Add("@updateTime", req.UpdateTime);

                    del = await conn.ExecuteAsync(authSql, param);
                    if (del > 0 && null != parenIdList && parenIdList.Any())
                    {
                        var roleAuthSql = @"UPDATE role_authority SET is_deleted = 1, update_by = @updateBy, update_time = @updateTime
                                            WHERE is_deleted = 0 AND authority_id = @id; ";

                        //var pIdStr = $"({string.Join(",", parenIdList)})";
                        param.Add("@authorityId", parenIdList);
                        var halfCheckSql = @"UPDATE role_authority SET half_check = 1, update_by = @updateBy, update_time = NOW(3)
                                             WHERE is_deleted = 0 AND half_check = 0 AND authority_id IN @authorityId; ";
                        var ud = await conn.ExecuteAsync(roleAuthSql + halfCheckSql, param);

                        if (ud > 0) del = ud;
                    }

                    if (del >= 0)
                    {
                        tran.Commit();
                    }
                }
                catch (Exception e)
                {
                    tran.Rollback();

                    logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateException), e);
                    throw new CustomException(CommonConstant.InternalDBError);
                }
            });

            return del >= 0;
        }

        public async Task<PagedEntity<AuthorityPageDO>> GetPagedAuthorityList(AuthorityListReqDTO req)
        {
            var res = new PagedEntity<AuthorityPageDO>();
            IEnumerable<AuthorityPageDO> enumerable = null;
            var total = 0;

            try
            {
                var param = new DynamicParameters();
                param.Add("@index", (req.PageIndex - 1) * req.PageSize);
                param.Add("@size", req.PageSize);

                var whereClause = "";
                if (req.IsDeleted >= 0)
                {
                    param.Add("@isDeleted", req.IsDeleted);
                    whereClause += "WHERE auth.is_deleted = @isDeleted ";
                }
                else
                {
                    whereClause += "WHERE (auth.is_deleted = 0 OR auth.is_deleted = 1) ";
                }

                if (req.ApplicationId > 0)
                {
                    param.Add("@ApplicationId", req.ApplicationId);
                    whereClause += "AND auth.application_id = @ApplicationId ";
                }

                if (req.Id > 0)
                {
                    param.Add("@id", req.Id);
                    whereClause += "AND auth.id = @id ";
                }

                if (req.Type >= 0)
                {
                    param.Add("@Type", req.Type);
                    whereClause += "AND auth.type = @Type ";
                }

                var sqlCount = @"SELECT COUNT(id) FROM authority auth " + whereClause;
                await OpenSlaveConnectionAsync(async conn =>
                {
                    total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
                });

                if (total > 0)
                {
                    var sql =
                        @"SELECT auth.id, auth.name, auth.parent_id parentId, authP.name parentName, auth.type, auth.application_id applicationId, app.name applicationName, 
                                auth.route, auth.route_parameter routeParameter, auth.menu_icon menuIcon, auth.sort, auth.description,
                                auth.is_deleted isdeleted, auth.create_by createby, auth.create_time createtime, auth.update_by updateby, auth.update_time updatetime
                                FROM authority auth
                                LEFT JOIN authority authP ON auth.parent_id = authP.id
                                LEFT JOIN application app ON auth.application_id = app.id "
                        + whereClause
                        + @"ORDER BY auth.update_time DESC, auth.create_time DESC 
                                  LIMIT @index, @size";
                    await OpenSlaveConnectionAsync(async conn =>
                    {
                        enumerable = await conn.QueryAsync<AuthorityPageDO>(sql, param);
                    });
                    res.Items = enumerable.ToList();
                    res.TotalItems = total;
                }
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ExceptionOccured}\n" +
                          $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);
                throw;
            }

            return res;
        }

        public async Task<List<AuthorityPageDO>> GetAllAuthorityList()
        {
            return await GetAuthorityListWithIsDeleted(null);
        }

        public async Task<List<AuthorityPageDO>> GetAuthorityListByIsDeleted(AuthorityListReqDTO req)
        {
            return await GetAuthorityListWithIsDeleted(req);
        }

        public async Task<List<AuthorityPageDO>> GetAuthorityListByApplicationIdAsync(AuthorityListReqDTO req)
        {
            return await GetAuthorityListWithIsDeleted(req);
        }

        public async Task<List<AuthorityPageDO>> GetAuthorityListByApplicationIdExceptIdAsync(AuthorityListReqDTO req)
        {
            IEnumerable<AuthorityPageDO> res = new List<AuthorityPageDO>();

            if (null == req || req.Id <= 0 || req.ApplicationId <= 0)
            {
                var msg = $"{CommonConstant.ArgumentValidateFailed}\n" +
                          $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(req)}";
                logger.Warn(msg);
                return res.ToList();
            }

            var param = new DynamicParameters();
            param.Add("@Id", req.Id);
            var whereClause = "WHERE id != @Id ";
            param.Add("@ApplicationId", req.ApplicationId);
            whereClause += "AND application_id = @ApplicationId ";

            var sql = @"SELECT id, name, parent_id parentId, type, 
                        CASE type
	                        WHEN 0 THEN '模块'
	                        WHEN 1 THEN '页面'	
	                        WHEN 2 THEN '按钮'
	                        ELSE ''
                        END typeName,
                        application_id applicationId,
                        route, route_parameter routeParameter, menu_icon menuIcon, sort, description,
                        is_deleted isdeleted, create_by createby, create_time createtime, update_by updateby, update_time updatetime
                        FROM authority " +
                      whereClause +
                      " ORDER BY is_deleted ASC ";
            await OpenSlaveConnectionAsync(async conn => { res = await conn.QueryAsync<AuthorityPageDO>(sql, param); });

            return res.ToList();
        }

        public async Task<List<AuthorityPageDO>> GetAuthorityListAnyCondition(AuthorityListInternalReqDTO req)
        {
            var param = new DynamicParameters();

            var condition = "";
            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                condition += "OR auth.name = @AuthorityName ";
                param.Add("@AuthorityName", req.Name);
            }
            if (!string.IsNullOrWhiteSpace(req.Route))
            {
                condition += "OR auth.route = @Route ";
                param.Add("@Route", req.Route);
            }

            param.Add("@ApplicationId", req.ApplicationId);
            var whereClause = string.IsNullOrWhiteSpace(condition)
                ? "WHERE auth.is_deleted = 0 AND application_id = @ApplicationId "
                : $"WHERE auth.is_deleted = 0 AND application_id = @ApplicationId AND ({condition.Substring(2)})";

            var sql = @"SELECT auth.id, auth.name, auth.parent_id parentId, auth.type, auth.application_id applicationId, app.`name` applicationName,
                        auth.route, auth.route_parameter routeParameter, auth.menu_icon menuIcon, auth.sort, auth.description,
                        auth.is_deleted isdeleted, auth.create_by createby, auth.create_time createtime, auth.update_by updateby, auth.update_time updatetime
                        FROM authority auth
                        LEFT JOIN application app ON auth.application_id = app.id "
                      + whereClause;

            var res = new List<AuthorityPageDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                var enumerable = await conn.QueryAsync<AuthorityPageDO>(sql, param);
                res = enumerable.ToList();
            });
            return res;
        }

        public async Task<List<AuthorityPageDO>> GetAuthorityListWithApplicationInfo(AuthorityListInternalReqDTO req)
        {
            return await GetAuthorityListWithApplicationInfo(new AuthorityListReqDTO { IsDeleted = 0,Initialism = req.Initialism });
        }

        #endregion 接口实现

        #region 私有方法

        private async Task<List<AuthorityPageDO>> GetAuthorityListWithIsDeleted(AuthorityListReqDTO req)
        {
            var param = new DynamicParameters();
            var whereClause = "";
            if (req?.IsDeleted >= 0)
            {
                param.Add("@isDeleted", req.IsDeleted);
                whereClause += "WHERE is_deleted = @isDeleted";
            }
            if (req?.ApplicationId > 0)
            {
                param.Add("@ApplicationId", req.ApplicationId);
                whereClause += whereClause.Contains("WHERE")
                    ? "AND application_id = @ApplicationId"
                    : "WHERE application_id = @ApplicationId";
            }

            IEnumerable<AuthorityPageDO> res = new List<AuthorityPageDO>();
            var sql = @"SELECT id, name, parent_id parentId, type, 
                        CASE type
	                        WHEN 0 THEN '模块'
	                        WHEN 1 THEN '页面'	
	                        WHEN 2 THEN '按钮'
	                        ELSE ''
                        END typeName,
                        application_id applicationId,
                        route, route_parameter routeParameter, menu_icon menuIcon, sort, description,
                        is_deleted isdeleted, create_by createby, create_time createtime, update_by updateby, update_time updatetime
                        FROM authority " +
                      whereClause +
                      " ORDER BY is_deleted ASC ";
            await OpenSlaveConnectionAsync(async conn => { res = await conn.QueryAsync<AuthorityPageDO>(sql, param); });

            return res.ToList();
        }

        private async Task<List<AuthorityPageDO>> GetAuthorityListWithApplicationInfo(AuthorityListReqDTO req)
        {
            var param = new DynamicParameters();
            var whereClause = "WHERE 1=1 ";
            if (req?.IsDeleted >= 0)
            {
                param.Add("@isDeleted", req.IsDeleted);
                whereClause += " and auth.is_deleted = @isDeleted AND app.is_deleted = @isDeleted";
            }
            if (!string.IsNullOrWhiteSpace(req.Initialism))
            {
                whereClause += " and app.initialism = @Initialism ";
                param.Add("@Initialism", req.Initialism);
            }

            IEnumerable<AuthorityPageDO> res = new List<AuthorityPageDO>();
            var sql = @"SELECT auth.id, auth.name, auth.parent_id parentId, auth.type, 
                        CASE auth.type
	                        WHEN 0 THEN '模块'
	                        WHEN 1 THEN '页面'	
	                        WHEN 2 THEN '按钮'
	                        ELSE ''
                        END typeName,
                        auth.application_id applicationId, app.name applicationName,
                        auth.route, auth.route_parameter routeParameter, auth.menu_icon menuIcon, auth.sort, auth.description,
                        app.is_deleted isdeleted, app.create_by createby, app.create_time createtime, app.update_by updateby, app.update_time updatetime
                        FROM authority auth
                        LEFT JOIN application app ON auth.application_id = app.id " +
                      whereClause +
                      " ORDER BY auth.parent_id, auth.application_id ";
            await OpenSlaveConnectionAsync(async conn => { res = await conn.QueryAsync<AuthorityPageDO>(sql, param); });

            return res.ToList();
        }

        #endregion 私有方法
    }
}