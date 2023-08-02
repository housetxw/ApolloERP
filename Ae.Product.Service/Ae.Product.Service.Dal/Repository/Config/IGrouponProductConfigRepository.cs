using ApolloErp.Data.DapperExtensions;
using Ae.Product.Service.Dal.Model.Config;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Product.Service.Core.Model.Config;
using Ae.Product.Service.Dal.Model.Condition;

namespace Ae.Product.Service.Dal.Repository.Config
{
    public interface IGrouponProductConfigRepository : IRepository<GrouponProductConfigDO>
    {
        Task<PagedEntity<GrouponProductVo>> GetGrouponProductPageList(
            GrouponProductPageListCondition request);

        /// <summary>
        /// 根据pid查询团购商品
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<GrouponProductConfigDO> GetGrouponProductByPid(string pid, bool readOnly = true);

        /// <summary>
        /// 根据pidList查询团购商品
        /// </summary>
        /// <param name="pidList"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<List<GrouponProductConfigDO>> GetGrouponProductByPidList(List<string> pidList, bool readOnly = true);
    }
}
