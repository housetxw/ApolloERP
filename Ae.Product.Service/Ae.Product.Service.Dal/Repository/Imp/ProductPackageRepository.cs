using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ae.Product.Service.Dal.Repository.Imp
{
    public class ProductPackageRepository : AbstractRepository<RelProductPackageDO>, IProductPackageRepository
    {
        public ProductPackageRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        ///  查询套餐明细
        /// </summary>
        public List<ProductPackageDetailVo> GetProductPackageDetailsByCodes(List<string> packageCodes)
        {
            var sql = @"select  fc.product_code ProductCode,fc.name Name,rpg.standard_price StandardPrice,rpg.sales_price SalesPrice,rpg.quantity Quantity,rpg.sort Sort
                        from rel_product_package   rpg 
                        inner join fct_product fc on fc.id=rpg.pid
                        where fc.is_deleted=0 and rpg.is_deleted=0 and fc.product_code in @packageCodes";
            var paras = new
            {
                packageCodes = packageCodes
            };
            IEnumerable<ProductPackageDetailVo> result = new List<ProductPackageDetailVo>();
            OpenConnection( conn =>
            {
                result = conn.Query<ProductPackageDetailVo>(sql, paras);
            }
            );
            return result.ToList();
        }
    }
}
