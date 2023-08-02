using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Core.Request;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Repository
{
    public interface ICategoryAttributeRepository: IRepository<RelProductAttributevalueDO>
    {

        /// <summary>
        ///  根据类目Id获取属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        List<CategoryAttributeVo> GetAttributesByCategoryId(string categoryId);

        /// <summary>
        ///  获取类目属性信息
        /// </summary>
        /// <param name="categoryId">三级类目Id</param>
        /// <returns></returns>
        List<CategoryAttributeVo> SelectCategoryAttribute(CategoryAttributeRequest request);
    }
}
