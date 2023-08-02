using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Core.Model;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Product.Service.Dal.Repository
{
    public interface ICategoryRepository: IRepository<DimCategoryDO>
    {
        /// <summary>
        /// 根据三级类目获取类目信息
        /// </summary>
        /// <param name="childCategoryId"></param>
        /// <returns></returns>
        List<AllCategoryVo> GetAllCategoryById(long childCategoryId);
        Task<int> UpdateCategory(DimCategoryDO categoryDo);
        Task<int> AddCategory(DimCategoryDO categoryDo);

        /// <summary>
        /// 获取所有产品类目
        /// </summary>
        /// <returns></returns>
        Task<List<DimCategoryDO>> GetAllCategory(long shopId);
    }
}
