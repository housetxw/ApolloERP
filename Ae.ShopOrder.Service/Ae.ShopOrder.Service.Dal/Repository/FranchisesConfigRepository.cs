using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Dal.Model;
using Ae.ShopOrder.Service.Dal.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository
{
    public class FranchisesConfigRepository : AbstractRepository<FranchisesConfigDO>, IFranchisesConfigRepository
    {
        public FranchisesConfigRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("OrderConfigSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("OrderConfigSqlReadOnly");
        }

        public async Task<List<FranchisesConfigDO>> GetFranchises(GetFranchisesConfigRequest request)
        {
            var builder = new StringBuilder();

            builder.Append(" where is_deleted=0");
            if (request.Type > 0)
            {
                builder.Append(" and type=" + request.Type);
            }
            var list = await GetListAsync(builder.ToString(), null, true);
            return list?.ToList();
        }
    }
}
