using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model;
using Ae.Product.Service.Dal.Model.Condition;
using Ae.Product.Service.Dal.Model.Extend;

namespace Ae.Product.Service.Dal.Repository
{
    public interface IDoorProductRepository : IRepository<DoorProductDO>
    {
        /// <summary>
        /// 查询上门产品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<DoorProductDTO>> GetDoorProductPageList(DoorProductPageListCondition request);

        /// <summary>
        /// Pid查询上门产品
        /// </summary>
        /// <param name="pidList"></param>
        /// <returns></returns>
        Task<List<DoorProductDO>> GetDoorProductByPidList(List<string> pidList);
    }
}
