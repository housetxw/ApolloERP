using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Imp
{
    public class ProductSecurityCodeRepository : AbstractRepository<ProductSecurityCodeDO>,
        IProductSecurityCodeRepository
    {
        public ProductSecurityCodeRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="securityCode"></param>
        /// <returns></returns>
        public async Task<ProductSecurityCodeDO> GetProductSecurityCode(string securityCode)
        {
            var para = new DynamicParameters();
            para.Add("@securityCode", securityCode);
            var result = await GetListAsync<ProductSecurityCodeDO>("WHERE security_code = @securityCode", para);
            return result?.FirstOrDefault();
        }

        public async Task<int> UpdateSearchCount(long id)
        {
            var para = new DynamicParameters();
            para.Add("@id", id);

            string sql = @"UPDATE product_security_code 
SET search_count = search_count + 1,
first_search_time = IFNULL( first_search_time, NOW( ) )
WHERE
	`id` = @id;";

            var result = 0;
            await OpenConnectionAsync(async conn => { result = await conn.ExecuteAsync(sql, para); });

            return result;
        }
    }
}
