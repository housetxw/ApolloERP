using Ae.BaoYang.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangConfigRepository
    {
        /// <summary>
        /// 获取保养Xml配置
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        Task<string> FetchBaoYangConfigByKeyAsync(string configName);

        /// <summary>
        /// 保养配件配置查询
        /// </summary>
        /// <param name="tidList"></param>
        /// <param name="specialPart"></param>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPartsAdaptationDO>> GetBaoYangPartAdaptationsAsync(List<string> tidList,
            List<string> specialPart);

        /// <summary>
        /// 添加partCode
        /// </summary>
        /// <param name="baoYangPartsDo"></param>
        /// <returns></returns>
        Task<int> InsertBaoYangPartCodeAsync(BaoYangPartsDO baoYangPartsDo);

        /// <summary>
        /// 根据id删除配件
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="id"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        Task<bool> DeletePartCodeByIdAsync(string tid, int id, string updatedBy);

        /// <summary>
        /// 根据OE件号删除配件
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="partName"></param>
        /// <param name="oePartCode"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        Task<bool> DeletePartCodeByOeCodeAsync(string tid, string partName, string oePartCode, string updatedBy);

        /// <summary>
        /// 删除所有OE号
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="partNames"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        Task<bool> DeletePartCodeByPartNamesAsync(string tid, List<string> partNames, string updatedBy);

        /// <summary>
        /// 修改OE件号
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="partNames"></param>
        /// <param name="originalOePart"></param>
        /// <param name="oePartCode"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        Task<bool> UpdateOePartCodeAsync(string tid, List<string> partNames, string originalOePart,
            string oePartCode, string updatedBy);

        /// <summary>
        /// 修改零件号
        /// </summary>
        /// <param name="tid"></param>
        /// <param name="id"></param>
        /// <param name="partCode"></param>
        /// <param name="brand"></param>
        /// <param name="updatedBy"></param>
        /// <returns></returns>
        Task<bool> UpdatePartCodeAsync(string tid, int id, string partCode, string brand, string updatedBy);
    }
}
