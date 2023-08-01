using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangProductRefRepository : AbstractRepository<BaoYangProductRefDO>,
        IBaoYangProductRefRepository
    {
        public BaoYangProductRefRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        public async Task<IEnumerable<BaoYangProductRefDO>> GetBaoYangProductRef(string partCode, string pid,
            bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@partCode", partCode);
            para.Add("@pid", pid);
            var result =
                await GetListAsync<BaoYangProductRefDO>(" WHERE part_code = @partCode AND pid = @pid", para, !readOnly);
            return result;
        }

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
        public async Task<PagedEntity<BaoYangProductRefDO>> GetPageBaoYangProductRef(string partCode, string pid,
            DateTime? startDate, DateTime? endDate, int pageIndex, int pageSize)
        {
            StringBuilder condition = new StringBuilder();
            var para = new DynamicParameters();
            condition.Append("WHERE 0 =0");
            if (!string.IsNullOrEmpty(partCode))
            {
                condition.Append(" AND part_code = @partCode");
                para.Add("@partCode", partCode);
            }

            if (!string.IsNullOrEmpty(pid))
            {
                condition.Append(" AND pid = @pid");
                para.Add("@pid", pid);
            }

            if (startDate != null)
            {
                condition.Append(" AND create_time >= @startDate");
                para.Add("@startDate", startDate);
            }

            if (endDate != null)
            {
                condition.Append(" AND create_time < @endDate");
                para.Add("@endDate", endDate);
            }

            var result =
                await GetListPagedAsync<BaoYangProductRefDO>(pageIndex, pageSize, condition.ToString(), "id DESC",
                    para);

            return result;
        }
    }
}
