using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangOeCodeMapRepository : IRepository<BaoYangOeCodeMapDO>
    {
        /// <summary>
        /// 根据oe号查询零件号
        /// </summary>
        /// <param name="oeCode"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangOeCodeMapDO>> GetPartCodeByOe(string oeCode, bool readOnly = true);

        /// <summary>
        /// Oe映射查询
        /// </summary>
        /// <param name="oeCode"></param>
        /// <param name="partCode"></param>
        /// <param name="partType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<Tuple<List<BaoYangOeCodeMapDO>, int>> GetOeCodeMap(string oeCode, string partCode,
            string partType, int pageIndex, int pageSize);

        /// <summary>
        /// 删除OE件号
        /// </summary>
        /// <param name="oePartCode"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        Task<int> DeleteOePartCode(string oePartCode, string submitBy);
    }
}
