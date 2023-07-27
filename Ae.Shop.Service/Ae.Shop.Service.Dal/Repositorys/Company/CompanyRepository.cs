using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Dal.Model;
using Dapper;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using DynamicParameters = Dapper.DynamicParameters;
using System.Linq;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.Company;

namespace Ae.Shop.Service.Dal.Repositorys.Company
{
    public class CompanyRepository : AbstractRepository<CompanyDO>, ICompanyRepository
    {
        public CompanyRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        ///新增公司
        /// </summary>
        /// <param name="companyDO"></param>
        /// <returns></returns>
        public async Task<int> AddCompanyAsync(CompanyDO companyDO) 
        {
            int id = await InsertAsync(companyDO);
            return id;
        }

        /// <summary>
        /// 根据公司名称查询公司简单信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="simpleName"></param>
        /// <returns></returns>
        public async Task<CompanySimpleInfoDO> GetCompanyByName(string name,string simpleName)
        {
            string sql = @"SELECT
	c.id,
	c.NAME,
    c.`type`
FROM
	company c
WHERE
	c.is_deleted = 0  AND c.simple_name = @simpleName ";

            var condtions = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(name)) 
            {
                condtions.Append(" OR c.name = @name");
            }

            var para = new DynamicParameters();
            para.Add("@name", name);
            para.Add("@simpleName", simpleName);

            CompanySimpleInfoDO result = new CompanySimpleInfoDO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<CompanySimpleInfoDO>(sql + condtions.ToString(), para)).FirstOrDefault();
            });
            return result;
        }

        /// <summary>
        /// 名称模糊查询公司
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<List<CompanyDO>> GetCompanyByName(string name)
        {
            var para = new DynamicParameters();
            para.Add("@name", $"{name}%");
            var result = await GetListAsync<CompanyDO>("WHERE (`simple_name` LIKE @name OR `name` LIKE @name)", para);

            return result?.ToList() ?? new List<CompanyDO>();
        }

        /// <summary>
        /// 根据公司ID查询公司简单信息
        /// </summary>
        /// <param name="name">公司名称</param>
        /// <param name="id">公司id</param>
        /// <returns></returns>
        public async Task<CompanySimpleInfoDO> GetCompanyInfo(long companyId) 
        {
            string sql = @"SELECT
	c.id,
	c.NAME,
	c.`type`,
	CONCAT(c.province,c.city,c.district,c.address) as Address,
	img.img_url as ImgUrl
FROM
	company c
	LEFT JOIN company_img img ON c.id = img.company_id  AND img.type = 1
WHERE
	c.id = @CompanyId AND c.is_deleted = 0;";

            
            var para = new DynamicParameters();
            para.Add("@CompanyId", companyId);

            CompanySimpleInfoDO result = new CompanySimpleInfoDO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<CompanySimpleInfoDO>(sql, para)).FirstOrDefault();
            });
            return result;
        }

        /// <summary>
        /// 查询公司列表-分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedEntity<CompanyDO>> GetPageCompanyListForShopAsync(GetPageCompanyListRequest request) 
        {
            // 按创建时间
            var orderBy = " id DESC";
            var para = new DynamicParameters();

            var condtions = new StringBuilder(); 

            if (request.ParentId == -1)
            {
                condtions = new StringBuilder("where is_deleted = 0 ");
            }
            else
            {
                condtions = new StringBuilder("where is_deleted = 0 and parent_id = 0");
               
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                condtions.Append(" AND name like @name ");
                para.Add("@name", string.Format("%{0}%", request.Name));
            }
            if (!string.IsNullOrEmpty(request.ProvinceId))
            {
                condtions.Append(" AND province_id = @ProvinceId");
                para.Add("@ProvinceId", request.ProvinceId);
            }
            if (!string.IsNullOrEmpty(request.CityId))
            {
                condtions.Append(" AND city_id = @CityId ");
                para.Add("@CityId", request.CityId);
            }
            if (!string.IsNullOrEmpty(request.DistrictId))
            {
                condtions.Append(" AND district_id = @DistrictId ");
                para.Add("@DistrictId", request.DistrictId);
            }
            if (request.Status != -1) 
            {
                condtions.Append(" AND status = @Status ");
                para.Add("@Status", request.Status);
            }

            var result = await GetListPagedAsync(request.PageIndex, request.PageSize, condtions.ToString(), orderBy, para, false);
            return result;
        }

        /// <summary>
        /// 获取所有公司数据
        /// </summary>
        /// <param name="brandName"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<List<CompanyDO>> GetAllCompanyAsync()
        {
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE is_deleted = 0");
            var result = await GetListAsync<CompanyDO>(condition.ToString());
            return result.ToList();
        }

        /// <summary>
        /// 根据门店ID查询公司信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<CompanySimpleInfoDO> GetCompanyInfoByShopId(long shopId)
        {
            //查询详情信息
            string sql = @"SELECT
	c.id,
	c.NAME,
	c.`type`,
    c.`level`,
	c.province_id ProvinceId,
	c.city_id CityId,
	c.district_id DistrictId,
	c.province,
	c.city,
	c.district,
	c.address,
    c.introduction,
	img.img_url AS ImgUrl 
FROM
	company c
	LEFT JOIN company_img img ON c.id = img.company_id 
	AND img.type = 2 
	AND img.is_deleted = 0
	LEFT JOIN shop s ON c.id = s.company_id 
WHERE
	s.id = @ShopId AND c.is_deleted = 0;";


            var para = new DynamicParameters();
            para.Add("@ShopId", shopId);

            CompanySimpleInfoDO result = new CompanySimpleInfoDO();
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = (await conn.QueryAsync<CompanySimpleInfoDO>(sql, para)).FirstOrDefault();
            });
            return result;
        }

        /// <summary>
        /// 查询公司下的子公司和门店
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<List<CompanyLessInfoDO>> GetCompanyAndShopByParentId(long parentId) 
        {
            var sql = @"SELECT
	                    c.id,
	                    c.NAME FullName,
	                    c.type,
                        c.system_type SystemType
                    FROM
	                    company c 
                    WHERE
	                    c.is_deleted = 0 
	                    AND c.parent_id = @ParentId AND c.`status` != 3
                UNION ALL
                    SELECT
	                    s.id,
	                    s.full_name FullName,
	                    1 type,
                        s.system_type SystemType
                    FROM
	                    shop s 
                    WHERE
	                    s.is_deleted = 0  and s.type > 0
	                    AND s.company_id = @ParentId AND s.`status` != 1";
            var para = new DynamicParameters();
            para.Add("@ParentId", parentId);
            IEnumerable<CompanyLessInfoDO> result = new List<CompanyLessInfoDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = await conn.QueryAsync<CompanyLessInfoDO>(sql, para);
            });
            return result.ToList();
        }


        /// <summary>
        /// 修改公司状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<int> UpdateCompanyStatus(UpdateCompanyStatusRequest request)
        {
            var param = new DynamicParameters();
            param.Add("@UpdateBy", request.UpdateBy);
            param.Add("@CompanyId", request.CompanyId);
            param.Add("@Status", request.Status);
            param.Add("@FailedExaminedReason", request.FailedExaminedReason);



            var sql = @"UPDATE company 
                        SET update_by = @UpdateBy,
                        update_time = SYSDATE( ),
                        status = @Status,
                        examiner = @UpdateBy";
            if (request.Status == 4) 
            {
                sql += ",failed_examined_reason = @FailedExaminedReason";
            }
            sql += @" WHERE id = @CompanyId";

            var result = -1;
            await OpenConnectionAsync(async conn => result = await conn.ExecuteAsync(sql, param));
            return result;
        }

        /// <summary>
        /// 根据名称查公司数量
        /// </summary>
        /// <param name="name"></param>
        /// <param name="simpleName"></param>
        /// <returns></returns>
        public async Task<int> GetCompanyTotalByName(string simpleName, long id)
        {
            string sql = @"SELECT
	COUNT(1)
FROM
	company c
WHERE
	c.is_deleted = 0  AND c.simple_name = @simpleName ";

            var condtions = new StringBuilder();
            if (id > 0)
            {
                condtions.Append(" AND c.id != @id");
            }

            var para = new DynamicParameters();
            para.Add("@id", id);
            para.Add("@simpleName", simpleName);

            int result = 0;
            await OpenSlaveConnectionAsync(async conn =>
            {
                result = await conn.ExecuteScalarAsync<int>(sql + condtions.ToString(), para);
            });
            return result;
        }

        /// <summary>
        /// 更新押金
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="depositAmount"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCompanyDeposit(long companyId, decimal depositAmount, string updateBy)
        {
            string sql = @"UPDATE company 
SET deposit_amount = deposit_amount + @depositAmount,
update_by = @updateBy,
update_time = NOW( ) 
WHERE
	id = @companyId;";

            var para = new DynamicParameters();
            para.Add("@depositAmount", depositAmount);
            para.Add("@updateBy", updateBy);
            para.Add("@companyId", companyId);

            int result = 0;
            await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para); });
            return result > 0;
        }
    }
}
