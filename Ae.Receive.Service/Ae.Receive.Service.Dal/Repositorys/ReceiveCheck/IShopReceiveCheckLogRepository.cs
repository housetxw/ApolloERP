using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public interface IShopReceiveCheckLogRepository : IRepository<ShopReceiveCheckLogDo>
    {
        /// <summary>
        /// 检查报告日志查询
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<List<ShopReceiveCheckLogDo>> GetShopReceiveCheckLog(long checkId, int categoryId);

        /// <summary>
        /// 批量查询检查报告日志
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        Task<List<ShopReceiveCheckLogDo>> GetReceiveCheckLogByCheckIds(List<long> checkIds, int categoryId);

        /// <summary>
        /// 批量删除上次检查结果数据
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <param name="checkModule"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        Task<int> BatchDeleteLastData(long checkId, int categoryId, string checkModule, string submitBy);

        /// <summary>
        /// 批量删除上次检查结果数据（签字）
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <param name="checkModule"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        Task<int> BatchDeleteLastSignatureData(long checkId, int categoryId, string checkModule, string submitBy);

        /// <summary>
        /// 批量删除上次检查结果数据（升级项目数据）
        /// </summary>
        /// <param name="checkId"></param>
        /// <param name="categoryId"></param>
        /// <param name="checkModule"></param>
        /// <param name="submitBy"></param>
        /// <returns></returns>
        Task<int> BatchDeleteLastUpgradeData(long checkId, int categoryId, string checkModule, string submitBy);
    }
}
