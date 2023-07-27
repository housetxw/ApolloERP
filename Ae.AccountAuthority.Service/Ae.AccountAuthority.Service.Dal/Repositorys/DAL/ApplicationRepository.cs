using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using NLog;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.AccountAuthority.Service.Common.Constant;
using Ae.AccountAuthority.Service.Core.Request;
using Ae.AccountAuthority.Service.Dal.Model;
using Ae.AccountAuthority.Service.Dal.Repositorys.IDAL;
using Ae.AccountAuthority.Service.Common.Logger;
using Ae.AccountAuthority.Service.Core.Response;

namespace Ae.AccountAuthority.Service.Dal.Repositorys.DAL
{
    public class ApplicationRepository : AbstractRepository<ApplicationDO>, IApplicationRepository
    {
        #region 变量和构造器

        private readonly ApolloErpLogger<ApplicationRepository> logger;

        public ApplicationRepository(ApolloErpLogger<ApplicationRepository> logger)
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("AccountAuthoritySql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("AccountAuthoritySqlReadOnly");

            this.logger = logger;
        }

        #endregion 变量和构造器

        #region 接口实现

        public async Task<bool> CreateApplication(ApplicationDO req)
        {
            bool res;
            try
            {
                res = await InsertAsync(req) > 0;
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

        public async Task<bool> UpdateApplicationById(ApplicationDO req)
        {
            bool res;
            try
            {
                var param = new List<string>
                {
                    "Name","Initialism","Route","UpdateBy","UpdateTime"
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

        public async Task<bool> DeleteApplicationById(ApplicationDO req)
        {
            var result = false;
            try
            {
                var sql = @"UPDATE application SET is_deleted = 1, update_by = @updateBy, update_time = @updateTime
                            WHERE is_deleted = 0 AND id = @id";
                var param = new DynamicParameters();
                param.Add("@id", req.Id);
                param.Add("@updateBy", req.UpdateBy);
                param.Add("@updateTime", req.UpdateTime);
                await OpenConnectionAsync(async conn =>
                {
                    var flag = await conn.ExecuteAsync(sql, param);
                    result = flag > 0;
                });
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.DBUpdateException}" +
                          $"\n{CommonConstant.ParameterReqDetail}{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);
                throw;
            }

            return result;
        }

        public async Task<PagedEntity<ApplicationDO>> GetPagedApplicationList(AppListReqDTO req)
        {
            PagedEntity<ApplicationDO> res = new PagedEntity<ApplicationDO>();
            IEnumerable<ApplicationDO> enumerable = null;
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

                var sqlCount = @"SELECT COUNT(id) FROM application " + whereClause;
                await OpenSlaveConnectionAsync(async conn =>
                {
                    total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);
                });

                if (total > 0)
                {
                    var sql = @"SELECT id, name, initialism, route, is_deleted isdeleted, create_by createby, create_time createtime, update_by updateby, update_time updatetime
                                FROM application "
                              + whereClause
                              + @"ORDER BY update_time DESC, create_time DESC  
                                  LIMIT @index, @size";
                    await OpenSlaveConnectionAsync(async conn =>
                    {
                        enumerable = await conn.QueryAsync<ApplicationDO>(sql, param);
                    });
                    res.Items = enumerable.ToList();
                    res.TotalItems = total;
                }
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ExceptionOccured}" +
                          $"\n{CommonConstant.ParameterReqDetail}{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);
                throw;
            }

            return res;

            #region MyRegion
            //var param = new DynamicParameters();
            //var condition = new StringBuilder();
            //condition.Append("WHERE is_deleted = 0");
            //if (req.Id > 0)
            //{
            //    condition.Append(" AND id = @Id");
            //    param.Add("@Id", req.Id);
            //}
            //if (!string.IsNullOrWhiteSpace(req?.Initialism))
            //{
            //    condition.Append(" AND initialism = @Initialism");
            //    param.Add("@Initialism", req.Initialism);
            //}
            //var result = await GetListPagedAsync(req.PageIndex, req.PageSize, condition.ToString(), orderBy, param);
            //return result; 
            #endregion
        }

        public async Task<ApplicationDO> GetApplicationById(long id)
        {
            return await GetApplication(new AppReqDTO { Id = id });
        }

        public async Task<ApplicationDO> GetApplicationByName(string name)
        {
            return await GetApplication(new AppReqDTO { Name = name });
        }

        public async Task<ApplicationDO> GetApplicationByInitialism(string initialism)
        {
            return await GetApplication(new AppReqDTO { Initialism = initialism });
        }

        public async Task<ApplicationDO> GetApplicationByRoute(string route)
        {
            return await GetApplication(new AppReqDTO { Route = route });
        }

        public async Task<ApplicationDO> GetApplication(AppReqDTO req)
        {
            ApplicationDO res;

            var param = new DynamicParameters();
            var sql = "WHERE is_deleted = 0";
            if (req.Id > 0)
            {
                sql += " AND id = @id";
                param.Add("@id", req.Id);
            }
            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                sql += " AND name = @name";
                param.Add("@name", req.Name);
            }
            if (!string.IsNullOrWhiteSpace(req.Initialism))
            {
                sql += " AND initialism = @initialism";
                param.Add("@initialism", req.Initialism);
            }
            if (!string.IsNullOrWhiteSpace(req.Route))
            {
                sql += " AND route = @route";
                param.Add("@route", req.Route);
            }

            var list = await GetListAsync(sql, param);
            res = list.FirstOrDefault();

            return res;

            //var param = new DynamicParameters();
            //param.Add("@AppId", req.AppId);

            //var sql = @"SELECT id, name, initialism, route, is_deleted isdeleted, create_by createby, create_time createtime, update_by updateby, update_time updatetime
            //            FROM application 
            //            WHERE is_deleted = 0 and id = @AppId";
            ////await OpenSlaveConnectionAsync(async conn =>
            //await OpenConnectionAsync(async conn =>
            //{
            //    res = await conn.QueryFirstOrDefaultAsync<ApplicationDO>(sql, param);
            //});
            //return res;
        }

        public async Task<List<ApplicationDO>> GetApplicationListAnyCondition(AppReqDTO req)
        {
            var param = new DynamicParameters();

            var condition = "";
            if (req.Id > 0)
            {
                condition += "OR id = @id ";
                param.Add("@id", req.Id);
            }
            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                condition += "OR name = @name ";
                param.Add("@name", req.Name);
            }
            if (!string.IsNullOrWhiteSpace(req.Initialism))
            {
                condition += "OR initialism = @initialism ";
                param.Add("@initialism", req.Initialism);
            }
            if (!string.IsNullOrWhiteSpace(req.Route))
            {
                condition += "OR route = @route ";
                param.Add("@route", req.Route);
            }

            var whereClause = string.IsNullOrWhiteSpace(condition)
                ? "WHERE is_deleted = 0"
                : $"WHERE is_deleted = 0 AND ({condition.Substring(2)})";

            var sql = @"SELECT id, name, initialism, route, is_deleted isdeleted, create_by createby, create_time createtime, update_by updateby, update_time updatetime
                        FROM application "
                      + whereClause;

            var res = new List<ApplicationDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                var enumerable = await conn.QueryAsync<ApplicationDO>(sql, param);
                res = enumerable.ToList();
            });
            return res;

            #region MyRegion

            //var param = new DynamicParameters();
            //var condition = "";
            //if (req.Id > 0)
            //{
            //    condition += " OR id = @id";
            //    param.Add("@id", req.Id);
            //}
            //if (!string.IsNullOrWhiteSpace(req.Name))
            //{
            //    condition += " OR name = @name";
            //    param.Add("@name", req.Name);
            //}
            //if (!string.IsNullOrWhiteSpace(req.Initialism))
            //{
            //    condition += " OR initialism = @initialism";
            //    param.Add("@initialism", req.Initialism);
            //}
            //if (!string.IsNullOrWhiteSpace(req.Route))
            //{
            //    condition += " OR route = @route";
            //    param.Add("@route", req.Route);
            //}

            //var sql = string.IsNullOrWhiteSpace(condition)
            //    ? "WHERE is_deleted = 0"
            //    : $"WHERE is_deleted = 0 AND ({condition.Substring(2)})";

            //var list = await GetListAsync(sql, param);
            //var res = list.FirstOrDefault();

            //return res; 

            #endregion 

        }

        public async Task<List<ApplicationDO>> GetApplicationList()
        {
            IEnumerable<ApplicationDO> result;

            IEnumerable<ApplicationDO> res = new List<ApplicationDO>();
            var sql = @"SELECT id, name, initialism, route, is_deleted isdeleted, create_by createby, create_time createtime, update_by updateby, update_time updatetime
                        FROM application
                        ORDER BY is_deleted ASC ";
            await OpenSlaveConnectionAsync(async conn =>
            {
                res = await conn.QueryAsync<ApplicationDO>(sql);
            });

            return res.ToList();

            #region MyRegion
            var condition = new StringBuilder();

            //TODO: Get all application infos
            //condition.Append("WHERE is_deleted = 0");

            try
            {
                result = await GetListAsync(condition.ToString());
            }
            catch (Exception e)
            {
                logger.Error(CommonConstant.ExceptionOccured, e);
                throw;
            }
            return result.ToList();
            #endregion
        }


        #endregion 接口实现

        #region 私有方法

        #endregion 私有方法


    }
}
