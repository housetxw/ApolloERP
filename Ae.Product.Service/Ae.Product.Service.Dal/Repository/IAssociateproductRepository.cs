using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Repository
{
    public interface IAssociateproductRepository : IRepository<FctAssociateproductDO>
    {
        /// <summary>
        /// 查询关联商品明细
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        List<AssociateProductDetailVo> GetProductsattributevalueById(string groupId, List<string> attributeNames);
    }
}
