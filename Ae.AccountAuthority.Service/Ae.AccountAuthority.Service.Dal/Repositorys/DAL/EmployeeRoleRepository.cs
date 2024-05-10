using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    public class EmployeeRoleRepository : AbstractRepository<EmployeeRoleDO>, IEmployeeRoleRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly ApolloErpLogger<EmployeeRoleRepository> logger;
        private readonly string className;
        

        public EmployeeRoleRepository(ApolloErpLogger<EmployeeRoleRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("AccountAuthoritySql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("AccountAuthoritySqlReadOnly");
            
            this.logger = logger;
            className = this.GetType().Name;
        }

        // ---------------------------------- 授权接口实现 --------------------------------------
        public async Task<List<AuthorizationDO>> GetEmpAuthorityListByEmpIdAsync(AuthorizationReqDTO req)
        {
            List<AuthorizationDO> res = new List<AuthorizationDO>();

            try
            {
                var param = new DynamicParameters();
                param.Add("@OrganizationId", req.EmployeeType == EmployeeType.Shop ? CommonConstant.Zero : req.OrganizationId);
                param.Add("@EmployeeType", req.EmployeeType);
                param.Add("@EmployeeId", req.EmployeeId);

                var sql = string.Empty;

                sql = @"SELECT DISTINCT er.employee_id employeeId, ra.authority_id authorityId, a.name authorityName, a.parent_id parentId, 
                            a.route, a.menu_icon menuIcon, a.route_parameter routeParameter, a.type authorityType, 
                            a.application_id applicationId, app.`name` applicationName,a.sort
                            FROM employee_role er
                            INNER JOIN role r ON r.id = er.role_id 
                            INNER JOIN role_authority ra ON r.id = ra.role_id
                            INNER JOIN authority a ON ra.authority_id = a.id
                            INNER JOIN application app ON a.application_id = app.id
                            WHERE er.is_deleted = 0 AND r.is_deleted = 0 AND ra.is_deleted = 0 AND a.is_deleted = 0 AND app.is_deleted = 0
                            AND (r.organization_id = @OrganizationId Or r.organization_id=0) AND r.type = @EmployeeType AND er.employee_id = @EmployeeId 
                            ORDER BY a.sort";


                res = new List<AuthorizationDO>();
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var enumerable = await conn.QueryAsync<AuthorizationDO>(sql, param);
                    res = enumerable.ToList();
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
            }
            finally
            {
                logger.Info($"DB: {className}.GetEmpAuthorityListByEmpIdAsync 请求参数：{JsonConvert.SerializeObject(req)}");
                logger.Info($"SVC: {className}.GetEmpAuthorityListByEmpIdAsync 返回值：TotalItems:{res.Count}, Items:{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        public async Task<List<AuthorizationDO>> GetRangeRoleAuthorityListByIds(RangeRoleAuthorityReqDTO req)
        {
            List<AuthorizationDO> res = new List<AuthorizationDO>();

            try
            {
                var param = new DynamicParameters();
                param.Add("@EmployeeId", req.EmployeeId);
                param.Add("@RoleIds", req.RoleIds);

                var sql = @"SELECT DISTINCT @EmployeeId employeeId, ra.authority_id authorityId, a.name authorityName, a.parent_id parentId, 
                            a.route, a.menu_icon menuIcon, a.route_parameter routeParameter, a.type authorityType, 
                            a.application_id applicationId, app.`name` applicationName,a.sort
                            FROM role r
                            INNER JOIN role_authority ra ON r.id = ra.role_id
                            INNER JOIN authority a ON ra.authority_id = a.id
                            INNER JOIN application app ON a.application_id = app.id
                            WHERE r.is_deleted = 0 AND ra.is_deleted = 0 AND a.is_deleted = 0 AND app.is_deleted = 0 
                            AND r.id IN @RoleIds
                            ORDER BY a.sort";

                res = new List<AuthorizationDO>();
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var enumerable = await conn.QueryAsync<AuthorizationDO>(sql, param);
                    res = enumerable.ToList();
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
            }
            finally
            {
                logger.Info($"DB: {className}.GetRangeRoleAuthorityListByIds 请求参数：{JsonConvert.SerializeObject(req)}");
                logger.Info($"DB: {className}.GetRangeRoleAuthorityListByIds 返回值：TotalItems:{res.Count}, Items:{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }


        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<List<EmployeeRoleListDO>> GetEmployeeRoleListByEmpIdAsync(EmployeeRoleListReqDTO req)
        {
            List<EmployeeRoleListDO> res = new List<EmployeeRoleListDO>();

            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var param = new DynamicParameters();
                    param.Add("@EmployeeId", req.EmployeeId);
                    var whrClu = "WHERE er.is_deleted = 0 AND r.is_deleted = 0 AND employee_id = @EmployeeId ";

                    var sql = @"SELECT er.employee_id employeeId, er.role_id roleId, r.name roleName, r.organization_id organizationId, r.description, r.type roleType,
                                    r.is_deleted isDeleted, 
                                    er.create_by createBy, er.create_time createTime, er.update_by updateBy, er.update_time updateTime
                                    FROM employee_role er
                                    INNER JOIN role r ON r.id = er.role_id "
                                  + whrClu +
                                  " ORDER BY er.is_deleted, er.update_time DESC, er.create_time DESC";

                    var enumerable = await conn.QueryAsync<EmployeeRoleListDO>(sql, param);
                    res = enumerable.ToList();
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
            }
            finally
            {
                logger.Info($"DB: {className}.GetEmployeeRoleListByEmpIdAsync 请求参数： {JsonConvert.SerializeObject(req)}");
                logger.Info($"DB: {className}.GetEmployeeRoleListByEmpIdAsync 返回值： {JsonConvert.SerializeObject(res)}");
            }
            return res;
        }

        public async Task<List<EmployeeRoleListDTO>> GetEmployeeRoleListByEmpIds(EmployeeRoleListByEmpIdsReqDTO req)
        {
            var res = new List<EmployeeRoleListDTO>();
            try
            {
                await OpenSlaveConnectionAsync(async conn =>
                {
                    var l = req.EmployeeIds.Count;

                    if (l <= 0) return;

                    var sb = new StringBuilder();
                    for (var i = 0; i < l; i++)
                    {
                        var sql = $@"SELECT er.employee_id employeeId, er.role_id roleId, r.name roleName, r.organization_id organizationId, r.description, r.type roleType,
                                            r.is_deleted isDeleted, 
                                            er.create_by createBy, er.create_time createTime, er.update_by updateBy, er.update_time updateTime
                                            FROM employee_role er
                                            INNER JOIN role r ON r.id = er.role_id 
                                            WHERE er.is_deleted = 0 AND r.is_deleted = 0 AND employee_id = '{req.EmployeeIds[i]}' ";
                        sb.AppendLine(sql);
                        if (i != l - 1) { sb.AppendLine("UNION ALL "); }
                    }

                    //var tmpSql = $@"SELECT employeeId, roleId, roleName, organizationId, roleType, isDeleted FROM
                    var tmpSql = $@"SELECT * FROM
                                        (
                                            {sb}
                                        ) AS t
                                        ORDER BY updateTime DESC, createTime DESC ";

                    var enumerable = await conn.QueryAsync<EmployeeRoleListDTO>(tmpSql);
                    res = enumerable.ToList();
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
            }
            finally
            {
                logger.Info($"DB: {className}.GetEmployeeRoleListByEmpIds 请求参数： {JsonConvert.SerializeObject(req)}");
                logger.Info($"DB: {className}.GetEmployeeRoleListByEmpIds 返回值： {JsonConvert.SerializeObject(res)}");
            }
            return res;
        }

        public async Task<bool> EditEmployeeRole(EmployeeRoleSaveReqDO req)
        {
            long ud = 0;
            await OpenConnectionAsync(async conn =>
            {
                var tran = conn.BeginTransaction();
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@employeeId", req.EmployeeId);
                    param.Add("@updateBy", req.Operator);

                    var udSql = @"UPDATE employee_role SET is_deleted = 1, update_by = @updateBy, update_time = NOW(3) 
                                WHERE is_deleted = 0 AND employee_id = @employeeId ";
                    ud = await conn.ExecuteAsync(udSql, param);

                    var list = req.EmployeeRoleList;
                    if (ud >= 0 && list.Any())
                    {
                        var crtSql = @"INSERT employee_role (employee_id, role_id, create_by)
                                       VALUES (@EmployeeId, @RoleId, @CreateBy)";
                        //await InsertBatchAsync(req.EmployeeRoleList);
                        ud = conn.Execute(crtSql, req.EmployeeRoleList);
                    }

                    if (ud >= 0)
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
            return ud >= 0;
        }


    }
}
