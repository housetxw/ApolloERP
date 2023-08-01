using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.BaoYang.Service.Dal.Model;

namespace Ae.BaoYang.Service.Dal.Repository
{
    public interface IBaoYangPartAccessoryMapRepository
    {
        /// <summary>
        /// 获取辅料关联baoYangType配置
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BaoYangPartAccessoryMapDO>> GetParAccessoryMapAsync();
    }
}
