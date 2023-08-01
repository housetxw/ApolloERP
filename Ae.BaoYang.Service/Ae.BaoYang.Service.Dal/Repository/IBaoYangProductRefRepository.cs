using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangProductRefRepository:IRepository<BaoYangProductRefDO>
    {
        Task<IEnumerable<BaoYangProductRefDO>> GetBaoYangProductRef(string partCode, string pid,
            bool readOnly = true);

        /// <summary>
        /// 配件关联pid 列表查询
        /// </summary>
        /// <param name="partCode"></param>
        /// <param name="pid"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<PagedEntity<BaoYangProductRefDO>> GetPageBaoYangProductRef(string partCode, string pid,
            DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize);
    }
}
