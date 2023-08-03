using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public class ShopCheckResultRepository : AbstractRepository<ShopCheckResultDo>, IShopCheckResultRepository
    {
        public ShopCheckResultRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ReceiveSqlReadOnly");
        }

        /// <summary>
        /// 检查结果查询
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <param name="propertyType"></param>
        /// <returns></returns>
        public async Task<List<ShopCheckResultDo>> GetShopCheckResult(long checkId, int categoryId, int propertyType)
        {
            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            condition.Append("WHERE check_id = @checkId AND category_id = @categoryId");
            para.Add("@checkId", checkId);
            para.Add("@categoryId", categoryId);
            if (propertyType >= 0)
            {
                condition.Append(" AND property_type = @propertyType");
                para.Add("@propertyType", propertyType);
            }

            var result = await GetListAsync<ShopCheckResultDo>(condition.ToString(), para);

            return result?.ToList() ?? new List<ShopCheckResultDo>();
        }
    }
}
