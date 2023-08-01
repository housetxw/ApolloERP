using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using Ae.BaoYang.Service.Dal.Model.Extend;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangPartsRepository : IRepository<BaoYangPartsDO>
    {
        /// <summary>
        /// 根据tid查配件配置
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPartsDO>> GetBaoYangPartsByTidAsync(string tid);

        /// <summary>
        /// 根据tid 和 partNames 查配件配置
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="partNames"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPartsDO>> GetBaoYangPartsByTidAndNamesAsync(string tid,
            List<string> partNames);

        /// <summary>
        /// 根据车型 配件名 查适配产品pid
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="partName"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPartProductDO>> GetBaoYangPartProductsAsync(string tid,
            List<string> partName);

        /// <summary>
        /// 根据配件号查询Parts
        /// </summary>
        /// <param name="partCode"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPartsDO>> GetBaoYangPartsByParCode(List<string> partCode);

        /// <summary>
        /// 根据Pid查询适配车型tid
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<IEnumerable<SimpleVehicleDo>> GetTidByPid(string pid);

        /// <summary>
        /// 根据配件号查询适配车型Tid
        /// </summary>
        /// <param name="partCode"></param>
        /// <returns></returns>
        Task<IEnumerable<SimpleVehicleDo>> GetTidByParCode(string partCode);

        /// <summary>
        /// 根据tid 和 partName PartCode 查配件配置
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="partName"></param>
        /// <param name="partCode"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPartsDO>> GetBaoYangPartsByTidAndCodeAsync(string tid, string partName,
            string partCode, bool readOnly = true);

        /// <summary>
        /// 根据TidList PartNames 查询配件适配
        /// </summary>
        /// <param name="tidList"></param>
        /// <param name="partNames"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPartsDO>> GetBaoYangParts(List<string> tidList, List<string> partNames);

        /// <summary>
        /// 配件类型 品牌 查五级车型Tid
        /// </summary>
        /// <param name="brand"></param>
        /// <param name="partName"></param>
        /// <returns></returns>
        Task<IEnumerable<SimpleVehicleDo>> GetTidByParCode(List<string> brand, List<string> partName);

        Task<IEnumerable<BaoYangPartsDO>> GetBaoYangPartsByOeCode(string oeCode, List<string> partNames, List<string> brands);
    }
}
