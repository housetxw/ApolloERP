using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangInstallTypeConfigRepository
    {
        /// <summary>
        /// 保养安装方式配置
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BaoYangInstallTypeConfigDO>> GetBaoYangInstallTypeConfigAsync();
    }
}
