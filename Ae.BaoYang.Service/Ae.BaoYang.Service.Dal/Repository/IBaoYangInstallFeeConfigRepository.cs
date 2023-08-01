using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.BaoYang.Service.Dal.Model;
using Ae.BaoYang.Service.Dal.Model.Condition;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangInstallFeeConfigRepository : IRepository<BaoYangInstallFeeConfigDO>
    {
        /// <summary>
        /// 查询安装服务费加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<BaoYangInstallFeeConfigDO>> GetBaoYangInstallFeeConfigPageList(
            BaoYangInstallFeeConfigPageListCondition request);

        /// <summary>
        /// pid查询安装服务配置
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<List<BaoYangInstallFeeConfigDO>> GetInstallFeeConfigByPid(string serviceId,
            bool readOnly = true);

        /// <summary>
        /// pidList查询安装服务配置
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="guidPrice"></param>
        /// <returns></returns>
        Task<List<BaoYangInstallFeeConfigDO>> GetInstallFeeConfigByPidList(List<string> serviceId,
            decimal guidPrice);
    }
}
