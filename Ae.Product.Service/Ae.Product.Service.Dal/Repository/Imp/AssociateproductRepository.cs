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
    public class AssociateproductRepository : AbstractRepository<FctAssociateproductDO>, IAssociateproductRepository
    {
        public AssociateproductRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 查询关联商品明细
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public List<AssociateProductDetailVo> GetProductsattributevalueById(string groupId, List<string> attributeNames)
        {
            string sql = @"select distinct t.product_code ProductCode,da.display_name AttributedisplayName,
             t.name Name, t.sales_price SalesPrice,
             da.attribute_name AttributeName, rpa.product_attribute_value AttributeValue
             from rel_product_attributevalue rpa 
			 inner join (
               select  fc.product_code ,fc.id product_id,fc.sales_price,fc.name
               from fct_product fc 
		       inner join rel_associateproduct_detail rad on fc.product_code=rad.product_code
               where fc.is_deleted=0 and rad.is_deleted=0 and group_id=@groupid
			 ) t on t.product_id=rpa.product_id
             inner join dim_attributemame da on rpa.attribute_name_id=da.id
             where rpa.is_deleted=0 and da.is_deleted=0 and da.attribute_name in @attributename";
            var paras = new
            {
                groupid = groupId,
                attributename = attributeNames
            };
            IEnumerable<AssociateProductDetailVo> result = new List<AssociateProductDetailVo>();
            OpenConnection(conn =>
            {
                result = conn.Query<AssociateProductDetailVo>(sql, paras);
            }
            );
            return result.ToList();
        }

    }
}
