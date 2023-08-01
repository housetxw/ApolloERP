using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangPackageRefRepository : IRepository<BaoYangPackageRefDO>
    {
        /// <summary>
        /// 根据Tid查询适配套餐
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPackageRefDO>> GetBaoYangPackageRefByTidAsync(string tid);

        /// <summary>
        /// 查询车型已绑定套餐
        /// </summary>
        /// <param name="tidList"></param>
        /// <param name="packageId"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPackageRefDO>> GetBaoYangPackageRefByTidsAsync(List<string> tidList,
            string packageId);

        /// <summary>
        /// 根据tid and baoYangType查询套餐
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="baoYangType"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPackageRefDO>> GetBaoYangPackageRefByParaAsync(string tid,
            List<string> baoYangType);

        /// <summary>
        /// 根据tid 类型  套餐 查配置数据
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="byType"></param>
        /// <param name="packagePid"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPackageRefDO>> GetBaoYangPackageRefAsync(string tid, string byType,
            string packagePid, bool readOnly = true);

        /// <summary>
        /// 根据tidList 类型  套餐 查配置数据
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="byType"></param>
        /// <param name="packagePid"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<List<BaoYangPackageRefDO>> GetBaoYangPackageRefAsync(List<string> tidList, string byType,
            string packagePid, bool readOnly = true);
    }
}
