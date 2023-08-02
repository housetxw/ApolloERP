using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository
{
    public interface IProductAttributevalueRepository: IRepository<RelProductAttributevalueDO>
    {

        /// <summary>
        /// 查询商品属性 By Ids
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        List<ProductAttributevalueVo> GetProductAttributeValueById(List<string> productIds);

        /// <summary>
        /// 查询商品属性 By Ids
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        Task<List<ProductAttributevalueVo>> GetProductAttributeValueByIdAsync(List<string> productIds);
    }
}
