using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Dapper;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangPropertyAdaptationRepository : AbstractRepository<BaoYangPropertyAdaptationDO>,
        IBaoYangPropertyAdaptationRepository
    {
        public BaoYangPropertyAdaptationRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// tid查询五级属性适配
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPropertyAdaptationDO>> GetPropertyAdaptationByTid(string tid,
            bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@tid", tid);
            var result =
                await GetListAsync<BaoYangPropertyAdaptationDO>(" WHERE tid = @tid", para, !readOnly);
            return result;
        }

        /// <summary>
        /// tidList 和 类型 查询五级属性适配
        /// </summary>
        /// <param name="tidList"></param>
        /// <param name="baoYangType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPropertyAdaptationDO>> GetPropertyAdaptation(List<string> tidList,
            string baoYangType)
        {
            var para = new DynamicParameters();
            para.Add("@tidList", tidList);
            para.Add("@partNameAbbr", baoYangType);
            var result =
                await GetListAsync<BaoYangPropertyAdaptationDO>(
                    " WHERE tid IN @tidList AND part_name_abbr = @partNameAbbr", para);
            return result;
        }
    }
}
