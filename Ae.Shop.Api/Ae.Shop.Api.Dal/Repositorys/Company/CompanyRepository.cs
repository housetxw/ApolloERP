using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.Company;
using Ae.Shop.Api.Dal.Model;
using Ae.Shop.Api.Dal.Model.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Company
{
    public class CompanyRepository : AbstractRepository<CompanyDO>, ICompanyRepository
    {
        public CompanyRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
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
        public async Task<CompanySimpleInfoDO> GetCompanyByName(string name, string simpleName)
        {
            string sql = @"SELECT
	c.id,
	c.NAME
FROM
	company c
WHERE
	c.is_deleted = 0  AND c.simple_name = @simpleName ";

            var condtions = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(name))
            {
                condtions.Append(" AND c.name = @name");
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
        /// 查询公司列表-分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PagedEntity<CompanyDO>> GetPageCompanyListForShopAsync(GetPageCompanyListRequest request)
        {
            // 按创建时间 id DESC
            var orderBy = "";
            var para = new DynamicParameters();
            var condtions = new StringBuilder("where  is_deleted = 0 ");

            if (request.ParentId > 0)
            {
                condtions.Append(" AND parent_id = @ParentId ");
                para.Add("@ParentId", request.ParentId);
            }

            if (request.Status > 0)
            {
                condtions.Append(" AND status = @Status ");
                para.Add("@Status", request.Status);
            }

            if (request.Type > 0)
            {
                condtions.Append(" AND type = @Type ");
                para.Add("@Type", request.Type);
            }

            if (request.Level > 0)
            {
                condtions.Append(" AND level = @Level ");
                para.Add("@Level", request.Level);
            }

            if (request.SystemType > 0)
            {
                condtions.Append(" AND system_type = @SystemType ");
                para.Add("@SystemType", request.SystemType);
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
            var result = await GetListPagedAsync(request.PageIndex, request.PageSize, condtions.ToString(), orderBy, para, true);
            return result;
        }


        /// <summary>
        /// 查询公司下属门店数量
        /// </summary>
        /// <param name="companyIds"></param>
        /// <returns></returns>
        public async Task<List<CompanyContainShopsDO>> GetShopCountUnderTheCompanyAsync(List<long> companyIds)
        {
            string sql = @"SELECT
    company_id CompanyId,COUNT(*) TotalCount
FROM
    shop WHERE company_id in(2,3) and is_deleted = 0 and online = 1 GROUP BY company_id";
            var param = new DynamicParameters();
            param.Add("@IdCardFront", companyIds);

            IEnumerable<CompanyContainShopsDO> ShopList = new List<CompanyContainShopsDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                ShopList = await conn.QueryAsync<CompanyContainShopsDO>(sql);
            });
            return ShopList.ToList();
        }

        /// <summary>
        /// 根据名称查公司数量
        /// </summary>
        /// <param name="name"></param>
        /// <param name="simpleName"></param>
        /// <returns></returns>
        public async Task<int> GetCompanyTotalByName(string simpleName,long id)
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

        public async Task<List<ShopSimpleInfoDO>> GetShopWareHouseListAsync(GetShopListRequest request)
        {
            string sql = @"SELECT
    s.id,
	s.simple_name as SimpleName,
    s.province,
    s.city,
    s.district,
    s.province_id ProvinceId,
    s.city_id CityId,
    s.district_id DistrictId,
    s.company_id as CompanyId,
    s.address,
    s.type,
	s.contact,
	s.telephone,
	s.mobile
FROM
    shop s ";
            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE s.is_deleted = 0  ");
            var para = new DynamicParameters();

            if (!string.IsNullOrEmpty(request.SimpleName))
            {
                para.Add("@SimpleName", request.SimpleName);
                condition.Append(" AND s.simple_name like CONCAT('%',@SimpleName,'%') ");
            }
            if (!string.IsNullOrEmpty(request.ProvinceId))
            {
                para.Add("@ProvinceId", request.ProvinceId);
                condition.Append(" AND s.province_id = @ProvinceId ");
            }
            if (!string.IsNullOrEmpty(request.CityId))
            {
                para.Add("@CityId", request.CityId);
                condition.Append(" AND s.city_id = @CityId ");
            }
            if (!string.IsNullOrEmpty(request.DistrictId))
            {
                para.Add("@DistrictId", request.DistrictId);
                condition.Append(" AND s.district_id = @DistrictId ");
            }

            if (request.ShopIds != null && request.ShopIds.Any())
            {
                para.Add("@ShopIds", request.ShopIds);
                condition.Append(" AND s.id in @ShopIds ");
            }

            if (request.ShopTypes != null && request.ShopTypes.Any())
            {
                para.Add("@ShopTypes", request.ShopTypes);
                condition.Append(" AND s.type in @ShopTypes ");
            }
            if (request.CompanyId > 0)
            {
                para.Add("@CompanyId", request.CompanyId);
                condition.Append(" AND s.company_id = @CompanyId ");
            }

            //condition.Append(" ORDER BY s.id desc");
            sql = sql + condition.ToString();

            IEnumerable<ShopSimpleInfoDO> ShopList = new List<ShopSimpleInfoDO>();
            await OpenSlaveConnectionAsync(async conn =>
            {
                ShopList = await conn.QueryAsync<ShopSimpleInfoDO>(sql, para);
            });

            return ShopList.ToList();
        }

    }
}
