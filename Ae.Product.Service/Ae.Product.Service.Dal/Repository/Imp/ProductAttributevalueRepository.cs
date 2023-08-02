using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository.Imp
{
    public class ProductAttributevalueRepository : AbstractRepository<RelProductAttributevalueDO>,
        IProductAttributevalueRepository
    {
        public ProductAttributevalueRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }


        /// <summary>
        /// 查询商品属性 By Ids
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public List<ProductAttributevalueVo> GetProductAttributeValueById(List<string> productIds)
        {
            string sql =
                @"select rpa.id Id, rpa.product_id ProductId,rpa.attribute_name_id AttributeNameId,da.attribute_name AttributeName,
             rpa.product_attribute_value AttributeValue
             from rel_product_attributevalue rpa 
             inner join dim_attributemame da on rpa.attribute_name_id=da.id
             where rpa.is_deleted=0 and da.is_deleted=0 and rpa.product_id in @productIds";
            var paras = new
            {
                productIds = productIds.ToArray()
            };
            IEnumerable<ProductAttributevalueVo> result = new List<ProductAttributevalueVo>();
            OpenConnection(conn => { result = conn.Query<ProductAttributevalueVo>(sql, paras); }
            );
            return result.ToList();
        }


        /// <summary>
        /// 查询商品属性 By Ids
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public async Task<List<ProductAttributevalueVo>> GetProductAttributeValueByIdAsync(List<string> productIds)
        {
            string sql =
                @"select rpa.id Id, rpa.product_id ProductId,rpa.attribute_name_id AttributeNameId,da.attribute_name AttributeName,
             rpa.product_attribute_value AttributeValue
             from rel_product_attributevalue rpa 
             inner join dim_attributemame da on rpa.attribute_name_id=da.id
             where rpa.is_deleted=0 and da.is_deleted=0 and rpa.product_id in @productIds";
            var paras = new
            {
                productIds = productIds.ToArray()
            };
            IEnumerable<ProductAttributevalueVo> result = new List<ProductAttributevalueVo>();

            await OpenConnectionAsync(async conn =>
            {
                result = await conn.QueryAsync<ProductAttributevalueVo>(sql, paras);
            });

            return result.ToList();
        }

    }
}
