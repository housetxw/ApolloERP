using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using Ae.BaoYang.Service.Dal.Model.Extend;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangPartAccessoryRepository : IRepository<BaoYangPartAccessoryDO>
    {
        /// <summary>
        /// 根据tid查询辅料配置
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPartAccessoryDO>> GetPartAccessoryByTidAsync(string tid);

        /// <summary>
        /// 根据TidList查询辅料配置
        /// </summary>
        /// <param name="tidList"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPartAccessoryDO>> GetPartAccessoryByTidListAsync(List<string> tidList);

        /// <summary>
        /// 根据tid和类型查询数据
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="accessoryName"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<BaoYangPartAccessoryDO> GetPartAccessoryByTidAndTypeAsync(string tid, string accessoryName,
            bool readOnly = true);

        /// <summary>
        /// 属性适配车型
        /// </summary>
        /// <param name="accessoryName"></param>
        /// <param name="volume"></param>
        /// <param name="viscosity"></param>
        /// <param name="level"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        Task<IEnumerable<SimpleVehicleDo>> GetTidByVolume(string accessoryName, int volume,
            List<string> viscosity = null, List<string> level = null, List<string> description = null);

        /// <summary>
        /// 删除辅料配置
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="accessoryName"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        Task<bool> DeleteAccessory(string tid, string accessoryName, string submitBy);
    }
}
