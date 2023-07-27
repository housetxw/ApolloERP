using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Dal.Model;
using Dapper;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using DynamicParameters = Dapper.DynamicParameters;
using System.Linq;

namespace Ae.Shop.Service.Dal.Repositorys.JoinIn
{
    public class JoinInRespository : AbstractRepository<JoinInDO>, IJoinInRespository
    {
        public JoinInRespository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 新增门店信息
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        public async Task<int> JoinInAsync(JoinInDO join)
        {
            int id = await InsertAsync(join);
            return id;
        }

        public async Task<PagedEntity<JoinInDO>> GetJoinInList(JoinInListCondition request)
        {
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE 0 = 0");
            var para = new DynamicParameters();
            if (!string.IsNullOrEmpty(request.Phone))
            {
                condition.Append(" AND `phone` = @phone");
                para.Add("@phone", request.Phone);
            }

            if (request.ProvinceId > 0)
            {
                condition.Append(" AND province_id = @provinceId");
                para.Add("@provinceId", request.ProvinceId);
            }

            if (request.CityId > 0)
            {
                condition.Append(" AND city_id = @cityId");
                para.Add("@cityId", request.CityId);
            }

            if (request.DistrictId > 0)
            {
                condition.Append(" AND district_id = @districtId");
                para.Add("@districtId", request.DistrictId);
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                condition.Append(" AND `name` LIKE @name");
                para.Add("@name", $"%{request.Name}%");
            }

            var result = await GetListPagedAsync<JoinInDO>(request.PageIndex, request.PageSize, condition.ToString(),
                "`id` DESC", para);

            return result;
        }
    }
}
