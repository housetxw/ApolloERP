using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.Product
{
    public interface IUnitRepository : IRepository<DimUnitDo>
    {
        /// <summary>
        /// 获取单位
        /// </summary>
        /// <returns></returns>
        Task<List<DimUnitDo>> GetDimUnitList();
    }
}
