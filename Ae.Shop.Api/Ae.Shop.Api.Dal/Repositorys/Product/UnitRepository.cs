using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Product
{
    public class UnitRepository: AbstractRepository<DimUnitDo>, IUnitRepository
    {
        public UnitRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ProductSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductSqlReadOnly");
        }

        /// <summary>
        /// 获取单位
        /// </summary>
        /// <returns></returns>
        public async Task<List<DimUnitDo>> GetDimUnitList()
        {
            var result = await GetListAsync<DimUnitDo>("");

            return result?.ToList() ?? new List<DimUnitDo>();
        }
    }
}
