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
using Ae.AccountAuthority.Service.Core.Model;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Core.Response;
using Ae.AccountAuthority.Service.Dal.Model;
using Ae.AccountAuthority.Service.Dal.Repositorys.IDAL;

namespace Ae.AccountAuthority.Service.Dal.Repositorys.DAL
{
    public class RoleRepository : AbstractRepository<RoleDO>, IRoleRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<RoleRepository> logger;
        private readonly string className;

        public RoleRepository(ApolloErpLogger<RoleRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("AccountAuthoritySql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("AccountAuthoritySqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<List<OrgRangeRolesDTO>> GetRoleListByOrgIds(List<OrgEntityReqDTO> req)
        {
            var res = new List<OrgRangeRolesDTO>();

            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var sb = new StringBuilder();
                    var len = req.Count;
                    for (int i = 0; i < len; i++)
                    {
                        var obj = req[i];

                        if (!req.Any()) continue;
                        if(obj.Type== RoleType.Shop)
                        {
                            var sel = @"SELECT id, name, type, organization_id organizationId,features, description, is_deleted isdeleted
                                    FROM role WHERE is_deleted = 0 ";
                            var whrClu = $"AND organization_id = {obj.OrganizationId} AND type = {Convert.ToInt32(obj.Type)} and features={obj.SystemType} ";
                            sb.AppendLine(sel).AppendLine(whrClu);
                            if (i != len - 1) { sb.AppendLine("UNION ALL "); }
                        }
                        else if(obj.Type == RoleType.Company)
                        {
                            var sel = @"SELECT id, name, type, organization_id organizationId,features, description, is_deleted isdeleted
                                    FROM role WHERE is_deleted = 0 ";
                            var whrClu = $"AND organization_id = {obj.OrganizationId} AND type = {Convert.ToInt32(obj.Type)} ";
                            //拼接虚拟角色的查询
                            var selt = $"UNION ALL SELECT id, name, type,{obj.OrganizationId} organizationId,features, description, is_deleted isdeleted FROM role WHERE is_deleted = 0 ";
                            var whrClut = $"AND features = {obj.SystemType} AND type = {Convert.ToInt32(obj.Type)} ";

                            sb.AppendLine(sel).AppendLine(whrClu).AppendLine(selt).AppendLine(whrClut);
                            if (i != len - 1) { sb.AppendLine("UNION ALL "); }
                        }
                        
                    }

                    if (sb.Length == CommonConstant.Zero)
                    {
                        logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo(CommonConstant.NotExpectationResult));
                        return;
                    }

                    var resDo = await conn.QueryAsync<RoleDTO>(sb.ToString());
                    if (resDo != null && resDo.Any())
                    {
                        resDo.ToList().ForEach(rdf =>
                        {
                            var tmp = res.Find(rf => rf.OrganizationId == rdf.OrganizationId && rf.Type == rdf.Type);
                            if (tmp == null)
                            {
                                var resEntity = new OrgRangeRolesDTO
                                {
                                    OrganizationId = rdf.OrganizationId,
                                    Type = rdf.Type,
                                    Roles = new List<RoleDTO>()
                                };

                                resEntity.Roles.Add(rdf);
                                res.Add(resEntity);
                            }
                            else
                            {
                                tmp.Roles.Add(rdf);
                            }
                        });
                    }
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            finally
            {
                logger.Info($"DB: {className}.GetRoleListByOrgIds 请求值：{JsonConvert.SerializeObject(req)}");
                logger.Info($"DB: {className}.GetRoleListByOrgIds 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }


        public async Task<bool> CreateRole(RoleDO req)
        {
            long id;
            try
            {
                id = await InsertAsync<long>(req);
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ExceptionOccured}" +
                          $"\n{CommonConstant.ParameterReqDetail}{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);
                throw;
            }

            return id > 0;
        }

        public async Task<bool> UpdateRoleById(RoleDO req)
        {
            bool res;
            try
            {
                var param = new List<string>
                {
                   "OrganizationId", "Name", "Type","ShopType", "Description", "UpdateBy", "UpdateTime","Features"
                };

                res = await UpdateAsync(req, param) > 0;
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ExceptionOccured}" +
                          $"\n{CommonConstant.ParameterReqDetail}{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);
                throw;
            }

            return res;
        }

        public async Task<bool> DeleteRoleById(RoleDO req)
        {
            var result = false;
            await OpenConnectionAsync(async conn =>
            {
                var tran = conn.BeginTransaction();

                try
                {
                    var roleSql = @"UPDATE role SET is_deleted = 1, update_by = @updateBy, update_time = @updateTime
                                    WHERE is_deleted = 0 AND id = @id";
                    var param = new DynamicParameters();
                    param.Add("@id", req.Id);
                    param.Add("@updateBy", req.UpdateBy);
                    param.Add("@updateTime", req.UpdateTime);

                    var flag = await conn.ExecuteAsync(roleSql, param);
                    if (flag > 0)
                    {
                        var roleAuthSql = @"UPDATE role_authority SET is_deleted = 1, update_by = @updateBy, update_time = @updateTime
                                            WHERE is_deleted = 0 AND role_id = @id; ";
                        var empRoleSql = @"UPDATE employee_role SET is_deleted = 1, update_by = @updateBy, update_time = @updateTime
                                           WHERE is_deleted = 0 AND role_id = @id; ";
                        flag = await conn.ExecuteAsync(roleAuthSql + empRoleSql, param);
                    }
                    else
                    {
                        tran.Rollback();
                    }

                    result = flag >= 0;

                    tran.Commit();
                }
                catch (Exception e)
                {
                    tran.Rollback();

                    logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBDeleteException), e);
                    throw new CustomException(CommonConstant.InternalDBError);
                }
            });

            return result;
        }

        public async Task<PagedEntity<RoleDO>> GetPagedRoleList(RoleListReqDTO req)
        {
            PagedEntity<RoleDO> res = new PagedEntity<RoleDO>();
            IEnumerable<RoleDO> enumerable = null;
            var total = 0;

            try
            {
                var param = new DynamicParameters();
                param.Add("@index", (req.PageIndex - 1) * req.PageSize);
                param.Add("@size", req.PageSize);

                #region Where Clause
                var whereClause = "";
                if (req.IsDeleted >= 0)
                {
                    param.Add("@isDeleted", req.IsDeleted);
                    whereClause += "WHERE is_deleted = @isDeleted ";
                }
                else
                {
                    whereClause += "WHERE (is_deleted = 0 OR is_deleted = 1) ";
                }
                if (req.Id > 0)
                {
                    param.Add("@id", req.Id);
                    whereClause += "AND id = @id ";
                }

                if (req.OrganizationId >= 0)
                {
                    param.Add("@OrganizationId", req.OrganizationId);
                    whereClause += "AND organization_id = @OrganizationId ";
                }

                if (req.Type >= 0)
                {
                    param.Add("@Type", req.Type);
                    whereClause += "AND type = @Type ";
                }
                #endregion Where Clause

                var sqlCount = @"SELECT COUNT(id) FROM role " + whereClause;
                await OpenSlaveConnectionAsync(async conn =>
                {
                    total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
                });

                if (total > 0)
                {
                    var sql = @"SELECT id, name, type, organization_id organizationid,shop_type ShopType, description, is_deleted isdeleted, create_by createby, create_time createtime, update_by updateby, update_time updatetime,features Features
                                FROM role "
                              + whereClause
                              + @"ORDER BY update_time DESC, create_time DESC  
                                  LIMIT @index, @size";
                    await OpenSlaveConnectionAsync(async conn =>
                    {
                        enumerable = await conn.QueryAsync<RoleDO>(sql, param);
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

        public async Task<List<RoleDO>> GetAllRoleList()
        {
            return await GetRoleListWithIsDeleted(null);
        }

        public async Task<List<RoleDO>> GetRoleListByName(List<string> roleName)
        {
            List<RoleDO> res = new List<RoleDO>();

            if (roleName == null || !roleName.Any()) return res;

            var param = new DynamicParameters();
            param.Add("@name", roleName);

            var sql = @"SELECT id, name, type, organization_id organizationId,features, description, 
                        is_deleted isdeleted, create_by createby, create_time createtime, update_by updateby, update_time updatetime
                        FROM role 
                        WHERE is_deleted = 0 AND name IN @name ";
            await OpenSlaveConnectionAsync(async conn => { res = (await conn.QueryAsync<RoleDO>(sql, param)).ToList(); });

            return res;
        }

        public async Task<List<RoleDO>> GetRoleListByOrgIdAsync(RoleListReqDTO req)
        {
            return await GetRoleListWithIsDeleted(req);
        }

        public async Task<List<RoleDO>> GetRoleListByIsDeleted(RoleListReqDTO req)
        {
            return await GetRoleListWithIsDeleted(req);
        }

        public async Task<List<RoleDO>> GetRoleListAnyCondition(RoleListInternalReqDTO req)
        {
            var param = new DynamicParameters();

            var condition = "";
            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                condition += "OR name = @AuthorityName ";
                param.Add("@AuthorityName", req.Name);
            }

            param.Add("@OrganizationId", req.OrganizationId);
            var whereClause = string.IsNullOrWhiteSpace(condition)
                ? "WHERE is_deleted = 0 AND organization_id = @OrganizationId "
                : $"WHERE is_deleted = 0 AND organization_id = @OrganizationId AND ({condition.Substring(2)})";

            var sql = @"SELECT id, name, type, organization_id organizationId,features, description, is_deleted isdeleted, create_by createby, create_time createtime, update_by updateby, update_time updatetime
                        FROM role "
                      + whereClause;

            var res = new List<RoleDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                var enumerable = await conn.QueryAsync<RoleDO>(sql, param);
                res = enumerable.ToList();
            });
            return res;
        }
        
        // ---------------------------------- 私有方法 --------------------------------------
        private async Task<List<RoleDO>> GetRoleListWithIsDeleted(RoleListReqDTO req)
        {
            var param = new DynamicParameters();
            var whereClause = "";
            if (req?.IsDeleted >= 0)
            {
                param.Add("@isDeleted", req.IsDeleted);
                whereClause += " WHERE is_deleted = @isDeleted ";
            }
            if (req?.Id > 0)
            {
                param.Add("@Id", req.Id);
                whereClause += whereClause.Contains("WHERE")
                    ? " AND id = @Id " : " WHERE id = @Id ";
            }
            if (req?.OrganizationId > 0)
            {
                param.Add("@OrganizationId", req.OrganizationId);
                whereClause += whereClause.Contains("WHERE")
                    ? " AND organization_id = @OrganizationId " : " WHERE organization_id = @OrganizationId ";
            }
            if (null != req?.Type && req.Type != RoleType.None)
            {
                param.Add("@Type", req.Type);
                whereClause += whereClause.Contains("WHERE")
                    ? " AND type = @Type " : " WHERE type = @Type ";
            }
            if (null != req?.Features)
            {
                param.Add("@Features", req.Features);
                whereClause += whereClause.Contains("WHERE")
                    ? " AND features = @Features " : " WHERE features = @Features ";
            }

            IEnumerable<RoleDO> res = new List<RoleDO>();
            var sql = @"SELECT id, name, type, organization_id organizationId, description,features, 
                        is_deleted isdeleted, create_by createby, create_time createtime, update_by updateby, update_time updatetime
                        FROM role " +
                      whereClause +
                      " ORDER BY is_deleted ASC, update_time DESC, create_by DESC ";
            await OpenSlaveConnectionAsync(async conn => { res = await conn.QueryAsync<RoleDO>(sql, param); });

            return res.ToList();
        }
        

    }
}
