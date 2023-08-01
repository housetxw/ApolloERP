using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public class BaoYangPackageRefRepository : AbstractRepository<BaoYangPackageRefDO>,
        IBaoYangPackageRefRepository
    {
        public BaoYangPackageRefRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("Product");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("ProductReadOnly");
        }

        /// <summary>
        /// 根据Tid查询适配套餐
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPackageRefDO>> GetBaoYangPackageRefByTidAsync(string tid)
        {
            var para = new DynamicParameters();
            para.Add("@tid", tid);

            var result = await GetListAsync<BaoYangPackageRefDO>("WHERE tid = @tid", para);

            return result;
        }

        /// <summary>
        /// 查询车型已绑定套餐
        /// </summary>
        /// <param name="tidList"></param>
        /// <param name="packageId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPackageRefDO>> GetBaoYangPackageRefByTidsAsync(List<string> tidList,
            string packageId)
        {
            if (string.IsNullOrEmpty(packageId) && (tidList == null || !tidList.Any()))
            {
                return new List<BaoYangPackageRefDO>();
            }

            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE 0 = 0 ");
            var para = new DynamicParameters();
            if (tidList != null && tidList.Any())
            {
                condition.Append(" AND tid IN @tids ");
                para.Add("@tids", tidList);
            }

            if (!string.IsNullOrEmpty(packageId))
            {
                condition.Append(" AND package_id = @package_id ");
                para.Add("@package_id", packageId);
            }

            var result = await GetListAsync<BaoYangPackageRefDO>(condition.ToString(), para);

            return result;
        }

        /// <summary>
        /// 根据tid and baoYangType查询套餐
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="baoYangType"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPackageRefDO>> GetBaoYangPackageRefByParaAsync(string tid,
            List<string> baoYangType)
        {
            var para = new DynamicParameters();
            para.Add("@tid", tid);
            para.Add("@baoYangType", baoYangType);

            var result =
                await GetListAsync<BaoYangPackageRefDO>("WHERE tid = @tid AND bao_yang_type IN @baoYangType", para);

            return result;
        }

        /// <summary>
        /// 根据tid 类型  套餐 查配置数据
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="byType"></param>
        /// <param name="packagePid"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<IEnumerable<BaoYangPackageRefDO>> GetBaoYangPackageRefAsync(string tid, string byType,
            string packagePid, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@tid", tid);
            para.Add("@baoYangType", byType);
            para.Add("@packagePid", packagePid);

            var result =
                await GetListAsync<BaoYangPackageRefDO>(
                    "WHERE tid = @tid AND bao_yang_type = @baoYangType AND  package_id = @packagePid", para, !readOnly);

            return result;
        }

        /// <summary>
        /// 根据tidList 类型  套餐 查配置数据
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="byType"></param>
        /// <param name="packagePid"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<List<BaoYangPackageRefDO>> GetBaoYangPackageRefAsync(List<string> tidList, string byType,
            string packagePid, bool readOnly = true)
        {
            var para = new DynamicParameters();
            para.Add("@tidList", tidList);
            para.Add("@baoYangType", byType);
            para.Add("@packagePid", packagePid);

            var result =
                await GetListAsync<BaoYangPackageRefDO>(
                    "WHERE tid IN @tidList AND bao_yang_type = @baoYangType AND  package_id = @packagePid", para, !readOnly);

            return result?.ToList() ?? new List<BaoYangPackageRefDO>();
        }
    }
}
