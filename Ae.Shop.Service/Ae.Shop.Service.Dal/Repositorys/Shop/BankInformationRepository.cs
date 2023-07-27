using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public class BankInformationRepository : AbstractRepository<BankInformationDO>, IBankInformationRepository
    {
        public BankInformationRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSql");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ShopManageSqlReadOnly");
        }

        /// <summary>
        /// 查询银行列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<BankInformationDO>> GetBankListAsync()
        {
            StringBuilder condition = new StringBuilder();
            condition.Append(" WHERE is_deleted = 0");
            var items = await GetListAsync<BankInformationDO>(condition.ToString());
            return items.ToList();
        }
    }
}
