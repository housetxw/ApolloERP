using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Product
{
    public interface ICategoryRepository : IRepository<DimCategoryDO>
    {
        /// <summary>
        /// 获取三级类目
        /// </summary>
        /// <param name="childCategoryId"></param>
        /// <returns></returns>
        Task<AllCategoryDo> GetAllCategoryById(int childCategoryId);
    }
}
