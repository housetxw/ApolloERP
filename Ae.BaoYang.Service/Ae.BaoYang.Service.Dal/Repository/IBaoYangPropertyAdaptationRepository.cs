using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangPropertyAdaptationRepository : IRepository<BaoYangPropertyAdaptationDO>
    {
        /// <summary>
        /// tid查询五级属性适配
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPropertyAdaptationDO>> GetPropertyAdaptationByTid(string tid,
            bool readOnly = true);

        /// <summary>
        /// tidList 和 类型 查询五级属性适配
        /// </summary>
        /// <param name="tidList"></param>
        /// <param name="baoYangType"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPropertyAdaptationDO>> GetPropertyAdaptation(List<string> tidList,
            string baoYangType);
    }
}
