using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Data.DapperExtensions;
using ApolloErp.Log;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Common.Exceptions;
using Ae.Shop.Service.Common.Extension;
using Ae.Shop.Service.Core.Enums;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;

namespace Ae.Shop.Service.Dal.Repositorys
{
    public class EmployeeRepository : AbstractRepository<EmployeeDO>, IEmployeeRepository
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private IConfiguration configuration;
        private readonly string qiNiuImageDomain;
        private readonly long technicanJobId;

        private readonly ApolloErpLogger<EmployeeRepository> logger;
        private readonly string className;

        public EmployeeRepository(ApolloErpLogger<EmployeeRepository> logger, IConfiguration configuration)
        {
            this.configuration = configuration;
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");

            this.logger = logger;
            className = this.GetType().Name;

            qiNiuImageDomain = configuration["QiNiuImageDomain"]?.ToString() ?? "";

            var jobId = configuration["ShopManageBiz:TechnicianJobId"];
            if (!long.TryParse(jobId, out technicanJobId))
            {
                technicanJobId = CommonConstant.Three;
            }
        }

        // ---------------------------------- 接口实现 --------------------------------------
        #region ！！！登录接口相关，请勿轻易对其修改！！！

        public async Task<List<EmployeeListDTO>> GetEmpAndOrgListForLoginByAccountIdAsync(EmployeeListReqDTO req)
        {
            List<EmployeeListDTO> res = new List<EmployeeListDTO>();

            try
            {
                var param = new DynamicParameters();

                #region Where Clause

                var whereClause = "";
                if (req.IsDeleted >= 0)
                {
                    param.Add("@isDeleted", req.IsDeleted);
                    whereClause += "WHERE e.is_deleted = @isDeleted ";
                }
                else
                {
                    whereClause += "WHERE (e.is_deleted = 0 OR e.is_deleted = 1) ";
                }
                if (req.AccountId.IsNotNullOrWhiteSpace())
                {
                    param.Add("@AccountId", req.AccountId);
                    whereClause += "AND e.account_id = @AccountId ";
                }
                if (req.OrganizationId > 0)
                {
                    param.Add("@OrganizationId", req.OrganizationId);
                    whereClause += "AND e.organization_id = @OrganizationId ";
                }
                if (req.EmployeeType != EmployeeType.None)
                {
                    param.Add("@EmployeeType", req.EmployeeType);
                    whereClause += "AND e.type = @EmployeeType ";
                }

                //门店下架，不允许登录；
                var validShopWhrCla = " AND s.online != 0 ";

                #endregion Where Clause

                var sql = @"SELECT * FROM
                                (
	                                SELECT e.id, e.account_id accountId, e.name,e.mobile, e.type employeeType, e.organization_id organizationId, c.name organizationName, c.simple_name organizationSimpleName, -1 shopType,  
                                    CONCAT(c.province, c.city, c.district, c.address) organizationAddress,
                                    IFNULL(e.department_id, 0) DepartmentId, IFNULL(d.name,'') departmentName, 
                                    IFNULL(e.job_id, 0) JobId, IFNULL(j.name,'') jobName, 	                                
                                    e.is_deleted IsDeleted, e.create_by createby, e.create_time createtime, e.update_by updateby, e.update_time updatetime
	                                FROM employee e
	                                INNER JOIN company c ON (e.organization_id = c.id AND e.type = c.type)
	                                LEFT JOIN department d ON e.department_id = d.id
	                                LEFT JOIN job j ON e.job_id = j.id "
                             + whereClause
                             + @" UNION ALL
	                                SELECT e.id, e.account_id accountId, e.name,e.mobile, e.type employeeType, e.organization_id organizationId, s.full_name organizationName, s.simple_name organizationSimpleName, s.type shopType, 
                                    CONCAT(s.province, s.city, s.district, s.address) organizationAddress,
                                    IFNULL(e.department_id, 0) DepartmentId, IFNULL(d.name,'') departmentName, 
                                    IFNULL(e.job_id, 0) JobId, IFNULL(j.name,'') jobName, 	
	                                e.is_deleted IsDeleted, e.create_by createby, e.create_time createtime, e.update_by updateby, e.update_time updatetime
	                                FROM employee e
	                                INNER JOIN shop s ON (e.organization_id = s.id AND e.type = 1)
	                                LEFT JOIN department d ON e.department_id = d.id
	                                LEFT JOIN job j ON e.job_id = j.id "
                             + whereClause + validShopWhrCla
                             + @" 
                                ) t
                                ORDER BY createtime DESC ";

                await OpenSlaveConnectionAsync(async conn =>
                 {
                     res = (await conn.QueryAsync<EmployeeListDTO>(sql, param)).ToList();

                     if (res != null && res.Any())
                     {
                         #region 获取所属单位Image

                         string imgSql;
                         List<EmployeePageDTO> imgList;
                         var orgIdList = res.Select(s => s.OrganizationId);
                         var orgIdStr = $"({string.Join(',', orgIdList)})";
                         if (req.EmployeeType != EmployeeType.None)
                         {
                             if (req.EmployeeType == EmployeeType.Shop)
                             {
                                 imgSql = @"SELECT shop_id organizationId, img_url organizationImage FROM shop_img
                                           WHERE is_deleted = 0 AND type = 1 "
                                         + $"AND shop_id IN {orgIdStr}";
                             }
                             else
                             {
                                 imgSql = @"SELECT company_id organizationId, img_url organizationImage FROM company_img
                                           WHERE is_deleted = 0 AND type = 1 "
                                          + $"AND company_id IN {orgIdStr}";
                             }

                             imgList = (await conn.QueryAsync<EmployeePageDTO>(imgSql)).ToList();
                             var imgDic = new Dictionary<int, string>();
                             if (imgList.Any())
                             {
                                 imgList.ForEach(f =>
                                 {
                                     if (imgDic.ContainsKey(f.OrganizationId))
                                     {
                                         imgDic[f.OrganizationId] = f.OrganizationImage;
                                     }
                                     else
                                     {
                                         imgDic.Add(f.OrganizationId, f.OrganizationImage);
                                     }
                                 });
                                 foreach (var obj in res)
                                 {
                                     obj.OrganizationImage = imgDic.ContainsKey(obj.OrganizationId)
                                         ? $"{qiNiuImageDomain}{imgDic[obj.OrganizationId]}"
                                         : $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                 }
                             }
                             else
                             {
                                 foreach (var obj in res)
                                 {
                                     obj.OrganizationImage = $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                 }
                             }
                         }
                         else
                         {
                             imgSql = @"SELECT * FROM
                                    (  
                                        SELECT company_id organizationId, img_url organizationImage, e.type employeeType
                                        FROM employee e
                                        INNER JOIN company_img ci ON (e.organization_id = ci.company_id AND e.type != 1)
                                        WHERE ci.is_deleted = 0 AND ci.type = 1 "
                                     + $"AND ci.company_id IN {orgIdStr} " +
                                      @"UNION ALL
                                       SELECT shop_id organizationId, img_url organizationImage, e.type employeeType
                                       FROM employee e
                                       INNER JOIN shop_img si ON (e.organization_id = si.shop_id AND e.type = 1)
                                       WHERE si.is_deleted = 0 AND si.type = 1 "
                                     + $"AND si.shop_id IN {orgIdStr} " +
                                    ") t";
                             imgList = (await conn.QueryAsync<EmployeePageDTO>(imgSql))
                                .GroupBy(g => new { g.OrganizationId, g.OrganizationImage, g.EmployeeType })
                                .Select(s => s.First())
                                .ToList();
                             var imgDic = new Dictionary<string, string>();
                             if (imgList.Any())
                             {
                                 imgList.ForEach(f =>
                                 {
                                     var key = string.Join("|", f.OrganizationId, f.EmployeeType);
                                     if (imgDic.ContainsKey(key))
                                     {
                                         imgDic[key] = f.OrganizationImage;
                                     }
                                     else
                                     {
                                         imgDic.Add(key, f.OrganizationImage);
                                     }
                                 });
                                 foreach (var obj in res)
                                 {
                                     var objKey = string.Join("|", obj.OrganizationId, obj.EmployeeType);
                                     obj.OrganizationImage = imgDic.ContainsKey(objKey)
                                         ? $"{qiNiuImageDomain}{imgDic[objKey]}"
                                         : $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                 }
                             }
                             else
                             {
                                 foreach (var obj in res)
                                 {
                                     obj.OrganizationImage = $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                 }
                             }
                         }

                         #endregion 获取所属单位Image
                     }
                 });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }

            //logger.Info($"Repo: {className}.GetEmpAndOrgListForLoginByAccountIdAsync 返回值：TotalItems:{res.Count()}, Items:{JsonConvert.SerializeObject(res)}");

            return res.ToList();
        }

        public async Task<PagedEntity<EmployeePageDTO>> GetEmpAndOrgPageForLoginByAccountIdAsync(EmployeePageForLoginListReqDTO req)
        {
            PagedEntity<EmployeePageDTO> res = new PagedEntity<EmployeePageDTO> { Items = new List<EmployeePageDTO>() };
            IEnumerable<EmployeePageDTO> enumerable = new List<EmployeePageDTO>();
            var total = 0;

            try
            {
                var param = new DynamicParameters();
                param.Add("@index", (req.PageIndex - 1) * req.PageSize);
                param.Add("@size", req.PageSize);

                #region Where Clause

                var whereClause = "";
                var comWhrClause = "";
                var shopWhrClause = "";
                if (req.IsDeleted >= 0)
                {
                    param.Add("@isDeleted", req.IsDeleted);
                    whereClause += "WHERE e.is_deleted = @isDeleted ";
                }
                else
                {
                    whereClause += "WHERE (e.is_deleted = 0 OR e.is_deleted = 1) ";
                }

                if (req.AccountId.IsNotNullOrWhiteSpace())
                {
                    param.Add("@AccountId", req.AccountId);
                    whereClause += "AND e.account_id = @AccountId ";
                }

                if (req.EmployeeType != EmployeeType.None)
                {
                    param.Add("@EmployeeType", req.EmployeeType);
                    whereClause += "AND e.type = @EmployeeType ";

                    if (req.OrganizationName.IsNotNullOrWhiteSpace())
                    {
                        if (req.EmployeeType == EmployeeType.Shop)
                        {
                            param.Add("@OrganizationName", $"%{req.OrganizationName}%");
                            shopWhrClause += "AND s.simple_name LIKE @OrganizationName ";
                        }
                        else //if (reqDto.EmployeeType == EmployeeType.Company)
                        {
                            param.Add("@OrganizationName", $"%{req.OrganizationName}%");
                            comWhrClause += "AND c.name LIKE @OrganizationName ";
                        }
                    }
                }
                else
                {
                    if (req.OrganizationName.IsNotNullOrWhiteSpace())
                    {
                        param.Add("@OrganizationName", $"%{req.OrganizationName}%");
                        shopWhrClause += "AND s.simple_name LIKE @OrganizationName ";

                        param.Add("@OrganizationName", $"%{req.OrganizationName}%");
                        comWhrClause += "AND c.name LIKE @OrganizationName ";
                    }
                }

                //门店下架，不允许登录；
                var validShopWhrCla = " AND s.online != 0 ";

                #endregion Where Clause

                string sqlCount;
                if (req.EmployeeType == EmployeeType.None)
                {
                    sqlCount = @"SELECT e.id FROM employee e " + whereClause;
                }
                else
                {
                    if (req.EmployeeType == EmployeeType.Shop)
                    {
                        sqlCount = @"SELECT e.id FROM employee e 
	                                 INNER JOIN shop s ON e.organization_id = s.id "
                                   + whereClause + shopWhrClause + validShopWhrCla;
                    }
                    else //if (reqDto.EmployeeType == EmployeeType.Company)
                    {
                        sqlCount = @"SELECT e.id FROM employee e 
                                     INNER JOIN company c ON e.organization_id = c.id 
	                             " + whereClause + comWhrClause;
                    }
                }

                await OpenSlaveConnectionAsync(async conn =>
                {
                    var list = await conn.QueryAsync<string>(sqlCount, param);
                    total = list.Distinct().Count();

                    if (total > 0)
                    {
                        var sql = @"SELECT * FROM
                                (
	                                SELECT e.id, e.account_id accountId, e.name,e.mobile, e.type employeeType, e.organization_id organizationId, c.name organizationName, c.simple_name organizationSimpleName, -1 shopType,  
                                    CONCAT(c.province, c.city, c.district, c.address) organizationAddress,
                                    IFNULL(e.department_id, 0) DepartmentId, IFNULL(d.name,'') departmentName, 
                                    IFNULL(e.job_id, 0) JobId, IFNULL(j.name,'') jobName, 
	                                e.is_deleted IsDeleted, e.create_by createby, e.create_time createtime, e.update_by updateby, e.update_time updatetime
	                                FROM employee e
	                                INNER JOIN company c ON (e.organization_id = c.id AND e.type = c.type)
	                                LEFT JOIN department d ON e.department_id = d.id
	                                LEFT JOIN job j ON e.job_id = j.id "
                                  + whereClause + comWhrClause
                                  + @" UNION ALL
	                                SELECT e.id, e.account_id accountId, e.name,e.mobile, e.type employeeType, e.organization_id organizationId, s.full_name organizationName, s.simple_name organizationSimpleName, s.type shopType, 
                                    CONCAT(s.province, s.city, s.district, s.address) organizationAddress,
                                    IFNULL(e.department_id, 0) DepartmentId, IFNULL(d.name,'') departmentName, 
                                    IFNULL(e.job_id, 0) JobId, IFNULL(j.name,'') jobName, 	
	                                e.is_deleted IsDeleted, e.create_by createby, e.create_time createtime, e.update_by updateby, e.update_time updatetime
	                                FROM employee e
	                                INNER JOIN shop s ON (e.organization_id = s.id AND e.type = 1)
	                                LEFT JOIN department d ON e.department_id = d.id
	                                LEFT JOIN job j ON e.job_id = j.id "
                                  + whereClause + shopWhrClause + validShopWhrCla
                                  + @" 
                                ) t
                                ORDER BY createtime DESC 
                                LIMIT @index, @size ";

                        enumerable = await conn.QueryAsync<EmployeePageDTO>(sql, param);
                        res.Items = enumerable.Distinct().ToList();
                        res.TotalItems = total;

                        #region 获取所属单位Image

                        if (res.Items != null && res.Items.Any())
                        {
                            string imgSql;
                            List<EmployeePageDTO> imgList;
                            var orgIdList = res.Items.Select(s => s.OrganizationId);
                            var orgIdStr = $"({string.Join(',', orgIdList)})";
                            if (req.EmployeeType != EmployeeType.None)
                            {
                                if (req.EmployeeType == EmployeeType.Shop)
                                {
                                    imgSql = @"SELECT shop_id organizationId, img_url organizationImage FROM shop_img
                                                WHERE is_deleted = 0 AND type = 1 "
                                             + $"AND shop_id IN {orgIdStr}";
                                }
                                else
                                {
                                    imgSql =
                                        @"SELECT company_id organizationId, img_url organizationImage FROM company_img
                                           WHERE is_deleted = 0 AND type = 1 "
                                        + $"AND company_id IN {orgIdStr}";
                                }

                                imgList = (await conn.QueryAsync<EmployeePageDTO>(imgSql)).ToList();
                                var imgDic = new Dictionary<int, string>();
                                if (imgList.Any())
                                {
                                    imgList.ForEach(f =>
                                    {
                                        if (imgDic.ContainsKey(f.OrganizationId))
                                        {
                                            imgDic[f.OrganizationId] = f.OrganizationImage;
                                        }
                                        else
                                        {
                                            imgDic.Add(f.OrganizationId, f.OrganizationImage);
                                        }
                                    });
                                    foreach (var obj in res.Items)
                                    {
                                        obj.OrganizationImage = imgDic.ContainsKey(obj.OrganizationId)
                                            ? $"{qiNiuImageDomain}{imgDic[obj.OrganizationId]}"
                                            : $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                    }
                                }
                                else
                                {
                                    foreach (var obj in res.Items)
                                    {
                                        obj.OrganizationImage = $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                    }
                                }
                            }
                            else
                            {
                                imgSql = @"SELECT * FROM
                                        (  
                                            SELECT company_id organizationId, img_url organizationImage, e.type employeeType
                                            FROM employee e
                                            INNER JOIN company_img ci ON (e.organization_id = ci.company_id AND e.type != 1)
                                            WHERE ci.is_deleted = 0 AND ci.type = 1 "
                                         + $"AND ci.company_id IN {orgIdStr} " +

                                           @"UNION ALL

                                           SELECT shop_id organizationId, img_url organizationImage, e.type employeeType
                                           FROM employee e
                                           INNER JOIN shop_img si ON (e.organization_id = si.shop_id AND e.type = 1)
                                           WHERE si.is_deleted = 0 AND si.type = 1 "
                                         + $"AND si.shop_id IN {orgIdStr} " +

                                        ") t";
                                imgList = (await conn.QueryAsync<EmployeePageDTO>(imgSql))
                                    .GroupBy(g => new { g.OrganizationId, g.OrganizationImage, g.EmployeeType })
                                    .Select(s => s.FirstOrDefault())
                                    .ToList();
                                var imgDic = new Dictionary<string, string>();
                                if (imgList.Any())
                                {
                                    imgList.ForEach(f =>
                                    {
                                        var key = string.Join("|", f.OrganizationId, f.EmployeeType);
                                        if (imgDic.ContainsKey(key))
                                        {
                                            imgDic[key] = f.OrganizationImage;
                                        }
                                        else
                                        {
                                            imgDic.Add(key, f.OrganizationImage);
                                        }
                                    });
                                    foreach (var obj in res.Items)
                                    {
                                        var objKey = string.Join("|", obj.OrganizationId, obj.EmployeeType);
                                        obj.OrganizationImage = imgDic.ContainsKey(objKey)
                                            ? $"{qiNiuImageDomain}{imgDic[objKey]}"
                                            : $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                    }
                                }
                                else
                                {
                                    foreach (var obj in res.Items)
                                    {
                                        obj.OrganizationImage = $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                    }
                                }
                            }
                        }

                        #endregion 获取所属单位Image
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
                //logger.Info($"Repo: {className}.GetEmpAndOrgPageForLoginByAccountIdAsync 请求参数：{JsonConvert.SerializeObject(req)}");
                //logger.Info($"Repo: {className}.GetEmpAndOrgPageForLoginByAccountIdAsync 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        public async Task<PagedEntity<EmployeePageDTO>> GetEmpAndOrgPageForLoginByAccountIdExcCurOrgIdAsync(EmployeePageForLoginListReqDTO req)
        {
            PagedEntity<EmployeePageDTO> res = new PagedEntity<EmployeePageDTO>();
            IEnumerable<EmployeePageDTO> enumerable = new List<EmployeePageDTO>();
            var total = 0;

            try
            {
                var param = new DynamicParameters();
                param.Add("@index", (req.PageIndex - 1) * req.PageSize);
                param.Add("@size", req.PageSize);

                #region Where Clause

                var whereClause = "";
                var comWhrClause = "";
                var shopWhrClause = "";
                if (req.IsDeleted >= 0)
                {
                    param.Add("@isDeleted", req.IsDeleted);
                    param.Add("@organizationId", req.OrganizationId);
                    whereClause += "WHERE e.is_deleted = @isDeleted AND e.organization_id != @organizationId ";
                }
                else
                {
                    param.Add("@organizationId", req.OrganizationId);
                    whereClause +=
                        "WHERE (e.is_deleted = 0 OR e.is_deleted = 1) AND e.organization_id != @organizationId ";
                }

                if (req.AccountId.IsNotNullOrWhiteSpace())
                {
                    param.Add("@AccountId", req.AccountId);
                    whereClause += "AND e.account_id = @AccountId ";
                }

                if (req.EmployeeType != EmployeeType.None)
                {
                    if (req.EmployeeType == EmployeeType.Shop)
                    {
                        param.Add("@EmployeeType", req.EmployeeType);
                        whereClause += "AND e.type = @EmployeeType ";
                    }
                    else
                    {
                        param.Add("@EmployeeType", new List<int>() { EmployeeType.Company.ToInt(), EmployeeType.Extend.ToInt(), EmployeeType.Supplier.ToInt() });
                        whereClause += "AND e.type in @EmployeeType ";
                    }
                    if (req.OrganizationName.IsNotNullOrWhiteSpace())
                    {
                        if (req.EmployeeType == EmployeeType.Shop)
                        {
                            param.Add("@OrganizationName", $"%{req.OrganizationName}%");
                            shopWhrClause += "AND s.simple_name LIKE @OrganizationName ";
                        }
                        else //if (reqDto.EmployeeType == EmployeeType.Company)
                        {
                            param.Add("@OrganizationName", $"%{req.OrganizationName}%");
                            comWhrClause += "AND c.name LIKE @OrganizationName ";
                        }
                    }
                }
                else
                {
                    if (req.OrganizationName.IsNotNullOrWhiteSpace())
                    {
                        param.Add("@OrganizationName", $"%{req.OrganizationName}%");
                        shopWhrClause += "AND s.simple_name LIKE @OrganizationName ";

                        param.Add("@OrganizationName", $"%{req.OrganizationName}%");
                        comWhrClause += "AND c.name LIKE @OrganizationName ";
                    }
                }

                //门店下架，不允许登录；
                var validShopWhrCla = " AND s.online != 0 ";

                #endregion Where Clause

                string sqlCount;
                if (req.EmployeeType == EmployeeType.None)
                {
                    sqlCount = @"SELECT e.id FROM employee e " + whereClause;
                }
                else
                {
                    if (req.EmployeeType == EmployeeType.Shop)
                    {
                        sqlCount = @"SELECT e.id FROM employee e 
	                                 INNER JOIN shop s ON e.organization_id = s.id "
                                   + whereClause + shopWhrClause + validShopWhrCla;
                    }
                    else //if (reqDto.EmployeeType == EmployeeType.Company)
                    {
                        sqlCount = @"SELECT e.id FROM employee e 
                                     INNER JOIN company c ON e.organization_id = c.id 
	                             " + whereClause + comWhrClause;
                    }
                }

                await OpenSlaveConnectionAsync(async conn =>
                {
                    var list = await conn.QueryAsync<string>(sqlCount, param);
                    total = list.Distinct().Count();

                    if (total > 0)
                    {
                        //var sql = @"SELECT * FROM
                        //        (
                        //         SELECT e.id, e.account_id accountId, e.name, e.type employeeType, e.organization_id organizationId, c.name organizationName, c.simple_name organizationSimpleName, -1 shopType,  
                        //            CONCAT(c.province, c.city, c.district, c.address) organizationAddress,
                        //            IFNULL(e.department_id, 0) DepartmentId, IFNULL(d.name,'') departmentName, 
                        //            IFNULL(e.job_id, 0) JobId, IFNULL(j.name,'') jobName, 	
                        //         e.is_deleted IsDeleted, e.create_by createby, e.create_time createtime, e.update_by updateby, e.update_time updatetime
                        //         FROM employee e
                        //         INNER JOIN company c ON (e.organization_id = c.id AND e.type = c.type)
                        //         LEFT JOIN department d ON e.department_id = d.id
                        //         LEFT JOIN job j ON e.job_id = j.id "
                        //          + whereClause + comWhrClause
                        //          + @" UNION ALL
                        //         SELECT e.id, e.account_id accountId, e.name, e.type employeeType, e.organization_id organizationId, s.full_name organizationName, s.simple_name organizationSimpleName, s.type shopType, 
                        //            CONCAT(s.province, s.city, s.district, s.address) organizationAddress,
                        //            IFNULL(e.department_id, 0) DepartmentId, IFNULL(d.name,'') departmentName, 
                        //            IFNULL(e.job_id, 0) JobId, IFNULL(j.name,'') jobName, 	
                        //         e.is_deleted IsDeleted, e.create_by createby, e.create_time createtime, e.update_by updateby, e.update_time updatetime
                        //         FROM employee e
                        //         INNER JOIN shop s ON (e.organization_id = s.id AND e.type = 1)
                        //         LEFT JOIN department d ON e.department_id = d.id
                        //         LEFT JOIN job j ON e.job_id = j.id "
                        //          + whereClause + shopWhrClause + validShopWhrCla
                        //          + @" 
                        //        ) t
                        //        ORDER BY createtime DESC 
                        //        LIMIT @index, @size ";

                        var sql = @"SELECT * FROM
                                (
	                                SELECT e.id, e.account_id accountId, e.name, e.mobile, e.type employeeType, e.organization_id organizationId, c.name organizationName, c.simple_name organizationSimpleName, -1 shopType,  
                                    CONCAT(c.province, c.city, c.district, c.address) organizationAddress,
                                    IFNULL(e.department_id, 0) DepartmentId, IFNULL(d.name,'') departmentName, 
                                    IFNULL(e.job_id, 0) JobId, IFNULL(j.name,'') jobName, 	
	                                e.is_deleted IsDeleted, e.create_by createby, e.create_time createtime, e.update_by updateby, e.update_time updatetime
	                                FROM employee e
	                                INNER JOIN company c ON (e.organization_id = c.id)
	                                LEFT JOIN department d ON e.department_id = d.id
	                                LEFT JOIN job j ON e.job_id = j.id "
                                 + whereClause + comWhrClause
                                 + @" UNION ALL
	                                SELECT e.id, e.account_id accountId, e.name, e.mobile, e.type employeeType, e.organization_id organizationId, s.full_name organizationName, s.simple_name organizationSimpleName, s.type shopType, 
                                    CONCAT(s.province, s.city, s.district, s.address) organizationAddress,
                                    IFNULL(e.department_id, 0) DepartmentId, IFNULL(d.name,'') departmentName, 
                                    IFNULL(e.job_id, 0) JobId, IFNULL(j.name,'') jobName, 	
	                                e.is_deleted IsDeleted, e.create_by createby, e.create_time createtime, e.update_by updateby, e.update_time updatetime
	                                FROM employee e
	                                INNER JOIN shop s ON (e.organization_id = s.id AND e.type = 1)
	                                LEFT JOIN department d ON e.department_id = d.id
	                                LEFT JOIN job j ON e.job_id = j.id "
                                 + whereClause + shopWhrClause + validShopWhrCla
                                 + @" 
                                ) t
                                ORDER BY createtime DESC 
                                LIMIT @index, @size ";

                        enumerable = await conn.QueryAsync<EmployeePageDTO>(sql, param);
                        res.Items = enumerable.Distinct().ToList();
                        res.TotalItems = total;

                        #region 获取所属单位Image

                        if (res.Items != null && res.Items.Any())
                        {
                            string imgSql;
                            List<EmployeePageDTO> imgList;
                            var orgIdList = res.Items.Select(s => s.OrganizationId);
                            var orgIdStr = $"({string.Join(',', orgIdList)})";
                            if (req.EmployeeType != EmployeeType.None)
                            {
                                if (req.EmployeeType == EmployeeType.Shop)
                                {
                                    imgSql = @"SELECT shop_id organizationId, img_url organizationImage FROM shop_img
                                                WHERE is_deleted = 0 AND type = 1 "
                                             + $"AND shop_id IN {orgIdStr}";
                                }
                                else
                                {
                                    imgSql =
                                        @"SELECT company_id organizationId, img_url organizationImage FROM company_img
                                                WHERE is_deleted = 0 AND type = 1 "
                                        + $"AND company_id IN {orgIdStr}";
                                }

                                imgList = (await conn.QueryAsync<EmployeePageDTO>(imgSql)).ToList();
                                var imgDic = new Dictionary<int, string>();
                                if (imgList.Any())
                                {
                                    imgList.ForEach(f =>
                                    {
                                        if (imgDic.ContainsKey(f.OrganizationId))
                                        {
                                            imgDic[f.OrganizationId] = f.OrganizationImage;
                                        }
                                        else
                                        {
                                            imgDic.Add(f.OrganizationId, f.OrganizationImage);
                                        }
                                    });
                                    foreach (var obj in res.Items)
                                    {
                                        obj.OrganizationImage = imgDic.ContainsKey(obj.OrganizationId)
                                            ? $"{qiNiuImageDomain}{imgDic[obj.OrganizationId]}"
                                            : $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                    }
                                }
                                else
                                {
                                    foreach (var obj in res.Items)
                                    {
                                        obj.OrganizationImage = $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                    }
                                }
                            }
                            else
                            {
                                imgSql = @"SELECT * FROM
                                       (  
                                            SELECT company_id organizationId, img_url organizationImage, e.type employeeType
                                            FROM employee e
                                            INNER JOIN company_img ci ON (e.organization_id = ci.company_id AND e.type != 1)
                                            WHERE ci.is_deleted = 0 AND ci.type = 1 "
                                         + $"AND ci.company_id IN {orgIdStr} " +

                                         @"UNION ALL

                                           SELECT shop_id organizationId, img_url organizationImage, e.type employeeType
                                           FROM employee e
                                           INNER JOIN shop_img si ON (e.organization_id = si.shop_id AND e.type = 1)
                                           WHERE si.is_deleted = 0 AND si.type = 1 "
                                         + $"AND si.shop_id IN {orgIdStr} " +
                                         ") t";
                                imgList = (await conn.QueryAsync<EmployeePageDTO>(imgSql))
                                    .GroupBy(g => new { g.OrganizationId, g.OrganizationImage, g.EmployeeType })
                                    .Select(s => s.First())
                                    .ToList();
                                var imgDic = new Dictionary<string, string>();
                                if (imgList.Any())
                                {
                                    imgList.ForEach(f =>
                                    {
                                        var key = string.Join("|", f.OrganizationId, f.EmployeeType);
                                        if (imgDic.ContainsKey(key))
                                        {
                                            imgDic[key] = f.OrganizationImage;
                                        }
                                        else
                                        {
                                            imgDic.Add(key, f.OrganizationImage);
                                        }
                                    });
                                    foreach (var obj in res.Items)
                                    {
                                        var objKey = string.Join("|", obj.OrganizationId, obj.EmployeeType);
                                        obj.OrganizationImage = imgDic.ContainsKey(objKey)
                                            ? $"{qiNiuImageDomain}{imgDic[objKey]}"
                                            : $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                    }
                                }
                                else
                                {
                                    foreach (var obj in res.Items)
                                    {
                                        obj.OrganizationImage = $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                    }
                                }
                            }
                        }

                        #endregion 获取所属单位Image
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
                //logger.Info($"Repo: {className}.GetEmpAndOrgPageForLoginByAccountIdExcCurOrgIdAsync 返回值：{JsonConvert.SerializeObject(res)}");
                //logger.Info($"Repo: {className}.GetEmpAndOrgPageForLoginByAccountIdExcCurOrgIdAsync 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        public async Task<PagedEntity<EmployeePageDTO>> GetEmpAndOrgPageForLoginByEmpIdAsync(EmployeePageForLoginListByTokenReqDTO req)
        {
            PagedEntity<EmployeePageDTO> res = new PagedEntity<EmployeePageDTO>();
            IEnumerable<EmployeePageDTO> enumerable = new List<EmployeePageDTO>();

            try
            {
                EmployeePageDTO resDo = new EmployeePageDTO();
                var paramEmp = new DynamicParameters();
                paramEmp.Add("@EmployeeId", req.EmployeeId);
                var empSql = "SELECT id, account_id AccountId FROM employee WHERE id = @EmployeeId";
                await OpenSlaveConnectionAsync(async conn =>
                {
                    resDo = await conn.QueryFirstOrDefaultAsync<EmployeePageDTO>(empSql, paramEmp);
                });
                if (null == resDo || resDo.AccountId.IsNullOrWhiteSpace())
                {
                    return res;
                }

                res = await GetEmpAndOrgPageForLoginByAccountIdAsync(new EmployeePageForLoginListReqDTO
                {
                    AccountId = resDo.AccountId,
                    EmployeeType = req.EmployeeType,
                    OrganizationName = req.OrganizationName,
                    IsDeleted = Convert.ToInt32(resDo.IsDeleted),
                    PageIndex = req.PageIndex,
                    PageSize = req.PageSize
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            finally
            {
                //logger.Info($"Repo: {className}.GetEmpAndOrgPageForLoginByEmpIdAsync 请求参数：{JsonConvert.SerializeObject(req)}");
                //logger.Info($"Repo: {className}.GetEmpAndOrgPageForLoginByEmpIdAsync 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        public async Task<PagedEntity<EmployeePageDTO>> GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync(EmployeePageForLoginListByTokenReqDTO req)
        {
            PagedEntity<EmployeePageDTO> res = new PagedEntity<EmployeePageDTO>();
            IEnumerable<EmployeePageDTO> enumerable = new List<EmployeePageDTO>();

            try
            {
                EmployeePageDTO resDo = new EmployeePageDTO();
                var paramEmp = new DynamicParameters();
                paramEmp.Add("@EmployeeId", req.EmployeeId);
                var empSql = "SELECT id, account_id AccountId FROM employee WHERE id = @EmployeeId";
                await OpenSlaveConnectionAsync(async conn =>
                {
                    resDo = await conn.QueryFirstOrDefaultAsync<EmployeePageDTO>(empSql, paramEmp);
                });
                if (null == resDo || resDo.AccountId.IsNullOrWhiteSpace())
                {
                    return res;
                }

                res = await GetEmpAndOrgPageForLoginByAccountIdExcCurOrgIdAsync(new EmployeePageForLoginListReqDTO
                {
                    AccountId = resDo.AccountId,
                    EmployeeType = req.EmployeeType,
                    OrganizationId = req.OrganizationId,
                    OrganizationName = req.OrganizationName,
                    IsDeleted = Convert.ToInt32(resDo.IsDeleted),
                    PageIndex = req.PageIndex,
                    PageSize = req.PageSize
                });
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }
            finally
            {
                //logger.Info($"Repo: {className}.GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync 请求参数：{JsonConvert.SerializeObject(req)}");
                //logger.Info($"Repo: {className}.GetEmpAndOrgPageForLoginByEmpIdExcCurOrgIdAsync 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        public async Task<PagedEntity<EmployeePageDTO>> GetOrgRangePageForLoginByEmpIdExcCurOrgId(EmployeePageForLoginListByTokenReqDTO req)
        {
            PagedEntity<EmployeePageDTO> res = new PagedEntity<EmployeePageDTO>();
            IEnumerable<EmployeePageDTO> enumerable;
            var total = 0;

            try
            {
                var param = new DynamicParameters();
                param.Add("@index", (req.PageIndex - 1) * req.PageSize);
                param.Add("@size", req.PageSize);

                #region Where Clause

                var whereClause = "";
                var comWhrClause = "";
                var shopWhrClause = "";

                if (req.EmployeeId.IsNotNullOrWhiteSpace())
                {
                    param.Add("@EmployeeId", req.EmployeeId);
                    whereClause += "AND eor.employee_id = @EmployeeId ";
                }

                if (req.OrganizationId > 0)
                {
                    param.Add("@organizationId", req.OrganizationId);
                    whereClause += "AND eor.organization_id != @organizationId ";
                }

                if (req.EmployeeType != EmployeeType.None)
                {
                    param.Add("@EmployeeType", req.EmployeeType);
                    whereClause += "AND eor.type = @EmployeeType ";

                    if (req.OrganizationName.IsNotNullOrWhiteSpace())
                    {
                        if (req.EmployeeType == EmployeeType.Shop)
                        {
                            param.Add("@OrganizationName", $"%{req.OrganizationName}%");
                            shopWhrClause += "AND s.simple_name LIKE @OrganizationName ";
                        }
                        else //if (reqDto.EmployeeType == EmployeeType.Company)
                        {
                            param.Add("@OrganizationName", $"%{req.OrganizationName}%");
                            comWhrClause += "AND c.name LIKE @OrganizationName ";
                        }
                    }
                }
                else
                {
                    if (req.OrganizationName.IsNotNullOrWhiteSpace())
                    {
                        param.Add("@OrganizationName", $"%{req.OrganizationName}%");
                        shopWhrClause += "AND s.simple_name LIKE @OrganizationName ";

                        param.Add("@OrganizationName", $"%{req.OrganizationName}%");
                        comWhrClause += "AND c.name LIKE @OrganizationName ";
                    }
                }

                //门店下架，不允许登录；
                var validShopWhrCla = " AND s.online != 0 ";

                #endregion Where Clause

                string sqlCount;
                if (req.EmployeeType == EmployeeType.None)
                {
                    sqlCount = @"SELECT * FROM
                                (
                                    SELECT eor.organization_id
                                    FROM employee_organization_range eor
                                    INNER JOIN employee e ON eor.employee_id = e.id
	                                INNER JOIN company c ON (eor.organization_id = c.id AND eor.type = c.type)
	                                LEFT JOIN department d ON e.department_id = d.id
	                                LEFT JOIN job j ON e.job_id = j.id 
                                    WHERE eor.is_deleted = 0 AND e.is_deleted = 0 AND c.is_deleted = 0 "
                                  + whereClause + comWhrClause
                                  + @" UNION ALL
	                                SELECT eor.organization_id
                                    FROM employee_organization_range eor
                                    INNER JOIN employee e ON eor.employee_id = e.id
                                    INNER JOIN shop s ON (eor.organization_id = s.id AND eor.type = 1)
	                                LEFT JOIN department d ON e.department_id = d.id
	                                LEFT JOIN job j ON e.job_id = j.id 
                                    WHERE eor.is_deleted = 0 AND e.is_deleted = 0 AND s.is_deleted = 0 "
                                  + whereClause + shopWhrClause + validShopWhrCla
                                  + @" 
                                ) t ";
                }
                else
                {
                    if (req.EmployeeType == EmployeeType.Shop)
                    {
                        sqlCount = @"SELECT eor.organization_id
                                    FROM employee_organization_range eor
                                    INNER JOIN employee e ON eor.employee_id = e.id
                                    INNER JOIN shop s ON (eor.organization_id = s.id AND eor.type = 1)
	                                LEFT JOIN department d ON e.department_id = d.id
	                                LEFT JOIN job j ON e.job_id = j.id 
                                    WHERE eor.is_deleted = 0 AND e.is_deleted = 0 AND s.is_deleted = 0 "
                                   + whereClause + shopWhrClause + validShopWhrCla;
                    }
                    else //if (reqDto.EmployeeType == EmployeeType.Company)
                    {
                        sqlCount = @"SELECT eor.organization_id
                                    FROM employee_organization_range eor
                                    INNER JOIN employee e ON eor.employee_id = e.id
	                                INNER JOIN company c ON (eor.organization_id = c.id AND eor.type = c.type)
	                                LEFT JOIN department d ON e.department_id = d.id
	                                LEFT JOIN job j ON e.job_id = j.id 
                                    WHERE eor.is_deleted = 0 AND e.is_deleted = 0 AND c.is_deleted = 0 "
                                   + whereClause + comWhrClause;
                    }
                }

                await OpenSlaveConnectionAsync(async conn =>
                {
                    var list = await conn.QueryAsync<string>(sqlCount, param);
                    total = list.Distinct().Count();

                    if (total > 0)
                    {
                        var sql = @"SELECT * FROM
                                (
                                    SELECT 1 IsOrganizationRange, e.id, e.account_id accountId, e.name , e.mobile ,  eor.type employeeType, 
                                    eor.organization_id organizationId, c.name organizationName, c.simple_name organizationSimpleName, -1 shopType, 
	                                    CONCAT(c.province, c.city, c.district, c.address) organizationAddress,
	                                    IFNULL(e.department_id, 0) DepartmentId, IFNULL(d.name,'') departmentName, 
	                                    IFNULL(e.job_id, 0) JobId, IFNULL(j.name,'') jobName, 	
                                    e.is_deleted IsDeleted, e.create_by createby, e.create_time createtime, e.update_by updateby, e.update_time updatetime
                                    FROM employee_organization_range eor
                                    INNER JOIN employee e ON eor.employee_id = e.id
	                                INNER JOIN company c ON (eor.organization_id = c.id AND eor.type = c.type)
	                                LEFT JOIN department d ON e.department_id = d.id
	                                LEFT JOIN job j ON e.job_id = j.id 
                                    WHERE eor.is_deleted = 0 AND e.is_deleted = 0 AND c.is_deleted = 0 "
                                  + whereClause + comWhrClause
                                  + @" UNION ALL
	                                SELECT 1 IsOrganizationRange, e.id, e.account_id accountId, e.name , e.mobile , eor.type employeeType, 
                                    eor.organization_id organizationId, s.full_name organizationName, s.simple_name organizationSimpleName, s.type shopType, 
                                        CONCAT(s.province, s.city, s.district, s.address) organizationAddress,
                                        IFNULL(e.department_id, 0) DepartmentId, IFNULL(d.name,'') departmentName, 
                                        IFNULL(e.job_id, 0) JobId, IFNULL(j.name,'') jobName, 	
	                                e.is_deleted IsDeleted, e.create_by createby, e.create_time createtime, e.update_by updateby, e.update_time updatetime
                                    FROM employee_organization_range eor
                                    INNER JOIN employee e ON eor.employee_id = e.id
                                    INNER JOIN shop s ON (eor.organization_id = s.id AND eor.type = 1)
	                                LEFT JOIN department d ON e.department_id = d.id
	                                LEFT JOIN job j ON e.job_id = j.id 
                                    WHERE eor.is_deleted = 0 AND e.is_deleted = 0 AND s.is_deleted = 0 "
                                  + whereClause + shopWhrClause + validShopWhrCla
                                  + @" 
                                ) t
                                ORDER BY createtime DESC 
                                LIMIT @index, @size ";

                        enumerable = await conn.QueryAsync<EmployeePageDTO>(sql, param);
                        res.Items = enumerable.Distinct().ToList();
                        res.TotalItems = total;

                        #region 获取所属单位Image

                        if (res.Items != null && res.Items.Any())
                        {
                            string imgSql;
                            List<EmployeePageDTO> imgList;
                            var orgIdList = res.Items.Select(s => s.OrganizationId);
                            var orgIdStr = $"({string.Join(',', orgIdList)})";
                            if (req.EmployeeType != EmployeeType.None)
                            {
                                if (req.EmployeeType == EmployeeType.Shop)
                                {
                                    imgSql = @"SELECT shop_id organizationId, img_url organizationImage FROM shop_img
                                                WHERE is_deleted = 0 AND type = 1 "
                                             + $"AND shop_id IN {orgIdStr}";
                                }
                                else
                                {
                                    imgSql =
                                        @"SELECT company_id organizationId, img_url organizationImage FROM company_img
                                                WHERE is_deleted = 0 AND type = 1 "
                                        + $"AND company_id IN {orgIdStr}";
                                }

                                imgList = (await conn.QueryAsync<EmployeePageDTO>(imgSql)).ToList();
                                var imgDic = new Dictionary<int, string>();
                                if (imgList.Any())
                                {
                                    imgList.ForEach(f =>
                                    {
                                        if (imgDic.ContainsKey(f.OrganizationId))
                                        {
                                            imgDic[f.OrganizationId] = f.OrganizationImage;
                                        }
                                        else
                                        {
                                            imgDic.Add(f.OrganizationId, f.OrganizationImage);
                                        }
                                    });
                                    foreach (var obj in res.Items)
                                    {
                                        obj.OrganizationImage = imgDic.ContainsKey(obj.OrganizationId)
                                            ? $"{qiNiuImageDomain}{imgDic[obj.OrganizationId]}"
                                            : $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                    }
                                }
                                else
                                {
                                    foreach (var obj in res.Items)
                                    {
                                        obj.OrganizationImage = $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                    }
                                }
                            }
                            else
                            {
                                imgSql = @"SELECT * FROM
                                       (  
                                            SELECT company_id organizationId, img_url organizationImage, e.type employeeType
                                            FROM employee e
                                            INNER JOIN company_img ci ON (e.organization_id = ci.company_id AND e.type != 1)
                                            WHERE ci.is_deleted = 0 AND ci.type = 1 "
                                         + $"AND ci.company_id IN {orgIdStr} " +

                                         @"UNION ALL

                                           SELECT shop_id organizationId, img_url organizationImage, e.type employeeType
                                           FROM employee e
                                           INNER JOIN shop_img si ON (e.organization_id = si.shop_id AND e.type = 1)
                                           WHERE si.is_deleted = 0 AND si.type = 1 "
                                         + $"AND si.shop_id IN {orgIdStr} " +
                                         ") t";
                                imgList = (await conn.QueryAsync<EmployeePageDTO>(imgSql))
                                    .GroupBy(g => new { g.OrganizationId, g.OrganizationImage, g.EmployeeType })
                                    .Select(s => s.First())
                                    .ToList();
                                var imgDic = new Dictionary<string, string>();
                                if (imgList.Any())
                                {
                                    imgList.ForEach(f =>
                                    {
                                        var key = string.Join("|", f.OrganizationId, f.EmployeeType);
                                        if (imgDic.ContainsKey(key))
                                        {
                                            imgDic[key] = f.OrganizationImage;
                                        }
                                        else
                                        {
                                            imgDic.Add(key, f.OrganizationImage);
                                        }
                                    });
                                    foreach (var obj in res.Items)
                                    {
                                        var objKey = string.Join("|", obj.OrganizationId, obj.EmployeeType);
                                        obj.OrganizationImage = imgDic.ContainsKey(objKey)
                                            ? $"{qiNiuImageDomain}{imgDic[objKey]}"
                                            : $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                    }
                                }
                                else
                                {
                                    foreach (var obj in res.Items)
                                    {
                                        obj.OrganizationImage = $"{qiNiuImageDomain}Shops/Images/202003270946015512.png";
                                    }
                                }
                            }
                        }

                        #endregion 获取所属单位Image
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
                //logger.Info($"Repo: {className}.GetOrgRangePageForLoginByEmpIdExcCurOrgId 返回值：{JsonConvert.SerializeObject(res)}");
                //logger.Info($"Repo: {className}.GetOrgRangePageForLoginByEmpIdExcCurOrgId 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        public async Task<List<OrgRangeRoleListForLoginResDTO>> GetOrgRangeRoleIdList(OrgRangeRoleListForLoginReqDTO req)
        {
            var res = new List<OrgRangeRoleListForLoginResDTO>();

            try
            {
                var param = new DynamicParameters();
                param.Add("@EmployeeId", req.EmployeeId);
                param.Add("@OrganizationId", req.OrganizationId);
                param.Add("@EmployeeType", req.EmployeeType);

                var sql = @"SELECT eor.employee_id employeeId, eor.organization_id organizationId, eor.role_ids roleIds, eor.type employeeType
                            FROM employee e
                            INNER JOIN employee_organization_range eor ON e.id = eor.employee_id
                            WHERE e.is_deleted = 0 AND eor.is_deleted = 0
                            AND eor.employee_id = @EmployeeId
                            AND eor.organization_id = @OrganizationId
                            AND eor.type = @EmployeeType;";

                await OpenSlaveConnectionAsync(async conn => res = (await conn.QueryAsync<OrgRangeRoleListForLoginResDTO>(sql, param)).ToList());
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
            }
            finally
            {
                //logger.Info($"DB: {className}.GetOrgRangeRoleIdList 请求参数：{JsonConvert.SerializeObject(req)}");
                //logger.Info($"DB: {className}.GetOrgRangeRoleIdList 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        public async Task<bool> EditEmployeeRangeRole(EmployeeRangeRoleSaveReqDO req)
        {
            long ud = 0;
            await OpenConnectionAsync(async conn =>
            {
                var tran = conn.BeginTransaction();
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@EmployeeId", req.EmployeeId);
                    param.Add("@Operator", req.Operator);

                    var udSql = @"UPDATE employee_organization_range SET is_deleted = 1, update_by = @Operator, update_time = NOW(3) 
                                    WHERE is_deleted = 0 AND employee_id = @EmployeeId ";
                    ud = await conn.ExecuteAsync(udSql, param);

                    var list = req.EmpOrgRoles;
                    if (ud >= 0 && list.Any())
                    {
                        var crtSql = @"INSERT employee_organization_range (employee_id, organization_id, type, role_ids, create_by)
                                        VALUES (@EmployeeId, @OrganizationId, @Type, @RoleIds, @CreateBy)";
                        ud = conn.Execute(crtSql, req.EmpOrgRoles);
                    }

                    if (ud >= 0) tran.Commit();
                }
                catch (Exception e)
                {
                    tran.Rollback();

                    logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateException), e);
                    //throw new CustomException(CommonConstant.InternalDBError);
                    ud = CommonConstant.OneNeg;
                }
            });

            return ud >= 0;
        }

        #endregion ！！！登录接口相关，请勿轻易对其修改！！！

        public async Task<List<EmployeeDO>> GetAllEmployeeListAsync()
        {
            return await GetEmployeeListWithIsDeletedAsync(null);
        }

        public async Task<List<EmployeeDO>> GetEmployeeListByOrgIdAndTypeAsync(EmployeeListReqDTO req)
        {
            return await GetEmployeeListWithIsDeletedAsync(req);
        }

        public async Task<PagedEntity<EmployeePageDTO>> GetEmployeePage(EmployeePageReqDTO req)
        {
            PagedEntity<EmployeePageDTO> res = new PagedEntity<EmployeePageDTO>();
            IEnumerable<EmployeePageDTO> enumerable = null;
            var total = 0;

            try
            {
                var param = new DynamicParameters();
                param.Add("@index", (req.PageIndex - 1) * req.PageSize);
                param.Add("@size", req.PageSize);

                #region Where Clause
                var whereClause = "";
                var comClu = "";
                var shopClu = "";
                var hasAcctId = req.AccountId.Any();

                if (req.IsDeleted >= 0 && !hasAcctId)
                {
                    param.Add("@isDeleted", req.IsDeleted);
                    whereClause += "WHERE e.is_deleted = @isDeleted ";
                }
                else
                {
                    whereClause += "WHERE (e.is_deleted = 0 OR e.is_deleted = 1) ";
                }

                if (req.Id.IsNotNullOrWhiteSpace())
                {
                    param.Add("@id", req.Id);
                    whereClause += "AND e.id = @id ";
                }

                if (hasAcctId)
                {
                    param.Add("@AccountId", req.AccountId);
                    whereClause += "AND e.account_id IN @AccountId ";
                }

                if (req.EmployeeName.IsNotNullOrWhiteSpace())
                {
                    param.Add("@EmployeeName", $"%{req.EmployeeName}%");
                    whereClause += "AND e.name LIKE @EmployeeName ";
                }

                if (req.Mobile.IsNotNullOrWhiteSpace())
                {
                    param.Add("@Mobile", req.Mobile);
                    whereClause += "AND e.mobile = @Mobile ";
                }

                if (req.OrganizationName.IsNotNullOrWhiteSpace())
                {
                    param.Add("@OrganizationName", $"%{req.OrganizationName}%");
                    comClu += "AND c.name LIKE @OrganizationName ";
                    shopClu += "AND s.simple_name LIKE @OrganizationName ";
                }

                if (req.OrganizationId > 0)
                {
                    param.Add("@OrganizationId", req.OrganizationId);
                    whereClause += "AND e.organization_id = @OrganizationId ";
                }

                if (req.Type >= 0)
                {
                    param.Add("@Type", req.Type);
                    whereClause += "AND e.type = @Type ";
                    if (req.Type == 0 && req.OrganizationId == 0)
                    {
                        param.Add("@OrganizationId", req.OrganizationId);
                        whereClause += "AND e.organization_id = @OrganizationId ";
                    }
                }
                #endregion Where Clause

                await OpenSlaveConnectionAsync(async conn =>
                {
                    //var sqlCount = @"SELECT COUNT(id) FROM employee e " + whereClause;
                    var sqlCount = @"SELECT SUM(id) FROM
                                    (
                                        SELECT COUNT(e.id) id FROM employee e
	                                    INNER JOIN company c ON (e.organization_id = c.id AND e.type = c.type)
	                                    LEFT JOIN department d ON e.department_id = d.id
	                                    LEFT JOIN job j ON e.job_id = j.id 
                                        LEFT JOIN work_kind wk ON e.work_kind_id = wk.id "
                                      + whereClause + comClu
                                      + @" UNION ALL

                                        SELECT COUNT(e.id) id FROM employee e
	                                    INNER JOIN shop s ON (e.organization_id = s.id AND e.type = 1)
	                                    LEFT JOIN department d ON e.department_id = d.id
	                                    LEFT JOIN job j ON e.job_id = j.id
                                        LEFT JOIN work_kind wk ON e.work_kind_id = wk.id "
                                      + whereClause + shopClu
                                      + @" 
                                    ) t ";
                    total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);

                    if (total > 0)
                    {
                        var sql = @"SELECT * FROM
                                    (
                                        SELECT e.id, e.account_id accountId, e.email, e.identity, e.mobile, e.gender, e.number, e.we_chat weChat, e.qq,
                                        e.name, e.type employeeType, e.organization_id organizationId, c.name organizationName, c.simple_name organizationSimpleName, 
                                        IFNULL(e.department_id, 0) DepartmentId, IFNULL(d.name,'') departmentName, 
                                        IFNULL(e.job_id, 0) JobId, IFNULL(j.name,'') jobName, e.level, IFNULL(wk.name,'') workKindName, IFNULL(wk.id,0) workKindId,
	                                    e.is_deleted IsDeleted, e.create_by createby, e.create_time createtime, e.update_by updateby, e.update_time updatetime
	                                    FROM employee e
	                                    INNER JOIN company c ON (e.organization_id = c.id AND e.type = c.type)
	                                    LEFT JOIN department d ON e.department_id = d.id
	                                    LEFT JOIN job j ON e.job_id = j.id 
                                        LEFT JOIN work_kind wk ON e.work_kind_id = wk.id "
                                      + whereClause + comClu
                                      + @" UNION ALL

                                        SELECT e.id, e.account_id accountId, e.email, e.identity, e.mobile, e.gender, e.number, e.we_chat weChat, e.qq,
                                        e.name, e.type employeeType, e.organization_id organizationId, s.full_name organizationName, s.simple_name organizationSimpleName, 
                                        IFNULL(e.department_id, 0) DepartmentId, IFNULL(d.name,'') departmentName, 
                                        IFNULL(e.job_id, 0) JobId, IFNULL(j.name,'') jobName, e.level, IFNULL(wk.name,'') workKindName, IFNULL(wk.id,0) workKindId,
	                                    e.is_deleted IsDeleted, e.create_by createby, e.create_time createtime, e.update_by updateby, e.update_time updatetime
	                                    FROM employee e
	                                    INNER JOIN shop s ON (e.organization_id = s.id AND e.type = 1)
	                                    LEFT JOIN department d ON e.department_id = d.id
	                                    LEFT JOIN job j ON e.job_id = j.id
                                        LEFT JOIN work_kind wk ON e.work_kind_id = wk.id "
                                      + whereClause + shopClu
                                      + @" 
                                    ) t
                                    ORDER BY updateTime DESC, createtime DESC 
                                    LIMIT @index, @size ";

                        enumerable = await conn.QueryAsync<EmployeePageDTO>(sql, param);

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

        public async Task<EmployeeCustomDO> GetEmployeeInfo(EmployeeInfoReqDTO req)
        {
            EmployeeCustomDO res = new EmployeeCustomDO();
            try
            {
                var param = new DynamicParameters();
                param.Add("@EmployeeId", req.EmployeeId);

                var sql = @"SELECT e.id, e.name, e.email, e.identity, e.type, e.organization_id organizationId, e.mobile, e.gender, e.we_chat weChat, e.qq,
                            e.department_id departmentId, e.level, IFNULL(wk.name,'') workKindName, e.work_kind_id workKindId, IFNULL(j.name,'') jobName, e.job_id jobId,
                            e.avatar, e.qualification_certificate qualificationCertificate, e.score, e.number, e.description,
                            e.is_deleted isDeleted
                            FROM employee e
                            LEFT JOIN department d ON e.department_id = d.id
                            LEFT JOIN job j ON e.job_id = j.id
                            LEFT JOIN work_kind wk ON e.work_kind_id = wk.id
                            WHERE e.id = @EmployeeId
                            LIMIT 1;";
                await OpenSlaveConnectionAsync(async conn =>
                {
                    res = await conn.QueryFirstOrDefaultAsync<EmployeeCustomDO>(sql, param);
                });
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.DBQueryException}\n{CommonConstant.ParameterReqDetail}";
                logger.Error(msg, e);
            }
            return res;
        }

        public async Task<List<OrgRangeRoleListForLoginResDTO>> GetOrgRangeRoleListByEmpId(OrgRangeRoleListForLoginReqDTO req)
        {
            var res = new List<OrgRangeRoleListForLoginResDTO>();

            try
            {
                var param = new DynamicParameters();
                param.Add("@EmployeeId", req.EmployeeId);

                var sql = @"SELECT eor.employee_id employeeId, eor.organization_id organizationId, eor.role_ids roleIds, eor.type employeeType
                            FROM employee e
                            INNER JOIN employee_organization_range eor ON e.id = eor.employee_id
                            WHERE e.is_deleted = 0 AND eor.is_deleted = 0
                            AND eor.employee_id = @EmployeeId ";

                await OpenSlaveConnectionAsync(async conn => res = (await conn.QueryAsync<OrgRangeRoleListForLoginResDTO>(sql, param)).ToList());
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBQueryException), e);
            }
            finally
            {
                //logger.Info($"DB: {className}.GetOrgRangeRoleListByEmpId 请求参数：{JsonConvert.SerializeObject(req)}");
                //logger.Info($"DB: {className}.GetOrgRangeRoleListByEmpId 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

        /// <summary>
        /// 查询员工列表
        /// </summary>
        /// <param name="organizationId">单位ID</param>
        /// <param name="type">单位类型 0：公司；1：门店；2：仓库</param>
        /// <returns></returns>
        public async Task<List<EmployeeDO>> GetEmployeeListByShopIdAsync(long organizationId, int type)
        {
            var sql = @"where organization_id=@OrganizationId AND type = @Type AND is_deleted = 0";
            var para = new DynamicParameters();
            para.Add("@OrganizationId", organizationId);
            para.Add("@Type", type);

            var employeeList = await GetListAsync(sql, para);
            return employeeList.ToList();
        }

        public async Task<List<EmployeeDO>> GetEmployeeListByEmpId(List<string> empId)
        {
            var sql = @"WHERE is_deleted = 0 AND id IN @EmpId ";
            var para = new DynamicParameters();
            para.Add("@EmpId", empId);

            var res = await GetListAsync(sql, para);
            return res.ToList();
        }

        /// <summary>
        /// 验证员工是否存在
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<EmployeeDO> EmployeeVerify(EmployeeEntityReqDTO req)
        {
            var para = new DynamicParameters();
            para.Add("@OrganizationId", req.OrganizationId);
            para.Add("@Type", req.Type);
            para.Add("@Email", req.Email);
            para.Add("@Mobile", req.Mobile);

            var sql = @"select id,name,mobile from employee 
                        where organization_id = @OrganizationId AND type = @Type 
                        AND mobile = @Mobile AND is_deleted = 0 ";

            if (req.EmployeeId.IsNotNullOrWhiteSpace())
            {
                para.Add("@EmployeeId", req.EmployeeId);
                sql += " AND id != @EmployeeId";
            }

            EmployeeDO employeeInfo = new EmployeeDO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                employeeInfo = (await conn.QueryAsync<EmployeeDO>(sql, para)).FirstOrDefault();
            });

            return employeeInfo;
        }

        /// <summary>
        /// 获取门店员工列表
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public async Task<PagedEntity<ShopEmployeeDO>> GetShopEmployeePagedListAsync(BasePageShopRequest req)
        {

            var total = 0;
            var param = new DynamicParameters();
            param.Add("@index", (req.PageIndex - 1) * req.PageSize);
            param.Add("@size", req.PageSize);
            param.Add("@ShopId", req.ShopId);

            #region Where Clause
            var whereClause = " where e.organization_id = @ShopId AND e.type = 1 AND e.is_deleted =0 ";
            #endregion Where Clause

            var sqlCount = @"SELECT COUNT(id) FROM employee e " + whereClause;

            PagedEntity<ShopEmployeeDO> res = new PagedEntity<ShopEmployeeDO>();
            IEnumerable<ShopEmployeeDO> enumerable = new List<ShopEmployeeDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                total = await conn.ExecuteScalarAsync<int>(sqlCount, param);
            });

            if (total > 0)
            {
                var sql = @"SELECT e.id, e.name,e.email,e.mobile,e.number,IFNULL(j.name,'') JobName,e.level Level,w.name WorkName,
                                    e.avatar,e.score,j.description
	                                FROM employee e
	                                INNER JOIN shop s ON (e.organization_id = s.id AND e.type = 1)
	                                LEFT JOIN department d ON e.department_id = d.id
	                                LEFT JOIN job j ON e.job_id = j.id 
                                    LEFT JOIN work_kind w on w.id=e.work_kind_id"
                          + whereClause
                          + @" ORDER BY e.update_time DESC, e.create_time DESC 
                                LIMIT @index, @size ";
                await OpenSlaveConnectionAsync(async conn =>
                {
                    enumerable = await conn.QueryAsync<ShopEmployeeDO>(sql, param);
                });
                res.Items = enumerable.ToList();
                res.TotalItems = total;
            }
            return res;
        }

        public async Task<bool> UpdateEmployeeById(EmployeeDO req)
        {
            bool res;
            try
            {
                var param = new List<string>
                {
                    "Avatar", "Name", "OrganizationId", "DepartmentId", "Type", "Identity", "Number", "Mobile", "Email", "Gender", "WeChat", "QQ",
                    "Level", "WorkKindId", "JobId", "QualificationCertificate", "Description", "IsDeleted", "UpdateBy", "UpdateTime"
                };

                res = await UpdateAsync(req, param) > 0;
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenDBExceptionInfo(CommonConstant.DBUpdateException), e);
                throw new CustomException(CommonConstant.InternalDBError);
            }

            return res;
        }

        public async Task<bool> DeleteEmployeeById(EmployeeDeleteReqByIdDTO req)
        {
            long del = 0;
            await OpenConnectionAsync(async conn =>
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@EmployeeId", req.EmployeeId);
                    param.Add("@UpdateBy", req.UpdateBy ?? "【DeleteEmployeeById】");

                    var sql = @"UPDATE employee SET is_deleted = 1, update_by = @UpdateBy, update_time = NOW(3) 
                                WHERE is_deleted = 0 AND id = @EmployeeId ";
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

        public async Task<bool> BatchDeleteEmployeeById(EmployeeBatchDeleteReqByIdDTO req)
        {
            long del = 0;
            await OpenConnectionAsync(async conn =>
            {
                try
                {
                    var param = new DynamicParameters();
                    param.Add("@EmployeeIds", req.EmployeeIds.Distinct().ToList());
                    param.Add("@UpdateBy", req.UpdateBy ?? "【DeleteEmployeeById】");

                    var sql = @"UPDATE employee SET is_deleted = 1, update_by = @UpdateBy, update_time = NOW(3) 
                                WHERE is_deleted = 0 AND id IN @EmployeeIds ";
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

        public async Task<PagedEntity<EmployeeCustomDO>> GetTechnicianPage(TechnicanPageReqDTO req)
        {
            PagedEntity<EmployeeCustomDO> res = new PagedEntity<EmployeeCustomDO>();
            IEnumerable<EmployeeCustomDO> enumerable = null;
            var total = 0;

            try
            {
                var param = new DynamicParameters();
                param.Add("@index", (req.PageIndex - 1) * req.PageSize);
                param.Add("@size", req.PageSize);
                //param.Add("@TechnicanJobId", technicanJobId);
                param.Add("@TechnicanJobId", req.JobId);
                param.Add("@ShopId", req.ShopId);

                #region Where Clause
                var whereClause = "";
                //if (req.Id > 0)
                //{
                //    param.Add("@id", req.Id);
                //    whereClause += "AND id = @id ";
                //}

                //if (req.OrganizationId > 0)
                //{
                //    param.Add("@OrganizationId", req.OrganizationId);
                //    whereClause += "AND organization_id = @OrganizationId ";
                //}
                #endregion Where Clause

                var sqlCount = @"SELECT COUNT(e.id) FROM employee e
                                        INNER JOIN shop s ON (e.organization_id = s.id AND e.type = 1)
                                        LEFT JOIN job j ON e.job_id = j.id
                                        LEFT JOIN work_kind wk ON e.work_kind_id = wk.id
                                        WHERE e.is_deleted = 0 AND e.type = 1 AND j.id in @TechnicanJobId
                                        AND e.organization_id = @ShopId "
                                    + whereClause;
                await OpenSlaveConnectionAsync(async conn =>
                {
                    total = await conn.QueryFirstOrDefaultAsync<int>(sqlCount, param);

                    if (total > 0)
                    {
                        var sql = @"SELECT e.id, e.name, e.email, e.identity, e.mobile, e.gender, e.we_chat weChat, e.qq,
                                        e.level, IFNULL(wk.name,'') workKindName, IFNULL(j.name,'') jobName, e.job_id jobId,
                                        e.avatar, e.qualification_certificate qualificationCertificate, e.score, e.number, e.description
                                        FROM employee e
                                        INNER JOIN shop s ON (e.organization_id = s.id AND e.type = 1)
                                        LEFT JOIN job j ON e.job_id = j.id
                                        LEFT JOIN work_kind wk ON e.work_kind_id = wk.id
                                        WHERE e.is_deleted = 0 AND e.type = 1 AND j.id in @TechnicanJobId
                                        AND e.organization_id = @ShopId "
                                    + whereClause
                                    + @"ORDER BY e.name DESC, e.update_time DESC, e.create_time DESC  
                                        LIMIT @index, @size";

                        enumerable = await conn.QueryAsync<EmployeeCustomDO>(sql, param);
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

        // ---------------------------------- 私有方法 --------------------------------------
        private async Task<List<EmployeeDO>> GetEmployeeListWithIsDeletedAsync(EmployeeListReqDTO req)
        {
            var param = new DynamicParameters();
            var whereClause = "";
            if (req?.IsDeleted >= 0)
            {
                param.Add("@isDeleted", req.IsDeleted);
                whereClause += "WHERE is_deleted = @isDeleted ";
            }
            if (req?.OrganizationId > 0)
            {
                param.Add("@OrganizationId", req.OrganizationId);
                whereClause += whereClause.Contains("WHERE")
                    ? "AND organization_id = @OrganizationId "
                    : "WHERE organization_id = @OrganizationId ";
            }
            if (null != req?.EmployeeType && req.EmployeeType != EmployeeType.None)
            {
                param.Add("@EmployeeType", req?.EmployeeType);
                whereClause += whereClause.Contains("WHERE")
                    ? "AND type = @EmployeeType "
                    : "WHERE type = @EmployeeType ";
            }

            IEnumerable<EmployeeDO> res = new List<EmployeeDO>();
            var sql = @"SELECT id, account_id accountId, name,mobile, type, organization_id organizationId, department_id DepartmentId, job_id JobId, is_deleted IsDeleted
                        FROM employee " +
                      whereClause +
                      " ORDER BY is_deleted ASC, update_time DESC, create_by DESC ";
            await OpenSlaveConnectionAsync(async conn => { res = await conn.QueryAsync<EmployeeDO>(sql, param); });

            return res.ToList();
        }

        /// <summary>
        /// 查询门店的技师
        /// 门店在职 的技师
        /// </summary>
        /// <param name="shopIds"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<EmployeeDO>> GetEmployeesByShopIds(List<long> shopIds)
        {
            //IEnumerable<EmployeeDO> employees= null;
            string conditions = " where is_deleted =0 and type=1 and dimission_type = 0 ";
            if (shopIds.Any())
            {
                conditions += $" and organization_id in ({string.Join(",", shopIds)})";
            }

            var employees = await GetListAsync(conditions);
            // await OpenConnectionAsync(async conn => employees = await conn.QueryAsync<EmployeeDO>(conditions));
            return employees?.ToList() ?? new List<EmployeeDO>();
        }
    }
}
