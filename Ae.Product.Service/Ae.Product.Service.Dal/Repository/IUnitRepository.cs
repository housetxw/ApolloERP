using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Product.Service.Core.Request;

namespace Ae.Product.Service.Dal.Repository
{
    public interface IUnitRepository : IRepository<DimUnitDO>
    {
        /// <summary>
        /// 分页请求商品的品牌
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<DimUnitDO>> GetProductUnitList(GetProductUnitListRequest request);


        /// <summary>
        /// 编辑品牌
        /// </summary>
        /// <param name="dimUnitDo"></param>
        /// <returns></returns>
        Task<int> EditUnitAsync(DimUnitDO dimUnitDo);
    }
}
